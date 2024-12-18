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

Public Class PR_StdRpt_CashDenominationList : Inherits Page

    Protected RptSelect As UserControl

    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblLocation As Label
    
    Protected WithEvents txtEmpIDFrom As TextBox
    Protected WithEvents txtEmpIDTo As TextBox
    Protected WithEvents txtGangCode As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtDN1 As TextBox
    Protected WithEvents txtDN2 As TextBox
    Protected WithEvents txtDN3 As TextBox
    Protected WithEvents txtDN4 As TextBox
    Protected WithEvents txtDN5 As TextBox
    Protected WithEvents txtDN6 As TextBox
    Protected WithEvents txtDN7 As TextBox
    Protected WithEvents txtDN8 As TextBox
    Protected WithEvents txtDN9 As TextBox
    Protected WithEvents txtDN10 As TextBox
    Protected WithEvents txtDN11 As TextBox
    Protected WithEvents txtDN12 As TextBox
    Protected WithEvents lblDN1 As Label
    Protected WithEvents lblDN2 As Label
    Protected WithEvents lblDN3 As Label
    Protected WithEvents lblDN4 As Label
    Protected WithEvents lblDN5 As Label
    Protected WithEvents lblDN6 As Label
    Protected WithEvents lblDN7 As Label
    Protected WithEvents lblDN8 As Label
    Protected WithEvents lblDN9 As Label
    Protected WithEvents lblDN10 As Label
    Protected WithEvents lblDN11 As Label
    Protected WithEvents lblDN12 As Label

    Protected WithEvents lblDNMsg As Label

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strLangCode As String
    Dim strParam As String
    Dim strDateFormat As String

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim dr As DataRow

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim ddlDec As DropDownList
        
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        strDateFormat = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")
        
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                SetupUI()
                ddlStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.All), objHRTrx.EnumEmpStatus.All))
                ddlStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.Active), objHRTrx.EnumEmpStatus.Active))
                ddlStatus.Items.Add(New ListItem(objHRTrx.mtdGetEmpStatus(objHRTrx.EnumEmpStatus.Terminated), objHRTrx.EnumEmpStatus.Terminated))
            End If
        End If
    End Sub

    Sub SetupUI()
        txtDN1.Text = "100"
        txtDN2.Text = "50"
        txtDN3.Text = "10"
        txtDN4.Text = "5"
        txtDN5.Text = "2"
        txtDN6.Text = "1"
        txtDN7.Text = "0.50"
        txtDN8.Text = "0.20"
        txtDN9.Text = "0.10"
        txtDN10.Text = "0.05"
        txtDN11.Text = "0.01"
        txtDN12.Text = ""

        lblDN1.Text = "(e.g. " & txtDN1.Text & ")"
        lblDN2.Text = "(e.g. " & txtDN2.Text & ")"
        lblDN3.Text = "(e.g. " & txtDN3.Text & ")"
        lblDN4.Text = "(e.g. " & txtDN4.Text & ")"
        lblDN5.Text = "(e.g. " & txtDN5.Text & ")"
        lblDN6.Text = "(e.g. " & txtDN6.Text & ")"
        lblDN7.Text = "(e.g. " & txtDN7.Text & ")"
        lblDN8.Text = "(e.g. " & txtDN8.Text & ")"
        lblDN9.Text = "(e.g. " & txtDN9.Text & ")"
        lblDN10.Text = "(e.g. " & txtDN10.Text & ")"
        lblDN11.Text = "(e.g. " & txtDN11.Text & ")"
        lblDN12.Text = "" '"(e.g. " & txtDN12.Text & ")"
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim HTR As HtmlTableRow
        HTR = RptSelect.FindControl("TrMthYr")
        HTR.Visible = True
        HTR = RptSelect.FindControl("TrPhyMthYr")
        HTR.Visible = False
        HTR = RptSelect.FindControl("TRFromTo")
        HTR.Visible = False
    End Sub
    
    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAccMonth As String
        Dim strAccYear As String

        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PM_STDRPT_MONTHLY_PROD_SUM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PM_StdRpt_Selection.aspx")
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



    Function GetDec(strVal)
        If Trim(strVal) = "" Then
            Return 0.0
        Else
            Return CDbl(strVal)
        End If
    End Function

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim arrDN As Array
        Dim I As Integer, J As Integer
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        
        Dim tempRpt As DropDownList
        Dim tempAccMonth As DropDownList
        Dim tempAccYear As DropDownList
        Dim tempDecimal As DropDownList
        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        
        Dim strAccMonth As String
        Dim strAccYear As String
        Dim strDecimal As String
        Dim strEmpIDFrom As String
        Dim strEmpIDTo As String
        Dim strGangCode As String
        Dim strStatus As String
        Dim strDN1 As String
        Dim strDN2 As String
        Dim strDN3 As String
        Dim strDN4 As String
        Dim strDN5 As String
        Dim strDN6 As String
        Dim strDN7 As String
        Dim strDN8 As String
        Dim strDN9 As String
        Dim strDN10 As String
        Dim strDN11 As String
        Dim strDN12 As String

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempAccMonth = RptSelect.FindControl("lstAccMonth")
        strAccMonth = Trim(tempAccMonth.SelectedItem.Value)
        tempAccYear = RptSelect.FindControl("lstAccYear")
        strAccYear = Trim(tempAccYear.SelectedItem.Value)
        tempDecimal = RptSelect.FindControl("lstDecimal")
        strDecimal = Trim(tempDecimal.selectedItem.Value)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)

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

        strEmpIDFrom = Server.URLEncode(Trim(txtEmpIDFrom.Text))
        strEmpIDTo = Server.URLEncode(Trim(txtEmpIDTo.Text))
        strGangCode = Server.URLEncode(Trim(txtGangCode.Text))
        strStatus = Server.URLEncode(Trim(ddlStatus.SelectedItem.Value))
        strDN1 = Trim(txtDN1.Text)
        strDN2 = Trim(txtDN2.Text)
        strDN3 = Trim(txtDN3.Text)
        strDN4 = Trim(txtDN4.Text)
        strDN5 = Trim(txtDN5.Text)
        strDN6 = Trim(txtDN6.Text)
        strDN7 = Trim(txtDN7.Text)
        strDN8 = Trim(txtDN8.Text)
        strDN9 = Trim(txtDN9.Text)
        strDN10 = Trim(txtDN10.Text)
        strDN11 = Trim(txtDN11.Text)
        strDN12 = Trim(txtDN12.Text)
        
        lblDNMsg.Visible = False
        If strDN1 = "" And strDN2 = "" And strDN3 = "" And strDN4 = "" And strDN5 = "" And strDN6 = "" And _
           strDN7 = "" And strDN8 = "" And strDN9 = "" And strDN10 = "" And strDN11 = "" And strDN12 = "" Then
            
            lblDNMsg.Text = "Please enter at least 1 Dollar Note."
            lblDNMsg.Visible = True
            Exit Sub
        Else
            lblDNMsg.Text = ""
            arrDN = Split(strDN1 & "|" & strDN2 & "|" & strDN3 & "|" & strDN4 & "|" & strDN5 & "|" & strDN6 & "|" & strDN7 & "|" & strDN8 & "|" & strDN9 & "|" & strDN10 & "|" & strDN11 & "|" & strDN12, "|")
            For I = 0 To arrDN.Length - 1
                If GetDec(arrDN(I)) = 0 And Trim(arrDN(I)) <> "" Then
                    lblDNMsg.Text = "The value for Dollar Note cannot equals to zero."
                End If
            Next
            If lblDNMsg.Text = "" Then
                For I = 0 To arrDN.Length - 2
                    If lblDNMsg.Text = "" And Trim(arrDN(I)) <> "" Then
                        For J = I + 1 To arrDN.Length - 1
                            If Trim(arrDN(J)) <> "" And I < J Then
                                If GetDec(arrDN(I)) < GetDec(arrDN(J)) Then
                                    lblDNMsg.Text = "Please enter the Dollar Note in descending order of the value."
                                    Exit For
                                ElseIf GetDec(arrDN(I)) = GetDec(arrDN(J)) Then
                                    lblDNMsg.Text = "Dollar Note cannot have duplicated value."
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Next
            End If
            
            If lblDNMsg.Text <> "" Then
                lblDNMsg.Visible = True
                Exit Sub
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_CashDenominationListPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&RptID=" & strRptID & _
                       "&RptName=" & strRptName & _
                       "&strAccMonth=" & strAccMonth & _
                       "&strAccYear=" & strAccYear & _
                       "&strEmpIDFrom=" & strEmpIDFrom & _
                       "&strEmpIDTo=" & strEmpIDTo & _
                       "&strGangCode=" & strGangCode & _
                       "&strStatus=" & strStatus & _
                       "&strDecimal=" & strDecimal & _
                       "&strDN1=" & strDN1 & _
                       "&strDN2=" & strDN2 & _
                       "&strDN3=" & strDN3 & _
                       "&strDN4=" & strDN4 & _
                       "&strDN5=" & strDN5 & _
                       "&strDN6=" & strDN6 & _
                       "&strDN7=" & strDN7 & _
                       "&strDN8=" & strDN8 & _
                       "&strDN9=" & strDN9 & _
                       "&strDN10=" & strDN10 & _
                       "&strDN11=" & strDN11 & _
                       "&strDN12=" & strDN12 & _
                       "&lblLocation=" & lblLocation.Text & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
