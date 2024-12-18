Imports System
Imports System.Math
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings


Public Class WS_TRX_JOB_DETAIL : Inherits Page

    Protected WithEvents dgWorkCode As DataGrid

    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblPayrollPosted As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblJobID As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblJobTypeText As Label
    Protected WithEvents lblJobType As Label
    Protected WithEvents lblStatusText As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblJobStockCount As Label
    Protected WithEvents lblMechHourCount As Label
    Protected WithEvents lblEditDependency As Label
    Protected WithEvents lblDescriptionErr As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblDocRefNoErr As Label
    Protected WithEvents lblUpdateDate As Label
    Protected WithEvents lblDocRefDateErr As Label
    Protected WithEvents lblUserName As Label
    Protected WithEvents lblJobStartDateErr As Label
    Protected WithEvents lblJobEndDateErr As Label
    Protected WithEvents lblChrgRateErr As Label
    Protected WithEvents lblServTypeCodeTag As Label
    Protected WithEvents lblServTypeCodeErr As Label
    Protected WithEvents lblChargeToErr As Label
    Protected WithEvents lblLocationTag As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents lblBlkCodeTag As Label
    Protected WithEvents lblBlkCodeErr As Label
    Protected WithEvents lblVehCodeTag As Label
    Protected WithEvents lblVehCodeErr As Label
    Protected WithEvents lblVehExpCodeTag As Label
    Protected WithEvents lblVehExpCodeErr As Label
    Protected WithEvents lblEmpCodeErr As Label
    Protected WithEvents lblBillPartyCode As Label
    Protected WithEvents lblBillPartyCodeErr As Label
    Protected WithEvents lblVehRegNoTag As Label
    Protected WithEvents lblVehRegNoErr As Label
    Protected WithEvents lblWorkCodeTag As Label
    Protected WithEvents lblWorkCodeErr As Label
    Protected WithEvents lblActionResult As Label

    Protected WithEvents ddlJobType As DropDownList
    Protected WithEvents ddlJobStartTimeDayNight As DropDownList
    Protected WithEvents ddlJobEndTimeDayNight As DropDownList
    Protected WithEvents ddlServTypeCode As DropDownList
    Protected WithEvents ddlChargeTo As DropDownList
    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents ddlBlkCode As DropDownList
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents ddlVehExpCode As DropDownList
    Protected WithEvents ddlEmpCode As DropDownList
    Protected WithEvents ddlBillPartyCode As DropDownList
    Protected WithEvents ddlWorkCode As DropDownList

    Protected WithEvents txtDescription As TextBox
    Protected WithEvents txtDocRefNo As TextBox
    Protected WithEvents txtDocRefDate As TextBox
    Protected WithEvents txtJobStartDate As TextBox
    Protected WithEvents txtJobStartTimeHour As TextBox
    Protected WithEvents txtJobStartTimeMinute As TextBox
    Protected WithEvents txtJobEndDate As TextBox
    Protected WithEvents txtJobEndTimeHour As TextBox
    Protected WithEvents txtJobEndTimeMinute As TextBox
    Protected WithEvents txtChrgRate As TextBox
    Protected WithEvents txtVehRegNo As TextBox
    Protected WithEvents txtRemark As TextBox

    Protected lblSubBlkCode As Label

    Protected WithEvents ibJobStartSetCurrentDateTime As ImageButton
    Protected WithEvents ibJobEndSetCurrentDateTime As ImageButton
    Protected WithEvents ibAdd As ImageButton
    Protected WithEvents ibSave As ImageButton
    Protected WithEvents ibDelete As ImageButton
    Protected WithEvents ibPartsIssueReturn As ImageButton
    Protected WithEvents ibProceedToJobClose As ImageButton
    Protected WithEvents ibGenerateDNCN As ImageButton
    Protected WithEvents ibNew As ImageButton
    Protected WithEvents ibBack As ImageButton

    Protected WithEvents imgDocRefDate As Image
    Protected WithEvents imgJobStartDate As Image
    Protected WithEvents imgJobEndDate As Image
    Protected WithEvents revChrgRate As RegularExpressionValidator

    Protected WithEvents tblMain As HtmlTable
    Protected WithEvents tblAdd As HtmlTable

    Protected WithEvents trChargeTo As HtmlTableRow
    Protected WithEvents trAccCode As HtmlTableRow
    Protected WithEvents trBlkCode As HtmlTableRow
    Protected WithEvents trVehCode As HtmlTableRow
    Protected WithEvents trVehExpCode As HtmlTableRow
    Protected WithEvents trEmpCode As HtmlTableRow
    Protected WithEvents trBillPartyCode As HtmlTableRow
    Protected WithEvents trVehRegNo As HtmlTableRow
    Protected WithEvents trWorkCode As HtmlTableRow

    Protected WithEvents btnFindAccCode As HtmlInputButton

    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden

    Protected objWSTrx As New agri.WS.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objWSSetup As New agri.WS.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim strOpCode_Job_Add As String = "WS_CLSTRX_JOB_ADD"
    Dim strOpCode_Job_Get As String = "WS_CLSTRX_JOB_GET"
    Dim strOpCode_Job_Upd As String = "WS_CLSTRX_JOB_UPD"
    Dim strOpCode_Job_Get_For_Upd As String = "WS_CLSTRX_JOB_GET_FOR_UPDATE"
    Dim strOpCode_JobWorkCode_Add As String = "WS_CLSTRX_JOBWORKCODE_ADD"
    Dim strOpCode_JobWorkCode_Del As String = "WS_CLSTRX_JOBWORKCODE_DEL"
    Dim strOpCode_JobWorkCode_Get As String = "WS_CLSTRX_JOBWORKCODE_GET"

    Dim strOpCode_Mechanic_Hour_Document_Get As String = "WS_CLSTRX_MECHANIC_HOUR_DOCUMENT_GET"
    Dim strOpCode_Job_Transaction_Get As String = "WS_CLSTRX_JOB_GENERATE_DNCN_GET"
    Dim strOpCode_Job_Stock_Upd As String = "WS_CLSTRX_JOB_STOCK_UPD"
    Dim strOpCode_Mechanic_Hour_Upd As String = "WS_CLSTRX_MECHANIC_HOUR_UPD"
    Dim strOpCode_Mechanic_Hour_Line_Upd As String = "WS_CLSTRX_MECHANIC_HOUR_LINE_UPD"

    Dim dsLangCap As New DataSet()

    Const APPEND_SERV_TYPE_CODE As Boolean = True
    Const APPEND_ACC_CODE As Boolean = True
    Const APPEND_BLK_CODE As Boolean = True
    Const APPEND_VEH_CODE As Boolean = True
    Const APPEND_VEH_EXP_CODE As Boolean = True
    Const APPEND_EMP_CODE As Boolean = True
    Const APPEND_BILL_PARTY_CODE As Boolean = True
    Const APPEND_WORK_CODE As Boolean = False

    Dim intErrNo As Integer
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim intConfigsetting As Integer
    Dim intCnt As Integer
    Dim strAction As String
    Dim strLocType As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblDescriptionErr.Visible = False
            lblDocRefNoErr.Visible = False
            lblDocRefDateErr.Visible = False
            lblJobStartDateErr.Visible = False
            lblJobEndDateErr.Visible = False
            lblChrgRateErr.Visible = False
            lblServTypeCodeErr.Visible = False
            lblChargeToErr.Visible = False
            lblAccCodeErr.Visible = False
            lblBlkCodeErr.Visible = False
            lblVehCodeErr.Visible = False
            lblVehExpCodeErr.Visible = False
            lblEmpCodeErr.Visible = False
            lblBillPartyCodeErr.Visible = False
            lblVehRegNoErr.Visible = False
            lblWorkCodeErr.Visible = False
            lblActionResult.Visible = False

            If Not Page.IsPostBack Then
                GetLangCap()
                BindJobTypeDropDownList()
                lblJobID.Text = Trim(Request.QueryString("jobid"))
                If lblJobID.Text = "" Then
                    lblJobType.Text = Trim(Request.QueryString("jobtype"))
                    ResetPage(False, False, True)
                Else
                    strAction = Trim(Request.QueryString("flag"))
                    Select Case LCase(strAction)
                        Case "act1"
                            Call mtdGenerateDNCN(True, False)
                        Case "act2"
                            Call mtdGenerateDNCN(False, True)
                        Case "act3"
                            Call mtdGenerateDNCN(True, True)
                        Case Else
                            ResetPage(True, True, False)
                    End Select
                End If
            End If
        End If
    End Sub

    Sub GetLangCap()
        dsLangCap = GetLanguageCaptionDS()

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        Else
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        End If

        lblLocationTag.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblServTypeCodeTag.Text = GetCaption(objLangCap.EnumLangCap.ServType) & lblCode.Text
        lblAccCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Expense) & lblCode.Text
        lblBillPartyCode.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & lblCode.Text
        lblVehRegNoTag.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Reg No"
        lblWorkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Work) & lblCode.Text

        lblServTypeCodeErr.Text = lblPleaseSelect.Text & lblServTypeCodeTag.Text
        lblChargeToErr.Text = lblPleaseSelect.Text & "Charge To " & lblLocationTag.Text
        lblAccCodeErr.Text = lblPleaseSelect.Text & lblAccCodeTag.Text
        lblBlkCodeErr.Text = lblPleaseSelect.Text & lblBlkCodeTag.Text
        lblVehCodeErr.Text = lblPleaseSelect.Text & lblVehCodeTag.Text
        lblVehExpCodeErr.Text = lblPleaseSelect.Text & lblVehExpCodeTag.Text
        lblBillPartyCodeErr.Text = lblPleaseSelect.Text & lblBillPartyCode.Text
        lblVehRegNoErr.Text = lblVehRegNoTag.Text & " cannot be blank"
        lblWorkCodeErr.Text = lblPleaseSelect.Text & lblWorkCodeTag.Text

        dgWorkCode.Columns(0).HeaderText = lblWorkCodeTag.Text
        dgWorkCode.Columns(1).HeaderText = GetCaption(objLangCap.EnumLangCap.WorkDesc)
    End Sub



    Function GetCaption(ByVal pv_TermCode As String) As String
        Dim I As Integer

        For I = 0 To dsLangCap.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(dsLangCap.Tables(0).Rows(I).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill Then
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTermMW"))
                Else
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function


    Sub SetObjectAccessibilityByStatus()
        ddlJobType.Enabled = False
        ddlJobStartTimeDayNight.Enabled = False
        ddlJobEndTimeDayNight.Enabled = False
        ddlServTypeCode.Enabled = False
        ddlChargeTo.Enabled = False
        ddlAccCode.Enabled = False
        ddlBlkCode.Enabled = False
        ddlVehCode.Enabled = False
        ddlVehExpCode.Enabled = False
        ddlEmpCode.Enabled = False
        ddlBillPartyCode.Enabled = False
        ddlWorkCode.Enabled = False

        txtDescription.Enabled = False
        txtDocRefNo.Enabled = False
        txtDocRefDate.Enabled = False
        txtJobStartDate.Enabled = False
        txtJobStartTimeHour.Enabled = False
        txtJobStartTimeMinute.Enabled = False
        txtJobEndDate.Enabled = False
        txtJobEndTimeHour.Enabled = False
        txtJobEndTimeMinute.Enabled = False
        txtChrgRate.Enabled = False
        txtVehRegNo.Enabled = False
        txtRemark.Enabled = False

        ibJobStartSetCurrentDateTime.Visible = False
        ibJobEndSetCurrentDateTime.Visible = False
        ibAdd.Visible = False
        ibSave.Visible = False
        ibDelete.Visible = False
        ibPartsIssueReturn.Visible = False
        ibProceedToJobClose.Visible = False
        ibGenerateDNCN.Visible = False
        ibNew.Visible = False

        imgDocRefDate.Visible = False
        imgJobStartDate.Visible = False
        imgJobEndDate.Visible = False


        revChrgRate.Enabled = False
        tblAdd.Visible = False
        btnFindAccCode.Visible = False

        Select Case Trim(lblStatus.Text)
            Case Trim(CStr(objWSTrx.EnumJobStatus.Deleted))
                ibNew.Visible = True
                ibDelete.Visible = True
                ibDelete.ImageUrl = "../../images/butt_undelete.gif"
                ibDelete.AlternateText = "Undelete"
                ibDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objWSTrx.EnumJobStatus.Closed))
                ibNew.Visible = True
                If Trim(lblJobType.Text) = Trim(CStr(objWSTrx.EnumJobType.StaffDebitNote)) Or Trim(lblJobType.Text) = Trim(CStr(objWSTrx.EnumJobType.ExternalParty)) Then
                    ibGenerateDNCN.Attributes("onclick") = "javascript:return fnGenerateDNCN();"
                    ibGenerateDNCN.Visible = True
                End If
            Case Else
                If Trim(lblStatus.Text) = Trim(CStr(objWSTrx.EnumJobStatus.Active)) Or Trim(lblJobID.Text) = "" Then
                    ddlJobStartTimeDayNight.Enabled = True
                    ddlJobEndTimeDayNight.Enabled = True
                    ddlWorkCode.Enabled = True

                    txtDescription.Enabled = True
                    txtDocRefNo.Enabled = True
                    txtDocRefDate.Enabled = True
                    txtJobStartDate.Enabled = True
                    txtJobStartTimeHour.Enabled = True
                    txtJobStartTimeMinute.Enabled = True
                    txtJobEndDate.Enabled = True
                    txtJobEndTimeHour.Enabled = True
                    txtJobEndTimeMinute.Enabled = True
                    txtRemark.Enabled = True

                    ibJobStartSetCurrentDateTime.Visible = True
                    ibJobEndSetCurrentDateTime.Visible = True
                    ibAdd.Visible = True
                    ibSave.Visible = True

                    imgDocRefDate.Visible = True
                    imgJobStartDate.Visible = True
                    imgJobEndDate.Visible = True

                    revChrgRate.Enabled = True

                    tblAdd.Visible = True
                End If

                If dgWorkCode.Items.Count = 0 Then
                    ddlServTypeCode.Enabled = True
                End If

                If (Trim(lblStatus.Text) = Trim(CStr(objWSTrx.EnumJobStatus.Active)) And CInt(Trim(lblEditDependency.Text)) = 0) Or Trim(lblJobID.Text) = "" Then
                    ddlChargeTo.Enabled = False
                    ddlAccCode.Enabled = True
                    ddlBlkCode.Enabled = True
                    ddlVehCode.Enabled = True
                    ddlVehExpCode.Enabled = True
                    ddlEmpCode.Enabled = True
                    ddlBillPartyCode.Enabled = True
                    txtVehRegNo.Enabled = True
                    txtChrgRate.Enabled = True
                    btnFindAccCode.Visible = True
                End If

                If Trim(lblStatus.Text) = Trim(CStr(objWSTrx.EnumJobStatus.Active)) Then
                    ibNew.Visible = True
                    If Trim(txtJobEndDate.Text) <> "" And Trim(txtJobEndTimeHour.Text) <> "" And Trim(txtJobEndTimeMinute.Text) <> "" Then
                        ibProceedToJobClose.Visible = True
                    End If
                    If Trim(lblJobType.Text) = Trim(CStr(objWSTrx.EnumJobType.StaffDebitNote)) Or Trim(lblJobType.Text) = Trim(CStr(objWSTrx.EnumJobType.ExternalParty)) Then
                        ibGenerateDNCN.Attributes("onclick") = "javascript:return fnGenerateDNCN();"
                        ibGenerateDNCN.Visible = True
                    End If
                    If Not (CInt(Trim(lblJobStockCount.Text)) <> 0 Or CInt(Trim(lblMechHourCount.Text)) <> 0) Then
                        ibDelete.Visible = True
                        ibDelete.ImageUrl = "../../images/butt_delete.gif"
                        ibDelete.AlternateText = "Delete"
                        ibDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    End If
                    ibPartsIssueReturn.Visible = True
                Else
                    ddlJobType.Enabled = True
                End If
        End Select
    End Sub

    Sub SetObjectAccessibilityByJobType()
        Select Case Trim(lblJobType.Text)
            Case Trim(CStr(objWSTrx.EnumJobType.InternalUse))
                lblJobTypeText.Visible = True
                ddlJobType.Visible = False
                trEmpCode.Visible = False
                trBillPartyCode.Visible = False
                trVehRegNo.Visible = False
                trChargeTo.Visible = Session("SS_INTER_ESTATE_CHARGING")
            Case Trim(CStr(objWSTrx.EnumJobType.StaffPayroll)), Trim(CStr(objWSTrx.EnumJobType.StaffDebitNote))
                lblJobTypeText.Visible = False
                ddlJobType.Visible = True
                trEmpCode.Visible = True
                trBillPartyCode.Visible = False
                trVehRegNo.Visible = True
                trChargeTo.Visible = False
            Case Trim(CStr(objWSTrx.EnumJobType.ExternalParty))
                lblJobTypeText.Visible = True
                ddlJobType.Visible = False
                trEmpCode.Visible = False
                trBillPartyCode.Visible = True
                trVehRegNo.Visible = True
                trChargeTo.Visible = False
        End Select

        If Trim(lblJobType.Text) = Trim(CStr(objWSTrx.EnumJobType.InternalUse)) Then
            trAccCode.Visible = True
            trBlkCode.Visible = True
            trVehCode.Visible = True
            trVehExpCode.Visible = True
        Else
            trAccCode.Visible = False
            trBlkCode.Visible = False
            trVehCode.Visible = False
            trVehExpCode.Visible = False
        End If
        lblJobTypeText.Text = objWSTrx.mtdGetJobType(lblJobType.Text)
    End Sub

    Sub ResetPage(ByVal blnHeader As Boolean, ByVal blnLines As Boolean, ByVal blnAdd As Boolean)
        lblPeriod.Text = ""

        If blnHeader = True Then
            DisplayJobHeader()
        End If

        If blnLines = True Then
            DisplayJobLines()
        End If

        If blnAdd = True Then
            BindServTypeCodeDropDownList("")
            BindChargeToDropDownList(Session("SS_LOCATION"))
            BindAccCodeDropDownList("")
            Call BindAccountComponents(Session("SS_LOCATION"), "", "", "", "")
            BindEmpCodeDropDownList(GetDropDownListValue(ddlChargeTo), "")
            BindBillPartyCodeDropDownList("")
            BindWorkCodeDropDownList(GetDropDownListValue(ddlServTypeCode), "")

            ddlJobStartTimeDayNight.SelectedIndex = 0
            ddlJobEndTimeDayNight.SelectedIndex = 0
            txtDescription.Text = ""
            txtDocRefNo.Text = ""
            txtDocRefDate.Text = ""
            txtJobStartDate.Text = ""
            txtJobStartTimeHour.Text = ""
            txtJobStartTimeMinute.Text = ""
            txtJobEndDate.Text = ""
            txtJobEndTimeHour.Text = ""
            txtJobEndTimeMinute.Text = ""
            txtChrgRate.Text = ""
            txtVehRegNo.Text = ""
            txtRemark.Text = ""
        End If

        SetObjectAccessibilityByStatus()
        SetObjectAccessibilityByJobType()
    End Sub

    Sub BindJobTypeDropDownList()
        ddlJobType.Items.Clear()
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.StaffPayroll), objWSTrx.EnumJobType.StaffPayroll))
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.StaffDebitNote), objWSTrx.EnumJobType.StaffDebitNote))
        ddlJobType.SelectedIndex = 0
    End Sub

    Sub BindServTypeCodeDropDownList(ByVal pv_strServTypeCode As String)
        Dim strOpCd As String = "WS_CLSSETUP_SERVTYPE_LIST_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "||" & objWSSetup.EnumServiceTypeStatus.Active & "||ServTypeCode||"
        Try
            intErrNo = objWSSetup.mtdGetServType(strOpCd, strParam, dsList)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_BIND_SERV_TYPE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("ServTypeCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("ServTypeCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("ServTypeCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("ServTypeCode"))) = LCase(Trim(pv_strServTypeCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("ServTypeCode") = ""
        drNew("Description") = lblSelect.Text & lblServTypeCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strServTypeCode) <> "" And intSelectedIndex = 0 And APPEND_SERV_TYPE_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("ServTypeCode") = Trim(pv_strServTypeCode)
            drNew("Description") = Trim(pv_strServTypeCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlServTypeCode.DataSource = dsList.Tables(0)
        ddlServTypeCode.DataValueField = "ServTypeCode"
        ddlServTypeCode.DataTextField = "Description"
        ddlServTypeCode.DataBind()
        ddlServTypeCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindChargeToDropDownList(ByVal pv_strLocCode As String)
        Dim strOpCd As String
        Dim drNew As DataRow
        Dim dsList As DataSet
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
                       objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Workshop) & "|" & _
                       "WSAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration) & "|" & _
                       "WSAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration)
            intErrNo = objAdminLoc.mtdGetInterEstateLoc(strOpCd, strParam, dsList)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_BIND_CHARGE_TO&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("LocCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("LocCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("Description"))
            If dsList.Tables(0).Rows(intCnt).Item("LocCode") = Trim(pv_strLocCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("LocCode") = ""
        drNew("Description") = lblSelect.Text & "Charge To " & lblLocationTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strLocCode) <> "" And intSelectedIndex = 0 Then
            drNew = dsList.Tables(0).NewRow()
            drNew("LocCode") = Trim(pv_strLocCode)
            drNew("Description") = Trim(pv_strLocCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlChargeTo.DataSource = dsList.Tables(0)
        ddlChargeTo.DataValueField = "LocCode"
        ddlChargeTo.DataTextField = "Description"
        ddlChargeTo.DataBind()
        ddlChargeTo.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindAccountComponents(ByVal pv_strLocCode As String, ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String, ByVal pv_strVehCode As String, ByVal pv_strVehExpCode As String)
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer

        GetAccountProperties(pv_strAccCode, intAccType, intAccPurpose, intNurseryInd)

        If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLSetup.EnumAccountPurpose.NonVehicle
                    BindBlkCodeDropDownList(pv_strLocCode, pv_strAccCode, pv_strBlkCode)
                    BindVehCodeDropDownList(pv_strLocCode, "", "")
                    BindVehExpCodeDropDownList("", True)
                Case objGLSetup.EnumAccountPurpose.VehicleDistribution
                    BindBlkCodeDropDownList(pv_strLocCode, "", "")
                    BindVehCodeDropDownList(pv_strLocCode, pv_strAccCode, pv_strVehCode)
                    BindVehExpCodeDropDownList(pv_strVehExpCode, False)
                Case Else
                    BindBlkCodeDropDownList(pv_strLocCode, pv_strAccCode, pv_strBlkCode)
                    BindVehCodeDropDownList(pv_strLocCode, "%", pv_strVehCode)
                    BindVehExpCodeDropDownList(pv_strVehExpCode, False)
            End Select
        ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
            BindBlkCodeDropDownList(pv_strLocCode, pv_strAccCode, pv_strBlkCode)
            BindVehCodeDropDownList(pv_strLocCode, "", "")
            BindVehExpCodeDropDownList("", True)
        Else
            BindBlkCodeDropDownList(pv_strLocCode, "", "")
            BindVehCodeDropDownList(pv_strLocCode, "", "")
            BindVehExpCodeDropDownList("", True)
        End If
    End Sub

    Sub BindAccCodeDropDownList(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsList)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_BIND_ACCOUNT&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("AccCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("AccCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("AccCode"))) = LCase(Trim(pv_strAccCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("AccCode") = ""
        drNew("Description") = lblSelect.Text & lblAccCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strAccCode) <> "" And intSelectedIndex = 0 And APPEND_ACC_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("AccCode") = Trim(pv_strAccCode)
            drNew("Description") = Trim(pv_strAccCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlAccCode.DataSource = dsList.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindBlkCodeDropDownList(ByVal pv_strLocation As String, ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strOpCd = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = Trim(pv_strAccCode) & "|" & pv_strLocation & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = Trim(pv_strAccCode) & "|" & pv_strLocation & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, strParam, dsList)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_BIND_BLOCK&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("BlkCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("BlkCode"))) = LCase(Trim(pv_strBlkCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("BlkCode") = ""
        drNew("Description") = lblSelect.Text & lblBlkCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strBlkCode) <> "" And intSelectedIndex = 0 And APPEND_BLK_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("BlkCode") = Trim(pv_strBlkCode)
            drNew("Description") = Trim(pv_strBlkCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlBlkCode.DataSource = dsList.Tables(0)
        ddlBlkCode.DataValueField = "BlkCode"
        ddlBlkCode.DataTextField = "Description"
        ddlBlkCode.DataBind()
        ddlBlkCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindVehCodeDropDownList(ByVal pv_strLocation As String, ByVal pv_strAccCode As String, ByVal pv_strVehCode As String)
        Dim strOpCd As String = "WS_CLSTRX_VEH_LIST_GET"

        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "|AccCode = '" & pv_strAccCode.Trim() & "' AND LocCode = '" & pv_strLocation & "' AND A.Status = '" & objGLSetup.EnumVehicleStatus.Active & "'"

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.Vehicle, dsList)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_BIND_VEHICLE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("VehCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("VehCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("VehCode")) & " ( " & _
                                                                Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & " )"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("VehCode"))) = LCase(Trim(pv_strVehCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drNew = dsList.Tables(0).NewRow()
        drNew("VehCode") = ""
        drNew("Description") = lblSelect.Text & lblVehCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strVehCode) <> "" And intSelectedIndex = 0 And APPEND_VEH_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("VehCode") = Trim(pv_strVehCode)
            drNew("Description") = Trim(pv_strVehCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlVehCode.DataSource = dsList.Tables(0)
        ddlVehCode.DataValueField = "VehCode"
        ddlVehCode.DataTextField = "Description"
        ddlVehCode.DataBind()
        ddlVehCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindVehExpCodeDropDownList(ByVal pv_strVehExpCode As String, ByVal pv_IsBlankList As Boolean)
        Dim strOpCd As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "Order By VehExpenseCode ASC|And Veh.Status = '" & objGLSetup.EnumVehicleExpenseStatus.Active & "'"
        Try
            If pv_IsBlankList Then
                strParam += " And Veh.VehExpensecode = ''"
            End If
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.VehicleExpense, dsList)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_BIND_VEHICLE_EXPENSE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode")) & " ( " & _
                                                                Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & " )"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode"))) = LCase(Trim(pv_strVehExpCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drNew = dsList.Tables(0).NewRow()
        drNew(0) = ""
        drNew(1) = lblSelect.Text & lblVehExpCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strVehExpCode) <> "" And intSelectedIndex = 0 And APPEND_VEH_EXP_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("VehExpenseCode") = Trim(pv_strVehExpCode)
            drNew("Description") = Trim(pv_strVehExpCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlVehExpCode.DataSource = dsList.Tables(0)
        ddlVehExpCode.DataValueField = "VehExpenseCode"
        ddlVehExpCode.DataTextField = "Description"
        ddlVehExpCode.DataBind()
        ddlVehExpCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindEmpCodeDropDownList(ByVal pv_strLocCode As String, ByVal pv_strEmpCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_EMPLOYEE_LIST_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "|||" & objHRTrx.EnumEmpStatus.Active & "|" & pv_strLocCode & "|Mst.EmpCode|ASC"
        Try
            intErrNo = objHRTrx.mtdGetEmployeeList(strOpCd, strParam, dsList)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_BIND_EMPLOYEE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("EmpCode"))
            dsList.Tables(0).Rows(intCnt).Item("EmpName") = Trim(dsList.Tables(0).Rows(intCnt).Item("EmpCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("EmpCode"))) = LCase(Trim(pv_strEmpCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("EmpCode") = ""
        drNew("EmpName") = lblSelect.Text & "Employee Code"
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strEmpCode) <> "" And intSelectedIndex = 0 And APPEND_EMP_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("EmpCode") = Trim(pv_strEmpCode)
            drNew("EmpName") = Trim(pv_strEmpCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlEmpCode.DataSource = dsList.Tables(0)
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataTextField = "EmpName"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindBillPartyCodeDropDownList(ByVal pv_strBillPartyCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "||" & objGLSetup.EnumBillPartyStatus.Active & "||BP.BillPartyCode|ASC|"
        Try
            intErrNo = objGLSetup.mtdGetBillParty(strOpCd, strParam, dsList)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_BIND_BILL_PARTY&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("BillPartyCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("BillPartyCode"))
            dsList.Tables(0).Rows(intCnt).Item("Name") = Trim(dsList.Tables(0).Rows(intCnt).Item("BillPartyCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Name")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("BillPartyCode"))) = LCase(Trim(pv_strBillPartyCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("BillPartyCode") = ""
        drNew("Name") = lblSelect.Text & lblBillPartyCode.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strBillPartyCode) <> "" And intSelectedIndex = 0 And APPEND_BILL_PARTY_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("BillPartyCode") = Trim(pv_strBillPartyCode)
            drNew("Name") = Trim(pv_strBillPartyCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlBillPartyCode.DataSource = dsList.Tables(0)
        ddlBillPartyCode.DataValueField = "BillPartyCode"
        ddlBillPartyCode.DataTextField = "Name"
        ddlBillPartyCode.DataBind()
        ddlBillPartyCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindWorkCodeDropDownList(ByVal pv_strServTypeCode As String, ByVal pv_strWorkCode As String)
        Dim strOpCd As String = "WS_CLSTRX_WORKCODE_GET"
        Dim colParam As New Collection
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strSearch As String
        Dim strErrMsg As String

        strSearch = "WHERE WC.Status = '" & objWSSetup.EnumWorkCodeStatus.Active & "' AND WC.ServTypeCode = '" & FixSQL(pv_strServTypeCode) & "'" & vbCrLf & "ORDER BY WC.WorkCode"
        colParam.Add(strSearch, "PM_SEARCH")
        colParam.Add(strOpCd, "OC_JOB_WORK_CODE_GET")
        Try
            intErrNo = objWSTrx.mtdJobWorkCode_Get(colParam, dsList, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_BIND_WORKCODE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("WorkCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("WorkCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("WorkCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("WorkCode"))) = LCase(Trim(pv_strWorkCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("WorkCode") = ""
        drNew("Description") = lblSelect.Text & lblWorkCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strWorkCode) <> "" And intSelectedIndex = 0 And APPEND_WORK_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("WorkCode") = Trim(pv_strWorkCode)
            drNew("Description") = Trim(pv_strWorkCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlWorkCode.DataSource = dsList.Tables(0)
        ddlWorkCode.DataValueField = "WorkCode"
        ddlWorkCode.DataTextField = "Description"
        ddlWorkCode.DataBind()
        ddlWorkCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub ddlServTypeCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        BindWorkCodeDropDownList(GetDropDownListValue(ddlServTypeCode), "")
    End Sub

    Sub ddlChargeTo_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        BindBlkCodeDropDownList(GetDropDownListValue(ddlChargeTo), GetDropDownListValue(ddlAccCode), "")
        BindVehCodeDropDownList(GetDropDownListValue(ddlChargeTo), GetDropDownListValue(ddlAccCode), "")
        BindEmpCodeDropDownList(GetDropDownListValue(ddlChargeTo), "")
        If Trim(lblJobType.Text) = Trim(CStr(objWSTrx.EnumJobType.InternalUse)) = True Then
            trAccCode.Visible = True
            trBlkCode.Visible = True
            trVehCode.Visible = True
            trVehExpCode.Visible = True
        Else
            trAccCode.Visible = False
            trBlkCode.Visible = False
            trVehCode.Visible = False
            trVehExpCode.Visible = False
        End If
    End Sub

    Sub ddlAccCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strLoc As String = Trim(GetDropDownListValue(ddlChargeTo))
        Dim strAcc As String = Trim(GetDropDownListValue(ddlAccCode))
        Dim strBlk As String = Trim(GetDropDownListValue(ddlBlkCode))
        Dim strVeh As String = Trim(GetDropDownListValue(ddlVehCode))
        Dim strVehExp As String = Trim(GetDropDownListValue(ddlVehExpCode))

        Call BindAccountComponents(strLoc, strAcc, strBlk, strVeh, strVehExp)
    End Sub

    Protected Function GetJobHeaderDS() As DataSet
        Dim dsTemp As New DataSet
        Dim colParam As New Collection
        Dim strSearch As String
        Dim strErrMsg As String

        Try
            strSearch = " WHERE J.JobID = '" & FixSQL(Trim(lblJobID.Text)) & "'"
            colParam.Add(strSearch, "PM_SEARCH")
            colParam.Add(strOpCode_Job_Get, "OC_JOB_GET")

            intErrNo = objWSTrx.mtdJob_Get(colParam, dsTemp, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If

            Return dsTemp
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_GET_JOB_HEADER&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
    End Function

    Protected Function GetJobLinesDS() As DataSet
        Dim dsTemp As New DataSet
        Dim colParam As New Collection
        Dim strSearch As String
        Dim strErrMsg As String

        Try
            strSearch = " WHERE JWC.JobID = '" & FixSQL(Trim(lblJobID.Text)) & "'"
            colParam.Add(strSearch, "PM_SEARCH")
            colParam.Add(Trim(lblWorkCodeTag.Text), "LC_WORKCODE")
            colParam.Add(strOpCode_JobWorkCode_Get, "OC_JOB_WORK_CODE_GET")

            intErrNo = objWSTrx.mtdJobWorkCode_Get(colParam, dsTemp, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If

            Return dsTemp
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_GET_JOB_LINE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
    End Function

    Function GetLanguageCaptionDS() As DataSet
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim dsLC As DataSet
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
                                                 dsLC, _
                                                 strParam)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_GET_LANGCAP&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        Return dsLC
        If Not dsLC Is Nothing Then
            dsLC = Nothing
        End If
    End Function

    Sub GetAccountProperties(ByVal pv_strAccCode As String, _
                             ByRef pr_strAccType As Integer, _
                             ByRef pr_strAccPurpose As Integer, _
                             ByRef pr_strNurseryInd As Integer)

        Dim objAccDs As New DataSet
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, strParam, objAccDs, True)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_GET_ACCOUNT_PROPERTY&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        If objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
        End If
    End Sub

    Sub DisplayJobHeader()
        Dim intCnt As Integer
        Dim dsHeader As DataSet
        dsHeader = GetJobHeaderDS()
        lblJobID.Text = Trim(dsHeader.Tables(0).Rows(0).Item("JobID"))
        lblJobType.Text = Trim(dsHeader.Tables(0).Rows(0).Item("JobType"))
        lblJobTypeText.Text = objWSTrx.mtdGetJobType(lblJobType.Text)
        If Trim(CStr(lblJobType.Text)) = Trim(CStr(ddlJobType.Items(0).Value)) Then
            ddlJobType.SelectedIndex = 0
        Else
            ddlJobType.SelectedIndex = 1
        End If
        txtDescription.Text = Trim(dsHeader.Tables(0).Rows(0).Item("Description"))
        txtDocRefNo.Text = Trim(dsHeader.Tables(0).Rows(0).Item("DocRefNo"))
        txtDocRefDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), dsHeader.Tables(0).Rows(0).Item("DocRefDate"))
        Call DecomposeDate(dsHeader.Tables(0).Rows(0).Item("JobStartDate"), txtJobStartDate, txtJobStartTimeHour, txtJobStartTimeMinute, ddlJobStartTimeDayNight)
        Call DecomposeDate(dsHeader.Tables(0).Rows(0).Item("JobEndDate"), txtJobEndDate, txtJobEndTimeHour, txtJobEndTimeMinute, ddlJobEndTimeDayNight)

        txtChrgRate.Text = RoundNumber(dsHeader.Tables(0).Rows(0).Item("ChrgRate"), 0)

        BindServTypeCodeDropDownList(Trim(dsHeader.Tables(0).Rows(0).Item("ServTypeCode")))
        BindChargeToDropDownList(Trim(dsHeader.Tables(0).Rows(0).Item("ChargeLocCode")))
        BindAccCodeDropDownList(Trim(dsHeader.Tables(0).Rows(0).Item("AccCode")))
        Call BindAccountComponents(Trim(dsHeader.Tables(0).Rows(0).Item("ChargeLocCode")), _
                                   Trim(dsHeader.Tables(0).Rows(0).Item("AccCode")), _
                                   Trim(dsHeader.Tables(0).Rows(0).Item("BlkCode")), _
                                   Trim(dsHeader.Tables(0).Rows(0).Item("VehCode")), _
                                   Trim(dsHeader.Tables(0).Rows(0).Item("VehLabExpCode")))
        BindEmpCodeDropDownList(Trim(dsHeader.Tables(0).Rows(0).Item("ChargeLocCode")), Trim(dsHeader.Tables(0).Rows(0).Item("EmpCode")))
        BindBillPartyCodeDropDownList(Trim(dsHeader.Tables(0).Rows(0).Item("BillPartyCode")))
        BindWorkCodeDropDownList(Trim(dsHeader.Tables(0).Rows(0).Item("ServTypeCode")), "")

        txtVehRegNo.Text = Trim(dsHeader.Tables(0).Rows(0).Item("VehRegNo"))
        txtRemark.Text = Trim(dsHeader.Tables(0).Rows(0).Item("Remark"))

        lblPeriod.Text = Trim(dsHeader.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsHeader.Tables(0).Rows(0).Item("AccYear"))
        lblStatus.Text = Trim(dsHeader.Tables(0).Rows(0).Item("Status"))
        lblStatusText.Text = objWSTrx.mtdGetJobStatus(lblStatus.Text)
        lblJobStockCount.Text = dsHeader.Tables(0).Rows(0).Item("JobStockCount")
        lblMechHourCount.Text = dsHeader.Tables(0).Rows(0).Item("MechHourCount")
        lblEditDependency.Text = dsHeader.Tables(0).Rows(0).Item("EditDependency")
        lblCreateDate.Text = objGlobal.GetLongDate(dsHeader.Tables(0).Rows(0).Item("CreateDate"))
        lblUpdateDate.Text = objGlobal.GetLongDate(dsHeader.Tables(0).Rows(0).Item("UpdateDate"))
        lblUserName.Text = Trim(dsHeader.Tables(0).Rows(0).Item("UserName"))
        lblPayrollPosted.Text = dsHeader.Tables(0).Rows(0).Item("PayrollPosted")
        lblPrintDate.Text = dsHeader.Tables(0).Rows(0).Item("PrintDate")
        SetObjectAccessibilityByStatus()
        SetObjectAccessibilityByJobType()
    End Sub

    Sub DisplayJobLines()
        dgWorkCode.DataSource = GetJobLinesDS()
        dgWorkCode.DataBind()
    End Sub

    Sub dgWorkCode_OnItemDataBound(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim lbTemp As LinkButton
        Dim lblTemp As Label

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            lbTemp = e.Item.FindControl("lbDelete")
            lblTemp = e.Item.FindControl("lblDeleteDependency")
            If Trim(lblStatus.Text) = Trim(CStr(objWSTrx.EnumJobStatus.Active)) And CInt(lblTemp.Text) = 0 Then
                lbTemp.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                lbTemp.Visible = True
            Else
                lbTemp.Visible = False
            End If
        End If
    End Sub

    Sub dgWorkCode_OnDeleteCommand(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim colParam As New Collection
        Dim lblTemp As Label
        Dim strErrMsg As String

        colParam.Add(Trim(lblJobID.Text), "PM_JOBID")
        lblTemp = E.Item.FindControl("lblWorkCode")
        colParam.Add(Trim(lblTemp.Text), "PM_WORKCODE")
        colParam.Add(Trim(lblWorkCodeTag.Text), "LC_WORKCODE")
        colParam.Add(strOpCode_JobWorkCode_Get, "OC_JOB_WORK_CODE_GET")
        colParam.Add(strOpCode_JobWorkCode_Del, "OC_JOB_WORK_CODE_DEL")

        Try
            intErrNo = objWSTrx.mtdJobWorkCode_Delete(colParam, strErrMsg)

            If intErrNo <> objWSTrx.EnumException.NoError Then
                lblActionResult.Text = "Cannot perform the operation. " & strErrMsg
                lblActionResult.Visible = True
            Else
                ResetPage(True, True, False)
            End If
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_ON_DELETE_COMMAND&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try
    End Sub

    Function mtdSaveJobHeader(ByVal pv_blnResetPage As Boolean) As Boolean
        Dim colParam As New Collection
        Dim strErrMsg As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strJobID As String = Trim(lblJobID.Text)
        Dim strJobType As String = Trim(lblJobType.Text)
        Dim strDescription As String = Trim(txtDescription.Text)
        Dim strDocRefNo As String = Trim(txtDocRefNo.Text)
        Dim strDocRefDate As String = Trim(txtDocRefDate.Text)
        Dim strJobStartDate As String
        Dim strJobStartTimeHour As String = Trim(txtJobStartTimeHour.Text)
        Dim strJobStartTimeMinute As String = Trim(txtJobStartTimeMinute.Text)
        Dim strJobStartDayNight As String = Trim(GetDropDownListValue(ddlJobStartTimeDayNight))
        Dim strJobEndDate As String
        Dim strJobEndTimeHour As String = Trim(txtJobEndTimeHour.Text)
        Dim strJobEndTimeMinute As String = Trim(txtJobEndTimeMinute.Text)
        Dim strJobEndTimeDayNight As String = Trim(GetDropDownListValue(ddlJobEndTimeDayNight))
        Dim strChrgRate As String = Trim(txtChrgRate.Text)
        Dim strServTypeCode As String = Trim(GetDropDownListValue(ddlServTypeCode))
        Dim strChargeLocCode As String = Trim(GetDropDownListValue(ddlChargeTo))
        Dim strAccCode As String = Trim(GetDropDownListValue(ddlAccCode))
        Dim strBlkCode As String = Trim(GetDropDownListValue(ddlBlkCode))
        Dim strVehCode As String = Trim(GetDropDownListValue(ddlVehCode))
        Dim strVehExpCode As String = Trim(GetDropDownListValue(ddlVehExpCode))
        Dim strEmpCode As String = Trim(GetDropDownListValue(ddlEmpCode))
        Dim strBillPartyCode As String = Trim(GetDropDownListValue(ddlBillPartyCode))
        Dim strVehRegNo As String = Trim(txtVehRegNo.Text)
        Dim strRemark As String = Trim(txtRemark.Text)
        Dim strStatus As String = Trim(lblStatus.Text)
        Dim strPrintDate As String = Trim(lblPrintDate.Text)

        If strJobID = "" And (strJobType = Trim(CStr(objWSTrx.EnumJobType.StaffPayroll)) Or strJobType = Trim(CStr(objWSTrx.EnumJobType.StaffDebitNote))) Then
            strJobType = Trim(GetDropDownListValue(ddlJobType))
        End If

        If strDescription = "" Then
            lblDescriptionErr.Visible = True
        End If
        If strDocRefNo = "" Then
            lblDocRefNoErr.Visible = True
        End If
        If strDocRefDate = "" Then
            lblDocRefDateErr.Text = "Document Ref Date cannot be blank"
            lblDocRefDateErr.Visible = True
        Else
            strDocRefDate = GetValidDate(strDocRefDate, strErrMsg)
            If strDocRefDate = "" Then
                lblDocRefDateErr.Text = strErrMsg
                lblDocRefDateErr.Visible = True
            End If
        End If
        If ComposeDate("Job Start Date", "Job Start Time (Hour)", "Job Start Time (Minute)", txtJobStartDate, txtJobStartTimeHour, txtJobStartTimeMinute, ddlJobStartTimeDayNight, strJobStartDate, strErrMsg) = False Then
            lblJobStartDateErr.Text = strErrMsg
            lblJobStartDateErr.Visible = True
        ElseIf strJobStartDate = "" Then
            lblJobStartDateErr.Text = "Job Start Date cannot be blank"
            lblJobStartDateErr.Visible = True
        End If
        If ComposeDate("Job End Date", "Job End Time (Hour)", "Job End Time (Minute)", txtJobEndDate, txtJobEndTimeHour, txtJobEndTimeMinute, ddlJobEndTimeDayNight, strJobEndDate, strErrMsg) = False Then
            lblJobEndDateErr.Text = strErrMsg
            lblJobEndDateErr.Visible = True
        End If

        If Trim(strJobEndDate) <> "" Then
            If CDate(strJobStartDate) >= CDate(strJobEndDate) Then
                lblJobEndDateErr.Text = "Job End Date can not less than or equal to Job Start Date"
                lblJobEndDateErr.Visible = True
            End If
        End If

        If strChrgRate = "" Then
            lblChrgRateErr.Text = "<br>Labour Hourly Rate cannot be blank"
            lblChrgRateErr.Visible = True
        ElseIf IsNumeric(strChrgRate) = False Then
            lblChrgRateErr.Text = "<br>Labour Hourly Rate must be a number"
            lblChrgRateErr.Visible = True
        End If
        If strServTypeCode = "" Then
            lblServTypeCodeErr.Visible = True
        End If
        If strChargeLocCode = "" Then
            lblChargeToErr.Visible = True
        End If
        If Trim(lblJobType.Text) = Trim(CStr(objWSTrx.EnumJobType.InternalUse)) Then
            If strAccCode = "" Then
                lblAccCodeErr.Visible = True
            Else
                GetAccountProperties(strAccCode, intAccType, intAccPurpose, intNurseryInd)
                If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
                    Select Case intAccPurpose
                        Case objGLSetup.EnumAccountPurpose.NonVehicle
                            If strBlkCode = "" Then
                                lblBlkCodeErr.Visible = True
                            End If
                            strVehCode = ""
                            strVehExpCode = ""
                        Case objGLSetup.EnumAccountPurpose.VehicleDistribution
                            strBlkCode = ""
                            If strVehCode = "" Then
                                lblVehCodeErr.Visible = True
                            End If
                            If strVehExpCode = "" Then
                                lblVehExpCodeErr.Visible = True
                            End If
                        Case Else
                            If strBlkCode = "" Then
                                lblBlkCodeErr.Visible = True
                            End If
                            If strVehCode = "" Then
                                lblVehCodeErr.Visible = True
                            End If
                            If strVehExpCode = "" Then
                                lblVehExpCodeErr.Visible = True
                            End If
                    End Select
                ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
                    If strBlkCode = "" Then
                        lblBlkCodeErr.Visible = True
                    End If
                    strVehCode = ""
                    strVehExpCode = ""
                Else
                    strBlkCode = ""
                    strVehCode = ""
                    strVehExpCode = ""
                End If
            End If
        Else
            strAccCode = ""
            strBlkCode = ""
            strVehCode = ""
            strVehExpCode = ""
        End If
        If strJobType = Trim(CStr(objWSTrx.EnumJobType.InternalUse)) Then
            strVehRegNo = ""
        Else
            If strVehRegNo = "" Then
                lblVehRegNoErr.Visible = True
            End If
        End If
        If strJobType = Trim(CStr(objWSTrx.EnumJobType.StaffPayroll)) Or strJobType = Trim(CStr(objWSTrx.EnumJobType.StaffDebitNote)) Then
            If strEmpCode = "" Then
                lblEmpCodeErr.Visible = True
            End If
        Else
            strEmpCode = ""
        End If
        If strJobType = Trim(CStr(objWSTrx.EnumJobType.ExternalParty)) Then
            If strBillPartyCode = "" Then
                lblBillPartyCodeErr.Visible = True
            End If
        Else
            strBillPartyCode = ""
        End If

        mtdSaveJobHeader = False
        If lblDescriptionErr.Visible = False And lblDocRefNoErr.Visible = False And _
           lblDocRefDateErr.Visible = False And lblJobStartDateErr.Visible = False And _
           lblJobEndDateErr.Visible = False And lblChrgRateErr.Visible = False And _
           lblServTypeCodeErr.Visible = False And lblChargeToErr.Visible = False And _
           lblAccCodeErr.Visible = False And lblBlkCodeErr.Visible = False And _
           lblVehCodeErr.Visible = False And lblVehExpCodeErr.Visible = False And _
           lblEmpCodeErr.Visible = False And lblBillPartyCodeErr.Visible = False And _
           lblVehRegNoErr.Visible = False Then
            If strJobID = "" Then
                colParam.Add(strDescription, "PM_DESCRIPTION")
                colParam.Add(strDocRefNo, "PM_DOCREFNO")
                colParam.Add(strDocRefDate, "PM_DOCREFDATE")
                colParam.Add(strAccCode, "PM_ACCCODE")
                colParam.Add(strBlkCode, "PM_BLKCODE")
                colParam.Add(strVehCode, "PM_VEHCODE")
                colParam.Add(strVehExpCode, "PM_VEHLABEXPCODE")
                colParam.Add(strChrgRate, "PM_CHRGRATE")
                colParam.Add(strServTypeCode, "PM_SERVTYPECODE")
                colParam.Add(strJobType, "PM_JOBTYPE")
                colParam.Add(strBillPartyCode, "PM_BILLPARTYCODE")
                colParam.Add(strEmpCode, "PM_EMPCODE")
                colParam.Add(strVehRegNo, "PM_VEHREGNO")
                colParam.Add(strJobStartDate, "PM_JOBSTARTDATE")
                colParam.Add(strJobEndDate, "PM_JOBENDDATE")
                colParam.Add(strRemark, "PM_REMARK")
                colParam.Add(strLocation, "PM_LOCCODE")
                colParam.Add(strAccMonth, "PM_ACCMONTH")
                colParam.Add(strAccYear, "PM_ACCYEAR")
                colParam.Add(strUserId, "PM_UPDATEID")
                colParam.Add(strChargeLocCode, "PM_CHARGELOCCODE")
                colParam.Add(strOpCode_Job_Add, "OC_JOB_ADD")
                Try
                    intErrNo = objWSTrx.mtdJob_Add(colParam, strJobID, strErrMsg)
                    If intErrNo = objWSTrx.EnumException.NoError Then
                        lblJobID.Text = strJobID
                        If pv_blnResetPage = True Then
                            ResetPage(True, True, False)
                        End If
                        mtdSaveJobHeader = True
                    Else
                        lblActionResult.Text = strErrMsg
                        lblActionResult.Visible = True
                    End If
                Catch ex As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_ADD&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
                End Try
            Else
                colParam.Add(strJobID, "PM_JOBID")

                colParam.Add(strChargeLocCode, "PM_CHARGELOCCODE")
                colParam.Add(strAccCode, "PM_ACCCODE")
                colParam.Add(strBlkCode, "PM_BLKCODE")
                colParam.Add(strVehCode, "PM_VEHCODE")
                colParam.Add(strVehExpCode, "PM_VEHLABEXPCODE")

                colParam.Add(strDescription, "PM_DESCRIPTION")
                colParam.Add(strDocRefNo, "PM_DOCREFNO")
                colParam.Add(strDocRefDate, "PM_DOCREFDATE")
                colParam.Add(strChrgRate, "PM_CHRGRATE")
                colParam.Add(strServTypeCode, "PM_SERVTYPECODE")
                colParam.Add(strJobStartDate, "PM_JOBSTARTDATE")
                colParam.Add(strJobEndDate, "PM_JOBENDDATE")
                colParam.Add(strRemark, "PM_REMARK")
                colParam.Add(strLocation, "PM_LOCCODE")
                colParam.Add(strJobType, "PM_JOBTYPE")
                colParam.Add(strBillPartyCode, "PM_BILLPARTYCODE")
                colParam.Add(strEmpCode, "PM_EMPCODE")
                colParam.Add(strVehRegNo, "PM_VEHREGNO")

                colParam.Add(strUserId, "PM_UPDATEID")
                colParam.Add("true", "PM_UPDATE_HEADER")
                colParam.Add("false", "PM_UPDATE_ACCOUNT_PERIOD")
                colParam.Add("true", "PM_UPDATE_ACCOUNT")
                colParam.Add("true", "PM_UPDATE_DETAIL")
                colParam.Add("true", "PM_UPDATE_LABOUR_HOURLY_RATE")
                colParam.Add("false", "PM_UPDATE_PRINTDATE")
                colParam.Add("false", "PM_UPDATE_STATUS")
                colParam.Add("false", "PM_UPDATE_PAYROLLPOSTED")

                colParam.Add(strOpCode_Job_Get_For_Upd, "OC_JOB_GET_FOR_UPDATE")
                colParam.Add(strOpCode_Job_Upd, "OC_JOB_UPD")
                colParam.Add(strOpCode_Job_Get, "OC_JOB_GET")
                colParam.Add(strOpCode_Mechanic_Hour_Document_Get, "OC_MECHANIC_HOUR_DOCUMENT_GET")
                colParam.Add(strOpCode_Mechanic_Hour_Line_Upd, "OC_MECHANIC_HOUR_LINE_UPD")
                colParam.Add(strOpCode_Mechanic_Hour_Upd, "OC_MECHANIC_HOUR_UPD")

                colParam.Add(Mid(lblAccCodeTag.Text, 1, Len(lblAccCodeTag.Text) - Len(lblCode.Text)), "LC_ACCOUNT")
                colParam.Add(Mid(lblBlkCodeTag.Text, 1, Len(lblBlkCodeTag.Text) - Len(lblCode.Text)), "LC_BLOCK")
                colParam.Add(Mid(lblVehCodeTag.Text, 1, Len(lblVehCodeTag.Text) - Len(lblCode.Text)), "LC_VEHICLE")
                Try
                    intErrNo = objWSTrx.mtdJob_Update(colParam, strErrMsg)
                    If intErrNo = objWSTrx.EnumException.NoError Then
                        If pv_blnResetPage = True Then
                            ResetPage(True, True, False)
                        End If
                        mtdSaveJobHeader = True
                    Else
                        lblActionResult.Text = strErrMsg
                        lblActionResult.Visible = True
                    End If
                Catch ex As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_UPDATE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
                End Try
            End If
        End If
    End Function

    Sub ibAdd_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim colParam As New Collection
        Dim strErrMsg As String
        Dim strWorkCode As String = Trim(GetDropDownListValue(ddlWorkCode))
        If strWorkCode = "" Then
            lblWorkCodeErr.Visible = True
            Exit Sub
        ElseIf mtdSaveJobHeader(False) = False Then
            Exit Sub
        End If

        colParam.Add(Trim(lblJobID.Text), "PM_JOBID")
        colParam.Add(strWorkCode, "PM_WORKCODE")
        colParam.Add(Trim(lblWorkCodeTag.Text), "LC_WORKCODE")
        colParam.Add(strOpCode_JobWorkCode_Add, "OC_JOB_WORK_CODE_ADD")
        colParam.Add(strOpCode_JobWorkCode_Get, "OC_JOB_WORK_CODE_GET")
        Try
            intErrNo = objWSTrx.mtdJobWorkCode_Add(colParam, strErrMsg)

            If intErrNo <> objWSTrx.EnumException.NoError Then
                lblActionResult.Text = "Cannot perform the operation. " & strErrMsg
                lblActionResult.Visible = True
            Else
                ResetPage(True, True, False)
            End If
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_ADD&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try
    End Sub

    Sub ibSave_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If mtdSaveJobHeader(True) = False Then
            Exit Sub
        End If

    End Sub

    Sub ibDelete_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim colParam As New Collection
        Dim strErrMsg As String

        colParam.Add(Trim(lblJobID.Text), "PM_JOBID")
        colParam.Add(strUserId, "PM_UPDATEID")
        colParam.Add(strOpCode_Job_Get, "OC_JOB_GET")
        colParam.Add(strOpCode_Job_Upd, "OC_JOB_UPD")
        Try
            If Trim(lblStatus.Text) = Trim(CStr(objWSTrx.EnumJobStatus.Active)) Then
                intErrNo = objWSTrx.mtdJob_Delete(colParam, strErrMsg)
            Else
                intErrNo = objWSTrx.mtdJob_Undelete(colParam, strErrMsg)
            End If

            If intErrNo <> objWSTrx.EnumException.NoError Then
                lblActionResult.Text = "Cannot perform the operation. " & strErrMsg
                lblActionResult.Visible = True
            Else
                ResetPage(True, True, False)
            End If
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_DELETE_UNDELETE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try
    End Sub

    Sub ibNew_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("WS_Trx_Job_Detail.aspx?jobtype=" & Trim(lblJobType.Text))
    End Sub

    Sub ibBack_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("WS_Trx_Job_List.aspx")
    End Sub

    Sub ibJobStartSetCurrentDateTime_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Call DecomposeDate(Now(), txtJobStartDate, txtJobStartTimeHour, txtJobStartTimeMinute, ddlJobStartTimeDayNight)
    End Sub

    Sub ibJobEndSetCurrentDateTime_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Call DecomposeDate(Now(), txtJobEndDate, txtJobEndTimeHour, txtJobEndTimeMinute, ddlJobEndTimeDayNight)
    End Sub

    Sub ibPartsIssueReturn_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strAccCode As String = Trim(GetDropDownListValue(ddlAccCode))

        If strLocType = objLoc.EnumLocType.Mill Then
            If lblJobType.Text = objWSTrx.EnumJobType.InternalUse Then
                Response.Redirect("WS_Trx_Job_PartsIssueReturn.aspx?jobid=" & Trim(lblJobID.Text) & "&AccCode=" & strAccCode & "&SubBlkCode= " & Trim(GetDropDownListValue(ddlBlkCode)))
            Else
                Response.Redirect("WS_Trx_Job_PartsIssueReturn.aspx?jobid=" & Trim(lblJobID.Text) & "&AccCode=" & strAccCode)
            End If
        Else
            Response.Redirect("WS_Trx_Job_PartsIssueReturn.aspx?jobid=" & Trim(lblJobID.Text))
        End If
    End Sub

    Sub ibProceedToJobClose_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("WS_Trx_Job_Close.aspx?jobid=" & Trim(lblJobID.Text))
    End Sub

    Sub mtdGenerateDNCN(ByVal pv_blnLabour As Boolean, ByVal pv_blnParts As Boolean)
        Dim strOpCode_DebitNote_Add As String = "BI_CLSTRX_DEBITNOTE_ADD"
        Dim strOpCode_DebitNote_Upd As String = "BI_CLSTRX_DEBITNOTE_UPD"
        Dim strOpCode_DebitNoteLine_Add As String = "BI_CLSTRX_DEBITNOTE_LINE_ADD"
        Dim strOpCode_DebitNoteLine_Sum As String = "BI_CLSTRX_DEBITNOTE_SUM_LINEAMOUNT_GET"
        Dim strOpCode_DebitNote_Amt_Upd As String = "BI_CLSTRX_DEBITNOTE_UPD"
        Dim strOpCode_CreditNote_Add As String = "BI_CLSTRX_CREDITNOTE_ADD"
        Dim strOpCode_CreditNote_Upd As String = "BI_CLSTRX_CREDITNOTE_UPD"
        Dim strOpCode_CreditNoteLine_Add As String = "BI_CLSTRX_CREDITNOTE_LINE_ADD"
        Dim strOpCode_CreditNoteLine_Sum As String = "BI_CLSTRX_CREDITNOTE_SUM_LINEAMOUNT_GET"
        Dim strOpCode_CreditNote_Amt_Upd As String = "BI_CLSTRX_CREDITNOTE_TOTALAMOUNT_UPD"

        Dim colParam As New Collection
        Dim strErrMsg As String
        Dim strDebitNoteID As String = ""
        Dim strCreditNoteID As String = ""

        colParam.Add(Trim(lblJobID.Text), "PM_JOBID")
        colParam.Add(IIf(pv_blnLabour = True, "true", "false"), "PM_LABOUR")
        colParam.Add(IIf(pv_blnParts = True, "true", "false"), "PM_PARTS")
        colParam.Add(Trim(strLocation), "PM_LOCCODE")
        colParam.Add(Session("SS_ARACCMONTH"), "PM_AR_ACCMONTH")
        colParam.Add(Session("SS_ARACCYEAR"), "PM_AR_ACCYEAR")
        colParam.Add(Trim(strCompany), "PM_COMPANY")
        colParam.Add(Trim(strUserId), "PM_UPDATEID")

        colParam.Add(strOpCode_Job_Transaction_Get, "OC_TRANSACTION_GET")
        colParam.Add(strOpCode_Job_Upd, "OC_JOB_UPD")
        colParam.Add(strOpCode_Job_Stock_Upd, "OC_JOB_STOCK_UPD")
        colParam.Add(strOpCode_Mechanic_Hour_Upd, "OC_MECHANIC_HOUR_UPD")
        colParam.Add(strOpCode_Mechanic_Hour_Line_Upd, "OC_MECHANIC_HOUR_LINE_UPD")

        colParam.Add(strOpCode_DebitNote_Add, "OC_DEBIT_NOTE_ADD")
        colParam.Add(strOpCode_DebitNote_Upd, "OC_DEBIT_NOTE_UPD")
        colParam.Add(strOpCode_DebitNoteLine_Add, "OC_DEBIT_NOTE_LINE_ADD")
        colParam.Add(strOpCode_DebitNoteLine_Sum, "OC_DEBIT_NOTE_LINE_AMOUNT_SUM_GET")
        colParam.Add(strOpCode_DebitNote_Amt_Upd, "OC_DEBIT_NOTE_TOTAL_AMOUNT_UPD")
        colParam.Add(strOpCode_CreditNote_Add, "OC_CREDIT_NOTE_ADD")
        colParam.Add(strOpCode_CreditNote_Upd, "OC_CREDIT_NOTE_UPD")
        colParam.Add(strOpCode_CreditNoteLine_Add, "OC_CREDIT_NOTE_LINE_ADD")
        colParam.Add(strOpCode_CreditNoteLine_Sum, "OC_CREDIT_NOTE_LINE_AMOUNT_SUM_GET")
        colParam.Add(strOpCode_CreditNote_Amt_Upd, "OC_CREDIT_NOTE_TOTAL_AMOUNT_UPD")
        Try
            intErrNo = objWSTrx.mtdJob_GenerateDNCN(colParam, strDebitNoteID, strCreditNoteID, strErrMsg)

            If intErrNo <> objWSTrx.EnumException.NoError Then
                ResetPage(True, True, False)
                lblActionResult.Text = "Cannot perform the operation. " & strErrMsg
                lblActionResult.Visible = True
            ElseIf strDebitNoteID <> "" Then
                Response.Redirect("../../BI/trx/BI_trx_DNDet.aspx?dbnid=" & strDebitNoteID & "&referer=" & Request.ServerVariables("SCRIPT_NAME") & "?jobid=" & lblJobID.Text, False)
            ElseIf strCreditNoteID <> "" Then
                Response.Redirect("../../BI/trx/BI_trx_CNDet.aspx?cnid=" & strCreditNoteID & "&referer=" & Request.ServerVariables("SCRIPT_NAME") & "?jobid=" & lblJobID.Text, False)
            Else
                ResetPage(True, True, False)
                lblActionResult.Text = "<font color=blue>No transaction line available to generate debit note/credit note</font>"
                lblActionResult.Visible = True
            End If
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_GENERATE_DNCN&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try
    End Sub

    Protected Function FixSQL(ByVal pv_strParam As String) As String
        Return Replace(Trim(pv_strParam), "'", "''")
    End Function

    Protected Function RoundNumber(ByVal d As Double, Optional ByVal decimals As Double = 0) As Decimal
        RoundNumber = Decimal.Divide(Int(Decimal.Add(Decimal.Multiply(d, 10.0 ^ decimals), 0.5)), 10.0 ^ decimals)
    End Function

    Protected Function GetDropDownListValue(ByRef pr_ddlObject As DropDownList) As String
        If Trim(Request.Form(pr_ddlObject.ID)) <> "" Then
            GetDropDownListValue = Trim(Request.Form(pr_ddlObject.ID))
        Else
            GetDropDownListValue = pr_ddlObject.SelectedItem.Value
        End If
    End Function

    Protected Function GetValidDate(ByVal pv_strInputDate As String, ByRef pr_strErrMsg As String) As String
        Dim strDateFormat As String
        Dim strSQLDate As String

        If objGlobal.mtdValidInputDate(Session("SS_DATEFMT"), _
                                       pv_strInputDate, _
                                       strDateFormat, _
                                       strSQLDate) = True Then
            GetValidDate = strSQLDate
            pr_strErrMsg = ""
        Else
            GetValidDate = ""
            pr_strErrMsg = "Date format should be in " & strDateFormat
        End If
    End Function

    Protected Function ComposeDate(ByVal pv_strLabelDate As String, ByVal pv_strLabelTimeHour As String, ByVal pv_strLabelTimeMinute As String, ByRef pr_txtDate As TextBox, ByRef pr_txtTimeHour As TextBox, ByRef pr_txtTimeMinute As TextBox, ByRef pr_ddlDayNight As DropDownList, ByRef pr_strDate As String, ByRef pr_strErrMsg As String) As Boolean
        Dim strDate As String = Trim(pr_txtDate.Text)
        Dim strTimeHour As String = Trim(pr_txtTimeHour.Text)
        Dim strTimeMinute As String = Trim(pr_txtTimeMinute.Text)
        Dim strDayNight As String = GetDropDownListValue(pr_ddlDayNight)
        Dim strValidDate As String
        Dim strErrMsg As String

        pr_strDate = ""
        ComposeDate = False
        If strDate = "" And strTimeHour = "" And strTimeMinute = "" Then
            ComposeDate = True
        ElseIf strTimeHour <> "" And strTimeMinute = "" Then
            pr_strErrMsg = pv_strLabelTimeMinute & " cannot be blank if " & pv_strLabelTimeHour & " is not blank"
        ElseIf strTimeHour = "" And strTimeMinute <> "" Then
            pr_strErrMsg = pv_strLabelTimeHour & " cannot be blank if " & pv_strLabelTimeMinute & " is not blank"
        ElseIf strDate <> "" And strTimeHour & strTimeMinute = "" Then
            pr_strErrMsg = pv_strLabelTimeHour & " and " & pv_strLabelTimeHour & " cannot be blank if " & pv_strLabelDate & " is not blank"
        ElseIf strDate = "" And strTimeHour & strTimeMinute <> "" Then
            pr_strErrMsg = pv_strLabelDate & " cannot be blank if " & pv_strLabelTimeHour & " and " & pv_strLabelTimeMinute & " is not blank"
        ElseIf IsNumeric(strTimeHour) = False Then
            pr_strErrMsg = pv_strLabelTimeHour & " must be an integer between 1 and 12"
        ElseIf IsNumeric(strTimeMinute) = False Then
            pr_strErrMsg = pv_strLabelTimeMinute & " must be an integer between 0 and 59"
        ElseIf CInt(strTimeHour) < 1 Or CInt(strTimeHour) > 12 Then
            pr_strErrMsg = pv_strLabelTimeHour & " must be an integer between 1 and 12"
        ElseIf CInt(strTimeMinute) < 0 Or CInt(strTimeMinute) > 59 Then
            pr_strErrMsg = pv_strLabelTimeMinute & " must be an integer between 0 and 59"
        Else
            strValidDate = GetValidDate(strDate, strErrMsg)
            If strValidDate = "" Then
                pr_strErrMsg = strErrMsg
            Else
                pr_strDate = strValidDate & " " & strTimeHour & ":" & Right("00" & strTimeMinute, 2) & ":00 " & strDayNight
                ComposeDate = True
            End If
        End If
    End Function

    Protected Sub DecomposeDate(ByVal pv_strDate As String, ByRef pr_txtDate As TextBox, ByRef pr_txtTimeHour As TextBox, ByRef pr_txtTimeMinute As TextBox, ByRef pr_ddlDayNight As DropDownList)
        pr_txtDate.Text = ""
        pr_txtTimeHour.Text = ""
        pr_txtTimeMinute.Text = ""
        pr_ddlDayNight.SelectedIndex = 0
        If IsDate(pv_strDate) = True Then
            If DateDiff(DateInterval.Day, CDate(pv_strDate), CDate("1 Jan 1900")) <> 0 Then
                pr_txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), pv_strDate)
                pr_txtTimeHour.Text = Right("00" & ((Hour(pv_strDate) + 11) Mod 12) + 1, 2)
                pr_txtTimeMinute.Text = Right("00" & Minute(pv_strDate), 2)
                If Hour(pv_strDate) > 11 Then
                    pr_ddlDayNight.SelectedIndex = 1
                End If
            End If
        End If
    End Sub

End Class
