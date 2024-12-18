
Imports System
Imports System.Data
Imports System.Collections
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.Drawing.Color

Public Class session_expire : Inherits Page

    Sub Page_Load(Sender As Object, E As EventArgs)
        Session.Abandon()
    End Sub

    Sub loginBtn_Click(Sender As Object, E As EventArgs)
        Response.Redirect("/default.aspx")
    End Sub

End Class
