Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.DateAndTime

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.HR
Imports agri.PR
Imports agri.GlobalHdl
Imports agri.PWSystem


Public Class PR_Trx_GenerateAttendance : Inherits Page

    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lblCompleteSetup As Label

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblSuccess As Label
    Protected WithEvents lblErrModule As Label
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblFailed As Label
    Protected WithEvents lblErrSetup As Label
    Protected WithEvents lblPeriod As Label

    Protected WithEvents txtProDateFrom As TextBox
    Protected WithEvents lblErrProcessDateFrom As Label
    Protected WithEvents lblErrProcessDateDescFrom As Label
    Protected WithEvents btnSelDate As Image

    Protected WithEvents txtProDateTo As TextBox
    Protected WithEvents lblErrProcessDateTo As Label
    Protected WithEvents lblErrProcessDateDescTo As Label

    Protected WithEvents ddlDeptCode As DropDownList

    Dim objAdmin As New agri.Admin.clsAccPeriod()
    Dim objHR As New agri.HR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPR As New agri.PR.clsMthEnd()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objHRSetup As New agri.HR.clsSetup()

    Dim objEmpDs As New Object()
    Dim objPayDs As New Object()
    Dim objDeptDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim blnAutoIncentive As Boolean = False
    Dim blnAutoLabOverheadDist As Boolean = False
    Dim strCostLevel As String
    Dim strDateFmt As String
    Dim recCount As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strCostLevel = Session("SS_COSTLEVEL")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthDaily), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrSetup.Visible = False
            lblNoRecord.Visible = False
            lblErrModule.Visible = False
            lblSuccess.Visible = False
            lblFailed.Visible = False
            lblPeriod.Text = Trim(strAccMonth) & "/" & Trim(strAccYear)
            If Not Page.IsPostBack Then
                txtProDateFrom.Text = objGlobal.GetShortDate(strDateFmt, Now())
                txtProDateTo.Text = objGlobal.GetShortDate(strDateFmt, Now())
                BindDept("")
            End If
        End If
    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdSP As String = "PR_CLSMTHEND_DAILYPROCESS_SP"
        Dim objResult As Object
        Dim objDataSet As Object
        Dim strParam As String
        Dim intErrNo As Integer
        Dim objFormatDate As String
        Dim objActualDate As String

        Dim strAutoConfirm As String
        Dim strEmpCode As String = ""
        Dim intNumWorkDay As Integer
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_GET"
        Dim objAccPeriodDs As New Object()
        Dim strPhyMonth As String
        Dim strPhyYear As String

        If lblCompleteSetup.text = "no" Then
            lblErrSetup.Visible = True
            Exit Sub
        End If

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtProDateFrom.Text, _
                                       objFormatDate, _
                                       objActualDate) = False Then
            lblErrProcessDateDescFrom.Text = lblErrProcessDateDescFrom.Text & objFormatDate
            lblErrProcessDateDescFrom.Visible = True
        End If

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtProDateTo.Text, _
                                       objFormatDate, _
                                       objActualDate) = False Then
            lblErrProcessDateDescTo.Text = lblErrProcessDateDescTo.Text & objFormatDate
            lblErrProcessDateDescTo.Visible = True
        End If

        strParam = strCompany & "|" & strLocation
        Try
            intErrNo = objAdmin.mtdGetAccPeriod(strOpCd, _
                                                strParam, _
                                                objAccPeriodDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_ACCPERIOD&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If (objAccPeriodDs.Tables(0).Rows.Count > 0) Then
            strPhyMonth = objAccPeriodDs.Tables(0).Rows(0).Item("PhyMonth")
            strPhyYear = objAccPeriodDs.Tables(0).Rows(0).Item("PhyYear")
        End If

        'strParam = strAccMonth & "|" & _
        '           strAccYear & "|" & _
        '           objActualDate & "|" & _
        '           txtNoHarvester.Text & "|" & _
        '           strChkAuto & "|" & _
        '           strPhyMonth & "|" & _
        '           strPhyYear

        Try
            intErrNo = objPR.mtdDailyProcess_SP(strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strOpCdSP, _
                                                  strParam, _
                                                  objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_DAILYPROCESS_GENERATE&errmesg=&redirect=")
        End Try

        If objResult = 0 Then
            lblSuccess.Visible = True
        ElseIf objResult = 1 Then
            lblNoRecord.Visible = True
        ElseIf objResult = 2 Then
            lblErrModule.Visible = True
        ElseIf objResult = 4 Then
            lblFailed.Visible = True
        ElseIf objResult = 5 Then
            lblErrSetup.Visible = True
        End If

    End Sub

    Sub BindDept(ByVal pv_strDeptCode As String)
        Dim strOpCd_Get As String = "HR_CLSSETUP_DEPT_SEARCH1"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|" & objHRSetup.EnumDeptStatus.Active & "||A.DeptCode||"

        Try
            intErrNo = objHRSetup.mtdGetDept(strOpCd_Get, strParam, objDeptDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPDET_GET_DEPT&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDeptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDeptDs.Tables(0).Rows.Count - 1
                If Trim(objDeptDs.Tables(0).Rows(intCnt).Item("DeptCode")) = Trim(pv_strDeptCode) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objDeptDs.Tables(0).NewRow()
        dr("DeptCode") = ""
        dr("_Description") = "Please Select Department Code"
        objDeptDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDeptCode.DataSource = objDeptDs.Tables(0)
        ddlDeptCode.DataTextField = "_Description"
        ddlDeptCode.DataValueField = "DeptCode"
        ddlDeptCode.DataBind()
        ddlDeptCode.SelectedIndex = intSelectedIndex
    End Sub
End Class
