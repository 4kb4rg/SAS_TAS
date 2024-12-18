Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
'Imports Microsoft.VisualBasic.Strings
'Imports Microsoft.VisualBasic.Information

'Imports agri.GlobalHdl.clsAccessRights

Public Class menu_session_expire : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable

    'Dim objAR As New agri.GlobalHdl.clsAccessRights()
    'Dim intCTAR As Integer
    
    Sub Page_Load(Sender As Object, E As EventArgs)
        Dim strActiveLeft = "<img src=""/en/images/dl.gif"" border=0 align=texttop>"
        Dim strActiveRight = "<img src=""/en/images/dr.gif"" border=0 align=texttop>"
        Dim strInActiveLeft = "<img src=""/en/images/ll.gif"" border=0 align=texttop>"
        Dim strInActiveRight = "<img src=""/en/images/lr.gif"" border=0 align=texttop>"

        'Dim strScriptPath As String = lcase(Request.ServerVariables("SCRIPT_NAME"))
        'Dim arrScriptName As Array = Split(strScriptPath, "/")
        'Dim strScriptName As String = arrScriptName(UBound(arrScriptName, 1))
        Dim strHrefSessExp As String = ""

        'If strScriptName = "menu_ctsetup_page.aspx" Then
        '    strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
        '    strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
        '    strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
        '    strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        'End If

        'intCTAR = Session("SS_CTAR")

        'If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTItem), intCTAR) = True) Then
        '    strHrefSetLoc = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/system/user/setlocation.aspx"" target=_self>Set Location</a>"
        'Else
            strHrefSessExp = "Session Expire"
        'End IF

        'tblMenu.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        'tblMenu.Rows(0).Cells(0).innerHTML = strInActiveLeft & strHrefSetLoc & strInActiveRight

        'Select Case strScriptName
        '    Case "ct_canteenitem.aspx", "ct_canteenitem_detail.aspx"
                    tblMenu.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(0).innerHTML = strActiveLeft & strHrefSessExp & strActiveRight
        'End Select
    End Sub
End Class
