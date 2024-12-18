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


Public Class WS_DirectCharge : Inherits Page

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
    Protected WithEvents srchDCCode As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchUpdateBy As TextBox

    Protected WithEvents lblCode As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblDirectChgItem As Label
    Protected WithEvents lblDirectChgItemDesc As Label

    Protected objWS As New agri.WS.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim strOppCd_GET As String = "IN_CLSSETUP_INVITEM_LIST_GET"

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()

    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim strLocType as String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItem), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.text = "" Then
                SortExpression.text = "ItemCode"
            End If
            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblDirectChgItem.text = GetCaption(objLangCap.EnumLangCap.DirectChgItem)
        lblDirectChgItemDesc.text = GetCaption(objLangCap.EnumLangCap.DirectChgItemDesc)
        lblTitle.text = UCase(lblDirectChgItem.text)

        EventData.Columns(0).HeaderText = lblDirectChgItem.text & lblCode.text
        EventData.Columns(1).HeaderText = lblDirectChgItemDesc.text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_DIRECTCHARGE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=ws/setup/wr_directcharge.aspx")
        End Try

    End Sub



    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function




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

        dsData = LoadData
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
        lblTracker.Text = "Page " & pageno & " of " & EventData.PageCount
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

        SearchStr = " AND Loccode = '" & strLocation & "' AND ItemType = '" & objWS.EnumInventoryItemType.DirectCharge & "' AND itm.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objWS.EnumStockItemStatus.All, _
                       srchStatusList.SelectedItem.Value, "%") & "' "

        If Not srchDCCode.Text = "" Then
            SearchStr = SearchStr & " AND itm.ItemCode like '" & srchDCCode.Text & "%' "
        End If

        If Not srchDesc.Text = "" Then
            SearchStr = SearchStr & " AND itm.Description like '" & _
                        srchDesc.Text & "%' "
        End If

        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & _
                        srchUpdateBy.Text & "%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.Text

        strParam = sortitem & "|" & _
                    SearchStr

        Try
            intErrNo = objWS.mtdGetMasterList(strOppCd_GET, strParam, objWS.EnumInventoryMasterType.StockItem, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_DIRECTCHARGEITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/WS_directcharge.aspx")
        End Try
        Return objDataSet
    End Function

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
        SortExpression.text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("WS_DirectCharge_Detail.aspx")
    End Sub



End Class
