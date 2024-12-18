
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


Public Class GL_Setup_ALKOHDet : Inherits Page

    Protected WithEvents txtVehicleCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents ddlAccCode2 As DropDownList
    Protected WithEvents ddlAccCode3 As DropDownList
    Protected WithEvents ddlBlkType As DropDownList
	Protected WithEvents ddlIP As DropDownList
	
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents dgLineDRDet As DataGrid
	Protected WithEvents dgLineUnit As DataGrid
	
	Protected WithEvents chkunit As Checkbox
	Protected WithEvents chkplasma As Checkbox
	
	
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents btnFind As HtmlInputButton
    Protected WithEvents btnFind2 As HtmlInputButton
    Protected WithEvents btnFind3 As HtmlInputButton
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents btnAddDR As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents lblErrAccCode2 As Label
    Protected WithEvents lblErrAccCode3 As Label
    Protected WithEvents lblErrBlkType As Label
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

	Protected WithEvents TRUnit as HtmlTableRow
	Protected WithEvents ddlunit as DropDownList
	Protected WithEvents txtAccCode3 as textbox
	Protected WithEvents txtyr as textbox
	Protected WithEvents txtyr2  as textbox
	Protected WithEvents txtarea as textbox
	
    Protected objGLSetup As New agri.GL.clsSetup()
	Protected objAdminSetup As New agri.Admin.clsLoc()
	Protected objPU As New agri.PU.clsTrx()
	
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

    Dim strSelectedVehicleCode As String = ""
    Dim intStatus As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        intGLAR = Session("SS_GLAR")
        strDateFMt = Session("SS_DATEFMT")

        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblErrAccCode.Visible = False
            'lblFmt.Visible = False
            'lblDate.Visible = False
            strSelectedVehicleCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)
            onload_GetLangCap()

            If Not IsPostBack Then
			    TRUnit.Visible = False
				txtyr.Text = strAccYear
				txtyr2.Text = strAccYear
                If strSelectedVehicleCode <> "" Then
                    tbcode.Value = strSelectedVehicleCode
					BindUnit()
                    onLoad_Display()
                    onLoad_LineDisplay()
                    onLoad_LineDisplayDR()
					onLoad_LineHistori()
                    onLoad_BindButton()
                Else
                    BindAccCode("")
					BindUnit()
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
        Dim strOpCd As String = "GL_CLSSETUP_ALKOH_GET_BY_ALKOHCODE"
        Dim strParam As String = strSelectedVehicleCode
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "ALKOHCODE|LOCCODE"
        strParamValue = Trim(tbcode.Value) & "|" & Trim(strLocation)

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objVehDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_GET_BY_VEHCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtVehicleCode.Text = Trim(objVehDs.Tables(0).Rows(0).Item("AlkohCode"))
        txtDescription.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Description"))
        intStatus = CInt(Trim(objVehDs.Tables(0).Rows(0).Item("Status")))

        lblHiddenSts.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objGLSetup.mtdGetActSatus(Trim(objVehDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objVehDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objVehDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objVehDs.Tables(0).Rows(0).Item("UserName"))
		chkunit.checked = Cbool(Trim(objVehDs.Tables(0).Rows(0).Item("isUnit")))
		chkplasma.checked = Cbool(Trim(objVehDs.Tables(0).Rows(0).Item("isPlasma")))
        BindAccCode(Trim(objVehDs.Tables(0).Rows(0).Item("AccCode")))
		TRunit.visible = chkunit.Checked
		
    End Sub

    Sub onLoad_BindButton()

        txtVehicleCode.Enabled = True
        txtDescription.Enabled = False
        ddlAccCode.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        btnFind.Disabled = False

        Select Case intStatus
            Case objGLSetup.EnumVehicleStatus.Active
                txtVehicleCode.Enabled = False
                txtDescription.Enabled = True
                ddlAccCode.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objGLSetup.EnumVehicleStatus.Deleted
                btnFind.Disabled = True
                UnDelBtn.Visible = True
            Case Else
                txtVehicleCode.Enabled = True
                txtDescription.Enabled = True
                ddlAccCode.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblTitle.Text = "ALOKASI OVERHEAD"
        lblVehCode.Text = "Alokasi Overhead Code"
        lblVehDesc.Text = "Deskripsi"
        lblVRA.Text = "COA Alokasi (CR)"

        validateCode.ErrorMessage = "<br>Please enter " & lblVehCode.Text & "."
        validateDesc.ErrorMessage = "Please enter " & lblVehDesc.Text & "."
        lblErrAccCode.Text = "Please select " & lblVRA.Text & "."
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

	Protected Sub DDListUnit(ByVal sender As Object, ByVal e As System.EventArgs)
		txtAccCode3.text = ddlunit.selectedItem.Value.trim()
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

    Sub BindAccCode(ByVal pv_strAccCode As String)

        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Select Akun"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccCode.DataSource = dsForDropDown.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "_Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectedIndex

        ddlAccCode2.DataSource = dsForDropDown.Tables(0)
        ddlAccCode2.DataValueField = "AccCode"
        ddlAccCode2.DataTextField = "_Description"
        ddlAccCode2.DataBind()
        ddlAccCode2.SelectedIndex = 0

        ddlAccCode3.DataSource = dsForDropDown.Tables(0)
        ddlAccCode3.DataValueField = "AccCode"
        ddlAccCode3.DataTextField = "_Description"
        ddlAccCode3.DataBind()
        ddlAccCode3.SelectedIndex = 0

    End Sub

	Sub BindUnit()
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim dsForDropDown As DataSet

        strParam = "" & "|" & objAdminSetup.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, dsForDropDown, "")

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("LocCode") = "-"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlunit.DataSource = dsForDropDown.Tables(0)
        ddlunit.DataValueField = "AccCode"
        ddlunit.DataTextField = "LocCode"
        ddlunit.DataBind()
        

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If

    End Sub
	
    Public Function CheckDate() As String
        'Dim objDateFormat As String
        'Dim strValidDate As String

        'If objGlobal.mtdValidInputDate(strDateFMt, txtPurchaseDate.Text, objDateFormat, strValidDate) = True Then
        '    Return strValidDate
        'Else
        '    lblFmt.Text = objDateFormat & "."
        '    lblDate.Visible = True
        '    lblFmt.Visible = True
        'End If
    End Function


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "GL_CLSSETUP_ALKOH_LIST_UPD"
        Dim strOpCd_Get As String = "GL_CLSSETUP_ALKOH_GET_BY_ALKOHCODE"
        Dim strOpCd_Add As String = "GL_CLSSETUP_ALKOH_ADD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim strVRAAcc As String = Request.Form("ddlAccCode")
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""
        Dim blnIsInUse As Boolean = False
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        If strVRAAcc = "" Then strVRAAcc = ddlAccCode.SelectedItem.Value

        If strVRAAcc = "" Then
            lblErrAccCode.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            strParamName = "ALKOHCODE|LOCCODE"
            strParamValue = Trim(txtVehicleCode.Text) & "|" & Trim(strLocation)

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get, _
                                                        strParamName, _
                                                        strParamValue, _
                                                        objVehDs)

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
                    End If
                End If

                Try
                    strParamName = "ALKOHCODE|DESCRIPTION|ACCCODE|STATUS|UPDATEID|LOCCODE|ISUNIT|ISPLASMA"
                    strParamValue = Trim(txtVehicleCode.Text) & "|" & _
                                    Trim(txtDescription.Text) & "|" & _
                                    strVRAAcc & "|" & objGLSetup.EnumVehicleStatus.Active & "|" & strUserId & "|" & strLocation & "|" & iif(chkunit.checked,"1","0")  & "|" & iif(chkplasma.checked,"1","0")

                    intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Add, _
                                                             strParamName, _
                                                             strParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
                End Try
            End If

            If blnIsInUse = False Then hidOriVehCode.Value = Trim(txtVehicleCode.Text)

        ElseIf strCmdArgs = "Del" Then
            Try
                strParamName = "ALKOHCODE|DESCRIPTION|ACCCODE|STATUS|UPDATEID|LOCCODE|ISUNIT|ISPLASMA"
                strParamValue = Trim(txtVehicleCode.Text) & "|" & _
                                Trim(txtDescription.Text) & "|" & _
                                strVRAAcc & "|" & objGLSetup.EnumVehicleStatus.Deleted & "|" & strUserId & "|" & strLocation & "|" & iif(chkunit.checked,"1","0")  & "|" & iif(chkplasma.checked,"1","0")

                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Add, _
                                                         strParamName, _
                                                         strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            Try
                strParamName = "ALKOHCODE|DESCRIPTION|ACCCODE|STATUS|UPDATEID|LOCCODE|ISUNIT|ISPLASMA"
                strParamValue = Trim(txtVehicleCode.Text) & "|" & _
                                Trim(txtDescription.Text) & "|" & _
                                strVRAAcc & "|" & objGLSetup.EnumVehicleStatus.Active & "|" & strUserId & "|" & strLocation & "|" & iif(chkunit.checked,"1","0")  & "|" & iif(chkplasma.checked,"1","0")

                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Add, _
                                                         strParamName, _
                                                         strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

        End If
        If strSelectedVehicleCode <> "" Then
            onLoad_Display()
            onLoad_BindButton()
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_AlkohList.aspx")
    End Sub


    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode As String = "GL_CLSSETUP_ALKOHACC_LINE_ADD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strAccCode As String
        Dim intErrNo As Integer

        strAccCode = ddlAccCode2.SelectedItem.Value

        If strAccCode = "" Then
            Exit Sub
        Else
            Try
                strParamName = "ACCCODE|ALKOHCODE|LOCCODE"
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
        Dim strOpCode As String = "GL_CLSSETUP_ALKOHACC_LINE_DEL"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim lblDelText As Label
        Dim strAccCode As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCode")
        strAccCode = lblDelText.Text

        Try
            strParamName = "ACCCODE|ALKOHCODE|LOCCODE"
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
        Dim strOpCode As String = "GL_CLSSETUP_ALKOHACC_LINE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim objRslSet As New DataSet()

        strParamName = "ALKOHCODE|LOCCODE"
        strParamValue = Trim(txtVehicleCode.Text) & "|" & strLocation

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

    Sub btnAddDR_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode As String = "GL_CLSSETUP_ALKOHACCDR_LINE_ADD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strAccCode As String
        Dim strBlkType As String
		Dim strIPType As String
        Dim intErrNo As Integer

        strAccCode = ddlAccCode3.SelectedItem.Value
        strBlkType = ddlBlkType.SelectedItem.Value
		strIPType = ddlIP.SelectedItem.Value

        If strAccCode = "" Or strBlkType = "" Then
            lblErrBlkType.Visible = True
            lblErrBlkType.Text = "Please select akun or tipe block"
            Exit Sub
        Else
            Try
                strParamName = "ACCCODE|BLKTYPE|ALKOHCODE|LOCCODE|IP"
                strParamValue = strAccCode & "|" & strBlkType & "|" & Trim(txtVehicleCode.Text) & "|" & strLocation & "|" & strIPType

                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                         strParamName, _
                                                         strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try
        End If

        onLoad_LineDisplayDR()
    End Sub

    Sub DEDR_DeleteDR(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCode As String = "GL_CLSSETUP_ALKOHACCDR_LINE_DEL"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim lblDelText As Label
        Dim strAccCode As String
        Dim strBlkType As String
        Dim intErrNo As Integer

        dgLineDRDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDRDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCode")
        strAccCode = lblDelText.Text
        lblDelText = dgLineDRDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblBlkType")
        strBlkType = lblDelText.Text

        Try
            strParamName = "ACCCODE|BLKTYPE|ALKOHCODE|LOCCODE"
            strParamValue = strAccCode & "|" & strBlkType & "|" & Trim(txtVehicleCode.Text) & "|" & strLocation

            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                        strParamName, _
                                                        strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        onLoad_LineDisplayDR()

    End Sub

    Sub onLoad_LineDisplayDR()
        Dim strOpCode As String = "GL_CLSSETUP_ALKOHACCDR_LINE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim objRslSet As New DataSet()

        strParamName = "ALKOHCODE|LOCCODE"
        strParamValue = Trim(txtVehicleCode.Text) & "|" & strLocation

        Try

            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objRslSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dgLineDRDet.DataSource = objRslSet.Tables(0)
        dgLineDRDet.DataBind()

    End Sub

	Sub Check_Clicked(sender As Object, e As EventArgs)        
        TRunit.visible = chkunit.Checked
    End Sub
	
	Sub CopyBtnKlik(ByVal Sender As Object, ByVal E As EventArgs)
		Dim strOppCd As String = "GL_CLSSETUP_ALKOHACCDR_UNIT_GEN_HIST"
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		
		if  txtyr2.Text.Trim() = "" 
			lblErrMessage.text = "Silakan isi periode sumber blok (copy dari periode)"
			lblErrMessage.visible = true
		end if
		
		if  txtyr.Text.Trim() = "" 
			lblErrMessage.text = "Silakan isi periode tujuan blok (search)"
			lblErrMessage.visible = true
		end if
		
		if ddlunit.selecteditem.text.trim() = "-"
			lblErrMessage.text = "Silakan isi unit lokasi"
			lblErrMessage.visible = true
		end if
		
		if txtAccCode3.Text.Trim() = "" 
			lblErrMessage.text = "Silakan isi COA lokasi"
			lblErrMessage.visible = true
		end if
		
		if txtarea.Text.Trim() = "" 
			lblErrMessage.text = "Silakan isi luasan HA lokasi"
			lblErrMessage.visible = true
		end if
		
		
		ParamNama = "YRCPY|YR|LOC|UNT|ACC|CODE|AREA"
        ParamValue = txtyr2.Text.Trim() & "|" & txtyr.Text.Trim() & "|" & strlocation & "|" & _
                     ddlunit.selecteditem.text.trim() & "|" & txtAccCode3.Text.Trim() & "|" & Trim(txtVehicleCode.Text) & "|" & iif(txtarea.Text.Trim()="","0",txtarea.Text.Trim())

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOppCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ALKOHACCDR_UNIT_GEN_HIST&errmesg=" & Exp.Message & "&redirect=")
        End Try
	
		dgLineUnit.EditItemIndex = -1
		onLoad_LineHistori()
	End Sub
	
	Sub onLoad_LineHistori()
        Dim strOpCd As String = "GL_CLSSETUP_ALKOHACCDR_UNIT_GET_HIST_LIST"
        Dim strParam As String
		Dim strValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim objResult As New Object()
    

			strParam = "CODE|LOC|YR|MN"
            strValue = Trim(txtVehicleCode.Text) & "|" & strlocation & "|" & txtyr.Text & "|" & strAccMonth
            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCd,strParam,strValue,objResult)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_GET_HIST&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
			
            dgLineUnit.DataSource = objResult.Tables(0)
            dgLineUnit.DataBind()
 End Sub

    Sub dgLineUnit_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim Updbutton As LinkButton
        Dim lblTemp As Label
        Dim ddlTemp As DropDownList

        dgLineUnit.EditItemIndex = CInt(E.Item.ItemIndex)
        onLoad_LineHistori()
        If CInt(E.Item.ItemIndex) >= dgLineUnit.Items.Count Then
            dgLineUnit.EditItemIndex = -1
            Exit Sub
        End If
    End Sub	
	
	Sub dgLineUnit_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgLineUnit.EditItemIndex = -1
        onLoad_LineHistori()
    End Sub
	
    Sub dgLineUnit_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd As String = "GL_CLSSETUP_ALKOHACCDR_UNIT_UPD_HIST"
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        Dim EditText As TextBox

        Dim strMN As String
        Dim strYR As String
        Dim strUNT As String
        Dim strACC As String
        Dim strTA As String
		
		
		EditText = E.Item.FindControl("id1")
		strUNT = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id2")
		strACC= EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id3")
		strTA = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id4")
		strMN = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id5")
		strYR = EditText.Text.Trim()
				
        ParamNama = "TA|ACC|UNT|CODE|MN|YR|LOC"
        ParamValue = strTA & "|" & strACC & "|" & strUNT & "|" & _
                     Trim(txtVehicleCode.Text) & "|" & strMN & "|" & _
					 strYR & "|" & strlocation 

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOppCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_UPD_HIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgLineUnit.EditItemIndex = -1
        onLoad_LineHistori()
    End Sub
	
	
End Class
