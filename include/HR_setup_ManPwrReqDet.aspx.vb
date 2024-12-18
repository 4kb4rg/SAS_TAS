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

Imports agri.HR
Imports agri.PR
Imports agri.GlobalHdl

Public Class HR_setup_ManPwrReqDet : Inherits Page

    Protected WithEvents txtManPwrReqCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents rbLocal As RadioButton
    Protected WithEvents rbForeigner As RadioButton
    Protected WithEvents txtTotal As Textbox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents mprcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objManPwrReqDs As New Object()
    Dim objADDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Dim strSelectedMPRCode As String = ""
    Dim intStatus As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRHoliday), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            strSelectedMPRCode = Trim(IIf(Request.QueryString("mprcode") <> "", Request.QueryString("mprcode"), Request.Form("mprcode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedMPRCode <> "" Then
                    mprcode.Value = strSelectedMPRCode
                    onLoad_Display()
                Else
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Sub onLoad_BindButton()
        txtManPwrReqCode.Enabled = False
        txtDescription.Enabled = False
        rbLocal.Enabled = False
        rbForeigner.Enabled = False
        txtTotal.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objHRSetup.EnumManPwrReqStatus.Active
                txtDescription.Enabled = True
                rbLocal.Enabled = True
                rbForeigner.Enabled = True
                txtTotal.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumManPwrReqStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtManPwrReqCode.Enabled = True
                txtDescription.Enabled = True
                rbLocal.Enabled = True
                rbForeigner.Enabled = True
                txtTotal.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_MANPOWERREQUIREMENT_GET"
        Dim strParam As String = strSelectedMPRCode        
        Dim intErrNo As Integer

        Try
            intErrNo = objHRSetup.mtdGetManPowerRequirement(strOpCd, _
                                                            strParam, _
                                                            objManPwrReqDs, _
                                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_MANPWRREQ_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtManPwrReqCode.Text = strSelectedMPRCode
        txtDescription.Text = Trim(objManPwrReqDs.Tables(0).Rows(0).Item("Description"))
        rbLocal.Checked = IIf(CInt(Trim(objManPwrReqDs.Tables(0).Rows(0).Item("Type"))) = objHRSetup.EnumManPwrReqType.Local, True, False)
        rbForeigner.Checked = IIf(rbLocal.Checked = False, True, False)
        txtTotal.Text = objManPwrReqDs.Tables(0).Rows(0).Item("Total")
        intStatus = CInt(Trim(objManPwrReqDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objManPwrReqDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objHRSetup.mtdGetCPStatus(Trim(objManPwrReqDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objManPwrReqDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objManPwrReqDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objManPwrReqDs.Tables(0).Rows(0).Item("UserName"))

        onLoad_BindButton()
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "HR_CLSSETUP_MANPOWERREQUIREMENT_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_MANPOWERREQUIREMENT_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_MANPOWERREQUIREMENT_GET"
        Dim strOpCd_Sts As String = "HR_CLSSETUP_MANPOWERREQUIREMENT_STATUS_UPD"
        Dim strOpCd As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim intMPRType As Integer

        If strCmdArgs = "Save" Then
            strParam = Trim(txtManPwrReqCode.Text)
            Try
                intErrNo = objHRSetup.mtdGetManPowerRequirement(strOpCd_Get, _
                                                                strParam, _
                                                                objManPwrReqDs, _
                                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_MANPWRREQ_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objManPwrReqDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDup.Visible = True
                Exit Sub
            Else
                strSelectedMPRCode = Trim(txtManPwrReqCode.Text)
                mprcode.Value = strSelectedMPRCode
                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
                intMPRType = IIf(rbLocal.Checked = True, objHRSetup.EnumManPwrReqType.Local, objHRSetup.EnumManPwrReqType.Foreigner)

                strParam = Trim(txtManPwrReqCode.Text) & "|" & _
                           Trim(txtDescription.Text) & "|" & _
                           intMPRType & "|" & _                            
                           Trim(txtTotal.Text) & "|" & _
                           objHRSetup.EnumManPwrReqStatus.Active
                Try
                    intErrNo = objHRSetup.mtdUpdManPowerRequirement(strOpCd, _
                                                                    strCompany, _
                                                                    strLocation, _
                                                                    strUserId, _
                                                                    strParam, _
                                                                    False)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_MANPWRREQ_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_ManPwrReqDet.aspx")
                End Try
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = strSelectedMPRCode & "|" & objHRSetup.EnumManPwrReqStatus.Deleted
            Try
                intErrNo = objHRSetup.mtdUpdManPowerRequirement(strOpCd_Sts, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strParam, _
                                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_MANPWRREQ_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_ManPwrReqDet.aspx?mprcode=" & strSelectedMPRCode)
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = strSelectedMPRCode & "|" & objHRSetup.EnumManPwrReqStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdManPowerRequirement(strOpCd_Sts, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strParam, _
                                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_MANPWRREQ_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_ManPwrReqDet.aspx?mprcode=" & strSelectedMPRCode)
            End Try
        End If

        If strSelectedMPRCode <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_ManPwrReqList.aspx")
    End Sub


End Class
