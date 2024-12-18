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

Public Class PM_STDRPT_SELECTION_CTRL : Inherits UserControl
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdmin As New agri.Admin.clsShare()
    'Dim objIN As New agri.IN.clsReport()

    Protected WithEvents lblErrMessage As Label
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
    Protected WithEvents TrFromTo As HtmlTableRow

    Dim dsForDropDown As New DataSet()
    Dim objUserLoc As New DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    'Dim intCTAR As Integer
    Dim strUserLoc As String

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim dr As DataRow
    Dim intSelIndex As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntDec As Integer

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")

        TrMthYr.Visible = False
        TrFromTo.Visible = False
        If Not Page.IsPostBack Then
            GetUserLoc()
            'GetAccPeriod()
            BindReportNameList()
            BindAccMonthList()
            BindAccYearList()

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
            LocCheck()
            lblUserLoc.Visible = False
        End If
    End Sub

    Sub BindAccMonthList()
        'Dim intCntddlMth As Integer

        'For intCntddlMth = 0 To lstAccMonth.Items.Count - 1

        'If lstAccMonth.Items(intCntddlMth).Value <> 1 Then
        'If lstAccMonth.Items(intCntddlMth).Value = strAccMonth - 1 Then
        '    Trace.Warn(strAccMonth, "strAccMonth")

        '    lstAccMonth.SelectedIndex = intCntddlMth
        'End If
        'Else
        '    lstAccMonth.SelectedIndex = 11
        'End If
        'Next

        If strAccMonth = 1 Then
            lstAccMonth.SelectedIndex = 11
        Else
            lstAccMonth.SelectedIndex = strAccMonth - 2
        End If

    End Sub

    Sub BindAccYearList()
        Dim CurrDate As Date
        Dim CurrYear As Integer
        Dim intCntAddYr As Integer = 1
        Dim intCntMinYr As Integer = 5
        Dim NewAddCurrYear As Integer
        Dim NewMinCurrYear As Integer
        Dim intCntddlYr As Integer = 0

        CurrDate = Today
        CurrYear = Year(CurrDate)

        While intCntMinYr <> 0
            intCntMinYr = intCntMinYr - 1
            NewMinCurrYear = CurrYear - intCntMinYr
            lstAccYear.Items.Add(NewMinCurrYear)
        End While

        For intCntAddYr = 1 To 5
            NewAddCurrYear = CurrYear + intCntAddYr
            lstAccYear.Items.Add(NewAddCurrYear)
        Next

        For intCntddlYr = 0 To lstAccYear.Items.Count - 1
            If strAccMonth = 1 Then
                If lstAccYear.Items(intCntddlYr).Text = strAccYear - 1 Then
                    lstAccYear.SelectedIndex = intCntddlYr
                End If
            Else
                If lstAccYear.Items(intCntddlYr).Text = strAccYear Then
                    lstAccYear.SelectedIndex = intCntddlYr
                End If
            End If
        Next

        'If strAccMonth = 1 Then
        '    lstAccYear.SelectedIndex = CInt(strAccYear) - 1
        'Else
        '    lstAccYear.SelectedIndex = strAccYear - 2
        'End If
    End Sub

    '-------Get User Location --------------
    Sub GetUserLoc()
        Dim strParam As String
        Dim objMapPath As String
        Dim strUserLoc As String
        Dim arrParam As Array
        Dim intCnt2 As Integer
        Dim intCnt3 As Integer

        Dim strArrUserLoc As String
        Dim strOppCd_UserLoc_GET As String = "IN_STDRPT_USERLOCATION_GET"

        strParam = "AND USERLOC.UserID = '" & strUserId & "'"
        Try
            intErrNo = objAdmin.mtdGetUserLocation(strOppCd_UserLoc_GET, strParam, objUserLoc, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_PM_SELECTIONCTRL_USERLOCATION&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/WM_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To objUserLoc.Tables(0).Rows.Count - 1
            objUserLoc.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objUserLoc.Tables(0).Rows(intCnt).Item("LocCode"))
        Next intCnt

        'dr = objUserLoc.Tables(0).NewRow()
        'dr("LocCode") = "All"
        ''dr("Description") = ""
        'objUserLoc.Tables(0).Rows.InsertAt(dr, 0)

        lblLocation.Visible = True
        cblUserLoc.DataSource = objUserLoc.Tables(0)
        cblUserLoc.DataValueField = "LocCode"
        'cblUserLoc.DataTextField = "Description"
        cblUserLoc.DataBind()

        hidUserLoc.Value = Request.QueryString("UserLoc")
        strUserLoc = Request.QueryString("UserLoc")
        If Left(strUserLoc, 3) = "','" Then
            arrParam = Split(strUserLoc, "','")
        ElseIf Right(strUserLoc, 1) = "," Then
            arrParam = Split(strUserLoc, ",")
        Else
            arrParam = Split(strUserLoc, ",")
        End If

        'arrParam = Split(strUserLoc, "','")
        'response.write("<BR>strUserLoc"& strUserLoc)
        'response.write("<BR>"& cblUserLoc.Items(intcnt2).value &" >> "&arrParam(1))
        If Not hidUserLoc.Value = "" Then
            For intCnt2 = 0 To cblUserLoc.Items.Count - 1
                For intCnt3 = 0 To arrParam.GetUpperBound(0)
                    If Trim(cblUserLoc.Items(intCnt2).Value) = Trim(arrParam(intCnt3)) Then
                        'response.write("<BR>"& cblUserLoc.Items(intcnt2).value &" === "&arrParam(intcnt3))
                        cblUserLoc.Items(intCnt2).Selected = True
                    End If
                Next intCnt3
            Next intCnt2
            'Else
            'lblUserLoc.Visible = True
            'Exit Sub
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

        'For intCnt2 = 0 To cblUserLoc.Items.Count - 1
        '    If cblUserLoc.Items(0).Selected Then
        '        cblUserLoc.Items(intCnt2).Selected = True
        '    Else
        '        If Not cblUserLoc.Items(intCnt2).Value = strLocation Then
        '            cblUserLoc.Items(intCnt2).Selected = False
        '        End If
        '    End If
        'Next

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
        'If Not hidUserLoc.Value = "" Then
        '    'get the textbox that the user location should be written to
        '    txt = Me.Parent.FindControl("hidUserLocPX")
        '    'Write value to appropriate textbox
        '    txt.Value = tempUserLoc
        'End If
        'GetAccPeriod()
    End Sub

    '-------Create Report Name Dropdownlist--------------
    Sub BindReportNameList()
        Dim strParam As String
        Dim objMapPath As String

        Dim strOppCd_StdRptName_GET As String = "IN_STDRPT_NAME_GET"
