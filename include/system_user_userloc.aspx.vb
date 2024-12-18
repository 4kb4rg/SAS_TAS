
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.DateAndTime


Imports agri.PWSystem.clsConfig
Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PWSystem.clsLangCap

Public Class system_user_userloc : Inherits Page

    Protected WithEvents lblUserId As Label    
    Protected WithEvents lblAccessExpire As Label
    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents txtAccessExpire As TextBox
    Protected WithEvents hidUserId As HtmlInputHidden
    Protected WithEvents hidLocation As HtmlInputHidden
    Protected WithEvents hidAccPeriod As HtmlInputHidden
    Protected WithEvents hidPeriodCfg As HtmlInputHidden
    Protected WithEvents lblErrInvalid As Label
    Protected WithEvents lblErrEarlyThen As Label
    Protected WithEvents lblErrMesage As Label
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
    Protected WithEvents cbINMthEnd As CheckBox
    Protected WithEvents cbItemToMachine As CheckBox
    Protected WithEvents cbStkTransferInternal As CheckBox

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
    Protected WithEvents cbCTMthEnd As CheckBox

    Protected WithEvents cbWSProdMaster As CheckBox
    Protected WithEvents cbWSWorkMaster As CheckBox
    Protected WithEvents cbWSItem As CheckBox
    Protected WithEvents cbWSDirChrg As CheckBox
    Protected WithEvents cbWSJob As CheckBox
    Protected WithEvents cbWSMechHr As CheckBox
    Protected WithEvents cbDtWS As CheckBox
    Protected WithEvents cbWSMthEnd As CheckBox
    Protected WithEvents cbWSPart As CheckBox

    Protected WithEvents cbPUSupp As CheckBox
    Protected WithEvents cbPUPelimpahan As CheckBox
    Protected WithEvents cbPURPH As CheckBox
    Protected WithEvents cbPUPO As CheckBox
    Protected WithEvents cbPUGoodsRcv As CheckBox
    Protected WithEvents cbPUGRN As CheckBox
    Protected WithEvents cbPUDA As CheckBox
    Protected WithEvents cbDtPU As CheckBox
    Protected WithEvents cbPUMthEnd As CheckBox

    Protected WithEvents cbAPInvoice As CheckBox
    Protected WithEvents cbAPDN As CheckBox
    Protected WithEvents cbAPCN As CheckBox
    Protected WithEvents cbAPCrtJrn As CheckBox
    Protected WithEvents cbAPPay As CheckBox
    Protected WithEvents cbAPMthEnd As CheckBox

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

    Protected WithEvents cbPRAD As CheckBox
    Protected WithEvents cbPRSal As CheckBox
    Protected WithEvents cbPRContract As CheckBox
    Protected WithEvents cbPRRice As CheckBox
    Protected WithEvents cbPRAttdTrx As CheckBox
    Protected WithEvents cbPRTripTrx As CheckBox
    Protected WithEvents cbPRRatePay As CheckBox
    Protected WithEvents cbPRADTrx As CheckBox
    Protected WithEvents cbPRContCheckroll As CheckBox
    Protected WithEvents cbDtPR As CheckBox
    Protected WithEvents cbDwPR As CheckBox
    Protected WithEvents cbPRMthEnd As CheckBox
    Protected WithEvents cbPRYearEnd As CheckBox
    Protected WithEvents cbPRWorkPerformance As CheckBox
    Protected WithEvents cbPREmployeeEvaluation As CheckBox
    Protected WithEvents cbPRStandardEvaluation As CheckBox
    Protected WithEvents cbPRSalaryIncrease As CheckBox
    Protected WithEvents cbPRWPContractor As CheckBox
    Protected WithEvents cbPRTranInc As CheckBox

    Protected WithEvents cbBillParty As CheckBox
    Protected WithEvents cbBINote As CheckBox
    Protected WithEvents cbDtBI As CheckBox
    Protected WithEvents cbBIMthEnd As CheckBox
    Protected WithEvents cbBIInvoice As CheckBox
    Protected WithEvents cbBIReceipt As CheckBox
    Protected WithEvents cbBIJournal As CheckBox

    Protected WithEvents cbEstProd As CheckBox
    Protected WithEvents cbPOMProd As CheckBox
    Protected WithEvents cbPDMthEnd As CheckBox
    Protected WithEvents cbMPOBPrice As CheckBox
    Protected WithEvents cbYearPlantYield As CheckBox

    Protected WithEvents cbWMTransport As CheckBox
    Protected WithEvents cbWMTicket As CheckBox
    Protected WithEvents cbWMFFBAssessment As CheckBox
    Protected WithEvents cbWMDataTransfer As CheckBox

    Protected WithEvents cbPMMasterSetup As CheckBox
    Protected WithEvents cbPMMill As CheckBox
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

    Protected WithEvents cbCMMasterSetup As CheckBox
    Protected WithEvents cbCMContractReg As CheckBox
    Protected WithEvents cbCMContractMatch As CheckBox

    Protected WithEvents cbCMContractDOReg As CheckBox

    Protected WithEvents cbCMGenDNCN As CheckBox
    Protected WithEvents cbCMDataTransfer As CheckBox

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
    Protected WithEvents cbFAPermissionSetup As CheckBox
    Protected WithEvents cbFAAddition As CheckBox
    Protected WithEvents cbFADepreciation As CheckBox
    Protected WithEvents cbFADisposal As CheckBox
    Protected WithEvents cbFAWriteOff As CheckBox
    Protected WithEvents cbFAAssetMasterSetup As CheckBox
    Protected WithEvents cbFAAssetItemSetup As CheckBox
    Protected WithEvents cbFAdtdw As CheckBox
    Protected WithEvents cbFAdtup As CheckBox
    Protected WithEvents cbFAGenDepreciation As CheckBox
    Protected WithEvents cbFAMonthEnd As CheckBox

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

    Protected WithEvents cbCB As CheckBox
    Protected WithEvents cbCBPayment As CheckBox
    Protected WithEvents cbCBReceipt As CheckBox
    Protected WithEvents cbCBDeposit As CheckBox
    Protected WithEvents cbCBInterAdj As CheckBox
    Protected WithEvents cbCBWithdrawal As CheckBox
    Protected WithEvents cbCBMthEnd As CheckBox
    Protected WithEvents cbCBCashFlow As CheckBox
    Protected WithEvents cbCBCashBank As CheckBox

    Protected WithEvents cbAccPeriod As CheckBox
    Protected WithEvents cbBudgeting As CheckBox
    Protected WithEvents cbCompany As CheckBox
    Protected WithEvents cbLocation As CheckBox
    Protected WithEvents cbUOM As CheckBox
    Protected WithEvents lblErrLocation As Label
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton

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
    Protected WithEvents cbWSMillProcDitr As CheckBox
    Protected WithEvents cbWSCalMachine As CheckBox

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

    Protected WithEvents cbDwAttdInterface As CheckBox

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objSysUser As New agri.PWSystem.clsUser()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLocDS As New Data.DataSet()
    Dim objLangCapDs As New Data.DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intADAR As Integer

    Dim strDateFormat As String = ""
    Dim strSelectedLocation As String
    Dim strSelectedUserId As String
    Protected WithEvents btnSelPlantDate As System.Web.UI.WebControls.Image
    Protected WithEvents cbFullRights As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbNU As System.Web.UI.WebControls.Label
    Protected WithEvents cbWM As System.Web.UI.WebControls.Label
    Protected WithEvents cbCM As System.Web.UI.WebControls.Label
    Protected WithEvents cbPM As System.Web.UI.WebControls.Label
    Protected WithEvents BackBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents cbPMKernelLoss As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbPMWastedWaterQuality As System.Web.UI.WebControls.CheckBox

    Protected WithEvents TrMPOB As HtmlTableRow
    Protected WithEvents TrTranInc As HtmlTableRow
    Protected WithEvents TrHarvInc As HtmlTableRow

    Dim strHarvTag As String = ""

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")

        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUser), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrLocation.Visible = False
            DelBtn.Visible = False
            strSelectedUserId = IIf(Request.Form("hidUserId") = "", Request.QueryString("userid"), Request.Form("hidUserId"))
            strSelectedLocation = IIf(Request.Form("ddlLocation") = "", Request.QueryString("loccode"), Request.Form("ddlLocation"))
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                onLoad_Display()
                If Trim(strLocType) <> "" Then

                    If Trim(strLocType) = objAdminLoc.EnumLocType.Mill Then
                        TrTranInc.Visible = True
                        TrHarvInc.Visible = False
                    ElseIf Trim(strLocType) = objAdminLoc.EnumLocType.Estate Then
                        TrTranInc.Visible = False
                        TrHarvInc.Visible = True
                    End If
                End If
            End If
        End If

        cbAPPay.visible = False
        cbBIReceipt.visible = False
        cbAPPay.checked = False
        cbBIReceipt.checked = False

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
        strHarvTag = " Harvesting Incentive"

        GetEntireLangCap()

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

        cbINItem.text = " " & strStockItemTag

        cbINDirChrg.text = " " & strDirectChgItemTag

        cbINProdBrand.text = " " & strProdBrandTag
        cbINProdModel.text = " " & strProdModelTag
        cbINProdCategory.text = " " & strProdCatTag
        cbINProdMaterial.text = " " & strProdMatTag
        cbINStockAnalysis.text = " " & strStockAnaTag
        cbINItemMaster.text = " " & strStockItemTag & " Master"

        cbWSProdBrand.text = " " & strProdBrandTag
        cbWSProdModel.text = " " & strProdModelTag
        cbWSProdCategory.text = " " & strProdCatTag
        cbWSProdMaterial.text = " " & strProdMatTag
        cbWSStockAnalysis.text = " " & strStockAnaTag
        cbWSItemMaster.text = " Workshop " & strStockItemTag & " Master"

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
        cbNUWorkAccDist.text = " " & strAcctDistTag
        cbNUMasterSetup.text = " " & strNUBatchTag
        cbNUCullType.text = " " & strNUCullTag

        cbHRDepartment.text = " " & strDepartmentTag & lblCode.text
        cbHRCompany.text = " " & strDepartmentTag

        cbHRFunc.text = " " & strFuncTag

        cbHREval.text = " " & lblEvaluation.text

        cbHRBank.text = " " & lblBank.text

        cbHRCP.text = strCareerProgTag

        cbPRAD.text = " " & lblAD.text

        cbPRRice.text = " " & strRiceRationTag
        cbPREmployeeEvaluation.text = " " & strEmployeeEvaluationTag

        cbPRStandardEvaluation.text = " " & strStandardEvaluationTag

        cbPRSalaryIncrease.text = " " & strSalaryIncreaseTag
        cbPRTranInc.text = " " & strTranIncTag
        cbBillParty.text = " " & strBillPartyTag

        cbAccCls.text = " " & strAccClsTag

        cbAct.text = " " & strActTag

        cbVehExp.text = " " & strVehExpenseTag & lblCode.text

        cbVeh.Text = " " & strVehTag

        cbBlkGrp.Text = " " & strBlockGrpTag

        cbBlk.Text = " " & strBlockTag

        cbExp.Text = " " & strExpenseTag & lblCode.Text

        cbAccount.text = " " & strAccTag

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
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function


    Sub onLoad_Display()
        Dim strOpCode_GetLoc As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strParam As String

        lblUserId.Text = strSelectedUserId
        hidUserId.Value = strSelectedUserId
        hidLocation.Value = strSelectedLocation

        If strSelectedLocation = "" Then
            Try
                strParam = objAdminLoc.EnumLocStatus.Active & "|" & strCompany & "|" & strSelectedUserId
                strOpCode_GetLoc = "ADMIN_CLSLOC_USERLOCLIST_GET"
                intErrNo = objAdminLoc.mtdGetLocList(strOpCode_GetLoc, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    objLocDs, _
                                                    strParam)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERLOC_GET_LOCLIST&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userdet.aspx?userid=" & strSelectedUserId)
            End Try

            For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
                objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objLocDs.Tables(0).Rows(intCnt).Item("LocCode"))
                objLocDs.Tables(0).Rows(intCnt).Item("Description") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode") & " (" & Trim(objLocDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next intCnt

            ddlLocation.DataSource = ObjLocDs.Tables(0)
            ddlLocation.DataTextField = "Description"
            ddlLocation.DataValueField = "LocCode"
            ddlLocation.DataBind()

            If objLocDs.Tables(0).Rows.Count > 0 Then
                onLoad_Process()
            Else
                lblErrLocation.Visible = True
            End If
        Else
            strOpCode_GetLoc = "ADMIN_CLSLOC_LOCATION_DETAILS_GET"
            strParam = strSelectedLocation & "|"
            Try
                intErrNo = objAdminLoc.mtdGetLocDetail(strOpCode_GetLoc, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        objLocDs, _
                                                        strParam)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERLOC_GET_LOCDETAIL&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userdet.aspx?userid=" & strSelectedUserId)
            End Try

            For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
                objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objLocDs.Tables(0).Rows(intCnt).Item("LocCode"))
                objLocDs.Tables(0).Rows(intCnt).Item("Description") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode") & " (" & Trim(objLocDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next intCnt

            ddlLocation.DataSource = ObjLocDs.Tables(0)
            ddlLocation.DataTextField = "Description"
            ddlLocation.DataValueField = "LocCode"
            ddlLocation.DataBind()

            If objLocDs.Tables(0).Rows.Count > 0 Then
                onLoad_Process()
            Else
                lblErrLocation.Visible = True
            End If
        End If
    End Sub

    Sub onLoad_Process()
        Dim strOpCode_User As String = "PWSYSTEM_CLSUSER_USER_ADAR_GET"
        Dim strOpCode_UserLoc As String = "PWSYSTEM_CLSUSER_USERLOC_DETAILS_GET"
        Dim strOpCode_SysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim objUserDs As New Data.DataSet()
        Dim objSysLocDs As New Data.DataSet()
        Dim objUserLocDs As New Data.DataSet()
        Dim objSysCfgDs As New Data.DataSet()
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Dim intLOCADAR As Integer
        Dim intLOCINAR As Integer
        Dim intLOCCTAR As Integer
        Dim intLOCWSAR As Integer
        Dim intLOCPUAR As Integer
        Dim intLOCAPAR As Integer
        Dim intLOCHRAR As Long
        Dim intLOCPRAR As Long
        Dim intLOCBIAR As Integer
        Dim intLOCPDAR As Integer
        Dim intLOCGLAR As Long
        Dim intLOCWMAR As Integer
        Dim intLOCPMAR As Integer
        Dim intLOCCMAR As Integer
        Dim intLOCNUAR As Integer
        Dim intLOCFAAR As Integer
        Dim intLOCCBAR As Integer

        Dim strExpiryDate As String
        Dim intModule As Integer

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
        Dim intADAR As Integer
        Dim intWMAR As Integer
        Dim intPMAR As Integer
        Dim intCMAR As Integer
        Dim intNUAR As Integer
        Dim intFAAR As Integer
        Dim intCBAR As Integer

        If strDateFormat = "" Then
            strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
            Try
                intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                      strCompany, _
                                                      strLocation, _
                                                      strUserId, _
                                                      objSysCfgDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERLOC_GET_CONFIG&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userdet.aspx?userid=" & strSelectedUserId)
            End Try

            strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))
        End If

        strParam = strCompany & "|" & ddlLocation.SelectedItem.Value & "|" & strSelectedUserId
        Try
            intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCode_SysLoc, _
                                                 strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysLocDs, _
                                                  strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERLOC_GET_SYSLOC1&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userdet.aspx?userid=" & strSelectedUserId)
        End Try

        intModule = Trim(objSysLocDs.Tables(0).Rows(0).Item("ModuleActivate"))
        intLOCADAR = objSysLocDs.Tables(0).Rows(0).Item("ADAR")
        intLOCINAR = objSysLocDs.Tables(0).Rows(0).Item("INAR")
        intLOCCTAR = objSysLocDs.Tables(0).Rows(0).Item("CTAR")
        intLOCWSAR = objSysLocDs.Tables(0).Rows(0).Item("WSAR")
        intLOCPUAR = objSysLocDs.Tables(0).Rows(0).Item("PUAR")
        intLOCAPAR = objSysLocDs.Tables(0).Rows(0).Item("APAR")
        intLOCHRAR = objSysLocDs.Tables(0).Rows(0).Item("HRAR")
        intLOCPRAR = objSysLocDs.Tables(0).Rows(0).Item("PRAR")
        intLOCBIAR = objSysLocDs.Tables(0).Rows(0).Item("BIAR")
        intLOCPDAR = objSysLocDs.Tables(0).Rows(0).Item("PDAR")
        intLOCGLAR = objSysLocDs.Tables(0).Rows(0).Item("GLAR")
        intLOCADAR = objSysLocDs.Tables(0).Rows(0).Item("ADAR")
        intLOCWMAR = objSysLocDs.Tables(0).Rows(0).Item("WMAR")
        intLOCPMAR = objSysLocDs.Tables(0).Rows(0).Item("PMAR")
        intLOCCMAR = objSysLocDs.Tables(0).Rows(0).Item("CMAR")
        intLOCNUAR = objSysLocDs.Tables(0).Rows(0).Item("NUAR")
        intLOCFAAR = objSysLocDs.Tables(0).Rows(0).Item("FAAR")
        intLOCCBAR = objSysLocDs.Tables(0).Rows(0).Item("CBAR")
        strLocType = Trim(objSysLocDs.Tables(0).Rows(0).Item("LocType"))



        strParam = "MAS" & "|" & ddlLocation.SelectedItem.Value & "|" & strSelectedUserId
        Try
            intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCode_UserLoc, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                objUserLocDs, _
                                                strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERLOC_GET_SYSLOC2&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userdet.aspx?userid=" & strSelectedUserId)
        End Try

        If objUserLocDs.Tables(0).Rows.Count > 0 Then
            strExpiryDate = objUserLocDs.Tables(0).Rows(0).Item("ExpiryDate")
            intINAR = objUserLocDs.Tables(0).Rows(0).Item("INAR")
            intCTAR = objUserLocDs.Tables(0).Rows(0).Item("CTAR")
            intWSAR = objUserLocDs.Tables(0).Rows(0).Item("WSAR")
            intPUAR = objUserLocDs.Tables(0).Rows(0).Item("PUAR")
            intAPAR = objUserLocDs.Tables(0).Rows(0).Item("APAR")
            intHRAR = objUserLocDs.Tables(0).Rows(0).Item("HRAR")
            intPRAR = objUserLocDs.Tables(0).Rows(0).Item("PRAR")
            intBIAR = objUserLocDs.Tables(0).Rows(0).Item("BIAR")
            intPDAR = objUserLocDs.Tables(0).Rows(0).Item("PDAR")
            intGLAR = objUserLocDs.Tables(0).Rows(0).Item("GLAR")
            intWMAR = objUserLocDs.Tables(0).Rows(0).Item("WMAR")
            intPMAR = objUserLocDs.Tables(0).Rows(0).Item("PMAR")
            intCMAR = objUserLocDs.Tables(0).Rows(0).Item("CMAR")
            intNUAR = objUserLocDs.Tables(0).Rows(0).Item("NUAR")
            intADAR = objUserLocDs.Tables(0).Rows(0).Item("ADAR")
            intFAAR = objUserLocDs.Tables(0).Rows(0).Item("FAAR")
            intCBAR = objUserLocDs.Tables(0).Rows(0).Item("CBAR")
            DelBtn.Visible = True
        Else
            strExpiryDate = ""
            intINAR = 0
            intCTAR = 0
            intWSAR = 0
            intPUAR = 0
            intAPAR = 0
            intHRAR = 0
            intPRAR = 0
            intBIAR = 0
            intPDAR = 0
            intGLAR = 0
            intWMAR = 0
            intPMAR = 0
            intCMAR = 0
            intNUAR = 0
            intADAR = 0
            intFAAR = 0
            intCBAR = 0
            DelBtn.Visible = False
        End If

        txtAccessExpire.Text = IIf(objGlobal.mtdEmptyDate(strExpiryDate) = True, _
                               "", _
                               objGlobal.GetShortDate(strDateFormat, strExpiryDate))

        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Inventory), intModule) = False) Then
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
            cbINMthEnd.Enabled = False
            cbItemToMachine.Enabled = False
            cbStkTransferInternal.Enabled = False
            cbINProdBrand.Enabled = False
            cbINProdModel.Enabled = False
            cbINProdCategory.Enabled = False
            cbINProdMaterial.Enabled = False
            cbINStockAnalysis.Enabled = False
            cbINItemMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductSetup), intLOCINAR) = True Then
            cbINProdMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductSetup), intINAR) = True Then
                cbINProdMaster.Checked = True
            End If
        Else
            cbINProdMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItem), intLOCINAR) = True Then
            cbINItem.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItem), intINAR) = True Then
                cbINItem.Checked = True
            End If
        Else
            cbINItem.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDirectChargeItem), intLOCINAR) = True Then
            cbINDirChrg.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDirectChargeItem), intINAR) = True Then
                cbINDirChrg.Checked = True
            End If
        Else
            cbINDirChrg.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMiscItem), intLOCINAR) = True Then
            cbMiscItem.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMiscItem), intINAR) = True Then
                cbMiscItem.Checked = True
            End If
        Else
            cbMiscItem.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest), intLOCINAR) = True Then
            cbINPR.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest), intINAR) = True Then
                cbINPR.Checked = True
            End If
        Else
            cbINPR.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturnAdvice), intLOCINAR) = True Then
            cbStkRtnAdv.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturnAdvice), intINAR) = True Then
                cbStkRtnAdv.Checked = True
            End If
        Else
            cbStkRtnAdv.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer), intLOCINAR) = True Then
            cbStkTransfer.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer), intINAR) = True Then
                cbStkTransfer.Checked = True
            End If
        Else
            cbStkTransfer.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue), intLOCINAR) = True Then
            cbStkIsu.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue), intINAR) = True Then
                cbStkIsu.Checked = True
            End If
        Else
            cbStkIsu.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReceive), intLOCINAR) = True Then
            cbStkRcv.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReceive), intINAR) = True Then
                cbStkRcv.Checked = True
            End If
        Else
            cbStkRcv.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturn), intLOCINAR) = True Then
            cbStkRtn.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturn), intINAR) = True Then
                cbStkRtn.Checked = True
            End If
        Else
            cbStkRtn.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockAdjustment), intLOCINAR) = True Then
            cbStkAdj.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockAdjustment), intINAR) = True Then
                cbStkAdj.Checked = True
            End If
        Else
            cbStkAdj.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INFuelIssue), intLOCINAR) = True Then
            cbFuelIsu.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INFuelIssue), intINAR) = True Then
                cbFuelIsu.Checked = True
            End If
        Else
            cbFuelIsu.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDataTransfer), intLOCINAR) = True Then
            cbDtInv.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDataTransfer), intINAR) = True Then
                cbDtInv.Checked = True
            End If
        Else
            cbDtInv.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMonthEnd), intLOCINAR) = True Then
            cbINMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMonthEnd), intINAR) = True Then
                cbINMthEnd.Checked = True
            End If
        Else
            cbINMthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INItemToMachine), intLOCINAR) = True Then
            cbItemToMachine.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INItemToMachine), intINAR) = True Then
                cbItemToMachine.Checked = True
            End If
        Else
            cbItemToMachine.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransferInternal), intLOCINAR) = True Then
            cbStkTransferInternal.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransferInternal), intINAR) = True Then
                cbStkTransferInternal.Checked = True
            End If
        Else
            cbStkTransferInternal.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductBrand), intLOCINAR) = True Then
            cbINProdBrand.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductBrand), intINAR) = True Then
                cbINProdBrand.Checked = True
            End If
        Else
            cbINProdBrand.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductModel), intLOCINAR) = True Then
            cbINProdModel.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductModel), intINAR) = True Then
                cbINProdModel.Checked = True
            End If
        Else
            cbINProdModel.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductCategory), intLOCINAR) = True Then
            cbINProdCategory.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductCategory), intINAR) = True Then
                cbINProdCategory.Checked = True
            End If
        Else
            cbINProdCategory.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductMaterial), intLOCINAR) = True Then
            cbINProdMaterial.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductMaterial), intINAR) = True Then
                cbINProdMaterial.Checked = True
            End If
        Else
            cbINProdMaterial.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INAnalisisStok), intLOCINAR) = True Then
            cbINStockAnalysis.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INAnalisisStok), intINAR) = True Then
                cbINStockAnalysis.Checked = True
            End If
        Else
            cbINStockAnalysis.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItemMaster), intLOCINAR) = True Then
            cbINItemMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItemMaster), intINAR) = True Then
                cbINItemMaster.Checked = True
            End If
        Else
            cbINItemMaster.Enabled = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Canteen), intModule) = False) Then
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
            cbCTMthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster), intLOCCTAR) = True Then
            cbCTMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster), intCTAR) = True Then
                cbCTMaster.Checked = True
            End If
        Else
            cbCTMaster.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTItem), intLOCCTAR) = True Then
            cbCTItem.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTItem), intCTAR) = True Then
                cbCTItem.Checked = True
            End If
        Else
            cbCTItem.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTPurchaseRequest), intLOCCTAR) = True Then
            cbCTPR.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTPurchaseRequest), intCTAR) = True Then
                cbCTPR.Checked = True
            End If
        Else
            cbCTPR.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReceive), intLOCCTAR) = True Then
            cbCTRcv.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReceive), intCTAR) = True Then
                cbCTRcv.Checked = True
            End If
        Else
            cbCTRcv.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturnAdvice), intLOCCTAR) = True Then
            cbCTRtnAdv.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturnAdvice), intCTAR) = True Then
                cbCTRtnAdv.Checked = True
            End If
        Else
            cbCTRtnAdv.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTIssue), intLOCCTAR) = True Then
            cbCTIsu.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTIssue), intCTAR) = True Then
                cbCTIsu.Checked = True
            End If
        Else
            cbCTIsu.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturn), intLOCCTAR) = True Then
            cbCTRtn.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturn), intCTAR) = True Then
                cbCTRtn.Checked = True
            End If
        Else
            cbCTRtn.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTAdjustment), intLOCCTAR) = True Then
            cbCTAdj.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTAdjustment), intCTAR) = True Then
                cbCTAdj.Checked = True
            End If
        Else
            cbCTAdj.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTTransfer), intLOCCTAR) = True Then
            cbCTTransfer.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTTransfer), intCTAR) = True Then
                cbCTTransfer.Checked = True
            End If
        Else
            cbCTTransfer.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTDataTransfer), intLOCCTAR) = True Then
            cbDtCT.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTDataTransfer), intCTAR) = True Then
                cbDtCT.Checked = True
            End If
        Else
            cbDtCT.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMonthEnd), intLOCCTAR) = True Then
            cbCTMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMonthEnd), intCTAR) = True Then
                cbCTMthEnd.Checked = True
            End If
        Else
            cbCTMthEnd.Enabled = False
        End If


        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Workshop), intModule) = False) Then
            cbWSProdMaster.Enabled = False
            cbWSWorkMaster.Enabled = False
            cbWSItem.Enabled = False
            cbWSDirChrg.Enabled = False
            cbWSJob.Enabled = False
            cbWSMechHr.Enabled = False
            cbDtWS.Enabled = False
            cbWSMthEnd.Enabled = False
            cbWSPart.Enabled = False
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
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaster), intLOCWSAR) = True Then
            cbWSProdMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaster), intWSAR) = True Then
                cbWSProdMaster.Checked = True
            End If
        Else
            cbWSProdMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkMaster), intLOCWSAR) = True Then
            cbWSWorkMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkMaster), intWSAR) = True Then
                cbWSWorkMaster.Checked = True
            End If
        Else
            cbWSWorkMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItem), intLOCWSAR) = True Then
            cbWSItem.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItem), intWSAR) = True Then
                cbWSItem.Checked = True
            End If
        Else
            cbWSItem.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemPart), intLOCWSAR) = True Then
            cbWSPart.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemPart), intWSAR) = True Then
                cbWSPart.Checked = True
            End If
        Else
            cbWSPart.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItem), intLOCWSAR) = True Then
            cbWSDirChrg.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItem), intWSAR) = True Then
                cbWSDirChrg.Checked = True
            End If
        Else
            cbWSDirChrg.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration), intLOCWSAR) = True Then
            cbWSJob.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration), intWSAR) = True Then
                cbWSJob.Checked = True
            End If
        Else
            cbWSJob.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMechanicHour), intLOCWSAR) = True Then
            cbWSMechHr.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMechanicHour), intWSAR) = True Then
                cbWSMechHr.Checked = True
            End If
        Else
            cbWSMechHr.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDataTransfer), intLOCWSAR) = True Then
            cbDtWS.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDataTransfer), intWSAR) = True Then
                cbDtWS.Checked = True
            End If
        Else
            cbDtWS.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMonthEnd), intLOCWSAR) = True Then
            cbWSMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMonthEnd), intWSAR) = True Then
                cbWSMthEnd.Checked = True
            End If
        Else
            cbWSMthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductBrand), intLOCWSAR) = True Then
            cbWSProdBrand.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductBrand), intWSAR) = True Then
                cbWSProdBrand.Checked = True
            End If
        Else
            cbWSProdBrand.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductModel), intLOCWSAR) = True Then
            cbWSProdModel.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductModel), intWSAR) = True Then
                cbWSProdModel.Checked = True
            End If
        Else
            cbWSProdModel.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductCategory), intLOCWSAR) = True Then
            cbWSProdCategory.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductCategory), intWSAR) = True Then
                cbWSProdCategory.Checked = True
            End If
        Else
            cbWSProdCategory.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaterial), intLOCWSAR) = True Then
            cbWSProdMaterial.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaterial), intWSAR) = True Then
                cbWSProdMaterial.Checked = True
            End If
        Else
            cbWSProdMaterial.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSAnalisisStok), intLOCWSAR) = True Then
            cbWSStockAnalysis.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSAnalisisStok), intWSAR) = True Then
                cbWSStockAnalysis.Checked = True
            End If
        Else
            cbWSStockAnalysis.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemMaster), intLOCWSAR) = True Then
            cbWSItemMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemMaster), intWSAR) = True Then
                cbWSItemMaster.Checked = True
            End If
        Else
            cbWSItemMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkshopService), intLOCWSAR) = True Then
            cbWSWorkshopService.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkshopService), intWSAR) = True Then
                cbWSWorkshopService.Checked = True
            End If
        Else
            cbWSWorkshopService.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItemMaster), intLOCWSAR) = True Then
            cbWSDirChrgMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItemMaster), intWSAR) = True Then
                cbWSDirChrgMaster.Checked = True
            End If
        Else
            cbWSDirChrgMaster.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMillProcDistr), intLOCWSAR) = True Then
            cbWSMillProcDitr.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMillProcDistr), intWSAR) = True Then
                cbWSMillProcDitr.Checked = True
            End If
        Else
            cbWSMillProcDitr.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSCalMachine), intLOCWSAR) = True Then
            cbWSCalMachine.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSCalMachine), intWSAR) = True Then
                cbWSCalMachine.Checked = True
            End If
        Else
            cbWSCalMachine.Enabled = False
        End If


        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Purchasing), intModule) = False) Then
            cbPUSupp.Enabled = False
            cbPUPO.Enabled = False
            cbPUGoodsRcv.Enabled = False
            cbPUGRN.Enabled = False
            cbPUDA.Enabled = False
            cbDtPU.Enabled = False
            cbPUMthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier), intLOCPUAR) = True Then
            cbPUSupp.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier), intPUAR) = True Then
                cbPUSupp.Checked = True
            End If
        Else
            cbPUSupp.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder), intLOCPUAR) = True Then
            cbPUPO.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder), intPUAR) = True Then
                cbPUPO.Checked = True
            End If
        Else
            cbPUPO.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPelimpahan), intLOCPUAR) = True Then
            cbPUPelimpahan.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPelimpahan), intPUAR) = True Then
                cbPUPelimpahan.Checked = True
            End If
        Else
            cbPUPelimpahan.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PURPH), intLOCPUAR) = True Then
            cbPURPH.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PURPH), intPUAR) = True Then
                cbPURPH.Checked = True
            End If
        Else
            cbPURPH.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReceive), intLOCPUAR) = True Then
            cbPUGoodsRcv.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReceive), intPUAR) = True Then
                cbPUGoodsRcv.Checked = True
            End If
        Else
            cbPUGoodsRcv.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReturnNote), intLOCPUAR) = True Then
            cbPUGRN.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReturnNote), intPUAR) = True Then
                cbPUGRN.Checked = True
            End If
        Else
            cbPUGRN.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDispatchAdvice), intLOCPUAR) = True Then
            cbPUDA.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDispatchAdvice), intPUAR) = True Then
                cbPUDA.Checked = True
            End If
        Else
            cbPUDA.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDataTransfer), intLOCPUAR) = True Then
            cbDtPU.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDataTransfer), intPUAR) = True Then
                cbDtPU.Checked = True
            End If
        Else
            cbDtPU.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUMonthEnd), intLOCPUAR) = True Then
            cbPUMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUMonthEnd), intPUAR) = True Then
                cbPUMthEnd.Checked = True
            End If
        Else
            cbPUMthEnd.Enabled = False
        End If


        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.AccountPayable), intModule) = False) Then
            cbAPInvoice.Enabled = False
            cbAPDN.Enabled = False
            cbAPCN.Enabled = False
            cbAPCrtJrn.Enabled = False
            cbAPPay.Enabled = False
            cbAPMthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APInvoiceReceive), intLOCAPAR) = True Then
            cbAPInvoice.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APInvoiceReceive), intAPAR) = True Then
                cbAPInvoice.Checked = True
            End If
        Else
            cbAPInvoice.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APDebitNote), intLOCAPAR) = True Then
            cbAPDN.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APDebitNote), intAPAR) = True Then
                cbAPDN.Checked = True
            End If
        Else
            cbAPDN.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditNote), intLOCAPAR) = True Then
            cbAPCN.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditNote), intAPAR) = True Then
                cbAPCN.Checked = True
            End If
        Else
            cbAPCN.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditorJournal), intLOCAPAR) = True Then
            cbAPCrtJrn.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditorJournal), intAPAR) = True Then
                cbAPCrtJrn.Checked = True
            End If
        Else
            cbAPCrtJrn.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APPayment), intLOCAPAR) = True Then
            cbAPPay.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APPayment), intAPAR) = True Then
                cbAPPay.Checked = True
            End If
        Else
            cbAPPay.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APMonthEnd), intLOCAPAR) = True Then
            cbAPMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APMonthEnd), intAPAR) = True Then
                cbAPMthEnd.Checked = True
            End If
        Else
            cbAPMthEnd.Enabled = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.HumanResource), intModule) = False) Then
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
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDepartment), intLOCHRAR) = True Then
            cbHRDepartment.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDepartment), intHRAR) = True Then
                cbHRDepartment.Checked = True
            End If
        Else
            cbHRDepartment.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intLOCHRAR) = True Then
            cbHRCompany.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = True Then
                cbHRCompany.Checked = True
            End If
        Else
            cbHRCompany.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRFunction), intLOCHRAR) = True Then
            cbHRFunc.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRFunction), intHRAR) = True Then
                cbHRFunc.Checked = True
            End If
        Else
            cbHRFunc.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSkill), intLOCHRAR) = True Then
            cbHRSkill.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSkill), intHRAR) = True Then
                cbHRSkill.Checked = True
            End If
        Else
            cbHRSkill.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREvaluation), intLOCHRAR) = True Then
            cbHREval.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREvaluation), intHRAR) = True Then
                cbHREval.Checked = True
            End If
        Else
            cbHREval.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBank), intLOCHRAR) = True Then
            cbHRBank.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBank), intHRAR) = True Then
                cbHRBank.Checked = True
            End If
        Else
            cbHRBank.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRHoliday), intLOCHRAR) = True Then
            cbHRHoliday.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRHoliday), intHRAR) = True Then
                cbHRHoliday.Checked = True
            End If
        Else
            cbHRHoliday.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCareerProgress), intLOCHRAR) = True Then
            cbHRCP.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCareerProgress), intHRAR) = True Then
                cbHRCP.Checked = True
            End If
        Else
            cbHRCP.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intLOCHRAR) = True Then
            cbHREmpDet.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intHRAR) = True Then
                cbHREmpDet.Checked = True
            End If
        Else
            cbHREmpDet.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeePayroll), intLOCHRAR) = True Then
            cbHREmpPR.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeePayroll), intHRAR) = True Then
                cbHREmpPR.Checked = True
            End If
        Else
            cbHREmpPR.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeEmployement), intLOCHRAR) = True Then
            cbHREmpEmploy.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeEmployement), intHRAR) = True Then
                cbHREmpEmploy.Checked = True
            End If
        Else
            cbHREmpEmploy.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeStatutory), intLOCHRAR) = True Then
            cbHRSat.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeStatutory), intHRAR) = True Then
                cbHRSat.Checked = True
            End If
        Else
            cbHRSat.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeFamily), intLOCHRAR) = True Then
            cbHREmpFam.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeFamily), intHRAR) = True Then
                cbHREmpFam.Checked = True
            End If
        Else
            cbHREmpFam.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeQualification), intLOCHRAR) = True Then
            cbHREmpQlf.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeQualification), intHRAR) = True Then
                cbHREmpQlf.Checked = True
            End If
        Else
            cbHREmpQlf.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeSkill), intLOCHRAR) = True Then
            cbHREmpSkill.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeSkill), intHRAR) = True Then
                cbHREmpSkill.Checked = True
            End If
        Else
            cbHREmpSkill.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRContractorSupervision), intLOCHRAR) = True Then
            cbHRContSuper.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRContractorSupervision), intHRAR) = True Then
                cbHRContSuper.Checked = True
            End If
        Else
            cbHRContSuper.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGenerateEmpCode), intLOCHRAR) = True Then
            cbGenEmpCode.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGenerateEmpCode), intHRAR) = True Then
                cbGenEmpCode.Checked = True
            End If
        Else
            cbGenEmpCode.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDataTransfer), intLOCHRAR) = True Then
            cbDtHR.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDataTransfer), intHRAR) = True Then
                cbDtHR.Checked = True
            End If
        Else
            cbDtHR.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRNationality), intLOCHRAR) = True Then
            cbHRNationality.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRNationality), intHRAR) = True Then
                cbHRNationality.Checked = True
            End If
        Else
            cbHRNationality.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPosition), intLOCHRAR) = True Then
            cbHRPosition.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPosition), intHRAR) = True Then
                cbHRPosition.Checked = True
            End If
        Else
            cbHRPosition.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRLevel), intLOCHRAR) = True Then
            cbHRLevel.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRLevel), intHRAR) = True Then
                cbHRLevel.Checked = True
            End If
        Else
            cbHRLevel.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRReligion), intLOCHRAR) = True Then
            cbHRReligion.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRReligion), intHRAR) = True Then
                cbHRReligion.Checked = True
            End If
        Else
            cbHRReligion.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRICType), intLOCHRAR) = True Then
            cbHRICType.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRICType), intHRAR) = True Then
                cbHRICType.Checked = True
            End If
        Else
            cbHRICType.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRRace), intLOCHRAR) = True Then
            cbHRRace.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRRace), intHRAR) = True Then
                cbHRRace.Checked = True
            End If
        Else
            cbHRRace.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRQualification), intLOCHRAR) = True Then
            cbHRQualification.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRQualification), intHRAR) = True Then
                cbHRQualification.Checked = True
            End If
        Else
            cbHRQualification.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSubject), intLOCHRAR) = True Then
            cbHRSubject.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSubject), intHRAR) = True Then
                cbHRSubject.Checked = True
            End If
        Else
            cbHRSubject.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCPCode), intLOCHRAR) = True Then
            cbHRCPCode.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCPCode), intHRAR) = True Then
                cbHRCPCode.Checked = True
            End If
        Else
            cbHRCPCode.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalScheme), intLOCHRAR) = True Then
            cbHRSalScheme.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalScheme), intHRAR) = True Then
                cbHRSalScheme.Checked = True
            End If
        Else
            cbHRSalScheme.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalGrade), intLOCHRAR) = True Then
            cbHRSalGrade.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalGrade), intHRAR) = True Then
                cbHRSalGrade.Checked = True
            End If
        Else
            cbHRSalGrade.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRShift), intLOCHRAR) = True Then
            cbHRShift.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRShift), intHRAR) = True Then
                cbHRShift.Checked = True
            End If
        Else
            cbHRShift.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGang), intLOCHRAR) = True Then
            cbHRGang.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGang), intHRAR) = True Then
                cbHRGang.Checked = True
            End If
        Else
            cbHRGang.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBankFormat), intLOCHRAR) = True Then
            cbHRBankFormat.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBankFormat), intHRAR) = True Then
                cbHRBankFormat.Checked = True
            End If
        Else
            cbHRBankFormat.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRJamsostek), intLOCHRAR) = True Then
            cbHRJamsostek.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRJamsostek), intHRAR) = True Then
                cbHRJamsostek.Checked = True
            End If
        Else
            cbHRJamsostek.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTaxBranch), intLOCHRAR) = True Then
            cbHRTaxBranch.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTaxBranch), intHRAR) = True Then
                cbHRTaxBranch.Checked = True
            End If
        Else
            cbHRTaxBranch.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTax), intLOCHRAR) = True Then
            cbHRTax.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTax), intHRAR) = True Then
                cbHRTax.Checked = True
            End If
        Else
            cbHRTax.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPublicHoliday), intLOCHRAR) = True Then
            cbHRPublicHoliday.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPublicHoliday), intHRAR) = True Then
                cbHRPublicHoliday.Checked = True
            End If
        Else
            cbHRPublicHoliday.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPointHired), intLOCHRAR) = True Then
            cbHRPOH.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPointHired), intHRAR) = True Then
                cbHRPOH.Checked = True
            End If
        Else
            cbHRPOH.Enabled = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Payroll), intModule) = False) Then
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
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAllowanceDeduction), intLOCPRAR) = True Then
            cbPRAD.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAllowanceDeduction), intPRAR) = True Then
                cbPRAD.Checked = True
            End If
        Else
            cbPRAD.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalary), intLOCPRAR) = True Then
            cbPRSal.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalary), intPRAR) = True Then
                cbPRSal.Checked = True
            End If
        Else
            cbPRSal.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRContract), intLOCPRAR) = True Then
            cbPRContract.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRContract), intPRAR) = True Then
                cbPRContract.Checked = True
            End If
        Else
            cbPRContract.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRice), intLOCPRAR) = True Then
            cbPRRice.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRice), intPRAR) = True Then
                cbPRRice.Checked = True
            End If
        Else
            cbPRRice.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PREmployeeEvaluation), intLOCPRAR) = True Then
            cbPREmployeeEvaluation.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PREmployeeEvaluation), intPRAR) = True Then
                cbPREmployeeEvaluation.Checked = True
            End If
        Else
            cbPREmployeeEvaluation.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRStandardEvaluation), intLOCPRAR) = True Then
            cbPRStandardEvaluation.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRStandardEvaluation), intPRAR) = True Then
                cbPRStandardEvaluation.Checked = True
            End If
        Else
            cbPRStandardEvaluation.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalaryIncrease), intLOCPRAR) = True Then
            cbPRSalaryIncrease.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalaryIncrease), intPRAR) = True Then
                cbPRSalaryIncrease.Checked = True
            End If
        Else
            cbPRSalaryIncrease.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTransportIncentive), intLOCPRAR) = True Then
            cbPRTranInc.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTransportIncentive), intPRAR) = True Then
                cbPRTranInc.Checked = True
            End If
        Else
            cbPRTranInc.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAttendance), intLOCPRAR) = True Then
            cbPRAttdTrx.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAttendance), intPRAR) = True Then
                cbPRAttdTrx.Checked = True
            End If
        Else
            cbPRAttdTrx.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxTrip), intLOCPRAR) = True Then
            cbPRTripTrx.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxTrip), intPRAR) = True Then
                cbPRTripTrx.Checked = True
            End If
        Else
            cbPRTripTrx.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxRatePayment), intLOCPRAR) = True Then
            cbPRRatePay.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxRatePayment), intPRAR) = True Then
                cbPRRatePay.Checked = True
            End If
        Else
            cbPRRatePay.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAllowanceDeduction), intLOCPRAR) = True Then
            cbPRADTrx.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAllowanceDeduction), intPRAR) = True Then
                cbPRADTrx.Checked = True
            End If
        Else
            cbPRADTrx.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractorCheckroll), intLOCPRAR) = True Then
            cbPRContCheckroll.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractorCheckroll), intPRAR) = True Then
                cbPRContCheckroll.Checked = True
            End If
        Else
            cbPRContCheckroll.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intLOCPRAR) = True Then
            cbPRWorkPerformance.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = True Then
                cbPRWorkPerformance.Checked = True
            End If
        Else
            cbPRWorkPerformance.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWPContractor), intLOCPRAR) = True Then
            cbPRWorkPerformance.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWPContractor), intPRAR) = True Then
                cbPRWPContractor.Checked = True
            End If
        Else
            cbPRWPContractor.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDataTransfer), intLOCPRAR) = True Then
            cbDtPR.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDataTransfer), intPRAR) = True Then
                cbDtPR.Checked = True
            End If
        Else
            cbDtPR.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownload), intLOCPRAR) = True Then
            cbDwPR.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownload), intPRAR) = True Then
                cbDwPR.Checked = True
            End If
        Else
            cbDwPR.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMonthEnd), intLOCPRAR) = True Then
            cbPRMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMonthEnd), intPRAR) = True Then
                cbPRMthEnd.Checked = True
            End If
        Else
            cbPRMthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRYearEnd), intLOCPRAR) = True Then
            cbPRYearEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRYearEnd), intPRAR) = True Then
                cbPRYearEnd.Checked = True
            End If
        Else
            cbPRYearEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRADGroup), intLOCPRAR) = True Then
            cbPRADGroup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRADGroup), intPRAR) = True Then
                cbPRADGroup.Checked = True
            End If
        Else
            cbPRADGroup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDenda), intLOCPRAR) = True Then
            cbPRDenda.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDenda), intPRAR) = True Then
                cbPRDenda.Checked = True
            End If
        Else
            cbPRDenda.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvInc), intLOCPRAR) = True Then
            cbPRHarvInc.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvInc), intPRAR) = True Then
                cbPRHarvInc.Checked = True
            End If
        Else
            cbPRHarvInc.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRLoad), intLOCPRAR) = True Then
            cbPRLoad.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRLoad), intPRAR) = True Then
                cbPRLoad.Checked = True
            End If
        Else
            cbPRLoad.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRoute), intLOCPRAR) = True Then
            cbPRRoute.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRoute), intPRAR) = True Then
                cbPRRoute.Checked = True
            End If
        Else
            cbPRRoute.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMedical), intLOCPRAR) = True Then
            cbPRMedical.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMedical), intPRAR) = True Then
                cbPRMedical.Checked = True
            End If
        Else
            cbPRMedical.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAirBus), intLOCPRAR) = True Then
            cbPRAirBus.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAirBus), intPRAR) = True Then
                cbPRAirBus.Checked = True
            End If
        Else
            cbPRAirBus.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMaternity), intLOCPRAR) = True Then
            cbPRMaternity.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMaternity), intPRAR) = True Then
                cbPRMaternity.Checked = True
            End If
        Else
            cbPRMaternity.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPensiun), intLOCPRAR) = True Then
            cbPRPensiun.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPensiun), intPRAR) = True Then
                cbPRPensiun.Checked = True
            End If
        Else
            cbPRPensiun.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRelocation), intLOCPRAR) = True Then
            cbPRRelocation.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRelocation), intPRAR) = True Then
                cbPRRelocation.Checked = True
            End If
        Else
            cbPRRelocation.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPremi), intLOCPRAR) = True Then
            cbPRIncentive.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPremi), intPRAR) = True Then
                cbPRIncentive.Checked = True
            End If
        Else
            cbPRIncentive.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractPayment), intLOCPRAR) = True Then
            cbPRContractPay.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractPayment), intPRAR) = True Then
                cbPRContractPay.Checked = True
            End If
        Else
            cbPRContractPay.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWagesPayment), intLOCPRAR) = True Then
            cbPRWagesPay.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWagesPayment), intPRAR) = True Then
                cbPRWagesPay.Checked = True
            End If
        Else
            cbPRWagesPay.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadWages), intLOCPRAR) = True Then
            cbDwPRWages.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadWages), intPRAR) = True Then
                cbDwPRWages.Checked = True
            End If
        Else
            cbDwPRWages.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadBankAuto), intLOCPRAR) = True Then
            cbDwPRBankAuto.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadBankAuto), intPRAR) = True Then
                cbDwPRBankAuto.Checked = True
            End If
        Else
            cbDwPRBankAuto.Enabled = False
        End If



        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intLOCPRAR) = True Then
            cbPRPaySetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = True Then
                cbPRPaySetup.Checked = True
            End If
        Else
            cbPRPaySetup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDailyAttd), intLOCPRAR) = True Then
            cbPRDailyAttd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDailyAttd), intPRAR) = True Then
                cbPRDailyAttd.Checked = True
            End If
        Else
            cbPRDailyAttd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvAttd), intLOCPRAR) = True Then
            cbPRHarvAttd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvAttd), intPRAR) = True Then
                cbPRHarvAttd.Checked = True
            End If
        Else
            cbPRHarvAttd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRWeekly), intLOCPRAR) = True Then
            cbPRWeekly.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRWeekly), intPRAR) = True Then
                cbPRWeekly.Checked = True
            End If
        Else
            cbPRWeekly.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRice), intLOCPRAR) = True Then
            cbPRMthRice.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRice), intPRAR) = True Then
                cbPRMthRice.Checked = True
            End If
        Else
            cbPRMthRice.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRapel), intLOCPRAR) = True Then
            cbPRMthRapel.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRapel), intPRAR) = True Then
                cbPRMthRapel.Checked = True
            End If
        Else
            cbPRMthRapel.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthBonus), intLOCPRAR) = True Then
            cbPRMthBonus.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthBonus), intPRAR) = True Then
                cbPRMthBonus.Checked = True
            End If
        Else
            cbPRMthBonus.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTHR), intLOCPRAR) = True Then
            cbPRMthTHR.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTHR), intPRAR) = True Then
                cbPRMthTHR.Checked = True
            End If
        Else
            cbPRMthTHR.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthDaily), intLOCPRAR) = True Then
            cbPRMthDaily.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthDaily), intPRAR) = True Then
                cbPRMthDaily.Checked = True
            End If
        Else
            cbPRMthDaily.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intLOCPRAR) = True Then
            cbPRMthPayroll.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = True Then
                cbPRMthPayroll.Checked = True
            End If
        Else
            cbPRMthPayroll.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTransfer), intLOCPRAR) = True Then
            cbPRMthTransfer.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTransfer), intPRAR) = True Then
                cbPRMthTransfer.Checked = True
            End If
        Else
            cbPRMthTransfer.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDwAttdInterface), intLOCPRAR) = True Then
            cbDwAttdInterface.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDwAttdInterface), intPRAR) = True Then
                cbDwAttdInterface.Checked = True
            End If
        Else
            cbDwAttdInterface.Enabled = False
        End If



        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Billing), intModule) = False) Then
            cbBillParty.Enabled = False
            cbBINote.Enabled = False
            cbDtBI.Enabled = False
            cbBIMthEnd.Enabled = False
            cbBICreditNote.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIBillParty), intLOCBIAR) = True Then
            cbBillParty.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIBillParty), intBIAR) = True Then
                cbBillParty.Checked = True
            End If
        Else
            cbBillParty.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BINote), intLOCBIAR) = True Then
            cbBINote.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BINote), intBIAR) = True Then
                cbBINote.Checked = True
            End If
        Else
            cbBINote.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIDataTransfer), intLOCBIAR) = True Then
            cbDtBI.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIDataTransfer), intBIAR) = True Then
                cbDtBI.Checked = True
            End If
        Else
            cbDtBI.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIMonthEnd), intLOCBIAR) = True Then
            cbBIMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIMonthEnd), intBIAR) = True Then
                cbBIMthEnd.Checked = True
            End If
        Else
            cbBIMthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIInvoice), intLOCBIAR) = True Then
            cbBIInvoice.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIInvoice), intBIAR) = True Then
                cbBIInvoice.Checked = True
            End If
        Else
            cbBIInvoice.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIReceipt), intLOCBIAR) = True Then
            cbBIReceipt.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIReceipt), intBIAR) = True Then
                cbBIReceipt.Checked = True
            End If
        Else
            cbBIReceipt.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIJournal), intLOCBIAR) = True Then
            cbBIJournal.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIJournal), intBIAR) = True Then
                cbBIJournal.Checked = True
            End If
        Else
            cbBIJournal.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BICreditNote), intLOCBIAR) = True Then
            cbBICreditNote.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BICreditNote), intBIAR) = True Then
                cbBICreditNote.Checked = True
            End If
        Else
            cbBICreditNote.Enabled = False
        End If


        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Production), intModule) = False) Then
            cbEstProd.Enabled = False
            cbPOMProd.Enabled = False
            cbPDMthEnd.Enabled = False
            cbMPOBPrice.Enabled = False
            cbYearPlantYield.Enabled = False
            cbPOMStorage.Enabled = False
            cbPOMStat.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDEstateProduction), intLOCPDAR) = True Then
            cbEstProd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDEstateProduction), intPDAR) = True Then
                cbEstProd.Checked = True
            End If
        Else
            cbEstProd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMProduction), intLOCPDAR) = True Then
            cbPOMProd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMProduction), intPDAR) = True Then
                cbPOMProd.Checked = True
            End If
        Else
            cbPOMProd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMPOBPrice), intLOCPDAR) = True Then
            cbMPOBPrice.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMPOBPrice), intPDAR) = True Then
                cbMPOBPrice.Checked = True
            End If
        Else
            cbMPOBPrice.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDYearOfPlantYield), intLOCPDAR) = True Then
            cbYearPlantYield.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDYearOfPlantYield), intPDAR) = True Then
                cbYearPlantYield.Checked = True
            End If
        Else
            cbYearPlantYield.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMonthEnd), intLOCPDAR) = True Then
            cbPDMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMonthEnd), intPDAR) = True Then
                cbPDMthEnd.Checked = True
            End If
        Else
            cbPDMthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStorage), intLOCPDAR) = True Then
            cbPOMStorage.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStorage), intPDAR) = True Then
                cbPOMStorage.Checked = True
            End If
        Else
            cbPOMStorage.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStatistics), intLOCPDAR) = True Then
            cbPOMStat.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStatistics), intPDAR) = True Then
                cbPOMStat.Checked = True
            End If
        Else
            cbPOMStat.Enabled = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.GeneralLedger), intModule) = False) Then
            cbAccCls.Enabled = False
            cbAct.Enabled = False
            cbVehExp.Enabled = False
            cbVeh.Enabled = False
            cbVehType.Enabled = False
            cbBlkGrp.Enabled = False
            cbBlk.Enabled = False
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
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccCls), intLOCGLAR) = True Then
            cbAccCls.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccCls), intGLAR) = True Then
                cbAccCls.Checked = True
            End If
        Else
            cbAccCls.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivity), intLOCGLAR) = True Then
            cbAct.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivity), intGLAR) = True Then
                cbAct.Checked = True
            End If
        Else
            cbAct.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpense), intLOCGLAR) = True Then
            cbVehExp.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpense), intGLAR) = True Then
                cbVehExp.Checked = True
            End If
        Else
            cbVehExp.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intLOCGLAR) = True Then
            cbVeh.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intGLAR) = True Then
                cbVeh.Checked = True
            End If
        Else
            cbVeh.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intLOCGLAR) = True Then
            cbVehType.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleType), intGLAR) = True Then
                cbVehType.Checked = True
            End If
        Else
            cbVehType.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlock), intLOCGLAR) = True Then
            cbBlkGrp.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlock), intGLAR) = True Then
                cbBlkGrp.Checked = True
            End If
        Else
            cbBlkGrp.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlk), intLOCGLAR) = True Then
            cbBlk.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlk), intGLAR) = True Then
                cbBlk.Checked = True
            End If
        Else
            cbBlk.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLExpense), intLOCGLAR) = True Then
            cbExp.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLExpense), intGLAR) = True Then
                cbExp.Checked = True
            End If
        Else
            cbExp.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccount), intLOCGLAR) = True Then
            cbAccount.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccount), intGLAR) = True Then
                cbAccount.Checked = True
            End If
        Else
            cbAccount.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLEntrySetup), intLOCGLAR) = True Then
            cbEntrySetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLEntrySetup), intGLAR) = True Then
                cbEntrySetup.Checked = True
            End If
        Else
            cbEntrySetup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBalSheetSetup), intLOCGLAR) = True Then
            cbBalSheetSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBalSheetSetup), intGLAR) = True Then
                cbBalSheetSetup.Checked = True
            End If
        Else
            cbBalSheetSetup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLProfLossSetup), intLOCGLAR) = True Then
            cbProfLossSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLProfLossSetup), intGLAR) = True Then
                cbProfLossSetup.Checked = True
            End If
        Else
            cbProfLossSetup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal), intLOCGLAR) = True Then
            cbJrn.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal), intGLAR) = True Then
                cbJrn.Checked = True
            End If
        Else
            cbJrn.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournalAdj), intLOCGLAR) = True Then
            cbJrnAdj.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournalAdj), intGLAR) = True Then
                cbJrnAdj.Checked = True
            End If
        Else
            cbJrnAdj.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleUsage), intLOCGLAR) = True Then
            cbVehUsg.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleUsage), intGLAR) = True Then
                cbVehUsg.Checked = True
            End If
        Else
            cbVehUsg.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLDataTransfer), intLOCGLAR) = True Then
            cbDtGL.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLDataTransfer), intGLAR) = True Then
                cbDtGL.Checked = True
            End If
        Else
            cbDtGL.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLUpload), intLOCGLAR) = True Then
            cbDtUp.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLUpload), intGLAR) = True Then
                cbDtUp.Checked = True
            End If
        Else
            cbDtUp.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMthEndGCDist), intLOCGLAR) = True Then
            cbGLGCDist.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMthEndGCDist), intGLAR) = True Then
                cbGLGCDist.Checked = True
            End If
        Else
            cbGLGCDist.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJrnMonthEnd), intLOCGLAR) = True Then
            cbGLJrnMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJrnMonthEnd), intGLAR) = True Then
                cbGLJrnMthEnd.Checked = True
            End If
        Else
            cbGLJrnMthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intLOCGLAR) = True Then
            cbGLMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = True Then
                cbGLMthEnd.Checked = True
            End If
        Else
            cbGLMthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLRunHour), intLOCGLAR) = True Then
            cbRunHour.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLRunHour), intGLAR) = True Then
                cbRunHour.Checked = True
            End If
        Else
            cbRunHour.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccClsGrp), intLOCGLAR) = True Then
            cbAccClsGrp.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccClsGrp), intGLAR) = True Then
                cbAccClsGrp.Checked = True
            Else
            End If
        Else
            cbAccClsGrp.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivityGrp), intLOCGLAR) = True Then
            cbActGrp.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivityGrp), intGLAR) = True Then
                cbActGrp.Checked = True
            End If
        Else
            cbActGrp.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubActivity), intLOCGLAR) = True Then
            cbSubAct.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubActivity), intGLAR) = True Then
                cbSubAct.Checked = True
            End If
        Else
            cbSubAct.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpenseGrp), intLOCGLAR) = True Then
            cbVehExpGrp.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpenseGrp), intGLAR) = True Then
                cbVehExpGrp.Checked = True
            End If
        Else
            cbVehExpGrp.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubBlk), intLOCGLAR) = True Then
            cbSubBlk.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubBlk), intGLAR) = True Then
                cbSubBlk.Checked = True
            End If
        Else
            cbSubBlk.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccountGrp), intLOCGLAR) = True Then
            cbAccountGrp.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccountGrp), intGLAR) = True Then
                cbAccountGrp.Checked = True
            End If
        Else
            cbAccountGrp.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLPosting), intLOCGLAR) = True Then
            cbPosting.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLPosting), intGLAR) = True Then
                cbPosting.Checked = True
            End If
        Else
            cbPosting.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleType), intLOCGLAR) = True Then
            cbVehType.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleType), intGLAR) = True Then
                cbVehType.Checked = True
            End If
        Else
            cbVehType.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlk), intLOCGLAR) = True Then
            cbBlk.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlk), intGLAR) = True Then
                cbBlk.Checked = True
            End If
        Else
            cbBlk.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLCOGS), intLOCGLAR) = True Then
            cbGLCOGS.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLCOGS), intGLAR) = True Then
                cbGLCOGS.Checked = True
            End If
        Else
            cbGLCOGS.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLFSSETUP), intLOCGLAR) = True Then
            cbFSSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLFSSETUP), intGLAR) = True Then
                cbFSSetup.Checked = True
            End If
        Else
            cbFSSetup.Enabled = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Reconciliation), intModule) = False) Then
            cbRCDA.Enabled = False
            cbRCJrn.Enabled = False
            cbReadInterRC.Enabled = False
            cbSendInterRC.Enabled = False
            cbDtRC.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDispatchAdvice), intLOCADAR) = True Then
            cbRCDA.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDispatchAdvice), intADAR) = True Then
                cbRCDA.Checked = True
            End If
        Else
            cbRCDA.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCJournal), intLOCADAR) = True Then
            cbRCJrn.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCJournal), intADAR) = True Then
                cbRCJrn.Checked = True
            End If
        Else
            cbRCJrn.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCReadInterface), intLOCADAR) = True Then
            cbReadInterRC.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCReadInterface), intADAR) = True Then
                cbReadInterRC.Checked = True
            End If
        Else
            cbReadInterRC.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCSendInterface), intLOCADAR) = True Then
            cbSendInterRC.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCSendInterface), intADAR) = True Then
                cbSendInterRC.Checked = True
            End If
        Else
            cbSendInterRC.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDataTransfer), intLOCADAR) = True Then
            cbDtRC.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDataTransfer), intADAR) = True Then
                cbDtRC.Checked = True
            End If
        Else
            cbDtRC.Enabled = False
        End If


        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillWeighing), intModule) = False) Then
            cbWMTransport.Enabled = False
            cbWMTicket.Enabled = False
            cbWMFFBAssessment.Enabled = False
            cbWMDataTransfer.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMTransporterSetup), intLOCWMAR) = True Then
            cbWMTransport.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMTransporterSetup), intWMAR) = True Then
                cbWMTransport.Checked = True
            End If
        Else
            cbWMTransport.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket), intLOCWMAR) = True Then
            cbWMTicket.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket), intWMAR) = True Then
                cbWMTicket.Checked = True
            End If
        Else
            cbWMTicket.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMFFBAssessment), intLOCWMAR) = True Then
            cbWMFFBAssessment.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMFFBAssessment), intWMAR) = True Then
                cbWMFFBAssessment.Checked = True
            End If
        Else
            cbWMFFBAssessment.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMDataTransfer), intLOCWMAR) = True Then
            cbWMDataTransfer.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMDataTransfer), intWMAR) = True Then
                cbWMDataTransfer.Checked = True
            End If
        Else
            cbWMDataTransfer.Enabled = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillProduction), intModule) = False) Then
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
            cbPMKernelLoss.Enabled = False
            cbPMWastedWaterQuality.Enabled = False
            cbPMMachineCriteria.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMasterSetup), intLOCPMAR) = True Then
            cbPMMasterSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMasterSetup), intPMAR) = True Then
                cbPMMasterSetup.Checked = True
            End If
        Else
            cbPMMasterSetup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMVolConvMaster), intLOCPMAR) = True Then
            cbPMVolConvMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMVolConvMaster), intPMAR) = True Then
                cbPMVolConvMaster.Checked = True
            End If
        Else
            cbPMVolConvMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAvgCapConvMaster), intLOCPMAR) = True Then
            cbPMAvgCapConvMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAvgCapConvMaster), intPMAR) = True Then
                cbPMAvgCapConvMaster.Checked = True
            End If
        Else
            cbPMAvgCapConvMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOPropertyMaster), intLOCPMAR) = True Then
            cbPMCPOPropertyMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOPropertyMaster), intPMAR) = True Then
                cbPMCPOPropertyMaster.Checked = True
            End If
        Else
            cbPMCPOPropertyMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageTypeMaster), intLOCPMAR) = True Then
            cbPMStorageTypeMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageTypeMaster), intPMAR) = True Then
                cbPMStorageTypeMaster.Checked = True
            End If
        Else
            cbPMStorageTypeMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageAreaMaster), intLOCPMAR) = True Then
            cbPMStorageAreaMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageAreaMaster), intPMAR) = True Then
                cbPMStorageAreaMaster.Checked = True
            End If
        Else
            cbPMStorageAreaMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProcessingLineMaster), intLOCPMAR) = True Then
            cbPMProcessingLineMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProcessingLineMaster), intPMAR) = True Then
                cbPMProcessingLineMaster.Checked = True
            End If
        Else
            cbPMProcessingLineMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineMaster), intLOCPMAR) = True Then
            cbPMMachineMaster.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineMaster), intPMAR) = True Then
                cbPMMachineMaster.Checked = True
            End If
        Else
            cbPMMachineMaster.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableOilQuality), intLOCPMAR) = True Then
            cbPMAcceptableOilQuality.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableOilQuality), intPMAR) = True Then
                cbPMAcceptableOilQuality.Checked = True
            End If
        Else
            cbPMAcceptableOilQuality.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableKernelQuality), intLOCPMAR) = True Then
            cbPMAcceptableKernelQuality.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableKernelQuality), intPMAR) = True Then
                cbPMAcceptableKernelQuality.Checked = True
            End If
        Else
            cbPMAcceptableKernelQuality.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMTestSample), intLOCPMAR) = True Then
            cbPMTestSample.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMTestSample), intPMAR) = True Then
                cbPMTestSample.Checked = True
            End If
        Else
            cbPMTestSample.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMHarvestingInterval), intLOCPMAR) = True Then
            cbPMHarvestingInterval.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMHarvestingInterval), intPMAR) = True Then
                cbPMHarvestingInterval.Checked = True
            End If
        Else
            cbPMHarvestingInterval.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineCriteria), intLOCPMAR) = True Then
            cbPMMachineCriteria.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineCriteria), intPMAR) = True Then
                cbPMMachineCriteria.Checked = True
            End If
        Else
            cbPMMachineCriteria.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMill), intLOCPMAR) = True Then
            cbPMMill.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMill), intPMAR) = True Then
                cbPMMill.Checked = True
            End If
        Else
            cbPMMill.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction), intLOCPMAR) = True Then
            cbPMDailyProd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction), intPMAR) = True Then
                cbPMDailyProd.Checked = True
            End If
        Else
            cbPMDailyProd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOStorage), intLOCPMAR) = True Then
            cbPMCPOStore.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOStorage), intPMAR) = True Then
                cbPMCPOStore.Checked = True
            End If
        Else
            cbPMCPOStore.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMPKStorage), intLOCPMAR) = True Then
            cbPMPKStore.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMPKStorage), intPMAR) = True Then
                cbPMPKStore.Checked = True
            End If
        Else
            cbPMPKStore.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilLoss), intLOCPMAR) = True Then
            cbPMOilLoss.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilLoss), intPMAR) = True Then
                cbPMOilLoss.Checked = True
            End If
        Else
            cbPMOilLoss.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilQuality), intLOCPMAR) = True Then
            cbPMOilQuality.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilQuality), intPMAR) = True Then
                cbPMOilQuality.Checked = True
            End If
        Else
            cbPMOilQuality.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelQuality), intLOCPMAR) = True Then
            cbPMKernelQuality.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelQuality), intPMAR) = True Then
                cbPMKernelQuality.Checked = True
            End If
        Else
            cbPMKernelQuality.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProducedKernel), intLOCPMAR) = True Then
            cbPMProdKernel.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProducedKernel), intPMAR) = True Then
                cbPMProdKernel.Checked = True
            End If
        Else
            cbPMProdKernel.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProducedKernel), intLOCPMAR) = True Then
            cbPMDispKernel.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProducedKernel), intPMAR) = True Then
                cbPMDispKernel.Checked = True
            End If
        Else
            cbPMDispKernel.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWaterQuality), intLOCPMAR) = True Then
            cbPMWater.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWaterQuality), intPMAR) = True Then
                cbPMWater.Checked = True
            End If
        Else
            cbPMWater.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMNutToFibre), intLOCPMAR) = True Then
            cbPMNutFibre.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMNutToFibre), intPMAR) = True Then
                cbPMNutFibre.Checked = True
            End If
        Else
            cbPMNutFibre.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDayEnd), intLOCPMAR) = True Then
            cbPMDayEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDayEnd), intPMAR) = True Then
                cbPMDayEnd.Checked = True
            End If
        Else
            cbPMDayEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMonthEnd), intLOCPMAR) = True Then
            cbPMMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMonthEnd), intPMAR) = True Then
                cbPMMthEnd.Checked = True
            End If
        Else
            cbPMMthEnd.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelLoss), intLOCPMAR) = True Then
            cbPMKernelLoss.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelLoss), intPMAR) = True Then
                cbPMKernelLoss.Checked = True
            End If
        Else
            cbPMKernelLoss.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWastedWaterQuality), intLOCPMAR) = True Then
            cbPMWastedWaterQuality.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWastedWaterQuality), intPMAR) = True Then
                cbPMWastedWaterQuality.Checked = True
            End If
        Else
            cbPMWastedWaterQuality.Enabled = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillContract), intModule) = False) Then
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
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMMasterSetup), intLOCCMAR) = True Then
            cbCMMasterSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMMasterSetup), intCMAR) = True Then
                cbCMMasterSetup.Checked = True
            End If
        Else
            cbCMMasterSetup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMExchangeRate), intLOCCMAR) = True Then
            cbCMExchangeRate.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMExchangeRate), intCMAR) = True Then
                cbCMExchangeRate.Checked = True
            End If
        Else
            cbCMExchangeRate.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractQuality), intLOCCMAR) = True Then
            cbCMContractQuality.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractQuality), intCMAR) = True Then
                cbCMContractQuality.Checked = True
            End If
        Else
            cbCMContractQuality.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMClaimQuality), intLOCCMAR) = True Then
            cbCMClaimQuality.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMClaimQuality), intCMAR) = True Then
                cbCMClaimQuality.Checked = True
            End If
        Else
            cbCMClaimQuality.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractReg), intLOCCMAR) = True Then
            cbCMContractReg.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractReg), intCMAR) = True Then
                cbCMContractReg.Checked = True
            End If
        Else
            cbCMContractReg.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractMatching), intLOCCMAR) = True Then
            cbCMContractMatch.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractMatching), intCMAR) = True Then
                cbCMContractMatch.Checked = True
            End If
        Else
            cbCMContractMatch.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDORegistration), intLOCCMAR) = True Then
            cbCMContractDOReg.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDORegistration), intCMAR) = True Then
                cbCMContractDOReg.Checked = True
            End If
        Else
            cbCMContractDOReg.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMGenDNCN), intLOCCMAR) = True Then
            cbCMGenDNCN.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMGenDNCN), intCMAR) = True Then
                cbCMGenDNCN.Checked = True
            End If
        Else
            cbCMGenDNCN.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDataTransfer), intLOCCMAR) = True Then
            cbCMDataTransfer.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDataTransfer), intCMAR) = True Then
                cbCMDataTransfer.Checked = True
            End If
        Else
            cbCMDataTransfer.Enabled = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Nursery), intModule) = False) Then
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
            cbNUCullType.Enabled = False

            cbNUMasterItem.Enabled = False
            cbNUItem.Enabled = False
            cbNUSeedIssue.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterSetup), intLOCNUAR) = True Then
            cbNUMasterSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterSetup), intNUAR) = True Then
                cbNUMasterSetup.Checked = True
            End If
        Else
            cbNUMasterSetup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUWorkAccDist), intLOCNUAR) = True Then
            cbNUWorkAccDist.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUWorkAccDist), intNUAR) = True Then
                cbNUWorkAccDist.Checked = True
            End If
        Else
            cbNUWorkAccDist.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedRcv), intLOCNUAR) = True Then
            cbNUSeedRcv.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedRcv), intNUAR) = True Then
                cbNUSeedRcv.Checked = True
            End If
        Else
            cbNUSeedRcv.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedPlant), intLOCNUAR) = True Then
            cbNUSeedPlant.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedPlant), intNUAR) = True Then
                cbNUSeedPlant.Checked = True
            End If
        Else
            cbNUSeedPlant.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDblTurn), intLOCNUAR) = True Then
            cbNUDblTurn.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDblTurn), intNUAR) = True Then
                cbNUDblTurn.Checked = True
            End If
        Else
            cbNUDblTurn.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUTransplanting), intLOCNUAR) = True Then
            cbNUTransplanting.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUTransplanting), intNUAR) = True Then
                cbNUTransplanting.Checked = True
            End If
        Else
            cbNUTransplanting.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch), intLOCNUAR) = True Then
            cbNUDispatch.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch), intNUAR) = True Then
                cbNUDispatch.Checked = True
            End If
        Else
            cbNUDispatch.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCulling), intLOCNUAR) = True Then
            cbNUCulling.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCulling), intNUAR) = True Then
                cbNUCulling.Checked = True
            End If
        Else
            cbNUCulling.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDataTransfer), intLOCNUAR) = True Then
            cbDtNu.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDataTransfer), intNUAR) = True Then
                cbDtNu.Checked = True
            End If
        Else
            cbDtNu.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMonthEnd), intLOCNUAR) = True Then
            cbNUMonthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMonthEnd), intNUAR) = True Then
                cbNUMonthEnd.Checked = True
            End If
        Else
            cbNUMonthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCullType), intLOCNUAR) = True Then
            cbNUCullType.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCullType), intNUAR) = True Then
                cbNUCullType.Checked = True
            End If
        Else
            cbNUCullType.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterItem), intLOCNUAR) = True Then
            cbNUMasterItem.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterItem), intNUAR) = True Then
                cbNUMasterItem.Checked = True
            End If
        Else
            cbNUMasterItem.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUItem), intLOCNUAR) = True Then
            cbNUItem.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUItem), intNUAR) = True Then
                cbNUItem.Checked = True
            End If
        Else
            cbNUItem.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedIssue), intLOCNUAR) = True Then
            cbNUSeedIssue.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedIssue), intNUAR) = True Then
                cbNUSeedIssue.Checked = True
            End If
        Else
            cbNUSeedIssue.Enabled = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.FixAsset), intModule) = False) Then
            cbFAClassSetup.Enabled = False
            cbFAGroupSetup.Enabled = False
            cbFARegSetup.Enabled = False
            cbFAPermissionSetup.Enabled = False
            cbFAAddition.Enabled = False
            cbFADepreciation.Enabled = False
            cbFADisposal.Enabled = False
            cbFAWriteOff.Enabled = False
            cbFAGenDepreciation.Enabled = False
            cbFAMonthEnd.Enabled = False
            cbFARegLine.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAClassSetup), intLOCFAAR) = True Then
            cbFAClassSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAClassSetup), intFAAR) = True Then
                cbFAClassSetup.Checked = True
            End If
        Else
            cbFAClassSetup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGroupSetup), intLOCFAAR) = True Then
            cbFAGroupSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGroupSetup), intFAAR) = True Then
                cbFAGroupSetup.Checked = True
            End If
        Else
            cbFAGroupSetup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegistrationSetup), intLOCFAAR) = True Then
            cbFARegSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegistrationSetup), intFAAR) = True Then
                cbFARegSetup.Checked = True
            End If
        Else
            cbFARegSetup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAPermissionSetup), intLOCFAAR) = True Then
            cbFAPermissionSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAPermissionSetup), intFAAR) = True Then
                cbFAPermissionSetup.Checked = True
            End If
        Else
            cbFAPermissionSetup.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMaster), intLOCFAAR) = True Then
            cbFAAssetMasterSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMaster), intFAAR) = True Then
                cbFAAssetMasterSetup.Checked = True
            End If
        Else
            cbFAAssetMasterSetup.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAItem), intLOCFAAR) = True Then
            cbFAAssetItemSetup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAItem), intFAAR) = True Then
                cbFAAssetItemSetup.Checked = True
            End If
        Else
            cbFAAssetItemSetup.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAAddition), intLOCFAAR) = True Then
            cbFAAddition.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAAddition), intFAAR) = True Then
                cbFAAddition.Checked = True
            End If
        Else
            cbFAAddition.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADepreciation), intLOCFAAR) = True Then
            cbFADepreciation.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADepreciation), intFAAR) = True Then
                cbFADepreciation.Checked = True
            End If
        Else
            cbFADepreciation.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADisposal), intLOCFAAR) = True Then
            cbFADisposal.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADisposal), intFAAR) = True Then
                cbFADisposal.Checked = True
            End If
        Else
            cbFADisposal.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAWriteOff), intLOCFAAR) = True Then
            cbFAWriteOff.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAWriteOff), intFAAR) = True Then
                cbFAWriteOff.Checked = True
            End If
        Else
            cbFAWriteOff.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADownload), intLOCFAAR) = True Then
            cbFAdtdw.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADownload), intFAAR) = True Then
                cbFAdtdw.Checked = True
            End If
        Else
            cbFAdtdw.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAUpload), intLOCFAAR) = True Then
            cbFAdtup.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAUpload), intFAAR) = True Then
                cbFAdtup.Checked = True
            End If
        Else
            cbFAdtup.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGenDepreciation), intLOCFAAR) = True Then
            cbFAGenDepreciation.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGenDepreciation), intFAAR) = True Then
                cbFAGenDepreciation.Checked = True
            End If
        Else
            cbFAGenDepreciation.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMonthEnd), intLOCFAAR) = True Then
            cbFAMonthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMonthEnd), intFAAR) = True Then
                cbFAMonthEnd.Checked = True
            End If
        Else
            cbFAMonthEnd.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegLine), intLOCFAAR) = True Then
            cbFARegLine.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegLine), intFAAR) = True Then
                cbFARegLine.Checked = True
            End If
        Else
            cbFARegLine.Enabled = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Budget), intModule) = True) Then
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting), intLOCADAR) = True Then
                cbBudgeting.Enabled = True
                If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting), intADAR) = True Then
                    cbBudgeting.Checked = True
                End If
            Else
                cbBudgeting.Enabled = False
            End If
        Else
            cbBudgeting.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAccPeriod), intLOCADAR) = True Then
            cbAccPeriod.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAccPeriod), intADAR) = True Then
                cbAccPeriod.Checked = True
                hidAccPeriod.Value = objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAccPeriod)
            End If
        Else
            cbAccPeriod.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADPeriodCfg), intLOCADAR) = True Then
            cbPeriodCfg.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADPeriodCfg), intADAR) = True Then
                cbPeriodCfg.Checked = True
                hidPeriodCfg.Value = objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADPeriodCfg)
            End If
        Else
            cbPeriodCfg.Enabled = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.CashAndBank), intModule) = False) Then
            cbCBPayment.Enabled = False
            cbCBReceipt.Enabled = False
            cbCBDeposit.Enabled = False
            cbCBInterAdj.Enabled = False
            cbCBWithdrawal.Enabled = False
            cbCBMthEnd.Enabled = False
            cbCBCashFlow.Enabled = False
            cbCBCashBank.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBPayment), intLOCCBAR) = True Then
            cbCBPayment.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBPayment), intCBAR) = True Then
                cbCBPayment.Checked = True
            End If
        Else
            cbCBPayment.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBReceipt), intLOCCBAR) = True Then
            cbCBReceipt.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBReceipt), intCBAR) = True Then
                cbCBReceipt.Checked = True
            End If
        Else
            cbCBReceipt.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBDeposit), intLOCCBAR) = True Then
            cbCBDeposit.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBDeposit), intCBAR) = True Then
                cbCBDeposit.Checked = True
            End If
        Else
            cbCBDeposit.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBInterAdj), intLOCCBAR) = True Then
            cbCBInterAdj.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBInterAdj), intCBAR) = True Then
                cbCBInterAdj.Checked = True
            End If
        Else
            cbCBInterAdj.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBWithdrawal), intLOCCBAR) = True Then
            cbCBWithdrawal.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBWithdrawal), intCBAR) = True Then
                cbCBWithdrawal.Checked = True
            End If
        Else
            cbCBWithdrawal.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBMthEnd), intLOCCBAR) = True Then
            cbCBMthEnd.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBMthEnd), intCBAR) = True Then
                cbCBMthEnd.Checked = True
            End If
        Else
            cbCBMthEnd.Enabled = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashFlow), intLOCCBAR) = True Then
            cbCBCashFlow.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashFlow), intCBAR) = True Then
                cbCBCashFlow.Checked = True
            End If
        Else
            cbCBCashFlow.Enabled = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashBank), intLOCCBAR) = True Then
            cbCBCashBank.Enabled = True
            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashBank), intCBAR) = True Then
                cbCBCashBank.Checked = True
            End If
        Else
            cbCBCashBank.Enabled = False
        End If


    End Sub


    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objUserDs As New Data.DataSet()
        Dim objSysCfgDs As New Data.DataSet()
        Dim objActualDate As New Object()
        Dim objFormatDate As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strOpCode_UpdUserLoc As String = ""
        Dim strOpCode_UpdUser As String = ""
        Dim strCompany As String = Request.Form("hidCompCode")
        Dim strLocation As String = Request.Form("ddlLocation")
        Dim strDocRetain As String = Request.Form("txtDoc")
        Dim strRecType As String = Request.Form("hidRecType")
        Dim strExpiryDate As String = ""
        Dim intINAccessRights As Integer = 0
        Dim intCTAccessRights As Integer = 0
        Dim intWSAccessRights As Integer = 0
        Dim intPUAccessRights As Integer = 0
        Dim intAPAccessRights As Integer = 0
        Dim intHRAccessRights As Long = 0
        Dim intPRAccessRights As Long = 0
        Dim intBIAccessRights As Integer = 0
        Dim intPDAccessRights As Integer = 0
        Dim intGLAccessRights As Long = 0
        Dim intWMAccessRights As Integer = 0
        Dim intPMAccessRights As Integer = 0
        Dim intCMAccessRights As Integer = 0
        Dim intNUAccessRights As Integer = 0
        Dim intFAAccessRights As Integer = 0
        Dim intADAccessRights As Integer = 0
        Dim intCBAccessRights As Integer = 0
        Dim strParam As String


        lblAccessExpire.Text = ""
        strSelectedUserId = IIf(Request.Form("hidUserId") = "", Request.QueryString("userid"), Request.Form("hidUserId"))
        strSelectedLocation = ddlLocation.SelectedItem.Value

        If Trim(txtAccessExpire.Text) <> "" Then
            strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
            Try
                intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                      strCompany, _
                                                      strLocation, _
                                                      strUserId, _
                                                      objSysCfgDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERLOC_GET_CONFIG2&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userdet.aspx?userid=" & strSelectedUserId)
            End Try

            strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           txtAccessExpire.Text, _
                                           objFormatDate, _
                                           objActualDate) = True Then
                strExpiryDate = objActualDate
            End If

            If (strExpiryDate = "") Then
                lblAccessExpire.Text = lblErrInvalid.Text & objFormatDate
                Exit Sub
            ElseIf (strExpiryDate < Today()) Then
                lblAccessExpire.Text = lblErrEarlyThen.Text
                Exit Sub
            End If
        End If

        If cbINProdMaster.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductSetup)
        End If
        If cbINItem.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItem)
        End If
        If cbINDirChrg.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDirectChargeItem)
        End If
        If cbMiscItem.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMiscItem)
        End If
        If cbINPR.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest)
        End If
        If cbStkRtnAdv.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturnAdvice)
        End If
        If cbStkTransfer.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer)
        End If
        If cbStkIsu.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue)
        End If
        If cbStkRcv.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReceive)
        End If
        If cbStkRtn.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturn)
        End If
        If cbStkAdj.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockAdjustment)
        End If
        If cbFuelIsu.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INFuelIssue)
        End If
        If cbDtInv.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDataTransfer)
        End If
        If cbINMthEnd.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMonthEnd)
        End If
        If cbItemToMachine.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INItemToMachine)
        End If
        If cbStkTransferInternal.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransferInternal)
        End If
        If cbINProdBrand.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductBrand)
        End If
        If cbINProdModel.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductModel)
        End If
        If cbINProdCategory.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductCategory)
        End If
        If cbINProdMaterial.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductMaterial)
        End If
        If cbINStockAnalysis.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INAnalisisStok)
        End If
        If cbINItemMaster.Checked Then
            intINAccessRights += objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItemMaster)
        End If

        If cbCTMaster.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster)
        End If
        If cbCTItem.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTItem)
        End If
        If cbCTPR.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTPurchaseRequest)
        End If
        If cbCTRcv.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReceive)
        End If
        If cbCTRtnAdv.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturnAdvice)
        End If
        If cbCTIsu.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTIssue)
        End If
        If cbCTRtn.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTReturn)
        End If
        If cbCTAdj.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTAdjustment)
        End If
        If cbCTTransfer.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTTransfer)
        End If
        If cbDtCT.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTDataTransfer)
        End If
        If cbCTMthEnd.Checked Then
            intCTAccessRights += objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMonthEnd)
        End If


        If cbWSProdMaster.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaster)
        End If
        If cbWSWorkMaster.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkMaster)
        End If
        If cbWSItem.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItem)
        End If
        If cbWSPart.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemPart)
        End If
        If cbWSDirChrg.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItem)
        End If
        If cbWSJob.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration)
        End If
        If cbWSMechHr.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMechanicHour)
        End If
        If cbDtWS.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDataTransfer)
        End If
        If cbWSMthEnd.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMonthEnd)
        End If
        If cbWSProdBrand.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductBrand)
        End If
        If cbWSProdModel.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductModel)
        End If
        If cbWSProdCategory.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductCategory)
        End If
        If cbWSProdMaterial.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaterial)
        End If
        If cbWSStockAnalysis.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSAnalisisStok)
        End If
        If cbWSItemMaster.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemMaster)
        End If
        If cbWSWorkshopService.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkshopService)
        End If

        If cbWSDirChrgMaster.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItemMaster)
        End If

        If cbWSMillProcDitr.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMillProcDistr)
        End If
        If cbWSCalMachine.Checked Then
            intWSAccessRights += objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSCalMachine)
        End If


        If cbPUSupp.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier)
        End If
        If cbPUPelimpahan.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPelimpahan)
        End If

        If cbPURPH.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PURPH)
        End If
        If cbPUPO.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder)
        End If
        If cbPUGoodsRcv.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReceive)
        End If
        If cbPUGRN.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReturnNote)
        End If
        If cbPUDA.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDispatchAdvice)
        End If
        If cbDtPU.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDataTransfer)
        End If
        If cbPUMthEnd.Checked Then
            intPUAccessRights += objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUMonthEnd)
        End If

        If cbAPInvoice.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APInvoiceReceive)
        End If
        If cbAPDN.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APDebitNote)
        End If
        If cbAPCN.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditNote)
        End If
        If cbAPCrtJrn.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditorJournal)
        End If
        If cbAPPay.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APPayment)
        End If
        If cbAPMthEnd.Checked Then
            intAPAccessRights += objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APMonthEnd)
        End If

        If cbHRDepartment.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDepartment)
        End If
        If cbHRCompany.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany)
        End If
        If cbHRFunc.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRFunction)
        End If
        If cbHRSkill.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSkill)
        End If
        If cbHREval.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREvaluation)
        End If
        If cbHRBank.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBank)
        End If
        If cbHRHoliday.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRHoliday)
        End If
        If cbHRCP.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCareerProgress)
        End If
        If cbHREmpDet.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails)
        End If
        If cbHREmpPR.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeePayroll)
        End If
        If cbHREmpEmploy.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeEmployement)
        End If
        If cbHRSat.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeStatutory)
        End If
        If cbHREmpFam.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeFamily)
        End If
        If cbHREmpQlf.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeQualification)
        End If
        If cbHREmpSkill.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeSkill)
        End If
        If cbHRContSuper.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRContractorSupervision)
        End If
        If cbGenEmpCode.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGenerateEmpCode)
        End If
        If cbDtHR.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDataTransfer)
        End If
        If cbHRNationality.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRNationality)
        End If

        If cbHRPosition.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPosition)
        End If
        If cbHRLevel.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRLevel)
        End If
        If cbHRReligion.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRReligion)
        End If
        If cbHRICType.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRICType)
        End If
        If cbHRRace.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRRace)
        End If
        If cbHRQualification.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRQualification)
        End If
        If cbHRSubject.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSubject)
        End If
        If cbHRCPCode.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCPCode)
        End If
        If cbHRSalScheme.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalScheme)
        End If
        If cbHRSalGrade.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalGrade)
        End If
        If cbHRShift.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRShift)
        End If
        If cbHRGang.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGang)
        End If
        If cbHRBankFormat.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBankFormat)
        End If
        If cbHRJamsostek.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRJamsostek)
        End If
        If cbHRTaxBranch.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTaxBranch)
        End If
        If cbHRTax.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTax)
        End If
        If cbHRPublicHoliday.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPublicHoliday)
        End If
        If cbHRPOH.Checked Then
            intHRAccessRights += objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRPointHired)
        End If

        If cbPRAD.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAllowanceDeduction)
        End If
        If cbPRSal.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalary)
        End If
        If cbPRContract.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRContract)
        End If
        If cbPRRice.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRice)
        End If
        If cbPREmployeeEvaluation.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PREmployeeEvaluation)
        End If
        If cbPRStandardEvaluation.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRStandardEvaluation)
        End If
        If cbPRSalaryIncrease.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalaryIncrease)
        End If
        If cbPRTranInc.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTransportIncentive)
        End If
        If cbPRAttdTrx.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAttendance)
        End If
        If cbPRTripTrx.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxTrip)
        End If
        If cbPRRatePay.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxRatePayment)
        End If
        If cbPRADTrx.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAllowanceDeduction)
        End If
        If cbPRContCheckroll.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractorCheckroll)
        End If
        If cbPRWorkPerformance.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance)
        End If
        If cbPRWPContractor.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWPContractor)
        End If
        If cbDtPR.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDataTransfer)
        End If
        If cbDwPR.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownload)
        End If
        If cbPRMthEnd.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMonthEnd)
        End If
        If cbPRYearEnd.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRYearEnd)
        End If
        If cbPRADGroup.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRADGroup)
        End If
        If cbPRDenda.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDenda)
        End If
        If cbPRHarvInc.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvInc)
        End If
        If cbPRLoad.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRLoad)
        End If
        If cbPRRoute.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRoute)
        End If
        If cbPRMedical.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMedical)
        End If
        If cbPRYearEnd.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAirBus)
        End If
        If cbPRMaternity.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMaternity)
        End If
        If cbPRPensiun.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPensiun)
        End If
        If cbPRRelocation.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRelocation)
        End If
        If cbPRIncentive.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPremi)
        End If
        If cbPRContractPay.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractPayment)
        End If
        If cbPRWagesPay.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWagesPayment)
        End If
        If cbDwPRWages.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadWages)
        End If
        If cbDwPRBankAuto.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadBankAuto)
        End If


        If cbPRPaySetup.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup)
        End If
        If cbPRDailyAttd.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDailyAttd)
        End If
        If cbPRHarvAttd.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvAttd)
        End If
        If cbPRWeekly.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRWeekly)
        End If
        If cbPRMthRice.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRice)
        End If
        If cbPRMthRapel.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRapel)
        End If
        If cbPRMthBonus.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthBonus)
        End If
        If cbPRMthTHR.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTHR)
        End If
        If cbPRMthDaily.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthDaily)
        End If
        If cbPRMthPayroll.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll)
        End If
        If cbPRMthTransfer.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthTransfer)
        End If
        If cbDwAttdInterface.Checked Then
            intPRAccessRights += objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDwAttdInterface)
        End If

        If cbBillParty.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIBillParty)
        End If
        If cbBINote.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BINote)
        End If
        If cbDtBI.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIDataTransfer)
        End If
        If cbBIMthEnd.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIMonthEnd)
        End If
        If cbBIInvoice.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIInvoice)
        End If
        If cbBIReceipt.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIReceipt)
        End If
        If cbBIJournal.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIJournal)
        End If
        If cbBICreditNote.Checked Then
            intBIAccessRights += objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BICreditNote)
        End If


        If cbEstProd.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDEstateProduction)
        End If
        If cbPOMProd.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMProduction)
        End If
        If cbPDMthEnd.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMonthEnd)
        End If
        If cbMPOBPrice.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMPOBPrice)
        End If
        If cbYearPlantYield.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDYearOfPlantYield)
        End If
        If cbPOMStorage.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStorage)
        End If
        If cbPOMStat.Checked Then
            intPDAccessRights += objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMStatistics)
        End If

        If cbAccCls.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccCls)
        End If
        If cbAct.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivity)
        End If
        If cbVehExp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpense)
        End If
        If cbVeh.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle)
        End If
        If cbVehType.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleType)
        End If
        If cbBlkGrp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlock)
        End If
        If cbBlk.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlk)
        End If
        If cbExp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLExpense)
        End If
        If cbAccount.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccount)
        End If
        If cbEntrySetup.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLEntrySetup)
        End If
        If cbBalSheetSetup.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBalSheetSetup)
        End If
        If cbProfLossSetup.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLProfLossSetup)
        End If
        If cbJrn.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal)
        End If
        If cbJrnAdj.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournalAdj)
        End If
        If cbVehUsg.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleUsage)
        End If
        If cbDtGL.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLDataTransfer)
        End If
        If cbDtUp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLUpload)
        End If
        If cbGLGCDist.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMthEndGCDist)
        End If
        If cbGLJrnMthEnd.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJrnMonthEnd)
        End If
        If cbGLMthEnd.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd)
        End If
        If cbRunHour.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLRunHour)
        End If
        If cbAccClsGrp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccClsGrp)
        End If
        If cbActGrp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivityGrp)
        End If
        If cbSubAct.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubActivity)
        End If
        If cbVehExpGrp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpenseGrp)
        End If
        If cbSubBlk.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubBlk)
        End If
        If cbAccountGrp.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccountGrp)
        End If
        If cbPosting.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLPosting)
        End If

        If cbGLCOGS.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLCOGS)
        End If

        If cbFSSetup.Checked Then
            intGLAccessRights += objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLFSSETUP)
        End If

        If cbRCDA.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDispatchAdvice)
        End If
        If cbRCJrn.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCJournal)
        End If
        If cbReadInterRC.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCReadInterface)
        End If
        If cbSendInterRC.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCSendInterface)
        End If
        If cbDtRC.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDataTransfer)
        End If

        If cbWMTransport.Checked Then
            intWMAccessRights += objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMTransporterSetup)
        End If
        If cbWMTicket.Checked Then
            intWMAccessRights += objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket)
        End If
        If cbWMFFBAssessment.Checked Then
            intWMAccessRights += objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMFFBAssessment)
        End If
        If cbWMDataTransfer.Checked Then
            intWMAccessRights += objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMDataTransfer)
        End If

        If cbPMMasterSetup.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMasterSetup)
        End If
        If cbPMVolConvMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMVolConvMaster)
        End If
        If cbPMAvgCapConvMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAvgCapConvMaster)
        End If
        If cbPMCPOPropertyMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOPropertyMaster)
        End If
        If cbPMStorageTypeMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageTypeMaster)
        End If
        If cbPMStorageAreaMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageAreaMaster)
        End If
        If cbPMProcessingLineMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProcessingLineMaster)
        End If
        If cbPMMachineMaster.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineMaster)
        End If
        If cbPMAcceptableOilQuality.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableOilQuality)
        End If
        If cbPMAcceptableKernelQuality.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableKernelQuality)
        End If
        If cbPMTestSample.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMTestSample)
        End If
        If cbPMHarvestingInterval.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMHarvestingInterval)
        End If
        If cbPMMachineCriteria.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineCriteria)
        End If

        If cbPMMill.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMill)
        End If
        If cbPMDailyProd.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction)
        End If
        If cbPMCPOStore.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOStorage)
        End If
        If cbPMPKStore.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMPKStorage)
        End If
        If cbPMOilLoss.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilLoss)
        End If
        If cbPMOilQuality.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilQuality)
        End If
        If cbPMKernelQuality.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelQuality)
        End If
        If cbPMProdKernel.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProducedKernel)
        End If
        If cbPMDispKernel.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDispatchedKernel)
        End If
        If cbPMWater.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWaterQuality)
        End If
        If cbPMNutFibre.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMNutToFibre)
        End If
        If cbPMDayEnd.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDayEnd)
        End If
        If cbPDMthEnd.Checked And cbPMMthEnd.Enabled = True Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMonthEnd)
        End If
        If cbPMKernelLoss.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelLoss)
        End If
        If cbPMWastedWaterQuality.Checked Then
            intPMAccessRights += objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWastedWaterQuality)
        End If

        If cbCMMasterSetup.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMMasterSetup)
        End If
        If cbCMExchangeRate.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMExchangeRate)
        End If
        If cbCMContractQuality.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractQuality)
        End If
        If cbCMClaimQuality.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMClaimQuality)
        End If
        If cbCMContractReg.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractReg)
        End If
        If cbCMContractMatch.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractMatching)
        End If

        If cbCMContractDOReg.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDORegistration)
        End If

        If cbCMGenDNCN.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMGenDNCN)
        End If
        If cbCMDataTransfer.Checked Then
            intCMAccessRights += objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDataTransfer)
        End If

        If cbNUMasterSetup.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterSetup)
        End If
        If cbNUWorkAccDist.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUWorkAccDist)
        End If
        If cbNUSeedRcv.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedRcv)
        End If
        If cbNUSeedPlant.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedPlant)
        End If
        If cbNUDblTurn.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDblTurn)
        End If
        If cbNUTransplanting.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUTransplanting)
        End If
        If cbNUDispatch.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch)
        End If
        If cbNUCulling.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCulling)
        End If
        If cbDtNu.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDataTransfer)
        End If
        If cbNUMonthEnd.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMonthEnd)
        End If
        If cbNUCullType.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCullType)
        End If

        If cbNUMasterItem.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterItem)
        End If
        If cbNUItem.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUItem)
        End If
        If cbNUSeedIssue.Checked Then
            intNUAccessRights += objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedIssue)
        End If


        If cbFAClassSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAClassSetup)
        End If
        If cbFAGroupSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGroupSetup)
        End If
        If cbFARegSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegistrationSetup)
        End If
        If cbFAPermissionSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAPermissionSetup)
        End If

        If cbFAAssetMasterSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMaster)
        End If
        If cbFAAssetItemSetup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAItem)
        End If
        If cbFAdtdw.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADownload)
        End If
        If cbFAdtup.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAUpload)
        End If

        If cbFAAddition.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAAddition)
        End If
        If cbFADepreciation.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADepreciation)
        End If
        If cbFADisposal.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADisposal)
        End If
        If cbFAWriteOff.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAWriteOff)
        End If
        If cbFAGenDepreciation.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGenDepreciation)
        End If
        If cbFAMonthEnd.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMonthEnd)
        End If
        If cbFARegLine.Checked Then
            intFAAccessRights += objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegLine)
        End If

        If cbAccPeriod.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAccPeriod)
        End If
        If cbBudgeting.Checked Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting)
        End If
        If cbPeriodCfg.Checked = True Then
            intADAccessRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADPeriodCfg)
        End If





        If cbCBPayment.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBPayment)
        End If
        If cbCBReceipt.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBReceipt)
        End If
        If cbCBDeposit.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBDeposit)
        End If
        If cbCBWithdrawal.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBWithdrawal)
        End If
        If cbCBInterAdj.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBInterAdj)
        End If
        If cbCBMthEnd.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBMthEnd)
        End If
        If cbCBCashFlow.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashFlow)
        End If
        If cbCBCashBank.Checked Then
            intCBAccessRights += objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashBank)
        End If

        If strCmdArgs = "Save" Then


            If hidLocation.Value = "" Then
                strOpCode_UpdUserLoc = "PWSYSTEM_CLSUSER_USERLOC_DETAILS_ADD"
            Else
                strOpCode_UpdUserLoc = "PWSYSTEM_CLSUSER_USERLOC_DETAILS_UPD"
            End If

            strParam = strSelectedUserId & "|" & _
                       ddlLocation.SelectedItem.Value & "|" & _
                       strExpiryDate & "|" & _
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
                       intFAAccessRights & "|" & _
                       intCBAccessRights
        ElseIf strCmdArgs = "Del" Then
            strOpCode_UpdUserLoc = "PWSYSTEM_CLSUSER_USERLOC_DEL"
            strParam = strSelectedUserId & "|" & ddlLocation.SelectedItem.Value & "||||||||||||||||||"
        End If

        Try
            intErrNo = objSysUser.mtdUpdUserLoc(strOpCode_UpdUserLoc, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERLOC_UPD_USERLOC&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userdet.aspx?userid=" & strSelectedUserId)
        End Try

        If strCmdArgs = "Del" Then
            Response.Redirect("userdet.aspx?userid=" & strSelectedUserId)
        Else
            onLoad_Display()
        End If
    End Sub

    Sub onSelect_Location(Sender As Object, E As EventArgs)
        onLoad_Process()
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("userdet.aspx?userid=" & Request.Form("hidUserId"))
    End Sub

End Class
