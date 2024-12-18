
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic
Imports agri.PWSystem.clsConfig
Imports agri.Admin
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PWSystem.clsLangCap

Public Class system_config_sysloc : Inherits Page
    
    Protected WithEvents lblCompany As Label
    Protected WithEvents lblLocError As Label
    Protected WithEvents lblNoLocError As Label
    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents hidCompName As HtmlInputHidden
    Protected WithEvents hidCompCode As HtmlInputHidden
    Protected WithEvents hidLocCode As HtmlInputHidden
    Protected WithEvents hidLocName As HtmlInputHidden
    Protected WithEvents hidLanguage As HtmlInputHidden
    Protected WithEvents hidRecType As HtmlInputHidden
    Protected WithEvents txtDoc As TextBox
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents lblComp As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblMaster As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblAnd As Label
    Protected WithEvents lblNationality As Label
    Protected WithEvents lblPosition As Label
    Protected WithEvents lblICType As Label
    Protected WithEvents lblEvaluation As Label
    Protected WithEvents lblSalary As Label
    Protected WithEvents lblBank As Label
    Protected WithEvents lblTax As Label
    Protected WithEvents lblAD As Label

    Protected WithEvents cbIN As Checkbox
    Protected WithEvents cbINProdMaster As CheckBox
    Protected WithEvents cbINItem As CheckBox
    Protected WithEvents cbINDirChrg As CheckBox
    Protected WithEvents cbMiscItem As CheckBox
    Protected WithEvents cbINPR As CheckBox
    Protected WithEvents cbStkRtnAdv As CheckBox
    Protected WithEvents cbStkTransfer As CheckBox
    Protected WithEvents cbStkIsu As CheckBox
    Protected WithEvents cbStkRcv As CheckBox
    Protected WithEvents cbStkRtn As CheckBox
    Protected WithEvents cbStkAdj As CheckBox
    Protected WithEvents cbFuelIsu As CheckBox
    Protected WithEvents cbDtInv As CheckBox
    Protected WithEvents cbDtDwIN As CheckBox
    Protected WithEvents cbINMthEnd As CheckBox
    Protected WithEvents cbItemToMachine As CheckBox
    Protected WithEvents cbStkTransferInternal As CheckBox

    Protected WithEvents cbCT As CheckBox
    Protected WithEvents cbCTMaster As CheckBox
    Protected WithEvents cbCTItem As CheckBox
    Protected WithEvents cbCTPR As CheckBox
    Protected WithEvents cbCTRcv As CheckBox
    Protected WithEvents cbCTRtnAdv As CheckBox
    Protected WithEvents cbCTIsu As CheckBox
    Protected WithEvents cbCTRtn As CheckBox
    Protected WithEvents cbCTAdj As CheckBox
    Protected WithEvents cbCTTransfer As CheckBox
    Protected WithEvents cbDtCT As CheckBox
    Protected WithEvents cbDtDwCT As CheckBox
    Protected WithEvents cbCTMthEnd As CheckBox

    Protected WithEvents cbWS As Checkbox
    Protected WithEvents cbWSProdMaster As CheckBox
    Protected WithEvents cbWSWorkMaster As CheckBox
    Protected WithEvents cbWSItem As CheckBox
    Protected WithEvents cbWSDirChrg As CheckBox
    Protected WithEvents cbWSJob As CheckBox
    Protected WithEvents cbWSMechHr As CheckBox
    Protected WithEvents cbWSDN As CheckBox
    Protected WithEvents cbDtWS As CheckBox
    Protected WithEvents cbDtDwWS As CheckBox
    Protected WithEvents cbWSMthEnd As CheckBox
    Protected WithEvents cbWSPart As CheckBox
    Protected WithEvents cbWSMillProcDitr As CheckBox
    Protected WithEvents cbWSCalMachine As CheckBox

    Protected WithEvents cbPU As Checkbox
    Protected WithEvents cbPUSupp As CheckBox
    Protected WithEvents cbPUPelimpahan As CheckBox
    Protected WithEvents cbPURPH As CheckBox
    Protected WithEvents cbPUPO As CheckBox
    Protected WithEvents cbPUGoodsRcv As CheckBox
    Protected WithEvents cbPUGRN As CheckBox
    Protected WithEvents cbPUDA As CheckBox
    Protected WithEvents cbDtPU As CheckBox
    Protected WithEvents cbDtDwPU As CheckBox
    Protected WithEvents cbPUMthEnd As CheckBox


    Protected WithEvents cbAP As Checkbox
    Protected WithEvents cbAPInvoice As CheckBox
    Protected WithEvents cbAPDN As CheckBox
    Protected WithEvents cbAPCN As CheckBox
    Protected WithEvents cbAPCrtJrn As CheckBox
    Protected WithEvents cbAPPay As CheckBox
    Protected WithEvents cbDtDwAP As CheckBox
    Protected WithEvents cbAPMthEnd As CheckBox

    Protected WithEvents cbHR As Checkbox
    Protected WithEvents cbHRDepartment As CheckBox
    Protected WithEvents cbHRCompany As CheckBox
    Protected WithEvents cbHRFunc As CheckBox
    Protected WithEvents cbHRSkill As CheckBox
    Protected WithEvents cbHREval As CheckBox
    Protected WithEvents cbHRBank As CheckBox
    Protected WithEvents cbHRHoliday As CheckBox
    Protected WithEvents cbHRCP As CheckBox
    Protected WithEvents cbHREmpDet As CheckBox
    Protected WithEvents cbHREmpPR As CheckBox
    Protected WithEvents cbHREmpEmploy As CheckBox
    Protected WithEvents cbHRSat As CheckBox
    Protected WithEvents cbHREmpFam As CheckBox
    Protected WithEvents cbHREmpQlf As CheckBox
    Protected WithEvents cbHREmpSkill As CheckBox
    Protected WithEvents cbHRContSuper As CheckBox
    Protected WithEvents cbGenEmpCode As CheckBox
    Protected WithEvents cbDtHR As CheckBox

    Protected WithEvents cbPR As Checkbox
    Protected WithEvents cbPRAD As CheckBox
    Protected WithEvents cbPRSal As CheckBox
    Protected WithEvents cbPRContract As CheckBox
    Protected WithEvents cbPRAttdTrx As CheckBox
    Protected WithEvents cbPRTripTrx As CheckBox
    Protected WithEvents cbPRRatePay As CheckBox
    Protected WithEvents cbPRADTrx As CheckBox
    Protected WithEvents cbPRContCheckroll As CheckBox
    Protected WithEvents cbDtPR As CheckBox
    Protected WithEvents cbDwPR As CheckBox
    Protected WithEvents cbPRMthEnd As CheckBox
    Protected WithEvents cbPRYearEnd As CheckBox
    Protected WithEvents cbPRRice As CheckBox
    Protected WithEvents cbPRWorkPerformance As CheckBox 
    Protected WithEvents cbPREmployeeEvaluation As CheckBox 
    Protected WithEvents cbPRStandardEvaluation As CheckBox 
    Protected WithEvents cbPRSalaryIncrease As CheckBox 
    Protected WithEvents cbPRWPContractor As CheckBox 
    Protected WithEvents cbPRTranInc As CheckBox 
    
    Protected WithEvents cbBI As Checkbox
    Protected WithEvents cbBINote As CheckBox
    Protected WithEvents cbBillParty As CheckBox
    Protected WithEvents cbDtBI As CheckBox
    Protected WithEvents cbDtDwBI As CheckBox
    Protected WithEvents cbBIMthEnd As CheckBox
    Protected WithEvents cbBIInvoice As CheckBox
    Protected WithEvents cbBIReceipt As CheckBox
    Protected WithEvents cbBIJournal As CheckBox

    Protected WithEvents cbPD As Checkbox
    Protected WithEvents cbEstProd As CheckBox
    Protected WithEvents cbPOMProd As CheckBox
    Protected WithEvents cbDtDwPD As CheckBox
    Protected WithEvents cbPDMthEnd As CheckBox
    Protected WithEvents cbYearPlantYield As CheckBox

    Protected WithEvents cbGL As Checkbox
    Protected WithEvents cbAccCls As CheckBox
    Protected WithEvents cbAct As CheckBox
    Protected WithEvents cbVehExp As CheckBox
    Protected WithEvents cbVeh As CheckBox
    Protected WithEvents cbVehType As CheckBox
    Protected WithEvents cbBlkGrp As CheckBox
    Protected WithEvents cbBlk As CheckBox
    Protected WithEvents cbExp As CheckBox
    Protected WithEvents cbAccount As CheckBox
    Protected WithEvents cbEntrySetup As CheckBox
    Protected WithEvents cbBalSheetSetup As CheckBox
    Protected WithEvents cbProfLossSetup As CheckBox
    Protected WithEvents cbJrn As CheckBox
    Protected WithEvents cbJrnAdj As CheckBox
    Protected WithEvents cbVehUsg As CheckBox
    Protected WithEvents cbDtGL As CheckBox
    Protected WithEvents cbDtUp As CheckBox
    Protected WithEvents cbGLGCDist As CheckBox
    Protected WithEvents cbGLJrnMthEnd As CheckBox
    Protected WithEvents cbGLMthEnd As CheckBox
    Protected WithEvents cbRunHour As CheckBox
    Protected WithEvents cbRC As Checkbox
    Protected WithEvents cbRCDA As CheckBox
    Protected WithEvents cbRCJrn As CheckBox
    Protected WithEvents cbReadInterRC As CheckBox
    Protected WithEvents cbSendInterRC As CheckBox
    Protected WithEvents cbDtRC As CheckBox

    Protected WithEvents cbAccPeriod As CheckBox
    Protected WithEvents cbBudgeting As CheckBox

    Protected WithEvents cbWM As CheckBox
    Protected WithEvents cbWMTransport As CheckBox
    Protected WithEvents cbWMTicket As CheckBox
    Protected WithEvents cbWMFFBAssessment As CheckBox
    Protected WithEvents cbWMDataTransfer As CheckBox

    Protected WithEvents cbPM As CheckBox
    Protected WithEvents cbPMMasterSetup As CheckBox
    Protected WithEvents cbPMDailyProd As CheckBox
    Protected WithEvents cbPMCPOStore As CheckBox
    Protected WithEvents cbPMPKStore As CheckBox
    Protected WithEvents cbPMOilLoss As CheckBox
    Protected WithEvents cbPMOilQuality As CheckBox
    Protected WithEvents cbPMKernelQuality As CheckBox
    Protected WithEvents cbPMProdKernel As CheckBox
    Protected WithEvents cbPMDispKernel As CheckBox
    Protected WithEvents cbPMWater As CheckBox
    Protected WithEvents cbPMNutFibre As CheckBox
    Protected WithEvents cbPMDayEnd As CheckBox
    Protected WithEvents cbPMMthEnd As CheckBox
    Protected WithEvents cbPMMill As CheckBox

    Protected WithEvents cbCM As CheckBox
    Protected WithEvents cbCMMasterSetup As CheckBox
    Protected WithEvents cbCMContractReg As CheckBox
    Protected WithEvents cbCMContractMatch As CheckBox
    Protected WithEvents cbCMGenDNCN As CheckBox
    Protected WithEvents cbCMDataTransfer As CheckBox

    Protected WithEvents cbNU As CheckBox
    Protected WithEvents cbNUMasterSetup As CheckBox
    Protected WithEvents cbNUWorkAccDist As CheckBox
    Protected WithEvents cbNUSeedRcv As CheckBox
    Protected WithEvents cbNUSeedPlant As CheckBox
    Protected WithEvents cbNUDblTurn As CheckBox
    Protected WithEvents cbNUTransplanting As CheckBox
    Protected WithEvents cbNUDispatch As CheckBox
    Protected WithEvents cbNUCulling As CheckBox
    Protected WithEvents cbDtNu As CheckBox
    Protected WithEvents cbNUMonthEnd As CheckBox

    Protected WithEvents cbFA As CheckBox
    Protected WithEvents cbFAClassSetup As CheckBox
    Protected WithEvents cbFAGroupSetup As CheckBox
    Protected WithEvents cbFARegSetup As CheckBox
    Protected WithEvents cbFAPermissionSetup AS CheckBox
    Protected WithEvents cbFAAddition As CheckBox
    Protected WithEvents cbFADepreciation As CheckBox
    Protected WithEvents cbFADisposal As CheckBox
    Protected WithEvents cbFAWriteOff As CheckBox
    Protected WithEvents cbFAGenDepreciation As CheckBox
    Protected WithEvents cbFAMasterSetup As CheckBox
    Protected WithEvents cbFAItemSetup As CheckBox
    Protected WithEvents cbDtDwFA As CheckBox
    Protected WithEvents cbDtUpFA As CheckBox
    Protected WithEvents cbFAMonthEnd As CheckBox
    
    Protected WithEvents cbADTIN As CheckBox
    Protected WithEvents cbADTCT As CheckBox
    Protected WithEvents cbADTWS As CheckBox
    Protected WithEvents cbADTNU As CheckBox
    Protected WithEvents cbADTPU As CheckBox
    Protected WithEvents cbADTAP As CheckBox
    Protected WithEvents cbADTPR As CheckBox
    Protected WithEvents cbADTPD As CheckBox
    Protected WithEvents cbADTBI As CheckBox
    Protected WithEvents cbADTGL As CheckBox
    Protected WithEvents DeleteBtn As ImageButton

    Protected WithEvents cbCB As CheckBox
    Protected WithEvents cbCBPayment As CheckBox
    Protected WithEvents cbCBReceipt As CheckBox
    Protected WithEvents cbCBDeposit As CheckBox
    Protected WithEvents cbCBInterAdj As CheckBox
    Protected WithEvents cbCBWithdrawal As CheckBox
    Protected WithEvents cbCBMthEnd As CheckBox
    Protected WithEvents cbCBCashFlow As CheckBox
    Protected WithEvents cbCBCashBank As CheckBox

    Protected WithEvents lblINARExt As Label
    Protected WithEvents lblCTARExt As Label
    Protected WithEvents lblWSARExt As Label
    Protected WithEvents lblPUARExt As Label
    Protected WithEvents lblAPARExt As Label
    Protected WithEvents lblPRARExt As Label
    Protected WithEvents lblHRARExt As Label
    Protected WithEvents lblBIARExt As Label
    Protected WithEvents lblPDARExt As Label
    Protected WithEvents lblGLARExt As Label
    Protected WithEvents lblWMARExt As Label
    Protected WithEvents lblPMARExt As Label
    Protected WithEvents lblCMARExt As Label
    Protected WithEvents lblNUARExt As Label
    Protected WithEvents lblFAARExt As Label
    Protected WithEvents lblADARExt As Label
    Protected WithEvents lblCBARExt As Label 

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim objLocDS As New Data.DataSet()
    Dim objLangCapDs As New Data.DataSet()

    Dim strOpCode_UpdUserLoc As String = "PWSYSTEM_CLSUSER_USERLOC_DETAILS_UPD"
    Dim strUserLocGet As String = "PWSYSTEM_CLSCONFIG_USERLOCLIST_GET"
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intADAR As Integer

    Dim gblLocCode As String
    Dim gblCompCode As String
    Dim gblCompName As string
    Protected WithEvents numericDocRetain As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents cbFullRights As System.Web.UI.WebControls.CheckBox
    Protected WithEvents SaveBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents BackBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents cbPMKernelLoss As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbPMWastedWaterQuality As System.Web.UI.WebControls.CheckBox

    Protected WithEvents TrMPOB As HtmlTableRow
    Protected WithEvents TrTranInc As HtmlTableRow
    Protected WithEvents TrHarvInc As HtmlTableRow

    Protected WithEvents cbINProdBrand As CheckBox
    Protected WithEvents cbINProdModel As CheckBox
    Protected WithEvents cbINProdCategory As CheckBox
    Protected WithEvents cbINProdMaterial As CheckBox
    Protected WithEvents cbINStockAnalysis As CheckBox
    Protected WithEvents cbINItemMaster As CheckBox

    Protected WithEvents cbWSProdBrand As CheckBox
    Protected WithEvents cbWSProdModel As CheckBox
    Protected WithEvents cbWSProdCategory As CheckBox
    Protected WithEvents cbWSProdMaterial As CheckBox
    Protected WithEvents cbWSStockAnalysis As CheckBox
    Protected WithEvents cbWSItemMaster As CheckBox
    Protected WithEvents cbWSWorkshopService As CheckBox

    Protected WithEvents cbNUCullType As CheckBox

    Protected WithEvents cbHRNationality As CheckBox
    Protected WithEvents cbHRPosition As CheckBox
    Protected WithEvents cbHRLevel As CheckBox
    Protected WithEvents cbHRReligion As CheckBox
    Protected WithEvents cbHRICType As CheckBox
    Protected WithEvents cbHRRace As CheckBox
    Protected WithEvents cbHRQualification As CheckBox
    Protected WithEvents cbHRSubject As CheckBox
    Protected WithEvents cbHRCPCode As CheckBox
    Protected WithEvents cbHRSalScheme As CheckBox
    Protected WithEvents cbHRSalGrade As CheckBox
    Protected WithEvents cbHRShift As CheckBox
    Protected WithEvents cbHRGang As CheckBox
    Protected WithEvents cbHRBankFormat As CheckBox
    Protected WithEvents cbHRJamsostek As CheckBox
    Protected WithEvents cbHRTaxBranch As CheckBox
    Protected WithEvents cbHRTax As CheckBox
    Protected WithEvents cbHRPublicHoliday As CheckBox
    Protected WithEvents cbHRPOH As CheckBox

    Protected WithEvents cbPRADGroup As CheckBox
    Protected WithEvents cbPRDenda As CheckBox
    Protected WithEvents cbPRHarvInc As CheckBox
    Protected WithEvents cbPRLoad As CheckBox
    Protected WithEvents cbPRRoute As CheckBox
    Protected WithEvents cbPRMedical As CheckBox
    Protected WithEvents cbPRAirBus As CheckBox
    Protected WithEvents cbPRMaternity As CheckBox
    Protected WithEvents cbPRPensiun As CheckBox
    Protected WithEvents cbPRRelocation As CheckBox
    Protected WithEvents cbPRIncentive As CheckBox
    Protected WithEvents cbPRContractPay As CheckBox
    Protected WithEvents cbPRWagesPay As CheckBox
    Protected WithEvents cbDwPRWages As CheckBox
    Protected WithEvents cbDwPRBankAuto As CheckBox

    Protected WithEvents cbBICreditNote As CheckBox

    Protected WithEvents cbAccClsGrp As CheckBox
    Protected WithEvents cbActGrp As CheckBox
    Protected WithEvents cbSubAct As CheckBox
    Protected WithEvents cbVehExpGrp As CheckBox
    Protected WithEvents cbSubBlk As CheckBox
    Protected WithEvents cbAccountGrp As CheckBox
    Protected WithEvents cbPosting As CheckBox

    Protected WithEvents cbPeriodCfg As CheckBox

    Protected WithEvents cbPOMStorage As CheckBox
    Protected WithEvents cbPOMStat As CheckBox

    Protected WithEvents cbWSDirChrgMaster As CheckBox

    Protected WithEvents cbPRPaySetup As CheckBox
    Protected WithEvents cbPRDailyAttd As CheckBox
    Protected WithEvents cbPRHarvAttd As CheckBox
    Protected WithEvents cbPRWeekly As CheckBox
    Protected WithEvents cbPRMthRice As CheckBox
    Protected WithEvents cbPRMthRapel As CheckBox
    Protected WithEvents cbPRMthBonus As CheckBox
    Protected WithEvents cbPRMthTHR As CheckBox
    Protected WithEvents cbPRMthDaily As CheckBox
    Protected WithEvents cbPRMthPayroll As CheckBox
    Protected WithEvents cbPRMthTransfer As CheckBox

    Protected WithEvents cbNUMasterItem As CheckBox
    Protected WithEvents cbNUItem As CheckBox
    Protected WithEvents cbNUSeedIssue As CheckBox

    Protected WithEvents cbFARegLine As CheckBox

    Protected WithEvents cbGLCOGS As CheckBox
    
    Protected WithEvents cbFSSetup As CheckBox

    Protected WithEvents cbPMVolConvMaster As CheckBox
    Protected WithEvents cbPMAvgCapConvMaster As CheckBox
    Protected WithEvents cbPMCPOPropertyMaster As CheckBox
    Protected WithEvents cbPMStorageTypeMaster As CheckBox
    Protected WithEvents cbPMStorageAreaMaster As CheckBox
    Protected WithEvents cbPMProcessingLineMaster As CheckBox
    Protected WithEvents cbPMMachineMaster As CheckBox
    Protected WithEvents cbPMAcceptableOilQuality As CheckBox
    Protected WithEvents cbPMAcceptableKernelQuality As CheckBox
    Protected WithEvents cbPMTestSample As CheckBox
    Protected WithEvents cbPMHarvestingInterval As CheckBox
    
    Protected WithEvents cbPMMachineCriteria As CheckBox

    Protected WithEvents cbCMExchangeRate As CheckBox
    Protected WithEvents cbCMContractQuality As CheckBox
    Protected WithEvents cbCMClaimQuality As CheckBox

    Protected WithEvents cbCMContractDOReg As CheckBox
  

    Protected WithEvents cbDwAttdInterface As CheckBox

    Dim strHarvTag As string = ""
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")
		
	        strLocType = Session("SS_LOCTYPE")
	    
        TrMPOB.Visible = False
        lblNoLocError.Visible = False
        lblLocError.Visible = False
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADSystemConfig), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                onLoad_Display()
                onLoad_ControlDelete()
                If Trim(strLocType) <> "" Then
                    If Trim(strLocType) = objAdminLoc.EnumLocType.Mill Then
                        TrTranInc.Visible = True
                        TrHarvInc.Visible = False                      
                    ElseIf Trim(strLocType) = objAdminLoc.EnumLocType.Estate Then
                        TrTranInc.Visible = False     
                        TrHarvInc.Visible = True                  
                    End If  
                End IF
            End if
        End If

        cbAPPay.visible = false
        cbBIReceipt.visible = false
        cbAPPay.Checked = false
        cbBIReceipt.Checked = false

    End Sub

    Sub onload_GetLangCap()
        Dim strProdTypeTag As String
        Dim strProdBrandTag As String
        Dim strProdModelTag As String
        Dim strProdCatTag As String
        Dim strProdMatTag As String
        Dim strStockAnaTag As String
        Dim strStockItemTag As String
        Dim strDirectChgItemTag As String
        Dim strDepartmentTag As String
        Dim strDepartmentDescTag As String
        Dim strFuncTag As String
        Dim strLevelTag As String
        Dim strReligionTag As String
        Dim strCareerProgTag As String
        Dim strLoadTag As String
        Dim strRouteTag As String
        Dim strMedicalTag As String
        Dim strAirBusTicketTag As String
        Dim strMaternityTag As String
        Dim strDanaPensiunTag As String
        Dim strRelocationTag As String
        Dim strJamsostekTag As String
        Dim strRiceRationTag As String
        Dim strIncentiveTag As String
        Dim strQuotaIncentiveTag As String
        Dim strBillPartyTag As String
        Dim strAccClsTag As String
        Dim strAccClsGrpTag As String
        Dim strActGrpTag As String
        Dim strActTag As String
        Dim strSubActTag As String
        Dim strVehExpenseGrpTag As String
        Dim strVehExpenseTag As String
        Dim strVehTag As String
        Dim strVehTypeTag As String
        Dim strBlockGrpTag As String
        Dim strBlockTag As String
        Dim strSubBlockTag As String
        Dim strExpenseTag As String
        Dim strAccGrpTag As String
        Dim strAccTag As String
        Dim strVehUsageTag As String
        Dim strEmployeeEvaluationTag As String
        Dim strStandardEvaluationTag As String
        Dim strSalaryIncreaseTag As String
        Dim strTranIncTag As String
        Dim strWorkTag As String
        Dim strAcctDistTag As String
        Dim strNUBatchTag As String
        Dim strNUCullTag As String
        strHarvTag= " Harvesting Incentive"

        GetEntireLangCap()

        lblComp.text = GetCaption(objLangCap.EnumLangCap.Company)
        lblLocation.text = GetCaption(objLangCap.EnumLangCap.Location)
        lblLocError.text = lblErrSelect.text & lblLocation.text

        strProdTypeTag = GetCaption(objLangCap.EnumLangCap.ProdType)
        strProdBrandTag = GetCaption(objLangCap.EnumLangCap.ProdBrand)
        strProdModelTag = GetCaption(objLangCap.EnumLangCap.ProdModel)
        strProdCatTag = GetCaption(objLangCap.EnumLangCap.ProdCat)
        strProdMatTag = GetCaption(objLangCap.EnumLangCap.ProdMat)
        strStockAnaTag = GetCaption(objLangCap.EnumLangCap.StockAnalysis)
        strStockItemTag = GetCaption(objLangCap.EnumLangCap.StockItem)
        strDirectChgItemTag = GetCaption(objLangCap.EnumLangCap.DirectChgItem)
        strDepartmentTag = GetCaption(objLangCap.EnumLangCap.Department)
        strDepartmentDescTag = GetCaption(objLangCap.EnumLangCap.DepartmentDesc)
        strFuncTag = GetCaption(objLangCap.EnumLangCap.Func)
        strLevelTag = GetCaption(objLangCap.EnumLangCap.Level)
        strReligionTag = GetCaption(objLangCap.EnumLangCap.Religion)
        strCareerProgTag = GetCaption(objLangCap.EnumLangCap.CareerProgress)
        strLoadTag = GetCaption(objLangCap.EnumLangCap.Load)
        strRouteTag = GetCaption(objLangCap.EnumLangCap.Route)
        strMedicalTag = GetCaption(objLangCap.EnumLangCap.Medical)
        strAirBusTicketTag = GetCaption(objLangCap.EnumLangCap.AirBusTicket)
        strMaternityTag = GetCaption(objLangCap.EnumLangCap.Maternity)
        strDanaPensiunTag = GetCaption(objLangCap.EnumLangCap.DanaPensiun)
        strRelocationTag = GetCaption(objLangCap.EnumLangCap.Relocation)
        strJamsostekTag = GetCaption(objLangCap.EnumLangCap.Jamsostek)
        strRiceRationTag = GetCaption(objLangCap.EnumLangCap.RiceRation)
        strIncentiveTag = GetCaption(objLangCap.EnumLangCap.Incentive)
        strQuotaIncentiveTag = GetCaption(objLangCap.EnumLangCap.QuotaIncentive)
        strBillPartyTag = GetCaption(objLangCap.EnumLangCap.BillParty)
        strAccClsTag = GetCaption(objLangCap.EnumLangCap.AccClass)
        strAccClsGrpTag = GetCaption(objLangCap.EnumLangCap.AccClsGrp)
        strActGrpTag = GetCaption(objLangCap.EnumLangCap.ActGrp)
        strActTag = GetCaption(objLangCap.EnumLangCap.Activity)
        strSubActTag = GetCaption(objLangCap.EnumLangCap.SubAct)
        strVehExpenseGrpTag = GetCaption(objLangCap.EnumLangCap.VehExpense)
        strVehExpenseTag = GetCaption(objLangCap.EnumLangCap.VehExpense)
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strVehTypeTag = GetCaption(objLangCap.EnumLangCap.VehType)
        strBlockGrpTag = GetCaption(objLangCap.EnumLangCap.BlockGrp)
        strBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        strSubBlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
        strExpenseTag = GetCaption(objLangCap.EnumLangCap.Expense)
        strAccGrpTag = GetCaption(objLangCap.EnumLangCap.AccGrp)
        strAccTag = GetCaption(objLangCap.EnumLangCap.Account)
        strVehUsageTag = GetCaption(objLangCap.EnumLangCap.VehUsage)
        strEmployeeEvaluationTag = GetCaption(objLangCap.EnumLangCap.EmployeeEvaluation)
        strStandardEvaluationTag = GetCaption(objLangCap.EnumLangCap.StandardEvaluation)
        strSalaryIncreaseTag = GetCaption(objLangCap.EnumLangCap.SalaryIncrease)
        strTranIncTag = GetCaption(objLangCap.EnumLangCap.TransportIncentive)
        strWorkTag = GetCaption(objLangCap.EnumLangCap.Work)
        strAcctDistTag = GetCaption(objLangCap.EnumLangCap.AccountDistribution)
        strNUBatchTag = GetCaption(objLangCap.EnumLangCap.NurseryBatch)
        strNUCullTag = GetCaption(objLangCap.EnumLangCap.CullType)


        cbINProdMaster.text = " " & strProdTypeTag
            cbINProdBrand.text =  " " & strProdBrandTag
            cbINProdModel.text = " " &  strProdModelTag
            cbINProdCategory.text = " " &  strProdCatTag
            cbINProdMaterial.text = " " &  strProdMatTag
            cbINStockAnalysis.text = " " &  strStockAnaTag
            cbINItemMaster.text = " " &  strStockItemTag & " Master"

            cbWSProdBrand.text =  " " & strProdBrandTag
            cbWSProdModel.text = " " &  strProdModelTag
            cbWSProdCategory.text = " " &  strProdCatTag
            cbWSProdMaterial.text = " " &  strProdMatTag
            cbWSStockAnalysis.text = " " &  strStockAnaTag
            cbWSItemMaster.text = " Workshop " &  strStockItemTag & " Master"

            cbHRFunc.text = " " & strFuncTag
            cbHRLevel.text = " " & strLevelTag
            cbHRReligion.text = " " & strReligionTag
            cbHRCPCode.text = " " & strCareerProgTag & " Code"
            cbHRJamsostek.text = " " & strJamsostekTag

            cbPRHarvInc.text = " " & strHarvTag 
            cbPRLoad.text = " " & strLoadTag 
            cbPRRoute.text = " " & strRouteTag 
            cbPRMedical.text = " " & strMedicalTag
            cbPRAirBus.text = " " & strAirBusTicketTag
            cbPRMaternity.text = " " & strMaternityTag
            cbPRPensiun.text = " " & strDanaPensiunTag
            cbPRRelocation.text = " " & strRelocationTag 
            cbPRIncentive.text = " " & strIncentiveTag 

            cbAccClsGrp.text = " " & strAccClsGrpTag
            cbActGrp.text = " " & strActGrpTag
            cbSubAct.text = " " & strSubActTag

            cbVehExpGrp.text = " " & strVehExpenseGrpTag & " Group" & lblCode.text
            cbVehType.Text = " " & strVehTypeTag

            cbBlk.Text = " " & strBlockTag
            cbSubBlk.Text = " " & strSubBlockTag

            cbExp.Text = " " & strExpenseTag & lblCode.Text

            cbAccountGrp.text = " " & strAccGrpTag
            cbWSWorkMaster.text = " " & strWorkTag & " Code"
            cbNUWorkAccDist.text =  " " & strAcctDistTag
            cbNUMasterSetup.text =  " " & strNUBatchTag
            cbNUCullType.text =  " " & strNUCullTag
        cbINItem.text = " " & strStockItemTag

        cbINDirChrg.text = " " & strDirectChgItemTag

        cbHRDepartment.text = " " & strDepartmentTag & lblCode.text
        cbHRCompany.text =  " "  & strDepartmentTag 

        cbHREval.text = " " & lblEvaluation.text

        cbHRBank.text = " " & lblBank.text

        cbHRCP.text = strCareerProgTag

        cbPRAD.text = " " & lblAD.text

        cbPREmployeeEvaluation.text = " " &  strEmployeeEvaluationTag

        cbPRStandardEvaluation.text = " " & strStandardEvaluationTag

        cbPRSalaryIncrease.text = " " & strSalaryIncreaseTag
        cbPRTranInc.text = " " &  strTranIncTag

        cbBillParty.text =  " " & strBillPartyTag

        cbAccCls.text = " " & strAccClsTag

        cbAct.text = " " & strActTag 

        cbVehExp.text = " " & strVehExpenseTag & lblCode.text

        cbVeh.Text = " " & strVehTag

        cbBlkGrp.Text = " " & strBlockGrpTag

        cbExp.Text = " " & strExpenseTag & lblCode.Text

        cbAccount.text = " "  & strAccTag

        cbVehUsg.text = " " & strVehUsageTag
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
                                                 "", _
                                                 "", _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSTEM_CONFIG_SYSLOC_LANGCAP&errmesg=" & lblErrMesage.Text & "&redirect=")
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

    Sub onLoad_LicChk()
        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Inventory)) = False Then
            cbIN.Enabled = False
            cbINProdMaster.Enabled = False
            cbINItem.Enabled = False
            cbINDirChrg.Enabled = False
            cbMiscItem.Enabled = False
            cbINPR.Enabled = False
            cbStkRtnAdv.Enabled = False
            cbStkTransfer.Enabled = False
            cbStkIsu.Enabled = False
            cbStkRcv.Enabled = False
            cbStkRtn.Enabled = False
            cbStkAdj.Enabled = False
            cbFuelIsu.Enabled = False
            cbDtInv.Enabled = False
            cbDtDwIN.Enabled = False
            cbINMthEnd.Enabled = False
            cbADTIN.Enabled = False
            cbItemToMachine.Enabled = False
            cbStkTransferInternal.Enabled = False
            cbINProdBrand.Enabled = False
            cbINProdModel.Enabled = False
            cbINProdCategory.Enabled = False
            cbINProdMaterial.Enabled = False
            cbINStockAnalysis.Enabled = False
            cbINItemMaster.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Canteen)) = False Then
            cbCT.Enabled = False
            cbCTMaster.Enabled = False
            cbCTItem.Enabled = False
            cbCTPR.Enabled = False
            cbCTRcv.Enabled = False
            cbCTRtnAdv.Enabled = False
            cbCTIsu.Enabled = False
            cbCTRtn.Enabled = False
            cbCTAdj.Enabled = False
            cbCTTransfer.Enabled = False
            cbDtCT.Enabled = False
            cbDtDwCT.Enabled = False
            cbCTMthEnd.Enabled = False
            cbADTCT.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Workshop)) = False Then
            cbWS.Enabled = False
            cbWSProdMaster.Enabled = False
            cbWSWorkMaster.Enabled = False
            cbWSItem.Enabled = False
            cbWSPart.Enabled = False
            cbWSDirChrg.Enabled = False
            cbWSJob.Enabled = False
            cbWSMechHr.Enabled = False
            cbWSDN.Enabled = False
            cbDtWS.Enabled = False
            cbDtDwWS.Enabled = False
            cbWSMthEnd.Enabled = False
            cbADTWS.Enabled = False
            cbWSProdBrand.Enabled = False
            cbWSProdModel.Enabled = False
            cbWSProdCategory.Enabled = False
            cbWSProdMaterial.Enabled = False
            cbWSStockAnalysis.Enabled = False
            cbWSItemMaster.Enabled = False
            cbWSWorkshopService.Enabled = False

            cbWSDirChrgMaster.Enabled = False
            cbWSMillProcDitr.Enabled = False
            cbWSCalMachine.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Purchasing)) = False Then
            cbPU.Enabled = False
            cbPUSupp.Enabled = False
            cbPUPelimpahan.Enabled = False
            cbPURPH.Enabled = False
            cbPUPO.Enabled = False
            cbPUGoodsRcv.Enabled = False
            cbPUGRN.Enabled = False
            cbPUDA.Enabled = False
            cbDtPU.Enabled = False
            cbDtDwPU.Enabled = False
            cbPUMthEnd.Enabled = False
            cbADTPU.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.AccountPayable)) = False Then
            cbAP.Enabled = False
            cbAPInvoice.Enabled = False
            cbAPDN.Enabled = False
            cbAPCN.Enabled = False
            cbAPCrtJrn.Enabled = False
            cbAPPay.Enabled = False
            cbDtDwAP.Enabled = False
            cbAPMthEnd.Enabled = False
            cbADTAP.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.HumanResource)) = False Then
            cbHR.Enabled = False
            cbHRDepartment.Enabled = False
            cbHRCompany.Enabled = False
            cbHRFunc.Enabled = False
            cbHRSkill.Enabled = False
            cbHREval.Enabled = False
            cbHRBank.Enabled = False
            cbHRHoliday.Enabled = False
            cbHRCP.Enabled = False
            cbHREmpDet.Enabled = False
            cbHREmpPR.Enabled = False
            cbHREmpEmploy.Enabled = False
            cbHRSat.Enabled = False
            cbHREmpFam.Enabled = False
            cbHREmpQlf.Enabled = False
            cbHREmpSkill.Enabled = False
            cbHREmpSkill.Enabled = False
            cbHRContSuper.Enabled = False
            cbDtHR.Enabled = False
            cbHRNationality.Enabled = False
            cbHRPosition.Enabled = False
            cbHRLevel.Enabled = False
            cbHRReligion.Enabled = False
            cbHRICType.Enabled = False
            cbHRRace.Enabled = False
            cbHRQualification.Enabled = False
            cbHRSubject.Enabled = False
            cbHRCPCode.Enabled = False
            cbHRSalScheme.Enabled = False
            cbHRSalGrade.Enabled = False
            cbHRShift.Enabled = False
            cbHRGang.Enabled = False
            cbHRBankFormat.Enabled = False
            cbHRJamsostek.Enabled = False
            cbHRTaxBranch.Enabled = False
            cbHRTax.Enabled = False
            cbHRPublicHoliday.Enabled = False
            cbHRPOH.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Payroll)) = False Then
            cbPR.Enabled = False
            cbPRAD.Enabled = False
            cbPRSal.Enabled = False
            cbPRContract.Enabled = False
            cbPRRice.Enabled = False
            cbPRAttdTrx.Enabled = False
            cbPRTripTrx.Enabled = False
            cbPRRatePay.Enabled = False
            cbPRADTrx.Enabled = False
            cbPRContCheckroll.Enabled = False
            cbDtPR.Enabled = False
            cbDwPR.Enabled = False
            cbPRMthEnd.Enabled = False
            cbPRYearEnd.Enabled = False
            cbADTPR.Enabled = False
            cbPRWorkPerformance.Enabled = False
            cbPREmployeeEvaluation.Enabled = False
            cbPRStandardEvaluation.Enabled = False
            cbPRSalaryIncrease.Enabled = False
            cbPRTranInc.Enabled = False
            cbPRADGroup.Enabled = False
            cbPRDenda.Enabled = False
            cbPRHarvInc.Enabled = False
            cbPRLoad.Enabled = False
            cbPRRoute.Enabled = False
            cbPRMedical.Enabled = False
            cbPRAirBus.Enabled = False
            cbPRMaternity.Enabled = False
            cbPRPensiun.Enabled = False
            cbPRRelocation.Enabled = False
            cbPRIncentive.Enabled = False
            cbPRContractPay.Enabled = False
            cbPRWagesPay.Enabled = False
            cbDwPRWages.Enabled = False
            cbDwPRBankAuto.Enabled = False

            cbPRPaySetup.Enabled = False
            cbPRDailyAttd.Enabled = False
            cbPRHarvAttd.Enabled = False
            cbPRWeekly.Enabled = False
            cbPRMthRice.Enabled = False
            cbPRMthRapel.Enabled = False
            cbPRMthBonus.Enabled = False
            cbPRMthTHR.Enabled = False
            cbPRMthDaily.Enabled = False
            cbPRMthPayroll.Enabled = False
            cbPRMthTransfer.Enabled = False
            cbDwAttdInterface.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Billing)) = False Then
            cbBI.Enabled = False
            cbBINote.Enabled = False
            cbBillParty.Enabled = False
            cbDtBI.Enabled = False
            cbDtDwBI.Enabled = False
            cbBIMthEnd.Enabled = False
            cbADTBI.Enabled = False
            cbBIInvoice.Enabled = False
            cbBIReceipt.Enabled = False
            cbBIJournal.Enabled = False
            cbBICreditNote.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Production)) = False Then
            cbPD.Enabled = False
            cbEstProd.Enabled = False
            cbPOMProd.Enabled = False
            cbDtDwPD.Enabled = False
            cbPDMthEnd.Enabled = False
            cbYearPlantYield.Enabled = False
            cbPOMStorage.Enabled = False
            cbPOMStat.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.GeneralLedger)) = False Then
            cbGL.Enabled = False
            cbAccCls.Enabled = False
            cbAct.Enabled = False
            cbVehExp.Enabled = False
            cbVeh.Enabled = False
            cbBlkGrp.Enabled = False
            cbExp.Enabled = False
            cbAccount.Enabled = False
            cbEntrySetup.Enabled = False
            cbBalSheetSetup.Enabled = False
            cbProfLossSetup.Enabled = False
            cbJrn.Enabled = False
            cbJrnAdj.Enabled = False
            cbVehUsg.Enabled = False
            cbDtGL.Enabled = False
            cbDtUp.Enabled = False
            cbGLGCDist.Enabled = False
            cbGLJrnMthEnd.Enabled = False
            cbGLMthEnd.Enabled = False
            cbADTGL.Enabled = False
            cbRunHour.Enabled = False
            cbAccClsGrp.Enabled = False
            cbActGrp.Enabled = False
            cbSubAct.Enabled = False
            cbVehExpGrp.Enabled = False
            cbVehType.Enabled = False
            cbBlk.Enabled = False
            cbSubBlk.Enabled = False
            cbAccountGrp.Enabled = False
            cbPosting.Enabled = False
            cbGLCOGS.Enabled = False
            cbFSSetup.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Reconciliation)) = False Then
            cbRC.Enabled = False
            cbRCDA.Enabled = False
            cbRCJrn.Enabled = False
            cbReadInterRC.Enabled = False
            cbSendInterRC.Enabled = False
            cbDtRC.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Budgeting)) = False Then
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Workflow)) = False Then
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.MillWeighing)) = False Then
            cbWM.Enabled = False
            cbWMTransport.Enabled = False
            cbWMTicket.Enabled = False
            cbWMFFBAssessment.Enabled = False
            cbWMDataTransfer.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.MillProduction)) = False Then
            cbPM.Enabled = False
            cbPMMasterSetup.Enabled = False
            cbPMMill.Enabled = False
            cbPMDailyProd.Enabled = False
            cbPMCPOStore.Enabled = False
            cbPMPKStore.Enabled = False
            cbPMOilLoss.Enabled = False
            cbPMOilQuality.Enabled = False
            cbPMKernelQuality.Enabled = False
            cbPMProdKernel.Enabled = False
            cbPMDispKernel.Enabled = False
            cbPMWater.Enabled = False
            cbPMNutFibre.Enabled = False
            cbPMDayEnd.Enabled = False
            cbPMMthEnd.Enabled = False

            cbPMKernelLoss.Enabled = False
            cbPMWastedWaterQuality.Enabled = False
            cbPMVolConvMaster.Enabled = False
            cbPMAvgCapConvMaster.Enabled = False
            cbPMCPOPropertyMaster.Enabled = False
            cbPMStorageTypeMaster.Enabled = False
            cbPMStorageAreaMaster.Enabled = False
            cbPMProcessingLineMaster.Enabled = False
            cbPMMachineMaster.Enabled = False
            cbPMAcceptableOilQuality.Enabled = False
            cbPMAcceptableKernelQuality.Enabled = False
            cbPMTestSample.Enabled = False
            cbPMHarvestingInterval.Enabled = False
            cbPMMachineCriteria.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.MillContract)) = False Then
            cbCM.Enabled = False
            cbCMMasterSetup.Enabled = False
            cbCMContractReg.Enabled = False
            cbCMContractMatch.Enabled = False
            cbCMGenDNCN.Enabled = False
            cbCMDataTransfer.Enabled = False
            cbCMExchangeRate.Enabled = False
            cbCMContractQuality.Enabled = False
            cbCMClaimQuality.Enabled = False

            cbCMContractDOReg.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Nursery)) = False Then
            cbNU.Enabled = False
            cbNUMasterSetup.Enabled = False
            cbNUWorkAccDist.Enabled = False
            cbNUSeedRcv.Enabled = False
            cbNUSeedPlant.Enabled = False
            cbNUDblTurn.Enabled = False
            cbNUTransplanting.Enabled = False
            cbNUDispatch.Enabled = False
            cbNUCulling.Enabled = False
            cbDtNu.Enabled = False
            cbNUMonthEnd.Enabled = False
            cbADTNU.Enabled = False
            cbNUCullType.Enabled = False

            cbNUMasterItem.Enabled = False
            cbNUItem.Enabled = False
            cbNUSeedIssue.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.FixAsset)) = False Then
            cbFA.Enabled = False
            cbFAClassSetup.Enabled = False
            cbFAGroupSetup.Enabled = False
            cbFARegSetup.Enabled = False
            cbFAPermissionSetup.Enabled = False
            cbFAAddition.Enabled = False
            cbFADepreciation.Enabled = False
            cbFADisposal.Enabled = False
            cbFAWriteOff.Enabled = False
            cbFAMasterSetup.Enabled = False
            cbFAItemSetup.Enabled = False
            cbDtDwFA.Enabled = False
            cbDtUpFA.Enabled = False
            cbFARegLine.Enabled = False
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.CashAndBank)) = False Then
            cbCB.Enabled = False
            cbCBPayment.Enabled = False
            cbCBReceipt.Enabled = False
            cbCBDeposit.Enabled = False
            cbCBInterAdj.Enabled = False
            cbCBWithdrawal.Enabled = False
            cbCBMthEnd.Enabled = False
            cbCBCashFlow.Enabled = False
            cbCBCashBank.Enabled = False
        End If
    End Sub

    Sub onLoad_ControlDelete()
        Dim objUserDs As New DataSet()
        Dim strOpCd_User As String = "PWSYSTEM_CLSCONFIG_BLOCK_COUNT_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strOpCd_User, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    objUserDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSSETTING_ACCOUNT_COUNT_GET&errmesg=" & Exp.ToString & "&redirect=system/config/syssetting.aspx")
        End Try

        If objUserDs.Tables(0).Rows(0).Item("RecordCount") > 0 Then
            DeleteBtn.Visible = False
        Else
            DeleteBtn.Visible = True
        End If

        objUserDs = Nothing
    End Sub

    Sub onLoad_Display()
        Dim objCompDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strOpCode_GetComp As String
        Dim strOpCode_GetLoc As String
        Dim strLocation As String = ""
        Dim strParam As String

        gblCompCode = IIf(Request.Form("CompCode") = "", Request.QueryString("CompCode"), Request.Form("CompCode"))
        gblLocCode = IIf(Request.Form("ddlLocation") = "", Request.QueryString("LocCode"), Request.Form("ddlLocation"))

        hidCompCode.Value = gblCompCode
        hidLocCode.Value = gblLocCode

        strOpCode_GetComp = "ADMIN_CLSCOMP_COMPANY_DETAILS_GET"
        Try
            intErrNo = objAdminComp.mtdGetComp(strOpCode_GetComp, _
                                               gblCompCode, _
                                               objCompDs, _
                                               True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSLOC_GET_COMP&errmesg=" & lblErrMesage.Text & "&redirect=system/config/syssetting.aspx")
        End Try
        lblCompany.Text = objCompDs.Tables(0).Rows(0).Item("CompName").Trim()
        hidCompName.Value = objCompDs.Tables(0).Rows(0).Item("CompName").Trim()

        If gblLocCode = "" Then
            DeleteBtn.Visible = False
            strParam = Convert.ToString(objAdminLoc.EnumLocStatus.Active) & "|" & gblCompCode & "|" & strUserId
            strOpCode_GetLoc = "ADMIN_CLSLOC_SYSCFGLOCLIST_GET"
            Try
                intErrNo = objAdminLoc.mtdGetLocList(strOpCode_GetLoc, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     objLocDS, _
                                                     strParam)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSLOC_GET_LOCLIST&errmesg=" & lblErrMesage.Text & "&redirect=system/config/syssetting.aspx")
            End Try

            If objLocDS.Tables(0).Rows.Count = 0 Then
                lblNoLocError.Visible = True
            Else
                For intCnt = 0 To objLocDS.Tables(0).Rows.Count - 1
                    objLocDS.Tables(0).Rows(intCnt).Item("Description") = objLocDS.Tables(0).Rows(intCnt).Item("LocCode") & " (" & objLocDS.Tables(0).Rows(intCnt).Item("Description") & ")"
                Next intCnt

                ddlLocation.DataSource = objLocDS.Tables(0)
                ddlLocation.DataTextField = "Description"
                ddlLocation.DataValueField = "LocCode"
                ddlLocation.DataBind()
            End If
            onLoad_LicChk()
        Else
            DeleteBtn.Visible = True
            DeleteBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            strOpCode_GetLoc = "ADMIN_CLSLOC_LOCATION_DETAILS_GET"
            strParam = gblLocCode & "|"
            Try
                intErrNo = objAdminLoc.mtdGetLocDetail(strOpCode_GetLoc, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        objLocDS, _
                                                        strParam)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSLOC_GET_LOCDETAIL&errmesg=" & lblErrMesage.Text & "&redirect=system/config/syssetting.aspx")
            End Try

            ddlLocation.DataSource = objLocDS.Tables(0)
            ddlLocation.DataTextField = "Description"
            ddlLocation.DataValueField = "LocCode"
            ddlLocation.DataBind()

            Load_SaveData()
        End If
    End Sub


    Sub Load_SaveData()
        Dim strLocation As String
        Dim strOpCode_GetSysLocDetails As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim objSysLocDetDs As New Data.DataSet()
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intINAR As Integer
        Dim intCTAR As Integer
        Dim intWSAR As Integer
        Dim intPUAR As Integer
        Dim intAPAR As Integer
        Dim intHRAR As Long
        Dim intPRAR As Long
        Dim intBIAR As Integer
        Dim intPDAR As Integer
        Dim intGLAR As Long
        Dim intModule As Integer
        Dim intAUTO As Integer
        Dim intWMAR As Integer
        Dim intPMAR As Integer
        Dim intCMAR As Integer
        Dim intNUAR As Integer
        Dim intFAAR As Integer
        Dim intCBAR As Integer

        strParam = gblCompCode & "|" & gblLocCode & "|" & strUserId

        Try
            intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCode_GetSysLocDetails, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                objSysLocDetDs, _
                                                strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSLOC_GET_SYSLOC&errmesg=" & lblErrMesage.Text & "&redirect=system/config/syssetting.aspx")
        End Try

        objSysLocDetDs.Tables(0).Rows(0).Item("DocRetain") = Trim(objSysLocDetDs.Tables(0).Rows(0).Item("DocRetain"))
        intADAR = objSysLocDetDs.Tables(0).Rows(0).Item("ADAR")
        lblADARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("ADAR")
        intINAR = objSysLocDetDs.Tables(0).Rows(0).Item("INAR")
        lblINARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("INAR")
        intCTAR = objSysLocDetDs.Tables(0).Rows(0).Item("CTAR")
        lblCTARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("CTAR")
        intWSAR = objSysLocDetDs.Tables(0).Rows(0).Item("WSAR")
        lblWSARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("WSAR")
        intPUAR = objSysLocDetDs.Tables(0).Rows(0).Item("PUAR")
        lblPUARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("PUAR")
        intAPAR = objSysLocDetDs.Tables(0).Rows(0).Item("APAR")
        lblAPARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("APAR")
        intHRAR = objSysLocDetDs.Tables(0).Rows(0).Item("HRAR")
        lblHRARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("HRAR")
        intPRAR = objSysLocDetDs.Tables(0).Rows(0).Item("PRAR")
        lblPRARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("PRAR")
        intBIAR = objSysLocDetDs.Tables(0).Rows(0).Item("BIAR")
        lblBIARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("BIAR")
        intPDAR = objSysLocDetDs.Tables(0).Rows(0).Item("PDAR")
        lblPDARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("PDAR")
        intGLAR = objSysLocDetDs.Tables(0).Rows(0).Item("GLAR")
        lblGLARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("GLAR")
        intModule = objSysLocDetDs.Tables(0).Rows(0).Item("ModuleActivate")
        intAUTO = objSysLocDetDs.Tables(0).Rows(0).Item("DataTransferInd")

        txtDoc.Text = objSysLocDetDs.Tables(0).Rows(0).Item("DocRetain")

        intWMAR = objSysLocDetDs.Tables(0).Rows(0).Item("WMAR")
        lblWMARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("WMAR")
        intPMAR = objSysLocDetDs.Tables(0).Rows(0).Item("PMAR")
        lblPMARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("PMAR")
        intCMAR = objSysLocDetDs.Tables(0).Rows(0).Item("CMAR")
        lblCMARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("CMAR")
        intNUAR = objSysLocDetDs.Tables(0).Rows(0).Item("NUAR")
        lblNUARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("NUAR")
        intFAAR = objSysLocDetDs.Tables(0).Rows(0).Item("FAAR")
        lblFAARExt.Text = objSysLocDetDs.Tables(0).Rows(0).Item("FAAR")

        intCBAR = objSysLocDetDs.Tables(0).Rows(0).Item("CBAR")
        lblCBARExt.text = objSysLocDetDs.Tables(0).Rows(0).Item("CBAR")

        strLocType = Trim(objSysLocDetDs.Tables(0).Rows(0).Item("LocType"))

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Inventory)) = False Then
            cbIN.Enabled = False
            cbINProdMaster.Enabled = False
            cbINItem.Enabled = False
            cbINDirChrg.Enabled = False
            cbMiscItem.Enabled = False
            cbINPR.Enabled = False
            cbStkRtnAdv.Enabled = False
            cbStkTransfer.Enabled = False
            cbStkIsu.Enabled = False
            cbStkRcv.Enabled = False
            cbStkRtn.Enabled = False
            cbStkAdj.Enabled = False
            cbFuelIsu.Enabled = False
            cbDtInv.Enabled = False
            cbDtDwIN.Enabled = False
            cbINMthEnd.Enabled = False
            cbADTIN.Enabled = False
            cbItemToMachine.Enabled = False
            cbStkTransferInternal.Enabled = False
            cbINProdBrand.Enabled = False
            cbINProdModel.Enabled = False
            cbINProdCategory.Enabled = False
            cbINProdMaterial.Enabled = False
            cbINStockAnalysis.Enabled = False
            cbINItemMaster.Enabled = False
        Else
            If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Inventory), intModule) = True Then
                cbIN.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductSetup), intINAR) = True Then
                cbINProdMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItem), intINAR) = True Then
                cbINItem.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDirectChargeItem), intINAR) = True Then
                cbINDirChrg.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMiscItem), intINAR) = True Then
                cbMiscItem.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest), intINAR) = True Then
                cbINPR.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturnAdvice), intINAR) = True Then
                cbStkRtnAdv.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer), intINAR) = True Then
                cbStkTransfer.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue), intINAR) = True Then
                cbStkIsu.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReceive), intINAR) = True Then
                cbStkRcv.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturn), intINAR) = True Then
                cbStkRtn.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockAdjustment), intINAR) = True Then
                cbStkAdj.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INFuelIssue), intINAR) = True Then
                cbFuelIsu.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDataTransfer), intINAR) = True Then
                cbDtInv.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDownload), intINAR) = True Then
                cbDtDwIN.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMonthEnd), intINAR) = True Then
                cbINMthEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INItemToMachine), intINAR) = True Then
                cbItemToMachine.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransferInternal), intINAR) = True Then
                cbStkTransferInternal.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductBrand), intINAR) = True Then
                cbINProdBrand.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductModel), intINAR) = True Then
                cbINProdModel.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductCategory), intINAR) = True Then
                cbINProdCategory.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductMaterial), intINAR) = True Then
                cbINProdMaterial.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INAnalisisStok), intINAR) = True Then
                cbINStockAnalysis.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItemMaster), intINAR) = True Then
                cbINItemMaster.Checked = True
            End If
        End If


        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Canteen)) = False Then
            cbCT.Enabled = False
            cbCTMaster.Enabled = False
            cbCTItem.Enabled = False
            cbCTPR.Enabled = False
            cbCTRcv.Enabled = False
            cbCTRtnAdv.Enabled = False
            cbCTIsu.Enabled = False
            cbCTRtn.Enabled = False
            cbCTAdj.Enabled = False
            cbCTTransfer.Enabled = False
            cbDtCT.Enabled = False
            cbDtDwCT.Enabled = False
            cbCTMthEnd.Enabled = False
            cbADTCT.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Canteen), intModule) = True) Then
                cbCT.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster), intCTAR) = True Then
                cbCTMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTItem), intCTAR) = True Then
                cbCTItem.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTPurchaseRequest), intCTAR) = True Then
                cbCTPR.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReceive), intCTAR) = True Then
                cbCTRcv.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturnAdvice), intCTAR) = True Then
                cbCTRtnAdv.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTIssue), intCTAR) = True Then
                cbCTIsu.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturn), intCTAR) = True Then
                cbCTRtn.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTAdjustment), intCTAR) = True Then
                cbCTAdj.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTTransfer), intCTAR) = True Then
                cbCTTransfer.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTDataTransfer), intCTAR) = True Then
                cbDtCT.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTDownload), intCTAR) = True Then
                cbDtDwCT.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMonthEnd), intCTAR) = True Then
                cbCTMthEnd.Checked = True
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Workshop)) = False Then
            cbWS.Enabled = False
            cbWSProdMaster.Enabled = False
            cbWSWorkMaster.Enabled = False
            cbWSItem.Enabled = False
            cbWSPart.Enabled = False
            cbWSDirChrg.Enabled = False
            cbWSJob.Enabled = False
            cbWSMechHr.Enabled = False
            cbWSDN.Enabled = False
            cbDtWS.Enabled = False
            cbDtDwWS.Enabled = False
            cbWSMthEnd.Enabled = False
            cbADTWS.Enabled = False
            cbWSProdBrand.Enabled = False
            cbWSProdModel.Enabled = False
            cbWSProdCategory.Enabled = False
            cbWSProdMaterial.Enabled = False
            cbWSStockAnalysis.Enabled = False
            cbWSItemMaster.Enabled = False
            cbWSWorkshopService.Enabled = False

            cbWSDirChrgMaster.Enabled = False
            cbWSMillProcDitr.Enabled = False
            cbWSCalMachine.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Workshop), intModule) = True) Then
                cbWS.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaster), intWSAR) = True Then
                cbWSProdMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkMaster), intWSAR) = True Then
                cbWSWorkMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItem), intWSAR) = True Then
                cbWSItem.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemPart), intWSAR) = True Then
                cbWSPart.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItem), intWSAR) = True Then
                cbWSDirChrg.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration), intWSAR) = True Then
                cbWSJob.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMechanicHour), intWSAR) = True Then
                cbWSMechHr.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDebitNote), intWSAR) = True Then
                cbWSDN.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDataTransfer), intWSAR) = True Then
                cbDtWS.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDownload), intWSAR) = True Then
                cbDtDwWS.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMonthEnd), intWSAR) = True Then
                cbWSMthEnd.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductBrand), intWSAR) = True Then
                cbWSProdBrand.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductModel), intWSAR) = True Then
                cbWSProdModel.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductCategory), intWSAR) = True Then
                cbWSProdCategory.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaterial), intWSAR) = True Then
                cbWSProdMaterial.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSAnalisisStok), intWSAR) = True Then
                cbWSStockAnalysis.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemMaster), intWSAR) = True Then
                cbWSItemMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkshopService), intWSAR) = True Then
                cbWSWorkshopService.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItemMaster), intWSAR) = True Then
                cbWSDirChrgMaster.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMillProcDistr), intWSAR) = True Then
                cbWSMillProcDitr.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSCalMachine), intWSAR) = True Then
                cbWSCalMachine.Checked = True
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Purchasing)) = False Then
            cbPU.Enabled = False
            cbPUSupp.Enabled = False
            cbPUPelimpahan.Enabled = False
            cbPURPH.Enabled = False
            cbPUPO.Enabled = False
            cbPUGoodsRcv.Enabled = False
            cbPUGRN.Enabled = False
            cbPUDA.Enabled = False
            cbDtPU.Enabled = False
            cbDtDwPU.Enabled = False
            cbPUMthEnd.Enabled = False
            cbADTPU.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Purchasing), intModule) = True) Then
                cbPU.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier), intPUAR) = True Then
                cbPUSupp.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPelimpahan), intPUAR) = True Then
                cbPUPelimpahan.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PURPH), intPUAR) = True Then
                cbPURPH.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder), intPUAR) = True Then
                cbPUPO.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReceive), intPUAR) = True Then
                cbPUGoodsRcv.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReturnNote), intPUAR) = True Then
                cbPUGRN.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDispatchAdvice), intPUAR) = True Then
                cbPUDA.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDataTransfer), intPUAR) = True Then
                cbDtPU.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDownload), intPUAR) = True Then
                cbDtDwPU.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUMonthEnd), intPUAR) = True Then
                cbPUMthEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPelimpahan), intPUAR) = True Then
                cbPUPelimpahan.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PURPH), intPUAR) = True Then
                cbPURPH.Checked = True
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.AccountPayable)) = False Then
            cbAP.Enabled = False
            cbAPInvoice.Enabled = False
            cbAPDN.Enabled = False
            cbAPCN.Enabled = False
            cbAPCrtJrn.Enabled = False
            cbAPPay.Enabled = False
            cbDtDwAP.Enabled = False
            cbAPMthEnd.Enabled = False
            cbADTAP.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.AccountPayable), intModule) = True) Then
                cbAP.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APInvoiceReceive), intAPAR) = True Then
                cbAPInvoice.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APDebitNote), intAPAR) = True Then
                cbAPDN.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditNote), intAPAR) = True Then
                cbAPCN.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditorJournal), intAPAR) = True Then
                cbAPCrtJrn.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APPayment), intAPAR) = True Then
                cbAPPay.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APDownload), intAPAR) = True Then
                cbDtDwAP.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APMonthEnd), intAPAR) = True Then
                cbAPMthEnd.Checked = True
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.HumanResource)) = False Then
            cbHR.Enabled = False
            cbHRDepartment.Enabled = False
            cbHRCompany.Enabled = False
            cbHRFunc.Enabled = False
            cbHRSkill.Enabled = False
            cbHREval.Enabled = False
            cbHRBank.Enabled = False
            cbHRHoliday.Enabled = False
            cbHRCP.Enabled = False
            cbHREmpDet.Enabled = False
            cbHREmpPR.Enabled = False
            cbHREmpEmploy.Enabled = False
            cbHRSat.Enabled = False
            cbHREmpFam.Enabled = False
            cbHREmpQlf.Enabled = False
            cbHREmpSkill.Enabled = False
            cbHRContSuper.Enabled = False
            cbGenEmpCode.Enabled = False
            cbDtHR.Enabled = False
            cbHRNationality.Enabled = False
            cbHRPosition.Enabled = False
            cbHRLevel.Enabled = False
            cbHRReligion.Enabled = False
            cbHRICType.Enabled = False
            cbHRRace.Enabled = False
            cbHRQualification.Enabled = False
            cbHRSubject.Enabled = False
            cbHRCPCode.Enabled = False
            cbHRSalScheme.Enabled = False
            cbHRSalGrade.Enabled = False
            cbHRShift.Enabled = False
            cbHRGang.Enabled = False
            cbHRBankFormat.Enabled = False
            cbHRJamsostek.Enabled = False
            cbHRTaxBranch.Enabled = False
            cbHRTax.Enabled = False
            cbHRPublicHoliday.Enabled = False
            cbHRPOH.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.HumanResource), intModule) = True) Then
                cbHR.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDepartment), intHRAR) = True Then
                cbHRDepartment.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = True Then
                cbHRCompany.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRFunction), intHRAR) = True Then
                cbHRFunc.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSkill), intHRAR) = True Then
                cbHRSkill.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREvaluation), intHRAR) = True Then
                cbHREval.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBank), intHRAR) = True Then
                cbHRBank.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRHoliday), intHRAR) = True Then
                cbHRHoliday.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCareerProgress), intHRAR) = True Then
                cbHRCP.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intHRAR) = True Then
                cbHREmpDet.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeePayroll), intHRAR) = True Then
                cbHREmpPR.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeEmployement), intHRAR) = True Then
                cbHREmpEmploy.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeStatutory), intHRAR) = True Then
                cbHRSat.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeFamily), intHRAR) = True Then
                cbHREmpFam.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeQualification), intHRAR) = True Then
                cbHREmpQlf.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeSkill), intHRAR) = True Then
                cbHREmpSkill.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRContractorSupervision), intHRAR) = True Then
                cbHRContSuper.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGenerateEmpCode), intHRAR) = True Then
                cbGenEmpCode.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDataTransfer), intHRAR) = True Then
                cbDtHR.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRNationality), intHRAR) = True Then
                cbHRNationality.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPosition), intHRAR) = True Then
                cbHRPosition.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRLevel), intHRAR) = True Then
                cbHRLevel.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRReligion), intHRAR) = True Then
                cbHRReligion.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRICType), intHRAR) = True Then
                cbHRICType.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRRace), intHRAR) = True Then
                cbHRRace.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRQualification), intHRAR) = True Then
                cbHRQualification.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSubject), intHRAR) = True Then
                cbHRSubject.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCPCode), intHRAR) = True Then
                cbHRCPCode.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalScheme), intHRAR) = True Then
                cbHRSalScheme.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalGrade), intHRAR) = True Then
                cbHRSalGrade.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRShift), intHRAR) = True Then
                cbHRShift.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGang), intHRAR) = True Then
                cbHRGang.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBankFormat), intHRAR) = True Then
                cbHRBankFormat.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRJamsostek), intHRAR) = True Then
                cbHRJamsostek.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTaxBranch), intHRAR) = True Then
                cbHRTaxBranch.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTax), intHRAR) = True Then
                cbHRTax.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPublicHoliday), intHRAR) = True Then
                cbHRPublicHoliday.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPointHired), intHRAR) = True Then
                cbHRPOH.Checked = True
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Payroll)) = False Then
            cbPR.Enabled = False
            cbPRAD.Enabled = False
            cbPRSal.Enabled = False
            cbPRContract.Enabled = False
            cbPRRice.Enabled = False
            cbPRAttdTrx.Enabled = False
            cbPRTripTrx.Enabled = False
            cbPRRatePay.Enabled = False
            cbPRADTrx.Enabled = False
            cbPRContCheckroll.Enabled = False
            cbDtPR.Enabled = False
            cbDwPR.Enabled = False
            cbPRMthEnd.Enabled = False
            cbPRYearEnd.Enabled = False
            cbADTPR.Enabled = False
            cbPRWorkPerformance.Enabled = False
            cbPREmployeeEvaluation.Enabled = False
            cbPRStandardEvaluation.Enabled = False
            cbPRSalaryIncrease.Enabled = False
            cbPRTranInc.Enabled = False
            cbPRADGroup.Enabled = False
            cbPRDenda.Enabled = False
            cbPRHarvInc.Enabled = False
            cbPRLoad.Enabled = False
            cbPRRoute.Enabled = False
            cbPRMedical.Enabled = False
            cbPRAirBus.Enabled = False
            cbPRMaternity.Enabled = False
            cbPRPensiun.Enabled = False
            cbPRRelocation.Enabled = False
            cbPRIncentive.Enabled = False
            cbPRContractPay.Enabled = False
            cbPRWagesPay.Enabled = False
            cbDwPRWages.Enabled = False
            cbDwPRBankAuto.Enabled = False

            cbPRPaySetup.Enabled = False
            cbPRDailyAttd.Enabled = False
            cbPRHarvAttd.Enabled = False
            cbPRWeekly.Enabled = False
            cbPRMthRice.Enabled = False
            cbPRMthRapel.Enabled = False
            cbPRMthBonus.Enabled = False
            cbPRMthTHR.Enabled = False
            cbPRMthDaily.Enabled = False
            cbPRMthPayroll.Enabled = False
            cbPRMthTransfer.Enabled = False
            cbDwAttdInterface.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Payroll), intModule) = True) Then
                cbPR.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAllowanceDeduction), intPRAR) = True Then
                cbPRAD.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalary), intPRAR) = True Then
                cbPRSal.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRContract), intPRAR) = True Then
                cbPRContract.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRice), intPRAR) = True Then
                cbPRRice.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAttendance), intPRAR) = True Then
                cbPRAttdTrx.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxTrip), intPRAR) = True Then
                cbPRTripTrx.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxRatePayment), intPRAR) = True Then
                cbPRRatePay.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAllowanceDeduction), intPRAR) = True Then
                cbPRADTrx.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractorCheckroll), intPRAR) = True Then
                cbPRContCheckroll.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDataTransfer), intPRAR) = True Then
                cbDtPR.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownload), intPRAR) = True Then
                cbDwPR.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMonthEnd), intPRAR) = True Then
                cbPRMthEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRYearEnd), intPRAR) = True Then
                cbPRYearEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = True Then
                cbPRWorkPerformance.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PREmployeeEvaluation), intPRAR) = True Then
                cbPREmployeeEvaluation.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRStandardEvaluation), intPRAR) = True Then
                cbPRStandardEvaluation.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalaryIncrease), intPRAR) = True Then
                cbPRSalaryIncrease.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTransportIncentive), intPRAR) = True Then
                cbPRTranInc.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWPContractor), intPRAR) = True Then
                cbPRWPContractor.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRADGroup), intPRAR) = True Then
                cbPRADGroup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDenda), intPRAR) = True Then
                cbPRDenda.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvInc), intPRAR) = True Then
                cbPRHarvInc.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRLoad), intPRAR) = True Then
                cbPRLoad.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRoute), intPRAR) = True Then
                cbPRRoute.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMedical), intPRAR) = True Then
                cbPRMedical.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAirBus), intPRAR) = True Then
                cbPRAirBus.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMaternity), intPRAR) = True Then
                cbPRMaternity.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPensiun), intPRAR) = True Then
                cbPRPensiun.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRelocation), intPRAR) = True Then
                cbPRRelocation.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPremi), intPRAR) = True Then
                cbPRIncentive.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractPayment), intPRAR) = True Then
                cbPRContractPay.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWagesPayment), intPRAR) = True Then
                cbPRWagesPay.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadWages), intPRAR) = True Then
                cbDwPRWages.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadBankAuto), intPRAR) = True Then
                cbDwPRBankAuto.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = True Then
                cbPRPaySetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDailyAttd), intPRAR) = True Then
                cbPRDailyAttd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvAttd), intPRAR) = True Then
                cbPRHarvAttd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRWeekly), intPRAR) = True Then
                cbPRWeekly.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRice), intPRAR) = True Then
                cbPRMthRice.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRapel), intPRAR) = True Then
                cbPRMthRapel.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthBonus), intPRAR) = True Then
                cbPRMthBonus.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTHR), intPRAR) = True Then
                cbPRMthTHR.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthDaily), intPRAR) = True Then
                cbPRMthDaily.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = True Then
                cbPRMthPayroll.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTransfer), intPRAR) = True Then
                cbPRMthTransfer.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDwAttdInterface), intPRAR) = True Then
                cbDwAttdInterface.Checked = True
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Billing)) = False Then
            cbBI.Enabled = False
            cbBINote.Enabled = False
            cbBillParty.Enabled = False
            cbDtBI.Enabled = False
            cbDtDwBI.Enabled = False
            cbBIMthEnd.Enabled = False
            cbADTBI.Enabled = False
            cbBIInvoice.Enabled = False
            cbBIReceipt.Enabled = False
            cbBIJournal.Enabled = False
            cbBICreditNote.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Billing), intModule) = True) Then
                cbBI.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIBillParty), intBIAR) = True Then
                cbBillParty.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BINote), intBIAR) = True Then
                cbBINote.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIDataTransfer), intBIAR) = True Then
                cbDtBI.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIDownload), intBIAR) = True Then
                cbDtDwBI.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIMonthEnd), intBIAR) = True Then
                cbBIMthEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIInvoice), intBIAR) = True Then
                cbBIInvoice.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIReceipt), intBIAR) = True Then
                cbBIReceipt.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIJournal), intBIAR) = True Then
                cbBIJournal.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BICreditNote), intBIAR) = True Then
                cbBICreditNote.Checked = True
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Production)) = False Then
            cbPD.Enabled = False
            cbEstProd.Enabled = False
            cbPOMProd.Enabled = False
            cbDtDwPD.Enabled = False
            cbPDMthEnd.Enabled = False
            cbYearPlantYield.Enabled = False
            cbPOMStorage.Enabled = False
            cbPOMStat.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Production), intModule) = True) Then
                cbPD.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDEstateProduction), intPDAR) = True Then
                cbEstProd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMProduction), intPDAR) = True Then
                cbPOMProd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDDownload), intPDAR) = True Then
                cbDtDwPD.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMonthEnd), intPDAR) = True Then
                cbPDMthEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDYearOfPlantYield), intPDAR) = True Then
                cbYearPlantYield.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStorage), intPDAR) = True Then
                cbPOMStorage.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStatistics), intPDAR) = True Then
                cbPOMStat.Checked = True
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.GeneralLedger)) = False Then
            cbGL.Enabled = False
            cbAccCls.Enabled = False
            cbAct.Enabled = False
            cbVehExp.Enabled = False
            cbVeh.Enabled = False
            cbBlkGrp.Enabled = False
            cbExp.Enabled = False
            cbAccount.Enabled = False
            cbEntrySetup.Enabled = False
            cbBalSheetSetup.Enabled = False
            cbProfLossSetup.Enabled = False
            cbJrn.Enabled = False
            cbJrnAdj.Enabled = False
            cbVehUsg.Enabled = False
            cbDtGL.Enabled = False
            cbDtUp.Enabled = False
            cbGLGCDist.Enabled = False
            cbGLJrnMthEnd.Enabled = False
            cbGLMthEnd.Enabled = False
            cbADTGL.Enabled = False
            cbRunHour.Enabled = False
            cbAccClsGrp.Enabled = False
            cbActGrp.Enabled = False
            cbSubAct.Enabled = False
            cbVehExpGrp.Enabled = False
            cbVehType.Enabled = False
            cbBlk.Enabled = False
            cbSubBlk.Enabled = False
            cbAccountGrp.Enabled = False
            cbPosting.Enabled = False
            cbGLCOGS.Enabled = False
            cbFSSetup.Enabled = False

        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.GeneralLedger), intModule) = True) Then
                cbGL.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccCls), intGLAR) = True Then
                cbAccCls.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivity), intGLAR) = True Then
                cbAct.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpense), intGLAR) = True Then
                cbVehExp.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intGLAR) = True Then
                cbVeh.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlock), intGLAR) = True Then
                cbBlkGrp.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLExpense), intGLAR) = True Then
                cbExp.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccount), intGLAR) = True Then
                cbAccount.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLEntrySetup), intGLAR) = True Then
                cbEntrySetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBalSheetSetup), intGLAR) = True Then
                cbBalSheetSetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLProfLossSetup), intGLAR) = True Then
                cbProfLossSetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal), intGLAR) = True Then
                cbJrn.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournalAdj), intGLAR) = True Then
                cbJrnAdj.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleUsage), intGLAR) = True Then
                cbVehUsg.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLDataTransfer), intGLAR) = True Then
                cbDtGL.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLUpload), intGLAR) = True Then
                cbDtUp.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMthEndGCDist), intGLAR) = True Then
                cbGLGCDist.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJrnMonthEnd), intGLAR) = True Then
                cbGLJrnMthEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = True Then
                cbGLMthEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLRunHour), intGLAR) = True Then
                cbRunHour.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccClsGrp), intGLAR) = True Then
                cbAccClsGrp.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivityGrp), intGLAR) = True Then
                cbActGrp.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubActivity), intGLAR) = True Then
                cbSubAct.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpenseGrp), intGLAR) = True Then
                cbVehExpGrp.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubBlk), intGLAR) = True Then
                cbSubBlk.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccountGrp), intGLAR) = True Then
                cbAccountGrp.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLPosting), intGLAR) = True Then
                cbPosting.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleType), intGLAR) = True Then
                cbVehType.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlk), intGLAR) = True Then
                cbBlk.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLCOGS), intGLAR) = True Then
                cbGLCOGS.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLFSSETUP), intGLAR) = True Then
                cbFSSetup.Checked = True
            End If

        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Reconciliation)) = False Then
            cbRC.Enabled = False
            cbRCDA.Enabled = False
            cbRCJrn.Enabled = False
            cbReadInterRC.Enabled = False
            cbSendInterRC.Enabled = False
            cbDtRC.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Reconciliation), intModule) = True) Then
                cbRC.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDispatchAdvice), intADAR) = True Then
                cbRCDA.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCJournal), intADAR) = True Then
                cbRCJrn.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCReadInterface), intADAR) = True Then
                cbReadInterRC.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCSendInterface), intADAR) = True Then
                cbSendInterRC.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDataTransfer), intADAR) = True Then
                cbDtRC.Checked = True
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.MillWeighing)) = False Then
            cbWM.Enabled = False
            cbWMTransport.Enabled = False
            cbWMTicket.Enabled = False
            cbWMFFBAssessment.Enabled = False
            cbWMDataTransfer.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillWeighing), intModule) = True) Then
                cbWM.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMTransporterSetup), intWMAR) = True Then
                cbWMTransport.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket), intWMAR) = True Then
                cbWMTicket.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMFFBAssessment), intWMAR) = True Then
                cbWMFFBAssessment.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMDataTransfer), intWMAR) = True Then
                cbWMDataTransfer.Checked = True
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.MillProduction)) = False Then
            cbPM.Enabled = False
            cbPMMasterSetup.Enabled = False
            cbPMMill.Enabled = False
            cbPMDailyProd.Enabled = False
            cbPMCPOStore.Enabled = False
            cbPMPKStore.Enabled = False
            cbPMOilLoss.Enabled = False
            cbPMOilQuality.Enabled = False
            cbPMKernelQuality.Enabled = False
            cbPMProdKernel.Enabled = False
            cbPMDispKernel.Enabled = False
            cbPMWater.Enabled = False
            cbPMNutFibre.Enabled = False
            cbPMDayEnd.Enabled = False
            cbPMMthEnd.Enabled = False

            cbPMKernelLoss.Enabled = False
            cbPMWastedWaterQuality.Enabled = False
            cbPMVolConvMaster.Enabled = False
            cbPMAvgCapConvMaster.Enabled = False
            cbPMCPOPropertyMaster.Enabled = False
            cbPMStorageTypeMaster.Enabled = False
            cbPMStorageAreaMaster.Enabled = False
            cbPMProcessingLineMaster.Enabled = False
            cbPMMachineMaster.Enabled = False
            cbPMAcceptableOilQuality.Enabled = False
            cbPMAcceptableKernelQuality.Enabled = False
            cbPMTestSample.Enabled = False
            cbPMHarvestingInterval.Enabled = False
            cbPMMachineCriteria.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillProduction), intModule) = True) Then
                cbPM.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMasterSetup), intPMAR) = True Then
                cbPMMasterSetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMill), intPMAR) = True Then
                cbPMMill.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction), intPMAR) = True Then
                cbPMDailyProd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOStorage), intPMAR) = True Then
                cbPMCPOStore.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMPKStorage), intPMAR) = True Then
                cbPMPKStore.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilLoss), intPMAR) = True Then
                cbPMOilLoss.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilQuality), intPMAR) = True Then
                cbPMOilQuality.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelQuality), intPMAR) = True Then
                cbPMKernelQuality.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProducedKernel), intPMAR) = True Then
                cbPMProdKernel.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDispatchedKernel), intPMAR) = True Then
                cbPMDispKernel.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWaterQuality), intPMAR) = True Then
                cbPMWater.Checked = True
            Else
                cbPMWater.Checked = False
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMNutToFibre), intPMAR) = True Then
                cbPMNutFibre.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDayEnd), intPMAR) = True Then
                cbPMDayEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMonthEnd), intPMAR) = True Then
                cbPMMthEnd.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelLoss), intPMAR) = True Then
                cbPMKernelLoss.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWastedWaterQuality), intPMAR) = True Then
                cbPMWastedWaterQuality.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMVolConvMaster), intPMAR) = True Then
                cbPMVolConvMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAvgCapConvMaster), intPMAR) = True Then
                cbPMAvgCapConvMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOPropertyMaster), intPMAR) = True Then
                cbPMCPOPropertyMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageTypeMaster), intPMAR) = True Then
                cbPMStorageTypeMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageAreaMaster), intPMAR) = True Then
                cbPMStorageAreaMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProcessingLineMaster), intPMAR) = True Then
                cbPMProcessingLineMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineMaster), intPMAR) = True Then
                cbPMMachineMaster.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableOilQuality), intPMAR) = True Then
                cbPMAcceptableOilQuality.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableKernelQuality), intPMAR) = True Then
                cbPMAcceptableKernelQuality.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMTestSample), intPMAR) = True Then
                cbPMTestSample.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMHarvestingInterval), intPMAR) = True Then
                cbPMHarvestingInterval.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineCriteria), intPMAR) = True Then
                cbPMMachineCriteria.Checked = True
            End If



        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.MillContract)) = False Then
            cbCM.Enabled = False
            cbCMMasterSetup.Enabled = False
            cbCMContractReg.Enabled = False
            cbCMContractMatch.Enabled = False
            cbCMGenDNCN.Enabled = False
            cbCMDataTransfer.Enabled = False
            cbCMExchangeRate.Enabled = False
            cbCMContractQuality.Enabled = False
            cbCMClaimQuality.Enabled = False

            cbCMContractDOReg.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillContract), intModule) = True) Then
                cbCM.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMMasterSetup), intCMAR) = True Then
                cbCMMasterSetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractReg), intCMAR) = True Then
                cbCMContractReg.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractMatching), intCMAR) = True Then
                cbCMContractMatch.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDORegistration), intCMAR) = True Then
                cbCMContractDOReg.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMGenDNCN), intCMAR) = True Then
                cbCMGenDNCN.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDataTransfer), intCMAR) = True Then
                cbCMDataTransfer.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMExchangeRate), intCMAR) = True Then
                cbCMExchangeRate.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractQuality), intCMAR) = True Then
                cbCMContractQuality.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMClaimQuality), intCMAR) = True Then
                cbCMClaimQuality.Checked = True
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Nursery)) = False Then
            cbNU.Enabled = False
            cbNUMasterSetup.Enabled = False
            cbNUWorkAccDist.Enabled = False
            cbNUSeedRcv.Enabled = False
            cbNUSeedPlant.Enabled = False
            cbNUDblTurn.Enabled = False
            cbNUTransplanting.Enabled = False
            cbNUDispatch.Enabled = False
            cbNUCulling.Enabled = False
            cbDtNu.Enabled = False
            cbNUMonthEnd.Enabled = False
            cbADTNU.Enabled = False
            cbNUCullType.Enabled = False

            cbNUMasterItem.Enabled = False
            cbNUItem.Enabled = False
            cbNUSeedIssue.Enabled = False

        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Nursery), intModule) = True) Then
                cbNU.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterSetup), intNUAR) = True Then
                cbNUMasterSetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUWorkAccDist), intNUAR) = True Then
                cbNUWorkAccDist.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedRcv), intNUAR) = True Then
                cbNUSeedRcv.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedPlant), intNUAR) = True Then
                cbNUSeedPlant.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDblTurn), intNUAR) = True Then
                cbNUDblTurn.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUTransplanting), intNUAR) = True Then
                cbNUTransplanting.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch), intNUAR) = True Then
                cbNUDispatch.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCulling), intNUAR) = True Then
                cbNUCulling.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDataTransfer), intNUAR) = True Then
                cbDtNu.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMonthEnd), intNUAR) = True Then
                cbNUMonthEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCullType), intNUAR) = True Then
                cbNUCullType.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterItem), intNUAR) = True Then
                cbNUMasterItem.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUItem), intNUAR) = True Then
                cbNUItem.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedIssue), intNUAR) = True Then
                cbNUSeedIssue.Checked = True
            End If

        End If


        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Budgeting)) = False Then
            cbBudgeting.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Budget), intModule) = True) Then
                If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting), intADAR) = True Then
                    cbBudgeting.Checked = True
                End If
            End If
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.FixAsset)) = False Then
            cbFA.Enabled = False
            cbFAClassSetup.Enabled = False
            cbFAGroupSetup.Enabled = False
            cbFARegSetup.Enabled = False
            cbFAPermissionSetup.Enabled = False
            cbFAAddition.Enabled = False
            cbFADepreciation.Enabled = False
            cbFADisposal.Enabled = False
            cbFAWriteOff.Enabled = False
            cbFAGenDepreciation.Enabled = False
            cbFAMasterSetup.Enabled = False
            cbFAItemSetup.Enabled = False
            cbDtDwFA.Enabled = False
            cbDtUpFA.Enabled = False
            cbFAMonthEnd.Enabled = False
            cbFARegLine.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.FixAsset), intModule) = True) Then
                cbFA.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAClassSetup), intFAAR) = True Then
                cbFAClassSetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGroupSetup), intFAAR) = True Then
                cbFAGroupSetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegistrationSetup), intFAAR) = True Then
                cbFARegSetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAPermissionSetup), intFAAR) = True Then
                cbFAPermissionSetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAAddition), intFAAR) = True Then
                cbFAAddition.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADepreciation), intFAAR) = True Then
                cbFADepreciation.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADisposal), intFAAR) = True Then
                cbFADisposal.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAWriteOff), intFAAR) = True Then
                cbFAWriteOff.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGenDepreciation), intFAAR) = True Then
                cbFAGenDepreciation.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMaster), intFAAR) = True Then
                cbFAMasterSetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAItem), intFAAR) = True Then
                cbFAItemSetup.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADownload), intFAAR) = True Then
                cbDtDwFA.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAUpload), intFAAR) = True Then
                cbDtUpFA.Checked = True
            End If

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMonthEnd), intFAAR) = True Then
                cbFAMonthEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegLine), intFAAR) = True Then
                cbFARegLine.Checked = True
            End If
        End If


        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAccPeriod), intADAR) = True Then
            cbAccPeriod.Checked = True
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADPeriodCfg), intADAR) = True Then
            cbPeriodCfg.Checked = True
        End If

        If (objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Inventory) And intAUTO) = objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Inventory) Then
            cbADTIN.Checked = True
        End If
        If (objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Canteen) And intAUTO) = objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Canteen) Then
            cbADTCT.Checked = True
        End If
        If (objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Workshop) And intAUTO) = objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Workshop) Then
            cbADTWS.Checked = True
        End If
        If (objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Nursery) And intAUTO) = objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Nursery) Then
            cbADTNU.Checked = True
        End If
        If (objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Purchasing) And intAUTO) = objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Purchasing) Then
            cbADTPU.Checked = True
        End If
        If (objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.AccountPayable) And intAUTO) = objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.AccountPayable) Then
            cbADTAP.Checked = True
        End If
        If (objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Payroll) And intAUTO) = objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Payroll) Then
            cbADTPR.Checked = True
        End If
        If (objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Billing) And intAUTO) = objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Billing) Then
            cbADTBI.Checked = True
        End If
        If (objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Production) And intAUTO) = objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Production) Then
            cbADTPD.Checked = True
        End If
        If (objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.GeneralLedger) And intAUTO) = objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.GeneralLedger) Then
            cbADTGL.Checked = True
        End If

        If objAdmin.mtdLicenseCheck(objGlobal.mtdGetModuleCode(objGlobal.EnumModule.CashAndBank)) = False Then
            cbCB.Enabled = False
            cbCBPayment.Enabled = False
            cbCBReceipt.Enabled = False
            cbCBDeposit.Enabled = False
            cbCBInterAdj.Enabled = False
            cbCBWithdrawal.Enabled = False
            cbCBMthEnd.Enabled = False
            cbCBCashFlow.Enabled = False
            cbCBCashBank.Enabled = False
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.CashAndBank), intModule) = True) Then
                cbCB.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBPayment), intCBAR) = True Then
                cbCBPayment.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBReceipt), intCBAR) = True Then
                cbCBReceipt.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBDeposit), intCBAR) = True Then
                cbCBDeposit.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBInterAdj), intCBAR) = True Then
                cbCBInterAdj.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBWithdrawal), intCBAR) = True Then
                cbCBWithdrawal.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBMthEnd), intCBAR) = True Then
                cbCBMthEnd.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashFlow), intCBAR) = True Then
                cbCBCashFlow.Checked = True
            End If
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashBank), intCBAR) = True Then
                cbCBCashBank.Checked = True
            End If

        End If
    End Sub


    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim strOpCode As String
        Dim strCompany As String = Request.Form("hidCompCode")
        Dim strLocation As String
        Dim strDocRetain As String = txtDoc.Text
        Dim strRecType As String = Request.Form("hidRecType")
        Dim intINAccessRights As Integer = 0
        Dim intCTAccessRights As Integer = 0
        Dim intWSAccessRights As Integer = 0
        Dim intPUAccessRights As Integer = 0
        Dim intAPAccessRights As Integer = 0
        Dim intPRAccessRights As Long = 0
        Dim intHRAccessRights As Long = 0
        Dim intBIAccessRights As Integer = 0
        Dim intPDAccessRights As Integer = 0
        Dim intGLAccessRights As Long = 0
        Dim intWMAccessRights As Integer = 0
        Dim intPMAccessRights As Integer = 0
        Dim intCMAccessRights As Integer = 0
        Dim intNUAccessRights As Integer = 0
        Dim intFAAccessRights As Integer = 0
        Dim intADAccessRights As Integer = 0
        Dim intModuleActivate As Integer = 0
        Dim intAutoDataTransfer As Integer = 0
        Dim intCBAccessRights As Integer = 0
        Dim strParam As String

        Dim strExtAR As String
        Dim strCtrlAR As String

        Dim strINARCtrl As String
        Dim strCTARCtrl As String
        Dim strWSARCtrl As String
        Dim strPUARCtrl As String
        Dim strAPARCtrl As String
        Dim strPRARCtrl As String
        Dim strHRARCtrl As String
        Dim strBIARCtrl As String
        Dim strPDARCtrl As String
        Dim strGLARCtrl As String
        Dim strADARCtrl As String
        Dim strWMARCtrl As String
        Dim strPMARCtrl As String
        Dim strCMARCtrl As String
        Dim strNUARCtrl As String
        Dim strFAARCtrl As String
        Dim strCBARCtrl As String

        Try
            strLocation = ddlLocation.SelectedItem.Value
        Catch Exp As Exception
            strLocation = ""
        End Try

        If strLocation = "" Then
            lblLocError.Visible = True
            Exit Sub
        End If

        If cbIN.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Inventory)
        End If

        If cbINProdMaster.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductSetup)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductSetup) & "|"
        End If
        If cbINItem.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItem)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItem) & "|"
        End If
        If cbINDirChrg.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDirectChargeItem)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDirectChargeItem) & "|"
        End If
        If cbMiscItem.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMiscItem)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMiscItem) & "|"
        End If
        If cbINPR.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest) & "|"
        End If
        If cbStkRtnAdv.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturnAdvice)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturnAdvice) & "|"
        End If
        If cbStkTransfer.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer) & "|"
        End If
        If cbStkIsu.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue) & "|"
        End If
        If cbStkRcv.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReceive)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReceive) & "|"
        End If
        If cbStkRtn.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturn)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturn) & "|"
        End If
        If cbStkAdj.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockAdjustment)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockAdjustment) & "|"
        End If
        If cbFuelIsu.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INFuelIssue)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INFuelIssue) & "|"
        End If
        If cbDtInv.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDataTransfer)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDataTransfer) & "|"
        End If
        If cbDtDwIN.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDownload)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDownload) & "|"
        End If
        If cbINMthEnd.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMonthEnd)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMonthEnd) & "|"
        End If
        If cbItemToMachine.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INItemToMachine)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INItemToMachine) & "|"
        End If
        If cbStkTransferInternal.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransferInternal)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransferInternal) & "|"
        End If
        If cbINProdBrand.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductBrand)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductBrand) & "|"
        End If
        If cbINProdModel.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductModel)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductModel) & "|"
        End If
        If cbINProdCategory.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductCategory)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductCategory) & "|"
        End If
        If cbINProdMaterial.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductMaterial)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductMaterial) & "|"
        End If
        If cbINStockAnalysis.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INAnalisisStok)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INAnalisisStok) & "|"
        End If
        If cbINItemMaster.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItemMaster)
        Else
            strINARCtrl = strINARCtrl & objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItemMaster) & "|"
        End If

        If cbCT.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Canteen)
        End If
        If cbCTMaster.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster) & "|"
        End If
        If cbCTItem.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTItem)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTItem) & "|"
        End If
        If cbCTPR.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTPurchaseRequest)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTPurchaseRequest) & "|"
        End If
        If cbCTRcv.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReceive)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReceive) & "|"
        End If
        If cbCTRtnAdv.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturnAdvice)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturnAdvice) & "|"
        End If
        If cbCTIsu.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTIssue)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTIssue) & "|"
        End If
        If cbCTRtn.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturn)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturn) & "|"
        End If
        If cbCTAdj.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTAdjustment)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTAdjustment) & "|"
        End If
        If cbCTTransfer.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTTransfer)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTTransfer) & "|"
        End If
        If cbDtCT.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTDataTransfer)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTDataTransfer) & "|"
        End If
        If cbDtDwCT.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTDownload)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTDownload) & "|"
        End If
        If cbCTMthEnd.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMonthEnd)
        Else
            strCTARCtrl = strCTARCtrl & objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMonthEnd) & "|"
        End If


        If cbWS.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Workshop)
        End If
        If cbWSProdMaster.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaster)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaster) & "|"
        End If
        If cbWSWorkMaster.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkMaster)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkMaster) & "|"
        End If
        If cbWSItem.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItem)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItem) & "|"
        End If
        If cbWSDirChrg.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItem)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItem) & "|"
        End If
        If cbWSJob.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration) & "|"
        End If
        If cbWSMechHr.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMechanicHour)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMechanicHour) & "|"
        End If
        If cbWSDN.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDebitNote)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDebitNote) & "|"
        End If
        If cbDtWS.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDataTransfer)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDataTransfer) & "|"
        End If
        If cbDtDwWS.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDownload)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDownload) & "|"
        End If
        If cbWSMthEnd.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMonthEnd)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMonthEnd) & "|"
        End If
        If cbWSPart.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemPart)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemPart) & "|"
        End If
        If cbWSProdBrand.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductBrand)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductBrand) & "|"
        End If
        If cbWSProdModel.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductModel)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductModel) & "|"
        End If
        If cbWSProdCategory.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductCategory)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductCategory) & "|"
        End If
        If cbWSProdMaterial.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaterial)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaterial) & "|"
        End If
        If cbWSStockAnalysis.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSAnalisisStok)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSAnalisisStok) & "|"
        End If
        If cbWSItemMaster.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemMaster)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemMaster) & "|"
        End If
        If cbWSWorkshopService.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkshopService)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkshopService) & "|"
        End If

        If cbWSDirChrgMaster.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItemMaster)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItemMaster) & "|"
        End If

        If cbWSMillProcDitr.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMillProcDistr)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMillProcDistr) & "|"
        End If
        If cbWSCalMachine.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSCalMachine)
        Else
            strWSARCtrl = strWSARCtrl & objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSCalMachine) & "|"
        End If

        If cbPU.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Purchasing)
        End If
        If cbPUSupp.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier)
        Else
            strPUARCtrl = strPUARCtrl & objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier) & "|"
        End If
        If cbPUPelimpahan.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPelimpahan)
        Else
            strPUARCtrl = strPUARCtrl & objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPelimpahan) & "|"
        End If

        If cbPURPH.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PURPH)
        Else
            strPUARCtrl = strPUARCtrl & objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PURPH) & "|"
        End If
        If cbPUPO.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder)
        Else
            strPUARCtrl = strPUARCtrl & objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder) & "|"
        End If
        If cbPUGoodsRcv.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReceive)
        Else
            strPUARCtrl = strPUARCtrl & objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReceive) & "|"
        End If
        If cbPUGRN.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReturnNote)
        Else
            strPUARCtrl = strPUARCtrl & objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReturnNote) & "|"
        End If
        If cbPUDA.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDispatchAdvice)
        Else
            strPUARCtrl = strPUARCtrl & objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDispatchAdvice) & "|"
        End If
        If cbDtPU.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDataTransfer)
        Else
            strPUARCtrl = strPUARCtrl & objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDataTransfer) & "|"
        End If
        If cbDtDwPU.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDownload)
        Else
            strPUARCtrl = strPUARCtrl & objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDownload) & "|"
        End If
        If cbPUMthEnd.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUMonthEnd)
        Else
            strPUARCtrl = strPUARCtrl & objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUMonthEnd) & "|"
        End If

        If cbAP.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.AccountPayable)
        End If
        If cbAPInvoice.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APInvoiceReceive)
        Else
            strAPARCtrl = strAPARCtrl & objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APInvoiceReceive) & "|"
        End If
        If cbAPDN.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APDebitNote)
        Else
            strAPARCtrl = strAPARCtrl & objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APDebitNote) & "|"
        End If
        If cbAPCN.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditNote)
        Else
            strAPARCtrl = strAPARCtrl & objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditNote) & "|"
        End If
        If cbAPCrtJrn.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditorJournal)
        Else
            strAPARCtrl = strAPARCtrl & objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditorJournal) & "|"
        End If
        If cbAPPay.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APPayment)
        Else
            strAPARCtrl = strAPARCtrl & objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APPayment) & "|"
        End If
        If cbDtDwAP.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APDownload)
        Else
            strAPARCtrl = strAPARCtrl & objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APDownload) & "|"
        End If
        If cbAPMthEnd.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APMonthEnd)
        Else
            strAPARCtrl = strAPARCtrl & objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APMonthEnd) & "|"
        End If

        If cbHR.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.HumanResource)
        End If
        If cbHRDepartment.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDepartment)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDepartment) & "|"
        End If
        If cbHRCompany.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany) & "|"
        End If
        If cbHRFunc.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRFunction)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRFunction) & "|"
        End If
        If cbHRSkill.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSkill)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSkill) & "|"
        End If
        If cbHREval.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREvaluation)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREvaluation) & "|"
        End If
        If cbHRBank.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBank)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBank) & "|"
        End If
        If cbHRHoliday.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRHoliday)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRHoliday) & "|"
        End If
        If cbHRCP.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCareerProgress)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCareerProgress) & "|"
        End If
        If cbHREmpDet.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails) & "|"
        End If
        If cbHREmpPR.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeePayroll)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeePayroll) & "|"
        End If
        If cbHREmpEmploy.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeEmployement)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeEmployement) & "|"
        End If
        If cbHRSat.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeStatutory)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeStatutory) & "|"
        End If
        If cbHREmpFam.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeFamily)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeFamily) & "|"
        End If
        If cbHREmpQlf.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeQualification)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeQualification) & "|"
        End If
        If cbHREmpSkill.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeSkill)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeSkill) & "|"
        End If
        If cbHRContSuper.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRContractorSupervision)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRContractorSupervision) & "|"
        End If
        If cbGenEmpCode.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGenerateEmpCode)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGenerateEmpCode) & "|"
        End If
        If cbDtHR.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDataTransfer)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDataTransfer) & "|"
        End If
        If cbHRNationality.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRNationality)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRNationality) & "|"
        End If

        If cbHRPosition.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPosition)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPosition) & "|"
        End If
        If cbHRLevel.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRLevel)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRLevel) & "|"
        End If
        If cbHRReligion.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRReligion)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRReligion) & "|"
        End If
        If cbHRICType.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRICType)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRICType) & "|"
        End If
        If cbHRRace.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRRace)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRRace) & "|"
        End If
        If cbHRQualification.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRQualification)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRQualification) & "|"
        End If
        If cbHRSubject.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSubject)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSubject) & "|"
        End If
        If cbHRCPCode.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCPCode)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCPCode) & "|"
        End If
        If cbHRSalScheme.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalScheme)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalScheme) & "|"
        End If
        If cbHRSalGrade.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalGrade)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalGrade) & "|"
        End If
        If cbHRShift.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRShift)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRShift) & "|"
        End If
        If cbHRGang.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGang)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGang) & "|"
        End If
        If cbHRBankFormat.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBankFormat)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBankFormat) & "|"
        End If
        If cbHRJamsostek.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRJamsostek)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRJamsostek) & "|"
        End If
        If cbHRTaxBranch.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTaxBranch)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTaxBranch) & "|"
        End If
        If cbHRTax.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTax)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTax) & "|"
        End If
        If cbHRPublicHoliday.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPublicHoliday)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPublicHoliday) & "|"
        End If
        If cbHRPOH.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPointHired)
        Else
            strHRARCtrl = strHRARCtrl & objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPointHired) & "|"
        End If

        If cbPR.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Payroll)
        End If
        If cbPRAD.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAllowanceDeduction)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAllowanceDeduction) & "|"
        End If
        If cbPRSal.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalary)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalary) & "|"
        End If
        If cbPRContract.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRContract)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRContract) & "|"
        End If
        If cbPRRice.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRice)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRice) & "|"
        End If
        If cbPREmployeeEvaluation.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PREmployeeEvaluation)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PREmployeeEvaluation) & "|"
        End If
        If cbPRStandardEvaluation.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRStandardEvaluation)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRStandardEvaluation) & "|"
        End If
        If cbPRSalaryIncrease.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalaryIncrease)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalaryIncrease) & "|"
        End If
        If cbPRTranInc.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTransportIncentive)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTransportIncentive) & "|"
        End If
        If cbPRAttdTrx.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAttendance)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAttendance) & "|"
        End If
        If cbPRTripTrx.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxTrip)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxTrip) & "|"
        End If
        If cbPRRatePay.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxRatePayment)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxRatePayment) & "|"
        End If
        If cbPRADTrx.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAllowanceDeduction)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAllowanceDeduction) & "|"
        End If
        If cbPRContCheckroll.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractorCheckroll)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractorCheckroll) & "|"
        End If
        If cbPRWorkPerformance.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance) & "|"
        End If
        If cbPRWPContractor.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWPContractor)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWPContractor) & "|"
        End If
        If cbDtPR.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDataTransfer)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDataTransfer) & "|"
        End If
        If cbDwPR.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownload)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownload) & "|"
        End If
        If cbPRMthEnd.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMonthEnd)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMonthEnd) & "|"
        End If
        If cbPRYearEnd.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRYearEnd)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRYearEnd) & "|"
        End If
        If cbPRADGroup.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRADGroup)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRADGroup) & "|"
        End If
        If cbPRDenda.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDenda)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDenda) & "|"
        End If
        If cbPRHarvInc.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvInc)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvInc) & "|"
        End If
        If cbPRLoad.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRLoad)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRLoad) & "|"
        End If
        If cbPRRoute.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRoute)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRoute) & "|"
        End If
        If cbPRMedical.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMedical)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMedical) & "|"
        End If
        If cbPRAirBus.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAirBus)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAirBus) & "|"
        End If
        If cbPRMaternity.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMaternity)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMaternity) & "|"
        End If
        If cbPRPensiun.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPensiun)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPensiun) & "|"
        End If
        If cbPRRelocation.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRelocation)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRelocation) & "|"
        End If
        If cbPRIncentive.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPremi)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPremi) & "|"
        End If
        If cbPRContractPay.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractPayment)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractPayment) & "|"
        End If
        If cbPRWagesPay.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWagesPayment)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWagesPayment) & "|"
        End If
        If cbDwPRWages.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadWages)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadWages) & "|"
        End If
        If cbDwPRBankAuto.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadBankAuto)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadBankAuto) & "|"
        End If


        If cbPRPaySetup.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup) & "|"
        End If
        If cbPRDailyAttd.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDailyAttd)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDailyAttd) & "|"
        End If
        If cbPRHarvAttd.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvAttd)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvAttd) & "|"
        End If
        If cbPRWeekly.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRWeekly)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRWeekly) & "|"
        End If
        If cbPRMthRice.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRice)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRice) & "|"
        End If
        If cbPRMthRapel.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRapel)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRapel) & "|"
        End If
        If cbPRMthBonus.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthBonus)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthBonus) & "|"
        End If
        If cbPRMthTHR.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTHR)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTHR) & "|"
        End If
        If cbPRMthDaily.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthDaily)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthDaily) & "|"
        End If
        If cbPRMthPayroll.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll) & "|"
        End If
        If cbPRMthTransfer.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTransfer)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTransfer) & "|"
        End If

        If cbDwAttdInterface.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDwAttdInterface)
        Else
            strPRARCtrl = strPRARCtrl & objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDwAttdInterface) & "|"
        End If

        If cbBI.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Billing)
        End If
        If cbBillParty.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIBillParty)
        Else
            strBIARCtrl = strBIARCtrl & objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIBillParty) & "|"
        End If
        If cbBINote.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BINote)
        Else
            strBIARCtrl = strBIARCtrl & objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BINote) & "|"
        End If
        If cbDtBI.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIDataTransfer)
        Else
            strBIARCtrl = strBIARCtrl & objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIDataTransfer) & "|"
        End If
        If cbDtDwBI.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIDownload)
        Else
            strBIARCtrl = strBIARCtrl & objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIDownload) & "|"
        End If
        If cbBIMthEnd.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIMonthEnd)
        Else
            strBIARCtrl = strBIARCtrl & objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIMonthEnd) & "|"
        End If
        If cbBIInvoice.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIInvoice)
        Else
            strBIARCtrl = strBIARCtrl & objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIInvoice) & "|"
        End If
        If cbBIReceipt.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIReceipt)
        Else
            strBIARCtrl = strBIARCtrl & objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIReceipt) & "|"
        End If
        If cbBIJournal.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIJournal)
        Else
            strBIARCtrl = strBIARCtrl & objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIJournal) & "|"
        End If
        If cbBICreditNote.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BICreditNote)
        Else
            strBIARCtrl = strBIARCtrl & objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BICreditNote) & "|"
        End If

        If cbPD.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Production)
        End If
        If cbEstProd.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDEstateProduction)
        Else
            strPDARCtrl = strPDARCtrl & objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDEstateProduction) & "|"
        End If
        If cbPOMProd.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMProduction)
        Else
            strPDARCtrl = strPDARCtrl & objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMProduction) & "|"
        End If
        If cbDtDwPD.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDDownload)
        Else
            strPDARCtrl = strPDARCtrl & objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDDownload) & "|"
        End If
        If cbPDMthEnd.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMonthEnd)
        Else
            strPDARCtrl = strPDARCtrl & objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMonthEnd) & "|"
        End If
        If cbYearPlantYield.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDYearOfPlantYield)
        Else
            strPDARCtrl = strPDARCtrl & objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDYearOfPlantYield) & "|"
        End If
        If cbPOMStorage.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStorage)
        Else
            strPDARCtrl = strPDARCtrl & objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStorage) & "|"
        End If
        If cbPOMStat.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStatistics)
        Else
            strPDARCtrl = strPDARCtrl & objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStatistics) & "|"
        End If

        If cbGL.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.GeneralLedger)
        End If
        If cbAccCls.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccCls)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccCls) & "|"
        End If
        If cbAct.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivity)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivity) & "|"
        End If
        If cbVehExp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpense)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpense) & "|"
        End If
        If cbVeh.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle) & "|"
        End If
        If cbBlkGrp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlock)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlock) & "|"
        End If
        If cbExp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLExpense)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLExpense) & "|"
        End If
        If cbAccount.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccount)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccount) & "|"
        End If
        If cbEntrySetup.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLEntrySetup)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLEntrySetup) & "|"
        End If
        If cbBalSheetSetup.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBalSheetSetup)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBalSheetSetup) & "|"
        End If
        If cbProfLossSetup.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLProfLossSetup)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLProfLossSetup) & "|"
        End If
        If cbJrn.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal) & "|"
        End If
        If cbJrnAdj.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournalAdj)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournalAdj) & "|"
        End If
        If cbVehUsg.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleUsage)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleUsage) & "|"
        End If
        If cbDtGL.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLDataTransfer)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLDataTransfer) & "|"
        End If
        If cbDtUp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLUpload)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLUpload) & "|"
        End If
        If cbGLGCDist.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMthEndGCDist)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMthEndGCDist) & "|"
        End If
        If cbGLJrnMthEnd.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJrnMonthEnd)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJrnMonthEnd) & "|"
        End If
        If cbGLMthEnd.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd) & "|"
        End If
        If cbRunHour.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLRunHour)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLRunHour) & "|"
        End If
        If cbAccClsGrp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccClsGrp)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccClsGrp) & "|"
        End If
        If cbActGrp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivityGrp)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivityGrp) & "|"
        End If
        If cbSubAct.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubActivity)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubActivity) & "|"
        End If
        If cbVehExpGrp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpenseGrp)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpenseGrp) & "|"
        End If
        If cbVehType.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleType)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleType) & "|"
        End If
        If cbBlk.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlk)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlk) & "|"
        End If
        If cbSubBlk.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubBlk)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubBlk) & "|"
        End If
        If cbAccountGrp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccountGrp)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccountGrp) & "|"
        End If
        If cbPosting.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLPosting)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLPosting) & "|"
        End If

        If cbGLCOGS.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLCOGS)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLCOGS) & "|"
        End If

        If cbFSSetup.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLFSSETUP)
        Else
            strGLARCtrl = strGLARCtrl & objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLFSSETUP) & "|"
        End If

        If cbRC.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Reconciliation)
        End If
        If cbRCDA.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDispatchAdvice)
        Else
            strADARCtrl = strADARCtrl & objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDispatchAdvice) & "|"
        End If
        If cbRCJrn.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCJournal)
        Else
            strADARCtrl = strADARCtrl & objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCJournal) & "|"
        End If
        If cbReadInterRC.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCReadInterface)
        Else
            strADARCtrl = strADARCtrl & objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCReadInterface) & "|"
        End If
        If cbSendInterRC.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCSendInterface)
        Else
            strADARCtrl = strADARCtrl & objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCSendInterface) & "|"
        End If
        If cbDtRC.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDataTransfer)
        Else
            strADARCtrl = strADARCtrl & objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDataTransfer) & "|"
        End If

        If cbWM.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillWeighing)
        End If
        If cbWMTransport.Checked Then
            intWMAccessRights += objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMTransporterSetup)
        Else
            strWMARCtrl = strWMARCtrl & objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMTransporterSetup) & "|"
        End If
        If cbWMTicket.Checked Then
            intWMAccessRights += objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket)
        Else
            strWMARCtrl = strWMARCtrl & objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket) & "|"
        End If
        If cbWMFFBAssessment.Checked Then
            intWMAccessRights += objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMFFBAssessment)
        Else
            strWMARCtrl = strWMARCtrl & objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMFFBAssessment) & "|"
        End If
        If cbWMDataTransfer.Checked Then
            intWMAccessRights += objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMDataTransfer)
        Else
            strWMARCtrl = strWMARCtrl & objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMDataTransfer) & "|"
        End If

        If cbPD.Checked And cbPM.Enabled = True Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillProduction)
        End If
        If cbPMMasterSetup.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMasterSetup)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMasterSetup) & "|"
        End If
        If cbPMVolConvMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMVolConvMaster)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMVolConvMaster) & "|"
        End If
        If cbPMAvgCapConvMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAvgCapConvMaster)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAvgCapConvMaster) & "|"
        End If
        If cbPMCPOPropertyMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOPropertyMaster)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOPropertyMaster) & "|"
        End If
        If cbPMStorageTypeMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageTypeMaster)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageTypeMaster) & "|"
        End If
        If cbPMStorageAreaMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageAreaMaster)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageAreaMaster) & "|"
        End If
        If cbPMProcessingLineMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProcessingLineMaster)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProcessingLineMaster) & "|"
        End If
        If cbPMMachineMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineMaster)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineMaster) & "|"
        End If
        If cbPMAcceptableOilQuality.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableOilQuality)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableOilQuality) & "|"
        End If
        If cbPMAcceptableKernelQuality.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableKernelQuality)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableKernelQuality) & "|"
        End If
        If cbPMTestSample.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMTestSample)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMTestSample) & "|"
        End If
        If cbPMHarvestingInterval.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMHarvestingInterval)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMHarvestingInterval) & "|"
        End If

        If cbPMMachineCriteria.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineCriteria)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineCriteria) & "|"
        End If

        If cbPMMill.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMill)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMill) & "|"
        End If
        If cbPMDailyProd.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction) & "|"
        End If
        If cbPMCPOStore.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOStorage)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOStorage) & "|"
        End If
        If cbPMPKStore.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMPKStorage)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMPKStorage) & "|"
        End If
        If cbPMOilLoss.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilLoss)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilLoss) & "|"
        End If
        If cbPMOilQuality.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilQuality)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilQuality) & "|"
        End If
        If cbPMKernelQuality.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelQuality)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelQuality) & "|"
        End If
        If cbPMProdKernel.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProducedKernel)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProducedKernel) & "|"
        End If
        If cbPMDispKernel.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDispatchedKernel)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDispatchedKernel) & "|"
        End If
        If cbPMWater.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWaterQuality)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWaterQuality) & "|"
        End If
        If cbPMNutFibre.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMNutToFibre)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMNutToFibre) & "|"
        End If
        If cbPMDayEnd.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDayEnd)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDayEnd) & "|"
        End If
        If cbPDMthEnd.Checked And cbPMMthEnd.Enabled = True Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMonthEnd)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMonthEnd) & "|"
        End If

        If cbPMKernelLoss.Checked And cbPMKernelLoss.Enabled = True Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelLoss)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelLoss) & "|"
        End If
        If cbPMWastedWaterQuality.Checked And cbPMWastedWaterQuality.Enabled = True Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWastedWaterQuality)
        Else
            strPMARCtrl = strPMARCtrl & objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWastedWaterQuality) & "|"
        End If

        If cbCM.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillContract)
        End If
        If cbCMMasterSetup.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMMasterSetup)
        Else
            strCMARCtrl = strCMARCtrl & objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMMasterSetup) & "|"
        End If
        If cbCMExchangeRate.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMExchangeRate)
        Else
            strCMARCtrl = strCMARCtrl & objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMExchangeRate) & "|"
        End If
        If cbCMContractQuality.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractQuality)
        Else
            strCMARCtrl = strCMARCtrl & objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractQuality) & "|"
        End If
        If cbCMClaimQuality.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMClaimQuality)
        Else
            strCMARCtrl = strCMARCtrl & objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMClaimQuality) & "|"
        End If

        If cbCMContractReg.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractReg)
        Else
            strCMARCtrl = strCMARCtrl & objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractReg) & "|"
        End If
        If cbCMContractMatch.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractMatching)
        Else
            strCMARCtrl = strCMARCtrl & objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractMatching) & "|"
        End If

        If cbCMContractDOReg.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDORegistration)
        Else
            strCMARCtrl = strCMARCtrl & objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDORegistration) & "|"
        End If


        If cbCMGenDNCN.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMGenDNCN)
        Else
            strCMARCtrl = strCMARCtrl & objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMGenDNCN) & "|"
        End If
        If cbCMDataTransfer.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDataTransfer)
        Else
            strCMARCtrl = strCMARCtrl & objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDataTransfer) & "|"
        End If

        If cbNU.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Nursery)
        End If
        If cbNUMasterSetup.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterSetup)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterSetup) & "|"
        End If
        If cbNUWorkAccDist.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUWorkAccDist)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUWorkAccDist) & "|"
        End If
        If cbNUSeedRcv.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedRcv)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedRcv) & "|"
        End If
        If cbNUSeedPlant.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedPlant)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedPlant) & "|"
        End If
        If cbNUDblTurn.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDblTurn)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDblTurn) & "|"
        End If
        If cbNUTransplanting.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUTransplanting)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUTransplanting) & "|"
        End If
        If cbNUDispatch.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch) & "|"
        End If
        If cbNUCulling.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCulling)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCulling) & "|"
        End If
        If cbDtNu.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDataTransfer)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDataTransfer) & "|"
        End If
        If cbNUMonthEnd.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMonthEnd)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMonthEnd) & "|"
        End If
        If cbNUCullType.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCullType)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCullType) & "|"
        End If

        If cbNUMasterItem.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterItem)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterItem) & "|"
        End If
        If cbNUItem.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUItem)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUItem) & "|"
        End If
        If cbNUSeedIssue.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedIssue)
        Else
            strNUARCtrl = strNUARCtrl & objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedIssue) & "|"
        End If

        If cbBudgeting.Checked = True Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Budget)
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting)
        Else
            strADARCtrl = strADARCtrl & objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting) & "|"
        End If

        If cbFA.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.FixAsset)
        End If
        If cbFAClassSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAClassSetup)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAClassSetup) & "|"
        End If
        If cbFAGroupSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGroupSetup)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGroupSetup) & "|"
        End If
        If cbFARegSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegistrationSetup)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegistrationSetup) & "|"
        End If
        If cbFAPermissionSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAPermissionSetup)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAPermissionSetup) & "|"
        End If
        If cbFAAddition.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAAddition)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAAddition) & "|"
        End If
        If cbFADepreciation.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADepreciation)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADepreciation) & "|"
        End If
        If cbFADisposal.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADisposal)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADisposal) & "|"
        End If
        If cbFAWriteOff.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAWriteOff)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAWriteOff) & "|"
        End If
        If cbFAGenDepreciation.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGenDepreciation)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGenDepreciation) & "|"
        End If

        If cbFAMasterSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMaster)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMaster) & "|"
        End If
        If cbFAItemSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAItem)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAItem) & "|"
        End If
        If cbDtDwFA.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADownload)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADownload) & "|"
        End If
        If cbDtUpFA.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAUpload)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAUpload) & "|"
        End If

        If cbFAMonthEnd.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMonthEnd)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMonthEnd) & "|"
        End If
        If cbFARegLine.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegLine)
        Else
            strFAARCtrl = strFAARCtrl & objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegLine) & "|"
        End If

        If cbAccPeriod.Checked = True Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAccPeriod)
        Else
            strADARCtrl = strADARCtrl & objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAccPeriod) & "|"
        End If
        If cbPeriodCfg.Checked = True Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADPeriodCfg)
        Else
            strADARCtrl = strADARCtrl & objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADPeriodCfg) & "|"
        End If
        If cbADTIN.Checked Then
            intAutoDataTransfer += objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Inventory)
        End If
        If cbADTCT.Checked Then
            intAutoDataTransfer += objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Canteen)
        End If
        If cbADTWS.Checked Then
            intAutoDataTransfer += objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Workshop)
        End If
        If cbADTNU.Checked Then
            intAutoDataTransfer += objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Nursery)
        End If
        If cbADTPU.Checked Then
            intAutoDataTransfer += objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Purchasing)
        End If
        If cbADTAP.Checked Then
            intAutoDataTransfer += objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.AccountPayable)
        End If
        If cbADTPR.Checked Then
            intAutoDataTransfer += objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Payroll)
        End If
        If cbADTBI.Checked Then
            intAutoDataTransfer += objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Billing)
        End If
        If cbADTPD.Checked Then
            intAutoDataTransfer += objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Production)
        End If
        If cbADTGL.Checked Then
            intAutoDataTransfer += objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.GeneralLedger)
        End If

        If cbCB.Checked Then
            intModuleActivate += objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.CashAndBank)
        End If
        If cbCBPayment.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBPayment)
        Else
            strCBARCtrl = strCBARCtrl & objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBPayment) & "|"
        End If
        If cbCBReceipt.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBReceipt)
        Else
            strCBARCtrl = strCBARCtrl & objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBReceipt) & "|"
        End If
        If cbCBDeposit.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBDeposit)
        Else
            strCBARCtrl = strCBARCtrl & objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBDeposit) & "|"
        End If
        If cbCBInterAdj.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBInterAdj)
        Else
            strCBARCtrl = strCBARCtrl & objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBInterAdj) & "|"
        End If
        If cbCBWithdrawal.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBWithdrawal)
        Else
            strCBARCtrl = strCBARCtrl & objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBWithdrawal) & "|"
        End If
        If cbCBMthEnd.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBMthEnd)
        Else
            strCBARCtrl = strCBARCtrl & objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBMthEnd) & "|"
        End If
        If cbCBCashFlow.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashFlow)
        Else
            strCBARCtrl = strCBARCtrl & objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashFlow) & "|"
        End If
        If cbCBCashBank.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashBank)
        Else
            strCBARCtrl = strCBARCtrl & objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashBank) & "|"
        End If


        strParam = strCompany & "|" & _
                   strLocation & "|" & _
                   intINAccessRights & "|" & _
                   intCTAccessRights & "|" & _
                   intWSAccessRights & "|" & _
                   intPUAccessRights & "|" & _
                   intAPAccessRights & "|" & _
                   intHRAccessRights & "|" & _
                   intPRAccessRights & "|" & _
                   intBIAccessRights & "|" & _
                   intPDAccessRights & "|" & _
                   intGLAccessRights & "|" & _
                   intWMAccessRights & "|" & _
                   intPMAccessRights & "|" & _
                   intCMAccessRights & "|" & _
                   intNUAccessRights & "|" & _
                   intADAccessRights & "|" & _
                   intModuleActivate & "|" & _
                   intAutoDataTransfer & "|" & _
                   strDocRetain & "|" & _
                   intFAAccessRights & "|" & _
                   intCBAccessRights

        If hidLocCode.Value = "" Then
            strOpCode = "PWSYSTEM_CLSCONFIG_SYSLOC_ADD"
        Else
            strOpCode = "PWSYSTEM_CLSCONFIG_SYSLOC_UPD"
        End If


        strExtAR = lblINARExt.Text & "|" & _
                    lblCTARExt.Text & "|" & _
                    lblWSARExt.Text & "|" & _
                    lblPUARExt.Text & "|" & _
                    lblAPARExt.Text & "|" & _
                    lblPRARExt.Text & "|" & _
                    lblHRARExt.Text & "|" & _
                    lblBIARExt.Text & "|" & _
                    lblPDARExt.Text & "|" & _
                    lblGLARExt.Text & "|" & _
                    lblWMARExt.Text & "|" & _
                    lblPMARExt.Text & "|" & _
                    lblCMARExt.Text & "|" & _
                    lblNUARExt.Text & "|" & _
                    lblADARExt.Text & "|" & _
                    lblFAARExt.Text & "|" & _
                    lblCBARExt.Text

        strCtrlAR = strINARCtrl & Chr(9) & _
                    strCTARCtrl & Chr(9) & _
                    strWSARCtrl & Chr(9) & _
                    strPUARCtrl & Chr(9) & _
                    strAPARCtrl & Chr(9) & _
                    strPRARCtrl & Chr(9) & _
                    strHRARCtrl & Chr(9) & _
                    strBIARCtrl & Chr(9) & _
                    strPDARCtrl & Chr(9) & _
                    strGLARCtrl & Chr(9) & _
                    strWMARCtrl & Chr(9) & _
                    strPMARCtrl & Chr(9) & _
                    strCMARCtrl & Chr(9) & _
                    strNUARCtrl & Chr(9) & _
                    strADARCtrl & Chr(9) & _
                    strFAARCtrl & Chr(9) & _
                    strCBARCtrl

        Try
            intErrNo = objSysCfg.mtdProcessSysLoc(strOpCode, _
                                                  strUserLocGet, _
                                                  strOpCode_UpdUserLoc, _
                                                  strUserId, _
                                                  strParam, _
                                                  strExtAR, _
                                                  strCtrlAR, _
                                                  strCompany, _
                                                  strLocation)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSLOC_PROCESS_SYSLOC&errmesg=" & Exp.Message & "&redirect=system/config/syssetting.aspx")
        End Try

        onLoad_Display()
    End Sub

    Sub DelBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strCompany As String = Request.Form("hidCompCode")
        Dim strLocation As String = Request.Form("ddlLocation")
        Dim strOpCode As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DEL"
        Dim intErrNo As Integer
        Dim strExtAR As String
        Dim strCtrlAR As String
        Dim strParam As String

        strParam = strCompany & "|" & strLocation & "|||||||||||||||||||"

        Try
            intErrNo = objSysCfg.mtdProcessSysLoc(strOpCode, _
                                                  strUserLocGet, _
                                                  strOpCode_UpdUserLoc, _
                                                  strUserId, _
                                                  strParam, _
                                                  strExtAR, _
                                                  strCtrlAR, _
                                                  strCompany, _
                                                  strLocation)

        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSLOC_DEL_PROCESS_SYSLOC&errmesg=" & lblErrMesage.Text & "&redirect=system/config/syssetting.aspx")
        End Try

        Response.Redirect("syssetting.aspx")
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("syssetting.aspx")
    End Sub



End Class
