Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services
Imports Microsoft.VisualBasic

Public Class PR_StdRpt_UsulNaikGaji : Inherits Page
    Protected WithEvents PrintPrev As ImageButton
    Protected RptSelect As UserControl
    
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblDept As Label
    Protected WithEvents lblErrYear As Label
    Protected WithEvents lblCurrYear As Label

    Protected WithEvents tbxYear As TextBox
    Protected WithEvents tbxPIC1 As TextBox
    Protected WithEvents tbxPIC2 As TextBox
    Protected WithEvents tbxDes1 As TextBox
    Protected WithEvents tbxDes2 As TextBox

    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strLocType as String
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        lblCurrYear.Text = Session("SS_PHYYEAR")

        lblErrYear.Visible = False
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                tbxYear.Text = lblCurrYear.Text
            End If
        End If

    End Sub
    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        Dim ucTrDecimal As HtmlTableRow
        Dim tempAccYr As DropDownList

        tempAccYr = RptSelect.FindControl("lstAccYear") 

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = False

        ucTrDecimal = RptSelect.FindControl("TrDecimal")
        ucTrDecimal.Visible = True

    End Sub
    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDendaCodeFrom As String
        Dim strDendaCodeTo As String
        Dim strDendaType As String
        Dim strStatus As String
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strYear As String
        Dim strPIC1 As String
        Dim strPIC2 As String
        Dim strDes1 As String
        Dim strDes2 As String
        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strParam As String

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
        strPIC1 = Trim(tbxPIC1.Text)
        strPIC2 = Trim(tbxPIC2.Text)
        strDes1 = Trim(tbxDes1.Text)
        strDes2 = Trim(tbxDes2.Text)
        strYear = Trim(tbxYear.Text)

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
        If tbxYear.Text = "" Then
            lblErrYear.Visible = True
            Exit Sub
        Else      
            Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_UsulNaikGajiPreview.aspx?CompCode=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                       "&strDes2=" & strDes2 &"&strDes1=" & strDes1 & "&strPIC2=" & strPIC2 & "&strPIC1=" & strPIC1 & "&strYear=" & strYear & "&lblDept=" & lblDept.Text & "&lblLocation=" & lblLocation.Text & "&SelAccMonth=" & strddlAccMth & "&SelAccYear=" & strddlAccYr & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If

    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblDept.Text = GetCaption(objLangCap.EnumLangCap.Department)

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_DENDALIST_GET_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
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
End Class
