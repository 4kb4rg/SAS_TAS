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


Public Class PR_DailyAttdDet_Estate : Inherits Page

    Protected WithEvents ddldivisicode As DropDownList
    Protected WithEvents dgLine As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnPrev As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lstDropList As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnNext As System.Web.UI.WebControls.ImageButton
    Protected WithEvents SortExpression As System.Web.UI.WebControls.Label
    Protected WithEvents SortCol As System.Web.UI.WebControls.Label
    Protected WithEvents lblTracker As System.Web.UI.WebControls.Label
    Protected WithEvents txtItemCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ibConfirm As System.Web.UI.WebControls.ImageButton


    Protected WithEvents lblErrMessage As Label
    

    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()


    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim Objok As New agri.GL.ClsTrx
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim objItemDs As New Object()

    Dim strDateFmt As String
    Dim strAcceptFormat As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            lblErrMessage.Text = "Page Is Expired"
            lblErrMessage.Visible = True
        Else
            If Not IsPostBack Then
                BindDivisi()
            End If


        End If
    End Sub

    Sub BindDivisi()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim StrFilter As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "Pilih Divisi"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddldivisicode.DataSource = objEmpDivDs.Tables(0)
        ddldivisicode.DataTextField = "Description"
        ddldivisicode.DataValueField = "BlkGrpCode"
        ddldivisicode.DataBind()
        ddldivisicode.SelectedIndex = 0
    End Sub

    Protected Sub ibConfirm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibConfirm.Click
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

    '-------Create Paging Dropdownlist--------------------
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

    '-------Calls the com to create a dataset  --------------------
    Protected Function LoadData() As DataSet

        Dim strOpCode As String = "HR_HR_TRX_EMPLOYEE_SEARCH"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strSortExp As String

        If Trim(SortCol.Text) = "" Then
            strSortExp = ""
        Else
            strSortExp = " ORDER BY " & SortExpression.Text & " " & SortCol.Text
        End If

        strParamName = "SEARCH|SORT"
        strParamValue = "AND (EmpCode like '%" & txtItemCode.Text & "%' OR EmpName like '%" & txtItemCode.Text & "%') AND IDDiv like '%" & ddldivisicode.SelectedItem.Value.Trim() & "%'|" & strSortExp

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_SEARCH&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objItemDs

    End Function

    '------------ Event for clicking on Previous or Next ---------------
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

    '------------ Event for Changing the Paging DropDownList ------------
    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    '------------ Event for a Page Change -----------------
    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    '------------ Event for Sorting Of Grid -----------------
    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub OnSelectItem(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim CPCell As TableCell = e.Item.Cells(0)
        Dim CPCell1 As TableCell = e.Item.Cells(1)
        Response.Write("<Script Language=""JavaScript"">window.location.href=""PR_trx_BKMDet_Estate.aspx"";window.close(); </Script>")
    End Sub

End Class
