
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


Public Class GL_Setup_VehicleDet : Inherits Page

    Protected WithEvents txtVehicleCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlVehType As DropDownList
    Protected WithEvents txtModel As TextBox
    Protected WithEvents txtHPCC As TextBox
    Protected WithEvents ddlUOM As DropDownList

    Protected WithEvents dgLineDet As DataGrid

    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtAccName As TextBox

    Protected WithEvents txtaccDet As TextBox
    Protected WithEvents txtaccNameDet As TextBox

    Protected WithEvents lblStatus As Label
    Protected WithEvents txtPurchaseDate As TextBox
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents FindAcc As HtmlInputButton
    Protected WithEvents FindAcc_Det As HtmlInputButton
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrVehType As Label
    Protected WithEvents lblErrUOM As Label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents lblErrAccCode2 As Label
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehDesc As Label
    Protected WithEvents lblVehType As Label
    Protected WithEvents lblVRA As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator
    Protected hidRecStatus As HtmlInputHidden
    Protected hidOriVehCode As HtmlInputHidden

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsUOM()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLTrx As New agri.GL.clsTrx()

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

    Dim strSelectedVehicleCode As String = ""
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
            lblErrVehType.Visible = False
            lblErrDup.Visible = False
            lblErrUOM.Visible = False
            lblErrAccCode.Visible = False
            lblFmt.Visible = False
            lblDate.Visible = False
            strSelectedVehicleCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)
            onload_GetLangCap()

            If Not IsPostBack Then
                txtAccCode.Attributes.Add("readonly", "readonly")
                txtaccDet.Attributes.Add("readonly", "readonly")
                txtAccName.Attributes.Add("readonly", "readonly")
                txtaccNameDet.Attributes.Add("readonly", "readonly")

                If strSelectedVehicleCode <> "" Then
                    tbcode.Value = strSelectedVehicleCode
                    onLoad_Display()
                    onLoad_LineDisplay()
                    onLoad_BindButton()
                Else
                    BindVehType("")
                    BindUOM("")

                    onLoad_BindButton()
                End If

                If Request.QueryString("tbcode") = "" Then
                    hidRecStatus.Value = "Unsaved"
                Else
                    hidRecStatus.Value = "Saved"
                    hidOriVehCode.Value = Trim(Request.QueryString("tbcode"))
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "GL_CLSSETUP_VEHICLE_GET_BY_VEHCODE"
        Dim strParam As String = strSelectedVehicleCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetVehicle(strOpCd, _
                                                strLocation, _
                                                strParam, _
                                                objVehDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_GET_BY_VEHCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtVehicleCode.Text = Trim(objVehDs.Tables(0).Rows(0).Item("VehCode"))
        txtDescription.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Description"))
        txtModel.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Model"))
        txtHPCC.Text = Trim(objVehDs.Tables(0).Rows(0).Item("HPCC"))
        intStatus = CInt(Trim(objVehDs.Tables(0).Rows(0).Item("Status")))

        lblHiddenSts.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objGLSetup.mtdGetActSatus(Trim(objVehDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objVehDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objVehDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objVehDs.Tables(0).Rows(0).Item("UserName"))

        BindVehType(Trim(objVehDs.Tables(0).Rows(0).Item("VehTypeCode")))
        BindUOM(Trim(objVehDs.Tables(0).Rows(0).Item("UOMCode")))

        txtAccCode.Text = Trim(objVehDs.Tables(0).Rows(0).Item("AccCode"))
        txtAccName.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Acc_Description"))
        txtPurchaseDate.Text = objGlobal.GetShortDate(strDateFMt, Trim(objVehDs.Tables(0).Rows(0).Item("PurchaseDate")))

    End Sub

    Sub onLoad_BindButton()

        txtVehicleCode.Enabled = True
        txtDescription.Enabled = False
        ddlVehType.Enabled = False
        txtModel.Enabled = False
        txtHPCC.Enabled = False
        ddlUOM.Enabled = False
        txtAccCode.Enabled = False
        txtPurchaseDate.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        FindAcc.Disabled = False

        Select Case intStatus
            Case objGLSetup.EnumVehicleStatus.Active
                txtDescription.Enabled = True
                ddlVehType.Enabled = True
                txtModel.Enabled = True
                txtHPCC.Enabled = True
                ddlUOM.Enabled = True
                txtAccCode.Enabled = True
                txtPurchaseDate.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objGLSetup.EnumVehicleStatus.Deleted
                FindAcc.Disabled = True
                UnDelBtn.Visible = True
            Case Else
                txtVehicleCode.Enabled = True
                txtDescription.Enabled = True
                ddlVehType.Enabled = True
                txtModel.Enabled = True
                txtHPCC.Enabled = True
                ddlUOM.Enabled = True
                txtAccCode.Enabled = True
                txtPurchaseDate.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Vehicle))
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code"
        lblVehDesc.Text = GetCaption(objLangCap.EnumLangCap.VehicleDesc)
        lblVehType.Text = GetCaption(objLangCap.EnumLangCap.VehType)
        lblVRA.Text = GetCaption(objLangCap.EnumLangCap.VRA)

        validateCode.ErrorMessage = "<br>Please enter " & lblVehCode.Text & "."
        validateDesc.ErrorMessage = "Please enter " & lblVehDesc.Text & "."
        lblErrAccCode.Text = "Please select " & lblVRA.Text & "."
        lblErrVehType.Text = "<br>Please select " & lblVehType.Text & "."
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
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function

    Sub BindVehType(ByVal pv_strVehType As String)
        Dim strOpCode As String = "GL_CLSSETUP_VEHICLE_VEHTYPECODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSort As String
        Dim strSearch As String

        strSort = "order by VehTypeCode"
        strSearch = "where Status = '" & objGLSetup.EnumVehTypeStatus.Active & "' "

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.VehType, _
                                                   objVehTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_VEHICLETYPECODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objVehTypeDs.Tables(0).Rows.Count - 1
            objVehTypeDs.Tables(0).Rows(intCnt).Item("VehTypeCode") = Trim(objVehTypeDs.Tables(0).Rows(intCnt).Item("VehTypeCode"))
            objVehTypeDs.Tables(0).Rows(intCnt).Item("Description") = objVehTypeDs.Tables(0).Rows(intCnt).Item("VehTypeCode") & " (" & Trim(objVehTypeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objVehTypeDs.Tables(0).Rows(intCnt).Item("VehTypeCode") = Trim(pv_strVehType) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objVehTypeDs.Tables(0).NewRow()
        dr("VehTypeCode") = ""
        dr("Description") = "Select " & lblVehType.Text
        objVehTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehType.DataSource = objVehTypeDs.Tables(0)
        ddlVehType.DataValueField = "VehTypeCode"
        ddlVehType.DataTextField = "Description"
        ddlVehType.DataBind()
        ddlVehType.SelectedIndex = intSelectIndex
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_UOMCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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

    Public Function CheckDate() As String
        Dim objDateFormat As String
        Dim strValidDate As String

        If objGlobal.mtdValidInputDate(strDateFMt, txtPurchaseDate.Text, objDateFormat, strValidDate) = True Then
            Return strValidDate
        Else
            lblFmt.Text = objDateFormat & "."
            lblDate.Visible = True
            lblFmt.Visible = True
        End If
    End Function


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "GL_CLSSETUP_VEHICLE_LIST_UPD"
        Dim strOpCd_Get As String = "GL_CLSSETUP_VEHICLE_GET_BY_VEHCODE"
        Dim strOpCd_Add As String = "GL_CLSSETUP_VEHICLE_ADD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim strVRAAcc As String = Request.Form("txtAccCode")
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""
        Dim strPurchaseDate As String
        Dim blnIsInUse As Boolean = False

        If strVRAAcc = "" Then strVRAAcc = Trim(txtAccCode.Text)

        If ddlVehType.SelectedItem.Value = "" Then
            lblErrVehType.Visible = True
            Exit Sub
        ElseIf ddlUOM.SelectedItem.Value = "" Then
            lblErrUOM.Visible = True
            Exit Sub
        ElseIf strVRAAcc = "" Then
            lblErrAccCode.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            strParam = Trim(txtVehicleCode.Text)
            Try
                intErrNo = objGLSetup.mtdGetVehicle(strOpCd_Get, strLocation, strParam, objVehDs, True)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_CHECK_FOR_DUPLICATION&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objVehDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDup.Visible = True
                Exit Sub
            Else
                If intStatus = 0 Then 
                    strSelectedVehicleCode = Trim(txtVehicleCode.Text)
                    blnIsUpdate = False 
                    tbcode.Value = strSelectedVehicleCode
                Else 
                    If hidOriVehCode.Value = Trim(txtVehicleCode.Text) Then
                        strSelectedVehicleCode = Trim(txtVehicleCode.Text)
                        blnIsUpdate = True 
                        tbcode.Value = strSelectedVehicleCode
                    Else
                        intErrNo = objGLSetup.mtdCheckSetupCodeUsage("VehCode", hidOriVehCode.Value, blnIsInUse)

                        If blnIsInUse = True Then
                            Response.Write("<script language='javascript'>alert('Transaction exists for Vehicle Code " & hidOriVehCode.Value & ". Editing this Vehicle Code is not allowed.')</script>")
                            strSelectedVehicleCode = hidOriVehCode.Value
                            onLoad_Display()
                            onLoad_BindButton()
                            Exit Sub
                        Else
                            intErrNo = objGLSetup.mtdDelPrevCode("VehCode", hidOriVehCode.Value)
                            strSelectedVehicleCode = Trim(txtVehicleCode.Text)
                            blnIsUpdate = False 
                            tbcode.Value = strSelectedVehicleCode
                        End If
                    End If
                End If

                If txtPurchaseDate.Text = "" Then
                    strPurchaseDate = txtPurchaseDate.Text
                Else
                    If CheckDate() = "" Then
                        Exit Sub
                    Else
                        strPurchaseDate = CheckDate()
                    End If
                End If

                strParam = Trim(txtVehicleCode.Text) & Chr(9) & _
                            Trim(txtDescription.Text) & Chr(9) & _
                            ddlVehType.SelectedItem.Value & Chr(9) & _
                            Trim(txtModel.Text) & Chr(9) & _
                            Trim(txtHPCC.Text) & Chr(9) & _
                            ddlUOM.SelectedItem.Value & Chr(9) & _
                            strVRAAcc & Chr(9) & _
                            objGLSetup.EnumVehicleStatus.Active & Chr(9) & _
                            strPurchaseDate & Chr(9) & strLocation
                Try
                    intErrNo = objGLSetup.mtdUpdVehicle(strOpCd_Add, _
                                                        strOpCd_Upd, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        False, _
                                                        blnIsUpdate)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_DETAILS_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleDet.aspx")
                End Try
            End If

            If blnIsInUse = False Then hidOriVehCode.Value = Trim(txtVehicleCode.Text)

        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(txtVehicleCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objGLSetup.EnumVehicleStatus.Deleted & Chr(9) & Chr(9)
            Try
                intErrNo = objGLSetup.mtdUpdVehicle(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True, _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_DETAILS_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleDet.aspx?tbcode=" & strSelectedVehicleCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParam = Trim(txtVehicleCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objGLSetup.EnumVehicleStatus.Active & Chr(9) & Chr(9)
            Try
                intErrNo = objGLSetup.mtdUpdVehicle(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True, _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_DETAILS_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleDet.aspx?tbcode=" & strSelectedVehicleCode)
            End Try
        End If
        If strSelectedVehicleCode <> "" Then
            onLoad_Display()
            onLoad_BindButton()
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_Vehicle.aspx")
    End Sub


    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode As String = "GL_CLSSETUP_VEHICLEACC_LINE_ADD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strAccCode As String
        Dim intErrNo As Integer

        strAccCode = Trim(txtaccDet.Text)

        If strAccCode = "" Then
            Exit Sub
        Else
            Try

                strParamName = "ACCCODE|VEHCODE|LOCCODE"

                strParamValue = strAccCode & "|" & Trim(txtVehicleCode.Text) & "|" & strLocation

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
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCode")
        strAccCode = lblDelText.Text

        Try
            strParamName = "ACCCODE|VEHCODE|LOCCODE"

            strParamValue = strAccCode & "|" & Trim(txtVehicleCode.Text) & "|" & strLocation

            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                        strParamName, _
                                                        strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        onLoad_LineDisplay()

    End Sub


    Sub onLoad_LineDisplay()
        Dim strOpCode As String = "GL_CLSSETUP_VEHICLEACC_LINE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim objRslSet As New DataSet()

        strParamName = "VEHCODE|LOCCODEJOIN|LOCCODE"

        'strParamValue = Trim(txtVehicleCode.Text) & "|" & strLocation
		strParamValue = Trim(txtVehicleCode.Text) & "|" & _ 
		                IIf(Session("SS_COACENTRALIZED") = "1", "", " and a.LocCode = b.LocCode ") & "|" & _
					    IIf(Session("SS_COACENTRALIZED") = "1", " AND a.LocCode = '" & Trim(strLocation) & "' ", " AND LocCode = '" & Trim(strLocation) & "' ")

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
