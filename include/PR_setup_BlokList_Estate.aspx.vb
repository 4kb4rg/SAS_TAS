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

Public Class PR_setup_BlokList_Estate : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtBlokCode As TextBox
    Protected WithEvents txtDivID As TextBox
    Protected WithEvents srcttanam As DropDownList
    Protected WithEvents srcdivisi As DropDownList
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
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "Divisi,ThnCode,SubBlkCode"
            End If
            If Not Page.IsPostBack Then
                BindDivisi()
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

    Sub BindDivisi()
        Dim strOpCd_DivId As String = "PR_PR_STP_DIVISICODE_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim objDeptCodeDs As New Object

        strSearch = "And A.LocCode='" & strLocation & "' AND A.Status='1'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objDeptCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objDeptCodeDs.Tables(0).Rows.Count - 1
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next

        dr = objDeptCodeDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "All"
        objDeptCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        srcdivisi.DataSource = objDeptCodeDs.Tables(0)
        srcdivisi.DataValueField = "BlkGrpCode"
        srcdivisi.DataTextField = "Description"
        srcdivisi.DataBind()
    End Sub

    Sub BindYearPlan(ByVal dv As String)
        Dim strOpCd_yearPlan As String = "PR_PR_STP_YEARPLAN_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim objyearplan As New Object

        strSearch = "AND A.BlkGrpCode='" & dv & "' AND A.LocCode='" & strLocation & "' And A.Status='1'"
        sortitem = ""
        ParamNama = "STRSEARCH|SORTEXP"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_yearPlan, ParamNama, ParamValue, objYearPlan)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEPTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objYearPlan.Tables(0).Rows.Count - 1
            objYearPlan.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objYearPlan.Tables(0).Rows(intCnt).Item("BlkCode"))
            objYearPlan.Tables(0).Rows(intCnt).Item("Description") = Trim(objYearPlan.Tables(0).Rows(intCnt).Item("Description"))
        Next

        dr = objYearPlan.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Pilih Tahun Tanam"
        objYearPlan.Tables(0).Rows.InsertAt(dr, 0)

        srcttanam.DataSource = objyearplan.Tables(0)
        srcttanam.DataValueField = "BlkCode"
        srcttanam.DataTextField = "Description"
        srcttanam.DataBind()
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "PR_PR_STP_BLOK_GET"
        Dim strSearch As String
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSearch = " AND sub.loccode='" & strLocation & "' AND sub.Status like '" & IIf(Not ddlStatus.SelectedItem.Value = "0", _
                       ddlStatus.SelectedItem.Value, "%") & "' "

        If Not txtBlokCode.Text = "" Then
            strSearch = strSearch & " AND SubBlkCode= '" & txtBlokCode.Text & "' "
        End If

        If Not srcdivisi.SelectedItem.Value.Trim = "" Then
            strSearch = strSearch & " AND thn.BlkGrpCode like '%" & srcdivisi.SelectedItem.Value.Trim & "%' "
        End If

        If Not srcttanam.Text = "" Then
            strSearch = strSearch & " AND sub.BlkCode like '%" & srcttanam.SelectedItem.Value.Trim & "%' "
        End If

        If Not txtLastUpdate.Text = "" Then
            strSearch = strSearch & " AND Usr.UserName like '%" & _
                        txtLastUpdate.Text & "%' "
        End If

        
        sortitem = "ORDER BY " & SortExpression.Text & " " & SortCol.Text
        ParamNama = "Loc|SEARCH|SORT"
        ParamValue = strLocation & "|" & strSearch & "|" & sortitem


        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GET, ParamNama, ParamValue, objBlokDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_BLOK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

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

    Sub srcdivisi_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindYearPlan(srcdivisi.SelectedItem.Value.Trim())
    End Sub

    Sub NewDeptBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_Blokdet_Estate.aspx")
    End Sub

End Class
