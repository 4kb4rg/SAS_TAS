
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

Public Class IN_StockMovement : Inherits Page

    Protected WithEvents dgStkTx As DataGrid
    Protected WithEvents lblStckTxID As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblDNNoteID As Label
    Protected WithEvents lblTotAmtFig As Label
    Protected WithEvents ToLocTag As Label
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
    'Protected WithEvents DebitNote As ImageButton
    Protected WithEvents Cancel As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents lblConfirmErr As Label
    Protected WithEvents lblStatusHid As Label
    'Protected WithEvents lblToLocErr As Label
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

    Protected objINtx As New agri.IN.clstrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objGLtrx As New agri.GL.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmin As New agri.Admin.clsShare()

    Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_STOCKMOVEMENT_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_STOCKMOVEMENT_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_STOCKMOVEMENT_LINE_GET"
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                lblStckTxID.Text = Request.QueryString("Id")
                LoadStockTxDetails()

                If objDataSet.Tables(0).Rows.Count > 0 Then
                    DisplayFromDB(False)
                    DisablePage()
                    BindGrid()
                End If
                BindItemCodeList()
                'BindLocDropList()
                BindInventoryBinLevel("")
            End If
            DisablePage()

            lblError.Visible = False
            lblStock.Visible = False
            lblUnDel.Visible = False
            lblConfirmErr.Visible = False
            'lblToLocErr.Visible = False
            lblItemCodeErr.Visible = False
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        'ToLocTag.Text = lblTo.Text & GetCaption(objLangCap.EnumLangCap.Location) & " :*"
        strLocationTag = GetCaption(objLangCap.EnumLangCap.Location)
        'lblToLocErr.Text = lblPleaseSelectOne.Text & strLocationTag & lblCode.Text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STKTRANSFER_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
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
        'lstToLoc.Enabled = False
        validateQty.Visible = False
        Save.Visible = False
        Confirm.Visible = False
        Print.Visible = False
        'DebitNote.Visible = False
        Cancel.Visible = False
        PRDelete.Visible = False
        btnNew.Visible = False
        Select Case Trim(lblStatusHid.Text)
            Case Trim(CStr(objINtx.EnumStockTransferStatus.Deleted))
                PRDelete.Visible = True
                btnNew.Visible = True
                PRDelete.ImageUrl = "../../images/butt_undelete.gif"
                PRDelete.AlternateText = "Undelete"
                PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objINtx.EnumStockTransferStatus.Confirmed))
                btnNew.Visible = True
                'DebitNote.Visible = True
                Print.Visible = True
            Case Trim(CStr(objINtx.EnumStockTransferStatus.DbNote))
                btnNew.Visible = True
                Print.Visible = True
            Case Else
                txtDesc.Enabled = True
                txtRemarks.Enabled = True
                'lstToLoc.Enabled = True
                validateQty.Visible = True
                Save.Visible = True
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
        Or lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.Cancelled) _
        Or lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.DbNote) Then
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
                    Case objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.Active)
                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = True
                        DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.Confirmed), _
                         objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.Deleted), _
                         objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.DbNote), _
                         objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.Cancelled)

                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = False

                End Select

        End Select




    End Sub

    Sub DisplayFromDB(ByVal pv_blnIsRedirect As Boolean)
        Status.Text = objINtx.mtdGetStockTransferStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
        lblStatusHid.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Status"))
        CreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("UserName"))
        txtDesc.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Description"))
        txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))
        lblTotAmtFig.Text = objGlobal.GetIDDecimalSeparator(Round(objDataSet.Tables(0).Rows(0).Item("TotalAmount"), 0))
        If Not objGlobal.mtdEmptyDate(objDataSet.Tables(0).Rows(0).Item("PrintDate")) Then
            lblPDateTag.Visible = True
            lblPrintDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("PrintDate")))
        End If

        If Not Trim(objDataSet.Tables(0).Rows(0).Item("DNID")) = "" Then
            lblDNIDTag.Visible = True
            lblDNNoteID.Text = Trim(objDataSet.Tables(0).Rows(0).Item("DNID"))
            If pv_blnIsRedirect = True Then
                Response.Redirect("../../BI/trx/BI_trx_DNDet.aspx?dbnid=" & lblDNNoteID.Text & "&referer=" & Request.ServerVariables("SCRIPT_NAME") & "?Id=" & lblStckTxID.Text)
            End If
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

    'Function CheckLocField() As Boolean
    '    If lstToLoc.SelectedItem.Value = "" Then
    '        lblToLocErr.Visible = True
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Protected Function LoadDataGrid() As DataSet

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
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKMOVEMENT&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
        End Try
        Return dsGrid
    End Function

    Sub LoadStockTxDetails()
        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_STOCKMOVEMENT_DETAIL_GET"
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKMOVEMENTLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
        End Try

    End Sub

    'Sub BindLocDropList()
    '    Dim strOpCdLocList_GET As String = "ADMIN_CLSLOC_SYSLOC_LIST_GET"
    '    Dim dsForDropDown As DataSet
    '    Dim dsForInactiveItem As DataSet
    '    Dim SelectedIndex As Integer = -1
    '    Dim intCnt As Integer
    '    Dim strParam As String
    '    Dim strFieldCheck As String
    '    Dim drinsert As DataRow
    '    Try
    '        strParam = "And SY.CompCode = '" & strCompany & "' AND LO.Status = " & objAdminLoc.EnumLocStatus.Active & " AND Not SY.LocCode = '" & strLocation & "'|"
    '        intErrNo = objAdminLoc.mtdGetCompLocList(strOpCdLocList_GET, strCompany, strLocation, strUserId, dsForDropDown, strParam)

    '    Catch Exp As System.Exception
    '        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
    '    End Try

    '    For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
    '        strFieldCheck = dsForDropDown.Tables(0).Rows(intCnt).Item(0)
    '        dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
    '        dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
    '                                                       Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
    '        If Not lblStckTxID.Text = "" Then
    '            If strFieldCheck.Trim.ToUpper = UCase(Trim(objDataSet.Tables(0).Rows(0).Item("ToLocCode"))) Then
    '                SelectedIndex = intCnt + 1

    '            End If
    '        End If
    '    Next intCnt
    '    drinsert = dsForDropDown.Tables(0).NewRow()
    '    drinsert(0) = ""
    '    drinsert(1) = lblSelect.Text & strLocationTag & lblCode.Text
    '    dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

    '    lstToLoc.DataSource = dsForDropDown.Tables(0)
    '    lstToLoc.DataValueField = "LocCode"
    '    lstToLoc.DataTextField = "LocDesc"
    '    lstToLoc.DataBind()

    '    If SelectedIndex = -1 And Not lblStckTxID.Text = "" Then

    '        Try
    '            strParam = "And Sy.CompCode = '" & strCompany & "' AND sy.Loccode = '" & _
    '                            Trim(objDataSet.Tables(0).Rows(0).Item("ToLocCode")) & "'|"
    '            intErrNo = objAdminLoc.mtdGetCompLocList(strOpCdLocList_GET, strCompany, strLocation, strUserId, dsForInactiveItem, strParam)

    '        Catch Exp As System.Exception
    '            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMNOTFOUND&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
    '        End Try

    '        If dsForInactiveItem.Tables(0).Rows.Count > 0 Then
    '            lstToLoc.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0)) & _
    '            " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.NotActive) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0))))
    '            SelectedIndex = lstToLoc.Items.Count - 1
    '        Else
    '            lstToLoc.Items.Add(New ListItem(Trim(objDataSet.Tables(0).Rows(0).Item("ToLocCode")) & _
    '                " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Unknown) & ") ", Trim(objDataSet.Tables(0).Rows(0).Item("ToLocCode"))))
    '            SelectedIndex = 0
    '        End If
    '    End If

    '    lstToLoc.SelectedIndex = SelectedIndex
    '    If Not dsForDropDown Is Nothing Then
    '        dsForDropDown = Nothing
    '    End If
    '    If Not dsForInactiveItem Is Nothing Then
    '        dsForInactiveItem = Nothing
    '    End If
    'End Sub

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTOSERVTYPEDROPDOWNLIST&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
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
        If lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.Active) Then
            Dim lbl As Label
            Dim ItemCode As String
            Dim strParam As String
            Dim StrTxParam As String
            Dim TxID As String
            Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
            Dim strOpCdStckTxLine_DEL As String = "IN_CLSTRX_STOCKMOVEMENT_LINE_DEL"
            Dim strOpCdStckTxLine_Det_GET As String = "IN_CLSTRX_STOCKMOVEMENTLINE_DETAILS_GET"

            lbl = E.Item.FindControl("ItemCode")
            ItemCode = lbl.Text

            strParam = lblStckTxID.Text & "|" & ItemCode
            Try
                intErrNo = objINtx.mtdDelStockTransferLn(strOpCdStckTxLine_DEL, _
                                                        strOpCdStckTxLine_Det_GET, _
                                                        strOpCdItem_Details_UPD, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_STOCKMOVEMENTLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End If
            End Try
            StrTxParam = lblStckTxID.Text & "|||||||||||||||"

            Try
                intErrNo = objINtx.mtdUpdStockTransferDetail(strOpCdStckTxDet_ADD, _
                                                             strOpCdStckTxDet_UPD, _
                                                             strOpCdStckTxLine_GET, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strARAccMonth, _
                                                             strARAccYear, _
                                                             StrTxParam, _
                                                             objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransfer), _
                                                             ErrorChk, _
                                                             TxID)
                If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                    lblError.Visible = True
                End If

                lblStckTxID.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End If
            End Try
            LoadStockTxDetails()
            DisplayFromDB(False)
            BindGrid()
            BindItemCodeList()
        End If
    End Sub

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_STOCKMOVEMENT_LINE_ADD"
        Dim strTxLnParam As String
        Dim TxID As String
        Dim StrTxParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim arrPartNo As Array
        Dim strItemCode As String

        If CheckRequiredField() Then
            Exit Sub
        End If

        'If CheckLocField() Then
        '    Exit Sub
        'End If

        If lblStckTxID.Text = "" Then
            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("||||||")
            StrTxParam.Append(lblDNNoteID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(txtDesc.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|")
            StrTxParam.Append(lstToLoc.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|0|")
            StrTxParam.Append(txtRemarks.Text)
            StrTxParam.Append("||")
            Try
                intErrNo = objINtx.mtdUpdStockTransferDetail(strOpCdStckTxDet_ADD, _
                                                             strOpCdStckTxDet_UPD, _
                                                             strOpCdStckTxLine_GET, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strARAccMonth, _
                                                             strARAccYear, _
                                                             StrTxParam.ToString, _
                                                             objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransfer), _
                                                             ErrorChk, _
                                                             TxID)
                lblStckTxID.Text = TxID
                If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                    lblError.Visible = True
                End If

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End If
            End Try

        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.NoError And intErrNo = 0 Then
            arrPartNo = Split(Request.Form("lstItem"), " @ ")
            If arrPartNo.GetUpperBound(0) = 1 Then
                strItemCode = arrPartNo(0)
            Else
                strItemCode = Request.Form("lstItem")
            End If

            strTxLnParam = lblStckTxID.Text & "|" & strItemCode.Trim & "|" & txtQty.Text
            Try
                intErrNo = objINtx.mtdAddStockTransferLn(strOpCdStckTxLine_ADD, _
                                                        strOpCdItem_Details_GET, _
                                                        strOpCdItem_Details_UPD, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        ErrorChk, _
                                                        strTxLnParam)
                Select Case ErrorChk
                    Case objINtx.EnumInventoryErrorType.Overflow
                        lblError.Visible = True
                    Case objINtx.EnumInventoryErrorType.InsufficientQty
                        lblStock.Visible = True
                End Select

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End If
            End Try

        End If

        StrTxParam.Remove(0, StrTxParam.Length)
        StrTxParam.Append(lblStckTxID.Text)
        StrTxParam.Append("|||||||||||||||")

        If ErrorChk = objINtx.EnumInventoryErrorType.NoError And intErrNo = 0 Then
            Try
                intErrNo = objINtx.mtdUpdStockTransferDetail(strOpCdStckTxDet_ADD, _
                                                                strOpCdStckTxDet_UPD, _
                                                                strOpCdStckTxLine_GET, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strAccMonth, _
                                                                strAccYear, _
                                                                strARAccMonth, _
                                                                strARAccYear, _
                                                                StrTxParam.ToString, _
                                                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransfer), _
                                                                ErrorChk, _
                                                            TxID)
                If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                    lblError.Visible = True
                End If

                lblStckTxID.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End If
            End Try

        End If

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        LoadStockTxDetails()
        DisplayFromDB(False)
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

        'If CheckLocField() Then
        '    Exit Sub
        'End If

        If lblStckTxID.Text = "" Then
            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("||||||")
            StrTxParam.Append(lblDNNoteID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(txtDesc.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|")
            StrTxParam.Append(lstToLoc.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|0|")
            StrTxParam.Append(txtRemarks.Text)
            StrTxParam.Append("||")
        Else

            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("|||||||")
            StrTxParam.Append(txtDesc.Text)
            StrTxParam.Append("||")
            StrTxParam.Append(lstToLoc.SelectedItem.Value)
            StrTxParam.Append("||||")
            StrTxParam.Append(txtRemarks.Text)
            StrTxParam.Append("||")
        End If

        Try
            intErrNo = objINtx.mtdUpdStockTransferDetail(strOpCdStckTxDet_ADD, _
                                                         strOpCdStckTxDet_UPD, _
                                                         strOpCdStckTxLine_GET, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         strARAccMonth, _
                                                         strARAccYear, _
                                                         StrTxParam.ToString, _
                                                         objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransfer), _
                                                         ErrorChk, _
                                                         TxID)
            lblStckTxID.Text = TxID
            If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                lblError.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
            End If
        End Try

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

        Dim strOpCodeGetAllLines As String = "IN_CLSTRX_STOCKMOVEMENT_LINE_GET_ALL"
        Dim strOpCodeUpdLine As String = "IN_CLSTRX_STOCKMOVEMENT_LINE_SYN"
        Dim strOpCodeGetItemDetail As String = "IN_CLSTRX_ITEM_DETAIL_GET"

        If Not LoadDataGrid.Tables(0).Rows.Count > 0 Then
            lblConfirmErr.Visible = True
            Exit Sub
        End If

        StrTxParam = lblStckTxID.Text
        Try
            intErrNo = objINtx.mtdSynStockTransferLn(strOpCodeGetAllLines, _
                                                     strOpCodeUpdLine, _
                                                     strOpCodeGetItemDetail, _
                                                     strOpCdItem_Details_UPD, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     ErrorChk, _
                                                     StrTxParam)
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=REMOVEINVENTORYITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
            End If
        End Try

        Select Case ErrorChk
            Case objINtx.EnumInventoryErrorType.Overflow
                lblError.Visible = True
                Exit Sub
            Case objINtx.EnumInventoryErrorType.InsufficientQty
                lblStock.Visible = True
                Exit Sub
            Case Else
                lblError.Visible = False
                lblStock.Visible = False
        End Select

        StrTxParam = lblStckTxID.Text & "|||||||||||||" & txtRemarks.Text & "||" & objINtx.EnumStockTransferStatus.Confirmed
        Try
            intErrNo = objINtx.mtdUpdStockTransferDetail(strOpCdStckTxDet_ADD, _
                                                         strOpCdStckTxDet_UPD, _
                                                         strOpCdStckTxLine_GET, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         strARAccMonth, _
                                                         strARAccYear, _
                                                         StrTxParam, _
                                                         objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransfer), _
                                                         ErrorChk, _
                                                         TxID)
            lblStckTxID.Text = TxID
            If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                lblError.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
            End If
        End Try
        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
        BindGrid()

    End Sub

    Sub btnCancel_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

        Try
            intErrNo = objINtx.mtdAdjustInvItemLevel(strOpCdStckTxLine_GET, _
                                                     strOpCdItem_Details_UPD, _
                                                     strOpCdItem_Details_GET, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     lblStckTxID.Text, _
                                                     objINtx.EnumTransactionAction.Cancel, _
                                                     ErrorChk)
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=REMOVEINVENTORYITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
            End If
        End Try
        StrTxParam = lblStckTxID.Text & "|||||||||||||||" & objINtx.EnumStockTransferStatus.Cancelled

        If intErrNo = 0 Then
            Try
                intErrNo = objINtx.mtdUpdStockTransferDetail(strOpCdStckTxDet_ADD, _
                                                             strOpCdStckTxDet_UPD, _
                                                             strOpCdStckTxLine_GET, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strARAccMonth, _
                                                             strARAccYear, _
                                                             StrTxParam, _
                                                             objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransfer), _
                                                             ErrorChk, _
                                                             TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CANCELSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End If
            End Try
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            lblUnDel.Visible = True
        End If
        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
        BindGrid()
    End Sub

    Sub btnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

        If lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.Deleted) Then
            Try
                intErrNo = objINtx.mtdAdjustInvItemLevel(strOpCdStckTxLine_GET, _
                                                         strOpCdItem_Details_UPD, _
                                                         strOpCdItem_Details_GET, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         lblStckTxID.Text, _
                                                         objINtx.EnumTransactionAction.Undelete, _
                                                         ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UNDELETESTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End If
            End Try
            StrTxParam = lblStckTxID.Text & "|||||||||||||||" & objINtx.EnumStockTransferStatus.Active
        Else
            Try
                intErrNo = objINtx.mtdAdjustInvItemLevel(strOpCdStckTxLine_GET, _
                                                         strOpCdItem_Details_UPD, _
                                                         strOpCdItem_Details_GET, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         lblStckTxID.Text, _
                                                         objINtx.EnumTransactionAction.Delete, _
                                                         ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETENEWSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End If
            End Try
            StrTxParam = lblStckTxID.Text & "|||||||||||||||" & objINtx.EnumStockTransferStatus.Deleted & "|"
        End If

        If intErrNo = 0 And Not ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            Try
                intErrNo = objINtx.mtdUpdStockTransferDetail(strOpCdStckTxDet_ADD, _
                                                             strOpCdStckTxDet_UPD, _
                                                             strOpCdStckTxLine_GET, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strARAccMonth, _
                                                             strARAccYear, _
                                                             StrTxParam, _
                                                             objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransfer), _
                                                             ErrorChk, _
                                                             TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End If
            End Try
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            lblUnDel.Visible = True
        End If
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
        strTable = "IN_STOCKMOVEMENT"
        strSortLine = ""


        If intStatus = objINtx.EnumStockTransferStatus.Confirmed Or intStatus = objINtx.EnumStockTransferStatus.DbNote Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                        strUpdString, _
                                                        strTable, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_SHARE_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End Try
                LoadStockTxDetails()
                DisplayFromDB(False)
                DisablePage()
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_STOCKMOVEMENTDet.aspx?strStockTxId=" & strStockTxId & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       "&strDocTitle=" & lblDocTitle.Text & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_Trx_STOCKMOVEMENT_Details.aspx")
    End Sub

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
        ddlInventoryBin1.Items.Clear()
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO), objINstp.EnumInventoryBinLevel.HO))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central), objINstp.EnumInventoryBinLevel.Central))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Other), objINstp.EnumInventoryBinLevel.Other))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinI), objINstp.EnumInventoryBinLevel.BinI))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinII), objINstp.EnumInventoryBinLevel.BinII))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIII), objINstp.EnumInventoryBinLevel.BinIII))
        ddlInventoryBin1.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIV), objINstp.EnumInventoryBinLevel.BinIV))

        ddlInventoryBin2.Items.Clear()
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO), objINstp.EnumInventoryBinLevel.HO))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central), objINstp.EnumInventoryBinLevel.Central))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Other), objINstp.EnumInventoryBinLevel.Other))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinI), objINstp.EnumInventoryBinLevel.BinI))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinII), objINstp.EnumInventoryBinLevel.BinII))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIII), objINstp.EnumInventoryBinLevel.BinIII))
        ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIV), objINstp.EnumInventoryBinLevel.BinIV))
    End Sub
End Class
