Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights

Public Class menu_AP_Data : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim strUserId As String
    Dim intAPAR As Integer

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
        Dim strHrefAPTrx As String = ""

        If strScriptName = "menu_apdata_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If

        intAPAR = Session("SS_APAR")

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APDownload), intAPAR) = True) Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetAutoDataTransfer(objSysCfg.EnumAutoDataTransfer.AccountPayable), Session("SS_AUTOGLPOSTING")) = False Then
                strHrefAPTrx = "<a class=mt-t target=_blank href=""/" & Session("SS_LANGCODE") & "/AP/data/AP_data_trx_savefile.aspx"" target=_self>Download Account Payable Transaction</a>"
            Else
                strHrefAPTrx = "Download Account Payable Transaction"
            End If
        Else
            strHrefAPTrx = "Download Account Payable Transaction"
        End IF

        'tblMenu.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        'tblMenu.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefAPTrx & strInActiveRight

        'Select Case strScriptName
            'Case "bi_data_download.aspx"
            '        tblMenu.Rows(0).Cells(0).bgcolor = "#d4d0c8"
            '        tblMenu.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefDownload & strActiveRight
        'End Select

    End Sub
End Class
