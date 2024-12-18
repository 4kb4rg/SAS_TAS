Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class menu_reports : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable

    Sub Page_Load(Sender As Object, E As EventArgs)
        Dim strActiveLeft = "<img src=""/en/images/dl.gif"" border=0 align=texttop>"
        Dim strActiveRight = "<img src=""/en/images/dr.gif"" border=0 align=texttop>"
        Dim strInActiveLeft = "<img src=""/en/images/ll.gif"" border=0 align=texttop>"
        Dim strInActiveRight = "<img src=""/en/images/lr.gif"" border=0 align=texttop>"

        Dim strHrefSessExp As String = ""

        strHrefSessExp = "Reports"
        tblMenu.Rows(0).Cells(0).bgcolor = "#d4d0c8"
        tblMenu.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefSessExp & strActiveRight
    End Sub
End Class
