Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings

Imports agri.GlobalHdl.clsAccessRights
Imports agri.PWSystem.clsConfig

Public Class sysmenu : Inherits Page

    Protected WithEvents lnkSysCfg As Hyperlink
    Protected WithEvents lnkParam As Hyperlink
    Protected WithEvents lnkAppUser As Hyperlink
    Protected WithEvents lnkLangCap As Hyperlink
    Protected WithEvents lnkAdminSetup As Hyperlink
    Protected WithEvents lnkAdminDT As Hyperlink
    Protected WithEvents lnkPreference As Hyperlink
    Protected WithEvents lnkChgPwd As Hyperlink


    Protected WithEvents eFlex As Image

    CONST C_ADMIN = "ADMIN"

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim strCompanyName As String
    Dim strLangCode As String
    Dim strUserId As String
    Dim strUserName As String
    Dim intADAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompanyName = Session("SS_COMPANYNAME")
        strLangCode = Session("SS_LANGCODE")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        intADAR = Session("SS_ADAR")

        If strUserId = "" Then
            Response.Write("<Script language=""Javascript"">parent.right.location.href = '/SessionExpire.aspx'</Script>")
        Else
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUser), intADAR) = True) Or _
               (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADSystemConfig), intADAR) = True) Or _
               (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADLanguageCaption), intADAR) = True) Then
                onLoad_Display()
            Else
                Response.Redirect("/login.aspx")
            End If
        End If

        onload_GetVersion()
        onload_GetImage()

	Response.Write("<Script language=""Javascript"">parent.banner.location.href = '/banner.aspx'</Script>")

        If Not IsPostBack Then
        End If
    End Sub

    Sub onLoad_Display()
       
        'lnkPreference.NavigateURL = "/" & strLangCode & "/system/user/userpreference.aspx?referer=" & Request.ServerVariables("SCRIPT_NAME")
        lnkChgPwd.NavigateURL = "/" & strLangCode & "/changepassword.aspx?referer=/" & strLangCode & "/sysmenu.aspx"
        lnkSysCfg.NavigateURL = "/" & strLangCode & "/system/config/syssetting.aspx"
        lnkParam.NavigateURL = "/" & strLangCode & "/system/config/sys_param_setting.aspx"
        lnkAppUser.NavigateURL = "/" & strLangCode & "/system/user/userlist.aspx"
        lnkLangCap.NavigateURL = "/" & strLangCode & "/system/langcap/Language_Cap.aspx"
        lnkAdminSetup.NavigateURL = "/" & strLangCode & "/menu/menu_admin_page.aspx"
        lnkAdminDT.NavigateURL = "/" & strLangCode & "/menu/menu_admindata_page.aspx"
    End Sub

     Sub onload_GetVersion()
        Dim objVersionDs As New Object()
        Dim strOpCode As String = "ADMIN_CLSSHARE_VERSION_GET"
        Dim strWhere As String
        Dim strOrderBy As String
        Dim intErrNo As Integer

        strWhere = ""
        strOrderBy = " ORDER BY ID DESC"
        Try
            intErrNo = objSysCfg.mtdGetVersion(strOpCode, strWhere, strOrderBy, objVersionDs)
            eFlex.Tooltip = "Version : " & UCase(Trim(objVersionDs.Tables(0).Rows(0).Item("Version")))
        Catch Exp As System.Exception
        End Try
    End Sub
    
    Sub onload_GetImage()
        Dim intErrNo As Integer, Flag As Boolean
        
        Try
            intErrNo = objSysCfg.mtdIsMillware(Flag)
        Catch Exp As System.Exception
        End Try
	   
    End Sub

End Class
