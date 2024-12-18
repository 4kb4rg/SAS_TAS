Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.DateAndTime

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.HR
Imports agri.PR
Imports agri.GlobalHdl


Public Class PR_mthend_dailyrollback : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblProcessed As Label
    Protected WithEvents lblErrProcessed As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents txtProDate As TextBox
    Protected WithEvents lblErrProcessDate As Label
    Protected WithEvents lblErrProcessDateDesc As Label
    Protected WithEvents btnSelDate As Image

    Protected WithEvents btnGenerate As ImageButton

    Dim objHR As New agri.HR.clsTrx()
    Dim objPR As New agri.PR.clsMthEnd()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objEmpDs As New Object()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
    Dim strDateFmt As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthDaily), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblPeriod.Text = Trim(strAccMonth) & "/" & Trim(strAccYear)

            If Not Page.IsPostBack Then
                txtProDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
            End If
        End If
    End Sub


    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        lblProcessed.Visible =False
        lblErrProcessed.Visible = False
        lblErrProcessDateDesc.Visible = False
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intNoDays As Integer
        Dim objResult As Object
        Dim objDataSet As Object
        Dim objFormatDate As String
        Dim objActualDate As String

        Dim strOpCd_SP As String = "PR_CLSMTHEND_DAILYROLLBACK_SP"

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtProDate.Text, _
                                       objFormatDate, _
                                       objActualDate) = False Then
            lblErrProcessDateDesc.Text = ""
            lblErrProcessDateDesc.Text = "Format Date " & objFormatDate
            lblErrProcessDateDesc.Visible = True
            exit sub
        End If
        strParam = objActualDate
        strParam = strParam & "|" & strOpCd_SP

        Try
            intErrNo = objPR.mtdDailyRollBack_SP(strCompany, _
                                                 strLocation, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 strUserId, _
                                                 strParam, _
                                                 objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_DAILY_ROLLBACK&errmesg=&redirect=")
        End Try

        If objResult = 1 Then 
            lblProcessed.Visible = True
        Else
            lblErrProcessed.Visible = True
        End If        
    End Sub


End Class
