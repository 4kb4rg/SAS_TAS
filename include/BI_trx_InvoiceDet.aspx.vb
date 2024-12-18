
Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.Math


Public Class BI_trx_InvoiceDet : Inherits Page


    Dim PreBlockTag As String
    Dim BlockTag As String

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objBITrx As New agri.BI.clsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objCMSetup As New agri.CM.clsSetup()
    Dim objCMTrx As New agri.CM.clsTrx()
    Dim objWMTrx As New agri.WM.clsTrx()
    Protected objGLTrx As New agri.GL.ClsTrx()

    Dim obInvoiceDs As New Object()
    Dim obInvoiceLnDs As New Object()
    Dim objBPDs As New Object()
    Dim objAccDs As New Object()
    Dim objBlkDs As New Object()
    Dim objVehDs As New Object()
    Dim objVehExpDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objContractDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intBIAR As Integer
    Dim intConfig As Integer

    Dim strSelectedIVID As String
    Dim intIVStatus As Integer
    Dim strAcceptDateFormat As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim pv_strCurrencyCode As String
    Dim strCurrency As String
    Dim strExRate As String
    Dim strSeller As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intBIAR = Session("SS_BIAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")
        txtDevRef.ReadOnly = True
        txtDevRef.Enabled = False
        txtDevRef.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIInvoice), intBIAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrBillParty.Visible = False
            lblErrAccCode.Visible = False
            lblPreBlockErr.Visible = False
            lblCRErrRefNo.Visible = False
            lblDLErrRefNo.Visible = False
            lblErrBlock.Visible = False
            lblErrVehicle.Visible = False
            lblErrVehicleExp.Visible = False
            lblErrDesc.Visible = False
            lblErrTotalUnits.Visible = False
            lblErrRate.Visible = False
            lblErrAmount.Visible = False
            lblErrTotal.Visible = False
            lblReferer.Text = Request.QueryString("referer")
            strSelectedIVID = Trim(IIf(Request.QueryString("IVid") = "", Request.Form("IVid"), Request.QueryString("IVid")))
            IVid.Value = strSelectedIVID

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            AddBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(AddBtn).ToString())
            SaveBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(SaveBtn).ToString())
            ConfirmBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ConfirmBtn).ToString())
            PrintBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(PrintBtn).ToString())
            CancelBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(CancelBtn).ToString())
            DeleteBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(DeleteBtn).ToString())
            UnDeleteBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(UnDeleteBtn).ToString())
            EditBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(EditBtn).ToString())

            If Not IsPostBack Then

                BindChargeLevelDropDownList()
                BindBillParty("")

                If strSelectedIVID <> "" Then
                    onLoad_Display(strSelectedIVID)
                    onLoad_DisplayLine(strSelectedIVID)
                    onLoad_Button()
                Else
                    BindDocList("")
                    BindPreBlock("", "")
                    BindBlock("", "")
                    BindVehicle("", "")
                    BindVehicleExpense(True, "")
                    BindCurrencyList("")
                    onLoad_Button()
                    TrLink.Visible = False
                End If
                BindAccount("")
            End If
        End If
    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Clear()
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
        If ddlChargeLevel.SelectedIndex = 0 Then
            RowBlk.Visible = False
            RowPreBlk.Visible = True
            hidBlockCharge.Value = "yes"
        Else
            RowBlk.Visible = True
            RowPreBlk.Visible = False
            hidBlockCharge.Value = ""
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfig) = True Then
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_DNDET_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=BI/trx/BI_trx_DNList.aspx")
        End Try

        lblBillParty.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & lblCode.Text
        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

        dgLineDet.Columns(0).HeaderText = lblAccount.Text
        dgLineDet.Columns(1).HeaderText = GetCaption(objLangCap.EnumLangCap.Account) & " Descr."
        dgLineDet.Columns(2).HeaderText = lblBlock.Text
        'dgLineDet.Columns(2).HeaderText = lblVehicle.Text
        'dgLineDet.Columns(3).HeaderText = lblVehExpense.Text

        lblErrBillParty.Text = lblPleaseSelect.Text & lblBillParty.Text
        lblErrAccCode.Text = "<br>" & lblPleaseSelect.Text & lblAccount.Text
        lblErrBlock.Text = lblPleaseSelect.Text & lblBlock.Text
        lblErrVehicle.Text = lblPleaseSelect.Text & lblVehicle.Text
        lblErrVehicleExp.Text = lblPleaseSelect.Text & lblVehExpense.Text
        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTag.Text = PreBlockTag & lblCode.Text & " : "
        lblPreBlockErr.Text = lblPleaseSelect.Text & PreBlockTag & lblCode.Text
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm,
                                                 strCompany,
                                                 strLocation,
                                                 strUserId,
                                                 strAccMonth,
                                                 strAccYear,
                                                 objLangCapDs,
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_DNDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=BI/trx/BI_trx_DNList.aspx")
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

    Sub onLoad_Button()
        Dim intStatus As Integer
        'ddlBillParty.Enabled = False
        txtRemark.Enabled = False
        txtCustRef.Enabled = False
        txtDevRef.Enabled = False
        ddlDocType.Enabled = False
        tblSelection.Visible = False
        SaveBtn.Visible = False
        ConfirmBtn.Visible = False

        CancelBtn.Visible = False
        DeleteBtn.Visible = False
        UnDeleteBtn.Visible = False
        'PrintBtn.Visible = False
        lblDateCreated.Visible = False
        'txtDateCreated.Visible = False
        txtDateCreated.Enabled = False
        btnDateCreated.Visible = False
        ddlCurrency.Enabled = False
        txtExRate.Enabled = False
        'ddlContract.Enabled = False


        If (lblStatusHidden.Text <> "") Then
            intStatus = CInt(lblStatusHidden.Text)
            Select Case intStatus
                Case objBITrx.EnumInvoiceStatus.Active
                    EditBtn.Visible = False
                    txtDateCreated.Enabled = True
                    btnDateCreated.Visible = True
                    txtRemark.Enabled = True
                    txtCustRef.Enabled = True
                    txtDevRef.Enabled = True
                    tblSelection.Visible = True
                    SaveBtn.Visible = True
                    ConfirmBtn.Visible = True
                    DeleteBtn.Visible = True
                    DeleteBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    ddlCurrency.Enabled = True
                    txtExRate.Enabled = True
                    If obInvoiceLnDs.Tables(0).Rows.Count = 0 Then
                        'ddlBillParty.Enabled = True
                        radcmbCust.Enabled = True
                    Else
                        radcmbCust.Enabled = False
                    End If

                Case objBITrx.EnumInvoiceStatus.Deleted
                    UnDeleteBtn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                    UnDeleteBtn.Visible = False

                Case objBITrx.EnumInvoiceStatus.Confirmed
                    If hidReceiptID.Value <> "" Then
                        Exit Sub
                    End If
                    EditBtn.Visible = True
                    CancelBtn.Visible = True
                    CancelBtn.Attributes("onclick") = "javascript:return ConfirmAction('cancel');"
                Case Else
            End Select
            lblDateCreated.Visible = True
        Else
            radcmbCust.Enabled = True
            txtRemark.Enabled = True
            txtCustRef.Enabled = True
            txtDevRef.Enabled = True
            ddlDocType.Enabled = False
            tblSelection.Visible = True
            SaveBtn.Visible = True
            'txtDateCreated.Visible = True
            txtDateCreated.Enabled = True
            btnDateCreated.Visible = True
            ddlCurrency.Enabled = True
            txtExRate.Enabled = True
            ddlContract.Enabled = True
        End If

        If lblInvoiceID.Text.Trim() = "" Then
            txtDateCreated.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
            txtFakturDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
        End If
    End Sub

    Sub BindDocList(ByVal pv_strDoctype As String)
        ddlDocType.Items.Clear()
        ddlDocType.Items.Add(New ListItem(objBITrx.mtdGetInvoiceDocType(objBITrx.EnumInvoiceDocType.Manual), objBITrx.EnumInvoiceDocType.Manual))
        ddlDocType.Items.Add(New ListItem(objBITrx.mtdGetInvoiceDocType(objBITrx.EnumInvoiceDocType.Manual_Millware), objBITrx.EnumInvoiceDocType.Manual_Millware))
        ddlDocType.Items.Add(New ListItem(objBITrx.mtdGetInvoiceDocType(objBITrx.EnumInvoiceDocType.Auto_Millware), objBITrx.EnumInvoiceDocType.Auto_Millware))
        Select Case Trim(pv_strDoctype)
            Case objBITrx.EnumInvoiceDocType.Manual
                ddlDocType.SelectedIndex = 0
            Case objBITrx.EnumInvoiceDocType.Manual_Millware
                ddlDocType.SelectedIndex = 1
            Case objBITrx.EnumInvoiceDocType.Auto_Millware
                ddlDocType.SelectedIndex = 2
            Case Else
                If strSelectedIVID <> "" Then
                    ddlDocType.Items.Add(New ListItem(objBITrx.mtdGetInvoiceDocType(pv_strDoctype), pv_strDoctype))
                    ddlDocType.SelectedIndex = 2
                End If
        End Select
    End Sub

    Sub onLoad_Display(ByVal pv_strInvoiceId As String)
        Dim strOpCd_Get As String = "BI_CLSTRX_INVOICE_DETAILS_GET"
        Dim obInvoiceDs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String = pv_strInvoiceId
        Dim intCnt As Integer = 0

        IVid.Value = pv_strInvoiceId '& "|"

        Try
            intErrNo = objBITrx.mtdGetInvoice(strOpCd_Get,
                                                strCompany,
                                                strLocation,
                                                strUserId,
                                                strAccMonth,
                                                strAccYear,
                                                strParam,
                                                obInvoiceDs,
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_INVOICE_GET_HEADER&errmesg=" & Exp.ToString & "&redirect=BI/trx/BI_trx_DNList.aspx")
        End Try

        lblInvoiceID.Text = pv_strInvoiceId
        lblDocTypeHidden.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("DocType"))
        lblAccPeriod.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(obInvoiceDs.Tables(0).Rows(0).Item("AccYear"))
        lblStatus.Text = objBITrx.mtdGetDebitNoteStatus(Trim(obInvoiceDs.Tables(0).Rows(0).Item("Status")))
        intIVStatus = CInt(Trim(obInvoiceDs.Tables(0).Rows(0).Item("Status")))
        lblStatusHidden.Text = intIVStatus
        lblDateCreated.Text = objGlobal.GetLongDate(obInvoiceDs.Tables(0).Rows(0).Item("CreateDate"))
        txtDateCreated.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), obInvoiceDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(obInvoiceDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblPrintDate.Text = objGlobal.GetLongDate(obInvoiceDs.Tables(0).Rows(0).Item("PrintDate"))
        lblUpdatedBy.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("UserName"))
        lblTotalAmount.Text = FormatNumber(obInvoiceDs.Tables(0).Rows(0).Item("TotalAmount"), CInt(Session("SS_ROUNDNO")))
        lblViewTotalAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(obInvoiceDs.Tables(0).Rows(0).Item("TotalAmountCurrency"), CInt(Session("SS_ROUNDNO"))), CInt(Session("SS_ROUNDNO")))
        txtRemark.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("Remark"))
        txtCustRef.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("CustRef"))
        txtDevRef.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("DeliveryRef"))
        lblCurrency.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("CurrencyCode"))
        txtExRate.Text = obInvoiceDs.Tables(0).Rows(0).Item("ExchangeRate")

        BindCurrencyList(Trim(obInvoiceDs.Tables(0).Rows(0).Item("CurrencyCode")))
        BindDocList(Trim(obInvoiceDs.Tables(0).Rows(0).Item("Doctype")))
        'BindAccount("")
        BindPreBlock("", "")
        BindBlock("", "")
        BindVehicle("", "")
        BindVehicleExpense(True, "")
        txtSeller.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("Seller"))
        BindContractNoList(Trim(obInvoiceDs.Tables(0).Rows(0).Item("BillPartyCode")), Trim(obInvoiceDs.Tables(0).Rows(0).Item("ContractNo")))

        txtUnderName.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("UnderName"))
        txtUnderPost.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("UnderPost"))
        txtFakturNo.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("FakturPajakNo"))
        txtFakturDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), obInvoiceDs.Tables(0).Rows(0).Item("FakturPajakDate"))
        txtAdvAmount.Text = Trim(obInvoiceDs.Tables(0).Rows(0).Item("UsedAdvAmount"))
        hidReceiptID.Value = Trim(obInvoiceDs.Tables(0).Rows(0).Item("ReceiptID"))



        Select Case CInt(obInvoiceDs.Tables(0).Rows(0).Item("DocType"))
            Case objBITrx.EnumDebitNoteDocType.Manual, objBITrx.EnumDebitNoteDocType.Manual_Millware
                ''BindBillParty(Trim(obInvoiceDs.Tables(0).Rows(0).Item("BillPartyCode")))
                radcmbCust.SelectedValue = obInvoiceDs.Tables(0).Rows(0).Item("BillPartyCode")
            Case Else
                BindBillParty("")
                ''ddlBillParty.Items.Add(New ListItem(Trim(obInvoiceDs.Tables(0).Rows(0).Item("BillPartyCode")), Trim(obInvoiceDs.Tables(0).Rows(0).Item("BillPartyCode"))))
        End Select

        hidProdCode.Value = Trim(obInvoiceDs.Tables(0).Rows(0).Item("ProductCode"))
        hidPPN.Value = Trim(obInvoiceDs.Tables(0).Rows(0).Item("PPNInit"))
        If hidPPN.Value = 1 Then
            cbPPN.Checked = True
            cbPPN.Text = "  Yes"
            cbPPN.Enabled = False
        Else
            cbPPN.Checked = False
            cbPPN.Text = "  No"
            cbPPN.Enabled = False
        End If

        If obInvoiceDs.Tables(0).Rows(0).Item("VATExempted") = 1 Then
            chkVATExempted.Checked = True
        Else
            chkVATExempted.Checked = False
        End If

        ddlInvoicetype.SelectedValue = obInvoiceDs.Tables(0).Rows(0).Item("isClosed")

        'If obInvoiceDs.Tables(0).Rows(0).Item("isClosed") = 1 Then
        '    cbClosed.Checked = True
        'Else
        '    cbClosed.Checked = False
        'End If

        GetAdvanceReceiveNo(ddlContract.SelectedItem.Value)
        If Trim(obInvoiceDs.Tables(0).Rows(0).Item("DeliveryRef")) = "1" Then
            BindAccount("")
            chkIsAdvanceRcv.Checked = True
        Else
            chkIsAdvanceRcv.Checked = False
        End If
    End Sub

    Sub onLoad_DisplayLine(ByVal pv_strInvoiceId As String)
        Dim strOpCd_GetLine As String = "BI_CLSTRX_INVOICE_LINE_GET"
        Dim strParam As String = pv_strInvoiceId
        Dim lbButton As LinkButton
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dblDebit As Double = 0
        Dim dblCredit As Double = 0
        Dim strInvRcvLnID As String

        Try
            intErrNo = objBITrx.mtdGetInvoiceLine(strOpCd_GetLine, strParam, obInvoiceLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_DEBITNOTEDET_GET_LINE&errmesg=" & lblErrMessage.Text & "&redirect=BI/trx/BI_trx_DNList.aspx")
        End Try

        chkIsAdvanceRcv.Enabled = True

        For intCnt = 0 To obInvoiceLnDs.Tables(0).Rows.Count - 1
            obInvoiceLnDs.Tables(0).Rows(intCnt).Item("InvoiceLnID") = Trim(obInvoiceLnDs.Tables(0).Rows(intCnt).Item("InvoiceLnID"))
            obInvoiceLnDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(obInvoiceLnDs.Tables(0).Rows(intCnt).Item("AccCode"))
            obInvoiceLnDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(obInvoiceLnDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            obInvoiceLnDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(obInvoiceLnDs.Tables(0).Rows(intCnt).Item("VehCode"))
            obInvoiceLnDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(obInvoiceLnDs.Tables(0).Rows(intCnt).Item("VehExpenseCode"))
            obInvoiceLnDs.Tables(0).Rows(intCnt).Item("Description") = Trim(obInvoiceLnDs.Tables(0).Rows(intCnt).Item("Description"))
            strInvRcvLnID = Trim(obInvoiceLnDs.Tables(0).Rows(intCnt).Item("InvoiceLnID"))
            txtPPHRate.Text = obInvoiceLnDs.Tables(0).Rows(intCnt).Item("PPHRate")
            cbPPN.Checked = IIf(obInvoiceLnDs.Tables(0).Rows(intCnt).Item("PPN") = objBITrx.EnumPPN.Yes, True, False)
            chkIsAdvanceRcv.Enabled = False
            ddlInvoicetype.Enabled = False
        Next intCnt

        dgLineDet.DataSource = obInvoiceLnDs.Tables(0)
        dgLineDet.DataBind()

        If strInvRcvLnID <> "" Then
            cbPPN.Enabled = False
            txtPPHRate.Enabled = False
        Else
            If hidPPN.Value = 1 Then
                cbPPN.Checked = True
            Else
                cbPPN.Checked = False
            End If
            'cbPPN.Enabled = True
            txtPPHRate.Text = "0"
            txtPPHRate.Enabled = True
        End If
        For intCnt = 0 To obInvoiceLnDs.Tables(0).Rows.Count - 1
            Select Case CInt(lblStatusHidden.Text)
                Case objBITrx.EnumInvoiceStatus.Active
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbEdit")
                    lbButton.Visible = True
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbCancel")
                    lbButton.Visible = False
                Case Else
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbEdit")
                    lbButton.Visible = True
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbCancel")
                    lbButton.Visible = False
            End Select
        Next

        If obInvoiceLnDs.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
            ddlContract.Enabled = False
        Else
            TrLink.Visible = False
            ddlContract.Enabled = True
        End If

    End Sub
    Sub BindBillParty(ByVal pv_strCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim dr As DataRow
        Dim strParam As String = "||1||BP.Name|ASC|"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetBillParty(strOpCd, strParam, objBPDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_DEBITNOTEDET_BILLPARTY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = objBPDs.Tables(0).NewRow()
        dr("BillPartyCode") = ""
        ''dr("Name") = lblPleaseSelect.Text & lblBillPartyTag.Text
        objBPDs.Tables(0).Rows.InsertAt(dr, 0)

        radcmbCust.DataSource = objBPDs.Tables(0)
        radcmbCust.DataValueField = "BillPartyCode"
        radcmbCust.DataTextField = "NameDisp"
        radcmbCust.DataBind()
        radcmbCust.SelectedIndex = 0
    End Sub
    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        ' Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim intSelectedIndex As Integer = 0

        strParamName = "SEARCHSTR|SORTEXP"

        Dim IsClosed As Integer = ddlInvoicetype.SelectedValue ''IIf(cbClosed.Checked = True, True, False)
        Dim IsAdvance As Boolean = IIf(chkIsAdvanceRcv.Checked = True, True, False)

        Select Case IsClosed
            Case 2
                If chkIsAdvanceRcv.Checked = False Then
                    strParamValue = "AND Acc.AccCODE LIKE '310.%' AND Acc.Status='1' " & "|" & "Order BY Acc.AccCode ASC"
                Else
                    strParamValue = "AND (Acc.AccCODE LIKE '61.%' or Acc.AccCODE LIKE '65.%' or Acc.AccCODE LIKE '115.%')  AND Acc.Status='1' " & "|" & "Order BY Acc.AccCode ASC"
                End If
            Case 1, 3
                strParamValue = "AND (Acc.AccCODE LIKE '61.%' or Acc.AccCODE LIKE '65.%' or Acc.AccCODE LIKE '115.%')  AND Acc.Status='1' " & "|" & "Order BY Acc.AccCode ASC"
        End Select

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd,
                                                    strParamName,
                                                    strParamValue,
                                                    objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPDATE_PAYMENT&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        ''dr("_Description") = ""
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        radcmbCOA.DataSource = objAccDs.Tables(0)
        radcmbCOA.DataValueField = "AccCode"
        radcmbCOA.DataTextField = "_Description"
        radcmbCOA.DataBind()
        radcmbCOA.SelectedIndex = 0

    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String,
                          ByRef pr_IsBalanceSheet As Boolean,
                          ByRef pr_IsNurseryInd As Boolean,
                          ByRef pr_IsBlockRequire As Boolean,
                          ByRef pr_IsVehicleRequire As Boolean,
                          ByRef pr_IsOthers As Boolean)

        Dim _objAccDs As New Object()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            pr_IsBalanceSheet = False
            pr_IsNurseryInd = False
            pr_IsBlockRequire = False
            pr_IsVehicleRequire = False
            pr_IsOthers = False
            intErrNo = objGLSetup.mtdGetAccount(strOpCd,
                                                strParam,
                                                _objAccDs,
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_DEBITNOTEDET_GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            If CInt(_objAccDs.Tables(0).Rows(0).Item("AccType")) = objGLSetup.EnumAccountType.BalanceSheet Then
                pr_IsBalanceSheet = True
                If CInt(_objAccDs.Tables(0).Rows(0).Item("NurseryInd")) = objGLSetup.EnumNurseryAccount.Yes Then
                    pr_IsNurseryInd = True
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

    Sub onSelect_Account(ByVal Sender As Object, ByVal E As EventArgs)
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strAcc As String = Request.Form("radcmbCOA").Trim
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strPreBlk As String = Request.Form("ddlPreBlock")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")

        GetAccountDetails(radcmbCOA.SelectedValue, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

        If Not blnIsBalanceSheet Then
            If blnIsBlockRequire Then
                BindPreBlock(radcmbCOA.SelectedValue, strPreBlk)
                BindBlock(radcmbCOA.SelectedValue, strBlk)
                BindVehicle("", strVeh)
                BindVehicleExpense(True, strVehExp)
            Else
                BindPreBlock("", strPreBlk)
                BindBlock("", strBlk)
                BindVehicle("", strVeh)
                BindVehicleExpense(True, strVehExp)
            End If

            If blnIsVehicleRequire Then
                BindVehicle(radcmbCOA.SelectedValue, strVeh)
                BindVehicleExpense(False, strVehExp)
            Else
                BindVehicle("", strVeh)
                BindVehicleExpense(True, strVehExp)
            End If

            If blnIsOthers Then
                lblVehicleOption.Text = True
                BindVehicle("%", strVeh)
                BindVehicleExpense(False, strVehExp)
            Else
                lblVehicleOption.Text = False
            End If
        ElseIf blnIsNurseryInd = True Then
            BindPreBlock(radcmbCOA.SelectedValue, strPreBlk)
            BindBlock(radcmbCOA.SelectedValue, strBlk)
            BindVehicle("", strVeh)
            BindVehicleExpense(True, strVehExp)
        Else
            BindPreBlock("", strPreBlk)
            BindBlock("", strBlk)
            BindVehicle("", strVeh)
            BindVehicleExpense(True, strVehExp)
        End If
    End Sub

    Sub BindPreBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim objBlkDs As DataSet
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        strOpCd = "GL_CLSSETUP_ACCOUNT_PREBLOCK_GET"
        intSelectedIndex = 0
        Try
            strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd,
                                                     strParam,
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        'For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
        '    objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode"))
        '    objBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
        '    If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
        '        intSelectedIndex = intCnt + 1
        '    End If
        'Next

        If objBlkDs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblPleaseSelect.Text & PreBlockTag & lblCode.Text

        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlPreBlock.DataSource = objBlkDs.Tables(0)
        ddlPreBlock.DataValueField = "BlkCode"
        ddlPreBlock.DataTextField = "Description"
        ddlPreBlock.DataBind()
        ddlPreBlock.SelectedIndex = intSelectedIndex

        If Not objBlkDs Is Nothing Then
            objBlkDs = Nothing
        End If
    End Sub

    Sub BindBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
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
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd,
                                                     strParam,
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_DEBITNOTEDET_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objBlkDs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblPleaseSelect.Text & lblBlock.Text
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindVehicle(ByVal pv_strAccCode As String, ByVal pv_strVehCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"
            strParam = "|AccCode = '" & pv_strAccCode & "' AND LocCode = '" & Session("SS_LOCATION") & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'"
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd,
                                                   strParam,
                                                   objGLSetup.EnumGLMasterType.Vehicle,
                                                   objVehDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_DEBITNOTEDET_VEH_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objVehDs.Tables(0).Rows.Count - 1
            objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode"))
            objVehDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode")) & " (" & Trim(objVehDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
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
        Dim dr As DataRow
        Dim strParam As String = "Order By VehExpenseCode ASC|"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Try
            If pv_IsBlankList Or ddlVehCode.Items.Count = 1 Then
                strParam += "And Veh.VehExpensecode = ''"
            End If
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd,
                                                   strParam,
                                                   objGLSetup.EnumGLMasterType.VehicleExpense,
                                                   objVehExpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_DEBITNOTEDET_VEHEXPENSE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objVehExpDs.Tables(0).Rows.Count - 1
            objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode"))
            objVehExpDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode")) & " (" & Trim(objVehExpDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
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

    Sub Update_Invoice(ByVal pv_intStatus As Integer, ByRef pr_objNewID As String, ByRef pr_intSuccess As Integer)
        Dim strRemark As String = txtRemark.Text

        Dim strOpCd_Add As String = "BI_CLSTRX_INVOICE_NEW_ADD" '"BI_CLSTRX_INVOICE_ADD"
        Dim strOpCd_Upd As String = "BI_CLSTRX_INVOICE_UPD"
        Dim strOpCodes As String = strOpCd_Add & "|" &
                                   strOpCd_Upd
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim objChkRef As Object
        Dim objInv As Object
        Dim intErrNoRef As Integer
        Dim strParamRef As String = ""
        Dim strOpCd_RefNo As String = "BI_CLSTRX_CHK_REF_NO"
        Dim strFakturDate As String = Date_Validation(txtFakturDate.Text, False)
        Dim strAccMonthRom As String
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)
        Dim strNewIDFormat As String
        Dim intPPN As Integer
        Dim indDate As String = ""
        Dim strOpCd As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim strInvoiceID As String

        If CheckDate(txtDateCreated.Text.Trim(), indDate) = False Then
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

        pr_intSuccess = 1

        lblDate.Visible = False
        lblFmt.Visible = False
        If strSelectedIVID = "" Then
            If txtDateCreated.Text.Trim() = "" Then
                lblFmt.Text = "Please enter Date Created"
                lblFmt.Visible = True
                pr_intSuccess = 0
                Exit Sub
            ElseIf CheckDate(txtDateCreated.Text.Trim(), strDate) = False Then
                lblDate.Visible = True
                lblFmt.Visible = True
                pr_intSuccess = 0
                Exit Sub
            End If
        End If
        If radcmbCust.SelectedValue = "" Then
            lblErrBillParty.Visible = True
            pr_intSuccess = 0
            Exit Sub
        End If

        strCurrency = ddlCurrency.SelectedItem.Value
        strExRate = Trim(txtExRate.Text)
        strSeller = Trim(txtSeller.Text)


        If Month(strDate) < strAccMonth And Year(strDate) <= strAccYear Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)


        intPPN = IIf(hidPPN.Value = 1, objBITrx.EnumPPN.Yes, objBITrx.EnumPPN.No)

        Dim strAdvanceRefNo As String = ""

        '' If Receipt Adavance Payment Exists Then Create Journal to counter Uang Muka without piutang
        If LnkAdvanceRcvNo.Text.Trim <> "" OR chkIsAdvanceRcv.Checked = True Then
            strAdvanceRefNo = "1"
        Else
            strAdvanceRefNo = "0"
        End If

        strParamName = "INVOICEID|LOCCODE|DOCTYPE|BILLPARTYCODE|CUSTREF|DELIVERYREF|REMARK|ACCMONTH|ACCYEAR|STATUS|CREATEDATE|PRINTDATE|UPDATEID|CURRENCYCODE|EXCHANGERATE|SELLER|CONTRACTNO|PRODUCTCODE|UNDERNAME|UNDERPOST|FAKTURPAJAKNO|FAKTURDATE|USEDADVAMOUNT|VATEXEMPTED|ISCLOSED"
        strParamValue = strSelectedIVID & "|" &
                        strLocation & "|" &
                        ddlDocType.SelectedItem.Value & "|" &
                        radcmbCust.SelectedValue & "|" &
                        txtCustRef.Text & "|" &
                        strAdvanceRefNo & "|" &
                        strRemark & "|" &
                        strAccMonth & "|" &
                        strAccYear & "|" &
                        pv_intStatus & "|" &
                        strDate & "|" &
                        Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "|" &
                        strUserId & "|" &
                        strCurrency & "|" &
                        strExRate & "|" &
                        "" & "|" &
                        ddlContract.SelectedItem.Value & "|" &
                        hidProdCode.Value & "|" &
                        txtUnderName.Text.Trim & "|" &
                        txtUnderPost.Text.Trim & "|" &
                        txtFakturNo.Text.Trim & "|" &
                        strFakturDate & "|" &
                        lCDbl(txtAdvAmount.Text) & "|" &
                        IIf(chkVATExempted.Checked = True, 1, 2) & "|" &
                        ddlInvoicetype.SelectedValue

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Add,
                                                strParamName,
                                                strParamValue,
                                                objInv)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        If objInv.Tables(0).Rows.Count > 0 Then
            strInvoiceID = objInv.Tables(0).Rows(0).Item("InvoiceID")

        End If


        pr_objNewID = IIf(strSelectedIVID = "", strInvoiceID, strSelectedIVID)

        If pr_objNewID <> "" Then
            'Dim strOpCd As String = "BI_CLSTRX_INVOICE_ADD_HISTORY"
            'Dim strParamName As String
            'Dim strParamValue As String

            strOpCd = "BI_CLSTRX_INVOICE_ADD_HISTORY"
            strParamName = "LOCCODE|INVOICEID|CONTRACTNO|ACCMONTH|ACCYEAR"
            strParamValue = strLocation & "|" & Trim(pr_objNewID) & "|" & Trim(ddlContract.SelectedItem.Value) & "|" & strAccMonth & "|" & strAccYear

            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd,
                                                        strParamName,
                                                        strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPDATE_PAYMENT&errmesg=" & Exp.ToString() & "&redirect=")
            End Try
        End If

        If pv_intStatus = "1" Then
            strOpCd = "BI_CLSTRX_INVOICE_UPD"
            strParamName = "UPDATESTR"
            strParamValue = "SET CreateDate='" & strDate & "',FakturPajakDate='" & strDate & "' WHERE InvoiceID='" & Trim(pr_objNewID) & "'"

            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd,
                                                        strParamName,
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PAYMENT_UPD&errmesg=" & Exp.ToString() & "&redirect=cb/trx/cb_trx_cashbanklist")
            End Try
        End If

    End Sub
    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("BI_trx_InvoiceDet.aspx")
    End Sub
    Sub AddBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim objIVID As String
        Dim strAccCode As String = radcmbCOA.SelectedValue ''Request.Form("radcmbCOA").Trim
        Dim strBlkCode As String
        Dim strVehCode As String = Request.Form("ddlVehCode")
        Dim strVehExpenseCode As String = Request.Form("ddlVehExpCode")
        Dim strDescription As String = Trim(txtDescription.Text)
        Dim dblTotalUnit As Double
        Dim dblRate As Double
        Dim dblAmount As Double
        Dim strOpCode_AddLine As String = "BI_CLSTRX_INVOICE_LINE_ADD"
        Dim strOpCode_GetSumAmount As String = "BI_CLSTRX_INVOICE_SUM_LINEAMOUNT_GET"
        Dim strOpCode_UpdTotalAmount As String = "BI_CLSTRX_INVOICE_UPD"
        Dim strOpCodes As String = strOpCode_AddLine & "|" & strOpCode_GetSumAmount & "|" & strOpCode_UpdTotalAmount
        Dim intErrNo As Integer
        Dim intSuccess As Integer
        Dim intPPN As Integer
        Dim intPPHRate As Integer

        Dim intAmount As Double
        Dim intPPNAmount As Double
        Dim intPPHAmount As Double
        Dim intNetAmount As Double
        Dim dblPPHRate As Double
        Dim dblPPNAmount As Double
        Dim dblPPHAmount As Double
        Dim dblNetAmount As Double

        Dim dblRateTemp As Double

        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If ddlChargeLevel.SelectedIndex = 1 Then
            strBlkCode = Request.Form("ddlBlock")
        Else
            strBlkCode = Request.Form("ddlPreBlock")
        End If

        'If cbClosed.Checked = False And txtAdvAmount.Text = 0 Then
        '    UserMsgBox(Me, "Please Input Advance Payment Value")
        '    txtAdvAmount.Focus()
        '    Exit Sub
        'End If

        If radcmbCOA.SelectedValue = "" Then
            ''UserMsgBox(Me,"Please Select Account")
            lblErrAccCode.Visible = True
            Exit Sub
        Else
            lblErrAccCode.Visible = False
        End If

        GetAccountDetails(strAccCode, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

        strCurrency = ddlCurrency.SelectedItem.Value
        strExRate = Trim(txtExRate.Text)

        If Not blnIsBalanceSheet Then
            If strAccCode = "" Then
                lblErrAccCode.Visible = True
                Exit Sub
            ElseIf strBlkCode = "" And blnIsBlockRequire = True Then
                If ddlChargeLevel.SelectedIndex = 1 Then
                    lblErrBlock.Visible = True
                Else
                    lblPreBlockErr.Visible = True
                End If
                Exit Sub
            ElseIf strVehCode = "" And blnIsVehicleRequire = True Then
                lblErrVehicle.Visible = True
                Exit Sub
            ElseIf strVehExpenseCode = "" And blnIsVehicleRequire = True Then
                lblErrVehicleExp.Visible = True
                Exit Sub
            ElseIf strVehCode <> "" And strVehExpenseCode = "" And lblVehicleOption.Text = True Then
                lblErrVehicleExp.Visible = True
                Exit Sub
            ElseIf strVehCode = "" And strVehExpenseCode <> "" And lblVehicleOption.Text = True Then
                lblErrVehicle.Visible = True
                Exit Sub
            End If
        ElseIf blnIsNurseryInd = True Then
            If strAccCode = "" Then
                lblErrAccCode.Visible = True
                Exit Sub
            ElseIf strBlkCode = "" Then
                If ddlChargeLevel.SelectedIndex = 1 Then
                    lblErrBlock.Visible = True
                Else
                    lblPreBlockErr.Visible = True
                End If
                Exit Sub
            End If
        End If

        If txtDescription.Text = "" Then
            lblErrDesc.Visible = True
            Exit Sub
        End If

        If lCDbl(txtTotalUnits.Text) = 0 Then
            lblErrTotalUnits.Visible = True
            Exit Sub
        Else
            dblTotalUnit = lCDbl(txtTotalUnits.Text)
        End If

        If lCDbl(txtRate.Text) = 0 Then
            lblErrRate.Visible = True
            Exit Sub
        Else
            dblRate = CDbl(txtRate.Text)
        End If

        If lCDbl(txtAmount.Text) = 0 Then
            lblErrAmount.Visible = True
            Exit Sub
        Else
            dblNetAmount = lCDbl(txtAmount.Text)
            dblNetAmount = Round(dblTotalUnit * dblRate, CInt(Session("SS_ROUNDNO")))
            txtAmount.Text = dblNetAmount
            'txtAmount.Text = Round(dblTotalUnit * dblRate, CInt(Session("SS_ROUNDNO")))
        End If

        If lCDbl(txttotal.Text) = 0 Then
            UserMsgBox(Me, "Invoice Amount Not Found")
            txttotal.Focus()
            Exit Sub
        End If

        If strSelectedIVID = "" Then
            Update_Invoice(objBITrx.EnumInvoiceStatus.Active, objIVID, intSuccess)
            If intSuccess = 1 Then
                If UCase(TypeName(objIVID)) = "OBJECT" Then
                    Exit Sub
                Else
                    strSelectedIVID = objIVID
                End If
            Else
                Exit Sub
            End If
        Else

            Update_Invoice(IIf(lblStatusHidden.Text.Trim = "", "1", lblStatusHidden.Text.Trim), strSelectedIVID, intSuccess)
        End If

        intPPN = IIf(hidPPN.Value = 1, objBITrx.EnumPPN.Yes, objBITrx.EnumPPN.No)
        intPPHRate = IIf(txtPPHRate.Text.Trim <> "", txtPPHRate.Text.Trim, "0")
        intNetAmount = lCDbl(txtAmount.Text)

        dblNetAmount = intNetAmount


        intPPNAmount = lCDbl(txtppnamount.Text)
        intAmount = objBITrx.RoundNumber(intNetAmount + intPPNAmount - intPPHAmount, 2)

        dblPPHRate = intPPHRate
        dblPPNAmount = intPPNAmount
        dblPPHAmount = intPPHAmount
        dblAmount = intAmount

        Dim strParam As String = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.ContractInvoiceLn) & "|" &
                                 strSelectedIVID & "|" &
                                 strAccCode & "|" &
                                 strBlkCode & "|" &
                                 strVehCode & "|" &
                                 strVehExpenseCode & "|" &
                                 txtDescription.Text & "|" &
                                 dblTotalUnit & "|" &
                                 dblRate & "|" &
                                 dblAmount & "|" &
                                 dblPPHRate & "|" &
                                 intPPN & "|" &
                                 dblPPNAmount & "|" &
                                 dblPPHAmount & "|" &
                                 dblNetAmount & "|" &
                                 strCurrency & "|" &
                                 strExRate & "|" &
                                 hidIRLnID.Value

        Try
            If ddlChargeLevel.SelectedIndex = 0 And RowPreBlk.Visible = True Then
                strParamList = Session("SS_LOCATION") & "|" &
                                       radcmbCOA.SelectedValue & "|" &
                                       ddlPreBlock.SelectedItem.Value.Trim & "|" &
                                       objGLSetup.EnumBlockStatus.Active & "|" &
                                       strAccMonth & "|" & strAccYear

                intErrNo = objBITrx.mtdUpdInvoiceLineByBlock(strOpCodeGLSubBlkByBlk,
                                                             strParamList,
                                                             strOpCodes,
                                                             strCompany,
                                                             strLocation,
                                                             strUserId,
                                                             strAccMonth,
                                                             strAccYear,
                                                             strParam,
                                                             strLocType)
            Else
                intErrNo = objBITrx.mtdUpdInvoiceLine(strOpCodes,
                                                      strCompany,
                                                      strLocation,
                                                      strUserId,
                                                      strAccMonth,
                                                      strAccYear,
                                                      strParam)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_INVOICEDET_ADD_LINE&errmesg=" & lblErrMessage.Text & "&redirect=BI/trx/BI_trx_InvoiceList.aspx")
        End Try


        onLoad_Display(strSelectedIVID)
        onLoad_DisplayLine(strSelectedIVID)
        onLoad_Button()
        txtppnamount.Text = 0
        txttotal.Text = 0
        txtTotalUnits.Text = 0
        txtRate.Text = 0
        txtAmount.Text = 0
        txtDescription.Text = ""
        radcmbCOA.SelectedIndex = 0
    End Sub

    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = ""
        Dim objIVID As String
        Dim intSuccess As Integer

        If strSelectedIVID = "" Then
            Exit Sub
        End If

        Update_Invoice(objBITrx.EnumInvoiceStatus.Active, objIVID, intSuccess)
        If intSuccess = 1 Then
            onLoad_Display(objIVID)
            onLoad_DisplayLine(objIVID)
            onLoad_Button()
        Else
            Exit Sub
        End If
    End Sub

    Sub ConfirmBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objIVID As String
        Dim intSuccess As Integer
        Update_Invoice(objBITrx.EnumInvoiceStatus.Active, objIVID, intSuccess)

        Dim intErrNo As Integer
        Dim strOpCd_Upd As String = "BI_CLSTRX_INVOICE_UPD"

        If Len(lblInvoiceID.Text.Trim) > 0 Then
            Dim strOpCd As String = "BI_CLSTRX_INVOICE_GLOBAL_PROCEDURE"
            Dim strParamName As String
            Dim strParamValue As String

            strParamName = "STRSEARCH"
            strParamValue = "UPDATE BI_INVOICE SET Status='2' Where InvoiceID='" & lblInvoiceID.Text & "'"

            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd,
                                                        strParamName,
                                                        strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPDATE_PAYMENT&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            onLoad_Display(lblInvoiceID.Text.Trim)
            onLoad_DisplayLine(lblInvoiceID.Text.Trim)
            onLoad_Button()

        End If

    End Sub

    Sub CancelBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objIVID As String
        Dim intSuccess As Integer
        Dim intErrNo As Integer

        If strSelectedIVID = "" Then
            Exit Sub
        End If

        If GetIsUseInvoiceID(lblInvoiceID.Text.Trim) = True Then
            UserMsgBox(Me, "Denied...!!! Other Invoice With Contract " & ddlContract.SelectedItem.Value & " has Created!!!")
            Exit Sub
        End If

        Update_Invoice(objBITrx.EnumInvoiceStatus.Cancelled, objIVID, intSuccess)

        Dim strOpCd As String = "BI_CLSTRX_INVOICE_CONFIRM_ADD"
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "STRSEARCH"
        strParamValue = "DELETE BI_INVOICE_HISTORY Where InvoiceID='" & lblInvoiceID.Text & "'"

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd,
                                                    strParamName,
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPDATE_PAYMENT&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If intSuccess = 1 Then
            onLoad_Display(objIVID)
            onLoad_DisplayLine(objIVID)
            onLoad_Button()
        Else
            Exit Sub
        End If
    End Sub

    Sub EditBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim objIVID As String
        Dim intSuccess As Integer

        If strSelectedIVID = "" Then
            Exit Sub
        End If

        If intLevel >= 3 Then
            Update_Invoice(objBITrx.EnumInvoiceStatus.Active, objIVID, intSuccess)
            If intSuccess = 1 Then
                onLoad_Display(objIVID)
                onLoad_DisplayLine(objIVID)
                onLoad_Button()
            Else
                Exit Sub
            End If
        Else
            UserMsgBox(Me, "Acsess Denied")
        End If

    End Sub


    Sub DeleteBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objIVID As String
        Dim intSuccess As Integer
        Dim intErrNo As Integer

        If Len(lblInvoiceID.Text.Trim) > 0 Then
            If GetIsUseInvoiceID(lblInvoiceID.Text.Trim) = True Then
                UserMsgBox(Me, "Denied...!!! Other Invoice With Contract " & ddlContract.SelectedItem.Value & " has Created!!!")
                Exit Sub
            End If

            Update_Invoice(objBITrx.EnumInvoiceStatus.Deleted, objIVID, intSuccess)


            Dim strOpCd As String = "BI_CLSTRX_INVOICE_GLOBAL_PROCEDURE"
            Dim strParamName As String
            Dim strParamValue As String

            strParamName = "STRSEARCH"
            strParamValue = "DELETE BI_INVOICE_HISTORY Where InvoiceID='" & lblInvoiceID.Text & "'"

            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd,
                                                        strParamName,
                                                        strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPDATE_PAYMENT&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If intSuccess = 1 Then
                onLoad_Display(objIVID)
                onLoad_DisplayLine(objIVID)
                onLoad_Button()
            Else
                Exit Sub
            End If
        End If
    End Sub

    Sub UnDeleteBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objIVID As String
        Dim intSuccess As Integer

        'Update_Invoice(objBITrx.EnumInvoiceStatus.Active, objIVID, intSuccess)
        If intSuccess = 1 Then
            onLoad_Display(objIVID)
            onLoad_DisplayLine(objIVID)
            onLoad_Button()
        Else
            Exit Sub
        End If
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCode_DelLine As String = "BI_CLSTRX_INVOICE_LINE_DEL"
        Dim strOpCode_GetSumAmount As String = "BI_CLSTRX_INVOICE_SUM_LINEAMOUNT_GET"
        Dim strOpCode_UpdTotalAmount As String = "BI_CLSTRX_INVOICE_TOTALAMOUNT_UPD"
        Dim strOpCodes = strOpCode_DelLine & "|" & strOpCode_GetSumAmount & "|" & strOpCode_UpdTotalAmount
        Dim strParam As String
        Dim lblDelText As Label
        Dim strLNId As String
        Dim intErrNo As Integer

        If GetIsUseInvoiceID(lblInvoiceID.Text.Trim) = True Then
            UserMsgBox(Me, "Denied...!!! Other Invoice With Contract " & ddlContract.SelectedItem.Value & " has Created!!!")
            Exit Sub
        End If

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("IVlnid")
        strLNId = lblDelText.Text

        strExRate = Trim(txtExRate.Text)
        Try
            strParam = strLNId & "|" & strSelectedIVID & "|" & strExRate
            intErrNo = objBITrx.mtdDelInvoiceLine(strOpCodes,
                                                    strCompany,
                                                    strLocation,
                                                    strUserId,
                                                    strAccMonth,
                                                    strAccYear,
                                                    strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_INVOICEDET_DEL_LINE&errmesg=" & lblErrMessage.Text & "&redirect=BI/trx/BI_trx_InvoiceList.aspx")
        End Try


        Dim strOpCd As String = "BI_CLSTRX_INVOICE_GLOBAL_PROCEDURE"
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "STRSEARCH"
        strParamValue = "DELETE BI_INVOICE_HISTORY Where InvoiceID='" & lblInvoiceID.Text & "'"

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd,
                                                    strParamName,
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPDATE_PAYMENT&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        onLoad_Display(strSelectedIVID)
        onLoad_DisplayLine(strSelectedIVID)
        onLoad_Button()
        BindContractNoList(radcmbCust.SelectedValue, "")
        ddlContract.Enabled = True

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If lblReferer.Text = "" Then
            Response.Redirect("BI_trx_InvoiceList.aspx")
        Else
            Response.Redirect(lblReferer.Text)
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

    Sub BindCurrencyList(ByVal pv_strCurrencyCode As String)
        Dim strParam As String
        Dim strSearch As String
        Dim strSort As String
        Dim strOpCdGet As String = "CM_CLSSETUP_CURRENCY_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer
        Dim objCurrencyDs As New Object()

        strSearch = "and curr.Status = '" & objCMSetup.EnumCurrencyStatus.Active & "' "
        strSort = "order by curr.CurrencyCode "

        strParam = strSearch & "|" & strSort

        Try
            intErrNo = objCMSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objCurrencyDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_CURRENCYLIST_GET&errmesg=" & Exp.ToString() & "&redirect=CM/Trx/CM_Trx_ContractRegList.aspx")
        End Try

        'intSelectedIndex = 1
        If pv_strCurrencyCode = "" Then
            pv_strCurrencyCode = "IDR"
        End If
        If objCurrencyDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objCurrencyDs.Tables(0).Rows.Count - 1
                objCurrencyDs.Tables(0).Rows(intCnt).Item("CurrencyCode") = Trim(objCurrencyDs.Tables(0).Rows(intCnt).Item("CurrencyCode"))
                objCurrencyDs.Tables(0).Rows(intCnt).Item("Description") = objCurrencyDs.Tables(0).Rows(intCnt).Item("CurrencyCode")
                If objCurrencyDs.Tables(0).Rows(intCnt).Item("CurrencyCode") = pv_strCurrencyCode Then
                    intSelectedIndex = intCnt
                End If
            Next
        End If


        ddlCurrency.DataSource = objCurrencyDs.Tables(0)
        ddlCurrency.DataValueField = "CurrencyCode"
        ddlCurrency.DataTextField = "Description"
        ddlCurrency.DataBind()
        ddlCurrency.SelectedIndex = intSelectedIndex
    End Sub

    Sub reCalculate_Amount(ByVal Sender As Object, ByVal E As EventArgs)
        'khusus Kalirejo
        Dim dblRate As Double
        Dim dblAmount As Double

        If strCompany = "SAM" Then
            dblRate = 0
            dblAmount = 0
            If cbPPN.Checked = True Then
                dblRate = Val(txtRate.Text) + (Val(txtRate.Text) * ((Session("SS_PPNRATE")) / 100))
                dblAmount = Val(txtTotalUnits.Text) * CDbl(FormatNumber(dblRate, 0))
                txtAmount.Text = Format((dblAmount / ((Session("SS_PPNRATE") + 100) / 100)), "###.##")
            Else
                dblAmount = Val(txtTotalUnits.Text) * Val(txtRate.Text)
                txtAmount.Text = dblAmount
            End If
        End If
    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcceptFormat As String

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam,
                                                  strCompany,
                                                  strLocation,
                                                  strUserId,
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat,
                                           pv_strInputDate,
                                           strAcceptFormat,
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Sub onChanged_BillParty(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strParam As String
        Dim strOpCdGet As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer
        Dim strBillPartyCode As String = radcmbCust.SelectedValue 'Request.Form("ddlBillParty")

        strParam = strBillPartyCode & "|" &
                   "" & "|" &
                   objGLSetup.EnumBillPartyStatus.Active & "|" &
                   "" & "|" &
                   "BP.BillPartyCode" & "|" &
                   "ASC" & "|"
        Try
            intErrNo = objGLSetup.mtdGetBillParty(strOpCdGet, strParam, objContractDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_BUYERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=CM/Trx/CM_Trx_ContractRegList.aspx")
        End Try

        If objContractDs.Tables(0).Rows.Count > 0 Then
            hidPPN.Value = Trim(objContractDs.Tables(0).Rows(0).Item("PPNInit"))

            If Trim(objContractDs.Tables(0).Rows(0).Item("PPNInit")) = "0" Then
                cbPPN.Checked = False
                cbPPN.Text = "  No"
                cbPPN.Enabled = False
            Else
                cbPPN.Checked = True
                cbPPN.Text = "  Yes"
                cbPPN.Enabled = False
            End If
        End If

        BindContractNoList(radcmbCust.SelectedValue, "")
    End Sub

    Sub BindContractNoList(ByVal pv_strBuyer As String, ByVal pv_strContNo As String)
        Dim strParam As String
        Dim strOpCdGet As String '= "CM_CLSTRX_CONTRACT_REG_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer
        Dim strSearch As String

        If Trim(pv_strContNo) = "" Then
            strOpCdGet = "CM_CLSTRX_CONTRACT_REG_GET_EXIST"
            If Trim(lblInvoiceID.Text) = "" Then
                strSearch = "ctr.LocCode = '" & strLocation & "' and ctr.BuyerCode like '%" & pv_strBuyer & "' and ctr.status in ('1') "
                strParam = strSearch & "|" & ""
            Else
                strSearch = "ctr.LocCode = '" & strLocation & "' and ctr.BuyerCode like '%" & pv_strBuyer & "'"
                strParam = strSearch & "|" & ""
            End If
        Else
            strOpCdGet = "CM_CLSTRX_CONTRACT_REG_GET"

            If Trim(lblInvoiceID.Text) = "" Then
                strSearch = "and ctr.LocCode = '" & strLocation & "' and ctr.BuyerCode like '%" & pv_strBuyer & "' and ctr.status in ('1') "
                strParam = strSearch & "|" & ""
            Else
                strSearch = "and ctr.LocCode = '" & strLocation & "' and ctr.BuyerCode like '%" & pv_strBuyer & "'"
                strParam = strSearch & "|" & ""
            End If
        End If

        Try
            intErrNo = objCMTrx.mtdGetContract(strOpCdGet, strParam, 0, objContractDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=CM/Trx/CM_Trx_ContractRegList.aspx")
        End Try

        If objContractDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objContractDs.Tables(0).Rows.Count - 1
                objContractDs.Tables(0).Rows(intCnt).Item("ContractNo") = Trim(objContractDs.Tables(0).Rows(intCnt).Item("ContractNo"))
                If objContractDs.Tables(0).Rows(intCnt).Item("ContractNo") = pv_strContNo Then
                    intSelectedIndex = intCnt + 1
                End If
                objContractDs.Tables(0).Rows(intCnt).Item("ContractDescr") = objContractDs.Tables(0).Rows(intCnt).Item("ContractDescr")
            Next
        End If

        dr = objContractDs.Tables(0).NewRow()
        dr("ContractNo") = ""
        dr("ContractDescr") = "Please Select Contract No"
        objContractDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlContract.DataSource = objContractDs.Tables(0)
        ddlContract.DataValueField = "ContractNo"
        ddlContract.DataTextField = "ContractDescr"
        ddlContract.DataBind()
        ddlContract.SelectedIndex = intSelectedIndex
    End Sub
    Sub onChanged_ContractNo(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strParam As String
        Dim strOpCdGet As String = "CM_CLSTRX_CONTRACT_REG_GET"
        Dim strOpCdGet_DO As String = "CM_CLSTRX_DO_REG_QTYAMOUNT_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer
        Dim strSearch As String
        Dim pv_strContNo As String = ddlContract.SelectedItem.Value
        Dim dblQtyAmount As Double
        Dim strProdCode As String

        strSearch = "WHERE LocCode = '" & strLocation & "' and ContractNo like '%" & pv_strContNo & "' and status in ('1', '4') "
        strSearch = strSearch & " group by contractno "

        strParam = strSearch & "|" & ""

        Try
            intErrNo = objCMTrx.mtdGetContract(strOpCdGet_DO, strParam, 0, objContractDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=CM/Trx/CM_Trx_DORegistrationDet.aspx")
        End Try

        If objContractDs.Tables(0).Rows.Count > 0 Then
            dblQtyAmount = Trim(objContractDs.Tables(0).Rows(0).Item("qtyamount"))
        Else
            dblQtyAmount = 0
        End If

        strSearch = "and ctr.LocCode = '" & strLocation & "' and ctr.ContractNo like '%" & pv_strContNo & "' "
        strParam = strSearch & "|" & ""

        Try
            intErrNo = objCMTrx.mtdGetContract(strOpCdGet, strParam, 0, objContractDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=CM/Trx/CM_Trx_DORegistrationDet.aspx")
        End Try

        If objContractDs.Tables(0).Rows.Count > 0 Then
            txtTotalUnits.Text = Trim(objContractDs.Tables(0).Rows(0).Item("ContractQty"))
            If Trim(objContractDs.Tables(0).Rows(0).Item("PPNInit")) = "1" Then
                'txtRate.Text = Trim(Round(objContractDs.Tables(0).Rows(0).Item("Price") / ((Session("SS_PPNRATE")+100)/100), CInt(Session("SS_ROUNDNO"))))
                'hidRate.Value = objContractDs.Tables(0).Rows(0).Item("Price") / ((Session("SS_PPNRATE")+100)/100)
                txtRate.Text = Trim(objContractDs.Tables(0).Rows(0).Item("Price"))
                hidRate.Value = objContractDs.Tables(0).Rows(0).Item("Price")
            Else
                txtRate.Text = Trim(objContractDs.Tables(0).Rows(0).Item("Price"))
                hidRate.Value = objContractDs.Tables(0).Rows(0).Item("Price")
            End If

            dblQtyAmount = Round(CDbl(txtTotalUnits.Text) * CDbl(txtRate.Text), CInt(Session("SS_ROUNDNO")))
            txtAmount.Text = Trim(dblQtyAmount)
            hidProdCode.Value = Trim(objContractDs.Tables(0).Rows(0).Item("ProductCode"))

            Select Case hidProdCode.Value
                Case objWMTrx.EnumWeighBridgeTicketProduct.CPO
                    hidProdType.Value = "CPO"
                Case objWMTrx.EnumWeighBridgeTicketProduct.PK
                    hidProdType.Value = "KNL"
                Case objWMTrx.EnumWeighBridgeTicketProduct.FFB
                    hidProdType.Value = "FFB"
                Case objWMTrx.EnumWeighBridgeTicketProduct.AbuJanjang
                    hidProdType.Value = "ABJ"
                Case objWMTrx.EnumWeighBridgeTicketProduct.MinyakLimbah
                    hidProdType.Value = "LMB"
                Case objWMTrx.EnumWeighBridgeTicketProduct.Shell
                    hidProdType.Value = "CKG"
                Case objWMTrx.EnumWeighBridgeTicketProduct.EFB
                    hidProdType.Value = "EFB"
                Case objWMTrx.EnumWeighBridgeTicketProduct.Others
                    hidProdType.Value = "OTH"
            End Select

            If Trim(objContractDs.Tables(0).Rows(0).Item("PPNInit")) = "0" Then
                cbPPN.Checked = False
                cbPPN.Text = "  No"
                cbPPN.Enabled = False
                chkVATExempted.Checked = True
                txtppnamount.Text = 0
            Else
                chkVATExempted.Checked = False

                cbPPN.Checked = True
                cbPPN.Text = "  Yes"
                cbPPN.Enabled = False
                If lCDbl(txtAmount.Text) > 0 Then
                    txtppnamount.Text = lCDbl(txtAmount.Text) * 0.11
                Else
                    txtppnamount.Text = 0
                End If
            End If

            txttotal.Text = lCDbl(txtAmount.Text) + lCDbl(txtppnamount.Text)
            If Trim(objContractDs.Tables(0).Rows(0).Item("AdvDocID")) = "" Then
                txtAdvAmount.Enabled = True
            Else
                txtAdvAmount.Enabled = False
                txtAdvAmount.Text = objContractDs.Tables(0).Rows(0).Item("AdvAmount")
            End If
        End If

        GetAdvanceReceiveNo(ddlContract.SelectedItem.Value)
    End Sub

    Sub GetAdvanceReceiveNo(ByVal pConractNo As String)
        '''---------GET 
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim nReturnAdvanceNo As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        sSQLKriteria = "Select Ln.ReceiptID,LN.AmountCurrency From CB_RECEIPTLN LN Inner Join CB_RECEIPT H On H.ReceiptID=LN.ReceiptID  Where Ln.DocType='6'  And H.Status IN ('2') And DocID='" & pConractNo & "'"
        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get,
                                         strParamName,
                                         strParamValue,
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objdsST.Tables(0).Rows.Count > 0 Then
            LnkAdvanceRcvNo.Text = objdsST.Tables(0).Rows(0).Item("ReceiptID")
            chkIsAdvanceRcv.Checked = True
        Else
            chkIsAdvanceRcv.Checked = False
            LnkAdvanceRcvNo.Text = ""
        End If

    End Sub

    Function GetIsUseInvoiceID(ByVal pInvoiceID) As Boolean
        Dim nUseValue As Boolean = False
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objTransDs As New DataSet

        Dim strOpCd As String = "BI_CLSTRX_INVOICE_GLOBAL_PROCEDURE"

        nUseValue = False
        strParamName = "STRSEARCH"
        strParamValue = "SELECT InvoiCeID FROM BI_INVOICE_HISTORY Where ContractNo='" & ddlContract.SelectedItem.Value & "' AND InVoiceID <> '" & lblInvoiceID.Text.Trim & "' " &
                            " AND NOUrut > (Select NoUrut FROM BI_INVOICE_HISTORY Where InVoiceID = '" & lblInvoiceID.Text.Trim & "')"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd,
                                                strParamName,
                                                strParamValue,
                                                objTransDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try


        If objTransDs.Tables(0).Rows.Count > 0 Then
            nUseValue = True
        End If

        Return nUseValue

    End Function

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

    Sub PrintBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strInvoiceID As String = strSelectedIVID
        Dim strOptionNo As String
        Dim strOptionDesc As String
        Dim strFakturDate As String = Date_Validation(txtFakturDate.Text, False)

        If Opt1.Checked Then
            strOptionNo = "1"
            strOptionDesc = "Untuk pembeli BKP/penerima JKP sebagai bukti pajak masukan"
        ElseIf Opt2.Checked Then
            strOptionNo = "2"
            strOptionDesc = "Untuk PKP yang menerbitkan Faktur Pajak Standar sebagai bukti pajak keluaran"
        Else
            strOptionNo = "3"
            strOptionDesc = "Arsip"
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/BI_Rpt_InvoiceDet.aspx?InvoiceId=" & Server.UrlEncode(strInvoiceID) &
                        "&ContractNo=" & Server.UrlEncode(ddlContract.SelectedItem.Value) &
                        "&Product=" & Server.UrlEncode(hidProdCode.Value) &
                        "&PPN=" & Server.UrlEncode(hidPPN.Value) &
                        "&FakturPajakNo=" & Server.UrlEncode(txtFakturNo.Text) &
                        "&FakturPajakDate=" & Server.UrlEncode(strFakturDate) &
                        "&OptionNo=" & Server.UrlEncode(strOptionNo) &
                        "&OptionDesc=" & Server.UrlEncode(strOptionDesc) &
                        "&strExportToExcel=" & IIf(cbExcel.Checked, "1", "0") &
                        """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub PrintFaktur_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strInvoiceID As String = strSelectedIVID
        Dim strOptionNo As String
        Dim strOptionDesc As String
        Dim strFakturDate As String = Date_Validation(txtFakturDate.Text, False)

        If Opt1.Checked Then
            strOptionNo = "1"
            strOptionDesc = "Untuk pembeli BKP/penerima JKP sebagai bukti pajak masukan"
        ElseIf Opt2.Checked Then
            strOptionNo = "2"
            strOptionDesc = "Untuk PKP yang menerbitkan Faktur Pajak Standar sebagai bukti pajak keluaran"
        Else
            strOptionNo = "3"
            strOptionDesc = "Arsip"
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/BI_Rpt_FakturPajak.aspx?InvoiceId=" & Server.UrlEncode(strInvoiceID) &
                        "&ContractNo=" & Server.UrlEncode(ddlContract.SelectedItem.Value) &
                        "&Product=" & Server.UrlEncode(hidProdCode.Value) &
                        "&PPN=" & Server.UrlEncode(hidPPN.Value) &
                        "&FakturPajakNo=" & Server.UrlEncode(txtFakturNo.Text) &
                        "&FakturPajakDate=" & Server.UrlEncode(strFakturDate) &
                        "&OptionNo=" & Server.UrlEncode(strOptionNo) &
                        "&OptionDesc=" & Server.UrlEncode(strOptionDesc) &
                        """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub PreviewKwitansiBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strInvoiceID As String = strSelectedIVID
        Dim strOptionNo As String
        Dim strOptionDesc As String
        Dim strFakturDate As String = Date_Validation(txtFakturDate.Text, False)

        If Opt1.Checked Then
            strOptionNo = "1"
            strOptionDesc = "Untuk pembeli BKP/penerima JKP sebagai bukti pajak masukan"
        ElseIf Opt2.Checked Then
            strOptionNo = "2"
            strOptionDesc = "Untuk PKP yang menerbitkan Faktur Pajak Standar sebagai bukti pajak keluaran"
        Else
            strOptionNo = "3"
            strOptionDesc = "Arsip"
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/BI_Rpt_InvoiceVoucher.aspx?InvoiceId=" & Server.UrlEncode(strInvoiceID) &
                        "&ContractNo=" & Server.UrlEncode(ddlContract.SelectedItem.Value) &
                        "&Product=" & Server.UrlEncode(hidProdCode.Value) &
                        "&PPN=" & Server.UrlEncode(hidPPN.Value) &
                        "&FakturPajakNo=" & Server.UrlEncode(txtFakturNo.Text) &
                        "&FakturPajakDate=" & Server.UrlEncode(strFakturDate) &
                        "&OptionNo=" & Server.UrlEncode(strOptionNo) &
                        "&OptionDesc=" & Server.UrlEncode(strOptionDesc) &
                        """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Private Sub DEDR_Cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgLineDet.CancelCommand
        onLoad_DisplayLine(lblInvoiceID.Text.Trim)
        onLoad_Button()
        txtTotalUnits.Text = 0
        txtRate.Text = 0
        txtAmount.Text = 0
        txtDescription.Text = ""
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs) Handles dgLineDet.EditCommand

        Dim lbl As Label
        Dim cButton As LinkButton
        Dim strIRLNId As String
        Dim strPOLNId As String
        Dim strQty As String
        Dim strAccCode As String
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)
        Dim indDate As String = ""


        If CheckDate(txtDateCreated.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblDate.Visible = True
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
        tblSelection.Visible = True
        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lbl = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblInvoiceLnID")
        strIRLNId = lbl.Text
        hidIRLnID.Value = lbl.Text
        lbl = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblDescription")
        txtDescription.Text = lbl.Text
        lbl = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblUnit")
        txtTotalUnits.Text = lbl.Text
        lbl = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCost")
        txtRate.Text = lbl.Text

        lbl = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblNetAmount")
        'txtAmount.Text = lbl.Text
        txtAmount.Text = txtTotalUnits.Text * txtRate.Text
        lbl = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblPPNAmount")
        txtppnamount.Text = lbl.Text
        lbl = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAmount")
        txttotal.Text = lbl.Text

        lbl = E.Item.FindControl("lblAccCode")
        strAccCode = lbl.Text.Trim

        radcmbCOA.SelectedValue = strAccCode
        'onSelect_Account(sender, e)
        lbl = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblPPNAmount")
        hidPPNValue.Value = CDbl(lbl.Text)
        If CDbl(lbl.Text) = 0 Then
            cbPPN.Checked = False
        Else
            cbPPN.Checked = True
        End If


        BindPreBlock(strAccCode, "")
        BindBlock(strAccCode, "")


        cButton = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lbEdit")
        cButton.Visible = False
        cButton = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lbDelete")
        cButton.Visible = False
        cButton = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lbCancel")
        cButton.Visible = True


        If intLevel < 3 And lblStatusHidden.Text = "2" Then
            txtAmount.Enabled = False
            txtRate.Enabled = False
            txtTotalUnits.Enabled = False
        Else
            txtAmount.Enabled = True
            txtRate.Enabled = True
            txtTotalUnits.Enabled = True
        End If
    End Sub

    Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Sub onSelect_StrAccCode(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetCOADetail(radcmbCOA.SelectedValue)
        onSelect_Account(sender, e)
    End Sub

    Sub GetCOADetail(ByVal pv_strCode As String)
        'Dim dr As DataRow
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0


        Dim strOpCode As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim objCOADs As New DataSet
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = " And ACC.AccCode = '" & Trim(pv_strCode) & "'  " & "|Order By ACC.AccCode"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode,
                                                strParamName,
                                                strParamValue,
                                                objCOADs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        If objCOADs.Tables(0).Rows.Count > 0 Then
            radcmbCOA.SelectedValue = objCOADs.Tables(0).Rows(0).Item("AccCode")
            'txtAccName.Text = objCOADs.Tables(0).Rows(0).Item("Description")
        Else
            radcmbCOA.SelectedIndex = 0
            'txtAccName.Text = ""
        End If
    End Sub

    Sub Closed_Changed(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        BindAccount("")

        If lblInvoiceID.Text <> "" Then
            Dim strOpCd As String = "BI_CLSTRX_INVOICE_UPD_HISTORY_CLOSED"
            Dim strParamName As String
            Dim strParamValue As String
            Dim intErrNo As Integer

            strParamName = "LOCCODE|INVOICEID|CONTRACTNO|ACCMONTH|ACCYEAR|ISCLOSED|USERID"
            strParamValue = strLocation & "|" & Trim(lblInvoiceID.Text) & "|" & Trim(ddlContract.SelectedItem.Value) & "|" & strAccMonth & "|" & strAccYear & _
                            "|" & ddlInvoicetype.SelectedValue & "|" & Trim(strUserId)

            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd, _
                                                        strParamName, _
                                                        strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPDATE_PAYMENT&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            onLoad_Display(lblInvoiceID.Text)
            onLoad_DisplayLine(lblInvoiceID.Text)
            onLoad_Button()
        End If
    End Sub

    Sub AdvanceReceive_Change(ByVal Sender As Object, ByVal E As EventArgs)
        BindAccount("")
    End Sub
    Private Sub lbViewJournal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbViewJournal.Click
        Dim intErrNo As Integer
        Dim dsResult As New Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "GL_JOURNAL_PREDICTION_BI"
        Dim arrPeriod As Array

        arrPeriod = Split(lblAccPeriod.Text, "/")

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|USERID|TRXID|TRANS"
        strParamValue = strLocation & "|" & arrPeriod(0) &
                        "|" & arrPeriod(1) & "|" &
                        Session("SS_USERID") & "|" & Trim(lblInvoiceID.Text) & "|" & "INVOICE"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
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
            lblTotalDB.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TotalDB, CInt(Session("SS_ROUNDNO"))), CInt(Session("SS_ROUNDNO")))
            lblTotalCR.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TotalCR, CInt(Session("SS_ROUNDNO"))), CInt(Session("SS_ROUNDNO")))

            dgViewJournal.DataSource = Nothing
            dgViewJournal.DataSource = dsResult.Tables(0)
            dgViewJournal.DataBind()

            lblTotalDB.Visible = True
            lblTotalCR.Visible = True
            lblTotalViewJournal.Visible = True
            lblTotalViewJournal.Text = "Total Amount : "
        End If

        onLoad_Display(lblInvoiceID.Text)
        onLoad_DisplayLine(lblInvoiceID.Text)
        onLoad_Button()
    End Sub

	Sub dgLine_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

    Protected Sub LnkAdvanceRcvNo_Click(sender As Object, e As EventArgs) Handles LnkAdvanceRcvNo.Click
        Response.Write("<Script Language=""JavaScript"">window.open(""../../cb/trx/cb_trx_ReceiptDet.aspx?receiptid=" & LnkAdvanceRcvNo.Text.Trim & """, null ,""status=yes,width=1000,height=500,top=100,left=100, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class

