
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

Public Class PR_Setup_RelocationDet : Inherits Page
    
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblUOM as Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents txtRelocationCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents ddlAD As DropDownList   
    Protected WithEvents ddlDefAccCode As DropDownList
    Protected WithEvents ddlType As DropDownList

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents RelocationCode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupRelocationCode As Label
    Protected WithEvents lblErrAD As Label   
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrType As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents lblErrExists As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblTitle2 As Label

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
    Dim objTypeDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strSelectedRelocationCode As String = ""
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRelocation), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrDupRelocationCode.Visible = False
            lblErrAD.Visible = False            
            lblErrType.Visible = False
            lblErrExists.Visible = False 
            
            strSelectedRelocationCode = Trim(IIf(Request.QueryString("Relocationcode") <> "", Request.QueryString("Relocationcode"), Request.Form("Relocationcode")))
            intStatus = CInt(lblHiddenSts.Text)
            If Not IsPostBack Then
                If strSelectedRelocationCode <> "" Then
                    Relocationcode.Value = strSelectedRelocationCode
                    onLoad_Display()
                Else   
                    onLoad_BindType("")
                    onLoad_BindADType("")                    
                    onLoad_BindButton()
                End If
            End If
        End If
        onload_GetLangCap()
    End Sub
    
    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_RELOCATION_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_RELOCATION_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_RELOCATION_LIST_GET"
        Dim strOpCd_Sts As String = "PR_CLSSETUP_RELOCATION_STATUS_UPD"
        Dim strOpCd As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If strSelectedRelocationCode = "" or strCmdArgs = "UnDel" Then
            ValidateType()
        End If
        If ddlType.SelectedItem.Value = "" Then
            lblErrType.Visible = True            
            Exit Sub
        ElseIf ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub 
        ElseIf lblErrExists.Visible = True Then
            Exit Sub
        Else
            If strCmdArgs = "Save" Then
                Select Case intStatus
                    Case objPRSetup.EnumRelocationStatus.Active
                        
                        UpdateRelocationRecord()
                    Case Else
                       
                        InsertRelocationRecord()
                    End Select
            ElseIf strCmdArgs = "Del" Then
                strParam = strSelectedRelocationCode & "|" & objPRSetup.EnumRelocationStatus.Deleted
                Try
                    intErrNo = objPRSetup.mtdUpdRelocation(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RELOCATION_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_RelocationDet.aspx?Relocationcode=" & strSelectedRelocationCode)
                End Try

            ElseIf strCmdArgs = "UnDel" Then
                strParam = strSelectedRelocationCode & "|" & objPRSetup.EnumRelocationStatus.Active
                Try
                    intErrNo = objPRSetup.mtdUpdRelocation(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RELOCATION_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_RelocationDet.aspx?Relocationcode=" & strSelectedRelocationCode)
                End Try
            End If

            If strSelectedRelocationCode <> "" Then                
                onLoad_Display()
            End If
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_setup_RelocationList.aspx")
    End Sub

       Sub onload_GetLangCap()
        GetEntireLangCap()     
        lblTitle.Text = Ucase(GetCaption(objLangCap.EnumLangCap.Relocation)) & " "
        lblTitle2.Text = GetCaption(objLangCap.EnumLangCap.Relocation) & " "
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
        txtRelocationCode.Enabled = False
        txtDesc.Enabled = False
        ddlAD.Enabled = False        
        ddlType.Enabled = False
        txtAmount.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objPRSetup.EnumRelocationStatus.Active
                txtDesc.Enabled = True
                ddlAD.Enabled = True                
                ddlType.Enabled = True 
                txtAmount.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objPRSetup.EnumRelocationStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtRelocationCode.Enabled = True
                txtDesc.Enabled = True
                ddlAD.Enabled = True                
                ddlType.Enabled = True 
                txtAmount.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_RELOCATION_LIST_GET"
        Dim strParam As String 
        Dim intErrNo As Integer        

        strParam = "|and R.RelocationCode = '" & strSelectedRelocationCode & "'"
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                           strParam, _
                                           objPRSetup.EnumPayrollMasterType.Relocation, _ 
                                           objDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_RELOCATION_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            txtRelocationCode.Text = objDs.Tables(0).Rows(0).Item("RelocationCode").Trim()
            txtDesc.Text = objDs.Tables(0).Rows(0).Item("RelocationDesc").Trim()
            intStatus = CInt(objDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenSts.Text = objDs.Tables(0).Rows(0).Item("Status").Trim()
            lblStatus.Text = objPRSetup.mtdGetRelocationStatus(objDs.Tables(0).Rows(0).Item("Status").Trim())
            lblDateCreated.Text = objGlobal.GetLongDate(objDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objDs.Tables(0).Rows(0).Item("UserName")
            txtAmount.Text = ObjGlobal.GetIDDecimalSeparator(objDs.Tables(0).Rows(0).Item("Amount"))   
            onLoad_BindType(objDs.Tables(0).Rows(0).Item("Type").Trim())
            onLoad_BindADType(objDs.Tables(0).Rows(0).Item("ADCode").Trim())             
            onLoad_BindButton()
        End If
    End Sub   

    Sub InsertRelocationRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_RELOCATION_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_RELOCATION_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_RELOCATION_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = False

        If ddlType.SelectedItem.Value = "" Then
            lblErrType.Visible = True
            Exit Sub
        ElseIf ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub        
        Else
            strParam = "|" & " AND R.RelocationCode like '" & Trim(txtRelocationCode.Text) & "%'"
            Try
                intErrNo = objPRSetup.mtdGetMasterList(strOpCd_Get, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.Relocation, _
                                                objDs)                

            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RELOCATION_GET&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_RelocationList.aspx")
            End Try

            If objDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDupRelocationcode.Visible = True
            Else
                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
                strSelectedRelocationCode = Trim(txtRelocationCode.Text)
                Relocationcode.Value = strSelectedRelocationCode
                strParam = strSelectedRelocationCode & "|" & _
                            Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _                            
                            ddlType.SelectedItem.Value & "|" & _
                            ddlAD.SelectedItem.Value & "|" & _
                            objPRSetup.EnumRelocationStatus.Active & "|" & _                                                        
                            Trim(txtAmount.Text)

                
                Try
                    intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strOpCd_Get, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objPRSetup.EnumPayrollMasterType.Relocation, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)                   
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RELOCATION_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_RelocationList.aspx")
                End Try
            End If
        End If
    End Sub

    Sub UpdateRelocationRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_RELOCATION_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_RELOCATION_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_RELOCATION_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = True
        If ddlAD.SelectedItem.Value = "" Then            
            lblErrAD.Visible = True
            Exit Sub      
        ElseIf ddlType.SelectedItem.Value = "" Then            
            lblErrType.Visible = True
            Exit Sub
        Else
            strSelectedRelocationCode = Trim(txtRelocationCode.Text)
            Relocationcode.Value = strSelectedRelocationCode
            strParam = strSelectedRelocationCode & "|" & _
                        Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _                        
                        ddlType.SelectedItem.Value & "|" & _
                        ddlAD.SelectedItem.Value & "|" & _
                        objPRSetup.EnumRelocationStatus.Active & "|" & _                                                    
                        Trim(txtAmount.Text)
            
            Try
                intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strOpCd_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.Relocation, _
                                                blnDupKey, _
                                                blnUpdate.Text)            
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_RELOCATION_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_RelocationList.aspx")
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
        Dim strCond As String
        
        If Trim(RelocationCode.Value) <> "" Then
            strCond = " AND AD.ADCode IN (SELECT ADCode FROM PR_RELOCATION WHERE Type='" & ddlType.SelectedItem.Value.ToString & "') "
        Else
            strCond = " AND AD.ADCode NOT IN (SELECT ADCode FROM PR_RELOCATION WHERE Status='1')"
        End If
        strADCode = ""
        strDesc = ""
        strStatus = objPRSetup.EnumADStatus.Active & "' " & strCond & " AND AD.RelocationInd like '1"
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_ADLIST_GET&errmesg=" & Exp.ToString & "&redirect=")
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


    Sub onLoad_BindType(ByVal pv_strType As String)

        ddlType.Items.Clear()
        ddlType.Items.Add(New ListItem("Select Type", ""))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetRelocationType(objPRSetup.EnumRelocationType.EstateMill_HQRegional), objPRSetup.EnumRelocationType.EstateMill_HQRegional))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetRelocationType(objPRSetup.EnumRelocationType.Regional_HQ), objPRSetup.EnumRelocationType.Regional_HQ))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetRelocationType(objPRSetup.EnumRelocationType.HQ_Regional), objPRSetup.EnumRelocationType.HQ_Regional))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetRelocationType(objPRSetup.EnumRelocationType.EstateMill_EstateMill), objPRSetup.EnumRelocationType.EstateMill_EstateMill))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetRelocationType(objPRSetup.EnumRelocationType.HQRegional_EstateMill), objPRSetup.EnumRelocationType.HQRegional_EstateMill))
        
        If Trim(pv_strType) = "" Then           
            ddlType.SelectedValue = ""
        Else
            ddlType.SelectedValue = CInt(Trim(pv_strType))
        End If
    End Sub
    Sub ValidateType() 
        Dim strOppCd_GET as String = "PR_CLSSETUP_RELOCATION_LIST_GET"
        Dim strParam As String
        Dim SearchStr As String
        Dim sortItem As String
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim objDataSet As New DataSet


        SearchStr = " AND R.Status like '" & objPRsetup.EnumRelocationStatus.Active & "' "       
        
        SearchStr = SearchStr & " AND R.Type like '" & Trim(ddlType.SelectedItem.Value.ToString) & "%'"
        SearchStr = SearchStr & " AND R.RelocationCode <>'" & Trim(txtRelocationCode.Text.ToString) & "'"
        SearchStr = SearchStr & " AND R.Status ='1'"
            

        sortItem = ""
        strParam = sortItem & "|" & SearchStr

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOppCd_GET, strParam, objPRSetup.EnumPayrollMasterType.Relocation, objDataSet)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_RELOCATION_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_RelocationList.aspx")
        End Try
        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblErrExists.Visible = True
        End If           
    End Sub  


End Class
