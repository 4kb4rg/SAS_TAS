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

Public Class HR_trx_MandoranList_Estate : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtBlokCode As TextBox
    Protected WithEvents ddldivid As DropDownList
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents ddlEmpCode As DropDownList

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

    Function GetMdrType(ByVal t As String) As String
        Select Case t
            Case "P"
                GetMdrType = "Potong Buah"
            Case "K"
                GetMdrType = "Kutip Brondolan"
            Case "M"
                GetMdrType = "Muat TBS"
            Case "R"
                GetMdrType = "Perawatan"
            Case "D"
                GetMdrType = "Deres/Rawat Karet"
            Case "J"
                GetMdrType = "Panen/Rawat Jati"
            Case "S"
                GetMdrType = "Keamanan"
            Case "T"
                GetMdrType = "Traksi"
            Case "A"
                GetMdrType = "Administrasi"
        End Select
    End Function

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
                SortExpression.Text = "Divisi,Jabatan"
            End If
            If Not Page.IsPostBack Then
                BindDivision()
                BindGrid()
                BindPageList()
            End If
            lblErrMessage.Visible = False
        End If
    End Sub

    Sub BindDivision()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim objEmpDivDs As New Object

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
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
        dr("BlkGrpCode") = " "
        dr("Description") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddldivid.DataSource = objEmpDivDs.Tables(0)
        ddldivid.DataTextField = "Description"
        ddldivid.DataValueField = "BlkGrpCode"
        ddldivid.DataBind()
        ddldivid.SelectedIndex = 0

    End Sub

    Sub BindEmployee(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
        Dim strAM as String
		
		If Cint(strAccMonth) < 10 Then
            strAM = "0" & rtrim(strAccMonth)
        Else
            strAM = rtrim(strAccMonth)
        End If

        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & strAM & "|" & strAccyear & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%'|ORDER BY EmpName"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description")) & " - " & Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("Job"))
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "Please Select Employee Code"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpCode.DataSource = objEmpCodeDs.Tables(0)
        ddlEmpCode.DataTextField = "_Description"
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intSelectedIndex

    End Sub

    Sub ddldivid_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strEmpDivCode As String = ddldivid.SelectedItem.Value.Trim()
        BindEmployee(strEmpDivCode)
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

    Function ExistEmpCode(ByVal str As String) As String
        Dim strOpCd_Get As String = "HR_HR_TRX_EMPMANDORLN_CON"
        Dim strParamName As String
        Dim strParamValue As String
        Dim strSearch As String = ""
        Dim intErrNo As Integer
        Dim objHvLnDs As New Object()

        strParamName = "LOC|SEARCH"
        strParamValue = strLocation & "| WHERE emp='" & str & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objHvLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMANDORLN_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objHvLnDs.Tables(0).Rows.Count = 1 Then
            strSearch = Trim(objHvLnDs.Tables(0).Rows(0).Item("mandorcode"))
        End If

        Return strSearch.Trim

    End Function

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "HR_HR_TRX_EMPMANDOR_GET"
        Dim strSearch As String
        Dim strtemp As String = ""
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objMdr As New Object()

        strSearch = " AND A.Status like '" & IIf(Not ddlStatus.SelectedItem.Value = "0", _
                       ddlStatus.SelectedItem.Value, "%") & "' "

        If Not txtBlokCode.Text = "" Then
            strSearch = strSearch & " AND B.EmpName like '%" & _
                        txtBlokCode.Text & "%' "
        End If

        If Not ddldivid.SelectedItem.Value.Trim = "" Then
            strSearch = strSearch & " AND A.IDDiv like '%" & ddldivid.SelectedItem.Value.Trim & "%' "
        End If

        If Not txtLastUpdate.Text = "" Then
            strSearch = strSearch & " AND E.UserName like '%" & _
                        txtLastUpdate.Text & "%' "
        End If

        If Not ddlEmpCode.Text.Trim = "" Then
            If Not ddlEmpCode.SelectedItem.Value.Trim = "" Then
                strtemp = ExistEmpCode(ddlEmpCode.SelectedItem.Value.Trim)
                If strtemp.Trim <> "" Then
                    strSearch = " AND A.MandorCode like '%" & strtemp & "%' "
                    lblErrMessage.Text = ddlEmpCode.SelectedItem.Text.Trim & " terdaftar di kode mandor " & strtemp
                Else
                    lblErrMessage.Text = ddlEmpCode.SelectedItem.Text.Trim & " belum terdaftar !"
                End If
                lblErrMessage.Visible = True
            End If
        End If

        sortitem = "ORDER BY " & SortExpression.Text & " " & SortCol.Text
        ParamNama = "LOC|SEARCH|SORT"
        ParamValue = strLocation & "|" & strSearch & "|" & sortitem

        tluas = 0
        tpokok = 0

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GET, ParamNama, ParamValue, objMdr)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMANDOR_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        Dim strOpCd_UPD As String = "HR_HR_TRX_EMPMANDOR_DEL"
        Dim int As Integer = e.Item.ItemIndex
        Dim DivCell As TableCell = e.Item.Cells(0)
        Dim strBlokCd As String
        Dim strIdDiv As String
        Dim strStatus As String
        Dim intErrNo As Integer
        Dim EditText As Label


        strBlokCd = DivCell.Text
        EditText = dgLine.Items.Item(int).FindControl("Status")
        strStatus = IIf(EditText.Text = "Active", "2", "1")

        ParamNama = "ST|UD|UI|ID"
        ParamValue = strStatus & "|" & DateTime.Now & "|" & strUserId & "|" & strBlokCd

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewDeptBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_MandoranDet_Estate.aspx")
    End Sub

End Class
