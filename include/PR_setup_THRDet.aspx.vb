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
Imports agri.GL


Public Class PR_setup_THRDet : Inherits Page


    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents AstekCd As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupDept As Label
    Protected WithEvents lblNoBrsRt As Label
    Protected WithEvents lblNoCompCode As Label
    Protected WithEvents lblNoLocCode As Label
   
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblCompany As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblDeptHead As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblList As Label
    Protected WithEvents lblCodeDiv As Label
    Protected WithEvents lblDivHead As Label
    
	Protected WithEvents lblAstekCd As Label
    Protected WithEvents txtAstekCd As TextBox
    Protected WithEvents txtPStart As TextBox
    Protected WithEvents txtPEnd As TextBox
	Protected WithEvents ddlMonth As DropDownList
	Protected WithEvents ddlyear As DropDownList
	Protected WithEvents ddlreligion As DropDownList
	Protected WithEvents txtdaging As TextBox
	
    
	
	Protected WithEvents chkjpk As CheckBox 
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents revCode As RegularExpressionValidator


    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
	Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim ObjOK As New agri.GL.ClsTrx

    Dim objJobCode As New Object()
    Dim objDeptDs As New Object()
    Dim objDeptCodeDs As New Object()
    Dim objCompDs As New Object()
    Dim objDivHeadDs As New Object()
    Dim objLocDs As New Object()
    Dim objEmpDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long

    Dim strSelectedAstekCd As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
	Dim strAcceptFormat As String

	 Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrNo As Integer

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", "THR" & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function
	
	Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object()
        Dim objActualDate As New Object()
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=pu/trx/pu_GRList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblAstekCd.Visible = False
          
            strSelectedAstekCd = Trim(IIf(Request.QueryString("THRid") <> "", Request.QueryString("THRid"), Request.Form("THRid")))
            intStatus = CInt(lblHiddenSts.Text)
			BindAccYear(Year(Now()))
			
            If Not IsPostBack Then
                If strSelectedAstekCd <> "" Then
                    onLoad_Display()
                Else
                    ddlMonth.SelectedIndex = Month(Now()) - 1
					BindReligion("")
				End If
            End If
			onLoad_BindButton()
        End If

    End Sub
	
	Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        ddlyear.DataSource = objAccYearDs.Tables(0)
        ddlyear.DataValueField = "AccYear"
        ddlyear.DataTextField = "UserName"
        ddlyear.DataBind()
        ddlyear.SelectedIndex = intSelectedIndex - 1
    End Sub
	
	Sub BindReligion(ByVal str As String)
        Dim strOpCd_Religion As String = "HR_CLSSETUP_RELIGION_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objDs As New Object()
        Dim intReligionIndex As Integer = 0
        Dim dr As DataRow


        ParamName = "SEARCHSTR|SORTEXP"
        ParamValue = "AND REL.Status='1'|ORDER By ReligionCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Religion, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSSETUP_RELIGION_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

		For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
            objDs.Tables(0).Rows(intCnt).Item("ReligionCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("ReligionCode"))
            objDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDs.Tables(0).Rows(intCnt).Item("Description"))
        Next intCnt

        

        dr = objDs.Tables(0).NewRow()
        dr("ReligionCode") = ""
        dr("Description") = "All"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlReligion.DataSource = objDs.Tables(0)
        ddlReligion.DataTextField = "Description"
        ddlReligion.DataValueField = "ReligionCode"
        ddlReligion.DataBind()
        ddlReligion.selectedValue = str
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_THR_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String


        strSearch = "AND A.LocCode='" & strLocation &"' AND A.THRid = '" & Trim(strSelectedAstekCd) & "'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_ASTEK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblHiddenSts.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Status"))
            txtAstekCd.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("THRid"))
            txtPStart.Text = Date_Validation(Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeMulai")),True)
            txtPEnd.Text = Date_Validation(Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeSelesai")),True)
            txtdaging.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("RpTambahan"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName")) 
			
			BindReligion(Trim(objDeptDs.Tables(0).Rows(0).Item("Religion")) ) 
			ddlMonth.SelectedValue = Right(Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeBayar")),2)
			ddlyear.selectedValue = Left(Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeBayar")),4)
			
         End If

    End Sub

    Sub onLoad_BindButton()

        txtAstekCd.Enabled = False
        SaveBtn.Visible = False
        UnDelBtn.Visible = False
        DelBtn.Visible = False

        Select Case Trim(intStatus)
            Case "1"
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                UnDelBtn.Visible = True
            Case Else
                SaveBtn.Visible = True
        End Select

    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_THR_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim Adate As String
        Dim strstatus As String = "" 
		Dim strreligion As String = ddlReligion.selectedItem.Value.Trim()
		Dim objID As New Object()

	     If txtAstekCd.Text = "" Then
            txtAstekCd.Text = getCode()
        End If
        
        If txtPStart.Text = "" Then
            lblErrMessage.Text = "Please Insert Periode start"
            lblErrMessage.Visible = True
            Exit Sub
        End If
		
		If txtPEnd.Text = "" Then
            lblErrMessage.Text = "Please Insert Periode End"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then

            strstatus = "1"

        ElseIf strCmdArgs = "Del" Then

            strstatus = "2"

        End If

        strSelectedAstekCd = Trim(txtAstekCd.Text)

        ParamNama = "AC|LOC|AY|PB|PS|PE|RG|RP|ST|CD|UD|UI"
        ParamValue = Trim(txtAstekCd.Text) & "|" & _ 
		             strLocation & "|" & _ 
					 ddlyear.selectedItem.Value.Trim() & "|" & _ 
					 ddlyear.selectedItem.Value.Trim() & ddlMonth.selectedItem.Value.Trim() & "|" & _ 
					 Date_Validation(Trim(txtPStart.Text), False)  & "|" & _ 
					 Date_Validation(Trim(txtPEnd.Text), False) & "|" & _ 
					 ddlReligion.selectedItem.Value.Trim()  & "|" & _ 
					 iif(txtdaging.Text.Trim()="","0",txtdaging.Text.Trim())  & "|" & _
                     strstatus & "|" & DateTime.Now & "|" & DateTime.Now & "|" & strUserId 
					  
					 
        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_UPD&errmesg=" & lblErrMessage.Text )
        End Try

       If strSelectedAstekCd <> "" Then
           onLoad_Display()
        End If

    End Sub
	


    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_THRList.aspx")
    End Sub

End Class
