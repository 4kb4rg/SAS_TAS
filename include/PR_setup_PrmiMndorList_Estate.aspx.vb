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
Imports agri.GL


Public Class PR_setup_PrmiMndorList_Estate : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtGolId As TextBox
    Protected WithEvents srcpmonth As DropDownList
    Protected WithEvents srcpyear As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim ParamNama As String
    Dim ParamValue As String
    Dim objBankDs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBank), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "PMdrID"
            End If
            If Not Page.IsPostBack Then
                BindAccYear("")
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
        Dim dr As DataRow
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
                intselection = intCnt + 1
            End If
        Next intCnt

        dr = objAccYearDs.Tables(0).NewRow()
        dr("AccYear") = ""
        dr("UserName") = "All"
        objAccYearDs.Tables(0).Rows.InsertAt(dr, 0)

        srcpyear.DataSource = objAccYearDs.Tables(0)
        srcpyear.DataValueField = "AccYear"
        srcpyear.DataTextField = "UserName"
        srcpyear.DataBind()
        srcpyear.SelectedIndex = intselection
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
                Case objHRSetup.EnumBankStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objHRSetup.EnumBankStatus.Deleted
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
        Dim strOpCd_GET As String = "PR_PR_STP_PREMI_MANDOR_GET"
        Dim strSearch As String
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        If (srcpmonth.SelectedItem.Value.Trim = "") Or (srcpyear.SelectedItem.Value.Trim = "") Then
            strSearch = "AND A.Status like '" & IIf(Not ddlStatus.SelectedItem.Value = "0", ddlStatus.SelectedItem.Value, "%") & "' "
        Else
            strSearch = " AND ('" & srcpyear.SelectedItem.Value.Trim & srcpmonth.SelectedItem.Value.Trim & "' >= right(rtrim(periodestart),4)+left(rtrim(periodestart),2)) AND " & _
                             "('" & srcpyear.SelectedItem.Value.Trim & srcpmonth.SelectedItem.Value.Trim & "' <= right(rtrim(periodeend),4)+left(rtrim(periodeend),2)) AND " & _
                             "A.Status like '" & IIf(Not ddlStatus.SelectedItem.Value = "0", ddlStatus.SelectedItem.Value, "%") & "' "
        End If

        If Not txtGolId.Text = "" Then
            strSearch = strSearch & " AND A.PRBrondolCode like '%" & _
                        txtGolId.Text & "%' "
        End If


        If Not txtLastUpdate.Text = "" Then
            strSearch = strSearch & " AND B.UserName like '%" & _
                        txtLastUpdate.Text & "%' "
        End If



        sortitem = "ORDER BY " & SortExpression.Text & " " & SortCol.Text
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem


        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GET, ParamNama, ParamValue, objBankDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_MANDOR_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objBankDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
                objBankDs.Tables(0).Rows(intCnt).Item("PMdrID") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("PMdrID"))
                objBankDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If
        Return objBankDs
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

    Sub NewAstekBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_PrmiMndor_Estate.aspx")
    End Sub

End Class
