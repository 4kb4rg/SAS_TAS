Imports System
Imports System.Data
Imports System.Text
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class FA_setup_AssetClass : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents lblOper As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents srchAssetClassCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblAssetClassCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblList As Label

    Protected objFASetup As New agri.FA.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOppCd_GET As String = "FA_CLSSETUP_ASSETCLASS_LIST_GET"
    Dim strOppCd_ADD As String = "FA_CLSSETUP_ASSETCLASS_LIST_ADD"
    Dim strOppCd_UPD As String = "FA_CLSSETUP_ASSETCLASS_LIST_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intFAAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim DocTitleTag As String
    Dim AssetClassCodeTag As String
    Dim DescriptionTag As String

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim intConfigSetting As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intFAAR = Session("SS_FAAR")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAClassSetup), intFAAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "AssetClassCode"
                SortCol.Text = "ASC"
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
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.AssetClass))
        lblAssetClassCode.Text = GetCaption(objLangCap.EnumLangCap.AssetClass) & lblCode.Text
        lblDescription.Text = GetCaption(objLangCap.EnumLangCap.AssetClassDesc)

        DocTitleTag = lblTitle.text & lblList.text
        AssetClassCodeTag = GetCaption(objLangCap.EnumLangCap.AssetClass) & lblCode.Text
        DescriptionTag = GetCaption(objLangCap.EnumLangCap.AssetClassDesc)

        strValidateCode = lblPleaseEnter.Text & lblAssetClassCode.Text & "."
        strValidateDesc = lblPleaseEnter.Text & lblDescription.Text & "."

        EventData.Columns(0).HeaderText = lblAssetClassCode.Text
        EventData.Columns(1).HeaderText = lblDescription.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_AssetClass_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=FA/Setup/FA_setup_AssetClass.aspx")
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
        lblTracker.Text="Page " & pageno & " of " & EventData.PageCount
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
        StatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetClassStatus(objFASetup.EnumAssetClassStatus.Active), objFASetup.EnumAssetClassStatus.Active))
        StatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetClassStatus(objFASetup.EnumAssetClassStatus.Deleted), objFASetup.EnumAssetClassStatus.Deleted))
    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetClassStatus(objFASetup.EnumAssetClassStatus.Active), objFASetup.EnumAssetClassStatus.Active))
        srchStatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetClassStatus(objFASetup.EnumAssetClassStatus.Deleted), objFASetup.EnumAssetClassStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetClassStatus(objFASetup.EnumAssetClassStatus.All), objFASetup.EnumAssetClassStatus.All))
    End Sub

    Protected Function LoadData() As DataSet
        Dim Period As String

        strParam = srchAssetClassCode.Text & "|" & _
                    srchDescription.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & _
                    srchUpdateBy.Text & "|" & _
                    SortExpression.Text & " " & SortCol.Text

        Try
            intErrNo = objFASetup.mtdGetAssetClass(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_AssetClass_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/Setup/FA_setup_AssetClass.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strAssetClassCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = srchStatusList.SelectedItem.Value
        strAssetClassCode = srchAssetClassCode.Text
        strDescription = srchDescription.Text
        strUpdateBy = srchUpdateBy.Text
        strSortExp = SortExpression.Text
        strSortCol = SortCol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/FA_Rpt_AssetClass.aspx?strStatus=" & strStatus & _
                       "&strAssetClassCode=" & strAssetClassCode & _
                       "&strDescription=" & strDescription & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&DocTitleTag=" & lblTitle.Text & _
                       "&AssetClassCodeTag=" & AssetClassCodeTag & _
                       "&DescriptionTag=" & DescriptionTag & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
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
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub


    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim LabelText As Label
        Dim EditList As DropDownList
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim intSelectedAcc As Integer

        lblOper.Text = objFASetup.EnumOperation.Update
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(EditText.Text) = objFASetup.EnumAssetClassStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("AssetClassCode")
                EditText.ReadOnly = True
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("AssetClassCode")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Description")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UpdateDate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UserName")
                EditText.Enabled = False
                EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("StatusList")
                EditList.Enabled = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select
        validateCode = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateCode")
        validateDesc = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim EditList As DropDownList
        Dim strAssetClassCode As String
        Dim strDescription As String
        Dim strStatus As String
        Dim strCreateDate As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim intError As Integer

        EditText = E.Item.FindControl("AssetClassCode")
        strAssetClassCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        strDescription = EditText.Text
        EditList = E.Item.FindControl("StatusList")
        strStatus = EditList.SelectedItem.Value
        EditText = E.Item.FindControl("CreateDate")
        strCreateDate = EditText.Text

        strParam = strAssetClassCode & "|" & _
                    strDescription & "|" & _
                    strStatus

        Try
            intErrNo = objFASetup.mtdUpdAssetClass(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    lblOper.Text, _
                                                    intError)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_AssetClass&errmesg=" & lblErrMessage.Text & "&redirect=FA/Setup/FA_setup_AssetClass.aspx")
        End Try

        If intError = objFASetup.EnumErrorType.DuplicateKey Then
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
        Dim EditList As DropDownList
        Dim strAssetClassCode As String
        Dim strDescription As String
        Dim strStatus As String
        Dim strCreateDate As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim intError As Integer

        EditText = E.Item.FindControl("AssetClassCode")
        strAssetClassCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        strDescription = EditText.Text
        EditText = E.Item.FindControl("Status")
        strStatus = IIf(EditText.Text = objFASetup.EnumAssetClassStatus.Active, _
                        objFASetup.EnumAssetClassStatus.Deleted, _
                        objFASetup.EnumAssetClassStatus.Active)
        EditText = E.Item.FindControl("CreateDate")
        strCreateDate = EditText.Text

        strParam = strAssetClassCode & "|" & _
                    strDescription & "|" & _
                    strStatus

        Try
            intErrNo = objFASetup.mtdUpdAssetClass(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    lblOper.Text, _
                                                    intError)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETE_ASSETCLASS&errmesg=" & lblErrMessage.Text & "&redirect=FA/Setup/FA_setup_AssetClass.aspx")
        End Try

        If intError = objFASetup.EnumErrorType.DuplicateKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid()
        End If
    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer
        
        lblOper.Text = objFASetup.EnumOperation.Add
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("AssetClassCode") = ""
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
        lblTracker.Text="Page " & (EventData.CurrentPageIndex + 1) & " of " & EventData.PageCount
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count - 1
        EventData.DataBind()
        BindStatusList(EventData.EditItemIndex)

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

        validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        validateDesc = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

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
