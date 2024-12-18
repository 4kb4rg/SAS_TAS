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

Public Class DP_BeneficiaryList : Inherits Page

    Protected WithEvents dgEmpList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlUnit As DropDownList
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox
    Protected WithEvents txtGangCode As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label
    Protected WithEvents lblTotBeneficiary As Label
    Protected WithEvents lblTotBenefit As Label

    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim blnCanDelete As Boolean = False
    Dim objEmpDs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblRedirect.Text = Request.QueryString("redirect")
            If SortExpression.Text = "" Then
                SortExpression.Text = "NoPesP"
            End If

            SwitchMode(lblRedirect.Text)
            If Not Page.IsPostBack Then
                BindUnit()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub SwitchMode(ByVal pv_strQuery As String)
        If pv_strQuery = "empdet" Then
            blnCanDelete = True
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        dgEmpList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
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
        BindPageList()
        PageNo = lblCurrentIndex.Text + 1
        lblTracker.Text = "Page " & PageNo & " of " & PageCount

        If blnCanDelete = True Then
            For intCnt = 0 To dgEmpList.Items.Count - 1
                Select Case CInt(objEmpDs.Tables(0).Rows(intCnt).Item("Status"))
                    Case objHR.EnumEmpStatus.Pending
                        lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objHR.EnumEmpStatus.Active
                        lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objHR.EnumEmpStatus.Deleted
                        lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
                End Select
            Next
        Else
            For intCnt = 0 To dgEmpList.Items.Count - 1
                lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = False
            Next
        End If

       
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim SearchStr As String
        Dim dbBenefit As Double

        Session("SS_PAGING") = lblCurrentIndex.Text

        strOpCd_Get = "DP_CLSTRX_MEMBER_LIST_GET"
        SearchStr = ""

        If Not txtEmpCode.Text = "" Then SearchStr = SearchStr & " AND NoPes LIKE '" & txtEmpCode.Text & "%' "
        If Not txtEmpName.Text = "" Then SearchStr = SearchStr & " AND Nam LIKE '%" & txtEmpName.Text & "%' "
        If Not ddlUnit.SelectedItem.Value = "" Then SearchStr = SearchStr & " AND Unit = '" & ddlUnit.SelectedItem.Value & "' "
        SearchStr = SearchStr & " Order By " & SortExpression.Text & " " & SortCol.Text

        strParamName = "STRSEARCH"
        strParamValue = "WHERE NoPesp <> '' " & SearchStr

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                                strParamName, _
                                                strParamValue, _
                                                objEmpDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INVOICERCV_GET_LINE&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvnotelist.aspx")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            dbBenefit += objEmpDs.Tables(0).Rows(intCnt).Item("Benefit")
        Next

        lblTotBenefit.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dbBenefit, 2), 2)
        lblTotBeneficiary.Text = intCnt

        Return objEmpDs
    End Function

    Sub EmpLink_Click(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If Not E.CommandName.ToString = "Sort" And Not E.CommandName.ToString = "Delete" Then
            Dim lbl As Label
            Dim intIndex As Integer = E.Item.ItemIndex
            Dim strEmpCode As String
            Dim strEmpName As String
            Dim strEmpStatus As String
            Dim strEmpMarital As String
            Dim strEmpGender As String
            Dim strLink As String

            lbl = dgEmpList.Items.Item(intIndex).FindControl("lblEmpCode")
            strEmpCode = lbl.Text
            lbl = dgEmpList.Items.Item(intIndex).FindControl("lblEmpName")
            strEmpName = lbl.Text
            lbl = dgEmpList.Items.Item(intIndex).FindControl("lblEmpStatus")
            strEmpStatus = lbl.Text
            lbl = dgEmpList.Items.Item(intIndex).FindControl("lblEmpMarital")
            strEmpMarital = lbl.Text
            lbl = dgEmpList.Items.Item(intIndex).FindControl("lblEmpGender")
            strEmpGender = lbl.Text

            Select Case LCase(lblRedirect.Text)
                Case "emptrx"
                    strLink = "DP_trx_EmployeeTransactionList.aspx?redirect=empcp&EmpCode=" & strEmpCode & "&EmpName=" & strEmpName & "&EmpStatus=" & strEmpStatus
                Case "empdet"
                    strLink = "DP_trx_EmployeeDet.aspx?redirect=empdet&EmpCode=" & strEmpCode & "&EmpName=" & strEmpName & "&EmpStatus=" & strEmpStatus
                Case "emppay"
                    strLink = "HR_trx_EmployeePay.aspx?redirect=emppay&EmpCode=" & strEmpCode & "&EmpName=" & strEmpName & "&EmpStatus=" & strEmpStatus
                Case "empemp"
                    strLink = "HR_trx_EmployeeEmp.aspx?redirect=empemp&EmpCode=" & strEmpCode & "&EmpName=" & strEmpName & "&EmpStatus=" & strEmpStatus
                Case "empstat"
                    strLink = "HR_trx_EmployeeStat.aspx?redirect=empstat&EmpCode=" & strEmpCode & "&EmpName=" & strEmpName & "&EmpStatus=" & strEmpStatus
                Case "empfam"
                    strLink = "HR_trx_EmployeeFamList.aspx?redirect=empfam&EmpCode=" & strEmpCode & "&EmpName=" & strEmpName & "&EmpStatus=" & strEmpStatus & "&EmpMarital=" & strEmpMarital & "&EmpGender=" & strEmpGender
                Case "empqlf"
                    strLink = "HR_trx_EmployeeQlfList.aspx?redirect=empqlf&EmpCode=" & strEmpCode & "&EmpName=" & strEmpName & "&EmpStatus=" & strEmpStatus
                Case "empskill"
                    strLink = "HR_trx_EmployeeSkillList.aspx?redirect=empskill&EmpCode=" & strEmpCode & "&EmpName=" & strEmpName & "&EmpStatus=" & strEmpStatus
            End Select
            Response.Redirect(strLink)
        End If

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count)
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

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Status As String = "HR_CLXTRX_EMPLOYEE_STATUS_UPD"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim lblEmpCode As Label
        Dim strEmpCode As String

        dgEmpList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblEmpCode = dgEmpList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblEmpCode")
        strEmpCode = lblEmpCode.Text

        strParam = strEmpCode & "|" & objHR.EnumEmpStatus.Deleted

        Try
            intErrNo = objHR.mtdUpdEmployeeDetStatus(strOpCd_Status, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strParam)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_DELETE_EMPLOYEE&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try
        dgEmpList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub BindUnit()
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objUnit As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "DP_CLSTRX_UNIT_LIST_GET"

        strParamName = "STRSEARCH"
        strParamValue = "WHERE CAB = '01'" '& strLocation

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objUnit)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_UNIT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objUnit.Tables(0).Rows.Count - 1
            objUnit.Tables(0).Rows(intCnt).Item("Unit") = Trim(objUnit.Tables(0).Rows(intCnt).Item("Unit"))
            objUnit.Tables(0).Rows(intCnt).Item("Nama") = Trim(objUnit.Tables(0).Rows(intCnt).Item("Nama"))
        Next intCnt

        dr = objUnit.Tables(0).NewRow()
        dr("Unit") = ""
        dr("Nama") = "Please Select Unit Code"
        objUnit.Tables(0).Rows.InsertAt(dr, 0)

        ddlUnit.DataSource = objUnit.Tables(0)
        ddlUnit.DataValueField = "Unit"
        ddlUnit.DataTextField = "Nama"
        ddlUnit.DataBind()
        ddlUnit.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub BeneficiaryDet_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("DP_trx_BeneficiaryDet.aspx?redirect=empdet")
    End Sub

    Sub BeneficiaryTrx_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("DP_trx_BeneficiaryTransactionDet.aspx?redirect=empdet")
    End Sub

    Sub BeneficiaryAD_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("DP_trx_BeneficiaryADDet.aspx?redirect=empdet")
    End Sub

    Sub BeneficiaryMov_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("DP_trx_BeneficiaryMovingFund.aspx?redirect=empdet")
    End Sub

    Sub BeneficiaryOvr_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("DP_trx_BeneficiaryBenefit.aspx?redirect=empdet")
    End Sub


End Class
