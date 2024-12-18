Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class IN_StockIssue : Inherits Page

    Protected WithEvents dgStockTx as DataGrid
    Protected WithEvents lblUnDel As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label
    Protected WithEvents SQLStatement as Label
    Protected WithEvents lstDropList as DropDownList
    Protected WithEvents StatusList as DropDownList
    Protected WithEvents srchStatusList as DropDownList
    Protected WithEvents srchIssueList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol as Label
    Protected WithEvents blnUpdate as Label
    Protected WithEvents curStatus as Label
    Protected WithEvents srchStockTxID as TextBox
    Protected WithEvents srchDesc as TextBox
    Protected WithEvents srchUpdateBy as TextBox
    Protected WithEvents srchItemCode as TextBox

    Protected WithEvents lblStkName As Label
    Protected WithEvents lblStkID As Label
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents lstStorage As DropDownList

    Protected WithEvents Issue As ImageButton
    Protected WithEvents Staff As ImageButton
    Protected WithEvents External As ImageButton
    Protected WithEvents Nursery As ImageButton
    'Protected WithEvents ibPrint As ImageButton

    Dim objGLtrx As New agri.GL.ClsTrx()
    Protected objINtx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "IN_CLSTRX_STOCKISSUE_LIST_GET"
    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strARAccMonth As String
    Dim strARAccYear As String
    Dim intINAR As Integer
    Dim TrxType As String
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
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            Issue.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Issue).ToString())
            Staff.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Staff).ToString())
            External.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(External).ToString())
            Nursery.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Nursery).ToString())
            'ibPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibPrint).ToString())

            TrxType = Request.QueryString("type")
            If TrxType = "IN" Then
                lblStkName.Text = "STOCK ISSUE LIST"
                lblStkID.Text = "Stock Issue ID"
                dgStockTx.Columns(0).HeaderText = "Stock Issue ID"
            Else
                lblStkName.Text = "SALES LIST"
                lblStkID.Text = "Sales ID"
                dgStockTx.Columns(0).HeaderText = "Sales ID"
            End If

            If SortExpression.Text = "" Then
                SortExpression.Text = "StockIssueID"
            End If
            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                BindStorage("")
                BindSearchList()
                BindIssueTypeList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim objLangCapDs As New DataSet()
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode & "|" & objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Inventory)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../../menu/menu_INTrx_Page.aspx")
        End Try


    End Sub


    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        dgStockTx.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbl As Label
        Dim btn As LinkButton
        
        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet

        Dim btnID As LinkButton
        Dim tes As HyperLinkColumn

        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgStockTx.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsData = LoadData
            End If
        End If
        
        dgStockTx.DataSource = dsData
        dgStockTx.DataBind()
        lblPageCount.Text = PageCount
        BindPageList()
        PageNo = lblCurrentIndex.Text + 1
        lblTracker.Text="Page " & pageno & " of " & PageCount

        For intCnt = 0 To dgStockTx.Items.Count - 1
            lbl = dgStockTx.Items.Item(intCnt).FindControl("lblStatus")
            btn = dgStockTx.Items.Item(intCnt).FindControl("Delete")

            If CInt(lbl.Text) = objINtx.EnumStockIssueStatus.Active Then
                btn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            ElseIf CInt(lbl.Text) = objINtx.EnumStockIssueStatus.Deleted Then
                If lstAccMonth.SelectedItem.Value >= Session("SS_INACCMONTH") Then
                    btn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                    btn.Text = "Undelete"
                    btn.Visible = True
                Else
                    btn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                    btn.Text = "Undelete"
                    btn.Visible = False
                End If
            Else
                btn.Visible = False
            End If
        Next intCnt

    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockIssueStatus(objINtx.EnumStockIssueStatus.All), objINtx.EnumStockIssueStatus.All))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockIssueStatus(objINtx.EnumStockIssueStatus.Active), objINtx.EnumStockIssueStatus.Active))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockIssueStatus(objINtx.EnumStockIssueStatus.Deleted), objINtx.EnumStockIssueStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockIssueStatus(objINtx.EnumStockIssueStatus.Confirmed), objINtx.EnumStockIssueStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockIssueStatus(objINtx.EnumStockIssueStatus.DbNote), objINtx.EnumStockIssueStatus.DbNote))

        If intLevel = 0 Then
            srchStatusList.SelectedIndex = 1
        Else
            srchStatusList.SelectedIndex = 9
        End If
    End Sub

    Sub BindIssueTypeList()

        srchIssueList.Items.Add(New ListItem(objINtx.mtdGetStockIssueType(objINtx.EnumStockIssueType.All), objINtx.EnumStockIssueType.All))
        srchIssueList.Items.Add(New ListItem(objINtx.mtdGetStockIssueType(objINtx.EnumStockIssueType.OwnUse), objINtx.EnumStockIssueType.OwnUse))
        srchIssueList.Items.Add(New ListItem(objINtx.mtdGetStockIssueType(objINtx.EnumStockIssueType.StaffPayroll), objINtx.EnumStockIssueType.StaffPayroll))
        srchIssueList.Items.Add(New ListItem(objINtx.mtdGetStockIssueType(objINtx.EnumStockIssueType.StaffDN), objINtx.EnumStockIssueType.StaffDN))
        srchIssueList.Items.Add(New ListItem(objINtx.mtdGetStockIssueType(objINtx.EnumStockIssueType.External), objINtx.EnumStockIssueType.External))
        srchIssueList.Items.Add(New ListItem(objINtx.mtdGetStockIssueType(objINtx.EnumStockIssueType.Nursery), objINtx.EnumStockIssueType.Nursery))
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub

    Sub BindStorage(ByVal pv_strcode As String)
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim strParamName As String
        Dim strParamValue As String

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

    Protected Function LoadData() As DataSet
        Dim strParamName As string
        Dim strParamValue As String
        Dim sSQLKriteria As String
        Session("SS_PAGING") = lblCurrentIndex.Text

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = ""  ''1','2','3','4','5','6','7','8','9','10','11','12""
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        sSQLKriteria="AND iss.LocCode='" & strlocation & "' And " & _
                    "((iss.IssueType='" & srchIssueList.SelectedItem.Value & "') OR ('" & srchIssueList.SelectedItem.Value & "'='9')) AND " & _
                    "((iss.AccMonth='" & strAccMonth & "') OR ('" & strAccMonth & "'='')) AND iss.Accyear='" & lstAccYear.SelectedItem.value & "' AND " & _
                    "((iss.Status='" & srchStatusList.SelectedItem.Value & "') OR ('" & srchStatusList.SelectedItem.Value & "'='9'))  AND " & _
                    "iss.StockIssueId IN (Select ln.Stockissueid From IN_StockIssueLN ln Inner JOIN IN_StockIssue H On H.StockIssueID=Ln.StockISsueiD Inner Join IN_ITEm i on i.ItemCode=ln.ItemCode And i.LocCode=H.LocCode " & _
                                          "Where H.LocCode='" & strlocation & "' And ((ln.ItemCode LIKE '%" & srchItemCode.Text & "%') OR (i.Description LIKE '%" & srchItemCode.Text & "%' )) AND  " & _
                                          "((ln.StorageCode='" & lstStorage.SelectedItem.value & "') OR ('" & lstStorage.SelectedItem.value & "'=''))  AND iss.StockIssueID LIKE '%" & srchStockTxID.Text & "%' AND " & _
                                          "((h.AccMonth='" & strAccMonth & "') OR ('" & strAccMonth & "'='')) AND h.Accyear='" & lstAccYear.SelectedItem.value & "')"

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = sSQLKriteria & "|" & _
                        "Order BY Iss.AccMonth ASC,Iss.StockIssueID ASC "

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOppCd_GET, _
                                         strParamName, _
                                         strParamValue, _
                                         objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
        End Try



        Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument

        Select Case direction
            Case "first"
                lblCurrentIndex.Text = 0
            Case "prev"
                lblCurrentIndex.Text = _
                    Math.Max(0, lblCurrentIndex.Text - 1)
            Case "next"
                lblCurrentIndex.Text = _
                    Math.Min(lblPageCount.Text - 1, lblCurrentIndex.Text + 1)
            Case "last"
                lblCurrentIndex.Text = lblPageCount.Text - 1
        End Select

        lstDropList.SelectedIndex = lblCurrentIndex.Text
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid()
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_STOCKISSUE_LINE_GET"
        Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"
        Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
        Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_STOCKISSUE_DETAIL_ADD"
        Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_STOCKISSUE_DETAIL_UPD"
        Dim lbl As Label
        Dim StrTxParam As String
        Dim strTxID As String
        Dim strstatus As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

        lbl = E.Item.FindControl("lblTxID")
        strTxID = Trim(lbl.Text)
        lbl = E.Item.FindControl("lblStatus")
        strstatus = Trim(lbl.Text)

        If strstatus = objINtx.EnumStockIssueStatus.Deleted Then
            Try
                intErrNo = objINtx.mtdAdjustInvItemLevel(strOpCdStckTxLine_GET, _
                                                         strOpCdItem_Details_UPD, _
                                                         strOpCdItem_Details_GET, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         strTxID, _
                                                         objINtx.EnumTransactionAction.Undelete, _
                                                         ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UNDELETESTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End If
            End Try
            StrTxParam = strTxID & "||||||||||||||||" & objINtx.EnumStockIssueStatus.Active & "||||"
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
                                                         strTxID, _
                                                         objINtx.EnumTransactionAction.Delete, _
                                                         ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETENEWSTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End If
            End Try
            StrTxParam = strTxID & "||||||||||||||||" & objINtx.EnumStockIssueStatus.Deleted & "||||"
        End If

        If intErrNo = 0 And Not ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            Try
                intErrNo = objINtx.mtdUpdStockIssueDetail(strOpCdStckTxDet_ADD, _
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
                                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue), _
                                                          ErrorChk, _
                                                          TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End If
            End Try
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            lblUnDel.Visible = True
        End If
        BindGrid()
        BindPageList()

    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim Issuetype As String = CType(sender, ImageButton).ID
        Response.Redirect("../../IN/Trx/IN_Trx_StockIssue_Details.aspx?istype=" & Issuetype & "&type=" & Request.QueryString("type"))
    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        lstAccYear.DataSource = objAccYearDs.Tables(0)
        lstAccYear.DataValueField = "AccYear"
        lstAccYear.DataTextField = "UserName"
        lstAccYear.DataBind()
        lstAccYear.SelectedIndex = intSelectedIndex - 1
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
