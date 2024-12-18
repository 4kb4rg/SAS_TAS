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
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GL

Public Class PR_Setup_AttdList_Estate : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtAttdId As TextBox
    Protected WithEvents txtEmpTy As TextBox
    Protected WithEvents txtAttCd As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected objPRSetup As New agri.PR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim ObjOK As New agri.GL.ClsTrx

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
	Dim intLevel As Integer
    Dim ParamNama As String
    Dim ParamValue As String

    Dim objAttDs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
		intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = False) and (intLevel < 2) Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "A.CodeEmpTy"
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
                Case objPRSetup.EnumAttStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPRSetup.EnumAttStatus.Deleted
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Text = "UnDelete"
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

        Dim strOpCd_GET As String = "PR_PR_STP_ATTID_GET"
        Dim strSearch As String
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSearch = " AND A.Status like '" & IIf(Not ddlStatus.SelectedItem.Value = "0", _
                         ddlStatus.SelectedItem.Value, "%") & "' "
        If Not txtAttdId.Text = "" Then
            strSearch = strSearch & " AND A.AttID like '%" & _
                        txtAttdId.Text & "%' "
        End If

        If Not txtEmpTy.Text = "" Then
            strSearch = strSearch & " AND D.Description like '%" & txtEmpTy.Text & "%' "
        End If

        If Not txtAttCd.Text = "" Then
            strSearch = strSearch & " AND D.Description like '%" & txtAttCd.Text & "%' "
        End If

        If Not txtLastUpdate.Text = "" Then
            strSearch = strSearch & " AND B.UserName like '%" & _
                        txtLastUpdate.Text & "%' "
        End If

        sortitem = "ORDER BY " & SortExpression.Text & " " & SortCol.Text
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GET, ParamNama, ParamValue, objAttDs)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_ATTENDANCE_GET&errmesg=" & lblErrMessage.Text & "&redirect=pr/setup/PR_setup_AttdList.aspx")
        End Try

        If objAttDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objAttDs.Tables(0).Rows.Count - 1
                objAttDs.Tables(0).Rows(intCnt).Item("AttID") = Trim(objAttDs.Tables(0).Rows(intCnt).Item("AttID"))
                objAttDs.Tables(0).Rows(intCnt).Item("DescEmp") = Trim(objAttDs.Tables(0).Rows(intCnt).Item("DescEmp"))
                objAttDs.Tables(0).Rows(intCnt).Item("DescAtt") = Trim(objAttDs.Tables(0).Rows(intCnt).Item("DescAtt"))
                objAttDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objAttDs.Tables(0).Rows(intCnt).Item("Status"))
                objAttDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objAttDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objAttDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAttDs.Tables(0).Rows(intCnt).Item("UserName"))
                objAttDs.Tables(0).Rows(intCnt).Item("PotUpah") = IIf(Trim(objAttDs.Tables(0).Rows(intCnt).Item("PotUpah")) = "1", "Y", "N")
                objAttDs.Tables(0).Rows(intCnt).Item("PotBeras") = IIf(Trim(objAttDs.Tables(0).Rows(intCnt).Item("PotBeras")) = "1", "Y", "N")
                objAttDs.Tables(0).Rows(intCnt).Item("PotCuti") = IIf(Trim(objAttDs.Tables(0).Rows(intCnt).Item("PotCuti")) = "1", "Y", "N")
                objAttDs.Tables(0).Rows(intCnt).Item("PotWeekend") = IIf(Trim(objAttDs.Tables(0).Rows(intCnt).Item("PotWeekend")) = "1", "Y", "N")
            Next
        End If

        Return objAttDs
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
        Dim strOpCd_UPD As String = "PR_PR_STP_ATTID_LIST_DEL"
        Dim int As Integer = e.Item.ItemIndex
        Dim strAcCd As String
        Dim CPCell As TableCell = e.Item.Cells(0)
        Dim strStatus As String
        Dim intErrNo As Integer
        Dim EditText As Label

        strAcCd = CPCell.Text
        EditText = dgLine.Items.Item(int).FindControl("Status")
        strStatus = IIf(EditText.Text = "Active", "2", "1")

        ParamNama = "AI|ST|UD|UI"
        ParamValue = strAcCd & "|" & strStatus & "|" & DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewTBBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_AttdDet_Estate.aspx")
    End Sub

End Class
