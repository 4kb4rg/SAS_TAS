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


Public Class PR_Setup_DanaPensiunDet : Inherits Page
    
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblUOM as Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents txtDanaPensiunCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtDanaPensiunRate As TextBox
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents txtBalance As TextBox
    Protected WithEvents txtTotalAmount As TextBox
    Protected WithEvents ddlAD As DropDownList
    Protected WithEvents ddlDanaPensiunType As DropDownList
    Protected WithEvents ddlDefAccCode As DropDownList
    Protected WithEvents ddlEmpCategory As DropDownList

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents danapensiuncode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupDanaPensiunCode As Label
    Protected WithEvents lblErrAD As Label
    Protected WithEvents lblErrDanaPensiunType As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrEmpCategory As Label
    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents lblBalance As Label
    Protected WithEvents lblAmount As Label
    Protected WithEvents lblErrTotalAmount As Label
    Protected WithEvents lblErrBalance As Label
    Protected WithEvents lblErrAmount As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrDanaPensiunRate As Label
    Protected WithEvents lblEmpCategory As Label
    Protected WithEvents lblErrCheckDP1 As Label
    Protected WithEvents lblErrCheckDP2 As Label
    Protected WithEvents lblErrCheckDP3 As Label
    Protected WithEvents lblRate As Label
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

    Dim objDPDs As New Object()
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
    Dim objEmpCodeDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strPhyMonth As String
    Dim strPhyYear As String

    Dim strSelectedDanaPensiunCode As String = ""
    Dim intStatus As Integer
    Dim dtAppJoinDate As Date
    Dim strFlag As String = ""
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
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPensiun), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrDupDanaPensiunCode.Visible = False
            lblErrAD.Visible = False
            lblErrDanaPensiunType.Visible = False
            lblErrEmpCategory.Visible = False
            lblErrCheckDP1.Visible = False
            lblErrCheckDP2.Visible = False
            
            strSelectedDanaPensiunCode = Trim(IIf(Request.QueryString("danapensiuncode") <> "", Request.QueryString("danapensiuncode"), Request.Form("danapensiuncode")))
            intStatus = CInt(lblHiddenSts.Text)
            If Not IsPostBack Then
                If strSelectedDanaPensiunCode <> "" Then
                    danapensiuncode.Value = strSelectedDanaPensiunCode
                    onLoad_Display()
                    onLoad_Enabled("0")
                Else
                    onLoad_BindDanaPensiunType("")
                    onLoad_BindADType("")
                    onLoad_BindEmpCategory("")
                    onLoad_BindButton()
                    txtTotalAmount.Text = "0"
                    txtAmount.text = "0"
                    txtBalance.text = "0"
                    onLoad_Enabled("1")
                End If
            End If
        End If
        lblErrEmpCategory.Text = "Please Select One "
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.DanaPensiun))
        lblTitle1.Text = GetCaption(objLangCap.EnumLangCap.DanaPensiun)
        lblTitle2.Text = GetCaption(objLangCap.EnumLangCap.DanaPensiun)
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
        txtDanaPensiunCode.Enabled = False
        txtDesc.Enabled = False
        ddlAD.Enabled = False
        ddlDanaPensiunType.Enabled = False
        ddlEmpCategory.Enabled = False
        txtDanaPensiunRate.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        txtAmount.Enabled = False
        txtBalance.Enabled = False
        txtTotalAmount.Enabled = False
        lblAmount.Enabled = False
        lblBalance.Enabled = False
        lblTotalAmount.Enabled = False


        Select Case intStatus
            Case objPRSetup.EnumDanaPensiunStatus.Active
                txtDesc.Enabled = True
                ddlAD.Enabled = True
                ddlDanaPensiunType.Enabled = True
                ddlEmpCategory.Enabled = True 
                txtDanaPensiunRate.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objPRSetup.EnumDanaPensiunStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtDanaPensiunCode.Enabled = True
                txtDesc.Enabled = True
                ddlAD.Enabled = True
                ddlDanaPensiunType.Enabled = True
                ddlEmpCategory.Enabled = True 
                txtDanaPensiunRate.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_DANAPENSIUN_LIST_GET"
        Dim strParam As String 
        Dim intErrNo As Integer        

        strParam = "|and P.DanaPensiunCode = '" & strSelectedDanaPensiunCode & "'"
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                           strParam, _
                                           objPRSetup.EnumPayrollMasterType.DanaPensiun, _ 
                                           objDPDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_DANAPENSIUN_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objDPDs.Tables(0).Rows.Count > 0 Then
            txtDanaPensiunCode.Text = objDPDs.Tables(0).Rows(0).Item("DanaPensiunCode").Trim()
            txtDesc.Text = objDPDs.Tables(0).Rows(0).Item("DanaPensiunDesc").Trim()
            intStatus = CInt(objDPDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenSts.Text = objDPDs.Tables(0).Rows(0).Item("Status").Trim()
            lblStatus.Text = objPRSetup.mtdGetDanaPensiunStatus(objDPDs.Tables(0).Rows(0).Item("Status").Trim())
            lblDateCreated.Text = objGlobal.GetLongDate(objDPDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDPDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objDPDs.Tables(0).Rows(0).Item("UserName")
            txtDanaPensiunRate.Text = FormatNumber(objDPDs.Tables(0).Rows(0).Item("DanaPensiunRate"),2)
            txtAmount.Text = objDPDs.Tables(0).Rows(0).Item("DanaPensiunAmount")
            txtBalance.Text = objDPDs.Tables(0).Rows(0).Item("DanaPensiunBalance")
            txtTotalAmount.Text = objDPDs.Tables(0).Rows(0).Item("DanaPensiunTotal")
            onLoad_BindADType(objDPDs.Tables(0).Rows(0).Item("ADCode").Trim())            
            onLoad_BindDanaPensiunType(objDPDs.Tables(0).Rows(0).Item("DanaPensiunType").Trim())

            if objDPDs.Tables(0).Rows(0).Item("DanaPensiunType").Trim() = objPRSetup.EnumDanaPensiunType.IDP Then
                onLoad_BindEmpCategory(objDPDs.Tables(0).Rows(0).Item("EmpCategory").Trim())
                lblEmpCategory.Text = "Employee Category :*"
            Else 
                onLoad_BindEmpCode(objDPDs.Tables(0).Rows(0).Item("EmpCode").Trim())
                lblEmpCategory.Text = "Employee Code :*"
                CalculateBalance()
            End If 
            txtBalance.Text = ObjGlobal.GetIDDecimalSeparator(txtBalance.Text)
            txtTotalAmount.Text = ObjGlobal.GetIDDecimalSeparator(txtTotalAmount.Text)
            txtAmount.Text = ObjGlobal.GetIDDecimalSeparator(txtAmount.Text)

            onLoad_BindButton()
        End If
    End Sub


    Sub onLoad_BindDanaPensiunType(ByVal pv_strDanaPensiunType As String)
        ddlDanaPensiunType.Items.Clear
        ddlDanaPensiunType.Items.Add(New ListItem("Select Dana Pensiun Type", ""))
        ddlDanaPensiunType.Items.Add(New ListItem(objPRSetup.mtdGetDanaPensiunType(objPRSetup.EnumDanaPensiunType.IDP), objPRSetup.EnumDanaPensiunType.IDP))
        ddlDanaPensiunType.Items.Add(New ListItem(objPRSetup.mtdGetDanaPensiunType(objPRSetup.EnumDanaPensiunType.PSL), objPRSetup.EnumDanaPensiunType.PSL))

        If Trim(pv_strDanaPensiunType) = "" Then
            ddlDanaPensiunType.SelectedValue = ""
        Else
            ddlDanaPensiunType.SelectedValue = CInt(Trim(pv_strDanaPensiunType))
        End If
    End Sub

    Sub InsertDanaPensiunRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_DANAPENSIUN_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_DANAPENSIUN_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_DANAPENSIUN_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim strEmpCategory As String = ""
        Dim strEmpCode As String = ""
        Dim strType as String = Request.Form("ddlDanaPensiunType")
        blnUpdate.Text = False

        If ddlDanaPensiunType.SelectedItem.Value = "" Then            
            lblErrDanaPensiunType.Visible = True
            Exit Sub
        ElseIf ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        ElseIf ddlEmpCategory.SelectedItem.Value = "" Then
            lblErrEmpCategory.Text = lblErrEmpCategory.Text & left(lblEmpCategory.Text, (len(lblEmpCategory.Text) - 2))        
            lblErrEmpCategory.Visible = True
            Exit Sub
        Else
            strParam = "|" & " AND P.DanaPensiunCode like '" & Trim(txtDanaPensiunCode.Text) & "%'"
            Try
                intErrNo = objPRSetup.mtdGetMasterList(strOpCd_Get, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.DanaPensiun, _
                                                objDPDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_DANAPENSIUN_GET&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_DanaPensiunList.aspx")
            End Try

            If objDPDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDupDanaPensiuncode.Visible = True
            Else
                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
                strSelectedDanaPensiunCode = Trim(txtDanaPensiunCode.Text)
                danapensiuncode.Value = strSelectedDanaPensiunCode

                if strType = objPRSetup.EnumDanaPensiunType.IDP Then
                    strEmpCategory = ddlEmpCategory.SelectedItem.Value
                    strEmpCode = ""
                Else 
                    strEmpCategory = ""
                    strEmpCode = ddlEmpCategory.SelectedItem.Value
                End If 



                strParam = strSelectedDanaPensiunCode & "|" & _
                            Trim(txtDesc.Text) & "|" & _
                            ddlDanaPensiunType.SelectedItem.Value & "|" & _
                            strEmpCategory & "|" & _
                            ddlAD.SelectedItem.Value & "|" & _
                            IIf(Trim(txtDanaPensiunRate.Text) = "", 0, Trim(txtDanaPensiunRate.Text)) & "|" & _                                                        
                            objPRSetup.EnumDanaPensiunStatus.Active & "|" & _
                            IIf(Trim(txtAmount.Text) = "", 0, Trim(txtAmount.Text)) & "|" & _
                            IIf(Trim(txtBalance.Text) = "", 0, Trim(txtBalance.Text)) & "|" & _
                            IIf(Trim(txtTotalAmount.Text) = "", 0, Trim(txtTotalAmount.Text)) & "|" & _
                            strEmpCode & "|2" 


                
                Try
                    intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strOpCd_Get, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objPRSetup.EnumPayrollMasterType.DanaPensiun, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_DANAPENSIUN_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_DanaPensiunList.aspx")
                End Try
           End If
        End If
    End Sub

    Sub UpdateDanaPensiunRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_DANAPENSIUN_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_DANAPENSIUN_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_DANAPENSIUN_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim strEmpCategory As String = ""
        Dim strEmpCode As String = ""
        Dim strType as String = ddlDanaPensiunType.SelectedItem.Value        

        blnUpdate.Text = True
        If ddlAD.SelectedItem.Value = "" Then            
            lblErrAD.Visible = True
            Exit Sub
        ElseIf ddlDanaPensiunType.SelectedItem.Value = "" Then            
            lblErrDanaPensiunType.Visible = True
            Exit Sub
        ElseIf ddlEmpCategory.SelectedItem.Value = "" Then   
            lblErrEmpCategory.Text = lblErrEmpCategory.Text & left(lblEmpCategory.Text, (len(lblEmpCategory.Text) - 2))      
            lblErrEmpCategory.Visible = True
            Exit Sub
        Else
            strSelectedDanaPensiunCode = Trim(txtDanaPensiunCode.Text)
            danapensiuncode.Value = strSelectedDanaPensiunCode

                if strType = objPRSetup.EnumDanaPensiunType.IDP Then
                    strEmpCategory = ddlEmpCategory.SelectedItem.Value
                    strEmpCode = ""
                Else 
                    strEmpCategory = ""
                    strEmpCode = ddlEmpCategory.SelectedItem.Value
                End If 

            strParam = strSelectedDanaPensiunCode & "|" & _
                        Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _
                        ddlDanaPensiunType.SelectedItem.Value & "|" & _
                        strEmpCategory & "|" & _
                        ddlAD.SelectedItem.Value & "|" & _
                        IIf(Trim(txtDanaPensiunRate.Text) = "", 0, Trim(txtDanaPensiunRate.Text)) & "|" & _                                                        
                        objPRSetup.EnumDanaPensiunStatus.Active & "|" & _
                        IIf(Trim(txtAmount.Text) = "", 0, Trim(txtAmount.Text)) & "|" & _
                        IIf(Trim(txtBalance.Text) = "", 0, Trim(txtBalance.Text)) & "|" & _
                        IIf(Trim(txtTotalAmount.Text) = "", 0, Trim(txtTotalAmount.Text)) & "|" & _
                        strEmpCode & "|2"
            
            Try
                intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strOpCd_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.DanaPensiun, _
                                                blnDupKey, _
                                                blnUpdate.Text)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_DANAPENSIUN_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_DanaPensiunList.aspx")
            End Try            
        End If
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_DANAPENSIUN_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_DANAPENSIUN_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_DANAPENSIUN_LIST_GET"
        Dim strOpCd_Sts As String = "PR_CLSSETUP_DANAPENSIUN_STATUS_UPD"
        Dim strOpCd As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""
        Dim strType as String = Request.Form("ddlDanaPensiunType")

        If ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        ElseIf ddlDanaPensiunType.SelectedItem.Value = "" Then
            lblErrDanaPensiunType.Visible = True            
            Exit Sub
        ElseIf ddlEmpCategory.SelectedItem.Value = "" Then
            lblErrEmpCategory.Text = lblErrEmpCategory.Text & left(lblEmpCategory.Text, (len(lblEmpCategory.Text) - 2))    
            lblErrEmpCategory.Visible = True            
            Exit Sub

        Else
            If strType = objPRSetup.EnumDanaPensiunType.PSL Then
                If txtTotalAmount.Text = "" Then
                    lblErrTotalAmount.Visible = True
                    Exit Sub
                Else
                    lblErrTotalAmount.Visible = False
                End If
                If txtBalance.Text = "" Then
                    lblErrBalance.Visible = True
                    Exit Sub
                Else
                    lblErrBalance.Visible = False
                End If
                If txtAmount.Text = "" Then
                    lblErrAmount.Visible = True
                    Exit Sub
                Else
                    lblErrAmount.Visible = False
                End If
            End If 
            If strType = objPRSetup.EnumDanaPensiunType.IDP Then
                If txtDanaPensiunRate.Text = "" Then
                    lblErrDanaPensiunRate.Visible = True
                    Exit Sub
                Else
                    lblErrDanaPensiunRate.Visible = False
                End If
                lblErrTotalAmount.Visible = False
                lblErrBalance.Visible = False
                lblErrAmount.Visible = False
            End If
            If strCmdArgs = "Save" Then
                Select Case intStatus
                    Case objPRSetup.EnumDanaPensiunStatus.Active
                        UpdateDanaPensiunRecord()
                    Case Else
                        DanaPensiunValidate()
                        if lblErrCheckDP1.Visible = True or lblErrCheckDP3.Visible = True Then
                            Exit Sub
                        End If
                        InsertDanaPensiunRecord()
                    End Select
            ElseIf strCmdArgs = "Del" Then
                strParam = strSelectedDanaPensiunCode & "|" & objPRSetup.EnumDanaPensiunStatus.Deleted
                Try
                    intErrNo = objPRSetup.mtdUpdDanaPensiun(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_DANAPENSIUN_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_DanaPensiunDet.aspx?danapensiuncode=" & strSelectedDanaPensiunCode)
                End Try

            ElseIf strCmdArgs = "UnDel" Then
                strFlag = "1"
                DanaPensiunValidate()
                If lblErrCheckDP2.Visible = True Then
                    Exit Sub
                End If
                strParam = strSelectedDanaPensiunCode & "|" & objPRSetup.EnumDanaPensiunStatus.Active
                Try
                    intErrNo = objPRSetup.mtdUpdDanaPensiun(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_DANAPENSIUN_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_DanaPensiunDet.aspx?danapensiuncode=" & strSelectedDanaPensiunCode)
                End Try
            End If

            If strSelectedDanaPensiunCode <> "" Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_setup_DanaPensiunList.aspx")
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
        strStatus = objPRSetup.EnumADStatus.Active & "' AND AD.DanaPensiunInd like '1"
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

    Sub onChange_DPType(Sender As Object, E As EventArgs)
        Dim strType as String = Request.Form("ddlDanaPensiunType")

        If strType = objPRSetup.EnumDanaPensiunType.IDP Then
            txtAmount.Enabled = False
            txtBalance.Enabled = False
            txtTotalAmount.Enabled = False
            lblTotalAmount.Enabled = False
            lblBalance.Enabled = False
            lblAmount.Enabled = False
            txtDanaPensiunRate.Enabled = True
            lblRate.Enabled = True
            lblEmpCategory.Text = "Employee Category :*"
            onLoad_BindEmpCategory("")
        Else
            txtAmount.Enabled = True
            txtBalance.Enabled = True
            txtTotalAmount.Enabled = False
            lblTotalAmount.Enabled = True
            lblBalance.Enabled = True
            lblAmount.Enabled = True
            txtDanaPensiunRate.Enabled = False
            lblRate.Enabled = False
            lblEmpCategory.Text = "Employee Code :*"
            txtDanaPensiunRate.Text = ""
            onLoad_BindEmpCode("")
        End If

    End Sub

    Sub onLoad_BindEmpCode(ByVal pv_strEmpCode As String)
        Dim strOpCode As String = "PR_CLSSETUP_DANAPENSIUN_EMP_GET"
        Dim strParam As String = ""
        Dim Searchstr As String 
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim intMasterType As Integer = 0

        SearchStr =  ""

        sortitem = "ORDER BY E.EmpCode " 
        strParam =  sortitem & "|" & SearchStr


        Try
            intErrNo = objHR.mtdGetMasterList(strOpCode, strParam, objHR.EnumHRMasterType.SalScheme, objEmpCodeDs)
                                           
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_DANAPENSIUN_GET_EMP_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
            objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpName") = objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") & " (" & Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
            If objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(pv_strEmpCode) Then
                intSelectIndex = intCnt + 1
                dtAppJoinDate = objEmpCodeDs.Tables(0).Rows(intCnt).Item("AppJoinDate")
            End If
        Next

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = "Select Employee Code"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpCategory.DataSource = objEmpCodeDs.Tables(0)
        ddlEmpCategory.DataValueField = "EmpCode"
        ddlEmpCategory.DataTextField = "EmpName"
        ddlEmpCategory.DataBind()
        ddlEmpCategory.SelectedIndex = intSelectIndex
    End Sub

    Sub CalculateBalance()
        Dim intMonthJoinDate as Integer
        Dim intYearJoinDate as Integer
        Dim dblTotalMonth as Double


        intMonthJoinDate = Month(dtAppJoinDate)
        intYearJoinDate = Year(dtAppJoinDate)
        
        dblTotalMonth = (12 - CDbl(intMonthJoinDate)) + (((CDbl(strPhyYear) - 1) - CDbl(intYearJoinDate)) * 12) + CDbl(strPhyMonth)

        txtTotalAmount.Text = dblTotalMonth * CDbl(txtAmount.Text)
        txtBalance.Text = CDbl(txtBalance.Text) - CDbl(txtTotalAmount.Text) 
        If CDbl(txtBalance.Text) < 0 Then
            txtTotalAmount.Text = CDbl(txtTotalAmount.Text) + CDbl(txtBalance.Text)  
            txtBalance.Text = "0"
            SetFlagDanaPensiun() 
        End If 
    End Sub

    Sub onLoad_Enabled(strFlag1 As String)
        Select Case strFlag1
            Case "0"
                txtDanaPensiunCode.Enabled = False
                txtDesc.Enabled = False
                ddlDanaPensiunType.Enabled = False
                ddlEmpCategory.Enabled = False
                ddlAD.Enabled = False
                If ddlDanaPensiunType.SelectedItem.Value = "1" Then
                    txtDanaPensiunRate.Enabled = True
                Else
                    txtDanaPensiunRate.Enabled = False
                End If
                
            Case "1"
                txtDanaPensiunCode.Enabled = True
                txtDesc.Enabled = True
                ddlDanaPensiunType.Enabled = True
                ddlEmpCategory.Enabled = True
                ddlAD.Enabled = True
                txtDanaPensiunRate.Enabled = True
        End Select
    End Sub

    Sub SetFlagDanaPensiun()
        Dim strOpCd_Add As String = "PR_CLSSETUP_DANAPENSIUN_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_DANAPENSIUN_FLAG_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_DANAPENSIUN_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim strEmpCategory As String = ""
        Dim strEmpCode As String = ""
        Dim strType as String = Request.Form("ddlDanaPensiunType")

            strParam = strSelectedDanaPensiunCode & "|||||||||||" & objPRSetup.EnumDanaPensiunStatus.Active

            Try
                intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strOpCd_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.DanaPensiun, _
                                                blnDupKey, _
                                                TRUE)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_DANAPENSIUN_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_DanaPensiunList.aspx")
            End Try            

        
        
    End Sub

    Sub DanaPensiunValidate()
        Dim strOpCd As String = "PR_CLSSETUP_DANAPENSIUN_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer        

        If ddlDanaPensiunType.SelectedItem.Value = "1" Then
            strParam = "|" & " AND P.EmpCategory = '" & ddlEmpCategory.SelectedItem.Value & "' AND P.Status = '1' AND P.DanaPensiunType = '1'" 
        Else
            strParam = "|" & " AND P.EmpCode = '" & ddlEmpCategory.SelectedItem.Value & "' AND P.Status = '1' AND P.DanaPensiunType = '2'" 
        End If 
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                           strParam, _
                                           objPRSetup.EnumPayrollMasterType.DanaPensiun, _ 
                                           objDPDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_DANAPENSIUN_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objDPDs.Tables(0).Rows.Count > 0 Then
            If strFlag <> "" Then
                lblErrCheckDP2.visible = True
            Else
                If ddlDanaPensiunType.SelectedItem.Value = "1" Then
                    lblErrCheckDP1.visible = True 
                Else
                    lblErrCheckDP3.visible = True 
                End If    
            End If
 
        Else
            lblErrCheckDP1.visible = false 
            lblErrCheckDP2.visible = false    
            lblErrCheckDP3.visible = false 
        End If
    End Sub


End Class
