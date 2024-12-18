Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings

Public Class menu_error : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable
    Dim strLangCode As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strLangCode = Session("SS_LANGCODE")

        Dim strActiveLeft = "<img src=""/en/images/dl.gif"" border=0 align=texttop>"
        Dim strActiveRight = "<img src=""/en/images/dr.gif"" border=0 align=texttop>"
        Dim strInActiveLeft = "<img src=""/en/images/ll.gif"" border=0 align=texttop>"
        Dim strInActiveRight = "<img src=""/en/images/lr.gif"" border=0 align=texttop>"
        Dim strHrefError As String

        Select Case LCase(strLangCode)
            Case "en"
                strHrefError = "Error"
            Case Else
                strHrefError = "Error"
        End Select
        
        tblMenu.Rows(0).Cells(0).bgcolor = "#d4d0c8"
        tblMenu.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefError & strActiveRight
    End Sub
End Class
