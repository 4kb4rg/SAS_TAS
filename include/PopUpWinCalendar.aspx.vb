Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class PopUpWinCal : Inherits Page

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected WithEvents Literal1 As Literal
    Protected WithEvents Calendar1 As Calendar

    Dim txtDate As String
    Dim strDateFMT As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strDateFMT = Session("SS_DATEFMT")
    End Sub

    Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDate = objGlobal.GetShortDate(strDateFMT, Calendar1.SelectedDate)
        Dim strjscript As String = "<script language=""javascript"">"
        strjscript = strjscript & "window.opener." & HttpContext.Current.Request.QueryString("formname") & ".value = '" & txtDate & "';window.opener." & HttpContext.Current.Request.QueryString("formname") & ".focus();window.close();"
        strjscript = strjscript & "</script" & ">"
        Literal1.Text = strjscript
    End Sub

    Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs)
        If e.Day.Date = DateTime.Now().ToString("d") Then
            e.Cell.BackColor = System.Drawing.Color.LightGray
        End If
    End Sub

End Class

