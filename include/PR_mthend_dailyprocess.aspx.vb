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


Public Class PR_mthend_dailyprocess : Inherits Page

    Protected WithEvents cbAC As CheckBox
    Protected Withevents txtNoHarvester As TextBox
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lblCompleteSetup As Label

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblSuccess As Label
    Protected WithEvents lblErrModule As Label
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblFailed As Label
    Protected WithEvents lblErrSetup As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblAdvSalary As Label

    Protected WithEvents txtProDate As TextBox
    Protected WithEvents lblErrProcessDate As Label
    Protected WithEvents lblErrProcessDateDesc As Label
    Protected WithEvents btnSelDate As Image
    
    Dim objAdmin As New agri.Admin.clsAccPeriod()    
    Dim objHR As New agri.HR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPR As New agri.PR.clsMthEnd()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objEmpDs As New Object()
    Dim objPayDs As New Object()

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

    Sub Page_Load(Sender As Object, E As EventArgs)
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
            lblAdvSalary.Visible = False
            lblPeriod.Text = Trim(strAccMonth) & "/" & Trim(strAccYear)
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoIncentive), intConfig) = True Then
                blnAutoIncentive = True
            End If
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoLabourOverheadDist), intConfig) = True Then
                blnAutoLabOverheadDist = True
            End If
            If Not Page.IsPostBack Then
                txtProDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                onLoad_PayrollSetup()
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
                objPayDs.Tables(0).Rows(0).Item("AbsentADCode").Trim() = "" Then
            Else
                lblCompleteSetup.text = "yes"
            End If
        Else
            lblCompleteSetup.text = "no"
        End If
    End Sub




    
    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCdSP As String = "PR_CLSMTHEND_DAILYPROCESS_SP"        
        Dim objResult As Object
        Dim objDataSet As Object
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strChkAuto As String         
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
                                       txtProDate.Text, _
                                       objFormatDate, _
                                       objActualDate) = False Then
            lblErrProcessDateDesc.Text = lblErrProcessDateDesc.Text & objFormatDate
            lblErrProcessDateDesc.Visible = True
        End If

        onLoad_DayEndPayLn(objActualDate)        
        IF recCount > 0 Then
            lblNoRecord.Visible = True
            Exit Sub
        End If
 
        If cbAC.Checked = True Then
            strChkAuto = "1"
            onLoad_AdvSalaryPayment(objActualDate)
            IF recCount > 0 Then
                lblAdvSalary.Visible = True
                Exit Sub
            End If
        Else
            strChkAuto = "2"
        End if

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

        strParam = strAccMonth & "|" & _
                   strAccYear & "|" & _
                   objActualDate & "|" & _
                   txtNoHarvester.Text & "|" & _
                   strChkAuto & "|" & _
                   strPhyMonth & "|" & _      
                   strPhyYear 

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

    Sub onLoad_DayEndPayLn(ByVal dtProcessDate As String)
        Dim strOpCdGet As String = "PR_CLSMTHEND_DAYENDPAYLN_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim objResult As New Object()
        strParam = dtProcessDate & "|"

        Try
            intErrNo = objPR.mtdGetDayEndPayLN(strOpCdGet, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               strAccMonth, _
                                               strAccYear, _ 
                                               strParam, _
                                               objResult)
            
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_PROCESS_DAYENDPAYLN_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        
        recCount = objResult.Tables(0).Rows(0).Item("RecordCount")
    End Sub

    Sub onLoad_AdvSalaryPayment(ByVal dtProcessDate As String)
        Dim strOpCdGet As String = "PR_CLSMTHEND_ADVSALARYPAYMENT_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim objResult As New Object()
        strParam = "|"

        Try
            intErrNo = objPR.mtdGetAdvSalaryPayment(strOpCdGet, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               strAccMonth, _
                                               strAccYear, _ 
                                               strParam, _
                                               objResult)
            
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_PROCESS_ADVSALARYPAYMENT_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        
        recCount = objResult.Tables(0).Rows(0).Item("RecordCount")
    End Sub

End Class
