
Imports System
Imports System.Data
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

Public Class GL_Setup_BlockGrp : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchBlkGrpCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchUpdBy As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents lblBlkGrpCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrEnter As Label
    Protected WithEvents ddlInventoryBin As DropDownList
	Protected WithEvents ddlInti As DropDownList
	

    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objINSetup As New agri.IN.clsSetup()

    Dim strOppCd_GET As String = "GL_CLSSETUP_BLOCKGROUP_LIST_GET"
    Dim strOppCd_ADD As String = "GL_CLSSETUP_BLOCKGROUP_LIST_ADD"
    Dim strOppCd_UPD As String = "GL_CLSSETUP_BLOCKGROUP_LIST_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strBlkGrpCodeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlock), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.text = "" Then
                SortExpression.text = "BlkGrpCode"
                sortcol.text = "ASC"
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

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.BlockGrp))
        lblBlkGrpCode.text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & lblCode.text
        lblDescription.text = GetCaption(objLangCap.EnumLangCap.BlockGrpDesc)
        strValidateCode = lblErrEnter.text & lblBlkGrpCode.text & "."
        strValidateDesc = lblErrEnter.text & lblDescription.text & "."

        EventData.Columns(0).HeaderText = lblBlkGrpCode.text
        EventData.Columns(1).HeaderText = lblDescription.text

        strBlkGrpCodeTag = lblBlkGrpCode.text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BlockGrp.aspx")
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





    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = EventData.PageCount + 1
            arrDList.Add("Page " & count)
            Count = Count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex

    End Sub

    Sub BindStatusList(ByVal index As Integer)

        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetBlkGrpStatus(objGLSetup.EnumBlkGrpStatus.Active), objGLSetup.EnumBlkGrpStatus.Active))
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetBlkGrpStatus(objGLSetup.EnumBlkGrpStatus.Deleted), objGLSetup.EnumBlkGrpStatus.Deleted))

    End Sub

    Sub BindSearchList()
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetBlkGrpStatus(objGLSetup.EnumBlkGrpStatus.All), objGLSetup.EnumBlkGrpStatus.All))
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetBlkGrpStatus(objGLSetup.EnumBlkGrpStatus.Active), objGLSetup.EnumBlkGrpStatus.Active))
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetBlkGrpStatus(objGLSetup.EnumBlkGrpStatus.Deleted), objGLSetup.EnumBlkGrpStatus.Deleted))
        srchStatus.SelectedIndex = 1
    End Sub

    Sub BindInventoryBinLevel(ByVal index As Integer)
        ddlInventoryBin = EventData.Items.Item(index).FindControl("ddlInventoryBin")
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.HO), objINSetup.EnumInventoryBinLevel.HO))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Central), objINSetup.EnumInventoryBinLevel.Central))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Other), objINSetup.EnumInventoryBinLevel.Other))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinI), objINSetup.EnumInventoryBinLevel.BinI))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinII), objINSetup.EnumInventoryBinLevel.BinII))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIII), objINSetup.EnumInventoryBinLevel.BinIII))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIV), objINSetup.EnumInventoryBinLevel.BinIV))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinV), objINSetup.EnumInventoryBinLevel.BinV))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinVI), objINSetup.EnumInventoryBinLevel.BinVI))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinVII), objINSetup.EnumInventoryBinLevel.BinVII))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinVIII), objINSetup.EnumInventoryBinLevel.BinVIII))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIX), objINSetup.EnumInventoryBinLevel.BinIX))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinX), objINSetup.EnumInventoryBinLevel.BinX))
    End Sub

    Protected Function LoadData() As DataSet

        Dim strParam As String
        Dim SearchStr As String
        Dim sortItem As String

        SearchStr = " and blk.LocCode = '" & strLocation & "' "
        If srchStatus.SelectedItem.Value <> objGLSetup.EnumBlkGrpStatus.All Then
            SearchStr = SearchStr & " and blk.Status like '" & srchStatus.SelectedItem.Value & "' "
        End If
        If Not srchBlkGrpCode.text = "" Then
            SearchStr = SearchStr & " and blk.BlkGrpCode like '" & srchBlkGrpCode.text & "%'"
        End If
        If Not srchDescription.text = "" Then
            SearchStr = SearchStr & " and blk.Description like '" & srchDescription.text & "%'"
        End If
        If Not srchUpdBy.text = "" Then
            SearchStr = SearchStr & " and usr.UserName like '" & srchUpdBy.text & "%'"
        End If

        sortItem = "ORDER BY " & sortexpression.text & " " & sortcol.text
        strParam = sortItem & "|" & SearchStr

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOppCd_GET, strParam, objGLSetup.EnumGLMasterType.BlkGrp, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCKGROUP_GET&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BlockGrp.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strBlkGrpCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIF(Not srchStatus.selectedItem.Value = objGLSetup.EnumBlkGrpStatus.All, srchStatus.selectedItem.Value, "")
        strBlkGrpCode = srchBlkGrpCode.text
        strDescription = srchDescription.text
        strUpdateBy = srchUpdBy.text
        strSortExp = sortexpression.text
        strSortCol = sortcol.text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_BlockGrp.aspx?strBlkGrpCodeTag=" & strBlkGrpCodeTag & _
                    "&strDescTag=" & strDescTag & "&strTitleTag=" & strTitleTag & _
                    "&strStatus=" & strStatus & _
                    "&strBlkGrpCode=" & strBlkGrpCode & "&strDescription=" & strDescription & _
                    "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
                    "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
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
        sortcol.text = IIF(sortcol.text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As Dropdownlist
		Dim lblList As Label
        Dim lbUpdbutton As linkbutton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator

        blnUpdate.text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid()
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)
        BindInventoryBinLevel(EventData.EditItemIndex)
				lblList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblInti")
				ddlList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlInti")
				ddlList.SelectedValue = lblList.Text.Trim()
				
				lblList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblBin")
				ddlList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlInventoryBin")
				ddlList.SelectedValue = lblList.Text.Trim()
				
        txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
		Select Case CInt(txtEditText.text) = objGLSetup.EnumBlkGrpStatus.Active
            Case True
                Statuslist.selectedindex = 0
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("BlkGrpCode")
                txtEditText.readonly = True
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
				
                lbUpdbutton.Text = "Delete"
                lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                Statuslist.selectedindex = 1
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("BlkGrpCode")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Description")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UpdateDate")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UserName")
                txtEditText.Enabled = False
                ddlList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("StatusList")
                ddlList.Enabled = False
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Update")
                lbUpdbutton.Visible = False
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Undelete"
        End Select
        validateCode = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateCode")
        validateDesc = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As Dropdownlist
        Dim strBlkGrpCode As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As label
        Dim strCreateDate As String
        Dim EditList As DropDownList
        Dim strBinLevel As String
		Dim strInti As String

        txtEditText = E.Item.FindControl("BlkGrpCode")
        strBlkGrpCode = txtEditText.Text
        txtEditText = E.Item.FindControl("Description")
        strDescription = txtEditText.Text
        ddlList = E.Item.FindControl("StatusList")
        strStatus = ddllist.Selecteditem.Value
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        EditList = E.Item.FindControl("ddlInventoryBin")
        strBinLevel = EditList.SelectedItem.Value
		EditList = E.Item.FindControl("ddlInti")
        strInti = EditList.SelectedItem.Value

        strParam = strBlkGrpCode & "|" & _
                    strDescription & "|" & _
                    strStatus & "|" & _
                    strBinLevel & "|" & _
					strInti 

        Try
            intErrNo = objGLSetup.mtdUpdMasterList(strOppCd_ADD, _
                                              strOppCd_UPD, _
                                              strOppCd_GET, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objGLSetup.EnumGLMasterType.BlkGrp, _
                                              blnDupKey, _
                                              blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCKGROUP_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BlockGrp.aspx")
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
        Dim txtEditText As TextBox
        Dim strBlkGrpCode As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim strCreateDate As String
        Dim EditList As DropDownList
        Dim strBinLevel As String
		Dim strInti As String

        txtEditText = E.Item.FindControl("BlkGrpCode")
        strBlkGrpCode = txtEditText.Text
        txtEditText = E.Item.FindControl("Description")
        strDescription = txtEditText.Text
        txtEditText = E.Item.FindControl("Status")
        strStatus = IIF(txtEditText.Text = objGLSetup.EnumBlkGrpStatus.Active, _
                        objGLSetup.EnumBlkGrpStatus.Deleted, _
                        objGLSetup.EnumBlkGrpStatus.Active)
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        EditList = E.Item.FindControl("Bin")
        strBinLevel = EditList.SelectedItem.Value
		EditList = E.Item.FindControl("ddlInti")
        strInti = EditList.SelectedItem.Value

        strParam = strBlkGrpCode & "|" & _
                    strDescription & "|" & _
                    strStatus & "|" & "|" & _
                    strBinLevel & "|" & "|" & _
					strInti 

        Try
            intErrNo = objGLSetup.mtdUpdMasterList(strOppCd_ADD, _
                                              strOppCd_UPD, _
                                              strOppCd_GET, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objGLSetup.EnumGLMasterType.BlkGrp, _
                                              blnDupKey, _
                                              blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCKGROUP_DELETEP&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BlockGrp.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim lbUpdbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer

        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("BlkGrpCode") = ""
        newRow.Item("Description") = ""
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.now()
        NewRow.Item("UpdateDate") = DateTime.now()
        NewRow.Item("UserName") = ""
        newRow.Item("Bin") = "0"
		newRow.Item("Inti") = "1"
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
        BindInventoryBinLevel(EventData.EditItemIndex)

        lbUpdbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        lbUpdbutton.visible = False

        validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        validateDesc = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

    End Sub


End Class
