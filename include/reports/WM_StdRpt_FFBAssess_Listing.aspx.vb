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

Public Class WM_StdRpt_FFBAssess_Listing : Inherits Page

    Protected RptSelect As UserControl

    Dim objWM As New agri.WM.clsReport()
    Dim objWMSetup As New agri.WM.clsSetup()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lstStatus As DropDownList

    Protected WithEvents txtTicketNoFrom As TextBox
    Protected WithEvents txtTicketNoTo As TextBox
    Protected WithEvents txtInspDate As TextBox
    Protected WithEvents txtInspDateTo As TextBox
    Protected WithEvents txtRipe As TextBox
    Protected WithEvents txtOverRipe As TextBox
    Protected WithEvents txtUnderRipe As TextBox
    Protected WithEvents txtUnripe As TextBox
    Protected WithEvents txtEmpty As TextBox
    Protected WithEvents txtRotten As TextBox
    Protected WithEvents txtPoor As TextBox
    Protected WithEvents txtSmall As TextBox
    Protected WithEvents txtLongStalk As TextBox
    Protected WithEvents txtTotal As TextBox
    Protected WithEvents txtGradedPercent As TextBox
    Protected WithEvents txtUngradableBunch As TextBox
    Protected WithEvents txtContamination As TextBox
    Protected WithEvents txtOthers As TextBox

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
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
        strLocType = Session("SS_LOCTYPE")

        lblDate.Visible = False
        lblDateFormat.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatusList()
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender

        Dim SAccMthYr As HtmlTableRow
        
        SAccMthYr =  RptSelect.FindControl("TrMthYr")
        SAccMthYr.visible = true

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_FFBASSESSLST_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/WM_StdRpt_Selection.aspx")
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



    Sub BindStatusList()
         
        lstStatus.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumFFBAssessStatus.All), objWMTrx.EnumFFBAssessStatus.All))
        lstStatus.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumFFBAssessStatus.Active), objWMTrx.EnumFFBAssessStatus.Active))
        lstStatus.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumFFBAssessStatus.Deleted), objWMTrx.EnumFFBAssessStatus.Deleted))

    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strTicketNoFrom As String
        Dim strTicketNoTo As String
        Dim strInspDate As String
        Dim strInspDateTo As String
        Dim strRipe As String
        Dim strOverRipe As String
        Dim strUnderRipe As String
        Dim strUnripe As String
        Dim strEmpty As String
        Dim strRotten As String
        Dim strPoor As String
        Dim strSmall As String
        Dim strLongStalk As String
        Dim strTotal As String
        Dim strGradedPercent As String
        Dim strUngradableBunch As String
        Dim strStatus As String

        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strAccMth1 As String
        Dim strAccYr1 As String

        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim tempAccMth As DropDownList
        Dim tempAccYear As DropDownList

        Dim strParam As String
        Dim strDateSetting As String


        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String = ""
        Dim objDateTo As String = ""

        Dim strContamination As String
        Dim strOthers As String

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = tempRpt.SelectedItem.Value.Trim
        strRptName = tempRpt.SelectedItem.Text.Trim
        tempDec = RptSelect.FindControl("lstDecimal")
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = tempUserLoc.Value.Trim
        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strAccMth1 = tempAccMth.SelectedItem.Text
        tempAccYear = RptSelect.FindControl("lstAccYear")
        strAccYr1 = tempAccYear.SelectedItem.Text
        strDec = tempDec.SelectedItem.Text.Trim


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

        strTicketNoFrom = txtTicketNoFrom.Text.Trim
        strTicketNoTo = txtTicketNoTo.Text.Trim
        strInspDate = txtInspDate.Text.Trim
        strInspDateTo = txtInspDateTo.Text.Trim
        strStatus = lstStatus.SelectedItem.Text.Trim

        strRipe = Server.UrlEncode(txtRipe.Text.Trim)
        strOverRipe = Server.UrlEncode(txtOverRipe.Text.Trim)
        strUnderRipe = Server.UrlEncode(txtUnderRipe.Text.Trim)
        strUnripe = Server.UrlEncode(txtUnripe.Text.Trim)
        strEmpty = Server.UrlEncode(txtEmpty.Text.Trim)
        strRotten = Server.UrlEncode(txtRotten.Text.Trim)
        strPoor = Server.UrlEncode(txtPoor.Text.Trim)
        strSmall = Server.UrlEncode(txtSmall.Text.Trim)
        strLongStalk = Server.UrlEncode(txtLongStalk.Text.Trim)
        strTotal = Server.UrlEncode(txtTotal.Text.Trim)
        strGradedPercent = Server.UrlEncode(txtGradedPercent.Text.Trim)
        strUngradableBunch = Server.UrlEncode(txtUngradableBunch.Text.Trim)
        strContamination = Server.UrlEncode(txtContamination.Text.Trim)
        strOthers = Server.UrlEncode(txtOthers.Text.Trim)
        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_WEIGHBRIDGE_TRANSACTION_LIST_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/WM_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strInspDate = "" And strInspDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strInspDate, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strInspDateTo, objDateFormat, objDateTo) = True Then
            
                Response.Write("<Script Language=""JavaScript"">window.open(""WM_StdRpt_FFBAssess_ListingPreview.aspx?Type=Print&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                                "&lblLocation=" & lblLocation.Text & "&ddlAccMth=" & strAccMth1 & "&ddlAccYear=" & strAccYr1 & _
                                "&TicketNoFrom=" & strTicketNoFrom & "&TicketNoTo=" & strTicketNoTo & "&InspDateFrom=" & objDateFrom & "&InspDateTo=" & objDateTo & _
                                "&ripe=" & strRipe & "&overripe=" & strOverRipe & "&underripe=" & strUnderRipe & "&unripe=" & strUnripe & "&empty=" & strEmpty & "&Rotten=" & strRotten & "&poor=" & strPoor & "&small=" & strSmall & _
                                "&longstalk=" & strLongStalk & "&total=" & strTotal & "&gradedpercent=" & strGradedPercent & "&UngradableBunch=" & strUngradableBunch & _
                                "&contamination=" & strContamination & "&others=" & strOthers & _
                                "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            
            Response.Write("<Script Language=""JavaScript"">window.open(""WM_StdRpt_FFBAssess_ListingPreview.aspx?Type=Print&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                            "&lblLocation=" & lblLocation.Text & "&ddlAccMth=" & strAccMth1 & "&ddlAccYear=" & strAccYr1 & _
                            "&TicketNoFrom=" & strTicketNoFrom & "&TicketNoTo=" & strTicketNoTo & "&InspDateFrom=" & "" & "&InspDateTo=" & "" & _
                            "&ripe=" & strRipe & "&overripe=" & strOverRipe & "&underripe=" & strUnderRipe & "&unripe=" & strUnripe & "&empty=" & strEmpty & "&Rotten=" & strRotten & "&poor=" & strPoor & "&small=" & strSmall & _
                            "&longstalk=" & strLongStalk & "&total=" & strTotal & "&gradedpercent=" & strGradedPercent & "&UngradableBunch=" & strUngradableBunch & _
                            "&contamination=" & strContamination & "&others=" & strOthers & _
                            "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

     
        End If
    End Sub

End Class
 
