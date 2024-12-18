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

Public Class PM_Setup_CPOPropertiesMaster : Inherits Page

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
    Protected WithEvents srchUVTableCode As TextBox
    Protected WithEvents srchTemperature As TextBox
    Protected WithEvents srchDensity As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblUVTableCode As Label
    Protected WithEvents lblTemperature As Label
    Protected WithEvents lblDensity As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblList As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label

    Protected objPM As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strOppCd_GET As String = "PM_CLSSETUP_CPOPROPERTIES_GET"
    Dim strOppCd_ADD As String = "PM_CLSSETUP_CPOPROPERTIES_ADD"
    Dim strOppCd_UPD As String = "PM_CLSSETUP_CPOPROPERTIES_UPD"
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
    Dim strValidateTableCode As String
    Dim strValidateTemperature As String
    Dim strValidateDensity As String
    Dim TitleTag As String
    Dim TableCodeTag As String
    Dim TemperatureTag As String
    Dim DensityTag As String
	
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOPropertyMaster), intPMAR) = False Then
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
        lblTitle.Text = "CPO PROPERTIES MASTER" 
        lblUVTableCode.Text = "Table Code" 
        lblTemperature.Text = "Temperature (&#176;C)" 
        lblDensity.Text = "Density (Kg/L)" 

        strValidateTableCode = lblPleaseEnter.Text & lblUVTableCode.Text & "."
        strValidateTemperature = lblPleaseEnter.Text & lblTemperature.Text & "."
        strValidateDensity = lblPleaseEnter.Text & lblDensity.Text & "."

        EventData.Columns(0).HeaderText = lblUVTableCode.Text
        EventData.Columns(1).HeaderText = lblTemperature.Text
        EventData.Columns(2).HeaderText = lblDensity.Text

        TitleTag = lblTitle.Text & lblList.Text
        TableCodeTag = lblUVTableCode.Text
        TemperatureTag = lblTemperature.Text
        DensityTag = lblDensity.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_SETUP_CPOPROPERTIES_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_CPOPropertiesMaster.aspx")
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

    Sub BindTableCodeDropList(ByRef lstTableCode As DropDownList, ByVal TableCode As String)

        Dim strOpCdTableCode_Get As String = "PM_CLSSETUP_UVTABLE_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow

        SearchStr = " AND UVT.Status = '" & objPM.EnumUllageVolumeTableStatus.Active & "'"

        strParam = "ORDER BY UVT.UVTableCode asc|" & SearchStr

        Try
            intErrNo = objPM.mtdGetUllageVolumeTable(strOpCdTableCode_Get, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ULLAGEVOLUMECONVERSION&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_UllageVolumeConversionMaster.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            If Not TableCode = "" Then
                If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))) = TableCode Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Please Select a Code "
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstTableCode.DataSource = dsForDropDown.Tables(0)
        lstTableCode.DataValueField = "UVTableCode"
        lstTableCode.DataTextField = "Description"
        lstTableCode.DataBind()

        If Not TableCode = "" Then
            If SelectedIndex = -1 Then
                lstTableCode.Items.Add(New ListItem(Trim(TableCode), Trim(TableCode)))
                SelectedIndex = lstTableCode.Items.Count - 1
            End If
            lstTableCode.SelectedIndex = SelectedIndex
        End If

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        EventData.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), EventData.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsData = LoadData()
            End If
        End If

        EventData.DataSource = dsData
        EventData.DataBind()
        lblPageCount.Text = PageCount
        BindPageList()
        PageNo = lblCurrentIndex.Text + 1
        lblTracker.Text = "Page " & PageNo & " of " & PageCount
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text

    End Sub

    Sub BindStatusList(ByVal index As Integer)

        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objPM.mtdGetCPOPropertiesStatus(objPM.EnumCPOPropertiesStatus.Active), objPM.EnumCPOPropertiesStatus.Active))
        StatusList.Items.Add(New ListItem(objPM.mtdGetCPOPropertiesStatus(objPM.EnumCPOPropertiesStatus.Deleted), objPM.EnumCPOPropertiesStatus.Deleted))

    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objPM.mtdGetCPOPropertiesStatus(objPM.EnumCPOPropertiesStatus.All), objPM.EnumCPOPropertiesStatus.All))
        srchStatusList.Items.Add(New ListItem(objPM.mtdGetCPOPropertiesStatus(objPM.EnumCPOPropertiesStatus.Active), objPM.EnumCPOPropertiesStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPM.mtdGetCPOPropertiesStatus(objPM.EnumCPOPropertiesStatus.Deleted), objPM.EnumCPOPropertiesStatus.Deleted))
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

        lblCurrentIndex.Text = IIf(lblCurrentIndex.Text < 0, 0, lblCurrentIndex.Text)
        Session("SS_PAGING") = lblCurrentIndex.Text

        SearchStr = " AND CPOP.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objPM.EnumCPOPropertiesStatus.All, _
                       srchStatusList.SelectedItem.Value, "%") & "' "

        If Not srchUVTableCode.Text = "" Then
            SearchStr = SearchStr & " AND CPOP.UVTableCode like '" & srchUVTableCode.Text & "%' "
        End If
        If Not srchTemperature.Text = "" Then
            SearchStr = SearchStr & " AND CPOP.Temperature like '" & srchTemperature.Text & "%' "
        End If
        If Not srchDensity.Text = "" Then
            SearchStr = SearchStr & " AND CPOP.Density like '" & srchDensity.Text & "%' "
        End If
        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & srchUpdateBy.Text & "%' "
        End If

        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objPM.mtdGetCPOProperties(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_CPOPROPERTIES&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_CPOPropertiesMaster.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                objDataSet.Tables(0).Rows(intCnt).Item("UVTableCode") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UVTableCode"))
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
        Dim strUVTableCode As String
        Dim strTemperature As String
        Dim strDensity As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIf(Not srchStatusList.SelectedItem.Value = objPM.EnumCPOPropertiesStatus.All, srchStatusList.SelectedItem.Value, "")

        strUVTableCode = srchUVTableCode.Text
        strTemperature = srchTemperature.Text
        strDensity = srchDensity.Text
        strUpdateBy = srchUpdateBy.Text
        strSortExp = SortExpression.Text
        strSortCol = sortcol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PM_Rpt_CPOPropertiesMasterList.aspx?strStatus=" & strStatus & _
                       "&strUVTableCode=" & strUVTableCode & _
                       "&strTemperature=" & strTemperature & _
                       "&strDensity=" & strDensity & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&TitleTag=" & TitleTag & _
                       "&UVTableCodeTag=" & TableCodeTag & _
                       "&TemperatureTag=" & TemperatureTag & _
                       "&DensityTag=" & DensityTag & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
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
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex
            EventData.EditItemIndex = -1
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As DropDownList
        Dim Updbutton As LinkButton
        Dim validateUVTableCode As RequiredFieldValidator
        Dim validateTemperature As RequiredFieldValidator
        Dim validateDensity As RequiredFieldValidator
        Dim TableCode As String

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)
        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtUVTableCode")
        TableCode = Trim(EditText.Text)
        List = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lstUVTableCode")
        List.Enabled = False
        BindTableCodeDropList(List, TableCode)

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtStatus")
        Select Case CInt(EditText.Text) = objPM.EnumCPOPropertiesStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtTemperature")
                EditText.ReadOnly = True
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtTemperature")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtDensity")
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
        validateUVTableCode = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateUVTableCode")
        validateTemperature = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateTemperature")
        validateDensity = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateDensity")

        validateTemperature.ErrorMessage = strValidateTemperature
        validateDensity.ErrorMessage = strValidateDensity

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim TableCode As String
        Dim Temperature As String
        Dim Density As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim CreateDate As String

        list = E.Item.FindControl("lstUVTableCode")
        TableCode = list.SelectedItem.Value
        EditText = E.Item.FindControl("txtTemperature")
        Temperature = EditText.Text
        EditText = E.Item.FindControl("txtDensity")
        Density = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.SelectedItem.Value
        EditText = E.Item.FindControl("txtCreateDate")
        CreateDate = EditText.Text
        strParam = TableCode & "|" & _
                    Temperature & "|" & _
                    Density & "|" & _
                    Status & "|" & _
                    CreateDate

        Try
            intErrNo = objPM.mtdUpdCPOProperties(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_CPOPROPERTIES&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_CPOPropertiesMaster.aspx")
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
        Dim list As DropDownList
        Dim EditText As TextBox
        Dim TableCode As String
        Dim Temperature As String
        Dim Density As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim CreateDate As String

        list = E.Item.FindControl("lstUVTableCode")
        TableCode = list.SelectedItem.Value
        EditText = E.Item.FindControl("txtTemperature")
        Temperature = EditText.Text
        EditText = E.Item.FindControl("txtDensity")
        Density = EditText.Text
        EditText = E.Item.FindControl("txtStatus")
        Status = IIf(EditText.Text = objPM.EnumCPOPropertiesStatus.Active, _
                        objPM.EnumCPOPropertiesStatus.Deleted, _
                        objPM.EnumCPOPropertiesStatus.Active)
        EditText = E.Item.FindControl("txtCreateDate")
        CreateDate = EditText.Text
        strParam = TableCode & "|" & _
                    Temperature & "|" & _
                    Density & "|" & _
                    Status & "|" & _
                    CreateDate
        Try
            intErrNo = objPM.mtdUpdCPOProperties(strOppCd_ADD, _
                                            strOppCd_UPD, _
                                            strOppCd_GET, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            blnDupKey, _
                                            blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_CPOPROPERTIES&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_CPOPropertiesMaster.aspx")
        End Try

        EventData.EditItemIndex = -1

        BindGrid()
        BindPageList()
    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim validateTableCode As RequiredFieldValidator
        Dim validateTemperature As RequiredFieldValidator
        Dim validateDensity As RequiredFieldValidator
        Dim List As DropDownList
        Dim intRecNo As Integer, intPageNo As Integer

        intRecNo = Session("SS_PAGINGCOUNT")
        intPageNo = objGlobal.mtdGetPageCount(intRecNo, EventData.PageSize)
        If intRecNo Mod EventData.PageSize = 0 Then
            dataSet.Tables(0).Clear()
            intPageNo = intPageNo + 1
        ElseIf lblCurrentIndex.Text <> intPageNo - 1 Then
            lblCurrentIndex.Text = intPageNo - 1
            dataSet = LoadData()
        End If
        If intPageNo = 0 Then
            intPageNo = 1
        End If
        lblCurrentIndex.Text = intPageNo - 1
        lblPageCount.Text = intPageNo

        blnUpdate.Text = False

        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("UVTableCode") = ""
        newRow.Item("Temperature") = 0
        newRow.Item("Density") = 0
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)

        EventData.DataSource = dataSet

        lblTracker.Text = "Page " & lblPageCount.Text & " of " & lblPageCount.Text
        BindPageList()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count - 1
        EventData.DataBind()
        BindStatusList(EventData.EditItemIndex)
        List = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("lstUVTableCode")
        List.Enabled = True
        BindTableCodeDropList(List, "")

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

        validateTableCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateUVTableCode")
        validateTemperature = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateTemperature")
        validateDensity = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDensity")

        validateTableCode.ErrorMessage = strValidateTableCode
        validateTemperature.ErrorMessage = strValidateTemperature
        validateDensity.ErrorMessage = strValidateDensity

    End Sub


End Class
