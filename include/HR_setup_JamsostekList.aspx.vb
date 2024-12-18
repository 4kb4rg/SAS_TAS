Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Public Class HR_setup_JamsostekList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtJamCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents NewTBBtn As ImageButton

    Protected WithEvents lblTitle As Label
    Protected WithEvents lblJamsostek As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblCode As Label

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objHRSetup As New agri.HR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim objJamDs As New Object()
    Dim objConfigDs As New Object()
    Dim objLangCapDs As New Object()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRJamsostek), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "jam.JamCode"
            End If
            If Not Page.IsPostBack Then
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLine.PageSize)

        dgLine.DataSource = dsData
        If dgLine.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLine.CurrentPageIndex = 0
            Else
                dgLine.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgLine.DataBind()
        BindPageList()
        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text = "Page " & pageno & " of " & dgLine.PageCount

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objHRSetup.EnumTaxStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objHRSetup.EnumTaxStatus.Deleted
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
        Next
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCdGet As String = "HR_CLSSETUP_JAMSOSTEK_GET"
        Dim strSearchExp As String
        Dim strSortExp As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSearchExp = ""

        If txtJamCode.Text <> "" Then
            strSearchExp = strSearchExp & "and jam.JamCode like '" & trim(txtJamCode.Text) & "%' "
        End If

        If txtDesc.Text <> "" Then
            strSearchExp = strSearchExp & "and jam.Description like '" & trim(txtDesc.Text) & "%' "
        End If

        If ddlStatus.SelectedItem.Value <> "" Then
            strSearchExp = strSearchExp & "and jam.Status = '" & ddlStatus.SelectedItem.Value & "' "
        End If

        If txtLastUpdate.Text <> "" Then
            strSearchExp = strSearchExp & "and usr.UserName like '" & txtLastUpdate.Text & "%' "
        End If

        strSortExp = "order by " & SortExpression.Text & " " & SortCol.Text

        strParam = strSortExp & "|" & strSearchExp 

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objJamDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_JAMSOSTEKLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        Return objJamDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgLine.CurrentPageIndex = 0
            Case "prev"
                dgLine.CurrentPageIndex = _
                    Math.Max(0, dgLine.CurrentPageIndex - 1)
            Case "next"
                dgLine.CurrentPageIndex = _
                    Math.Min(dgLine.PageCount - 1, dgLine.CurrentPageIndex + 1)
            Case "last"
                dgLine.CurrentPageIndex = dgLine.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid()

    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "asc", "desc", "asc")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCdUpd As String = "HR_CLSSETUP_JAMSOSTEK_UPD_STS"
        Dim strOpCdGet As String = "HR_CLSSETUP_JAMSOSTEK_GET"
        Dim strOpCdAdd As String = "HR_CLSSETUP_JAMSOSTEK_ADD"
        Dim lbl As Label
        Dim strParam As String = ""
        Dim strSelJamCode As String
        Dim intErrNo As Integer

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lbl = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblLnId")
        strSelJamCode = lbl.Text

        strParam = strSelJamCode & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & objHRSetup.EnumJamsostekStatus.Deleted
        Try
            intErrNo = objHRSetup.mtdUpdJamsostek(strOpCdGet, _
                                                  strOpCdAdd, _
                                                  strOpCdUpd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  True, _
                                                  True, _
                                                  False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_JAMSOSTEKDET_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_jamsosteklist.aspx")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewTBBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_setup_JamsostekDet.aspx")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Jamsostek))
        lblJamsostek.Text = GetCaption(objLangCap.EnumLangCap.Jamsostek) & lblCode.Text
        lblDesc.Text = GetCaption(objLangCap.EnumLangCap.JamsostekDesc)
        dgLine.Columns(0).HeaderText = GetCaption(objLangCap.EnumLangCap.Jamsostek) & lblCode.Text
        dgLine.Columns(1).HeaderText = GetCaption(objLangCap.EnumLangCap.JamsostekDesc)
        NewTBBtn.AlternateText = "New " & GetCaption(objLangCap.EnumLangCap.Jamsostek)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_JAMSOSTEKLIST_LANGCAP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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


End Class
