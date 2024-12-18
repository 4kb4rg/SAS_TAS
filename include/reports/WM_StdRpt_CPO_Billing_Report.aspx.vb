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

Public Class WM_StdRpt_CPOBillRpt : Inherits Page

    Protected RptSelect As UserControl

    Dim objWM As New agri.WM.clsReport()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblVehicleTag As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents txtDelDateFrom As TextBox
    Protected WithEvents txtDelDateTo As TextBox
    Protected WithEvents lblBillPartyTag As Label
    Protected WithEvents txtBillPartyCode As TextBox

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFormat As String
    Dim strLocType as String
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strDateFormat = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")
        lblDate.Visible = False
        lblDateFormat.Visible = False

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

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text
        lblBillPartyTag.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & lblCode.Text
        lblVehicleTag.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_CPO_BILLING_RPT_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/WM_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDelDateFrom As String
        Dim strDelDateTo As String
        Dim strBillPartyCode As String

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

        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String

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

        strDelDateFrom = IIf(Trim(txtDelDateFrom.Text) = "", "", Trim(txtDelDateFrom.Text))
        strDelDateTo = IIf(Trim(txtDelDateTo.Text) = "", "", Trim(txtDelDateTo.Text))
        strBillPartyCode = IIf(Trim(txtBillPartyCode.Text) = "", "", Trim(txtBillPartyCode.Text))

        strBillPartyCode = Server.UrlEncode(strBillPartyCode)

        If Not (strDelDateFrom = "" And strDelDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateFormat, strDelDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateFormat, strDelDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""WM_StdRpt_CPO_Billing_RptPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                               "&strddlAccMth=" & strddlAccMth & "&strddlAccYr=" & strddlAccYr & "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & "&lblBillPartyTag=" & lblBillPartyTag.Text & "&BillPartyCode=" & strBillPartyCode & _
                               "&lblVehicleTag=" & lblVehicleTag.Text & "&DelDateFrom=" & objDateFrom & "&DelDateTo=" & objDateTo & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""WM_StdRpt_CPO_Billing_RptPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                           "&strddlAccMth=" & strddlAccMth & "&strddlAccYr=" & strddlAccYr & "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & "&lblBillPartyTag=" & lblBillPartyTag.Text & "&BillPartyCode=" & strBillPartyCode & _
                           "&lblVehicleTag=" & lblVehicleTag.Text & "&DelDateFrom=" & objDateFrom & "&DelDateTo=" & objDateTo & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

End Class

