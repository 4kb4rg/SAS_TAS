Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsAccessRights
Imports agri.PR.clsYearEnd

Public Class PR_yearend_Process : Inherits Page

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents btnProceed As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblYearEnd As Label
    Protected WithEvents lblErrYearEnd As Label
    Protected WithEvents lstYearEnd As DropDownList

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objYearEnd As New agri.PR.clsYearEnd()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRYearEnd), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblYearEnd.Visible = False
            lblErrYearEnd.Visible = False
            If Not Page.IsPostBack Then
                BindEAYearList()
            End If
        End If
    End Sub

    Sub BindEAYearList()
        Dim CurrDate As Date
        Dim CurrYear As Integer
        Dim intCntAddYr As Integer = 1
        Dim intCntMinYr As Integer = 5
        Dim NewAddCurrYear As Integer
        Dim NewMinCurrYear As Integer
        Dim intCntddlYr As Integer = 0

        CurrDate = Today
        CurrYear = Year(CurrDate)

        While intCntMinYr <> 0
            intCntMinYr = intCntMinYr - 1
            NewMinCurrYear = CurrYear - intCntMinYr
            lstYearEnd.Items.Add(NewMinCurrYear)
        End While

        For intCntAddYr = 1 To 5
            NewAddCurrYear = CurrYear + intCntAddYr
            lstYearEnd.Items.Add(NewAddCurrYear)
        Next

        For intCntddlYr = 0 To lstYearEnd.Items.Count - 1
            If lstYearEnd.Items(intCntddlYr).Text = strAccYear Then
                lstYearEnd.SelectedIndex = intCntddlYr
            End If
        Next
    End Sub

    Sub btnProceed_Click(Sender As Object, E As ImageClickEventArgs)
        Dim intErrNo As Integer

        Try 
            intErrNo = objYearEnd.mtdEA(strCompany, _
                                        strLocation, _
                                        strAccMonth, _
                                        strAccYear, _
                                        lstYearEnd.SelectedItem.Value)
        Catch Exp As System.Exception 
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_YEAREND_PROCESS&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If intErrNo = 0 Then
            lblYearEnd.Visible = True
        Else
            lblErrYearEnd.Visible = True
        End If
    End Sub



End Class
