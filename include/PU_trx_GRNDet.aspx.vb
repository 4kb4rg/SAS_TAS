
Imports System
Imports System.Data
Imports System.Collections
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.Drawing.Color


Public Class PU_GRNDet : Inherits Page

    Protected WithEvents lblErrManySelectDoc As Label
    Protected WithEvents lblErrOnHand As Label
    Protected WithEvents lblErrOnHold As Label
    Protected WithEvents lblQty As Label
    Protected WithEvents lblErrQtyReturn As Label
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblErrVehExp As Label
    Protected WithEvents lblCost As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblPleaseSelectOne As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehExpense As Label

    Protected WithEvents lblGoodsRetId As Label
    Protected WithEvents lblGoodsRetType As Label
    Protected WithEvents lblAccPeriod As Label
    'Protected WithEvents ddlSuppCode As DropDownList
    Protected WithEvents txtSupCode As TextBox
    Protected WithEvents txtSupName As TextBox

    Protected WithEvents lblSuppCode As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblHidStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblUpdateDate As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents ddlInvRcvId As DropDownList
    Protected WithEvents ddlRetAdvId As DropDownList
    Protected WithEvents ddlGoodsRcvId As DropDownList
    Protected WithEvents ddlItemCode As DropDownList
    Protected WithEvents lblQtyOnHand As Label
    Protected WithEvents txtQty As TextBox
    Protected WithEvents txtCost As TextBox
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents ddlVehExpCode As DropDownList
    Protected WithEvents dgGRNDet As DataGrid
    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents lblVehicleOption As Label
    Protected WithEvents lblErrFACode As Label

    Protected WithEvents lblAssetCode As Label

    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnConfirm As ImageButton
    Protected WithEvents btnPrint As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnCancel As ImageButton
    Protected WithEvents GoodsRetId As HtmlInputHidden
    Protected WithEvents tblLine As HtmlTable
    Protected WithEvents tblDoc As HtmlTable
    Protected WithEvents tblAcc As HtmlTable
    Protected WithEvents FindAcc As HtmlInputButton
    Protected WithEvents hidItemType As HtmlInputHidden

    Protected WithEvents tblDoc1 As HtmlTable
    Protected WithEvents tblFACode As HtmlTable

    Protected WithEvents lblGoodsRet As Label
    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlk As HtmlTableRow
    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents lblPreBlockErr As Label
    Protected WithEvents lblPreBlkTag As Label
    Protected WithEvents ddlPreBlock As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Protected WithEvents lblID As Label
    Protected WithEvents lblInvoiceRcvTag As Label
    Protected WithEvents lblInvoiceRcvIDTag AS Label
    Protected WithEvents lblDocument As Label
    Dim PreBlockTag As String
    Dim BlockTag As String
    Protected WithEvents lblErrRcvQty As Label
    Protected WithEvents lblRcvQty As Label
    Protected WithEvents ddlInventoryBin As DropDownList
    Protected WithEvents lblInventoryBin As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents txtGoodsRetDate As TextBox
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents lblQtyDisp As Label
    Protected WithEvents ddlDispAdvID As DropDownList

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbViewJournal As LinkButton
    Protected WithEvents dgViewJournal As DataGrid
    Protected WithEvents lblTotalDB As Label
    Protected WithEvents lblTotalCR As Label
    Protected WithEvents lblTotalViewJournal As Label

    Protected WithEvents hidOriCost As HtmlInputHidden
    Protected WithEvents hidPOLNID As HtmlInputHidden

    Protected objPU As New agri.PU.clsTrx()
    Protected objPUSetup As New agri.PU.clsSetup()
    Protected objAP As New agri.AP.clsTrx()
    Protected objIN As New agri.IN.clsTrx()
    Protected objFA As New agri.FA.clsSetup()
    Protected objFATrx As New agri.FA.clsTrx()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objAdmin As New agri.Admin.clsLoc()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmShare As New agri.Admin.clsShare()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objAccDs As New DataSet()
    Dim objBlkDs As New DataSet()
    Dim objVehDs As New DataSet()
    Dim objVehExpDs As New DataSet()
    Dim objLangCapDs As New DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPUAR As Integer
    Dim intFAAR As Integer
    Dim intConfigSetting As Integer

    Dim strSelectedGoodsRetId As String
    Dim strSelectedGoodsRetType As String
    Dim strSelectedSuppCode As String
    Dim strSelectedInvRcvId As String
    Dim strSelectedRetAdvId As String
    Dim strSelectedGoodsRcvId As String
    Dim strSelectedItemCode As String
    Dim strItemType As String
    Dim strAcceptFormat As String
    Dim strSelectedDispAdvId As String

    Protected WithEvents lblIDQtyOnHand As Label
    Protected WithEvents lblInvisibleCost As Label
    Protected WithEvents lblIDTotalAmount As Label
    Protected WithEvents RegularExpressionValidatorCost As RegularExpressionValidator

    Const ITEM_PART_SEPERATOR As String = " @ "
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer
    Dim strLocLevel As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
        intFAAR = Session("SS_FAAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        intLevel = Session("SS_USRLEVEL")
        strLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReturnNote), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrManySelectDoc.ForeColor = Black
            lblErrQtyReturn.Visible = False
            lblErrAccount.Visible = False
            lblPreBlockErr.Visible = False
            lblErrBlock.Visible = False
            lblErrVehicle.Visible = False
            lblErrVehExp.Visible = False
            lblSuppCode.Visible = False

            lblErrRcvQty.Visible = False
            btnDelete.Visible = False
            btnUnDelete.Visible = False
            btnCancel.Visible = False
            btnPrint.Visible = False
            btnConfirm.Visible = False
            lblDate.Visible = False
            lblInventoryBin.Visible = False
            lblQtyDisp.Visible = False

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            btnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnNew).ToString())
            btnSave.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnSave).ToString())
            btnConfirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnConfirm).ToString())
            btnPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnPrint).ToString())
            btnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnDelete).ToString())
            btnUnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnUnDelete).ToString())
            btnBack.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnBack).ToString())
            btnCancel.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnCancel).ToString())

            strSelectedGoodsRetId = Trim(IIf(Request.QueryString("GoodsRetId") = "", Request.Form("GoodsRetId"), Request.QueryString("GoodsRetId")))
            strSelectedGoodsRetType = Trim(IIf(Request.QueryString("GRNType") = "", Request.Form("GRNType"), Request.QueryString("GRNType")))

            If strSelectedGoodsRetType = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
                tblFACode.Visible = True
                lblAssetCode.Visible = True
            End If

            onload_GetLangCap()


            txtSupCode.Attributes.Add("readonly", "readonly")
            txtSupName.Attributes.Add("readonly", "readonly")

            If Not IsPostBack Then
                lblQtyOnHand.Text = 0
                txtQty.Text = 0
                txtCost.Text = 0
                BindInventoryBinLevel("")
                BindChargeLevelDropDownList()
                If strSelectedGoodsRetId <> "" Then
                    onLoad_Display(strSelectedGoodsRetId)
                    onLoad_DisplayLn(strSelectedGoodsRetId)
                    'BindSupp(strSelectedGoodsRetId)
                    BindInvRcv(strSelectedSuppCode)
                    BindRetAdv(strSelectedSuppCode)
                    BindGoodsRcv(strSelectedSuppCode)
                    BindDispAdv(strSelectedSuppCode)
                Else
                    If strSelectedGoodsRetType <> "" Then
                        lblGoodsRetType.Text = strSelectedGoodsRetType
                        If lblGoodsRetType.Text = objPU.EnumGRNType.Stock Then
                            lblGoodsRet.Text = "Stock / Workshop"
                        Else
                            lblGoodsRet.Text = objPU.mtdGetGRNType(CInt(strSelectedGoodsRetType))
                        End If
                    End If
                    txtGoodsRetDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    'BindSupp("")
                    BindInvRcv(strSelectedSuppCode)
                    BindRetAdv(strSelectedSuppCode)
                    BindGoodsRcv(strSelectedSuppCode)
                    BindDispAdv(strSelectedSuppCode)
                    TrLink.Visible = True
                End If
                BindItemCode("")
                BindAccount("")
                BindPreBlock("", "")
                BindBlock("", "")
                BindVehicle("", "")
                BindVehicleExpense(True, "")
            End If
        End If
        'btnCancel.Visible = False
    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.SelectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub

    Sub ddlChargeLevel_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ToggleChargeLevel()
    End Sub

    Sub ToggleChargeLevel()
        If ddlChargeLevel.selectedIndex = 0 Then
            RowBlk.Visible = False
            RowPreBlk.Visible = True
            hidBlockCharge.value = "yes"
        Else
            RowBlk.Visible = True
            RowPreBlk.Visible = False
            hidBlockCharge.value = ""
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblInvoiceRcvTag.Text = GetCaption(objLangCap.EnumLangCap.InvoiceReceive)
        lblInvoiceRcvIDTag.Text = GetCaption(objLangCap.EnumLangCap.InvoiceReceive) & lblID.Text
        lblErrManySelectDoc.Text = lblPleaseSelect.Text & lblInvoiceRcvIDTag.Text & lblDocument.Text
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GRNDET_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=pu/trx/PU_trx_GRNList.aspx")
        End Try

        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

        dgGRNDet.Columns(5).HeaderText = lblAccount.Text
        dgGRNDet.Columns(6).HeaderText = lblBlock.Text
        dgGRNDet.Columns(7).HeaderText = lblVehicle.Text
        dgGRNDet.Columns(8).HeaderText = lblVehExpense.Text

        lblErrAccount.Text = "<BR>" & lblPleaseSelectOne.Text & lblAccount.Text
        lblErrBlock.Text = lblPleaseSelectOne.Text & lblBlock.Text
        lblErrVehicle.Text = lblPleaseSelectOne.Text & lblVehicle.Text
        lblErrVehExp.Text = lblPleaseSelectOne.Text & lblVehExpense.Text
        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTag.Text = PreBlockTag & lblCode.Text & " : "
        lblPreBlockErr.Text = lblPleaseSelectOne.Text & PreBlockTag & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GRNDET_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=pu/setup/PU_trx_GRNList.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If pv_TermCode = objLangCapDs.Tables(0).Rows(count).Item("TermCode") Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub onLoad_Display(ByVal pv_strGRNId As String)
        Dim strOpCd As String = "PU_CLSTRX_GRN_GET"

        Dim strParam As String = pv_strGRNId & "|" & strLocation & "|||||A.GoodsRetId||||"

        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim objGRNDs As New Object()

        Try
            intErrNo = objPU.mtdGetGRN(strOpCd, strParam, objGRNDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_GRN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
        End Try

        If objGRNDs.Tables(0).Rows.Count > 0 Then
            lblGoodsRetId.Text = pv_strGRNId
            lblAccPeriod.Text = objGRNDs.Tables(0).Rows(0).Item("AccMonth") & "/" & objGRNDs.Tables(0).Rows(0).Item("AccYear")
            lblGoodsRetType.Text = objGRNDs.Tables(0).Rows(0).Item("GoodsRetType")
            If objGRNDs.Tables(0).Rows(0).Item("GoodsRetType") = objPU.EnumGRNType.Stock Then
                lblGoodsRet.Text = "Stock / Workshop"
            Else
                lblGoodsRet.Text = objPU.mtdGetGRNType(objGRNDs.Tables(0).Rows(0).Item("GoodsRetType"))
            End If

            strSelectedSuppCode = objGRNDs.Tables(0).Rows(0).Item("SupplierCode").Trim()
            txtSupCode.Text = strSelectedSuppCode
            txtSupName.Text = objGRNDs.Tables(0).Rows(0).Item("SupplierName").Trim()
            lblStatus.Text = objPU.mtdGetGRNStatus(objGRNDs.Tables(0).Rows(0).Item("Status"))
            lblHidStatus.Text = objGRNDs.Tables(0).Rows(0).Item("Status").Trim()
            lblCreateDate.Text = objGlobal.GetLongDate(objGRNDs.Tables(0).Rows(0).Item("CreateDate"))
            lblUpdateDate.Text = objGlobal.GetLongDate(objGRNDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblPrintDate.Text = objGlobal.GetLongDate(objGRNDs.Tables(0).Rows(0).Item("PrintDate"))
            lblUpdateBy.Text = objGRNDs.Tables(0).Rows(0).Item("UserName")
            lblIDTotalAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objGRNDs.Tables(0).Rows(0).Item("TotalAmount"), 2), 2)
            lblTotalAmount.Text = FormatNumber(objGRNDs.Tables(0).Rows(0).Item("TotalAmount"), 2)
            txtRemark.Text = objGRNDs.Tables(0).Rows(0).Item("Remark").Trim()
            BindInventoryBinLevel(Trim(objGRNDs.Tables(0).Rows(0).Item("Bin")))
            txtGoodsRetDate.Text = Date_Validation(objGRNDs.Tables(0).Rows(0).Item("CreateDate"), True)
        End If


        Select Case objGRNDs.Tables(0).Rows(0).Item("Status").Trim()
            Case objPU.EnumGRNStatus.Active
                txtSupCode.Enabled = False
                tblLine.Visible = True
                tblDoc.Visible = True
                tblDoc1.Visible = True
                If lblGoodsRetType.Text = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
                    tblFACode.Visible = True
                Else
                    tblFACode.Visible = False
                End If

                btnSave.Visible = True
                btnConfirm.Visible = True
                btnDelete.Visible = True
                btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                btnUnDelete.Visible = False
                btnCancel.Visible = False
                btnPrint.Visible = True
                txtRemark.Enabled = True
                ddlInventoryBin.Enabled = True
                txtGoodsRetDate.Enabled = True

            Case objPU.EnumGRNStatus.Confirmed
                txtSupCode.Enabled = False
                tblLine.Visible = False
                tblDoc.Visible = False
                tblDoc1.Visible = False
                tblFACode.Visible = False
                tblAcc.Visible = False
                btnSave.Visible = False
                btnConfirm.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Visible = False
                btnPrint.Visible = True
                txtRemark.Enabled = False
                ddlInventoryBin.Enabled = False
                btnCancel.Visible = True
                btnCancel.Attributes("onclick") = "javascript:return ConfirmAction('cancel');"
                txtGoodsRetDate.Enabled = False

            Case objPU.EnumGRNStatus.Deleted
                txtSupCode.Enabled = False
                tblLine.Visible = False
                tblDoc.Visible = False
                tblDoc1.Visible = False
                tblFACode.Visible = False
                tblAcc.Visible = False
                btnSave.Visible = False
                btnConfirm.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                btnUnDelete.Visible = True
                btnCancel.Visible = False
                btnPrint.Visible = False
                txtRemark.Enabled = False
                ddlInventoryBin.Enabled = False
                txtGoodsRetDate.Enabled = False

            Case objPU.EnumGRNStatus.Cancelled, objPU.EnumGRNStatus.Closed
                txtSupCode.Enabled = False
                tblLine.Visible = False
                tblDoc.Visible = False
                tblDoc1.Visible = False
                tblFACode.Visible = False
                tblAcc.Visible = False
                btnSave.Visible = False
                btnConfirm.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Visible = False
                btnCancel.Visible = False
                btnPrint.Visible = False
                txtRemark.Enabled = False
                ddlInventoryBin.Enabled = False
                txtGoodsRetDate.Enabled = False
        End Select
    End Sub

    Sub onLoad_DisplayLn(ByVal pv_strGRNId As String)
        Dim strOpCd As String = "PU_CLSTRX_GRN_LINE_GET"
        Dim objGRNLnDs As New Object()
        Dim strParam As String = pv_strGRNId
        Dim UpdButton As LinkButton
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim dbAmount As Double

        Try
            intErrNo = objPU.mtdGetGRNLn(strOpCd, _
                                         strCompany, _
                                         strLocation, _
                                         strUserId, _
                                         strAccMonth, _
                                         strAccYear, _
                                         strParam, _
                                         objGRNLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_GRNLn&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
        End Try

        For intCnt = 0 To objGRNLnDs.Tables(0).Rows.Count - 1
            dbAmount += objGRNLnDs.Tables(0).Rows(intCnt).Item("OriAmount")

            objGRNLnDs.Tables(0).Rows(intCnt).Item("InvoiceRcvId") = objGRNLnDs.Tables(0).Rows(intCnt).Item("InvoiceRcvId").Trim()
            objGRNLnDs.Tables(0).Rows(intCnt).Item("ItemRetAdvId") = objGRNLnDs.Tables(0).Rows(intCnt).Item("ItemRetAdvId").Trim()
            If IsDBNull(objGRNLnDs.Tables(0).Rows(intCnt).Item("DocID")) Then
                objGRNLnDs.Tables(0).Rows(intCnt).Item("DocID") = ""
            Else
                objGRNLnDs.Tables(0).Rows(intCnt).Item("DocID") = objGRNLnDs.Tables(0).Rows(intCnt).Item("DocID").Trim()
            End If
            objGRNLnDs.Tables(0).Rows(intCnt).Item("ItemCode") = objGRNLnDs.Tables(0).Rows(intCnt).Item("ItemCode").Trim()
            objGRNLnDs.Tables(0).Rows(intCnt).Item("Description") = objGRNLnDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                    objGRNLnDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            objGRNLnDs.Tables(0).Rows(intCnt).Item("AccCode") = objGRNLnDs.Tables(0).Rows(intCnt).Item("AccCode").Trim()
            objGRNLnDs.Tables(0).Rows(intCnt).Item("BlkCode") = objGRNLnDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            objGRNLnDs.Tables(0).Rows(intCnt).Item("VehCode") = objGRNLnDs.Tables(0).Rows(intCnt).Item("VehCode").Trim()
            objGRNLnDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = objGRNLnDs.Tables(0).Rows(intCnt).Item("VehExpenseCode").Trim()
            objGRNLnDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") = objGRNLnDs.Tables(0).Rows(intCnt).Item("PurchaseUOM").Trim()
        Next intCnt

        dgGRNDet.DataSource = objGRNLnDs.Tables(0)
        dgGRNDet.DataBind()

        lblIDTotalAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dbAmount, 2), 2)
        lblTotalAmount.Text = FormatNumber(dbAmount, 2)

        For intCnt = 0 To objGRNLnDs.Tables(0).Rows.Count - 1
            Select Case lblStatus.Text
                Case objPU.mtdGetGRNStatus(objPU.EnumGRNStatus.Confirmed), objPU.mtdGetGRNStatus(objPU.EnumGRNStatus.Deleted), objPU.mtdGetGRNStatus(objPU.EnumGRNStatus.Cancelled), objPU.mtdGetGRNStatus(objPU.EnumGRNStatus.Closed)
                    UpdButton = dgGRNDet.Items.Item(intCnt).FindControl("lbDelete")
                    UpdButton.Visible = False
                Case Else
                    UpdButton = dgGRNDet.Items.Item(intCnt).FindControl("lbDelete")
                    UpdButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            End Select
        Next intCnt

        If objGRNLnDs.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
        Else
            TrLink.Visible = False
        End If
    End Sub

    'Sub BindSupp(ByVal pv_strGRNId As String)
    '    Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_GET"
    '    Dim objSuppDs As New Object()
    '    Dim strSuppCode As String
    '    Dim strParam As String = ""
    '    Dim intCnt As Integer = 0
    '    Dim intErrNo As Integer
    '    Dim dr As DataRow
    '    Dim intSelectedIndex As Integer
    '    Dim strSuppType As String = objPUSetup.EnumSupplierType.Internal & "','" & objPUSetup.EnumSupplierType.External & "','" & objPUSetup.EnumSupplierType.Associate & "','" & objPUSetup.EnumSupplierType.Contractor

    '    strSuppCode = IIf(pv_strGRNId = "", "", strSelectedSuppCode)
    '    strParam = strSuppCode & "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||SELECT"
    '    strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")
    '    strParam = strParam & "|" & strSuppType

    '    Try
    '        intErrNo = objPUSetup.mtdGetSupplier(strOpCd, strParam, objSuppDs)
    '    Catch Exp As System.Exception
    '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_SUPPLIER&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
    '    End Try

    '    For intCnt = 0 To objSuppDs.Tables(0).Rows.Count - 1
    '        objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode").Trim()
    '        objSuppDs.Tables(0).Rows(intCnt).Item("Name") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") & " (" & objSuppDs.Tables(0).Rows(intCnt).Item("Name").Trim() & ")"

    '        If objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = strSelectedSuppCode Then
    '            intSelectedIndex = intCnt + 1
    '        End If
    '    Next intCnt

    '    dr = objSuppDs.Tables(0).NewRow()
    '    dr("SupplierCode") = ""
    '    dr("Name") = "Please select Supplier Code"
    '    objSuppDs.Tables(0).Rows.InsertAt(dr, 0)

    '    ddlSuppCode.DataSource = objSuppDs.Tables(0)
    '    ddlSuppCode.DataValueField = "SupplierCode"
    '    ddlSuppCode.DataTextField = "Name"
    '    ddlSuppCode.DataBind()
    '    ddlSuppCode.SelectedIndex = intSelectedIndex
    '    strSelectedSuppCode = ddlSuppCode.SelectedItem.Value
    'End Sub

    'Sub SuppIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    strSelectedSuppCode = ddlSuppCode.SelectedItem.Value
    '    BindInvRcv(ddlSuppCode.SelectedItem.Value)
    '    BindRetAdv(ddlSuppCode.SelectedItem.Value)
    '    BindGoodsRcv(ddlSuppCode.SelectedItem.Value)
    '    BindDispAdv(ddlSuppCode.SelectedItem.Value)

    '    ddlInvRcvId.Enabled = True
    '    ddlInvRcvId.SelectedIndex = -1
    '    ddlRetAdvId.Enabled = True
    '    ddlRetAdvId.SelectedIndex = -1
    '    ddlGoodsRcvId.Enabled = True
    '    ddlGoodsRcvId.SelectedIndex = -1
    '    ddlDispAdvID.Enabled = True
    '    ddlDispAdvID.SelectedIndex = -1
    '    ddlItemCode.Enabled = True
    '    ddlItemCode.SelectedIndex = -1
    '    If lblGoodsRetType.Text = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
    '        tblFACode.Visible = True
    '        lblAssetCode.Text = ""
    '    End If

    '    lblQtyOnHand.Text = ""
    '    txtQty.Text = ""
    '    txtCost.Text = ""

    '    txtCost.Enabled = True
    '    lblIDQtyOnHand.Text = ""
    '    lblInvisibleCost.Text = ""
    '    RegularExpressionValidatorCost.Enabled = True
    'End Sub

    Sub BindInvRcv(ByVal pv_strSuppCode As String)
        Dim strOpCd As String = "PU_CLSTRX_GRN_INVOICERCV_GET"
        Dim objInvRcvDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParam = pv_strSuppCode & "|" & _
                   objAP.EnumInvoiceRcvStatus.Confirmed & "|" & _
                   objAP.EnumInvoiceRcvStatus.Closed & "','" & objAP.EnumInvoiceRcvStatus.Paid & "|InvoiceRcvId||"

        Try
            intErrNo = objPU.mtdGetRetDoc(strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strOpCd, _
                                        strParam, _
                                        objInvRcvDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_INVOICE&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
        End Try

        For intCnt = 0 To objInvRcvDs.Tables(0).Rows.Count - 1
            objInvRcvDs.Tables(0).Rows(intCnt).Item("InvoiceRcvId") = objInvRcvDs.Tables(0).Rows(intCnt).Item("InvoiceRcvId").Trim()
            objInvRcvDs.Tables(0).Rows(intCnt).Item("DispInvoiceRcvId") = objInvRcvDs.Tables(0).Rows(intCnt).Item("InvoiceRcvId").Trim()

            If objInvRcvDs.Tables(0).Rows(intCnt).Item("InvoiceRcvId") = strSelectedInvRcvId Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        If txtSupCode.Text = "" Then
            objInvRcvDs.Tables(0).Clear()
        End If

        dr = objInvRcvDs.Tables(0).NewRow()
        dr("InvoiceRcvId") = ""
        dr("DispInvoiceRcvId") = lblPleaseSelect.Text & lblInvoiceRcvIDTag.Text
        objInvRcvDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlInvRcvId.DataSource = objInvRcvDs.Tables(0)
        ddlInvRcvId.DataValueField = "InvoiceRcvId"
        ddlInvRcvId.DataTextField = "DispInvoiceRcvId"
        ddlInvRcvId.DataBind()
        ddlInvRcvId.SelectedIndex = intSelectedIndex
        strSelectedInvRcvId = ddlInvRcvId.SelectedItem.Value
    End Sub

    Sub InvRcvIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedInvRcvId = ddlInvRcvId.SelectedItem.Value

        If ddlInvRcvId.SelectedItem.Text = lblPleaseSelect.Text & lblInvoiceRcvIDTag.Text Then
            ddlGoodsRcvId.Enabled = True
            ddlRetAdvId.Enabled = True
            ddlDispAdvID.Enabled = True
            If lblGoodsRetType.Text = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
                tblFACode.Visible = True
                lblAssetCode.Text = ""
            End If
            lblQtyOnHand.Text = ""
            txtQty.Text = ""
            txtCost.Text = ""
            txtCost.Enabled = True
            lblIDQtyOnHand.Text = ""
            lblInvisibleCost.Text = ""
            RegularExpressionValidatorCost.Enabled = True
        Else
            CtrlGoodsRetDocType()
        End If

    End Sub

    Sub BindGoodsRcv(ByVal pv_strSuppCode As String)
        Dim strOpCd As String = "PU_CLSTRX_GRN_GOODSRCV_GET"
        Dim objGoodsRcvDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParam = pv_strSuppCode & "|" & _
                   objPU.EnumGRStatus.Confirmed & "|" & _
                   objPU.EnumGRStatus.Closed & "|x.GoodsRcvId||"
        Try
            intErrNo = objPU.mtdGetRetDoc(strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strOpCd, _
                                        strParam, _
                                        objGoodsRcvDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GRN_GOODSRCV_GET&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
        End Try

        For intCnt = 0 To objGoodsRcvDs.Tables(0).Rows.Count - 1
            objGoodsRcvDs.Tables(0).Rows(intCnt).Item("GoodsRcvId") = objGoodsRcvDs.Tables(0).Rows(intCnt).Item("GoodsRcvId").Trim()
            objGoodsRcvDs.Tables(0).Rows(intCnt).Item("DispGoodsRcvId") = objGoodsRcvDs.Tables(0).Rows(intCnt).Item("GoodsRcvId").Trim()

            If objGoodsRcvDs.Tables(0).Rows(intCnt).Item("GoodsRcvId") = strSelectedGoodsRcvId Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        If txtSupCode.Text.Trim = "" Then
            objGoodsRcvDs.Tables(0).Clear()
        End If

        dr = objGoodsRcvDs.Tables(0).NewRow()
        dr("GoodsRcvId") = ""
        dr("DispGoodsRcvId") = "Please select Goods Received ID"
        objGoodsRcvDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlGoodsRcvId.DataSource = objGoodsRcvDs.Tables(0)
        ddlGoodsRcvId.DataValueField = "GoodsRcvId"
        ddlGoodsRcvId.DataTextField = "DispGoodsRcvId"
        ddlGoodsRcvId.DataBind()
        ddlGoodsRcvId.SelectedIndex = intSelectedIndex
        strSelectedGoodsRcvId = ddlGoodsRcvId.SelectedItem.Value
    End Sub

    Sub GoodsRcvIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedGoodsRcvId = ddlGoodsRcvId.SelectedItem.Value

        If ddlGoodsRcvId.SelectedItem.Text = "Please select Goods Received ID" Then
            ddlInvRcvId.Enabled = True
            ddlRetAdvId.Enabled = True
            ddlDispAdvID.Enabled = True
            If lblGoodsRetType.Text = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
                tblFACode.Visible = True
                lblAssetCode.Text = ""
            End If
            lblQtyOnHand.Text = ""
            txtQty.Text = ""
            txtCost.Text = ""
            txtCost.Enabled = True
            lblIDQtyOnHand.Text = ""
            lblInvisibleCost.Text = ""
            RegularExpressionValidatorCost.Enabled = True
        Else
            CtrlGoodsRetDocType()
        End If

    End Sub

    Sub BindRetAdv(ByVal pv_strSuppCode As String)
        Dim strOpCd As String = "PU_CLSTRX_GRN_ITEMRETADV_GET"
        Dim objItemRetAdvDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim strGoodsRetType As String

        If lblGoodsRetType.Text = objPU.EnumGRNType.Stock Or lblGoodsRetType.Text = objPU.EnumGRNType.DirectCharge Then
            strGoodsRetType = objGlobal.EnumDocType.StockReturnAdvice
        ElseIf lblGoodsRetType.Text = objPU.EnumGRNType.Canteen Then
            strGoodsRetType = objGlobal.EnumDocType.CanteenReturnAdvice
        End If

        strParam = "|" & objIN.EnumGoodRetAdvStatus.Confirmed & "||ItemRetAdvId||" & strGoodsRetType

        Try
            intErrNo = objPU.mtdGetRetDoc(strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strOpCd, _
                                        strParam, _
                                        objItemRetAdvDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_ITEMRETADV&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
        End Try

        For intCnt = 0 To objItemRetAdvDs.Tables(0).Rows.Count - 1
            objItemRetAdvDs.Tables(0).Rows(intCnt).Item("ItemRetAdvId") = objItemRetAdvDs.Tables(0).Rows(intCnt).Item("ItemRetAdvId").Trim()
            objItemRetAdvDs.Tables(0).Rows(intCnt).Item("DispItemRetAdvId") = objItemRetAdvDs.Tables(0).Rows(intCnt).Item("ItemRetAdvId").Trim()

            If objItemRetAdvDs.Tables(0).Rows(intCnt).Item("ItemRetAdvId") = strSelectedRetAdvId Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        If txtSupCode.Text = "" Then
            objItemRetAdvDs.Tables(0).Clear()
        End If

        dr = objItemRetAdvDs.Tables(0).NewRow()
        dr("ItemRetAdvId") = ""
        dr("DispItemRetAdvId") = "Please select Stock Return Advice ID"
        objItemRetAdvDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlRetAdvId.DataSource = objItemRetAdvDs.Tables(0)
        ddlRetAdvId.DataValueField = "ItemRetAdvId"
        ddlRetAdvId.DataTextField = "DispItemRetAdvId"
        ddlRetAdvId.DataBind()
        ddlRetAdvId.SelectedIndex = intSelectedIndex
        strSelectedInvRcvId = ddlRetAdvId.SelectedItem.Value
    End Sub

    Sub RetAdvIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedRetAdvId = ddlRetAdvId.SelectedItem.Value
        If ddlRetAdvId.SelectedItem.Text = "Please select Stock Return Advice ID" Then
            ddlInvRcvId.Enabled = True
            ddlGoodsRcvId.Enabled = True
            ddlDispAdvID.Enabled = True
            If lblGoodsRetType.Text = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
                tblFACode.Visible = True
                lblAssetCode.Text = ""
            End If
            lblQtyOnHand.Text = ""
            txtQty.Text = ""
            txtCost.Text = ""
            txtCost.Enabled = True
            lblIDQtyOnHand.Text = ""
            lblInvisibleCost.Text = ""
            RegularExpressionValidatorCost.Enabled = True
        Else
            CtrlGoodsRetDocType()
        End If
    End Sub

    Function CtrlGoodsRetDocType()
        If strSelectedInvRcvId <> "" Then
            ddlGoodsRcvId.Enabled = False
            ddlRetAdvId.Enabled = False
            ddlInvRcvId.Enabled = True
            BindRetItem(ddlInvRcvId.SelectedItem.Value, "")
        ElseIf strSelectedRetAdvId <> "" Then
            ddlGoodsRcvId.Enabled = False
            ddlInvRcvId.Enabled = False
            ddlRetAdvId.Enabled = True
            BindRetItem("", ddlRetAdvId.SelectedItem.Value)
        ElseIf strSelectedGoodsRcvId <> "" Then
            ddlInvRcvId.Enabled = False
            ddlRetAdvId.Enabled = False
            ddlGoodsRcvId.Enabled = True
            BindGRRetItem(ddlGoodsRcvId.SelectedItem.Value)
        ElseIf strSelectedDispAdvId <> "" Then
            ddlInvRcvId.Enabled = False
            ddlRetAdvId.Enabled = False
            ddlGoodsRcvId.Enabled = False
            ddlDispAdvID.Enabled = True
            BindDispItem(ddlDispAdvID.SelectedItem.Value)
        Else
            BindItemCode("")
        End If
    End Function

    Sub BindRetItem(ByVal pv_strInvRcvId As String, ByVal pv_strItemRetAdvId As String)
        Dim strOpCd_GetInvRcv As String = "PU_CLSTRX_GRN_INVOICERCV_LINE_GET"
        Dim strOpCd_GetItemRetAdv As String = "PU_CLSTRX_GRN_ITEMRETADV_LINE_GET"
        Dim objRetItemDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim strGRNType As String
        Dim POLnID As String

        If lblGoodsRetType.Text = objPU.EnumGRNType.Stock Then
            strGRNType = objIN.EnumPurReqDocType.StockPR & "','" & objIN.EnumPurReqDocType.WorkshopPR
        Else
            strGRNType = lblGoodsRetType.Text
        End If

        strParam = pv_strInvRcvId & "|" & pv_strItemRetAdvId & "|" & strGRNType & "|" & lblGoodsRetId.Text.Trim
        Try
            intErrNo = objPU.mtdGetRetItem(strCompany, _
                                           strLocation, _
                                           strUserId, _
                                           strAccMonth, _
                                           strAccYear, _
                                           strOpCd_GetInvRcv, _
                                           strOpCd_GetItemRetAdv, _
                                           strParam, _
                                           objRetItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_RETURNITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
        End Try

        lblQtyOnHand.Text = ""
        lblIDQtyOnHand.Text = ""

        For intCnt = 0 To objRetItemDs.Tables(0).Rows.Count - 1

            If lblGoodsRetType.Text = objPU.EnumGRNType.Stock Then
                If objRetItemDs.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then
                    objRetItemDs.Tables(0).Rows(intCnt).Item("Description") = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                              objRetItemDs.Tables(0).Rows(intCnt).Item("PartNo") & " (" & _
                                                                              objRetItemDs.Tables(0).Rows(intCnt).Item("Description") & ")"
                    objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                            objRetItemDs.Tables(0).Rows(intCnt).Item("PartNo")
                Else
                    objRetItemDs.Tables(0).Rows(intCnt).Item("Description") = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                              objRetItemDs.Tables(0).Rows(intCnt).Item("Description") & ")"
                End If
            Else
                objRetItemDs.Tables(0).Rows(intCnt).Item("Description") = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                          objRetItemDs.Tables(0).Rows(intCnt).Item("Description") & ")"
            End If

            If objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = strSelectedItemCode Then
                intSelectedIndex = intCnt + 1
                lblQtyOnHand.Text = objPU.mtdGetQty(Trim(strLocation), _
                                                    Trim(strSelectedItemCode), _
                                                    False, _
                                                    objRetItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"))
                lblIDQtyOnHand.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Convert.ToDouble(lblQtyOnHand.Text), 5)
                strItemType = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemType")
                hidItemType.Value = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemType").Trim()


                txtCost.Enabled = False
                lblInvisibleCost.Text = objRetItemDs.Tables(0).Rows(intCnt).Item("Cost")
                txtCost.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(CDbl(lblInvisibleCost.Text), 0))
                RegularExpressionValidatorCost.Enabled = False
                hidOriCost.Value = objRetItemDs.Tables(0).Rows(intCnt).Item("Cost")
                POLnID = objRetItemDs.Tables(0).Rows(intCnt).Item("POLnId")
                hidPOLNID.Value = objRetItemDs.Tables(0).Rows(intCnt).Item("POLnId")
            End If
        Next intCnt

        If txtSupCode.Text.Trim = "" Then
            objRetItemDs.Tables(0).Clear()
        End If

        dr = objRetItemDs.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = "Please select Item Code"
        objRetItemDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlItemCode.DataSource = objRetItemDs.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex

        If ddlItemCode.SelectedIndex = 0 Then
            tblAcc.Visible = False
        Else
            If strItemType = objPU.EnumGRNType.DirectCharge Then
                tblAcc.Visible = True
                FindAcc.Visible = True
            Else
                tblAcc.Visible = False
            End If
        End If
    End Sub

    Sub BindItemCode(ByVal pv_strItemCode As String)
        Dim strOpCd As String = "IN_CLSTRX_ITEMPART_ITEM_GET"
        Dim objItemDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim strGRNType As String

        If lblGoodsRetType.Text = objPU.EnumGRNType.Stock Then
            strGRNType = objIN.EnumPurReqDocType.StockPR & "','" & objIN.EnumPurReqDocType.WorkshopPR
        Else
            strGRNType = lblGoodsRetType.Text
        End If

        strParam = strGRNType & "|" & strLocation & "|itm.ItemCode"

        Try
            If lblGoodsRetType.Text = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
                intErrNo = objPU.mtdGetGRNFAItem(strOpCd, strParam, objItemDs)
            Else
                intErrNo = objPU.mtdGetGRNDCItem(strOpCd, strParam, objItemDs)
            End If

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GRNDET_GET_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
        End Try

        lblQtyOnHand.Text = ""
        lblIDQtyOnHand.Text = ""

        'For intCnt = 0 To objItemDs.Tables(0).Rows.Count - 1

        '    If lblGoodsRetType.Text = objPU.EnumGRNType.Stock Then
        '        If objItemDs.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then

        '            objItemDs.Tables(0).Rows(intCnt).Item("Description") = objItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " @ " & _
        '                                                                   objItemDs.Tables(0).Rows(intCnt).Item("PartNo") & " (" & _
        '                                                                   objItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
        '                                                                   objItemDs.Tables(0).Rows(intCnt).Item("LocCode") & ", " & _
        '                                                                   "Rp. " & objGlobal.GetIDDecimalSeparator(objItemDs.Tables(0).Rows(intCnt).Item("LatestCost")) & ", " & _
        '                                                                     objGlobal.GetIDDecimalSeparator_FreeDigit(objItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
        '                                                                   objItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")
        '        Else

        '            objItemDs.Tables(0).Rows(intCnt).Item("Description") = objItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
        '                                                                   objItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
        '                                                                   objItemDs.Tables(0).Rows(intCnt).Item("LocCode") & ", " & _
        '                                                                   "Rp. " & objGlobal.GetIDDecimalSeparator(objItemDs.Tables(0).Rows(intCnt).Item("LatestCost")) & ", " & _
        '                                                                     objGlobal.GetIDDecimalSeparator_FreeDigit(objItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
        '                                                                   objItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")
        '        End If
        '    Else

        '        objItemDs.Tables(0).Rows(intCnt).Item("Description") = objItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
        '                                                               objItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
        '                                                               objItemDs.Tables(0).Rows(intCnt).Item("LocCode") & ", " & _
        '                                                               "Rp. " & objGlobal.GetIDDecimalSeparator(objItemDs.Tables(0).Rows(intCnt).Item("LatestCost")) & ", " & _
        '                                                                     objGlobal.GetIDDecimalSeparator_FreeDigit(objItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
        '                                                               objItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")
        '    End If

        '    If objItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = strSelectedItemCode Then
        '        intSelectedIndex = intCnt + 1
        '        lblQtyOnHand.Text = objPU.mtdGetQty(Trim(strLocation), _
        '                                           Trim(strSelectedItemCode), _
        '                                           False, _
        '                                           objItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"))
        '        lblIDQtyOnHand.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Convert.ToDouble(lblQtyOnHand.Text), 5) 
        '        strItemType = objItemDs.Tables(0).Rows(intCnt).Item("ItemType")
        '        hidItemType.Value = Trim(objItemDs.Tables(0).Rows(intCnt).Item("ItemType")) 
        '        If lblGoodsRetType.Text = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
        '            tblFACode.Visible = True
        '            lblAssetCode.Text = "" 'Fixed Asset code only available for item involving Goods Receive transaction."
        '        End If

        '    End If
        'Next intCnt

        dr = objItemDs.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = "Please select Item Code"
        objItemDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlItemCode.DataSource = objItemDs.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex

        If ddlItemCode.SelectedIndex = 0 Then
            tblAcc.Visible = False
        Else
            If strItemType = objPU.EnumGRNType.DirectCharge Then
                tblAcc.Visible = True
                FindAcc.Visible = True
            Else
                tblAcc.Visible = False
            End If
        End If

    End Sub

    Sub ItemIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedSuppCode = txtSupCode.Text
        strSelectedItemCode = ddlItemCode.SelectedItem.Value

        If ddlItemCode.SelectedItem.Text = "Please select Item Code" Then
            ddlInvRcvId.Enabled = True
            ddlInvRcvId.SelectedIndex = -1

            ddlRetAdvId.Enabled = True
            ddlRetAdvId.SelectedIndex = -1

            ddlGoodsRcvId.Enabled = True
            ddlGoodsRcvId.SelectedIndex = -1

            ddlDispAdvID.Enabled = True
            ddlDispAdvID.SelectedIndex = -1

            If lblGoodsRetType.Text = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
                tblFACode.Visible = True
                lblAssetCode.Text = ""
            End If
            lblQtyOnHand.Text = ""
            txtQty.Text = ""
            txtCost.Text = ""
            txtCost.Enabled = True
            lblIDQtyOnHand.Text = ""
            lblInvisibleCost.Text = ""
            RegularExpressionValidatorCost.Enabled = True
        End If

        If ddlInvRcvId.SelectedItem.Value = "" And ddlRetAdvId.SelectedItem.Value = "" And ddlGoodsRcvId.SelectedItem.Value = "" And ddlDispAdvID.SelectedItem.Value = "" Then
            BindItemCode(strSelectedItemCode)
        Else
            If ddlInvRcvId.SelectedItem.Value <> "" Then
                BindRetItem(ddlInvRcvId.SelectedItem.Value, "")
            ElseIf ddlRetAdvId.SelectedItem.Value <> "" Then
                BindRetItem("", ddlRetAdvId.SelectedItem.Value)
            ElseIf ddlGoodsRcvId.SelectedItem.Value <> "" Then
                BindGRRetItem(ddlGoodsRcvId.SelectedItem.Value)
            ElseIf ddlDispAdvID.SelectedItem.Value <> "" Then
                BindDispItem(ddlDispAdvID.SelectedItem.Value)
            End If
        End If
    End Sub

    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = lblPleaseSelect.Text & lblAccount.Text & lblCode.Text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = objAccDs.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "_Description"
        ddlAccount.DataBind()
        ddlAccount.SelectedIndex = intSelectedIndex
        ddlAccount.AutoPostBack = True
    End Sub

    Sub onSelect_Account(ByVal Sender As Object, ByVal E As EventArgs)
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim intNurseryInd As Integer

        GetAccountDetails(ddlAccount.SelectedItem.Value, blnIsBalanceSheet, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers, intNurseryInd)

        If Not blnIsBalanceSheet Then
            If blnIsBlockRequire Then
                BindPreBlock(ddlAccount.SelectedItem.Value, ddlPreBlock.SelectedItem.Value)
                BindBlock(ddlAccount.SelectedItem.Value, ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            Else
                BindPreBlock("", ddlPreBlock.SelectedItem.Value)
                BindBlock("", ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            End If
            If blnIsVehicleRequire Then
                BindVehicle(ddlAccount.SelectedItem.Value, ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(False, ddlVehExpCode.SelectedItem.Value)
            End If
            If blnIsOthers Then
                lblVehicleOption.Text = True
                BindVehicle("%", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(False, ddlVehExpCode.SelectedItem.Value)
            Else
                lblVehicleOption.Text = False
            End If
        Else
            If blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
                BindPreBlock(ddlAccount.SelectedItem.Value, ddlPreBlock.SelectedItem.Value)
                BindBlock(ddlAccount.SelectedItem.Value, ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            ElseIf blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.No Then
                BindPreBlock("", ddlPreBlock.SelectedItem.Value)
                BindBlock("", ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            End If
        End If
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_IsBalanceSheet As Boolean, _
                          ByRef pr_IsBlockRequire As Boolean, _
                          ByRef pr_IsVehicleRequire As Boolean, _
                          ByRef pr_IsOthers As Boolean, _
                          ByRef pr_strNurseryInd As Integer)

        Dim _objAccDs As New Object
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            pr_IsBalanceSheet = False
            pr_IsBlockRequire = False
            pr_IsVehicleRequire = False
            pr_IsOthers = False
            pr_strNurseryInd = False
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            If CInt(_objAccDs.Tables(0).Rows(0).Item("AccType")) = objGLSetup.EnumAccountType.BalanceSheet Then
                pr_IsBalanceSheet = True
                If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd")) = objGLSetup.EnumNurseryAccount.Yes Then
                    pr_strNurseryInd = objGLSetup.EnumNurseryAccount.Yes
                End If
            End If
            If CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.NonVehicle Then
                pr_IsBlockRequire = True
            ElseIf CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.VehicleDistribution Then
                pr_IsVehicleRequire = True
            ElseIf CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.Others Then
                pr_IsBlockRequire = True
                pr_IsOthers = True
            End If
        End If
    End Sub

    Sub BindPreBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strOpCd = "GL_CLSSETUP_ACCOUNT_PREBLOCK_GET"
        intSelectedIndex = 0
        Try
            strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("_Description") = lblPleaseSelect.Text & PreBlockTag & lblCode.Text

        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlPreBlock.DataSource = objBlkDs.Tables(0)
        ddlPreBlock.DataValueField = "BlkCode"
        ddlPreBlock.DataTextField = "_Description"
        ddlPreBlock.DataBind()
        ddlPreBlock.SelectedIndex = intSelectedIndex

        If Not objBlkDs Is Nothing Then
            objBlkDs = Nothing
        End If
    End Sub

    Sub BindBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                strOpCd = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = pv_strBlkCode.Trim Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("_Description") = lblPleaseSelect.Text & lblBlock.Text & lblCode.Text
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "_Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindVehicle(ByVal pv_strAccCode As String, ByVal pv_strVehCode As String)
        Dim strOpCd As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"
            strParam = "|AccCode = '" & pv_strAccCode & "' AND LocCode = '" & Session("SS_LOCATION") & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'"
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   objVehDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objVehDs.Tables(0).Rows.Count - 1
            objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = objVehDs.Tables(0).Rows(intCnt).Item("VehCode").Trim()
            objVehDs.Tables(0).Rows(intCnt).Item("Description") = objVehDs.Tables(0).Rows(intCnt).Item("VehCode") & " (" & objVehDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(pv_strVehCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objVehDs.Tables(0).NewRow()
        dr("VehCode") = ""
        dr("Description") = lblPleaseSelect.Text & lblVehicle.Text
        objVehDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehCode.DataSource = objVehDs.Tables(0)
        ddlVehCode.DataValueField = "VehCode"
        ddlVehCode.DataTextField = "Description"
        ddlVehCode.DataBind()
        ddlVehCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindVehicleExpense(ByVal pv_IsBlankList As Boolean, ByVal pv_strVehExpCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim strParam As String = "Order By VehExpenseCode ASC| And Veh.Status = '" & objGLSetup.EnumVehExpenseStatus.Active & "' "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            If pv_IsBlankList Then
                strParam += "And Veh.VehExpensecode = ''"
            End If
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.VehicleExpense, _
                                                   objVehExpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEHEXPENSE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objVehExpDs.Tables(0).Rows.Count - 1
            objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode").Trim()
            objVehExpDs.Tables(0).Rows(intCnt).Item("Description") = objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") & " (" & objVehExpDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(pv_strVehExpCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objVehExpDs.Tables(0).NewRow()
        dr("VehExpenseCode") = ""
        dr("Description") = lblPleaseSelect.Text & lblVehExpense.Text
        objVehExpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehExpCode.DataSource = objVehExpDs.Tables(0)
        ddlVehExpCode.DataValueField = "VehExpenseCode"
        ddlVehExpCode.DataTextField = "Description"
        ddlVehExpCode.DataBind()
        ddlVehExpCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strOpCd_AddGRN As String = "PU_CLSTRX_GRN_ADD"
        Dim strOpCd_AddGRNLn As String = "PU_CLSTRX_GRN_LINE_ADD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_GetGRNLn As String = "PU_CLSTRX_GRN_LINE_GET"
        Dim strOpCd_UpdGRN As String = "PU_CLSTRX_GRN_UPD"
        Dim objGRNId As Object
        Dim objGRNLnId As Object
        Dim strParam As String = ""
        Dim strParamLn As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strGRNId As String
        Dim strSuppCode As String = txtSupCode.Text
        Dim strItemCode As String = ddlItemCode.SelectedItem.Value
        Dim dblAmount As Double
        Dim dblTotalAmount As Double
        Dim intInvRcvId As Integer
        Dim intRetAdvId As Integer
        Dim intItemCode As Integer
        Dim intGoodsRcvId As Integer
        Dim intDispAdvId As Integer
        Dim intErrorCheck As Integer
        Dim intNurseryInd As Integer
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strPreBlk As String = Request.Form("ddlPreBlock")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strNewIDFormat As String
        Dim strDate As String = Date_Validation(txtGoodsRetDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRetDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If strSuppCode = "" Then
            lblSuppCode.Visible = True
            Exit Sub
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        lblInvisibleCost.Text = IIf(TRIM(lblInvisibleCost.Text) = "", TRIM(txtCost.Text), lblInvisibleCost.Text)

        intInvRcvId = IIf(ddlInvRcvId.SelectedItem.Value = "", 0, 1)
        intRetAdvId = IIf(ddlRetAdvId.SelectedItem.Value = "", 0, 1)
        intItemCode = IIf(ddlItemCode.SelectedItem.Value = "", 0, 1)
        intGoodsRcvId = IIf(ddlGoodsRcvId.SelectedItem.Value = "", 0, 1)
        intDispAdvId = IIf(ddlDispAdvID.SelectedItem.Value = "", 0, 1)

        If (intInvRcvId + intRetAdvId + intItemCode + intGoodsRcvId + intDispAdvId) = 0 Then
            lblErrManySelectDoc.ForeColor = Red
            Exit Sub
        ElseIf (intInvRcvId + intRetAdvId + intGoodsRcvId + intDispAdvId) > 1 Then
            lblErrManySelectDoc.ForeColor = Red
            Exit Sub
        ElseIf intItemCode = 0 Then
            lblErrManySelectDoc.ForeColor = Red
            Exit Sub
        End If

        If ddlDispAdvID.SelectedIndex <> 0 Then
            ddlInventoryBin.SelectedItem.Value = objINSetup.EnumInventoryBinLevel.Central
        End If

        If ddlItemCode.SelectedIndex = 0 Then
        Else
            If InStr(strItemCode, ITEM_PART_SEPERATOR) <> 0 Then
                strItemCode = Trim(Mid(strItemCode, 1, InStr(strItemCode, ITEM_PART_SEPERATOR) - 1))
            End If
        End If

        strGRNId = IIf(lblGoodsRetId.Text = "", "", lblGoodsRetId.Text)

        GetAccountDetails(ddlAccount.SelectedItem.Value, blnIsBalanceSheet, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers, intNurseryInd)


        If blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
            If ddlChargeLevel.SelectedIndex = 0 Then
                If strPreBlk = "" Then
                    lblPreBlockErr.Visible = True
                    Exit Sub
                End If
            Else
                If strBlk = "" Then
                    lblErrBlock.Visible = True
                    Exit Sub
                End If
            End If
        End If

        If Not blnIsBalanceSheet Then
            If hidItemType.Value = objINSetup.EnumInventoryItemType.DirectCharge Then
                If ddlAccount.SelectedItem.Value = "" Then
                    lblErrAccount.Visible = True
                    Exit Sub
                ElseIf ddlBlock.SelectedItem.Value = "" And ddlChargeLevel.SelectedIndex = 1 And blnIsBlockRequire = True Then
                    lblErrBlock.Visible = True
                    Exit Sub
                ElseIf ddlPreBlock.SelectedItem.Value = "" And ddlChargeLevel.SelectedIndex = 0 And blnIsBlockRequire = True Then
                    lblPreBlockErr.Visible = True
                    Exit Sub
                ElseIf ddlVehCode.SelectedItem.Value = "" And blnIsVehicleRequire = True Then
                    lblErrVehicle.Visible = True
                    Exit Sub
                ElseIf ddlVehExpCode.SelectedItem.Value = "" And blnIsVehicleRequire = True Then
                    lblErrVehExp.Visible = True
                    Exit Sub
                ElseIf ddlVehCode.SelectedItem.Value <> "" And ddlVehExpCode.SelectedItem.Value = "" And lblVehicleOption.Text = True Then
                    lblErrVehExp.Visible = True
                    Exit Sub
                ElseIf ddlVehCode.SelectedItem.Value = "" And ddlVehExpCode.SelectedItem.Value <> "" And lblVehicleOption.Text = True Then
                    lblErrVehicle.Visible = True
                    Exit Sub
                End If
            End If
        End If

        'If Month(strDate) < strAccMonth And Year(strDate) <= strAccYear Then
        '    lblDate.Visible = True
        '    lblDate.Text = "Invalid transaction date."
        '    Exit Sub
        'End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strNewIDFormat = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReturn) & Right(strAccYear, 2) & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth)
        '    Case Else
        '        strNewIDFormat = "NRP" & "/" & strCompany & "/" & strLocation & "/" & Trim(lblGoodsRetType.Text) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        If strLocLevel = "3" And strLocType = "4" Then
            strNewIDFormat = "NRP" & "/" & strCompany & "/" & strLocation & "/" & Trim(lblGoodsRetType.Text) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        Else
            strNewIDFormat = "NRP" & "/" & strCompany & "/" & strLocation & "/" & Trim(lblGoodsRetType.Text) & "L/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        End If
        
        If Trim(ddlDispAdvID.SelectedItem.Value) = "" Then
            If CDbl(txtQty.Text) > 0 And (CDbl(txtQty.Text) <= CDbl(lblQtyOnHand.Text)) Then
                lblErrQtyReturn.Visible = False
                If intGoodsRcvId = 1 Then
                    If (CDbl(txtQty.Text) > CDbl(lblRcvQty.Text)) Then
                        lblErrRcvQty.Visible = True
                        Exit Sub
                    End If
                Else
                    lblErrRcvQty.Visible = False
                End If
                dblAmount = FormatNumber((CDbl(txtQty.Text) * CDbl(lblInvisibleCost.Text)), 0)
                dblTotalAmount += dblAmount

                strParamLn = strGRNId & "|" & _
                             ddlInvRcvId.SelectedItem.Value & "|" & _
                             ddlRetAdvId.SelectedItem.Value & "|" & _
                             strItemCode & "|" & _
                             txtQty.Text & "|" & _
                             lblInvisibleCost.Text & "|" & _
                             dblAmount & "|" & _
                             ddlAccount.SelectedItem.Value & "|" & _
                             IIf(ddlChargeLevel.SelectedIndex = 0, ddlPreBlock.SelectedItem.Value, ddlBlock.SelectedItem.Value) & "|" & _
                             ddlVehCode.SelectedItem.Value & "|" & _
                             ddlVehExpCode.SelectedItem.Value & "|" & _
                             lblAssetCode.Text & "|" & _
                             ddlGoodsRcvId.SelectedItem.Value & "|" & _
                             ddlDispAdvID.SelectedItem.Value & "|" & _
                             hidOriCost.Value
            Else
                lblErrQtyReturn.Visible = True
                Exit Sub
            End If
        Else
            strNewIDFormat = "NRP" & "/" & strCompany & "/" & Trim(Mid(Trim(ddlDispAdvID.SelectedItem.Value), 9, 3)) & "/" & Trim(lblGoodsRetType.Text) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

            dblAmount = FormatNumber((CDbl(txtQty.Text) * CDbl(lblInvisibleCost.Text)), 2)
            dblTotalAmount += dblAmount

            strParamLn = strGRNId & "|" & _
                         ddlInvRcvId.SelectedItem.Value & "|" & _
                         ddlRetAdvId.SelectedItem.Value & "|" & _
                         strItemCode & "|" & _
                         txtQty.Text & "|" & _
                         lblInvisibleCost.Text & "|" & _
                         dblAmount & "|" & _
                         ddlAccount.SelectedItem.Value & "|" & _
                         IIf(ddlChargeLevel.SelectedIndex = 0, ddlPreBlock.SelectedItem.Value, ddlBlock.SelectedItem.Value) & "|" & _
                         ddlVehCode.SelectedItem.Value & "|" & _
                         ddlVehExpCode.SelectedItem.Value & "|" & _
                         lblAssetCode.Text & "|" & _
                         ddlGoodsRcvId.SelectedItem.Value & "|" & _
                         ddlDispAdvID.SelectedItem.Value & "|" & _
                         hidOriCost.Value
        End If

        strParam = strGRNId & "|" & _
                   strLocation & "|" & _
                   lblGoodsRetType.Text & "|" & _
                   txtSupCode.Text & "|" & _
                   dblTotalAmount & "|" & _
                   txtRemark.Text & "|" & _
                   objPU.EnumGRNStatus.Active & "|" & _
                   Trim(ddlInventoryBin.SelectedItem.Value) & "|" & _
                   strNewIDFormat & "|" & _
                   strDate

        Try
            If ddlChargeLevel.SelectedIndex = 0 And tblAcc.Visible = True And RowPreBlk.Visible = True Then
                strParamList = Session("SS_LOCATION") & "|" & _
                                       ddlAccount.SelectedItem.Value.Trim & "|" & _
                                       ddlPreBlock.SelectedItem.Value.Trim & "|" & _
                                       objGLSetup.EnumBlockStatus.Active
                intErrNo = objPU.mtdAddGRNLnByBlock(strOpCodeGLSubBlkByBlk, _
                                                    strParamList, _
                                                    strOpCd_AddGRN, _
                                                    strOpCd_AddGRNLn, _
                                                    strOpCd_GetItem, _
                                                    strOpCd_UpdItem, _
                                                    strOpCd_GetGRNLn, _
                                                    strOpCd_UpdGRN, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    strParamLn, _
                                                    objGRNId, _
                                                    objGRNLnId, _
                                                    intErrorCheck, _
                                                    strLocType, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReturn), _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReturnLn))
            Else
                intErrNo = objPU.mtdAddGRNLn(strOpCd_AddGRN, _
                                            strOpCd_AddGRNLn, _
                                            strOpCd_GetItem, _
                                            strOpCd_UpdItem, _
                                            strOpCd_GetGRNLn, _
                                            strOpCd_UpdGRN, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            strParamLn, _
                                            objGRNId, _
                                            objGRNLnId, _
                                            intErrorCheck, _
                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReturn), _
                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReturnLn))
            End If
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_NEW_GRNLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
            End If
        End Try


        If intErrorCheck = objPU.EnumPUErrorType.NoError Then
            Dim strOpCode_ItemUpd As String = "PU_CLSTRX_GRN_LINE_UPD"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""

            strParamName = "GOODSRETID|GOODSRETLNID|POLNID"

            strParamValue = Trim(objGRNId) & _
                            "|" & Trim(objGRNLnId) & _
                            "|" & Trim(hidPOLNID.Value)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
            End Try
        End If

        lblQtyOnHand.Text = ""
        txtQty.Text = ""
        txtCost.Text = ""
        txtCost.Enabled = True
        lblIDQtyOnHand.Text = ""
        lblInvisibleCost.Text = ""
        RegularExpressionValidatorCost.Enabled = True
        strSelectedGoodsRetId = objGRNId
        strSelectedSuppCode = txtSupCode.Text
        onLoad_Display(strSelectedGoodsRetId)
        onLoad_DisplayLn(strSelectedGoodsRetId)
        If ddlInvRcvId.SelectedItem.Value = "" And ddlRetAdvId.SelectedItem.Value = "" And ddlGoodsRcvId.SelectedItem.Value = "" And ddlDispAdvID.SelectedItem.Value = "" Then
            BindItemCode(strSelectedItemCode)
        Else
            If ddlInvRcvId.SelectedItem.Value <> "" Then
                BindRetItem(ddlInvRcvId.SelectedItem.Value, "")
            ElseIf ddlRetAdvId.SelectedItem.Value <> "" Then
                BindRetItem("", ddlRetAdvId.SelectedItem.Value)
            ElseIf ddlGoodsRcvId.SelectedItem.Value <> "" Then
                BindGRRetItem(ddlGoodsRcvId.SelectedItem.Value)
            ElseIf ddlDispAdvID.SelectedItem.Value <> "" Then
                BindDispItem(ddlDispAdvID.SelectedItem.Value)
            End If
        End If
        BindAccount("")
        BindPreBlock("", "")
        BindBlock("", "")
        BindVehicle("", "")
        BindVehicleExpense(True, "")
        hidPOLNID.Value = ""
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_DelGRNLn As String = "PU_CLSTRX_GRN_LINE_DEL"
        Dim strOpCd_GetGRNLn As String = "PU_CLSTRX_GRN_LINE_GET"
        Dim strOpCd_UpdGRN As String = "PU_CLSTRX_GRN_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strParam As String = ""
        Dim GRNLnIdCell As TableCell = E.Item.Cells(0)
        Dim ItemCodeCell As TableCell = E.Item.Cells(1)
        Dim QtyReturnCell As TableCell = E.Item.Cells(2)
        Dim strGRNLnId As String = GRNLnIdCell.Text
        Dim strItemCode As String = ItemCodeCell.Text
        Dim strQtyReturn As String = QtyReturnCell.Text
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim strDate As String = Date_Validation(txtGoodsRetDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRetDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strParam = lblGoodsRetId.Text & "|" & _
                   strGRNLnId & "|" & _
                   strItemCode & "|" & _
                   strQtyReturn

        Try
            intErrNo = objPU.mtdDelGRNLn(strOpCd_DelGRNLn, _
                                         strOpCd_GetGRNLn, _
                                         strOpCd_UpdGRN, _
                                         strOpCd_GetItem, _
                                         strOpCd_UpdItem, _
                                         strCompany, _
                                         strLocation, _
                                         strUserId, _
                                         strParam, _
                                         intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DEL_GRNLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
            End If
        End Try

        strSelectedGoodsRetId = lblGoodsRetId.Text
        onLoad_Display(strSelectedGoodsRetId)
        onLoad_DisplayLn(strSelectedGoodsRetId)
        If ddlInvRcvId.SelectedItem.Value = "" And ddlRetAdvId.SelectedItem.Value = "" And ddlGoodsRcvId.SelectedItem.Value = "" And ddlDispAdvID.SelectedItem.Value = "" Then
            BindItemCode(strSelectedItemCode)
        Else
            If ddlInvRcvId.SelectedItem.Value <> "" Then
                BindRetItem(ddlInvRcvId.SelectedItem.Value, "")
            ElseIf ddlRetAdvId.SelectedItem.Value <> "" Then
                BindRetItem("", ddlRetAdvId.SelectedItem.Value)
            ElseIf ddlGoodsRcvId.SelectedItem.Value <> "" Then
                BindGRRetItem(ddlGoodsRcvId.SelectedItem.Value)
            ElseIf ddlDispAdvID.SelectedItem.Value <> "" Then
                BindDispItem(ddlDispAdvID.SelectedItem.Value)
            End If
        End If
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddGRN As String = "PU_CLSTRX_GRN_ADD"
        Dim strOpCd_UpdGRN As String = "PU_CLSTRX_GRN_UPD"
        Dim objGRNId As Object
        Dim strSupplierCode As String = txtSupCode.Text
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strDate As String = Date_Validation(txtGoodsRetDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRetDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        Dim arrParam As Array
        arrParam = Split(lblAccPeriod.Text, "/")
        If Month(strDate) <> arrParam(0) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        ElseIf Year(strDate) <> arrParam(1) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        If lblGoodsRetId.Text = "" Then
            Exit Sub
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        If strSupplierCode = "" Then
            lblSuppCode.Visible = True
            Exit Sub
        End If

        strParam = lblGoodsRetId.Text & "|" & _
                   strLocation & "|" & _
                   lblGoodsRetType.Text & "|" & _
                   strSupplierCode & "|" & _
                   strRemark & "|" & _
                   objPU.EnumGRNStatus.Active & "|" & _
                   Trim(ddlInventoryBin.SelectedItem.Value)

        Try
            intErrNo = objPU.mtdUpdGRN(strOpCd_AddGRN, _
                                       strOpCd_UpdGRN, _
                                       strCompany, _
                                       strLocation, _
                                       strUserId, _
                                       strAccMonth, _
                                       strAccYear, _
                                       strParam, _
                                       objGRNId, _
                                       objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReturn))
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_SAVE_GRN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
        End Try

        strSelectedGoodsRetId = lblGoodsRetId.Text
        onLoad_Display(strSelectedGoodsRetId)
        onLoad_DisplayLn(strSelectedGoodsRetId)
    End Sub

    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetGRNLn As String = "PU_CLSTRX_GRN_LINE_GET"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGRN As String = "PU_CLSTRX_GRN_UPD"
        Dim strOpCd_GRNFAGoodRetLn As String = "PU_CLSTRX_GRN_LINE_FAGRETLN_GET"
        Dim strOpCd_GRNFARegLn As String = "PU_CLSTRX_GRN_FAREGLN_UPD"
        Dim strOpCd_GRFAAdd As String = "PU_CLSTRX_GR_FAADD_ADD"
        Dim strOpCd_GRNFACodeValue As String = "PU_CLSTRX_FACODEVALUE_GET"
        Dim strOpCd_GetGRLn As String = "PU_CLSTRX_GR_LINE_GET"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim objGRNId As Object
        Dim strSupplierCode As String = txtSupCode.Text
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer

        Dim objRsl As Object
        Dim objFAAddPermRsl As Object
        Dim objFAAddRsl As Object
        Dim objGRDs As Object
        Dim objFARegLnRsl As Object
        Dim objFACodeVal As Object
        Dim Amount As Double
        Dim count As Integer
        Dim FixedAssetCode As String
        Dim GoodsRcvId As String
        Dim strGoodsRcvRefNo As String
        Dim strGoodsRcvRefDate As String
        Dim strOpCd_GetGR As String = "PU_CLSTRX_GR_GET"
        Dim strOpCd_GRFAAddPerm As String = "FA_CLSSETUP_ASSETPERMIT_GET"
        Dim TxID As String
        Dim GMValue As Double = 0.0
        Dim AssetValue As Double = 0.0
        Dim NetValue As Double = 0.0
        Dim strDate As String = Date_Validation(txtGoodsRetDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRetDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        Dim arrParam As Array
        arrParam = Split(lblAccPeriod.Text, "/")
        If Month(strDate) <> arrParam(0) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        ElseIf Year(strDate) <> arrParam(1) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        strParam = lblGoodsRetId.Text & "|" & _
                   strLocation & "|" & _
                   strSupplierCode & "|" & _
                   strRemark & "|" & _
                   objPU.EnumGRNStatus.Confirmed

        Try
            intErrNo = objPU.mtdUpdGRNLn(strOpCd_GetGRNLn, _
                                         strOpCd_GetItem, _
                                         strOpCd_UpdItem, _
                                         strOpCd_UpdGRN, _
                                         strCompany, _
                                         strLocation, _
                                         strUserId, _
                                         strParam, _
                                         intErrorCheck)

            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CONFIRM_GRN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
            End If
        End Try

        Try
            intErrNo = objPU.mtdUpdGRNPOLn(strOpCd_GetGRNLn, _
                                        strOpCd_GetGRLn, _
                                        strOpCd_GetPOLn, _
                                        strOpCd_UpdPOLn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdGRN, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)


            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CONFIRM_GRN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRNList.aspx")
            End If
        End Try

        If Trim(lblGoodsRetType.Text) = CStr(objPU.EnumGRNType.FixedAsset) And _
           objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True And _
           ddlGoodsRcvId.SelectedItem.Text <> "" Then

            Try
                intErrNo = objPU.mtdGetGRNFALine(strOpCd_GRNFAGoodRetLn, lblGoodsRetId.Text, objRsl)

                For count = 0 To objRsl.Tables(0).Rows.Count - 1
                    Amount = Convert.ToDouble(objRsl.Tables(0).Rows(count).Item("Amount"))
                    FixedAssetCode = objRsl.Tables(0).Rows(count).Item("FixedAssetCode").Trim()
                    GoodsRcvId = objRsl.Tables(0).Rows(count).Item("GoodsRcvId").Trim()

                    Try
                        strParam = FixedAssetCode & "|" & strLocation
                        intErrNo = objPU.mtdGetGRFAAddPermission(strOpCd_GRFAAddPerm, strParam, objFAAddPermRsl)

                        If objFAAddPermRsl.tables(0).rows(0).item("AssetAddPerm") = objFA.EnumAssetAddPerm.Yes Then
                            Try
                                strParam = FixedAssetCode & "|" & strLocation
                                intErrNo = objPU.mtdGetFACodeValue(strOpCd_GRNFACodeValue, strParam, objFACodeVal)

                                If (objFACodeVal.tables(0).rows.count > 0) Then
                                    GMValue = Convert.ToDouble(objFACodeVal.Tables(0).Rows(0).Item("GMValue"))
                                    AssetValue = Convert.ToDouble(objFACodeVal.Tables(0).Rows(0).Item("AssetValue"))
                                    NetValue = Convert.ToDouble(objFACodeVal.Tables(0).Rows(0).Item("NetValue"))
                                End If

                                If Amount <= GMValue And Amount <= AssetValue And Amount <= NetValue Then

                                    Try
                                        strParam = Amount & "|" & Amount & "|" & Amount & "|" & _
                                                   strUserId & "|" & strLocation & "|" & FixedAssetCode

                                        intErrNo = objPU.mtdUpdGRNFARegLn(strOpCd_GRNFARegLn, strParam, objFARegLnRsl)
                                    Catch Exp As System.Exception
                                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GR_FAREGLN_UPD&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRNList.aspx")
                                    End Try


                                    Try
                                        strParam = GoodsRcvId & "|" & strLocation & "|||||A.GoodsRcvId|"
                                        intErrNo = objPU.mtdGetGR(strCompany, _
                                                                  strLocation, _
                                                                  strUserId, _
                                                                  strAccMonth, _
                                                                  strAccYear, _
                                                                  strOpCd_GetGR, _
                                                                  strParam, _
                                                                  objGRDs)

                                        If objGRDs.Tables(0).Rows.Count > 0 Then
                                            strGoodsRcvRefNo = Trim(objGRDs.Tables(0).Rows(0).Item("GoodsRcvRefNo"))
                                            strGoodsRcvRefDate = objGlobal.GetLongDate(objGRDs.Tables(0).Rows(0).Item("GoodsRcvRefDate"))
                                        End If
                                    Catch Exp As System.Exception
                                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GR_FAREGLN_UPD&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRNList.aspx")
                                    End Try
                                    Try
                                        strParam = strLocation & "|" & _
                                                   strGoodsRcvRefNo & "|" & _
                                                   strGoodsRcvRefDate & "|" & _
                                                   FixedAssetCode & "|" & _
                                                   -Amount & "|" & _
                                                   "|" & _
                                                   strAccMonth & "|" & _
                                                   strAccYear & "|" & _
                                                   objFATrx.EnumAssetAddStatus.Confirmed & "|" & _
                                                   strUserId & "|" & _
               objRsl.Tables(0).Rows(count).Item("AccCode") & "|" & _
               objRsl.Tables(0).Rows(count).Item("BlkCode") & "|" & _
               objRsl.Tables(0).Rows(count).Item("VehCode") & "|" & _
               objRsl.Tables(0).Rows(count).Item("VehExpenseCode")

                                        intErrNo = objPU.mtdAddFAAdd(strOpCd_GRFAAdd, _
                                                                     strParam, _
                                                                     objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FAAssetAddition), _
                                                                     TxID, _
                                                                     objFAAddRsl)

                                    Catch Exp As System.Exception
                                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GRN_FAADD_ADD&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRNList.aspx")
                                    End Try

                                End If
                            Catch Exp As System.Exception
                                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_FACODEVALUE_GET&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRNList.aspx")
                            End Try
                        End If
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETPERMIT_GET&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRNList.aspx")
                    End Try
                Next


            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GR_LINE_FAGRETLN_GET&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRNList.aspx")
            End Try
        End If

        If intErrorCheck = objPU.EnumPUErrorType.NoError Then
            Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""

            strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

            strParamValue = Trim(strLocation) & _
                            "|" & "PU_GOODSRET" & _
                            "|" & "PU_GOODSRETLN" & _
                            "|" & "GOODSRETID" & _
                            "|" & Trim(lblGoodsRetId.Text) & _
                            "|" & "QTYRETURN" & _
                            "|" & ddlInventoryBin.SelectedItem.Value & _
                            "|" & "-" & _
                            "|" & Trim(strUserId)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
            End Try
        End If
        onLoad_Display(lblGoodsRetId.Text)
        onLoad_DisplayLn(lblGoodsRetId.Text)
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetGRNLn As String = "PU_CLSTRX_GRN_LINE_GET"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGRN As String = "PU_CLSTRX_GRN_UPD"
        Dim objGRNId As Object
        Dim strSupplierCode As String = txtSupCode.Text
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim strDate As String = Date_Validation(txtGoodsRetDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRetDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strParam = lblGoodsRetId.Text & "|" & _
                   strLocation & "|" & _
                   strSupplierCode & "|" & _
                   strRemark & "|" & _
                   objPU.EnumGRNStatus.Deleted

        Try
            intErrNo = objPU.mtdUpdGRNLn(strOpCd_GetGRNLn, _
                                         strOpCd_GetItem, _
                                         strOpCd_UpdItem, _
                                         strOpCd_UpdGRN, _
                                         strCompany, _
                                         strLocation, _
                                         strUserId, _
                                         strParam, _
                                         intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DELETE_GRN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
            End If
        End Try

        onLoad_Display(lblGoodsRetId.Text)
        onLoad_DisplayLn(lblGoodsRetId.Text)
    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetGRNLn As String = "PU_CLSTRX_GRN_LINE_GET"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGRN As String = "PU_CLSTRX_GRN_UPD"
        Dim objGRNId As Object
        Dim strSupplierCode As String = txtSupCode.Text
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim strDate As String = Date_Validation(txtGoodsRetDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRetDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If
        strParam = lblGoodsRetId.Text & "|" & _
                   strLocation & "|" & _
                   strSupplierCode & "|" & _
                   strRemark & "|" & _
                   objPU.EnumGRNStatus.Active

        Try
            intErrNo = objPU.mtdUpdGRNLn(strOpCd_GetGRNLn, _
                                         strOpCd_GetItem, _
                                         strOpCd_UpdItem, _
                                         strOpCd_UpdGRN, _
                                         strCompany, _
                                         strLocation, _
                                         strUserId, _
                                         strParam, _
                                         intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UNDELETE_GRN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
            End If
        End Try

        onLoad_Display(lblGoodsRetId.Text)
        onLoad_DisplayLn(lblGoodsRetId.Text)
    End Sub

    Sub btnCancel_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetGRNLn As String = "PU_CLSTRX_GRN_LINE_GET"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGRN As String = "PU_CLSTRX_GRN_UPD"
        Dim strOpCd_GetGRLn As String = "PU_CLSTRX_GR_LINE_GET"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim objGRNId As Object
        Dim strSupplierCode As String = txtSupCode.Text
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim strDate As String = Date_Validation(txtGoodsRetDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRetDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strParam = lblGoodsRetId.Text & "|" & _
                   strLocation & "|" & _
                   strSupplierCode & "|" & _
                   strRemark & "|" & _
                   objPU.EnumGRNStatus.Cancelled

        Try
            intErrNo = objPU.mtdUpdGRNLn(strOpCd_GetGRNLn, _
                                         strOpCd_GetItem, _
                                         strOpCd_UpdItem, _
                                         strOpCd_UpdGRN, _
                                         strCompany, _
                                         strLocation, _
                                         strUserId, _
                                         strParam, _
                                         intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CANCEL_GRN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
            End If
        End Try

        Try
            intErrNo = objPU.mtdUpdGRNPOLn(strOpCd_GetGRNLn, _
                                        strOpCd_GetGRLn, _
                                        strOpCd_GetPOLn, _
                                        strOpCd_UpdPOLn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdGRN, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)


            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CONFIRM_GRN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRNList.aspx")
            End If
        End Try

        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

        strParamValue = Trim(strLocation) & _
                        "|" & "PU_GOODSRET" & _
                        "|" & "PU_GOODSRETLN" & _
                        "|" & "GOODSRETID" & _
                        "|" & Trim(lblGoodsRetId.Text) & _
                        "|" & "QTYRETURN" & _
                        "|" & ddlInventoryBin.SelectedItem.Value & _
                        "|" & "+" & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try

        onLoad_Display(lblGoodsRetId.Text)
        onLoad_DisplayLn(lblGoodsRetId.Text)
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String
        Dim intStatus As Integer
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String
        Dim intErrNo As Integer

        strUpdString = "where GoodsRetId = '" & lblGoodsRetId.Text & "'"
        strStatus = lblStatus.Text
        intStatus = Convert.ToInt16(lblHidStatus.Text)
        strPrintDate = Trim(lblPrintDate.Text)
        strSortLine = "PU_GOODSRETLN.GoodsRetLnId"
        strTable = "PU_GOODSRET"

        If intStatus = objPU.EnumGRNStatus.Confirmed Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmShare.mtdUpdPrintDate(strOpCodePrint, _
                                                           strUpdString, _
                                                           strTable, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GRN_DETAILS_UPD_PRINTDATE&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
                End Try
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        onLoad_Display(lblGoodsRetId.Text)
        onLoad_DisplayLn(lblGoodsRetId.Text)

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_GRNDet.aspx?strGoodsRetId=" & lblGoodsRetId.Text & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_GRNList.aspx")
    End Sub

    Sub BindGRRetItem(ByVal pv_strGoodsRcvId As String)
        Dim strOpCd_GetGoodsRcv As String = "PU_CLSTRX_GRN_GOODSRCV_LINE_GET"
        Dim strOpCd_GetPOLnUnitCost As String = "PU_CLSTRX_GRN_GOODSRCV_LINE_COST_GET"
        Dim objRetItemDs As New Object
        Dim objCostDs As New Object
        Dim strParam As String = ""
        Dim dr As DataRow
        Dim POLnID As String
        Dim GoodsRcvID As String
        Dim AssetCode As String
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim POUnitCost As Double
        Dim strGRNType As String

        If lblGoodsRetType.Text = objPU.EnumGRNType.Stock Then
            strGRNType = objIN.EnumPurReqDocType.StockPR & "','" & objIN.EnumPurReqDocType.WorkshopPR
        Else
            strGRNType = lblGoodsRetType.Text
        End If

        strParam = pv_strGoodsRcvId & "|" & strGRNType
        Try
            intErrNo = objPU.mtdGetGRRetItem(strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strOpCd_GetGoodsRcv, _
                                            strParam, _
                                            objRetItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_RETURNITEM_FOR_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
        End Try

        lblQtyOnHand.Text = ""
        lblIDQtyOnHand.Text = ""

        If objRetItemDs.Tables(0).Rows.Count > 0 Then
            BindInventoryBinLevel(objRetItemDs.Tables(0).Rows(0).Item("Bin"))
        End If

        For intCnt = 0 To objRetItemDs.Tables(0).Rows.Count - 1
            GoodsRcvID = objRetItemDs.Tables(0).Rows(intCnt).Item("GoodsRcvID")

            If lblGoodsRetType.Text = objPU.EnumGRNType.Stock Then
                If objRetItemDs.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then
                    objRetItemDs.Tables(0).Rows(intCnt).Item("Description") = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                              objRetItemDs.Tables(0).Rows(intCnt).Item("PartNo") & " (" & _
                                                                              objRetItemDs.Tables(0).Rows(intCnt).Item("Description") & ")"
                    objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                           objRetItemDs.Tables(0).Rows(intCnt).Item("PartNo")
                Else
                    objRetItemDs.Tables(0).Rows(intCnt).Item("Description") = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                              objRetItemDs.Tables(0).Rows(intCnt).Item("Description") & ")"
                End If
            Else
                objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                           objRetItemDs.Tables(0).Rows(intCnt).Item("POLnID")
                objRetItemDs.Tables(0).Rows(intCnt).Item("Description") = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                          objRetItemDs.Tables(0).Rows(intCnt).Item("Description") & " - " & _
                                                                          objRetItemDs.Tables(0).Rows(intCnt).Item("AdditionalNote") & ")"
            End If

            If objRetItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = strSelectedItemCode Then
                intSelectedIndex = intCnt + 1
                lblQtyOnHand.Text = objPU.mtdGetQty(Trim(strLocation), _
                                                   Trim(strSelectedItemCode), _
                                                   False, _
                                                   objRetItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"))
                lblIDQtyOnHand.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Convert.ToDouble(lblQtyOnHand.Text), 5)

                lblRcvQty.Text = objRetItemDs.Tables(0).Rows(intCnt).Item("Qty")

                Select Case Trim(Replace(lblRcvQty.Text, ",", "."))
                    Case "0.00000", "0,00000", "0", ""
                        lblQtyDisp.Visible = True
                    Case Else
                End Select

                strItemType = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemType")
                hidItemType.Value = objRetItemDs.Tables(0).Rows(intCnt).Item("ItemType")

                If lblGoodsRetType.Text = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
                    AssetCode = objRetItemDs.Tables(0).Rows(intCnt).Item("FixedAssetCode")
                    tblFACode.Visible = True
                    lblAssetCode.Text = AssetCode
                End If

                POLnID = objRetItemDs.Tables(0).Rows(intCnt).Item("POLnId")
                hidPOLNID.Value = objRetItemDs.Tables(0).Rows(intCnt).Item("POLnId")

                Try
                    strParam = POLnID & "|" & GoodsRcvID

                    intErrNo = objPU.mtdGetPOLnUnitCost(strCompany, _
                                                      strLocation, _
                                                      strAccMonth, _
                                                      strAccYear, _
                                                      strOpCd_GetPOLnUnitCost, _
                                                      strParam, _
                                                      objCostDs)

                    If objCostDs.Tables(0).Rows.count > 0 Then
                        POUnitCost = CDbl(objCostDs.Tables(0).Rows(0).Item("Cost"))
                    Else
                        POUnitCost = 0
                    End If

                    txtCost.Enabled = False
                    lblInvisibleCost.Text = POUnitCost
                    txtCost.Text = objCostDs.Tables(0).Rows(0).Item("OriCost") 'objGlobal.GetIDDecimalSeparator(FormatNumber(POUnitCost, 2))
                    RegularExpressionValidatorCost.Enabled = False
                    hidOriCost.Value = objCostDs.Tables(0).Rows(0).Item("OriCost")

                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GRN_GOODSRCV_LINE_COST_GET&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=pu/trx/pu_trx_grnlist.aspx")
                End Try

            End If

        Next intCnt

        If txtSupCode.Text.Trim = "" Then
            objRetItemDs.Tables(0).Clear()
        End If

        dr = objRetItemDs.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = "Please select Item Code"
        objRetItemDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlItemCode.DataSource = objRetItemDs.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex



        If ddlItemCode.SelectedIndex = 0 Then
            tblAcc.Visible = False
        Else
            If strItemType = objINSetup.EnumInventoryItemType.DirectCharge Then
                tblAcc.Visible = True
                FindAcc.Visible = True
            Else
                tblAcc.Visible = False
            End If
        End If
    End Sub

    Function CheckDate(ByVal pv_strDate As String, ByRef pr_strDate As String) As Boolean
        Dim strDateFormatCode As String = Session("SS_DATEFMT")
        Dim strDateFormat As String

        pr_strDate = ""
        CheckDate = True
        If Not pv_strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormatCode, pv_strDate, strDateFormat, pr_strDate) = False Then
                lblFmt.Text = strDateFormat
                pr_strDate = ""
                CheckDate = False
            End If
        End If
    End Function

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_GRList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
        Dim strText = "Please select Inventory Bin"

        ddlInventoryBin.Items.Clear()
        ddlInventoryBin.Items.Add(New ListItem(strText, "0"))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.HO), objINSetup.EnumInventoryBinLevel.HO))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Central), objINSetup.EnumInventoryBinLevel.Central))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Other), objINSetup.EnumInventoryBinLevel.Other))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinI), objINSetup.EnumInventoryBinLevel.BinI))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinII), objINSetup.EnumInventoryBinLevel.BinII))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIII), objINSetup.EnumInventoryBinLevel.BinIII))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIV), objINSetup.EnumInventoryBinLevel.BinIV))

        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        Else
            ddlInventoryBin.SelectedIndex = -1
        End If
    End Sub

    Sub btnNew_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("PU_trx_GRNDet.aspx?GRNType=" & lblGoodsRetType.Text)
    End Sub

    Sub DispAdvIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedDispAdvId = ddlDispAdvID.SelectedItem.Value

        If ddlGoodsRcvId.SelectedItem.Text = "Please select Dispatch Advice ID" Then
            ddlInvRcvId.Enabled = True
            ddlRetAdvId.Enabled = True
            ddlGoodsRcvId.Enabled = True
            If lblGoodsRetType.Text = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
                tblFACode.Visible = True
                lblAssetCode.Text = ""
            End If
            lblQtyOnHand.Text = ""
            txtQty.Text = ""
            txtCost.Text = ""
            txtCost.Enabled = True
            lblIDQtyOnHand.Text = ""
            lblInvisibleCost.Text = ""
            RegularExpressionValidatorCost.Enabled = True
        Else
            CtrlGoodsRetDocType()
        End If

    End Sub

    Sub BindDispAdv(ByVal pv_strSuppCode As String)
        Dim strOpCd As String = "PU_CLSTRX_GRN_DISPADV_GET"
        Dim objDispAdvDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "STATUS|LOCCODE|SUPPLIERCODE"
        strParamValue = objPU.EnumDAStatus.Confirmed & "','" & objPU.EnumDAStatus.Closed & _
                        "|" & strLocation & _
                        "|" & pv_strSuppCode

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objDispAdvDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GRN_DISPADV_GET&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
        End Try

        For intCnt = 0 To objDispAdvDs.Tables(0).Rows.Count - 1
            objDispAdvDs.Tables(0).Rows(intCnt).Item("DispAdvId") = objDispAdvDs.Tables(0).Rows(intCnt).Item("DispAdvId").Trim()
            objDispAdvDs.Tables(0).Rows(intCnt).Item("DispDispAdvId") = objDispAdvDs.Tables(0).Rows(intCnt).Item("DispAdvId").Trim()

            If objDispAdvDs.Tables(0).Rows(intCnt).Item("DispAdvId") = strSelectedDispAdvId Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        If txtSupCode.Text.Trim = "" Then
            objDispAdvDs.Tables(0).Clear()
        End If

        dr = objDispAdvDs.Tables(0).NewRow()
        dr("DispAdvId") = ""
        dr("DispDispAdvId") = "Please select Dispatch Advice ID"
        objDispAdvDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDispAdvID.DataSource = objDispAdvDs.Tables(0)
        ddlDispAdvID.DataValueField = "DispAdvId"
        ddlDispAdvID.DataTextField = "DispDispAdvId"
        ddlDispAdvID.DataBind()
        ddlDispAdvID.SelectedIndex = intSelectedIndex
        strSelectedDispAdvId = ddlDispAdvID.SelectedItem.Value
    End Sub

    Sub BindDispItem(ByVal pv_strDispAdvId As String)
        Dim strOpCd As String = "PU_CLSTRX_GRN_DISPADV_LINE_GET"
        Dim objDispItemDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim POLnID As String
        Dim intSelectedIndex As Integer = 0
        Dim strGRNType As String
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        If lblGoodsRetType.Text = objPU.EnumGRNType.Stock Then
            strGRNType = objIN.EnumPurReqDocType.StockPR & "','" & objIN.EnumPurReqDocType.WorkshopPR
        Else
            strGRNType = lblGoodsRetType.Text
        End If

        strParamName = "STATUS|LOCCODE|GRNTYPE|DISPADVID|GOODSRETID"
        strParamValue = objPU.EnumDAStatus.Confirmed & "','" & objPU.EnumDAStatus.Closed & _
                        "|" & strLocation & _
                        "|" & strGRNType & _
                        "|" & pv_strDispAdvId & _
                        "|" & lblGoodsRetId.Text.Trim

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objDispItemDs)
          
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_RETURNITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_grnlist.aspx")
        End Try

        lblQtyOnHand.Text = ""
        lblIDQtyOnHand.Text = ""

        For intCnt = 0 To objDispItemDs.Tables(0).Rows.Count - 1

            If lblGoodsRetType.Text = objPU.EnumGRNType.Stock Then
                If objDispItemDs.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then
                    objDispItemDs.Tables(0).Rows(intCnt).Item("Description") = objDispItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                              objDispItemDs.Tables(0).Rows(intCnt).Item("PartNo") & " (" & _
                                                                              objDispItemDs.Tables(0).Rows(intCnt).Item("Description") & ")"
                    objDispItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = objDispItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                            objDispItemDs.Tables(0).Rows(intCnt).Item("PartNo")
                Else
                    objDispItemDs.Tables(0).Rows(intCnt).Item("Description") = objDispItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                              objDispItemDs.Tables(0).Rows(intCnt).Item("Description") & ")"
                End If
            Else
                objDispItemDs.Tables(0).Rows(intCnt).Item("Description") = objDispItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                          objDispItemDs.Tables(0).Rows(intCnt).Item("Description") & ")"
            End If

            If objDispItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = strSelectedItemCode Then
                intSelectedIndex = intCnt + 1
                lblQtyOnHand.Text = objPU.mtdGetQty(Trim(strLocation), _
                                                    Trim(strSelectedItemCode), _
                                                    False, _
                                                    objDispItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"))
                'lblIDQtyOnHand.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Convert.ToDouble(lblQtyOnHand.Text), 5)
                lblIDQtyOnHand.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Convert.ToDouble(objDispItemDs.Tables(0).Rows(intCnt).Item("Qty")), 5)
                strItemType = objDispItemDs.Tables(0).Rows(intCnt).Item("ItemType")
                hidItemType.Value = objDispItemDs.Tables(0).Rows(intCnt).Item("ItemType").Trim()


                txtCost.Enabled = False
                lblInvisibleCost.Text = objDispItemDs.Tables(0).Rows(intCnt).Item("Cost")
                txtCost.Text = objDispItemDs.Tables(0).Rows(intCnt).Item("OriCost")
                RegularExpressionValidatorCost.Enabled = False
                hidOriCost.Value = objDispItemDs.Tables(0).Rows(intCnt).Item("OriCost")
                POLnID = objDispItemDs.Tables(0).Rows(intCnt).Item("POLnId")
                hidPOLNID.Value = objDispItemDs.Tables(0).Rows(intCnt).Item("POLnId")
            End If
        Next intCnt

        If txtSupCode.Text.Trim = "" Then
            objDispItemDs.Tables(0).Clear()
        End If

        dr = objDispItemDs.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = "Please select Item Code"
        objDispItemDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlItemCode.DataSource = objDispItemDs.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex

        If ddlItemCode.SelectedIndex = 0 Then
            tblAcc.Visible = False
        Else
            If strItemType = objPU.EnumGRNType.DirectCharge Then
                tblAcc.Visible = True
                FindAcc.Visible = True
            Else
                tblAcc.Visible = False
            End If
        End If
    End Sub

    Private Sub lbViewJournal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbViewJournal.Click
        Dim intErrNo As Integer
        Dim dsResult As New Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "GL_JOURNAL_PREDICTION"
        Dim arrPeriod As Array

        arrPeriod = Split(lblAccPeriod.Text, "/")

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|USERID|TRXID"
        strParamValue = strLocation & "|" & arrPeriod(0) & _
                        "|" & arrPeriod(1) & "|" & _
                        Session("SS_USERID") & "|" & Trim(lblGoodsRetId.Text)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsResult)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DAYEND_PROCESS&errmesg=" & Exp.Message.ToString & "&redirect=")
        End Try

        If dsResult.Tables(0).Rows.Count > 0 Then

            Dim TotalDB As Double
            Dim TotalCR As Double
            Dim intCnt As Integer
            For intCnt = 0 To dsResult.Tables(0).Rows.Count - 1
                TotalDB += dsResult.Tables(0).Rows(intCnt).Item("AmountDB")
                TotalCR += dsResult.Tables(0).Rows(intCnt).Item("AmountCR")
            Next
            lblTotalDB.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TotalDB, 2), 2)
            lblTotalCR.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TotalCR, 2), 2)

            dgViewJournal.DataSource = Nothing
            dgViewJournal.DataSource = dsResult.Tables(0)
            dgViewJournal.DataBind()

            lblTotalDB.Visible = True
            lblTotalCR.Visible = True
            lblTotalViewJournal.Visible = True
            lblTotalViewJournal.Text = "Total Amount : "
        End If

        strSelectedGoodsRetId = lblGoodsRetId.Text
        onLoad_Display(strSelectedGoodsRetId)
        onLoad_DisplayLn(strSelectedGoodsRetId)
    End Sub

    Sub GetSupplierBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        strSelectedSuppCode = Trim(txtSupCode.Text)
        BindInvRcv(Trim(txtSupCode.Text))
        BindRetAdv(Trim(txtSupCode.Text))
        BindGoodsRcv(Trim(txtSupCode.Text))
        BindDispAdv(Trim(txtSupCode.Text))

        ddlInvRcvId.Enabled = True
        ddlInvRcvId.SelectedIndex = -1
        ddlRetAdvId.Enabled = True
        ddlRetAdvId.SelectedIndex = -1
        ddlGoodsRcvId.Enabled = True
        ddlGoodsRcvId.SelectedIndex = -1
        ddlDispAdvID.Enabled = True
        ddlDispAdvID.SelectedIndex = -1
        ddlItemCode.Enabled = True
        ddlItemCode.SelectedIndex = -1
        If lblGoodsRetType.Text = CStr(objPU.EnumGRNType.FixedAsset) And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
            tblFACode.Visible = True
            lblAssetCode.Text = ""
        End If

        lblQtyOnHand.Text = 0
        txtQty.Text = 0
        txtCost.Text = 0
 

        txtCost.Enabled = True
        lblIDQtyOnHand.Text = 0
        lblInvisibleCost.Text = 0
        RegularExpressionValidatorCost.Enabled = True
    End Sub
End Class
