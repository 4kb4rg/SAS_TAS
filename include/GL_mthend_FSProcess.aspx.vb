Imports System
Imports System.Data
Imports System.Math
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.GL.clsMthEnd

Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter


Public Class GL_mthend_FSProcess : Inherits Page


    Dim objAR As New agri.GlobalHdl.clsAccessRights
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objGLTrx As New agri.GL.ClsTrx
    Protected objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompanyName As String
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String = ""
    Dim strAccYear As String = ""
    Dim intConfig As Integer
    Dim intGLAR As Integer
    Dim objLangCapDs As New DataSet
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strAcceptDateFormat As String
    Dim DateOfPeriod As Date
    Dim DateOfBeforePeriod As Date
    Dim DateOfLastPeriod As Date
    Dim strLocLevel As String
    Dim intLevel As Integer


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompanyName = Session("SS_COMPANYNAME")
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intConfig = Session("SS_CONFIGSETTING")
        intGLAR = Session("SS_GLAR")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLocLevel = Session("SS_LOCLEVEL")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            lblErrProcess.Visible = False

            If strLocLevel <> objAdminLoc.EnumLocLevel.HQ Then
                btnProceed.Visible = False
                btnPreview.Visible = False
            Else
                If intLevel > 2 Then
                    btnProceed.Visible = True
                Else
                    btnProceed.Visible = False
                End If
            End If

            btnPreview.Visible = True
            GetFSPeriod()

            If Not Page.IsPostBack Then
                btnProceed.Attributes("onclick") = "javascript:return ConfirmAction('Process Financial Stetement (Please do not cancel until any message appears) ');"
                OnLoad_Display("")

            End If

        End If
    End Sub

    Sub GetFSPeriod()
        Dim intErrNo As Integer
        Dim dsResult As New Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String

        strOpCode = "GL_CLSSETUP_FS_LASTPERIOD_GET"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsResult)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_FS_LASTPERIOD_GET&errmesg=" & Exp.Message.ToString & "&redirect=")
        End Try

        If dsResult.Tables(0).Rows.Count > 0 Then
            rbMethod1.Text = " Direct Value (from TB Amount)" + " <i>(Latest period : " + Trim(dsResult.Tables(0).Rows(0).Item("Period")) + " " + Trim(dsResult.Tables(0).Rows(0).Item("AccYear")) + ")</i>"
        End If

        If dsResult.Tables(1).Rows.Count > 0 Then
            rbMethod2.Text = " Indirect Value (recalculating)" + " <i>(Latest period : " + Trim(dsResult.Tables(1).Rows(0).Item("Period")) + " " + Trim(dsResult.Tables(1).Rows(0).Item("AccYear")) + ")</i>"
        End If
    End Sub

    Sub OnLoad_Display(ByVal pv_strAccYear As String)
        Dim objAccCfg As New DataSet()
        Dim strOpCd_AccCfg_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_GET"
        Dim intMaxPeriod As Integer
        Dim intAccMonth As Integer
        Dim _strAccYear As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strParam As String

        If pv_strAccYear = "" Then
            ddlAccYear.Items.Clear()


            For intCnt = (Convert.ToInt16(strAccYear) - 1) To IIf(UCase(Trim(strCompany)) = "STA", Convert.ToInt16(strAccYear) + 2, Convert.ToInt16(strAccYear) + 1)
                ddlAccYear.Items.Add(New ListItem(intCnt, intCnt))
            Next

            'ddlAccYear.SelectedIndex = ddlAccYear.Items.Count - 2
            ddlAccYear.Text = strAccYear
            _strAccYear = ddlAccYear.SelectedItem.Value
        Else
            _strAccYear = pv_strAccYear
        End If

        ddlAccMonth.Items.Clear()
        If _strAccYear = strAccYear Then
            intAccMonth = 12
            For intCnt = 1 To intAccMonth
                ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            Next
            'ddlAccMonth.SelectedIndex = Val(strAccMonth)
            ddlAccMonth.Text = Val(strAccMonth)
        Else
            Try
                strParam = "||" & _strAccYear
                intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_AccCfg_Get, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        objAccCfg)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNALADJ_DETAIL_ACCCFG_GET&errmesg=" & Exp.ToString() & "&redirect=GL/trx/GL_trx_JournalAdj_List.aspx")
            End Try

            Try
                btnProceed.Visible = True
                intAccMonth = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                lblErrProcess.Visible = True
                btnProceed.Visible = False
            End Try
            objAccCfg = Nothing

            For intCnt = 1 To 12
                ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            Next
            'ddlAccMonth.SelectedIndex = Val(strAccMonth)
            ddlAccMonth.Text = Val(strAccMonth)
        End If
    End Sub

    Sub OnIndexChage_ReloadAccPeriod(ByVal sender As Object, ByVal e As System.EventArgs)
        'OnLoad_Display(ddlAccYear.SelectedItem.Value)
    End Sub

    Sub btnProceed_Click(ByVal Sender As Object, ByVal E As System.EventArgs)
        Dim intErrNo As Integer
        Dim dsResult As New Object
        Dim objRptDs As New DataSet()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCdGetReport As String = ""
        Dim strDetail As String = "1"
        Dim strSelAccMonth As String
        Dim strSelAccYear As String

        Dim objDataSet As New Object()

        Dim intEndPeriod As Integer
        Dim intSelPeriod As Integer
        Dim strEndAccMonth As String = ""
        Dim strEndAccYear As String = ""

        strEndAccMonth = IIf(Session("SS_GLACCMONTH") <> "", Session("SS_GLACCMONTH"), Month(Now()))
        strEndAccYear = IIf(Session("SS_GLACCYEAR") <> "", Session("SS_GLACCYEAR"), Year(Now()))

        strAccMonth = ddlAccMonth.SelectedValue
        strAccYear = ddlAccYear.SelectedValue

        intEndPeriod = (CInt(strEndAccYear) * 100) + CInt(strEndAccMonth)
        intSelPeriod = (CInt(strAccYear) * 100) + CInt(strAccMonth)

        If intSelPeriod < intEndPeriod Then
            lblErrProcess.Visible = True
            lblErrProcess.Text = "Invalid period"
            Exit Sub
        End If

        strSelAccMonth = ddlAccMonth.SelectedItem.Value
        strSelAccYear = ddlAccYear.SelectedItem.Value

        strOpCdGetReport = "GL_FS_REPORT_MONTHEND_PROCESS"
        strParamName = "ACCYEAR|ACCMONTH|USERID"
        strParamValue = strAccYear & "|" & strAccMonth & "|" & strUserId

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCdGetReport,
                                                strParamName,
                                                strParamValue,
                                                objRptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_StdRpt_FS&errmesg=" & lblErrProcess.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            UserMsgBox(Me, objRptDs.Tables(0).Rows(0).Item("Msg"))
            optGroup.Checked = True
            ReportCOGS("1")

        Else
            lblErrProcess.Visible = True
            Exit Sub
        End If

    End Sub

    Sub optGroup_CheckedChanged(sender As Object, e As EventArgs)
        If optGroup.Checked = True Then
            optExpand.Checked = False
            optGroup.Checked = True
            ReportCOGS("1")
        End If
    End Sub

    Sub optExpand_CheckedChanged(sender As Object, e As EventArgs)
        If optExpand.Checked = True Then
            optExpand.Checked = True
            optGroup.Checked = False
            ReportCOGS("2")
        End If
    End Sub
    Sub PreviewBtn_Click(ByVal Sender As Object, ByVal E As System.EventArgs)
        Dim strMethod As String

        optExpand.Checked = False
        optGroup.Checked = True
        ReportCOGS("1")

    End Sub

    Sub ReportCOGS(ByVal pTipe As String)
        Dim strOpCd_Get As String = "GL_FS_REPORT_COGS"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer

        strParamName = "ACCYEAR|ACCMONTH|RPTCODE|RPTTYPE"
        strParamValue = ddlAccYear.SelectedItem.Value & "|" & ddlAccMonth.SelectedItem.Value & "|" & "COGSNEW" & "|" & pTipe

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get,
                                         strParamName,
                                         strParamValue,
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrProcess.Text & "&redirect=")
        End Try

        TABBK.Visible = True
        dgCOGS.Visible = True
        dgCOGS.DataSource = Nothing
        dgCOGS.DataSource = objdsST.Tables(0)
        dgCOGS.DataBind()

        Dim vSpace As String = ""

        For intCnt = 0 To dgCOGS.Items.Count - 1
            If CType(dgCOGS.Items(intCnt).FindControl("lblFBold"), Label).Text = "1" Then
                dgCOGS.Items(intCnt).Font.Bold = True
            End If

            Select Case CType(dgCOGS.Items(intCnt).FindControl("lblSpace"), Label).Text
                Case "1"
                    vSpace = "&nbsp&nbsp"
                Case "2"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp"
                Case "3"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                Case "4"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                Case "5"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                Case "6"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                Case "7"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                Case Else
                    vSpace = ""
            End Select

            CType(dgCOGS.Items(intCnt).FindControl("lblDescription"), Label).Text = vSpace & CType(dgCOGS.Items(intCnt).FindControl("lblDescription"), Label).Text
        Next
    End Sub


    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INVOICERCV_GET_CONFIG&errmesg=" & Exp.Message.ToString & "&redirect=pu/trx/pu_polist.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                            pv_strInputDate, _
                                            strAcceptDateFormat, _
                                            objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Public Function GetTotDayInMonth(ByVal dMonth As Integer, ByVal dYear As Integer) As Integer
        Dim dDate As Date
        Dim lDate As Date
        Dim dDay As Integer
        Dim dSunday As Integer = 0
        Dim dWeek As Integer = 0
        Dim cDay As Integer = 0

        dDate = dMonth & "/1/" & dYear
        Do While Month(dDate) = dMonth
            If DatePart(DateInterval.Weekday, CDate(dDate)) = vbSunday Then
                dSunday = dSunday + 1
            End If

            If Month(dDate) = dMonth Then
                If DatePart(DateInterval.Weekday, CDate(dDate)) = vbSaturday Then
                    dWeek = dWeek + 1
                End If
            End If

            dDate = DateAdd(DateInterval.Day, 1, CDate(dDate))
            If Month(dDate) <> dMonth Then
                lDate = DateAdd(DateInterval.Day, -1, CDate(dDate))
                If DatePart(DateInterval.Weekday, CDate(lDate)) <> vbSaturday Then
                    dWeek = dWeek + 1
                End If
            End If
        Loop
        dDate = DateAdd(DateInterval.Day, -1, CDate(dDate))
        dDay = DatePart(DateInterval.Day, dDate)
        GetTotDayInMonth = dDay '- dSunday
    End Function

    Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub
End Class
