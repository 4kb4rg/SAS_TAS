
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

Public Class IN_RetAdv : Inherits Page

    Protected WithEvents dgStockTx As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblUnDel As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents srchStockTxID As TextBox
    Protected WithEvents srchRef As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents TxtRcvNo As TextBox

    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList


    Protected WithEvents ibNew As ImageButton
    Protected WithEvents ibPrint As ImageButton

    Protected objINtx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim strOppCd_GET As String = "IN_CLSTRX_STOCKRETADV_LIST_GET"
    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intINAR As Integer
    Dim strSelAccMonth As String
    Dim strSelAccYear As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intINAR = Session("SS_INAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReturnAdvice), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            ibNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibNew).ToString())
            'ibPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibPrint).ToString())


            If SortExpression.Text = "" Then
                SortExpression.Text = "ItemRetAdvID"
            End If
            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                BindSearchList()
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
        dgStockTx.CurrentPageIndex = 0
        dgStockTx.EditItemIndex = -1
        FindGrid()
        BindPageList()
    End Sub
    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbl As Label
        Dim btn As LinkButton

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgStockTx.PageSize)

        dgStockTx.DataSource = dsData
        If dgStockTx.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgStockTx.CurrentPageIndex = 0
            Else
                dgStockTx.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgStockTx.DataBind()
        BindPageList()
        PageNo = dgStockTx.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgStockTx.PageCount

        For intCnt = 0 To dgStockTx.Items.Count - 1
            lbl = dgStockTx.Items.Item(intCnt).FindControl("lblStatus")
            btn = dgStockTx.Items.Item(intCnt).FindControl("Delete")
            If CInt(lbl.Text) = objINtx.EnumStockTransferStatus.Active Then
                btn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            ElseIf CInt(lbl.Text) = objINtx.EnumStockTransferStatus.Deleted Then
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
    Sub FindGrid()
        Dim intCnt As Integer
        Dim lbl As Label
        Dim btn As LinkButton

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = FillData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgStockTx.PageSize)

        dgStockTx.DataSource = dsData
        If dgStockTx.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgStockTx.CurrentPageIndex = 0
            Else
                dgStockTx.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgStockTx.DataBind()
        BindPageList()
        PageNo = dgStockTx.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgStockTx.PageCount

        For intCnt = 0 To dgStockTx.Items.Count - 1
            lbl = dgStockTx.Items.Item(intCnt).FindControl("lblStatus")
            btn = dgStockTx.Items.Item(intCnt).FindControl("Delete")
            If CInt(lbl.Text) = objINtx.EnumStockTransferStatus.Active Then
                btn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            ElseIf CInt(lbl.Text) = objINtx.EnumStockTransferStatus.Deleted Then
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
        'srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockRetAdvStatus(objINtx.EnumStockRetAdvStatus.All), objINtx.EnumStockRetAdvStatus.All))
		srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockRetAdvStatus(objINtx.EnumStockRetAdvStatus.All), ""))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockRetAdvStatus(objINtx.EnumStockRetAdvStatus.Active), objINtx.EnumStockRetAdvStatus.Active))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockRetAdvStatus(objINtx.EnumStockRetAdvStatus.Confirmed), objINtx.EnumStockRetAdvStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockRetAdvStatus(objINtx.EnumStockRetAdvStatus.Deleted), objINtx.EnumStockRetAdvStatus.Deleted))
        srchStatusList.SelectedIndex = 1
    End Sub
    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgStockTx.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgStockTx.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim StrParamName As String

        strAccYear = lstAccYear.SelectedItem.Value
        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        StrParamName = "SEARCHSTR|SORTEXP"
        strParam = srchStockTxID.Text & "|" & objGlobal.EnumDocType.StockReturnAdvice & "|" & srchUpdateBy.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & SortExpression.Text & "|" & sortcol.Text _
                    & "|" & strAccMonth & "|" & strAccYear & "|" & strLocation

        Try
            intErrNo = objINtx.mtdGetStockRetAdvList(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKRETADVLIST&errmesg=" & lblErrMessage.Text & "&redirect=../EN/menu/menu_INTrx_page.aspx")
        End Try
        Return objDataSet
    End Function

    Protected Function FillData() As DataSet
        Dim strOppCd_Find As String = "IN_CLSTRX_STOCKRETADV_FIND_GET"
        Dim StrParamName As String


        strAccYear = lstAccYear.SelectedItem.Value
        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        StrParamName = "ITEMRET|USERID|DISPID|DOCTYPE|LOCCODE|ACCMONTH|ACCYEAR|STATUS"

        strParam = srchStockTxID.Text & "|" & srchUpdateBy.Text & "|" & TxtRcvNo.Text & "|" & objGlobal.EnumDocType.StockReturnAdvice & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & srchStatusList.SelectedItem.Value


        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOppCd_Find, _
                                                 StrParamName, _
                                                 strParam, _
                                                 objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKRETADVLIST&errmesg=" & lblErrMessage.Text & "&redirect=../EN/menu/menu_INTrx_page.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgStockTx.CurrentPageIndex = 0
            Case "prev"
                dgStockTx.CurrentPageIndex = _
                    Math.Max(0, dgStockTx.CurrentPageIndex - 1)
            Case "next"
                dgStockTx.CurrentPageIndex = _
                    Math.Min(dgStockTx.PageCount - 1, dgStockTx.CurrentPageIndex + 1)
            Case "last"
                dgStockTx.CurrentPageIndex = dgStockTx.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgStockTx.CurrentPageIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim StrTxParam As String

        Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_ITEMRETURNADVICE_ADD"
        Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_ITEMRETURNADVICE_SELECTIVE_UPD"
        Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_ITEMRETURNADVICE_LINE_GET"
        Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
        Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"
        Dim strOpCdStckRecvLine_Det_GET As String = "IN_CLSTRX_STOCKRECEIVELINE_DETAILS_GET"
        Dim strOpCdStckRecvLine_Det_UPD As String = "IN_CLSTRX_STOCKRECEIVE_LINE_UPD"
        Dim TxID As String
        Dim strTxID As String
        Dim strstatus As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim lbl As Label

        lbl = E.Item.FindControl("lblTxID")
        strTxID = Trim(lbl.Text)
        lbl = E.Item.FindControl("lblStatus")
        strstatus = Trim(lbl.Text)
        If strstatus = objINtx.EnumStockRetAdvStatus.Deleted Then
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
                                                            strOpCdItem_Details_GET, _
                                                            strTxID, _
                                                            objINtx.EnumTransactionAction.Undelete, _
                                                            ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UNDELETESTOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try
            StrTxParam = strTxID & "|||||||||||||" & objINtx.EnumStockRetAdvStatus.Active & "|"

        ElseIf strstatus = objINtx.EnumStockRetAdvStatus.Active Then
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
                                                            strOpCdItem_Details_GET, _
                                                            strTxID, _
                                                            objINtx.EnumTransactionAction.Delete, _
                                                            ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETENEWSTOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try
            StrTxParam = strTxID & "|||||||||||||" & objINtx.EnumStockRetAdvStatus.Deleted & "|||"
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
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRETURNADVICE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockRetAdv_List.aspx")
                End If
            End Try
        End If
        If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            lblUnDel.Visible = True
        End If
        BindGrid()
        BindPageList()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgStockTx.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgStockTx.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgStockTx.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_StockRetAdv_Details.aspx")
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.ToString() & "&redirect=")
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
