
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


Public Class WS_WorkshopItem : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents srchItemCode As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchUpdateBy As TextBox

    Protected objIN As New agri.IN.clsSetup()
    Protected objWS As New agri.WS.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strOppCd_GET As String = "IN_CLSSETUP_INVITEM_LIST_GET"
    Dim objDataSet As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intWSAR As Integer

    Dim strStockItemCodeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intWSAR = Session("SS_WSAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItem), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else

            If SortExpression.Text = "" Then
                SortExpression.Text = "ItemCode"
            End If
            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, EventData.PageSize)

        EventData.DataSource = dsData
        If EventData.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EventData.CurrentPageIndex = 0
            Else
                EventData.CurrentPageIndex = PageCount - 1
            End If
        End If

        EventData.DataBind()
        BindPageList()
        PageNo = EventData.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & EventData.PageCount
    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objWS.mtdGetStockItemStatus(objWS.EnumStockItemStatus.All), objWS.EnumStockItemStatus.All))
        srchStatusList.Items.Add(New ListItem(objWS.mtdGetStockItemStatus(objWS.EnumStockItemStatus.Active), objWS.EnumStockItemStatus.Active))
        srchStatusList.Items.Add(New ListItem(objWS.mtdGetStockItemStatus(objWS.EnumStockItemStatus.Deleted), objWS.EnumStockItemStatus.Deleted))

        srchStatusList.SelectedIndex = 1
    End Sub
    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = EventData.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet

        Dim StockCode As String
        Dim Desc As String
        Dim UpdateBy As String
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String

        SearchStr = " AND Loccode = '" & strLocation & "' AND ItemType = '" & objWS.EnumInventoryItemType.Workshopitem & "' AND itm.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objWS.EnumStockItemStatus.All, _
                       srchStatusList.SelectedItem.Value, "%") & "' "

        If Not srchItemCode.Text = "" Then
            SearchStr = SearchStr & " AND itm.ItemCode like '" & srchItemCode.Text & "%' "
        End If

        If Not srchDesc.Text = "" Then
            SearchStr = SearchStr & " AND itm.Description like '" & _
                        srchDesc.Text & "%' "
        End If

        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & _
                        srchUpdateBy.Text & "%' "
        End If

        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text

        strParam = sortitem & "|" & _
                    SearchStr

        Try
            intErrNo = objWS.mtdGetMasterList(strOppCd_GET, strParam, objWS.EnumInventoryMasterType.StockItem, objDataSet)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_WORKSHOPITEM&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_WorkShopItem_List.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPreview_Click (sender As Object, e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strStockCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String


        strStockItemCodeTag = "Workshop Item Code"
        strDescTag = "Description"
        strTitleTag = "Workshop Item"


        strStatus = IIF(Not srchStatusList.selectedItem.Value = objIN.EnumStockItemStatus.All, srchStatusList.selectedItem.Value, "")
        strStockCode = srchItemCode.text
        strDescription = srchDesc.text
        strUpdateBy =  srchUpdateBy.text
        strSortExp = sortexpression.text
        strSortCol = sortcol.text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/WS_Rpt_StockItem.aspx?strStockItemCodeTag=" & strStockItemCodeTag & _
                    "&strDescTag=" & strDescTag & "&strTitleTag=" & strTitleTag & "&strStatus=" & strStatus & _
                    "&strStockCode=" & strStockCode & "&strDescription=" & strDescription & _
                    "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
                    "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                EventData.CurrentPageIndex = 0
            Case "prev"
                EventData.CurrentPageIndex = _
                    Math.Max(0, EventData.CurrentPageIndex - 1)
            Case "next"
                EventData.CurrentPageIndex = _
                    Math.Min(EventData.PageCount - 1, EventData.CurrentPageIndex + 1)
            Case "last"
                EventData.CurrentPageIndex = EventData.PageCount - 1
        End Select
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            EventData.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        EventData.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("WS_WorkshopItem_Detail.aspx")
    End Sub


End Class
