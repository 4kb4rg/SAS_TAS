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
Public Class PR_trx_BKMEmp_Estate : Inherits Page

    Protected WithEvents tblSelection As HtmlTable


    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlEmpCode As DropDownList
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList

    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents BackBtn As ImageButton
    Protected WithEvents lblErrMessage As Label

    'absen
    Protected WithEvents dgabsen As DataGrid
    Dim ArrayHoliday As New ArrayList()
    Dim ArrayWeekend As New ArrayList()

    'pekerjaan
    Protected WithEvents dgbkm As DataGrid

    ' koreksi
    Protected WithEvents dgkoreksi As DataGrid
	
	' finger
    Protected WithEvents dgfinger As DataGrid
	
	' Exption
    Protected WithEvents dgExp As DataGrid


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
    Dim strAcceptFormat As String
	Dim intLevel As Integer

 

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
            If Not IsPostBack Then
                BindDivision()
                ddlMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))
				BindEmployee("")
                If Session("BKMEMP") <> "" Then
                    Dim prevset As String
                    Dim ary As Array
                    prevset = Session("BKMEMP")
                    ary = Split(prevset, "|")
                    ddlEmpDiv.SelectedValue = ary(0)
                    ddlMonth.SelectedValue = ary(2)
                    BindAccYear(ary(3))
					ddlEmpCode.SelectedValue = ary(1)
                    BindAbsen()
					BindFinger()
					BindExp()
                    BindKoreksi()
                    BindBkm()
                End If
         
            End If
            lblErrMessage.Visible = False

        End If
    End Sub

    

    Function GetAtt(ByVal n As String) As Drawing.Color

        Select Case Trim(n)
            Case ""
                Return Drawing.Color.Gold
            Case "K"
                Return Drawing.Color.Green
            Case "M", "P1"
                Return Drawing.Color.Red
            Case "HM", "HB"
                Return Drawing.Color.Blue
            Case Else
                Return Drawing.Color.Black
        End Select


    End Function

    Function GetWeekEnd(ByVal d As String) As System.Drawing.Color
        Dim strMonth As String = ddlMonth.SelectedItem.Value
        Dim StrYear As String = ddlyear.SelectedItem.Value.Trim
        Dim Strdate As String
        Dim Strdate2 As String

        Select Case strMonth
            Case "1" : strMonth = " Jan "
            Case "2" : strMonth = " Feb "
            Case "3" : strMonth = " Mar "
            Case "4" : strMonth = " Apr "
            Case "5" : strMonth = " May "
            Case "6" : strMonth = " Jun "
            Case "7" : strMonth = " Jul "
            Case "8" : strMonth = " Aug "
            Case "9" : strMonth = " Sep "
            Case "10" : strMonth = " Oct "
            Case "11" : strMonth = " Nov "
            Case "12" : strMonth = " Dec "
        End Select

        Strdate = d & strMonth & StrYear
        If ddlMonth.SelectedItem.Value < 10 Then
            Strdate2 = StrYear & "0" & ddlMonth.SelectedItem.Value & d
        Else
            Strdate2 = StrYear & ddlMonth.SelectedItem.Value & d
        End If

        If d <= DateTime.DaysInMonth(StrYear, ddlMonth.SelectedItem.Value) Then
            If (isWeekend(Strdate2)) Then
                Return Drawing.ColorTranslator.FromHtml("#F78181")
            End If
            If (isHoliday(Strdate2)) Then
                Return Drawing.ColorTranslator.FromHtml("#FE2E2E")
            End If
        End If

    End Function

    Function isDaysInMonth(ByVal d As String) As Boolean
        Dim StrYear As String = ddlyear.SelectedItem.Value.Trim
        If d <= DateTime.DaysInMonth(StrYear, ddlMonth.SelectedItem.Value) Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub GetHoliday(ByVal m As String, ByVal y As String)
        Dim strOpCd_GetID As String = "PR_PR_STP_HOLIDAY_GET_DATE"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim IntCnt As Integer

        ParamName = "TY|MN|YR"
        ParamValue = "B|" & m & "|" & y

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_HOLIDAY_GET_DATE&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        ArrayHoliday.Clear()
        If objNewID.Tables(0).Rows.Count > 0 Then
            For IntCnt = 0 To objNewID.Tables(0).Rows.Count - 1
                ArrayHoliday.Add(Trim(objNewID.Tables(0).Rows(IntCnt).Item("HolidayDate")))
            Next
        End If

    End Sub

    Function isHoliday(ByVal s As String) As Boolean
        Dim i As Integer
        Dim fnd As Boolean = False

        For i = 0 To ArrayHoliday.Count - 1
            If ArrayHoliday(i).ToString = s Then
                fnd = True
            End If
        Next
        Return fnd
    End Function

    Function isWeekend(ByVal s As String) As Boolean
        Dim i As Integer
        Dim fnd As Boolean = False

        For i = 0 To ArrayWeekend.Count - 1
            If ArrayWeekend(i).ToString = s Then
                fnd = True
            End If
        Next
        Return fnd
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

    Sub GetWeekend(ByVal m As String, ByVal y As String)
        Dim strOpCd_GetID As String = "PR_PR_STP_HOLIDAY_GET_DATE"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim IntCnt As Integer

        ParamName = "TY|MN|YR"
        ParamValue = "M|" & m & "|" & y

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_HOLIDAY_GET_DATE&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        ArrayWeekend.Clear()
        If objNewID.Tables(0).Rows.Count > 0 Then
            For IntCnt = 0 To objNewID.Tables(0).Rows.Count - 1
                ArrayWeekend.Add(Trim(objNewID.Tables(0).Rows(IntCnt).Item("HolidayDate")))
            Next
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

    Sub BindDivision()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpDivDs As New Object
        Dim intCnt As Integer
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
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
        dr("BlkGrpCode") = " "
        dr("Description") = "Pilih Divisi"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpDiv.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDiv.DataTextField = "Description"
        ddlEmpDiv.DataValueField = "BlkGrpCode"
        ddlEmpDiv.DataBind()
        ddlEmpDiv.SelectedIndex = 0

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
		Dim strsrc As String
		Dim Strdate2 AS String 
		Dim strAM as String
		
		If ddlMonth.SelectedItem.Value < 10 Then
            Strdate2 = ddlyear.SelectedItem.Value & "0" & ddlMonth.SelectedItem.Value 
			strAM    = "0" & ddlMonth.SelectedItem.Value 
        Else
            Strdate2 = ddlyear.SelectedItem.Value & ddlMonth.SelectedItem.Value 
			strAM    = ddlMonth.SelectedItem.Value 
        End If

		strsrc = " AND  A.EmpCode Not in (Select empcode from HR_TRX_EMPRESIGN where status='1' and " & Strdate2 & " > convert(char(6),efektifdate,112)) "



        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & strAM & "|" & ddlyear.SelectedItem.Value &"|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%' " & strsrc & "|ORDER BY EmpName"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description")) & " - " & Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("Job"))
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "Pilih Karyawan"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpCode.DataSource = objEmpCodeDs.Tables(0)
        ddlEmpCode.DataTextField = "_Description"
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intSelectedIndex

    End Sub

    Sub ddlEmpDiv_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindEmployee(ddlEmpDiv.SelectedItem.Value.Trim())
    End Sub

    Sub SearchBtn_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If ddlEmpDiv.SelectedItem.Value.Trim() = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan pilih Divisi"
            Exit Sub
        End If

        If ddlEmpCode.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan pilih Karyawan"
            Exit Sub
        End If

        BindAbsen()
		BindFinger()
		BindExp
        BindKoreksi()
        BindBkm()

    End Sub

    Sub BindAbsen()
        Dim dsTemp As DataSet

        GetHoliday(ddlMonth.SelectedItem.Value.Trim(), ddlyear.SelectedItem.Value.Trim)
        GetWeekEnd(ddlMonth.SelectedItem.Value.Trim(), ddlyear.SelectedItem.Value.Trim)
        dsTemp = LoadAttData()

        dgabsen.DataSource = dsTemp
        dgabsen.DataBind()

    End Sub

    Protected Function LoadAttData() As DataSet
        Dim strOpCd_Get As String = "PR_PR_TRX_EMPATT_CHECKLIST_GET"
        Dim strOpCd_AbsGet As String = "PR_PR_TRX_ATTEMP_LIST_GET"
        Dim strSrchEmpCode As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intCnt2 As Integer
        Dim objEmpDs As New Object()
        Dim ObjAtt As New Object()
        Dim StrEmp As String
        Dim ListEmp As String
        Dim MandorFilter As String = ""
        Dim strPrd As String = ""

        'GET Data Emp
        strSrchEmpCode = ddlEmpCode.SelectedItem.Value.Trim()
        
        If ddlMonth.SelectedItem.Value < 10 Then
            strPrd = ddlyear.SelectedItem.Value.Trim & "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strPrd = ddlyear.SelectedItem.Value.Trim & ddlMonth.SelectedItem.Value.Trim
        End If


        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & _
                        "AND A.EmpCode = '" & strSrchEmpCode & "' " & _
                        "AND A.LocCode Like '%" & strLocation & "%' " & _
                        "AND A.EmpCode Not in (Select empcode from HR_TRX_EMPRESIGN where status='1' and '" & strPrd & "' > convert(char(6),efektifdate,112))" & _
                        "|"


        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_EMPATT_CHECKLIST_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        ListEmp = "("

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpName")) '& " (" & Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpName")) & ")"

            ListEmp = ListEmp & "'" & objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") & "'"
            If intCnt = objEmpDs.Tables(0).Rows.Count - 1 Then
                ListEmp = ListEmp & ""
            Else
                ListEmp = ListEmp & ","
            End If
        Next

        If ListEmp = "(" Then
            ListEmp = "('')"
        Else
            ListEmp = ListEmp & ")"
        End If

        'GET Data Absent

        If Not ListEmp = "('')" Then
            strSrchEmpCode = ListEmp
            strParamName = "SEARCH|SORT"
            strParamValue = "AND EmpCode IN " & strSrchEmpCode & _
                   " AND Month(AttDate)='" & ddlMonth.SelectedItem.Value.Trim() & "' AND Year(AttDate)='" & ddlyear.SelectedItem.Value.Trim & "'|Order By EmpCode,AttDate"
            Try
                intErrNo = Objok.mtdGetDataCommon(strOpCd_AbsGet, strParamName, strParamValue, ObjAtt)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTEMP_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
            End Try

            If ObjAtt.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
                    StrEmp = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                    For intCnt2 = 0 To ObjAtt.Tables(0).Rows.Count - 1
                        If StrEmp = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("EmpCode")) Then
                            Select Case Day(ObjAtt.Tables(0).Rows(intCnt2).Item("AttDate"))
                                Case 1
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_1") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C1") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 2
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_2") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C2") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 3
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_3") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C3") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 4
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_4") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C4") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 5
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_5") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C5") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 6
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_6") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C6") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 7
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_7") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C7") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 8
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_8") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C8") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 9
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_9") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C9") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 10
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_10") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C10") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 11
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_11") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C11") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 12
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_12") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C12") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 13
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_13") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C13") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 14
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_14") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C14") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 15
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_15") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C15") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 16
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_16") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C16") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 17
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_17") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C17") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 18
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_18") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C18") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 19
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_19") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C19") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 20
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_20") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C20") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 21
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_21") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C21") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 22
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_22") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C22") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 23
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_23") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C23") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 24
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_24") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C24") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 25
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_25") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C25") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 26
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_26") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C26") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 27
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_27") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C27") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 28
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_28") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C28") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 29
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_29") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C29") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 30
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_30") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C30") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 31
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_31") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C31") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                            End Select
                        End If
                    Next
                Next
            End If

        End If
        Return objEmpDs
    End Function

    Sub dgabsen_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")

            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If

        End If
    End Sub

    Sub dgabsen_OnCommand(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strCmdArgs As String = E.CommandArgument
        Dim strDay As String = strCmdArgs
        Dim AMonth As String = ddlMonth.SelectedItem.Value
        Dim strMonth As String = ""
        Dim strYear As String = ddlyear.SelectedItem.Value.Trim
        Dim strDate As String
        Dim lbl As Label
        Dim intIndex As Integer = E.Item.ItemIndex
        Dim strEmpCode As String
        Dim strEmpName As String
        Dim strEmpTy As String
        Dim strAMonth As String

        Dim a As String = E.Item.Cells(intIndex).Text

        Select Case AMonth
            Case "1" : strMonth = " Jan "
            Case "2" : strMonth = " Feb "
            Case "3" : strMonth = " Mar "
            Case "4" : strMonth = " Apr "
            Case "5" : strMonth = " May "
            Case "6" : strMonth = " Jun "
            Case "7" : strMonth = " Jul "
            Case "8" : strMonth = " Aug "
            Case "9" : strMonth = " Sep "
            Case "10" : strMonth = " Oct "
            Case "11" : strMonth = " Nov "
            Case "12" : strMonth = " Dec "
        End Select


        If AMonth < 10 Then
            strAMonth = "0" & AMonth
        Else
            strAMonth = AMonth
        End If


        strDate = strDay & strMonth & strYear

        lbl = dgabsen.Items.Item(intIndex).FindControl("lblEmpCode")
        strEmpCode = lbl.Text
        lbl = dgabsen.Items.Item(intIndex).FindControl("lblEmpName")
        strEmpName = lbl.Text

        lbl = dgabsen.Items.Item(intIndex).FindControl("lblEmpType")
        strEmpTy = lbl.Text

        Dim mdr As String = ""

        Session("BKMEMP") = ddlEmpDiv.SelectedItem.Value.Trim() & "|" & _
                            ddlEmpCode.SelectedItem.Value.Trim & "|" & _
                            ddlMonth.SelectedItem.Value.Trim() & "|" & _
                            ddlyear.SelectedItem.Value.Trim()

        Response.Write("<Script Language=""JavaScript"">pop_Att=window.open(""PR_trx_DailyAttdDet_Estate.aspx?redirect=attm&EmpCode=" & strEmpCode & "&Attdate=" & strDate & "&EmpName=" & strEmpName & "&EmpTy=" & strEmpTy & _
                               "&AccMonth=" & strAMonth & "&AccYear=" & strYear & "&AttTy=" & a & "&HdrDiv=" & ddlEmpDiv.SelectedItem.Value.Trim() & """, null ,""'pop_Att',width=650,height=300,top=230,left=200,status=yes, resizable=no, scrollbars=yes, toolbar=no, location=no"");pop_Att.focus();</Script>")

    End Sub


    Public Sub BindFinger()
        Dim dsTemp As DataSet

        dsTemp = LoadFinger()

        dgfinger.DataSource = dsTemp
        dgfinger.DataBind()

    End Sub

    Protected Function LoadFinger()
        Dim strOpCd_Get As String = "PR_PR_TRX_FINGER_SCAN_EMPGET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpDs As New Object()

        strParamName = "LOC|EC|AM|AY"
        strParamValue = strLocation & "|" & _
                        ddlEmpCode.SelectedItem.Value.Trim & "|" & _
                        ddlMonth.SelectedItem.Value.Trim & "|" & _
                        ddlyear.SelectedItem.Value.Trim


        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_FINGER_SCAN_EMPGET&errmesg=" & Exp.Message)
        End Try

        Return objEmpDs

    End Function

    Public Sub BindExp()
        Dim dsTemp As DataSet

        dsTemp = LoadExp()

        dgExp.DataSource = dsTemp
        dgExp.DataBind()

    End Sub

    Protected Function LoadExp()
        Dim strOpCd_Get As String = "PR_PR_TRX_ATTENDANCE_EXPLST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpDs As New Object()

        strParamName = "LOC|EC|AM|AY|DV|TY|EN"
        strParamValue = strLocation & "|" & _
                        ddlEmpCode.SelectedItem.Value.Trim & "|" & _
                        ddlMonth.SelectedItem.Value.Trim & "|" & _
                        ddlyear.SelectedItem.Value.Trim & "|||"


        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_FINGER_SCAN_EMPGET&errmesg=" & Exp.Message)
        End Try

        Return objEmpDs

    End Function

    Public Sub BindBkm()
        Dim dsTemp As DataSet

        dsTemp = LoadBkmData()

        dgbkm.DataSource = dsTemp
        dgbkm.DataBind()

    End Sub

    Protected Function LoadBkmData()
        Dim strOpCd As String = "PR_PR_TRX_BKMEMP_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpDs As New Object()

        strParamName = "LOC|EC|AM|AY"
        strParamValue = strLocation & "|" & _
                        ddlEmpCode.SelectedItem.Value.Trim & "|" & _
                        ddlMonth.SelectedItem.Value.Trim & "|" & _
                        ddlyear.SelectedItem.Value.Trim


        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMEMP_LIST_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objEmpDs

    End Function

    Sub dgbkm_Bind(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim lbl As Label
            Dim lblbkm As Label
            Dim lblpremi As label

            lbl = CType(e.Item.FindControl("lbID"), Label)
            lblbkm = CType(e.Item.FindControl("lblbkmcode"), Label)
            lblpremi = CType(e.Item.FindControl("lblpremi"), Label)

            If lblpremi.Text = "0.0000" Then
                lblpremi.visible = False
            Else
                lblpremi.visible = True
            End If


            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If


        End If
    End Sub

    Sub dgbkm_OnCommand(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strCmdArgs As String = E.CommandArgument
        Dim strDay As String = strCmdArgs
        Dim lbl As Label
        Dim intIndex As Integer = E.Item.ItemIndex

        lbl = dgbkm.Items.Item(intIndex).FindControl("lblbkmcode")

        Session("BKMEMP") = ddlEmpDiv.SelectedItem.Value.Trim() & "|" & _
                            ddlEmpCode.SelectedItem.Value.Trim & "|" & _
                            ddlMonth.SelectedItem.Value.Trim() & "|" & _
                            ddlyear.SelectedItem.Value.Trim()


        Response.Write("<Script Language=""JavaScript"">pop_Att=window.open(""PR_trx_BKMDet_New_Estate.aspx?redirect=attm&BKMCode=" & lbl.Text.Trim & """, null ,""'pop_Att',width=800,height=500,top=100,left=100,status=yes, resizable=no, scrollbars=yes, toolbar=no, location=no"");pop_Att.focus();</Script>")

    End Sub

    Public Sub BindKoreksi()
        Dim dsTemp As DataSet
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        dsTemp = LoadKoreksi()

        dgkoreksi.DataSource = dsTemp
        dgkoreksi.DataBind()

        For intCnt = 0 To dgkoreksi.Items.Count - 1
            lbButton = dgkoreksi.Items.Item(intCnt).FindControl("dgkoreksi_lbDelete")
            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        Next

    End Sub

    Protected Function LoadKoreksi()
        Dim strOpCd As String = "PR_PR_TRX_ATTENDANCE_KOREKSI_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpDs As New Object()

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.Status='1' AND LocCode='" & strLocation & "' " & _
                        "AND Month(AttDate)='" & ddlMonth.SelectedItem.Value.Trim() & "' AND Year(AttDate)='" & ddlyear.SelectedItem.Value.Trim & "' " & _
                        "AND EmpCode Like '%" & ddlEmpCode.SelectedItem.Value.Trim() & "%'|" & _
                        "Order By AttDate"


        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_KOREKSI_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objEmpDs

    End Function

    Sub dgkoreksi_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")

            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If

        End If
    End Sub

    Sub dgkoreksi_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_UPD As String = "PR_PR_TRX_ATTENDANCE_KOREKSI_DEL"
        Dim int As Integer = e.Item.ItemIndex
        Dim strParamName As String
        Dim strParamValue As String
        Dim strAI As String
		Dim strEC As String
		Dim strDT As String
        Dim intErrNo As Integer
        Dim EditText As Label

		IF intLevel > 2
		
        EditText = dgkoreksi.Items.Item(int).FindControl("dgkoreksi_Attid")
        strAI = EditText.Text.Trim
		EditText = dgkoreksi.Items.Item(int).FindControl("lblempcode")
        strEC = EditText.Text.Trim
		EditText = dgkoreksi.Items.Item(int).FindControl("dgkoreksi_attdate")
        strDT = EditText.Text.Trim
		
        
        strParamName = "AI|EC|DT|CD|UD|UI|LOC"
        strParamValue = strAI & "|" & _
		                strEC & "|" & _
						strDT & "|" & _
                        Now() & "|" & _
                        Now() & "|" & _
                        strUserId & "|" & _
                        strLocation



        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_UPD, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_KOREKSI_DEL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgkoreksi.EditItemIndex = -1

        BindAbsen()
        BindKoreksi()
        BindBkm()
		
		Else
			
		End If 
    End Sub
End Class
