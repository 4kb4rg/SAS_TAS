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

Public Class PR_StdRpt_DailyAttendanceSummaryList : Inherits Page

    Protected RptSelect As UserControl

    Dim objPR As New agri.PR.clsReport()
    Dim objHRTrx as New agri.HR.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents txtEmpFrom As Textbox
    Protected WithEvents txtEmpTo As TextBox
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lblHidCostLevel As Label

    Dim objLangCapDs As New Object()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc As String

    Dim intCnt As Integer
    Dim intErrNo As Integer

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
                If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                    lblHidCostLevel.text = "block"
                Else
                    lblHidCostLevel.text = "subblock"
                End If
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_REPORTS_DAILYALLOWANCETRANS_LANGCAP_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
        End Try

    End Sub



    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    
    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptName As String
        Dim strRptID As String
        Dim strUserLoc As String
        Dim strEmpFrom As String
        Dim strEmpTo As String
        Dim strStatus As String
        Dim strStatusText As String
        Dim strDec As String

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
        strRptName = Trim(tempRptName.SelectedItem.Text)
        strRptID = Trim(tempRptName.SelectedItem.Value)
        
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)
        strEmpFrom = txtEmpFrom.Text
        strEmpTo = txtEmpTo.Text

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

        If ddlStatus.SelectedItem.Value = "All" Then
            strStatus = ""
            strStatusText = "ALL"
        Else
            strStatus = ddlStatus.SelectedItem.Value
            strStatusText = UCase(ddlStatus.SelectedItem.Text)
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_DailyAttendanceSummaryListPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&strCostLevel=" & lblHidCostLevel.text & _
                       "&lblLocation=" & lblLocation.Text & _
                       "&EmpFrom=" & strEmpFrom & "&EmpTo=" & strEmpTo & _
                       "&StatusText=" & strStatusText & _
                       "&strRptID=" & strRptID & _
                       "&strRptTitle=" & strRptName & _
                       "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