'----- #1 -----
        'strParam = " WHERE ReportType = '" & objGlobal.EnumStdRptType.MillProduction & "' AND Status = '" & objGlobal.EnumStdRptStatus.Active & "' ORDER BY RptName"
        'If Session("PW_ISMILLWARE") = True Then

        'If 
        'strParam = " WHERE (ReportType = '" & objGlobal.EnumStdRptType.Production & "' OR ReportType = '" & objGlobal.EnumStdRptType.MillProduction & "') AND Status = '" & objGlobal.EnumStdRptStatus.Active & "' ORDER BY RptName"
        'Else
        strParam = " WHERE ReportType = '" & objGlobal.EnumStdRptType.MillProduction & "' AND Status = '" & objGlobal.EnumStdRptStatus.Active & "'   AND Reportid IN (Select ReportID From SH_REPORT_USER Where UserId='" & strUserId & "') ORDER BY RptName"
        'End If
        '----- #1 -----
        Try
            intErrNo = objAdmin.mtdGetStdRptName(strOppCd_StdRptName_GET, strParam, dsForDropDown, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_PM_SELECTIONCTRL_REPORT_NAME_LIST&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("ReportID") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("ReportID"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("RptName") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("RptName"))
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("ReportID") = ""
        dr("RptName") = "Select Report Name"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstRptname.DataSource = dsForDropDown.Tables(0)
        lstRptname.DataValueField = "ReportID"
        lstRptname.DataTextField = "RptName"
        lstRptname.DataBind()
    End Sub


    Sub CheckRptName(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strRptname As String = Trim(lstRptname.SelectedItem.Value)
        Dim strSelectedIndex As String = LCase(lstRptname.SelectedItem.Value)
        Dim intSelectedIndex As Integer = lstRptname.SelectedIndex
        Dim strUserLoc As String
        'Dim strDateFrom As String
        'Dim strDateTo As String
        'Dim strDec As String

        'strDateFrom = txtDateFrom.Text
        'strDateTo = txtDateTo.Text
        strUserLoc = hidUserLoc.Value
        'strDec = lstDecimal.SelectedItem.Value

        'If hidUserLoc.Value = "" Then
        '    lblUserLoc.Visible = True
        '    Exit Sub
        'End If
        lblErrMessage.Text = strSelectedIndex
        lblErrMessage.Visible = True
'----- #1 -----
        If strSelectedIndex = "rptpd1000001" Then
            Response.Redirect("../../" & strLangCode & "/reports/PD_StdRpt_OilPalmYieldStatList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000001" Then
'----- #1 -----
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_DailyProdReport.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000002" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_MonthlyProdSummary.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000003" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_UllageVolumeTableMasterList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000004" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_UllageVolumeConversionMasterList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000005" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_UllageAverageCapacityConversionMasterList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000006" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_CPOPropertiesMasterList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000007" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_StorageTypeMasterList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000008" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_StorageTypeFormulaList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000009" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_StorageAreaMasterList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000010" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_StorageAreaFormulaList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000011" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_CPOStorageTransactionList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000012" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_PKStorageTransactionList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000013" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_OilLossTransactionList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000014" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_OilQualityTransactionList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000015" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_KernelQualityTransactionList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000016" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_ProducedKernelQualityTransactionList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000017" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_DispatchedKernelQualityTransactionList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpd1000002" Then
            Response.Redirect("../../" & strLangCode & "/reports/PD_StdRpt_OilPalmList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)        
        'CR_THG00009 START
        ElseIf strSelectedIndex = "rptpm1000018" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_DailyOilLoss.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        'CR_THG00009 END
        'dian start
        ElseIf strSelectedIndex = "rptpd1000003" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_ProdStat.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
            'dian end
        ElseIf strSelectedIndex = "rptpm1000023" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_KernelLossesTransactionList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
	ElseIf strSelectedIndex = "rptpm1000025" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_DailyWaterQuality.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)

        ElseIf strSelectedIndex = "rptpm1000026" Then
            Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_MonthlyRecordProd.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpm1000027" Then            
                Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_GrafikProdReportRend_PK.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
 	ElseIf strSelectedIndex = "rptpm1000028" Then            
                Response.Redirect("../../" & strLangCode & "/reports/PM_StdRpt_GrafikProdReportRend_CPO.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
	End If


    End Sub
End Class

