
Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.Admin
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl

Public Class HR_setup_GangDet : Inherits Page

    Protected WithEvents txtGangCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents ddlGangLeader As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents ddlGangMember As DropDownList
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents gangcode As HtmlInputHidden
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblErrGangLeader As Label
    Protected WithEvents ddlGangLevel As DropDownList
    Protected WithEvents ddlDivision As DropDownList
    Protected WithEvents lblErrGangLevel As Label

    Protected WithEvents lblErrCheckGangLeader As Label

    Protected WithEvents lblErrDivision As Label
    Protected WithEvents lblPlsSelect As Label
    Protected WithEvents lblErrGangMember As Label    
    Protected WithEvents lblFlagBindGang As Label    


    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim objEmpDs As New Object()
    'new
    Dim objMemDs As New Object()

    Dim objGangDs As New Object()
    Dim objGangLnDs As New Object()
    Dim objBlkGrpDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objEmpDsTop As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strSelGangCode As String = ""
    Dim intStatus As Integer

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String


    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        onload_GetLangCap()

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGang), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblErrGangLevel.Visible = False
            lblErrDivision.Visible = False
            lblErrGangMember.Visible = False
            strSelGangCode = Trim(IIf(Request.QueryString("gangcode") <> "", Request.QueryString("gangcode"), Request.Form("gangcode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelGangCode <> "" Then
                    Response.Write("masuk1")
                    gangcode.Value = strSelGangCode
                    lblFlagBindGang.Text = "0"
                    onLoad_Display()
                    onLoad_LineDisplay()
                    BindEmpCode_Member(strCompany, strLocation, "", "")
                    onLoad_BindButton()
                Else
                    Response.Write("masuk2")
                    lblFlagBindGang.Text = "1"
                    BindGangLevel("0")
                    BindDept("")
                    BindEmpCode_Leader(strCompany, strLocation, "", "")
                    BindEmpCode_Member(strCompany, strLocation, "", "")
                    'BindBlockGroup("")
                    'BindEmp("", True)
                    'BindEmp("", False)
                    onLoad_BindButton()
                End If

            End If
        End If
    End Sub

    Sub onLoad_BindButton()
        txtGangCode.Enabled = False
        txtDescription.Enabled = False
        ddlGangLeader.Enabled = False
        ddlGangLevel.Enabled = False
        tblSelection.Visible = False
        lblErrGangLeader.Visible = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objHRSetup.EnumGangStatus.Active
                txtDescription.Enabled = True
                ddlGangLeader.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumGangStatus.Deleted
                UnDelBtn.Visible = True
                UnDelBtn.Attributes("onclick") = "javascript:return ConfirmAction('deletegangmember');"
            Case Else
                txtGangCode.Enabled = True
                txtDescription.Enabled = True
                ddlGangLevel.Enabled = True
                ddlGangLeader.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
        End Select
        
        'If ddlGangLevel.SelectedItem.Value = CStr(objHRSetup.EnumGangLevel.Top) Then

        '    If ddlGangLeader.SelectedItem.Value <> "" Then
        '        BindEmpTop(ddlGangLeader.SelectedItem.Value)
        '    Else
        '        BindEmpTop("")
        '    End IF
        'Else
        '    BindEmp("",False)
        'End IF

        If ddlGangLevel.SelectedItem.Value = "" Then
            ddlGangLevel.Enabled = True
        End If 
        
        
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_GANG_GET"
        Dim strParam As String 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strSelGangCodeTemp as String
        
        if Trim(lblFlagBindGang.Text) = "0" then
            strSelGangCodeTemp = Trim(strLocation) & Trim(strSelGangCode)
        else
            strSelGangCodeTemp = Trim(strSelGangCode)
        end if
        strParam = strLocation & "|" & strSelGangCodeTemp
        Try
            intErrNo = objHRSetup.mtdGetGang(strOpCd, _
                                            strParam, _
                                            objGangDs, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_GANG_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        if objGangDs.Tables(0).Rows.Count > 0 then
            txtGangCode.Text = objGangDs.Tables(0).Rows(0).Item("GangCode").Trim()
            txtGangCode.Text = right(trim(txtGangCode.Text), (len(txtGangCode.Text) - len(Trim(strLocation))))
            txtDescription.Text = objGangDs.Tables(0).Rows(0).Item("Description").Trim()
            BindGangLevel(objGangDs.Tables(0).Rows(0).Item("GangLevel").Trim())
            BindDept(objGangDs.Tables(0).Rows(0).Item("BlkGrpCode").Trim())
            'BindBlockGroup(objGangDs.Tables(0).Rows(0).Item("BlkGrpCode").Trim())
            BindEmpCode_Leader(strCompany, strLocation, "", objGangDs.Tables(0).Rows(0).Item("GangLeader").Trim())
            'BindEmp(objGangDs.Tables(0).Rows(0).Item("GangLeader").Trim(), True)
            intStatus = CInt(Trim(objGangDs.Tables(0).Rows(0).Item("Status")))
            lblHiddenSts.Text = Trim(objGangDs.Tables(0).Rows(0).Item("Status"))
            lblStatus.Text = objHRSetup.mtdGetGangStatus(Trim(objGangDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objGangDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objGangDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objGangDs.Tables(0).Rows(0).Item("UserName"))

        end if
    End Sub

    Sub onLoad_LineDisplay()
        Dim strOpCd As String = "HR_CLSSETUP_GANG_LINE_GET"
        Dim strParam As String 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim strSelGangCodeTemp as String

        if Trim(lblFlagBindGang.Text) = "0" then
            strSelGangCodeTemp = Trim(strLocation) & Trim(strSelGangCode)
        else
            strSelGangCodeTemp = Trim(strSelGangCode)
        end if

        strParam = strLocation & "|" & strSelGangCodeTemp
        Try
            intErrNo = objHRSetup.mtdGetGang(strOpCd, _
                                            strParam, _
                                            objGangLnDs, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_GANGLINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dgLineDet.DataSource = objGangLnDs.Tables(0)
        dgLineDet.DataBind()
        
        For intCnt=0 To dgLineDet.Items.Count - 1
            Select Case CInt(objGangDs.Tables(0).Rows(0).Item("Status"))
                Case objHRSetup.EnumGangStatus.Active
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objHRSetup.EnumGangStatus.Deleted
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.visible = False
            End Select
        Next
    End Sub

    Sub BindEmpCode_Leader(ByVal pv_strCompCode As String, _
                           ByVal pv_strLocCode As String, _
                           ByVal pv_strDeptCode As String, _
                           ByVal pv_strEmpCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_DEPT_EMPLOYEE_GET"
        Dim dr As DataRow
        Dim strParam As String = pv_strCompCode & "|" & pv_strLocCode & "|" & pv_strDeptCode
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objHRSetup.mtdGetDept_Head(strOpCd, _
                                                  strParam, _
                                                  objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_HEAD_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode")) & " (" & Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
            If objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = pv_strEmpCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objEmpDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = "Please select one employee"
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlGangLeader.DataSource = objEmpDs.Tables(0)
        ddlGangLeader.DataValueField = "EmpCode"
        ddlGangLeader.DataTextField = "EmpName"
        ddlGangLeader.DataBind()
        ddlGangLeader.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindEmpCode_Member(ByVal pv_strCompCode As String, _
                          ByVal pv_strLocCode As String, _
                          ByVal pv_strDeptCode As String, _
                          ByVal pv_strEmpCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_MANDOR_MEMBER_GET"
        Dim dr As DataRow
        Dim strParam As String = pv_strCompCode & "|" & pv_strLocCode & "|" & pv_strDeptCode
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objHRSetup.mtdGetDept_Head(strOpCd, _
                                                  strParam, _
                                                  objMemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_HEAD_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objMemDs.Tables(0).Rows.Count - 1
            objMemDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objMemDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objMemDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objMemDs.Tables(0).Rows(intCnt).Item("EmpCode")) & "(" & Trim(objMemDs.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
        Next

        dr = objMemDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = "Please select one member"
        objMemDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlGangMember.DataSource = objMemDs.Tables(0)
        ddlGangMember.DataValueField = "EmpCode"
        ddlGangMember.DataTextField = "EmpName"
        ddlGangMember.DataBind()
        ddlGangMember.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindEmp(ByVal pv_strEmpId As String, ByVal pv_blnIsLeader As Boolean)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParam As String

        'If pv_blnIsLeader Then
        '    strOpCd = "HR_CLSSETUP_GANG_LEADER_GET"
        'Else
        '    strOpCd = "HR_CLSSETUP_GANG_MEMBER_GET"
        'End If

        'strParam = strLocation & "|" & pv_strEmpId

        'Try
        '    intErrNo = objHRSetup.mtdGetGangEmployee(strOpCd, strParam, pv_blnIsLeader, objEmpDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_GANG_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        'End Try

        'pv_strEmpId = Trim(UCase(pv_strEmpId))

        'For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
        '    objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode").Trim()
        '    objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") & " (" & objEmpDs.Tables(0).Rows(intCnt).Item("EmpName").Trim() & ")"
        '    If UCase(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode")) = pv_strEmpId Then
        '        intSelectedIndex = intCnt + 1
        '    End If
        'Next

        'dr = objEmpDs.Tables(0).NewRow()

        'dr("EmpCode") = ""
        'dr("EmpName") = "Please select one employee"
        'objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        'If pv_blnIsLeader = True Then
        '    ddlGangLeader.DataSource = objEmpDs.Tables(0)
        '    ddlGangLeader.DataValueField = "EmpCode"
        '    ddlGangLeader.DataTextField = "EmpName"
        '    ddlGangLeader.DataBind()
        '    ddlGangLeader.SelectedIndex = intSelectedIndex
        'Else
        '    ddlGangMember.DataSource = objEmpDs.Tables(0)
        '    ddlGangMember.DataValueField = "EmpCode"
        '    ddlGangMember.DataTextField = "EmpName"
        '    ddlGangMember.DataBind()
        '    ddlGangMember.SelectedIndex = intSelectedIndex
        'End If
    End Sub

    Sub InsertGangRecord()
        Dim strOpCd_Add As String = "HR_CLSSETUP_GANG_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_GANG_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_GANG_GET"
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If ddlGangLeader.SelectedItem.Value = "" Then
            lblErrGangLeader.Visible = True
            Exit Sub
        End If

        If ddlGangLevel.SelectedItem.Value = "" Then
            lblErrGangLevel.Visible = True
            Exit Sub
        End If

        If ddlDivision.SelectedItem.Value = "" Then
            lblErrDivision.Visible = True
            Exit Sub
        End If

        strParam = strLocation & "|" & Trim(txtGangCode.Text)

        Try
            intErrNo = objHRSetup.mtdGetGang(strOpCd_Get, _
                                            strParam, _
                                            objGangDs, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_GANG_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objGangDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
            lblErrDup.Visible = True
            Exit Sub
        Else
            strSelGangCode = Trim(strLocation) & Trim(txtGangCode.Text)
            blnIsUpdate = IIf(intStatus = 0, False, True)
            gangcode.Value = strSelGangCode


            strParam = Trim(strSelGangCode) & "|" & _
                        Trim(txtDescription.Text) & "|" & _
                        ddlGangLeader.SelectedItem.Value & "||" & _
                        strLocation & "|" & _
                        objHRSetup.EnumGangStatus.Active & "|" & _
                        ddlGangLevel.SelectedItem.Value & "|" & _
                        ddlDivision.SelectedItem.Value


            Try
                intErrNo = objHRSetup.mtdUpdGang(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnIsUpdate)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_GANG_SAVE&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_GangDet.aspx?gangcode=" & strSelGangCode)
            End Try
        End If
    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCd_Add As String = "HR_CLSSETUP_GANG_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_GANG_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_GANG_GET"
        Dim strOpCd_UpdSts As String = "HR_CLSSETUP_GANG_UPDSTS"
        Dim strOpCd_DelLn As String = "HR_CLSSETUP_GANG_LINE_DEL_IFEXIST"

        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""
        Dim strSelGangCodeTemp As String

        If ddlGangLeader.SelectedItem.Value = "" Then
            lblErrGangLeader.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            InsertGangRecord()
        ElseIf strCmdArgs = "Del" Then
            If lblFlagBindGang.Text = "0" Then
                strSelGangCodeTemp = Trim(strLocation) & Trim(txtGangCode.Text)
            Else
                strSelGangCodeTemp = Trim(txtGangCode.Text)
            End If

            strParam = Trim(strSelGangCodeTemp) & "|||||" & objHRSetup.EnumGangStatus.Deleted & "||"
            Try
                intErrNo = objHRSetup.mtdUpdGang(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_GANG_DEL&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_GangDet.aspx?gangcode=" & strSelGangCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            If lblFlagBindGang.Text = "0" Then
                strSelGangCodeTemp = Trim(strLocation) & Trim(txtGangCode.Text)
            Else
                strSelGangCodeTemp = Trim(txtGangCode.Text)
            End If
            strParam = Trim(strSelGangCodeTemp) & "|"
            Try
                intErrNo = objHRSetup.mtdUnDelGang(strOpCd_UpdSts, _
                                                   strOpCd_DelLn, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_GANG_UNDEL&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_GangDet.aspx?gangcode=" & strSelGangCode)
            End Try
        End If

        If strSelGangCode <> "" Then
            lblFlagBindGang.Text = "0"
            onLoad_Display()
            onLoad_LineDisplay()
            'BindEmp("", False)
            onLoad_BindButton()
        End If
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_AddLine As String = "HR_CLSSETUP_GANG_LINE_ADD"
        Dim strOpCode_UpdID As String = "HR_CLSSETUP_GANG_UPDATEID_UPD"
        Dim strParam As String
        Dim strGangCode As String
        Dim intErrNo As Integer

        Try
            strGangCode = ddlGangMember.SelectedItem.Value
        Catch Exp As System.Exception
            Exit Sub
        End Try

        'If (ddlGangLevel.SelectedItem.Value) = objHRSetup.EnumGangLevel.Bottom Then
        '    If Trim(ddlGangMember.SelectedItem.Value) = Trim(ddlGangLeader.SelectedItem.Value) Then
        '        lblErrGangMember.Visible = True
        '        Exit Sub
        '    End If
        'End If

        InsertGangRecord()

        If strSelGangCode = "" Then
            Exit Sub
        Else
            Try
                strParam = strSelGangCode & "|" & strGangCode & "|"
                intErrNo = objHRSetup.mtdUpdGangLine(strOpCode_UpdID, _
                                                    strOpCode_AddLine, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    False, _
                                                    objResult)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_GANGLINE_UPD&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_GangDet.aspx?gangcode=" & strSelGangCode)
            End Try
        End If

        lblFlagBindGang.Text = "1"
        onLoad_Display()
        onLoad_LineDisplay()
        'BindEmp("", False)
        onLoad_BindButton()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "HR_CLSSETUP_GANG_LINE_DEL"
        Dim strOpCode_UpdID As String = "HR_CLSSETUP_GANG_UPDATEID_UPD"
        Dim strParam As String
        Dim lblDelText As Label
        Dim strGangMember As String
        Dim intErrNo As Integer
        Dim strSelGangCodeTemp As String

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCode")
        strGangMember = lblDelText.Text
        strSelGangCodeTemp = Trim(strLocation) & Trim(strSelGangCode)

        Try
            strParam = strSelGangCodeTemp & "|" & strGangMember & "|"
            intErrNo = objHRSetup.mtdUpdGangLine(strOpCode_UpdID, _
                                                strOpCode_DelLine, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                False, _
                                                objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_GANGLINE_DEL&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_GangDet.aspx?gangcode=" & strSelGangCode)
        End Try

        onLoad_Display()
        onLoad_LineDisplay()
        'BindEmp("", False)
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_setup_Mdrlist_Estate.aspx")
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLKDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_BlockDet.aspx")
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

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & " Code"
        lblErrDivision.Text = lblPlsSelect.Text & lblBlkGrp.Text & "."
    End Sub

    Sub BindGangLevel(ByVal pv_strGangLevel As String)
        ddlGangLevel.Items.Clear()
        ddlGangLevel.Items.Add(New ListItem("Please Select Gang Level", ""))
        ddlGangLevel.Items.Add(New ListItem(objHRSetup.mtdGetGangLevel(objHRSetup.EnumGangLevel.Top), objHRSetup.EnumGangLevel.Top))
        ddlGangLevel.Items.Add(New ListItem(objHRSetup.mtdGetGangLevel(objHRSetup.EnumGangLevel.Bottom), objHRSetup.EnumGangLevel.Bottom))
        If pv_strGangLevel <> "" Then
            ddlGangLevel.SelectedIndex = CInt(pv_strGangLevel)
        Else
            ddlGangLevel.SelectedIndex = 0
        End If
    End Sub

    Sub BindDept(ByVal pv_strDeptCode As String)
        Dim strOpCode As String = "HR_CLSSETUP_DEPT_SEARCH"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSortCol As String = "A.DeptCode"
        Dim strSortExp As String = "ASC"
        Dim strSearch As String

        strSearch = objHRSetup.EnumDeptStatus.Active & "' AND A.LocCode = '" & strLocation

        strParam = "|" & _
                   strSearch & "||" & _
                   strSortCol & "|" & _
                   strSortExp

        Try
            intErrNo = objHRSetup.mtdGetDept(strOpCode, _
                                             strParam, _
                                             objBlkGrpDs, _
                                             False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_GET&errmesg=" & Exp.Message & "&redirect=hr/setup/hr_setup_gangdet.aspx")
        End Try

        pv_strDeptCode = Trim(UCase(pv_strDeptCode))
        For intCnt = 0 To objBlkGrpDs.Tables(0).Rows.Count - 1
            objBlkGrpDs.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(objBlkGrpDs.Tables(0).Rows(intCnt).Item("DeptCode"))
            objBlkGrpDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objBlkGrpDs.Tables(0).Rows(intCnt).Item("_Description"))
            If UCase(objBlkGrpDs.Tables(0).Rows(intCnt).Item("DeptCode")) = Trim(pv_strDeptCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objBlkGrpDs.Tables(0).NewRow()
        If intSelectIndex = 0 Then
            If pv_strDeptCode <> "" Then
                dr("DeptCode") = Trim(pv_strDeptCode)
                dr("_Description") = Trim(pv_strDeptCode)
            Else
                dr("DeptCode") = ""
                dr("_Description") = "Select " & lblBlkGrp.Text
            End If
        Else
            dr("DeptCode") = ""
            dr("_Description") = "Select " & lblBlkGrp.Text
        End If
        objBlkGrpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDivision.DataSource = objBlkGrpDs.Tables(0)
        ddlDivision.DataValueField = "DeptCode"
        ddlDivision.DataTextField = "_Description"
        ddlDivision.DataBind()
        ddlDivision.SelectedIndex = intSelectIndex
    End Sub


    Sub BindEmpTop(ByVal pv_strEmpId As String)
        Dim strOpCd As String = "HR_CLSSETUP_GANG_MEMBER_TOP_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParam As String

        'strParam = strLocation & "|" & pv_strEmpId

        'Try
        '    intErrNo = objHRSetup.mtdGetGangEmployeeTop(strOpCd, strParam, True, objEmpDsTop)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_GANG_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        'End Try

        'pv_strEmpId = Trim(UCase(pv_strEmpId))

        'For intCnt = 0 To objEmpDsTop.Tables(0).Rows.Count - 1
        '    objEmpDsTop.Tables(0).Rows(intCnt).Item("EmpCode") = objEmpDsTop.Tables(0).Rows(intCnt).Item("EmpCode").Trim()
        '    objEmpDsTop.Tables(0).Rows(intCnt).Item("EmpName") = objEmpDsTop.Tables(0).Rows(intCnt).Item("EmpCode") & " (" & objEmpDsTop.Tables(0).Rows(intCnt).Item("EmpName").Trim() & ")"
        '    If UCase(objEmpDsTop.Tables(0).Rows(intCnt).Item("EmpCode")) = pv_strEmpId Then
        '        intSelectedIndex = intCnt + 1
        '    End If
        'Next

        'dr = objEmpDsTop.Tables(0).NewRow()

        'dr("EmpCode") = ""
        'dr("EmpName") = "Please select one employee"
        'objEmpDsTop.Tables(0).Rows.InsertAt(dr, 0)

        'ddlGangMember.DataSource = objEmpDsTop.Tables(0)
        'ddlGangMember.DataValueField = "EmpCode"
        'ddlGangMember.DataTextField = "EmpName"
        'ddlGangMember.DataBind()
        'ddlGangMember.SelectedIndex = intSelectedIndex
    End Sub

    Sub GangLevel_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Dim strGangLevel As String = ddlGangLevel.SelectedItem.Value

        'If strGangLevel = CStr(objHRSetup.EnumGangLevel.Top) Then
        '    If ddlGangLeader.SelectedItem.Value <> "" Then
        '        BindEmpTop(ddlGangLeader.SelectedItem.Value)
        '    Else
        '        BindEmpTop("")
        '    End IF
        'Else
        '    BindEmp("",False)
        'End IF
    End Sub

    Sub GangLeader_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Dim strGangLevel As String = ddlGangLevel.SelectedItem.Value

        Dim strOpCd As String = "HR_CLSSETUP_CHECK_GANG_GET"
        Dim strGangLeader As String = ddlGangLeader.SelectedItem.Value
        Dim strParam As String
        Dim pv_blnIsLeader As Boolean
        Dim intErrNo As Integer

        'lblErrCheckGangLeader.visible = false
        'SaveBtn.enabled = true

        'strParam = "|" & Trim(strGangLeader)
        'pv_blnIsLeader = True

        'Try
        '    intErrNo = objHRSetup.mtdGetGangEmployee(strOpCd, strParam, pv_blnIsLeader, objEmpDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_GANG_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        'End Try

        'if objEmpDs.Tables(0).Rows.Count > 0 then 
        '    lblErrCheckGangLeader.visible = true
        '    SaveBtn.enabled = false
        '    exit sub
        'else
        '    lblErrCheckGangLeader.visible = false
        '    SaveBtn.enabled = true
        'end if

        'If strGangLevel = CStr(objHRSetup.EnumGangLevel.Top) Then
        '    If strGangLeader <> "" Then
        '            BindEmpTop(ddlGangLeader.SelectedItem.Value)
        '    Else    
        '        BindEmpTop("")
        '    End IF
        'Else
        '    BindEmp("",False)
        'End IF
    End Sub

    Sub BindBlockGroup(ByVal pv_strBlkGrpCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_BLOCKGROUP_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSort As String
        Dim strSearch As String

        strSort = "order by blk.BlkGrpCode"
        strSearch = "and blk.Status = '" & objGLSetup.EnumBlkGrpStatus.Active & "' and blk.LocCode = '" & strLocation & "'"

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.BlkGrp, _
                                                   objBlkGrpDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_BLOCKGROUP_GET&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_block.aspx")
        End Try

        For intCnt = 0 To objBlkGrpDs.Tables(0).Rows.Count - 1
            objBlkGrpDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objBlkGrpDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
            objBlkGrpDs.Tables(0).Rows(intCnt).Item("Description") = objBlkGrpDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") & " (" & Trim(objBlkGrpDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBlkGrpDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(pv_strBlkGrpCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objBlkGrpDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "Select " & lblBlkGrp.Text
        objBlkGrpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDivision.DataSource = objBlkGrpDs.Tables(0)
        ddlDivision.DataValueField = "BlkGrpCode"
        ddlDivision.DataTextField = "Description"
        ddlDivision.DataBind()
        ddlDivision.SelectedIndex = intSelectIndex
    End Sub
End Class
