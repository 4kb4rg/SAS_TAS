Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.PWSystem
Imports agri.Admin
Imports agri.PR
Imports agri.GL
Imports agri.GlobalHdl

Public Class PR_Setup_MedicalDet : Inherits Page
    
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblUOM as Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents txtMedicalCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtMedicalRate As TextBox
    Protected WithEvents ddlAD As DropDownList
    Protected WithEvents ddlMedicalType As DropDownList
    Protected WithEvents ddlDefAccCode As DropDownList
    Protected WithEvents ddlEmpCategory As DropDownList

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents MedicalCode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupMedicalCode As Label
    Protected WithEvents lblErrAD As Label
    Protected WithEvents lblErrMedicalType As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrEmpCategory As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrCheckMedical1 As Label
    Protected WithEvents lblErrCheckMedical2 As Label
    Protected WithEvents lblErrCheckMedical3 As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblTitle1 As Label
    Protected WithEvents lblTitle2 As Label

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objHR As New agri.HR.clsSetup()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objMedDs As New Object()
    Dim objADTypeDs As New Object()
    Dim objADLnDs As New Object()
    Dim objADGroupDs As New Object()
    Dim objDefAccDs As New Object()
    Dim objDefBlkDs As New Object()
    Dim objUOMDs As New Object()
    Dim objPayADDs As New Object()
    Dim objLocDs As New Object()
    Dim objLangCapDs As New Object()
    dim objVehTypeDs as New Object()
    Dim objEmpCategoryDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strSelectedMedicalCode As String = ""
    Dim strFlag As String = ""
    Dim intStatus As Integer
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMedical), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrDupMedicalCode.Visible = False
            lblErrAD.Visible = False
            lblErrMedicalType.Visible = False
            lblErrEmpCategory.Visible = False
            lblErrCheckMedical1.visible = false 
            lblErrCheckMedical2.visible = false 
            
            strSelectedMedicalCode = Trim(IIf(Request.QueryString("medicalcode") <> "", Request.QueryString("medicalcode"), Request.Form("medicalcode")))
            intStatus = CInt(lblHiddenSts.Text)
            If Not IsPostBack Then
                If strSelectedMedicalCode <> "" Then
                    medicalcode.Value = strSelectedMedicalCode
                    onLoad_Display()
                Else
                    txtMedicalRate.Text = ""
                    txtMedicalRate.Enabled = True
                    onLoad_BindMedicalType("")
                    onLoad_BindADType("")
                    onLoad_BindEmpCategory("")
                    onLoad_BindButton()
                    
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Medical))
        lblTitle1.Text = GetCaption(objLangCap.EnumLangCap.Medical)
        lblTitle2.Text = GetCaption(objLangCap.EnumLangCap.Medical)

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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PR/Setup/PR_setup_ADDet.aspx")
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


    Sub onLoad_BindButton()
        txtMedicalCode.Enabled = False
        txtDesc.Enabled = False
        ddlAD.Enabled = False
        ddlMedicalType.Enabled = False
        ddlEmpCategory.Enabled = False
        txtMedicalRate.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objPRSetup.EnumMedicalStatus.Active
                txtDesc.Enabled = false
                ddlAD.Enabled = false
                ddlMedicalType.Enabled = false
                ddlEmpCategory.Enabled = false 
                txtMedicalRate.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objPRSetup.EnumMedicalStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtMedicalCode.Enabled = True
                txtDesc.Enabled = True
                ddlAD.Enabled = True
                ddlMedicalType.Enabled = True
                ddlEmpCategory.Enabled = True 
                txtMedicalRate.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_MEDICAL_LIST_GET"
        Dim strParam As String 
        Dim intErrNo As Integer        

        strParam = "|and M.MedicalCode = '" & strSelectedMedicalCode & "'"
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                           strParam, _
                                           objPRSetup.EnumPayrollMasterType.Medical, _ 
                                           objMedDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_MEDICAL_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objMedDs.Tables(0).Rows.Count > 0 Then
            txtMedicalCode.Text = objMedDs.Tables(0).Rows(0).Item("MedicalCode").Trim()
            txtDesc.Text = objMedDs.Tables(0).Rows(0).Item("MedicalDesc").Trim()
            intStatus = CInt(objMedDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenSts.Text = objMedDs.Tables(0).Rows(0).Item("Status").Trim()
            lblStatus.Text = objPRSetup.mtdGetMedicalStatus(objMedDs.Tables(0).Rows(0).Item("Status").Trim())
            lblDateCreated.Text = objGlobal.GetLongDate(objMedDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objMedDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objMedDs.Tables(0).Rows(0).Item("UserName")
            txtMedicalRate.Text = FormatNumber(objMedDs.Tables(0).Rows(0).Item("MedicalRate"),2)
            onLoad_BindADType(objMedDs.Tables(0).Rows(0).Item("ADCode").Trim())            
            onLoad_BindMedicalType(objMedDs.Tables(0).Rows(0).Item("MedicalType").Trim())
            onLoad_BindEmpCategory(objMedDs.Tables(0).Rows(0).Item("EmpCategory").Trim())
            onLoad_BindButton()
        End If
    End Sub


    Sub onLoad_BindMedicalType(ByVal pv_strMedicalType As String)
        ddlMedicalType.Items.Clear
        ddlMedicalType.Items.Add(New ListItem("Select Medical Type", ""))
        ddlMedicalType.Items.Add(New ListItem(objPRSetup.mtdGetMedicalType(objPRSetup.EnumMedicalType.Medicine), objPRSetup.EnumMedicalType.Medicine))
        ddlMedicalType.Items.Add(New ListItem(objPRSetup.mtdGetMedicalType(objPRSetup.EnumMedicalType.CheckUp), objPRSetup.EnumMedicalType.CheckUp))

        If Trim(pv_strMedicalType) = "" Then
            ddlMedicalType.SelectedValue = ""
        Else
            ddlMedicalType.SelectedValue = CInt(Trim(pv_strMedicalType))
        End If
    End Sub

    Sub InsertMedicalRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_MEDICAL_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_MEDICAL_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_MEDICAL_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = False

        If ddlMedicalType.SelectedItem.Value = "" Then            
            lblErrMedicalType.Visible = True
            Exit Sub
        ElseIf ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        ElseIf ddlEmpCategory.SelectedItem.Value = "" Then
            lblErrEmpCategory.Visible = True
            Exit Sub
        Else
            strParam = "|" & " AND M.MedicalCode like '" & Trim(txtMedicalCode.Text) & "%'"
            Try
                intErrNo = objPRSetup.mtdGetMasterList(strOpCd_Get, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.Medical, _
                                                objMedDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_MEDICAL_GET&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_MedicalList.aspx")
            End Try

            If objMedDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDupMedicalcode.Visible = True
            Else
                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
                strSelectedMedicalCode = Trim(txtMedicalCode.Text)
                medicalcode.Value = strSelectedMedicalCode
                strParam = strSelectedMedicalCode & "|" & _
                            Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _
                            ddlMedicalType.SelectedItem.Value & "|" & _
                            ddlEmpCategory.SelectedItem.Value & "|" & _
                            ddlAD.SelectedItem.Value & "|" & _
                            Trim(txtMedicalRate.Text) & "|" & _                                                        
                            objPRSetup.EnumMedicalStatus.Active

                
                Try
                    intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strOpCd_Get, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objPRSetup.EnumPayrollMasterType.Medical, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_MEDICAL_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_MedicalList.aspx")
                End Try
            End If
        End If
    End Sub

    Sub UpdateMedicalRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_MEDICAL_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_MEDICAL_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_MEDICAL_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = True
        If ddlAD.SelectedItem.Value = "" Then            
            lblErrAD.Visible = True
            Exit Sub
        ElseIf ddlMedicalType.SelectedItem.Value = "" Then            
            lblErrMedicalType.Visible = True
            Exit Sub
        ElseIf ddlEmpCategory.SelectedItem.Value = "" Then            
            lblErrEmpCategory.Visible = True
            Exit Sub
        Else
            strSelectedMedicalCode = Trim(txtMedicalCode.Text)
            medicalcode.Value = strSelectedMedicalCode
            strParam = strSelectedMedicalCode & "|" & _
                        Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _
                        ddlMedicalType.SelectedItem.Value & "|" & _
                        ddlEmpCategory.SelectedItem.Value & "|" & _
                        ddlAD.SelectedItem.Value & "|" & _
                        Trim(txtMedicalRate.Text) & "|" & _                                                    
                        objPRSetup.EnumMedicalStatus.Active
            
            Try
                intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strOpCd_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.Medical, _
                                                blnDupKey, _
                                                blnUpdate.Text)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_MEDICAL_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_MedicalList.aspx")
            End Try            
        End If
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_MEDICAL_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_MEDICAL_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_MEDICAL_LIST_GET"
        Dim strOpCd_Sts As String = "PR_CLSSETUP_MEDICAL_STATUS_UPD"
        Dim strOpCd As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        ElseIf ddlMedicalType.SelectedItem.Value = "" Then
            lblErrMedicalType.Visible = True            
            Exit Sub
        ElseIf ddlEmpCategory.SelectedItem.Value = "" Then
            lblErrEmpCategory.Visible = True            
            Exit Sub
        Else
            If strCmdArgs = "Save" Then
                Select Case intStatus
                    Case objPRSetup.EnumMedicalStatus.Active
                        UpdateMedicalRecord()
                    Case Else
                        MedicalValidate()
                        if lblErrCheckMedical1.Visible = True or lblErrCheckMedical2.Visible = True Then
                            Exit Sub
                        End If
                        InsertMedicalRecord()
                    End Select
            ElseIf strCmdArgs = "Del" Then
                strParam = strSelectedMedicalCode & "|" & objPRSetup.EnumMedicalStatus.Deleted
                Try
                    intErrNo = objPRSetup.mtdUpdMedical(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_MEDICAL_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_MedicalDet.aspx?medicalcode=" & strSelectedMedicalCode)
                End Try

            ElseIf strCmdArgs = "UnDel" Then
                strFlag = "1"
                MedicalValidate()
                if lblErrCheckMedical1.Visible = True or lblErrCheckMedical2.Visible = True or lblErrCheckMedical3.Visible = True  Then
                    Exit Sub
                End If
                strParam = strSelectedMedicalCode & "|" & objPRSetup.EnumMedicalStatus.Active
                Try
                    intErrNo = objPRSetup.mtdUpdMedical(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_MEDICAL_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_MedicalDet.aspx?medicalcode=" & strSelectedMedicalCode)
                End Try
            End If

            If strSelectedMedicalCode <> "" Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_setup_MedicalList.aspx")
    End Sub
    

    Sub onLoad_BindADType(ByVal pv_strAD As String)
        Dim strOpCode As String = "PR_CLSSETUP_AD_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strADCode As String       
        Dim strDesc As String        
        Dim strStatus As String
        Dim strLastUpdate As String 
        Dim strSort as String = "AD.ADCode"
        Dim strSortCol as String = ""


        strADCode = ""
        strDesc = ""
        strStatus = objPRSetup.EnumADStatus.Active & "' AND AD.MedicalInd like '1"
        strLastUpdate = ""
        strParam = strADCode & "|" & _
                   strDesc & "|" & _
                   strStatus & "|" & _
                   strLastUpdate & "|" & _
                   strSort & "|" & _
                   strSortCol & "|"


        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCode, _
                                           strParam, _
                                           objADTypeDs, _
                                           False)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_HARVINCENTIVE_AD_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try
        
        For intCnt = 0 To objADTypeDs.Tables(0).Rows.Count - 1
            objADTypeDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(objADTypeDs.Tables(0).Rows(intCnt).Item("ADCode"))
            objADTypeDs.Tables(0).Rows(intCnt).Item("Description") = objADTypeDs.Tables(0).Rows(intCnt).Item("ADCode") & " (" & Trim(objADTypeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objADTypeDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(pv_strAD) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objADTypeDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("Description") = "Select Allowance & Deduction Code"
        objADTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAD.DataSource = objADTypeDs.Tables(0)
        ddlAD.DataValueField = "ADCode"
        ddlAD.DataTextField = "Description"
        ddlAD.DataBind()
        ddlAD.SelectedIndex = intSelectIndex
    End Sub


    Sub onLoad_BindEmpCategory(ByVal pv_strEmpCategory As String)
        Dim strOpCode As String = "HR_CLSSETUP_SALSCHEME_LIST_GET"
        Dim strParam As String = ""
        Dim Searchstr As String 
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim intMasterType As Integer = 0

        SearchStr =  " AND SAL.Status = '1' "

        sortitem = "ORDER BY SAL.SalSchemeCode " 
        strParam =  sortitem & "|" & SearchStr


        Try
            intErrNo = objHR.mtdGetMasterList(strOpCode, strParam, objHR.EnumHRMasterType.SalScheme, objEmpCategoryDs)
                                           
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SALSCHEME_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For intCnt = 0 To objEmpCategoryDs.Tables(0).Rows.Count - 1
            objEmpCategoryDs.Tables(0).Rows(intCnt).Item("SalSchemeCode") = Trim(objEmpCategoryDs.Tables(0).Rows(intCnt).Item("SalSchemeCode"))
            objEmpCategoryDs.Tables(0).Rows(intCnt).Item("Description") = objEmpCategoryDs.Tables(0).Rows(intCnt).Item("_Description")
            If objEmpCategoryDs.Tables(0).Rows(intCnt).Item("SalSchemeCode") = Trim(pv_strEmpCategory) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objEmpCategoryDs.Tables(0).NewRow()
        dr("SalSchemeCode") = ""
        dr("Description") = "Select Employee Category Code"
        objEmpCategoryDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpCategory.DataSource = objEmpCategoryDs.Tables(0)
        ddlEmpCategory.DataValueField = "SalSchemeCode"
        ddlEmpCategory.DataTextField = "Description"
        ddlEmpCategory.DataBind()
        ddlEmpCategory.SelectedIndex = intSelectIndex
    End Sub

    Sub MedicalValidate()
        Dim strOpCd As String = "PR_CLSSETUP_MEDICAL_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer        

        strParam = " AND M.MedicalType = '" & ddlMedicalType.SelectedItem.Value & "'" & "|" & _
                   " AND M.EmpCategory = '" & ddlEmpCategory.SelectedItem.Value & "' AND M.Status = '1'" 
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                           strParam, _
                                           objPRSetup.EnumPayrollMasterType.Medical, _ 
                                           objMedDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_MEDICAL_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objMedDs.Tables(0).Rows.Count > 0 Then
            If strFlag <> "" Then
                lblErrCheckMedical3.visible = True
                txtMedicalRate.Enabled = False
            Else
                lblErrCheckMedical1.visible = True 
                lblErrCheckMedical2.visible = False
            End If
 
            txtMedicalRate.Text = FormatNumber(objMedDs.Tables(0).Rows(0).Item("MedicalRate"),2)
            txtMedicalRate.Enabled = False
        Else
            strParam = " AND M.ADCode = '" & ddlAD.SelectedItem.Value & "'" & "|" & _
                       " AND M.EmpCategory = '" & ddlEmpCategory.SelectedItem.Value & "' AND M.Status = '1' " 
                  

            Try
                intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                        strParam, _
                                                        objPRSetup.EnumPayrollMasterType.Medical, _ 
                                                        objMedDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_MEDICAL_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objMedDs.Tables(0).Rows.Count > 0 Then
                If strFlag <> "" Then
                    lblErrCheckMedical3.visible = True 
                    txtMedicalRate.Enabled = False
                Else
                    lblErrCheckMedical1.visible = false 
                    lblErrCheckMedical2.visible = true
                End If
            Else 
                lblErrCheckMedical1.visible = false 
                lblErrCheckMedical2.visible = false    
                lblErrCheckMedical3.visible = false 
            End If     
        End If

    End Sub

    Sub onChange_ADCode(Sender As Object, E As EventArgs)
        Dim strOpCd As String = "PR_CLSSETUP_MEDICAL_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer        

        strParam = "|" & _
                   " AND M.EmpCategory = '" & ddlEmpCategory.SelectedItem.Value & "' AND M.Status = '1' " 
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                           strParam, _
                                           objPRSetup.EnumPayrollMasterType.Medical, _ 
                                           objMedDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_MEDICAL_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objMedDs.Tables(0).Rows.Count > 0 Then
            txtMedicalRate.Text = objMedDs.Tables(0).Rows(0).Item("MedicalRate")     
            txtMedicalRate.Enabled = false
        else
            txtMedicalRate.Text = ""
            txtMedicalRate.Enabled = true
        end if  
    End Sub


End Class
