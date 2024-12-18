
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services

Public Class PR_StdRpt_TripTransList : Inherits Page

    Protected RptSelect As UserControl

    Dim objPRPrt As New agri.PR.clsReport()
    Dim objPRTrx As New agri.Pr.clsTrx()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblTracker As Label
    Protected WithEvents txtFromEmp As TextBox
    Protected WithEvents txtToEmp As TextBox
    Protected WithEvents lstEmpStatus As DropDownList
    Protected WithEvents txtRouteCode As Textbox
    Protected WithEvents txtLoadCode As TextBox
    Protected WithEvents lblAccCode As Label
    Protected WithEvents txtSrchAccCode As TextBox
    Protected WithEvents lblBlock As Label
    Protected WithEvents ddlBlkType As DropDownList
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents lblBlkGrpCode As Label
    Protected WithEvents txtSrchBlkGrpCode As TextBox
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents txtSrchBlkCode As TextBox
    Protected WithEvents TrSubBlk As HtmlTableRow
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents txtSrchSubBlkCode As TextBox
    Protected WithEvents lblVehCode As Label
    Protected WithEvents txtSrchVehCode As TextBox
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents txtSrchVehExpCode As TextBox
    Protected WithEvents lstTransStat As DropDownList
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lstSortBy As DropDownList
    Protected WithEvents lstOrderBy As DropDownList

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblErrMessage As Label

    Dim objLangCapDs As New Object()
    Dim objSysCfgDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim tempAD As String
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindEmpStatus()
                BindBlkType()
                BindTransStatus()
                BindSortByList()
            End If

            If ddlBlkType.SelectedItem.Value = "BlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
            ElseIf ddlBlkType.SelectedItem.Value = "BlkGrp" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
            End If
        End If
    End Sub


    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblVehCode.text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense)
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code "
        lblAccCode.text = GetCaption(objLangCap.EnumLangCap.Account) & " Code"  

        lblBlock.text = GetCaption(objLangCap.EnumLangCap.Block)
        lblBlkGrpCode.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & " Code "

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_REPORTS_TRIPTRANSLIST_LANGCAP_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
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

    Sub BindBlkType()
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.Text, "BlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkGrpCode.text, "BlkGrp"))
    End Sub

    Sub BindEmpStatus()

        lstEmpStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.All), objHRTrx.EnumEmpStatus.All))
        lstEmpStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.Active), objHRTrx.EnumEmpStatus.Active))
        lstEmpStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.Terminated), objHRTrx.EnumEmpStatus.Terminated))

        lstEmpStatus.SelectedIndex = 1

    End Sub

    Sub BindTransStatus()

        lstTransStat.Items.Add(New ListItem(objPRTrx.mtdGetTripStatus(objPRTrx.EnumTripStatus.All), objPRTrx.EnumTripStatus.All))
        lstTransStat.Items.Add(New ListItem(objPRTrx.mtdGetTripStatus(objPRTrx.EnumTripStatus.Active), objPRTrx.EnumTripStatus.Active))
        lstTransStat.Items.Add(New ListItem(objPRTrx.mtdGetTripStatus(objPRTrx.EnumTripStatus.Closed), objPRTrx.EnumTripStatus.Closed))
        lstTransStat.Items.Add(New ListItem(objPRTrx.mtdGetTripStatus(objPRTrx.EnumTripStatus.Cancelled), objPRTrx.EnumTripStatus.Cancelled))

        lstTransStat.SelectedIndex = 1

    End Sub


    Sub BindSortByList()
        lstSortBy.Items.Add(New ListItem("Trip ID", "trp"))
        lstSortBy.Items.Add(New ListItem("Employee Code", "emp"))
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strFromEmp As String
        Dim strToEmp As String
        Dim strEmpStatus As String
        Dim strRouteCode As String
        Dim strLoadCode As String
        Dim strEmpStatusText As String
        Dim strDec As String
        Dim strBlkType As String
        Dim strSrchAccCode As String
        Dim strSrchBlkCode As String
        Dim strSrchBlkGrpCode As String
        Dim strSrchVehCode As String
        Dim strSrchVehExpCode As String
        Dim strDdlTransStat As String
        Dim strDdlTransStatText As String
        Dim strSortBy As String
        Dim strSortByText As String
        Dim strOrderBy As String
        Dim strOrderByText As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRptName As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        
        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)

        tempRptName = RptSelect.FindControl("lstRptName")
        strRptName = Trim(tempRptName.SelectedItem.Value)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)
        strFromEmp = txtFromEmp.Text
        strToEmp = txtToEmp.Text

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

        If lstEmpStatus.SelectedItem.Value = "" Then
            strEmpStatus = ""
            strEmpStatusText = "ALL"
        Else
            strEmpStatus = lstEmpStatus.SelectedItem.Value
            strEmpStatusText = UCase(lstEmpStatus.SelectedItem.Text)
        End If

        strRouteCode = Trim(txtRouteCode.Text)
        strLoadCode = Trim(txtLoadCode.Text)
        strBlkType = Trim(ddlBlkType.SelectedItem.Value)
        strSrchAccCode = Trim(txtSrchAccCode.Text)
        strSrchBlkCode = Trim(txtSrchBlkCode.Text)
        strSrchBlkGrpCode = Trim(txtSrchBlkGrpCode.Text)
        strSrchVehCode = Trim(txtSrchVehCode.Text)
        strSrchVehExpCode = Trim(txtSrchVehExpCode.Text)

        If lstTransStat.SelectedItem.Value = "" Then
            strDdlTransStat = ""
            strDdlTransStatText = "ALL"
        Else
            strDdlTransStat = lstTransStat.SelectedItem.Value
            strDdlTransStatText = UCase(lstTransStat.SelectedItem.Text)
        End If


        strSortBy = Trim(lstSortBy.SelectedItem.Value)
        strSortByText = Trim(lstSortBy.SelectedItem.Text)
        strOrderBy = Trim(lstOrderBy.SelectedItem.Value)
        strOrderByText = Trim(lstOrderBy.SelectedItem.Text)

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_TripTransListPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & "&ddlAccMth=" & strDdlAccMth & "&ddlAccYr=" & strDdlAccYr & _
                       "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & _
                       "&FromEmp=" & strFromEmp & "&ToEmp=" & strToEmp & _
                       "&EmpStatus=" & strEmpStatus & "&EmpStatusText=" & strEmpStatusText & _
                       "&RouteCode=" & strRouteCode & "&LoadCode=" & strLoadCode & _
                       "&strBlkType=" & strBlkType & _ 
                       "&lblAccCode=" & lblAccCode.Text & "&strSrchAccCode=" & strSrchAccCode & _
                       "&lblBlkGrpCode=" & lblBlkGrpCode.Text & "&strSrchBlkGrpCode=" & strSrchBlkGrpCode & _
                       "&lblBlkCode=" & lblBlkCode.Text & "&strSrchBlkCode=" & strSrchBlkCode & _
                       "&lblVehCode=" & lblVehCode.Text & "&strSrchVehCode=" & strSrchVehCode & _
                       "&lblVehExpCode=" & lblVehExpCode.Text & "&strSrchVehExpCode=" & strSrchVehExpCode & _
                       "&ddlTransStat=" &  strDdlTransStat & "&ddlTransStatText=" &  strDdlTransStatText & _
                       "&SortBy=" & strSortBy & "&SortByText=" & strSortByText & _
                       "&OrderBy=" & strOrderBy & "&OrderByText=" & strOrderByText & "&lblBlockTag=" & lblBlock.Text & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
