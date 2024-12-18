
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic

Imports agri.HR
Imports agri.PWSystem
Imports agri.GlobalHdl

Public Class DP_EmployeeDet : Inherits Page

    Protected WithEvents ddlEmpCode As DropDownList
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox
    Protected WithEvents ddlSex As DropDownList
    Protected WithEvents ddlMarital As DropDownList
    Protected WithEvents txtNewICNo As TextBox
    Protected WithEvents ddlNewICType As DropDownList
    Protected WithEvents txtOldICNo As TextBox
    Protected WithEvents ddlOldICType As DropDownList
    Protected WithEvents txtDOB As TextBox
    Protected WithEvents ddlRace As DropDownList
    Protected WithEvents txtBloodType As TextBox
    Protected WithEvents ddlReligion As DropDownList
    Protected WithEvents txtState As TextBox
    Protected WithEvents cbBumi As CheckBox
    Protected WithEvents ddlNation As DropDownList
    Protected WithEvents txtPassportNo As TextBox
    Protected WithEvents txtPassportExpDate As TextBox
    Protected WithEvents txtWorkpassNo As TextBox
    Protected WithEvents txtWorkpassExpDate As TextBox
    Protected WithEvents txtDriveCls As TextBox
    Protected WithEvents txtDriveExpDate As TextBox
    Protected WithEvents txtOpClass As TextBox
    Protected WithEvents txtOpExpDate As TextBox
    Protected WithEvents txtResAddress As HtmlTextArea
    Protected WithEvents txtPostAddress As HtmlTextArea
    Protected WithEvents txtICAddress As HtmlTextArea
    Protected WithEvents txtResTel As TextBox
    Protected WithEvents txtMobileTel As TextBox
    Protected WithEvents EmpCode As HtmlInputHidden
    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents revEmpCode1 As RequiredFieldValidator
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrEmpCode As Label
    Protected WithEvents lblErrDupEmpCode As Label
    Protected WithEvents lblErrSex As Label
    Protected WithEvents lblErrMarital As Label
    Protected WithEvents lblErrDOB As Label
    Protected WithEvents lblErrRace As Label
    Protected WithEvents lblErrNation As Label
    Protected WithEvents lblErrRecruitment As Label
    Protected WithEvents lblErrPassportExpDate As Label
    Protected WithEvents lblErrWorkpassExpDate As Label
    Protected WithEvents lblErrDriveExpDate As Label
    Protected WithEvents lblErrOpExpDate As Label
    Protected WithEvents lblErrResAddress As Label
    Protected WithEvents lblErrPostAddress As Label
    Protected WithEvents lblErrICAddress As Label
    Protected WithEvents lblReligion As Label
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnSelDOB As Image
    Protected WithEvents btnSelPassportExpDate As Image
    Protected WithEvents btnSelWorkpassExpDate As Image
    Protected WithEvents btnSelDriveExpDate As Image
    Protected WithEvents btnSelOpExpDate As Image
    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents hidEmpName As HtmlInputHidden
    Protected WithEvents hidStatus As HtmlInputHidden
    Protected WithEvents lbPayroll As LinkButton
    Protected WithEvents lbEmployment As LinkButton
    Protected WithEvents lbStatutory As LinkButton
    Protected WithEvents lbFamily As LinkButton
    Protected WithEvents lbQualific As LinkButton
    Protected WithEvents lbSkill As LinkButton
    Protected WithEvents lbCareerProg As LinkButton

    Protected WithEvents txtBatchNo As TextBox
    Protected WithEvents txtLabLicNo As TextBox
    Protected WithEvents txtInsPolNo As TextBox
    Protected WithEvents txtBankGuaNo As TextBox
    Protected WithEvents txtMRExpDate As TextBox
    Protected WithEvents txtLCExpDate As TextBox
    Protected WithEvents txtIPExpDate As TextBox
    Protected WithEvents txtBGExpDate As TextBox
    Protected WithEvents lblErrMRExpDate As Label
    Protected WithEvents lblErrLCExpDate As Label
    Protected WithEvents lblErrIPExpDate As Label
    Protected WithEvents lblErrBGExpDate As Label
    Protected WithEvents btnSelMRExpDate As Image
    Protected WithEvents btnSelLCExpDate As Image
    Protected WithEvents btnSelIPExpDate As Image
    Protected WithEvents btnSelBGExpDate As Image

    Protected WithEvents txtKoperasiNo As TextBox
    Protected WithEvents txtPensionNo As TextBox
    Protected WithEvents ddlKoperasiID As DropDownList

    Protected WithEvents ddlRecruitment As DropDownList
    Protected WithEvents ddltaxstatus As DropDownList
    Protected WithEvents ddlallwstatus As DropDownList
    Protected WithEvents lblErrTaxStatus As label
    Protected WithEvents lblErrAllwStatus As label

    Dim objHR As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objEmpCodeDs As New Object()
    Dim objEmpDetDs As New Object()
    Dim objICTypeDs As New Object()
    Dim objRaceDs As New Object()
    Dim objReligionDs As New Object()
    Dim objNationDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long
    Dim intConfigSetting As Integer

    Dim strSelectedEmpCode As String = ""
    Dim strSelectedNewICType As String = ""
    Dim strSelectedOldICType As String = ""
    Dim strSelectedKoperasiID As String = ""
    Dim strSelectedRace As String = ""
    Dim strSelectedReligion As String = ""
    Dim strSelectedNation As String = ""
    Dim strSortExpression As String = "EmpCode"
    Dim strAcceptFormat As String
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrEmpCode.Visible = False
            lblErrSex.Visible = False
            lblErrMarital.Visible = False
            lblErrDOB.Visible = False
            lblErrRace.Visible = False
            lblErrNation.Visible = False
            lblErrRecruitment.Visible = False
            lblErrPassportExpDate.Visible = False
            lblErrWorkpassExpDate.Visible = False
            lblErrDriveExpDate.Visible = False
            lblErrOpExpDate.Visible = False
            lblErrResAddress.Visible = False
            lblErrPostAddress.Visible = False
            lblErrICAddress.Visible = False
            lblErrDupEmpCode.Visible = False

            lblErrMRExpDate.Visible = False
            lblErrLCExpDate.Visible = False
            lblErrIPExpDate.Visible = False
            lblErrBGExpDate.Visible = False

            revEmpCode1.Visible = False
            btnDelete.Visible = False
            btnUnDelete.Visible = False
            lblRedirect.Text = Request.QueryString("redirect")
            strSelectedEmpCode = Trim(IIf(Request.QueryString("EmpCode") <> "", Request.QueryString("EmpCode"), Request.Form("EmpCode")))

            If Not IsPostBack Then
                If strSelectedEmpCode <> "" Then
                    EmpCode.Value = strSelectedEmpCode
                    onload_Employee(strSelectedEmpCode)
                Else
                    BindSex("0")
                    BindMarital("0")
                    BindRecruitment("0")
                    BindTaxStatus("0")
                    BindAllwStatus("0")
                End If
                BindICType()
                BindRace()
                BindReligion()
                BindNation()
                BindKoperasiID()
                onload_LinkButton()
                If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoEmpCode), intConfigSetting) = True Then
                    ddlEmpCode.Visible = True
                    txtEmpCode.Visible = False
                    BindEmpCode(strSelectedEmpCode)
                Else
                    ddlEmpCode.Visible = False
                    txtEmpCode.Visible = True
                    txtEmpCode.Text = strSelectedEmpCode
                    If strSelectedEmpCode <> "" Then
                        txtEmpCode.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

    Sub onload_Employee(ByVal pv_strEmpCode As String)
        Dim strOpCd_EmpDet As String = "HR_CLSTRX_EMPLOYEE_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        strParam = pv_strEmpCode & "||||" & strLocation & "||" & strSortExpression & "|"

        Try
            intErrNo = objHR.mtdGetEmployeeDet(strOpCd_EmpDet, strParam, objEmpDetDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_EMPLOYEEDETAILS&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objEmpDetDs.Tables(0).Rows.Count > 0 Then
            EmpCode.Value = objEmpDetDs.Tables(0).Rows(0).Item("EmpCode")
            txtEmpName.Text = objEmpDetDs.Tables(0).Rows(0).Item("EmpName")
            hidEmpName.Value = objEmpDetDs.Tables(0).Rows(0).Item("EmpName")
            BindSex(Trim(objEmpDetDs.Tables(0).Rows(0).Item("Gender")))
            BindMarital(Trim(objEmpDetDs.Tables(0).Rows(0).Item("MaritalStatus")))
            BindRecruitment(Trim(objEmpDetDs.Tables(0).Rows(0).Item("RecruitmentType")))
            BindTaxStatus(Trim(objEmpDetDs.Tables(0).Rows(0).Item("taxstatus")))
            BindAllwStatus(Trim(objEmpDetDs.Tables(0).Rows(0).Item("allwstatus")))
            txtBloodType.Text = Trim(objEmpDetDs.Tables(0).Rows(0).Item("BloodType"))
            txtNewICNo.Text = objEmpDetDs.Tables(0).Rows(0).Item("NewICNo")
            strSelectedNewICType = objEmpDetDs.Tables(0).Rows(0).Item("NewICType")
            txtOldICNo.Text = objEmpDetDs.Tables(0).Rows(0).Item("OldICNo")
            strSelectedOldICType = objEmpDetDs.Tables(0).Rows(0).Item("OldICType")
            txtDOB.Text = Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("DOB"), True)
            strSelectedRace = objEmpDetDs.Tables(0).Rows(0).Item("Race")
            strSelectedReligion = objEmpDetDs.Tables(0).Rows(0).Item("Religion")
            txtState.Text = objEmpDetDs.Tables(0).Rows(0).Item("State")
            strSelectedKoperasiID = objEmpDetDs.Tables(0).Rows(0).Item("KoperasiID")
            txtKoperasiNo.Text = objEmpDetDs.Tables(0).Rows(0).Item("KoperasiNo")
            txtPensionNo.Text = objEmpDetDs.Tables(0).Rows(0).Item("PensionNo")
            If objEmpDetDs.Tables(0).Rows(0).Item("Bumi") = objHR.mtdGetBumiStatus(objHR.EnumBumiStatus.Yes) Then
                cbBumi.Checked = True
            Else
                cbBumi.Checked = False
            End If
            strSelectedNation = objEmpDetDs.Tables(0).Rows(0).Item("Nation")
            txtPassportNo.Text = objEmpDetDs.Tables(0).Rows(0).Item("PassportNo")
            txtPassportExpDate.Text = Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("PassportExpDate"), True)
            txtWorkpassNo.Text = objEmpDetDs.Tables(0).Rows(0).Item("WorkPassNo")
            txtWorkpassExpDate.Text = Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("WorkPassExpDate"), True)
            txtDriveCls.Text = objEmpDetDs.Tables(0).Rows(0).Item("DriveCls")
            txtDriveExpDate.Text = Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("DriveExpDate"), True)
            txtOpClass.Text = objEmpDetDs.Tables(0).Rows(0).Item("OpClass")
            txtOpExpDate.Text = Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("OpExpDate"), True)
            txtResAddress.Value = objEmpDetDs.Tables(0).Rows(0).Item("ResAddress")
            txtPostAddress.Value = objEmpDetDs.Tables(0).Rows(0).Item("PostAddress")
            txtICAddress.Value = objEmpDetDs.Tables(0).Rows(0).Item("ICAddress")
            txtResTel.Text = objEmpDetDs.Tables(0).Rows(0).Item("ResTel")
            txtMobileTel.Text = objEmpDetDs.Tables(0).Rows(0).Item("MobileTel")
            lblStatus.Text = objHR.mtdGetEmpStatus(objEmpDetDs.Tables(0).Rows(0).Item("Status"))
            hidStatus.Value = objEmpDetDs.Tables(0).Rows(0).Item("Status")
            lblDateCreated.Text = objGlobal.GetLongDate(objEmpDetDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objEmpDetDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdateBy.Text = objEmpDetDs.Tables(0).Rows(0).Item("UserName")

            If IsDBNull(objEmpDetDs.Tables(0).Rows(0).Item("BatchNo")) Then
                txtBatchNo.Text = ""
            Else
                txtBatchNo.Text = Trim(objEmpDetDs.Tables(0).Rows(0).Item("BatchNo"))
            End If

            If IsDBNull(objEmpDetDs.Tables(0).Rows(0).Item("LabLicenseNo")) Then
                txtLabLicNo.Text = ""
            Else
                txtLabLicNo.Text = Trim(objEmpDetDs.Tables(0).Rows(0).Item("LabLicenseNo"))
            End If

            If IsDBNull(objEmpDetDs.Tables(0).Rows(0).Item("InsPolicyNo")) Then
                txtInsPolNo.Text = ""
            Else
                txtInsPolNo.Text = Trim(objEmpDetDs.Tables(0).Rows(0).Item("InsPolicyNo"))
            End If

            If IsDBNull(objEmpDetDs.Tables(0).Rows(0).Item("BankGuaNo")) Then
                txtBankGuaNo.Text = ""
            Else
                txtBankGuaNo.Text = Trim(objEmpDetDs.Tables(0).Rows(0).Item("BankGuaNo"))
            End If

            If IsDBNull(objEmpDetDs.Tables(0).Rows(0).Item("MedicalExpDate")) Then
                txtMRExpDate.Text = ""
            Else
                txtMRExpDate.Text = Trim(Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("MedicalExpDate"), True))
            End If

            If IsDBNull(objEmpDetDs.Tables(0).Rows(0).Item("LabLicenseExpDate")) Then
                txtLCExpDate.Text = ""
            Else
                txtLCExpDate.Text = Trim(Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("LabLicenseExpDate"), True))
            End If

            If IsDBNull(objEmpDetDs.Tables(0).Rows(0).Item("InsPolicyExpDate")) Then
                txtIPExpDate.Text = ""
            Else
                txtIPExpDate.Text = Trim(Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("InsPolicyExpDate"), True))
            End If

            If IsDBNull(objEmpDetDs.Tables(0).Rows(0).Item("BankGuaExpDate")) Then
                txtBGExpDate.Text = ""
            Else
                txtBGExpDate.Text = Trim(Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("BankGuaExpDate"), True))
            End If

            Select Case Trim(lblStatus.Text)
                Case objHR.mtdGetEmpStatus(objHR.EnumEmpStatus.Active), objHR.mtdGetEmpStatus(objHR.EnumEmpStatus.Pending)
                    If Trim(lblStatus.Text) = objHR.mtdGetEmpStatus(objHR.EnumEmpStatus.Active) Or _
                        Trim(lblStatus.Text) = objHR.mtdGetEmpStatus(objHR.EnumEmpStatus.Pending) Then
                        ddlEmpCode.Enabled = False
                        txtEmpCode.Enabled = False
                    Else
                        ddlEmpCode.Enabled = True
                        txtEmpCode.Enabled = True
                    End If
                    txtEmpName.Enabled = True
                    ddlSex.Enabled = True
                    ddlMarital.Enabled = True
                    ddlRecruitment.Enabled = True
                    txtNewICNo.Enabled = True
                    ddlNewICType.Enabled = True
                    txtOldICNo.Enabled = True
                    ddlOldICType.Enabled = True
                    txtDOB.Enabled = True
                    ddlRace.Enabled = True
                    txtBloodType.Enabled = True
                    ddlReligion.Enabled = True
                    txtState.Enabled = True
                    cbBumi.Enabled = True
                    ddlNation.Enabled = True
                    txtPassportNo.Enabled = True
                    txtPassportExpDate.ReadOnly = False
                    txtPassportExpDate.Enabled = True
                    txtWorkpassNo.Enabled = True
                    txtWorkpassExpDate.Enabled = True
                    txtDriveCls.Enabled = True
                    txtDriveExpDate.Enabled = True
                    txtOpClass.Enabled = True
                    txtOpExpDate.Enabled = True
                    txtResAddress.Disabled = False
                    txtPostAddress.Disabled = False
                    txtICAddress.Disabled = False
                    txtResTel.Enabled = True
                    txtMobileTel.Enabled = True

                    txtBatchNo.Enabled = True
                    txtLabLicNo.Enabled = True
                    txtInsPolNo.Enabled = True
                    txtBankGuaNo.Enabled = True
                    txtMRExpDate.Enabled = True
                    txtLCExpDate.Enabled = True
                    txtIPExpDate.Enabled = True
                    txtBGExpDate.Enabled = True

                    btnDelete.Visible = True
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    btnSelDOB.Visible = True
                    btnSelPassportExpDate.Visible = True
                    btnSelWorkpassExpDate.Visible = True
                    btnSelDriveExpDate.Visible = True
                    btnSelOpExpDate.Visible = True

                    btnSelMRExpDate.Visible = True
                    btnSelLCExpDate.Visible = True
                    btnSelIPExpDate.Visible = True
                    btnSelBGExpDate.Visible = True

                Case objHR.mtdGetEmpStatus(objHR.EnumEmpStatus.Deleted)
                    ddlEmpCode.Enabled = False
                    txtEmpCode.Enabled = False
                    txtEmpName.Enabled = False
                    ddlSex.Enabled = False
                    ddlMarital.Enabled = False
                    ddlRecruitment.Enabled = False
                    txtNewICNo.Enabled = False
                    ddlNewICType.Enabled = False
                    txtOldICNo.Enabled = False
                    ddlOldICType.Enabled = False
                    txtDOB.Enabled = False
                    ddlRace.Enabled = False
                    txtBloodType.Enabled = False
                    ddlReligion.Enabled = False
                    txtState.Enabled = False
                    cbBumi.Enabled = False
                    ddlNation.Enabled = False
                    txtPassportNo.Enabled = False
                    txtPassportExpDate.Enabled = False
                    txtWorkpassNo.Enabled = False
                    txtWorkpassExpDate.Enabled = False
                    txtDriveCls.Enabled = False
                    txtDriveExpDate.Enabled = False
                    txtOpClass.Enabled = False
                    txtOpExpDate.Enabled = False
                    txtResAddress.Disabled = True
                    txtPostAddress.Disabled = True
                    txtICAddress.Disabled = True
                    txtResTel.Enabled = False
                    txtMobileTel.Enabled = False

                    txtBatchNo.Enabled = False
                    txtLabLicNo.Enabled = False
                    txtInsPolNo.Enabled = False
                    txtBankGuaNo.Enabled = False
                    txtMRExpDate.Enabled = False
                    txtLCExpDate.Enabled = False
                    txtIPExpDate.Enabled = False
                    txtBGExpDate.Enabled = False

                    btnDelete.Visible = False
                    btnUnDelete.Visible = True
                    btnSelDOB.Visible = False
                    btnSelPassportExpDate.Visible = False
                    btnSelWorkpassExpDate.Visible = False
                    btnSelDriveExpDate.Visible = False
                    btnSelOpExpDate.Visible = False

                    btnSelMRExpDate.Visible = False
                    btnSelLCExpDate.Visible = False
                    btnSelIPExpDate.Visible = False
                    btnSelBGExpDate.Visible = False
            End Select
        End If
    End Sub

    Sub onload_LinkButton()
        If hidStatus.Value = 0 Then
            TrLink.Visible = False
        Else
            TrLink.Visible = True
        End If
    End Sub

    Sub BindEmpCode(ByVal pv_strEmpCode As String)
        Dim strOpCd_EmpCode As String = "HR_CLSTRX_EMPCODE_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intEmpCodeIndex As Integer = 0
        Dim dr As DataRow
        Dim blnFound As Boolean = False

        If pv_strEmpCode = Nothing Then
            strParam = pv_strEmpCode & "|" & objHR.EnumEmpCodeStatus.Generated & "|" & strLocation
        Else
            strParam = pv_strEmpCode & "|" & objHR.EnumEmpCodeStatus.Used & "|" & strLocation
        End If

        Try
            intErrNo = objHR.mtdGetEmpCode(strOpCd_EmpCode, strParam, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_EMPLOYEECODE&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
            objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objEmpCodeDs.Tables(0).Rows(intCnt).Item("DispEmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))

            If objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = strSelectedEmpCode Then
                blnFound = True
                intEmpCodeIndex = intCnt + 1
            End If
        Next intCnt

        dr = objEmpCodeDs.Tables(0).NewRow()
        If blnFound Or strSelectedEmpCode = "" Then
            dr("EmpCode") = ""
            dr("DispEmpCode") = "Please Select Employee Code"
        Else
            dr("EmpCode") = strSelectedEmpCode
            dr("DispEmpCode") = strSelectedEmpCode
        End If
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpCode.DataSource = objEmpCodeDs.Tables(0)
        ddlEmpCode.DataTextField = "DispEmpCode"
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intEmpCodeIndex
    End Sub

    Sub BindSex(ByVal pv_strSex As String)
        ddlSex.Items.Clear()
        ddlSex.Items.Add(New ListItem("Please Select Gender", "0"))
        ddlSex.Items.Add(New ListItem(objHR.mtdGetSex(objHR.EnumSex.Male), objHR.EnumSex.Male))
        ddlSex.Items.Add(New ListItem(objHR.mtdGetSex(objHR.EnumSex.Female), objHR.EnumSex.Female))

        If pv_strSex = "" Then
            ddlSex.SelectedIndex = 0
        Else
            ddlSex.SelectedIndex = CInt(pv_strSex)
        End If
    End Sub

    Sub BindMarital(ByVal pv_strMarital As String)
        ddlMarital.Items.Clear()
        ddlMarital.Items.Add(New ListItem("Please Select Marital Status", "0"))
        ddlMarital.Items.Add(New ListItem(objHR.mtdGetMarital(objHR.EnumMarital.Singled), objHR.EnumMarital.Singled))
        ddlMarital.Items.Add(New ListItem(objHR.mtdGetMarital(objHR.EnumMarital.Married), objHR.EnumMarital.Married))
        ddlMarital.Items.Add(New ListItem(objHR.mtdGetMarital(objHR.EnumMarital.Divorced), objHR.EnumMarital.Divorced))

        If pv_strMarital = "" Then
            ddlMarital.SelectedIndex = 0
        Else
            ddlMarital.SelectedIndex = CInt(pv_strMarital)
        End If

    End Sub


    Sub BindRecruitment(ByVal pv_strRecruitment As String)
        ddlRecruitment.Items.Clear()
        ddlRecruitment.Items.Add(New ListItem("Please Select Recruitment Type", "0"))
        ddlRecruitment.Items.Add(New ListItem(objHR.mtdGetRecruitment(objHR.EnumRecruitment.Lokal), objHR.EnumRecruitment.Lokal))
        ddlRecruitment.Items.Add(New ListItem(objHR.mtdGetRecruitment(objHR.EnumRecruitment.Akad), objHR.EnumRecruitment.Akad))
        If pv_strRecruitment = "" Then
            ddlRecruitment.SelectedIndex = 0
        Else
            ddlRecruitment.SelectedIndex = CInt(pv_strRecruitment)
        End If
    End Sub

    Sub BindTaxStatus(ByVal pv_strTaxStatus As String)
        ddltaxstatus.Items.Clear()
        ddltaxstatus.Items.Add(New ListItem("Please Select Tax Status", "0"))
        ddltaxstatus.Items.Add(New ListItem(objHR.mtdGetEnumTaxStatus(objHR.EnumTaxStatus.TK0), objHR.EnumTaxStatus.TK0))
        ddltaxstatus.Items.Add(New ListItem(objHR.mtdGetEnumTaxStatus(objHR.EnumTaxStatus.TK1), objHR.EnumTaxStatus.TK1))
        ddltaxstatus.Items.Add(New ListItem(objHR.mtdGetEnumTaxStatus(objHR.EnumTaxStatus.TK2), objHR.EnumTaxStatus.TK2))
        ddltaxstatus.Items.Add(New ListItem(objHR.mtdGetEnumTaxStatus(objHR.EnumTaxStatus.TK3), objHR.EnumTaxStatus.TK3))
        ddltaxstatus.Items.Add(New ListItem(objHR.mtdGetEnumTaxStatus(objHR.EnumTaxStatus.K0), objHR.EnumTaxStatus.K0))
        ddltaxstatus.Items.Add(New ListItem(objHR.mtdGetEnumTaxStatus(objHR.EnumTaxStatus.K1), objHR.EnumTaxStatus.K1))
        ddltaxstatus.Items.Add(New ListItem(objHR.mtdGetEnumTaxStatus(objHR.EnumTaxStatus.K2), objHR.EnumTaxStatus.K2))
        ddltaxstatus.Items.Add(New ListItem(objHR.mtdGetEnumTaxStatus(objHR.EnumTaxStatus.K3), objHR.EnumTaxStatus.K3))
        If pv_strTaxStatus = "" Then
            ddltaxstatus.SelectedIndex = 0
        Else
            ddltaxstatus.SelectedIndex = CInt(pv_strTaxStatus) + 1
        End If
    End Sub

    Sub BindAllwStatus(ByVal pv_strallwStatus As String)
        ddlallwstatus.Items.Clear()
        ddlallwstatus.Items.Add(New ListItem("Please Select Allowance Status", "0"))
        ddlallwstatus.Items.Add(New ListItem(objHR.mtdGetEnumAllwStatus(objHR.EnumAllwStatus.TK0), objHR.EnumAllwStatus.TK0))
        ddlallwstatus.Items.Add(New ListItem(objHR.mtdGetEnumAllwStatus(objHR.EnumAllwStatus.TK1), objHR.EnumAllwStatus.TK1))
        ddlallwstatus.Items.Add(New ListItem(objHR.mtdGetEnumAllwStatus(objHR.EnumAllwStatus.TK2), objHR.EnumAllwStatus.TK2))
        ddlallwstatus.Items.Add(New ListItem(objHR.mtdGetEnumAllwStatus(objHR.EnumAllwStatus.TK3), objHR.EnumAllwStatus.TK3))
        ddlallwstatus.Items.Add(New ListItem(objHR.mtdGetEnumAllwStatus(objHR.EnumAllwStatus.K0), objHR.EnumAllwStatus.K0))
        ddlallwstatus.Items.Add(New ListItem(objHR.mtdGetEnumAllwStatus(objHR.EnumAllwStatus.K1), objHR.EnumAllwStatus.K1))
        ddlallwstatus.Items.Add(New ListItem(objHR.mtdGetEnumAllwStatus(objHR.EnumAllwStatus.K2), objHR.EnumAllwStatus.K2))
        ddlallwstatus.Items.Add(New ListItem(objHR.mtdGetEnumAllwStatus(objHR.EnumAllwStatus.K3), objHR.EnumAllwStatus.K3))
        If pv_strallwStatus = "" Then
            ddlallwstatus.SelectedIndex = 0
        Else
            ddlallwstatus.SelectedIndex = CInt(pv_strallwStatus) + 1
        End If
    End Sub



    Sub BindICType()
        Dim strOpCd_ICType As String = "HR_CLSSETUP_ICTYPE_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intNewICTypeIndex As Integer = 0
        Dim intOldICTypeIndex As Integer = 0
        Dim intKoperasiIDIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|" & "AND IC.Status LIKE '" & objHRSetup.EnumICTypeStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_ICType, strParam, objHRSetup.EnumHRMasterType.ICType, objICTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_NEWICTYPE&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objICTypeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objICTypeDs.Tables(0).Rows.Count - 1
                objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode") = Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode"))
                objICTypeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode")) & " (" & _
                                                                         Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode") = strSelectedNewICType Then
                    intNewICTypeIndex = intCnt + 1
                End If

                If objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode") = strSelectedOldICType Then
                    intOldICTypeIndex = intCnt + 1
                End If

            Next
        End If

        dr = objICTypeDs.Tables(0).NewRow()
        dr("ICTypeCode") = ""
        dr("Description") = "Please Select New IC Type"
        objICTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlNewICType.DataSource = objICTypeDs.Tables(0)
        ddlNewICType.DataTextField = "Description"
        ddlNewICType.DataValueField = "ICTypeCode"
        ddlNewICType.DataBind()
        ddlNewICType.SelectedIndex = intNewICTypeIndex

        ddlOldICType.DataSource = objICTypeDs.Tables(0)
        ddlOldICType.DataTextField = "Description"
        ddlOldICType.DataValueField = "ICTypeCode"
        ddlOldICType.DataBind()
        ddlOldICType.SelectedIndex = intOldICTypeIndex

    End Sub



    Sub BindRace()
        Dim strOpCd_Race As String = "HR_CLSSETUP_RACE_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intRaceIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|" & "AND RA.Status LIKE '" & objHRSetup.EnumRaceStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Race, strParam, objHRSetup.EnumHRMasterType.Race, objRaceDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_RACE&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objRaceDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRaceDs.Tables(0).Rows.Count - 1
                objRaceDs.Tables(0).Rows(intCnt).Item("RaceCode") = Trim(objRaceDs.Tables(0).Rows(intCnt).Item("RaceCode"))
                objRaceDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objRaceDs.Tables(0).Rows(intCnt).Item("RaceCode")) & " (" & _
                                                                       Trim(objRaceDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If objRaceDs.Tables(0).Rows(intCnt).Item("RaceCode") = strSelectedRace Then
                    intRaceIndex = intCnt + 1
                End If
            Next
        End If

        dr = objRaceDs.Tables(0).NewRow()
        dr("RaceCode") = ""
        dr("Description") = "Please Select Race"
        objRaceDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlRace.DataSource = objRaceDs.Tables(0)
        ddlRace.DataTextField = "Description"
        ddlRace.DataValueField = "RaceCode"
        ddlRace.DataBind()
        ddlRace.SelectedIndex = intRaceIndex
    End Sub

    Sub BindReligion()
        Dim strOpCd_Religion As String = "HR_CLSSETUP_RELIGION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intReligionIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|" & "AND REL.Status LIKE '" & objHRSetup.EnumReligionStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Religion, strParam, objHRSetup.EnumHRMasterType.Religion, objReligionDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_RELIGION&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objReligionDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objReligionDs.Tables(0).Rows.Count - 1
                objReligionDs.Tables(0).Rows(intCnt).Item("ReligionCode") = Trim(objReligionDs.Tables(0).Rows(intCnt).Item("ReligionCode"))
                objReligionDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objReligionDs.Tables(0).Rows(intCnt).Item("ReligionCode")) & " (" & _
                                                                           Trim(objReligionDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If objReligionDs.Tables(0).Rows(intCnt).Item("ReligionCode") = strSelectedReligion Then
                    intReligionIndex = intCnt + 1
                End If
            Next
        End If

        dr = objReligionDs.Tables(0).NewRow()
        dr("ReligionCode") = ""
        dr("Description") = "(please select one)"
        objReligionDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlReligion.DataSource = objReligionDs.Tables(0)
        ddlReligion.DataTextField = "Description"
        ddlReligion.DataValueField = "ReligionCode"
        ddlReligion.DataBind()
        ddlReligion.SelectedIndex = intReligionIndex
    End Sub

    Sub BindNation()
        Dim strOpCd_Nation As String = "HR_CLSSETUP_NATION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intNationIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|" & "AND NA.Status LIKE '" & objHRSetup.EnumNationalityStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Nation, strParam, objHRSetup.EnumHRMasterType.Nationality, objNationDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_NATION&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objNationDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objNationDs.Tables(0).Rows.Count - 1
                objNationDs.Tables(0).Rows(intCnt).Item("CountryCode") = Trim(objNationDs.Tables(0).Rows(intCnt).Item("CountryCode"))
                objNationDs.Tables(0).Rows(intCnt).Item("CountryDesc") = Trim(objNationDs.Tables(0).Rows(intCnt).Item("CountryCode")) & " (" & _
                                                                         Trim(objNationDs.Tables(0).Rows(intCnt).Item("CountryDesc")) & ")"

                If objNationDs.Tables(0).Rows(intCnt).Item("CountryCode") = strSelectedNation Then
                    intNationIndex = intCnt + 1
                End If
            Next
        End If

        dr = objNationDs.Tables(0).NewRow()
        dr("CountryCode") = ""
        dr("CountryDesc") = "Please Select Nationality"
        objNationDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlNation.DataSource = objNationDs.Tables(0)
        ddlNation.DataTextField = "CountryDesc"
        ddlNation.DataValueField = "CountryCode"
        ddlNation.DataBind()
        ddlNation.SelectedIndex = intNationIndex
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_GRList.aspx")
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
        End If
    End Function

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objTmpDs As New DataSet()
        Dim strOpCd_Get As String = "HR_CLSTRX_EMPLOYEE_GET"
        Dim strOpCd_Upd As String = "HR_CLSTRX_EMPLOYEE_UPD"
        Dim strOpCd_Add As String = "HR_CLSTRX_EMPLOYEE_ADD"
        Dim strOpCd_EmpCode_Upd As String = "HR_CLSTRX_EMPCODE_UPD"
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strEmpCode As String
        Dim strEmpName As String = txtEmpName.Text
        Dim strSex As String = ddlSex.SelectedItem.Value
        Dim strMarital As String = ddlMarital.SelectedItem.Value
        Dim strRecruitment As String = ddlRecruitment.SelectedItem.Value
        Dim strNewICNo As String = txtNewICNo.Text
        Dim strNewICType As String = ddlNewICType.SelectedItem.Value
        Dim strOldICNo As String = txtOldICNo.Text
        Dim strOldICType As String = ddlOldICType.SelectedItem.Value
        Dim strDOB As String = txtDOB.Text
        Dim strRace As String = ddlRace.SelectedItem.Value
        Dim strBloodType As String = txtBloodType.Text
        Dim strReligion As String = ddlReligion.SelectedItem.Value
        Dim strState As String = txtState.Text
        Dim strBumi As String = IIf(cbBumi.Checked = True, objHR.mtdGetBumiStatus(objHR.EnumBumiStatus.Yes), objHR.mtdGetBumiStatus(objHR.EnumBumiStatus.No))
        Dim strNation As String = ddlNation.SelectedItem.Value
        Dim strPassportNo As String = txtPassportNo.Text
        Dim strPassportExpDate As String = txtPassportExpDate.Text
        Dim strWorkpassNo As String = txtWorkpassNo.Text
        Dim strWorkpassExpDate As String = txtWorkpassExpDate.Text
        Dim strDriveCls As String = txtDriveCls.Text
        Dim strDriveExpDate As String = txtDriveExpDate.Text
        Dim strOpClass As String = txtOpClass.Text
        Dim strOpExpDate As String = txtOpExpDate.Text
        Dim strResAddress As String = txtResAddress.Value
        Dim strPostAddress As String = txtPostAddress.Value
        Dim strICAddress As String = txtICAddress.Value
        Dim strResTel As String = txtResTel.Text
        Dim strMobileTel As String = txtMobileTel.Text
        Dim strStatus As String = lblStatus.Text

        Dim strBatchNo As String = txtBatchNo.Text
        Dim strLabLicNo As String = txtLabLicNo.Text
        Dim strInsPolNo As String = txtInsPolNo.Text
        Dim strBankGuaNo As String = txtBankGuaNo.Text
        Dim strMRExpDate As String = txtMRExpDate.Text
        Dim strLCExpDate As String = txtLCExpDate.Text
        Dim strIPExpDate As String = txtIPExpDate.Text
        Dim strBGExpDate As String = txtBGExpDate.Text
        Dim blnIsUpdate As Boolean = False

        Dim strKoperasiNo As String = txtKoperasiNo.Text
        Dim strKoperasiID As String = ddlKoperasiID.SelectedItem.Value
        Dim strPensionNo As String = txtPensionNo.Text

        If ddlEmpCode.Visible = True Then
            strEmpCode = ddlEmpCode.SelectedItem.Value
        Else
            If Left(Trim(txtEmpCode.Text), 3) = Trim(strLocation) Then
                strEmpCode = Trim(txtEmpCode.Text)
            Else
                strEmpCode = Trim(strLocation) & RTrim(txtEmpCode.Text)
            End If
        End If

        If strEmpCode = "" Then
            lblErrEmpCode.Visible = True
            Exit Sub
        Else
            Try
                strParam = strEmpCode & "||||" & strLocation & "||" & strSortExpression & "|"
                intErrNo = objHR.mtdGetEmployeeDet(strOpCd_Get, strParam, objTmpDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_EMPLOYEEDETAILS2&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
            End Try
            If objTmpDs.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To objTmpDs.Tables(0).Rows.Count - 1
                    If objTmpDs.Tables(0).Rows(intCnt).Item("EmpCode").Trim() = strEmpCode Then
                        lblErrDupEmpCode.Visible = False
                        revEmpCode1.Visible = False
                        blnIsUpdate = True
                    End If
                Next
            End If
        End If

        If strSex = 0 Then
            lblErrSex.Visible = True
            Exit Sub
        End If

        If strMarital = 0 Then
            lblErrMarital.Visible = True
            Exit Sub
        End If

        If strNation = "" Then
            lblErrNation.Visible = True
            Exit Sub
        End If

        If strRecruitment = "0" Then
            lblErrRecruitment.Visible = True
            Exit Sub
        End If

        If strRace = "" Then
            lblErrRace.Visible = True
            Exit Sub
        End If

        If strDOB <> "" Then
            strDOB = Date_Validation(strDOB, False)
            If strDOB = "" Then
                lblErrDOB.Visible = True
                lblErrDOB.Text = lblErrDOB.Text & strAcceptFormat
                Exit Sub
            End If
        End If

        If strPassportExpDate <> "" Then
            strPassportExpDate = Date_Validation(strPassportExpDate, False)
            If strPassportExpDate = "" Then
                lblErrPassportExpDate.Visible = True
                lblErrPassportExpDate.Text = lblErrPassportExpDate.Text & strAcceptFormat
                Exit Sub
            End If
        End If

        If strWorkpassExpDate <> "" Then
            strWorkpassExpDate = Date_Validation(strWorkpassExpDate, False)
            If strWorkpassExpDate = "" Then
                lblErrWorkpassExpDate.Visible = True
                lblErrWorkpassExpDate.Text = lblErrWorkpassExpDate.Text & strAcceptFormat
                Exit Sub
            End If
        End If

        If strDriveExpDate <> "" Then
            strDriveExpDate = Date_Validation(strDriveExpDate, False)
            If strDriveExpDate = "" Then
                lblErrDriveExpDate.Visible = True
                lblErrDriveExpDate.Text = lblErrDriveExpDate.Text & strAcceptFormat
                Exit Sub
            End If
        End If

        If strOpExpDate <> "" Then
            strOpExpDate = Date_Validation(strOpExpDate, False)
            If strOpExpDate = "" Then
                lblErrOpExpDate.Visible = True
                lblErrOpExpDate.Text = lblErrOpExpDate.Text & strAcceptFormat
                Exit Sub
            End If
        End If

        If strMRExpDate <> "" Then
            strMRExpDate = Date_Validation(strMRExpDate, False)
            If strMRExpDate = "" Then
                lblErrMRExpDate.Visible = True
                lblErrMRExpDate.Text = lblErrMRExpDate.Text & strAcceptFormat
                Exit Sub
            End If
        End If

        If strLCExpDate <> "" Then
            strLCExpDate = Date_Validation(strLCExpDate, False)
            If strLCExpDate = "" Then
                lblErrLCExpDate.Visible = True
                lblErrLCExpDate.Text = lblErrLCExpDate.Text & strAcceptFormat
                Exit Sub
            End If
        End If

        If strIPExpDate <> "" Then
            strIPExpDate = Date_Validation(strIPExpDate, False)
            If strIPExpDate = "" Then
                lblErrIPExpDate.Visible = True
                lblErrIPExpDate.Text = lblErrIPExpDate.Text & strAcceptFormat
                Exit Sub
            End If
        End If

        If strBGExpDate <> "" Then
            strBGExpDate = Date_Validation(strBGExpDate, False)
            If strBGExpDate = "" Then
                lblErrBGExpDate.Visible = True
                lblErrBGExpDate.Text = lblErrBGExpDate.Text & strAcceptFormat
                Exit Sub
            End If
        End If

        If Len(strResAddress) > 512 Then
            lblErrResAddress.Visible = True
            Exit Sub
        ElseIf Len(strPostAddress) > 512 Then
            lblErrPostAddress.Visible = True
            Exit Sub
        ElseIf Len(strICAddress) > 512 Then
            lblErrICAddress.Visible = True
            Exit Sub
        End If


        If ddltaxstatus.SelectedItem.Value = "" Then
            lblErrTaxStatus.Visible = True
            Exit Sub
        Else
            lblErrTaxStatus.Visible = False
        End If
        If ddlallwstatus.SelectedItem.Value = "" Then
            lblErrAllwStatus.Visible = True
            Exit Sub
        Else
            lblErrAllwStatus.Visible = False
        End If


        strParam = strEmpCode & "|" & strEmpName & "|" & strSex & "|" & strMarital & "|" & _
                   strNewICNo & "|" & strNewICType & "|" & strOldICNo & "|" & strOldICType & "|" & _
                   strDOB & "|" & strRace & "|" & strReligion & "|" & strBloodType & "|" & strBumi & "|" & _
                   strState & "|" & strNation & "|" & strPassportNo & "|" & strPassportExpDate & "|" & _
                   strWorkpassNo & "|" & strWorkpassExpDate & "|" & strDriveCls & "|" & strDriveExpDate & "|" & _
                   strOpClass & "|" & strOpExpDate & "|" & strResAddress & "|" & strPostAddress & "|" & _
                   strICAddress & "|" & strResTel & "|" & strMobileTel & "|" & _
                   IIf(strStatus = "", objHR.EnumEmpStatus.Pending, "") & "|" & _
                   strBatchNo & "|" & strMRExpDate & "|" & strLabLicNo & "|" & _
                   strLCExpDate & "|" & strInsPolNo & "|" & strIPExpDate & "|" & _
                   strBankGuaNo & "|" & strBGExpDate & "|" & strKoperasiID & "|" & _
                   strKoperasiNo & "|" & strPensionNo & "|" & strRecruitment & "|" & Trim(ddltaxstatus.SelectedItem.Value) & "|" & _
                   Trim(ddlallwstatus.SelectedItem.Value)


        Try
            intErrNo = objHR.mtdUpdEmployeeDet(strOpCd_Upd, _
                                               strOpCd_Add, _
                                               strOpCd_EmpCode_Upd, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               strParam, _
                                               blnIsUpdate)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SAVE_EMPLOYEE&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try
        onload_Employee(strEmpCode)
        onload_LinkButton()
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Status As String = "HR_CLXTRX_EMPLOYEE_STATUS_UPD"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strEmpCode As String

        If ddlEmpCode.Visible = True Then
            strEmpCode = ddlEmpCode.SelectedItem.Value
        Else
            strEmpCode = txtEmpCode.Text
        End If

        strParam = strEmpCode & "|" & objHR.EnumEmpStatus.Deleted
        Try
            intErrNo = objHR.mtdUpdEmployeeDetStatus(strOpCd_Status, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strParam)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_DELETE_EMPLOYEE&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try
        lblStatus.Text = objHR.mtdGetEmpStatus(objHR.EnumEmpStatus.Deleted)
        btnDelete.Visible = False
        btnUnDelete.Visible = True
        onload_Employee(strEmpCode)
        onload_LinkButton()
    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetPay As String = "HR_CLSTRX_PAYROLL_GET"
        Dim strOpCd_GetEmp As String = "HR_CLSTRX_EMPLOYMENT_GET"
        Dim strOpCd_GetStat As String = "HR_CLSTRX_STATUTORY_GET"
        Dim strOpCd_Get As String = "HR_CLSTRX_EMPLOYEE_GET"
        Dim strOpCd_Status As String = "HR_CLXTRX_EMPLOYEE_STATUS_UPD"
        Dim objTmpDs As New DataSet()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strEmpCode As String

        If ddlEmpCode.Visible = True Then
            strEmpCode = ddlEmpCode.SelectedItem.Value
        Else
            strEmpCode = txtEmpCode.Text
        End If

        strParam = strEmpCode

        Try
            intErrNo = objHR.mtdUpdEmpStatusUnDel(strOpCd_GetPay, _
                                                   strOpCd_GetEmp, _
                                                   strOpCd_GetStat, _
                                                   strOpCd_Status, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_UPD_EMPLOYEE_STATUS_FOR_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        btnDelete.Visible = True
        btnUnDelete.Visible = False
        onload_Employee(strEmpCode)
        onload_LinkButton()
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeList.aspx?redirect=" & lblRedirect.Text)
    End Sub

    Private Sub lbPayroll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbPayroll.Click
        Response.Redirect("HR_trx_EmployeePay.aspx?redirect=emppay&EmpCode=" & strSelectedEmpCode & "&EmpName=" & hidEmpName.Value & "&EmpStatus=" & hidStatus.Value)
    End Sub

    Private Sub lbEmployment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEmployment.Click
        Response.Redirect("HR_trx_EmployeeEmp.aspx?redirect=empemp&EmpCode=" & strSelectedEmpCode & "&EmpName=" & hidEmpName.Value & "&EmpStatus=" & hidStatus.Value)
    End Sub

    Private Sub lbStatutory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbStatutory.Click
        Response.Redirect("HR_trx_EmployeeStat.aspx?redirect=empstat&EmpCode=" & strSelectedEmpCode & "&EmpName=" & hidEmpName.Value & "&EmpStatus=" & hidStatus.Value)
    End Sub

    Private Sub lbFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFamily.Click
        Response.Redirect("HR_trx_EmployeeFamList.aspx?redirect=empfam&EmpCode=" & strSelectedEmpCode & "&EmpName=" & hidEmpName.Value & "&EmpStatus=" & hidStatus.Value)
    End Sub

    Private Sub lbQualific_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbQualific.Click
        Response.Redirect("HR_trx_EmployeeQlfList.aspx?redirect=empqlf&EmpCode=" & strSelectedEmpCode & "&EmpName=" & hidEmpName.Value & "&EmpStatus=" & hidStatus.Value)
    End Sub

    Private Sub lbSkill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSkill.Click
        Response.Redirect("HR_trx_EmployeeSkillList.aspx?redirect=empskill&EmpCode=" & strSelectedEmpCode & "&EmpName=" & hidEmpName.Value & "&EmpStatus=" & hidStatus.Value)
    End Sub

    Private Sub lbCareerProg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCareerProg.Click
        Response.Redirect("HR_trx_EmployeeCPList.aspx?redirect=empcp&EmpCode=" & strSelectedEmpCode & "&EmpName=" & hidEmpName.Value & "&EmpStatus=" & hidStatus.Value)
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblReligion.Text = GetCaption(objLangCap.EnumLangCap.Religion)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/hr_trx_employeelist.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function


    Sub BindKoperasiID()
        Dim strOpCd_ICType As String = "HR_CLSSETUP_ICTYPE_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intNewICTypeIndex As Integer = 0
        Dim intOldICTypeIndex As Integer = 0
        Dim intKoperasiIDIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|" & "AND IC.Status LIKE '" & objHRSetup.EnumICTypeStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_ICType, strParam, objHRSetup.EnumHRMasterType.ICType, objICTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_NEWICTYPE&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objICTypeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objICTypeDs.Tables(0).Rows.Count - 1
                objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode") = Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode"))
                objICTypeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode")) & " (" & _
                                                                         Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode") = strSelectedKoperasiID Then
                    intKoperasiIDIndex = intCnt + 1
                End If
            Next
        End If

        dr = objICTypeDs.Tables(0).NewRow()
        dr("ICTypeCode") = ""
        dr("Description") = "Please Select New Koperasi ID"
        objICTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlKoperasiID.DataSource = objICTypeDs.Tables(0)
        ddlKoperasiID.DataTextField = "Description"
        ddlKoperasiID.DataValueField = "ICTypeCode"
        ddlKoperasiID.DataBind()
        ddlKoperasiID.SelectedIndex = intKoperasiIDIndex
    End Sub


End Class
