Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction


Public Class PR_trx_TripDet : Inherits Page

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid

    Protected WithEvents txtDesc As Textbox
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents txtDate As Textbox
    Protected WithEvents ddlRoute As DropDownList

    Protected WithEvents ddlLoad As DropDownList

    Protected WithEvents ddlDenda As DropDownList
    Protected WithEvents lblErrDenda As Label
    Protected WithEvents txtDendaQty As Textbox
    Protected WithEvents lblErrDendaQty As Label

    Protected WithEvents txtTotal As Textbox
    Protected WithEvents lblTripId As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents btnFind1 As HtmlInputButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents tripid As HtmlInputHidden
    Protected WithEvents hidPayrollInd As HtmlInputHidden

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrEmp As Label
    Protected WithEvents lblErrRoute As Label
    Protected WithEvents lblErrDate As Label
    Protected WithEvents lblErrDateFmt As Label
    Protected WithEvents lblErrDateFmtMsg As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblErrLoad As Label
    Protected WithEvents lblErrTotalTrip As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblRoute As Label
    Protected WithEvents lblLoad As Label

    Protected WithEvents lblAccount As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehExpense As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblAccountErr As Label
    Protected WithEvents lblBlockErr As Label
    Protected WithEvents lblVehicleErr As Label
    Protected WithEvents lblVehExpenseErr As Label
    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents btnSelDate As Image
    Protected WithEvents ddlAccount As Dropdownlist
    Protected WithEvents ddlBlock As Dropdownlist
    Protected WithEvents ddlVehicle As Dropdownlist
    Protected WithEvents ddlVehExpense As Dropdownlist

    Dim objPRTrx As New agri.PR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLset As New agri.GL.clsSetup()

    Dim objTripDs As New Object()

    Dim objTripLnDs As New Object()

    Dim objDendaDs As New Object()
    Dim objEmpDs As New Object()
    Dim objLoadDs As New Object()
    Dim objRouteDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFormat As String
    Dim intPRAR As Long
    Dim intConfig As Integer

    Dim strSelectedTripId As String = ""
    Dim intStatus As Integer

    Dim AccountTag As String
    Dim BlockTag As String
    Dim VehicleTag As String
    Dim VehExpenseTag As String

    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strDateFormat = Session("SS_DATEFMT")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxTrip), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrEmp.Visible = False
            lblErrDate.Visible = False
            lblErrDateFmt.Visible = False
            lblErrDateFmtMsg.Visible = False
            lblErrRoute.Visible = False
            lblErrLoad.Visible = False  
            lblErrTotalTrip.Visible = False
            lblAccountErr.Visible = False
            lblBlockErr.Visible = False
            lblVehicleErr.Visible = False
            lblVehExpenseErr.Visible = False

            strSelectedTripId = Trim(IIf(Request.QueryString("tripid") <> "", Request.QueryString("tripid"), Request.Form("tripid")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedTripId <> "" Then
                    tripid.Value = strSelectedTripId
                    onLoad_Display()

                    onLoad_DisplayLine()
                Else
                    BindEmployee("")
                    BindRoute("")
                    
                    BindLoad("")
                    
                    BindDendaCode("")
                    BindAccCodeDropList()
                    BindBlockDropList("")
                    BindVehicleCodeDropList("")
                    BindVehicleExpDropList(True)
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfig) = True Then
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNAL_DETAILS_GET_COSTLEVEL_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
        End Try

        lblRoute.text = GetCaption(objLangCap.EnumLangCap.Route)
        lblErrRoute.text = lblErrSelect.text & lblRoute.text

        lblLoad.text = GetCaption(objLangCap.EnumLangCap.Load)
        lblErrLoad.text = lblErrSelect.text & lblLoad.text

        AccountTag = GetCaption(objLangCap.EnumLangCap.Account)
        VehicleTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        VehExpenseTag = GetCaption(objLangCap.EnumLangCap.VehExpense)

        lblAccount.Text = AccountTag & lblCode.Text
        lblBlock.Text = BlockTag & lblCode.Text
        lblVehicle.Text = VehicleTag & lblCode.Text
        lblVehExpense.Text = VehExpenseTag & lblCode.Text

        dgLineDet.Columns(6).HeaderText = lblAccount.Text & " (DR)"
        dgLineDet.Columns(7).HeaderText = lblBlock.Text
        dgLineDet.Columns(8).HeaderText = lblVehicle.Text
        dgLineDet.Columns(9).HeaderText = lblVehExpense.Text

        lblAccountErr.Text = lblErrSelect.Text & lblAccount.Text
        lblBlockErr.Text = lblErrSelect.Text & lblBlock.Text
        lblVehicleErr.Text = lblErrSelect.Text & lblVehicle.Text
        lblVehExpenseErr.Text = lblErrSelect.Text & lblVehExpense.Text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIPDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
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



    Sub CallCheckVehicleUse(ByVal sender As Object, ByVal e As System.EventArgs)
        CheckVehicleUse()
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer, _
                          ByRef pr_strNurseryInd As Integer, _
                          ByRef pr_blnFound As Boolean)

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = CInt(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = CInt(_objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
            pr_blnFound = True
        Else
            pr_blnFound = False
            pr_strAccType = 0
            pr_strAccPurpose = 0
            pr_strNurseryInd = 0
        End If
    End Sub

    Sub CheckVehicleUse()
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehicle")
        Dim strVehExp As String = Request.Form("ddlVehExpense")

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd, blnFound)

        If intAccType = objGLset.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLset.EnumAccountPurpose.NonVehicle
                    BindBlockDropList(strAcc, strBlk)
                    BindVehicleCodeDropList("")
                    BindVehicleExpDropList(True)
                Case objGLset.EnumAccountPurpose.VehicleDistribution
                    BindBlockDropList("")
                    BindVehicleCodeDropList(strAcc, strVeh)
                    BindVehicleExpDropList(False, strVehExp)
                Case Else
            End Select
        ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
            BindBlockDropList(strAcc, strBlk)
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        Else
            BindBlockDropList("")
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        End If
    End Sub

    Function CheckRequiredField() As Boolean
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehicle")
        Dim strVehExp As String = Request.Form("ddlVehExpense")

        If strAcc = "" Then
            lblAccountErr.Visible = True
            Return True
        End If

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd, blnFound)

        If blnFound = True Then
            If intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.No Then
                Return False
            ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.Yes And strBlk = "" Then
                lblBlockErr.Visible = True
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And intAccPurpose = objGLset.EnumAccountPurpose.NonVehicle And strBlk = "" Then
                lblBlockErr.Visible = True
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
               intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution And _
                 strVeh = "" And strVehExp = "" Then
                lblVehicleErr.Visible = True
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution And _
                strVeh = "" And strVehExp <> "" Then
                lblVehicleErr.Visible = True
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
               intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution And _
               strVeh <> "" And strVehExp = "" Then
                lblVehExpenseErr.Visible = True
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")

        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLset.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet
        
        Try
            intErrNo = objGLset.mtdGetMasterList(strOpCd, _
                                                 strParam, _
                                                 objGLset.EnumGLMasterType.AccountCode, _
                                                 dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIPDET_BINDACCOUNT&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = lblSelect.Text & AccountTag & lblCode.Text
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = dsForDropDown.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "Description"
        ddlAccount.DataBind()
        ddlAccount.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindBlockDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfig) = True Then
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLset.EnumBlockStatus.Active
            Else
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLset.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLset.mtdGetAccountBlock(strOpCdBlockList_Get, _
                                                   strParam, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIPDET_BINDBLOCK&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & BlockTag & lblCode.Text
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

    Sub BindVehicleCodeDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strVehCode As String = "")

        Dim dsForDropDown As DataSet
        Dim strOpCd As String
        Dim drinsert As DataRow
        Dim strParam As New StringBuilder()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        strParam.Append("|AccCode Like '" & pv_strAccCode & "' AND LocCode = '" & Session("SS_LOCATION") & "' AND Status = '" & objGLset.EnumVehicleStatus.Active & "'")

        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"
            intErrNo = objGLset.mtdGetMasterList(strOpCd, _
                                                 strParam.ToString, _
                                                 objGLset.EnumGLMasterType.Vehicle, _
                                                 dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIPDET_BINDVEHICLE&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                           Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("VehCode") = Trim(pv_strVehCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = lblSelect.Text & VehicleTag & lblCode.Text
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        ddlVehicle.DataSource = dsForDropDown.Tables(0)
        ddlVehicle.DataValueField = "VehCode"
        ddlVehicle.DataTextField = "Description"
        ddlVehicle.DataBind()
        ddlVehicle.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindVehicleExpDropList(ByVal pv_IsBlankList As Boolean, Optional ByVal pv_strVehExpCode As String = "")

        Dim dsForDropDown As DataSet
        Dim strOpCd As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim drinsert As DataRow
        Dim strParam As String = "Order By VehExpenseCode ASC|And Veh.Status = '" & objGLset.EnumVehicleExpenseStatus.active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Trace.Warn("pv_IsBlankList", pv_IsBlankList)
        Try
            If pv_IsBlankList Then
                strParam += " And Veh.VehExpensecode = ''"
            End If
            intErrNo = objGLset.mtdGetMasterList(strOpCd, _
                                                 strParam, _
                                                 objGLset.EnumGLMasterType.VehicleExpense, _
                                                 dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIPDET_BINDVEHEXPENSE&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(pv_strVehExpCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = lblSelect.text & VehExpenseTag & lblCode.text
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        ddlVehExpense.DataSource = dsForDropDown.Tables(0)
        ddlVehExpense.DataValueField = "VehExpenseCode"
        ddlVehExpense.DataTextField = "Description"
        ddlVehExpense.DataBind()
        ddlVehExpense.SelectedIndex = intSelectedIndex


        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub




    Sub onLoad_BindButton()
        txtDesc.Enabled = False
        ddlEmployee.Enabled = False
        tblSelection.Visible = False
        SaveBtn.Visible = False
        CancelBtn.Visible = False
        btnFind1.Visible = False
        Select Case intStatus
            Case objPRTrx.EnumTripStatus.Active
                txtDesc.Enabled = True
                If hidPayrollInd.Value = objPRTrx.EnumPayrollPosted.No Then
                    ddlEmployee.Enabled = True
                    tblSelection.Visible = True
                    CancelBtn.Visible = True
                    btnFind1.Visible = True
                End If
                SaveBtn.Visible = True
                ddlEmployee.Enabled = False
                txtDesc.Enabled = False
            Case objPRTrx.EnumTripStatus.Closed, objPRTrx.EnumTripStatus.Cancelled
                ddlEmployee.Enabled = False
                txtDesc.Enabled = False
            Case Else
                txtDesc.Enabled = True
                ddlEmployee.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
                btnFind1.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSTRX_TRIP_GET"
        Dim strParam As String = strSelectedTripId
        Dim intErrNo As Integer

        Try
            intErrNo = objPRTrx.mtdGetTrip(strOpCd, _
                                           strCompany, _
                                           strLocation, _
                                           strUserId, _
                                           strAccMonth, _
                                           strAccYear, _
                                           strParam, _
                                           objTripDs, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        lblTripId.Text = strSelectedTripId
        txtDesc.Text = Trim(objTripDs.Tables(0).Rows(0).Item("Description"))


        intStatus = CInt(Trim(objTripDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objTripDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objPRTrx.mtdGetTripStatus(Trim(objTripDs.Tables(0).Rows(0).Item("Status")))
        lblPeriod.Text = Trim(objTripDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objTripDs.Tables(0).Rows(0).Item("AccYear"))
        lblDateCreated.Text = objGlobal.GetLongDate(objTripDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objTripDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objTripDs.Tables(0).Rows(0).Item("UserName"))

        BindEmployee(Trim(objTripDs.Tables(0).Rows(0).Item("EmpCode")))

        BindRoute(Request.Form("ddlRoute"))
        BindLoad(Request.Form("ddlLoad"))
        BindDendaCode(Request.Form("ddlDenda"))
        BindAccCodeDropList(Request.Form("ddlAccount"))

        If (Request.Form("ddlBlock") = "") Then
            BindBlockDropList("")
        Else
            BindBlockDropList(Request.Form("ddlAccount"), Request.Form("ddlBlock"))
        End If

        If (Request.Form("ddlVehicle") = "") Then
            BindVehicleCodeDropList("")
        Else
            BindVehicleCodeDropList(Request.Form("ddlAccount"), Request.Form("ddlVehicle"))
        End If

        If (Request.Form("ddlVehExpense") = "") Then
            BindVehicleExpDropList(True)
        Else
            BindVehicleExpDropList(False, Request.Form("ddlVehExpense"))
        End If


        onLoad_BindButton()
    End Sub


    Sub onLoad_DisplayLine()
        Dim strOpCd As String = "PR_CLSTRX_TRIP_LINE_GET"
	    Dim strParam As String = strSelectedTripId	
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label

        Try
            intErrNo = objPRTrx.mtdGetTrip(strOpCd, _
                                           strCompany, _
                                           strLocation, _
                                           strUserId, _
                                           strAccMonth, _
                                           strAccYear, _
                                           strParam, _
                                           objTripLnDs, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIP_LINE_GET&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_trx_tripdet.aspx")
        End Try

        dgLineDet.DataSource = objTripLnDs.Tables(0)
        dgLineDet.DataBind()

        If intStatus = objPRTrx.EnumTripStatus.Active Then
            If hidPayrollInd.Value = objPRTrx.EnumPayrollPosted.No Then
                For intCnt = 0 To dgLineDet.Items.Count - 1
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Next
            Else
                For intCnt = 0 To dgLineDet.Items.Count - 1
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                Next
            End If
        Else
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = False
            Next
        End If

    End Sub


    Sub BindEmployee(ByVal pv_strEmpId As String)
        Dim strOpCd As String = "PR_TRX_TRIPDET_GET_EMPLIST"
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        If trim(pv_strEmpId) = "" Then
            strParam = "order by emp.EmpCode " & "|" & _
                       "and emp.Status = '" & objHRTrx.EnumEmpStatus.Active & "' " & _
                       "and emp.LocCode = '" & strLocation & "' " & _
                       "and emp.PayrollInd = '" & objPRTrx.EnumPayrollPosted.No & "' "
        Else
            strParam = "order by emp.EmpCode " & "|" & _
                       "and emp.Status = '" & objHRTrx.EnumEmpStatus.Active & "' " & _
                       "and emp.LocCode = '" & strLocation & "' "
        End If

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   0, _
                                                   objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIP_EMP_GET&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            If Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode")) = Trim(pv_strEmpId) Then
                intSelectedIndex = intCnt + 1
                hidPayrollInd.Value = objEmpDs.Tables(0).Rows(intCnt).Item("PayrollInd")
                Exit For
            End If
        Next

        dr = objEmpDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_EmpName") = "Select one Employee Code"
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmployee.DataSource = objEmpDs.Tables(0)
        ddlEmployee.DataValueField = "EmpCode"
        ddlEmployee.DataTextField = "_EmpName"
        ddlEmployee.DataBind()
        ddlEmployee.SelectedIndex = intSelectedIndex
    End Sub


    Sub BindLoad(ByVal pv_strLoadCode As String)
        Dim strOpCd As String = "PR_CLSSETUP_LOAD_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By L.LoadCode ASC|And L.Status = '" & objPRSetup.EnumLoadStatus.Active & "' And L.LocCode = '" & strLocation & "' "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objPRSetup.EnumPayrollMasterType.Load, _
                                                   objLoadDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIP_LOAD_GET&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
        End Try

        For intCnt = 0 To objLoadDs.Tables(0).Rows.Count - 1
            objLoadDs.Tables(0).Rows(intCnt).Item("LoadCode") = Trim(objLoadDs.Tables(0).Rows(intCnt).Item("LoadCode"))
            objLoadDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objLoadDs.Tables(0).Rows(intCnt).Item("LoadCode")) & " (" & Trim(objLoadDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objLoadDs.Tables(0).Rows(intCnt).Item("LoadCode") = Trim(pv_strLoadCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objLoadDs.Tables(0).NewRow()
        dr("LoadCode") = ""

        dr("Description") = lblSelect.Text & lblLoad.Text
        objLoadDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLoad.DataSource = objLoadDs.Tables(0)
        ddlLoad.DataValueField = "LoadCode"
        ddlLoad.DataTextField = "Description"
        ddlLoad.DataBind()
        ddlLoad.SelectedIndex = intSelectedIndex
    End Sub


    Sub BindRoute(ByVal pv_strRouteCode As String)
        Dim strOpCd As String = "PR_CLSSETUP_ROUTE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By R.RouteCode ASC|And R.Status = '" & objPRSetup.EnumRouteStatus.Active & "' And R.LocCode = '" & strLocation & "' "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objPRSetup.EnumPayrollMasterType.Route, _
                                                   objRouteDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIP_ROUTE_GET&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
        End Try

        For intCnt = 0 To objRouteDs.Tables(0).Rows.Count - 1
            objRouteDs.Tables(0).Rows(intCnt).Item("RouteCode") = Trim(objRouteDs.Tables(0).Rows(intCnt).Item("RouteCode"))
            objRouteDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objRouteDs.Tables(0).Rows(intCnt).Item("RouteCode")) & " (" & Trim(objRouteDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objRouteDs.Tables(0).Rows(intCnt).Item("RouteCode") = Trim(pv_strRouteCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objRouteDs.Tables(0).NewRow()
        dr("RouteCode") = ""
        dr("Description") = lblSelect.Text & lblRoute.Text
        objRouteDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlRoute.DataSource = objRouteDs.Tables(0)
        ddlRoute.DataValueField = "RouteCode"
        ddlRoute.DataTextField = "Description"
        ddlRoute.DataBind()
        ddlRoute.SelectedIndex = intSelectedIndex
    End Sub


    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSTRX_TRIP_LINE_ADD"
        Dim strOpCd As String
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehicle")
        Dim strVehExp As String = Request.Form("ddlVehExpense")

        strAcc = IIf(strAcc = "", ddlAccount.SelectedItem.Value, strAcc)
        strBlk = IIf(strBlk = "", ddlBlock.SelectedItem.Value, strBlk)
        strVeh = IIf(strVeh = "", ddlVehicle.SelectedItem.Value, strVeh)
        strVehExp = IIf(strVehExp = "", ddlVehExpense.SelectedItem.Value, strVehExp)

        If ddlEmployee.SelectedItem.Value = "" Then
            lblErrEmp.Visible = True
            Exit Sub
        ElseIf Trim(txtDate.Text) = "" Then
            lblErrDate.Visible = True
            Exit Sub
        ElseIf objGlobal.mtdValidInputDate(strDateFormat, _
                                           Trim(txtDate.Text), _
                                           objFormatDate, _
                                           objActualDate) = False Then
    	    lblErrDateFmt.Visible = True
            lblErrDateFmt.Text = lblErrDateFmtMsg.Text & objFormatDate
            Exit Sub
	    ElseIf ddlRoute.SelectedItem.Value = "" Then
       		lblErrRoute.Visible = True
        	Exit Sub
        ElseIf ddlLoad.SelectedItem.Value = "" Then
            lblErrLoad.Visible = True
            Exit Sub
        ElseIf Trim(txtTotal.Text) = "" Then
            lblErrTotalTrip.Visible = True
            Exit Sub
	    End If

        If Trim(ddlDenda.SelectedItem.Value) <> "" Then
            If Trim(txtDendaQty.Text) = "0" Then
                lblErrDendaQty.Visible = True
                Exit Sub 
            End If  
        End If

        If CheckRequiredField() = False Then
            InsertRecord("btnNew_onlick")
            strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRTripLn) & "||" & _
                           strSelectedTripId & "|" & _
                           objActualDate & "|" & _
                           ddlRoute.SelectedItem.Value & "|" & _
                           ddlLoad.SelectedItem.Value & "|" & _ 
                           Trim(txtTotal.Text) & "|" & _
                           strAcc & "|" & _
                           strBlk & "|" & _
                           strVeh & "|" & _
                           strVehExp & "|" & _
                           ddlDenda.SelectedItem.Value & "|" & _
                           Trim(txtDendaQty.Text)  

            strOpCd = strOpCd_Add

            Try
                intErrNo = objPRTrx.mtdUpdTripLine(strOpCd, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIP_LINE_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripDet.aspx")
            End Try
        End If

        If (strSelectedTripId <> "" And CheckRequiredField() = False) Then 
            onLoad_Display()
	        onLoad_DisplayLine()	
        End If
    End Sub
    
    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        InsertRecord (strCmdArgs)
        onLoad_DisplayLine()
    End Sub


    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_TripList.aspx")
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)        
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "PR_CLSTRX_TRIP_LINE_DEL"
        Dim strOpCod As String = strOpCode_DelLine
        Dim strParam As String
        Dim lblText As Label
        Dim strTripLnId As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = Convert.ToInt16(E.Item.ItemIndex)
        lblText = dgLineDet.Items.Item(Convert.ToInt16(E.Item.ItemIndex)).FindControl("lblTripLnId")
        strTripLnId = lblText.Text

        Try

            strParam = "|" & strTripLnId & "|||||||||||"

            intErrNo = objPRTrx.mtdUpdTripLine(strOpCod, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               strAccMonth, _
                                               strAccYear, _
                                               strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIP_LINE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/pr_clstrx_TripDet.aspx?tripid=" & strSelectedTripId)
        End Try

        onLoad_Display()
        onLoad_DisplayLine()
    End Sub

    Sub InsertRecord(ByVal EventType As String)
        Dim strOpCd_Add As String = "PR_CLSTRX_TRIP_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_TRIP_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_TRIP_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_TRIP_STATUS_UPD"
        Dim strOpCd As String
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim objTripId As String

        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim blnUpdateSts As Boolean
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strVeh As String = Request.Form("ddlVehicle")
        Dim strVehExp As String = Request.Form("ddlVehExpense")

        strEmp = IIf(strEmp = "", ddlEmployee.SelectedItem.Value, strEmp)
        strAcc = IIf(strAcc = "", ddlAccount.SelectedItem.Value, strAcc)
        strBlk = IIf(strBlk = "", ddlBlock.SelectedItem.Value, strBlk)
        strVeh = IIf(strVeh = "", ddlVehicle.SelectedItem.Value, strVeh)
        strVehExp = IIf(strVehExp = "", ddlVehExpense.SelectedItem.Value, strVehExp)

        If EventType = "Save" Or EventType = "btnNew_onlick" Then
            If strEmp = "" Then
                lblErrEmp.Visible = True
                Exit Sub
            End If
            strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRTrip) & "|" & _
                    strSelectedTripId & "|" & _
                    Replace(txtDesc.Text, "'", "''") & "|" & _
                    strEmp & "|" & _
                    objPRTrx.EnumTripStatus.Active

            strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
            blnUpdateSts = IIf(intStatus = 0, False, True)

            Try
                intErrNo = objPRTrx.mtdUpdTrip(strOpCd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                blnUpdateSts, _
                                                objTripId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIP_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
            End Try

            If blnUpdateSts = False Then
                strSelectedTripId = objTripId
            Else
                strSelectedTripId = lblTripId.Text.Trim
            End If
            tripid.Value = strSelectedTripId
        ElseIf EventType = "Cancel" Then
            strParam = strSelectedTripId & "|" & objPRTrx.EnumTripStatus.Cancelled
            Try
                intErrNo = objPRTrx.mtdUpdTripStatus(strOpCd_Sts, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            True, _
                                            objTripId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIP_CANCEL&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripList.aspx")
            End Try
        End If

        If strSelectedTripId <> "" Then
            If EventType = "Save" Or EventType = "Cancel" Then 
                onLoad_Display()
            End If
        End If

    End Sub
   
    Sub BindDendaCode(ByVal pv_strDendaCode As String)
        Dim strOpCd As String = "PR_CLSSETUP_DENDA_SEARCH"
        Dim dr As DataRow
        Dim strParam As String = "Order By D.DendaCode ASC|And D.Status = '" & objPRSetup.EnumDendaStatus.Active & "' And D.LocCode = '" & strLocation & "' "
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
        dr("Description") = "Select one Denda"
        objDendaDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDenda.DataSource = objDendaDs.Tables(0)
        ddlDenda.DataValueField = "DendaCode"
        ddlDenda.DataTextField = "Description"
        ddlDenda.DataBind()
        ddlDenda.SelectedIndex = intSelectedIndex
    End Sub



End Class
