'***********************************************************************************************************
'No.    Author  Date            Remarks                                                             Patch
'***********************************************************************************************************
'1.     GCH     03 Feb 2005     CR_TW00026 Workshop Reinvent
'************************************************* END *****************************************************
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
Imports agri.GlobalHdl.clsGlobalHdl

Public Class WS_STDRPT_SELECTION_CTRL : Inherits UserControl
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objWS As New agri.WS.clsReport()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()

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
    Protected WithEvents lstAccMonthTo As DropDownList
    Protected WithEvents lstAccYearTo As DropDownList

    Protected WithEvents hidUserLoc As HtmlInputHidden

    Protected WithEvents TrMthYr As HtmlTableRow
    Protected WithEvents TrFromTo As HtmlTableRow

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

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntDec As Integer

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")

        TrMthYr.Visible = False
        TrFromTo.Visible = False
        If Not Page.IsPostBack Then
            GetUserLoc()        '--bind all locations authorised to user
            BindReportNameList()'--bind all module reports
            BindAccMonthList(BindAccYearList(strLocation, strAccYear, True))  '--from accounting month
            BindAccMonthToList(BindAccYearList(strLocation, strAccYear, False)) '--to accounting month (certain reports are in used)

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
    End Sub

    Sub BindAccMonthList(ByVal pv_intMaxMonth As Integer)
        Dim intCnt As Integer
        Dim intSelIndex As Integer = 0

        lstAccMonth.Items.Clear
        For intCnt = 1 To pv_intMaxMonth
            lstAccMonth.Items.Add(intCnt)
            If intCnt = Convert.ToInt16(strAccMonth) Then
                intSelIndex = intCnt - 1
            End If
        Next

        lstAccMonth.SelectedIndex = intSelIndex
    End Sub

    Function BindAccYearList(ByVal pv_strLocation As String, _
                             ByVal pv_strAccYear As String, _
                             ByVal pv_blnIsFrom As Boolean) As Integer
        Dim strOpCd_Max_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ALLLOC_MAXPERIOD_GET"
        Dim strOpCd_Dist_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_DISTINCT_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intAccYear As Integer
        Dim intMaxPeriod As Integer
        Dim intCnt As Integer
        Dim intSelIndex As Integer
        Dim objAccCfg As New Dataset()

        If pv_strLocation = "" Then
            pv_strLocation = strLocation
        Else
            If Left(pv_strLocation, 3) = "','" Then
                pv_strLocation = Right(pv_strLocation, Len(pv_strLocation) - 3)
            ElseIf Right(pv_strLocation, 3) = "','" Then
                pv_strLocation = Left(pv_strLocation, Len(pv_strLocation) - 3)
            ElseIf Left(pv_strLocation, 1) = "," Then
                pv_strLocation = Right(pv_strLocation, Len(pv_strLocation) - 1)
            ElseIf Right(pv_strLocation, 1) = "," Then
                pv_strLocation = Left(pv_strLocation, Len(pv_strLocation) - 1)
            End If
        End If

        Try
            '--to find highest period of all locations given
            strParam = "||"
            intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_Dist_Get, _
                                                    strCompany, _
                                                    pv_strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objAccCfg)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_STDRPT_CTRL_ACCCFG_DIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        intSelIndex = 0 
        If pv_blnIsFrom = True Then
            lstAccYear.Items.Clear
        Else
            lstAccYearTo.Items.Clear
        End If

        If objAccCfg.Tables(0).Rows.Count > 0 Then      '--found year's record
            For intCnt = 0 To objAccCfg.Tables(0).Rows.Count - 1    '--bind accounting year dropdownlist
                If pv_blnIsFrom = True Then
                    lstAccYear.Items.Add(objAccCfg.Tables(0).Rows(intCnt).Item("AccYear"))
                Else
                    lstAccYearTo.Items.Add(objAccCfg.Tables(0).Rows(intCnt).Item("AccYear"))
                End If

                If objAccCfg.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                    intSelIndex = intCnt    '--selected index for accounting year dropdownlist
                End If
            Next

            If pv_blnIsFrom = True Then
                lstAccYear.SelectedIndex = intSelIndex
                intAccYear = lstAccYear.SelectedItem.Value
            Else
                lstAccYearTo.SelectedIndex = intSelIndex
                intAccYear = lstAccYearTo.SelectedItem.Value
            End If

            Try
                strParam = "||" & intAccYear             '--to find highest period of all locations given
                intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_Max_Get, _
                                                        strCompany, _
                                                        pv_strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        objAccCfg)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_STDRPT_CTRL_ACCCFG_MAX_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            '--to check whether any records for last year maximum accounting period
            Try
                intMaxPeriod = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_STDRPT_CTLR_ACCCFG_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & Convert.ToString(intAccYear) & "&redirect=")
            End Try

        Else
            If pv_blnIsFrom = True Then
                lstAccYear.Items.Add(strAccYear)    '--no accounting year found
                lstAccYear.SelectedIndex = intSelIndex
            Else
                lstAccYearTo.Items.Add(strAccYear)    '--no accounting year found
                lstAccYearTo.SelectedIndex = intSelIndex    
            End If
            intMaxPeriod = Convert.ToInt16(strAccMonth) '--only takes up to current accounting month
        End If

        objAccCfg = Nothing
        Return intMaxPeriod
    End Function

    Sub OnIndexChage_FromAccPeriod(ByVal sender As Object, ByVal e As System.EventArgs)
        BindAccMonthList(BindAccYearList(hidUserLoc.Value, lstAccYear.SelectedItem.Value, True))
    End Sub

    Sub OnIndexChage_ToAccPeriod(ByVal sender As Object, ByVal e As System.EventArgs)
        BindAccMonthList(BindAccYearList(hidUserLoc.Value, lstAccYear.SelectedItem.Value, False))
    End Sub

    Sub BindAccMonthToList(ByVal pv_intMaxMonth As Integer)
        Dim intCnt As Integer
        Dim intSelIndex As Integer = 0

        lstAccMonthTo.Items.Clear
        For intCnt = 1 To pv_intMaxMonth
            lstAccMonthTo.Items.Add(intCnt)
            If intCnt = Convert.ToInt16(strAccMonth) Then
                intSelIndex = intCnt - 1
            End If
        Next

        lstAccMonthTo.SelectedIndex = intSelIndex
    End Sub

    '-------Get User Location --------------
    Sub GetUserLoc()
        Dim objUserLoc As New DataSet()
        Dim strParam As String
        Dim objMapPath As String
        Dim strUserLoc As String
        Dim arrParam As Array
        Dim intCnt2 As Integer
        Dim intCnt3 As Integer

        Dim strOppCd_UserLoc_GET As String = "WS_STDRPT_USERLOCATION_GET"

        Try
            strParam = "AND USERLOC.UserID = '" & strUserId & "'"
            intErrNo = objAdmin.mtdGetUserLocation(strOppCd_UserLoc_GET, strParam, objUserLoc, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_WS_SELECTIONCTRL_USERLOCATION&errmesg=" & Exp.ToString() & "&redirect=../en/reports/WS_StdRpt_Selection.aspx")
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
        BindAccMonthList(BindAccYearList(hidUserLoc.Value, lstAccYear.SelectedItem.Value, True))
        BindAccMonthToList(BindAccYearList(hidUserLoc.Value, lstAccYearTo.SelectedItem.Value, False))
    End Sub

    '-------Create Report Name Dropdownlist--------------
    Sub BindReportNameList()
        Dim dsForDropDown As New DataSet()
        Dim strParam As String
        Dim objMapPath As String

        Dim strOppCd_StdRptName_GET As String = "WS_STDRPT_NAME_GET"

        strParam = " WHERE ReportType = '" & Convert.ToString(EnumStdRptType.Workshop) & "' AND Status = '" & Convert.ToString(EnumStdRptStatus.Active) & "'   AND Reportid IN (Select ReportID From SH_REPORT_USER Where UserId='" & strUserId & "') ORDER BY RptName"
        Try
            intErrNo = objAdmin.mtdGetStdRptName(strOppCd_StdRptName_GET, strParam, dsForDropDown, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_WS_SELECTIONCTRL_REPORT_NAME_LIST&errmesg=" & Exp.ToString() & "&redirect=../en/reports/WS_StdRpt_Selection.aspx")
        End Try

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
        Dim strRptname As String = Trim(lstRptname.SelectedItem.Value)
        Dim strSelectedIndex As String = LCase(lstRptname.SelectedItem.Value)
        Dim intSelectedIndex As Integer = lstRptname.SelectedIndex
        Dim strUserLoc As String

        strUserLoc = hidUserLoc.Value

        If strSelectedIndex = "rptws1000001" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_JobList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptws1000002" Then
'----- #1 Start -----
'            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_MechanicRepairsList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_WorkshopMechanicRepairsListing.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
'----- #1 End -----
        ElseIf strSelectedIndex = "rptws1000003" Then
'----- #1 Start -----
'            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_VehBreakdownList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_WorkshopVehicleBreakdownListing.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
'----- #1 End -----
        ElseIf strSelectedIndex = "rptws1000004" Then
'----- #1 Start -----
'            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_CompletedJobList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_CompletedJobSheet.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
'----- #1 End -----
'----- [S] Workshop New Reports----
        ElseIf strSelectedIndex = "rptws1000005" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_InventoryValList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptws1000006" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_MthAccMoveList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptws1000007" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_MthStkAccMoveDetails.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptws1000008" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_StkMoveDetList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptws1000009" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_MthWSUtilByBlockList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptws1000010" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_StkSummaryList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptws1000011" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_StkUsageFreqList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptws1000012" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_StockTakeList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptws1000013" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_SumStkMoveList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
'------[E]
'----- #1 Start -----
        ElseIf strSelectedIndex = "rptws1000014" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_WorkshopIssueReturnSlip.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptws1000015" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_SummaryOfWorkshopHours.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
'----- #1 End -----
        ElseIf strSelectedIndex = "rptws1000016" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_ReplacementScheduleList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)

        ElseIf strSelectedIndex = "rptws1000017" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_MaintenanceList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)

        ElseIf strSelectedIndex = "rptws1000018" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_RemainLifeTime_Det.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptws1000019" Then
            Response.Redirect("../../" & strLangCode & "/reports/WS_StdRpt_MachineHistoryInstalList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        End If

    End Sub
End Class

