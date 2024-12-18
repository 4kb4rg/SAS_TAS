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

Public Class PU_StdRpt_Selection_Ctrl_RPT : Inherits UserControl
    Protected objPU As New agri.PU.clsReport()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdmin As New agri.Admin.clsShare()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objGLtrx As New agri.GL.ClsTrx()

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

    Protected WithEvents TrMthYr As HtmlTableRow
    Protected WithEvents TRDocDateFromTo As HtmlTableRow
    Protected WithEvents hidUserLoc As HtmlInputHidden

    Protected WithEvents cblCompList As CheckBoxList
    Protected WithEvents hidCompList As HtmlInputHidden


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
    Dim strCompList As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntDec As Integer

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")

        TrMthYr.Visible = False
        TRDocDateFromTo.Visible = False
        If Not Page.IsPostBack Then
            GetCompanyList()
            GetUserLoc()        '--bind all locations authorised by user
            BindReportNameList() '--bind all module reports
            BindAccMonthList(BindAccYearList(strLocation, strAccYear))  '--bind accounting periods

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_CTRL_ACCCFG_DIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_CTRL_ACCCFG_MAX_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            '--to check whether any records for last year maximum accounting period
            Try
                intMaxPeriod = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_CTLR_ACCCFG_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & Convert.ToString(intAccYear) & "&redirect=")
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
        Dim strOppCd_UserLoc_GET As String = "PU_STDRPT_USERLOCATION_GET"

        strParam = "AND USERLOC.UserID = '" & strUserId & "'"
        Try
            intErrNo = objAdmin.mtdGetUserLocation(strOppCd_UserLoc_GET, strParam, objUserLoc, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_PU_SELECTIONCTRL_USERLOCATION&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PU_StdRpt_Selection.aspx")
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
        BindAccMonthList(BindAccYearList(hidUserLoc.Value, lstAccYear.SelectedItem.Value))
    End Sub

    '-------Create Report Name Dropdownlist--------------
    Sub BindReportNameList()
        Dim strParam As String
        Dim objMapPath As String
        Dim dsForDropDown As New DataSet()
        Dim strOppCd_StdRptName_GET As String = "PU_STDRPT_NAME_GET"

        strParam = " WHERE ReportType = '" & Convert.ToString(objGlobal.EnumStdRptType.Purchasing) & "' AND Status = '" & Convert.ToString(objGlobal.EnumStdRptStatus.Active) & "' ORDER BY RptName"
        Try
            intErrNo = objAdmin.mtdGetStdRptName(strOppCd_StdRptName_GET, strParam, dsForDropDown, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_PU_SELECTIONCTRL_REPORT_NAME_LIST&errmesg=" & Exp.ToString() & "redirect=../en/reports/PU_StdRpt_Selection.aspx")
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

        If strSelectedIndex = "rptpu1000001" Then
            Response.Redirect("../../en/reports/PU_StdRpt_GoodsRcvList.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000002" Then
            Response.Redirect("../../en/reports/PU_StdRpt_POList.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000003" Then
            Response.Redirect("../../en/reports/PU_StdRpt_GoodsRtnList.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000004" Then
            Response.Redirect("../../en/reports/PU_StdRpt_DAList.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000005" Then
            '            Response.Redirect("../../en/reports/PU_StdRpt_POStmt.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
            Response.Redirect("../../en/reports/PU_StdRpt_POStatementNew.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
            'Start PL-KMB0506280046 & PL-KMB0506280047 14/7/2005 
        ElseIf strSelectedIndex = "rptpu1000008" Then
            Response.Redirect("../../en/reports/PU_StdRpt_MthStoreItem.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000009" Then
            Response.Redirect("../../en/reports/PU_StdRpt_MthStockReceive.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000010" Then
            Response.Redirect("../../en/reports/PU_StdRpt_PRRealization.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
            'End PL-KMB0506280046 & PL-KMB0506280047 14/7/2005

            'add by LSY
        ElseIf strSelectedIndex = "rptpu1000011" Then
            Response.Redirect("../../en/reports/PU_StdRpt_PengirimanBarangInternal.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000012" Then
            Response.Redirect("../../en/reports/PU_StdRpt_PurchaseOrderPF.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000013" Then
            Response.Redirect("../../en/reports/PU_StdRpt_PelimpahanList.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000014" Then
            Response.Redirect("../../en/reports/PU_StdRpt_RPHList.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000015" Then
            Response.Redirect("../../en/reports/PU_StdRpt_PurchasesItemList.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000016" Then
            Response.Redirect("../../en/reports/PU_StdRpt_OutstandingPO.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptpu1000017" Then
            Response.Redirect("../../en/reports/PU_StdRpt_HistoricalServicePrice.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)

        ElseIf strSelectedIndex = "rptpu1000018" Then
            Response.Redirect("../../en/reports/PU_StdRpt_LastItemPrice.aspx?SelIndex=" & Convert.ToString(intSelectedIndex) & "&UserLoc=" & strUserLoc)

            'end of LSY
        End If
    End Sub

    Sub GetCompanyList()
        Dim strOpCd_Get As String = "REPORT_CLSCOMP_COMPANY_LOCATION_LIST_GET"
        Dim objInvRcvDs As New Object
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim strParamName As String
        Dim strParamValue As String
        Dim arrParam As Array
        Dim intCnt2 As Integer
        Dim intCnt3 As Integer
        Dim objCompList As New DataSet()

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                                strParamName, _
                                                strParamValue, _
                                                objCompList)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_COMPANY&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        lblLocation.Visible = True
        cblCompList.DataSource = objCompList.Tables(0)
        cblCompList.DataValueField = "CompCode"
        cblCompList.DataBind()

        objCompList = Nothing

        hidCompList.Value = Request.QueryString("CompCode")
        strCompList = Request.QueryString("CompCode")
        If Left(strCompList, 3) = "','" Then
            arrParam = Split(strCompList, "','")
        ElseIf Right(strCompList, 1) = "," Then
            arrParam = Split(strCompList, ",")
        Else
            arrParam = Split(strCompList, ",")
        End If

        If Not hidCompList.Value = "" Then
            For intCnt2 = 0 To cblCompList.Items.Count - 1
                For intCnt3 = 0 To arrParam.GetUpperBound(0)
                    If Trim(cblCompList.Items(intCnt2).Value) = Trim(arrParam(intCnt3)) Then
                        cblCompList.Items(intCnt2).Selected = True
                    End If
                Next intCnt3
            Next intCnt2
        End If
    End Sub

End Class

