
Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic
Imports agri.HR
Imports agri.PR
Imports agri.Admin
Imports agri.PWSystem
Imports agri.GlobalHdl

Public Class HR_EmployeeCPDet : Inherits Page

    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblEmpName As Label
    Protected WithEvents lblCPID As Label
    Protected WithEvents ddlCPCode As DropDownList
    Protected WithEvents lblCPType As Label
    Protected WithEvents lblPeriodInd As Label
    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents txtCeaseDate As TextBox
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents ddlCompCode As DropDownList
    Protected WithEvents ddlLocCode As DropDownList
    Protected WithEvents ddlDeptCode As DropDownList
    Protected WithEvents ddlLevelCode As DropDownList
    Protected WithEvents ddlRptTo As DropDownList
    Protected WithEvents ddlSalSchemeCode As DropDownList
    Protected WithEvents ddlSalGradeCode As DropDownList
    Protected WithEvents ddlPosCode As DropDownList
    Protected WithEvents txtProbation As TextBox
    Protected WithEvents ddlPayType As DropDownList
    Protected WithEvents lblCurrentRate As Label
    Protected WithEvents txtIncrementAmt As TextBox
    Protected WithEvents rbNoQuota As RadioButton
    Protected WithEvents rbActivity As RadioButton
    Protected WithEvents rbBlock As RadioButton
    Protected WithEvents rbIndividual As RadioButton
    Protected WithEvents txtIndDailyQuota As TextBox
    Protected WithEvents rbByHour As RadioButton
    Protected WithEvents rbByVolume As RadioButton
    Protected WithEvents txtIndQuotaIncRate As TextBox
    Protected WithEvents lblEmpStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents ddlSalary As DropDownList
    Protected WithEvents ddlDaily As DropDownList
    Protected WithEvents ddlPiece As DropDownList
    Protected WithEvents ddlBonus As DropDownList
    Protected WithEvents ddlTHR As DropDownList
    Protected WithEvents ddlHouse As DropDownList
    Protected WithEvents ddlHardShip As DropDownList
    Protected WithEvents ddlIncAward As DropDownList
    Protected WithEvents ddlTransport As DropDownList        
    Protected WithEvents ddlOvertime As DropDownList
    Protected WithEvents ddlTrip As DropDownList
    Protected WithEvents ddlRiceRation As DropDownList    
    Protected WithEvents ddlQuotaIncCode As DropDownList
    Protected WithEvents ddlIncentive As DropDownList
    Protected WithEvents ddlContractPay As DropDownList
    Protected WithEvents ddlBIKAccom As DropDownList
    Protected WithEvents ddlBIKVeh As DropDownList
    Protected WithEvents ddlBIKHP As DropDownList
    Protected WithEvents ddlGratuity As DropDownList
    Protected WithEvents ddlRetrench As DropDownList
    Protected WithEvents ddlESOS As DropDownList
    Protected WithEvents ddlAttAllow As DropDownList

    Protected WithEvents ddlHouseRent As DropDownList        
    Protected WithEvents ddlMedical As DropDownList        
    Protected WithEvents ddlTax As DropDownList        
    Protected WithEvents ddlDanaPensiun As DropDownList        
    Protected WithEvents ddlRapel As DropDownList
    Protected WithEvents ddlStaff As DropDownList
    Protected WithEvents ddlFunctional As DropDownList
    Protected WithEvents ddlHutang As DropDownList

    Protected WithEvents ddlMeal As DropDownList
    Protected WithEvents ddlLeave As DropDownList
    Protected WithEvents ddlAirBus As DropDownList
    Protected WithEvents ddlMaternity As DropDownList
    Protected WithEvents ddlRelocation As DropDownList

    Protected WithEvents ddlAdvSalary As DropDownList    
    Protected WithEvents ddlIN As DropDownList
    Protected WithEvents ddlCT As DropDownList
    Protected WithEvents ddlWS As DropDownList
    Protected WithEvents ddlWSRefund As DropDownList
    Protected WithEvents ddlLoan As DropDownList
    Protected WithEvents ddlBFEmp As DropDownList
    Protected WithEvents ddlOutPayEmp As DropDownList
    Protected WithEvents ddlAbsent As DropDownList
    Protected WithEvents ddlRiceDeduction As DropDownList


    Protected WithEvents ddlSPSICOP As DropDownList
    Protected WithEvents ddlLuranCOP As DropDownList
    Protected WithEvents ddlOther As DropDownList

    Protected WithEvents ddlHold As DropDownList
    Protected WithEvents ddlPayment As DropDownList
    Protected WithEvents ddlSubsidy As DropDownList    
    Protected WithEvents ddlDeficit As DropDownList
    Protected WithEvents ddlLevyAdj As DropDownList
    Protected WithEvents rbAccumulate As RadioButton
    Protected WithEvents rbPush As RadioButton

    Protected WithEvents lblErrSalary As Label
    Protected WithEvents lblErrDaily As Label
    Protected WithEvents lblErrPiece As Label
    Protected WithEvents lblErrBonus As Label
    Protected WithEvents lblErrTHR As Label
    Protected WithEvents lblErrHouse As Label
    Protected WithEvents lblErrHardShip As Label
    Protected WithEvents lblErrIncAward As Label
    Protected WithEvents lblErrTransport As Label        
    Protected WithEvents lblErrOvertime As Label
    Protected WithEvents lblErrTrip As Label
    Protected WithEvents lblErrRice As Label    
    Protected WithEvents lblErrQuotaIncCode As Label
    Protected WithEvents lblErrIncentive As Label
    Protected WithEvents lblErrContractPay As Label
    Protected WithEvents lblErrBIKAccom As Label
    Protected WithEvents lblErrBIKVeh As Label
    Protected WithEvents lblErrBIKHP As Label
    Protected WithEvents lblErrGratuity As Label
    Protected WithEvents lblErrRetrench As Label
    Protected WithEvents lblErrESOS As Label
    Protected WithEvents lblErrAttAllow As Label

    Protected WithEvents lblErrHouseRent As Label
    Protected WithEvents lblErrMedical As Label
    Protected WithEvents lblErrTax As Label
    Protected WithEvents lblErrDanaPensiun As Label
    Protected WithEvents lblErrRapel As Label
    Protected WithEvents lblErrStaff As Label
    Protected WithEvents lblErrFunctional As Label
    Protected WithEvents lblErrHutang As Label

    Protected WithEvents lblErrMeal As Label
    Protected WithEvents lblErrLeave As Label
    Protected WithEvents lblErrAirBus As Label
    Protected WithEvents lblErrMaternity As Label
    Protected WithEvents lblErrRelocation As Label

    Protected WithEvents lblErrAdvSalary As Label    
    Protected WithEvents lblErrIN As Label
    Protected WithEvents lblErrCT As Label
    Protected WithEvents lblErrWS As Label
    Protected WithEvents lblErrWSRefund As Label
    Protected WithEvents lblErrLoan As Label
    Protected WithEvents lblErrBF As Label
    Protected WithEvents lblErrOutPay As Label
    Protected WithEvents lblErrAbsent As Label
    Protected WithEvents lblErrRiceDeduction As Label


    Protected WithEvents lblErrSPSICOP As Label
    Protected WithEvents lblErrLuranCOP As Label
    Protected WithEvents lblErrOther As Label

    Protected WithEvents lblErrHold As Label
    Protected WithEvents lblErrPayment As Label
    Protected WithEvents lblErrSubsidy As Label    
    Protected WithEvents lblErrDeficit As Label
    Protected WithEvents lblErrLevyAdj As Label

    Protected WithEvents lblRiceRation As Label
    Protected WithEvents lblIncentive As Label
    Protected WithEvents lblQuotaIncCode As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblBenefit As Label
    Protected WithEvents lblErrSelect As Label

    Protected WithEvents EmpCode As HtmlInputHidden
    Protected WithEvents EmpName As HtmlInputHidden
    Protected WithEvents btnFind As HtmlInputButton
    Protected WithEvents ProbationPeriod As HtmlInputHidden
    Protected WithEvents IncrementAmt As HtmlInputHidden

    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblHidQuotaLevel As Label
    Protected WithEvents lblHidIndQuota As Label
    Protected WithEvents lblHidIndQuotaMethod As Label
    Protected WithEvents lblHidIndQuotaIncRate As Label

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrDateFrom As Label
    Protected WithEvents lblErrDateTo As Label
    Protected WithEvents lblErrReqDateTo As Label
    Protected WithEvents lblErrCeaseDate As Label
    Protected WithEvents lblErrReqCeaseDate As Label
    Protected WithEvents lblErrIncrementAmt As Label
    Protected WithEvents lblErrPayType As Label

    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCP As Label
    Protected WithEvents lblCompany As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblLevel As Label
    Protected WithEvents lblQuotaInc As Label

    Protected WithEvents ddlEvalCode As DropDownList
    Dim objEvalDs As New Object()

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents btnSelDateTo As Image
    Protected WithEvents btnSelCeaseDate As Image

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbDetails As LinkButton
    Protected WithEvents lbPayroll As LinkButton
    Protected WithEvents lbEmployment As LinkButton
    Protected WithEvents lbStatutory As LinkButton
    Protected WithEvents lbFamily As LinkButton
    Protected WithEvents lbQualific As LinkButton
    Protected WithEvents lbSkill As LinkButton

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objEmpCPDs As New Object()
    Dim objLastCPDs As New Object()
    Dim objCPDs As New Object()
    Dim objCompDs As New Object()
    Dim objLocDs As New Object()
    Dim objDeptDs As New Object()
    Dim objLevelDs As New Object()
    Dim objRptDs As New Object()
    Dim objSalSchemeDs As New Object()
    Dim objSalGradeDs As New Object()
    Dim objPosDs As New Object()
    Dim objRiceDs As New Object()
    Dim objIncDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objADDs As New Object()
    Dim objADPaySetupDs As New Object()
    
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long
    Dim strCostLevel As String

    Dim strSelEmpCode As String = ""
    Dim strSelEmpName As String = ""
    Dim strSelEmpStatus As String = ""
    Dim strSelCPID As String = ""
    Dim strSortExpression As String = "CPID"
    Dim strDateFmt As String
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strCostLevel = Session("SS_COSTLEVEL")
        strDateFmt = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCareerProgress), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else   
            lblErrDateFrom.visible = False
            lblErrDateTo.visible = False
            lblErrReqDateTo.visible = False
            lblErrCeaseDate.visible = False
            lblErrReqCeaseDate.visible = False
            lblErrPayType.visible = False
            lblErrIncrementAmt.visible = False

            strSelEmpCode = Trim(Request.QueryString("EmpCode"))
            strSelEmpName = Trim(Request.QueryString("EmpName"))
            strSelEmpStatus = Trim(Request.QueryString("EmpStatus"))
            strSelCPID = Trim(Request.QueryString("CPID"))
            lblRedirect.Text = Request.QueryString("redirect")

            onload_GetLangCap()
            lblErrSalary.Visible = False
            lblErrDaily.Visible = False
            lblErrPiece.Visible = False
            lblErrBonus.Visible = False
            lblErrAdvSalary.Visible = False
            lblErrTHR.Visible = False            
            lblErrIN.Visible = False
            lblErrHouse.Visible = False
            lblErrCT.Visible = False
            lblErrHardShip.Visible = False
            lblErrWS.Visible = False
            lblErrIncAward.Visible = False
            lblErrTransport.Visible = False
            lblErrLoan.Visible = False
            lblErrOvertime.Visible = False
            lblErrBF.Visible = False
            lblErrTrip.Visible = False
            lblErrOutPay.Visible = False
            lblErrRice.Visible = False
            lblErrAbsent.Visible = False
            lblErrRiceDeduction.Visible = False
            lblErrSPSICOP.Visible = False
            lblErrLuranCOP.Visible = False
            lblErrOther.Visible = False
            lblErrIncentive.Visible = False
            lblErrQuotaIncCode.Visible = False
            lblErrContractPay.Visible = False
            lblErrHold.Visible = False
            lblErrBIKAccom.Visible = False
            lblErrPayment.Visible = False
            lblErrBIKVeh.Visible = False
            lblErrSubsidy.Visible = False
            lblErrBIKHP.Visible = False
            lblErrDeficit.Visible = False
            lblErrGratuity.Visible = False
            lblErrLevyAdj.Visible = False
            lblErrRetrench.Visible = False
            lblErrWSRefund.Visible = False
            lblErrESOS.Visible = False
            lblErrAttAllow.Visible = False

            lblErrHouseRent.Visible = False
            lblErrMedical.Visible = False
            lblErrTax.Visible = False
            lblErrDanaPensiun.Visible = False
            lblErrRapel.Visible = False
            lblErrStaff.Visible = False
            lblErrFunctional.Visible = False
            lblErrHutang.Visible = False

            lblErrTransport.Visible = False
            lblErrMeal.Visible = False
            lblErrLeave.Visible = False
            lblErrAirBus.Visible = False
            lblErrMaternity.Visible = False
            lblErrRelocation.Visible = False 
     
            If Not IsPostBack Then
                If strSelCPID <> "" Then
                    onLoad_Display()
                    onLoad_BindButton()
                Else
                    onLoad_DispLastCP()
                    BindCPCode("")
                    onLoad_BindButton()
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

    Private Sub lbPayroll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbPayroll.Click
        Response.Redirect("HR_trx_EmployeePay.aspx?redirect=emppay&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
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

    Sub onLoad_BindButton()
        ddlCPCode.Enabled = False
        txtDateFrom.Enabled = False
        txtDateTo.Enabled = False
        txtCeaseDate.Enabled = False
        txtRemark.Enabled = False
        ddlCompCode.Enabled = False
        ddlLocCode.Enabled = False
        ddlDeptCode.Enabled = False
        ddlLevelCode.Enabled = False
        ddlRptTo.Enabled = False
        btnFind.visible = False
        ddlSalSchemeCode.Enabled = False
        ddlEvalCode.Enabled = False
        ddlSalGradeCode.Enabled = False
        ddlPosCode.Enabled = False
        txtProbation.Enabled = False
        ddlPayType.Enabled = False
        txtIncrementAmt.Enabled = False
        rbNoQuota.Enabled = False
        rbActivity.Enabled = False
        rbBlock.Enabled = False
        rbIndividual.Enabled = False
        txtIndDailyQuota.Enabled = False
        rbByHour.Enabled = False
        rbByVolume.Enabled = False
        txtIndQuotaIncRate.Enabled = False
        btnSave.Visible = False

        ddlSalary.Enabled = False
        ddlDaily.Enabled = False
        ddlPiece.Enabled = False
        ddlBonus.Enabled = False
        ddlTHR.Enabled = False
        ddlHouse.Enabled = False
        ddlHardShip.Enabled = False
        ddlIncAward.Enabled = False
        ddlTransport.Enabled = False        
        ddlOvertime.Enabled = False
        ddlTrip.Enabled = False
        ddlRiceRation.Enabled = False    
        ddlQuotaIncCode.Enabled = False
        ddlIncentive.Enabled = False
        ddlContractPay.Enabled = False
        ddlBIKAccom.Enabled = False
        ddlBIKVeh.Enabled = False
        ddlBIKHP.Enabled = False
        ddlGratuity.Enabled = False
        ddlRetrench.Enabled = False
        ddlESOS.Enabled = False
        ddlAttAllow.Enabled = False

        ddlHouseRent.Enabled = False        
        ddlMedical.Enabled = False  
        ddlTax.Enabled = False       
        ddlDanaPensiun.Enabled = False       
        ddlRapel.Enabled = False 
        ddlStaff.Enabled = False       
        ddlFunctional.Enabled = False       
        ddlHutang.Enabled = False      
        ddlMeal.Enabled = False 
        ddlLeave.Enabled = False 
        ddlAirBus.Enabled = False 
        ddlMaternity.Enabled = False 
        ddlRelocation.Enabled = False 

        ddlAdvSalary.Enabled = False    
        ddlIN.Enabled = False
        ddlCT.Enabled = False
        ddlWS.Enabled = False
        ddlWSRefund.Enabled = False
        ddlLoan.Enabled = False
        ddlBFEmp.Enabled = False
        ddlOutPayEmp.Enabled = False
        ddlAbsent.Enabled = False
        ddlRiceDeduction.Enabled = False

        ddlSPSICOP.Enabled = False
        ddlLuranCOP.Enabled = False
        ddlOther.Enabled = False

        ddlHold.Enabled = False
        ddlPayment.Enabled = False
        ddlSubsidy.Enabled = False    
        ddlDeficit.Enabled = False
        ddlLevyAdj.Enabled = False
        rbAccumulate.Enabled = False
        rbPush.Enabled = False

        If CInt(lblEmpStatus.Text) = objHRTrx.EnumEmpStatus.Active And strSelCPID = "" Then
            ddlCPCode.Enabled = True
            txtDateFrom.Enabled = True
            txtDateTo.Enabled = True
            txtCeaseDate.Enabled = True
            txtRemark.Enabled = True
            ddlDeptCode.Enabled = True
            ddlLevelCode.Enabled = True
            ddlRptTo.Enabled = True
            btnFind.visible = True
            ddlSalSchemeCode.Enabled = True
            ddlEvalCode.Enabled = True
            ddlSalGradeCode.Enabled = True
            ddlPosCode.Enabled = True
            txtProbation.Enabled = True
            ddlPayType.Enabled = True
            txtIncrementAmt.Enabled = True
            btnSave.Visible = True
            CheckPayType(ddlPayType.SelectedItem.Value)
            CheckQuotaLevel(CInt(lblHidQuotaLevel.Text))
            ddlSalary.Enabled = True
            ddlDaily.Enabled = True
            ddlPiece.Enabled = True
            ddlBonus.Enabled = True
            ddlTHR.Enabled = True
            ddlHouse.Enabled = True
            ddlHardShip.Enabled = True
            ddlIncAward.Enabled = True
            ddlTransport.Enabled = True        
            ddlOvertime.Enabled = True
            ddlTrip.Enabled = True
            ddlRiceRation.Enabled = True    
            ddlQuotaIncCode.Enabled = True
            ddlIncentive.Enabled = True
            ddlContractPay.Enabled = True
            ddlBIKAccom.Enabled = True
            ddlBIKVeh.Enabled = True
            ddlBIKHP.Enabled = True
            ddlGratuity.Enabled = True
            ddlRetrench.Enabled = True
            ddlESOS.Enabled = True
            ddlAttAllow.Enabled = True

            ddlHouseRent.Enabled = True        
            ddlMedical.Enabled = True  
            ddlTax.Enabled = True  
            ddlDanaPensiun.Enabled = True  
            ddlRapel.Enabled = True  
            ddlStaff.Enabled = True  
            ddlFunctional.Enabled = True  
            ddlHutang.Enabled = True 
            ddlMeal.Enabled = True
            ddlLeave.Enabled = True
            ddlAirBus.Enabled = True
            ddlMaternity.Enabled = True      
            ddlRelocation.Enabled = True            

            ddlAdvSalary.Enabled = True    
            ddlIN.Enabled = True
            ddlCT.Enabled = True
            ddlWS.Enabled = True
            ddlWSRefund.Enabled = True
            ddlLoan.Enabled = True
            ddlBFEmp.Enabled = True
            ddlOutPayEmp.Enabled = True
            ddlAbsent.Enabled = True
            ddlRiceDeduction.Enabled = True
            ddlSPSICOP.Enabled = True
            ddlLuranCOP.Enabled = True
            ddlOther.Enabled = True

            ddlHold.Enabled = True
            ddlPayment.Enabled = True
            ddlSubsidy.Enabled = True    
            ddlDeficit.Enabled = True
            ddlLevyAdj.Enabled = True
            rbAccumulate.Enabled = True
            rbPush.Enabled = True
        End If
    End Sub

    Sub ItemIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        BindCPCode(ddlCPCode.SelectedItem.Value)
    End Sub

    Sub CheckPayType(ByVal pv_strPayType As String)
        rbNoQuota.Enabled = False
        rbActivity.Enabled = False
        rbBlock.Enabled = False
        rbIndividual.Enabled = False

        If Trim(pv_strPayType) = "" Then
        Else
            If Trim(pv_strPayType) = objPRSetup.EnumPayType.DailyRate Then
                rbNoQuota.Enabled = True
                rbActivity.Enabled = True
                rbBlock.Enabled = True
                rbIndividual.Enabled = True
                CheckQuotaLevel(CInt(lblHidQuotaLevel.Text))
            Else
                CheckQuotaLevel(objHRTrx.EnumQuotaLevel.NoQuota)
            End If
        End If
    End Sub

    Sub onChg_PayType(ByVal Sender As System.Object, ByVal E As System.EventArgs)
        CheckPayType(ddlPayType.SelectedItem.Value)
    End Sub

    Sub CheckQuotaLevel(ByVal pv_intQuotaLevel As Integer)
        txtIndDailyQuota.Enabled = False
        rbByHour.Enabled = False
        rbByVolume.Enabled = False
        txtIndQuotaIncRate.Enabled = False

        rbNoQuota.Checked = False
        rbActivity.Checked = False
        rbBlock.Checked = False
        rbIndividual.Checked = False
        txtIndDailyQuota.Text = "0"
        rbByHour.Checked = True
        rbByVolume.Checked = False
        txtIndQuotaIncRate.Text = "0"

        If pv_intQuotaLevel = objHRTrx.EnumQuotaLevel.NoQuota Then
            rbNoQuota.Checked = True
        ElseIf pv_intQuotaLevel = objHRTrx.EnumQuotaLevel.Activity Then
            rbActivity.Checked = True
        ElseIf pv_intQuotaLevel = objHRTrx.EnumQuotaLevel.Block Then
            rbBlock.Checked = True
        Else
            rbIndividual.Checked = True
            txtIndDailyQuota.Enabled = True
            txtIndDailyQuota.Text = lblHidIndQuota.Text
            rbByHour.Enabled = True
            rbByVolume.Enabled = True
            txtIndQuotaIncRate.Enabled = True
            txtIndQuotaIncRate.Text = lblHidIndQuotaIncRate.Text
            If lblHidIndQuotaMethod.Text = objHRTrx.EnumQuotaMethod.ByHour Then
                rbByHour.Checked = True
            Else
                rbByVolume.Checked = True
            End If
        End If
    End Sub

    Sub onChg_QuotaLevel(ByVal Sender AS System.Object, ByVal E As System.EventArgs)
        Dim intQuotaLevel As Integer
        If rbActivity.Checked Then
            intQuotaLevel = objHRTrx.EnumQuotaLevel.Activity
        ElseIf rbBlock.Checked Then
            intQuotaLevel = objHRTrx.EnumQuotaLevel.Block
        ElseIf rbIndividual.Checked Then
            intQuotaLevel = objHRTrx.EnumQuotaLevel.Individual
        Else
            intQuotaLevel = objHRTrx.EnumQuotaLevel.NoQuota
        End If
        CheckQuotaLevel(intQuotaLevel)
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


    Sub BindCPCode(ByVal pv_strCPCode As String)
        Dim strOpCd_Get As String = "HR_CLSSETUP_CP_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objHRSetup.EnumCareerProgressStatus.Active & "||CP.CPCode|"

        Try
            intErrNo = objHRSetup.mtdGetCareerProgress(strOpCd_Get, strParam, objCPDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPDET_GET_CPCODE&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objCPDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objCPDs.Tables(0).Rows.Count - 1
                If Trim(objCPDs.Tables(0).Rows(intCnt).Item("CPCode")) = Trim(pv_strCPCode) Then
                    lblCPType.Text = Trim(objCPDs.Tables(0).Rows(intCnt).Item("CPType"))
                    intSelectedIndex = intCnt + 1
                    lblPeriodInd.Text = objCPDs.Tables(0).Rows(intCnt).Item("PeriodInd")

                    If lblCPType.Text <> "" Then
                        If lblCPType.Text = objHRSetup.EnumCPType.Cease Then
                            txtCeaseDate.Enabled = True
                            btnSelCeaseDate.visible = True
                        Else
                            txtCeaseDate.Enabled = False
                            btnSelCeaseDate.visible = False
                        End If
                    Else
                        txtCeaseDate.Enabled = False
                    End If

                    If lblPeriodInd.Text <> "" Then
                        If lblPeriodInd.Text = objHRSetup.EnumCPPeriod.Active Then
                            txtDateTo.Enabled = True
                            btnSelDateTo.visible = True
                        Else
                            txtDateTo.Enabled = False
                            btnSelDateTo.visible = False
                        End If
                    Else
                        txtDateTo.Enabled = False
                        btnSelDateTo.visible = False
                    End If
                    Exit For
                End If
            Next
        End If

        dr = objCPDs.Tables(0).NewRow()
        dr("CPCode") = ""
        dr("_Description") = "Select " & lblCP.Text & " Code"
        objCPDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlCPCode.DataSource = objCPDs.Tables(0)
        ddlCPCode.DataTextField = "_Description"
        ddlCPCode.DataValueField = "CPCode"
        ddlCPCode.DataBind()
        ddlCPCode.SelectedIndex = intSelectedIndex
    End Sub


    Sub BindComp(ByVal pv_strCompCode As String)
        Dim strOpCd_Get As String = "ADMIN_CLSCOMP_COMPANY_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objAdminComp.EnumCompanyStatus.Active & "||CompCode|"

        Try
            intErrNo = objAdminComp.mtdGetComp(strOpCd_Get, strParam, objCompDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPDET_GET_COMPANY&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objCompDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objCompDs.Tables(0).Rows.Count - 1
                If objCompDs.Tables(0).Rows(intCnt).Item("CompCode") = Trim(pv_strCompCode) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objCompDs.Tables(0).NewRow()
        dr("CompCode") = ""
        dr("_Description") = "Select " & lblCompany.Text
        objCompDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlCompCode.DataSource = objCompDs.Tables(0)
        ddlCompCode.DataTextField = "_Description"
        ddlCompCode.DataValueField = "CompCode"
        ddlCompCode.DataBind()
        ddlCompCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindLoc(ByVal pv_strLocCode As String)
        Dim strOpCd_Get As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objAdminLoc.EnumLocStatus.Active & "||LocCode||"

        Try
            intErrNo = objAdminLoc.mtdGetLocCode(strOpCd_Get, strParam, objLocDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPDET_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objLocDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
                If Trim(objLocDs.Tables(0).Rows(intCnt).Item("LocCode")) = Trim(pv_strLocCode) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("_Description") = "Select " & lblLocation.Text
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocCode.DataSource = objLocDs.Tables(0)
        ddlLocCode.DataTextField = "_Description"
        ddlLocCode.DataValueField = "LocCode"
        ddlLocCode.DataBind()
        ddlLocCode.SelectedIndex = intSelectedIndex
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
        dr("_Description") = "Select " & lblDepartment.text
        objDeptDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDeptCode.DataSource = objDeptDs.Tables(0)
        ddlDeptCode.DataTextField = "_Description"
        ddlDeptCode.DataValueField = "DeptCode"
        ddlDeptCode.DataBind()
        ddlDeptCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindLevel(ByVal pv_strLevelCode As String)
        Dim strOpCd_Get As String = "HR_CLSSETUP_LEVEL_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "Order By LVL.LevelCode | AND LVL.Status = '" & objHRSetup.EnumLevelStatus.Active & "'"

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Get, strParam, objHRSetup.EnumHRMasterType.Level, objLevelDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPDET_GET_LEVEL&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objLevelDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objLevelDs.Tables(0).Rows.Count - 1
                If Trim(objLevelDs.Tables(0).Rows(intCnt).Item("LevelCode")) = Trim(pv_strLevelCode) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objLevelDs.Tables(0).NewRow()
        dr("LevelCode") = ""
        dr("_Description") = "Select " & lblLevel.Text
        objLevelDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLevelCode.DataSource = objLevelDs.Tables(0)
        ddlLevelCode.DataTextField = "_Description"
        ddlLevelCode.DataValueField = "LevelCode"
        ddlLevelCode.DataBind()
        ddlLevelCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindRptTo(ByVal pv_strRptTo As String)
        Dim strOpCd_Get As String = "HR_CLSTRX_EMPLOYEE_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = lblEmpCode.Text & "|" & objHRTrx.EnumEmpStatus.Active

        Try
            intErrNo = objHRTrx.mtdGetOthEmployee(strOpCd_Get, strParam, strLocation, objRptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPDET_GET_RPTTO&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
                If objRptDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(pv_strRptTo) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objRptDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "Select Report To"
        objRptDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlRptTo.DataSource = objRptDs.Tables(0)
        ddlRptTo.DataTextField = "_Description"
        ddlRptTo.DataValueField = "EmpCode"
        ddlRptTo.DataBind()
        ddlRptTo.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindSalScheme(ByVal pv_strSalScheme As String)
        Dim strOpCd_Get As String = "HR_CLSSETUP_SALSCHEME_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "Order By SAL.SalSchemeCode | AND SAL.Status = '" & objHRSetup.EnumSalSchemeStatus.Active & "'"

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Get, strParam, objHRSetup.EnumHRMasterType.SalScheme, objSalSchemeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPDET_GET_SALSCHEME&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objSalSchemeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objSalSchemeDs.Tables(0).Rows.Count - 1
                If Trim(objSalSchemeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode")) = Trim(pv_strSalScheme) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objSalSchemeDs.Tables(0).NewRow()
        dr("SalSchemeCode") = ""
        dr("_Description") = "Please Select Employee Category"
        objSalSchemeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSalSchemeCode.DataSource = objSalSchemeDs.Tables(0)
        ddlSalSchemeCode.DataTextField = "_Description"
        ddlSalSchemeCode.DataValueField = "SalSchemeCode"
        ddlSalSchemeCode.DataBind()
        ddlSalSchemeCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindSalGrade(ByVal pv_strSalGrade As String)
        Dim strOpCd_Get As String = "HR_CLSSETUP_SALGRADE_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objHRSetup.EnumSalGradeStatus.Active & "||SG.SalGradeCode||"

        Try
            intErrNo = objHRSetup.mtdGetSalGrade(strOpCd_Get, strParam, objSalGradeDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPDET_GET_SALGRADE&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objSalGradeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objSalGradeDs.Tables(0).Rows.Count - 1
                If Trim(objSalGradeDs.Tables(0).Rows(intCnt).Item("SalGradeCode")) = Trim(pv_strSalGrade) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objSalGradeDs.Tables(0).NewRow()
        dr("SalGradeCode") = ""
        dr("_Description") = "Select Salary Grade"
        objSalGradeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSalGradeCode.DataSource = objSalGradeDs.Tables(0)
        ddlSalGradeCode.DataTextField = "_Description"
        ddlSalGradeCode.DataValueField = "SalGradeCode"
        ddlSalGradeCode.DataBind()
        ddlSalGradeCode.SelectedIndex = intSelectedIndex
    End Sub


    Sub BindPosition(ByVal pv_strPosCode As String)
        Dim strOpCd_Get As String = "HR_CLSSETUP_POSITION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = "Order By POS.PositionCode | AND POS.Status = '" & objHRSetup.EnumPositionStatus.Active & "'"

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Get, strParam, objHRSetup.EnumHRMasterType.Position, objPosDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPDET_GET_POSITION&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objPosDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objPosDs.Tables(0).Rows.Count - 1
                If Trim(objPosDs.Tables(0).Rows(intCnt).Item("PositionCode")) = Trim(pv_strPosCode) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objPosDs.Tables(0).NewRow()
        dr("PositionCode") = ""
        dr("_Description") = "Select Position"
        objPosDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPosCode.DataSource = objPosDs.Tables(0)
        ddlPosCode.DataTextField = "_Description"
        ddlPosCode.DataValueField = "PositionCode"
        ddlPosCode.DataBind()
        ddlPosCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSTRX_CAREERPROGRESS_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim objAllowanceDs As New Object()
        Dim objDeductionDs As New Object()
        Dim objMemoItemDs As New Object()
        Dim strType_Allowance As String 
        Dim strType_Deduction As String
        Dim strType_EAItem As String
        Dim strType_MemoItem As String
        Dim intIndex As Integer

        strType_Allowance = objPRSetup.EnumADType.Allowance
        strType_Deduction = objPRSetup.EnumADType.Deduction
        strType_EAItem = objPRSetup.EnumADType.EAItem
        strType_MemoItem = objPRSetup.EnumADType.MemoItem

        lblEmpCode.Text = strSelEmpCode
        lblEmpName.Text = strSelEmpName
        lblEmpStatus.Text = strSelEmpStatus
        lblCPID.Text = strSelCPID
        
        strParam = strSelCPID & "|" & strSelEmpCode & "|||" & strSortExpression & "|"

        Try
            intErrNo = objHRTrx.mtdGetEmployeeCP(strOpCd, strParam, objEmpCPDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeCPList.aspx")
        End Try

        If objEmpCPDs.Tables(0).Rows.Count > 0 Then
            txtDateFrom.Text = objGlobal.GetShortDate(strDateFmt, objEmpCPDs.Tables(0).Rows(0).Item("DateFrom"))
            txtDateTo.Text = objGlobal.GetShortDate(strDateFmt, objEmpCPDs.Tables(0).Rows(0).Item("DateTo"))
            txtCeaseDate.Text = objGlobal.GetShortDate(strDateFmt, objEmpCPDs.Tables(0).Rows(0).Item("CeaseDate"))
            txtRemark.Text = objEmpCPDs.Tables(0).Rows(0).Item("Remark")

            txtIndDailyQuota.Text = ObjGlobal.GetIDDecimalSeparator(objEmpCPDs.Tables(0).Rows(0).Item("IndQuota"))
            txtIndQuotaIncRate.Text = ObjGlobal.GetIDDecimalSeparator(objEmpCPDs.Tables(0).Rows(0).Item("IndQuotaIncRate"))

            BindPayType(objEmpCPDs.Tables(0).Rows(0).Item("PayType"))
            BindCPCode(objEmpCPDs.Tables(0).Rows(0).Item("CPCode"))
            BindComp(objEmpCPDs.Tables(0).Rows(0).Item("CompCode"))
            BindLoc(objEmpCPDs.Tables(0).Rows(0).Item("LocCode"))
            BindDept(objEmpCPDs.Tables(0).Rows(0).Item("DeptCode"))
            BindLevel(objEmpCPDs.Tables(0).Rows(0).Item("LevelCode"))
            BindRptTo(objEmpCPDs.Tables(0).Rows(0).Item("RptTo"))
            BindSalScheme(objEmpCPDs.Tables(0).Rows(0).Item("SalSchemeCode"))
            BindEvaluation(objEmpCPDs.Tables(0).Rows(0).Item("EvalCode"))
            BindSalGrade(objEmpCPDs.Tables(0).Rows(0).Item("SalGradeCode"))
            BindPosition(objEmpCPDs.Tables(0).Rows(0).Item("PosCode")) 
            
            lblCurrentRate.Text = ObjGlobal.GetIDDecimalSeparator(objEmpCPDs.Tables(0).Rows(0).Item("CurrentRate"))
            lblDateCreated.Text = objGlobal.GetLongDate(objEmpCPDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objEmpCPDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdateBy.Text = Trim(objEmpCPDs.Tables(0).Rows(0).Item("UserName"))
            txtProbation.Text = objEmpCPDs.Tables(0).Rows(0).Item("Probation")
            txtIncrementAmt.Text = ObjGlobal.GetIDDecimalSeparator(objEmpCPDs.Tables(0).Rows(0).Item("IncrementAmt"))

            If CInt(objEmpCPDs.Tables(0).Rows(0).Item("LevyDeductInd")) = objPRSetup.EnumPayrollSetupLevyDeduction.AccumulateMethod Then
                rbAccumulate.Checked = True
                rbPush.Checked = False
            Else
                rbAccumulate.Checked = False
                rbPush.Checked = True
            End If
            ddlSalary.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("SalaryADCode").Trim(), strType_Allowance, intIndex, "")
            ddlSalary.DataValueField = "ADCode"
            ddlSalary.DataTextField = "_Description"
            ddlSalary.DataBind()
            ddlSalary.SelectedIndex = intIndex

            ddlDaily.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("DailyRateADCode").Trim(), strType_Allowance, intIndex, "")
            ddlDaily.DataValueField = "ADCode"
            ddlDaily.DataTextField = "_Description"
            ddlDaily.DataBind()
            ddlDaily.SelectedIndex = intIndex

            ddlPiece.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("PieceRateADCode").Trim(), strType_Allowance, intIndex, "")
            ddlPiece.DataValueField = "ADCode"
            ddlPiece.DataTextField = "_Description"
            ddlPiece.DataBind()
            ddlPiece.SelectedIndex = intIndex

            ddlBonus.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("BonusADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBonus.DataValueField = "ADCode"
            ddlBonus.DataTextField = "_Description"
            ddlBonus.DataBind()
            ddlBonus.SelectedIndex = intIndex

            ddlTHR.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("THRADCode").Trim(), strType_Allowance, intIndex, "THR")
            ddlTHR.DataValueField = "ADCode"
            ddlTHR.DataTextField = "_Description"
            ddlTHR.DataBind()
            ddlTHR.SelectedIndex = intIndex

            ddlHouse.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("HouseADCode").Trim(), strType_Allowance, intIndex, "")
            ddlHouse.DataValueField = "ADCode"
            ddlHouse.DataTextField = "_Description"
            ddlHouse.DataBind()
            ddlHouse.SelectedIndex = intIndex

            ddlHardShip.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("HardshipADCode").Trim(), strType_Allowance, intIndex, "")
            ddlHardShip.DataValueField = "ADCode"
            ddlHardShip.DataTextField = "_Description"
            ddlHardShip.DataBind()
            ddlHardShip.SelectedIndex = intIndex

            ddlIncAward.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("IncAwardADCode").Trim(), strType_Allowance, intIndex, "")
            ddlIncAward.DataValueField = "ADCode"
            ddlIncAward.DataTextField = "_Description"
            ddlIncAward.DataBind()
            ddlIncAward.SelectedIndex = intIndex

            ddlTransport.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("TransADCode").Trim(), strType_Allowance, intIndex, "Tran")
            ddlTransport.DataValueField = "ADCode"
            ddlTransport.DataTextField = "_Description"
            ddlTransport.DataBind()
            ddlTransport.SelectedIndex = intIndex

            ddlOvertime.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("OTADCode").Trim(), strType_Allowance, intIndex, "")
            ddlOvertime.DataValueField = "ADCode"
            ddlOvertime.DataTextField = "_Description"
            ddlOvertime.DataBind()
            ddlOvertime.SelectedIndex = intIndex

            ddlTrip.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("TripADCode").Trim(), strType_Allowance, intIndex, "")
            ddlTrip.DataValueField = "ADCode"
            ddlTrip.DataTextField = "_Description"
            ddlTrip.DataBind()
            ddlTrip.SelectedIndex = intIndex

            ddlRiceRation.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("RiceADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRiceRation.DataValueField = "ADCode"
            ddlRiceRation.DataTextField = "_Description"
            ddlRiceRation.DataBind()
            ddlRiceRation.SelectedIndex = intIndex

            ddlQuotaIncCode.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("QuotaIncADCode").Trim(), strType_Allowance, intIndex, "")
            ddlQuotaIncCode.DataValueField = "ADCode"
            ddlQuotaIncCode.DataTextField = "_Description"
            ddlQuotaIncCode.DataBind()
            ddlQuotaIncCode.SelectedIndex = intIndex

            ddlIncentive.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("IncADCode").Trim(), strType_Allowance, intIndex, "")
            ddlIncentive.DataValueField = "ADCode"
            ddlIncentive.DataTextField = "_Description"
            ddlIncentive.DataBind()
            ddlIncentive.SelectedIndex = intIndex

            ddlContractPay.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("ContractPayADCode").Trim(), strType_Allowance, intIndex, "")
            ddlContractPay.DataValueField = "ADCode"
            ddlContractPay.DataTextField = "_Description"
            ddlContractPay.DataBind()
            ddlContractPay.SelectedIndex = intIndex

            ddlBIKAccom.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("BIKAccomADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKAccom.DataValueField = "ADCode"
            ddlBIKAccom.DataTextField = "_Description"
            ddlBIKAccom.DataBind()
            ddlBIKAccom.SelectedIndex = intIndex

            ddlBIKVeh.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("BIKVehADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKVeh.DataValueField = "ADCode"
            ddlBIKVeh.DataTextField = "_Description"
            ddlBIKVeh.DataBind()
            ddlBIKVeh.SelectedIndex = intIndex

            ddlBIKHP.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("BIKHPADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKHP.DataValueField = "ADCode"
            ddlBIKHP.DataTextField = "_Description"
            ddlBIKHP.DataBind()
            ddlBIKHP.SelectedIndex = intIndex

            ddlGratuity.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("GratuityADCode").Trim(), strType_Allowance, intIndex, "")
            ddlGratuity.DataValueField = "ADCode"
            ddlGratuity.DataTextField = "_Description"
            ddlGratuity.DataBind()
            ddlGratuity.SelectedIndex = intIndex

            ddlRetrench.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("RetrenchCompADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRetrench.DataValueField = "ADCode"
            ddlRetrench.DataTextField = "_Description"
            ddlRetrench.DataBind()
            ddlRetrench.SelectedIndex = intIndex

            ddlESOS.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("ESOSADCode").Trim(), strType_Allowance, intIndex, "")
            ddlESOS.DataValueField = "ADCode"
            ddlESOS.DataTextField = "_Description"
            ddlESOS.DataBind()
            ddlESOS.SelectedIndex = intIndex

            ddlAttAllow.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("AttAllowanceADCode").Trim(), strType_Allowance, intIndex, "")
            ddlAttAllow.DataValueField = "ADCode"
            ddlAttAllow.DataTextField = "_Description"
            ddlAttAllow.DataBind()
            ddlAttAllow.SelectedIndex = intIndex
            
            ddlAdvSalary.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("AdvSalaryCode").Trim(), strType_Deduction, intIndex, "")
            ddlAdvSalary.DataValueField = "ADCode"
            ddlAdvSalary.DataTextField = "_Description"
            ddlAdvSalary.DataBind()
            ddlAdvSalary.SelectedIndex = intIndex

            ddlIN.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("INADCode").Trim(), strType_Deduction, intIndex, "")
            ddlIN.DataValueField = "ADCode"
            ddlIN.DataTextField = "_Description"
            ddlIN.DataBind()
            ddlIN.SelectedIndex = intIndex

            ddlCT.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("CTADCode").Trim(), strType_Deduction, intIndex, "")
            ddlCT.DataValueField = "ADCode"
            ddlCT.DataTextField = "_Description"
            ddlCT.DataBind()
            ddlCT.SelectedIndex = intIndex

            ddlWS.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("WSADCode").Trim(), strType_Deduction, intIndex, "")
            ddlWS.DataValueField = "ADCode"
            ddlWS.DataTextField = "_Description"
            ddlWS.DataBind()
            ddlWS.SelectedIndex = intIndex

            ddlWSRefund.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("WSRefundADCode").Trim(), strType_Deduction, intIndex, "")
            ddlWSRefund.DataValueField = "ADCode"
            ddlWSRefund.DataTextField = "_Description"
            ddlWSRefund.DataBind()
            ddlWSRefund.SelectedIndex = intIndex
            
            ddlLoan.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("LoanADCode").Trim(), strType_Deduction, intIndex, "")
            ddlLoan.DataValueField = "ADCode"
            ddlLoan.DataTextField = "_Description"
            ddlLoan.DataBind()
            ddlLoan.SelectedIndex = intIndex

            ddlBFEmp.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("BFEmpADCode").Trim(), strType_Deduction, intIndex, "")
            ddlBFEmp.DataValueField = "ADCode"
            ddlBFEmp.DataTextField = "_Description"
            ddlBFEmp.DataBind()
            ddlBFEmp.SelectedIndex = intIndex

            ddlOutPayEmp.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("OutPayEmpADCode").Trim(), strType_Deduction, intIndex, "")
            ddlOutPayEmp.DataValueField = "ADCode"
            ddlOutPayEmp.DataTextField = "_Description"
            ddlOutPayEmp.DataBind()
            ddlOutPayEmp.SelectedIndex = intIndex

            ddlAbsent.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("AbsentADCode").Trim(), strType_Deduction, intIndex, "")
            ddlAbsent.DataValueField = "ADCode"
            ddlAbsent.DataTextField = "_Description"
            ddlAbsent.DataBind()
            ddlAbsent.SelectedIndex = intIndex 

            ddlRiceDeduction.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("RiceDeductionADCode").Trim(), strType_Deduction, intIndex, "")
            ddlRiceDeduction.DataValueField = "ADCode"
            ddlRiceDeduction.DataTextField = "_Description"
            ddlRiceDeduction.DataBind()
            ddlRiceDeduction.SelectedIndex = intIndex 

            ddlSPSICOP.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("SPSICOPADCode").Trim(), strType_Deduction, intIndex, "")
            ddlSPSICOP.DataValueField = "ADCode"
            ddlSPSICOP.DataTextField = "_Description"
            ddlSPSICOP.DataBind()
            ddlSPSICOP.SelectedIndex = intIndex 

            ddlLuranCOP.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("LuranCOPADCode").Trim(), strType_Deduction, intIndex, "")
            ddlLuranCOP.DataValueField = "ADCode"
            ddlLuranCOP.DataTextField = "_Description"
            ddlLuranCOP.DataBind()
            ddlLuranCOP.SelectedIndex = intIndex 

            ddlOther.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("OtherADCode").Trim(), strType_Deduction, intIndex, "")
            ddlOther.DataValueField = "ADCode"
            ddlOther.DataTextField = "_Description"
            ddlOther.DataBind()
            ddlOther.SelectedIndex = intIndex 

            ddlHold.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("LevyHoldADCode").Trim(), strType_Deduction, intIndex, "")
            ddlHold.DataValueField = "ADCode"
            ddlHold.DataTextField = "_Description"
            ddlHold.DataBind()
            ddlHold.SelectedIndex = intIndex

            ddlPayment.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("LevyPayADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlPayment.DataValueField = "ADCode"
            ddlPayment.DataTextField = "_Description"
            ddlPayment.DataBind()
            ddlPayment.SelectedIndex = intIndex

            ddlSubsidy.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("LevySubsiADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlSubsidy.DataValueField = "ADCode"
            ddlSubsidy.DataTextField = "_Description"
            ddlSubsidy.DataBind()
            ddlSubsidy.SelectedIndex = intIndex

            ddlDeficit.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("LevyDeficitADCode").Trim(), strType_Deduction, intIndex, "")
            ddlDeficit.DataValueField = "ADCode"
            ddlDeficit.DataTextField = "_Description"
            ddlDeficit.DataBind()
            ddlDeficit.SelectedIndex = intIndex

            ddlLevyAdj.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("LevyAdjustADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlLevyAdj.DataValueField = "ADCode"
            ddlLevyAdj.DataTextField = "_Description"
            ddlLevyAdj.DataBind()
            ddlLevyAdj.SelectedIndex = intIndex

            ddlHouseRent.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("HouseRentADCode").Trim(), strType_Allowance, intIndex, "House")
            ddlHouseRent.DataValueField = "ADCode"
            ddlHouseRent.DataTextField = "_Description"
            ddlHouseRent.DataBind()
            ddlHouseRent.SelectedIndex = intIndex

            ddlMedical.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("MedicalADCode").Trim(), strType_Allowance, intIndex, "Med")
            ddlMedical.DataValueField = "ADCode"
            ddlMedical.DataTextField = "_Description"
            ddlMedical.DataBind()
            ddlMedical.SelectedIndex = intIndex

            ddlTax.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("TaxADCode").Trim(), strType_Allowance, intIndex, "Tax")
            ddlTax.DataValueField = "ADCode"
            ddlTax.DataTextField = "_Description"
            ddlTax.DataBind()
            ddlTax.SelectedIndex = intIndex

            ddlDanaPensiun.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("DanaPensiunADCode").Trim(), strType_Allowance, intIndex, "Pensiun")
            ddlDanaPensiun.DataValueField = "ADCode"
            ddlDanaPensiun.DataTextField = "_Description"
            ddlDanaPensiun.DataBind()
            ddlDanaPensiun.SelectedIndex = intIndex

            ddlRapel.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("RapelADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRapel.DataValueField = "ADCode"
            ddlRapel.DataTextField = "_Description"
            ddlRapel.DataBind()
            ddlRapel.SelectedIndex = intIndex

            ddlStaff.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("StaffADCode").Trim(), strType_Allowance, intIndex, "")
            ddlStaff.DataValueField = "ADCode"
            ddlStaff.DataTextField = "_Description"
            ddlStaff.DataBind()
            ddlStaff.SelectedIndex = intIndex

            ddlFunctional.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("FunctADCode").Trim(), strType_Allowance, intIndex, "")
            ddlFunctional.DataValueField = "ADCode"
            ddlFunctional.DataTextField = "_Description"
            ddlFunctional.DataBind()
            ddlFunctional.SelectedIndex = intIndex

            ddlHutang.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("HutangADCode").Trim(), strType_Deduction, intIndex, "")
            ddlHutang.DataValueField = "ADCode"
            ddlHutang.DataTextField = "_Description"
            ddlHutang.DataBind()
            ddlHutang.SelectedIndex = intIndex


            ddlMeal.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("MealADCode").Trim(), strType_Allowance, intIndex, "Meal")
            ddlMeal.DataValueField = "ADCode"
            ddlMeal.DataTextField = "_Description"
            ddlMeal.DataBind()
            ddlMeal.SelectedIndex = intIndex

            ddlLeave.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("LeaveADCode").Trim(), strType_Allowance, intIndex, "Leave")
            ddlLeave.DataValueField = "ADCode"
            ddlLeave.DataTextField = "_Description"
            ddlLeave.DataBind()
            ddlLeave.SelectedIndex = intIndex

            ddlAirBus.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("AirBusADCode").Trim(), strType_Allowance, intIndex, "AirBus")
            ddlAirBus.DataValueField = "ADCode"
            ddlAirBus.DataTextField = "_Description"
            ddlAirBus.DataBind()
            ddlAirBus.SelectedIndex = intIndex

            ddlMaternity.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("MaternityADCode").Trim(), strType_Allowance, intIndex, "Mat")
            ddlMaternity.DataValueField = "ADCode"
            ddlMaternity.DataTextField = "_Description"
            ddlMaternity.DataBind()
            ddlMaternity.SelectedIndex = intIndex

            ddlRelocation.DataSource = BindAD(objEmpCPDs.Tables(0).Rows(0).Item("RelocationADCode").Trim(), strType_Allowance, intIndex, "Relocation")
            ddlRelocation.DataValueField = "ADCode"
            ddlRelocation.DataTextField = "_Description"
            ddlRelocation.DataBind()
            ddlRelocation.SelectedIndex = intIndex

            If objEmpCPDs.Tables(0).Rows(0).Item("QuotaLevel") = objHRTrx.EnumQuotaLevel.NoQuota Then
                rbNoQuota.Checked = True
            ElseIf objEmpCPDs.Tables(0).Rows(0).Item("QuotaLevel") = objHRTrx.EnumQuotaLevel.Activity Then
                rbActivity.Checked = True
            ElseIf objEmpCPDs.Tables(0).Rows(0).Item("QuotaLevel") = objHRTrx.EnumQuotaLevel.Block Then
                rbBlock.Checked = True
            ElseIf objEmpCPDs.Tables(0).Rows(0).Item("QuotaLevel") = objHRTrx.EnumQuotaLevel.Individual Then
                rbIndividual.Checked = True
            Else
            End If

            If objEmpCPDs.Tables(0).Rows(0).Item("IndQuotaMethod") = objHRTrx.EnumQuotaMethod.ByHour Then
                rbByHour.Checked = True
            ElseIf objEmpCPDs.Tables(0).Rows(0).Item("IndQuotaMethod") = objHRTrx.EnumQuotaMethod.ByVolume Then
                rbByVolume.Checked = True
            Else
            End If
        ELSE
            objAllowanceDs = BindAD("", strType_Allowance, intIndex, "")

            objDeductionDs = BindAD("", strType_Deduction, intIndex, "")

            objMemoItemDs = BindAD("", strType_MemoItem, intIndex, "")
            
            ddlSalary.DataSource = objAllowanceDs
            ddlSalary.DataValueField = "ADCode"
            ddlSalary.DataTextField = "_Description"
            ddlSalary.DataBind()
            ddlSalary.SelectedIndex = intIndex

            ddlDaily.DataSource = objAllowanceDs
            ddlDaily.DataValueField = "ADCode"
            ddlDaily.DataTextField = "_Description"
            ddlDaily.DataBind()
            ddlDaily.SelectedIndex = intIndex

            ddlPiece.DataSource = objAllowanceDs
            ddlPiece.DataValueField = "ADCode"
            ddlPiece.DataTextField = "_Description"
            ddlPiece.DataBind()
            ddlPiece.SelectedIndex = intIndex

            ddlBonus.DataSource = objAllowanceDs
            ddlBonus.DataValueField = "ADCode"
            ddlBonus.DataTextField = "_Description"
            ddlBonus.DataBind()
            ddlBonus.SelectedIndex = intIndex

            ddlTHR.DataSource = objAllowanceDs
            ddlTHR.DataValueField = "ADCode"
            ddlTHR.DataTextField = "_Description"
            ddlTHR.DataBind()
            ddlTHR.SelectedIndex = intIndex

            ddlHouse.DataSource = objAllowanceDs
            ddlHouse.DataValueField = "ADCode"
            ddlHouse.DataTextField = "_Description"
            ddlHouse.DataBind()
            ddlHouse.SelectedIndex = intIndex

            ddlHardShip.DataSource = objAllowanceDs
            ddlHardShip.DataValueField = "ADCode"
            ddlHardShip.DataTextField = "_Description"
            ddlHardShip.DataBind()
            ddlHardShip.SelectedIndex = intIndex

            ddlIncAward.DataSource = objAllowanceDs
            ddlIncAward.DataValueField = "ADCode"
            ddlIncAward.DataTextField = "_Description"
            ddlIncAward.DataBind()
            ddlIncAward.SelectedIndex = intIndex

            ddlTransport.DataSource = objAllowanceDs
            ddlTransport.DataValueField = "ADCode"
            ddlTransport.DataTextField = "_Description"
            ddlTransport.DataBind()
            ddlTransport.SelectedIndex = intIndex
               
            ddlOvertime.DataSource = objAllowanceDs
            ddlOvertime.DataValueField = "ADCode"
            ddlOvertime.DataTextField = "_Description"
            ddlOvertime.DataBind()
            ddlOvertime.SelectedIndex = intIndex

            ddlTrip.DataSource = objAllowanceDs
            ddlTrip.DataValueField = "ADCode"
            ddlTrip.DataTextField = "_Description"
            ddlTrip.DataBind()
            ddlTrip.SelectedIndex = intIndex

            ddlRiceRation.DataSource = objAllowanceDs
            ddlRiceRation.DataValueField = "ADCode"
            ddlRiceRation.DataTextField = "_Description"
            ddlRiceRation.DataBind()
            ddlRiceRation.SelectedIndex = intIndex

            ddlQuotaIncCode.DataSource = objAllowanceDs
            ddlQuotaIncCode.DataValueField = "ADCode"
            ddlQuotaIncCode.DataTextField = "_Description"
            ddlQuotaIncCode.DataBind()
            ddlQuotaIncCode.SelectedIndex = intIndex

            ddlIncentive.DataSource = objAllowanceDs
            ddlIncentive.DataValueField = "ADCode"
            ddlIncentive.DataTextField = "_Description"
            ddlIncentive.DataBind()
            ddlIncentive.SelectedIndex = intIndex

            ddlContractPay.DataSource = objAllowanceDs
            ddlContractPay.DataValueField = "ADCode"
            ddlContractPay.DataTextField = "_Description"
            ddlContractPay.DataBind()
            ddlContractPay.SelectedIndex = intIndex

            ddlBIKAccom.DataSource = objAllowanceDs
            ddlBIKAccom.DataValueField = "ADCode"
            ddlBIKAccom.DataTextField = "_Description"
            ddlBIKAccom.DataBind()
            ddlBIKAccom.SelectedIndex = intIndex

            ddlBIKVeh.DataSource = objAllowanceDs
            ddlBIKVeh.DataValueField = "ADCode"
            ddlBIKVeh.DataTextField = "_Description"
            ddlBIKVeh.DataBind()
            ddlBIKVeh.SelectedIndex = intIndex

            ddlBIKHP.DataSource = objAllowanceDs
            ddlBIKHP.DataValueField = "ADCode"
            ddlBIKHP.DataTextField = "_Description"
            ddlBIKHP.DataBind()
            ddlBIKHP.SelectedIndex = intIndex

            ddlGratuity.DataSource = objAllowanceDs
            ddlGratuity.DataValueField = "ADCode"
            ddlGratuity.DataTextField = "_Description"
            ddlGratuity.DataBind()
            ddlGratuity.SelectedIndex = intIndex

            ddlRetrench.DataSource = objAllowanceDs
            ddlRetrench.DataValueField = "ADCode"
            ddlRetrench.DataTextField = "_Description"
            ddlRetrench.DataBind()
            ddlRetrench.SelectedIndex = intIndex

            ddlESOS.DataSource = objAllowanceDs
            ddlESOS.DataValueField = "ADCode"
            ddlESOS.DataTextField = "_Description"
            ddlESOS.DataBind()
            ddlESOS.SelectedIndex = intIndex

            ddlAttAllow.DataSource = objAllowanceDs
            ddlAttAllow.DataValueField = "ADCode"
            ddlAttAllow.DataTextField = "_Description"
            ddlAttAllow.DataBind()
            ddlAttAllow.SelectedIndex = intIndex
            
            ddlAdvSalary.DataSource = objDeductionDs
            ddlAdvSalary.DataValueField = "ADCode"
            ddlAdvSalary.DataTextField = "_Description"
            ddlAdvSalary.DataBind()
            ddlAdvSalary.SelectedIndex = intIndex 

            ddlIN.DataSource = objDeductionDs
            ddlIN.DataValueField = "ADCode"
            ddlIN.DataTextField = "_Description"
            ddlIN.DataBind()
            ddlIN.SelectedIndex = intIndex

            ddlCT.DataSource = objDeductionDs
            ddlCT.DataValueField = "ADCode"
            ddlCT.DataTextField = "_Description"
            ddlCT.DataBind()
            ddlCT.SelectedIndex = intIndex

            ddlWS.DataSource = objDeductionDs
            ddlWS.DataValueField = "ADCode"
            ddlWS.DataTextField = "_Description"
            ddlWS.DataBind()
            ddlWS.SelectedIndex = intIndex

            ddlWSRefund.DataSource = objDeductionDs
            ddlWSRefund.DataValueField = "ADCode"
            ddlWSRefund.DataTextField = "_Description"
            ddlWSRefund.DataBind()
            ddlWSRefund.SelectedIndex = intIndex

            ddlLoan.DataSource = objDeductionDs
            ddlLoan.DataValueField = "ADCode"
            ddlLoan.DataTextField = "_Description"
            ddlLoan.DataBind()
            ddlLoan.SelectedIndex = intIndex

            ddlBFEmp.DataSource = objDeductionDs
            ddlBFEmp.DataValueField = "ADCode"
            ddlBFEmp.DataTextField = "_Description"
            ddlBFEmp.DataBind()
            ddlBFEmp.SelectedIndex = intIndex

            ddlOutPayEmp.DataSource = objDeductionDs
            ddlOutPayEmp.DataValueField = "ADCode"
            ddlOutPayEmp.DataTextField = "_Description"
            ddlOutPayEmp.DataBind()
            ddlOutPayEmp.SelectedIndex = intIndex

            ddlAbsent.DataSource = objDeductionDs
            ddlAbsent.DataValueField = "ADCode"
            ddlAbsent.DataTextField = "_Description"
            ddlAbsent.DataBind()
            ddlAbsent.SelectedIndex = intIndex

            ddlRiceDeduction.DataSource = objDeductionDs
            ddlRiceDeduction.DataValueField = "ADCode"
            ddlRiceDeduction.DataTextField = "_Description"
            ddlRiceDeduction.DataBind()
            ddlRiceDeduction.SelectedIndex = intIndex

            ddlSPSICOP.DataSource = objDeductionDs
            ddlSPSICOP.DataValueField = "ADCode"
            ddlSPSICOP.DataTextField = "_Description"
            ddlSPSICOP.DataBind()
            ddlSPSICOP.SelectedIndex = intIndex

            ddlLuranCOP.DataSource = objDeductionDs
            ddlLuranCOP.DataValueField = "ADCode"
            ddlLuranCOP.DataTextField = "_Description"
            ddlLuranCOP.DataBind()
            ddlLuranCOP.SelectedIndex = intIndex

            ddlOther.DataSource = objDeductionDs
            ddlOther.DataValueField = "ADCode"
            ddlOther.DataTextField = "_Description"
            ddlOther.DataBind()
            ddlOther.SelectedIndex = intIndex

            ddlHold.DataSource = objDeductionDs
            ddlHold.DataValueField = "ADCode"
            ddlHold.DataTextField = "_Description"
            ddlHold.DataBind()
            ddlHold.SelectedIndex = intIndex

            ddlPayment.DataSource = objMemoItemDs
            ddlPayment.DataValueField = "ADCode"
            ddlPayment.DataTextField = "_Description"
            ddlPayment.DataBind()
            ddlPayment.SelectedIndex = intIndex

            ddlSubsidy.DataSource = objMemoItemDs
            ddlSubsidy.DataValueField = "ADCode"
            ddlSubsidy.DataTextField = "_Description"
            ddlSubsidy.DataBind()
            ddlSubsidy.SelectedIndex = intIndex

            ddlDeficit.DataSource = objDeductionDs
            ddlDeficit.DataValueField = "ADCode"
            ddlDeficit.DataTextField = "_Description"
            ddlDeficit.DataBind()
            ddlDeficit.SelectedIndex = intIndex 

            ddlLevyAdj.DataSource = objMemoItemDs
            ddlLevyAdj.DataValueField = "ADCode"
            ddlLevyAdj.DataTextField = "_Description"
            ddlLevyAdj.DataBind()
            ddlLevyAdj.SelectedIndex = intIndex

            ddlHouseRent.DataSource = objAllowanceDs
            ddlHouseRent.DataValueField = "ADCode"
            ddlHouseRent.DataTextField = "_Description"
            ddlHouseRent.DataBind()
            ddlHouseRent.SelectedIndex = intIndex

            ddlMedical.DataSource = objAllowanceDs
            ddlMedical.DataValueField = "ADCode"
            ddlMedical.DataTextField = "_Description"
            ddlMedical.DataBind()
            ddlMedical.SelectedIndex = intIndex

            ddlTax.DataSource = objAllowanceDs
            ddlTax.DataValueField = "ADCode"
            ddlTax.DataTextField = "_Description"
            ddlTax.DataBind()
            ddlTax.SelectedIndex = intIndex

            ddlDanaPensiun.DataSource = objAllowanceDs
            ddlDanaPensiun.DataValueField = "ADCode"
            ddlDanaPensiun.DataTextField = "_Description"
            ddlDanaPensiun.DataBind()
            ddlDanaPensiun.SelectedIndex = intIndex

            ddlRapel.DataSource = objAllowanceDs
            ddlRapel.DataValueField = "ADCode"
            ddlRapel.DataTextField = "_Description"
            ddlRapel.DataBind()
            ddlRapel.SelectedIndex = intIndex

            ddlStaff.DataSource = objAllowanceDs
            ddlStaff.DataValueField = "ADCode"
            ddlStaff.DataTextField = "_Description"
            ddlStaff.DataBind()
            ddlStaff.SelectedIndex = intIndex

            ddlFunctional.DataSource = objAllowanceDs
            ddlFunctional.DataValueField = "ADCode"
            ddlFunctional.DataTextField = "_Description"
            ddlFunctional.DataBind()
            ddlFunctional.SelectedIndex = intIndex

            ddlHutang.DataSource = objDeductionDs
            ddlHutang.DataValueField = "ADCode"
            ddlHutang.DataTextField = "_Description"
            ddlHutang.DataBind()
            ddlHutang.SelectedIndex = intIndex


            ddlMeal.DataSource = objAllowanceDs
            ddlMeal.DataValueField = "ADCode"
            ddlMeal.DataTextField = "_Description"
            ddlMeal.DataBind()
            ddlMeal.SelectedIndex = intIndex

            ddlLeave.DataSource = objAllowanceDs
            ddlLeave.DataValueField = "ADCode"
            ddlLeave.DataTextField = "_Description"
            ddlLeave.DataBind()
            ddlLeave.SelectedIndex = intIndex

            ddlAirBus.DataSource = objAllowanceDs
            ddlAirBus.DataValueField = "ADCode"
            ddlAirBus.DataTextField = "_Description"
            ddlAirBus.DataBind()
            ddlAirBus.SelectedIndex = intIndex

            ddlMaternity.DataSource = objAllowanceDs
            ddlMaternity.DataValueField = "ADCode"
            ddlMaternity.DataTextField = "_Description"
            ddlMaternity.DataBind()
            ddlMaternity.SelectedIndex = intIndex

            ddlRelocation.DataSource = objAllowanceDs
            ddlRelocation.DataValueField = "ADCode"
            ddlRelocation.DataTextField = "_Description"
            ddlRelocation.DataBind()
            ddlRelocation.SelectedIndex = intIndex

           
            If Not objAllowanceDs Is Nothing Then
                objAllowanceDs = Nothing
            End If
            If Not objDeductionDs Is Nothing Then
                objDeductionDs = Nothing
            End If
            If Not objMemoItemDs Is Nothing Then
                objMemoItemDs = Nothing
            End If

                
        
        End If
    End Sub

    Function BindAD(ByVal pv_strAD As String, ByVal pv_strADType As String, ByRef pv_intIndex As Integer, ByVal pv_strFlag As String) As Dataset
        Dim strOpCode As String = "PR_CLSSETUP_ADLIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        If Trim(pv_strADType) <> "" Then
            strParam = "AND AD.ADType = '" & trim(pv_strADType) & "' "
        End If

        If Trim(pv_strFlag) <> "" Then
            If Trim(pv_strFlag) = "Mat" Then
                strParam = strParam & " AND AD.MaternityInd='1' "
            End If
            If Trim(pv_strFlag) = "THR" Then
                strParam = strParam & " AND AD.THRInd='1' "
            End If
            If Trim(pv_strFlag) = "Tran" Then
                strParam = strParam & " AND AD.TransportInd='1' "
            End If
            If Trim(pv_strFlag) = "Med" Then
                strParam = strParam & " AND AD.MedicalInd='1' "
            End If
            If Trim(pv_strFlag) = "Meal" Then
                strParam = strParam & " AND AD.MealInd='1' "
            End If
            If Trim(pv_strFlag) = "Leave" Then
                strParam = strParam & " AND AD.LeaveInd='1' "
            End If
            If Trim(pv_strFlag) = "AirBus" Then
                strParam = strParam & " AND AD.AirBusInd='1' "
            End If
            If Trim(pv_strFlag) = "Tax" Then
                strParam = strParam & " AND AD.TaxInd='1' "
            End If
            If Trim(pv_strFlag) = "Pensiun" Then
                strParam = strParam & " AND AD.DanaPensiunInd='1' "
            End If
            If Trim(pv_strFlag) = "Relocation" Then
                strParam = strParam & " AND AD.RelocationInd='1' "
            End If
            If Trim(pv_strFlag) = "House" Then
                strParam = strParam & " AND AD.HouseInd='1' "
            End If
        End If

        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCode, _
                                            strParam, _
                                            objADDs, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_PAYROLLPROCESS_AD_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If Trim(pv_strAD) <> "" Then
            For intCnt = 0 To objADDs.Tables(0).Rows.Count - 1
                If Trim(objADDs.Tables(0).Rows(intCnt).Item("ADCode")) = Trim(pv_strAD) Then
                    intSelectIndex = intCnt + 1
                End If
            Next
        End If

        dr = objADDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("_Description") = "Select Allowance & Deduction Code"
        objADDs.Tables(0).Rows.InsertAt(dr, 0)
        pv_intIndex = intSelectIndex
        Return objADDs
    End Function

    Sub onLoad_DispLastCP()
        Dim strOpCdGet As String = "HR_CLSTRX_LASTCPDET_GET"
        Dim strOpCd_GetNew as string = "PR_CLSSETUP_PAYROLLPROCESS_GET"
        Dim strParam As String
        Dim strParamPPA As String
        Dim intErrNo As Integer
        Dim objDsPPA As New DataSet()
        Dim objAllowanceDs As New Object()
        Dim objDeductionDs As New Object()
        Dim objMemoItemDs As New Object()
        Dim strType_Allowance As String 
        Dim strType_Deduction As String
        Dim strType_EAItem As String
        Dim strType_MemoItem As String
        Dim intIndex As Integer

        strType_Allowance = objPRSetup.EnumADType.Allowance
        strType_Deduction = objPRSetup.EnumADType.Deduction
        strType_EAItem = objPRSetup.EnumADType.EAItem
        strType_MemoItem = objPRSetup.EnumADType.MemoItem

        lblEmpCode.Text = strSelEmpCode
        lblEmpName.Text = strSelEmpName
        lblCPID.Text = strSelCPID
        lblEmpStatus.Text = strSelEmpStatus

        strParam = "|" & strSelEmpCode & "|||B.CreateDate|desc"

        Try
            intErrNo = objHRTrx.mtdGetEmployeeCP(strOpCdGet, strParam, objLastCPDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPDET_GET_LASTCP&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeCPList.aspx")
        End Try

        If objLastCPDs.Tables(0).Rows.Count > 0 Then
            txtProbation.Text = objLastCPDs.Tables(0).Rows(0).Item("Probation")

            txtIndDailyQuota.Text = ObjGlobal.GetIDDecimalSeparator(objLastCPDs.Tables(0).Rows(0).Item("IndQuota"))
            txtIndQuotaIncRate.Text = ObjGlobal.GetIDDecimalSeparator(objLastCPDs.Tables(0).Rows(0).Item("IndQuotaIncRate"))
            lblCurrentRate.Text = ObjGlobal.GetIDDecimalSeparator(objLastCPDs.Tables(0).Rows(0).Item("NewRate"))
            lblHidQuotaLevel.Text = objLastCPDs.Tables(0).Rows(0).Item("QuotaLevel")
            
            lblHidIndQuota.Text = ObjGlobal.GetIDDecimalSeparator(objLastCPDs.Tables(0).Rows(0).Item("IndQuota"))
            lblHidIndQuotaMethod.Text = objLastCPDs.Tables(0).Rows(0).Item("IndQuotaMethod")
            
            lblHidIndQuotaIncRate.Text = ObjGlobal.GetIDDecimalSeparator(objLastCPDs.Tables(0).Rows(0).Item("IndQuotaIncRate"))
            BindPayType(objLastCPDs.Tables(0).Rows(0).Item("PayType"))
            BindComp(objLastCPDs.Tables(0).Rows(0).Item("CompCode"))
            BindLoc(objLastCPDs.Tables(0).Rows(0).Item("LocCode"))
            BindDept(objLastCPDs.Tables(0).Rows(0).Item("DeptCode"))
            BindLevel(objLastCPDs.Tables(0).Rows(0).Item("LevelCode"))
            BindRptTo(objLastCPDs.Tables(0).Rows(0).Item("RptTo"))
            BindSalScheme(objLastCPDs.Tables(0).Rows(0).Item("SalSchemeCode"))
            BindSalGrade(objLastCPDs.Tables(0).Rows(0).Item("SalGradeCode"))
            BindPosition(objLastCPDs.Tables(0).Rows(0).Item("PosCode")) 
            BindEvaluation(objLastCPDs.Tables(0).Rows(0).Item("EvalCode"))
            If Trim(objLastCPDs.Tables(0).Rows(0).Item("QuotaLevel")) = objHRTrx.EnumQuotaLevel.NoQuota Then
                rbNoQuota.Checked = True
            ElseIf Trim(objLastCPDs.Tables(0).Rows(0).Item("QuotaLevel")) = objHRTrx.EnumQuotaLevel.Activity Then
                rbActivity.Checked = True
            ElseIf Trim(objLastCPDs.Tables(0).Rows(0).Item("QuotaLevel")) = objHRTrx.EnumQuotaLevel.Block Then
                rbBlock.Checked = True
            ElseIf Trim(objLastCPDs.Tables(0).Rows(0).Item("QuotaLevel")) = objHRTrx.EnumQuotaLevel.Individual Then
                rbIndividual.Checked = True
            Else
            End If

            If objLastCPDs.Tables(0).Rows(0).Item("IndQuotaMethod") = objHRTrx.EnumQuotaMethod.ByHour Then
                rbByHour.Checked = True
            ElseIf objLastCPDs.Tables(0).Rows(0).Item("IndQuotaMethod") = objHRTrx.EnumQuotaMethod.ByVolume Then
                rbByVolume.Checked = True
            Else
            End If

            If CInt(objLastCPDs.Tables(0).Rows(0).Item("LevyDeductInd")) = objPRSetup.EnumPayrollSetupLevyDeduction.AccumulateMethod Then
                rbAccumulate.Checked = True
                rbPush.Checked = False
            Else
                rbAccumulate.Checked = False
                rbPush.Checked = True
            End If
            ddlSalary.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("SalaryADCode").Trim(), strType_Allowance, intIndex, "")
            ddlSalary.DataValueField = "ADCode"
            ddlSalary.DataTextField = "_Description"
            ddlSalary.DataBind()
            ddlSalary.SelectedIndex = intIndex

            ddlDaily.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("DailyRateADCode").Trim(), strType_Allowance, intIndex, "")
            ddlDaily.DataValueField = "ADCode"
            ddlDaily.DataTextField = "_Description"
            ddlDaily.DataBind()
            ddlDaily.SelectedIndex = intIndex

            ddlPiece.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("PieceRateADCode").Trim(), strType_Allowance, intIndex, "")
            ddlPiece.DataValueField = "ADCode"
            ddlPiece.DataTextField = "_Description"
            ddlPiece.DataBind()
            ddlPiece.SelectedIndex = intIndex

            ddlBonus.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("BonusADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBonus.DataValueField = "ADCode"
            ddlBonus.DataTextField = "_Description"
            ddlBonus.DataBind()
            ddlBonus.SelectedIndex = intIndex

            ddlTHR.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("THRADCode").Trim(), strType_Allowance, intIndex, "THR")
            ddlTHR.DataValueField = "ADCode"
            ddlTHR.DataTextField = "_Description"
            ddlTHR.DataBind()
            ddlTHR.SelectedIndex = intIndex

            ddlHouse.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("HouseADCode").Trim(), strType_Allowance, intIndex, "")
            ddlHouse.DataValueField = "ADCode"
            ddlHouse.DataTextField = "_Description"
            ddlHouse.DataBind()
            ddlHouse.SelectedIndex = intIndex

            ddlHardShip.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("HardshipADCode").Trim(), strType_Allowance, intIndex, "")
            ddlHardShip.DataValueField = "ADCode"
            ddlHardShip.DataTextField = "_Description"
            ddlHardShip.DataBind()
            ddlHardShip.SelectedIndex = intIndex

            ddlIncAward.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("IncAwardADCode").Trim(), strType_Allowance, intIndex, "")
            ddlIncAward.DataValueField = "ADCode"
            ddlIncAward.DataTextField = "_Description"
            ddlIncAward.DataBind()
            ddlIncAward.SelectedIndex = intIndex

            ddlTransport.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("TransADCode").Trim(), strType_Allowance, intIndex, "")
            ddlTransport.DataValueField = "ADCode"
            ddlTransport.DataTextField = "_Description"
            ddlTransport.DataBind()
            ddlTransport.SelectedIndex = intIndex

            ddlOvertime.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("OTADCode").Trim(), strType_Allowance, intIndex, "")
            ddlOvertime.DataValueField = "ADCode"
            ddlOvertime.DataTextField = "_Description"
            ddlOvertime.DataBind()
            ddlOvertime.SelectedIndex = intIndex

            ddlTrip.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("TripADCode").Trim(), strType_Allowance, intIndex, "")
            ddlTrip.DataValueField = "ADCode"
            ddlTrip.DataTextField = "_Description"
            ddlTrip.DataBind()
            ddlTrip.SelectedIndex = intIndex

            ddlRiceRation.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("RiceADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRiceRation.DataValueField = "ADCode"
            ddlRiceRation.DataTextField = "_Description"
            ddlRiceRation.DataBind()
            ddlRiceRation.SelectedIndex = intIndex

            ddlQuotaIncCode.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("QuotaIncADCode").Trim(), strType_Allowance, intIndex, "")
            ddlQuotaIncCode.DataValueField = "ADCode"
            ddlQuotaIncCode.DataTextField = "_Description"
            ddlQuotaIncCode.DataBind()
            ddlQuotaIncCode.SelectedIndex = intIndex

            ddlIncentive.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("IncADCode").Trim(), strType_Allowance, intIndex, "")
            ddlIncentive.DataValueField = "ADCode"
            ddlIncentive.DataTextField = "_Description"
            ddlIncentive.DataBind()
            ddlIncentive.SelectedIndex = intIndex

            ddlContractPay.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("ContractPayADCode").Trim(), strType_Allowance, intIndex, "")
            ddlContractPay.DataValueField = "ADCode"
            ddlContractPay.DataTextField = "_Description"
            ddlContractPay.DataBind()
            ddlContractPay.SelectedIndex = intIndex

            ddlBIKAccom.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("BIKAccomADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKAccom.DataValueField = "ADCode"
            ddlBIKAccom.DataTextField = "_Description"
            ddlBIKAccom.DataBind()
            ddlBIKAccom.SelectedIndex = intIndex

            ddlBIKVeh.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("BIKVehADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKVeh.DataValueField = "ADCode"
            ddlBIKVeh.DataTextField = "_Description"
            ddlBIKVeh.DataBind()
            ddlBIKVeh.SelectedIndex = intIndex

            ddlBIKHP.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("BIKHPADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKHP.DataValueField = "ADCode"
            ddlBIKHP.DataTextField = "_Description"
            ddlBIKHP.DataBind()
            ddlBIKHP.SelectedIndex = intIndex

            ddlGratuity.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("GratuityADCode").Trim(), strType_Allowance, intIndex, "")
            ddlGratuity.DataValueField = "ADCode"
            ddlGratuity.DataTextField = "_Description"
            ddlGratuity.DataBind()
            ddlGratuity.SelectedIndex = intIndex

            ddlRetrench.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("RetrenchCompADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRetrench.DataValueField = "ADCode"
            ddlRetrench.DataTextField = "_Description"
            ddlRetrench.DataBind()
            ddlRetrench.SelectedIndex = intIndex

            ddlESOS.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("ESOSADCode").Trim(), strType_Allowance, intIndex, "")
            ddlESOS.DataValueField = "ADCode"
            ddlESOS.DataTextField = "_Description"
            ddlESOS.DataBind()
            ddlESOS.SelectedIndex = intIndex

            ddlAttAllow.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("AttAllowanceADCode").Trim(), strType_Allowance, intIndex, "")
            ddlAttAllow.DataValueField = "ADCode"
            ddlAttAllow.DataTextField = "_Description"
            ddlAttAllow.DataBind()
            ddlAttAllow.SelectedIndex = intIndex
            
            ddlAdvSalary.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("AdvSalaryCode").Trim(), strType_Deduction, intIndex, "")
            ddlAdvSalary.DataValueField = "ADCode"
            ddlAdvSalary.DataTextField = "_Description"
            ddlAdvSalary.DataBind()
            ddlAdvSalary.SelectedIndex = intIndex

            ddlIN.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("INADCode").Trim(), strType_Deduction, intIndex, "")
            ddlIN.DataValueField = "ADCode"
            ddlIN.DataTextField = "_Description"
            ddlIN.DataBind()
            ddlIN.SelectedIndex = intIndex

            ddlCT.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("CTADCode").Trim(), strType_Deduction, intIndex, "")
            ddlCT.DataValueField = "ADCode"
            ddlCT.DataTextField = "_Description"
            ddlCT.DataBind()
            ddlCT.SelectedIndex = intIndex

            ddlWS.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("WSADCode").Trim(), strType_Deduction, intIndex, "")
            ddlWS.DataValueField = "ADCode"
            ddlWS.DataTextField = "_Description"
            ddlWS.DataBind()
            ddlWS.SelectedIndex = intIndex

            ddlWSRefund.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("WSRefundADCode").Trim(), strType_Deduction, intIndex, "")
            ddlWSRefund.DataValueField = "ADCode"
            ddlWSRefund.DataTextField = "_Description"
            ddlWSRefund.DataBind()
            ddlWSRefund.SelectedIndex = intIndex
            
            ddlLoan.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("LoanADCode").Trim(), strType_Deduction, intIndex, "")
            ddlLoan.DataValueField = "ADCode"
            ddlLoan.DataTextField = "_Description"
            ddlLoan.DataBind()
            ddlLoan.SelectedIndex = intIndex

            ddlBFEmp.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("BFEmpADCode").Trim(), strType_Deduction, intIndex, "")
            ddlBFEmp.DataValueField = "ADCode"
            ddlBFEmp.DataTextField = "_Description"
            ddlBFEmp.DataBind()
            ddlBFEmp.SelectedIndex = intIndex

            ddlOutPayEmp.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("OutPayEmpADCode").Trim(), strType_Deduction, intIndex, "")
            ddlOutPayEmp.DataValueField = "ADCode"
            ddlOutPayEmp.DataTextField = "_Description"
            ddlOutPayEmp.DataBind()
            ddlOutPayEmp.SelectedIndex = intIndex

            ddlAbsent.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("AbsentADCode").Trim(), strType_Deduction, intIndex, "")
            ddlAbsent.DataValueField = "ADCode"
            ddlAbsent.DataTextField = "_Description"
            ddlAbsent.DataBind()
            ddlAbsent.SelectedIndex = intIndex 

            ddlRiceDeduction.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("RiceDeductionADCode").Trim(), strType_Deduction, intIndex, "")
            ddlRiceDeduction.DataValueField = "ADCode"
            ddlRiceDeduction.DataTextField = "_Description"
            ddlRiceDeduction.DataBind()
            ddlRiceDeduction.SelectedIndex = intIndex 

            ddlSPSICOP.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("SPSICOPADCode").Trim(), strType_Deduction, intIndex, "")
            ddlSPSICOP.DataValueField = "ADCode"
            ddlSPSICOP.DataTextField = "_Description"
            ddlSPSICOP.DataBind()
            ddlSPSICOP.SelectedIndex = intIndex 

            ddlLuranCOP.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("LuranCOPADCode").Trim(), strType_Deduction, intIndex, "")
            ddlLuranCOP.DataValueField = "ADCode"
            ddlLuranCOP.DataTextField = "_Description"
            ddlLuranCOP.DataBind()
            ddlLuranCOP.SelectedIndex = intIndex 

            ddlOther.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("OtherADCode").Trim(), strType_Deduction, intIndex, "")
            ddlOther.DataValueField = "ADCode"
            ddlOther.DataTextField = "_Description"
            ddlOther.DataBind()
            ddlOther.SelectedIndex = intIndex 

            ddlHold.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("LevyHoldADCode").Trim(), strType_Deduction, intIndex, "")
            ddlHold.DataValueField = "ADCode"
            ddlHold.DataTextField = "_Description"
            ddlHold.DataBind()
            ddlHold.SelectedIndex = intIndex

            ddlPayment.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("LevyPayADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlPayment.DataValueField = "ADCode"
            ddlPayment.DataTextField = "_Description"
            ddlPayment.DataBind()
            ddlPayment.SelectedIndex = intIndex

            ddlSubsidy.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("LevySubsiADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlSubsidy.DataValueField = "ADCode"
            ddlSubsidy.DataTextField = "_Description"
            ddlSubsidy.DataBind()
            ddlSubsidy.SelectedIndex = intIndex

            ddlDeficit.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("LevyDeficitADCode").Trim(), strType_Deduction, intIndex, "")
            ddlDeficit.DataValueField = "ADCode"
            ddlDeficit.DataTextField = "_Description"
            ddlDeficit.DataBind()
            ddlDeficit.SelectedIndex = intIndex

            ddlLevyAdj.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("LevyAdjustADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlLevyAdj.DataValueField = "ADCode"
            ddlLevyAdj.DataTextField = "_Description"
            ddlLevyAdj.DataBind()
            ddlLevyAdj.SelectedIndex = intIndex

            ddlHouseRent.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("HouseRentADCode").Trim(), strType_Allowance, intIndex, "House")
            ddlHouseRent.DataValueField = "ADCode"
            ddlHouseRent.DataTextField = "_Description"
            ddlHouseRent.DataBind()
            ddlHouseRent.SelectedIndex = intIndex

            ddlMedical.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("MedicalADCode").Trim(), strType_Allowance, intIndex, "Med")
            ddlMedical.DataValueField = "ADCode"
            ddlMedical.DataTextField = "_Description"
            ddlMedical.DataBind()
            ddlMedical.SelectedIndex = intIndex

            ddlTax.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("TaxADCode").Trim(), strType_Allowance, intIndex, "Tax")
            ddlTax.DataValueField = "ADCode"
            ddlTax.DataTextField = "_Description"
            ddlTax.DataBind()
            ddlTax.SelectedIndex = intIndex

            ddlDanaPensiun.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("DanaPensiunADCode").Trim(), strType_Allowance, intIndex, "Pensiun")
            ddlDanaPensiun.DataValueField = "ADCode"
            ddlDanaPensiun.DataTextField = "_Description"
            ddlDanaPensiun.DataBind()
            ddlDanaPensiun.SelectedIndex = intIndex

            ddlRapel.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("RapelADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRapel.DataValueField = "ADCode"
            ddlRapel.DataTextField = "_Description"
            ddlRapel.DataBind()
            ddlRapel.SelectedIndex = intIndex

            ddlStaff.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("StaffADCode").Trim(), strType_Allowance, intIndex, "")
            ddlStaff.DataValueField = "ADCode"
            ddlStaff.DataTextField = "_Description"
            ddlStaff.DataBind()
            ddlStaff.SelectedIndex = intIndex

            ddlFunctional.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("FunctADCode").Trim(), strType_Allowance, intIndex, "")
            ddlFunctional.DataValueField = "ADCode"
            ddlFunctional.DataTextField = "_Description"
            ddlFunctional.DataBind()
            ddlFunctional.SelectedIndex = intIndex

            ddlHutang.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("HutangADCode").Trim(), strType_Deduction, intIndex, "")
            ddlHutang.DataValueField = "ADCode"
            ddlHutang.DataTextField = "_Description"
            ddlHutang.DataBind()
            ddlHutang.SelectedIndex = intIndex


            ddlMeal.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("MealADCode").Trim(), strType_Allowance, intIndex, "Meal")
            ddlMeal.DataValueField = "ADCode"
            ddlMeal.DataTextField = "_Description"
            ddlMeal.DataBind()
            ddlMeal.SelectedIndex = intIndex

            ddlLeave.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("LeaveADCode").Trim(), strType_Allowance, intIndex, "Leave")
            ddlLeave.DataValueField = "ADCode"
            ddlLeave.DataTextField = "_Description"
            ddlLeave.DataBind()
            ddlLeave.SelectedIndex = intIndex

            ddlAirBus.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("AirBusADCode").Trim(), strType_Allowance, intIndex, "AirBus")
            ddlAirBus.DataValueField = "ADCode"
            ddlAirBus.DataTextField = "_Description"
            ddlAirBus.DataBind()
            ddlAirBus.SelectedIndex = intIndex

            ddlMaternity.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("MaternityADCode").Trim(), strType_Allowance, intIndex, "Mat")
            ddlMaternity.DataValueField = "ADCode"
            ddlMaternity.DataTextField = "_Description"
            ddlMaternity.DataBind()
            ddlMaternity.SelectedIndex = intIndex

            ddlRelocation.DataSource = BindAD(objLastCPDs.Tables(0).Rows(0).Item("RelocationADCode").Trim(), strType_Allowance, intIndex, "Relocation")
            ddlRelocation.DataValueField = "ADCode"
            ddlRelocation.DataTextField = "_Description"
            ddlRelocation.DataBind()
            ddlRelocation.SelectedIndex = intIndex

            If objLastCPDs.Tables(0).Rows(0).Item("QuotaLevel") = objHRTrx.EnumQuotaLevel.NoQuota Then
                rbNoQuota.Checked = True
            ElseIf objLastCPDs.Tables(0).Rows(0).Item("QuotaLevel") = objHRTrx.EnumQuotaLevel.Activity Then
                rbActivity.Checked = True
            ElseIf objLastCPDs.Tables(0).Rows(0).Item("QuotaLevel") = objHRTrx.EnumQuotaLevel.Block Then
                rbBlock.Checked = True
            ElseIf objLastCPDs.Tables(0).Rows(0).Item("QuotaLevel") = objHRTrx.EnumQuotaLevel.Individual Then
                rbIndividual.Checked = True
            Else
            End If

            If objLastCPDs.Tables(0).Rows(0).Item("IndQuotaMethod") = objHRTrx.EnumQuotaMethod.ByHour Then
                rbByHour.Checked = True
            ElseIf objLastCPDs.Tables(0).Rows(0).Item("IndQuotaMethod") = objHRTrx.EnumQuotaMethod.ByVolume Then
                rbByVolume.Checked = True
            Else
            End If
        Else
            BindComp(strCompany)
            BindLoc(strLocation)
            BindDept("")
            BindLevel("")
            BindRptTo("")
            BindSalScheme("")
            BindSalGrade("")
            BindPosition("")
            BindEvaluation("")
            BindPayType("0")

            strParam = ""
            Try
                intErrNo = objPRSetup.mtdGetAD(strOpCd_GetNew, _
                                                strParam, _
                                                objADPaySetupDs, _
                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_PAYROLLPROCESS_AD_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try
            if objADPaySetupDs.Tables(0).Rows.Count > 0 then

            ddlSalary.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("SalaryADCode").Trim(), strType_Allowance, intIndex, "")
            ddlSalary.DataValueField = "ADCode"
            ddlSalary.DataTextField = "_Description"
            ddlSalary.DataBind()
            ddlSalary.SelectedIndex = intIndex

            ddlDaily.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("DailyRateADCode").Trim(), strType_Allowance, intIndex, "")
            ddlDaily.DataValueField = "ADCode"
            ddlDaily.DataTextField = "_Description"
            ddlDaily.DataBind()
            ddlDaily.SelectedIndex = intIndex

            ddlPiece.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("PieceRateADCode").Trim(), strType_Allowance, intIndex, "")
            ddlPiece.DataValueField = "ADCode"
            ddlPiece.DataTextField = "_Description"
            ddlPiece.DataBind()
            ddlPiece.SelectedIndex = intIndex

            ddlBonus.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("BonusADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBonus.DataValueField = "ADCode"
            ddlBonus.DataTextField = "_Description"
            ddlBonus.DataBind()
            ddlBonus.SelectedIndex = intIndex

            ddlTHR.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("THRADCode").Trim(), strType_Allowance, intIndex, "THR")
            ddlTHR.DataValueField = "ADCode"
            ddlTHR.DataTextField = "_Description"
            ddlTHR.DataBind()
            ddlTHR.SelectedIndex = intIndex

            ddlHouse.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("HouseADCode").Trim(), strType_Allowance, intIndex, "")
            ddlHouse.DataValueField = "ADCode"
            ddlHouse.DataTextField = "_Description"
            ddlHouse.DataBind()
            ddlHouse.SelectedIndex = intIndex

            ddlHardShip.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("HardshipADCode").Trim(), strType_Allowance, intIndex, "")
            ddlHardShip.DataValueField = "ADCode"
            ddlHardShip.DataTextField = "_Description"
            ddlHardShip.DataBind()
            ddlHardShip.SelectedIndex = intIndex

            ddlIncAward.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("IncAwardADCode").Trim(), strType_Allowance, intIndex, "")
            ddlIncAward.DataValueField = "ADCode"
            ddlIncAward.DataTextField = "_Description"
            ddlIncAward.DataBind()
            ddlIncAward.SelectedIndex = intIndex

            ddlTransport.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("TransADCode").Trim(), strType_Allowance, intIndex, "Tran")
            ddlTransport.DataValueField = "ADCode"
            ddlTransport.DataTextField = "_Description"
            ddlTransport.DataBind()
            ddlTransport.SelectedIndex = intIndex

            ddlOvertime.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("OTADCode").Trim(), strType_Allowance, intIndex, "")
            ddlOvertime.DataValueField = "ADCode"
            ddlOvertime.DataTextField = "_Description"
            ddlOvertime.DataBind()
            ddlOvertime.SelectedIndex = intIndex

            ddlTrip.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("TripADCode").Trim(), strType_Allowance, intIndex, "")
            ddlTrip.DataValueField = "ADCode"
            ddlTrip.DataTextField = "_Description"
            ddlTrip.DataBind()
            ddlTrip.SelectedIndex = intIndex

            ddlRiceRation.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("RiceADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRiceRation.DataValueField = "ADCode"
            ddlRiceRation.DataTextField = "_Description"
            ddlRiceRation.DataBind()
            ddlRiceRation.SelectedIndex = intIndex

            ddlQuotaIncCode.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("QuotaIncADCode").Trim(), strType_Allowance, intIndex, "")
            ddlQuotaIncCode.DataValueField = "ADCode"
            ddlQuotaIncCode.DataTextField = "_Description"
            ddlQuotaIncCode.DataBind()
            ddlQuotaIncCode.SelectedIndex = intIndex

            ddlIncentive.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("IncADCode").Trim(), strType_Allowance, intIndex, "")
            ddlIncentive.DataValueField = "ADCode"
            ddlIncentive.DataTextField = "_Description"
            ddlIncentive.DataBind()
            ddlIncentive.SelectedIndex = intIndex

            ddlContractPay.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("ContractPayADCode").Trim(), strType_Allowance, intIndex, "")
            ddlContractPay.DataValueField = "ADCode"
            ddlContractPay.DataTextField = "_Description"
            ddlContractPay.DataBind()
            ddlContractPay.SelectedIndex = intIndex

            ddlBIKAccom.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("BIKAccomADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKAccom.DataValueField = "ADCode"
            ddlBIKAccom.DataTextField = "_Description"
            ddlBIKAccom.DataBind()
            ddlBIKAccom.SelectedIndex = intIndex

            ddlBIKVeh.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("BIKVehADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKVeh.DataValueField = "ADCode"
            ddlBIKVeh.DataTextField = "_Description"
            ddlBIKVeh.DataBind()
            ddlBIKVeh.SelectedIndex = intIndex

            ddlBIKHP.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("BIKHPADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKHP.DataValueField = "ADCode"
            ddlBIKHP.DataTextField = "_Description"
            ddlBIKHP.DataBind()
            ddlBIKHP.SelectedIndex = intIndex

            ddlGratuity.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("GratuityADCode").Trim(), strType_Allowance, intIndex, "")
            ddlGratuity.DataValueField = "ADCode"
            ddlGratuity.DataTextField = "_Description"
            ddlGratuity.DataBind()
            ddlGratuity.SelectedIndex = intIndex

            ddlRetrench.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("RetrenchCompADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRetrench.DataValueField = "ADCode"
            ddlRetrench.DataTextField = "_Description"
            ddlRetrench.DataBind()
            ddlRetrench.SelectedIndex = intIndex

            ddlESOS.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("ESOSADCode").Trim(), strType_Allowance, intIndex, "")
            ddlESOS.DataValueField = "ADCode"
            ddlESOS.DataTextField = "_Description"
            ddlESOS.DataBind()
            ddlESOS.SelectedIndex = intIndex

            ddlAttAllow.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("AttAllowanceADCode").Trim(), strType_Allowance, intIndex, "")
            ddlAttAllow.DataValueField = "ADCode"
            ddlAttAllow.DataTextField = "_Description"
            ddlAttAllow.DataBind()
            ddlAttAllow.SelectedIndex = intIndex
            
            ddlAdvSalary.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("AdvSalaryCode").Trim(), strType_Deduction, intIndex, "")
            ddlAdvSalary.DataValueField = "ADCode"
            ddlAdvSalary.DataTextField = "_Description"
            ddlAdvSalary.DataBind()
            ddlAdvSalary.SelectedIndex = intIndex

            ddlIN.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("INADCode").Trim(), strType_Deduction, intIndex, "")
            ddlIN.DataValueField = "ADCode"
            ddlIN.DataTextField = "_Description"
            ddlIN.DataBind()
            ddlIN.SelectedIndex = intIndex

            ddlCT.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("CTADCode").Trim(), strType_Deduction, intIndex, "")
            ddlCT.DataValueField = "ADCode"
            ddlCT.DataTextField = "_Description"
            ddlCT.DataBind()
            ddlCT.SelectedIndex = intIndex

            ddlWS.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("WSADCode").Trim(), strType_Deduction, intIndex, "")
            ddlWS.DataValueField = "ADCode"
            ddlWS.DataTextField = "_Description"
            ddlWS.DataBind()
            ddlWS.SelectedIndex = intIndex

            ddlWSRefund.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("WSRefundADCode").Trim(), strType_Deduction, intIndex, "")
            ddlWSRefund.DataValueField = "ADCode"
            ddlWSRefund.DataTextField = "_Description"
            ddlWSRefund.DataBind()
            ddlWSRefund.SelectedIndex = intIndex
            
            ddlLoan.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("LoanADCode").Trim(), strType_Deduction, intIndex, "")
            ddlLoan.DataValueField = "ADCode"
            ddlLoan.DataTextField = "_Description"
            ddlLoan.DataBind()
            ddlLoan.SelectedIndex = intIndex

            ddlBFEmp.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("BFEmpADCode").Trim(), strType_Deduction, intIndex, "")
            ddlBFEmp.DataValueField = "ADCode"
            ddlBFEmp.DataTextField = "_Description"
            ddlBFEmp.DataBind()
            ddlBFEmp.SelectedIndex = intIndex

            ddlOutPayEmp.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("OutPayEmpADCode").Trim(), strType_Deduction, intIndex, "")
            ddlOutPayEmp.DataValueField = "ADCode"
            ddlOutPayEmp.DataTextField = "_Description"
            ddlOutPayEmp.DataBind()
            ddlOutPayEmp.SelectedIndex = intIndex

            ddlAbsent.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("AbsentADCode").Trim(), strType_Deduction, intIndex, "")
            ddlAbsent.DataValueField = "ADCode"
            ddlAbsent.DataTextField = "_Description"
            ddlAbsent.DataBind()
            ddlAbsent.SelectedIndex = intIndex 

            ddlRiceDeduction.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("RiceDeductionADCode").Trim(), strType_Deduction, intIndex, "")
            ddlRiceDeduction.DataValueField = "ADCode"
            ddlRiceDeduction.DataTextField = "_Description"
            ddlRiceDeduction.DataBind()
            ddlRiceDeduction.SelectedIndex = intIndex 

            ddlSPSICOP.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("SPSICOPADCode").Trim(), strType_Deduction, intIndex, "")
            ddlSPSICOP.DataValueField = "ADCode"
            ddlSPSICOP.DataTextField = "_Description"
            ddlSPSICOP.DataBind()
            ddlSPSICOP.SelectedIndex = intIndex 

            ddlLuranCOP.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("LuranCOPADCode").Trim(), strType_Deduction, intIndex, "")
            ddlLuranCOP.DataValueField = "ADCode"
            ddlLuranCOP.DataTextField = "_Description"
            ddlLuranCOP.DataBind()
            ddlLuranCOP.SelectedIndex = intIndex 

            ddlOther.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("OtherADCode").Trim(), strType_Deduction, intIndex, "")
            ddlOther.DataValueField = "ADCode"
            ddlOther.DataTextField = "_Description"
            ddlOther.DataBind()
            ddlOther.SelectedIndex = intIndex 

            ddlHold.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("LevyHoldADCode").Trim(), strType_Deduction, intIndex, "")
            ddlHold.DataValueField = "ADCode"
            ddlHold.DataTextField = "_Description"
            ddlHold.DataBind()
            ddlHold.SelectedIndex = intIndex

            ddlPayment.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("LevyPayADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlPayment.DataValueField = "ADCode"
            ddlPayment.DataTextField = "_Description"
            ddlPayment.DataBind()
            ddlPayment.SelectedIndex = intIndex

            ddlSubsidy.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("LevySubsiADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlSubsidy.DataValueField = "ADCode"
            ddlSubsidy.DataTextField = "_Description"
            ddlSubsidy.DataBind()
            ddlSubsidy.SelectedIndex = intIndex

            ddlDeficit.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("LevyDeficitADCode").Trim(), strType_Deduction, intIndex, "")
            ddlDeficit.DataValueField = "ADCode"
            ddlDeficit.DataTextField = "_Description"
            ddlDeficit.DataBind()
            ddlDeficit.SelectedIndex = intIndex

            ddlLevyAdj.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("LevyAdjustADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlLevyAdj.DataValueField = "ADCode"
            ddlLevyAdj.DataTextField = "_Description"
            ddlLevyAdj.DataBind()
            ddlLevyAdj.SelectedIndex = intIndex

            ddlHouseRent.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("HouseRentADCode").Trim(), strType_Allowance, intIndex, "House")
            ddlHouseRent.DataValueField = "ADCode"
            ddlHouseRent.DataTextField = "_Description"
            ddlHouseRent.DataBind()
            ddlHouseRent.SelectedIndex = intIndex

            ddlMedical.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("MedicalADCode").Trim(), strType_Allowance, intIndex, "Med")
            ddlMedical.DataValueField = "ADCode"
            ddlMedical.DataTextField = "_Description"
            ddlMedical.DataBind()
            ddlMedical.SelectedIndex = intIndex

            ddlTax.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("TaxADCode").Trim(), strType_Allowance, intIndex, "Tax")
            ddlTax.DataValueField = "ADCode"
            ddlTax.DataTextField = "_Description"
            ddlTax.DataBind()
            ddlTax.SelectedIndex = intIndex

            ddlDanaPensiun.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("DanaPensiunADCode").Trim(), strType_Allowance, intIndex, "Pensiun")
            ddlDanaPensiun.DataValueField = "ADCode"
            ddlDanaPensiun.DataTextField = "_Description"
            ddlDanaPensiun.DataBind()
            ddlDanaPensiun.SelectedIndex = intIndex

            ddlRapel.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("RapelADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRapel.DataValueField = "ADCode"
            ddlRapel.DataTextField = "_Description"
            ddlRapel.DataBind()
            ddlRapel.SelectedIndex = intIndex

            ddlStaff.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("StaffADCode").Trim(), strType_Allowance, intIndex, "")
            ddlStaff.DataValueField = "ADCode"
            ddlStaff.DataTextField = "_Description"
            ddlStaff.DataBind()
            ddlStaff.SelectedIndex = intIndex

            ddlFunctional.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("FunctADCode").Trim(), strType_Allowance, intIndex, "")
            ddlFunctional.DataValueField = "ADCode"
            ddlFunctional.DataTextField = "_Description"
            ddlFunctional.DataBind()
            ddlFunctional.SelectedIndex = intIndex

            ddlHutang.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("HutangADCode").Trim(), strType_Deduction, intIndex, "")
            ddlHutang.DataValueField = "ADCode"
            ddlHutang.DataTextField = "_Description"
            ddlHutang.DataBind()
            ddlHutang.SelectedIndex = intIndex


            ddlMeal.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("MealADCode").Trim(), strType_Allowance, intIndex, "Meal")
            ddlMeal.DataValueField = "ADCode"
            ddlMeal.DataTextField = "_Description"
            ddlMeal.DataBind()
            ddlMeal.SelectedIndex = intIndex

            ddlLeave.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("LeaveADCode").Trim(), strType_Allowance, intIndex, "Leave")
            ddlLeave.DataValueField = "ADCode"
            ddlLeave.DataTextField = "_Description"
            ddlLeave.DataBind()
            ddlLeave.SelectedIndex = intIndex

            ddlAirBus.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("AirBusADCode").Trim(), strType_Allowance, intIndex, "AirBus")
            ddlAirBus.DataValueField = "ADCode"
            ddlAirBus.DataTextField = "_Description"
            ddlAirBus.DataBind()
            ddlAirBus.SelectedIndex = intIndex

            ddlMaternity.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("MaternityADCode").Trim(), strType_Allowance, intIndex, "Mat")
            ddlMaternity.DataValueField = "ADCode"
            ddlMaternity.DataTextField = "_Description"
            ddlMaternity.DataBind()
            ddlMaternity.SelectedIndex = intIndex

            ddlRelocation.DataSource = BindAD(objADPaySetupDs.Tables(0).Rows(0).Item("RelocationADCode").Trim(), strType_Allowance, intIndex, "Relocation")
            ddlRelocation.DataValueField = "ADCode"
            ddlRelocation.DataTextField = "_Description"
            ddlRelocation.DataBind()
            ddlRelocation.SelectedIndex = intIndex

            End If

        End If

    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddCP As String = "HR_CLSTRX_CAREERPROGRESS_ADD"
        Dim strOpCd_UpdEmp As String = "HR_CLSTRX_EMPLOYMENT_UPD"
        Dim strOpCd_UpdPay As String = "HR_CLSTRX_PAYROLL_UPD"
        Dim strOpCd_SalGradeGet As String = "HR_CLSSETUP_SALGRADE_GET"

        Dim strEmpCode As String = lblEmpCode.Text
        Dim strCPCode As String = ddlCPCode.SelectedItem.Value
        Dim strCPType As String = lblCPType.Text
        Dim strDateFrom As String = txtDateFrom.Text
        Dim strDateTo As String = txtDateTo.Text
        Dim strCeaseDate As String = txtCeaseDate.Text
        Dim strRemark As String = txtRemark.Text
        Dim strCompCode As String = ddlCompCode.SelectedItem.Value
        Dim strLocCode As String = ddlLocCode.SelectedItem.Value
        Dim strDeptCode As String = ddlDeptCode.SelectedItem.Value
        Dim strLevelCode As String = ddlLevelCode.SelectedItem.Value
        Dim strRptTo As String = Request.Form("ddlRptTo")
        Dim strSalScheme As String = ddlSalSchemeCode.SelectedItem.Value
        Dim strSalGrade As String = ddlSalGradeCode.SelectedItem.Value
        Dim strPosCode As String = ddlPosCode.SelectedItem.Value
        Dim strProbation As String = txtProbation.Text
        Dim strPayType As String = ddlPayType.SelectedItem.Value
        Dim strCurrentRate As String = lblCurrentRate.Text
        Dim strIncrementAmt As String = txtIncrementAmt.Text
        Dim decIndDailyQuota As Decimal = txtIndDailyQuota.Text
        Dim decIndQuotaIncRate As Decimal = txtIndQuotaIncRate.Text
        Dim strEvalCode As String = ddlEvalCode.SelectedItem.Value
        Dim intLevyDeduction As Integer
        Dim decCurrentRate As Decimal
        Dim decIncrementRate As Decimal
        Dim decTotalRate As Decimal
        Dim strQuotaLevel As String
        Dim strIndQuotaMethod As String
        
        Dim objDateFromFmt As String
        Dim objDateFrom As String
        Dim objDateToFmt As String
        Dim objDateTo As String
        Dim objCeaseDateFmt As String
        Dim objCeaseDate As String

        Dim intErrNo As Integer
        Dim strParam As String
        Dim objCPID As New Object()
        Dim decMaxBasicSal As Decimal
        
        IncrementAmt.Value = strIncrementAmt
        ProbationPeriod.Value = strProbation

        If strDateFrom <> "" Then
            If objGlobal.mtdValidInputDate(strDateFmt, strDateFrom, objDateFromFmt, objDateFrom) = False Then
                lblErrDateFrom.Text = lblErrDateFrom.Text & objDateFromFmt
                lblErrDateFrom.Visible = True
                Exit Sub
            End If
        End If

        If strDateTo <> "" Then
            If objGlobal.mtdValidInputDate(strDateFmt, strDateTo, objDateToFmt, objDateTo) = False Then
                lblErrDateTo.Text = lblErrDateTo.Text & objDateToFmt
                lblErrDateTo.Visible = True
                Exit Sub
            End If
        End If

        If strCeaseDate <> "" Then
            If objGlobal.mtdValidInputDate(strDateFmt, strCeaseDate, objCeaseDateFmt, objCeaseDate) = False Then
                lblErrCeaseDate.Text = lblErrCeaseDate.Text & objCeaseDateFmt
                lblErrCeaseDate.Visible = True
                Exit Sub
            End If
        End If

        If lblCPType.Text <> "" Then
            If CInt(lblCPType.Text) = objHRSetup.EnumCPType.Cease And txtCeaseDate.Text = "" Then
                lblErrReqCeaseDate.Visible = True
                Exit Sub
            End If
        End If

        If lblPeriodInd.Text <> "" Then
            If lblPeriodInd.Text = objHRSetup.EnumCPPeriod.Active Then
                txtDateTo.Enabled = True
            Else
                txtDateTo.Enabled = False
            End If
        End If

        If Trim(strPayType) = "" Then
            lblErrPayType.visible = True
            Exit Sub
        End If

        If txtIncrementAmt.text = "" Then
            lblErrIncrementAmt.text = "Please enter Adjustment Amount."
            lblErrIncrementAmt.visible = True
            Exit Sub
        Else

            decCurrentRate = CDbl(Replace(Replace(strCurrentRate,".",""),",","."))
            decIncrementRate = CDbl(Replace(Replace(strIncrementAmt,".",""),",","."))
            decTotalRate = decCurrentRate + decIncrementRate

            If decTotalRate < 0 Then
                lblErrIncrementAmt.Text = "Current Amount minus adjustment amount cannot be less than 0."
                lblErrIncrementAmt.Visible = True
                Exit Sub

            Else
                strParam = ddlSalGradeCode.SelectedItem.Value
                Try
                    intErrNo = objHRSetup.mtdGetSalGrade(strOpCd_SalGradeGet, _
                                                        strParam, _
                                                        objSalGradeDs, _
                                                        True)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SALGRADE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
                if objSalGradeDs.Tables(0).Rows.count > 0 then 
                    if CDbl(objSalGradeDs.Tables(0).Rows(0).Item("MaxBasicSal")) > 0 then
                        decMaxBasicSal = CDbl(objSalGradeDs.Tables(0).Rows(0).Item("MaxBasicSal"))
                        If decCurrentRate > 0 then 
                            If CDbl(txtIncrementAmt.text) > decMaxBasicSal Then
                                lblErrIncrementAmt.Text = "Exceeding salary grade limit."
                                lblErrIncrementAmt.Visible = True
                                Exit Sub
                            End If   
                        End If
                    End If 
                else
                    exit sub    
                end if 
           End If  
        End If

        If rbNoQuota.Checked Then
            strQuotaLevel = objHRTrx.EnumQuotaLevel.NoQuota
            decIndDailyQuota = 0
        ElseIf rbActivity.Checked Then
            strQuotaLevel = objHRTrx.EnumQuotaLevel.Activity
            decIndDailyQuota = 0
        ElseIf rbBlock.Checked Then
            strQuotaLevel = objHRTrx.EnumQuotaLevel.Block
            decIndDailyQuota = 0
        Else
            strQuotaLevel = objHRTrx.EnumQuotaLevel.Individual
        End If

        If rbByHour.Checked Then
            strIndQuotaMethod = objHRTrx.EnumQuotaMethod.ByHour
        Else
            strIndQuotaMethod = objHRTrx.EnumQuotaMethod.ByVolume
        End If

        If rbAccumulate.Checked Then
            intLevyDeduction = objPRSetup.EnumPayrollSetupLevyDeduction.AccumulateMethod
        Else
            intLevyDeduction = objPRSetup.EnumPayrollSetupLevyDeduction.PushMethod
        End If


        If ddlSalary.SelectedItem.Value = "" Then
            lblErrSalary.Visible = True
            Exit Sub
        ElseIf ddlDaily.SelectedItem.Value = "" Then
            lblErrDaily.Visible = True
            Exit Sub        
        ElseIf ddlPiece.SelectedItem.Value = "" Then
            lblErrPiece.Visible = True
            Exit Sub        
        ElseIf ddlBonus.SelectedItem.Value = "" Then
            lblErrBonus.Visible = True
            Exit Sub
        ElseIf ddlTHR.SelectedItem.Value = "" Then
            lblErrTHR.Visible = True
            Exit Sub
        ElseIf ddlHouse.SelectedItem.Value = "" Then
            lblErrHouse.Visible = True
            Exit Sub        
        ElseIf ddlHardShip.SelectedItem.Value = "" Then
            lblErrHardShip.Visible = True
            Exit Sub  
        ElseIf ddlIncAward.SelectedItem.Value = "" Then
            lblErrIncAward.Visible = True
            Exit Sub      
            'ElseIf ddlTransport.SelectedItem.Value = "" Then
            '    lblErrTransport.Visible = True
            '    Exit Sub
        ElseIf ddlOvertime.SelectedItem.Value = "" Then
            lblErrOvertime.Visible = True
            Exit Sub        
        ElseIf ddlTrip.SelectedItem.Value = "" Then
            lblErrTrip.Visible = True
            Exit Sub 
        ElseIf ddlRiceRation.SelectedItem.Value = "" Then
            lblErrRice.Visible = True
            Exit Sub
            'ElseIf ddlQuotaIncCode.SelectedItem.Value = "" Then
            '    lblErrQuotaIncCode.Visible = True
            '    Exit Sub   
            'ElseIf ddlIncentive.SelectedItem.Value = "" Then
            '    lblErrIncentive.Visible = True
            '    Exit Sub
        ElseIf ddlContractPay.SelectedItem.Value = "" Then
            lblErrContractPay.Visible = True
            Exit Sub
        ElseIf ddlBIKAccom.SelectedItem.Value = "" Then
            lblErrBIKAccom.Visible = True
            Exit Sub        
        ElseIf ddlBIKVeh.SelectedItem.Value = "" Then
            lblErrBIKVeh.Visible = True
            Exit Sub
        ElseIf ddlBIKHP.SelectedItem.Value = "" Then
            lblErrBIKHP.Visible = True
            Exit Sub             
        ElseIf ddlGratuity.SelectedItem.Value = "" Then
            lblErrGratuity.Visible = True
            Exit Sub        
        ElseIf ddlRetrench.SelectedItem.Value = "" Then
            lblErrRetrench.Visible = True
            Exit Sub        
        ElseIf ddlESOS.SelectedItem.Value = "" Then
            lblErrESOS.Visible = True
            Exit Sub        
        ElseIf ddlAttAllow.SelectedItem.Value = "" Then
            lblErrAttAllow.Visible = True
            Exit Sub
        ElseIf ddlAdvSalary.SelectedItem.Value = "" Then
            lblErrAdvSalary.Visible = True
            Exit Sub       
        ElseIf ddlIN.SelectedItem.Value = "" Then
            lblErrIN.Visible = True
            Exit Sub
        ElseIf ddlCT.SelectedItem.Value = "" Then
            lblErrCT.Visible = True
            Exit Sub        
        ElseIf ddlWS.SelectedItem.Value = "" Then
            lblErrWS.Visible = True
            Exit Sub  
        ElseIf ddlWSRefund.SelectedItem.Value = "" Then
            lblErrWSRefund.Visible = True
            Exit Sub
        ElseIf ddlLoan.SelectedItem.Value = "" Then
            lblErrLoan.Visible = True
            Exit Sub 
        ElseIf ddlBFEmp.SelectedItem.Value = "" Then
            lblErrBF.Visible = True
            Exit Sub
        ElseIf ddlOutPayEmp.SelectedItem.Value = "" Then
            lblErrOutPay.Visible = True
            Exit Sub  
        ElseIf ddlAbsent.SelectedItem.Value = "" Then
            lblErrAbsent.Visible = True
            Exit Sub
        ElseIf ddlRiceDeduction.SelectedItem.Value = "" Then
            lblErrRiceDeduction.Visible = True
            Exit Sub

        ElseIf ddlSPSICOP.SelectedItem.Value = "" Then
            lblErrSPSICOP.Visible = True
            Exit Sub
        ElseIf ddlLuranCOP.SelectedItem.Value = "" Then
            lblErrLuranCOP.Visible = True
            Exit Sub
        ElseIf ddlOther.SelectedItem.Value = "" Then
            lblErrOther.Visible = True
            Exit Sub
        ElseIf ddlHold.SelectedItem.Value = "" Then
            lblErrHold.Visible = False
        ElseIf ddlPayment.SelectedItem.Value = "" Then
            lblErrPayment.Visible = False
        ElseIf ddlSubsidy.SelectedItem.Value = "" Then
            lblErrSubsidy.Visible = False
        ElseIf ddlDeficit.SelectedItem.Value = "" Then
            lblErrDeficit.Visible = False
        ElseIf ddlLevyAdj.SelectedItem.Value = "" Then
            lblErrLevyAdj.Visible = False
        ElseIf ddlHouseRent.SelectedItem.Value = "" Then
            lblErrHouseRent.Visible = True
            Exit Sub                      
        ElseIf ddlMedical.SelectedItem.Value = "" Then
            lblErrMedical.Visible = True
            Exit Sub 
        ElseIf ddlTax.SelectedItem.Value = "" Then
            lblErrTax.Visible = True
            Exit Sub 
        ElseIf ddlDanaPensiun.SelectedItem.Value = "" Then
            lblErrDanaPensiun.Visible = True
            Exit Sub 
        ElseIf ddlRapel.SelectedItem.Value = "" Then
            lblErrRapel.Visible = True
            Exit Sub 
        ElseIf ddlStaff.SelectedItem.Value = "" Then
            lblErrStaff.Visible = True
            Exit Sub 
        ElseIf ddlFunctional.SelectedItem.Value = "" Then
            lblErrFunctional.Visible = True
            Exit Sub 
        ElseIf ddlHutang.SelectedItem.Value = "" Then
            lblErrHutang.Visible = True
            Exit Sub 
        ElseIf ddlMeal.SelectedItem.Value = "" Then
            lblErrMeal.Visible = True
            Exit Sub 
        ElseIf ddlLeave.SelectedItem.Value = "" Then
            lblErrLeave.Visible = True
            Exit Sub 
        ElseIf ddlAirBus.SelectedItem.Value = "" Then
            lblErrAirBus.Visible = True
            Exit Sub 
        ElseIf ddlMaternity.SelectedItem.Value = "" Then
            lblErrMaternity.Visible = True
            Exit Sub  
        ElseIf ddlRelocation.SelectedItem.Value = "" Then
            lblErrRelocation.Visible = True
            Exit Sub 
        End If



        strParam = strEmpCode & "|" & strCPType & "|" & strCPCode & "|" & objDateFrom & "|" & objDateTo & "|" & _
                   objCeaseDate & "|" & strRemark & "|" & strCompCode & "|" & strLocCode & "|" & _
                   strDeptCode & "|" & strLevelCode & "|" & strRptTo & "|" & strSalScheme & "|" & _
                   strSalGrade & "|" & strPosCode & "|" & strProbation & "|" & strPayType & "|" & _
                   Replace(Replace(strCurrentRate,".",""),",",".") & "|" & strIncrementAmt & "|" & strQuotaLevel & "|" & _
                   decIndDailyQuota & "|" & strIndQuotaMethod & "|" & decIndQuotaIncRate & "|" & _
                   objHRTrx.EnumEmpCPStatus.Active & "|" & strEvalCode & "|" & _
                   ddlSalary.SelectedItem.Value & "|" & _
                   ddlDaily.SelectedItem.Value & "|" & _
                   ddlPiece.SelectedItem.Value & "|" & _
                   ddlBonus.SelectedItem.Value & "|" & _
                   ddlTHR.SelectedItem.Value & "|" & _ 
                   ddlHouse.SelectedItem.Value & "|" & _
                   ddlHardShip.SelectedItem.Value & "|" & _
                   ddlIncAward.SelectedItem.Value & "|" & _
                   ddlTransport.SelectedItem.Value & "|" & _
                   ddlOvertime.SelectedItem.Value & "|" & _
                   ddlTrip.SelectedItem.Value & "|" & _
                   ddlRiceRation.SelectedItem.Value & "|" & _
                   ddlQuotaIncCode.SelectedItem.Value & "|" & _
                   ddlIncentive.SelectedItem.Value & "|" & _
                   ddlContractPay.SelectedItem.Value & "|" & _
                   ddlBIKAccom.SelectedItem.Value & "|" & _
                   ddlBIKVeh.SelectedItem.Value & "|" & _
                   ddlBIKHP.SelectedItem.Value & "|" & _
                   ddlGratuity.SelectedItem.Value & "|" & _
                   ddlRetrench.SelectedItem.Value & "|" & _
                   ddlESOS.SelectedItem.Value & "|" & _
                   ddlAttAllow.SelectedItem.Value & "|" & _
                   ddlAdvSalary.SelectedItem.Value & "|" & _
                   ddlIN.SelectedItem.Value & "|" & _
                   ddlCT.SelectedItem.Value & "|" & _
                   ddlWS.SelectedItem.Value & "|" & _
                   ddlWSRefund.SelectedItem.Value & "|" & _
                   ddlLoan.SelectedItem.Value & "|" & _
                   ddlBFEmp.SelectedItem.Value & "|" & _
                   ddlOutPayEmp.SelectedItem.Value & "|" & _
                   ddlAbsent.SelectedItem.Value & "|" & _
                   ddlSPSICOP.SelectedItem.Value & "|" & _
                   ddlLuranCOP.SelectedItem.Value & "|" & _
                   ddlOther.SelectedItem.Value & "|" & _
                   ddlHold.SelectedItem.Value & "|" & _
                   ddlPayment.SelectedItem.Value & "|" & _
                   ddlSubsidy.SelectedItem.Value & "|" & _
                   ddlDeficit.SelectedItem.Value & "|" & _
                   ddlLevyAdj.SelectedItem.Value & "|" & _
                   intLevyDeduction & "|" & _
                   ddlHouseRent.SelectedItem.Value & "|" & _
                   ddlMedical.SelectedItem.Value & "|" & _                   
                   ddlMeal.SelectedItem.Value & "|" & _
                   ddlLeave.SelectedItem.Value & "|" & _
                   ddlAirBus.SelectedItem.Value & "|" & _
                   ddlMaternity.SelectedItem.Value & "|" & _
                   ddlTax.SelectedItem.Value & "|" & _
                   ddlDanaPensiun.SelectedItem.Value & "|" & _
                   ddlRelocation.SelectedItem.Value & "|" & _
                   ddlRapel.SelectedItem.Value & "|" & _
                   ddlStaff.SelectedItem.Value & "|" & _ 
                   ddlFunctional.SelectedItem.Value & "|" & _ 
                   ddlHutang.SelectedItem.Value & "|" & _
                   ddlRiceDeduction.SelectedItem.Value 


        Try
            intErrNo = objHRTrx.mtdAddEmployeeCP(strOpCd_AddCP, _
                                                 strOpCd_UpdEmp, _
                                                 strOpCd_UpdPay, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strParam, _
                                                 objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.HRCareerProgress), _
                                                 objCPID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_CPTDET_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeCPList.aspx")
        End Try

        strSelEmpCode = strEmpCode
        strSelCPID = objCPID
        onLoad_Display()
        onLoad_BindButton()
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeCPList.aspx?redirect=" & lblRedirect.Text & "&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text &"&EmpStatus=" & lblEmpStatus.text)
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.CareerProgress))
        lblCP.Text = GetCaption(objLangCap.EnumLangCap.CareerProgress)
        lblCompany.Text = GetCaption(objLangCap.EnumLangCap.Company)
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblDepartment.Text = GetCaption(objLangCap.EnumLangCap.Department)
        lblLevel.Text = GetCaption(objLangCap.EnumLangCap.Level)
        rbActivity.Text = GetCaption(objLangCap.EnumLangCap.Activity)
        If strCostLevel = "block" Then
            rbBlock.Text = GetCaption(objLangCap.EnumLangCap.Block)
        Else
            rbBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
        End If
        lblQuotaInc.Text = GetCaption(objLangCap.EnumLangCap.QuotaIncentive)

        lblVehicle.text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblErrBIKVeh.text = lblErrSelect.text & lblVehicle.text & lblBenefit.text
        lblRiceRation.text = GetCaption(objLangCap.EnumLangCap.RiceRation)
        lblIncentive.text = GetCaption(objLangCap.EnumLangCap.Incentive)
        lblQuotaIncCode.text = GetCaption(objLangCap.EnumLangCap.QuotaIncentive)

        lblErrRice.Text = lblErrSelect.text & lblRiceRation.Text & "."
        lblErrIncentive.Text = lblErrSelect.text & lblIncentive.Text & "."
        lblErrQuotaIncCode.Text = lblErrSelect.text & lblQuotaInc.Text & "."
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CPDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeCPDet.aspx")
        End Try

    End Sub


    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    
    Sub BindEvaluation(ByVal pv_strEvalCode As String)
        Dim strOpCd_Get As String = "HR_CLSSETUP_EVALUATION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow

        strParam = " AND EVAL.Status = '" & CInt(objHRSetup.EnumEvaluationStatus.Active) & "' | Order By EVAL.EvalCode "

        Try
            intErrNo = objHRSetup.mtdGetEvaluationList(strOpCd_Get, strParam, objEvalDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSSETUP_EVALUATION_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objEvalDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEvalDs.Tables(0).Rows.Count - 1
                If Trim(objEvalDs.Tables(0).Rows(intCnt).Item("EvalCode")) = Trim(pv_strEvalCode) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objEvalDs.Tables(0).NewRow()
        dr("EvalCode") = ""
        dr("_Description") = "Select Evaluation"
        objEvalDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEvalCode.DataSource = objEvalDs.Tables(0)
        ddlEvalCode.DataTextField = "_Description"
        ddlEvalCode.DataValueField = "EvalCode"
        ddlEvalCode.DataBind()
        ddlEvalCode.SelectedIndex = intSelectedIndex
    End Sub

End Class

