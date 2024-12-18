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

Public Class PR_StdRpt_DaftarAbsensi : Inherits Page
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents BtnDateFrom As Image
    Protected WithEvents BtnDateTo As Image
    Protected RptSelect As UserControl
    
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents txtDateFrom as TextBox
    Protected WithEvents txtDateTo as TextBox
    Protected WithEvents lblDateFromFmt As Label
    Protected WithEvents lblDateToFmt As Label
    Protected WithEvents lblErrDate As Label
    Protected WithEvents lblvaldate As Label
    Protected WithEvents lblErrAttdDate As Label
    Protected WithEvents lblErrAttdDateDesc As Label
    Protected WithEvents lblErrMaxDate As Label
    
    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strPhyMonth As String
    Dim strPhyYear As String


    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Dim strDateFmt As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            lblErrAttdDate.visible = False
            lblErrAttdDateDesc.visible = False
            lblErrMaxDate.Visible = False
            If Not Page.IsPostBack Then
                onload_GetLangCap()
            End If
        End If

    End Sub
    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        Dim ucTrDecimal As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

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
        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strParam As String
        Dim strMonth as string 
        Dim strDate as string 
        Dim strYear as string 
        Dim strFromDate As String = ""
        Dim strToDate As String = ""
        Dim objFormatDate As String
        Dim strDuration As String = "0"
        Dim strFStartDate As String
        Dim strFEndDate As String

        if len(trim(txtDateFrom.Text)) < 10 or len(trim(txtDateTo.Text)) < 10 then 
            lblvaldate.visible = True
            exit sub
        else
            lblvaldate.visible = false
        end if 

        If trim(txtDateFrom.Text) <> "" And trim(txtDateTo.Text) <> "" Then
            If objGlobal.mtdValidInputDate(strDateFmt, _
                                            txtDateFrom.Text, _
                                            objFormatDate, _
                                            strFromDate) = False  Or _
            objGlobal.mtdValidInputDate(strDateFmt, _
                                            txtDateTo.Text, _
                                            objFormatDate, _
                                            strToDate) = False Then
                lblvaldate.visible = True
                Exit Sub
            End If
        End If

        if trim(txtDateFrom.Text) <> "" then 
            strDate = Left(txtDateFrom.Text, 2)
            strMonth = Right(Left(txtDateFrom.text, 5), 2)
            strYear = Right(txtDateFrom.Text, 4)

            lblDateFromFmt.Text = strMonth & "/" & strDate & "/" & strYear
        else
            lblDateFromFmt.Text = strPhyMonth & "/" & "01" & "/" & strPhyYear
        end if
        
        If Trim(txtDateTo.Text) <> "" Then
            strDate = Left(txtDateTo.Text, 2)
            strMonth = Right(Left(txtDateTo.Text, 5), 2)
            strYear = Right(txtDateTo.Text, 4)

            lblDateToFmt.Text = strMonth & "/" & strDate & "/" & strYear
        Else
            If (strPhyMonth = "1") Or (strPhyMonth = "3") Or (strPhyMonth = "5") Or (strPhyMonth = "7") Or (strPhyMonth = "8") Or (strPhyMonth = "10") Or (strPhyMonth = "12") Then
                lblDateToFmt.Text = strPhyMonth & "/" & "31" & "/" & strPhyYear
            End If
            If (strPhyMonth = "4") Or (strPhyMonth = "6") Or (strPhyMonth = "11") Then
                lblDateToFmt.Text = strPhyMonth & "/" & "30" & "/" & strPhyYear
            End If
            If (strPhyMonth = "2") And (CDbl(strPhyYear) Mod 4) > 0 Then
                lblDateToFmt.Text = strPhyMonth & "/" & "28" & "/" & strPhyYear
            End If
            If (strPhyMonth = "2") And (CDbl(strPhyYear) Mod 4) = 0 Then
                lblDateToFmt.Text = strPhyMonth & "/" & "29" & "/" & strPhyYear
            End If
        End If

        If Trim(lblDateFromFmt.Text) = "" Then
            lblErrDate.Visible = True
            Exit Sub
        Else
            lblErrDate.Visible = False
        End If

        strFStartDate = lblDateFromFmt.Text
        strFEndDate = lblDateToFmt.Text
        strDuration = CalcDuration(strFStartDate, strFEndDate)

        If Val(strDuration) > 31 Then
            lblErrMaxDate.Visible = True
            Exit Sub
        End If

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
            ElseIf Left(strUserLoc, 1) = "," Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 1)
            ElseIf Right(strUserLoc, 1) = "," Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 1)
            End If


        End If


        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_DaftarAbsensiPreview.aspx?CompCode=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                       "&lblLocation=" & lblLocation.Text & "&SelAccMonth=" & strddlAccMth & "&SelAccYear=" & strddlAccYr & "&SelPhyMonth=" & strPhyMonth & "&SelPhyYear=" & strPhyYear & "&SelDateFrom=" & lblDateFromFmt.Text & "&SelDateTo=" & lblDateToFmt.Text & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
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

    Function CalcDuration(ByVal vstrStartDt As String, ByVal vstrEndDate As String) As String
        Dim dblDay As Double

        dblDay = DateDiff(DateInterval.Day, CDate(vstrStartDt), CDate(vstrEndDate))

        CalcDuration = dblDay
    End Function

End Class
