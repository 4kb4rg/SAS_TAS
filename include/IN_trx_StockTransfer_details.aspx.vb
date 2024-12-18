
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

Public Class IN_StockTransfer : Inherits Page

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
    Protected WithEvents txtCost As TextBox
    Protected WithEvents txtQtyPO As TextBox
    Protected WithEvents lblErrQty As Label

    Protected WithEvents txtTransporter As TextBox
    Protected WithEvents txtVehicle As TextBox
    Protected WithEvents txtDriverName As TextBox
    Protected WithEvents txtDriverIC As TextBox

    Protected WithEvents lblStockReceiveID As Label
    Protected WithEvents lblPDateTag As Label
    Protected WithEvents lblDNIDTag As Label
    Protected WithEvents tblAdd As HtmlTable
    Protected WithEvents validateQty As RequiredFieldValidator
    Protected WithEvents lblError As Label
    Protected WithEvents lblStock As Label
    Protected WithEvents lblUnDel As Label
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents PRDelete As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Confirm As ImageButton
    Protected WithEvents Print As ImageButton
    Protected WithEvents DebitNote As ImageButton
    Protected WithEvents Cancel As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents btnEdited As ImageButton

    Protected WithEvents Back As ImageButton
    Protected WithEvents lblConfirmErr As Label
    Protected WithEvents lblStatusHid As Label
    Protected WithEvents lblToLocErr As Label
    Protected WithEvents lblItemCodeErr As Label
    Protected WithEvents lstToLoc As DropDownList
    Protected WithEvents lstItem As DropDownList
    Protected WithEvents FindIN As HtmlInputButton
    Protected WithEvents lblTo As Label
    Protected WithEvents lblPleaseSelectOne As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblDocTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelectedItemCode As Label
    Protected WithEvents ddlInventoryBin As DropDownList
    Protected WithEvents lblInventoryBin As Label
    Protected WithEvents lstPR As DropDownList


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
    Protected objINtx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objGLtrx As New agri.GL.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmin As New agri.Admin.clsShare()

    Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_STOCKTRANSFER_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_STOCKTRANSFER_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_STOCKTRANSFER_LINE_GET"
    Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
    Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"
    Dim strOpCdItem_Details_DEL As String = "IN_CLSTRX_STOCKITEM_DETAIL_DEL"
    
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
        strARAccMonth = Session("SS_ARACCMONTH")
        strARAccYear = Session("SS_ARACCYEAR")
        intINAR = Session("SS_INAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer), intINAR) = False Then
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
            DebitNote.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(DebitNote).ToString())
            btnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnNew).ToString())
            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())

            If Not Page.IsPostBack Then
                Confirm.Attributes("onclick") = "javascript:return ConfirmAction('Confirmed Stock Transfer');"
                PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('Delete Stock Transfer');"
                btnEdited.Attributes("onclick") = "javascript:return ConfirmAction('Edit Stock Transfer');"

                BindInventoryBinLevel("")
                BindLocDropList("")
                lblStckTxID.Text = Request.QueryString("Id")
                LoadStockTxDetails()
                BindPO(lstToLoc.SelectedItem.Value)
                BindItemCodeList()
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    DisplayFromDB(False)
                    DisablePage()
                    BindGrid()
                End If

                If lblStckTxID.Text = "" Then
                    txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    TrLink.Visible = False
                End If
            End If
            DisablePage()

            lblError.Visible = False
            lblStock.Visible = False
            lblUnDel.Visible = False
            lblConfirmErr.Visible = False
            lblToLocErr.Visible = False
            lblItemCodeErr.Visible = False
            lblInventoryBin.Visible = False
            lblDate.Visible = False
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        ToLocTag.Text = lblTo.Text & GetCaption(objLangCap.EnumLangCap.Location) & " :*"
        strLocationTag = GetCaption(objLangCap.EnumLangCap.Location)
        lblToLocErr.Text = lblPleaseSelectOne.Text & strLocationTag & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STKTRANSFER_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
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

    Sub PageConTrol()
        Dim intCnt As Integer

        For intCnt = 0 To dgStkTx.Items.Count - 1
            If Len(CType(dgStkTx.Items(intCnt).FindControl("ReceiveID"), Label).Text.Trim) = 0 Then
                CType(dgStkTx.Items(intCnt).FindControl("Delete"), LinkButton).Visible = True
            Else
                CType(dgStkTx.Items(intCnt).FindControl("Delete"), LinkButton).Visible = False
            End If
        Next

    End Sub

    Sub DisablePage()
        txtDesc.Enabled = False
        txtRemarks.Enabled = False
        lstToLoc.Enabled = False
        validateQty.Visible = False
        Save.Visible = False
        Confirm.Visible = False
        Print.Visible = False
        DebitNote.Visible = False
        Cancel.Visible = False
        PRDelete.Visible = False
        btnNew.Visible = False
        ddlInventoryBin.Enabled = False
        btnEdited.Visible = False

        Select Case Trim(lblStatusHid.Text)
            Case Trim(CStr(objINtx.EnumStockTransferStatus.Deleted))
                PRDelete.Visible = True
                btnNew.Visible = True
                PRDelete.Visible = False
                'PRDelete.ImageUrl = "../../images/butt_undelete.gif"
                'PRDelete.AlternateText = "Undelete"
                'PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objINtx.EnumStockTransferStatus.Confirmed))
                btnNew.Visible = True
                DebitNote.Visible = False
                Print.Visible = True

                If Len(Trim(lblStockReceiveID.Text)) = 0 And intLevel >= 2 Then
                    btnEdited.Visible = True
                Else
                    btnEdited.Visible = False
                End If

            Case Trim(CStr(objINtx.EnumStockTransferStatus.DBNote))
                btnNew.Visible = True
                Print.Visible = True
            Case Else
                txtDesc.Enabled = True
                txtRemarks.Enabled = True
                lstToLoc.Enabled = True
                validateQty.Visible = True
                Save.Visible = True
                ddlInventoryBin.Enabled = True

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
        Dim lblReceiveiD As Label
            Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem

                Select Case Trim(Status.Text)
                    Case objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.Active)
                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('Delete');"
                        DeleteButton.Visible = True
                        DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.Confirmed), _
                         objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.Deleted), _
                         objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.DbNote), _
                         objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.Cancelled)

                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('Delete');"
                        'lblReceiveiD = e.Item.FindControl("ReceiveID")

                        'If Len(lblReceiveiD.Text.Trim) > 0 Then
                        '    DeleteButton.Visible = False
                        'Else
                        '    DeleteButton.Visible = True
                        'End If

                End Select

        End Select
    End Sub

    Sub DisplayFromDB(ByVal pv_blnIsRedirect As Boolean)
        Status.Text = objINtx.mtdGetStocktransferStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
        lblStatusHid.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Status"))
        CreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("UserName"))
        txtDesc.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Description"))
        txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))
        lblTotAmtFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objDataSet.Tables(0).Rows(0).Item("TotalAmount"), 2), 2)
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
        dgStkTx.DataSource = LoadDataGrid()
        dgStkTx.DataBind()

        PageConTrol()
    End Sub

    Function CheckRequiredField() As Boolean
        If Request.Form("lstItem") = "" Then
            lblItemCodeErr.Visible = True
            Return True
        Else
            Return False
        End If
    End Function

    Function CheckLocField() As Boolean
        If lstToLoc.SelectedItem.Value = "" Then
            lblToLocErr.Visible = True
            Return True
        Else
            Return False
        End If
    End Function

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
        End Try

        If dsGrid.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
        Else
            TrLink.Visible = False
        End If

        Return dsGrid
    End Function

    Sub BindPO(ByVal StrLocCode As String)
        Dim strOpCd As String = "IN_CLSTRX_STOCKTRANSFER_PO_DISP_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim strParamValue As String = ""

        strParam = "STRSEARCH|LOCCODE"
        strParamValue = ""
        strParamValue = "" & "|" & strLocation

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, strParam, strParamValue, objLocDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            objLocDs.Tables(0).Rows(intCnt).Item("GoodsRcvID") = objLocDs.Tables(0).Rows(intCnt).Item("GoodsRcvID").Trim()
            objLocDs.Tables(0).Rows(intCnt).Item("POID") = objLocDs.Tables(0).Rows(intCnt).Item("GoodsRcvID") & ", POID(" & objLocDs.Tables(0).Rows(intCnt).Item("POID").Trim() & ") " & ", PRID (" & objLocDs.Tables(0).Rows(intCnt).Item("PRID") & ")"

            'If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = StrLocCode Then
            '    intSelectedIndex = intCnt + 1
            'End If
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("PRID") = ""
        dr("POID") = "Please Select" 'lblSelectListLoc.Text & lblLocation.Text
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        lstPR.DataSource = objLocDs.Tables(0)
        lstPR.DataValueField = "GoodsRcvID"
        lstPR.DataTextField = "POID"
        lstPR.DataBind()
        lstPR.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindItemCodeList()
        Dim strOpCd As String = "IN_CLSTRX_STOCKTRANSFER_PO_DISP_ITEM_GET"
        Dim objDSItem As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim strParamValue As String = ""

        strParam = "STRSEARCH|DOCID"
        strParamValue = "" & "|" & lstPR.SelectedItem.Value

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, strParam, strParamValue, objDSItem)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objDSItem.Tables(0).Rows.Count - 1
            objDSItem.Tables(0).Rows(intCnt).Item("GoodsRcvLnID") = objDSItem.Tables(0).Rows(intCnt).Item("GoodsRcvLnID").Trim()
            objDSItem.Tables(0).Rows(intCnt).Item("Description") = objDSItem.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & objDSItem.Tables(0).Rows(intCnt).Item("Description").Trim() & ") " & objDSItem.Tables(0).Rows(intCnt).Item("QtyDisp")
        Next intCnt

        dr = objDSItem.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("ItemCode") = "Please Select" 'lblSelectListLoc.Text & lblLocation.Text
        objDSItem.Tables(0).Rows.InsertAt(dr, 0)

        lstItem.DataSource = objDSItem.Tables(0)
        lstItem.DataValueField = "GoodsRcvLnID"
        lstItem.DataTextField = "Description"
        lstItem.DataBind()
        lstItem.SelectedIndex = intSelectedIndex
    End Sub

    Sub RebindItemList(ByVal sender As Object, ByVal e As System.EventArgs)
        BindItemCodeList()
    End Sub

    Sub RebindPOList(ByVal sender As Object, ByVal e As System.EventArgs)
        lstPR.Items.Clear()
        lstItem.Items.Clear()
        BindPO(lstToLoc.SelectedItem.Value)
        BindItemCodeList()
    End Sub

    Sub RebindItemDetail(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strOpCd As String = "IN_CLSTRX_STOCKTRANSFER_PO_DISP_ITEM_GET"
        Dim objDSItem As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer        
        Dim strParamValue As String = ""

        strParam = "DOCID|STRSEARCH"
        strParamValue = lstPR.SelectedItem.Value & "|" & "Where D.GoodsRcvLnID='" & lstItem.SelectedItem.Value & "'"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, strParam, strParamValue, objDSItem)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        txtQty.Text = 0
        If objDSItem.Tables(0).Rows.Count > 0 Then
            lblSelectedItemCode.Text = Trim(objDSItem.Tables(0).Rows(0).Item("ItemCode"))
            txtQtyPO.Text = objDSItem.Tables(0).Rows(0).Item("QtyDisp")
            txtCost.Text = objDSItem.Tables(0).Rows(0).Item("Cost")
        End If

    End Sub

    Sub LoadStockTxDetails()
        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_STOCKTRANSFER_DETAIL_GET"
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFERLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblAccPeriod.Text = objDataSet.Tables(0).Rows(0).Item("AccMonth") & "/" & objDataSet.Tables(0).Rows(0).Item("AccYear")
            'txtDate.Text = objGlobal.GetShortDate(strDateFMT, Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
            txtDate.Text = Date_Validation(objDataSet.Tables(0).Rows(0).Item("CreateDate"), True)
            'txtDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
            BindInventoryBinLevel(Trim(objDataSet.Tables(0).Rows(0).Item("Bin")))
            BindLocDropList(Trim(objDataSet.Tables(0).Rows(0).Item("ToLocCode")))
            txtTransporter.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Transporter"))
            txtVehicle.Text = Trim(objDataSet.Tables(0).Rows(0).Item("VehicleCode"))
            txtDriverName.Text = Trim(objDataSet.Tables(0).Rows(0).Item("DriverName"))
            txtDriverIC.Text = Trim(objDataSet.Tables(0).Rows(0).Item("DriverIC"))
            lblStockReceiveID.Text = Trim(objDataSet.Tables(0).Rows(0).Item("ReceiveID"))

            lstToLoc.Enabled = False
        Else
            lstToLoc.Enabled = True
        End If
    End Sub

    Sub BindLocDropList(ByVal StrLocCode As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()        
        Dim strParam As String = ""
        Dim strParamValue As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        'strSelectedPRRefLocCode = StrLocCode
        'strLocPenyerahan = IIf(StrLocCode = "", "", strSelectedPRRefLocCode)
        'strParam = strLocPenyerahan & "|" & objAdmin.EnumLocStatus.Active & "|LocCode|"

        strParam = "STRSEARCH"
        If Trim(StrLocCode) = "" Then
            strParamValue = ""
        Else
            strParamValue = "" ''"And A.LocCode='" & StrLocCode & "' "
        End If


        Try
            'intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objLocDs)
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, strParam, strParamValue, objLocDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode").Trim()
            objLocDs.Tables(0).Rows(intCnt).Item("Description") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode") & " (" & objLocDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"

            If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = StrLocCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelect.Text & strLocationTag & lblCode.Text
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        lstToLoc.DataSource = objLocDs.Tables(0)
        lstToLoc.DataValueField = "LocCode"
        lstToLoc.DataTextField = "Description"
        lstToLoc.DataBind()
        lstToLoc.SelectedIndex = intSelectedIndex

    End Sub

    Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objItemDs As New Object()
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

        Dim lblItem As Label
        Dim lblST As Label
        Dim StockTrnsID As String
        Dim ItemCode As String
        Dim strParamName As String

        Dim strParamValue As String

        lblItem = E.Item.FindControl("ItemCode")
        ItemCode = lblItem.Text.Trim

        'If lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.Active) Then
        strParamName = "ST|ITEM"
        strParamValue = lblStckTxID.Text.Trim & "|" & ItemCode

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCdItem_Details_DEL, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_PURREQLN&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        LoadStockTxDetails()
        DisplayFromDB(False)
        BindGrid()
        BindItemCodeList()
        'End If
    End Sub

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_STOCKTRANSFER_LINE_ADD"
        Dim strTxLnParam As String
        Dim TxID As String
        Dim StrTxParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim arrPartNo As Array 
        Dim strItemCode As String 
        Dim strNewIDFormat As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        txtQty.Text = Request.Form("txtQty")
        txtQtyPO.Text = Request.Form("txtQtyPO")
        txtCost.Text = Request.Form("txtCost")

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

        If CheckRequiredField() Then
            Exit Sub
        End If

        If lCDbl(txtQty.Text) > lCDbl(txtQtyPO.Text) Then
            UserMsgBox(Me, "Qty Must Less Than or Equal Qty PO...!!!")
            txtQty.Focus()
            Exit Sub
        Else
            validateQty.Visible = False
        End If

        If CheckLocField() Then
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
        '        strNewIDFormat = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransfer) & Right(strAccYear, 2) & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth)
        '    Case Else
        '        strNewIDFormat = "NTI" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select

        '        strNewIDFormat = "NTI" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        strNewIDFormat = "STR" & "/" & strCompany & "/" & strLocation & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & strAccYear & "/"

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
            StrTxParam.Append("|")
            StrTxParam.Append(Trim(ddlInventoryBin.SelectedItem.Value))
            StrTxParam.Append("|")
            StrTxParam.Append(strNewIDFormat)
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|")
            StrTxParam.Append(txtTransporter.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(txtVehicle.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(txtDriverName.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(txtDriverIC.Text)

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
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                End If
            End Try
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.NoError And intErrNo = 0 Then

            arrPartNo = Split(Request.Form("lblSelectedItemCode"), " @ ")
            If arrPartNo.GetUpperBound(0) = 1 Then
                strItemCode = arrPartNo(0)
            Else
                strItemCode = lblSelectedItemCode.Text 'Request.Form("lblSelectedItemCode")
            End If

            strTxLnParam = lblStckTxID.Text & "|" & strItemCode.Trim & "|" & lCDbl(txtQty.Text) & "|" & lCDbl(txtCost.Text) & "|" & lstPR.SelectedItem.Value & "|" & lstItem.SelectedItem.Value

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
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                End If
            End Try
        End If


        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        LoadStockTxDetails()
        DisplayFromDB(False)
        BindGrid()
        BindPO(lstToLoc.SelectedItem.Value)
        BindItemCodeList()
        DisablePage()
        lblSelectedItemCode.Text = ""
        txtQty.Text = ""
        txtCost.Text = ""
        txtQtyPO.Text = ""
    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        'Dim TxID As String
        'Dim StrTxParam As New StringBuilder()
        'Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        'Dim strAllowVehicle As String
        'Dim strNewIDFormat As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If Len(lblStckTxID.Text) = 0 Then
            UserMsgBox(Me, "No Record Found...!!!")
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

        If CheckLocField() Then
            Exit Sub
        End If

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "UPDATESTR"
        strParamValue = "SET TransPorter='" & txtTransporter.Text & _
                                "',VehicleCode='" & txtVehicle.Text & _
                                "',DriverName='" & txtDriverName.Text & _
                                "',DriverIC='" & txtDriverIC.Text & _
                                "',ToLocCode='" & lstToLoc.SelectedItem.Value & _
                                "',Bin='" & ddlInventoryBin.SelectedItem.Value & _
                                "',Description='" & txtDesc.Text & _
                                "',CreateDate='" & strDate & _
                        "' Where StockTransferID='" & lblStckTxID.Text & "'"

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCdStckTxDet_UPD, _
                                                    strParamName, _
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            Exit Sub
        End Try


        UserMsgBox(Me, "Save Sucsess...!!!")

        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

        Dim strOpCodeGetAllLines As String = "IN_CLSTRX_STOCKTRANSFER_LINE_GET_ALL"
        Dim strOpCodeUpdLine As String = "IN_CLSTRX_STOCKTRANSFER_LINE_SYN"
        Dim strOpCodeGetItemDetail As String = "IN_CLSTRX_ITEM_DETAIL_GET"
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

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "UPDATESTR"
        strParamValue = "SET Status='2' Where StockTransferID='" & lblStckTxID.Text & "'"

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCdStckTxDet_UPD, _
                                                    strParamName, _
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            Exit Sub
        End Try


        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
        BindGrid()

    End Sub

    Sub btnDebitNote_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim strDocTypeId As String = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNote) & "|" & objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNoteLn)
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

        StrTxParam = lblStckTxID.Text & "|||||||||" & lstToLoc.SelectedItem.Value & "||||||" & objINtx.EnumStockTransferStatus.DbNote & "||"
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
                                                         strDocTypeId, _
                                                         ErrorChk, _
                                                         TxID)
            lblStckTxID.Text = TxID
            If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                lblError.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_NEWSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End If
        End Try
        LoadStockTxDetails()
        DisplayFromDB(True)
        DisablePage()
        BindGrid()
    End Sub

    Sub btnCancel_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
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
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=REMOVEINVENTORYITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End If
        End Try
        StrTxParam = lblStckTxID.Text & "|||||||||||||||" & objINtx.EnumStockTransferStatus.Cancelled & "||"

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
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CANCELSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                End If
            End Try
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            lblUnDel.Visible = True
        End If

        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|BIN|SIGN|UPDATEID"

        strParamValue = Trim(strLocation) & _
                        "|" & "IN_STOCKTRANSFER" & _
                        "|" & "IN_STOCKTRANSFERLN" & _
                        "|" & "STOCKTRANSFERID" & _
                        "|" & Trim(lblStckTxID.Text) & _
                        "|" & ddlInventoryBin.SelectedItem.Value & _
                        "|" & "+" & _
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

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "UPDATESTR"
        strParamValue = "SET Status='3' Where StockTransferID='" & lblStckTxID.Text & "'"

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCdStckTxDet_UPD, _
                                                    strParamName, _
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            Exit Sub
        End Try

       
        LoadStockTxDetails()
        DisplayFromDB(False)
        DisablePage()
        BindGrid()
    End Sub

    Sub btnEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
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

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "UPDATESTR"
        strParamValue = "SET Status='1' Where StockTransferID='" & lblStckTxID.Text & "'"

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCdStckTxDet_UPD, _
                                                    strParamName, _
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
            Exit Sub
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
        strTable = "IN_STOCKTRANSFER"
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
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_SHARE_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                End Try
                LoadStockTxDetails()
                DisplayFromDB(False)
                DisablePage()
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockTransferDet.aspx?strStockTxId=" & strStockTxId & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       "&strDocTitle=" & lblDocTitle.Text & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_Stocktransfer_List.aspx")
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_Trx_StockTransfer_Details.aspx")
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

        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        Else
            ddlInventoryBin.SelectedIndex = -1
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
        DisplayFromDB(False)
        DisablePage()
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
