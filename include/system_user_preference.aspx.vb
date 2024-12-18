Imports System
Imports System.Data
Imports System.Web
Imports System.Web.HttpResponse
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.DateAndTime
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic
Imports agri.PWSystem

Public Class system_user_preference : Inherits Page

    Protected WithEvents lblErrMesage As Label
    Protected WithEvents rb01 As RadioButton
    Protected WithEvents rb02 As RadioButton
    Protected WithEvents rb03 As RadioButton
    Protected WithEvents rb04 As RadioButton
    Protected WithEvents rb05 As RadioButton
    Protected WithEvents rb06 As RadioButton
    Protected WithEvents rb07 As RadioButton

    Dim objSysUser As New agri.PWSystem.clsUser()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strADAR As String

    Sub Page_Load(Sender As Object, E As EventArgs)        
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strLangCode = Session("SS_LANGCODE")
        strUserId = Session("SS_USERID")
        strADAR = Session("SS_ADAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            Session("SS_REFERER") = IIf(Request.QueryString("referer") = "", Session("SS_REFERER"), Request.QueryString("referer"))

            If Not Page.IsPostBack Then
                onload_display()
            Else
                onclick_change()
            End If
        End If
    End Sub


    Sub onload_display()
        Dim objUserDs As New Dataset()
        Dim strOpCode As String = "PWSYSTEM_CLSUSER_USERDETAILS_GET"
        Dim intErrNo As Integer
        Dim strParam As String

        strParam = strUserId
        Try
            intErrNo = objSysUser.mtdGetUser(strOpCode, strParam, objUserDs, True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=COLORPREFERENCE_GET_USER&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userpreference.aspx")
        End Try
        objUserDs.Tables(0).Rows(0).Item("ColorInd") = Trim(objUserDs.Tables(0).Rows(0).Item("ColorInd"))

        Select Case CInt(objUserDs.Tables(0).Rows(0).Item("ColorInd"))
                Case objSysUser.EnumUserColor.SeaBlue
                        rb01.Checked = True
                Case objSysUser.EnumUserColor.SkyBlue
                        rb02.Checked = True
                Case objSysUser.EnumUserColor.RainForestGreen
                        rb03.Checked = True
                Case objSysUser.EnumUserColor.HawaiiLight
                        rb04.Checked = True
                Case objSysUser.EnumUserColor.VioletPurple
                        rb05.Checked = True
                Case objSysUser.EnumUserColor.ChilliRed
                        rb06.Checked = True
                Case objSysUser.EnumUserColor.Yellow
                        rb07.Checked = True
        End Select
    End Sub

    Sub onclick_change()
        Dim strOpCode_UpdUser As String = "PWSYSTEM_CLSUSER_USERDETAILS_UPD"
        Dim strReferer As String = Session("SS_REFERER")
        Dim strParam As String
        Dim intErrNo As String
        Dim strColor As String = ""


        If rb01.Checked Then
            strColor = objSysUser.EnumUserColor.SeaBlue
        ElseIf rb02.Checked then
            strColor = objSysUser.EnumUserColor.SkyBlue
        ElseIf rb03.Checked then
            strColor = objSysUser.EnumUserColor.RainForestGreen
        ElseIf rb04.Checked then
            strColor = objSysUser.EnumUserColor.HawaiiLight
        ElseIf rb05.Checked then
            strColor = objSysUser.EnumUserColor.VioletPurple
        ElseIf rb06.Checked then
            strColor = objSysUser.EnumUserColor.ChilliRed
        ElseIf rb07.Checked then
            strColor = objSysUser.EnumUserColor.Yellow
        End If

        If strColor <> "" Then
            Session("SS_USERCOLOR") = strColor
            Response.Cookies("CK_USERCOLOR").Value = strColor
            Response.Cookies("CK_USERCOLOR").Expires = DateAdd("d", 30, Now())

            Try
                strParam = strUserId & "|||||" & strColor & "||"
                intErrNo = objSysUser.mtdUpdUser(strOpCode_UpdUser, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objSysUser.EnumUserUpdType.Update)                                            
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=COLORPREFERENCE_UPD_USER&errmesg=" & lblErrMesage.Text & "&redirect=system/user/userpreference.aspx")
            End Try
        End If

        Response.Write("<Script language=""Javascript"">parent.banner.location.href = '../../../banner.aspx'</Script>")
        Response.Write("<Script language=""Javascript"">parent.left.location.href = '" & strReferer & "?referer=" & Request.ServerVariables("SCRIPT_NAME") & "'</Script>")
        
        onload_Display()
    End Sub



End Class
