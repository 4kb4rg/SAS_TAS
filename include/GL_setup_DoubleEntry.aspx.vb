
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


Imports agri.GL
Imports agri.GlobalHdl

Public Class GL_setup_DoubleEntry : Inherits Page

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLTrx As New agri.GL.ClsTrx()

    Dim objGLDs As New Object()
    Dim objGlobalAccDs As New Object()
    Dim objGlobalNurseryAccDs As New Object()

    Dim objGlobalAllAccDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim intModuleActivate As Integer

    Dim intConfigsetting As Integer
    Dim strBlkTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim intLocLevel As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        intModuleActivate = Session("SS_MODULEACTIVATE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        intLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLEntrySetup), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            onload_GetLangCap()
            lblErrDRINStkRcvDADirectPR.Visible = False
            lblErrCRINStkRcvDADirectPR.Visible = False
            lblErrDRINStkRcvDAStockPR.Visible = False
            lblErrCRINStkRcvDAStockPR.Visible = False
            lblErrDRINStkRcvStkTransfer.Visible = False
            lblErrCRINStkRcvStkTransfer.Visible = False
            lblErrDRINStkRcvStkRtnAdvice.Visible = False
            lblErrCRINStkRcvStkRtnAdvice.Visible = False
            lblErrDRINStkRtnAdvice.Visible = False
            lblErrCRINStkRtnAdvice.Visible = False
            lblErrDRINStkIssueEmp.Visible = False
            lblErrCRINStkIssueEmp.Visible = False
            lblErrDRINFuelIssueEmp.Visible = False
            lblErrCRINFuelIssueEmp.Visible = False
            lblErrDRINBalanceFromStkRtnAdvice.Visible = False
            lblErrCRINBalanceFromStkRtnAdvice.Visible = False
            lblErrDRCTStkRcvDADirectPR.Visible = False
            lblErrCRCTStkRcvDADirectPR.Visible = False
            lblErrDRCTStkRcvDAStockPR.Visible = False
            lblErrCRCTStkRcvDAStockPR.Visible = False
            lblErrDRCTStkRcvStkTransfer.Visible = False
            lblErrCRCTStkRcvStkTransfer.Visible = False
            lblErrDRCTStkRcvStkRtnAdvice.Visible = False
            lblErrCRCTStkRcvStkRtnAdvice.Visible = False
            lblErrDRCTStkRtnAdvice.Visible = False
            lblErrCRCTStkRtnAdvice.Visible = False
            lblErrDRCTStkIssueEmp.Visible = False
            lblErrCRCTStkIssueEmp.Visible = False
            lblErrDRCTBalanceFromCTRtnAdvice.Visible = False
            lblErrCRCTBalanceFromCTRtnAdvice.Visible = False
            lblErrDRWSJobEmp.Visible = False
            lblErrCRWSJobEmp.Visible = False
            lblErrDRNUSeedlingsIssue.Visible = False
            lblErrCRNUSeedlingsIssue.Visible = False
            lblErrDRPUGoodsRcv.Visible = False
            lblErrCRPUGoodsRcv.Visible = False
            lblErrDRAPInvRcv.Visible = False
            lblErrCRAPInvRcv.Visible = False
            lblErrDRAPPPNInvRcv.Visible = False
            lblErrCRAPPPNInvRcv.Visible = False

            lblErrDRAPPPNInvRcv2.Visible = False
            lblErrCRAPPPNInvRcv2.Visible = False
            lblErrDRAPPPNInvRcv3.Visible = False
            lblErrCRAPPPNInvRcv3.Visible = False

            lblErrDRAPPPHInvRcv.Visible = False
            lblErrCRAPPPHInvRcv.Visible = False

            lblErrDRCBPPHInvPay.Visible = False
            lblErrCRCBPPHInvPay.Visible = False

            lblErrDRAPPPNCrdJrn.Visible = False
            lblErrCRAPPPNCrdJrn.Visible = False
            lblErrDRAPPPHCrdJrn.Visible = False
            lblErrCRAPPPHCrdJrn.Visible = False
            lblErrDRPRClr.Visible = False
            lblErrCRPRClr.Visible = False
            lblErrDREstYield.Visible = False
            lblErrCREstYield.Visible = False

            lblErrDRIntIncome.Visible = False
            lblErrCRIntIncome.Visible = False
            lblErrDRIntIncome2.Visible = False
            lblErrCRIntIncome2.Visible = False

            lblErrDRSunIncome.Visible = False
            lblErrCRSunIncome.Visible = False
            lblErrDRVehSuspende.Visible = False
            lblErrCRVehSuspende.Visible = False
            lblErrDRRetainEarn.Visible = False
            lblErrCRRetainEarn.Visible = False
            lblErrDRBalFrmWSBlkCode.Visible = False
            lblErrCRBalFrmWSBlkCode.Visible = False
            lblErrDRBalFrmWSAccCode.Visible = False
            lblErrCRBalFrmWSAccCode.Visible = False
            lblErrDRWSCtrlAcc.Visible = False
            lblErrCRWSCtrlAcc.Visible = False
            lblErrDRPRHK.Visible = False
            lblErrCRPRHK.Visible = False

            lblErrDRPEBMovementAccCode.Visible = False
            lblErrCRPEBMovementAccCode.Visible = False

            lblErrDRIntIncomeBlkCode.Visible = False
            lblErrCRIntIncomeBlkCode.Visible = False
            lblErrDRSunIncomeBlkCode.Visible = False
            lblErrCRSunIncomeBlkCode.Visible = False

            lblErrDRAPPPNInvRcv3Blk.Visible = False
            lblErrCRAPPPNInvRcv3Blk.Visible = False

            lblErrDRINStockAdj.Visible = False
            lblErrCRINStockAdj.Visible = False
            lblErrDRPDHPPCost.Visible = False
            lblErrCRPDHPPCost.Visible = False
            lblErrDRPDHPPBlkCode.Visible = False
            lblErrCRPDHPPBlkCode.Visible = False
            lblErrDRPDTBSKebun.Visible = False
            lblErrCRPDTBSKebun.Visible = False
            lblErrDRPDTBSKebunBlkCode.Visible = False
            lblErrCRPDTBSKebunBlkCode.Visible = False
            lblErrDRPDStockCPO.Visible = False
            lblErrCRPDStockCPO.Visible = False
            lblErrDRPDStockPK.Visible = False
            lblErrCRPDStockPK.Visible = False

            If Not IsPostBack Then
                If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Inventory)) = True Then
                    ddlDRINStkRcvDADirectPR.Enabled = False
                    ddlDRINStkRcvDADirectPR.Visible = False
                    ddlCRINStkRcvDADirectPR.Enabled = True
                    ddlDRINStkRcvDAStockPR.Enabled = False
                    ddlDRINStkRcvDAStockPR.Visible = False
                    ddlCRINStkRcvDAStockPR.Enabled = True
                    ddlDRINStkRcvStkTransfer.Enabled = False
                    ddlDRINStkRcvStkTransfer.Visible = False
                    ddlCRINStkRcvStkTransfer.Enabled = True
                    ddlDRINStkRcvStkRtnAdvice.Enabled = False
                    ddlDRINStkRcvStkRtnAdvice.Visible = False
                    ddlCRINStkRcvStkRtnAdvice.Enabled = True
                    ddlDRINStkRtnAdvice.Enabled = True
                    ddlCRINStkRtnAdvice.Enabled = False
                    ddlCRINStkRtnAdvice.Visible = False
                    ddlDRINStkIssueEmp.Enabled = True
                    ddlCRINStkIssueEmp.Enabled = False
                    ddlCRINStkIssueEmp.Visible = False
                    ddlDRINFuelIssueEmp.Enabled = True
                    ddlCRINFuelIssueEmp.Enabled = False
                    ddlCRINFuelIssueEmp.Visible = False
                    ddlDRINBalanceFromStkRtnAdvice.Enabled = True
                    ddlCRINBalanceFromStkRtnAdvice.Enabled = False
                    ddlCRINBalanceFromStkRtnAdvice.Visible = False
                    ddlDRINStockAdj.Visible = False
                    ddlCRINStockAdj.Enabled = True
                    ddlDRINStockAdjBlkCode.Visible = False
                    ddlCRINStockAdjBlkCode.Enabled = True
                End If
                If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Canteen)) = True Then
                    ddlDRCTStkRcvDADirectPR.Enabled = False
                    ddlDRCTStkRcvDADirectPR.Visible = False
                    ddlCRCTStkRcvDADirectPR.Enabled = True
                    ddlDRCTStkRcvDAStockPR.Enabled = False
                    ddlDRCTStkRcvDAStockPR.Visible = False
                    ddlCRCTStkRcvDAStockPR.Enabled = True
                    ddlDRCTStkRcvStkTransfer.Enabled = False
                    ddlDRCTStkRcvStkTransfer.Visible = False
                    ddlCRCTStkRcvStkTransfer.Enabled = True
                    ddlDRCTStkRcvStkRtnAdvice.Enabled = False
                    ddlDRCTStkRcvStkRtnAdvice.Visible = False
                    ddlCRCTStkRcvStkRtnAdvice.Enabled = True
                    ddlDRCTStkRtnAdvice.Enabled = True
                    ddlCRCTStkRtnAdvice.Enabled = False
                    ddlCRCTStkRtnAdvice.Visible = False
                    ddlDRCTStkIssueEmp.Enabled = True
                    ddlCRCTStkIssueEmp.Enabled = False
                    ddlCRCTStkIssueEmp.Visible = False
                    ddlDRCTBalanceFromCTRtnAdvice.Enabled = True
                    ddlCRCTBalanceFromCTRtnAdvice.Enabled = False
                    ddlCRCTBalanceFromCTRtnAdvice.Visible = False
                End If
                If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Workshop)) = True Then
                    ddlDRWSJobEmp.Enabled = True
                    ddlCRWSJobEmp.Enabled = False
                    ddlCRWSJobEmp.Visible = False
                End If
                If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Nursery)) = True Then
                    ddlDRNUSeedlingsIssue.Enabled = False
                    ddlDRNUSeedlingsIssue.Visible = False
                    ddlCRNUSeedlingsIssue.Enabled = True
                End If
                If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Purchasing)) = True Then
                    ddlDRPUGoodsRcv.Enabled = False
                    ddlDRPUGoodsRcv.Visible = False
                    ddlCRPUGoodsRcv.Enabled = True
                    ddlDRPUDispAdv.Enabled = True
                    ddlCRPUDispAdv.Visible = False
                    ddlCRPUDispAdv.Enabled = False
                End If
                If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.AccountPayable)) = True Then
                    ddlDRAPInvRcv.Enabled = False 
                    ddlCRAPInvRcv.Enabled = False
                    ddlCRAPInvRcv.Visible = False

                    ddlDRAPPPNInvRcv.Enabled = True
                    ddlCRAPPPNInvRcv.Enabled = False

                    ddlDRAPPPNInvRcv2.Enabled = False
                    ddlDRAPPPNInvRcv2.Visible = False
                    ddlCRAPPPNInvRcv2.Enabled = True

                    ddlDRAPPPNInvRcv4.Enabled = True
                    ddlCRAPPPNInvRcv4.Enabled = False

                    ddlDRAPPPNInvRcv3.Enabled = True
                    ddlCRAPPPNInvRcv3.Enabled = False
                    ddlCRAPPPNInvRcv3.Visible = False

                    ddlDRAPPPNInvRcv3Blk.Enabled = True
                    ddlCRAPPPNInvRcv3Blk.Enabled = False
                    ddlCRAPPPNInvRcv3Blk.Visible = False

                    ddlCRAPPPNInvRcv.Visible = False
                    ddlDRAPPPHInvRcv.Enabled = False
                    ddlDRAPPPHInvRcv.Visible = False
                    ddlCRAPPPHInvRcv.Enabled = True

                    ddlDRCBPPHInvPay.Enabled = True
                    ddlCRCBPPHInvPay.Enabled = False
                    ddlCRCBPPHInvPay.Visible = False


                    ddlCRAPPPNCrdJrn.Enabled = True
                    ddlDRAPPPNCrdJrn.Enabled = False
                    ddlDRAPPPNCrdJrn.Visible = False
                    ddlDRAPPPHCrdJrn.Enabled = True
                    ddlCRAPPPHCrdJrn.Enabled = False
                    ddlCRAPPPHCrdJrn.Visible = False
                    ddlCRBIPPNInvRcv.Enabled = True
                    ddlDRBIPPNInvRcv.Enabled = False
                    ddlDRBIPPNInvRcv.Visible = False
                    ddlDRBIPPHInvRcv.Enabled = True
                    ddlCRBIPPHInvRcv.Enabled = False
                    ddlCRBIPPHInvRcv.Visible = False
                    
                    ddlDRCBPPNRcpt.Enabled = True
                    ddlCRCBPPNRcpt.Enabled = False
                    ddlCRCBPPNRcpt.Visible = False
                    ddlCRCBPPHRcpt.Enabled = True
                    ddlDRCBPPHRcpt.Enabled = False
                    ddlDRCBPPHRcpt.Visible = False

                    ddlDRAdvPayment.Visible = True
                    ddlCRAdvPayment.Visible = False
                    ddlDRAdvPayment.Enabled = True
                    ddlCRAdvPayment.Enabled = False

                End If
                If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Payroll)) = True Then
                    ddlDRPRClr.Enabled = False
                    ddlDRPRClr.Visible = False
                    ddlCRPRClr.Enabled = True
                    ddlDRPRHK.Enabled = False
                    ddlDRPRHK.Visible = False
                    ddlCRPRHK.Enabled = True
                End If
                If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Production)) = True Then
                    ddlDREstYield.Enabled = False
                    ddlDREstYield.Visible = False
                    ddlCREstYield.Enabled = True
                    ddlDRPDHPPCost.Enabled = True
                    ddlDRPDHPPBlkCode.Visible = True
                    ddlDRPDHPPBlkCode.Enabled = True
                    ddlCRPDHPPCost.Enabled = True
                    ddlCRPDHPPCost.Visible = False
                    ddlCRPDHPPBlkCode.Enabled = True
                    ddlCRPDHPPBlkCode.Visible = False
                    ddlDRPDTBSKebun.Enabled = True
                    ddlDRPDTBSKebunBlkCode.Visible = True
                    ddlDRPDTBSKebunBlkCode.Enabled = True
                    ddlCRPDTBSKebun.Enabled = True
                    ddlCRPDTBSKebun.Visible = False
                    ddlCRPDTBSKebunBlkCode.Enabled = True
                    ddlCRPDTBSKebunBlkCode.Visible = False
                    ddlDRPDStockCPO.Enabled = True
                    ddlCRPDStockCPO.Visible = False
                    ddlDRPDStockPK.Enabled = True
                    ddlCRPDStockPK.Enabled = True
                    ddlCRPDStockPK.Visible = False
                End If

                If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.CashAndBank)) = True Then
                    ddlDRIntIncome.Enabled = False
                    ddlDRIntIncome.Visible = False
                    ddlCRIntIncome.Enabled = True
                    ddlDRIntIncomeBlkCode.Enabled = False
                    ddlDRIntIncomeBlkCode.Visible = False
                    ddlCRIntIncomeBlkCode.Enabled = True

                    ddlDRIntIncome2.Enabled = False
                    ddlDRIntIncome2.Visible = False
                    ddlCRIntIncome2.Enabled = True
                    ddlDRIntIncomeBlkCode2.Enabled = False
                    ddlDRIntIncomeBlkCode2.Visible = False
                    ddlCRIntIncomeBlkCode2.Enabled = True


                End If

                If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.GeneralLedger)) = True Then
                    ddlDRSunIncome.Enabled = False
                    ddlDRSunIncome.Visible = False
                    ddlCRSunIncome.Enabled = True
                    ddlDRVehSuspende.Enabled = True
                    ddlCRVehSuspende.Enabled = False
                    ddlCRVehSuspende.Visible = False
                    ddlDRRetainEarn.Enabled = True
                    ddlCRRetainEarn.Enabled = False
                    ddlCRRetainEarn.Visible = False
                    ddlDRBalFrmWSAccCode.Enabled = True
                    ddlDRBalFrmWSAccCode.Visible = True
                    ddlCRBalFrmWSAccCode.Enabled = True
                    ddlCRBalFrmWSAccCode.Visible = True
                    ddlDRBalFrmWSBlkCode.Enabled = True
                    ddlDRBalFrmWSBlkCode.Visible = True
                    ddlCRBalFrmWSBlkCode.Enabled = True
                    ddlCRBalFrmWSBlkCode.Visible = True

                    ddlDRPEBMovementAccCode.Enabled = True 
                    ddlDRPEBMovementAccCode.visible = True 
                    ddlCRPEBMovementAccCode.Enabled = True
                    ddlDRPEBMovementAccCode.visible = True 
                    ddlDRSunIncomeBlkCode.Enabled = False
                    ddlDRSunIncomeBlkCode.Visible = False
                    ddlCRSunIncomeBlkCode.Enabled = True

                End If

                If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.UseControlAccForWorkExp), intConfigSetting) = True Then
                        ddlDRWSCtrlAcc.Enabled = True
                        ddlCRWSCtrlAcc.Enabled = True
                Else
                        ddlDRWSCtrlAcc.Enabled = False
                        ddlCRWSCtrlAcc.Enabled = False
                End If

                BindAccount_APTBS()               
                BindAccount_Sales()
                onLoad_Display()
                LoadCOATBS()
                LoadCOASales()
            End If
        End If
    End Sub

    Protected Function LoadCOATBS() As DataSet
        Dim strParamName AS String
        Dim strParamValue AS String
        Dim objTicketDs AS New Dataset
        Dim strOpCd_DKtr As String = "WM_CLSTRX_TICKET_COASETTING_BUY_GET"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim strMn As String
        Dim strYr As String

        'strMn = ddlMonth.SelectedItem.Value.Trim
        'strYr = ddlyear.SelectedItem.Value.Trim

        strParamName = "STRSEARCH"
        strParamValue = ""

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_DKtr, strParamName, strParamValue, objTicketDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objTicketDs.Tables(0).Rows.Count > 0 Then 
            radTbsPemilik.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COATBSPemilik"))
            radTbsAgen.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COATBSAgen"))
            radTbsPPN.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COAPPN"))
            radTbsPPH.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COAPPH"))
            radTBSOBongkar.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COAOngkosBongkar"))
            radTbsOLapangan.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COAOngkosLapangan"))
               
        End If
    End Function

    Protected Function LoadCOASales() As DataSet
        Dim strOpCd_DKtr As String = "WM_CLSTRX_TICKET_COASETTING_SALE_GET"
        Dim objTicketDs As New Dataset()
        Dim intErrNo As Integer
        Dim strParamName AS String
        Dim strParamValue AS String       
        strParamName = "STRSEARCH"
        strParamValue = ""

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_DKtr, strParamName, strParamValue, objTicketDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objTicketDs.Tables(0).Rows.Count > 0 Then
            ddlSalesCPO.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COASalesCPO"))
            ddlSalesKNL.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COASalesKNL"))
            ddlSalesEFB.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COASalesEFB"))
            ddlSalesCKG.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COASalesCKG"))
            ddlSalesABJ.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COASalesABJ"))
            ddlSalesFBR.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COASalesFBR"))
            ddlSalesSLD.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COASalesSLD"))
            ddlSalesLMB.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COASalesLMB"))
            ddlSalesOTH.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COASalesOTH"))
            ddlSalesPPN.SelectedValue= RTrim(objTicketDs.Tables(0).Rows(0).Item("COASalesPPN"))
               
        End If
    End Function


    Sub onLoad_Display()
        Dim strOpCd As String = "GL_CLSSETUP_ENTRYSETUP_GET"
        Dim intErrNo As Integer
        Dim intIndex As Integer
        Dim intCnt As Integer
        Dim strParam As String
        Dim objDs As New Object()
        Dim objNUDs As New Object()
        Dim intNUIndex As Integer

        Dim objDRBalFrmWSDs As New Object()
        Dim objCRBalFrmWSDs As New Object()
        Dim objDRBalFrmWSBCDs As New Object()
        Dim objCRBalFrmWSBCDs As New Object()
        Dim intDRBalFrmWSDsIndex As Integer
        Dim intCRBalFrmWSDsIndex As Integer 
        Dim intDRBalFrmWSBCDsIndex As Integer
        Dim intCRBalFrmWSBCDsIndex As Integer 

        Try
            strParam = "|"
            intErrNo = objGLSetup.mtdEntrySetup(strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strOpCd, _
                                                strParam, _
                                                True, _
                                                objGLDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If objGLDs.Tables(0).Rows.Count > 0 Then
            lblHasRecord.Text = True
            lblLastUpdate.Text = objGlobal.GetLongDate(objGLDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objGLDs.Tables(0).Rows(0).Item("UserName"))

            For intCnt = 0 To objGLDs.Tables(0).Rows.Count - 1
                Select Case CInt(objGLDs.Tables(0).Rows(intCnt).Item("EntryType"))
                    Case objGLSetup.EnumEntryType.INDRStockReceiveDADirect
                        ddlDRINStkRcvDADirectPR.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRINStkRcvDADirectPR.DataValueField = "AccCode"
                        ddlDRINStkRcvDADirectPR.DataTextField = "_Description"
                        ddlDRINStkRcvDADirectPR.DataBind()
                        ddlDRINStkRcvDADirectPR.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRINStkRcvDADirectPR.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRINStkRcvDADirectPR.DataValueField = "AccCode"
                        ddlCRINStkRcvDADirectPR.DataTextField = "_Description"
                        ddlCRINStkRcvDADirectPR.DataBind()
                        ddlCRINStkRcvDADirectPR.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.INDRStockReceiveDAStock
                        ddlDRINStkRcvDAStockPR.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRINStkRcvDAStockPR.DataValueField = "AccCode"
                        ddlDRINStkRcvDAStockPR.DataTextField = "_Description"
                        ddlDRINStkRcvDAStockPR.DataBind()
                        ddlDRINStkRcvDAStockPR.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))
                        
                        ddlCRINStkRcvDAStockPR.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRINStkRcvDAStockPR.DataValueField = "AccCode"
                        ddlCRINStkRcvDAStockPR.DataTextField = "_Description"
                        ddlCRINStkRcvDAStockPR.DataBind()
                        ddlCRINStkRcvDAStockPR.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.INDRStockReceiveStkTransfer
                        ddlDRINStkRcvStkTransfer.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRINStkRcvStkTransfer.DataValueField = "AccCode"
                        ddlDRINStkRcvStkTransfer.DataTextField = "_Description"
                        ddlDRINStkRcvStkTransfer.DataBind()
                        ddlDRINStkRcvStkTransfer.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRINStkRcvStkTransfer.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRINStkRcvStkTransfer.DataValueField = "AccCode"
                        ddlCRINStkRcvStkTransfer.DataTextField = "_Description"
                        ddlCRINStkRcvStkTransfer.DataBind()
                        ddlCRINStkRcvStkTransfer.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.INDRStockReceiveStkRtnAdvice
                        ddlDRINStkRcvStkRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRINStkRcvStkRtnAdvice.DataValueField = "AccCode"
                        ddlDRINStkRcvStkRtnAdvice.DataTextField = "_Description"
                        ddlDRINStkRcvStkRtnAdvice.DataBind()
                        ddlDRINStkRcvStkRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRINStkRcvStkRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRINStkRcvStkRtnAdvice.DataValueField = "AccCode"
                        ddlCRINStkRcvStkRtnAdvice.DataTextField = "_Description"
                        ddlCRINStkRcvStkRtnAdvice.DataBind()
                        ddlCRINStkRcvStkRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))
                    Case objGLSetup.EnumEntryType.INDRStockReturnAdvice
                        ddlDRINStkRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRINStkRtnAdvice.DataValueField = "AccCode"
                        ddlDRINStkRtnAdvice.DataTextField = "_Description"
                        ddlDRINStkRtnAdvice.DataBind()
                        ddlDRINStkRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRINStkRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRINStkRtnAdvice.DataValueField = "AccCode"
                        ddlCRINStkRtnAdvice.DataTextField = "_Description"
                        ddlCRINStkRtnAdvice.DataBind()
                        ddlCRINStkRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.INDRStockIssue
                        ddlDRINStkIssueEmp.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRINStkIssueEmp.DataValueField = "AccCode"
                        ddlDRINStkIssueEmp.DataTextField = "_Description"
                        ddlDRINStkIssueEmp.DataBind()
                        ddlDRINStkIssueEmp.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRINStkIssueEmp.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRINStkIssueEmp.DataValueField = "AccCode"
                        ddlCRINStkIssueEmp.DataTextField = "_Description"
                        ddlCRINStkIssueEmp.DataBind()
                        ddlCRINStkIssueEmp.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.INDRFuelIssueEmp
                        ddlDRINFuelIssueEmp.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRINFuelIssueEmp.DataValueField = "AccCode"
                        ddlDRINFuelIssueEmp.DataTextField = "_Description"
                        ddlDRINFuelIssueEmp.DataBind()
                        ddlDRINFuelIssueEmp.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRINFuelIssueEmp.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRINFuelIssueEmp.DataValueField = "AccCode"
                        ddlCRINFuelIssueEmp.DataTextField = "_Description"
                        ddlCRINFuelIssueEmp.DataBind()
                        ddlCRINFuelIssueEmp.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.INDRBalanceFromStkRtnAdvice
                        ddlDRINBalanceFromStkRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRINBalanceFromStkRtnAdvice.DataValueField = "AccCode"
                        ddlDRINBalanceFromStkRtnAdvice.DataTextField = "_Description"
                        ddlDRINBalanceFromStkRtnAdvice.DataBind()
                        ddlDRINBalanceFromStkRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRINBalanceFromStkRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRINBalanceFromStkRtnAdvice.DataValueField = "AccCode"
                        ddlCRINBalanceFromStkRtnAdvice.DataTextField = "_Description"
                        ddlCRINBalanceFromStkRtnAdvice.DataBind()
                        ddlCRINBalanceFromStkRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.INDRStockAdj
                        ddlDRINStockAdj.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRINStockAdj.DataValueField = "AccCode"
                        ddlDRINStockAdj.DataTextField = "_Description"
                        ddlDRINStockAdj.DataBind()
                        ddlDRINStockAdj.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRINStockAdj.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRINStockAdj.DataValueField = "AccCode"
                        ddlCRINStockAdj.DataTextField = "_Description"
                        ddlCRINStockAdj.DataBind()
                        ddlCRINStockAdj.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), ddlDRINStockAdjBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRBlkCode")))
                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), ddlCRINStockAdjBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRBlkCode")))

                    Case objGLSetup.EnumEntryType.CTDRCanteenReceiveDADirect
                        ddlDRCTStkRcvDADirectPR.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRCTStkRcvDADirectPR.DataValueField = "AccCode"
                        ddlDRCTStkRcvDADirectPR.DataTextField = "_Description"
                        ddlDRCTStkRcvDADirectPR.DataBind()
                        ddlDRCTStkRcvDADirectPR.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRCTStkRcvDADirectPR.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRCTStkRcvDADirectPR.DataValueField = "AccCode"
                        ddlCRCTStkRcvDADirectPR.DataTextField = "_Description"
                        ddlCRCTStkRcvDADirectPR.DataBind()
                        ddlCRCTStkRcvDADirectPR.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))
                    Case objGLSetup.EnumEntryType.CTDRCanteenReceiveDAStock
                        ddlDRCTStkRcvDAStockPR.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRCTStkRcvDAStockPR.DataValueField = "AccCode"
                        ddlDRCTStkRcvDAStockPR.DataTextField = "_Description"
                        ddlDRCTStkRcvDAStockPR.DataBind()
                        ddlDRCTStkRcvDAStockPR.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRCTStkRcvDAStockPR.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRCTStkRcvDAStockPR.DataValueField = "AccCode"
                        ddlCRCTStkRcvDAStockPR.DataTextField = "_Description"
                        ddlCRCTStkRcvDAStockPR.DataBind()
                        ddlCRCTStkRcvDAStockPR.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))
                    Case objGLSetup.EnumEntryType.CTDRCanteenReceiveStkTransfer
                        ddlDRCTStkRcvStkTransfer.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRCTStkRcvStkTransfer.DataValueField = "AccCode"
                        ddlDRCTStkRcvStkTransfer.DataTextField = "_Description"
                        ddlDRCTStkRcvStkTransfer.DataBind()
                        ddlDRCTStkRcvStkTransfer.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRCTStkRcvStkTransfer.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRCTStkRcvStkTransfer.DataValueField = "AccCode"
                        ddlCRCTStkRcvStkTransfer.DataTextField = "_Description"
                        ddlCRCTStkRcvStkTransfer.DataBind()
                        ddlCRCTStkRcvStkTransfer.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.CTDRCanteenReceiveStkRtnAdvice
                        ddlDRCTStkRcvStkRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRCTStkRcvStkRtnAdvice.DataValueField = "AccCode"
                        ddlDRCTStkRcvStkRtnAdvice.DataTextField = "_Description"
                        ddlDRCTStkRcvStkRtnAdvice.DataBind()
                        ddlDRCTStkRcvStkRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))
                        ddlCRCTStkRcvStkRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRCTStkRcvStkRtnAdvice.DataValueField = "AccCode"
                        ddlCRCTStkRcvStkRtnAdvice.DataTextField = "_Description"
                        ddlCRCTStkRcvStkRtnAdvice.DataBind()
                        ddlCRCTStkRcvStkRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))
                    Case objGLSetup.EnumEntryType.CTDRCanteenReturnAdvice
                        ddlDRCTStkRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRCTStkRtnAdvice.DataValueField = "AccCode"
                        ddlDRCTStkRtnAdvice.DataTextField = "_Description"
                        ddlDRCTStkRtnAdvice.DataBind()
                        ddlDRCTStkRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRCTStkRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRCTStkRtnAdvice.DataValueField = "AccCode"
                        ddlCRCTStkRtnAdvice.DataTextField = "_Description"
                        ddlCRCTStkRtnAdvice.DataBind()
                        ddlCRCTStkRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.CTDRCanteenIssue
                        ddlDRCTStkIssueEmp.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRCTStkIssueEmp.DataValueField = "AccCode"
                        ddlDRCTStkIssueEmp.DataTextField = "_Description"
                        ddlDRCTStkIssueEmp.DataBind()
                        ddlDRCTStkIssueEmp.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRCTStkIssueEmp.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRCTStkIssueEmp.DataValueField = "AccCode"
                        ddlCRCTStkIssueEmp.DataTextField = "_Description"
                        ddlCRCTStkIssueEmp.DataBind()
                        ddlCRCTStkIssueEmp.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.CTDRBalanceFromCTRtnAdvice
                        ddlDRCTBalanceFromCTRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRCTBalanceFromCTRtnAdvice.DataValueField = "AccCode"
                        ddlDRCTBalanceFromCTRtnAdvice.DataTextField = "_Description"
                        ddlDRCTBalanceFromCTRtnAdvice.DataBind()
                        ddlDRCTBalanceFromCTRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))
                        
                        ddlCRCTBalanceFromCTRtnAdvice.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRCTBalanceFromCTRtnAdvice.DataValueField = "AccCode"
                        ddlCRCTBalanceFromCTRtnAdvice.DataTextField = "_Description"
                        ddlCRCTBalanceFromCTRtnAdvice.DataBind()
                        ddlCRCTBalanceFromCTRtnAdvice.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.WSDRJobIssue
                        ddlDRWSJobEmp.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRWSJobEmp.DataValueField = "AccCode"
                        ddlDRWSJobEmp.DataTextField = "_Description"
                        ddlDRWSJobEmp.DataBind()
                        ddlDRWSJobEmp.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRWSJobEmp.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRWSJobEmp.DataValueField = "AccCode"
                        ddlCRWSJobEmp.DataTextField = "_Description"
                        ddlCRWSJobEmp.DataBind()
                        ddlCRWSJobEmp.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.NUDRSeedlingsIssue
                        ddlDRNUSeedlingsIssue.DataSource = BindNurseryAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRNUSeedlingsIssue.DataValueField = "AccCode"
                        ddlDRNUSeedlingsIssue.DataTextField = "_Description"
                        ddlDRNUSeedlingsIssue.DataBind()
                        ddlDRNUSeedlingsIssue.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))
                        
                        ddlCRNUSeedlingsIssue.DataSource = BindNurseryAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRNUSeedlingsIssue.DataValueField = "AccCode"
                        ddlCRNUSeedlingsIssue.DataTextField = "_Description"
                        ddlCRNUSeedlingsIssue.DataBind()
                        ddlCRNUSeedlingsIssue.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))
                    Case objGLSetup.EnumEntryType.PUDRGoodsReceive
                        ddlDRPUGoodsRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRPUGoodsRcv.DataValueField = "AccCode"
                        ddlDRPUGoodsRcv.DataTextField = "_Description"
                        ddlDRPUGoodsRcv.DataBind()
                        ddlDRPUGoodsRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))
                        
                        ddlCRPUGoodsRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRPUGoodsRcv.DataValueField = "AccCode"
                        ddlCRPUGoodsRcv.DataTextField = "_Description"
                        ddlCRPUGoodsRcv.DataBind()
                        ddlCRPUGoodsRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.PUDRDispatchAdvice
                        ddlDRPUDispAdv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRPUDispAdv.DataValueField = "AccCode"
                        ddlDRPUDispAdv.DataTextField = "_Description"
                        ddlDRPUDispAdv.DataBind()
                        ddlDRPUDispAdv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRPUDispAdv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRPUDispAdv.DataValueField = "AccCode"
                        ddlCRPUDispAdv.DataTextField = "_Description"
                        ddlCRPUDispAdv.DataBind()
                        ddlCRPUDispAdv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.APDRInvoiceReceive
                        ddlDRAPInvRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRAPInvRcv.DataValueField = "AccCode"
                        ddlDRAPInvRcv.DataTextField = "_Description"
                        ddlDRAPInvRcv.DataBind()
                        ddlDRAPInvRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRAPInvRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRAPInvRcv.DataValueField = "AccCode"
                        ddlCRAPInvRcv.DataTextField = "_Description"
                        ddlCRAPInvRcv.DataBind()
                        ddlCRAPInvRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.APDRPPNInvoiceReceive
                        ddlDRAPPPNInvRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRAPPPNInvRcv.DataValueField = "AccCode"
                        ddlDRAPPPNInvRcv.DataTextField = "_Description"
                        ddlDRAPPPNInvRcv.DataBind()
                        ddlDRAPPPNInvRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRAPPPNInvRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRAPPPNInvRcv.DataValueField = "AccCode"
                        ddlCRAPPPNInvRcv.DataTextField = "_Description"
                        ddlCRAPPPNInvRcv.DataBind()
                        ddlCRAPPPNInvRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.APDRPPNInvoiceReceive2
                        ddlDRAPPPNInvRcv2.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRAPPPNInvRcv2.DataValueField = "AccCode"
                        ddlDRAPPPNInvRcv2.DataTextField = "_Description"
                        ddlDRAPPPNInvRcv2.DataBind()
                        ddlDRAPPPNInvRcv2.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRAPPPNInvRcv2.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRAPPPNInvRcv2.DataValueField = "AccCode"
                        ddlCRAPPPNInvRcv2.DataTextField = "_Description"
                        ddlCRAPPPNInvRcv2.DataBind()
                        ddlCRAPPPNInvRcv2.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))


                    Case objGLSetup.EnumEntryType.APDRPPNInvoiceReceive4

                        ddlDRAPPPNInvRcv4.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRAPPPNInvRcv4.DataValueField = "AccCode"
                        ddlDRAPPPNInvRcv4.DataTextField = "_Description"
                        ddlDRAPPPNInvRcv4.DataBind()
                        ddlDRAPPPNInvRcv4.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRAPPPNInvRcv4.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRAPPPNInvRcv4.DataValueField = "AccCode"
                        ddlCRAPPPNInvRcv4.DataTextField = "_Description"
                        ddlCRAPPPNInvRcv4.DataBind()
                        ddlCRAPPPNInvRcv4.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.APDRPPNInvoiceReceive3

                        ddlDRAPPPNInvRcv3.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRAPPPNInvRcv3.DataValueField = "AccCode"
                        ddlDRAPPPNInvRcv3.DataTextField = "_Description"
                        ddlDRAPPPNInvRcv3.DataBind()
                        ddlDRAPPPNInvRcv3.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))


                        ddlCRAPPPNInvRcv3.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRAPPPNInvRcv3.DataValueField = "AccCode"
                        ddlCRAPPPNInvRcv3.DataTextField = "_Description"
                        ddlCRAPPPNInvRcv3.DataBind()
                        ddlCRAPPPNInvRcv3.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))


                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), ddlDRAPPPNInvRcv3Blk, Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRBlkCode")))
                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), ddlCRAPPPNInvRcv3Blk, Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRBlkCode")))


                    Case objGLSetup.EnumEntryType.APDRPPHInvoiceReceive
                        ddlDRAPPPHInvRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRAPPPHInvRcv.DataValueField = "AccCode"
                        ddlDRAPPPHInvRcv.DataTextField = "_Description"
                        ddlDRAPPPHInvRcv.DataBind()
                        ddlDRAPPPHInvRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRAPPPHInvRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRAPPPHInvRcv.DataValueField = "AccCode"
                        ddlCRAPPPHInvRcv.DataTextField = "_Description"
                        ddlCRAPPPHInvRcv.DataBind()
                        ddlCRAPPPHInvRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))
                    Case objGLSetup.EnumEntryType.APDRPPHPayment

                        ddlDRCBPPHInvPay.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRCBPPHInvPay.DataValueField = "AccCode"
                        ddlDRCBPPHInvPay.DataTextField = "_Description"
                        ddlDRCBPPHInvPay.DataBind()
                        ddlDRCBPPHInvPay.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

            
                        ddlCRCBPPHInvPay.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRCBPPHInvPay.DataValueField = "AccCode"
                        ddlCRCBPPHInvPay.DataTextField = "_Description"
                        ddlCRCBPPHInvPay.DataBind()
                        ddlCRCBPPHInvPay.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))
                    Case objGLSetup.EnumEntryType.APDRPPNCREDITJRN
                        ddlDRAPPPNCrdJrn.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRAPPPNCrdJrn.DataValueField = "AccCode"
                        ddlDRAPPPNCrdJrn.DataTextField = "_Description"
                        ddlDRAPPPNCrdJrn.DataBind()
                        ddlDRAPPPNCrdJrn.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRAPPPNCrdJrn.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRAPPPNCrdJrn.DataValueField = "AccCode"
                        ddlCRAPPPNCrdJrn.DataTextField = "_Description"
                        ddlCRAPPPNCrdJrn.DataBind()
                        ddlCRAPPPNCrdJrn.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.APDRPPHCREDITJRN
                        ddlDRAPPPHCrdJrn.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRAPPPHCrdJrn.DataValueField = "AccCode"
                        ddlDRAPPPHCrdJrn.DataTextField = "_Description"
                        ddlDRAPPPHCrdJrn.DataBind()
                        ddlDRAPPPHCrdJrn.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRAPPPHCrdJrn.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRAPPPHCrdJrn.DataValueField = "AccCode"
                        ddlCRAPPPHCrdJrn.DataTextField = "_Description"
                        ddlCRAPPPHCrdJrn.DataBind()
                        ddlCRAPPPHCrdJrn.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.APAdvPayment
                        ddlDRAdvPayment.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRAdvPayment.DataValueField = "AccCode"
                        ddlDRAdvPayment.DataTextField = "_Description"
                        ddlDRAdvPayment.DataBind()
                        ddlDRAdvPayment.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRAdvPayment.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRAdvPayment.DataValueField = "AccCode"
                        ddlCRAdvPayment.DataTextField = "_Description"
                        ddlCRAdvPayment.DataBind()
                        ddlCRAdvPayment.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.BIDRPPNInvoiceReceive
                        ddlDRBIPPNInvRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRBIPPNInvRcv.DataValueField = "AccCode"
                        ddlDRBIPPNInvRcv.DataTextField = "_Description"
                        ddlDRBIPPNInvRcv.DataBind()
                        ddlDRBIPPNInvRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRBIPPNInvRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRBIPPNInvRcv.DataValueField = "AccCode"
                        ddlCRBIPPNInvRcv.DataTextField = "_Description"
                        ddlCRBIPPNInvRcv.DataBind()
                        ddlCRBIPPNInvRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.BIDRPPHInvoiceReceive
                        ddlDRBIPPHInvRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRBIPPHInvRcv.DataValueField = "AccCode"
                        ddlDRBIPPHInvRcv.DataTextField = "_Description"
                        ddlDRBIPPHInvRcv.DataBind()
                        ddlDRBIPPHInvRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRBIPPHInvRcv.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRBIPPHInvRcv.DataValueField = "AccCode"
                        ddlCRBIPPHInvRcv.DataTextField = "_Description"
                        ddlCRBIPPHInvRcv.DataBind()
                        ddlCRBIPPHInvRcv.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))




                    Case objGLSetup.EnumEntryType.BIDRPPNReceipt
                        ddlDRCBPPNRcpt.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRCBPPNRcpt.DataValueField = "AccCode"
                        ddlDRCBPPNRcpt.DataTextField = "_Description"
                        ddlDRCBPPNRcpt.DataBind()
                        ddlDRCBPPNRcpt.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRCBPPNRcpt.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRCBPPNRcpt.DataValueField = "AccCode"
                        ddlCRCBPPNRcpt.DataTextField = "_Description"
                        ddlCRCBPPNRcpt.DataBind()
                        ddlCRCBPPNRcpt.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.BIDRPPHReceipt
                        ddlDRCBPPHRcpt.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRCBPPHRcpt.DataValueField = "AccCode"
                        ddlDRCBPPHRcpt.DataTextField = "_Description"
                        ddlDRCBPPHRcpt.DataBind()
                        ddlDRCBPPHRcpt.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRCBPPHRcpt.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRCBPPHRcpt.DataValueField = "AccCode"
                        ddlCRCBPPHRcpt.DataTextField = "_Description"
                        ddlCRCBPPHRcpt.DataBind()
                        ddlCRCBPPHRcpt.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))
                    
                    Case objGLSetup.EnumEntryType.PRDRPayClearance
                        ddlDRPRClr.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRPRClr.DataValueField = "AccCode"
                        ddlDRPRClr.DataTextField = "_Description"
                        ddlDRPRClr.DataBind()
                        ddlDRPRClr.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRPRClr.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRPRClr.DataValueField = "AccCode"
                        ddlCRPRClr.DataTextField = "_Description"
                        ddlCRPRClr.DataBind()
                        ddlCRPRClr.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.PRDRHutangKaryawan
                        ddlDRPRHK.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRPRHK.DataValueField = "AccCode"
                        ddlDRPRHK.DataTextField = "_Description"
                        ddlDRPRHK.DataBind()
                        ddlDRPRHK.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRPRHK.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRPRHK.DataValueField = "AccCode"
                        ddlCRPRHK.DataTextField = "_Description"
                        ddlCRPRHK.DataBind()
                        ddlCRPRHK.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.PDDREstateYield
                        ddlDREstYield.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDREstYield.DataValueField = "AccCode"
                        ddlDREstYield.DataTextField = "_Description"
                        ddlDREstYield.DataBind()
                        ddlDREstYield.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCREstYield.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCREstYield.DataValueField = "AccCode"
                        ddlCREstYield.DataTextField = "_Description"
                        ddlCREstYield.DataBind()
                        ddlCREstYield.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.PDDRHPPCost
                        ddlDRPDHPPCost.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRPDHPPCost.DataValueField = "AccCode"
                        ddlDRPDHPPCost.DataTextField = "_Description"
                        ddlDRPDHPPCost.DataBind()
                        ddlDRPDHPPCost.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRPDHPPCost.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRPDHPPCost.DataValueField = "AccCode"
                        ddlCRPDHPPCost.DataTextField = "_Description"
                        ddlCRPDHPPCost.DataBind()
                        ddlCRPDHPPCost.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), ddlDRPDHPPBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRBlkCode")))
                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), ddlCRPDHPPBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRBlkCode")))


                    Case objGLSetup.EnumEntryType.DRPDTBSKebun
                        ddlDRPDTBSKebun.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRPDTBSKebun.DataValueField = "AccCode"
                        ddlDRPDTBSKebun.DataTextField = "_Description"
                        ddlDRPDTBSKebun.DataBind()
                        ddlDRPDTBSKebun.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRPDTBSKebun.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRPDTBSKebun.DataValueField = "AccCode"
                        ddlCRPDTBSKebun.DataTextField = "_Description"
                        ddlCRPDTBSKebun.DataBind()
                        ddlCRPDTBSKebun.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), ddlDRPDTBSKebunBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRBlkCode")))
                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), ddlCRPDTBSKebunBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRBlkCode")))


                    Case objGLSetup.EnumEntryType.PDDRStockCPO
                        ddlDRPDStockCPO.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRPDStockCPO.DataValueField = "AccCode"
                        ddlDRPDStockCPO.DataTextField = "_Description"
                        ddlDRPDStockCPO.DataBind()
                        ddlDRPDStockCPO.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRPDStockCPO.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRPDStockCPO.DataValueField = "AccCode"
                        ddlCRPDStockCPO.DataTextField = "_Description"
                        ddlCRPDStockCPO.DataBind()
                        ddlCRPDStockCPO.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.PDDRStockPK
                        ddlDRPDStockPK.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRPDStockPK.DataValueField = "AccCode"
                        ddlDRPDStockPK.DataTextField = "_Description"
                        ddlDRPDStockPK.DataBind()
                        ddlDRPDStockPK.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRPDStockPK.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRPDStockPK.DataValueField = "AccCode"
                        ddlCRPDStockPK.DataTextField = "_Description"
                        ddlCRPDStockPK.DataBind()
                        ddlCRPDStockPK.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.CBDRIntIncome
                        ddlDRIntIncome.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRIntIncome.DataValueField = "AccCode"
                        ddlDRIntIncome.DataTextField = "_Description"
                        ddlDRIntIncome.DataBind()
                        ddlDRIntIncome.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRIntIncome.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRIntIncome.DataValueField = "AccCode"
                        ddlCRIntIncome.DataTextField = "_Description"
                        ddlCRIntIncome.DataBind()
                        ddlCRIntIncome.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))
                
                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), ddlDRIntIncomeBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRBlkCode")))                        
                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), ddlCRIntIncomeBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRBlkCode")))

                    Case objGLSetup.EnumEntryType.CBDRIntIncome2
                        ddlDRIntIncome2.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRIntIncome2.DataValueField = "AccCode"
                        ddlDRIntIncome2.DataTextField = "_Description"
                        ddlDRIntIncome2.DataBind()
                        ddlDRIntIncome2.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRIntIncome2.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRIntIncome2.DataValueField = "AccCode"
                        ddlCRIntIncome2.DataTextField = "_Description"
                        ddlCRIntIncome2.DataBind()
                        ddlCRIntIncome2.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), ddlDRIntIncomeBlkCode2, Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRBlkCode")))
                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), ddlCRIntIncomeBlkCode2, Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRBlkCode")))


                    Case objGLSetup.EnumEntryType.GLDRSunIncome
                        ddlDRSunIncome.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRSunIncome.DataValueField = "AccCode"
                        ddlDRSunIncome.DataTextField = "_Description"
                        ddlDRSunIncome.DataBind()
                         ddlCRIntIncome.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                        ddlCRSunIncome.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRSunIncome.DataValueField = "AccCode"
                        ddlCRSunIncome.DataTextField = "_Description"
                        ddlCRSunIncome.DataBind()
                        ddlCRSunIncome.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), ddlDRSunIncomeBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRBlkCode")))                        
                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), ddlCRSunIncomeBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRBlkCode")))
    

                    Case objGLSetup.EnumEntryType.GLDRVehSuspende
                        ddlDRVehSuspende.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRVehSuspende.DataValueField = "AccCode"
                        ddlDRVehSuspende.DataTextField = "_Description"
                        ddlDRVehSuspende.DataBind()
                        ddlDRVehSuspende.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRVehSuspende.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRVehSuspende.DataValueField = "AccCode"
                        ddlCRVehSuspende.DataTextField = "_Description"
                        ddlCRVehSuspende.DataBind()
                        ddlCRVehSuspende.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.GLDRRetainEarn
                        ddlDRRetainEarn.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRRetainEarn.DataValueField = "AccCode"
                        ddlDRRetainEarn.DataTextField = "_Description"
                        ddlDRRetainEarn.DataBind()
                        ddlDRRetainEarn.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRRetainEarn.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRRetainEarn.DataValueField = "AccCode"
                        ddlCRRetainEarn.DataTextField = "_Description"
                        ddlCRRetainEarn.DataBind()
                        ddlCRRetainEarn.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))
                    Case objGLSetup.EnumEntryType.GLDRBalFrmWS
                        ddlDRBalFrmWSAccCode.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRBalFrmWSAccCode.DataValueField = "AccCode"
                        ddlDRBalFrmWSAccCode.DataTextField = "_Description"
                        ddlDRBalFrmWSAccCode.DataBind()
                        ddlDRBalFrmWSAccCode.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRBalFrmWSAccCode.DataSource = BindAllAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRBalFrmWSAccCode.DataValueField = "AccCode"
                        ddlCRBalFrmWSAccCode.DataTextField = "_Description"
                        ddlCRBalFrmWSAccCode.DataBind()
                        ddlCRBalFrmWSAccCode.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))
                                                                        
                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), ddlDRBalFrmWSBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRBlkCode")))                        
                        BindBlockDropList(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), ddlCRBalFrmWSBlkCode, Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRBlkCode")))
                    
                    Case objGLSetup.EnumEntryType.WSDRCTRLACC
                        ddlDRWSCtrlAcc.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRWSCtrlAcc.DataValueField = "AccCode"
                        ddlDRWSCtrlAcc.DataTextField = "_Description"
                        ddlDRWSCtrlAcc.DataBind()
                        ddlDRWSCtrlAcc.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRWSCtrlAcc.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRWSCtrlAcc.DataValueField = "AccCode"
                        ddlCRWSCtrlAcc.DataTextField = "_Description"
                        ddlCRWSCtrlAcc.DataBind()
                        ddlCRWSCtrlAcc.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                    Case objGLSetup.EnumEntryType.GLDRPEBMovement
                        ddlDRPEBMovementAccCode.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode")), intIndex)
                        ddlDRPEBMovementAccCode.DataValueField = "AccCode"
                        ddlDRPEBMovementAccCode.DataTextField = "_Description"
                        ddlDRPEBMovementAccCode.DataBind()
                        ddlDRPEBMovementAccCode.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("DRAccCode"))

                        ddlCRPEBMovementAccCode.DataSource = BindAccount(Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode")), intIndex)
                        ddlCRPEBMovementAccCode.DataValueField = "AccCode"
                        ddlCRPEBMovementAccCode.DataTextField = "_Description"
                        ddlCRPEBMovementAccCode.DataBind()
                        ddlCRPEBMovementAccCode.SelectedValue = Trim(objGLDs.Tables(0).Rows(intCnt).Item("CRAccCode"))

                End Select
            Next

            objDs = BindAccount("", intIndex)
            objNUDs = BindNurseryAccount("", intNUIndex)

            objDRBalFrmWSDs = BindAllAccount("", intDRBalFrmWSDsIndex)
            objCRBalFrmWSDs = BindAllAccount("", intCRBalFrmWSDsIndex)
            If ddlDRBalFrmWSBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRBalFrmWSBlkCode, "")
            End If
            If ddlCRBalFrmWSBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRBalFrmWSBlkCode, "")
            End If
            If ddlDRIntIncomeBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRIntIncomeBlkCode, "")
            End If
            If ddlCRIntIncomeBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRIntIncomeBlkCode, "")
            End If
            'add
            If ddlCRIntIncomeBlkCode2.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRIntIncomeBlkCode2, "")
            End If

            If ddlDRSunIncomeBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRSunIncomeBlkCode, "")
            End If
            If ddlCRSunIncomeBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRSunIncomeBlkCode, "")
            End If

            If ddlDRAPPPNInvRcv3Blk.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRAPPPNInvRcv3Blk, "")
            End If
            If ddlCRAPPPNInvRcv3Blk.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRAPPPNInvRcv3Blk, "")
            End If

            If ddlDRAPPPNInvRcv3Blk.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRAPPPNInvRcv3Blk, "")
            End If
            If ddlCRAPPPNInvRcv3Blk.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRAPPPNInvRcv3Blk, "")
            End If

            If ddlCRINStockAdjBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRINStockAdjBlkCode, "")
            End If

            If ddlDRPDHPPBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRPDHPPBlkCode, "")
            End If
            If ddlCRPDHPPBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRPDHPPBlkCode, "")
            End If

            If ddlDRPDTBSKebunBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRPDTBSKebunBlkCode, "")
            End If
            If ddlCRPDTBSKebunBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRPDTBSKebunBlkCode, "")
            End If

            If ddlDRINStkRcvDADirectPR.Items.Count = 0 Then
                ddlDRINStkRcvDADirectPR.DataSource = objDs
                ddlDRINStkRcvDADirectPR.DataValueField = "AccCode"
                ddlDRINStkRcvDADirectPR.DataTextField = "_Description"
                ddlDRINStkRcvDADirectPR.DataBind()
                ddlDRINStkRcvDADirectPR.SelectedIndex = intIndex
            End If
            If ddlCRINStkRcvDADirectPR.Items.Count = 0 Then
                ddlCRINStkRcvDADirectPR.DataSource = objDs
                ddlCRINStkRcvDADirectPR.DataValueField = "AccCode"
                ddlCRINStkRcvDADirectPR.DataTextField = "_Description"
                ddlCRINStkRcvDADirectPR.DataBind()
                ddlCRINStkRcvDADirectPR.SelectedIndex = intIndex
            End If
            If ddlDRINStkRcvDAStockPR.Items.Count = 0 Then
                ddlDRINStkRcvDAStockPR.DataSource = objDs
                ddlDRINStkRcvDAStockPR.DataValueField = "AccCode"
                ddlDRINStkRcvDAStockPR.DataTextField = "_Description"
                ddlDRINStkRcvDAStockPR.DataBind()
                ddlDRINStkRcvDAStockPR.SelectedIndex = intIndex
            End If
            If ddlCRINStkRcvDAStockPR.Items.Count = 0 Then
                ddlCRINStkRcvDAStockPR.DataSource = objDs
                ddlCRINStkRcvDAStockPR.DataValueField = "AccCode"
                ddlCRINStkRcvDAStockPR.DataTextField = "_Description"
                ddlCRINStkRcvDAStockPR.DataBind()
                ddlCRINStkRcvDAStockPR.SelectedIndex = intIndex
            End If
            If ddlDRINStkRcvStkTransfer.Items.Count = 0 Then
                ddlDRINStkRcvStkTransfer.DataSource = objDs
                ddlDRINStkRcvStkTransfer.DataValueField = "AccCode"
                ddlDRINStkRcvStkTransfer.DataTextField = "_Description"
                ddlDRINStkRcvStkTransfer.DataBind()
                ddlDRINStkRcvStkTransfer.SelectedIndex = intIndex
            End If
            If ddlCRINStkRcvStkTransfer.Items.Count = 0 Then
                ddlCRINStkRcvStkTransfer.DataSource = objDs
                ddlCRINStkRcvStkTransfer.DataValueField = "AccCode"
                ddlCRINStkRcvStkTransfer.DataTextField = "_Description"
                ddlCRINStkRcvStkTransfer.DataBind()
                ddlCRINStkRcvStkTransfer.SelectedIndex = intIndex
            End If
            If ddlDRINStkRcvStkRtnAdvice.Items.Count = 0 Then
                ddlDRINStkRcvStkRtnAdvice.DataSource = objDs
                ddlDRINStkRcvStkRtnAdvice.DataValueField = "AccCode"
                ddlDRINStkRcvStkRtnAdvice.DataTextField = "_Description"
                ddlDRINStkRcvStkRtnAdvice.DataBind()
                ddlDRINStkRcvStkRtnAdvice.SelectedIndex = intIndex
            End If
            If ddlCRINStkRcvStkRtnAdvice.Items.Count = 0 Then
                ddlCRINStkRcvStkRtnAdvice.DataSource = objDs
                ddlCRINStkRcvStkRtnAdvice.DataValueField = "AccCode"
                ddlCRINStkRcvStkRtnAdvice.DataTextField = "_Description"
                ddlCRINStkRcvStkRtnAdvice.DataBind()
                ddlCRINStkRcvStkRtnAdvice.SelectedIndex = intIndex
            End If
            If ddlDRINStkRtnAdvice.Items.Count = 0 Then
                ddlDRINStkRtnAdvice.DataSource = objDs
                ddlDRINStkRtnAdvice.DataValueField = "AccCode"
                ddlDRINStkRtnAdvice.DataTextField = "_Description"
                ddlDRINStkRtnAdvice.DataBind()
                ddlDRINStkRtnAdvice.SelectedIndex = intIndex
            End If
            If ddlCRINStkRtnAdvice.Items.Count = 0 Then
                ddlCRINStkRtnAdvice.DataSource = objDs
                ddlCRINStkRtnAdvice.DataValueField = "AccCode"
                ddlCRINStkRtnAdvice.DataTextField = "_Description"
                ddlCRINStkRtnAdvice.DataBind()
                ddlCRINStkRtnAdvice.SelectedIndex = intIndex
            End If
            If ddlDRINStkIssueEmp.Items.Count = 0 Then
                ddlDRINStkIssueEmp.DataSource = objDs
                ddlDRINStkIssueEmp.DataValueField = "AccCode"
                ddlDRINStkIssueEmp.DataTextField = "_Description"
                ddlDRINStkIssueEmp.DataBind()
                ddlDRINStkIssueEmp.SelectedIndex = intIndex
            End If
            If ddlCRINStkIssueEmp.Items.Count = 0 Then
                ddlCRINStkIssueEmp.DataSource = objDs
                ddlCRINStkIssueEmp.DataValueField = "AccCode"
                ddlCRINStkIssueEmp.DataTextField = "_Description"
                ddlCRINStkIssueEmp.DataBind()
                ddlCRINStkIssueEmp.SelectedIndex = intIndex
            End If
            If ddlDRINFuelIssueEmp.Items.Count = 0 Then
                ddlDRINFuelIssueEmp.DataSource = objDs
                ddlDRINFuelIssueEmp.DataValueField = "AccCode"
                ddlDRINFuelIssueEmp.DataTextField = "_Description"
                ddlDRINFuelIssueEmp.DataBind()
                ddlDRINFuelIssueEmp.SelectedIndex = intIndex
            End If
            If ddlCRINFuelIssueEmp.Items.Count = 0 Then
                ddlCRINFuelIssueEmp.DataSource = objDs
                ddlCRINFuelIssueEmp.DataValueField = "AccCode"
                ddlCRINFuelIssueEmp.DataTextField = "_Description"
                ddlCRINFuelIssueEmp.DataBind()
                ddlCRINFuelIssueEmp.SelectedIndex = intIndex
            End If
            If ddlDRINBalanceFromStkRtnAdvice.Items.Count = 0 Then
                ddlDRINBalanceFromStkRtnAdvice.DataSource = objDs
                ddlDRINBalanceFromStkRtnAdvice.DataValueField = "AccCode"
                ddlDRINBalanceFromStkRtnAdvice.DataTextField = "_Description"
                ddlDRINBalanceFromStkRtnAdvice.DataBind()
                ddlDRINBalanceFromStkRtnAdvice.SelectedIndex = intIndex
            End If
            If ddlCRINBalanceFromStkRtnAdvice.Items.Count = 0 Then
                ddlCRINBalanceFromStkRtnAdvice.DataSource = objDs
                ddlCRINBalanceFromStkRtnAdvice.DataValueField = "AccCode"
                ddlCRINBalanceFromStkRtnAdvice.DataTextField = "_Description"
                ddlCRINBalanceFromStkRtnAdvice.DataBind()
                ddlCRINBalanceFromStkRtnAdvice.SelectedIndex = intIndex
            End If

            If ddlDRINStockAdj.Items.Count = 0 Then
                ddlDRINStockAdj.DataSource = objDRBalFrmWSDs
                ddlDRINStockAdj.DataValueField = "AccCode"
                ddlDRINStockAdj.DataTextField = "_Description"
                ddlDRINStockAdj.DataBind()
                ddlDRINStockAdj.SelectedIndex = intDRBalFrmWSDsIndex
            End If
            If ddlCRINStockAdj.Items.Count = 0 Then
                ddlCRINStockAdj.DataSource = objCRBalFrmWSDs
                ddlCRINStockAdj.DataValueField = "AccCode"
                ddlCRINStockAdj.DataTextField = "_Description"
                ddlCRINStockAdj.DataBind()
                ddlCRINStockAdj.SelectedIndex = intCRBalFrmWSDsIndex
            End If

            If ddlDRCTStkRcvDADirectPR.Items.Count = 0 Then
                ddlDRCTStkRcvDADirectPR.DataSource = objDs
                ddlDRCTStkRcvDADirectPR.DataValueField = "AccCode"
                ddlDRCTStkRcvDADirectPR.DataTextField = "_Description"
                ddlDRCTStkRcvDADirectPR.DataBind()
                ddlDRCTStkRcvDADirectPR.SelectedIndex = intIndex
            End If
            If ddlCRCTStkRcvDADirectPR.Items.Count = 0 Then
                ddlCRCTStkRcvDADirectPR.DataSource = objDs
                ddlCRCTStkRcvDADirectPR.DataValueField = "AccCode"
                ddlCRCTStkRcvDADirectPR.DataTextField = "_Description"
                ddlCRCTStkRcvDADirectPR.DataBind()
                ddlCRCTStkRcvDADirectPR.SelectedIndex = intIndex
            End If
            If ddlDRCTStkRcvDAStockPR.Items.Count = 0 Then
                ddlDRCTStkRcvDAStockPR.DataSource = objDs
                ddlDRCTStkRcvDAStockPR.DataValueField = "AccCode"
                ddlDRCTStkRcvDAStockPR.DataTextField = "_Description"
                ddlDRCTStkRcvDAStockPR.DataBind()
                ddlDRCTStkRcvDAStockPR.SelectedIndex = intIndex
            End If
            If ddlCRCTStkRcvDAStockPR.Items.Count = 0 Then
                ddlCRCTStkRcvDAStockPR.DataSource = objDs
                ddlCRCTStkRcvDAStockPR.DataValueField = "AccCode"
                ddlCRCTStkRcvDAStockPR.DataTextField = "_Description"
                ddlCRCTStkRcvDAStockPR.DataBind()
                ddlCRCTStkRcvDAStockPR.SelectedIndex = intIndex
            End If
            If ddlDRCTStkRcvStkTransfer.Items.Count = 0 Then
                ddlDRCTStkRcvStkTransfer.DataSource = objDs
                ddlDRCTStkRcvStkTransfer.DataValueField = "AccCode"
                ddlDRCTStkRcvStkTransfer.DataTextField = "_Description"
                ddlDRCTStkRcvStkTransfer.DataBind()
                ddlDRCTStkRcvStkTransfer.SelectedIndex = intIndex
            End If
            If ddlCRCTStkRcvStkTransfer.Items.Count = 0 Then
                ddlCRCTStkRcvStkTransfer.DataSource = objDs
                ddlCRCTStkRcvStkTransfer.DataValueField = "AccCode"
                ddlCRCTStkRcvStkTransfer.DataTextField = "_Description"
                ddlCRCTStkRcvStkTransfer.DataBind()
                ddlCRCTStkRcvStkTransfer.SelectedIndex = intIndex
            End If
            If ddlDRCTStkRcvStkRtnAdvice.Items.Count = 0 Then
                ddlDRCTStkRcvStkRtnAdvice.DataSource = objDs
                ddlDRCTStkRcvStkRtnAdvice.DataValueField = "AccCode"
                ddlDRCTStkRcvStkRtnAdvice.DataTextField = "_Description"
                ddlDRCTStkRcvStkRtnAdvice.DataBind()
                ddlDRCTStkRcvStkRtnAdvice.SelectedIndex = intIndex
            End If
            If ddlCRCTStkRcvStkRtnAdvice.Items.Count = 0 Then
                ddlCRCTStkRcvStkRtnAdvice.DataSource = objDs
                ddlCRCTStkRcvStkRtnAdvice.DataValueField = "AccCode"
                ddlCRCTStkRcvStkRtnAdvice.DataTextField = "_Description"
                ddlCRCTStkRcvStkRtnAdvice.DataBind()
                ddlCRCTStkRcvStkRtnAdvice.SelectedIndex = intIndex
            End If
            If ddlDRCTStkRtnAdvice.Items.Count = 0 Then
                ddlDRCTStkRtnAdvice.DataSource = objDs
                ddlDRCTStkRtnAdvice.DataValueField = "AccCode"
                ddlDRCTStkRtnAdvice.DataTextField = "_Description"
                ddlDRCTStkRtnAdvice.DataBind()
                ddlDRCTStkRtnAdvice.SelectedIndex = intIndex
            End If
            If ddlCRCTStkRtnAdvice.Items.Count = 0 Then
                ddlCRCTStkRtnAdvice.DataSource = objDs
                ddlCRCTStkRtnAdvice.DataValueField = "AccCode"
                ddlCRCTStkRtnAdvice.DataTextField = "_Description"
                ddlCRCTStkRtnAdvice.DataBind()
                ddlCRCTStkRtnAdvice.SelectedIndex = intIndex
            End If
            If ddlDRCTStkIssueEmp.Items.Count = 0 Then
                ddlDRCTStkIssueEmp.DataSource = objDs
                ddlDRCTStkIssueEmp.DataValueField = "AccCode"
                ddlDRCTStkIssueEmp.DataTextField = "_Description"
                ddlDRCTStkIssueEmp.DataBind()
                ddlDRCTStkIssueEmp.SelectedIndex = intIndex
            End If
            If ddlCRCTStkIssueEmp.Items.Count = 0 Then
                ddlCRCTStkIssueEmp.DataSource = objDs
                ddlCRCTStkIssueEmp.DataValueField = "AccCode"
                ddlCRCTStkIssueEmp.DataTextField = "_Description"
                ddlCRCTStkIssueEmp.DataBind()
                ddlCRCTStkIssueEmp.SelectedIndex = intIndex
            End If
            If ddlDRCTBalanceFromCTRtnAdvice.Items.Count = 0 Then
                ddlDRCTBalanceFromCTRtnAdvice.DataSource = objDs
                ddlDRCTBalanceFromCTRtnAdvice.DataValueField = "AccCode"
                ddlDRCTBalanceFromCTRtnAdvice.DataTextField = "_Description"
                ddlDRCTBalanceFromCTRtnAdvice.DataBind()
                ddlDRCTBalanceFromCTRtnAdvice.SelectedIndex = intIndex
            End If
            If ddlCRCTBalanceFromCTRtnAdvice.Items.Count = 0 Then
                ddlCRCTBalanceFromCTRtnAdvice.DataSource = objDs
                ddlCRCTBalanceFromCTRtnAdvice.DataValueField = "AccCode"
                ddlCRCTBalanceFromCTRtnAdvice.DataTextField = "_Description"
                ddlCRCTBalanceFromCTRtnAdvice.DataBind()
                ddlCRCTBalanceFromCTRtnAdvice.SelectedIndex = intIndex
            End If
            If ddlDRWSJobEmp.Items.Count = 0 Then
                ddlDRWSJobEmp.DataSource = objDs
                ddlDRWSJobEmp.DataValueField = "AccCode"
                ddlDRWSJobEmp.DataTextField = "_Description"
                ddlDRWSJobEmp.DataBind()
                ddlDRWSJobEmp.SelectedIndex = intIndex
            End If
            If ddlCRWSJobEmp.Items.Count = 0 Then
                ddlCRWSJobEmp.DataSource = objDs
                ddlCRWSJobEmp.DataValueField = "AccCode"
                ddlCRWSJobEmp.DataTextField = "_Description"
                ddlCRWSJobEmp.DataBind()
                ddlCRWSJobEmp.SelectedIndex = intIndex
            End If
            If ddlDRNUSeedlingsIssue.Items.Count = 0 Then
                ddlDRNUSeedlingsIssue.DataSource = objNUDs
                ddlDRNUSeedlingsIssue.DataValueField = "AccCode"
                ddlDRNUSeedlingsIssue.DataTextField = "_Description"
                ddlDRNUSeedlingsIssue.DataBind()
                ddlDRNUSeedlingsIssue.SelectedIndex = intNUIndex
            End If
            If ddlCRNUSeedlingsIssue.Items.Count = 0 Then
                ddlCRNUSeedlingsIssue.DataSource = objNUDs
                ddlCRNUSeedlingsIssue.DataValueField = "AccCode"
                ddlCRNUSeedlingsIssue.DataTextField = "_Description"
                ddlCRNUSeedlingsIssue.DataBind()
                ddlCRNUSeedlingsIssue.SelectedIndex = intNUIndex
            End If
            If ddlDRPUGoodsRcv.Items.Count = 0 Then
                ddlDRPUGoodsRcv.DataSource = objDs
                ddlDRPUGoodsRcv.DataValueField = "AccCode"
                ddlDRPUGoodsRcv.DataTextField = "_Description"
                ddlDRPUGoodsRcv.DataBind()
                ddlDRPUGoodsRcv.SelectedIndex = intIndex
            End If
            If ddlCRPUGoodsRcv.Items.Count = 0 Then
                ddlCRPUGoodsRcv.DataSource = objDs
                ddlCRPUGoodsRcv.DataValueField = "AccCode"
                ddlCRPUGoodsRcv.DataTextField = "_Description"
                ddlCRPUGoodsRcv.DataBind()
                ddlCRPUGoodsRcv.SelectedIndex = intIndex
            End If
            If ddlDRPUDispAdv.Items.Count = 0 Then
                ddlDRPUDispAdv.DataSource = objDs
                ddlDRPUDispAdv.DataValueField = "AccCode"
                ddlDRPUDispAdv.DataTextField = "_Description"
                ddlDRPUDispAdv.DataBind()
                ddlDRPUDispAdv.SelectedIndex = intIndex
            End If
            If ddlCRPUDispAdv.Items.Count = 0 Then
                ddlCRPUDispAdv.DataSource = objDs
                ddlCRPUDispAdv.DataValueField = "AccCode"
                ddlCRPUDispAdv.DataTextField = "_Description"
                ddlCRPUDispAdv.DataBind()
                ddlCRPUDispAdv.SelectedIndex = intIndex
            End If
            If ddlDRAPInvRcv.Items.Count = 0 Then
                ddlDRAPInvRcv.DataSource = objDs
                ddlDRAPInvRcv.DataValueField = "AccCode"
                ddlDRAPInvRcv.DataTextField = "_Description"
                ddlDRAPInvRcv.DataBind()
                ddlDRAPInvRcv.SelectedIndex = intIndex
            End If
            If ddlCRAPInvRcv.Items.Count = 0 Then
                ddlCRAPInvRcv.DataSource = objDs
                ddlCRAPInvRcv.DataValueField = "AccCode"
                ddlCRAPInvRcv.DataTextField = "_Description"
                ddlCRAPInvRcv.DataBind()
                ddlCRAPInvRcv.SelectedIndex = intIndex
            End If
            If ddlDRAPPPNInvRcv.Items.Count = 0 Then
                ddlDRAPPPNInvRcv.DataSource = objDs
                ddlDRAPPPNInvRcv.DataValueField = "AccCode"
                ddlDRAPPPNInvRcv.DataTextField = "_Description"
                ddlDRAPPPNInvRcv.DataBind()
                ddlDRAPPPNInvRcv.SelectedIndex = intIndex
            End If
            If ddlCRAPPPNInvRcv.Items.Count = 0 Then
                ddlCRAPPPNInvRcv.DataSource = objDs
                ddlCRAPPPNInvRcv.DataValueField = "AccCode"
                ddlCRAPPPNInvRcv.DataTextField = "_Description"
                ddlCRAPPPNInvRcv.DataBind()
                ddlCRAPPPNInvRcv.SelectedIndex = intIndex
            End If

            If ddlDRAPPPNInvRcv2.Items.Count = 0 Then
                ddlDRAPPPNInvRcv2.DataSource = objDs
                ddlDRAPPPNInvRcv2.DataValueField = "AccCode"
                ddlDRAPPPNInvRcv2.DataTextField = "_Description"
                ddlDRAPPPNInvRcv2.DataBind()
                ddlDRAPPPNInvRcv2.SelectedIndex = intIndex
            End If
            If ddlCRAPPPNInvRcv2.Items.Count = 0 Then
                ddlCRAPPPNInvRcv2.DataSource = objDs
                ddlCRAPPPNInvRcv2.DataValueField = "AccCode"
                ddlCRAPPPNInvRcv2.DataTextField = "_Description"
                ddlCRAPPPNInvRcv2.DataBind()
                ddlCRAPPPNInvRcv2.SelectedIndex = intIndex
            End If

            If ddlDRAPPPNInvRcv4.Items.Count = 0 Then
                ddlDRAPPPNInvRcv4.DataSource = objDs
                ddlDRAPPPNInvRcv4.DataValueField = "AccCode"
                ddlDRAPPPNInvRcv4.DataTextField = "_Description"
                ddlDRAPPPNInvRcv4.DataBind()
                ddlDRAPPPNInvRcv4.SelectedIndex = intIndex
            End If
            If ddlCRAPPPNInvRcv4.Items.Count = 0 Then
                ddlCRAPPPNInvRcv4.DataSource = objDs
                ddlCRAPPPNInvRcv4.DataValueField = "AccCode"
                ddlCRAPPPNInvRcv4.DataTextField = "_Description"
                ddlCRAPPPNInvRcv4.DataBind()
                ddlCRAPPPNInvRcv4.SelectedIndex = intIndex
            End If

           

            If ddlDRAPPPHInvRcv.Items.Count = 0 Then
                ddlDRAPPPHInvRcv.DataSource = objDs
                ddlDRAPPPHInvRcv.DataValueField = "AccCode"
                ddlDRAPPPHInvRcv.DataTextField = "_Description"
                ddlDRAPPPHInvRcv.DataBind()
                ddlDRAPPPHInvRcv.SelectedIndex = intIndex
            End If
            If ddlCRAPPPHInvRcv.Items.Count = 0 Then
                ddlCRAPPPHInvRcv.DataSource = objDs
                ddlCRAPPPHInvRcv.DataValueField = "AccCode"
                ddlCRAPPPHInvRcv.DataTextField = "_Description"
                ddlCRAPPPHInvRcv.DataBind()
                ddlCRAPPPHInvRcv.SelectedIndex = intIndex
            End If

            If ddlDRCBPPHInvPay.Items.Count = 0 Then
                ddlDRCBPPHInvPay.DataSource = objDs
                ddlDRCBPPHInvPay.DataValueField = "AccCode"
                ddlDRCBPPHInvPay.DataTextField = "_Description"
                ddlDRCBPPHInvPay.DataBind()
                ddlDRCBPPHInvPay.SelectedIndex = intIndex
            End If
            If ddlCRCBPPHInvPay.Items.Count = 0 Then
                ddlCRCBPPHInvPay.DataSource = objDs
                ddlCRCBPPHInvPay.DataValueField = "AccCode"
                ddlCRCBPPHInvPay.DataTextField = "_Description"
                ddlCRCBPPHInvPay.DataBind()
                ddlCRCBPPHInvPay.SelectedIndex = intIndex
            End If
            If ddlDRAPPPNCrdJrn.Items.Count = 0 Then
                ddlDRAPPPNCrdJrn.DataSource = objDs
                ddlDRAPPPNCrdJrn.DataValueField = "AccCode"
                ddlDRAPPPNCrdJrn.DataTextField = "_Description"
                ddlDRAPPPNCrdJrn.DataBind()
                ddlDRAPPPNCrdJrn.SelectedIndex = intIndex
            End If
            If ddlCRAPPPNCrdJrn.Items.Count = 0 Then
                ddlCRAPPPNCrdJrn.DataSource = objDs
                ddlCRAPPPNCrdJrn.DataValueField = "AccCode"
                ddlCRAPPPNCrdJrn.DataTextField = "_Description"
                ddlCRAPPPNCrdJrn.DataBind()
                ddlCRAPPPNCrdJrn.SelectedIndex = intIndex
            End If
            If ddlDRAPPPHCrdJrn.Items.Count = 0 Then
                ddlDRAPPPHCrdJrn.DataSource = objDs
                ddlDRAPPPHCrdJrn.DataValueField = "AccCode"
                ddlDRAPPPHCrdJrn.DataTextField = "_Description"
                ddlDRAPPPHCrdJrn.DataBind()
                ddlDRAPPPHCrdJrn.SelectedIndex = intIndex
            End If
            If ddlCRAPPPHCrdJrn.Items.Count = 0 Then
                ddlCRAPPPHCrdJrn.DataSource = objDs
                ddlCRAPPPHCrdJrn.DataValueField = "AccCode"
                ddlCRAPPPHCrdJrn.DataTextField = "_Description"
                ddlCRAPPPHCrdJrn.DataBind()
                ddlCRAPPPHCrdJrn.SelectedIndex = intIndex
            End If
            If ddlDRAdvPayment.Items.Count = 0 Then
                ddlDRAdvPayment.DataSource = objDs
                ddlDRAdvPayment.DataValueField = "AccCode"
                ddlDRAdvPayment.DataTextField = "_Description"
                ddlDRAdvPayment.DataBind()
                ddlDRAdvPayment.SelectedIndex = intIndex
            End If
            If ddlCRAdvPayment.Items.Count = 0 Then
                ddlCRAdvPayment.DataSource = objDs
                ddlCRAdvPayment.DataValueField = "AccCode"
                ddlCRAdvPayment.DataTextField = "_Description"
                ddlCRAdvPayment.DataBind()
                ddlCRAdvPayment.SelectedIndex = intIndex
            End If
            If ddlDRBIPPNInvRcv.Items.Count = 0 Then
                ddlDRBIPPNInvRcv.DataSource = objDs
                ddlDRBIPPNInvRcv.DataValueField = "AccCode"
                ddlDRBIPPNInvRcv.DataTextField = "_Description"
                ddlDRBIPPNInvRcv.DataBind()
                ddlDRBIPPNInvRcv.SelectedIndex = intIndex
            End If
            If ddlCRBIPPNInvRcv.Items.Count = 0 Then
                ddlCRBIPPNInvRcv.DataSource = objDs
                ddlCRBIPPNInvRcv.DataValueField = "AccCode"
                ddlCRBIPPNInvRcv.DataTextField = "_Description"
                ddlCRBIPPNInvRcv.DataBind()
                ddlCRBIPPNInvRcv.SelectedIndex = intIndex
            End If
            If ddlDRBIPPHInvRcv.Items.Count = 0 Then
                ddlDRBIPPHInvRcv.DataSource = objDs
                ddlDRBIPPHInvRcv.DataValueField = "AccCode"
                ddlDRBIPPHInvRcv.DataTextField = "_Description"
                ddlDRBIPPHInvRcv.DataBind()
                ddlDRBIPPHInvRcv.SelectedIndex = intIndex
            End If
            If ddlCRBIPPHInvRcv.Items.Count = 0 Then
                ddlCRBIPPHInvRcv.DataSource = objDs
                ddlCRBIPPHInvRcv.DataValueField = "AccCode"
                ddlCRBIPPHInvRcv.DataTextField = "_Description"
                ddlCRBIPPHInvRcv.DataBind()
                ddlCRBIPPHInvRcv.SelectedIndex = intIndex
            End If


             If ddlDRCBPPNRcpt.Items.Count = 0 Then
                ddlDRCBPPNRcpt.DataSource = objDs
                ddlDRCBPPNRcpt.DataValueField = "AccCode"
                ddlDRCBPPNRcpt.DataTextField = "_Description"
                ddlDRCBPPNRcpt.DataBind()
                ddlDRCBPPNRcpt.SelectedIndex = intIndex
            End If
            If ddlCRCBPPNRcpt.Items.Count = 0 Then
                ddlCRCBPPNRcpt.DataSource = objDs
                ddlCRCBPPNRcpt.DataValueField = "AccCode"
                ddlCRCBPPNRcpt.DataTextField = "_Description"
                ddlCRCBPPNRcpt.DataBind()
                ddlCRCBPPNRcpt.SelectedIndex = intIndex
            End If
            If ddlDRCBPPHRcpt.Items.Count = 0 Then
                ddlDRCBPPHRcpt.DataSource = objDs
                ddlDRCBPPHRcpt.DataValueField = "AccCode"
                ddlDRCBPPHRcpt.DataTextField = "_Description"
                ddlDRCBPPHRcpt.DataBind()
                ddlDRCBPPHRcpt.SelectedIndex = intIndex
            End If
            If ddlCRCBPPHRcpt.Items.Count = 0 Then
                ddlCRCBPPHRcpt.DataSource = objDs
                ddlCRCBPPHRcpt.DataValueField = "AccCode"
                ddlCRCBPPHRcpt.DataTextField = "_Description"
                ddlCRCBPPHRcpt.DataBind()
                ddlCRCBPPHRcpt.SelectedIndex = intIndex
            End If
            If ddlDRPRClr.Items.Count = 0 Then
                ddlDRPRClr.DataSource = objDs
                ddlDRPRClr.DataValueField = "AccCode"
                ddlDRPRClr.DataTextField = "_Description"
                ddlDRPRClr.DataBind()
                ddlDRPRClr.SelectedIndex = intIndex
            End If
            If ddlCRPRClr.Items.Count = 0 Then
                ddlCRPRClr.DataSource = objDs
                ddlCRPRClr.DataValueField = "AccCode"
                ddlCRPRClr.DataTextField = "_Description"
                ddlCRPRClr.DataBind()
                ddlCRPRClr.SelectedIndex = intIndex
            End If
            If ddlDRPRHK.Items.Count = 0 Then
                ddlDRPRHK.DataSource = objDs
                ddlDRPRHK.DataValueField = "AccCode"
                ddlDRPRHK.DataTextField = "_Description"
                ddlDRPRHK.DataBind()
                ddlDRPRHK.SelectedIndex = intIndex
            End If
            If ddlCRPRHK.Items.Count = 0 Then
                ddlCRPRHK.DataSource = objDs
                ddlCRPRHK.DataValueField = "AccCode"
                ddlCRPRHK.DataTextField = "_Description"
                ddlCRPRHK.DataBind()
                ddlCRPRHK.SelectedIndex = intIndex
            End If
            If ddlDREstYield.Items.Count = 0 Then
                ddlDREstYield.DataSource = objDs
                ddlDREstYield.DataValueField = "AccCode"
                ddlDREstYield.DataTextField = "_Description"
                ddlDREstYield.DataBind()
                ddlDREstYield.SelectedIndex = intIndex
            End If
            If ddlCREstYield.Items.Count = 0 Then
                ddlCREstYield.DataSource = objDs
                ddlCREstYield.DataValueField = "AccCode"
                ddlCREstYield.DataTextField = "_Description"
                ddlCREstYield.DataBind()
                ddlCREstYield.SelectedIndex = intIndex
            End If

            If ddlDRPDHPPCost.Items.Count = 0 Then
                ddlDRPDHPPCost.DataSource = objDRBalFrmWSDs
                ddlDRPDHPPCost.DataValueField = "AccCode"
                ddlDRPDHPPCost.DataTextField = "_Description"
                ddlDRPDHPPCost.DataBind()
                ddlDRPDHPPCost.SelectedIndex = intDRBalFrmWSDsIndex
            End If
            If ddlCRPDHPPCost.Items.Count = 0 Then
                ddlCRPDHPPCost.DataSource = objCRBalFrmWSDs
                ddlCRPDHPPCost.DataValueField = "AccCode"
                ddlCRPDHPPCost.DataTextField = "_Description"
                ddlCRPDHPPCost.DataBind()
                ddlCRPDHPPCost.SelectedIndex = intCRBalFrmWSDsIndex
            End If
            If ddlDRPDTBSKebun.Items.Count = 0 Then
                ddlDRPDTBSKebun.DataSource = objDRBalFrmWSDs
                ddlDRPDTBSKebun.DataValueField = "AccCode"
                ddlDRPDTBSKebun.DataTextField = "_Description"
                ddlDRPDTBSKebun.DataBind()
                ddlDRPDTBSKebun.SelectedIndex = intDRBalFrmWSDsIndex
            End If
            If ddlCRPDTBSKebun.Items.Count = 0 Then
                ddlCRPDTBSKebun.DataSource = objCRBalFrmWSDs
                ddlCRPDTBSKebun.DataValueField = "AccCode"
                ddlCRPDTBSKebun.DataTextField = "_Description"
                ddlCRPDTBSKebun.DataBind()
                ddlCRPDTBSKebun.SelectedIndex = intCRBalFrmWSDsIndex
            End If
            If ddlDRPDStockCPO.Items.Count = 0 Then
                ddlDRPDStockCPO.DataSource = objDRBalFrmWSDs
                ddlDRPDStockCPO.DataValueField = "AccCode"
                ddlDRPDStockCPO.DataTextField = "_Description"
                ddlDRPDStockCPO.DataBind()
                ddlDRPDStockCPO.SelectedIndex = intIndex
            End If
            If ddlCRPDStockCPO.Items.Count = 0 Then
                ddlCRPDStockCPO.DataSource = objDRBalFrmWSDs
                ddlCRPDStockCPO.DataValueField = "AccCode"
                ddlCRPDStockCPO.DataTextField = "_Description"
                ddlCRPDStockCPO.DataBind()
                ddlCRPDStockCPO.SelectedIndex = intIndex
            End If
            If ddlDRPDStockPK.Items.Count = 0 Then
                ddlDRPDStockPK.DataSource = objDs
                ddlDRPDStockPK.DataValueField = "AccCode"
                ddlDRPDStockPK.DataTextField = "_Description"
                ddlDRPDStockPK.DataBind()
                ddlDRPDStockPK.SelectedIndex = intIndex
            End If
            If ddlCRPDStockPK.Items.Count = 0 Then
                ddlCRPDStockPK.DataSource = objDs
                ddlCRPDStockPK.DataValueField = "AccCode"
                ddlCRPDStockPK.DataTextField = "_Description"
                ddlCRPDStockPK.DataBind()
                ddlCRPDStockPK.SelectedIndex = intIndex
            End If

            If ddlDRIntIncome.Items.Count = 0 Then
                ddlDRIntIncome.DataSource = objDRBalFrmWSDs
                ddlDRIntIncome.DataValueField = "AccCode"
                ddlDRIntIncome.DataTextField = "_Description"
                ddlDRIntIncome.DataBind()
                ddlDRIntIncome.SelectedIndex = intDRBalFrmWSDsIndex
            End If
            If ddlCRIntIncome.Items.Count = 0 Then
                ddlDRIntIncome.DataSource = objDRBalFrmWSDs
                ddlCRIntIncome.DataValueField = "AccCode"
                ddlCRIntIncome.DataTextField = "_Description"
                ddlCRIntIncome.DataBind()
                ddlCRIntIncome.SelectedIndex = intDRBalFrmWSDsIndex
            End If

            If ddlDRIntIncome2.Items.Count = 0 Then
                ddlDRIntIncome2.DataSource = objDRBalFrmWSDs
                ddlDRIntIncome2.DataValueField = "AccCode"
                ddlDRIntIncome2.DataTextField = "_Description"
                ddlDRIntIncome2.DataBind()
                ddlDRIntIncome2.SelectedIndex = intDRBalFrmWSDsIndex
            End If
            If ddlCRIntIncome2.Items.Count = 0 Then
                ddlDRIntIncome2.DataSource = objDRBalFrmWSDs
                ddlCRIntIncome2.DataValueField = "AccCode"
                ddlCRIntIncome2.DataTextField = "_Description"
                ddlCRIntIncome2.DataBind()
                ddlCRIntIncome2.SelectedIndex = intDRBalFrmWSDsIndex
            End If

            If ddlDRAPPPNInvRcv3.Items.Count = 0 Then
                ddlDRAPPPNInvRcv3.DataSource = objDRBalFrmWSDs
                ddlDRAPPPNInvRcv3.DataValueField = "AccCode"
                ddlDRAPPPNInvRcv3.DataTextField = "_Description"
                ddlDRAPPPNInvRcv3.DataBind()
                ddlDRAPPPNInvRcv3.SelectedIndex = intDRBalFrmWSDsIndex
            End If
            If ddlCRAPPPNInvRcv3.Items.Count = 0 Then
                ddlCRAPPPNInvRcv3.DataSource = objDRBalFrmWSDs
                ddlCRAPPPNInvRcv3.DataValueField = "AccCode"
                ddlCRAPPPNInvRcv3.DataTextField = "_Description"
                ddlCRAPPPNInvRcv3.DataBind()
                ddlCRAPPPNInvRcv3.SelectedIndex = intDRBalFrmWSDsIndex
            End If




            If ddlDRSunIncome.Items.Count = 0 Then
                ddlDRSunIncome.DataSource = objDRBalFrmWSDs
                ddlDRSunIncome.DataValueField = "AccCode"
                ddlDRSunIncome.DataTextField = "_Description"
                ddlDRSunIncome.DataBind()
                ddlDRSunIncome.SelectedIndex = intDRBalFrmWSDsIndex
            End If
            If ddlCRSunIncome.Items.Count = 0 Then
                ddlCRSunIncome.DataSource = objDRBalFrmWSDs
                ddlCRSunIncome.DataValueField = "AccCode"
                ddlCRSunIncome.DataTextField = "_Description"
                ddlCRSunIncome.DataBind()
                ddlCRSunIncome.SelectedIndex = intDRBalFrmWSDsIndex
            End If

            If ddlDRVehSuspende.Items.Count = 0 Then
                ddlDRVehSuspende.DataSource = objDs
                ddlDRVehSuspende.DataValueField = "AccCode"
                ddlDRVehSuspende.DataTextField = "_Description"
                ddlDRVehSuspende.DataBind()
                ddlDRVehSuspende.SelectedIndex = intIndex
            End If
            If ddlCRVehSuspende.Items.Count = 0 Then
                ddlCRVehSuspende.DataSource = objDs
                ddlCRVehSuspende.DataValueField = "AccCode"
                ddlCRVehSuspende.DataTextField = "_Description"
                ddlCRVehSuspende.DataBind()
                ddlCRVehSuspende.SelectedIndex = intIndex
            End If
            If ddlDRRetainEarn.Items.Count = 0 Then
                ddlDRRetainEarn.DataSource = objDs
                ddlDRRetainEarn.DataValueField = "AccCode"
                ddlDRRetainEarn.DataTextField = "_Description"
                ddlDRRetainEarn.DataBind()
                ddlDRRetainEarn.SelectedIndex = intIndex
            End If
            If ddlCRRetainEarn.Items.Count = 0 Then
                ddlCRRetainEarn.DataSource = objDs
                ddlCRRetainEarn.DataValueField = "AccCode"
                ddlCRRetainEarn.DataTextField = "_Description"
                ddlCRRetainEarn.DataBind()
                ddlCRRetainEarn.SelectedIndex = intIndex
            End If

            If ddlDRBalFrmWSAccCode.Items.Count = 0 Then
                ddlDRBalFrmWSAccCode.DataSource = objDRBalFrmWSDs
                ddlDRBalFrmWSAccCode.DataValueField = "AccCode"
                ddlDRBalFrmWSAccCode.DataTextField = "_Description"
                ddlDRBalFrmWSAccCode.DataBind()
                ddlDRBalFrmWSAccCode.SelectedIndex = intDRBalFrmWSDsIndex
            End If            
            If ddlCRBalFrmWSAccCode.Items.Count = 0 Then
                ddlCRBalFrmWSAccCode.DataSource = objCRBalFrmWSDs
                ddlCRBalFrmWSAccCode.DataValueField = "AccCode"
                ddlCRBalFrmWSAccCode.DataTextField = "_Description"
                ddlCRBalFrmWSAccCode.DataBind()
                ddlCRBalFrmWSAccCode.SelectedIndex = intCRBalFrmWSDsIndex
            End If 

            If ddlDRWSCtrlAcc.Items.Count = 0 Then
                ddlDRWSCtrlAcc.DataSource = objDs
                ddlDRWSCtrlAcc.DataValueField = "AccCode"
                ddlDRWSCtrlAcc.DataTextField = "_Description"
                ddlDRWSCtrlAcc.DataBind()
                ddlDRWSCtrlAcc.SelectedIndex = intIndex
            End If 
            If ddlCRWSCtrlAcc.Items.Count = 0 Then
                ddlCRWSCtrlAcc.DataSource = objDs
                ddlCRWSCtrlAcc.DataValueField = "AccCode"
                ddlCRWSCtrlAcc.DataTextField = "_Description"
                ddlCRWSCtrlAcc.DataBind()
                ddlCRWSCtrlAcc.SelectedIndex = intIndex
            End If 
            If ddlDRPEBMovementAccCode.Items.Count = 0 Then
                ddlDRPEBMovementAccCode.DataSource = objDs
                ddlDRPEBMovementAccCode.DataValueField = "AccCode"
                ddlDRPEBMovementAccCode.DataTextField = "_Description"
                ddlDRPEBMovementAccCode.DataBind()
                ddlDRPEBMovementAccCode.SelectedIndex = intIndex
            End If

            If ddlCRPEBMovementAccCode.Items.Count = 0 Then
                ddlCRPEBMovementAccCode.DataSource = objDs
                ddlCRPEBMovementAccCode.DataValueField = "AccCode"
                ddlCRPEBMovementAccCode.DataTextField = "_Description"
                ddlCRPEBMovementAccCode.DataBind()
                ddlCRPEBMovementAccCode.SelectedIndex = intIndex
            End If

        Else
            objDs = BindAccount("", intIndex)
            objNUDs = BindNurseryAccount("", intNUIndex)

            objDRBalFrmWSDs = BindAllAccount("", intDRBalFrmWSDsIndex)
            objCRBalFrmWSDs = BindAllAccount("", intCRBalFrmWSDsIndex)
            If ddlDRBalFrmWSBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRBalFrmWSBlkCode, "")
            End If
            If ddlCRBalFrmWSBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRBalFrmWSBlkCode, "")
            End If

            If ddlDRIntIncomeBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRIntIncomeBlkCode, "")
            End If
            If ddlCRIntIncomeBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRIntIncomeBlkCode, "")
            End If

            'add
            If ddlCRIntIncomeBlkCode2.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRIntIncomeBlkCode2, "")
            End If

            If ddlDRSunIncomeBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRSunIncomeBlkCode, "")
            End If
            If ddlCRSunIncomeBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRSunIncomeBlkCode, "")
            End If

            If ddlDRAPPPNInvRcv3Blk.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRAPPPNInvRcv3Blk, "")
            End If
            If ddlCRAPPPNInvRcv3Blk.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRAPPPNInvRcv3Blk, "")
            End If

            If ddlDRAPPPNInvRcv3Blk.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRAPPPNInvRcv3Blk, "")
            End If
            If ddlCRAPPPNInvRcv3Blk.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRAPPPNInvRcv3Blk, "")
            End If

            If ddlDRINStockAdjBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRINStockAdjBlkCode, "")
            End If
            If ddlCRINStockAdjBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRINStockAdjBlkCode, "")
            End If


            If ddlDRPDHPPBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRPDHPPBlkCode, "")
            End If
            If ddlCRPDHPPBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRPDHPPBlkCode, "")
            End If

            If ddlDRPDTBSKebunBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlDRPDTBSKebunBlkCode, "")
            End If
            If ddlCRPDTBSKebunBlkCode.Items.Count <= 0 Then
                BindBlockDropList("", ddlCRPDTBSKebunBlkCode, "")
            End If

            ddlDRINStkRcvDADirectPR.DataSource = objDs
            ddlDRINStkRcvDADirectPR.DataValueField = "AccCode"
            ddlDRINStkRcvDADirectPR.DataTextField = "_Description"
            ddlDRINStkRcvDADirectPR.DataBind()
            ddlDRINStkRcvDADirectPR.SelectedIndex = intIndex

            ddlCRINStkRcvDADirectPR.DataSource = objDs
            ddlCRINStkRcvDADirectPR.DataValueField = "AccCode"
            ddlCRINStkRcvDADirectPR.DataTextField = "_Description"
            ddlCRINStkRcvDADirectPR.DataBind()
            ddlCRINStkRcvDADirectPR.SelectedIndex = intIndex

            ddlDRINStkRcvDAStockPR.DataSource = objDs
            ddlDRINStkRcvDAStockPR.DataValueField = "AccCode"
            ddlDRINStkRcvDAStockPR.DataTextField = "_Description"
            ddlDRINStkRcvDAStockPR.DataBind()
            ddlDRINStkRcvDAStockPR.SelectedIndex = intIndex

            ddlCRINStkRcvDAStockPR.DataSource = objDs
            ddlCRINStkRcvDAStockPR.DataValueField = "AccCode"
            ddlCRINStkRcvDAStockPR.DataTextField = "_Description"
            ddlCRINStkRcvDAStockPR.DataBind()
            ddlCRINStkRcvDAStockPR.SelectedIndex = intIndex

            ddlDRINStkRcvStkTransfer.DataSource = objDs
            ddlDRINStkRcvStkTransfer.DataValueField = "AccCode"
            ddlDRINStkRcvStkTransfer.DataTextField = "_Description"
            ddlDRINStkRcvStkTransfer.DataBind()
            ddlDRINStkRcvStkTransfer.SelectedIndex = intIndex

            ddlCRINStkRcvStkTransfer.DataSource = objDs
            ddlCRINStkRcvStkTransfer.DataValueField = "AccCode"
            ddlCRINStkRcvStkTransfer.DataTextField = "_Description"
            ddlCRINStkRcvStkTransfer.DataBind()
            ddlCRINStkRcvStkTransfer.SelectedIndex = intIndex

            ddlDRINStkRcvStkRtnAdvice.DataSource = objDs
            ddlDRINStkRcvStkRtnAdvice.DataValueField = "AccCode"
            ddlDRINStkRcvStkRtnAdvice.DataTextField = "_Description"
            ddlDRINStkRcvStkRtnAdvice.DataBind()
            ddlDRINStkRcvStkRtnAdvice.SelectedIndex = intIndex

            ddlCRINStkRcvStkRtnAdvice.DataSource = objDs
            ddlCRINStkRcvStkRtnAdvice.DataValueField = "AccCode"
            ddlCRINStkRcvStkRtnAdvice.DataTextField = "_Description"
            ddlCRINStkRcvStkRtnAdvice.DataBind()
            ddlCRINStkRcvStkRtnAdvice.SelectedIndex = intIndex

            ddlDRINStkRtnAdvice.DataSource = objDs
            ddlDRINStkRtnAdvice.DataValueField = "AccCode"
            ddlDRINStkRtnAdvice.DataTextField = "_Description"
            ddlDRINStkRtnAdvice.DataBind()
            ddlDRINStkRtnAdvice.SelectedIndex = intIndex

            ddlCRINStkRtnAdvice.DataSource = objDs
            ddlCRINStkRtnAdvice.DataValueField = "AccCode"
            ddlCRINStkRtnAdvice.DataTextField = "_Description"
            ddlCRINStkRtnAdvice.DataBind()
            ddlCRINStkRtnAdvice.SelectedIndex = intIndex

            ddlDRINStkIssueEmp.DataSource = objDs
            ddlDRINStkIssueEmp.DataValueField = "AccCode"
            ddlDRINStkIssueEmp.DataTextField = "_Description"
            ddlDRINStkIssueEmp.DataBind()
            ddlDRINStkIssueEmp.SelectedIndex = intIndex

            ddlCRINStkIssueEmp.DataSource = objDs
            ddlCRINStkIssueEmp.DataValueField = "AccCode"
            ddlCRINStkIssueEmp.DataTextField = "_Description"
            ddlCRINStkIssueEmp.DataBind()
            ddlCRINStkIssueEmp.SelectedIndex = intIndex

            ddlDRINFuelIssueEmp.DataSource = objDs
            ddlDRINFuelIssueEmp.DataValueField = "AccCode"
            ddlDRINFuelIssueEmp.DataTextField = "_Description"
            ddlDRINFuelIssueEmp.DataBind()
            ddlDRINFuelIssueEmp.SelectedIndex = intIndex

            ddlCRINFuelIssueEmp.DataSource = objDs
            ddlCRINFuelIssueEmp.DataValueField = "AccCode"
            ddlCRINFuelIssueEmp.DataTextField = "_Description"
            ddlCRINFuelIssueEmp.DataBind()
            ddlCRINFuelIssueEmp.SelectedIndex = intIndex

            ddlDRINBalanceFromStkRtnAdvice.DataSource = objDs
            ddlDRINBalanceFromStkRtnAdvice.DataValueField = "AccCode"
            ddlDRINBalanceFromStkRtnAdvice.DataTextField = "_Description"
            ddlDRINBalanceFromStkRtnAdvice.DataBind()
            ddlDRINBalanceFromStkRtnAdvice.SelectedIndex = intIndex
            
            ddlCRINBalanceFromStkRtnAdvice.DataSource = objDs
            ddlCRINBalanceFromStkRtnAdvice.DataValueField = "AccCode"
            ddlCRINBalanceFromStkRtnAdvice.DataTextField = "_Description"
            ddlCRINBalanceFromStkRtnAdvice.DataBind()
            ddlCRINBalanceFromStkRtnAdvice.SelectedIndex = intIndex

            ddlDRINStockAdj.DataSource = objDRBalFrmWSDs
            ddlDRINStockAdj.DataValueField = "AccCode"
            ddlDRINStockAdj.DataTextField = "_Description"
            ddlDRINStockAdj.DataBind()
            ddlDRINStockAdj.SelectedIndex = intDRBalFrmWSDsIndex

            ddlCRINStockAdj.DataSource = objCRBalFrmWSDs
            ddlCRINStockAdj.DataValueField = "AccCode"
            ddlCRINStockAdj.DataTextField = "_Description"
            ddlCRINStockAdj.DataBind()
            ddlCRINStockAdj.SelectedIndex = intCRBalFrmWSDsIndex

            ddlDRCTStkRcvDADirectPR.DataSource = objDs
            ddlDRCTStkRcvDADirectPR.DataValueField = "AccCode"
            ddlDRCTStkRcvDADirectPR.DataTextField = "_Description"
            ddlDRCTStkRcvDADirectPR.DataBind()
            ddlDRCTStkRcvDADirectPR.SelectedIndex = intIndex

            ddlCRCTStkRcvDADirectPR.DataSource = objDs
            ddlCRCTStkRcvDADirectPR.DataValueField = "AccCode"
            ddlCRCTStkRcvDADirectPR.DataTextField = "_Description"
            ddlCRCTStkRcvDADirectPR.DataBind()
            ddlCRCTStkRcvDADirectPR.SelectedIndex = intIndex

            ddlDRCTStkRcvDAStockPR.DataSource = objDs
            ddlDRCTStkRcvDAStockPR.DataValueField = "AccCode"
            ddlDRCTStkRcvDAStockPR.DataTextField = "_Description"
            ddlDRCTStkRcvDAStockPR.DataBind()
            ddlDRCTStkRcvDAStockPR.SelectedIndex = intIndex

            ddlCRCTStkRcvDAStockPR.DataSource = objDs
            ddlCRCTStkRcvDAStockPR.DataValueField = "AccCode"
            ddlCRCTStkRcvDAStockPR.DataTextField = "_Description"
            ddlCRCTStkRcvDAStockPR.DataBind()
            ddlCRCTStkRcvDAStockPR.SelectedIndex = intIndex

            ddlDRCTStkRcvStkTransfer.DataSource = objDs
            ddlDRCTStkRcvStkTransfer.DataValueField = "AccCode"
            ddlDRCTStkRcvStkTransfer.DataTextField = "_Description"
            ddlDRCTStkRcvStkTransfer.DataBind()
            ddlDRCTStkRcvStkTransfer.SelectedIndex = intIndex

            ddlCRCTStkRcvStkTransfer.DataSource = objDs
            ddlCRCTStkRcvStkTransfer.DataValueField = "AccCode"
            ddlCRCTStkRcvStkTransfer.DataTextField = "_Description"
            ddlCRCTStkRcvStkTransfer.DataBind()
            ddlCRCTStkRcvStkTransfer.SelectedIndex = intIndex

            ddlDRCTStkRcvStkRtnAdvice.DataSource = objDs
            ddlDRCTStkRcvStkRtnAdvice.DataValueField = "AccCode"
            ddlDRCTStkRcvStkRtnAdvice.DataTextField = "_Description"
            ddlDRCTStkRcvStkRtnAdvice.DataBind()
            ddlDRCTStkRcvStkRtnAdvice.SelectedIndex = intIndex

            ddlCRCTStkRcvStkRtnAdvice.DataSource = objDs
            ddlCRCTStkRcvStkRtnAdvice.DataValueField = "AccCode"
            ddlCRCTStkRcvStkRtnAdvice.DataTextField = "_Description"
            ddlCRCTStkRcvStkRtnAdvice.DataBind()
            ddlCRCTStkRcvStkRtnAdvice.SelectedIndex = intIndex

            ddlDRCTStkRtnAdvice.DataSource = objDs
            ddlDRCTStkRtnAdvice.DataValueField = "AccCode"
            ddlDRCTStkRtnAdvice.DataTextField = "_Description"
            ddlDRCTStkRtnAdvice.DataBind()
            ddlDRCTStkRtnAdvice.SelectedIndex = intIndex

            ddlCRCTStkRtnAdvice.DataSource = objDs
            ddlCRCTStkRtnAdvice.DataValueField = "AccCode"
            ddlCRCTStkRtnAdvice.DataTextField = "_Description"
            ddlCRCTStkRtnAdvice.DataBind()
            ddlCRCTStkRtnAdvice.SelectedIndex = intIndex

            ddlDRCTStkIssueEmp.DataSource = objDs
            ddlDRCTStkIssueEmp.DataValueField = "AccCode"
            ddlDRCTStkIssueEmp.DataTextField = "_Description"
            ddlDRCTStkIssueEmp.DataBind()
            ddlDRCTStkIssueEmp.SelectedIndex = intIndex

            ddlCRCTStkIssueEmp.DataSource = objDs
            ddlCRCTStkIssueEmp.DataValueField = "AccCode"
            ddlCRCTStkIssueEmp.DataTextField = "_Description"
            ddlCRCTStkIssueEmp.DataBind()
            ddlCRCTStkIssueEmp.SelectedIndex = intIndex

            ddlDRCTBalanceFromCTRtnAdvice.DataSource = objDs
            ddlDRCTBalanceFromCTRtnAdvice.DataValueField = "AccCode"
            ddlDRCTBalanceFromCTRtnAdvice.DataTextField = "_Description"
            ddlDRCTBalanceFromCTRtnAdvice.DataBind()
            ddlDRCTBalanceFromCTRtnAdvice.SelectedIndex = intIndex
            
            ddlCRCTBalanceFromCTRtnAdvice.DataSource = objDs
            ddlCRCTBalanceFromCTRtnAdvice.DataValueField = "AccCode"
            ddlCRCTBalanceFromCTRtnAdvice.DataTextField = "_Description"
            ddlCRCTBalanceFromCTRtnAdvice.DataBind()
            ddlCRCTBalanceFromCTRtnAdvice.SelectedIndex = intIndex

            ddlDRWSJobEmp.DataSource = objDs
            ddlDRWSJobEmp.DataValueField = "AccCode"
            ddlDRWSJobEmp.DataTextField = "_Description"
            ddlDRWSJobEmp.DataBind()
            ddlDRWSJobEmp.SelectedIndex = intIndex

            ddlCRWSJobEmp.DataSource = objDs
            ddlCRWSJobEmp.DataValueField = "AccCode"
            ddlCRWSJobEmp.DataTextField = "_Description"
            ddlCRWSJobEmp.DataBind()
            ddlCRWSJobEmp.SelectedIndex = intIndex
            ddlDRNUSeedlingsIssue.DataSource = objNUDs
            ddlDRNUSeedlingsIssue.DataValueField = "AccCode"
            ddlDRNUSeedlingsIssue.DataTextField = "_Description"
            ddlDRNUSeedlingsIssue.DataBind()
            ddlDRNUSeedlingsIssue.SelectedIndex = intNUIndex

            ddlCRNUSeedlingsIssue.DataSource = objNUDs
            ddlCRNUSeedlingsIssue.DataValueField = "AccCode"
            ddlCRNUSeedlingsIssue.DataTextField = "_Description"
            ddlCRNUSeedlingsIssue.DataBind()
            ddlCRNUSeedlingsIssue.SelectedIndex = intNUIndex

            ddlDRPUGoodsRcv.DataSource = objDs
            ddlDRPUGoodsRcv.DataValueField = "AccCode"
            ddlDRPUGoodsRcv.DataTextField = "_Description"
            ddlDRPUGoodsRcv.DataBind()
            ddlDRPUGoodsRcv.SelectedIndex = intIndex

            ddlCRPUGoodsRcv.DataSource = objDs
            ddlCRPUGoodsRcv.DataValueField = "AccCode"
            ddlCRPUGoodsRcv.DataTextField = "_Description"
            ddlCRPUGoodsRcv.DataBind()
            ddlCRPUGoodsRcv.SelectedIndex = intIndex

            ddlDRPUDispAdv.DataSource = objDs
            ddlDRPUDispAdv.DataValueField = "AccCode"
            ddlDRPUDispAdv.DataTextField = "_Description"
            ddlDRPUDispAdv.DataBind()
            ddlDRPUDispAdv.SelectedIndex = intIndex

            ddlCRPUDispAdv.DataSource = objDs
            ddlCRPUDispAdv.DataValueField = "AccCode"
            ddlCRPUDispAdv.DataTextField = "_Description"
            ddlCRPUDispAdv.DataBind()
            ddlCRPUDispAdv.SelectedIndex = intIndex

            ddlDRAPInvRcv.DataSource = objDs
            ddlDRAPInvRcv.DataValueField = "AccCode"
            ddlDRAPInvRcv.DataTextField = "_Description"
            ddlDRAPInvRcv.DataBind()
            ddlDRAPInvRcv.SelectedIndex = intIndex

            ddlCRAPInvRcv.DataSource = objDs
            ddlCRAPInvRcv.DataValueField = "AccCode"
            ddlCRAPInvRcv.DataTextField = "_Description"
            ddlCRAPInvRcv.DataBind()
            ddlCRAPInvRcv.SelectedIndex = intIndex

            ddlDRAPPPNInvRcv.DataSource = objDs
            ddlDRAPPPNInvRcv.DataValueField = "AccCode"
            ddlDRAPPPNInvRcv.DataTextField = "_Description"
            ddlDRAPPPNInvRcv.DataBind()
            ddlDRAPPPNInvRcv.SelectedIndex = intIndex

            ddlCRAPPPNInvRcv.DataSource = objDs
            ddlCRAPPPNInvRcv.DataValueField = "AccCode"
            ddlCRAPPPNInvRcv.DataTextField = "_Description"
            ddlCRAPPPNInvRcv.DataBind()
            ddlCRAPPPNInvRcv.SelectedIndex = intIndex


            ddlDRAPPPNInvRcv2.DataSource = objDs
            ddlDRAPPPNInvRcv2.DataValueField = "AccCode"
            ddlDRAPPPNInvRcv2.DataTextField = "_Description"
            ddlDRAPPPNInvRcv2.DataBind()
            ddlDRAPPPNInvRcv2.SelectedIndex = intIndex

            ddlCRAPPPNInvRcv2.DataSource = objDs
            ddlCRAPPPNInvRcv2.DataValueField = "AccCode"
            ddlCRAPPPNInvRcv2.DataTextField = "_Description"
            ddlCRAPPPNInvRcv2.DataBind()
            ddlCRAPPPNInvRcv2.SelectedIndex = intIndex


            ddlDRAPPPNInvRcv4.DataSource = objDs
            ddlDRAPPPNInvRcv4.DataValueField = "AccCode"
            ddlDRAPPPNInvRcv4.DataTextField = "_Description"
            ddlDRAPPPNInvRcv4.DataBind()
            ddlDRAPPPNInvRcv4.SelectedIndex = intIndex

            ddlCRAPPPNInvRcv4.DataSource = objDs
            ddlCRAPPPNInvRcv4.DataValueField = "AccCode"
            ddlCRAPPPNInvRcv4.DataTextField = "_Description"
            ddlCRAPPPNInvRcv4.DataBind()
            ddlCRAPPPNInvRcv4.SelectedIndex = intIndex


            ddlDRAPPPHInvRcv.DataSource = objDs
            ddlDRAPPPHInvRcv.DataValueField = "AccCode"
            ddlDRAPPPHInvRcv.DataTextField = "_Description"
            ddlDRAPPPHInvRcv.DataBind()
            ddlDRAPPPHInvRcv.SelectedIndex = intIndex

            ddlCRAPPPHInvRcv.DataSource = objDs
            ddlCRAPPPHInvRcv.DataValueField = "AccCode"
            ddlCRAPPPHInvRcv.DataTextField = "_Description"
            ddlCRAPPPHInvRcv.DataBind()
            ddlCRAPPPHInvRcv.SelectedIndex = intIndex

            
            ddlDRCBPPHInvPay.DataSource = objDs
            ddlDRCBPPHInvPay.DataValueField = "AccCode"
            ddlDRCBPPHInvPay.DataTextField = "_Description"
            ddlDRCBPPHInvPay.DataBind()
            ddlDRCBPPHInvPay.SelectedIndex = intIndex

            ddlCRCBPPHInvPay.DataSource = objDs
            ddlCRCBPPHInvPay.DataValueField = "AccCode"
            ddlCRCBPPHInvPay.DataTextField = "_Description"
            ddlCRCBPPHInvPay.DataBind()
            ddlCRCBPPHInvPay.SelectedIndex = intIndex


            
            ddlDRAPPPNCrdJrn.DataSource = objDs
            ddlDRAPPPNCrdJrn.DataValueField = "AccCode"
            ddlDRAPPPNCrdJrn.DataTextField = "_Description"
            ddlDRAPPPNCrdJrn.DataBind()
            ddlDRAPPPNCrdJrn.SelectedIndex = intIndex

            ddlCRAPPPNCrdJrn.DataSource = objDs
            ddlCRAPPPNCrdJrn.DataValueField = "AccCode"
            ddlCRAPPPNCrdJrn.DataTextField = "_Description"
            ddlCRAPPPNCrdJrn.DataBind()
            ddlCRAPPPNCrdJrn.SelectedIndex = intIndex

            ddlDRAPPPHCrdJrn.DataSource = objDs
            ddlDRAPPPHCrdJrn.DataValueField = "AccCode"
            ddlDRAPPPHCrdJrn.DataTextField = "_Description"
            ddlDRAPPPHCrdJrn.DataBind()
            ddlDRAPPPHCrdJrn.SelectedIndex = intIndex

            ddlCRAPPPHCrdJrn.DataSource = objDs
            ddlCRAPPPHCrdJrn.DataValueField = "AccCode"
            ddlCRAPPPHCrdJrn.DataTextField = "_Description"
            ddlCRAPPPHCrdJrn.DataBind()
            ddlCRAPPPHCrdJrn.SelectedIndex = intIndex

            ddlDRAdvPayment.DataSource = objDs
            ddlDRAdvPayment.DataValueField = "AccCode"
            ddlDRAdvPayment.DataTextField = "_Description"
            ddlDRAdvPayment.DataBind()
            ddlDRAdvPayment.SelectedIndex = intIndex

            ddlCRAdvPayment.DataSource = objDs
            ddlCRAdvPayment.DataValueField = "AccCode"
            ddlCRAdvPayment.DataTextField = "_Description"
            ddlCRAdvPayment.DataBind()
            ddlCRAdvPayment.SelectedIndex = intIndex

            ddlDRBIPPNInvRcv.DataSource = objDs
            ddlDRBIPPNInvRcv.DataValueField = "AccCode"
            ddlDRBIPPNInvRcv.DataTextField = "_Description"
            ddlDRBIPPNInvRcv.DataBind()
            ddlDRBIPPNInvRcv.SelectedIndex = intIndex

            ddlCRBIPPNInvRcv.DataSource = objDs
            ddlCRBIPPNInvRcv.DataValueField = "AccCode"
            ddlCRBIPPNInvRcv.DataTextField = "_Description"
            ddlCRBIPPNInvRcv.DataBind()
            ddlCRBIPPNInvRcv.SelectedIndex = intIndex

            ddlDRBIPPHInvRcv.DataSource = objDs
            ddlDRBIPPHInvRcv.DataValueField = "AccCode"
            ddlDRBIPPHInvRcv.DataTextField = "_Description"
            ddlDRBIPPHInvRcv.DataBind()
            ddlDRBIPPHInvRcv.SelectedIndex = intIndex

            ddlCRBIPPHInvRcv.DataSource = objDs
            ddlCRBIPPHInvRcv.DataValueField = "AccCode"
            ddlCRBIPPHInvRcv.DataTextField = "_Description"
            ddlCRBIPPHInvRcv.DataBind()
            ddlCRBIPPHInvRcv.SelectedIndex = intIndex





            ddlDRCBPPNRcpt.DataSource = objDs
            ddlDRCBPPNRcpt.DataValueField = "AccCode"
            ddlDRCBPPNRcpt.DataTextField = "_Description"
            ddlDRCBPPNRcpt.DataBind()
            ddlDRCBPPNRcpt.SelectedIndex = intIndex

            ddlCRCBPPNRcpt.DataSource = objDs
            ddlCRCBPPNRcpt.DataValueField = "AccCode"
            ddlCRCBPPNRcpt.DataTextField = "_Description"
            ddlCRCBPPNRcpt.DataBind()
            ddlCRCBPPNRcpt.SelectedIndex = intIndex

            ddlDRCBPPHRcpt.DataSource = objDs
            ddlDRCBPPHRcpt.DataValueField = "AccCode"
            ddlDRCBPPHRcpt.DataTextField = "_Description"
            ddlDRCBPPHRcpt.DataBind()
            ddlDRCBPPHRcpt.SelectedIndex = intIndex

            ddlCRCBPPHRcpt.DataSource = objDs
            ddlCRCBPPHRcpt.DataValueField = "AccCode"
            ddlCRCBPPHRcpt.DataTextField = "_Description"
            ddlCRCBPPHRcpt.DataBind()
            ddlCRCBPPHRcpt.SelectedIndex = intIndex

            ddlDRPRClr.DataSource = objDs
            ddlDRPRClr.DataValueField = "AccCode"
            ddlDRPRClr.DataTextField = "_Description"
            ddlDRPRClr.DataBind()
            ddlDRPRClr.SelectedIndex = intIndex

            ddlCRPRClr.DataSource = objDs
            ddlCRPRClr.DataValueField = "AccCode"
            ddlCRPRClr.DataTextField = "_Description"
            ddlCRPRClr.DataBind()
            ddlCRPRClr.SelectedIndex = intIndex

            ddlDRPRHK.DataSource = objDs
            ddlDRPRHK.DataValueField = "AccCode"
            ddlDRPRHK.DataTextField = "_Description"
            ddlDRPRHK.DataBind()
            ddlDRPRHK.SelectedIndex = intIndex

            ddlCRPRHK.DataSource = objDs
            ddlCRPRHK.DataValueField = "AccCode"
            ddlCRPRHK.DataTextField = "_Description"
            ddlCRPRHK.DataBind()
            ddlCRPRHK.SelectedIndex = intIndex

            ddlDREstYield.DataSource = objDs
            ddlDREstYield.DataValueField = "AccCode"
            ddlDREstYield.DataTextField = "_Description"
            ddlDREstYield.DataBind()
            ddlDREstYield.SelectedIndex = intIndex

            ddlCREstYield.DataSource = objDs
            ddlCREstYield.DataValueField = "AccCode"
            ddlCREstYield.DataTextField = "_Description"
            ddlCREstYield.DataBind()
            ddlCREstYield.SelectedIndex = intIndex


            ddlDRPDHPPCost.DataSource = objDRBalFrmWSDs
            ddlDRPDHPPCost.DataValueField = "AccCode"
            ddlDRPDHPPCost.DataTextField = "_Description"
            ddlDRPDHPPCost.DataBind()
            ddlDRPDHPPCost.SelectedIndex = intDRBalFrmWSDsIndex
        
            ddlCRPDHPPCost.DataSource = objCRBalFrmWSDs
            ddlCRPDHPPCost.DataValueField = "AccCode"
            ddlCRPDHPPCost.DataTextField = "_Description"
            ddlCRPDHPPCost.DataBind()
            ddlCRPDHPPCost.SelectedIndex = intCRBalFrmWSDsIndex

            ddlDRPDTBSKebun.DataSource = objDRBalFrmWSDs
            ddlDRPDTBSKebun.DataValueField = "AccCode"
            ddlDRPDTBSKebun.DataTextField = "_Description"
            ddlDRPDTBSKebun.DataBind()
            ddlDRPDTBSKebun.SelectedIndex = intDRBalFrmWSDsIndex
        
            ddlCRPDTBSKebun.DataSource = objCRBalFrmWSDs
            ddlCRPDTBSKebun.DataValueField = "AccCode"
            ddlCRPDTBSKebun.DataTextField = "_Description"
            ddlCRPDTBSKebun.DataBind()
            ddlCRPDTBSKebun.SelectedIndex = intCRBalFrmWSDsIndex


            ddlDRPDStockCPO.DataSource = objDs
            ddlDRPDStockCPO.DataValueField = "AccCode"
            ddlDRPDStockCPO.DataTextField = "_Description"
            ddlDRPDStockCPO.DataBind()
            ddlDRPDStockCPO.SelectedIndex = intIndex

            ddlCRPDStockCPO.DataSource = objDs
            ddlCRPDStockCPO.DataValueField = "AccCode"
            ddlCRPDStockCPO.DataTextField = "_Description"
            ddlCRPDStockCPO.DataBind()
            ddlCRPDStockCPO.SelectedIndex = intIndex

            ddlDRPDStockPK.DataSource = objDs
            ddlDRPDStockPK.DataValueField = "AccCode"
            ddlDRPDStockPK.DataTextField = "_Description"
            ddlDRPDStockPK.DataBind()
            ddlDRPDStockPK.SelectedIndex = intIndex

            ddlCRPDStockPK.DataSource = objDs
            ddlCRPDStockPK.DataValueField = "AccCode"
            ddlCRPDStockPK.DataTextField = "_Description"
            ddlCRPDStockPK.DataBind()
            ddlCRPDStockPK.SelectedIndex = intIndex


            ddlDRIntIncome.DataSource = objDRBalFrmWSDs
            ddlDRIntIncome.DataValueField = "AccCode"
            ddlDRIntIncome.DataTextField = "_Description"
            ddlDRIntIncome.DataBind()
            ddlDRIntIncome.SelectedIndex = intDRBalFrmWSDsIndex

            ddlCRIntIncome.DataSource = objDRBalFrmWSDs
            ddlCRIntIncome.DataValueField = "AccCode"
            ddlCRIntIncome.DataTextField = "_Description"
            ddlCRIntIncome.DataBind()
            ddlCRIntIncome.SelectedIndex = intDRBalFrmWSDsIndex

            ddlDRIntIncome2.DataSource = objDRBalFrmWSDs
            ddlDRIntIncome2.DataValueField = "AccCode"
            ddlDRIntIncome2.DataTextField = "_Description"
            ddlDRIntIncome2.DataBind()
            ddlDRIntIncome2.SelectedIndex = intDRBalFrmWSDsIndex

            ddlCRIntIncome2.DataSource = objDRBalFrmWSDs
            ddlCRIntIncome2.DataValueField = "AccCode"
            ddlCRIntIncome2.DataTextField = "_Description"
            ddlCRIntIncome2.DataBind()
            ddlCRIntIncome2.SelectedIndex = intDRBalFrmWSDsIndex

            ddlDRAPPPNInvRcv3.DataSource = objDRBalFrmWSDs
            ddlDRAPPPNInvRcv3.DataValueField = "AccCode"
            ddlDRAPPPNInvRcv3.DataTextField = "_Description"
            ddlDRAPPPNInvRcv3.DataBind()
            ddlDRAPPPNInvRcv3.SelectedIndex = intDRBalFrmWSDsIndex

            ddlCRAPPPNInvRcv3.DataSource = objDRBalFrmWSDs
            ddlCRAPPPNInvRcv3.DataValueField = "AccCode"
            ddlCRAPPPNInvRcv3.DataTextField = "_Description"
            ddlCRAPPPNInvRcv3.DataBind()
            ddlCRAPPPNInvRcv3.SelectedIndex = intDRBalFrmWSDsIndex


            ddlDRSunIncome.DataSource = objDRBalFrmWSDs
            ddlDRSunIncome.DataValueField = "AccCode"
            ddlDRSunIncome.DataTextField = "_Description"
            ddlDRSunIncome.DataBind()
            ddlDRSunIncome.SelectedIndex = intDRBalFrmWSDsIndex

            ddlCRSunIncome.DataSource = objDRBalFrmWSDs
            ddlCRSunIncome.DataValueField = "AccCode"
            ddlCRSunIncome.DataTextField = "_Description"
            ddlCRSunIncome.DataBind()
            ddlCRSunIncome.SelectedIndex = intDRBalFrmWSDsIndex

            ddlDRVehSuspende.DataSource = objDs
            ddlDRVehSuspende.DataValueField = "AccCode"
            ddlDRVehSuspende.DataTextField = "_Description"
            ddlDRVehSuspende.DataBind()
            ddlDRVehSuspende.SelectedIndex = intIndex

            ddlCRVehSuspende.DataSource = objDs
            ddlCRVehSuspende.DataValueField = "AccCode"
            ddlCRVehSuspende.DataTextField = "_Description"
            ddlCRVehSuspende.DataBind()
            ddlCRVehSuspende.SelectedIndex = intIndex

            ddlDRRetainEarn.DataSource = objDs
            ddlDRRetainEarn.DataValueField = "AccCode"
            ddlDRRetainEarn.DataTextField = "_Description"
            ddlDRRetainEarn.DataBind()
            ddlDRRetainEarn.SelectedIndex = intIndex

            ddlCRRetainEarn.DataSource = objDs
            ddlCRRetainEarn.DataValueField = "AccCode"
            ddlCRRetainEarn.DataTextField = "_Description"
            ddlCRRetainEarn.DataBind()
            ddlCRRetainEarn.SelectedIndex = intIndex

            ddlDRBalFrmWSAccCode.DataSource = objDRBalFrmWSDs
            ddlDRBalFrmWSAccCode.DataValueField = "AccCode"
            ddlDRBalFrmWSAccCode.DataTextField = "_Description"
            ddlDRBalFrmWSAccCode.DataBind()
            ddlDRBalFrmWSAccCode.SelectedIndex = intDRBalFrmWSDsIndex

            ddlCRBalFrmWSAccCode.DataSource = objCRBalFrmWSDs
            ddlCRBalFrmWSAccCode.DataValueField = "AccCode"
            ddlCRBalFrmWSAccCode.DataTextField = "_Description"
            ddlCRBalFrmWSAccCode.DataBind()
            ddlCRBalFrmWSAccCode.SelectedIndex = intCRBalFrmWSDsIndex

            ddlDRWSCtrlAcc.DataSource = objDs
            ddlDRWSCtrlAcc.DataValueField = "AccCode"
            ddlDRWSCtrlAcc.DataTextField = "_Description"
            ddlDRWSCtrlAcc.DataBind()
            ddlDRWSCtrlAcc.SelectedIndex = intIndex

            ddlCRWSCtrlAcc.DataSource = objDs
            ddlCRWSCtrlAcc.DataValueField = "AccCode"
            ddlCRWSCtrlAcc.DataTextField = "_Description"
            ddlCRWSCtrlAcc.DataBind()
            ddlCRWSCtrlAcc.SelectedIndex = intIndex
            ddlDRPEBMovementAccCode.DataSource = objDs
            ddlDRPEBMovementAccCode.DataValueField = "AccCode"
            ddlDRPEBMovementAccCode.DataTextField = "_Description"
            ddlDRPEBMovementAccCode.DataBind()
            ddlDRPEBMovementAccCode.SelectedIndex = intIndex

            ddlCRPEBMovementAccCode.DataSource = objDs
            ddlCRPEBMovementAccCode.DataValueField = "AccCode"
            ddlCRPEBMovementAccCode.DataTextField = "_Description"
            ddlCRPEBMovementAccCode.DataBind()
            ddlCRPEBMovementAccCode.SelectedIndex = intIndex



        End If
    End Sub
    
    Function BindAccount(ByVal pv_strAccCode As String, ByRef pv_intIndex As Integer) As Dataset
        Dim objAccDs As New Object()
        Dim strOpCode As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "'" & _
                                " And ACC.AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "'" & _
                                " And ACC.NurseryInd = '" & objGLSetup.EnumNurseryAccount.No & "'"
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        If UCase(TypeName(objGlobalAccDs)) = "DATASET" Then
            pv_intIndex = intSelectIndex
            BindAccount = objGlobalAccDs
        Else
            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                        strParam, _
                                                        objGLSetup.EnumGLMasterType.AccountCode, _
                                                        objGlobalAccDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_ACCOUNT_GET&errmesg=" & Exp.ToString & "&redirect=")
            End Try
            

            dr = objGlobalAccDs.Tables(0).NewRow()
            dr("AccCode") = ""
            dr("_Description") = "Select Account Code"
            objGlobalAccDs.Tables(0).Rows.InsertAt(dr, 0)
            pv_intIndex = intSelectIndex
            BindAccount = objGlobalAccDs
        End If
    End Function

    Sub BindAccount_APTBS()
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndexCOATBSPemilik As Integer = 0
        Dim intSelectedIndexCOATBSAgen As Integer = 0
        Dim intSelectedIndexCOAPPN As Integer = 0
        Dim intSelectedIndexCOAOB As Integer = 0
        Dim intSelectedIndexCOAOL As Integer = 0
        Dim intSelectedIndexCOAOBBiaya As Integer = 0
        Dim intSelectedIndexCOAOLBiaya As Integer = 0
        Dim objAccDs As New Object

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
           intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                  strParam, _
                                                  objGLSetup.EnumGLMasterType.AccountCode, _
                                                  objAccDs)
        Catch Exp As System.Exception
           Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
 
        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
       ' dr("_Description") = "Please select account code"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        radTbsPemilik.DataSource = objAccDs.Tables(0)
        radTbsPemilik.DataValueField = "AccCode"
        radTbsPemilik.DataTextField = "_Description"
        radTbsPemilik.DataBind()
        

        radTbsAgen.DataSource = objAccDs.Tables(0)
        radTbsAgen.DataValueField = "AccCode"
        radTbsAgen.DataTextField = "_Description"
        radTbsAgen.DataBind()
        

        radTbsPPN.DataSource = objAccDs.Tables(0)
        radTbsPPN.DataValueField = "AccCode"
        radTbsPPN.DataTextField = "_Description"
        radTbsPPN.DataBind()
        

        radTbsPPH.DataSource = objAccDs.Tables(0)
        radTbsPPH.DataValueField = "AccCode"
        radTbsPPH.DataTextField = "_Description"
        radTbsPPH.DataBind()
        

        radTBSOBongkar.DataSource = objAccDs.Tables(0)
        radTBSOBongkar.DataValueField = "AccCode"
        radTBSOBongkar.DataTextField = "_Description"
        radTBSOBongkar.DataBind()
        

        radTbsOLapangan.DataSource = objAccDs.Tables(0)
        radTbsOLapangan.DataValueField = "AccCode"
        radTbsOLapangan.DataTextField = "_Description"
        radTbsOLapangan.DataBind()
        
    End Sub

    Sub BindAccount_Sales()
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow

        Dim objAccDs As New Object

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
      

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Please select account code"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSalesCPO.DataSource = objAccDs.Tables(0)
        ddlSalesCPO.DataValueField = "AccCode"
        ddlSalesCPO.DataTextField = "_Description"
        ddlSalesCPO.DataBind()

        ddlSalesKNL.DataSource = objAccDs.Tables(0)
        ddlSalesKNL.DataValueField = "AccCode"
        ddlSalesKNL.DataTextField = "_Description"
        ddlSalesKNL.DataBind()

        ddlSalesOTH.DataSource = objAccDs.Tables(0)
        ddlSalesOTH.DataValueField = "AccCode"
        ddlSalesOTH.DataTextField = "_Description"
        ddlSalesOTH.DataBind()

        ddlSalesEFB.DataSource = objAccDs.Tables(0)
        ddlSalesEFB.DataValueField = "AccCode"
        ddlSalesEFB.DataTextField = "_Description"
        ddlSalesEFB.DataBind()

        ddlSalesCKG.DataSource = objAccDs.Tables(0)
        ddlSalesCKG.DataValueField = "AccCode"
        ddlSalesCKG.DataTextField = "_Description"
        ddlSalesCKG.DataBind()

        ddlSalesABJ.DataSource = objAccDs.Tables(0)
        ddlSalesABJ.DataValueField = "AccCode"
        ddlSalesABJ.DataTextField = "_Description"
        ddlSalesABJ.DataBind()

        ddlSalesFBR.DataSource = objAccDs.Tables(0)
        ddlSalesFBR.DataValueField = "AccCode"
        ddlSalesFBR.DataTextField = "_Description"
        ddlSalesFBR.DataBind()

        ddlSalesSLD.DataSource = objAccDs.Tables(0)
        ddlSalesSLD.DataValueField = "AccCode"
        ddlSalesSLD.DataTextField = "_Description"
        ddlSalesSLD.DataBind()

        ddlSalesLMB.DataSource = objAccDs.Tables(0)
        ddlSalesLMB.DataValueField = "AccCode"
        ddlSalesLMB.DataTextField = "_Description"
        ddlSalesLMB.DataBind()

        ddlSalesPPN.DataSource = objAccDs.Tables(0)
        ddlSalesPPN.DataValueField = "AccCode"
        ddlSalesPPN.DataTextField = "_Description"
        ddlSalesPPN.DataBind()

        ddlSalesTrxCPO.DataSource = objAccDs.Tables(0)
        ddlSalesTrxCPO.DataValueField = "AccCode"
        ddlSalesTrxCPO.DataTextField = "_Description"
        ddlSalesTrxCPO.DataBind()

        ddlSalesTrxPK.DataSource = objAccDs.Tables(0)
        ddlSalesTrxPK.DataValueField = "AccCode"
        ddlSalesTrxPK.DataTextField = "_Description"
        ddlSalesTrxPK.DataBind()

        ddlSalesTrxOTH.DataSource = objAccDs.Tables(0)
        ddlSalesTrxOTH.DataValueField = "AccCode"
        ddlSalesTrxOTH.DataTextField = "_Description"
        ddlSalesTrxOTH.DataBind()

        ddlSalesTrxEFB.DataSource = objAccDs.Tables(0)
        ddlSalesTrxEFB.DataValueField = "AccCode"
        ddlSalesTrxEFB.DataTextField = "_Description"
        ddlSalesTrxEFB.DataBind()

        ddlSalesTrxCKG.DataSource = objAccDs.Tables(0)
        ddlSalesTrxCKG.DataValueField = "AccCode"
        ddlSalesTrxCKG.DataTextField = "_Description"
        ddlSalesTrxCKG.DataBind()

        ddlSalesTrxABJ.DataSource = objAccDs.Tables(0)
        ddlSalesTrxABJ.DataValueField = "AccCode"
        ddlSalesTrxABJ.DataTextField = "_Description"
        ddlSalesTrxABJ.DataBind()

        ddlSalesTrxFBR.DataSource = objAccDs.Tables(0)
        ddlSalesTrxFBR.DataValueField = "AccCode"
        ddlSalesTrxFBR.DataTextField = "_Description"
        ddlSalesTrxFBR.DataBind()

        ddlSalesTrxSLD.DataSource = objAccDs.Tables(0)
        ddlSalesTrxSLD.DataValueField = "AccCode"
        ddlSalesTrxSLD.DataTextField = "_Description"
        ddlSalesTrxSLD.DataBind()

        ddlSalesTrxLMB.DataSource = objAccDs.Tables(0)
        ddlSalesTrxLMB.DataValueField = "AccCode"
        ddlSalesTrxLMB.DataTextField = "_Description"
        ddlSalesTrxLMB.DataBind()

    End Sub

    Function BindNurseryAccount(ByVal pv_strAccCode As String, ByRef pv_intIndex As Integer) As Dataset
        Dim objAccDs As New Object()
        Dim strOpCode As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "'" & _
                                " And ACC.AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "'" 
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        If UCase(TypeName(objGlobalNurseryAccDs)) = "DATASET" Then
            pv_intIndex = intSelectIndex
            BindNurseryAccount = objGlobalNurseryAccDs
        Else
            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                        strParam, _
                                                        objGLSetup.EnumGLMasterType.AccountCode, _
                                                        objGlobalNurseryAccDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_ACCOUNT_GET&errmesg=" & Exp.ToString & "&redirect=")
            End Try
            

            dr = objGlobalNurseryAccDs.Tables(0).NewRow()
            dr("AccCode") = ""
            dr("_Description") = "Select Account Code"
            objGlobalNurseryAccDs.Tables(0).Rows.InsertAt(dr, 0)
            pv_intIndex = intSelectIndex
            BindNurseryAccount = objGlobalNurseryAccDs
        End If
    End Function

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_ACCOUNT_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_ACCOUNT_GET&errmesg=" & Exp.ToString & "&redirect=ws/setup/ws_workcodedet.aspx")
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

    Function BindAllAccount(ByVal pv_strAccCode As String, ByRef pv_intIndex As Integer) As Dataset
        Dim objAccDs As New Object()
        Dim strOpCode As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "'"
                                
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0       

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        If UCase(TypeName(objGlobalAllAccDs)) = "DATASET" Then
            pv_intIndex = intSelectIndex
            BindAllAccount = objGlobalAllAccDs            
        Else            
            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                        strParam, _
                                                        objGLSetup.EnumGLMasterType.AccountCode, _
                                                        objGlobalAllAccDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_ACCOUNT_GET&errmesg=" & Exp.ToString & "&redirect=")
            End Try
            

            dr = objGlobalAllAccDs.Tables(0).NewRow()
            dr("AccCode") = ""
            dr("_Description") = "Select Account Code"
            objGlobalAllAccDs.Tables(0).Rows.InsertAt(dr, 0)
            pv_intIndex = intSelectIndex
            BindAllAccount = objGlobalAllAccDs
        End If
    End Function

    Sub ddlDRBalFrmWSAccCode_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strAccCode As String = Request.Form("ddlDRBalFrmWSAccCode")
        Dim strBlkCode As String = Request.Form("ddlDRBalFrmWSBlkCode")

        BindBlockDropList(strAccCode, ddlDRBalFrmWSBlkCode, strBlkCode)

    End Sub

    Sub ddlCRBalFrmWSAccCode_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strAccCode As String = Request.Form("ddlCRBalFrmWSAccCode")
        Dim strBlkCode As String = Request.Form("ddlCRBalFrmWSBlkCode")

        BindBlockDropList(strAccCode, ddlCRBalFrmWSBlkCode, strBlkCode)
    End Sub

    Sub ddlCRIntIncome2_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strAccCode As String = Request.Form("ddlCRIntIncome2")
        Dim strBlkCode As String = Request.Form("ddlCRIntIncomeBlkCode2")
        BindBlockDropList(strAccCode, ddlCRIntIncomeBlkCode2, strBlkCode)
    End Sub

    Sub ddlDRAPPPNInvRcv3_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strAccCode As String = Request.Form("ddlDRAPPPNInvRcv3")
        Dim strBlkCode As String = Request.Form("ddlDRAPPPNInvRcv3Blk")
        BindBlockDropList(strAccCode, ddlDRAPPPNInvRcv3Blk, strBlkCode)
    End Sub

     Sub ddlCRIntIncome_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strAccCode As String = Request.Form("ddlCRIntIncome")
        Dim strBlkCode As String = Request.Form("ddlCRIntIncomeBlkCode")
        BindBlockDropList(strAccCode, ddlCRIntIncomeBlkCode, strBlkCode)
    End Sub

    Sub ddlCRSunIncome_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strAccCode As String = Request.Form("ddlCRSunIncome")
        Dim strBlkCode As String = Request.Form("ddlCRSunIncomeBlkCode")
        BindBlockDropList(strAccCode, ddlCRSunIncomeBlkCode, strBlkCode)
    End Sub

    Sub ddlCRINStockAdj_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strAccCode As String = Request.Form("ddlCRINStockAdj")
        Dim strBlkCode As String = Request.Form("ddlCRINStockAdjBlkCode")
        BindBlockDropList(strAccCode, ddlCRINStockAdjBlkCode, strBlkCode)
    End Sub

    Sub ddlDRPDHPPCost_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strAccCode As String = Request.Form("ddlDRPDHPPCost")
        Dim strBlkCode As String = Request.Form("ddlDRPDHPPBlkCode")
        BindBlockDropList(strAccCode, ddlDRPDHPPBlkCode, strBlkCode)
    End Sub

    Sub ddlCRPDHPPCost_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strAccCode As String = Request.Form("ddlCRPDHPPCost")
        Dim strBlkCode As String = Request.Form("ddlCRPDHPPBlkCode")
        BindBlockDropList(strAccCode, ddlCRPDHPPBlkCode, strBlkCode)
    End Sub

    Sub ddlDRPDTBSKebun_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strAccCode As String = Request.Form("ddlDRPDTBSKebun")
        Dim strBlkCode As String = Request.Form("ddlDRPDTBSKebunBlkCode")
        BindBlockDropList(strAccCode, ddlDRPDTBSKebunBlkCode, strBlkCode)
    End Sub

    Sub ddlCRPDTBSKebun_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strAccCode As String = Request.Form("ddlCRPDTBSKebun")
        Dim strBlkCode As String = Request.Form("ddlCRPDTBSKebunBlkCode")
        BindBlockDropList(strAccCode, ddlCRPDTBSKebunBlkCode, strBlkCode)
    End Sub

    Sub BindBlockDropList(ByVal pv_strAccCode As String, byRef pv_ddlBlock As Object, Optional ByVal pv_strBlkCode As String = "")
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim strParam As String
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim stDisplay As String = lblSelect.Text 
                                
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & strLocation & "|" & objGLSetup.EnumBlockStatus.Active                
                stDisplay = stDisplay & strBlkTag & lblCode.Text                
            Else                
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & strLocation & "|" & objGLSetup.EnumBlockStatus.Active                
                stDisplay = stDisplay & strBlkTag & lblCode.Text
            End If

            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCdBlockList_Get, _
                                                     strParam, _
                                                     dsForDropDown)
        Catch Exp As System.Exception            
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If dsForDropDown.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = stDisplay

        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)
        pv_ddlBlock.DataSource = dsForDropDown.Tables(0)
        pv_ddlBlock.DataValueField = "BlkCode"
        pv_ddlBlock.DataTextField = "Description"
        pv_ddlBlock.DataBind()
        pv_ddlBlock.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer, _
                          ByRef pr_strNurseryInd As Integer)

        Dim _objAccDs As New DataSet()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
        End If
    End Sub

    Function CheckRequireField()  As Boolean
        Dim stAccCode As String = ""
        Dim iAccType As Integer
        Dim iAccPurpose As Integer
        Dim iNurseryInd As Integer
        Dim bRet As Boolean = True
                
        lblErrDRBalFrmWSBlkCode.Visible = False
        lblErrCRBalFrmWSBlkCode.Visible = False

        

        If ddlDRBalFrmWSAccCode.SelectedIndex > 0 Then
            stAccCode = Request.Form("ddlDRBalFrmWSAccCode")
            GetAccountDetails(stAccCode, iAccType, iAccPurpose, iNurseryInd)

            If iAccType = objGLSetup.EnumAccountType.BalanceSheet Then  
                
                Select Case iNurseryInd
                    Case objGLSetup.EnumNurseryAccount.Yes
                        If ddlDRBalFrmWSBlkCode.SelectedIndex <= 0 Then
                            lblErrDRBalFrmWSBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrDRBalFrmWSBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
                
            Else If iAccType = objGLSetup.EnumAccountType.ProfitAndLost
                Select Case iAccPurpose
                    Case objGLSetup.EnumAccountPurpose.NonVehicle 
                        If ddlDRBalFrmWSBlkCode.SelectedIndex <= 0 Then
                            lblErrDRBalFrmWSBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrDRBalFrmWSBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
            End If
        End If


        If ddlCRBalFrmWSAccCode.SelectedIndex > 0 Then
            stAccCode = Request.Form("ddlCRBalFrmWSAccCode")
            GetAccountDetails(stAccCode, iAccType, iAccPurpose, iNurseryInd)

            If iAccType = objGLSetup.EnumAccountType.BalanceSheet Then  
                
                Select Case iNurseryInd
                    Case objGLSetup.EnumNurseryAccount.Yes
                        If ddlCRBalFrmWSBlkCode.SelectedIndex <= 0 Then
                            lblErrCRBalFrmWSBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRBalFrmWSBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
                
            Else If iAccType = objGLSetup.EnumAccountType.ProfitAndLost
                Select Case iAccPurpose
                    Case objGLSetup.EnumAccountPurpose.NonVehicle 
                        If ddlCRBalFrmWSBlkCode.SelectedIndex <= 0 Then
                            lblErrCRBalFrmWSBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRBalFrmWSBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
            End If
        End If

         If ddlCRIntIncome.SelectedIndex > 0 Then
            stAccCode = Request.Form("ddlCRIntIncome")
            GetAccountDetails(stAccCode, iAccType, iAccPurpose, iNurseryInd)

            If iAccType = objGLSetup.EnumAccountType.BalanceSheet Then  
                
                Select Case iNurseryInd
                    Case objGLSetup.EnumNurseryAccount.Yes
                        If ddlCRIntIncomeBlkCode.SelectedIndex <= 0 Then
                            lblErrCRIntIncomeBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRIntIncomeBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
                
            Else If iAccType = objGLSetup.EnumAccountType.ProfitAndLost
                Select Case iAccPurpose
                    Case objGLSetup.EnumAccountPurpose.NonVehicle 
                        If ddlCRIntIncomeBlkCode.SelectedIndex <= 0 Then
                            lblErrCRIntIncomeBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRIntIncomeBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
            End If
        End If
        
        If ddlCRSunIncome.SelectedIndex > 0 Then
            stAccCode = Request.Form("ddlCRSunIncome")
            GetAccountDetails(stAccCode, iAccType, iAccPurpose, iNurseryInd)

            If iAccType = objGLSetup.EnumAccountType.BalanceSheet Then  
                
                Select Case iNurseryInd
                    Case objGLSetup.EnumNurseryAccount.Yes
                        If ddlCRSunIncomeBlkCode.SelectedIndex <= 0 Then
                            lblErrCRSunIncomeBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRSunIncomeBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
                
            Else If iAccType = objGLSetup.EnumAccountType.ProfitAndLost
                Select Case iAccPurpose
                    Case objGLSetup.EnumAccountPurpose.NonVehicle 
                        If ddlCRSunIncomeBlkCode.SelectedIndex <= 0 Then
                            lblErrCRSunIncomeBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRSunIncomeBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
            End If
        End If


        If ddlDRAPPPNInvRcv3.SelectedIndex > 0 Then
            stAccCode = Request.Form("ddlDRAPPPNInvRcv3")
            GetAccountDetails(stAccCode, iAccType, iAccPurpose, iNurseryInd)

            If iAccType = objGLSetup.EnumAccountType.BalanceSheet Then

                Select Case iNurseryInd
                    Case objGLSetup.EnumNurseryAccount.Yes
                        If ddlDRAPPPNInvRcv3.SelectedIndex <= 0 Then
                            lblErrDRAPPPNInvRcv3Blk.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrDRAPPPNInvRcv3Blk.Visible = True
                            bRet = False
                        End If
                End Select

            ElseIf iAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
                Select Case iAccPurpose
                    Case objGLSetup.EnumAccountPurpose.NonVehicle
                        If ddlDRAPPPNInvRcv3.SelectedIndex <= 0 Then
                            lblErrDRAPPPNInvRcv3Blk.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrDRAPPPNInvRcv3Blk.Visible = True
                            bRet = False
                        End If
                End Select
            End If
        End If

        If ddlCRINStockAdj.SelectedIndex > 0 Then
            stAccCode = Request.Form("ddlCRINStockAdj")
            GetAccountDetails(stAccCode, iAccType, iAccPurpose, iNurseryInd)

            If iAccType = objGLSetup.EnumAccountType.BalanceSheet Then

                Select Case iNurseryInd
                    Case objGLSetup.EnumNurseryAccount.Yes
                        If ddlCRINStockAdj.SelectedIndex <= 0 Then
                            lblErrCRINStockAdjBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRINStockAdjBlkCode.Visible = True
                            bRet = False
                        End If
                End Select

            ElseIf iAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
                Select Case iAccPurpose
                    Case objGLSetup.EnumAccountPurpose.NonVehicle
                        If ddlCRINStockAdj.SelectedIndex <= 0 Then
                            lblErrCRINStockAdjBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRINStockAdjBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
            End If
        End If

        If ddlDRPDHPPCost.SelectedIndex > 0 Then
            stAccCode = Request.Form("ddlDRPDHPPCost")
            GetAccountDetails(stAccCode, iAccType, iAccPurpose, iNurseryInd)

            If iAccType = objGLSetup.EnumAccountType.BalanceSheet Then

                Select Case iNurseryInd
                    Case objGLSetup.EnumNurseryAccount.Yes
                        If ddlDRPDHPPCost.SelectedIndex <= 0 Then
                            lblErrDRPDHPPBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrDRPDHPPBlkCode.Visible = True
                            bRet = False
                        End If
                End Select

            ElseIf iAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
                Select Case iAccPurpose
                    Case objGLSetup.EnumAccountPurpose.NonVehicle
                        If ddlDRPDHPPCost.SelectedIndex <= 0 Then
                            lblErrDRPDHPPBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrDRPDHPPBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
            End If
        End If

        If ddlCRPDHPPCost.SelectedIndex > 0 Then
            stAccCode = Request.Form("ddlCRPDHPPCost")
            GetAccountDetails(stAccCode, iAccType, iAccPurpose, iNurseryInd)

            If iAccType = objGLSetup.EnumAccountType.BalanceSheet Then

                Select Case iNurseryInd
                    Case objGLSetup.EnumNurseryAccount.Yes
                        If ddlCRPDHPPCost.SelectedIndex <= 0 Then
                            lblErrCRPDHPPBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRPDHPPBlkCode.Visible = True
                            bRet = False
                        End If
                End Select

            ElseIf iAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
                Select Case iAccPurpose
                    Case objGLSetup.EnumAccountPurpose.NonVehicle
                        If ddlCRPDHPPCost.SelectedIndex <= 0 Then
                            lblErrCRPDHPPBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRPDHPPBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
            End If
        End If

        If ddlDRPDTBSKebun.SelectedIndex > 0 Then
            stAccCode = Request.Form("ddlDRPDTBSKebun")
            GetAccountDetails(stAccCode, iAccType, iAccPurpose, iNurseryInd)

            If iAccType = objGLSetup.EnumAccountType.BalanceSheet Then

                Select Case iNurseryInd
                    Case objGLSetup.EnumNurseryAccount.Yes
                        If ddlDRPDTBSKebun.SelectedIndex <= 0 Then
                            lblErrDRPDTBSKebunBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrDRPDTBSKebunBlkCode.Visible = True
                            bRet = False
                        End If
                End Select

            ElseIf iAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
                Select Case iAccPurpose
                    Case objGLSetup.EnumAccountPurpose.NonVehicle
                        If ddlDRPDTBSKebun.SelectedIndex <= 0 Then
                            lblErrDRPDTBSKebunBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrDRPDTBSKebunBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
            End If
        End If

        If ddlCRPDTBSKebun.SelectedIndex > 0 Then
            stAccCode = Request.Form("ddlCRPDTBSKebun")
            GetAccountDetails(stAccCode, iAccType, iAccPurpose, iNurseryInd)

            If iAccType = objGLSetup.EnumAccountType.BalanceSheet Then

                Select Case iNurseryInd
                    Case objGLSetup.EnumNurseryAccount.Yes
                        If ddlCRPDTBSKebun.SelectedIndex <= 0 Then
                            lblErrCRPDTBSKebunBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRPDTBSKebunBlkCode.Visible = True
                            bRet = False
                        End If
                End Select

            ElseIf iAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
                Select Case iAccPurpose
                    Case objGLSetup.EnumAccountPurpose.NonVehicle
                        If ddlCRPDTBSKebun.SelectedIndex <= 0 Then
                            lblErrCRPDTBSKebunBlkCode.Text = lblPleaseSelect.Text & strBlkTag
                            lblErrCRPDTBSKebunBlkCode.Visible = True
                            bRet = False
                        End If
                End Select
            End If
        End If

        Return bRet
    End Function

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "GL_CLSSETUP_ENTRYSETUP_ADD"
        Dim strOpCd_Upd As String = "GL_CLSSETUP_ENTRYSETUP_UPD"
        Dim strOpCd As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim strDRBalFrmWSBlkCode As String = Request.Form("ddlDRBalFrmWSBlkCode")
        Dim strCRBalFrmWSBlkCode As String = Request.Form("ddlCRBalFrmWSBlkCode")
   
        Dim strCRIntIncomeBlkCode As String = Request.Form("ddlCRIntIncomeBlkCode")
        Dim strDRIntIncomeBlkCode As String = ""
        Dim strCRSunIncomeBlkCode As String = Request.Form("ddlCRSunIncomeBlkCode")
        Dim strDRSunIncomeBlkCode As String = ""

        Dim strCRIntIncomeBlkCode2 As String = Request.Form("ddlCRIntIncomeBlkCode2")
        Dim strDRIntIncomeBlkCode2 As String = ""

        Dim strDRAPPPNInvRcv3Block As String = Request.Form("ddlDRAPPPNInvRcv3Blk")
        Dim strCRAPPPNInvRcv3Block As String = ""

        Dim strddlCRINStockAdjBlkCode As String = Request.Form("ddlCRINStockAdjBlkCode")
        Dim strddlDRINStockAdjBlkCode As String = ""

        Dim strddlDRPDHPPBlkCode As String = Request.Form("ddlDRPDHPPBlkCode")
        Dim strddlCRPDHPPBlkCode As String = ""

        Dim strddlDRPDTBSKebunBlkCode As String = Request.Form("ddlDRPDTBSKebunBlkCode")
        Dim strddlCRPDTBSKebunBlkCode As String = ""

        ' SEMENTARA SAVE APA ADANYA
        'If ddlDRINStkRcvDADirectPR.Enabled = True And ddlDRINStkRcvDADirectPR.SelectedItem.Value = "" Then
        '    lblErrDRINStkRcvDADirectPR.Visible = True
        '    Exit Sub
        'ElseIf ddlCRINStkRcvDADirectPR.Enabled = True And ddlCRINStkRcvDADirectPR.SelectedItem.Value = "" Then
        '    lblErrCRINStkRcvDADirectPR.Visible = True
        '    Exit Sub
        'ElseIf ddlDRINStkRcvDAStockPR.Enabled = True And ddlDRINStkRcvDAStockPR.SelectedItem.Value = "" Then
        '    lblErrDRINStkRcvDAStockPR.Visible = True
        '    Exit Sub
        'ElseIf ddlCRINStkRcvDAStockPR.Enabled = True And ddlCRINStkRcvDAStockPR.SelectedItem.Value = "" Then
        '    lblErrCRINStkRcvDAStockPR.Visible = True
        '    Exit Sub
        'ElseIf ddlDRINStkRcvStkTransfer.Enabled = True And ddlDRINStkRcvStkTransfer.SelectedItem.Value = "" Then
        '    lblErrDRINStkRcvStkTransfer.Visible = True
        '    Exit Sub
        'ElseIf ddlCRINStkRcvStkTransfer.Enabled = True And ddlCRINStkRcvStkTransfer.SelectedItem.Value = "" Then
        '    lblErrCRINStkRcvStkTransfer.Visible = True
        '    Exit Sub
        'ElseIf ddlDRINStkRcvStkRtnAdvice.Enabled = True And ddlDRINStkRcvStkRtnAdvice.SelectedItem.Value = "" Then
        '    lblErrDRINStkRcvStkRtnAdvice.Visible = True
        '    Exit Sub
        'ElseIf ddlCRINStkRcvStkRtnAdvice.Enabled = True And ddlCRINStkRcvStkRtnAdvice.SelectedItem.Value = "" Then
        '    lblErrCRINStkRcvStkRtnAdvice.Visible = True
        '    Exit Sub
        'ElseIf ddlDRINStkRtnAdvice.Enabled = True And ddlDRINStkRtnAdvice.SelectedItem.Value = "" Then
        '    lblErrDRINStkRtnAdvice.Visible = True
        '    Exit Sub
        'ElseIf ddlCRINStkRtnAdvice.Enabled = True And ddlCRINStkRtnAdvice.SelectedItem.Value = "" Then
        '    lblErrCRINStkRtnAdvice.Visible = True
        '    Exit Sub
        'ElseIf ddlDRINStkIssueEmp.Enabled = True And ddlDRINStkIssueEmp.SelectedItem.Value = "" Then
        '    lblErrDRINStkIssueEmp.Visible = True
        '    Exit Sub
        'ElseIf ddlCRINStkIssueEmp.Enabled = True And ddlCRINStkIssueEmp.SelectedItem.Value = "" Then
        '    lblErrCRINStkIssueEmp.Visible = True
        '    Exit Sub
        'ElseIf ddlDRINFuelIssueEmp.Enabled = True And ddlDRINFuelIssueEmp.SelectedItem.Value = "" Then
        '    lblErrDRINFuelIssueEmp.Visible = True
        '    Exit Sub
        'ElseIf ddlCRINFuelIssueEmp.Enabled = True And ddlCRINFuelIssueEmp.SelectedItem.Value = "" Then
        '    lblErrCRINFuelIssueEmp.Visible = True
        '    Exit Sub
        'ElseIf ddlCRINStockAdj.Enabled = True And ddlCRINStockAdj.SelectedItem.Value = "" Then
        '    lblErrCRINStockAdj.Visible = True
        '    Exit Sub

        '    'ElseIf ddlDRINBalanceFromStkRtnAdvice.Enabled = True And ddlDRINBalanceFromStkRtnAdvice.SelectedItem.Value = "" Then
        '    '    lblErrDRINBalanceFromStkRtnAdvice.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlCRINBalanceFromStkRtnAdvice.Enabled = True And ddlCRINBalanceFromStkRtnAdvice.SelectedItem.Value = "" Then
        '    '    lblErrCRINBalanceFromStkRtnAdvice.Visible = True
        '    '    Exit Sub 

        '    'ElseIf ddlDRCTStkRcvStkTransfer.Enabled = True And ddlDRCTStkRcvStkTransfer.SelectedItem.Value = "" Then
        '    '    lblErrDRCTStkRcvStkTransfer.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlCRCTStkRcvStkTransfer.Enabled = True And ddlCRCTStkRcvStkTransfer.SelectedItem.Value = "" Then
        '    '    lblErrCRCTStkRcvStkTransfer.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlDRCTStkRcvStkRtnAdvice.Enabled = True And ddlDRCTStkRcvStkRtnAdvice.SelectedItem.Value = "" Then
        '    '    lblErrDRCTStkRcvStkRtnAdvice.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlCRCTStkRcvStkRtnAdvice.Enabled = True And ddlCRCTStkRcvStkRtnAdvice.SelectedItem.Value = "" Then
        '    '    lblErrCRCTStkRcvStkRtnAdvice.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlDRCTStkRtnAdvice.Enabled = True And ddlDRCTStkRtnAdvice.SelectedItem.Value = "" Then
        '    '    lblErrDRCTStkRtnAdvice.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlCRCTStkRtnAdvice.Enabled = True And ddlCRCTStkRtnAdvice.SelectedItem.Value = "" Then
        '    '    lblErrCRCTStkRtnAdvice.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlDRCTStkIssueEmp.Enabled = True And ddlDRCTStkIssueEmp.SelectedItem.Value = "" Then
        '    '    lblErrDRCTStkIssueEmp.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlCRCTStkIssueEmp.Enabled = True And ddlCRCTStkIssueEmp.SelectedItem.Value = "" Then
        '    '    lblErrCRCTStkIssueEmp.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlDRCTBalanceFromCTRtnAdvice.Enabled = True And ddlDRCTBalanceFromCTRtnAdvice.SelectedItem.Value = "" Then
        '    '    lblErrDRCTBalanceFromCTRtnAdvice.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlCRCTBalanceFromCTRtnAdvice.Enabled = True And ddlCRCTBalanceFromCTRtnAdvice.SelectedItem.Value = "" Then
        '    '    lblErrCRCTBalanceFromCTRtnAdvice.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlDRWSJobEmp.Enabled = True And ddlDRWSJobEmp.SelectedItem.Value = "" Then
        '    '    lblErrDRWSJobEmp.Visible = True
        '    '    Exit Sub
        'ElseIf ddlDRNUSeedlingsIssue.Enabled = True And ddlDRNUSeedlingsIssue.SelectedItem.Value = "" Then
        '    lblErrDRNUSeedlingsIssue.Visible = True
        '    Exit Sub
        'ElseIf ddlCRNUSeedlingsIssue.Enabled = True And ddlCRNUSeedlingsIssue.SelectedItem.Value = "" Then
        '    lblErrCRNUSeedlingsIssue.Visible = True
        '    Exit Sub
        'ElseIf ddlDRPUGoodsRcv.Enabled = True And ddlDRPUGoodsRcv.SelectedItem.Value = "" Then
        '    lblErrDRPUGoodsRcv.Visible = True
        '    Exit Sub
        'ElseIf ddlCRPUGoodsRcv.Enabled = True And ddlCRPUGoodsRcv.SelectedItem.Value = "" Then
        '    lblErrCRPUGoodsRcv.Visible = True
        '    Exit Sub
        'ElseIf ddlDRAPInvRcv.Enabled = True And ddlDRAPInvRcv.SelectedItem.Value = "" Then
        '    lblErrDRAPInvRcv.Visible = True
        '    Exit Sub
        'ElseIf ddlCRAPInvRcv.Enabled = True And ddlCRAPInvRcv.SelectedItem.Value = "" Then
        '    lblErrCRAPInvRcv.Visible = True
        '    Exit Sub

        'ElseIf ddlDRAPPPNInvRcv.Enabled = True And ddlDRAPPPNInvRcv.SelectedItem.Value = "" Then
        '    lblErrDRAPPPNInvRcv.Visible = True
        '    Exit Sub
        'ElseIf ddlCRAPPPNInvRcv.Enabled = True And ddlCRAPPPNInvRcv.SelectedItem.Value = "" Then
        '    lblErrCRAPPPNInvRcv.Visible = True
        '    Exit Sub

        'ElseIf ddlDRAPPPNInvRcv2.Enabled = True And ddlDRAPPPNInvRcv2.SelectedItem.Value = "" Then
        '    lblErrDRAPPPNInvRcv2.Visible = True
        '    Exit Sub
        'ElseIf ddlCRAPPPNInvRcv2.Enabled = True And ddlCRAPPPNInvRcv2.SelectedItem.Value = "" Then
        '    lblErrCRAPPPNInvRcv2.Visible = True
        '    Exit Sub

        'ElseIf ddlDRAPPPNInvRcv4.Enabled = True And ddlDRAPPPNInvRcv4.SelectedItem.Value = "" Then
        '    lblErrDRAPPPNInvRcv4.Visible = True
        '    Exit Sub
        'ElseIf ddlCRAPPPNInvRcv4.Enabled = True And ddlCRAPPPNInvRcv4.SelectedItem.Value = "" Then
        '    lblErrCRAPPPNInvRcv4.Visible = True
        '    Exit Sub

        'ElseIf ddlDRAPPPNInvRcv3.Enabled = True And ddlDRAPPPNInvRcv3.SelectedItem.Value = "" Then
        '    lblErrDRAPPPNInvRcv3.Visible = True
        '    Exit Sub
        'ElseIf ddlCRAPPPNInvRcv3.Enabled = True And ddlCRAPPPNInvRcv3.SelectedItem.Value = "" Then
        '    lblErrCRAPPPNInvRcv3.Visible = True
        '    Exit Sub


        'ElseIf ddlDRAPPPHInvRcv.Enabled = True And ddlDRAPPPHInvRcv.SelectedItem.Value = "" Then
        '    lblErrDRAPPPHInvRcv.Visible = True
        '    Exit Sub
        'ElseIf ddlCRAPPPHInvRcv.Enabled = True And ddlCRAPPPHInvRcv.SelectedItem.Value = "" Then
        '    lblErrCRAPPPHInvRcv.Visible = True
        '    Exit Sub
        'ElseIf ddlDRCBPPHInvPay.Enabled = True And ddlDRCBPPHInvPay.SelectedItem.Value = "" Then
        '    lblErrDRCBPPHInvPay.Visible = True
        '    Exit Sub
        'ElseIf ddlCRCBPPHInvPay.Enabled = True And ddlCRCBPPHInvPay.SelectedItem.Value = "" Then
        '    lblErrCRCBPPHInvPay.Visible = True
        '    Exit Sub
        'ElseIf ddlDRAPPPNCrdJrn.Enabled = True And ddlDRAPPPNCrdJrn.SelectedItem.Value = "" Then
        '    lblErrDRAPPPNCrdJrn.Visible = True
        '    Exit Sub
        'ElseIf ddlCRAPPPNCrdJrn.Enabled = True And ddlCRAPPPNCrdJrn.SelectedItem.Value = "" Then
        '    lblErrCRAPPPNCrdJrn.Visible = True
        '    Exit Sub
        'ElseIf ddlDRAPPPHCrdJrn.Enabled = True And ddlDRAPPPHCrdJrn.SelectedItem.Value = "" Then
        '    lblErrDRAPPPHCrdJrn.Visible = True
        '    Exit Sub
        'ElseIf ddlCRAPPPHCrdJrn.Enabled = True And ddlCRAPPPHCrdJrn.SelectedItem.Value = "" Then
        '    lblErrCRAPPPHCrdJrn.Visible = True
        '    Exit Sub
        'ElseIf ddlDRAdvPayment.Enabled = True And ddlDRAdvPayment.SelectedItem.Value = "" Then
        '    lblErrDRAdvPayment.Visible = True
        '    Exit Sub
        'ElseIf ddlDRBIPPNInvRcv.Enabled = True And ddlDRBIPPNInvRcv.SelectedItem.Value = "" Then
        '    lblErrDRBIPPNInvRcv.Visible = True
        '    Exit Sub
        'ElseIf ddlCRBIPPNInvRcv.Enabled = True And ddlCRBIPPNInvRcv.SelectedItem.Value = "" Then
        '    lblErrCRBIPPNInvRcv.Visible = True
        '    Exit Sub
        'ElseIf ddlDRBIPPHInvRcv.Enabled = True And ddlDRBIPPHInvRcv.SelectedItem.Value = "" Then
        '    lblErrDRBIPPHInvRcv.Visible = True
        '    Exit Sub
        'ElseIf ddlCRBIPPHInvRcv.Enabled = True And ddlCRBIPPHInvRcv.SelectedItem.Value = "" Then
        '    lblErrCRBIPPHInvRcv.Visible = True
        '    Exit Sub
        'ElseIf ddlDRCBPPNRcpt.Enabled = True And ddlDRCBPPNRcpt.SelectedItem.Value = "" Then
        '    lblErrDRCBPPNRcpt.Visible = True
        '    Exit Sub
        'ElseIf ddlCRCBPPNRcpt.Enabled = True And ddlCRCBPPNRcpt.SelectedItem.Value = "" Then
        '    lblErrCRCBPPNRcpt.Visible = True
        '    Exit Sub
        'ElseIf ddlDRCBPPHRcpt.Enabled = True And ddlDRCBPPHRcpt.SelectedItem.Value = "" Then
        '    lblErrDRBIPPHRcpt.Visible = True
        '    Exit Sub
        'ElseIf ddlCRCBPPHRcpt.Enabled = True And ddlCRCBPPHRcpt.SelectedItem.Value = "" Then
        '    lblErrCRCBPPHRcpt.Visible = True
        '    Exit Sub
        'ElseIf ddlDRPRClr.Enabled = True And ddlDRPRClr.SelectedItem.Value = "" Then
        '    lblErrCRPRClr.Visible = True
        '    Exit Sub
        'ElseIf ddlCRPRClr.Enabled = True And ddlCRPRClr.SelectedItem.Value = "" Then
        '    lblErrCRPRClr.Visible = True
        '    Exit Sub
        'ElseIf ddlDRPRHK.Enabled = True And ddlDRPRHK.SelectedItem.Value = "" Then
        '    lblErrDRPRHK.Visible = True
        '    Exit Sub
        'ElseIf ddlCRPRHK.Enabled = True And ddlCRPRHK.SelectedItem.Value = "" Then
        '    lblErrCRPRHK.Visible = True
        '    Exit Sub
        'ElseIf ddlDRIntIncome.Enabled = True And ddlDRIntIncome.SelectedItem.Value = "" Then
        '    lblErrDRIntIncome.Visible = True
        '    Exit Sub
        'ElseIf ddlCRIntIncome.Enabled = True And ddlCRIntIncome.SelectedItem.Value = "" Then
        '    lblErrCRIntIncome.Visible = True
        '    Exit Sub

        'ElseIf ddlDRIntIncome2.Enabled = True And ddlDRIntIncome2.SelectedItem.Value = "" Then
        '    lblErrDRIntIncome2.Visible = True
        '    Exit Sub
        'ElseIf ddlCRIntIncome2.Enabled = True And ddlCRIntIncome2.SelectedItem.Value = "" Then
        '    lblErrCRIntIncome2.Visible = True
        '    Exit Sub

        '    'ElseIf ddlDREstYield.Enabled = True And ddlDREstYield.SelectedItem.Value = "" Then
        '    '    lblErrDREstYield.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlCREstYield.Enabled = True And ddlCREstYield.SelectedItem.Value = "" Then
        '    '    lblErrCREstYield.Visible = True
        '    '    Exit Sub
        'ElseIf ddlDRSunIncome.Enabled = True And ddlDRSunIncome.SelectedItem.Value = "" Then
        '    lblErrDRSunIncome.Visible = True
        '    Exit Sub
        'ElseIf ddlCRSunIncome.Enabled = True And ddlCRSunIncome.SelectedItem.Value = "" Then
        '    lblErrCRSunIncome.Visible = True
        '    Exit Sub
        'ElseIf ddlDRVehSuspende.Enabled = True And ddlDRVehSuspende.SelectedItem.Value = "" Then
        '    lblErrDRVehSuspende.Visible = True
        '    Exit Sub
        'ElseIf ddlCRVehSuspende.Enabled = True And ddlCRVehSuspende.SelectedItem.Value = "" Then
        '    lblErrCRVehSuspende.Visible = True
        '    Exit Sub
        'ElseIf ddlDRRetainEarn.Enabled = True And ddlDRRetainEarn.SelectedItem.Value = "" Then
        '    lblErrDRRetainEarn.Visible = True
        '    Exit Sub
        'ElseIf ddlCRRetainEarn.Enabled = True And ddlCRRetainEarn.SelectedItem.Value = "" Then
        '    lblErrCRRetainEarn.Visible = True
        '    Exit Sub
        'ElseIf ddlDRBalFrmWSAccCode.Enabled = True And ddlDRBalFrmWSAccCode.SelectedItem.Value = "" Then
        '    lblErrDRBalFrmWSAccCode.Visible = True
        '    Exit Sub
        'ElseIf ddlCRBalFrmWSAccCode.Enabled = True And ddlCRBalFrmWSAccCode.SelectedItem.Value = "" Then
        '    lblErrCRBalFrmWSAccCode.Visible = True
        '    Exit Sub
        'ElseIf ddlDRWSCtrlAcc.Enabled = True And ddlDRWSCtrlAcc.SelectedItem.Value = "" Then
        '    lblErrDRWSCtrlAcc.Visible = True
        '    Exit Sub
        'ElseIf ddlCRWSCtrlAcc.Enabled = True And ddlCRWSCtrlAcc.SelectedItem.Value = "" Then
        '    lblErrCRWSCtrlAcc.Visible = True
        '    Exit Sub
        'ElseIf ddlDRPEBMovementAccCode.Enabled = True And ddlDRPEBMovementAccCode.SelectedItem.Value = "" Then
        '    lblErrDRPEBMovementAccCode.Visible = True
        '    Exit Sub
        'ElseIf ddlCRPEBMovementAccCode.Enabled = True And ddlCRPEBMovementAccCode.SelectedItem.Value = "" Then
        '    lblErrCRPEBMovementAccCode.Visible = True
        '    Exit Sub
        '    'ElseIf ddlDRPUDispAdv.Enabled = True And ddlDRPUDispAdv.SelectedItem.Value = "" Then
        '    '    lblErrDRPUDispAdv.Visible = True
        '    '    Exit Sub
        '    'ElseIf ddlCRPUDispAdv.Enabled = True And ddlCRPUDispAdv.SelectedItem.Value = "" Then
        '    '    lblErrCRPUDispAdv.Visible = True
        '    '    Exit Sub

        'End If


        If strLocType = objAdminLoc.EnumLocType.Mill And intLocLevel = objAdminLoc.EnumLocLevel.Estate Then
            If ddlDRPDHPPCost.Enabled = True And ddlDRPDHPPCost.SelectedItem.Value = "" Then
                lblErrDRPDHPPCost.Visible = True
                Exit Sub
            ElseIf ddlCRPDHPPCost.Enabled = True And ddlCRPDHPPCost.SelectedItem.Value = "" Then
                lblErrCRPDHPPCost.Visible = True
                Exit Sub
            End If

            ' CEK APAKAH PUNYA UNIT KEBUN 
            Dim strParamName As String = "STRSEARCH"
            Dim strParamValue As String = " AND LocLevel='1' AND LocType='1' "
            Dim strOp_Cd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
            Dim objDS As New DataSet()

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOp_Cd, strParamName, strParamValue, objDS)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & Exp.ToString & "&redirect=")
            End Try

            If objDS.Tables(0).Rows.Count > 0 Then
                If ddlDRPDTBSKebun.Enabled = True And ddlDRPDTBSKebun.SelectedItem.Value = "" Then
                    lblErrDRPDTBSKebun.Visible = True
                    Exit Sub
                ElseIf ddlCRPDTBSKebun.Enabled = True And ddlCRPDTBSKebun.SelectedItem.Value = "" Then
                    lblErrCRPDTBSKebun.Visible = True
                    Exit Sub
                End If
            End If
        End If


        If strLocType = objAdminLoc.EnumLocType.Estate And intLocLevel = objAdminLoc.EnumLocLevel.Estate Then
            ' CEK APAKAH PUNYA UNIT PMKS
            Dim strParamName As String = "STRSEARCH"
            Dim strParamValue As String = " AND LocLevel='1' AND LocType='4' "
            Dim strOp_Cd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
            Dim objDS As New DataSet()

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOp_Cd, strParamName, strParamValue, objDS)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & Exp.ToString & "&redirect=")
            End Try

            If objDS.Tables(0).Rows.Count > 0 Then
                If ddlDRPDHPPCost.Enabled = True And ddlDRPDHPPCost.SelectedItem.Value = "" Then
                    lblErrDRPDHPPCost.Visible = True
                    Exit Sub
                    'ElseIf ddlCRPDHPPCost.Enabled = True And ddlCRPDHPPCost.SelectedItem.Value = "" Then
                    '    lblErrCRPDHPPCost.Visible = True
                    '    Exit Sub
                End If
            End If
        End If

        If CheckRequireField() <> True Then
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            strOpCd = strOpCd_Upd
            strParam = ddlDRINStkRcvDADirectPR.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlCRINStkRcvDADirectPR.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlDRINStkRcvDAStockPR.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlCRINStkRcvDAStockPR.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlDRINStkRcvStkTransfer.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlCRINStkRcvStkTransfer.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlDRINStkRcvStkRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlCRINStkRcvStkRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlDRINStkRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlCRINStkRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlDRINStkIssueEmp.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlCRINStkIssueEmp.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlDRINFuelIssueEmp.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlCRINFuelIssueEmp.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlDRCTStkRcvDADirectPR.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlCRCTStkRcvDADirectPR.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlDRCTStkRcvDAStockPR.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlCRCTStkRcvDAStockPR.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlDRCTStkRcvStkTransfer.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlCRCTStkRcvStkTransfer.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlDRCTStkRcvStkRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlCRCTStkRcvStkRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlDRCTStkRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlCRCTStkRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlDRCTStkIssueEmp.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlCRCTStkIssueEmp.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlDRWSJobEmp.SelectedItem.Value & "|" & objGlobal.EnumModule.Workshop & "|" & Chr(9) & _
                        ddlCRWSJobEmp.SelectedItem.Value & "|" & objGlobal.EnumModule.Workshop & "|" & Chr(9) & _
                        ddlDRPUGoodsRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.Purchasing & "|" & Chr(9) & _
                        ddlCRPUGoodsRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.Purchasing & "|" & Chr(9) & _
                        ddlDRAPInvRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlCRAPInvRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlDRPRClr.SelectedItem.Value & "|" & objGlobal.EnumModule.Payroll & "|" & Chr(9) & _
                        ddlCRPRClr.SelectedItem.Value & "|" & objGlobal.EnumModule.Payroll & "|" & Chr(9) & _
                        ddlDREstYield.SelectedItem.Value & "|" & objGlobal.EnumModule.Production & "|" & Chr(9) & _
                        ddlCREstYield.SelectedItem.Value & "|" & objGlobal.EnumModule.Production & "|" & Chr(9) & _
                        ddlDRSunIncome.SelectedItem.Value & "|" & objGlobal.EnumModule.GeneralLedger & "|" & strDRSunIncomeBlkCode & Chr(9) & _
                        ddlCRSunIncome.SelectedItem.Value & "|" & objGlobal.EnumModule.GeneralLedger & "|" & strCRSunIncomeBlkCode & Chr(9) & _
                        ddlDRVehSuspende.SelectedItem.Value & "|" & objGlobal.EnumModule.GeneralLedger & "|" & Chr(9) & _
                        ddlCRVehSuspende.SelectedItem.Value & "|" & objGlobal.EnumModule.GeneralLedger & "|" & Chr(9) & _
                        ddlDRRetainEarn.SelectedItem.Value & "|" & objGlobal.EnumModule.GeneralLedger & "|" & Chr(9) & _
                        ddlCRRetainEarn.SelectedItem.Value & "|" & objGlobal.EnumModule.GeneralLedger & "|" & Chr(9) & _
                        ddlDRINBalanceFromStkRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlCRINBalanceFromStkRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & Chr(9) & _
                        ddlDRCTBalanceFromCTRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlCRCTBalanceFromCTRtnAdvice.SelectedItem.Value & "|" & objGlobal.EnumModule.Canteen & "|" & Chr(9) & _
                        ddlDRNUSeedlingsIssue.SelectedItem.Value & "|" & objGlobal.EnumModule.Nursery & "|" & Chr(9) & _
                        ddlCRNUSeedlingsIssue.SelectedItem.Value & "|" & objGlobal.EnumModule.Nursery & "|" & Chr(9) & _
                        ddlDRBalFrmWSAccCode.SelectedItem.Value & "|" & objGlobal.EnumModule.GeneralLedger & "|" & strDRBalFrmWSBlkCode & Chr(9) & _
                        ddlCRBalFrmWSAccCode.SelectedItem.Value & "|" & objGlobal.EnumModule.GeneralLedger & "|" & strCRBalFrmWSBlkCode & Chr(9) & _
                        ddlDRWSCtrlAcc.SelectedItem.Value & "|" & objGlobal.EnumModule.Workshop & "|" & Chr(9) & _
                        ddlCRWSCtrlAcc.SelectedItem.Value & "|" & objGlobal.EnumModule.Workshop & "|" & Chr(9) & _
                        ddlDRAPPPNInvRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlCRAPPPNInvRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlDRAPPPHInvRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlCRAPPPHInvRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlDRCBPPHInvPay.SelectedItem.Value & "|" & objGlobal.EnumModule.CashAndBank & "|" & Chr(9) & _
                        ddlCRCBPPHInvPay.SelectedItem.Value & "|" & objGlobal.EnumModule.CashAndBank & "|" & Chr(9) & _
                        ddlDRAPPPNCrdJrn.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlCRAPPPNCrdJrn.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlDRAPPPHCrdJrn.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlCRAPPPHCrdJrn.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlDRBIPPNInvRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.Billing & "|" & Chr(9) & _
                        ddlCRBIPPNInvRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.Billing & "|" & Chr(9) & _
                        ddlDRBIPPHInvRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.Billing & "|" & Chr(9) & _
                        ddlCRBIPPHInvRcv.SelectedItem.Value & "|" & objGlobal.EnumModule.Billing & "|" & Chr(9) & _
                        ddlDRCBPPNRcpt.SelectedItem.Value & "|" & objGlobal.EnumModule.CashAndBank & "|" & Chr(9) & _
                        ddlCRCBPPNRcpt.SelectedItem.Value & "|" & objGlobal.EnumModule.CashAndBank & "|" & Chr(9) & _
                        ddlDRCBPPHRcpt.SelectedItem.Value & "|" & objGlobal.EnumModule.CashAndBank & "|" & Chr(9) & _
                        ddlCRCBPPHRcpt.SelectedItem.Value & "|" & objGlobal.EnumModule.CashAndBank & "|" & Chr(9) & _
                        ddlDRPRHK.SelectedItem.Value & "|" & objGlobal.EnumModule.Payroll & "|" & Chr(9) & _
                        ddlCRPRHK.SelectedItem.Value & "|" & objGlobal.EnumModule.Payroll & "|" & Chr(9) & _
                        ddlDRIntIncome.SelectedItem.Value & "|" & objGlobal.EnumModule.CashAndBank & "|" & strDRIntIncomeBlkCode & Chr(9) & _
                        ddlCRIntIncome.SelectedItem.Value & "|" & objGlobal.EnumModule.CashAndBank & "|" & strCRIntIncomeBlkCode & Chr(9) & _
                        ddlDRPEBMovementAccCode.SelectedItem.Value & "|" & objGlobal.EnumModule.GeneralLedger & "|" & Chr(9) & _
                        ddlCRPEBMovementAccCode.SelectedItem.Value & "|" & objGlobal.EnumModule.GeneralLedger & "|" & Chr(9) & _
                        ddlDRPUDispAdv.SelectedItem.Value & "|" & objGlobal.EnumModule.Purchasing & "|" & Chr(9) & _
                        ddlCRPUDispAdv.SelectedItem.Value & "|" & objGlobal.EnumModule.Purchasing & "|" & Chr(9) & _
                        ddlDRIntIncome2.SelectedItem.Value & "|" & objGlobal.EnumModule.CashAndBank & "|" & strDRIntIncomeBlkCode2 & Chr(9) & _
                        ddlCRIntIncome2.SelectedItem.Value & "|" & objGlobal.EnumModule.CashAndBank & "|" & strCRIntIncomeBlkCode2 & Chr(9) & _
                        ddlDRAPPPNInvRcv2.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlCRAPPPNInvRcv2.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlDRAPPPNInvRcv3.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & strDRAPPPNInvRcv3Block & Chr(9) & _
                        ddlCRAPPPNInvRcv3.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & strCRAPPPNInvRcv3Block & Chr(9) & _
                        ddlDRAPPPNInvRcv4.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlCRAPPPNInvRcv4.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlDRINStockAdj.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & strddlDRINStockAdjBlkCode & Chr(9) & _
                        ddlCRINStockAdj.SelectedItem.Value & "|" & objGlobal.EnumModule.Inventory & "|" & strddlCRINStockAdjBlkCode & Chr(9) & _
                        ddlDRPDHPPCost.SelectedItem.Value & "|" & objGlobal.EnumModule.Production & "|" & strddlDRPDHPPBlkCode & Chr(9) & _
                        ddlCRPDHPPCost.SelectedItem.Value & "|" & objGlobal.EnumModule.Production & "|" & strddlCRPDHPPBlkCode & Chr(9) & _
                        ddlDRPDTBSKebun.SelectedItem.Value & "|" & objGlobal.EnumModule.Production & "|" & strddlDRPDTBSKebunBlkCode & Chr(9) & _
                        ddlCRPDTBSKebun.SelectedItem.Value & "|" & objGlobal.EnumModule.Production & "|" & strddlCRPDTBSKebunBlkCode & Chr(9) & _
                        ddlDRPDStockCPO.SelectedItem.Value & "|" & objGlobal.EnumModule.Production & "|" & Chr(9) & _
                        ddlCRPDStockCPO.SelectedItem.Value & "|" & objGlobal.EnumModule.Production & "|" & Chr(9) & _
                        ddlDRPDStockPK.SelectedItem.Value & "|" & objGlobal.EnumModule.Production & "|" & Chr(9) & _
                        ddlCRPDStockPK.SelectedItem.Value & "|" & objGlobal.EnumModule.Production & "|" & Chr(9) & _
                        ddlDRAdvPayment.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9) & _
                        ddlCRAdvPayment.SelectedItem.Value & "|" & objGlobal.EnumModule.AccountPayable & "|" & Chr(9)

            Try
                intErrNo = objGLSetup.mtdEntrySetup(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strOpCd, _
                                                    strParam, _
                                                    False, _
                                                    objGLDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_SAVE&errmesg=" & Exp.ToString & "&redirect=GL/setup/GL_setup_DoubleEntry.aspx")
            End Try
        End If

        onLoad_Display()
    End Sub

    Sub btnSaveTBS_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_DKtr As String = "WM_CLSTRX_TICKET_COASETTING_BUY_UPDATE"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim strMn As String
        Dim strYr As String
        Dim bOK AS Boolean=False
        Dim strParamName AS String
        Dim strParamValue AS String

        'strMn = ddlMonth.SelectedItem.Value.Trim
        'strYr = ddlyear.SelectedItem.Value.Trim

        strParamName = "COATBSPEMILIK|COATBSAGEN|COAPPN|COAONGKOSBONGKAR|COAONGKOSLAPANGAN|COAPPH"
        strParamValue = Trim(radTbsPemilik.SelectedValue) & "|" & Trim(radTbsAgen.SelectedValue) & "|" & Trim(radTbsPPN.SelectedValue) & "|" & _
                        Trim(radTBSOBongkar.SelectedValue) & "|" & Trim(radTbsOLapangan.SelectedValue) & "|" & Trim(radTbsPPH.SelectedValue)

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_DKtr, strParamName, strParamValue)
            bOK=True
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        IF bOK=True Then
            UserMsgBox(Me,"Save Completed")
        End If
        

        LoadCOATBS()
    End Sub

    Sub btnSaveSales_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_DKtr As String = "WM_CLSTRX_TICKET_COASETTING_SALE_UPDATE"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim strParamName AS String
        Dim strParamValue AS String
        Dim bOK AS Boolean=False
       
        strParamName = "COASalesCPO|COASalesKNL|COASalesOTH|COASalesEFB|COASalesCKG|COASalesABJ|COASalesFBR|COASalesSLD|COASalesLMB|COASalesPPN"
        strParamValue = Trim(ddlSalesCPO.SelectedValue) & _
                        "|" & Trim(ddlSalesKNL.SelectedValue) & _
                        "|" & Trim(ddlSalesOTH.SelectedValue) & _
                        "|" & Trim(ddlSalesEFB.SelectedValue) & _
                        "|" & Trim(ddlSalesCKG.SelectedValue) & _
                        "|" & Trim(ddlSalesABJ.SelectedValue) & _
                        "|" & Trim(ddlSalesFBR.SelectedValue) & _
                        "|" & Trim(ddlSalesSLD.SelectedValue) & _
                        "|" & Trim(ddlSalesLMB.SelectedValue) & _
                        "|" & Trim(ddlSalesPPN.SelectedValue)        
        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_DKtr, strParamName, strParamValue)
            bOK=True
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        IF bOK=True Then
            UserMsgBox(Me,"Save Completed")
        End If

        LoadCOASales()
    End Sub

    Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

End Class
