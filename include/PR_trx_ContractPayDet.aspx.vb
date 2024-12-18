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

Public Class PR_trx_ContractPayDet : Inherits Page

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents lblContractID As Label
    Protected WithEvents ddlContractor As DropDownList
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents ddlVehExpCode As DropDownList
    Protected WithEvents txtQty As TextBox
    Protected WithEvents txtQtyCompleted As TextBox
    Protected WithEvents txtAmt As TextBox
    Protected WithEvents txtPayAmt As TextBox
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents payid As HtmlInputHidden
    Protected WithEvents PrintBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents CloseBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblVehicleOption As Label
    Protected WithEvents lblErrContractor As Label
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblErrVehExp As Label
    Protected WithEvents lblErrNull As Label
    Protected WithEvents lblErrQtyClose As Label
    Protected WithEvents lblErrAmtClose As Label
    Protected WithEvents lblErrTotalAmount As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehExpense As Label
    Protected WithEvents hidAllowClose As HtmlInputHidden

    Dim objPRTrx As New agri.PR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String
    
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim objContractDs As New Object()
    Dim objContractLnDs As New Object()
    Dim objContractorDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objAccDs As New Object()
    Dim objBlkDs As New Object()
    Dim objVehDs As New Object()
    Dim objVehExpDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer

    Dim strSelectedPayId As String = ""
    Dim intStatus As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractPayment), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrContractor.Visible = False
            lblErrAccount.Visible = False
            lblErrBlock.Visible = False
            lblErrVehicle.Visible = False
            lblErrVehExp.Visible = False
            lblErrNull.Visible = False
            lblErrQtyClose.Visible = False
            lblErrAmtClose.Visible = False
            lblErrTotalAmount.Visible = False
            strSelectedPayId = Trim(IIf(Request.QueryString("payid") <> "", Request.QueryString("payid"), Request.Form("payid")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedPayId <> "" Then
                    payid.Value = strSelectedPayId
                    onLoad_Display()
                    onLoad_DisplayLine()
                    onLoad_BindButton()
                Else
                    BindContractor("")
                    BindAccount("")
                    BindBlock("", "")
                    BindVehicle("", "")
                    BindVehicleExpense(True, "")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfig) = True Then
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
            Else
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
            End If

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAYDET_GET_LANGCAP_COST_LEVEL&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_ContractPayList.aspx")
        End Try

        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

        lblErrAccount.Text = lblErrSelect.Text & lblAccount.Text
        lblErrBlock.Text = lblErrSelect.Text & lblBlock.Text
        lblErrVehicle.Text = lblErrSelect.Text & lblVehicle.Text
        lblErrVehExp.Text = lblErrSelect.Text & lblVehExpense.Text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAYDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_ContractPayList.aspx")
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
        ddlContractor.Enabled = False
        ddlAccount.Enabled = False
        ddlBlock.Enabled = False
        ddlVehCode.Enabled = False
        ddlVehExpCode.Enabled = False
        txtQty.Enabled = False
        txtQtyCompleted.Enabled = False
        txtAmt.Enabled = False
        txtPayAmt.Enabled = False
        txtRemark.Enabled = False
        SaveBtn.Visible = False
        CancelBtn.Visible = False
        CloseBtn.Visible = False
        tblSelection.Visible = False
        PrintBtn.Visible = False
        Select Case intStatus
            Case objPRTrx.EnumContractPayStatus.Active
                ddlContractor.Enabled = True
                ddlAccount.Enabled = True
                ddlBlock.Enabled = True
                ddlVehCode.Enabled = True
                ddlVehExpCode.Enabled = True
                txtQty.Enabled = True
                txtQtyCompleted.Enabled = True
                txtAmt.Enabled = True
                txtPayAmt.Enabled = True
                txtRemark.Enabled = True
                SaveBtn.Visible = True
                CancelBtn.Visible = True
                If hidAllowClose.Value = "yes" Then
                    CloseBtn.Visible = True
                End If
                tblSelection.Visible = True
                PrintBtn.Visible = True
            Case objPRTrx.EnumContractPayStatus.Closed, objPRTrx.EnumContractPayStatus.Closed
                PrintBtn.Visible = True
            Case objPRTrx.EnumContractPayStatus.Closed, objPRTrx.EnumContractPayStatus.Cancelled
            Case Else
                ddlContractor.Enabled = True
                ddlAccount.Enabled = True
                ddlBlock.Enabled = True
                ddlVehCode.Enabled = True
                ddlVehExpCode.Enabled = True
                txtQty.Enabled = True
                txtQtyCompleted.Enabled = True
                txtAmt.Enabled = True
                txtPayAmt.Enabled = True
                txtRemark.Enabled = True
                SaveBtn.Visible = True
                tblSelection.Visible = True
        End Select
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSTRX_CONTRACTPAY_GET"
        Dim strParam As String = strSelectedPayId
        Dim intErrNo As Integer

        Try
            intErrNo = objPRTrx.mtdGetContractPay(strOpCd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strAccMonth, _
                                                  strAccYear, _
                                                  strParam, _
                                                  objContractDs, _
                                                  True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        payid.Value = strSelectedPayId
        lblContractID.Text = strSelectedPayId
        txtQty.Text = objContractDs.Tables(0).Rows(0).Item("QtyReq").Trim()
        txtQtyCompleted.Text = objContractDs.Tables(0).Rows(0).Item("QtyComplete").Trim()
        txtAmt.Text = FormatNumber(objContractDs.Tables(0).Rows(0).Item("AmountReq"), 2, True, False, False)
        txtPayAmt.Text = FormatNumber(objContractDs.Tables(0).Rows(0).Item("AmountComplete"), 2, True, False, False)
        txtRemark.Text = Trim(objContractDs.Tables(0).Rows(0).Item("Remark"))
        lblPeriod.Text = Trim(objContractDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objContractDs.Tables(0).Rows(0).Item("AccYear"))
        intStatus = CInt(Trim(objContractDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objContractDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objPRTrx.mtdGetContractPayStatus(Trim(objContractDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objContractDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objContractDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objContractDs.Tables(0).Rows(0).Item("UserName"))
        lblPrintDate.Text = objGlobal.GetLongDate(objContractDs.Tables(0).Rows(0).Item("PrintDate"))

        BindContractor("")
        BindAccount(Trim(objContractDs.Tables(0).Rows(0).Item("AccCode")))
        BindVehicle(Trim(objContractDs.Tables(0).Rows(0).Item("AccCode")), Trim(objContractDs.Tables(0).Rows(0).Item("VehCode")))

        If Trim(objContractDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.NonVehicle Then
            BindBlock(Trim(objContractDs.Tables(0).Rows(0).Item("AccCode")), Trim(objContractDs.Tables(0).Rows(0).Item("BlkCode")))
            BindVehicleExpense(True, Trim(objContractDs.Tables(0).Rows(0).Item("VehExpenseCode")))
        ElseIf Trim(objContractDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.VehicleDistribution Then
            BindBlock("", Trim(objContractDs.Tables(0).Rows(0).Item("BlkCode")))
            BindVehicleExpense(False, Trim(objContractDs.Tables(0).Rows(0).Item("VehExpenseCode")))
        ElseIf Trim(objContractDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.Others Then
            BindBlock(Trim(objContractDs.Tables(0).Rows(0).Item("AccCode")), Trim(objContractDs.Tables(0).Rows(0).Item("BlkCode")))
            BindVehicleExpense(False, Trim(objContractDs.Tables(0).Rows(0).Item("VehExpenseCode")))
        End If
    End Sub

    Sub onLoad_DisplayLine()
        Dim strOpCd As String = "PR_CLSTRX_CONTRACTPAY_LINE_GET"
        Dim strParam As String = strSelectedPayId
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dblAmount As Decimal = 0
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim intFlag As Integer = 0

        Try
            intErrNo = objPRTrx.mtdGetContractPay(strOpCd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strAccMonth, _
                                                  strAccYear, _
                                                  strParam, _
                                                  objContractLnDs, _
                                                  True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_LINE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objContractLnDs.Tables(0).Rows.Count - 1
            objContractLnDs.Tables(0).Rows(intCnt).Item("ContractorCode") = Trim(objContractLnDs.Tables(0).Rows(intCnt).Item("ContractorCode"))
            objContractLnDs.Tables(0).Rows(intCnt).Item("ContractorName") = Trim(objContractLnDs.Tables(0).Rows(intCnt).Item("ContractorName"))
            dblAmount += objContractLnDs.Tables(0).Rows(intCnt).Item("Amount")
        Next

        dgLineDet.DataSource = objContractLnDs.Tables(0)
        dgLineDet.DataBind()

        
        lblTotalAmount.Text = ObjGlobal.GetIDDecimalSeparator(dblAmount)

        If intStatus = objPRTrx.EnumContractPayStatus.Active Then
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = True
                lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                lbl = dgLineDet.Items.Item(intCnt).FindControl("lblPayrollInd")
                If lbl.Text = objHRTrx.EnumEmpPayrollInd.Yes Then
                    intFlag = intFlag + 1
                End If
            Next
        Else
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = False
            Next
        End If
        If intFlag = 0 Then
            hidAllowClose.Value = "yes"
        End If
    End Sub

    Sub BindContractor(ByVal pv_strContractor As String)
        Dim strOpCd As String = "PR_CLSTRX_CONTRACTPAY_CONTRACTORLIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "ORDER BY O.ContractorCode ASC|O.Status = '" & objPRSetup.EnumContrListStatus.Active & _
                                 "' AND O.ContractorCode NOT IN (SELECT LN.ContractorCode FROM PR_CONTRACTPAYLN LN WHERE LN.ContractID = '" & strSelectedPayId & "')"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   0, _
                                                   objContractorDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAY_CONTRACTOR_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objContractorDs.Tables(0).Rows.Count - 1
            objContractorDs.Tables(0).Rows(intCnt).Item("ContractorCode") = Trim(objContractorDs.Tables(0).Rows(intCnt).Item("ContractorCode"))
            objContractorDs.Tables(0).Rows(intCnt).Item("Name") = Trim(objContractorDs.Tables(0).Rows(intCnt).Item("ContractorCode")) & " (" & Trim(objContractorDs.Tables(0).Rows(intCnt).Item("Name")) & ")"
            If objContractorDs.Tables(0).Rows(intCnt).Item("ContractorCode") = Trim(pv_strContractor) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objContractorDs.Tables(0).NewRow()
        dr("ContractorCode") = ""
        dr("Name") = "Select Contractor"
        objContractorDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlContractor.DataSource = objContractorDs.Tables(0)
        ddlContractor.DataValueField = "ContractorCode"
        ddlContractor.DataTextField = "Name"
        ddlContractor.DataBind()
        ddlContractor.SelectedIndex = intSelectedIndex
    End Sub


    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAY_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(objAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = lblSelect.Text & lblAccount.Text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = objAccDs.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "Description"
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAY_GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            If CInt(_objAccDs.Tables(0).Rows(0).Item("AccType")) = objGLSetup.EnumAccountType.BalanceSheet Then
                pr_IsBalanceSheet = True
                If CInt(_objAccDs.Tables(0).Rows(0).Item("NurseryInd")) = objGLSetup.EnumNurseryAccount.Yes Then
                    pr_IsNurseryInd = True
                End If
            End If
            If CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.NonVehicle Then
                pr_IsBlockRequire = True
            ElseIf CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.VehicleDistribution Then
                pr_IsVehicleRequire = True
            ElseIf CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.Others Then
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

        GetAccountDetails(ddlAccount.SelectedItem.Value, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

        If Not blnIsBalanceSheet Then
            If blnIsBlockRequire Then
                BindBlock(ddlAccount.SelectedItem.Value, ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            Else
                BindBlock("", ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            End If
            If blnIsVehicleRequire Then
                BindVehicle(ddlAccount.SelectedItem.Value, ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(False, ddlVehExpCode.SelectedItem.Value)
            End If
            If blnIsOthers Then
                lblVehicleOption.Text = True
                BindVehicle("%", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(False, ddlVehExpCode.SelectedItem.Value)
            Else
                lblVehicleOption.Text = False
            End If
        ElseIf blnIsNurseryInd = True Then
            BindBlock(ddlAccount.SelectedItem.Value, ddlBlock.SelectedItem.Value)
            BindVehicle("", ddlVehCode.SelectedItem.Value)
            BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
        Else
            BindBlock("", ddlBlock.SelectedItem.Value)
            BindVehicle("", ddlVehCode.SelectedItem.Value)
            BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
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
                strOpCd = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAY_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & lblBlock.Text
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "Description"
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
            strParam = "|AccCode = '" & pv_strAccCode & "' AND LocCode = '" & Session("SS_LOCATION") & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'"
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   objVehDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAY_VEH_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objVehDs.Tables(0).Rows.Count - 1
            objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode"))
            objVehDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode")) & " (" & Trim(objVehDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(pv_strVehCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objVehDs.Tables(0).NewRow()
        dr("VehCode") = ""
        dr("Description") = lblSelect.Text & lblVehicle.Text
        objVehDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehCode.DataSource = objVehDs.Tables(0)
        ddlVehCode.DataValueField = "VehCode"
        ddlVehCode.DataTextField = "Description"
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAY_VEHEXPENSE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objVehExpDs.Tables(0).Rows.Count - 1
            objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode"))
            objVehExpDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode")) & " (" & Trim(objVehExpDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(pv_strVehExpCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objVehExpDs.Tables(0).NewRow()
        dr("VehExpenseCode") = ""
        dr("Description") = lblSelect.Text & lblVehExpense.Text
        objVehExpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehExpCode.DataSource = objVehExpDs.Tables(0)
        ddlVehExpCode.DataValueField = "VehExpenseCode"
        ddlVehExpCode.DataTextField = "Description"
        ddlVehExpCode.DataBind()
        ddlVehExpCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub InsertRecord()
        Dim objContractId As String
        Dim strOpCd_Add As String = "PR_CLSTRX_CONTRACTPAY_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_CONTRACTPAY_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_CONTRACTPAY_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_CONTRACTPAY_STATUS_UPD"
        Dim strOpCd As String
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim strAccCode As String = Request.Form("ddlAccount")
        Dim strBlkCode As String = Request.Form("ddlBlock")
        Dim strVehCode As String = Request.Form("ddlVehCode")
        Dim strVehExpense As String = Request.Form("ddlVehExpCode")
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean

        If strAccCode = "" Then
            lblErrAccount.Visible = True
            Exit Sub
        End If

        GetAccountDetails(strAccCode, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

        If strBlkCode = "" And blnIsBlockRequire = True And blnIsBalanceSheet = False Then
            lblErrBlock.Visible = True
            Exit Sub
        ElseIf strBlkCode = "" And blnIsBalanceSheet = True And blnIsNurseryInd = True Then
            lblErrBlock.Visible = True
            Exit Sub
        ElseIf strVehCode = "" And blnIsVehicleRequire = True Then
            lblErrVehicle.Visible = True
            Exit Sub
        ElseIf strVehExpense = "" And blnIsVehicleRequire = True Then
            lblErrVehExp.Visible = True
            Exit Sub
        ElseIf strVehCode <> "" And strVehExpense = "" And lblVehicleOption.Text = True Then
            lblErrVehExp.Visible = True
            Exit Sub
        ElseIf strVehCode = "" And strVehExpense <> "" And lblVehicleOption.Text = True Then
            lblErrVehicle.Visible = True
            Exit Sub
        ElseIf (Trim(txtQty.Text) = "" And Trim(txtAmt.Text) = "") Then
            lblErrNull.Visible = True
            Exit Sub
        End If

        strSelectedPayId = Trim(payid.Value)
        strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)

        strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRContractPay) & "|" & _
                   strSelectedPayId & "|" & _
                   strAccCode & "|" & _
                   strBlkCode & "|" & _
                   strVehCode & "|" & _
                   strVehExpense & "|" & _
                   Trim(txtQty.Text) & "|" & _
                   Trim(txtAmt.Text) & "|" & _
                   Trim(txtQtyCompleted.Text) & "|" & _
                   Trim(txtPayAmt.Text) & "|" & _
                   Trim(txtRemark.Text) & "|" & _
                   objPRTrx.EnumContractPayStatus.Active & "|" & _
                   objPRTrx.EnumPayrollPosted.No
        Try
            intErrNo = objPRTrx.mtdUpdContractPay(strOpCd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                False, _
                                                objContractId)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_CONTRACTPAY_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_contractpaydet.aspx")
        End Try
        strSelectedPayId = objContractId
        payid.Value = strSelectedPayId
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_AddLine As String = "PR_CLSTRX_CONTRACTPAY_LINE_ADD"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_CONTRACTPAY_STATUS_UPD"
        Dim strParam As String
        Dim intErrNo As Integer

        If ddlContractor.SelectedItem.Value = "" Then
            lblErrContractor.Visible = True
            Exit Sub
        Else
            InsertRecord()

            If strSelectedPayId = "" Then
                Exit Sub
            Else
                Try
                    strParam = strSelectedPayId & "|" & _
                               ddlContractor.SelectedItem.Value & "|" & _
                               txtAmount.Text & "|" & _
                               objPRTrx.EnumContractPayStatus.Active

                    intErrNo = objPRTrx.mtdUpdContractPayLine(strOpCode_UpdID, _
                                                            strOpCode_AddLine, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            False, _
                                                            True, _
                                                            objResult)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAY_LINE_ADD&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_contractpaydet.aspx?payid=" & strSelectedPayId)
                End Try
            End If
            onLoad_Display()
            onLoad_DisplayLine()
            onLoad_BindButton()
        End If
    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSTRX_CONTRACTPAY_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_CONTRACTPAY_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_CONTRACTPAY_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_CONTRACTPAY_STATUS_UPD"
        Dim strOpCd As String
        Dim objContractId As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strParam As String = ""

        If ddlAccount.SelectedItem.Value = "" Then
            lblErrAccount.Visible = True
            Exit Sub
        ElseIf (ddlBlock.Items.Count > 1) And (ddlBlock.SelectedItem.Value = "") Then
            lblErrBlock.Visible = True
            Exit Sub
        ElseIf (ddlVehCode.Items.Count > 1) And (ddlVehCode.SelectedItem.Value = "") And (lblVehicleOption.Text = False) Then
            lblErrVehicle.Visible = True
            Exit Sub
        ElseIf (Trim(txtQty.Text) = "" And Trim(txtAmt.Text) = "") Then
            lblErrNull.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            InsertRecord()
        ElseIf strCmdArgs = "Cancel" Then
            strParam = strSelectedPayId & "|" & objPRTrx.EnumContractPayStatus.Cancelled
            Try
                intErrNo = objPRTrx.mtdUpdContractPay(strOpCd_Sts, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    True, _
                                                    objContractId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_CONTRACTPAY_CANCEL&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_contractpaydet.aspx?payid=" & strSelectedPayId)
            End Try

        ElseIf strCmdArgs = "Close" Then
            If ddlAccount.SelectedItem.Value = "" Then
                lblErrAccount.Visible = True
                Exit Sub
            ElseIf (ddlBlock.Items.Count > 1) And (ddlBlock.SelectedItem.Value = "") Then
                lblErrBlock.Visible = True
                Exit Sub
            ElseIf (ddlVehCode.Items.Count > 1) And (ddlVehCode.SelectedItem.Value = "") And (lblVehicleOption.Text = False) Then
                lblErrVehicle.Visible = True
                Exit Sub
            ElseIf (Trim(txtQty.Text) = "" And Trim(txtAmt.Text) = "") Then
                lblErrNull.Visible = True
                Exit Sub
            ElseIf Trim(txtQty.Text) <> "" And Trim(txtQtyCompleted.Text) = "" Then
                lblErrQtyClose.Visible = True
                Exit Sub
            ElseIf Trim(txtAmt.Text) = "" Or Trim(txtPayAmt.Text) = "" Then
                lblErrAmtClose.Visible = True
                Exit Sub
            Else
                If CDbl(txtPayAmt.Text) <> CDbl(Replace(Replace(lblTotalAmount.Text,".",""),",",".")) Then
                    lblErrTotalAmount.Visible = True
                    Exit Sub
                End If
            End If

            strParam = strSelectedPayId & "|" & objPRTrx.EnumContractPayStatus.Closed
            Try
                intErrNo = objPRTrx.mtdUpdContractPay(strOpCd_Sts, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    True, _
                                                    objContractId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_CONTRACTPAY_CLOSE&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_contractpaydet.aspx?payid=" & strSelectedPayId)
            End Try
        End If

        If strSelectedPayId <> "" Then
            onLoad_Display()
            onLoad_DisplayLine()
            onLoad_BindButton()
        End If
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "PR_CLSTRX_CONTRACTPAY_LINE_DEL"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_CONTRACTPAY_STATUS_UPD"
        Dim strParam As String
        Dim lblText As Label
        Dim strContractorCode As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblContractorCode")
        strContractorCode = lblText.Text

        Try
            strParam = strSelectedPayId & "|" & strContractorCode & "||" & objPRTrx.EnumContractPayStatus.Active
            intErrNo = objPRTrx.mtdUpdContractPayLine(strOpCode_UpdID, _
                                                    strOpCode_DelLine, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    False, _
                                                    False, _
                                                    objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAY_LINE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_contractpaydet.aspx?payid=" & strSelectedPayId)
        End Try
        onLoad_Display()
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String
        Dim intStatus As Integer
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String
        Dim strContractId As String
        Dim intErrNo As Integer
        Dim AccountTag As String
        Dim BlockTag As String
        Dim VehicleTag As String
        Dim VehExpenseCodeTag As String

        strContractId = Trim(lblContractID.Text)

        strUpdString = "where ContractId = '" & strContractId & "'"
        strStatus = Trim(lblStatus.Text)
        intStatus = CInt(Trim(lblHiddenSts.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strTable = "PR_CONTRACTPAY"
        strSortLine = ""

        AccountTag = lblAccount.Text
        BlockTag = lblBlock.Text
        VehicleTag = lblVehicle.Text
        VehExpenseCodeTag = lblVehExpense.Text
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                        strUpdString, _
                                                        strTable, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_SHARE_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_contractpaydet.aspx")
                End Try
                onLoad_Display()
                onLoad_DisplayLine()
                onLoad_BindButton()
            Else
                strStatus = strStatus & " (re-printed)"
            End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PR_Rpt_ContractPayDet.aspx?strContractId=" & strContractId & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       "&AccountTag=" & AccountTag & _
                       "&BlockTag=" & BlockTag & _
                       "&VehicleTag=" & VehicleTag & _
                       "&VehExpenseCodeTag=" & VehExpenseCodeTag & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_ContractPayList.aspx")
    End Sub


End Class
