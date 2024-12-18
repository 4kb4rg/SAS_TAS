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
Public Class HR_trx_ResignDet_Estate : Inherits Page

#Region "Declare Var"


    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLine As DataGrid

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents idStat As HtmlInputHidden
    Protected WithEvents isNew As HtmlInputHidden

    Protected WithEvents LblidM As Label
    Protected WithEvents lbldivisilama As Label
    Protected WithEvents lbljabatanlama As Label
    Protected WithEvents lblempcode As Label
    Protected WithEvents lbldivisibaru As Label
    Protected WithEvents lbljabatanbaru As Label
    Protected WithEvents lblefektifdate As Label

    Protected WithEvents ddldivisicode As DropDownList
    Protected WithEvents ddlempcode As DropDownList
    Protected WithEvents ddlberhenti As DropDownList
  
    Protected WithEvents txtket As TextBox
    Protected WithEvents txtefektifdate As TextBox
    Protected WithEvents btnSelDate As Image
    Protected WithEvents DelBtn As ImageButton
	Protected WithEvents UnDelBtn As ImageButton
	
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents BackBtn As ImageButton

    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

	Protected WithEvents ddlpt As DropDownList
	Protected WithEvents ddllokasi As DropDownList
	
    Dim objPRTrx As New agri.PR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim ObjOk As New agri.GL.ClsTrx()
    Dim strLocType As String

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strCompany As String

    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFmt As String
    Dim intPRAR As Long
    Dim intConfig As Integer

    Dim strSelectedPayId As String = ""
    Dim strStatus As String = "1" 
    Dim TarifLembur As Single

    Dim strID As String = ""
    Dim strEmpCode As String = ""
    Dim strEmpDivCode As String = ""
    Dim strAcceptFormat As String

#End Region

#Region "Page Load"
     Sub ShowHidePT(ByVal tf As Boolean)
        Dim found As Control = Me.FindControl("divpt")
        If Not found Is Nothing Then
            Dim cast As HtmlGenericControl = CType(found, HtmlGenericControl)
            If Not cast Is Nothing Then
                cast.Visible = tf
            End If
        End If


    End Sub
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_HRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            strID = Trim(IIf(Request.QueryString("ResignCode") <> "", Request.QueryString("ResignCode"), Request.Form("ResignCode")))

            lblErrMessage.Visible = False

            If Not IsPostBack Then
                txtefektifdate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                If strID <> "" Then
                    onLoad_Display()
                Else
                    isNew.Value = "True"
                    LblidM.Text = getCode()
                    BindDivision("")
                    BindBerhenti("")
                End If
                onLoad_button()
            End If
        End If
    End Sub

#End Region

