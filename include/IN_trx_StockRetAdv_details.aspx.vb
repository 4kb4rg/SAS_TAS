
Imports System
Imports System.Data
Imports System.Math
Imports System.Text
Imports System.Collections
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class IN_StockRetAdv : Inherits Page

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
    Protected WithEvents lblDate As Label
    Protected WithEvents txtQty As TextBox
    Protected WithEvents lblPDateTag As Label
    Protected WithEvents tblAdd As HtmlTable
    Protected WithEvents lblError As Label
    Protected WithEvents lblStock As Label
    Protected WithEvents lblUnDel As Label
    Protected WithEvents lblDoc As Label
    Protected WithEvents lblPR As Label
    Protected WithEvents lblTxError As Label
    Protected WithEvents lblStatusHid As Label
    Protected WithEvents lblConfirmErr As Label
    Protected WithEvents lblItemCodeErr As Label
    Protected WithEvents txtRemarks As TextBox

    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnConfirm As ImageButton
    Protected WithEvents btnPrint As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents lstItem As DropDownList
    Protected WithEvents lstDoc As DropDownList
    Protected WithEvents FindIN As HtmlInputButton
    Protected WithEvents lblDocTitle As Label
    Protected WithEvents ddlInventoryBin As DropDownList
    Protected WithEvents lstStorage As DropDownList
    Protected WithEvents lblInventoryBin As Label
    Protected WithEvents txtDate As TextBox
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblstoragemsg As Label

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbViewJournal As LinkButton
    Protected WithEvents dgViewJournal As DataGrid
    Protected WithEvents lblTotalDB As Label
    Protected WithEvents lblTotalCR As Label
    Protected WithEvents lblTotalViewJournal As Label

    Protected objINtx As New agri.IN.clstrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objGLtrx As New agri.GL.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAdmin As New agri.Admin.clsShare()

    Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_ITEMRETURNADVICE_ADD"
    Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_ITEMRETURNADVICE_SELECTIVE_UPD"
    Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_ITEMRETURNADVICE_LINE_GET"
    Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
    Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"
    Dim strOpCdStckRecvLine_Det_GET As String = "IN_CLSTRX_STOCKRECEIVELINE_DETAILS_GET"
    Dim strOpCdItem_Details_GET2 As String = "IN_CLSTRX_STOCKITEM_DETAILS_GET"
    Dim strOpCdStckRecvLine_Det_UPD As String = "IN_CLSTRX_STOCKRECEIVE_LINE_UPD"

    Dim objDataSet As New DataSet()
    Dim dsGrid As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strDateFMT As String
    Dim intINAR As Integer
    Dim intConfigsetting As Integer

    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objINSetup As New agri.IN.clsSetup()

    Dim objLangCapDs As New Dataset()
    Dim objAccDs As New Dataset()
    Dim objBlkDs As New Dataset()
    Dim objVehDs As New Dataset()
    Dim objVehExpDs As New Dataset()

    Protected WithEvents tblAcc As HtmlTable
    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlk As HtmlTableRow
    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents ddlPreBlock As DropDownList
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents ddlVehExpCode As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList

    Protected WithEvents FindAcc As HtmlInputButton
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Protected WithEvents hidItemType As HtmlInputHidden

    Protected WithEvents lblAccount As Label
    Protected WithEvents lblPreBlkTag As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehExpense As Label
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblPreBlockErr As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblErrVehExp As Label
    Protected WithEvents lblVehicleOption As Label

    Dim PreBlockTag As String
    Dim BlockTag As String
	
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strLocType As String
    Dim intLevel As Integer
	
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
	    
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturnAdvice), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrAccount.Visible = False
            lblPreBlockErr.Visible = False
            lblErrBlock.Visible = False
            lblErrVehicle.Visible = False
            lblErrVehExp.Visible = False

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            btnSave.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnSave).ToString())
            btnConfirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnConfirm).ToString())
            btnPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnPrint).ToString())
            btnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnDelete).ToString())
            btnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnNew).ToString())
            btnBack.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnBack).ToString())

            If Not Page.IsPostBack Then
                BindInventoryBinLevel("")
                BindStorage("")
                lblStckTxID.Text = Request.QueryString("Id")
                LoadStockTxDetails()

                If objDataSet.Tables(0).Rows.Count > 0 Then
                    DisplayFromDB()
                    DisablePage()
                    BindGrid()
                End If

                If lblStckTxID.Text = "" Then
                    txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    TrLink.Visible = False
                End If

                BindDocList()
                'BindItemCodeList()

                BindChargeLevelDropDownList()
                'BindAccount("")
                BindPreBlock("", "")
                BindBlock("", "")
                BindVehicle("", "")
                BindVehicleExpense(True, "")
                tblAcc.Visible = False
            End If
            lblError.Visible = False
            lblStock.Visible = False
            lblUnDel.Visible = False
            lblTxError.Visible = False
            lblConfirmErr.Visible = False
            lblItemCodeErr.Visible = False
            lblPR.Visible = False
            lblInventoryBin.Visible = False
            lblDate.Visible = False
            DisablePage()
        End If

    End Sub

    Sub DisablePage()
        txtRemarks.Enabled = False
        btnSave.Visible = False
        btnConfirm.Visible = False
        btnDelete.Visible = False
        btnPrint.Visible = False
        btnNew.Visible = False
        ddlInventoryBin.Enabled = False

        Select Case Trim(lblStatusHid.Text)
            Case Trim(CStr(objINtx.EnumStockRetAdvStatus.Deleted))
                btnDelete.Visible = True
                btnNew.Visible = True
                btnDelete.ImageUrl = "../../images/butt_undelete.gif"
                btnDelete.AlternateText = "Undelete"
                btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objINtx.EnumStockRetAdvStatus.Confirmed))
                btnNew.Visible = True
                btnPrint.Visible = True
            Case Else
                txtRemarks.Enabled = True
                btnSave.Visible = True
                ddlInventoryBin.Enabled = True

                If Trim(lblStckTxID.Text) <> "" Then
                    btnConfirm.Visible = True
                    btnDelete.Visible = True
                    btnPrint.Visible = True
                    btnNew.Visible = True
                    btnDelete.ImageUrl = "../../images/butt_delete.gif"
                    btnDelete.AlternateText = "Delete"
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                End If
        End Select
        DisableItemTable()
    End Sub

    Sub DisableItemTable()

        If lblStatusHid.Text = CStr(objINtx.EnumStockRetAdvStatus.Deleted) Or _
           lblStatusHid.Text = CStr(objINtx.EnumStockRetAdvStatus.Confirmed) Then

            tblAdd.Visible = False

        Else

            tblAdd.Visible = True

        End If

    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)

        Dim DeleteButton As LinkButton
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                DeleteButton = e.Item.FindControl("Delete")
                If Trim(lblStatusHid.Text) = Trim(CStr(objINtx.EnumStockRetAdvStatus.Active)) Then
                    DeleteButton.Visible = True
                    DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Else
                    DeleteButton.Visible = False
                End If
        End Select

    End Sub

    Sub DisplayFromDB()
        Status.Text = objINtx.mtdGetStockRetAdvStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
        lblStatusHid.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Status"))
        CreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("UserName"))
        txtDate.Text = objGlobal.GetShortDate(strDateFMT, Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
        txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))
        lblTotAmtFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objDataSet.Tables(0).Rows(0).Item("TotalAmount"), 2), 2)
        lblAccPeriod.Text = objDataSet.Tables(0).Rows(0).Item("AccMonth") & "/" & objDataSet.Tables(0).Rows(0).Item("AccYear")
        BindInventoryBinLevel(Trim(objDataSet.Tables(0).Rows(0).Item("Bin")))
        If Not objGlobal.mtdEmptyDate(objDataSet.Tables(0).Rows(0).Item("PrintDate")) Then
            lblPDateTag.Visible = True
            lblPrintDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("PrintDate")))
        End If
    End Sub

    Sub BindGrid()

        dgStkTx.DataSource = LoadDataGrid()
        dgStkTx.DataBind()
    End Sub

    Protected Function LoadDataGrid() As DataSet
        Dim strStorage As String
        Dim strParam As String
        Dim intCnt As Integer

        strParam = Trim(lblStckTxID.Text)

        Try
            intErrNo = objINtx.mtdGetStockTransactDetails(strOpCdStckTxLine_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          dsGrid)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
        End Try

        lstStorage.Enabled = True
        For intCnt = 0 To dsGrid.Tables(0).Rows.Count - 1
            lstStorage.Enabled = False
            strStorage = Trim(dsGrid.Tables(0).Rows(intCnt).Item("StorageCode"))
            dsGrid.Tables(0).Rows(intCnt).Item("Description") = Trim(dsGrid.Tables(0).Rows(intCnt).Item("ItemCode")) & " (" & _
                                                                Trim(dsGrid.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next intCnt

        BindStorage(strStorage)
        If dsGrid.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
        Else
            TrLink.Visible = False
        End If

        Return dsGrid
    End Function

    Sub LoadStockTxDetails()

        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_ITEMRETURNADVICE_DETAIL_GET"
        Dim StockCode As String
        strParam = Trim(lblStckTxID.Text)
        Try
            intErrNo = objINtx.mtdGetStockTransactDetails(strOpCdStckTxDet_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKRETURNADVICELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
        End Try
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

        If lblStatusHid.Text = CStr(objINtx.EnumStockRetAdvStatus.Active) Then

            Dim strOpCdStckTxLine_Det_GET As String = "IN_CLSTRX_ITEMRETURNADVICELN_GET"
            Dim strOpCdStckTxLine_DEL As String = "IN_CLSTRX_ITEMRETURNADVICELN_DEL"
            Dim lbl As Label
            Dim ItemCode As String
            Dim StockRetAdvLnID As String
            Dim strParam As String
            Dim StrTxParam As String
            Dim TxID As String
            Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

            lbl = E.Item.FindControl("ItemCode")
            ItemCode = lbl.Text
            lbl = E.Item.FindControl("RtnAdvLnID")
            StockRetAdvLnID = lbl.Text

            strParam = StockRetAdvLnID & "|" & ItemCode & "|" & Trim(lblStckTxID.Text)
            Try
                intErrNo = objINtx.mtdDelRtnAdvTransactLn(strOpCdStckTxLine_DEL, _
                                                        strOpCdStckTxLine_Det_GET, _
                                                        strOpCdItem_Details_UPD, _
                                                        strOpCdStckRecvLine_Det_GET, _
                                                        strOpCdStckRecvLine_Det_UPD, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_STOCKRETURNADVICELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try
            StrTxParam = lblStckTxID.Text & "|||||||||||||||"
            Try
                intErrNo = objINtx.mtdUpdStockRtnAdvDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            StrTxParam, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnAdvice), _
                                                            ErrorChk, _
                                                            TxID)
                lblStckTxID.Text = TxID
                If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                    lblError.Visible = True
                End If

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try

            LoadStockTxDetails()
            DisplayFromDB()
            BindGrid()
            BindItemCodeList()
        End If
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ITEM_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
        End Try

        If _objItemDs.Tables(0).Rows.Count = 1 Then
            pr_strItemType = CInt(_objItemDs.Tables(0).Rows(0).Item("ItemType"))
        End If
    End Sub

    Function CheckRequiredField() As Boolean
        If Request.Form("lstItem") = "" Then
            lblItemCodeErr.Visible = True
            Return True
        Else
            Return False
        End If

    End Function

    Sub RebindItemList(ByVal sender As Object, ByVal e As System.EventArgs)
        BindItemCodeList()
        If lstDoc.SelectedItem.Value = "NoDoc|" Then
            FindIN.Visible = True
        Else
            FindIN.Visible = False
        End If
        tblAcc.Visible = False 
    End Sub

    Sub BindItemCodeList()
        Dim strOpCdItem_List_GET As String
        Dim dsItemCodeDropList As DataSet
        Dim intCnt As Integer
        Dim strparam As String
        Dim DocID As Array
        Dim strRecvID As String
        Dim strItemCode As String
        Dim drinsert As DataRow

        If lstDoc.SelectedItem.Value = "NoDoc|" Then
            strOpCdItem_List_GET = "IN_CLSTRX_ITEMPART_ITEM_GET" 
        Else
            strOpCdItem_List_GET = "IN_CLSTRX_STOCKRTNADV_ITEMPART_WITHSTKRCVID_GET" 
        End If

        DocID = Split(lstDoc.SelectedItem.Value.Trim, "|")
        strparam = objINstp.EnumInventoryItemType.Stock & "','" & _
                    objINstp.EnumInventoryItemType.DirectCharge & "','" & _
                    objINstp.EnumInventoryItemType.WorkshopItem & _
                    "|" & objINstp.EnumStockItemStatus.Active & "|" & lblStckTxID.Text & "|" & "itm.ItemCode" & "|" & DocID(0)

        Try
            intErrNo = objINstp.mtdGetFilteredItemList(strOpCdItem_List_GET, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strparam, _
                                                       objINtx.EnumInventoryTransactionType.StockReturnAdvice, _
                                                       dsItemCodeDropList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTOSERVTYPEDROPDOWNLIST&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
        End Try

        If lstDoc.SelectedItem.Value = "NoDoc|" Then
            'For intCnt = 0 To dsItemCodeDropList.Tables(0).Rows.Count - 1
            '    strItemCode = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")
            '    dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = strItemCode

            '    If dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then
            '        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = strItemCode & " @ " & dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo") & " ( " & _
            '                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
            '                                                                        "Rp. " & objGlobal.GetIDDecimalSeparator(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("AverageCost")) & ", " & _
            '                                                                        objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
            '                                                                Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode")) 
            '    Else
            '        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = strItemCode & " ( " & dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
            '                                                                        "Rp. " & objGlobal.GetIDDecimalSeparator(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("AverageCost")) & ", " & _
            '                                                                        objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
            '                                                                Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode")) 
            '    End If
            'Next intCnt

        Else
            For intCnt = 0 To dsItemCodeDropList.Tables(0).Rows.Count - 1
                If dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then
                    strItemCode = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")
                    strRecvID = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("StockReceiveLnID")
                    dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = strItemCode & "|" & strRecvID
                    dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = strItemCode & " @ " & dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo") & " ( " & _
                                                                                    dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
                                                                                    objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
                                                                            Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode"))
                Else
                    strItemCode = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")
                    strRecvID = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("StockReceiveLnID")
                    dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = strItemCode & "|" & strRecvID
                    dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = strItemCode & " ( " & dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
                                                                                    objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
                                                                            Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode"))
                End If
            Next intCnt
        End If

        drinsert = dsItemCodeDropList.Tables(0).NewRow()
        drinsert("ItemCode") = ""
        drinsert("Description") = "Select Item Code"
        dsItemCodeDropList.Tables(0).Rows.InsertAt(drinsert, 0)

        lstItem.DataSource = dsItemCodeDropList.Tables(0)
        lstItem.DataValueField = "ItemCode"
        lstItem.DataTextField = "Description"
        lstItem.DataBind()


        If Not dsItemCodeDropList Is Nothing Then
            dsItemCodeDropList = Nothing
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

    Sub BindDocList()

        Dim strOpCdPR_List_GET As String = "IN_CLSTRX_STOCKRETURN_STKRECEIVE_LIST_GET"
        Dim dsDropList As DataSet
        Dim drinsert As DataRow
        Dim intCnt As Integer
        Dim strparam As String
        Dim strRecvID As String
        Dim strDAID As String
        Dim strGrRefNo As String

        strparam = objINtx.EnumStockReceiveDocType.DispatchAdvice & "','" & objINtx.EnumStockReceiveDocType.StockReturnAdvice & "','" & objINtx.EnumStockReceiveDocType.StockTransfer & "','" & objINtx.EnumStockReceiveDocType.PurchaseRequisition & "|" & objINtx.EnumStockRetAdvStatus.Confirmed & "|" & strLocation & "|" & "tx.StockReceiveID|DESC"
        Try
            intErrNo = objINtx.mtdStockReceiveForReturnGet(strOpCdPR_List_GET, strparam, dsDropList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTOPRDROPDOWNLIST&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
        End Try

        For intCnt = 0 To dsDropList.Tables(0).Rows.Count - 1
            strRecvID = Trim(dsDropList.Tables(0).Rows(intCnt).Item(0))
            strDAID = Trim(dsDropList.Tables(0).Rows(intCnt).Item(1))
            strGrRefNo = Trim(dsDropList.Tables(0).Rows(intCnt).Item(2))

            dsDropList.Tables(0).Rows(intCnt).Item(0) = strRecvID & "|" & strDAID
            dsDropList.Tables(0).Rows(intCnt).Item(1) = strRecvID & " (" & strDAID & " , " & strGrRefNo & ")"
        Next intCnt

        drinsert = dsDropList.Tables(0).NewRow()
        drinsert(0) = "NoDoc|"
        drinsert(1) = "Select Stock Receive ID or leave blank"


        dsDropList.Tables(0).Rows.InsertAt(drinsert, 0)
        lstDoc.DataSource = dsDropList.Tables(0)
        lstDoc.DataValueField = "StockReceiveID"
        lstDoc.DataTextField = "StockRefno"
        lstDoc.DataBind()

        If Not dsDropList Is Nothing Then
            dsDropList = Nothing
        End If
    End Sub

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_ITEMRETURNADVICELN_ADD"
        Dim strTxLnParam As String
        Dim TxID As String
        Dim StrTxParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strAllowVehicle As String
        Dim PRID As String
        Dim arrPartNo As Array 
        Dim strItemCode As String 
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim intNurseryInd As Integer
        Dim strPreBlk As String = Request.Form("ddlPreBlock") 
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim IsDirectCharge As Boolean
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

        If CheckRequiredField() Then
            Exit Sub
        End If


        'GetAccountDetails(ddlAccount.SelectedItem.Value, blnIsBalanceSheet, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers, intNurseryInd)

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
        '        strNewIDFormat = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnAdvice) & Right(strAccYear, 2) & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth)
        '    Case Else
        '        strNewIDFormat = "NRI" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select

        'strNewIDFormat = "NRI" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        strNewIDFormat = "SRA" & "/" & strCompany & "/" & strLocation & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & strAccYear & "/"

        If lblStckTxID.Text = "" Then

            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(objGlobal.EnumDocType.StockReturnAdvice)
            StrTxParam.Append("||||||")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|0|")
            StrTxParam.Append(txtRemarks.Text)
            StrTxParam.Append("||")
            StrTxParam.Append("|")
            StrTxParam.Append(Trim(ddlInventoryBin.SelectedItem.Value))
            StrTxParam.Append("|")
            StrTxParam.Append(strNewIDFormat)
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)

            Try
                intErrNo = objINtx.mtdUpdStockRtnAdvDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnAdvice), _
                                                            ErrorChk, _
                                                            TxID)

                lblStckTxID.Text = TxID
                If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                    lblError.Visible = True
                End If

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try

        End If 

        If ErrorChk = objINtx.EnumInventoryErrorType.noError And intErrNo = 0 Then
            Dim DocID As Array
            Dim ItemCode As Array
            DocID = lstDoc.SelectedItem.Value.Trim.Split("|")

            If lstDoc.SelectedItem.Value = "NoDoc|" Then

                arrPartNo = Split(Request.Form("lstItem"), " @ ")
                If arrPartNo.GetUpperBound(0) = 1 Then
                    strItemCode = arrPartNo(0)
                Else
                    strItemCode = Request.Form("lstItem")
                End If

                strTxLnParam = lblStckTxID.Text & "|" & DocID(0) & "|" & txtQty.Text & "||" & DocID(1) & "|" & strItemCode

                IsDirectChargeItem (strItemCode, IsDirectCharge) 
            Else
                ItemCode = lstItem.SelectedItem.Value.Trim.Split("|")
                strTxLnParam = lblStckTxID.Text & "|" & DocID(0) & "|" & txtQty.Text & "|" & ItemCode(1) & "|" & DocID(1) & "|" & ItemCode(0)

                IsDirectChargeItem (ItemCode(0), IsDirectCharge) 
            End If


            If IsDirectCharge = True Then
                strTxLnParam = strTxLnParam & "|" & ddlAccount.SelectedItem.Value & "|" & _
                                IIf(ddlChargeLevel.SelectedIndex = 0, ddlPreBlock.SelectedItem.Value, ddlBlock.SelectedItem.Value) & "|" & _
                                ddlVehCode.SelectedItem.Value & "|" & _
                                ddlVehExpCode.SelectedItem.Value
            Else
                strTxLnParam = strTxLnParam & "||||"
            End If

            Try
                intErrNo = objINtx.mtdAddStockRtnAdvLn(strOpCdStckTxLine_ADD, _
                                                        strOpCdItem_Details_GET2, _
                                                        strOpCdItem_Details_UPD, _
                                                        strOpCdStckRecvLine_Det_GET, _
                                                        strOpCdStckRecvLine_Det_UPD, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnAdviceLn), _
                                                        ErrorChk, _
                                                        strTxLnParam)
                Select Case ErrorChk
                    Case objINtx.EnumInventoryErrorType.OverFlow
                        lblError.Visible = True
                    Case objINtx.EnumInventoryErrorType.InsufficientQty
                        lblStock.Visible = True
                    Case objINtx.EnumInventoryErrorType.PRNotfound
                        lblPR.Visible = True
                End Select

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try
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

        sSQLKriteria = "UPDATE IN_ITEMRETADVLN SET StorageCode='" & lstStorage.SelectedItem.Value & "' Where ItemRetAdvID='" & strTransID & "'"

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
        StrTxParam.Append("||||||||||||||||")

        If ErrorChk = objINtx.EnumInventoryErrorType.noError And intErrNo = 0 Then
            Try
                intErrNo = objINtx.mtdUpdStockRtnAdvDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnAdvice), _
                                                            ErrorChk, _
                                                            TxID)

                If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                    lblError.Visible = True
                End If

                lblStckTxID.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try

        End If

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        LoadStockTxDetails()
        DisplayFromDB()
        BindGrid()
        BindItemCodeList()
        DisablePage()
        txtQty.Text = ""

    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim TxID As String
        Dim StrTxParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strAllowVehicle As String
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

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strNewIDFormat = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnAdvice) & Right(strAccYear, 2) & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth)
        '    Case Else
        '        strNewIDFormat = "NRI" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        strNewIDFormat = "NRI" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        If lblStckTxID.Text = "" Then
            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(objGlobal.EnumDocType.StockReturnAdvice)
            StrTxParam.Append("||||||")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|0|")
            StrTxParam.Append(txtRemarks.Text)
            StrTxParam.Append("||")
            StrTxParam.Append("|")
            StrTxParam.Append(Trim(ddlInventoryBin.SelectedItem.Value))
            StrTxParam.Append("|")
            StrTxParam.Append(strNewIDFormat)
        Else
            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(objGlobal.EnumDocType.StockReturnAdvice)
            StrTxParam.Append("||||||")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("||")
            StrTxParam.Append(txtRemarks.Text)
            StrTxParam.Append("||")
            StrTxParam.Append("|")
            StrTxParam.Append(Trim(ddlInventoryBin.SelectedItem.Value))
            StrTxParam.Append("|")
            StrTxParam.Append(strNewIDFormat)
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
        End If

        Try
            intErrNo = objINtx.mtdUpdStockRtnAdvDetail(strOpCdStckTxDet_ADD, _
                                                        strOpCdStckTxDet_UPD, _
                                                        strOpCdStckTxLine_GET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnAdvice), _
                                                        ErrorChk, _
                                                        TxID)
            lblStckTxID.Text = TxID
            If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                lblError.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
            End If
        End Try

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If


        LoadStockTxDetails()
        DisplayFromDB()
        DisablePage()
        BindGrid()

    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strItemCodeList As String
        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_ITEMRETURNADVICELN_ADD"
        Dim strOpCdTxLnList_GET As String = "IN_CLSTRX_ITEMRETURNADVICE_LINE_GET"
        Dim strOpCdTxLnList_UPD As String = "IN_CLSTRX_ITEMRETURNADVICELN_UPD"
        Dim strOpCdRecvLine_GET As String = "IN_CLSTRX_ITEMRETURNADVICELN_STOCKRECEIVELN_GET"
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

        If Not LoadDataGrid.Tables(0).Rows.Count > 0 Then
            lblConfirmErr.Visible = True
            Exit Sub
        End If

        Try
            'intErrNo = objINtx.mtdConfirmReturnAdviceDoc(strOpCdTxLnList_GET, _
            '                                            strOpCdStckTxLine_ADD, _
            '                                            strOpCdTxLnList_UPD, _
            '                                            strOpCdRecvLine_GET, _
            '                                            strOpCdItem_Details_GET2, _
            '                                            strOpCdItem_Details_UPD, _
            '                                            strCompany, _
            '                                            strLocation, _
            '                                            strUserId, _
            '                                            strAccMonth, _
            '                                            strAccYear, _
            '                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnAdviceLn), _
            '                                            lblStckTxID.Text, _
            '                                            strItemCodeList, _
            '                                            ErrorChk)
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CONFIRMRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
            End If
        End Try

        If ErrorChk <> objINtx.EnumInventoryErrorType.NoError Then
            lblTxError.Visible = True
        Else

        End If

        If intErrNo = 0 And ErrorChk = objINtx.EnumInventoryErrorType.NoError Then
            StrTxParam = lblStckTxID.Text & "|||||||||||" & txtRemarks.Text & "||" & objINtx.EnumStockRetAdvStatus.Confirmed & "|||"
            Try
                intErrNo = objINtx.mtdUpdStockRtnAdvDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnAdvice), _
                                                            ErrorChk, _
                                                            TxID)
                lblStckTxID.Text = TxID
                If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                    lblError.Visible = True
                End If

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try
        End If

        If intErrNo = 0 And ErrorChk = objINtx.EnumInventoryErrorType.NoError Then
            Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""

            strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

            strParamValue = Trim(strLocation) & _
                            "|" & "IN_ITEMRETADV" & _
                            "|" & "IN_ITEMRETADVLN" & _
                            "|" & "ITEMRETADVID" & _
                            "|" & Trim(lblStckTxID.Text) & _
                            "|" & "QTY" & _
                            "|" & ddlInventoryBin.SelectedItem.Value & _
                            "|" & "-" & _
                            "|" & Trim(strUserId)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
            End Try

            LoadStockTxDetails()
            DisplayFromDB()
            DisablePage()
            BindGrid()
        End If

        If strItemCodeList <> "" Then
            If InStr(strItemCodeList, ",") = 0 Then
                Response.Write("<Script Language=""JavaScript"">window.alert(""As a result of confirming the document,\nthe average cost for this item had been set to zero.\nPlease carry out necessary stock adjustment.\n\n" & strItemCodeList & """);</Script>")
            Else
                Response.Write("<Script Language=""JavaScript"">window.alert(""As a result of confirming the document,\nthe average cost for these items had been set to zero.\nPlease carry out necessary stock adjustment.\n\n" & strItemCodeList & """);</Script>")
            End If
        End If
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

        If lblStatusHid.Text = CStr(objINtx.EnumStockRetAdvStatus.Deleted) Then
            Try

                intErrNo = objINtx.mtdReturnAdvInvItemLevel(strOpCdStckTxLine_GET, _
                                                            strOpCdStckRecvLine_Det_GET, _
                                                            strOpCdStckRecvLine_Det_UPD, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            strOpCdItem_Details_UPD, _
                                                            strOpCdItem_Details_GET2, _
                                                            lblStckTxID.Text, _
                                                            objINtx.EnumTransactionAction.Undelete, _
                                                            ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UNDELETESTOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try
            StrTxParam = lblStckTxID.Text & "|||||||||||||" & objINtx.EnumStockRetAdvStatus.Active & "|"

        ElseIf lblStatusHid.Text = CStr(objINtx.EnumStockRetAdvStatus.Active) Then
            Try

                intErrNo = objINtx.mtdReturnAdvInvItemLevel(strOpCdStckTxLine_GET, _
                                                            strOpCdStckRecvLine_Det_GET, _
                                                            strOpCdStckRecvLine_Det_UPD, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            strOpCdItem_Details_UPD, _
                                                            strOpCdItem_Details_GET2, _
                                                            lblStckTxID.Text, _
                                                            objINtx.EnumTransactionAction.Delete, _
                                                            ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETENEWSTOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try
            StrTxParam = lblStckTxID.Text & "|||||||||||||" & objINtx.EnumStockRetAdvStatus.Deleted & "|||"
        End If

        If intErrNo = 0 And Not ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            Try
                intErrNo = objINtx.mtdUpdStockRtnAdvDetail(strOpCdStckTxDet_ADD, _
                                                           strOpCdStckTxDet_UPD, _
                                                           strOpCdStckTxLine_GET, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           StrTxParam, _
                                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnAdvice), _
                                                           ErrorChk, _
                                                           TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_NEWSTOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            lblUnDel.Visible = True
        End If
        LoadStockTxDetails()
        DisplayFromDB()
        DisablePage()
        BindGrid()
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String
        Dim intStatus As Integer
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String
        Dim strStockTxId As String

        strStockTxId = Trim(lblStckTxID.Text)

        strUpdString = "where ItemRetAdvID = '" & strStockTxId & "'"
        strStatus = Trim(Status.Text)
        intStatus = CInt(Trim(lblStatusHid.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strTable = "IN_ITEMRETADV"
        strSortLine = "order by ItemRetAdvLnID"


        If intStatus = objINtx.EnumStockRetAdvStatus.Confirmed Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                        strUpdString, _
                                                        strTable, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_SHARE_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End Try
                LoadStockTxDetails()
                DisplayFromDB()
                DisablePage()
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockRetAdvDet.aspx?strStockTxId=" & strStockTxId & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       "&strDocTitle=" & lblDocTitle.Text & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_StockRetAdv_List.aspx")
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_Trx_StockRetAdv_Details.aspx")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code"
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code"
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_GRNDET_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=pu/trx/PU_trx_GRNList.aspx")
        End Try

        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & " Code"
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code"
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code"

        lblErrAccount.Text = "<BR>" & "Please select one " & lblAccount.Text
        lblErrBlock.Text = "Please select one " & lblBlock.Text
        lblErrVehicle.Text = "Please select one " & lblVehicle.Text
        lblErrVehExp.Text = "Please select one " & lblVehExpense.Text

        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTag.Text = PreBlockTag & " Code" & " : "
        lblPreBlockErr.Text = "Please select one " & PreBlockTag & " Code"

        dgStkTx.Columns(2).HeaderText = lblAccount.Text
        dgStkTx.Columns(3).HeaderText = lblBlock.Text
        dgStkTx.Columns(4).HeaderText = lblVehicle.Text
        dgStkTx.Columns(5).HeaderText = lblVehExpense.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_GR_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Please select one " & lblAccount.Text
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
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, strParam, _objAccDs, True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_GR_GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString() & "&redirect=")
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
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, strParam, objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        If objBlkDs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("_Description") = "Please select one " & PreBlockTag & " Code"

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

            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, strParam, objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_GR_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = pv_strBlkCode.Trim Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        If objBlkDs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("_Description") = "Please select one " & lblBlock.Text & " Code"
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
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.Vehicle, objVehDs)
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
        dr("Description") = "Please select one " & lblVehicle.Text
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
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.VehicleExpense, objVehExpDs)
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
        dr("Description") = "Please select one " & lblVehExpense.Text
        objVehExpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehExpCode.DataSource = objVehExpDs.Tables(0)
        ddlVehExpCode.DataValueField = "VehExpenseCode"
        ddlVehExpCode.DataTextField = "Description"
        ddlVehExpCode.DataBind()
        ddlVehExpCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.SelectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
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

    Sub ItemIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim IsDirectCharge As Boolean
        Dim ItemCode As Array

        hidItemType.Value = 0

        If lstItem.SelectedIndex > 0 Then
            ItemCode = lstItem.SelectedItem.Value.Trim.Split("|")
            IsDirectChargeItem (ItemCode(0), IsDirectCharge)

            If IsDirectCharge = True Then
                tblAcc.Visible = True
            Else
                tblAcc.Visible = False
            End If
        Else
            tblAcc.Visible = False
        End If
    End Sub

    Sub IsDirectChargeItem (ByVal pr_strItemCode As String, ByRef pr_IsDirectCharge As Boolean)
        Dim strItemOpCodeGet As String = "IN_CLSSETUP_ITEM_DETAIL_GET"
        Dim strWhere As String
        Dim strSortBy As String
        Dim objDataSet As New Dataset()

        pr_IsDirectCharge = False

        strWhere = " WHERE IN_ITEM.ItemCode = '" & pr_strItemCode & "'"
        strSortBy = ""

        Try
            intErrNo = objINtx.mtdGetStockAdjustment(strItemOpCodeGet, strWhere, strSortBy, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKITEM_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_trx_StockRetAdv_details.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            hidItemType.Value = objDataSet.Tables(0).Rows(0).Item("ItemType").Trim()
            If hidItemType.Value = objINSetup.EnumInventoryItemType.DirectCharge Then
                pr_IsDirectCharge = True
            End If
        End If
    End Sub

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
        Dim strText = "Please select Inventory Bin"

        ddlInventoryBin.Items.Clear()
        ddlInventoryBin.Items.Add(New ListItem(strText, "0"))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.HO), objINSetup.EnumInventoryBinLevel.HO))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Central), objINSetup.EnumInventoryBinLevel.Central))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Other), objINSetup.EnumInventoryBinLevel.Other))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinI), objINSetup.EnumInventoryBinLevel.BinI))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinII), objINSetup.EnumInventoryBinLevel.BinII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIII), objINSetup.EnumInventoryBinLevel.BinIII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIV), objINSetup.EnumInventoryBinLevel.BinIV))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinV), objINSetup.EnumInventoryBinLevel.BinV))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinVI), objINSetup.EnumInventoryBinLevel.BinVI))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinVII), objINSetup.EnumInventoryBinLevel.BinVII))

        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        Else
            ddlInventoryBin.SelectedIndex = -1
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

        LoadStockTxDetails()
        DisplayFromDB()
        DisablePage()
        BindGrid()
    End Sub

End Class
