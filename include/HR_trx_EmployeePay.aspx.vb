Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic
Imports agri.HR
Imports agri.PR
Imports agri.GL
Imports agri.PWSystem
Imports agri.GlobalHdl
Imports agri.Admin

Public Class HR_trx_EmployeePay : Inherits Page

    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblEmpName As Label
    Protected WithEvents ddlPayType As DropDownList
    Protected WithEvents lblPayRate As Label
    Protected WithEvents ddlPayMode As DropDownList

    Protected WithEvents ddlBankCode1 As DropDownList
    Protected WithEvents txtBankAccNo1 As TextBox
    Protected WithEvents ddlBankCode2 As DropDownList
    Protected WithEvents txtBankAccNo2 As TextBox
    Protected WithEvents ddlBankCode3 As DropDownList
    Protected WithEvents txtBankAccNo3 As TextBox

    Protected WithEvents txtPortionAmount1 As TextBox
    Protected WithEvents txtPortionRate1 As TextBox
    Protected WithEvents txtPortionAmount2 As TextBox
    Protected WithEvents txtPortionRate2 As TextBox
    Protected WithEvents txtPortionAmount3 As TextBox
    Protected WithEvents txtPortionRate3 As TextBox

    Protected WithEvents lblPayRateHid As Label
    Protected WithEvents lblAllowanceHid As Label
    Protected WithEvents lblDeductionHid As Label

    Protected WithEvents ddlTranIncCode As DropDownList
    Protected WithEvents lblTranIncCode As Label
    Protected WithEvents trTranInc As HtmlTableRow
    Protected WithEvents trHarvInc As HtmlTableRow

    Protected WithEvents txtAccHdl1 As TextBox
    Protected WithEvents txtAccHdl2 As TextBox
    Protected WithEvents txtAccHdl3 As TextBox

    Protected WithEvents lblErrAccHdlEmpty1 As Label
    Protected WithEvents lblErrAccHdlEmpty2 As Label
    Protected WithEvents lblErrAccHdlEmpty3 As Label

    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents cbDeferPayInd As CheckBox
    Protected WithEvents txtDeferRemark As TextBox
    Protected WithEvents cbLevyInd As CheckBox
    Protected WithEvents cbOTInd As CheckBox
    Protected WithEvents ddlHarvIncCode As DropDownList
    Protected WithEvents txtPayslipNote As TextBox
    Protected WithEvents lblAllowance As Label
    Protected WithEvents lblDeduction As Label
    Protected WithEvents lblEmpStatus As Label
    Protected WithEvents ddlRiceRation As DropDownList
    Protected WithEvents ddlIncentive As DropDownList
    Protected WithEvents ddlDenda As DropDownList
    Protected WithEvents cbAbsDeductInd As CheckBox
    Protected WithEvents cbTwicePayInd As CheckBox
    Protected WithEvents hidPayType As HtmlInputHidden
    Protected WithEvents hidPayMode As HtmlInputHidden

    Protected WithEvents lblRiceRationValue As Label
    Protected WithEvents lblQuotaLevelDesc As Label
    Protected WithEvents lblQuotaLevelValue As Label
    Protected WithEvents lblIndQuotaTag As Label
    Protected WithEvents lblIndQuotaValue As Label
    Protected WithEvents lblIndQuotaMethodTag As Label
    Protected WithEvents lblIndQuotaMethodDesc As Label
    Protected WithEvents lblIndQuotaMethodValue As Label
    Protected WithEvents lblIndQuotaIncRate As Label
    Protected WithEvents lblNotApplicable As Label
    Protected WithEvents lblActivity As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblIndividual As Label
    Protected WithEvents lblQuotaIncRateTag As Label

    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblRiceRation As Label
    Protected WithEvents lblRiceCode As Label
    Protected WithEvents lblIncentiveCode As Label

    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrBankInfo1 As Label
    Protected WithEvents lblErrBankInfo2 As Label
    Protected WithEvents lblErrBankInfo3 As Label
    Protected WithEvents lblErrPortion As Label
    Protected WithEvents lblErrRateAmount As Label
    Protected WithEvents lblErrAccEmpty2 As Label
    Protected WithEvents lblErrAccEmpty3 As Label
    Protected WithEvents lblErrEmpty1 As Label
    Protected WithEvents lblErrEmpty2 As Label
    Protected WithEvents lblErrEmpty3 As Label
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnFind As HtmlInputButton

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbDetails As LinkButton
    Protected WithEvents lbEmployment As LinkButton
    Protected WithEvents lbStatutory As LinkButton
    Protected WithEvents lbFamily As LinkButton
    Protected WithEvents lbQualific As LinkButton
    Protected WithEvents lbSkill As LinkButton
    Protected WithEvents lbCareerProg As LinkButton

    Protected WithEvents ddlDefAccCode As DropDownList
    Protected WithEvents ddlDefBlkCode As DropDownList
    Protected WithEvents lblErrDefAccCode As Label
    Protected WithEvents lblErrDefBlkCode As Label



    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objAdmin As New agri.Admin.clsLoc()

    Dim objEmpPayDs As New Object()
    Dim objBankDs As New Object()
    Dim objVehDs As New Object()
    Dim objAttdDs As New Object()
    Dim objHarvDs As New Object()
    Dim objRiceDs As New Object()
    Dim objIncDs As New Object()
    Dim objDendaDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objTranDs As New Object()

    Dim objDefAccDs As New Object()
    Dim objDefBlkDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long
    Dim strCostLevel As String
    Dim intConfig As Integer

    Dim strSelectedEmpCode As String = ""
    Dim strSelectedEmpName As String = ""
    Dim strSelectedEmpStatus As String = ""
    Dim strSortExpression As String = "EmpCode"
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strCostLevel = Session("SS_COSTLEVEL")
        strLocType = Session("SS_LOCTYPE")
        intConfig = Session("SS_CONFIGSETTING")

        If Trim(Session("SS_LOCTYPE")) = objAdmin.EnumLocType.Mill Then
            trTranInc.Visible = True
            trHarvInc.Visible = False
        ElseIf Trim(Session("SS_LOCTYPE")) = objAdmin.EnumLocType.Estate Then
            trTranInc.Visible = False
            trHarvInc.Visible = True
        End If
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeePayroll), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblIndQuotaTag.Visible = False
            lblIndQuotaValue.Visible = False
            lblIndQuotaMethodTag.Visible = False
            lblIndQuotaMethodDesc.Visible = False
            lblIndQuotaMethodValue.Visible = False
            lblQuotaIncRateTag.Visible = False
            lblIndQuotaIncRate.Visible = False  
            lblErrBankInfo1.Visible = False
            lblErrBankInfo2.Visible = False
            lblErrBankInfo3.Visible = False
            lblErrPortion.Visible = False
            lblErrRateAmount.Visible = False
            lblErrAccEmpty2.Visible = False
            lblErrAccEmpty3.Visible = False
            lblErrEmpty1.Visible = False
            lblErrEmpty2.Visible = False
            lblErrEmpty3.Visible = False

            lblErrAccHdlEmpty1.Visible = False
            lblErrAccHdlEmpty2.Visible = False
            lblErrAccHdlEmpty3.Visible = False

            'simon
            lblErrDefAccCode.Visible = False
            lblErrDefBlkCode.Visible = False


            onload_GetLangCap()
            lblRedirect.Text = Request.QueryString("redirect")
            strSelectedEmpCode = Trim(IIf(Request.QueryString("EmpCode") <> "", Request.QueryString("EmpCode"), Request.Form("EmpCode")))
            strSelectedEmpName = Trim(IIf(Request.QueryString("EmpName") <> "", Request.QueryString("EmpName"), Request.Form("EmpName")))
            strSelectedEmpStatus = Trim(IIf(Request.QueryString("EmpStatus") <> "", Request.QueryString("EmpStatus"), Request.Form("EmpStatus")))
            If Not IsPostBack Then
                If strSelectedEmpCode <> "" Then
                    BindEmpPay(strSelectedEmpCode)
                    OnLoad_BindButton()
                     onload_LinkButton() 
                End If
            End If
        End If
    End Sub

    Sub onload_LinkButton()
        If lblEmpStatus.Text = 0 Then
            TrLink.Visible = False
        Else
            TrLink.Visible = True
        End If
    End Sub

    Private Sub lbDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDetails.Click
        Response.Redirect("HR_trx_EmployeeDet.aspx?redirect=empdet&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbEmployment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEmployment.Click
        Response.Redirect("HR_trx_EmployeeEmp.aspx?redirect=empemp&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbStatutory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbStatutory.Click
        Response.Redirect("HR_trx_EmployeeStat.aspx?redirect=empstat&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFamily.Click
        Response.Redirect("HR_trx_EmployeeFamList.aspx?redirect=empfam&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbQualific_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbQualific.Click
        Response.Redirect("HR_trx_EmployeeQlfList.aspx?redirect=empqlf&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)

    End Sub

    Private Sub lbSkill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSkill.Click
        Response.Redirect("HR_trx_EmployeeSkillList.aspx?redirect=empskill&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)

    End Sub

    Private Sub lbCareerProg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCareerProg.Click
        Response.Redirect("HR_trx_EmployeeCPList.aspx?redirect=empcp&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Sub BindEmpPay(ByVal pv_strEmpCode As String)
        Dim strOpCd_EmpPay As String = "HR_CLSTRX_PAYROLL_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        lblEmpCode.Text = strSelectedEmpCode
        lblEmpName.Text = strSelectedEmpName
        lblEmpStatus.Text = strSelectedEmpStatus

        strParam = strSelectedEmpCode & "|||" & strSortExpression & "|"

        Try
            intErrNo = objHRTrx.mtdGetEmployeePay(strOpCd_EmpPay, strParam, objEmpPayDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_GET&errmesg=" & Exp.ToString() & "&redirect=hr/trx/hr_trx_employeelist.aspx")
        End Try

        If objEmpPayDs.Tables(0).Rows.Count > 0 Then
            BindPayType(objEmpPayDs.Tables(0).Rows(0).Item("PayType"))
            hidPayType.Value = IIf(objEmpPayDs.Tables(0).Rows(0).Item("PayType") = "", 0, objEmpPayDs.Tables(0).Rows(0).Item("PayType"))

            lblPayRate.Text = ObjGlobal.GetIDDecimalSeparator(objEmpPayDs.Tables(0).Rows(0).Item("PayRate"))
            lblPayRateHid.Text = CInt(objEmpPayDs.Tables(0).Rows(0).Item("PayRate"))
            lblAllowanceHid.Text = CInt(objEmpPayDs.Tables(0).Rows(0).Item("Allowance"))
            lblDeductionHid.Text = CInt(objEmpPayDs.Tables(0).Rows(0).Item("Deduction"))
            BindPayMode(objEmpPayDs.Tables(0).Rows(0).Item("PayMode"))
            hidPayMode.Value = objEmpPayDs.Tables(0).Rows(0).Item("PayMode")
            BindBank(objEmpPayDs.Tables(0).Rows(0).Item("BankCode"))
            txtBankAccNo1.Text = objEmpPayDs.Tables(0).Rows(0).Item("BankAccNo")
            BindVeh(objEmpPayDs.Tables(0).Rows(0).Item("VehCode"))

            If objEmpPayDs.Tables(0).Rows(0).Item("DeferPayInd") = objHRTrx.mtdGetDeferPayStatus(objHRTrx.EnumDeferPayStatus.Yes) Then
                cbDeferPayInd.Checked = True
            Else
                cbDeferPayInd.Checked = False
            End If 

            txtDeferRemark.Text = objEmpPayDs.Tables(0).Rows(0).Item("DeferRemark")
            If objEmpPayDs.Tables(0).Rows(0).Item("LevyInd") = objHRTrx.mtdGetLevyStatus(objHRTrx.EnumLevyStatus.Yes) Then
                cbLevyInd.Checked = True
            Else
                cbLevyInd.Checked = False
            End If 

            If objEmpPayDs.Tables(0).Rows(0).Item("OTInd") = objHRTrx.mtdGetOTStatus(objHRTrx.EnumOTStatus.Yes) Then
                cbOTInd.Checked = True
            Else
                cbOTInd.Checked = False
            End If 

            BindHarv(objEmpPayDs.Tables(0).Rows(0).Item("HarvIncCode"))
            BindTran(objEmpPayDs.Tables(0).Rows(0).Item("TranIncCode"))
            txtPayslipNote.Text = objEmpPayDs.Tables(0).Rows(0).Item("PayslipNote")
            lblAllowance.Text =  ObjGlobal.GetIDDecimalSeparator(objEmpPayDs.Tables(0).Rows(0).Item("Allowance"))
            lblDeduction.Text =  ObjGlobal.GetIDDecimalSeparator(objEmpPayDs.Tables(0).Rows(0).Item("Deduction"))
            lblRiceRationValue.Text =  ObjGlobal.GetIDDecimalSeparator(objEmpPayDs.Tables(0).Rows(0).Item("RiceRation"))
            lblQuotaLevelValue.Text = objEmpPayDs.Tables(0).Rows(0).Item("QuotaLevel")
            lblIndQuotaValue.Text = objEmpPayDs.Tables(0).Rows(0).Item("IndQuota")
            lblIndQuotaMethodValue.Text = objEmpPayDs.Tables(0).Rows(0).Item("IndQuotaMethod")
            lblIndQuotaMethodDesc.Text = objHRTrx.mtdGetIndQuotaMethod(CInt(objEmpPayDs.Tables(0).Rows(0).Item("IndQuotaMethod").Trim()))
            lblIndQuotaIncRate.Text = objEmpPayDs.Tables(0).Rows(0).Item("IndQuotaIncRate")
            BindRiceRation(objEmpPayDs.Tables(0).Rows(0).Item("RiceRationCode"), objEmpPayDs.Tables(0).Rows(0).Item("Category"))
            BindIncentive(objEmpPayDs.Tables(0).Rows(0).Item("IncentiveCode"))
            BindDenda(objEmpPayDs.Tables(0).Rows(0).Item("DendaCode"))
            BindBank2(objEmpPayDs.Tables(0).Rows(0).Item("BankCode2"))
            BindBank3(objEmpPayDs.Tables(0).Rows(0).Item("BankCode3"))

            'simon
            BindAccount(objEmpPayDs.Tables(0).Rows(0).Item("AccCode"))
            BindBlkCode(objEmpPayDs.Tables(0).Rows(0).Item("AccCode"), objEmpPayDs.Tables(0).Rows(0).Item("BlkCode"))

            txtBankAccNo2.Text = objEmpPayDs.Tables(0).Rows(0).Item("BankAccNo2")
            txtBankAccNo3.Text = objEmpPayDs.Tables(0).Rows(0).Item("BankAccNo3")
            txtPortionRate1.Text = IIF(objEmpPayDs.Tables(0).Rows(0).Item("PortionRate1")= "0","",objEmpPayDs.Tables(0).Rows(0).Item("PortionRate1"))
            txtPortionRate2.Text = IIF(objEmpPayDs.Tables(0).Rows(0).Item("PortionRate2")= "0","",objEmpPayDs.Tables(0).Rows(0).Item("PortionRate2"))
            txtPortionRate3.Text = IIF(objEmpPayDs.Tables(0).Rows(0).Item("PortionRate3")= "0","",objEmpPayDs.Tables(0).Rows(0).Item("PortionRate3"))
            txtPortionAmount1.Text = IIF(objEmpPayDs.Tables(0).Rows(0).Item("PortionAmount1")= "0","",objEmpPayDs.Tables(0).Rows(0).Item("PortionAmount1"))
            txtPortionAmount2.Text = IIF(objEmpPayDs.Tables(0).Rows(0).Item("PortionAmount2")= "0","",objEmpPayDs.Tables(0).Rows(0).Item("PortionAmount2"))
            txtPortionAmount3.Text = IIF(objEmpPayDs.Tables(0).Rows(0).Item("PortionAmount3")= "0","",objEmpPayDs.Tables(0).Rows(0).Item("PortionAmount3"))
            
            txtAccHdl1.Text = objEmpPayDs.Tables(0).Rows(0).Item("AccountHolder1")
            txtAccHdl2.Text = objEmpPayDs.Tables(0).Rows(0).Item("AccountHolder2")
            txtAccHdl3.Text = objEmpPayDs.Tables(0).Rows(0).Item("AccountHolder3")

            If objEmpPayDs.Tables(0).Rows(0).Item("AbsDeductInd") = objHRTrx.EnumAbsDeductInd.Yes Then
                cbAbsDeductInd.Checked = True
            Else
                cbAbsDeductInd.Checked = False
            End If

            If objEmpPayDs.Tables(0).Rows(0).Item("TwicePayInd") = objHRTrx.EnumTwicePayInd.Yes Then
                cbTwicePayInd.Checked = True
            End If
            If objEmpPayDs.Tables(0).Rows(0).Item("QuotaLevel") = objHRTrx.EnumQuotaLevel.NoQuota Then
                lblQuotaLevelDesc.Text = lblNotApplicable.Text
            ElseIf objEmpPayDs.Tables(0).Rows(0).Item("QuotaLevel") = objHRTrx.EnumQuotaLevel.Activity Then
                lblQuotaLevelDesc.Text = lblActivity.Text
            ElseIf objEmpPayDs.Tables(0).Rows(0).Item("QuotaLevel") = objHRTrx.EnumQuotaLevel.Block Then
                lblQuotaLevelDesc.Text = lblBlock.Text
            Else
                lblQuotaLevelDesc.Text = lblIndividual.Text
                lblIndQuotaTag.Visible = True
                lblIndQuotaValue.Visible = True
                lblIndQuotaMethodTag.Visible = True
                lblIndQuotaMethodDesc.Visible = True
                lblQuotaIncRateTag.Visible = True
                lblIndQuotaIncRate.Visible = True
            End If
        Else
            BindPayType("0")
            BindPayMode("0")
            BindBank("")
            BindBank2("")
            BindBank3("")
            BindVeh("")
            BindHarv("")
            BindTran("")
            BindRiceRation("", "")
            BindIncentive("")
            BindDenda("")
            BindAccount("")
            BindBlkCode("", "")
        End If
    End Sub

    Sub OnLoad_BindButton()
        ddlPayMode.Enabled = False
        ddlBankCode1.Enabled = False
        txtBankAccNo1.Enabled = False
        ddlBankCode2.Enabled = False
        txtBankAccNo2.Enabled = False
        ddlBankCode3.Enabled = False
        txtBankAccNo3.Enabled = False
        txtPortionRate1.Enabled = False
        txtPortionRate2.Enabled = False
        txtPortionRate3.Enabled = False
        txtPortionAmount1.Enabled = False
        txtPortionAmount2.Enabled = False
        txtPortionAmount3.Enabled = False
        txtAccHdl1.Enabled = False
        txtAccHdl2.Enabled = False
        txtAccHdl3.Enabled = False

        ddlVehCode.Enabled = False
        cbDeferPayInd.Enabled = False
        txtDeferRemark.Enabled = False
        ddlHarvIncCode.Enabled = False
        ddlTranIncCode.Enabled = False
        ddlRiceRation.Enabled = False
        ddlIncentive.Enabled = False
        ddlDenda.Enabled = False        
        cbLevyInd.Enabled = False
        cbOTInd.Enabled = False
        txtPayslipNote.Enabled = False
        cbAbsDeductInd.Enabled = False
        cbTwicePayInd.Enabled = False
        btnSave.visible = False
        btnFind.visible = False
        Select case CInt(lblEmpStatus.Text)
            Case objHRTrx.EnumEmpStatus.Active, objHRTrx.EnumEmpStatus.Pending
                ddlPayMode.Enabled = True
                If hidPayMode.Value = objHRTrx.EnumPayMode.Bank Then
                    ddlBankCode1.Enabled = True
                    ddlBankCode2.Enabled = True
                    ddlBankCode3.Enabled = True
                End If
                txtBankAccNo1.Enabled = True
                txtBankAccNo2.Enabled = True
                txtBankAccNo3.Enabled = True
                txtPortionRate1.Enabled = True
                txtPortionRate2.Enabled = True
                txtPortionRate3.Enabled = True
                txtPortionAmount1.Enabled = True
                txtPortionAmount2.Enabled = True
                txtPortionAmount3.Enabled = True
                txtAccHdl1.Enabled = True                
                txtAccHdl2.Enabled = True 
                txtAccHdl3.Enabled = True 

                ddlVehCode.Enabled = True
                cbDeferPayInd.Enabled = True
                txtDeferRemark.Enabled = True
                ddlHarvIncCode.Enabled = True
                ddlTranIncCode.Enabled = True
                ddlIncentive.Enabled = True
                ddlDenda.Enabled = True
                cbLevyInd.Enabled = True
                cbOTInd.Enabled = True
                txtPayslipNote.Enabled = True
                cbTwicePayInd.Enabled = True
                btnSave.Visible = True
                If hidPayType.Value <> objPRSetup.EnumPayType.NoRate Then
                     ddlRiceRation.Enabled = True
                End If

                If hidPayType.Value = objPRSetup.EnumPayType.MonthlyRate Then
                    cbAbsDeductInd.Enabled = True
                Else
                    cbAbsDeductInd.Enabled = False
                End If

            Case Else
        End Select
    End Sub

    Sub BindPayType(ByVal pv_strPayType As String)
        Dim intSelectedIndex As Integer
        Dim intCnt As Integer
        ddlPayType.Items.Clear()
        ddlPayType.Items.Add(New ListItem("Select Pay Type", ""))
        ddlPayType.Items.Add(New ListItem(objPRSetup.mtdGetPayType(objPRSetup.EnumPayType.DailyRate), objPRSetup.EnumPayType.DailyRate))
        ddlPayType.Items.Add(New ListItem(objPRSetup.mtdGetPayType(objPRSetup.EnumPayType.PieceRate), objPRSetup.EnumPayType.PieceRate))
        ddlPayType.Items.Add(New ListItem(objPRSetup.mtdGetPayType(objPRSetup.EnumPayType.MonthlyRate), objPRSetup.EnumPayType.MonthlyRate))
        If Trim(pv_strPayType) <> "" Then
            For intCnt=0 To ddlPayType.Items.Count -1
                If Trim(ddlPayType.Items(intCnt).Value) = Trim(pv_strPayType) Then
                    intSelectedIndex = intCnt
                End If
            Next
        End If
        ddlPayType.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindPayMode(ByVal pv_strPayMode As String)
        ddlPayMode.Items.Clear()
        ddlPayMode.Items.Add(New ListItem("Please select Pay Mode", "0"))
        ddlPayMode.Items.Add(New ListItem(objHRTrx.mtdGetPayMode(objHRTrx.EnumPayMode.Bank), objHRTrx.EnumPayMode.Bank))
        ddlPayMode.Items.Add(New ListItem(objHRTrx.mtdGetPayMode(objHRTrx.EnumPayMode.Cheque), objHRTrx.EnumPayMode.Cheque))
        ddlPayMode.Items.Add(New ListItem(objHRTrx.mtdGetPayMode(objHRTrx.EnumPayMode.Cash), objHRTrx.EnumPayMode.Cash))
        ddlPayMode.SelectedIndex = CInt(pv_strPayMode)
        CheckPayMode()
    End Sub

    Sub CallCheckPayMode(ByVal Sender As Object, ByVal E As EventArgs)
        CheckPayMode()
    End Sub

    Sub CheckPayMode()
        If ddlPayMode.SelectedItem.Value = objHRTrx.EnumPayMode.Bank Then
            ddlBankCode1.Enabled = True
            txtBankAccNo1.Enabled = True
            ddlBankCode2.Enabled = True
            txtBankAccNo2.Enabled = True
            ddlBankCode3.Enabled = True
            txtBankAccNo3.Enabled = True
            txtPortionRate1.Enabled = True
            txtPortionRate2.Enabled = True
            txtPortionRate3.Enabled = True
            txtPortionAmount1.Enabled = True
            txtPortionAmount2.Enabled = True
            txtPortionAmount3.Enabled = True
            txtAccHdl1.Enabled = True 
            txtAccHdl2.Enabled = True
            txtAccHdl3.Enabled = True  

        Else
            ddlBankCode1.SelectedIndex = 0
            ddlBankCode1.Enabled = False
            txtBankAccNo1.Text = ""
            txtBankAccNo1.Enabled = False
            ddlBankCode2.SelectedIndex = 0
            ddlBankCode2.Enabled = False
            txtBankAccNo2.Text = ""
            txtBankAccNo2.Enabled = False
            ddlBankCode3.SelectedIndex = 0
            ddlBankCode3.Enabled = False
            txtBankAccNo3.Text = ""
            txtBankAccNo3.Enabled = False
            txtPortionRate1.Text = ""
            txtPortionRate1.Enabled = False
            txtPortionRate2.Text = ""
            txtPortionRate2.Enabled = False
            txtPortionRate3.Text = ""
            txtPortionRate3.Enabled = False
            txtPortionAmount1.Text = ""
            txtPortionAmount1.Enabled = False
            txtPortionAmount2.Text = ""
            txtPortionAmount2.Enabled = False
            txtPortionAmount3.Text = ""
            txtPortionAmount3.Enabled = False
            txtAccHdl1.Text = ""
            txtAccHdl1.Enabled = False 
            txtAccHdl2.Text = ""
            txtAccHdl2.Enabled = False 
            txtAccHdl3.Text = ""
            txtAccHdl3.Enabled = False

        End If
    End Sub

    Sub BindBank(ByVal pv_strSelectedBank As String)
        Dim strOpCd_Bank As String = "HR_CLSSETUP_BANK_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intBankIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objHRSetup.EnumBankStatus.Active & "||B.BankCode|"

        Try
            intErrNo = objHRSetup.mtdGetBank(strOpCd_Bank, strParam, objBankDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_GET_BANK&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objBankDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
                objBankDs.Tables(0).Rows(intCnt).Item("BankCode") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode"))
                objBankDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode")) & " (" & _
                                                                       Trim(objBankDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                If Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode")) = Trim(pv_strSelectedBank) Then
                    intBankIndex = intCnt + 1
                End If
            Next
        End If

        dr = objBankDs.Tables(0).NewRow()
        dr("BankCode") = ""
        dr("Description") = "Please select Bank Code"
        objBankDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBankCode1.DataSource = objBankDs.Tables(0)
        ddlBankCode1.DataTextField = "Description"
        ddlBankCode1.DataValueField = "BankCode"
        ddlBankCode1.DataBind()
        ddlBankCode1.SelectedIndex = intBankIndex
    End Sub
    Sub BindBank2(ByVal pv_strSelectedBank As String)
        Dim strOpCd_Bank As String = "HR_CLSSETUP_BANK_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intBankIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objHRSetup.EnumBankStatus.Active & "||B.BankCode|"

        Try
            intErrNo = objHRSetup.mtdGetBank(strOpCd_Bank, strParam, objBankDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_GET_BANK&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objBankDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
                objBankDs.Tables(0).Rows(intCnt).Item("BankCode") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode"))
                objBankDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode")) & " (" & _
                                                                       Trim(objBankDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                If Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode")) = Trim(pv_strSelectedBank) Then
                    intBankIndex = intCnt + 1
                End If
            Next
        End If

        dr = objBankDs.Tables(0).NewRow()
        dr("BankCode") = ""
        dr("Description") = "Please select Bank Code"
        objBankDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBankCode2.DataSource = objBankDs.Tables(0)
        ddlBankCode2.DataTextField = "Description"
        ddlBankCode2.DataValueField = "BankCode"
        ddlBankCode2.DataBind()
        ddlBankCode2.SelectedIndex = intBankIndex
    End Sub
    Sub BindBank3(ByVal pv_strSelectedBank As String)
        Dim strOpCd_Bank As String = "HR_CLSSETUP_BANK_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intBankIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objHRSetup.EnumBankStatus.Active & "||B.BankCode|"

        Try
            intErrNo = objHRSetup.mtdGetBank(strOpCd_Bank, strParam, objBankDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_GET_BANK&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objBankDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
                objBankDs.Tables(0).Rows(intCnt).Item("BankCode") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode"))
                objBankDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode")) & " (" & _
                                                                       Trim(objBankDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                If Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode")) = Trim(pv_strSelectedBank) Then
                    intBankIndex = intCnt + 1
                End If
            Next
        End If

        dr = objBankDs.Tables(0).NewRow()
        dr("BankCode") = ""
        dr("Description") = "Please select Bank Code"
        objBankDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBankCode3.DataSource = objBankDs.Tables(0)
        ddlBankCode3.DataTextField = "Description"
        ddlBankCode3.DataValueField = "BankCode"
        ddlBankCode3.DataBind()
        ddlBankCode3.SelectedIndex = intBankIndex
    End Sub
    Sub BindVeh(ByVal pv_strSelectedVeh As String)
        Dim strOpCd_Veh As String = "GL_CLSSETUP_VEH_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intVehIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|" & " LocCode = '" & strLocation & "' AND Status LIKE '" & objGLSetup.EnumVehicleStatus.Active & "' "

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd_Veh, strParam, objGLSetup.EnumGLMasterType.Vehicle, objVehDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_GET_VEHICLE&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objVehDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objVehDs.Tables(0).Rows.Count - 1
                objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode"))
                objVehDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode")) & " (" & _
                                                                       Trim(objVehDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode")) = Trim(pv_strSelectedVeh) Then
                    intVehIndex = intCnt + 1
                End If
            Next
        End If

        dr = objVehDs.Tables(0).NewRow()
        dr("VehCode") = ""
        dr("Description") = lblSelect.text & lblVehCode.text
        objVehDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehCode.DataSource = objVehDs.Tables(0)
        ddlVehCode.DataTextField = "Description"
        ddlVehCode.DataValueField = "VehCode"
        ddlVehCode.DataBind()
        ddlVehCode.SelectedIndex = intVehIndex
    End Sub


    Sub BindHarv(ByVal pv_strSelectedHarv As String)
        Dim strOpCd_Harv As String = "PR_CLSSETUP_HARVINCENTIVE_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intHarvIndex As Integer = 0
        Dim dr As DataRow

        strParam = strLocation & "|||" & objPRSetup.EnumHarvIncentiveStatus.Active & "||HI.HarvIncCode|"

        Try
            intErrNo = objPRSetup.mtdGetHarvIncentive(strOpCd_Harv, strParam, objHarvDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_GET_HARVINC&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objHarvDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objHarvDs.Tables(0).Rows.Count - 1
                objHarvDs.Tables(0).Rows(intCnt).Item("HarvIncCode") = Trim(objHarvDs.Tables(0).Rows(intCnt).Item("HarvIncCode"))
                objHarvDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objHarvDs.Tables(0).Rows(intCnt).Item("HarvIncCode")) & " (" & _
                                                                       Trim(objHarvDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If Trim(objHarvDs.Tables(0).Rows(intCnt).Item("HarvIncCode")) = Trim(pv_strSelectedHarv) Then
                    intHarvIndex = intCnt + 1
                End If
            Next
        End If
  
        dr = objHarvDs.Tables(0).NewRow()
        dr("HarvIncCode") = ""
        dr("Description") = "Please select Harvesting Incentive Code"
        objHarvDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlHarvIncCode.DataSource = objHarvDs.Tables(0)
        ddlHarvIncCode.DataTextField = "Description"
        ddlHarvIncCode.DataValueField = "HarvIncCode"
        ddlHarvIncCode.DataBind()
        ddlHarvIncCode.SelectedIndex = intHarvIndex
    End Sub
    Sub BindTran(ByVal pv_strSelectedTran As String)
        Dim strOpCd_Tran As String = "PR_CLSSETUP_TRANINCENTIVE_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intTranIndex As Integer = 0
        Dim dr As DataRow
        Dim strSearch As String

        strSearch = " AND TI.Status = '" & objPRSetup.EnumTranIncentiveStatus.Active & "'"
        strParam = "ORDER BY TI.TranIncCode ASC|" & strSearch

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd_Tran, strParam,objPRSetup.EnumPayrollMasterType.TranIncentive, objTranDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_GET_TRANINC&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeePay.aspx")
        End Try

        If objTranDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objTranDs.Tables(0).Rows.Count - 1
                objTranDs.Tables(0).Rows(intCnt).Item("TranIncCode") = Trim(objTranDs.Tables(0).Rows(intCnt).Item("TranIncCode"))
                objTranDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objTranDs.Tables(0).Rows(intCnt).Item("TranIncCode")) & " (" & _
                                                                       Trim(objTranDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If Trim(objTranDs.Tables(0).Rows(intCnt).Item("TranIncCode")) = Trim(pv_strSelectedTran) Then
                    intTranIndex = intCnt + 1
                End If
            Next
        End If
  
        dr = objTranDs.Tables(0).NewRow()
        dr("TranIncCode") = ""
        dr("Description") = "Please select " & lblTranIncCode.Text
        objTranDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTranIncCode.DataSource = objTranDs.Tables(0)
        ddlTranIncCode.DataTextField = "Description"
        ddlTranIncCode.DataValueField = "TranIncCode"
        ddlTranIncCode.DataBind()
        ddlTranIncCode.SelectedIndex = intTranIndex
    End Sub
    Sub BindRiceRation(ByVal pv_strSelRiceCode As String, ByVal pv_strPayType As String)
        Dim strOpCdGet As String = "PR_CLSSETUP_RICERATION_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "order by rc.RiceRationCode|and rc.Status = '" & objPRSetup.EnumRiceStatus.Active & "' "
        If Trim(pv_strPayType) <> "" Then 
            strParam = strParam & "and rc.Category = '" & Trim(pv_strPayType) & "' "
        End If

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCdGet, _
                                                   strParam, _
                                                   0, _
                                                   objRiceDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_GET_RICE&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objRiceDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRiceDs.Tables(0).Rows.Count - 1
                If Trim(objRiceDs.Tables(0).Rows(intCnt).Item("RiceRationCode")) = Trim(pv_strSelRiceCode) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objRiceDs.Tables(0).NewRow()
        dr("RiceRationCode") = ""
        dr("_Description") = lblSelect.Text & lblRiceCode.Text
        objRiceDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlRiceRation.DataSource = objRiceDs.Tables(0)
        ddlRiceRation.DataValueField = "RiceRationCode"
        ddlRiceRation.DataTextField = "_Description"
        ddlRiceRation.DataBind()
        ddlRiceRation.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindIncentive(ByVal pv_strSelIncentiveCode As String)
        Dim strOpCdGet As String = "PR_CLSSETUP_INCENTIVE_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "order by inc.IncentiveCode|and inc.Status = '" & objPRSetup.EnumIncentiveStatus.Active & "' And inc.LocCode = '" & strLocation & "' "

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCdGet, _
                                                   strParam, _
                                                   0, _
                                                   objIncDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_GET_INCENTIVE&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objIncDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objIncDs.Tables(0).Rows.Count - 1
                If objIncDs.Tables(0).Rows(intCnt).Item("IncentiveCode") = Trim(pv_strSelIncentiveCode) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objIncDs.Tables(0).NewRow()
        dr("IncentiveCode") = ""
        dr("_Description") = lblSelect.Text & lblIncentiveCode.Text
        objIncDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlIncentive.DataSource = objIncDs.Tables(0)
        ddlIncentive.DataValueField = "IncentiveCode"
        ddlIncentive.DataTextField = "_Description"
        ddlIncentive.DataBind()
        ddlIncentive.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindDenda(ByVal pv_strSelDendaCode As String)
        Dim strOpCdGet As String = "PR_CLSSETUP_DENDA_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "order by D.DendaCode|and D.Status = '" & objPRSetup.EnumDendaStatus.Active & "' And D.LocCode = '" & strLocation & "' "

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCdGet, _
                                                   strParam, _
                                                   0, _
                                                   objDendaDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_DENDA_SEARCH&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDendaDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDendaDs.Tables(0).Rows.Count - 1
                If objDendaDs.Tables(0).Rows(intCnt).Item("DendaCode") = Trim(pv_strSelDendaCode) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objDendaDs.Tables(0).NewRow()
        dr("DendaCode") = ""
        dr("_Description") = "Select Denda Code"
        objDendaDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDenda.DataSource = objDendaDs.Tables(0)
        ddlDenda.DataValueField = "DendaCode"
        ddlDenda.DataTextField = "_Description"
        ddlDenda.DataBind()
        ddlDenda.SelectedIndex = intSelectedIndex
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetDet As String = "HR_CLSTRX_EMPLOYEE_GET"
        Dim strOpCd_GetPay As String = "HR_CLSTRX_PAYROLL_GET"
        Dim strOpCd_GetEmp As String = "HR_CLSTRX_EMPLOYMENT_GET"
        Dim strOpCd_GetStat As String = "HR_CLSTRX_STATUTORY_GET"
        Dim strOpCd_UpdPay As String = "HR_CLSTRX_PAYROLL_UPD"
        Dim strOpCd_AddPay As String = "HR_CLSTRX_PAYROLL_ADD"
        Dim strOpCd_UpdDet As String = "HR_CLSTRX_EMPLOYEE_UPD"
        Dim intErrNo As Integer
        Dim strChkParam As String
        Dim strParam As String
        Dim strEmpCode As String = lblEmpCode.Text
        Dim strPayType As String = ddlPayType.SelectedItem.Value
        Dim strPayRate As String = Replace(Replace(lblPayRate.Text,".",""),",",".")
        Dim strPayMode As String = ddlPayMode.SelectedItem.Value
        Dim strBankCode As String = ddlBankCode1.SelectedItem.Value
        Dim strBankAccNo As String = txtBankAccNo1.Text
        Dim strBankCode2 As String = ddlBankCode2.SelectedItem.Value
        Dim strBankAccNo2 As String = txtBankAccNo2.Text
        Dim strBankCode3 As String = ddlBankCode3.SelectedItem.Value
        Dim strBankAccNo3 As String = txtBankAccNo3.Text

        Dim strPortionRate1 As String = txtPortionRate1.Text
        Dim strPortionRate2 As String = txtPortionRate2.Text
        Dim strPortionRate3 As String = txtPortionRate3.Text
        Dim strPortionAmount1 As String = Trim(txtPortionAmount1.Text)
        Dim strPortionAmount2 As String = Trim(txtPortionAmount2.Text)
        Dim strPortionAmount3 As String = Trim(txtPortionAmount3.Text)
        Dim intTotalPortionRate as Integer = CInt(IIF(strPortionRate1= "",0,strPortionRate1)) + CInt(IIF(strPortionRate2= "",0,strPortionRate2)) + CInt(IIF(strPortionRate3= "",0,strPortionRate3))
        Dim strTranIncCode As String = Trim(ddlTranIncCode.SelectedItem.Value)

        'simon
        Dim strDefAccCode As String = Trim(ddlDefAccCode.SelectedItem.Value)
        Dim strDefBlkCode As String = Trim(ddlDefBlkCode.SelectedItem.Value)

        Dim strAccHld1 As String = txtAccHdl1.Text
        Dim strAccHld2 As String = txtAccHdl2.Text
        Dim strAccHld3 As String = txtAccHdl3.Text

        Dim strDendaCode As String = ddlDenda.SelectedItem.Value
        Dim strVehCode As String = Request.Form("ddlVehCode")
        Dim strDeferPayInd As String = IIf(cbDeferPayInd.Checked = True, objHRTrx.mtdGetDeferPayStatus(objHRTrx.EnumDeferPayStatus.Yes), objHRTrx.mtdGetDeferPayStatus(objHRTrx.EnumDeferPayStatus.No))
        Dim strDeferRemark As String = txtDeferRemark.Text
        Dim strLevyInd As String = IIf(cbLevyInd.Checked = True, objHRTrx.mtdGetLevyStatus(objHRTrx.EnumLevyStatus.Yes), objHRTrx.mtdGetLevyStatus(objHRTrx.EnumLevyStatus.No))
        Dim strOTInd As String = IIf(cbOTInd.Checked = True, objHRTrx.mtdGetOTStatus(objHRTrx.EnumOTStatus.Yes), objHRTrx.mtdGetOTStatus(objHRTrx.EnumOTStatus.No))
        Dim strHarvIncCode As String = ddlHarvIncCode.SelectedItem.Value
        Dim strPayslipNote As String = txtPayslipNote.Text
        Dim strAllowance As String = lblAllowanceHid.Text
        Dim strDeduction As String = lblDeductionHid.Text
        Dim strRiceRation As String = lblRiceRationValue.Text
        Dim strRiceRationCode As String = ddlRiceRation.SelectedItem.Value
        Dim strIncentiveCode AS String = ddlIncentive.SelectedItem.Value
        Dim strAbsDeductInd As String = IIf(cbAbsDeductInd.Checked = True, objHRTrx.EnumAbsDeductInd.Yes, objHRTrx.EnumAbsDeductInd.No)
        Dim strTwicePayInd As String = IIf(cbTwicePayInd.Checked = True, objHRTrx.EnumTwicePayInd.Yes, objHRTrx.EnumTwicePayInd.No)
        Dim strQuotaLevel As String = lblQuotaLevelValue.Text
        Dim strIndQuota As String = lblIndQuotaValue.Text
        Dim strIndQuotaMethod As String = lblIndQuotaMethodValue.Text
        Dim strIndQuotaIncRate As String = lblIndQuotaIncRate.Text

        If strPayMode = objHRTrx.EnumPayMode.Bank Then
            If strBankCode = "" Or strBankAccNo = "" Then
                lblErrBankInfo1.Visible = True
                Exit Sub
            End If
        End If
            If (strBankCode = "" or strBankAccNo = "") AND (strBankCode2 <> "" or strBankAccNo2 <> "") Then
                lblErrBankInfo2.Visible = True
                Exit Sub
            End If
            If (strBankCode <> "" or strBankAccNo <> "") AND (strBankCode2 = "" or strBankAccNo2 = "") AND (strBankCode3 <> "" Or strBankAccNo3 <> "") Then
                lblErrBankInfo3.Visible = True
                Exit Sub
            End If
            If strBankCode <> "" or strBankCode2 <> "" or strBankCode3 <> "" Then
                If (strPortionRate1 <> "" or strPortionRate2 <> "" Or strPortionRate3 <> "" ) AND _
                    (strPortionAmount1 <> "" or strPortionAmount2 <> "" or strPortionAmount3 <> "") Then
                    lblErrRateAmount.Visible = True
                    Exit Sub
                End If
            End If
            If strBankCode2 <> "" and strBankAccNo2 ="" Then
                lblErrAccEmpty2.Visible = True
                Exit Sub
            End If
            If strBankCode3 <> "" and strBankAccNo3 ="" Then
                lblErrAccEmpty3.Visible = True
                Exit Sub
            End If
            If strBankCode <> "" and strPortionRate1 = "" and strPortionAmount1 = "" Then
                lblErrEmpty1.Visible = True
                Exit Sub
            End If
            If strBankCode2 <> "" and strPortionRate2 = "" and strPortionAmount2 = ""  Then
                lblErrEmpty2.Visible = True
                Exit Sub
            End If
            If strBankCode3 <> "" and strPortionRate3 = "" and strPortionAmount3 = "" Then
                lblErrEmpty3.Visible = True
                Exit Sub
            End If
            If (strBankCode <> "" or strBankCode2 <> "" or strBankCode3 <> "") AND (strPortionRate1 <> "" or strPortionRate2 <> "" Or strPortionRate3 <> "" ) Then   
                    If intTotalPortionRate < 100 or intTotalPortionRate > 100  Then
                        lblErrPortion.Visible = True
                        Exit Sub
                    End If               
            End If

         If strBankCode <> "" and strAccHld1 = "" then
            lblErrAccHdlEmpty1.Visible = True
            Exit Sub
         End If
         If strBankCode2 <> "" and strAccHld2 = "" then
            lblErrAccHdlEmpty2.Visible = True
            Exit Sub
         End If
         If strBankCode3 <> "" and strAccHld3 = "" then
            lblErrAccHdlEmpty3.Visible = True
            Exit Sub
        End If

        'Simon
        If TRIM(strDefAccCode) = "" Then
            lblErrDefAccCode.Visible = True
            Exit Sub
        End If

        If TRIM(strDefBlkCode) = "" Then
            lblErrDefBlkCode.Visible = True
            Exit Sub
        End If



        strPortionRate1 = IIF(txtPortionRate1.Text = "", 0, txtPortionRate1.Text)
        strPortionRate2 = IIF(txtPortionRate2.Text = "", 0, txtPortionRate2.Text)
        strPortionRate3 = IIF(txtPortionRate3.Text = "", 0, txtPortionRate3.Text)
        strPortionAmount1 = IIF(txtPortionAmount1.Text = "", 0, txtPortionAmount1.Text)
        strPortionAmount2 = IIF(txtPortionAmount2.Text = "", 0, txtPortionAmount2.Text)
        strPortionAmount3 = IIF(txtPortionAmount3.Text = "", 0, txtPortionAmount3.Text)


        strParam = strEmpCode & "|" & strPayType & "|" & strPayRate & "|" & strPayMode & "|" & _
                   strBankCode & "|" & strBankAccNo & "|" & strVehCode & "|" & strDeferPayInd & "|" & _
                   strDeferRemark & "|" & strLevyInd & "|" & strOTInd & "|" & strHarvIncCode & "|" & _
                   strPayslipNote & "|" & strAllowance & "|" & strDeduction & "|" & strRiceRationCode & "|" & _
                   strIncentiveCode & "|" & strAbsDeductInd & "|" & strTwicePayInd & "|" & strQuotaLevel & "|" & _
                   strIndQuota & "|" & strIndQuotaMethod & "|" & strIndQuotaIncRate & "|" & strRiceRation & "|" & _
                   strDendaCode & "|" & strBankCode2 & "|" & strBankCode3 & "|" & strBankAccNo2 & "|" & strBankAccNo3 & "|" & _
                   strPortionRate1 & "|" & strPortionRate2 & "|" & strPortionRate3 & "|" & strPortionAmount1 & "|" & _
                   strPortionAmount2 & "|" & strPortionAmount3 & "|" & _
                   strAccHld1 & "|" & strAccHld2 & "|" & strAccHld3 & "|" & strTranIncCode & "|" & strDefAccCode & "|" & strDefBlkCode

        Try
            intErrNo = objHRTrx.mtdUpdEmployeePay(strOpCd_GetPay, _
                                                  strOpCd_UpdPay, _
                                                  strOpCd_AddPay, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_UPD&errmesg=" & Exp.ToString() & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        strChkParam = strEmpCode & "|" & lblRedirect.Text

        Try
            intErrNo = objHRTrx.mtdCheckEmployee(strOpCd_GetDet, _
                                                 strOpCd_GetPay, _
                                                 strOpCd_GetEmp, _
                                                 strOpCd_GetStat, _
                                                 strOpCd_UpdDet, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_CHECK&errmesg=" & Exp.ToString() & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        strSelectedEmpCode = strEmpCode
        BindEmpPay(strSelectedEmpCode)
        OnLoad_BindButton()
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeList.aspx?redirect=" & lblRedirect.Text)
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblRiceRation.Text = GetCaption(objLangCap.EnumLangCap.RiceRation)
        lblRiceCode.Text = GetCaption(objLangCap.EnumLangCap.RiceRation) & lblCode.Text
        lblIncentiveCode.Text = GetCaption(objLangCap.EnumLangCap.Incentive) & lblCode.Text
        lblActivity.Text = GetCaption(objLangCap.EnumLangCap.Activity)
        lbCareerProg.Text = GetCaption(objLangCap.EnumLangCap.CareerProgress)   
        If strCostLevel = "block" Then
            lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block)
        Else
            lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
        End If
        lblQuotaIncRateTag.Text = GetCaption(objLangCap.EnumLangCap.QuotaIncentive) & " Rate :"
        lblTranIncCode.Text = GetCaption(objLangCap.EnumLangCap.TransportIncentive) & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_PAYROLL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                If strLocType = objAdmin.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & _
                                 objGLSetup.EnumAccountCodeStatus.Active & _
                                 "' And ACC.AccType = '" & objGLSetup.EnumAccountType.ProfitAndLost & "'" & _
                                 " And ACC.AccPurpose = '" & objGLSetup.EnumAccountPurpose.NonVehicle & "'"

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objDefAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_DEFACC_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objDefAccDs.Tables(0).Rows.Count - 1
            objDefAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objDefAccDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objDefAccDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDefAccDs.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(objDefAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDefAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDefAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Please Select One"
        objDefAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDefAccCode.DataSource = objDefAccDs.Tables(0)
        ddlDefAccCode.DataValueField = "AccCode"
        ddlDefAccCode.DataTextField = "Description"
        ddlDefAccCode.DataBind()
        ddlDefAccCode.SelectedIndex = intSelectedIndex
        ddlDefAccCode.AutoPostBack = True
    End Sub

    Sub onSelect_DefAccount(ByVal Sender As Object, ByVal E As EventArgs)
       
        BindBlkCode(Request.Form("ddlDefAccCode"), Request.Form("ddlDefBlkCode"))
      
    End Sub

    Sub BindBlkCode(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)

        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfig) = True Then
                strOpCd = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            End If

            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                    strParam, _
                                                    objDefBlkDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_DEFBLK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objDefBlkDs.Tables(0).Rows.Count - 1
            objDefBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objDefBlkDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objDefBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDefBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objDefBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDefBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDefBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Please Select One"
        objDefBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDefBlkCode.DataSource = objDefBlkDs.Tables(0)
        ddlDefBlkCode.DataValueField = "BlkCode"
        ddlDefBlkCode.DataTextField = "Description"
        ddlDefBlkCode.DataBind()
        ddlDefBlkCode.SelectedIndex = intSelectedIndex

    End Sub


End Class
