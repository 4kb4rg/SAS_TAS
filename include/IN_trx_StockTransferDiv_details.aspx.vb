
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

Public Class IN_trx_StockTransferDiv : Inherits Page

    Dim strDateFMT As String
 
    Protected ToLocTag AS New Label
    Protected objINtx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objGLtrx As New agri.GL.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmin As New agri.Admin.clsShare()

    Dim strOpCdStckTx_UPD As String = "IN_CLSTRX_STOCKTRANSFERDIV_UPD"
    Dim strOpCdStckTx_ADD As String = "IN_CLSTRX_STOCKTRANSFERDIV_ADD"
    Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
    Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"

    'Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_STOCKTRANSFER_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_STOCKTRANSFERDIV_LINE_GET"

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

    Dim ObjOk As New agri.GL.ClsTrx()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strLocLevel As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer

    Dim ParamNama As String
    Dim ParamValue As String = ""

    Dim strParamName As String
    Dim strParamValue As String

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
        strLocLevel = Session("SS_LOCLEVEL")
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
                ''BindInventoryBinLevel2("")
                BindStorageFr("")
                BindStorageTo("")

                lblStckTxID.Text = Request.QueryString("Id")
                LoadStockTxDetails()
                BindGrid()
                If lblStckTxID.Text = "" Then
                    txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    TrLink.Visible = False
                End If

                'BindItemCodeList()

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

            TxtItemCode.Attributes.Add("readonly", "readonly")
            TxtItemName.Attributes.Add("readonly", "readonly")

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

    Sub DisablePage()
        txtDesc.Enabled = False
        txtRemarks.Enabled = False
        ddlInventoryBin.Enabled = False
        validateQty.Visible = False
        Save.Visible = False
        Confirm.Visible = False
        Print.Visible = False
        Cancel.Visible = False
        PRDelete.Visible = False
        ddlInventoryBin.Enabled = False

        Select Case Trim(lblStatusHid.Text)
            Case Trim(CStr(objINtx.EnumStockTransferStatus.Deleted))
                PRDelete.Visible = True
                btnNew.Visible = True
                PRDelete.ImageUrl = "../../images/butt_undelete.gif"
                PRDelete.AlternateText = "Undelete"
                PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objINtx.EnumStockTransferStatus.Confirmed))
                btnNew.Visible = True
                Print.Visible = True
                Cancel.Visible = True
            Case Trim(CStr(objINtx.EnumStockTransferStatus.DBNote))
                btnNew.Visible = True
                Print.Visible = True
            Case Else

                txtDesc.Enabled = True
                txtRemarks.Enabled = True
                ddlInventoryBin.Enabled = False
   
                validateQty.Visible = True
                Save.Visible = True
                Cancel.Visible = False
                If Trim(lblStckTxID.Text) <> "" Then
                    Confirm.Visible = True
                    Print.Visible = True
                    PRDelete.Visible = True
                    btnNew.Visible = True
                    PRDelete.ImageUrl = "../../images/butt_delete.gif"
                    PRDelete.AlternateText = "Delete"
                    PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Else
                    ddlInventoryBin.Enabled = True
           
           
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

    Function CheckRequiredField() As Boolean
        If Request.Form("txtItemCode") = "" Then
            lblItemCodeErr.Visible = True
            Return True
        Else
            Return False
        End If
    End Function

    Protected Function LoadDataGrid() As DataSet
        'Dim intCnt As Integer
        Dim objDs As New Object()
		Dim strOpCd_TransDivGet As String = "IN_CLSTRX_STOCKTRANSFERDIV_DISPLAYLINE_GET"
        Dim intErrNo As Integer
        Dim objTransDivDs As New Object()

        Dim strStorageCodeFr As String = ""
        Dim strStorageCodeTo As String = ""

		strParamName="TRANSID"
        strParamValue = lblStckTxID.Text.Trim

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_TransDivGet, strParamName, strParamValue, objDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKTRANSFERDIV_DISPLAYLINE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        lstStorageFr.Enabled = True
        lstStorageTo.Enabled = True

        For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
            lstStorageFr.Enabled = False
            lstStorageTo.Enabled = False
            strStorageCodeFr = Trim(objDs.Tables(0).Rows(intCnt).Item("StorageCodeFr"))
            strStorageCodeTo = Trim(objDs.Tables(0).Rows(intCnt).Item("StorageCodeTo"))
        Next

        BindStorageFr(strStorageCodeFr)
        BindStorageTo(strStorageCodeTo)

        If objDs.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
        Else
            TrLink.Visible = False
        End If

        Return objDs
    End Function
    
	Sub BindGrid()
        Dim PageNo As Integer
		Dim dsData As DataSet
        dsData = LoadDataGrid()
		
        dgStkTx.DataSource = dsData
        dgStkTx.DataBind()
    End Sub

    Sub LoadStockTxDetails()
        Dim strOpCd_TransDiv As String = "IN_CLSTRX_STOCKTRANSFERDIV_DETAIL_GET"
        Dim intErrNo As Integer
        Dim objTransDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "LOCCODE|ID"
        strParamValue = strLocation & "|" & lblStckTxID.Text

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_TransDiv, strParamName, strParamValue, objTransDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKTRANSFERDIV_DETAIL_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objTransDs.Tables(0).Rows.Count <> 0 Then
            lblStckTxID.Text = objTransDs.Tables(0).Rows(0).Item("StockTransferDivID")
            lblAccPeriod.Text = objTransDs.Tables(0).Rows(0).Item("AccMonth") & "/" & objTransDs.Tables(0).Rows(0).Item("AccYear")


            CreateDate.Text = Format(objTransDs.Tables(0).Rows(0).Item("CreateDate"), "dd-MM-yyyy HH:mm:ss")
            UpdateDate.Text = Format(objTransDs.Tables(0).Rows(0).Item("UpdateDate"), "dd-MM-yyyy HH:mm:ss")
            UpdateBy.Text = objTransDs.Tables(0).Rows(0).Item("UpdateID")
            lblStatusHid.Text = objTransDs.Tables(0).Rows(0).Item("Status").Trim()
            Status.Text = objINtx.mtdGetStockIssueStatus(Trim(objTransDs.Tables(0).Rows(0).Item("Status")))
            txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Trim(objTransDs.Tables(0).Rows(0).Item("StockTransferDate")))
		
        
            txtDesc.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("Description"))
            txtRemarks.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("Remark"))
            BindInventoryBinLevel(Trim(objTransDs.Tables(0).Rows(0).Item("FromBin")))
 
        End If
    End Sub

    Function GetAutoNum(ByVal pBulan As String, ByVal pTahun As String, ByVal pLocCode As String) As String
        Dim strOpCd_AutoNum As String = "IN_CLSTRX_STOCKTRANSFERDIV_AUTONUM"
        Dim intErrNo As Integer
        Dim objNumDs As New Object()
        Dim nNOUrut As Long = 0
        Dim Newno As Long = 0
        Dim NewNoStr As String


        strParamName = "BULAN|TAHUN|LOC"
        strParamValue = pBulan & "|" & pTahun & "|" & pLocCode

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_AutoNum, strParamName, strParamValue, objNumDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKTRANSFERDIV_DETAIL_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objNumDs.Tables(0).Rows.Count = 1 Then
            nNOUrut = objNumDs.Tables(0).Rows(0).Item("StockTransferDivID")
        Else
            nNOUrut = 0
        End If

        Newno = nNOUrut + 1
        NewNoStr = Strings.Right("0000" & CStr(Newno), 4)
        Return NewNoStr
    End Function

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        Dim lbl As Label
        Dim ItemCode As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strOpCdStckTxLine_DEL As String = "IN_CLSTRX_STOCKTRANSFERDIV_LINE_DEL"
        Dim strOpCdStckTxLine_Det_GET As String = "IN_CLSTRX_STOCKTRANSFERLINE_DETAILS_GET"


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
        txtRemarks.Text = lblStatusHid.Text

        If lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.Active) Then
            lbl = E.Item.FindControl("ItemCode")
            ItemCode = lbl.Text

            ParamNama = "STOCKTRANSFERID|ITEMCODE"
            ParamValue = lblStckTxID.Text & "|" & ItemCode

            If Len(lblStckTxID.Text) > 0 Then

                ParamNama = "STOCKTRANSFERID|ITEMCODE"
                ParamValue = lblStckTxID.Text & "|" & _
                             ItemCode
                Try
                    intErrNo = ObjOk.mtdInsertDataCommon(strOpCdStckTxLine_DEL, ParamNama, ParamValue)
                Catch ex As Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                End Try
            End If

            'Try
            '    intErrNo = objINtx.mtdUpdStockTransferDetail(strOpCdStckTxDet_ADD, _
            '                                                 strOpCdStckTxDet_UPD, _
            '                                                 strOpCdStckTxLine_GET, _
            '                                                 strCompany, _
            '                                                 strLocation, _
            '                                                 strUserId, _
            '                                                 strAccMonth, _
            '                                                 strAccYear, _
            '                                                 strARAccMonth, _
            '                                                 strARAccYear, _
            '                                                 StrTxParam, _
            '                                                 objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransfer), _
            '                                                 ErrorChk, _
            '                                                 TxID)
            '    If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
            '        lblError.Visible = True
            '    End If

            '    lblStckTxID.Text = TxID
            'Catch Exp As System.Exception
            '    If intErrNo <> -5 Then
            '        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            '    End If
            'End Try

            LoadStockTxDetails()
            BindGrid()
        End If
    End Sub

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_STOCKTRANSFERDIV_LINE_ADD"
        Dim strOpCdStckTxLine_CheckStock As String = "IN_CLSTRX_STOCKTRANSFERDIV_SALDOSTOCK_GET"
        Dim objStckDiv As New Object()
        Dim strTxLnParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim arrPartNo As Array
        Dim strItemCode As String
        Dim strNewIDFormat As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        Dim StrNoTransF As String = vbNullString
        Dim IntSaldoStock As Double

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
 
        If lstStorageFr.SelectedItem.Value = "" Then
            lblstoragemsgFr.Visible = True
            Exit Sub
        Else
            lblstoragemsgFr.Visible = False
        End If

        If lstStorageTo.SelectedItem.Value = "" Then
            lblstoragemsgTo.Visible = True
            Exit Sub
        Else    
            lblstoragemsgTo.Visible = False
        End If

        If lstStorageFr.SelectedItem.Value = lstStorageTo.SelectedItem.Value Then
            lblstoragemsgFr.Visible = True
            lblstoragemsgTo.Visible = True
            Exit Sub
        Else
            lblstoragemsgFr.Visible = True
            lblstoragemsgTo.Visible = True

        End If

        If CheckRequiredField() Then
            Exit Sub
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
 
         ''////control stock by storage
        Dim vStockBalance As double=lGetStockBalanceStorage(strDate,lstStorageFr.SelectedItem.value,0)
        IF vStockBalance <  CDbl(0 & txtQty.text) Then
            UserMsgBox(Me,"Stock Balance Less Then Qty Issue " & vbCrLf & " Stock Balance : " & vStockBalance & "")
            txtQty.Focus
            Exit sub
        End If

 
        If Len(lblStckTxID.Text) = 0 Then
            StrNoTransF = GetAutoNum(strAccMonth, strAccYear, strLocation)
            strNewIDFormat = "DTI" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & StrNoTransF
            ParamNama = "ID|DT|DES|LOC|FB|TB|AM|AR|RK|ST|CD|UP|UI"
            ParamValue = strNewIDFormat & "|" & _
                         strDate & "|" & _
                         txtDesc.Text & "|" & _
                         strLocation & "|" & _
                         ddlInventoryBin.SelectedItem.Value & "|" & _
                         ddlInventoryBin.SelectedItem.Value & "|" & _
                         strAccMonth & "|" & _
                         strAccYear & "|" & _
                         txtRemarks.Text & "|" & _
                         "1" & "|" & _
                         Date.Now() & "|" & _
                         Date.Now() & "|" & _
                         strUserId
            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdStckTx_ADD, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
            lblStckTxID.Text = strNewIDFormat
        End If

        If Len(lblStckTxID.Text) > 0 Then
            ParamNama = "ID|IT|QT|PRICE|AMOUNT|PRICEAMOUNT|STFR|STTO"
            ParamValue = lblStckTxID.Text & "|" & _
                          TxtItemCode.Text & "|" & _
                          txtQty.Text & "|" & _
                          unitcost.Text & "|" & _
                          cdbl(0 & unitcost.Text) * cdbl(0 & txtQty.Text)  & "|" & _
                          cdbl(0 & unitcost.Text) * cdbl(0 & txtQty.Text)  & "|" & _
                          lstStorageFr.SelectedItem.Value   & "|" & _
                          lstStorageTo.SelectedItem.Value

            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdStckTxLine_ADD, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
        End If
 
        LoadStockTxDetails()
        BindGrid()
        DisablePage()
        txtQty.Text = ""
        TxtItemCode.Text = ""
        TxtItemName.Text=""
        unitcost.Text=""

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

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If



        If Len(lblStckTxID.Text) > 0 Then
            ParamNama = "ID|DT|DS|LOC|RK|ST|UP|UI"
            ParamValue = lblStckTxID.Text & "|" & _
                         Format(CDate(strDate), "yyyy-MM-dd") & "|" & _
                         txtDesc.Text & "|" & _
                         strLocation & "|" & _
                         txtRemarks.Text & "|" & _
                         "1" & "|" & _
                          Format(Date.Now(), "yyyy-MM-dd HH:mm:ss") & "|" & _
                         strUserId
            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdStckTx_UPD, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
        End If

        LoadStockTxDetails()
        DisablePage()
    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
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

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If



        If Not LoadDataGrid.Tables(0).Rows.Count > 0 Then
            lblConfirmErr.Visible = True
            Exit Sub
        End If

        If Len(lblStckTxID.Text) > 0 Then
            ParamNama = "ID|DT|DS|LOC|RK|ST|UP|UI"
            ParamValue = lblStckTxID.Text & "|" & _
                         strDate & "|" & _
                         txtDesc.Text & "|" & _
                         strLocation & "|" & _
                         txtRemarks.Text & "|" & _
                         "2" & "|" & _
                         Date.Now() & "|" & _
                         strUserId

            'ParamValue = lblStckTxID.Text & "|" & _
            '             Format(strDate, "yyyy-MM-dd") & "|" & _
            '             txtDesc.Text & "|" & _
            '             strLocation & "|" & _
            '             txtRemarks.Text & "|" & _
            '             "2" & "|" & _
            '              Format(Date.Now(), "yyyy-MM-dd HH:mm:ss") & "|" & _
            '             strUserId
            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdStckTx_UPd, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.NoError Then
            Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""

            strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"
            strParamValue = Trim(strLocation) & _
                            "|" & "IN_STOCKTRANSFERDIV" & _
                            "|" & "IN_STOCKTRANSFERDIVLN" & _
                            "|" & "STOCKTRANSFERDIVID" & _
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
        End If


        If ErrorChk = objINtx.EnumInventoryErrorType.NoError Then
            Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""

            strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"
            strParamValue = Trim(strLocation) & _
                            "|" & "IN_STOCKTRANSFERDIV" & _
                            "|" & "IN_STOCKTRANSFERDIVLN" & _
                            "|" & "STOCKTRANSFERDIVID" & _
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
        End If

        LoadStockTxDetails()
        BindGrid()
        DisablePage()

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

        If Len(lblStckTxID.Text) > 0 Then
            ParamNama = "ID|DT|DS|LOC|RK|ST|UP|UI"
            ParamValue = lblStckTxID.Text & "|" & _
                         strDate & "|" & _
                         txtDesc.Text & "|" & _
                         strLocation & "|" & _
                         txtRemarks.Text & "|" & _
                         "3" & "|" & _
                          Date.Now() & "|" & _
                         strUserId
            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdStckTx_UPD, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
        End If

        If Trim(CStr(objINtx.EnumStockTransferStatus.Confirmed)) Then
            If ErrorChk = objINtx.EnumInventoryErrorType.NoError Then
                Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
                Dim strParamName As String = ""
                Dim strParamValue As String = ""

                strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"
                strParamValue = Trim(strLocation) & _
                                "|" & "IN_STOCKTRANSFERDIV" & _
                                "|" & "IN_STOCKTRANSFERDIVLN" & _
                                "|" & "STOCKTRANSFERDIVID" & _
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
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                End Try
            End If


            If ErrorChk = objINtx.EnumInventoryErrorType.NoError Then
                Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
                Dim strParamName As String = ""
                Dim strParamValue As String = ""

                strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"
                strParamValue = Trim(strLocation) & _
                                "|" & "IN_STOCKTRANSFERDIV" & _
                                "|" & "IN_STOCKTRANSFERDIVLN" & _
                                "|" & "STOCKTRANSFERDIVID" & _
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
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                End Try
            End If
        End If

        LoadStockTxDetails()
        DisablePage()
        BindGrid()
    End Sub

    Sub btnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        Dim intStatus As String = 0

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

        If lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.Active) Then

            If Len(lblStckTxID.Text) > 0 Then
                ParamNama = "ID|DT|DS|LOC|RK|ST|UP|UI"
                ParamValue = lblStckTxID.Text & "|" & _
                             strdate & "|" & _
                             txtDesc.Text & "|" & _
                             strLocation & "|" & _
                             txtRemarks.Text & "|" & _
                             objINtx.EnumStockTransferStatus.Deleted & "|" & _
                              Format(Date.Now(), "yyyy-MM-dd HH:mm:ss") & "|" & _
                             strUserId
                Try
                    intErrNo = ObjOk.mtdInsertDataCommon(strOpCdStckTx_UPD, ParamNama, ParamValue)
                Catch ex As Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                End Try
            End If
        Else
            If Len(lblStckTxID.Text) > 0 Then
                ParamNama = "ID|DT|DS|LOC|RK|ST|UP|UI"
                ParamValue = lblStckTxID.Text & "|" & _
                             strdate & "|" & _
                             txtDesc.Text & "|" & _
                             strLocation & "|" & _
                             txtRemarks.Text & "|" & _
                             objINtx.EnumStockTransferStatus.Active & "|" & _
                              Format(Date.Now(), "yyyy-MM-dd HH:mm:ss") & "|" & _
                             strUserId
                Try
                    intErrNo = ObjOk.mtdInsertDataCommon(strOpCdStckTx_UPD, ParamNama, ParamValue)
                Catch ex As Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                End Try
            End If
        End If

        LoadStockTxDetails()
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

        strUpdString = "where StockTransferDivID = '" & strStockTxId & "'"
        strStatus = Trim(Status.Text)
        intStatus = CInt(Trim(lblStatusHid.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strTable = "IN_StockTransferDiv"
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
                 DisablePage()
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        txtRemarks.Text = strStockTxId
        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockTransferDivDet.aspx?strStockTxId=" & strStockTxId & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       "&strDocTitle=" & lblDocTitle.Text & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_StocktransferDiv_List.aspx")
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_trx_StockTransferDiv_details.aspx")
    End Sub

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
        Dim strText = "Please select Inventory Bin"
        Dim strText2 = "Please select Inventory Bin"

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
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinVIII), objINstp.EnumInventoryBinLevel.BinVIII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIX), objINstp.EnumInventoryBinLevel.BinIX))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinX), objINstp.EnumInventoryBinLevel.BinX))
        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        Else
            ddlInventoryBin.SelectedIndex = -1
        End If
       
    End Sub


    Sub BindStorageFr(ByVal pv_strcode As String)
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
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

        lstStorageFr.DataSource = objdsST.Tables(0)
        lstStorageFr.DataValueField = "StorageCode"
        lstStorageFr.DataTextField = "Description"
        lstStorageFr.DataBind()
        lstStorageFr.SelectedIndex = intSelectedIndex

    End Sub


    Sub BindStorageTo(ByVal pv_strcode As String)
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
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

        lstStorageTo.DataSource = objdsST.Tables(0)
        lstStorageTo.DataValueField = "StorageCode"
        lstStorageTo.DataTextField = "Description"
        lstStorageTo.DataBind()
        lstStorageTo.SelectedIndex = intSelectedIndex

    End Sub

    Function lGetStockBalanceStorage(ByVal pTransDate AS String,ByVal pStorage AS String,ByVal pOldValue As double ) AS Double
        Dim strOpCode_StorageBalance AS String="IN_CLSTRX_STOCKBALANCE_CONTROL_INOUT"
        Dim vValueExist as Double=0
        Dim vQtyStockBalance AS Double=0
        Dim dtSet AS new Dataset

        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "ITEMCODE|STORAGE|VEXIST|VNEW|DT|LOC"
        strParamValue = txtItemCode.text.trim & "|" & lstStorageFr.SelectedItem.value & "|" & pOldValue & "|" &  CDbl(txtQty.text) & "|" & pTransDate & "|" &  strlocation

        Try
           intErrNo = objGLtrx.mtdGetDataCommon(strOpCode_StorageBalance, _
                                               strParamName, _
                                               strParamValue, _
                                               dtSet)

        Catch Exp As System.Exception
           Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKISSUE_STOCKRECEIVE_DETAILS_GET&errmesg=" & Exp.ToString() & "&redirect=IN/Trx/IN_Trx_StockIssue_Details.aspx")
        End Try

       If dtSet.Tables(0).Rows.Count > 0 Then       
           vQtyStockBalance = dtSet.Tables(0).Rows(0).Item("QtyEnd")       
        End If

        Return vQtyStockBalance
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


End Class
