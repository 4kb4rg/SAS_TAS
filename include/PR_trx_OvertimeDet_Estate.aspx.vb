Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Public Class PR_trx_OvertimeDet_Estate : Inherits Page

#Region "declaration"

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgOvtDet As DataGrid

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents lbldivisi As Label
    Protected WithEvents lblempcode As Label

    Protected WithEvents LblTarifLembur As Label

    Protected WithEvents LblidM As Label
    Protected WithEvents LblidD As Label

    Protected WithEvents txtWPDate As TextBox
    Protected WithEvents ddlsubcat As DropDownList
    Protected WithEvents ddljob As DropDownList
    Protected WithEvents ddlttnm As DropDownList

    Protected WithEvents txtStartTm As TextBox
    Protected WithEvents txtEndTm As TextBox
    Protected WithEvents TxtQty As TextBox

    Protected WithEvents lbl_psn1 As Label
    Protected WithEvents lbl_psn2 As Label
    Protected WithEvents lbl_psn3 As Label
    Protected WithEvents lbl_psn4 As Label

    Protected WithEvents Txt150 As TextBox
    Protected WithEvents Txt200 As TextBox
    Protected WithEvents Txt300 As TextBox
    Protected WithEvents Txt400 As TextBox


    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlEmpCode As DropDownList

    Protected WithEvents lblkoreksi As Label
    Protected WithEvents txtkoreksi As TextBox
    Protected WithEvents TxtDeskripsi As TextBox

    Protected WithEvents NewBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DeleteBtn As ImageButton

    Protected WithEvents isNew As HtmlInputHidden
	Protected WithEvents isEdit As HtmlInputHidden
	Protected WithEvents otlnid As HtmlInputHidden
	
    Protected WithEvents thrs As HtmlInputHidden
    Protected WithEvents tpsn1 As HtmlInputHidden
    Protected WithEvents tpsn2 As HtmlInputHidden
    Protected WithEvents tpsn3 As HtmlInputHidden
    Protected WithEvents tpsn4 As HtmlInputHidden
    Protected WithEvents ovrrate As HtmlInputHidden
	Protected WithEvents codeempty As HtmlInputHidden
	Protected WithEvents hidstatus As HtmlInputHidden
	
	Protected WithEvents VerBtn As ImageButton
    Protected WithEvents ConfBtn As ImageButton
	Protected WithEvents ReActBt As Button

	Protected WithEvents ddlstp As DropDownList
	
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
    Dim objEmpDivDs As New Object()


    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFmt As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strAcceptFormat As String
    Dim intLevel As Integer

    Dim strSelectedPayId As String = ""
    Dim intStatus As Integer

    Dim strOTCode As String = ""
    Dim StrOTLCode As String = ""
    Dim strEmpCode As String = ""
    Dim strEmpDivisi As String = ""
    Dim StrTotHours As String = "0"
	
	Dim CntTotal As Single 
	Dim CntTotal1 As Single
	Dim CntTotal2 As Single
	Dim CntTotal3 As Single
	Dim CntTotal4 As Single
	Dim CntTotalAmt As Single

#End Region

