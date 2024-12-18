
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


Public Class FA_trx_AssetTranDetails : Inherits Page

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

    Protected WithEvents hidAccMonth As Label
    Protected WithEvents hidAccYear As Label

    Protected WithEvents ddlTranTo As DropDownList

    Protected WithEvents lblAssetValueTag As Label
    Protected WithEvents txtAssetValue As TextBox
    Protected WithEvents lblAccumDeprValueTag As Label
    Protected WithEvents txtAccumDeprValue As TextBox
    Protected WithEvents lblTranValueTag As Label
    Protected WithEvents txtTranValue As TextBox
    Protected WithEvents lblTranValueErr As Label
    Protected WithEvents lblAssetValueZeroErr As Label
    Protected WithEvents lblRemarkTag As Label
    Protected WithEvents txtRemark As TextBox

    Protected WithEvents txtQty As TextBox

    Protected WithEvents lblCode As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblDeleteErr As Label

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents lblSelect As Label


    'Protected WithEvents WOGLWOBlkCode As HtmlTableRow

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnConfirm As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents btnSelDateFrom As Image

    Protected WithEvents Find As HtmlInputButton

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
    Dim strOppCd_GET As String = "FA_CLSTRX_ASSETTRAN_GET"
    Dim strOppCd_ADD As String = "FA_CLSTRX_ASSETTRAN_ADD"
    Dim strOppCd_UPD As String = "FA_CLSTRX_ASSETTRAN_UPD"
    Dim intConfigSetting As Integer

    Dim strId As String
    Dim strBlkTag As String
    Dim strLocType As String

    Dim strSelAccMonth As String
    Dim strSelAccYear As String

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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAWriteOff), intFAAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblRefDateErr.Visible = False
            lblFmt.Visible = False
            lblAssetCodeErr.Visible = False
            lblTranValueErr.Visible = False
            lblAssetValueZeroErr.Visible = False

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
                    BidTranLoc("")

                    lblOper.Text = objFATrx.EnumOperation.Add

                    EnableControl()
                    btnSave.Visible = True
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
		    btnNew.Visible = True
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                strBlkTag = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
            Else
                strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKISSUE_DET_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_trx_stockissue_list.aspx")
        End Try

        lblTitle.Text = "ASSET TRANSFER" 'UCase(GetCaption(objLangCap.EnumLangCap.AssetWO))
        lblTxIDTag.Text = "Asset Transfer ID"
        lblAssetCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Asset) & lblCode.Text

        lblAssetCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.Asset)

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_ASSETWODETAILS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetWODetails.aspx")
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

    Protected Function LoadData() As DataSet

        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "SEARCH|ORDER"
        strParamValue = " AND FA.AssetTranID = '" & trim(strId) & "' AND FA.LocCode = '" & strLocation & "'|"

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOppCd_GET, strParamName, strParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_ASSETWO_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetTranList.aspx")
        End Try

        Return objDataSet
    End Function

    Sub DisableControl()
        Dim strView As Boolean

        strView = False
        txtRefNo.Enabled = strView
        txtRefDate.Enabled = strView
        ddlAssetCode.Enabled = strView
        ddlTranTo.Enabled = strView
        'ddlWOGLWOAccCode.Enabled = strView
        txtQty.Enabled = strView
        txtAssetValue.Enabled = strView
        txtAccumDeprValue.Enabled = strView
        txtTranValue.Enabled = strView
        txtRemark.Enabled = strView
        btnSelDateFrom.Visible = strView
    End Sub

    Sub EnableControl()
        Dim strView As Boolean

        strView = True
        txtRefNo.Enabled = strView
        'ddlWOGLWOAccCode.Enabled = strView
        ddlTranTo.Enabled = strView
        txtAssetValue.Enabled = strView
        txtQty.Enabled = strView
        txtAccumDeprValue.Enabled = strView
        txtTranValue.Enabled = strView
        txtRemark.Enabled = strView
        btnSelDateFrom.Visible = strView
    End Sub

    Sub DisplayData()
        Dim dsTx As DataSet = LoadData()

        If dsTx.Tables(0).Rows.Count > 0 Then

            lblDeleteErr.visible = "False"

            hidAccMonth.Text = dsTx.Tables(0).Rows(0).Item("AccMonth").Trim
            hidAccYear.Text = dsTx.Tables(0).Rows(0).Item("AccYear").Trim

            txtRefNo.Text = dsTx.Tables(0).Rows(0).Item("RefNo").Trim
            txtRefDate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsTx.Tables(0).Rows(0).Item("RefDate")))

            txtAssetValue.Text = dsTx.Tables(0).Rows(0).Item("AssetValue")
            txtAccumDeprValue.Text = dsTx.Tables(0).Rows(0).Item("AccumDeprValue")
            txtTranValue.Text = dsTx.Tables(0).Rows(0).Item("TranValue")
            txtQty.Text = dsTx.Tables(0).Rows(0).Item("Qty")
            txtRemark.Text = dsTx.Tables(0).Rows(0).Item("Remark").Trim

            lblAccPeriod.Text = Trim(dsTx.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsTx.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objFATrx.mtdGetAssetWOStatus(Trim(dsTx.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("Username"))

            BindAssetCode(Trim(dsTx.Tables(0).Rows(0).Item("AssetCode")))
            BidTranLoc(Trim(dsTx.Tables(0).Rows(0).Item("TranLocCode")))

            ddlAssetCode.Enabled = False
            ddlTranTo.Enabled = False
            txtRefDate.Enabled = False
            btnSelDateFrom.Visible = False


            Select Case Trim(lblStatus.Text)
                Case objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Active)
                    EnableControl()
                    btnSave.Visible = True
                    btnConfirm.Visible = True
                    btnDelete.Visible = True
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Deleted)
                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                Case objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Confirmed)
                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                Case objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Closed)
                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
            End Select
        End If
    End Sub

    Sub Get_Asset_Details(ByVal Sender As Object, ByVal E As EventArgs)



        Dim strDate As String = CheckDate()
        Dim intDay As Integer

        If Not strDate = "" Then
            intDay = Day(strDate)
        Else
            lblRefDateErr.Text = "Please Input Date First!"
            lblRefDateErr.Visible = True
            Exit Sub
        End If


        Dim strOppCd_AssetRegln_GET As String = "FA_CLSSETUP_ASSETREGLN_GET"
        Dim dsAssetRegln As New DataSet()
        Dim dblAccumDeprValue As Double

        strParam = ddlAssetCode.SelectedItem.Value & "|||||"

        Try
            intErrNo = objFASetup.mtdGetAssetRegln(strOppCd_AssetRegln_GET, strLocation, strParam, dsAssetRegln)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetWODetails.aspx")
        End Try

        txtAssetValue.Text = dsAssetRegln.Tables(0).Rows(0).Item("CurValue")
        txtQty.Text = dsAssetRegln.Tables(0).Rows(0).Item("CurQty")

        dblAccumDeprValue = 0
        If dsAssetRegln.Tables(0).Rows(0).Item("CurValue") >= dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue") Then
            dblAccumDeprValue = dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue")
        End If
        If dsAssetRegln.Tables(0).Rows(0).Item("CurValue") >= dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue") + dsAssetRegln.Tables(0).Rows(0).Item("MonthlyDepr") Then

            If intDay > 15 Then
		response.write ("Diatas Tgl 15")
                dblAccumDeprValue = dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue") + dsAssetRegln.Tables(0).Rows(0).Item("MonthlyDepr")
            Else
		response.write ("Dibawah Tgl 15")
                dblAccumDeprValue = dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue")
            End If

        End If

        txtAccumDeprValue.Text = dblAccumDeprValue
        txtTranValue.Text = txtAssetValue.Text - dblAccumDeprValue

	txtRefDate.Enabled = False


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

        strParam = "||||" & "REGLN.AssetCode ASC " & _
                    "|" & objFASetup.EnumAssetReglnStatus.Deleted
        strLocPerm = strLocation & "' AND REGLN.AssetCode NOT IN (SELECT ASSETCODE FROM FA_ASSETDISP WHERE DISPVALUE = 0 AND LOCCODE = '" & strLocation & "') AND PER.AssetWOPerm = '1"

        Try
            intErrNo = objFASetup.mtdGetAssetRegln(strOpCode, strLocPerm, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetWODetails.aspx")
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
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetWODetails.aspx")
            End Try
        End If

        ddlAssetCode.SelectedIndex = intSelectedIndex
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

        If txtAssetValue.Text > 0 Then
            If txtAccumDeprValue.Text < 0 Or txtTranValue.Text < 0 Then
                lblTranValueErr.Visible = True
                Exit Sub
            End If
        ElseIf txtAssetValue.Text < 0 Then
            If txtAccumDeprValue.Text > 0 Or txtTranValue.Text > 0 Then
                lblTranValueErr.Visible = True
                Exit Sub
            End If
        Else
            lblAssetValueZeroErr.Visible = True
            Exit Sub
        End If

        If Trim(lblTxID.Text) = "" Then
            Call AddData()
        Else
            Call UpdateData()
        End If

    End Sub


    Sub UpdateData()

        Dim strDate As String = CheckDate()
        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objRslSet As DataSet


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


        strOpCode = "FA_CLSTRX_ASSETTRAN_UPD"

        strParamName = "TRXID|REFNO|REFDATE|" & _
                      "ASSETCODE|TRANLOCCODE|" & _
                      "QTY|ASSETVALUE|ACCUMDEPRVALUE|REMARK|USERID|LOCCODE"

        
        strParamValue = lblTxID.Text & "|" & txtRefNo.Text & "|" & _
                        strDate & "|" & _
                        ddlAssetCode.SelectedItem.Value & "|" & _
                        ddlTranTo.SelectedItem.Value & "|" & _
                        txtQty.Text & "|" & _
                        txtAssetValue.Text & "|" & _
                        txtAccumDeprValue.Text & "|" & _
                        txtRemark.Text & "|" & strUserId & "|" & strLocation


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

        strOpCode = "FA_CLSTRX_ASSETTRAN_ADD"

        strParamName = "REFNO|REFDATE|" & _
                      "ASSETCODE|TRANLOCCODE|" & _
                      "QTY|ASSETVALUE|ACCUMDEPRVALUE|REMARK|LOCCODE|" & _
                      "USERID|ACCPERIODE"

        
        strParamValue = txtRefNo.Text & "|" & _
                        strDate & "|" & _
                        ddlAssetCode.SelectedItem.Value & "|" & _
                        ddlTranTo.SelectedItem.Value & "|" & _
                        txtQty.Text & "|" & _
                        txtAssetValue.Text & "|" & _
                        txtAccumDeprValue.Text & "|" & _
                        txtRemark.Text & "|" & _
                        strLocation & "|" & strUserId & "|" & _
                        format(dtmDate, "yyyyMM")


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


    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        lblOper.Text = objFATrx.EnumOperation.Confirm

        Call ConfirmData()

    End Sub


    Sub btnNew_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

	Response.Redirect("FA_trx_AssetTranDetails.aspx")


    End Sub


    Sub ConfirmData()

        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objRslSet As DataSet

        strOpCode = "FA_CLSTRX_ASSETTRAN_CONFIRM"

        strParamName = "TRXID|USERID"

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

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        lblOper.Text = objFATrx.EnumOperation.Delete

        Call DeleteData()

    End Sub


    Sub DeleteData()

        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objRslSet As DataSet

        strOpCode = "FA_CLSTRX_ASSETTRAN_DELETE"

        strParamName = "TRXID|USERID"

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
        Response.Redirect("FA_trx_AssetTranList.aspx")
    End Sub


    Sub BidTranLoc(ByVal pv_strLocCode As String)

        Dim strOpCode As String = "FA_CLSTRX_GET_TRANLOC"
        Dim strParamName As String
        Dim strParamValue As String
        Dim dsForDropDown As DataSet
        Dim intCnt As Integer

        Dim intSelectedIndex As Integer = 0

        strParamName = "LOCCODE"
        strParamValue = strLocation
        
        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, strParamName, strParamValue, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_GET_TRANLOC&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetWODetails.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("LocCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("LocCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim()
            If dsForDropDown.Tables(0).Rows(intCnt).Item("LocCode") = Trim(pv_strLocCode) Then
                intSelectedIndex = intCnt 
            End If
        Next intCnt

        ddlTranTo.DataSource = dsForDropDown.Tables(0)
        ddlTranTo.DataValueField = "LocCode"
        ddlTranTo.DataTextField = "Description"
        ddlTranTo.DataBind()

        ddlTranTo.SelectedIndex = intSelectedIndex

    End Sub


End Class
