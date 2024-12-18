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

Public Class PR_mthend_gajibesarprocess : Inherits Page

    Protected WithEvents RdoMonthProcess As RadioButton
    Protected WithEvents RdoDayProcess As RadioButton
    Protected WithEvents rbAllEmp As RadioButton
    Protected WithEvents rbSelectedEmp As RadioButton
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents btnGenerate As ImageButton

    Protected WithEvents btnDateFrom As Image
    Protected WithEvents btnDateTo As Image

    Protected WithEvents lblNote As Label
    Protected WithEvents lblCompleteSetup As Label
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label

    Protected WithEvents dgprocess As DataGrid
	Protected WithEvents dgnotmatch As DataGrid
	Protected WithEvents dgmsg As DataGrid

    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox

    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox

    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlmandor As DropDownList

    Protected WithEvents chkDelDaily As CheckBox
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

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        intPRAR = Session("SS_PRAR")

        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrMessage.Text = ""
			btnGenerate.Attributes("onclick") = "javascript:return confirm('Pastikan anda sudah melakukan Proses HK sebelumnya ');"
			btnGenerate.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnGenerate).ToString())
			
            If Not Page.IsPostBack Then
				ddlMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))
                BindEmpDiv()
				BindEmployee()
                BindMandoran("")
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

    Sub BindEmployee()
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
		Dim Strdate2 AS String 
		Dim strsrc As String
		Dim strAM as String
		
		If ddlMonth.SelectedItem.Value < 10 Then
            Strdate2 = ddlyear.SelectedItem.Value & "0" & ddlMonth.SelectedItem.Value
            strAM    = "0" & ddlMonth.SelectedItem.Value 			
        Else
            Strdate2 = ddlyear.SelectedItem.Value & ddlMonth.SelectedItem.Value 
			strAM    = ddlMonth.SelectedItem.Value 
        End If
		
		strsrc = " AND  A.EmpCode Not in (Select empcode from HR_TRX_EMPRESIGN where status='1' and " & Strdate2 & " > convert(char(6),efektifdate,112)) "

        strParamName =  "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & strAM & "|" & ddlyear.SelectedItem.Value.trim() & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "'" & strsrc & "|ORDER BY A.EmpName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description"))
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = ""
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmployee.DataSource = objEmpCodeDs.Tables(0)
        ddlEmployee.DataTextField = "_Description"
        ddlEmployee.DataValueField = "EmpCode"
        ddlEmployee.DataBind()
        ddlEmployee.SelectedIndex = intSelectedIndex

    End Sub

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

    Sub Check_Clicked(ByVal Sender As Object, ByVal E As EventArgs)
        If rbSelectedEmp.Checked Then
            ddlEmployee.Enabled = True
        Else
            ddlEmployee.Enabled = False
        End If
    End Sub

    Sub Check_Gaji_Clicked(ByVal Sender As Object, ByVal E As EventArgs)
        If RdoMonthProcess.Checked Then
            RdoMonthProcess.Checked = True
            RdoDayProcess.Checked = False
            txtDateFrom.Visible = False
            txtDateTo.Visible = False
            btnDateFrom.Visible = False
            btnDateTo.Visible = False
            lblNote.Visible = False
            chkDelDaily.Enabled = True
        Else
            RdoMonthProcess.Checked = False
            RdoDayProcess.Checked = True
            txtDateFrom.Visible = True
            txtDateTo.Visible = True
            btnDateFrom.Visible = True
            btnDateTo.Visible = True
            lblNote.Visible = True
            chkDelDaily.Checked = False
            chkDelDaily.Enabled = False

        End If
    End Sub

    Sub Check_Del_DailyProcess(ByVal Sender As Object, ByVal E As EventArgs)
       
    End Sub

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

    Sub BindEmpDiv()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim objEmpDivDs As New Object

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpDiv.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDiv.DataTextField = "Description"
        ddlEmpDiv.DataValueField = "BlkGrpCode"
        ddlEmpDiv.DataBind()
        ddlEmpDiv.SelectedIndex = 0

    End Sub

    Sub BindMandoran(ByVal strdivcode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
		Dim strAM as String
		If ddlMonth.SelectedItem.Value < 10 Then
            strAM    = "0" & ddlMonth.SelectedItem.Value 			
        Else
        	strAM    = ddlMonth.SelectedItem.Value 
        End If

        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & strAM & "|" & ddlyear.SelectedItem.Value.trim()  & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strdivcode & "%' AND (ismandor='1')|ORDER BY A.EmpName"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description"))
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "All"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlmandor.DataSource = objEmpCodeDs.Tables(0)
        ddlmandor.DataTextField = "_Description"
        ddlmandor.DataValueField = "EmpCode"
        ddlmandor.DataBind()
    End Sub
	
	Function cekbasis(ByVal mn As String,ByVal yr As String) As Integer
        Dim strOpCd As String = "PR_PR_STP_BLOK_BJR_COMPARE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objCodeDs As New Object()

        strParamName = "LOC|MN|YR"
        strParamValue = strLocation & "|" & mn & "|" & yr
                       
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_BJR_COMPARE&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

		If trim(objCodeDs.Tables(0).Rows(0).Item("ket")) <> "" Then
			dgmsg.DataSource = Nothing
			dgmsg.DataBind()
			dgmsg.DataSource = objCodeDs.Tables(0)
			dgmsg.DataBind()
			ShowHideDiv("divmsg")
		end if
		
		Return Trim(objCodeDs.Tables(1).Rows(0).Item("Msg"))
		
    End Function
	
	Function cekhk(ByVal mn1 As String,ByVal mn As String,ByVal yr As String) As Integer
        Dim strOpCd As String = "PR_PR_MTHEND_HK_CEK"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objCodeDs As New Object()

        strParamName = "LOC|ACCMONTH|AM|AY"
        strParamValue = strLocation & "|" & mn1 & "|" & mn & "|" & yr
                       
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_HK_CEK&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

		If trim(objCodeDs.Tables(0).Rows(0).Item("ket")) <> "" Then
			dgmsg.DataSource = Nothing
			dgmsg.DataBind()
			dgmsg.DataSource = objCodeDs.Tables(0)
			dgmsg.DataBind()
			ShowHideDiv("divmsg")
		end if
		
		Return Trim(objCodeDs.Tables(1).Rows(0).Item("Msg"))
		
    End Function

    Function CheckDate(ByVal pv_strDate As String, ByRef pr_strDate As String) As Boolean
        Dim strDateFormatCode As String = Session("SS_DATEFMT")
        Dim strDateFormat As String

        pr_strDate = ""
        CheckDate = True
        If Not pv_strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormatCode, pv_strDate, strDateFormat, pr_strDate) = False Then
                lblFmt.Text = strDateFormat
                pr_strDate = ""
                CheckDate = False
            End If
        End If
    End Function

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdSP As String = "PR_PR_MTHEND_PAYROLL_PROCESS_SP"
        Dim strOpCdSP_DAILY As String = "PR_PR_MTHEND_PAYROLL_DAILY_PROCESS_SP"
        Dim strOpCdSmall_Pay As String = "PR_PR_MTHEND_PAYROLL_PROCESS_STATUS_SP"

        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
		Dim strMn1 As String
        Dim strYr As String
        Dim strEc As String = ""
        Dim indDate As String = ""
        Dim StrDelGj_Daily As String = ""
        Dim strStatusPay As String = ""
        Dim strDateFrom As String = Date_Validation(txtDateFrom.Text, False)
        Dim strDateTo As String = Date_Validation(txtDateTo.Text, False)



        If RdoDayProcess.Checked = True Then
            If CheckDate(txtDateFrom.Text.Trim(), indDate) = False Or CheckDate(txtDateTo.Text.Trim(), indDate) = False Then
                lblDate.Visible = True
                lblFmt.Visible = True
                lblDate.Text = "<br>Date Entered should be in the format"
                Exit Sub
            Else
                lblDate.Visible = False
                lblFmt.Visible = False

                If (Month(strDateFrom) <> ddlMonth.SelectedItem.Value) And (Month(strDateTo) <> ddlMonth.SelectedItem.Value) Then
                    lblDate.Visible = True
                    lblFmt.Visible = True
                    lblDate.Text = "<br>Date Entered should be in the format"
                    Exit Sub
                End If

                If (Year(strDateFrom) <> ddlMonth.SelectedItem.Value) And (Year(strDateTo) <> ddlyear.SelectedItem.Value) Then
                    lblDate.Visible = True
                    lblFmt.Visible = True
                    lblDate.Text = "<br>Date Entered should be in the format"
                    Exit Sub
                End If
            End If
        End If

        If StatusPayroll(CInt(ddlMonth.SelectedItem.Value.Trim), ddlyear.SelectedItem.Value.Trim, strLocation) = "3" Then
            Exit Sub
        Else
            UpdateStatusPayroll(CInt(ddlMonth.SelectedItem.Value.Trim), ddlyear.SelectedItem.Value.Trim, strLocation)
        End If

        If ddlMonth.SelectedItem.Value < 10 Then
            strMn = "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strMn = ddlMonth.SelectedItem.Value.Trim
        End If
        strMn1 = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        If cekbasis(RTrim(strMn), RTrim(strYr)) = 0 Then
            lblErrMessage.Text = "HR-Setup-Tabel Basis & Premi Panen"
            UserMsgBox(Me, lblErrMessage.Text)
            Exit Sub
        End If
		
		If cekhk(RTrim(strMn),RTrim(strMn1)  ,RTrim(strYr)) = 0 Then
            lblErrMessage.Text = "Cek HK dan BKM dibawah ini"
            UserMsgBox(Me, lblErrMessage.Text)
            Exit Sub
        End If

        If ddlEmployee.Enabled Then
            If ddlEmployee.Text = "" Then
                lblErrMessage.Text = "Silakan pilih Employee code"
                lblErrMessage.Visible = True
                Exit Sub
            End If
            strEc = ddlEmployee.SelectedItem.Value.Trim
        Else
            strEc = ""
        End If

        If GetCutOff(strYr) = 0 Then
            lblErrMessage.Text = "Silakan isi setup cut off"
            lblErrMessage.Visible = True
            Exit Sub
        End If


        ParamName = "AM|AY|LOC|UI"
        ParamValue = CInt(strMn) & "|" & strYr & "|" & _
               strLocation & "|" & strUserId
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSmall_Pay, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_GET&errmesg=" & Exp.Message)
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            strStatusPay = Trim(objDataSet.Tables(0).Rows(0).Item("StatusGaji"))

            If strStatusPay = "3" Then
                lblErrMessage.Text = "Gaji Besar Sudah DiConfirm"
                UserMsgBox(Me, lblErrMessage.Text)
                Exit Sub
            End If
        End If

        If RdoMonthProcess.Checked = True Then
            If chkDelDaily.Checked = True Then
                StrDelGj_Daily = "1"
            Else
                StrDelGj_Daily = "0"
            End If

            ParamName = "HEADER|ACCMONTH|ACCYEAR|LOC|UI|EC|DG"
            ParamValue = "PAY/" & strLocation & "/|" & strMn & "|" & strYr & "|" & strLocation & "|" & strUserId & "|" & strEc & "|" & StrDelGj_Daily

            Try
                intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_PAYROLL_PROCESS_SP&errmesg=" & Exp.Message & "&redirect=")
            End Try

        ElseIf RdoDayProcess.Checked = True Then
            ParamName = "HEADER|ACCMONTH|ACCYEAR|DATEFR|DATETO|LOC|UI|EC"
            ParamValue = "PAY/" & strLocation & "/|" & strMn & "|" & strYr & "|" & strDateFrom & "|" & strDateTo & "|" & strLocation & "|" & strUserId & "|" & strEc

            Try
                intErrNo = objOk.mtdGetDataCommon(strOpCdSP_DAILY, ParamName, ParamValue, objDataSet)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_PAYROLL_DAILY_PROCESS_SP&errmesg=" & Exp.Message & "&redirect=")
            End Try
        End If

        If objDataSet.Tables(1).Rows.Count = 0 Then
            lblErrMessage.Text = "No Record Created"
        ElseIf objDataSet.Tables(1).Rows.Count > 0 Then
            lblErrMessage.Text = "Process Success"
        Else
            lblErrMessage.Text = "Process Failed"
        End If

        UserMsgBox(Me, lblErrMessage.Text)

        If objDataSet.Tables(1).Rows.Count > 0 Then

            If objDataSet.Tables(2).Rows.Count > 0 Then
                dgnotmatch.DataSource = Nothing
                dgnotmatch.DataBind()
                dgnotmatch.DataSource = objDataSet.Tables(2)
                dgnotmatch.DataBind()
                ShowHideDiv("divnotmatch")
            End If

            ShowHideDiv("divprocess")
            txtEmpCode.Text = strEc
            HitView()
        End If


    End Sub

    Sub SearchBtn_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        HitView()
    End Sub

    Sub PrintBtn_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        HitPrint()
    End Sub

    Sub HitView()
        Dim strOpCdSP As String = "PR_PR_STDRPT_GAJIBESAR_REPORT"
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

        ParamName = "ACCMONTH|ACCYEAR|PHYMONTH|PHYYEAR|COMPCODE|LOCCODE|DATEFROM|DATETO|DECIMAL|F_EMPCODE|F_DIVCODE|F_MCODE|F_EMPTYPE"

        ParamValue = strMn & "|" & _
                        strYr & "|" & _
                        strMn & "|" & _
                        strYr & "|" & _
                        strCompany & "|" & _
                        strLocation & "|" & _
                        "" & "|" & _
                        "" & "|2|" & _
                        txtEmpCode.Text.Trim & "|" & _
                        ddlEmpDiv.SelectedItem.Value.Trim & "|" & _
                        ddlmandor.SelectedItem.Value.Trim & "|OTH"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_RICERATION_PROCESS_SP&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgprocess.DataSource = Nothing
        dgprocess.DataBind()
        dgprocess.DataSource = objDataSet.Tables(0)
        dgprocess.DataBind()

    End Sub

    Sub HitPrint()
        Dim strMn As String
        Dim strYr As String
        Dim strRptID As String = "RPTPR1000057"
        Dim strRptName As String = "Gaji Besar"

        If ddlMonth.SelectedItem.Value < 10 Then
            strMn = "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strMn = ddlMonth.SelectedItem.Value.Trim
        End If

        strYr = ddlyear.SelectedItem.Value.Trim

        Response.Write("<Script Language=""JavaScript"">window.open(""../../reports/PR_StdRpt_GajiBesarPreview_Estate.aspx?CompCode=" & strCompany & "&Location=" & strLocation & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=2" & _
                       "&lblLocation=" & strLocation & "&AccMonth=" & strMn & "&AccYear=" & strYr & _
                       "&EmpCode=" & txtEmpCode.Text.Trim & "&Divisi=" & ddlEmpDiv.SelectedItem.Value.Trim & "&Mandor=" & ddlmandor.SelectedItem.Value.Trim & """,""" & strRptID & """ ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub
	
	Sub ddlEmpDiv_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindEmployee()
    End Sub
	
	Function StatusPayroll(Byval mn as String,Byval yr as String,Byval loc as String)as Integer
        Dim strOpCdSP As String = "PR_PR_TRX_MTHEND_GET_STATUS"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
		Dim i as Integer
      
       
        ParamName = "MN|YR|LOC"
        ParamValue = mn & "|" & _
                     yr & "|" & _
                     loc
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_GET_STATUS&errmesg=" & Exp.Message & "&redirect=")
        End Try
	
		If objDataSet.Tables(0).Rows.Count > 0 Then
        		i = objDataSet.Tables(0).Rows(0).Item("Status")
				IF i = 3 Then
					UserMsgBox(Me, "Proses ditutup, Periode "& mn & "/" & yr & " Sudah Confirm")
				End If
		Else
		        i = 0 
		end if
		
       Return i

    End Function
	
    Sub UpdateStatusPayroll(Byval mn as String,Byval yr as String,Byval loc as String)
        Dim strOpCdSP As String = "PR_PR_TRX_MTHEND_UPD"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
      
       
        ParamName = "MN|YR|LOC|S1|S2|S3|S4|S5"
        ParamValue = mn & "|" & _
                     yr & "|" & _
                     loc & "|1||||"
        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCdSP, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_UPD&errmesg=" & Exp.Message & "&redirect=")
        End Try

    End Sub
	
	
End Class
