Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services

Public Class HR_StdRpt_EmployeeDetail : Inherits Page

    Protected RptSelect As UserControl
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents txtEmployeeCodeFrom As TextBox
    Protected WithEvents txtEmployeeCodeTo As TextBox
    Protected WithEvents txtEmployeeName As TextBox
    Protected WithEvents txtGangCode As TextBox
    Protected WithEvents lstGender As DropDownList
    Protected WithEvents lstMaritalStatus As DropDownList
    Protected WithEvents lstNationality As DropDownList
    Protected WithEvents lstRace As DropDownList
    Protected WithEvents lstReligion As DropDownList
    Protected WithEvents txtDOBFrom As TextBox
    Protected WithEvents txtDOBTo As TextBox
    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label

    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindGender()
                BindMaritalStatus()
                BindNationality()
                BindRace()
                BindReligion()
                BindStatus()
            End If
        End If

    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        Dim ucTrDecimal As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = False

        ucTrDecimal = RptSelect.FindControl("TrDecimal")
        ucTrDecimal.Visible = False

    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)

    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_STDRPT_MPOBPRICE_LIST_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/HR_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strEmployeeCodeFrom As String
        Dim strEmployeeCodeTo As String
        Dim strEmployeeName As String
        Dim strGangCode As String
        Dim strGender As String
        Dim strMaritalStatus As String
        Dim strNationality As String
        Dim strRace As String
        Dim strReligion As String
        Dim strDOBFrom As String
        Dim strDOBTo As String
        Dim strStatus As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim tempRpt As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strParam As String
        Dim strDateSetting As String

        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)

        If txtDOBFrom.Text = "" Then
            strDOBFrom = ""
        Else
            strDOBFrom = Trim(txtDOBFrom.Text)
        End If

        If txtDOBTo.Text = "" Then
            strDOBTo = ""
        Else
            strDOBTo = Trim(txtDOBTo.Text)
        End If

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=HR_STDRPT_EMPLOYEEDETAIL_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/HR_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strDOBFrom = "" And strDOBTo = "") Then
            If Not (objGlobal.mtdValidInputDate(strDateSetting, strDOBFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDOBTo, objDateFormat, objDateTo) = True) Then
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
                Exit Sub
            End If
        End If

        If strUserLoc = "" Then
            templblUL = RptSelect.FindControl("lblUserLoc")
            templblUL.Visible = True
            Exit Sub
        Else
            If Left(strUserLoc, 3) = "','" Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
            ElseIf Right(strUserLoc, 3) = "','" Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
            End If
        End If

        If txtEmployeeCodeFrom.Text = "" Then
            strEmployeeCodeFrom = ""
        Else
            strEmployeeCodeFrom = Trim(txtEmployeeCodeFrom.Text)
        End If

        If txtEmployeeCodeTo.Text = "" Then
            strEmployeeCodeTo = ""
        Else
            strEmployeeCodeTo = Trim(txtEmployeeCodeTo.Text)
        End If

        If txtEmployeeName.Text = "" Then
            strEmployeeName = ""
        Else
            strEmployeeName = Trim(txtEmployeeName.Text)
        End If

        If txtGangCode.Text = "" Then
            strGangCode = ""
        Else
            strGangCode = Trim(txtGangCode.Text)
        End If

        If lstGender.SelectedItem.Value = "All" Then
            strGender = ""
        Else
            strGender = Trim(lstGender.SelectedItem.Value)
        End If

        If lstMaritalStatus.SelectedItem.Value = "All" Then
            strMaritalStatus = ""
        Else
            strMaritalStatus = Trim(lstMaritalStatus.SelectedItem.Value)
        End If

        If lstNationality.SelectedItem.Value = "All" Then
            strNationality = ""
        Else
            strNationality = Trim(lstNationality.SelectedItem.Value)
        End If

        If lstRace.SelectedItem.Value = "All" Then
            strRace = ""
        Else
            strRace = Trim(lstRace.SelectedItem.Value)
        End If

        If lstReligion.SelectedItem.Value = "All" Then
            strReligion = ""
        Else
            strReligion = Trim(lstReligion.SelectedItem.Value)
        End If

        If lstStatus.SelectedItem.Value = "All" Then
            strStatus = ""
        Else
            strStatus = Trim(lstStatus.SelectedItem.Value)
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""HR_StdRpt_EmployeeDetailPreview.aspx?CompCode=" & strCompany & _
                       "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                       "&lblLocation=" & lblLocation.Text & "&EmployeeCodeFrom=" & strEmployeeCodeFrom & "&EmployeeCodeTo=" & strEmployeeCodeTo & _
                       "&EmployeeName=" & strEmployeeName & "&GangCode=" & strGangCode & "&Gender=" & strGender & _
                       "&MaritalStatus=" & strMaritalStatus & "&Nationality=" & strNationality & "&Race=" & strRace & _
                       "&Religion=" & strReligion & "&DOBFrom=" & objDateFrom & "&DOBTo=" & objDateTo & _
                       "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub BindGender()
        Dim strText = "All"

        lstGender.Items.Add(New ListItem(strText))
        lstGender.Items.Add(New ListItem(objHRTrx.mtdGetSex(objHRTrx.EnumSex.Male), objHRTrx.EnumSex.Male))
        lstGender.Items.Add(New ListItem(objHRTrx.mtdGetSex(objHRTrx.EnumSex.Female), objHRTrx.EnumSex.Female))

    End Sub

    Sub BindMaritalStatus()
        Dim strText = "All"

        lstMaritalStatus.Items.Add(New ListItem(strText))
        lstMaritalStatus.Items.Add(New ListItem(objHRTrx.mtdGetMarital(objHRTrx.EnumMarital.Singled), objHRTrx.EnumMarital.Singled))
        lstMaritalStatus.Items.Add(New ListItem(objHRTrx.mtdGetMarital(objHRTrx.EnumMarital.Married), objHRTrx.EnumMarital.Married))
        lstMaritalStatus.Items.Add(New ListItem(objHRTrx.mtdGetMarital(objHRTrx.EnumMarital.Divorced), objHRTrx.EnumMarital.Divorced))

    End Sub

    Sub BindNationality()

        Dim strOpCd_Nation As String = "HR_CLSSETUP_NATION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim objNationDs As New Object()

        strParam = "|" & "AND NA.Status LIKE '" & objHRSetup.EnumNationalityStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Nation, strParam, objHRSetup.EnumHRMasterType.Nationality, objNationDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_NATION&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objNationDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objNationDs.Tables(0).Rows.Count - 1
                objNationDs.Tables(0).Rows(intCnt).Item("CountryCode") = Trim(objNationDs.Tables(0).Rows(intCnt).Item("CountryCode"))
                objNationDs.Tables(0).Rows(intCnt).Item("CountryDesc") = Trim(objNationDs.Tables(0).Rows(intCnt).Item("CountryCode")) & " (" & _
                                                                         Trim(objNationDs.Tables(0).Rows(intCnt).Item("CountryDesc")) & ")"
            Next
        End If

        dr = objNationDs.Tables(0).NewRow()
        dr("CountryCode") = ""
        dr("CountryDesc") = "All"
        objNationDs.Tables(0).Rows.InsertAt(dr, 0)

        lstNationality.DataSource = objNationDs.Tables(0)
        lstNationality.DataTextField = "CountryDesc"
        lstNationality.DataValueField = "CountryCode"
        lstNationality.DataBind()

    End Sub

    Sub BindRace()

        Dim strOpCd_Race As String = "HR_CLSSETUP_RACE_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim objRaceDs As New Object()

        strParam = "|" & "AND RA.Status LIKE '" & objHRSetup.EnumRaceStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Race, strParam, objHRSetup.EnumHRMasterType.Race, objRaceDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_RACE&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objRaceDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRaceDs.Tables(0).Rows.Count - 1
                objRaceDs.Tables(0).Rows(intCnt).Item("RaceCode") = Trim(objRaceDs.Tables(0).Rows(intCnt).Item("RaceCode"))
                objRaceDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objRaceDs.Tables(0).Rows(intCnt).Item("RaceCode")) & " (" & _
                                                                       Trim(objRaceDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objRaceDs.Tables(0).NewRow()
        dr("RaceCode") = ""
        dr("Description") = "All"
        objRaceDs.Tables(0).Rows.InsertAt(dr, 0)

        lstRace.DataSource = objRaceDs.Tables(0)
        lstRace.DataTextField = "Description"
        lstRace.DataValueField = "RaceCode"
        lstRace.DataBind()

    End Sub

    Sub BindReligion()

        Dim strOpCd_Religion As String = "HR_CLSSETUP_RELIGION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim objReligionDs As New Object()

        strParam = "|" & "AND REL.Status LIKE '" & objHRSetup.EnumReligionStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd_Religion, strParam, objHRSetup.EnumHRMasterType.Religion, objReligionDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_RELIGION&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objReligionDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objReligionDs.Tables(0).Rows.Count - 1
                objReligionDs.Tables(0).Rows(intCnt).Item("ReligionCode") = Trim(objReligionDs.Tables(0).Rows(intCnt).Item("ReligionCode"))
                objReligionDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objReligionDs.Tables(0).Rows(intCnt).Item("ReligionCode")) & " (" & _
                                                                           Trim(objReligionDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objReligionDs.Tables(0).NewRow()
        dr("ReligionCode") = ""
        dr("Description") = "All"
        objReligionDs.Tables(0).Rows.InsertAt(dr, 0)

        lstReligion.DataSource = objReligionDs.Tables(0)
        lstReligion.DataTextField = "Description"
        lstReligion.DataValueField = "ReligionCode"
        lstReligion.DataBind()

    End Sub

    Sub BindStatus()

        lstStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.All), objHRTrx.EnumEmpStatus.All))
        lstStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.Active), objHRTrx.EnumEmpStatus.Active))
        lstStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.Deleted), objHRTrx.EnumEmpStatus.Deleted))
        lstStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.Pending), objHRTrx.EnumEmpStatus.Pending))
        lstStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.Terminated), objHRTrx.EnumEmpStatus.Terminated))

    End Sub

End Class
