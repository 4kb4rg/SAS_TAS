Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic
Imports agri.HR
Imports agri.PWSystem
Imports agri.GlobalHdl

Public Class HR_EmployeeQlfDet : Inherits Page

    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblEmpName As Label
    Protected WithEvents lblQualificationID As Label
    Protected WithEvents ddlQualificationCode As DropDownList
    Protected WithEvents ddlSubjectCode As DropDownList
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblEmpStatus As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrQualification As Label
    Protected WithEvents lblErrSubject As Label
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbDetails As LinkButton
    Protected WithEvents lbPayroll As LinkButton
    Protected WithEvents lbEmployment As LinkButton
    Protected WithEvents lbStatutory As LinkButton
    Protected WithEvents lbFamily As LinkButton
    Protected WithEvents lbSkill As LinkButton
    Protected WithEvents lbCareerProg As LinkButton

    Dim objHR As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objEmpQlfDs As New Object()
    Dim objQualificationDs As New Object()
    Dim objSubjectDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long

    Dim strSelectedEmpCode As String = ""
    Dim strSelectedEmpName As String = ""
    Dim strSelectedEmpStatus As String = ""
    Dim strSelectedQualificationID As String = ""
    Dim strSelectedQualificationCode As String = ""
    Dim strSelectedSubjectCode As String = ""
    Dim strSortExpression As String = "QualificationID"
    Dim strAcceptFormat As String
    Dim strIsNew As String
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeQualification), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrQualification.Visible = False
            lblErrSubject.Visible = False
            lblRedirect.Text = Request.QueryString("redirect")
            strIsNew = Trim(Request.QueryString("new"))

            strSelectedEmpCode = Trim(IIf(Request.QueryString("EmpCode") <> "", Request.QueryString("EmpCode"), Request.Form("EmpCode")))
            strSelectedEmpName = Trim(IIf(Request.QueryString("EmpName") <> "", Request.QueryString("EmpName"), Request.Form("EmpName")))
            strSelectedQualificationID = Trim(IIf(Request.QueryString("QualificationID") <> "", Request.QueryString("QualificationID"), Request.Form("QualificationID")))
            strSelectedEmpStatus = Trim(IIf(Request.QueryString("EmpStatus") <> "", Request.QueryString("EmpStatus"), Request.Form("EmpStatus")))

            If Not IsPostBack Then
                If strSelectedQualificationID <> "" Then
                    BindEmpQlf(strSelectedEmpCode)
                Else
                    lblEmpCode.Text = strSelectedEmpCode
                    lblEmpName.Text = strSelectedEmpName
                    lblQualificationID.Text = strSelectedQualificationID
                    lblEmpStatus.Text = strSelectedEmpStatus
                End If
                BindQualification()
                BindSubject()
                BindButton()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPQLFDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/hr_trx_employeelist.aspx")
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

    Sub BindButton()
        ddlQualificationCode.Enabled = False
        ddlSubjectCode.Enabled = False
        txtRemark.Enabled = False
        btnSave.visible = False
        btnDelete.visible = False
        btnBack.visible = False
        
        Select Case CInt(lblEmpStatus.text)
            Case objHR.EnumEmpStatus.Active, objHR.EnumEmpStatus.Pending
                ddlQualificationCode.Enabled = True
                ddlSubjectCode.Enabled = True
                txtRemark.Enabled = True
                btnSave.visible = True
                btnDelete.visible = True
                btnBack.visible = True
            Case objHR.EnumEmpStatus.Deleted
                btnBack.visible = True
        End Select

        If strIsNew = "yes" Then
            btnDelete.visible = False
        End If
    End Sub

    Sub BindEmpQlf(ByVal pv_strEmpCode As String)
        Dim strOpCd As String = "HR_CLSTRX_QUALIFICATION_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        lblEmpCode.Text = strSelectedEmpCode
        lblEmpName.Text = strSelectedEmpName
        lblQualificationID.Text = strSelectedQualificationID
        lblEmpStatus.Text = strSelectedEmpStatus

        strParam = strSelectedQualificationID & "|" & strSelectedEmpCode & "|||" & strSortExpression & "|"

        Try
            intErrNo = objHR.mtdGetEmployeeQlf(strOpCd, strParam, objEmpQlfDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_QUALIFICATION&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeQlfList.aspx")
        End Try

        If objEmpQlfDs.Tables(0).Rows.Count > 0 Then
            objEmpQlfDs.Tables(0).Rows(0).Item("QualificationID") = Trim(objEmpQlfDs.Tables(0).Rows(0).Item("QualificationID"))
            objEmpQlfDs.Tables(0).Rows(0).Item("EmpCode") = Trim(objEmpQlfDs.Tables(0).Rows(0).Item("EmpCode"))
            objEmpQlfDs.Tables(0).Rows(0).Item("EmpName") = Trim(objEmpQlfDs.Tables(0).Rows(0).Item("EmpName"))
            objEmpQlfDs.Tables(0).Rows(0).Item("QualificationCode") = Trim(objEmpQlfDs.Tables(0).Rows(0).Item("QualificationCode"))
            objEmpQlfDs.Tables(0).Rows(0).Item("SubjectCode") = Trim(objEmpQlfDs.Tables(0).Rows(0).Item("SubjectCode"))
            objEmpQlfDs.Tables(0).Rows(0).Item("Remark") = Trim(objEmpQlfDs.Tables(0).Rows(0).Item("Remark"))
            objEmpQlfDs.Tables(0).Rows(0).Item("Status") = Trim(objEmpQlfDs.Tables(0).Rows(0).Item("Status"))
            strSelectedQualificationCode = objEmpQlfDs.Tables(0).Rows(0).Item("QualificationCode")
            strSelectedSubjectCode = objEmpQlfDs.Tables(0).Rows(0).Item("SubjectCode")
            txtRemark.Text = objEmpQlfDs.Tables(0).Rows(0).Item("Remark")
        End If
    End Sub

    Sub BindQualification()
        Dim strOpCd As String = "HR_CLSSETUP_QUALIFICATION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intQualificationIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|AND QUA.Status LIKE '" & objHRSetup.EnumQualificationStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd, strParam, objHRSetup.EnumHRMasterType.Qualification, objQualificationDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_QUALIFICATION&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeQlfList.aspx")
        End Try

        If objQualificationDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objQualificationDs.Tables(0).Rows.Count - 1
                objQualificationDs.Tables(0).Rows(intCnt).Item("QualificationCode") = Trim(objQualificationDs.Tables(0).Rows(intCnt).Item("QualificationCode"))
                objQualificationDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objQualificationDs.Tables(0).Rows(intCnt).Item("QualificationCode")) & " (" & _
                                                                                Trim(objQualificationDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objQualificationDs.Tables(0).Rows(intCnt).Item("QualificationCode") = strSelectedQualificationCode
                    intQualificationIndex = intCnt + 1
                End If
            Next
        End If

        dr = objQualificationDs.Tables(0).NewRow()
        dr("QualificationCode") = ""
        dr("Description") = "Please Select Qualification Code"
        objQualificationDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlQualificationCode.DataSource = objQualificationDs.Tables(0)
        ddlQualificationCode.DataTextField = "Description"
        ddlQualificationCode.DataValueField = "QualificationCode"
        ddlQualificationCode.DataBind()
        ddlQualificationCode.SelectedIndex = intQualificationIndex
    End Sub

    Sub BindSubject()
        Dim strOpCd As String = "HR_CLSSETUP_SUBJECT_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSubjectIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|AND SU.Status LIKE '" & objHRSetup.EnumSubjectStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd, strParam, objHRSetup.EnumHRMasterType.Subject, objSubjectDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_SUBJECT&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeQlfList.aspx")
        End Try

        If objSubjectDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objSubjectDs.Tables(0).Rows.Count - 1
                objSubjectDs.Tables(0).Rows(intCnt).Item("SubjectCode") = Trim(objSubjectDs.Tables(0).Rows(intCnt).Item("SubjectCode"))
                objSubjectDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objSubjectDs.Tables(0).Rows(intCnt).Item("SubjectCode")) & " (" & _
                                                                          Trim(objSubjectDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objSubjectDs.Tables(0).Rows(intCnt).Item("SubjectCode") = strSelectedSubjectCode
                    intSubjectIndex = intCnt + 1
                End If
            Next
        End If

        dr = objSubjectDs.Tables(0).NewRow()
        dr("SubjectCode") = ""
        dr("Description") = "Please Select Subject Code"
        objSubjectDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSubjectCode.DataSource = objSubjectDs.Tables(0)
        ddlSubjectCode.DataTextField = "Description"
        ddlSubjectCode.DataValueField = "SubjectCode"
        ddlSubjectCode.DataBind()
        ddlSubjectCode.SelectedIndex = intSubjectIndex
    End Sub

    Sub btnSave_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_UpdQlf As String = "HR_CLSTRX_QUALIFICATION_UPD"
        Dim strOpCd_AddQlf As String = "HR_CLSTRX_QUALIFICATION_ADD"
        Dim objQualificationID As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strEmpCode As String = lblEmpCode.Text
        Dim strQualificationCode As String = ddlQualificationCode.SelectedItem.Value
        Dim strSubjectCode As String = ddlSubjectCode.SelectedItem.Value
        Dim strRemark As String = txtRemark.Text

        If strQualificationCode = "" Then
            lblErrQualification.Visible = True
            Exit Sub
        ElseIf strSubjectCode = "" Then
            lblErrSubject.Visible = True
            Exit Sub
        End If

        strParam = strSelectedQualificationID & "|" & strEmpCode & "|" & strQualificationCode & "|" & _
                   strSubjectCode & "|" & strRemark
        Try
            intErrNo = objHR.mtdUpdEmployeeQlf(strOpCd_UpdQlf, _
                                               strOpCd_AddQlf, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               strParam, _
                                               objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.HREmployeeQualification), _
                                               objQualificationID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SAVE_QUALIFICATION&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeQlfList.aspx")
        End Try

        lblQualificationID.Text = objQualificationID
        btnBack_Click(Sender, E)
    End Sub

    Sub btnDelete_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_DelQlf As String = "HR_CLSTRX_QUALIFICATION_DEL"
        Dim intErrNo As Integer

        Try
            intErrNo = objHR.mtdDelEmployeeQlf(strOpCd_DelQlf, _
                                               strSelectedQualificationID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_DELETE_QUALIFICATION&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeQlfList.aspx")
        End Try

        Response.Redirect("HR_trx_EmployeeQlfList.aspx?redirect=" & lblRedirect.Text & "&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.text)
    End Sub

    Sub btnBack_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeQlfList.aspx?redirect=" & lblRedirect.Text & "&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&QualificationID=" & lblQualificationID.Text & "&EmpStatus=" & lblEmpStatus.text)
    End Sub


End Class
