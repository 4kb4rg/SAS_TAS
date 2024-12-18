
Imports System.Data

Imports agri.PWSystem.clsUser
Imports agri.GlobalHdl.clsAccessRights
Imports agri.GL.clsSetup
Imports agri.PWsystem.clsConfig

Public Class login : Inherits Page

    Protected WithEvents txtUserId As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents loginBtn As System.Web.UI.WebControls.Button
    Protected WithEvents lblLoginResult As System.Web.UI.WebControls.Label
    Protected WithEvents validateUserID As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents validatePassword As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblErrLoginFail As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrAccInactive As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrAccDeleted As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrMesage As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrLicense As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrExpire As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrExpired As System.Web.UI.WebControls.Label
    Protected WithEvents continueBtn As System.Web.UI.WebControls.Button

    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysUser As New agri.PWSystem.clsUser()
    Dim objSysCfg As New agri.PWsystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()

    Dim strUserId As String
    Dim strPassword As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        lblLoginResult.Text = ""
        If Not IsPostBack Then
            If Session("SS_USERID") = "" Then
            Else
                txtUserId.Text = Session("SS_USERID")
            End If
            Session.Abandon()
        End If

        Session("SS_COMPANYURL") = LCase(Request.ServerVariables("HTTP_HOST"))
    End Sub

    Sub loginBtn_Click(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strOpCode_Login As String = "PWSYSTEM_CLSUSER_USER_LOGIN_GET"
        Dim strOpCodes As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim objLoginResult As New Object()
        Dim objLoginResultDay As New Object()
        Dim objUserReference As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strError As String = ""

        strUserId = txtUserId.Text
        strPassword = txtPassword.Text
        strParam = strUserId & "|" & strPassword
        Try
            intErrNo = objSysUser.mtdValidateLogin(strOpCode_Login, _
                                                   strOpCodes, _
                                                   strParam, _
                                                   objLoginResult, _
                                                   objLoginResultDay, _
                                                   objUserReference)
        Catch Exp As System.Exception
            objLoginResult = -1
            strError = "System.Exception Message: " & Exp.Message
        End Try


        Select Case objLoginResult
            Case EnumLoginResult.Success
                onSuccess_LoadSession(objUserReference)
                onSuccess_Redirect()
            Case EnumLoginResult.Fail
                lblLoginResult.Text = lblErrLoginFail.Text
                Session.Abandon()
            Case EnumLoginResult.Inactive
                lblLoginResult.Text = lblErrAccInactive.Text
                Session.Abandon()
            Case EnumLoginResult.Deleted
                lblLoginResult.Text = lblErrAccDeleted.Text
                Session.Abandon()
            Case EnumLoginResult.HectarageLicense
                lblLoginResult.Text = lblErrLicense.Text
                Session.Abandon()
            Case -1
                lblLoginResult.Text = lblErrMesage.Text
                Session.Abandon()
                Response.Write("<script language=""javascript"">alert('" & Replace(strError, "'", "''") & "');</script>")
            
        End Select
    End Sub

    Sub continueBtn_Click(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strOpCode_Login As String = "PWSYSTEM_CLSUSER_USER_LOGIN_GET"
        Dim strOpCodes As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim objLoginResult As New Object()
        Dim objLoginResultDay As New Object()
        Dim objUserReference As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strError As String = ""

        strUserId = txtUserId.Text
        strPassword = txtPassword.Text
        strParam = strUserId & "|" & strPassword
        Try
            intErrNo = objSysUser.mtdValidateLogin(strOpCode_Login, _
                                                   strOpCodes, _
                                                   strParam, _
                                                   objLoginResult, _
                                                   objLoginResultDay, _
                                                   objUserReference)
        Catch Exp As System.Exception
            objLoginResult = -1
            strError = "System.Exception Message: " & Exp.Message
        End Try

        Select Case objLoginResult
            Case EnumLoginResult.Success
                onSuccess_LoadSession(objUserReference)
                onSuccess_Redirect()
            Case EnumLoginResult.Fail
                lblLoginResult.Text = lblErrLoginFail.Text
                Session.Abandon()
            Case EnumLoginResult.Inactive
                lblLoginResult.Text = lblErrAccInactive.Text
                Session.Abandon()
            Case EnumLoginResult.Deleted
                lblLoginResult.Text = lblErrAccDeleted.Text
                Session.Abandon()
            Case EnumLoginResult.HectarageLicense
                lblLoginResult.Text = lblErrLicense.Text
                Session.Abandon()
            Case -1
                lblLoginResult.Text = lblErrMesage.Text
                Session.Abandon()
                Response.Write("<script language=""javascript"">alert('" & Replace(strError, "'", "''") & "');</script>")
            Case Else
                onSuccess_LoadSession(objUserReference)
                onSuccess_Redirect()
        End Select

    End Sub

    Sub onSuccess_LoadSession(ByVal arrUserReference As Array)
        Dim objAccCodeLen As New DataSet()
        Dim strOpCd_AccCodeLen As String = "GL_CLSSETUP_ACCCODELEN_GET"
        Dim intErrNo As Integer
        Dim strParam As String = "|"
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd_AccCodeLen, _
                                                   strParam, _
                                                   EnumGLMasterType.AccountCode, _
                                                   objAccCodeLen)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FIND_ACCCODE_LEN_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objAccCodeLen.Tables(0).Rows.Count > 0 Then
            Session("SS_ACCCODELEN") = objAccCodeLen.Tables(0).Rows(0).Item("AccCodeLen").Trim()
        Else
            Session("SS_ACCCODELEN") = "32"
        End If
        Session("SS_USERID") = Trim(arrUserReference(0))
        Session("SS_LASTUSERID") = Trim(arrUserReference(0))
        Session("SS_USERNAME") = Trim(arrUserReference(1))
        Session("SS_USERCOLOR") = Trim(arrUserReference(2))
        Session("SS_ADAR") = CInt(arrUserReference(3))
        Session("SS_SH_USER_ADAR") = CInt(arrUserReference(3))
        Session("SS_USRLEVEL") = CInt(arrUserReference(4))
        Session("SS_COMPANY") = arrUserReference(5)
        Session("SS_COMPANYNAME") = arrUserReference(6)
        Session("SS_LANGCODE") = IIf(arrUserReference(7) = "", "en", arrUserReference(7))
        arrUserReference(7) = IIf(arrUserReference(8) = "", "1", Trim(arrUserReference(8)))
        Session("SS_DATEFMT") = objSysCfg.mtdGetDateFormat(arrUserReference(8))
        Session("SS_CONFIGSETTING") = arrUserReference(9)
        Session("SS_COACENTRALIZED") = arrUserReference(11)
        Session("SS_FILTERPERIOD") = arrUserReference(12)
        Session("SS_ROUNDNO") = arrUserReference(13)
        
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(EnumConfig.BlockCostLevel), CInt(Session("SS_CONFIGSETTING"))) = True Then
            Session("SS_COSTLEVEL") = "block"
        Else
            Session("SS_COSTLEVEL") = "subBlock"
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(EnumConfig.BlockYieldLevel), CInt(Session("SS_CONFIGSETTING"))) = True Then
            Session("SS_YIELDLEVEL") = "block"
        Else
            Session("SS_YIELDLEVEL") = "subblock"
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(EnumConfig.AutoEstateYieldRate), CInt(Session("SS_CONFIGSETTING"))) = True Then
            Session("SS_AUTO_ESTYIELLDRATE") = 1
        Else
            Session("SS_AUTO_ESTYIELLDRATE") = 0
        End If


        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(EnumConfig.BlockCostLevel), CInt(Session("SS_CONFIGSETTING"))) = True Then
            Session("SS_BLOCK_CHARGE_VISIBLE") = False
            Session("SS_BLOCK_CHARGE_DEFAULT") = 1
        Else
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(EnumConfig.ChargingToBlock), CInt(Session("SS_CONFIGSETTING"))) = True Then
                Session("SS_BLOCK_CHARGE_VISIBLE") = True
                If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(EnumConfig.DefaultChargeToBlock), CInt(Session("SS_CONFIGSETTING"))) = True Then
                    Session("SS_BLOCK_CHARGE_DEFAULT") = 0
                Else
                    Session("SS_BLOCK_CHARGE_DEFAULT") = 1
                End If
            Else
                Session("SS_BLOCK_CHARGE_VISIBLE") = False
                Session("SS_BLOCK_CHARGE_DEFAULT") = 1
            End If
        End If
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(EnumConfig.InterEstateCharging), CInt(Session("SS_CONFIGSETTING"))) = True Then
            Session("SS_INTER_ESTATE_CHARGING") = True
        Else
            Session("SS_INTER_ESTATE_CHARGING") = False
        End If

        Response.Cookies("CK_USERCOLOR").Value = Session("SS_USERCOLOR")
        Response.Cookies("CK_USERCOLOR").Expires = DateAdd("d", 30, Now())
    End Sub


    Sub onSuccess_Redirect()
        Dim intADAR As Integer = Session("SS_ADAR")
        Dim intUserAR As Integer = objAR.mtdGetAccessRights(EnumADAccessRights.ADUser)
        Dim intSysCfgAR As Integer = objAR.mtdGetAccessRights(EnumADAccessRights.ADSystemConfig)
        Dim intLangCapAR As Integer = objAR.mtdGetAccessRights(EnumADAccessRights.ADLanguageCaption)

        If ((intUserAR And intADAR) = intUserAR) Or _
           ((intSysCfgAR And intADAR) = intSysCfgAR) Or _
           ((intLangCapAR And intADAR) = intLangCapAR) Then
            Response.Redirect("/" & Session("SS_LANGCODE") & "/sysadm.aspx")
        Else

            'Remark,
            'Response.Redirect("/" & Session("SS_LANGCODE") & "/appmenu.aspx")
            Response.Redirect("/" & Session("SS_LANGCODE") & "/system/user/setlocation.aspx")
            'Response.Write("<Script language=""Javascript"">parent.left.location.href = '/" & strLangCode & "/menu.aspx'</Script>")


        End If
    End Sub

    
End Class
