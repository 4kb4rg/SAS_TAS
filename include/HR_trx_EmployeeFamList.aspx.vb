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

Public Class HR_EmployeeFamList : Inherits Page

    Protected WithEvents dgEmpFamList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblEmpName As Label
    Protected WithEvents lblFamMemberID As Label
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblEmpStatus As Label
    Protected WithEvents lblEmpMarital As Label
    Protected WithEvents NewEmpFamBtn As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbDetails As LinkButton    
    Protected WithEvents lbPayroll As LinkButton
    Protected WithEvents lbEmployment As LinkButton
    Protected WithEvents lbStatutory As LinkButton
    Protected WithEvents lbQualific As LinkButton
    Protected WithEvents lbSkill As LinkButton
    Protected WithEvents lbCareerProg As LinkButton

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objEmpDs As New Object()
    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strEmpCode As String = ""
    Dim strEmpName As String = ""
    Dim strEmpStatus As String = ""
    Dim strEmpMarital As String = ""
    Dim strEmpGender As String = ""
    Protected WithEvents lblEmpGender As Label
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeFamily), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblRedirect.Text = Request.QueryString("redirect")
            If SortExpression.Text = "" Then
                SortExpression.Text = "Relationship"
            End If

            strEmpCode = Trim(Request.QueryString("EmpCode"))
            strEmpName = Trim(Request.QueryString("EmpName"))
            strEmpStatus = Trim(Request.QueryString("EmpStatus"))
            strEmpMarital = Trim(Request.QueryString("EmpMarital"))
            strEmpGender = Trim(Request.QueryString("EmpGender"))
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPFAMLIST_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/hr_trx_employeelist.aspx")
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

    Private Sub lbSatutory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbStatutory.Click
        Response.Redirect("HR_trx_EmployeeStat.aspx?redirect=empstat&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbQualific_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbQualific.Click
        Response.Redirect("HR_trx_EmployeeQlfList.aspx?redirect=empqlf&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbSkill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSkill.Click
        Response.Redirect("HR_trx_EmployeeSkillList.aspx?redirect=empskill&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbCareerProg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCareerProg.Click
        Response.Redirect("HR_trx_EmployeeCPList.aspx?redirect=empcp&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub


    Sub onLoad_BindButton()
        NewEmpFamBtn.visible = False
        btnBack.visible = False 
        Select Case CInt(strEmpStatus)
            Case objHR.EnumEmpStatus.Active, objHR.EnumEmpStatus.Pending
                NewEmpFamBtn.visible = True
                btnBack.visible = True 
            Case objHR.EnumEmpStatus.Deleted
                btnBack.visible = True
        End Select
    End Sub

    Sub BindGrid() 
        Dim strOpCd_Get As String = "HR_CLSTRX_FAMILY_GET"
        Dim strParam as string
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim PageNo as Integer
        Dim lbButton As LinkButton

        strParam = "|" & strEmpCode & "|||" & SortExpression.Text & "|" & SortCol.Text
        
        Try
            intErrNo = objHR.mtdGetEmployeeFam(strOpCd_Get, strParam, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPFAMLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        lblEmpCode.Text = strEmpCode
        lblEmpName.Text = strEmpName
        lblEmpStatus.Text = strEmpStatus
        lblEmpMarital.Text = strEmpMarital
        lblEmpGender.Text = strEmpGender
        dgEmpFamList.DataSource = objEmpDs
        dgEmpFamList.DataBind()

        If CInt(lblEmpStatus.text) = objHR.EnumEmpStatus.Active or CInt(lblEmpStatus.text) = objHR.EnumEmpStatus.Pending Then
            For intCnt = 0 To dgEmpFamList.Items.Count - 1
                lbButton = dgEmpFamList.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = True
                lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Next
        End If

        PageNo = dgEmpFamList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & Pageno & " of " & dgEmpFamList.PageCount
    End Sub 

    Sub BindPageList() 
        Dim count as integer = 1   
        Dim arrDList As New ArrayList()

        While not count = dgEmpFamList.PageCount + 1
            arrDList.Add("Page " & count)
            Count = Count + 1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgEmpFamList.CurrentPageIndex
    End Sub 

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                dgEmpFamList.CurrentPageIndex = 0
            Case "prev"
                dgEmpFamList.CurrentPageIndex = _
                Math.Max(0, dgEmpFamList.CurrentPageIndex - 1)
            Case "next"
                dgEmpFamList.CurrentPageIndex = _
                Math.Min(dgEmpFamList.PageCount - 1, dgEmpFamList.CurrentPageIndex + 1)
            Case "last"
                dgEmpFamList.CurrentPageIndex = dgEmpFamList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgEmpFamList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            dgEmpFamList.CurrentPageIndex = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        dgEmpFamList.CurrentPageIndex=e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgEmpFamList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub
 
    Sub DEDR_FamDet(Sender As Object, E As DataGridCommandEventArgs)
        Dim lblCodeValue As Label
        Dim lblNameValue As Label
        Dim lblFamMemberIDValue As Label

        Dim intIndex As Integer = E.Item.ItemIndex    
        Dim strEmpCode As String
        Dim strEmpName As String
        Dim strFamMemberID As String
        Dim strEmpStatus As String
        Dim strEmpMarital As String
        Dim strLink As String

        lblCodeValue = dgEmpFamList.Items.Item(intIndex).FindControl("lblEmpCode")
        lblNameValue = dgEmpFamList.Items.Item(intIndex).FindControl("lblEmpName")
        lblFamMemberIDValue = dgEmpFamList.Items.Item(intIndex).FindControl("lblFamMemberID")
  
        strEmpCode = lblCodeValue.Text
        strEmpName = lblNameValue.Text
        strFamMemberID = lblFamMemberIDValue.Text
        strEmpStatus = lblEmpStatus.Text
        strEmpMarital = lblEmpMarital.Text
        strEmpGender = lblEmpGender.Text
        
        strLink = "HR_trx_EmployeeFamDet.aspx?redirect=empfam&EmpCode=" & strEmpCode & _
                  "&EmpName=" & strEmpName & _
                  "&FamMemberID=" & strFamMemberID & _
                  "&EmpStatus=" & strEmpStatus & _
                  "&EmpMarital=" & strEmpMarital & _
                  "&EmpGender=" & strEmpGender                    
        Response.Redirect(strLink)
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim strOpCd_DelFam As String = "HR_CLSTRX_FAMILY_DEL"
        Dim intIndex As Integer = E.Item.ItemIndex    
        Dim lblFamMemberIDValue As Label
        Dim strFamMemberID As String
        Dim intErrNo As Integer

        lblFamMemberIDValue = dgEmpFamList.Items.Item(intIndex).FindControl("lblFamMemberID")
        strFamMemberID = lblFamMemberIDValue.Text

        Try
            intErrNo = objHR.mtdDelEmployeeFam(strOpCd_DelFam, _
                                               strFamMemberID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPFAMLIST_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeFamList.aspx")
        End Try

        dgEmpFamList.EditItemIndex = -1
        BindGrid()
    End Sub
    
    Sub NewEmpFamBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeFamDet.aspx?redirect=" & lblRedirect.Text & "&new=yes&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.text & "&EmpMarital=" & lblEmpMarital.text & "&EmpGender=" & lblEmpGender.Text)
    End Sub

    Sub btnBack_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeList.aspx?redirect=" & lblRedirect.Text)
    End Sub


End Class
