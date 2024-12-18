
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

Public Class PR_StdRpt_Selection_Ctrl : Inherits UserControl

    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objPR As New agri.PR.clsReport()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()

    'lsy -> 160706
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objDataSet As New Object()
    'end lsy

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblUserLoc As Label

    Protected WithEvents lstRptname As DropDownList
    Protected WithEvents lstDecimal As DropDownList
    Protected WithEvents cbLocAll As CheckBox
    Protected WithEvents cblUserLoc As CheckBoxList

    Protected WithEvents lstPhyMonth As DropDownList
    Protected WithEvents lstPhyYear As DropDownList
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox

    'LSY -> 160706
    Protected WithEvents ddlDivision As DropDownList
    'end 

    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents btnSelDateTo As Image

    Protected WithEvents hidUserLoc As HtmlInputHidden
    Protected WithEvents TrPhyMthYr As HtmlTableRow
    Protected WithEvents TrMthYr As HtmlTableRow
    Protected WithEvents TrFromTo As HtmlTableRow

    'LSY -> 160706
    Protected WithEvents TrDivision As HtmlTableRow
    'end LSY

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserLoc As String

    'lsy -> 100706
    Dim lblErrMessage As Label

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim dr As DataRow
    Dim intSelIndex As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntDec As Integer
        Dim strResult As String

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")

        TrPhyMthYr.Visible = False
        TrMthYr.Visible = False
        TrFromTo.Visible = False

        'LSY -> 160706
        TrDivision.Visible = False
        'end

        If Not Page.IsPostBack Then
            GetUserLoc()        '--bind all locations authorised by user
            BindReportNameList() '--bind all module reports
            BindAccMonthList(BindAccYearList(strLocation, strAccYear))  '--bind accounting periods
            BindPhyMonthList()
            BindPhyYearList()

            'add by lsy -> 160706
            BindDivision(strLocation, strResult)

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

    Sub BindPhyMonthList()
        Dim CurrDate As Date
        Dim CurrMonth As Integer

        CurrDate = Today
        CurrMonth = Month(CurrDate)

        If CurrMonth = 1 Then
            lstPhyMonth.SelectedIndex = 0
        Else
            lstPhyMonth.SelectedIndex = CurrMonth - 1
        End If
    End Sub

    Sub BindPhyYearList()
        Dim CurrDate As Date
        Dim CurrYear As Integer
        Dim intCntAddPhyYr As Integer = 1
        Dim intCntMinPhyYr As Integer = 5
        Dim NewAddCurrPhyYear As Integer
        Dim NewMinCurrPhyYear As Integer
        Dim intCntddlPhyYr As Integer = 0

        CurrDate = Today
        CurrYear = Year(CurrDate)

        While intCntMinPhyYr <> 0
            intCntMinPhyYr = intCntMinPhyYr - 1
            NewMinCurrPhyYear = CurrYear - intCntMinPhyYr
            lstPhyYear.Items.Add(NewMinCurrPhyYear)
        End While

        For intCntAddPhyYr = 1 To 5
            NewAddCurrPhyYear = CurrYear + intCntAddPhyYr
            lstPhyYear.Items.Add(NewAddCurrPhyYear)
        Next

        For intCntddlPhyYr = 0 To lstPhyYear.Items.Count - 1
            If lstPhyYear.Items(intCntddlPhyYr).Text = CurrYear Then
                lstPhyYear.SelectedIndex = intCntddlPhyYr
            End If
        Next
    End Sub

    Sub BindAccMonthList(ByVal pv_intMaxMonth As Integer)
        Dim intCnt As Integer
        Dim intSelIndex As Integer = 0

        lstAccMonth.Items.Clear()
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
        Dim objAccCfg As New DataSet()

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_CTRL_ACCCFG_DIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        intSelIndex = 0
        lstAccYear.Items.Clear()

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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_CTRL_ACCCFG_MAX_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            '--to check whether any records for last year maximum accounting period
            Try
                intMaxPeriod = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_CTLR_ACCCFG_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & Convert.ToString(intAccYear) & "&redirect=")
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
        Dim objUserLoc As New DataSet()
        Dim strArrUserLoc As String
        Dim strOppCd_UserLoc_GET As String = "PR_STDRPT_USERLOCATION_GET"

        Try
            strParam = "AND USERLOC.UserID = '" & strUserId & "'"
            intErrNo = objAdmin.mtdGetUserLocation(strOppCd_UserLoc_GET, strParam, objUserLoc, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_PR_SELECTIONCTRL_USERLOCATION&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
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
        Dim strResult As String

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
        'LSY -> 160706
        BindDivision(hidUserLoc.Value, strResult)
    End Sub

    '-------Create Report Name Dropdownlist--------------
    Sub BindReportNameList()
        Dim strParam As String
        Dim objMapPath As String
        Dim dsForDropDown As New DataSet()
        Dim strOppCd_StdRptName_GET As String = "PR_STDRPT_NAME_GET"

        strParam = " WHERE ReportType = '" & Convert.ToString(objGlobal.EnumStdRptType.Payroll) & "' AND Status = '" & Convert.ToString(objGlobal.EnumStdRptStatus.Active) & "' ORDER BY RptName"
        Try
            intErrNo = objAdmin.mtdGetStdRptName(strOppCd_StdRptName_GET, strParam, dsForDropDown, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_PR_SELECTIONCTRL_REPORT_NAME_LIST&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
        End Try

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
        Dim strAccMthPeriod As String
        Dim strAccYrPeriod As String
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strUserLoc As String
        Dim strDec As String

        strAccMthPeriod = lstAccMonth.SelectedItem.Value
        strAccYrPeriod = lstAccYear.SelectedItem.Value

        strUserLoc = hidUserLoc.Value
        strDec = lstDecimal.SelectedItem.Value

        If strSelectedIndex = "rptpr1000001" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_CheckRollReconList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000002" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_EAForm.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000003" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_EmpMoveList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000004" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_ADList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000005" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_RetRemu.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000006" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_StmtTaxDeduct.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000007" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_BadDebtList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000008" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_TaxDeductList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000009" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_SocsoDeductList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000010" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_EPFDeductList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000011" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_AveEarningList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000012" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DailyAllowanceTrans.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000013" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_EmpCP8AList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000014" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_YearEndList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000015" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_LevyList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000016" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_Payslip.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000017" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_ADTrxList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000018" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PieceRatePayTrxList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000019" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_SOCSOBorang8A.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000020" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_EPFBorangA.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000021" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_TAXBorangCP39.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000022" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_Cheque.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000023" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_BankCreditList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000024" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_WagesPaymentList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000025" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_EmpList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000026" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_CashDenominationList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000027" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_LoaderPaymentList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000028" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PieceRatedAttdTrans.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000029" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_MthLabOverheadDist.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000030" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PayslipTW.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000031" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_SarawakLabourRet.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000032" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_ContractCheckrollAllow.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000033" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_BalDueOutLeave.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000034" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_MandoreAttdList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000035" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_GangAvgEarn.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000036" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_TripTransList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000037" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_ContractPaymentTransList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
            '----- #1 Start -----
        ElseIf strSelectedIndex = "rptpr1000039" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DailyAttendanceSummaryList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
            '----- #1 End -----
        ElseIf strSelectedIndex = "rptpr1000040" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DendaListing.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
            '----- BHL ------
        ElseIf strSelectedIndex = "rptpr1000041" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_AdvPayment.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000042" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_ActualPayment.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000043" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_BankTransListing.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&AccMth=" & strAccMthPeriod & "&AccYr=" & strAccYrPeriod & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000044" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PotKoperasi.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000045" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PensiunListing.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000046" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_ListSalAndAllw.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000047" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_UsulNaikGaji.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000048" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_AirBus.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000049" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_EvalSum.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000050" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_MonthlyTaxRpt.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000051" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_Medical.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000052" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_Jamsostek.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000053" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_WPList.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpr1000054" Then
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DaftarAbsensi.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        End If
    End Sub
    'add by LSY
    Sub BindDivision(ByVal pv_strEstate As String, ByVal pv_strResult As String)
        Dim strParam As String = ""
        Dim strOppCd_GET As String = "GL_CLSSETUP_BLOCKGROUP_LIST_GET"
        Dim SearchStr As String = ""
        Dim sortItem As String = ""

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

        If pv_strEstate <> "" Then
            SearchStr = SearchStr & " and blk.LocCode in ( '" & pv_strEstate & "' ) "
        End If

        sortItem = "ORDER BY blk.BlkGrpCode"
        strParam = sortItem & "|" & SearchStr

        Try
            '------------ USING COMPONENTS -----------------
            intErrNo = objGLSetup.mtdGetMasterList(strOppCd_GET, strParam, objGLSetup.EnumGLMasterType.BlkGrp, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCKGROUP_GET&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_BlockGrp.aspx")
        End Try


        For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
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
        '        ddlDivision.SelectedIndex = 0

        If objDataSet.Tables(0).Rows.Count > 0 Then
            ddlDivision.Items.Add("All")
        End If
        ddlDivision.SelectedIndex = (objDataSet.Tables(0).Rows.Count)


        '        end if

    End Sub
End Class
