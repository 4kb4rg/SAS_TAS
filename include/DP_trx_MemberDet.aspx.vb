
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

Public Class DP_trx_MemberDet : Inherits Page

    Protected WithEvents txtMemCode As TextBox
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtMemName As TextBox
    Protected WithEvents ddlUnit As DropDownList
    Protected WithEvents ddlSex As DropDownList
    Protected WithEvents txtDOB As TextBox
    Protected WithEvents txtAge As TextBox
    Protected WithEvents txtWorkDate As TextBox
    Protected WithEvents txtWorkPeriod As TextBox
    Protected WithEvents txtMemDate As TextBox
    Protected WithEvents txtMemPeriod As TextBox
    Protected WithEvents txtPromDate As TextBox
    Protected WithEvents ddlGol As DropDownList
    Protected WithEvents lblPhDP As Label
    Protected WithEvents lblEmpeCo As Label
    Protected WithEvents lblEmpeCoAmount As Label
    Protected WithEvents lblEmprCo As Label
    Protected WithEvents lblEmprCoAmount As Label
    Protected WithEvents lblTotCoAmount As Label
    Protected WithEvents ddlMarital As DropDownList
    Protected WithEvents ddlReligion As DropDownList
    Protected WithEvents ddltaxstatus As DropDownList
    Protected WithEvents ddlRace As DropDownList
    Protected WithEvents txtNPWP As TextBox
    Protected WithEvents ddlNation As DropDownList
    Protected WithEvents ddlICType As DropDownList
    Protected WithEvents txtICNo As TextBox
    Protected WithEvents txtResAddress As HtmlTextArea
    Protected WithEvents txtICAddress As HtmlTextArea
    Protected WithEvents txtResTel As TextBox
    Protected WithEvents txtMobileTel As TextBox
    Protected WithEvents txtPostAddress As HtmlTextArea
    Protected WithEvents txtFamName As TextBox
    Protected WithEvents ddlRelationship As DropDownList
    Protected WithEvents ddlGender As DropDownList
    Protected WithEvents txtFamDOB As TextBox
    Protected WithEvents txtSKNo As TextBox
    Protected WithEvents txtSKDate As TextBox
    Protected WithEvents txtNotes As TextBox
    Protected WithEvents txtFile As TextBox
    Protected WithEvents lblTotEmpeConAmount As Label
    Protected WithEvents lblTotEmprConAmount As Label
    Protected WithEvents lblTotConAmount As Label
    Protected WithEvents txtRemark As TextBox

    Protected WithEvents MemCode As HtmlInputHidden
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
    Protected WithEvents lblErrTaxStatus As Label
    Protected WithEvents lblErrRace As Label
    Protected WithEvents lblErrNation As Label
    Protected WithEvents lblReligion As Label
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnSelDOB As Image
    Protected WithEvents btnSelWorkDate As Image
    Protected WithEvents btnSelMemDate As Image
    Protected WithEvents btnSelPromDate As Image
    Protected WithEvents btnSelFamDOB As Image
    Protected WithEvents btnSelSKDate As Image
    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents hidMemName As HtmlInputHidden
    Protected WithEvents hidStatus As HtmlInputHidden
    Protected WithEvents DgFamily As DataGrid

    Dim objHR As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim objEmpCodeDs As New Object()
    Dim objEmpDetDs As New Object()
    Dim objICTypeDs As New Object()
    Dim objRaceDs As New Object()
    Dim objReligionDs As New Object()
    Dim objNationDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long
    Dim intConfigSetting As Integer

    Dim strSelectedMemCode As String = ""
    Dim strSelectedICType As String = ""
    Dim strSelectedRace As String = ""
    Dim strSelectedReligion As String = ""
    Dim strSelectedNation As String = ""
    Dim strSelectedGol As String = ""
    Dim strSelectedUnit As String = ""
    Dim strSortExpression As String = "MemCode"
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
            lblErrDupEmpCode.Visible = False
            revEmpCode1.Visible = False
            btnDelete.Visible = False
            btnUnDelete.Visible = False

            lblRedirect.Text = Request.QueryString("redirect")
            strSelectedMemCode = Trim(IIf(Request.QueryString("NoPes") <> "", Request.QueryString("NoPes"), Request.Form("NoPes")))

            If Not IsPostBack Then
                If strSelectedMemCode <> "" Then
                    MemCode.Value = strSelectedMemCode
                    onload_Member(strSelectedMemCode)
                    onLoad_DisplayFamily(strSelectedMemCode)
                Else
                    BindSex("0")
                    BindMarital("0")
                    BindTaxStatus("0")
                End If
                BindICType()
                BindRace()
                BindReligion()
                BindNation()
                BindGolongan("")
                BindUnit("")
                onload_LinkButton()

                If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoEmpCode), intConfigSetting) = True Then
                    txtMemCode.Visible = False
                Else
                    txtMemCode.Visible = True
                    txtMemCode.Text = strSelectedMemCode
                    If strSelectedMemCode <> "" Then
                        txtMemCode.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

    Sub onload_Member(ByVal pv_strMemCode As String)
        Dim strOpCd_Get As String = "DP_CLSTRX_MEMBER_DETAIL_GET"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim SearchStr As String

        SearchStr = ""

        If Not MemCode.Value = "" Then SearchStr = SearchStr & " AND A.NoPes = '" & Trim(MemCode.Value) & "' "
        strParamName = "STRSEARCH"
        strParamValue = "WHERE A.NoPesp = '' " & SearchStr

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                                strParamName, _
                                                strParamValue, _
                                                objEmpDetDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_EMPLOYEEDETAILS&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvnotelist.aspx")
        End Try

        If objEmpDetDs.Tables(0).Rows.Count > 0 Then
            txtMemCode.Text = objEmpDetDs.Tables(0).Rows(0).Item("NoPes")
            MemCode.Value = objEmpDetDs.Tables(0).Rows(0).Item("NoPes")
            strSelectedUnit = objEmpDetDs.Tables(0).Rows(0).Item("Unit")
            BindUnit(strSelectedUnit)
            txtEmpCode.Text = objEmpDetDs.Tables(0).Rows(0).Item("NoPeg")
            txtMemName.Text = objEmpDetDs.Tables(0).Rows(0).Item("Nam")
            hidMemName.Value = objEmpDetDs.Tables(0).Rows(0).Item("Nam")
            BindSex(Trim(objEmpDetDs.Tables(0).Rows(0).Item("Sex")))
            BindMarital(Trim(objEmpDetDs.Tables(0).Rows(0).Item("Stat")))
            BindTaxStatus(Trim(objEmpDetDs.Tables(0).Rows(0).Item("Stat")))
            'txtICNo.Text = objEmpDetDs.Tables(0).Rows(0).Item("ICNo")
            'strSelectedICType = objEmpDetDs.Tables(0).Rows(0).Item("ICType")
            txtDOB.Text = Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("lah"), True)
            txtWorkDate.Text = Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("mas"), True)
            txtMemDate.Text = Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("kep"), True)
            txtPromDate.Text = Date_Validation(objEmpDetDs.Tables(0).Rows(0).Item("tglmutasi"), True)
            txtAge.Text = objEmpDetDs.Tables(0).Rows(0).Item("Age")
            txtWorkPeriod.Text = objEmpDetDs.Tables(0).Rows(0).Item("WorkPeriod")
            txtMemPeriod.Text = objEmpDetDs.Tables(0).Rows(0).Item("MemPeriod")
            strSelectedGol = objEmpDetDs.Tables(0).Rows(0).Item("Gol")
            BindGolongan(strSelectedGol)
            lblPhDP.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objEmpDetDs.Tables(0).Rows(0).Item("PhDP"), 2), 2)
            lblEmpeCo.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objEmpDetDs.Tables(0).Rows(0).Item("prs_pst"), 2), 2)
            lblEmprCo.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objEmpDetDs.Tables(0).Rows(0).Item("prs_prh"), 2), 2)
            lblEmpeCoAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objEmpDetDs.Tables(0).Rows(0).Item("i_pst"), 2), 2)
            lblEmprCoAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objEmpDetDs.Tables(0).Rows(0).Item("i_prh"), 2), 2)
            lblTotCoAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objEmpDetDs.Tables(0).Rows(0).Item("t_iuran"), 2), 2)
            lblTotEmpeConAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objEmpDetDs.Tables(0).Rows(0).Item("s_pst_bi"), 2), 2)
            lblTotEmprConAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objEmpDetDs.Tables(0).Rows(0).Item("s_prh_bi"), 2), 2)
            lblTotConAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objEmpDetDs.Tables(0).Rows(0).Item("t_bi"), 2), 2)
            'strSelectedRace = objEmpDetDs.Tables(0).Rows(0).Item("Race")
            'strSelectedReligion = objEmpDetDs.Tables(0).Rows(0).Item("Religion")
            'strSelectedNation = objEmpDetDs.Tables(0).Rows(0).Item("Nation")
            'txtResAddress.Value = objEmpDetDs.Tables(0).Rows(0).Item("ResAddress")
            'txtPostAddress.Value = objEmpDetDs.Tables(0).Rows(0).Item("PostAddress")
            'txtICAddress.Value = objEmpDetDs.Tables(0).Rows(0).Item("ICAddress")
            'txtResTel.Text = objEmpDetDs.Tables(0).Rows(0).Item("ResTel")
            'txtMobileTel.Text = objEmpDetDs.Tables(0).Rows(0).Item("MobileTel")
            'lblStatus.Text = objHR.mtdGetEmpStatus(objEmpDetDs.Tables(0).Rows(0).Item("Status"))
            'hidStatus.Value = objEmpDetDs.Tables(0).Rows(0).Item("Status")
            'lblDateCreated.Text = objGlobal.GetLongDate(objEmpDetDs.Tables(0).Rows(0).Item("CreateDate"))
            'lblLastUpdate.Text = objGlobal.GetLongDate(objEmpDetDs.Tables(0).Rows(0).Item("UpdateDate"))
            'lblUpdateBy.Text = objEmpDetDs.Tables(0).Rows(0).Item("UserName")

            Select Case Trim(lblStatus.Text)
                Case objHR.mtdGetEmpStatus(objHR.EnumEmpStatus.Active), objHR.mtdGetEmpStatus(objHR.EnumEmpStatus.Pending)
                    If Trim(lblStatus.Text) = objHR.mtdGetEmpStatus(objHR.EnumEmpStatus.Active) Or _
                        Trim(lblStatus.Text) = objHR.mtdGetEmpStatus(objHR.EnumEmpStatus.Pending) Then
                        txtMemCode.Enabled = False
                    Else
                        txtMemCode.Enabled = True
                    End If
                    txtMemName.Enabled = True
                    ddlSex.Enabled = True
                    ddlMarital.Enabled = True
                    txtICNo.Enabled = True
                    ddlICType.Enabled = True
                    txtDOB.Enabled = True
                    ddlRace.Enabled = True
                    ddlReligion.Enabled = True
                    ddlNation.Enabled = True
                    txtResAddress.Disabled = False
                    txtPostAddress.Disabled = False
                    txtICAddress.Disabled = False
                    txtResTel.Enabled = True
                    txtMobileTel.Enabled = True

                    btnDelete.Visible = True
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    btnSelDOB.Visible = True

                Case objHR.mtdGetEmpStatus(objHR.EnumEmpStatus.Deleted)
                    txtMemCode.Enabled = False
                    txtMemName.Enabled = False
                    ddlSex.Enabled = False
                    ddlMarital.Enabled = False
                    txtICNo.Enabled = False
                    ddlICType.Enabled = False
                    txtDOB.Enabled = False
                    ddlRace.Enabled = False
                    ddlReligion.Enabled = False
                    ddlNation.Enabled = False
                    txtResAddress.Disabled = True
                    txtPostAddress.Disabled = True
                    txtICAddress.Disabled = True
                    txtResTel.Enabled = False
                    txtMobileTel.Enabled = False

                    btnDelete.Visible = False
                    btnUnDelete.Visible = True
                    btnSelDOB.Visible = False
            End Select
        End If
    End Sub

    Sub onLoad_DisplayFamily(ByVal pv_strMemCode As String)
        Dim strOpCd_Get As String = "DP_CLSTRX_FAMILY_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim lbEditButton As LinkButton
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbl As Label
        Dim SearchStr As String

        SearchStr = ""
        If Not MemCode.Value = "" Then SearchStr = SearchStr & " NoPes = '" & Trim(MemCode.Value) & "' "
        strParamName = "STRSEARCH"
        strParamValue = "WHERE " & SearchStr

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                                strParamName, _
                                                strParamValue, _
                                                objEmpDetDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_EMPLOYEEDETAILS&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvnotelist.aspx")
        End Try

        DgFamily.DataSource = objEmpDetDs.Tables(0)
        DgFamily.DataBind()
    End Sub

    Sub onload_LinkButton()
        'If hidStatus.Value = 0 Then
        '    TrLink.Visible = False
        'Else
        '    TrLink.Visible = True
        'End If
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

        ddlGender.Items.Clear()
        ddlGender.Items.Add(New ListItem("Please Select Gender", "0"))
        ddlGender.Items.Add(New ListItem(objHR.mtdGetSex(objHR.EnumSex.Male), objHR.EnumSex.Male))
        ddlGender.Items.Add(New ListItem(objHR.mtdGetSex(objHR.EnumSex.Female), objHR.EnumSex.Female))
    End Sub

    Sub BindMarital(ByVal pv_strMarital As String)
        'ddlMarital.Items.Clear()
        'ddlMarital.Items.Add(New ListItem("Please Select Marital Status", "0"))
        'ddlMarital.Items.Add(New ListItem(objHR.mtdGetMarital(objHR.EnumMarital.Singled), objHR.EnumMarital.Singled))
        'ddlMarital.Items.Add(New ListItem(objHR.mtdGetMarital(objHR.EnumMarital.Married), objHR.EnumMarital.Married))
        'ddlMarital.Items.Add(New ListItem(objHR.mtdGetMarital(objHR.EnumMarital.Divorced), objHR.EnumMarital.Divorced))

        'If pv_strMarital = "" Then
        '    ddlMarital.SelectedIndex = 0
        'Else
        '    ddlMarital.SelectedIndex = CInt(pv_strMarital)
        'End If

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

    Sub BindICType()
        Dim strOpCd_ICType As String = "HR_CLSSETUP_ICTYPE_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intICTypeIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|" & "AND IC.Status LIKE '" & objHRSetup.EnumICTypeStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_ICType, strParam, objHRSetup.EnumHRMasterType.ICType, objICTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_ICTYPE&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objICTypeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objICTypeDs.Tables(0).Rows.Count - 1
                objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode") = Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode"))
                objICTypeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode")) & " (" & _
                                                                         Trim(objICTypeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If objICTypeDs.Tables(0).Rows(intCnt).Item("ICTypeCode") = strSelectedICType Then
                    intICTypeIndex = intCnt + 1
                End If
            Next
        End If

        dr = objICTypeDs.Tables(0).NewRow()
        dr("ICTypeCode") = ""
        dr("Description") = "Please Select New IC Type"
        objICTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlICType.DataSource = objICTypeDs.Tables(0)
        ddlICType.DataTextField = "Description"
        ddlICType.DataValueField = "ICTypeCode"
        ddlICType.DataBind()
        ddlICType.SelectedIndex = intICTypeIndex
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
        dr("Description") = "please select one"
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

    Sub BindGolongan(ByVal pv_strGol As String)
        Dim strOpCd_Get As String = "DP_CLSTRX_GOLONGAN_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim SearchStr As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intIndex As Integer = 0

        SearchStr = ""

        strParamName = "STRSEARCH"
        strParamValue = SearchStr

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                                strParamName, _
                                                strParamValue, _
                                                objDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_GOLONGAN&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvnotelist.aspx")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("Gol_Lama") = Trim(objDs.Tables(0).Rows(intCnt).Item("Gol_Lama"))
                objDs.Tables(0).Rows(intCnt).Item("Gol_Impas") = Trim(objDs.Tables(0).Rows(intCnt).Item("Gol_Lama"))
                If Trim(objDs.Tables(0).Rows(intCnt).Item("Gol_Lama")) = Trim(strSelectedGol) Then
                    intIndex = intCnt + 1
                End If
            Next
        End If

        dr = objDs.Tables(0).NewRow()
        dr("Gol_Lama") = ""
        dr("Gol_Impas") = "Please Select Golongan"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlGol.DataSource = objDs.Tables(0)
        ddlGol.DataTextField = "Gol_Impas"
        ddlGol.DataValueField = "Gol_Lama"
        ddlGol.DataBind()
        ddlGol.SelectedIndex = intIndex
    End Sub

    Sub BindUnit(ByVal pv_strUnit As String)
        Dim strOpCd_Get As String = "DP_CLSTRX_UNIT_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim SearchStr As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intIndex As Integer = 0

        SearchStr = ""

        strParamName = "STRSEARCH"
        strParamValue = "WHERE CAB = '01'"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                                strParamName, _
                                                strParamValue, _
                                                objDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_UNIT&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvnotelist.aspx")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("Unit") = Trim(objDs.Tables(0).Rows(intCnt).Item("Unit"))
                objDs.Tables(0).Rows(intCnt).Item("Nama") = Trim(objDs.Tables(0).Rows(intCnt).Item("Nama"))
                If Trim(objDs.Tables(0).Rows(intCnt).Item("Unit")) = Trim(strSelectedUnit) Then
                    intIndex = intCnt + 1
                End If
            Next
        End If

        dr = objDs.Tables(0).NewRow()
        dr("Unit") = ""
        dr("Nama") = "Please Select Unit"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlUnit.DataSource = objDs.Tables(0)
        ddlUnit.DataTextField = "Nama"
        ddlUnit.DataValueField = "Unit"
        ddlUnit.DataBind()
        ddlUnit.SelectedIndex = intIndex
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
        Dim strMemCode As String

        strMemCode = txtMemCode.Text
        onload_Member(strMemCode)
        onload_LinkButton()
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strMemCode As String

        strMemCode = txtMemCode.Text
        onload_Member(strMemCode)
        onload_LinkButton()
    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strMemCode As String

        strMemCode = txtMemCode.Text
        onload_Member(strMemCode)
        onload_LinkButton()
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeList.aspx?redirect=" & lblRedirect.Text)
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
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

End Class
