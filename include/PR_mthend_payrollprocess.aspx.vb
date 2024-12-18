Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.HR
Imports agri.PR
Imports agri.GlobalHdl
Imports agri.PWSystem

Public Class PR_mthend_payrollprocess : Inherits Page

    Protected WithEvents rbAllEmp As RadioButton
    Protected WithEvents rbSelectedEmp As RadioButton
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents cbAC As CheckBox
    Protected WithEvents txtNoWorkDay As TextBox
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lblCompleteSetup As Label

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblSuccess As Label
    Protected WithEvents lblErrModule As Label
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblFailed As Label
    Protected WithEvents lblErrSetup As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblErrDaily As Label


    Dim objHR As New agri.HR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPR As New agri.PR.clsMthEnd()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdmin As New agri.Admin.clsAccPeriod()

    Dim objEmpDs As New Object()
    Dim objPayDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strPhyMonth As String
    Dim strPhyYear As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim blnAutoIncentive As Boolean = False
    Dim blnAutoLabOverheadDist As Boolean = False
    Dim strCostLevel As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strCostLevel = Session("SS_COSTLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrSetup.Visible = False
            lblNoRecord.Visible = False
            lblErrModule.Visible = False
            lblSuccess.Visible = False
            lblFailed.Visible = False
            lblErrDaily.Visible = False
            lblPeriod.Text = Trim(strAccMonth) & "/" & Trim(strAccYear)
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoIncentive), intConfig) = True Then
                blnAutoIncentive = True
            End If
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoLabourOverheadDist), intConfig) = True Then
                blnAutoLabOverheadDist = True
            End If
            If Not Page.IsPostBack Then
                onLoad_PayrollSetup()
                BindEmployee()
            End If
        End If
    End Sub

    Sub onLoad_PayrollSetup()
        Dim strOpCdGet As String = "PR_CLSSETUP_PAYROLLPROCESS_GET"
        Dim strParam As String = "|"
        Dim intErrNo As Integer

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objPayDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_PROCESS_PAYSETUP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objPayDs.Tables(0).Rows.Count > 0 Then
            txtNoWorkDay.Text = objPayDs.Tables(0).Rows(0).Item("WorkDay")
            If objPayDs.Tables(0).Rows(0).Item("SalaryADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("DailyRateADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("PieceRateADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("BonusADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("HouseADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("HardShipADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("IncAwardADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("TransADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("LoanADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("OTADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("BIKAccomADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("BIKVehADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("BIKHPADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("GratuityADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("RetrenchCompADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("ESOSADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("ContractPayADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("INADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("CTADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("WSADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("BFEmpADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("TripADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("OutPayEmpADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("AttAllowanceADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("RiceADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("IncADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("QuotaIncADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("AbsentADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("MealADCode").Trim() = "" Then
            Else
                lblCompleteSetup.Text = "yes"
            End If
        Else
            txtNoWorkDay.Text = "0"
            lblCompleteSetup.Text = "no"
        End If
    End Sub

    Sub BindEmployee()
        Dim strOpCdGet As String = "PR_CLSMTHEND_GET_EMPLOYEELIST"
        Dim strParam As String = "|" & "and e.Status = '" & objHR.EnumEmpStatus.Active & "' and e.LocCode = '" & strLocation & "' "
        Dim intErrNo As Integer

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_PROCESS_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        ddlEmployee.DataSource = objEmpDs.Tables(0)
        ddlEmployee.DataValueField = "EmpCode"
        ddlEmployee.DataTextField = "_Description"
        ddlEmployee.DataBind()
    End Sub

    Sub Check_Clicked(ByVal Sender As Object, ByVal E As EventArgs)
        If rbSelectedEmp.Checked Then
            ddlEmployee.Enabled = True
        Else
            ddlEmployee.Enabled = False
        End If
    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdAC As String = "PR_CLSMTHEND_ATTENDANCE_AUTOCONFIRM"
        Dim strOpCdSP As String = "PR_CLSMTHEND_PAYROLLPROCESS_SP"
        Dim strOpCdDist As String = "PR_CLSMTHEND_POPULATE_MTHENDACC_SP"

        Dim strOpCdDaily As String = "PR_CLSMTHEND_RUN_DAILY_PROCESS"
        Dim objDaily As Object

        Dim objResult As Object
        Dim objDataSet As Object
        Dim strParam As String
        Dim intErrNo As Integer

        Dim strAutoConfirm As String
        Dim strEmpCode As String = ""
        Dim intNumWorkDay As Integer

        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_GET"
        Dim objAccPeriodDs As New Object()
        Dim strPhyMonth As String
        Dim strPhyYear As String

        lblErrDaily.Text = "Payroll hasn't processed yet. Kindly run the daily process first "

        If lblCompleteSetup.Text = "no" Then
            lblErrSetup.Visible = True
            Exit Sub
        End If

        If rbSelectedEmp.Checked Then
            strEmpCode = ddlEmployee.SelectedItem.Value
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





        intNumWorkDay = txtNoWorkDay.Text

        strParam = strAccMonth & "|" & _
                   strAccYear & "|" & _
                   cbAC.Checked & "|" & _
                   strEmpCode & "|" & _
                   blnAutoIncentive & "|" & _
                   intNumWorkDay & "|" & _
                   strCostLevel & "|" & _
                   blnAutoLabOverheadDist & "|" & _
                   strPhyMonth & "|" & _
                   strPhyYear

        Try
            intErrNo = objPR.mtdPayrollProcess_SP(strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strOpCdAC, _
                                                  strOpCdSP, _
                                                  strOpCdDist, _
                                                  strParam, _
                                                  objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_PROCESS_GENERATE&errmesg=&redirect=")
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
        ElseIf objResult = 7 Then
            strParam = "|"
            Try
                intErrNo = objPRSetup.mtdGetMasterList(strOpCdDaily, strParam, 0, objDaily)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_PROCESS_DAILYFLAG_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objDaily.Tables(0).Rows.Count > 0 Then
                lblErrDaily.Text = lblErrDaily.Text & " (" & Trim(objDaily.Tables(0).Rows(0).Item("DocDate")) & ") "
            End If
            lblErrDaily.Visible = True
        End If
    End Sub


End Class
