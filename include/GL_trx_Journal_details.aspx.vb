
Imports System
Imports System.Collections
Imports System.Data
Imports System.Data.SqlClient
Imports System.Math
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Public Class GL_Journal_Det : Inherits Page

    Protected WithEvents dgTx As DataGrid
    Protected WithEvents lblTxID As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblBlkTag As Label
    Protected WithEvents lblVehTag As Label
    Protected WithEvents lblVehExpTag As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblReprint As Label
    Protected WithEvents ddlReceiveFrom As DropDownList

    Protected WithEvents lstVehExp As DropDownList
    Protected WithEvents lstVehCode As DropDownList 
    Protected WithEvents lstBlock As DropDownList
    Protected WithEvents lstTxType As DropDownList
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblTotAmtFig As Label
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtRefNo As TextBox
    Protected WithEvents txtAmt As TextBox
    Protected WithEvents txtDescLn As TextBox
    Protected WithEvents txtQty As TextBox
    Protected WithEvents txtQtyCR As TextBox
    Protected WithEvents txtPrice As TextBox
    Protected WithEvents txtPriceCR As TextBox
    Protected WithEvents txtDRTotalAmount As TextBox
    Protected WithEvents txtCRTotalAmount As TextBox
    Protected WithEvents TAmt As Label
    Protected WithEvents tblAdd As HtmlTable
    Protected WithEvents lblError As Label
    Protected WithEvents lblStock As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents lblBlockErr As Label
    Protected WithEvents lblVehCodeErr As Label
    Protected WithEvents lblVehExpCodeErr As Label
    Protected WithEvents lblDescErr As Label
    Protected WithEvents lblStsHid As Label
    Protected WithEvents lblCtrlAmtFig As Label
    Protected WithEvents lblTwoAmount As Label
    Protected WithEvents LblIsSKBActive As Label
    Protected WithEvents lblSKBStartDate As Label
    Protected WithEvents Add As ImageButton
    Protected WithEvents Update As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Print As ImageButton
    Protected WithEvents Delete As ImageButton
    Protected WithEvents Back As ImageButton
    Protected WithEvents NewBtn As ImageButton
    Protected WithEvents UnDelete As ImageButton
    Protected WithEvents Confirm As ImageButton
    Protected WithEvents Edited As ImageButton

    Protected WithEvents btnSelDate As Image
    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents blnShortCut As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblTxLnID As Label
    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlk As HtmlTableRow
    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents lblPreBlockErr As Label
    Protected WithEvents lblPreBlkTag As Label
    Protected WithEvents ddlPreBlock As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Dim PreBlockTag As String
    Dim BlockTag As String
    Protected WithEvents RowChargeTo As HtmlTableRow
    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents lblLocationErr As Label

    Protected WithEvents txtSupCode As TextBox
    Protected WithEvents txtSupName As TextBox
    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtAccName As TextBox
    Protected WithEvents btnGet As ImageButton


    Protected WithEvents hidCtrlAmtFig As HtmlInputHidden
    Protected WithEvents hidDBCR As HtmlInputHidden
    Protected WithEvents hidDBAmt As HtmlInputHidden
    Protected WithEvents hidCRAmt As HtmlInputHidden
    Protected WithEvents hidTtlDBAmt As HtmlInputHidden
    Protected WithEvents hidTtlCRAmt As HtmlInputHidden

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbViewJournal As LinkButton
    Protected WithEvents dgViewJournal As DataGrid
    Protected WithEvents lblTotalDB As Label
    Protected WithEvents lblTotalCR As Label
    Protected WithEvents lblTotalViewJournal As Label

    Protected WithEvents RowTax As HtmlTableRow
    Protected WithEvents RowTaxAmt As HtmlTableRow
    Protected WithEvents lstTaxObject As DropDownList
    Protected WithEvents lblTaxObjectErr As Label
    Protected WithEvents txtDPPAmountDR As TextBox
    Protected WithEvents txtDPPAmountCR As TextBox
    Protected WithEvents hidNPWPNo As HtmlInputHidden
    Protected WithEvents hidTaxObjectRate As HtmlInputHidden
    Protected WithEvents lblTwoAmountDPP As Label
    Protected WithEvents hidCOATax As HtmlInputHidden
    Protected WithEvents hidTaxStatus As HtmlInputHidden
    Protected WithEvents hidHadCOATax As HtmlInputHidden
    Protected WithEvents lblTaxStatus As Label
    Protected WithEvents lblTaxStatusDesc As Label
    Protected WithEvents hidFFBSpl As HtmlInputHidden
    Protected WithEvents lblSupplier As Label
    Protected WithEvents lblErrSupplier As Label
    'Protected WithEvents ddlSupplier As DropDownList
    Protected WithEvents btnFind As HtmlInputButton
    Protected WithEvents lblFromLocCode As Label
    Protected WithEvents ddlFromLocCode As DropDownList
    Protected WithEvents lblErrFromLocCode As Label
    Protected WithEvents RangetxtAmtDR As RangeValidator
    Protected WithEvents RangetxtAmtCR As RangeValidator

    Protected WithEvents lblTaxObject As Label
    Protected WithEvents RowPO As HtmlTableRow
    Protected WithEvents lstPOID As DropDownList
    Protected WithEvents lblPOIDErr As Label

    Protected WithEvents lblRefNo As Label
    Protected WithEvents ddlRefNo As DropDownList

    Protected WithEvents RowSPL As HtmlTableRow
    Protected WithEvents RowFP As HtmlTableRow
    Protected WithEvents RowFPDate As HtmlTableRow
    Protected WithEvents btnFindDet As HtmlInputButton
    Protected WithEvents txtSupCodeDet As TextBox
    Protected WithEvents txtSupNameDet As TextBox
    Protected WithEvents lblErrSupplierDet As Label
    Protected WithEvents txtFakturPjkNo As TextBox
    Protected WithEvents hidTaxPPN As HtmlInputHidden
    Protected WithEvents txtFakturDate As TextBox
    
    Protected objGLtrx As New agri.GL.ClsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objPU As New agri.PU.clsTrx()

    Dim strOpCdStckTxDet_ADD As String = "GL_CLSTRX_JOURNAL_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "GL_CLSTRX_JOURNAL_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "GL_CLSTRX_JOURNAL_LINE_GET"
    Dim strOpCdStckTxLine_UPD As String = "GL_CLSTRX_JOURNAL_LINE_UPD"
    Dim strOpCdAccCode_GET As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"

    Dim objDataSet As New DataSet()
    Dim dsGrid As New DataSet()
    Dim objLangCapDs As New Object()
    Dim objSuppDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strDateFMT As String
    Dim intConfigsetting As Integer

    Dim strAccountTag As String
    Dim strVehTag As String
    Dim strBlockTag As String
    Dim strVehExpTag As String

    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim dblTotDocAmt As Double

    Protected WithEvents lblBPErr As Label
    Protected WithEvents lblLocCodeErr As Label
	
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strLocType As String
    Dim intLevel As Integer
    Dim strLocLevel As String

    Dim TaxLnID As String = ""
    Dim TaxRate As Double = 0
    Dim DPPAmount As Double = 0
    Dim SPLCode As String = ""
    Dim FakturNo As String = ""

    Dim strSelectedPOId As String = ""
    Dim strTxType As String
    Dim strRcvFrom As String

    Dim strParamName As String
    Dim strParamValue As String
    Dim objPODs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strDateFMT = Session("SS_DATEFMT")
        intGLAR = Session("SS_GLAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")
        strLocLevel = Session("SS_LOCLEVEL")

        txtCRTotalAmount.Attributes.Add("onfocus", "gotFocusCR()")
        txtDRTotalAmount.Attributes.Add("onfocus", "gotFocusDR()")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            ddlLocation.Enabled = False
            onload_GetLangCap()
            lblTwoAmount.Visible = False
            lblError.Visible = False
            lblDate.Visible = False
            lblFmt.Visible = False
            lblLocationErr.Visible = False
            lblAccCodeErr.Visible = False
            lblPreBlockErr.Visible = False
            lblBlockErr.Visible = False
            lblVehCodeErr.Visible = False
            lblVehExpCodeErr.Visible = False
            lblDescErr.Visible = False
            lblBPErr.Visible = False
            lblLocCodeErr.Visible = False
            TrLink.Visible = False
            lblErrSupplier.Visible = False
            lblErrFromLocCode.Visible = False
            lblTaxObjectErr.Visible = False
            lblPOIDErr.Visible = False
            lblErrSupplierDet.Visible = False

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            Add.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Add).ToString())
            NewBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewBtn).ToString())
            Save.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Save).ToString())
            Delete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Delete).ToString())
            Print.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Print).ToString())
            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())
            UnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(UnDelete).ToString())
            Confirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Confirm).ToString())
            Edited.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Edited).ToString())

            txtSupCode.Attributes.Add("readonly", "readonly")
            txtSupName.Attributes.Add("readonly", "readonly")
            txtAccName.Attributes.Add("readonly", "readonly")

            If Not Page.IsPostBack Then
                LblIsSKBActive.Text = 0
                lblSKBStartDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)

                BindLocationDropDownList(Trim(Session("SS_LOCATION")))
                RowChargeTo.Visible = Session("SS_INTER_ESTATE_CHARGING")
                BindChargeLevelDropDownList()
                Trace.Warn("postback")
                txtDate.Text = "" 'objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                lblTxID.Text = Request.QueryString("Id")
                If Not Request.QueryString("Id") = "" Then
                    LoadStockTxDetails()
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        DisplayFromDB()
                        BindGrid()
                    End If
                End If
                'BindAccCodeDropList()
                BindPreBlock("", "")
                BindBlockDropList("")
                BindVehicleCodeDropList("")
                BindVehicleExpDropList(True)
                BindTxTypeList()
                BindTaxObjectList("", "")
                PageControl()
            End If
        End If
    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.selectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub
    
    Sub ddlChargeLevel_OnSelectedIndexChanged(sender As Object, e As EventArgs)
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

    Sub ddlLocation_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strAccCode As String = txtAccCode.Text.Trim
        Dim strVehCode As String = lstVehCode.SelectedItem.Value.Trim()
        Dim strPreBlkCode As String = ddlPreBlock.SelectedItem.Value.Trim()
        Dim strBlkCode As String = lstBlock.SelectedItem.Value.Trim()
        BindVehicleCodeDropList(strAccCode, strVehCode)
        BindBlockDropList(strAccCode, strBlkCode)
        BindPreBlock(strAccCode, strPreBlkCode)
        hidChargeLocCode.value = ddlLocation.SelectedItem.Value.Trim()
    End Sub

    Sub BindLocationDropDownList(ByVal pv_strLocCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim dsLoc As DataSet
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        strOpCd = "ADMIN_CLSLOC_INTER_ESTATE_LOCATION_GET"
        intSelectedIndex = 0
        Try
            strParam = objAdminLoc.EnumLocStatus.Active & "|" & _
                       Trim(Session("SS_COMPANY")) & "|" & _
                       Trim(Session("SS_LOCATION")) & "|" & _
                       Trim(Session("SS_USERID")) & "|" & _
                       objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.GeneralLedger) & "|" & _
                       "GLAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal) & "|" & _
                       "GLAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal)
            intErrNo = objAdminLoc.mtdGetInterEstateLoc(strOpCd, _
                                                        strParam, _
                                                        dsLoc)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsLoc.Tables(0).Rows.Count - 1
            dsLoc.Tables(0).Rows(intCnt).Item("LocCode") = Trim(dsLoc.Tables(0).Rows(intCnt).Item("LocCode"))
            dsLoc.Tables(0).Rows(intCnt).Item("Description") = Trim(dsLoc.Tables(0).Rows(intCnt).Item("Description"))
            If dsLoc.Tables(0).Rows(intCnt).Item("LocCode") = Trim(pv_strLocCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next
        
        If Trim(pv_strLocCode) <> "" And intSelectedIndex = 0 Then
            dr = dsLoc.Tables(0).NewRow()
            dr("LocCode") = Trim(pv_strLocCode)
            dr("Description") = Trim(pv_strLocCode) & " (Deleted)"
            dsLoc.Tables(0).Rows.InsertAt(dr, 0)
            intSelectedIndex = 1
        End If

        dr = dsLoc.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelect.Text & GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text

        dsLoc.Tables(0).Rows.InsertAt(dr, 0)
        ddlLocation.DataSource = dsLoc.Tables(0)
        ddlLocation.DataValueField = "LocCode"
        ddlLocation.DataTextField = "Description"
        ddlLocation.DataBind()
        ddlLocation.SelectedIndex = intSelectedIndex

        If Not dsLoc Is Nothing Then
            dsLoc = Nothing
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlockTag = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                strBlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNAL_DETAILS_GET_COSTLEVEL_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/journal_details.aspx")
        End Try


        strAccountTag = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        strVehExpTag = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

        lblAccCodeTag.Text = strAccountTag & " :* "
        lblBlkTag.Text = strBlockTag & " : "
        lblVehTag.Text = strVehTag & " : "
        lblVehExpTag.Text = strVehExpTag & " :"

        lblAccCodeErr.Text = "<BR>" & lblPleaseSelect.Text & strAccountTag
        lblVehCodeErr.Text = lblPleaseSelect.Text & strVehTag
        lblBlockErr.Text = lblPleaseSelect.Text & strBlockTag
        lblVehExpCodeErr.Text = lblPleaseSelect.Text & strVehExpTag

        dgTx.Columns(3).HeaderText = strAccountTag
        dgTx.Columns(5).HeaderText = strBlockTag
        dgTx.Columns(6).HeaderText = strVehTag & "<BR>" & strVehExpTag
        'dgTx.Columns(7).HeaderText = strVehExpTag

        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTag.Text = PreBlockTag & lblCode.Text & " : "
        lblPreBlockErr.Text = lblPleaseSelect.Text & PreBlockTag & lblCode.Text
        lblLocationErr.Text = lblPleaseSelect.Text & GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNAL_DETAIL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/journal_details.aspx")
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

    Sub PageControl()
        txtDesc.Enabled = False
        lstTxType.Enabled = False
        ddlReceiveFrom.Enabled = False
        txtRefNo.Enabled = False
        txtDate.Enabled = False
        btnSelDate.Visible = False
        txtAmt.Enabled = False

        txtDescLn.Enabled = False
        txtAccCode.Enabled = False
        lstBlock.Enabled = False
        lstVehCode.Enabled = False
        lstVehExp.Enabled = False
        txtDRTotalAmount.Enabled = False
        txtCRTotalAmount.Enabled = False

        Add.Enabled = False
        Save.Visible = False
        Print.Visible = False
        Delete.Visible = False
        tblAdd.Visible = False
        UnDelete.Visible = False
        Edited.Visible = False

        Select Case lblStsHid.Text
            Case CStr(objGLtrx.EnumJournalStatus.Closed)
                Print.Visible = True

            Case CStr(objGLtrx.EnumJournalStatus.Posted)
                Print.Visible = True
                If Left(Trim(lblTxID.Text), 3) = "SJU" Or Left(Trim(lblTxID.Text), 3) = "MMO" Then
                    Edited.Visible = True
                End If

            Case CStr(objGLtrx.EnumJournalStatus.Deleted)
                UnDelete.Visible = True

            Case Else
                txtDesc.Enabled = True
                ddlReceiveFrom.Enabled = True
                txtRefNo.Enabled = True
                txtDate.Enabled = True
                btnSelDate.Visible = True
                txtAmt.Enabled = False

                txtDescLn.Enabled = True
                txtAccCode.Enabled = True
                lstBlock.Enabled = True
                lstVehCode.Enabled = True
                lstVehExp.Enabled = True
                txtDRTotalAmount.Enabled = True
                txtCRTotalAmount.Enabled = True

                Add.Enabled = True
                Save.Visible = True
                If lblTxID.Text.Trim() <> "" Then
                    Delete.Visible = True
                    txtDate.Enabled = False
                    Print.Visible = True
                End If
                tblAdd.Visible = True

                If lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax Then
                    lstTxType.Enabled = False
                    ddlReceiveFrom.Enabled = False
                Else
                    lstTxType.Enabled = True
                    ddlReceiveFrom.Enabled = True
                End If
        End Select

        Select Case Trim(Replace(Replace(Replace(lblCtrlAmtFig.Text, ".", ""), ",", "."), "-", ""))
            Case "0.00", "0,00", "0", ""
                Back.Attributes("onclick") = ""
                NewBtn.Attributes("onclick") = ""
                Save.Attributes("onclick") = ""
            Case Else
                Back.Attributes("onclick") = "javascript:return ConfirmAction('exit while amount in current transaction is not balance. Difference = " & Trim(lblCtrlAmtFig.Text) & " ');"
                Save.Attributes("onclick") = "javascript:return ConfirmAction('save while amount in current transaction is not balance. Difference = " & Trim(lblCtrlAmtFig.Text) & " ');"
                NewBtn.Attributes("onclick") = "javascript:return ConfirmAction('create new transaction while amount in current transaction is not balance. Difference = " & Trim(lblCtrlAmtFig.Text) & " ');"
        End Select

    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)

        If dgTx.Enabled = True Then
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim lbl As Label
                    Dim lblAmt As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    Dim DeleteButton As LinkButton
                    Dim EditButton As LinkButton
                    Select Case Status.Text.Trim
                        Case objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Active)
                            DeleteButton = e.Item.FindControl("Delete")
                            DeleteButton.Visible = True
                            DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                            EditButton = e.Item.FindControl("Edit")
                            EditButton.Visible = True
                        Case objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Deleted), _
                              objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Posted), _
                              objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Closed)
                            DeleteButton = e.Item.FindControl("Delete")
                            DeleteButton.Visible = False
                            EditButton = e.Item.FindControl("Edit")
                            EditButton.Visible = False
                    End Select

                    lblAmt = e.Item.FindControl("lblAmount")
                    lbl = e.Item.FindControl("lblAccTx")

                    If Sign(CDbl(Replace(Replace(lblAmt.Text.Trim, ".", ""), ",", "."))) = 1 Then
                        lbl.Text = "DR"
                    ElseIf Sign(CDbl(Replace(Replace(lblAmt.Text.Trim, ".", ""), ",", "."))) = -1 Then
                        lbl.Text = "CR"
                    End If
                Case ListItemType.EditItem
                    Dim lbl As Label
                    Dim lblAmt As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    lblAmt = e.Item.FindControl("lblAmount")
                    lbl = e.Item.FindControl("lblAccTx")


                    If Sign(CDbl(Replace(Replace(lblAmt.Text.Trim, ".", ""), ",", "."))) = 1 Then
                        lbl.Text = "DR"
                    ElseIf Sign(CDbl(Replace(Replace(lblAmt.Text.Trim, ".", ""), ",", "."))) = -1 Then
                        lbl.Text = "CR"
                    End If
            End Select

        Else
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim lbl As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    Dim DeleteButton As LinkButton
                    DeleteButton = e.Item.FindControl("Delete")
                    DeleteButton.Enabled = False
            End Select
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If

    End Sub

    Sub DisplayFromDB()
        ddlReceiveFrom.SelectedIndex = CInt(objDataSet.Tables(0).Rows(0).Item("ReceiveFrom")) - 1
        lblAccPeriod.Text = objDataSet.Tables(0).Rows(0).Item("AccMonth") & "/" & objDataSet.Tables(0).Rows(0).Item("AccYear")
        Status.Text = objGLtrx.mtdGetJournalStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
        lblStsHid.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Status"))
        CreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("UserName"))
        lblCtrlAmtFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objDataSet.Tables(0).Rows(0).Item("GrandTotal"), CInt(Session("SS_ROUNDNO"))), CInt(Session("SS_ROUNDNO")))
        'objGlobal.GetIDDecimalSeparator(FormatNumber(Trim(objDataSet.Tables(0).Rows(0).Item("GrandTotal")), 2, True, False, False))
        lblPrintDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("PrintDate")))
        txtDesc.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Description"))
        txtRefNo.Text = Trim(objDataSet.Tables(0).Rows(0).Item("RefNo"))
        txtDate.Text = objGlobal.GetShortDate(strDateFMT, Trim(objDataSet.Tables(0).Rows(0).Item("DocDate")))
        txtAmt.Text = FormatNumber(Trim(objDataSet.Tables(0).Rows(0).Item("DocAmt")), 2, True, False, False)
        lstTxType.SelectedValue = Trim(objDataSet.Tables(0).Rows(0).Item("TransactType"))
        hidCtrlAmtFig.Value = FormatNumber(objDataSet.Tables(0).Rows(0).Item("GrandTotal"), 2, True, False, False)

        hidNPWPNo.Value = Trim(objDataSet.Tables(0).Rows(0).Item("NPWPNo"))
        hidFFBSpl.Value = Trim(objDataSet.Tables(0).Rows(0).Item("SuppCat"))

        strTxType = Trim(objDataSet.Tables(0).Rows(0).Item("TransactType"))
        strRcvFrom = Trim(objDataSet.Tables(0).Rows(0).Item("ReceiveFrom"))

        If Trim(objDataSet.Tables(0).Rows(0).Item("TransactType")) = objGLtrx.EnumJournalTransactType.Tax Then
            lblSupplier.Visible = False
            txtSupCode.Visible = False
            btnGet.Visible = False
            txtSupName.Visible = False
            btnFind.Visible = False
            lblFromLocCode.Visible = True
            ddlFromLocCode.Visible = True
            txtSupCode.Text = Trim(objDataSet.Tables(0).Rows(0).Item("SupplierCode"))
            txtSupName.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Name"))
            'BindSupplier(Trim(objDataSet.Tables(0).Rows(0).Item("SupplierCode")))
            BindFromLoc(Trim(objDataSet.Tables(0).Rows(0).Item("FromLocCode")))
            lstTxType.Enabled = False
            ddlReceiveFrom.Enabled = False
            RowSPL.Visible = False
            RowFP.Visible = False
            RowFPDate.Visible = False
            RowTaxAmt.Visible = False
        ElseIf Trim(objDataSet.Tables(0).Rows(0).Item("TransactType")) = objGLtrx.EnumJournalTransactType.Adjustment And Trim(objDataSet.Tables(0).Rows(0).Item("ReceiveFrom")) = "4" Then
            lblSupplier.Visible = True
            txtSupCode.Visible = True
            txtSupName.Visible = True
            btnGet.Visible = True
            btnFind.Visible = True
            RowPO.Visible = True
            txtSupCode.Text = Trim(objDataSet.Tables(0).Rows(0).Item("SupplierCode"))
            txtSupName.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Name"))
            'BindSupplier(Trim(objDataSet.Tables(0).Rows(0).Item("SupplierCode")))
            BindPOID(Trim(objDataSet.Tables(0).Rows(0).Item("SupplierCode")))
            lstTxType.Enabled = False
            ddlReceiveFrom.Enabled = False
        ElseIf Trim(objDataSet.Tables(0).Rows(0).Item("TransactType")) = objGLtrx.EnumJournalTransactType.Umum And Trim(objDataSet.Tables(0).Rows(0).Item("ReceiveFrom")) = "5" Then
            'pertanggung jawaban uang muka
            lblRefNo.Visible = True
            ddlRefNo.Visible = True
            BindRefNo(Trim(objDataSet.Tables(0).Rows(0).Item("StaffAdvRefNo")))
        Else
            lblSupplier.Visible = False
            txtSupCode.Visible = False
            txtSupName.Visible = False
            btnGet.Visible = False
            btnFind.Visible = False
            lblFromLocCode.Visible = False
            ddlFromLocCode.Visible = False
            lstTxType.Enabled = True
            ddlReceiveFrom.Enabled = True
        End If
    End Sub

    Sub Initialize()
        txtAccCode.Text = ""
        lstBlock.SelectedIndex = 0
        lstVehCode.SelectedIndex = 0
        lstVehExp.SelectedIndex = 0
        ddlChargeLevel.selectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim Total As Double

        dgTx.DataSource = LoadDataGrid()
        dgTx.DataBind()

        Total = 0
        hidTtlDBAmt.Value = 0
        hidTtlCRAmt.Value = 0
        For intCnt = 0 To dsGrid.Tables(0).Rows.Count - 1
            If Sign(dsGrid.Tables(0).Rows(intCnt).Item("Total")) = 1 Then
                Total += dsGrid.Tables(0).Rows(intCnt).Item("Total")
                hidTtlDBAmt.Value += dsGrid.Tables(0).Rows(intCnt).Item("Total")
            Else
                hidTtlCRAmt.Value += dsGrid.Tables(0).Rows(intCnt).Item("Total")
            End If
        Next intCnt

        dblTotDocAmt = Total
        txtAmt.Text = FormatNumber(dblTotDocAmt, 2, True, False, False)

        lblTotAmtFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Total, CInt(Session("SS_ROUNDNO"))), CInt(Session("SS_ROUNDNO")))
        'objGlobal.GetIDDecimalSeparator(FormatNumber(Total, 0, True, False, False))
        dgTx.Columns(2).Visible = Session("SS_INTER_ESTATE_CHARGING")
    End Sub

    Protected Function LoadDataGrid() As DataSet
        Dim strParam As String

        strParam = Trim(lblTxID.Text)

        Try
            intErrNo = objGLtrx.mtdGetGLTxDetails(strOpCdStckTxLine_GET, strParam, dsGrid)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_JOURNALLN&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
        End Try

        If dsGrid.Tables(0).Rows.Count > 0 Then
            txtSupCode.Enabled = False
            btnFind.Disabled = True
            'btnGet.Visible = True
            ddlFromLocCode.Enabled = False
            ddlReceiveFrom.Enabled = False
        Else
            txtSupCode.Enabled = True
            btnFind.Disabled = False
            btnGet.Visible = False
            ddlFromLocCode.Enabled = True
            ddlReceiveFrom.Enabled = True
        End If

        Return dsGrid
    End Function

    Sub LoadStockTxDetails()
        Dim strOpCdStckTxDet_GET As String = "GL_CLSTRX_JOURNAL_DETAIL_GET"
        Dim StockCode As String

        strParam = Trim(lblTxID.Text)
        Try
            intErrNo = objGLtrx.mtdGetGLTxDetails(strOpCdStckTxDet_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_USAGEDETAILS&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
        Else
            TrLink.Visible = False
        End If
    End Sub

    'Protected Function CheckDate() As String
    '    Dim strDateSetting As String
    '    Dim objSysCfgDs As DataSet
    '    Dim objDateFormat As String
    '    Dim strValidDate As String

    '    If Not txtDate.Text = "" Then
    '        If objGlobal.mtdValidInputDate(strDateFMT, txtDate.Text, objDateFormat, strValidDate) = True Then
    '            Return strValidDate
    '        Else
    '            lblFmt.Text = objDateFormat & "."
    '            lblDate.Visible = True
    '            lblFmt.Visible = True
    '        End If
    '    End If
    'End Function

    Sub CallCheckVehicleUse(ByVal sender As Object, ByVal e As System.EventArgs)
        CheckVehicleUse()

        If lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax Then
            CheckCOATax()
        End If
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer, _
                          ByRef pr_strNurseryInd As Integer, _
                          ByRef pr_blnFound As Boolean)

        Dim _objAccDs As New DataSet()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLset.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
            pr_blnFound = True
        Else
            pr_blnFound = False
            pr_strAccType = 0
            pr_strAccPurpose = 0
            pr_strNurseryInd = 0
        End If
    End Sub

    Sub CheckVehicleUse()
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strAcc As String = Request.Form("txtAccCode")
        Dim strBlk As String = Request.Form("lstBlock")
        Dim strPreBlk As String = Request.Form("ddlPreBlock")
        Dim strVeh As String = Request.Form("lstVehCode")
        Dim strVehExp As String = Request.Form("lstVehExp")

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd, blnFound)

        If intAccType = objGLset.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLset.EnumAccountPurpose.NonVehicle
                    BindPreBlock(strAcc, strPreBlk)
                    BindBlockDropList(strAcc, strBlk)
                    BindVehicleCodeDropList("")
                    BindVehicleExpDropList(True)
                Case objGLset.EnumAccountPurpose.VehicleDistribution
                    BindPreBlock("", "")
                    BindBlockDropList("")
                    BindVehicleCodeDropList(strAcc, strVeh)
                    BindVehicleExpDropList(False, strVehExp)
                Case Else
                    BindPreBlock(strAcc, strPreBlk)
                    BindBlockDropList(strAcc, strBlk)
                    BindVehicleCodeDropList("%", strVeh)
                    BindVehicleExpDropList(False, strVehExp)
            End Select
        ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet Or intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
            'BindPreBlock(strAcc, strPreBlk)
            'BindBlockDropList(strAcc, strBlk)

            BindPreBlockBalanceSheet(strAcc, strPreBlk)
            BindBlockBalanceSheetDropList(strAcc, strPreBlk)

            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        Else
            BindPreBlock("", "")
            BindBlockDropList("", "")
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        End If
    End Sub

    Function CheckRequiredField() As Boolean
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strAcc As String = Request.Form("txtAccCode")
        Dim strBlk As String
        Dim strVeh As String = Request.Form("lstVehCode")
        Dim strVehExp As String = Request.Form("lstVehExp")

        If ddlChargeLevel.SelectedIndex = 1 Then
            strBlk = Request.Form("lstBlock")
        Else
            strBlk = Request.Form("ddlPreBlock")
        End If

        If txtDescLn.Text = "" Then
            lblDescErr.Visible = True
            Return True
        End If

        If ddlLocation.SelectedIndex = 0 Then
            lblLocationErr.Visible = True
            Return True
        End If

        If strAcc = "" Then
            lblAccCodeErr.Visible = True
            Return True
        End If

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd, blnFound)

        If blnFound = True Then
            If intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.No Then
                Return False
            ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
                If strBlk = "" Then
                    If ddlChargeLevel.SelectedIndex = 1 Then
                        lblBlockErr.Visible = True
                    Else
                        lblPreBlockErr.Visible = True
                    End If
                    Return True
                Else
                    Return False
                End If
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.NonVehicle And _
                strBlk = "" Then

                If ddlChargeLevel.SelectedIndex = 1 Then
                    lblBlockErr.Visible = True
                Else
                    lblPreBlockErr.Visible = True
                End If
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution And _
                strVeh = "" Then
                lblVehCodeErr.Visible = True
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution And _
                strVehExp = "" Then
                lblVehExpCodeErr.Visible = True
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.Others And _
                strVeh <> "" And strVehExp = "" Then
                lblVehExpCodeErr.Visible = True
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.Others And _
                strBlk = "" Then
                If ddlChargeLevel.SelectedIndex = 1 Then
                    lblBlockErr.Visible = True
                Else
                    lblPreBlockErr.Visible = True
                End If
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.Others And _
                strVeh = "" And strVehExp <> "" Then
                lblVehCodeErr.Visible = True
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label
        Dim Delbutton As LinkButton
        Dim strDesc As String
        Dim strLocCode As String
        Dim strAccCode As String
        Dim strBlkCode As String
        Dim strVehCode As String
        Dim strVehExpCode As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strDBCR As String = ""
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
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

        blnUpdate.Text = True
        dgTx.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblID")
        lblTxLnID.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblDesc")
        txtDescLn.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblAccTx")
        hidDBCR.Value = lbl.Text.Trim
        strDBCR = lbl.Text.Trim
        If lbl.Text.Trim = "DR" Then
            lbl = E.Item.FindControl("lblAmt")
            txtDRTotalAmount.Text = Replace(Replace(Replace(lbl.Text.Trim, ".", ""), ",", "."), "-", "")
            hidDBAmt.Value = txtDRTotalAmount.Text
            hidCRAmt.Value = 0
        ElseIf lbl.Text = "CR" Then
            lbl = E.Item.FindControl("lblAmt")
            txtCRTotalAmount.Text = Replace(Replace(Replace(lbl.Text.Trim, ".", ""), ",", "."), "-", "")
            hidCRAmt.Value = txtCRTotalAmount.Text
            hidDBAmt.Value = 0
        End If

        lbl = E.Item.FindControl("lblLocCode")
        strLocCode = lbl.Text.Trim
        BindLocationDropDownList(strLocCode)
        lbl = E.Item.FindControl("lblAccCode")
        strAccCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBlkCode")
        strBlkCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVehCode")
        strVehCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVehExpCode")
        strVehExpCode = lbl.Text.Trim

        txtAccCode.Text = strAccCode
        'BindAccCodeDropList(strAccCode)

        GetAccountDetails(strAccCode, intAccType, intAccPurpose, intNurseryInd, blnFound)

        If intAccType = objGLset.EnumAccountType.ProfitAndLost Then

            Select Case intAccPurpose
                Case objGLset.EnumAccountPurpose.NonVehicle
                    BindBlockDropList(strAccCode, strBlkCode)
                    BindVehicleCodeDropList("", strVehCode)
                    BindVehicleExpDropList(True)
                Case objGLset.EnumAccountPurpose.VehicleDistribution
                    BindBlockDropList("")
                    BindVehicleCodeDropList(strAccCode, strVehCode)
                    BindVehicleExpDropList(False, strVehExpCode)
                Case Else
                    BindBlockDropList(strAccCode, strBlkCode)
                    BindVehicleCodeDropList("%", strVehCode)
                    BindVehicleExpDropList(False, strVehExpCode)
            End Select
        ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet Or intNurseryInd = objGLset.EnumNurseryAccount.Yes Then

            'BindBlockDropList(strAccCode, strBlkCode)
            BindBlockBalanceSheetDropList(strAccCode, strBlkCode)
            BindVehicleCodeDropList("", strVehCode)
            BindVehicleExpDropList(True)
        Else

            BindBlockDropList("", "")
            BindVehicleCodeDropList("", strVehCode)
            BindVehicleExpDropList(True)
        End If

        Delbutton = E.Item.FindControl("Delete")
        Delbutton.Visible = False

        If lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax Then
            CheckCOATax()
            If hidCOATax.Value = 1 Then
                hidCOATax.Value = 1
                lbl = E.Item.FindControl("lblTaxLnID")
                BindTaxObjectList(strAccCode, lbl.Text.Trim)
                lbl = E.Item.FindControl("lblDPPAmount")
                If CDbl(lbl.Text.Trim) < 0 Then
                    txtDPPAmountDR.Text = ""
                    txtDPPAmountCR.Text = FormatNumber(Abs(CDbl(lbl.Text.Trim)), 2, True, False, False)
                ElseIf CDbl(lbl.Text.Trim) > 0 Then
                    txtDPPAmountCR.Text = ""
                    txtDPPAmountDR.Text = FormatNumber(Abs(CDbl(lbl.Text.Trim)), 2, True, False, False)
                End If

                lbl = E.Item.FindControl("lblSPLCode")
                txtSupCodeDet.Text = Trim(lbl.Text)
                lbl = E.Item.FindControl("lblSPLName")
                txtSupNameDet.Text = Trim(lbl.Text)
                lbl = E.Item.FindControl("lblSPLFaktur")
                txtFakturPjkNo.Text = Trim(lbl.Text)
                lbl = E.Item.FindControl("lblSPLFakturDate")
                txtFakturDate.Text = objGlobal.GetShortDate(strDateFMT, Trim(lbl.Text))

                txtCRTotalAmount.Attributes.Remove("onfocus")
                txtDRTotalAmount.Attributes.Remove("onfocus")

                lbl = E.Item.FindControl("lblTaxRate")
                hidTaxObjectRate.Value = CDbl(lbl.Text.Trim)
                lstTaxObject_OnSelectedIndexChanged(Sender, E)

                If txtDPPAmountDR.Text <> "" Or txtDPPAmountCR.Text <> "" Then
                    txtCRTotalAmount.ReadOnly = True
                    txtDRTotalAmount.ReadOnly = True
                Else
                    txtCRTotalAmount.ReadOnly = False
                    txtDRTotalAmount.ReadOnly = False
                End If
            Else
                hidCOATax.Value = 0
                RowTax.Visible = False
                RowTaxAmt.Visible = False
                txtCRTotalAmount.ReadOnly = False
                txtDRTotalAmount.ReadOnly = False
            End If
        ElseIf lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Adjustment And ddlReceiveFrom.SelectedItem.Value = "4" Then
            lbl = E.Item.FindControl("lblTaxLnID")
            strSelectedPOId = lbl.Text.Trim
            BindPOID(txtSupCode.Text)
        End If

        BindGrid()


        If lblTxLnID.Text <> "" Then
            Add.Visible = False
            Update.Visible = True
        Else
            Add.Visible = True
            Update.Visible = False
        End If
        RowChargeLevel.Visible = False
        ddlChargeLevel.SelectedIndex = 1
        ToggleChargeLevel()
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        lblTxLnID.Text = ""
        txtDescLn.Text = ""
        'txtQty.Text = ""
        'txtQtyCR.Text = ""
        'txtPrice.Text = ""
        'txtPriceCR.Text = ""
        txtDRTotalAmount.Text = ""
        txtCRTotalAmount.Text = ""
        hidDBCR.Value = ""
        hidCRAmt.Value = 0
        hidDBAmt.Value = 0

        RowSPL.Visible = False
        RowTaxAmt.Visible = False
        RowTax.Visible = False
        RowFP.Visible = False
        RowFPDate.Visible = False

        Initialize()
        dgTx.EditItemIndex = -1
        BindGrid()

        Add.Visible = True
        Update.Visible = False
        BindLocationDropDownList(Trim(Session("SS_LOCATION")))
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
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

        If lblStsHid.Text = CStr(objGLtrx.EnumJournalStatus.Active) Then

            Dim strOpCdStckTxLine_DEL As String = "GL_CLSTRX_JOURNAL_LINE_DEL"
            Dim lbl As Label
            Dim id As String
            Dim ItemCode As String
            Dim strParam As String
            Dim StrTxParam As String
            Dim TxID As String
            Dim ErrorChk As Integer = objGLtrx.EnumGeneralLedgerTxErrorType.NoError

            lbl = E.Item.FindControl("lblID")
            id = lbl.Text

            strParam = id & "|" & Trim(lblTxID.Text)
            Try
                intErrNo = objGLtrx.mtdDelTransactLn(strOpCdStckTxLine_DEL, strParam)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_USAGELINE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If

            End Try

            StrTxParam = lblTxID.Text & "|||||||||||||||||||"

            Try
                intErrNo = objGLtrx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strUserId, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                            ErrorChk, _
                                                            TxID)

                If ErrorChk = objGLtrx.EnumGeneralLedgerTxErrorType.Overflow Then
                    lblError.Visible = True
                End If

                lblTxID.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWJOURNAL&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If
            End Try
            LoadStockTxDetails()
            DisplayFromDB()
            BindGrid()
            PageControl()
        End If
    End Sub

    Sub BindTxTypeList()
        Dim IntCnt As Integer

        lstTxType.Items.Add(New ListItem(objGLtrx.mtdGetJournalTransactType(objGLtrx.EnumJournalTransactType.Adjustment), objGLtrx.EnumJournalTransactType.Adjustment))
        lstTxType.Items.Add(New ListItem(objGLtrx.mtdGetJournalTransactType(objGLtrx.EnumJournalTransactType.CreditNote), objGLtrx.EnumJournalTransactType.CreditNote))
        lstTxType.Items.Add(New ListItem(objGLtrx.mtdGetJournalTransactType(objGLtrx.EnumJournalTransactType.DebitNote), objGLtrx.EnumJournalTransactType.DebitNote))
        lstTxType.Items.Add(New ListItem(objGLtrx.mtdGetJournalTransactType(objGLtrx.EnumJournalTransactType.Invoice), objGLtrx.EnumJournalTransactType.Invoice))
        lstTxType.Items.Add(New ListItem(objGLtrx.mtdGetJournalTransactType(objGLtrx.EnumJournalTransactType.Others), objGLtrx.EnumJournalTransactType.Others))
        lstTxType.Items.Add(New ListItem(objGLtrx.mtdGetJournalTransactType(objGLtrx.EnumJournalTransactType.Umum), objGLtrx.EnumJournalTransactType.Umum))
        lstTxType.Items.Add(New ListItem(objGLtrx.mtdGetJournalTransactType(objGLtrx.EnumJournalTransactType.Tax), objGLtrx.EnumJournalTransactType.Tax))
        If lblTxID.Text <> "" Then
            lstTxType.Items.Add(New ListItem(objGLtrx.mtdGetJournalTransactType(objGLtrx.EnumJournalTransactType.WorkshopDistribution), objGLtrx.EnumJournalTransactType.WorkshopDistribution))
            For IntCnt = 0 To lstTxType.Items.Count - 1
                If Trim(objDataSet.Tables(0).Rows(0).Item("Transacttype")) = lstTxType.Items(IntCnt).Value Then
                    lstTxType.SelectedIndex = IntCnt
                End If
            Next
        Else
            lstTxType.SelectedIndex = 5
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
            strParam = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLset.EnumBlockStatus.Active
            intErrNo = objGLset.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

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

    Sub BindBlockDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim strParam As String
        Dim dr As DataRow

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLset.EnumBlockStatus.Active
            Else
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLset.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLset.mtdGetAccountBlock(strOpCdBlockList_Get, _
                                                     strParam, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        dr("Description") = lblSelect.Text & strBlockTag
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)
        lstBlock.DataSource = dsForDropDown.Tables(0)
        lstBlock.DataValueField = "BlkCode"
        lstBlock.DataTextField = "Description"
        lstBlock.DataBind()
        lstBlock.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindPreBlockBalanceSheet(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0


        strOpCd = "GL_CLSSETUP_ACCOUNT_PREBLOCK_BALANCESHEET_GET"

        strParamName = "ACCCODE|LOCCODE|STATUS"
        strParamValue = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLset.EnumBlockStatus.Active
        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                            strParamName, _
                                            strParamValue, _
                                            objPODs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objPODs.Tables(0).Rows(intCnt).Item("Description") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objPODs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If objPODs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objPODs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & PreBlockTag & lblCode.Text

        objPODs.Tables(0).Rows.InsertAt(dr, 0)
        ddlPreBlock.DataSource = objPODs.Tables(0)
        ddlPreBlock.DataValueField = "BlkCode"
        ddlPreBlock.DataTextField = "Description"
        ddlPreBlock.DataBind()
        ddlPreBlock.SelectedIndex = intSelectedIndex

        If Not objPODs Is Nothing Then
            objPODs = Nothing
        End If
    End Sub

    Sub BindBlockBalanceSheetDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim dr As DataRow

        Try

            strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_BALANCE_SHEET_GET"

            strParamName = "ACCCODE|LOCCODE|STATUS"
            strParamValue = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLset.EnumBlockStatus.Active

            intErrNo = objGLtrx.mtdGetDataCommon(strOpCdBlockList_Get, _
                                            strParamName, _
                                            strParamValue, _
                                            objPODs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objPODs.Tables(0).Rows(intCnt).Item("Description") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objPODs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If objPODs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objPODs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & strBlockTag & lblCode.Text
        objPODs.Tables(0).Rows.InsertAt(dr, 0)

        lstBlock.DataSource = objPODs.Tables(0)
        lstBlock.DataValueField = "BlkCode"
        lstBlock.DataTextField = "Description"
        lstBlock.DataBind()
        lstBlock.SelectedIndex = intSelectedIndex

        If Not objPODs Is Nothing Then
            objPODs = Nothing
        End If
    End Sub

    'Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")

    '    Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
    '    Dim dr As DataRow
    '    'Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLset.EnumAccountCodeStatus.Active & "'"
    '    Dim intErrNo As Integer
    '    Dim intCnt As Integer
    '    Dim intSelectedIndex As Integer = 0
    '    Dim dsForDropDown As DataSet


    '    If LblIsSKBActive.Text = 1 And (Format(txtDate.Text, "dd/MM/yyyy") >= Format(lblSKBStartDate.Text, "dd/MM/yyyy")) Then
    '        strParam = "Order By ACC.AccCode|And ACC.Status = '" & objGLset.EnumAccountCodeStatus.Active & "' AND ACC.AccCode NOT IN (SELECT AccCode FROM dbo.TX_TAXOBJECTRATELN) "
    '    Else
    '        strParam = "Order By ACC.AccCode|And ACC.Status = '" & objGLset.EnumAccountCodeStatus.Active & "'"
    '    End If

    '    strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

    '    Try
    '        intErrNo = objGLset.mtdGetMasterList(strOpCd, _
    '                                               strParam, _
    '                                               objGLset.EnumGLMasterType.AccountCode, _
    '                                               dsForDropDown)
    '    Catch Exp As System.Exception
    '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
    '    End Try

    '    For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
    '        If dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
    '            intSelectedIndex = intCnt + 1
    '            Exit For
    '        End If
    '    Next

    '    dr = dsForDropDown.Tables(0).NewRow()
    '    dr("AccCode") = ""
    '    dr("_Description") = lblSelect.Text & strAccountTag
    '    dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

    '    lstAccCode.DataSource = dsForDropDown.Tables(0)
    '    lstAccCode.DataValueField = "AccCode"
    '    lstAccCode.DataTextField = "_Description"
    '    lstAccCode.DataBind()
    '    lstAccCode.SelectedIndex = intSelectedIndex

    '    If Not dsForDropDown Is Nothing Then
    '        dsForDropDown = Nothing
    '    End If
    'End Sub

    Sub BindVehicleCodeDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strVehCode As String = "")

        Dim dsForDropDown As DataSet
        Dim strOpCd As String
        Dim drinsert As DataRow
        Dim strParam As New StringBuilder()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        strParam.Append("|LocCode = '" & ddlLocation.SelectedItem.Value.Trim() & "' AND Status = '" & objGLset.EnumVehicleStatus.Active & "'")
        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"
            intErrNo = objGLset.mtdGetMasterList(strOpCd, _
                                                   strParam.ToString, _
                                                   objGLset.EnumGLMasterType.Vehicle, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
        End Try




        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("VehCode") = Trim(pv_strVehCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = lblSelect.Text & strVehTag
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstVehCode.DataSource = dsForDropDown.Tables(0)
        lstVehCode.DataValueField = "VehCode"
        lstVehCode.DataTextField = "Description"
        lstVehCode.DataBind()
        lstVehCode.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindVehicleExpDropList(ByVal pv_IsBlankList As Boolean, Optional ByVal pv_strVehExpCode As String = "")

        Dim dsForDropDown As DataSet
        Dim strOpCd As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim drinsert As DataRow
        Dim strParam As String = "Order By VehExpenseCode ASC|And Veh.Status = '" & objGLset.EnumVehicleExpenseStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            If pv_IsBlankList Then
                strParam += " And Veh.VehExpensecode = ''"
            End If
            intErrNo = objGLset.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLset.EnumGLMasterType.VehicleExpense, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=JR_TRX_ADTRX_VEHEXPENSE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(pv_strVehExpCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = lblSelect.Text & strVehExpTag
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstVehExp.DataSource = dsForDropDown.Tables(0)
        lstVehExp.DataValueField = "VehExpenseCode"
        lstVehExp.DataTextField = "Description"
        lstVehExp.DataBind()
        lstVehExp.SelectedIndex = intSelectedIndex


        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
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
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objCOADs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        If objCOADs.Tables(0).Rows.Count > 0 Then
            txtAccCode.Text = objCOADs.Tables(0).Rows(0).Item("AccCode")
            txtAccName.Text = objCOADs.Tables(0).Rows(0).Item("Description")
        Else
            txtAccCode.Text = ""
            txtAccName.Text = ""
        End If
    End Sub

    Sub GetSupplierBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        onSelect_Change(sender, e)
    End Sub

    Sub GetSupplierDetBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        onSelect_ChangeDet(sender, e)
    End Sub

    Sub onSelect_StrAccCode(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetCOADetail(txtAccCode.Text.Trim)
        CallCheckVehicleUse(sender, e)
    End Sub

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If CheckRequiredField() Then
            Exit Sub
        End If
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strOpCdStckTxLine_ADD As String = "GL_CLSTRX_JOURNAL_LINE_ADD"

        Dim strStatus As String
        Dim TxID As String
        Dim IssType As String
        Dim strAmount As String
        Dim StrTxParam As New StringBuilder()
        Dim strTxLnParam As New StringBuilder()
        Dim ErrorChk As Integer = objGLtrx.EnumGeneralLedgerTxErrorType.NoError
        Dim strAllowVehicle As String
        Dim dblQty As Double = 1
        Dim dblPrice As Double = 0
        Dim dblTotal As Double = 0
        Dim strNewIDFormat As String
        Dim strCRTotalAmount As String = IIf(Trim(Request.Form("txtCRTotalAmount")) = "", 0, Trim(Request.Form("txtCRTotalAmount")))
        Dim strDRTotalAmount As String = IIf(Trim(Request.Form("txtDRTotalAmount")) = "", 0, Trim(Request.Form("txtDRTotalAmount")))
        Dim strCRTotalAmountDPP As String = IIf(Trim(Request.Form("txtDPPAmountCR")) = "", 0, Trim(Request.Form("txtDPPAmountCR")))
        Dim strDRTotalAmountDPP As String = IIf(Trim(Request.Form("txtDPPAmountDR")) = "", 0, Trim(Request.Form("txtDPPAmountDR")))

        Dim strSPLCode As String = Trim(Request.Form("txtSupCodeDet"))
        Dim strFromLocCode As String = Trim(Request.Form("ddlFromLocCode"))
        Dim strFakturNo As String = Trim(Request.Form("txtFakturPjkNo"))
        Dim strFPDate As String = Date_Validation(Request.Form("txtFakturDate"), False)
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        
        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
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
        
        If lstTxType.SelectedItem.Value <> objGLtrx.EnumJournalTransactType.Tax Then
            If CheckLocBillParty() = False Then
                lblBPErr.Visible = True
                lblLocCodeErr.Visible = True
                Exit Sub
            End If
        End If

        If CDbl(strCRTotalAmount) <> 0 And CDbl(strDRTotalAmount) <> 0 Then
            lblTwoAmount.Visible = True
            Exit Sub
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        If Trim(lstTxType.SelectedItem.Value) = Trim(objGLtrx.EnumJournalTransactType.Tax) Then
            If hidCOATax.Value = 1 Then
                Dim dblTotalDPP As Double = 0

                TaxLnID = lstTaxObject.SelectedItem.Value
                TaxRate = hidTaxObjectRate.Value
                FakturNo = txtFakturPjkNo.Text

                If CDbl(strDRTotalAmountDPP) = 0 And CDbl(strCRTotalAmountDPP) = 0 Then
                    If TaxLnID = "" Then
                        lblTaxObjectErr.Visible = True
                        lblTaxObjectErr.Text = "Please select Tax Object"
                        Exit Sub
                    Else
                        lblTwoAmountDPP.Visible = True
                        Exit Sub
                    End If
                End If
                If CDbl(strDRTotalAmountDPP) <> 0 Or CDbl(strCRTotalAmountDPP) <> 0 Then
                    If TaxLnID = "" And hidTaxPPN.Value = 0 Then
                        lblTaxObjectErr.Visible = True
                        lblTaxObjectErr.Text = "Please select Tax Object"
                        Exit Sub
                    Else
                    End If
                End If

                If CDbl(strDRTotalAmountDPP) <> 0 Then
                    DPPAmount = CDbl(txtDPPAmountDR.Text)
                End If
                If CDbl(strCRTotalAmountDPP) <> 0 Then
                    DPPAmount = CDbl(txtDPPAmountCR.Text) * -1
                End If
            Else
                TaxLnID = ""
                TaxRate = 0
                DPPAmount = 0
            End If

            If lstTaxObject.SelectedItem.Value <> "" Or txtFakturPjkNo.Text <> "" Then
                If strSPLCode = "" Or txtSupCodeDet.Text = "" Then
                    lblErrSupplierDet.Visible = True
                    Exit Sub
                End If
                If ddlFromLocCode.SelectedItem.Value = "" Then
                    lblErrFromLocCode.Visible = True
                    Exit Sub
                End If
            End If
            'strNewIDFormat = "MMT" & "/" & strCompany & "/" & strLocation & "/" & Trim(lstTxType.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        ElseIf lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Adjustment And ddlReceiveFrom.SelectedItem.Value = "4" Then
            If lstPOID.SelectedItem.Value = "" Then
                lblPOIDErr.Visible = True
                lblPOIDErr.Text = "Please select Purchase Order ID"
                Exit Sub
            End If
            TaxLnID = lstPOID.SelectedItem.Value
            strSelectedPOId = lstPOID.SelectedItem.Value
            'strNewIDFormat = "MMH" & "/" & strCompany & "/" & strLocation & "/" & Trim(lstTxType.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        Else
            TaxLnID = ""
            TaxRate = 0
            DPPAmount = 0
            strSPLCode = ""

            'strNewIDFormat = "MMO" & "/" & strCompany & "/" & strLocation & "/" & Trim(lstTxType.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        End If

        strNewIDFormat = "SJU" & "/" & strCompany & "/" & strLocation & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & strAccYear & "/"


        'If txtDRTotalAmount.Text <> "" Then
        '    dblQty = 1
        '    dblPrice = CDbl(txtDRTotalAmount.Text)
        '    dblTotal = CDbl(txtDRTotalAmount.Text)
        'End If

        'If txtCRTotalAmount.Text <> "" Then
        '    dblQty = 1
        '    dblPrice = objGlobal.mtdGetReverseValue(txtCRTotalAmount.Text)
        '    dblTotal = CDbl(txtCRTotalAmount.Text)
        '    dblTotal = dblTotal - (dblTotal * 2)
        'End If

        If CDbl(strDRTotalAmount) <> 0 Then
            dblQty = 1
            dblPrice = CDbl(IIf(strDRTotalAmount = "", "0", strDRTotalAmount))
            dblTotal = CDbl(IIf(strDRTotalAmount = "", "0", strDRTotalAmount))
        End If

        If CDbl(strCRTotalAmount) <> 0 Then
            dblQty = 1
            dblPrice = objGlobal.mtdGetReverseValue(strCRTotalAmount)
            dblTotal = CDbl(IIf(strCRTotalAmount = "", "0", strCRTotalAmount))
            dblTotal = dblTotal - (dblTotal * 2)
        End If

        Dim strStaffAdvID As String
        Dim strStaffAdvDoc As String
        Dim arrParam As Array

        If ddlReceiveFrom.SelectedItem.Value <> "5" Then
            strStaffAdvID = ""
            strStaffAdvDoc = ""
        Else
            If ddlRefNo.SelectedItem.Value = "" Then
                strStaffAdvID = ""
                strStaffAdvDoc = ""
            Else
                arrParam = Split(Trim(ddlRefNo.SelectedItem.Value), "|")
                strStaffAdvID = Trim(arrParam(0))
                strStaffAdvDoc = Trim(arrParam(1))
            End If
        End If


        If lblTxID.Text = "" Then
            StrTxParam.Append(lblTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(txtDesc.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(ddlReceiveFrom.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(lstTxType.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(Replace(txtRefNo.Text, "'", "''"))
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|")
            StrTxParam.Append(IIf(txtAmt.Text = "", "0", txtAmt.Text))
            StrTxParam.Append("|")
            StrTxParam.Append(0)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|||")
            StrTxParam.Append(strNewIDFormat)
            StrTxParam.Append("|")
            StrTxParam.Append(strSPLCode)
            StrTxParam.Append("|")
            StrTxParam.Append(IIf(lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax, strFromLocCode, strLocation))
            StrTxParam.Append("|")
            StrTxParam.Append(strStaffAdvID)
            StrTxParam.Append("|")
            StrTxParam.Append(strStaffAdvDoc)

            Try
                intErrNo = objGLtrx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                    strOpCdStckTxDet_UPD, _
                                                    strOpCdStckTxLine_GET, _
                                                    strUserId, _
                                                    StrTxParam.ToString, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                    ErrorChk, _
                                                    TxID)
                lblTxID.Text = TxID
                If ErrorChk = objGLtrx.EnumGeneralLedgerTxErrorType.Overflow Then
                    lblError.Visible = True
                End If

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWJOURNAL&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If
            End Try
        End If

        txtDescLn.Text = Replace(txtDescLn.Text, "|", " ")
        If lblTxLnID.Text = "" Then
            strTxLnParam.Append(lblTxID.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(txtDescLn.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblQty)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblPrice)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblTotal)
            strTxLnParam.Append("|")
            strTxLnParam.Append(Request.Form("txtAccCode"))
            strTxLnParam.Append("|")

            If ddlChargeLevel.SelectedIndex = 0 Then
                strTxLnParam.Append(Request.Form("ddlPreBlock"))
            Else
                strTxLnParam.Append(Request.Form("lstBlock"))
            End If
            strTxLnParam.Append("||")
            strTxLnParam.Append(Request.Form("lstVehCode"))
            strTxLnParam.Append("|")
            strTxLnParam.Append(Request.Form("lstVehExp"))
            strTxLnParam.Append("|")
            strTxLnParam.Append(Trim(ddlLocation.SelectedItem.Value))
            strTxLnParam.Append("|")
            strTxLnParam.Append(TaxLnID)
            strTxLnParam.Append("|")
            strTxLnParam.Append(TaxRate)
            strTxLnParam.Append("|")
            strTxLnParam.Append(DPPAmount)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strSPLCode)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strFakturNo)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strFPDate)

            Try
                If ddlChargeLevel.SelectedIndex = 0 And RowPreBlk.Visible = True Then
                    strParamList = Session("SS_LOCATION") & "|" & _
                                       txtAccCode.Text.Trim & "|" & _
                                       ddlPreBlock.SelectedItem.Value.Trim & "|" & _
                                       objGLset.EnumBlockStatus.Active & "|" & _
                                       strAccMonth & "|" & strAccYear


                    intErrNo = objGLtrx.mtdAddJournalLnByBlock(strOpCodeGLSubBlkByBlk, _
                                                              strParamList, _
                                                              strOpCdStckTxLine_ADD, _
                                                              objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.JournalLn), _
                                                              strTxLnParam.ToString, _
                                                              strLocType, _
                                                              ErrorChk)

                Else
                    intErrNo = objGLtrx.mtdAddJournalLn(strOpCdStckTxLine_ADD, _
                                                       objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.JournalLn), _
                                                       strTxLnParam.ToString, _
                                                       ErrorChk)
                End If
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDUSAGELINE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If
            End Try
        Else
            strTxLnParam.Append(lblTxLnID.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(txtDescLn.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblQty)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblPrice)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblTotal)
            strTxLnParam.Append("|")
            strTxLnParam.Append(Request.Form("txtAccCode"))
            strTxLnParam.Append("|")
            strTxLnParam.Append(Request.Form("lstBlock"))
            strTxLnParam.Append("||")
            strTxLnParam.Append(Request.Form("lstVehCode"))
            strTxLnParam.Append("|")
            strTxLnParam.Append(Request.Form("lstVehExp"))
            strTxLnParam.Append("|")
            strTxLnParam.Append(Trim(ddlLocation.SelectedItem.Value))
            strTxLnParam.Append("|")
            strTxLnParam.Append(lblTxID.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(TaxLnID)
            strTxLnParam.Append("|")
            strTxLnParam.Append(TaxRate)
            strTxLnParam.Append("|")
            strTxLnParam.Append(DPPAmount)

            Try
                intErrNo = objGLtrx.mtdUpdJournalLineDetail(strOpCdStckTxLine_UPD, _
                                                           strTxLnParam.ToString, _
                                                           ErrorChk)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPD_JRNLINE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
            End Try
            'lblTxLnID.Text = ""
            dgTx.EditItemIndex = -1

        End If

        Select Case ErrorChk
            Case objGLtrx.EnumGeneralLedgerTxErrorType.Overflow
                lblError.Visible = True
        End Select

        StrTxParam.Remove(0, StrTxParam.Length)
        StrTxParam.Append(lblTxID.Text)
        StrTxParam.Append("||||||||||||||")
        StrTxParam.Append(strSPLCode)
        StrTxParam.Append("|")
        StrTxParam.Append(IIf(lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax, strFromLocCode, strLocation))
        StrTxParam.Append("|")
        StrTxParam.Append(strStaffAdvID)
        StrTxParam.Append("|")
        StrTxParam.Append(strStaffAdvDoc)

        Try
            intErrNo = objGLtrx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                    strOpCdStckTxDet_UPD, _
                                                    strOpCdStckTxLine_GET, _
                                                    strUserId, _
                                                    StrTxParam.ToString, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                    ErrorChk, _
                                                    TxID)
            If ErrorChk = objGLtrx.EnumGeneralLedgerTxErrorType.Overflow Then
                lblError.Visible = True
            End If

            lblTxID.Text = TxID
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
            End If
        End Try

        If lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax And hidCOATax.Value = 1 Then
            Dim strOpCd As String
            Dim intErrNo As Integer
            Dim strParamName As String
            Dim strParamValue As String

            If txtFakturPjkNo.Text = "" Then
                'insert into tx_taxverified_status --pph
                strOpCd = "TX_CLSTRX_TAXVERIFICATION_DETAIL_ADD"

                strParamName = "LOCCODE|DOCID|TRXID|TAXSTATUS|FROMLOCCODE|ADDITIONALNOTE|UPDATEID"
                strParamValue = strLocation & "|" & _
                                Trim(lblTxID.Text) & "|" & _
                                "" & "|" & _
                                "2" & "|" & _
                                Trim(ddlFromLocCode.SelectedItem.Value) & "|" & _
                                "" & "|" & _
                                strUserId

                Try
                    intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                            strParamName, _
                                                            strParamValue)

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/TX_Setup_TaxObjectList.aspx")
                End Try
            End If

            '--ppn/pph
            If Trim(lblTxLnID.Text) <> "" Then
                strOpCd = "GL_CLSTRX_JOURNAL_TAX_UPDATE"
                strParamName = "LOCCODE|UPDATEID|JOURNALID|JRNLINEID|ACCMONTH|ACCYEAR|SUPPLIERCODE|TAXID|TAXLNID|DPPAMOUNT|TAXAMOUNT|KPPINIT|STATUS|ACCCODE|TAXDATE"
                strParamValue = strLocation & "|" & strUserId & "|" & Trim(lblTxID.Text) & "|" & Trim(lblTxLnID.Text) & "|" & strAccMonth & "|" & strAccYear & "|" & _
                                Trim(txtSupCodeDet.Text) & "|" & _
                                Trim(txtFakturPjkNo.Text) & "|" & _
                                TaxLnID & "|" & _
                                DPPAmount & "|" & dblTotal & "|" & _
                                ddlFromLocCode.SelectedItem.Value & "|" & "1" & "|" & _
                                Request.Form("txtAccCode") & "|" & _
                                strFPDate

                Try
                    intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                            strParamName, _
                                                            strParamValue)

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_TAX&errmesg=" & Exp.ToString() & "&redirect=pu/trx/AP_trx_InvoiceRcvList")
                End Try
            End If

            lstTaxObject.SelectedIndex = 0
            txtFakturPjkNo.Text = ""
            txtSupCodeDet.Text = ""
            txtSupNameDet.Text = ""
        End If

        lblTxLnID.Text = ""
        Initialize()
        LoadStockTxDetails()
        DisplayFromDB()
        BindGrid()
        PageControl()
        txtDRTotalAmount.Text = 0
        txtCRTotalAmount.Text = 0
        txtDPPAmountDR.Text = 0
        txtDPPAmountCR.Text = 0
        blnShortCut.Text = False
        hidDBCR.Value = ""
        hidCRAmt.Value = 0
        hidDBAmt.Value = 0
        RowTax.Visible = False
        RowFP.Visible = False
        RowFPDate.Visible = False

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If
        If Not strTxLnParam Is Nothing Then
            strTxLnParam = Nothing
        End If

        If lblTxLnID.Text <> "" Then
            Add.Visible = False
            Update.Visible = True
        Else
            Add.Visible = True
            Update.Visible = False
        End If

    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim TxID As String
        Dim IssType As String
        Dim strAllowVehicle As String
        Dim ErrorChk As Integer = objGLtrx.EnumGeneralLedgerTxErrorType.NoError
        Dim StrTxParam As New StringBuilder()
        Dim strNewIDFormat As String
        Dim strSPLCode As String = Trim(Request.Form("txtSupCode"))
        Dim strFromLocCode As String = Trim(Request.Form("ddlFromLocCode"))
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
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

        If lblTxID.Text = "" Then
            Exit Sub
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

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        strNewIDFormat = "SJU" & "/" & strCompany & "/" & strLocation & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & strAccYear & "/"

        'If lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax Then
        '    strNewIDFormat = "MMT" & "/" & strCompany & "/" & strLocation & "/" & Trim(lstTxType.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'Else
        '    strNewIDFormat = "MMO" & "/" & strCompany & "/" & strLocation & "/" & Trim(lstTxType.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End If

        Dim strStaffAdvID As String
        Dim strStaffAdvDoc As String
        
        If ddlReceiveFrom.SelectedItem.Value <> "5" Then
            strStaffAdvID = ""
            strStaffAdvDoc = ""
        Else
            If ddlRefNo.SelectedItem.Value = "" Then
                strStaffAdvID = ""
                strStaffAdvDoc = ""
            Else
                arrParam = Split(Trim(ddlRefNo.SelectedItem.Value), "|")
                strStaffAdvID = Trim(arrParam(0))
                strStaffAdvDoc = Trim(arrParam(1))
            End If
        End If


        If lblTxID.Text = "" Then
            StrTxParam.Append(lblTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(txtDesc.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(ddlReceiveFrom.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(lstTxType.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(Replace(txtRefNo.Text, "'", "''"))
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|")
            StrTxParam.Append(IIf(txtAmt.Text = "", "0", cdbl(txtAmt.Text)))
            StrTxParam.Append("|")
            StrTxParam.Append(0)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|||")
            StrTxParam.Append(strNewIDFormat)
            StrTxParam.Append("|")
            StrTxParam.Append(strSPLCode)
            StrTxParam.Append("|")
            StrTxParam.Append(IIf(lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax, strFromLocCode, strLocation))
            StrTxParam.Append("|")
            StrTxParam.Append(strStaffAdvID)
            StrTxParam.Append("|")
            StrTxParam.Append(strStaffAdvDoc)
        Else
            StrTxParam.Append(lblTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(txtDesc.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(ddlReceiveFrom.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(lstTxType.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(Replace(txtRefNo.Text, "'", "''"))
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|")
            StrTxParam.Append(cdbl(txtAmt.Text))
            StrTxParam.Append("||||")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("||||")
            StrTxParam.Append(strSPLCode)
            StrTxParam.Append("|")
            StrTxParam.Append(IIf(lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax, strFromLocCode, strLocation))
            StrTxParam.Append("|")
            StrTxParam.Append(strStaffAdvID)
            StrTxParam.Append("|")
            StrTxParam.Append(strStaffAdvDoc)
        End If

        Try

            intErrNo = objGLtrx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                        strOpCdStckTxDet_UPD, _
                                                        strOpCdStckTxLine_GET, _
                                                        strUserId, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                        ErrorChk, _
                                                        TxID)

            lblTxID.Text = TxID
            If ErrorChk = objGLtrx.EnumGeneralLedgerTxErrorType.Overflow Then
                lblError.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
            End If
        End Try
        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        LoadStockTxDetails()
        DisplayFromDB()
        PageControl()

    End Sub

    Sub btnPrint_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        'Dim StrTxParam As String
        'Dim TxID As String
        'Dim ErrorChk As Integer = objGLtrx.EnumGeneralLedgerTxErrorType.NoError
        'If lblPrintDate.Text = "" Then

        '    StrTxParam = lblTxID.Text & "|||||||||||" & Now() & "||"
        '    Try
        '        intErrNo = objGLtrx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
        '                                                    strOpCdStckTxDet_UPD, _
        '                                                    strOpCdStckTxLine_GET, _
        '                                                    strUserId, _
        '                                                    StrTxParam.ToString, _
        '                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
        '                                                    ErrorChk, _
        '                                                    TxID)
        '        lblTxID.Text = TxID
        '        If ErrorChk = objGLtrx.EnumGeneralLedgerTxErrorType.OverFlow Then
        '            lblError.Visible = True
        '        End If

        '    Catch Exp As System.Exception
        '        If intErrNo <> -5 Then
        '            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
        '        End If
        '    End Try
        'Else
        '    lblReprint.Visible = False
        'End If

        'LoadStockTxDetails()
        'DisplayFromDB()
        'PageControl()

        Dim strTRXID As String

        strTRXID = Trim(lblTxID.Text)

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_Journal_Details.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strLocation & _
                       "&TRXID=" & strTRXID & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")


    End Sub

    Sub btnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objGLtrx.EnumGeneralLedgerTxErrorType.NoError
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
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

        StrTxParam = lblTxID.Text & "||||||||||||" & objGLtrx.EnumJournalStatus.Deleted & "|||||"

        If intErrNo = 0 And ErrorChk = objGLtrx.EnumGeneralLedgerTxErrorType.NoError Then
            Try
                intErrNo = objGLtrx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strUserId, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                            ErrorChk, _
                                                            TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CANCELUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If
            End Try
        End If

        LoadStockTxDetails()
        DisplayFromDB()
        PageControl()
        BindGrid()

    End Sub

    Sub btnUnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objGLtrx.EnumGeneralLedgerTxErrorType.NoError
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
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

        StrTxParam = lblTxID.Text & "||||||||||||" & objGLtrx.EnumJournalStatus.Active & "|||||"

        If intErrNo = 0 And ErrorChk = objGLtrx.EnumGeneralLedgerTxErrorType.NoError Then
            Try
                intErrNo = objGLtrx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strUserId, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                            ErrorChk, _
                                                            TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CANCELUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If
            End Try
        End If

        LoadStockTxDetails()
        DisplayFromDB()
        PageControl()
        BindGrid()

    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../GL/Trx/GL_Trx_Journal_List.aspx")
    End Sub

    Function CheckLocBillParty() As Boolean
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strSearch As String
        Dim objBPLocDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strLocCode As String
        Dim objDS As New DataSet()

        strLocCode = Trim(ddlLocation.SelectedItem.Value)

        If Not (strLocCode = "" Or strLocCode = Trim(strLocation)) Then
            strSearch = " AND BP.Status = '" & objGLset.EnumBillPartyStatus.Active & "'" & _
                        " AND BP.InterLocCode = '" & strLocCode & "'"

            Try
                intErrNo = objGLset.mtdGetBPInterLoc(strOpCd_Get, strSearch, objBPLocDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_SETUP_BILLPARTY_GET&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_DailyAttd.aspx")
            End Try

            If objBPLocDs.Tables(0).Rows.Count <= 0 Then
                lblLocCodeErr.Text = strLocCode
                Return False
            End If
        End If

        Return True
    End Function

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
        Dim strAcceptFormat As String

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
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

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_trx_Journal_details.aspx")
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
                        Session("SS_USERID") & "|" & Trim(lblTxID.Text)

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

    End Sub

    Sub BindTaxObjectList(Optional ByVal pv_strAccCode As String = "", Optional ByVal pv_strTaxLnID As String = "")
        Dim strOpCd As String = "TX_CLSSETUP_TAXOBJECTRATELN_LIST_GET"
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet
        Dim intSPL As Char

        If hidNPWPNo.Value = "" Then
            intSPL = "2"
        Else
            intSPL = "1"
        End If
        strParamName = "STRSEARCH|INTSPL"
        strParamValue = "AccCode = '" & Trim(pv_strAccCode) & "' ORDER By TaxLnID ASC" & "|" & Trim(intSPL)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                dsForDropDown)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("TaxLnID")) = Trim(pv_strTaxLnID) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("TaxLnID") = ""
        dr("Descr") = lblSelect.Text & "Tax Object"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstTaxObject.DataSource = dsForDropDown.Tables(0)
        lstTaxObject.DataValueField = "TaxLnID"
        lstTaxObject.DataTextField = "Descr"
        lstTaxObject.DataBind()
        lstTaxObject.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub CheckCOATax()
        Dim strParamName As String
        Dim strParamValue As String
        Dim objTaxDs As New Object
        Dim strOpCd As String = "GL_CLSTRX_TAXOBJECTRATE_LIST_GET" '"TX_CLSSETUP_TAXOBJECTRATE_LIST_GET"

        strParamName = "STRSEARCH"
        strParamValue = " AND TOB.AccCode = '" & Trim(txtAccCode.Text) & "' "

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        If objTaxDs.Tables(0).Rows.Count > 0 Then
            RowSPL.Visible = True
            If hidFFBSpl.Value <> "2" Then
                If Trim(objTaxDs.Tables(0).Rows(0).Item("TaxID")) <> "" Then
                    lblTaxObject.Text = "Tax Object : "
                    RowTax.Visible = True
                    BindTaxObjectList(txtAccCode.Text, "")
                    RowFP.Visible = False
                    RowFPDate.Visible = False
                    hidTaxPPN.Value = 0
                Else
                    RowTax.Visible = False
                    RowFP.Visible = True
                    RowFPDate.Visible = True
                    hidTaxPPN.Value = 1
                End If
                RowTaxAmt.Visible = True
                hidCOATax.Value = 1
            Else
                RowTax.Visible = False
                RowTaxAmt.Visible = False
                RowFP.Visible = False
                RowFPDate.Visible = False
                hidCOATax.Value = 0
                hidTaxPPN.Value = 0
                txtCRTotalAmount.ReadOnly = False
                txtDRTotalAmount.ReadOnly = False
            End If
        Else
            RowTax.Visible = False
            RowTaxAmt.Visible = False
            RowSPL.Visible = False
            RowFP.Visible = False
            RowFPDate.Visible = False
            hidCOATax.Value = 0
            hidTaxPPN.Value = 0
            txtCRTotalAmount.ReadOnly = False
            txtDRTotalAmount.ReadOnly = False
        End If
        
        'RangetxtAmtDR.Visible = False
        'RangetxtAmtCR.Visible = False
    End Sub

    'Sub BindSupplier(ByVal pv_strSupplierId As String)
    '    Dim strOpCode_GetSupp As String = "PU_CLSSETUP_SUPPLIER_GET"
    '    Dim strParam As String
    '    Dim intErrNo As Integer
    '    Dim intCnt As Integer
    '    Dim intSelectedSuppIndex As Integer = 0

    '    If strTxType = objGLtrx.EnumJournalTransactType.Tax Then
    '        strParam = "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode|||"
    '    ElseIf strTxType = objGLtrx.EnumJournalTransactType.Adjustment And strRcvFrom = "4" Then
    '        strParam = "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||"
    '        strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') And SuppGrp='1' ")
    '    End If

    '    Try
    '        intErrNo = objPUSetup.mtdGetSupplier(strOpCode_GetSupp, strParam, objSuppDs)
    '    Catch Exp As Exception
    '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_TRX_PAYMENT_GET_SUPPCODE&errmesg=" & Exp.ToString() & "&redirect=CB/trx/cb_trx_paylist.aspx")
    '    End Try

    '    For intCnt = 0 To objSuppDs.Tables(0).Rows.Count - 1
    '        objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode").Trim()
    '        objSuppDs.Tables(0).Rows(intCnt).Item("Name") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") & " (" & objSuppDs.Tables(0).Rows(intCnt).Item("Name").Trim() & ")"
    '        If Trim(objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode")) = Trim(pv_strSupplierId) Then
    '            intSelectedSuppIndex = intCnt + 1
    '        End If
    '    Next intCnt

    '    Dim dr As DataRow
    '    dr = objSuppDs.Tables(0).NewRow()
    '    dr("SupplierCode") = ""
    '    dr("Name") = lblPleaseSelect.Text & " Supplier" & lblCode.Text
    '    objSuppDs.Tables(0).Rows.InsertAt(dr, 0)

    '    ddlSupplier.DataSource = objSuppDs.Tables(0)
    '    ddlSupplier.DataValueField = "SupplierCode"
    '    ddlSupplier.DataTextField = "Name"
    '    ddlSupplier.DataBind()
    '    ddlSupplier.SelectedIndex = intSelectedSuppIndex
    '    ddlSupplier.AutoPostBack = True
    'End Sub

    Sub onSelect_Change(ByVal Sender As System.Object, ByVal E As System.EventArgs)
        Dim strOpCode As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim dsMaster As Object

        strParamName = "STRSEARCH"
        If txtSupCode.Text = "" Then
            strParamValue = ""
        Else
            strParamValue = " And A.SupplierCode Like '" & Trim(txtSupCode.Text) & "%'"
        End If

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_TRX_PAYMENT_GET_SUPPCODE&errmesg=" & Exp.ToString() & "&redirect=CB/trx/cb_trx_paylist.aspx")
        End Try

        If dsMaster.Tables(0).Rows.Count > 0 Then
            hidNPWPNo.Value = Trim(dsMaster.Tables(0).Rows(0).Item("NPWPNo"))
            hidFFBSpl.Value = Trim(dsMaster.Tables(0).Rows(0).Item("SuppCat"))
            LblIsSKBActive.Text = Trim(dsMaster.Tables(0).Rows(0).Item("SKBIsActivation"))
            lblSKBStartDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), dsMaster.Tables(0).Rows(0).Item("SKBDate"))

            If lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Adjustment And ddlReceiveFrom.SelectedItem.Value = "4" Then
                BindPOID(txtSupCode.Text)
            ElseIf lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax Then
                BindFromLoc(Trim(dsMaster.Tables(0).Rows(0).Item("FromLocCode")))
            End If
            'BindAccCodeDropList("")
        End If
    End Sub

    Sub onSelect_ChangeDet(ByVal Sender As System.Object, ByVal E As System.EventArgs)
        Dim strOpCode As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim dsMaster As Object
        Dim strSPLCode As String = Trim(Request.Form("txtSupCodeDet"))

        strParamName = "STRSEARCH"
        If strSPLCode = "" Then
            strParamValue = ""
        Else
            strParamValue = " And A.SupplierCode Like '" & Trim(strSPLCode) & "%'"
        End If

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_TRX_PAYMENT_GET_SUPPCODE&errmesg=" & Exp.ToString() & "&redirect=CB/trx/cb_trx_paylist.aspx")
        End Try

        If dsMaster.Tables(0).Rows.Count > 0 Then
            hidNPWPNo.Value = Trim(dsMaster.Tables(0).Rows(0).Item("NPWPNo"))
            hidFFBSpl.Value = Trim(dsMaster.Tables(0).Rows(0).Item("SuppCat"))
            LblIsSKBActive.Text = Trim(dsMaster.Tables(0).Rows(0).Item("SKBIsActivation"))
            lblSKBStartDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), dsMaster.Tables(0).Rows(0).Item("SKBDate"))

            If lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax Then
                CheckCOATax()
            End If

            'If lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Adjustment And ddlReceiveFrom.SelectedItem.Value = "4" Then
            '    BindPOID(txtSupCode.Text)
            'ElseIf lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax Then
            '    BindFromLoc(Trim(dsMaster.Tables(0).Rows(0).Item("FromLocCode")))
            'End If
            'BindAccCodeDropList("")
        End If
    End Sub

    Sub TxType_Change(ByVal Sender As System.Object, ByVal E As System.EventArgs)
        strTxType = lstTxType.SelectedItem.Value
        If lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax Then
            lblSupplier.Visible = False
            txtSupCode.Visible = False
            txtSupName.Visible = False
            btnGet.Visible = False
            btnFind.Visible = False
            lblFromLocCode.Visible = True
            ddlFromLocCode.Visible = True
            'BindSupplier("")
            BindFromLoc("")
            RowSPL.Visible = False
            RowFP.Visible = False
            RowFPDate.Visible = False
            hidCOATax.Value = "1"
        Else
            RowTax.Visible = False
            RowTaxAmt.Visible = False
            hidCOATax.Value = 0
            lblSupplier.Visible = False
            txtSupCode.Visible = False
            txtSupName.Visible = False
            btnGet.Visible = False
            btnFind.Visible = False
            lblFromLocCode.Visible = False
            ddlFromLocCode.Visible = False
            RowSPL.Visible = False
            RowFP.Visible = False
            RowFPDate.Visible = False
            hidCOATax.Value = "0"
        End If
    End Sub

    Sub BindFromLoc(ByVal pv_strPRId As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        strPRRefLocCode = IIf(pv_strPRId = "", "", pv_strPRId)
        strParam = strPRRefLocCode & "|" & objAdminLoc.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objLocDs, "")

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            With objLocDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If .Item("LocCode") = strPRRefLocCode Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelect.Text & GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlFromLocCode.DataSource = objLocDs.Tables(0)
        ddlFromLocCode.DataValueField = "LocCode"
        ddlFromLocCode.DataTextField = "Description"
        ddlFromLocCode.DataBind()
        ddlFromLocCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub lstTaxObject_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim arrParam As Array
        Dim CRAmt As Double
        Dim DRAmt As Double
        arrParam = Split(lstTaxObject.SelectedItem.Text, ";")

        If lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Tax Then
            If lstTaxObject.SelectedItem.Value = "" Then
                txtCRTotalAmount.ReadOnly = False
                txtDRTotalAmount.ReadOnly = False
            Else
                hidTaxObjectRate.Value = CDbl(Replace(arrParam(1), "%", ""))
                If txtDPPAmountDR.Text <> "" Then
                    DRAmt = CDbl(IIf(txtDPPAmountDR.Text = "", 0, txtDPPAmountDR.Text)) * (hidTaxObjectRate.Value / 100)
                    DRAmt = Math.Floor(DRAmt) 'ROUNDDOWN
                    'DRAmt = Math.Floor(DRAmt + 0.5)
                    txtDRTotalAmount.Text = DRAmt
                Else
                    CRAmt = CDbl(IIf(txtDPPAmountCR.Text = "", 0, txtDPPAmountCR.Text)) * (hidTaxObjectRate.Value / 100)
                    CRAmt = Math.Floor(CRAmt) 'ROUNDDOWN
                    'CRAmt = Math.Floor(CRAmt + 0.5)
                    txtCRTotalAmount.Text = CRAmt
                End If

                txtCRTotalAmount.ReadOnly = True
                txtDRTotalAmount.ReadOnly = True
            End If
        End If
    End Sub

    Sub RcvFrom_Change(ByVal Sender As System.Object, ByVal E As System.EventArgs)
        strTxType = lstTxType.SelectedItem.Value
        strRcvFrom = ddlReceiveFrom.SelectedItem.Value

        If lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Adjustment And ddlReceiveFrom.SelectedItem.Value = "4" Then
            'adjust TB Hutang Dagang
            lblSupplier.Visible = True
            txtSupCode.Visible = True
            txtSupName.Visible = True
            btnGet.Visible = True
            btnFind.Visible = True
            RowPO.Visible = True
            'BindSupplier("")
            lblRefNo.Visible = False
            ddlRefNo.Visible = False
        ElseIf lstTxType.SelectedItem.Value = objGLtrx.EnumJournalTransactType.Umum And ddlReceiveFrom.SelectedItem.Value = "5" Then
            'pertanggung jawaban uang muka
            lblRefNo.Visible = True
            ddlRefNo.Visible = True
            BindRefNo("")
        Else
            btnGet.Visible = False
            lblSupplier.Visible = False
            txtSupCode.Visible = False
            txtSupName.Visible = False
            btnFind.Visible = False
            RowPO.Visible = False
            lblRefNo.Visible = False
            ddlRefNo.Visible = False
        End If
    End Sub

    Sub BindRefNo(ByVal pv_strRefNo As String)
        Dim strOpCode As String = "HR_CLSSETUP_STAFF_GET_TRX"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim dsMaster As Object
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        strParamName = "LOCCODE|STRSEARCH"
        strParamValue = strLocation & "|" & " AND A.Status='1' AND Outstanding > 0 "

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & Exp.Message.ToString & "&redirect=")
        End Try

        For intCnt = 0 To dsMaster.Tables(0).Rows.Count - 1
            dsMaster.Tables(0).Rows(intCnt).Item("RefNo") = dsMaster.Tables(0).Rows(intCnt).Item("RefNo").Trim()
            dsMaster.Tables(0).Rows(intCnt).Item("Name") = Trim(dsMaster.Tables(0).Rows(intCnt).Item("Name")) & ", " & Trim(dsMaster.Tables(0).Rows(intCnt).Item("StaffAdvDoc")) & ", Rp " & objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dsMaster.Tables(0).Rows(intCnt).Item("Outstanding"), CInt(Session("SS_ROUNDNO"))), CInt(Session("SS_ROUNDNO")))
            If Trim(pv_strRefNo) = Trim(dsMaster.Tables(0).Rows(intCnt).Item("RefNo")) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        Dim dr As DataRow
        dr = dsMaster.Tables(0).NewRow()
        dr("RefNo") = ""
        dr("Name") = lblPleaseSelect.Text & " reference/staff advance"
        dsMaster.Tables(0).Rows.InsertAt(dr, 0)

        ddlRefNo.DataSource = dsMaster.Tables(0)
        ddlRefNo.DataValueField = "RefNo"
        ddlRefNo.DataTextField = "Name"
        ddlRefNo.DataBind()
        ddlRefNo.SelectedIndex = intSelectedIndex
        ddlRefNo.AutoPostBack = True

    End Sub

    Sub BindPOID(ByVal pv_strSuppCode As String)
        Dim strOpCd_GetPO As String = "PU_CLSTRX_PO_ADJ_HUTANGDAGANG_GET"
        Dim strParam As String = "||||||||"
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim blnFound As Boolean = False
        Dim dr As DataRow
        Dim strPOID As String
        Dim objPODs As New Object()
        Dim strParamName As String
        Dim strParamValue As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strSrchPeriode As String

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If Len(Trim(strAccMonth)) = 1 Then
            strSrchPeriode = strAccYear & "0" & strAccMonth
        Else
            strSrchPeriode = strAccYear & strAccMonth
        End If


        If pv_strSuppCode <> "" Then
            strParamName = "STRSEARCH"
            strParamValue = "AND D.LocCode = '" & Trim(strLocation) & "' AND D.SupplierCode LIKE '" & IIf(pv_strSuppCode = "", "XXX", pv_strSuppCode) & "' " & _
                            "AND (cast(D.AccYear AS int)*100) + (cast(D.AccMonth AS int)) <= " & strSrchPeriode

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_GetPO, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objPODs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INVOICERCV_GET_PODETAIL&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvlist.aspx")
            End Try

            For intCnt = 0 To objPODs.Tables(0).Rows.Count - 1
                objPODs.Tables(0).Rows(intCnt).Item("POID") = Trim(objPODs.Tables(0).Rows(intCnt).Item("POID"))
                objPODs.Tables(0).Rows(intCnt).Item("DispPOId") = Trim(objPODs.Tables(0).Rows(intCnt).Item("POId")) 

                If objPODs.Tables(0).Rows(intCnt).Item("POId") = strSelectedPOId Then
                    intSelectedIndex = intCnt + 1
                End If
            Next intCnt
        End If

        dr = objPODs.Tables(0).NewRow()
        dr("POId") = ""
        dr("DispPOId") = lblPleaseSelect.Text & "Purchase Order ID"
        objPODs.Tables(0).Rows.InsertAt(dr, 0)

        lstPOID.DataSource = objPODs.Tables(0)
        lstPOID.DataValueField = "POId"
        lstPOID.DataTextField = "DispPOId"
        lstPOID.DataBind()
        lstPOID.SelectedIndex = intSelectedIndex
        If lstPOID.SelectedIndex <> -1 Then
            strSelectedPOId = lstPOID.SelectedItem.Value
        End If
    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

    End Sub

    Sub btnEdited_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
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

        strParamName = "UPDATESTR"
        strParamValue = " SET STATUS = '" & Trim(objGLtrx.EnumJournalStatus.Active) & "', UPDATEID = '" & Trim(strUserId) & "', UPDATEDATE = GETDATE() " & _
                        " WHERE JournalID = '" & Trim(lblTxID.Text) & "'"

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCdStckTxDet_UPD, _
                                                strParamName, _
                                                strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        LoadStockTxDetails()
        DisplayFromDB()
        PageControl()
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

End Class
