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


Public Class WS_ItemPart : Inherits Page

    Protected WithEvents dgItem As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents sortcol As Label

    Protected WithEvents srchItemCode As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchPartNo As TextBox
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchUpdateBy As TextBox

    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents ddlStatus As DropDownList

    Protected objWS As New agri.WS.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strOppCd_GET As String = "WS_CLSSETUP_ITEMPART_LIST_GET"
    Dim strOppCd_ADD As String = "WS_CLSSETUP_ITEMPART_LIST_ADD"
    Dim strOppCd_UPD As String = "WS_CLSSETUP_ITEMPART_LIST_UPD"

    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemPart), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "ITMP.ItemCode"
                sortcol.Text = "ASC"
            End If

            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub


    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgItem.CurrentPageIndex = 0
        dgItem.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgItem.PageSize)

        dgItem.DataSource = dsData
        If dgItem.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgItem.CurrentPageIndex = 0
            Else
                dgItem.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgItem.DataBind()
        BindPageList()
        PageNo = dgItem.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgItem.PageCount
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgItem.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgItem.CurrentPageIndex

    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objWS.mtdGetItemPartStatus(objWS.EnumItemPartStatus.All), objWS.EnumItemPartStatus.All))
        srchStatusList.Items.Add(New ListItem(objWS.mtdGetItemPartStatus(objWS.EnumItemPartStatus.Active), objWS.EnumItemPartStatus.Active))
        srchStatusList.Items.Add(New ListItem(objWS.mtdGetItemPartStatus(objWS.EnumItemPartStatus.Deleted), objWS.EnumItemPartStatus.Deleted))
        srchStatusList.SelectedIndex = 1

    End Sub

    Sub BindStatusList(ByVal index As Integer)

        ddlStatus = dgItem.Items.Item(index).FindControl("ddlStatus")
        ddlStatus.Items.Add(New ListItem(objWS.mtdGetItemPartStatus(objWS.EnumItemPartStatus.Active), objWS.EnumItemPartStatus.Active))
        ddlStatus.Items.Add(New ListItem(objWS.mtdGetItemPartStatus(objWS.EnumItemPartStatus.Deleted), objWS.EnumItemPartStatus.Deleted))

    End Sub

    Sub BindItemCodeDropList(ByRef pr_lstItemCode As DropDownList, ByVal pv_ItemCode As String)

        Dim strOpCdItemCode_Get As String = "IN_CLSSETUP_INVITEM_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow

        SearchStr = " AND ITM.LocCode = '" & strLocation & "' " & _
                    " AND ITM.ItemType = '" & objWS.EnumInventoryItemType.WorkShopItem & "' " & _
                    " AND ITM.Status = '" & objWS.EnumStockItemStatus.Active & "'"

        strParam = "ORDER BY ITM.ItemCode ASC|" & SearchStr
        Try
            intErrNo = objWS.mtdGetMasterList(strOpCdItemCode_Get, strParam, objWS.EnumInventoryMasterType.ItemPart, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_ITEMPART_BINDITEMCODE&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_Setup_ItemPart.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode")) & " (" & _
                                                                       Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If Not pv_ItemCode = "" Then
                If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))) = pv_ItemCode Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert("ItemCode") = ""
        drinsert("Description") = "Please Select a Code "
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        pr_lstItemCode.DataSource = dsForDropDown.Tables(0)
        pr_lstItemCode.DataValueField = "ItemCode"
        pr_lstItemCode.DataTextField = "Description"
        pr_lstItemCode.DataBind()

        If Not pv_ItemCode = "" Then
            If SelectedIndex = -1 Then
                pr_lstItemCode.Items.Add(New ListItem(Trim(pv_ItemCode), Trim(pv_ItemCode)))
                SelectedIndex = pr_lstItemCode.Items.Count - 1
            End If
            pr_lstItemCode.SelectedIndex = SelectedIndex
        End If

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Protected Function LoadData() As DataSet

        Dim sortitem As String
        Dim strParam As String
        Dim SearchStr As String

        SearchStr = " AND ITM.LocCode = '" & strLocation & _
                    "' AND ITMP.Status like '" & _
                    IIf(Not srchStatusList.SelectedItem.Value = objWS.EnumItemPartStatus.All, srchStatusList.SelectedItem.Value, "%") & "' "

        If Not srchItemCode.Text = "" Then
            SearchStr = SearchStr & " AND ITMP.ItemCode like '" & srchItemCode.Text & "%' "
        End If

        If Not srchDesc.Text = "" Then
            SearchStr = SearchStr & " AND ITM.Description like '" & srchDesc.Text & "%' "
        End If

        If Not srchPartNo.Text = "" Then
            SearchStr = SearchStr & " AND ITMP.PartNo like '" & srchPartNo.Text & "%' "
        End If

        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND USR.Username like '" & srchUpdateBy.Text & "%' "
        End If

        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objWS.mtdGetMasterList(strOppCd_GET, strParam, objWS.EnumInventoryMasterType.ItemPart, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_ITEMPART_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/ItemPart.aspx")
        End Try

        Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgItem.CurrentPageIndex = 0
            Case "prev"
                dgItem.CurrentPageIndex = _
                    Math.Max(0, dgItem.CurrentPageIndex - 1)
            Case "next"
                dgItem.CurrentPageIndex = _
                    Math.Min(dgItem.PageCount - 1, dgItem.CurrentPageIndex + 1)
            Case "last"
                dgItem.CurrentPageIndex = dgItem.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgItem.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgItem.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgItem.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgItem.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As DropDownList
        Dim Updbutton As LinkButton
        Dim strItemCode As String
        Dim strDescription As String

        blnUpdate.Text = True
        dgItem.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= dgItem.Items.Count Then
            dgItem.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(dgItem.EditItemIndex)
        EditText = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtItemCode")
        strItemCode = Trim(EditText.Text)
        List = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlItemCode")
        List.Enabled = False
        EditText = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtDescription")
        EditText.Enabled = False
        BindItemCodeDropList(List, strItemCode)

        EditText = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtStatus")
        Select Case CInt(EditText.Text) = objWS.EnumItemPartStatus.Active
            Case True
                ddlStatus.SelectedIndex = 0
                Updbutton = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                ddlStatus.SelectedIndex = 1
                EditText = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtPartNo")
                EditText.Enabled = False
                EditText = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtDescription")
                EditText.Enabled = False
                EditText = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtUpdateDate")
                EditText.Enabled = False
                EditText = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtUserName")
                EditText.Enabled = False
                List = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlStatus")
                List.Enabled = False
                Updbutton = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = dgItem.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim label As label
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim strItemCode As String
        Dim strPartNo As String
        Dim strPartNoKey As String
        Dim strStatus As String
        Dim strCreateDate As String

        list = E.Item.FindControl("ddlItemCode")
        strItemCode = list.SelectedItem.Value
        EditText = E.Item.FindControl("txtPartNo")
        strPartNo = EditText.Text
        label = E.Item.FindControl("lblPartNo")
        strPartNoKey = label.Text
        list = E.Item.FindControl("ddlStatus")
        strStatus = list.SelectedItem.Value
        EditText = E.Item.FindControl("txtCreateDate")
        strCreateDate = EditText.Text

        strParam = strPartNo & "|" & _
                   strItemCode & "|" & _
                   strStatus & "|" & _
                   strCreateDate & "|" & _
                   strPartNoKey
        Try
            intErrNo = objWS.mtdUpdMasterList(strOppCd_ADD, _
                                              strOppCd_UPD, _
                                              strOppCd_GET, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objWS.EnumInventoryMasterType.ItemPart, _
                                              blnDupKey, _
                                              blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_ITEMPART_LIST_UPD&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/ItemPart.aspx")
        End Try

        If blnDupKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            dgItem.EditItemIndex = -1
            BindGrid()
        End If

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgItem.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim strItemCode As String
        Dim strPartNo As String
        Dim strStatus As String
        Dim strCreateDate As String
        Dim strPartNoKey As String
        Dim blnDupKey As Boolean = False
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim label As label

        list = E.Item.FindControl("ddlItemCode")
        strItemCode = list.SelectedItem.Value
        EditText = E.Item.FindControl("txtPartNo")
        strPartNo = EditText.Text
        label = E.Item.FindControl("lblPartNo")
        strPartNoKey = Label.Text
        list = E.Item.FindControl("ddlStatus")
        EditText = E.Item.FindControl("txtStatus")
        strStatus = IIf(EditText.Text = objWS.EnumItemPartStatus.Active, _
                        objWS.EnumItemPartStatus.Deleted, _
                        objWS.EnumItemPartStatus.Active)
        EditText = E.Item.FindControl("txtCreateDate")
        strCreateDate = EditText.Text

        strParam = strPartNo & "|" & _
                   strItemCode & "|" & _
                   strStatus & "|" & _
                   strCreateDate & "|" & _
                   strPartNoKey
        Try
            intErrNo = objWS.mtdUpdMasterList(strOppCd_ADD, _
                                              strOppCd_UPD, _
                                              strOppCd_GET, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objWS.EnumInventoryMasterType.ItemPart, _
                                              blnDupKey, _
                                              blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_ITEMPART_LIST_DEL&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/ItemPart.aspx")
        End Try

        dgItem.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim List As DropDownList
        Dim PageCount As Integer
        Dim TextBox As TextBox

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("ItemCode") = ""
        newRow.Item("Description") = ""
        newRow.Item("PartNo") = ""
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)

        dgItem.DataSource = dataSet
        PageCount = objGlobal.mtdGetPageCount(dataSet.Tables(0).Rows.Count, dgItem.PageSize)
        If dgItem.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgItem.CurrentPageIndex = 0
            Else
                dgItem.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgItem.DataBind()
        BindPageList()

        dgItem.CurrentPageIndex = dgItem.PageCount - 1
        lblTracker.Text = "Page " & (dgItem.CurrentPageIndex + 1) & " of " & dgItem.PageCount
        lstDropList.SelectedIndex = dgItem.CurrentPageIndex
        dgItem.DataBind()
        dgItem.EditItemIndex = dgItem.Items.Count - 1
        dgItem.DataBind()
        BindStatusList(dgItem.EditItemIndex)
        List = dgItem.Items.Item(CInt(dgItem.EditItemIndex)).FindControl("ddlItemCode")
        List.Enabled = True
        BindItemCodeDropList(List, "")

        TextBox = dgItem.Items.Item(CInt(dgItem.EditItemIndex)).FindControl("txtDescription")
        TextBox.Visible = False
        Updbutton = dgItem.Items.Item(CInt(dgItem.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

    End Sub



End Class
