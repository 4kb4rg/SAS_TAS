Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports agri.GlobalHdl.clsAccessRights

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class NU_data_download : Inherits Page

    Protected WithEvents cbNUBatch As CheckBox
    Protected WithEvents cbCullType As CheckBox
    Protected WithEvents cbAccCls As CheckBox
    Protected WithEvents lblErrGenerate As Label
    Protected WithEvents lblDownloadfile As Label
    Protected WithEvents btnGenerate As ImageButton

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intNUAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intNUAR = Session("SS_NUAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDataTransfer), intNUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If Not Page.IsPostBack Then
            End If
        End If
    End Sub


    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strParam As String = ""
        Dim strQuery As String = ""

        If cbCullType.Checked Then strParam = strParam & "cbCullType"
        If cbAccCls.Checked Then strParam = strParam & "cbAccCls"

        If strParam = "" Then
            lblErrGenerate.Visible = True
        Else
            strQuery = "nubatch=false&" & _
                    "culltype=" & cbCullType.Checked & "&" & _
                    "acccls=" & cbAccCls.Checked

            Response.Redirect("NU_data_download_savefile.aspx?" & strQuery)
        End If

    End Sub


End Class
