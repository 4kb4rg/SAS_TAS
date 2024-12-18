Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Public Class menu_ap_mtdend : Inherits UserControl

    Sub Page_Load(Sender As Object, E As EventArgs)
        Dim strUserId As String
        strUserId = Session("SS_USERID")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        End If
    End Sub
End Class
