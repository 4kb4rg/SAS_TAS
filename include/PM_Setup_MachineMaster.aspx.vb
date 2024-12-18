Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Web.Services

Public Class PM_Setup_MachineMaster : Inherits Page

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
    Protected WithEvents srchMachineCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblMachineCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblList As Label

    Protected objPM As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strOppCd_GET As String = "PM_CLSSETUP_MACHINE_GET"
    Dim strOppCd_ADD As String = "PM_CLSSETUP_MACHINE_ADD"
    Dim strOppCd_UPD As String = "PM_CLSSETUP_MACHINE_UPD"
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
    Dim TitleTag As String
    Dim MachineCodeTag As String
    Dim DescriptionTag As String

    Protected WithEvents txtProcessLine As TextBox
    Protected WithEvents ddlProcessingLine As DropDownList
	
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineMaster), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "MachineCode"
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
        lblTitle.Text = "MACHINE MASTER" 
        lblMachineCode.Text = "Machine Code" 
        lblDescription.Text = "Description" 

        strValidateCode = lblPleaseEnter.Text & lblMachineCode.Text & "."
        strValidateDesc = lblPleaseEnter.Text & lblDescription.Text & "."

        EventData.Columns(0).HeaderText = lblMachineCode.Text
        EventData.Columns(1).HeaderText = lblDescription.Text

        TitleTag = lblTitle.Text & lblList.Text
        MachineCodeTag = lblMachineCode.Text
        DescriptionTag = lblDescription.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_SETUP_MACHINE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineMaster.aspx")
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
        StatusList.Items.Add(New ListItem(objPM.mtdGetMachineStatus(objPM.EnumMachineStatus.Active), objPM.EnumMachineStatus.Active))
        StatusList.Items.Add(New ListItem(objPM.mtdGetMachineStatus(objPM.EnumMachineStatus.Deleted), objPM.EnumMachineStatus.Deleted))

    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objPM.mtdGetMachineStatus(objPM.EnumMachineStatus.All), objPM.EnumMachineStatus.All))
        srchStatusList.Items.Add(New ListItem(objPM.mtdGetMachineStatus(objPM.EnumMachineStatus.Active), objPM.EnumMachineStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPM.mtdGetMachineStatus(objPM.EnumMachineStatus.Deleted), objPM.EnumMachineStatus.Deleted))
        srchStatusList.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet

        Dim MachineCode As String
        Dim Desc As String
        Dim UpdateBy As String
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String
        Dim intCnt As Integer


        SearchStr = " AND M.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objPM.EnumMachineStatus.All, _
                       srchStatusList.SelectedItem.Value, "%") & "' AND M.LocCode='" & strLocation & "'"

        If Not srchMachineCode.Text = "" Then
            SearchStr = SearchStr & " AND M.MachineCode like '" & srchMachineCode.Text & "%' "
        End If
        If Not srchDescription.Text = "" Then
            SearchStr = SearchStr & " AND M.Description like '" & srchDescription.Text & "%' "
        End If
        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & srchUpdateBy.Text & "%' "
        End If

        If Not txtProcessLine.Text = "" Then
            SearchStr = SearchStr & " AND ProcessingLnNo = " & txtProcessLine.Text
        End If

        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objPM.mtdGetMachine(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_MACHINE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineMaster.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                objDataSet.Tables(0).Rows(intCnt).Item("MachineCode") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("MachineCode"))
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
        Dim strMachineCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String
        Dim strProcessLn As String 

        strStatus = IIf(Not srchStatusList.SelectedItem.Value = objPM.EnumMachineStatus.All, srchStatusList.SelectedItem.Value, "")
        strMachineCode = srchMachineCode.Text
        strDescription = srchDescription.Text
        strUpdateBy = srchUpdateBy.Text
        strSortExp = SortExpression.Text
        strProcessLn = txtProcessLine.Text 
        strSortCol = sortcol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PM_Rpt_MachineMasterList.aspx?strStatus=" & strStatus & _
                       "&strMachineCode=" & strMachineCode & _
                       "&strDescription=" & strDescription & _
                       "&strProcessLine=" & strProcessLn & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&TitleTag=" & TitleTag & _
                       "&MachineCodeTag=" & MachineCodeTag & _
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
        Dim txtMachineCode As TextBox 

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtStatus")
        Select Case CInt(EditText.Text) = objPM.EnumMachineStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtMachineCode")
                EditText.ReadOnly = True
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtMachineCode")
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
        validateCode = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateMachineCode")
        validateDesc = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateDescription")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

        ddlProcessingLine = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlProcessingLine")
        txtMachineCode = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtMachineCode")
        BindProcessingLine (ddlProcessingLine, txtMachineCode.Text)

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim MachineCode As String
        Dim Description As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim CreateDate As String
        Dim lstProcessLn As DropDownList
        Dim strProcessLn As String 

        EditText = E.Item.FindControl("txtMachineCode")
        MachineCode = EditText.Text
        EditText = E.Item.FindControl("txtDescription")
        Description = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.SelectedItem.Value
        EditText = E.Item.FindControl("txtCreateDate")
        CreateDate = EditText.Text
        lstProcessLn = E.Item.FindControl("ddlProcessingLine")
        strProcessLn = lstProcessLn.SelectedItem.Value
        strParam = MachineCode & "|" & _
                    Description & "|" & _
                    Status & "|" & _
                    CreateDate & "|" & strProcessLn 

        Try
            intErrNo = objPM.mtdUpdMachine(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_MACHINE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineMaster.aspx")
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
        Dim MachineCode As String
        Dim Description As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim CreateDate As String
        Dim lstProcessLn As DropDownList
        Dim strProcessLn As String 

        EditText = E.Item.FindControl("txtMachineCode")
        MachineCode = EditText.Text
        EditText = E.Item.FindControl("txtDescription")
        Description = EditText.Text
        EditText = E.Item.FindControl("txtStatus")
        Status = IIf(EditText.Text = objPM.EnumMachineStatus.Active, _
                        objPM.EnumMachineStatus.Deleted, _
                        objPM.EnumMachineStatus.Active)
        EditText = E.Item.FindControl("txtCreateDate")
        CreateDate = EditText.Text
        lstProcessLn = E.Item.FindControl("ddlProcessingLine")
        strProcessLn = lstProcessLn.SelectedItem.Value
        strParam = MachineCode & "|" & _
                    Description & "|" & _
                    Status & "|" & _
                    CreateDate & "|" & strProcessLn 
        Try
            intErrNo = objPM.mtdUpdMachine(strOppCd_ADD, _
                                            strOppCd_UPD, _
                                            strOppCd_GET, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            blnDupKey, _
                                            blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_MACHINE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineMaster.aspx")
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
        newRow.Item("MachineCode") = ""
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

        validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateMachineCode")
        validateDesc = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDescription")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

        ddlProcessingLine = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ddlProcessingLine")
        BindProcessingLine (ddlProcessingLine, "")
    End Sub

    Sub BindProcessingLine (ByRef lstProcessingLine As DropDownList, ByVal pstrMachineCode As String)
        Dim objPLNDataSet As New Object()
        Dim objMachineDS As New Object()
        Dim strParam As String
        Dim strSearchStr As String
        Dim strSortItem As String
        Dim dr As DataRow
        Dim strPLNOppCd_GET As String = "PM_CLSSETUP_PROCESSINGLINE_GET"
        Dim inSelectedIdx As Integer = 0
        Dim inCtr As Integer = 0

        strSearchStr = " AND PL.LocCode = '" & strLocation & "'"
        strSortItem = " ORDER BY ProcessingLnNo "
        strParam = strSortItem & "|" & strSearchStr

        Try
            intErrNo = objPM.mtdGetProcessingLine(strPLNOppCd_GET, strParam, objPLNDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_PROCESSINGLINE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineMaster.aspx")
        End Try

        strSearchStr = " AND M.MachineCode = '" & pstrMachineCode & "'"
        strSearchStr = strSearchStr + " AND M.LocCode = '" & strLocation & "'"
        strSortItem = ""
        strParam = strSortItem & "|" & strSearchStr

        Try
            intErrNo = objPM.mtdGetMachine(strOppCd_GET, strParam, objMachineDS)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_MACHINE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineMaster.aspx")
        End Try

        If objMachineDS.Tables(0).Rows.Count = 1 AND objPLNDataSet.Tables(0).Rows.Count > 0 Then
            For inCtr = 0 To objPLNDataSet.Tables(0).Rows.Count - 1
                If objPLNDataSet.Tables(0).Rows(inCtr).Item("ProcessingLnNo") = objMachineDS.Tables(0).Rows(0).Item("ProcessingLnNo") Then
                    Exit For
                End If
                inSelectedIdx = inSelectedIdx + 1
            Next
        End If

        If objPLNDataSet.Tables(0).Rows.Count > 0 Then
            lstProcessingLine.DataSource = objPLNDataSet.Tables(0)
            lstProcessingLine.DataTextField = "ProcessingLnNo"
            lstProcessingLine.DataValueField = "ProcessingLnNo"
            lstProcessingLine.DataBind()
            lstProcessingLine.SelectedIndex = inSelectedIdx
        End If

    End Sub

End Class
