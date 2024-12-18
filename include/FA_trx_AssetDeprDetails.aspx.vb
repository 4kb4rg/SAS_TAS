
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

Public Class FA_trx_AssetDeprDetails : Inherits Page

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
 
    Protected WithEvents lblDeprValueTag As Label
    Protected WithEvents txtDeprValue As TextBox
    Protected WithEvents txtDeprValueF As TextBox

    Protected WithEvents lblDeprValueZeroErr As Label
    Protected WithEvents lblRemarkTag As Label
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents lblSelect As Label

    Protected WithEvents lblCode As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblDeleteErr As Label

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents lblNetValueTag As Label
    Protected WithEvents lblNetValue As Label
    Protected WithEvents lblFinalValueTag As Label
    Protected WithEvents lblFinalValue As Label
    
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnConfirm As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnSelDateFrom As Image

    Protected WithEvents hidAccMonth As Label
    Protected WithEvents hidAccYear As Label
    Protected WithEvents hidFiscalSame As Label
    Protected WithEvents DeprFiskalRow As HtmlTableRow


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
    Dim strOppCd_GET As String = "FA_CLSTRX_ASSETDEPR_GET"
    Dim strOppCd_ADD As String = "FA_CLSTRX_ASSETDEPR_ADD"
    Dim strOppCd_UPD As String = "FA_CLSTRX_ASSETDEPR_UPD"
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FADepreciation), intFAAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblRefDateErr.Visible = False
            lblFmt.Visible = False
            lblAssetCodeErr.Visible = False
            lblDeprValueZeroErr.Visible = False

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


                    lblOper.Text = objFATrx.EnumOperation.Add

                    EnableControl()
                    btnSave.Visible = True
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlkTag = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
            Else
                strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKISSUE_DET_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_trx_stockissue_list.aspx")
        End Try

        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.AssetDepr))
        lblTxIDTag.Text = GetCaption(objLangCap.EnumLangCap.AssetDepr) & " ID"
        lblAssetCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Asset) & lblCode.Text
        lblAssetCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.Asset)

        lblNetValueTag.Text = GetCaption(objLangCap.EnumLangCap.NetValue)
        lblFinalValueTag.Text = GetCaption(objLangCap.EnumLangCap.FinalValue)

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_ASSETDEPRDETAILS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetDeprDetails.aspx")
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

        strParam = strId & "|||||||"

        Try
            intErrNo = objFATrx.mtdGetAssetDepr(strOppCd_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_ASSETDepr_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetDeprList.aspx")
        End Try

        Return objDataSet
    End Function

    Sub DisableControl()
        Dim strView As Boolean

        strView = False
        txtRefNo.Enabled = strView
        txtRefDate.Enabled = strView
        ddlAssetCode.Enabled = strView
        txtDeprValue.Enabled = strView
        txtDeprValueF.Enabled = strView
        txtRemark.Enabled = strView
        btnSelDateFrom.Visible = strView
    End Sub

    Sub EnableControl()
        Dim strView As Boolean

        strView = True
        txtRefNo.Enabled = strView
        'txtRefDate.Enabled = strView
        'ddlAssetCode.Enabled = strView
        txtDeprValue.Enabled = strView
        txtDeprValueF.Enabled = strView
        txtRemark.Enabled = strView
        btnSelDateFrom.Visible = strView
    End Sub

    Sub DisplayData()
        Dim dsTx As DataSet = LoadData()

        If dsTx.Tables(0).Rows.Count > 0 Then

            hidAccMonth.Text = dsTx.Tables(0).Rows(0).Item("AccMonth").Trim
            hidAccYear.Text = dsTx.Tables(0).Rows(0).Item("AccYear").Trim

            txtRefNo.Text = dsTx.Tables(0).Rows(0).Item("RefNo").Trim
            txtRefDate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsTx.Tables(0).Rows(0).Item("RefDate")))

            txtDeprValue.Text = Round(dsTx.Tables(0).Rows(0).Item("Amount"), 0)
            txtDeprValueF.Text = Round(dsTx.Tables(0).Rows(0).Item("FiskalDeprAmount"), 0)
            txtRemark.Text = dsTx.Tables(0).Rows(0).Item("Remark").Trim

            lblAccPeriod.Text = Trim(dsTx.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsTx.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objFATrx.mtdGetAssetDeprStatus(Trim(dsTx.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("Username"))

            lblNetValue.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(dsTx.Tables(0).Rows(0).Item("NetValue"), 0))
            lblFinalValue.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(dsTx.Tables(0).Rows(0).Item("FinalValue"), 0))

            BindAssetCode(Trim(dsTx.Tables(0).Rows(0).Item("AssetCode")))

            ddlAssetCode.Enabled = False
            txtRefDate.Enabled = False
            btnSelDateFrom.Visible = False

            hidFiscalSame.Text = IIf(dsTx.Tables(0).Rows(0).Item("IsFiskalSame") = True, 1, 0)
            If hidFiscalSame.Text = 0 Then
                DeprFiskalRow.Visible = True
            Else
                DeprFiskalRow.Visible = False
            End If


            Select Case Trim(lblStatus.Text)
                Case objFATrx.mtdGetAssetDeprStatus(objFATrx.EnumAssetDeprStatus.Active)
                    EnableControl()
                    btnSave.Visible = True
                    btnConfirm.Visible = True
                    btnDelete.Visible = True
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objFATrx.mtdGetAssetDeprStatus(objFATrx.EnumAssetDeprStatus.Deleted)

                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                Case objFATrx.mtdGetAssetDeprStatus(objFATrx.EnumAssetDeprStatus.Confirmed)

                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                Case objFATrx.mtdGetAssetDeprStatus(objFATrx.EnumAssetDeprStatus.Closed)

                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
            End Select

        End If
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


        hidFiscalSame.Text = IIf(dsAssetRegln.Tables(0).Rows(0).Item("IsFiskalSame") = True, 1, 0)
        If hidFiscalSame.Text = 0 Then
            DeprFiskalRow.Visible = True
        Else
            DeprFiskalRow.Visible = False
        End If

        'lblNetValue.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(dsAssetRegln.Tables(0).Rows(0).Item("NetValue"), 0))

        Dim dblNetValue As Double
        dblNetValue = dsAssetRegln.Tables(0).Rows(0).Item("CurValue") - dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue")

        lblNetValue.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(dblNetValue, 0))


        lblFinalValue.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(dsAssetRegln.Tables(0).Rows(0).Item("FinalValue"), 0))

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

        strLocPerm = strLocation & "' AND PER.AssetManDeprPerm = '1"

        Try
            intErrNo = objFASetup.mtdGetAssetRegln(strOpCode, strLocPerm, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetDeprDetails.aspx")
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

    Sub AddData()

        Dim strDate As String = CheckDate()
        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objRslSet As DataSet
        Dim dtmDate As Date
        Dim dblKomersial As String

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


        strOpCode = "FA_CLSTRX_ASSETDEPR_ADD"

        strParamName = "REFNO|REFDATE|" & _
                      "ASSETCODE|AMOUNT|AMOUNTF|" & _
                      "REMARK|LOCCODE|" & _
                      "USERID|ACCPERIODE|GENDEPRIND"

        If DeprFiskalRow.Visible = False Then
            dblKomersial = txtDeprValue.Text
        Else
            dblKomersial = txtDeprValueF.Text
        End If

        strParamValue = txtRefNo.Text & "|" & _
                        strDate & "|" & _
                        ddlAssetCode.SelectedItem.Value & "|" & _
                        txtDeprValue.Text & "|" & _
                        dblKomersial & "|" & _
                        txtRemark.Text & "|" & _
                        strLocation & "|" & strUserId & "|" & _
                        format(dtmDate, "yyyyMM") & "|2"


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
        Dim dblKomersial As String

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



        strOpCode = "FA_CLSTRX_ASSETDEPR_UPD"

        strParamName = "TRXID|REFNO|REFDATE|" & _
                      "ASSETCODE|AMOUNT|AMOUNTF|" & _
                      "REMARK|USERID"

        If DeprFiskalRow.Visible = False Then
            dblKomersial = txtDeprValue.Text
        Else
            dblKomersial = txtDeprValueF.Text
        End If


        strParamValue = lblTxID.Text & "|" & txtRefNo.Text & "|" & _
                        strDate & "|" & _
                        ddlAssetCode.SelectedItem.Value & "|" & _
                        txtDeprValue.Text & "|" & _
                        dblKomersial & "|" & _
                        txtRemark.Text & "|" & strUserId

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

        If txtDeprValue.Text = 0 Then
            lblDeprValueZeroErr.Visible = True
            Exit Sub
        End If


        If Trim(lblTxID.Text) = "" Then
            Call AddData()
        Else
            Call UpdateData()
        End If

    End Sub

    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If ddlAssetCode.SelectedItem.Value = "" Then
            lblAssetCodeErr.Visible = True
            Exit Sub
        End If

        If txtDeprValue.Text = 0 Then
            lblDeprValueZeroErr.Visible = True
            Exit Sub
        End If
        lblOper.Text = objFATrx.EnumOperation.Confirm

        Call ConfirmData()

    End Sub

    Sub ConfirmData()

        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objRslSet As DataSet

        strOpCode = "FA_CLSTRX_ASSETDEPR_CONFIRM"

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

        strOpCode = "FA_CLSTRX_ASSETDEPR_DELETE"

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
        Response.Redirect("FA_trx_AssetDeprList.aspx")
    End Sub


    Sub GetAccountDetails(ByVal pv_strAccCode As String, ByRef pr_strAccType As Integer)

        Dim _objAccDs As New DataSet
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer
        Dim strAccType As String
        Dim strAllowBlk As String
        
        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
           
            strAccType = Trim(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            If strAccType = "1" Then
                strAllowBlk = _objAccDs.Tables(0).Rows(0).Item("NurseryInd")         
            Else
                strAllowBlk = _objAccDs.Tables(0).Rows(0).Item("AccPurpose") 
            End If
           
            pr_strAccType = Convert.ToInt16(strAllowBlk)
        End If
    End Sub



End Class
