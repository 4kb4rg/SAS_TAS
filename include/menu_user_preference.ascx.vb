Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class menu_user_preference : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable
    Dim strUserId As String
    
    Sub Page_Load(Sender As Object, E As EventArgs)
        strUserId = Session("SS_USERID")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        End If

        Dim strActiveLeft = "<img src=""../../images/dl.gif"" border=0 align=texttop>"
        Dim strActiveRight = "<img src=""../../images/dr.gif"" border=0 align=texttop>"
        Dim strInActiveLeft = "<img src=""../../images/ll.gif"" border=0 align=texttop>"
        Dim strInActiveRight = "<img src=""../../images/lr.gif"" border=0 align=texttop>"

        Dim strHrefSetLoc As String = ""

        strHrefSetLoc = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/system/user/userpreference.aspx"" target=_self>Colour Preference</a>"
        tblMenu.Rows(0).Cells(0).bgcolor = "#d4d0c8"
        tblMenu.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefSetLoc & strActiveRight
    End Sub
End Class
