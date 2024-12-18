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

Imports agri.PWSystem.clsUser
Imports agri.PWSystem.clsLangCap
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl

Public Class system_user_userdet : Inherits Page

    Protected WithEvents ActiveLocation As DataGrid
    Protected WithEvents dgTxSHReport As DataGrid

    Protected WithEvents txtUserId As TextBox
    Protected WithEvents txtPassword As TextBox
    Protected WithEvents txtConfirmPwd As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents txtEmail As TextBox
    Protected WithEvents ddlEmpID As DropDownList
    Protected WithEvents ddlLevel As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents lblPassword As Label
    Protected WithEvents lblNewLoc As Label
    Protected WithEvents lblADAR As Label
    Protected WithEvents lblErrUserId As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblDownload As Label
    Protected WithEvents lblUOMfile As Label
    
    Protected WithEvents cbSysAll As CheckBox
    Protected WithEvents cbSysUser As CheckBox
    Protected WithEvents cbSysCfg As CheckBox
    Protected WithEvents cbSysLangCap As CheckBox
    Protected WithEvents cbAdminCompany As CheckBox
    Protected WithEvents cbAdminLocation As CheckBox
    Protected WithEvents cbAdminUOM As CheckBox
    Protected WithEvents cbAdminDT As CheckBox
    Protected WithEvents chkMenurptAll As CheckBox


    Protected cbAdminBackup As CheckBox
    Protected WithEvents cbAdminNearestLocation As CheckBox

    Protected WithEvents userid As HtmlInputHidden
    Protected WithEvents hidUserLoc As HtmlInputHidden
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents BackBtn As ImageButton
    Protected WithEvents NewLocBtn As ImageButton

    Protected WithEvents ddlDeptCode As DropDownList
	Protected WithEvents ddluseraccessref As DropDownList

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysUser As New agri.PWSystem.clsUser()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objHR As New agri.HR.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objUserDs As New Object()
    Dim objUserLocDs As New Object()
    Dim objEmpDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objOk As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intADAR As Integer
    Dim strSelectedUserId As String = ""
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")
	    strLocType = Session("SS_LOCTYPE")
	    

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUser), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            strSelectedUserId = Trim(IIf(Request.QueryString("userid") <> "", Request.QueryString("userid"), Request.Form("userid")))
            lblPassword.Visible = False
            lblNewLoc.Visible = False
            lblADAR.Visible = False
            lblErrUserId.Visible = False
            DelBtn.Visible = False
            UnDelBtn.Visible = False
            NewLocBtn.Visible = False

            If Not IsPostBack Then
                BindDeptCode("")
				BindUseraccess()
                onLoad_Display()
                BindGrid_ReportSetup()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        Dim strCompTag As String
        Dim strLocTag As String
        Dim strLocDescTag As String
        Dim strNearestLocTag As String

        GetEntireLangCap()

        strCompTag = GetCaption(objLangCap.EnumLangCap.Company)
        strLocTag = GetCaption(objLangCap.EnumLangCap.Location)
        strLocDescTag = GetCaption(objLangCap.EnumLangCap.LocDesc)
        strNearestLocTag = GetCaption(objLangCap.EnumLangCap.NearestLocation)

        cbAdminCompany.text = " " & strCompTag
        cbAdminLocation.text = " " & strLocTag
        cbAdminNearestLocation.text = " " & strNearestLocTag
        cbAdminDT.text = lblDownload.text & strCompTag & ", " & strLocTag & lblUOMfile.text
        ActiveLocation.Columns(0).headertext = strLocTag & lblCode.text
        ActiveLocation.Columns(1).headertext = strLocDescTag
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
                                                 "", _
                                                 "", _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSTEM_USER_USERDET_LANGCAP&errmesg=" & lblErrMesage.Text & "&redirect=")
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


    Sub onLoad_Display()
        Dim strOpCode_User As String = "PWSYSTEM_CLSUSER_USERDETAILS_GET"
        Dim strOpCode_UserLoc As String = "PWSYSTEM_CLSUSER_USERLOCLIST_GET"
        Dim strOpCode_EmpId As String = "HR_CLSSETUP_EMPID_LIST_GET"
        Dim blnUserDetails As Boolean = True
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intIndex As Integer = 0 
        Dim lbUpdbutton As LinkButton

        userid.Value = strSelectedUserId

        strParam = strSelectedUserId
        Try
            intErrNo = objSysUser.mtdGetUser(strOpCode_User, strParam, objUserDs, blnUserDetails)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERDET_GET_USER&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userlist.aspx")
        End Try

        If objUserDs.Tables(0).Rows.Count > 0 Then
            objUserDs.Tables(0).Rows(0).Item("UserPwd") = Trim(objUserDs.Tables(0).Rows(0).Item("UserPwd"))
            objUserDs.Tables(0).Rows(0).Item("UserName") = Trim(objUserDs.Tables(0).Rows(0).Item("UserName"))
            objUserDs.Tables(0).Rows(0).Item("UserEmail") = Trim(objUserDs.Tables(0).Rows(0).Item("UserEmail"))
            objUserDs.Tables(0).Rows(0).Item("EmployeeId") = Trim(objUserDs.Tables(0).Rows(0).Item("EmployeeId"))
            objUserDs.Tables(0).Rows(0).Item("ColorInd") = Trim(objUserDs.Tables(0).Rows(0).Item("ColorInd"))
            objUserDs.Tables(0).Rows(0).Item("Status") = Trim(objUserDs.Tables(0).Rows(0).Item("Status"))

            strParam = strSelectedUserId
            Try
                intErrNo = objSysUser.mtdGetUserLocInfo(strOpCode_UserLoc, _
                                                        objUserLocDs, _
                                                        strParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERDET_GET_USER_LOC&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userlist.aspx")
            End Try

            If objUserLocDs.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To objUserLocDs.Tables(0).Rows.Count - 1
                    objUserLocDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objUserLocDs.Tables(0).Rows(intCnt).Item("LocCode"))
                    objUserLocDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objUserLocDs.Tables(0).Rows(intCnt).Item("Description"))
                Next intCnt
            End If

            txtUserId.ReadOnly = True
            txtUserId.Text = strSelectedUserId
            txtPassword.Text = objUserDs.Tables(0).Rows(0).Item(0)
            txtConfirmPwd.Text = objUserDs.Tables(0).Rows(0).Item(0)
            txtName.Text = objUserDs.Tables(0).Rows(0).Item("UserName")
            txtEmail.Text = objUserDs.Tables(0).Rows(0).Item("UserEmail")
            lblStatus.Text = objSysUser.mtdGetUserStatus(objUserDs.Tables(0).Rows(0).Item("Status"))
            lblDateCreated.Text = objGlobal.GetLongDate(objUserDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objUserDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objUserDs.Tables(0).Rows(0).Item("UpdateName")

            ddlLevel.SelectedValue = objUserDs.Tables(0).Rows(0).Item("UsrLevel")
            BindDeptCode(objUserDs.Tables(0).Rows(0).Item("DeptCode"))

            If (objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUser) And objUserDs.Tables(0).Rows(0).Item("ADAR")) = objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUser) Then
                cbSysCfg.Checked = True
            End If
            If (objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADSystemConfig) And objUserDs.Tables(0).Rows(0).Item("ADAR")) = objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADSystemConfig) Then
                cbSysUser.Checked = True
            End If
            If (objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADLanguageCaption) And objUserDs.Tables(0).Rows(0).Item("ADAR")) = objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADLanguageCaption) Then
                cbSysLangCap.Checked = True
            End If
            If (objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADCompany) And objUserDs.Tables(0).Rows(0).Item("ADAR")) = objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADCompany) Then
                cbAdminCompany.Checked = True
            End If
            If (objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADLocation) And objUserDs.Tables(0).Rows(0).Item("ADAR")) = objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADLocation) Then
                cbAdminLocation.Checked = True
            End If
            If (objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADNearestLocation) And objUserDs.Tables(0).Rows(0).Item("ADAR")) = objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADNearestLocation) Then
                cbAdminNearestLocation.Checked = True
            End If
            If (objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUOM) And objUserDs.Tables(0).Rows(0).Item("ADAR")) = objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUOM) Then
                cbAdminUOM.Checked = True
            End If
            If (objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAdmDataTransfer) And objUserDs.Tables(0).Rows(0).Item("ADAR")) = objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAdmDataTransfer) Then
                cbAdminDT.Checked = True
            End If
            If (objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBackupRestore) And objUserDs.Tables(0).Rows(0).Item("ADAR")) = objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBackupRestore) Then
                Me.cbAdminBackup.Checked = True
            End If

            hidUserLoc.Value = IIf(objUserLocDs.Tables(0).Rows.Count > 0, "0", "1")
            ActiveLocation.DataSource = objUserLocDs
            ActiveLocation.DataBind()

            For intCnt = 0 To ActiveLocation.Items.Count - 1
                Select Case CInt(objUserDs.Tables(0).Rows(0).Item("Status"))
                    Case objSysUser.EnumUserStatus.Active
                        lbUpdbutton = ActiveLocation.Items.Item(intCnt).FindControl("lbDelete")
                        lbUpdbutton.Visible = True
                        lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objSysUser.EnumUserStatus.Inactive
                        lbUpdbutton = ActiveLocation.Items.Item(intCnt).FindControl("lbDelete")
                        lbUpdbutton.Visible = True
                        lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objSysUser.EnumUserStatus.Deleted
                        lbUpdbutton = ActiveLocation.Items.Item(intCnt).FindControl("lbDelete")
                        lbUpdbutton.Visible = False
                End Select
            Next
			
            Select Case CInt(objUserDs.Tables(0).Rows(0).Item("Status"))
                Case objSysUser.EnumUserStatus.Active
                    txtUserId.Enabled = True
                    txtPassword.Enabled = True
                    txtConfirmPwd.Enabled = True
                    txtName.Enabled = True
                    txtEmail.Enabled = True
                    ddlEmpID.Enabled = True
                    ddlLevel.Enabled = True
                    cbSysAll.Enabled = True
                    cbSysUser.Enabled = True
                    cbSysCfg.Enabled = True
                    cbSysLangCap.Enabled = True
                    cbAdminCompany.Enabled = True
                    cbAdminLocation.Enabled = True
                    cbAdminNearestLocation.Enabled = True
                    cbAdminUOM.Enabled = True
                    cbAdminDT.Enabled = True
                    Me.cbAdminBackup.Enabled = True
                    SaveBtn.Visible = True
                    DelBtn.Visible = True
                    DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    NewLocBtn.Visible = True
                Case objSysUser.EnumUserStatus.Inactive
                    txtUserId.Enabled = False
                    txtPassword.Enabled = False
                    txtConfirmPwd.Enabled = False
                    txtName.Enabled = False
                    txtEmail.Enabled = False
                    ddlEmpID.Enabled = False
                    ddlLevel.Enabled = False
                    cbSysAll.Enabled = False
                    cbSysUser.Enabled = False
                    cbSysCfg.Enabled = False
                    cbSysLangCap.Enabled = False
                    cbAdminCompany.Enabled = False
                    cbAdminLocation.Enabled = False
                    cbAdminNearestLocation.Enabled = False
                    cbAdminUOM.Enabled = False
                    cbAdminDT.Enabled = False
                    Me.cbAdminBackup.Enabled = False
                    SaveBtn.Visible = True
                    DelBtn.Visible = True
                    NewLocBtn.Visible = True
                Case objSysUser.EnumUserStatus.Deleted
                    txtUserId.Enabled = False
                    txtPassword.Enabled = False
                    txtConfirmPwd.Enabled = False
                    txtName.Enabled = False
                    txtEmail.Enabled = False
                    ddlEmpID.Enabled = False
                    ddlLevel.Enabled = False
                    cbSysAll.Enabled = False
                    cbSysUser.Enabled = False
                    cbSysCfg.Enabled = False
                    cbSysLangCap.Enabled = False
                    cbAdminCompany.Enabled = False
                    cbAdminLocation.Enabled = False
                    cbAdminNearestLocation.Enabled = False
                    cbAdminUOM.Enabled = False
                    cbAdminDT.Enabled = False
                    Me.cbAdminBackup.Enabled = False
                    UnDelBtn.Visible = True
            End Select
        Else
            SaveBtn.Visible = True
        End If

        Try
            intErrNo = objHR.mtdGetEmployee(strOpCode_EmpId, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERDET_GET_EMPLOYEE&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userlist.aspx")
        End Try

        If objEmpDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
                objEmpDs.Tables(0).Rows(intCnt).Item(0) = Trim(objEmpDs.Tables(0).Rows(intCnt).Item(0))
                objEmpDs.Tables(0).Rows(intCnt).Item(1) = objEmpDs.Tables(0).Rows(intCnt).Item(0) & " (" & Trim(objEmpDs.Tables(0).Rows(intCnt).Item(1)) & ")"
            Next intCnt

            If objUserDs.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
                    If objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = objUserDs.Tables(0).Rows(0).Item("EmployeeId") Then
                        intIndex = intCnt + 1
                        Exit For
                    End If
                Next
            End If
        End If

        Dim dr As DataRow
        dr = objEmpDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = "Select one Employee"
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlEmpID.DataSource = objEmpDs.Tables(0)
        ddlEmpID.DataTextField = "EmpName"
        ddlEmpID.DataValueField = "EmpCode"
        ddlEmpID.DataBind()
        ddlEmpID.SelectedIndex = intIndex

        If strSelectedUserId = strUserId Then
            ActiveLocation.Enabled = False
            txtUserId.ReadOnly = True
            txtPassword.ReadOnly = True
            txtConfirmPwd.ReadOnly = True
            txtName.ReadOnly = True
            txtEmail.ReadOnly = True
            ddlEmpID.Enabled = False
            cbSysAll.Enabled = False
            cbSysUser.Enabled = False
            cbSysCfg.Enabled = False
            cbSysLangCap.Enabled = False
            cbAdminCompany.Enabled = False
            cbAdminLocation.Enabled = False
            cbAdminNearestLocation.Enabled = False
            cbAdminUOM.Enabled = False
            cbAdminDT.Enabled = False
            Me.cbAdminBackup.Enabled = False
            SaveBtn.Visible = False
            DelBtn.Visible = False
            UnDelBtn.Visible = False
            NewLocBtn.Visible = False
        End If
    End Sub

    Sub BindGrid_ReportSetup()
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim intCnt As Integer = 0
        Dim lbl As Label

        dsData = LoadData_ReportSetup()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTxSHReport.PageSize)

        dgTxSHReport.DataSource = dsData
        If dgTxSHReport.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgTxSHReport.CurrentPageIndex = 0
            Else
                dgTxSHReport.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgTxSHReport.DataBind()

        'For intCnt = 0 To dgInvOst.Items.Count - 1
        '    lbl = dgInvOst.Items.Item(intCnt).FindControl("lblNo")
        '    lbl.Text = intCnt + 1
        'Next


        For intCnt = 0 To dgTxSHReport.Items.Count - 1
            If CType(dgTxSHReport.Items(intCnt).FindControl("lblRptCheck"), Label).Text = "0" Then
                CType(dgTxSHReport.Items(intCnt).FindControl("chkMenu"), CheckBox).Checked = False
            Else
                CType(dgTxSHReport.Items(intCnt).FindControl("chkMenu"), CheckBox).Checked = True
            End If


            If CType(dgTxSHReport.Items(intCnt).FindControl("lblrptLevel"), Label).Text = "0" Then

                CType(dgTxSHReport.Items(intCnt).FindControl("lblrptDesc"), Label).Text = CType(dgTxSHReport.Items(intCnt).FindControl("lblrptName"), Label).Text
                dgTxSHReport.Items(intCnt).BackColor = Drawing.Color.DarkBlue
                dgTxSHReport.Items(intCnt).ForeColor = Drawing.Color.WhiteSmoke
            End If

            If CType(dgTxSHReport.Items(intCnt).FindControl("lblrptLevel"), Label).Text = "1" Then
                CType(dgTxSHReport.Items(intCnt).FindControl("lblrptDesc"), Label).Text = "&nbsp&nbsp&nbsp" & CType(dgTxSHReport.Items(intCnt).FindControl("lblrptName"), Label).Text
                dgTxSHReport.Items(intCnt).BackColor = Drawing.Color.Blue
            End If

            If CType(dgTxSHReport.Items(intCnt).FindControl("lblrptLevel"), Label).Text = "2" Then
                CType(dgTxSHReport.Items(intCnt).FindControl("lblrptDesc"), Label).Text = "&nbsp&nbsp&nbsp&nbsp&nbsp" & " - " & CType(dgTxSHReport.Items(intCnt).FindControl("lblrptName"), Label).Text
            End If

            If CType(dgTxSHReport.Items(intCnt).FindControl("lblrptLevel"), Label).Text = "3" Then
                CType(dgTxSHReport.Items(intCnt).FindControl("lblrptDesc"), Label).Text = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" & " - " & CType(dgTxSHReport.Items(intCnt).FindControl("lblrptName"), Label).Text
            End If
        Next

    End Sub

    Protected Function LoadData_ReportSetup() As DataSet
        Dim strParamName As String
        Dim strParamValue As String
        Dim objOst As New DataSet()

        Dim strOppCode_Get As String = "GL_STDRPT_NEW_NAME_GET"
        Dim intErrNo As Integer

        strParamName = "STRUSER"
        strParamValue = txtUserId.Text.Trim

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objOst)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgTxSHReport.DataSource = objOst
        dgTxSHReport.DataBind()

        Return objOst

    End Function


    Sub OnItemCommand_Process(Sender As Object, E As DataGridCommandEventArgs)
        Dim strSelectedLocCode As String
        Dim strParam As String
        Dim intErrNo As String
        Dim lbLocCode as LinkButton
        Dim intIndex As Integer = E.Item.ItemIndex
        Dim LocCodeCell As TableCell = E.Item.Cells(0)

        If E.CommandSource.CommandName = "UserLocDetails" Then
            lbLocCode = ActiveLocation.Items.Item(intIndex).FindControl("UserLoc")
            Response.Redirect("userloc.aspx?userid=" & Request.Form("userid") & "&loccode=" & lbLocCode.Text)
        End If

        onLoad_Display()
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCode_AddUser As String = "PWSYSTEM_CLSUSER_USERDETAILS_ADD"
        Dim strOpCode_UpdUser As String = "PWSYSTEM_CLSUSER_USERDETAILS_UPD"
        Dim strOpCode_GetUser As String = "PWSYSTEM_CLSUSER_USERDETAILS_GET"
        Dim objUserDs As New Object()
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim strPassword As String
        Dim strUserName As String
        Dim strEmail As String
        Dim strEmpId As String = Request.Form("ddlEmpId")
        Dim intAdminRights As Integer = 0
        Dim strParam As String = ""
        Dim intLevel As Integer
        Dim strDeptCode As String

        If (txtPassword.Text <> txtConfirmPwd.Text) Then
            lblPassword.Visible = True
            Exit Sub
        End If

        If ((cbSysUser.Checked) Or (cbSysCfg.Checked) Or (cbSysLangCap.Checked)) And (hidUserLoc.Value = "0") Then
            lblADAR.Visible = True
            Exit Sub
        End If


        strSelectedUserId = txtUserId.Text
        strPassword = txtPassword.Text
        strUserName = txtName.Text
        strEmail = txtEmail.Text
        intLevel = ddlLevel.SelectedValue
        strDeptCode = Trim(ddlDeptCode.SelectedItem.Value)

        If cbSysUser.Checked Then
           intAdminRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUser)
        End If
        If cbSysCfg.Checked Then
           intAdminRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADSystemConfig)
        End If
        If cbSysLangCap.Checked Then
           intAdminRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADLanguageCaption)
        End If
        If cbAdminCompany.Checked Then
           intAdminRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADCompany)
        End If
        If cbAdminLocation.Checked Then
           intAdminRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADLocation)
        End If
        If cbAdminNearestLocation.Checked Then
           intAdminRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADNearestLocation)
        End If
        If cbAdminUOM.Checked Then
           intAdminRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUOM)
        End If
        If cbAdminDT.Checked Then
           intAdminRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAdmDataTransfer)
        End If
        If Me.cbAdminBackup.Checked Then
            intAdminRights += objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBackupRestore)
        End If

        If strCmdArgs = "Save" Then
            If userid.Value = "" Then
                Try
                    intErrNo = objSysUser.mtdGetUser(strOpCode_GetUser, _
                                                    strSelectedUserId, _
                                                    objUserDs, _
                                                    True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERDET_SAVENEW_GET_USER&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userlist.aspx")
                End Try

                If objUserDs.Tables(0).Rows.Count <> 0 Then
                    lblErrUserId.Visible = True
                    Exit Sub
                Else
                    strParam = strSelectedUserId & "|" & _
                               strPassword & "|" & _
                               strUserName & "|" & _
                               strEmail & "|" & _
                               strEmpId & "|" & _
                               intAdminRights & "|" & intLevel & "|" & strDeptCode
                    Try
                        intErrNo = objSysUser.mtdUpdUser(strOpCode_AddUser, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        objSysUser.EnumUserUpdType.Add)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERDET_SAVENEW_UPD_USER&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userlist.aspx")
                    End Try
                End If
            Else
                strParam = strSelectedUserId & "|" & _
                           strPassword & "|" & _
                           strUserName & "|" & _
                           strEmail & "|" & _
                           strEmpId & "|||" & _
                           intAdminRights & "|" & intLevel & "|" & strDeptCode
                Try
                    intErrNo = objSysUser.mtdUpdUser(strOpCode_UpdUser, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objSysUser.EnumUserUpdType.Update)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERDET_SAVE_UPD_USER&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userlist.aspx")
                End Try


            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = strSelectedUserId & "||||||" & objSysUser.EnumUserStatus.Deleted & "|||"
            Try
                intErrNo = objSysUser.mtdUpdUser(strOpCode_UpdUser, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objSysUser.EnumUserUpdType.Update)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERDET_DEL_UPD_USER&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userlist.aspx")
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = strSelectedUserId & "||||||" & objSysUser.EnumUserStatus.Active & "|||"
            Try
                intErrNo = objSysUser.mtdUpdUser(strOpCode_UpdUser, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objSysUser.EnumUserUpdType.Update)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERDET_UNDEL_UPD_USER&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userlist.aspx")
            End Try
        End If


        Dim strOpCodeRprt As String = "PWSYSTEM_CLSUSER_REPORT_DETAILS_UPD"
        Dim strOpCodeDel As String = "PWSYSTEM_CLSUSER_REPORT_DETAILS_DEL"

        Dim dsMaster As Object

        Dim strParamName As String = ""
        Dim strParamValue As String = ""


        strParamName = "USRID"
        strParamValue = txtUserId.Text.Trim

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCodeDel, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_SPK&errmesg=" & Exp.ToString() & "&redirect=PU/trx/PU_PODET.aspx")
        End Try

        For intCnt = 0 To dgTxSHReport.Items.Count - 1
            If CType(dgTxSHReport.Items(intCnt).FindControl("chkMenu"), CheckBox).Checked = True Then

                strParamName = "RPTID|USRID"
                strParamValue = Trim(CType(dgTxSHReport.Items(intCnt).FindControl("lblrptid"), Label).Text) & "|" & txtUserId.Text.Trim

                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCodeRprt, _
                                                            strParamName, _
                                                            strParamValue)

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_SPK&errmesg=" & Exp.ToString() & "&redirect=PU/trx/PU_PODET.aspx")
                End Try
            End If
        Next

        UserMsgBox(Me, "Save Sucsess !!!")
        onLoad_Display()
    End Sub


    Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim strSelectedLocCode As String
        Dim strParam As String
        Dim lbl As Label 
        Dim intErrNo As Integer
        Dim strOpCode_DelUserLoc As String = "PWSYSTEM_CLSUSER_USERLOC_DEL"

        lbl = E.Item.FindControl("lblLocCode")
        strSelectedLocCode = lbl.Text

        If strSelectedLocCode <> "" Then
            strParam = userid.Value & "|" & strSelectedLocCode & "|||||||||||||||||||"
            Try
                intErrNo = objSysUser.mtdUpdUserLoc(strOpCode_DelUserLoc, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERDET_UPD_USER_LOC&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userlist.aspx")
            End Try
        End If
        onLoad_Display()
    End Sub

    Sub NewLocBtn_Click(Sender As Object, E As ImageClickEventArgs)
        If (cbSysUser.Checked) Or (cbSysCfg.Checked) Or (cbSysLangCap.Checked) Then
            lblNewLoc.Visible = True
        Else
            Response.Redirect("userloc.aspx?userid=" & Request.Form("userid"))
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("userlist.aspx")
    End Sub

    Sub BindDeptCode(ByVal pv_strDept As String)
        Dim strOpCode As String = "HR_CLSSETUP_DEPT_DEPTCODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intErrNo As Integer

        'strParamName = "STRSEARCH"
        'strParamValue = "and A.LocCode = '" & strLocation & "' and A.Status = '1'"

        strParamName = ""
        strParamValue = ""

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                     strParamName, _
                                                     strParamValue, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnList.aspx")
        End Try


        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("DeptCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("DeptCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim()
            If dsForDropDown.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(pv_strDept) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("DeptCode") = ""
        dr("Description") = "Select one"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlDeptCode.DataSource = dsForDropDown.Tables(0)
        ddlDeptCode.DataValueField = "DeptCode"
        ddlDeptCode.DataTextField = "Description"
        ddlDeptCode.DataBind()

        ddlDeptCode.SelectedIndex = intSelectedIndex

    End Sub
	
	Sub BindUseraccess()
        Dim strOpCode As String = "PWSYSTEM_CLSUSER_USERLOCLIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intErrNo As Integer

        'strParamName = "STRSEARCH"
        'strParamValue = "and A.LocCode = '" & strLocation & "' and A.Status = '1'"

        strParamName = "STRSEARCH"
        strParamValue = ""

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                     strParamName, _
                                                     strParamValue, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnList.aspx")
        End Try


        'For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
        '    dsForDropDown.Tables(0).Rows(intCnt).Item("DeptCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("DeptCode").Trim()
        '    dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim()
        '    If dsForDropDown.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(pv_strDept) Then
        '        intSelectedIndex = intCnt + 1
        '    End If
        'Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("Val1") = ""
        dr("Desc1") = "Select one"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddluseraccessref.DataSource = dsForDropDown.Tables(0)
        ddluseraccessref.DataValueField = "Val1"
        ddluseraccessref.DataTextField = "Desc1"
        ddluseraccessref.DataBind()

        'ddluseraccessref.SelectedIndex = intSelectedIndex

    End Sub
	
	Sub CopyBtn_Click(Sender As Object, E As ImageClickEventArgs)
	Dim strOpCd_Upd As String = "PWSYSTEM_CLSUSER_USERLOC_DETAILS_COPY"
	Dim ParamName As String
    Dim ParamValue As String
    Dim intErrNo As Integer
		
	Dim tmpuser As String
	Dim tmploc AS String
	 
	Dim NewUser AS String = txtUserId.Text.Trim()
	 
		if ddluseraccessref.selectedItem.Value.Trim() <> "" Then
		    Dim ar As Array
            ar = Split(ddluseraccessref.selectedItem.Value.Trim(), "|")
            tmpuser = ar(0)
            tmploc = ar(1)
			
			ParamName = "USERID|LOCCODE|NEWUSERID"
            ParamValue = Trim(tmpuser) & "|" & _
                         Trim(tmploc) & "|" & _
                         Trim(NewUser)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MJOB_TEMP_UPDADD&errmesg=" & Exp.Message & "&redirect=")
            End Try
			
			 onLoad_Display()
		End if
	End Sub

    Protected Sub CheckBoxMenu_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim rptModule As String = ""
        Dim rptType As String = ""
        Dim nLoop As Integer = 0


        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim dgItem As DataGridItem = CType(chk.NamingContainer, DataGridItem)


        If CType(dgItem.Cells(0).FindControl("chkMenu"), CheckBox).Checked = True And CType(dgItem.Cells(0).FindControl("lblrptLevel"), Label).Text.Trim = "0" Then
            rptModule = CType(dgItem.Cells(0).FindControl("lblrptModule"), Label).Text.Trim
            For nLoop = 0 To dgTxSHReport.Items.Count - 1
                If CType(dgTxSHReport.Items(nLoop).FindControl("lblrptModule"), Label).Text.Trim = rptModule Then
                    CType(dgTxSHReport.Items(nLoop).FindControl("chkMenu"), CheckBox).Checked = True
                End If
            Next
        ElseIf CType(dgItem.Cells(0).FindControl("chkMenu"), CheckBox).Checked = False And CType(dgItem.Cells(0).FindControl("lblrptLevel"), Label).Text.Trim = "0" Then
            rptModule = CType(dgItem.Cells(0).FindControl("lblrptModule"), Label).Text.Trim
            For nLoop = 0 To dgTxSHReport.Items.Count - 1
                If CType(dgTxSHReport.Items(nLoop).FindControl("lblrptModule"), Label).Text.Trim = rptModule Then
                    CType(dgTxSHReport.Items(nLoop).FindControl("chkMenu"), CheckBox).Checked = False
                End If
            Next
        End If


        If CType(dgItem.Cells(0).FindControl("chkMenu"), CheckBox).Checked = True And CType(dgItem.Cells(0).FindControl("lblrptLevel"), Label).Text.Trim = "1" Then
            rptModule = CType(dgItem.Cells(0).FindControl("lblrptModule"), Label).Text.Trim
            rptType = CType(dgItem.Cells(0).FindControl("lblrptType"), Label).Text.Trim

            For nLoop = 0 To dgTxSHReport.Items.Count - 1
                If CType(dgTxSHReport.Items(nLoop).FindControl("lblrptModule"), Label).Text.Trim = rptModule And CType(dgTxSHReport.Items(nLoop).FindControl("lblrptType"), Label).Text.Trim = rptType Then
                    CType(dgTxSHReport.Items(nLoop).FindControl("chkMenu"), CheckBox).Checked = True
                End If
            Next
        ElseIf CType(dgItem.Cells(0).FindControl("chkMenu"), CheckBox).Checked = False And CType(dgItem.Cells(0).FindControl("lblrptLevel"), Label).Text.Trim = "1" Then
            rptModule = CType(dgItem.Cells(0).FindControl("lblrptModule"), Label).Text.Trim
            rptType = CType(dgItem.Cells(0).FindControl("lblrptType"), Label).Text.Trim

            For nLoop = 0 To dgTxSHReport.Items.Count - 1
                If CType(dgTxSHReport.Items(nLoop).FindControl("lblrptModule"), Label).Text.Trim = rptModule And CType(dgTxSHReport.Items(nLoop).FindControl("lblrptType"), Label).Text.Trim = rptType Then
                    CType(dgTxSHReport.Items(nLoop).FindControl("chkMenu"), CheckBox).Checked = False
                End If
            Next
        End If


        'For intCnt = 0 To dgTxSHReport.Items.Count - 1          
        '    ''centang module
        '    nLoop = 0
        '    If Trim(CType(dgTxSHReport.Items(intCnt).FindControl("lblrptLevel"), Label).Text) = "0" And CType(dgTxSHReport.Items(intCnt).FindControl("chkMenu"), CheckBox).Checked = True Then
        '        rptModule = CType(dgTxSHReport.Items(intCnt).FindControl("lblrptModule"), Label).Text.Trim
        '        For nLoop = 0 To dgTxSHReport.Items.Count - 1
        '            If CType(dgTxSHReport.Items(nLoop).FindControl("lblrptModule"), Label).Text.Trim = rptModule Then
        '                CType(dgTxSHReport.Items(nLoop).FindControl("chkMenu"), CheckBox).Checked = True
        '            End If
        '        Next
        '    End If

        '    'uncentang module
        '    nLoop = 0
        '    If Trim(CType(dgTxSHReport.Items(intCnt).FindControl("lblrptLevel"), Label).Text) = "0" And CType(dgTxSHReport.Items(intCnt).FindControl("chkMenu"), CheckBox).Checked = False Then
        '        rptModule = CType(dgTxSHReport.Items(intCnt).FindControl("lblrptModule"), Label).Text.Trim
        '        For nLoop = 0 To dgTxSHReport.Items.Count - 1
        '            If CType(dgTxSHReport.Items(nLoop).FindControl("lblrptModule"), Label).Text.Trim = rptModule Then
        '                CType(dgTxSHReport.Items(nLoop).FindControl("chkMenu"), CheckBox).Checked = False
        '            End If
        '        Next
        '    End If
        'Next


        'For intCnt = 0 To dgTxSHReport.Items.Count - 1
        '    ''centang Sub Module
        '    nLoop = 0
        '    If Trim(CType(dgTxSHReport.Items(intCnt).FindControl("lblrptLevel"), Label).Text) = "1" And CType(dgTxSHReport.Items(intCnt).FindControl("chkMenu"), CheckBox).Checked = True Then
        '        rptType = CType(dgTxSHReport.Items(intCnt).FindControl("lblrptType"), Label).Text.Trim
        '        For nLoop = 0 To dgTxSHReport.Items.Count - 1
        '            If CType(dgTxSHReport.Items(nLoop).FindControl("lblrptType"), Label).Text.Trim = rptType Then
        '                CType(dgTxSHReport.Items(nLoop).FindControl("chkMenu"), CheckBox).Checked = True
        '            End If
        '        Next
        '    End If
        'Next
    End Sub

    Protected Sub CheckBoxMenuAll_CheckedChanged(sender As Object, e As System.EventArgs)
        If chkMenurptAll.Checked = True Then
            For intCnt = 0 To dgTxSHReport.Items.Count - 1
                CType(dgTxSHReport.Items(intCnt).FindControl("chkMenu"), CheckBox).Checked = True
            Next
        Else
            For intCnt = 0 To dgTxSHReport.Items.Count - 1
                CType(dgTxSHReport.Items(intCnt).FindControl("chkMenu"), CheckBox).Checked = False
            Next
        End If
    End Sub

End Class
