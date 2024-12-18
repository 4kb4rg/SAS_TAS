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

Public Class GL_STDRPT_SELECTION_CTRL : Inherits UserControl

    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGL As New agri.GL.clsReport()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objGLSetup As New agri.GL.clsSetup()

    Dim objDataSet As New Object()

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblUserLoc As Label
    Protected WithEvents lblLocTag1 As Label
    Protected WithEvents lblLocTag2 As Label

    Protected WithEvents lblErrMessage As Label

    Protected WithEvents lstRptname As DropDownList
    Protected WithEvents lstDecimal As DropDownList
    Protected WithEvents cbLocAll As CheckBox
    Protected WithEvents cblUserLoc As CheckBoxList
    Protected WithEvents rblLocation As RadioButtonList
    Protected WithEvents ddlStmtBy As DropDownList

    'LSY
    Protected WithEvents ddlDivision As DropDownList
    'end 

    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents btnSelDateTo As Image

    Protected WithEvents hidUserLoc As HtmlInputHidden
    Protected WithEvents TrMthYr As HtmlTableRow
    Protected WithEvents TrFromTo As HtmlTableRow
    Protected WithEvents TrRadioLoc As HtmlTableRow
    Protected WithEvents TrCheckLoc As HtmlTableRow
    Protected WithEvents TrStmtBy As HtmlTableRow

    'LSY
    Protected WithEvents TrDivision As HtmlTableRow
    'end LSY

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserLoc as String
    Dim strSelectedRptName As String

    Dim intCnt as integer
    Dim intErrNo as integer
    Dim dr As DataRow
    Dim intSelIndex As Integer
    Dim LocationTag As String
    Dim CompanyTag As String
	'add by alim
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	'End of Add by alim

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntDec As Integer
        Dim strResult as String

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        'add by alim
        strLocType = Session("SS_LOCTYPE")
        'End of Add by alim        TrMthYr.Visible = False
        TrFromTo.Visible = False
        TrRadioLoc.visible = False
        TrStmtBy.Visible = False
        'LSY
        TrDivision.Visible = False
        'end
        onload_GetLangCap()

        If Not Page.IsPostBack Then
            GetUserLoc()        '--bind all locations authorised by user
            BindReportNameList()
            BindAccMonthList(BindAccYearList(strLocation, strAccYear))  '--bind accounting periods

            intSelIndex = Request.QueryString("SelIndex")
            lstRptname.SelectedIndex = intSelIndex
            lstDecimal.SelectedIndex = 0
            strSelectedRptName = lstRptname.SelectedItem.Value
            BindStmtBy()
            BindDivision(strLocation,strResult)
            LoadPage_CheckStmtBy()

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
                             ByVal pv_strAccYear As String) As Integer

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_CTRL_ACCCFG_DIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        intSelIndex = 0 
        lstAccYear.Items.Clear

        If objAccCfg.Tables(0).Rows.Count > 0 Then      '--found year's record
            For intCnt = 0 To objAccCfg.Tables(0).Rows.Count - 1    '--bind accounting year dropdownlist
                lstAccYear.Items.Add(objAccCfg.Tables(0).Rows(intCnt).Item("AccYear"))

                If objAccCfg.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                    intSelIndex = intCnt    '--selected index for accounting year dropdownlist
                End If
            Next

            lstAccYear.SelectedIndex = intSelIndex
            intAccYear = lstAccYear.SelectedItem.Value
            Try
                strParam = "||" & intAccYear             '--to find highest period of all locations given
                intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_Max_Get, _
                                                        strCompany, _
                                                        pv_strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        objAccCfg)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_CTRL_ACCCFG_MAX_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            '--to check whether any records for last year maximum accounting period
            Try
                intMaxPeriod = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_CTLR_ACCCFG_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & Convert.ToString(intAccYear) & "&redirect=")
            End Try

        Else
            lstAccYear.Items.Add(strAccYear)    '--no accounting year found
            lstAccYear.SelectedIndex = intSelIndex
            intMaxPeriod = Convert.ToInt16(strAccMonth) '--only takes up to current accounting month
        End If

        objAccCfg = Nothing
        Return intMaxPeriod
    End Function

    Sub OnIndexChage_FromAccPeriod(ByVal sender As Object, ByVal e As System.EventArgs)
        BindAccMonthList(BindAccYearList(hidUserLoc.Value, lstAccYear.SelectedItem.Value))
    End Sub

    '-------Get User Location --------------
    Sub GetUserLoc()
        Dim strParam As String
        Dim objMapPath As String
        Dim strUserLoc As String
        Dim arrParam As Array
        Dim intCnt2 As Integer
        Dim intCnt3 As Integer
        Dim counter As Integer
        Dim objUserLoc As New DataSet()
        Dim strArrUserLoc As String
        Dim strOppCd_UserLoc_GET As String = "GL_STDRPT_USERLOCATION_GET"

        Try
            strParam = "AND USERLOC.UserID = '" & strUserId & "'"
            intErrNo = objAdmin.mtdGetUserLocation(strOppCd_UserLoc_GET, strParam, objUserLoc, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_GL_SELECTIONCTRL_USERLOCATION&errmesg=" & Exp.ToString() & "&redirect=../" & strLangCode & "/reports/GL_StdRpt_Selection.aspx")
        End Try

        rblLocation.DataSource = objUserLoc.Tables(0)
        rblLocation.DataValueField = "LocCode"
        rblLocation.DataBind()

        For counter=0 To rblLocation.Items.Count - 1
            If Trim(rblLocation.Items(counter).value) = Trim(strLocation) Then
                rblLocation.Items(counter).Selected = True
                Exit For
            End If
        Next
        
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


    Sub LocCheck()
        Dim intCnt2 As Integer = 0
        Dim tempUserLoc As String
        Dim txt As HtmlInputHidden
        Dim strResult as String

        For intCnt2 = 0 To cblUserLoc.Items.Count - 1
            If cblUserLoc.Items(intCnt2).Selected Then
                If cblUserLoc.Items.Count = 1 Then
                    tempUserLoc = cblUserLoc.Items(intCnt2).Text
                Else
                    tempUserLoc = tempUserLoc & "','" & cblUserLoc.Items(intCnt2).Text
                End If
            End If
        Next intCnt2

        hidUserLoc.Value = tempUserLoc
        BindAccMonthList(BindAccYearList(hidUserLoc.Value, lstAccYear.SelectedItem.Value))
        'LSY
        BindDivision(hidUserLoc.Value, strResult)
        'end
    End Sub

    '-------Create Report Name Dropdownlist--------------
    Sub BindReportNameList()
        Dim strParam As String
        Dim objMapPath As String
        Dim dsForDropDown As New DataSet()
        Dim strOppCd_StdRptName_GET As String = "GL_STDRPT_NAME_GET"

        strParam = " WHERE ReportType = '" & Convert.ToString(objGlobal.EnumStdRptType.GeneralLedger) & "' AND Status = '" & Convert.ToString(objGlobal.EnumStdRptStatus.Active) & "'   AND Reportid IN (Select ReportID From SH_REPORT_USER Where UserId='" & strUserId & "') ORDER BY RptName "
        Try
            intErrNo = objAdmin.mtdGetStdRptName(strOppCd_StdRptName_GET, strParam, dsForDropDown, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_GL_SELECTIONCTRL_REPORT_NAME_LIST&errmesg=" & Exp.ToString() & "&redirect=../" & strLangCode & "/reports/GL_StdRpt_Selection.aspx")
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
        Dim strAccMthPeriod As String
        Dim strAccYrPeriod As String
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strRadioLoc As String

        strAccMthPeriod = lstAccMonth.SelectedItem.Value
        strAccYrPeriod = lstAccYear.SelectedItem.Value
        strDateFrom = txtDateFrom.Text
        strDateTo = txtDateTo.Text

        strUserLoc = hidUserLoc.Value
        strDec = lstDecimal.SelectedItem.Value

        If strSelectedIndex = "rptgl1000001" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_JournalList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptgl1000002" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_VehExpenseDet.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&Dec=" & strDec)
        'ElseIf strSelectedIndex = "rptgl1000003" Then
        '    Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_TrialBalSummaryList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&Dec=" & strDec & "&strSum=yes")
        ElseIf strSelectedIndex = "rptgl1000003" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_TrialBalSum.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&Dec=" & strDec & "&strSum=yes")
        ElseIf strSelectedIndex = "rptgl1000004" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_MaintainHarvestList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'ElseIf strSelectedIndex = "rptgl1000005" Then
        '    strRadioLoc = rblLocation.SelectedItem.Value
        '    Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_TrialBalSummaryDet.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&Dec=" & strDec & "&strSum=no" & "&strRadioLoc=" & strRadioLoc)
        ElseIf strSelectedIndex = "rptgl1000005" Then
            strRadioLoc = rblLocation.SelectedItem.Value
            'Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_DetTrialBal.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&Dec=" & strDec & "&strSum=no" & "&strRadioLoc=" & strRadioLoc)
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_DetAccList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&Dec=" & strDec & "&strSum=no" & "&strRadioLoc=" & strRadioLoc)
        ElseIf strSelectedIndex = "rptgl1000006" Then
            '    Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_ActGrpSummaryList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_MthExpSummary.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptgl1000007" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_DetAccLedger.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000008" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_BlockLedgerAct.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000009" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_CostLedgerSummary.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000010" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_VehRunExpense.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000011" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_MthEndTrxSummary.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000012" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_VehicleUsageList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000013" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_TrialBalance.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000014" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_BalanceSheet.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000015" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_ProfitLoss.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000016" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_MthVehDist.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptgl1000017" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_MthBlkLedgerList.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000018" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_GeneralLedgerTrxListing.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000019" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_DetailedTrialBalance.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000020" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_JournalAdjVoucher.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000021" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_SummVehTypeExpense.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000022" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_MthGCDistribution.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
'----- #1 Start -----
        ElseIf strSelectedIndex = "rptgl1000026" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_MthWorkShopExpSummary.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
'----- #1 End -----
'----- #2 Start -----
        ElseIf strSelectedIndex = "rptgl1000027" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_LabourHourReconciliation.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
'----- #2 End -----
'#3 START
        ElseIf strSelectedIndex = "rptgl1000028" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_AreaStatement.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
'#3 END
        'LSY -> PTR
        ElseIf strSelectedIndex = "rptgl1000029" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_PekDanTang.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end LSY   
        'dian [S]-Stok Pupuk
        ElseIf strSelectedIndex = "rptgl1000030" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_StokPupuk.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'dian [E]   
        'LSY -> MTK            
        ElseIf strSelectedIndex = "rptgl1000031" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_MutTeKer.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end LSY     
        ElseIf strSelectedIndex = "rptgl1000032" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_COGS.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end LSY     
        ElseIf strSelectedIndex = "rptgl1000033" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_BukaLahanAwal.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'LSY -> DPPK
        ElseIf strSelectedIndex = "rptgl1000034" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_DistribusiPemakaian.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end LSY
        'Dian 
        ElseIf strSelectedIndex = "rptgl1000035" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_BiayaUmum.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end Dian
        'LSY -> PTM
        ElseIf strSelectedIndex = "rptgl1000037" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_PemeliharaanTanaman.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end LSY
        'Dian
        ElseIf strSelectedIndex = "rptgl1000036" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_OutputPotongBuah.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end Dian
        'SMS [S]
        ElseIf strSelectedIndex = "rptgl1000038" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_BiayaProduksi.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000039" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_StatistikProduksi.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'SMN [E] 
        'Dian
        ElseIf strSelectedIndex = "rptgl1000040" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_PemupukanTM.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end Dian
        'Dian
        ElseIf strSelectedIndex = "rptgl1000041" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_RekapOutputPotongBuah.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end Dian
        ElseIf strSelectedIndex = "rptgl1000042" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_RincianPersediaan.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'Dian
        ElseIf strSelectedIndex = "rptgl1000043" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_RincianBiayaPanenPerThnTanam.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end Dian
        ElseIf strSelectedIndex = "rptgl1000044" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_HKEfektifSKUH.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'Dian
        ElseIf strSelectedIndex = "rptgl1000045" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_PemupukanOrganikTM.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end Dian
         ElseIf strSelectedIndex = "rptgl1000047" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_UpahRata2SKUH.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'SMN [S] 
        ElseIf strSelectedIndex = "rptgl1000046" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_RekapProdTahunTanam.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        
        ElseIf strSelectedIndex = "rptgl1000048" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_KaryawanStaff.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'SMN [E] 
        'Dian
        ElseIf strSelectedIndex = "rptgl1000049" Then            
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_UpahPanen.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        'end Dian
        '#4 - Start
        ElseIf strSelectedIndex = "rptgl1000050" Then
            'Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_FS.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
            Response.Redirect("../../en/reports/GL_StdRpt_FS.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)

        ElseIf strSelectedIndex = "rptgl1000052" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_TrialBalanceTrial.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)

        ElseIf strSelectedIndex = "rptgl1000053" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_NotaDebet.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)


        ElseIf strSelectedIndex = "rptgl1000054" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_TrialBalDailyTemp.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)

        ElseIf strSelectedIndex = "rptgl1000055" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_TrialBalDailyByTransTemp.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)

        ElseIf strSelectedIndex = "rptgl1000060" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_LaporanBiayaPabrik.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000062" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_LaporanBiayaEstate.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)

        ElseIf strSelectedIndex = "rptgl1000061" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_TrialBalDaily.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)

        ElseIf strSelectedIndex = "rptgl1000063" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_TrialBal_HutangDagang.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptgl1000064" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_TrialBal_PPNMasukan.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)

        ElseIf strSelectedIndex = "rptgl1000065" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_LaporanMREstate.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)

		ElseIf strSelectedIndex = "rptgl1000066" Then
            Response.Redirect("../../" & strLangCode & "/reports/GL_StdRpt_LaporanMutasiTBM_TM.aspx?RptName=" & strRptname & "&SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)

        End If

    End Sub

    Sub BindStmtBy()
        If ddlStmtBy.Items.Count = 0 Then
            ddlStmtBy.Items.Add(New ListItem(CompanyTag, "comp"))
            ddlStmtBy.Items.Add(New ListItem(LocationTag, "loc"))
            ddlStmtBy.SelectedIndex = 1
        End If
    End Sub

    Sub CheckStmtBy(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim intCntLocAll As Integer = 0
        
        Dim strSelectedIndex As String = LCase(ddlStmtBy.SelectedItem.Value)
        If strSelectedIndex = "comp" Then
            TrCheckLoc.Visible = False
        Else
            TrCheckLoc.Visible = True
        End If
    End Sub

    Sub LoadPage_CheckStmtBy()
        If ddlStmtBy.SelectedItem.Value = "comp" Then
            TrCheckLoc.Visible = False
        End If
    End Sub


    '=== For Language Caption==================================================
    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblLocTag1.Text = LCase(GetCaption(objLangCap.EnumLangCap.Location))
        lblLocTag2.Text = LCase(GetCaption(objLangCap.EnumLangCap.Location))
        LocationTag = GetCaption(objLangCap.EnumLangCap.Location)
        CompanyTag = GetCaption(objLangCap.EnumLangcap.Company)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_REPORTS_SELECTION_CTRL_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
        End Try

    End Sub

    'add by alim
    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    'Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    Exit For
                End If
            Next
        End Function
    'End of Add by alim

    '=====End for Language Caption ===============================================

    'add by LSY
    Sub BindDivision(ByVal pv_strEstate as String, ByVal pv_strResult as String)
        Dim strParam as String = ""
        Dim strOppCd_GET As String = "GL_CLSSETUP_BLOCKGROUP_LIST_GET"
        Dim SearchStr as String = ""
        Dim sortItem as String = ""

        If pv_strEstate = "" Then
            pv_strEstate = strLocation
        Else
            If Left(pv_strEstate, 3) = "','" Then
                pv_strEstate = Right(pv_strEstate, Len(pv_strEstate) - 3)
            ElseIf Right(pv_strEstate, 3) = "','" Then
                pv_strEstate = Left(pv_strEstate, Len(pv_strEstate) - 3)
            ElseIf Left(pv_strEstate, 1) = "," Then
                pv_strEstate = Right(pv_strEstate, Len(pv_strEstate) - 1)
            ElseIf Right(pv_strEstate, 1) = "," Then
                pv_strEstate = Left(pv_strEstate, Len(pv_strEstate) - 1)
            End If
        End If


        SearchStr = " and blk.Status like '1' "
        
        if pv_strEstate <> "" Then    
            SearchStr = SearchStr & " and blk.LocCode in ( '" & pv_strEstate & "' ) "
        end if 

        sortItem = "ORDER BY blk.BlkGrpCode" 
        strParam = sortItem & "|" & SearchStr

        Try
            '------------ USING COMPONENTS -----------------
            intErrNo = objGLSetup.mtdGetMasterList(strOppCd_GET, strParam, objGLSetup.EnumGLMasterType.BlkGrp, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCKGROUP_GET&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BlockGrp.aspx")
        End Try
    
   
        For intCnt = 0 to objDataSet.Tables(0).Rows.Count - 1
            objDataSet.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
            objDataSet.Tables(0).Rows(intCnt).Item("Description") = objDataSet.Tables(0).Rows(intCnt).Item("BlkGrpCode") & " ( " & Trim(objDataSet.Tables(0).Rows(intCnt).Item("Description")) & " )"
        Next

      
        dr = objDataSet.Tables(0).NewRow
        dr("BlkGrpCode") = ""
        dr("Description") = "Please Select Division"
        objDataSet.Tables(0).Rows.InsertAt(dr, 0)

        ddlDivision.DataSource = objDataSet.Tables(0)
        ddlDivision.DataValueField = "BlkGrpCode"
        ddlDivision.DataTextField = "Description"
        ddlDivision.DataBind()
        ddlDivision.SelectedIndex = 0

        If objDataSet.Tables(0).Rows.Count > 0 Then
                ddlDivision.Items.add("All")
        end if

        ddlDivision.SelectedIndex = intCnt + 1


'        end if

    End Sub


End Class

'Response.Write("<Script Language=""JavaScript"">alert('" & strParam & "');</Script>")
