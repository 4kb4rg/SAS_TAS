
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
Imports System.Math 


Public Class FA_trx_AssetAddDetails : Inherits Page

    Dim objFASetup As New agri.FA.clsSetup()
    Dim objFATrx As New agri.FA.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblOper As Label
    Protected WithEvents lblDupMsg As Label

    Protected WithEvents lblTxIDTag As Label
    Protected WithEvents lblTxID As Label
    Protected WithEvents lblRefNoTag As Label
    Protected WithEvents txtRefNo As TextBox
    Protected WithEvents lblRefDateTag As Label
    Protected WithEvents txtRefDate As TextBox
    Protected WithEvents lblRefDateErr As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblAssetCodeTag As Label
    Protected WithEvents ddlAssetCode As DropDownList
    Protected WithEvents lblAssetCodeErr As Label
    Protected WithEvents lblAssetValueTag As Label
    Protected WithEvents txtAssetValue As TextBox
    Protected WithEvents lblRemarkTag As Label
    Protected WithEvents txtRemark As TextBox

    Protected WithEvents lblCode As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblDeleteErr As Label

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents lblAssetValueZeroErr As Label

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnConfirm As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnSelDateFrom As Image

    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrBlock As Label

    Protected WithEvents lblAccount As Label

    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Protected WithEvents lblPleaseSelectOne As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents ddlBlock As DropDownList

    Protected WithEvents lblNetValueTag As Label
    Protected WithEvents lblNetValue As Label

    Protected WithEvents hidAccMonth As Label
    Protected WithEvents hidAccYear As Label

    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents Find1 As HtmlInputButton

    Protected WithEvents ddlAsset As DropDownList
    Protected WithEvents trAsset As HtmlTableRow
    Protected WithEvents txtQty As TextBox
    Protected WithEvents hidQty As HtmlInputHidden
    Protected WithEvents hidAmount As HtmlInputHidden


    Dim PreBlockTag As String
    Dim BlockTag As String

    Dim objAccDs As New Dataset()
    Dim objBlkDs As New Dataset()
    Dim objVehDs As New Dataset()
    Dim objVehExpDs As New Dataset()


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
    Dim strOppCd_GET As String = "FA_CLSTRX_ASSETADD_GET"
    Dim strOppCd_ADD As String = "FA_CLSTRX_ASSETADD_ADD"
    Dim strOppCd_UPD As String = "FA_CLSTRX_ASSETADD_UPD"
    Dim intConfigSetting As Integer

    Dim strSelAccMonth As String
    Dim strSelAccYear As String

    Dim strId As String
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

        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAAddition), intFAAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblRefDateErr.Visible = False
            lblFmt.Visible = False
            lblAssetCodeErr.Visible = False
            lblAssetValueZeroErr.Visible = False
            lblErrAccount.Visible = False
            lblErrBlock.Visible = False
          

            onload_GetLangCap()

            If Not IsPostBack Then

                If Not Request.QueryString("Id") = "" Then
                    strId = Request.QueryString("Id")
                    lblTxID.Text = strId
                End If

                If Not strId = "" Then
                    lblOper.Text = objFATrx.EnumOperation.Update
                    DisplayData()
                Else
                    BindAssetCode("")
                    BindAccount("")
                    BindBlkDropList("", "")

                    trAsset.Visible = True
                    BindAssetIssue()

                    lblOper.Text = objFATrx.EnumOperation.Add
                    EnableControl()
                    btnSave.Visible = True
                    btnConfirm.Visible = False
                    btnDelete.Visible = False


                    lblNetValue.Text = "0"
                End If

            End If
        End If
    End Sub

    Sub BindAssetIssue()

        Dim strOpCode As String = "FA_CLSSETUP_ASSET_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim dr As DataRow

        strParamName = "LOCCODE"
        strParamValue = strLocation

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                                     strParamName, _
                                                     strParamValue, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & Exp.ToString() & "&redirect=redirect=FA/trx/FA_trx_AssetAddList.aspx")
        End Try


        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("Code") = dsForDropDown.Tables(0).Rows(intCnt).Item("Code").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = format(dsForDropDown.Tables(0).Rows(intCnt).Item("StockIssueDate"), "dd/MM/yyyy") & " (" & dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode").Trim() & " - " & dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("Code") = ""
        dr("Description") = "Pilih Asset"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlAsset.DataSource = dsForDropDown.Tables(0)
        ddlAsset.DataValueField = "Code"
        ddlAsset.DataTextField = "Description"
        ddlAsset.DataBind()

        ddlAsset.SelectedIndex = 0

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.AssetAdd))
        lblTxIDTag.Text = GetCaption(objLangCap.EnumLangCap.AssetAdd) & " ID"
        lblAssetCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Asset) & lblCode.Text

        lblAssetCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.Asset)

        lblNetValueTag.Text = GetCaption(objLangCap.EnumLangCap.NetValue)
        
        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
      
        lblErrAccount.Text = "<BR>" & lblPleaseSelectOne.Text & lblAccount.Text
        lblErrBlock.Text = lblPleaseSelectOne.Text & " Cost Center"
    
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_AssetAddDETAILS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetAddDetails.aspx")
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

        strParam = strId & "|||||||"

        Try
            intErrNo = objFATrx.mtdGetAssetAdd(strOppCd_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_ASSETADD_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetAddList.aspx")
        End Try

        Return objDataSet
    End Function

    Sub DisableControl()
        Dim strView As Boolean

        strView = False
        txtRefNo.Enabled = strView
        txtRefDate.Enabled = strView
        ddlAssetCode.Enabled = strView
        txtAssetValue.Enabled = strView
        txtRemark.Enabled = strView
        ddlAccount.Enabled = strView
        ddlBlock.Enabled = strView
       
    End Sub

    Sub EnableControl()
        Dim strView As Boolean

        strView = True
        txtRefNo.Enabled = strView
        'txtRefDate.Enabled = strView
        'ddlAssetCode.Enabled = strView
        txtAssetValue.Enabled = strView
        txtRemark.Enabled = strView
        ddlAccount.Enabled = strView
        ddlBlock.Enabled = strView

    End Sub

    Sub DisplayData()
        Dim dsTx As DataSet = LoadData()
        Dim strAccCode As String
        Dim strBlkCode As String
        Dim strVehCode As String
        Dim strVehExpenseCode As String

        If dsTx.Tables(0).Rows.Count > 0 Then

            trAsset.Visible = False

            hidAccMonth.Text = dsTx.Tables(0).Rows(0).Item("AccMonth").Trim
            hidAccYear.Text = dsTx.Tables(0).Rows(0).Item("AccYear").Trim

            txtRefNo.Text = dsTx.Tables(0).Rows(0).Item("RefNo").Trim
            txtRefDate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsTx.Tables(0).Rows(0).Item("RefDate")))
            txtAssetValue.Text = Round(dsTx.Tables(0).Rows(0).Item("Amount"), 2)
            txtRemark.Text = dsTx.Tables(0).Rows(0).Item("Remark").Trim
            lblAccPeriod.Text = Trim(dsTx.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsTx.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objFATrx.mtdGetAssetAddStatus(Trim(dsTx.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("Username"))
            BindAssetCode(Trim(dsTx.Tables(0).Rows(0).Item("AssetCode")))
            ddlAssetCode.Enabled = False
            txtRefDate.Enabled = False
            btnSelDateFrom.Visible = False
            strAccCode = Trim(dsTx.Tables(0).Rows(0).Item("AccCode"))
            strBlkCode = Trim(dsTx.Tables(0).Rows(0).Item("BlkCode"))


            lblNetValue.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(dsTx.Tables(0).Rows(0).Item("NetValue"), 0))

            BindAccount(strAccCode)
            BindBlkDropList(strAccCode, strBlkCode)

            Select Case Trim(lblStatus.Text)
                Case objFATrx.mtdGetAssetAddStatus(objFATrx.EnumAssetAddStatus.Active)
                    EnableControl()
                    btnSave.Visible = True
                    btnConfirm.Visible = True
                    btnDelete.Visible = True
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objFATrx.mtdGetAssetAddStatus(objFATrx.EnumAssetAddStatus.Deleted)
                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                Case objFATrx.mtdGetAssetAddStatus(objFATrx.EnumAssetAddStatus.Confirmed)
                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False

                Case objFATrx.mtdGetAssetAddStatus(objFATrx.EnumAssetAddStatus.Closed)
                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False

            End Select
        End If
    End Sub


    Sub BindAssetCode(ByVal pv_strAssetCode As String)
        Dim strOpCode As String = "FA_CLSSETUP_ASSETREGLN_GET_PERM"
        Dim strParam As String
        Dim dsForDropDown As DataSet
        Dim dsForInactiveItem As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim dr As DataRow
        Dim SearchStr As String
        Dim sortitem As String
        Dim strLocPerm As String

        strParam = "||" & objFASetup.EnumAssetReglnStatus.Active & _
                    "||" & "REGLN.AssetCode ASC " & "|"

        strLocPerm = strLocation & "' AND PER.AssetAddPerm = '1"


        Try
            intErrNo = objFASetup.mtdGetAssetRegln(strOpCode, strLocPerm, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetAddDetails.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("AssetCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("AssetCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("AssetCode").Trim() & " (" & dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("AssetCode") = Trim(pv_strAssetCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AssetCode") = ""
        dr("Description") = "Select " & lblAssetCodeTag.Text
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlAssetCode.DataSource = dsForDropDown.Tables(0)
        ddlAssetCode.DataValueField = "AssetCode"
        ddlAssetCode.DataTextField = "Description"
        ddlAssetCode.DataBind()

        If intSelectedIndex = 0 And Not strId = "" Then
            strParam = pv_strAssetCode & "||||" & "REGLN.AssetCode ASC " & _
                        "|"

            Try
                intErrNo = objFASetup.mtdGetAssetRegln(strOpCode, strLocation, strParam, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then
                    ddlAssetCode.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AssetCode")) & _
                     " (" & objFASetup.mtdGetAssetReglnStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AssetCode"))))
                    intSelectedIndex = ddlAssetCode.Items.Count - 1
                Else
                    intSelectedIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetDispDetails.aspx")
            End Try
        End If

        ddlAssetCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'" 'AND Acc.LocCode = '" & strLocation & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = lblPleaseSelect.Text & lblAccount.Text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = objAccDs.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "_Description"
        ddlAccount.DataBind()
        ddlAccount.SelectedIndex = intSelectedIndex
        ddlAccount.AutoPostBack = True
    End Sub


    Sub CheckAccBlk(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim intAccType As Integer
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strBlk As String = Request.Form("ddlBlock")

        GetAccountDetails(strAcc, intAccType)

        If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
            BindBlkDropList(strAcc, strBlk)
            RowBlk.Visible = True
        ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet Then
            BindBlkDropList("")
            RowBlk.Visible = False
        End If

    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, ByRef pr_strAccType As Integer)

        Dim _objAccDs As New DataSet
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType"))
        End If
    End Sub


    Sub BindBlkDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim strParam As String
        Dim dr As DataRow

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & strLocation & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & strLocation & "|" & objGLSetup.EnumBlockStatus.Active
            End If

            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCdBlockList_Get, _
                                                     strParam, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=" & strOpCdBlockList_Get & "&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            RowBlk.Visible = True
            dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Select Cost Center"

        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)
        ddlBlock.DataSource = dsForDropDown.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If

    End Sub

   
    Protected Function CheckDate() As String
        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        If Not txtRefDate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, txtRefDate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Text = objDateFormat & "."
                lblRefDateErr.Visible = True
                lblFmt.Visible = True
            End If
        End If

    End Function

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        If ddlAssetCode.SelectedItem.Value = "" Then
            lblAssetCodeErr.Visible = True
            Exit Sub
        End If
        If txtAssetValue.Text = 0 Then
            lblAssetValueZeroErr.Visible = True
            Exit Sub
        End If
        If ddlAccount.SelectedItem.Value = "" Then
            lblErrAccount.Visible = True
            Exit Sub
        End If

        If Request.Form("ddlBlock") = "" And RowBlk.Visible = True Then
            lblErrBlock.Visible = True
            Exit Sub
        End If

        If Trim(lblTxID.Text) = "" Then
            Call AddData()
        Else
            Call UpdateData()
        End If

    End Sub

    Sub AddData()

        Dim strDate As String = CheckDate()
        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objRslSet As DataSet
        Dim dtmDate As Date

        If Not strDate = "" Then
            Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
            Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)

            If intCurPeriod <> intInputPeriod Then
                lblRefDateErr.Visible = True
                lblRefDateErr.Text = "Invalid Transaction date."
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        dtmDate = strDate

        strOpCode = "FA_CLSTRX_ASSETADD_ADD"

        strParamName = "REFNO|REFDATE|" & _
                      "ASSETCODE|AMOUNT|" & _
                      "REMARK|ACCCODE|" & _
                      "BLKCODE|VEHCODE|" & _
                      "VEHEXPCODE|LOCCODE|" & _
                      "USERID|ACCPERIODE|STOCKISSUELNID|QTY"

        Dim strBlockCd As String
        strBlockCd = Request.Form("ddlBlock")

        strParamValue = txtRefNo.Text & "|" & _
                        strDate & "|" & _
                        ddlAssetCode.SelectedItem.Value & "|" & _
                        txtAssetValue.Text & "|" & _
                        txtRemark.Text & "|" & _
                        ddlAccount.SelectedItem.Value & "|" & _
                        strBlockCd & "|||" & _
                        strLocation & "|" & strUserId & "|" & _
                        format(dtmDate, "yyyyMM") & "|" & ddlAsset.SelectedItem.Value & "|" & txtQty.Text

       
        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objRslSet)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLNDETAILS_UPDATE&errmesg=" & Exp.ToString() & "&redirect=FA/trx/FA_trx_AssetAddDetails.aspx")
        End Try

        If objRslSet.Tables(0).Rows(0).Item("Msg") = "OK" Then

            strId = objRslSet.Tables(0).Rows(0).Item("ID")
            lblTxID.Text = strId
            Call DisplayData()
        Else
            lblDeleteErr.Text = objRslSet.Tables(0).Rows(0).Item("Msg")
            lblDeleteErr.visible = "True"
        End If


    End Sub


    Sub UpdateData()

        Dim strDate As String = CheckDate()
        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objRslSet As DataSet

        'If Not strDate = "" Then
        '    Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        '    Dim intCurPeriod As Integer = (CInt(hidAccYear.Text) * 100) + CInt(hidAccMonth.Text)

        '    If intCurPeriod <> intInputPeriod Then
        '        lblRefDateErr.Visible = True
        '        lblRefDateErr.Text = "Invalid Transaction date."
        '        Exit Sub
        '    End If
        'Else
        '    Exit Sub
        'End If


        If Not strDate = "" Then
            Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
            Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)

            If intCurPeriod <> intInputPeriod Then
                lblRefDateErr.Visible = True
                lblRefDateErr.Text = "Invalid Transaction date."
                Exit Sub
            End If
        Else
            Exit Sub
        End If


        strOpCode = "FA_CLSTRX_ASSETADD_UPD"

        strParamName = "TRXID|REFNO|REFDATE|" & _
                      "AMOUNT|" & _
                      "REMARK|ACCCODE|" & _
                      "BLKCODE|VEHCODE|" & _
                      "VEHEXPCODE|" & _
                      "USERID"

        Dim strBlockCd As String
        strBlockCd = Request.Form("ddlBlock")

        strParamValue = lblTxID.Text & "|" & txtRefNo.Text & "|" & _
                        strDate & "|" & _
                        txtAssetValue.Text & "|" & _
                        txtRemark.Text & "|" & _
                        ddlAccount.SelectedItem.Value & "|" & _
                        strBlockCd & "|||" & strUserId


        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objRslSet)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLNDETAILS_UPDATE&errmesg=" & Exp.ToString() & "&redirect=FA/trx/FA_trx_AssetAddDetails.aspx")
        End Try

        If objRslSet.Tables(0).Rows(0).Item("Msg") = "OK" Then
            strId = lblTxID.Text
            Call DisplayData()
        Else
            lblDeleteErr.Text = objRslSet.Tables(0).Rows(0).Item("Msg")
            lblDeleteErr.visible = "True"
        End If


    End Sub


    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If ddlAssetCode.SelectedItem.Value = "" Then
            lblAssetCodeErr.Visible = True
            Exit Sub
        End If
        If txtAssetValue.Text = 0 Then
            lblAssetValueZeroErr.Visible = True
            Exit Sub
        End If
        lblOper.Text = objFATrx.EnumOperation.Confirm

        ConfirmData()

    End Sub

    Sub ConfirmData()

        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objRslSet As DataSet

        strOpCode = "FA_CLSTRX_ASSETADD_CONFIRM"

        strParamName = "ASSETID|USERID"

        strParamValue = lblTxID.Text & "|" &  strUserId


        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objRslSet)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLNDETAILS_UPDATE&errmesg=" & Exp.ToString() & "&redirect=FA/trx/FA_trx_AssetAddDetails.aspx")
        End Try

        If objRslSet.Tables(0).Rows(0).Item("Msg") = "OK" Then
            strId = lblTxID.Text
            Call DisplayData()
        Else
            lblDeleteErr.Text = objRslSet.Tables(0).Rows(0).Item("Msg")
            lblDeleteErr.visible = "True"
        End If

    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Call DeleteData()
    End Sub

    Sub DeleteData()

        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objRslSet As DataSet

        strOpCode = "FA_CLSTRX_ASSETADD_DELETE"

        strParamName = "ASSETID|USERID"

        strParamValue = lblTxID.Text & "|" & strUserId


        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objRslSet)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLNDETAILS_UPDATE&errmesg=" & Exp.ToString() & "&redirect=FA/trx/FA_trx_AssetAddDetails.aspx")
        End Try

        If objRslSet.Tables(0).Rows(0).Item("Msg") = "OK" Then
            strId = lblTxID.Text
            Call DisplayData()
        Else
            lblDeleteErr.Text = objRslSet.Tables(0).Rows(0).Item("Msg")
            lblDeleteErr.visible = "True"
        End If

    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("FA_trx_AssetAddList.aspx")
    End Sub

     Sub Get_Asset_Details(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strOppCd_AssetRegln_GET As String = "FA_CLSSETUP_ASSETREGLN_GET"
        Dim dsAssetRegln As New DataSet()

        strParam = ddlAssetCode.SelectedItem.Value & "|||||"

        Try
            intErrNo = objFASetup.mtdGetAssetRegln(strOppCd_AssetRegln_GET, strLocation, strParam, dsAssetRegln)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetDeprDetails.aspx")
        End Try


        Dim dblNetValue As Double
        dblNetValue = dsAssetRegln.Tables(0).Rows(0).Item("CurValue") - dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue")

        lblNetValue.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(dblNetValue, 0))
      

    End Sub

    Sub SelectAsset(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim strOpCode As String = "FA_CLSSETUP_ASSET_DETAIL_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim strIssueLnID As String = Request.Form("ddlAsset")
        Dim dsResult As DataSet
        Dim strAccCode As String


        strParamName = "LOCCODE|ISSUELNID"
        strParamValue = strLocation & "|" & strIssueLnID

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                                     strParamName, _
                                                     strParamValue, _
                                                     dsResult)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnList.aspx")
        End Try


        If dsResult.tables(0).Rows.Count > 0 Then
            'fill default
            BindAccount(dsResult.Tables(0).Rows(0).Item("AccCode"))
            ddlAccount.SelectedValue = dsResult.Tables(0).Rows(0).Item("AccCode").Trim
            txtAssetValue.Text = Round(dsResult.Tables(0).Rows(0).Item("PriceAmount"), 2)
            txtQty.Text = Round(dsResult.Tables(0).Rows(0).Item("Qty"), 2)
            hidQty.Value = Round(dsResult.Tables(0).Rows(0).Item("Qty"), 2)
            hidAmount.Value = Round(dsResult.Tables(0).Rows(0).Item("PriceAmount"), 2)
        End If

    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class
