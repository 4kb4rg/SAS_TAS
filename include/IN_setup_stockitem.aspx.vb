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
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.IN
Imports agri.PWSystem.clsLangCap

Public Class IN_StockItem : Inherits Page

    Protected WithEvents EventData as DataGrid
    Protected WithEvents ErrorMessage as Label
    Protected WithEvents lblTracker as Label
    Protected WithEvents lblErrMessage as Label
    Protected WithEvents SQLStatement as Label
    Protected WithEvents lstDropList as DropDownList
    Protected WithEvents StatusList as DropDownList
    Protected WithEvents srchStatusList as DropDownList
    Protected WithEvents SortExpression as Label
    Protected WithEvents sortcol as Label
    Protected WithEvents blnUpdate as Label
    Protected WithEvents curStatus as Label
    Protected WithEvents srchStockCode as TextBox
    Protected WithEvents srchDesc as TextBox
    Protected WithEvents srchUpdateBy as TextBox
    Protected WithEvents lblTitle as Label
    Protected WithEvents lblStockItemCode as Label
    Protected WithEvents lblDescription as Label
    Protected WithEvents lblCode as Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label

    Protected objIN As New agri.IN.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "IN_CLSSETUP_INVITEM_LIST_GET"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strStockItemCodeTag AS String
    Dim strDescTag As String
    Dim strTitleTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String
    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItem), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap
            If SortExpression.text = "" Then
                SortExpression.text = "right(rtrim(ItemCode),5)"
            End If
            If Not Page.IsPostBack Then
                BindSearchList() 
                BindGrid() 
                BindPageList()
            End If
        End IF
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.StockItem))
        lblStockItemCode.text = GetCaption(objLangCap.EnumLangCap.StockItem) & lblCode.text
        lblDescription.text = GetCaption(objLangCap.EnumLangCap.StockItemDesc)

        EventData.Columns(0).HeaderText = lblStockItemCode.text
        EventData.Columns(1).HeaderText = lblDescription.text

        strStockItemCodeTag = lblStockItemCode.text
        strDescTag = lblDescription.text
        strTitleTag = lblTitle.text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=IN/setup/IN_setup_stockitem.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function


    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
        lblCurrentIndex.Text = 0
        EventData.EditItemIndex = -1
        BindGrid() 
        BindPageList()
    End Sub 

    Sub BindGrid() 


        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), EventData.PageSize)
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
        
        EventData.DataSource = dsData
        EventData.DataBind()
        lblPageCount.Text = PageCount
        BindPageList()
        PageNo = lblCurrentIndex.Text + 1
        lblTracker.Text="Page " & pageno & " of " & PageCount
    End Sub 

    Sub BindSearchList() 

        srchStatusList.Items.Add(New ListItem(objIN.mtdGetStockItemStatus(objIN.EnumStockItemStatus.Active), objIN.EnumStockItemStatus.Active))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetStockItemStatus(objIN.EnumStockItemStatus.Deleted), objIN.EnumStockItemStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetStockItemStatus(objIN.EnumStockItemStatus.All), objIN.EnumStockItemStatus.All))

    End Sub 

    Sub BindPageList() 

        Dim count as integer = 1   
        Dim arrDList As New ArrayList()

        While not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count)
            Count = Count +1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub 

    Protected Function LoadData() As DataSet
        
        Dim strParam as string
        Dim SearchStr as string
        Dim sortitem as string

        Session("SS_PAGING") = lblCurrentIndex.Text

        SearchStr = "AND Loccode = '" & strLocation & "' AND ItemType in ('" & objIN.EnumInventoryItemType.Stock & "','" & objIN.EnumInventoryItemType.WorkshopItem & "','7') AND itm.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objIN.EnumStockItemStatus.All, _
                       srchStatusList.SelectedItem.Value, "%") & "' "
        
        If NOT srchStockCode.text = "" Then
            SearchStr = SearchStr & " AND itm.ItemCode like '" & srchStockCode.text &"%' "
        End If

        If NOT srchDesc.text = "" Then
            SearchStr = SearchStr & " AND itm.Description like '" & _
                        srchDesc.text &"%' "
        End If

        If NOT srchUpdateBy.text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & _
                        srchUpdateBy.text &"%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text 

        strParam =  sortitem & "|" & _
                    SearchStr

        Try
        intErrNo = objIN.mtdGetMasterList(strOppCd_GET, strParam, objIN.EnumInventoryMasterType.StockItem, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_stockitem.aspx")
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

        strStatus = IIF(Not srchStatusList.selectedItem.Value = objIN.EnumStockItemStatus.All, srchStatusList.selectedItem.Value, "")
        strStockCode = srchStockCode.text
        strDescription = srchDesc.text
        strUpdateBy =  srchUpdateBy.text
        strSortExp = sortexpression.text
        strSortCol = sortcol.text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockItem.aspx?strStockItemCodeTag=" & strStockItemCodeTag & _
                    "&strDescTag=" & strDescTag & "&strTitleTag=" & strTitleTag & "&strStatus=" & strStatus & _
                    "&strStockCode=" & strStockCode & "&strDescription=" & strDescription & _
                    "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
                    "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
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

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text=e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.text = e.SortExpression.ToString()
        sortcol.text = IIF(sortcol.text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid() 
    End Sub

    Sub btnNewItm_Click(sender As Object, e As ImageClickEventArgs)
        Response.Redirect("IN_StockItem_Detail.aspx")
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
