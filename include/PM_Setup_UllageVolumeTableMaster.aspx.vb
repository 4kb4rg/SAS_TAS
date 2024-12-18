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

Public Class PM_Setup_UllageVolumeTableMaster : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchTableCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblTableCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblList As Label

    Protected objPM As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strOppCd_GET As String = "PM_CLSSETUP_UVTABLE_GET"
    Dim strOppCd_ADD As String = "PM_CLSSETUP_UVTABLE_ADD"
    Dim strOppCd_UPD As String = "PM_CLSSETUP_UVTABLE_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim DocTitleTag As String
    Dim UVTableCodeTag As String
    Dim UVTableDescTag As String
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPMAR = Session("SS_PMAR")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
	    
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMasterSetup), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "UVTableCode"
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
        lblTitle.Text = "ULLAGE - VOLUME TABLE MASTER" 
        lblTableCode.Text = "Table Code" 
        lblDescription.Text = "Description" 

        strValidateCode = lblPleaseEnter.Text & lblTableCode.Text & "."
        strValidateDesc = lblPleaseEnter.Text & lblDescription.Text & "."

        EventData.Columns(0).HeaderText = lblTableCode.Text
        EventData.Columns(1).HeaderText = lblDescription.Text

        DocTitleTag = lblTitle.Text & lblList.Text
        UVTableCodeTag = lblTableCode.Text
        UVTableDescTag = lblDescription.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_SETUP_ULLAGEVOLUMETABLE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_UllageVolumeTableMaster.aspx")
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
        StatusList.Items.Add(New ListItem(objPM.mtdGetUllageVolumeTableStatus(objPM.EnumUllageVolumeTableStatus.Active), objPM.EnumUllageVolumeTableStatus.Active))
        StatusList.Items.Add(New ListItem(objPM.mtdGetUllageVolumeTableStatus(objPM.EnumUllageVolumeTableStatus.Deleted), objPM.EnumUllageVolumeTableStatus.Deleted))

    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objPM.mtdGetUllageVolumeTableStatus(objPM.EnumUllageVolumeTableStatus.All), objPM.EnumUllageVolumeTableStatus.All))
        srchStatusList.Items.Add(New ListItem(objPM.mtdGetUllageVolumeTableStatus(objPM.EnumUllageVolumeTableStatus.Active), objPM.EnumUllageVolumeTableStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPM.mtdGetUllageVolumeTableStatus(objPM.EnumUllageVolumeTableStatus.Deleted), objPM.EnumUllageVolumeTableStatus.Deleted))

        srchStatusList.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet

        Dim TableCode As String
        Dim Desc As String
        Dim UpdateBy As String
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String
        Dim intCnt As Integer


        SearchStr = " AND UVT.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objPM.EnumUllageVolumeTableStatus.All, _
                       srchStatusList.SelectedItem.Value, "%") & "' "

        If Not srchTableCode.Text = "" Then
            SearchStr = SearchStr & " AND UVT.UVTableCode like '" & srchTableCode.Text & "%' "
        End If
        If Not srchDescription.Text = "" Then
            SearchStr = SearchStr & " AND UVT.Description like '" & srchDescription.Text & "%' "
        End If
        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & srchUpdateBy.Text & "%' "
        End If

        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objPM.mtdGetUllageVolumeTable(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ULLAGEVOLUMETABLE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_UllageVolumeTableMaster.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                objDataSet.Tables(0).Rows(intCnt).Item("UVTableCode") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UVTableCode"))
                objDataSet.Tables(0).Rows(intCnt).Item("Description") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Description"))
                objDataSet.Tables(0).Rows(intCnt).Item("Status") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Status"))
                objDataSet.Tables(0).Rows(intCnt).Item("CreateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("CreateDate"))
                objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate"))
                objDataSet.Tables(0).Rows(intCnt).Item("UserName") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objDataSet
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strTableCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIf(Not srchStatusList.SelectedItem.Value = objPM.EnumUllageVolumeTableStatus.All, srchStatusList.SelectedItem.Value, "")
        strTableCode = srchTableCode.Text
        strDescription = srchDescription.Text
        strUpdateBy = srchUpdateBy.Text
        strSortExp = SortExpression.Text
        strSortCol = sortcol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PM_Rpt_UllageVolumeTableMasterList.aspx?strStatus=" & strStatus & _
                       "&strTableCode=" & strTableCode & _
                       "&strDescription=" & strDescription & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&TitleTag=" & DocTitleTag & _
                       "&TableCodeTag=" & UVTableCodeTag & _
                       "&DescriptionTag=" & UVTableDescTag & _
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

        EventData.EditItemIndex = -1
        BindGrid()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            EventData.CurrentPageIndex = lstDropList.SelectedIndex
            EventData.EditItemIndex = -1
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        EventData.CurrentPageIndex = e.NewPageIndex
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As DropDownList
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtStatus")
        Select Case CInt(EditText.Text) = objPM.EnumUllageVolumeTableStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtTableCode")
                EditText.ReadOnly = True
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtTableCode")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtDescription")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtUpdateDate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtUserName")
                EditText.Enabled = False
                List = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("StatusList")
                List.Enabled = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select
        validateCode = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateTableCode")
        validateDesc = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateDescription")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim TableCode As String
        Dim Description As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim CreateDate As String

        EditText = E.Item.FindControl("txtTableCode")
        TableCode = EditText.Text
        EditText = E.Item.FindControl("txtDescription")
        Description = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.SelectedItem.Value
        EditText = E.Item.FindControl("txtCreateDate")
        CreateDate = EditText.Text
        strParam = TableCode & "|" & _
                    Description & "|" & _
                    Status & "|" & _
                    CreateDate

        Try
            intErrNo = objPM.mtdUpdUllageVolumeTable(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_ULLAGEVOLUMETABLE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_UllageVolumeTableMaster.aspx")
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
        Dim TableCode As String
        Dim Description As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim CreateDate As String


        EditText = E.Item.FindControl("txtTableCode")
        TableCode = EditText.Text
        EditText = E.Item.FindControl("txtDescription")
        Description = EditText.Text
        EditText = E.Item.FindControl("txtStatus")
        Status = IIf(EditText.Text = objPM.EnumUllageVolumeTableStatus.Active, _
                        objPM.EnumUllageVolumeTableStatus.Deleted, _
                        objPM.EnumUllageVolumeTableStatus.Active)
        EditText = E.Item.FindControl("txtCreateDate")
        CreateDate = EditText.Text
        strParam = TableCode & "|" & _
                    Description & "|" & _
                    Status & "|" & _
                    CreateDate
        Try
            intErrNo = objPM.mtdUpdUllageVolumeTable(strOppCd_ADD, _
                                            strOppCd_UPD, _
                                            strOppCd_GET, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            blnDupKey, _
                                            blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_ULLAGEVOLUMETABLE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_UllageVolumeTableMaster.aspx")
        End Try

        EventData.EditItemIndex = -1

        BindGrid()
    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("UVTableCode") = ""
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

        validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateTableCode")
        validateDesc = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDescription")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

    End Sub

End Class
