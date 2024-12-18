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
Public Class HR_trx_MutasiPenghasilanDet_Estate : Inherits Page

#Region "Declare Var"


    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents idMDR As HtmlInputHidden
    Protected WithEvents isNew As HtmlInputHidden
    Protected WithEvents idWH As HtmlInputHidden
    Protected WithEvents LblidM As Label
    Protected WithEvents lbldivisilama As Label
    Protected WithEvents lbljabatanlama As Label
    Protected WithEvents lblempcode As Label
    Protected WithEvents lbldivisibaru As Label
    Protected WithEvents lbljabatanbaru As Label
    Protected WithEvents lblefektifdate As Label

    Protected WithEvents ddldivisicode As DropDownList
    Protected WithEvents ddlempcode As DropDownList
    Protected WithEvents ddldivisibaru As DropDownList
    Protected WithEvents ddljabatanbaru As DropDownList
    Protected WithEvents ddlAccMonth As DropDownList
    Protected WithEvents ddlAccYear As DropDownList
    Protected WithEvents ddlType As DropDownList

    Protected WithEvents txtefektifdate As TextBox
    Protected WithEvents txtdoc As TextBox
    Protected WithEvents txtket As TextBox
    Protected WithEvents txtgajinetto As TextBox

    Protected WithEvents btnSelDate As Image
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents BackBtn As ImageButton

    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

	Protected WithEvents txtgajipokok As TextBox
	Protected WithEvents txtpremi  As TextBox
	Protected WithEvents txtpremitetap As TextBox
	Protected WithEvents txtpremilain As TextBox
	Protected WithEvents txtlembur As TextBox
	Protected WithEvents txttlain As TextBox
	Protected WithEvents txtastek As TextBox
	Protected WithEvents txtcatuberas As TextBox
	Protected WithEvents txtRapel As TextBox
	Protected WithEvents txtthrbonus As TextBox
	Protected WithEvents txtpotjbt As TextBox 
	Protected WithEvents txtpotjht As TextBox
	Protected WithEvents txtpotlain As TextBox
	Protected WithEvents txtpph21 As TextBox
			
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
    Dim strLocationName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFmt As String
    Dim intPRAR As Long
    Dim intConfig As Integer

    Dim strSelectedPayId As String = ""
    Dim intStatus As Integer
    Dim TarifLembur As Single

    Dim strID As String = ""
    Dim strEmpCode As String = ""
    Dim strEmpDivCode As String = ""
    Dim strAcceptFormat As String

#End Region

