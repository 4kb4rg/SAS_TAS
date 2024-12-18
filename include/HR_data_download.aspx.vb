Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic


Imports agri.GlobalHdl.clsAccessRights

Public Class HR_data_download : Inherits Page

    Protected WithEvents tblDownload As HtmlTable
    Protected WithEvents cbEmpCode As CheckBox
    Protected WithEvents cbDeptCode As CheckBox
    Protected WithEvents cbNationality As CheckBox
    Protected WithEvents cbFunction As CheckBox
    Protected WithEvents cbLevel As CheckBox
    Protected WithEvents cbReligion As CheckBox
    Protected WithEvents cbICType As CheckBox
    Protected WithEvents cbRace As CheckBox
    Protected WithEvents cbSkill As CheckBox
    Protected WithEvents cbShift As CheckBox
    Protected WithEvents cbQualification As CheckBox
    Protected WithEvents cbSubject As CheckBox
    Protected WithEvents cbEvaluation As CheckBox
    Protected WithEvents cbCP As CheckBox
    Protected WithEvents cbSalScheme As CheckBox
    Protected WithEvents cbBank As CheckBox
    Protected WithEvents cbTax As CheckBox
    Protected WithEvents cbHoliday As CheckBox

    Protected WithEvents lblErrGenerate As Label
    Protected WithEvents lblDownloadfile As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lnkSaveTheFile As Hyperlink

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDataTransfer), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            cbEmpCode.Visible = False
            If Not Page.IsPostBack Then
            End If
        End If
    End Sub


    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strParam As String = ""
        Dim strQuery As String = ""

        If cbEmpCode.Checked Then strParam = strParam & "cbEmpCode"
        If cbDeptCode.Checked Then strParam = strParam & "cbDeptCode"
        If cbNationality.Checked Then strParam = strParam & "cbNationality"
        If cbFunction.Checked Then strParam = strParam & "cbFunction"
        If cbLevel.Checked Then strParam = strParam & "cbLevel"
        If cbReligion.Checked Then strParam = strParam & "cbReligion"
        If cbICType.Checked Then strParam = strParam & "cbICType"
        If cbRace.Checked Then strParam = strParam & "cbRace"
        If cbSkill.Checked Then strParam = strParam & "cbSkill"
        If cbQualification.Checked Then strParam = strParam & "cbQualification"
        If cbSubject.Checked Then strParam = strParam & "cbSubject"
        If cbEvaluation.Checked Then strParam = strParam & "cbEvaluation"
        If cbCP.Checked Then strParam = strParam & "cbCP"
        If cbSalScheme.Checked Then strParam = strParam & "cbSalScheme"
        If cbBank.Checked Then strParam = strParam & "cbBank"
        If cbTax.Checked Then strParam = strParam & "cbTax"
        If cbHoliday.Checked Then strParam = strParam & "cbHoliday"
        If cbShift.Checked Then strParam = strParam & "cbShift"

        If strParam = "" Then
            lblErrGenerate.Visible = True
        Else
            strQuery = "empcode=" & cbEmpCode.Checked & "&" & _
                       "deptcode=" & cbDeptCode.Checked & "&" & _
                       "nation=" & cbNationality.Checked & "&" & _
                       "function=" & cbFunction.Checked & "&" & _
                       "level=" & cbLevel.Checked & "&" & _
                       "religion=" & cbReligion.Checked & "&" & _
                       "ictype=" & cbICType.Checked & "&" & _
                       "race=" & cbRace.Checked & "&" & _
                       "skill=" & cbSkill.Checked & "&" & _
                       "shift=" & cbShift.Checked & "&" & _
                       "qualification=" & cbQualification.Checked & "&" & _
                       "subject=" & cbSubject.Checked & "&" & _
                       "evaluation=" & cbEvaluation.Checked & "&" & _
                       "cp=" & cbCP.Checked & "&" & _
                       "salscheme=" & cbSalScheme.Checked & "&" & _
                       "bank=" & cbBank.Checked & "&" & _
                       "tax=" & cbTax.Checked & "&" & _
                       "holiday=" & cbHoliday.Checked & "&"

            Response.Redirect("HR_data_download_savefile.aspx?" & strQuery)
        End If

    End Sub


End Class
