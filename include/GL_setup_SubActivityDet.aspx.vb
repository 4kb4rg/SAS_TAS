
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
Imports agri.PWSystem.clsLangCap

Public Class GL_Setup_SubActivityDet : Inherits Page

    Protected WithEvents txtSubActCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents ddlActCode As DropDownList
    Protected WithEvents ddlUOM As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents hidActCode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrUOM As Label
    Protected WithEvents lblErrLen As Label
    Protected WithEvents lblSubActCode As Label
    Protected WithEvents lblSubActDesc As Label
    Protected WithEvents lblActivity As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsUOM()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim objSubActDs As New Object()
    Dim objActDs As New Object()
    Dim objUOMDs As New Object()
    Dim objConfigDs As New Object()
    Dim objLangCapDs As New object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strSelSubActID As String = ""
    Dim intStatus As Integer
    Dim intMaxLen As Integer = 0
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String
    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubActivity), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrDup.Visible = False
            lblErrUOM.Visible = False
            strSelSubActID = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelSubActID <> "" Then
                    tbcode.Value = strSelSubActID
                    onLoad_Display()
                    BindActCodeBySubAct()
                Else
                    AssignMaxLength(intMaxLen)
                    BindUOM("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub ActCodeBySubAct(ByVal sender As Object, ByVal E As EventArgs)
        BindActCodeBySubAct()
    End Sub


    Sub BindActCodeBySubAct()

        Dim strOppCd_SubAct_GET As String = "GL_CLSSETUP_SUBACTIVITY_LIST_GET_BY_SUBACTCODE"
        Dim strActCode As String
        Dim strParam As String
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim dsSubAct As New DataSet()

        If strSelSubActID <> "" Then
            strParam = "AND SubAct.SubActCode = '" & Trim(objSubActDs.Tables(0).Rows(0).Item("SubActCode")) & "' AND SubAct.SubActID NOT IN ('" & tbcode.Value & "')"
        Else
            strParam = "AND SubAct.SubActCode = '" & Trim(txtSubActCode.Text) & "'"
        End If

        Try
            intErrNo = objGLSetup.mtdGetSubActivity(strOppCd_SubAct_GET, _
                                                  strParam, _
                                                  objSubActDs, _
                                                  True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBACTIVITY_BINDACTCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objSubActDs.Tables(0).Rows.Count - 1
            strActCode = strActCode & "','" & Trim(objSubActDs.Tables(0).Rows(intCnt).Item("ActCode"))
        Next

        If strActCode <> "" Then
            hidActCode.Value = Right(strActCode, Len(strActCode) - 3)
        Else
            hidActCode.Value = ""
        End If

        If strSelSubActID <> "" Then
            strParam = "AND SubAct.SubActID = '" & Trim(tbcode.Value) & "'"
            Try
                intErrNo = objGLSetup.mtdGetSubActivity(strOppCd_SubAct_GET, _
                                                      strParam, _
                                                      dsSubAct, _
                                                      True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBACTIVITY_BINDACTCODE2&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
            BindActCode(Trim(dsSubAct.Tables(0).Rows(0).Item("ActCode")), hidActCode.Value)
        Else
            BindActCode("", hidActCode.Value)
        End If
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "GL_CLSSETUP_SUBACTIVITY_LIST_GET_BY_SUBACTCODE"
        Dim intErrNo As Integer
        Dim strActCode As String

        Dim strParam As String = "AND SubAct.SubActID = '" & tbcode.Value & "'"
        Try
            intErrNo = objGLSetup.mtdGetSubActivity(strOpCd, _
                                                  strParam, _
                                                  objSubActDs, _
                                                  True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBACTIVITY_LIST_GET_BY_SUBACTCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtSubActCode.Text = Trim(objSubActDs.Tables(0).Rows(0).Item("SubActCode"))
        txtDescription.Text = Trim(objSubActDs.Tables(0).Rows(0).Item("Description"))

        intStatus = CInt(Trim(objSubActDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objSubActDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objGLSetup.mtdGetSubActStatus(Trim(objSubActDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objSubActDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objSubActDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objSubActDs.Tables(0).Rows(0).Item("UserName"))

        BindActCode(Trim(objSubActDs.Tables(0).Rows(0).Item("ActCode")), hidActCode.Value)
        BindUOM(Trim(objSubActDs.Tables(0).Rows(0).Item("UOMCode")))
        onLoad_BindButton()
    End Sub

    Sub onLoad_BindButton()
        txtSubActCode.Enabled = False
        txtDescription.Enabled = False
        ddlActCode.Enabled = False
        ddlUOM.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objGLSetup.EnumSubActStatus.Active
                txtDescription.Enabled = True
                ddlActCode.Enabled = True
                ddlUOM.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objGLSetup.EnumSubActStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtSubActCode.Enabled = True
                txtDescription.Enabled = True
                ddlActCode.Enabled = True
                ddlUOM.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.SubAct))
        lblSubActCode.Text = GetCaption(objLangCap.EnumLangCap.SubAct) & " Code"
        lblSubActDesc.Text = GetCaption(objLangCap.EnumLangCap.SubActDesc)
        lblActivity.Text = GetCaption(objLangCap.EnumLangCap.Activity)
        validateCode.ErrorMessage = "<br>Please enter " & lblSubActCode.Text & "."
        validateDesc.ErrorMessage = "Please enter " & lblSubActDesc.Text & "."

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_SubActivityDet.aspx")
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

    Sub BindActCode(ByVal pv_strActCode As String, ByVal pv_UsedActCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_SUBACTIVITY_ACTCODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        strParam = pv_UsedActCode & "|" & objGLSetup.EnumActStatus.Active
        Try
            intErrNo = objGLSetup.mtdGetActivity(strOpCode, _
                                                 strParam, _
                                                 objActDs, _
                                                 True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBACTIVITY_ACTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("ActCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("ActCode"))
            objActDs.Tables(0).Rows(intCnt).Item("Description") = objActDs.Tables(0).Rows(intCnt).Item("ActCode") & " (" & Trim(objActDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objActDs.Tables(0).Rows(intCnt).Item("ActCode") = Trim(pv_strActCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("ActCode") = ""
        dr("Description") = "Select " & lblActivity.Text
        objActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlActCode.DataSource = objActDs.Tables(0)
        ddlActCode.DataValueField = "ActCode"
        ddlActCode.DataTextField = "Description"
        ddlActCode.DataBind()

        If strSelSubActID <> "" Then
            If intSelectIndex = 0 Then
                ddlActCode.Items.Add(New ListItem(Trim(pv_strActCode) & " (deleted)", Trim(pv_strActCode)))
                intSelectIndex = ddlActCode.Items.Count - 1
            End If
        End If

        ddlActCode.SelectedIndex = intSelectIndex
    End Sub

    Sub BindUOM(ByVal pv_strUOMCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_SUBACTIVITY_UOM_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        strParam = "" & "|" & "" & "|"

        Try
            intErrNo = objAdmin.mtdGetUOM(strOpCode, _
                                          strParam, _
                                          objUOMDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBACTIVITY_UOMCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objUOMDs.Tables(0).Rows.Count - 1
            objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode"))
            objUOMDs.Tables(0).Rows(intCnt).Item("UOMDesc") = objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") & " (" & Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMDesc")) & ")"
            If objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(pv_strUOMCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objUOMDs.Tables(0).NewRow()
        dr("UOMCode") = ""
        dr("UOMDesc") = "Select Unit of Measurement"
        objUOMDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlUOM.DataSource = objUOMDs.Tables(0)
        ddlUOM.DataValueField = "UOMCode"
        ddlUOM.DataTextField = "UOMDesc"
        ddlUOM.DataBind()
        ddlUOM.SelectedIndex = intSelectIndex
    End Sub

    Sub AssignMaxLength(ByRef pr_intMaxLen)
        Dim strOpCd As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim intErrNo As Integer
        Dim intMaxLen As Integer

        Try
            intErrNo = objSys.mtdGetConfigInfo(strOpCd, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               objConfigDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTIVITY_GETMAXLENGTH&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_SubActivityDet.aspx")
        End Try

        pr_intMaxLen = CInt(Trim(objConfigDs.Tables(0).Rows(0).Item("SubActLen")))
        txtSubActCode.MaxLength = pr_intMaxLen
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCd_Upd As String = "GL_CLSSETUP_SUBACTIVITY_LIST_UPD"
        Dim strOpCd_Add As String = "GL_CLSSETUP_SUBACTIVITY_LIST_ADD"
        Dim strOpCd_ID_GET As String = "GL_CLSSETUP_SUBACT_GET"
        Dim dsLastRec As New DataSet()

        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If ddlUOM.SelectedItem.Value = "" Then
            lblErrUOM.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            AssignMaxLength(intMaxLen)
            If txtSubActCode.Text.Length = intMaxLen Then
                lblErrLen.Visible = False
            Else
                lblErrLen.Text = "<br>Sub Activity Code should be in " & intMaxLen & " character(s)."
                lblErrLen.Visible = True
                Exit Sub
            End If

            blnIsUpdate = IIf(intStatus = 0, False, True)
            tbcode.Value = strSelSubActID

            If tbcode.Value = "" Then
                tbcode.Value = txtSubActCode.Text
            End If

            strParam = Trim(tbcode.Value) & Chr(9) & _
                        Trim(txtDescription.Text) & Chr(9) & _
                        ddlActCode.SelectedItem.Value & Chr(9) & _
                        ddlUOM.SelectedItem.Value & Chr(9) & _
                        objGLSetup.EnumSubActStatus.Active
            Try
                intErrNo = objGLSetup.mtdUpdSubActivity(strOpCd_Add, _
                                                       strOpCd_Upd, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strParam, _
                                                       blnIsUpdate)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBACTIVITY_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_SubActivityDet.aspx")
            End Try
        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(tbcode.Value) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objGLSetup.EnumSubActStatus.Deleted
            Try
                intErrNo = objGLSetup.mtdUpdSubActivity(strOpCd_Add, _
                                                       strOpCd_Upd, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strParam, _
                                                       True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBACTIVITY_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_SubActivityDet.aspx?tbcode=" & strSelSubActID)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParam = Trim(tbcode.Value) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objGLSetup.EnumSubActStatus.Active
            Try
                intErrNo = objGLSetup.mtdUpdSubActivity(strOpCd_Add, _
                                                       strOpCd_Upd, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strParam, _
                                                       True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBACTIVITY_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_SubActivityDet.aspx?tbcode=" & strSelSubActID)
            End Try
        End If

        If strSelSubActID = "" Then

            Dim strParamGet As String = " WHERE UpdateID = '" & strUserId & "'"
            Try
                intErrNo = objGLSetup.mtdGetSubActivity(strOpCd_ID_GET, _
                                                      strParamGet, _
                                                      dsLastRec, _
                                                      True) 
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBACTIVITY_LIST_GET_BY_SUBACTCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            tbcode.Value = Trim(dsLastRec.Tables(0).Rows(0).Item("SubActID"))
        End If

        If tbcode.Value <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_SubActivity.aspx")
    End Sub


End Class
