Imports System
Imports System.Data
Imports System.Math
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports microsoft.VisualBasic.DateAndTime


Public Class IN_StockReceive : Inherits Page

    Protected WithEvents dgStkTx As DataGrid
    Protected WithEvents lblStckTxID As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblTotAmtFig As Label
    Protected WithEvents lblReprint As Label
    Protected WithEvents lblAccTag As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblFertInd As Label

    Protected WithEvents lblStockReceiveID As Label
    Protected WithEvents txtRemarks As TextBox
    Protected WithEvents lblBlkTag As Label
    Protected WithEvents lblVehTag As Label
    Protected WithEvents lblVehExpTag As Label
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtQty As TextBox
    Protected WithEvents txtCost As TextBox
    Protected WithEvents txtRefNo As TextBox
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtAccName As TextBox
    Protected WithEvents chkCentralized As CheckBox

    Protected WithEvents lblPDateTag As Label
    Protected WithEvents tblAdd As HtmlTable
    Protected WithEvents tblPR As HtmlTable
    Protected WithEvents RowAcc As HtmlTableRow
    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents RowVeh As HtmlTableRow
    Protected WithEvents RowVehExp As HtmlTableRow
    Protected WithEvents RowFromLoc As HtmlTableRow
    Protected WithEvents RowAMt As HtmlTableRow
    Protected WithEvents RowCost As HtmlTableRow

    Protected WithEvents lblError As Label
    Protected WithEvents lblStock As Label
    Protected WithEvents lblUnDel As Label
    Protected WithEvents lblErrQty As Label
    Protected WithEvents lblErrUnitCost As Label
    Protected WithEvents lblErrTotalAmt As Label
    Protected WithEvents lblPR As Label
    Protected WithEvents lblTxError As Label
    Protected WithEvents lblStatusHid As Label
    Protected WithEvents lblConfirmErr As Label
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents lblBlockErr As Label
    Protected WithEvents lblVehCodeErr As Label
    Protected WithEvents lblVehExpCodeErr As Label
    Protected WithEvents lblItemCodeErr As Label
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents FindIN_Type As Button

    Protected WithEvents btnUpdate As ImageButton

    Protected WithEvents PRDelete As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Confirm As ImageButton
    Protected WithEvents Print As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents Back As ImageButton
    Protected WithEvents btnSelDate As Image
    Protected WithEvents lstAccCode As DropDownList
    
    Protected WithEvents TxtItemCode As TextBox
    Protected WithEvents TxtItemName As TextBox

    Protected WithEvents lstItem As DropDownList
    Protected WithEvents lstBlock As DropDownList
    Protected WithEvents lstFromLoc As DropDownList
    Protected WithEvents lstVehCode As DropDownList
    Protected WithEvents lstVehExp As DropDownList
    Protected WithEvents lstRecDoc As DropDownList
    Protected WithEvents lstPR As DropDownList
    Protected WithEvents lstStorage As DropDownList

    Protected WithEvents lblSRType As Label
    Protected WithEvents FindIN As HtmlInputButton
    Protected WithEvents FindIN_Txt As HtmlInputButton
    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblFromLocErr As Label
    Protected WithEvents lblstoragemsg As Label

    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlk As HtmlTableRow
    Protected WithEvents lblPreBlockErr As Label
    Protected WithEvents lblPreBlkTag As Label
    Protected WithEvents lstPreBlock As DropDownList
    Protected WithEvents lstChargeLevel As DropDownList
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Protected WithEvents lblPBBKB1 As HtmlInputHidden
    
    Dim PreBlockTag As String
    Protected WithEvents RowChargeTo As HtmlTableRow
    Protected WithEvents ddlChargeTo As DropDownList
    Protected WithEvents lblChargeToErr As Label
    Protected WithEvents lblPBBKB As Label
    Protected WithEvents lblErrRefNo As Label

    Protected WithEvents ddlInventoryBin As DropDownList
    Protected WithEvents lblInventoryBin As Label

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbViewJournal As LinkButton
    Protected WithEvents dgViewJournal As DataGrid
    Protected WithEvents lblTotalDB As Label
    Protected WithEvents lblTotalCR As Label
    Protected WithEvents lblTotalViewJournal As Label

    Protected WithEvents btnAddAllItem As ImageButton
    Protected WithEvents lblErrDoc As Label
    Protected WithEvents TxtNetTransPortFee As TextBox

    Protected objINtx As New agri.IN.clstrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objGLtrx As New agri.GL.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objOk As New agri.GL.ClsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()

    Protected objPU As New agri.PU.clsTrx()
    
    Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_STOCKRECEIVE_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_STOCKRECEIVE_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_STOCKRECEIVE_LINE_GET"
    Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
    Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"
    Dim strOpPRLNDet_Details_GET As String = "IN_CLSTRX_PURREQLN_DET_GET"
    Dim strOpPRLN_UPD As String = "IN_CLSTRX_PRLN_UPD"
    Dim strOpCdPR_Count_GET As String = "IN_CLSTRX_PURREQLN_FINDLIST_GET"
    Dim strOpCdPR_Details_UPD As String = "IN_CLSTRX_PURREQ_DET_UPD"
    Dim strOpCdDA_Details_UPD as String = "PU_CLSTRX_DA_LINE_UPD"
    Dim strOpDALNDet_Details_GET as string = "PU_CLSTRX_DA_LINE_GET"
    Dim strOpCdDA_Count_GET as string = ""
    Dim strOpDALN_UPD as string = ""

    Const ITEM_PART_SEPERATOR As String = " @ " 

    Dim objStkRcv As New DataSet()
    Dim objStkRcvLn As New DataSet()
    Dim objLangCapDs As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strDateFMT As String
    Dim intINAR As Integer
    Dim intConfigsetting As Integer
    Dim AccountTag As String
    Dim BlockTag As String
    Dim VehicleTag As String
    Dim VehExpCodeTag As String
    Dim strLocationTag As String
    Dim strPhyMonth As String
    Dim strPhyYear As String
    Dim strFromLoc As String
    Dim strBlkVal As String

    Protected WithEvents lblBillPartyErr As Label
    Protected WithEvents lblLocCodeErr As Label
	
    Dim strLocType As String

    Dim dblRate As Double
    Dim strParamUOM As String = ""
    Dim strUOMCode As String = ""
    Dim strPurchaseUOM As String = ""
    Protected objAdmin As New agri.Admin.clsUom()
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer
    Dim nCOAdefault As String = ""

#Region "TOOLS & COMPONENT"


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strDateFMT = Session("SS_DATEFMT")
        intINAR = Session("SS_INAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")
	    
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReceive), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If RowFromLoc.Visible = True And lstFromLoc.Items.Count > 0 Then
                strFromLoc = lstFromLoc.SelectedItem.Value
            End If
            lblFromLocErr.Visible = False
            lblInventoryBin.Visible = False
            lblErrDoc.Visible = False

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            Save.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Save).ToString())
            Confirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Confirm).ToString())
            Print.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Print).ToString())
            PRDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(PRDelete).ToString())
            btnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnNew).ToString())
            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())

            If Not Page.IsPostBack Then
                BindInventoryBinLevel("")
                BindChargeToDropDownList(strLocation)
                BindChargeLevelDropDownList()
                BindStorage("")
                lblStckTxID.Text = Request.QueryString("Id")

                TxtItemCode.Attributes.Add("readonly", "readonly")
                TxtItemName.Attributes.Add("readonly", "readonly")
                txtAccCode.Attributes.Add("readonly", "readonly")
                txtAccName.Attributes.Add("readonly", "readonly")
                RowAMt.Visible = False
                RowCost.Visible = False

                If lblStckTxID.Text <> "" Then
                    onLoad_Display()
                    onLoad_DisplayLine()
                    BindDocList()
                    DisablePage()
                End If

                If lblStckTxID.Text = "" Then
                    txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    TrLink.Visible = False
                End If

                BindDocList()


                BindBlockDropList("")
                BindVehicleCodeDropList("")
                BindVehicleExpDropList(True)
        End If
                BindLocDropList()            
                DisablePage()
                lblError.Visible = False
                lblStock.Visible = False
                lblUnDel.Visible = False
                lblErrQty.Visible = False
                lblErrTotalAmt.Visible = False
                lblErrUnitCost.Visible = False
                lblDate.Visible = False
                lblFmt.Visible = False
                lblPR.Visible = False
                lblTxError.Visible = False
                lblConfirmErr.Visible = False
                lblAccCodeErr.Visible = False
                lblBlockErr.Visible = False
                lblPreBlockErr.Visible = False
                lblVehCodeErr.Visible = False
                lblVehExpCodeErr.Visible = False
                lblItemCodeErr.Visible = False
                lblChargeToErr.Visible = False
                lblBillPartyErr.Visible = False
                lblLocCodeErr.Visible = False
                lblErrRefNo.Visible = False
            lblDate.Visible = False

        End If
    End Sub

    Sub ddlChargeTo_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strAccCode As String = Request.Form("txtAccCode")
        Dim strVehCode As String = Request.Form("lstVehCode")
        Dim strPreBlkCode As String = Request.Form("lstPreBlock")
        Dim strBlkCode As String = Request.Form("lstBlock")
        BindVehicleCodeDropList(strAccCode, strVehCode)
        BindBlockDropList(strAccCode, strBlkCode)
        hidChargeLocCode.value = ddlChargeTo.SelectedItem.Value.Trim()
        lblChargeToErr.Visible = (ddlChargeTo.SelectedIndex < 1)
        CheckVehicleUse()
    End Sub
    
    Sub BindChargeToDropDownList(ByVal pv_strLocCode As String)
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
                       objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Inventory) & "|" & _
                       "INAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReceive) & "|" & _
                       "INAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReceive)
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
        ddlChargeTo.DataSource = dsLoc.Tables(0)
        ddlChargeTo.DataValueField = "LocCode"
        ddlChargeTo.DataTextField = "Description"
        ddlChargeTo.DataBind()
        ddlChargeTo.SelectedIndex = intSelectedIndex

        If Not dsLoc Is Nothing Then
            dsLoc = Nothing
        End If
    End Sub

    Sub TextChanged()
        If Trim(lstRecDoc.SelectedItem.Value) = (objINtx.EnumStockReceiveDocType.DispatchAdvice) Then
            txtQty.Enabled = True
            txtCost.Enabled = False
            txtAmount.Enabled = False
            GetDataDA()
            TxtNetTransPortFee.Text = objGlobal.DisplayForEditCurrencyFormat(GetOngkosAngkut(lstPR.SelectedItem.Text, TxtItemCode.Text))
        ElseIf Trim(lstRecDoc.SelectedItem.Value) = (objINtx.EnumStockReceiveDocType.StockReturnAdvice) Then
            txtQty.Enabled = True
            txtCost.Enabled = False
            txtAmount.Enabled = False
            GetDataSRA()
        ElseIf Trim(lstRecDoc.SelectedItem.Value) = (objINtx.EnumStockReceiveDocType.StockTransfer) Then
            txtQty.Enabled = True
            txtCost.Enabled = True
            txtAmount.Enabled = False
            GetDataST()
        Else
            txtAmount.Enabled = False
            GetPRCost()
           
        End If
        GetItemDetails(TxtItemCode.Text, 0)

        CheckType(TxtItemCode.Text)
    End Sub

    Sub txtAccCode_TextChanged()
        GetCOADetail(txtAccCode.Text)
    End Sub

    Sub lstChargeLevel_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        CheckVehicleUse()
        ToggleChargeLevel()
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
                        Session("SS_USERID") & "|" & Trim(lblStckTxID.Text)

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

        onLoad_Display()
        DisablePage()
    End Sub

