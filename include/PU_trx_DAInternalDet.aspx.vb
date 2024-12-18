
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

Public Class PU_DAInternal : Inherits Page

    Protected WithEvents dgStkTx As DataGrid
    Protected WithEvents lblStckTxID As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents ToLocTag As Label
    Protected WithEvents lblReprint As Label
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtRemarks As TextBox
    Protected WithEvents txtQty As TextBox
    Protected WithEvents lblPDateTag As Label
    Protected WithEvents tblAdd As HtmlTable
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

    Protected WithEvents ddlInventoryBin As DropDownList
    Protected WithEvents txtAddNote As TextBox
    Protected WithEvents ddlDAIssue As DropDownList
    Protected WithEvents lblDAIssue As Label
    Protected WithEvents tblFACode As HtmlTableRow
    Protected WithEvents txtPRRefId As TextBox
    Protected WithEvents ddlPRRefLocCode As DropDownList
    Protected WithEvents lblDispAdvType As Label
    Protected WithEvents ddlPOId As DropDownList
    Protected WithEvents lblPOIDErr As Label
    Protected WithEvents lblSelectedItemCode As Label
    Protected WithEvents lblSelectedGRID As Label
    Protected WithEvents lblQtyReceive As Label
    Protected WithEvents lblIDQtyReceive As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblPRLocation As Label
    Protected WithEvents lblDocType As Label
    Protected WithEvents lblSelectListLoc As Label
    Protected WithEvents hidCost As HtmlInputHidden
    Protected WithEvents hidQty As HtmlInputHidden

    Protected objPUTrx As New agri.PU.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Public objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmin As New agri.Admin.clsShare()
    Protected objAdminSetup As New agri.Admin.clsLoc()

    Dim strOpCdStckTxDet_ADD As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_LINE_GET"
    Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
    Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"

    Const ITEM_PART_SEPERATOR As String = " @ "

    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New DataSet()
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
    Dim intPUAR As Integer
    Dim intFAAR As Integer
    Dim intConfigsetting As Integer
    Dim strLocationTag As String

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strPhyYear As String
    Dim strPhyMonth As String

    Dim strSelectedDispAdvId As String
    Dim strSelectedDAType As String
    Dim strSelectedPOId As String
    Dim strSelectedPRRefLocCode As String
    Dim strSelectedGoodsRcvLnId As String
    Dim strSelectedItemCode As String
    Dim strItemType As String
    Dim strPOLocCode As String
    Dim strDeptCode As String
    Dim dblCost As Double
    Dim dblAmount As Double


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strARAccMonth = Session("SS_ARACCMONTH")
        strARAccYear = Session("SS_ARACCYEAR")
        strPhyYear = Session("SS_PHYYEAR")
        strPhyMonth = Session("SS_PHYMONTH")
        intPUAR = Session("SS_PUAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDispatchAdvice), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedDispAdvId = Trim(IIf(Request.QueryString("DispAdvId") = "", Request.Form("DispAdvId"), Request.QueryString("DispAdvId")))
            strSelectedDAType = Trim(IIf(Request.QueryString("DAType") = "", Request.Form("DAType"), Request.QueryString("DAType")))

            onload_GetLangCap()
            If Not Page.IsPostBack Then
                lblStckTxID.Text = strSelectedDispAdvId
                lblDispAdvType.Text = strSelectedDAType
                BindInventoryBinLevel("")
                BindPOIssued("")

                If strSelectedDispAdvId <> "" Then
                    LoadStockTxDetails()
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        DisplayFromDB(False)
                        BindGrid()
                    End If
                Else
                    If lblDispAdvType.Text = Trim(objPUTrx.EnumDAType.Stock) Then
                        lblDocType.Text = "Stock / Workshop"
                    Else
                        lblDocType.Text = objPUTrx.mtdGetDAType(CInt(strSelectedDAType))
                    End If
                End If

                BindPO("")
                BindItemCodeList()
                BindLoc("")
            End If
            DisablePage()

            lblError.Visible = False
            lblStock.Visible = False
            lblUnDel.Visible = False
            lblConfirmErr.Visible = False
            lblItemCodeErr.Visible = False
            lblPOIDErr.Visible = False
            lblDAIssue.Visible = False
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STKTRANSFER_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
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
        Save.Visible = False
        Confirm.Visible = False
        Print.Visible = False
        Cancel.Visible = False
        PRDelete.Visible = False
        btnNew.Visible = False
        ddlInventoryBin.Enabled = False

        If lblDispAdvType.Text = CInt(objPUTrx.EnumGRNType.FixedAsset) Then
            tblFACode.Visible = True
        Else
            tblFACode.Visible = False
        End If

        Select Case Trim(lblStatusHid.Text)
            Case Trim(CStr(objPUTrx.EnumDAInternalStatus.Deleted))
                PRDelete.Visible = True
                btnNew.Visible = True
                PRDelete.ImageUrl = "../../images/butt_undelete.gif"
                PRDelete.AlternateText = "Undelete"
                PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objPUTrx.EnumDAInternalStatus.Confirmed))
                btnNew.Visible = True
                Print.Visible = True
                Cancel.Visible = True
            Case Trim(CStr(objPUTrx.EnumDAInternalStatus.Cancelled))
                btnNew.Visible = True
            Case Else
                txtDesc.Enabled = True
                txtRemarks.Enabled = True
                Save.Visible = True
                ddlInventoryBin.Enabled = True
                ddlPOId.Enabled = True
                txtPRRefId.Enabled = True
                ddlPRRefLocCode.Enabled = True
                lstItem.Enabled = True

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
        If lblStatusHid.Text = CStr(objPUTrx.EnumDAInternalStatus.Deleted) _
        Or lblStatusHid.Text = CStr(objPUTrx.EnumDAInternalStatus.Cancelled) Then
            tblAdd.Visible = False
        ElseIf lblStatusHid.Text = CStr(objPUTrx.EnumDAInternalStatus.Confirmed) Then
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
                    Case objPUTrx.mtdGetDAInternalStatus(objPUTrx.EnumDAInternalStatus.Active)
                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = True
                        DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objPUTrx.mtdGetDAInternalStatus(objPUTrx.EnumDAInternalStatus.Confirmed), _
                         objPUTrx.mtdGetDAInternalStatus(objPUTrx.EnumDAInternalStatus.Deleted), _
                         objPUTrx.mtdGetDAInternalStatus(objPUTrx.EnumDAInternalStatus.Cancelled)

                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = False

                End Select

        End Select
    End Sub

    Sub DisplayFromDB(ByVal pv_blnIsRedirect As Boolean)
        Status.Text = objPUTrx.mtdGetDAInternalStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
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
        If ddlDAIssue.SelectedItem.Value = "" Then
            lblDAIssue.Visible = True
            Return True
        Else
            Return False
        End If
        If ddlPOId.SelectedItem.Value <> "" Then
            If Request.Form("lstItem") = "" Then
                lblItemCodeErr.Visible = True
                Return True
            Else
                Return False
            End If
        Else
            lblPOIDErr.Visible = True
            Return True
        End If
    End Function

    Protected Function LoadDataGrid() As DataSet
        Dim strOpCode As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_LINE_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "DISPADVID|LOCCODE"
        strParamValue = Trim(lblStckTxID.Text) & "|" & strLocation

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsGrid)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFERINTERNAL&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
        End Try

        Return dsGrid
    End Function

    Sub LoadStockTxDetails()
        Dim strOpCode As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_DETAIL_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "DISPADVID|LOCCODE"
        strParamValue = Trim(lblStckTxID.Text) & "|" & strLocation

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFERINTERNALLINE&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblDispAdvType.Text = Trim(objDataSet.Tables(0).Rows(0).Item("DispAdvType"))

            If objDataSet.Tables(0).Rows(0).Item("DispAdvType") = objPUTrx.EnumDAType.Stock Then
                lblDocType.Text = "Stock / Workshop"
            Else
                lblDocType.Text = objPUTrx.mtdGetDAType(objDataSet.Tables(0).Rows(0).Item("DispAdvType"))
            End If
            BindPOIssued(Trim(objDataSet.Tables(0).Rows(0).Item("DALoc")))
            BindInventoryBinLevel(Trim(objDataSet.Tables(0).Rows(0).Item("Bin")))
            txtDesc.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Description"))

            If lblDispAdvType.Text = CInt(objPUTrx.EnumGRNType.FixedAsset) Then
                tblFACode.Visible = True
            Else
                tblFACode.Visible = False
            End If
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
                                                       objPUTrx.EnumDAType.All, _
                                                       dsItemCodeDropList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTOSERVTYPEDROPDOWNLIST&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
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
        Dim DispAdvLnIdCell As TableCell = E.Item.Cells(0)
        Dim DispAdvLnID As String = DispAdvLnIdCell.Text

        Dim strOpCode As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_LINE_DEL"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        If lblStatusHid.Text = CStr(objPUTrx.EnumDAInternalStatus.Active) Then

            lblItem = E.Item.FindControl("ItemCode")
            ItemCode = lblItem.Text

            strParamName = "DISPADVID|DISPADVLNID"

            strParamValue = Trim(lblStckTxID.Text) & "|" & DispAdvLnID

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
        Dim strOpCode_GetID As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_GET_ID"
        Dim strOpCode_UpdID As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_UPD_ID"
        Dim strOpCode As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_DETAIL_ADD"
        Dim strOpCodeLn As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_LINE_ADD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim pr_strID As String
        Dim pr_ID As String
        Dim strGRLnID As String = ""
        Dim arrGRLnID As Array

        If ddlDAIssue.SelectedItem.Value = "" Then
            lblDAIssue.Visible = True
            Exit Sub
        End If

        If lstItem.SelectedItem.Value = "" Then
            lblItemCodeErr.Visible = True
            Exit Sub
        End If

        If ddlPOId.SelectedItem.Value = "" Then
            lblPOIDErr.Visible = True
            Exit Sub
        End If

        If lblStckTxID.Text = "" Then
            UpdateDataMaster()
        End If

        If txtQty.Text > "0" Then
            strParamName = "TRAN_PREFIX"
            strParamValue = Trim(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.DispatchAdviceLn))

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCode_GetID, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objDataSet)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ID&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
            End Try

            pr_ID = Trim(objDataSet.Tables(0).Rows(0).Item("Val") + 1)
            pr_strID = strParamValue & Mid(Year(Now()), 3, 2) & Format(objDataSet.Tables(0).Rows(0).Item("Val") + 1, New String("0", objDataSet.Tables(0).Rows(0).Item("Length")))

            strParamName = "TRAN_PREFIX|VAL_ID"
            strParamValue = Trim(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.DispatchAdviceLn)) & "|" & pr_ID

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_UpdID, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_TBID&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
            End Try

            arrGRLnID = Split(lstItem.SelectedItem.Value.Trim, ITEM_PART_SEPERATOR)
            If UBound(arrGRLnID) <> -1 Then
                strGRLnID = arrGRLnID(0)
            End If

            dblCost = CDbl(hidCost.Value)
            dblAmount = FormatNumber(Val(txtQty.Text) * dblCost, 0)
            strParamName = "DISPADVLNID|DISPADVID|ITEMCODE|ADDITIONALNOTE|QTYDISP|COST|AMOUNT|PRID|PRLOCCODE|GOODSRCVID|GOODSRCVLNID"

            strParamValue = Trim(pr_strID) & _
                            "|" & Trim(lblStckTxID.Text) & _
                            "|" & lblSelectedItemCode.Text & _
                            "|" & Trim(txtAddNote.Text) & _
                            "|" & txtQty.Text & _
                            "|" & dblCost & _
                            "|" & dblAmount & _
                            "|" & txtPRRefId.Text & _
                            "|" & ddlPRRefLocCode.SelectedItem.Value & _
                            "|" & IIf(ddlPOId.SelectedItem.Value = "", "", lblSelectedGRID.Text) & _
                            "|" & strGRLnID

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCodeLn, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
            End Try

        End If

        LoadStockTxDetails()
        DisplayFromDB(False)
        BindGrid()
        BindPO("")
        DisablePage()
        txtQty.Text = ""
        lblIDQtyReceive.Text = ""
        txtDesc.Text = ""
    End Sub

    Sub UpdateDataMaster()
        Dim strOpCode_GetID As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_GET_ID"
        Dim strOpCode_UpdID As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_UPD_ID"
        Dim strOpCode_Add As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_DETAIL_ADD"
        Dim strOpCode_Upd As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim pr_strID As String
        Dim pr_ID As String

        If ddlDAIssue.SelectedItem.Value = "" Then
            lblDAIssue.Visible = True
            Exit Sub
        End If

        If lblStckTxID.Text = "" Then
            strParamName = "TRAN_PREFIX"
            strParamValue = Trim(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.DispatchAdvice))

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCode_GetID, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objDataSet)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ID&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
            End Try

            If Len(strPhyMonth) = 1 Then
                strPhyMonth = "0" & strPhyMonth
            End If

            strDeptCode = ddlDAIssue.SelectedItem.Value
            If Trim(strDeptCode) = "0" Then
                strDeptCode = "JKT"
            End If
            If Trim(strDeptCode) = "1" Then
                strDeptCode = "PKU"
            End If
            If Trim(strDeptCode) = "2" Then
                strDeptCode = "LMP"
            End If
            If Trim(strDeptCode) = "3" Then
                strDeptCode = "PLM"
            End If
            If Trim(strDeptCode) = "4" Then
                strDeptCode = "BKL"
            End If
            If Trim(strDeptCode) = "5" Then
                strDeptCode = "LOK"
            End If

            pr_ID = Trim(objDataSet.Tables(0).Rows(0).Item("Val") + 1)
            If lblDispAdvType.Text = objPUTrx.EnumDAType.Stock Or lblDispAdvType.Text = objPUTrx.EnumDAType.FixedAsset Then
                pr_strID = "SPBi" & "-" & Left(Trim(strDeptCode), 3) & "/" & strLocation & "/" & Right(strPhyYear, 2) & "/" & strPhyMonth & "/" & Format(objDataSet.Tables(0).Rows(0).Item("Val") + 1, New String("0", 4))
            Else
                pr_strID = strParamValue & Mid(Year(Now()), 3, 2) & Format(objDataSet.Tables(0).Rows(0).Item("Val") + 1, New String("0", objDataSet.Tables(0).Rows(0).Item("Length")))
            End If
            Response.Write(pr_ID)
            Response.Write(pr_strID)
            strParamName = "TRAN_PREFIX|VAL_ID"
            strParamValue = Trim(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.DispatchAdvice)) & "|" & pr_ID

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_UpdID, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_TBID&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
            End Try


            strParamName = "DISPADVID|DISPADVTYPE|DALOC|DESCRIPTION|LOCCODE|BIN|ACCMONTH|ACCYEAR|REMARK|STATUS|UPDATEID"

            strParamValue = Trim(pr_strID) & _
                            "|" & Trim(lblDispAdvType.Text) & _
                            "|" & ddlDAIssue.SelectedItem.Value & _
                            "|" & Trim(txtDesc.Text) & _
                            "|" & strLocation & _
                            "|" & ddlInventoryBin.SelectedItem.Value & _
                            "|" & strAccMonth & _
                            "|" & strAccYear & _
                            "|" & Trim(txtRemarks.Text) & _
                            "|" & objPUTrx.EnumDAInternalStatus.Active & _
                            "|" & strUserId

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_Add, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
            End Try

            lblStckTxID.Text = pr_strID
        Else
            strParamName = "DISPADVID|DALOC|DESCRIPTION|BIN|REMARK|LOCCODE|STATUS|UPDATEID"

            strParamValue = Trim(lblStckTxID.Text) & _
                            "|" & ddlDAIssue.SelectedItem.Value & _
                            "|" & Trim(txtDesc.Text) & _
                            "|" & ddlInventoryBin.SelectedItem.Value & _
                            "|" & Trim(txtRemarks.Text) & _
                            "|" & strLocation & _
                            "|" & objPUTrx.EnumDAInternalStatus.Active & _
                            "|" & strUserId

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
        UpdateDataMaster()

        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCode As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_DETAIL_UPD"
        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|DISPADVID|DESCRIPTION|BIN|DALOC|REMARK|STATUS|UPDATEID"

        strParamValue = strLocation & _
                        "|" & Trim(lblStckTxID.Text) & _
                        "|" & Trim(txtDesc.Text) & _
                        "|" & ddlInventoryBin.SelectedItem.Value & _
                        "|" & ddlDAIssue.SelectedItem.Value & _
                        "|" & Trim(txtRemarks.Text) & _
                        "|" & objPUTrx.EnumDAInternalStatus.Confirmed & _
                        "|" & strUserId

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx")
        End Try

        strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

        strParamValue = Trim(strLocation) & _
                        "|" & "PU_DISPADVINTERNAL" & _
                        "|" & "PU_DISPADVINTERNALLN" & _
                        "|" & "DISPADVID" & _
                        "|" & Trim(lblStckTxID.Text) & _
                        "|" & "QTYDISP" & _
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

        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
        BindGrid()
    End Sub

    Sub btnCancel_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCode As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_DETAIL_UPD"
        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|DISPADVID|DESCRIPTION|BIN|DALOC|REMARK|STATUS|UPDATEID"

        strParamValue = strLocation & _
                        "|" & Trim(lblStckTxID.Text) & _
                        "|" & Trim(txtDesc.Text) & _
                        "|" & ddlInventoryBin.SelectedItem.Value & _
                        "|" & ddlDAIssue.SelectedItem.Value & _
                        "|" & Trim(txtRemarks.Text) & _
                        "|" & objPUTrx.EnumDAInternalStatus.Cancelled & _
                        "|" & strUserId
        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx")
        End Try

        strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

        strParamValue = Trim(strLocation) & _
                        "|" & "PU_DISPADVINTERNAL" & _
                        "|" & "PU_DISPADVINTERNALLN" & _
                        "|" & "DISPADVID" & _
                        "|" & Trim(lblStckTxID.Text) & _
                        "|" & "QTYDISP" & _
                        "|" & ddlInventoryBin.SelectedItem.Value & _
                        "|" & "+" & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
        BindGrid()
    End Sub

    Sub btnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCode As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|DISPADVID|DESCRIPTION|BIN|REMARK|STATUS|UPDATEID"

        If lblStatusHid.Text = CStr(objPUTrx.EnumDAInternalStatus.Deleted) Then
            strParamValue = strLocation & "|" & Trim(lblStckTxID.Text) & "|" & Trim(txtDesc.Text) & _
                            "|" & ddlInventoryBin.SelectedItem.Value & "|" & _
                            "|" & Trim(txtRemarks.Text) & "|" & objPUTrx.EnumDAInternalStatus.Active & "|" & strUserId
        Else
            strParamValue = strLocation & "|" & Trim(lblStckTxID.Text) & "|" & Trim(txtDesc.Text) & _
                            "|" & ddlInventoryBin.SelectedItem.Value & "|" & _
                            "|" & Trim(txtRemarks.Text) & "|" & objPUTrx.EnumDAInternalStatus.Deleted & "|" & strUserId
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
        Dim intErrNo As Integer

        strUpdString = "where DispAdvId = '" & lblStckTxID.Text & "'"
        strStatus = Trim(Status.Text)
        intStatus = CInt(Trim(lblStatusHid.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strSortLine = "PU_DISPADVINTERNALLN.GoodsRcvLnID"
        strTable = "PU_DISPADVINTERNAL"

        If intStatus = objPUTrx.EnumDAStatus.Confirmed Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                           strUpdString, _
                                                           strTable, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_DA_DETAILS_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
                End Try
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        LoadStockTxDetails()
        DisplayFromDB(False)

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_DADet.aspx?strDispAdvId=" & lblStckTxID.Text & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../PU/trx/PU_trx_DAInternalList.aspx")
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("PU_trx_DAInternalDet.aspx?DAType=" & lblDispAdvType.Text)
    End Sub

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
        ddlInventoryBin.Items.Clear()
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO), objINstp.EnumInventoryBinLevel.HO))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central), objINstp.EnumInventoryBinLevel.Central))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Other), objINstp.EnumInventoryBinLevel.Other))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinI), objINstp.EnumInventoryBinLevel.BinI))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinII), objINstp.EnumInventoryBinLevel.BinII))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIII), objINstp.EnumInventoryBinLevel.BinIII))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIV), objINstp.EnumInventoryBinLevel.BinIV))

        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        End If
    End Sub

    Sub BindPOIssued(ByVal pv_strPOIssued As String)
        ddlDAIssue.Items.Clear()
        ddlDAIssue.Items.Add(New ListItem("Select DA Issued Location", ""))
        ddlDAIssue.Items.Add(New ListItem(objPUTrx.mtdGetPOIssued(objPUTrx.EnumPOIssued.JKT), objPUTrx.EnumPOIssued.JKT))
        ddlDAIssue.Items.Add(New ListItem(objPUTrx.mtdGetPOIssued(objPUTrx.EnumPOIssued.PKU), objPUTrx.EnumPOIssued.PKU))
        ddlDAIssue.Items.Add(New ListItem(objPUTrx.mtdGetPOIssued(objPUTrx.EnumPOIssued.LMP), objPUTrx.EnumPOIssued.LMP))
        ddlDAIssue.Items.Add(New ListItem(objPUTrx.mtdGetPOIssued(objPUTrx.EnumPOIssued.PLM), objPUTrx.EnumPOIssued.PLM))
        ddlDAIssue.Items.Add(New ListItem(objPUTrx.mtdGetPOIssued(objPUTrx.EnumPOIssued.BKL), objPUTrx.EnumPOIssued.BKL))
        ddlDAIssue.Items.Add(New ListItem(objPUTrx.mtdGetPOIssued(objPUTrx.EnumPOIssued.LOK), objPUTrx.EnumPOIssued.LOK))


        If Trim(pv_strPOIssued) <> "" Then
            With ddlDAIssue
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strPOIssued)))
            End With
        End If
    End Sub

    Sub BindPO(ByVal pv_strSuppCode As String)
        Dim strOpCd As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_PO_ID_GET"
        Dim objPODs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "STATUS|POTYPE|LOCCODE|DISPADVID"
        strParamValue = objPUTrx.EnumGRStatus.Confirmed & _
                        "|" & lblDispAdvType.Text & _
                        "|" & strLocation & _
                        "|" & lblStckTxID.Text

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objPODs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ID&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
        End Try

        For intCnt = 0 To objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("POId") = Trim(objPODs.Tables(0).Rows(intCnt).Item("POId"))
            objPODs.Tables(0).Rows(intCnt).Item("DispPOId") = Trim(objPODs.Tables(0).Rows(intCnt).Item("POId"))
            objPODs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objPODs.Tables(0).Rows(intCnt).Item("LocCode"))

            If objPODs.Tables(0).Rows(intCnt).Item("POId") = strSelectedPOId Then
                intSelectedIndex = intCnt + 1

                lblPRLocation.Text = objPODs.Tables(0).Rows(intCnt).Item("LocCode")
                strPOLocCode = lblPRLocation.Text
            Else
                lblPRLocation.Text = ""
            End If
        Next intCnt

        dr = objPODs.Tables(0).NewRow()
        dr("POId") = ""
        dr("DispPOId") = "Please select Purchase Order ID"
        objPODs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPOId.DataSource = objPODs.Tables(0)
        ddlPOId.DataValueField = "POId"
        ddlPOId.DataTextField = "DispPOId"
        ddlPOId.DataBind()
        ddlPOId.SelectedIndex = intSelectedIndex
        strSelectedPOId = ddlPOId.SelectedItem.Value

        If ddlPOId.SelectedItem.Value = "" Then
            BindItemCodeList()
        Else
            BindPOItem(strSelectedPOId)
        End If
    End Sub

    Sub BindPOItem(ByVal pv_strPOId As String)
        Dim strOpCd As String = "IN_CLSTRX_PURREQ_ITEMLIST_GET"
        Dim objPOItemDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "STATUS|POID|LOCCODE|DISPADVID"
        strParamValue = objPUTrx.EnumGRStatus.Confirmed & _
                        "|" & pv_strPOId & _
                        "|" & strLocation & _
                        "|" & lblStckTxID.Text

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objPOItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ID&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
        End Try

        'strParam = pv_strPOId & "|" & objPUTrx.EnumGRStatus.Confirmed
        'Try
        '    intErrNo = objPUTrx.mtdGetDAGRLine(strOpCd, _
        '                                    strCompany, _
        '                                    strLocation, _
        '                                    strUserId, _
        '                                    strAccMonth, _
        '                                    strAccYear, _
        '                                    strParam, _
        '                                    objPOItemDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_GRItem&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        'End Try

        lblSelectedItemCode.Text = ""
        lblSelectedGRID.Text = ""
        lblQtyReceive.Text = ""
        txtPRRefId.Text = ""
        txtQty.Text = ""
        lblIDQtyReceive.Text = ""

        For intCnt = 0 To objPOItemDs.Tables(0).Rows.Count - 1

            If lblDispAdvType.Text = objPUTrx.EnumDAType.Stock Then
                If objPOItemDs.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then


                    objPOItemDs.Tables(0).Rows(intCnt).Item("Description") = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("PartNo") & " (" & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("ItemLocCode") & ", " & _
                                                                             "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("Cost"), 0) & ", " & _
                                                                             objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty"), 5) & ", " & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("ReceiveUOM")

                    objPOItemDs.Tables(0).Rows(intCnt).Item("GoodsRcvLnId") = objPOItemDs.Tables(0).Rows(intCnt).Item("GoodsRcvLnId") & ITEM_PART_SEPERATOR & _
                                                                              objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                              objPOItemDs.Tables(0).Rows(intCnt).Item("PartNo")
                Else


                    objPOItemDs.Tables(0).Rows(intCnt).Item("Description") = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("ItemLocCode") & ", " & _
                                                                             "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("Cost"), 0) & ", " & _
                                                                             objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty"), 5) & ", " & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("ReceiveUOM")
                End If
            Else


                objPOItemDs.Tables(0).Rows(intCnt).Item("Description") = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                         objPOItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                         objPOItemDs.Tables(0).Rows(intCnt).Item("ItemLocCode") & ", " & _
                                                                         "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("Cost"), 0) & ", " & _
                                                                         objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty"), 5) & ", " & _
                                                                         objPOItemDs.Tables(0).Rows(intCnt).Item("ReceiveUOM")
            End If

            If objPOItemDs.Tables(0).Rows(intCnt).Item("GoodsRcvLnId") = strSelectedGoodsRcvLnId Then
                intSelectedIndex = intCnt + 1

                lblSelectedItemCode.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode")
                lblSelectedGRID.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("GoodsRcvId")
                txtPRRefId.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemPR")
                strSelectedPRRefLocCode = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemLocCode")
                lblQtyReceive.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty")
                lblIDQtyReceive.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty"), 5)
                txtQty.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty")
                hidQty.Value = txtQty.Text
                dblCost = FormatNumber(objPOItemDs.Tables(0).Rows(intCnt).Item("Cost"), 0, -2, -2, 0)
                hidCost.Value = dblCost
                strItemType = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemType")
            End If
        Next intCnt

        dr = objPOItemDs.Tables(0).NewRow()
        dr("GoodsRcvLnId") = ""
        dr("Description") = "Please select Item Code"
        objPOItemDs.Tables(0).Rows.InsertAt(dr, 0)

        lstItem.DataSource = objPOItemDs.Tables(0)
        lstItem.DataValueField = "GoodsRcvLnId"
        lstItem.DataTextField = "Description"
        lstItem.DataBind()
        lstItem.SelectedIndex = intSelectedIndex        
    End Sub

    Sub POIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim POType As String

        strSelectedPOId = ddlPOId.SelectedItem.Value

        If strSelectedPOId = "" Then
            BindItemCodeList()
            txtPRRefId.Enabled = True
            ddlPRRefLocCode.Enabled = True
            ddlPOId.Enabled = True
            lblPRLocation.Text = ""
            lblIDQtyReceive.Text = ""
        Else

            BindPOItem(strSelectedPOId)
            txtPRRefId.Enabled = False
            ddlPRRefLocCode.Enabled = False
            lblPRLocation.Text = strPOLocCode
            POType = GetPOType(strSelectedPOId)

            If POType = objPUTrx.EnumPOType.FixedAsset And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
                tblFACode.Visible = True
                'BindFACode(lstItem.SelectedItem.Value)
            End If

        End If

        txtPRRefId.Text = ""
    End Sub

    Function GetPOType(ByVal strSelectedPOId As String) As String

        Dim strOpCd_GRPOType As String = "PU_CLSTRX_GR_POTYPE_GET"
        Dim objPOType As New Object()
        Dim intErrNo As Integer
        Dim POType As String

        Try
            intErrNo = objPUTrx.mtdGetPOType(strOpCd_GRPOType, strSelectedPOId, objPOType)
            POType = Trim(objPOType.Tables(0).Rows(0).Item("POType"))
            Return POType
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_POType&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

    End Function

    Sub FAItemIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtQty.Enabled = False        
    End Sub

    Sub LocIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If strSelectedPOId = "" Then
            BindItemCodeList()
        Else
            BindPOItem(strSelectedPOId)
        End If

        If ddlPRRefLocCode.SelectedItem.Value <> "" Then
            strSelectedPOId = ddlPOID.SelectedItem.Value
            strSelectedPRRefLocCode = ddlPRRefLocCode.SelectedItem.Value
            strSelectedItemCode = lstItem.SelectedItem.Value
            ddlPOId.Enabled = False
            lblPRLocation.Text = ""
        Else
            ddlPOId.Enabled = True
            txtPRRefId.Enabled = False
            ddlPRRefLocCode.Enabled = False
        End If

    End Sub

    Sub BindLoc(ByVal pv_strPRId As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        strPRRefLocCode = IIf(pv_strPRId = "", "", strSelectedPRRefLocCode)
        strParam = strPRRefLocCode & "|" & objAdminSetup.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPUTrx.mtdGetLoc(strOpCd, strParam, objLocDs, strLocation)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            With objLocDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If .Item("LocCode") = strSelectedPRRefLocCode Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelectListLoc.Text & strLocationTag
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPRRefLocCode.DataSource = objLocDs.Tables(0)
        ddlPRRefLocCode.DataValueField = "LocCode"
        ddlPRRefLocCode.DataTextField = "Description"
        ddlPRRefLocCode.DataBind()
        ddlPRRefLocCode.SelectedIndex = intSelectedIndex
        strSelectedPRRefLocCode = ddlPRRefLocCode.SelectedItem.Value
    End Sub

    Sub ItemIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedPOId = ddlPOId.SelectedItem.Value
        strSelectedGoodsRcvLnId = lstItem.SelectedItem.Value
        strSelectedItemCode = lstItem.SelectedItem.Value
        lblSelectedItemCode.Text = lstItem.SelectedItem.Value
        If strSelectedPOId = "" Then
            BindItemCodeList()
        Else
            BindPOItem(strSelectedPOId)
        End If
    End Sub
End Class
