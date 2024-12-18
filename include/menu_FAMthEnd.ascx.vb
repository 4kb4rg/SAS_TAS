Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights

Public Class menu_fa_mthend : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strUserId As String
    Dim intFAAR As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strUserId = Session("SS_USERID")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        End If

        Dim strActiveLeft = "<img src=""../../images/dl.gif"" border=0 align=texttop>"
        Dim strActiveRight = "<img src=""../../images/dr.gif"" border=0 align=texttop>"
        Dim strInActiveLeft = "<img src=""../../images/ll.gif"" border=0 align=texttop>"
        Dim strInActiveRight = "<img src=""../../images/lr.gif"" border=0 align=texttop>"

        Dim strScriptPath As String = LCase(Request.ServerVariables("SCRIPT_NAME"))
        Dim arrScriptName As Array = Split(strScriptPath, "/")
        Dim strScriptName As String = arrScriptName(UBound(arrScriptName, 1))
        Dim strHrefGenDepr As String = ""
        Dim strHrefMthEnd As String = ""

        If strScriptName = "menu_famthend_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If

        intFAAR = Session("SS_FAAR")

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGenDepreciation), intFAAR) = True) Then
            'strHrefGenDepr = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/FA/mthend/FA_mthend_GenDepr.aspx"" target=_self>Generate Depreciation</a>"
            strHrefGenDepr = "Generate Depreciation"
        Else
            strHrefGenDepr = "Generate Depreciation"
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMonthEnd), intFAAR) = True) Then
            strHrefMthEnd = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/FA/mthend/FA_mthend_Process.aspx"" target=_self>Month End Process</a>"
        Else
            strHrefMthEnd = "Month End Process"
        End If

        tblMenu.Rows(0).Cells(0).BgColor = "#e5e5e5"
        tblMenu.Rows(0).Cells(0).InnerHtml = strInActiveLeft & strHrefGenDepr & strInActiveRight
        tblMenu.Rows(0).Cells(2).BgColor = "#e5e5e5"
        tblMenu.Rows(0).Cells(2).InnerHtml = strInActiveLeft & strHrefMthEnd & strInActiveRight

        Select Case strScriptName
            Case "fa_mthend_gendepr.aspx"
                tblMenu.Rows(0).Cells(0).BgColor = "#d4d0c8"
                tblMenu.Rows(0).Cells(0).InnerHtml = strActiveLeft & strHrefGenDepr & strActiveRight
            Case "fa_mthend_process.aspx"
                tblMenu.Rows(0).Cells(2).BgColor = "#d4d0c8"
                tblMenu.Rows(0).Cells(2).InnerHtml = strActiveLeft & strHrefMthEnd & strActiveRight
        End Select
    End Sub
End Class
