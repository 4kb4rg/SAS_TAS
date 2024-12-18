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

Public Class PR_setup_PrmKaretList_Estate : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtBlokCode As TextBox
    Protected WithEvents txtDivID As TextBox

    Protected WithEvents srcpmonth As DropDownList
    Protected WithEvents srcpyear As DropDownList

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
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "periodestart,periodeend"
            End If
            If Not Page.IsPostBack Then
                srcpmonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) -1
                BindAccYear(Session("SS_SELACCYEAR"))
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

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intselection As Integer = 0
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If pv_strAccYear = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear")) Then
                intselection = intCnt
            End If
        Next intCnt

        srcpyear.DataSource = objAccYearDs.Tables(0)
        srcpyear.DataValueField = "AccYear"
        srcpyear.DataTextField = "UserName"
        srcpyear.DataBind()
        srcpyear.SelectedIndex = intselection
    End Sub


    Sub OnDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        Const target = "PR_setup_PrmKaretDet_Estate.aspx?periodestart={0}&periodeend={1}&loccode={2}"
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim ctrl = CType(e.Item.FindControl("LinkColumn"), HyperLink)
                Dim lb As Label
                Dim ps As String
                Dim pe As String
                Dim yp As String

                lb = CType(e.Item.FindControl("periodestart"), Label)
                ps = lb.Text.Trim()

                lb = CType(e.Item.FindControl("periodeend"), Label)
                pe = lb.Text.Trim()

                Dim row = CType(e.Item.DataItem, DataRowView)
                If ctrl IsNot Nothing Then

                    Dim rw_ps = row("periodestart")
                    Dim rw_pe = row("periodeend")
                    ctrl.NavigateUrl = String.Format(target, rw_ps, rw_pe, strLocation)
                End If
        End Select
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

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "PR_PR_STP_PREMIDERES_GET"
        Dim strSearch As String
        Dim sortitem As String
        Dim intErrNo As Integer

        strSearch = " AND ('" & srcpyear.SelectedItem.Value.Trim & srcpmonth.SelectedItem.Value.Trim & "' >= right(rtrim(periodestart),4)+left(rtrim(periodestart),2)) AND " & _
                             "('" & srcpyear.SelectedItem.Value.Trim & srcpmonth.SelectedItem.Value.Trim & "' <= right(rtrim(periodeend),4)+left(rtrim(periodeend),2)) AND " & _
                             "Sub.Status like '" & IIf(Not ddlStatus.SelectedItem.Value = "0", ddlStatus.SelectedItem.Value, "%") & "' AND LocCode='" & strLocation & "'"

        If Not txtLastUpdate.Text = "" Then
            strSearch = strSearch & " AND Usr.UserName like '%" & _
                        txtLastUpdate.Text & "%' "
        End If


        sortitem = "ORDER BY " & SortExpression.Text & " " & SortCol.Text
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem


        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GET, ParamNama, ParamValue, objBlokDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIDERES_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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

    Sub NewDeptBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_PrmKaretDet_Estate.aspx")
    End Sub

End Class
