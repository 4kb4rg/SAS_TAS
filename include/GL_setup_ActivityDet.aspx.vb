

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
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class GL_Setup_ActivityDet : Inherits Page

    Protected WithEvents txtActCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents ddlActGrpCode As DropDownList
    Protected WithEvents ddlUOM As DropDownList
    Protected WithEvents cbLabourOverhead As CheckBox
    Protected WithEvents txtQuotaIncRate As TextBox
    Protected WithEvents txtQuota As TextBox
    Protected WithEvents rbByHour As RadioButton
    Protected WithEvents rbByVolume As RadioButton
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrActCode As Label
    Protected WithEvents lblErrUOM As Label
    Protected WithEvents lblErrLen As Label
    Protected WithEvents ddlAccCls As Dropdownlist
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblErrAccCls As Label
    Protected WithEvents lblActCode As Label
    Protected WithEvents lblActDesc As Label
    Protected WithEvents lblActGrp As Label
    Protected WithEvents lblAccClass As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblQuotaInc As Label
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
        
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsUOM()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objHRTrx As New agri.HR.clsTrx()

    Dim objActDs As New Object()
    Dim objActGrpDs As New Object()
    Dim objUOMDs As New Object()
    Dim objActLnDs As New Object()
    Dim objAccDs As New Object()
    Dim objConfigDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer

    Dim strSelectedActCode As String = ""
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivity), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrAccCls.Visible = False
            lblErrDup.Visible = False
            lblErrActCode.visible = False
            lblErrUOM.visible = False
            strSelectedActCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)
            onload_GetLangCap()
            If Not IsPostBack Then
                If strSelectedActCode <> "" Then
                    tbcode.Value = strSelectedActCode
                    onLoad_Display()
                    onLoad_LineDisplay()
                    BindAccCls()
                    onLoad_BindButton()
                Else
                    AssignMaxLength(intMaxLen)
                    BindActGrpCode("")
                    BindUOM("")
                    BindAccCls()
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "GL_CLSSETUP_ACTIVITY_LIST_GET_BY_ACTCODE"
        Dim strParam As String        
        Dim intErrNo As Integer

        strParam = strSelectedActCode & "|" & ""
        Try
            intErrNo = objGLSetup.mtdGetActivity(strOpCd, _
                                                 strParam, _
                                                 objActDs, _
                                                 True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACTIVITY_LIST_GET_BY_ACTCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtActCode.Text = Trim(objActDs.Tables(0).Rows(0).Item("ActCode"))
        txtDescription.Text = Trim(objActDs.Tables(0).Rows(0).Item("Description"))
        txtQuota.Text = objActDs.Tables(0).Rows(0).Item("Quota")

        txtQuotaIncRate.Text = ObjGlobal.DisplayForEditCurrencyFormat(objActDs.Tables(0).Rows(0).Item("QuotaIncRate"))
        intStatus = CInt(Trim(objActDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objActDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objGLSetup.mtdGetActSatus(Trim(objActDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objActDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objActDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objActDs.Tables(0).Rows(0).Item("UserName"))
        
        BindActGrpCode(Trim(objActDs.Tables(0).Rows(0).Item("ActGrpCode")))
        BindUOM(Trim(objActDs.Tables(0).Rows(0).Item("UOMCode")))

        If CInt(objActDs.Tables(0).Rows(0).Item("LabOverheadInd")) = objGLSetup.EnumLabOverheadInd.Yes Then
            cbLabourOverhead.Checked = True
        Else
            cbLabourOverhead.Checked = False
        End If

        If CInt(objActDs.Tables(0).Rows(0).Item("QuotaMethod")) = objHRTrx.EnumQuotaMethod.ByHour Then
            rbByHour.Checked = True
        Else
            rbByVolume.Checked = True
        End If
    End Sub

    Sub onLoad_BindButton()
        txtActCode.Enabled = False
        txtDescription.Enabled = False
        ddlActGrpCode.Enabled = False
        ddlUOM.Enabled = False
        cbLabourOverhead.Enabled = False
        txtQuota.Enabled = False
        txtQuotaIncRate.Enabled = False
        rbByHour.Enabled = False
        rbByVolume.Enabled = False
        tblSelection.Visible = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objGLSetup.EnumActStatus.Active
                txtDescription.Enabled = True
                ddlActGrpCode.Enabled = True
                ddlUOM.Enabled = True
                cbLabourOverhead.Enabled = True
                txtQuota.Enabled = True
                txtQuotaIncRate.Enabled = True
                rbByHour.Enabled = True
                rbByVolume.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objGLSetup.EnumActStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtActCode.Enabled = True
                txtDescription.Enabled = True
                ddlActGrpCode.Enabled = True
                ddlUOM.Enabled = True
                cbLabourOverhead.Enabled = True
                txtQuota.Enabled = True
                txtQuotaIncRate.Enabled = True
                rbByHour.Enabled = True
                rbByVolume.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
        End Select
    End Sub



    Sub BindActGrpCode(ByVal pv_strActCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_ACTGRP_SEARCH"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSort As String
        Dim strSearch As String
        
        strSort = "ORDER BY Act.ActGrpCode"
        strSearch = "AND Act.Status = '" & objGLSetup.EnumActGrpStatus.Active & "' "
        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.ActGrp, _
                                                   objActGrpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTIVITY_ACTGRPCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For intCnt = 0 To objActGrpDs.Tables(0).Rows.Count - 1
            objActGrpDs.Tables(0).Rows(intCnt).Item("ActGrpCode") = Trim(objActGrpDs.Tables(0).Rows(intCnt).Item("ActGrpCode"))
            objActGrpDs.Tables(0).Rows(intCnt).Item("Description") = objActGrpDs.Tables(0).Rows(intCnt).Item("ActGrpCode") & " (" & Trim(objActGrpDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objActGrpDs.Tables(0).Rows(intCnt).Item("ActGrpCode") = Trim(pv_strActCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActGrpDs.Tables(0).NewRow()
        dr("ActGrpCode") = ""
        dr("Description") = "Select " & lblActGrp.text
        objActGrpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlActGrpCode.DataSource = objActGrpDs.Tables(0)
        ddlActGrpCode.DataValueField = "ActGrpCode"
        ddlActGrpCode.DataTextField = "Description"
        ddlActGrpCode.DataBind()
        ddlActGrpCode.SelectedIndex = intSelectIndex
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTIVITY_UOMCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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

    Sub onLoad_LineDisplay()
        Dim strOpCd As String = "GL_CLSSETUP_ACT_ACCCLSLINE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        strParam = strSelectedActCode & "|" & objGLSetup.EnumAccClsStatus.Active
        Try
            intErrNo = objGLSetup.mtdGetActivity(strOpCd, _
                                                 strParam, _
                                                 objActLnDs, _
                                                 True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACCCLSLINE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLineDet.DataSource = objActLnDs.Tables(0)
        dgLineDet.DataBind()
        For intCnt = 0 To dgLineDet.Items.Count - 1
            Select Case CInt(objActDs.Tables(0).Rows(0).Item("Status"))
                Case objGLSetup.EnumActStatus.Active
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                Case objGLSetup.EnumActStatus.Deleted
                     lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                     lbButton.Visible = False
            End Select
        Next

    End Sub

    Sub BindAccCls()
        Dim strOpCd_ACC As String = "GL_CLSSETUP_ACT_ACCCLSLIST_GET"
        Dim strParam As String = strSelectedActCode        
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Try
            strParam = strSelectedActCode & "|"
            intErrNo = objGLSetup.mtdUpdActivityLine("", _
                                                     strOpCd_ACC, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     True, _
                                                     objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTLINE_GETACC&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("AccClsCode") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccClsCode"))
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = objAccDs.Tables(0).Rows(intCnt).Item("AccClsCode") & " (" & Trim(objAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next

        Dim dr As DataRow
        dr = objAccDs.Tables(0).NewRow()
        dr("AccClsCode") = ""
        dr("Description") = "Select " & lblAccClass.text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccCls.DataSource = objAccDs.Tables(0)
        ddlAccCls.DataValueField = "AccClsCode"
        ddlAccCls.DataTextField = "Description"
        ddlAccCls.DataBind()
    End Sub

    Sub InsertActRecord()
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strOpCd_Upd As String = "GL_CLSSETUP_ACTIVITY_LIST_UPD"
        Dim strOpCd_Get As String = "GL_CLSSETUP_ACTIVITY_LIST_GET_BY_ACTCODE"
        Dim strOpCd_Add As String = "GL_CLSSETUP_ACTIVITY_LIST_ADD"
        Dim intLabOverheadInd As Integer
        Dim intQuotaMethod As Integer
        
        AssignMaxLength(intMaxLen)
        If txtActCode.Text.Length = intMaxLen Then
            lblErrLen.visible = False
        Else
            lblErrLen.text = "<br>The Activity Code should be in " & intMaxLen & " character(s)."
            lblErrLen.visible = True
            Exit Sub
        End If

        strParam = Trim(txtActCode.Text) & "|" & ""
        Try
            intErrNo = objGLSetup.mtdGetActivity(strOpCd_Get, _
                                                 strParam, _
                                                 objActDs, _
                                                 True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTIVITY_GET_BY_SUBACTCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        If objActDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
            lblErrDup.Visible = True
            Exit Sub
        Else
            strSelectedActCode = Trim(txtActCode.Text)
            blnIsUpdate = IIf(intStatus = 0, False, True)
            tbcode.Value = strSelectedActCode   

            If cbLabourOverhead.Checked Then
                intLabOverheadInd = objGLSetup.EnumLabOverheadInd.Yes
            Else
                intLabOverheadInd = objGLSetup.EnumLabOverheadInd.No
            End If  

            If rbByHour.Checked Then
                intQuotaMethod = objHRTrx.EnumQuotaMethod.ByHour
            Else
                intQuotaMethod = objHRTrx.EnumQuotaMethod.ByVolume
            End If
      
           
            strParam = Trim(txtActCode.Text) & Chr(9) & _
                        Trim(txtDescription.Text) & Chr(9) & _
                        ddlActGrpCode.SelectedItem.value & Chr(9) & _
                        ddlUOM.SelectedItem.value & Chr(9) & _
                        CStr(intLabOverheadInd) & Chr(9) & _
                        txtQuota.Text & Chr(9) & _
                        intQuotaMethod & Chr(9) & _
                        IIf(Trim(txtQuotaIncRate.Text) = "", 0, Trim(txtQuotaIncRate.Text)) & Chr(9) & _
                        objGLSetup.EnumActStatus.Active 
            
            Try
                intErrNo = objGLSetup.mtdUpdActivity(strOpCd_Add, _
                                                     strOpCd_Upd, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     blnIsUpdate)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTIVITY_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_ActivityDet.aspx")
            End Try
        End If
    End Sub

    Sub AssignMaxLength(ByRef pr_intMaxValue)
        Dim strOpCd As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim intErrNo As Integer

        Try
            intErrNo = objSys.mtdGetConfigInfo(strOpCd, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               objConfigDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTIVITY_GETMAXLENGTH&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_ActivityDet.aspx")
        End Try

        pr_intMaxValue = CInt(Trim(objConfigDs.Tables(0).Rows(0).Item("ActLen")))
        txtActCode.maxlength = pr_intMaxValue
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)

        Dim strOpCd_Upd As String = "GL_CLSSETUP_ACTIVITY_LIST_UPD"
        Dim strOpCd_Get As String = "GL_CLSSETUP_ACTIVITY_LIST_GET_BY_ACTCODE"
        Dim strOpCd_Add As String = "GL_CLSSETUP_ACTIVITY_LIST_ADD"

        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If ddlActGrpCode.SelectedItem.Value = "" Then
            lblErrActCode.Visible = True
            Exit Sub
        End If
        
        If ddlUOM.SelectedItem.value = "" Then
            lblErrUOM.visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            InsertActRecord()
        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(txtActCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objGLSetup.EnumActStatus.Deleted
            Try
                intErrNo = objGLSetup.mtdUpdActivity(strOpCd_Add, _
                                                     strOpCd_Upd, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTIVITY_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_ActivityDet.aspx?tbcode=" & strSelectedActCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParam = Trim(txtActCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objGLSetup.EnumActStatus.Active
            Try
                intErrNo = objGLSetup.mtdUpdActivity(strOpCd_Add, _
                                                     strOpCd_Upd, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTIVITY_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_ActivityDet.aspx?tbcode=" & strSelectedActCode)
            End Try
        End If
        If strSelectedActCode <> "" Then
            onLoad_Display()
            onLoad_LineDisplay()
            BindAccCls()
            onLoad_BindButton()
        End If
    End Sub

    Sub btnAdd_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_AddLine As String = "GL_CLSSETUP_ACT_LINE_ADD"
        Dim strOpCode_UpdID As String = "GL_CLSSETUP_ACT_ACCCLSLIST_UPDATEID"
        Dim strParam As String
        Dim strAccClsCode As String
        Dim intErrNo As Integer

        strAccClsCode = ddlAccCls.SelectedItem.Value
        If strAccClsCode = "" Then
            lblErrAccCls.Visible = True
            Exit sub
        End If

        InsertActRecord()
        If strSelectedActCode = "" Then
            Exit Sub
        Else
            Try
                strParam = strSelectedActCode & "|" & strAccClsCode
                intErrNo = objGLSetup.mtdUpdActivityLine(strOpCode_UpdID, _
                                                         strOpCode_AddLine, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strParam, _
                                                         False, _
                                                         objResult)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTLINE_ADD&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_ActivityDet.aspx?tbcode=" & strSelectedActCode)
            End Try
        End If

        onLoad_Display()
        onLoad_LineDisplay()
        BindAccCls()
        onLoad_BindButton()
    End Sub


    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "GL_CLSSETUP_ACT_ACCCLSLIST_DEL"
        Dim strOpCode_UpdID As String = "GL_CLSSETUP_ACT_ACCCLSLIST_UPDATEID"
        Dim strParam As String
        Dim lblDelText As Label
        Dim strAccClsCode As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCode")
        strAccClsCode = lblDelText.Text

        Try
            strParam = strSelectedActCode & "|" & strAccClsCode
            intErrNo = objGLSetup.mtdUpdActivityLine(strOpCode_UpdID, _
                                                     strOpCode_DelLine, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     False, _
                                                     objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTLINE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_activitydet.aspx?tbcode=" & strSelectedActCode)
        End Try

        onLoad_Display()
        onLoad_LineDisplay()
        BindAccCls()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_Activity.aspx")
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.Activity))
        lblActCode.text = GetCaption(objLangCap.EnumLangCap.Activity) & " Code"
        lblActDesc.text = GetCaption(objLangCap.EnumLangCap.ActDesc)
        lblActGrp.text = GetCaption(objLangCap.EnumLangCap.ActGrp)
        lblAccClass.text = GetCaption(objLangCap.EnumLangCap.AccClass)
        lblQuotaInc.text = GetCaption(objLangCap.EnumLangCap.QuotaIncentive)
        validateCode.ErrorMessage = "<br>Please enter " & lblActCode.text & "."
        validateDesc.ErrorMessage = "Please enter " & lblActDesc.text & "."
        lblErrActCode.text = "Please select " & lblActGrp.text & "."
        lblErrAccCls.text = "Please select " & lblAccClass.text & "."

        dgLineDet.Columns(0).HeaderText = lblAccClass.text & " Code"
        dgLineDet.Columns(1).HeaderText = GetCaption(objLangCap.EnumLangCap.AccClassDesc)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_ActivityDet.aspx")
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



End Class
