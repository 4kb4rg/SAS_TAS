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

Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl

Public Class PR_RKBList : Inherits Page

    Protected WithEvents dgEmpList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents NewEmpBtn As ImageButton
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label
    Protected WithEvents txtRKH As TextBox
    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlEmpMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList
 

    Dim ObjOk As New agri.GL.ClsTrx
    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim blnCanDelete As Boolean = False
    Dim objEmpDs As New Object()
	Dim intlevel As Integer

    Dim objEmpDivDs As New Object()
    Dim cnt As Double


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_PRAR")
		intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intHRAR) = False  AND intLevel < 1 Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "RKBCode"
            End If

            If Not Page.IsPostBack Then
                BindEmpDiv()
                BindAccYear(Session("SS_SELACCYEAR"))
                ddlEmpMonth.SelectedIndex = CInt(Session("SS_SELACCMONTH")) - 1
                If Session("RKB") <> "" Then
                    Dim prevset As String
                    Dim ary As Array
                    prevset = Session("RKB")
                    ary = Split(prevset, "|")
                    ddlEmpDiv.SelectedValue = ary(0)
                    ddlEmpMonth.SelectedValue = ary(1)
                    BindAccYear(ary(2))
                    lblCurrentIndex.Text = ary(3)
                End If
                BindGrid()
            End If
        End If
    End Sub

	Public Function getstat(byval st as String) as String
		Select Case trim(st)
			Case "1"
				Return "Confirm"
			Case "2"
				Return "Verified"
			Case "3"
				Return "Active"
			Case "4"
				Return "Deleted"
		End Select
		
    End Function
	
    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        dgEmpList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        ddlyear.DataSource = objAccYearDs.Tables(0)
        ddlyear.DataValueField = "AccYear"
        ddlyear.DataTextField = "UserName"
        ddlyear.DataBind()
        ddlyear.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub BindEmpDiv()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_DIVISI_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpDiv.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDiv.DataTextField = "Description"
        ddlEmpDiv.DataValueField = "BlkGrpCode"
        ddlEmpDiv.DataBind()
        ddlEmpDiv.SelectedIndex = 0

    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbstatus As Label

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgEmpList.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsData = LoadData()
            End If
        End If

        dgEmpList.DataSource = dsData
        dgEmpList.DataBind()
        lblPageCount.Text = PageCount
        BindPageList(PageCount)
        PageNo = lblCurrentIndex.Text + 1
        
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "PR_PR_TRX_RKB_MAIN_GET"
        Dim strSrchEmpDiv As String
        Dim strSrchMonth As String
        Dim strSrchYear As String
        Dim strSrchStatus As String
        Dim strSrchRKH As String

        Dim strSearch As String

        Dim strParamName As String
        Dim strParamValue As String

        Dim intErrNo As Integer
        Dim intCnt As Integer


        Dim strSortExpression As String


        Session("SS_PAGING") = lblCurrentIndex.Text

        cnt = 0
        strSrchRKH = IIf(txtRKH.Text.Trim() = "", "", txtRKH.Text.Trim())
        strSrchEmpDiv = IIf(ddlEmpDiv.SelectedItem.Value.Trim() = "", "", ddlEmpDiv.SelectedItem.Value.Trim())
        strSrchMonth = ddlEmpMonth.SelectedItem.Value.Trim()
        strSrchYear = ddlyear.SelectedItem.Value.Trim()
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value.Trim() = "0", "%", ddlStatus.SelectedItem.Value.Trim())


        If SortExpression.Text = "UserName" Then
            strSortExpression = "C.UserName"
        Else
            strSortExpression = SortExpression.Text & " " & SortCol.Text
        End If

        strSearch = "AND A.RKBCode Like '%" & strSrchRKH & "%' " & _
                    "AND A.IDDiv Like '" & strSrchEmpDiv & "%' " & _
                    "AND A.AccMonth = '" & strSrchMonth & "' " & _
                    "AND A.AccYear = '" & strSrchYear & "' " & _
                    "AND A.LocCode = '" & strLocation & "' " & _
                    "AND A.Status like '" & strSrchStatus & "%' "


        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & strSearch & "|ORDER BY " & strSortExpression

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKH_MAIN_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        
        Return objEmpDs
    End Function

    Sub BKMLink_Click(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If Not E.CommandName.ToString = "Sort" And Not E.CommandName.ToString = "Delete" Then
            Dim intIndex As Integer = E.Item.ItemIndex
            Dim hid As HiddenField = dgEmpList.Items.Item(intIndex).FindControl("hidbkm")

            Session("RKB") = ddlEmpDiv.SelectedItem.Value.Trim() & "|" & _
                          ddlEmpMonth.SelectedItem.Value.Trim() & "|" & _
                          ddlyear.SelectedItem.Value.Trim() & "|" & _
                          lblCurrentIndex.Text


            Response.Redirect("PR_trx_RKBDet.aspx?RKBCode=" & hid.Value.Trim)

        End If
    End Sub

    Sub BindPageList(ByVal cnt As String)
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count & " of " & cnt)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                lblCurrentIndex.Text = 0
            Case "prev"
                lblCurrentIndex.Text = _
                Math.Max(0, lblCurrentIndex.Text - 1)
            Case "next"
                lblCurrentIndex.Text = _
                Math.Min(lblPageCount.Text - 1, lblCurrentIndex.Text + 1)
            Case "last"
                lblCurrentIndex.Text = lblPageCount.Text - 1
        End Select
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub NewEmpBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_RKBDet.aspx")

    End Sub

End Class
