Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports agri.GlobalHdl.clsAccessRights

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic


Public Class PU_data_download : Inherits Page

    Protected WithEvents tblDownload As HtmlTable
    Protected WithEvents tblSave As HtmlTable

    Protected WithEvents cbSupplier As CheckBox
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
    Dim inPUAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        inPUAR = Session("SS_PUAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDataTransfer), inPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If Not Page.IsPostBack Then
                tblDownload.Visible = True
                tblSave.Visible = False
            Else
                tblDownload.Visible = True
                tblSave.Visible = False
            End If
        End If
    End Sub

    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strParam As String = ""
        Dim strQuery As String = ""

        If cbSupplier.Checked Then strParam = strParam & "cbSupplier"

        If strParam = "" Then
            lblErrGenerate.Visible = True
            Exit Sub
        Else
            strQuery = "supplier=" & cbSupplier.Checked

            Response.Redirect("PU_data_download_savefile.aspx?" & strQuery)
        End If
    End Sub



End Class
