Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient


Imports agri.HR
Imports agri.PR
Imports agri.GlobalHdl
Imports agri.PWSystem

Public Class PR_mthend_payrollprocess : Inherits Page

    Protected WithEvents rbAllEmp As RadioButton
    Protected WithEvents rbSelectedEmp As RadioButton
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lblCompleteSetup As Label
	
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents dgprocess As DataGrid
	Protected WithEvents dgnotmatch As DataGrid

    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox

    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlmandor As DropDownList
	
	Protected WithEvents btnProceed As ImageButton
	Protected WithEvents btnRefresh As ImageButton
	Protected WithEvents btnPosted As ImageButton
	Protected WithEvents btnConfirm As ImageButton
	
    Protected WithEvents dgAMPRAH As DataGrid
    Protected WithEvents dgRAPEL As DataGrid
    Protected WithEvents dgBONUS As DataGrid
    Protected WithEvents dgTHR As DataGrid
	
	'PPH21
    Protected WithEvents dgKRYWN21 As DataGrid
    Protected WithEvents dgNONKRYWN21 As DataGrid
    Protected WithEvents dgKRYWNThn21 As DataGrid
    Protected WithEvents dgNONKRYWNThn21 As DataGrid
	Protected WithEvents BtnPreviewdgKRYWN21 As ImageButton
	Protected WithEvents BtnPreviewNONKRYWN21 As ImageButton
	Protected WithEvents btnGenerateThn21 As ImageButton
	Protected WithEvents BtnPreviewThn21 As ImageButton
	Protected WithEvents btnGenerateNONKRYWNThn21 As ImageButton
	Protected WithEvents btnPreviewNONKRYWNThn21 As ImageButton
	
	
	Protected WithEvents dgTRANSIT As DataGrid
    Protected WithEvents dgALOKASI As DataGrid
	Protected WithEvents dgRECON As DataGrid
	
	
	Protected WithEvents BtnPreviewdgAMPRAH As ImageButton
	Protected WithEvents BtnPreviewdgRAPEL As ImageButton
	Protected WithEvents BtnPreviewdgBONUS As ImageButton
	Protected WithEvents BtnPreviewdgTHR As ImageButton
	Protected WithEvents BtnPreviewTransit As ImageButton
	Protected WithEvents BtnPreviewAlokasi As ImageButton
	
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objOk As New agri.GL.ClsTrx()
    Dim objHR As New agri.HR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPR As New agri.PR.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim objEmpDs As New Object()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
    Dim strAcceptFormat As String

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
	Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        intPRAR = Session("SS_PRAR")

        strLocType = Session("SS_LOCTYPE")
		intLevel = Session("SS_USRLEVEL")
		
		IF intLevel < 2
		btnPosted.Visible = False
		btnRefresh.Visible = False
		btnConfirm.Visible = False
		End if
	
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrMessage.Text = ""
			btnGenerate.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnGenerate).ToString())
			btnRefresh.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnRefresh).ToString())
			btnConfirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnConfirm).ToString())
	        btnConfirm.Attributes("onclick") = "javascript:return confirm('ANDA YAKIN AKAN CONFIRM PAYROLL ?');"

			
            If Not Page.IsPostBack Then
               BindAccYear(Session("SS_SELACCYEAR"))
			   ddlMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
 
				IF GetStatus(cint(ddlMonth.SelectedItem.Value.Trim),ddlyear.SelectedItem.Value.Trim,strLocation,"1","","","","","Payroll") = "3" Then
					btngenerate.Visible = False
					btnPosted.Visible = False
					btnConfirm.Visible = False
				END IF
            End If
        End If
    End Sub

    Public Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_GRList.aspx")
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

   	Function GetCutOff(Byval yr as String)as Integer
	    Dim strOpCd As String = "PR_PR_STP_CUTOFF_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim objCodeDs As New Object()

	    strParamName = "SEARCH"
        strParamValue = "AND A.LocCode = '" & strLocation & "' AND A.COYear='" & yr & "'"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_CUTOFF_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try
		
		Return objCodeDs.Tables(0).Rows.Count
		
	End Function

    Sub ShowHideDiv(ByVal S as String)
        Dim found As Control = Me.FindControl(S)
        If Not found Is Nothing Then
            Dim cast As HtmlGenericControl = CType(found, HtmlGenericControl)
            If Not cast Is Nothing Then
                cast.Visible = True
            Else
                cast.Visible = False
            End If
        End If
    End Sub

   
    	
	Function GetStatus(Byval mn as String,Byval yr as String,Byval loc as String,Byval S1 as String,ByVal S2 as String,ByVal S3 as String , ByVal S4 as String, Byval S5 as String,ByVal ms as String)as Integer
        Dim strOpCdSP As String = "PR_PR_TRX_MTHEND_GET_STATUS_NEW"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
		Dim i as Integer
		DIm aa as String
      
       
        ParamName = "MN|YR|LOC|S1|S2|S3|S4|S5"
        ParamValue = mn & "|" & _
                     yr & "|" & _
                     loc & "|" & _
					 "1" & "|" & _
					 "2" & "|" & _
					 "3" & "|" & _
					 "4" & "|" & _
					 "5"
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_GET_STATUS&errmesg=" & Exp.Message & "&redirect=")
        End Try
	
		If objDataSet.Tables(0).Rows.Count > 0 Then
        		aa = objDataSet.Tables(0).Rows(0).Item("Status")
				IF trim(aa)="" Then
					i = 0
				Else
					i = cint(aa)
				End If
				IF i = 3 Then
					'UserMsgBox(Me, "Proses "+ ms.Trim()+", Periode "& mn & "/" & yr & " Sudah di Posting")
				End If
		Else
		        i = 0 
		end if
		
       Return i

    End Function
	
	
    Sub UpdateStatusPayroll(Byval mn as String,Byval yr as String,Byval loc as String,ByVal ty as String)
        Dim strOpCdSP As String = "PR_PR_TRX_MTHEND_UPD"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
      
       
        ParamName = "MN|YR|LOC|S1|S2|S3|S4|S5"
        ParamValue = mn & "|" & yr & "|" & loc & "|" & ty & "|" & ty & "|" & ty &"|" & ty &"|" & ty 
        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCdSP, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_UPD&errmesg=" & Exp.Message & "&redirect=")
        End Try

    End Sub
	
	 Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdSP As String = "PR_PR_MTHEND_PAYROLL_PROCESS_SP"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim strEc As String = ""

		IF GetStatus(cint(ddlMonth.SelectedItem.Value.Trim),ddlyear.SelectedItem.Value.Trim,strLocation,"1","","","","","Gaji Besar") = "3" Then
		  Exit Sub
		ELSE 
		  UpdateStatusPayroll(cint(ddlMonth.SelectedItem.Value.Trim),ddlyear.SelectedItem.Value.Trim,strLocation,"1")
		End IF
		
        If ddlMonth.SelectedItem.Value < 10 Then
            strMn = "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strMn = ddlMonth.SelectedItem.Value.Trim
        End If

        strYr = ddlyear.SelectedItem.Value.Trim

        	
		if GetCutOff(strYr)=0 Then
				lblErrMessage.Text = "Silakan isi setup cut off"
                lblErrMessage.Visible = True
                Exit Sub
		end if

        ParamName = "HEADER|ACCMONTH|ACCYEAR|LOC|UI|EC"
        ParamValue = "PAY/" & strLocation & "/|" & strMn & "|" & strYr & "|" & strLocation & "|" & strUserId & "|"

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCdSP, ParamName, ParamValue)
			UserMsgBox(Me, "Proses Hitung Gajian Selesai")
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_PAYROLL_PROCESS_SP&errmesg=" & Exp.Message & "&redirect=")
        End Try

    End Sub
	
	Sub btnPosted_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
		Dim strMn As String
        Dim strYr As String
        Dim strMn2 As String
        
		Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim objDataSet As New Object()
		
		strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim
        If ddlMonth.SelectedItem.Value.Trim < 10 Then
            strMn2 = "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strMn2 = ddlMonth.SelectedItem.Value.Trim
        End If

		ParamName = "ACCMONTH|ACCYEAR|COMP|LOC|UI"
		ParamValue = strMn2 & "|" & strYr & "|" & strCompany & "|" & strLocation & "|" & strUserId 
		Try
			intErrNo = objOk.mtdGetDataCommon("PR_PR_MTHEND_TUTUPBUKU_PROCESS_SP_ALL", ParamName, ParamValue, objDataSet)
			UserMsgBox(Me, "Proses Posting Ke Jurnal Selesai")
		Catch Exp As System.Exception
			Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_TUTUPBUKU_PROCESS_SP_ALL&errmesg=" & Exp.Message & "&redirect=")
		End Try
	End Sub
	
	Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
		UpdateStatusPayroll(cint(ddlMonth.SelectedItem.Value.Trim),ddlyear.SelectedItem.Value.Trim,strLocation,"3")
		btngenerate.Visible = False
		btnPosted.Visible = False
		btnConfirm.Visible = False
	End Sub

    Sub ViewData()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_GETLIST_ALL"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|GROUPTYPE|PPHTYPE"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & "1" & "|" & "BLN"

        'ParamName = "LOCCODE|ACCMONTH|ACCYEAR|STRSEARCH"
        'ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & _
        '     "AND A.EmpCode IN ( " & _
        '     " SELECT EmpCode " & _
        '     "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='1' AND CAST(AccMonth AS INT)=RTRIM('1') AND AccYear=RTRIM('" & Trim(strYr) & "') " & _
        '     "    UNION " & _
        '     "    SELECT EmpCode " & _
        '     "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='1' AND EmpCode NOT IN (SELECT EmpCode FROM HR_TRX_EMPHIST WHERE LocCode='" & Trim(strLocation) & "' AND CAST(AccMonth AS INT)=RTRIM('" & Trim(strMn) & "') AND AccYear=RTRIM('" & Trim(strYr) & "')) " & _
        '     "    AND (CAST(AccYear AS INT)*100)+CAST(AccMonth AS INT) <  (CAST('" & Trim(strYr) & "' AS INT)*100)+CAST(RTRIM('" & Trim(strMn) & "') AS INT) " & _
        '     "    GROUP BY LocCode, EmpCode, CodeEmpTy, IdDiv, CodeGrpJob " & _
        '     " ) "


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgKRYWN21.DataSource = objDataSet
        dgKRYWN21.DataBind()

        If objDataSet.Tables(0).Rows.Count > 0 Then
            'btnGenerate.Visible = False
            BtnPreviewdgKRYWN21.Visible = True
            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblEmpCode")
                If Trim(lbl.Text) = "ZZZZZZ" Then
                    lbl.Visible = False
                    'lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblEmpType")
                    'lbl.Visible = False
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblNama")
                    lbl.Text = Replace(lbl.Text, "ZZZ", "")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblRowNo")
                    lbl.Visible = False
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPeriod")
                    lbl.Visible = False
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblIDDiv")
                    lbl.Visible = False
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblCodeGrbJob")
                    lbl.Visible = False
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblCodeTg")
                    lbl.Visible = False
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblDOJ")
                    lbl.Visible = False
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblNPWP")
                    lbl.Visible = False
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblGapok")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPremi")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPremiTetap")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPremiLain")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblLembur")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblTPajak")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblTLain")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblAstek")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblCatuBeras")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblRapel")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblTHRBonus")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblTotPDP")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPotJbt")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPotJHT")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPotLain")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblTotPOT")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblDPP")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPTKP")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPKP")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPPH21Non")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPPH21Selisih")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN21.Items.Item(intCnt).FindControl("lblPPH21")
                    lbl.Font.Bold = True
                End If
            Next
        End If

    End Sub

    Sub ViewDataNonKrywn()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_GETLIST_ALL"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|GROUPTYPE|PPHTYPE"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & "2" & "|" & "BLN"

        'ParamName = "LOCCODE|ACCMONTH|ACCYEAR|STRSEARCH"
        'ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & _
        '            "AND A.EmpCode IN ( " & _
        '            " SELECT EmpCode " & _
        '            "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='2' AND CAST(AccMonth AS INT)=RTRIM('1') AND AccYear=RTRIM('" & Trim(strYr) & "') " & _
        '            "    UNION " & _
        '            "    SELECT EmpCode " & _
        '            "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='2' AND EmpCode NOT IN (SELECT EmpCode FROM HR_TRX_EMPHIST WHERE LocCode='" & Trim(strLocation) & "' AND CAST(AccMonth AS INT)=RTRIM('" & Trim(strMn) & "') AND AccYear=RTRIM('" & Trim(strYr) & "')) " & _
        '            "    AND (CAST(AccYear AS INT)*100)+CAST(AccMonth AS INT) <  (CAST('" & Trim(strYr) & "' AS INT)*100)+CAST(RTRIM('" & Trim(strMn) & "') AS INT) " & _
        '            "    GROUP BY LocCode, EmpCode, CodeEmpTy, IdDiv, CodeGrpJob " & _
        '            " ) "
                    
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
            End Try

        dgNONKRYWN21.DataSource = objDataSet
        dgNONKRYWN21.DataBind()

        If objDataSet.Tables(0).Rows.Count > 0 Then
                'btnGenerateNONKRYWN.Visible = False
            BtnPreviewNONKRYWN21.Visible = True

            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblEmpCodePPH")
                If Trim(lbl.Text) = "ZZZZZZ" Then
                    lbl.Visible = False
                    'lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblEmpTypePPH")
                    'lbl.Visible = False
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblNamaPPH")
                    lbl.Text = Replace(lbl.Text, "ZZZ", "")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblRowNoPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPeriodPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblIDDivPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblCodeGrbJobPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblCodeTgPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblDOJPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblNPWPPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblGapokPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPremiPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPremiTetapPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPremiLainPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblLemburPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblTPajakPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblTLainPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblAstekPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblCatuBerasPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblRapelPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblTHRBonusPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblTotPDPPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPotJbtPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPotJHTPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPotLainPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblTotPOTPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblDPPPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPTKPPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPKPPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPPH21PPHNon")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPPH21PPHSelisih")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN21.Items.Item(intCnt).FindControl("lblPPH21PPH")
                    lbl.Font.Bold = True
                    End If
            Next
            End If

    End Sub

    Sub ViewDataThn()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_GETLIST_ALL"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|GROUPTYPE|PPHTYPE"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & "1" & "|" & "THN"

        'ParamName = "LOCCODE|ACCMONTH|ACCYEAR|STRSEARCH"
        'ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & _
        '      "AND A.EmpCode IN ( " & _
        '      " SELECT EmpCode " & _
        '      "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='1' AND CAST(AccMonth AS INT)=RTRIM('1') AND AccYear=RTRIM('" & Trim(strYr) & "') " & _
        '      "    UNION " & _
        '      "    SELECT EmpCode " & _
        '      "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='1' AND EmpCode NOT IN (SELECT EmpCode FROM HR_TRX_EMPHIST WHERE LocCode='" & Trim(strLocation) & "' AND CAST(AccMonth AS INT)=RTRIM('" & Trim(strMn) & "') AND AccYear=RTRIM('" & Trim(strYr) & "')) " & _
        '      "    AND (CAST(AccYear AS INT)*100)+CAST(AccMonth AS INT) <  (CAST('" & Trim(strYr) & "' AS INT)*100)+CAST(RTRIM('" & Trim(strMn) & "') AS INT) " & _
        '      "    GROUP BY LocCode, EmpCode, CodeEmpTy, IdDiv, CodeGrpJob " & _
        '      " ) "


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgKRYWNThn21.DataSource = objDataSet
        dgKRYWNThn21.DataBind()

        If objDataSet.Tables(0).Rows.Count > 0 Then
            btnGenerateThn21.Visible = True
            BtnPreviewThn21.Visible = True
            
            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblEmpCodeThn")
                If Trim(lbl.Text) = "ZZZZZZ" Then
                    lbl.Visible = False
                    'lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblEmpTypeThn")
                    'lbl.Visible = False
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblNamaThn")
                    lbl.Text = Replace(lbl.Text, "ZZZ", "")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblRowNoThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPeriodThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblIDDivThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblCodeGrbJobThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblCodeTgThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblDOJThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblNPWPThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblGapokThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPremiThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPremiTetapThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPremiLainThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblLemburThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblTPajakThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblTLainThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblAstekThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblCatuBerasThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblRapelThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblTHRBonusThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblTotPDPThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPotJbtThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPotJHTThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPotLainThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblTotPOTThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblDPPThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPTKPThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPKPThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPPH21NonThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPPH21SelisihThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPPH21Thn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPPH21DiSetorThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn21.Items.Item(intCnt).FindControl("lblPPH21KurangBayarThn")
                    lbl.Font.Bold = True
                End If
            Next
        End If

    End Sub

    Sub ViewDataNonKrywnThn()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_GETLIST_ALL"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|GROUPTYPE|PPHTYPE"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & "2" & "|" & "THN"

        'ParamName = "LOCCODE|ACCMONTH|ACCYEAR|STRSEARCH"
        'ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & _
        '            "AND A.EmpCode IN ( " & _
        '            " SELECT EmpCode " & _
        '            "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='2' AND CAST(AccMonth AS INT)=RTRIM('1') AND AccYear=RTRIM('" & Trim(strYr) & "') " & _
        '            "    UNION " & _
        '            "    SELECT EmpCode " & _
        '            "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='2' AND EmpCode NOT IN (SELECT EmpCode FROM HR_TRX_EMPHIST WHERE LocCode='" & Trim(strLocation) & "' AND CAST(AccMonth AS INT)=RTRIM('" & Trim(strMn) & "') AND AccYear=RTRIM('" & Trim(strYr) & "')) " & _
        '            "    AND (CAST(AccYear AS INT)*100)+CAST(AccMonth AS INT) <  (CAST('" & Trim(strYr) & "' AS INT)*100)+CAST(RTRIM('" & Trim(strMn) & "') AS INT) " & _
        '            "    GROUP BY LocCode, EmpCode, CodeEmpTy, IdDiv, CodeGrpJob " & _
        '            " ) "


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgNONKRYWNThn21.DataSource = objDataSet
        dgNONKRYWNThn21.DataBind()

        If objDataSet.Tables(0).Rows.Count > 0 Then
            btnGenerateNONKRYWNThn21.Visible = True
            BtnPreviewNONKRYWNThn21.Visible = True

            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblEmpCodePPHThn")
                If Trim(lbl.Text) = "ZZZZZZ" Then
                    lbl.Visible = False
                    'lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblEmpTypePPHThn")
                    'lbl.Visible = False
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblNamaPPHThn")
                    lbl.Text = Replace(lbl.Text, "ZZZ", "")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblRowNoPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPeriodPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblIDDivPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblCodeGrbJobPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblCodeTgPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblDOJPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblNPWPPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblGapokPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPremiPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPremiTetapPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPremiLainPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblLemburPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblTPajakPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblTLainPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblAstekPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblCatuBerasPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblRapelPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblTHRBonusPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblTotPDPPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPotJbtPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPotJHTPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPotLainPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblTotPOTPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblDPPPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPTKPPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPKPPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPPH21PPHNonThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPPH21PPHSelisihThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPPH21PPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPPH21DiSetorPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn21.Items.Item(intCnt).FindControl("lblPPH21KurangBayarPPHThn")
                    lbl.Font.Bold = True
                End If
            Next
        End If

    End Sub
	
	Sub btnRefreh_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
		Dim strOpCdSP As String = "PR_PR_MTHEND_PAYROLLPROCESS_STA_VIEW"
		Dim objDataSet As New Object()
	    Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
		Dim strMn As String
        Dim strYr As String
		
		If ddlMonth.SelectedItem.Value < 10 Then
            strMn = "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strMn = ddlMonth.SelectedItem.Value.Trim
        End If

        strYr = ddlyear.SelectedItem.Value.Trim
		ParamName = "ACCMONTH|ACCYEAR|LOC"
        ParamValue =  strMn & "|" & strYr & "|" & strLocation 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_PAYROLLPROCESS_STA_VIEW&errmesg=" & Exp.Message & "&redirect=")
        End Try
		
		dgAMPRAH.DataSource = Nothing
		dgAMPRAH.DataBind()
		dgRAPEL.DataSource = Nothing
		dgRAPEL.DataBind()
		dgBONUS.DataSource = Nothing
		dgBONUS.DataBind()
		dgTHR.DataSource = Nothing
		dgTHR.DataBind()
			
		If objDataSet.Tables(0).Rows.Count > 0 Then  ' Amprah
			dgAMPRAH.DataSource = objDataSet.Tables(0)
			dgAMPRAH.DataBind()
			BtnPreviewdgAMPRAH.Visible = True
		End If
		
		If objDataSet.Tables(1).Rows.Count > 0 Then  ' RAPEL		    
			dgRAPEL.DataSource = objDataSet.Tables(1)
			dgRAPEL.DataBind()
			BtnPreviewdgRAPEL.Visible = True
		End If
		
		If objDataSet.Tables(2).Rows.Count > 0 Then  ' BONUS		    
			dgBONUS.DataSource = objDataSet.Tables(2)
			dgBONUS.DataBind()
			BtnPreviewdgBONUS.Visible = True
		End If
		
		If objDataSet.Tables(3).Rows.Count > 0 Then  ' THR		    
			dgTHR.DataSource = objDataSet.Tables(3)
			dgTHR.DataBind()
			BtnPreviewdgTHR.Visible = True
		End If
		
		If objDataSet.Tables(4).Rows.Count > 0 Then  ' TRANSIT	    
			dgTransit.DataSource = objDataSet.Tables(4)
			dgTransit.DataBind()
			BtnPreviewTransit.Visible = True
		End If
		
		If objDataSet.Tables(5).Rows.Count > 0 Then  ' ALOKASI	    
			dgALOKASI.DataSource = objDataSet.Tables(5)
			dgALOKASI.DataBind()
			BtnPreviewAlokasi.Visible = True
		End If

		If objDataSet.Tables(5).Rows.Count > 0 Then  ' RECON	    
			dgRecon.DataSource = objDataSet.Tables(6)
			dgRecon.DataBind()
			BtnPreviewAlokasi.Visible = True
		End If
		
		'PPH21
		ViewData()
        ViewDataNonKrywn()
        ViewDataThn()
        ViewDataNonKrywnThn()
					
	End Sub
	
		
	Sub BtnPreviewdgAMPRAH_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=AMPRAH-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgAMPRAH.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	
	Sub BtnPreviewdgRAPEL_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=RAPEL-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgRAPEL.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	
	Sub BtnPreviewdgBONUS_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=BONUS-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgBONUS.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	
	Sub BtnPreviewdgTHR_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=THR-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgTHR.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	
	Sub BtnPreviewTransit_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=TRANSIT-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgTransit.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	
	Sub BtnPreviewAlokasi_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=ALOKASI-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgAlokasi.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	
	Sub BtnPreviewRecon_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=RECON-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgRecon.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

	Sub BtnPreviewdgKRYWN21_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=SPTKRY-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgKRYWN21.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	
	Sub BtnPreviewNONKRYWN21_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=SPTNONKRY-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgNONKRYWN21.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	'
	
	Sub BtnPreviewThn21_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=SPTTHNKRY-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgKRYWNThn21.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	
	Sub BtnPreviewNONKRYWNThn21_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=SPTTHNNONKRY-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgNONKRYWNThn21.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	
	Sub ddlMonth_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) 
		IF GetStatus(cint(ddlMonth.SelectedItem.Value.Trim),ddlyear.SelectedItem.Value.Trim,strLocation,"1","","","","","Payroll") = "3" Then
			btnPosted.Visible = False
			btngenerate.Visible = False
			btnConfirm.visible = False
		ELSE
			btnPosted.Visible = True
			btngenerate.Visible = True
			btnConfirm.visible = True
		END IF	
	End Sub
		
End Class
