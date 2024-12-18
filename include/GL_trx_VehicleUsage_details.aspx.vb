
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class GL_VehUse_Det : Inherits Page

    Protected WithEvents dgStkTx As DataGrid
    Protected WithEvents lblTxID As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblVehUsageID As Label
    Protected WithEvents lblVehLnId As Label
    Protected WithEvents lblVehTag As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblUsageUnit As Label
    Protected WithEvents lblReprint As Label
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents lstVehCode As DropDownList
    Protected WithEvents lstPreBlock As DropDownList
    Protected WithEvents lstChargeLevel As DropDownList
    Protected WithEvents lblRunErr As Label
    Protected WithEvents Label1 As Label

    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtAccName As TextBox
    Protected WithEvents lblChargeMsg As Label

    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblTotAmtFig As Label
    Protected WithEvents lblPreBlkTag As Label
    Protected WithEvents txtUnit As TextBox
    Protected WithEvents txtDate As TextBox
    Protected WithEvents tblAdd As HtmlTable
    Protected WithEvents lblError As Label
    Protected WithEvents lblStock As Label
    Protected WithEvents lblVehCodeErr As Label
    Protected WithEvents Add As ImageButton
    Protected WithEvents btnUpdate As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Print As ImageButton
    Protected WithEvents Cancel As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents Back As ImageButton
    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents validateAccCode As RequiredFieldValidator
    Protected WithEvents validateBlock As RequiredFieldValidator
    Protected WithEvents validateUnit As RequiredFieldValidator
    Protected WithEvents lblID As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblPleaseSpecify As Label
    Protected WithEvents lblTotalAmt As Label
    Protected WithEvents lblPreBlockErr As Label
    Protected WithEvents RowChargeTo As HtmlTableRow
    Protected WithEvents ddlType As DropDownList

    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlk As HtmlTableRow
    Protected WithEvents ddlPreBlock As DropDownList
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Dim PreBlockTag As String

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbViewJournal As LinkButton
    Protected WithEvents dgViewJournal As DataGrid
    Protected WithEvents lblTotalDB As Label
    Protected WithEvents lblTotalCR As Label
    Protected WithEvents lblTotalViewJournal As Label

    Protected WithEvents txtAddNote As HtmlTextArea
    Protected WithEvents hidVehActCode As HtmlInputHidden
    Protected WithEvents hidAccCode As HtmlInputHidden
    Protected WithEvents txtRunFrom As TextBox
    Protected WithEvents txtRunTo As TextBox

    Protected objGLtx As New agri.GL.clstrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objGLtrx As New agri.GL.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objINTrx As New agri.IN.clsTrx()
    Protected objGLSetup As New agri.GL.clsSetup()
    Dim objOk As New agri.GL.ClsTrx()

    Dim strOpCdStckTxDet_ADD As String = "GL_CLSTRX_VEHICLEUSAGE_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "GL_CLSTRX_VEHICLEUSAGE_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "GL_CLSTRX_VEHICLEUSAGE_LINE_GET"
    Dim strOpCdAccCode_GET As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"

    Dim objDataSet As New DataSet()
    Dim dsGrid As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim AccountTag As String
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strDateFMT As String
    Dim intGLAR As Integer
    Dim intConfigsetting As Integer
    Dim strVehTag As String
    Dim strAccCodeTag As String
    Dim strBlkTag As String
    Dim strUsageUnit As String

    Protected WithEvents lblLocCodeErr As Label
	
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer
	Dim strLocType as String
	
	Dim strParamName As String
    Dim strParamValue As String
    Dim objPODs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strDateFMT = Session("SS_DATEFMT")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleUsage), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            Add.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Add).ToString())
            btnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnNew).ToString())
            Save.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Save).ToString())
            Cancel.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Cancel).ToString())
            Print.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Print).ToString())
            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())

            If Not Page.IsPostBack Then
                dgStkTx.Columns(1).Visible = Session("SS_INTER_ESTATE_CHARGING")
                lblTxID.Text = Request.QueryString("Id")
                If Not Request.QueryString("Id") = "" Then
                    LoadTxDetails()            
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        DisplayFromDB()
                        BindGrid()
                    End If

                End If

                txtAccCode.Attributes.Add("readonly", "readonly")
                txtDate.Text = objGlobal.GetShortDate(strDateFMT, Now())
                'BindAccCodeDropList()
                BindChargeLevelDropDownList()
                'BindAccCode2List()
                BindVehicleCodeDropList()
                PageControl()
                txtRunFrom.Text = GetMaxHM()
            End If

            lblError.Visible = False
            lblDate.Visible = False
            lblFmt.Visible = False
            lblVehCodeErr.Visible = False
            lblLocCodeErr.Visible = False
			'list acccode di disable biar ga diganti2
            'lstAccCode2.Enabled = False
        End If
    End Sub

    Sub ddlType_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        'BindAccCodeDropList()
        'lstAccCode2.Items.Clear()

    End Sub

    Sub lstVehCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        txtRunFrom.Text = GetMaxHM()
    End Sub

    Sub BindChargeLevelDropDownList()
        lstChargeLevel.Items.Add("Please Select Charge Level")
        lstChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        lstChargeLevel.Items.Add(New ListItem(strBlkTag, objLangCap.EnumLangCap.SubBlock))
        lstChargeLevel.SelectedIndex = 0 ''Session("SS_BLOCK_CHARGE_DEFAULT")
        'ToggleChargeLevel()
    End Sub

    Sub lstChargeLevel_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ToggleChargeLevel()
        CheckVehicleUse()
    End Sub

    Sub ToggleChargeLevel()
        If lstChargeLevel.SelectedIndex = 0 Then
            '  RowPreBlk.Visible = True
            hidBlockCharge.Value = "yes"
        Else
            '   RowPreBlk.Visible = False
            hidBlockCharge.Value = ""
        End If
    End Sub

    Sub PageControl()
        If Status.Text = objGLtx.mtdGetVehicleUsageStatus(objGLtx.EnumVehicleUsageStatus.Cancelled) Then

            Save.Visible = False
            btnNew.Visible = True
            Select Case Status.Text
                Case objGLtx.mtdGetVehicleUsageStatus(objGLtx.EnumVehicleUsageStatus.Cancelled)
                    Cancel.Visible = False
            End Select
        Else
            Save.Visible = True
            btnNew.Visible = False
            Print.Visible = True
            If lblTxID.Text <> "" Then
                btnNew.Visible = True
                Cancel.Visible = True
            Else
                btnNew.Visible = False
                Print.Visible = False
            End If

        End If
        If lblTxID.Text <> "" Then
            lstVehCode.Enabled = False
        End If
        DisableItemTable()
    End Sub

    Sub DisableItemTable()

        If Status.Text = objGLtx.mtdGetVehicleUsageStatus(objGLtx.EnumVehicleUsageStatus.Cancelled) Then
            tblAdd.Visible = False
        ElseIf Status.Text = objGLtx.mtdGetVehicleUsageStatus(objGLtx.EnumVehicleUsageStatus.Active) Then
            tblAdd.Visible = True
        End If

    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)

        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim DeleteButton As LinkButton
                Select Case Status.Text.Trim
                    Case objGLtrx.mtdGetVehicleUsageStatus(objGLtrx.EnumVehicleUsageStatus.Active)
                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = True
                        DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objGLtrx.mtdGetVehicleUsageStatus(objGLtrx.EnumVehicleUsageStatus.Cancelled)
                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = False
                End Select
        End Select

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_VEHICLEUSAGE_DETAILS_GET_COSTLEVEL_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/vehicleusage_details.aspx")
        End Try

        AccountTag = GetCaption(objLangCap.EnumLangCap.Account)
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strAccCodeTag = GetCaption(objLangCap.EnumLangCap.Account)
        strUsageUnit = GetCaption(objLangCap.EnumLangCap.VehUsageUnit)

        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.VehUsage))
        lblVehUsageID.Text = GetCaption(objLangCap.EnumLangCap.VehUsage) & lblID.Text
        lblVehTag.Text = strVehTag & lblCode.Text
        lblAccCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Activity) & " " & GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text 'strAccCodeTag & lblCode.Text
        lblUsageUnit.Text = strUsageUnit
        lblTotalAmt.Text = "Total " & strUsageUnit

        lblVehCodeErr.Text = lblPleaseSelect.Text & strVehTag & lblCode.Text
        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTag.Text = strBlkTag & lblCode.Text & " : "
        'validateAccCode.ErrorMessage = "<BR>" & lblPleaseSelect.Text & "Aktivity " & lblCode.Text
        validateUnit.ErrorMessage = "<BR>" & lblPleaseSpecify.Text & strUsageUnit
        'dgStkTx.Columns(2).HeaderText = GetCaption(objLangCap.EnumLangCap.Activity) & " " & GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text 'strAccCodeTag & lblCode.Text
        'dgStkTx.Columns(3).HeaderText = strAccCodeTag & lblCode.Text
        '  dgStkTx.Columns(6).HeaderText = strUsageUnit
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_VEHUSAGEDET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/vehicleusage_details.aspx")
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

    Sub DisplayFromDB()
        Status.Text = objGLtx.mtdGetVehicleUsageStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
        lblAccPeriod.Text = objDataSet.Tables(0).Rows(0).Item("AccMonth") & "/" & objDataSet.Tables(0).Rows(0).Item("AccYear")
        CreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("UserName"))
        lblTotAmtFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(objDataSet.Tables(0).Rows(0).Item("TotalAmount")), 5)
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer

        dgStkTx.DataSource = LoadDataGrid()
        dgStkTx.DataBind()
    End Sub

    Protected Function LoadDataGrid() As DataSet

        Dim strParam As String
        strParam = Trim(lblTxID.Text)
        
        Try
            intErrNo = objGLtx.mtdGetGLTxDetails(strOpCdStckTxLine_GET, strParam, dsGrid)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_USAHELN&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
        End Try

        Return dsGrid
    End Function

    Sub LoadTxDetails()

        Dim strOpCdStckTxDet_GET As String = "GL_CLSTRX_VEHICLEUSAGE_DETAIL_GET"
        Dim StockCode As String

        strParam = Trim(lblTxID.Text)
       
        Try
            intErrNo = objGLtx.mtdGetGLTxDetails(strOpCdStckTxDet_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_USAGEDETAILS&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
        End Try

    End Sub

    Function GetMaxHM() As Double
        Dim strParam As String
        Dim strParamValue As String
        Dim objItemDs As New DataSet
        Dim nMaxHm As Double = 0
        Dim strOppCd_GET As String = "GL_CLSTRX_VEHICLEUSAGE_MAXHM_GET"

        strParam = "VEHCODE|LOCCODE"
        strParamValue = Trim(lstVehCode.Text) & "|" & strLocation

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOppCd_GET, _
                                    strParam, _
                                    strParamValue, _
                                    objItemDs)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_GET_VehActCode&errmesg=" & Exp.ToString() & "&redirect=GL/Setup/GL_Setup_VehicleSubGrpCode.aspx")
        End Try
        If objItemDs.Tables(0).Rows.Count > 0 Then
            nMaxHm = Trim(objItemDs.Tables(0).Rows(0).Item("RunHourTo"))
        Else
            nMaxHm = 0
        End If

        Return nMaxHm
    End Function

    Function GetHMListUseByDate(ByVal pmachine As String, ByVal pLocCode As String, ByVal pDate As Date, ByVal pRHFrom As Double) As Double
        Dim strParam As String
        Dim strParamValue As String
        Dim objItemDs As New DataSet
        Dim nHmUse As Double = 0
        Dim strOppCd_GET As String = "GL_CLSTRX_VEHICLEUSAGE_HMLISTUSE_GET"

        strParam = "VEHCODE|LOCCODE|TDATE|RHF"
        strParamValue = pmachine & "|" & strLocation & "|" & pDate & "|" & pRHFrom

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOppCd_GET, _
                                    strParam, _
                                    strParamValue, _
                                    objItemDs)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_GET_VehActCode&errmesg=" & Exp.ToString() & "&redirect=GL/Setup/GL_Setup_VehicleSubGrpCode.aspx")
        End Try
        If objItemDs.Tables(0).Rows.Count > 0 Then
            nHmUse = Trim(objItemDs.Tables(0).Rows(0).Item("RunHour"))
        Else
            nHmUse = 0
        End If

        Return nHmUse
    End Function

    Sub CallCheckVehicleUse(ByVal sender As Object, ByVal e As System.EventArgs)
        CheckVehicleUse()
    End Sub

    Sub CheckVehicleUse()

        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strBlkVal As String
        Dim strAcc As String = Request.Form("txtAccCode")
        Dim strPreBlk As String = Request.Form("lstPreBlock")
        Dim strBlk As String = Request.Form("lstBlock")

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd)

        If lstChargeLevel.SelectedIndex = 0 Then
            strBlkVal = lstPreBlock.SelectedItem.Value
        End If

        If intAccType = objGLset.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLset.EnumAccountPurpose.NonVehicle
                    BindPreBlock(strAcc, strPreBlk)
                Case objGLset.EnumAccountPurpose.VehicleDistribution
                    If strBlkVal = "" Then
                        BindPreBlock("", "")
                    End If
                    BindVehicleCodeDropList()
                Case objGLset.EnumAccountPurpose.Others
                    If strBlkVal = "" Then
                        BindPreBlock(strAcc, strPreBlk)
                    End If
            End Select
        ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet Or intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
            'If strBlkVal = "" Then
            '    BindPreBlock(strAcc, strPreBlk)
            'End If
			
			BindPreBlockBalanceSheet(strAcc, strPreBlk)
            'BindBlockBalanceSheetDropList(strAcc, strPreBlk)
        Else
            BindPreBlock("", "")
        End If
    End Sub

    Sub BindPreBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim objBlkDs As DataSet
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        strOpCd = "GL_CLSSETUP_ACCOUNT_PREBLOCK_UNION_BLOCK__GET"
        intSelectedIndex = 0

        Try

            strParam = pv_strAccCode & "|" & strLocation & "|" & objGLset.EnumBlockStatus.Active
            intErrNo = objGLset.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If objBlkDs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & PreBlockTag & lblCode.Text

        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)
        lstPreBlock.DataSource = objBlkDs.Tables(0)
        lstPreBlock.DataValueField = "BlkCode"
        lstPreBlock.DataTextField = "Description"
        lstPreBlock.DataBind()
        lstPreBlock.SelectedIndex = intSelectedIndex

        If Not objBlkDs Is Nothing Then
            objBlkDs = Nothing
        End If
    End Sub

	Sub BindPreBlockBalanceSheet(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0


        strOpCd = "GL_CLSSETUP_ACCOUNT_PREBLOCK_UNION_BLOCK_BALANCESHEET_GET"

        strParamName = "ACCCODE|LOCCODE|STATUS"
        strParamValue = pv_strAccCode & "|" & strLocation & "|" & objGLset.EnumBlockStatus.Active
        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                            strParamName, _
                                            strParamValue, _
                                            objPODs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objPODs.Tables(0).Rows(intCnt).Item("Description") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objPODs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If objPODs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objPODs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & PreBlockTag & lblCode.Text

        objPODs.Tables(0).Rows.InsertAt(dr, 0)
        lstPreBlock.DataSource = objPODs.Tables(0)
        lstPreBlock.DataValueField = "BlkCode"
        lstPreBlock.DataTextField = "Description"
        lstPreBlock.DataBind()
        lstPreBlock.SelectedIndex = intSelectedIndex

        If Not objPODs Is Nothing Then
            objPODs = Nothing
        End If
    End Sub

    Sub BindBlockBalanceSheetDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim dr As DataRow

        Try

            strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_BALANCE_SHEET_GET"

            strParamName = "ACCCODE|LOCCODE|STATUS"
            strParamValue = pv_strAccCode & "|" & strLocation & "|" & objGLset.EnumBlockStatus.Active

            intErrNo = objGLtrx.mtdGetDataCommon(strOpCdBlockList_Get, _
                                            strParamName, _
                                            strParamValue, _
                                            objPODs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objPODs.Tables(0).Rows(intCnt).Item("Description") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objPODs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If objPODs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objPODs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & PreBlockTag & lblCode.Text
        objPODs.Tables(0).Rows.InsertAt(dr, 0)

        lstPreBlock.DataSource = objPODs.Tables(0)
        lstPreBlock.DataValueField = "BlkCode"
        lstPreBlock.DataTextField = "Description"
        lstPreBlock.DataBind()
        lstPreBlock.SelectedIndex = intSelectedIndex

        If Not objPODs Is Nothing Then
            objPODs = Nothing
        End If
    End Sub
	
    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If Status.Text = objGLtx.mtdGetVehicleUsageStatus(objGLtx.EnumVehicleUsageStatus.Active) Then

            Dim strOpCdStckTxLine_DEL As String = "GL_CLSTRX_VEHICELUSAGE_LINE_DEL"
            Dim lbl As Label
            Dim id As String
            Dim ItemCode As String
            Dim strParam As String
            Dim StrTxParam As String
            Dim TxID As String
            Dim ErrorChk As Integer = objGLtx.EnumGeneralLedgerTxErrorType.NoError
            Dim strDate As String = Date_Validation(txtDate.Text, False)
            Dim indDate As String = ""

            If txtDate.Text = "" Then
                lblDate.Visible = True
                lblFmt.Visible = True
                lblDate.Text = "<br>Date Entered should be in the format"
                Exit Sub
            End If
            If CheckDate(txtDate.Text.Trim(), indDate) = False Then
                lblDate.Visible = True
                lblFmt.Visible = True
                lblDate.Text = "<br>Date Entered should be in the format"
                Exit Sub
            End If

            Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
            Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
            Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

            If Session("SS_FILTERPERIOD") = "0" Then
                If intCurPeriod < intInputPeriod Then
                    lblDate.Visible = True
                    lblDate.Text = "Invalid transaction date."
                    Exit Sub
                End If
            Else
                If intSelPeriod <> intInputPeriod Then
                    lblDate.Visible = True
                    lblDate.Text = "Invalid transaction date."
                    Exit Sub
                End If
                If intSelPeriod < intCurPeriod And intLevel < 2 Then
                    lblDate.Visible = True
                    lblDate.Text = "This period already locked."
                    Exit Sub
                End If
            End If

            lbl = E.Item.FindControl("lblID")
            id = lbl.Text

            strParam = id & "|" & lblTxID.Text
            Try
                intErrNo = objGLtx.mtdDelTransactLn(strOpCdStckTxLine_DEL, strParam)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_USAGELINE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
                End If
            End Try

            StrTxParam = lblTxID.Text & "|||||||"

            Try
                intErrNo = objGLtx.mtdUpdVehicleUsageDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strUserId, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.VehicleUsage), _
                                                            ErrorChk, _
                                                            TxID)

                If ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.Overflow Then
                    lblError.Visible = True
                End If

                lblTxID.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWVEHICLEUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
                End If
            End Try
            LoadTxDetails()
            DisplayFromDB()
            BindGrid()
        End If
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label
        Dim Delbutton As LinkButton
        Dim strAcc As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        Dim strPreBlk As String = Request.Form("lstPreBlock")

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        ''edit confirmed
        Delbutton = E.Item.FindControl("Edit")
        Delbutton.Visible = False
        Delbutton = E.Item.FindControl("Delete")
        Delbutton.Visible = False
        Delbutton = E.Item.FindControl("Cancel")
        Delbutton.Visible = True

        dgStkTx.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblTransDate")
        txtDate.Text = Format(CDate(lbl.Text), "dd/MM/yyyy")

        lstChargeLevel.Items.Clear()
        BindChargeLevelDropDownList()

        lstChargeLevel.SelectedIndex = 1

        lbl = E.Item.FindControl("lblAccCode")
        strAcc = lbl.Text.Trim
        txtAccCode.Text = strAcc

        lbl = E.Item.FindControl("lblAccDesc")
        txtAccName.Text = RTrim(lbl.Text)

        lbl = E.Item.FindControl("lblBlock")
        strPreBlk = lbl.Text.Trim

        'GetAccCodeDropList(strAcc)


        BindPreBlock(strAcc, strPreBlk)

        lbl = E.Item.FindControl("RunHourFrom")
        txtRunFrom.Text = Trim(Replace(Replace(Replace(lbl.Text, ".", ""), ",", "."), "-", ""))

        lbl = E.Item.FindControl("RunHourTo")
        txtRunTo.Text = Trim(Replace(Replace(Replace(lbl.Text, ".", ""), ",", "."), "-", ""))

        lbl = E.Item.FindControl("UsageUnit")
        txtUnit.Text = Trim(Replace(Replace(Replace(lbl.Text, ".", ""), ",", "."), "-", ""))

        lbl = E.Item.FindControl("lblAddNote")
        txtAddNote.Value = lbl.Text.Trim

        lbl = E.Item.FindControl("LNID")
        lblVehLnId.Text = lbl.Text.Trim

        If lblTxID.Text <> "" Then
            Add.Visible = False
            btnUpdate.Visible = True
        Else
            Add.Visible = True
            btnUpdate.Visible = False
        End If

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgStkTx.EditItemIndex = -1
        DisableItemTable()
        LoadTxDetails()
        DisplayFromDB()
        BindGrid()
        PageControl()

        txtAccCode.Text = ""
        txtAccName.Text = ""
        lstPreBlock.SelectedItem.Value = ""
        lstPreBlock.SelectedIndex = 0
        lstChargeLevel.SelectedItem.Value = ""
        txtAddNote.Value = ""
        txtRunFrom.Text = ""
        txtRunTo.Text = ""
        txtUnit.Text = ""

        lstChargeLevel.Enabled = True
        Add.Visible = True
        btnUpdate.Visible = False
    End Sub

    Sub BindVehicleCodeDropList()

        Dim strOpCdVehCode_Get As String = "GL_CLSTRX_VEHICLE_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim dsForInactiveItem As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As New StringBuilder("")

        Try
            strParam.Append("|")
            strParam.Append(objGLset.EnumVehicleStatus.Active)
            strParam.Append("| AND GL.LocCode = '" & Session("SS_LOCATION") & "' AND VT.IsPowerSupply = 0")
            strParam.Append("|||VehCode|ASC")

            intErrNo = objGLtrx.mtdGetGLMasterList(strOpCdVehCode_Get, strParam.ToString, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If Not lblTxID.Text = "" Then
                If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("VehCode"))) = UCase(Trim(objDataSet.Tables(0).Rows(0).Item("VehCode"))) Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt
        Dim drinsert As DataRow
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = " "
        drinsert(1) = lblSelect.Text & strVehTag
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstVehCode.DataSource = dsForDropDown.Tables(0)
        lstVehCode.DataValueField = "VehCode"
        lstVehCode.DataTextField = "Description"
        lstVehCode.DataBind()

        If SelectedIndex = -1 And Not lblTxID.Text = "" Then

            Try
                strParam.Remove(0, strParam.Length)
                strParam.Append("|| AND VehCode = '")
                strParam.Append(Trim(objDataSet.Tables(0).Rows(0).Item("VehCode")))
                strParam.Append("'|||VehCode|ASC")

                intErrNo = objGLtrx.mtdGetGLMasterList(strOpCdVehCode_Get, strParam.ToString, dsForInactiveItem)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMNOTFOUND&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
            End Try

            If dsForInactiveItem.Tables(0).Rows.Count > 0 Then
                lstVehCode.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0)) & _
                " (" & Trim(dsForInactiveItem.Tables(0).Rows(0).Item(1)) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0))))
            Else
                lstVehCode.Items.Add(New ListItem(Trim(objDataSet.Tables(0).Rows(0).Item("VehCode")) & _
                    " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Unknown) & ") ", Trim(objDataSet.Tables(0).Rows(0).Item("VehCode"))))
            End If
            SelectedIndex = lstVehCode.Items.Count - 1
        End If

        lstVehCode.SelectedIndex = SelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
        If Not dsForInactiveItem Is Nothing Then
            dsForInactiveItem = Nothing
        End If
    End Sub

    Function CheckDate(ByVal pv_strDate As String, ByRef pr_strDate As String) As Boolean
        Dim strDateFormatCode As String = Session("SS_DATEFMT")
        Dim strDateFormat As String

        pr_strDate = ""
        CheckDate = True
        If Not pv_strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormatCode, pv_strDate, strDateFormat, pr_strDate) = False Then
                lblFmt.Text = strDateFormat
                pr_strDate = ""
                CheckDate = False
            End If
        End If
    End Function

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

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer, _
                          ByRef pr_strNurseryInd As Integer)

        Dim _objAccDs As New DataSet()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLset.mtdGetAccount(strOpCd, _
                                              strParam, _
                                              _objAccDs, _
                                              True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_VEHICLEUSG_GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
        Else
            pr_strAccType = 0
            pr_strAccPurpose = 0
            pr_strNurseryInd = 0
        End If
    End Sub

    Function CheckRequiredField() As Boolean
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strAcc As String = Request.Form("lstAccCode2")
        Dim strBlk As String = lstPreBlock.SelectedValue
        Dim strVeh As String = Request.Form("lstVehCode")
        Dim strVehExp As String = Request.Form("lstVehExp")

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd)

        If intAccType = objGLset.EnumAccountType.ProfitAndLost And strBlk = "" Then
            lblPreBlockErr.Visible = True
            Return True
        ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And strBlk = "" Then
            lblPreBlockErr.Visible = False
            Return False
        Else
            Return False
        End If

    End Function

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strOpCdStckTxLine_ADD As String = "GL_CLSTRX_VEHICLEUSAGE_LINE_ADD"
        Dim strStatus As String
        Dim TxID As String
        Dim IssType As String
        Dim strAmount As String
        Dim StrTxParam As New StringBuilder()
        Dim strTxLnParam As New StringBuilder()
        Dim ParamNama As String
        Dim ParamValue As String
        Dim ErrorChk As Integer = objGLtx.EnumGeneralLedgerTxErrorType.NoError
        Dim strAllowVehicle As String
        Dim arrAccCode As Array
        Dim nValHMLast As Double
        Dim nValRHUseFrom As Double
        Dim sTrAccCode = Trim(txtAccCode.Text)

        '    arrAccCode = Split(lstAccCode.SelectedItem.Text.Trim, ", ")
        Dim strBlkCode As String = lstPreBlock.SelectedValue
        Dim strNewIDFormat As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If lstChargeLevel.SelectedIndex = 0 Then
            lblChargeMsg.Visible = True
            lstChargeLevel.Focus()
            Exit Sub
        Else
            lblChargeMsg.Visible = False
        End If

        If CheckRequiredField() Then
            Exit Sub
        End If


        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        strNewIDFormat = "VHU" & "/" & strCompany & "/" & strLocation & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        ''===NILAI RUNNING HOUR TO HARUS LEBIH BESAR DARI RUNNING HOUR FROM
        If CDbl(txtRunFrom.Text) > CDbl(txtRunTo.Text) Then
            lblRunErr.Text = "Data Running Hour From Lebih Besar Dari Running Hour To..!"
            lblRunErr.Visible = True
            Exit Sub
        Else
            lblRunErr.Visible = False
            lblRunErr.Text = ""
        End If

        'If strBlkCode = vbNullString Then
        '    lblPreBlockErr.Visible = True
        '    Exit Sub
        'Else
        '    lblPreBlockErr.Visible = False
        'End If

        ''==CEK NILAI RUNNING HOUR YANG LEBIH BESAR DARI TANGGAL INPUT
        'nValRHUseFrom = 0
        'nValRHUseFrom = GetHMListUseByDate(lstVehCode.SelectedValue, strLocation, Format(CDate(strDate), "yyyy-MM-dd"), CDbl(txtRunFrom.Text))

        'If nValRHUseFrom > 0 Then
        '    lblRunErr.Text = "Cek Kembali Tanggal Input, Running Hour Tidak Sesuai..!"
        '    lblRunErr.Visible = True
        '    Exit Sub
        'Else
        '    lblRunErr.Visible = False
        '    lblRunErr.Text = ""
        'End If

        ''===CEK NILAI HM TERKAHIR
        'nValHMLast = GetMaxHM()
        'If CDbl(txtRunFrom.Text) <> nValHMLast Then
        '    lblRunErr.Text = "Running Hour From Tidak Sesuai Dengan Data Terakhir..!"
        '    lblRunErr.Visible = True
        '    Exit Sub
        'Else
        '    lblRunErr.Visible = False
        '    lblRunErr.Text = ""
        'End If

        If lblTxID.Text = "" Then
            StrTxParam.Append(lblTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(lstVehCode.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|0||")
            StrTxParam.Append(strNewIDFormat)

            Try
                intErrNo = objGLtx.mtdUpdVehicleUsageDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strUserId, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.VehicleUsage), _
                                                            ErrorChk, _
                                                            TxID)
                lblTxID.Text = TxID
                If ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.Overflow Then
                    lblError.Visible = True
                End If

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWVEHICLEUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
                End If
            End Try
        End If

        If sTrAccCode = vbNullString Then
            txtAddNote.Visible = True
            txtAddNote.Value = sTrAccCode
            Exit Sub
        End If

        strTxLnParam.Append(lblTxID.Text)
        strTxLnParam.Append("|")
        strTxLnParam.Append(strDate)
        strTxLnParam.Append("|")
        '   strTxLnParam.Append(hidAccCode.Value)
        strTxLnParam.Append(sTrAccCode)
        strTxLnParam.Append("|")
        strTxLnParam.Append(strBlkCode)
        strTxLnParam.Append("||")
        strTxLnParam.Append(hidVehActCode.Value)
        strTxLnParam.Append("|")
        strTxLnParam.Append(txtUnit.Text)
        strTxLnParam.Append("|")
        strTxLnParam.Append(strLocation)
        strTxLnParam.Append("|")
        strTxLnParam.Append(txtAddNote.Value.Trim())
        strTxLnParam.Append("|")
        strTxLnParam.Append(txtRunFrom.Text.Trim())
        strTxLnParam.Append("|")
        strTxLnParam.Append(txtRunTo.Text.Trim())

        Try

            intErrNo = objGLtx.mtdAddVehicleUsageLn(strOpCdStckTxLine_ADD, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.VehicleUsageLn), _
                                                    strTxLnParam.ToString)
            'End If

            Select Case ErrorChk
                Case objGLtx.EnumGeneralLedgerTxErrorType.Overflow
                    lblError.Visible = True
            End Select

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDUSAGELINE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
            End If
        End Try

        StrTxParam.Remove(0, StrTxParam.Length)
        StrTxParam.Append(lblTxID.Text)
        StrTxParam.Append("|||||||")

        Try
            intErrNo = objGLtx.mtdUpdVehicleUsageDetail(strOpCdStckTxDet_ADD, _
                                                        strOpCdStckTxDet_UPD, _
                                                        strOpCdStckTxLine_GET, _
                                                        strUserId, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.VehicleUsage), _
                                                        ErrorChk, _
                                                        TxID)
            If ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.Overflow Then
                lblError.Visible = True
            End If

            lblTxID.Text = TxID
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
            End If
        End Try

        txtAccCode.Text = ""
        txtAccName.Text = ""
        lstPreBlock.Items.Clear()
        lstChargeLevel.SelectedItem.Value = ""
        txtAddNote.Value = ""
        txtRunFrom.Text = ""
        txtRunTo.Text = ""
        txtUnit.Text = ""
        LoadTxDetails()
        DisplayFromDB()
        BindGrid()
        PageControl()
        'txtRunFrom.Text = GetMaxHM()
        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If
        If Not strTxLnParam Is Nothing Then
            strTxLnParam = Nothing
        End If

    End Sub

    Sub btnUpdate_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdStckTxLine_NEW_UPD As String = "GL_CLSTRX_VEHICLEUSAGE_DETAIL_NEW_UPD"
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim StrTxParam As New StringBuilder()
        Dim strBlkCode As String = lstPreBlock.SelectedValue
        Dim strItemCode As String = Request.Form("lstItem")
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        Dim ParamNama As String
        Dim ParamValue As String

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If CheckRequiredField() Then
            Exit Sub
        End If

        ''UPDATE HEADER
        ParamNama = ""
        ParamValue = ""

        ParamNama = "UPDATESTR"
        ParamValue = "SET UpdateDate='" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',UpdateID='" & strUserId & "' where VehUsageID = '" & lblTxID.Text & "'"

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCdStckTxDet_UPD, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MAIN_ADD&errmesg=" & ex.ToString() & "&redirect=")

        End Try

        ''''''UPDATE DETAIL
        ParamNama = ""
        ParamValue = ""

        ParamNama = "ACCCODE|BLOCK|RH|RT|UU|AD|VUID|VULNID"
        ParamValue = txtAccCode.Text & "|" & _
                     lstPreBlock.SelectedValue & "|" & _
                     txtRunFrom.Text & "|" & _
                     txtRunTo.Text & "|" & _
                     txtUnit.Text & "|" & _
                     txtAddNote.Value.Replace("'", "''") & "|" & _
                     lblTxID.Text & "|" & _
                     lblVehLnId.Text

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCdStckTxLine_NEW_UPD, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MAIN_ADD&errmesg=" & ex.ToString() & "&redirect=")
        End Try

        LoadTxDetails()
        DisplayFromDB()
        BindGrid()
        PageControl()

        txtAccCode.Text = ""
        txtAccName.Text = ""
        lstPreBlock.Items.Clear()
        'lstPreBlock.SelectedItem.Value = ""
        'lstPreBlock.SelectedIndex = 0
        lblPreBlockErr.Visible = False
        lstChargeLevel.SelectedItem.Value = ""
        txtAddNote.Value = ""
        txtRunFrom.Text = ""
        txtRunTo.Text = ""
        txtUnit.Text = ""
        ' txtRunFrom.Text = GetMaxHM()
        btnUpdate.Visible = False
        Add.Visible = True

    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim TxID As String
        Dim IssType As String
        Dim strAllowVehicle As String
        Dim ErrorChk As Integer = objGLtx.EnumGeneralLedgerTxErrorType.NoError
        Dim StrTxParam As New StringBuilder()
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If lblTxID.Text = "" Then
            StrTxParam.Append(lblTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(lstVehCode.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|0|")
        Else
            StrTxParam.Append(lblTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(lstVehCode.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|||")
        End If

        Try

            intErrNo = objGLtx.mtdUpdVehicleUsageDetail(strOpCdStckTxDet_ADD, _
                                                        strOpCdStckTxDet_UPD, _
                                                        strOpCdStckTxLine_GET, _
                                                        strUserId, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.VehicleUsage), _
                                                        ErrorChk, _
                                                        TxID)

            lblTxID.Text = TxID
            If ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.Overflow Then
                lblError.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
            End If
        End Try
        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        LoadTxDetails()
        DisplayFromDB()
        PageControl()
        lstPreBlock.Items.Clear()
    End Sub

    Sub btnPrint_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        'Dim StrTxParam As String
        'Dim TxID As String
        'Dim ErrorChk As Integer = objGLtx.EnumGeneralLedgerTxErrorType.NoError
        'If lblPrintDate.Text = "" Then

        '    StrTxParam = lblTxID.Text & "||||||" & Now() & "|"
        '    Try
        '        intErrNo = objGLtx.mtdUpdVehicleUsageDetail(strOpCdStckTxDet_ADD, _
        '                                                    strOpCdStckTxDet_UPD, _
        '                                                    strOpCdStckTxLine_GET, _
        '                                                    strUserId, _
        '                                                    StrTxParam.ToString, _
        '                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.VehicleUsage), _
        '                                                    ErrorChk, _
        '                                                    TxID)
        '        lblTxID.Text = TxID
        '        If ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.OverFlow Then
        '            lblError.Visible = True
        '        End If

        '    Catch Exp As System.Exception
        '        If intErrNo <> -5 Then
        '            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
        '        End If
        '    End Try
        'Else
        '    lblReprint.Visible = False
        'End If

        'LoadTxDetails()
        'DisplayFromDB()
        'PageControl()

        Dim strTRXID As String

        strTRXID = Trim(lblTxID.Text)

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_VehUsage_Details.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strLocation & _
                       "&TRXID=" & strTRXID & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnCancel_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objGLtx.EnumGeneralLedgerTxErrorType.NoError

        StrTxParam = lblTxID.Text & "|||||||" & objGLtx.EnumVehicleUsageStatus.Cancelled

        If intErrNo = 0 And ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.NoError Then
            Try
                intErrNo = objGLtx.mtdUpdVehicleUsageDetail(strOpCdStckTxDet_ADD, _
                                                           strOpCdStckTxDet_UPD, _
                                                           strOpCdStckTxLine_GET, _
                                                           strUserId, _
                                                           StrTxParam.ToString, _
                                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.VehicleUsage), _
                                                           ErrorChk, _
                                                           TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CANCELUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
                End If
            End Try
        End If

        LoadTxDetails()
        DisplayFromDB()
        PageControl()
        BindGrid()

    End Sub

    Sub btnNew_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("GL_Trx_VehicleUsage_details.aspx")
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../GL/Trx/GL_Trx_VehicleUsage_List.aspx")
    End Sub


End Class
