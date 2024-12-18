
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


Public Class PR_Setup_VehicleDet_Estate : Inherits Page

    Protected WithEvents txtVehicleCode As TextBox
	Protected WithEvents ddlVehicleCode As DropDownList
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlVehType As DropDownList
    Protected WithEvents txtModel As TextBox
    Protected WithEvents txtHPCC As TextBox

    Protected WithEvents txtthn As TextBox
    Protected WithEvents txtPol As TextBox

    Protected WithEvents ddlUOM As DropDownList
    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents ddlAccCode_premi As DropDownList
    Protected WithEvents ddlAccCode_astek As DropDownList
    Protected WithEvents ddlAccCode_jht As DropDownList

    Protected WithEvents lblStatus As Label
    Protected WithEvents txtPurchaseDate As TextBox
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
    Protected WithEvents lblErrVehType As Label
    Protected WithEvents lblErrUOM As Label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label

    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validatePol As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator
    Protected hidRecStatus As HtmlInputHidden
    Protected hidOriVehCode As HtmlInputHidden

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsUOM()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim ObjOK As New agri.GL.ClsTrx

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
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intGLAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrVehType.Visible = False
            lblErrDup.Visible = False
            lblErrUOM.Visible = False
            lblErrAccCode.Visible = False
            lblFmt.Visible = False
            lblDate.Visible = False
            strSelectedVehicleCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedVehicleCode <> "" Then
                    tbcode.Value = strSelectedVehicleCode
                    onLoad_Display()
                Else
				    BindVehicle() 
                    BindVehType("")
                    BindUOM("")
                    BindAccCode("", "", "", "")
                End If

                If Request.QueryString("tbcode") = "" Then 
				    intStatus=9
                    hidRecStatus.Value = "Unsaved"
                Else
                    hidRecStatus.Value = "Saved"
                    hidOriVehCode.Value = Trim(Request.QueryString("tbcode"))
                End If
				onLoad_BindButton()
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_PR_STP_KENDARAAN_GET_BY_VEHCODE"
        Dim strParamName As String 
        Dim strParamValue As String 
	    Dim intErrNo As Integer

		strParamName = "VEHCODE|LOCCODE|ST"
		strParamValue =   strSelectedVehicleCode & "|" & _
						  strLocation & "|1" 
						  
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objVehDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_GET_BY_VEHCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

	 If objVehDs.Tables(0).Rows.Count > 0 Then
        txtVehicleCode.Text = Trim(objVehDs.Tables(0).Rows(0).Item("VehCode"))
        txtDescription.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Description"))
        txtModel.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Model"))
        txtHPCC.Text = Trim(objVehDs.Tables(0).Rows(0).Item("HPCC"))
        txtPol.Text = Trim(objVehDs.Tables(0).Rows(0).Item("NoPolisi"))
        txtthn.Text = Trim(objVehDs.Tables(0).Rows(0).Item("BuildYear"))
        intStatus = CInt(Trim(objVehDs.Tables(0).Rows(0).Item("Status")))

        lblHiddenSts.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objGLSetup.mtdGetActSatus(Trim(objVehDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objVehDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objVehDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objVehDs.Tables(0).Rows(0).Item("UserName"))

        BindVehType(Trim(objVehDs.Tables(0).Rows(0).Item("VehTypeCode")))
        BindUOM(Trim(objVehDs.Tables(0).Rows(0).Item("UOMCode")))
        BindAccCode(Trim(objVehDs.Tables(0).Rows(0).Item("COAUpah")), Trim(objVehDs.Tables(0).Rows(0).Item("COApremi")), Trim(objVehDs.Tables(0).Rows(0).Item("COAastek")), Trim(objVehDs.Tables(0).Rows(0).Item("COAjht")))

        txtPurchaseDate.Text = objGlobal.GetShortDate(strDateFMt, Trim(objVehDs.Tables(0).Rows(0).Item("PurchaseDate")))
	  else
		lblErrDup.text = strSelectedVehicleCode & " sudah didelete" 
		lblErrDup.Visible = true
		exit sub
	  end if
    End Sub

    Sub onLoad_BindButton()

        txtVehicleCode.visible = False
		ddlVehicleCode.visible = False
		
        txtDescription.Enabled = False
        ddlVehType.Enabled = False
        txtModel.Enabled = False
        txtHPCC.Enabled = False
        ddlUOM.Enabled = False
        ddlAccCode.Enabled = False
        txtPurchaseDate.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

		Select Case intStatus
            Case objGLSetup.EnumVehicleStatus.Active
			    txtVehicleCode.visible = True
				ddlVehicleCode.visible = False
                txtDescription.Enabled = True
                ddlVehType.Enabled = True
                txtModel.Enabled = True
                txtHPCC.Enabled = True
                ddlUOM.Enabled = True
                ddlAccCode.Enabled = True
                txtPurchaseDate.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objGLSetup.EnumVehicleStatus.Deleted 
			    txtVehicleCode.visible = True
				ddlVehicleCode.visible = False
                UnDelBtn.Visible = True
            Case Else
			    txtVehicleCode.visible = False
                ddlVehicleCode.visible = True
                txtDescription.Enabled = True
                ddlVehType.Enabled = True
                txtModel.Enabled = True
                txtHPCC.Enabled = True
                ddlUOM.Enabled = True
                ddlAccCode.Enabled = True
                txtPurchaseDate.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub

    Sub BindVehicle()
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"
            strParam = "|LocCode = '" & Session("SS_LOCATION") & "' AND Status = '1'"
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   objVehDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAY_VEH_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objVehDs.Tables(0).Rows.Count - 1
            objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode"))
            objVehDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("_Description")) 
        Next

        dr = objVehDs.Tables(0).NewRow()
        dr("VehCode") = ""
        dr("_Description") = "Pilih Kendaraan Code "
        objVehDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehicleCode.DataSource = objVehDs.Tables(0)
        ddlVehicleCode.DataValueField = "VehCode"
        ddlVehicleCode.DataTextField = "_Description"
        ddlVehicleCode.DataBind()
        ddlVehicleCode.SelectedIndex = intSelectedIndex
    End Sub
	
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
        dr("Description") = "Select Type"
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

    Sub BindAccCode(ByVal pv_strAccCode As String, ByVal pv_p As String, ByVal pv_a As String, ByVal pv_j As String)
        Dim strOpCode As String = "GL_CLSSETUP_VEHICLE_ACCCODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim intSelectIndex_p As Integer = 0
        Dim intSelectIndex_a As Integer = 0
        Dim intSelectIndex_j As Integer = 0

        Dim strSort As String
        Dim strSearch As String

        strSort = "order by AccCode"
        strSearch = "Where Status = '" & objGLSetup.EnumAccStatus.Active & "' " & _
                    IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ") & _
                    "AND AccCode Like '4%'    "

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = objAccDs.Tables(0).Rows(intCnt).Item("AccCode") & " (" & Trim(objAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectIndex = intCnt + 1
            End If

            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_p) Then
                intSelectIndex_p = intCnt + 1
            End If

            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_a) Then
                intSelectIndex_a = intCnt + 1
            End If

            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_j) Then
                intSelectIndex_j = intCnt + 1
            End If

        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Select COA"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccCode.DataSource = objAccDs.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectIndex

        ddlAccCode_premi.DataSource = objAccDs.Tables(0)
        ddlAccCode_premi.DataValueField = "AccCode"
        ddlAccCode_premi.DataTextField = "Description"
        ddlAccCode_premi.DataBind()
        ddlAccCode_premi.SelectedIndex = intSelectIndex_p

        ddlAccCode_astek.DataSource = objAccDs.Tables(0)
        ddlAccCode_astek.DataValueField = "AccCode"
        ddlAccCode_astek.DataTextField = "Description"
        ddlAccCode_astek.DataBind()
        ddlAccCode_astek.SelectedIndex = intSelectIndex_a

        ddlAccCode_jht.DataSource = objAccDs.Tables(0)
        ddlAccCode_jht.DataValueField = "AccCode"
        ddlAccCode_jht.DataTextField = "Description"
        ddlAccCode_jht.DataBind()
        ddlAccCode_jht.SelectedIndex = intSelectIndex_j
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
        Dim strOpCd_Upd As String = "PR_PR_STP_KENDARAAN_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim strVRAAcc As String = Request.Form("ddlAccCode")
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim ParamNama As String = ""
        Dim ParamValue As String = ""

		Dim vercode as string 
        Dim strPurchaseDate As String
        Dim blnIsInUse As Boolean = False
        Dim strstatus As String


        If strVRAAcc = "" Then strVRAAcc = ddlAccCode.SelectedItem.Value
        
	
        If ddlVehType.SelectedItem.Value = "" Then
		    lblErrVehType.Text = "Silakan pilih tipe kendaraan"
            lblErrVehType.Visible = True
            Exit Sub
        ElseIf ddlUOM.SelectedItem.Value = "" Then
		    lblErrUOM.Text = "Silakan pilih UOM kendaraan"
            lblErrUOM.Visible = True
            Exit Sub
        ElseIf strVRAAcc = "" Then
            lblErrAccCode.Text = "Silakan COA kendaraan"		
            lblErrAccCode.Visible = True
            Exit Sub
        End If

        'strPurchaseDate = CheckDate()

        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If

        'if hidRecStatus.Value = "Unsaved" then 
		'   vercode = ddlVehicleCode.SelectedItem.Value.Trim() 
		'Else
        '   vercode = txtVehicleCode.Text.Trim()   
		'end if 
		
		if ddlVehicleCode.visible then
			vercode = ddlVehicleCode.SelectedItem.Value.Trim() 
		else
			vercode = txtVehicleCode.Text.Trim()  
		end	if
		
		if txtPol.text.trim() = "" then 
		txtPol.text = vercode  
		end if 
		
		if vercode = "" Then 
		lblErrDup.Text = "Silakan input No polisi"
		lblErrDup.Visible = True
		exit sub
		end if
		
		ParamNama = "VEHCODE|POL|BY|ACCCODE|ACCCODE_P|ACCCODE_A|ACCCODE_J|LOC"
        ParamValue = vercode & "|" & _
                     txtPol.Text.Trim.ToUpper & "|" & _
                     txtthn.Text.Trim & "|" & _
                     ddlAccCode.SelectedItem.Value.Trim & "|" & _
                     ddlAccCode_premi.SelectedItem.Value.Trim & "|" & _
                     ddlAccCode_astek.SelectedItem.Value.Trim & "|" & _
                     ddlAccCode_jht.SelectedItem.Value.Trim  & "|" & _
					 strlocation


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_KENDARAAN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strSelectedVehicleCode = vercode
        If strSelectedVehicleCode <> "" Then
            onLoad_Display()
            onLoad_BindButton()
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_Setup_Vehicle_Estate.aspx")
    End Sub

	Sub ddlVehicleCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strOpCd As String = "GL_CLSSETUP_VEHICLE_GET_BY_VEHCODE"
        Dim strParam As String = ddlVehicleCode.SelectedItem.Value.Trim() 
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

        txtDescription.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Description"))
        txtModel.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Model"))
        txtHPCC.Text = Trim(objVehDs.Tables(0).Rows(0).Item("HPCC"))
       
        BindVehType(Trim(objVehDs.Tables(0).Rows(0).Item("VehTypeCode")))
        BindUOM(Trim(objVehDs.Tables(0).Rows(0).Item("UOMCode"))) 
		BindAccCode(Trim(objVehDs.Tables(0).Rows(0).Item("AccCode")), "", "", "") 
		
		txtPurchaseDate.Text = objGlobal.GetShortDate(strDateFMt, Trim(objVehDs.Tables(0).Rows(0).Item("PurchaseDate")))
		
    End Sub
	
    Sub ddlAccCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
      '  txtVehicleCode.Text = Left(ddlAccCode.SelectedItem.Value.Trim, 6) & Right(ddlAccCode.SelectedItem.Value.Trim, 1)
    End Sub

End Class
