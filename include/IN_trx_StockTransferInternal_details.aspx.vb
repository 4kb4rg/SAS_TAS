
Imports System
Imports System.Data
Imports System.Math
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Public Class IN_StockTransferInternal : Inherits Page

    Protected WithEvents dgStkTx As DataGrid
    Protected WithEvents lblStckTxID As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblReprint As Label
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtRemarks As TextBox
    Protected WithEvents txtQty As TextBox
    Protected WithEvents lblPDateTag As Label
    Protected WithEvents lblDNIDTag As Label
    Protected WithEvents tblAdd As HtmlTable
    Protected WithEvents validateQty As RequiredFieldValidator
    Protected WithEvents lblError As Label
    Protected WithEvents lblStock As Label
    Protected WithEvents lblUnDel As Label
    Protected WithEvents Add As ImageButton
    Protected WithEvents PRDelete As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Confirm As ImageButton
    Protected WithEvents Print As ImageButton
    Protected WithEvents Cancel As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents lblConfirmErr As Label
    Protected WithEvents lblStatusHid As Label
    Protected WithEvents lblItemCodeErr As Label
    Protected WithEvents lstToLoc As DropDownList
    Protected WithEvents lstItem As DropDownList
    Protected WithEvents FindIN As HtmlInputButton
    Protected WithEvents lblTo As Label
    Protected WithEvents lblPleaseSelectOne As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblDocTitle As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents ddlInventoryBin1 As DropDownList
    Protected WithEvents ddlInventoryBin2 As DropDownList
    Protected WithEvents txtAddNote As TextBox
    Protected WithEvents lblInventoryBin1 As Label
    Protected WithEvents lblInventoryBin2 As Label


    Protected objINtx As New agri.IN.clstrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Public objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objGLtrx As New agri.GL.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmin As New agri.Admin.clsShare()

    Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_LINE_GET"
    Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
    Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"

    Const ITEM_PART_SEPERATOR As String = " @ "

    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New Dataset()
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
    Dim strARAccMonth As String
    Dim strARAccYear As String
    Dim intINAR As Integer
    Dim intConfigsetting As Integer
    Dim strLocationTag As String

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strARAccMonth = Session("SS_ARACCMONTH")
        strARAccYear = Session("SS_ARACCYEAR")
        intINAR = Session("SS_INAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransferInternal), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                lblStckTxID.Text = Request.QueryString("Id")
                BindInventoryBinLevelFrom("")
                BindInventoryBinLevelTo("")
                LoadStockTxDetails()
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    DisplayFromDB(False)
                    DisablePage()
                    BindGrid()
                End If
                BindItemCodeList()
            End If
            DisablePage()

            lblError.Visible = False
            lblStock.Visible = False
            lblUnDel.Visible = False
            lblConfirmErr.Visible = False
            lblItemCodeErr.Visible = False
            lblInventoryBin1.Visible = False
            lblInventoryBin2.Visible = False

        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        strLocationTag = GetCaption(objLangCap.EnumLangCap.Location)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STKTRANSFER_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKTRANSFERINTERNAL_List.aspx")
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
        txtDesc.Enabled = False
        txtRemarks.Enabled = False
        validateQty.Visible = False
        Save.Visible = False
        Confirm.Visible = False
        Print.Visible = False
        Cancel.Visible = False
        PRDelete.Visible = False
        btnNew.Visible = False
        ddlInventoryBin1.Enabled = False
        ddlInventoryBin2.Enabled = False

        Select Case Trim(lblStatusHid.Text)
            Case Trim(CStr(objINtx.EnumStockTransferInternalStatus.Deleted))
                PRDelete.Visible = True
                btnNew.Visible = True
                PRDelete.ImageUrl = "../../images/butt_undelete.gif"
                PRDelete.AlternateText = "Undelete"
                PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objINtx.EnumStockTransferInternalStatus.Confirmed))
                btnNew.Visible = True
                Print.Visible = True
                Cancel.Visible = True
            Case Trim(CStr(objINtx.EnumStockTransferInternalStatus.Cancelled))
                btnNew.Visible = True
            Case Else
                txtDesc.Enabled = True
                txtRemarks.Enabled = True
                validateQty.Visible = True
                Save.Visible = True
                ddlInventoryBin1.Enabled = True
                ddlInventoryBin2.Enabled = True
                If Trim(lblStckTxID.Text) <> "" Then
                    Confirm.Visible = True
                    Print.Visible = True
                    PRDelete.Visible = True
                    btnNew.Visible = True
                    PRDelete.ImageUrl = "../../images/butt_delete.gif"
                    PRDelete.AlternateText = "Delete"
                    PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                End If
        End Select
        DisableItemTable()
    End Sub

    Sub DisableItemTable()
        If lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.Deleted) _
        Or lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.Cancelled) Then
            tblAdd.Visible = False
        ElseIf lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.Confirmed) Then
            tblAdd.Visible = False
        Else
            tblAdd.Visible = True
        End If
    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim DeleteButton As LinkButton

        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem

                Select Case Trim(Status.Text)
                    Case objINtx.mtdGetStockTransferInternalStatus(objINtx.EnumStockTransferStatus.Active)
                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = True
                        DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objINtx.mtdGetStockTransferInternalStatus(objINtx.EnumStockTransferStatus.Confirmed), _
                         objINtx.mtdGetStockTransferInternalStatus(objINtx.EnumStockTransferStatus.Deleted), _
                         objINtx.mtdGetStockTransferInternalStatus(objINtx.EnumStockTransferStatus.Cancelled)

                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = False

                End Select

        End Select
    End Sub

    Sub DisplayFromDB(ByVal pv_blnIsRedirect As Boolean)
        Status.Text = objINtx.mtdGetStockTransferInternalStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
        lblStatusHid.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Status"))
        CreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("UserName"))
        txtDesc.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Description"))
        txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))

        If Not objGlobal.mtdEmptyDate(objDataSet.Tables(0).Rows(0).Item("PrintDate")) Then
            lblPDateTag.Visible = True
            lblPrintDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("PrintDate")))
        End If
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer

        dgStkTx.DataSource = LoadDataGrid()
        dgStkTx.DataBind()
    End Sub

    Function CheckRequiredField() As Boolean
        If Request.Form("lstItem") = "" Then
            lblItemCodeErr.Visible = True
            Return True
        Else
            Return False
        End If
    End Function

    Protected Function LoadDataGrid() As DataSet
        Dim strOpCode As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_LINE_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "STOCKTRANSFERID|LOCCODE"
        strParamValue = Trim(lblStckTxID.Text) & "|" & strLocation

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsGrid)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFERINTERNAL&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKTRANSFERINTERNAL_List.aspx")
        End Try

        Return dsGrid
    End Function

    Sub LoadStockTxDetails()
        Dim strOpCode As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_DETAIL_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "STOCKTRANSFERID|LOCCODE"
        strParamValue = Trim(lblStckTxID.Text) & "|" & strLocation

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFERINTERNALLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKTRANSFERINTERNAL_List.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            BindInventoryBinLevelFrom(Trim(objDataSet.Tables(0).Rows(0).Item("BinFrom")))
            BindInventoryBinLevelTo(Trim(objDataSet.Tables(0).Rows(0).Item("BinTo")))
            txtDesc.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Description"))
        End If
    End Sub

    Sub BindItemCodeList()
        Dim strOpCdItem_List_GET = "IN_CLSTRX_ITEMPART_ITEM_GET"
        Dim dsItemCodeDropList As DataSet
        Dim intCnt As Integer
        Dim strparam As String
        Dim drinsert As DataRow

        strparam = objINstp.EnumInventoryItemType.Stock & "','" & objINstp.EnumInventoryItemType.WorkshopItem & "|" & objINstp.EnumStockItemStatus.Active & "|" & lblStckTxID.Text & "|" & "itm.ItemCode"
        Try
            intErrNo = objINstp.mtdGetFilteredItemList(strOpCdItem_List_GET, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strparam, _
                                                       objINtx.EnumInventoryTransactionType.StockTransfer, _
                                                       dsItemCodeDropList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTOSERVTYPEDROPDOWNLIST&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKTRANSFERINTERNAL_List.aspx")
        End Try

        For intCnt = 0 To dsItemCodeDropList.Tables(0).Rows.Count - 1
            If dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then
                dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") & " @ " & _
                                                                                dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo") & " ( " & _
                                                                                dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
                                                                                "Rp. " & objGlobal.GetIDDecimalSeparator(Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("AverageCost"))) & ", " & _
                                                                                objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand")), 5) & ", " & _
                                                                            Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode"))

                dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") & _
                                                                              ITEM_PART_SEPERATOR & dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo").Trim()
            Else
                dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") & " ( " & _
                                                                                dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
                                                                                "Rp. " & objGlobal.GetIDDecimalSeparator(Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("AverageCost"))) & ", " & _
                                                                                objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand")), 5) & ", " & _
                                                                            Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode"))
                dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")
            End If
        Next intCnt

        drinsert = dsItemCodeDropList.Tables(0).NewRow()
        drinsert("ItemCode") = ""
        drinsert("Description") = "Select Item Code"
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

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lblItem As Label
        Dim ItemCode As String
        Dim lblAddNote As Label
        Dim AddNote As String

        Dim strOpCode As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_LINE_DEL"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        If lblStatusHid.Text = CStr(objINtx.EnumStockTransferInternalStatus.Active) Then

            lblItem = E.Item.FindControl("ItemCode")
            ItemCode = lblItem.Text

            lblAddNote = E.Item.FindControl("AddNote")
            AddNote = lblAddNote.Text

            strParamName = "STOCKTRANSFERID|ITEMCODE|ADDITIONALNOTE"

            strParamValue = Trim(lblStckTxID.Text) & "|" & ItemCode & "|" & AddNote

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
            End Try

            LoadStockTxDetails()
            DisplayFromDB(False)
            BindGrid()
            BindItemCodeList()
        End If
    End Sub

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCode As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_DETAIL_ADD"
        Dim strOpCodeLn As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_LINE_ADD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim pr_strID As String

        If ddlInventoryBin1.SelectedItem.Value = 0 Then
            lblInventoryBin1.Visible = True
            Exit Sub
        End If

        If ddlInventoryBin2.SelectedItem.Value = 0 Then
            lblInventoryBin2.Visible = True
            Exit Sub
        End If

        If CheckRequiredField() Then
            Exit Sub
        End If

        If lblStckTxID.Text = "" Then
            UpdateDataMaster()
        End If

        strParamName = "STOCKTRANSFERID|ITEMCODE|QTY|ADDITIONALNOTE"

        strParamValue = Trim(lblStckTxID.Text) & "|" & lstItem.SelectedItem.Value & _
                        "|" & txtQty.Text & "|" & Trim(txtAddNote.Text)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCodeLn, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try


        LoadStockTxDetails()
        DisplayFromDB(False)
        BindGrid()
        BindItemCodeList()
        DisablePage()
        txtQty.Text = ""
    End Sub

    Sub UpdateDataMaster()
        Dim strOpCode_GetID As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_GET_ID"
        Dim strOpCode_UpdID As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_UPD_ID"
        Dim strOpCode_Add As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_DETAIL_ADD"
        Dim strOpCode_Upd As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim pr_strID As String
        Dim pr_ID As String


        If lblStckTxID.Text = "" Then
            strParamName = "TRAN_PREFIX"
            strParamValue = Trim(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransferInternal))

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCode_GetID, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objDataSet)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ID&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKTRANSFERINTERNAL_List.aspx")
            End Try


            pr_ID = Trim(objDataSet.Tables(0).Rows(0).Item("Val") + 1)
            pr_strID = strParamValue & Mid(Year(Now()), 3, 2) & Format(objDataSet.Tables(0).Rows(0).Item("Val") + 1, New String("0", objDataSet.Tables(0).Rows(0).Item("Length")))

            strParamName = "TRAN_PREFIX|VAL_ID"
            strParamValue = Trim(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransferInternal)) & "|" & pr_ID

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_UpdID, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_TBID&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
            End Try

            strParamName = "STOCKTRANSFERID|DESCRIPTION|LOCCODE|BINFROM|BINTO|ACCMONTH|ACCYEAR|REMARK|STATUS|UPDATEID"

            strParamValue = Trim(pr_strID) & "|" & Trim(txtDesc.Text) & _
                            "|" & strLocation & "|" & ddlInventoryBin1.SelectedItem.Value & _
                            "|" & ddlInventoryBin2.SelectedItem.Value & _
                            "|" & strAccMonth & "|" & strAccYear & "|" & Trim(txtRemarks.Text) & _
                            "|" & objINtx.EnumStockTransferInternalStatus.Active & "|" & strUserId

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_Add, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
            End Try

            lblStckTxID.Text = pr_strID
        Else
            strParamName = "STOCKTRANSFERID|DESCRIPTION|BINFROM|BINTO|REMARK|STATUS|UPDATEID"

            strParamValue = Trim(lblStckTxID.Text) & "|" & Trim(txtDesc.Text) & _
                            "|" & ddlInventoryBin1.SelectedItem.Value & "|" & ddlInventoryBin2.SelectedItem.Value & _
                            "|" & Trim(txtRemarks.Text) & "|" & objINtx.EnumStockTransferInternalStatus.Active & "|" & strUserId

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_Upd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
            End Try
        End If
    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If ddlInventoryBin1.SelectedItem.Value = 0 Then
            lblInventoryBin1.Visible = True
            Exit Sub
        End If

        If ddlInventoryBin2.SelectedItem.Value = 0 Then
            lblInventoryBin2.Visible = True
            Exit Sub
        End If

        UpdateDataMaster()

        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCode As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_DETAIL_UPD"
        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_ITEM_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        If ddlInventoryBin1.SelectedItem.Value = 0 Then
            lblInventoryBin1.Visible = True
            Exit Sub
        End If

        If ddlInventoryBin2.SelectedItem.Value = 0 Then
            lblInventoryBin2.Visible = True
            Exit Sub
        End If

        strParamName = "LOCCODE|STOCKTRANSFERID|DESCRIPTION|BINFROM|BINTO|REMARK|STATUS|UPDATEID"

        strParamValue = strLocation & "|" & Trim(lblStckTxID.Text) & "|" & Trim(txtDesc.Text) & _
                        "|" & ddlInventoryBin1.SelectedItem.Value & "|" & ddlInventoryBin2.SelectedItem.Value & _
                        "|" & Trim(txtRemarks.Text) & "|" & objINtx.EnumStockTransferInternalStatus.Confirmed & "|" & strUserId

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=in/trx/IN_trx_stocktransferinternal_list")
        End Try

        strParamName = "LOCCODE|STOCKTRANSFERID|BINFROM|BINTO|SIGNFROM|SIGNTO|UPDATEID"

        strParamValue = Trim(strLocation) & _
                        "|" & Trim(lblStckTxID.Text) & _
                        "|" & ddlInventoryBin1.SelectedItem.Value & _
                        "|" & ddlInventoryBin2.SelectedItem.Value & _
                        "|" & "-" & _
                        "|" & "+" & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=in/trx/IN_trx_stocktransferinternal_list")
        End Try

        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
        BindGrid()
    End Sub

    Sub btnCancel_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCode As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_DETAIL_UPD"
        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_ITEM_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        If ddlInventoryBin1.SelectedItem.Value = 0 Then
            lblInventoryBin1.Visible = True
            Exit Sub
        End If

        If ddlInventoryBin2.SelectedItem.Value = 0 Then
            lblInventoryBin2.Visible = True
            Exit Sub
        End If

        strParamName = "LOCCODE|STOCKTRANSFERID|DESCRIPTION|BINFROM|BINTO|REMARK|STATUS|UPDATEID"

        strParamValue = strLocation & "|" & Trim(lblStckTxID.Text) & "|" & Trim(txtDesc.Text) & _
                        "|" & ddlInventoryBin1.SelectedItem.Value & "|" & ddlInventoryBin2.SelectedItem.Value & _
                        "|" & Trim(txtRemarks.Text) & "|" & objINtx.EnumStockTransferInternalStatus.Cancelled & "|" & strUserId

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        strParamName = "LOCCODE|STOCKTRANSFERID|BINFROM|BINTO|SIGNFROM|SIGNTO|UPDATEID"

        strParamValue = Trim(strLocation) & "|" & Trim(lblStckTxID.Text) & _
                        "|" & ddlInventoryBin1.SelectedItem.Value & "|" & ddlInventoryBin2.SelectedItem.Value & _
                        "|" & "+" & "|" & "-" & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
        BindGrid()
    End Sub

    Sub btnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCode As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|STOCKTRANSFERID|DESCRIPTION|BINFROM|BINTO|REMARK|STATUS|UPDATEID"

        If lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.Deleted) Then
            strParamValue = strLocation & "|" & Trim(lblStckTxID.Text) & "|" & Trim(txtDesc.Text) & _
                            "|" & ddlInventoryBin1.SelectedItem.Value & "|" & ddlInventoryBin2.SelectedItem.Value & _
                            "|" & Trim(txtRemarks.Text) & "|" & objINtx.EnumStockTransferInternalStatus.Active & "|" & strUserId
        Else
            strParamValue = strLocation & "|" & Trim(lblStckTxID.Text) & "|" & Trim(txtDesc.Text) & _
                            "|" & ddlInventoryBin1.SelectedItem.Value & "|" & ddlInventoryBin2.SelectedItem.Value & _
                            "|" & Trim(txtRemarks.Text) & "|" & objINtx.EnumStockTransferInternalStatus.Deleted & "|" & strUserId
        End If

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        LoadStockTxDetails()
        DisplayFromDB(False)
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

        strUpdString = "where StockTransferID = '" & strStockTxId & "'"
        strStatus = Trim(Status.Text)
        intStatus = CInt(Trim(lblStatusHid.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strTable = "IN_STOCKTRANSFERINTERNAL"
        strSortLine = ""


        If intStatus = objINtx.EnumStockTransferStatus.Confirmed Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                        strUpdString, _
                                                        strTable, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_SHARE_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKTRANSFERINTERNAL_List.aspx")
                End Try
                LoadStockTxDetails()
                DisplayFromDB(False)
                DisablePage()
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_STOCKTRANSFERINTERNALDet.aspx?strStockTxId=" & strStockTxId & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       "&strDocTitle=" & lblDocTitle.Text & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_STOCKTRANSFERINTERNAL_List.aspx")
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_Trx_STOCKTRANSFERINTERNAL_Details.aspx")
    End Sub

    Sub BindInventoryBinLevelFrom(ByVal pv_strInvBin As String)
        Dim strText = "Please select Inventory Bin"

        'ddlInventoryBin1.Items.Clear()
        ddlInventoryBin1.Items.Add(New ListItem(strText, "0"))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO), objINstp.EnumInventoryBinLevel.HO))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central), objINstp.EnumInventoryBinLevel.Central))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Other), objINstp.EnumInventoryBinLevel.Other))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinI), objINstp.EnumInventoryBinLevel.BinI))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinII), objINstp.EnumInventoryBinLevel.BinII))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIII), objINstp.EnumInventoryBinLevel.BinIII))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIV), objINstp.EnumInventoryBinLevel.BinIV))

        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin1
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        Else
            ddlInventoryBin1.SelectedIndex = -1
        End If
    End Sub

    Sub BindInventoryBinLevelTo(ByVal pv_strInvBin As String)
        Dim strText = "Please select Inventory Bin"

        'ddlInventoryBin2.Items.Clear()
        ddlInventoryBin2.Items.Add(New ListItem(strText, "0"))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO), objINstp.EnumInventoryBinLevel.HO))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central), objINstp.EnumInventoryBinLevel.Central))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Other), objINstp.EnumInventoryBinLevel.Other))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinI), objINstp.EnumInventoryBinLevel.BinI))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinII), objINstp.EnumInventoryBinLevel.BinII))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIII), objINstp.EnumInventoryBinLevel.BinIII))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIV), objINstp.EnumInventoryBinLevel.BinIV))

        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin2
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        Else
            ddlInventoryBin2.SelectedIndex = -1
        End If
    End Sub
End Class
