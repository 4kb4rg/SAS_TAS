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

Public Class HR_StdRpt_MPOBPriceListing : Inherits Page

    Protected RptSelect As UserControl
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents txtMPOBCodeFrom As TextBox
    Protected WithEvents txtMPOBCodeTo As TextBox
    Protected WithEvents lstMPOBType As DropDownList
    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents PrintPrev As ImageButton

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
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindMPOBType()
                BindStatus()
            End If
        End If

    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        Dim ucTrDecimal As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = False

        ucTrDecimal = RptSelect.FindControl("TrDecimal")
        ucTrDecimal.Visible = False

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_STDRPT_MPOBPRICE_LIST_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/HR_StdRpt_Selection.aspx")
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

    Sub BindMPOBType()
        Dim strText = "All"

        lstMPOBType.Items.Add(New ListItem(strText))
        lstMPOBType.Items.Add(New ListItem(objHRSetup.mtdGetBonusType(objHRSetup.EnumBonusType.PriceBonus), objHRSetup.EnumBonusType.PriceBonus))
        lstMPOBType.Items.Add(New ListItem(objHRSetup.mtdGetBonusType(objHRSetup.EnumBonusType.AdditionalPay), objHRSetup.EnumBonusType.AdditionalPay))
        lstMPOBType.Items.Add(New ListItem(objHRSetup.mtdGetBonusType(objHRSetup.EnumBonusType.LoadBasicPay), objHRSetup.EnumBonusType.LoadBasicPay))
        lstMPOBType.Items.Add(New ListItem(objHRSetup.mtdGetBonusType(objHRSetup.EnumBonusType.HarvestBasicPay), objHRSetup.EnumBonusType.HarvestBasicPay))

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strMPOBCodeFrom As String
        Dim strMPOBCodeTo As String
        Dim strMPOBType As String
        Dim strStatus As String
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

        If txtMPOBCodeFrom.Text = "" Then
            strMPOBCodeFrom = ""
        Else
            strMPOBCodeFrom = Trim(txtMPOBCodeFrom.Text)
        End If

        If txtMPOBCodeTo.Text = "" Then
            strMPOBCodeTo = ""
        Else
            strMPOBCodeTo = Trim(txtMPOBCodeTo.Text)
        End If

        If lstMPOBType.SelectedItem.Value = "All" Then
            strMPOBType = ""
        Else
            strMPOBType = Trim(lstMPOBType.SelectedItem.Value)
        End If

        strStatus = Trim(lstStatus.SelectedItem.Value)

        Response.Write("<Script Language=""JavaScript"">window.open(""HR_StdRpt_MPOBPriceListingPreview.aspx?CompCode=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                       "&lblLocation=" & lblLocation.Text & "&MPOBCodeFrom=" & strMPOBCodeFrom & "&MPOBCodeTo=" & strMPOBCodeTo & _
                       "&MPOBType=" & strMPOBType & "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub BindStatus()

        lstStatus.Items.Add(New ListItem(objHRSetup.mtdGetBonusStatus(objHRSetup.EnumBonusStatus.All), objHRSetup.EnumBonusStatus.All))
        lstStatus.Items.Add(New ListItem(objHRSetup.mtdGetBonusStatus(objHRSetup.EnumBonusStatus.Active), objHRSetup.EnumBonusStatus.Active))
        lstStatus.Items.Add(New ListItem(objHRSetup.mtdGetBonusStatus(objHRSetup.EnumBonusStatus.Deleted), objHRSetup.EnumBonusStatus.Deleted))

    End Sub

End Class
