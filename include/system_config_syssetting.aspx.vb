Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.FileSystem
Imports Microsoft.VisualBasic
Imports agri.PWSystem.clsConfig
Imports agri.PWSystem.clsLangCap
Imports agri.Admin.clsComp
Imports agri.GlobalHdl.clsGlobalHdl

Public Class system_config_syssetting : Inherits Page

    Protected WithEvents SelectedComp As DropDownList
    Protected WithEvents SelectedLanguage As DropDownList
    Protected WithEvents ddl1st As DropDownList
    Protected WithEvents ddl2nd As DropDownList
    Protected WithEvents ddl3rd As DropDownList
    Protected WithEvents ddl4th As DropDownList
    Protected WithEvents DateFmt_DMY As RadioButton
    Protected WithEvents DateFmt_MDY As RadioButton
    Protected WithEvents ActiveLocation As DataGrid
    Protected WithEvents LocalControlInd_Yes As RadioButton
    Protected WithEvents LocalControlInd_No As RadioButton
    Protected WithEvents editLocation As LinkButton
    Protected WithEvents AccClsCodeFormat As TextBox
    Protected WithEvents ActCodeFormat As TextBox
    Protected WithEvents SubActCodeFormat As TextBox
    Protected WithEvents ExpCodeFormat As TextBox
    Protected WithEvents AccountLength As Label
    Protected WithEvents DateCreated As Label
    Protected WithEvents LastUpdated As Label
    Protected WithEvents UpdatedBy As Label
    Protected WithEvents DateCreateInd As HtmlInputHidden
    Protected WithEvents lblErrUpload As Label
    Protected WithEvents lblErrNoFile As Label
    Protected WithEvents lblOKUpload As Label
    Protected WithEvents lblErrStructure As Label
    Protected WithEvents lblErrAddLoc As Label
    Protected WithEvents lblErrCompany As Label
    Protected WithEvents flUpload As HtmlInputFile
    Protected WithEvents UploadBtn As ImageButton
    Protected WithEvents lnkDownload As Hyperlink
    Protected WithEvents lblComp1 As Label
    Protected WithEvents lblComp2 As Label
    Protected WithEvents lblLoc1 As Label
    Protected WithEvents lblAcc1 As Label
    Protected WithEvents lblAcc2 As Label
    Protected WithEvents lblAcc3 As Label
    Protected WithEvents lblAcc4 As Label
    Protected WithEvents lblAcc5 As Label
    Protected WithEvents lblAcc7 As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblConfigSetting As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblErrEnterLen As Label
    Protected WithEvents validateAccCls As RequiredFieldValidator
    Protected WithEvents validateAct As RequiredFieldValidator
    Protected WithEvents validateSubAct As RequiredFieldValidator
    Protected WithEvents validateExp As RequiredFieldValidator
    Protected WithEvents lblWSCostDist As Label

    Protected WithEvents Central_Yes As RadioButton
    Protected WithEvents Central_No As RadioButton

    Protected WithEvents FilterPeriod_Yes As RadioButton
    Protected WithEvents FilterPeriod_No As RadioButton

    Protected WithEvents RoundNo As TextBox

    Const CENTRECONTROL = 1
    Const REMOTECONTROL = 2

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim objLangCapDs As New Data.DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intADAR As Integer

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")

        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADSystemConfig), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblOKUpload.Visible = False
            lblErrUpload.Visible = False
            lblErrNoFile.Visible = False
            lblErrStructure.Visible = False
            lblErrAddLoc.Visible = False
            lblErrCompany.Visible = False
            SelectedComp.Enabled = False
            If Not IsPostBack Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        Dim strAccTag As String
        Dim strAccClsTag As String
        Dim strActTag As String
        Dim strSubActTag As String
        Dim strExpTag As String
        Dim strBlockTag As String
        Dim strSubBlockTag As String
        Dim strVehTypeTag As String

        GetEntireLangCap()
        lblComp1.Text = LCase(GetCaption(objLangCap.EnumLangCap.Company))
        lblComp2.Text = GetCaption(objLangCap.EnumLangCap.Company)
        lblLoc1.Text = GetCaption(objLangCap.EnumLangCap.Location)
        strAccTag = GetCaption(objLangCap.EnumLangCap.Account)
        lblAcc1.Text = strAccTag
        lblAcc2.Text = strAccTag
        lblAcc3.Text = strAccTag
        lblAcc4.Text = strAccTag
        lblAcc5.Text = strAccTag
        lblAcc7.Text = strAccTag

        ActiveLocation.Columns(0).HeaderText = lblLoc1.Text & lblCode.Text
        ActiveLocation.Columns(1).HeaderText = GetCaption(objLangCap.EnumLangCap.LocDesc)

        strAccClsTag = GetCaption(objLangCap.EnumLangCap.AccClass)
        strActTag = GetCaption(objLangCap.EnumLangCap.Activity)
        strSubActTag = GetCaption(objLangCap.EnumLangCap.SubAct)
        strExpTag = GetCaption(objLangCap.EnumLangCap.Expense)
        strBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        strSubBlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
        strVehTypeTag = GetCaption(objLangCap.EnumLangCap.VehType)

        ddl1st.Items(0).Text = strAccClsTag & lblCode.Text
        ddl1st.Items(1).Text = strActTag & lblCode.Text
        ddl1st.Items(2).Text = strSubActTag & lblCode.Text
        ddl1st.Items(3).Text = strExpTag & lblCode.Text

        ddl2nd.Items(0).Text = strAccClsTag & lblCode.Text
        ddl2nd.Items(1).Text = strActTag & lblCode.Text
        ddl2nd.Items(2).Text = strSubActTag & lblCode.Text
        ddl2nd.Items(3).Text = strExpTag & lblCode.Text

        ddl3rd.Items(0).Text = strAccClsTag & lblCode.Text
        ddl3rd.Items(1).Text = strActTag & lblCode.Text
        ddl3rd.Items(2).Text = strSubActTag & lblCode.Text
        ddl3rd.Items(3).Text = strExpTag & lblCode.Text

        ddl4th.Items(0).Text = strAccClsTag & lblCode.Text
        ddl4th.Items(1).Text = strActTag & lblCode.Text
        ddl4th.Items(2).Text = strSubActTag & lblCode.Text
        ddl4th.Items(3).Text = strExpTag & lblCode.Text

        lblErrCompany.Text = lblErrSelect.Text & lblComp2.Text
        validateAccCls.ErrorMessage = lblErrEnterLen.Text & strAccClsTag & "."
        validateAct.ErrorMessage = lblErrEnterLen.Text & strActTag & "."
        validateSubAct.ErrorMessage = lblErrEnterLen.Text & strSubActTag & "."
        validateExp.ErrorMessage = lblErrEnterLen.Text & strExpTag & "."
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
                                                 "", _
                                                 "", _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSTEM_CONFIGSETTING_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub onLoad_Display()
        Dim objCompDS As New Data.DataSet()
        Dim objConfigDS As New Data.DataSet()
        Dim objSysLocDS As New Data.DataSet()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intIndex As Integer = 0
        Dim intConfigSetting As Integer
        Dim strOpCode_Comp As String = "ADMIN_CLSCOMP_COMPANYLIST_GET"
        Dim strOpCode_Config As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim strOpCode_SysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOCLIST_GET"
        Dim strParam As String = ""
        Dim arrAccStructure As Array

        Try
            intErrNo = objAdminComp.mtdGetCompList(strOpCode_Comp, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   objCompDS)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSSETTING_GETCOMP_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strOpCode_Config, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                objConfigDS)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSSETTING_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        objConfigDS.Tables(0).Rows(0).Item("CompCode") = objConfigDS.Tables(0).Rows(0).Item("CompCode").Trim()
        objConfigDS.Tables(0).Rows(0).Item("CompName") = objConfigDS.Tables(0).Rows(0).Item("CompName").Trim()
        objConfigDS.Tables(0).Rows(0).Item("LangCode") = objConfigDS.Tables(0).Rows(0).Item("LangCode").Trim()
        objConfigDS.Tables(0).Rows(0).Item("Datefmt") = objConfigDS.Tables(0).Rows(0).Item("Datefmt").Trim()
        intConfigSetting = objConfigDS.Tables(0).Rows(0).Item("ConfigSetting")
        lblConfigSetting.Text = intConfigSetting
        objConfigDS.Tables(0).Rows(0).Item("StartAccMonth") = objConfigDS.Tables(0).Rows(0).Item("StartAccMonth").Trim()
        objConfigDS.Tables(0).Rows(0).Item("AccStructure") = objConfigDS.Tables(0).Rows(0).Item("AccStructure").Trim()
        objConfigDS.Tables(0).Rows(0).Item("AccClassLen") = objConfigDS.Tables(0).Rows(0).Item("AccClassLen").Trim()
        objConfigDS.Tables(0).Rows(0).Item("ActLen") = objConfigDS.Tables(0).Rows(0).Item("ActLen").Trim()
        objConfigDS.Tables(0).Rows(0).Item("SubActLen") = objConfigDS.Tables(0).Rows(0).Item("SubActLen").Trim()
        objConfigDS.Tables(0).Rows(0).Item("ExpenseLen") = objConfigDS.Tables(0).Rows(0).Item("ExpenseLen").Trim()
        objConfigDS.Tables(0).Rows(0).Item("AccCodeLen") = objConfigDS.Tables(0).Rows(0).Item("AccCodeLen").Trim()
        objConfigDS.Tables(0).Rows(0).Item("UserName") = objConfigDS.Tables(0).Rows(0).Item("UserName").Trim()

        If objConfigDS.Tables(0).Rows(0).Item("DateFmt") = objSysCfg.EnumDateFormat.MDY Then
            DateFmt_MDY.Checked = True
        Else
            DateFmt_DMY.Checked = True
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.CentreControl), intConfigSetting) = True Then
            LocalControlInd_Yes.Checked = True
            Switch_Control(CENTRECONTROL)
        Else
            LocalControlInd_No.Checked = True
            Switch_Control(REMOTECONTROL)
        End If

        If objConfigDS.Tables(0).Rows(0).Item("AccStructure") <> "" Then
            arrAccStructure = Split(objConfigDS.Tables(0).Rows(0).Item("AccStructure"), Chr(9))
            ddl1st.SelectedIndex = Convert.ToInt32(arrAccStructure(0)) - 1
            ddl2nd.SelectedIndex = Convert.ToInt32(arrAccStructure(1)) - 1
            ddl3rd.SelectedIndex = Convert.ToInt32(arrAccStructure(2)) - 1
            ddl4th.SelectedIndex = Convert.ToInt32(arrAccStructure(3)) - 1
        End If

        AccClsCodeFormat.Text = objConfigDS.Tables(0).Rows(0).Item("AccClassLen")
        ActCodeFormat.Text = objConfigDS.Tables(0).Rows(0).Item("ActLen")
        SubActCodeFormat.Text = objConfigDS.Tables(0).Rows(0).Item("SubActLen")
        ExpCodeFormat.Text = objConfigDS.Tables(0).Rows(0).Item("ExpenseLen")
        AccountLength.Text = objConfigDS.Tables(0).Rows(0).Item("AccCodeLen")
        DateCreated.Text = objGlobal.GetLongDate(objConfigDS.Tables(0).Rows(0).Item("CreateDate"))
        DateCreateInd.Value = objConfigDS.Tables(0).Rows(0).Item("CreateDate")
        LastUpdated.Text = objGlobal.GetLongDate(objConfigDS.Tables(0).Rows(0).Item("UpdateDate"))
        UpdatedBy.Text = objConfigDS.Tables(0).Rows(0).Item("UserName")
        lblWSCostDist.Text = objConfigDS.Tables(0).Rows(0).Item("WSDistCost")
        RoundNo.Text = objConfigDS.Tables(0).Rows(0).Item("RoundNo")

        If objConfigDS.Tables(0).Rows(0).Item("Centralized") = "1" Then
            Central_Yes.Checked = True
            Central_No.Checked = False
        Else
            Central_Yes.Checked = False
            Central_No.Checked = True
        End If

        If objConfigDS.Tables(0).Rows(0).Item("FilterPeriod") = "1" Then
            FilterPeriod_Yes.Checked = True
            FilterPeriod_No.Checked = False
        Else
            FilterPeriod_Yes.Checked = False
            FilterPeriod_No.Checked = True
        End If

        strParam = objConfigDS.Tables(0).Rows(0).Item("CompCode") & "|" & strLocation & "|" & strUserId
        Try
            intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCode_SysLoc, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                objSysLocDS, _
                                                strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSSETTING_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objCompDS.Tables(0).Rows.Count - 1
            objCompDS.Tables(0).Rows(intCnt).Item("CompCode") = objCompDS.Tables(0).Rows(intCnt).Item("CompCode").Trim()
            objCompDS.Tables(0).Rows(intCnt).Item("CompName") = objCompDS.Tables(0).Rows(intCnt).Item("CompCode") & " (" & objCompDS.Tables(0).Rows(intCnt).Item("CompName").Trim() & ")"
            If objCompDS.Tables(0).Rows(intCnt).Item("CompCode") = objConfigDS.Tables(0).Rows(0).Item("CompCode") Then
                intIndex = intCnt + 1
                Exit For
            End If
        Next

        Dim dr As DataRow
        dr = objCompDS.Tables(0).NewRow()
        dr("CompCode") = ""
        dr("CompName") = lblSelect.Text & lblComp2.Text
        dr("CompName") = "Select one Company"
        objCompDS.Tables(0).Rows.InsertAt(dr, 0)

        SelectedComp.DataSource = objCompDS.Tables(0)
        SelectedComp.DataTextField = "CompName"
        SelectedComp.DataValueField = "CompCode"
        SelectedComp.DataBind()
        SelectedComp.SelectedIndex = intIndex

        If intIndex = 0 Then
            SelectedComp.Enabled = True
        End If

        ActiveLocation.DataSource = objSysLocDS
        ActiveLocation.DataBind()

        objCompDS = Nothing
        objConfigDS = Nothing
        objSysLocDS = Nothing
    End Sub


    Sub AddLocBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If SelectedComp.SelectedItem.Value = "" Then
            SelectedComp.Enabled = True
            lblErrAddLoc.Visible = True
        Else
            Response.Redirect("sysloc.aspx?CompCode=" & SelectedComp.SelectedItem.Value & "&CompName=" & SelectedComp.SelectedItem.Value)
        End If
    End Sub


    Sub editLocation_Click(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lblValue As Label
        Dim intIndex As Integer = E.Item.ItemIndex
        Dim strLocCode As String
        Dim strLocDesc As String

        lblValue = ActiveLocation.Items.Item(intIndex).FindControl("lblLocCode")
        strLocCode = lblValue.Text
        lblValue = ActiveLocation.Items.Item(intIndex).FindControl("lblLocDesc")
        strLocDesc = lblValue.Text

        If SelectedComp.SelectedItem.Value = "" Then
            SelectedComp.Enabled = True
            lblErrAddLoc.Visible = True
        Else
            Response.Redirect("sysloc.aspx?CompCode=" & SelectedComp.SelectedItem.Value & "&LocCode=" & strLocCode & "&LocDesc=" & strLocDesc)
        End If
    End Sub

    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim intDateFmt As Integer
        Dim intConfigSetting As Integer = 0
        Dim intAccControl As Integer
        Dim intAccLength As Integer
        Dim blnUpdate As Boolean = False
        Dim strOpCode_UpdCfg As String = "PWSYSTEM_CLSCONFIG_CONFIG_UPD"
        Dim strOpCode_AddCfg As String = "PWSYSTEM_CLSCONFIG_CONFIG_ADD"
        Dim strParam As String
        Dim strAccStructure As String

        If SelectedComp.SelectedItem.Value = "" Then
            SelectedComp.Enabled = True
            lblErrCompany.Visible = True
            Exit Sub
        End If

        If (funcValCodeLength(AccClsCodeFormat.Text)) And _
           (funcValCodeLength(ActCodeFormat.Text)) And _
           (funcValCodeLength(SubActCodeFormat.Text)) And _
           (funcValCodeLength(ExpCodeFormat.Text)) Then

            If DateFmt_DMY.Checked Then
                intDateFmt = objSysCfg.EnumDateFormat.DMY
            Else
                intDateFmt = objSysCfg.EnumDateFormat.MDY
            End If

            If (LocalControlInd_Yes.Checked = True) And _
                objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.CentreControl), lblConfigSetting.Text) = False Then
                intConfigSetting = lblConfigSetting.Text + objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.CentreControl)
            ElseIf (LocalControlInd_Yes.Checked = False) And _
                objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.CentreControl), Convert.ToInt64(lblConfigSetting.Text)) = True Then
                intConfigSetting = lblConfigSetting.Text - objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.CentreControl)
            Else
                intConfigSetting = lblConfigSetting.Text
            End If

            strAccStructure = ddl1st.SelectedItem.Value & Chr(9) & _
                              ddl2nd.SelectedItem.Value & Chr(9) & _
                              ddl3rd.SelectedItem.Value & Chr(9) & _
                              ddl4th.SelectedItem.Value

            intAccLength = Convert.ToInt16(AccClsCodeFormat.Text) + Convert.ToInt16(ActCodeFormat.Text) + Convert.ToInt16(SubActCodeFormat.Text) + Convert.ToInt16(ExpCodeFormat.Text)

            If Trim(DateCreateInd.Value) <> "" Then
                blnUpdate = True
            Else
                intConfigSetting = intConfigSetting _
                                 + objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel) _
                                 + objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel) _
                                 + objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoEmpCode) _
                                 + objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.OneStageNursery) _
                                 + objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoResetPLAcc) _
                                 + objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.NoGCDistribute)
            End If

            strParam = SelectedComp.SelectedItem.Value & "|" & _
                       SelectedLanguage.SelectedItem.Value & "|" & _
                       Convert.ToString(intDateFmt) & "|" & _
                       Convert.ToString(intConfigSetting) & "|" & _
                       Convert.ToString(objSysCfg.EnumVehDistMethod.ByMTD) & "|" & _
                       Convert.ToString(objSysCfg.EnumVehDistMethod.UsingVehicle) & "|1|" & _
                       strAccStructure & "|" & _
                       AccClsCodeFormat.Text & "|" & _
                       ActCodeFormat.Text & "|" & _
                       SubActCodeFormat.Text & "|" & _
                       ExpCodeFormat.Text & "|" & _
                       Convert.ToString(intAccLength) & "|" & _
                       lblWSCostDist.Text & "|" & _
                       IIf(Central_Yes.Checked = True, "1", "0") & "|" & _
                       IIf(FilterPeriod_Yes.Checked = True, "1", "0") & "|" & _
                       IIf(Trim(RoundNo.Text) = "", "0", Trim(RoundNo.Text))

            Try
                intErrNo = objSysCfg.mtdUpdConfigInfo(strOpCode_AddCfg, _
                                                    strOpCode_UpdCfg, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    blnUpdate)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSSETTING_UPD_CONFIG&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            Session("SS_COMPANY") = SelectedComp.SelectedItem.Value
        End If

        Response.Redirect("sys_param_setting.aspx")
    End Sub

    Function funcValCodeLength(ByVal pv_strLength As String)
        Dim str1st As String = ddl1st.SelectedItem.Value
        Dim str2nd As String = ddl2nd.SelectedItem.Value
        Dim str3rd As String = ddl3rd.SelectedItem.Value
        Dim str4th As String = ddl4th.SelectedItem.Value

        If ((str1st <> str2nd) And (str1st <> str3rd) And (str1st <> str4th)) And _
           ((str2nd <> str1st) And (str2nd <> str3rd) And (str2nd <> str4th)) And _ 
           ((str3rd <> str1st) And (str3rd <> str2nd) And (str3rd <> str4th)) And _
           ((str4th <> str1st) And (str4th <> str2nd) And (str4th <> str3rd)) Then
            Try
                Dim intLength = Integer.Parse(pv_strLength)
                funcValCodeLength = ((intLength >= 1) And (intLength <= 8))
            Catch ex As System.Exception
                Return False
            End Try
        Else
            lblErrStructure.Visible = True
            Return False
        End If
    End Function


    Sub UploadBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCode_Get As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim strOpCode_Upd As String = "PWSYSTEM_CLSCONFIG_ACCLENGTH_UPLOAD"
        Dim strOpCodes As String = strOpCode_Get & "|" & strOpCode_Upd
        Dim objStreamReader As StreamReader
        Dim strZipPath As String = ""
        Dim strXmlPath As String = ""

        Dim arrZipPath As Array
        Dim strZipName As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intFreeFile As Integer
        Dim strFtpPath As String
        Dim strXmlEncrypted As String = ""
        Dim objXmlDecrypted As New Object()
        Dim strErrMsg As String
        
        If Trim(flUpload.Value) = "" Then
            lblErrNoFile.Text = "Please select a file before clicking Upload button."
            lblErrNoFile.Visible = True
            Exit Sub
        ElseIf flUpload.PostedFile.ContentLength = 0 Then
            lblErrNoFile.Text = "The selected data transfer file is either not found or is corrupted."
            lblErrNoFile.Visible = True
            Exit Sub
        End If
        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            lblErrUpload.Visible = True
            Exit Sub
        End Try

        Try
            strZipPath = flUpload.PostedFile.FileName
        Catch
            lblErrNoFile.Visible = True
            Exit Sub
        End Try

        arrZipPath = Split(strZipPath, "\")
        strZipName = arrZipPath(UBound(arrZipPath))
        strZipPath = strFtpPath & strZipName
        If objGlobal.mtdValidateUploadFileName(strZipName, objGlobal.EnumDataTransferFileType.SC_SystemConfigurationData, strErrMsg) = False Then
            lblErrNoFile.Text = "<br>" & strErrMsg
            lblErrNoFile.Visible = True
            Exit Sub
        End If
        strXmlPath = strFtpPath & Mid(strZipName, 1, Len(strZipName) - 3) & "xml"

        Dim Xmlfile As New FileInfo(strXmlPath)

        If Xmlfile.Exists Then
            File.Delete(strXmlPath)
        End If

        Try
            flUpload.PostedFile.SaveAs(strZipPath)
        Catch Exp As System.Exception
            lblErrUpload.Visible = True
            Exit Sub            
        End Try

        objStreamReader = File.OpenText(strZipPath)
        strXmlEncrypted = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        Try
            intErrNo = objSysCfg.mtdDecryptRef(strXmlEncrypted, objXmlDecrypted)
        Catch Exp As System.Exception
            lblErrUpload.Visible = True
            Exit Sub
        End Try

        intFreeFile = FreeFile()
        FileOpen(intFreeFile, strXmlPath, 8)  
        Print(intFreeFile, objXmlDecrypted)
        FileClose(intFreeFile)

        Try
            intErrNo = objSysCfg.mtdUploadRef(strOpCodes, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strXmlPath)
        Catch Exp As System.Exception
            lblErrUpload.Visible = True
            Exit Sub
        End Try

        lblOKUpload.Visible = True
        onLoad_Display()
        Switch_Control(REMOTECONTROL)
    End Sub


    Sub Switch_Control(ByVal pv_intControl As Integer)
        Dim objAccDs As New Dataset()
        Dim objBlkDs As New Dataset()
        Dim strOpCd_Acc As String = "PWSYSTEM_CLSCONFIG_ACCOUNT_COUNT_GET"
        Dim strOpCd_Blk As String = "PWSYSTEM_CLSCONFIG_BLOCK_COUNT_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strOpCd_Acc, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSSETTING_ACCOUNT_COUNT_GET&errmesg=" & Exp.ToString & "&redirect=system/config/syssetting.aspx")
        End Try

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strOpCd_Blk, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSSETTING_ACCOUNT_COUNT_GET&errmesg=" & Exp.ToString & "&redirect=system/config/syssetting.aspx")
        End Try

        Select Case pv_intControl
            Case CENTRECONTROL :
                    flUpload.Disabled = True
                    UploadBtn.Visible = False
                    lnkDownload.NavigateURL = "sys_data_download.aspx"

                    If objAccDs.Tables(0).Rows(0).Item("RecordCount") > 0 Then
                        ddl1st.Enabled = False
                        ddl2nd.Enabled = False
                        ddl3rd.Enabled = False
                        ddl4th.Enabled = False
                        AccClsCodeFormat.Enabled = False
                        ActCodeFormat.Enabled = False
                        SubActCodeFormat.Enabled = False
                        ExpCodeFormat.Enabled = False
                    Else
                        ddl1st.Enabled = True
                        ddl2nd.Enabled = True
                        ddl3rd.Enabled = True
                        ddl4th.Enabled = True
                        AccClsCodeFormat.Enabled = True
                        ActCodeFormat.Enabled = True
                        SubActCodeFormat.Enabled = True
                        ExpCodeFormat.Enabled = True
                    End If

            Case REMOTECONTROL :
                    ddl1st.Enabled = False
                    ddl2nd.Enabled = False
                    ddl3rd.Enabled = False
                    ddl4th.Enabled = False
                    AccClsCodeFormat.Enabled = False
                    ActCodeFormat.Enabled = False
                    SubActCodeFormat.Enabled = False
                    ExpCodeFormat.Enabled = False
                    flUpload.Disabled = False
                    UploadBtn.Visible = True
                    lnkDownload.NavigateURL = ""

                    If objAccDs.Tables(0).Rows(0).Item("RecordCount") > 0 Then
                        flUpload.Disabled = True
                        UploadBtn.Visible = False
                    End If
        End Select

        objAccDs = Nothing
        objBlkDs = Nothing
    End Sub

    Sub onselect_change(Sender As Object, E As EventArgs)
         If LocalControlInd_Yes.Checked Then
            Switch_Control(CENTRECONTROL)
         Else
            Switch_Control(REMOTECONTROL)
         End If
    End Sub



End Class
