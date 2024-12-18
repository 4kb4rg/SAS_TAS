
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


Public Class IN_Movement : Inherits Page

    Protected WithEvents dgStockMovement As DataGrid
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
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchUpdateBy As TextBox

    Protected objINtx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "IN_CLSTRX_STOCKMOVEMENT_LIST_GET"
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

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockTransfer), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "StockMovementID"
            End If
            If Not Page.IsPostBack Then
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
        dgStockMovement.CurrentPageIndex = 0
        dgStockMovement.EditItemIndex = -1
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

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgStockMovement.PageSize)

        dgStockMovement.DataSource = dsData
        If dgStockMovement.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgStockMovement.CurrentPageIndex = 0
            Else
                dgStockMovement.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgStockMovement.DataBind()
        BindPageList()
        PageNo = dgStockMovement.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgStockMovement.PageCount

        For intCnt = 0 To dgStockMovement.Items.Count - 1
            lbl = dgStockMovement.Items.Item(intCnt).FindControl("lblStatus")
            btn = dgStockMovement.Items.Item(intCnt).FindControl("Delete")
            If CInt(lbl.Text) = objINtx.EnumStockMovementStatus.Active Then
                btn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            ElseIf CInt(lbl.Text) = objINtx.EnumStockMovementStatus.Deleted Then
                btn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                btn.Text = "Undelete"
            Else
                btn.Visible = False
            End If
        Next intCnt

    End Sub
    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.All), objINtx.EnumStockTransferStatus.All))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.Active), objINtx.EnumStockTransferStatus.Active))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.Confirmed), objINtx.EnumStockTransferStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.DbNote), objINtx.EnumStockTransferStatus.DbNote))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockTransferStatus(objINtx.EnumStockTransferStatus.Deleted), objINtx.EnumStockTransferStatus.Deleted))
        srchStatusList.SelectedIndex = 1
    End Sub
    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgStockMovement.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgStockMovement.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet



        strParam = srchStockTxID.Text & "|" & srchDesc.Text & "|" & srchUpdateBy.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & SortExpression.Text & "|" & sortcol.Text _
                    & "|" & strAccMonth & "|" & strAccYear & "|" & strLocation

        Try
            intErrNo = objINtx.mtdGetStockTransferList(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFERLIST&errmesg=" & lblErrMessage.Text & "&redirect=../EN/menu/menu_INTrx_page.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgStockMovement.CurrentPageIndex = 0
            Case "prev"
                dgStockMovement.CurrentPageIndex = _
                    Math.Max(0, dgStockMovement.CurrentPageIndex - 1)
            Case "next"
                dgStockMovement.CurrentPageIndex = _
                    Math.Min(dgStockMovement.PageCount - 1, dgStockMovement.CurrentPageIndex + 1)
            Case "last"
                dgStockMovement.CurrentPageIndex = dgStockMovement.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgStockMovement.CurrentPageIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim StrTxParam As String
        Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
        Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_STOCKMOVEMENT_LINE_GET"
        Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"
        Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_STOCKMOVEMENT_DETAIL_ADD"
        Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_STOCKMOVEMENT_DETAIL_UPD"

        Dim TxID As String
        Dim strTxID As String
        Dim strstatus As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim lbl As Label

        lbl = E.Item.FindControl("lblTxID")
        strTxID = Trim(lbl.Text)
        lbl = E.Item.FindControl("lblStatus")
        strstatus = Trim(lbl.Text)

        If strstatus = objINtx.EnumStockTransferStatus.Deleted Then
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
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UNDELETESTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End If
            End Try
            StrTxParam = strTxID & "|||||||||||||||" & objINtx.EnumStockTransferStatus.Active & "|"
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
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETENEWSTOCKTRANSFER&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_STOCKMOVEMENT_List.aspx")
                End If
            End Try
            StrTxParam = strTxID & "|||||||||||||||" & objINtx.EnumStockTransferStatus.Deleted & "|"
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
        BindGrid()
        BindPageList()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgStockMovement.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgStockMovement.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgStockMovement.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_STOCKMOVEMENT_Details.aspx")
    End Sub

   
End Class
