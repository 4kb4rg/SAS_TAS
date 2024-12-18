Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Globalization
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class PR_trx_AngsuranDet_Estate : Inherits Page

#Region "Declare Var"


    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgAngsur As DataGrid
    Protected WithEvents dgBayar As DataGrid
    Protected WithEvents dgBil As DataGrid

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents idMDR As HtmlInputHidden
    Protected WithEvents isNew As HtmlInputHidden

    Protected WithEvents LblidM As Label
    Protected WithEvents lblmonth_end As TextBox
    Protected WithEvents lblyear_end As TextBox
    Protected WithEvents lbldivisi As Label
    Protected WithEvents lblempcode As Label
    Protected WithEvents hidangsuran As HiddenField
    
    Protected WithEvents ddldivisicode As DropDownList
    Protected WithEvents ddltype As DropDownList

    Protected WithEvents ddlempcode As DropDownList
    Protected WithEvents ddlmonth_start As DropDownList
    Protected WithEvents ddlyear As DropDownList
	

    Protected WithEvents txtket As TextBox
    Protected WithEvents txtangsurantot As TextBox
    Protected WithEvents txtangsuranbln As TextBox
    Protected WithEvents txtangsuran As TextBox

    Protected WithEvents txtyear_start As TextBox

	Protected WithEvents ddlpay_krdCode As DropDownList
    Protected WithEvents txtWPDate As TextBox
    Protected WithEvents ddlpay_sisa As TextBox
    Protected WithEvents ddlpay_pay As TextBox
	Protected WithEvents lblpay_prd As label
	Protected WithEvents lblpay_prdawl As label

	
    Protected WithEvents btnSelDate As Image
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents BackBtn As ImageButton
    Protected WithEvents ApvBtn As ImageButton

	
	
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

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
    Dim intStatus As Integer
    Dim TarifLembur As Single
    Dim intLevel As Integer

    Dim strID As String = ""
    Dim strEmpCode As String = ""
    Dim strEmpDivCode As String = ""
    Dim strAcceptFormat As String

#End Region

#Region "Page Load"
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            ApvBtn.Attributes("onclick") = "javascript:return ConfirmAction('Confirm');"

            strID = Trim(IIf(Request.QueryString("CodeEmp") <> "", Request.QueryString("CodeEmp"), Request.Form("CodeEmp")))

            lblErrMessage.Visible = False

            If Not IsPostBack Then
                ddlmonth_start.SelectedIndex = Month(Now()) - 1
                BindAccYear(Year(Now()))
				txtWPDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
				
                If strID <> "" Then
                    isNew.Value = "False"
                    onLoad_Display()
                Else
                    isNew.Value = "True"
                    BindDivision()
					LblidM.text = getCode()
                End If
          
                onLoad_button()
            End If
        End If
    End Sub

#End Region

