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


Public Class PR_mthend_BonusProcess : Inherits Page

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


    Dim objHR As New agri.HR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPR As New agri.PR.clsMthEnd()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objEmpDs As New Object()
    Dim objPayDs As New Object()
    Dim objSysDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strPhyMonth as String
    Dim strPhyYear as String
    Dim strDateFmt as String   
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim blnAutoIncentive As Boolean = False
    Dim blnAutoLabOverheadDist As Boolean = False
    Dim strCostLevel As String
    Dim CurDate as String
    Dim CurDt as Date

    Sub Page_Load(Sender As Object, E As EventArgs)
        Dim strOpCdGet As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim strParam As String = "|"
        Dim intErrNo As Integer

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strDateFmt = Session("SS_DATEFMT")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strCostLevel = Session("SS_COSTLEVEL")
        strParam = strCompany & "|" & strLocation & "|" & strUserId

        Try
            intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCdGet,strCompany,strLocation, strUserId, objSysDs, strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_PROCESS_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        
        If objSysDs.Tables(0).Rows.Count > 0 Then
            strPhyYear = objSysDs.Tables(0).Rows(0).Item("PhyYear") 
            strPhyMonth = objSysDs.Tables(0).Rows(0).Item("PhyMonth") 
        End If    
        

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthBonus), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrSetup.Visible = False
            lblNoRecord.Visible = False
            lblErrModule.Visible = False
            lblSuccess.Visible = False
            lblFailed.Visible = False


                btnGenerate.Visible = True
                btnGenerate.Enabled = False

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
            If objPayDs.Tables(0).Rows(0).Item("SalaryADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("DailyRateADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("PieceRateADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("BonusADCode").Trim() = "" Or _
                objPayDs.Tables(0).Rows(0).Item("RiceADCode").Trim() = "" Then

                lblCompleteSetup.text = "no"
            Else
                lblCompleteSetup.text = "yes"
            End If
        Else
            lblCompleteSetup.text = "no"
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

    Sub Check_Clicked(Sender As Object, E As EventArgs)
        If rbSelectedEmp.Checked Then
            ddlEmployee.Enabled = True
        Else
            ddlEmployee.Enabled = False
        End If
    End Sub

    
    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCdSP As String = "PR_CLSMTHEND_BONUSPROCESS_SP"

        Dim objResult As Object
        Dim objDataSet As Object
        Dim strParam As String
        Dim intErrNo As Integer

        Dim strAutoConfirm As String
        Dim strEmpCode As String = ""
        Dim intNumWorkDay As Integer
        
        If lblCompleteSetup.text = "no" Then
            lblErrSetup.Visible = True
            Exit Sub
        End If

        If rbSelectedEmp.Checked Then 
            strEmpCode = ddlEmployee.SelectedItem.Value 
        End If

        strParam = strAccMonth & "|" & _
                   strAccYear & "|" & _
                   strEmpCode 



        Try
            intErrNo = objPR.mtdBonusProcess_SP(strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strOpCdSP, _
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
        End If
    End Sub

End Class
