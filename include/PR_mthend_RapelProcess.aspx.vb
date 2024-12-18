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

Public Class PR_mthend_RapelProcess : Inherits Page

    Protected WithEvents rbAllEmp As RadioButton
    Protected WithEvents rbSelectedEmp As RadioButton
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents ddlAppMonth As DropDownList
    Protected WithEvents ddlEffMonth As DropDownList
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
    Protected WithEvents lblErrAppMonth As Label
    Protected WithEvents lblErrEffMonth As Label
    Protected WithEvents lblErrCheckMonth As Label


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
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")
        strParam = strCompany & "|" & strLocation & "|" & strUserId



        Try
            intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCdGet,strCompany,strLocation, strUserId, objSysDs, strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_PROCESS_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        
        If objSysDs.Tables(0).Rows.Count > 0 Then
            strPhyYear = objSysDs.Tables(0).Rows(0).Item("PhyYear") 
            strPhyMonth = objSysDs.Tables(0).Rows(0).Item("PhyMonth") 
        End If    
        

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRapel), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrSetup.Visible = False
            lblNoRecord.Visible = False
            lblErrModule.Visible = False
            lblSuccess.Visible = False
            lblFailed.Visible = False


            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoIncentive), intConfig) = True Then
                blnAutoIncentive = True
            End If
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoLabourOverheadDist), intConfig) = True Then
                blnAutoLabOverheadDist = True
            End If
                

            If Not Page.IsPostBack Then
                onLoad_PayrollSetup()
                BindEmployee()
                BindPeriod("App")
                BindPeriod("Eff")
            End If
        End If
        
        CheckRapelProcess()

    End Sub

    Sub onLoad_PayrollSetup()
        Dim strOpCdGet As String = "PR_CLSSETUP_PAYROLLPROCESS_GET"
        Dim strParam As String = "|"
        Dim intErrNo As Integer

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objPayDs)
        Catch Exp As Exception
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
        Catch Exp As Exception
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
        Dim strOpCdSP As String = "PR_CLSMTHEND_RAPELPROCESS_SP"
        Dim objResult As Object
        Dim objDataSet As Object
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAutoConfirm As String
        Dim strEmpCode As String = ""
        Dim intNumWorkDay As Integer
        Dim strAppMonth As String 
        Dim strEffMonth As String 
        


        if ddlAppMonth.SelectedItem.Value = "Select Approval Month" Then
            lblErrAppMonth.Visible = True
            Exit Sub
        Else 
            lblErrAppMonth.Visible = False
        End if

        if ddlEffMonth.SelectedItem.Value = "Select Effective Month" Then
            lblErrEffMonth.Visible = True
            Exit Sub
        Else 
            lblErrEffMonth.Visible = False
        End if
            
        If lblCompleteSetup.text = "no" Then
            lblErrSetup.Visible = True
            Exit Sub
        End If

        If rbSelectedEmp.Checked Then 
            strEmpCode = ddlEmployee.SelectedItem.Value 
        End If

        strAppMonth = ddlAppMonth.SelectedItem.Value
        strEffMonth = ddlEffMonth.SelectedItem.Value

        strParam = strAccMonth & "|" & _
                   strAccYear & "|" & strPhyMonth & "|" & strPhyYear & "|1|" & _
                   strAppMonth & "|" & strEffMonth & "|" & strEmpCode 

        Try
            intErrNo = objPR.mtdRapelProcess_SP(strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strOpCdSP, _
                                                  strParam, _
                                                  objResult)
        Catch Exp As Exception
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

    Sub BindPeriod(ByVal pv_strFlagMonth as String)
        Dim intPhyMonth As Integer
        Dim intAccMonth As Integer


        Select case pv_strFlagMonth
        Case "App"
            ddlAppMonth.Items.Add(New ListItem("Select Approval Month"))
            ddlAppMonth.Items.Add(New ListItem("January","1"))
            ddlAppMonth.Items.Add(New ListItem("February","2"))
            ddlAppMonth.Items.Add(New ListItem("March","3"))
            ddlAppMonth.Items.Add(New ListItem("April","4"))
            ddlAppMonth.Items.Add(New ListItem("May","5"))
            ddlAppMonth.Items.Add(New ListItem("June","6"))
            ddlAppMonth.Items.Add(New ListItem("July","7"))
            ddlAppMonth.Items.Add(New ListItem("August","8"))
            ddlAppMonth.Items.Add(New ListItem("September","9"))
            ddlAppMonth.Items.Add(New ListItem("October","10"))
            ddlAppMonth.Items.Add(New ListItem("November","11"))
            ddlAppMonth.Items.Add(New ListItem("December","12"))

        Case "Eff"
            ddlEffMonth.Items.Add(New ListItem("Select Effective Month"))
            ddlEffMonth.Items.Add(New ListItem("January","1"))
            ddlEffMonth.Items.Add(New ListItem("February","2"))
            ddlEffMonth.Items.Add(New ListItem("March","3"))
            ddlEffMonth.Items.Add(New ListItem("April","4"))
            ddlEffMonth.Items.Add(New ListItem("May","5"))
            ddlEffMonth.Items.Add(New ListItem("June","6"))
            ddlEffMonth.Items.Add(New ListItem("July","7"))
            ddlEffMonth.Items.Add(New ListItem("August","8"))
            ddlEffMonth.Items.Add(New ListItem("September","9"))
            ddlEffMonth.Items.Add(New ListItem("October","10"))
            ddlEffMonth.Items.Add(New ListItem("November","11"))
            ddlEffMonth.Items.Add(New ListItem("December","12"))
        End Select
    End Sub

    Sub onChange_EffMonth(Sender As Object, E As EventArgs)
        Dim strAppMonth as String = Request.Form("ddlAppMonth")

        If strAppMonth <> "" Then
            If Trim(ddlEffMonth.SelectedItem.Value) = strAppMonth Then
                lblErrCheckMonth.Visible = True
                btnGenerate.Enabled = False             
            Else
                lblErrCheckMonth.Visible = False
                btnGenerate.Enabled = True              
            End If
        End If   
    End Sub

    Sub onChange_AppMonth(Sender As Object, E As EventArgs)
        Dim strEffMonth as String = Request.Form("ddlEffMonth")

        If strEffMonth <> "" Then
            If Trim(ddlAppMonth.SelectedItem.Value) = strEffMonth Then
                lblErrCheckMonth.Visible = True
                btnGenerate.Enabled = False             
            Else
                lblErrCheckMonth.Visible = False
                btnGenerate.Enabled = True              
            End If
        End If   
    End Sub

    Sub CheckRapelProcess()
        Dim strOpCode As String = "PR_CLSMTHEND_RAPELPROCESS_SEARCH"
        Dim strParam as String 
        Dim strEmpCode as String 
        Dim SortExp as String 
        Dim intErrNo As Integer

        SortExp = ""        
        strParam = " WHERE PhyYear = '" & Trim(strPhyYear) & "' "

        If rbSelectedEmp.Checked Then
            strEmpCode = ddlEmployee.SelectedItem.Value
            strParam = strParam & " AND EmpCode = '" & Trim(strEmpCode) & "'"
        End If

        strParam = SortExp & "|" & strParam

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCode, strParam, 0, objEmpDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_RAPELPROCESS_SEARCH&errmesg=&redirect=")
        End Try

        If objEmpDs.Tables(0).Rows.Count > 0 Then
            btnGenerate.Visible = false
        Else 
            btnGenerate.Visible = true
        End If
        
    End Sub
        

End Class