#Region "Page Load"
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_HRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")

        'set tahun periode SPT
        Dim yesterday As Integer = DateTime.Today.Year
        'ddlAccYear.Items.Clear()
        For value As Integer = (yesterday - 5) To (yesterday + 5)
            'ddlAccYear
            Dim newListItem As ListItem
            newListItem = New ListItem(value, value)
            ddlAccYear.Items.Add(newListItem)
        Next
        'end of set tahun periode SPT


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            strID = Trim(IIf(Request.QueryString("MutasiPenghasilanCode") <> "", Request.QueryString("MutasiPenghasilanCode"), Request.Form("MutasiPenghasilanCode")))

            lblErrMessage.Visible = False
            If Not IsPostBack Then
                ddlAccMonth.SelectedIndex = CInt(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))

                txtefektifdate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                If strID <> "" Then
                    isNew.Value = "False"
                    onLoad_Display()
                Else
                    isNew.Value = "True"
                    LblidM.Text = getCode()
                    BindDivision("")
                    BindJabatan("")
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

        tcode = "MTP/" & strLocation & "/" & Mid(Trim(dt), 4, 2) & Right(Trim(dt), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|55|" & tcode & "|3"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    
    Function getJabatanLama(ByVal ec As String) As String
        Dim strOpCd As String = "HR_HR_TRX_EMPWORK_GET_BY_EMPCODE"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer

        ParamName = "SEARCH|SORT"
        ParamValue = "AND x.codeemp='" & ec & "'|ORDER By JbtCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_JABATAN_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        getJabatanLama = Trim(objDs.Tables(0).Rows(0).Item("jbt")) & " (" & Trim(objDs.Tables(0).Rows(0).Item("Codejbt")) & ")"

    End Function

    Sub clearAll()
        LblidM.Text = ""
    End Sub

    Sub onLoad_button()
        If strID <> "" Then
            lbldivisilama.Visible = True
            lbldivisibaru.Visible = True
            lbljabatanbaru.Visible = True
            lblempcode.Visible = True
            ddldivisicode.Visible = False
            ddlempcode.Visible = False
            ddldivisibaru.Visible = False
            ddljabatanbaru.Visible = False
        Else
            lbldivisilama.Visible = False
            lbldivisibaru.Visible = False
            lbljabatanbaru.Visible = False
            lblempcode.Visible = False
            ddldivisicode.Visible = True
            ddlempcode.Visible = True
            ddldivisibaru.Visible = True
            ddljabatanbaru.Visible = True
        End If
    End Sub

    Sub Save_Work(ByVal id As String)
        Dim strOpCd As String = "HR_HR_TRX_EMPWORK_UPD"
        Dim strYM As String = Mid(txtefektifdate.Text.Trim, 4, 2) + Right(txtefektifdate.Text.Trim, 4)

        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String


        ParamNama = "ID|CE|CN|PS|PE|JB|LOC|CD|UD|UI|CDV|CJB"
        ParamValue = id.Trim() & "|" & _
                     ddlempcode.SelectedItem.Value.Trim() & "|" & _
                     strLocationName & "|" & _
                     strYM & "|" & _
                     "000000" & "|" & _
                     ddldivisibaru.SelectedItem.Text.Trim() & "-" & ddljabatanbaru.SelectedItem.Text.Trim() & "|" & _
                     strLocation & "|" & _
                     Now() & "|" & _
                     Now() & "|" & _
                     strUserId & "|" & _
      ddldivisibaru.SelectedItem.Value.Trim() & "|" & _
      ddljabatanbaru.SelectedItem.Value.Trim()


        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try



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
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, ObjDiv)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
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

        ddldivisibaru.DataSource = ObjDiv.Tables(0)
        ddldivisibaru.DataTextField = "Description"
        ddldivisibaru.DataValueField = "BlkGrpCode"
        ddldivisibaru.DataBind()
        ddldivisibaru.SelectedIndex = 0

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


        strParamName = "LOC|SEARCH|SORT|AM|AY"
        strParamValue = strLocation & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%'|ORDER BY A.EmpName|" & _
		                ddlAccMonth.SelectedItem.Value.Trim() & "|" & _
						ddlAccYear.SelectedItem.Value.Trim()

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

    Sub BindJabatan(ByVal str As String)
        Dim strOpCd As String = "HR_HR_STP_JABATAN_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0

        Dim dr As DataRow

        ParamName = "SEARCH|SORT"
        ParamValue = "WHERE Status='1'|ORDER By JbtCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_JABATAN_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("JbtCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("JbtCode"))
                objDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDs.Tables(0).Rows(intCnt).Item("Description"))


                If objDs.Tables(0).Rows(intCnt).Item("JbtCode") = str Then
                    intselectIndex = intCnt + 1
                End If

            Next
        End If

        dr = objDs.Tables(0).NewRow()
        dr("JbtCode") = ""
        dr("Description") = "Select Jabatan"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddljabatanbaru.DataSource = objDs.Tables(0)
        ddljabatanbaru.DataTextField = "Description"
        ddljabatanbaru.DataValueField = "JbtCode"
        ddljabatanbaru.DataBind()
        ddljabatanbaru.SelectedIndex = intselectIndex

    End Sub

