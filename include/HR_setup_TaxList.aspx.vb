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

Public Class HR_setup_TaxList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtTaxCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objHRSetup As New agri.HR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSys As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Dim objTaxDs As New Object()
    Dim objConfigDs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTax), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "tax.TaxCode"
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
        Dim strOpCdGet As String = "HR_ClSSETUP_TAX_GET"
        Dim strSearchExp As String
        Dim strSortExp As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSearchExp = ""

        If txtTaxCode.Text <> "" Then
            strSearchExp = strSearchExp & "and tax.TaxCode like '" & trim(txtTaxCode.Text) & "%' "
        End If

        If txtDesc.Text <> "" Then
            strSearchExp = strSearchExp & "and tax.Description like '" & trim(txtDesc.Text) & "%' "
        End If

        If ddlStatus.SelectedItem.Value <> "" Then
            strSearchExp = strSearchExp & "and tax.Status = '" & ddlStatus.SelectedItem.Value & "' "
        End If

        If txtLastUpdate.Text <> "" Then
            strSearchExp = strSearchExp & "and usr.UserName like '" & txtLastUpdate.Text & "%' "
        End If

        strSortExp = "order by " & SortExpression.Text & " " & SortCol.Text

        strParam = strSortExp & "|" & strSearchExp 

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objTaxDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objTaxDs
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
        Dim strOpCdUpd As String = "HR_CLSSETUP_TAX_UPD_STS"
        Dim strOpCdGet As String = "HR_CLSSETUP_TAX_GET"
        Dim strOpCdAdd As String = "HR_CLSSETUP_TAX_ADD"
        Dim lbl As Label
        Dim strParam As String = ""
        Dim strSelTaxCode As String
        Dim intErrNo As Integer

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lbl = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblLnId")
        strSelTaxCode = lbl.Text

        strParam = strSelTaxCode & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & objHRSetup.EnumTaxStatus.Deleted & chr(9) & chr(9) & chr(9) & chr(9) & chr(9)
        
        
        Try
            intErrNo = objHRSetup.mtdUpdTax(strOpCdGet, _
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXLIST_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_taxlist")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewTBBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_setup_TaxDet.aspx")
    End Sub

End Class