#Region "Function & Procedure"
    Public Function getCulturInfoByCurr(ByVal curr As String) As CultureInfo
        If (curr = "USD") Then
            Return (New CultureInfo("en-US", False))

        Else
            Return (New CultureInfo("id-ID", False))
        End If

    End Function

    Public Function currFormat(ByVal strnilai As String, ByVal curr As String) As String
        Dim s As String
        If (strnilai = "") Then
            Return ""
        End If

        Dim d As Decimal = Convert.ToDecimal(strnilai)
        Dim kultur As CultureInfo = getCulturInfoByCurr(curr)

        kultur.NumberFormat.CurrencySymbol = ""

        s = String.Format(kultur, "{0:c4}", d) '4 angka di belakang koma

        Return s

    End Function

    Public Function changeCurrFormat(ByVal strnilai As String, ByVal currawal As String, ByVal currakhir As String) As String
        Dim s As String

        If (strnilai = "") Then
            Return ""
        End If

        Dim d As Decimal = Decimal.Parse(strnilai, getCulturInfoByCurr(currawal))

        s = currFormat(d.ToString(), currakhir)

        Return s
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

    Function getCode() As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String
        Dim dt As String = objGlobal.GetShortDate(strDateFmt, Now())

        tcode = "KRD/" & strLocation & "/" & Mid(Trim(dt), 4, 2) & Right(Trim(dt), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|18|" & tcode & "|3"

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
        If isNew.Value = "False" Then
            ddldivisicode.Visible = False
            lbldivisi.Visible = True
            ddlempcode.Visible = False
            lblempcode.Visible = True

            ddlmonth_start.Enabled = True
            ddlyear.Enabled = True
            txtket.ReadOnly = False
            txtangsurantot.ReadOnly = False
            txtangsuranbln.ReadOnly = False
			
			txtket.Text = ""
			txtangsurantot.text = ""
			txtangsuranbln.text = ""
			txtangsuran.text = "0"
			lblmonth_end.text = ""
			lblyear_end.text = ""
			
			
                SaveBtn.Visible = True
                DelBtn.Visible = False
                ApvBtn.Visible = False
           Else
            ddldivisicode.Visible = True
            lbldivisi.Visible = False
            ddlempcode.Visible = True
            lblempcode.Visible = False
            ddlmonth_start.Enabled = True
            ddlyear.Enabled = True
            txtket.ReadOnly = False
            txtangsurantot.ReadOnly = False
            txtangsuranbln.ReadOnly = False
			SaveBtn.Visible = True
            ApvBtn.Visible = False
            DelBtn.Visible = False
			
			txtket.Text = ""
			txtangsurantot.text = ""
			txtangsuranbln.text = ""
			txtangsuran.text = "0"
			lblmonth_end.text = ""
			lblyear_end.text = ""
        End If
    End Sub

#End Region

#Region "Binding"
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
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, _
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

   

    Sub BindDivision()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim ObjDiv As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode = '" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, ObjDiv)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If ObjDiv.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To ObjDiv.Tables(0).Rows.Count - 1
                ObjDiv.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(ObjDiv.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                ObjDiv.Tables(0).Rows(intCnt).Item("Description") = Trim(ObjDiv.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(ObjDiv.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = ObjDiv.Tables(0).NewRow()
        dr("BlkGrpCode") = " "
        dr("Description") = "Pilih Divisi"
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
		Dim strAM as String
		
		If cint(strAccMonth) < 10 Then
            strAm = "0" & strAccMonth
        Else
            strAm =  strAccMonth
        End If

		'strAccYear = Session("SS_PRACCYEAR")

        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strlocation & "|" & strAm  & "|" & strAccYear & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%'|ORDER BY A.EmpName"

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
        dr("_Description") = "Pilih Karyawan"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlempcode.DataSource = objEmpCodeDs.Tables(0)
        ddlempcode.DataTextField = "_Description"
        ddlempcode.DataValueField = "EmpCode"
        ddlempcode.DataBind()
        ddlempcode.SelectedIndex = intSelectedIndex

    End Sub

	Sub Get_Angsuran(byVal krd as String)
		Dim strOpCd_EmpDiv As String = "PR_PR_TRX_ANGSURAN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim sc As String
        Dim intErrNo As Integer

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & _
                        "AND krdcode='" & krd & "'|" & _
                        "ORDER BY KrdCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ANGSURAN_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

		If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
           ddltype.SelectedValue = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("typekrd")) 
          
		   txtket.text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("ket"))
		   txtangsuranTot.text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("totalrp"))
		   txtangsuranBln.text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("totalbulan"))
		   txtangsuran.text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("krdperbulan"))
		   hidangsuran.value = txtangsuran.text
		   ddlMonth_start.selectedindex = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("bulanmulai"))-1
		   ddlyear.text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("tahunmulai"))
		   lblmonth_end.text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("bulanselesai"))
		   lblyear_end.text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("tahunselesai"))
		   
		   lblDateCreated.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("createdate"))
		   lblStatus.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("status"))
		   lblLastUpdate.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("updatedate"))
		   lblUpdatedBy.Text =  Trim(objEmpCodeDs.Tables(0).Rows(0).Item("uname"))
		   
		   if Trim(objEmpCodeDs.Tables(0).Rows(0).Item("st")) = "1" 
		  
			   DelBtn.Visible = True
               ApvBtn.Visible = True
		   else
		       SaveBtn.Visible = False 
		       DelBtn.Visible = False
               ApvBtn.Visible = False
		   end if
		   
        End If

	End Sub
	
    Function Bind_Angsuran(ByVal ec As String) As DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_ANGSURAN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim sc As String
        Dim intErrNo As Integer

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & _
                        "AND CodeEmp='" & ec & "'|" & _
                        "ORDER BY KrdCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ANGSURAN_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try
        Return objEmpCodeDs

    End Function
	
	
	Sub Bind_AngsuranPay(ByVal ec As String)
		Dim strOpCd_EmpDiv As String = "PR_PR_TRX_ANGSURAN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim sc As String
        Dim intErrNo As Integer
		Dim intCnt As Integer
		Dim dr As DataRow

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & _
                        "AND CodeEmp='" & ec & "' And a.Status='3' And SisaKredit <> 0|" & _
                        "ORDER BY KrdCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ANGSURAN_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try
		
		If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("krdcode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("krdcode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("status") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("krdcode")) & " (sisa :" & Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("sisakredit")) & ")"
            Next
        End If
		
		dr = objEmpCodeDs.Tables(0).NewRow()
        dr("krdcode") = ""
        dr("status") = "Pilih Angsuran"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)
		
		ddlpay_krdCode.DataSource = objEmpCodeDs.Tables(0)
        ddlpay_krdCode.DataTextField = "status"
        ddlpay_krdCode.DataValueField = "krdcode"
        ddlpay_krdCode.DataBind()
	End Sub

	Sub Get_AngsuranPay(ByVal ec As String)
		Dim strOpCd_EmpDiv As String = "PR_PR_TRX_ANGSURAN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim sc As String
        Dim intErrNo As Integer
		
        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & _
                        "AND KrdCode='" & ec & "'|" 

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ANGSURAN_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try
		
		If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
               ddlpay_sisa.text = objEmpCodeDs.Tables(0).Rows(0).Item("sisakredit") 
               ddlpay_pay.text =  0
			   lblpay_prd.text = trim(objEmpCodeDs.Tables(0).Rows(0).Item("tahunselesai")) & trim(objEmpCodeDs.Tables(0).Rows(0).Item("bulanselesai"))
			   lblpay_prdawl.text = trim(objEmpCodeDs.Tables(0).Rows(0).Item("tahunmulai")) & trim(objEmpCodeDs.Tables(0).Rows(0).Item("bulanmulai"))
        End If
		
	
	End Sub
	
    Function Bind_Bayar(ByVal ec As String) As DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_ANGSURAN_BAYAR_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim sc As String
        Dim intErrNo As Integer

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & _
                        "AND CodeEmp='" & ec & "'|" & _
                        "ORDER BY AccYear,AccMonth,A.KrdCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ANGSURAN_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try
        Return objEmpCodeDs
    End Function

    Function Bind_Billing(ByVal ec As String) As DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_ANGSURAN_BILLING_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim sc As String
        Dim intErrNo As Integer

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strlocation & "|" & _
		                "AND CodeEmp like '" & ec & "%'|" & _
                        "ORDER BY AccYear,AccMonth"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ANGSURAN_BILLING_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try
        Return objEmpCodeDs
    End Function

