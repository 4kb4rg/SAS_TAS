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


Public Class GL_Setup_VehicleSubCodeDet : Inherits Page

    Protected WithEvents txtVehExpenseCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents ddlVehExpGrp As DropDownList
    Protected WithEvents ddlUOM As DropDownList
    Protected WithEvents cbRunCost As CheckBox
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
    Protected WithEvents lblErrUOM As Label
    Protected WithEvents lblErrVehExpGrp As Label
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblVehExpDesc As Label
    Protected WithEvents lblVehExpGrp As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsUOM()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim objVehExpenseDs As New Object()
    Dim objUOMDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer

    Dim strSelectedVehExpenseCode As String = ""
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpense), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblErrUOM.visible = False
            lblErrVehExpGrp.Visible = False
            strSelectedVehExpenseCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)
            onload_GetLangCap()
            If Not IsPostBack Then
                If strSelectedVehExpenseCode <> "" Then
                    tbcode.Value = strSelectedVehExpenseCode
                    onLoad_Display()
                    onLoad_BindButton()
                Else
                    BindVehExpGrp("")
                    BindUOM("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "GL_CLSSETUP_VEHICLE_EXPENSE_CODE_GET_BY_VEHEXPENSECODE"
        Dim strParam As String = strSelectedVehExpenseCode        
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetVehExpCode(strOpCd, _
                                                   strParam, _
                                                   objVehExpenseDs, _
                                                   True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_EXPENSE_GET_BY_VEHEXPENSECODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtVehExpenseCode.Text = Trim(objVehExpenseDs.Tables(0).Rows(0).Item("VehExpenseCode"))
        txtDescription.Text = Trim(objVehExpenseDs.Tables(0).Rows(0).Item("Description"))
        intStatus = CInt(Trim(objVehExpenseDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objVehExpenseDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objGLSetup.mtdGetActSatus(Trim(objVehExpenseDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objVehExpenseDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objVehExpenseDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objVehExpenseDs.Tables(0).Rows(0).Item("UserName"))
        BindVehExpGrp(Trim(objVehExpenseDs.Tables(0).Rows(0).Item("VehExpGrpCode")))
        BindUOM(Trim(objVehExpenseDs.Tables(0).Rows(0).Item("UOMCode")))
        If Cint(objVehExpenseDs.Tables(0).Rows(0).Item("RunCostInd")) = objGLSetup.EnumVehRuningCost.Yes Then
            cbRunCost.Checked = True
        Else
            cbRunCost.Checked = False
        End If
    End Sub

    Sub onLoad_BindButton()
        txtVehExpenseCode.Enabled = False
        txtDescription.Enabled = False
        ddlVehExpGrp.Enabled = False
        ddlUOM.Enabled = False
        cbRunCost.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objGLSetup.EnumActStatus.Active
                txtDescription.enabled = True
                ddlVehExpGrp.enabled = True
                ddlUOM.enabled = True
                cbRunCost.Enabled = True
                SaveBtn.visible = True
                DelBtn.visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objGLSetup.EnumActStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtVehExpenseCode.Enabled = True
                txtDescription.Enabled = True
                ddlVehExpGrp.Enabled = True
                ddlUOM.Enabled = True
                cbRunCost.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.VehExpense))
        lblVehExpCode.text = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code"
        lblVehExpDesc.text = GetCaption(objLangCap.EnumLangCap.VehExpenseDesc)
        lblVehExpGrp.text = GetCaption(objLangCap.EnumLangCap.VehExpGrp) & " Code"
        validateCode.ErrorMessage = "<br>Please enter " & lblVehExpCode.text & "."
        validateDesc.ErrorMessage = "Please enter " & lblVehExpDesc.text & "."
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleSubCodeDet.aspx")
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

    Sub BindVehExpGrp(ByVal pv_strVehExpGrpCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_VEHSUBGROUP_LIST_SEARCH"
        Dim objDataSet As New Dataset()
        Dim strParam As String 
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        strParam = "ORDER BY Acc.VehExpGrpCode ASC|AND Acc.Status = '" & objGLSetup.EnumVehicleExpenseGrpStatus.Active & "'"

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.VehicleExpenseGrp, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_EXPENSE_GET_VEHEXPGRP&errmesg=" & Exp.ToString() & "&redirect=GL/Setup/GL_Setup_VehicleSubCodeDet.aspx")
        End Try
        
        For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
            objDataSet.Tables(0).Rows(intCnt).Item("VehExpGrpCode") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("VehExpGrpCode"))
            objDataSet.Tables(0).Rows(intCnt).Item("Description") = objDataSet.Tables(0).Rows(intCnt).Item("VehExpGrpCode") & " (" & Trim(objDataSet.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDataSet.Tables(0).Rows(intCnt).Item("VehExpGrpCode") = Trim(pv_strVehExpGrpCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objDataSet.Tables(0).NewRow()
        dr("VehExpGrpCode") = ""
        dr("Description") = "Select " & lblVehExpGrp.text
        objDataSet.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehExpGrp.DataSource = objDataSet.Tables(0)
        ddlVehExpGrp.DataValueField = "VehExpGrpCode"
        ddlVehExpGrp.DataTextField = "Description"
        ddlVehExpGrp.DataBind()
        ddlVehExpGrp.SelectedIndex = intSelectIndex
        objDataSet = Nothing
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_EXPENSE_DETAILS_UOMCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)

        Dim strOpCd_Upd As String = "GL_CLSSETUP_VEHICLE_EXPENSE_CODE_UPD"
        Dim strOpCd_Get As String = "GL_CLSSETUP_VEHICLE_EXPENSE_CODE_GET_BY_VEHEXPENSECODE"
        Dim strOpCd_Add As String = "GL_CLSSETUP_VEHICLE_EXPENSE_CODE_ADD"
        Dim intRunCost As Integer
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If ddlVehExpGrp.SelectedItem.value = "" Then
            lblErrVehExpGrp.visible = True
            Exit Sub
        ElseIf ddlUOM.SelectedItem.value = "" Then
            lblErrUOM.visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            intRunCost = IIf(cbRunCost.Checked = True, objGLSetup.EnumVehRuningCost.Yes, objGLSetup.EnumVehRuningCost.No)

            strParam = Trim(txtVehExpenseCode.Text)
            Try
                intErrNo = objGLSetup.mtdGetVehExpCode(strOpCd_Get, _
                                                       strParam, _
                                                       objVehExpenseDs, _
                                                       True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_EXPENSE_DETAILS_GET_BY_VEHEXPENSECODE&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        
            If objVehExpenseDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDup.Visible = True
                Exit Sub
            Else
                strSelectedVehExpenseCode = Trim(txtVehExpenseCode.Text)
                blnIsUpdate = IIf(intStatus = 0, False, True)
                tbcode.Value = strSelectedVehExpenseCode           
                strParam = Trim(txtVehExpenseCode.Text) & Chr(9) & _
                           Trim(txtDescription.Text) & Chr(9) & _
                           ddlVehExpGrp.SelectedItem.value & Chr(9) & _
                           ddlUOM.SelectedItem.value & Chr(9) & _
                           intRunCost & Chr(9) & _
                           objGLSetup.EnumActStatus.Active
                Try
                    intErrNo = objGLSetup.mtdUpdVehExpCode(strOpCd_Add, _
                                                           strOpCd_Upd, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strParam, _
                                                           blnIsUpdate)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_EXPENSE_DETAILS_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleSubCodeDet.aspx")
                End Try
            End If
        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(txtVehExpenseCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objGLSetup.EnumActStatus.Deleted
            Try
                intErrNo = objGLSetup.mtdUpdVehExpCode(strOpCd_Add, _
                                                     strOpCd_Upd, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_EXPENSE_DETAILS_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleSubCodeDet.aspx?tbcode=" & strSelectedVehExpenseCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParam = Trim(txtVehExpenseCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objGLSetup.EnumActStatus.Active
            Try
                intErrNo = objGLSetup.mtdUpdVehExpCode(strOpCd_Add, _
                                                     strOpCd_Upd, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_EXPENSE_DETAILS_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleSubCodeDet.aspx?tbcode=" & strSelectedVehExpenseCode)
            End Try
        End If
        If strSelectedVehExpenseCode <> "" Then
            onLoad_Display()
            onLoad_BindButton()
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_VehicleSubCode.aspx")
    End Sub


End Class
