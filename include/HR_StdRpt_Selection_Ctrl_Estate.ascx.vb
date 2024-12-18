Imports System
Imports System.Data
Imports System.Collections
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic

Public Class HR_STDRPT_SELECTION_CTRL : Inherits UserControl

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdmin As New agri.Admin.clsShare()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblUserLoc As Label
    Protected WithEvents lstRptname As DropDownList
    Protected WithEvents lstDecimal As DropDownList
    Protected WithEvents cbLocAll As CheckBox
    Protected WithEvents cblUserLoc As CheckBoxList
    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents btnSelDateTo As Image
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents hidUserLoc As HtmlInputHidden
    Protected WithEvents TrMthYr As HtmlTableRow
    Protected WithEvents TRDocDateFromTo As HtmlTableRow
    Dim strCPTag As String
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserLoc As String
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim dr As DataRow
    Dim intSelIndex As Integer
    'add by alim
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    'End of Add by alim


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntDec As Integer

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                GetUserLoc()        '--bind all locations authorised by user
                BindReportNameList() '--bind all module reports
                intSelIndex = Request.QueryString("SelIndex")
                lstRptname.SelectedIndex = intSelIndex

                lstDecimal.SelectedIndex = 2
                If Not Request.QueryString("Dec") = "" Then
                    For intCntDec = 0 To lstDecimal.Items.Count - 1
                        If lstDecimal.Items(intCntDec).Value = Request.QueryString("Dec") Then
                            lstDecimal.SelectedIndex = intCntDec
                        End If
                    Next
                End If
            Else
                lblUserLoc.Visible = False
            End If
		End If
    End Sub

    '-------Get User Location --------------
    Sub GetUserLoc()
        Dim strParam As String
        Dim objMapPath As String
        Dim strUserLoc As String
        Dim arrParam As Array
        Dim intCnt2 As Integer
        Dim intCnt3 As Integer
        Dim objUserLoc As New DataSet()
        Dim strArrUserLoc As String
        Dim strOppCd_UserLoc_GET As String = "HR_STDRPT_USERLOCATION_GET"

        Try
            strParam = "AND USERLOC.UserID = '" & strUserId & "'"
            intErrNo = objAdmin.mtdGetUserLocation(strOppCd_UserLoc_GET, strParam, objUserLoc, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_HR_SELECTIONCTRL_USERLOCATION&errmesg=" & Exp.ToString() & "&redirect=../en/reports/HR_StdRpt_Selection.aspx")
        End Try

        lblLocation.Visible = True
        cblUserLoc.DataSource = objUserLoc.Tables(0)
        cblUserLoc.DataValueField = "LocCode"
        cblUserLoc.DataBind()

        objUserLoc = Nothing

        hidUserLoc.Value = Request.QueryString("UserLoc")
        strUserLoc = Request.QueryString("UserLoc")
        If Left(strUserLoc, 3) = "','" Then
            arrParam = Split(strUserLoc, "','")
        ElseIf Right(strUserLoc, 1) = "," Then
            arrParam = Split(strUserLoc, ",")
        Else
            arrParam = Split(strUserLoc, ",")
        End If

        If Not hidUserLoc.Value = "" Then
            For intCnt2 = 0 To cblUserLoc.Items.Count - 1
                For intCnt3 = 0 To arrParam.GetUpperBound(0)
                    If Trim(cblUserLoc.Items(intCnt2).Value) = Trim(arrParam(intCnt3)) Then
                        cblUserLoc.Items(intCnt2).Selected = True
                    End If
                Next intCnt3
            Next intCnt2
        End If
    End Sub

    Sub Check_Clicked(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntLocAll As Integer = 0

        If cbLocAll.Checked Then
            For intCntLocAll = 0 To cblUserLoc.Items.Count - 1
                cblUserLoc.Items(intCntLocAll).Selected = True
            Next
        Else
            For intCntLocAll = 0 To cblUserLoc.Items.Count - 1
                cblUserLoc.Items(intCntLocAll).Selected = False
            Next
        End If
        LocCheck()
    End Sub

    Sub LocCheckList(ByVal Sender As Object, ByVal E As EventArgs)
        LocCheck()
    End Sub

    '-------Check User Selection on Location--------------
    Sub LocCheck()
        Dim intCnt2 As Integer = 0
        Dim tempUserLoc As String
        Dim txt As HtmlInputHidden

        For intCnt2 = 0 To cblUserLoc.Items.Count - 1
            If cblUserLoc.Items(intCnt2).Selected Then
                If cblUserLoc.Items.Count = 1 Then
                    tempUserLoc = cblUserLoc.Items(intCnt2).Text
                Else
                    tempUserLoc = tempUserLoc & "','" & cblUserLoc.Items(intCnt2).Text
                End If
            End If
        Next

        hidUserLoc.Value = tempUserLoc
    End Sub

    '-------Create Report Name Dropdownlist--------------
    Sub BindReportNameList()
        Dim strParam As String
        Dim objMapPath As String
        Dim dsForDropDown As New DataSet()
        Dim strOppCd_StdRptName_GET As String = "HR_STDRPT_NAME_GET"
        Dim intCnt As Integer

        strParam = " WHERE ReportType = '" & Convert.ToString(objGlobal.EnumStdRptType.HumanResource) & "' AND Status = '" & Convert.ToString(objGlobal.EnumStdRptStatus.Active) & "' ORDER BY RptName"
        Try
            intErrNo = objAdmin.mtdGetStdRptName(strOppCd_StdRptName_GET, strParam, dsForDropDown, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_HR_SELECTIONCTRL_REPORT_NAME_LIST&errmesg=" & Exp.ToString() & "redirect=../en/reports/HR_StdRpt_Selection.aspx")
        End Try

        If dsForDropDown.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
                If dsForDropDown.Tables(0).Rows(intCnt).Item("ReportID").Trim = "RPTHR1000007" Then
                    dsForDropDown.Tables(0).Rows(intCnt).Item("RptName") = "Employee " & strCPTag
                ElseIf dsForDropDown.Tables(0).Rows(intCnt).Item("ReportID").Trim = "RPTHR1000008" Then
                    dsForDropDown.Tables(0).Rows(intCnt).Item("RptName") = strCPTag & " Listing"
                End If
            Next
        End If

        dr = dsForDropDown.Tables(0).NewRow()
        dr("ReportID") = ""
        dr("RptName") = "Select Report Name"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstRptname.DataSource = dsForDropDown.Tables(0)
        lstRptname.DataValueField = "ReportID"
        lstRptname.DataTextField = "RptName"
        lstRptname.DataBind()
        dsForDropDown = Nothing
    End Sub


    Sub CheckRptName(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strRptName As String = Trim(lstRptname.SelectedItem.Text)
        Dim strSelectedIndex As String = LCase(lstRptname.SelectedItem.Value)
        Dim intSelectedIndex As Integer = lstRptname.SelectedIndex
        Dim strUserLoc As String

        strUserLoc = hidUserLoc.Value

        If strSelectedIndex = "rpthr1000009" Then ' laporan penerimaan karyawan
            Response.Redirect("../../en/reports/HR_StdRpt_Penerimaan_Kary_Estate.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rpthr1000010" Then ' laporan mutasi karyawan
            Response.Redirect("../../en/reports/HR_StdRpt_Mutasi_Kary_Estate.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rpthr1000011" Then ' laporan berhenti karyawan
            Response.Redirect("../../en/reports/HR_StdRpt_Berhenti_Kary_Estate.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rpthr1000012" Then ' laporan profile karyawan
            Response.Redirect("../../en/reports/HR_StdRpt_Profile_Kary_Estate.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rpthr1000013" Then ' laporan detail karyawan
            Response.Redirect("../../en/reports/HR_StdRpt_EmployeeDetail_Estate.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        End If
    End Sub

    '--Start #1
    Sub onload_GetLangCap()
        GetEntireLangCap()
        strCPTag = GetCaption(objLangCap.EnumLangCap.CareerProgress)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_SELECTCTRL_CLSLANGCAP_BUSSTERM_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

    End Sub

    'Function GetCaption(ByVal pv_TermCode) As String
    '    Dim count As Integer
    '
    '    For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
    '        If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
    '            Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
    '            Exit For
    '        End If
    '    Next
    'End Function

    'add by alim
    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                'Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                Exit For
            End If
        Next
    End Function
    'End of Add by alim

    '--End #1
End Class

