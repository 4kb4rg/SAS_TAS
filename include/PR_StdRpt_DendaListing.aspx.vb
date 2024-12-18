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

Public Class PR_StdRpt_DendaListing : Inherits Page

    Protected RptSelect As UserControl
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents txtDendaCodeFrom As TextBox
    Protected WithEvents txtDendaCodeTo As TextBox
    Protected WithEvents lstDendaType As DropDownList
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
    Dim objLoc As New agri.Admin.clsLoc()
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

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BinddendaType()
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_DENDALIST_GET_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
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


    Sub BinddendaType()
        Dim strText = "All"

        lstDendaType.Items.Add(New ListItem(strText))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BrondolanTerkontaminasi), objPRSetup.EnumPenaltyType.BrondolanTerkontaminasi))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BrondolanTerkontaminasiKG), objPRSetup.EnumPenaltyType.BrondolanTerkontaminasiKG))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BrondolanTerkontaminasiKG), objPRSetup.EnumPenaltyType.BrondolanTerkontaminasiKG))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BrondolanTertinggalBlok), objPRSetup.EnumPenaltyType.BrondolanTertinggalBlok))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BrondolanTertinggalTPH), objPRSetup.EnumPenaltyType.BrondolanTertinggalTPH))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BuahDiperam), objPRSetup.EnumPenaltyType.BuahDiperam))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BuahMatangTerTinggal), objPRSetup.EnumPenaltyType.BuahMatangTerTinggal))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BuahMentah), objPRSetup.EnumPenaltyType.BuahMentah))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BuahTertinggalTPH), objPRSetup.EnumPenaltyType.BuahTertinggalTPH))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BuahTidakLetakTPH), objPRSetup.EnumPenaltyType.BuahTidakLetakTPH))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.EmptyBunch), objPRSetup.EnumPenaltyType.EmptyBunch))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.LongStalk), objPRSetup.EnumPenaltyType.LongStalk))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.Others), objPRSetup.EnumPenaltyType.Others))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.OverPrunning), objPRSetup.EnumPenaltyType.OverPrunning))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.PanenMatahari), objPRSetup.EnumPenaltyType.PanenMatahari))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.PelepahSengkleh), objPRSetup.EnumPenaltyType.PelepahSengkleh))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.PelepahTidakSusun), objPRSetup.EnumPenaltyType.PelepahTidakSusun))
        lstDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.UnderRipe), objPRSetup.EnumPenaltyType.UnderRipe))
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

        If txtDendaCodeFrom.Text = "" Then
            strDendaCodeFrom = ""
        Else
            strDendaCodeFrom = Trim(txtDendaCodeFrom.Text)
        End If

        If txtDendaCodeTo.Text = "" Then
            strDendaCodeTo = ""
        Else
            strDendaCodeTo = Trim(txtDendaCodeTo.Text)
        End If

        If lstDendaType.SelectedItem.Value = "All" Then
            strDendaType = ""
        Else
            strDendaType = Trim(lstDendaType.SelectedItem.Value)
        End If

        strStatus = Trim(lstStatus.SelectedItem.Value)

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_DendaListingPreview.aspx?CompCode=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                       "&lblLocation=" & lblLocation.Text & "&DendaCodeFrom=" & strDendaCodeFrom & "&DendaCodeTo=" & strDendaCodeTo & _
                       "&DendaType=" & strDendaType & "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub BindStatus()
        lstStatus.Items.Add(New ListItem(objPRSetup.mtdGetDendaStatus(objPRSetup.EnumDendaStatus.All), objPRSetup.EnumDendaStatus.All))
        lstStatus.Items.Add(New ListItem(objPRSetup.mtdGetDendaStatus(objPRSetup.EnumDendaStatus.Active), objPRSetup.EnumDendaStatus.Active))
        lstStatus.Items.Add(New ListItem(objPRSetup.mtdGetDendaStatus(objPRSetup.EnumDendaStatus.Deleted), objPRSetup.EnumDendaStatus.Deleted))

    End Sub

End Class
