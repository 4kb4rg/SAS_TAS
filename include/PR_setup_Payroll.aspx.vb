Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.PR
Imports agri.GlobalHdl
Imports agri.PWSystem.clsLangCap

Public Class PR_setup_Payroll : Inherits Page

    Protected WithEvents ddlSalary As DropDownList
    Protected WithEvents ddlDaily As DropDownList
    Protected WithEvents ddlPiece As DropDownList
    Protected WithEvents ddlBonus As DropDownList
    Protected WithEvents ddlHouse As DropDownList
    Protected WithEvents ddlHardShip As DropDownList
    Protected WithEvents ddlIncAward As DropDownList
    Protected WithEvents ddlTransport As DropDownList
    Protected WithEvents ddlLoan As DropDownList
    Protected WithEvents ddlOvertime As DropDownList
    Protected WithEvents ddlIN As DropDownList
    Protected WithEvents ddlCT As DropDownList
    Protected WithEvents ddlWS As DropDownList
    Protected WithEvents ddlWSRefund As DropDownList
    
    Protected WithEvents ddlTHR As DropDownList
    Protected WithEvents ddlAdvSalary As DropDownList
    Protected WithEvents ddlBFEmp As DropDownList
    Protected WithEvents ddlOutPayEmp As DropDownList
    Protected WithEvents ddlAbsent As DropDownList
    Protected WithEvents ddlRiceDeduction As DropDownList
    Protected WithEvents ddlHold As DropDownList
    Protected WithEvents ddlPayment As DropDownList
    Protected WithEvents ddlSubsidy As DropDownList
    Protected WithEvents ddlLevyAdj As DropDownList
    Protected WithEvents ddlDeficit As DropDownList
    Protected WithEvents ddlTrip As DropDownList
    Protected WithEvents ddlRiceRation As DropDownList
    Protected WithEvents ddlIncentive As DropDownList
    Protected WithEvents ddlQuotaInc As DropDownList
    Protected WithEvents txtMaxDeduct As TextBox
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
    Protected WithEvents ddlHarvestInc As DropDownList
    Protected WithEvents lblErrHarvestInc As Label
    Protected WithEvents ddlCashAccount As DropDownList
    Protected WithEvents lblErrCashAccount As Label
    Protected WithEvents ddlBankCode As DropDownList
    Protected WithEvents lblErrBankCode As Label

    Protected WithEvents txtDailyHour As Textbox
    Protected WithEvents txtWorkDay As Textbox
    Protected WithEvents txtOffPayRate As Textbox
    Protected WithEvents txtHolidayPayRate As Textbox
    Protected WithEvents txtOffMonthRate As Textbox
    Protected WithEvents txtHolidayMonthRate As Textbox
    Protected WithEvents txtOTRate As TextBox

    Protected WithEvents rbAccumulate As RadioButton
    Protected WithEvents rbPush As RadioButton
    Protected WithEvents cbHarvAutoWeight As CheckBox
    Protected WithEvents cbAutoWeightGroup As CheckBox
    Protected WithEvents cbAutoWeightDaily As CheckBox

    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrSalary As Label
    Protected WithEvents lblErrDaily As Label
    Protected WithEvents lblErrPiece As Label
    Protected WithEvents lblErrBonus As Label
    Protected WithEvents lblErrHouse As Label
    Protected WithEvents lblErrHardShip As Label
    Protected WithEvents lblErrIncAward As Label
    Protected WithEvents lblErrTransport As Label
    Protected WithEvents lblErrLoan As Label
    Protected WithEvents lblErrOvertime As Label
    Protected WithEvents lblErrIN As Label
    Protected WithEvents lblErrCT As Label
    Protected WithEvents lblErrWS As Label
    Protected WithEvents lblErrWSRefund As Label
    Protected WithEvents lblErrTHR As Label
    Protected WithEvents lblErrAdvSalary As Label
    Protected WithEvents lblErrBF As Label
    Protected WithEvents lblErrOutPay As Label
    Protected WithEvents lblErrHold As Label
    Protected WithEvents lblErrPayment As Label
    Protected WithEvents lblErrSubsidy As Label
    Protected WithEvents lblErrLevyAdj As Label
    Protected WithEvents lblErrDeficit As Label
    Protected WithEvents lblErrTrip As Label
    Protected WithEvents lblErrContractPay As Label
    Protected WithEvents lblErrBIKAccom As Label
    Protected WithEvents lblErrBIKVeh As Label
    Protected WithEvents lblErrBIKHP As Label
    Protected WithEvents lblErrGratuity As Label
    Protected WithEvents lblErrRetrench As Label
    Protected WithEvents lblErrESOS As Label
    Protected WithEvents lblErrAttAllow As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblBenefit As Label
    Protected WithEvents lblRiceRation As Label
    Protected WithEvents lblIncentive As Label
    Protected WithEvents lblQuotaInc As Label
    Protected WithEvents lblErrRice As Label
    Protected WithEvents lblErrIncentive As Label
    Protected WithEvents lblErrQuotaInc As Label
    Protected WithEvents lblErrAbsent As Label
    Protected WithEvents lblErrRiceDeduction As Label

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

    Protected WithEvents ddlSPSICOP As DropDownList
    Protected WithEvents ddlLuranCOP As DropDownList
    Protected WithEvents ddlOther As DropDownList

    Protected WithEvents lblErrSPSICOP As Label
    Protected WithEvents lblErrLuranCOP As Label
    Protected WithEvents lblErrOther As Label
    Protected WithEvents ddlInvCatuBeras As DropDownList
    Protected WithEvents txtBasicPayDay As Textbox
    Protected WithEvents txtProratePayDay As Textbox
    Protected WithEvents txtAdvPytSKUHarian As Textbox
    Protected WithEvents txtWorkDaySKUHarian As Textbox
    Protected WithEvents txtAdvPytSKUBulanan As Textbox
    Protected WithEvents txtWorkDaySKUBulanan As Textbox    
    Protected WithEvents lblErrInvCatuBeras As Label    

    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objIN As New agri.IN.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objPaySetupDs As New Object()
    Dim objADDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaysetup), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrSalary.Visible = False
            lblErrOvertime.Visible = False
            lblErrHold.Visible = False
            lblErrPayment.Visible = False
            lblErrSubsidy.Visible = False
            lblErrLevyAdj.Visible = False
            lblErrDeficit.Visible = False
            lblErrTrip.Visible = False
            lblErrIN.Visible = False
            lblErrCT.Visible = False
            lblErrWS.Visible = False
            lblErrWSRefund.Visible = False
            lblErrTHR.Visible = False
            lblErrInvCatuBeras.Visible = False
            lblErrBF.Visible = False
            lblErrOutPay.Visible = False
            lblErrDaily.Visible = False
            lblErrPiece.Visible = False
            lblErrBonus.Visible = False
            lblErrHouse.Visible = False
            lblErrHardShip.Visible = False
            lblErrIncAward.Visible = False
            lblErrTransport.Visible = False
            lblErrLoan.Visible = False
            lblErrBIKAccom.Visible = False
            lblErrBIKVeh.Visible = False
            lblErrBIKHP.Visible = False
            lblErrGratuity.Visible = False
            lblErrRetrench.Visible = False
            lblErrESOS.Visible = False
            lblErrAttAllow.Visible = False
            lblErrRice.Visible = False
            lblErrIncentive.Visible = False
            lblErrQuotaInc.Visible = False
            lblErrSPSICOP.Visible = False
            lblErrLuranCOP.Visible = False
            lblErrOther.Visible = False
            lblErrRiceDeduction.Visible = False

            lblErrHouseRent.Visible= False
            lblErrMedical.Visible= False
            lblErrDanaPensiun.Visible= False
            lblErrRapel.Visible= False
            lblErrStaff.Visible= False
            lblErrFunctional.Visible= False
            lblErrHutang.Visible= False

            lblErrMeal.Visible = False
            lblErrLeave.Visible = False
            lblErrAirBus.Visible = False
            lblErrMaternity.Visible = False
            lblErrRelocation.Visible = False
            lblErrHarvestInc.Visible = False
            
            lblErrCashAccount.Visible = False
            lblErrBankCode.Visible = False

            If Not IsPostBack Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblVehicle.text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblErrBIKVeh.text = lblErrSelect.text & lblVehicle.text & lblBenefit.text
        lblRiceRation.text = GetCaption(objLangCap.EnumLangCap.RiceRation)
        lblIncentive.text = GetCaption(objLangCap.EnumLangCap.Incentive)
        lblQuotaInc.text = GetCaption(objLangCap.EnumLangCap.QuotaIncentive)

        lblErrRice.Text = lblErrSelect.text & lblRiceRation.Text & "."
        lblErrIncentive.Text = lblErrSelect.text & lblIncentive.Text & "."
        lblErrQuotaInc.Text = lblErrSelect.text & lblQuotaInc.Text & "."
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_setup_Payroll.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function



    Sub onCheck_HarvAutoWeight(ByVal Sender As Object, ByVal E As EventArgs)
        If cbHarvAutoWeight.Checked Then
            cbAutoWeightGroup.Enabled = True
            cbAutoWeightDaily.Enabled = True
        Else
            cbAutoWeightGroup.Enabled = False
            cbAutoWeightDaily.Enabled = False
            cbAutoWeightGroup.Checked = False
            cbAutoWeightDaily.Checked = False
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_PAYROLLPROCESS_GET"
        Dim intErrNo As Integer
        Dim intIndex As Integer
        Dim strParam As String
        Dim objAllowanceDs As New Object()
        Dim objDeductionDs As New Object()
        Dim objMemoItemDs As New Object()
        Dim strType_Allowance As String 
        Dim strType_Deduction As String
        Dim strType_EAItem As String
        Dim strType_MemoItem As String

        strType_Allowance = objPRSetup.EnumADType.Allowance
        strType_Deduction = objPRSetup.EnumADType.Deduction
        strType_EAItem = objPRSetup.EnumADType.EAItem
        strType_MemoItem = objPRSetup.EnumADType.MemoItem

        Try
            strParam = "|"
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   0, _
                                                   objPaySetupDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_PAYROLLPROCESS_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objPaySetupDs.Tables(0).Rows.Count > 0 Then
            If CInt(objPaySetupDs.Tables(0).Rows(0).Item("LevyDeductInd")) = objPRSetup.EnumPayrollSetupLevyDeduction.AccumulateMethod Then
                rbAccumulate.Checked = True
                rbPush.Checked = False
            Else
                rbAccumulate.Checked = False
                rbPush.Checked = True
            End If
     
            If objPaySetupDs.Tables(0).Rows(0).Item("HarvAutoWeight") = objPRSetup.EnumHarvAutoWeight.Yes Then
                cbHarvAutoWeight.Checked = True
                cbAutoWeightGroup.Enabled = True
                cbAutoWeightDaily.Enabled = True
                If objPaySetupDs.Tables(0).Rows(0).Item("HarvGroupWeight") = objPRSetup.EnumHarvAutoWeightGroup.Yes Then
                    cbAutoWeightGroup.Checked = True
                End If
                If objPaySetupDs.Tables(0).Rows(0).Item("HarvDailyWeight") = objPRSetup.EnumHarvAutoWeightDaily.Yes Then
                    cbAutoWeightDaily.Checked = True
                End If                   
            Else
                cbHarvAutoWeight.Checked = False
                cbAutoWeightGroup.Enabled = False
                cbAutoWeightDaily.Enabled = False
            End If
            
            txtDailyHour.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("DailyHour"),0)
            txtWorkDay.Text = objPaySetupDs.Tables(0).Rows(0).Item("WorkDay")
            txtOffPayRate.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("OffPayRate"), 2, True, False, False)
            txtHolidayPayRate.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("HolidayPayRate"), 2, True, False, False)
            txtOffMonthRate.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("MthOffPayRate"), 2, True, False, False)
            txtHolidayMonthRate.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("MthHolPayRate"), 2, True, False, False)
            txtOTRate.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("OTRate"), 2, True, False, False)
            txtMaxDeduct.Text = objPaySetupDs.Tables(0).Rows(0).Item("MaxDeduct")
            lblLastUpdate.Text = objGlobal.GetLongDate(objPaySetupDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objPaySetupDs.Tables(0).Rows(0).Item("UserName").Trim()

            txtBasicPayDay.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("BasicPayDay"),0)
            txtProratePayDay.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("ProratePayDay"),0)
            txtAdvPytSKUHarian.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("AdvPytSKUHarian"),0,True,False, False)
            txtAdvPytSKUBulanan.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("AdvPytSKUBulanan"),0,True,False,False)
            txtWorkDaySKUHarian.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("WorkDaySKUHarian"),0)
            txtWorkDaySKUBulanan.Text = FormatNumber(objPaySetupDs.Tables(0).Rows(0).Item("WorkDaySKUBulanan"),0)
            BindCatuBeras(Trim(objPaySetupDs.Tables(0).Rows(0).Item("CatuBerasInvCode")))   
            BindHarvestIncAcc(Trim(objPaySetupDs.Tables(0).Rows(0).Item("HarvestIncAcc")))         

            BindBankCode(Trim(objPaySetupDs.Tables(0).Rows(0).Item("BankCode")))
            BindCashAccCode(Trim(objPaySetupDs.Tables(0).Rows(0).Item("CashAcc")))

            ddlSalary.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("SalaryADCode").Trim(), strType_Allowance, intIndex, "")
            ddlSalary.DataValueField = "ADCode"
            ddlSalary.DataTextField = "_Description"
            ddlSalary.DataBind()
            ddlSalary.SelectedIndex = intIndex

            ddlDaily.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("DailyRateADCode").Trim(), strType_Allowance, intIndex, "")
            ddlDaily.DataValueField = "ADCode"
            ddlDaily.DataTextField = "_Description"
            ddlDaily.DataBind()
            ddlDaily.SelectedIndex = intIndex

            ddlPiece.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("PieceRateADCode").Trim(), strType_Allowance, intIndex, "")
            ddlPiece.DataValueField = "ADCode"
            ddlPiece.DataTextField = "_Description"
            ddlPiece.DataBind()
            ddlPiece.SelectedIndex = intIndex

            ddlBonus.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("BonusADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBonus.DataValueField = "ADCode"
            ddlBonus.DataTextField = "_Description"
            ddlBonus.DataBind()
            ddlBonus.SelectedIndex = intIndex

            ddlTHR.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("THRADCode").Trim(), strType_Allowance, intIndex, "THR")
            ddlTHR.DataValueField = "ADCode"
            ddlTHR.DataTextField = "_Description"
            ddlTHR.DataBind()
            ddlTHR.SelectedIndex = intIndex

            ddlHouse.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("HouseADCode").Trim(), strType_Allowance, intIndex, "")
            ddlHouse.DataValueField = "ADCode"
            ddlHouse.DataTextField = "_Description"
            ddlHouse.DataBind()
            ddlHouse.SelectedIndex = intIndex

            ddlHardShip.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("HardshipADCode").Trim(), strType_Allowance, intIndex, "")
            ddlHardShip.DataValueField = "ADCode"
            ddlHardShip.DataTextField = "_Description"
            ddlHardShip.DataBind()
            ddlHardShip.SelectedIndex = intIndex

            ddlIncAward.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("IncAwardADCode").Trim(), strType_Allowance, intIndex, "")
            ddlIncAward.DataValueField = "ADCode"
            ddlIncAward.DataTextField = "_Description"
            ddlIncAward.DataBind()
            ddlIncAward.SelectedIndex = intIndex

            ddlTransport.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("TransADCode").Trim(), strType_Allowance, intIndex, "Tran")
            ddlTransport.DataValueField = "ADCode"
            ddlTransport.DataTextField = "_Description"
            ddlTransport.DataBind()
            ddlTransport.SelectedIndex = intIndex
            
            ddlOvertime.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("OTADCode").Trim(), strType_Allowance, intIndex, "")
            ddlOvertime.DataValueField = "ADCode"
            ddlOvertime.DataTextField = "_Description"
            ddlOvertime.DataBind()
            ddlOvertime.SelectedIndex = intIndex

            ddlTrip.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("TripADCode").Trim(), strType_Allowance, intIndex, "")
            ddlTrip.DataValueField = "ADCode"
            ddlTrip.DataTextField = "_Description"
            ddlTrip.DataBind()
            ddlTrip.SelectedIndex = intIndex

            ddlRiceRation.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("RiceADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRiceRation.DataValueField = "ADCode"
            ddlRiceRation.DataTextField = "_Description"
            ddlRiceRation.DataBind()
            ddlRiceRation.SelectedIndex = intIndex

            ddlIncentive.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("IncADCode").Trim(), strType_Allowance, intIndex, "")
            ddlIncentive.DataValueField = "ADCode"
            ddlIncentive.DataTextField = "_Description"
            ddlIncentive.DataBind()
            ddlIncentive.SelectedIndex = intIndex

            ddlQuotaInc.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("QuotaIncADCode").Trim(), strType_Allowance, intIndex, "")
            ddlQuotaInc.DataValueField = "ADCode"
            ddlQuotaInc.DataTextField = "_Description"
            ddlQuotaInc.DataBind()
            ddlQuotaInc.SelectedIndex = intIndex

            ddlBIKAccom.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("BIKAccomADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKAccom.DataValueField = "ADCode"
            ddlBIKAccom.DataTextField = "_Description"
            ddlBIKAccom.DataBind()
            ddlBIKAccom.SelectedIndex = intIndex

            ddlBIKVeh.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("BIKVehADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKVeh.DataValueField = "ADCode"
            ddlBIKVeh.DataTextField = "_Description"
            ddlBIKVeh.DataBind()
            ddlBIKVeh.SelectedIndex = intIndex

            ddlBIKHP.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("BIKHPADCode").Trim(), strType_Allowance, intIndex, "")
            ddlBIKHP.DataValueField = "ADCode"
            ddlBIKHP.DataTextField = "_Description"
            ddlBIKHP.DataBind()
            ddlBIKHP.SelectedIndex = intIndex

            ddlGratuity.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("GratuityADCode").Trim(), strType_Allowance, intIndex, "")
            ddlGratuity.DataValueField = "ADCode"
            ddlGratuity.DataTextField = "_Description"
            ddlGratuity.DataBind()
            ddlGratuity.SelectedIndex = intIndex

            ddlRetrench.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("RetrenchCompADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRetrench.DataValueField = "ADCode"
            ddlRetrench.DataTextField = "_Description"
            ddlRetrench.DataBind()
            ddlRetrench.SelectedIndex = intIndex

            ddlESOS.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("ESOSADCode").Trim(), strType_Allowance, intIndex, "")
            ddlESOS.DataValueField = "ADCode"
            ddlESOS.DataTextField = "_Description"
            ddlESOS.DataBind()
            ddlESOS.SelectedIndex = intIndex

            ddlAttAllow.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("AttAllowanceADCode").Trim(), strType_Allowance, intIndex, "")
            ddlAttAllow.DataValueField = "ADCode"
            ddlAttAllow.DataTextField = "_Description"
            ddlAttAllow.DataBind()
            ddlAttAllow.SelectedIndex = intIndex

            ddlContractPay.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("ContractPayADCode").Trim(), strType_Allowance, intIndex, "")
            ddlContractPay.DataValueField = "ADCode"
            ddlContractPay.DataTextField = "_Description"
            ddlContractPay.DataBind()
            ddlContractPay.SelectedIndex = intIndex

            ddlLoan.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("LoanADCode").Trim(), strType_Deduction, intIndex, "")
            ddlLoan.DataValueField = "ADCode"
            ddlLoan.DataTextField = "_Description"
            ddlLoan.DataBind()
            ddlLoan.SelectedIndex = intIndex

            ddlIN.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("INADCode").Trim(), strType_Deduction, intIndex, "")
            ddlIN.DataValueField = "ADCode"
            ddlIN.DataTextField = "_Description"
            ddlIN.DataBind()
            ddlIN.SelectedIndex = intIndex

            ddlCT.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("CTADCode").Trim(), strType_Deduction, intIndex, "")
            ddlCT.DataValueField = "ADCode"
            ddlCT.DataTextField = "_Description"
            ddlCT.DataBind()
            ddlCT.SelectedIndex = intIndex

            ddlWS.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("WSADCode").Trim(), strType_Deduction, intIndex, "")
            ddlWS.DataValueField = "ADCode"
            ddlWS.DataTextField = "_Description"
            ddlWS.DataBind()
            ddlWS.SelectedIndex = intIndex

            ddlWSRefund.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("WSRefundADCode").Trim(), strType_Deduction, intIndex, "")
            ddlWSRefund.DataValueField = "ADCode"
            ddlWSRefund.DataTextField = "_Description"
            ddlWSRefund.DataBind()
            ddlWSRefund.SelectedIndex = intIndex

            ddlBFEmp.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("BFEmpADCode").Trim(), strType_Deduction, intIndex, "")
            ddlBFEmp.DataValueField = "ADCode"
            ddlBFEmp.DataTextField = "_Description"
            ddlBFEmp.DataBind()
            ddlBFEmp.SelectedIndex = intIndex

            ddlOutPayEmp.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("OutPayEmpADCode").Trim(), strType_Deduction, intIndex, "")
            ddlOutPayEmp.DataValueField = "ADCode"
            ddlOutPayEmp.DataTextField = "_Description"
            ddlOutPayEmp.DataBind()
            ddlOutPayEmp.SelectedIndex = intIndex
    
            ddlAbsent.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("AbsentADCode").Trim(), strType_Deduction, intIndex, "")
            ddlAbsent.DataValueField = "ADCode"
            ddlAbsent.DataTextField = "_Description"
            ddlAbsent.DataBind()
            ddlAbsent.SelectedIndex = intIndex   

            ddlRiceDeduction.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("RiceDeductionADCode").Trim(), strType_Deduction, intIndex, "")
            ddlRiceDeduction.DataValueField = "ADCode"
            ddlRiceDeduction.DataTextField = "_Description"
            ddlRiceDeduction.DataBind()
            ddlRiceDeduction.SelectedIndex = intIndex   

            ddlSPSICOP.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("SPSICOPADCode").Trim(), strType_Deduction, intIndex, "")
            ddlSPSICOP.DataValueField = "ADCode"
            ddlSPSICOP.DataTextField = "_Description"
            ddlSPSICOP.DataBind()
            ddlSPSICOP.SelectedIndex = intIndex 

            ddlLuranCOP.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("LuranCOPADCode").Trim(), strType_Deduction, intIndex, "")
            ddlLuranCOP.DataValueField = "ADCode"
            ddlLuranCOP.DataTextField = "_Description"
            ddlLuranCOP.DataBind()
            ddlLuranCOP.SelectedIndex = intIndex 

            ddlOther.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("OtherADCode").Trim(), strType_Deduction, intIndex, "")
            ddlOther.DataValueField = "ADCode"
            ddlOther.DataTextField = "_Description"
            ddlOther.DataBind()
            ddlOther.SelectedIndex = intIndex 

            
            ddlHold.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("LevyHoldADCode").Trim(), strType_Deduction, intIndex, "")
            ddlHold.DataValueField = "ADCode"
            ddlHold.DataTextField = "_Description"
            ddlHold.DataBind()
            ddlHold.SelectedIndex = intIndex
            ddlHold.Enabled = False
    
            ddlDeficit.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("LevyDeficitADCode").Trim(), strType_Deduction, intIndex, "")
            ddlDeficit.DataValueField = "ADCode"
            ddlDeficit.DataTextField = "_Description"
            ddlDeficit.DataBind()
            ddlDeficit.SelectedIndex = intIndex
            ddlDeficit.Enabled = False

            ddlAdvSalary.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("AdvSalaryCode").Trim(), strType_Deduction, intIndex, "")
            ddlAdvSalary.DataValueField = "ADCode"
            ddlAdvSalary.DataTextField = "_Description"
            ddlAdvSalary.DataBind()
            ddlAdvSalary.SelectedIndex = intIndex
            ddlAdvSalary.Enabled = False

            ddlPayment.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("LevyPayADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlPayment.DataValueField = "ADCode"
            ddlPayment.DataTextField = "_Description"
            ddlPayment.DataBind()
            ddlPayment.SelectedIndex = intIndex
            ddlPayment.Enabled = False

            ddlSubsidy.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("LevySubsiADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlSubsidy.DataValueField = "ADCode"
            ddlSubsidy.DataTextField = "_Description"
            ddlSubsidy.DataBind()
            ddlSubsidy.SelectedIndex = intIndex
            ddlSubsidy.Enabled = False

            ddlLevyAdj.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("LevyAdjustADCode").Trim(), strType_MemoItem, intIndex, "")
            ddlLevyAdj.DataValueField = "ADCode"
            ddlLevyAdj.DataTextField = "_Description"
            ddlLevyAdj.DataBind()
            ddlLevyAdj.SelectedIndex = intIndex
            ddlLevyAdj.Enabled = False

            rbAccumulate.Enabled = False
            rbPush.Enabled = False

            ddlHouseRent.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("HouseRentADCode").Trim(), strType_Allowance, intIndex, "House")
            ddlHouseRent.DataValueField = "ADCode"
            ddlHouseRent.DataTextField = "_Description"
            ddlHouseRent.DataBind()
            ddlHouseRent.SelectedIndex = intIndex

            ddlMedical.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("MedicalADCode").Trim(), strType_Allowance, intIndex, "Med")
            ddlMedical.DataValueField = "ADCode"
            ddlMedical.DataTextField = "_Description"
            ddlMedical.DataBind()
            ddlMedical.SelectedIndex = intIndex

            ddlTax.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("TaxADCode").Trim(), strType_Allowance, intIndex, "Tax")
            ddlTax.DataValueField = "ADCode"
            ddlTax.DataTextField = "_Description"
            ddlTax.DataBind()
            ddlTax.SelectedIndex = intIndex

            ddlDanaPensiun.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("DanaPensiunADCode").Trim(), strType_Allowance, intIndex, "Pensiun")
            ddlDanaPensiun.DataValueField = "ADCode"
            ddlDanaPensiun.DataTextField = "_Description"
            ddlDanaPensiun.DataBind()
            ddlDanaPensiun.SelectedIndex = intIndex

            ddlRapel.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("RapelADCode").Trim(), strType_Allowance, intIndex, "")
            ddlRapel.DataValueField = "ADCode"
            ddlRapel.DataTextField = "_Description"
            ddlRapel.DataBind()
            ddlRapel.SelectedIndex = intIndex

            ddlStaff.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("StaffADCode").Trim(), strType_Allowance, intIndex, "")
            ddlStaff.DataValueField = "ADCode"
            ddlStaff.DataTextField = "_Description"
            ddlStaff.DataBind()
            ddlStaff.SelectedIndex = intIndex

            ddlFunctional.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("FunctADCode").Trim(), strType_Allowance, intIndex, "")
            ddlFunctional.DataValueField = "ADCode"
            ddlFunctional.DataTextField = "_Description"
            ddlFunctional.DataBind()
            ddlFunctional.SelectedIndex = intIndex

            ddlHutang.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("HutangADCode").Trim(), strType_Deduction, intIndex, "")
            ddlHutang.DataValueField = "ADCode"
            ddlHutang.DataTextField = "_Description"
            ddlHutang.DataBind()
            ddlHutang.SelectedIndex = intIndex

            ddlMeal.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("MealADCode").Trim(), strType_Allowance, intIndex, "Meal")
            ddlMeal.DataValueField = "ADCode"
            ddlMeal.DataTextField = "_Description"
            ddlMeal.DataBind()
            ddlMeal.SelectedIndex = intIndex

            ddlLeave.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("LeaveADCode").Trim(), strType_Allowance, intIndex, "Leave")
            ddlLeave.DataValueField = "ADCode"
            ddlLeave.DataTextField = "_Description"
            ddlLeave.DataBind()
            ddlLeave.SelectedIndex = intIndex

            ddlAirBus.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("AirBusADCode").Trim(), strType_Allowance, intIndex, "AirBus")
            ddlAirBus.DataValueField = "ADCode"
            ddlAirBus.DataTextField = "_Description"
            ddlAirBus.DataBind()
            ddlAirBus.SelectedIndex = intIndex

            ddlMaternity.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("MaternityADCode").Trim(), strType_Allowance, intIndex, "Mat")
            ddlMaternity.DataValueField = "ADCode"
            ddlMaternity.DataTextField = "_Description"
            ddlMaternity.DataBind()
            ddlMaternity.SelectedIndex = intIndex

            ddlRelocation.DataSource = BindAD(objPaySetupDs.Tables(0).Rows(0).Item("RelocationADCode").Trim(), strType_Allowance, intIndex, "Relocation")
            ddlRelocation.DataValueField = "ADCode"
            ddlRelocation.DataTextField = "_Description"
            ddlRelocation.DataBind()
            ddlRelocation.SelectedIndex = intIndex

        Else
            BindCatuBeras("")
            BindHarvestIncAcc("") 
            
            BindBankCode("")
            BindCashAccCode("")

            objAllowanceDs = BindAD("", strType_Allowance, intIndex, "")
            
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

            ddlIncentive.DataSource = objAllowanceDs
            ddlIncentive.DataValueField = "ADCode"
            ddlIncentive.DataTextField = "_Description"
            ddlIncentive.DataBind()
            ddlIncentive.SelectedIndex = intIndex

            ddlQuotaInc.DataSource = objAllowanceDs
            ddlQuotaInc.DataValueField = "ADCode"
            ddlQuotaInc.DataTextField = "_Description"
            ddlQuotaInc.DataBind()
            ddlQuotaInc.SelectedIndex = intIndex

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

            ddlContractPay.DataSource = objAllowanceDs
            ddlContractPay.DataValueField = "ADCode"
            ddlContractPay.DataTextField = "_Description"
            ddlContractPay.DataBind()
            ddlContractPay.SelectedIndex = intIndex

            objDeductionDs = BindAD("", strType_Deduction, intIndex,"")

            ddlLoan.DataSource = objDeductionDs
            ddlLoan.DataValueField = "ADCode"
            ddlLoan.DataTextField = "_Description"
            ddlLoan.DataBind()
            ddlLoan.SelectedIndex = intIndex

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

            ddlHold.DataSource = objDeductionDs
            ddlHold.DataValueField = "ADCode"
            ddlHold.DataTextField = "_Description"
            ddlHold.DataBind()
            ddlHold.SelectedIndex = intIndex

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

            ddlDeficit.DataSource = objDeductionDs
            ddlDeficit.DataValueField = "ADCode"
            ddlDeficit.DataTextField = "_Description"
            ddlDeficit.DataBind()
            ddlDeficit.SelectedIndex = intIndex


            ddlAdvSalary.DataSource = objDeductionDs
            ddlAdvSalary.DataValueField = "ADCode"
            ddlAdvSalary.DataTextField = "_Description"
            ddlAdvSalary.DataBind()
            ddlAdvSalary.SelectedIndex = intIndex

            objMemoItemDs = BindAD("", strType_MemoItem, intIndex,"")
           
            ddlPayment.DataSource = objMemoItemDs
            ddlPayment.DataValueField = "ADCode"
            ddlPayment.DataTextField = "_Description"
            ddlPayment.DataBind()
            ddlPayment.SelectedIndex = intIndex

            ddlLevyAdj.DataSource = objMemoItemDs
            ddlLevyAdj.DataValueField = "ADCode"
            ddlLevyAdj.DataTextField = "_Description"
            ddlLevyAdj.DataBind()
            ddlLevyAdj.SelectedIndex = intIndex

            ddlSubsidy.DataSource = objMemoItemDs
            ddlSubsidy.DataValueField = "ADCode"
            ddlSubsidy.DataTextField = "_Description"
            ddlSubsidy.DataBind()
            ddlSubsidy.SelectedIndex = intIndex

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

            ddlHutang.DataSource = objAllowanceDs
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

            cbHarvAutoWeight.Checked = False
            cbAutoWeightGroup.Enabled = False
            cbAutoWeightDaily.Enabled = False

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

        If Not objPaySetupDs Is Nothing Then
            objPaySetupDs = Nothing
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

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_PAYROLLPROCESS_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_PAYROLLPROCESS_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_PAYROLLPROCESS_GET"
        Dim strOpCd As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim intLevyDeduction As Integer
        Dim strParam As String = ""
        Dim strHarvAutoWeight As String
        Dim strHarvGroupWeight As String
        Dim strHarvDailyWeight As String

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
        ElseIf ddlTransport.SelectedItem.Value = "" Then
            lblErrTransport.Visible = True
            Exit Sub             
        ElseIf ddlOvertime.SelectedItem.Value = "" Then
            lblErrOvertime.Visible = True
            Exit Sub 
        ElseIf ddlTrip.SelectedItem.Value = "" Then
            lblErrTrip.Visible = True
            Exit Sub
        ElseIf ddlRiceRation.SelectedItem.Value = "" Then
            lblErrRice.Visible = True
            Exit Sub
        ElseIf ddlIncentive.SelectedItem.Value = "" Then
            lblErrIncentive.Visible = True
            Exit Sub
        ElseIf ddlQuotaInc.SelectedItem.Value = "" Then
            lblErrQuotaInc.Visible = True
            Exit Sub
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
        ElseIf ddlAdvSalary.SelectedItem.Value = "" Then
            lblErrAdvSalary.Visible = True
            Exit Sub        
        ElseIf ddlInvCatuBeras.SelectedItem.Value = "" Then
            lblErrInvCatuBeras.Visible = True
            Exit Sub
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
        ElseIf ddlHarvestInc.SelectedItem.Value = "" Then
            lblErrHarvestInc.Visible = True
            Exit Sub
       ElseIf ddlCashAccount.SelectedItem.Value = "" Then
            lblErrCashAccount.Visible = True
            Exit Sub
       ElseIf ddlBankCode.SelectedItem.Value = "" Then
            lblErrBankCode.Visible = True
            Exit Sub
        End If


        If strCmdArgs = "Save" Then
            strParam = "|"
            Try
                intErrNo = objPRSetup.mtdGetMasterList(strOpCd_Get, _
                                                       strParam, _
                                                       0, _
                                                       objPaySetupDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_PAYROLLPROCESS_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objPaySetupDs.Tables(0).Rows.Count > 0 Then
                strOpCd = strOpCd_Upd
            Else
                strOpCd = strOpCd_Add
            End If

            If rbAccumulate.Checked Then
                intLevyDeduction = objPRSetup.EnumPayrollSetupLevyDeduction.AccumulateMethod
            Else
                intLevyDeduction = objPRSetup.EnumPayrollSetupLevyDeduction.PushMethod
            End If

            If cbHarvAutoWeight.Checked Then
                strHarvAutoWeight = objPRSetup.EnumHarvAutoWeight.Yes
            Else
                strHarvAutoWeight = objPRSetup.EnumHarvAutoWeight.No
            End If
        
            If cbAutoWeightGroup.Checked Then
                strHarvGroupWeight = objPRSetup.EnumHarvAutoWeightGroup.Yes
            Else
                strHarvGroupWeight = objPRSetup.EnumHarvAutoWeightGroup.No
            End If
    
            If cbAutoWeightDaily.Checked Then
                strHarvDailyWeight = objPRSetup.EnumHarvAutoWeightDaily.Yes
            Else
                strHarvDailyWeight = objPRSetup.EnumHarvAutoWeightDaily.No
            End If

           
 


 
            strParam = ddlSalary.SelectedItem.Value & "|" & _
                       ddlDaily.SelectedItem.Value & "|" & _
                       ddlPiece.SelectedItem.Value & "|" & _
                       ddlBonus.SelectedItem.Value & "|" & _
                       ddlHouse.SelectedItem.Value & "|" & _
                       ddlHardShip.SelectedItem.Value & "|" & _
                       ddlIncAward.SelectedItem.Value & "|" & _
                       ddlTransport.SelectedItem.Value & "|" & _
                       ddlOvertime.SelectedItem.Value & "|" & _
                       ddlTrip.SelectedItem.Value & "|" & _                       
                       ddlRiceRation.SelectedItem.Value & "|" & _
                       ddlIncentive.SelectedItem.Value & "|" & _
                       ddlQuotaInc.SelectedItem.Value & "|" & _
                       ddlContractPay.SelectedItem.Value & "|" & _
                       ddlBIKAccom.SelectedItem.Value & "|" & _
                       ddlBIKVeh.SelectedItem.Value & "|" & _
                       ddlBIKHP.SelectedItem.Value & "|" & _
                       ddlGratuity.SelectedItem.Value & "|" & _
                       ddlRetrench.SelectedItem.Value & "|" & _
                       ddlESOS.SelectedItem.Value & "|" & _                       
                       ddlAttAllow.SelectedItem.Value & "|" & _
                       ddlIN.SelectedItem.Value & "|" & _
                       ddlCT.SelectedItem.Value & "|" & _
                       ddlWS.SelectedItem.Value & "|" & _
                       ddlLoan.SelectedItem.Value & "|" & _
                       ddlBFEmp.SelectedItem.Value & "|" & _
                       ddlOutPayEmp.SelectedItem.Value & "|" & _
                       ddlAbsent.SelectedItem.Value & "|" & _
                       ddlHold.SelectedItem.Value & "|" & _
                       ddlPayment.SelectedItem.Value & "|" & _                       
                       ddlSubsidy.SelectedItem.Value & "|" & _
                       ddlDeficit.SelectedItem.Value & "|" & _
                       ddlLevyAdj.SelectedItem.Value & "|" & _
                       intLevyDeduction & "|" & _
                       Trim(txtDailyHour.Text) & "|" & _
                       Trim(txtWorkDay.Text) & "|" & _
                       Trim(txtOffPayRate.Text) & "|" & _
                       Trim(txtHolidayPayRate.Text) & "|" & _
                       Trim(txtOffMonthRate.text) & "|" & _
                       Trim(txtHolidayMonthRate.text) & "|" & _                       
                       Trim(txtOTRate.Text) & "|" & _
                       Trim(txtMaxDeduct.text) & "|" & _
                       strHarvAutoWeight & "|" & _
                       strHarvGroupWeight & "|" & _
                       strHarvDailyWeight & "|" & _
                       ddlWSRefund.SelectedItem.Value & "|" & _ 
                       ddlTHR.SelectedItem.Value & "|" & _ 
                       ddlAdvSalary.SelectedItem.Value & "|" & _ 
                       ddlSPSICOP.SelectedItem.Value & "|" & _
                       ddlLuranCOP.SelectedItem.Value & "|" & _                       
                       ddlOther.SelectedItem.Value & "|" & _
                       ddlInvCatuBeras.SelectedItem.Value & "|" & _
                       Trim(txtBasicPayDay.Text) & "|" & _
                       Trim(txtProratePayDay.Text) & "|" & _                          
                       Trim(txtAdvPytSKUHarian.Text) & "|" & _ 
                       Trim(txtAdvPytSKUBulanan.Text) & "|" & _ 
                       Trim(txtWorkDaySKUHarian.Text) & "|" & _ 
                       Trim(txtWorkDaySKUBulanan.Text) & "|" & _
                       ddlHouseRent.SelectedItem.Value & "|" & _
                       ddlMeal.SelectedItem.Value & "|" & _
                       ddlLeave.SelectedItem.Value & "|" & _
                       ddlAirBus.SelectedItem.Value & "|" & _
                       ddlMedical.SelectedItem.Value & "|" & _
                       ddlMaternity.SelectedItem.Value & "|" & _
                       ddlTax.SelectedItem.Value & "|" & _
                       ddlDanaPensiun.SelectedItem.Value & "|" & _
                       ddlRelocation.SelectedItem.Value & "|" & _
                       ddlRapel.SelectedItem.Value & "|" & _
                       ddlStaff.SelectedItem.Value & "|" & _
                       ddlFunctional.SelectedItem.Value & "|" & _
                       ddlHutang.SelectedItem.Value & "|" & _ 
                       ddlRiceDeduction.SelectedItem.Value & "|" & _ 
                       ddlHarvestInc.SelectedItem.Value

                strParam =  strParam & "|" & ddlCashAccount.SelectedItem.Value & "|" & _
                                             ddlBankCode.SelectedItem.Value  

            Try
                intErrNo = objPRSetup.mtdUpdPayrollConfig(strOpCd, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_PAYROLLPROCESS_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=pr/setup/PR_setup_Payroll.aspx")
            End Try
        End If

        onLoad_Display()
    End Sub

    Sub BindCatuBeras(ByVal pv_strCatuberas As String)
        Dim strOpCdItemCode_Get As String = "IN_CLSSETUP_INVMASTER_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = 0
        Dim intCnt As Integer
        dim intErrNo As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow

        SearchStr = "AND ItemType = '" & objIN.EnumInventoryItemType.Stock & "' AND itm.Status = '" & objIN.EnumStockItemStatus.Active & "'"

        strParam = "ORDER BY ItemCode asc|" & SearchStr

        Try
            intErrNo = objIN.mtdGetMasterList(strOpCdItemCode_Get, strParam, objIN.EnumInventoryMasterType.StockItem, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_stockitem.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If Not pv_strCatuberas = "" Then
                If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode"))) = UCase(Trim(pv_strCatuberas)) Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Please Select a Catu Beras Code "
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        ddlInvCatuBeras.DataSource = dsForDropDown.Tables(0)
        ddlInvCatuBeras.DataValueField = "ItemCode"
        ddlInvCatuBeras.DataTextField = "Description"
        ddlInvCatuBeras.DataBind()

        ddlInvCatuBeras.SelectedIndex = SelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindHarvestIncAcc(ByVal pv_strHarvestIncAcc As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objAccDs As DataSet


        strOpCd = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        strParam = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "' "

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If pv_strHarvestIncAcc <> "" Then
            For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
                If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strHarvestIncAcc) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Please select Harvester Incentive Account"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlHarvestInc.DataSource = objAccDs.Tables(0)
        ddlHarvestInc.DataValueField = "AccCode"
        ddlHarvestInc.DataTextField = "_Description"
        ddlHarvestInc.DataBind()
        ddlHarvestInc.SelectedIndex = intSelectedIndex
    End Sub
   
    Sub BindBankCode(ByVal pv_strBankCode As String)
        Dim strOpCode As String = "HR_CLSSETUP_BANK_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedBankIndex As Integer = 0
        Dim objBankDs As DataSet

        strParam = "|"

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   objHRSetup.EnumHRMasterType.Bank, _
                                                   objBankDs)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_STP_PAYROLL_GETBANKCODE&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
            objBankDs.Tables(0).Rows(intCnt).Item("BankCode") = objBankDs.Tables(0).Rows(intCnt).Item("BankCode").Trim()
            objBankDs.Tables(0).Rows(intCnt).Item("Description") = objBankDs.Tables(0).Rows(intCnt).Item("BankCode") & " (" & objBankDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If pv_strBankCode = objBankDs.Tables(0).Rows(intCnt).Item("BankCode") Then
                intSelectedBankIndex = intCnt + 1
            End If
        Next intCnt

        Dim dr As DataRow
        dr = objBankDs.Tables(0).NewRow()
        dr("BankCode") = ""
        dr("Description") = "Please Select Bank Code" 
        objBankDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBankCode.DataSource = objBankDs.Tables(0)
        ddlBankCode.DataValueField = "BankCode"
        ddlBankCode.DataTextField = "Description"
        ddlBankCode.DataBind()
        ddlBankCode.SelectedIndex = intSelectedBankIndex
    End Sub

   Sub BindCashAccCode(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedAccIndex As Integer = 0
        Dim objAccDs As DataSet

        strParam = "Order By ACC.AccCode|AND ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "' " &  " AND ACC.AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "'"
       
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_STP_PAYROLL_GETACCCODE&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(objAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            
            If pv_strAccCode = objAccDs.Tables(0).Rows(intCnt).Item("AccCode") Then
                intSelectedAccIndex = intCnt + 1
            End If
        
        Next

        Dim dr As DataRow
        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Please Select Account Code" 
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlCashAccount.DataSource = objAccDs.Tables(0)
        ddlCashAccount.DataValueField = "AccCode"
        ddlCashAccount.DataTextField = "Description"
        ddlCashAccount.DataBind()
        ddlCashAccount.SelectedIndex = intSelectedAccIndex
    End Sub




End Class
