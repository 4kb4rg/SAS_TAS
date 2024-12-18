Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports agri.PR
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap



Public Class PR_trx_AttdList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlEmpType As DropDownList
    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlmandor As DropDownList
    Protected WithEvents ddlview As DropDownList

    Protected WithEvents ddlyear As DropDownList
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SortExpression As Label

	Protected WithEvents GenerateBtn As ImageButton 
	Protected WithEvents ExpBtn  As Button 

    Protected objPRTrx As New agri.PR.clsTrx()
    Protected objHRTrx As New agri.HR.clsTrx()
    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim Objok As New agri.GL.ClsTrx

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intLevel As Integer
    Dim strLocType As String

    Dim objEmpTypeDs As New Object()
    Dim objEmpDivDs As New Object()
    Dim objEmpJobDs As New Object()

    Dim strAcceptFormat As String
    Dim ArrayHoliday As New ArrayList()
    Dim ArrayWeekend As New ArrayList()


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        strLocType = Session("SS_LOCTYPE")
		intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAttendance), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            GenerateBtn.Visible = True
		IF intLevel < 2
		ExpBtn.Visible = False
		Else
		ExpBtn.Visible = True
		End if
		
		
            If SortExpression.Text = "" Then
                SortExpression.Text = "EmpName"
            End If

            If Not Page.IsPostBack Then
                ddlMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))
                BindEmpDiv()
                BindEmpType()
                BindMandoran("")
                If Session("ATT") <> "" Then
                    Dim prevset As String
                    Dim ary As Array
                    prevset = Session("ATT")
                    ary = Split(prevset, "|")
                    ddlMonth.SelectedValue = ary(0)
                    BindAccYear(ary(1))
                    txtEmpCode.Text = ary(2)
                    txtEmpName.Text = ary(3)
                    ddlEmpDiv.SelectedValue = ary(4)
                    ddlmandor.SelectedValue = ary(5)
                    ddlEmpType.SelectedValue = ary(6)
                    ddlview.SelectedValue = ary(7)
                    lblCurrentIndex.Text = ary(8)
                End If
                BindGrid()

            End If
        End If

    End Sub

	Sub GenEmployee()
		Dim strOpCd_Get2 As String = "HR_HR_TRX_EMPLOYEE_GEN"
		Dim strAM as String
		DIm strAY as String
		Dim strParamName As String
        Dim strParamValue As String
		Dim objEmpDs As New Object
        Dim intErrNo As Integer
        
		
		 If ddlMonth.SelectedItem.Value < 10 Then
        	strAM    = "0" & ddlMonth.SelectedItem.Value 
        Else
        	strAM    = ddlMonth.SelectedItem.Value 
        End If
		
		strParamName = "AM|AY|LOC"
        strParamValue = strAM & "|" & _
						ddlyear.SelectedItem.Value.trim() & "|" & _
						strLocation
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_Get2, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_GEN &errmesg=" & Exp.Message )
        End Try
        
		
	end Sub
	
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

    Function GetDivKebunKantor(ByVal d As String) As String
        Dim strOpCd_GetID As String = "PR_PR_STP_DIVISICODE_GET"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim StrParamName As String
        Dim StrParamValue As String

        strParamName = "SEARCH|SORT"
        StrParamValue = "AND A.BlkGrpCode Like '%" & d & "%' AND A.LocCode = '" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_GetID, StrParamName, StrParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return ""
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

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsTemp As DataSet

        GetHoliday(ddlMonth.SelectedItem.Value.Trim(), ddlyear.SelectedItem.Value.Trim)
        GetWeekEnd(ddlMonth.SelectedItem.Value.Trim(), ddlyear.SelectedItem.Value.Trim)
        dsTemp = LoadAttData()

        Dim KK As String = GetDivKebunKantor(ddlEmpDiv.SelectedItem.Value.Trim())
        If (KK = "K") Or (KK = "") Then
            dgLine.PageSize = 15
        End If

        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgLine.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsTemp = LoadAttData()
            End If
        End If

        dgLine.DataSource = dsTemp
        dgLine.DataBind()
        lblPageCount.Text = PageCount
        BindPageList(PageCount)
        PageNo = lblCurrentIndex.Text + 1

    End Sub

    Sub BindEmpType()
        Dim strOpCd_EmpType As String = "HR_HR_STP_EMPTYPE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow

        strParamName = "SEARCH|SORT"
        strParamValue = "|"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpType, strParamName, strParamValue, objEmpTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_EMPTYPE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objEmpTypeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpTypeDs.Tables(0).Rows.Count - 1
                objEmpTypeDs.Tables(0).Rows(intCnt).Item("EmpTyCode") = Trim(objEmpTypeDs.Tables(0).Rows(intCnt).Item("EmpTyCode"))
                objEmpTypeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpTypeDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objEmpTypeDs.Tables(0).NewRow()
        dr("EmpTyCode") = ""
        dr("Description") = "All"
        objEmpTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpType.DataSource = objEmpTypeDs.Tables(0)
        ddlEmpType.DataTextField = "Description"
        ddlEmpType.DataValueField = "EmpTyCode"
        ddlEmpType.DataBind()
        ddlEmpType.SelectedIndex = 0

    End Sub

    Sub BindEmpDiv()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow



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
    	Dim strAM As String 
		 
		 If ddlMonth.SelectedItem.Value < 10 Then
        	strAM    = "0" & ddlMonth.SelectedItem.Value 
        Else
        	strAM    = ddlMonth.SelectedItem.Value 
        End If
        
        strParamName  = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & strAM & "|" & ddlyear.SelectedItem.Value.trim() & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strdivcode & "%' AND (isMandor<>'0')|ORDER BY A.EmpName"

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


    Sub BindPageList(ByVal cnt As String)
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count & " of " & cnt)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub

    Protected Function LoadAttData() As DataSet
        Dim strOpCd_Get As String = "PR_PR_TRX_EMPATT_CHECKLIST_GET"
        Dim strOpCd_AbsGet As String = "PR_PR_TRX_ATTEMP_LIST_GET"
        Dim strSrchEmpCode As String
        Dim strSrchEmpName As String
        Dim strSrchEmpDiv As String
        Dim strSrchEmpType As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intCnt2 As Integer
        Dim strSortExpression As String
        Dim objEmpDs As New Object()
        Dim ObjAtt As New Object()
        Dim StrEmp As String
        Dim ListEmp As String
        Dim MandorFilter As String = "" 
		Dim strPrd as String = "" 

        Session("SS_PAGING") = lblCurrentIndex.Text

        'GET Data Emp
        strSrchEmpCode = IIf(txtEmpCode.Text = "", "", txtEmpCode.Text)
        strSrchEmpName = IIf(txtEmpName.Text = "", "", txtEmpName.Text)
        strSrchEmpDiv = IIf(ddlEmpDiv.SelectedItem.Value = "0", "", ddlEmpDiv.SelectedItem.Value)
        strSrchEmpType = IIf(ddlEmpType.SelectedItem.Value = "0", "", ddlEmpType.SelectedItem.Value) 
		
		If ddlMonth.SelectedItem.Value < 10 Then
            strPrd = ddlyear.SelectedItem.Value.Trim & "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strPrd = ddlyear.SelectedItem.Value.Trim & ddlMonth.SelectedItem.Value.Trim
        End If 

        If SortExpression.Text = "UserName" Then
            strSortExpression = "B.UserName"
        Else
            strSortExpression = "A." & SortExpression.Text
        End If

        If ddlmandor.Text.Trim() <> "" Then
            If ddlmandor.SelectedItem.Value.Trim() <> "" Then 
			'by aam 9 sep 2012
                MandorFilter = "AND A.EmpCode IN (SELECT y.CodeEmp FROM HR_TRX_EMPMANDOR x ,HR_TRX_EMPMANDORLN y WHERE x.status='1' AND x.LocCode='" & strLocation & "' AND x.MandorCode=y.CodeMandor AND x.CodeEmp='" & ddlmandor.SelectedItem.Value.Trim() & "' and y.accyear+y.accmonth='" & strPrd & "') "
            End If
        End If

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & _
                        "AND A.EmpCode Like '%" & strSrchEmpCode & "%' " & _
                        "AND A.EmpName Like '%" & strSrchEmpName & "%' " & _
                        "AND C.IDDiv Like '%" & strSrchEmpDiv & "%' " & _
                        "AND C.CodeEmpty Like '%" & strSrchEmpType & "%' " & _
                        "AND A.LocCode Like '%" & strLocation & "%' " & _
                        "AND A.EmpCode Not in (Select empcode from HR_TRX_EMPRESIGN where status='1' and '" & strPrd & "' > convert(char(6),efektifdate,112))" & _
                         MandorFilter & "|" & _
                        "ORDER BY " & strSortExpression

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
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_1") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_1") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C1") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 2
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_2") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_2") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C2") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 3
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_3") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_3") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C3") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 4
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_4") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_4") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C4") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 5
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_5") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_5") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C5") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 6
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_6") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_6") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C6") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 7
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_7") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_7") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C7") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 8
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_8") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_8") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C8") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 9
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_9") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_9") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C9") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 10
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_10") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_10") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C10") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 11
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_11") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_11") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C11") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 12
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_12") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_12") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C12") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 13
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_13") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_13") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C13") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 14
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_14") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_14") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C14") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 15
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_15") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_15") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C15") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 16
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_16") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_16") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C16") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 17
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_17") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_17") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C17") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 18
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_18") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_18") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C18") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 19
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_19") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_19") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C19") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 20
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_20") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_20") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C20") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 21
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_21") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_21") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C21") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 22
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_22") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_22") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C22") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 23
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_23") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_23") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C23") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 24
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_24") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_24") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C24") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 25
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_25") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_25") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C25") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 26
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_26") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_26") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C26") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 27
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_27") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_27") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C27") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 28
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_28") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_28") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C28") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 29
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_29") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_29") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C29") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 30
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_30") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_30") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C30") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                Case 31
                                    If ddlview.SelectedItem.Value.Trim = "0" Then
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_31") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                                    Else
                                        objEmpDs.Tables(0).Rows(intCnt).Item("_31") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("Hk"))
                                    End If
                                    objEmpDs.Tables(0).Rows(intCnt).Item("_C31") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                            End Select
                        End If
                    Next
                Next
            End If

        End If
        Return objEmpDs
    End Function



    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                lblCurrentIndex.Text = 0
            Case "prev"
                lblCurrentIndex.Text = _
                    Math.Max(0, lblCurrentIndex.Text - 1)
            Case "next"
                lblCurrentIndex.Text = _
                    Math.Min(lblPageCount.Text - 1, lblCurrentIndex.Text + 1)
            Case "last"
                lblCurrentIndex.Text = lblPageCount.Text - 1
        End Select
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        BindGrid()
    End Sub

    Sub dgLineBindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")

            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If

        End If
    End Sub

    Sub OnCommand_Redirect(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strCmdArgs As String = E.CommandArgument
        'Dim strDay As String = Mid(strCmdArgs, 2, Len(strCmdArgs) - 1)
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

        lbl = dgLine.Items.Item(intIndex).FindControl("lblEmpCode")
        strEmpCode = lbl.Text
        lbl = dgLine.Items.Item(intIndex).FindControl("lblEmpName")
        strEmpName = lbl.Text

        lbl = dgLine.Items.Item(intIndex).FindControl("lblEmpType")
        strEmpTy = lbl.Text

        Dim mdr As String

        If ddlmandor.Text <> "" Then
            mdr = ddlmandor.SelectedItem.Value.Trim()
        Else
            mdr = ""
        End If

        Session("ATT") = ddlMonth.SelectedItem.Value.Trim() & "|" & _
                          ddlyear.SelectedItem.Value.Trim() & "|" & _
                          txtEmpCode.Text.Trim() & "|" & _
                          txtEmpName.Text.Trim() & "|" & _
                          ddlEmpDiv.SelectedItem.Value.Trim() & "|" & _
                          mdr & "|" & _
                          ddlEmpType.SelectedItem.Value.Trim() & "|" & _
                          ddlview.SelectedItem.Value.Trim() & "|" & _
                          lblCurrentIndex.Text


        Response.Write("<Script Language=""JavaScript"">pop_Att=window.open(""PR_trx_DailyAttdDet_Estate.aspx?redirect=attm&EmpCode=" & strEmpCode & "&Attdate=" & strDate & "&EmpName=" & strEmpName & "&EmpTy=" & strEmpTy & _
                               "&AccMonth=" & strAMonth & "&AccYear=" & strYear & "&AttTy=" & a & "&HdrDiv=" & ddlEmpDiv.SelectedItem.Value.Trim() & """, null ,""'pop_Att',width=650,height=320,top=230,left=200,status=yes, resizable=no, scrollbars=yes, toolbar=no, location=no"");pop_Att.focus();</Script>")

    End Sub

    Sub AttdBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_Trx_DailyAttd_Estate.aspx")
    End Sub

    Sub RefrehBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        srchBtn_Click(Sender, E)
    End Sub

    Sub GenerateBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim AMonth As String = ddlMonth.SelectedItem.Value
        Dim strAMonth As String
        Dim strYear As String = ddlyear.SelectedItem.Value.Trim

        If AMonth < 10 Then
            strAMonth = "0" & AMonth
        Else
            strAMonth = AMonth
        End If

        Dim mdr As String

        If ddlmandor.Text <> "" Then
            mdr = ddlmandor.SelectedItem.Value.Trim()
        Else
            mdr = ""
        End If

        Session("ATT") = ddlMonth.SelectedItem.Value.Trim() & "|" & _
                          ddlyear.SelectedItem.Value.Trim() & "|" & _
                          txtEmpCode.Text.Trim() & "|" & _
                          txtEmpName.Text.Trim() & "|" & _
                          ddlEmpDiv.SelectedItem.Value.Trim() & "|" & _
                          mdr & "|" & _
                          ddlEmpType.SelectedItem.Value.Trim() & "|" & _
                          ddlview.SelectedItem.Value.Trim() & "|" & _
                          lblCurrentIndex.Text

        Response.Write("<Script Language=""JavaScript"">pop_gen=window.open(""PR_trx_DailyAttdGenerate_Estate.aspx?redirect=attm&AccMonth=" & strAMonth & "&AccYear=" & strYear & """, null ,""'pop_gen',width=400,height=300,top=230,left=300,status=yes, resizable=no, scrollbars=yes, toolbar=no, location=no"");pop_gen.focus();</Script>")
    End Sub

    'Sub ddlEmpDiv_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    If ddlEmpDiv.SelectedItem.Value.Trim() <> " " Then
    '        BindMandoran(ddlEmpDiv.SelectedItem.Value.Trim())
    '    End If
    'End Sub
	Sub ExpBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) 
		Dim mdr As String

        If ddlmandor.Text <> "" Then
            mdr = ddlmandor.SelectedItem.Value.Trim()
        Else
            mdr = ""
        End If
		Session("ATT") = ddlMonth.SelectedItem.Value.Trim() & "|" & _
                          ddlyear.SelectedItem.Value.Trim() & "|" & _
                          txtEmpCode.Text.Trim() & "|" & _
                          txtEmpName.Text.Trim() & "|" & _
                          ddlEmpDiv.SelectedItem.Value.Trim() & "|" & _
                          mdr & "|" & _
                          ddlEmpType.SelectedItem.Value.Trim() & "|" & _
                          ddlview.SelectedItem.Value.Trim() & "|" & _
                          lblCurrentIndex.Text
  	   
	   Response.Write("<Script Language=""JavaScript"">pop_Att=window.open(""PR_trx_FingerException_Estate.aspx?"", null ,""'pop_exp',width=850,height=500,top=100,left=100,status=yes, resizable=no, scrollbars=yes, toolbar=no, location=no"");pop_Att.focus();</Script>")
    End Sub
	
	Sub ScanBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) 
		Dim mdr As String

        If ddlmandor.Text <> "" Then
            mdr = ddlmandor.SelectedItem.Value.Trim()
        Else
            mdr = ""
        End If
		Session("ATT") = ddlMonth.SelectedItem.Value.Trim() & "|" & _
                          ddlyear.SelectedItem.Value.Trim() & "|" & _
                          txtEmpCode.Text.Trim() & "|" & _
                          txtEmpName.Text.Trim() & "|" & _
                          ddlEmpDiv.SelectedItem.Value.Trim() & "|" & _
                          mdr & "|" & _
                          ddlEmpType.SelectedItem.Value.Trim() & "|" & _
                          ddlview.SelectedItem.Value.Trim() & "|" & _
                          lblCurrentIndex.Text
  	   
	   Response.Write("<Script Language=""JavaScript"">pop_Att=window.open(""PR_trx_FingerScan_Estate.aspx?"", null ,""'pop_scan',width=850,height=500,top=100,left=100,status=yes, resizable=no, scrollbars=yes, toolbar=no, location=no"");pop_Att.focus();</Script>")
    End Sub


End Class
