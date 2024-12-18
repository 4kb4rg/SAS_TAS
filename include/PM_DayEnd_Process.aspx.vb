Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.DateAndTime

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PM.clsMthEnd
Imports agri.Admin.clsShare

Public Class PM_dayend_Process : Inherits Page

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblErrProcess As Label
    Protected WithEvents lblSuccess As Label
    Protected WithEvents lbltoday As Label
    Protected WithEvents lblErrDayEnd As Label
    Protected WithEvents lblErrDate As Label
    Protected WithEvents txtDayEnd As TextBox

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objMthEndDs As New agri.PM.clsMthEnd()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strDateFmt As String
    Dim intPMAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strDateFmt = Session("SS_DATEFMT")
        intPMAR = Session("SS_PMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDayEnd), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrProcess.Visible = False
            lblErrDayEnd.Visible = False
            lblSuccess.Visible = False
            lblErrDate.Visible = False
            If Not Page.IsPostBack Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub onLoad_Display()
        lbltoday.Text = objGlobal.GetShortDate(strDateFmt, Now())
        lblAccPeriod.Text = strAccMonth & "/" & strAccYear
        If txtDayEnd.Text = "" Then
            txtDayEnd.Text = objGlobal.GetShortDate(strDateFmt, DateAdd("d", -1, Now()))
        End If
    End Sub

    Sub btnProceed_Click(Sender As Object, E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim strParam As String
        Dim objResult As Integer
        Dim objFormatDate As String
        Dim objActualDate As String

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtDayEnd.Text, _
                                       objFormatDate, _
                                       objActualDate) = False Then
            lblErrDayEnd.Text = objFormatDate
            lblErrDayEnd.Visible = True
            Exit Sub
        End If

        If DateDiff("d", objGlobal.GetLongDate(Now()), objActualDate) > 0 Then
            lblErrDate.Visible = True
            Exit Sub
        End If

        Try 
            strParam = objGlobal.EnumModule.MillProduction & "|" & objActualDate
            intErrNo = objMthEndDs.mtdDayEndProcess(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objResult)
        Catch Exp As System.Exception 
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_DAYEND_PROCESS&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If objResult = 1 Then 
            lblSuccess.Visible = True
            onLoad_Display()
        Else
            lblErrProcess.Visible = True
        End If        
    End Sub

End Class
