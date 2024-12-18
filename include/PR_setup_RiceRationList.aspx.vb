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

Public Class PR_setup_RiceRationList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents lblTitle As Label
    Protected WithEvents lblRiceCode As Label
    Protected WithEvents lblRiceDesc As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents txtRiceRationCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label

    Protected objPRSetup As New agri.PR.clsSetup
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl
    Dim objAR As New agri.GlobalHdl.clsAccessRights
    Dim objLangCap As New agri.PWSystem.clsLangCap
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Dim objRiceDs As New DataSet
    Dim objLangCapDs As New Object

    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_ACCMONTH")
        strAccYear = Session("SS_ACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRice), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If SortExpression.Text = "" Then
                SortExpression.Text = "Rice.RiceRationCode"
            End If

            If Not Page.IsPostBack Then
                BindStatus()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.RiceRation))
        lblRiceCode.text = GetCaption(objLangCap.EnumLangCap.RiceRation) & lblCode.text
        lblRiceDesc.text = GetCaption(objLangCap.EnumLangCap.RiceRationDesc)

        dgLine.Columns(1).HeaderText = lblRiceCode.Text
        dgLine.Columns(2).HeaderText = lblRiceDesc.Text

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RICERATIONLIST_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=pr/Setup/pr_setup_ricerationlist.aspx")
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

        dsData = LoadData()
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
        lblTracker.Text = "Page " & PageNo & " of " & dgLine.PageCount

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objPRSetup.EnumAttStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPRSetup.EnumAttStatus.Deleted
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
        Next

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList

        While Not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub

    Sub BindStatus()

        ddlStatus.Items.Add(New ListItem(objPRSetup.mtdGetRiceStatus(objPRSetup.EnumRiceStatus.All), objPRSetup.EnumRiceStatus.All))
        ddlStatus.Items.Add(New ListItem(objPRSetup.mtdGetRiceStatus(objPRSetup.EnumRiceStatus.Active), objPRSetup.EnumRiceStatus.Active))
        ddlStatus.Items.Add(New ListItem(objPRSetup.mtdGetRiceStatus(objPRSetup.EnumRiceStatus.Deleted), objPRSetup.EnumRiceStatus.Deleted))

        ddlStatus.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet

        Dim strOpCd_GET As String = "PR_CLSSETUP_RICERATION_LIST_GET"
        Dim strSrchRiceRationCode As String
        Dim strSrchDesc As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intCnt As Integer

        strSrchRiceRationCode = IIf(txtRiceRationCode.Text = "", "", txtRiceRationCode.Text)
        strSrchDesc = IIf(txtDescription.Text = "", "", txtDescription.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = objPRSetup.EnumRiceStatus.All, "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchRiceRationCode & "|" & _
                   strSrchDesc & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|"

        Try
            intErrNo = objPRSetup.mtdGetRiceRation(strOpCd_GET, strParam, objRiceDs, False)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RICERATION_GET&errmesg=" & lblErrMessage.Text & "&redirect=pr/setup/PR_setup_RiceRationList.aspx")
        End Try

        Return objRiceDs
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
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_UPD As String = "PR_CLSSETUP_RICERATION_DETAIL_UPD"
        Dim strOpCd_ADD As String = ""
        Dim strParam As String = ""
        Dim CPCell As TableCell = e.Item.Cells(0)
        Dim strSelectedRiceRationCode As String

        strSelectedRiceRationCode = CPCell.Text
        strParam = strSelectedRiceRationCode & "|||||||" & objPRSetup.EnumRiceStatus.Deleted & "||||||"

        Try
            intErrNo = objPRSetup.mtdUpdRiceRation(strOpCd_ADD, _
                                                   strOpCd_UPD, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam, _
                                                   True)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RICERATION_DEL&errmesg=" & lblErrMessage.Text & "&redirect=pr/setup/PR_setup_RiceRationList.aspx")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewRiceRationBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_RiceRationDet.aspx")
    End Sub



End Class
