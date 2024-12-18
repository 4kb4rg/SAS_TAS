Imports System
Imports System.Web.UI
Imports System.Web.UI.Page
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class error_message : Inherits Page

    Protected WithEvents lblErrHeader As Label
    Protected WithEvents lblErrCodeName As Label
    Protected WithEvents lblErrMesgName As Label
    Protected WithEvents lblErrCode As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents redirectBtn As Button

    Dim strLangCode As String
    Dim strErrCode As String
    Dim strErrMesg As String
    Dim strRedirect As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strLangCode = Session("SS_LANGCODE")
        strErrCode = Request.QueryString("errcode")
        strErrMesg = Request.QueryString("errmesg")
        strRedirect = Request.QueryString("redirect")

        If strRedirect = "" Then
            redirectBtn.Visible = False
        End If

        Select Case LCase(strLangCode)
            Case "en"
                lblErrHeader.Text = "ERROR"
                lblErrCodeName.Text = "Error Code: "
                lblErrMesgName.Text = "Error Message: "
                lblErrCode.Text = IIf(strErrCode = "", "UNKNOWN CODE", strErrCode)
                lblErrMessage.Text = IIf(strErrMesg = "", _
                                         "There was an error on page '<b>" & Request.ServerVariables("HTTP_REFERER") & "</b>'. Please contact system administrator for assitance.", _
                                         strErrMesg & " Please contact system administrator for assistance.")
            Case Else
                lblErrHeader.Text = "ERROR"
                lblErrCodeName.Text = "Error Code: "
                lblErrMesgName.Text = "Error Message: "
                lblErrCode.Text = IIf(strErrCode = "", "UNKNOWN CODE", strErrCode)
                lblErrMessage.Text = IIf(strErrMesg = "", _
                                         "There was an error on page '" & Request.ServerVariables("HTTP_REFERER") & "'. Please contact system administrator for assitance.", _
                                         strErrMesg & " Please contact system administrator for assistance.")
        End Select
    End Sub

    Sub redirectBtn_Click(ByVal Sender As Object, ByVal E As EventArgs)
        If strRedirect <> "" Then
            Response.Redirect("/" & strLangCode & "/" & strRedirect)
        End If
    End Sub


    Sub En()
    End Sub
End Class
