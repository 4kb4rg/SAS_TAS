
Imports System.Data

Public Class WS_Trx_StockTransact_List : Inherits Page

    Protected WithEvents dgStockTrx As DataGrid
    Protected WithEvents JobID As Label
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents lstItemCode As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents TypeID As Label
    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents txtQtyRtrn As TextBox
    Protected WithEvents lblQtyRtrn As Label
    Protected WithEvents lblQtyRecv As Label

    Protected objWS As New agri.WS.clsTrx()
    Protected objHRTrx As New agri.HR.clsTrx()
    Protected objINtx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objIN As New agri.IN.clsSetup()

    Dim strOppCd_ADD As String = "IN_CLSSETUP_PRODTYPE_LIST_ADD"
    Dim strOppCd_UPD As String = "IN_CLSSETUP_PRODTYPE_LIST_UPD"
    Dim strJobItem_Upd As String = "WS_CLSTRX_JOBSTOCKITEM_UPD"

    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intWSAR As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intWSAR = Session("SS_WSAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            JobID.Text = Request.QueryString("ID")
            TypeID.Text = Request.QueryString("Type")
            If SortExpression.Text = "" Then
                SortExpression.Text = "stk.TransactionDate"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgStockTrx.CurrentPageIndex = 0
        dgStockTrx.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label
        Dim IssQty As String
        Dim RtnQty As String
        Dim txt As TextBox

        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                lbl = e.Item.FindControl("Qty")
                IssQty = lbl.Text
                lbl = e.Item.FindControl("QtyRtn")
                RtnQty = lbl.Text
                If CInt(IssQty) - CInt(RtnQty) <= 0 Then
                    btn = e.Item.FindControl("Return")
                    btn.Visible = False
                End If
            Case ListItemType.EditItem
                txt = e.Item.FindControl("Remark")
                txt.Text = Trim(txt.Text)
        End Select


    End Sub
    Sub BindGrid()
        Dim PageNo As Integer
        Dim Lbl As Label
        Dim btn As LinkButton
        Dim intCnt As Integer

        dgStockTrx.DataSource = LoadData()
        dgStockTrx.DataBind()

        PageNo = dgStockTrx.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgStockTrx.PageCount
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgStockTrx.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgStockTrx.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet

        'Dim strParam As String
        'Dim strOppCd_GET As String = "WS_CLSTRX_JOBSTOCK_LIST_GET"


        'strParam = JobID.Text & "|" & SortExpression.Text & "|" & sortcol.Text & "|||"

        'Try
        '    intErrNo = objWS.mtdjobstockListget(strOppCd_GET, _
        '                                        strCompany, _
        '                                        strLocation, _
        '                                        strUserId, _
        '                                        strAccMonth, _
        '                                        strAccYear, _
        '                                        strParam, _
        '                                        objDataSet)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_JOBSTOCK&errmesg=" & Exp.ToString() & "&redirect=")
        'End Try
        'Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgStockTrx.CurrentPageIndex = 0
            Case "prev"
                dgStockTrx.CurrentPageIndex = _
                    Math.Max(0, dgStockTrx.CurrentPageIndex - 1)
            Case "next"
                dgStockTrx.CurrentPageIndex = _
                    Math.Min(dgStockTrx.PageCount - 1, dgStockTrx.CurrentPageIndex + 1)
            Case "last"
                dgStockTrx.CurrentPageIndex = dgStockTrx.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgStockTrx.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgStockTrx.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgStockTrx.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgStockTrx.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Protected Function EmpDataSet(ByVal pv_strEmpCode As String, ByRef pr_intIndex As Integer) As DataSet
        'Dim strOpCd As String = "WS_CLSTRX_MECHANIC_LIST_GET"
        'Dim objEmpDs As New DataSet()
        'Dim strParam As String
        'Dim intCnt As Integer
        'Dim dr As DataRow

        'pr_intIndex = 0
        'strParam = objHRTrx.EnumMechStatus.Yes & "|" & objHRTrx.EnumEmpStatus.Active & "|mst.EmpCode|ASC"

        'Try
        '    intErrNo = objWS.mtdGetMechanicList(strCompany, _
        '                                        strLocation, _
        '                                        strUserId, _
        '                                        strAccMonth, _
        '                                        strAccYear, _
        '                                        strOpCd, _
        '                                        strParam, _
        '                                        objEmpDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_JOBSTOCK_GET_MECHANIC&errmesg=" & Exp.ToString() & "&redirect=system/user/userlist.aspx")
        'End Try

        'For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
        '    objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode").Trim()
        '    objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") & " (" & objEmpDs.Tables(0).Rows(intCnt).Item("EmpName").Trim() & ")"
        '    If objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(pv_strEmpCode) Then
        '        pr_intIndex = intCnt + 1
        '    End If
        'Next

        'dr = objEmpDs.Tables(0).NewRow()
        'dr("EmpCode") = ""
        'dr("EmpName") = "Select Employee Code"
        'objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        'Return objEmpDs
    End Function

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lst As DropDownList
        Dim Lbl As Label
        Dim Txt As TextBox
        Dim Range As RangeValidator
        Dim ddlEmpCode As DropDownList
        Dim intSelected As Integer
        Dim ItemCode As String
        Dim desc As String
        Dim Qty As String
        Dim Cost As String
        Dim TotalQty As String
        Dim TotalCost As Double
        Dim TotalReturn As Double
        Dim TotalIssue As Double
        Dim intCnt As Integer

        dgStockTrx.EditItemIndex = CInt(E.Item.ItemIndex)
        BindGrid()
        blnUpdate.Text = "Return"

        Lbl = dgStockTrx.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblEmpcode")
        lst = dgStockTrx.Items.Item(CInt(E.Item.ItemIndex)).FindControl("EmpCode")
        lst.DataSource = EmpDataSet(Lbl.Text, intSelected)
        lst.DataValueField = "EmpCode"
        lst.DataTextField = "EmpName"
        lst.DataBind()
        lst.SelectedIndex = intSelected
        lst.Visible = False

        Lbl = E.Item.FindControl("QtyRtn")
        TotalReturn = Lbl.Text

        lst = dgStockTrx.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lstItemCode")
        lst.Visible = False
        Lbl = dgStockTrx.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ItemCode")
        Lbl.Visible = True
        Lbl = dgStockTrx.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyRecv")
        Lbl.Visible = True
        TotalIssue = Lbl.Text

        Txt = dgStockTrx.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Quantity")
        Txt.Visible = False
        Range = dgStockTrx.Items.Item(CInt(E.Item.ItemIndex)).FindControl("RangeRtn")
        Range.Visible = True
        Range.MaximumValue = CDbl(TotalIssue - TotalReturn)
    End Sub

    Sub CheckItem(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
        Dim lst As DropDownList
        Dim txt As TextBox
        Dim dsItem As DataSet
        Dim paramItem As String

        lst = dgStockTrx.Items.Item(CInt(dgStockTrx.EditItemIndex)).FindControl("lstItemCode")
        paramItem = lst.SelectedItem.Value & "||" & Trim(strLocation)
        Try
            intErrNo = objIN.mtdGetMasterDetail(strOpCdItem_Details_GET, paramItem, dsItem)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_STOCKITEM_DETAILS&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        txt = dgStockTrx.Items.Item(CInt(dgStockTrx.EditItemIndex)).FindControl("txtCost")

        If Trim(dsItem.Tables(0).Rows(0).Item("itemtype")) = objIN.EnumInventoryItemType.DirectCharge Then
            txt.Visible = True
        Else
            txt.Visible = False
        End If

    End Sub


    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        'Dim strItemDet_Upd As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"
        'Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
        'Dim strItemDetails_Get As String = "IN_CLSSETUP_INVITEM_DETAILS_GET"
        'Dim strJobItemDetails_Get As String = "WS_CLSTRX_JOBSTOCKITEM_DETAILS_GET"
        'Dim strJob_UPD As String = "WS_CLSTRX_JOB_UPD"
        'Dim strJobItem_Add As String = "WS_CLSTRX_JOBSTOCKITEM_ADD"
        'Dim strJobItem_Upd As String = "WS_CLSTRX_JOBSTOCKITEM_UPD"

        'Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        'Dim lst As DropDownList
        'Dim Range As RangeValidator
        'Dim Req As RequiredFieldValidator
        'Dim Lbl As Label
        'Dim Txt As TextBox
        'Dim ItemCode As String
        'Dim MechanicCode As String
        'Dim Remark As String
        'Dim Qty As String
        'Dim RtnQty As Double
        'Dim trxtype As Integer
        'Dim TotalIssue As Double
        'Dim PassStr As String
        'Dim Cost As String
        'Dim strParam As String
        'Dim JobStockID As String
        'Dim DCCost As String

        'If blnUpdate.Text = "Issue" Then
        '    Req = E.Item.FindControl("validateReq")
        '    Req.Visible = False
        '    lst = E.Item.FindControl("lstItemCode")
        '    ItemCode = lst.SelectedItem.Value
        '    Txt = E.Item.FindControl("Quantity")
        '    Qty = Txt.Text
        '    lst = E.Item.FindControl("EmpCode")
        '    MechanicCode = lst.SelectedItem.Value
        '    Txt = E.Item.FindControl("Remark")
        '    Remark = Txt.Text
        '    trxtype = objWS.EnumStockTrxType.Issued

        '    strParam = Trim(ItemCode) & "|" & Trim(Qty) & "|0"
        '    Txt = E.Item.FindControl("txtCost")

        '    If Txt.Visible = True Then
        '        DCCost = Txt.Text
        '    Else
        '        DCCost = ""
        '    End If

        'ElseIf blnUpdate.Text = "Return" Then

        '    Req = E.Item.FindControl("validateQty")
        '    Req.Visible = False
        '    Lbl = E.Item.FindControl("ItemCode")
        '    ItemCode = Lbl.Text
        '    Lbl = E.Item.FindControl("JobStockID")
        '    JobStockID = Lbl.Text
        '    Lbl = E.Item.FindControl("lblQtyRecv")
        '    TotalIssue = Lbl.Text
        '    Txt = E.Item.FindControl("Quantity")
        '    Qty = Txt.Text
        '    Txt = E.Item.FindControl("txtQtyRtrn")
        '    RtnQty = Txt.Text
        '    lst = E.Item.FindControl("EmpCode")
        '    MechanicCode = lst.SelectedItem.Value
        '    Txt = E.Item.FindControl("Remark")
        '    Remark = Txt.Text
        '    trxtype = objWS.EnumJobStockType.Returned
        '    strParam = Trim(ItemCode) & "|-" & Trim(RtnQty) & "|0"

        '    If TotalIssue - RtnQty < 0 Then
        '        Lbl = E.Item.FindControl("lblErr")
        '        Lbl.Visible = True
        '        Exit Sub
        '    End If

        'End If

        'PassStr = _
        'Trim(JobStockID) & "|" & _
        'Trim(JobID.Text) & "|" & _
        'Trim(ItemCode) & "|" & _
        'Trim(Remark) & "|" & _
        'Trim(Qty) & "|" & _
        'Trim(RtnQty) & "|" & _
        'MechanicCode & "|" & _
        'DCCost
        'Try
        '    intErrNo = objINtx.mtdUpdInvItemQtyWithCheck(strItemDet_Upd, _
        '                                                 strOpCdItem_Details_GET, _
        '                                                 strCompany, _
        '                                                 strLocation, _
        '                                                 strUserId, _
        '                                                 strAccMonth, _
        '                                                 strAccYear, _
        '                                                 strParam, _
        '                                                 ErrorChk)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPDATE_INVITEM&errmesg=" & Exp.ToString() & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        'End Try
        'If intErrNo = 0 And ErrorChk = objINtx.EnumInventoryErrorType.NoError Then
        '    Try
        '        intErrNo = objWS.mtdUpdJobstock(strJob_UPD, _
        '                                        strItemDetails_Get, _
        '                                        strJobItemDetails_Get, _
        '                                        strJobItem_Add, _
        '                                        strJobItem_Upd, _
        '                                        trxtype, _
        '                                        TypeID.Text, _
        '                                        strUserId, _
        '                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.WorkShopJobStock), _
        '                                        PassStr)

        '    Catch Exp As System.Exception
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPDATE_JOBDETAILS&errmesg=" & Exp.ToString() & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        '    End Try
        'End If

        'If Not ErrorChk = objINtx.EnumInventoryErrorType.NoError Then
        '    Lbl = E.Item.FindControl("lblErr")
        '    Lbl.Visible = True
        'Else
        '    dgStockTrx.EditItemIndex = -1
        '    BindGrid()
        'End If
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If CInt(E.Item.ItemIndex) = 0 And dgStockTrx.Items.Count = 1 And Not dgStockTrx.CurrentPageIndex = 0 Then
            dgStockTrx.CurrentPageIndex = dgStockTrx.PageCount - 2
            BindGrid()
            BindPageList()
        End If
        dgStockTrx.EditItemIndex = -1
        BindGrid()
    End Sub


    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim strEmpCode As String = ""
        Dim intSelected As Integer = 0
        Dim ddlEmpCode As DropDownList
        Dim Txt As TextBox
        Dim Val As RegularExpressionValidator
        Dim Req As RangeValidator
        Dim lbl As Label

        blnUpdate.Text = "Issue"
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("ItemCode") = ""
        newRow.Item("Transactiondate") = "1/1/1900"
        newRow.Item("Description") = ""
        newRow.Item("Qty") = 0
        newRow.Item("Price") = 0
        newRow.Item("QtyReturned") = 0
        newRow.Item("Cost") = 0
        dataSet.Tables(0).Rows.Add(newRow)

        dgStockTrx.DataSource = dataSet
        dgStockTrx.DataBind()

        dgStockTrx.CurrentPageIndex = dgStockTrx.PageCount - 1
        dgStockTrx.DataBind()
        dgStockTrx.EditItemIndex = dgStockTrx.Items.Count - 1
        dgStockTrx.DataBind()

        Txt = dgStockTrx.Items.Item(dgStockTrx.Items.Count - 1).FindControl("txtQtyRtrn")
        Txt.Visible = False
        Val = dgStockTrx.Items.Item(dgStockTrx.Items.Count - 1).FindControl("ValidatortxtQtyRtrn")
        Val.Visible = False
        Req = dgStockTrx.Items.Item(dgStockTrx.Items.Count - 1).FindControl("RangeRtn")
        Req.Visible = False

        ddlEmpCode = dgStockTrx.Items.Item(CInt(dgStockTrx.EditItemIndex)).FindControl("EmpCode")
        ddlEmpCode.DataSource = EmpDataSet(strEmpCode, intSelected)
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataTextField = "EmpName"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intSelected

        BindItemCodeList()
    End Sub


    Sub BindItemCodeList()

        'Dim strJobItem_Get As String = "WS_CLSTRX_JOBSTOCKITEM_LIST_GET"
        'Dim dsItemCodeDropList As DataSet
        'Dim intCnt As Integer
        'Dim drinsert As DataRow

        'Try
        '    intErrNo = objWS.mtdJobStockItemGet(strJobItem_Get, _
        '                                        strCompany, _
        '                                        strLocation, _
        '                                        strUserId, _
        '                                        strAccMonth, _
        '                                        strAccYear, _
        '                                        JobID.Text, _
        '                                        dsItemCodeDropList)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTOSERVTYPEDROPDOWNLIST&errmesg=" & Exp.ToString() & "&redirect=WS/Trx/WS_Trx_Job_List.aspx")
        'End Try

        'For intCnt = 0 To dsItemCodeDropList.Tables(0).Rows.Count - 1
        '    dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")

        '    If dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then
        '        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") & " @ " & _
        '                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("PartNo") & " ( " & _
        '                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
        '                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand") & " units"
        '    Else
        '        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") & " ( " & _
        '                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
        '                                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand") & " units"
        '    End If
        'Next intCnt
        'drinsert = dsItemCodeDropList.Tables(0).NewRow()
        'drinsert("ItemCode") = " "
        'drinsert("Description") = "Please select Item code"
        'dsItemCodeDropList.Tables(0).Rows.InsertAt(drinsert, 0)

        'lstItemCode = dgStockTrx.Items.Item(dgStockTrx.Items.Count - 1).FindControl("lstItemCode")
        'lstItemCode.DataSource = dsItemCodeDropList.Tables(0)
        'lstItemCode.DataValueField = "ItemCode"
        'lstItemCode.DataTextField = "Description"
        'lstItemCode.DataBind()

        'If Not dsItemCodeDropList Is Nothing Then
        '    dsItemCodeDropList = Nothing
        'End If
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/WS_Rpt_IssueReturn.aspx?strJobId=" & JobID.Text & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WS_Trx_Job_Detail.aspx?ID=" & JobID.Text)
    End Sub


End Class
