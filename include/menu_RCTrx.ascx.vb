Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsAccessRights

Public Class menu_RC_Trx : Inherits UserControl

    Protected WithEvents tblMenu As HtmlTable

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strUserId As String
    Dim intADAR As Integer
    
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
        Dim strDA As String = ""
        Dim strJrn As String = ""

        If strScriptName = "menu_rctrx_page.aspx" Then
            strActiveLeft = "<img src=""../images/dl.gif"" border=0 align=texttop>"
            strActiveRight = "<img src=""../images/dr.gif"" border=0 align=texttop>"
            strInActiveLeft = "<img src=""../images/ll.gif"" border=0 align=texttop>"
            strInActiveRight = "<img src=""../images/lr.gif"" border=0 align=texttop>"
        End If

        intADAR = Session("SS_ADAR")

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDispatchAdvice), intADAR) = True) Then
            strDA = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/RC/trx/RC_Trx_DAList.aspx"" target=_self>Dispatch Advice</a>"
        Else
            strDA = "Dispatch Advice"
        End IF

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCJournal), intADAR) = True) Then
            strJrn = "<a class=mt-t href=""/" & Session("SS_LANGCODE") & "/RC/trx/RC_Trx_JournalList.aspx"" target=_self>Journal</a>"
        Else
            strJrn = "Journal"
        End IF

        tblMenu.Rows(0).Cells(0).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(0).innerHTML = strInActiveLeft & strDA & strInActiveRight
        tblMenu.Rows(0).Cells(2).bgcolor = "#e5e5e5"
        tblMenu.Rows(0).Cells(2).innerHTML = strInActiveLeft & strJrn & strInActiveRight

        Select Case strScriptName
            Case "rc_trx_dalist.aspx", "rc_trx_dadet.aspx"
                    tblMenu.Rows(0).Cells(0).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(0).innerHTML = strActiveLeft & strDA & strActiveRight
            Case "rc_trx_journallist.aspx", "rc_trx_journaldet.aspx"
                    tblMenu.Rows(0).Cells(2).bgcolor = "#d4d0c8"
                    tblMenu.Rows(0).Cells(2).innerHTML = strActiveLeft & strJrn & strActiveRight
        End Select
    End Sub
End Class
