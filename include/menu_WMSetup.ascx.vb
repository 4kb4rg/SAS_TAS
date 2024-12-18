Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information


Public Class menu_WM_Setup : Inherits UserControl


    Dim strUserId As String
   
    Sub Page_Load(Sender As Object, E As EventArgs)
        strUserId = Session("SS_USERID")
        

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        End If

        
    End Sub

End Class
