
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


Public Class FA_trx_AssetWODetails : Inherits Page

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
   
    Protected WithEvents lblWOGLWOAccCodeTag As Label
    'Protected WithEvents lblWOGLWOBlkCodeTag As Label
    Protected WithEvents ddlWOGLWOAccCode As DropDownList
    Protected WithEvents ddlWOGLWOBlkCode As DropDownList
    Protected WithEvents lblWOGLWOAccCodeErr As Label
    Protected WithEvents lblWOGLWOBlkCodeErr As Label
    Protected WithEvents lblAssetValueTag As Label
    Protected WithEvents txtAssetValue As TextBox
    Protected WithEvents lblAccumDeprValueTag As Label
    Protected WithEvents txtAccumDeprValue As TextBox
    Protected WithEvents lblWOValueTag As Label
    Protected WithEvents txtWOValue As TextBox
    Protected WithEvents lblWOValueErr As Label
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

    Protected WithEvents WOGLWOBlkCode As HtmlTableRow

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnConfirm As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnSelDateFrom As Image

    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents Find1 As HtmlInputButton

    Protected WithEvents txtNP As TextBox
    Protected WithEvents lblErrNP As Label

    Protected WithEvents txtAssetValueF As TextBox
    Protected WithEvents txtAccumDeprValueF As TextBox
    Protected WithEvents txtWOValueF As TextBox
    Protected WithEvents lblWOValueFErr As Label
    Protected WithEvents lblAssetValueZeroFErr As Label

    Protected WithEvents hidFiskalSame As Label

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
    Dim strOppCd_GET As String = "FA_CLSTRX_ASSETWO_GET"
    Dim strOppCd_ADD As String = "FA_CLSTRX_ASSETWO_ADD"
    Dim strOppCd_UPD As String = "FA_CLSTRX_ASSETWO_UPD"
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
            lblWOGLWOAccCodeErr.Visible = False
            lblWOValueErr.Visible = False
            lblAssetValueZeroErr.Visible = False
            lblWOValueFErr.Visible = False
            lblAssetValueZeroFErr.Visible = False

            onload_GetLangCap()

            If Not IsPostBack Then
                txtRefDate.Text = "1/" & strSelAccMonth & "/" & strSelAccYear

                If Not Request.QueryString("Id") = "" Then
                    strId = Request.QueryString("Id")
                    lblTxID.Text = strId
                End If

                If Not strId = "" Then
                    lblOper.Text = objFATrx.EnumOperation.Update
                    DisplayData()
                Else
                    BindAssetCode("")
                    BindWOGLWOAccCode("")

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
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                strBlkTag = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
            Else
                strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKISSUE_DET_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_trx_stockissue_list.aspx")
        End Try

        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.AssetWO))
        lblTxIDTag.Text = GetCaption(objLangCap.EnumLangCap.AssetWO) & " ID"
        lblAssetCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Asset) & lblCode.Text

        lblWOGLWOAccCodeTag.Text = GetCaption(objLangCap.EnumLangCap.WOGLWOAccCode)

        lblAssetCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.Asset)
      
        lblWOGLWOAccCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.WOGLWOAccCode)
        lblWOGLWOBlkCodeErr.Text = "Please select Cost Center"

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
            intErrNo = objFATrx.mtdGetAssetWO(strOppCd_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_ASSETWO_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetWOList.aspx")
        End Try

        Return objDataSet
    End Function

    Sub DisableControl()
        Dim strView As Boolean

        strView = False
        txtRefNo.Enabled = strView
        txtRefDate.Enabled = strView
        ddlAssetCode.Enabled = strView
        ddlWOGLWOAccCode.Enabled = strView
        txtQty.Enabled = strView
        txtAssetValue.Enabled = strView
        txtAccumDeprValue.Enabled = strView
        txtWOValue.Enabled = strView
        txtAssetValueF.Enabled = strView
        txtAccumDeprValueF.Enabled = strView
        txtWOValueF.Enabled = strView
        txtRemark.Enabled = strView
        btnSelDateFrom.Visible = strView
    End Sub

    Sub EnableControl()
        Dim strView As Boolean

        strView = True
        txtRefNo.Enabled = strView
        'txtRefDate.Enabled = strView
        'ddlAssetCode.Enabled = strView
        ddlWOGLWOAccCode.Enabled = strView
        txtAssetValue.Enabled = strView
        txtQty.Enabled = strView
        txtAccumDeprValue.Enabled = strView
        txtWOValue.Enabled = strView
        txtAssetValueF.Enabled = strView
        txtAccumDeprValueF.Enabled = strView
        txtWOValueF.Enabled = strView
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
            txtWOValue.Text = dsTx.Tables(0).Rows(0).Item("WOValue")

            txtAssetValueF.Text = dsTx.Tables(0).Rows(0).Item("AssetValueF")
            txtAccumDeprValueF.Text = dsTx.Tables(0).Rows(0).Item("AccumDeprValueF")
            txtWOValueF.Text = dsTx.Tables(0).Rows(0).Item("WOValueF")

            txtQty.Text = dsTx.Tables(0).Rows(0).Item("Qty")
            txtRemark.Text = dsTx.Tables(0).Rows(0).Item("Remark").Trim

            lblAccPeriod.Text = Trim(dsTx.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsTx.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objFATrx.mtdGetAssetWOStatus(Trim(dsTx.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("Username"))

            BindAssetCode(Trim(dsTx.Tables(0).Rows(0).Item("AssetCode")))

            ddlAssetCode.Enabled = False
            txtRefDate.Enabled = False
            btnSelDateFrom.Visible = False

            BindWOGLWOAccCode(Trim(dsTx.Tables(0).Rows(0).Item("WOGLWOAccCode")))


            If Trim(dsTx.Tables(0).Rows(0).Item("WOGLWOBlkCode")) <> "" Then
                WOGLWOBlkCode.Visible = True
                BindWOGLWOBlkDropList(Trim(dsTx.Tables(0).Rows(0).Item("WOGLWOAccCode")), Trim(dsTx.Tables(0).Rows(0).Item("WOGLWOBlkCode")))
            End If

            txtNP.Text = dsTx.Tables(0).Rows(0).Item("NilaiPerolehan")

            Select Case Trim(lblStatus.Text)
                Case objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Active)
                    EnableControl()
                    btnSave.Visible = True
                    btnConfirm.Visible = True
                    btnDelete.Visible = True
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Deleted)

                    ddlWOGLWOBlkCode.Enabled = False

                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                Case objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Confirmed)

                    ddlWOGLWOBlkCode.Enabled = False

                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                Case objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Closed)

                    ddlWOGLWOBlkCode.Enabled = False

                    DisableControl()
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
            End Select

            If dsTx.Tables(0).Rows(0).Item("IsFiskalSame") = True Then
                txtAssetValueF.Enabled = False
                txtAccumDeprValueF.Enabled = False
                txtWOValueF.Enabled = False
                hidFiskalSame.Text = "1"
            Else
                txtAssetValueF.Enabled = True
                txtAccumDeprValueF.Enabled = True
                txtWOValueF.Enabled = True
                hidFiskalSame.Text = "0"
            End If
        End If
    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcceptFormat As String

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Sub Get_Asset_Details(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strOppCd_AssetRegln_GET As String = "FA_CLSSETUP_ASSETREGLN_GETDTL_TRX"
        Dim dsAssetRegln As New DataSet()
        Dim dblAccumDeprValue As Double
        Dim dblAccumDeprValueF As Double
        Dim strDate As String = Date_Validation(txtRefDate.Text, False)
        Dim TrxPeriod As Integer
        Dim FAEndPeriod As Integer
        Dim FAEndPeriodF As Integer


        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        TrxPeriod = (strAccYear * 10) + strAccMonth

        strParam = ddlAssetCode.SelectedItem.Value & "|||||"

        Try
            intErrNo = objFASetup.mtdGetAssetRegln(strOppCd_AssetRegln_GET, strLocation, strParam, dsAssetRegln)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetWODetails.aspx")
        End Try

        txtAssetValue.Text = dsAssetRegln.Tables(0).Rows(0).Item("CurValue")
        txtQty.Text = dsAssetRegln.Tables(0).Rows(0).Item("CurQty")
        FAEndPeriod = dsAssetRegln.Tables(0).Rows(0).Item("FAEndPeriod")
        FAEndPeriodF = dsAssetRegln.Tables(0).Rows(0).Item("FAEndPeriodF")
        'txtAccumDeprValue.Text = dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue")
        'txtWOValue.Text = txtAssetValue.Text - txtAccumDeprValue.Text

        dblAccumDeprValue = 0
        If dsAssetRegln.Tables(0).Rows(0).Item("CurValue") >= dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue") Then
            dblAccumDeprValue = dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue")
        End If
        If dsAssetRegln.Tables(0).Rows(0).Item("CurValue") >= dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue") + dsAssetRegln.Tables(0).Rows(0).Item("MonthlyDepr") Then
            dblAccumDeprValue = dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue") + dsAssetRegln.Tables(0).Rows(0).Item("MonthlyDepr")
        End If
        If TrxPeriod = FAEndPeriod Then
            If Round(dsAssetRegln.Tables(0).Rows(0).Item("CurValue"), 0) >= Round(dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue") + dsAssetRegln.Tables(0).Rows(0).Item("MonthlyDepr"), 0) Then
                dblAccumDeprValue = Round(dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValue") + dsAssetRegln.Tables(0).Rows(0).Item("MonthlyDepr"), 0)
            End If
        End If

        txtAccumDeprValue.Text = dblAccumDeprValue
        txtWOValue.Text = txtAssetValue.Text - dblAccumDeprValue
        txtNP.Text = dsAssetRegln.Tables(0).Rows(0).Item("AssetValue")

        txtAssetValueF.Text = dsAssetRegln.Tables(0).Rows(0).Item("CurValueF")
        dblAccumDeprValueF = 0
        If dsAssetRegln.Tables(0).Rows(0).Item("CurValueF") >= dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValueF") Then
            dblAccumDeprValueF = dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValueF")
        End If
        If dsAssetRegln.Tables(0).Rows(0).Item("CurValueF") >= dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValueF") + dsAssetRegln.Tables(0).Rows(0).Item("MonthlyDeprF") Then
            dblAccumDeprValueF = dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValueF") + dsAssetRegln.Tables(0).Rows(0).Item("MonthlyDeprF")
        End If
        If TrxPeriod = FAEndPeriodF Then
            If Round(dsAssetRegln.Tables(0).Rows(0).Item("CurValueF"), 0) >= Round(dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValueF") + dsAssetRegln.Tables(0).Rows(0).Item("MonthlyDeprF"), 0) Then
                dblAccumDeprValueF = Round(dsAssetRegln.Tables(0).Rows(0).Item("CurDeprValueF") + dsAssetRegln.Tables(0).Rows(0).Item("MonthlyDeprF"), 0)
            End If
        End If

        txtAccumDeprValueF.Text = dblAccumDeprValueF
        txtWOValueF.Text = txtAssetValueF.Text - dblAccumDeprValueF

        If dsAssetRegln.Tables(0).Rows(0).Item("IsFiskalSame") = True Then
            txtAssetValueF.Enabled = False
            txtAccumDeprValueF.Enabled = False
            txtWOValueF.Enabled = False
            hidFiskalSame.Text = "1"
        Else
            txtAssetValueF.Enabled = True
            txtAccumDeprValueF.Enabled = True
            txtWOValueF.Enabled = True
            hidFiskalSame.Text = "0"
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

        strParam = "||||" & "REGLN.AssetCode ASC " & _
                    "|" & objFASetup.EnumAssetReglnStatus.Deleted
        strLocPerm = strLocation &  "' AND REGLN.AssetCode NOT IN (SELECT ASSETCODE FROM FA_ASSETDISP WHERE DISPVALUE = 0 AND LOCCODE = '" & strLocation & "') AND PER.AssetWOPerm = '1"

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

    
    Sub BindWOGLWOAccCode(ByVal pv_strAccCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
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

                strParam = "Order By ACC.AccCode|And ACC.AccPurpose = " & objGLSetup.EnumAccountPurpose.NonVehicle & _
                                   " And ACC.Status = '" & _
                                   objGLSetup.EnumAccountCodeStatus.Active & "' " 'AND ACC.LocCode = '" & strLocation & "'"

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND Acc.LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_WOGLWOACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetWODetails.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode").Trim() & " (" & dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Select " & lblWOGLWOAccCodeTag.Text
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlWOGLWOAccCode.DataSource = dsForDropDown.Tables(0)
        ddlWOGLWOAccCode.DataValueField = "AccCode"
        ddlWOGLWOAccCode.DataTextField = "Description"
        ddlWOGLWOAccCode.DataBind()

        If intSelectedIndex = 0 And Not strId = "" Then
            strParam = "Order By ACC.AccCode|And ACC.AccCode = '" & _
                        pv_strAccCode & "'"

            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                    ddlWOGLWOAccCode.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode")) & _
                     " (" & objGLSetup.mtdGetAccStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode"))))
                    intSelectedIndex = ddlWOGLWOAccCode.Items.Count - 1
                Else 
                    intSelectedIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_WOGLWOACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetWODetails.aspx")
            End Try
        End If

        ddlWOGLWOAccCode.SelectedIndex = intSelectedIndex
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
      
        If ddlWOGLWOAccCode.SelectedItem.Value = "" Then
            lblWOGLWOAccCodeErr.Visible = True
            Exit Sub
        End If
        If txtAssetValue.Text > 0 Then
            If txtAccumDeprValue.Text < 0 Or txtWOValue.Text < 0 Then
                lblWOValueErr.Visible = True
                Exit Sub
            End If
        ElseIf txtAssetValue.Text < 0 Then
            If txtAccumDeprValue.Text > 0 Or txtWOValue.Text > 0 Then
                lblWOValueErr.Visible = True
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

        If hidFiskalSame.Text = "1" Then
            txtAssetValueF.Text = txtAssetValue.Text
            txtAccumDeprValueF.Text = txtAccumDeprValue.Text
            txtWOValueF.Text = txtWOValue.Text
        End If

        strOpCode = "FA_CLSTRX_ASSETWO_UPD"

        strParamName = "TRXID|REFNO|REFDATE|" & _
                      "ASSETCODE|WOACCCODE|WOBLKCODE|" & _
                      "QTY|ASSETVALUE|ACCUMDEPRVALUE|REMARK|USERID|LOCCODE|NILAIPEROLEHAN|ASSETVALUEF|ACCUMDEPRVALUEF"

        Dim strBlockCd As String
        strBlockCd = Request.Form("ddlWOGLWOBlkCode")

        strParamValue = lblTxID.Text & "|" & txtRefNo.Text & "|" & _
                        strDate & "|" & _
                        ddlAssetCode.SelectedItem.Value & "|" & _
                        ddlWOGLWOAccCode.SelectedItem.Value & "|" & _
                        strBlockCd & "|" & _
                        txtQty.Text & "|" & _
                        txtAssetValue.Text & "|" & _
                        txtAccumDeprValue.Text & "|" & _
                        txtRemark.Text & "|" & strUserId & "|" & strLocation & "|" & _
                        txtNP.Text & "|" & _
                        txtAssetValueF.Text & "|" & _
                        txtAccumDeprValueF.Text


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
            lblDeleteErr.Visible = "True"
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

        If hidFiskalSame.Text = "1" Then
            txtAssetValueF.Text = txtAssetValue.Text
            txtAccumDeprValueF.Text = txtAccumDeprValue.Text
            txtWOValueF.Text = txtWOValue.Text
        End If

        strOpCode = "FA_CLSTRX_ASSETWO_ADD"

        strParamName = "REFNO|REFDATE|" & _
                      "ASSETCODE|WOACCCODE|WOBLKCODE|" & _
                      "QTY|ASSETVALUE|ACCUMDEPRVALUE|REMARK|LOCCODE|" & _
                      "USERID|ACCPERIODE|NILAIPEROLEHAN|ASSETVALUEF|ACCUMDEPRVALUEF"

        Dim strBlockCd As String
        strBlockCd = Request.Form("ddlWOGLWOBlkCode")

        strParamValue = txtRefNo.Text & "|" & _
                        strDate & "|" & _
                        ddlAssetCode.SelectedItem.Value & "|" & _
                        ddlWOGLWOAccCode.SelectedItem.Value & "|" & _
                        strBlockCd & "|" & _
                        txtQty.Text & "|" & _
                        txtAssetValue.Text & "|" & _
                        txtAccumDeprValue.Text & "|" & _
                        txtRemark.Text & "|" & _
                        strLocation & "|" & strUserId & "|" & _
                        Format(dtmDate, "yyyyMM") & "|" & _
                        txtNP.Text & "|" & _
                        txtAssetValueF.Text & "|" & _
                        txtAccumDeprValueF.Text


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
            lblDeleteErr.Visible = "True"
        End If


    End Sub


    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
       
        lblOper.Text = objFATrx.EnumOperation.Confirm

        Call ConfirmData()

    End Sub

    Sub ConfirmData()

        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objRslSet As DataSet

        strOpCode = "FA_CLSTRX_ASSETWO_CONFIRM"

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

        strOpCode = "FA_CLSTRX_ASSETWO_DELETE"

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
        Response.Redirect("FA_trx_AssetWOList.aspx")
    End Sub

    
    Sub CheckWOGLWOAccBlk(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim intAccType As Integer
        Dim strAcc As String = Request.Form("ddlWOGLWOAccCode")
        Dim strBlk As String = Request.Form("ddlWOGLWOBlkCode")

        GetAccountDetails(strAcc, intAccType)

        If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then 
            BindWOGLWOBlkDropList(strAcc, strBlk)
            WOGLWOBlkCode.Visible = True
        ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet Then 
            BindWOGLWOBlkDropList("")
            WOGLWOBlkCode.Visible = False
        End If

    End Sub

    
    Sub BindWOGLWOBlkDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")
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
            WOGLWOBlkCode.Visible = True
            dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & " Cost Center"

        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)
        ddlWOGLWOBlkCode.DataSource = dsForDropDown.Tables(0)
        ddlWOGLWOBlkCode.DataValueField = "BlkCode"
        ddlWOGLWOBlkCode.DataTextField = "Description"
        ddlWOGLWOBlkCode.DataBind()
        ddlWOGLWOBlkCode.SelectedIndex = intSelectedIndex

        If dsForDropDown.Tables(0).Rows.Count = 2 And lblTxID.Text = "" Then
            ddlWOGLWOBlkCode.SelectedIndex = 1
        End If

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
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



End Class
