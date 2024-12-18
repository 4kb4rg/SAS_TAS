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

Public Class HR_setup_BlokList_Estate : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtBlokCode As TextBox
    Protected WithEvents txtDivID As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblTitle As Label

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

    Dim objBlokDs As New Object()
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "BlokCode"
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
        Dim strOpCd_GET As String = "HR_HR_STP_BLOK_GET"
        Dim strSearch As String
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSearch = " AND A.LocCode='" & strlocation & "' AND A.Status like '" & IIf(Not ddlStatus.SelectedItem.Value = "0", _
                       ddlStatus.SelectedItem.Value, "%") & "' "
        If Not txtBlokCode.Text = "" Then
            strSearch = strSearch & " AND A.BlokCode like '%" & _
                        txtBlokCode.Text & "%' "
        End If
        If Not txtDescription.Text = "" Then
            strSearch = strSearch & " AND A.Description like '%" & _
                        txtDescription.Text & "%' "
        End If
        If Not txtDivID.Text = "" Then
            strSearch = strSearch & " AND D.Description like '%" & txtDivID.Text & "%' "
        End If
        If Not txtLastUpdate.Text = "" Then
            strSearch = strSearch & " AND B.UserName like '%" & _
                        txtLastUpdate.Text & "%' "
        End If

        
        sortitem = "ORDER BY " & SortExpression.Text & " " & SortCol.Text
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        tluas = 0
        tpokok = 0

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GET, ParamNama, ParamValue, objBlokDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_BLOK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objBlokDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objBlokDs.Tables(0).Rows.Count - 1
                objBlokDs.Tables(0).Rows(intCnt).Item("BlokCode") = Trim(objBlokDs.Tables(0).Rows(intCnt).Item("BlokCode"))
                objBlokDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBlokDs.Tables(0).Rows(intCnt).Item("Description"))
                objBlokDs.Tables(0).Rows(intCnt).Item("Division") = Trim(objBlokDs.Tables(0).Rows(intCnt).Item("Division"))
                objBlokDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objBlokDs.Tables(0).Rows(intCnt).Item("Status"))
                objBlokDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objBlokDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objBlokDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objBlokDs.Tables(0).Rows(intCnt).Item("UserName"))
                tluas = tluas + objBlokDs.Tables(0).Rows(intCnt).Item("Luas")
                tpokok = tpokok + objBlokDs.Tables(0).Rows(intCnt).Item("TotPKK")
            Next
        End If

        Return objBlokDs
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
        Dim strOpCd_UPD As String = "HR_HR_STP_BLOK_LIST_DEL"
        Dim int As Integer = e.Item.ItemIndex
        Dim DivCell As TableCell = e.Item.Cells(0)
        Dim strBlokCd As String
        Dim strIdDiv As String
        Dim strStatus As String
        Dim intErrNo As Integer
        Dim EditText As Label


        strBlokCd = DivCell.Text
        EditText = dgLine.Items.Item(int).FindControl("IDDiv")
        strIdDiv = EditText.Text
        EditText = dgLine.Items.Item(int).FindControl("Status")
        strStatus = IIf(EditText.Text = "Active", "2", "1")

        ParamNama = "BlokCd|IdDiv|ST|UD|UI"
        ParamValue = strBlokCd & "|" & strIdDiv & "|" & strStatus & "|" & DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub KeepRunningSum(ByVal sender As Object, ByVal E As DataGridItemEventArgs)
        If E.Item.ItemType = ListItemType.Footer Then
            E.Item.Cells(5).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(tluas, 2)
            E.Item.Cells(6).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(tpokok, 2)
        End If

    End Sub
    Sub NewDeptBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_setup_Blokdet_Estate.aspx")
    End Sub

End Class
