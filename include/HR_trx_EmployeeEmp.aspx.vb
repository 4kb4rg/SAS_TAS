
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
Imports agri.Admin
Imports agri.PWSystem
Imports agri.GlobalHdl

Public Class HR_EmployeeEmp : Inherits Page

    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblEmpName As Label
    Protected WithEvents lblAppJoinDate As Label
    Protected WithEvents txtAppJoinGrpDate As TextBox
    Protected WithEvents ddlAppCPCode As DropDownList
    Protected WithEvents lblLastCPDateFrom As Label
    Protected WithEvents lblLastCPDateTo As Label
    Protected WithEvents ddlLastCPCode As DropDownList
    Protected WithEvents lblTerminateDate As Label
    Protected WithEvents ddlTerminateCPCode As DropDownList
    Protected WithEvents lblLastIncDate As Label
    Protected WithEvents lblNextIncDate As Label
    Protected WithEvents ddlCompCode As DropDownList
    Protected WithEvents ddlLocCode As DropDownList
    Protected WithEvents ddlDeptCode As DropDownList
    Protected WithEvents ddlLevelCode As DropDownList
    Protected WithEvents ddlRptTo As DropDownList
    Protected WithEvents ddlPosCode As DropDownList
    Protected WithEvents ddlShift As DropDownList
    Protected WithEvents cbMechInd As CheckBox
    Protected WithEvents cbGangLeader As CheckBox
    Protected WithEvents ddlSalSchemeCode As DropDownList
    Protected WithEvents ddlSalGradeCode As DropDownList
    Protected WithEvents lblBlackListDate As Label
    Protected WithEvents lblProbation As Label
    Protected WithEvents lblConfirmDate As Label
    Protected WithEvents ddlHolSchedule As DropDownList
    Protected WithEvents txtAnnualLeave As TextBox
    Protected WithEvents txtSickLeave As TextBox
    Protected WithEvents txtBFLeave As TextBox
    Protected WithEvents txtOffDay As TextBox
    Protected WithEvents lblEmpStatus As Label
    Protected WithEvents lblCP1 As Label
    Protected WithEvents lblCP2 As Label
    Protected WithEvents lblCP3 As Label
    Protected WithEvents lblCP4 As Label
    Protected WithEvents lblCompany As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblLevel As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrAppJoinGrpDate As Label
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnSelAppJoinGrpDate As Image

    Protected WithEvents ddlPOH As DropDownList
    Protected WithEvents txtRemark1 As TextBox
    Protected WithEvents txtRemark2 As TextBox
    Protected WithEvents txtRemark3 As TextBox
    Protected WithEvents txtInsuranceNo1 As TextBox
    Protected WithEvents txtInsuranceNo2 As TextBox
    Protected WithEvents txtInsuranceNo3 As TextBox

    Protected WithEvents lblErrAnnualLeaveBalance As Label
    Protected WithEvents lblErrSickLeaveBalance As Label
    Protected WithEvents lblAnnualLeaveBalance As Label 
    Protected WithEvents lblSickLeaveBalance As Label 
    Protected WithEvents lblAnnualLeave As Label 
    Protected WithEvents lblSickLeave As Label 
    Protected WithEvents lblBFLeave As Label
    Protected WithEvents lblErrBF As Label    
    Protected WithEvents txtAnnualLeaveBalance As TextBox
    Protected WithEvents txtSickLeaveBalance As TextBox
    Protected WithEvents lblErrSickLeaveBalanceAmt As Label
    Protected WithEvents lblErrUpdateSick As Label 
    Protected WithEvents lblErrAnnualLeaveBalanceAmt As Label
    Protected WithEvents lblErrUpdateAnnual As Label 
    Protected WithEvents lblErrEmpValidation As Label

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbDetails As LinkButton
    Protected WithEvents lbPayroll As LinkButton
    Protected WithEvents lbStatutory As LinkButton
    Protected WithEvents lbFamily As LinkButton
    Protected WithEvents lbQualific As LinkButton
    Protected WithEvents lbSkill As LinkButton
    Protected WithEvents lbCareerProg As LinkButton

    Protected WithEvents ddlKeraniType As DropDownList
    Protected WithEvents lblErrKeraniType As Label
    Dim objHR As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPR As New agri.PR.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()


    Dim objEmpEmpDs As New Object()
    Dim objCPDs As New Object()
    Dim objCompDs As New Object()
    Dim objLocDs As New Object()
    Dim objDeptDs As New Object()
    Dim objLevelDs As New Object()
    Dim objRptDs As New Object()
    Dim objPosDs As New Object()
    Dim objSalSchemeDs As New Object()
    Dim objSalGradeDs As New Object()
    Dim objHolScheduleDs As New Object()
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
    Dim strSelectedAppCPCode As String = ""
    Dim strSelectedLastCPCode As String = ""
    Dim strSelectedTerminateCPCode As String = ""
    Dim strSelectedCompCode As String = ""
    Dim strSelectedLocCode As String = ""
    Dim strSelectedDeptCode As String = ""
    Dim strSelectedLevelCode As String = ""
    Dim strSelectedRptTo As String = ""
    Dim strSelectedPosCode As String = ""
    Dim strSelectedSalSchemeCode As String = ""
    Dim strSelectedSalGradeCode As String = ""
    Dim strSelectedHolSchedule As String = ""
    Dim strSelectedPointOfHired As String = ""
    Dim strSortExpression As String = "EmpCode"
    Dim strAcceptFormat As String
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeEmployement), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrAnnualLeaveBalance.Visible = False
            lblErrSickLeaveBalance.Visible = False
            lblErrBF.Visible = False
            lblErrAnnualLeaveBalanceAmt.Visible = False
            lblErrSickLeaveBalanceAmt.Visible = False
            lblErrUpdateSick.Visible = False
            lblErrUpdateAnnual.Visible = False
            lblErrEmpValidation.Visible = False
            ddlPOH.Enabled = True
            lblErrKeraniType.Visible = False
            lblRedirect.Text = Request.QueryString("redirect")

            strSelectedEmpCode = Trim(IIf(Request.QueryString("EmpCode") <> "", Request.QueryString("EmpCode"), Request.Form("EmpCode")))
            strSelectedEmpName = Trim(IIf(Request.QueryString("EmpName") <> "", Request.QueryString("EmpName"), Request.Form("EmpName")))
            strSelectedEmpStatus = Trim(IIf(Request.QueryString("EmpStatus") <> "", Request.QueryString("EmpStatus"), Request.Form("EmpStatus")))
            If Not IsPostBack Then
                BindKeraniType("")
                If strSelectedEmpCode <> "" Then
                    BindEmpEmp(strSelectedEmpCode)
                End If                
                BindCP()
                BindComp()
                BindLoc()
                BindDept()
                BindLevel()
                BindRpt()
                BindPos()
                BindSalScheme()
                BindSalGrade()
                BindHolSchedule()
                BindShift("") 
                BindPOH()                 
                onload_LinkButton() 
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblCP1.text = GetCaption(objLangCap.EnumLangCap.CareerProgress)
        lblCP2.text = GetCaption(objLangCap.EnumLangCap.CareerProgress)
        lblCP3.text = GetCaption(objLangCap.EnumLangCap.CareerProgress)
        lblCP4.text = GetCaption(objLangCap.EnumLangCap.CareerProgress)
        lblCompany.text = GetCaption(objLangCap.EnumLangCap.Company)
        lblLocation.text = GetCaption(objLangCap.EnumLangCap.Location)
        lblDepartment.text = GetCaption(objLangCap.EnumLangCap.Department)
        lblLevel.text = GetCaption(objLangCap.EnumLangCap.Level)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
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

    Private Sub lbStatutory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbStatutory.Click
        Response.Redirect("HR_trx_EmployeeStat.aspx?redirect=empstat&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFamily.Click
        Response.Redirect("HR_trx_EmployeeFamList.aspx?redirect=empfam&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
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

    Sub BindEmpEmp(ByVal pv_strEmpCode As String)
        Dim strOpCd_EmpEmp As String = "HR_CLSTRX_EMPLOYMENT_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        lblEmpCode.Text = strSelectedEmpCode
        lblEmpName.Text = strSelectedEmpName
        lblEmpStatus.Text = strSelectedEmpStatus

        strParam = strSelectedEmpCode & "|||" & strSortExpression & "|"

        Try
            intErrNo = objHR.mtdGetEmployeeEmp(strOpCd_EmpEmp, strParam, objEmpEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_EMPLOYEE_EMPLOYMENT&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objEmpEmpDs.Tables(0).Rows.Count > 0 Then
            objEmpEmpDs.Tables(0).Rows(0).Item("EmpCode") = objEmpEmpDs.Tables(0).Rows(0).Item("EmpCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("EmpName") = objEmpEmpDs.Tables(0).Rows(0).Item("EmpName").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("AppCPCode") = objEmpEmpDs.Tables(0).Rows(0).Item("AppCPCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("LastCPDateFrom") = objEmpEmpDs.Tables(0).Rows(0).Item("LastCPDateFrom")
            objEmpEmpDs.Tables(0).Rows(0).Item("LastCPCode") = objEmpEmpDs.Tables(0).Rows(0).Item("LastCPCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("TerminateCPCode") = objEmpEmpDs.Tables(0).Rows(0).Item("TerminateCPCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("CompCode") = objEmpEmpDs.Tables(0).Rows(0).Item("CompCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("LocCode") = objEmpEmpDs.Tables(0).Rows(0).Item("LocCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("DeptCode") = objEmpEmpDs.Tables(0).Rows(0).Item("DeptCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("LevelCode") = objEmpEmpDs.Tables(0).Rows(0).Item("LevelCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("RptTo") = objEmpEmpDs.Tables(0).Rows(0).Item("RptTo").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("PosCode") = objEmpEmpDs.Tables(0).Rows(0).Item("PosCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("ShiftCode") = objEmpEmpDs.Tables(0).Rows(0).Item("ShiftCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("MechInd") = objEmpEmpDs.Tables(0).Rows(0).Item("MechInd").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("SalSchemeCode") = objEmpEmpDs.Tables(0).Rows(0).Item("SalSchemeCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("SalGradeCode") = objEmpEmpDs.Tables(0).Rows(0).Item("SalGradeCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("Probation") = objEmpEmpDs.Tables(0).Rows(0).Item("Probation").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("HolSchedule") = objEmpEmpDs.Tables(0).Rows(0).Item("HolSchedule").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeave") = objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeave").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("SickLeave") = objEmpEmpDs.Tables(0).Rows(0).Item("SickLeave").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("BFLeave") = objEmpEmpDs.Tables(0).Rows(0).Item("BFLeave").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("OffDay") = objEmpEmpDs.Tables(0).Rows(0).Item("OffDay").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("Status") = objEmpEmpDs.Tables(0).Rows(0).Item("Status").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("KeraniType") = objEmpEmpDs.Tables(0).Rows(0).Item("KeraniType").Trim()

            objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeaveBalance") = objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeaveBalance").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("SickLeaveBalance") = objEmpEmpDs.Tables(0).Rows(0).Item("SickLeaveBalance").Trim()

            objEmpEmpDs.Tables(0).Rows(0).Item("POHCode") = objEmpEmpDs.Tables(0).Rows(0).Item("POHCode").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceNo1") = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceNo1").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceName1") = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceName1").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceNo2") = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceNo2").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceName2") = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceName2").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceNo3") = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceNo3").Trim()
            objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceName3") = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceName3").Trim()
            lblAppJoinDate.Text = Date_Validation(objEmpEmpDs.Tables(0).Rows(0).Item("AppJoinDate"), True)
            txtAppJoinGrpDate.Text = Date_Validation(objEmpEmpDs.Tables(0).Rows(0).Item("AppJoinGrpDate"), True)
            strSelectedAppCPCode = objEmpEmpDs.Tables(0).Rows(0).Item("AppCPCode")
            lblLastCPDateFrom.Text = Date_Validation(objEmpEmpDs.Tables(0).Rows(0).Item("LastCPDateFrom"), True)
            lblLastCPDateTo.Text = Date_Validation(objEmpEmpDs.Tables(0).Rows(0).Item("LastCPDateTo"), True)
            strSelectedLastCPCode = objEmpEmpDs.Tables(0).Rows(0).Item("LastCPCode")
            lblTerminateDate.Text = Date_Validation(objEmpEmpDs.Tables(0).Rows(0).Item("TerminateDate"), True)
            strSelectedTerminateCPCode = objEmpEmpDs.Tables(0).Rows(0).Item("TerminateCPCode")
            lblLastIncDate.Text = Date_Validation(objEmpEmpDs.Tables(0).Rows(0).Item("LastIncDate"), True)
            lblNextIncDate.Text = Date_Validation(objEmpEmpDs.Tables(0).Rows(0).Item("NextIncDate"), True)
            strSelectedCompCode = objEmpEmpDs.Tables(0).Rows(0).Item("CompCode")
            strSelectedLocCode = objEmpEmpDs.Tables(0).Rows(0).Item("LocCode")
            strSelectedDeptCode = objEmpEmpDs.Tables(0).Rows(0).Item("DeptCode")
            strSelectedLevelCode = objEmpEmpDs.Tables(0).Rows(0).Item("LevelCode")
            strSelectedRptTo = objEmpEmpDs.Tables(0).Rows(0).Item("RptTo")
            strSelectedPosCode = objEmpEmpDs.Tables(0).Rows(0).Item("PosCode")
            strSelectedPointOfHired = objEmpEmpDs.Tables(0).Rows(0).Item("POHCode")

            If CInt(objEmpEmpDs.Tables(0).Rows(0).Item("MechInd")) = objHR.EnumMechStatus.Yes
                cbMechInd.Checked = True
            Else
                cbMechInd.Checked = False
            End If 

            If CInt(objEmpEmpDs.Tables(0).Rows(0).Item("IsGangLeader")) = objHRSetup.EnumIsGangLeader.Yes
                cbGangLeader.Checked = True
            Else
                cbGangLeader.Checked = False
            End If 

            strSelectedSalSchemeCode = objEmpEmpDs.Tables(0).Rows(0).Item("SalSchemeCode")
            strSelectedSalGradeCode = objEmpEmpDs.Tables(0).Rows(0).Item("SalGradeCode")
            lblBlackListDate.Text = Date_Validation(objEmpEmpDs.Tables(0).Rows(0).Item("BlackListDate"), True)
            lblProbation.Text = objEmpEmpDs.Tables(0).Rows(0).Item("Probation")
            lblConfirmDate.Text = Date_Validation(objEmpEmpDs.Tables(0).Rows(0).Item("ConfirmDate"), True)
            strSelectedHolSchedule = objEmpEmpDs.Tables(0).Rows(0).Item("HolSchedule")
            txtAnnualLeave.Text = objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeave")
            txtSickLeave.Text = objEmpEmpDs.Tables(0).Rows(0).Item("SickLeave")
            txtAnnualLeaveBalance.Text = objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeaveBalance")
            txtSickLeaveBalance.Text = objEmpEmpDs.Tables(0).Rows(0).Item("SickLeaveBalance")
            If objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeaveBalance") = "0" Then
                lblErrAnnualLeaveBalance.Visible = True
            Else
                lblErrAnnualLeaveBalance.Visible = False
            End If
            If objEmpEmpDs.Tables(0).Rows(0).Item("SickLeaveBalance") = "0" Then
                lblErrSickLeaveBalance.Visible = True
            Else
                lblErrSickLeaveBalance.Visible = False
            End If
            lblAnnualLeaveBalance.Text = objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeaveBalance")
            lblSickLeaveBalance.Text = objEmpEmpDs.Tables(0).Rows(0).Item("SickLeaveBalance")
            lblAnnualLeave.Text = objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeave")
            lblSickLeave.Text = objEmpEmpDs.Tables(0).Rows(0).Item("SickLeave")
            lblBFLeave.Text = objEmpEmpDs.Tables(0).Rows(0).Item("BFLeave")
            txtBFLeave.Text = objEmpEmpDs.Tables(0).Rows(0).Item("BFLeave")

            BindShift(objEmpEmpDs.Tables(0).Rows(0).Item("ShiftCode"))
            BindKeraniType(objEmpEmpDs.Tables(0).Rows(0).Item("KeraniType"))

            txtOffDay.Text = objEmpEmpDs.Tables(0).Rows(0).Item("OffDay")

            txtInsuranceNo1.Text = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceNo1")
            txtInsuranceNo2.Text = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceNo2")
            txtInsuranceNo3.Text = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceNo3")
            txtRemark1.Text = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceName1")      
            txtRemark2.Text = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceName2")      
            txtRemark3.Text = objEmpEmpDs.Tables(0).Rows(0).Item("InsuranceName3")      

        Else
            BindShift("")
            BindKeraniType("")
        End If

        Select case CInt(lblEmpStatus.Text)
            case objHR.EnumEmpStatus.Active
                txtAppJoinGrpDate.ReadOnly = False
                ddlLevelCode.Enabled = True
                ddlRptTo.Enabled = True
                ddlShift.Enabled = True
                cbMechInd.Enabled = True
                cbGangLeader.Enabled = True
                txtOffDay.ReadOnly = False
                ddlHolSchedule.Enabled = True
                txtBFLeave.ReadOnly = False
                txtSickLeave.ReadOnly = False
                txtAnnualLeave.ReadOnly = False
                txtSickLeaveBalance.ReadOnly = False
                txtAnnualLeaveBalance.ReadOnly = False
                txtInsuranceNo1.ReadOnly = False
                txtRemark1.ReadOnly = False
                txtInsuranceNo2.ReadOnly = False
                txtRemark2.ReadOnly = False
                txtInsuranceNo3.ReadOnly = False
                txtRemark3.ReadOnly = False
            case objHR.EnumEmpStatus.Deleted
                txtAppJoinGrpDate.ReadOnly = True
                ddlLevelCode.Enabled = False
                ddlRptTo.Enabled = False
                ddlShift.Enabled = False
                cbMechInd.Enabled = False
                cbGangLeader.Enabled = False
                txtOffDay.ReadOnly = True
                ddlHolSchedule.Enabled = False
                txtBFLeave.ReadOnly = True
                txtSickLeave.ReadOnly = True
                txtAnnualLeave.ReadOnly = True
                txtSickLeaveBalance.ReadOnly = True
                txtAnnualLeaveBalance.ReadOnly = True
                txtInsuranceNo1.ReadOnly = True
                txtRemark1.ReadOnly = True
                txtInsuranceNo2.ReadOnly = True
                txtRemark2.ReadOnly = True
                txtInsuranceNo3.ReadOnly = True
                txtRemark3.ReadOnly = True
                btnSelAppJoinGrpDate.visible = False
                btnSave.visible = False
        End Select

    End Sub

    Sub BindCP()
        Dim strOpCd_Get As String = "HR_CLSSETUP_CP_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intAppCPIndex As Integer = 0
        Dim intLastCPIndex As Integer = 0
        Dim intTerminateCPIndex As Integer = 0
        Dim dr As DataRow
        
        strParam = "||" & objHRSetup.EnumCareerProgressStatus.Active & "||CP.CPCode||" 

        Try
            intErrNo = objHRSetup.mtdGetCareerProgress(strOpCd_Get, strParam, objCPDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_CP&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objCPDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objCPDs.Tables(0).Rows.Count - 1
                objCPDs.Tables(0).Rows(intCnt).Item("CPCode") = Trim(objCPDs.Tables(0).Rows(intCnt).Item("CPCode"))
                objCPDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objCPDs.Tables(0).Rows(intCnt).Item("CPCode")) & " (" & _
                                                                     Trim(objCPDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objCPDs.Tables(0).Rows(intCnt).Item("CPCode") = strSelectedAppCPCode
                    intAppCPIndex = intCnt + 1
                End If
                If objCPDs.Tables(0).Rows(intCnt).Item("CPCode") = strSelectedLastCPCode
                    intLastCPIndex = intCnt + 1
                End If
                If objCPDs.Tables(0).Rows(intCnt).Item("CPCode") = strSelectedTerminateCPCode
                    intTerminateCPIndex = intCnt + 1
                End If
            Next
        End If

        dr = objCPDs.Tables(0).NewRow()
        dr("CPCode") = ""
        dr("Description") = lblSelect.text & lblCP1.text & lblCode.text
        objCPDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAppCPCode.DataSource = objCPDs.Tables(0)
        ddlAppCPCode.DataTextField = "Description"
        ddlAppCPCode.DataValueField = "CPCode"
        ddlAppCPCode.DataBind()
        ddlAppCPCode.SelectedIndex = intAppCPIndex

        ddlLastCPCode.DataSource = objCPDs.Tables(0)
        ddlLastCPCode.DataTextField = "Description"
        ddlLastCPCode.DataValueField = "CPCode"
        ddlLastCPCode.DataBind()
        ddlLastCPCode.SelectedIndex = intLastCPIndex

        ddlTerminateCPCode.DataSource = objCPDs.Tables(0)
        ddlTerminateCPCode.DataTextField = "Description"
        ddlTerminateCPCode.DataValueField = "CPCode"
        ddlTerminateCPCode.DataBind()
        ddlTerminateCPCode.SelectedIndex = intTerminateCPIndex
    End Sub

    Sub BindComp()
        Dim strOpCd_Get As String = "ADMIN_CLSCOMP_COMPANY_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intCompIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objAdminComp.EnumCompanyStatus.Active & "||CompCode|" 

        Try
            intErrNo = objAdminComp.mtdGetComp(strOpCd_Get, strParam, objCompDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_COMPANY&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objCompDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objCompDs.Tables(0).Rows.Count - 1
                objCompDs.Tables(0).Rows(intCnt).Item("CompCode") = Trim(objCompDs.Tables(0).Rows(intCnt).Item("CompCode"))
                objCompDs.Tables(0).Rows(intCnt).Item("CompName") = Trim(objCompDs.Tables(0).Rows(intCnt).Item("CompCode")) & " (" & _
                                                                    Trim(objCompDs.Tables(0).Rows(intCnt).Item("CompName")) & ")"
                
                If objCompDs.Tables(0).Rows(intCnt).Item("CompCode") = strSelectedCompCode
                    intCompIndex = intCnt + 1
                End If
            Next
        End If

        dr = objCompDs.Tables(0).NewRow()
        dr("CompCode") = ""
        dr("CompName") = lblSelect.text & lblCompany.text & lblCode.text
        objCompDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlCompCode.DataSource = objCompDs.Tables(0)
        ddlCompCode.DataTextField = "CompName"
        ddlCompCode.DataValueField = "CompCode"
        ddlCompCode.DataBind()
        ddlCompCode.SelectedIndex = intCompIndex
    End Sub

    Sub BindLoc()
        Dim strOpCd_Get As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intLocIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objAdminLoc.EnumLocStatus.Active & "||LocCode||" 

        Try
            intErrNo = objAdminLoc.mtdGetLocCode(strOpCd_Get, strParam, objLocDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_LOCATION&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objLocDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
                objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objLocDs.Tables(0).Rows(intCnt).Item("LocCode"))
                objLocDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objLocDs.Tables(0).Rows(intCnt).Item("LocCode")) & " (" & _
                                                                      Trim(objLocDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = strSelectedLocCode
                    intLocIndex = intCnt + 1
                End If
            Next
        End If

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelect.text & lblLocation.text & lblCode.text
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocCode.DataSource = objLocDs.Tables(0)
        ddlLocCode.DataTextField = "Description"
        ddlLocCode.DataValueField = "LocCode"
        ddlLocCode.DataBind()
        ddlLocCode.SelectedIndex = intLocIndex
    End Sub

    Sub BindDept()
        Dim strOpCd_Get As String = "HR_CLSSETUP_DEPT_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intDeptIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|" & objHRSetup.EnumDeptStatus.Active & "||A.DeptCode||" 

        Try
            intErrNo = objHRSetup.mtdGetDept(strOpCd_Get, strParam, objDeptDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_DEPARTMENT&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDeptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDeptDs.Tables(0).Rows.Count - 1
                objDeptDs.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(objDeptDs.Tables(0).Rows(intCnt).Item("DeptCode"))
                objDeptDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDeptDs.Tables(0).Rows(intCnt).Item("DeptCode")) & " (" & _
                                                                       Trim(objDeptDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objDeptDs.Tables(0).Rows(intCnt).Item("DeptCode") = strSelectedDeptCode
                    intDeptIndex = intCnt + 1
                End If
            Next
        End If

        dr = objDeptDs.Tables(0).NewRow()
        dr("DeptCode") = ""
        dr("Description") = lblSelect.text & lblDepartment.text
        objDeptDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDeptCode.DataSource = objDeptDs.Tables(0)
        ddlDeptCode.DataTextField = "Description"
        ddlDeptCode.DataValueField = "DeptCode"
        ddlDeptCode.DataBind()
        ddlDeptCode.SelectedIndex = intDeptIndex
    End Sub

    Sub BindLevel()
        Dim strOpCd_Get As String = "HR_CLSSETUP_LEVEL_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intLevelIndex As Integer = 0
        Dim dr As DataRow

        strParam = "Order By LVL.LevelCode | AND LVL.Status LIKE '" & objHRSetup.EnumLevelStatus.Active & "'"

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Get, strParam, objHRSetup.EnumHRMasterType.Level, objLevelDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_LEVEL&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objLevelDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objLevelDs.Tables(0).Rows.Count - 1
                objLevelDs.Tables(0).Rows(intCnt).Item("LevelCode") = Trim(objLevelDs.Tables(0).Rows(intCnt).Item("LevelCode"))
                objLevelDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objLevelDs.Tables(0).Rows(intCnt).Item("LevelCode")) & " (" & _
                                                                        Trim(objLevelDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objLevelDs.Tables(0).Rows(intCnt).Item("LevelCode") = strSelectedLevelCode
                    intLevelIndex = intCnt + 1
                End If
            Next
        End If

        dr = objLevelDs.Tables(0).NewRow()
        dr("LevelCode") = ""
        dr("Description") = lblSelect.text & lblLevel.text & lblCode.text
        objLevelDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLevelCode.DataSource = objLevelDs.Tables(0)
        ddlLevelCode.DataTextField = "Description"
        ddlLevelCode.DataValueField = "LevelCode"
        ddlLevelCode.DataBind()
        ddlLevelCode.SelectedIndex = intLevelIndex
    End Sub

    Sub BindRpt()
        Dim strOpCd_Get As String = "HR_CLSTRX_EMPLOYEE_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intRptToIndex As Integer = 0
        Dim dr As DataRow 

        strParam = lblEmpCode.Text & "|" & objHR.EnumEmpStatus.Active

        Try
            intErrNo = objHR.mtdGetOthEmployee(strOpCd_Get, strParam, strLocation, objRptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_REPORTTO&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
                objRptDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objRptDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objRptDs.Tables(0).Rows(intCnt).Item("EmpCode")) & " (" & _
                                                                  Trim(objRptDs.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
                
                If objRptDs.Tables(0).Rows(intCnt).Item("EmpCode") = strSelectedRptTo
                    intRptToIndex = intCnt + 1
                End If
            Next
        End If

        dr = objRptDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = "Please Select Report To"
        objRptDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlRptTo.DataSource = objRptDs.Tables(0)
        ddlRptTo.DataTextField = "EmpName"
        ddlRptTo.DataValueField = "EmpCode"
        ddlRptTo.DataBind()
        ddlRptTo.SelectedIndex = intRptToIndex
    End Sub

    Sub BindPos()
        Dim strOpCd_Get As String = "HR_CLSSETUP_POSITION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intPosIndex As Integer = 0
        Dim dr As DataRow

        strParam = "Order By POS.PositionCode | AND POS.Status LIKE '" & objHRSetup.EnumPositionStatus.Active & "'"

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Get, strParam, objHRSetup.EnumHRMasterType.Position, objPosDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_POSITION&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objPosDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objPosDs.Tables(0).Rows.Count - 1
                objPosDs.Tables(0).Rows(intCnt).Item("PositionCode") = Trim(objPosDs.Tables(0).Rows(intCnt).Item("PositionCode"))
                objPosDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objPosDs.Tables(0).Rows(intCnt).Item("PositionCode")) & " (" & _
                                                                      Trim(objPosDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objPosDs.Tables(0).Rows(intCnt).Item("PositionCode") = strSelectedPosCode
                    intPosIndex = intCnt + 1
                End If
            Next
        End If

        dr = objPosDs.Tables(0).NewRow()
        dr("PositionCode") = ""
        dr("Description") = "Please Select Position"
        objPosDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPosCode.DataSource = objPosDs.Tables(0)
        ddlPosCode.DataTextField = "Description"
        ddlPosCode.DataValueField = "PositionCode"
        ddlPosCode.DataBind()
        ddlPosCode.SelectedIndex = intPosIndex
    End Sub

    Sub BindShift(ByVal pv_SelShift As String)
        Dim strOpCd_Get As String = "HR_CLSSETUP_SHIFT_SEARCH"
        Dim objShift As New Dataset()
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intIndex As Integer = 0
        Dim dr As DataRow

        Try
            strParam = "||" & objHRSetup.EnumShiftStatus.Active & "||S.ShiftCode|ASC"
            intErrNo = objHRSetup.mtdGetShift(strOpCd_GET, strParam, objShift, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPLOYMENT_SHIFT_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objShift.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objShift.Tables(0).Rows.Count - 1
                objShift.Tables(0).Rows(intCnt).Item("ShiftCode") = objShift.Tables(0).Rows(intCnt).Item("ShiftCode").Trim()
                objShift.Tables(0).Rows(intCnt).Item("Description") = objShift.Tables(0).Rows(intCnt).Item("ShiftCode") & " (" & objShift.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
                If objShift.Tables(0).Rows(intCnt).Item("ShiftCode") = pv_SelShift Then
                    intIndex = intCnt + 1
                End If
            Next
        End If

        dr = objShift.Tables(0).NewRow()
        dr("ShiftCode") = ""
        dr("Description") = "Please Select Shift"
        objShift.Tables(0).Rows.InsertAt(dr, 0)

        ddlShift.DataSource = objShift.Tables(0)
        ddlShift.DataTextField = "Description"
        ddlShift.DataValueField = "ShiftCode"
        ddlShift.DataBind()
        ddlShift.SelectedIndex = intIndex
    End Sub

    Sub BindSalScheme()
        Dim strOpCd_Get As String = "HR_CLSSETUP_SALSCHEME_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSalSchemeIndex As Integer = 0
        Dim dr As DataRow

        strParam = "Order By SAL.SalSchemeCode | AND SAL.Status LIKE '" & objHRSetup.EnumSalSchemeStatus.Active & "'"

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Get, strParam, objHRSetup.EnumHRMasterType.SalScheme, objSalSchemeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_SALSCHEME&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objSalSchemeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objSalSchemeDs.Tables(0).Rows.Count - 1
                objSalSchemeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode") = Trim(objSalSchemeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode"))
                objSalSchemeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objSalSchemeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode")) & " (" & _
                                                                            Trim(objSalSchemeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objSalSchemeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode") = strSelectedSalSchemeCode
                    intSalSchemeIndex = intCnt + 1
                End If
            Next
        End If

        dr = objSalSchemeDs.Tables(0).NewRow()
        dr("SalSchemeCode") = ""
        dr("Description") = "Please Select Salary Scheme"
        objSalSchemeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSalSchemeCode.DataSource = objSalSchemeDs.Tables(0)
        ddlSalSchemeCode.DataTextField = "Description"
        ddlSalSchemeCode.DataValueField = "SalSchemeCode"
        ddlSalSchemeCode.DataBind()
        ddlSalSchemeCode.SelectedIndex = intSalSchemeIndex
    End Sub

    Sub BindSalGrade()
        Dim strOpCd_Get As String = "HR_CLSSETUP_SALGRADE_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSalGradeIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objHRSetup.EnumSalGradeStatus.Active & "||SG.SalGradeCode||" 

        Try
            intErrNo = objHRSetup.mtdGetSalGrade(strOpCd_Get, strParam, objSalGradeDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_SALGRADE&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objSalGradeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objSalGradeDs.Tables(0).Rows.Count - 1
                objSalGradeDs.Tables(0).Rows(intCnt).Item("SalGradeCode") = Trim(objSalGradeDs.Tables(0).Rows(intCnt).Item("SalGradeCode"))
                objSalGradeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode") = Trim(objSalGradeDs.Tables(0).Rows(intCnt).Item("SalGradeCode")) & " (" & _
                                                                            Trim(objSalGradeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode")) & ")"
                
                If objSalGradeDs.Tables(0).Rows(intCnt).Item("SalGradeCode") = strSelectedSalGradeCode
                    intSalGradeIndex = intCnt + 1
                End If
            Next
        End If

        dr = objSalGradeDs.Tables(0).NewRow()
        dr("SalGradeCode") = ""
        dr("SalSchemeCode") = "Please Select Salary Grade"
        objSalGradeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSalGradeCode.DataSource = objSalGradeDs.Tables(0)
        ddlSalGradeCode.DataTextField = "SalSchemeCode"
        ddlSalGradeCode.DataValueField = "SalGradeCode"
        ddlSalGradeCode.DataBind()
        ddlSalGradeCode.SelectedIndex = intSalGradeIndex
    End Sub

    Sub BindHolSchedule()
        Dim strOpCd_Get As String = "HR_CLSSETUP_HS_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intHolScheduleIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objHRSetup.EnumHSStatus.Active & "||HS.HSCode||" 

        Try
            intErrNo = objHRSetup.mtdGetHolidaySchedule(strOpCd_GET, strParam, objHolScheduleDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_HOLIDAYSCHEDULE&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objHolScheduleDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objHolScheduleDs.Tables(0).Rows.Count - 1
                objHolScheduleDs.Tables(0).Rows(intCnt).Item("HSCode") = Trim(objHolScheduleDs.Tables(0).Rows(intCnt).Item("HSCode"))
                objHolScheduleDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objHolScheduleDs.Tables(0).Rows(intCnt).Item("HSCode")) & " (" & _
                                                                              Trim(objHolScheduleDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objHolScheduleDs.Tables(0).Rows(intCnt).Item("HSCode") = strSelectedHolSchedule
                    intHolScheduleIndex = intCnt + 1
                End If
            Next
        End If

        dr = objHolScheduleDs.Tables(0).NewRow()
        dr("HSCode") = ""
        dr("Description") = "Please Select Holiday Schedule"
        objHolScheduleDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlHolSchedule.DataSource = objHolScheduleDs.Tables(0)
        ddlHolSchedule.DataTextField = "Description"
        ddlHolSchedule.DataValueField = "HSCode"
        ddlHolSchedule.DataBind()
        ddlHolSchedule.SelectedIndex = intHolScheduleIndex
    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object()
        Dim objActualDate As New Object()
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_EmployeeList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End if
    End Function

    Sub btnSave_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_GetDet As String = "HR_CLSTRX_EMPLOYEE_GET"
        Dim strOpCd_GetPay As String = "HR_CLSTRX_PAYROLL_GET"
        Dim strOpCd_GetEmp As String = "HR_CLSTRX_EMPLOYMENT_GET"
        Dim strOpCd_GetStat As String = "HR_CLSTRX_STATUTORY_GET"
        Dim strOpCd_UpdEmp As String = "HR_CLSTRX_EMPLOYMENT_UPD"
        Dim strOpCd_AddEmp As String = "HR_CLSTRX_EMPLOYMENT_ADD"
        Dim strOpCd_UpdDet As String = "HR_CLSTRX_EMPLOYEE_UPD"
        Dim intErrNo As Integer
        Dim strChkParam As String
        Dim strParam As String
        Dim strEmpCode As String = lblEmpCode.Text
        Dim strAppJoinDate As String = lblAppJoinDate.Text
        Dim strAppJoinGrpDate As String = txtAppJoinGrpDate.Text
        Dim strAppCPCode As String = ddlAppCPCode.SelectedItem.Value
        Dim strLastCPDateFrom As String = lblLastCPDateFrom.Text
        Dim strLastCPDateTo As String = lblLastCPDateTo.Text
        Dim strLastCPCode As String = ddlLastCPCode.SelectedItem.Value
        Dim strTerminateDate As String = lblTerminateDate.Text
        Dim strTerminateCPCode As String = ddlTerminateCPCode.SelectedItem.Value
        Dim strLastIncDate As String = lblLastIncDate.Text
        Dim strNextIncDate As String = lblNextIncDate.Text
        Dim strCompCode As String = ddlCompCode.SelectedItem.Value
        Dim strLocCode As String = ddlLocCode.SelectedItem.Value
        Dim strDeptCode As String = ddlDeptCode.SelectedItem.Value
        Dim strLevelCode As String = ddlLevelCode.SelectedItem.Value
        Dim strRptTo As String = ddlRptTo.SelectedItem.Value
        Dim strPosCode As String = ddlPosCode.SelectedItem.Value
        Dim strShiftCode As String = ddlShift.SelectedItem.Value
        Dim strMechInd As String = IIf(cbMechInd.Checked = True, objHR.EnumMechStatus.Yes, objHR.EnumMechStatus.No)
        Dim strIsGangLader As String = IIf(cbGangLeader.Checked = True, objHRSetup.EnumIsGangLeader.Yes, objHRSetup.EnumIsGangLeader.No)
        Dim strSalSchemeCode As String = ddlSalSchemeCode.SelectedItem.Value
        Dim strSalGradeCode As String = ddlSalGradeCode.SelectedItem.Value
        Dim strBlackListDate As String = lblBlackListDate.Text
        Dim strProbation As String = lblProbation.Text
        Dim strConfirmDate As String = lblConfirmDate.Text
        Dim strHolSchedule As String = ddlHolSchedule.SelectedItem.Value
        Dim strAnnualLeave As String = txtAnnualLeave.Text
        Dim strSickLeave As String = txtSickLeave.Text
        Dim strBFLeave As String = txtBFLeave.Text
        Dim strOffDay As String = txtOffDay.Text
        Dim strAnnualLeaveBalance As String
        Dim strSickLeaveBalance As String
        Dim strSelectedPointOfHired As String = ddlPOH.SelectedItem.Value
        Dim strInsuranceNo1 As String = txtInsuranceNo1.Text
        Dim strRemark1 As String = txtRemark1.Text
        Dim strInsuranceNo2 As String = txtInsuranceNo2.Text  
        Dim strRemark2 As String = txtRemark2.Text
        Dim strInsuranceNo3 As String = txtInsuranceNo3.Text
        Dim strRemark3 As String = txtRemark3.Text

        Dim strKeraniType As String = ddlKeraniType.SelectedItem.Value

        
            If Trim(txtAnnualLeave.Text) <> "" Or Trim(txtAnnualLeaveBalance.Text) <> "" Or _
                    Trim(txtSickLeave.Text) <> "" Or Trim(txtSickLeave.Text) <> "" Or Trim(txtBFLeave.Text) <> "" Then
                ValidationEmployee(strSelectedEmpCode)
                If lblErrEmpValidation.Visible = True Then
                    Exit Sub
                End if
            End If
            If (Trim(lblAnnualLeave.Text) <> "" Or Trim(lblAnnualLeaveBalance.Text) <> "") Then
                If (txtAnnualLeave.Text <> lblAnnualLeave.Text) And (txtAnnualLeaveBalance.Text <> lblAnnualLeaveBalance.Text ) Then
                    lblErrUpdateAnnual.Visible = True
                    txtAnnualLeave.Text = lblAnnualLeave.Text
                    txtAnnualLeaveBalance.Text = lblAnnualLeaveBalance.Text
                    Exit Sub    
                End If 
            End If
            If Trim(lblSickLeave.Text) <> "" Or Trim(lblSickLeaveBalance.Text) <> "" Then
                If (txtSickLeave.Text <> lblSickLeave.Text) And (txtSickLeaveBalance.Text <> lblSickLeaveBalance.Text ) Then
                    lblErrUpdateSick.Visible = True
                    txtSickLeave.Text = lblSickLeave.Text
                    txtSickLeaveBalance.Text = lblSickLeaveBalance.Text
                    Exit Sub
                End If 
            End If
            If Trim(txtAnnualLeaveBalance.Text) <> "" Then 
                If Trim(lblAnnualLeaveBalance.Text) <> Trim(txtAnnualLeaveBalance.Text) And Trim(lblAnnualLeaveBalance.Text) <> "" Then
                    If CInt(IIf(Trim(txtAnnualLeaveBalance.Text) = "",0,Trim(txtAnnualLeaveBalance.Text))) > CInt(IIf(Trim(txtAnnualLeave.Text) = "",0,Trim(txtAnnualLeave.Text))) Then
                        lblErrAnnualLeaveBalanceAmt.Visible = True
                        Exit Sub
                    End If
                End If
            End If
            
            If Trim(txtSickLeaveBalance.Text) <> "" Then 
                If Trim(lblSickLeaveBalance.Text) <> Trim(txtSickLeaveBalance.Text) And Trim(lblSickLeaveBalance.Text) <> "" Then
                    If CInt(IIf(Trim(txtSickLeaveBalance.Text) = "",0,Trim(txtSickLeaveBalance.Text))) > CInt(IIf(Trim(txtSickLeave.Text) = "",0,Trim(txtSickLeave.Text))) Then
                        lblErrSickLeaveBalanceAmt.Visible = True
                        Exit Sub
                    End If
                End If
            End If

            If Trim(txtBFLeave.Text) <> "" Then 
                If Trim(lblBFLeave.Text) <> Trim(txtBFLeave.Text) And Trim(lblBFLeave.Text) <> "" Then
                    If CInt(IIf(Trim(txtBFLeave.Text) = "",0,Trim(txtBFLeave.Text))) > CInt(IIf(Trim(txtAnnualLeaveBalance.Text)= "",0,Trim(txtAnnualLeaveBalance.Text))) Then
                        lblErrBF.Visible = True
                        Exit Sub
                    End If
                End If
            End If
            
            If ((txtAnnualLeave.Text <> lblAnnualLeave.Text) And (Trim(txtAnnualLeave.Text)<> "" )) Or ((txtSickLeave.Text <> lblSickLeave.Text) And (Trim(txtSickLeave.Text)<> "" )) Then
                strAnnualLeaveBalance = (CInt(IIf(Trim(strAnnualLeave) = "",0,Trim(strAnnualLeave))) - CInt(IIf(Trim(lblAnnualLeave.Text) ="",0,Trim(lblAnnualLeave.Text)))) + CInt(IIf(Trim(lblAnnualLeaveBalance.Text)= "",0,Trim(lblAnnualLeaveBalance.Text)))
                strSickLeaveBalance =   (CInt(IIf(Trim(strSickLeave)= "",0,Trim(strSickLeave))) - CInt(IIf(Trim(lblSickLeave.Text)= "",0,Trim(lblSickLeave.Text)))) + CInt(IIf(Trim(lblSickLeaveBalance.Text)= "",0,Trim(lblSickLeaveBalance.Text)))
            Else
                strAnnualLeaveBalance = txtAnnualLeaveBalance.Text
                strSickLeaveBalance = txtSickLeaveBalance.Text
            End If
       

        If strAppJoinDate <> "" Then
            strAppJoinDate = Date_Validation(strAppJoinDate, False)
        End If

        If strAppJoinGrpDate <> "" Then
            strAppJoinGrpDate = Date_Validation(strAppJoinGrpDate, False)
            If strAppJoinGrpDate = "" Then
                lblErrAppJoinGrpDate.Visible = True
                lblErrAppJoinGrpDate.Text = lblErrAppJoinGrpDate.Text & strAcceptFormat
                Exit Sub
            End If
        End If

        If strIsGangLader = objHRSetup.EnumIsGangLeader.Yes And Trim(strKeraniType) <> "" Then
            lblErrKeraniType.Visible = True
            Exit sub            
        End If

        If strLastCPDateFrom <> "" Then
            strLastCPDateFrom = Date_Validation(strLastCPDateFrom, False)
        End If

        If strLastCPDateTo <> "" Then
            strLastCPDateTo = Date_Validation(strLastCPDateTo, False)
        End If

        If strTerminateDate <> "" Then
            strTerminateDate = Date_Validation(strTerminateDate, False)
        End If

        If strLastIncDate <> "" Then
            strLastIncDate = Date_Validation(strLastIncDate, False)
        End If

        If strNextIncDate <> "" Then
            strNextIncDate = Date_Validation(strNextIncDate, False)
        End If

        If strBlackListDate <> "" Then
            strBlackListDate = Date_Validation(strBlackListDate, False)
        End If

        If strConfirmDate <> "" Then
            strConfirmDate = Date_Validation(strConfirmDate, False)
        End If



        strParam = strEmpCode & "|" & strAppJoinDate & "|" & strAppJoinGrpDate & "|" & strAppCPCode & "|" & _
                   strLastCPDateFrom & "|" & strLastCPDateTo & "|" & strLastCPCode & "|" & _
                   strTerminateDate & "|" & strTerminateCPCode & "|" & _
                   strLastIncDate & "|" & strNextIncDate & "|" & strCompCode & "|" & strLocCode & "|" & _
                   strDeptCode & "|" & strLevelCode & "|" & strRptTo & "|" & strPosCode & "|" & _
                   strMechInd & "|" & strSalSchemeCode & "|" & strSalGradeCode & "|" & strBlackListDate & "|" & _
                   strProbation & "|" & strConfirmDate & "|" & strHolSchedule & "|" & strAnnualLeave & "|" & _
                   strSickLeave & "|" & strBFLeave & "|" & strOffDay & "|" & strShiftCode & "|" & strIsGangLader & "|" & strKeraniType & "|" & _
                   strAnnualLeaveBalance & "|" & strSickLeaveBalance & "|" & strSelectedPointOfHired  & "|" & _
                   strInsuranceNo1 & "|" & strRemark1 & "|" & strInsuranceNo2 & "|" & strRemark2 & "|" & strInsuranceNo3 & "|" & strRemark3
        
        Try
            intErrNo = objHR.mtdUpdEmployeeEmp(strOpCd_GetEmp, _
                                               strOpCd_UpdEmp, _
                                               strOpCd_AddEmp, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SAVE_EMPLOYMENT&errmesg=" & Exp.ToString() & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        strChkParam = strEmpCode & "|" & lblRedirect.Text
        
        Try
            intErrNo = objHR.mtdCheckEmployee(strOpCd_GetDet, _
                                              strOpCd_GetPay, _
                                              strOpCd_GetEmp, _
                                              strOpCd_GetStat, _
                                              strOpCd_UpdDet, _
                                              strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CHECK_EMPLOYEE&errmesg=" & Exp.ToString() & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        strSelectedEmpCode = strEmpCode
        BindEmpEmp(strSelectedEmpCode)
    End Sub

    Sub btnBack_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeList.aspx?redirect=" & lblRedirect.Text)
    End Sub

    Sub BindKeraniType(ByVal pv_strKeraniType As String)
        ddlKeraniType.Items.Clear()
        ddlKeraniType.Items.Add(New ListItem("Please Select Kerani Type",""))
        ddlKeraniType.Items.Add(New ListItem(objHR.mtdGetKeraniType(objHR.EnumKeraniType.KeraniPanen),objHR.EnumKeraniType.KeraniPanen))
        ddlKeraniType.Items.Add(New ListItem(objHR.mtdGetKeraniType(objHR.EnumKeraniType.KeraniTunasPanen),objHR.EnumKeraniType.KeraniTunasPanen))
        ddlKeraniType.Items.Add(New ListItem(objHR.mtdGetKeraniType(objHR.EnumKeraniType.KeraniSupirTransport),objHR.EnumKeraniType.KeraniSupirTransport))
        If pv_strKeraniType <> "" Then
            ddlKeraniType.SelectedIndex = Cint(pv_strKeraniType)
        Else
            ddlKeraniType.SelectedIndex = 0
        End If
    End Sub
    Sub ValidationEmployee(pr_strEmpCode as String)

        Dim objADDs As New Dataset()
        Dim strOpCd_Get As String = "HR_CLSTRX_EMPLOYMENT_CHECKEMP_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strCategory As String
        Dim strPosition As String        
        Dim strSearch As String
        Dim strEmpCode As String


        strEmpCode = Trim(pr_strEmpCode)
        strCategory = " AND (SAL.CategoryTypeInd ='" & objHRSetup.EnumCategoryType.Staff & "' OR SAL.CategoryTypeInd ='" & objHRSetup.EnumCategoryType.NonStaff & "' OR SAL.CategoryTypeInd ='" & objHRSetup.EnumCategoryType.SKUB & "') "
        strPosition = " AND (SAL.PositionInd ='" & objHRSetup.EnumPosition.HQRegional & "' OR SAL.PositionInd ='" & objHRSetup.EnumPosition.Estate & "') "
                
        strParam = strEmpCode & "|" & _
                    strCategory & "|" & _
                      strPosition
                      

            Try
                intErrNo = objHR.mtdValEmployee(strOpCd_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strParam, _
                                                objADDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSTRX_EMPLOYMENT_CHECKEMP_GET&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeEmp.aspx")
            End Try

            If objADDs.Tables(0).Rows.Count > 0 Then
                lblErrEmpValidation.Visible = False                 
            Else
                lblErrEmpValidation.Visible = False                 
            End If 
        
    End Sub


     Sub BindPOH()
        Dim strOpCd_Get As String = "HR_CLSSETUP_POH_SEARCH"
        Dim objPOH As New Dataset()
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intIndex As Integer = 0
        Dim dr As DataRow
        
        strParam = "|" & objHRSetup.EnumPOHCodeStatus.Active & "||POH.POHCode|ASC"

        Try
            intErrNo = objHRSetup.mtdGetPOH(strOpCd_GET, strParam, objPOH, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPLOYMENT_POH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objPOH.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objPOH.Tables(0).Rows.Count - 1
                objPOH.Tables(0).Rows(intCnt).Item("POHCode") = objPOH.Tables(0).Rows(intCnt).Item("POHCode").Trim()
                objPOH.Tables(0).Rows(intCnt).Item("POHDesc") = objPOH.Tables(0).Rows(intCnt).Item("POHCode") & " (" & objPOH.Tables(0).Rows(intCnt).Item("POHDesc").Trim() & ")"
             
                If objPOH.Tables(0).Rows(intCnt).Item("POHCode") = strSelectedPointOfHired Then
                    intIndex = intCnt + 1
                End If
            Next
        End If

        dr = objPOH.Tables(0).NewRow()
        dr("POHCode") = ""
        dr("POHDesc") = "Please Select Point of Hired"
        objPOH.Tables(0).Rows.InsertAt(dr, 0)

        ddlPOH.DataSource = objPOH.Tables(0)
        ddlPOH.DataTextField = "POHDesc"
        ddlPOH.DataValueField = "POHCode"
        ddlPOH.DataBind()
        ddlPOH.SelectedIndex = intIndex
    End Sub



End Class
