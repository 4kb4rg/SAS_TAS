
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls


Imports agri.GlobalHdl.clsAccessRights
Imports agri.PWSystem.clsConfig

Public Class menudetail1_stp : Inherits Page


    Const C_ADMIN = "ADMIN"

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim strCompanyName As String
    Dim strLangCode As String
    Dim strUserId As String
    Dim strUserName As String
    Dim intADAR As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompanyName = Session("SS_COMPANYNAME")
        strLangCode = Session("SS_LANGCODE")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        intADAR = Session("SS_ADAR")

        If strUserId = "" Then
            Response.Write("<Script language=""Javascript"">parent.right.location.href = '/SessionExpire.aspx'</Script>")
        Else

            onLoad_Display()

        End If

    End Sub

    Sub onLoad_Display()

        'lnkChgPwd.NavigateURL = "/" & strLangCode & "/changepassword.aspx?referer=/" & strLangCode & "/sysmenu.aspx"
        'lnkSysCfg.NavigateURL = "/" & strLangCode & "/system/config/syssetting.aspx"
        'lnkParam.NavigateURL = "/" & strLangCode & "/system/config/sys_param_setting.aspx"
        'lnkAppUser.NavigateURL = "/" & strLangCode & "/system/user/userlist.aspx"
        'lnkLangCap.NavigateURL = "/" & strLangCode & "/system/langcap/Language_Cap.aspx"
        'lnkAdminSetup.NavigateURL = "/" & strLangCode & "/menu/menu_admin_page.aspx"
        'lnkAdminDT.NavigateURL = "/" & strLangCode & "/menu/menu_admindata_page.aspx"
    End Sub



End Class
