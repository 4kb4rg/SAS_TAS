
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

Public Class IN_STOCKRESERVDET : Inherits Page

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
    Protected WithEvents txtRemarks As TextBox
    Protected WithEvents txtQty As TextBox
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
    
    Protected WithEvents Print As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents Back As ImageButton
    
	Protected WithEvents Approved As Button
	Protected WithEvents Verified As Button
	Protected WithEvents Confirm As Button
	
	Protected WithEvents lblConfirmErr As Label
    Protected WithEvents lblStatusHid As Label
    Protected WithEvents lblToLocErr As Label
    Protected WithEvents lblItemCodeErr As Label
    ' Protected WithEvents lstToLoc As DropDownList

    Protected WithEvents TxtItemCode As TextBox
    Protected WithEvents TxtItemName As TextBox


    Protected WithEvents FindIN As HtmlInputButton
    Protected WithEvents lblTo As Label
    Protected WithEvents lblPleaseSelectOne As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblDocTitle As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents ddlInventoryBin As DropDownList
    Protected WithEvents lblInventoryBin As Label
    Protected WithEvents hid_status As HtmlInputHidden    

    Protected WithEvents txtDate As TextBox
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Dim strDateFMT As String

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents TrLink As HtmlTableRow
    Protected objINtx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objGLtrx As New agri.GL.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmin As New agri.Admin.clsShare()

    Dim strOpCdStckTx_UPD As String = "IN_CLSTRX_STOCKRESERV_UPD"
    Dim strOpCdStckTx_ADD As String = "IN_CLSTRX_STOCKRESERV_ADD"
    Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
    Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_STOCKRESERV_LINE_GET"

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
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            Save.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Save).ToString())
            Print.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Print).ToString())
            PRDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(PRDelete).ToString())
            btnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnNew).ToString())
            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())

            If Not Page.IsPostBack Then
                'BindInventoryBinLevel("")
                BindDivision("")
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

    
    
    Sub BindDivision(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim objDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.BlkGrpCode Like '%" & strDivCode & "%' AND A.LocCode = '" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objDs.Tables(0).NewRow()
        dr("BlkGrpCode") = " "
        dr("Description") = "Please Select Division"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlInventoryBin.DataSource = objDs.Tables(0)
        ddlInventoryBin.DataTextField = "Description"
        ddlInventoryBin.DataValueField = "BlkGrpCode"
        ddlInventoryBin.DataBind()
        ddlInventoryBin.SelectedIndex = 0

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
        ddlInventoryBin.Enabled = False
        validateQty.Visible = False
        Save.Visible = False
        Print.Visible = False
        PRDelete.Visible = False
        ddlInventoryBin.Enabled = False
		
		Approved.Visible = False
		Verified.Visible = False
		Confirm.Visible = False
		
        
        Select Case Trim(lblStatusHid.Text)
            Case "9"
                PRDelete.Visible = True
                btnNew.Visible = True
                PRDelete.ImageUrl = "../../images/butt_undelete.gif"
                PRDelete.AlternateText = "Undelete"
                PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case "4" 'Active
                btnNew.Visible = True
                Print.Visible = True
				PRDelete.Visible = True				
				Approved.Visible = True
				Verified.Visible = False
				Confirm.Visible = False
		    Case "3" 'Approved
                btnNew.Visible = True
                Print.Visible = True
				PRDelete.Visible = True				
				Approved.Visible = False
				Verified.Visible = True
				Confirm.Visible = False
		   Case "2" 'Verified
                btnNew.Visible = True
                Print.Visible = True
				PRDelete.Visible = True				
				Approved.Visible = False
				Verified.Visible = False
				Confirm.Visible = True
		
		
            Case Else

                txtRemarks.Enabled = True
                ddlInventoryBin.Enabled = False
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
                Else
                    ddlInventoryBin.Enabled = True
                End If
        End Select
        DisableItemTable()
    End Sub

    Sub DisableItemTable()
        If lblStatusHid.Text = CStr(objINtx.EnumStockTransferStatus.Deleted) _
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
		Dim strOpCd_TransDivGet As String = "IN_CLSTRX_STOCKRESERV_DISPLAYLINE_GET"
        Dim intErrNo As Integer
        Dim objTransDivDs As New Object()

		strParamName="TRANSID"
        strParamValue = lblStckTxID.Text.Trim

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_TransDivGet, strParamName, strParamValue, objDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKRESERV_DISPLAYLINE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

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
	
	Function mtdGetStockreservStatus(ByVal n As String) As String

        Select Case Trim(n)
            Case "4"
                Return "Active"
            Case "3"
                Return "Approved"
            Case "2"
                Return "Verified"
			Case "1"
                Return "Confirm"
            Case Else
                Return "Delete"
        End Select


    End Function


    Sub LoadStockTxDetails()
        Dim strOpCd_TransDiv As String = "IN_CLSTRX_STOCKRESERV_DETAIL_GET"
        Dim intErrNo As Integer
        Dim objTransDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "LOCCODE|ID"
        strParamValue = strLocation & "|" & lblStckTxID.Text

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_TransDiv, strParamName, strParamValue, objTransDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKRESERV_DETAIL_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objTransDs.Tables(0).Rows.Count <> 0 Then
            lblStckTxID.Text = objTransDs.Tables(0).Rows(0).Item("StockReservID")
            lblAccPeriod.Text = objTransDs.Tables(0).Rows(0).Item("AccMonth") & "/" & objTransDs.Tables(0).Rows(0).Item("AccYear")


            CreateDate.Text = Format(objTransDs.Tables(0).Rows(0).Item("CreateDate"), "dd-MM-yyyy HH:mm:ss")
            UpdateDate.Text = Format(objTransDs.Tables(0).Rows(0).Item("UpdateDate"), "dd-MM-yyyy HH:mm:ss")
            UpdateBy.Text = objTransDs.Tables(0).Rows(0).Item("UpdateID")
            lblStatusHid.Text = objTransDs.Tables(0).Rows(0).Item("Status").Trim()
            Status.Text = mtdGetStockreservStatus(Trim(objTransDs.Tables(0).Rows(0).Item("Status")))
            txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Trim(objTransDs.Tables(0).Rows(0).Item("StockReservDate")))
		
        
            txtRemarks.Text = RTrim(objTransDs.Tables(0).Rows(0).Item("Remark"))
            'BindInventoryBinLevel(Trim(objTransDs.Tables(0).Rows(0).Item("FromBin")))
            'BindInventoryBinLevel2(Trim(objTransDs.Tables(0).Rows(0).Item("ToBin")))
        End If
    End Sub

    Function GetAutoNum(ByVal pBulan As String, ByVal pTahun As String, ByVal pLocCode As String) As String
        Dim strOpCd_AutoNum As String = "IN_CLSTRX_STOCKRESERV_AUTONUM"
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
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKRESERV_DETAIL_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objNumDs.Tables(0).Rows.Count = 1 Then
            nNOUrut = objNumDs.Tables(0).Rows(0).Item("StockReservID")
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
        Dim strOpCdStckTxLine_DEL As String = "IN_CLSTRX_STOCKRESERV_LINE_DEL"
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

        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_STOCKRESERV_LINE_ADD"
        Dim strOpCdStckTxLine_CheckStock As String = "IN_CLSTRX_STOCKRESERV_SALDOSTOCK_GET"
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


        If CheckRequiredField() Then
            Exit Sub
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        ''==CHECK SALDO STOCK
        'strParamName = "ITEM|LOC"
        'strParamValue = TxtItemCode.Text & "|" & _
        '                  strLocation

        'Try
        '    intErrNo = ObjOk.mtdGetDataCommon(strOpCdStckTxLine_CheckStock, strParamName, strParamValue, objStckDiv)
        'Catch Exp As System.Exception
        '    Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKRESERV_DETAIL_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        'End Try

        'If objStckDiv.Tables(0).Rows.Count <> 0 Then
        '    IntSaldoStock = objStckDiv.Tables(0).Rows(0).Item("QtyOnHand")
        'End If

		'temp remark aam 11/2014
        'If IntSaldoStock < Val(txtQty.Text) Then
        '    lblStock.Visible = True
        '    Exit Sub
        'Else
        '    lblStock.Visible = False
        'End If
		
        If Len(lblStckTxID.Text) = 0 Then
            StrNoTransF = GetAutoNum(strAccMonth, strAccYear, strLocation)
            strNewIDFormat = "RSV" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & StrNoTransF
            ParamNama = "ID|DT|LOC|FB|AM|AR|RK|ST|CD|UP|UI"
            ParamValue = strNewIDFormat & "|" & _
                         strDate & "|" & _
                         strLocation & "|" & _
                         ddlInventoryBin.SelectedItem.Value & "|" & _
                         strAccMonth & "|" & _
                         strAccYear & "|" & _
                         txtRemarks.Text & "|" & _
                         "4" & "|" & _
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
            ParamNama = "ID|IT|QT"
            ParamValue = lblStckTxID.Text & "|" & _
                          TxtItemCode.Text & "|" & _
                          txtQty.Text
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
        TxtItemCode.Text = """"

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
            ParamNama = "ID|DT|LOC|RK|ST|UP|UI"
            ParamValue = lblStckTxID.Text & "|" & _
                         Format(CDate(strDate), "yyyy-MM-dd") & "|" & _
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

	Sub btnApproved_Click(ByVal sender As Object, ByVal e As System.EventArgs)
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
            ParamNama = "ID|DT|LOC|RK|ST|UP|UI"
            ParamValue = lblStckTxID.Text & "|" & _
                         strDate & "|" & _
                         strLocation & "|" & _
                         txtRemarks.Text & "|" & _
                         "3" & "|" & _
                         Date.Now() & "|" & _
                         strUserId

            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdStckTx_UPd, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
        End If

        
        LoadStockTxDetails()
        BindGrid()
        DisablePage()

    End Sub

	Sub btnVerified_Click(ByVal sender As Object, ByVal e As System.EventArgs)
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
            ParamNama = "ID|DT|LOC|RK|ST|UP|UI"
            ParamValue = lblStckTxID.Text & "|" & _
                         strDate & "|" & _
                         strLocation & "|" & _
                         txtRemarks.Text & "|" & _
                         "2" & "|" & _
                         Date.Now() & "|" & _
                         strUserId

            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdStckTx_UPd, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
        End If

        
        LoadStockTxDetails()
        BindGrid()
        DisablePage()

    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
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
            ParamNama = "ID|DT|LOC|RK|ST|UP|UI"
            ParamValue = lblStckTxID.Text & "|" & _
                         strDate & "|" & _
                         strLocation & "|" & _
                         txtRemarks.Text & "|" & _
                         "1" & "|" & _
                         Date.Now() & "|" & _
                         strUserId

            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCdStckTx_UPd, ParamNama, ParamValue)
            Catch ex As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
            End Try
        End If

        
        LoadStockTxDetails()
        BindGrid()
        DisablePage()

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

        If lblStatusHid.Text = "4" Then

            If Len(lblStckTxID.Text) > 0 Then
                ParamNama = "ID|DT|LOC|RK|ST|UP|UI"
                ParamValue = lblStckTxID.Text & "|" & _
                             strdate & "|" & _
                             strLocation & "|" & _
                             txtRemarks.Text & "|" & _
                             "9" & "|" & _
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
                ParamNama = "ID|DT|LOC|RK|ST|UP|UI"
                ParamValue = lblStckTxID.Text & "|" & _
                             strdate & "|" & _
                             strLocation & "|" & _
                             txtRemarks.Text & "|" & _
                             "4" & "|" & _
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

        strUpdString = "where StockReservID = '" & strStockTxId & "'"
        strStatus = Trim(Status.Text)
        intStatus = CInt(Trim(lblStatusHid.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strTable = "IN_STOCKRESERV"
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
        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_STOCKRESERVDet.aspx?strStockTxId=" & strStockTxId & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       "&strDocTitle=" & lblDocTitle.Text & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_trx_StockReserv_list.aspx")
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_trx_STOCKRESERV_details.aspx")
    End Sub

    'Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
    '    Dim strText = "Please select Inventory Bin"
    '    Dim strText2 = "Please select Inventory Bin"

    '    'ddlInventoryBin.Items.Clear()
    '    ddlInventoryBin.Items.Add(New ListItem(strText, "0"))
    '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO), objINstp.EnumInventoryBinLevel.HO))
    '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central), objINstp.EnumInventoryBinLevel.Central))
    '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Other), objINstp.EnumInventoryBinLevel.Other))
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
    '    If Not Trim(pv_strInvBin) = "" Then
    '        With ddlInventoryBin
    '            .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
    '        End With
    '    Else
    '        ddlInventoryBin.SelectedIndex = -1
    '    End If
       
    'End Sub

    'Sub BindInventoryBinLevel2(ByVal pv_strInvBin As String)

    '    Dim strText2 = "Please select Inventory Bin"

     '   'ddlInventoryBin.Items.Clear()
     '   ddlInventoryBin2.Items.Add(New ListItem(strText2, "0"))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO), objINstp.EnumInventoryBinLevel.HO))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central), objINstp.EnumInventoryBinLevel.Central))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Other), objINstp.EnumInventoryBinLevel.Other))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinI), objINstp.EnumInventoryBinLevel.BinI))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinII), objINstp.EnumInventoryBinLevel.BinII))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIII), objINstp.EnumInventoryBinLevel.BinIII))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIV), objINstp.EnumInventoryBinLevel.BinIV))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinV), objINstp.EnumInventoryBinLevel.BinV))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinVI), objINstp.EnumInventoryBinLevel.BinVI))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinVII), objINstp.EnumInventoryBinLevel.BinVII))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinVIII), objINstp.EnumInventoryBinLevel.BinVIII))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIX), objINstp.EnumInventoryBinLevel.BinIX))
     '   ddlInventoryBin2.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinX), objINstp.EnumInventoryBinLevel.BinX))

     '   If Not Trim(pv_strInvBin) = "" Then
     '       With ddlInventoryBin2
     '           .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
     '       End With
     '   Else
     '       ddlInventoryBin2.SelectedIndex = -1
     '   End If
    'End Sub

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


End Class
