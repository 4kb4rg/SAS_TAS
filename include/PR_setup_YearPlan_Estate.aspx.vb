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
Imports agri.HR
Imports agri.PWSystem.clsLangCap

Public Class PR_Stp_YearPlan : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchdivisi As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchYearPlan As TextBox
    Protected WithEvents srchName As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblValidate As Label
    Protected WithEvents lblCode As Label

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "PR_PR_STP_YEARPLAN_GET"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim ObjOK As New agri.GL.ClsTrx
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
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDepartment), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "BlkCode"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindSearchList()
                BindDivisi()
                BindGrid()
                BindPageList()
            End If
        End If
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

        dsData = LoadData
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
        lblTracker.Text = "Page " & pageno & " of " & EventData.PageCount
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = EventData.PageCount + 1
            arrDList.Add("Page " & count)
            Count = Count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex

    End Sub

    Sub BindStatusList(ByVal index As Integer)

        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Active), objHR.EnumDeptCodeStatus.Active))
        StatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Deleted), objHR.EnumDeptCodeStatus.Deleted))

    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.All), objHR.EnumDeptCodeStatus.All))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Active), objHR.EnumDeptCodeStatus.Active))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Deleted), objHR.EnumDeptCodeStatus.Deleted))
        srchStatusList.SelectedIndex = 1

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

        srchdivisi.DataSource = objDeptCodeDs.Tables(0)
        srchdivisi.DataValueField = "BlkGrpCode"
        srchdivisi.DataTextField = "Description"
        srchdivisi.DataBind()
    End Sub

    Protected Function LoadData() As DataSet
        Dim SearchStr As String
        Dim sortitem As String


        SearchStr = " AND A.LocCode='" & strLocation & "' And A.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objHR.EnumDeptCodeStatus.All, _
                       srchStatusList.SelectedItem.Value, "%") & "' "
        If Not srchdivisi.SelectedItem.Value.Trim = "" Then
            SearchStr = SearchStr & " AND A.BlkGrpCode like '" & srchdivisi.SelectedItem.Value.Trim & "%' "
        End If
        If Not srchYearPlan.Text = "" Then
            SearchStr = SearchStr & " AND A.BlkCode like '" & srchYearPlan.Text & "%' "
        End If
        If Not srchUpdateBy.text = "" Then
            SearchStr = SearchStr & " AND B.UserName like '" & _
                        srchUpdateBy.text & "%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text
        ParamNama = "STRSEARCH|SORTEXP"
        ParamValue = SearchStr & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOppCd_GET, ParamNama, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Tostring & "&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
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
        SortExpression.text = e.SortExpression.ToString()
        sortcol.text = IIF(sortcol.text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub NewTBBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_YearPlanDet_Estate.aspx")
    End Sub

End Class
