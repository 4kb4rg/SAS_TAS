Imports System
Imports System.Web
Imports System.Web.HttpResponse
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.PWSystem

Public Class preference_handler : Inherits UserControl

    Dim objSysUser As New agri.PWSystem.clsUser()

    Dim strUserId As String
    Dim strLangCode As String
    Dim strColor As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        'strColor = Session("SS_USERCOLOR")

        'If strColor = "" Then
        'Try
        'strColor = Request.Cookies("CK_USERCOLOR").Value
        'Catch Exp As System.Exception
        strColor = "1"  '--set to sea blue color
        'End Try
        strLangCode = "en"
        'End If

        If Not Page.IsPostBack Then
            '--do nothing
        End If

        onload_display()
    End Sub

    Sub onload_display()
        'Select Case CInt(strColor)
        'Case objSysUser.EnumUserColor.SeaBlue
        '    Response.Write("<link rel=""STYLESHEET"" type=""text/css"" href=""/" & strLangCode & "/include/css/seablue_01.css"">")
        'Case objSysUser.EnumUserColor.SkyBlue
        '    Response.Write("<link rel=""STYLESHEET"" type=""text/css"" href=""/" & strLangCode & "/include/css/skyblue_02.css"">")
        'Case objSysUser.EnumUserColor.RainForestGreen
        '    Response.Write("<link rel=""STYLESHEET"" type=""text/css"" href=""/" & strLangCode & "/include/css/rainforestgreen_03.css"">")
        'Case objSysUser.EnumUserColor.HawaiiLight
        '    Response.Write("<link rel=""STYLESHEET"" type=""text/css"" href=""/" & strLangCode & "/include/css/hawaiilight_04.css"">")
        'Case objSysUser.EnumUserColor.VioletPurple
        '    Response.Write("<link rel=""STYLESHEET"" type=""text/css"" href=""/" & strLangCode & "/include/css/violetpurple_05.css"">")
        'Case objSysUser.EnumUserColor.ChilliRed
        '    Response.Write("<link rel=""STYLESHEET"" type=""text/css"" href=""/" & strLangCode & "/include/css/chillired_06.css"">")
        'Case objSysUser.EnumUserColor.Yellow
        '    Response.Write("<link rel=""STYLESHEET"" type=""text/css"" href=""/" & strLangCode & "/include/css/yellow_07.css"">")
        'Case Else
        '    Response.Write("<link rel=""STYLESHEET"" type=""text/css"" href=""/" & strLangCode & "/include/css/seablue_01.css"">")
        'End Select
        Response.Write(Chr(10) & Chr(13))
        Response.Write("<Script language=""Javascript"" src=""/" & strLangCode & "/include/script/jscript.js""></Script>")
        Response.Write(Chr(10) & Chr(13))
    End Sub


End Class
