Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights

Public Class menu_Admin_Data : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strUserId As String
    Dim intADAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strUserId = Session("SS_USERID")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        End If

        Dim strActiveLeft = "<img src=""../../images/dl.gif"" border=0 align=texttop>"
        Dim strActiveRight = "<img src=""../../images/dr.gif"" border=0 align=texttop>"
        Dim strInActiveLeft = "<img src=""../../images/ll.gif"" border=0 align=texttop>"
        Dim strInActiveRight = "<img src=""../../images/lr.gif"" border=0 align=texttop>"

        Dim strScriptPath As String = lcase(Request.ServerVariables("SCRIPT_NAME"))
        Dim arrScriptName As Array = Split(strScriptPath, "/")
        Dim strScriptName As String = arrScriptName(UBound(arrScriptName, 1))
        Dim strHrefDownload As String = ""
        Dim strHrefUpload As String = ""
        Dim strHrefBackup As String = ""

        If strScriptName = "menu_admindata_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If

        intADAR = Session("SS_ADAR")

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADAdmDataTransfer), intADAR) = True) Then
            strHrefDownload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/data/admin_data_download.aspx"" target=_self>Download Reference File</a>"
            strHrefUpload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/data/admin_data_upload.aspx"" target=_self>Upload Reference File</a>"
        Else
            strHrefDownload = "Download Reference File"
            strHrefUpload = "Upload Reference File"
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBackupRestore), intADAR) = True Then
            strHrefBackup = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/admin/data/admin_data_backup.aspx"" target=_self>Database Backup & Restoration</a>"
        Else
            strHrefBackup = "Database Backup & Restoration"
        End If

        tblMenu.Rows(0).Cells(0).BgColor = "#e5e5e5"
        tblMenu.Rows(0).Cells(0).InnerHtml = strInActiveLeft & strHrefDownload & strInActiveRight
        tblMenu.Rows(0).Cells(2).BgColor = "#e5e5e5"
        tblMenu.Rows(0).Cells(2).InnerHtml = strInActiveLeft & strHrefUpload & strInActiveRight
        tblMenu.Rows(0).Cells(4).BgColor = "#e5e5e5"
        tblMenu.Rows(0).Cells(4).InnerHtml = strInActiveLeft & strHrefBackup & strInActiveRight

        Select Case strScriptName
            Case "admin_data_download.aspx"
                tblMenu.Rows(0).Cells(0).BgColor = "#d4d0c8"
                tblMenu.Rows(0).Cells(0).InnerHtml = strActiveLeft & strHrefDownload & strActiveRight
            Case "admin_data_upload.aspx"
                tblMenu.Rows(0).Cells(2).BgColor = "#d4d0c8"
                tblMenu.Rows(0).Cells(2).InnerHtml = strActiveLeft & strHrefUpload & strActiveRight
            Case "admin_data_backup.aspx"
                tblMenu.Rows(0).Cells(4).BgColor = "#d4d0c8"
                tblMenu.Rows(0).Cells(4).InnerHtml = strActiveLeft & strHrefBackup & strActiveRight
        End Select

    End Sub
End Class