#Region "Function & Procedure"

    Function IsTime(ByVal str As String)
        Dim r As Regex = New Regex("^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$")
        Dim m As Match = r.Match(str)
        If (m.Success) Then
            Return True
        End If
        Return False
    End Function

    Function toNumber(ByVal s As String) As String
        If (s = "" Or s = "NULL") Then
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber("0", 2), 2)
        Else
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(s, 2), 2)
        End If

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

        tcode = "OVR/" & strLocation & "/" & Mid(Trim(txtWPDate.Text), 4, 2) & Right(Trim(txtWPDate.Text), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|8|" & tcode & "|5"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Function getCodeln() As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        tcode = "OVRLN/" & strLocation & "/" & Mid(Trim(txtWPDate.Text), 4, 2) & Right(Trim(txtWPDate.Text), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|9|" & tcode & "|5"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Function GetAttDate(ByVal strEmpCode As String, ByVal strAttDate As String) As String
        Dim strOpCd_EmpAtt As String = "PR_PR_TRX_ATTENDANCE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpAttDs As New Object()

        strParamName = "SEARCH"
        strParamValue = "AND EmpCode='" & strEmpCode & "' AND AttDate='" & strAttDate & "' AND LocCode='" & strLocation & "' AND AttCode in ('K','P3','P4','HM','HB') "

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpAtt, strParamName, strParamValue, objEmpAttDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpAttDs.Tables(0).Rows.Count = 1 Then
            Return (Trim(objEmpAttDs.Tables(0).Rows(0).Item("Hk")))
        Else
            Return "0"
        End If
    End Function

    Sub clearAll()

        txtStartTm.Text = ""
        txtEndTm.Text = ""
        TxtQty.Text = ""
        Txt150.Text = ""
        Txt200.Text = ""
        Txt300.Text = ""
        Txt400.Text = ""

        lbl_psn1.Text = ""
        lbl_psn2.Text = ""
        lbl_psn3.Text = ""
        lbl_psn4.Text = ""

        TxtDeskripsi.Text = ""
        'LblTarifLembur.Text = toNumber("0")
        StrTotHours = toNumber("0")
    End Sub

#End Region

#Region "Page Load"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
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
            lblErrMessage.Visible = False
            DeleteBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            strOTCode = Trim(IIf(Request.QueryString("OtID") <> "", Request.QueryString("OtID"), Request.Form("OtID")))

            If Not IsPostBack Then
                txtWPDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                txtStartTm.Text = FormatDateTime(Now(), DateFormat.ShortTime)
                txtEndTm.Text = FormatDateTime(Now(), DateFormat.ShortTime)

                If intLevel = 0 Then
                    lblkoreksi.Visible = False
                    txtkoreksi.Visible = False
                Else
                    lblkoreksi.Visible = True
                    txtkoreksi.Visible = True
                End If

				BindStp("")
                If strOTCode <> "" Then
                    isNew.Value = "False"
                    isEdit.Value = "False"					
                    LblidM.Text = strOTCode
                    LblidD.Text = getCodeln()
					hidstatus.Value = "3"
                    onLoad_Display()
                    BindBKSubKategory()
                Else
                    isNew.Value = "True"
					isEdit.Value = "False"
                    LblidM.Text = getCode()
                    LblidD.Text = getCodeln()
                    BindDivision("")
                    BindBKSubKategory()
					
                End If
                onLoad_button()
            End If

            If LblidM.Text <> "" Then
                BindEmpOvertimeLn(LblidM.Text)
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
	
    

#End Region

#Region "Binding"

    Sub Bind_Pekerjaan(ByVal idscat As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_JOB_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE SubCatID='" & idscat & "' AND Status='1' AND LocCode='" & strLocation & "'|Order by Description"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=idscat&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("JobCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("JobCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("description"))
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("JobCode") = " "
        dr("description") = "Pilih Pekerjaan"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddljob.DataSource = objEmpCodeDs.Tables(0)
        ddljob.DataTextField = "description"
        ddljob.DataValueField = "JobCode"
        ddljob.DataBind()
        ddljob.SelectedIndex = 0

    End Sub

    Sub BindDivision(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.BlkGrpCode Like '%" & strDivCode & "%' AND A.LocCode = '" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = " "
        dr("Description") = "Please Select Employee Division"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpDiv.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDiv.DataTextField = "Description"
        ddlEmpDiv.DataValueField = "BlkGrpCode"
        ddlEmpDiv.DataBind()
        ddlEmpDiv.SelectedIndex = 0

    End Sub
	
	Sub BindStp(ByVal ty As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_OVERTIME_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim intCntSelect As Integer = 0
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.Status ='1' AND A.LocCode = '" & strLocation & "'|ORDER By OTCode "

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("OTCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("OTCode"))
				if Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("TyHari")) = ty
					intCntSelect = intCnt
				end if
            Next
        End If

        ddlstp.DataSource = objEmpDivDs.Tables(0)
        ddlstp.DataTextField = "Ket"
        ddlstp.DataValueField = "OTCode"
        ddlstp.DataBind()
		ddlstp.SelectedIndex = intCntSelect
    End Sub
	
	Private Function Cek_tyDate(ByVal dt As String) As String
		Dim TS As String = Date_Validation(dt, False)
		Dim strOpCd_EmpDiv As String = "PR_PR_STP_HOLIDAY_GET"
		Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
		
		strParamName = "SEARCH|SORT"
        strParamValue = "AND A.HolidayDate='" & TS & "'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIMELN_CEK&errmesg=" & Exp.Message & "&redirect=")
        End Try

		if objEmpCodeDs.Tables(0).Rows.Count > 0 Then
		    Return Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TyDate"))
		else 
			Return "R"
		end if
		
	End Function

    Sub BindEmployee(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
		Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)


        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & SM & "|" & SY & "|AND C.IDDiv Like '%" & strDivCode & "%' AND A.LocCode = '" & strLocation & "' AND A.Status='1' AND C.CodeempTy <> 'PHL'|ORDER BY A.EmpName"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description"))
                If objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(strEmpCode) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "Please Select Employee Code"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpCode.DataSource = objEmpCodeDs.Tables(0)
        ddlEmpCode.DataTextField = "_Description"
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intSelectedIndex

    End Sub

    Sub BindEmpPayrol(ByVal str_EmpCode As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_OVRTIME_GET_EMPPAYROL"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
		Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)

        strParamName = "ID|AM|AY|LOC"
        strParamValue = str_EmpCode & "|" & SM & "|" & SY & "|" & strlocation

        If str_EmpCode = "" Then
            Exit Sub
        End If

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIME_GET_EMPPAYROL&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try


        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then

            LblTarifLembur.Text = toNumber(objEmpCodeDs.Tables(0).Rows(0).Item("OvrTmRate").ToString)
            ovrrate.Value = objEmpCodeDs.Tables(0).Rows(0).Item("OvrTmRate")
			codeempty.Value = objEmpCodeDs.Tables(0).Rows(0).Item("CodeempTy")
        End If

    End Sub

    Sub BindEmpOvertimeLn(ByVal strOTID As String)
        Dim strOpCd_Get As String = "PR_PR_TRX_OVRTIMELN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objOTLnDs As New Object()

        strSearch = "AND IDOt='" & strOTID & "'"

        strParamName = "SEARCH|SORT"
        strParamValue = strSearch & "|ORDER BY OtDate"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objOTLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIMELN_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

		CntTotal = 0
		CntTotal1 = 0
		CntTotal2 = 0
		CntTotal3 = 0
		CntTotal4 = 0
		CntTotalAmt = 0
	
        For intCnt = 0 To objOTLnDs.Tables(0).Rows.Count - 1
            objOTLnDs.Tables(0).Rows(intCnt).Item("OtLnID") = Trim(objOTLnDs.Tables(0).Rows(intCnt).Item("OtLnID"))
            objOTLnDs.Tables(0).Rows(intCnt).Item("OtDate") = objOTLnDs.Tables(0).Rows(intCnt).Item("OtDate")
            objOTLnDs.Tables(0).Rows(intCnt).Item("TimeOT") = Format(objOTLnDs.Tables(0).Rows(intCnt).Item("StartTm"), "HH:mm") & "-" & Format(objOTLnDs.Tables(0).Rows(intCnt).Item("EndTm"), "HH:mm")
				Select Case Trim(objOTLnDs.Tables(0).Rows(intCnt).Item("Status"))
				Case "1" 
					objOTLnDs.Tables(0).Rows(intCnt).Item("StatApv") = "Confirm"
				Case "2" 
					objOTLnDs.Tables(0).Rows(intCnt).Item("StatApv") = "Verified"
				Case "3" 
					objOTLnDs.Tables(0).Rows(intCnt).Item("StatApv") = "Active"
				Case "4"
					objOTLnDs.Tables(0).Rows(intCnt).Item("StatApv") = "Delete"
				End Select  
            'IF Trim(objOTLnDs.Tables(0).Rows(intCnt).Item("Status")) = "1" Then
            CntTotal = CntTotal + objOTLnDs.Tables(0).Rows(intCnt).Item("Hours")
            CntTotal1 = CntTotal1 + objOTLnDs.Tables(0).Rows(intCnt).Item("Ovr1")
            CntTotal2 = CntTotal2 + objOTLnDs.Tables(0).Rows(intCnt).Item("Ovr2")
            CntTotal3 = CntTotal3 + objOTLnDs.Tables(0).Rows(intCnt).Item("Ovr3")
            CntTotal4 = CntTotal4 + objOTLnDs.Tables(0).Rows(intCnt).Item("Ovr4")
            CntTotalAmt = CntTotalAmt + objOTLnDs.Tables(0).Rows(intCnt).Item("Amount")
            'End If
        Next

        dgOvtDet.DataSource = objOTLnDs
        dgOvtDet.DataBind()

    End Sub

    Sub BindBKSubKategory()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_SUBCATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBCATEGORY_GET_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatName") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatName")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("SubCatID") = ""
        dr("SubCatName") = "Pilih Sub Kategori"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlsubcat.DataSource = objEmpDivDs.Tables(0)
        ddlsubcat.DataTextField = "SubCatName"
        ddlsubcat.DataValueField = "SubCatID"
        ddlsubcat.DataBind()

    End Sub

    Sub Bind_Blok(ByVal sdc As String, ByVal sjc As String, ByVal ssc As String, ByVal sku As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BLOK_BKM_GETTT"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow
		Dim intselect As Integer = 0

        strParamName = "LOC|SC|JC"
        strParamValue = strLocation & "|" & ssc & "|" & sjc & "|" & sku

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_BKM_GETTT&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode"))
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("blok") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("blok"))
            Next
        End If

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("BlokCode") = ""
        dr("blok") = "Pilih Blok"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlttnm.DataSource = objEmpBlkDs.Tables(0)
        ddlttnm.DataTextField = "blok"
        ddlttnm.DataValueField = "BlokCode"
        ddlttnm.DataBind()
	End Sub

#End Region

#Region "Event"

    Sub onload_button()
	        NewBtn.visible = False
			ConfBtn.visible = False
			VerBtn.visible = False
			SaveBtn.visible = False
			ReActBt.visible = False
		
        If isNew.Value = "False" Then
            ddlEmpDiv.Visible = False
            ddlEmpCode.Visible = False
            lbldivisi.Visible = True
            lblempcode.Visible = True
			Select Case hidstatus.Value.Trim()
				Case "3" 'Active
					Select Case intlevel 
						Case 0 'User
						NewBtn.visible = True
						ConfBtn.visible = False
						VerBtn.visible = False
						SaveBtn.visible = True
						ReActBt.visible = False
						Case 1 'Supervisor/Asistent
						ConfBtn.visible = False
						VerBtn.visible = True
						SaveBtn.visible = True
						ReActBt.visible = False
						Case >=2 'Manager/KTU
						ConfBtn.visible = False
						VerBtn.visible = False
						SaveBtn.visible = False
						ReActBt.visible = False
					End Select
				Case "2" 'Verified
					Select Case intlevel 
                        Case 0 'User
                            ConfBtn.Visible = False
                            VerBtn.Visible = False
                            SaveBtn.Visible = False
                            ReActBt.Visible = False
						Case 1 'Supervisor/Asistent
                            ConfBtn.Visible = False
                            VerBtn.Visible = False
                            SaveBtn.Visible = False
                            ReActBt.Visible = True
						Case >=2 'Manager/KTU
                            ConfBtn.Visible = True
                            VerBtn.Visible = False
                            SaveBtn.Visible = True
                            ReActBt.Visible = True
					End Select
				Case "1" 'Confirm
					Select Case intlevel 
						Case <=1 'User
						ConfBtn.visible = False
						VerBtn.visible = False
						SaveBtn.visible = False
						ReActBt.visible = False
						Case 1 'Supervisor/Asistent
						ConfBtn.visible = False
						VerBtn.visible = False
						SaveBtn.visible = False
						ReActBt.visible = False
						Case >=2 'Manager/KTU
						ConfBtn.visible = False
						VerBtn.visible = False
						SaveBtn.visible = False
						ReActBt.visible = True
					End Select	
			End Select
        Else
            ddlEmpDiv.Visible = True
            ddlEmpCode.Visible = True
            lbldivisi.Visible = False
            lblempcode.Visible = False
			Select Case intlevel 
						Case <=1 'User
						NewBtn.visible = True
						ConfBtn.visible = False
						VerBtn.visible = False
						SaveBtn.visible = True
						ReActBt.visible = False
						Case 2 'Supervisor
						ConfBtn.visible = False
						VerBtn.visible = False
						SaveBtn.visible = False
						ReActBt.visible = False
						Case >=3 'Manager
						ConfBtn.visible = False
						VerBtn.visible = False
						SaveBtn.visible = False
						ReActBt.visible = False
			End Select	
				
        End If

    End Sub

    Sub getselectjob(ByVal jb As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_JOB_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim strscat As String

        strParamName = "SEARCH|SORT"
        strParamValue = "AND jobcode Like '%" & jb & "%' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        strscat = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("subcatid"))
        ddlsubcat.SelectedValue = strscat
        Bind_Pekerjaan(strscat)
        ddljob.SelectedValue = Trim(jb)
        Bind_Blok("", Trim(jb), strscat, "")

    End Sub

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_OVRTIME_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "LOC|SEARCH"
        strParamValue = strLocation & "|AND OtID Like '%" & strOTCode & "%' AND LocCode='" & strLocation & "'"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIME_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            strEmpCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpCode"))
            strEmpDivisi = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDDiv"))
            lbldivisi.Text = strEmpDivisi & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Description")) & ")"
            lblempcode.Text = strEmpCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpName")) & ")"
            ovrrate.Value = objEmpCodeDs.Tables(0).Rows(0).Item("OvrRate")
            thrs.Value = objEmpCodeDs.Tables(0).Rows(0).Item("TotalHours")
            LblTarifLembur.Text = toNumber(ovrrate.Value)
			codeempty.Value = objEmpCodeDs.Tables(0).Rows(0).Item("EmpType")

			if ovrrate.Value = 0 then
			 BindEmpPayrol(strEmpCode)
			end if
		End If
    End Sub

    Sub ddlEmpDiv_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpDivisi = ddlEmpDiv.SelectedItem.Value.Trim()
        BindEmployee(strEmpDivisi)
    End Sub

    Sub ddlEmpCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpCode = ddlEmpCode.SelectedItem.Value.Trim()
        clearAll()
        BindEmpPayrol(strEmpCode)
        'cek sdh ada blm kalo ada load display.. 
    End Sub

    Sub ddlsubcat_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strscat As String = ddlsubcat.SelectedItem.Value.Trim()
        Bind_Pekerjaan(strscat)
    End Sub

    Sub ddljob_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strjob As String = ddljob.SelectedItem.Value.Trim()
        Dim strscat As String = ddlsubcat.SelectedItem.Value.Trim()
        Bind_Blok("", strjob, strscat, "")
		
		BindStp(Cek_tyDate(trim(txtWPDate.text)))
		 
    End Sub

    Sub getitem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)

        LblidD.Text = ""
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbl As Label
            Dim hid As HiddenField
            Dim lst As DropDownList

			isEdit.Value = "True"
            lbl = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblID")
            LblidD.Text = lbl.Text
			otlnid.Value = LblidD.Text.Trim()
			
            lbl = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbldate")
            txtWPDate.Text = lbl.Text
            hid = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidjob")
            getselectjob(hid.Value.Trim)
            
			
			lbl = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblblk")
			ddlttnm.SelectedValue = Trim(lbl.Text)
			
			lbl = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblstartTm")
			txtStartTm.Text = lbl.Text
            lbl = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblendTm")
            txtEndTm.Text = lbl.Text
            lbl = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblQty")
            TxtQty.Text = lbl.Text

            hid = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidovr1")
            Txt150.Text = hid.Value
            hid = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidpsn1")
            lbl_psn1.Text = hid.Value


            hid = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidovr2")
            Txt200.Text = hid.Value
            hid = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidpsn2")
            lbl_psn2.Text = hid.Value

            hid = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidovr3")
            Txt300.Text = hid.Value
            hid = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidpsn3")
            lbl_psn3.Text = hid.Value

            hid = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidovr4")
            Txt400.Text = hid.Value
            hid = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidpsn4")
            lbl_psn4.Text = hid.Value

            lbl = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDesc")
            TxtDeskripsi.Text = lbl.Text

			lbl = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblOC")
			ddlstp.selectedValue = lbl.Text.Trim()
			
			lbl = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblstat2")
			hidstatus.value = lbl.Text.Trim()
			
			lbl = dgOvtDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblstat")
			lblStatus.Text = lbl.Text.Trim()
			
            ddlEmpDiv.Enabled = False
            ddlEmpCode.Enabled = False
			onload_button()
        End If
    End Sub

   	Private Function Cek_ALokasi(ByVal jb As String) As Boolean
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_OVRTIMELN_CEK"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()

		Dim PD As String = Right(Trim(txtWPDate.Text), 4) & Mid(Trim(txtWPDate.Text), 4, 2)
        
        strParamName = "JC|LOC|PD"
        strParamValue = jb & "|" & strLocation & "|" & PD 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIMELN_CEK&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return Trim(objEmpCodeDs.Tables(0).Rows(0).Item("status"))


    End Function
	
	Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
	 
		  Response.Redirect("PR_trx_OvertimeList_Estate.aspx")
	End Sub
	
	Sub CalRPLembur(ByVal ID As String)
		Dim strOpCd_Cl As String = "PR_PR_TRX_OVRTIME_CALC"
		Dim intErrNo As Integer
		Dim ParamNama As String
        Dim ParamValue As String
        Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
		
        ParamNama = "ID|AM|AY|LOC"
        ParamValue = ID & "|" & _
                     SM & "|" & _
                     SY & "|" & _
                     strLocation

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Cl, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIME_CALC&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")
        End Try
	End Sub
	
    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
	Dim ObjDs As New DataSet
        Dim strIDM As String
        Dim SEmpCode As String
        Dim SEmpName As String
        Dim SDate As String = ""
        Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
        Dim TS As String = Date_Validation(txtWPDate.Text, False) & " " & Trim(txtStartTm.Text)
        Dim TE As String = Date_Validation(txtWPDate.Text, False) & " " & Trim(txtStartTm.Text)
        Dim strStatusAPP As String

        Dim abs As String
        Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PR_PR_TRX_OVRTIME_UPD"
       
        Dim intErrNo As Integer
		Dim strOC As String =  ddlstp.SelectedItem.Value.Trim()

        If ddlEmpCode.Visible Then
            SEmpCode = ddlEmpCode.SelectedItem.Value.Trim()
            SEmpName = ddlEmpCode.SelectedItem.Text
        Else
            SEmpCode = Left(lblempcode.Text, InStr(lblempcode.Text, "(") - 1)
            SEmpName = Mid(Trim(lblempcode.Text), InStr(lblempcode.Text, "(") + 1, Len(Trim(lblempcode.Text)) - InStr(Trim(lblempcode.Text), "(") - 1)
        End If
		
	
		If len(txtWPDate.Text.Trim()) <> 10 Then
				lblErrMessage.Visible = True
                lblErrMessage.Text = "Silakan input tanggal lembur format dd/mm/yyyy !"
                Exit Sub
		End if
		
		If txtWPDate.Text <> "" Then
            SDate = Date_Validation(txtWPDate.Text, False)
            If SDate = "" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Silakan input tanggal lembur !"
                Exit Sub
            End If
        End If
		
		IF StatusPayroll(Cint(SM),SY,strLocation) = "3" Then
		  Exit Sub
		End IF
	
		 
        If SEmpCode = "" Then
            lblErrMessage.Text = "Silakan pilih Employee Code "
            lblErrMessage.Visible = True
            lblErrMessage.Attributes.Add("style", "text-decoration:blink")
            Exit Sub
        End If
		
		
		'IF trim(codeempty.Value) = "KHL" Then
		'    lblErrMessage.Text = SEmpName & " Kary KHL silakan input lembur di BKM premi lain"
       '     lblErrMessage.Visible = True
       '     Exit Sub
       ' End If
		
        abs = GetAttDate(SEmpCode, SDate)

        If abs = "0" Then
            lblErrMessage.Text = SEmpName & " belum melakukan absent di tanggal " & SDate & " / Hanya status kerja yang dapat di proses!"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If TxtDeskripsi.Text.Trim = "" Then
            lblErrMessage.Text = "Deskripsi Lembur belum terisi ..."
            TxtDeskripsi.Focus()
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If Not IsTime(Trim(txtStartTm.Text)) Or Not IsTime(Trim(txtEndTm.Text)) Then
            lblErrMessage.Text = "input format jam (hh:mm) !"
            lblErrMessage.Visible = True
            Exit Sub
        End If
		
		If ddlttnm.SelectedItem.Value.Trim() = "" Then
			lblErrMessage.Text = "Tahun Tanam belum terisi ..."
            lblErrMessage.Visible = True
			Exit Sub
		End if

		'If Cek_ALokasi(ddljob.SelectedItem.Value.Trim())  then
            'lblErrMessage.Text = "Alokasi COA Pekerjaan '" & ddljob.SelectedItem.Text.Trim() & "' belum terisi ..."
            'lblErrMessage.Visible = True
		'	Exit Sub
        'End If
				
        If isNew.Value = "True" Then
            strIDM = getCode()
        Else
            strIDM = LblidM.Text
        End If
		
        LblidM.Text = strIDM
		
		if isEdit.Value = "True" Then
			LblidD.Text = otlnid.Value
		Else
			LblidD.Text = ""
		End If

        If txtkoreksi.Text.Trim <> "" Then
            TxtQty.Text = Request.Form("txtkoreksi")
        Else
            TxtQty.Text = Request.Form("TxtQty")
        End If

     
        If Trim(TxtQty.Text) = "" Or TxtQty.Text = "0" Then
            lblErrMessage.Text = "Silakan input jumlah jam lembur !"
            lblErrMessage.Visible = True
            Exit Sub
        End If



        If Txt150.Text = "" Then Txt150.Text = "0"
        If Txt200.Text = "" Then Txt200.Text = "0"
        If Txt300.Text = "" Then Txt300.Text = "0"
        If Txt400.Text = "" Then Txt400.Text = "0"

        TS = Date_Validation(txtWPDate.Text, False) & " " & Trim(txtStartTm.Text)
        TE = Date_Validation(txtWPDate.Text, False) & " " & Trim(txtEndTm.Text)

        ParamNama = "EC|AM|AY|LC|AI|OR|ST|CD|UD|UI|OD|AD|JB|TS|TE|HR|P1|OV1|P2|OV2|P3|OV3|P4|OV4|STD|CJ|CB|DESC|OC"
        ParamValue = SEmpCode & "|" & _
                     SM & "|" & _
                     SY & "|" & _
                     strLocation & "|" & _
                     strIDM & "|" & _
                     ovrrate.Value & "|1|" & _
                     DateTime.Now() & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId & "|" & _
                     SDate & "|" & _
                     LblidD.Text & "|" & _
                     ddljob.SelectedItem.Text.Trim() & "|" & _
                     TS & "|" & _
                     TE & "|" & _
                     TxtQty.Text & "|" & _
                     lbl_psn1.Text.Trim & "|" & _
                     Txt150.Text & "|" & _
                     lbl_psn2.Text.Trim & "|" & _
                     Txt200.Text & "|" & _
                     lbl_psn3.Text.Trim & "|" & _
                     Txt300.Text & "|" & _
                     lbl_psn4.Text.Trim & "|" & _
                     Txt400.Text & "|" & _
					 iif(intlevel<=1,"3",hidstatus.value) & "|" & _
                     ddljob.SelectedItem.Value.Trim() & "|" & _
                     ddlttnm.SelectedItem.Value.Trim() & "|" & _
                     TxtDeskripsi.Text.Trim() & "|" & _
					 strOC 

													
        Try
   
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Up, _
                                                    ParamNama, _
                                                    ParamValue, ObjDs)
													
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIPANEN_UPD&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")
        End Try

        strIDM = Trim(ObjDs.Tables(0).Rows(0).Item("OtId"))
        CalRPLembur(strIDM)
        isNew.Value = "False"

        strStatusAPP = ""
        strOTCode = strIDM
        onLoad_Display()
		onLoad_button()
        BindEmpOvertimeLn(strIDM)
        LblidD.Text = getCodeln()

        Dim vOtlnID As String
        For intCnt = 0 To dgOvtDet.Items.Count - 1
            ''-- SET DEFAULT
            If Trim(CType(dgOvtDet.Items(intCnt).FindControl("lblID"), Label).Text) <> "" Then
                vOtlnID = CType(dgOvtDet.Items(intCnt).FindControl("lblID"), Label).Text
                strStatusAPP = CType(dgOvtDet.Items(intCnt).FindControl("lblstat2"), Label).Text.Trim
                PayrollApp(strStatusAPP, vOtlnID)
            End If
        Next

        clearAll()
		isEdit.Value = "False"
    End Sub

	
    Sub DeleteBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PR_PR_TRX_OVRTIMELN_DEL"
       
        Dim intErrNo As Integer
		Dim SDate As String = Date_Validation(txtWPDate.Text, False) 
		Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
		
		If SDate = "" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Silakan input tanggal lembur !"
                Exit Sub
        End If
			
		IF StatusPayroll(Cint(SM),SY,strLocation) = "3" Then
		  Exit Sub
		End IF
		
        ParamNama = "AD|AM"
        ParamValue = LblidD.Text.Trim() & "|" & LblidM.Text.Trim()
        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIMELN_DEL&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")
        End Try

        CalRPLembur(LblidM.Text.Trim())
		
        onLoad_Display()
		onLoad_button()
        BindEmpOvertimeLn(LblidM.Text.Trim())
    End Sub

    Sub btnCalcClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetID As String = "PR_PR_TRX_OVRTIME_CALC_OT"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim SDate As String = ""
		Dim SEmpCode As String
		Dim SEmpName As String
		
		Dim abs As String
				
		If ddlEmpCode.Visible Then
            SEmpCode = ddlEmpCode.SelectedItem.Value.Trim()
            SEmpName = ddlEmpCode.SelectedItem.Text
        Else
            SEmpCode = Left(lblempcode.Text, InStr(lblempcode.Text, "(") - 1)
            SEmpName = Mid(Trim(lblempcode.Text), InStr(lblempcode.Text, "(") + 1, Len(Trim(lblempcode.Text)) - InStr(Trim(lblempcode.Text), "(") - 1)
        End If

        If txtWPDate.Text <> "" Then
            SDate = Date_Validation(txtWPDate.Text, False)
            If SDate = "" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Silakan input tanggal lembur !"
                Exit Sub
            End If
        End If

        If Not IsTime(Trim(txtStartTm.Text)) Or Not IsTime(Trim(txtEndTm.Text)) Then
            lblErrMessage.Text = "input format jam (hh:mm) !"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If txtkoreksi.Text.Trim <> "" Then
            TxtQty.Text = Request.Form("txtkoreksi")
        Else
            TxtQty.Text = Request.Form("TxtQty")
        End If
        
        If TxtQty.Text.Trim = "" Then
            lblErrMessage.Text = "input format jam lembur !"
            lblErrMessage.Visible = True
            Exit Sub
        End If
		
		abs = GetAttDate(SEmpCode, SDate)

        If abs = "0" Then
            lblErrMessage.Text = SEmpName & " belum melakukan absent di tanggal " & SDate & " / Hanya status kerja yang dapat di proses !"
            lblErrMessage.Visible = True
            Exit Sub
        End If


        ParamName = "DT|LOC|JM|TY"
        ParamValue = SDate & "|" & strLocation & "|" & TxtQty.Text.Trim & "|" & ddlstp.SelectedItem.Value.Trim() 

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIME_CALC_OT&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        lbl_psn1.Text = Trim(objNewID.Tables(0).Rows(0).Item("p1"))
        Txt150.Text = Trim(objNewID.Tables(0).Rows(0).Item("ov1"))

        lbl_psn2.Text = Trim(objNewID.Tables(0).Rows(0).Item("p2"))
        Txt200.Text = Trim(objNewID.Tables(0).Rows(0).Item("ov2"))

        lbl_psn3.Text = Trim(objNewID.Tables(0).Rows(0).Item("p3"))
        Txt300.Text = Trim(objNewID.Tables(0).Rows(0).Item("ov3"))

        lbl_psn4.Text = Trim(objNewID.Tables(0).Rows(0).Item("p4"))
        Txt400.Text = Trim(objNewID.Tables(0).Rows(0).Item("ov4"))

    End Sub

	Sub KeepRunningSum(ByVal sender As Object, ByVal E As DataGridItemEventArgs)
        If E.Item.ItemType = ListItemType.Footer Then
            E.Item.Cells(7).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cntTotal, 2)
            E.Item.Cells(8).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cntTotal1, 2)
            E.Item.Cells(9).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cntTotal2, 2)
            E.Item.Cells(10).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cntTotal3, 2)
            E.Item.Cells(11).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(cntTotal4, 2)
            E.Item.Cells(12).Text = objGlobal.GetIDDecimalSeparator_FreeDigit(CntTotalAmt, 2)
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_OvertimeList_Estate.aspx")
    End Sub
	
    Sub BtnVerBK_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        'Dim ParamNama As String
        'Dim ParamValue As String
        'Dim strOpCd_Up As String = "PR_PR_TRX_OVRTIME_UPD_APV"
        'Dim intErrNo As Integer

        'ParamNama = "STD|UI|AD"
        'ParamValue = "2|" & strUserId & "|" & LblidD.Text.Trim()

        'Try
        '    intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        'Catch ex As Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIME_CALC&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")
        'End Try

        'CalRPLembur(LblidM.Text.Trim())

        PayrollApp("2", LblidD.Text.Trim())

        'isNew.Value = "False"
        '      onLoad_Display()
        'onLoad_button()
        '      BindEmpOvertimeLn(LblidM.Text.Trim())

    End Sub
	
	Sub BtnConfBK_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        PayrollApp("1", LblidD.Text.Trim())
	End Sub
	
	Sub BtnReActBK_onClick(ByVal sender As Object, ByVal e As System.EventArgs)
	   Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PR_PR_TRX_OVRTIME_UPD_APV"
        Dim intErrNo As Integer
		
		ParamNama = "STD|UI|AD" 
        ParamValue = "3|" & strUserId & "|" & LblidD.Text.Trim() 

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIME_CALC&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")
        End Try

		CalRPLembur(LblidM.Text.Trim())
		
		isNew.Value = "False"
		hidstatus.value = "3" 
        onLoad_Display()
		onLoad_button()
        BindEmpOvertimeLn(LblidM.Text.Trim())
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
      
       
        ParamName = "MN|YR|LOC|S1|S2|S3|S4"
        ParamValue = mn & "|" & _
                     yr & "|" & _
                     loc & "|1|||" 
        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCdSP, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_UPD&errmesg=" & Exp.Message & "&redirect=")
        End Try

    End Sub

    Sub PayrollApp(ByVal pStatus As String, ByVal pPRID As String)
        Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PR_PR_TRX_OVRTIME_UPD_APV"
        Dim intErrNo As Integer

        ParamNama = "STD|UI|AD"
        ParamValue = pStatus & "|" & strUserId & "|" & pPRID

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_OVRTIME_CALC&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")
        End Try

        'CalRPLembur(LblidM.Text.Trim())

        isNew.Value = "False"
        onLoad_Display()
        onload_button()
        BindEmpOvertimeLn(LblidM.Text.Trim())

    End Sub
	
	#End Region


End Class
