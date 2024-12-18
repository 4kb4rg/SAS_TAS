Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Data.SqlTypes
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic

Public Class PR_trx_ADDet : Inherits Page

    Protected WithEvents lblADTrxID As Label
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtBalAmount As TextBox
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents ddlADCode As DropDownList
    Protected WithEvents ddlTrxType As DropDownList
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents ddlVehExpCode As DropDownList
    Protected WithEvents txtEffPeriod As TextBox
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents txtMonth As TextBox
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents lblAmountPercentage As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblBalAmount As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents adid As HtmlInputHidden
    Protected WithEvents btnFind1 As HtmlInputButton
    Protected WithEvents btnFind2 As HtmlInputButton
    Protected WithEvents btnFind3 As HtmlInputButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents NewBtn As ImageButton
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblADType As Label
    Protected WithEvents lblVehicleOption As Label
    Protected WithEvents lblErrEmp As Label
    Protected WithEvents lblErrADCode As Label
    Protected WithEvents lblErrTrxType As Label
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblErrVehExp As Label
    Protected WithEvents lblErrEffPeriod As Label
    Protected WithEvents lblErrAmount As Label
    Protected WithEvents lblInvalidRange As Label
    Protected WithEvents lblErrMonth As Label
    Protected WithEvents lblErrPeriodFound As Label
    Protected WithEvents lblErrOneTime As Label
    Protected WithEvents lblErrChecking As Label
    Protected WithEvents lblErrPermanent As Label
    Protected WithEvents lblErrPercentage As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehExpense As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrAccPeriod As Label
    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents hidTrxType As HtmlInputHidden
    Protected WithEvents lblPercentTag As Label
    Protected WithEvents lblAmountTag As Label
    Protected WithEVents lblADCodeNotExist As Label
    Protected WithEvents lblErrMedical1 As Label
    Protected WithEvents lblErrMedical2 As Label
    Protected WithEvents lblhouseind As Label
    Protected WithEvents lblMedicalInd As Label
    Protected WithEvents txtBalance As TextBox
    Protected WithEvents txtBalAmount1 As TextBox
    Protected WithEvents lblErrCheckBal As Label
    Protected WithEvents lblErrBalAmount As Label
    Protected WithEvents lblErrHouseRent As Label
    Protected WithEvents lblErrMedical As Label
    Protected WithEvents lblFlgMedCheckEmp as Label
    Protected WithEvents lblMedicalOnTypeAD as Label
    Protected lblCloseExist As Label    
    Protected objPRTrx As New agri.PR.clsTrx()
    Protected WithEvents lblErrMedical3 As Label

    Protected WithEvents lblTransportInd As Label  
    Protected WithEvents lblErrTransport As Label
    Protected WithEvents lblErrMaternity As Label 
    Protected WithEvents lblErrNoMaternity As Label
    Protected WithEvents lblErr3Maternity As Label  
    Protected WithEvents lblErrMeal As Label
    Protected WithEvents lblMealInd As Label   
    Protected WithEvents lblMaternityInd As Label  
    Protected WithEvents lblRelocationInd As Label   
    Protected WithEvents lblErrNoRelocation As Label
    Protected WithEvents lblErrRelocation As Label
    Protected WithEvents lblErrTransGiven As Label
    Protected WithEvents lblErrMealGiven As Label
    Protected WithEvents lblErrCheckADCode As Label
    Protected WithEvents lblErrTypeAD as Label

    Protected WithEvents trSpacer1 As HtmlTableRow
    Protected WithEvents trSpacer2 As HtmlTableRow
    Protected WithEvents trPeriod As HtmlTableRow
    Protected WithEvents trDetailDaily As HtmlTableRow
    Protected WithEvents tblDetailDaily As HtmlTable

    Protected WithEvents lblTrxTypeInd As Label

    Protected WithEvents lblEffDate As Label
    Protected WithEvents txtEffDate As TextBox
    Protected WithEvents lblRfvEffDate As Label
    Protected WithEvents lblErrEffDate As Label
    Protected WithEvents lblErrEffDateDesc As Label
    Protected WithEvents btnSelDate As Image

    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim objHRSetup As New agri.HR.clsSetup()

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objLangCapDs As New Dataset()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strPhyMonth As String
    Dim strPhyYear As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strSelectedADId As String = ""
    Dim intStatus As Integer
    Dim flgHouseInd As Boolean
    Dim flgMedCloseInd As string = ""
    Dim flgMedCheckEmp As string = ""
    Dim flgMedCheck As string = ""
    Dim strLocType as String

    Dim strDateFmt as String
    Dim objFormatDate As String
    Dim objActualDate As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intTrxType As Integer = 0
        Dim strAD As String = Request.QueryString("adcd")
        Dim strTrxType As String = Request.QueryString("adtype")
        Dim strAcc As String = Request.QueryString("acc")
        Dim strBlk As String = Request.QueryString("blk")
        Dim strVeh As String = Request.QueryString("veh")
        Dim strVehExp As String = Request.QueryString("vehexp")
        Dim strType As String = Request.QueryString("type")

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")



        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAllowanceDeduction), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrEmp.Visible = False
            lblErrADCode.Visible = False
            lblErrTrxType.Visible = False
            lblErrAccount.Visible = False
            lblErrBlock.Visible = False
            lblErrVehicle.Visible = False
            lblErrVehExp.Visible = False
            lblErrEffPeriod.Visible = False
            lblErrAccPeriod.Visible = False
            lblErrAmount.Visible = False
            lblInvalidRange.Visible = False
            lblErrMonth.Visible = False
            lblErrPeriodFound.Visible = False
            lblErrOneTime.Visible = False
            lblErrPermanent.Visible = False
            lblErrPercentage.Visible = False
            lblADCodeNotExist.Visible = False
            lblErrNoMaternity.Visible = False
            lblErrNoRelocation.Visible = False
            lblErrMedical.Visible = False
            lblErrMedical1.Visible = False
            lblErrMedical2.Visible = False
            lblErrMealGiven.Visible = False
            lblErrTransGiven.Visible = False
            lblErrCheckADCode.Visible = False
            lblErrMaternity.Visible = False
            lblErrChecking.Visible = False
            lblErrTransport.Visible= False
            lblErrMeal.Visible= False
            lblErrMaternity.Visible= False
            lblErr3Maternity.Visible= False
            lblErrRelocation.Visible= False
            lblRfvEffDate.Visible = False
            lblErrEffDate.Visible = False
            lblErrTypeAD.Visible = False

            strSelectedADId = Trim(IIf(Request.QueryString("adid") <> "", Request.QueryString("adid"), Request.Form("adid")))
            intStatus = Convert.ToInt32(lblHiddenSts.Text)


              If Not IsPostBack Then
                If strSelectedADId <> "" Then
                    adid.Value = strSelectedADId
                    lblFlgMedCheckEmp.Text = "1"
                    onLoad_Display()
                    onLoad_DisplayLine()
                    onLoad_BindButton()
                    
                Else
                    If strTrxType <> "" Then
                        intTrxType = Convert.ToInt32(strTrxType)
                    End If
                    BindEmployee("")
                    BindADTrxType(intTrxType)
                    BindADCode(strAD, strTrxType)
                    BindAccount(strAcc)
                    BindBlock(strAcc, strBlk)
                    BindVehicle(strAcc, strVeh)
                    If Trim(strVehExp) = "" Then
                        BindVehicleExpense(True, strVehExp)
                    Else
                        BindVehicleExpense(False, strVehExp)
                    End If
                    If strType = "new" Then
                        BindIndicator(ddlADCode.SelectedItem.Value.ToString)
                        
                              
                        BindMaternityAmt(ddlADCode.SelectedItem.Value.ToString)
                        BindRelocationAmt(ddlADCode.SelectedItem.Value.ToString)
                        BindIndicator(ddlADCode.SelectedItem.Value.ToString)
                        lblTrxTypeInd.Text = Trim(ddlTrxType.SelectedItem.Value)
                    End If
                    onLoad_BindButton()
                    btnAdd.Visible = True
                End If
            End If

            onTrxType_Display()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADDET_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_ADDet.aspx")
        End Try

        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
        dgLineDet.Columns(1).HeaderText = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        dgLineDet.Columns(2).HeaderText = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        dgLineDet.Columns(3).HeaderText = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        dgLineDet.Columns(4).HeaderText = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADDET_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_ADDet.aspx")
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
        ddlEmployee.Enabled = False
        if lblMedicalInd.Text <> "" then
            ddlADCode.Enabled = True
        else    
            ddlADCode.Enabled = False
        end if 
        ddlTrxType.Enabled = False
        ddlAccount.Enabled = False 
        ddlBlock.Enabled = False
        ddlVehCode.Enabled = False
        ddlVehExpCode.Enabled = False
        txtMonth.Enabled = True
        SaveBtn.Visible = False
        CancelBtn.Visible = False
        NewBtn.Visible = False
        tblSelection.Visible = False
        btnFind1.Disabled = False
        btnFind2.Disabled = False
        btnFind3.Disabled = False
        txtEffPeriod.Text = ""

        Select Case intStatus
            Case objPRTrx.EnumADTrxStatus.Active
                txtDesc.Enabled = True
                SaveBtn.Visible = True
                NewBtn.Visible = True
                tblSelection.Visible = True
                btnFind1.Disabled = True
                btnFind2.Disabled = True
                btnFind3.Disabled = True
                If lblCloseExist.Text = "no" Then
                    CancelBtn.Visible = True
                End If
                txtEffDate.Enabled = false
                If lblTrxTypeInd.Text <> "" Then
                    If lblTrxTypeInd.Text = objPRTrx.EnumADTrxTypeStatus.Daily Then
                        ddlADCode.Enabled = True
                        ddlAccount.Enabled = True
                        ddlBlock.Enabled = True
                        ddlVehCode.Enabled = True
                        ddlVehExpCode.Enabled = True

                        btnFind1.Disabled = false
                        btnFind2.Disabled = false
                        btnFind3.Disabled = false
                    Else
                        ddlADCode.Enabled = false
                        ddlAccount.Enabled = false
                        ddlBlock.Enabled = false
                        ddlVehCode.Enabled = false
                        ddlVehExpCode.Enabled = false
                    End If

                    if lblMedicalInd.Text <> "" then
                        ddlADCode.Enabled = True
                    else
                        If lblTrxTypeInd.Text <> "" Then
                            If lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
                                ddlADCode.Enabled = False
                            Else
                                ddlADCode.Enabled = True
                            End If
                        End If
                    end if 
                End If
            Case objPRTrx.EnumADTrxStatus.Closed, objPRTrx.EnumADTrxStatus.Cancelled
                btnFind1.Disabled = True
                btnFind2.Disabled = True
                btnFind3.Disabled = True
                If lblTransportInd.Text="1" or lblMealInd.Text="1" Then
                    tblSelection.Visible = True
                End If 
                txtEffDate.Enabled = false
                ddlADCode.Enabled = False
                If lblTrxTypeInd.Text <> "" Then
                    If lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
                        trDetailDaily.visible = true
                    Else
                        trDetailDaily.visible = false
                    End If
                End If
            Case Else
                txtEffDate.Enabled = true
                txtDesc.Enabled = True
                ddlEmployee.Enabled = True
                ddlADCode.Enabled = True
                ddlTrxType.Enabled = True
                ddlAccount.Enabled = True
                ddlBlock.Enabled = True
                ddlVehCode.Enabled = True
                ddlVehExpCode.Enabled = True
                SaveBtn.Visible = True
                tblSelection.Visible = True
                txtEffPeriod.Text = strAccMonth & "/" & strAccYear
                trDetailDaily.Visible = True
        End Select
    End Sub

    Sub onSelect_ChangeTrxType(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intTrxType As Integer = 0

        txtAmount.Text = ""
        If Trim(ddlTrxType.SelectedItem.Value) <> "" Then
            intTrxType = Convert.ToInt32(ddlTrxType.SelectedItem.Value)
        End If
        If Trim(ddlTrxType.SelectedItem.Value) <> "" Then
            intTrxType = Convert.ToInt32(ddlTrxType.SelectedItem.Value)
        End If

        If intTrxType = objPRTrx.EnumADTrxTypeStatus.OneTime Or intTrxType = objPRTrx.EnumADTrxTypeStatus.Permanent Then
            txtMonth.Text = "1"
            txtMonth.Enabled = False
        Else
            txtMonth.Enabled = True
            If intTrxType = objPRTrx.EnumADTrxTypeStatus.Percentage Then
                lblAmountPercentage.Text = "Percent"
            Else
                lblAmountPercentage.Text = "Amount"
            End If
        End If
        If intTrxType <> 0 Then
            BindADCode("", CStr(intTrxType))
        Else
            BindADCode("", "")
        End If
        BtnAdd.Visible = True 

        lblMedicalOnTypeAD.visible = false

        lblTrxTypeInd.Text = Trim(ddlTrxType.SelectedItem.Value)
        onTrxType_Display()

        
    End Sub

    Sub onLoad_Display()
        Dim objADTrxDs As New Dataset
        Dim strOpCd As String = "PR_CLSTRX_ADTRX_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        Dim strOpCd_Daily As String = "PR_CLSTRX_ADTRX_SEARCH"
        Dim strStatus As String
        Dim objFormatDate As String
        Dim objActualDate As String


        If lblTrxTypeInd.Text <> "" Then       
            If lblTrxTypeInd.Text = objPRTrx.EnumADTrxTypeStatus.Daily And Trim(lblADTrxID.Text) = "" Then

                If objGlobal.mtdValidInputDate(strDateFmt, _
                                                    txtEffDate.Text, _
                                                    objFormatDate, _
                                                    objActualDate) = True Then
                End If
                strStatus = objPRTrx.EnumADTrxStatus.Active & "' And AD.EffDate='" & objActualDate & "' And AD.EmpCode='" & Trim(ddlEmployee.SelectedItem.Value)
                strParam =   "|||" & strStatus & "||" & "AD.ADTrxID|ASC" 

                
                Try
                    intErrNo = objPRTrx.mtdGetADTrx(strOpCd_Daily, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objADTrxDs, _
                                                    False)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
                End Try
            Else
                strParam = strSelectedADId

                Try
                    intErrNo = objPRTrx.mtdGetADTrx(strOpCd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objADTrxDs, _
                                                    True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
                End Try
            End If
        Else
            strParam = strSelectedADId

            Try
                 intErrNo = objPRTrx.mtdGetADTrx(strOpCd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objADTrxDs, _
                                                    True)
            Catch Exp As System.Exception
                 Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try
        End If


        If objADTrxDs.Tables(0).Rows.Count > 0 Then
            strSelectedADId = objADTrxDs.Tables(0).Rows(0).Item("ADTrxId").Trim()
            adid.Value = strSelectedADId
            lblADTrxID.Text = strSelectedADId
            txtDesc.Text = objADTrxDs.Tables(0).Rows(0).Item("Description").Trim()
            lblPeriod.Text = objADTrxDs.Tables(0).Rows(0).Item("AccMonth").Trim() & "/" & objADTrxDs.Tables(0).Rows(0).Item("AccYear").Trim()
            intStatus = Convert.ToInt32(objADTrxDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenSts.Text = objADTrxDs.Tables(0).Rows(0).Item("Status").Trim()
            lblStatus.Text = objPRTrx.mtdGetADTrxStatus(Convert.ToInt16(objADTrxDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objADTrxDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objADTrxDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objADTrxDs.Tables(0).Rows(0).Item("UserName")
            If Convert.ToInt32(Trim(objADTrxDs.Tables(0).Rows(0).Item("TrxType"))) = objPRTrx.EnumADTrxTypeStatus.Percentage Then
                lblAmountPercentage.Text = "Percent"
            Else
                lblAmountPercentage.Text = "Amount"
            End If
            lblTrxTypeInd.Text = Trim(objADTrxDs.Tables(0).Rows(0).Item("TrxType"))
            txtEffDate.Text = objGlobal.GetShortDate(strDateFmt,Trim(objADTrxDs.Tables(0).Rows(0).Item("EffDate")))
            BindEmployee(objADTrxDs.Tables(0).Rows(0).Item("EmpCode").Trim())
            BindADCode(objADTrxDs.Tables(0).Rows(0).Item("ADCode").Trim(), "")
            BindADTrxType(objADTrxDs.Tables(0).Rows(0).Item("TrxType").Trim())
            hidTrxType.value = objADTrxDs.Tables(0).Rows(0).Item("TrxType").Trim()
            BindAccount(objADTrxDs.Tables(0).Rows(0).Item("AccCode").Trim())
            BindBlock(objADTrxDs.Tables(0).Rows(0).Item("AccCode").Trim(), objADTrxDs.Tables(0).Rows(0).Item("BlkCode").Trim())
            BindVehicle(objADTrxDs.Tables(0).Rows(0).Item("AccCode").Trim(), objADTrxDs.Tables(0).Rows(0).Item("VehCode").Trim())
            BindVehicleExpense(False, objADTrxDs.Tables(0).Rows(0).Item("ExpenseCode").Trim())

            BindIndicator(ddlADCode.SelectedItem.Value.ToString)
            if lblMedicalInd.Text = "1" Then
                BindBalanceAmt(strSelectedADId)
                ddlADCode.Enabled = true 
            end if
                  
            BindMaternityAmt(ddlADCode.SelectedItem.Value.ToString)
            BindRelocationAmt(ddlADCode.SelectedItem.Value.ToString)

            objADTrxDs = Nothing
        End If
    End Sub

    Sub onLoad_DisplayLine()
        Dim objADTrxLnDs As New Dataset()
        Dim strOpCd As String = "PR_CLSTRX_ADTRX_LINE_GET"
        Dim strOpCd_Med As String = "PR_CLSTRX_ADTRX_LINE_GET_MED"
        Dim strParam As String = strSelectedADId
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lblLabel As Label
        Dim dblAmount As Double = 0

        if lblMedicalInd.Text = "1" then
            strParam = strParam & "|" & strPhyYear
            Try
                intErrNo = objPRTrx.mtdGetADTrx1(strOpCd_Med, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objADTrxLnDs, _
                                            False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRXLINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try
        else
            Try
            intErrNo = objPRTrx.mtdGetADTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objADTrxLnDs, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRXLINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        end if

        For intCnt = 0 To objADTrxLnDs.Tables(0).Rows.Count - 1
            dblAmount += objADTrxLnDs.Tables(0).Rows(intCnt).Item("Amount")
        Next

        If hidTrxType.Value = objPRTrx.EnumADTrxTypeStatus.Percentage Then
            dgLineDet.Columns(7).HeaderText = lblPercentTag.text
        Else
            dgLineDet.Columns(7).HeaderText = lblAmountTag.text
        End If
        dgLineDet.DataSource = objADTrxLnDs.Tables(0)
        dgLineDet.DataBind()

        lblTotalAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(dblAmount, 0)

        If intStatus = objPRTrx.EnumADTrxStatus.Active Then
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lblLabel = dgLineDet.Items.Item(intCnt).FindControl("Status")
                If Convert.ToInt32(lblLabel.Text) = objPRTrx.EnumADTrxLnStatus.Active Then
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Else
                    lbButton.Visible = False
                    If Convert.ToInt32(lblLabel.Text) = objPRTrx.EnumADTrxLnStatus.Closed Then
                        lblCloseExist.Text = "yes"
                        If lblTransportInd.Text="1" or lblMealInd.Text="1" Then
                            lbButton.Visible = True
                            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                        End If
                    End If
                End If
            Next
        Else
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = False
                If lblTransportInd.Text="1" or lblMealInd.Text="1" Then
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                End If
            Next
        End If
    End Sub

    Sub BindEmployee(ByVal pv_strEmpId As String)
        Dim objEmpDs As New Dataset
        Dim strOpCd As String = "HR_CLSSETUP_EMPID_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "|" & Convert.ToString(objHRTrx.EnumEmpStatus.Active) & "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objHRTrx.mtdGetEmpCode(strOpCd, strParam, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strEmpId = Trim(UCase(pv_strEmpId))

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") & " (" & objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") & ")"
            If UCase(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode")) = pv_strEmpId Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objEmpDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strEmpId <> "" Then
                dr("EmpCode") = Trim(pv_strEmpId)
                dr("EmpName") = Trim(pv_strEmpId)
            Else
                dr("EmpCode") = ""
                dr("EmpName") = "Select one Employee ID"
            End If
        Else
            dr("EmpCode") = ""
            dr("EmpName") = "Select one Employee ID"
        End If
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmployee.DataSource = objEmpDs.Tables(0)
        ddlEmployee.DataValueField = "EmpCode"
        ddlEmployee.DataTextField = "EmpName"
        ddlEmployee.DataBind()
        ddlEmployee.SelectedIndex = intSelectedIndex
        objEmpDs = Nothing
    End Sub

    Sub BindADCode(ByVal pv_strADCode As String, ByVal pv_strTrxType As String)
        Dim objADCodeDs As New Dataset()
        Dim strOpCd As String = "PR_CLSTRX_ADDET_ADCODE_GET"
        Dim dr As DataRow
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String = ""
        Dim blnFound As Boolean = False
    
        strSearch = "and ad.Status = '" & objPRSetup.EnumADStatus.Active & "' "

        If pv_strTrxType <> "" Then
            Select Case CInt(pv_strTrxType)
                Case objPRTrx.EnumADTrxTypeStatus.OneTime
                    strSearch = strSearch & "and AD.ADType in ('" & objPRSetup.EnumADType.Allowance & "','" & _
                                objPRSetup.EnumADType.Deduction & "','" & _
                                objPRSetup.EnumADType.EAItem & "','" & _
                                objPRSetup.EnumADType.MemoItem & "') " 

                Case objPRTrx.EnumADTrxTypeStatus.Recurring, objPRTrx.EnumADTrxTypeStatus.Permanent
                    strSearch = strSearch & "and AD.ADType in ('" & objPRSetup.EnumADType.Allowance & "','" & _
                                objPRSetup.EnumADType.Deduction & "') "
                Case objPRTrx.EnumADTrxTypeStatus.Reducing, objPRTrx.EnumADTrxTypeStatus.Percentage
                    strSearch = strSearch & "and AD.ADType = '" & objPRSetup.EnumADType.Deduction & "' "

            End Select

        End If
        If Trim(lblTrxTypeInd.Text) <> "" Then
            If Trim(lblTrxTypeInd.Text) = objPRTrx.EnumADTrxTypeStatus.Daily Then
                        strSearch = strSearch & _
                                    " and AD.MaternityInd <> '" & objPRSetup.EnumMaternityInd.Yes & "' " & _
                                    " and AD.BonusInd <> '" & objPRSetup.EnumBonus.Yes & "' " & _
                                    " and AD.THRInd <> '" & objPRSetup.EnumTHRInd.Yes & "' " & _
                                    " and AD.HouseInd <> '" & objPRSetup.EnumHouseInd.Yes & "' " & _
                                    " and AD.TransportInd <> '" & objPRSetup.EnumTransportInd.Yes & "' " & _
                                    " and AD.MedicalInd <> '" & objPRSetup.EnumMedicalInd.Yes & "' " & _
                                    " and AD.MealInd <> '" & objPRSetup.EnumMealInd.Yes & "' " & _
                                    " and AD.LeaveInd <> '" & objPRSetup.EnumLeaveInd.Yes & "' " & _
                                    " and AD.AirBusInd <> '" & objPRSetup.EnumAirBusInd.Yes & "' " & _
                                    " and AD.TaxInd <> '" & objPRSetup.EnumTaxInd.Yes & "' " & _ 
                                    " and AD.DanaPensiunInd <> '" & objPRSetup.EnumDanaPensiunInd.Yes & "' " & _
                                    " and AD.RelocationInd <> '" & objPRSetup.EnumRelocationInd.Yes & "' " 
            End If
        End If
        strSearch = strSearch & "order by AD.ADCode"
        strParam = strSearch

        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCd, _
                                           strParam, _
                                           objADCodeDs, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_ADCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strADCode = Trim(UCase(pv_strADCode))
        For intCnt = 0 To objADCodeDs.Tables(0).Rows.Count - 1
            If UCase(objADCodeDs.Tables(0).Rows(intCnt).Item("AdCode")) = pv_strADCode Then
                intSelectedIndex = intCnt + 1
                blnFound = True
            End If
        Next

        dr = objADCodeDs.Tables(0).NewRow()
        dr("_Value") = "|"
        dr("_Description") = "Select Allowance & Deduction Code"
        objADCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlADCode.DataSource = objADCodeDs.Tables(0)
        ddlADCode.DataValueField = "_Value"
        ddlADCode.DataTextField = "_Description"
        ddlADCode.DataBind()
        ddlADCode.SelectedIndex = intSelectedIndex

        If trim(pv_strADCode) <> "" and blnFound = False Then
            lblADCodeNotExist.Visible = True
        End If

    End Sub

    Sub BindADTrxType(ByVal pv_strAdTrxType As String)
        ddlTrxType.SelectedIndex = Convert.ToInt32(pv_strAdTrxType)
    End Sub

    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim objAccDs As New Dataset()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strAccCode = UCase(Trim(pv_strAccCode))

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = objAccDs.Tables(0).Rows(intCnt).Item("AccCode").Trim()
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = objAccDs.Tables(0).Rows(intCnt).Item("AccCode") & " (" & objAccDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If UCase(objAccDs.Tables(0).Rows(intCnt).Item("AccCode")) = pv_strAccCode Then
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

        objAccDs = Nothing
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            If Convert.ToInt32(_objAccDs.Tables(0).Rows(0).Item("AccType")) = objGLSetup.EnumAccountType.BalanceSheet Then
                pr_IsBalanceSheet = True
                If Convert.ToInt32(_objAccDs.Tables(0).Rows(0).Item("NurseryInd")) = objGLSetup.EnumNurseryAccount.Yes Then
                    pr_IsNurseryInd = True
                End If
            End If
            If Convert.ToInt32(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.NonVehicle Then
                pr_IsBlockRequire = True
            ElseIf Convert.ToInt32(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.VehicleDistribution Then
                pr_IsVehicleRequire = True
            ElseIf Convert.ToInt32(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.Others Then
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
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim strAD As String = Request.Form("ddlADCode")
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")

        GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

        If Not blnIsBalanceSheet Then
            If blnIsBlockRequire Then
                BindBlock(strAcc, strBlk)
                BindVehicle("", strVeh)
                BindVehicleExpense(True, strVehExp)
            Else
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
            BindBlock(strAcc, strBlk)
            BindVehicle("", strVeh)
            BindVehicleExpense(True, strVehExp)
        Else
            BindBlock("", strBlk)
            BindVehicle("", strVeh)
            BindVehicleExpense(True, strVehExp)
        End If
    End Sub

    Sub BindBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim objBlkDs As New Dataset()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strBlkCode = Trim(UCase(pv_strBlkCode))

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & objBlkDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If UCase(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) = pv_strBlkCode Then
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
        ddlBlock.AutoPostBack = True

        objBlkDs = Nothing
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

    Sub InsertRecord(ByRef pr_blnIsUpdated As Boolean)
        Dim objADTrxId As String
        Dim strOpCd_Add As String = "PR_CLSTRX_ADTRX_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_ADTRX_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_ADTRX_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_ADTRX_STATUS_UPD"
        Dim strOpCd_ADList As String = "PR_CLSSETUP_ADLIST_GET"
        Dim objADCodeDs As New Dataset()
        Dim strOpCd As String
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim strAD As String = Request.Form("ddlADCode")
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strSearch As String = ""
        Dim arrAD As Array
        Dim strADCode As String

        pr_blnIsUpdated = False

        strEmp = IIf(strEmp = "", ddlEmployee.SelectedItem.Value, strEmp)
        strAD = IIf(strAD = "", ddlADCode.SelectedItem.Value, strAD)
        arrAD = Split(strAD, "|")
        strADCode = arrAD(0)
        lblADType.Text = arrAD(1)
        strAcc = IIf(strAcc = "", ddlAccount.SelectedItem.Value, strAcc)
        strBlk = IIf(strBlk = "", ddlBlock.SelectedItem.Value, strBlk)
        strVeh = IIf(strVeh = "", ddlVehCode.SelectedItem.Value, strVeh)
        strVehExp = IIf(strVehExp = "", ddlVehExpCode.SelectedItem.Value, strVehExp)

        If strEmp = "" Then
            lblErrEmp.Visible = True
            Exit Sub
        ElseIf ddlTrxType.SelectedItem.Value = "" Then
            lblErrTrxType.Visible = True
            Exit Sub
        ElseIf strAD.Trim() = "|" And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            lblErrADCode.Visible = True
            Exit Sub
        ElseIf strAcc = ""  And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            If lblADType.Text = objPRSetup.EnumADType.Allowance Or lblADType.Text = objPRSetup.EnumADType.Deduction Or lblADType.Text = objPRSetup.EnumADType.LevyItem Then
                lblErrAccount.Visible = True
                Exit Sub
            End If
        End If

        GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)
        If strBlk = "" And blnIsBlockRequire = True And blnIsBalanceSheet = False And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            lblErrBlock.Visible = True
            Exit Sub
        ElseIf strBlk = "" And blnIsBalanceSheet = True And blnIsNurseryInd = True And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            lblErrBlock.Visible = True
            Exit Sub
        ElseIf strVeh = "" And blnIsVehicleRequire = True And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            lblErrVehicle.Visible = True
            Exit Sub
        ElseIf strVehExp = "" And blnIsVehicleRequire = True And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            lblErrVehExp.Visible = True
            Exit Sub
        ElseIf strVeh <> "" And strVehExp = "" And lblVehicleOption.Text = True And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            lblErrVehExp.Visible = True
            Exit Sub
        ElseIf strVeh = "" And strVehExp <> "" And lblVehicleOption.Text = True And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            lblErrVehicle.Visible = True
            Exit Sub
        End If







        strSelectedADId = Trim(adid.Value)
        strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)

        If lblTrxTypeInd.Text = objPRTrx.EnumADTrxTypeStatus.Daily Then
            strADCode = ""
            strAcc = ""
            strBlk = ""
            strVeh = ""
            strVehExp =	""	
        End If

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                            txtEffDate.Text, _
                                            objFormatDate, _
                                            objActualDate) = True Then
        End If
        strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRADTrx) & "|" & _
                    strSelectedADId & "|" & _
                    Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _
                    strEmp & "|" & _
                    strADCode & "|" & _
                    ddlTrxType.SelectedItem.Value & "|" & _
                    strAcc & "|" & _
                    strBlk & "|" & _
                    strVeh & "|" & _
                    strVehExp & "|" & _
                    objPRTrx.EnumADTrxStatus.Active & "|" & _
                    objActualDate
        Try
            intErrNo = objPRTrx.mtdUpdADTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            False, _
                                            objADTrxId)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_ADTRX_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_ADdet.aspx")
        End Try
        strSelectedADId = objADTrxId
        adid.Value = strSelectedADId
        pr_blnIsUpdated = True
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objResult As New Dataset()
        Dim objAccCfg As New Dataset()
        Dim objFound As Boolean
        Dim strOpCode_GetLine As String = "PR_CLSTRX_ADTRX_LINE_GET"
        Dim strOpCode_AddLine As String = "PR_CLSTRX_ADTRX_LINE_ADD"
        Dim strOpCode_SearchLine As String = "PR_CLSTRX_ADTRX_LINE_SEARCH"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_ADTRX_STATUS_UPD"
        Dim strOpCode_AccCfg As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_GET"
        Dim blnIsUpdated As Boolean
        Dim strParam As String
        Dim intErrNo As Integer
        Dim arrPeriod As Array
        Dim intMonth As Integer
        Dim intYear As Integer
        Dim intMaxPeriod As Integer
        Dim strEmpCode As String  
        Dim strAllowance As String
        Dim strVehCode as String
        Dim strPayType as String
        Dim strMaritalStatus as String
        Dim strEmpCategory as String
        Dim strEmpPosition as String
        Dim strEmpCategory2 as String
        Dim strEmpPosition2 as String
        Dim intAmount as Double
        Dim intBalAmount as Double
        Dim s as string 
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim strAD As String = Request.Form("ddlADCode")
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strExp As String = Request.Form("ddlVehExpCode")

        Dim strADCode As String
        Dim arrAD As Array

        If lblTrxTypeInd.Text <> "" Then
            If (Trim(txtEffPeriod.Text) = "" Or InStr(txtEffPeriod.Text, "/") = 0) And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
                lblErrEffPeriod.Visible = True
                Exit Sub
            ElseIf Trim(txtAmount.Text) = "" Then
                lblErrAmount.Visible = True
                Exit Sub
            ElseIf Trim(txtMonth.Text) = "" And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
                lblErrMonth.Visible = True
                Exit Sub
            End If        
            If lblTrxTypeInd.Text = objPRTrx.EnumADTrxTypeStatus.Daily Then
                ValidateDailyADCode()
                If lblErrCheckADCode.Visible = True Then
                    Exit Sub
                End If
                If Trim(txtEffDate.Text) = "" Then
                    lblRfvEffDate.visible = true
                    Exit Sub
                End If
                If ddlADCode.SelectedIndex = 0 Then
                        lblErrADCode.Visible = True
                        Exit Sub
                    ElseIf strAcc = ""  Then
                        If lblADType.Text = objPRSetup.EnumADType.Allowance Or lblADType.Text = objPRSetup.EnumADType.Deduction Or lblADType.Text = objPRSetup.EnumADType.LevyItem Then
                            lblErrAccount.Visible = True
                            Exit Sub
                        End If
                    End If

                    GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)
                    If strBlk = "" And blnIsBlockRequire = True And blnIsBalanceSheet = False Then
                        lblErrBlock.Visible = True
                        Exit Sub
                    ElseIf strBlk = "" And blnIsBalanceSheet = True And blnIsNurseryInd = True  Then
                        lblErrBlock.Visible = True
                        Exit Sub
                    ElseIf strVeh = "" And blnIsVehicleRequire = True  Then
                        lblErrVehicle.Visible = True
                        Exit Sub
                    ElseIf strExp = "" And blnIsVehicleRequire = True Then
                        lblErrVehExp.Visible = True
                        Exit Sub
                    ElseIf strVeh <> "" And strExp = "" And lblVehicleOption.Text = True Then
                        lblErrVehExp.Visible = True
                        Exit Sub
                    ElseIf strVeh = "" And strExp <> "" And lblVehicleOption.Text = True Then
                        lblErrVehicle.Visible = True
                        Exit Sub
                    End If
            End If
        End If

        lblErrChecking.Visible = False
        lblErrHouseRent.Visible = False
        lblErrTransport.Visible= False
        lblErrMeal.Visible= False
        lblErrMaternity.Visible= False
        lblErr3Maternity.Visible= False
        lblErrRelocation.Visible= False

        If lblTrxTypeInd.Text <> "" Then 
            If lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
                If lblhouseind.Text <> "" Then 
                    strEmpCode = ddlEmployee.SelectedItem.Value
                    strAllowance = ddlADCode.SelectedItem.Value
                    strVehCode= ""
                    strPayType= ""
                    strMaritalStatus= objHRTrx.EnumMarital.Married
                    ValidateHouseRentGiven()
                    if lblErrHouseRent.Visible = True Then
                        Exit Sub
                    End If 
                    ValidationEmployee(strEmpCode, "3", "2", strMaritalStatus, strVehCode, strPayType, strAllowance, "3", "2")
                    If lblErrChecking.Visible = True Then
                        Exit Sub
                    End if            
                End If

                If lblTransportInd.Text <> "" Then 
                    strEmpCode = ddlEmployee.SelectedItem.Value
                    strAllowance = ddlADCode.SelectedItem.Value
                    strEmpCategory= objHRSetup.EnumCategoryType.Staff
                    strEmpPosition= objHRSetup.EnumPosition.HQRegional
                    strVehCode= ""
                    strPayType= objPRSetup.EnumPayType.MonthlyRate  
                    If strSelectedADId = "" Then
                        ValidateTransportGiven()
                        if lblErrTransGiven.Visible = True Then
                            Exit Sub
                        End If 
                    End If         
                    ValidationEmployee(strEmpCode, strEmpCategory, strEmpPosition, "", strVehCode, strPayType, strAllowance,strEmpCategory,strEmpPosition)
                    If lblErrTransport.Visible = True Then
                        Exit Sub
                    End if
                    ValidateADCode ("Trans")
                    If lblErrCheckADCode.Visible = True Then
                        Exit Sub
                    End if
                End If

                If lblMealInd.Text <> "" Then 
                    strEmpCode = ddlEmployee.SelectedItem.Value
                    strAllowance = ddlADCode.SelectedItem.Value
                    strEmpCategory= objHRSetup.EnumCategoryType.Staff
                    strEmpPosition= objHRSetup.EnumPosition.HQRegional
                    strVehCode= ""
                    strPayType= objPRSetup.EnumPayType.MonthlyRate
                    If strSelectedADId = "" Then
                        ValidateMealGiven()
                        if lblErrMealGiven.Visible = True Then
                            Exit Sub
                        End If 
                    End If
                    ValidationEmployee(strEmpCode, strEmpCategory, strEmpPosition, "", strVehCode, strPayType, strAllowance,strEmpCategory,strEmpPosition)
                    If lblErrMeal.Visible = True Then
                        Exit Sub
                    End if
                    ValidateADCode ("Meal")
                    If lblErrCheckADCode.Visible = True Then
                        Exit Sub
                    End if
                End If

                If lblMaternityInd.Text <> "" Then 
                    strEmpCode = ddlEmployee.SelectedItem.Value
                    strAllowance = ddlADCode.SelectedItem.Value
                    strEmpCategory= objHRSetup.EnumCategoryType.Staff
                    strEmpPosition= objHRSetup.EnumPosition.HQRegional
                    strEmpCategory2= objHRSetup.EnumCategoryType.NonStaff
                    strEmpPosition2= objHRSetup.EnumPosition.Estate  
                    strVehCode= ""
                    strPayType= ""    
                    strMaritalStatus= objHRTrx.EnumMarital.Married      
                    ValidationEmployee(strEmpCode, strEmpCategory, strEmpPosition, strMaritalStatus, strVehCode, strPayType, strAllowance,strEmpCategory2,strEmpPosition2)
                    If lblErrMaternity.Visible = True Then
                        Exit Sub
                    End if
                    If strSelectedADId = "" Then
                        ValidateMaternityGiven()
                        If lblErr3Maternity.Visible = True Then
                            Exit Sub
                        End if
                    End If
                End If
                If lblRelocationInd.Text <> "" Then 
                    strEmpCode = ddlEmployee.SelectedItem.Value
                    strAllowance = ddlADCode.SelectedItem.Value
                    strEmpCategory= objHRSetup.EnumCategoryType.Staff
                    strEmpPosition= objHRSetup.EnumPosition.HQRegional
                    strEmpCategory2= objHRSetup.EnumCategoryType.NonStaff
                    strEmpPosition2= objHRSetup.EnumPosition.Estate  
                    strVehCode= ""
                    strPayType= ""    
                    strMaritalStatus= "" 
                    ValidationEmployee(strEmpCode, strEmpCategory, strEmpPosition, strMaritalStatus, strVehCode, strPayType, strAllowance,strEmpCategory,strEmpPosition)
                    If lblErrRelocation.Visible = True Then
                        Exit Sub
                    End if
                End If

                If txtBalAmount.Visible = True  Then
                    If CDbl(Trim(txtAmount.Text)) > CDbl(Trim(txtBalAmount1.Text)) Then
                        lblErrCheckBal.Visible = True 
                        Exit Sub
                    Else
                        lblErrCheckBal.Visible = False
                    End If
                    txtBalance.Text = CStr(CDbl(Trim(txtBalAmount1.Text)) - CDbl(Trim(txtAmount.Text)))
                    txtBalAmount.Text = CStr(CDbl(Trim(txtBalAmount1.Text)) - CDbl(Trim(txtAmount.Text)))
                End If
            End If
        End If
        
        InsertRecord(blnIsUpdated)
        If blnIsUpdated = False Then
            Exit Sub
        Else
            If lblTrxTypeInd.Text <> "" Then
                If lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
                    arrPeriod = Split(Trim(txtEffPeriod.Text), "/")
                    If (UBound(arrPeriod, 1) <> 1) Then
                        lblErrEffPeriod.Visible = True
                        Exit Sub
                    Else
                        intMonth = arrPeriod(0)
                        intYear = arrPeriod(1)

                        Try
                            strParam = "||" & Convert.ToString(intYear)             
                            intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCode_AccCfg, _
                                                                    strCompany, _
                                                                    strLocation, _
                                                                    strUserId, _
                                                                    strParam, _
                                                                    objAccCfg)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADDET_ACCCFG_MAX_GET&errmesg=" & Exp.ToString() & "&redirect=")
                        End Try

                        Try
                            intMaxPeriod = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADDET_ACCCFG_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & Convert.ToString(intYear) & "&redirect=")
                        End Try

                        If (intMonth < 1) Or (intMonth > intMaxPeriod) Or (intYear < strAccYear) Then
                            lblErrAccPeriod.Visible = True
                            Exit Sub
                        End If

                        If (intMonth < Convert.ToInt32(strAccMonth)) And (intYear <= Convert.ToInt32(strAccYear)) Then
                            lblErrAccPeriod.Visible = True
                            Exit Sub
                        End If
                    End If


                    If (Convert.ToInt32(ddlTrxType.SelectedItem.Value) = objPRTrx.EnumADTrxTypeStatus.OneTime And Convert.ToInt32(txtMonth.Text) <> 1) Or _
                        (lblHiddenSts.Text <> "0" And Convert.ToInt32(ddlTrxType.SelectedItem.Value) = objPRTrx.EnumADTrxTypeStatus.OneTime) And (dgLineDet.Items.Count = 1) Then
                        lblErrOneTime.Visible = True
                        Exit Sub
                    ElseIf (Convert.ToInt32(ddlTrxType.SelectedItem.Value) = objPRTrx.EnumADTrxTypeStatus.Permanent And Convert.ToInt32(txtMonth.Text) <> 1) Or _
                        (lblHiddenSts.Text <> "0" And Convert.ToInt32(ddlTrxType.SelectedItem.Value) = objPRTrx.EnumADTrxTypeStatus.Permanent) And (dgLineDet.Items.Count = 1) Then
                        lblErrPermanent.Visible = True
                        Exit Sub
                    ElseIf (Convert.ToInt32(ddlTrxType.SelectedItem.Value) = objPRTrx.EnumADTrxTypeStatus.Percentage) And (CDbl(Trim(txtAmount.Text)) > 100 Or CDbl(Trim(txtAmount.Text)) <= 0) Then
                        lblErrPercentage.Visible = True
                        Exit Sub
                    End If
                    If lblADType.Text <> objPRSetup.EnumADType.MemoItem And txtAmount.Text <= 0 Then
                        lblInvalidRange.Visible = True
                        Exit Sub
                    End If
                End If
            End If

            If lblTrxTypeInd.Text <> "" Then
                If lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
                    Try
                        strParam = strSelectedADId & "|" & _
                                    intMonth & "|" & _
                                    intYear & "|" & _
                                    Trim(txtAmount.Text) & "|" & _
                                    objPRTrx.EnumADTrxLnStatus.Active & "|" & _
                                    Trim(txtMonth.Text)
         

                        intErrNo = objPRTrx.mtdValidateADTrxLine(strOpCode_SearchLine, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strAccMonth, _
                                                                strAccYear, _
                                                                strParam, _
                                                            objFound)

                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRXLINE_VALIDATE&errmesg=" & Exp.ToString() & "&redirect=")
                    End Try

                    If objFound = True Then
                        lblErrPeriodFound.Visible = True
                        Exit Sub
                    End If
                End If

                If lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
                    strAcc = ""
                    strBlk = ""
                    strVeh = ""
                    strExp = ""
                    strAD = IIf(strAD = "", ddlADCode.SelectedItem.Value, strAD)
                    arrAD = Split(strAD, "|")
                    strADCode = arrAD(0)   
                Else
                    strAcc = ddlAccount.SelectedItem.Value.ToString
                    strBlk = ddlBlock.SelectedItem.Value.ToString
                    strVeh = ddlVehCode.SelectedItem.Value.ToString
                    strExp = ddlVehExpCode.SelectedItem.Value.ToString

                    strAD = IIf(strAD = "", ddlADCode.SelectedItem.Value, strAD)
                    arrAD = Split(strAD, "|")
                    strADCode = arrAD(0)   

                    intMonth = CInt(strAccMonth)
                    intYear = CInt(strAccYear)

                End If
                Try
                    strParam = strSelectedADId & "|" & _
                                Convert.ToString(intMonth) & "|" & _
                                Convert.ToString(intYear) & "|" & _
                                Trim(txtAmount.Text) & "|" & _
                                Convert.ToString(objPRTrx.EnumADTrxLnStatus.Active) & "|" & _
                                Trim(txtMonth.Text)& "|" & _
                                IIf((Trim(txtBalance.Text) = ""), 0, Trim(txtBalance.Text)) & "|" & _
                                Trim(strADCode) & "|" & _
                                Trim(strAcc) & "|" & _
                                Trim(strBlk) & "|" & _
                                Trim(strVeh) & "|" & _
                                Trim(strExp)

                    intErrNo = objPRTrx.mtdUpdADTrxLine(strOpCode_UpdID, _
                                                        strOpCode_AddLine, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        False, _
                                                        objResult)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_ADdet.aspx?adid=" & strSelectedADId)
                End Try
                onLoad_Display()
                onLoad_DisplayLine()
                onLoad_BindButton()
            End If
        End If

        objResult = Nothing
        objAccCfg = Nothing
    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSTRX_ADTRX_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_ADTRX_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_ADTRX_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_ADTRX_STATUS_UPD"
        Dim strOpCd As String
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim strAD As String = Request.Form("ddlADCode")
        Dim strTrxType As String = Request.Form("ddlTrxType")
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")
        Dim blnIsUpdated As Boolean
        Dim objADTrxId As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strParam As String = "" 
        Dim arrAD As Array
        Dim strADCode As String
        Dim strADType As String

        Dim strEmpCode As String  
        Dim strAllowance As String
        Dim strVehCode as String
        Dim strPayType as String
        Dim strMaritalStatus as String
        Dim strEmpCategory as String
        Dim strEmpPosition as String
        Dim strEmpCategory2 as String
        Dim strEmpPosition2 as String

        strEmp = IIf(strEmp = "", ddlEmployee.SelectedItem.Value, strEmp)
        strAD = IIf(strAD = "", ddlADCode.SelectedItem.Value, strAD)
        arrAD = Split(strAD, "|")
        strADCode = arrAD(0)
        strADType = arrAD(1)
        lblADType.Text = arrAD(1)
        strTrxType = IIf(strTrxType = "", ddlTrxType.SelectedItem.Value, strTrxType)
        strAcc = IIf(strAcc = "", ddlAccount.SelectedItem.Value, strAcc)
        strBlk = IIf(strBlk = "", ddlBlock.SelectedItem.Value, strBlk)
        strVeh = IIf(strVeh = "", ddlVehCode.SelectedItem.Value, strVeh)
        strVehExp = IIf(strVehExp = "", ddlVehExpCode.SelectedItem.Value, strVehExp)

        If strCmdArgs = "New" Then
            Response.Redirect("PR_trx_ADDet.aspx?" & _
                            "adcd=" & strADCode & _
                            "&adtype=" & strTrxType & _
                            "&acc=" & strAcc & _
                            "&blk=" & strBlk & _
                            "&veh=" & strVeh & _
                            "&vehexp=" & strVehExp & _
                            "&type=new")
            lblMedicalInd.Text = "1"
        End If

        If strEmp = "" Then
            lblErrEmp.Visible = True
            Exit Sub
        ElseIf ddlTrxType.SelectedItem.Value = "" Then
            lblErrTrxType.Visible = True
            Exit Sub
        ElseIf strADCode = "" And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            lblErrADCode.Visible = True
            Exit Sub
        ElseIf strAcc = "" And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            If lblADType.Text = objPRSetup.EnumADType.Allowance Or lblADType.Text = objPRSetup.EnumADType.Deduction Or lblADType.Text = objPRSetup.EnumADType.LevyItem Then
                lblErrAccount.Visible = True
                Exit Sub
            End If
        ElseIf (ddlBlock.Items.Count > 1) And (strBlk = "")  And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            lblErrBlock.Visible = True
            Exit Sub
        ElseIf (ddlVehCode.Items.Count > 1) And (strVeh = "") And (lblVehicleOption.Text = False)  And lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then
            lblErrVehicle.Visible = True
            Exit Sub
        End If

        If lblTrxTypeInd.Text <> "" Then
            If lblTrxTypeInd.Text = objPRTrx.EnumADTrxTypeStatus.Daily Then
                If Trim(txtEffDate.Text) = "" Then
                    lblRfvEffDate.visible = true
                    Exit Sub
                End If
            End If
        End If

        lblErrChecking.Visible = False
        lblErrTransport.Visible= False
        lblErrMeal.Visible= False
        lblErrMaternity.Visible= False
        lblErr3Maternity.Visible= False
        lblErrRelocation.Visible= False
        If lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.Daily Then       
            If lblhouseind.Text <> "" Then 
                strEmpCode = ddlEmployee.SelectedItem.Value
                strAllowance = ddlADCode.SelectedItem.Value
                strVehCode= ""
                strPayType= ""
                strMaritalStatus= objHRTrx.EnumMarital.Married
                ValidationEmployee(strEmpCode, "3", "2", strMaritalStatus, strVehCode, strPayType, strAllowance, "3", "2")
                If lblErrChecking.Visible = True Then
                    Exit Sub
                End if            
            End If

            If lblTransportInd.Text <> "" Then 
                If strCmdArgs <> "Cancel" Then
                    strEmpCode = ddlEmployee.SelectedItem.Value
                    strAllowance = ddlADCode.SelectedItem.Value
                    strEmpCategory= objHRSetup.EnumCategoryType.Staff
                    strEmpPosition= objHRSetup.EnumPosition.HQRegional
                    strVehCode= ""
                    strPayType= objPRSetup.EnumPayType.MonthlyRate
                    If strSelectedADId = "" Then
                        ValidateTransportGiven()
                        if lblErrTransGiven.Visible = True Then
                            Exit Sub
                        End If 
                    End If
                    ValidationEmployee(strEmpCode, strEmpCategory, strEmpPosition, "", strVehCode, strPayType, strAllowance,strEmpCategory,strEmpPosition)
                    If lblErrTransport.Visible = True Then
                        Exit Sub
                    End if
                    ValidateADCode ("Trans")
                    If lblErrCheckADCode.Visible = True Then
                        Exit Sub
                    End if
                End If
            End If

            If lblMealInd.Text <> "" Then 
                If strCmdArgs <> "Cancel" Then
                    strEmpCode = ddlEmployee.SelectedItem.Value
                    strAllowance = ddlADCode.SelectedItem.Value
                    strEmpCategory= objHRSetup.EnumCategoryType.Staff
                    strEmpPosition= objHRSetup.EnumPosition.HQRegional
                    strVehCode= ""
                    strPayType= objPRSetup.EnumPayType.MonthlyRate
                    If strSelectedADId = "" Then
                        ValidateMealGiven()
                        if lblErrMealGiven.Visible = True Then
                            Exit Sub
                        End If 
                    End If
                    ValidationEmployee(strEmpCode, strEmpCategory, strEmpPosition, "", strVehCode, strPayType, strAllowance,strEmpCategory,strEmpPosition)
                    If lblErrMeal.Visible = True Then
                        Exit Sub
                    End if
                    ValidateADCode ("Meal")
                    If lblErrCheckADCode.Visible = True Then
                        Exit Sub
                    End if
                End If
            End If

            If lblMaternityInd.Text <> "" Then 
                If strCmdArgs <> "Cancel" Then
                    strEmpCode = ddlEmployee.SelectedItem.Value
                    strAllowance = ddlADCode.SelectedItem.Value
                    strEmpCategory= objHRSetup.EnumCategoryType.Staff
                    strEmpPosition= objHRSetup.EnumPosition.HQRegional
                    strEmpCategory2= objHRSetup.EnumCategoryType.NonStaff
                    strEmpPosition2= objHRSetup.EnumPosition.Estate  
                    strVehCode= ""
                    strPayType= ""    
                    strMaritalStatus= objHRTrx.EnumMarital.Married      
                    ValidationEmployee(strEmpCode, strEmpCategory, strEmpPosition, strMaritalStatus, strVehCode, strPayType, strAllowance,strEmpCategory2,strEmpPosition2)
                    If lblErrMaternity.Visible = True Then
                        Exit Sub
                    End if
                    If strSelectedADId = "" Then
                        ValidateMaternityGiven()
                        If lblErr3Maternity.Visible = True Then
                            Exit Sub
                        End if
                    End If
                End If
            End If

            If lblRelocationInd.Text <> "" Then 
                If strCmdArgs <> "Cancel" Then
                    strEmpCode = ddlEmployee.SelectedItem.Value
                    strAllowance = ddlADCode.SelectedItem.Value
                    strEmpCategory= objHRSetup.EnumCategoryType.Staff
                    strEmpPosition= objHRSetup.EnumPosition.HQRegional
                    strEmpCategory2= objHRSetup.EnumCategoryType.NonStaff
                    strEmpPosition2= objHRSetup.EnumPosition.Estate  
                    strVehCode= ""
                    strPayType= ""    
                    strMaritalStatus= ""      
                    ValidationEmployee(strEmpCode, strEmpCategory, strEmpPosition, strMaritalStatus, strVehCode, strPayType, strAllowance,strEmpCategory2,strEmpPosition2)
                    If lblErrRelocation.Visible = True Then
                        Exit Sub
                    End if    
                End If        
            End If
        End If

        If strCmdArgs = "Save" Then
            InsertRecord(blnIsUpdated)
        ElseIf strCmdArgs = "Cancel" Then
            strParam = strSelectedADId & "|" & objPRTrx.EnumADTrxStatus.Cancelled
            Try
                intErrNo = objPRTrx.mtdUpdADTrx(strOpCd_Sts, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True, _
                                                objADTrxId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_ADTRX_CANCEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_ADdet.aspx?adid=" & strSelectedADId)
            End Try
        ElseIf strCmdArgs = "Close" Then
            strParam = strSelectedADId & "|" & objPRTrx.EnumADTrxStatus.Closed
            Try
                intErrNo = objPRTrx.mtdUpdADTrx(strOpCd_Sts, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True, _
                                                objADTrxId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_ADTRX_CLOSE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_ADdet.aspx?adid=" & strSelectedADId)
            End Try
        End If

        If strSelectedADId <> "" Then
            onLoad_Display()
            onLoad_DisplayLine()
            onLoad_BindButton()
        End If
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "PR_CLSTRX_ADTRX_LINE_DEL"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_ADTRX_STATUS_UPD"
        Dim strParam As String
        Dim lblText As Label
        Dim strEffMonth As String
        Dim strEffYear As String
        Dim intErrNo As Integer
        Dim strAmount As String
        Dim strADCode As String
        Dim strAcc As String
        Dim strBlk As String
        Dim strVeh As String
        Dim strExp As String    

        dgLineDet.EditItemIndex = Convert.ToInt32(E.Item.ItemIndex)
        lblText = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("EffMonth")
        strEffMonth = lblText.Text
        lblText = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("EffYear")
        strEffYear = lblText.Text
        lblText = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("Amount")
        strAmount = lblText.Text
        lblText = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("ADCode")
        strADCode = lblText.Text
        lblText = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("AccCode")
        strAcc = lblText.Text
        lblText = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("BlkCode")
        strBlk = lblText.Text
        lblText = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("VehCode")
        strVeh = lblText.Text
        lblText = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("ExpenseCode")
        strExp = lblText.Text

        Try
           
                strParam = strSelectedADId & "|" & _
                         strEffMonth & "|" & _
                         strEffYear & "|" & strAmount & "|" & objPRTrx.EnumADTrxStatus.Active & "|||" & _
                         Trim(strADCode)  & "|" & _
                            Trim(strAcc) & "|" & _
                            Trim(strBlk) & "|" & _
                            Trim(strVeh) & "|" & _
                            Trim(strExp)

            intErrNo = objPRTrx.mtdUpdADTrxLine(strOpCode_UpdID, _
                                                strOpCode_DelLine, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                False, _
                                                objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_LINE_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_ADdet.aspx?adid=" & strSelectedADId)
        End Try

        onLoad_Display()
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub


    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_ADList.aspx")
    End Sub
    

    Sub onChange_ADCode(Sender As Object, E As EventArgs)
        Dim objADDs As New Dataset()
        Dim strOpCd As String = "PR_CLSSETUP_AD_GET"
        Dim strParam As String = Request.Form("ddlADCode")
        Dim intErrNo As Integer
        Dim arrAD As Array
        Dim strADCode As String
        Dim strADType As String
        Dim strAD as String

        lblErrMedical3.visible = false 
        txtBalAmount.visible = false 

        strAD = IIf(strAD = "", ddlADCode.SelectedItem.Value, strAD)
        arrAD = Split(strAD, "|")
        strADCode = arrAD(0)
        strADType = arrAD(1)        
        txtAmount.Text = ""

        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCd, _
                                           strADCode, _
                                           objADDs, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_AD_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objADDs.Tables(0).Rows.Count > 0 Then      
    
            BindAccount(objADDs.Tables(0).Rows(0).Item("DefAccCode"))
            BindBlock(objADDs.Tables(0).Rows(0).Item("DefAccCode"), objADDs.Tables(0).Rows(0).Item("DefBlockCode"))
            If Trim(objADDs.Tables(0).Rows(0).Item("VehCode")) = "" Then
                BindVehicle("%","") 
            Else
                BindVehicle(ddlADCode.SelectedItem.Value,objADDs.Tables(0).Rows(0).Item("VehCode")) 
            End If

            If Trim(objADDs.Tables(0).Rows(0).Item("ExpenseCode")) = "" Then
                BindVehicleExpense(True,objADDs.Tables(0).Rows(0).Item("ExpenseCode"))
            Else
                BindVehicleExpense(False,objADDs.Tables(0).Rows(0).Item("ExpenseCode"))
            End If
            If(Trim(objADDs.Tables(0).Rows(0).Item("HouseInd"))) = "1" Then
                lblhouseind.Text = Trim(objADDs.Tables(0).Rows(0).Item("HouseInd"))
            Else
                lblhouseind.Text = ""
            End If

            If(Trim(objADDs.Tables(0).Rows(0).Item("TransportInd"))) = "1" Then
                lblTransportInd.Text = Trim(objADDs.Tables(0).Rows(0).Item("TransportInd"))
                If lblTrxTypeInd.Text <> "" Then
                    If lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.OneTime Then
                        lblErrTypeAD.Visible = True
                    End If
                End If
            Else
                lblTransportInd.Text = ""
            End If

            If(Trim(objADDs.Tables(0).Rows(0).Item("MaternityInd"))) = "1" Then
                lblMaternityInd.Text = Trim(objADDs.Tables(0).Rows(0).Item("MaternityInd"))
                GetMaternityAmount(ddlEmployee.SelectedItem.Value.ToString,ddlADCode.SelectedItem.Value.ToString)
                If lblTrxTypeInd.Text <> "" Then
                    If lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.OneTime Then
                        lblErrTypeAD.Visible = True
                    End If
                End If
            Else
                lblMaternityInd.Text = ""               
            End If

            If(Trim(objADDs.Tables(0).Rows(0).Item("MealInd"))) = "1" Then
                lblMealInd.Text = Trim(objADDs.Tables(0).Rows(0).Item("MealInd"))
                If lblTrxTypeInd.Text <> "" Then
                    If lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.OneTime Then
                        lblErrTypeAD.Visible = True
                    End If
                End If
            Else
                lblMealInd.Text = ""
            End If
            
            If(Trim(objADDs.Tables(0).Rows(0).Item("RelocationInd"))) = "1" Then
                lblRelocationInd.Text = Trim(objADDs.Tables(0).Rows(0).Item("RelocationInd"))
                GetRelocationAmount(ddlADCode.SelectedItem.Value.ToString)
                If lblTrxTypeInd.Text <> "" Then
                    If lblTrxTypeInd.Text <> objPRTrx.EnumADTrxTypeStatus.OneTime Then
                        lblErrTypeAD.Visible = True
                    End If
                End If
            Else
                lblRelocationInd.Text = ""                
            End If

  
            If(Trim(objADDs.Tables(0).Rows(0).Item("MedicalInd"))) = "1" And Trim(ddlEmployee.SelectedItem.Value.ToString) <> "" Then
                CheckMedicalClosed(ddlEmployee.SelectedItem.Value.ToString)
                lblMedicalInd.Text = Trim(objADDs.Tables(0).Rows(0).Item("MedicalInd"))
                lblBalAmount.Visible = True
                txtBalAmount.Visible = True
                txtBalAmount.Enabled = False
                GetBalanceAmount(ddlEmployee.SelectedItem.Value.ToString)
            ElseIf (Trim(objADDs.Tables(0).Rows(0).Item("MedicalInd"))) = "1" And Trim(ddlEmployee.SelectedItem.Value.ToString) = "" Then
                CheckMedicalClosed(ddlEmployee.SelectedItem.Value.ToString)
                lblMedicalInd.Text = Trim(objADDs.Tables(0).Rows(0).Item("MedicalInd"))
                lblBalAmount.Visible = True
                txtBalAmount.Visible = True
                txtBalAmount.Enabled = False
            Else
                lblMedicalInd.Text = ""
                lblBalAmount.Visible = False
                txtBalAmount.Visible = False
                txtBalAmount.Enabled = False
            End If
        End If
    End Sub
    Sub onChange_Employee(Sender As Object, E As EventArgs)
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strType As String = Request.QueryString("type")
        If Trim(lblMaternityInd.Text) <> "" Then
            GetMaternityAmount(ddlEmployee.SelectedItem.Value.ToString,ddlADCode.SelectedItem.Value.ToString)
        End If

        If Trim(lblMedicalInd.Text) <> "" Then
            GetBalanceAmount(ddlEmployee.SelectedItem.Value.ToString)
            lblBalAmount.Visible = True
            txtBalAmount.Visible = True
        End If

        If Trim(lblRelocationInd.Text) <> "" Then
            BindRelocationAmt(ddlADCode.SelectedItem.Value.ToString)
        End If
        If strType = "new" Then
                        BindBalanceAmt(strSelectedADId)
                              
                        BindMaternityAmt(ddlADCode.SelectedItem.Value.ToString)
                        BindRelocationAmt(ddlADCode.SelectedItem.Value.ToString)
                        BindIndicator(ddlADCode.SelectedItem.Value.ToString)
        End If

        If txtEffDate.Text <> "" Then
            If Trim(ddlEmployee.SelectedItem.Value.ToString) <> "" Then
                onload_Display()
                onLoad_DisplayLine()
                onLoad_BindButton()
            End If
        End If
        
    End Sub

    Sub ValidationEmployee(ByRef pr_EmpCode As String, _
                         ByRef pr_EmpCategory As String, _
                         ByRef pr_EmpPosition As String, _
                         ByRef pr_EmpMaritalStatus As String, _
                         ByRef pr_EmpVehCode As String, _
                         ByRef pr_EmpPayType As String, _
                         ByRef pr_ADCode As String, _
                         ByRef pr_EmpCategory2 As String, _
                         ByRef pr_EmpPosition2 As String)

        Dim objADTrxDs As New Dataset()
        Dim strOpCd_Get As String = "PR_CLSTRX_ADTRX_CHECKEMP_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        
        If lblMaternityInd.Text <> "" Then
            strOpCd_Get = "PR_CLSTRX_ADTRX_CHECKMATERNITY_GET" 
        End If

        If pr_EmpCode <> "" and pr_ADCode <> "" Then
            If lblhouseind.Text <> "" Then
            strParam = pr_EmpCode & "|" & _
                       pr_EmpCategory & "|" & _ 
                       pr_EmpPosition & "|" & _
                       pr_EmpMaritalStatus & "|||" & _
                       pr_EmpCategory2 & "|" & _
                       pr_EmpPosition2
                              
            End If

            If lblTransportInd.Text <> "" Then
            strParam = pr_EmpCode & "|" & _
                       pr_EmpCategory & "|" & _ 
                       pr_EmpPosition & "||" & _                       
                       pr_EmpVehCode & "|" & _
                       pr_EmpPayType & "|" & _
                       pr_EmpCategory2 & "|" & _
                       pr_EmpPosition2
            End If
            If lblMealInd.Text <> "" Then
            strParam = pr_EmpCode & "|" & _
                       pr_EmpCategory & "|" & _ 
                       pr_EmpPosition & "||" & _                       
                       pr_EmpVehCode & "|" & _
                       pr_EmpPayType & "|" & _
                       pr_EmpCategory2 & "|" & _
                       pr_EmpPosition2
            End If
            If lblMaternityInd.Text <> "" Then
            strParam = pr_EmpCode & "|" & _
                       pr_EmpCategory & "|" & _ 
                       pr_EmpPosition & "|" & _
                       pr_EmpMaritalStatus & "|||" & _
                       pr_EmpCategory2 & "|" & _
                       pr_EmpPosition2
                              
            End If
            
            If lblRelocationInd.Text <> "" Then
            strParam = pr_EmpCode & "|" & _
                       pr_EmpCategory & "|" & _ 
                       pr_EmpPosition & "|||||" & _                        
                       pr_EmpCategory2 & "|" & _
                       pr_EmpPosition2
                              
            End If
                        

            Try
                intErrNo = objPRTrx.mtdCheckEmployee(strOpCd_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strParam, _
                                                objADTrxDs, _
                                                False)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADDET_GET_CHECKEMP&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_ADDet.aspx")
            End Try

            If objADTrxDs.Tables(0).Rows.Count > 0 Then
                If lblhouseind.Text <> "" Then
                    lblErrChecking.Visible = False
                End If
                If lblTransportInd.Text <> "" Then
                    lblErrTransport.Visible = False
                End If  
                 If lblMealInd.Text <> "" Then
                    lblErrMeal.Visible = False
                End If 
                If lblMaternityInd.Text <> "" Then
                    lblErrMaternity.Visible = False
                End If   
                If lblRelocationInd.Text <> "" Then
                    lblErrRelocation.Visible = False
                End If              
            Else
                If lblhouseind.Text <> "" Then
                    lblErrChecking.Visible = True 
                End If   
                If lblTransportInd.Text <> "" Then
                    lblErrTransport.Visible = True
                End If
                 If lblMealInd.Text <> "" Then
                    lblErrMeal.Visible = True
                End If
                If lblMaternityInd.Text <> "" Then
                    lblErrMaternity.Visible = True
                End If
                If lblRelocationInd.Text <> "" Then
                    lblErrRelocation.Visible = True
                End If
            End If 
        End If 
    End Sub

    Sub GetBalanceAmount(ByRef pr_EmpCode As String)
        Dim objEmpDs As New Dataset()
        Dim objDs As New Dataset()
        Dim strOpCd_Get As String = "PR_CLSTRX_ADTRX_BALANCEAMOUNT_MEDICAL_GET"
        Dim strOpCd_GetEmp As String = "PR_CLSTRX_ADTRX_EMPLOYEE_MEDICAL_GET"
        Dim strOpCd_Check As String = "PR_CLSTRX_ADTRX_BALANCEAMOUNT_MEDICAL_CHECK"
        Dim strOpCd_Chk as string = "PR_CLSTRX_ADTRX_CHECK_ADMEDIC"
        Dim strADMedic as string = ddlADCode.SelectedItem.Value
        Dim strParam As String
        Dim dtMonth As Date 
        Dim dblBSalary As Double
        Dim dblRate As Double
        Dim intErrNo As Integer
        Dim dblBalance As Double
        Dim dblBalanceAmt As Double
        Dim objADMedic as New Dataset()

        strParam = trim(pr_EmpCode) & "|" & Trim(strADMedic)   

        Try
             intErrNo = objPRTrx.mtdCheckADCode(strOpCd_Chk, _
                                           strParam, _
                                           objADMedic)
        Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_AD_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objADMedic.Tables(0).Rows.Count = 0 Then
            lblErrMedical3.visible = true 
            Exit Sub
        End If

        strParam = pr_EmpCode & "||" & pr_EmpCode & "|" & strPhyYear
       
        lblMedicalInd.Text = "1"

        Try
            intErrNo = objPRTrx.mtdGetEmpMedicalAmt(strOpCd_Get, strParam, objEmpDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objEmpDs.Tables(0).Rows.Count > 0 Then
              dtMonth = objEmpDs.Tables(0).Rows(0).Item("AppJoinDate") 
              dblBSalary = objEmpDs.Tables(0).Rows(0).Item("PayRate")
              dblRate = objEmpDs.Tables(0).Rows(0).Item("MedicalRate")
              dblBalanceAmt = objEmpDs.Tables(0).Rows(0).Item("balanceamount")

              If dblRate = 0 Then
                    lblErrMedical1.Visible = True
                    Exit Sub
              End If  

              strParam = pr_EmpCode & "|1|" & pr_EmpCode & "|" & strPhyYear


              Try
                    intErrNo = objPRTrx.mtdGetEmpMedicalAmt(strOpCd_GetEmp, strParam, objDs)
              Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
              End Try
        
              If objDs.Tables(0).Rows.Count > 0 Then
                    If Trim(txtEffPeriod.Text) <> "" Then
                        If Len(txtEffPeriod.Text) = 6 And Left(txtEffPeriod.Text,1) = "1" Then
                            dblBalance = dblRate * dblBSalary
                            lblErrMedical.Visible = False
                            btnAdd.Visible = True
                        Else 
                            if flgMedCloseInd <> "" Then
                                lblErrMedical.Visible = false
                                btnAdd.Visible = True
                            Else
                                if lblFlgMedCheckEmp.Text <> "" Then
                                    lblErrMedical.Visible = false
                                    btnAdd.Visible = true
                                Else
                                    if strPhyMonth <> "1" then
                                        lblErrMedical.Visible = true
                                    else 
                                        lblErrMedical.Visible = false
                                    end if 
                                    btnAdd.Visible = False
                                End If
                            End If
                            lblMedicalInd.Text = "1"
                        End If
                    Else
                        If strPhyMonth = 1 Then
                            dblBalance = dblRate * dblBSalary
                            lblErrMedical.Visible = False
                            btnAdd.Visible = True
                            txtBalAmount.Text = dblBalance
                            txtBalAmount1.Text = dblBalance
                            txtBalAmount.Text = ObjGlobal.GetIDDecimalSeparator(txtBalAmount.Text)
                        Else 
                            strParam = pr_EmpCode & "|1|" & pr_EmpCode & "|" & strPhyYear
                            Try
                                intErrNo = objPRTrx.mtdGetEmpMedicalAmt(strOpCd_Check, strParam, objDs)
                            Catch Exp As Exception
                                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
                            End Try
                            If objDs.Tables(0).Rows.Count = 0 Then
                                dblBalance = dblRate * dblBSalary
                                lblErrMedical.Visible = False
                                btnAdd.Visible = True
                                txtBalAmount.Text = dblBalance
                                txtBalAmount1.Text = dblBalance
                                txtBalAmount.Text = ObjGlobal.GetIDDecimalSeparator(txtBalAmount.Text)
                            end if

                            if flgMedCloseInd <> "" Then
                                lblErrMedical.Visible = false
                                btnAdd.Visible = True
                            Else
                                if lblFlgMedCheckEmp.Text <> "" Then
                                    lblErrMedical.Visible = false
                                    btnAdd.Visible = true
                                Else
                                    if strPhyMonth <> "1" then
                                        lblErrMedical.Visible = true
                                    else
                                        lblErrMedical.Visible = false
                                    end if 
                                    btnAdd.Visible = False
                                End If
                            End If
                            lblMedicalInd.Text = "1"
                        End If 
                    End If
              Else
                    if txtEffPeriod.Text = "" then 
                        txtEffPeriod.Text = strAccMonth & "/" & strAccYear
                    end if
                    If Year(dtMonth) = Right(Trim(txtEffPeriod.Text),4) Then
                        dblBalance = ((13 - Month(dtMonth))/12) * (dblRate * dblBSalary)
                        txtBalAmount.Text = dblBalance
                        txtBalAmount1.Text = dblBalance
                        txtBalAmount.Text = ObjGlobal.GetIDDecimalSeparator(txtBalAmount.Text)
                    Else 
                        dblBalance = (dblRate * dblBSalary)
                        txtBalAmount.Text = dblBalance
                        txtBalAmount1.Text = dblBalance
                        txtBalAmount.Text = ObjGlobal.GetIDDecimalSeparator(txtBalAmount.Text)
                    End If
                    btnAdd.Visible = True 
              End If 
              
              txtBalAmount.Enabled = False
        End If
    End Sub

    Sub BindBalanceAmt(ByVal pv_strADId As String)
        Dim objDS As New Dataset
        Dim strOpCd As String = "PR_CLSTRX_ADTRX_BALANCE_SEARCH"
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dblPayRate As Double
        Dim dblMedRate As Double
        Dim dblAmount As Double
        Dim dblBalanceAmt as Double

        if Trim(pv_strADId) <> "" then
        strParam = Trim(pv_strADId) & "|" & Trim(strPhyYear)

        Try
            intErrNo = objPRTrx.mtdGetADTrx1(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objDS, _
                                            false)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_BALANCE_SEARCH&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objDS.Tables(0).Rows.Count > 0 Then
            If objDS.Tables(0).Rows(0).Item("MedicalInd") = "1" Then
                If (strPhyMonth = 1) and (strPhyYear <> objDS.Tables(0).Rows(0).Item("EffYear")) Then
                    GetBalanceAmount(ddlEmployee.SelectedItem.Value)
                Else

                    dblPayRate = objDS.Tables(0).Rows(0).Item("PayRate")
                    dblMedRate = objDS.Tables(0).Rows(0).Item("MedicalRate")
                    dblAmount = objDS.Tables(0).Rows(0).Item("Amount")
                    dblBalanceAmt = objDS.Tables(0).Rows(0).Item("BalanceAmt")

                    txtBalAmount.Text = CStr(dblBalanceAmt)
                    txtBalAmount1.Text = CStr(dblBalanceAmt)

                    txtBalAmount.Enabled = False

                    If dblBalanceAmt < = 0 then
                        lblErrMedical2.Visible = True
                        txtBalAmount.Text = "0"
                        txtBalAmount1.Text = "0"
                    End If
                    txtBalAmount.Text = ObjGlobal.GetIDDecimalSeparator(txtBalAmount.Text)
                End If
                lblBalAmount.Visible = True 
                txtBalAmount.Visible = True 
                txtBalAmount.Enabled = False
            Else                
                lblBalAmount.Visible = False 
                txtBalAmount.Visible = False 
                txtBalAmount.Enabled = False
            End If 
        Else
            GetBalanceAmount(ddlEmployee.SelectedItem.Value)   
            lblBalAmount.Visible = true 
            txtBalAmount.Visible = true 
            txtBalAmount.Enabled = false
        End If 
    End If
    End Sub
    Sub GetMaternityAmount(pv_strEmpCode as String,pv_strADCode as String)

        Dim strOpCd As String = "PR_CLSTRX_ADTRX_MATERNITYAMOUNT_GET"
        Dim strParam As String 
        Dim intErrNo As Integer        
        Dim objDS As New Dataset
        Dim strADCode As String
        Dim arrADCode as Array

        arrADCode = Split(pv_strADCode, "|")
        strADCode = arrADCode(0)

        strParam =  Trim(pv_strEmpCode) & "|" & Trim(strADCode)
        Try
            intErrNo = objPRTrx.mtdGetMaternityAmount(strOpCd, _   
                                                        strCompany, _
                                                        strLocation, _                                         
                                                        strParam, _
                                                        objDS)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_ADTRX_MATERNITYAMOUNT_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then            
            txtAmount.Text = objDs.Tables(0).Rows(0).Item("Amount")
        Else
           If Trim(ddlEmployee.SelectedItem.Value) <> ""  Then           
                lblErrNoMaternity.Visible = True
                txtAmount.Text = ""
           End If  
           
        End If
    End Sub

    Sub ValidateMaternityGiven()
        Dim objDs As New Dataset()
        Dim strOpCd As String = "PR_CLSTRX_ADTRX_MATERNITY_GIVEN"
        Dim strADTrxId As String
        Dim arrADCode as Array
        Dim arrEmpCode as Array
        Dim strDesc As String
        Dim strEmp As String
        Dim strStatus As String
        Dim strLastUpdate As String     
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strSort as String
        Dim strSortCol as String

        arrADCode = Split(ddlADCode.SelectedItem.Value, "|")
        strADTrxId = arrADCode(0)
        strDesc = "" 
        arrEmpCode = Split(ddlEmployee.SelectedItem.Value, "|")      
        strEmp = arrEmpCode(0)
        strStatus = objPRTrx.EnumADTrxStatus.Active & "','" &  objPRTrx.EnumADTrxStatus.Closed                


        strParam =  Right(Trim(txtEffPeriod.Text), 4) & "|" & _
                    strEmp & "|" & _
                    strStatus & "|" & _
                    strLocation & "|" & _
                    strADTrxId 
        
        Try
            intErrNo = objPRTrx.mtdCheckEmployee(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strParam, _
                                            objDs, _
                                            True)
        
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try       

        If objDs.Tables(0).Rows.Count = 3 Then
            lblErr3Maternity.Visible = True
        End If

    End Sub
    Sub GetRelocationAmount(pv_strADCode as String)
        Dim strOppCd_Amt as String = "PR_CLSSETUP_RELOCATION_LIST_GET"
        Dim strOppCd_Rate as String = "HR_CLSTRX_PAYROLL_GET"
        Dim strParam As String
        Dim SearchStr As String        
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim objDSAmt As New DataSet
        Dim objDSRate As New DataSet
        Dim sortItem As String = ""
        Dim arrADCode As Array
        Dim arrEmpCode As Array
        Dim strEmp As String
        Dim strADCode As String
        Dim strStatus As String
        Dim dblTotalAmount As Double
        Dim dblAmount As Double
        Dim dblPayRate As Double

        arrADCode = Split(pv_strADCode, "|")
        strADCode = arrADCode(0)       
        
        SearchStr = " AND R.ADCode ='" & Trim(strADCode)  & "' "
        strParam = sortItem & "|" & SearchStr

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOppCd_Amt, strParam, objPRSetup.EnumPayrollMasterType.Relocation, objDSAmt)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_RELOCATION_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        
        If objDSAmt.Tables(0).Rows.Count > 0 Then
            dblAmount = objDSAmt.Tables(0).Rows(0).Item("Amount")            
            arrEmpCode = Split(ddlEmployee.SelectedItem.Value, "|")      
            strEmp = arrEmpCode(0)
            strParam = strEmp & "|||EmpCode|"
                       
            Try
                intErrNo = objHRTrx.mtdGetEmployeePay(strOppCd_Rate, strParam, objDSRate)
            Catch Exp As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_CLSTRX_PAYROLL_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try
            If objDSRate.Tables(0).Rows.Count > 0                
                dblPayRate = objDSRate.Tables(0).Rows(0).Item("PayRate")                
                Select Case Trim(objDSAmt.Tables(0).Rows(0).Item("Type"))
                    Case objPRSetup.EnumRelocationType.EstateMill_HQRegional
                        dblTotalAmount = dblAmount + (4 * dblPayRate)                      
                    Case objPRSetup.EnumRelocationType.Regional_HQ
                        dblTotalAmount = dblAmount + (4 * dblPayRate) 
                    Case objPRSetup.EnumRelocationType.HQ_Regional    
                        dblTotalAmount = dblAmount + (4 * dblPayRate)
                    Case objPRSetup.EnumRelocationType.EstateMill_EstateMill
                        dblTotalAmount = dblAmount  
                    Case objPRSetup.EnumRelocationType.HQRegional_EstateMill  
                        dblTotalAmount = dblAmount  
                End Select
                txtAmount.Text = dblTotalAmount
            Else
                txtAmount.Text = ""
            End If      

        Else
            lblErrNoRelocation.Visible = True
            txtAmount.Text = ""
        End If
    End Sub
    Sub BindMaternityAmt(ByVal pv_strADId As String)
        Dim strOpCd As String = "PR_CLSSETUP_AD_GET"
        Dim arrADCode as Array
        Dim strParam As String      
        Dim intErrNo As Integer
        Dim objADDs as New DataSet

        arrADCode = Split(pv_strADId, "|")
        strParam = arrADCode(0)

            Try
               intErrNo = objPRSetup.mtdGetAD(strOpCd, _
                                           strParam, _
                                           objADDs, _
                                           True)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_AD_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objADDs.Tables(0).Rows.Count > 0 Then
                If objADDs.Tables(0).Rows(0).Item("MaternityInd") = "1" Then
                    GetMaternityAmount(ddlEmployee.SelectedItem.Value.ToString,ddlADCode.SelectedItem.Value.ToString)
                ElseIf txtAmount.Text = "" Then                 
                    txtAmount.Text = ""
                End If 

            End If 
     End Sub
    Sub BindRelocationAmt(ByVal pv_strADId As String)
        Dim strOpCd As String = "PR_CLSSETUP_AD_GET"
        Dim arrADCode as Array
        Dim strParam As String      
        Dim intErrNo As Integer
        Dim objADDs as New DataSet

        arrADCode = Split(pv_strADId, "|")
        strParam = arrADCode(0)

            Try
               intErrNo = objPRSetup.mtdGetAD(strOpCd, _
                                           strParam, _
                                           objADDs, _
                                           True)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_AD_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objADDs.Tables(0).Rows.Count > 0 Then
                If objADDs.Tables(0).Rows(0).Item("RelocationInd") = "1" Then
                    GetRelocationAmount(ddlADCode.SelectedItem.Value.ToString)
                ElseIf txtAmount.Text = "" Then                
                    txtAmount.Text = ""
                End If 

            End If 
     End Sub
    Sub BindIndicator(ByVal pv_strADId As String)
        Dim strOpCd As String = "PR_CLSSETUP_AD_GET"
        Dim arrADCode as Array
        Dim strParam As String      
        Dim intErrNo As Integer
        Dim objADDs as New DataSet

        arrADCode = Split(pv_strADId, "|")
        strParam = arrADCode(0)

            Try
               intErrNo = objPRSetup.mtdGetAD(strOpCd, _
                                           strParam, _
                                           objADDs, _
                                           True)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_AD_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objADDs.Tables(0).Rows.Count > 0 Then
                If Trim(objADDs.Tables(0).Rows(0).Item("RelocationInd")) = "1" Then
                   lblRelocationInd.Text = objADDs.Tables(0).Rows(0).Item("RelocationInd")
                ElseIf Trim(objADDs.Tables(0).Rows(0).Item("MaternityInd")) = "1" Then
                   lblMaternityInd.Text = objADDs.Tables(0).Rows(0).Item("MaternityInd")
                ElseIf Trim(objADDs.Tables(0).Rows(0).Item("TransportInd")) = "1" Then
                   lblTransportInd.Text = objADDs.Tables(0).Rows(0).Item("TransportInd")
                ElseIf Trim(objADDs.Tables(0).Rows(0).Item("MealInd")) = "1" Then
                   lblMealInd.Text = objADDs.Tables(0).Rows(0).Item("MealInd")                
                ElseIf Trim(objADDs.Tables(0).Rows(0).Item("HouseInd")) = "1" Then
                   lblHouseInd.Text = objADDs.Tables(0).Rows(0).Item("HouseInd")
                ElseIf Trim(objADDs.Tables(0).Rows(0).Item("MedicalInd")) = "1" Then
                   lblMedicalInd.Text = objADDs.Tables(0).Rows(0).Item("MedicalInd")                
                End If        
            End If 

     End Sub
    Sub ValidateTransportGiven()
        Dim objDs As New Dataset()
        Dim strOpCd As String = "PR_CLSTRX_ADTRX_TRANSPORT_GIVEN"
        Dim strADTrxId As String
        Dim arrADCode as Array
        Dim arrEmpCode as Array
        Dim strDesc As String
        Dim strEmp As String
        Dim strStatus As String
        Dim strLastUpdate As String     
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strSort as String
        Dim strSortCol as String

        arrADCode = Split(ddlADCode.SelectedItem.Value, "|")
        strADTrxId = arrADCode(0)
        strDesc = "" 
        arrEmpCode = Split(ddlEmployee.SelectedItem.Value, "|")      
        strEmp = arrEmpCode(0)
        strStatus = objPRTrx.EnumADTrxStatus.Active & "','" &  objPRTrx.EnumADTrxStatus.Closed                 


        strParam =   "|" & _
                    strEmp & "|" & _
                    strStatus & "|" & _
                    strLocation & "|" & _
                    strADTrxId 
        
        Try
            intErrNo = objPRTrx.mtdCheckEmployee(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strParam, _
                                            objDs, _
                                            True)
        
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            lblErrTransGiven.Visible = True
        End If
    End Sub
    Sub ValidateMealGiven()
        Dim objDs As New Dataset()
        Dim strOpCd As String = "PR_CLSTRX_ADTRX_MEAL_GIVEN"
        Dim strADTrxId As String
        Dim arrADCode as Array
        Dim arrEmpCode as Array
        Dim strDesc As String
        Dim strEmp As String
        Dim strStatus As String
        Dim strLastUpdate As String     
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strSort as String
        Dim strSortCol as String

        arrADCode = Split(ddlADCode.SelectedItem.Value, "|")
        strADTrxId = arrADCode(0)
        strDesc = "" 
        arrEmpCode = Split(ddlEmployee.SelectedItem.Value, "|")      
        strEmp = arrEmpCode(0)
        strStatus = objPRTrx.EnumADTrxStatus.Active & "','" &  objPRTrx.EnumADTrxStatus.Closed            


        strParam =  "|" & _
                    strEmp & "|" & _
                    strStatus & "|" & _
                    strLocation & "|" & _
                    strADTrxId 
        
        Try
            intErrNo = objPRTrx.mtdCheckEmployee(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strParam, _
                                            objDs, _
                                            True)
        
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            lblErrMealGiven.Visible = True
        End If
    End Sub
    Sub ValidateADCode(pv_Type as String)
        Dim objDs As New Dataset()
        Dim strOpCd As String = "PR_CLSTRX_ADTRX_CHECK_CONFIG"
        Dim strADType As String
        Dim arrADCode as Array
        Dim arrEmpCode as Array       
        Dim strEmp As String          
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strSort as String
        Dim strSortCol as String
        Dim strADTrxId As String

        arrADCode = Split(ddlADCode.SelectedItem.Value, "|")
        strADTrxId = arrADCode(0)
       
        strADType = Trim(pv_Type) & "ADCode"
        arrEmpCode = Split(ddlEmployee.SelectedItem.Value, "|")      
        strEmp = arrEmpCode(0)                      


        strParam =  strEmp & "|" & _
                    strADType & "|"

                   
        Try
           intErrNo = objPRTrx.mtdCheckADCode(strOpCd, _                                            
                                            strParam, _
                                            objDs)
        
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_ADTRX_CHECK_CONFIG&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then  
            If Trim(objDs.Tables(0).Rows(0).Item(0).ToString) <> Trim(strADTrxId) Then 
                lblErrCheckADCode.Text = "AD Code selected above is not compatible with Career Progress screen for selected employee.<br>"       
                lblErrCheckADCode.Visible = True
            End If
        End If
    End Sub

    Sub ValidateHouseRentGiven()
        Dim objHouseRentDs As New Dataset()
        Dim strOpCd As String = "PR_CLSTRX_ADTRX_HOUSERENT_GIVEN"
        Dim strADTrxId As String
        Dim arrADCode as Array
        Dim arrEmpCode as Array
        Dim strDesc As String
        Dim strEmp As String
        Dim strStatus As String
        Dim strLastUpdate As String     
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strSort as String
        Dim strSortCol as String

        arrADCode = Split(ddlADCode.SelectedItem.Value, "|")
        strADTrxId = arrADCode(0)
        strDesc = "" 
        arrEmpCode = Split(ddlEmployee.SelectedItem.Value, "|")      
        strEmp = arrEmpCode(0)
        strStatus = objPRTrx.EnumADTrxStatus.Active                 


        strParam =  Right(Trim(txtEffPeriod.Text), 4) & "|" & _
                    strEmp & "|" & _
                    strStatus & "|" & _
                    strLocation & "|" & _
                    strADTrxId 
        
        Try
            intErrNo = objPRTrx.mtdCheckEmployee(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strParam, _
                                            objHouseRentDs, _
                                            True)
        
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objHouseRentDs.Tables(0).Rows.Count > 0 Then
            lblErrHouseRent.Visible = True
        End If
    End Sub

    Sub CheckMedicalClosed(ByRef pr_EmpCode As String)
        Dim objEmpDs As New Dataset()
        Dim objDs As New Dataset()
        Dim strOpCd_GetEmp As String = "PR_CLSTRX_ADTRX_EMPLOYEE_MEDICAL_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_ADTRX_STATUS_UPD"
        Dim strOpCd_Stsln As String = "PR_CLSTRX_ADTRXLN_STATUS_UPD"
        Dim strParam As String
        Dim strADId as String 
        Dim intErrNo as integer
        Dim objADTrxId As String

        strParam = pr_EmpCode & "|2|" & pr_EmpCode & "|" & strPhyYear

        Try
            intErrNo = objPRTrx.mtdGetEmpMedicalAmt(strOpCd_GetEmp, strParam, objDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ADTRX_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        if objDs.Tables(0).Rows.Count > 0 Then
            strADId = objDS.Tables(0).Rows(0).Item("ADTrxID").Trim()
            strSelectedADId = objDS.Tables(0).Rows(0).Item("ADTrxID").Trim()
            strParam = strADId & "|" & objPRTrx.EnumADTrxStatus.Active
            Try
                intErrNo = objPRTrx.mtdUpdADTrx(strOpCd_Sts, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True, _
                                                objADTrxId)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_ADTRX_ACTIVE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_ADdet.aspx?adid=" & strSelectedADId)
            End Try

            strParam = strADId & "|" & objPRTrx.EnumADTrxStatus.Active
            Try
                intErrNo = objPRTrx.mtdUpdADTrx(strOpCd_Stsln, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True, _
                                                objADTrxId)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_ADTRXLN_ACTIVE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_ADdet.aspx?adid=" & strSelectedADId)
            End Try
            flgMedCloseInd = "1"
            lblFlgMedCheckEmp.Text = "1"
            onLoad_Display()
            onLoad_DisplayLine()            
        end if 
    End Sub
    Sub onTrxType_Display()

        If lblTrxTypeInd.Text <> "" Then
            If lblTrxTypeInd.Text = objPRTrx.EnumADTrxTypeStatus.Daily Then
                trDetailDaily.Attributes("class") ="mb-t"
                tblDetailDaily.Attributes("class") ="mb-c"
                trSpacer2.visible = false
                trSpacer1.visible = true
                trPeriod.visible = false
                lblEffDate.visible =  true
                txtEffDate.visible = true
                btnSelDate.visible = true
                dgLineDet.Columns(0).visible = true
                dgLineDet.Columns(1).visible = true
                dgLineDet.Columns(2).visible = true
                dgLineDet.Columns(3).visible = true
                dgLineDet.Columns(4).visible = true

                dgLineDet.Columns(5).visible = false
            Else
                trDetailDaily.Attributes("class") =""
                tblDetailDaily.Attributes("class") =""
                trSpacer2.visible = true
                trSpacer1.visible = false
                trPeriod.visible = true
                lblEffDate.visible =  false
                txtEffDate.visible = false
                btnSelDate.visible = false

                dgLineDet.Columns(0).visible = false
                dgLineDet.Columns(1).visible = false
                dgLineDet.Columns(2).visible = false
                dgLineDet.Columns(3).visible = false
                dgLineDet.Columns(4).visible = false

                dgLineDet.Columns(5).visible = true
            End If
        Else
            trDetailDaily.Attributes("class") =""
            tblDetailDaily.Attributes("class") =""
            trSpacer2.visible = true
            trSpacer1.visible = false
            trPeriod.visible = true
            lblEffDate.visible =  false
            txtEffDate.visible = false
            btnSelDate.visible = false

            dgLineDet.Columns(0).visible = false
            dgLineDet.Columns(1).visible = false
            dgLineDet.Columns(2).visible = false
            dgLineDet.Columns(3).visible = false

            dgLineDet.Columns(5).visible = true
        End If
    End Sub
    Sub onChg_EffDate(ByVal sender As Object, ByVal E As EventArgs)
        lblTrxTypeInd.Text = Trim(ddlTrxType.SelectedItem.Value)
        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtEffDate.Text, _
                                       objFormatDate, _
                                       objActualDate) = False Then
            lblErrEffDate.Text = lblErrEffDateDesc.Text & objFormatDate
            lblErrEffDate.Visible = True
            Exit Sub
        Else
            If Trim(ddlEmployee.SelectedItem.Value.ToString) <> "" Then
                onload_Display()
                onLoad_DisplayLine()
                onLoad_BindButton()
            End If
        End If

    End Sub
    Sub ValidateDailyADCode()
        Dim objDs As New Dataset()
        Dim strOpCd As String = "PR_CLSTRX_ADTRX_LINE_CHECK_DAILYBASIS"
        Dim arrADCode as Array
        Dim strADTrxID As String   = adid.Value     
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strADCode As String

        arrADCode = Split(ddlADCode.SelectedItem.Value, "|")
        strADCode = arrADCode(0)

        strParam =  strADTrxID & "|" & _
                    strADCode & "|"

                   
        Try
           intErrNo = objPRTrx.mtdValidateDailyADCode(strOpCd, _                                            
                                            strParam, _
                                            objDs)
        
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_ADTRX_CHECK_CONFIG&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then  
            If Trim(objDs.Tables(0).Rows(0).Item(0).ToString) <> Trim(strADTrxId) Then
                lblErrCheckADCode.Text = "Selected AD Code already set below.<br>"  
                lblErrCheckADCode.Visible = True
            End If
        End If
    End Sub

End Class
