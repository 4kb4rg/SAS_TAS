Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page

Imports agri.GlobalHdl.clsAccessRights

Public Class RC_trx_download : Inherits Page

    Protected WithEvents tblDownload As HtmlTable
    Protected WithEvents tblSave As HtmlTable
    Protected WithEvents cbStkTrf As CheckBox
    Protected WithEvents cbStkRcv As CheckBox
    Protected WithEvents cbGRN As CheckBox
    Protected WithEvents cbBill As CheckBox
    Protected WithEvents cbJournal As CheckBox
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
    Dim intADAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDataTransfer), intADAR) = False Then
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

        If cbStkTrf.Checked Then strParam = strParam & "cbStkTrf"
        If cbStkRcv.Checked Then strParam = strParam & "cbStkRcv"
        If cbGRN.Checked Then strParam = strParam & "cbGRN"
        If cbBill.Checked Then strParam = strParam & "cbBill"
        If cbJournal.Checked Then strParam = strParam & "cbJournal"

        If strParam = "" Then
            lblErrGenerate.Visible = True
            Exit Sub
        Else
            strQuery = "stktrf=" & cbStkTrf.Checked
            strQuery = strQuery & "stkrcv=" & cbStkRcv.Checked
            strQuery = strQuery & "grn=" & cbGRN.Checked
            strQuery = strQuery & "&bill=" & cbBill.Checked
            strQuery = strQuery & "&jrn=" & cbJournal.Checked

            Response.Redirect("RC_trx_download_savefile.aspx?" & strQuery)
        End If
    End Sub

End Class
