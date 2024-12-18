
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


Public Class FA_setup_AssetReglnDetails : Inherits Page

    Dim objFASetup As New agri.FA.clsSetup()
    Dim objFATrx As New agri.FA.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblCode As Label
    Protected WithEvents lblOper As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents txtAssetCode As TextBox
    Protected WithEvents rfvAssetCode As RequiredFieldValidator
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents rfvDescription As RequiredFieldValidator
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents txtExtDescription As HtmlTextArea
    Protected WithEvents lblExtDescriptionErr As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents txtQty As TextBox
    Protected WithEvents rvQty As RangeValidator
    Protected WithEvents rfvQty As RequiredFieldValidator

    Protected WithEvents ddlUOM As DropDownList
    Protected WithEvents lblUOMErr As Label

    Protected WithEvents ddlAssetHeaderCode As DropDownList
    Protected WithEvents lblAssetHeaderCodeErr As Label
    Protected WithEvents txtSerialNo As TextBox
    Protected WithEvents ddlAssetCondition As DropDownList
    Protected WithEvents lblAssetConditionErr As Label
    Protected WithEvents ddlAcquisitionMode As DropDownList
    Protected WithEvents lblAcquisitionModeErr As Label
    Protected WithEvents txtPurchaseDate As TextBox
    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents lblPurchaseDateErr As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents ddlDeptCode As DropDownList
    Protected WithEvents ddlGLAssetAccCode As DropDownList
    Protected WithEvents lblGLAssetAccCodeErr As Label
    Protected WithEvents ddlDeprGLDeprAccCode As DropDownList
    Protected WithEvents lblDeprGLDeprAccCodeErr As Label
    Protected WithEvents ddlDeprGLDeprBlkCode As DropDownList
    Protected WithEvents lblDeprGLDeprBlkCodeErr As Label
    Protected WithEvents GLDeprBlkCode As HtmlTableRow
	
	
    Protected WithEvents trAsset As HtmlTableRow
    Protected WithEvents trTransfer As HtmlTableRow


    Protected WithEvents ddlDeprGLAccumDeprAccCode As DropDownList
    Protected WithEvents lblDeprGLAccumDeprAccCodeErr As Label
    Protected WithEvents cbDeprInd As CheckBox
    Protected WithEvents cbDeprIndF As CheckBox

    Protected WithEvents cbFiskalSame As CheckBox

    Protected WithEvents ddlDeprMethod As DropDownList
    Protected WithEvents lblDeprMethodErr As Label

    Protected WithEvents ddlDeprMethodF As DropDownList
    Protected WithEvents lblDeprMethodErrF As Label

    Protected WithEvents ddlNumber As DropDownList

    Protected WithEvents ddlFiskalCategory As DropDownList
    Protected WithEvents lblFiskalCategoryErr As Label
    Protected WithEvents txtLifeTime As TextBox
    Protected WithEvents rfvLifeTime As RequiredFieldValidator
    Protected WithEvents revLifeTime As RegularExpressionValidator
    Protected WithEvents rvLifeTime As RangeValidator
    Protected WithEvents txtLifeTimeF As TextBox
    Protected WithEvents rfvLifeTimeF As RequiredFieldValidator
    Protected WithEvents revLifeTimeF As RegularExpressionValidator
    Protected WithEvents rvLifeTimeF As RangeValidator
    Protected WithEvents txtStartMonth As TextBox
    Protected WithEvents rfvStartMonth As RequiredFieldValidator
    Protected WithEvents revStartMonth As RegularExpressionValidator
    Protected WithEvents rvStartMonth As RangeValidator
    Protected WithEvents txtStartYear As TextBox
    Protected WithEvents rfvStartYear As RequiredFieldValidator
    Protected WithEvents revStartYear As RegularExpressionValidator
    Protected WithEvents rvStartYear As RangeValidator
    Protected WithEvents txtStartMonthF As TextBox
    Protected WithEvents rfvStartMonthF As RequiredFieldValidator
    Protected WithEvents revStartMonthF As RegularExpressionValidator
    Protected WithEvents rvStartMonthF As RangeValidator
    Protected WithEvents txtStartYearF As TextBox
    Protected WithEvents rfvStartYearF As RequiredFieldValidator
    Protected WithEvents revStartYearF As RegularExpressionValidator
    Protected WithEvents rvStartYearF As RangeValidator

    Protected WithEvents tblFiscal As HtmlTable

    'Protected WithEvents lblLastDeprPeriod As Label
    'Protected WithEvents lblLastDeprPeriodF As Label
    Protected WithEvents lblDeprMonth As Label
    Protected WithEvents lblDeprMonthF As Label
    Protected WithEvents txtAssetValue As TextBox
    Protected WithEvents txtAssetValueF As TextBox
    Protected WithEvents txtAccumDeprValue As TextBox
    Protected WithEvents txtAccumDeprValueF As TextBox
    Protected WithEvents txtDispWOAssetValue As Label
    Protected WithEvents txtDispWOAssetValueF As Label
    Protected WithEvents txtDispWOAccumDeprValue As Label
    Protected WithEvents txtDispWOAccumDeprValueF As Label
    Protected WithEvents lblNetValue As Label
    Protected WithEvents lblNetValueF As Label
    Protected WithEvents txtFinalValue As TextBox
    Protected WithEvents rfvFinalValue As RequiredFieldValidator
    Protected WithEvents txtFinalValueF As TextBox
    Protected WithEvents rfvFinalValueF As RequiredFieldValidator
    Protected WithEvents txtBegYearValue As TextBox
    Protected WithEvents txtBegYearValueF As TextBox
    Protected WithEvents lblSaveErr As Label
    Protected WithEvents lblDeleteErr As Label
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents NewBtn As ImageButton

    Protected WithEvents ddlAsset As DropDownList
    Protected WithEvents ddlTransfer As DropDownList


    Protected WithEvents hidQty As HtmlInputHidden
    Protected WithEvents hidAmount As HtmlInputHidden

    Protected WithEvents ddlTrxType As DropDownList
    Protected WithEvents ddlKategori As DropDownList
    Protected WithEvents ddlGLAssetAccCodeLease As DropDownList
    Protected WithEvents lblGLAssetAccCodeLeaseErr As Label
    Protected WithEvents ddlDeprGLAccumDeprAccCodeLease As DropDownList
    Protected WithEvents lblDeprGLAccumDeprAccCodeLeaseErr As Label

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
    Dim intConfigSetting As Integer

    Dim strAssetCode As String
    Dim intDeprInd As Integer

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

 
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegLine), intFAAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblDeleteErr.Visible = False
            lblSaveErr.Visible = False
            lblDeprMethodErr.Visible = False
            lblFiskalCategoryErr.Visible = False
            lblDeprGLAccumDeprAccCodeErr.Visible = False
            lblDeprGLDeprAccCodeErr.Visible = False
            lblGLAssetAccCodeErr.Visible = False
            lblPurchaseDateErr.Visible = False
            lblAcquisitionModeErr.Visible = False
            lblAssetConditionErr.Visible = False
            lblAssetHeaderCodeErr.Visible = False
            lblDupMsg.Visible = False
            lblUOMErr.Visible = False
            lblGLAssetAccCodeLeaseErr.Visible = False
            lblDeprGLAccumDeprAccCodeLeaseErr.Visible = False

            onload_GetLangCap()


            If Not IsPostBack Then

                'cbFiskalSame.Attributes.Add("onclick", "JavaScript:setFiscal()")

                If Not Request.QueryString("AssetCode") = "" Then
                    strAssetCode = Request.QueryString("AssetCode")
                    txtAssetCode.Text = strAssetCode
                End If

                If Not strAssetCode = "" Then
                    lblOper.Text = ""
                    txtAssetCode.Text = strAssetCode
                    DisplayData()
                Else
                    lblOper.Text = ""

                    BindAssetIssue()

                    BindAssetTransfer()

                    BindAssetHeaderCode("")
                    BindAcquisitionMode("")
                    BindDeprMethod("")
                    BindDeprMethodF("")
                    txtLifeTime.Text = 0
                    txtLifeTimeF.Text = 0
                    txtStartMonth.Text = strAccMonth
                    txtStartYear.Text = strAccYear
                    txtStartMonthF.Text = strAccMonth
                    txtStartYearF.Text = strAccYear
                    'lblLastDeprPeriod.Text = ""
                    'lblLastDeprPeriodF.Text = ""
                    lblDeprMonth.Text = ""
                    lblDeprMonthF.Text = ""
                    txtAssetValue.Text = 0
                    txtAssetValueF.Text = 0
                    txtAccumDeprValue.Text = 0
                    txtAccumDeprValueF.Text = 0
                    txtDispWOAssetValue.Text = 0
                    txtDispWOAssetValueF.Text = 0
                    txtDispWOAccumDeprValue.Text = 0
                    txtDispWOAccumDeprValueF.Text = 0
                    lblNetValue.Text = 0
                    lblNetValueF.Text = 0
                    txtFinalValue.Text = 0
                    txtFinalValueF.Text = 0
                    txtBegYearValue.Text = 0
                    txtBegYearValueF.Text = 0
                    cbFiskalSame.Checked = True
                    tblFiscal.Visible = False

                    BindDeprGLDeprAccCode("")
                    BindDeprGLAccumDeprAccCode("")
                    BindDispGLAssetAccCode("")
                    BindAssetCondition("")
                    BindFiskalCategory("")
                    BindUOM("")
                    BindDept("")
                    txtAssetCode.Text = ""
                    txtDescription.Text = ""
                    txtQty.Text = 1

                    BindDispGLAssetAccCodeLease("")
                    BindDeprGLAccumDeprAccCodeLease("")

                    EnableControl()

                    btnSave.Visible = True
                    btnDelete.Visible = False
                    btnUnDelete.Visible = False
                End If
            End If
        End If
    End Sub


    Sub onload_GetLangCap()

        lblAssetHeaderCodeErr.Text = "Please select Jenis Asset"
        lblAcquisitionModeErr.Text = "Please select Cara Perolehan"
        lblDeprMethodErr.Text = "Please select Metoda Penyusutan"
        lblDeprGLDeprAccCodeErr.Text = "Please select Akun"
        lblDeprGLDeprBlkCodeErr.Text = "Please select Cost Center"
        lblDeprGLAccumDeprAccCodeErr.Text = "Please select Akun"
        lblGLAssetAccCodeErr.Text = "Please select Akun"

        lblFiskalCategoryErr.Text = "Please select Kategory Fiskal"
        lblAssetConditionErr.Text = "Please select Kondisi Asset"

        lblGLAssetAccCodeLeaseErr.Text = "Please select Akun"
        lblDeprGLAccumDeprAccCodeLeaseErr.Text = "Please select Akun"

    End Sub

    Protected Function LoadData2() As DataSet

        Dim strOpCode As String = "FA_CLSSETUP_ASSET_DETAILTRAN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim strTrxID As String = Request.Form("ddlTransfer")
        Dim dsResult As DataSet
        Dim strAccCode As String


        strParamName = "TRXID"
        strParamValue = strTrxID

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                                     strParamName, _
                                                     strParamValue, _
                                                     objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnList.aspx")
        End Try

        Return objDataSet

    End Function


    Protected Function LoadData() As DataSet

        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String

        strOpCode = "FA_CLSSETUP_ASSETREGLN_GETDTL"
        strParamName = "ASSETCODE|LOCCODE"
        strParamValue = txtAssetCode.Text & "|" & strLocation

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                                     strParamName, _
                                                     strParamValue, _
                                                     objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnList.aspx")
        End Try

        Return objDataSet
    End Function

    Sub DisableControl()
        Dim strView As Boolean

        strView = False

        txtAssetCode.Enabled = strView
        txtQty.Enabled = strView
        ddlUOM.Enabled = strView
        ddlAssetHeaderCode.Enabled = strView

        txtPurchaseDate.Enabled = strView
        btnSelDateFrom.Visible = strView

        ddlGLAssetAccCode.Enabled = strView
        ddlDeprGLDeprAccCode.Enabled = strView
        ddlDeprGLDeprBlkCode.Enabled = strView
        ddlDeprGLAccumDeprAccCode.Enabled = strView
        ddlDeprMethod.Enabled = strView
        ddlDeprMethodF.Enabled = strView
        ddlFiskalCategory.Enabled = strView

        txtLifeTime.Enabled = strView
        txtLifeTimeF.Enabled = strView

        txtStartMonth.Enabled = strView
        txtStartMonthF.Enabled = strView
        txtStartYear.Enabled = strView
        txtStartYearF.Enabled = strView

        txtAssetValue.Enabled = strView
        txtAssetValueF.Enabled = strView
        txtAccumDeprValue.Enabled = strView
        txtAccumDeprValueF.Enabled = strView
        txtDispWOAssetValue.Enabled = strView
        txtDispWOAssetValueF.Enabled = strView
        txtDispWOAccumDeprValue.Enabled = strView
        txtDispWOAccumDeprValueF.Enabled = strView
        txtFinalValue.Enabled = strView
        txtFinalValueF.Enabled = strView

        txtBegYearValue.Enabled = strView
        txtBegYearValueF.Enabled = strView

    End Sub

    Sub EnableControl()
        Dim strView As Boolean

        strView = True

        txtAssetCode.Enabled = strView
        txtQty.Enabled = strView
        ddlUOM.Enabled = strView
        ddlAssetHeaderCode.Enabled = strView

        txtPurchaseDate.Enabled = strView
        btnSelDateFrom.Visible = strView

        ddlGLAssetAccCode.Enabled = strView
        ddlDeprGLDeprAccCode.Enabled = strView
        ddlDeprGLDeprBlkCode.Enabled = strView
        ddlDeprGLAccumDeprAccCode.Enabled = strView
        ddlDeprMethod.Enabled = strView
        ddlDeprMethodF.Enabled = strView
        ddlFiskalCategory.Enabled = strView

        txtLifeTime.Enabled = strView
        txtLifeTimeF.Enabled = strView

        txtStartMonth.Enabled = strView
        txtStartMonthF.Enabled = strView
        txtStartYear.Enabled = strView
        txtStartYearF.Enabled = strView

        txtAssetValue.Enabled = strView
        txtAssetValueF.Enabled = strView
        txtAccumDeprValue.Enabled = strView
        txtAccumDeprValueF.Enabled = strView
        txtDispWOAssetValue.Enabled = strView
        txtDispWOAssetValueF.Enabled = strView
        txtDispWOAccumDeprValue.Enabled = strView
        txtDispWOAccumDeprValueF.Enabled = strView
        txtFinalValue.Enabled = strView
        txtFinalValueF.Enabled = strView

        txtBegYearValue.Enabled = strView
        txtBegYearValueF.Enabled = strView
    End Sub

    Sub DisplayData(Optional ByVal vSource As Integer = 0)

        Dim dsTx As New Dataset

        If vSource = 0 Then
            dsTx = LoadData()
        Else
            dsTx = LoadData2()
        End If

        Dim dblNetValue As Double
        Dim dblNetValueF As Double
        Dim dblTrxAssetVal As Double
        Dim dblTrxAssetValF As Double
        Dim dblTrxAssetAccDeprVal As Double
        Dim dblTrxAssetAccDeprValF As Double

        If dsTx.Tables(0).Rows.Count > 0 Then

            txtAssetCode.Text = dsTx.Tables(0).Rows(0).Item("AssetCode").Trim

            If vSource = 0 Then

                lblStatus.Text = objFASetup.mtdGetAssetReglnStatus(Trim(dsTx.Tables(0).Rows(0).Item("Status")))
                lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
                lblLastUpdate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("UpdateDate")))
                lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("Username"))
                BindAcquisitionMode(Trim(dsTx.Tables(0).Rows(0).Item("AcquisitionMode")))
                BindDept(Trim(dsTx.Tables(0).Rows(0).Item("DeptCode")))
            End If

            txtDescription.Text = dsTx.Tables(0).Rows(0).Item("Description").Trim
            txtExtDescription.Value = dsTx.Tables(0).Rows(0).Item("ExtDescription").Trim


            txtQty.Text = dsTx.Tables(0).Rows(0).Item("Qty")
            BindUOM(Trim(dsTx.Tables(0).Rows(0).Item("UOM")))
            BindAssetHeaderCode(Trim(dsTx.Tables(0).Rows(0).Item("AssetHeaderCode")))
            txtSerialNo.Text = dsTx.Tables(0).Rows(0).Item("SerialNo")
            BindAssetCondition(Trim(dsTx.Tables(0).Rows(0).Item("AssetCondition")))
            txtPurchaseDate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsTx.Tables(0).Rows(0).Item("PurchaseDate")))

            BindDispGLAssetAccCode(Trim(dsTx.Tables(0).Rows(0).Item("AssetAccCode")))

            BindDeprGLDeprAccCode(Trim(dsTx.Tables(0).Rows(0).Item("DeprGLDeprAccCode")))
            BindDeprGLAccumDeprAccCode(Trim(dsTx.Tables(0).Rows(0).Item("DeprGLAccumDeprAccCode")))

            If dsTx.Tables(0).Rows(0).Item("DeprInd") = objFASetup.EnumDeprInd.Yes Then
                cbDeprInd.Checked = True
            Else
                cbDeprInd.Checked = False
            End If

            If dsTx.Tables(0).Rows(0).Item("DeprIndF") = objFASetup.EnumDeprInd.Yes Then
                cbDeprIndF.Checked = True
            Else
                cbDeprIndF.Checked = False
            End If


            BindDeprMethod(Trim(dsTx.Tables(0).Rows(0).Item("DeprMethod")))
            BindDeprMethodF(Trim(dsTx.Tables(0).Rows(0).Item("DeprMethodF")))

            BindFiskalCategory(Trim(dsTx.Tables(0).Rows(0).Item("FiskalCategory")))
            txtLifeTime.Text = dsTx.Tables(0).Rows(0).Item("LifeTime")
            txtLifeTimeF.Text = dsTx.Tables(0).Rows(0).Item("LifeTimeF")
            txtStartMonth.Text = dsTx.Tables(0).Rows(0).Item("StartMonth").Trim
            txtStartMonthF.Text = dsTx.Tables(0).Rows(0).Item("StartMonthF").Trim
            txtStartYear.Text = dsTx.Tables(0).Rows(0).Item("StartYear").Trim
            txtStartYearF.Text = dsTx.Tables(0).Rows(0).Item("StartYearF").Trim

            txtAssetValue.Text = dsTx.Tables(0).Rows(0).Item("AssetValue") '- dsTx.Tables(0).Rows(0).Item("DispAssetValue")
            txtAssetValueF.Text = dsTx.Tables(0).Rows(0).Item("AssetValueF")
            txtAccumDeprValue.Text = dsTx.Tables(0).Rows(0).Item("AccumDeprValue")
            txtAccumDeprValueF.Text = dsTx.Tables(0).Rows(0).Item("AccumDeprValueF")


            dblTrxAssetVal = dsTx.Tables(0).Rows(0).Item("AddAssetValue") - dsTx.Tables(0).Rows(0).Item("WOAssetValue") - dsTx.Tables(0).Rows(0).Item("DispAssetValue")
            dblTrxAssetValF = dsTx.Tables(0).Rows(0).Item("AddAssetValue") - dsTx.Tables(0).Rows(0).Item("WOAssetValueF") - dsTx.Tables(0).Rows(0).Item("DispAssetValueF")
            dblTrxAssetAccDeprVal = dsTx.Tables(0).Rows(0).Item("DepAssetValue") - dsTx.Tables(0).Rows(0).Item("DispAccumDeprValue") - dsTx.Tables(0).Rows(0).Item("WOAccumDeprValue")
            dblTrxAssetAccDeprValF = dsTx.Tables(0).Rows(0).Item("DepAssetValueF") - dsTx.Tables(0).Rows(0).Item("DispAccumDeprValueF") - dsTx.Tables(0).Rows(0).Item("WOAccumDeprValueF")


            txtDispWOAssetValue.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dblTrxAssetVal, 2), 2)
            txtDispWOAssetValueF.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dblTrxAssetValF, 2), 2)
            txtDispWOAccumDeprValue.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dblTrxAssetAccDeprVal, 2), 2)
            txtDispWOAccumDeprValueF.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dblTrxAssetAccDeprValF, 2), 2)


            dblNetValue = dsTx.Tables(0).Rows(0).Item("AssetValue") + dblTrxAssetVal - _
                         (dsTx.Tables(0).Rows(0).Item("AccumDeprValue") + dblTrxAssetAccDeprVal)

            dblNetValueF = dsTx.Tables(0).Rows(0).Item("AssetValueF") + dblTrxAssetValF - _
                         (dsTx.Tables(0).Rows(0).Item("AccumDeprValueF") + dblTrxAssetAccDeprValF)

            lblNetValue.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dblNetValue, 2), 2)
            lblNetValueF.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dblNetValueF, 2), 2)

            txtFinalValue.Text = dsTx.Tables(0).Rows(0).Item("FinalValue")
            txtFinalValueF.Text = dsTx.Tables(0).Rows(0).Item("FinalValueF")
            txtBegYearValue.Text = dsTx.Tables(0).Rows(0).Item("BegYearValue")
            txtBegYearValueF.Text = dsTx.Tables(0).Rows(0).Item("BegYearValueF")

            lblDeprMonth.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dsTx.Tables(0).Rows(0).Item("MonthlyDepr"), 2), 2)
            lblDeprMonthF.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dsTx.Tables(0).Rows(0).Item("MonthlyDeprF"), 2), 2)

            ddlKategori.SelectedValue = dsTx.Tables(0).Rows(0).Item("Kategori")
            ddlTrxType.SelectedValue = dsTx.Tables(0).Rows(0).Item("TrxType")
            BindDispGLAssetAccCodeLease(Trim(dsTx.Tables(0).Rows(0).Item("AssetAccCodeLease")))
            BindDeprGLAccumDeprAccCodeLease(Trim(dsTx.Tables(0).Rows(0).Item("DeprGLAccumDeprAccCodeLease")))



            If dsTx.Tables(0).Rows(0).Item("IsFiskalSame") = True Then
                cbFiskalSame.Checked = True
                tblFiscal.Visible = False
            Else
                cbFiskalSame.Checked = False
                tblFiscal.Visible = True
            End If

            If Trim(dsTx.Tables(0).Rows(0).Item("DeprGLDeprBlkCode")) <> "" Then
                GLDeprBlkCode.Visible = True
                BindDeprGLDeprBlkDropList(Trim(dsTx.Tables(0).Rows(0).Item("DeprGLDeprAccCode")), Trim(dsTx.Tables(0).Rows(0).Item("DeprGLDeprBlkCode")))
            End If

            If vSource = 0 Then

                Select Case Trim(lblStatus.Text)
                    Case objFASetup.mtdGetAssetReglnStatus(objFASetup.EnumAssetReglnStatus.Active)
                        EnableControl()
                        txtAssetCode.Enabled = False
                        ddlAcquisitionMode.Enabled = False
                        btnSave.Visible = True
                        btnDelete.Visible = True
                        btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                        btnUnDelete.Visible = False
                    Case objFASetup.mtdGetAssetReglnStatus(objFASetup.EnumAssetReglnStatus.Deleted)
                        DisableControl()
                        btnSave.Visible = False
                        btnDelete.Visible = False
                        btnUnDelete.Visible = True
                    Case objFASetup.mtdGetAssetReglnStatus(objFASetup.EnumAssetReglnStatus.Disposed)
                        DisableControl()
                        btnSave.Visible = False
                        btnDelete.Visible = False
                        btnUnDelete.Visible = False
                    Case objFASetup.mtdGetAssetReglnStatus(objFASetup.EnumAssetReglnStatus.WrittenOff)
                        DisableControl()
                        btnSave.Visible = False
                        btnDelete.Visible = False
                        btnUnDelete.Visible = False
                    Case objFASetup.mtdGetAssetReglnStatus(objFASetup.EnumAssetReglnStatus.Transfer)
                        DisableControl()
                        btnSave.Visible = False
                        btnDelete.Visible = False
                        btnUnDelete.Visible = False
                End Select

                If dblTrxAssetAccDeprVal <> 0 Then
                    txtAssetValue.enabled = False
                    txtAccumDeprValue.enabled = False
                    txtFinalValue.enabled = False
                    txtBegYearValue.enabled = False
                    ddlDeprMethod.enabled = False
                    txtLifeTime.enabled = False
                    txtStartMonth.enabled = False
                    txtStartYear.enabled = False

                    txtAssetValueF.enabled = False
                    txtAccumDeprValueF.enabled = False
                    txtFinalValueF.enabled = False
                    txtBegYearValueF.enabled = False
                    ddlDeprMethodF.enabled = False
                    txtLifeTimeF.enabled = False
                    txtStartMonthF.enabled = False
                    txtStartYearF.enabled = False

                End If

                trAsset.Visible = False
                trTransfer.Visible = False
                ddlNumber.Visible = False
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnList.aspx")
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

    Sub BindAssetTransfer()

        Dim strOpCode As String = "FA_CLSSETUP_ASSET_TRAN_LIST_GET"
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnList.aspx")
        End Try


        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("Code") = dsForDropDown.Tables(0).Rows(intCnt).Item("Code").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = format(dsForDropDown.Tables(0).Rows(intCnt).Item("RefDate"), "dd/MM/yyyy") & " (" & dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("Code") = ""
        dr("Description") = "Pilih Asset"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlTransfer.DataSource = dsForDropDown.Tables(0)
        ddlTransfer.DataValueField = "Code"
        ddlTransfer.DataTextField = "Description"
        ddlTransfer.DataBind()

        ddlTransfer.SelectedIndex = 0

    End Sub



    Sub BindAssetHeaderCode(ByVal pv_strAssetHeaderCode As String)
        Dim strOpCode As String = "FA_CLSSETUP_ASSETREG_LIST_GET"
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

        strParam = "||||" & objFASetup.EnumAssetRegStatus.Active & "||" & _
                    "FA.AssetHeaderCode ASC "

        Try
            intErrNo = objFASetup.mtdGetAssetReg(strOpCode, strLocation, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("AssetHeaderCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("AssetHeaderCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("AssetHeaderCode").Trim() & " (" & dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim() & ")" & _
                                                                        " - " & dsForDropDown.Tables(0).Rows(intCnt).Item("AssetClassDesc").Trim()
            If dsForDropDown.Tables(0).Rows(intCnt).Item("AssetHeaderCode") = Trim(pv_strAssetHeaderCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AssetHeaderCode") = ""
        dr("Description") = "Pilih Jenis Asset"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlAssetHeaderCode.DataSource = dsForDropDown.Tables(0)
        ddlAssetHeaderCode.DataValueField = "AssetHeaderCode"
        ddlAssetHeaderCode.DataTextField = "Description"
        ddlAssetHeaderCode.DataBind()

        If intSelectedIndex = 0 And Not strAssetCode = "" Then
            strParam = pv_strAssetHeaderCode & "||||||" & _
                        "FA.AssetHeaderCode ASC "

            Try
                intErrNo = objFASetup.mtdGetAssetReg(strOpCode, strLocation, strParam, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then
                    ddlAssetHeaderCode.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AssetHeaderCode")) & _
                     " (" & objFASetup.mtdGetAssetRegStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AssetHeaderCode"))))
                    intSelectedIndex = ddlAssetHeaderCode.Items.Count - 1
                Else
                    intSelectedIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
            End Try
        End If

        ddlAssetHeaderCode.SelectedIndex = intSelectedIndex

    End Sub


    Sub BindUOM(ByVal pv_strUOM As String)
        Dim strOpCode As String = "ADMIN_CLSUOM_UOM_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim dr As DataRow

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = "AND UOM.STATUS = 1|"

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                                     strParamName, _
                                                     strParamValue, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnList.aspx")
        End Try


        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("UOMCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("UOMCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("UOMDesc") = dsForDropDown.Tables(0).Rows(intCnt).Item("UOMCode").Trim() & " (" & dsForDropDown.Tables(0).Rows(intCnt).Item("UOMDesc").Trim() & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(pv_strUOM) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("UOMCode") = ""
        dr("UOMDesc") = "Pilih UOM"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlUOM.DataSource = dsForDropDown.Tables(0)
        ddlUOM.DataValueField = "UOMCode"
        ddlUOM.DataTextField = "UOMDesc"
        ddlUOM.DataBind()

        ddlUOM.SelectedIndex = intSelectedIndex

    End Sub


    Sub BindDept(ByVal pv_strDept As String)
        Dim strOpCode As String = "HR_CLSSETUP_DEPT_SEARCH1"
        Dim strParamName As String
        Dim strParamValue As String
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim dr As DataRow

        strParamName = "STRSEARCH"
        strParamValue = "and A.LocCode = '" & strLocation & "' and A.Status = '1'"

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                                     strParamName, _
                                                     strParamValue, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLN_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnList.aspx")
        End Try


        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("DeptCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("DeptCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("_Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("_Description").Trim()
            If dsForDropDown.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(pv_strDept) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("DeptCode") = ""
        dr("_Description") = "Pilih Lokasi Aset"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlDeptCode.DataSource = dsForDropDown.Tables(0)
        ddlDeptCode.DataValueField = "DeptCode"
        ddlDeptCode.DataTextField = "_Description"
        ddlDeptCode.DataBind()

        ddlDeptCode.SelectedIndex = intSelectedIndex

    End Sub


    Sub BindAcquisitionMode(ByVal pv_strAcquisitionMode As String)
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer

        If ddlAcquisitionMode.Items.Count = 0 Then
            'ddlAcquisitionMode.Items.Add(New ListItem("Pilih Cara Perolehan", ""))
            ddlAcquisitionMode.Items.Add(New ListItem(objFASetup.mtdGetAcquisitionMode(objFASetup.EnumAcquisitionMode.NewPurchase), objFASetup.EnumAcquisitionMode.NewPurchase))
            ddlAcquisitionMode.Items.Add(New ListItem(objFASetup.mtdGetAcquisitionMode(objFASetup.EnumAcquisitionMode.TransferIn), objFASetup.EnumAcquisitionMode.TransferIn))
        End If

        If Trim(pv_strAcquisitionMode) <> "" Then
            For intCnt = 0 To ddlAcquisitionMode.Items.Count - 1
                If ddlAcquisitionMode.Items(intCnt).Value = pv_strAcquisitionMode Then
                    intSelectedIndex = intCnt
                End If
            Next
            ddlAcquisitionMode.SelectedIndex = intSelectedIndex
        Else
            ddlAcquisitionMode.SelectedIndex = 0
        End If
    End Sub


    Sub BindDeprMethod(ByVal pv_strDeprMethod As String)
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer

        If ddlDeprMethod.Items.Count = 0 Then
            ddlDeprMethod.Items.Add(New ListItem("Pilih Metoda Penyusutan", ""))
            ddlDeprMethod.Items.Add(New ListItem(objFASetup.mtdGetDeprMethod(objFASetup.EnumDeprMethod.None), objFASetup.EnumDeprMethod.None))
            ddlDeprMethod.Items.Add(New ListItem(objFASetup.mtdGetDeprMethod(objFASetup.EnumDeprMethod.StraightLine), objFASetup.EnumDeprMethod.StraightLine))
            ddlDeprMethod.Items.Add(New ListItem(objFASetup.mtdGetDeprMethod(objFASetup.EnumDeprMethod.Declining), objFASetup.EnumDeprMethod.Declining))
            ddlDeprMethod.Items.Add(New ListItem(objFASetup.mtdGetDeprMethod(objFASetup.EnumDeprMethod.DoubleDeclining), objFASetup.EnumDeprMethod.DoubleDeclining))
        End If

        If Trim(pv_strDeprMethod) <> "" Then
            For intCnt = 0 To ddlDeprMethod.Items.Count - 1
                If ddlDeprMethod.Items(intCnt).Value = pv_strDeprMethod Then
                    intSelectedIndex = intCnt
                End If
            Next
            ddlDeprMethod.SelectedIndex = intSelectedIndex
        Else
            ddlDeprMethod.SelectedIndex = 0
        End If
    End Sub

    Sub BindDeprMethodF(ByVal pv_strDeprMethod As String)
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer

        If ddlDeprMethodF.Items.Count = 0 Then
            ddlDeprMethodF.Items.Add(New ListItem("Pilih Metoda Penyusutan", ""))
            ddlDeprMethodF.Items.Add(New ListItem(objFASetup.mtdGetDeprMethod(objFASetup.EnumDeprMethod.None), objFASetup.EnumDeprMethod.None))
            ddlDeprMethodF.Items.Add(New ListItem(objFASetup.mtdGetDeprMethod(objFASetup.EnumDeprMethod.StraightLine), objFASetup.EnumDeprMethod.StraightLine))
            ddlDeprMethodF.Items.Add(New ListItem(objFASetup.mtdGetDeprMethod(objFASetup.EnumDeprMethod.Declining), objFASetup.EnumDeprMethod.Declining))
            ddlDeprMethodF.Items.Add(New ListItem(objFASetup.mtdGetDeprMethod(objFASetup.EnumDeprMethod.DoubleDeclining), objFASetup.EnumDeprMethod.DoubleDeclining))
        End If

        If Trim(pv_strDeprMethod) <> "" Then
            For intCnt = 0 To ddlDeprMethod.Items.Count - 1
                If ddlDeprMethodF.Items(intCnt).Value = pv_strDeprMethod Then
                    intSelectedIndex = intCnt
                End If
            Next
            ddlDeprMethodF.SelectedIndex = intSelectedIndex
        Else
            ddlDeprMethodF.SelectedIndex = 0
        End If
    End Sub


    Sub BindDeprGLDeprAccCode(ByVal pv_strAccCode As String)
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

        If Session("SS_COACENTRALIZED") = "1" Then
            strParam = "Order By ACC.AccCode|And ACC.AccPurpose = " & objGLSetup.EnumAccountPurpose.NonVehicle & _
                    " And ACC.Status = '" & _
                    objGLSetup.EnumAccountCodeStatus.Active & "' AND Acc.AccType = '2'"

        Else
            strParam = "Order By ACC.AccCode|And ACC.AccPurpose = " & objGLSetup.EnumAccountPurpose.NonVehicle & _
                                " And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "' AND Acc.LocCode = '" & strLocation & "' AND Acc.AccType = '2'"

        End If

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_DEPRGLDEPRACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Pilih Akun"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlDeprGLDeprAccCode.DataSource = dsForDropDown.Tables(0)
        ddlDeprGLDeprAccCode.DataValueField = "AccCode"
        ddlDeprGLDeprAccCode.DataTextField = "_Description"
        ddlDeprGLDeprAccCode.DataBind()

        ddlDeprGLDeprAccCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindDeprGLAccumDeprAccCode(ByVal pv_strAccCode As String)
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

        If Session("SS_COACENTRALIZED") = "1" Then
            strParam = "Order By ACC.AccCode|And ACC.AccPurpose = " & objGLSetup.EnumAccountPurpose.NonVehicle & _
                        " And ACC.Status = '" & _
                        objGLSetup.EnumAccountCodeStatus.Active & "' AND Acc.AccType = '1'"

        Else
            strParam = "Order By ACC.AccCode|And ACC.AccPurpose = " & objGLSetup.EnumAccountPurpose.NonVehicle & _
            " And ACC.Status = '" & _
            objGLSetup.EnumAccountCodeStatus.Active & "' AND Acc.LocCode = '" & strLocation & "' AND Acc.AccType = '1'"

        End If

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_DEPRGLACCUMDEPRACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Pilih Akun"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlDeprGLAccumDeprAccCode.DataSource = dsForDropDown.Tables(0)
        ddlDeprGLAccumDeprAccCode.DataValueField = "AccCode"
        ddlDeprGLAccumDeprAccCode.DataTextField = "_Description"
        ddlDeprGLAccumDeprAccCode.DataBind()

        If intSelectedIndex = 0 And Not strAssetCode = "" Then
            strParam = "Order By ACC.AccCode|And ACC.AccCode = '" & _
                        pv_strAccCode & "'"

            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then
                    ddlDeprGLAccumDeprAccCode.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode")) & _
                     " (" & objGLSetup.mtdGetAccStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode"))))
                    intSelectedIndex = ddlDeprGLAccumDeprAccCode.Items.Count - 1
                Else
                    intSelectedIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_DEPRGLACCUMDEPRACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
            End Try
        End If

        ddlDeprGLAccumDeprAccCode.SelectedIndex = intSelectedIndex


        'ddlDeprGLAccumDeprAccCodeLease.DataSource = dsForDropDown.Tables(0)
        'ddlDeprGLAccumDeprAccCodeLease.DataValueField = "AccCode"
        'ddlDeprGLAccumDeprAccCodeLease.DataTextField = "_Description"
        'ddlDeprGLAccumDeprAccCodeLease.DataBind()
        'ddlDeprGLAccumDeprAccCodeLease.SelectedIndex = intSelectedIndex

    End Sub

    Sub BindDispGLAssetAccCode(ByVal pv_strAccCode As String)
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

        If Session("SS_COACENTRALIZED") = "1" Then
            strParam = "Order By ACC.AccCode|And ACC.AccPurpose = " & objGLSetup.EnumAccountPurpose.NonVehicle & _
                                " And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "' AND Acc.AccType = '1'"

        Else
            strParam = "Order By ACC.AccCode|And ACC.AccPurpose = " & objGLSetup.EnumAccountPurpose.NonVehicle & _
                                " And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "' AND Acc.LocCode = '" & strLocation & "' AND Acc.AccType = '1'"

        End If

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_DISPGLASSETACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Pilih Akun "
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlGLAssetAccCode.DataSource = dsForDropDown.Tables(0)
        ddlGLAssetAccCode.DataValueField = "AccCode"
        ddlGLAssetAccCode.DataTextField = "_Description"
        ddlGLAssetAccCode.DataBind()

        If intSelectedIndex = 0 And Not strAssetCode = "" Then
            strParam = "Order By ACC.AccCode|And ACC.AccCode = '" & _
                        pv_strAccCode & "'"

            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then
                    ddlGLAssetAccCode.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode")) & _
                     " (" & objGLSetup.mtdGetAccStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode"))))
                    intSelectedIndex = ddlGLAssetAccCode.Items.Count - 1
                Else
                    intSelectedIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_DISPGLASSETACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
            End Try
        End If

        ddlGLAssetAccCode.SelectedIndex = intSelectedIndex



        'ddlGLAssetAccCodeLease.DataSource = dsForDropDown.Tables(0)
        'ddlGLAssetAccCodeLease.DataValueField = "AccCode"
        'ddlGLAssetAccCodeLease.DataTextField = "_Description"
        'ddlGLAssetAccCodeLease.DataBind()
        'ddlGLAssetAccCodeLease.SelectedIndex = intSelectedIndex

    End Sub

    Sub BindAssetCondition(ByVal pv_strAssetCondition As String)
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer

        If ddlFiskalCategory.Items.Count = 0 Then
            ddlAssetCondition.Items.Add(New ListItem("Select Kondisi Asset", ""))
            ddlAssetCondition.Items.Add(New ListItem(objFASetup.mtdGetAssetCondition(objFASetup.EnumAssetCondition.VeryGood), objFASetup.EnumAssetCondition.VeryGood))
            ddlAssetCondition.Items.Add(New ListItem(objFASetup.mtdGetAssetCondition(objFASetup.EnumAssetCondition.Good), objFASetup.EnumAssetCondition.Good))
            ddlAssetCondition.Items.Add(New ListItem(objFASetup.mtdGetAssetCondition(objFASetup.EnumAssetCondition.OK), objFASetup.EnumAssetCondition.OK))
            ddlAssetCondition.Items.Add(New ListItem(objFASetup.mtdGetAssetCondition(objFASetup.EnumAssetCondition.Bad), objFASetup.EnumAssetCondition.Bad))
            ddlAssetCondition.Items.Add(New ListItem(objFASetup.mtdGetAssetCondition(objFASetup.EnumAssetCondition.VeryBad), objFASetup.EnumAssetCondition.VeryBad))
            ddlAssetCondition.Items.Add(New ListItem(objFASetup.mtdGetAssetCondition(objFASetup.EnumAssetCondition.NeedReplacement), objFASetup.EnumAssetCondition.NeedReplacement))
        End If

        If Trim(pv_strAssetCondition) <> "" Then
            For intCnt = 0 To ddlAssetCondition.Items.Count - 1
                If ddlAssetCondition.Items(intCnt).Value = pv_strAssetCondition Then
                    intSelectedIndex = intCnt
                End If
            Next
            ddlAssetCondition.SelectedIndex = intSelectedIndex
        Else
            ddlAssetCondition.SelectedIndex = 1
        End If
    End Sub

    Sub BindFiskalCategory(ByVal pv_strFiskalCategory As String)
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer

        If ddlFiskalCategory.Items.Count = 0 Then
            ddlFiskalCategory.Items.Add(New ListItem("Pilih Kategory Fiskal", ""))
            ddlFiskalCategory.Items.Add(New ListItem(objFASetup.mtdGetFiskalCategory(objFASetup.EnumFiskalCategory.Permanen), objFASetup.EnumFiskalCategory.Permanen))
            ddlFiskalCategory.Items.Add(New ListItem(objFASetup.mtdGetFiskalCategory(objFASetup.EnumFiskalCategory.NonPermanen), objFASetup.EnumFiskalCategory.NonPermanen))
            ddlFiskalCategory.Items.Add(New ListItem(objFASetup.mtdGetFiskalCategory(objFASetup.EnumFiskalCategory.Kelompok_1), objFASetup.EnumFiskalCategory.Kelompok_1))
            ddlFiskalCategory.Items.Add(New ListItem(objFASetup.mtdGetFiskalCategory(objFASetup.EnumFiskalCategory.Kelompok_2), objFASetup.EnumFiskalCategory.Kelompok_2))
            ddlFiskalCategory.Items.Add(New ListItem(objFASetup.mtdGetFiskalCategory(objFASetup.EnumFiskalCategory.Kelompok_3), objFASetup.EnumFiskalCategory.Kelompok_3))
            ddlFiskalCategory.Items.Add(New ListItem(objFASetup.mtdGetFiskalCategory(objFASetup.EnumFiskalCategory.Kelompok_4), objFASetup.EnumFiskalCategory.Kelompok_4))
            ddlFiskalCategory.Items.Add(New ListItem(objFASetup.mtdGetFiskalCategory(objFASetup.EnumFiskalCategory.None), objFASetup.EnumFiskalCategory.None))
        End If

        If Trim(pv_strFiskalCategory) <> "" Then
            For intCnt = 0 To ddlFiskalCategory.Items.Count - 1
                If ddlFiskalCategory.Items(intCnt).Value = pv_strFiskalCategory Then
                    intSelectedIndex = intCnt
                End If
            Next
            ddlFiskalCategory.SelectedIndex = intSelectedIndex
        Else
            ddlFiskalCategory.SelectedIndex = 0
        End If
    End Sub

    Sub UpdateData()

        Dim strDate As String = CheckDate()
        Dim TxID As String
        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String

        Dim strDeprMethodF As String
        Dim dblLifetimeF As String
        Dim strStartMonthF As String
        Dim strStartYearF As String
        Dim dblAssetValueF As String
        Dim dblAccumDeprValueF As String
        Dim dblFinalValueF As String
        Dim dblBegYearValueF As String
        Dim intDeprIndF As Integer
        Dim intSame As Integer

        strOpCode = "FA_CLSSETUP_ASSETREGLN_UPD"

        strParamName = "ASSETCODE|LOCCODE|" & _
                      "ASSETHEADERCODE|SERIALNO|" & _
                      "DESCRIPTION|EXTDESCRIPTION|" & _
                      "ACQUISITIONMODE|PURCHASEDATE|" & _
                      "DEPRMETHOD|DEPRMETHODF|FISKALCATEGORY|" & _
                      "LIFETIME|LIFETIMEF|" & _
                      "STARTMONTH|STARTMONTHF|" & _
                      "STARTYEAR|STARTYEARF|" & _
                      "ASSETVALUE|ASSETVALUEF|" & _
                      "ACCUMDEPRVALUE|ACCUMDEPRVALUEF|" & _
                      "FINALVALUE|FINALVALUEF|" & _
                      "BEGYEARVALUE|BEGYEARVALUEF|ISSAME|" & _
                      "UPDATEID|DEPRGLDEPRACCCODE|" & _
                      "DEPRGLDEPRBLKCODE|DEPRGLACCUMDEPRACCCODE|" & _
                      "ASSETACCCODE|ASSETCONDITION|" & _
                      "QTY|UOM|DEPTCODE|DEPRIND|DEPRINDF|" & _
                      "TRXTYPE|KATEGORI|ASSETACCCODELEASE|DEPRGLACCUMDEPRACCCODELEASE"

        intDeprInd = IIf(cbDeprInd.Checked = True, objFASetup.EnumDeprInd.Yes, objFASetup.EnumDeprInd.No)

        If cbFiskalSame.checked = True Then
            strDeprMethodF = ddlDeprMethod.SelectedItem.Value
            dblLifetimeF = txtLifeTime.Text
            strStartMonthF = txtStartMonth.Text
            strStartYearF = txtStartYear.Text
            dblAssetValueF = txtAssetValue.Text
            dblAccumDeprValueF = txtAccumDeprValue.Text
            dblFinalValueF = txtFinalValue.Text
            dblBegYearValueF = txtBegYearValue.Text
            intDeprIndF = intDeprInd
            intSame = 1
        Else
            strDeprMethodF = ddlDeprMethodF.SelectedItem.Value
            dblLifetimeF = txtLifeTimeF.Text
            strStartMonthF = txtStartMonthF.Text
            strStartYearF = txtStartYearF.Text
            dblAssetValueF = txtAssetValueF.Text
            dblAccumDeprValueF = txtAccumDeprValueF.Text
            dblFinalValueF = txtFinalValueF.Text
            dblBegYearValueF = txtBegYearValueF.Text
            intDeprIndF = IIf(cbDeprIndF.Checked = True, objFASetup.EnumDeprInd.Yes, objFASetup.EnumDeprInd.No)
            intSame = 0
        End If

        strParamValue = txtAssetCode.Text & "|" & strLocation & "|" & _
                    ddlAssetHeaderCode.SelectedItem.Value & "|" & txtSerialNo.Text & "|" & _
                    txtDescription.Text & "|" & txtExtDescription.Value & "|" & _
                    ddlAcquisitionMode.SelectedItem.Value & "|" & strDate & "|" & _
                    ddlDeprMethod.SelectedItem.Value & "|" & strDeprMethodF & "|" & ddlFiskalCategory.SelectedItem.Value & "|" & _
                    txtLifeTime.Text & "|" & dblLifetimeF & "|" & _
                    txtStartMonth.Text & "|" & strStartMonthF & "|" & _
                    txtStartYear.Text & "|" & strStartYearF & "|" & _
                    txtAssetValue.Text & "|" & dblAssetValueF & "|" & _
                    txtAccumDeprValue.Text & "|" & dblAccumDeprValueF & "|" & _
                    txtFinalValue.Text & "|" & dblFinalValueF & "|" & _
                    txtBegYearValue.Text & "|" & dblBegYearValueF & "|" & intSame & "|" & _
                    strUserId & "|" & ddlDeprGLDeprAccCode.SelectedItem.Value & "|" & _
                    ddlDeprGLDeprBlkCode.SelectedItem.Value & "|" & ddlDeprGLAccumDeprAccCode.SelectedItem.Value & "|" & _
                    ddlGLAssetAccCode.SelectedItem.Value & "|" & ddlAssetCondition.SelectedItem.Value & "|" & _
                    txtQty.Text & "|" & ddlUOM.SelectedItem.Value & "|" & _
                    ddlDeptCode.SelectedItem.Value & "|" & intDeprInd & "|" & intDeprIndF & "|" & _
                    ddlTrxType.SelectedItem.Value & "|" & ddlKategori.SelectedItem.Value & "|" & _
                    ddlGLAssetAccCodeLease.SelectedItem.Value & "|" & ddlDeprGLAccumDeprAccCodeLease.SelectedItem.Value

        Try
            intErrNo = objFATrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLNDETAILS_UPDATE&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        End Try

        Call DisplayData()

    End Sub

    Protected Function CheckDate() As String
        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        If Not txtPurchaseDate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, txtPurchaseDate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Text = objDateFormat & "."
                lblPurchaseDateErr.Visible = True
                lblFmt.Visible = True
            End If
        End If

    End Function

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        'validation
        If Len(txtExtDescription.Value.Trim) > 512 Then
            lblExtDescriptionErr.Visible = True
            Exit Sub
        End If
        If ddlAssetHeaderCode.SelectedItem.Value = "" Then
            lblAssetHeaderCodeErr.Visible = True
            Exit Sub
        End If
        If ddlAcquisitionMode.SelectedItem.Value = "" Then
            lblAcquisitionModeErr.Visible = True
            Exit Sub
        End If
        If txtPurchaseDate.Text = "" Then
            lblPurchaseDateErr.Visible = True
            lblPurchaseDateErr.Text = "Please insert Tanggal Perolehan"
            Exit Sub
        End If

        If ddlKategori.SelectedItem.Value = "1" Then
            If ddlDeprMethod.SelectedItem.Value = "" Then
                lblDeprMethodErr.Visible = True
                Exit Sub
            End If
            If ddlDeprGLDeprAccCode.SelectedItem.Value = "" Then
                lblDeprGLDeprAccCodeErr.Visible = True
                Exit Sub
            End If
            If ddlDeprGLAccumDeprAccCode.SelectedItem.Value = "" Then
                lblDeprGLAccumDeprAccCodeErr.Visible = True
                Exit Sub
            End If
            If ddlGLAssetAccCode.SelectedItem.Value = "" Then
                lblGLAssetAccCodeErr.Visible = True
                Exit Sub
            End If
            If Request.Form("ddlDeprGLDeprBlkCode") = "" And GLDeprBlkCode.Visible = True Then
                lblDeprGLDeprBlkCodeErr.Visible = True
                Exit Sub
            End If
            If ddlAssetCondition.SelectedItem.Value = "" Then
                lblAssetConditionErr.Visible = True
                Exit Sub
            End If
            If ddlFiskalCategory.SelectedItem.Value = "" Then
                lblFiskalCategoryErr.Visible = True
                Exit Sub
            End If
            If ddlUOM.SelectedItem.Value = "" Then
                lblUOMErr.Visible = True
                Exit Sub
            End If

            If ddlTrxType.SelectedItem.Value = "1" Then
                If ddlGLAssetAccCodeLease.SelectedItem.Value = "" Then
                    lblGLAssetAccCodeLeaseErr.Visible = True
                    Exit Sub
                End If
                If ddlDeprGLAccumDeprAccCodeLease.SelectedItem.Value = "" Then
                    lblDeprGLAccumDeprAccCodeLeaseErr.Visible = True
                    Exit Sub
                End If
            End If
        End If


        If Trim(lblCreateDate.Text) = "" Then
            Call AddData()
        Else
            Call UpdateData()
        End If



    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim TxID As String
        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String

        strOpCode = "FA_CLSSETUP_ASSETREGLN_DELETE"


        strParamName = "ASSETCODE|LOCCODE|" & _
                        "STATUS|UPDATEID"


        strParamValue = txtAssetCode.Text & "|" & strLocation & "|" & _
                      "2|" & strUserId



        Try
            intErrNo = objFATrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLNDETAILS_UPDATE&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        End Try

        Call DisplayData()

    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim TxID As String
        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String

        strOpCode = "FA_CLSSETUP_ASSETREGLN_UNDELETE"


        strParamName = "ASSETCODE|LOCCODE|" & _
                        "STATUS|UPDATEID"

        strParamValue = txtAssetCode.Text & "|" & strLocation & "|" & _
                     "1|" & strUserId


        Try
            intErrNo = objFATrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLNDETAILS_UPDATE&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        End Try

        Call DisplayData()


    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("FA_setup_AssetReglnList.aspx")
    End Sub

    Sub CheckGLDeprAccBlk(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim intAccType As Integer
        Dim strAcc As String = Request.Form("ddlDeprGLDeprAccCode")
        Dim strBlk As String = Request.Form("ddlDeprGLDeprBlkCode")

        GetAccountDetails(strAcc, intAccType)

        If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
            BindDeprGLDeprBlkDropList(strAcc, strBlk)
            GLDeprBlkCode.Visible = True
        ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet Then
            BindDeprGLDeprBlkDropList("")
            GLDeprBlkCode.Visible = False
        End If

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
           
			txtDescription.Text = dsResult.Tables(0).Rows(0).Item("Description").Trim
            txtQty.Text = format(dsResult.Tables(0).Rows(0).Item("Qty"), "###")
            txtPurchaseDate.Text = format(dsResult.Tables(0).Rows(0).Item("StockIssueDate"), "dd/MM/yyyy")

            ddlUOM.SelectedValue = dsResult.Tables(0).Rows(0).Item("UOMCode").Trim
            ddlGLAssetAccCode.SelectedValue = dsResult.Tables(0).Rows(0).Item("AccCode").Trim
            ddlAcquisitionMode.SelectedIndex = 0
            ddlAssetCondition.SelectedIndex = 1

            ddlDeprMethod.SelectedIndex = 2
            ddlDeprMethodF.SelectedIndex = 2

            txtAssetValue.Text = dsResult.Tables(0).Rows(0).Item("PriceAmount")
            txtAssetValueF.Text = dsResult.Tables(0).Rows(0).Item("PriceAmount")

            txtBegYearValue.Text = dsResult.Tables(0).Rows(0).Item("PriceAmount")
            txtBegYearValueF.Text = dsResult.Tables(0).Rows(0).Item("PriceAmount")

            hidQty.Value = dsResult.Tables(0).Rows(0).Item("Qty")
            hidAmount.Value = dsResult.Tables(0).Rows(0).Item("PriceAmount")

            Try
                If mid(dsResult.Tables(0).Rows(0).Item("AccCode").Trim, 1, 3) = "120" Then
                    strAccCode = "121" & mid(dsResult.Tables(0).Rows(0).Item("AccCode").Trim, 4)
                    ddlDeprGLAccumDeprAccCode.SelectedValue = strAccCode
                    'response.write(strAccCode)
                End If
                If mid(dsResult.Tables(0).Rows(0).Item("AccCode").Trim, 1, 3) = "122" Then
                    strAccCode = "123" & mid(dsResult.Tables(0).Rows(0).Item("AccCode").Trim, 4)
                    ddlDeprGLAccumDeprAccCode.SelectedValue = strAccCode
                    'response.write(strAccCode)
                End If
            Catch ex As Exception

            End Try


        End If

    End Sub

    Sub SelectAssetTran(ByVal sender As Object, ByVal e As System.EventArgs)

        Call DisplayData(1)

    End Sub

    Sub SelectMode(ByVal sender As Object, ByVal e As System.EventArgs)
        If ddlAcquisitionMode.SelectedIndex = 0 Then
            trAsset.Visible = True
            trTransfer.Visible = False
        Else
            trAsset.Visible = False
            trTransfer.Visible = True
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCOUNT_DETAILS&errmesg=" & "" & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType"))
        End If
    End Sub

    Sub BindDeprGLDeprBlkDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=" & strOpCdBlockList_Get & "&errmesg=" & lblSaveErr.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            ddlDeprGLDeprBlkCode.Visible = True
            dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Pilih Cost Center"

        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)
        ddlDeprGLDeprBlkCode.DataSource = dsForDropDown.Tables(0)
        ddlDeprGLDeprBlkCode.DataValueField = "BlkCode"
        ddlDeprGLDeprBlkCode.DataTextField = "Description"
        ddlDeprGLDeprBlkCode.DataBind()
        ddlDeprGLDeprBlkCode.SelectedIndex = intSelectedIndex

        If pv_strBlkCode = "" And dsForDropDown.Tables(0).Rows.Count > 0 Then
            ddlDeprGLDeprBlkCode.SelectedIndex = 0
        End If

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub AddData()

        Dim strDate As String = CheckDate()
        Dim TxID As String
        Dim intError As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim objRslSet As DataSet


        Dim strDeprMethodF As String
        Dim dblLifetimeF As String
        Dim strStartMonthF As String
        Dim strStartYearF As String
        Dim dblAssetValueF As String
        Dim dblAccumDeprValueF As String
        Dim dblFinalValueF As String
        Dim dblBegYearValueF As String
        Dim intDeprIndF As Integer
        Dim intSame As Integer

        strOpCode = "FA_CLSSETUP_ASSETREGLN_ADD"

        strParamName = "ASSETCODE|LOCCODE|" & _
                       "ASSETHEADERCODE|SERIALNO|" & _
                       "DESCRIPTION|EXTDESCRIPTION|" & _
                       "ACQUISITIONMODE|PURCHASEDATE|" & _
                       "DEPRMETHOD|DEPRMETHODF|FISKALCATEGORY|" & _
                       "LIFETIME|LIFETIMEF|" & _
                       "STARTMONTH|STARTMONTHF|" & _
                       "STARTYEAR|STARTYEARF|" & _
                       "ASSETVALUE|ASSETVALUEF|" & _
                       "ACCUMDEPRVALUE|ACCUMDEPRVALUEF|" & _
                       "FINALVALUE|FINALVALUEF|" & _
                       "BEGYEARVALUE|BEGYEARVALUEF|ISSAME|" & _
                       "UPDATEID|DEPRGLDEPRACCCODE|" & _
                       "DEPRGLDEPRBLKCODE|DEPRGLACCUMDEPRACCCODE|" & _
                       "ASSETACCCODE|ASSETCONDITION|" & _
                       "QTY|UOM|DEPTCODE|DEPRIND|DEPRINDF|STOCKISSUELNID|NO|ASSETTRANID|" & _
                       "TRXTYPE|KATEGORI|ASSETACCCODELEASE|DEPRGLACCUMDEPRACCCODELEASE"


        intDeprInd = IIf(cbDeprInd.Checked = True, objFASetup.EnumDeprInd.Yes, objFASetup.EnumDeprInd.No)

        If cbFiskalSame.checked = True Then
            strDeprMethodF = ddlDeprMethod.SelectedItem.Value
            dblLifetimeF = txtLifeTime.Text
            strStartMonthF = txtStartMonth.Text
            strStartYearF = txtStartYear.Text
            dblAssetValueF = txtAssetValue.Text
            dblAccumDeprValueF = txtAccumDeprValue.Text
            dblFinalValueF = txtFinalValue.Text
            dblBegYearValueF = txtBegYearValue.Text
            intDeprIndF = intDeprInd
            intSame = 1
        Else
            strDeprMethodF = ddlDeprMethodF.SelectedItem.Value
            dblLifetimeF = txtLifeTimeF.Text
            strStartMonthF = txtStartMonthF.Text
            strStartYearF = txtStartYearF.Text
            dblAssetValueF = txtAssetValueF.Text
            dblAccumDeprValueF = txtAccumDeprValueF.Text
            dblFinalValueF = txtFinalValueF.Text
            dblBegYearValueF = txtBegYearValueF.Text
            intDeprIndF = IIf(cbDeprIndF.Checked = True, objFASetup.EnumDeprInd.Yes, objFASetup.EnumDeprInd.No)
            intSame = 0
        End If

        strParamValue = txtAssetCode.Text & "|" & strLocation & "|" & _
                    ddlAssetHeaderCode.SelectedItem.Value & "|" & txtSerialNo.Text & "|" & _
                    txtDescription.Text & "|" & txtExtDescription.Value & "|" & _
                    ddlAcquisitionMode.SelectedItem.Value & "|" & strDate & "|" & _
                    ddlDeprMethod.SelectedItem.Value & "|" & strDeprMethodF & "|" & ddlFiskalCategory.SelectedItem.Value & "|" & _
                    txtLifeTime.Text & "|" & dblLifetimeF & "|" & _
                    txtStartMonth.Text & "|" & strStartMonthF & "|" & _
                    txtStartYear.Text & "|" & strStartYearF & "|" & _
                    txtAssetValue.Text & "|" & dblAssetValueF & "|" & _
                    txtAccumDeprValue.Text & "|" & dblAccumDeprValueF & "|" & _
                    txtFinalValue.Text & "|" & dblFinalValueF & "|" & _
                    txtBegYearValue.Text & "|" & dblBegYearValueF & "|" & intSame & "|" & _
                    strUserId & "|" & ddlDeprGLDeprAccCode.SelectedItem.Value & "|" & _
                    ddlDeprGLDeprBlkCode.SelectedItem.Value & "|" & ddlDeprGLAccumDeprAccCode.SelectedItem.Value & "|" & _
                    ddlGLAssetAccCode.SelectedItem.Value & "|" & ddlAssetCondition.SelectedItem.Value & "|" & _
                    txtQty.Text & "|" & ddlUOM.SelectedItem.Value & "|" & _
                    ddlDeptCode.SelectedItem.Value & "|" & intDeprInd & "|" & intDeprIndF & "|" & ddlAsset.SelectedItem.Value & _
                    "|" & ddlNumber.SelectedItem.Value & "|" & ddlTransfer.SelectedItem.Value & "|" & _
                    ddlTrxType.SelectedItem.Value & "|" & ddlKategori.SelectedItem.Value & "|" & _
                    ddlGLAssetAccCodeLease.SelectedItem.Value & "|" & ddlDeprGLAccumDeprAccCodeLease.SelectedItem.Value

        Try
            intErrNo = objFATrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objRslSet)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETREGLNDETAILS_UPDATE&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        End Try

        If objRslSet.Tables(0).Rows(0).Item("Msg") = "OK" Then
            txtAssetCode.Text = objRslSet.Tables(0).Rows(0).Item("ID")
            Call DisplayData()
        Else
            lblSaveErr.Text = objRslSet.Tables(0).Rows(0).Item("Msg")
            lblSaveErr.visible = "True"
        End If


    End Sub

    Sub ChekFiscalSame(ByVal sender As Object, ByVal e As System.EventArgs)

        If sender.Checked = True Then
            tblFiscal.Visible = False
        Else
            tblFiscal.Visible = True
        End If

    End Sub

    Sub BindDispGLAssetAccCodeLease(ByVal pv_strAccCode As String)
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

        If Session("SS_COACENTRALIZED") = "1" Then
            strParam = "Order By ACC.AccCode|And ACC.AccPurpose = " & objGLSetup.EnumAccountPurpose.NonVehicle & _
                                " And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "' AND Acc.AccType = '1'"

        Else
            strParam = "Order By ACC.AccCode|And ACC.AccPurpose = " & objGLSetup.EnumAccountPurpose.NonVehicle & _
                                " And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "' AND Acc.LocCode = '" & strLocation & "' AND Acc.AccType = '1'"

        End If

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_DISPGLASSETACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Pilih Akun "
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlGLAssetAccCodeLease.DataSource = dsForDropDown.Tables(0)
        ddlGLAssetAccCodeLease.DataValueField = "AccCode"
        ddlGLAssetAccCodeLease.DataTextField = "_Description"
        ddlGLAssetAccCodeLease.DataBind()
        ddlGLAssetAccCodeLease.SelectedIndex = intSelectedIndex

        'If intSelectedIndex = 0 And Not pv_strAccCode = "" Then
        '    strParam = "Order By ACC.AccCode|And ACC.AccCode = '" & _
        '                pv_strAccCode & "'"

        '    Try
        '        intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForInactiveItem)
        '        If dsForInactiveItem.Tables(0).Rows.Count > 0 Then
        '            ddlGLAssetAccCodeLease.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode")) & _
        '             " (" & objGLSetup.mtdGetAccStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode"))))
        '            intSelectedIndex = ddlGLAssetAccCodeLease.Items.Count - 1
        '        Else
        '            intSelectedIndex = 0
        '        End If

        '    Catch Exp As System.Exception
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_DISPGLASSETACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        '    End Try
        'End If
    End Sub

    Sub BindDeprGLAccumDeprAccCodeLease(ByVal pv_strAccCode As String)
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

        If Session("SS_COACENTRALIZED") = "1" Then
            strParam = "Order By ACC.AccCode|And ACC.AccPurpose = " & objGLSetup.EnumAccountPurpose.NonVehicle & _
                                " And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "' AND Acc.AccType = '1'"

        Else
            strParam = "Order By ACC.AccCode|And ACC.AccPurpose = " & objGLSetup.EnumAccountPurpose.NonVehicle & _
                                " And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "' AND Acc.LocCode = '" & strLocation & "' AND Acc.AccType = '1'"

        End If

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_DISPGLASSETACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Pilih Akun "
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlDeprGLAccumDeprAccCodeLease.DataSource = dsForDropDown.Tables(0)
        ddlDeprGLAccumDeprAccCodeLease.DataValueField = "AccCode"
        ddlDeprGLAccumDeprAccCodeLease.DataTextField = "_Description"
        ddlDeprGLAccumDeprAccCodeLease.DataBind()
        ddlDeprGLAccumDeprAccCodeLease.SelectedIndex = intSelectedIndex

        'If intSelectedIndex = 0 And Not pv_strAccCode = "" Then
        '    strParam = "Order By ACC.AccCode|And ACC.AccCode = '" & _
        '                pv_strAccCode & "'"

        '    Try
        '        intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForInactiveItem)
        '        If dsForInactiveItem.Tables(0).Rows.Count > 0 Then
        '            ddlDeprGLAccumDeprAccCodeLease.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode")) & _
        '             " (" & objGLSetup.mtdGetAccStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode"))))
        '            intSelectedIndex = ddlDeprGLAccumDeprAccCodeLease.Items.Count - 1
        '        Else
        '            intSelectedIndex = 0
        '        End If

        '    Catch Exp As System.Exception
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_DISPGLASSETACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=FA/setup/FA_setup_AssetReglnDetails.aspx")
        '    End Try
        'End If
    End Sub


    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("FA_setup_AssetReglnDetails.aspx")
    End Sub

End Class
