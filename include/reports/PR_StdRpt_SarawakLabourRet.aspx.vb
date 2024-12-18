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
Imports Microsoft.VisualBasic.DateAndTime

Public Class PR_StdRpt_SarawakLabourRet : Inherits Page

    Protected RptSelect As UserControl

    Dim objPR As New agri.PR.clsReport()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents txtAttdDateFrom As TextBox
    Protected WithEvents txtAttdDateTo As TextBox
    Protected WithEvents txtFromEmp As TextBox
    Protected WithEvents txtToEmp As TextBox
    Protected WithEvents lstStatus As DropDownList

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblDate As Label

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

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
    Dim strDateSetting As String
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strDateSetting = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                GetStartEndDate()
            End If
        End If
    End Sub

    Sub GetStartEndDate()
        Dim strDate As String
        Dim intMonth As Integer
        Dim intYear As Integer
        Dim strAttdDateFrom As String
        Dim strAttdDateTo As String

        strDate = Now()
        intMonth = Month(strDate)
        intYear = Year(strDate)

        txtAttdDateFrom.Text = "01/" & intMonth & "/" & intYear
        txtAttdDateTo.Text = objGlobal.mtdGetTotalDays(intMonth, intYear) & "/" & intMonth & "/" & intYear


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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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
        Dim strDdlAccMth As String
        Dim strDdlAccYr As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strFromEmp As String
        Dim strToEmp As String
        Dim strStatus As String
        Dim strEmpStatusText As String
        Dim strDec As String
        Dim strAttdDateFrom As String
        Dim strAttdDateTo As String

        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String


        Dim tempRptName As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        strAttdDateFrom = txtAttdDateFrom.Text.Trim
        strAttdDateTo = txtAttdDateTo.Text.Trim
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

        strStatus = lstStatus.SelectedItem.Value
        If strStatus = "All" Then
            strStatus = "%"
        End If
        strEmpStatusText = lstStatus.SelectedItem.Text

        If objGlobal.mtdValidInputDate(strDateSetting, strAttdDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strAttdDateTo, objDateFormat, objDateTo) = True Then
            Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_SarawakLabourRetPreview.aspx?Type=Print&CompName=" & strCompany & _
                                      "&Location=" & strUserLoc & _
                                      "&AttdDateFrom=" & strAttdDateFrom & _
                                      "&AttdDateTo=" & strAttdDateTo & _
                                      "&RptName=" & strRptName & _
                                      "&Decimal=" & strDec & _
                                      "&lblLocation=" & lblLocation.Text & _
                                      "&FromEmp=" & strFromEmp & _
                                      "&ToEmp=" & strToEmp & _
                                      "&Status=" & strStatus & _
                                      "&EmpStatusText=" & strEmpStatusText & _
                                      """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

        Else
            lblDateFormat.Text = objDateFormat & "."
            lblDate.Visible = True
            lblDateFormat.Visible = True
        End If

    End Sub

End Class
