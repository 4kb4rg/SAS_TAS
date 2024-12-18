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


Public Class HR_EmployeeFamDet : Inherits Page

    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblEmpName As Label
    Protected WithEvents lblEmpStatus As Label
    Protected WithEvents lblEmpMarital As Label
    Protected WithEvents lblFamMemberID As Label
    Protected WithEvents txtMemberName As TextBox
    Protected WithEvents ddlFamEmpCode As DropDownList
    Protected WithEvents btnFindEmp As HtmlInputButton
    Protected WithEvents ddlGender As DropDownList
    Protected WithEvents ddlRelationship As DropDownList
    Protected WithEvents txtDOB As TextBox
    Protected WithEvents btnSelDOB As Image
    Protected WithEvents txtTelNo As TextBox
    Protected WithEvents cbEmergContactInd As CheckBox
    Protected WithEvents cbWorkCompInd As CheckBox
    Protected WithEvents cbCeaseRiceInd As CheckBox
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents lblRedirect As Label

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrGender As Label
    Protected WithEvents lblErrRelationship As Label
    Protected WithEvents lblErrDOB As Label
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbDetails As LinkButton    
    Protected WithEvents lbPayroll As LinkButton
    Protected WithEvents lbEmployment As LinkButton
    Protected WithEvents lbStatutory As LinkButton
    Protected WithEvents lbQualific As LinkButton
    Protected WithEvents lbSkill As LinkButton
    Protected WithEvents lbCareerProg As LinkButton
    Protected WithEvents lblEmpGender As Label
    Dim strEmpGender As String = ""

    Dim objHR As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objEmpFamDs As New Object()
    Dim objFamEmpCodeDs As New Object()
    Dim objSexDs As New Object()
    Dim objRelationshipDs As New Object()
    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long
    Dim strDateFmt As String
    Dim strIsNew As String
    
    Dim strEmpCode As String = ""
    Dim strEmpName As String = ""
    Dim strEmpStatus As String = ""
    Dim strEmpMarital As String = ""
    Dim strMemberID As String = ""
    Dim strLocType as String
    
    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strDateFmt = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeFamily), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrGender.Visible = False 
            lblErrRelationship.Visible = False
            lblRedirect.Text = Request.QueryString("redirect")
            strEmpCode = Trim(Request.QueryString("EmpCode"))
            strEmpName = Trim(Request.QueryString("EmpName"))
            strEmpStatus = Trim(Request.QueryString("EmpStatus"))
            strEmpMarital = Trim(Request.QueryString("EmpMarital"))
            strMemberID = Trim(Request.QueryString("FamMemberID"))
            strEmpGender = Trim(Request.QueryString("EmpGender"))             
            

            If Not IsPostBack Then
                If strMemberID <> "" Then
                    onLoad_Display()
                    onLoad_BindButton()
                Else
                    lblEmpCode.Text = strEmpCode
                    lblEmpName.Text = strEmpName
                    lblEmpStatus.text = strEmpStatus
                    lblEmpMarital.text = strEmpMarital
                    lblFamMemberID.Text = strMemberID
                    lblEmpGender.text = strEmpGender                    
                    BindGender("0")
                    BindRelationship("0")
                    BindFamEmpCode("")
                    BindCeaseForRice(strEmpGender)
                    onLoad_BindButton()
                End If                
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPFAMDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/hr_trx_employeelist.aspx")
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
        txtMemberName.Enabled = False
        ddlFamEmpCode.Enabled = False
        btnFindEmp.Visible = False
        ddlGender.Enabled = False
        ddlRelationship.Enabled = False
        txtDOB.Enabled = False
        btnSelDOB.visible = False
        txtTelNo.Enabled = False
        cbEmergContactInd.Enabled = False
        cbWorkCompInd.Enabled = False
        cbCeaseRiceInd.Enabled = False
        txtRemark.Enabled = False
        btnSave.visible = False
        btnDelete.visible = False
        btnBack.visible = False

        Select Case CInt(lblEmpStatus.text) 
            Case objHR.EnumEmpStatus.Active, objHR.EnumEmpStatus.Pending
                txtMemberName.Enabled = True
                ddlFamEmpCode.Enabled = True
                btnFindEmp.Visible = True
                ddlGender.Enabled = True
                ddlRelationship.Enabled = True
                txtDOB.Enabled = True
                btnSelDOB.visible = True
                txtTelNo.Enabled = True
                cbEmergContactInd.Enabled = True
                cbWorkCompInd.Enabled = True
                cbCeaseRiceInd.Enabled = True
                txtRemark.Enabled = True
                btnSave.visible = True
                If lblFamMemberID.text <> "" Then
                    btnDelete.visible = True
                End If
                btnBack.visible = True
            Case objHR.EnumEmpStatus.Deleted
                btnBack.visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSTRX_FAMILY_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        lblEmpCode.Text = strEmpCode
        lblEmpName.Text = strEmpName
        lblEmpStatus.Text = strEmpStatus
        lblEmpMarital.Text = strEmpMarital
        lblFamMemberID.Text = strMemberID
        lblEmpGender.Text = strEmpGender

        strParam = strMemberID & "|" & strEmpCode & "|||" & "FamMemberID" & "|"

        Try
            intErrNo = objHR.mtdGetEmployeeFam(strOpCd, strParam, objEmpFamDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPFAMDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeFamList.aspx")
        End Try

        If objEmpFamDs.Tables(0).Rows.Count > 0 Then
            txtMemberName.Text = objEmpFamDs.Tables(0).Rows(0).Item("FamMemberName")
            txtDOB.Text = objGlobal.GetShortDate(strDateFmt, objEmpFamDs.Tables(0).Rows(0).Item("DOB"))
            txtTelNo.Text = objEmpFamDs.Tables(0).Rows(0).Item("TelNo")
            txtRemark.Text = objEmpFamDs.Tables(0).Rows(0).Item("Remark")
            
            BindGender(objEmpFamDs.Tables(0).Rows(0).Item("Gender"))
            BindRelationship(objEmpFamDs.Tables(0).Rows(0).Item("Relationship"))
            BindFamEmpCode(objEmpFamDs.Tables(0).Rows(0).Item("FamEmpCode").Trim())
            
            If objEmpFamDs.Tables(0).Rows(0).Item("EmergContactInd") = objHR.EnumEmergContactInd.Yes Then
                cbEmergContactInd.Checked = True
            Else
                cbEmergContactInd.Checked = False
            End If
            If objEmpFamDs.Tables(0).Rows(0).Item("WorkCompInd") = objHR.EnumWorkCompInd.Yes Then
                cbWorkCompInd.Checked = True
            Else
                cbWorkCompInd.Checked = False
            End If
            If objEmpFamDs.Tables(0).Rows(0).Item("CeaseRiceInd") = objHR.EnumCeaseRiceInd.Yes Then
                cbCeaseRiceInd.Checked = True
            Else
                cbCeaseRiceInd.Checked = False
            End If              
        Else
            BindGender("0")
            BindRelationship("0")
            BindFamEmpCode("")
            BindCeaseForRice(Cstr(lblEmpGender.Text))
        End If
    End Sub

    Sub BindFamEmpCode(ByVal pv_strFamEmpCode As String)
        Dim strOpCd_Get As String = "HR_CLSTRX_EMPLOYEE_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow 
        strParam = lblEmpCode.Text & "|" & objHR.EnumEmpStatus.Active
        
        Try
            intErrNo = objHR.mtdGetOthEmployee(strOpCd_Get, strParam, strLocation, objFamEmpCodeDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPFAMDET_GET_FAMEMPCODE&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeFamList.aspx")
        End Try

        If objFamEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objFamEmpCodeDs.Tables(0).Rows.Count - 1
                If Trim(objFamEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode")) = trim(pv_strFamEmpCode)
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If
        
        dr = objFamEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "Select Member Employee Code"
        objFamEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlFamEmpCode.DataSource = objFamEmpCodeDs.Tables(0)
        ddlFamEmpCode.DataTextField = "_Description"
        ddlFamEmpCode.DataValueField = "EmpCode"
        ddlFamEmpCode.DataBind()
        ddlFamEmpCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindGender(ByVal pv_strGender As String)
        ddlGender.Items.Clear()
        ddlGender.Items.Add(New ListItem("Please Select Gender", "0"))
        ddlGender.Items.Add(New ListItem(objHR.mtdGetSex(objHR.EnumSex.Male), objHR.EnumSex.Male))
        ddlGender.Items.Add(New ListItem(objHR.mtdGetSex(objHR.EnumSex.Female), objHR.EnumSex.Female))
        ddlGender.SelectedIndex = CInt(pv_strGender)
    End Sub
    
    Sub BindRelationship(ByVal pv_strRelationship As String)
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        If Trim(lblEmpMarital.Text) = "" Then
            lblEmpMarital.Text = "0"
        End If
        ddlRelationship.Items.Clear()
        ddlRelationship.Items.Add(New ListItem("Please Select Relationship", "0"))
        ddlRelationship.Items.Add(New ListItem(objHR.mtdGetRelationship(objHR.EnumRelationship.Spouse), objHR.EnumRelationship.Spouse))
        ddlRelationship.Items.Add(New ListItem(objHR.mtdGetRelationship(objHR.EnumRelationship.Child), objHR.EnumRelationship.Child))
        ddlRelationship.Items.Add(New ListItem(objHR.mtdGetRelationship(objHR.EnumRelationship.Parent), objHR.EnumRelationship.Parent))
        If Cint(lblEmpMarital.Text) = objHR.EnumMarital.Married Then
            ddlRelationship.Items.Add(New ListItem(objHR.mtdGetRelationship(objHR.EnumRelationship.ParentInLaw), objHR.EnumRelationship.ParentInLaw))
        End If
        ddlRelationship.Items.Add(New ListItem(objHR.mtdGetRelationship(objHR.EnumRelationship.GrandParent), objHR.EnumRelationship.GrandParent))
        ddlRelationship.Items.Add(New ListItem(objHR.mtdGetRelationship(objHR.EnumRelationship.Sibling), objHR.EnumRelationship.Sibling))
        ddlRelationship.Items.Add(New ListItem(objHR.mtdGetRelationship(objHR.EnumRelationship.Others), objHR.EnumRelationship.Others))
        
        For intCnt=0 To ddlRelationship.Items.Count - 1
            If ddlRelationship.Items(intCnt).Value = CInt(pv_strRelationship) Then
                intSelectedIndex = intCnt
                Exit For
            End If
        Next
        ddlRelationship.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindCeaseForRice(ByVal pv_strGender As String)        
        If pv_strGender = "2" Then
            cbCeaseRiceInd.Checked = True
        Else
            cbCeaseRiceInd.Checked = False
        End if
    End Sub

    Sub btnSave_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_UpdFam As String = "HR_CLSTRX_FAMILY_UPD"
        Dim strOpCd_AddFam As String = "HR_CLSTRX_FAMILY_ADD"
        Dim objFamMemberID As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strEmpCode As String = lblEmpCode.Text
        Dim strFamEmpName As String = txtMemberName.Text
        Dim strFamEmpCode As String = Request.Form("ddlFamEmpCode")
        Dim strGender As String = ddlGender.SelectedItem.Value
        Dim strRelationship As String = Trim(ddlRelationship.SelectedItem.Value)
        Dim strDOB As String = txtDOB.Text
        Dim strTelNo As String = txtTelNo.Text
        Dim strEmergContactInd As String = IIf(cbEmergContactInd.Checked = True, objHR.EnumEmergContactInd.Yes, objHR.EnumEmergContactInd.No)
        Dim strWorkCompInd As String = IIf(cbWorkCompInd.Checked = True, objHR.EnumWorkCompInd.Yes, objHR.EnumWorkCompInd.No)
        Dim strCeaseRiceInd As String = IIf(cbCeaseRiceInd.Checked = True, objHR.EnumCeaseRiceInd.Yes, objHR.EnumCeaseRiceInd.No)
        Dim strRemark As String = txtRemark.Text
        Dim objFormatDate As String
        Dim objActualDate As String

        If strGender = 0 Then
            lblErrGender.Visible = True
            Exit Sub
        End If

        If strRelationship = 0 Then
            lblErrRelationship.Visible = True
            Exit Sub
        End If

        If strDOB <> "" Then
            If objGlobal.mtdValidInputDate(strDateFmt, _
                                           strDOB, _
                                           objFormatDate, _
                                           objActualDate) = False Then
                lblErrDOB.Text = lblErrDOB.Text & objFormatDate
                lblErrDOB.Visible = True
                Exit Sub
            Else
                strDOB = objActualDate
            End If
        End If

        strParam = strMemberID & "|" & strEmpCode & "|" & strFamEmpName & "|" & strFamEmpCode & "|" & _
                   strGender & "|" & strRelationship & "|" & strDOB & "|" & strTelNo & "|" & _
                   strEmergContactInd & "|" & strWorkCompInd & "|" & strCeaseRiceInd & "|" & strRemark

        Try
            intErrNo = objHR.mtdUpdEmployeeFam(strOpCd_UpdFam, _
                                               strOpCd_AddFam, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               strParam, _
                                               objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.HREmployeeFamily), _
                                               objFamMemberID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPFAMDET_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeFamList.aspx")
        End Try

        strIsNew = ""
        lblFamMemberID.Text = objFamMemberID
        btnBack_Click(Sender, E)
    End Sub

    Sub btnDelete_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_DelFam As String = "HR_CLSTRX_FAMILY_DEL"
        Dim intErrNo As Integer

        Try
            intErrNo = objHR.mtdDelEmployeeFam(strOpCd_DelFam, _
                                               lblFamMemberID.text)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPFAMDET_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeFamList.aspx")
        End Try
        Response.Redirect("HR_trx_EmployeeFamList.aspx?redirect=" & lblRedirect.Text & "&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text & "&EmpMarital=" & lblEmpMarital.Text & "&EmpGender=" & lblEmpGender.Text)
    End Sub

    Sub btnBack_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeFamList.aspx?redirect=" & lblRedirect.Text & "&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&FamMemberID=" & lblFamMemberID.Text & "&EmpStatus=" & lblEmpStatus.Text & "&EmpMarital=" & lblEmpMarital.Text & "&EmpGender=" & lblEmpGender.Text)
    End Sub



End Class
