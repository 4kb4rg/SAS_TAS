Imports System
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

Public Class BD_StdRpt_VehRunningSum : Inherits Page

    Protected RptSelect As UserControl

    Dim objBD As New agri.BD.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblVehType As Label
    Protected WithEvents lblVeh As Label
    Protected WithEvents lblVehExp As Label
    Protected WithEvents lstVehType As DropDownList

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindVehType()
            End If

        End If
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()
        lblVeh.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblVehType.Text = GetCaption(objLangCap.EnumLangCap.VehType) & " Code :"
        lblVehExp.Text = GetCaption(objLangCap.EnumLangCap.VehExpense)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If Trim(strLocType) = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub BindVehType()
        Dim strOppCd_VehRunning_VehType_GET As String = "BD_STDRPT_VEHRUNNING_VEHTYPE_GET"
        Dim strParam As String
        Dim objRptDsVehRun As New DataSet()
        Dim intCnt As Integer

        strParam = objGLSetup.EnumVehTypeStatus.Active & "|"
        Try
            intErrNo = objBD.mtdGetVehType(strOppCd_VehRunning_VehType_GET, strParam, objRptDsVehRun)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_VEH_RUNNING_VEHTYPE_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To objRptDsVehRun.Tables(0).Rows.Count - 1
            lstVehType.Items.Add(New ListItem(Trim(objRptDsVehRun.Tables(0).Rows(intCnt).Item("VehTypeCode")), Trim(objRptDsVehRun.Tables(0).Rows(intCnt).Item("VehTypeCode"))))
        Next

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strVehType As String

        Dim strddlPeriodID As String
        Dim strddlPeriodName As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strDec As String

        Dim tempPeriod As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim lblPeriod As Label

        tempPeriod = RptSelect.FindControl("lstPeriodName")
        strddlPeriodID = Trim(tempPeriod.SelectedItem.Value)
        strddlPeriodName = Trim(tempPeriod.SelectedItem.Text)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)


        strVehType = Trim(lstVehType.SelectedItem.Value)

        If strddlPeriodID = 0 Then
            lblPeriod = RptSelect.FindControl("lblPeriod")
            lblPeriod.Visible = True
            Exit Sub
        Else
            lblPeriod = RptSelect.FindControl("lblPeriod")
            lblPeriod.Visible = False
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""BD_StdRpt_VehRunningSumPreview.aspx?DDLPeriodID=" & strddlPeriodID & "&DDLPeriodName=" & strddlPeriodName & "&lblVehType=" & lblVehType.Text & "&lblVeh=" & lblVeh.Text & _
                       "&lblVehExp= " & lblVehExp.Text & "&VehType=" & strVehType & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