#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPMUTASIPENGHASILAN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()
        Dim newListItem1 As ListItem
        Dim newListItem2 As ListItem

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & " AND MutasiPenghasilanCode Like '%" & strID & "%' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMUTASI_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            LblidM.Text = strID
            strEmpCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpCode"))
            txtdoc.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Dari_Lokasi"))
            lblempcode.Text = strEmpCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpName")) & ")"
            txtefektifdate.Text = Date_Validation(objEmpCodeDs.Tables(0).Rows(0).Item("Tgl_Pindah"), True)
            txtgajinetto.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Gaji_Netto"))
            lblStatus.Text = Trim(iif(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Status")) = "1", "Active", "Delete"))
            lblDateCreated.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("uName"))

            ddlAccMonth.SelectedValue = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PeriodeMonth"))
            ddlAccYear.SelectedValue = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PeriodeYear"))
            ddlType.SelectedValue = objEmpCodeDs.Tables(0).Rows(0).Item("TrxType")
            ddlType.Enabled = False 
			txtgajipokok.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Gapok"))
			txtpremi.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Premi"))
			txtpremitetap.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PremiTetap"))
			txtpremilain.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PremiLain"))
			txtlembur.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Lembur"))
			txttlain.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TLain"))
			txtastek.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Astek"))
			txtcatuberas.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CatuBeras"))
			txtRapel.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Rapel"))
			txtthrbonus.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("THRBonus"))
			txtpotjbt.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("POTJBT"))
			txtpotjht.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PotJHT"))
			txtpotlain.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PotLain"))
			txtpph21.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PotPPH21"))
			
			showhide()
            'idWH.Value = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IdWorkHist"))

        End If
    End Sub

	Sub LoadMutasi(ByVal ec As String)
		Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPMUTASIPENGHASILAN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
		strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & " AND TrxType='1' AND A.EmpCode = '" & ec & "' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMUTASI_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
		    isNew.Value = "False"
            strID = Trim(ObjEmpCodeDs.Tables(0).Rows(0).Item("MutasiPenghasilanCode"))
			LblidM.Text = strID
            strEmpCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpCode"))
            txtdoc.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Dari_Lokasi"))
            lblempcode.Text = strEmpCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpName")) & ")"
            txtefektifdate.Text = Date_Validation(objEmpCodeDs.Tables(0).Rows(0).Item("Tgl_Pindah"), True)
            txtgajinetto.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Gaji_Netto"))
            lblStatus.Text = Trim(iif(objEmpCodeDs.Tables(0).Rows(0).Item("Status") = "1", "Active", "Delete"))
            lblDateCreated.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("uName"))

            ddlAccMonth.SelectedValue = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PeriodeMonth"))
            ddlAccYear.SelectedValue = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PeriodeYear"))
            ddlType.SelectedValue = objEmpCodeDs.Tables(0).Rows(0).Item("TrxType")

			txtgajipokok.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Gapok"))
			txtpremi.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Premi"))
			txtpremitetap.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PremiTetap"))
			txtpremilain.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PremiLain"))
			txtlembur.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Lembur"))
			txttlain.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TLain"))
			txtastek.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Astek"))
			txtcatuberas.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CatuBeras"))
			txtRapel.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Rapel"))
			txtthrbonus.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("THRBonus"))
			txtpotjbt.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("POTJBT"))
			txtpotjht.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PotJHT"))
			txtpotlain.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PotLain"))
			txtpph21.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PotPPH21"))
		    
			
        Else
			if isNew.Value = "True"
            strID = getcode()
			LblidM.Text = strID
            txtdoc.Text = ""
            lblempcode.Text = ""
            txtefektifdate.Text = objGlobal.GetShortDate(strDateFmt, Now())
            txtgajinetto.Text = ""
            lblStatus.Text = ""
            lblDateCreated.Text = ""
            lblLastUpdate.Text = ""
            lblUpdatedBy.Text = ""

            ddlAccMonth.SelectedIndex = CInt(Session("SS_SELACCMONTH")) - 1
            BindAccYear(Session("SS_SELACCYEAR"))
         
			txtgajipokok.Text = ""
			txtpremi.Text = ""
			txtpremitetap.Text = ""
			txtpremilain.Text = ""
			txtlembur.Text = ""
			txttlain.Text = ""
			txtastek.Text = ""
			txtcatuberas.Text = ""
			txtRapel.Text = ""
			txtthrbonus.Text = ""
			txtpotjbt.Text = ""
			txtpotjht.Text = ""
			txtpotlain.Text = ""
			txtpph21.Text = ""
			end if
        End If
		    showhide()
	End Sub
	
	Sub showhide()
			Dim divmutasi As Control = Me.FindControl("divmutasi")
			Dim divkoreksi As Control = Me.FindControl("divkoreksi")
			Dim cast1 As HtmlGenericControl = CType(divmutasi, HtmlGenericControl)
			Dim cast2 As HtmlGenericControl = CType(divkoreksi, HtmlGenericControl)
			
			cast1.Visible = False
			cast2.Visible = False
			Select Case Trim(ddlType.SelectedItem.Value) 
				Case "1" 
					cast1.Visible = True
				Case "2"
					cast2.Visible = True
				Case ""
				    cast1.Visible = False
					cast2.Visible = False
			End Select
	End Sub
	
	Sub ddlType_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
			
            if 	ddlType.SelectedItem.Value.Trim() = "1" Then
				LoadMutasi(ddlempcode.SelectedItem.Value.Trim())
				else
				showhide()
            End if			

	End Sub

    Sub ddldivisicode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpDivCode = ddldivisicode.SelectedItem.Value.Trim()
        BindEmployee(strEmpDivCode)
    End Sub

    Sub ddlempcode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
	    if ddlempcode.SelectedItem.Value.Trim() <> "" then
			lbljabatanlama.Text = getJabatanLama(ddlempcode.SelectedItem.Value.Trim())
			LoadMutasi(ddlempcode.SelectedItem.Value.Trim())
		end if
    End Sub

    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "HR_HR_TRX_EMPMUTASIPENGHASILAN_UPD"
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String
        Dim strdtefektif As String
		Dim strID As String
		Dim strEmpCode As String 


        If isNew.Value = "True" Then
            'insert data baru
			strID = getCode()
			
            If ddlempcode.SelectedItem.Value.Trim() = "" Then
                lblErrMessage.Text = "Silakan pilih employee code !!"
                lblErrMessage.Visible = True
                Exit Sub
            End If

            strdtefektif = Date_Validation(txtefektifdate.Text.Trim(), False)
            If strdtefektif = "" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Silakan input tanggal pindah !"
                Exit Sub
            End If

            If txtdoc.Text.Trim = "" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Silakan input dari lokasi !"
                Exit Sub
            End If
			
			strEmpCode = ddlempcode.SelectedItem.Value.Trim()

        Else
            'update data
			strID = lblidM.Text.Trim() 
			
            strdtefektif = Date_Validation(txtefektifdate.Text.Trim(), False)
			
            If strdtefektif = "" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Silakan input tanggal pindah !"
                Exit Sub
            End If

            If txtdoc.Text.Trim = "" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Silakan input dari lokasi !"
                Exit Sub
            End If
			
			strEmpCode = Left(rtrim(lblempcode.Text),InStr(lblempcode.Text, "(")-1)
		End If

		    
			ParamNama = "MC|EC|TP|DL|GN|LOC|CD|UD|UI|PSM|PSY|TRXTYPE|TGP|TPM|TPT|TPL|TLR|TTL|TAT|TCB|TRP|TTB|PJB|PAT|PPL|P21"
            ParamValue = strID & "|" & _
                         strEmpCode & "|" & _
                         strdtefektif & "|" & _
                         txtdoc.Text.Trim() & "|" & _
                         iif(txtgajinetto.Text.Trim() ="",0,txtgajinetto.Text.Trim()) & "|" & _
                         strLocation & "|" & _
                         Now() & "|" & _
                         Now() & "|" & _
                         strUserId & "|" & _
                         ddlAccMonth.SelectedItem.Value.Trim() & "|" & _
                         ddlAccYear.SelectedItem.Value.Trim() & "|" & _
                         ddlType.SelectedItem.Value & "|" & _
						 iif(txtgajipokok.Text.Trim() ="",0,txtgajipokok.Text.Trim()) & "|" & _
						 iif(txtpremi.Text.Trim() ="",0,txtpremi.Text.Trim()) & "|" & _
						 iif(txtpremitetap.Text.Trim() ="",0,txtpremitetap.Text.Trim()) & "|" & _
						 iif(txtpremilain.Text.Trim() ="",0,txtpremilain.Text.Trim()) & "|" & _
						 iif(txtlembur.Text.Trim() ="",0,txtlembur.Text.Trim()) & "|" & _
						 iif(txttlain.Text.Trim() ="",0,txttlain.Text.Trim()) & "|" & _
						 iif(txtastek.Text.Trim() ="",0,txtastek.Text.Trim()) & "|" & _
						 iif(txtcatuberas.Text.Trim() ="",0,txtcatuberas.Text.Trim()) & "|" & _
						 iif(txtRapel.Text.Trim() ="",0,txtRapel.Text.Trim()) & "|" & _
						 iif(txtthrbonus.Text.Trim() ="",0,txtthrbonus.Text.Trim()) & "|" & _
						 iif(txtpotjbt.Text.Trim() ="",0,txtpotjbt.Text.Trim()) & "|" & _
						 iif(txtpotjht.Text.Trim() ="",0,txtpotjht.Text.Trim()) & "|" & _
						 iif(txtpotlain.Text.Trim() ="",0,txtpotlain.Text.Trim()) & "|" & _
						 iif(txtpph21.Text.Trim() ="",0,txtpph21.Text.Trim())
						 


            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
            End Try

            BackBtn_Click(Sender, E)
    End Sub

    Sub DelBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "HR_HR_TRX_EMPMUTASIPENGHASILAN_DEL"
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String
		Dim strID As String = lblidM.Text.Trim() 

        ParamNama = "MC|UI"
        ParamValue = strID & "|" & strUserId

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMUTASI_DEL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        BackBtn_Click(Sender, E)
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_MutasiPenghasilanList_Estate.aspx")
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

        ddlAccYear.DataSource = objAccYearDs.Tables(0)
        ddlAccYear.DataValueField = "AccYear"
        ddlAccYear.DataTextField = "UserName"
        ddlAccYear.DataBind()
        ddlAccYear.SelectedIndex = intSelectedIndex - 1
    End Sub

#End Region

End Class
