Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic

Imports agri.Admin
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap

Public Class HR_setup_CPdet : Inherits Page

    Protected WithEvents txtCPCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents ddlCPType As DropDownList
    Protected WithEvents cbBlackList As Checkbox
    Protected WithEvents cbPeriod As Checkbox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents cpcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupCP As Label
    Protected WithEvents lblErrCPType As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblEnter As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblList As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCP As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblCPType As Label
    Protected WithEvents lblCPTypeList As Label
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator
    Protected WithEvents trBlackList As HtmlTableRow

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objCPDs As New Object()
    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long

    Dim strSelectedCPCode As String = ""
    Dim intStatus As Integer
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCPCode), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrDupCP.Visible = False
            lblErrCPType.Visible = False
            strSelectedCPCode = Trim(IIf(Request.QueryString("cpcode") <> "", Request.QueryString("cpcode"), Request.Form("cpcode")))
            intStatus = CInt(lblHiddenSts.Text)
            If Not IsPostBack Then
                If strSelectedCPCode <> "" Then
                    cpcode.Value = strSelectedCPCode
                    onLoad_Display()
                Else
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.CareerProgress))
        lblCP.text = GetCaption(objLangCap.EnumLangCap.CareerProgress)
        lblDesc.text = GetCaption(objLangCap.EnumLangCap.CareerProgressDesc)
        lblCPType.text = GetCaption(objLangCap.EnumLangCap.CareerProgressType)

        validateCode.ErrorMessage = lblEnter.text & lblCP.text & lblCode.text & "."
        validateDesc.text = lblEnter.text & lblDesc.text & "."
        lblErrCPType.text = lblSelect.text & lblCPType.text & "."
        
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=")
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



    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_CP_GET"
        Dim strParam As String = strSelectedCPCode        
        Dim intErrNo As Integer

        Try
            intErrNo = objHRSetup.mtdGetCareerProgress(strOpCd, _
                                                       strParam, _
                                                       objCPDs, _
                                                       True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_CP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtCPCode.Text = Trim(objCPDs.Tables(0).Rows(0).Item("CPCode"))
        txtDescription.Text = Trim(objCPDs.Tables(0).Rows(0).Item("Description"))
        ddlCPType.SelectedIndex = CInt(Trim(objCPDs.Tables(0).Rows(0).Item("CPType")))
        cbBlackList.Checked = IIf(CInt(objCPDs.Tables(0).Rows(0).Item("BlackListInd")) = objHRSetup.EnumCPBlackList.Active, True, False)
        cbPeriod.Checked = IIf(CInt(objCPDs.Tables(0).Rows(0).Item("PeriodInd")) = objHRSetup.EnumCPPeriod.Active, True, False)
        intStatus = CInt(Trim(objCPDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objCPDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objHRSetup.mtdGetCPStatus(Trim(objCPDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objCPDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objCPDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objCPDs.Tables(0).Rows(0).Item("UserName"))
        
        If Trim(objCPDs.Tables(0).Rows(0).Item("CPType")) = "1" Then
            cbBlackList.Checked = False
            trBlackList.Visible = False
        End If

        onLoad_BindButton()
    End Sub

    Sub onLoad_BindButton()
        txtCPCode.Enabled = False
        txtDescription.Enabled = False
        ddlCPType.Enabled = False
        cbBlackList.Enabled = False
        cbPeriod.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objHRSetup.EnumCPStatus.Active
                txtDescription.Enabled = True
                ddlCPType.Enabled = True
                cbBlackList.Enabled = True
                cbPeriod.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumCPStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtCPCode.Enabled = True
                txtDescription.Enabled = True
                ddlCPType.Enabled = True
                cbBlackList.Enabled = True
                cbPeriod.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "HR_CLSSETUP_CP_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_CP_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_CP_GET"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim intBlackList As Integer
        Dim intPeriod As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If ddlCPType.SelectedItem.Value = "" Then
            lblErrCPType.Visible = True
            Exit Sub
        Else
            If strCmdArgs = "Save" Then
                strParam = Trim(txtCPCode.Text)
                Try
                    intErrNo = objHRSetup.mtdGetCareerProgress(strOpCd_Get, _
                                                               strParam, _
                                                               objCPDs, _
                                                               True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_CP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                If objCPDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                    lblErrDupCP.Visible = True
                    Exit Sub
                Else
                    strSelectedCPCode = Trim(txtCPCode.Text)
                    blnIsUpdate = IIf(intStatus = 0, False, True)
                    intBlackList = IIf(cbBlackList.Checked = True, objHRSetup.EnumCPBlackList.Active, objHRSetup.EnumCPBlackList.InActive)
                    intPeriod = IIf(cbPeriod.Checked = True, objHRSetup.EnumCPPeriod.Active, objHRSetup.EnumCPPeriod.InActive)

                    CPcode.Value = strSelectedCPCode
                    strParam = Trim(txtCPCode.Text) & "|" & _
                               Trim(txtDescription.Text) & "|" & _
                               ddlCPType.SelectedItem.Value & "|" & _
                               intBlackList & "|" & _
                               intPeriod & "|" & _
                               objHRSetup.EnumCPStatus.Active
                    Try
                        intErrNo = objHRSetup.mtdUpdCareerProgress(strOpCd_Add, _
                                                                    strOpCd_Upd, _
                                                                    strCompany, _
                                                                    strLocation, _
                                                                    strUserId, _
                                                                    strParam, _
                                                                    blnIsUpdate)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_CP_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_CPdet.aspx")
                    End Try
                End If

            ElseIf strCmdArgs = "Del" Then
                strParam = Trim(txtCPCode.Text) & "|||||" & objHRSetup.EnumCPStatus.Deleted
                Try
                    intErrNo = objHRSetup.mtdUpdCareerProgress(strOpCd_Add, _
                                                                strOpCd_Upd, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strParam, _
                                                                True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_CP_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_CPdet.aspx?CPcode=" & strSelectedCPCode)
                End Try

            ElseIf strCmdArgs = "UnDel" Then
                strParam = Trim(txtCPCode.Text) & "|||||" & objHRSetup.EnumCPStatus.Active
                Try
                    intErrNo = objHRSetup.mtdUpdCareerProgress(strOpCd_Add, _
                                                                strOpCd_Upd, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strParam, _
                                                                True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_CP_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_CPdet.aspx?CPcode=" & strSelectedCPCode)
                End Try
            End If

            If strSelectedCPCode <> "" Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub ChangeBlackList(ByVal sender As Object, ByVal e As EventArgs)
        If ddlCPType.SelectedItem.Value = "1" Then
            trBlackList.Visible = False
            cbBlackList.Checked = False
        Else 
            trBlackList.Visible = True
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_CPlist.aspx")
    End Sub


End Class