#Region "GRID EVENT"

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)

        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim DeleteButton As LinkButton

                Select Case Status.Text.Trim
                    Case objINtx.mtdGetStockReceiveStatus(objINtx.EnumStockReceiveStatus.Active)
                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = True
                        DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objINtx.mtdGetStockReceiveStatus(objINtx.EnumStockReceiveStatus.Confirmed), _
                         objINtx.mtdGetStockReceiveStatus(objINtx.EnumStockReceiveStatus.Deleted)
                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = False
                End Select
        End Select

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If

    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
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
            'If intSelPeriod <> intInputPeriod Then
            '    lblDate.Visible = True
            '    lblDate.Text = "Invalid transaction date."
            '    Exit Sub
            'End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If lblStatusHid.Text = objINtx.EnumStockReceiveStatus.Active Then
            Dim strOpCdStckTxLine_Det_GET As String = "IN_CLSTRX_STOCK_RECEIVE_LN_DETAIL_GET"
            Dim strOpCdStckTxLine_DEL As String = "IN_CLSTRX_STOCKRECEIVE_LINE_DEL"
            Dim lbl As Label
            Dim ItemCode As String
            Dim StockReceiveLnID As String
            Dim strParam As String
            Dim StrTxParam As String
            Dim TxID As String
            Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

            lbl = E.Item.FindControl("ItemCode")
            ItemCode = lbl.Text
            lbl = E.Item.FindControl("RecvLnID")
            StockReceiveLnID = lbl.Text
            strParam = StockReceiveLnID & "|" & ItemCode & "|" & lstRecDoc.SelectedItem.Value.Trim & "|" & lblStckTxID.Text.Trim

            Try
                intErrNo = objINtx.mtdDelReceiveTransactLn(strOpCdStckTxLine_DEL, _
                                                           strOpCdStckTxLine_Det_GET, _
                                                           strOpCdItem_Details_UPD, _
                                                           strOpPRLN_UPD, _
                                                           strOpCdPR_Count_GET, _
                                                           strOpCdDA_Details_UPD, _
                                                           strOpDALNDet_Details_GET, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           ErrorChk)



                Select Case ErrorChk
                    Case objINtx.EnumInventoryErrorType.Overflow
                        lblError.Visible = True
                    Case objINtx.EnumInventoryErrorType.InsufficientQty
                        lblStock.Visible = True
                    Case objINtx.EnumInventoryErrorType.PRNotfound
                        lblPR.Visible = True
                End Select
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDETLN_DEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                End If
            End Try

            StrTxParam = lblStckTxID.Text & "||||||||||||||||||"

            Try
                intErrNo = objINtx.mtdUpdStockReceiveDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            StrTxParam, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReceive), _
                                                            ErrorChk, _
                                                            TxID)

                If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                    lblError.Visible = True
                End If

                lblStckTxID.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_DEL_UPD2&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                End If
            End Try
            onLoad_Display()
            onLoad_DisplayLine()
            If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
                BindDAList("")
                'ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
                '    BindItemCodeList()
            ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then
                BindSRAList("")
            ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
                BindPRList("")
            End If

            BindLocDropList()
            TxtItemCode.Text = ""

            txtQty.Text = ""
            txtCost.Text = ""
            txtAmount.Text = ""
        End If
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label
        Dim Delbutton As LinkButton
        Dim strItemCode As String
        Dim strItemDesc As String
        Dim strAcc As String
        Dim strPRID As String
        Dim lblCharge As Label
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

        ''edit confirmed
        If lblStatusHid.Text = CStr(objINtx.EnumStockReceiveStatus.Confirmed) Then
            tblAdd.Visible = True
            txtAccCode.Enabled = True

            lstChargeLevel.Enabled = True
            lstBlock.Enabled = True
            lstVehCode.Enabled = True
            lstVehExp.Enabled = True
            txtQty.Enabled = False
            Delbutton = E.Item.FindControl("Edit")
            Delbutton.Visible = False
            Delbutton = E.Item.FindControl("Cancel")
            Delbutton.Visible = True
        Else
            Delbutton = E.Item.FindControl("Edit")
            Delbutton.Visible = False
            Delbutton = E.Item.FindControl("Delete")
            Delbutton.Visible = False
            Delbutton = E.Item.FindControl("Cancel")
            Delbutton.Visible = True
        End If


        dgStkTx.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("RecvLnID")
        lblStockReceiveID.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("DocID")
        strPRID = lbl.Text.Trim
        GetDAID(strPRID)
        lblCharge = E.Item.FindControl("lblToCharge")

        TxtItemCode.Visible = True
        TxtItemName.Visible = True
        FindIN_Txt.Visible = False
        lstItem.Visible = False

        lbl = E.Item.FindControl("ItemCode")
        strItemCode = lbl.Text.Trim
        TxtItemCode.Text = Trim(strItemCode)

        lbl = E.Item.FindControl("ItemDesc")
        strItemDesc = lbl.Text.Trim
        TxtItemName.Text = strItemDesc

        'GetItem(strItemCode)

        lstChargeLevel.Items.Clear()
        BindChargeLevelDropDownList()
        lstChargeLevel.SelectedIndex = 1

        lbl = E.Item.FindControl("AccCode")
        strAcc = lbl.Text.Trim
        txtAccCode.Text = Trim(strAcc)

        GetCOADetail(txtAccCode.Text)

        lbl = E.Item.FindControl("lblQtyTrx")
        txtQty.Text = Trim(Replace(Replace(Replace(lbl.Text, ".", ""), ",", "."), "-", ""))

        lbl = E.Item.FindControl("lblUnitCost")
        txtCost.Text = Trim(Replace(Replace(Replace(lbl.Text, ".", ""), ",", "."), "-", ""))

        lbl = E.Item.FindControl("lblAmount")
        txtAmount.Text = Trim(Replace(Replace(Replace(lbl.Text, ".", ""), ",", "."), "-", ""))

        lbl = E.Item.FindControl("lblAmount")
        txtAmount.Text = Trim(Replace(Replace(Replace(lbl.Text, ".", ""), ",", "."), "-", ""))

        TxtNetTransPortFee.Text = objGlobal.DisplayForEditCurrencyFormat(GetOngkosAngkut(lstPR.SelectedItem.Text, TxtItemCode.Text))
        lEnable(False)


        If lblStckTxID.Text <> "" Then
            btnAdd.Visible = False
            btnUpdate.Visible = True
        Else
            btnAdd.Visible = True
            btnUpdate.Visible = False
        End If

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgStkTx.EditItemIndex = -1
        DisableItemTable()
        onLoad_DisplayLine()
        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
            BindDAList("")
            TxtItemCode.Visible = False
            TxtItemName.Visible = False
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
            BindSRAList("")
            TxtItemCode.Visible = False
            TxtItemName.Visible = False
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then
            BindSRAList("")
            TxtItemCode.Visible = False
            TxtItemName.Visible = False
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
            BindPRList("")
            TxtItemCode.Visible = False
            TxtItemName.Visible = False
        End If

        lstPR.Enabled = True
        TxtItemCode.Text = ""
        TxtItemName.Text = ""
        txtAccCode.Text = ""
        txtAccName.Text = ""
        txtQty.Text = ""
        txtCost.Text = ""
        txtAmount.Text = ""
        lstChargeLevel.Enabled = True
    End Sub

#End Region

