Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsAccessRights

Public Class GL_trx_download : Inherits Page

    Protected WithEvents tblDownload As HtmlTable
    Protected WithEvents ddlAccMonth As DropDownList
    Protected WithEvents ddlAccYear As DropDownList
    Protected WithEvents btnGenerate As ImageButton

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intGLAR As Integer
    Dim strAccMonth  As Integer
    Dim strAccYear As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intGLAR = Session("SS_GLAR")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLDataTransfer), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If Not Page.IsPostBack Then
                BindYear()
                BindMonth()
            End If
        End If
    End Sub

    Sub BindYear() 
        Dim intYear As Integer = CInt(strAccYear)
        Dim intCnt As Integer

        For intCnt = intYear - 3 To intYear + 3
            ddlAccYear.Items.Add(New ListItem(intCnt, intCnt))
        Next
        ddlAccYear.SelectedIndex = 3
    End Sub 

    Sub BindMonth()
        Dim intMonth As Integer = CInt(strAccMonth)
        Dim intCnt As Integer
        
        For intCnt = 1 to 12
            ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            If intMonth = intCnt Then
                ddlAccMonth.SelectedIndex = intCnt - 1
            End If
        Next
    End Sub

    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strParam As String = ""
        Dim strQuery As String = ""

        Response.Redirect("GL_trx_download_savefile.aspx?m=" & ddlAccMonth.SelectedItem.Value & "&y=" & ddlAccYear.SelectedItem.Value)
    End Sub


End Class
