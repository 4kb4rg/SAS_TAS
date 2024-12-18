Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl

Public Class HR_EmployeeQlfList : Inherits Page

    Protected WithEvents dgEmpQlfList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblEmpName As Label
    Protected WithEvents lblQualificationID As Label
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblEmpStatus As Label
    Protected WithEvents NewEmpQlfBtn As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbDetails As LinkButton
    Protected WithEvents lbPayroll As LinkButton
    Protected WithEvents lbEmployment As LinkButton
    Protected WithEvents lbStatutory As LinkButton
    Protected WithEvents lbFamily As LinkButton
    Protected WithEvents lbSkill As LinkButton
    Protected WithEvents lbCareerProg As LinkButton
    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objEmpDs As New Object()
    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strSelectedEmpCode As String = ""
    Dim strSelectedEmpName As String = ""
    Dim strSelectedQualificationID As String = ""
    Dim strSelectedEmpStatus As String = ""
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeQualification), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblRedirect.Text = Request.QueryString("redirect")
            If SortExpression.Text = "" Then
                SortExpression.Text = "QualificationCode"     
            End If
            strSelectedEmpCode = Trim(IIf(Request.QueryString("EmpCode") <> "", Request.QueryString("EmpCode"), Request.Form("EmpCode")))
            strSelectedEmpName = Trim(IIf(Request.QueryString("EmpName") <> "", Request.QueryString("EmpName"), Request.Form("EmpName")))
            strSelectedQualificationID = Trim(IIf(Request.QueryString("QualificationID") <> "", Request.QueryString("QualificationID"), Request.Form("QualificationID")))
            strSelectedEmpStatus = Trim(IIf(Request.QueryString("EmpStatus") <> "", Request.QueryString("EmpStatus"), Request.Form("EmpStatus")))
            If Not Page.IsPostBack Then
                BindGrid() 
                BindPageList()
                onLoad_BindButton()
                onload_LinkButton()     
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lbCareerProg.Text = GetCaption(objLangCap.EnumLangCap.CareerProgress)   
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPQLFLIST_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/hr_trx_employeelist.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function


    Sub onload_LinkButton()
        If lblEmpStatus.Text = 0 Then
            TrLink.Visible = False
        Else
            TrLink.Visible = True
        End If
    End Sub

    Private Sub lbDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDetails.Click
        Response.Redirect("HR_trx_EmployeeDet.aspx?redirect=empdet&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbPayroll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbPayroll.Click
        Response.Redirect("HR_trx_EmployeePay.aspx?redirect=emppay&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbEmployment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEmployment.Click
        Response.Redirect("HR_trx_EmployeeEmp.aspx?redirect=empemp&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbStatutory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbStatutory.Click
        Response.Redirect("HR_trx_EmployeeStat.aspx?redirect=empstat&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFamily.Click
        Response.Redirect("HR_trx_EmployeeFamList.aspx?redirect=empfam&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbSkill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSkill.Click
        Response.Redirect("HR_trx_EmployeeSkillList.aspx?redirect=empskill&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbCareerProg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCareerProg.Click
        Response.Redirect("HR_trx_EmployeeCPList.aspx?redirect=empcp&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Sub onLoad_BindButton()
        NewEmpQlfBtn.visible = False
        btnBack.visible = False
        Select Case CInt(strSelectedEmpStatus)
            Case objHR.EnumEmpStatus.Active, objHR.EnumEmpStatus.Pending
                NewEmpQlfBtn.visible = True
                btnBack.visible = True 
            Case objHR.EnumEmpStatus.Deleted
                btnBack.visible = True
        End Select
    End Sub

    Sub BindGrid() 
        Dim strOpCd_Get As String = "HR_CLSTRX_QUALIFICATION_GET"
        Dim strParam as string
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim PageNo as Integer
        Dim lbButton As LinkButton

        lblEmpCode.Text = strSelectedEmpCode
        lblEmpName.Text = strSelectedEmpName
        lblEmpStatus.Text = strSelectedEmpStatus

        strParam = "|" & strSelectedEmpCode & "|||" & SortExpression.Text & "|" & SortCol.Text
 
        Try
            intErrNo = objHR.mtdGetEmployeeQlf(strOpCd_Get, strParam, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_EMPLOYEEQUALIFICATION&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpName"))
            objEmpDs.Tables(0).Rows(intCnt).Item("QualificationID") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("QualificationID"))
            objEmpDs.Tables(0).Rows(intCnt).Item("QualificationCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("QualificationCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("QlfDesc") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("QualificationCode")) & " (" & _
                                                              Trim(objEmpDs.Tables(0).Rows(intCnt).Item("QlfDesc")) & ")"
            objEmpDs.Tables(0).Rows(intCnt).Item("SubjectCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("SubjectCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("SubjDesc") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("SubjectCode")) & " (" & _
                                                               Trim(objEmpDs.Tables(0).Rows(intCnt).Item("SubjDesc")) & ")"
            objEmpDs.Tables(0).Rows(intCnt).Item("Remark") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("Remark"))
        Next                

        dgEmpQlfList.DataSource = objEmpDs
        dgEmpQlfList.DataBind()

        If CInt(lblEmpStatus.text) = objHR.EnumEmpStatus.Active or CInt(lblEmpStatus.text) = objHR.EnumEmpStatus.Pending Then
            For intCnt = 0 To dgEmpQlfList.Items.Count - 1
                lbButton = dgEmpQlfList.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = True
                lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Next
        End If

        PageNo = dgEmpQlfList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & Pageno & " of " & dgEmpQlfList.PageCount
    End Sub 

    Sub BindPageList() 
        Dim count as integer = 1   
        Dim arrDList As New ArrayList()

        While not count = dgEmpQlfList.PageCount + 1
            arrDList.Add("Page " & count)
            Count = Count + 1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgEmpQlfList.CurrentPageIndex
    End Sub 

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                dgEmpQlfList.CurrentPageIndex = 0
            Case "prev"
                dgEmpQlfList.CurrentPageIndex = _
                Math.Max(0, dgEmpQlfList.CurrentPageIndex - 1)
            Case "next"
                dgEmpQlfList.CurrentPageIndex = _
                Math.Min(dgEmpQlfList.PageCount - 1, dgEmpQlfList.CurrentPageIndex + 1)
            Case "last"
                dgEmpQlfList.CurrentPageIndex = dgEmpQlfList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgEmpQlfList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            dgEmpQlfList.CurrentPageIndex = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        dgEmpQlfList.CurrentPageIndex=e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgEmpQlfList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub
 
    Sub DEDR_QlfDet(Sender As Object, E As DataGridCommandEventArgs)
        Dim lblCodeValue As Label
        Dim lblNameValue As Label
        Dim lblQualificationIDValue As Label
        Dim intIndex As Integer = E.Item.ItemIndex    
        Dim strEmpCode As String
        Dim strEmpName As String
        Dim strEmpStatus As String
        Dim strQualificationID As String
        Dim strLink As String

        lblCodeValue = dgEmpQlfList.Items.Item(intIndex).FindControl("lblEmpCode")
        lblNameValue = dgEmpQlfList.Items.Item(intIndex).FindControl("lblEmpName")
        lblQualificationIDValue = dgEmpQlfList.Items.Item(intIndex).FindControl("lblQualificationID")
        strEmpCode = lblCodeValue.Text
        strEmpName = lblNameValue.Text
        strQualificationID = lblQualificationIDValue.Text
        strEmpStatus = lblEmpStatus.text

        strLink = "HR_trx_EmployeeQlfDet.aspx?redirect=empqlf&EmpCode=" & strEmpCode & _
                  "&EmpName=" & strEmpName & _
                  "&QualificationID=" & strQualificationID & _
                  "&EmpStatus=" & strEmpStatus

        Response.Redirect(strLink)
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim strOpCd_DelQlf As String = "HR_CLSTRX_QUALIFICATION_DEL"
        Dim intIndex As Integer = E.Item.ItemIndex    
        Dim lblQualificationIDValue As Label
        Dim strQualificationID As String
        Dim intErrNo As Integer

        lblQualificationIDValue = dgEmpQlfList.Items.Item(intIndex).FindControl("lblQualificationID")
        strQualificationID = lblQualificationIDValue.Text

        Try
            intErrNo = objHR.mtdDelEmployeeQlf(strOpCd_DelQlf, _
                                               strQualificationID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_DELETE_QUALIFICATION&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeQlfList.aspx")
        End Try

        dgEmpQlfList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewEmpQlfBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeQlfDet.aspx?redirect=" & lblRedirect.Text & "&new=yes&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Sub btnBack_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeList.aspx?redirect=" & lblRedirect.Text)
    End Sub

End Class
