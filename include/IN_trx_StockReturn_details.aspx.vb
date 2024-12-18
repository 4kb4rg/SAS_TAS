
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

Public Class IN_ReturnDet : Inherits Page

    Protected WithEvents dgStkTx As DataGrid
    Protected WithEvents lblStckTxID As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblTotAmtFig As Label
    Protected WithEvents lblTotPriceFig As Label
    Protected WithEvents lblReprint As Label
    Protected WithEvents lblAccTag As Label
    Protected WithEvents txtRemarks As TextBox
    Protected WithEvents lblBlkTag As Label
    Protected WithEvents lblVehTag As Label
    Protected WithEvents lblVehExpTag As Label
    Protected WithEvents txtQty As TextBox
    Protected WithEvents lblPDateTag As Label
    Protected WithEvents tblAdd As HtmlTable
    Protected WithEvents lblError As Label
    Protected WithEvents lblStock As Label
    Protected WithEvents lblUnDel As Label
    Protected WithEvents lblQty As Label
    Protected WithEvents lblTxError As Label
    Protected WithEvents lblConfirmErr As Label
    Protected WithEvents lblStatusHid As Label
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents PRDelete As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Confirm As ImageButton
    Protected WithEvents Print As ImageButton
    Protected WithEvents Cancel As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents Back As ImageButton
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lstItem As DropDownList
    Protected WithEvents lstStckIss As DropDownList
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehExp As Label
    Protected WithEvents lblStckIssErr As Label
    Protected WithEvents lblItemCodeErr As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblDocTitle As Label
    Protected WithEvents lblChargeTo As Label
    Protected WithEvents RowChargeTo As HtmlTableRow
    Protected WithEvents RowAcc As HtmlTableRow
    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents RowVeh As HtmlTableRow
    Protected WithEvents RowVehExp As HtmlTableRow

    Protected WithEvents ddlInventoryBin As DropDownList
    Protected WithEvents lblInventoryBin As Label
    Protected WithEvents lstStorage As DropDownList

    Protected WithEvents txtDate As TextBox
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Dim strDateFMT As String

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbViewJournal As LinkButton
    Protected WithEvents dgViewJournal As DataGrid
    Protected WithEvents lblTotalDB As Label
    Protected WithEvents lblTotalCR As Label
    Protected WithEvents lblTotalViewJournal As Label
    Protected WithEvents lblstoragemsg As Label

    Protected WithEvents txtAddNote As HtmlTextArea
    Protected WithEvents lblTxLnID As Label
    Protected WithEvents lblOldQty As Label
    Protected WithEvents lblOldItemCode As Label
    Protected WithEvents btnUpdate As ImageButton

    Protected objINtx As New agri.IN.clstrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLSetup As New agri.GL.clsSetup()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_STOCKRETURN_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_STOCKRETURN_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_STOCKRETURN_LINE_GET"
    Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
    Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"
    Dim strOpCdIssueLn_Det_GET As String = "IN_CLSTRX_STOCKISSUE_LINE_DETAILS_GET"
    Dim strOpCdIssueLn_Det_UPD As String = "IN_CLSTRX_STOCKISSUE_LINE_DETAILS_UPD"
    Dim strOpCdIssue_Det_GET As String = "IN_CLSTRX_STOCKISSUE_DETAIL_GET"

    Dim objDataSet As New DataSet()
    Dim dsGrid As New DataSet()
    Dim objLangCapDs As New Dataset()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objINstp As New agri.IN.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmin As New agri.Admin.clsShare()

    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim intINAR As Integer
    Dim intConfigsetting As Integer
    Dim strAccTag As String
    Dim strBlkTag As String
    Dim strVehTag As String
    Dim strVehExpCodeTag As String

    Protected WithEvents lblBPErr As Label
    Protected WithEvents lblLocCodeErr As Label
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
     Dim strLocLevel As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
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
        strLocLevel = Session("SS_LOCLEVEL")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturn), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            Save.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Save).ToString())
            Confirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Confirm).ToString())
            Cancel.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Cancel).ToString())
            Print.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Print).ToString())
            PRDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(PRDelete).ToString())
            btnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnNew).ToString())
            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())

            If Not Page.IsPostBack Then
                If strLocLevel = "1" Then
                    BindInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central)
                ElseIf strLocLevel = "3" Then
                    BindInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO)
                Else
                    BindInventoryBinLevel("")
                End If
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

                BindStockIssueDropList()
                LoadIssueDetails()
                BindItemCodeList()
                RowChargeTo.Visible = False
                RowAcc.Visible = False
                RowBlk.Visible = False
                RowVeh.Visible = False
                RowVehExp.Visible = False
            End If

            lblError.Visible = False
            lblStock.Visible = False
            lblUnDel.Visible = False
            lblStckIssErr.Visible = False
            lblTxError.Visible = False
            lblItemCodeErr.Visible = False
            DisablePage()
            lblBPErr.Visible = False
            lblLocCodeErr.Visible = False
            lblInventoryBin.Visible = False
            lblDate.Visible = False
        End If

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
               strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKRETURN_DET_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_trx_StockReturn_list.aspx")
        End Try

        strAccTag = GetCaption(objLangCap.EnumLangCap.Account)
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strVehExpCodeTag = GetCaption(objLangCap.EnumLangCap.VehExpense)

        lblAccTag.Text = strAccTag & lblCode.Text & " (CR) :* "
        lblBlkTag.Text = strBlkTag & lblCode.Text & " : "
        lblVehTag.Text = strVehTag & lblCode.Text & " : "
        lblVehExpTag.text = strVehExpCodeTag & lblCode.text & " : "

        dgStkTx.Columns(3).HeaderText = strAccTag & lblCode.Text
        dgStkTx.Columns(4).HeaderText = strBlkTag & lblCode.Text
        dgStkTx.Columns(5).HeaderText = strVehTag & lblCode.Text
        dgStkTx.Columns(6).HeaderText = strVehExpCodeTag & lblCode.Text
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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKRETURN_DET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
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

    Sub DisablePage()
        txtRemarks.Enabled = False
        Save.Visible = False
        PRDelete.Visible = False
        Confirm.Visible = False
        Print.Visible = False
        cancel.Visible = False
        btnNew.Visible = False
        ddlInventoryBin.Enabled = False

        Select Case Trim(lblStatusHid.Text)
            Case Trim(CStr(objINtx.EnumStockReturnStatus.Deleted))
                PRDelete.Visible = True
                btnNew.Visible = True
                PRDelete.ImageUrl = "../../images/butt_undelete.gif"
                PRDelete.AlternateText = "Undelete"
                PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objINtx.EnumStockReturnStatus.Active)), ""
                txtRemarks.Enabled = True
                Save.Visible = True
                ddlInventoryBin.Enabled = True

                If Trim(lblStckTxID.Text) <> "" Then
                    PRDelete.Visible = True
                    Confirm.Visible = True
                    Print.Visible = True
                    btnNew.Visible = True
                    PRDelete.ImageUrl = "../../images/butt_delete.gif"
                    PRDelete.AlternateText = "Delete"
                    PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                End If
            Case Else
                Print.Visible = True
                btnNew.Visible = True
        End Select
        DisableItemTable()
    End Sub

    Sub BindStorage(ByVal pv_strcode As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"

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

    Sub DisableItemTable()
        If lblStatusHid.Text = CStr(objINtx.EnumStockReturnStatus.Deleted) Or _
            lblStatusHid.Text = CStr(objINtx.EnumStockReturnStatus.cancelled) Then
            tblAdd.Visible = False
        ElseIf lblStatusHid.Text = CStr(objINtx.EnumStockReturnStatus.Confirmed) Then
            tblAdd.Visible = False
        Else
            tblAdd.Visible = True

        End If

    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        If lblStatusHid.Text = CStr(objINtx.EnumStockReturnStatus.Active) Then
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim DeleteButton As LinkButton
                    Dim EditButton As LinkButton
                    DeleteButton = e.Item.FindControl("Delete")
                    DeleteButton.Visible = True
                    DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    EditButton = e.Item.FindControl("Edit")
                    EditButton.Visible = False
                    EditButton = e.Item.FindControl("Cancel")
                    EditButton.Visible = False
            End Select
        ElseIf lblStatusHid.Text = CStr(objINtx.EnumStockReturnStatus.Confirmed) Then
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim DeleteButton As LinkButton
                    Dim EditButton As LinkButton
                    DeleteButton = e.Item.FindControl("Delete")
                    DeleteButton.Visible = False
                    EditButton = e.Item.FindControl("Edit")
                    EditButton.Visible = True
                    EditButton = e.Item.FindControl("Cancel")
                    EditButton.Visible = False
            End Select
        Else
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim DeleteButton As LinkButton
                    Dim EditButton As LinkButton
                    DeleteButton = e.Item.FindControl("Delete")
                    DeleteButton.Visible = False
                    EditButton = e.Item.FindControl("Edit")
                    EditButton.Visible = False
                    EditButton = e.Item.FindControl("Cancel")
                    EditButton.Visible = False
            End Select
        End If
    End Sub

    Sub DisplayFromDB()
        If objDataSet.Tables(0).Rows.Count > 0 Then
            Status.Text = objINtx.mtdGetStockReturnStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
            lblStatusHid.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Status"))
            CreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
            UpdateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
            UpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("UserName"))
            txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))
            lblTotAmtFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objDataSet.Tables(0).Rows(0).Item("TotalAmount"), 2), 2)
            lblTotPriceFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objDataSet.Tables(0).Rows(0).Item("TotalPrice"), 2), 2)
            lblAccPeriod.Text = objDataSet.Tables(0).Rows(0).Item("AccMonth") & "/" & objDataSet.Tables(0).Rows(0).Item("AccYear")
            BindInventoryBinLevel(Trim(objDataSet.Tables(0).Rows(0).Item("Bin")))
            txtDate.Text = objGlobal.GetShortDate(strDateFMT, Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))

            If Not objGlobal.mtdEmptyDate(objDataSet.Tables(0).Rows(0).Item("PrintDate")) Then
                lblPDateTag.Visible = True
                lblPrintDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("PrintDate")))
            End If

        End If
    End Sub

    Function CheckRequiredField() As Boolean
        If lstStckIss.SelectedItem.Value = "" Then
            lblStckIssErr.Visible = True
            Return True
        ElseIf lstItem.SelectedItem.Value = "" Then
            lblItemCodeErr.Visible = True
            Return True
        Else
            Return False
        End If
    End Function

    Sub BindGrid()
        dgStkTx.DataSource = LoadDataGrid()
        dgStkTx.DataBind()
    End Sub

    Protected Function LoadDataGrid() As DataSet
        Dim strStorageCode As String = ""
        Dim strParam As String
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
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
        End Try

        lstStorage.Enabled = True
        For intCnt = 0 To dsGrid.Tables(0).Rows.Count - 1
            lstStorage.Enabled = False
            strStorageCode = Trim(dsGrid.Tables(0).Rows(intCnt).Item("StorageCode"))
        Next


        If dsGrid.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
        Else
            TrLink.Visible = False
        End If

        BindStorage(strStorageCode)

        Return dsGrid
    End Function

    Sub LoadStockTxDetails()
        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_STOCKRETURN_DETAIL_GET"
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
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKRETURNDETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
        End Try
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim Delbutton As LinkButton
        Dim strItemCode As String
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

        'edit confirmed
        If lblStatusHid.Text = CStr(objINtx.EnumStockIssueStatus.Confirmed) Then
            tblAdd.Visible = True
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

        lbl = E.Item.FindControl("RtnLnID")
        lblTxLnID.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("stkIssueID")
        lstStckIss.SelectedItem.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("ItemCode")
        strItemCode = lbl.Text.Trim
        GetItem(strItemCode)

        lbl = E.Item.FindControl("lblQtyTrx")
        txtQty.Text = lbl.Text.Trim
        lblOldQty.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("ItemCode")
        lblOldItemCode.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblAddNote")
        txtAddNote.Value = lbl.Text.Trim

        'BindGrid()

        If lblStckTxID.Text <> "" Then
            btnAdd.Visible = False
            btnUpdate.Visible = True
        Else
            btnAdd.Visible = True
            btnUpdate.Visible = False
        End If


        lstStckIss.Enabled = False
        lstItem.Enabled = False

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgStkTx.EditItemIndex = -1
        DisableItemTable()
        BindGrid()
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

        If lblStatusHid.Text = CStr(objINtx.EnumStockReturnStatus.Active) Then

            Dim strOpCdStckTxLine_Det_GET As String = "IN_CLSTRX_STOCKRETURNLINE_DETAILS_GET"
            Dim strOpCdStckTxLine_DEL As String = "IN_CLSTRX_STOCKRETURN_LINE_DEL"
            Dim lbl As Label
            Dim ItemCode As String
            Dim StockRtnLnID As String
            Dim strParam As String
            Dim StrTxParam As String
            Dim TxID As String
            Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

            lbl = E.Item.FindControl("ItemCode")
            ItemCode = lbl.Text
            lbl = E.Item.FindControl("RtnLnID")
            StockRtnLnID = lbl.Text

            strParam = StockRtnLnID & "|" & ItemCode & "|" & Trim(lblStckTxID.Text)
            Try
                intErrNo = objINtx.mtdDelReturnTransactLn(strOpCdStckTxLine_DEL, _
                                                        strOpCdStckTxLine_Det_GET, _
                                                        strOpCdItem_Details_UPD, _
                                                        strOpCdIssueLn_Det_GET, _
                                                        strOpCdIssueLn_Det_UPD, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam)
            Catch Exp As Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_STOCKRECEIVELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
                End If
            End Try

            Try
                StrTxParam = lblStckTxID.Text & "||||||||||||||||"

                intErrNo = objINtx.mtdUpdStockReturnDetail(strOpCdStckTxDet_ADD, _
                                                           strOpCdStckTxDet_UPD, _
                                                           strOpCdStckTxLine_GET, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           StrTxParam, _
                                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturn), _
                                                           ErrorChk, _
                                                           TxID)

                If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                    lblError.Visible = True
                End If

                lblStckTxID.Text = TxID
            Catch Exp As Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
                End If
            End Try

            LoadStockTxDetails()
            DisplayFromDB()
            BindGrid()
            BindItemCodeList()
        End If
    End Sub

    Sub GetItem(ByVal pv_strItemCode As String)
        Dim dr As DataRow
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strPOType As String

        Dim strOpCode As String = "IN_CLSTRX_ITEMPART_ITEM_GET"
        Dim dsMaster As Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = " And itm.ItemCode = '" & Trim(pv_strItemCode) & "' AND itm.LocCode = '" & strLocation & "' AND itm.Status = '" & objINstp.EnumStockItemStatus.Active & "'  " & "|itm.ItemCode"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        For intCnt = 0 To dsMaster.Tables(0).Rows.Count - 1
            If dsMaster.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(pv_strItemCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsMaster.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = "Please select Item Code"
        dsMaster.Tables(0).Rows.InsertAt(dr, 0)

        lstItem.DataSource = dsMaster.Tables(0)
        lstItem.DataValueField = "ItemCode"
        lstItem.DataTextField = "Description"
        lstItem.DataBind()
        lstItem.SelectedIndex = intSelectedIndex
    End Sub

    Sub CallLoadIssueDetails(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrIssueType As Array
        Dim arrIssueType2 As Array

        BindItemCodeList()

        If lstStckIss.SelectedIndex <> 0 Then
            arrIssueType = Split(lstStckIss.SelectedItem.Text.Trim, "(")
            arrIssueType2 = Split(arrIssueType(1), ")")

            If arrIssueType2(0) = objINtx.mtdGetStockIssueType(objINtx.EnumStockIssueType.OwnUse) Then
                RowChargeTo.Visible = Session("SS_INTER_ESTATE_CHARGING")
                RowAcc.Visible = True
                RowBlk.Visible = True
                RowVeh.Visible = True
                RowVehExp.Visible = True
            Else
                RowChargeTo.Visible = False
                RowAcc.Visible = False
                RowBlk.Visible = False
                RowVeh.Visible = False
                RowVehExp.Visible = False
            End If
        End If
    End Sub

    Sub ShowIssuedLnDetails(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrIssueDet As Array

        If lstItem.SelectedIndex <> 0 Then
            arrIssueDet = Split(lstItem.SelectedItem.Value.Trim, "|")
            lblChargeTo.Text = arrIssueDet(8)
            lblAccCode.Text = arrIssueDet(2)
            lblBlock.Text = arrIssueDet(3)
            lblVehCode.Text = arrIssueDet(4)
            lblVehExp.Text = arrIssueDet(5)
        End If
    End Sub

    Sub LoadIssueDetails()
        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_STOCKISSUE_DETAIL_GET"
        Dim dsIssue As New DataSet()
        strParam = Trim(lstStckIss.SelectedItem.Value)

        Try
            intErrNo = objINtx.mtdGetStockTransactDetails(strOpCdStckTxDet_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          dsIssue)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKISSUELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
        End Try
    End Sub

    Sub BindStockIssueDropList()

        Dim strOpCdStkIssue_Get As String = "IN_CLSTRX_STOCKISSUE_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim dsForInactiveItem As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow

        Try
            strParam = objINtx.EnumStockIssueStatus.Confirmed & "|" & _
                        objINtx.EnumStockIssueType.All & "|" & _
                        strLocation & "|" & _
                        strAccMonth & "|" & _
                        strAccYear & "|stockIssueID|DESC|"

            intErrNo = objINtx.mtdGetStockIssueListForReturn(strOpCdStkIssue_Get, strParam, dsForDropDown)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("StockIssueID") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("StockIssueID"))
            If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("IssueType")) = objINtx.EnumStockIssueType.External Then
                dsForDropDown.Tables(0).Rows(intCnt).Item("IssueType") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("StockIssueID")) & " (" & _
                                                          objINtx.mtdGetStockIssueType(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("IssueType"))) & "), " & _
                                                          dsForDropDown.Tables(0).Rows(intCnt).Item("BillParty")
            Else
                dsForDropDown.Tables(0).Rows(intCnt).Item("IssueType") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("StockIssueID")) & " (" & _
                                                          objINtx.mtdGetStockIssueType(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("IssueType"))) & ")"
            End If
        Next intCnt
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Select Stock Issue ID"
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstStckIss.DataSource = dsForDropDown.Tables(0)
        lstStckIss.DataValueField = "StockIssueID"
        lstStckIss.DataTextField = "IssueType"
        lstStckIss.DataBind()






        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
        If Not dsForInactiveItem Is Nothing Then
            dsForInactiveItem = Nothing
        End If
    End Sub

    Sub BindItemCodeList()
        Dim strOpCdItem_List_GET As String = "IN_CLSTRX_STOCKRETURN_ITEMLIST_GET"
        Dim dsItemCodeDropList As DataSet
        Dim intCnt As Integer
        Dim strparam As String
        Dim strEMP As String



        If lstStckIss.SelectedIndex <> 0 Then
            Dim arrIssueType3 As Array
            Dim arrIssueType4 As Array

            arrIssueType3 = Split(lstStckIss.SelectedItem.Text.Trim, "(")
            arrIssueType4 = Split(arrIssueType3(1), ")")
            
            If arrIssueType4(0) = objINtx.mtdGetStockIssueType(objINtx.EnumStockIssueType.Nursery) Then
                strparam = objINstp.EnumInventoryItemType.NurseryItem & "|" & objINstp.EnumStockItemStatus.Active & "|" & lstStckIss.SelectedItem.Value & "|" & "itm.ItemCode" & "|" & lblStckTxID.Text
            Else
                strparam = objINstp.EnumInventoryItemType.Stock & "','" & objINstp.EnumInventoryItemType.WorkshopItem & "|" & objINstp.EnumStockItemStatus.Active & "|" & lstStckIss.SelectedItem.Value & "|" & "itm.ItemCode" & "|" & lblStckTxID.Text
			End if     
        Else
            strparam = objINstp.EnumInventoryItemType.Stock & "|" & objINstp.EnumStockItemStatus.Active & "|" & lstStckIss.SelectedItem.Value & "|" & "itm.ItemCode" & "|" & lblStckTxID.Text
		End If
        
        Try
            intErrNo = objINstp.mtdGetFilteredItemList(strOpCdItem_List_GET, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strparam, _
                                                       objINtx.EnumInventoryTransactionType.StockReturn, _
                                                       dsItemCodeDropList)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTOSERVTYPEDROPDOWNLIST&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
        End Try

        For intCnt = 0 To dsItemCodeDropList.Tables(0).Rows.Count - 1
            If Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("psEmpCode")) = "" Then
                strEMP = ""
            Else
                strEMP = Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("psEmpCode")) & " (" & Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("psEmpName")) & "), "
            End If
            dsItemCodeDropList.Tables(0).Rows(intCnt).Item(1) = strEMP & Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")) & " ( " _
                                                                & Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description")) & " ), " & _                                                                
                                                                objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Qty")), 5) & ", " & _
                                                                            Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode")) 
            dsItemCodeDropList.Tables(0).Rows(intCnt).Item(0) = Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("StockIssueLnID")) & "|" & _
                                                               Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("StockIssueID")) & "|" & _
                                                               Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Acccode")) & "|" & _
                                                               Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("BlkCode")) & "|" & _
                                                               Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Vehcode")) & "|" & _
                                                               Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("VehExpCode")) & "|" & _
                                                               Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PsEmpCode")) & "|" & _
                                                               Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")) & "|" & _
                                                               Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ChargeLocCode"))
        Next intCnt
        Dim drinsert As DataRow

        If lstStckIss.SelectedItem.Value.Trim = "" Then
            dsItemCodeDropList.Tables(0).Clear()
        End If

        drinsert = dsItemCodeDropList.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Select Item Code"
        dsItemCodeDropList.Tables(0).Rows.InsertAt(drinsert, 0)

        lstItem.DataSource = dsItemCodeDropList.Tables(0)
        lstItem.DataValueField = "ItemCode"
        lstItem.DataTextField = "Description"
        lstItem.DataBind()

        DisableItemTable()

        If Not dsItemCodeDropList Is Nothing Then
            dsItemCodeDropList = Nothing
        End If
    End Sub

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_STOCKRETURN_LINE_ADD"
        Dim strTxLnParam As New StringBuilder()
        Dim TxID As String
        Dim StrTxParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strAllowVehicle As String
        Dim PRID As String
        Dim arrIssueDet As Array
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

        arrIssueDet = Split(lstItem.SelectedItem.Value.Trim, "|")

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

        'If Month(strDate) < strAccMonth And Year(strDate) <= strAccYear Then
        '    lblDate.Visible = True
        '    lblDate.Text = "Invalid transaction date."
        '    Exit Sub
        'End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strNewIDFormat = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturn) & Right(strAccYear, 2) & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth)
        '    Case Else
        '        strNewIDFormat = "NRB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        'strNewIDFormat = "NRB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        strNewIDFormat = "SRN" & "/" & strCompany & "/" & strLocation & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & strAccYear & "/"

        If lblStckTxID.Text = "" Then
            StrTxParam.Append(lblStckTxID.Text.Trim)
            StrTxParam.Append("|||||||")
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
                intErrNo = objINtx.mtdUpdStockReturnDetail(strOpCdStckTxDet_ADD, _
                                                           strOpCdStckTxDet_UPD, _
                                                           strOpCdStckTxLine_GET, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           StrTxParam.ToString, _
                                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturn), _
                                                           ErrorChk, _
                                                           TxID)

                lblStckTxID.Text = TxID
                If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                    lblError.Visible = True
                End If

            Catch Exp As Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
                End If
            End Try

        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.noError And intErrNo = 0 Then

            strTxLnParam.Append(lblStckTxID.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(arrIssueDet(7))
            strTxLnParam.Append("|")
            strTxLnParam.Append(txtQty.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(arrIssueDet(0))
            strTxLnParam.Append("|")
            strTxLnParam.Append(lstStckIss.SelectedItem.Value)
            strTxLnParam.Append("|")
            strTxLnParam.Append(arrIssueDet(2))
            strTxLnParam.Append("|")
            strTxLnParam.Append(arrIssueDet(3))
            strTxLnParam.Append("|")
            strTxLnParam.Append(arrIssueDet(4))
            strTxLnParam.Append("|")
            strTxLnParam.Append(arrIssueDet(5))
            strTxLnParam.Append("|")
            strTxLnParam.Append(arrIssueDet(6))
            strTxLnParam.Append("|")
            strTxLnParam.Append(arrIssueDet(8))
            strTxLnParam.Append("|")
            strTxLnParam.Append(strLocation)
            strTxLnParam.Append("|")
            strTxLnParam.Append(Trim(txtAddNote.Value))

            Try
                intErrNo = objINtx.mtdAddStockReturnLn(strOpCdStckTxLine_ADD, _
                                                        strOpCdItem_Details_GET, _
                                                        strOpCdIssueLn_Det_GET, _
                                                        strOpCdIssue_Det_GET, _
                                                        strOpCdItem_Details_UPD, _
                                                        strOpCdIssueLn_Det_UPD, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnLn), _
                                                        ErrorChk, _
                                                        strTxLnParam.ToString)

            Catch Exp As Exception

                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
                End If

            End Try

            Select Case ErrorChk
                Case objINtx.EnumInventoryErrorType.OverFlow
                    lblError.Visible = True
                Case objINtx.EnumInventoryErrorType.InsufficientQty
                    lblStock.Visible = True
            End Select
        End If


        '''---------UPDATE STORAGE SELECT - SUPAYA TIDAK MENGUBAH DLL
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim strParamName As String
        Dim strParamValue As String

        sSQLKriteria = "UPDATE IN_STOCKRTNLN SET StorageCode='" & lstStorage.SelectedItem.Value & "' Where StockRtnId='" & lblStckTxID.Text & "'"

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
                intErrNo = objINtx.mtdUpdStockReturnDetail(strOpCdStckTxDet_ADD, _
                                                           strOpCdStckTxDet_UPD, _
                                                           strOpCdStckTxLine_GET, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           StrTxParam.ToString, _
                                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturn), _
                                                           ErrorChk, _
                                                           TxID)

                If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                    lblError.Visible = True
                End If

                lblStckTxID.Text = TxID
            Catch Exp As Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
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
        '        strNewIDFormat = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturn) & Right(strAccYear, 2) & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth)
        '    Case Else
        '        strNewIDFormat = "NRB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        strNewIDFormat = "NRB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        StrTxParam.Append(lblStckTxID.Text.Trim)
        StrTxParam.Append("|||||||")
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
            intErrNo = objINtx.mtdUpdStockReturnDetail(strOpCdStckTxDet_ADD, _
                                                       strOpCdStckTxDet_UPD, _
                                                       strOpCdStckTxLine_GET, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       StrTxParam.ToString, _
                                                       objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturn), _
                                                       ErrorChk, _
                                                       TxID)

            lblStckTxID.Text = TxID
            If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                lblError.Visible = True
            End If

        Catch Exp As Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
            End If
        End Try


        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        LoadStockTxDetails()
        DisplayFromDB()
        DisablePage()

    End Sub

    Sub btnCancel_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim TxID As String
        Dim StrTxParam As String
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

        Try
            intErrNo = objINtx.mtdReturnInvItemLevel(strOpCdStckTxLine_GET, _
                                                     strOpCdItem_Details_UPD, _
                                                     strOpCdIssueLn_Det_GET, _
                                                     strOpCdIssueLn_Det_UPD, _
                                                     strOpCdItem_Details_GET, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     lblStckTxID.Text, _
                                                     objINtx.EnumTransactionAction.Cancel, _
                                                     ErrorChk)

        Catch Exp As Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CONFIRMRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
            End If
        End Try
        If intErrNo = 0 And ErrorChk = objINtx.EnumInventoryErrorType.NoError Then
            Try
                StrTxParam = lblStckTxID.Text & "|||||||||||||" & objINtx.EnumStockReturnStatus.Cancelled & "|||"

                intErrNo = objINtx.mtdUpdStockReturnDetail(strOpCdStckTxDet_ADD, _
                                                           strOpCdStckTxDet_UPD, _
                                                           strOpCdStckTxLine_GET, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           StrTxParam, _
                                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturn), _
                                                           ErrorChk, _
                                                           TxID)

                If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                    lblError.Visible = True
                End If

                lblStckTxID.Text = TxID
            Catch Exp As Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
                End If
            End Try

        End If

        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|BIN|SIGN|UPDATEID"

        strParamValue = Trim(strLocation) & _
                        "|" & "IN_STOCKRECEIVE" & _
                        "|" & "IN_STOCKRECEIVELN" & _
                        "|" & "STOCKRECEIVEID" & _
                        "|" & Trim(lblStckTxID.Text) & _
                        "|" & ddlInventoryBin.SelectedItem.Value & _
                        "|" & "-" & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        LoadStockTxDetails()
        DisplayFromDB()
        DisablePage()
    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim colParam As New Collection
        Dim colOpCode As New Collection
        Dim intError As Integer
        Dim strErrMsg As String
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

        colOpCode.Add("IN_CLSTRX_STOCK_RETURN_GET_FOR_CONFIRM", "STOCK_RETURN_GET")
        colOpCode.Add("IN_CLSTRX_STOCKRETURN_DETAIL_UPD", "STOCK_RETURN_UPD")
        colOpCode.Add("IN_CLSTRX_STOCKISSUE_LINE_DETAILS_GET2", "STOCK_ISSUE_LINE_GET")
        colOpCode.Add("IN_CLSTRX_STOCKRETURN_LINE_SYN", "STOCK_RETURN_LINE_UPD")
        colOpCode.Add("IN_CLSTRX_STOCKITEM_DETAILS_GET", "STOCK_ITEM_GET")
        colOpCode.Add("IN_CLSTRX_STOCKITEM_DETAIL_UPD", "STOCK_ITEM_UPD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_DETAIL_ADD", "JOURNAL_ADD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_DETAIL_UPD", "JOURNAL_UPD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_LINE_ADD", "JOURNAL_LINE_ADD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_LINE_GET", "JOURNAL_LINE_GET")

        colParam.Add(strCompany, "COMPANY")
        colParam.Add(strLocation, "LOCCODE")
        colParam.Add(strUserId, "USER_ID")
        colParam.Add(lblStckTxID.Text, "STOCK_RETURN_ID")
        colParam.Add("Inter-" & GetCaption(objLangCap.EnumLangCap.Location), "MS_INTER_LOCATION")
        colParam.Add(GetCaption(objLangCap.EnumLangCap.Account), "MS_COA")
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), Session("SS_CONFIGSETTING")) = True Then
            colParam.Add(GetCaption(objLangCap.EnumLangCap.Block), "MS_BLOCK")
        Else
            colParam.Add(GetCaption(objLangCap.EnumLangCap.SubBlock), "MS_BLOCK")
        End If
        colParam.Add(GetCaption(objLangCap.EnumLangCap.Vehicle), "MS_VEHICLE")
        colParam.Add(GetCaption(objLangCap.EnumLangCap.VehExpense), "MS_VEHEXP")

        intError = objINtx.EnumTransactionError.NoError
        strErrMsg = ""

        Try
            intErrNo = objINtx.mtdStockReturn_Confirm(colOpCode, _
                                                      colParam, _
                                                      intError, _
                                                      strErrMsg)

            If intError = objINtx.EnumTransactionError.NoError Then
                LoadStockTxDetails()
                DisplayFromDB()
                DisablePage()
                BindGrid()
            Else
                lblConfirmErr.Text = strErrMsg
                lblConfirmErr.Visible = True
            End If
        Catch Exp As Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CONFIRMRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
            End If
        End Try

        If intError = objINtx.EnumTransactionError.NoError Then
            Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""

            strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

            strParamValue = Trim(strLocation) & _
                            "|" & "IN_STOCKRTN" & _
                            "|" & "IN_STOCKRTNLN" & _
                            "|" & "STOCKRTNID" & _
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
            End Try
        End If
    End Sub

    Sub btnPrint_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

        If lblPrintDate.Text = "" Then
            Try
                StrTxParam = lblStckTxID.Text & "||||||||||||" & Now() & "||"

                intErrNo = objINtx.mtdUpdStockReturnDetail(strOpCdStckTxDet_ADD, _
                                                           strOpCdStckTxDet_UPD, _
                                                           strOpCdStckTxLine_GET, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           StrTxParam, _
                                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturn), _
                                                           ErrorChk, _
                                                           TxID)

                If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                    lblError.Visible = True
                End If

                lblStckTxID.Text = TxID
            Catch Exp As Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
                End If
            End Try
        Else
            lblReprint.Visible = True
        End If

        LoadStockTxDetails()
        DisplayFromDB()
        DisablePage()

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

        If lblStatusHid.Text = CStr(objINtx.EnumStockReturnStatus.Deleted) Then
            Try

                intErrNo = objINtx.mtdReturnInvItemLevel(strOpCdStckTxLine_GET, _
                                                         strOpCdItem_Details_UPD, _
                                                         strOpCdIssueLn_Det_GET, _
                                                         strOpCdIssueLn_Det_UPD, _
                                                         strOpCdItem_Details_GET, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         lblStckTxID.Text, _
                                                         objINtx.EnumTransactionAction.Undelete, _
                                                         ErrorChk)
            Catch Exp As Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UNDELETESTOCKRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
                End If
            End Try
            StrTxParam = lblStckTxID.Text & "|||||||||||||" & objINtx.EnumStockReturnStatus.Active & "|"

        ElseIf lblStatusHid.Text = CStr(objINtx.EnumStockReturnStatus.Active) Then
            Try
                intErrNo = objINtx.mtdReturnInvItemLevel(strOpCdStckTxLine_GET, _
                                                         strOpCdItem_Details_UPD, _
                                                         strOpCdIssueLn_Det_GET, _
                                                         strOpCdIssueLn_Det_UPD, _
                                                         strOpCdItem_Details_GET, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         lblStckTxID.Text, _
                                                         objINtx.EnumTransactionAction.Delete, _
                                                         ErrorChk)
            Catch Exp As Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETENEWSTOCKRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
                End If
            End Try
            StrTxParam = lblStckTxID.Text & "|||||||||||||" & objINtx.EnumStockReturnStatus.Deleted & "|||"
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.noError And intErrNo = 0 Then
            Try
                intErrNo = objINtx.mtdUpdStockReturnDetail(strOpCdStckTxDet_ADD, _
                                                           strOpCdStckTxDet_UPD, _
                                                           strOpCdStckTxLine_GET, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           StrTxParam, _
                                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturn), _
                                                           ErrorChk, _
                                                           TxID)

                If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                    lblError.Visible = True
                End If

                lblStckTxID.Text = TxID
            Catch Exp As Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
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
        Dim strStockReturnId As String

        strStockReturnId = Trim(lblStckTxID.Text)
        strUpdString = "where StockRtnID = '" & strStockReturnId & "'"
        strStatus = Trim(Status.Text)
        intStatus = CInt(Trim(lblStatusHid.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strTable = "IN_STOCKRTN"
        strSortLine = ""


        If intStatus = objINtx.EnumStockReturnStatus.Confirmed Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                        strUpdString, _
                                                        strTable, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId)
                Catch Exp As Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_SHARE_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReturn_List.aspx")
                End Try
                LoadStockTxDetails()
                DisplayFromDB()
                DisablePage()
                BindGrid()
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockReturnDet.aspx?strStockReturnId=" & strStockReturnId & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       "&strAccountTag=" & strAccTag & _
                       "&strBlockTag=" & strBlkTag & _
                       "&strVehicleTag=" & strVehTag & _
                       "&strVehExpTag=" & strVehExpCodeTag & _
                       "&strDocTitle=" & lblDocTitle.Text & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_StockReturn_List.aspx")
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_Trx_StockReturn_Details.aspx")
    End Sub

    Function CheckLocBillParty () As Boolean
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_STOCKRETURN_LINE_GET"
        Dim strSearch As String
        Dim objBPLocDs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim intCnt As Integer
        Dim strLocCode As String
        Dim objStockIssueDS As New DataSet()

        strParam = Trim(lblStckTxID.Text)

        Try
            intErrNo = objINtx.mtdGetStockTransactDetails(strOpCdStckTxDet_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          objStockIssueDS)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_FUELISSUELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
        End Try

        For intCnt = 0 To objStockIssueDS.Tables(0).Rows.Count - 1
            strLocCode = TRIM(objStockIssueDS.Tables(0).Rows(intCnt).Item("ChargeLocCode"))

            If Not (strLocCode = "" Or strLocCode = TRIM(strLocation)) Then
                strSearch = " AND BP.Status = '" & objGLSetup.EnumBillPartyStatus.Active & "'" & _
                            " AND BP.InterLocCode = '" & strLocCode & "'" 
                    
                Try
                    intErrNo = objGLSetup.mtdGetBPInterLoc(strOpCd_Get, strSearch, objBPLocDs)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_SETUP_BILLPARTY_GET&errmesg=" & lblErrMessage.Text & "&redirect=BI/setup/BI_setup_BillPartyDet.aspx")
                End Try

                If objBPLocDs.Tables(0).Rows.Count <= 0 Then
                    lblLocCodeErr.Text = strLocCode
                    return False
                End If
            End If
        Next intCnt

        return True
    End Function

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
        Dim strText = "Please select Inventory Bin"

        'ddlInventoryBin.Items.Clear()
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
    End Sub

    Sub btnUpdate_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_STOCKRETURN_LINE_ADD"
        Dim strTxLnParam As New StringBuilder()
        Dim TxID As String
        Dim StrTxParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strAllowVehicle As String
        Dim PRID As String
        Dim arrIssueDet As Array
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

        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_STOCKRETURN_LINE_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "UPDATESTR"
        strParamValue = "SET AdditionalNote = '" & Trim(txtAddNote.Value) & "' WHERE StockRtnID = '" & Trim(lblStckTxID.Text) & "' AND StockRtnLNID = '" & Trim(lblTxLnID.Text) & "' "

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        End Try

        LoadStockTxDetails()
        DisplayFromDB()
        BindItemCodeList()
        DisablePage()
        txtQty.Text = ""
        BindGrid()
    End Sub

End Class
