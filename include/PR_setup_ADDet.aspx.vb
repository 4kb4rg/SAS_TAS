Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.PWSystem
Imports agri.Admin
Imports agri.PR
Imports agri.GL
Imports agri.GlobalHdl

Public Class PR_setup_ADdet : Inherits Page

    Protected WithEvents txtADCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents ddlADGroup As DropDownList
    Protected WithEvents ddlType As DropDownList
    Protected WithEvents ddlDefAccCode As DropDownList
    Protected WithEvents ddlDefBlkCode As DropDownList
    Protected WithEvents cbJamsContribute As CheckBox
    Protected WithEvents cbTaxContribute As CheckBox
    Protected WithEvents ddlPaySlip As DropDownList

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents adcode As HtmlInputHidden
    Protected WithEvents btnFind1 As HtmlInputButton
    Protected WithEvents btnFind2 As HtmlInputButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupADCode As Label
    Protected WithEvents lblErrADGroup As Label
    Protected WithEvents lblErrADType As Label
    Protected WithEvents lblErrDefAccCode As Label
    Protected WithEvents lblErrDefBlkCode As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblDefAcc As Label
    Protected WithEvents lblDefBlock As Label
    Protected WithEvents lblDefault As Label
    Protected WithEvents lblJamsostek As Label


    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents ddlVehExpCode As DropDownList
    Protected WithEvents lblVehicle as Label
    Protected WithEvents lblVehExpense as Label
    Protected WithEvents cbMaternityAllowance As CheckBox
    Protected WithEvents cbBonus As CheckBox
    Protected WithEvents cbTHR As CheckBox
    Protected WithEvents cbHouseRent As CheckBox
    Protected WithEvents cbMedical As CheckBox
    Protected WithEvents cbTax As CheckBox
    Protected WithEvents cbDanaPensiun As CheckBox

    Protected WithEvents cbTransport As CheckBox
    Protected WithEvents cbMeal As CheckBox
    Protected WithEvents cbLeave As CheckBox
    Protected WithEvents cbAirBus As CheckBox
    Protected WithEvents cbRelocation As CheckBox

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdminUOM As New agri.Admin.clsUOM()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objADDs As New Object()
    Dim objADLnDs As New Object()
    Dim objADGroupDs As New Object()
    Dim objDefAccDs As New Object()
    Dim objDefBlkDs As New Object()
    Dim objUOMDs As New Object()
    Dim objPayADDs As New Object()
    Dim objLocDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strSelectedADCode As String = ""
    Dim intStatus As Integer
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAllowanceDeduction), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrDupADCode.Visible = False
            lblErrADGroup.Visible = False
            lblErrADType.Visible = False
            lblErrDefAccCode.Visible = False
            lblErrDefBlkCode.Visible = False
            
            strSelectedADCode = Trim(IIf(Request.QueryString("adcode") <> "", Request.QueryString("adcode"), Request.Form("adcode")))
            intStatus = CInt(lblHiddenSts.Text)
            If Not IsPostBack Then
                If strSelectedADCode <> "" Then
                    adcode.Value = strSelectedADCode
                    onLoad_Display()
                Else
                    onLoad_BindADGroup("")
                    onLoad_BindADType("")
                    onLoad_BindAccCode("")
                    onLoad_BindBlkCode("", "")
                    onLoad_BindADCode("")
                    onLoad_BindButton()
                    BindVehicle("","")
                    BindVehicleExpense(False, "")
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblDefAcc.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfig) = True Then
            lblDefBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        Else
            lblDefBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        End If
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

        lblErrDefAccCode.Text = lblErrSelect.Text & lblDefault.Text & " " & lblDefAcc.Text
        lblJamsostek.Text = GetCaption(objLangCap.EnumLangCap.Jamsostek) 
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PR/Setup/PR_setup_ADDet.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function


    Sub onLoad_BindButton()
        txtADCode.Enabled = False
        txtDesc.Enabled = False
        ddlADGroup.Enabled = False
        ddlType.Enabled = False
        ddlDefAccCode.Enabled = False
        ddlDefBlkCode.Enabled = False
        cbJamsContribute.Enabled = False
        cbTaxContribute.Enabled = False
        ddlPaySlip.Enabled = False
        cbMaternityAllowance.Enabled = False
        cbBonus.Enabled = False
        cbTHR.Enabled = False
        ddlVehExpCode.Enabled = False
        ddlVehCode.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        btnFind1.Disabled = False
        btnFind2.Disabled = False

        Select Case intStatus
            Case objPRSetup.EnumADStatus.Active
                txtDesc.Enabled = True
                ddlADGroup.Enabled = True
                ddlType.Enabled = True
                ddlDefAccCode.Enabled = True
                ddlDefBlkCode.Enabled = True
                cbJamsContribute.Enabled = True
                cbTaxContribute.Enabled = True
                ddlPaySlip.Enabled = True
                cbMaternityAllowance.Enabled = True
                cbBonus.Enabled = True
                cbTHR.Enabled = True
                ddlVehExpCode.Enabled = True
                ddlVehCode.Enabled = True

                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objPRSetup.EnumADStatus.Deleted
                btnFind1.Disabled = True
                btnFind2.Disabled = True
                UnDelBtn.Visible = True
            Case Else
                txtADCode.Enabled = True
                txtDesc.Enabled = True
                ddlADGroup.Enabled = True
                ddlType.Enabled = True
                ddlDefAccCode.Enabled = True
                ddlDefBlkCode.Enabled = True
                cbJamsContribute.Enabled = True
                cbTaxContribute.Enabled = True
                ddlPaySlip.Enabled = True
                cbMaternityAllowance.Enabled = True
                cbBonus.Enabled = True
                cbTHR.Enabled = True
                ddlVehExpCode.Enabled = True
                ddlVehCode.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_AD_GET"
        Dim strParam As String = strSelectedADCode        
        Dim intErrNo As Integer

        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCd, _
                                           strParam, _
                                           objADDs, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objADDs.Tables(0).Rows.Count > 0 Then
            txtADCode.Text = objADDs.Tables(0).Rows(0).Item("ADCode").Trim()
            txtDesc.Text = objADDs.Tables(0).Rows(0).Item("Description").Trim()
            cbJamsContribute.Checked = IIf(CInt(objADDs.Tables(0).Rows(0).Item("JamsDeductInd")) = objPRSetup.EnumJamsDeductInd.Yes, True, False)
            cbTaxContribute.Checked = IIf(CInt(objADDs.Tables(0).Rows(0).Item("TaxDeductInd")) = objPRSetup.EnumTaxDeductInd.Yes, True, False)
            cbMaternityAllowance.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("MaternityInd")),objADDs.Tables(0).Rows(0).Item("MaternityInd"),"2")) = objPRSetup.EnumMaternityInd.Yes, True, False)
            cbBonus.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("BonusInd")),objADDs.Tables(0).Rows(0).Item("BonusInd"),"2")) = objPRSetup.EnumBonus.Yes, True, False)
            cbTHR.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("THRInd")),objADDs.Tables(0).Rows(0).Item("THRInd"),"2")) = objPRSetup.EnumTHRInd.Yes, True, False)
            cbHouseRent.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("HouseInd")),objADDs.Tables(0).Rows(0).Item("HouseInd"),"2")) = objPRSetup.EnumHouseInd.Yes, True, False)
            cbMedical.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("MedicalInd")),objADDs.Tables(0).Rows(0).Item("MedicalInd"),"2")) = objPRSetup.EnumMedicalInd.Yes, True, False)
            cbTax.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("TaxInd")),objADDs.Tables(0).Rows(0).Item("TaxInd"),"2")) = objPRSetup.EnumTaxInd.Yes, True, False)
            cbDanaPensiun.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("DanaPensiunInd")),objADDs.Tables(0).Rows(0).Item("DanaPensiunInd"),"2")) = objPRSetup.EnumDanaPensiunInd.Yes, True, False)
 
            cbTransport.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("TransportInd")),objADDs.Tables(0).Rows(0).Item("TransportInd"),"2")) = objPRSetup.EnumTransportInd.Yes, True, False)
            cbMeal.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("MealInd")),objADDs.Tables(0).Rows(0).Item("MealInd"),"2")) = objPRSetup.EnumMealInd.Yes, True, False)
            cbLeave.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("LeaveInd")),objADDs.Tables(0).Rows(0).Item("LeaveInd"),"2")) = objPRSetup.EnumLeaveInd.Yes, True, False)
            cbAirBus.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("AirBusInd")),objADDs.Tables(0).Rows(0).Item("AirBusInd"),"2")) = objPRSetup.EnumAirBusInd.Yes, True, False)
            cbRelocation.Checked = IIf(CInt(IIF(IsNumeric(objADDs.Tables(0).Rows(0).Item("RelocationInd")),objADDs.Tables(0).Rows(0).Item("RelocationInd"),"2")) = objPRSetup.EnumRelocationInd.Yes, True, False)
            
            intStatus = CInt(objADDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenSts.Text = objADDs.Tables(0).Rows(0).Item("Status").Trim()
            lblStatus.Text = objPRSetup.mtdGetADStatus(objADDs.Tables(0).Rows(0).Item("Status").Trim())
            lblDateCreated.Text = objGlobal.GetLongDate(objADDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objADDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objADDs.Tables(0).Rows(0).Item("UserName")
            onLoad_BindADGroup(objADDs.Tables(0).Rows(0).Item("ADGrpCode").Trim())
            onLoad_BindADType(objADDs.Tables(0).Rows(0).Item("ADType").Trim())
            onLoad_BindAccCode(objADDs.Tables(0).Rows(0).Item("DefAccCode").Trim())
            onLoad_BindBlkCode(objADDs.Tables(0).Rows(0).Item("DefAccCode").Trim(), objADDs.Tables(0).Rows(0).Item("DefBlockCode").Trim())
            onLoad_BindADCode(objADDs.Tables(0).Rows(0).Item("PayslipADCode").Trim())
            BindVehicle(objADDs.Tables(0).Rows(0).Item("DefAccCode").Trim(), objADDs.Tables(0).Rows(0).Item("VehCode").Trim())
            BindVehicleExpense(False,objADDs.Tables(0).Rows(0).Item("ExpenseCode").Trim())
            onLoad_BindButton()
            Change_ADType(objADDs.Tables(0).Rows(0).Item("ADType").Trim())
        End If
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer, _
                          ByRef pr_strNurseryInd As Integer)

        Dim _objAccDs As New DataSet()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_ADCODE_GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString() & "&redirect=PR/Setup/PR_setup_ADDet.aspx")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd"))      
        End If
    End Sub

    Sub onLoad_BindADGroup(ByVal pv_strADGroup As String)
        Dim strOpCd As String = "PR_CLSSETUP_ADGRP_LIST_GET"
        Dim strParam As String = "Order By ADGrp.ADGrpCode ASC|And ADGrp.Status = '" & objPRSetup.EnumADGrpStatus.Active & "'"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objPRSetup.EnumPayrollMasterType.ADGroup, _
                                                   objADGroupDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_ADGROUP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objADGroupDs.Tables(0).Rows.Count - 1
            objADGroupDs.Tables(0).Rows(intCnt).Item("AdGrpCode") = Trim(objADGroupDs.Tables(0).Rows(intCnt).Item("AdGrpCode"))
            objADGroupDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objADGroupDs.Tables(0).Rows(intCnt).Item("AdGrpCode")) & " (" & Trim(objADGroupDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objADGroupDs.Tables(0).Rows(intCnt).Item("AdGrpCode") = Trim(pv_strADGroup) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objADGroupDs.Tables(0).NewRow()
        dr("AdGrpCode") = ""
        dr("Description") = "Select AD Group"
        objADGroupDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlADGroup.DataSource = objADGroupDs.Tables(0)
        ddlADGroup.DataValueField = "AdGrpCode"
        ddlADGroup.DataTextField = "Description"
        ddlADGroup.DataBind()
        ddlADGroup.SelectedIndex = intSelectedIndex
    End Sub

    Sub Change_ADType(ByVal pv_strType As String)
        If pv_strType <> "" Then
            If CInt(pv_strType) = objPRSetup.EnumADType.Deduction Then
                cbJamsContribute.Checked = False
                cbJamsContribute.Enabled = False
                cbTaxContribute.Checked = False
                cbTaxContribute.Enabled = False
                cbMaternityAllowance.Checked = False
                cbMaternityAllowance.Enabled = False
                cbBonus.Checked = False
                cbBonus.Enabled = False
                cbTHR.Checked = False
                cbTHR.Enabled = False
                cbHouseRent.Enabled = False
                cbMedical.Enabled = False
                cbTax.Enabled = False
                cbDanaPensiun.Enabled = False

                cbMeal.Enabled = False
                cbLeave.Enabled = False
                cbTransport.Enabled = False
                cbAirBus.Enabled = False
                cbRelocation.Enabled = False
            Else
                cbJamsContribute.Enabled = True
                cbTaxContribute.Enabled = True
                cbMaternityAllowance.Enabled = True
                cbBonus.Enabled = True
                cbTHR.Enabled = True

                cbHouseRent.Enabled = True
                cbMedical.Enabled = True
                cbTax.Enabled = True
                cbDanaPensiun.Enabled = True

                cbMeal.Enabled = True
                cbLeave.Enabled = True
                cbTransport.Enabled = True
                cbAirBus.Enabled = True
                cbRelocation.Enabled = True
            End If
        Else
            cbJamsContribute.Enabled = True
            cbTaxContribute.Enabled = True
            cbMaternityAllowance.Enabled = True
            cbBonus.Enabled = True
            cbTHR.Enabled = True

            cbHouseRent.Enabled = True
            cbMedical.Enabled = True
            cbTax.Enabled = True
            cbDanaPensiun.Enabled = True

            cbMeal.Enabled = True
            cbLeave.Enabled = True
            cbTransport.Enabled = True
            cbAirBus.Enabled = True
            cbRelocation.Enabled = True
        End If
    End Sub

    Sub onChange_ADType(Sender As Object, E As EventArgs)
        Change_ADType(ddlType.SelectedItem.Value)
    End Sub

    Sub onLoad_BindADType(ByVal pv_strADType As String)
        ddlType.Items.Add(New ListItem("Select AD Type", ""))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetADType(objPRSetup.EnumADType.Allowance), objPRSetup.EnumADType.Allowance))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetADType(objPRSetup.EnumADType.Deduction), objPRSetup.EnumADType.Deduction))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetADType(objPRSetup.EnumADType.RPTItem), objPRSetup.EnumADType.RPTItem))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetADType(objPRSetup.EnumADType.MemoItem), objPRSetup.EnumADType.MemoItem))
        
        If Trim(pv_strADType) = "" Then
            ddlType.SelectedValue = ""
        Else
            ddlType.SelectedValue = CInt(Trim(pv_strADType))
        End If
    End Sub


    Sub onLoad_BindAccCode(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & _
                                 objGLSetup.EnumAccountCodeStatus.Active & _
                                 "' And ((ACC.AccType = '" & objGLSetup.EnumAccountType.ProfitAndLost & "'" & _
                                 " And ACC.AccPurpose = '" & objGLSetup.EnumAccountPurpose.NonVehicle & "') OR " & _
                                 "(ACC.AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "')) "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objDefAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_DEFACC_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objDefAccDs.Tables(0).Rows.Count - 1
            objDefAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objDefAccDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objDefAccDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDefAccDs.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(objDefAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDefAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDefAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = lblSelect.text & lblDefAcc.text
        objDefAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDefAccCode.DataSource = objDefAccDs.Tables(0)
        ddlDefAccCode.DataValueField = "AccCode"
        ddlDefAccCode.DataTextField = "Description"
        ddlDefAccCode.DataBind()
        ddlDefAccCode.SelectedIndex = intSelectedIndex
        ddlDefAccCode.AutoPostBack = True
    End Sub

    Sub onSelect_DefAccount(Sender As Object, E As EventArgs)
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")

        onLoad_BindBlkCode(Request.Form("ddlDefAccCode"), Request.Form("ddlDefBlkCode"))
        BindVehicle(Request.Form("ddlDefAccCode"),strVeh)
        BindVehicleExpense(False, strVehExp)
    End Sub

    Sub onLoad_BindBlkCode(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
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
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            End If

            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                    strParam, _
                                                    objDefBlkDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_DEFBLK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objDefBlkDs.Tables(0).Rows.Count - 1
            objDefBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objDefBlkDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objDefBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDefBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objDefBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDefBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDefBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.text & lblDefBlock.text
        objDefBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDefBlkCode.DataSource = objDefBlkDs.Tables(0)
        ddlDefBlkCode.DataValueField = "BlkCode"
        ddlDefBlkCode.DataTextField = "Description"
        ddlDefBlkCode.DataBind()
        ddlDefBlkCode.SelectedIndex = intSelectedIndex

    End Sub

    Sub onLoad_BindADCode(ByVal pv_strPayslipAD As String)
        Dim strOpCd As String = "PR_CLSSETUP_AD_SEARCH"
        Dim dr As DataRow
        Dim strParam As String = "||" & objPRSetup.EnumADStatus.Active & "||AD.ADCode|ASC"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCd, _
                                           strParam, _
                                           objPayADDs, _
                                           False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_PAYSLIP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objPayADDs.Tables(0).Rows.Count - 1
            objPayADDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(objPayADDs.Tables(0).Rows(intCnt).Item("ADCode"))
            objPayADDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objPayADDs.Tables(0).Rows(intCnt).Item("ADCode")) & " (" & Trim(objPayADDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objPayADDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(pv_strPayslipAD) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objPayADDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("Description") = "Select Payslip AD Code"
        objPayADDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPaySlip.DataSource = objPayADDs.Tables(0)
        ddlPaySlip.DataValueField = "ADCode"
        ddlPaySlip.DataTextField = "Description"
        ddlPaySlip.DataBind()
        ddlPaySlip.SelectedIndex = intSelectedIndex
    End Sub

    Sub InsertADRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_AD_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_AD_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_AD_GET"
        Dim strOpCd As String
        Dim strDefAcc As String = Request.Form("ddlDefAccCode")
        Dim strDefBlk As String = Request.Form("ddlDefBlkCode")
        Dim strPaySlip As String = Request.Form("ddlPaySlip")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")
        Dim intErrNo As Integer
        Dim strParam As String = ""

        strDefAcc = IIf(strDefAcc = "", ddlDefAccCode.SelectedItem.Value, strDefAcc)
        strDefBlk = IIf(strDefBlk = "", ddlDefBlkCode.SelectedItem.Value, strDefBlk)
        strPaySlip = IIf(strPaySlip = "", ddlPaySlip.SelectedItem.Value, strPaySlip)

        If ddlADGroup.SelectedItem.Value = "" Then
            lblErrADGroup.Visible = True
            Exit Sub
        ElseIf ddlType.SelectedItem.Value = "" Then
            lblErrADType.Visible = True
            Exit Sub
        Else
            strParam = Trim(txtADCode.Text)
            Try
                intErrNo = objPRSetup.mtdGetAD(strOpCd_Get, _
                                                strParam, _
                                                objADDs, _
                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_GET&errmesg=" & Exp.ToString() & "&redirect=pr/setup/HR_setup_ADList.aspx")
            End Try

            If objADDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDupADCode.Visible = True
            Else
                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
                strSelectedADCode = Trim(txtADCode.Text)
                adcode.Value = strSelectedADCode
                strParam = strSelectedADCode & "|" & _
                            Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _
                            ddlADGroup.SelectedItem.Value & "|" & _
                            ddlType.SelectedItem.Value & "|" & _
                            strDefAcc & "|" & _
                            strDefBlk & "|" & _
                            IIf(cbJamsContribute.Checked = True, objPRSetup.EnumJamsDeductInd.Yes, objPRSetup.EnumJamsDeductInd.No) & "|" & _
                            IIf(cbTaxContribute.Checked = True, objPRSetup.EnumTaxDeductInd.Yes, objPRSetup.EnumTaxDeductInd.No) & "|" & _
                            strPaySlip & "|" & _
                            objPRSetup.EnumADStatus.Active & "|" & _
                            IIf(cbMaternityAllowance.Checked = True, objPRSetup.EnumMaternityInd.Yes, objPRSetup.EnumMaternityInd.No) & "|" & _
                            IIf(cbBonus.Checked = True, objPRSetup.EnumBonus.Yes, objPRSetup.EnumBonus.No) & "|" & _
                            IIf(cbTHR.Checked = True, objPRSetup.EnumTHRInd.Yes, objPRSetup.EnumTHRInd.No) & "|" & _
                            ddlVehCode.SelectedItem.Value & "|" & _
                            ddlVehExpCode.SelectedItem.Value & "|" & _
                            IIf(cbHouseRent.Checked = True, objPRSetup.EnumHouseInd.Yes, objPRSetup.EnumHouseInd.No) & "|" & _
                            IIf(cbTransport.Checked = True, objPRSetup.EnumTransportInd.Yes, objPRSetup.EnumTransportInd.No) & "|" & _
                            IIf(cbMedical.Checked = True, objPRSetup.EnumMedicalInd.Yes, objPRSetup.EnumMedicalInd.No) & "|" & _
                            IIf(cbMeal.Checked = True, objPRSetup.EnumMealInd.Yes, objPRSetup.EnumMealInd.No) & "|" & _
                            IIf(cbLeave.Checked = True, objPRSetup.EnumLeaveInd.Yes, objPRSetup.EnumLeaveInd.No) & "|" & _
                            IIf(cbAirBus.Checked = True, objPRSetup.EnumAirBusInd.Yes, objPRSetup.EnumAirBusInd.No) & "|" & _
                            IIf(cbTax.Checked = True, objPRSetup.EnumTaxInd.Yes, objPRSetup.EnumTaxInd.No) & "|" & _
                            IIf(cbDanaPensiun.Checked = True, objPRSetup.EnumDanaPensiunInd.Yes, objPRSetup.EnumDanaPensiunInd.No) & "|" & _
                            IIf(cbRelocation.Checked = True, objPRSetup.EnumRelocationInd.Yes, objPRSetup.EnumRelocationInd.No)
                Try
                    intErrNo = objPRSetup.mtdUpdAD(strOpCd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    False)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/HR_setup_ADList.aspx")
                End Try
            End If
        End If
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_AD_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_AD_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_AD_GET"
        Dim strOpCd_Sts As String = "PR_CLSSETUP_AD_STATUS_UPD"
        Dim strDefAcc As String = Request.Form("ddlDefAccCode")
        Dim strDefBlk As String = Request.Form("ddlDefBlkCode")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")

        Dim strOpCd As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If strDefAcc = "" Then strDefAcc = ddlDefAccCode.SelectedItem.Value
        If strDefBlk = "" Then strDefBlk = ddlDefBlkCode.SelectedItem.Value
        GetAccountDetails(strDefAcc, intAccType, intAccPurpose, intNurseryInd)

        If ddlADGroup.SelectedItem.Value = "" Then
            lblErrADGroup.Visible = True
            Exit Sub
        ElseIf ddlType.SelectedItem.Value = "" Then
            lblErrADType.Visible = True
            Exit Sub
        ElseIf strDefAcc = "" Then
            lblErrDefAccCode.Visible = True
            Exit Sub
        ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes And strDefBlk = "" Then
            lblErrDefBlkCode.Visible = True
            Exit Sub
        ElseIf intAccType = objGLSetup.EnumAccountType.ProfitAndLost And strDefBlk = "" Then
            lblErrDefBlkCode.Visible = True
            Exit Sub
        Else
            If strCmdArgs = "Save" Then
                InsertADRecord()
            ElseIf strCmdArgs = "Del" Then
                strParam = strSelectedADCode & "|" & objPRSetup.EnumADStatus.Deleted
                Try
                    intErrNo = objPRSetup.mtdUpdAD(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam, _
                                                   True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_ADDet.aspx?adcode=" & strSelectedADCode)
                End Try

            ElseIf strCmdArgs = "UnDel" Then
                strParam = strSelectedADCode & "|" & objPRSetup.EnumADStatus.Active
                Try
                    intErrNo = objPRSetup.mtdUpdAD(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam, _
                                                   True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_ADDet.aspx?adcode=" & strSelectedADCode)
                End Try
            End If

            If strSelectedADCode <> "" Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_setup_ADList.aspx")
    End Sub
    
    Sub Bonus_Clicked(Sender As Object, E As EventArgs)
        If cbBonus.Checked Then
            cbTaxContribute.Checked = True
            cbTaxContribute.Enabled = False
        Else
            cbTaxContribute.Checked = False
            cbTaxContribute.Enabled = True
        End If
    End Sub

    Sub BindVehicle(ByVal pv_strAccCode As String, ByVal pv_strVehCode As String)
        Dim objVehDs As New Dataset
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strVehCode = Trim(UCase(pv_strVehCode))
        For intCnt = 0 To objVehDs.Tables(0).Rows.Count - 1
            objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode"))
            objVehDs.Tables(0).Rows(intCnt).Item("Description") = objVehDs.Tables(0).Rows(intCnt).Item("VehCode") & " (" & Trim(objVehDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If UCase(objVehDs.Tables(0).Rows(intCnt).Item("VehCode")) = pv_strVehCode Then
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
        ddlVehCode.AutoPostBack = True
        objVehDs = Nothing
    End Sub

    Sub BindVehicleExpense(ByVal pv_IsBlankList As Boolean, ByVal pv_strVehExpCode As String)
        Dim objVehExpDs As New Dataset()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_VEHEXPENSE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strVehExpCode = Trim(UCase(pv_strVehExpCode))

        For intCnt = 0 To objVehExpDs.Tables(0).Rows.Count - 1
            objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode").Trim()
            objVehExpDs.Tables(0).Rows(intCnt).Item("Description") = objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") & " (" & objVehExpDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If UCase(objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode")) = pv_strVehExpCode Then
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
        ddlVehExpCode.AutoPostBack = True
        objVehExpDs = Nothing
    End Sub

End Class
