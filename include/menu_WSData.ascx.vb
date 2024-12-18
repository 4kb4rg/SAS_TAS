Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights
Imports agri.PWSystem.clsConfig

Public Class menu_WS_Data : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim strUserId As String
    Dim intWSAR As Integer

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
        Dim strHrefWSTrx As String = ""

        If strScriptName = "menu_wsdata_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If
        
        intWSAR = Session("SS_WSAR")

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDataTransfer), intWSAR) = True) Then
            strHrefDownload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/WS/data/WS_data_download.aspx"" target=_self>Download Reference File</a>"
            strHrefUpload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/WS/data/WS_data_upload.aspx"" target=_self>Upload Reference File</a>"
        Else
            strHrefDownload = "Download Reference File"
            strHrefUpload = "Upload Reference File"
        End IF

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDownload), intWSAR) = True) Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.Workshop), Session("SS_AUTOGLPOSTING")) = False Then
                strHrefWSTrx = "<a class=mt-t target=_blank href=""/" & Session("SS_LANGCODE") & "/WS/data/WS_data_trx_savefile.aspx"" target=_self>Download Workshop Transaction</a>"
            Else
                strHrefWSTrx = "Download Workshop Transaction"
            End If
        Else
            strHrefWSTrx = "Download Workshop Transaction"
        End IF

        tblMenu.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefDownload & strInActiveRight
        tblMenu.Rows(0).Cells(2).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(2).innerHTML = strInActiveLeft & strHrefUpload & strInActiveRight
        'tblMenu.Rows(0).Cells(4).bgcolor = "#e5e5e5"
        'tblMenu.Rows(0).Cells(4).innerHTML = strInActiveLeft & strHrefWSTrx & strInActiveRight

        Select Case strScriptName
            Case "ws_data_download.aspx"
                    tblMenu.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefDownload & strActiveRight
            Case "ws_data_upload.aspx"
                    tblMenu.Rows(0).Cells(2).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(2).innerHTML = strActiveLeft & strHrefUpload & strActiveRight
        End Select

    End Sub
End Class
