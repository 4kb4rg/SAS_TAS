
Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction


Public Class System_user_DailyControl_Det : Inherits Page

    Protected WithEvents ddlUser As DropDownList
    Protected WithEvents DDLLocation As DropDownList
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents TxtDay As TextBox
    Protected WithEvents lblUserID As Label
    Protected WithEvents LblLocation As Label
    Protected WithEvents LblLocName As Label

    Protected WithEvents validateDesc As RequiredFieldValidator
    Protected hidRecStatus As HtmlInputHidden
    Protected hidOriVehCode As HtmlInputHidden

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsUom()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLTrx As New agri.GL.ClsTrx()
    Dim ObjOk As New agri.GL.ClsTrx()

    Dim objVehDs As New Object()
    Dim objVehTypeDs As New Object()
    Dim objUOMDs As New Object()
    Dim objAccDs As New Object()
    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strDateFMt As String

    Dim strSelectedVehicleCode As String = ""
    Dim intStatus As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strSelectedUserId As String
    Dim strSelectedLoc As String
    Dim nLevel As Integer


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strDateFMt = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
            'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intGLAR) = False Then
            '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedUserId = Trim(IIf(Request.QueryString("UsrLevel") <> "", Request.QueryString("UsrLevel"), Request.Form("UsrLevel")))
            If Not IsPostBack Then

                'If strSelectedVehicleCode <> "" Then
                '    onLoad_Display()
                '    onLoad_BindButton()
                'Else
                '    onLoad_BindButton()
                'End If
                If RTrim(Session("USERLEVEL")) <> vbNullString Then
                    lblUserID.Text = RTrim(Session("USERLEVEL"))
                    LblLocation.Text = RTrim(Session("USERLOC"))
                    onLoad_Display(lblUserID.Text, LblLocation.Text)
                End If

                BindUserLevel()
                BindLocation()
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            End If
            End If
    End Sub

    Sub onLoad_Display(ByVal pUserid As String, ByVal pLoc As String)
        Dim strOpCd As String = "PWSYSTEM_CLSUSER_USERDAILY_CONTROL_DETAIL_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objDetail As New Object()
        Dim nLevelValue As Integer

        nLevelValue = GetLevelNumberUser(pUserid)
        strParamName = "USRLEVEL|LOCCODE"
        strParamValue = nLevelValue & "|" & LblLocation.Text

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objDetail)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_GET_BY_VEHCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objDetail.Tables(0).Rows(0).Item(0) <> "" Then
            LblLocName.Text = "(" & objDetail.Tables(0).Rows(0).Item("Description") & ")"
            TxtDay.Text = objDetail.Tables(0).Rows(0).Item("MaximumDay")
            lblDateCreated.Text = objDetail.Tables(0).Rows(0).Item("CreateDate")
            lblLastUpdate.Text = objDetail.Tables(0).Rows(0).Item("UpdateDate")
            lblUpdatedBy.Text = objDetail.Tables(0).Rows(0).Item("UpdateId")

            DDLLocation.Visible = False
            ddlUser.Visible = False
            LblLocation.Visible = True
            lblUserID.Visible = True
            LblLocName.Visible = True
        Else
            DDLLocation.Visible = True
            ddlUser.Visible = True
            LblLocation.Visible = False
            lblUserID.Visible = False
            LblLocName.Visible = False
        End If
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_VehicleDet.aspx")
        End Try

    End Sub

    Sub BindUserLevel()
        ddlUser.Items.Clear()
        ddlUser.Items.Add("ALL")
        ddlUser.Items.Add("User")
        ddlUser.Items.Add("Supervisor")
        ddlUser.Items.Add("Manager")
        ddlUser.Items.Add("General Manager")
        ddlUser.Items.Add("VP/CEO")
    End Sub

    Function GetLevelNumberUser(ByVal pLeveName As String) As Integer
        Dim nValLevel As Integer
        Select Case pLeveName
            Case "User"
                nValLevel = 0
            Case "Supervisor"
                nValLevel = 1
            Case "Manager"
                nValLevel = 2
            Case "General Manager"
                nValLevel = 3
            Case "VP/CEO"
                nValLevel = 4
        End Select
        Return nValLevel
    End Function

    Sub BindLocation()
        Dim strOpCd As String = "PWSYSTEM_CLSUSER_USERDAILY_CONTROL_GET_LOCATION_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet
        Dim strParamValue As String = ""
        Dim StrParamName As String = ""
        Dim objTransDs As New Object()

        StrParamName = ""
        strParamValue = ""

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objTransDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = objTransDs.Tables(0).NewRow()
        dr("LocCode") = ""
        'dr("_Description") = lblSelect.Text & strAccountTag
        objTransDs.Tables(0).Rows.InsertAt(dr, 0)

        DDLLocation.DataSource = objTransDs.Tables(0)
        DDLLocation.DataValueField = "LocCode"
        DDLLocation.DataTextField = "Description"
        DDLLocation.DataBind()
        DDLLocation.SelectedIndex = intSelectedIndex

        If Not objTransDs Is Nothing Then
            objTransDs = Nothing
        End If
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PWSYSTEM_CLSUSER_USERDAILY_CONTROL_UPD"
        Dim strOpCd_Add As String = "PWSYSTEM_CLSUSER_USERDAILY_CONTROL_ADD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim strParamValue As String = ""

        If TxtDay.Text.Trim = vbNullString Or CDbl(0 & TxtDay.Text) = 0 Then
            MsgBox("D")
            validateDesc.Text = "Please Input Maximum Day...!"
            validateDesc.Visible = True
        Else
            validateDesc.Visible = False
        End If

        nLevel = GetLevelNumberUser(IIf(lblDateCreated.Text = "", ddlUser.SelectedItem.Text, lblUserID.Text))

        If Len(lblDateCreated.Text) = 0 Then
            strParam = "USRLVL|LOCCODE|MAXDAY|CD|UPD|UPID"
            strParamValue = nLevel & "|" & IIf(lblDateCreated.Text = "", DDLLocation.SelectedItem.Value, LblLocation.Text) & "|" & TxtDay.Text & "|" & Date.Now & "|" & Date.Now & "|" & strUserId

            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Add, strParam, strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_CHECK_FOR_DUPLICATION&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        Else
            strParam = "USRLVL|LOCCODE|MAXDAY|UPD"
            strParamValue = nLevel & "|" & IIf(lblDateCreated.Text = "", DDLLocation.SelectedItem.Value, LblLocation.Text) & "|" & TxtDay.Text & "|" & Date.Now

            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Upd, strParam, strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_CHECK_FOR_DUPLICATION&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        End If

        Response.Redirect("userDailyControl.aspx")

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("userDailyControl.aspx")
    End Sub

    Sub Delete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument

        Dim strOpCode As String = "PWSYSTEM_CLSUSER_USERDAILY_CONTROL_DEL"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer

        nLevel = GetLevelNumberUser(IIf(lblDateCreated.Text = "", ddlUser.SelectedItem.Text, lblUserID.Text))
        strParamName = "USRLVL|LOCCODE"
        strParamValue = nLevel & "|" & IIf(lblDateCreated.Text = "", DDLLocation.SelectedItem.Text, LblLocation.Text)

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        Response.Redirect("userDailyControl.aspx")

    End Sub

End Class
