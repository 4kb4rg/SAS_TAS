
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


Public Class GL_Setup_VehActDet : Inherits Page

    Protected WithEvents txtVehActCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblVehActCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator
    Protected hidRecStatus As HtmlInputHidden
    Protected hidOriVehActCode As HtmlInputHidden

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsUom()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLTrx As New agri.GL.ClsTrx()

    Dim objVehDs As New Object()
    Dim objVehTypeDs As New Object()
    Dim objUOMDs As New Object()
    Dim objAccDs As New Object()
    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strDateFMt As String

    Dim strSelectedVehActCode As String = ""
    Dim intStatus As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strDateFMt = Session("SS_DATEFMT")

        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblAccCodeErr.Visible = False
            strSelectedVehActCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)
            onload_GetLangCap()

            If Not IsPostBack Then
                BindAccCode("")

                If strSelectedVehActCode <> "" Then
                    tbcode.Value = strSelectedVehActCode
                    onLoad_Display()
                    onLoad_LineDisplay()
                    onLoad_BindButton()
                Else
                    onLoad_BindButton()
                End If



                If Request.QueryString("tbcode") = "" Then
                    hidRecStatus.Value = "Unsaved"
                Else
                    hidRecStatus.Value = "Saved"
                    hidOriVehActCode.Value = Trim(Request.QueryString("tbcode"))
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "GL_CLSSETUP_VEHACTIVITY_SEARCH"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "VEHACTCODE|LOCCODE"
        strParamValue = Trim(strSelectedVehActCode) & "|" & strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objVehDs)
           
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_GET_BY_VEHCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtVehActCode.Text = Trim(objVehDs.Tables(0).Rows(0).Item("VehActCode"))
        txtDescription.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Description"))
        intStatus = CInt(Trim(objVehDs.Tables(0).Rows(0).Item("Status")))

        lblHiddenSts.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objGLSetup.mtdGetActSatus(Trim(objVehDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objVehDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objVehDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objVehDs.Tables(0).Rows(0).Item("UserName"))

    End Sub

    Sub onLoad_BindButton()

        txtVehActCode.Enabled = True
        txtDescription.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objGLSetup.EnumVehicleStatus.Active
                txtDescription.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objGLSetup.EnumVehicleStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtVehActCode.Enabled = True
                txtDescription.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Vehicle))
        lblVehActCode.Text = GetCaption(objLangCap.EnumLangCap.Activity) & " " & GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code"
        lblDescription.Text = GetCaption(objLangCap.EnumLangCap.Activity) & " " & GetCaption(objLangCap.EnumLangCap.VehicleDesc)
        lblAccCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Account) & " Code"

        validateCode.ErrorMessage = "<br>Please enter " & lblVehActCode.Text & "."
        validateDesc.ErrorMessage = "Please enter " & lblDescription.Text & "."
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleDet.aspx")
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

    Sub BindAccCode(ByVal pv_strSubBlkCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_VEHACTIVITY_SEARCH_ACCOUNT"
        Dim strParamName As String
        Dim strParamValue As String
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim dsForDropDown As New DataSet()
        Dim intSelectedIndex As Integer = 0

        strParamName = "LOCCODE"
        strParamValue = strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Please select " & lblAccCodeTag.Text
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstAccCode.DataSource = dsForDropDown.Tables(0)
        lstAccCode.DataValueField = "AccCode"
        lstAccCode.DataTextField = "_Description"
        lstAccCode.DataBind()
        lstAccCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "GL_CLSSETUP_VEHACTIVITY_UPD"
        Dim strOpCd_Get As String = "GL_CLSSETUP_VEHACTIVITY_SEARCH"
        Dim strOpCd_Add As String = "GL_CLSSETUP_VEHACTIVITY_ADD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim strBlock As String = Request.Form("lstBlock")
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim blnIsInUse As Boolean = False
        
        If strCmdArgs = "Save" Then
            strParamName = "VEHACTCODE|LOCCODE"
            strParamValue = Trim(txtVehActCode.Text) & "|" & strLocation

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objVehDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHACT_CHECK_FOR_DUPLICATION&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objVehDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDup.Visible = True
                Exit Sub
            Else
                If intStatus = 0 Then
                    strSelectedVehActCode = Trim(txtVehActCode.Text)
                    blnIsUpdate = False
                    tbcode.Value = strSelectedVehActCode
                Else
                    If hidOriVehActCode.Value = Trim(txtVehActCode.Text) Then
                        strSelectedVehActCode = Trim(txtVehActCode.Text)
                        blnIsUpdate = True
                        tbcode.Value = strSelectedVehActCode
                    Else
                        intErrNo = objGLSetup.mtdCheckSetupCodeUsage("VehCode", hidOriVehActCode.Value, blnIsInUse)

                        If blnIsInUse = True Then
                            Response.Write("<script language='javascript'>alert('Transaction exists for Vehicle Activity Code " & hidOriVehActCode.Value & ". Editing this Vehicle Code is not allowed.')</script>")
                            strSelectedVehActCode = hidOriVehActCode.Value
                            onLoad_Display()
                            onLoad_BindButton()
                            Exit Sub
                        Else
                            intErrNo = objGLSetup.mtdDelPrevCode("VehActCode", hidOriVehActCode.Value)
                            strSelectedVehActCode = Trim(txtVehActCode.Text)
                            blnIsUpdate = False
                            tbcode.Value = strSelectedVehActCode
                        End If
                    End If
                End If


                strParamName = "VEHACTCODE|DESCRIPTION|LOCCODE|STATUS|UPDATEID"
                strParamValue = Trim(txtVehActCode.Text) & "|" & Trim(txtDescription.Text) & "|" & strLocation & "|" & objGLSetup.EnumVehicleStatus.Active & "|" & Trim(strUserId)

                Try
                    intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, _
                                                             strParamName, _
                                                             strParamValue)

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_DETAILS_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleDet.aspx")
                End Try
            End If

            If blnIsInUse = False Then hidOriVehActCode.Value = Trim(txtVehActCode.Text)

        ElseIf strCmdArgs = "Del" Then
            strParamName = "VEHACTCODE|DESCRIPTION|LOCCODE|STATUS|UPDATEID"
            strParamValue = Trim(txtVehActCode.Text) & "|" & Trim(txtDescription.Text) & "|" & strLocation & "|" & objGLSetup.EnumVehicleStatus.Deleted & "|" & Trim(strUserId)


            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, _
                                                         strParamName, _
                                                         strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_DETAILS_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleDet.aspx?tbcode=" & strSelectedVehActCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParamName = "VEHACTCODE|DESCRIPTION|LOCCODE|STATUS|UPDATEID"
            strParamValue = Trim(txtVehActCode.Text) & "|" & Trim(txtDescription.Text) & "|" & strLocation & "|" & objGLSetup.EnumVehicleStatus.Active & "|" & Trim(strUserId)

            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, _
                                                          strParamName, _
                                                          strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_DETAILS_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleDet.aspx?tbcode=" & strSelectedVehActCode)
            End Try
        End If
        If strSelectedVehActCode <> "" Then
            onLoad_Display()
            onLoad_BindButton()
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_Vehicle.aspx")
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode As String = "GL_CLSSETUP_VEHACTIVITY_ADD_LINE"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strAccCode As String
        Dim strSubBlkCode As String
        Dim intErrNo As Integer

        strAccCode = lstAccCode.SelectedItem.Value
        If strAccCode = "" Then
            Exit Sub
        Else
            Try
                strParamName = "VEHACTCODE|ACCCODE|LOCCODE"
                strParamValue = Trim(txtVehActCode.Text) & "|" & strAccCode & "|" & strLocation

                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                         strParamName, _
                                                         strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try
        End If

        onLoad_LineDisplay()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCode As String = "GL_CLSSETUP_VEHICLEACC_LINE_DEL"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim lblDelText As Label
        Dim strAccCode As String
        Dim strSubBlkCode As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAccCode")
        strAccCode = lblDelText.Text

        Try
            strParamName = "VEHACTCODE|ACCCODE|LOCCODE"
            strParamValue = Trim(txtVehActCode.Text) & "|" & strAccCode & "|" & strLocation

            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                        strParamName, _
                                                        strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        onLoad_LineDisplay()
    End Sub

    Sub onLoad_LineDisplay()
        Dim strOpCode As String = "GL_CLSSETUP_VEHACTIVITY_SEARCH_LINE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objRslSet As New DataSet()

        strParamName = "VEHACTCODE|LOCCODE"
        strParamValue = Trim(txtVehActCode.Text) & "|" & strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objRslSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dgLineDet.DataSource = objRslSet.Tables(0)
        dgLineDet.DataBind()
    End Sub

End Class