#Region "Function & Procedure"
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

    Function getCode() As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String
        Dim dt As String = objGlobal.GetShortDate(strDateFmt, Now())

        tcode = "RSN/" & strLocation & "/" & Mid(Trim(dt), 4, 2) & Right(Trim(dt), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|4|" & tcode & "|3"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Sub clearAll()
        LblidM.Text = ""
    End Sub

    Sub onLoad_button()
        If strID <> "" Then
            lbldivisilama.Visible = True
            lblempcode.Visible = True
            ddldivisicode.Visible = False
            ddlempcode.Visible = False
        Else
            lbldivisilama.Visible = False
            lblempcode.Visible = False
            ddldivisicode.Visible = True
            ddlempcode.Visible = True
        End If
    End Sub

#End Region

#Region "Binding"
    Sub BindDivision(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim ObjDiv As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.BlkGrpCode Like '%" & strDivCode & "%' AND A.LocCode = '" & strLocation & "' AND A.Status='1' |ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, ObjDiv)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_DIVISI_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If ObjDiv.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To ObjDiv.Tables(0).Rows.Count - 1
                ObjDiv.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(ObjDiv.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                ObjDiv.Tables(0).Rows(intCnt).Item("Description") = Trim(ObjDiv.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(ObjDiv.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = ObjDiv.Tables(0).NewRow()
        dr("BlkGrpCode") = " "
        dr("Description") = "Please Select Division"
        ObjDiv.Tables(0).Rows.InsertAt(dr, 0)

        ddldivisicode.DataSource = ObjDiv.Tables(0)
        ddldivisicode.DataTextField = "Description"
        ddldivisicode.DataValueField = "BlkGrpCode"
        ddldivisicode.DataBind()
        ddldivisicode.SelectedIndex = 0

    End Sub

    Sub BindEmployee(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
		Dim strAm as String

		If Cint(strAccMonth) < 10 Then
            strAM = "0" & rtrim(strAccMonth)
        Else
            strAM = rtrim(strAccMonth)
        End If



        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & strAM & "|" & strAccyear &  "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%'|ORDER BY A.EmpName"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description")) & " - " & Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("Job"))
                If objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(strEmpCode) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "Please Select Employee Code"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlempcode.DataSource = objEmpCodeDs.Tables(0)
        ddlempcode.DataTextField = "_Description"
        ddlempcode.DataValueField = "EmpCode"
        ddlempcode.DataBind()
        ddlempcode.SelectedIndex = intSelectedIndex

    End Sub

    Sub BindBerhenti(ByVal pv_berhenti As String)
        ddlberhenti.Items.Clear()
        ddlberhenti.Items.Add(New ListItem("Pilih Kategori", ""))
        ddlberhenti.Items.Add(New ListItem("Pindah PT", "1"))
        ddlberhenti.Items.Add(New ListItem("Kecelakaan", "2"))
        ddlberhenti.Items.Add(New ListItem("Meninggal Dunia", "3"))
        ddlberhenti.Items.Add(New ListItem("Lain-lain", "4"))

        If pv_berhenti = "" Then
            ddlberhenti.SelectedIndex = 0
        Else
            ddlberhenti.SelectedIndex = CInt(pv_berhenti)
        End If
    End Sub

	Sub BindPT(ByVal strCode As String)
        Dim strOpCd_EmpDiv As String = "ADMIN_CLSCOMP_COMPANY_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim ObjDiv As New Object()
		Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow


        strParamName = "STRSEARCH"
        strParamValue = ""

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, ObjDiv)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADMIN_CLSCOMP_COMPANY_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If ObjDiv.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To ObjDiv.Tables(0).Rows.Count - 1
                ObjDiv.Tables(0).Rows(intCnt).Item("CompCode") = Trim(ObjDiv.Tables(0).Rows(intCnt).Item("CompCode"))
                ObjDiv.Tables(0).Rows(intCnt).Item("_Description") = Trim(ObjDiv.Tables(0).Rows(intCnt).Item("_Description"))
				If ObjDiv.Tables(0).Rows(intCnt).Item("CompCode") = Trim(strCode) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = ObjDiv.Tables(0).NewRow()
        dr("CompCode") = ""
        dr("_Description") = "Pilih PT"
        ObjDiv.Tables(0).Rows.InsertAt(dr, 0)

        ddlpt.DataSource = ObjDiv.Tables(0)
        ddlpt.DataTextField = "_Description"
        ddlpt.DataValueField = "CompCode"
        ddlpt.DataBind()
        ddlpt.SelectedIndex = intSelectedIndex
	End Sub

	Sub BindLokasi(Byval strcomp as string,ByVal strCode As String)
        Dim strOpCd_EmpDiv As String = "ADMIN_CLSCOMP_COMPANY_LOCATION_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim ObjDiv As New Object()
		Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow


        strParamName = "COMPCODE"
        strParamValue = trim(strcomp)

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, ObjDiv)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADMIN_CLSCOMP_COMPANY_LOCATION_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If ObjDiv.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To ObjDiv.Tables(0).Rows.Count - 1
                ObjDiv.Tables(0).Rows(intCnt).Item("LocCode") = Trim(ObjDiv.Tables(0).Rows(intCnt).Item("LocCode"))
                ObjDiv.Tables(0).Rows(intCnt).Item("Description") = Trim(ObjDiv.Tables(0).Rows(intCnt).Item("LocCode")) & " (" & Trim(ObjDiv.Tables(0).Rows(intCnt).Item("Description")) & ")"
				If ObjDiv.Tables(0).Rows(intCnt).Item("LocCode") = Trim(strCode) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = ObjDiv.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "Pilih Lokasi"
        ObjDiv.Tables(0).Rows.InsertAt(dr, 0)

        ddllokasi.DataSource = ObjDiv.Tables(0)
        ddllokasi.DataTextField = "Description"
        ddllokasi.DataValueField = "LocCode"
        ddllokasi.DataBind()
        ddllokasi.SelectedIndex = intSelectedIndex
	End Sub
	
#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPRESIGN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strlocation & "| AND ResignCode Like '%" & strID & "%' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMUTASI_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            isNew.Value = "False"
            LblidM.Text = strID
            strEmpCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpCode"))
            lbldivisilama.Text = objEmpCodeDs.Tables(0).Rows(0).Item("IdDiv") & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Divisi")) & ")"
            lblempcode.Text = strEmpCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpName")) & ")"
            txtefektifdate.Text = Date_Validation(objEmpCodeDs.Tables(0).Rows(0).Item("EfektifDate"), True)

            BindBerhenti(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("ResignType")))
            txtket.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Notes"))
            lblStatus.Text = IIf(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Status")) = "1", "Aktive", "Delete")
            lblDateCreated.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("uName"))

            idStat.Value = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Status"))
			
			if idStat.Value="1" then
			DelBtn.visible = True
			else
			UnDelBtn.visible = True
			end if


        End If
    End Sub


    Sub ddldivisicode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpDivCode = ddldivisicode.SelectedItem.Value.Trim()
        BindEmployee(strEmpDivCode)
    End Sub


	Sub ddlberhenti_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		if ddlberhenti.SelectedItem.Value.Trim()="1" Then
		ShowHidePT(True)
		BindPT("")
		else
		ShowHidePT(False)
		end if
	End Sub
	
	 Sub ddlpt_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindLokasi(ddlpt.SelectedItem.Value.Trim(),"")
    End Sub
	
	
    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "HR_HR_TRX_EMPRESIGN_UPD"
        Dim Status As String
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String

        Dim strEmpCode As String
        Dim strberhenti As String
        Dim strdtefektif As String
		Dim strNComp as String
		Dim strNLoc as String
		

        If isNew.Value = "True" Then
            If ddlempcode.Text = "" Then
                lblErrMessage.Text = "Silakan pilih employee code !!"
                lblErrMessage.Visible = True
                Exit Sub
            End If
        End If
        
        If ddlberhenti.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Text = "Silakan pilih kategori berhenti !!"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If txtket.Text.Trim = "" Then
            lblErrMessage.Text = "Silakan input keterangan berhenti !!"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        strdtefektif = Date_Validation(txtefektifdate.Text.Trim(), False)
        If strdtefektif = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan input tanggal efektif !"
            Exit Sub
        End If
		
		if ddlpt.text <> "" then
			strNComp = ddlpt.SelectedItem.Value.Trim() 
		else
		    strNComp = ""
		end if

        
		if ddllokasi.text <> "" then
			strNLoc = ddllokasi.SelectedItem.Value.Trim() 
		else
		    strNLoc = ""
		end if

		
		If isNew.Value = "True" Then
            strEmpCode = ddlempcode.SelectedItem.Value.Trim()
        Else
            strEmpCode = Left(lblempcode.Text.Trim, InStr(lblempcode.Text.Trim, "(") - 1)
        End If

        strberhenti = ddlberhenti.SelectedItem.Value.Trim()

        ParamNama = "RC|RD|RT|CE|ED|KET|LOC|NCOMP|NLOC|CD|UD|UI"
        ParamValue = LblidM.Text.Trim() & "|" & _
                     Date_Validation(Format(Now(), "dd/MM/yyyy"), False) & "|" & _
                     strberhenti & "|" & _
                     strEmpCode & "|" & _
                     strdtefektif & "|" & _
                     txtket.Text.Trim() & "|" & _
                     strLocation & "|" & _
					 strNComp & "|" & _
					 strNLoc & "|" & _
                     Now() & "|" & _
                     Now() & "|" & _
                     strUserId

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try

        BackBtn_Click(Sender, E)
    End Sub

    Sub DelBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "HR_HR_TRX_EMPRESIGN_DEL"
        Dim Status As String
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String
		
		if idStat.Value = "1" then
		Status = "2" 
		else
		status = "1"
		end if
		
		
		ParamNama = "ST|UD|UI|MC"
        ParamValue = Status & "|" & _
		             Now() & "|" & _
		             strUserId  & "|" & _
		             LblidM.Text.Trim() 
                  
        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try

		BackBtn_Click(Sender, E)
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_ResignList_Estate.aspx")
    End Sub


#End Region

End Class