#Region "BUTTON & IMAGE EVENT"

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_STOCKRECEIVE_LINE_ADD"
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strTxLnParam As New StringBuilder()
        Dim TxID As String
        Dim StrTxParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strAllowVehicle As String
        Dim PRID As String
        Dim strItemType As String
        Dim strItemCode As String
        Dim strSRIDType As String
        Dim strNewIDFormat As String
        Dim dblAmount As Double
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        strItemCode = ""

        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
            strItemCode = TxtItemCode.Text.Trim
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
            If chkCentralized.Checked = True Then
                strItemCode = TxtItemCode.Text.Trim
            Else
                strItemCode = Request.Form("TxtItemCode")
            End If
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then
            strItemCode = TxtItemCode.Text.Trim
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
            strItemCode = TxtItemCode.Text.Trim
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

        If txtRefNo.Text = "" Then
            lblErrRefNo.Visible = True
            Exit Sub
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If


        If lstStorage.SelectedItem.Value = "" Then
            lblstoragemsg.Visible = True
            Exit Sub
        Else
            lblstoragemsg.Visible = False
        End If

        If lstStorage.SelectedItem.Value = "" Then
            lblstoragemsg.Visible = True
            Exit Sub
        Else
            lblstoragemsg.Visible = False
        End If

        'If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
        If txtAccCode.Text = "" Then
            lblAccCodeErr.Visible = True
            Exit Sub
        End If
        'End If

        If InStr(strItemCode, ITEM_PART_SEPERATOR) <> 0 Then
            strItemCode = Trim(Mid(strItemCode, 1, InStr(strItemCode, ITEM_PART_SEPERATOR) - 1))
        End If

        If RowChargeTo.Visible = True And ddlChargeTo.SelectedIndex < 1 Then
            lblChargeToErr.Visible = True
            Exit Sub
        Else
            lblChargeToErr.Visible = False
        End If


        GetItemDetails(strItemCode, strItemType)

        If strItemType.Trim = objINstp.EnumInventoryItemType.DirectCharge Then
            If CheckRequiredField() Then
                Exit Sub
            End If
        End If

        If strItemCode = "" Then
            lblItemCodeErr.Visible = True
            Exit Sub
        ElseIf Trim(strFromLoc) = "" And RowFromLoc.Visible = True Then
            lblFromLocErr.Visible = True
            Exit Sub
        ElseIf lCDbl(txtQty.Text) = 0 Then
            lblErrQty.Visible = True
            Exit Sub
        ElseIf lCDbl(txtCost.Text) = 0 Then
            lblErrUnitCost.Visible = True
            Exit Sub
            'ElseIf txtAmount.Text = "" Then
            '    lblErrTotalAmt.Visible = True
            '    Exit Sub
        ElseIf Not strDate = "" Then
            If Month(strDate) < strAccMonth And Year(strDate) <= strAccYear Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If

            If Trim(lblFertInd.Text) <> "" Then
                strSRIDType = "BASTB"
                If Trim(lblFertInd.Text) = objINstp.EnumFertilizerInd.Yes Then
                    strSRIDType = "BASTP"
                Else
                    strSRIDType = "BASTB"
                End If
            Else
                strSRIDType = "BASTB"
            End If


            strAccYear = Year(strDate)
            strAccMonth = Month(strDate)
            'Select Case strCompany
            '    Case "SAM", "MIL"
            '        strNewIDFormat = strSRIDType & "-" & strLocation & "/" & Right(strAccYear, 2) & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
            '    Case Else
            '        strNewIDFormat = "NTB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
            'End Select
            strNewIDFormat = "SRV" & "/" & strCompany & "/" & strLocation & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & strAccYear & "/"


            If lblStckTxID.Text = "" Then
                StrTxParam.Append(lblStckTxID.Text)
                StrTxParam.Append("||||||")
                StrTxParam.Append(lstRecDoc.SelectedItem.Value.Trim)
                StrTxParam.Append("|")
                StrTxParam.Append(txtRefNo.Text)
                StrTxParam.Append("|")
                StrTxParam.Append(strDate)
                StrTxParam.Append("|")
                StrTxParam.Append(strLocation)
                StrTxParam.Append("|")
                StrTxParam.Append(strAccMonth)
                StrTxParam.Append("|")
                StrTxParam.Append(strAccYear)
                StrTxParam.Append("|0|")
                StrTxParam.Append(txtRemarks.Text)
                StrTxParam.Append("|||")
                'If Trim(lblFertInd.Text) <> "" Then
                '    strSRIDType = "BASTB"
                '    If Trim(lblFertInd.Text) = objINstp.EnumFertilizerInd.Yes Then
                '        strSRIDType = "BASTP"
                '    Else
                '        strSRIDType = "BASTB"
                '    End If
                'Else
                '    strSRIDType = "BASTB"
                'End If
                'If strPhyMonth.Length < 2 Then
                '    strPhyMonth = "0" & strPhyMonth
                'End If
                'strNewIDFormat = strSRIDType & "-" & strLocation & "/" & Right(strPhyYear, 2) & "/" & strPhyMonth & "/"

                StrTxParam.Append(strNewIDFormat)
                StrTxParam.Append("|")
                StrTxParam.Append(strCompany)
                StrTxParam.Append("|")
                StrTxParam.Append(Trim(ddlInventoryBin.SelectedItem.Value))


                Try
                    intErrNo = objINtx.mtdUpdStockReceiveDetail(strOpCdStckTxDet_ADD, _
                                                                strOpCdStckTxDet_UPD, _
                                                                strOpCdStckTxLine_GET, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strAccMonth, _
                                                                strAccYear, _
                                                                StrTxParam.ToString, _
                                                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReceive), _
                                                                ErrorChk, _
                                                                TxID)

                    lblStckTxID.Text = TxID
                    If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                        lblError.Visible = True
                    End If

                Catch Exp As System.Exception
                    If intErrNo <> -5 Then
                        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_ADD_CLICK&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                    End If
                End Try
            End If


            If ErrorChk = objINtx.EnumInventoryErrorType.NoError And intErrNo = 0 Then
                If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
                    If chkCentralized.Checked = True Then
                        PRID = lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim
                    Else
                        PRID = ""
                    End If
                ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
                    PRID = lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim
                ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
                    PRID = lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim
                ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then
                    PRID = lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim
                End If

                strTxLnParam.Append(lblStckTxID.Text)
                strTxLnParam.Append("|")
                strTxLnParam.Append(strItemCode.Trim)
                strTxLnParam.Append("|")
                strTxLnParam.Append(txtQty.Text)
                strTxLnParam.Append("|")
                strTxLnParam.Append(txtCost.Text)
                strTxLnParam.Append("|")
                strTxLnParam.Append(lstRecDoc.SelectedItem.Value)
                strTxLnParam.Append("|")
                strTxLnParam.Append(PRID)
                strTxLnParam.Append("|")

                'tambahan untuk stock transfer
                If strItemType.Trim = objINstp.EnumInventoryItemType.DirectCharge Or _
                    lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Or _
                    lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Or _
                    lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Or _
                    lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then

                    strTxLnParam.Append(Request.Form("txtAccCode").Trim)
                    strTxLnParam.Append("|")

                    If lstChargeLevel.SelectedIndex = 1 Then
                        strTxLnParam.Append(Request.Form("lstBlock").Trim)
                    Else
                        strTxLnParam.Append(Request.Form("lstPreBlock").Trim)
                    End If
                    strTxLnParam.Append("|")
                    strTxLnParam.Append(Request.Form("lstVehCode").Trim)
                    strTxLnParam.Append("|")
                    strTxLnParam.Append(Request.Form("lstVehExp").Trim)
                Else
                    strTxLnParam.Append("|||")
                End If

                If lstRecDoc.SelectedItem.Value.Trim = objINtx.EnumStockReceiveDocType.StockTransfer Then
                    strTxLnParam.Append("|")
                    strTxLnParam.Append(Request.Form("lstFromLoc").Trim)
                Else
                    strTxLnParam.Append("|")
                End If


                'tambahan untuk stock transfer
                If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
                    dblAmount = Round(lCDbl(Trim(txtQty.Text)) * lCDbl(Trim(txtCost.Text)), 2)
                Else
                    If lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim <> "NoDA" Then
                        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then

                            If (lCDbl(lblPBBKB.Text) + 0) > 0 Then
                                dblAmount = lCDbl(Trim(txtQty.Text)) * lCDbl(Trim(txtCost.Text)) + lCDbl(Trim(txtQty.Text)) * lCDbl(Trim(txtCost.Text)) * lCDbl(lblPBBKB.Text) / 100
                            Else
                                dblAmount = Round(lCDbl(Trim(txtQty.Text)) * lCDbl(Trim(txtCost.Text)), 2)
                            End If

                        Else
                            dblAmount = Round(lCDbl(txtAmount.Text), 2) ' 'Round(CDbl(Trim(txtQty.Text)) * CDbl(Trim(txtCost.Text)), 2)
                        End If
                    Else
                        dblAmount = Round(CDbl(Trim(txtQty.Text)) * CDbl(Trim(txtCost.Text)), 2)
                    End If
                End If

                strTxLnParam.Append("|")

                strTxLnParam.Append(dblAmount)
                If RowChargeTo.Visible = True Then
                    strTxLnParam.Append("|")
                    strTxLnParam.Append(ddlChargeTo.SelectedItem.Value.Trim())
                Else
                    strTxLnParam.Append("|")
                    strTxLnParam.Append(Session("SS_LOCATION"))
                End If

                strTxLnParam.Append("|")

                If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer And chkCentralized.Checked = False Then
                    strTxLnParam.Append(strItemCode)
                Else
                    strTxLnParam.Append(lstItem.SelectedItem.Value)
                End If


                'MsgBox(Err.Description)
                Try

                    If lstChargeLevel.SelectedIndex = 0 And RowPreBlk.Visible = True Then

                        strParamList = ddlChargeTo.SelectedItem.Value.Trim() & "|" & _
                                       txtAccCode.Text & "|" & _
                                       lstPreBlock.SelectedItem.Value.Trim & "|" & _
                                       objGLset.EnumBlockStatus.Active & "|" & _
                                       strAccMonth & "|" & strAccYear

                        intErrNo = objINtx.mtdAddStockReceiveLnByBlock(strOpCodeGLSubBlkByBlk, _
                                                                       strParamList, _
                                                                       strOpCdStckTxLine_ADD, _
                                                                       strOpCdItem_Details_GET, _
                                                                       strOpCdItem_Details_UPD, _
                                                                       strOpDALNDet_Details_GET, _
                                                                       strOpDALN_UPD, _
                                                                       strOpCdDA_Count_GET, _
                                                                       strOpCdDA_Details_UPD, _
                                                                       strCompany, _
                                                                       strLocation, _
                                                                       strUserId, _
                                                                       strAccMonth, _
                                                                       strAccYear, _
                                                                       objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReceiveLn), _
                                                                       objINtx.EnumStockReceiveDocType.DispatchAdvice, _
                                                                       ErrorChk, _
                                                                       strTxLnParam.ToString, _
                                                                       strLocType)

                    Else

                        intErrNo = objINtx.mtdAddStockReceiveLnDA(strOpCdStckTxLine_ADD, _
                                                                strOpCdItem_Details_GET, _
                                                                strOpCdItem_Details_UPD, _
                                                                strOpDALNDet_Details_GET, _
                                                                strOpDALN_UPD, _
                                                                strOpCdDA_Count_GET, _
                                                                strOpCdDA_Details_UPD, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strAccMonth, _
                                                                strAccYear, _
                                                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReceiveLn), _
                                                                objINtx.EnumStockReceiveDocType.DispatchAdvice, _
                                                                ErrorChk, _
                                                                strTxLnParam.ToString)

                    End If
                Catch Exp As System.Exception
                    If intErrNo <> -5 Then
                        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_ADDLINE_CLICK&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                    End If
                End Try

                Select Case ErrorChk
                    Case objINtx.EnumInventoryErrorType.Overflow
                        lblError.Visible = True
                    Case objINtx.EnumInventoryErrorType.InsufficientQty
                        lblStock.Visible = True
                    Case objINtx.EnumInventoryErrorType.PRNotfound
                        lblPR.Visible = True
                End Select

            End If

            '''---------UPDATE STORAGE SELECT - SUPAYA TIDAK MENGUBAH DLL
            Dim strParamName As String
            Dim strParamValue As String
            Dim strTransID As String
            Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
            Dim sSQLKriteria As String = ""
            Dim objdsST As New DataSet

            If lblStckTxID.Text.Trim = "" Then
                strTransID = TxID
            Else
                strTransID = lblStckTxID.Text.Trim
            End If

            sSQLKriteria = "UPDATE IN_STOCKRECEIVELN SET StorageCode='" & lstStorage.SelectedItem.Value & "' Where StockReceiveID='" & strTransID & "'"

            strParamName = "STRSEARCH"
            strParamValue = sSQLKriteria

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                             strParamName, _
                                             strParamValue, _
                                             objdsST)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            ''---------------------

            StrTxParam.Remove(0, StrTxParam.Length)
            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("||||||||||||||||||")

            If ErrorChk = objINtx.EnumInventoryErrorType.NoError And intErrNo = 0 Then
                Try
                    intErrNo = objINtx.mtdUpdStockReceiveDetail(strOpCdStckTxDet_ADD, _
                                                                strOpCdStckTxDet_UPD, _
                                                                strOpCdStckTxLine_GET, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strAccMonth, _
                                                                strAccYear, _
                                                                StrTxParam.ToString, _
                                                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReceive), _
                                                                ErrorChk, _
                                                                TxID)
                    If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                        lblError.Visible = True
                    End If

                    lblStckTxID.Text = TxID
                Catch Exp As System.Exception
                    If intErrNo <> -5 Then
                        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRECEIVE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                    End If
                End Try

            End If


           

            If Not StrTxParam Is Nothing Then
                StrTxParam = Nothing
            End If

            If Not strTxLnParam Is Nothing Then
                strTxLnParam = Nothing
            End If

            onLoad_Display()
            onLoad_DisplayLine()

    
            If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
                BindDAList("")
            ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
                BindSTList("")
            ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then
                BindSRAList("")
            ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
                BindPRList("")
            End If

            TxtItemCode.Text = ""
            TxtItemName.Text = ""
            txtAccCode.Text = ""
            txtAccName.Text = ""
            txtQty.Text = ""
            txtCost.Text = ""
            txtAmount.Text = ""
            TxtNetTransPortFee.Text = ""

            BindLocDropList()
            DisablePage()
            BindItemCodeList()

            End If

    End Sub

    Sub btnUpdate_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_STOCKRECEIVE_LINE_UPDATECOA"
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strTxLnParam As New StringBuilder()
        Dim TxID As String
        Dim StrTxParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strAllowVehicle As String
        Dim PRID As String
        Dim strItemType As String
        Dim strItemCode As String
        Dim strSRIDType As String
        Dim strNewIDFormat As String
        Dim dblAmount As Double
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        Dim ParamNama As String
        Dim ParamValue As String


        strItemCode = ""
        If lstRecDoc.SelectedIndex = 0 Then
            strItemCode = Request.Form("lstItem")
        ElseIf lstRecDoc.SelectedIndex = 1 Then
            strItemCode = Request.Form("txtItemCode")
        ElseIf lstRecDoc.SelectedIndex = 2 Then
            strItemCode = Request.Form("lstItem")
        ElseIf lstRecDoc.SelectedIndex = 3 Then
            strItemCode = Request.Form("lstItem")
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

        If txtRefNo.Text = "" Then
            lblErrRefNo.Visible = True
            Exit Sub
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
            If txtAccCode.Text = "" Then
                lblAccCodeErr.Visible = True
                Exit Sub
            End If
        End If

        If lstChargeLevel.SelectedIndex = 0 Then
            strBlkVal = lstPreBlock.SelectedItem.Value
        Else
            strBlkVal = lstBlock.SelectedItem.Value
        End If

        If InStr(strItemCode, ITEM_PART_SEPERATOR) <> 0 Then
            strItemCode = Trim(Mid(strItemCode, 1, InStr(strItemCode, ITEM_PART_SEPERATOR) - 1))
        End If
        If RowChargeTo.Visible = True And ddlChargeTo.SelectedIndex < 1 Then
            lblChargeToErr.Visible = True
            Exit Sub
        Else
            lblChargeToErr.Visible = False
        End If

        ''UPDATE HEADER
        ParamNama = ""
        ParamValue = ""

        ParamNama = "UPDATESTR"
        ParamValue = "SET UpdateDate='" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',UpdateID='" & strUserId & "' where StockReceiveID = '" & lblStckTxID.Text & "'"

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCdStckTxDet_UPD, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MAIN_ADD&errmesg=" & ex.ToString() & "&redirect=")
        End Try


        '''UPDATE DETAIL
        ParamNama = ""
        ParamValue = ""

        ParamNama = "COA|BLKCODE|LNID|DOCID|NTB"
        ParamValue = txtAccCode.Text.Trim & "|" & _
                     strBlkVal & "|" & _
                     lblStockReceiveID.Text & "|" & _
                     lstPR.Text & "|" & _
                     lblStckTxID.Text
        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCdStckTxLine_ADD, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MAIN_ADD&errmesg=" & ex.ToString() & "&redirect=")
        End Try

        onLoad_Display()
        onLoad_DisplayLine()

        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
            BindDAList("")
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
            TxtItemCode.Text = ""
            TxtItemName.Text = ""

            'TxtItemCode.Visible = True
            'TxtItemName.Visible = True
            'FindIN_Txt.Visible = True
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then
            BindSRAList("")
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
            BindPRList("")

        End If

        BindLocDropList()
        DisablePage()
        lEnable(True)
        TxtItemCode.Text = ""

        txtAccCode.Text = ""
        txtAccName.Text = ""
        txtQty.Text = ""
        txtCost.Text = ""
        txtAmount.Text = ""
        TxtNetTransPortFee.Text = ""
 

    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim TxID As String
        Dim StrTxParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strAllowVehicle As String
        Dim strSRIDType As String
        Dim strNewIDFormat As String
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
        If lblStckTxID.Text = "" Then
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

        If Trim(lstRecDoc.SelectedItem.Value) = objINtx.EnumStockReceiveDocType.DispatchAdvice And Trim(lblStckTxID.Text) = "" Then
            If Len(TxtItemCode.Text) = 0 Then
                lblItemCodeErr.Visible = True
                Exit Sub
            End If
        End If
        If lblFertInd.Text = "" And Trim(lblStckTxID.Text) = "" Then
            lblItemCodeErr.Visible = True
            Exit Sub
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        If Trim(lblFertInd.Text) <> "" Then
            strSRIDType = "BASTB"
            If Trim(lblFertInd.Text) = objINstp.EnumFertilizerInd.Yes Then
                strSRIDType = "BASTP"
            Else
                strSRIDType = "BASTB"
            End If
        Else
            strSRIDType = "BASTB"
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strNewIDFormat = strSRIDType & "-" & strLocation & "/" & Right(strAccYear, 2) & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        '    Case Else
        '        strNewIDFormat = "NTB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        strNewIDFormat = "NTB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        If lblStckTxID.Text = "" Then
            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("||||||")
            StrTxParam.Append(lstRecDoc.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(txtRefNo.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|0|")
            StrTxParam.Append(txtRemarks.Text)
            StrTxParam.Append("|||")
        Else
            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("||||||")
            StrTxParam.Append(lstRecDoc.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(txtRefNo.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|||||")
            StrTxParam.Append(txtRemarks.Text)
            StrTxParam.Append("|||")
        End If
        'If Trim(lblFertInd.Text) <> "" Then
        '    If Trim(lblFertInd.Text) = objINStp.EnumFertilizerInd.Yes Then
        '        strSRIDType = "BASTP"
        '    Else
        '        strSRIDType = "BASTB"
        '    End If
        'Else
        '    strSRIDType = "BASTB"
        'End If
        'If strPhyMonth.Length < 2 Then
        '    strPhyMonth = "0" & strPhyMonth
        'End If
        'strNewIDFormat = strSRIDType & "-" & strLocation & "/" & Right(strPhyYear, 2) & "/" & strPhyMonth & "/"
        StrTxParam.Append(strNewIDFormat)
        StrTxParam.Append("|")
        StrTxParam.Append(strCompany)
        StrTxParam.Append("|")
        StrTxParam.Append(Trim(ddlInventoryBin.SelectedItem.Value))

        Try
            intErrNo = objINtx.mtdUpdStockReceiveDetail(strOpCdStckTxDet_ADD, _
                                                        strOpCdStckTxDet_UPD, _
                                                        strOpCdStckTxLine_GET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReceive), _
                                                        ErrorChk, _
                                                        TxID)
            lblStckTxID.Text = TxID
            If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                lblError.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
            End If
        End Try

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        onLoad_Display()
        DisablePage()

    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim colParam As New Collection
        Dim colOpCode As New Collection
        Dim intError As Integer
        Dim strErrMsg As String

        Dim objUOMDs As New Object
        Dim strOpCd_GetUOM As String = "ADMIN_CLSUOM_CONVERTION_LIST_GET"
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
        If lblStckTxID.Text = "" Then
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

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        colOpCode.Add("IN_CLSTRX_STOCK_RECEIVE_GET_FOR_CONFIRM", "STOCK_RECEIVE_GET")
        colOpCode.Add("IN_CLSTRX_STOCKRECEIVE_DETAIL_UPD", "STOCK_RECEIVE_UPD")
        colOpCode.Add("IN_CLSTRX_STOCKITEM_DETAILS_GET", "STOCK_ITEM_GET")
        colOpCode.Add("IN_CLSTRX_STOCKITEM_DETAIL_UPD", "STOCK_ITEM_UPD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_DETAIL_ADD", "JOURNAL_ADD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_DETAIL_UPD", "JOURNAL_UPD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_LINE_ADD", "JOURNAL_LINE_ADD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_LINE_GET", "JOURNAL_LINE_GET")

        If strPurchaseUOM <> strUOMCode Then
            strParamUOM = strPurchaseUOM & "|" & _
                            strUOMCode & "|" & _
                            objAdmin.EnumUOMStatus.Active & "|A.UOMFrom"

            Try
                intErrNo = objAdmin.mtdGetUOMRate(strOpCd_GetUOM, strParamUOM, objUOMDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_GET_UOMConvertion&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End Try
            If objUOMDs.Tables(0).Rows.Count > 0 Then
                dblRate = Convert.ToDouble(objUOMDs.Tables(0).Rows(0).Item("Rate"))
            Else
                strParamUOM = strUOMCode & "|" & _
                            strPurchaseUOM & "|" & _
                            objAdmin.EnumUOMStatus.Active & "|A.UOMFrom"

                Try
                    intErrNo = objAdmin.mtdGetUOMRate(strOpCd_GetUOM, strParamUOM, objUOMDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_GET_UOMConvertion&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                End Try

                If objUOMDs.Tables(0).Rows.Count > 0 Then
                    If Convert.ToDouble(objUOMDs.Tables(0).Rows(0).Item("Rate")) <> 0 Then
                        dblRate = 1.0 / Convert.ToDouble(objUOMDs.Tables(0).Rows(0).Item("Rate"))
                    Else
                        dblRate = 1
                    End If
                Else
                    dblRate = 1
                End If
            End If
        Else
            dblRate = 1
        End If


        colParam.Add(strCompany, "COMPANY")
        colParam.Add(strLocation, "LOCCODE")
        colParam.Add(strUserId, "USER_ID")
        colParam.Add(lblStckTxID.Text, "STOCK_RECEIVE_ID")
        colParam.Add("Inter-" & GetCaption(objLangCap.EnumLangCap.Location), "MS_INTER_LOCATION")
        colParam.Add(GetCaption(objLangCap.EnumLangCap.Account), "MS_COA")
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), Session("SS_CONFIGSETTING")) = True Then
            colParam.Add(GetCaption(objLangCap.EnumLangCap.Block), "MS_BLOCK")
        Else
            colParam.Add(GetCaption(objLangCap.EnumLangCap.SubBlock), "MS_BLOCK")
        End If
        colParam.Add(GetCaption(objLangCap.EnumLangCap.Vehicle), "MS_VEHICLE")
        colParam.Add(GetCaption(objLangCap.EnumLangCap.VehExpense), "MS_VEHEXP")
        colParam.Add(dblRate, "RATE_UOM")

        intError = objINtx.EnumTransactionError.NoError
        strErrMsg = ""


        Try
            intErrNo = objINtx.mtdStockReceive_Confirm(colOpCode, _
                                                       colParam, _
                                                       intError, _
                                                       strErrMsg)

            If intError = objINtx.EnumTransactionError.NoError Then
                onLoad_Display()
                onLoad_DisplayLine()
                DisablePage()
            Else
                lblConfirmErr.Text = strErrMsg
                lblConfirmErr.Visible = True
            End If
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_UPD&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
            End If
        End Try

        If intError = objINtx.EnumTransactionError.NoError Then
            Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""

            strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

            strParamValue = Trim(strLocation) & _
                            "|" & "IN_STOCKRECEIVE" & _
                            "|" & "IN_STOCKRECEIVELN" & _
                            "|" & "STOCKRECEIVEID" & _
                            "|" & Trim(lblStckTxID.Text) & _
                            "|" & "QTY" & _
                            "|" & ddlInventoryBin.SelectedItem.Value & _
                            "|" & "+" & _
                            "|" & Trim(strUserId)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
            End Try
        End If
    End Sub

    Sub btnPrint_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String = ""
        Dim intStatus As Integer
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String
        Dim intErrNo As Integer

        strUpdString = "where StockReceiveID = '" & lblStckTxID.Text & "'"
        strPrintDate = Trim(lblPrintDate.Text)
        strStatus = Trim(Status.Text)
        intStatus = CInt(Trim(lblStatusHid.Text))
        strSortLine = "IN_STOCKRECEIVELN.StockReceiveLnId"
        strTable = "IN_STOCKRECEIVE"

        'onLoad_Display()
        'onLoad_DisplayLine()

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockReceivePrint.aspx?STOCKRECEIVEID=" & lblStckTxID.Text & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
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

        If lblStatusHid.Text = objINtx.EnumStockReceiveStatus.Deleted Then
            Try


                intErrNo = objINtx.mtdReceiveInvItemLevel(strOpCdStckTxLine_GET, _
                                                          strOpCdItem_Details_UPD, _
                                                          strOpCdItem_Details_GET, _
                                                          strOpCdPR_Count_GET, _
                                                          strOpCdPR_Details_UPD, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          lblStckTxID.Text, _
                                                          strOpPRLNDet_Details_GET, _
                                                          strOpPRLN_UPD, _
                                                          objINtx.EnumTransactionAction.Undelete, _
                                                          objGlobal.EnumDocType.DispatchAdvice, _
                                                          ErrorChk)


            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                End If
            End Try
            StrTxParam = lblStckTxID.Text & "|||||||||||||||" & objINtx.EnumStockReceiveStatus.Active & "|||"

        ElseIf lblStatusHid.Text = objINtx.EnumStockReceiveStatus.Active Then

            Try

                intErrNo = objINtx.mtdReceiveInvItemLevel(strOpCdStckTxLine_GET, _
                                                          strOpCdItem_Details_UPD, _
                                                          strOpCdItem_Details_GET, _
                                                          strOpCdPR_Count_GET, _
                                                          strOpCdPR_Details_UPD, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          lblStckTxID.Text, _
                                                          strOpPRLNDet_Details_GET, _
                                                          strOpPRLN_UPD, _
                                                          objINtx.EnumTransactionAction.Delete, _
                                                          objGlobal.EnumDocType.DispatchAdvice, _
                                                          ErrorChk)


            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                End If
            End Try
            StrTxParam = lblStckTxID.Text & "|||||||||||||||" & objINtx.EnumStockReceiveStatus.Deleted & "|||"
        End If

        If intErrNo = 0 And Not ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            Try
                intErrNo = objINtx.mtdUpdStockReceiveDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            StrTxParam, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReceive), _
                                                            ErrorChk, _
                                                            TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_DEL_UPD&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                End If
            End Try
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            lblUnDel.Visible = True
        End If

        onLoad_Display()
        onLoad_DisplayLine()
        DisablePage()
    End Sub

    Sub btnNew_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_trx_StockReceive_Details.aspx")
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_StockReceive_List.aspx")
    End Sub

    Sub BtnAddAllItem_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCode_GenSR As String
        Dim dsMaster As Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
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

        If txtRefNo.Text = "" Then
            lblErrRefNo.Visible = True
            Exit Sub
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        If Trim(lstPR.SelectedItem.Value) = "" Then
            lblErrDoc.Visible = True
            Exit Sub
        End If

        strOpCode_GenSR = "IN_CLSTRX_SR_GENERATE_FROM_DA"
        strParamName = "STOCKRECEIVEID|LOCCODE|DISPADVID|ACCYEAR|ACCMONTH|DOCTYPE|REFNO|REFDATE|BIN|USERID"
        strParamValue = Trim(lblStckTxID.Text) & _
                        "|" & Trim(strLocation) & _
                        "|" & Trim(lstPR.SelectedItem.Text) & _
                        "|" & Year(strDate) & _
                        "|" & Month(strDate) & _
                        "|" & lstRecDoc.SelectedItem.Value.Trim & _
                        "|" & Trim(txtRefNo.Text) & _
                        "|" & strDate & _
                        "|" & ddlInventoryBin.SelectedItem.Value & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode_GenSR, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsMaster)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try


        If dsMaster.Tables(0).Rows.Count > 0 Then
            lblStckTxID.Text = Trim(dsMaster.Tables(0).Rows(0).Item("StockReceiveID"))
            If lblStckTxID.Text = "" Then
                lblError.Text = Trim(dsMaster.Tables(0).Rows(0).Item("ErrDescr"))
                lblError.Visible = True
                Exit Sub
            ElseIf Trim(dsMaster.Tables(0).Rows(0).Item("ErrDescr")) <> "" Then
                lblError.Text = Trim(dsMaster.Tables(0).Rows(0).Item("ErrDescr"))
                lblError.Visible = True
            End If
        End If

        onLoad_Display()
        onLoad_DisplayLine()
        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
            BindDAList("")
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
            BindItemCodeList()
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then
            BindSRAList("")
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
            BindPRList("")
        End If

        BindLocDropList()
        DisablePage()
        TxtItemCode.Text = ""

        txtQty.Text = ""
        txtCost.Text = ""
        txtAmount.Text = ""
    End Sub

#End Region

#End Region

#Region "locaL procedure & function "

    Sub BindChargeLevelDropDownList()
        lstChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        lstChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        lstChargeLevel.SelectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        ToggleChargeLevel()
    End Sub

    Sub ToggleChargeLevel()
        If lstChargeLevel.SelectedIndex = 0 Then
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
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_GET_LANGCAP_COST_LEVEL&errmesg=" & lblErrMessage.Text & "&redirect=in/trx/in_trx_stockreceive_list.aspx")
        End Try

        AccountTag = GetCaption(objLangCap.EnumLangCap.Account)
        VehicleTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        VehExpCodeTag = GetCaption(objLangCap.EnumLangCap.VehExpense)
        strLocationTag = GetCaption(objLangCap.EnumLangCap.Location)

        lblAccTag.Text = AccountTag & lblCode.Text & " (DR) :*"
        lblBlkTag.Text = BlockTag & lblCode.Text & " : "
        lblVehTag.Text = VehicleTag & lblCode.Text & " : "
        lblVehExpTag.Text = VehExpCodeTag & lblCode.Text & " : "

        lblAccCodeErr.Text = lblPleaseSelect.Text & AccountTag & lblCode.Text
        lblBlockErr.Text = lblPleaseSelect.Text & BlockTag & lblCode.Text
        lblVehCodeErr.Text = lblPleaseSelect.Text & VehicleTag & lblCode.Text
        lblVehExpCodeErr.Text = lblPleaseSelect.Text & VehExpCodeTag & lblCode.Text
        lblFromLocErr.Text = lblPleaseSelect.Text & strLocationTag & lblCode.Text
        lblChargeToErr.Text = lblPleaseSelect.Text & strLocationTag & lblCode.Text

        dgStkTx.Columns(3).HeaderText = AccountTag & lblCode.Text
        dgStkTx.Columns(4).HeaderText = BlockTag & lblCode.Text
        dgStkTx.Columns(5).HeaderText = VehicleTag & lblCode.Text
        dgStkTx.Columns(6).HeaderText = VehExpCodeTag & lblCode.Text
        dgStkTx.Columns(7).HeaderText = "From " & strLocationTag
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
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=in/trx/in_trx_stockreceive_list.aspx")
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

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        ItemTypeCheck()
    End Sub

    Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Sub DisablePage()
        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
            lstItem.Visible = True
            lstItem.AutoPostBack = True
            TxtItemCode.Visible = False
            TxtItemName.Visible = False
            FindIN_Txt.Visible = False
            tblPR.Visible = True
            lblSRType.Text = "Dispatch Advice ID :*"
            RowFromLoc.Visible = False
            lblAccTag.Text = AccountTag & lblCode.Text & " (CR) :*"

        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
            '   lstItem.Visible = False
            chkCentralized.Visible = True
            If chkCentralized.checked = False Then
                TxtItemCode.Visible = True
                TxtItemName.Visible = True
                FindIN_Txt.Visible = True
            Else
                TxtItemCode.Visible = False
                TxtItemName.Visible = False
                FindIN_Txt.Visible = False
                lstitem.visible = True
            End If
            'lstItem.AutoPostBack = True
            tblPR.Visible = True
            RowFromLoc.Visible = True
            lblSRType.Text = "Dispatch Advice ID :*"
            lblAccTag.Text = AccountTag & lblCode.Text & " (CR) :*"
            ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then
                'lstItem.AutoPostBack = True
                lstItem.Visible = True
                TxtItemCode.Visible = False
                TxtItemName.Visible = False
                FindIN_Txt.Visible = False
                tblPR.Visible = True
                lblSRType.Text = "Stock Return Advice ID :*"
                RowFromLoc.Visible = False
                lblAccTag.Text = AccountTag & lblCode.Text & " (CR) :*"
            ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
                'lstItem.AutoPostBack = True
                FindIN_Txt.Visible = False
                lstItem.Visible = True
                TxtItemCode.Visible = False
                TxtItemName.Visible = False
                tblPR.Visible = True
                lblSRType.Text = "Purchase Requisition ID :*"
                RowFromLoc.Visible = False
                lblAccTag.Text = AccountTag & lblCode.Text & " (CR) :*"
            End If


            lstRecDoc.Enabled = False
            txtRefNo.Enabled = False
            txtDate.Enabled = False
            txtRemarks.Enabled = False
            btnSelDate.Visible = False
            Save.Visible = False
            Confirm.Visible = False
            Print.Visible = True
            PRDelete.Visible = False
            btnNew.Visible = False
            ddlInventoryBin.Enabled = False

            Select Case lblStatusHid.Text.Trim
                Case CStr(objINtx.EnumStockReceiveStatus.Deleted)
                    tblPR.Visible = False
                    PRDelete.Visible = False
                    btnNew.Visible = True
                    'PRDelete.ImageUrl = "../../images/butt_undelete.gif"
                    PRDelete.AlternateText = "Undelete"
                    PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                Case CStr(objINtx.EnumStockReceiveStatus.Confirmed)
                    tblPR.Visible = False
                    btnNew.Visible = True
                Case Else
                    txtRefNo.Enabled = True
                    txtDate.Enabled = True
                    btnSelDate.Visible = True
                    txtRemarks.Enabled = True
                    Save.Visible = True
                    ddlInventoryBin.Enabled = True
                    If lblStckTxID.Text.Trim = "" Then
                        lstRecDoc.Enabled = True
                    Else
                        Confirm.Visible = True
                        PRDelete.Visible = True
                        btnNew.Visible = True
                        PRDelete.ImageUrl = "../../images/butt_delete.gif"
                        PRDelete.AlternateText = "Delete"
                        PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    End If
            End Select

            dgStkTx.Columns(2).Visible = Session("SS_INTER_ESTATE_CHARGING")
            DisableItemTable()


    End Sub

    Sub DisableItemTable()

        If lblStatusHid.Text = CStr(objINtx.EnumStockReceiveStatus.Deleted) Or _
           lblStatusHid.Text = CStr(objINtx.EnumStockReceiveStatus.Confirmed) Then
            tblAdd.Visible = False

        Else
            tblAdd.Visible = True

        End If

    End Sub

    Sub onLoad_Display()

        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_STOCKRECEIVE_DETAIL_GET"

        strParam = lblStckTxID.Text.Trim
        Try
            intErrNo = objINtx.mtdGetStockTransactDetails(strOpCdStckTxDet_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          objStkRcv)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        Status.Text = objINtx.mtdGetStockReceiveStatus(objStkRcv.Tables(0).Rows(0).Item("Status").Trim())
        lblAccPeriod.Text = objStkRcv.Tables(0).Rows(0).Item("AccMonth") & "/" & objStkRcv.Tables(0).Rows(0).Item("AccYear")
        lblStatusHid.Text = objStkRcv.Tables(0).Rows(0).Item("Status").Trim()
        CreateDate.Text = objGlobal.GetLongDate(Trim(objStkRcv.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = objGlobal.GetLongDate(Trim(objStkRcv.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = objStkRcv.Tables(0).Rows(0).Item("UserName").Trim()
        txtRemarks.Text = objStkRcv.Tables(0).Rows(0).Item("Remark").Trim()
        lblTotAmtFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objStkRcv.Tables(0).Rows(0).Item("TotalAmount"), 2), 2)
        txtDate.Text = objGlobal.GetShortDate(strDateFMT, Trim(objStkRcv.Tables(0).Rows(0).Item("StockRefDate")))
        txtRefNo.Text = objStkRcv.Tables(0).Rows(0).Item("StockRefNo").Trim()
        chkCentralized.Checked = True
        BindInventoryBinLevel(Trim(objStkRcv.Tables(0).Rows(0).Item("Bin")))
    End Sub

    Protected Function onLoad_DisplayLine() As DataSet
        Dim strParam As String
        Dim intCnt As Integer
        Dim UpdButton As LinkButton
        Dim strStorage As String

        strParam = lblStckTxID.Text.Trim
        Try
            intErrNo = objINtx.mtdGetStockTransactDetails(strOpCdStckTxLine_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          objStkRcvLn)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDETLN_GET&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        lstStorage.Enabled = True
        For intCnt = 0 To objStkRcvLn.Tables(0).Rows.Count - 1

            objStkRcvLn.Tables(0).Rows(intCnt).Item("Description") = objStkRcvLn.Tables(0).Rows(intCnt).Item("ItemCode").Trim() & " (" & _
                                                                     objStkRcvLn.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            lstStorage.Enabled = False

            strStorage = Trim(objStkRcvLn.Tables(0).Rows(intCnt).Item("StorageCode"))


        Next intCnt

        BindStorage(strStorage)


        dgStkTx.DataSource = objStkRcvLn.Tables(0)
        dgStkTx.DataBind()

        If objStkRcvLn.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
        Else
            TrLink.Visible = False
        End If

        Return objStkRcvLn

    End Function

    Sub Centralized_Type(ByVal Sender As Object, ByVal E As EventArgs)
        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
            If chkCentralized.Checked = True Then
                chkCentralized.Text = "Centralized"
                tblPR.Visible = True
                'lstPR.Visible = True
                FindIN_Txt.Visible = False
                lstItem.Visible = True
                TxtItemCode.Visible = False
                TxtItemName.Visible = False
                'btnAddAllItem.Visible = True
                'lblSRType.Visible = True
            Else
                tblPR.Visible = False
                'lblSRType.Visible = False
                chkCentralized.Text = " Open Stock"
                FindIN_Txt.Visible = True
                lstItem.Visible = False
                'lstPR.Visible = False
                TxtItemCode.Visible = True
                TxtItemName.Visible = True
                'btnAddAllItem.Visible = False
            End If
            chkCentralized.Visible = True
        End If
    End Sub

    Sub lEnable(ByVal pStatus As Boolean)
        With Me

            If pStatus = False Then
                .txtQty.Enabled = False
                .txtCost.Enabled = False
                .txtAmount.Enabled = False
                .TxtItemCode.Enabled = False
                .lstPR.Enabled = False
                .btnAddAllItem.Enabled = False
                .btnUpdate.Visible = True
                .btnAdd.Visible = False

            Else
                .txtQty.Enabled = True
                .txtCost.Enabled = True
                .txtAmount.Enabled = False 'True
                .TxtItemCode.Enabled = True
                .lstPR.Enabled = True
                .btnUpdate.Visible = False
                .btnAddAllItem.Enabled = False
                .btnAdd.Visible = True
            End If
        End With
    End Sub

    Sub BindDocList()
        lstRecDoc.Items.Clear()
        lstRecDoc.Items.Add(New ListItem(objINtx.mtdGetStockReceiveDocType(objINtx.EnumStockReceiveDocType.DispatchAdvice), objINtx.EnumStockReceiveDocType.DispatchAdvice))
        lstRecDoc.Items.Add(New ListItem(objINtx.mtdGetStockReceiveDocType(objINtx.EnumStockReceiveDocType.StockTransfer), objINtx.EnumStockReceiveDocType.StockTransfer))
        lstRecDoc.Items.Add(New ListItem(objINtx.mtdGetStockReceiveDocType(objINtx.EnumStockReceiveDocType.StockReturnAdvice), objINtx.EnumStockReceiveDocType.StockReturnAdvice))
        lstRecDoc.Items.Add(New ListItem(objINtx.mtdGetStockReceiveDocType(objINtx.EnumStockReceiveDocType.PurchaseRequisition), objINtx.EnumStockReceiveDocType.PurchaseRequisition))
        If Not Request.QueryString("Id") = "" Then
            Select Case objStkRcv.Tables(0).Rows(0).Item("StockDocType").Trim()
                Case objINtx.EnumStockReceiveDocType.DispatchAdvice
                    lstRecDoc.SelectedIndex = 0
                Case objINtx.EnumStockReceiveDocType.StockTransfer
                    lstRecDoc.SelectedIndex = 1
                Case objINtx.EnumStockReceiveDocType.StockReturnAdvice
                    lstRecDoc.SelectedIndex = 2
                Case objINtx.EnumStockReceiveDocType.PurchaseRequisition
                    lstRecDoc.SelectedIndex = 3
            End Select
        End If

        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
            BindDAList("")
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then
            BindSRAList("")
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
            BindPRList("")
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
            BindSTList("")
        End If

    End Sub

    Sub BindStorage(ByVal pv_strcode As String)
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer

        sSQLKriteria = "Select StorageCode,Description From IN_STORAGE Where LocCode='" & strLocation & "'"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objdsST.Tables(0).Rows.Count - 1
            objdsST.Tables(0).Rows(intCnt).Item("StorageCode") = Trim(objdsST.Tables(0).Rows(intCnt).Item("StorageCode"))
            objdsST.Tables(0).Rows(intCnt).Item("Description") = objdsST.Tables(0).Rows(intCnt).Item("StorageCode") & " (" & Trim(objdsST.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objdsST.Tables(0).Rows(intCnt).Item("StorageCode") = Trim(pv_strcode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objdsST.Tables(0).NewRow()
        dr("StorageCode") = ""
        dr("Description") = "Please Select Storage"
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        lstStorage.DataSource = objdsST.Tables(0)
        lstStorage.DataValueField = "StorageCode"
        lstStorage.DataTextField = "Description"
        lstStorage.DataBind()
        lstStorage.SelectedIndex = intSelectedIndex

    End Sub

    Sub GetItem(ByVal pv_strItemCode As String)
        'Dim dr As DataRow
        'Dim intCnt As Integer = 0
        'Dim intErrNo As Integer
        'Dim intSelectedIndex As Integer = 0


        'Dim strOpCode As String = "IN_CLSTRX_ITEMPART_ITEM_GET"
        'Dim dsMaster As Object
        'Dim strParamName As String = ""
        'Dim strParamValue As String = ""

        'strParamName = "SEARCHSTR|SORTEXP"
        'strParamValue = " And itm.ItemCode = '" & Trim(pv_strItemCode) & "' AND itm.LocCode = '" & strLocation & "' AND itm.Status = '" & objINstp.EnumStockItemStatus.Active & "'  " & "|itm.ItemCode"

        'Try
        '    intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
        '                                        strParamName, _
        '                                        strParamValue, _
        '                                        dsMaster)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        'End Try

        'For intCnt = 0 To dsMaster.Tables(0).Rows.Count - 1
        '    If dsMaster.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(pv_strItemCode) Then
        '        intSelectedIndex = intCnt + 1
        '    End If
        'Next intCnt

        'dr = dsMaster.Tables(0).NewRow()
        'dr("ItemCode") = ""
        'dr("Description") = "Please select Item Code"
        'dsMaster.Tables(0).Rows.InsertAt(dr, 0)

        'lstItem.DataSource = dsMaster.Tables(0)
        'lstItem.DataValueField = "ItemCode"
        'lstItem.DataTextField = "Description"
        'lstItem.DataBind()
        'lstItem.SelectedIndex = intSelectedIndex
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
            txtAccName.Text = objCOADs.Tables(0).Rows(0).Item("Description")

        End If
    End Sub

    Sub GetPRCost()

        Dim strOpCd As String = "IN_CLSTRX_PRLNCOST_GET"
        Dim strParam As String
        Dim dsCost As New DataSet()
        Dim intErrNo As Integer
        Dim strItemCode As String = lstItem.SelectedItem.Value

        If InStr(strItemCode, ITEM_PART_SEPERATOR) <> 0 Then
            strItemCode = Trim(Mid(strItemCode, 1, InStr(strItemCode, ITEM_PART_SEPERATOR) - 1))
        End If

        strParam = lstPR.SelectedItem.Text.Trim & "|" & "AND PRLN.PRLnID = '" & strItemCode & "'|"

        Try
            intErrNo = objINtx.mtdGetPRLnList(strOpCd, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strAccMonth, _
                                              strAccYear, _
                                              strParam, _
                                              dsCost)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_PR_COST_GET&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        If dsCost.Tables(0).Rows.Count > 0 Then
            TxtItemCode.Text = dsCost.Tables(0).Rows(0).Item("ItemCode")
            txtCost.Text = objGlobal.DisplayForEditCurrencyFormat(dsCost.Tables(0).Rows(0).Item("Cost"))
            If txtQty.Text <> "" Then txtAmount.Text = objGlobal.DisplayForEditCurrencyFormat(txtCost.Text * txtQty.Text)
        End If

    End Sub

    Function BindAccCodeDoubleEntry(ByVal pLocCode As String) As String

        Dim strOpCdPR_List_GET As String = "IN_CLSTRX_SR_DA_DOUBLEENTRY_SEARCH"
        Dim strparam As String
        Dim strParamValue As String
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objItemDs As New DataSet
        Dim nCoA As String

        strparam = "LOCCODE"
        strParamValue = pLocCode

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCdPR_List_GET, _
                              strparam, _
                              strParamValue, _
                              objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_PR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try
        nCoA = vbNullString
        If objItemDs.Tables(0).Rows.Count > 0 Then
            nCoA = objItemDs.Tables(0).Rows(intCnt).Item("AccCode")
        End If

        Return nCoA
    End Function

    Sub GetDAID(ByVal pv_strPRId As String)
        Dim strOpCdPR_List_GET As String = "IN_CLSTRX_SR_DADOCID_SEARCH"
        Dim drinsert As DataRow
        Dim intCnt As Integer
        Dim strparam As String
        Dim strParamValue As String
        Dim objItemDs As New DataSet


        strparam = "DOCID"
        strParamValue = pv_strPRId

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCdPR_List_GET, _
                              strparam, _
                              strParamValue, _
                              objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_PR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objItemDs.Tables(0).Rows.Count - 1
            objItemDs.Tables(0).Rows(intCnt).Item("DocID") = objItemDs.Tables(0).Rows(intCnt).Item("DocID").Trim()
        Next intCnt

        drinsert = objItemDs.Tables(0).NewRow()
        lstPR.DataSource = objItemDs.Tables(0)
        lstPR.DataValueField = "DocId"
        lstPR.DataTextField = "DocId"
        lstPR.DataBind()

        If Not objItemDs Is Nothing Then
            objItemDs = Nothing
        End If
    End Sub

    Sub CallCheckVehicleUse(ByVal sender As Object, ByVal e As System.EventArgs)
        CheckVehicleUse()
    End Sub

    Sub CheckVehicleUse()
        Dim intAccType As Integer
        Dim intAccPurpose As Integer

        Dim strAcc As String = Request.Form("txtAccCode")
        Dim strBlk As String = Request.Form("lstBlock")
        Dim strVeh As String = Request.Form("lstVehCode")
        Dim strVehExp As String = Request.Form("lstVehExp")

        GetAccountDetails(strAcc, intAccType, intAccPurpose)

        If intAccType = objGLset.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLset.EnumAccountPurpose.NonVehicle
                    BindBlockDropList(strAcc, strBlk)
                    BindVehicleCodeDropList("")
                    BindVehicleExpDropList(True)
                Case objGLset.EnumAccountPurpose.VehicleDistribution
                    BindBlockDropList("")
                    BindVehicleCodeDropList(strAcc, strVeh)
                    BindVehicleExpDropList(False, strVehExp)
                Case objGLset.EnumAccountPurpose.Others
                    BindBlockDropList(strAcc, strBlk)
                    BindVehicleCodeDropList("%", strVeh)
                    BindVehicleExpDropList(False, strVehExp)
            End Select
        Else
            BindBlockDropList(strAcc, strBlk)
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        End If

        tblAdd.Visible = True

    End Sub

    Sub GetItemDetails(ByVal pv_strItemCode As String, ByRef pr_strItemType As Integer)

        Dim _objItemDs As New DataSet()
        Dim strParam As String = pv_strItemCode & "||" & Trim(strLocation)
        Dim intErrNo As Integer

        Try
            intErrNo = objINstp.mtdGetMasterDetail(strOpCdItem_Details_GET, _
                                                strParam, _
                                                _objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_GET_ITEM_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        If _objItemDs.Tables(0).Rows.Count = 1 Then
            TxtItemName.Text = _objItemDs.Tables(0).Rows(0).Item("Description")
            pr_strItemType = CInt(_objItemDs.Tables(0).Rows(0).Item("ItemType"))
        End If
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer)

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = CInt(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
        End If
    End Sub

    Function CheckRequiredField() As Boolean
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim strAcc As String = Request.Form("txtAccCode")
        Dim strPreBlk As String = Request.Form("lstPreBlock")
        Dim strBlk As String = Request.Form("lstBlock")
        Dim strVeh As String = Request.Form("lstVehCode")
        Dim strVehExp As String = Request.Form("lstVehExp")
        Dim strItem As String = ""

        If Trim(lstItem.SelectedItem.Value) = "" Then
            strItem = Request.Form("txtItemCode")
        Else
            strItem = Request.Form("lstItem")
        End If

        GetAccountDetails(strAcc, intAccType, intAccPurpose)

        If strItem.Trim = "" Then

            lblItemCodeErr.Visible = True
            Return True
        Else

            If InStr(strItem, ITEM_PART_SEPERATOR) <> 0 Then
                strItem = Trim(Mid(strItem, 1, InStr(strItem, ITEM_PART_SEPERATOR) - 1))
            End If

        End If


        If intAccType = objGLset.EnumAccountType.BalanceSheet Then
            If lstChargeLevel.SelectedIndex = 0 Then
                If lstPreBlock.Items.Count > 1 And strPreBlk = "" Then
                    lblPreBlockErr.Visible = True
                    Return True
                Else
                    Return False
                End If
            Else
                If lstBlock.Items.Count > 1 And strBlk = "" Then
                    lblBlockErr.Visible = True
                    Return True
                Else
                    Return False
                End If
            End If
        Else

            If strAcc = "" Then
                lblAccCodeErr.Visible = True
                Return True
            ElseIf strPreBlk = "" And lstChargeLevel.SelectedIndex = 0 And (Not intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution) Then
                lblPreBlockErr.Visible = True
                Return True
            ElseIf strBlk = "" And lstChargeLevel.SelectedIndex = 1 And (Not intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution) Then
                lblBlockErr.Visible = True
                Return True
            ElseIf strVeh = "" And intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution Then
                lblVehCodeErr.Visible = True
                Return True
            ElseIf strVehExp = "" And intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution Then
                lblVehExpCodeErr.Visible = True
                Return True
            ElseIf strVeh <> "" And strVehExp = "" And intAccPurpose = objGLset.EnumAccountPurpose.Others Then
                lblVehExpCodeErr.Visible = True
                Return True
            ElseIf strVeh = "" And strVehExp <> "" And intAccPurpose = objGLset.EnumAccountPurpose.Others Then
                lblVehCodeErr.Visible = True
                Return True
            Else
                Return False
            End If
        End If

    End Function

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
                strParam = pv_strAccCode & "|" & ddlChargeTo.SelectedItem.Value.Trim() & "|" & objGLset.EnumBlockStatus.Active
            Else
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & ddlChargeTo.SelectedItem.Value.Trim() & "|" & objGLset.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLset.mtdGetAccountBlock(strOpCdBlockList_Get, _
                                                     strParam, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_BLOCK_BIND&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        'For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
        '    If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = pv_strBlkCode.Trim Then
        '        intSelectedIndex = intCnt + 1
        '        Exit For
        '    End If
        'Next

        If dsForDropDown.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("_Description") = lblSelect.Text & BlockTag & lblCode.Text

        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)
        lstBlock.DataSource = dsForDropDown.Tables(0)
        lstBlock.DataValueField = "BlkCode"
        lstBlock.DataTextField = "_Description"
        lstBlock.DataBind()
        lstBlock.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If

        strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_PREBLOCK_GET"
        intSelectedIndex = 0
        Try
            strParam = pv_strAccCode & "|" & ddlChargeTo.SelectedItem.Value.Trim() & "|" & objGLset.EnumBlockStatus.Active
            intErrNo = objGLset.mtdGetAccountBlock(strOpCdBlockList_Get, _
                                                     strParam, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        'For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
        '    dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
        '    dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode").Trim() & " (" & dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
        '    If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = pv_strBlkCode.Trim Then
        '        intSelectedIndex = intCnt + 1
        '    End If
        'Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & PreBlockTag & lblCode.Text

        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)
        lstPreBlock.DataSource = dsForDropDown.Tables(0)
        lstPreBlock.DataValueField = "BlkCode"
        lstPreBlock.DataTextField = "Description"
        lstPreBlock.DataBind()
        lstPreBlock.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindVehicleCodeDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strVehCode As String = "")

        Dim dsForDropDown As DataSet
        Dim strOpCd As String
        Dim drinsert As DataRow
        Dim strParam As New StringBuilder()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0
        strParam.Append("|AccCode = '" & pv_strAccCode & "' AND LocCode = '" & ddlChargeTo.SelectedItem.Value.Trim() & "' AND Status = '" & objGLset.EnumVehicleStatus.Active & "'")

        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"
            intErrNo = objGLset.mtdGetMasterList(strOpCd, _
                                                   strParam.ToString, _
                                                   objGLset.EnumGLMasterType.Vehicle, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_VEHCODE_BIND&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = dsForDropDown.Tables(0).Rows(intCnt).Item(0).Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = dsForDropDown.Tables(0).Rows(intCnt).Item(0).Trim() & " ( " & _
                                                           dsForDropDown.Tables(0).Rows(intCnt).Item(1).Trim() & " )"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("VehCode") = pv_strVehCode.Trim Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = lblSelect.Text & VehicleTag & lblCode.Text
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_VEHEXPENSE_BIND&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = dsForDropDown.Tables(0).Rows(intCnt).Item(0).Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = dsForDropDown.Tables(0).Rows(intCnt).Item(0).Trim() & " ( " & _
                                                           dsForDropDown.Tables(0).Rows(intCnt).Item(1).Trim() & " )"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("VehExpenseCode") = pv_strVehExpCode.Trim Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = lblSelect.Text & VehExpCodeTag & lblCode.Text
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

    Sub RebindItemList(ByVal sender As Object, ByVal e As System.EventArgs)
        TxtItemCode.Visible = False
        TxtItemName.Visible = False
        lstItem.Visible = True
        BindItemCodeList()

        Select Case lstPR.SelectedItem.Value.Substring(lstPR.SelectedItem.Value.Length - 1)
            Case objINtx.EnumPurReqDocType.StockPR
                RowChargeTo.Visible = False
                RowAcc.Visible = False
                RowBlk.Visible = False
                RowVeh.Visible = False
                RowVehExp.Visible = False
                RowChargeLevel.Visible = False
                RowPreBlk.Visible = False
            Case objINtx.EnumPurReqDocType.DirectChargePR
                RowChargeTo.Visible = Session("SS_INTER_ESTATE_CHARGING")
                RowAcc.Visible = True
                RowBlk.Visible = True
                RowVeh.Visible = True
                RowVehExp.Visible = True
                RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
                ToggleChargeLevel()
        End Select
    End Sub

    Function GetOngkosAngkut(ByVal pDAId As String, ByVal pItemCode As String) As Double
        Dim strParam As String
        Dim strParamValue As String
        Dim objItemDs As New DataSet
        Dim nNetTransportFee As Double = 0

        Dim strOppCd_GET As String = "PU_CLSTRX_SR_DALIST_ONGKOSANGKUT_GET"

        strParam = "DPVAID|ITEMCODE"
        strParamValue = pDAId & "|" & pItemCode


        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOppCd_GET, _
                                    strParam, _
                                    strParamValue, _
                                    objItemDs)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_GET_VehActCode&errmesg=" & Exp.ToString() & "&redirect=GL/Setup/GL_Setup_VehicleSubGrpCode.aspx")
        End Try

        If objItemDs.Tables(0).Rows.Count > 0 Then
            nNetTransportFee = Trim(objItemDs.Tables(0).Rows(0).Item("NetAMTTransportFee"))
        Else
            nNetTransportFee = 0
        End If

        Return nNetTransportFee
    End Function

    Sub Set_Focus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtQty.Text = ""
        txtCost.Text = ""
        txtAmount.Text = ""
        txtQty.Enabled = True
        txtCost.Enabled = True
        txtAmount.Enabled = False 'true  
        lstPR.SelectedItem.Value = "NoDA" & " "
        TxtItemCode.Text = ""
        TxtItemName.Text = ""
        lstItem.Items.Clear()
        btnAddAllItem.Visible = False
        'BindItemCodeList()
        setFocus(txtRefNo)

        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
            BindDAList("")
            btnAddAllItem.Visible = True

            RowChargeTo.Visible = Session("SS_INTER_ESTATE_CHARGING")
            ddlChargeTo.Enabled = False
            RowAcc.Visible = True
            RowBlk.Visible = True
            RowVeh.Visible = True
            RowVehExp.Visible = True

            TxtItemCode.Text = ""
            TxtItemName.Text = ""

            TxtItemCode.Visible = False
            TxtItemName.Visible = False
            FindIN_Txt.Visible = False
            lstItem.Visible = True

            RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")

            ToggleChargeLevel()

            chkCentralized.Visible = False

        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then
            TxtItemCode.Text = ""
            TxtItemName.Text = ""
            TxtItemCode.Visible = False
            TxtItemName.Visible = False
            lstItem.Visible = True
            FindIN_Txt.Visible = False

            BindSRAList("")


            RowAcc.Visible = True
            RowBlk.Visible = True
            RowVeh.Visible = True
            RowVehExp.Visible = True

            RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")

            ToggleChargeLevel()

        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
            TxtItemCode.Text = ""
            TxtItemName.Text = ""
            TxtItemCode.Visible = False
            TxtItemName.Visible = False
            lstItem.Visible = True
            FindIN_Txt.Visible = False

            BindPRList("")

            chkCentralized.Visible = False
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
            BindSTList("")
            RowChargeTo.Visible = Session("SS_INTER_ESTATE_CHARGING")
            ddlChargeTo.Enabled = False
            RowAcc.Visible = True
            RowBlk.Visible = True
            RowVeh.Visible = True
            RowVehExp.Visible = True

            TxtItemCode.Text = ""
            TxtItemName.Text = ""

            TxtItemCode.Visible = False
            TxtItemName.Visible = False
            FindIN_Txt.Visible = False

            lstItem.Visible = True
            RowAMt.Visible = True
            RowCost.Visible = True
            RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
            ToggleChargeLevel()
            lstPR.Visible = True
            chkCentralized.Checked = True
            chkCentralized.Visible = True
        End If
    End Sub

    Sub ItemTypeCheck()
        Dim strItemType As Integer
        Dim strItemCode As String = Request.Form("txtItemCode")

        If InStr(strItemCode, ITEM_PART_SEPERATOR) <> 0 Then
            strItemCode = Trim(Mid(strItemCode, 1, InStr(strItemCode, ITEM_PART_SEPERATOR) - 1))
        End If

        GetItemDetails(strItemCode, strItemType)

        Select Case strItemType
            Case objINstp.EnumInventoryItemType.DirectCharge
                RowChargeTo.Visible = Session("SS_INTER_ESTATE_CHARGING")
                ddlChargeTo.Enabled = False
                RowAcc.Visible = True
                RowBlk.Visible = True
                RowVeh.Visible = True
                RowVehExp.Visible = True
                RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
                ToggleChargeLevel()
            Case Else
                RowChargeTo.Visible = False
                RowAcc.Visible = False
                RowBlk.Visible = False
                RowVeh.Visible = False
                RowVehExp.Visible = False
                RowChargeLevel.Visible = False
                RowPreBlk.Visible = False
                ddlChargeTo.Enabled = True
        End Select

        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
            RowChargeTo.Visible = Session("SS_INTER_ESTATE_CHARGING")
            ddlChargeTo.Enabled = False
            RowAcc.Visible = True
            RowBlk.Visible = True
            RowVeh.Visible = True
            RowVehExp.Visible = True
            RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
            ToggleChargeLevel()
        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Or lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Or lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
            RowAcc.Visible = True
            RowBlk.Visible = True
            RowVeh.Visible = True
            RowVehExp.Visible = True
            RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
            ToggleChargeLevel()
        End If
    End Sub

    Sub BindItemCodeList()
        Dim strItemCode As String = Request.Form("lstItem")
        Dim dsItemCodeDropList As DataSet
        Dim strOpCdItem_List_GET As String
        Dim strParam As String
        Dim strParamValue As String
        Dim strItemType As String
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer
        Dim arrPartNo As Array        

        If lstPR.SelectedIndex = 0 Then
            lstItem.Items.Clear()
            Exit Sub
        End If

        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then
            strOpCdItem_List_GET = "IN_CLSTRX_SR_ST_LINE_GET" '"IN_CLSTRX_ITEMPART_ITEM_GET"
        Else
            If lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim = "NoDA" Then
                strOpCdItem_List_GET = "IN_CLSTRX_ITEMPART_ITEM_GET"
            Else
                If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then
                    strOpCdItem_List_GET = "IN_CLSTRX_SRA_LINE_ITEM_GET"
                ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
                    strOpCdItem_List_GET = "IN_CLSTRX_DA_LINE_GET"
                ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
                    strOpCdItem_List_GET = "IN_CLSTRX_PR_LINE_ITEM_GET"
                End If
            End If

            Select Case lstPR.SelectedItem.Value.Substring(lstPR.SelectedItem.Value.Length - 1)
                Case objINtx.EnumPurReqDocType.StockPR
                    strItemType = objINstp.EnumInventoryItemType.Stock & "','" & objINstp.EnumInventoryItemType.WorkshopItem & "','" & objINstp.EnumInventoryItemType.NurseryItem
                Case objINtx.EnumPurReqDocType.DirectChargePR
                    strItemType = objINstp.EnumInventoryItemType.DirectCharge
                Case objINtx.EnumPurReqDocType.WorkshopPR
                    strItemType = objINstp.EnumInventoryItemType.WorkshopItem & "','" & objINstp.EnumInventoryItemType.Stock
                Case objINtx.EnumPurReqDocType.NurseryPR
                    strItemType = objINstp.EnumInventoryItemType.NurseryItem
                Case Else
                    strItemType = objINstp.EnumInventoryItemType.Stock & "','" & _
                                    objINstp.EnumInventoryItemType.DirectCharge & "','" & _
                                    objINstp.EnumInventoryItemType.WorkshopItem & "','" & _
                                    objINstp.EnumInventoryItemType.NurseryItem & "','" & _
                                    objINstp.EnumInventoryItemType.FixedAssetItem & "','" & _
									objINstp.EnumInventoryItemType.NurseryItem
            End Select
        End If

        If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
            ' strParam = strItemType & "|" & objINstp.EnumStockItemStatus.Active & _
            '             "|" & lblStckTxID.Text & "|" & "itm.ItemCode" & "|" & lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim & "|DA"

            strParam = "SEARCHSTR|SORTEXP"
            strParamValue = "AND ItemType in ('1','4','7') AND itm.Status = '1' AND ITM.LocCode = '" & strLocation & "' AND LN.DispAdvId ='" & lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim & "' " & _
                            "GROUP BY ITMP.PartNo,ln.DispAdvLnID, ITM.ItemCode, ITM.Description, ITM.ItemType, GR.StockUOM, ITM.UOMCODE, ITM.PURCHASEUOM, LN.Cost,  " & _
                            "GR.DispQty, GR.ReceiveUOM, PO.Cost, X.RPHID, X.PBBKB, PO.Transporter, GRH.SupplierCode, PO.NetAmtTransportFee, PO.QtyOrder   " & _
                            "Having rtrim(LN.DispAdvLnID)+rtrim(ITM.ItemCode)+rtrim(SUM(LN.QtyDisp))+rtrim(PO.Cost) Not In  " & _
                            "(SELECT rtrim(ln.DociDLN) + rtrim(ln.ItemCode)+rtrim(sum(ln.Qty))+rtrim(ln.Cost)  " & _
                            "FROM IN_StockReceive tx, IN_StockReceiveLn ln WHERE tx.StockReceiveID = ln.StockReceiveID AND DocID ='" & lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim & "' And  " & _
                            "Status not in ('3','4') Group By DocID,ln.DociDLN, ItemCode, Qty, Cost)" & "|" & " itm.ItemCode "


            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCdItem_List_GET, _
                                        strParam, _
                                        strParamValue, _
                                        dsItemCodeDropList)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCV_BIND_ITEMCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
            End Try

        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.PurchaseRequisition Then
            strParam = strItemType & "|" & objINstp.EnumStockItemStatus.Active & _
                    "|" & lblStckTxID.Text & "|" & "itm.ItemCode" & "|" & lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim & "|PR"

            Try
                intErrNo = objINstp.mtdGetFilteredItemListDA(strOpCdItem_List_GET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        objINtx.EnumInventoryTransactionType.StockReceive, _
                                                        dsItemCodeDropList)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCV_BIND_ITEMCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
            End Try

        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockReturnAdvice Then          
            strParam = "LOCCODE|DOCID|STRSEARCH"
            strParamValue = Trim(strLocation) & "|" & lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim & "|" & " And LN.ItemCode NOT IN (SELECT ItemCode From IN_STOCKRECEIVELN Where DocID='" & lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim & "')"

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCdItem_List_GET, _
                                                        strParam, _
                                                        strParamValue, _
                                                        dsItemCodeDropList)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCV_BIND_ITEMCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
            End Try

        ElseIf lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.StockTransfer Then

            strParam = "LOCCODE|DOCID|STRSEARCH"
            strParamValue = Trim(strLocation) & "|" & lstPR.SelectedItem.Value & "|" & ""

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCdItem_List_GET, _
                                                        strParam, _
                                                        strParamValue, _
                                                        dsItemCodeDropList)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
            End Try

            For intCnt = 0 To dsItemCodeDropList.Tables(0).Rows.Count - 1
                dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = RTrim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("DocLNID")) & _
                                ", " & RTrim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")) & _
                                ", " & RTrim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description")) & _
                                ",  Qty: " & lCDbl(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Qty")) & _
                                ",  UOM : " & dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PurchaseUOM")
                If dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode").Trim() = Trim(strItemCode) Then
                    strUOMCode = Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode"))
                    strPurchaseUOM = Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PurchaseUOM"))
                    intSelectedIndex = intCnt + 1
                End If

                dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")
            Next

            Dim drinsert As DataRow
            drinsert = dsItemCodeDropList.Tables(0).NewRow()
            drinsert("ItemCode") = ""
            drinsert("Description") = "Select Item Code"
            dsItemCodeDropList.Tables(0).Rows.InsertAt(drinsert, 0)

            lstItem.DataSource = dsItemCodeDropList.Tables(0)
            lstItem.DataValueField = "DocLNID"
            lstItem.DataTextField = "Description"
            lstItem.DataBind()

            lstItem.SelectedIndex = intSelectedIndex

            DisableItemTable()

        End If

        If lstRecDoc.SelectedItem.Value <> objINtx.EnumStockReceiveDocType.StockTransfer Then
            If lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim <> "NoDA" Then
                For intCnt = 0 To dsItemCodeDropList.Tables(0).Rows.Count - 1

                    If dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then

                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") & " @ " & _
                                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo") & " ( " & _
                                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
                                                                                        "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("AverageCost"), 0) & ", " & _
                                                                                        objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
                                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode") & " ( " & _
                                                                                        "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Cost"), 0) & ", " & _
                                                                                        objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyPurchase"), 5) & ", " & _
                                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PurchaseUOM") & " ) "

                        If strItemCode <> "" Then
                            arrPartNo = Split(strItemCode, " @ ")
                            If arrPartNo.GetUpperBound(0) = 1 Then
                                If dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo").trim() = arrPartNo(1) Then
                                    intSelectedIndex = intCnt + 1
                                End If
                            ElseIf arrPartNo.GetUpperBound(0) = 0 Then
                                If lstPR.SelectedItem.Value = "" Then
                                    intSelectedIndex = intCnt + 1
                                Else
                                    If arrPartNo(0) = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode").trim() Then
                                        intSelectedIndex = intCnt + 1
                                    End If
                                End If
                            End If
                        End If
                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") & _
                                                                                     ITEM_PART_SEPERATOR & dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo").Trim()
                    Else

                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("DocLNID") & " , " & dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") & " ( " & _
                                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
                                                                                        "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("AverageCost"), 0) & ", " & _
                                                                                        objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
                                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode") & " ( " & _
                                                                                        "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Cost"), 0) & ", " & _
                                                                                        objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyPurchase"), 5) & ", " & _
                                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PurchaseUOM") & " ) "


                        If dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode").trim() = strItemCode Then
                            strUOMCode = Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode"))
                            strPurchaseUOM = Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PurchaseUOM"))
                            If lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim <> "NoDA" Then
                                If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
                                    lblPBBKB.Text = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PBBKB")
                                End If
                            End If
                            intSelectedIndex = intCnt + 1
                        End If

                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")

                    End If
                Next intCnt
            End If

            Dim drinsert As DataRow
            drinsert = dsItemCodeDropList.Tables(0).NewRow()
            drinsert("ItemCode") = ""
            drinsert("Description") = "Select Item Code"
            dsItemCodeDropList.Tables(0).Rows.InsertAt(drinsert, 0)

            lstItem.DataSource = dsItemCodeDropList.Tables(0)
            lstItem.DataValueField = "DocLnID"
            lstItem.DataTextField = "Description"
            lstItem.DataBind()

            lstItem.SelectedIndex = intSelectedIndex

            DisableItemTable()
        End If

        If Not dsItemCodeDropList Is Nothing Then
            dsItemCodeDropList = Nothing
        End If

    End Sub

    Sub BindLocDropList()
        Dim strOpCdLocList_GET As String = "ADMIN_CLSLOC_SYSLOC_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim intCnt As Integer
        Dim strParam As String
        Dim drinsert As DataRow
        Try
            strParam = "And SY.CompCode = '" & strCompany & "' AND LO.Status = " & objAdminLoc.EnumLocStatus.Active & " AND Not SY.LocCode = '" & strLocation & "'|"

            intErrNo = objAdminLoc.mtdGetCompLocList(strOpCdLocList_GET, strCompany, strLocation, strUserId, dsForDropDown, strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDET_FROMLOCATION&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = dsForDropDown.Tables(0).Rows(intCnt).Item(0).Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = dsForDropDown.Tables(0).Rows(intCnt).Item(0).Trim() & " ( " & _
                                                           dsForDropDown.Tables(0).Rows(intCnt).Item(1).Trim() & " )"
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = lblSelect.Text & strLocationTag
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)


        lstFromLoc.DataSource = dsForDropDown.Tables(0)
        lstFromLoc.DataValueField = "LocCode"
        lstFromLoc.DataTextField = "LocDesc"
        lstFromLoc.DataBind()
        lstFromLoc.Items.Add(New ListItem("OTHER COMPANY", "OTH"))

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If


        'If lstRecDoc.SelectedItem.Value = objINtx.EnumStockReceiveDocType.DispatchAdvice Then
        '    nCOAdefault = BindAccCodeDoubleEntry(strLocation)
        '    BindAccCodeDropList(nCOAdefault)
        'End If

    End Sub

    Sub BindPRList(ByVal pv_strPRId As String)
        Dim strOpCdPR_List_GET As String = "IN_CLSTRX_SR_PR_GET"
        Dim dsPRDropList As DataSet
        Dim drinsert As DataRow
        Dim intCnt As Integer
        Dim strparam As String


        strparam = "|" & objINtx.EnumPurReqDocType.StockPR & "','" & objINtx.EnumPurReqDocType.WorkshopPR & "','" & objINtx.EnumPurReqDocType.DirectChargePR & _
                   "|" & objINtx.EnumPurReqStatus.Confirmed & "|" & strLocation

        Try
            intErrNo = objPU.mtdGetPR_New(strOpCdPR_List_GET, strAccMonth, strAccYear, strparam, dsPRDropList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_PR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To dsPRDropList.Tables(0).Rows.Count - 1
            dsPRDropList.Tables(0).Rows(intCnt).Item("PRId") = dsPRDropList.Tables(0).Rows(intCnt).Item("PRId").Trim()
            dsPRDropList.Tables(0).Rows(intCnt).Item("Prtype") = dsPRDropList.Tables(0).Rows(intCnt).Item("PRId").Trim() & dsPRDropList.Tables(0).Rows(intCnt).Item("PRType").Trim()
        Next intCnt

        drinsert = dsPRDropList.Tables(0).NewRow()
        drinsert("Prtype") = "NoPR" & " "
        drinsert("PRID") = "Select purchase requisition or leave blank"

        dsPRDropList.Tables(0).Rows.InsertAt(drinsert, 0)
        lstPR.DataSource = dsPRDropList.Tables(0)
        lstPR.DataValueField = "Prtype"
        lstPR.DataTextField = "PRId"
        lstPR.DataBind()

        If Not dsPRDropList Is Nothing Then
            dsPRDropList = Nothing
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

    Sub DisplayPRCost(ByVal sender As Object, ByVal e As System.EventArgs)

        If Trim(lstRecDoc.SelectedItem.Value) = (objINtx.EnumStockReceiveDocType.DispatchAdvice) Then
            txtQty.Enabled = True
            txtCost.Enabled = False
            txtAmount.Enabled = False
            GetDataDA()
            TxtNetTransPortFee.Text = objGlobal.DisplayForEditCurrencyFormat(GetOngkosAngkut(lstPR.SelectedItem.Text, TxtItemCode.Text))
        ElseIf Trim(lstRecDoc.SelectedItem.Value) = (objINtx.EnumStockReceiveDocType.StockReturnAdvice) Then
            txtQty.Enabled = True
            txtCost.Enabled = False
            txtAmount.Enabled = False
            GetDataSRA()
        ElseIf Trim(lstRecDoc.SelectedItem.Value) = (objINtx.EnumStockReceiveDocType.StockTransfer) Then
            txtQty.Enabled = True
            txtCost.Enabled = True
            txtAmount.Enabled = False
            GetDataST()
        Else
            txtAmount.Enabled = False
            GetPRCost()
        End If

        'If lstRecDoc.SelectedIndex = 0 Then
        '    CheckType(lstItem.Text)
        'Else
        '    CheckType(TxtItemCode.Text)
        'End If

    End Sub

    Function CheckLocBillParty() As Boolean
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strSearch As String
        Dim objBPLocDs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim intCnt As Integer
        Dim strLocCode As String

        strParam = lblStckTxID.Text.Trim
        Try
            intErrNo = objINtx.mtdGetStockTransactDetails(strOpCdStckTxLine_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          objStkRcvLn)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCVDETLN_GET&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        For intCnt = 0 To objStkRcvLn.Tables(0).Rows.Count - 1
            strLocCode = Trim(objStkRcvLn.Tables(0).Rows(intCnt).Item("ChargeLocCode"))

            If Not (strLocCode = "" Or strLocCode = Trim(strLocation)) Then
                strSearch = " AND BP.Status = '" & objGLset.EnumBillPartyStatus.Active & "'" & _
                            " AND BP.InterLocCode = '" & strLocCode & "'"

                Try
                    intErrNo = objGLset.mtdGetBPInterLoc(strOpCd_Get, strSearch, objBPLocDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_SETUP_BILLPARTY_GET&errmesg=" & lblErrMessage.Text & "&redirect=BI/setup/BI_setup_BillPartyDet.aspx")
                End Try

                If objBPLocDs.Tables(0).Rows.Count <= 0 Then
                    lblLocCodeErr.Text = strLocCode
                    Return False
                End If
            End If
        Next intCnt

        Return True
    End Function

    Sub CheckType(ByVal pv_ItemCode As String)
        Dim strOppCd_GET As String = "IN_CLSSETUP_INVMASTER_DETAILS_GET"
        Dim objDataSet As New DataSet
        Dim strParam As String
        strParam = Trim(pv_ItemCode) & "|" & objINstp.EnumInventoryItemType.Stock & "|" & Trim(strLocation)
        Try
            intErrNo = objINstp.mtdGetMasterDetail(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKITEM_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_StockMaster.aspx")
        End Try
        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblFertInd.Text = Trim(objDataSet.Tables(0).Rows(0).Item("FertInd"))
        End If
    End Sub

    Protected Sub setFocus(ByVal ctrl As System.Web.UI.Control)
        Dim s As String = ("<SCRIPT language='javascript'>document.getElementById('" _
                    + (ctrl.ID + "').focus() </SCRIPT>"))
        Me.RegisterStartupScript("focus", s)
    End Sub

    Sub BindDAList(ByVal pv_strDispAdvId As String)
        Dim strOpCdPR_List_GET As String = "PU_CLSTRX_SR_DA_GET"
        Dim dsPRDropList As DataSet
        Dim drinsert As DataRow
        Dim intCnt As Integer
        Dim strparam As String
        Dim objDADs As New Object()
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCd_DAGET As String = "PU_CLSTRX_SR_DALIST_GET"

        ''strParam  = pv_strDispAdvId & "|||||" & objPU.EnumDAStatus.Confirmed & "||A.DispAdvId||||"
        '      strParam  = pv_strDispAdvId & "||||" & strLocation & "|" & objPU.EnumDAStatus.Confirmed & "||A.DispAdvId||||"

        '      Try
        '          intErrNo = objPU.mtdGetDA(strOpCdPR_List_GET, strParam, dsPRDropList)
        '      Catch Exp As System.Exception
        '          Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        '      End Try

        '      For intCnt = 0 To dsPRDropList.Tables(0).Rows.Count - 1
        '          dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvId") = dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvId").Trim()
        '          dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvType") = dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvId").Trim() & dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvType").Trim()
        '      Next intCnt

        strParamName = "LOCCODE"
        strParamValue = Trim(strLocation)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_DAGET, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsPRDropList)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try

        For intCnt = 0 To dsPRDropList.Tables(0).Rows.Count - 1
            dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvId") = dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvId").Trim()
            dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvType") = dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvId").Trim() & dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvType").Trim()
        Next intCnt

        drinsert = dsPRDropList.Tables(0).NewRow()
        drinsert("DispAdvType") = "NoDA" & " "
        drinsert("DispAdvId") = "Select Dispatch ID or leave blank"

        dsPRDropList.Tables(0).Rows.InsertAt(drinsert, 0)
        lstPR.DataSource = dsPRDropList.Tables(0)
        lstPR.DataValueField = "DispAdvType"
        lstPR.DataTextField = "DispAdvId"
        lstPR.DataBind()

        If Not dsPRDropList Is Nothing Then
            dsPRDropList = Nothing
        End If


    End Sub

    Sub BindSTList(ByVal pv_strDispAdvId As String)
        Dim dsPRDropList As DataSet
        Dim drinsert As DataRow
        Dim intCnt As Integer
        Dim strparam As String
        Dim objDADs As New Object()
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCd_DAGET As String = "PU_CLSTRX_SR_ST_GET"

        strParamName = "LOCCODE"
        strParamValue = Trim(strLocation)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_DAGET, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsPRDropList)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try

        'For intCnt = 0 To dsPRDropList.Tables(0).Rows.Count - 1
        '    dsPRDropList.Tables(0).Rows(intCnt).Item("StockTransferID") = dsPRDropList.Tables(0).Rows(intCnt).Item("StockTransferID").Trim()
        '    dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvType") = dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvId").Trim() & dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvType").Trim()
        'Next intCnt

        drinsert = dsPRDropList.Tables(0).NewRow()
        drinsert("StockTransferID") = "NoDA" & " "
        drinsert("StockTransferID") = "Select Transfer ID or leave blank"

        dsPRDropList.Tables(0).Rows.InsertAt(drinsert, 0)
        lstPR.DataSource = dsPRDropList.Tables(0)
        lstPR.DataValueField = "StockTransferID"
        lstPR.DataTextField = "StockTransferID"
        lstPR.DataBind()

        If Not dsPRDropList Is Nothing Then
            dsPRDropList = Nothing
        End If

    End Sub

    Sub GetDataDA()
        Dim strParam As String
        Dim strOpCd_Get As String = "IN_CLSTRX_DA_LINE_GET_DETAIL"
        Dim intErrNo As Integer
        Dim objDALnDs As New Object()

        Dim strParamName As String
        Dim strParamValue As String
        strParamName = "STRSEARCH1"
        strParamValue = "AND A.DispAdvId LIKE  '" & Trim(lstPR.SelectedItem.Text) & "%'  AND A.DispAdvLnID  = '" & Trim(lstItem.SelectedItem.Value) & "'"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get,
                                                    strParamName,
                                                    strParamValue,
                                                    objDALnDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try

        If objDALnDs.Tables(0).Rows.Count > 0 Then
            
            TxtItemCode.Text = objDALnDs.Tables(0).Rows(0).Item("ItemCode")
            txtQty.Text = objDALnDs.Tables(0).Rows(0).Item("QtyDisp") - objDALnDs.Tables(0).Rows(0).Item("QtyRcv")
            txtCost.Text = objDALnDs.Tables(0).Rows(0).Item("Cost")
            txtAmount.Text = objDALnDs.Tables(0).Rows(0).Item("Amount")
            lblPBBKB1.Value = objDALnDs.Tables(0).Rows(0).Item("PBBKB")


        End If
    End Sub

    Sub GetDataST()
        Dim strOpCd_Get As String = "IN_CLSTRX_SR_ST_LINE_ITEM_GET"
        Dim intErrNo As Integer
        Dim objDALnDs As New Object()

        Dim strParamName As String
        Dim strParamValue As String
        strParamName = "LOCCODE|DOCID|STRSEARCH"
        strParamValue = Trim(strLocation) & "|" & lstPR.SelectedItem.Value & "|" & "AND ln.DocLNID='" & lstItem.SelectedItem.Value & "'"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objDALnDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try

        If objDALnDs.Tables(0).Rows.Count > 0 Then
            TxtItemCode.Text = objDALnDs.Tables(0).Rows(0).Item("ItemCode")
            txtQty.Text = objDALnDs.Tables(0).Rows(0).Item("Qty")
            txtCost.Text = objDALnDs.Tables(0).Rows(0).Item("Cost")
            txtAmount.Text = objDALnDs.Tables(0).Rows(0).Item("Amount")
            lstFromLoc.SelectedItem.Value = Trim(objDALnDs.Tables(0).Rows(0).Item("LocCode"))
            lblPBBKB1.Value = 0
        End If
    End Sub

    Sub BindSRAList(ByVal pv_strSRAId As String)
        Dim strOpCdSRA_List_GET As String = "IN_CLSTRX_SRA_GET"
        Dim dsPRDropList As DataSet
        Dim drinsert As DataRow
        Dim intCnt As Integer
        Dim strparam As String
        Dim objSRADs As New Object()
        Dim intErrNo As Integer


        strparam = pv_strSRAId & "||" & objINtx.EnumStockRetAdvStatus.Confirmed & "||" & "A.ItemRetAdvId" & "|||"
        Try
            intErrNo = objINtx.mtdGetSRA(strOpCdSRA_List_GET, strLocation, strparam, dsPRDropList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_GET_SRA&errmesg=" & lblErrMessage.Text & "&redirect=in/trx/IN_trx_StockReceive_list.aspx")
        End Try

        For intCnt = 0 To dsPRDropList.Tables(0).Rows.Count - 1
            dsPRDropList.Tables(0).Rows(intCnt).Item("ItemRetAdvId") = dsPRDropList.Tables(0).Rows(intCnt).Item("ItemRetAdvId").Trim()
            dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvType") = dsPRDropList.Tables(0).Rows(intCnt).Item("ItemRetAdvId").Trim() & dsPRDropList.Tables(0).Rows(intCnt).Item("DispAdvType").Trim()
        Next intCnt

        drinsert = dsPRDropList.Tables(0).NewRow()
        drinsert("DispAdvType") = "NoSRA" & " "
        drinsert("ItemRetAdvId") = "Select Stock Return Advice ID or leave blank"

        dsPRDropList.Tables(0).Rows.InsertAt(drinsert, 0)
        lstPR.DataSource = dsPRDropList.Tables(0)
        lstPR.DataValueField = "DispAdvType"
        lstPR.DataTextField = "ItemRetAdvId"
        lstPR.DataBind()

        If Not dsPRDropList Is Nothing Then
            dsPRDropList = Nothing
        End If
    End Sub

    Sub GetDataSRA()
        Dim strParam As String
        Dim strOpCd_Get As String = "IN_CLSTRX_SRA_LINE_GET"
        Dim intErrNo As Integer
        Dim objSRALnDs As New Object()

        Dim strParamName As String
        Dim strParamValue As String
        strParamName = "LOCCODE|DOCID|STRSEARCH"
        strParamValue = Trim(strLocation) & "|" & lstPR.SelectedItem.Value.Substring(0, lstPR.SelectedItem.Value.Length - 1).Trim & "|" & " AND A.ItemRetAdvLnID='" & lstItem.SelectedItem.Value & "' "

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objSRALnDs)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STKRCV_BIND_ITEMCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        If objSRALnDs.Tables(0).Rows.Count > 0 Then
            TxtItemCode.Text = objSRALnDs.Tables(0).Rows(0).Item("ItemCode")
            txtQty.Text = objSRALnDs.Tables(0).Rows(0).Item("Qty")
            txtCost.Text = objSRALnDs.Tables(0).Rows(0).Item("Cost")
            txtAmount.Text = objSRALnDs.Tables(0).Rows(0).Item("Amount")
        End If


        'strParam = Trim(lstPR.SelectedItem.Text) & "|" & Trim(TxtItemCode.Text)

        'Try
        '    intErrNo = objINtx.mtdGetSRALn(strOpCd_Get, _
        '                                strCompany, _
        '                                strLocation, _
        '                                strUserId, _
        '                                strAccMonth, _
        '                                strAccYear, _
        '                                strParam, _
        '                                objSRALnDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_GET_SRALn&errmesg=" & lblErrMessage.Text & "&redirect=in/trx/IN_trx_StockReceive_list.aspx")
        'End Try

        'If objSRALnDs.Tables(0).Rows.Count > 0 Then

        '    txtQty.Text = objSRALnDs.Tables(0).Rows(0).Item("Qty")
        '    txtCost.Text = objSRALnDs.Tables(0).Rows(0).Item("Cost")
        '    txtAmount.Text = objSRALnDs.Tables(0).Rows(0).Item("Amount")
        'End If
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

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)

        'Dim strText = "Please select Inventory Bin"

        'ddlInventoryBin.Items.Clear()
        'ddlInventoryBin.Items.Add(New ListItem(strText, "0"))


        'ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO), objINstp.EnumInventoryBinLevel.HO))
        'ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Other), objINstp.EnumInventoryBinLevel.Other))

        'If Session("SS_LOCLEVEL") = "1" Then
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central), objINstp.EnumInventoryBinLevel.Central))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinI), objINstp.EnumInventoryBinLevel.BinI))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinII), objINstp.EnumInventoryBinLevel.BinII))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIII), objINstp.EnumInventoryBinLevel.BinIII))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIV), objINstp.EnumInventoryBinLevel.BinIV))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinV), objINstp.EnumInventoryBinLevel.BinV))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinVI), objINstp.EnumInventoryBinLevel.BinVI))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinVII), objINstp.EnumInventoryBinLevel.BinVII))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinVIII), objINstp.EnumInventoryBinLevel.BinVIII))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIX), objINstp.EnumInventoryBinLevel.BinIX))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinX), objINstp.EnumInventoryBinLevel.BinX))

        'End If

        'If Not Trim(pv_strInvBin) = "" Then
        '    With ddlInventoryBin
        '        .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
        '    End With
        'Else
        '    ddlInventoryBin.SelectedIndex = -1
        'End If

        Dim strText = "Please select Inventory Bin"

        ddlInventoryBin.Items.Clear()
        ddlInventoryBin.Items.Add(New ListItem(strText, "0"))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO), objINstp.EnumInventoryBinLevel.HO))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central), objINstp.EnumInventoryBinLevel.Central))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Other), objINstp.EnumInventoryBinLevel.Other))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinI), objINstp.EnumInventoryBinLevel.BinI))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinII), objINstp.EnumInventoryBinLevel.BinII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIII), objINstp.EnumInventoryBinLevel.BinIII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIV), objINstp.EnumInventoryBinLevel.BinIV))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinV), objINstp.EnumInventoryBinLevel.BinV))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinVI), objINstp.EnumInventoryBinLevel.BinVI))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinVII), objINstp.EnumInventoryBinLevel.BinVII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinVIII), objINstp.EnumInventoryBinLevel.BinVIII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIX), objINstp.EnumInventoryBinLevel.BinIX))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinX), objINstp.EnumInventoryBinLevel.BinX))


        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        Else
            If Session("SS_LOCLEVEL") = objAdminLoc.EnumLocLevel.HQ Then
                ddlInventoryBin.SelectedIndex = 1
            ElseIf Session("SS_LOCLEVEL") = objAdminLoc.EnumLocLevel.Estate Or Session("SS_LOCLEVEL") = objAdminLoc.EnumLocLevel.Mill Then
                ddlInventoryBin.SelectedIndex = 2
            Else
                ddlInventoryBin.SelectedIndex = -1
            End If
        End If
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

#End Region


End Class
