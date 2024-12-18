Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class HR_ContractorSuper : Inherits Page

    Protected WithEvents dgContSuperList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtSuppCode As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label

    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim objContDs As New Dataset()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRContractorSupervision), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "CG.SupplierCode"
            End If

            If Not Page.IsPostBack Then
                BindStatusList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindStatusList()

        ddlStatus.Items.Add(New ListItem(objHRSetup.mtdGetContSuperStatus(objHRSetup.EnumContSupervision.All), objHRSetup.EnumContSupervision.All))
        ddlStatus.Items.Add(New ListItem(objHRSetup.mtdGetContSuperStatus(objHRSetup.EnumContSupervision.Active), objHRSetup.EnumContSupervision.Active))
        ddlStatus.Items.Add(New ListItem(objHRSetup.mtdGetContSuperStatus(objHRSetup.EnumContSupervision.Deleted), objHRSetup.EnumContSupervision.Deleted))

        ddlStatus.SelectedIndex = 1
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgContSuperList.CurrentPageIndex = 0
        dgContSuperList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lblStatus As Label
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgContSuperList.PageSize)

        dgContSuperList.DataSource = dsData
        If dgContSuperList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgContSuperList.CurrentPageIndex = 0
            Else
                dgContSuperList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgContSuperList.DataBind()
        BindPageList()
        PageNo = dgContSuperList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgContSuperList.PageCount
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgContSuperList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgContSuperList.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "HR_CLSSETUP_CONTRACTOR_SUPER_GET"
        Dim strSrchSuppCode As String
        Dim strSrchName As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchSuppCode = IIf(txtSuppCode.Text = "", "", txtSuppCode.Text)
        strSrchName = IIf(txtName.Text = "", "", txtName.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = objHRSetup.EnumContSupervision.All, "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchSuppCode & "|" & _
                   strSrchName & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   strLocation & "||" & _
                   SortExpression.Text & " " & SortCol.Text
        Try
            intErrNo = objHRSetup.mtdGetContractorSupervision(strOpCd_Get, strParam, objContDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSSETUP_CONTSUPERLIST_GET&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ContractorSuperList.aspx")
        End Try

        For intCnt = 0 To objContDs.Tables(0).Rows.Count - 1
            objContDs.Tables(0).Rows(intCnt).Item("SupplierCode") = objContDs.Tables(0).Rows(intCnt).Item("SupplierCode").Trim()
            objContDs.Tables(0).Rows(intCnt).Item("Name") = objContDs.Tables(0).Rows(intCnt).Item("Name").Trim()
            objContDs.Tables(0).Rows(intCnt).Item("Status") = objContDs.Tables(0).Rows(intCnt).Item("Status").Trim()
        Next

        Return objContDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgContSuperList.CurrentPageIndex = 0
            Case "prev"
                dgContSuperList.CurrentPageIndex = _
                Math.Max(0, dgContSuperList.CurrentPageIndex - 1)
            Case "next"
                dgContSuperList.CurrentPageIndex = _
                Math.Min(dgContSuperList.PageCount - 1, dgContSuperList.CurrentPageIndex + 1)
            Case "last"
                dgContSuperList.CurrentPageIndex = dgContSuperList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgContSuperList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgContSuperList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgContSuperList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgContSuperList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub NewSuppBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_setup_ContractorSuperDet.aspx")
    End Sub


End Class
