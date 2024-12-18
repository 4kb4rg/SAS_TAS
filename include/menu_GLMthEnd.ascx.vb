Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights

Public Class menu_gl_mtdend : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strUserId As String
    Dim intGLAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strUserId = Session("SS_USERID")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        End If

        Dim strActiveLeft = "<img src=""../../images/dl.gif"" border=0 align=texttop>"
        Dim strActiveRight = "<img src=""../../images/dr.gif"" border=0 align=texttop>"
        Dim strInActiveLeft = "<img src=""../../images/ll.gif"" border=0 align=texttop>"
        Dim strInActiveRight = "<img src=""../../images/lr.gif"" border=0 align=texttop>"

        Dim strScriptPath As String = lcase(Request.ServerVariables("SCRIPT_NAME"))
        Dim arrScriptName As Array = Split(strScriptPath, "/")
        Dim strScriptName As String = arrScriptName(UBound(arrScriptName, 1))
        Dim strHrefGCDist As String = ""
        Dim strHrefJrnMthEnd As String = ""
        Dim strHrefMthEnd As String = ""
        Dim strHrefDayEnd As String = ""
        Dim strHrefFSEnd As String = ""

        If strScriptName = "menu_glmthend_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If

        intGLAR = Session("SS_GLAR")

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = True) Then
            strHrefDayEnd = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/GL/mthend/GL_DayEnd_Process.aspx"" target=_self>Month End - Trial</a>"
        Else
            strHrefDayEnd = "Month End - Trial"
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = True) Then
            strHrefFSEnd = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/GL/mthend/GL_MthEnd_FSProcess.aspx"" target=_self>Financial Statement Process</a>"
        Else
            strHrefFSEnd = "Financial Statement Process"
        End If

        ' If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMthEndGCDist), intGLAR) = True) Then
            ' strHrefGCDist = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/GL/mthend/GL_MthEnd_GCDist.aspx"" target=_self>General Charges Distribution</a>"
        ' Else
            ' strHrefGCDist = "General Charges Distribution"
        ' End If

        ' If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJrnMonthEnd), intGLAR) = True) Then
            ' strHrefJrnMthEnd = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/GL/mthend/GL_MthEnd_Journal.aspx"" target=_self>Journal Adjustment Process</a>"
        ' Else
            ' strHrefJrnMthEnd = "Journal Adjustment Process"
        ' End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = True) Then
            'strHrefMthEnd = "Month End Process"
			strHrefMthEnd = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/GL/mthend/GL_MthEnd_Process.aspx"" target=_self>Month End Process</a>"
        Else
            strHrefMthEnd = "Month End Process"
        End If

        

        tblMenu.Rows(0).Cells(0).BgColor = "#e5e5e5"
        tblMenu.Rows(0).Cells(0).InnerHtml = strInActiveLeft & strHrefDayEnd & strInActiveRight
        tblMenu.Rows(0).Cells(2).BgColor = "#e5e5e5"
        tblMenu.Rows(0).Cells(2).InnerHtml = strInActiveLeft & strHrefFSEnd & strInActiveRight
        ' tblMenu.Rows(0).Cells(4).BgColor = "#e5e5e5"
        ' tblMenu.Rows(0).Cells(4).InnerHtml = strInActiveLeft & strHrefGCDist & strInActiveRight
        ' tblMenu.Rows(0).Cells(6).BgColor = "#e5e5e5"
        ' tblMenu.Rows(0).Cells(6).InnerHtml = strInActiveLeft & strHrefJrnMthEnd & strInActiveRight
        tblMenu.Rows(0).Cells(8).BgColor = "#e5e5e5"
        tblMenu.Rows(0).Cells(8).InnerHtml = strInActiveLeft & strHrefMthEnd & strInActiveRight

        Select Case strScriptName
            Case "gl_dayend_process.aspx"
                tblMenu.Rows(0).Cells(0).BgColor = "#d4d0c8"
                tblMenu.Rows(0).Cells(0).InnerHtml = strActiveLeft & strHrefDayEnd & strActiveRight
            Case "gl_mthend_fsprocess.aspx"
                tblMenu.Rows(0).Cells(2).BgColor = "#d4d0c8"
                tblMenu.Rows(0).Cells(2).InnerHtml = strActiveLeft & strHrefFSEnd & strActiveRight
            Case "gl_mthend_gcdist.aspx", "gl_mthend_gcdist_loc.aspx"
                tblMenu.Rows(0).Cells(4).BgColor = "#d4d0c8"
                tblMenu.Rows(0).Cells(4).InnerHtml = strActiveLeft & strHrefGCDist & strActiveRight
            Case "gl_mthend_journal.aspx"
                tblMenu.Rows(0).Cells(6).BgColor = "#d4d0c8"
                tblMenu.Rows(0).Cells(6).InnerHtml = strActiveLeft & strHrefJrnMthEnd & strActiveRight
            Case "gl_mthend_process.aspx"
                tblMenu.Rows(0).Cells(8).BgColor = "#d4d0c8"
                tblMenu.Rows(0).Cells(8).InnerHtml = strActiveLeft & strHrefMthEnd & strActiveRight
            
        End Select
    End Sub
End Class
