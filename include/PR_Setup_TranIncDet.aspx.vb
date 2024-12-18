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


Public Class PR_Setup_TranIncDet : Inherits Page
    
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblUOM as Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents txtTranIncCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtQuoQty As TextBox
    Protected WithEvents txtQuoInc As TextBox
    Protected WithEvents txtAboveQuo As TextBox
    Protected WithEvents ddlAD As DropDownList  

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents TranIncCode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupTranIncCode As Label
    Protected WithEvents lblErrAD As Label   
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblSelect As Label
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

    Dim objLocDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strSelectedTranIncCode As String = ""
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTransportIncentive), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrDupTranIncCode.Visible = False
            lblErrAD.Visible = False  
            lblErrExists.Visible = False 
            
            strSelectedTranIncCode = Trim(IIf(Request.QueryString("TranInccode") <> "", Request.QueryString("TranInccode"), Request.Form("TranInccode")))
            intStatus = CInt(lblHiddenSts.Text)
            If Not IsPostBack Then
                If strSelectedTranIncCode <> "" Then
                    TranInccode.Value = strSelectedTranIncCode
                    onLoad_Display()
                Else                    
                    onLoad_BindADType("")
                    onLoad_BindButton()
                End If
            End If
        End If
        onload_GetLangCap()
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_TRANINCENTIVE_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_TRANINCENTIVE_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_TRANINCENTIVE_GET"
        Dim strOpCd_Sts As String = "PR_CLSSETUP_TRANINCENTIVE_STATUS_UPD"
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
        ElseIf lblErrExists.Visible = True Then
            Exit Sub
        Else
            If strCmdArgs = "Save" Then
                Select Case intStatus
                    Case objPRSetup.EnumTranIncentiveStatus.Active
                        UpdateTranIncRecord()
                    Case Else
                        InsertTranIncRecord()
                    End Select
            ElseIf strCmdArgs = "Del" Then
                strParam = strSelectedTranIncCode & "|" & objPRSetup.EnumTranIncentiveStatus.Deleted
                Try
                    intErrNo = objPRSetup.mtdUpdTranIncentive(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_TranInc_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_TranIncDet.aspx?TranInccode=" & strSelectedTranIncCode)
                End Try

            ElseIf strCmdArgs = "UnDel" Then
                strParam = strSelectedTranIncCode & "|" & objPRSetup.EnumTranIncentiveStatus.Active
                Try
                    intErrNo = objPRSetup.mtdUpdTranIncentive(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_TranInc_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_TranIncDet.aspx?TranInccode=" & strSelectedTranIncCode)
                End Try
            End If

            If strSelectedTranIncCode <> "" Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_setup_TranIncList.aspx")
    End Sub

       Sub onload_GetLangCap()
        GetEntireLangCap()     
        lblTitle.Text = Ucase(GetCaption(objLangCap.EnumLangCap.TransportIncentive)) & " "
        lblTitle1.Text = GetCaption(objLangCap.EnumLangCap.TransportIncentive) & " "
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
        txtTranIncCode.Enabled = False
        txtDesc.Enabled = False
        ddlAD.Enabled = False
        txtQuoQty.Enabled = False
        txtQuoInc.Enabled = False
        txtAboveQuo.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objPRSetup.EnumTranIncentiveStatus.Active
                txtDesc.Enabled = True
                ddlAD.Enabled = True
                txtQuoQty.Enabled = True
                txtQuoInc.Enabled = True
                txtAboveQuo.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            Case objPRSetup.EnumTranIncentiveStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtTranIncCode.Enabled = True
                txtDesc.Enabled = True
                ddlAD.Enabled = True  
                txtQuoQty.Enabled = True
                txtQuoInc.Enabled = True
                txtAboveQuo.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_TRANINCENTIVE_GET"
        Dim strParam As String 
        Dim intErrNo As Integer        

        strParam = "|and TI.TranIncCode = '" & strSelectedTranIncCode & "'"
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                           strParam, _
                                           objPRSetup.EnumPayrollMasterType.TranIncentive, _ 
                                           objDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_TranInc_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            txtTranIncCode.Text = objDs.Tables(0).Rows(0).Item("TranIncCode").Trim()
            txtDesc.Text = objDs.Tables(0).Rows(0).Item("Description").Trim()
            intStatus = CInt(objDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenSts.Text = objDs.Tables(0).Rows(0).Item("Status").Trim()
            lblStatus.Text = objPRSetup.mtdGetTranIncentiveStatus(objDs.Tables(0).Rows(0).Item("Status").Trim())
            lblDateCreated.Text = objGlobal.GetLongDate(objDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objDs.Tables(0).Rows(0).Item("UserName")
            txtQuoQty.Text = ObjGlobal.GetIDDecimalSeparator(objDs.Tables(0).Rows(0).Item("QuoQty"))  
            txtQuoInc.Text = ObjGlobal.GetIDDecimalSeparator(objDs.Tables(0).Rows(0).Item("QuoInc"))  
            txtAboveQuo.Text = ObjGlobal.GetIDDecimalSeparator(objDs.Tables(0).Rows(0).Item("AboveQuo"))           
            onLoad_BindADType(objDs.Tables(0).Rows(0).Item("ADCode").Trim())  
            onLoad_BindButton()
        End If
    End Sub   

    Sub InsertTranIncRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_TRANINCENTIVE_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_TRANINCENTIVE_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_TRANINCENTIVE_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = False

       
        If ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        Else
            strParam = "|" & " AND TI.TranIncCode like '" & Trim(txtTranIncCode.Text) & "%'"
            Try
                intErrNo = objPRSetup.mtdGetMasterList(strOpCd_Get, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.TranIncentive, _
                                                objDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_TranInc_GET&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_TranIncList.aspx")
            End Try

            If objDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDupTranInccode.Visible = True
            Else
                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
                strSelectedTranIncCode = Trim(txtTranIncCode.Text)
                TranInccode.Value = strSelectedTranIncCode
                strParam = strSelectedTranIncCode & "|" & _
                            Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _ 
                            ddlAD.SelectedItem.Value & "|" & _
                            Trim(txtQuoQty.Text) & "|" & _    
                            Trim(txtQuoInc.Text) & "|" & _ 
                            Trim(txtAboveQuo.Text) & "|" & _                                                     
                            objPRSetup.EnumTranIncentiveStatus.Active

                
                Try
                    intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strOpCd_Get, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objPRSetup.EnumPayrollMasterType.TranIncentive, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_TranInc_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_TranIncList.aspx")
                End Try
            End If
        End If
    End Sub

    Sub UpdateTranIncRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_TRANINCENTIVE_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_TRANINCENTIVE_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_TRANINCENTIVE_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = True
        If ddlAD.SelectedItem.Value = "" Then            
            lblErrAD.Visible = True
            Exit Sub  
        Else
            strSelectedTranIncCode = Trim(txtTranIncCode.Text)
            TranInccode.Value = strSelectedTranIncCode
                strParam = strSelectedTranIncCode & "|" & _
                            Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _ 
                            ddlAD.SelectedItem.Value & "|" & _
                            Trim(txtQuoQty.Text) & "|" & _    
                            Trim(txtQuoInc.Text) & "|" & _ 
                            Trim(txtAboveQuo.Text) & "|" & _                                                     
                            objPRSetup.EnumTranIncentiveStatus.Active
            
            Try
                intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strOpCd_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.TranIncentive, _
                                                blnDupKey, _
                                                blnUpdate.Text)
            
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_TranInc_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_TranIncList.aspx")
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
        strStatus = ""
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
 

End Class
