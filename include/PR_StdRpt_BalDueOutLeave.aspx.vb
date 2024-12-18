Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services

Public Class PR_StdRpt_BalDueOutLeave : Inherits Page

    Protected RptSelect As UserControl

    Dim objPR As New agri.PR.clsReport()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblErrEmpCodeFrom As Label
    Protected WithEvents lblErrEmpCodeTo As Label
    Protected WithEvents txtAttdCode As TextBox
    Protected WithEvents ddlPayType As DropDownList
    Protected WithEvents txtNoWorkDays As TextBox
    Protected WithEvents txtEmpCodeFrom As TextBox
    Protected WithEvents txtEmpCodeTo As TextBox
    Protected WithEvents ddlEmpStatus As DropDownList
    Protected WithEvents txtGangCode As TextBox
    Protected WithEvents trWorkDays As HtmlTableRow
    Protected WithEvents PrintPrev As ImageButton

    Protected WithEvents lblAttdCode As Label

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim strAttdCode As String
    Dim intCnt As Integer
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            lblErrEmpCodeFrom.Visible = False
            lblErrEmpCodeTo.Visible = False

            If Not Page.IsPostBack Then
                DisplayXmlParameters(strAttdCode)
                lblAttdCode.Text = strAttdCode

                If strAttdCode.Trim <> "" Then
                    txtAttdCode.Text = strAttdCode
                End If

                onload_GetLangCap()
                BindPayType()
                BindEmpStatus()
                GetWorkingDays()
            End If
        End If
    End Sub


    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        Dim ucTrDecimal As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

        ucTrDecimal = RptSelect.FindControl("TrDecimal")
        ucTrDecimal.Visible = False

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_BALDUEOUTLEAVE_CLSLANGCAP_BUSSTERM_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
        End Try

    End Sub



    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function


    Sub BindPayType()
        Dim strText = "All"
        ddlPayType.Items.Add(New ListItem(strText))
        ddlPayType.Items.Add(New ListItem(objPRSetup.mtdGetPayType(objPRSetup.EnumPayType.DailyRate), objPRSetup.EnumPayType.DailyRate))
        ddlPayType.Items.Add(New ListItem(objPRSetup.mtdGetPayType(objPRSetup.EnumPayType.MonthlyRate), objPRSetup.EnumPayType.MonthlyRate))
        ddlPayType.Items.Add(New ListItem(objPRSetup.mtdGetPayType(objPRSetup.EnumPayType.PieceRate), objPRSetup.EnumPayType.PieceRate))
    End Sub

    Sub BindEmpStatus()

        ddlEmpStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.All), objHRTrx.EnumEmpStatus.All))
        ddlEmpStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.Active), objHRTrx.EnumEmpStatus.Active))
        ddlEmpStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.Terminated), objHRTrx.EnumEmpStatus.Terminated))

        ddlEmpStatus.SelectedIndex = 1

    End Sub

    Sub CheckPayType(ByVal sender As Object, ByVal e As System.EventArgs)
        If ddlPayType.SelectedItem.Value = "All" Then
            trWorkDays.Visible = False
        ElseIf ddlPayType.SelectedItem.Value = objPRSetup.EnumPayType.MonthlyRate Then
            trWorkDays.Visible = True
        Else
            trWorkDays.Visible = False
        End If
    End Sub

    Sub DisplayXmlParameters(ByRef pr_strAttdCode As String)

        Dim objStreamReader As StreamReader
        Dim objDtsXml As New DataSet()
        Dim intErrNo As Integer
        Dim strFtpPath As String
        Dim strFile As String

        pr_strAttdCode = ""

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_GET_FTPPATH_DISPLAY&errmesg=" & Exp.ToString() & "&redirect=reports/pr_stdrpt_baldueoutleave.aspx")
        End Try

        strFile = strFtpPath & "param\PR_REPORT_PARAM.xml"

        objDtsXml.EnforceConstraints = False
        objDtsXml.ReadXml(strFile)

        If objDtsXml.Tables(0).Rows.Count > 0 Then
            pr_strAttdCode = objDtsXml.Tables(0).Rows(intCnt).Item("AttendanceCode")
        End If
        objDtsXml = Nothing
    End Sub

    Sub UpdateXmlParameters()
        Dim objStreamReader As StreamReader
        Dim strXmlSrc As String
        Dim intErrNo As Integer
        Dim strFtpPath As String
        Dim strFile As String

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STD_RPT_GET_FTPPATH_UPD&errmesg=" & Exp.ToString() & "&redirect=reports/pr_stdrpt_baldueoutleave.aspx")
        End Try

        strFile = strFtpPath & "param\PR_REPORT_PARAM.xml"

        objStreamReader = File.OpenText(strFile)
        strXmlSrc = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        strXmlSrc = Replace(strXmlSrc, "<AttendanceCode>" & lblAttdCode.Text & "</AttendanceCode>", "<AttendanceCode>" & txtAttdCode.Text.Trim & "</AttendanceCode>")

        Dim objStreamWrite As StreamWriter = New StreamWriter(strFile)
        objStreamWrite.Write(strXmlSrc)
        objStreamWrite.Close()

        lblAttdCode.Text = txtAttdCode.Text.Trim

    End Sub

    Sub GetWorkingDays()

        Dim strOppCode_GET As String = "PR_STDRPT_WORKDAYS_BALDUEOUTLEAVE_GET"
        Dim objPaySetupDs As New DataSet()
        Dim strParam As String
        Dim intErrNo As Integer

        Try
            strParam = "|"
            intErrNo = objPRSetup.mtdGetMasterList(strOppCode_GET, _
                                                   strParam, _
                                                   0, _
                                                   objPaySetupDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_BALDUEOUTLEAVE_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
        End Try

        txtNoWorkDays.Text = Trim(objPaySetupDs.Tables(0).Rows(0).Item("Workday"))

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strAttdCode As String
        Dim strPayType As String
        Dim strNoWorkDays As String
        Dim strEmpCodeFrom As String
        Dim strEmpCodeTo As String
        Dim strEmpStatus As String
        Dim strGangCode As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)

        If strUserLoc = "" Then
            templblUL = RptSelect.FindControl("lblUserLoc")
            templblUL.Visible = True
            Exit Sub
        Else
            If Left(strUserLoc, 3) = "','" Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
            ElseIf Right(strUserLoc, 3) = "','" Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
            End If
        End If

        strAttdCode = txtAttdCode.Text.Trim
        strPayType = ddlPayType.SelectedItem.Value.Trim
        strNoWorkDays = txtNoWorkDays.Text.Trim

        If txtEmpCodeFrom.Text.Trim <> "" And txtEmpCodeTo.Text.Trim = "" Then
            lblErrEmpCodeTo.Visible = True
            Exit Sub
        ElseIf txtEmpCodeFrom.Text.Trim = "" And txtEmpCodeTo.Text.Trim <> "" Then
            lblErrEmpCodeFrom.Visible = True
            Exit Sub
        Else
            strEmpCodeFrom = txtEmpCodeFrom.Text.Trim
            strEmpCodeTo = txtEmpCodeTo.Text.Trim
        End If

        strEmpStatus = ddlEmpStatus.SelectedItem.Value.Trim
        strGangCode = txtGangCode.Text.Trim

        strGangCode = Server.UrlEncode(strGangCode)

        UpdateXmlParameters()

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_BalDueOutLeavePreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptID=" & strRptID & _
                       "&RptName=" & strRptName & _
                       "&lblLocation=" & lblLocation.Text & _
                       "&AttdCode=" & strAttdCode & _
                       "&PayType=" & strPayType & _
                       "&Workdays=" & strNoWorkDays & _
                       "&EmpCodeFrom=" & strEmpCodeFrom & _
                       "&EmpCodeTo=" & strEmpCodeTo & _
                       "&EmpStatus=" & strEmpStatus & _
                       "&GangCode=" & strGangCode & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
