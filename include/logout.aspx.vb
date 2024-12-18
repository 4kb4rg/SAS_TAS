Imports System
Imports System.Web.UI
Imports System.Web.UI.Page

Public Class logout : Inherits Page

    Sub Page_Load(Sender As Object, E As EventArgs)
        
        Session("SS_USERNAME") = ""
        Session("SS_LOCATIONNAME") = ""
        Response.Redirect("/default.aspx")
        Response.Write("<Script language=""Javascript"">parent.banner.location.href = '../../../banner.aspx'</Script>")

    End Sub

End Class
