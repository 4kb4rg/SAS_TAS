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

Public Class GL_Setup_ExpenseCode : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchExpCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchUpdBy As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents lblErrLen As Label
    Protected WithEvents lblExpenseCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label

    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strOppCd_GET As String = "GL_CLSSETUP_EXPCODE_LIST_SEARCH"
    Dim strOppCd_ADD As String = "GL_CLSSETUP_EXPCODE_LIST_ADD"
    Dim strOppCd_UPD As String = "GL_CLSSETUP_EXPCODE_LIST_UPD"

    Dim objConfigDs As New Object()
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
    Dim intGLAR As Integer
    Dim intMaxLen As Integer = 0
    Dim strValidateCode As String
    Dim strValidateDesc As String

    Dim DocTitleTag As String
    Dim ExpenseTag As String
    Dim ExpDescTag As String
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLExpense), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "ExpenseCode"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
                AssignMaxLength(intMaxLen)
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub AssignMaxLength(ByRef pr_intMaxLen As Integer)
        Dim strOpCd As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim intErrNo As Integer

        Try
            intErrNo = objSys.mtdGetConfigInfo(strOpCd, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               objConfigDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTIVITY_GETMAXLENGTH&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_Activity.aspx")
        End Try

        pr_intMaxLen = CInt(Trim(objConfigDs.Tables(0).Rows(0).Item("ExpenseLen")))
        srchExpCode.MaxLength = pr_intMaxLen
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

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Expense)) & UCase(lblCode.Text)
        lblExpenseCode.Text = GetCaption(objLangCap.EnumLangCap.Expense) & lblCode.Text
        lblDescription.Text = GetCaption(objLangCap.EnumLangCap.ExpenseDesc)
        strValidateCode = lblPleaseEnter.Text & lblExpenseCode.Text & "."
        strValidateDesc = lblPleaseEnter.Text & lblDescription.Text & "."

        EventData.Columns(0).HeaderText = lblExpenseCode.Text
        EventData.Columns(1).HeaderText = lblDescription.Text

        DocTitleTag = lblTitle.Text
        ExpenseTag = GetCaption(objLangCap.EnumLangCap.Expense)
        ExpDescTag = lblDescription.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_EXPENSECODE_LIST&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_ExpenseCode.aspx")
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
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex

    End Sub

    Sub BindStatusList(ByVal index As Integer)

        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetExpCodeStatus(objGLSetup.EnumExpCodeStatus.Active), objGLSetup.EnumExpCodeStatus.Active))
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetExpCodeStatus(objGLSetup.EnumExpCodeStatus.Deleted), objGLSetup.EnumExpCodeStatus.Deleted))

    End Sub

    Sub BindSearchList()
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetExpCodeStatus(objGLSetup.EnumExpCodeStatus.All), objGLSetup.EnumExpCodeStatus.All))
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetExpCodeStatus(objGLSetup.EnumExpCodeStatus.Active), objGLSetup.EnumExpCodeStatus.Active))
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetExpCodeStatus(objGLSetup.EnumExpCodeStatus.Deleted), objGLSetup.EnumExpCodeStatus.Deleted))
        srchStatus.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet

        Dim strParam As String
        Dim SearchStr As String
        Dim sortItem As String


        SearchStr = " AND Expense.Status like '" & IIf(srchStatus.SelectedItem.Value <> objGLSetup.EnumExpCodeStatus.All, srchStatus.SelectedItem.Value, "%") & "' "

        If Not srchExpCode.Text = "" Then
            SearchStr = SearchStr & " AND Expense.ExpenseCode like '" & srchExpCode.Text & "%'"
        End If
        If Not srchDescription.Text = "" Then
            SearchStr = SearchStr & " AND Expense.Description like '" & srchDescription.Text & "%'"
        End If
        If Not srchUpdBy.Text = "" Then
            SearchStr = SearchStr & " AND Usr.UserName like '" & srchUpdBy.Text & "%'"
        End If

        sortItem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortItem & "|" & SearchStr

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOppCd_GET, strParam, objGLSetup.EnumGLMasterType.ActGrp, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_CLSETUP_GET_EXPENSECODE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_ExpenseCode.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strExpenseCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIf(Not srchStatus.SelectedItem.Value = objGLSetup.EnumExpCodeStatus.All, srchStatus.SelectedItem.Value, "")
        strExpenseCode = srchExpCode.Text
        strDescription = srchDescription.Text
        strUpdateBy = srchUpdBy.Text
        strSortExp = SortExpression.Text
        strSortCol = sortcol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_ExpenseCode.aspx?strStatus=" & strStatus & _
                       "&strExpenseCode=" & strExpenseCode & _
                       "&strDescription=" & strDescription & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&DocTitleTag=" & DocTitleTag & _
                       "&ExpenseTag=" & ExpenseTag & _
                       "&ExpDescTag=" & ExpDescTag & _
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
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim lbUpdbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid()
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(txtEditText.Text) = objGLSetup.EnumExpCodeStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ExpenseCode")
                txtEditText.ReadOnly = True
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Delete"
                lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ExpenseCode")
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
        Dim ddlList As DropDownList
        Dim strExpenseCode As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim strCreateDate As String

        AssignMaxLength(intMaxLen)

        txtEditText = E.Item.FindControl("ExpenseCode")
        strExpenseCode = txtEditText.Text

        If strExpenseCode.Length = intMaxLen Then
        Else
            lblMsg = E.Item.FindControl("lblErrLen")
            lblMsg.Text = "The Expense Code should be in " & intMaxLen & " character(s)."
            lblMsg.Visible = True
            Exit Sub
        End If

        txtEditText = E.Item.FindControl("Description")
        strDescription = txtEditText.Text
        ddlList = E.Item.FindControl("StatusList")
        strStatus = ddlList.SelectedItem.Value
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        strParam = strExpenseCode & "|" & _
                    strDescription & "|" & _
                    strStatus & "|" & _
                    strCreateDate
        Try
            intErrNo = objGLSetup.mtdUpdMasterList(strOppCd_ADD, _
                                              strOppCd_UPD, _
                                              strOppCd_GET, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objGLSetup.EnumGLMasterType.ExpCode, _
                                              blnDupKey, _
                                              blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_UPDATE_EXPENSECODE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_ActivityGrp.aspx")
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
        Dim strExpenseCode As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim strCreateDate As String

        txtEditText = E.Item.FindControl("ExpenseCode")
        strExpenseCode = txtEditText.Text
        txtEditText = E.Item.FindControl("Description")
        strDescription = txtEditText.Text
        txtEditText = E.Item.FindControl("Status")
        strStatus = IIf(txtEditText.Text = objGLSetup.EnumExpCodeStatus.Active, _
                        objGLSetup.EnumExpCodeStatus.Deleted, _
                        objGLSetup.EnumExpCodeStatus.Active)
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        strParam = strExpenseCode & "|" & _
                    strDescription & "|" & _
                    strStatus & "|" & _
                    strCreateDate
        Try
            intErrNo = objGLSetup.mtdUpdMasterList(strOppCd_ADD, _
                                              strOppCd_UPD, _
                                              strOppCd_GET, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objGLSetup.EnumGLMasterType.ExpCode, _
                                              blnDupKey, _
                                              blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_UPDATE_EXPENSECODE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_ActivityGrp.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim lbUpdbutton As LinkButton
        Dim txtExpenseCode As TextBox
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("ExpenseCode") = ""
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

        lbUpdbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        lbUpdbutton.Visible = False

        AssignMaxLength(intMaxLen)
        txtExpenseCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ExpenseCode")
        txtExpenseCode.MaxLength = intMaxLen

        validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        validateDesc = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc
    End Sub


End Class
