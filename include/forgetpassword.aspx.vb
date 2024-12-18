
Imports System
Imports System.Data
Imports System.Web.Mail
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.DateAndTime
Imports Microsoft.VisualBasic

Imports agri.PWSystem
Imports agri.GlobalHdl.clsAccessRights



Public Class forget_password : Inherits Page

    Protected WithEvents tblBefore As HtmlTable
    Protected WithEvents hidReferer As HtmlInputHidden
    Protected WithEvents txtUserId As TextBox
    Protected WithEvents lblSuccess As Label
    Protected WithEvents lblErrNoUser As Label
    Protected WithEvents lblErrNoEmail As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents lblErrSentFail As Label

    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim objSysUser As New agri.PWSystem.clsUser()
    Dim strUserId As String


    Sub Page_Load(Sender As Object, E As EventArgs)
        lblSuccess.Visible = False
        lblErrNoUser.Visible = False
        lblErrNoEmail.Visible = False
        lblErrSentFail.Visible = False

        If Not IsPostBack Then

        End If
    End Sub

    Sub OKBtn_Click(Sender As Object, E As EventArgs)
        Dim objUserDs As New Object
        Dim strOpCode As String = "PWSYSTEM_CLSUSER_USERDETAILS_GET"
        Dim Mail As New MailMessage()
        Dim intErrNo As Integer
        Dim intPos As Integer
        Dim strParam As String
        Dim strUserName As String
        Dim strUserEmail As String
        Dim strPassword As String
        Dim strMessage As String
        Dim strFromEmail As String
        Dim strServerName As String = "mail.thgroup.com.my"
        
        strParam = Trim(txtUserId.Text)
        Try
            intErrNo = objSysUser.mtdGetUser(strOpCode, _
                                             strParam, _
                                             objUserDs, _
                                             True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FORGETPWD_GET_USER&errmesg=" & lblErrMesage.Text & server.urlencode(exp.ToString) & "&redirect=" & Request.QueryString("referer"))
        End Try

        If objUserDs.Tables(0).Rows.Count = 1 Then
            strUserName = Trim(objUserDs.Tables(0).Rows(0).Item("UserName"))
            strUserEmail = Trim(objUserDs.Tables(0).Rows(0).Item("UserEmail"))
            strPassword = Trim(objUserDs.Tables(0).Rows(0).Item("UserPwd"))
            If strUserEmail <> "" Then
                strMessage = "To " & strUserName & ",<P>" & _
                             "You have requested Agrosoft login password on " & Now() & ".<br>" & _
                             "Please destroy this email immediately after memorised the password.<P>" & _
                             "Password: " & strPassword & "<P>" & _
                             "같같같같같같같같같같같같같같같<BR>" & _
                             "<a href=""http://" & strServerName & """>Agrosoft</a> (agrosoft), Auto Mailer"

                intPos = InStr(strServerName, ".")
                If intPos > 0 Then
                    strFromEmail = Mid(strServerName, intPos + 1, Len(strServerName))
                    strFromEmail = "automailer@" & strFromEmail
                Else
                    strFromEmail = "automailer"
                End If

                Try
                    mail.From = strFromEmail
                    mail.To = strUserEmail
                    mail.Subject = "Your Plantware User Password"
                    mail.Body = strMessage
                    mail.BodyFormat = MailFormat.Html
                    SmtpMail.SmtpServer = strServerName
                    SmtpMail.Send(mail)
                Catch Exp As System.Exception
                    lblErrSentFail.Visible = True
                    Exit Sub
                End Try

                lblSuccess.Visible = True
            Else
                lblErrNoEmail.Visible = True
            End If
        Else
            lblErrNoUser.Visible = True
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As EventArgs)
        Response.Redirect(Request.QueryString("referer"))
    End Sub



End Class
