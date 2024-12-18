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


Public Class WS_ProdCategory : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchProdCatCode As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchUpdateBy As TextBox

    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblProdCat As Label
    Protected WithEvents lblProdCatDesc As Label

    Protected objWS As New agri.WS.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim strOppCd_GET As String = "IN_CLSSETUP_PRODCAT_LIST_GET"
    Dim strOppCd_ADD As String = "IN_CLSSETUP_PRODCAT_LIST_ADD"
    Dim strOppCd_UPD As String = "IN_CLSSETUP_PRODCAT_LIST_UPD"

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
    Dim strProdCatErr As String
    Dim strDescErr As String
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductCategory), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "ProdCatCode"
                sortcol.Text = "ASC"
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
        lblProdCat.Text = GetCaption(objLangCap.EnumLangCap.ProdCat)
        lblProdCatDesc.Text = GetCaption(objLangCap.EnumLangCap.ProdCatDesc)
        lblTitle.Text = UCase(lblProdCat.Text)

        EventData.Columns(0).HeaderText = lblProdCat.Text & lblCode.Text
        EventData.Columns(1).HeaderText = lblProdCatDesc.Text

        strProdCatErr = lblPleaseEnter.Text & lblProdCat.Text & lblCode.Text
        strDescErr = lblPleaseEnter.Text & lblProdCatDesc.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_PRODCATEGORY_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=ws/setup/ws_prodcategory.aspx")
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

    Sub BindStatusList(ByVal index As Integer)

        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objWS.mtdGetProductCategoryStatus(objWS.EnumProductCategoryStatus.Active), objWS.EnumProductCategoryStatus.Active))
        StatusList.Items.Add(New ListItem(objWS.mtdGetProductCategoryStatus(objWS.EnumProductCategoryStatus.Deleted), objWS.EnumProductCategoryStatus.Deleted))

    End Sub
    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objWS.mtdGetProductCategoryStatus(objWS.EnumProductCategoryStatus.All), objWS.EnumProductCategoryStatus.All))
        srchStatusList.Items.Add(New ListItem(objWS.mtdGetProductCategoryStatus(objWS.EnumProductCategoryStatus.Active), objWS.EnumProductCategoryStatus.Active))
        srchStatusList.Items.Add(New ListItem(objWS.mtdGetProductCategoryStatus(objWS.EnumProductCategoryStatus.Deleted), objWS.EnumProductCategoryStatus.Deleted))

        srchStatusList.SelectedIndex = 1
    End Sub
    Protected Function LoadData() As DataSet

        Dim ProdCatCode As String
        Dim Desc As String
        Dim UpdateBy As String
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String


        SearchStr = " AND PCat.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objWS.EnumProductCategoryStatus.All, _
                       srchStatusList.SelectedItem.Value, "%") & "' "
        If Not srchProdCatCode.Text = "" Then
            SearchStr = SearchStr & " AND PCat.ProdCatCode like '" & srchProdCatCode.Text & "%' "
        End If

        If Not srchDesc.Text = "" Then
            SearchStr = SearchStr & " AND PCat.Description like '" & _
                        srchDesc.Text & "%' "
        End If

        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & _
                        srchUpdateBy.Text & "%' "
        End If

        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objWS.mtdGetMasterList(strOppCd_GET, strParam, objWS.EnumInventoryMasterType.ProductCategory, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_PRODCATEGORY&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_prodcategory.aspx")
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
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As DropDownList
        Dim Updbutton As LinkButton
        Dim rfv As RequiredFieldValidator

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(EditText.Text) = objWS.EnumProductCategoryStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ProdCatCode")
                EditText.ReadOnly = True
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ProdCatCode")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Description")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UpdateDate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UserName")
                EditText.Enabled = False
                List = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("StatusList")
                List.Enabled = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select
        rfv = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validateCode")
        rfv.ErrorMessage = strProdCatErr
        rfv = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validateDesc")
        rfv.ErrorMessage = strDescErr

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim ProdCatCode As String
        Dim lblMsg As Label
        Dim blnDupKey As Boolean = False
        Dim Description As String
        Dim Status As String
        Dim CreateDate As String
        Dim strCompany As String = Session("SS_COMPANY")
        Dim strLocation As String = Session("SS_LOCATION")
        Dim strUserId As String = Session("SS_USERID")

        EditText = E.Item.FindControl("ProdCatCode")
        ProdCatCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.SelectedItem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam = ProdCatCode & "|" & _
                    Description & "|" & _
                    Status & "|" & _
                    CreateDate
        Try
            intErrNo = objWS.mtdUpdMasterList(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objWS.EnumInventoryMasterType.ProductCategory, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_PRODCATEGORY&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_prodcategory.aspx")
        End Try

        If blnDupKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid()
        End If

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim EditText As TextBox
        Dim ProdCatCode As String
        Dim blnDupKey As Boolean = False
        Dim Description As String
        Dim Status As String
        Dim CreateDate As String
        Dim strCompany As String = Session("SS_COMPANY")
        Dim strLocation As String = Session("SS_LOCATION")
        Dim strUserId As String = Session("SS_USERID")

        EditText = E.Item.FindControl("ProdCatCode")
        ProdCatCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        EditText = E.Item.FindControl("Status")
        Status = IIf(EditText.Text = objWS.EnumProductCategoryStatus.Active, _
                        objWS.EnumProductCategoryStatus.Deleted, _
                        objWS.EnumProductCategoryStatus.Active)
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam = ProdCatCode & "|" & _
                    Description & "|" & _
                    Status & "|" & _
                    CreateDate
        Try
            intErrNo = objWS.mtdUpdMasterList(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objWS.EnumInventoryMasterType.ProductCategory, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_PRODCATEGORY&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_prodcategory.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim rfv As RequiredFieldValidator
        Dim PageCount As Integer

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("ProdCatCode") = ""
        newRow.Item("Description") = ""
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)

        EventData.DataSource = dataSet
        PageCount = objGlobal.mtdGetPageCount(dataSet.Tables(0).Rows.Count, EventData.PageSize)
        If EventData.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EventData.CurrentPageIndex = 0
            Else
                EventData.CurrentPageIndex = PageCount - 1
            End If
        End If
        EventData.DataBind()
        BindPageList()

        EventData.CurrentPageIndex = EventData.PageCount - 1
        lblTracker.Text = "Page " & (EventData.CurrentPageIndex + 1) & " of " & EventData.PageCount
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count - 1
        EventData.DataBind()
        BindStatusList(EventData.EditItemIndex)

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

        rfv = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        rfv.ErrorMessage = strProdCatErr

        rfv = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")
        rfv.ErrorMessage = strDescErr

    End Sub



End Class
