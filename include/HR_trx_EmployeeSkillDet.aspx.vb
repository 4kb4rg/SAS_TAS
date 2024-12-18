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

Public Class HR_EmployeeSkillDet : Inherits Page

    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblEmpName As Label
    Protected WithEvents lblSkillID As Label
    Protected WithEvents ddlSkillCode As DropDownList
    Protected WithEvents ddlLevel As DropDownList
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblEmpStatus As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrSkill As Label
    Protected WithEvents lblErrLevel As Label
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbDetails As LinkButton
    Protected WithEvents lbPayroll As LinkButton
    Protected WithEvents lbEmployment As LinkButton
    Protected WithEvents lbStatutory As LinkButton
    Protected WithEvents lbFamily As LinkButton
    Protected WithEvents lbQualific As LinkButton
    Protected WithEvents lbCareerProg As LinkButton

    Dim objHR As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objEmpSkillDs As New Object()
    Dim objSkillDs As New Object()
    Dim objLangCapDs As New Object()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
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
    Dim strSelectedSkillID As String = ""
    Dim strSelectedSkillCode As String = ""
    Dim strSelectedLevelCode As String = ""
    Dim strSortExpression As String = "SkillID"
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

            lblErrSkill.Visible = False
            lblErrLevel.Visible = False
            lblRedirect.Text = Request.QueryString("redirect")
            strIsNew = Trim(Request.QueryString("new"))

            strSelectedEmpCode = Trim(IIf(Request.QueryString("EmpCode") <> "", Request.QueryString("EmpCode"), Request.Form("EmpCode")))
            strSelectedEmpName = Trim(IIf(Request.QueryString("EmpName") <> "", Request.QueryString("EmpName"), Request.Form("EmpName")))
            strSelectedSkillID = Trim(IIf(Request.QueryString("SkillID") <> "", Request.QueryString("SkillID"), Request.Form("SkillID")))
            strSelectedEmpStatus = Trim(IIf(Request.QueryString("EmpStatus") <> "", Request.QueryString("EmpStatus"), Request.Form("EmpStatus")))

            If Not IsPostBack Then
                If strSelectedSkillID <> "" Then
                    BindEmpSkill(strSelectedEmpCode)
                Else
                    lblEmpCode.Text = strSelectedEmpCode
                    lblEmpName.Text = strSelectedEmpName
                    lblSkillID.Text = strSelectedSkillID
                    lblEmpStatus.Text = strSelectedEmpStatus
                    BindLevel("0")
                End If
                BindSkill()
                BindButton()
                onload_LinkButton()     
            End If
        End If
    End Sub

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

    Private Sub lbQualific_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbQualific.Click
        Response.Redirect("HR_trx_EmployeeQlfList.aspx?redirect=empqlf&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbCareerProg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCareerProg.Click
        Response.Redirect("HR_trx_EmployeeCPList.aspx?redirect=empcp&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Sub BindButton()
        ddlSkillCode.Enabled = False
        ddlLevel.Enabled = False
        txtRemark.Enabled = False
        btnSave.visible = False
        btnDelete.visible = False
        btnBack.visible = False

        Select Case CInt(lblEmpStatus.text)
            Case objHR.EnumEmpStatus.Active, objHR.EnumEmpStatus.Pending
                ddlSkillCode.Enabled = True
                ddlLevel.Enabled = True
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

    Sub BindEmpSkill(ByVal pv_strEmpCode As String)
        Dim strOpCd As String = "HR_CLSTRX_SKILL_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        lblEmpCode.Text = strSelectedEmpCode
        lblEmpName.Text = strSelectedEmpName
        lblSkillID.Text = strSelectedSkillID
        lblEmpStatus.Text = strSelectedEmpStatus

        strParam = strSelectedSkillID & "|" & strSelectedEmpCode & "|||" & strSortExpression & "|"

        Try
            intErrNo = objHR.mtdGetEmployeeSkill(strOpCd, strParam, objEmpSkillDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_SKILL&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeSkillList.aspx")
        End Try

        If objEmpSkillDs.Tables(0).Rows.Count > 0 Then
            objEmpSkillDs.Tables(0).Rows(0).Item("SkillID") = Trim(objEmpSkillDs.Tables(0).Rows(0).Item("SkillID"))
            objEmpSkillDs.Tables(0).Rows(0).Item("EmpCode") = Trim(objEmpSkillDs.Tables(0).Rows(0).Item("EmpCode"))
            objEmpSkillDs.Tables(0).Rows(0).Item("EmpName") = Trim(objEmpSkillDs.Tables(0).Rows(0).Item("EmpName"))
            objEmpSkillDs.Tables(0).Rows(0).Item("SkillCode") = Trim(objEmpSkillDs.Tables(0).Rows(0).Item("SkillCode"))
            objEmpSkillDs.Tables(0).Rows(0).Item("Level") = Trim(objEmpSkillDs.Tables(0).Rows(0).Item("Level"))
            objEmpSkillDs.Tables(0).Rows(0).Item("Remark") = Trim(objEmpSkillDs.Tables(0).Rows(0).Item("Remark"))
            objEmpSkillDs.Tables(0).Rows(0).Item("Status") = Trim(objEmpSkillDs.Tables(0).Rows(0).Item("Status"))
            lblEmpStatus.Text = objEmpSkillDs.Tables(0).Rows(0).Item("Status")
            strSelectedSkillCode = objEmpSkillDs.Tables(0).Rows(0).Item("SkillCode")
            BindLevel(objEmpSkillDs.Tables(0).Rows(0).Item("Level"))
            txtRemark.Text = objEmpSkillDs.Tables(0).Rows(0).Item("Remark")
        End If
    End Sub



    Sub BindSkill()
        Dim strOpCd As String = "HR_CLSSETUP_SKILL_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSkillIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|AND SK.Status LIKE '" & objHRSetup.EnumSkillStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd, strParam, objHRSetup.EnumHRMasterType.Skill, objSkillDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_SKILL&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeSkillList.aspx")
        End Try

        If objSkillDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objSkillDs.Tables(0).Rows.Count - 1
                objSkillDs.Tables(0).Rows(intCnt).Item("SkillCode") = Trim(objSkillDs.Tables(0).Rows(intCnt).Item("SkillCode"))
                objSkillDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objSkillDs.Tables(0).Rows(intCnt).Item("SkillCode")) & " (" & _
                                                                        Trim(objSkillDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objSkillDs.Tables(0).Rows(intCnt).Item("SkillCode") = strSelectedSkillCode
                    intSkillIndex = intCnt + 1
                End If
            Next
        End If

        dr = objSkillDs.Tables(0).NewRow()
        dr("SkillCode") = ""
        dr("Description") = "Please Select Skill Code"
        objSkillDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSkillCode.DataSource = objSkillDs.Tables(0)
        ddlSkillCode.DataTextField = "Description"
        ddlSkillCode.DataValueField = "SkillCode"
        ddlSkillCode.DataBind()
        ddlSkillCode.SelectedIndex = intSkillIndex
    End Sub

    Sub BindLevel(ByVal pv_strLevel As String)
        ddlLevel.Items.Clear()
        ddlLevel.Items.Add(New ListItem("Please Select Level Code", ""))
        ddlLevel.Items.Add(New ListItem(objHR.mtdGetSkillLevel(objHR.EnumSkillLevel.Good), objHR.EnumSkillLevel.Good))
        ddlLevel.Items.Add(New ListItem(objHR.mtdGetSkillLevel(objHR.EnumSkillLevel.Fair), objHR.EnumSkillLevel.Fair))
        ddlLevel.Items.Add(New ListItem(objHR.mtdGetSkillLevel(objHR.EnumSkillLevel.Poor), objHR.EnumSkillLevel.Poor))
        ddlLevel.SelectedIndex = CInt(pv_strLevel)
    End Sub

    Sub btnSave_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_UpdSkill As String = "HR_CLSTRX_SKILL_UPD"
        Dim strOpCd_AddSkill As String = "HR_CLSTRX_SKILL_ADD"
        Dim objSkillID As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strEmpCode As String = lblEmpCode.Text
        Dim strSkillCode As String = ddlSkillCode.SelectedItem.Value
        Dim strLevel As String = ddlLevel.SelectedItem.Value
        Dim strRemark As String = txtRemark.Text

        If strSkillCode = "" Then
            lblErrSkill.Visible = True
            Exit Sub
        ElseIf strLevel = "" Then
            lblErrLevel.Visible = True
            Exit Sub
        End If

        strParam = strSelectedSkillID & "|" & strEmpCode & "|" & strSkillCode & "|" & _
                   strLevel & "|" & strRemark

        Try
            intErrNo = objHR.mtdUpdEmployeeSkill(strOpCd_UpdSkill, _
                                                 strOpCd_AddSkill, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strParam, _
                                                 objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.HREmployeeSkill), _
                                                 objSkillID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SAVE_SKILL&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeSkillList.aspx")
        End Try

        lblSkillID.Text = objSkillID
        btnBack_Click(Sender, E)
    End Sub

    Sub btnDelete_Click(Sender As Object, E As ImageClickEventArgs)        
        Dim strOpCd_DelSkill As String = "HR_CLSTRX_SKILL_DEL"
        Dim intErrNo As Integer

        Try
            intErrNo = objHR.mtdDelEmployeeSkill(strOpCd_DelSkill, _
                                                 strSelectedSkillID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_DELETE_SKILL&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeSkillList.aspx")
        End Try

        Response.Redirect("HR_trx_EmployeeSkillList.aspx?redirect=" & lblRedirect.Text & "&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.text)
    End Sub

    Sub btnBack_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeSkillList.aspx?redirect=" & lblRedirect.Text & "&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.text)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeSkillList.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function


End Class
