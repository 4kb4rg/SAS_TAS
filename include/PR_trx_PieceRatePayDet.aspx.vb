Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class PR_trx_PieceRatePayDet : Inherits Page

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents lblPieceRateID As Label
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents ddlPreBlock As DropDownList
    Protected WithEvents ddlHarvInc As DropDownlist
    Protected WithEvents txtLoseFruit As TextBox
    Protected WithEvents lblLoseFruit As Label
    Protected WithEvents txtDendaQty As TextBox
    Protected WithEvents lblErrPenaltyQty As Label
    Protected WithEvents ddlDenda As DropDownList
    Protected WithEvents lblErrHarvInc As Label
    Protected WithEvents lblErrDenda As Label
    Protected WithEvents txtDate As TextBox
    Protected WithEvents btnSelDate1 As Image
    Protected WithEvents lblErrDate As Label
    Protected WithEvents lblErrDateFmt As Label
    Protected WithEvents lblErrDateFmtMsg As Label

    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents ddlVehExpCode As DropDownList
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents txtMandays As TextBox
    Protected WithEvents txtTotalUnits As TextBox
    Protected WithEvents txtRate As TextBox
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents payid As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents NewBtn As ImageButton
    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents lblTotalUnit As Label
    Protected WithEvents txtLnDesc As TextBox
    Protected WithEvents lblLnDesc As Label

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblVehicleOption As Label
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrPreBlock As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblErrVehExp As Label
    Protected WithEvents lblErrEmployee As Label
    Protected WithEvents lblErrTotalUnits As Label
    Protected WithEvents lblErrRate As Label
    Protected WithEvents lblErrAmount As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblErrMandays As Label
    Protected WithEvents lblPreBlock As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehExpense As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlock As HtmlTableRow
    Protected WithEvents RowBlock As HtmlTableRow

    Protected WithEvents txtGroupRef As TextBox
    Protected WithEvents txtRefDate As TextBox
    Protected WithEvents btnSelDate As Image
    Protected WithEvents lblErrRefDate As Label
    Protected WithEvents lblErrRefDateDesc As Label
    Protected WithEvents txtRipeBunch As TextBox
    Protected WithEvents txtUnripeBunch As TextBox
    Protected WithEvents txtBunch As TextBox
    Protected WithEvents lblErrRipeBunch As Label
    Protected WithEvents lblErrUnripeBunch As Label
    Protected WithEvents lblErrBunch As Label
    Protected WithEvents hidHarvAutoWeight As HtmlInputHidden
    Protected WithEvents hidHarvGroupWeight As HtmlInputHidden
    Protected WithEvents hidHarvDailyWeight As HtmlInputHidden
    Protected WithEvents hidBunchRatio As HtmlInputHidden
    Protected WithEvents lblBunchRatio As Label
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Protected WithEvents hidAllowCancel As HtmlInputHidden
    Protected WithEvents RowChargeTo As HtmlTableRow
    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents lblLocationErr As Label

    Protected WithEvents btnUpdate As ImageButton
    Protected WithEvents lblTxLnID As Label
    Protected WithEvents btnAdd As ImageButton

    Dim objPRTrx As New agri.PR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()
    
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl() 
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objPieceRateDs As New Object()
    Dim objPieceRateLnDs As New Object()
    Dim objAccDs As New Object()
    Dim objBlkDs As New Object()
    Dim objVehDs As New Object()
    Dim objVehExpDs As New Object()
    Dim objActDs As New Object()
    Dim objADDs As New Object()
    Dim objEmpDs As New Object()
    Dim objLangCapDs As New Object()

    Dim objPaySetupDs As New Object()
    Dim objHarvIncDs As New Object()
    Dim objDendaDs As New Object()
    
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strDateFmt As String

    Dim strSelectedPayId As String = ""
    Dim intStatus As Integer
    Dim PreBlockTag As String
    Dim BlockTag As String

    Dim strCostLevel As String
    Dim strPickerCategory As String

    Protected WithEvents lblBPErr As Label
    Protected WithEvents lblLocCodeErr As Label

    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strEmp As String = Request.QueryString("emp")
        Dim strAcc As String = Request.QueryString("acc")
        Dim strPreBlk As String = Request.QueryString("PreBlk")
        Dim strBlk As String = Request.QueryString("blk")
        Dim strVeh As String = Request.QueryString("veh")
        Dim strVehExp As String = Request.QueryString("vehexp")
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strDateFmt = Session("SS_DATEFMT")
        strCostLevel = Session("SS_COSTLEVEL")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxRatePayment), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
                    ddlLocation.Enabled = False
            onload_GetLangCap()
            lblErrAccount.Visible = False
            lblErrPreBlock.Visible = False
            lblErrBlock.Visible = False
            lblErrVehicle.Visible = False
            lblErrVehExp.Visible = False
            lblErrEmployee.Visible = False
            lblErrTotalUnits.Visible = False
            lblErrRate.Visible = False
            lblErrAmount.Visible = False
            lblErrRefDate.Visible = False
            lblErrRipeBunch.Visible = False
            lblErrUnripeBunch.Visible = False
            strSelectedPayId = Trim(IIf(Request.QueryString("payid") <> "", Request.QueryString("payid"), Request.Form("payid")))
            intStatus = Convert.ToInt16(lblHiddenSts.Text)

            If Not IsPostBack Then
                BindLocationDropDownList(Trim(Session("SS_LOCATION")))
                RowChargeTo.Visible = Session("SS_INTER_ESTATE_CHARGING")
                dgLineDet.Columns(1).Visible = Session("SS_INTER_ESTATE_CHARGING")
                BindChargeLevelDropDownList()
                If strSelectedPayId <> "" Then
                    payid.Value = strSelectedPayId
                    onLoad_PaySetup()
                    onLoad_Display()
                Else
                    txtDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                    onLoad_PaySetup()
                    BindEmployee(strEmp)
                    BindAccount(strAcc)
                    BindPreBlock(strAcc, strPreBlk)
                    BindBlock(strAcc, strBlk)
                    BindVehicle(strAcc, strVeh)
                    BindVehicleExpense(True, "")
                    BindHarvIncentive("")
                    BindDendaCode("")
                    onLoad_BindButton()
                End If
            End If

            lblBPErr.Visible = False
            lblLocCodeErr.Visible = False
        End If
    End Sub

    Sub ddlLocation_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strAccCode As String = ddlAccount.SelectedItem.Value.Trim()
        Dim strVehCode As String = ddlVehCode.SelectedItem.Value.Trim()
        Dim strPreBlkCode As String = ddlPreBlock.SelectedItem.Value.Trim()
        Dim strBlkCode As String = ddlBlock.SelectedItem.Value.Trim()
        BindVehicle(strAccCode, strVehCode)
        BindBlock(strAccCode, strBlkCode)
        BindPreBlock(strAccCode, strPreBlkCode)
        hidChargeLocCode.value = ddlLocation.SelectedItem.Value.Trim()
    End Sub

    Sub BindLocationDropDownList(ByVal pv_strLocCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim dsLoc As DataSet
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        strOpCd = "ADMIN_CLSLOC_INTER_ESTATE_LOCATION_GET"
        intSelectedIndex = 0
        Try
            strParam = objAdminLoc.EnumLocStatus.Active & "|" & _
                       Trim(Session("SS_COMPANY")) & "|" & _
                       Trim(Session("SS_LOCATION")) & "|" & _
                       Trim(Session("SS_USERID")) & "|" & _
                       objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Payroll) & "|" & _
                       "PRAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxRatePayment) & "|" & _
                       "PRAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxRatePayment)
            intErrNo = objAdminLoc.mtdGetInterEstateLoc(strOpCd, _
                                                        strParam, _
                                                        dsLoc)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsLoc.Tables(0).Rows.Count - 1
            dsLoc.Tables(0).Rows(intCnt).Item("LocCode") = Trim(dsLoc.Tables(0).Rows(intCnt).Item("LocCode"))
            dsLoc.Tables(0).Rows(intCnt).Item("Description") = Trim(dsLoc.Tables(0).Rows(intCnt).Item("Description"))
            If dsLoc.Tables(0).Rows(intCnt).Item("LocCode") = Trim(pv_strLocCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next
        
        If Trim(pv_strLocCode) <> "" And intSelectedIndex = 0 Then
            dr = dsLoc.Tables(0).NewRow()
            dr("LocCode") = Trim(pv_strLocCode)
            dr("Description") = Trim(pv_strLocCode) & " (Deleted)"
            dsLoc.Tables(0).Rows.InsertAt(dr, 0)
            intSelectedIndex = 1
        End If

        dr = dsLoc.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelect.Text & GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text

        dsLoc.Tables(0).Rows.InsertAt(dr, 0)
        ddlLocation.DataSource = dsLoc.Tables(0)
        ddlLocation.DataValueField = "LocCode"
        ddlLocation.DataTextField = "Description"
        ddlLocation.DataBind()
        ddlLocation.SelectedIndex = intSelectedIndex

        If Not dsLoc Is Nothing Then
            dsLoc = Nothing
        End If
    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.selectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub
    
    Sub ddlChargeLevel_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        ToggleChargeLevel()
    End Sub
    
    Sub ToggleChargeLevel()
        If ddlChargeLevel.selectedIndex = 0 Then
            RowBlock.Visible = False
            RowPreBlock.Visible = True
            hidBlockCharge.value = "yes"
        Else
            RowBlock.Visible = True
            RowPreBlock.Visible = False
            hidBlockCharge.value = ""
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfig) = True Then
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAYDET_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_PieceRatePayList.aspx")
        End Try

        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

        dgLineDet.Columns(4).HeaderText = lblAccount.Text
        dgLineDet.Columns(5).HeaderText = lblBlock.Text
        dgLineDet.Columns(6).HeaderText = lblVehicle.Text
        dgLineDet.Columns(7).HeaderText = lblVehExpense.Text

        lblErrAccount.Text = lblErrSelect.Text & lblAccount.Text
        lblErrBlock.Text = lblErrSelect.Text & lblBlock.Text
        lblErrVehicle.Text = lblErrSelect.Text & lblVehicle.Text
        lblErrVehExp.Text = lblErrSelect.Text & lblVehExpense.Text

        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlock.Text =  GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text 
        lblErrPreBlock.Text = lblErrSelect.Text & GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        lblLocationErr.Text = lblErrSelect.Text & GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PieceRatePayDet.aspx")
        End Try

    End Sub



    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function



    Sub onLoad_BindButton()
        txtDesc.Enabled = False
        SaveBtn.Visible = False
        CancelBtn.Visible = False
        NewBtn.Visible = False
        tblSelection.Visible = False
        txtGroupRef.Enabled = False
        txtRefDate.Enabled = False
        btnSelDate.Visible = False
        txtRipeBunch.Enabled = False
        txtUnripeBunch.Enabled = False

        Select Case intStatus
            Case objPRTrx.EnumPieceRateStatus.Active
                txtDesc.Enabled = True
                SaveBtn.Visible = True
                If hidAllowCancel.Value = "yes" Then
                    CancelBtn.Visible = True
                End If
                NewBtn.Visible = True
                tblSelection.Visible = True
                If hidHarvAutoWeight.Value = objPRSetup.EnumHarvAutoWeight.Yes Then
                    txtRipeBunch.Enabled = True
                    txtUnripeBunch.Enabled = True
                End If
                If hidHarvGroupWeight.Value = objPRSetup.EnumHarvAutoWeightGroup.Yes Then
                    txtGroupRef.Enabled = True
                End If
                If hidHarvDailyWeight.Value = objPRSetup.EnumHarvAutoWeightDaily.Yes Then
                    txtRefDate.Enabled = True
                    btnSelDate.Visible = True
                End If

            Case objPRTrx.EnumPieceRateStatus.Closed, objPRTrx.EnumPieceRateStatus.Cancelled
            Case Else
                txtDesc.Enabled = True
                SaveBtn.Visible = True
                tblSelection.Visible = True
                If hidHarvAutoWeight.Value = objPRSetup.EnumHarvAutoWeight.Yes Then
                    txtRipeBunch.Enabled = True
                    txtUnripeBunch.Enabled = True
                End If
                If hidHarvGroupWeight.Value = objPRSetup.EnumHarvAutoWeightGroup.Yes Then
                    txtGroupRef.Enabled = True
                End If
                If hidHarvDailyWeight.Value = objPRSetup.EnumHarvAutoWeightDaily.Yes Then
                    txtRefDate.Enabled = True
                    btnSelDate.Visible = True
                End If
        End Select
    End Sub


    Sub onLoad_PaySetup()
        Dim strOpCd As String = "PR_CLSSETUP_PAYROLLPROCESS_GET"
        Dim intErrNo As Integer
        Dim intIndex As Integer
        Dim strParam As String
        Dim strHarvAutoWeight As String
        Dim strHarvGroupWeight As String
        Dim strHarvDailyWeight As String

        Try
            strParam = "|"
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   0, _
                                                   objPaySetupDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_GETPAYSETUP&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_pieceratedet.aspx")
        End Try

        If objPaySetupDs.Tables(0).Rows.Count > 0 Then
            hidHarvAutoWeight.Value = objPaySetupDs.Tables(0).Rows(0).Item("HarvAutoWeight")
            hidHarvGroupWeight.Value = objPaySetupDs.Tables(0).Rows(0).Item("HarvGroupWeight")
            hidHarvDailyWeight.Value = objPaySetupDs.Tables(0).Rows(0).Item("HarvDailyWeight")
        End If

    End Sub


    Sub onLoad_Display()
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strPreBlk As String = Request.Form("ddlAccount")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")
        Dim strHarvInc As string = Request.Form("ddlHarvInc")
        Dim strDenda As String = Request.Form("ddlDenda")
        Dim strOpCd As String = "PR_CLSTRX_PIECERATEPAY_GET"
        Dim strParam As String = strSelectedPayId & "|||"
        Dim intErrNo As Integer

        Try
            intErrNo = objPRTrx.mtdGetPieceRatePay(strOpCd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strAccMonth, _
                                                  strAccYear, _
                                                  strParam, _
                                                  objPieceRateDs, _
                                                  True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_GET&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_trx_pieceratepaydet.aspx")
        End Try

        payid.Value = strSelectedPayId
        lblPieceRateID.Text = strSelectedPayId
        txtDesc.Text = objPieceRateDs.Tables(0).Rows(0).Item("Description").Trim()
        lblPeriod.Text = objPieceRateDs.Tables(0).Rows(0).Item("AccMonth") & "/" & objPieceRateDs.Tables(0).Rows(0).Item("AccYear")
        intStatus = Convert.ToInt16(objPieceRateDs.Tables(0).Rows(0).Item("Status"))
        lblHiddenSts.Text = objPieceRateDs.Tables(0).Rows(0).Item("Status").Trim()
        lblStatus.Text = objPRTrx.mtdGetPieceRateStatus(objPieceRateDs.Tables(0).Rows(0).Item("Status"))
        lblDateCreated.Text = objGlobal.GetLongDate(objPieceRateDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objPieceRateDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = objPieceRateDs.Tables(0).Rows(0).Item("UserName")

        
        BindEmployee(strEmp)
        BindAccount(strAcc)
        BindPreBlock(strAcc, strPreBlk)        
        
        BindHarvIncentive(strHarvInc)
        BindDendaCode(strDenda)
        BindBlock(strAcc, strBlk)
        BindVehicle(strAcc, strVeh)
        If Trim(strVehExp) = "" Then
            BindVehicleExpense(True, strVehExp)
        Else
            BindVehicleExpense(False, strVehExp)
        End If
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub

    Sub onLoad_DisplayLine()
        Dim strOpCd As String = "PR_CLSTRX_PIECERATEPAY_LINE_GET"
        Dim strParam As String = strSelectedPayId & "|||"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dblTotalAmount As Double = 0
        Dim dblTotalUnit As Double = 0
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim intFlag As Integer = 0
        Dim EditButton As LinkButton

        Try
            intErrNo = objPRTrx.mtdGetPieceRatePay(strOpCd, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strParam, _
                                                   objPieceRateLnDs, _
                                                   True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_LINE_GET&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_trx_pieceratedet.aspx")
        End Try

        dgLineDet.DataSource = objPieceRateLnDs.Tables(0)
        dgLineDet.DataBind()

        If intStatus = objPRTrx.EnumPieceRateStatus.Active Then
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbl = dgLineDet.Items.Item(intCnt).FindControl("lblEmpPayrollInd")
                If lbl.Text = objHRTrx.EnumEmpPayrollInd.No Then
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Else
                    intFlag = intFlag + 1
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    EditButton = dgLineDet.Items.Item(intCnt).FindControl("Edit")
                    EditButton.Visible = False
                End If
            Next
        Else
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = False
                EditButton = dgLineDet.Items.Item(intCnt).FindControl("Edit")
                EditButton.Visible = False
            Next
        End If

        For intCnt = 0 To objPieceRateLnDs.Tables(0).Rows.Count - 1
            objPieceRateLnDs.Tables(0).Rows(intCnt).Item("PieceRateLnId") = objPieceRateLnDs.Tables(0).Rows(intCnt).Item("PieceRateLnId").Trim()
            objPieceRateLnDs.Tables(0).Rows(intCnt).Item("EmpCode") = objPieceRateLnDs.Tables(0).Rows(intCnt).Item("EmpCode").Trim()
            objPieceRateLnDs.Tables(0).Rows(intCnt).Item("EmpName") = objPieceRateLnDs.Tables(0).Rows(intCnt).Item("EmpName").Trim()
            dblTotalAmount += objPieceRateLnDs.Tables(0).Rows(intCnt).Item("Amount")
            dblTotalUnit += objPieceRateLnDs.Tables(0).Rows(intCnt).Item("Unit")

            If IsDBNull(objPieceRateLnDs.Tables(0).Rows(intCnt).Item("Description")) Then
                objPieceRateLnDs.Tables(0).Rows(intCnt).Item("Description") = ""
            Else
                objPieceRateLnDs.Tables(0).Rows(intCnt).Item("Description") = objPieceRateLnDs.Tables(0).Rows(intCnt).Item("Description").Trim()
            End If

            
            If objPieceRateLnDs.Tables(0).Rows(intCnt).Item("RefDate") = "1 Jan 1900" Then
                lbl = dgLineDet.Items.Item(intCnt).FindControl("lblRefDate")
                lbl.text = ""
            Else
                lbl = dgLineDet.Items.Item(intCnt).FindControl("lblRefDate")
                lbl.text = objGlobal.GetShortDate(strDateFmt, objPieceRateLnDs.Tables(0).Rows(intCnt).Item("RefDate"))
            End If
        Next
        lblTotalAmount.Text = ObjGlobal.GetIDDecimalSeparator(FormatNumber(dblTotalAmount, 2))
        lblTotalUnit.Text = ObjGlobal.GetIDDecimalSeparator(FormatNumber(dblTotalUnit, 5))
        If intFlag = 0 Then
            hidAllowCancel.Value = "yes"
        End If
    End Sub

    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & _
                                 objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = lblSelect.Text & lblAccount.Text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = objAccDs.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "_Description"
        ddlAccount.DataBind()
        ddlAccount.SelectedIndex = intSelectedIndex
        ddlAccount.AutoPostBack = True
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_IsBalanceSheet As Boolean, _
                          ByRef pr_IsNurseryInd As Boolean, _
                          ByRef pr_IsBlockRequire As Boolean, _
                          ByRef pr_IsVehicleRequire As Boolean, _
                          ByRef pr_IsOthers As Boolean)

        Dim _objAccDs As New Object()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            pr_IsBalanceSheet = False
            pr_IsNurseryInd = False
            pr_IsBlockRequire = False
            pr_IsVehicleRequire = False
            pr_IsOthers = False
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType")) = objGLSetup.EnumAccountType.BalanceSheet Then
                pr_IsBalanceSheet = True
                If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd")) = objGLSetup.EnumNurseryAccount.Yes Then
                    pr_IsNurseryInd = True
                End If
            End If
            If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.NonVehicle Then
                pr_IsBlockRequire = True
            ElseIf Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.VehicleDistribution Then
                pr_IsVehicleRequire = True
            ElseIf Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.Others Then
                pr_IsBlockRequire = True
                pr_IsOthers = True
            End If
        End If
    End Sub


    Sub onSelect_Account(ByVal Sender As Object, ByVal E As EventArgs)
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strPreBlk As String = Request.Form("ddlPreBlock")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")

        GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

        If Not blnIsBalanceSheet Then
            If blnIsBlockRequire Then
                BindPreBlock(strAcc, strPreBlk)
                BindBlock(strAcc, strBlk)
                BindVehicle("", strVeh)
                BindVehicleExpense(True, strVehExp)
            Else
                BindPreBlock("", strPreBlk)
                BindBlock("", strBlk)
                BindVehicle("", strVeh)
                BindVehicleExpense(True, strVehExp)
            End If
            If blnIsVehicleRequire Then
                BindVehicle(strAcc, strVeh)
                BindVehicleExpense(False, strVehExp)
            End If
            If blnIsOthers Then
                lblVehicleOption.Text = True
                BindVehicle("%", strVeh)
                BindVehicleExpense(False, strVehExp)
            Else
                lblVehicleOption.Text = False
            End If
        ElseIf blnIsNurseryInd = True Then
            BindPreBlock(strAcc, strPreBlk)
            BindBlock(strAcc, strBlk)
            BindVehicle("", strVeh)
            BindVehicleExpense(True, strVehExp)
        Else
            BindPreBlock("", strPreBlk)
            BindBlock("", strBlk)
            BindVehicle("", strVeh)
            BindVehicleExpense(True, strVehExp)
        End If
    End Sub

    Sub onSelect_Block(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strOpCd As String
        Dim objBlkDs As New Object
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strSelBlkCode As String
        
        If LCase(strCostLevel) = "block" Then
            If ddlBlock.SelectedItem.Value <> "" Then
                strOpCd = "GL_CLSSETUP_BLOCK_LIST_GET"
                strParam = "|and blk.BlkCode = '" & ddlBlock.SelectedItem.Value & "' "
                Try
                    intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                           strParam, _
                                                           0, _
                                                           objBlkDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_GET_BUNCHRATIO&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
                If objBlkDs.Tables(0).Rows.Count > 0 Then
                    lblBunchRatio.Text = objBlkDs.Tables(0).Rows(0).Item("BunchRatio")
                    hidBunchRatio.Value = objBlkDs.Tables(0).Rows(0).Item("BunchRatio")
                End If
            End If 
        Else
            If ddlChargeLevel.SelectedItem.Value = objLangCap.EnumLangCap.SubBlock And ddlBlock.SelectedItem.Value <> "" Then
                strOpCd = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
                strParam = "|and sub.SubBlkCode = '" & ddlBlock.SelectedItem.Value & "' "
                Try
                    intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                           strParam, _
                                                           0, _
                                                           objBlkDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_GET_BUNCHRATIO_SUB&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
                If objBlkDs.Tables(0).Rows.Count > 0 Then
                    lblBunchRatio.Text = objBlkDs.Tables(0).Rows(0).Item("BunchRatio")
                    hidBunchRatio.Value = objBlkDs.Tables(0).Rows(0).Item("BunchRatio")
                End If
            End If
        End If

        If Not objBlkDs Is Nothing Then
            objBlkDs = Nothing
        End If
    
    End Sub

    Sub BindPreBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim objBlkDs As DataSet
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        strOpCd = "PR_CLSTRX_PIECERATEPAY_PREBLOCK_GET"
        intSelectedIndex = 0
        Try
            strParam = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLSetup.EnumBlockStatus.Active
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("_Description") = lblSelect.Text & PreBlockTag & lblCode.Text

        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlPreBlock.DataSource = objBlkDs.Tables(0)
        ddlPreBlock.DataValueField = "BlkCode"
        ddlPreBlock.DataTextField = "_Description"
        ddlPreBlock.DataBind()
        ddlPreBlock.SelectedIndex = intSelectedIndex

        If Not objBlkDs Is Nothing Then
            objBlkDs = Nothing
        End If
    End Sub

    Sub BindBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfig) = True Then
                strOpCd = "PR_CLSTRX_PIECERATEPAY_GET_BLOCK_BY_ACC"
                strParam = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCd = "PR_CLSTRX_PIECERATEPAY_GET_SUBBLK_BY_ACC"
                strParam = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("_Description") = lblSelect.Text & lblBlock.Text
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "_Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindVehicle(ByVal pv_strAccCode As String, ByVal pv_strVehCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"
            strParam = "|AccCode = '" & pv_strAccCode & "' AND LocCode = '" & ddlLocation.SelectedItem.Value.Trim() & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'"
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   objVehDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_VEH_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objVehDs.Tables(0).Rows.Count - 1
            If objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(pv_strVehCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objVehDs.Tables(0).NewRow()
        dr("VehCode") = ""
        dr("_Description") = lblSelect.Text & lblVehicle.Text
        objVehDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehCode.DataSource = objVehDs.Tables(0)
        ddlVehCode.DataValueField = "VehCode"
        ddlVehCode.DataTextField = "_Description"
        ddlVehCode.DataBind()
        ddlVehCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindVehicleExpense(ByVal pv_IsBlankList As Boolean, ByVal pv_strVehExpCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By VehExpenseCode ASC| And Veh.Status = '" & objGLSetup.EnumVehExpenseStatus.Active & "' "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            If pv_IsBlankList Then
                strParam += "And Veh.VehExpensecode = ''"
            End If
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.VehicleExpense, _
                                                   objVehExpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_VEHEXPENSE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objVehExpDs.Tables(0).Rows.Count - 1
            If objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(pv_strVehExpCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objVehExpDs.Tables(0).NewRow()
        dr("VehExpenseCode") = ""
        dr("_Description") = lblSelect.Text & lblVehExpense.Text
        objVehExpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehExpCode.DataSource = objVehExpDs.Tables(0)
        ddlVehExpCode.DataValueField = "VehExpenseCode"
        ddlVehExpCode.DataTextField = "_Description"
        ddlVehExpCode.DataBind()
        ddlVehExpCode.SelectedIndex = intSelectedIndex
    End Sub






    Sub BindEmployee(ByVal pv_strEmpId As String)
        Dim strOpCd As String = "PR_CLSTRX_PIECERATEPAY_EMPLOYEE_GET"
        Dim dr As DataRow
        Dim strPayTypeList As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        strPayTypeList = CStr(objPRSetup.EnumPayType.DailyRate) & "','" & CStr(objPRSetup.EnumPayType.PieceRate)

        Try
            strParam = "|" & _
                        strPayTypeList & "|" & _
                        objPRTrx.EnumPieceRateStatus.Active & "|" & _
                        objHRTrx.EnumEmpStatus.Active

            intErrNo = objPRTrx.mtdGetPieceRatePay(strOpCd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strAccMonth, _
                                                  strAccYear, _
                                                  strParam, _
                                                  objEmpDs, _
                                                  True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_EMP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            If objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(pv_strEmpId) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objEmpDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_EmpName") = "Select Employee Code"
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmployee.DataSource = objEmpDs.Tables(0)
        ddlEmployee.DataValueField = "EmpCode"
        ddlEmployee.DataTextField = "_EmpName"
        ddlEmployee.DataBind()
        ddlEmployee.SelectedIndex = intSelectedIndex
    End Sub

    Sub InsertRecord(ByRef pr_blnUpdated As Boolean)
        Dim objPieceRateId As String
        Dim strOpCd_Add As String = "PR_CLSTRX_PIECERATEPAY_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_PIECERATEPAY_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_PIECERATEPAY_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_PIECERATEPAY_STATUS_UPD"
        Dim strOpCd As String
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim objFormatDate As String
        Dim objActualDate As String

        pr_blnUpdated = False



        If Trim(txtDate.Text) = "" Then
            lblErrDate.Visible = True
            Exit Sub
        ElseIf objGlobal.mtdValidInputDate(strDateFmt, _
                                           Trim(txtDate.Text), _
                                           objFormatDate, _
                                           objActualDate) = False Then
    	    lblErrDateFmt.Visible = True
            lblErrDateFmt.Text = lblErrDateFmtMsg.Text & objFormatDate
            Exit Sub
        End If

        strSelectedPayId = Trim(payid.Value)
        strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)

        strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRPieceRatePay) & "|" & _
                   strSelectedPayId & "|" & _
                   txtDesc.Text & "|0|0|" & _
                   objPRTrx.EnumPieceRateStatus.Active & "|" & _
                   trim(objActualDate) 
        Try
            intErrNo = objPRTrx.mtdUpdPieceRatePay(strOpCd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                False, _
                                                objPieceRateId)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_PIECERATEPAY_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_PieceRatepaydet.aspx")
        End Try
        strSelectedPayId = objPieceRateId
        payid.Value = strSelectedPayId
        pr_blnUpdated = True
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCodeGLSubBlkByBlk As String = "PR_CLSTRX_PIECERATEPAY_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strOpCode_AddLine As String = "PR_CLSTRX_PIECERATEPAY_LINE_ADD"
        Dim strOpCode_TotalLine As String = "PR_CLSTRX_PIECERATEPAY_LINE_TOTAL_GET"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_PIECERATEPAY_TOTALAMT_UPD"

        Dim strOpCodes As String = strOpCode_AddLine & "|" & strOpCode_TotalLine & "|" & strOpCode_UpdID
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strBlk As String 
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")
        Dim blnUpdated As Boolean
        Dim strParam As String
        Dim intErrNo As Integer
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim objFormatDate As String
        Dim strRefDate As String        
        Dim decRipeBunch As Decimal
        Dim decUnripeBunch As Decimal
        Dim decTotalBunch As Decimal

        If ddlLocation.SelectedIndex = 0 Then
            lblLocationErr.Visible = True
            Exit Sub
        End If

        If ddlChargeLevel.SelectedIndex = 0 Then
            strBlk = Request.Form("ddlPreBlock")
        Else
            strBlk = Request.Form("ddlBlock")
        End If

        If strEmp = "" Then
            lblErrEmployee.Visible = True
            Exit Sub
        ElseIf strAcc = "" Then
            lblErrAccount.Visible = True
            Exit Sub
        End If

        If CheckLocBillParty() = False Then
            lblBPErr.Visible = True
            lblLocCodeErr.Visible = True
            Exit Sub
        End If  
        
        If ddlHarvInc.SelectedIndex = 0 Then
            lblErrHarvInc.Visible = True
            Exit Sub
        ElseIf ddlHarvInc.SelectedItem.Value <> "" Then
            SearchPickerCategory(ddlHarvInc.SelectedItem.Value)        
        End IF

        If txtDendaQty.Text = "" Then
            txtDendaQty.Text = "0" 
        End If

        GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

        If strBlk = "" And blnIsBlockRequire = True And blnIsBalanceSheet = False Then
            If ddlChargeLevel.SelectedIndex = 0 Then
                lblErrPreBlock.Visible = True
            Else
                lblErrBlock.Visible = True
            End If
            Exit Sub
        ElseIf strBlk = "" And blnIsBalanceSheet = True And blnIsNurseryInd = True Then
            If ddlChargeLevel.SelectedIndex = 0 Then
                lblErrPreBlock.Visible = True
            Else
                lblErrBlock.Visible = True
            End If
            Exit Sub
        ElseIf strVeh = "" And blnIsVehicleRequire = True Then
            lblErrVehicle.Visible = True
            Exit Sub
        ElseIf strVehExp = "" And blnIsVehicleRequire = True Then
            lblErrVehExp.Visible = True
            Exit Sub
        ElseIf strVeh <> "" And strVehExp = "" And lblVehicleOption.Text = True Then
            lblErrVehExp.Visible = True
            Exit Sub
        ElseIf strVeh = "" And strVehExp <> "" And lblVehicleOption.Text = True Then
            lblErrVehicle.Visible = True
            Exit Sub


        ElseIf txtTotalUnits.Text = "" Then
            lblErrTotalUnits.Visible = True
            Exit Sub
        ElseIf txtRate.Text = "" Then
            lblErrRate.Visible = True
            Exit Sub
        ElseIf txtRipeBunch.Text = "" And hidHarvAutoWeight.Value = objPRSetup.EnumHarvAutoWeight.Yes Then
            lblErrRipeBunch.Visible = True
            Exit Sub
        ElseIf txtUnripeBunch.Text = "" And hidHarvAutoWeight.Value = objPRSetup.EnumHarvAutoWeight.Yes Then
            lblErrUnripeBunch.Visible = True
            Exit Sub
        Else
            InsertRecord(blnUpdated)

            If txtRefDate.Text <> "" Then
                If objGlobal.mtdValidInputDate(strDateFmt, _
                                                txtRefDate.Text, _
                                                objFormatDate, _
                                                strRefDate) = False Then
                lblErrRefDate.Text = lblErrRefDateDesc.Text & objFormatDate & "."
                lblErrRefDate.Visible = True
                Exit Sub
                End If
            End If

            txtMandays.Text = IIF(txtMandays.Text.Trim = "", 0, txtMandays.Text)
            decRipeBunch = IIF(txtRipeBunch.Text = "", 0, txtRipeBunch.Text)
            decUnripeBunch = IIF(txtUnripeBunch.Text = "", 0, txtUnripeBunch.Text)
            decTotalBunch = IIF(txtBunch.Text = "", 0, txtBunch.Text)
            strPickerCategory = IIF(strPickerCategory = "", "0" ,strPickerCategory)

            txtAmount.Text = Cdbl(txtTotalUnits.Text) * CInt(txtRate.Text)
            txtAmount.Text = CInt(txtAmount.Text)

            If blnUpdated = False Then
                Exit Sub
            Else
                Try
                    strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRPieceRatePayLn) & "||" & _
                               strSelectedPayId & "|" & _
                               strEmp & "|" & _
                               strAcc & "|" & _
                               strBlk & "|" & _
                               strVeh & "|" & _
                               strVehExp & "|" & _
                               txtTotalUnits.Text & "|" & _
                               txtRate.Text & "|" & _
                               txtAmount.Text & "|" & _
                               objPRTrx.EnumPieceRateStatus.Active & "|" & _
                               txtMandays.Text & "|" & _
                               txtLnDesc.Text & "|" & _
                               txtGroupRef.Text & "|" & _
                               strRefDate & "|" & _
                               decRipeBunch & "|" & _
                               decUnRipeBunch & "|" & _
                               decTotalBunch & "|" & _
                               ddlLocation.SelectedItem.Value.Trim & "|" & _
                               ddlDenda.SelectedItem.value.Trim & "|" & _
                               Trim(txtDendaQty.Text) & "|" & _ 
                               ddlHarvInc.SelectedItem.value.Trim & "|" & _                                 
                               Trim(txtLoseFruit.Text) & "|" & _  
                               strPickerCategory 
                
                    If ddlChargeLevel.SelectedIndex = 0 And RowPreBlock.Visible = True Then
                        strParamList = ddlLocation.SelectedItem.Value.Trim & "|" & _
                                       strAcc & "|" & _
                                       ddlPreBlock.SelectedItem.Value.Trim & "|" & _
                                       objGLSetup.EnumBlockStatus.Active
                        intErrNo = objPRTrx.mtdUpdPieceRatePayLineByBlock(strOpCodeGLSubBlkByBlk, _
                                                                          strParamList, _ 
                                                                          strOpCodes, _
                                                                          strCompany, _
                                                                          strLocation, _
                                                                          strUserId, _
                                                                          strParam, _
                                                                          False, _
                                                                          objResult)
                    Else
                        intErrNo = objPRTrx.mtdUpdPieceRatePayLine(strOpCodes, _
                                                                   strCompany, _
                                                                   strLocation, _
                                                                   strUserId, _
                                                                   strParam, _
                                                                   False, _
                                                                   objResult)
                    End If
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_LINE_ADD&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_PieceRatepaydet.aspx?payid=" & strSelectedPayId)
                End Try
            End If
            onLoad_Display()
        End If
    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSTRX_PIECERATEPAY_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_PIECERATEPAY_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_PIECERATEPAY_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_PIECERATEPAY_STATUS_UPD"
        Dim strOpCd As String
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strPreBlk As String = Request.Form("ddlPreBlock")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")
        Dim objPieceRateId As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim blnUpdated As Boolean
        Dim intErrNo As Integer
        Dim strParam As String = ""


        If strCmdArgs = "New" Then
            Response.Redirect("PR_trx_PieceRatePayDet.aspx?" & _
                              "&emp=" & strEmp & _
                              "&acc=" & strAcc & _
                              "&blk=" & strBlk & _
                              "&veh=" & strVeh & _
                              "&vehexp=" & strVehExp & _
                              "&preblk=" & strPreBlk)
        End If


        If strCmdArgs = "Save" Then
            InsertRecord(blnUpdated)
            If blnUpdated = False Then
                Exit Sub
            End If
        ElseIf strCmdArgs = "Cancel" Then
            strParam = strSelectedPayId & "|" & objPRTrx.EnumPieceRateStatus.Cancelled
            Try
                intErrNo = objPRTrx.mtdUpdPieceRatePay(strOpCd_Sts, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strParam, _
                                                       True, _
                                                       objPieceRateId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_PIECERATEPAY_CANCEL&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_PieceRatepaydet.aspx?payid=" & strSelectedPayId)
            End Try
        End If

        If strSelectedPayId <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub btnUpdate_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCodeGLSubBlkByBlk As String = "PR_CLSTRX_PIECERATEPAY_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strOpCode_UpdLine As String = "PR_CLSTRX_PIECERATEPAY_LINE_UPD"
        Dim strOpCode_TotalLine As String = "PR_CLSTRX_PIECERATEPAY_LINE_TOTAL_GET"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_PIECERATEPAY_TOTALAMT_UPD"
        Dim strOpCodes As String = strOpCode_UpdLine & "|" & strOpCode_TotalLine & "|" & strOpCode_UpdID
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")
        Dim blnUpdated As Boolean
        Dim strParam As String
        Dim intErrNo As Integer
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim objFormatDate As String
        Dim strRefDate As String        
        Dim decRipeBunch As Decimal
        Dim decUnripeBunch As Decimal
        Dim decTotalBunch As Decimal

        If ddlLocation.SelectedIndex = 0 Then
            lblLocationErr.Visible = True
            Exit Sub
        End If

        If ddlChargeLevel.SelectedIndex = 0 Then
            strBlk = Request.Form("ddlPreBlock")
        Else
            strBlk = Request.Form("ddlBlock")
        End If

        If strEmp = "" Then
            lblErrEmployee.Visible = True
            Exit Sub
        ElseIf strAcc = "" Then
            lblErrAccount.Visible = True
            Exit Sub
        End If

        If CheckLocBillParty() = False Then
            lblBPErr.Visible = True
            lblLocCodeErr.Visible = True
            Exit Sub
        End If  

        If ddlHarvInc.SelectedIndex = 0 Then
            lblErrHarvInc.Visible = True
            Exit Sub
        ElseIf ddlHarvInc.SelectedItem.Value <> "" Then
            SearchPickerCategory(ddlHarvInc.SelectedItem.Value)        
        End IF

        If ddlDenda.SelectedIndex <> 0 And txtDendaQty.Text = "0" Then
            lblErrPenaltyQty.Visible = True
            Exit Sub
        End If
        GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

        If strBlk = "" And blnIsBlockRequire = True And blnIsBalanceSheet = False Then
            If ddlChargeLevel.SelectedIndex = 0 Then
                lblErrPreBlock.Visible = True
            Else
                lblErrBlock.Visible = True
            End If
            Exit Sub
        ElseIf strBlk = "" And blnIsBalanceSheet = True And blnIsNurseryInd = True Then
            If ddlChargeLevel.SelectedIndex = 0 Then
                lblErrPreBlock.Visible = True
            Else
                lblErrBlock.Visible = True
            End If
            Exit Sub
        ElseIf strVeh = "" And blnIsVehicleRequire = True Then
            lblErrVehicle.Visible = True
            Exit Sub
        ElseIf strVehExp = "" And blnIsVehicleRequire = True Then
            lblErrVehExp.Visible = True
            Exit Sub
        ElseIf strVeh <> "" And strVehExp = "" And lblVehicleOption.Text = True Then
            lblErrVehExp.Visible = True
            Exit Sub
        ElseIf strVeh = "" And strVehExp <> "" And lblVehicleOption.Text = True Then
            lblErrVehicle.Visible = True
            Exit Sub
        ElseIf txtTotalUnits.Text = "" Then
            lblErrTotalUnits.Visible = True
            Exit Sub
        ElseIf txtRate.Text = "" Then
            lblErrRate.Visible = True
            Exit Sub
        ElseIf txtRipeBunch.Text = "" And hidHarvAutoWeight.Value = objPRSetup.EnumHarvAutoWeight.Yes Then
            lblErrRipeBunch.Visible = True
            Exit Sub
        ElseIf txtUnripeBunch.Text = "" And hidHarvAutoWeight.Value = objPRSetup.EnumHarvAutoWeight.Yes Then
            lblErrUnripeBunch.Visible = True
            Exit Sub
        Else
            InsertRecord(blnUpdated)

            If txtRefDate.Text <> "" Then
                If objGlobal.mtdValidInputDate(strDateFmt, _
                                                txtRefDate.Text, _
                                                objFormatDate, _
                                                strRefDate) = False Then
                lblErrRefDate.Text = lblErrRefDateDesc.Text & objFormatDate & "."
                lblErrRefDate.Visible = True
                Exit Sub
                End If
            End If

            decRipeBunch = IIF(txtRipeBunch.Text = "", 0, txtRipeBunch.Text)
            decUnripeBunch = IIF(txtUnripeBunch.Text = "", 0, txtUnripeBunch.Text)
            decTotalBunch = IIF(txtBunch.Text = "", 0, txtBunch.Text)
            strPickerCategory = IIF(strPickerCategory = "", "0" ,strPickerCategory)
            txtMandays.Text = IIF(txtMandays.Text.Trim = "", 0, txtMandays.Text)
            txtAmount.Text = CDbl(txtTotalUnits.Text) * CInt(txtRate.Text)
            txtAmount.Text = CInt(txtAmount.Text)

            If blnUpdated = False Then
                Exit Sub
            Else
                Try
                    strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRPieceRatePayLn) & "|" & _
                               lblTxLnID.Text  & "|" & _
                               strSelectedPayId & "|" & _
                               strEmp & "|" & _
                               strAcc & "|" & _
                               strBlk & "|" & _
                               strVeh & "|" & _
                               strVehExp & "|" & _
                               txtTotalUnits.Text & "|" & _
                               txtRate.Text & "|" & _
                               txtAmount.Text & "|" & _
                               objPRTrx.EnumPieceRateStatus.Active & "|" & _
                               txtMandays.Text & "|" & _
                               txtLnDesc.Text & "|" & _
                               txtGroupRef.Text & "|" & _
                               strRefDate & "|" & _
                               decRipeBunch & "|" & _
                               decUnRipeBunch & "|" & _
                               decTotalBunch & "|" & _
                               ddlLocation.SelectedItem.Value.Trim & "|" & _ 
                               ddlDenda.SelectedItem.value.Trim & "|" & _
                               Trim(txtDendaQty.Text) & "|" & _ 
                               ddlHarvInc.SelectedItem.value.Trim & "|" & _                                 
                               Trim(txtLoseFruit.Text) & "|" & _  
                               strPickerCategory    


                    If ddlChargeLevel.SelectedIndex = 0 And RowPreBlock.Visible = True Then

                        strParamList = ddlLocation.SelectedItem.Value.Trim & "|" & _
                                       strAcc & "|" & _
                                       ddlPreBlock.SelectedItem.Value.Trim & "|" & _
                                       objGLSetup.EnumBlockStatus.Active

                        intErrNo = objPRTrx.mtdEditPieceRatePayLineByBlock(strOpCodeGLSubBlkByBlk, _
                                                                          strParamList, _ 
                                                                          strOpCodes, _
                                                                          strCompany, _
                                                                          strLocation, _
                                                                          strUserId, _
                                                                          strParam, _
                                                                          False, _
                                                                          objResult)
                    Else
                        intErrNo = objPRTrx.mtdEditPieceRatePayLine(strOpCodes, _
                                                                   strCompany, _
                                                                   strLocation, _
                                                                   strUserId, _
                                                                   strParam, _
                                                                   False, _
                                                                   objResult)
                    End If
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_LINE_ADD&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_PieceRatepaydet.aspx?payid=" & strSelectedPayId)
                End Try
            End If
            dgLineDet.EditItemIndex = -1
            onLoad_DisplayLine()
            Initialize()
        End If
    End Sub

    Sub Initialize()
        ddlEmployee.SelectedIndex = 0
        ddlLocation.SelectedIndex = 0
        ddlAccount.SelectedIndex = 0
        ddlBlock.SelectedIndex = 0
        ddlVehCode.SelectedIndex = 0
        ddlVehExpCode.SelectedIndex = 0
        ddlHarvInc.SelectedIndex = 0
        ddlDenda.SelectedIndex = 0
        txtLoseFruit.Text = "0"
        txtDendaQty.Text = "0"
        txtGroupRef.Text = ""
        txtRefDate.Text = ""
        txtMandays.Text = "0"
        txtRipeBunch.Text = "0"
        txtUnripeBunch.Text = "0"
        txtBunch.Text = "0"
        txtTotalUnits.Text = "0"
        txtRate.Text = "0"
        txtAmount.Text = "0"
        txtLnDesc.Text = ""
        btnAdd.Visible = True
        btnUpdate.Visible = False
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label
        Dim Delbutton As LinkButton
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strEmp As String
        Dim strAcc As String 
        Dim strPreBlk As String 
        Dim strBlk As String
        Dim strVeh As String 
        Dim strVehExp As String 
        Dim strLocCode As String
        Dim strDendaCode As String
        Dim strHarvInc As String        

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblPieceRateLnId")
        lblTxLnID.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblEmp")
        strEmp = lbl.Text.Trim
        BindEmployee(strEmp)

        lbl = E.Item.FindControl("lblChargeLocCode")
        strLocCode = lbl.Text.Trim
        BindLocationDropDownList(strLocCode)

        lbl = E.Item.FindControl("lblAccCode")
        strAcc = lbl.Text.Trim
        BindAccount(strAcc)

        lbl = E.Item.FindControl("lblHarvIncCode")
        strHarvInc = lbl.Text.Trim()
        BindHarvIncentive(strHarvInc)

        lbl = E.Item.FindControl("lblDendaCode")
        strDendaCode = lbl.Text.Trim
        BindDendaCode(strDendaCode)

        lbl = E.Item.FindControl("lblBlock")
        strBlk = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVeh")
        strVeh = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVehExp")
        strVehExp = lbl.Text.Trim
        lbl = E.Item.FindControl("lblGroupRef")
        txtGroupRef.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblRefDate")
        txtRefDate.Text = lbl.Text.Trim  
        lbl = E.Item.FindControl("lblMandays")
        txtMandays.Text = FormatNumber(Replace(Replace(lbl.Text.Trim,".",""),",","."),1)      
        lbl = E.Item.FindControl("lblRipeBunch")
        txtRipeBunch.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblUnripeBunch")
        txtUnripeBunch.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBunch")
        txtBunch.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblUnit")
        txtTotalUnits.Text = Replace(Replace(lbl.Text.Trim,".",""),",",".")   
        lbl = E.Item.FindControl("lblRate")
        txtRate.Text = Replace(Replace(lbl.Text.Trim,".",""),",",".")            
        lbl = E.Item.FindControl("lblAmount")
        txtAmount.Text = Replace(Replace(lbl.Text.Trim,".",""),",",".")          
        lbl = E.Item.FindControl("lblLnDesc")
        txtLnDesc.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblHUnit")
        txtTotalUnits.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblHLoseFruit")
        txtLoseFruit.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblHDendaQty")
        txtDendaQty.Text = lbl.Text.Trim
        GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

        If Not blnIsBalanceSheet Then
            If blnIsBlockRequire Then
                BindPreBlock(strAcc, strPreBlk)
                BindBlock(strAcc, strBlk)
                BindVehicle("", strVeh)
                BindVehicleExpense(True, strVehExp)
            Else
                BindPreBlock("", strPreBlk)
                BindBlock("", strBlk)
                BindVehicle("", strVeh)
                BindVehicleExpense(True, strVehExp)
            End If
            If blnIsVehicleRequire Then
                BindVehicle(strAcc, strVeh)
                BindVehicleExpense(False, strVehExp)
            End If
            If blnIsOthers Then
                lblVehicleOption.Text = True
                BindVehicle("%", strVeh)
                BindVehicleExpense(False, strVehExp)
            Else
                lblVehicleOption.Text = False
            End If
        ElseIf blnIsNurseryInd = True Then
            BindPreBlock(strAcc, strPreBlk)
            BindBlock(strAcc, strBlk)
            BindVehicle("", strVeh)
            BindVehicleExpense(True, strVehExp)
        Else
            BindPreBlock("", strPreBlk)
            BindBlock("", strBlk)
            BindVehicle("", strVeh)
            BindVehicleExpense(True, strVehExp)
        End If
 
        Delbutton = E.Item.FindControl("lbDelete")
        Delbutton.Visible = False

        onLoad_DisplayLine()

        If lblPieceRateID.Text <> "" Then
            btnAdd.Visible = False
            btnUpdate.Visible = True
        Else
            btnAdd.Visible = True
            btnUpdate.Visible = False
        End If

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        dgLineDet.EditItemIndex = -1
        onLoad_DisplayLine()

        Initialize()        
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "PR_CLSTRX_PIECERATEPAY_LINE_DEL"
        Dim strOpCode_TotalLine As String = "PR_CLSTRX_PIECERATEPAY_LINE_TOTAL_GET"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_PIECERATEPAY_TOTALAMT_UPD"
        Dim strOpCodes As String = strOpCode_DelLine & "|" & strOpCode_TotalLine & "|" & strOpCode_UpdID
        Dim strParam As String
        Dim lblText As Label
        Dim strPieceRateLnId As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = Convert.ToInt16(E.Item.ItemIndex)
        lblText = dgLineDet.Items.Item(Convert.ToInt16(E.Item.ItemIndex)).FindControl("lblPieceRateLnId")
        strPieceRateLnId = lblText.Text

        Try

            strParam = "|" & strPieceRateLnId & "|" & _
                        strSelectedPayId & "|||||||||" & _
                        objPRTrx.EnumPieceRateStatus.Active & "|||||||||||||" 'Add 1 more "|"


            intErrNo = objPRTrx.mtdUpdPieceRatePayLine(strOpCodes, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strParam, _
                                                       False, _
                                                       objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_LINE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_PieceRatepaydet.aspx?payid=" & strSelectedPayId)
        End Try

        onLoad_Display()
    End Sub


    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_PieceRatePayList.aspx")
    End Sub

    Function CheckLocBillParty () As Boolean
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strSearch As String
        Dim objBPLocDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strLocCode As String



            strLocCode = TRIM(ddlLocation.SelectedItem.Value)

            If Not (strLocCode = "" Or strLocCode = TRIM(strLocation)) Then
                strSearch = " AND BP.Status = '" & objGLSetup.EnumBillPartyStatus.Active & "'" & _
                            " AND BP.InterLocCode = '" & strLocCode & "'" 
                    
                Try
                    intErrNo = objGLSetup.mtdGetBPInterLoc(strOpCd_Get, strSearch, objBPLocDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_SETUP_BILLPARTY_GET&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_trx_pieceratedet.aspx")
                End Try

                If objBPLocDs.Tables(0).Rows.Count <= 0 Then
                    lblLocCodeErr.Text = strLocCode
                    return False
                End If
            End If

        return True
    End Function

    Sub BindHarvIncentive(ByVal pv_strHarvIncCode As String)
        Dim strOpCd As String = "PR_CLSSETUP_HARVINCENTIVE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By HarvIncCode ASC| Status = '" & objPRSetup.EnumHarvIncentiveStatus.Active & "' And LocCode = '" & strLocation & "' "

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try            
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   0, _
                                                   objHarvIncDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objHarvIncDs.Tables(0).Rows.Count - 1
            If objHarvIncDs.Tables(0).Rows(intCnt).Item("HarvIncCode") = Trim(pv_strHarvIncCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objHarvIncDs.Tables(0).NewRow()
        dr("HarvIncCode") = ""
        dr("_Description") = "Select Harvesting Incentive Scheme"        
        objHarvIncDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlHarvInc.DataSource = objHarvIncDs.Tables(0)
        ddlHarvInc.DataValueField = "HarvIncCode"
        ddlHarvInc.DataTextField = "_Description"
        ddlHarvInc.DataBind()
        ddlHarvInc.SelectedIndex = intSelectedIndex
    End Sub

    Sub ddlHarvInc_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strHarvIncCode As String = ddlHarvInc.SelectedItem.Value.Trim()
        Dim strOpCd As String = "PR_CLSSETUP_HARVINCENTIVE_GET"
        Dim strParam As String = strLocation & "|" & ddlHarvInc.SelectedItem.Value.Trim
        Dim objResult As New Object
        Dim strDivLabour As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Try            
            intErrNo = objPRSetup.mtdGetHarvIncentive(strOpCd, _
                                                      strParam, _
                                                      objResult, _
                                                      True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        If objResult.Tables(0).Rows.Count > 0 Then
            strDivLabour = objResult.Tables(0).Rows(0).Item("DivLabour")
            strPickerCategory = objResult.Tables(0).Rows(0).Item("PickerCategory")

            If strDivLabour = objPRSetup.EnumDivisionLabour.NonDOLUnpaid Then
                txtLoseFruit.Enabled = False
                lblLoseFruit.Enabled = False
            Else
                txtLoseFruit.Enabled = True
                lblLoseFruit.Enabled = True
            End If    
        End If
    End Sub


    Sub BindDendaCode(ByVal pv_strDendaCode As String)
        Dim strOpCd As String = "PR_CLSSETUP_DENDA_SEARCH"
        Dim dr As DataRow
        Dim strParam As String = "Order By D.DendaCode ASC|And D.Status = '" & objPRSetup.EnumDendaStatus.Active & "' And D.LocCode='" & strLocation & "' "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objPRSetup.EnumPayrollMasterType.Route, _
                                                   objDendaDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_DENDA_SEARCH&errmesg=" & Exp.ToString & "&redirect=PR/trx/PR_trx_TripList.aspx")
        End Try

        For intCnt = 0 To objDendaDs.Tables(0).Rows.Count - 1
            objDendaDs.Tables(0).Rows(intCnt).Item("DendaCode") = Trim(objDendaDs.Tables(0).Rows(intCnt).Item("DendaCode"))
            objDendaDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDendaDs.Tables(0).Rows(intCnt).Item("DendaCode")) & " (" & Trim(objDendaDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDendaDs.Tables(0).Rows(intCnt).Item("DendaCode") = Trim(pv_strDendaCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDendaDs.Tables(0).NewRow()
        dr("DendaCode") = ""
        dr("Description") = "Select Denda Code"
        objDendaDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDenda.DataSource = objDendaDs.Tables(0)
        ddlDenda.DataValueField = "DendaCode"
        ddlDenda.DataTextField = "Description"
        ddlDenda.DataBind()
        ddlDenda.SelectedIndex = intSelectedIndex
    End Sub

    Sub SearchPickerCategory(Byval pv_strHarvInc As String)
        Dim strOpCd As String = "PR_CLSSETUP_HARVINCENTIVE_GET"
        Dim strParam As String = strLocation & "|" & Trim(pv_strHarvInc)
        Dim objResult As New Object
        Dim strDivLabour As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Try            
            intErrNo = objPRSetup.mtdGetHarvIncentive(strOpCd, _
                                                      strParam, _
                                                      objResult, _
                                                      True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        If objResult.Tables(0).Rows.Count > 0 Then
            strDivLabour = objResult.Tables(0).Rows(0).Item("DivLabour")
            strPickerCategory = objResult.Tables(0).Rows(0).Item("PickerCategory")
        End If
    End Sub


End Class
