Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services

Public Class PM_StdRpt_GrafikProdReportRend_CPO : Inherits Page

    Protected RptSelect As UserControl

    Dim objPM As New agri.PM.clsReport()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblLocation As Label

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim intErrNo As Integer

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Protected WithEvents PrintPrev As System.Web.UI.WebControls.ImageButton
    'Protected WithEvents btnSelDateFrom As System.Web.UI.WebControls.Image
    'Protected WithEvents txtTrxDate As System.Web.UI.WebControls.TextBox
    'Protected WithEvents rfvTrxDate As System.Web.UI.WebControls.RequiredFieldValidator
    'Protected WithEvents lblDateFormat As System.Web.UI.WebControls.Label
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strLocType = Session("SS_LOCTYPE")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PM_STDRPT_DAILY_PROD_RPT_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PM_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strTrxDate As String

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

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

        'strTrxDate = CheckDate(txtTrxDate.Text)
        'If strTrxDate Is Nothing OrElse strTrxDate.Trim().Length() = 0 Then
        '    Exit Sub
        'End If

        Response.Write("<Script Language=""JavaScript"">window.open(""PM_StdRpt_GrafikProdReportRend_CPOPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                       "&strddlAccMth=" & strddlAccMth & "&strddlAccYr=" & strddlAccYr & "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & "&TransDate=" & strTrxDate & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Private Function CheckDate(ByVal strInputDate As String) As String
        Dim strOpCode As String = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Dim objSysCfg As New agri.PWSystem.clsConfig
        Dim dsSysDate As DataSet
        Dim strOutputDate, strDateFormat As String


        Try
            If objSysCfg.mtdGetConfigInfo(strOpCode, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            dsSysDate) = 0 Then
                Dim strDateSetting As String = objSysCfg.mtdGetDateFormat(dsSysDate.Tables(0).Rows(0).Item("Datefmt"))

                If strInputDate.Trim().Length() > 0 Then
                    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl

                    'If objGlobal.mtdValidInputDate(strDateSetting, strInputDate, strDateFormat, strOutputDate) = False Then
                    '    lblDateFormat.Text = "Date format in " & strDateFormat & "."
                    '    lblDateFormat.Visible = True
                    'End If
                End If
            End If
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PM_STDRPT_DAILY_PROD_RPT_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PM_StdRpt_Selection.aspx")
        End Try

        Return strOutputDate
    End Function

    Private Sub InitializeComponent()

    End Sub
End Class

