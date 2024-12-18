
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


Public Class IN_StockTransferInternal : Inherits Page

    Protected WithEvents dgStockTransfer As DataGrid
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
    Public objINstp As New agri.IN.clsSetup()

    Dim strOppCd_GET As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_LIST_GET"
    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLtrx As New agri.GL.ClsTrx()

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
                SortExpression.Text = "StockTransferID"
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
        dgStockTransfer.CurrentPageIndex = 0
        dgStockTransfer.EditItemIndex = -1
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
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgStockTransfer.PageSize)

        dgStockTransfer.DataSource = dsData
        If dgStockTransfer.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgStockTransfer.CurrentPageIndex = 0
            Else
                dgStockTransfer.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgStockTransfer.DataBind()
        BindPageList()
        PageNo = dgStockTransfer.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgStockTransfer.PageCount

        For intCnt = 0 To dgStockTransfer.Items.Count - 1
            lbl = dgStockTransfer.Items.Item(intCnt).FindControl("lblStatus")
            btn = dgStockTransfer.Items.Item(intCnt).FindControl("Delete")
            If CInt(lbl.Text) = objINtx.EnumStockTransferInternalStatus.Active Then
                btn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            ElseIf CInt(lbl.Text) = objINtx.EnumStockTransferInternalStatus.Deleted Then
                btn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                btn.Text = "Undelete"
            Else
                btn.Visible = False
            End If
        Next intCnt

    End Sub
    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockTransferInternalStatus(objINtx.EnumStockTransferInternalStatus.All), objINtx.EnumStockTransferInternalStatus.All))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockTransferInternalStatus(objINtx.EnumStockTransferInternalStatus.Active), objINtx.EnumStockTransferInternalStatus.Active))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockTransferInternalStatus(objINtx.EnumStockTransferInternalStatus.Confirmed), objINtx.EnumStockTransferInternalStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockTransferInternalStatus(objINtx.EnumStockTransferInternalStatus.Cancelled), objINtx.EnumStockTransferInternalStatus.Cancelled))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockTransferInternalStatus(objINtx.EnumStockTransferInternalStatus.Deleted), objINtx.EnumStockTransferInternalStatus.Deleted))
        srchStatusList.SelectedIndex = 1
    End Sub
    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgStockTransfer.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgStockTransfer.CurrentPageIndex

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
                dgStockTransfer.CurrentPageIndex = 0
            Case "prev"
                dgStockTransfer.CurrentPageIndex = _
                    Math.Max(0, dgStockTransfer.CurrentPageIndex - 1)
            Case "next"
                dgStockTransfer.CurrentPageIndex = _
                    Math.Min(dgStockTransfer.PageCount - 1, dgStockTransfer.CurrentPageIndex + 1)
            Case "last"
                dgStockTransfer.CurrentPageIndex = dgStockTransfer.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgStockTransfer.CurrentPageIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strTxID As String
        Dim strstatus As String
        Dim lbl As Label

        lbl = E.Item.FindControl("lblTxID")
        strTxID = Trim(lbl.Text)
        lbl = E.Item.FindControl("lblStatus")
        strstatus = Trim(lbl.Text)

        Dim strOpCode As String = "IN_CLSTRX_STOCKTRANSFERINTERNAL_LIST_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|STOCKTRANSFERID|STATUS|UPDATEID"

        If strstatus = objINtx.EnumStockTransferInternalStatus.Active Then

            strParamValue = strLocation & "|" & Trim(strTxID) & _
                            "|" & objINtx.EnumStockTransferInternalStatus.Deleted & "|" & strUserId

        Else

            strParamValue = strLocation & "|" & Trim(strTxID) & _
                            "|" & objINtx.EnumStockTransferInternalStatus.Active & "|" & strUserId

            
        End If

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        BindGrid()
        BindPageList()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgStockTransfer.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgStockTransfer.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgStockTransfer.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_STOCKTRANSFERINTERNAL_Details.aspx")
    End Sub


End Class
