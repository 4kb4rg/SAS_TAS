Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsAccessRights


Public Class PR_data_download : Inherits Page

    Protected WithEvents tblDownload As HtmlTable

    Protected WithEvents cbADGroup As CheckBox
    Protected WithEvents cbContractor As CheckBox
    Protected WithEvents cbAttd As CheckBox
    Protected WithEvents cbAttdIncScheme As CheckBox
    Protected WithEvents cbAttdHarvScheme As CheckBox
    Protected WithEvents cbDiff As CheckBox
    Protected WithEvents cbPayDiv As CheckBox
    Protected WithEvents cbMAC As CheckBox

    Protected WithEvents lblErrGenerate As Label
    Protected WithEvents lblDownloadfile As Label
    Protected WithEvents btnGenerate As ImageButton

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDataTransfer), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If Not Page.IsPostBack Then
            End If
        End If
    End Sub


    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strParam As String = ""
        Dim strQuery As String = ""

        If cbADGroup.Checked Then strParam = strParam & "cbADGroup"
        If cbContractor.Checked Then strParam = strParam & "cbContractor"
        If cbAttd.Checked Then strParam = strParam & "cbAttd"
        If cbAttdIncScheme.Checked Then strParam = strParam & "cbAttdIncScheme"
        If cbAttdHarvScheme.Checked Then strParam = strParam & "cbAttdHarvScheme"
        If cbDiff.Checked Then strParam = strParam & "cbDiff"
        If cbPayDiv.Checked Then strParam = strParam & "cbPayDiv"
        If cbMAC.Checked Then strParam = strParam & "cbMAC"

        If strParam = "" Then
            lblErrGenerate.Visible = True
        Else
            strQuery = "adgroup=" & cbADGroup.Checked & "&" & _
                       "contractor=" & cbContractor.Checked & "&" & _
                       "attd=" & cbAttd.Checked & "&" & _
                       "attdincscheme=" & cbAttdIncScheme.Checked & "&" & _
                       "harvincscheme=" & cbAttdHarvScheme.Checked & "&" & _
                       "diff=" & cbDiff.Checked & "&" & _
                       "paydiv=" & cbPayDiv.Checked & "&" & _
                       "mac=" & cbMAC.Checked

            Response.Redirect("PR_data_download_savefile.aspx?" & strQuery)
        End If
    End Sub


End Class