#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_ANGSURAN_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "LOC|ST|SEARCH|SORT"
        strParamValue = strLocation & "|1|" & " WHERE x.codeemp = '" & strID & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ANGSURAN_GET_LIST&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            LblidM.Text = ""
            strEmpCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("codeemp"))
            lbldivisi.Text = objEmpCodeDs.Tables(0).Rows(0).Item("IdDiv") & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Divisi")) & ")"
            lblempcode.Text = strEmpCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpName")) & ")"

            Angsuran_OnLoad()
            Bayar_OnLoad()
            Billing_OnLoad()
        End If
    End Sub

    Sub ddldivisicode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpDivCode = ddldivisicode.SelectedItem.Value.Trim()
        BindEmployee(strEmpDivCode)
    End Sub
	
	

	Sub getitem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        lblidM.Text = ""
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbl As Label
            Dim hid As HiddenField
            Dim lst As DropDownList

            lbl = dgAngsur.Items.Item(CInt(e.Item.ItemIndex)).FindControl("dgAngsur_KrdCode")
            lblidM.Text = lbl.Text
			
			Get_Angsuran(lbl.Text.Trim())
            
        End If
    End Sub
	
    Sub Save(ByVal st As String)
        Dim strOpCd As String = "PR_PR_TRX_ANGSURAN_UPD"
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer
        Dim mend As String
        Dim yend As String

        Dim strEmpCode As String
		
        If isNew.Value = "True"  Then
            If ddlempcode.Text = "" Then
                lblErrMessage.Text = "Silakan pilih employee code !!"
                lblErrMessage.Visible = True
                Exit Sub
            End If
            LblidM.Text = getCode()
            strEmpCode = ddlempcode.SelectedItem.Value.Trim()
        Else
		    if LblidM.text.trim = "" then
			LblidM.text = getCode()
			end if
            strEmpCode = Left(lblempcode.Text.Trim(), InStr(lblempcode.Text.Trim(), "(") - 1) 
        End If

        If txtangsurantot.Text.Trim = "" Then
            lblErrMessage.Text = "Silakan input Tot.Angsuran(Rp)!!"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If txtangsuranbln.Text.Trim = "" Then
            lblErrMessage.Text = "Silakan input Tempo!!"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        txtangsuran.Text = hidangsuran.Value

        mend = Request.Form("lblmonth_end")
        yend = Request.Form("lblyear_end")

        ParamNama = "ID|EC|IC|KET|TOT|BLN|ASR|MS|YS|ME|YE|LOC|ST|CD|UD|UI|TY"
        ParamValue = LblidM.Text.Trim() & "|" & _
                     strEmpCode & "|" & _
					 "" & "|" & _
                     txtket.Text.Trim() & "|" & _
                     txtangsurantot.Text.Trim() & "|" & _
                     txtangsuranbln.Text.Trim() & "|" & _
                     txtangsuran.Text & "|" & _
                     ddlmonth_start.SelectedItem.Value.Trim() & "|" & _
                     ddlyear.SelectedItem.Value.Trim() & "|" & _
                     iif(len(Trim(mend))=1,"0" & Trim(mend) ,Trim(mend)) & "|" & _
                     Trim(yend) & "|" & _
                     strLocation & "|" & _
                     st & "|" & _
                     Now() & "|" & _
                     Now() & "|" & _
                     strUserId & "|" & _
					 ddltype.SelectedItem.Value.Trim() 


        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try

		strID = trim(strEmpCode)
		isNew.Value = "False" 
		
    End Sub
	
	Sub Del(ByVal st As String)
	    Dim strOpCd As String = "PR_PR_TRX_ANGSURAN_DEL"
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		
		ParamNama = "ID|LOC"
        ParamValue = LblidM.Text.Trim() & "|" & strlocation 
		
		Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try

		isNew.Value = "False" 
		
		
	End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
	    STRID = ""
        isNew.Value= "True" 
        LblidM.text = getCode()
		BindDivision()
		onLoad_button()
		Angsuran_OnLoad()
        Bayar_OnLoad()
        Billing_OnLoad()
    End Sub

    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Save("1")
		onLoad_Display()
		onLoad_button()
		'Angsuran_OnLoad()
        'Bayar_OnLoad()
        'Billing_OnLoad()
        'BackBtn_Click(Sender, E)
    End Sub

    Sub DelBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Del("1")
        Angsuran_OnLoad()
        Bayar_OnLoad()
        Billing_OnLoad()
    End Sub

    Sub ApvBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Save("3")
			   SaveBtn.Visible = False 
		       DelBtn.Visible = False
               ApvBtn.Visible = False
        Angsuran_OnLoad()
        Bayar_OnLoad()
        Billing_OnLoad()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_AngsuranList_Estate.aspx")
    End Sub

    Sub Angsuran_OnLoad()
        dgAngsur.DataSource = Bind_Angsuran(strID)
        dgAngsur.DataBind()
    End Sub

    Sub Bayar_OnLoad()
        dgBayar.DataSource = Bind_Bayar(strID)
        dgBayar.DataBind()
		Dim intCnt As Integer
        Dim lbButton As LinkButton
		
		For intCnt = 0 To dgBayar.Items.Count - 1
            lbButton = dgBayar.Items.Item(intCnt).FindControl("lbDelete")
            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        Next

		Bind_AngsuranPay(strID)
    End Sub

    Sub Billing_OnLoad()
        dgBil.DataSource = Bind_Billing(strID)
        dgBil.DataBind()
    End Sub

	Sub ddlpay_krdCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
	    if ddlpay_krdCode.SelectedItem.Value.Trim() <> "" then
			Get_AngsuranPay(ddlpay_krdCode.SelectedItem.Value.Trim())
		end if
	End Sub
	
	Sub PayBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
		Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)
		Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
		
		If ddlpay_krdCode.SelectedItem.Value.Trim() = "" Then
			lblErrMessage.Text = "Silakan pilih angsuran"
			lblErrMessage.Visible = True
			exit sub
		End if
		
		If (SDate = "") Then
            lblErrMessage.Text = "Silakan input tanggal bayar" 
			lblErrMessage.Visible = True
            exit sub
        End If
		
		If SY.Trim & SM.Trim < lblpay_prdawl.Text.Trim() Then
			lblErrMessage.Text = "Tanggal bayar < dari periode awal" 
			lblErrMessage.Visible = True
            exit sub
		End if
		
		If SY.Trim & SM.Trim > lblpay_prd.Text.Trim() Then
			lblErrMessage.Text = "Tanggal bayar > dari periode akhir" 
			lblErrMessage.Visible = True
            exit sub
		End if
		
		if ddlpay_pay.text.trim = ""  then 'or ddlpay_pay.text.trim = "0" then
			lblErrMessage.Text = "input jumlah pembayaran" 
			lblErrMessage.Visible = True
            exit sub
		End if
		
		if Convert.ToDouble(ddlpay_pay.text.trim) > Convert.ToDouble(ddlpay_sisa.text.trim)  then
			lblErrMessage.Text = "jumlah pembayaran > sisa kredit" 
			lblErrMessage.Visible = True
            exit sub
		End if
		
		
		Dim strOpCd As String = "PR_PR_TRX_ANGSURAN_BAYAR_UPD"
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		
		ParamNama = "ID|MN|YR|DT|PAY|LOC|UI"
        ParamValue = ddlpay_krdCode.SelectedItem.Value.Trim() & "|" & _ 
		             SM.Trim & "|" & _ 
					 SY.Trim & "|" & _ 
					 SDate & "|" & _ 
					 ddlpay_pay.text & "|" & _ 
					 strlocation & "|" & _ 
					 strUserId 
		
		Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try

		isNew.Value = "False" 
		
		Angsuran_OnLoad()
        Bayar_OnLoad()
        Billing_OnLoad()
	
	End Sub
	
	Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Status As String = "PR_PR_TRX_ANGSURAN_BAYAR_DEL"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strValue As String
        Dim lblID As Label
        Dim lbMN As Label
		Dim lbYR As Label
		
        Dim strEmpCode As String
        Dim strstatus As String

        dgBayar.EditItemIndex = CInt(E.Item.ItemIndex)
        lblID = dgBayar.Items.Item(CInt(E.Item.ItemIndex)).FindControl("dgBayar_KrdCode")
        lbMN = dgBayar.Items.Item(CInt(E.Item.ItemIndex)).FindControl("dgBayar_AccMonth")
        lbYR = dgBayar.Items.Item(CInt(E.Item.ItemIndex)).FindControl("dgBayar_AccYear")

        strParam = "ID|MN|YR|LOC|UI"
        strValue = lblID.text.trim() & "|" & lbMN.text.trim() & "|" & lbYR.text.trim() & "|" & strlocation & "|" & strUserId

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Status, strParam, strValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ANGSURAN_BAYAR_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try
		
        isNew.Value = "False" 
		
		Angsuran_OnLoad()
        Bayar_OnLoad()
        Billing_OnLoad()
    End Sub
	
	Sub ANGSUR_DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Status As String = "PR_PR_TRX_ANGSURAN_DEL"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strValue As String
        Dim lblID As Label
        Dim lbMN As Label
		Dim lbYR As Label
		
        Dim strEmpCode As String
        Dim strstatus As String

        dgAngsur.EditItemIndex = CInt(E.Item.ItemIndex)
        lblID = dgAngsur.Items.Item(CInt(E.Item.ItemIndex)).FindControl("dgAngsur_KrdCode")
      
        strParam = "ID|LOC|UI"
        strValue = lblID.text.trim() & "|" & strlocation & "|" & strUserId

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Status, strParam, strValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ANGSURAN_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try
		
        isNew.Value = "False" 
		
		Angsuran_OnLoad()
        Bayar_OnLoad()
        Billing_OnLoad()
    End Sub
#End Region

End Class
