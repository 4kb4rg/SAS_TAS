Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Globalization


Public Class FA_setup_AssetPermitDetails : Inherits Page

    Dim objFASetup As New agri.FA.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblOper As Label
    Protected WithEvents lblDupMsg As Label

    Protected WithEvents lblAssetCodeTag As Label
    Protected WithEvents txtAssetCode As TextBox
    Protected WithEvents lblAssetCodeErr As Label
    Protected WithEvents lblAssetAddPermTag As Label
    Protected WithEvents cbAssetAddPerm As CheckBox
    Protected WithEvents lblAssetGenDeprPermTag As Label
    Protected WithEvents cbAssetGenDeprPerm As CheckBox
    Protected WithEvents lblAssetManDeprPermTag As Label
    Protected WithEvents cbAssetManDeprPerm As CheckBox
    Protected WithEvents lblAssetDispPermTag As Label
    Protected WithEvents cbAssetDispPerm As CheckBox
    Protected WithEvents lblAssetWOPermTag As Label
    Protected WithEvents cbAssetWOPerm As CheckBox

    Protected WithEvents lblCode As Label
    Protected WithEvents lblTitle As Label

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnBack As ImageButton

    Dim strParam As String
    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intFAAR As Integer
    Dim strDateFormat As String
    Dim strOppCd_GET As String = "FA_CLSSETUP_ASSETPERMIT_GET"
    Dim strOppCd_ADD As String = "FA_CLSSETUP_ASSETPERMIT_ADD"
    Dim strOppCd_UPD As String = "FA_CLSSETUP_ASSETPERMIT_UPD"
    Dim intConfigSetting As Integer

    Dim strAssetCode As String
    Dim intAssetAddPerm As Integer
    Dim intAssetGenDeprPerm As Integer
    Dim intAssetManDeprPerm As Integer
    Dim intAssetDispPerm As Integer
    Dim intAssetWOPerm As Integer

    Dim strSelAssetCode As String
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")
        intFAAR = Session("SS_FAAR")
        strDateFormat = Session("SS_DATEFMT")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAPermissionSetup), intFAAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblAssetCodeErr.Visible = False

            onload_GetLangCap()

            strSelAssetCode = Request.QueryString("AssetCode")

            If Not IsPostBack Then
                If Not Request.QueryString("AssetCode") = "" Then
                    strAssetCode = Request.QueryString("AssetCode")
                End If


                If Not strAssetCode = "" Then
                    lblOper.Text = objFASetup.EnumOperation.Update
                    DisplayData()
                Else
                    lblOper.Text = objFASetup.EnumOperation.Add
                    EnableControl()
                    btnSave.Visible = True
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.AssetPermit))
        lblAssetCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Asset) & lblCode.Text
        lblAssetAddPermTag.Text = GetCaption(objLangCap.EnumLangCap.AssetAddPerm)
        lblAssetGenDeprPermTag.Text = GetCaption(objLangCap.EnumLangCap.AssetGenDeprPerm)
        lblAssetManDeprPermTag.Text = GetCaption(objLangCap.EnumLangCap.AssetManDeprPerm)
        lblAssetDispPermTag.Text = GetCaption(objLangCap.EnumLangCap.AssetDispPerm)
        lblAssetWOPermTag.Text = GetCaption(objLangCap.EnumLangCap.AssetWOPerm)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETPERMITDETAILS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=FA/setup/FA_setup_AssetPermitDetails.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function



    Protected Function LoadData() As DataSet

        strParam = strAssetCode & "|||"

        Try
            intErrNo = objFASetup.mtdGetAssetPermit(strOppCd_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETPERMIT_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/setup/FA_setup_AssetPermitList.aspx")
        End Try

        Return objDataSet
    End Function

    Sub DisableControl()
        Dim strView As Boolean

        strView = False
        txtAssetCode.Enabled = strView
        cbAssetAddPerm.Enabled = strView
        cbAssetGenDeprPerm.Enabled = strView
        cbAssetManDeprPerm.Enabled = strView
        cbAssetDispPerm.Enabled = strView
        cbAssetWOPerm.Enabled = strView
    End Sub

    Sub EnableControl()
        Dim strView As Boolean

        strView = True
        txtAssetCode.Enabled = strView
        cbAssetAddPerm.Enabled = strView
        cbAssetGenDeprPerm.Enabled = strView
        cbAssetManDeprPerm.Enabled = strView
        cbAssetDispPerm.Enabled = strView
        cbAssetWOPerm.Enabled = strView
    End Sub

    Sub DisplayData()
        Dim dsTx As DataSet = LoadData()

        If dsTx.Tables(0).Rows.Count > 0 Then
            If dsTx.Tables(0).Rows(0).Item("AssetAddPerm") = objFASetup.EnumAssetAddPerm.Yes Then
                cbAssetAddPerm.Checked = True
            Else
                cbAssetAddPerm.Checked = False
            End If
            If dsTx.Tables(0).Rows(0).Item("AssetGenDeprPerm") = objFASetup.EnumAssetGenDeprPerm.Yes Then
                cbAssetGenDeprPerm.Checked = True
            Else
                cbAssetGenDeprPerm.Checked = False
            End If
            If dsTx.Tables(0).Rows(0).Item("AssetManDeprPerm") = objFASetup.EnumAssetManDeprPerm.Yes Then
                cbAssetManDeprPerm.Checked = True
            Else
                cbAssetManDeprPerm.Checked = False
            End If
            If dsTx.Tables(0).Rows(0).Item("AssetDispPerm") = objFASetup.EnumAssetDispPerm.Yes Then
                cbAssetDispPerm.Checked = True
            Else
                cbAssetDispPerm.Checked = False
            End If
            If dsTx.Tables(0).Rows(0).Item("AssetWOPerm") = objFASetup.EnumAssetWOPerm.Yes Then
                cbAssetWOPerm.Checked = True
            Else
                cbAssetWOPerm.Checked = False
            End If
            lblStatus.Text = objFASetup.mtdGetAssetPermitStatus(Trim(dsTx.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("Username"))


            strSelAssetCode = Trim(dsTx.Tables(0).Rows(0).Item("AssetCode"))
            txtAssetCode.Text = strSelAssetCode & " (" & Trim(dsTx.Tables(0).Rows(0).Item("Description")) & ")"


            Select Case Trim(lblStatus.Text)
                Case objFASetup.mtdGetAssetPermitStatus(objFASetup.EnumAssetPermitStatus.Active)
                    EnableControl()
                    txtAssetCode.Enabled = False
                    btnSave.Visible = True
                Case objFASetup.mtdGetAssetPermitStatus(objFASetup.EnumAssetPermitStatus.Deleted)
                    DisableControl()
                    btnSave.Visible = False
            End Select
        End If
    End Sub







    
    Sub UpdateData(ByVal strAction As String)
        Dim blnDupKey As Boolean = False
        Dim strStatus As String
        Dim TxID As String
        Dim blnDeleteErr As Boolean
        Dim intError As Integer

        strStatus = objFASetup.EnumAssetPermitStatus.Active

        intAssetAddPerm = IIf(cbAssetAddPerm.Checked = True, objFASetup.EnumAssetAddPerm.Yes, objFASetup.EnumAssetAddPerm.No)
        intAssetGenDeprPerm = IIf(cbAssetGenDeprPerm.Checked = True, objFASetup.EnumAssetGenDeprPerm.Yes, objFASetup.EnumAssetGenDeprPerm.No)
        intAssetManDeprPerm = IIf(cbAssetManDeprPerm.Checked = True, objFASetup.EnumAssetManDeprPerm.Yes, objFASetup.EnumAssetManDeprPerm.No)
        intAssetDispPerm = IIf(cbAssetDispPerm.Checked = True, objFASetup.EnumAssetDispPerm.Yes, objFASetup.EnumAssetDispPerm.No)
        intAssetWOPerm = IIf(cbAssetWOPerm.Checked = True, objFASetup.EnumAssetWOPerm.Yes, objFASetup.EnumAssetWOPerm.No)

        strParam = strSelAssetCode & "|" & _
                    intAssetAddPerm & "|" & _
                    intAssetGenDeprPerm & "|" & _
                    intAssetManDeprPerm & "|" & _
                    intAssetDispPerm & "|" & _
                    intAssetWOPerm & "|" & _
                    strStatus

        Try
            intErrNo = objFASetup.mtdUpdAssetPermit(strOppCd_ADD, _
                                            strOppCd_UPD, _
                                            strOppCd_GET, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            intError, _
                                            lblOper.Text, _
                                            blnDeleteErr)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETPERMITDETAILS_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=FA/setup/FA_setup_AssetPermitDetails.aspx")
        End Try

        If intError = objFASetup.EnumErrorType.DuplicateKey Then
            lblDupMsg.Visible = True
        Else
            If blnDeleteErr = False Then
                Response.Redirect("FA_setup_AssetPermitList.aspx")
            End If

        End If
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If txtAssetCode.Text = "" Then
            lblAssetCodeErr.Visible = True
            Exit Sub
        End If
        UpdateData("Save")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("FA_setup_AssetPermitList.aspx")
    End Sub


End Class
