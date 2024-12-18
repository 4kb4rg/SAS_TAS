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
Imports agri.HR.clsSetup
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap
Imports agri.GL

Public Class HR_trx_MutasiPenghasilanList_Estate : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCode As Label

    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long

    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim tluas As Double
    Dim tpokok As Double

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "MutasiPenghasilanCode"
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
                Case objHRSetup.EnumDeptStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Text = "Delete"
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objHRSetup.EnumDeptStatus.Deleted
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Text = "Undelete"
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('Undelete');"
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
        Dim strOpCd_GET As String = "HR_HR_TRX_EMPMUTASIPENGHASILAN_GET"
        Dim strSearch As String
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim objMdr As New Object()

        strSearch = " AND A.LocCode='" & strLocation & "' AND A.Status like '" & IIf(Not ddlStatus.SelectedItem.Value = "0", _
                       ddlStatus.SelectedItem.Value, "%") & "' "

        If Not txtEmpCode.Text = "" Then
            strSearch = strSearch & " AND A.EmpCode like '%" & _
                        txtEmpCode.Text & "%' "
        End If

        If Not txtEmpName.Text = "" Then
            strSearch = strSearch & " AND B.EmpName like '%" & txtEmpName.Text & "%' "
        End If

        If Not txtLastUpdate.Text = "" Then
            strSearch = strSearch & " AND G.UserName like '%" & _
                        txtLastUpdate.Text & "%' "
        End If


        sortitem = "ORDER BY " & SortExpression.Text & " " & SortCol.Text
        ParamNama = "LOC|SEARCH|SORT"
        ParamValue = strLocation & "|" & strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GET, ParamNama, ParamValue, objMdr)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMUTASI_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objMdr
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
        Dim strOpCd_UPD As String = "HR_HR_TRX_EMPMUTASIPENGHASILAN_DEL"
        Dim int As Integer = e.Item.ItemIndex
        Dim DivCell As TableCell = e.Item.Cells(0)
        Dim strid As String
        Dim strStatus As String
        Dim intErrNo As Integer
        Dim EditText As Label
        Dim idWH As String

        strid = DivCell.Text
        'EditText = dgLine.Items.Item(int).FindControl("lblwh")
        'idWH = EditText.Text.Trim()

        EditText = dgLine.Items.Item(int).FindControl("Status")

        strStatus = IIf(EditText.Text = "Active", "2", "1")

        ParamNama = "ST|UD|UI|MC"
        ParamValue = strStatus & "|" & Now() & "|" & strUserId & "|" & Trim(strid)

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_MutasiPenghasilanDet_Estate.aspx")
    End Sub

    Function Active(ByVal b As String) As String
        Dim s As String = ""
        Select Case Trim(b)
            Case "1" : s = "Active"
            Case "2" : s = "Deleted"
        End Select
        Return s
    End Function
End Class
