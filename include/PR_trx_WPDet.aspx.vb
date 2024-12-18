Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Infragistics.WebUI.UltraWebTab

Public Class PR_trx_WPDet : Inherits Page

    Protected WithEvents txtWPTrxID As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtWPDate As TextBox
    Protected WithEvents ddlGang As DropDownList
    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents ddlPreBlock As DropDownList
    Protected WithEvents txtWorkProductivity As TextBox
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents ddlUOMCode As DropDownList

    Protected WithEvents lblBlock As Label
    Protected WithEvents lblPreBlock As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents WPTrxID As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents NewBtn As ImageButton

    Protected WithEvents lblErrDupWPCode As Label
    Protected WithEvents lblErrWPDate As Label
    Protected WithEvents lblErrWPDateFmt As Label
    Protected WithEvents lblErrWPDateFmtMsg As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblPreBlockErr As Label
    Protected WithEvents lblErrUOMCode As label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents lblErrWorkProductivity As Label
    Protected WithEvents lblErrExceeding As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblErrDupl As Label
    Protected WithEvents lblErrGangEmployee As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblToDateWP As Label
    Protected WithEvents lblTotalWP As Label

    Protected lblCloseExist As Label
    Protected lblTotArea As Label
    Protected WithEvents RefreshBtn As ImageButton
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList

    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlk As HtmlTableRow
    Protected WithEvents RowBlk As HtmlTableRow

    Protected WithEvents ddlItemCode As DropDownList
    Protected WithEvents txtQuantity As TextBox
    Protected WithEvents txtAddNote As TextBox
    Protected WithEvents dgLineItem As DataGrid
    Protected WithEvents ddlActItem As DropDownList
    Protected WithEvents ddlChargeLevelItem As DropDownList
    Protected WithEvents ddlBlockItem As DropDownList
    Protected WithEvents ddlSubBlockItem As DropDownList
    Protected WithEvents RowPreBlkItem As HtmlTableRow
    Protected WithEvents RowBlkItem As HtmlTableRow

    Protected WithEvents lblErrItemCode As Label
    Protected WithEvents lblErrQuantity As Label
    Protected WithEvents lblItemCode As Label
    Protected WithEvents lblActItem As Label
    Protected WithEvents lblBlockItem As Label
    Protected WithEvents lblSubBlockItem As Label
    Protected WithEvents lblErrBlockItem As Label
    Protected WithEvents lblErrSubBlockItem As Label
    Protected WithEvents lblErrDuplItem As Label

    Protected WithEvents DgLineAttendance As DataGrid
    Protected WithEvents ddlActAttd As DropDownList
    Protected WithEvents ddlChargeLevelAttd As DropDownList
    Protected WithEvents ddlBlockAttd As DropDownList
    Protected WithEvents ddlSubBlockAttd As DropDownList
    Protected WithEvents ddlEmpAttd As DropDownList
    Protected WithEvents RowPreBlkAttd As HtmlTableRow
    Protected WithEvents RowBlkAttd As HtmlTableRow
    Protected WithEvents txtPremi As TextBox
    Protected WithEvents txtOTHours As TextBox
    Protected WithEvents ddlVehicle As DropDownList
    Protected WithEvents ddlVehExpense As DropDownList

    Protected WithEvents lblActAttd As Label
    Protected WithEvents lblBlockAttd As Label
    Protected WithEvents lblSubBlockAttd As Label
    Protected WithEvents lblErrBlockAttd As Label
    Protected WithEvents lblErrSubBlockAttd As Label
    Protected WithEvents lblPremi As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblVehExpense As Label
    Protected WithEvents lblErrVehExpense As Label
    Protected WithEvents lblErrEmpAttd As Label
    Protected WithEvents UltraWebTab1 As UltraWebTab

    Protected WithEvents btnAddAttd As ImageButton

    Protected objPRTrx As New agri.PR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objAdmin As New agri.Admin.clsUom()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim objAccDs As New Object()
    Dim objEmpDs As New Object()
    Dim objGangDs As New Object()
    Dim objUOMDs As New Object()
    Dim objLangCapDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strDateFmt As String
    Dim strSelectedWPId As String = ""
    Dim intStatus As Integer
    Dim strAcceptFormat As String
    Dim strUOMSubActCode As String
    Dim strUOMBlock As String
    Dim pv_strSubActCode As String
    Dim pv_strActCode As String
    Dim pv_strFunctioCode As String
    Dim strSelectedDate As String
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim PreBlockTag As String
    Dim BlockTag As String
    Dim strSelectedEmpCode As String = ""
    Dim strGangCode As String = ""
    Dim blnUseBlk As Boolean = False
    'Dim blnUpdate As Boolean = False


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strDateFmt = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrGangEmployee.Visible = False
            lblPreBlockErr.Visible = False
            lblErrBlock.Visible = False
            lblErrAccCode.Visible = False
            lblErrUOMCode.Visible = False
            lblErrDupl.Visible = False
            lblErrExceeding.Visible = False
            lblErrDupWPCode.Visible = False
            lblErrWPDate.Visible = False
            lblErrWPDateFmt.Visible = False
            lblErrWPDateFmtMsg.Visible = False

            strSelectedWPId = Trim(IIf(Request.QueryString("WPTrxID") <> "", Request.QueryString("WPTrxID"), Request.Form("WPTrxID")))
            intStatus = Convert.ToInt32(lblHiddenSts.Text)

            If Not IsPostBack Then
                BindChargeLevelDropDownList()
                If strSelectedWPId <> "" Then
                    WPTrxID.Value = strSelectedWPId
                    BindGang("")
                    BindEmployee("", "")
                    BindAccCode("")
                    BindPreBlock("", "")
                    BindBlock("", "")
                    BindUOMCode("")
                    BindItemCode("")
                    BindEmpAttd()
                    onLoad_Display()
                    onLoad_DisplayLine()
                    onLoad_BindButton()
                    onLoad_DisplayLineItem(strSelectedWPId)
                    onLoad_DisplayLineAttendance(strSelectedWPId)
                Else
                    txtWPDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                    BindGang("")
                    BindEmployee("", "")
                    BindAccCode("")
                    BindPreBlock("", "")
                    BindBlock("", "")
                    BindUOMCode("")
                    BindItemCode("")
                    BindActItemAttd()
                    BindEmpAttd()
                    onLoad_BindButton()
                End If
            End If

        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblPreBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & "/" & GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfig) = True Then
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                lblSubBlockItem.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                lblSubBlockAttd.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                lblSubBlockItem.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                lblSubBlockAttd.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_TRX_CJDET_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/AP_trx_CJDet.aspx")
        End Try


        dgLineDet.Columns(1).HeaderText = lblPreBlock.Text
        dgLineItem.Columns(1).HeaderText = lblPreBlock.Text
        DgLineAttendance.Columns(1).HeaderText = lblPreBlock.Text

        lblErrAccCode.Text = "<br>" & lblPleaseSelect.Text & " Activity Code"
        lblErrBlock.Text = lblPleaseSelect.Text & lblBlock.Text
        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlock.Text = PreBlockTag & lblCode.Text
        lblPreBlockErr.Text = lblPleaseSelect.Text & PreBlockTag & lblCode.Text
        lblBlockItem.Text = PreBlockTag & lblCode.Text
        lblErrBlockItem.Text = lblPleaseSelect.Text & PreBlockTag & lblCode.Text
        lblBlockAttd.Text = PreBlockTag & lblCode.Text
        lblErrBlockAttd.Text = lblPleaseSelect.Text & PreBlockTag & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
        lblErrVehicle.Text = lblErrSelect.Text & lblVehicle.Text
        lblErrVehExpense.Text = lblErrSelect.Text & lblVehExpense.Text
        lblItemCode.Text = GetCaption(objLangCap.EnumLangCap.StockItem)

        'If ddlChargeLevel.SelectedIndex = 0 Then
        '    lblBlockItem.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        '    lblBlockAttd.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        'Else
        '    lblBlockItem.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        '    lblBlockAttd.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        'End If
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_DAILYATTD_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_DailyAttd.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub onLoad_BindButton()
        RefreshBtn.Visible = False
        txtWPTrxID.Enabled = False
        txtDesc.Enabled = False
        txtWPDate.Enabled = False
        ddlGang.Enabled = False
        ddlEmployee.Enabled = False
        ddlAccCode.Enabled = False
        txtWorkProductivity.Enabled = False
        SaveBtn.Visible = False
        tblSelection.Visible = False
        ddlUOMCode.Enabled = False
        lblToDateWP.Text = 0
        txtWorkProductivity.Text = 0
        RowPreBlk.Visible = False
        RowChargeLevel.Visible = False
        RowPreBlkItem.Visible = False
        RowPreBlkAttd.Visible = False

        Select Case intStatus
            Case objPRTrx.EnumWPTrxStatus.Active
                RefreshBtn.Visible = True
                ddlAccCode.Enabled = True
                ddlUOMCode.Enabled = True
                txtWorkProductivity.Enabled = True
                SaveBtn.Visible = True
                tblSelection.Visible = True
                txtWPTrxID.Enabled = False
                txtDesc.Enabled = False
                txtWPDate.Enabled = False
                ddlGang.Enabled = False
                ddlEmployee.Enabled = False
                RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
                ToggleChargeLevel()

            Case objPRTrx.EnumWPTrxStatus.Closed, objPRTrx.EnumWPTrxStatus.Deleted

            Case Else
                RefreshBtn.Visible = True
                txtWPTrxID.Enabled = True
                txtDesc.Enabled = True
                txtWPDate.Enabled = True
                ddlGang.Enabled = True
                ddlEmployee.Enabled = True
                ddlAccCode.Enabled = True
                ddlUOMCode.Enabled = True
                txtWorkProductivity.Enabled = True
                SaveBtn.Visible = True
                tblSelection.Visible = True
                RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
                ToggleChargeLevel()
        End Select
    End Sub

    Sub onClick_Refresh(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        strSelectedDate = txtWPDate.Text
        BindGang("")
    End Sub

    Sub onLoad_Display()
        Dim objWPTrxDs As New DataSet
        Dim strOpCd As String = "PR_CLSTRX_WPTRX_GET"
        Dim strParam As String = strSelectedWPId
        Dim intErrNo As Integer

        Try
            intErrNo = objPRTrx.mtdGetWPTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objWPTrxDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        WPTrxID.Value = strSelectedWPId
        txtWPTrxID.Text = strSelectedWPId
        txtDesc.Text = objWPTrxDs.Tables(0).Rows(0).Item("Description").Trim()
        txtWPDate.Text = Date_Validation(objWPTrxDs.Tables(0).Rows(0).Item("WPDate"), True)
        intStatus = Convert.ToInt32(objWPTrxDs.Tables(0).Rows(0).Item("Status"))
        lblHiddenSts.Text = objWPTrxDs.Tables(0).Rows(0).Item("Status").Trim()
        lblStatus.Text = objPRTrx.mtdGetWPTrxStatus(Convert.ToInt16(objWPTrxDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objWPTrxDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objWPTrxDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = objWPTrxDs.Tables(0).Rows(0).Item("UserName")
        'BindGang("")
        BindGang(objWPTrxDs.Tables(0).Rows(0).Item("GangCode").Trim())
        If objWPTrxDs.Tables(0).Rows(0).Item("GangCode").Trim() <> "" Then
            BindEmployee(objWPTrxDs.Tables(0).Rows(0).Item("GangCode").Trim(), objWPTrxDs.Tables(0).Rows(0).Item("EmpCode").Trim())
        Else
            BindEmployee("", objWPTrxDs.Tables(0).Rows(0).Item("EmpCode").Trim())
        End If
        BindAccCode("")
        objWPTrxDs = Nothing
    End Sub

    Sub onLoad_DisplayLine()
        Dim objWPTrxLnDs As New DataSet()
        Dim strOpCd As String = "PR_CLSTRX_WPTRX_LINE_GET"
        Dim strParam As String = strSelectedWPId
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton1 As LinkButton
        Dim lbButton2 As LinkButton
        Dim dblWorkProductivity As Integer = 0
        Dim strAccCode As String
        Dim strBlkCode As String
        Dim intSelectedIndex As Integer = 0


        Try
            intErrNo = objPRTrx.mtdGetWPTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objWPTrxLnDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRXLINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objWPTrxLnDs.Tables(0).Rows.Count - 1
            dblWorkProductivity += objWPTrxLnDs.Tables(0).Rows(intCnt).Item("WorkProductivity")
        Next

        dgLineDet.DataSource = objWPTrxLnDs.Tables(0)
        dgLineDet.DataBind()

        'lblTotalWP.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(dblWorkProductivity, 0)

        If intStatus = objPRTrx.EnumWPTrxStatus.Active Then
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton1 = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lbButton1.Visible = True
                lbButton1.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                lbButton2 = dgLineDet.Items.Item(intCnt).FindControl("lbAddItem")
                lbButton2.Visible = False
                strAccCode = Trim(objWPTrxLnDs.Tables(0).Rows(intCnt).Item("AccCode"))
                strBlkCode = Trim(objWPTrxLnDs.Tables(0).Rows(intCnt).Item("BlkCode"))
                lbButton2.Attributes("onclick") = "javascript:PopUpAddItem('" + txtWPTrxID.Text + "','" + strAccCode + "','" + strBlkCode + "');"
            Next
        Else
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton1 = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lbButton1.Visible = False
                lbButton2 = dgLineDet.Items.Item(intCnt).FindControl("lbAddItem")
                lbButton2.Visible = False
            Next
        End If

        For intCnt = 0 To objWPTrxLnDs.Tables(0).Rows.Count - 1
            objWPTrxLnDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objWPTrxLnDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objWPTrxLnDs.Tables(0).Rows(intCnt).Item("Description") = objWPTrxLnDs.Tables(0).Rows(intCnt).Item("AccCode") & " (" & Trim(objWPTrxLnDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next intCnt

        BindActItemAttd()
    End Sub

    Sub BindGang(ByVal pv_strGangCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_GANG_SEARCH"
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        strParam = strLocation & "||||" & objHRSetup.EnumGangStatus.Active & "||GangCode|"
        Try
            intErrNo = objHRSetup.mtdGetGang(strOpCd, strParam, objGangDs, False)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If pv_strGangCode <> "" Then
            For intCnt = 0 To objGangDs.Tables(0).Rows.Count - 1
                If Trim(objGangDs.Tables(0).Rows(intCnt).Item("GangCode")) = Trim(pv_strGangCode) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objGangDs.Tables(0).NewRow()
        dr("GangCode") = ""
        dr("_Description") = "Select Gang Code"
        objGangDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlGang.DataSource = objGangDs.Tables(0)
        ddlGang.DataValueField = "GangCode"
        ddlGang.DataTextField = "_Description"
        ddlGang.DataBind()
        ddlGang.SelectedIndex = intSelectedIndex
        ddlGang.AutoPostBack = True
    End Sub

    Sub onSelect_Gang(ByVal Sender As Object, ByVal E As EventArgs)
        strSelectedDate = txtWPDate.Text
        strGangCode = ddlGang.SelectedItem.Value
        strSelectedEmpCode = ddlEmployee.SelectedItem.Value

        BindEmployee(strGangCode, "")
    End Sub

    Sub BindBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim objBlkDs As DataSet
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_TRX_CREDITJRNDET_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        dr("Description") = lblPleaseSelect.Text & lblBlock.Text
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectedIndex
    End Sub

    Sub onSelect_Block(ByVal Sender As Object, ByVal E As EventArgs)
        BlockChange()
    End Sub

    Sub BlockChange()
        If ddlChargeLevel.SelectedIndex = 0 Then
            If Not (ddlAccCode.SelectedItem.Value = "" And ddlpreblock.SelectedItem.Value = "") Then
                GetTotWorkProductivity(ddlAccCode.SelectedItem.Value, ddlPreBlock.SelectedItem.Value)
            End If
        Else
            If Not (ddlAccCode.SelectedItem.Value = "" And ddlBlock.SelectedItem.Value = "") Then
                GetTotWorkProductivity(ddlAccCode.SelectedItem.Value, ddlBlock.SelectedItem.Value)
            End If
        End If
    End Sub

    Sub GetUOMBlock(ByVal pv_strBlockCode As String)
        Dim objUOMDs As New Object()
        Dim strOpCd As String = "PR_CLSTRX_WPTRX_GET_BLOCKLIST"
        Dim dr As DataRow
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strWPDate As String = txtWPDate.Text
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim arrParam As Array

        If Not (ddlAccCode.SelectedItem.Value = "") Then
            arrParam = Split(ddlAccCode.SelectedItem.Value, "-")
            pv_strSubActCode = arrParam(0)
            pv_strActCode = arrParam(1)
        Else
            Exit Sub
        End If

        If objGlobal.mtdValidInputDate(strDateFmt, strWPDate, objFormatDate, objActualDate) = False Then
            lblErrWPDateFmt.Visible = True
            lblErrWPDateFmt.Text = lblErrWPDateFmtMsg.Text & objFormatDate
            Exit Sub
        Else
            strWPDate = Date_Validation(strWPDate, False)
        End If


        strParam = objHRSetup.EnumGangStatus.Active & "|" & _
                   objPRTrx.EnumAttdTrxStatus.Active & "','" & objPRTrx.EnumAttdTrxStatus.Confirmed & "','" & objPRTrx.EnumAttdTrxStatus.Closed & "|" & _
                   strWPDate & "|" & _
                   strLocation & "|" & _
                   Trim(ddlGang.SelectedItem.Value) & "|" & _
                   pv_strSubActCode & "|" & _
                   pv_strActCode

        Try
            intErrNo = objPRTrx.mtdGetWPBindBlock(strOpCd, strParam, objUOMDs, False)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_SubAct_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objUOMDs.Tables(0).Rows.Count <= 0 Then
            Exit Sub
        Else
            'strUOMBlock = objUOMDs.Tables(0).Rows(0).Item("AreaUOM").Trim()
            'lblUOM.Text = strUOMBlock
            lblTotArea.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(objUOMDs.Tables(0).Rows(0).Item("TotalArea"), 0)
        End If
    End Sub

    Function GetTotWorkProductivity(ByVal pv_strAccCode As String, ByVal pv_strBlockCode As String) As Integer
        Dim objTotWP As New Object()
        Dim objWPTrxDs As New DataSet()
        Dim strOpCd As String = "PR_CLSTRX_WPTRX_GET_TOTWP"
        Dim strOpCode_UpdLine As String = "PR_CLSTRX_WPTRX_LINE_UPD"
        Dim dr As DataRow
        Dim strParam As String = "|" & "where AccCode = '" & Trim(ddlAccCode.SelectedItem.Value) & "' And BlkCode = '" & Trim(pv_strBlockCode) & "' And wpln.Status = '" & objPRTrx.EnumWPTrxLnStatus.Active & "' And wp.Status = '" & objPRTrx.EnumWPTrxStatus.Active & "' "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strAccCode As String = Trim(ddlAccCode.SelectedItem.Value)
        Dim strBlock As String = Trim(ddlBlock.SelectedItem.Value)
        Dim strPreBlock As String = Trim(ddlPreBlock.SelectedItem.Value)
        Dim intSelectedIndex As Integer = 0
        Dim strToDateWP As Integer = 0


        strParam = "|" & "where AccCode = '" & Trim(ddlAccCode.SelectedItem.Value) & "' And BlkCode = '" & iif(ddlChargeLevel.SelectedIndex = 0, strPreBlock, strBlock) & "' And wpln.Status = '" & objPRTrx.EnumWPTrxLnStatus.Active & "' And wp.Status = '" & objPRTrx.EnumWPTrxStatus.Active & "' "
        Try
            intErrNo = objPRTrx.mtdGetWPTotWorkProductivity(strOpCd, strParam, objTotWP)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_TotWP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objTotWP.Tables(0).Rows.Count <= 0 Then
            strToDateWP = 0
            lblToDateWP.Text = strToDateWP            
        Else
            strToDateWP = objTotWP.Tables(0).Rows(0).Item("TotWP")
            lblToDateWP.Text = strToDateWP

            'later on if budget ready
            'If strToDateWP < lblTotArea.Text Then
            '    lblToDateWP.Text = strToDateWP
            'Else
            '    strParam = strSelectedWPId & "|" & _
            '                strAccCode & "|" & _
            '                strBlock & "|||" & _
            '                Convert.ToString(objPRTrx.EnumWPTrxLnStatus.Closed)
            '    Try
            '        intErrNo = objPRTrx.mtdUpdWPTrxLine(" ", _
            '                                                strOpCode_UpdLine, _
            '                                                strCompany, _
            '                                                strLocation, _
            '                                                strUserId, _
            '                                                strParam, _
            '                                                True, _
            '                                                objWPTrxDs)
            '    Catch Exp As Exception
            '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_LINE_UPD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPId)
            '    End Try
            '    lblToDateWP.Text = 0
            'End If
        End If
    End Function

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_WP_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_GRList.aspx")
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

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objWPTrxDs As New DataSet()
        Dim objFound As Boolean
        Dim blnIsUpdated As Boolean
        Dim strOpCode_GetLine As String = "PR_CLSTRX_WPTRX_LINEDUPL_GET"
        Dim strOpCode_AddLine As String = "PR_CLSTRX_WPTRX_LINE_ADD"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_WPTRX_STATUS_UPD"
        Dim strOpCode_AddLineByBlock As String = "PR_CLSTRX_WPTRX_LINEBYBLOCK_ADD"
        Dim strOpCode As String
        Dim strAccCode As String = Trim(ddlAccCode.SelectedItem.Value)
        Dim strBlock As String = Trim(ddlBlock.SelectedItem.Value)
        Dim strPreBlock As String = Trim(ddlPreBlock.SelectedItem.Value)
        Dim strUOMCode As String = Trim(ddlUOMCode.SelectedItem.Value)
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intTotWP As Integer = 0


        If strAccCode = "" Then
            lblErrAccCode.Visible = True
            Exit Sub
        ElseIf strUOMCode = "" Then
            lblErrUOMCode.visible = True
            Exit Sub
        End If

        If ddlChargeLevel.SelectedIndex = 0 Then
            If strPreBlock = "" Then
                lblPreBlockErr.Visible = True
                Exit Sub
            End If
        Else
            If strBlock = "" Then
                lblErrBlock.Visible = True
                Exit Sub
            End If
        End If


        strSelectedWPId = Trim(txtWPTrxID.Text)
        InsertRecord(blnIsUpdated)
        If strSelectedWPId = "" Then
            Exit Sub
        End If

        If txtWorkProductivity.Text > 0 Then
            If ddlChargeLevel.SelectedIndex = 0 And RowPreBlk.Visible = True Then
                strParam = "|" & "WPTrxID = '" & strSelectedWPId & "' And AccCode = '" & Trim(strAccCode) & "' And BlkCode = '" & Trim(strPreBlock) & "' "
            Else
                strParam = "|" & "WPTrxID = '" & strSelectedWPId & "' And AccCode = '" & Trim(strAccCode) & "' And SubBlkCode = '" & Trim(strBlock) & "' "
            End If

            Try
                intErrNo = objPRTrx.mtdGetWPTrxLn(strOpCode_GetLine, _
                                                strParam, _
                                                objWPTrxDs, _
                                                False)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_WPTRXLN_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objWPTrxDs.Tables(0).Rows.Count > 0 Then
                lblErrDupl.Visible = True
                Exit Sub
            Else
                If ddlChargeLevel.SelectedIndex = 0 And RowPreBlk.Visible = True Then
                    strOpCode = strOpCode_AddLineByBlock
                    strParam = strSelectedWPId & "|" & _
                                strAccCode & "|" & _
                                strPreBlock & "|" & _
                                strBlock & "|" & _
                                strUOMCode & "|" & _
                                Trim(txtWorkProductivity.Text) & "|" & _
                                Convert.ToString(objPRTrx.EnumWPTrxLnStatus.Active)
                Else
                    strOpCode = strOpCode_AddLine
                    strParam = strSelectedWPId & "|" & _
                                strAccCode & "|" & _
                                "" & "|" & _
                                strBlock & "|" & _
                                strUOMCode & "|" & _
                                Trim(txtWorkProductivity.Text) & "|" & _
                                Convert.ToString(objPRTrx.EnumWPTrxLnStatus.Active)
                End If

                Try
                    intErrNo = objPRTrx.mtdUpdWPTrxLine(strOpCode_UpdID, _
                                                            strOpCode, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            False, _
                                                            objWPTrxDs)

                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPId)
                End Try


                Dim objFormatDate As String
                Dim objActualDateFrom As String
                Dim strParamName As String = ""
                Dim strParamValue As String = ""
                strOpCode = "PR_CLSTRX_GENERATE_ATTENDANCE_FROM_WPTRX"

                If objGlobal.mtdValidInputDate(strDateFmt, _
                                               txtWPDate.Text, _
                                               objFormatDate, _
                                               objActualDateFrom) = False Then
                    lblErrWPDateFmt.Visible = True
                    lblErrWPDateFmt.Text = lblErrWPDateFmtMsg.Text & objFormatDate

                End If


                strParamName = "LOCCODE|STARTDATE|ENDDATE|GANGCODE|EMPLCODE|WPTRXID"
                strParamValue = strLocation & "|" & _
                                objActualDateFrom & "|" & _
                                objActualDateFrom & "|" & _
                                Trim(ddlGang.SelectedItem.Value) & "|" & _
                                Trim(ddlEmployee.SelectedItem.Value) & "|" & _
                                Trim(txtWPTrxID.Text)


                Try
                    intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                           strParamName, _
                                                           strParamValue)

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_Trx_GENERATEATTENDANCE&errmesg=&redirect=")
                End Try
            End If

            If strSelectedWPId <> "" Then
                onLoad_Display()
                onLoad_DisplayLine()
                onLoad_BindButton()
                BindAccCode("")
                BindBlock("", "")
                BindPreBlock("", "")
                onLoad_DisplayLineAttendance(strSelectedWPId)
            End If
        End If

            objWPTrxDs = Nothing
    End Sub

    Sub InsertRecord(ByRef pr_blnIsUpdated As Boolean)
        Dim objWPTrxDs As New DataSet()
        Dim objWPTrxID As String
        Dim strOpCd_Add As String = "PR_CLSTRX_WPTRX_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_WPTRX_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_WPTRX_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_WPTRX_STATUS_UPD"
        Dim objWPCodeDs As New DataSet()
        Dim strOpCd As String
        Dim strWPDate As String = txtWPDate.Text
        Dim intErrNo As Integer
        Dim strParam As String = strSelectedWPId
        Dim objFormatDate As String
        Dim objActualDate As String

        strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)

        If strSelectedWPId = "" Then
            Exit Sub
        End If

        If Trim(txtWPDate.Text) = "" Then
            lblErrWPDate.Visible = True
            Exit Sub
        ElseIf objGlobal.mtdValidInputDate(strDateFmt, _
                                           strWPDate, _
                                           objFormatDate, _
                                           objActualDate) = False Then
            lblErrWPDateFmt.Visible = True
            lblErrWPDateFmt.Text = lblErrWPDateFmtMsg.Text & objFormatDate
            Exit Sub
        Else
            strWPDate = Date_Validation(strWPDate, False)
        End If

        Try
            intErrNo = objPRTrx.mtdGetWPTrx(strOpCd_Get, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objWPTrxDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_WPTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objWPTrxDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
            lblErrDupWPCode.Visible = True
            strSelectedWPId = ""
            Exit Sub
        Else
            strSelectedWPId = Trim(txtWPTrxID.Text)
            strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
            strParam = strSelectedWPId & "|" & _
                    Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _
                    strWPDate & "|" & _
                    Trim(ddlGang.SelectedItem.Value) & "|" & _
                    Trim(ddlEmployee.SelectedItem.Value) & "|" & _
                    objPRTrx.EnumWPTrxStatus.Active

            Try
                intErrNo = objPRTrx.mtdUpdWPTrx(strOpCd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                False, _
                                                objWPTrxID)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_WPTRX_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx")
            End Try
        End If

        objWPTrxDs = Nothing
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSTRX_WPTRX_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_WPTRX_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_WPTRX_GET"
        Dim strOpCd_Sts As String = "PR_CLSTRX_WPTRX_STATUS_UPD"
        Dim blnIsUpdated As Boolean
        Dim strGang As String = Trim(ddlGang.SelectedItem.Value)
        Dim strEmployee As String = Trim(ddlEmployee.SelectedItem.Value)
        Dim objWPTrxId As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strParam As String = ""

        strGang = IIf(strGang = "", ddlGang.SelectedItem.Value, strGang)

        If strCmdArgs = "New" Then
            Response.Redirect("PR_trx_WPDet.aspx?" & _
                            "WPTrxID=" & strSelectedWPId)
        End If

        If strGang = "" And strEmployee = "" Then
            lblErrGangEmployee.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            InsertRecord(blnIsUpdated)
        ElseIf strCmdArgs = "Back" Then
            strParam = strSelectedWPId & "|" & objPRTrx.EnumWPTrxStatus.Closed
            Try
                intErrNo = objPRTrx.mtdUpdWPTrx(strOpCd_Sts, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True, _
                                                objWPTrxId)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_WPTRX_CLOSE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPId)
            End Try
        End If

        If strSelectedWPId <> "" Then
            onLoad_Display()
            onLoad_DisplayLine()
            onLoad_BindButton()
        End If
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "PR_CLSTRX_WPTRX_LINE_DEL"
        Dim strOpCode_DelLineItem As String = "PR_CLSTRX_WPTRX_LINE_ITEM_DEL_ALL"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_WPTRX_STATUS_UPD"
        Dim strParam As String
        Dim lblAccCode As Label
        Dim lblBlk As Label
        Dim strAccCode As String
        Dim strBlock As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = Convert.ToInt32(E.Item.ItemIndex)
        lblAccCode = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("lblAccCode")
        strAccCode = lblAccCode.Text

        lblBlk = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("lblSubBlkCode")
        strBlock = lblBlk.Text

        Try
            strParam = strSelectedWPId & "|" & _
                       strAccCode & "|" & _
                       "" & "|" & _
                       strBlock & "|||" & objPRTrx.EnumWPTrxStatus.Active

            intErrNo = objPRTrx.mtdUpdWPTrxLine(strOpCode_UpdID, _
                                                strOpCode_DelLine, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                False, _
                                                objResult)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_LINE_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPId)
        End Try

        Try
            strParam = strSelectedWPId & "|" & _
                       strAccCode & "|" & _
                       "" & "|" & _
                       strBlock & "|" & _
                       "|||" & _
                       objPRTrx.EnumWPTrxStatus.Active

            intErrNo = objPRTrx.mtdUpdWPTrxLineItem(strOpCode_UpdID, _
                                                strOpCode_DelLineItem, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                False, _
                                                objResult)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_LINE_ITEM_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPId)
        End Try

        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "PR_CLSTRX_WPTRX_LINEATTD_DEL"

        strParamName = "LOCCODE|GANGCODE|EMPLCODE|WPTRXID|ACCCODE|SUBBLKCODE"
        strParamValue = strLocation & "|" & _
                        Trim(ddlGang.SelectedItem.Value) & "|" & _
                        Trim(ddlEmployee.SelectedItem.Value) & "|" & _
                        Trim(txtWPTrxID.Text) & "|" & _
                        strAccCode & "|" & _
                        Trim(strBlock)


        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                   strParamName, _
                                                   strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_Trx_GENERATEATTENDANCE&errmesg=&redirect=")
        End Try

        onLoad_Display()
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_WPList.aspx")
    End Sub

    Sub onSelect_ChangeLevel(ByVal Sender As Object, ByVal E As EventArgs)
        ToggleChargeLevel()
    End Sub

    Sub ToggleChargeLevel()
        If UltraWebTab1.SelectedTabIndex = 0 Then
            If ddlChargeLevel.SelectedIndex = 0 Then
                RowBlk.Visible = False
                RowPreBlk.Visible = True
            Else
                RowBlk.Visible = True
                RowPreBlk.Visible = False
            End If
        ElseIf UltraWebTab1.SelectedTabIndex = 1 Then
            If ddlChargeLevelItem.SelectedIndex = 0 Then
                RowBlkItem.Visible = False
                RowPreBlkItem.Visible = True
            Else
                RowBlkItem.Visible = True
                RowPreBlkItem.Visible = False
            End If
            BindBlkItemAttd(ddlActItem.SelectedItem.Value)
        ElseIf UltraWebTab1.SelectedTabIndex = 2 Then
            If ddlChargeLevelAttd.SelectedIndex = 0 Then
                RowBlkAttd.Visible = False
                RowPreBlkAttd.Visible = True
            Else
                RowBlkAttd.Visible = True
                RowPreBlkAttd.Visible = False
            End If
            BindBlkItemAttd(ddlActAttd.SelectedItem.Value)
        End If
    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.SelectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")

        ddlChargeLevelItem.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevelItem.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevelItem.SelectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")

        ddlChargeLevelAttd.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevelAttd.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevelAttd.SelectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        ToggleChargeLevel()
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

        strOpCd = "GL_CLSSETUP_ACCOUNT_PREBLOCK_GET"
        intSelectedIndex = 0
        Try
            strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
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

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblPleaseSelect.Text & PreBlockTag & lblCode.Text

        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlPreBlock.DataSource = objBlkDs.Tables(0)
        ddlPreBlock.DataValueField = "BlkCode"
        ddlPreBlock.DataTextField = "Description"
        ddlPreBlock.DataBind()
        ddlPreBlock.SelectedIndex = intSelectedIndex

        If Not objBlkDs Is Nothing Then
            objBlkDs = Nothing
        End If
    End Sub

    Sub BindAccCode(ByVal pv_strAccCode As String)
        Dim strOpCode_GetAcc As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            strParam = "Order By ACC.AccCode|And ACC.Status = '" & _
                        objGLSetup.EnumAccountCodeStatus.Active & _
                        "' And ACC.AccType in ('" & objGLSetup.EnumAccountType.BalanceSheet & "','" & objGLSetup.EnumAccountType.ProfitAndLost & "')"
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode_GetAcc, strParam, objGLSetup.EnumGLMasterType.AccountCode, objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CREDITNOTE_GET_SUPP2&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_cnlist.aspx")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = objAccDs.Tables(0).Rows(intCnt).Item("AccCode") & " (" & Trim(objAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt


        Dim dr As DataRow
        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = lblPleaseSelect.Text & " Activity Code"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccCode.DataSource = objAccDs.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                      ByRef pr_IsBalanceSheet As Boolean, _
                      ByRef pr_IsNurseryInd As Boolean, _
                      ByRef pr_IsBlockRequire As Boolean, _
                      ByRef pr_IsVehicleRequire As Boolean, _
                      ByRef pr_IsOthers As Boolean)

        Dim _objAccDs As New Object
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_TRX_CREDITJRNDET_GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")

        GetAccountDetails(ddlAccCode.SelectedItem.Value, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

        If Not blnIsBalanceSheet Then
            If blnIsBlockRequire Then
                BindPreBlock(ddlAccCode.SelectedItem.Value, Request.Form("ddlPreBlock"))
                BindBlock(ddlAccCode.SelectedItem.Value, Request.Form("ddlBlock"))
            Else
                BindPreBlock("", Request.Form("ddlPreBlock"))
                BindBlock("", Request.Form("ddlBlock"))
            End If

        ElseIf blnIsNurseryInd = True Then
            BindPreBlock(ddlAccCode.SelectedItem.Value, Request.Form("ddlPreBlock"))
            BindBlock(ddlAccCode.SelectedItem.Value, Request.Form("ddlBlock"))
        Else
            BindPreBlock("", Request.Form("ddlPreBlock"))
            BindBlock("", Request.Form("ddlBlock"))
        End If
    End Sub

    Sub BindEmployee(ByVal pv_strGangCode As String, ByVal pv_strEmpCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_GANG_LINE_GET"
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objDateFormat As String
        Dim objValidDate As String
        Dim intSelectedIndex As Integer = 0
        Dim arrEmp As Array
        Dim strOpCdEmployee_Get As String = "HR_CLSSETUP_EMPLOYEE_LIST_GET"

        If lblHiddenSts.Text = objPRTrx.EnumAttdTrxStatus.Confirmed Then
            arrEmp = Split(pv_strEmpCode, "|")
            ddlEmployee.Items.Clear()
            ddlEmployee.Items.Add(New ListItem(arrEmp(0), arrEmp(1)))
        Else
            strGangCode = Trim(pv_strGangCode)
            If Trim(strGangCode) <> "" Then
                If objGlobal.mtdValidInputDate(strDateFmt, _
                                            txtWPDate.Text, _
                                            objDateFormat, _
                                            objValidDate) = True Then
                    Try
                        strParam = strLocation & "|" & strGangCode
                        intErrNo = objHRSetup.mtdGetGang(strOpCd, _
                                                        strParam, _
                                                        objEmpDs, _
                                                        True)
                    Catch Exp As System.Exception
                        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
                    End Try

                    If Trim(pv_strEmpCode) <> "" Then
                        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
                            If Trim(objEmpDs.Tables(0).Rows(intCnt).Item("GangMember")) = Trim(pv_strEmpCode) Then
                                intSelectedIndex = intCnt + 1
                                Exit For
                            End If
                        Next

                    Else
                        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
                            objEmpDs.Tables(0).Rows(intCnt).Item("GangMember") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("GangMember"))
                            objEmpDs.Tables(0).Rows(intCnt).Item("GangMemberName") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("GangLeaderName")) & " - " & _
                                                                                      Trim(objEmpDs.Tables(0).Rows(intCnt).Item("GangMemberName"))
                        Next intCnt
                    End If

                    dr = objEmpDs.Tables(0).NewRow()
                    dr("GangMember") = ""
                    dr("GangMemberName") = "Select Employee Code"
                    objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

                    ddlEmployee.DataSource = objEmpDs.Tables(0)
                    ddlEmployee.DataValueField = "GangMember"
                    ddlEmployee.DataTextField = "GangMemberName"
                    ddlEmployee.DataBind()
                    ddlEmployee.SelectedIndex = intSelectedIndex
                Else
                    ddlEmployee.Items.Clear()
                    ddlEmployee.Items.Add(New ListItem("Select Employee Code", ""))
                End If
            Else
                Try
                    strParam = "|||" & objHRTrx.EnumEmpStatus.Active & "|" & strLocation & "|Mst.EmpCode|ASC"

                    intErrNo = objHRTrx.mtdGetEmployeeList(strOpCdEmployee_Get, strParam, objEmpDs)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=mtdGetEmployeeList&errmesg=" & Exp.Message & "&redirect=PR/Trx/PR_Trx_WPDet.aspx")
                End Try

                For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
                    objEmpDs.Tables(0).Rows(intCnt).Item(0) = Trim(objEmpDs.Tables(0).Rows(intCnt).Item(0))
                    objEmpDs.Tables(0).Rows(intCnt).Item(1) = Trim(objEmpDs.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                                Trim(objEmpDs.Tables(0).Rows(intCnt).Item(1)) & " )"
                    If objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(pv_strEmpCode) Then
                        intSelectedIndex = intCnt + 1
                    End If
                Next intCnt
                dr = objEmpDs.Tables(0).NewRow()
                dr(0) = ""
                dr(1) = "Select employee"
                objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

                ddlEmployee.DataSource = objEmpDs.Tables(0)
                ddlEmployee.DataValueField = "EmpCode"
                ddlEmployee.DataTextField = "EmpName"
                ddlEmployee.DataBind()
                ddlEmployee.SelectedIndex = intSelectedIndex
            End If
        End If
    End Sub

    Sub BindUOMCode(ByVal pv_strUOMCode As String)
        Dim strOpCode_GetUOM As String = "ADMIN_CLSUOM_UOM_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            strParam = "Order By UOM.UOMCode|And UOM.Status = '" & _
                        objAdmin.EnumUOMStatus.Active & "' "

            intErrNo = objGLSetup.mtdGetMasterList(strOpCode_GetUOM, strParam, objGLSetup.EnumGLMasterType.AccountCode, objUOMDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CREDITNOTE_GET_SUPP2&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_cnlist.aspx")
        End Try

        For intCnt = 0 To objUOMDs.Tables(0).Rows.Count - 1
            objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode"))
            objUOMDs.Tables(0).Rows(intCnt).Item("UOMDesc") = objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") & " (" & Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMDesc")) & ")"
            If objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(pv_strUOMCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt


        Dim dr As DataRow
        dr = objUOMDs.Tables(0).NewRow()
        dr("UOMCode") = ""
        dr("UOMDesc") = lblPleaseSelect.Text & " UOM Code"
        objUOMDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlUOMCode.DataSource = objUOMDs.Tables(0)
        ddlUOMCode.DataValueField = "UOMCode"
        ddlUOMCode.DataTextField = "UOMDesc"
        ddlUOMCode.DataBind()
        ddlUOMCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindItemCode(ByVal pv_ItemCode As String)
        Dim strOpCdItemCode_Get As String = "IN_CLSSETUP_INVMASTER_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim SearchStr As String
        Dim strParam As String
        Dim intErrNo As Integer

        SearchStr = "AND ItemType in ('" & objINSetup.EnumInventoryItemType.Stock & "') AND itm.Status = '" & objINSetup.EnumStockItemStatus.Active & "' "

        strParam = "ORDER BY ItemCode asc|" & SearchStr

        Try
            intErrNo = objINSetup.mtdGetMasterList(strOpCdItemCode_Get, strParam, objINSetup.EnumInventoryMasterType.StockItem, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_stockitem.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
        Next intCnt

        Dim drinsert As DataRow
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Please Select Item Code "
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        ddlItemCode.DataSource = dsForDropDown.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
    End Sub

    Sub btnAddItem_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objWPTrxDs As New DataSet()
        Dim strOpCode_AddLine As String = "PR_CLSTRX_WPTRX_LINE_ITEM_ADD"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_WPTRX_STATUS_UPD"
        Dim strOpCode_AddLineItemByBlock As String = "PR_CLSTRX_WPTRX_LINEITEMBYBLOCK_ADD"
        Dim strOpCode_GetLine As String = "PR_CLSTRX_WPTRX_LINE_ITEM_GET"
        Dim strOpCode As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strItem As String = Trim(ddlItemCode.SelectedItem.Value)
        Dim strQty As String = Trim(txtQuantity.Text)
        Dim strAddNote As String = Trim(txtAddNote.Text)

        If strItem = "" Then
            lblErrItemCode.Visible = True
            Exit Sub
        ElseIf strQty = "" Or strQty = "0" Then
            lblErrQuantity.Visible = True
            Exit Sub
        End If

        If ddlChargeLevel.SelectedIndex = 0 And RowPreBlk.Visible = True Then
            If Trim(ddlBlockItem.SelectedItem.Value) = "" Then
                lblBlockItem.Visible = True
                Exit Sub
            End If
            strParam = "|" & "A.WPTrxID = '" & strSelectedWPId & "' And A.AccCode = '" & Trim(ddlActItem.SelectedItem.Value) & "' And A.BlkCode = '" & Trim(ddlBlockItem.SelectedItem.Value) & "' And A.ItemCode = '" & Trim(strItem) & "' And AdditionalNote = '" & Trim(strAddNote) & "' "
        Else
            If Trim(ddlSubBlockItem.SelectedItem.Value) = "" Then
                lblSubBlockItem.Visible = True
                Exit Sub
            End If
            strParam = "|" & "A.WPTrxID = '" & strSelectedWPId & "' And A.AccCode = '" & Trim(ddlActItem.SelectedItem.Value) & "' And A.SubBlkCode = '" & Trim(ddlSubBlockItem.SelectedItem.Value) & "' And A.ItemCode = '" & Trim(strItem) & "' And AdditionalNote = '" & Trim(strAddNote) & "' "
        End If

        Try
            intErrNo = objPRTrx.mtdGetWPTrxLn(strOpCode_GetLine, _
                                            strParam, _
                                            objWPTrxDs, _
                                            False)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_WPTRXLN_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objWPTrxDs.Tables(0).Rows.Count > 0 Then
            lblErrDuplItem.Visible = True
            Exit Sub
        Else
            If ddlChargeLevelItem.SelectedIndex = 0 And RowPreBlkItem.Visible = True Then
                strOpCode = strOpCode_AddLineItemByBlock
                strParam = strSelectedWPId & "|" & _
                       Trim(ddlActItem.SelectedItem.Value) & "|" & _
                       Trim(ddlBlockItem.SelectedItem.Value) & "|" & _
                       Trim(ddlSubBlockItem.SelectedItem.Value) & "|" & _
                       strItem & "|" & _
                       strQty & "|" & _
                       strAddNote & "|" & _
                       Convert.ToString(objPRTrx.EnumWPTrxStatus.Active)
            Else
                strOpCode = strOpCode_AddLine
                strParam = strSelectedWPId & "|" & _
                       Trim(ddlActItem.SelectedItem.Value) & "|" & _
                       "" & "|" & _
                       Trim(ddlSubBlockItem.SelectedItem.Value) & "|" & _
                       strItem & "|" & _
                       strQty & "|" & _
                       strAddNote & "|" & _
                       Convert.ToString(objPRTrx.EnumWPTrxStatus.Active)
            End If

            Try
                intErrNo = objPRTrx.mtdUpdWPTrxLineItem(strOpCode_UpdID, _
                                                        strOpCode, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        False, _
                                                        objWPTrxDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPId)
            End Try
        End If

        onLoad_DisplayLineItem(strSelectedWPId)

        objWPTrxDs = Nothing
    End Sub

    Sub DEDRItem_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "PR_CLSTRX_WPTRX_LINE_ITEM_DEL"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_WPTRX_STATUS_UPD"
        Dim strParam As String
        Dim lblItemCode As Label
        Dim strItem As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = Convert.ToInt32(E.Item.ItemIndex)
        lblItemCode = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("lblItemCode")
        strItem = lblItemCode.Text

        Try
            strParam = strSelectedWPId & "|" & _
                       ddlAccCode.SelectedItem.Value & "|" & _
                       IIf(ddlChargeLevel.SelectedIndex = 0, ddlBlock.SelectedItem.Value, ddlPreBlock.SelectedItem.Value) & "|" & _
                       strItem & "|||" & _
                       Convert.ToString(objPRTrx.EnumWPTrxStatus.Active)

            intErrNo = objPRTrx.mtdUpdWPTrxLineItem(strOpCode_UpdID, _
                                                strOpCode_DelLine, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                False, _
                                                objResult)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_LINE_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPId)
        End Try

        'onLoad_DisplayLineItem(strSelectedWPId, ddlActivity.SelectedItem.Value, ddlBlockAct.SelectedItem.Value)

    End Sub

    'Sub onLoad_DisplayLineItem(ByVal pv_strWPTrxID As String, ByVal pv_strActivity As String, ByVal pv_strBlockAct As String)
    Sub onLoad_DisplayLineItem(ByVal pv_strWPTrxID As String)
        Dim objWPTrxLnDs As New DataSet()
        Dim strOpCd As String = "PR_CLSTRX_WPTRX_LINE_ITEM_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton1 As LinkButton
        Dim intStatus As Integer

        strParam = "|" & " A.WPTrxID = '" & Trim(pv_strWPTrxID) & "' " ' And A.AccCode = '" & Trim(pv_strActivity) & "' And A.BlkCode = '" & Trim(pv_strBlockAct) & "' "

        Try
            intErrNo = objPRTrx.mtdGetWPTrxLn(strOpCd, _
                                                strParam, _
                                                objWPTrxLnDs, _
                                                False)

        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRXLINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dgLineItem.DataSource = objWPTrxLnDs.Tables(0)
        dgLineItem.DataBind()
        If objWPTrxLnDs.Tables(0).Rows.Count > 0 Then
            intStatus = objWPTrxLnDs.Tables(0).Rows(0).Item("Status").Trim()
            If intStatus = objPRTrx.EnumWPTrxStatus.Active Then
                For intCnt = 0 To dgLineDet.Items.Count - 1
                    lbButton1 = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton1.Visible = True
                    lbButton1.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Next
            Else
                For intCnt = 0 To dgLineDet.Items.Count - 1
                    lbButton1 = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton1.Visible = False
                Next
            End If
        End If
    End Sub

    Sub onSelect_ActItemAttd(ByVal Sender As Object, ByVal E As EventArgs)
        Dim pv_strAccCode As String

        If UltraWebTab1.SelectedTabIndex = 1 Then
            pv_strAccCode = ddlActItem.SelectedItem.Value
        Else
            pv_strAccCode = ddlActAttd.SelectedItem.Value
        End If
        BindBlkItemAttd(pv_strAccCode)
    End Sub

    Sub onLoad_DisplayLineAttendance(ByVal pv_strWPTrxID As String)
        Dim strOpCd As String = "PR_CLSTRX_WPTRX_GET_ATTENDANCETRX"
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim objAttdLnDs As New Object()

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|WPTRXID"
        strParamValue = strLocation & _
                        "|" & txtWPTrxID.Text

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAttdLnDs)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_LINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objAttdLnDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objAttdLnDs.Tables(0).Rows.Count - 1
                If blnUseBlk = True Then
                    objAttdLnDs.Tables(0).Rows(intCnt).Item("BlkCode") = objAttdLnDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
                Else
                    objAttdLnDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = objAttdLnDs.Tables(0).Rows(intCnt).Item("SubBlkCode").Trim()
                End If
            Next
        End If

        DgLineAttendance.DataSource = objAttdLnDs.Tables(0)
        DgLineAttendance.DataBind()

       
    End Sub


    Sub DEDR_EditAttd(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim lbUpdbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator

        'blnUpdate.Text = True
        DgLineAttendance.EditItemIndex = CInt(e.Item.ItemIndex)

        onLoad_DisplayLineAttendance(txtWPTrxID.Text)
        If CInt(e.Item.ItemIndex) >= DgLineAttendance.Items.Count Then
            DgLineAttendance.EditItemIndex = -1
            Exit Sub
        End If
        'BindStatusList(DgLineAttendance.EditItemIndex)

        txtEditText = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(txtEditText.Text) = objGLSetup.EnumVehicleExpenseGrpStatus.Active
            Case True
                'StatusList.SelectedIndex = 0
                txtEditText = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("VehExpGrpCode")
                txtEditText.ReadOnly = True
                lbUpdbutton = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Delete"
                lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            Case False
                'StatusList.SelectedIndex = 1
                txtEditText = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("VehExpGrpCode")
                txtEditText.Enabled = False
                txtEditText = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Description")
                txtEditText.Enabled = False
                txtEditText = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UpdateDate")
                txtEditText.Enabled = False
                txtEditText = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UserName")
                txtEditText.Enabled = False
                ddlList = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("StatusList")
                ddlList.Enabled = False
                lbUpdbutton = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Update")
                lbUpdbutton.Visible = False
                lbUpdbutton = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Undelete"
        End Select
        validateCode = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateCode")
        validateDesc = DgLineAttendance.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateDesc")

        'validateCode.ErrorMessage = strValidateCode
        'validateDesc.ErrorMessage = strValidateDesc
    End Sub

    Sub DEDR_UpdateAttd(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim strVehExpGrpCode As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim strCreateDate As String
        Dim strParam As String = ""
        Dim intErrNo As Integer

        txtEditText = E.Item.FindControl("VehExpGrpCode")
        strVehExpGrpCode = txtEditText.Text
        txtEditText = E.Item.FindControl("Description")
        strDescription = txtEditText.Text
        ddlList = E.Item.FindControl("StatusList")
        strStatus = ddlList.SelectedItem.Value
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        strParam = strVehExpGrpCode & "|" & _
                    strDescription & "|" & _
                    strStatus & "|" & _
                    strCreateDate
        'Try
        '    intErrNo = objGLSetup.mtdUpdMasterList(strOppCd_ADD, _
        '                                      strOppCd_UPD, _
        '                                      strOppCd_GET, _
        '                                      strCompany, _
        '                                      strLocation, _
        '                                      strUserId, _
        '                                      strParam, _
        '                                      objGLSetup.EnumGLMasterType.VehicleExpenseGrp, _
        '                                      blnDupKey, _
        '                                      blnUpdate.Text)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_UPDATE_VEHEXPGRP&errmesg=" & Exp.ToString() & "&redirect=GL/Setup/GL_Setup_VehicleSubGrpCode.aspx")
        'End Try

        If blnDupKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            DgLineAttendance.EditItemIndex = -1
            onLoad_DisplayLineAttendance(txtWPTrxID.Text)
        End If

    End Sub

    Sub DEDR_CancelAttd(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        DgLineAttendance.EditItemIndex = -1
        onLoad_DisplayLineAttendance(txtWPTrxID.Text)
    End Sub

    Sub DEDR_DeleteAttd(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strParam As String = ""
        Dim txtEditText As Label
        Dim strAccCode As String
        Dim strSubBlkCode As String
        Dim strEmpCode As String
        Dim blnDupKey As Boolean = False
        Dim strCreateDate As String
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "PR_CLSTRX_WPTRX_LINEATTD_DEL"

        txtEditText = E.Item.FindControl("lblAccCode")
        strAccCode = txtEditText.Text
        txtEditText = E.Item.FindControl("lblSubBlkCode")
        strSubBlkCode = txtEditText.Text
        txtEditText = E.Item.FindControl("lblEmpCode")
        strEmpCode = txtEditText.Text

        strParamName = "LOCCODE|GANGCODE|EMPLCODE|WPTRXID|ACCCODE|SUBBLKCODE|EMPCODE"
        strParamValue = strLocation & "|" & _
                        Trim(ddlGang.SelectedItem.Value) & "|" & _
                        Trim(ddlEmployee.SelectedItem.Value) & "|" & _
                        Trim(txtWPTrxID.Text) & "|" & _
                        strAccCode & "|" & _
                        Trim(strSubBlkCode) & "|" & _
                        Trim(strEmpCode)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                   strParamName, _
                                                   strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_Trx_GENERATEATTENDANCE&errmesg=&redirect=")
        End Try

        DgLineAttendance.EditItemIndex = -1
        onLoad_DisplayLineAttendance(txtWPTrxID.Text)

    End Sub

    Sub BindBlkItemAttd(ByVal pv_strAccCode As String)
        Dim strOpCd As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objData As New Object()

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        If ddlChargeLevelItem.SelectedIndex = 0 Or ddlChargeLevelAttd.SelectedIndex = 0 Then
            strOpCd = "PR_CLSTRX_WPTRX_GET_BLOCKACTIVITY"
        Else
            strOpCd = "PR_CLSTRX_WPTRX_GET_SUBBLOCKACTIVITY"
        End If

        strParamName = "LOCCODE|WPTRXID|ACCCODE"
        strParamValue = strLocation & _
                        "|" & txtWPTrxID.Text & _
                        "|" & pv_strAccCode

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objData)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_LINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objData.Tables(0).Rows.Count - 1
            If ddlChargeLevelItem.SelectedIndex = 0 Then
                objData.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objData.Tables(0).Rows(intCnt).Item("BlkCode"))
                objData.Tables(0).Rows(intCnt).Item("Description") = Trim(objData.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objData.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Else
                objData.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objData.Tables(0).Rows(intCnt).Item("SubBlkCode"))
                objData.Tables(0).Rows(intCnt).Item("Description") = Trim(objData.Tables(0).Rows(intCnt).Item("SubBlkCode")) & " (" & Trim(objData.Tables(0).Rows(intCnt).Item("Description")) & ")"
            End If
        Next

        If UltraWebTab1.SelectedTabIndex = 1 Then
            If ddlChargeLevelItem.SelectedIndex = 0 Then
                Dim drinsert As DataRow
                drinsert = objData.Tables(0).NewRow()
                drinsert("BlkCode") = ""
                drinsert("Description") = lblPleaseSelect.Text & PreBlockTag & lblCode.Text
                objData.Tables(0).Rows.InsertAt(drinsert, 0)

                ddlBlockItem.DataSource = objData.Tables(0)
                ddlBlockItem.DataValueField = "BlkCode"
                ddlBlockItem.DataTextField = "Description"
                ddlBlockItem.DataBind()
            Else
                Dim drinsert As DataRow
                drinsert = objData.Tables(0).NewRow()
                drinsert("SubBlkCode") = ""
                drinsert("Description") = lblPleaseSelect.Text & lblBlock.Text
                objData.Tables(0).Rows.InsertAt(drinsert, 0)

                ddlSubBlockItem.DataSource = objData.Tables(0)
                ddlSubBlockItem.DataValueField = "SubBlkCode"
                ddlSubBlockItem.DataTextField = "Description"
                ddlSubBlockItem.DataBind()
            End If
        Else
            If ddlChargeLevelAttd.SelectedIndex = 0 Then
                Dim drinsert As DataRow
                drinsert = objData.Tables(0).NewRow()
                drinsert("BlkCode") = ""
                drinsert("Description") = lblPleaseSelect.Text & PreBlockTag & lblCode.Text
                objData.Tables(0).Rows.InsertAt(drinsert, 0)

                ddlBlockAttd.DataSource = objData.Tables(0)
                ddlBlockAttd.DataValueField = "BlkCode"
                ddlBlockAttd.DataTextField = "Description"
                ddlBlockAttd.DataBind()
            Else
                Dim drinsert As DataRow
                drinsert = objData.Tables(0).NewRow()
                drinsert("SubBlkCode") = ""
                drinsert("Description") = lblPleaseSelect.Text & lblBlock.Text
                objData.Tables(0).Rows.InsertAt(drinsert, 0)

                ddlSubBlockAttd.DataSource = objData.Tables(0)
                ddlSubBlockAttd.DataValueField = "SubBlkCode"
                ddlSubBlockAttd.DataTextField = "Description"
                ddlSubBlockAttd.DataBind()
            End If
        End If
    End Sub

    Sub BindEmpAttd()
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strOpCdEmployee_Get As String = "HR_CLSSETUP_EMPLOYEE_LIST_GET"

        Try
            strParam = "|||" & objHRTrx.EnumEmpStatus.Active & "|" & strLocation & "|Mst.EmpCode|ASC"

            intErrNo = objHRTrx.mtdGetEmployeeList(strOpCdEmployee_Get, strParam, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=mtdGetEmployeeList&errmesg=" & Exp.Message & "&redirect=PR/Trx/PR_Trx_WPDet.aspx")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item(0) = Trim(objEmpDs.Tables(0).Rows(intCnt).Item(0))
            objEmpDs.Tables(0).Rows(intCnt).Item(1) = Trim(objEmpDs.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                        Trim(objEmpDs.Tables(0).Rows(intCnt).Item(1)) & " )"
        Next intCnt
        dr = objEmpDs.Tables(0).NewRow()
        dr(0) = ""
        dr(1) = "Select employee"
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpAttd.DataSource = objEmpDs.Tables(0)
        ddlEmpAttd.DataValueField = "EmpCode"
        ddlEmpAttd.DataTextField = "EmpName"
        ddlEmpAttd.DataBind()
    End Sub

    Sub BindActItemAttd()
        Dim objWPTrxLnDs As New DataSet()
        Dim strOpCd As String = "PR_CLSTRX_WPTRX_GET_ACTIVITY"
        Dim strParam As String = strSelectedWPId
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRTrx.mtdGetWPTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objWPTrxLnDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRXLINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objWPTrxLnDs.Tables(0).Rows.Count - 1
            objWPTrxLnDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objWPTrxLnDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objWPTrxLnDs.Tables(0).Rows(intCnt).Item("Description") = objWPTrxLnDs.Tables(0).Rows(intCnt).Item("AccCode") & " (" & Trim(objWPTrxLnDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next intCnt

        Dim dr As DataRow
        dr = objWPTrxLnDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = lblPleaseSelect.Text & " Activity Code"
        objWPTrxLnDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlActItem.DataSource = objWPTrxLnDs.Tables(0)
        ddlActItem.DataValueField = "AccCode"
        ddlActItem.DataTextField = "Description"
        ddlActItem.DataBind()
        ddlActItem.SelectedIndex = intSelectedIndex

        ddlActAttd.DataSource = objWPTrxLnDs.Tables(0)
        ddlActAttd.DataValueField = "AccCode"
        ddlActAttd.DataTextField = "Description"
        ddlActAttd.DataBind()
        ddlActAttd.SelectedIndex = intSelectedIndex
    End Sub

    Sub Onselect_TabChanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebTab.WebTabEvent)
        BindBlkItemAttd("")

        If UltraWebTab1.SelectedTabIndex = 1 Then
            onLoad_DisplayLineItem(txtWPTrxID.Text)
        Else
            BindVehicle("")
            BindVehExpense("")
            onLoad_DisplayLineAttendance(txtWPTrxID.Text)

            If ddlGang.SelectedItem.Value = "" Then
                ddlActAttd.Enabled = False
                ddlChargeLevelAttd.Enabled = False
                ddlBlockAttd.Enabled = False
                ddlSubBlockAttd.Enabled = False
                ddlVehicle.Enabled = False
                ddlVehExpense.Enabled = False
                ddlEmpAttd.Enabled = False
                txtPremi.Enabled = False
                txtOTHours.Enabled = False
                btnAddAttd.Enabled = False
            Else
                BindEmpAttd("")
            End If
        End If
    End Sub

    Sub btnAddAttd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim objFormatDate As String
        Dim objActualDateFrom As String
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "PR_CLSTRX_GENERATE_ATTENDANCE_FROM_WPTRX_ADDATTD"

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtWPDate.Text, _
                                       objFormatDate, _
                                       objActualDateFrom) = False Then
            lblErrWPDateFmt.Visible = True
            lblErrWPDateFmt.Text = lblErrWPDateFmtMsg.Text & objFormatDate
        End If

        If Trim(ddlEmpAttd.SelectedItem.Value) = "" Then
            lblErrEmpAttd.Visible = True
            Exit Sub
        End If

        strParamName = "LOCCODE|STARTDATE|ENDDATE|ACCCODE|BLKCODE|SUBBLKCODE|VEHCODE|VEHEXPENSE|EMPCODE|WPTRXID"
        If ddlChargeLevelAttd.SelectedIndex = 0 And RowPreBlkAttd.Visible = True Then
            If Trim(ddlBlockAttd.SelectedItem.Value) = "" Then
                lblBlockAttd.Visible = True
                Exit Sub
            End If
            strParamValue = strLocation & "|" & _
                                    objActualDateFrom & "|" & _
                                    objActualDateFrom & "|" & _
                                    Trim(ddlActAttd.SelectedItem.Value) & "|" & _
                                    Trim(ddlBlockAttd.SelectedItem.Value) & "|" & _
                                    Trim(ddlSubBlockAttd.SelectedItem.Value) & "|" & _
                                    Trim(ddlVehicle.SelectedItem.Value) & "|" & _
                                    Trim(ddlVehExpense.SelectedItem.Value) & "|" & _
                                    Trim(ddlEmpAttd.SelectedItem.Value) & "|" & _
                                    Trim(txtWPTrxID.Text)
        Else
            If Trim(ddlSubBlockAttd.SelectedItem.Value) = "" Then
                lblSubBlockAttd.Visible = True
                Exit Sub
            End If
            strParamValue = strLocation & "|" & _
                        objActualDateFrom & "|" & _
                        objActualDateFrom & "|" & _
                        Trim(ddlActAttd.SelectedItem.Value) & "|" & _
                        "" & "|" & _
                        Trim(ddlSubBlockAttd.SelectedItem.Value) & "|" & _
                        Trim(ddlVehicle.SelectedItem.Value) & "|" & _
                        Trim(ddlVehExpense.SelectedItem.Value) & "|" & _
                        Trim(ddlEmpAttd.SelectedItem.Value) & "|" & _
                        Trim(txtWPTrxID.Text)
        End If
        
        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                   strParamName, _
                                                   strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_Trx_GENERATEATTENDANCE&errmesg=&redirect=")
        End Try

        onLoad_DisplayLineAttendance(txtWPTrxID.Text)
    End Sub

    Sub BindVehicle(ByVal pv_strVehCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_VEHICLE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        If pv_strVehCode <> "" Then
            strParam = Trim(ddlVehicle.SelectedItem.Value) & "|" & _
                                   "" & "|" & _
                                   "" & "|" & _
                                   "" & "|" & _
                                   "" & "|" & _
                                   "veh.VehCode" & "|"
        Else
            strParam = "" & "|" & _
                                   "" & "|" & _
                                   "" & "|" & _
                                   "" & "|" & _
                                   "veh.VehCode" & "|" & _
                                   "" & "|"
        End If
        

        Try
            intErrNo = objGLSetup.mtdGetVehicle(strOpCd, strLocation, strParam, objGangDs, False)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If pv_strVehCode <> "" Then
            For intCnt = 0 To objGangDs.Tables(0).Rows.Count - 1
                If Trim(objGangDs.Tables(0).Rows(intCnt).Item("VehCode")) = Trim(pv_strVehCode) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objGangDs.Tables(0).NewRow()
        dr("VehCode") = ""
        dr("Description") = "Select Vehicle Code"
        objGangDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehicle.DataSource = objGangDs.Tables(0)
        ddlVehicle.DataValueField = "VehCode"
        ddlVehicle.DataTextField = "Description"
        ddlVehicle.DataBind()
        ddlVehicle.SelectedIndex = intSelectedIndex
        ddlVehicle.AutoPostBack = True
    End Sub

    Sub BindVehExpense(ByVal pv_strVehExpense As String)
        Dim strOpCd As String = "GL_CLSSETUP_VEHICLE_EXPENSE_CODE_GET"
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        If pv_strVehExpense <> "" Then
            strParam = Trim(ddlVehExpense.SelectedItem.Value) & "|" & _
                       "" & "|" & _
                       "" & "|" & _
                       "" & "|" & _
                       "" & "|" & _
                       "veh.VehExpenseCode" & "|"
        Else
            strParam = "" & "|" & _
                               "" & "|" & _
                               "" & "|" & _
                               "" & "|" & _
                               "veh.VehExpenseCode" & "|" & _
                               "" & "|"
        End If

        Try
            intErrNo = objGLSetup.mtdGetVehExpCode(strOpCd, strParam, objGangDs, False)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_EXPENSE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If pv_strVehExpense <> "" Then
            For intCnt = 0 To objGangDs.Tables(0).Rows.Count - 1
                If Trim(objGangDs.Tables(0).Rows(intCnt).Item("VehExpenseCode")) = Trim(pv_strVehExpense) Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objGangDs.Tables(0).NewRow()
        dr("VehExpenseCode") = ""
        dr("Description") = "Select Vehicle Expense Code"
        objGangDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehExpense.DataSource = objGangDs.Tables(0)
        ddlVehExpense.DataValueField = "VehExpenseCode"
        ddlVehExpense.DataTextField = "Description"
        ddlVehExpense.DataBind()
        ddlVehExpense.SelectedIndex = intSelectedIndex
        ddlVehExpense.AutoPostBack = True
    End Sub

    Sub BindEmpAttd(ByVal pv_strEmpCode As String)
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objDateFormat As String
        Dim objValidDate As String
        Dim intSelectedIndex As Integer = 0
        Dim arrEmp As Array
        Dim strOpCdEmployee_Get As String = "HR_CLSSETUP_EMPLOYEE_LIST_GET"

        Try
            strParam = "|||" & objHRTrx.EnumEmpStatus.Active & "|" & strLocation & "|Mst.EmpCode|ASC"

            intErrNo = objHRTrx.mtdGetEmployeeList(strOpCdEmployee_Get, strParam, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=mtdGetEmployeeList&errmesg=" & Exp.Message & "&redirect=PR/Trx/PR_Trx_WPDet.aspx")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item(0) = Trim(objEmpDs.Tables(0).Rows(intCnt).Item(0))
            objEmpDs.Tables(0).Rows(intCnt).Item(1) = Trim(objEmpDs.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                        Trim(objEmpDs.Tables(0).Rows(intCnt).Item(1)) & " )"
            If objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(pv_strEmpCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt
        dr = objEmpDs.Tables(0).NewRow()
        dr(0) = ""
        dr(1) = "Select employee"
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpAttd.DataSource = objEmpDs.Tables(0)
        ddlEmpAttd.DataValueField = "EmpCode"
        ddlEmpAttd.DataTextField = "EmpName"
        ddlEmpAttd.DataBind()
        ddlEmpAttd.SelectedIndex = intSelectedIndex

    End Sub
End Class
