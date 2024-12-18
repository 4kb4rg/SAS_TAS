
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


Public Class PR_Setup_MaternityDet : Inherits Page
    
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblUOM as Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents txtMaternityCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents ddlAD As DropDownList   
    Protected WithEvents ddlDefAccCode As DropDownList
    Protected WithEvents ddlEmpCategory As DropDownList

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents MaternityCode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupMaternityCode As Label
    Protected WithEvents lblErrAD As Label   
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrEmpCategory As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblTitle1 As Label

    Protected WithEvents lblErrExists As Label

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objHR As New agri.HR.clsSetup()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objDs As New Object()
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

    Dim strSelectedMaternityCode As String = ""
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMaternity), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrDupMaternityCode.Visible = False
            lblErrAD.Visible = False            
            lblErrEmpCategory.Visible = False
            lblErrExists.Visible = False 
            
            strSelectedMaternityCode = Trim(IIf(Request.QueryString("Maternitycode") <> "", Request.QueryString("Maternitycode"), Request.Form("Maternitycode")))
            intStatus = CInt(lblHiddenSts.Text)
            If Not IsPostBack Then
                If strSelectedMaternityCode <> "" Then
                    Maternitycode.Value = strSelectedMaternityCode
                    onLoad_Display()
                Else                    
                    onLoad_BindADType("")
                    onLoad_BindEmpCategory("")
                    onLoad_BindButton()
                End If
            End If
        End If
        onload_GetLangCap()
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_Maternity_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_Maternity_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_Maternity_LIST_GET"
        Dim strOpCd_Sts As String = "PR_CLSSETUP_Maternity_STATUS_UPD"
        Dim strOpCd As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""
        
        If strSelectedMaternityCode = "" or strCmdArgs = "UnDel" Then
            ValidateADCode()
        End If
        If ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub       
        ElseIf ddlEmpCategory.SelectedItem.Value = "" Then
            lblErrEmpCategory.Visible = True            
            Exit Sub
        ElseIf lblErrExists.Visible = True Then
            Exit Sub
        Else
            If strCmdArgs = "Save" Then
                Select Case intStatus
                    Case objPRSetup.EnumMaternityStatus.Active
                        UpdateMaternityRecord()
                    Case Else
                        InsertMaternityRecord()
                    End Select
            ElseIf strCmdArgs = "Del" Then
                strParam = strSelectedMaternityCode & "|" & objPRSetup.EnumMaternityStatus.Deleted
                Try
                    intErrNo = objPRSetup.mtdUpdMaternity(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_MATERNITY_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_MaternityDet.aspx?Maternitycode=" & strSelectedMaternityCode)
                End Try

            ElseIf strCmdArgs = "UnDel" Then
                strParam = strSelectedMaternityCode & "|" & objPRSetup.EnumMaternityStatus.Active
                Try
                    intErrNo = objPRSetup.mtdUpdMaternity(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_MATERNITY_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_MaternityDet.aspx?Maternitycode=" & strSelectedMaternityCode)
                End Try
            End If

            If strSelectedMaternityCode <> "" Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_setup_MaternityList.aspx")
    End Sub

       Sub onload_GetLangCap()
        GetEntireLangCap()     
        lblTitle.Text = Ucase(GetCaption(objLangCap.EnumLangCap.Maternity)) & " "
        lblTitle1.Text = GetCaption(objLangCap.EnumLangCap.Maternity) & " "
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 "", _
                                                 "", _
                                                 strUserId, _
                                                 "", _
                                                 "", _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=MENU_PRSETUP_LANGCAP&errmesg=&redirect=")
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
        txtMaternityCode.Enabled = False
        txtDesc.Enabled = False
        ddlAD.Enabled = False        
        ddlEmpCategory.Enabled = False
        txtAmount.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objPRSetup.EnumMaternityStatus.Active
                txtDesc.Enabled = True
                ddlAD.Enabled = True                
                ddlEmpCategory.Enabled = True 
                txtAmount.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            Case objPRSetup.EnumMaternityStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtMaternityCode.Enabled = True
                txtDesc.Enabled = True
                ddlAD.Enabled = True                
                ddlEmpCategory.Enabled = True 
                txtAmount.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_MATERNITY_LIST_GET"
        Dim strParam As String 
        Dim intErrNo As Integer        

        strParam = "|and M.MaternityCode = '" & strSelectedMaternityCode & "'"
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                           strParam, _
                                           objPRSetup.EnumPayrollMasterType.Maternity, _ 
                                           objDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_MATERNITY_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            txtMaternityCode.Text = objDs.Tables(0).Rows(0).Item("MaternityCode").Trim()
            txtDesc.Text = objDs.Tables(0).Rows(0).Item("MaternityDesc").Trim()
            intStatus = CInt(objDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenSts.Text = objDs.Tables(0).Rows(0).Item("Status").Trim()
            lblStatus.Text = objPRSetup.mtdGetMaternityStatus(objDs.Tables(0).Rows(0).Item("Status").Trim())
            lblDateCreated.Text = objGlobal.GetLongDate(objDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objDs.Tables(0).Rows(0).Item("UserName")
            txtAmount.Text = ObjGlobal.GetIDDecimalSeparator(objDs.Tables(0).Rows(0).Item("Amount"))            
            onLoad_BindADType(objDs.Tables(0).Rows(0).Item("ADCode").Trim())  
            onLoad_BindEmpCategory(objDs.Tables(0).Rows(0).Item("EmpCategory").Trim())
            onLoad_BindButton()
        End If
    End Sub   

    Sub InsertMaternityRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_MATERNITY_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_MATERNITY_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_MATERNITY_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = False

       
        If ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        ElseIf ddlEmpCategory.SelectedItem.Value = "" Then
            lblErrEmpCategory.Visible = True
            Exit Sub
        Else
            strParam = "|" & " AND M.MaternityCode like '" & Trim(txtMaternityCode.Text) & "%'"
            Try
                intErrNo = objPRSetup.mtdGetMasterList(strOpCd_Get, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.Maternity, _
                                                objDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_MATERNITY_GET&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_MaternityList.aspx")
            End Try

            If objDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDupMaternitycode.Visible = True
            Else
                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
                strSelectedMaternityCode = Trim(txtMaternityCode.Text)
                Maternitycode.Value = strSelectedMaternityCode
                strParam = strSelectedMaternityCode & "|" & _
                            Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _                            
                            ddlEmpCategory.SelectedItem.Value & "|" & _
                            ddlAD.SelectedItem.Value & "|" & _
                            Trim(txtAmount.Text) & "|" & _                                                        
                            objPRSetup.EnumMaternityStatus.Active

                
                Try
                    intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strOpCd_Get, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objPRSetup.EnumPayrollMasterType.Maternity, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_MATERNITY_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_MaternityList.aspx")
                End Try
            End If
        End If
    End Sub

    Sub UpdateMaternityRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_MATERNITY_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_MATERNITY_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_MATERNITY_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = True
        If ddlAD.SelectedItem.Value = "" Then            
            lblErrAD.Visible = True
            Exit Sub      
        ElseIf ddlEmpCategory.SelectedItem.Value = "" Then            
            lblErrEmpCategory.Visible = True
            Exit Sub
        Else
            strSelectedMaternityCode = Trim(txtMaternityCode.Text)
            Maternitycode.Value = strSelectedMaternityCode
            strParam = strSelectedMaternityCode & "|" & _
                        Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _                        
                        ddlEmpCategory.SelectedItem.Value & "|" & _
                        ddlAD.SelectedItem.Value & "|" & _
                        Trim(txtAmount.Text) & "|" & _                                                    
                        objPRSetup.EnumMaternityStatus.Active
            
            Try
                intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strOpCd_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.Maternity, _
                                                blnDupKey, _
                                                blnUpdate.Text)
            
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_Maternity_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_MaternityList.aspx")
            End Try            
        End If
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
        strStatus = objPRSetup.EnumADStatus.Active & "' AND AD.MaternityInd like '1"
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

        SearchStr =  " AND SAL.Status = '1' AND SAL.CategoryTypeInd in('" & objHR.EnumCategoryType.Staff & "','" & objHR.EnumCategoryType.NonStaff &  "')"

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
            If objEmpCategoryDs.Tables(0).Rows(intCnt).Item("Description") = Trim(pv_strEmpCategory) Then
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
    Sub ValidateADCode() 
        Dim strOppCd_GET as String = "PR_CLSSETUP_MATERNITY_LIST_GET"
        Dim strParam As String
        Dim SearchStr As String
        Dim sortItem As String
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim objDataSet As New DataSet


        SearchStr = " AND M.Status like '" & objPRsetup.EnumMaternityStatus.Active & "' "
        
        SearchStr = SearchStr & " AND M.ADCode like '" & Trim(ddlAD.SelectedItem.Value.ToString) & "%'" 
        SearchStr = SearchStr & " AND M.EmpCategory like '" & Trim(ddlEmpCategory.SelectedItem.Value.ToString) & "%'"
        SearchStr = SearchStr & " AND M.MaternityCode <>'" & Trim(txtMaternityCode.Text.ToString) & "'"
            

        sortItem = ""
        strParam = sortItem & "|" & SearchStr

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOppCd_GET, strParam, objPRSetup.EnumPayrollMasterType.Maternity, objDataSet)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_MATERNITY_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_MaternityList.aspx")
        End Try
        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblErrExists.Visible = True
        End If           
    End Sub  


End Class
