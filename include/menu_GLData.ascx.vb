Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights

Public Class menu_GL_Data : Inherits UserControl

    Protected WithEvents tblMenu1 As HtmlTable
    Protected WithEvents tblMenu2 As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim strUserId As String
    Dim intGLAR As Integer

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
        Dim strHrefDwTrx As String = ""
        Dim strHrefUpTrx As String = ""
        Dim strHrefDwnFlatFile as string = ""

        If strScriptName = "menu_gldata_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If
        
        intGLAR = Session("SS_GLAR")

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLDataTransfer), intGLAR) = True) Then
            strHrefDownload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/GL/data/GL_data_download.aspx"" target=_self>Download Reference File</a>"
            strHrefUpload = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/GL/data/GL_data_upload.aspx"" target=_self>Upload Reference File</a>"
            strHrefDwTrx = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/GL/data/GL_trx_download.aspx"" target=_self>Download GL Transactions</a>"
            strHrefUpTrx = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/GL/data/GL_trx_upload.aspx"" target=_self>Upload GL Transactions</a>"
            strHrefDwnFlatFile = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/GL/data/GL_data_download_FlatFile.aspx"" target=_self>Download PeopleSoft ASCII File</a>"
        Else
            strHrefDownload = "Download Reference File"
            strHrefUpload = "Upload Reference File"
            strHrefDwTrx = "Download GL Transactions"
            strHrefUpTrx = "Upload GL Transactions"
            strHrefDwnFlatFile = "Download PeopleSoft ASCII File"
        End IF

        'If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLUpload), intGLAR) = True) Then
        '    If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.GeneralLedger), Session("SS_AUTOGLPOSTING")) = False Then
        '        strHrefDwTrx = "<a class=mt-t target=_blank href=""/" & Session("SS_LANGCODE") & "/GL/data/GL_data_trx_savefile.aspx"" target=_self>Download General Ledger Transaction</a>"
        '    Else
        '        strHrefDwTrx = "Download General Ledger Transaction"
        '    End If
        '    strHrefUpTrx = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/GL/data/GL_data_trx_upload.aspx"" target=_self>Upload Transaction</a>"
        'Else
        '    strHrefUpTrx = "Upload Transaction"
        'End IF

        tblMenu1.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefDownload & strInActiveRight
        tblMenu1.Rows(0).Cells(2).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(2).innerHTML = strInActiveLeft & strHrefUpload & strInActiveRight
        tblMenu1.Rows(0).Cells(4).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(4).innerHTML = strInActiveLeft & strHrefDwTrx & strInActiveRight
        tblMenu1.Rows(0).Cells(6).bgcolor = "#e5e5e5"
        tblMenu1.Rows(0).Cells(6).innerHTML = strInActiveLeft & strHrefUpTrx & strInActiveRight
        tblMenu2.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        tblMenu2.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefDwnFlatFile & strInActiveRight

        Select Case strScriptName
            Case "gl_data_download.aspx"
                    tblMenu1.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefDownload & strActiveRight
            Case "gl_data_upload.aspx"
                    tblMenu1.Rows(0).Cells(2).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(2).innerHTML = strActiveLeft & strHrefUpload & strActiveRight
            Case "gl_trx_download.aspx"
                    tblMenu1.Rows(0).Cells(4).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(4).innerHTML = strActiveLeft & strHrefDwTrx & strActiveRight
            Case "gl_trx_upload.aspx"
                    tblMenu1.Rows(0).Cells(6).bgcolor = "#d4d0c8"
                    tblMenu1.Rows(0).Cells(6).innerHTML = strActiveLeft & strHrefUpTrx & strActiveRight
            Case "gl_data_download_flatfile.aspx"
                    tblMenu2.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                    tblMenu2.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefDwnFlatFile & strActiveRight
        End Select

    End Sub
End Class
