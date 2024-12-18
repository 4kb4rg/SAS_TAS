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
Imports Microsoft.VisualBasic.DateAndTime
Imports Microsoft.VisualBasic

Public Class PR_trx_PieceAttd : Inherits Page

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents txtAttdDate As TextBox
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents ddlAttendance As DropDownList
    Protected WithEvents ddlHarvInc As DropDownList
    Protected WithEvents lblErrHarvInc As Label
    Protected WithEvents lblErrDenda As Label
    Protected Withevents lblLoseFruit As Label
    Protected WithEvents txtLoseFruit As TextBox
    Protected WithEvents lblErrLoseFruit As Label
    Protected WithEvents txtDendaQty As TextBox
    Protected WithEvents lblErrPenaltyQty As Label
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents btnSelDate As Image
    Protected WithEvents lblAttdId As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents attdid As HtmlInputHidden
    Protected WithEvents btnFind1 As HtmlInputButton
    Protected WithEvents RefreshBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents ConfirmBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblVehicleOption As Label

    Protected WithEvents lblRfvAttdDate As Label   
    Protected WithEvents lblErrAttdDate As Label
    Protected WithEvents lblErrAttdDateDesc As Label
    Protected WithEvents lblErrAttendance As Label
    Protected WithEvents lblErrPreBlock As Label
    Protected WithEvents lblErrPreBlock1 As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblErrSubBlock As Label
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrVeh As Label
    Protected WithEvents lblErrVehExp As Label
    Protected WithEvents lblErrHour As Label
    Protected WithEvents lblErrTotal As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblPreBlock As Label
    Protected WithEvents lblPreBlock1 As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehExpense As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblAttdPayType As Label
    Protected WithEvents lblEmpPayType As Label
    Protected WithEvents lblOTInd As Label
    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlock As HtmlTableRow
    Protected WithEvents RowPreBlock1 As HtmlTableRow
    Protected WithEvents RowBlock As HtmlTableRow
    Protected WithEvents RowChargeTo As HtmlTableRow
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Protected WithEvents txtHarvestInc As TextBox

    Protected WithEvents lblPayType as Label
    Protected WithEvents txtDendaCode As TextBox
    Protected WithEvents txtAccount As TextBox
    Protected WithEvents txtPreBlock As TextBox
    Protected WithEvents txtPreBlock1 As TextBox
    Protected WithEvents txtVeh As TextBox
    Protected WithEvents txtVehExp As TextBox
    Protected WithEvents txtJanjangBruto as TextBox
    Protected WithEvents lblErrJanjangBruto as label
    Protected WithEvents txtJanjangNetto as TextBox
    Protected WithEvents lblErrJanjangNetto as label
    Protected WithEvents lblQuotaInQtyVal as label
    Protected WithEvents lblTotalJanjangBruto as label
    Protected WithEvents lblTotalJanjangNetto as label
    Protected WithEvents lblTotalDenda as label
    Protected WithEvents lblJmlPremi as label
    Protected WithEvents lblCountDayType as label
    Protected WithEvents lblErrValidation as label
    Protected WithEvents lblErrMessage as label
    Protected WithEvents lblAccountDesc as label
    Protected WithEvents lblPreBlockDesc as label
    Protected WithEvents lblPreBlockDesc1 as label
    Protected WithEvents lblVehDesc as label
    Protected WithEvents lblVehExpDesc as label
    Protected WithEvents lblDendaDesc as label
    Protected WithEvents lblOTHours as Label
    Protected WithEvents txtOTHours as textbox



    Protected objPRTrx As New agri.PR.clsTrx()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objAttdDs As New Object()
    Dim objAttdLnDs As New Object()
    Dim objEmpDs As New Object()
    Dim objAttdCodeDs As New Object()
    Dim objAccDs As New Object()
    Dim objBlkDs As New Object()
    Dim objVehDs As New Object()
    Dim objVehExpDs As New Object()
    Dim objPayDivDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objMACDs As New Object()
    Dim objHarvIncDs As New Object()
    Dim objDendaDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFmt As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim blnHasLine As Boolean = False
    Dim blnUseBlk As Boolean = False

    Dim strSelectedAcc As String = ""
    Dim strSelectedPreBlk As String = ""
    Dim strSelectedBlk As String = ""
    Dim strSelectedVeh As String = ""
    Dim strSelectedVehExp As String = ""
    
    Dim dblTotalAmount As Double

    Dim dblTotalHour As Decimal
    Dim dblTotalUnitWorked As Decimal
    Dim dblTotalLoseFruit As Decimal
    Dim dblTotalDenda As Decimal
    Dim dblTotalAllowance As Decimal

    Dim dblTotalUnitNet As Decimal
    Dim dblTotalPremi As Decimal

    Dim dblADHours As Double

    Dim strSelectedAttdId As String = ""
    Dim strSelectedEmpCode As String = ""
    Dim strSelectedDate As String = ""
    Dim intStatus As Integer
    Dim strCostLevel As String
    Dim strPickerCategory As String
    Dim PreBlockTag As String
    Dim BlockTag As String

    Protected WithEvents lblBPErr As Label
    Protected WithEvents lblLocCodeErr As Label
    Protected WithEvents lblDayType As Label


    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strDateFmt = Session("SS_DATEFMT")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strCostLevel = Session("SS_COSTLEVEL")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvAttd), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            dgLineDet.Visible = True
            lblErrAttdDate.Visible = False
            lblErrAttdDateDesc.Visible = False
            lblErrAttendance.Visible = False
            lblErrAttendance.Visible = False            
            lblErrPreBlock.Visible = False
            lblErrPreBlock1.Visible = False
            lblErrAccount.Visible = False
            lblErrVeh.Visible = False
            lblErrVehExp.Visible = False
            lblErrTotal.Visible = False
            lblErrHarvInc.Visible = False
            lblErrDenda.Visible = False
            strSelectedAttdId = Trim(IIf(Request.QueryString("attdid") <> "", Request.QueryString("attdid"), Request.Form("attdid")))
            strSelectedEmpCode = Trim(Request.QueryString("empcode"))
            strSelectedDate = Trim(Request.QueryString("date"))
            intStatus = CInt(lblHiddenSts.Text)

            If strCostLevel = "block" Then
                blnUseBlk = True
            End If

            onload_GetLangCap()

            If Not IsPostBack Then
                BindChargeLevelDropDownList()

                lblPreBlock.Text = "Tahun Tanam/Block Code "
                lblPreBlock1.Text = "Tahun Tanam/Block Code "

                If strSelectedAttdId <> "" Then
                    attdid.Value = strSelectedAttdId
                    onLoad_Display()
                Else
                    If (strSelectedEmpCode <> "") And (strSelectedDate <> "") Then
                        onLoad_Display()
                    Else
                        txtAttdDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                        BindEmployee("", False)
                        BindAttdCode("")
                        BindHarvIncentive("")
                        CheckEmpPayType("")
                        onLoad_BindButton()
                    End If
                End If
            End If
            lblBPErr.Visible = False
            lblLocCodeErr.Visible = False
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
        if ddlChargeLevel.SelectedIndex = 0 then
            RowPreBlock.visible = true
            RowPreBlock1.visible = false
        else
            RowPreBlock.visible = false
            RowPreBlock1.visible = true
        end if 
    End Sub

    Sub CheckEmpPayType(ByVal pv_strEmpCode As String)
        Dim objPayDs As New DataSet()
        Dim strOpCd As String = "HR_CLSTRX_PAYROLL_GET"
        Dim strParam As String = pv_strEmpCode & "|||EmpCode|ASC"
        Dim intErrNo As Integer


        If Trim(pv_strEmpCode) = "" Then
        Else
            Try
                intErrNo = objHRTrx.mtdGetEmployeePay(strOpCd, _
                                                    strParam, _
                                                    objPayDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_PAYTYPE_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objPayDs.Tables(0).Rows.Count > 0 Then
                lblOTInd.text = "0"
                Select Case objPayDs.Tables(0).Rows(0).Item("PayType").Trim()
                End Select

            End If
            objPayDs = Nothing
        End If
    End Sub

    Sub onLoad_BindButton()
        txtAttdDate.Enabled = False
        ddlEmployee.Enabled = False
        SaveBtn.Visible = False
        ConfirmBtn.Visible = False
        CancelBtn.Visible = False
        tblSelection.Visible = False
        btnFind1.Disabled = False
        if Trim(lblCountDayType.Text) <> "" then
                    if (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Others) and (Trim(lblDayType.Text) = objPRSetup.EnumDayType.Off) then 
                        tblSelection.Visible = True
                        dgLineDet.visible = true
                        SaveBtn.visible = false 
                        txtAccount.enabled = false
                        txtPreBlock.enabled = false
                        txtPreBlock1.enabled = false
                        txtVeh.enabled = false
                        txtVehExp.enabled = false
                    elseif (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working) then
                        tblSelection.Visible = True
                        dgLineDet.visible = true
                        SaveBtn.visible = false 
                        txtAccount.enabled = true
                        txtPreBlock.enabled = true
                        txtPreBlock1.enabled = true
                        txtVeh.enabled = true
                        txtVehExp.enabled = true
                    else
                        tblSelection.Visible = false
                        dgLineDet.visible = false
                        SaveBtn.visible = true 
                    end if
        end if
        Select Case intStatus
            Case objPRTrx.EnumAttdTrxStatus.Active
                txtAttdDate.Enabled = True
                ddlEmployee.Enabled = True
                SaveBtn.Visible = True
                ConfirmBtn.Visible = True
                tblSelection.Visible = True
                    if (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Others) and (Trim(lblDayType.Text) = objPRSetup.EnumDayType.Off) then 
                        tblSelection.Visible = True
                        dgLineDet.visible = true
                        SaveBtn.visible = false 
                        txtAccount.enabled = false
                        txtPreBlock.enabled = false
                        txtPreBlock1.enabled = false
                        txtVeh.enabled = false
                        txtVehExp.enabled = false

                    elseif (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working) then
                        tblSelection.Visible = True
                        dgLineDet.visible = true
                        SaveBtn.visible = false 
                        txtAccount.enabled = true
                        txtPreBlock.enabled = true
                        txtPreBlock1.enabled = true
                        txtVeh.enabled = true
                        txtVehExp.enabled = true
                    else
                        tblSelection.Visible = false
                        dgLineDet.visible = false
                        SaveBtn.visible = true 
                    end if


            Case objPRTrx.EnumAttdTrxStatus.Closed, objPRTrx.EnumAttdTrxStatus.Cancelled
                btnFind1.Disabled = True
                tblSelection.Visible = False
                    if Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working then 
                        tblSelection.Visible = false
                        dgLineDet.visible = true
                    elseif (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Others) and (Trim(lblDayType.Text) = objPRSetup.EnumDayType.Off) then
                        tblSelection.Visible = false
                        dgLineDet.visible = true
                    else
                        tblSelection.Visible = false
                        dgLineDet.visible = false
                    end if

            Case objPRTrx.EnumAttdTrxStatus.Confirmed
                btnFind1.Disabled = True
                btnSelDate.Visible = False
                RefreshBtn.Visible = False
                ddlEmployee.Enabled = False
                tblSelection.Visible = False
                CancelBtn.Visible = True
                    if Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working then 
                        tblSelection.Visible = false
                        dgLineDet.visible = true
                    elseif (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Others) and (Trim(lblDayType.Text) = objPRSetup.EnumDayType.Off) then
                        tblSelection.Visible = false
                        dgLineDet.visible = true
                    else
                        tblSelection.Visible = false
                        dgLineDet.visible = false
                    end if

            Case Else
                txtAttdDate.Enabled = True
                ddlEmployee.Enabled = True
                ConfirmBtn.Visible = True
                dgLineDet.DataBind()
        End Select


    End Sub

    Sub onLoad_Display()
        Dim strOpCd_AttDate As String = "PR_CLSTRX_ATTENDANCETRX_ATTDATE_GET"
        Dim strOpCd As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim objFormatDate As String
        Dim objActualDate As String
       
            


        If objGlobal.mtdValidInputDate(strDateFmt, _
                                            strSelectedDate, _
                                            objFormatDate, _
                                            objActualDate) = True Then
        End if


        CheckEmpPayType(strSelectedEmpCode)

        Try
            strOpCd = strOpCd_AttDate


            if strSelectedAttdId = "" then
                strParam = "And trx.EmpCode = '" & Trim(strSelectedEmpCode) & _
                            "' And trx.AttDate = '" & objActualDate & _
                            "' And trx.Status IN ('" & objPRTrx.EnumAttdTrxStatus.Active & _
                            "','" & objPRTrx.EnumAttdTrxStatus.Confirmed & _
                            "','" & objPRTrx.EnumAttdTrxStatus.Closed & "') " & _
                            "And trx.AttType = '" & objPRTrx.EnumAttdTrxType.PieceRated & "' "
            else

                strParam = "And trx.attid = '" & Trim(strSelectedAttdId) & _
                            "' And trx.Status IN ('" & objPRTrx.EnumAttdTrxStatus.Active & _
                            "','" & objPRTrx.EnumAttdTrxStatus.Confirmed & _
                            "','" & objPRTrx.EnumAttdTrxStatus.Closed & "') " & _
                            "And trx.AttType = '" & objPRTrx.EnumAttdTrxType.PieceRated & "' "
            end if 

            intErrNo = objPRTrx.mtdGetAttendanceTrx(strOpCd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objAttdDs, _
                                                    True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        If objAttdDs.Tables(0).Rows.Count > 0 Then

            strSelectedAttdId = objAttdDs.Tables(0).Rows(0).Item("AttId").Trim()
            attdid.Value = objAttdDs.Tables(0).Rows(0).Item("AttId").Trim()
            lblAttdId.Text = objAttdDs.Tables(0).Rows(0).Item("AttId").Trim()
            txtAttdDate.Text = objGlobal.GetShortDate(strDateFmt, objAttdDs.Tables(0).Rows(0).Item("AttDate"))
            lblPeriod.Text = objAttdDs.Tables(0).Rows(0).Item("AccMonth").Trim() & "/" & objAttdDs.Tables(0).Rows(0).Item("AccYear").Trim()
            intStatus = CInt(objAttdDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenSts.Text = objAttdDs.Tables(0).Rows(0).Item("Status").Trim()
            lblStatus.Text = objPRTrx.mtdGetAttdTrxStatus(objAttdDs.Tables(0).Rows(0).Item("Status").Trim())
            lblDateCreated.Text = objGlobal.GetLongDate(objAttdDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objAttdDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objAttdDs.Tables(0).Rows(0).Item("UserName").Trim()
            lblEmpPayType.Text = objAttdDs.Tables(0).Rows(0).Item("PayType").Trim()
            BindAttdCode(objAttdDs.Tables(0).Rows(0).Item("AttCode").Trim())

            If Request.Form("ddlAccount") = "" Then
            Else
                onSelect_Account_Click()
            End If


            If lblHiddenSts.Text = objPRTrx.EnumAttdTrxStatus.Confirmed Then
                BindEmployee(objAttdDs.Tables(0).Rows(0).Item("EmpName").Trim() & "|" & objAttdDs.Tables(0).Rows(0).Item("EmpCode").Trim(), True)
            Else
                BindEmployee(objAttdDs.Tables(0).Rows(0).Item("EmpCode").Trim(), False)
            End If

            txtDendaQty.Text = 0
            txtJanjangNetto.Text = 0
            txtLoseFruit.Text = 0
            txtJanjangBruto.Text = 0
            txtOTHours.Text = 0
            txtHarvestInc.Text = 0
             
            onLoad_DisplayLine()
        Else
            strSelectedAttdId = ""
            attdid.Value = ""
            lblHiddenSts.Text = "0"
            intStatus = 0
               
            lblPeriod.Text = ""
            lblStatus.Text = ""
            lblDateCreated.Text = ""
            lblLastUpdate.Text = ""
            lblUpdatedBy.Text = ""
            txtAttdDate.Text = strSelectedDate
            BindEmployee(strSelectedEmpCode, False)
            BindHarvIncentive("")
            ddlAttendance.Enabled = True
        End If
        onLoad_BindButton()
    End Sub


    Sub onLoad_DisplayLine()
        Dim strOpCd As String = "PR_CLSTRX_ATTENDANCETRX_LINE_GET"
        Dim strParam As String = strSelectedAttdId
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        blnHasLine = False

        Try
            intErrNo = objPRTrx.mtdGetAttendanceTrx(strOpCd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objAttdLnDs, _
                                                    True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_LINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        If objAttdLnDs.Tables(0).Rows.Count > 0 Then
            blnHasLine = True
            dblTotalHour = 0
            dblTotalAmount = 0
            dblTotalUnitWorked = 0
            dblTotalLoseFruit = 0
            dblTotalDenda = 0
            dblTotalAllowance = 0

            dblTotalUnitNet = 0
            dblTotalPremi = 0

            For intCnt = 0 To objAttdLnDs.Tables(0).Rows.Count - 1
                If blnUseBlk = True Then
                    objAttdLnDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objAttdLnDs.Tables(0).Rows(intCnt).Item("BlkCode"))
                Else
                    objAttdLnDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objAttdLnDs.Tables(0).Rows(intCnt).Item("SubBlkCode"))
                End If
                dblTotalDenda += CDbl(objAttdLnDs.Tables(0).Rows(intCnt).Item("DendaQty"))
                dblTotalUnitWorked += CDbl(objAttdLnDs.Tables(0).Rows(intCnt).Item("UnitWorked"))
                dblTotalUnitNet += CDbl(objAttdLnDs.Tables(0).Rows(intCnt).Item("UnitWorkNet"))
                dblTotalPremi += CDbl(objAttdLnDs.Tables(0).Rows(intCnt).Item("HarvestInc"))
    
            Next

            lblTotalJanjangBruto.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(dblTotalUnitWorked, 2)
            lblTotalJanjangNetto.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(dblTotalUnitNet,2)
            lblTotalDenda.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(dblTotalDenda,2)
            lblJmlPremi.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(dblTotalPremi,2)
    
            ddlAttendance.Enabled = False
        Else

            If intStatus = objPRTrx.EnumAttdTrxStatus.Confirmed Then
                ddlAttendance.Enabled = False
            Else
                ddlAttendance.Enabled = True
            End If
        End If

        dgLineDet.DataSource = objAttdLnDs.Tables(0)
        dgLineDet.DataBind()

        If intStatus = objPRTrx.EnumAttdTrxStatus.Active Then
            For intCnt = 0 To dgLineDet.Items.Count - 1
                If intCnt = 0 Then
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('deleteall');"
                Else
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                End If
            Next
        Else
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = False
            Next
        End If

            if (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Others and trim(lblStatus.Text) = "Active" and (Trim(lblDayType.Text) = objPRSetup.EnumDayType.Off)) or (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working and trim(lblStatus.Text) = "Active") then 
                tblSelection.Visible = True
                dgLineDet.visible = true
            elseif (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Others and trim(lblStatus.Text) <> "Active" and (Trim(lblDayType.Text) = objPRSetup.EnumDayType.Off)) or (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working and trim(lblStatus.Text) <> "Active")then 
                tblSelection.Visible = false
                dgLineDet.visible = true
            else
                tblSelection.Visible = false
                dgLineDet.visible = false
            end if


        GetAccountDesc(Trim(txtAccount.Text))
        GetPreBlockDesc("")
        GetVehDesc(Trim(txtVeh.Text))
        GetVehExpDesc(Trim(txtVehExp.Text))
        GetDendaDesc(Trim(txtDendaCode.Text))

    End Sub

    Sub BindEmployee(ByVal pv_strEmpId As String, ByVal pv_blnHasConfirmed As Boolean)
        Dim strOpCd As String = "PR_CLSTRX_PIECEATTDTRX_SHORTLIST_EMPCODE_GET"
        Dim dr As DataRow
        Dim strParam As String = objPRTrx.EnumAttdTrxStatus.Confirmed & "|" & objHRTrx.EnumEmpStatus.Active
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim intSelectedIndex As Integer = 0
        Dim arrEmp As Array


        If pv_blnHasConfirmed = False Then
            If objGlobal.mtdValidInputDate(strDateFmt, _
                                            txtAttdDate.Text, _
                                            objFormatDate, _
                                            objActualDate) = True Then
            End If

            pv_strEmpId = Trim(pv_strEmpId)

            Try
                strParam = strParam & "|" & objActualDate & "|" & objPRTrx.EnumAttdTrxType.Daily
                intErrNo = objPRTrx.mtdGetAttendanceTrx(strOpCd, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        objEmpDs, _
                                                        False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If pv_strEmpId <> "" Then
                For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
                    If objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode").Trim() = pv_strEmpId Then
                        intSelectedIndex = intCnt + 1
                        Exit For
                    End If
                Next
            End If
            
            dr = objEmpDs.Tables(0).NewRow()
            dr("EmpCode") = ""
            dr("_Description") = "Select Employee Code"
            objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

            ddlEmployee.DataSource = objEmpDs.Tables(0)
            ddlEmployee.DataValueField = "EmpCode"
            ddlEmployee.DataTextField = "_Description"
            ddlEmployee.DataBind()
            ddlEmployee.SelectedIndex = intSelectedIndex
            ddlEmployee.AutoPostBack = True
        Else
            arrEmp = Split(pv_strEmpId, "|")
            ddlEmployee.Items.Clear()
            ddlEmployee.Items.Add(New ListItem(arrEmp(0), arrEmp(1)))
        End If
    End Sub

    Sub onClick_Employee(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Refresh_Employee()
    End Sub

    Sub Refresh_Employee()
        Dim strOpCd As String = "PR_CLSTRX_ATTENDANCETRX_EMPCODE_ATTDATE_GET"
        Dim strEmployee As String = Request.Form("ddlEmployee")
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strPreBlk As String = Request.Form("ddlPreBlock")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strSubBlk As String = Request.Form("ddlSubBlock")
        Dim strVeh As String = Request.Form("ddlVeh")
        Dim strVehExp As String = Request.Form("ddlVehExp")
        Dim strParam As String
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim intErrNo As Integer
        Dim intAttdTrxType As Integer

       lblTotalJanjangBruto.Text = ""
       lblTotalJanjangNetto.Text = ""
       lblTotalDenda.Text = ""
       lblJmlPremi.Text = ""


        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtAttdDate.Text, _
                                       objFormatDate, _
                                       objActualDate) = False Then
            lblErrAttdDate.Text = lblErrAttdDateDesc.Text & objFormatDate
            lblErrAttdDate.Visible = True
            dgLineDet.Visible = False
            lblHiddenSts.Text = "0"
            intStatus = 0
            lblAttdId.Text = ""
            lblPeriod.Text = ""
            lblStatus.Text = ""
            lblDateCreated.Text = ""
            lblLastUpdate.Text = ""
            lblUpdatedBy.Text = ""

            BindAttdCode(ddlAttendance.SelectedItem.Value)
            BindEmployee(strEmployee, False)            
            onLoad_BindButton()
            Exit Sub
        Else
            ddlharvinc.enabled = true 
            Try
                strParam = " AND DateDiff(day, '" & objActualDate & "', trx.AttDate) = 0 " & _
                           " AND trx.EmpCode = '" & trim(strEmployee) & "' " & _
                           " AND trx.Status IN ('" & objPRTrx.EnumAttdTrxStatus.Active & "','" & objPRTrx.EnumAttdTrxStatus.Confirmed & "','" & objPRTrx.EnumAttdTrxStatus.Closed & "') "
                intErrNo = objPRTrx.mtdGetAttendanceTrx(strOpCd, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        objAttdDs, _
                                                        True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_FINDEMPCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objAttdDs.Tables(0).Rows.Count > 0 Then

                attdid.Value = Trim(objAttdDs.Tables(0).Rows(0).Item("AttId"))
                strSelectedAttdId = Trim(objAttdDs.Tables(0).Rows(0).Item("AttId"))
                intAttdTrxType = CInt(IIF(Trim(objAttdDs.Tables(0).Rows(0).Item("AttType"))="","0",objAttdDs.Tables(0).Rows(0).Item("AttType")))
                strSelectedEmpCode = Trim(strEmployee)
                strSelectedAcc = strAcc
                strSelectedPreBlk = strPreBlk
                If blnUseBlk = True Then
                    strSelectedBlk = strBlk
                Else
                    strSelectedBlk = strSubBlk
                End If
                strSelectedVeh = strVeh
                strSelectedVehExp = strVehExp
                If intAttdTrxType = objPRTrx.EnumAttdTrxType.Daily Then
                    Response.Redirect("pr_trx_dailyattd.aspx?empcode=" & strSelectedEmpCode & "&date=" & strSelectedDate)
                Else
                    onLoad_Display()
                End If
            Else
                dgLineDet.Visible = False
                attdid.Value = ""
                lblHiddenSts.Text = "0"
                intStatus = 0
                lblAttdId.Text = ""
                lblPeriod.Text = ""
                lblStatus.Text = ""
                lblDateCreated.Text = ""
                lblLastUpdate.Text = ""
                lblUpdatedBy.Text = ""
                BindAttdCode(ddlAttendance.SelectedItem.Value)
                BindEmployee(strEmployee, False)
                onLoad_BindButton()
                ddlAttendance.Enabled = True
            End If
        End If
    End Sub


    Sub BindAttdCode(ByVal pv_strAttdCode As String)
        Dim strOpCd As String = "PR_CLSTRX_DAILYATTENDANCE_ATTDANCE_GET"
        Dim dr As DataRow
        Dim strParam As String
        Dim strSort As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        pv_strAttdCode = Trim(pv_strAttdCode)

        strSort = "ORDER BY Atd.AttCode ASC"
        strSearch = "AND Atd.PayType in ('" & objPRSetup.EnumPayType.DailyRate & "','" & objPRSetup.EnumPayType.NoRate & "') AND Atd.Status = '" & objPRSetup.EnumAttStatus.Active & "' "

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objAttdCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If pv_strAttdCode <> "" Then
            For intCnt = 0 To objAttdCodeDs.Tables(0).Rows.Count - 1
                If objAttdCodeDs.Tables(0).Rows(intCnt).Item("AttCode").Trim() = pv_strAttdCode Then
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objAttdCodeDs.Tables(0).NewRow()
        dr("AttCode") = ""
        dr("_Description") = "Select Attendance Code"
        dr("Hours") = 0
        dr("PayType") = 0
        objAttdCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        dblADHours = objAttdCodeDs.Tables(0).Rows(intSelectedIndex).Item("Hours")
        lblAttdPayType.text = objAttdCodeDs.Tables(0).Rows(intSelectedIndex).Item("PayType")

        ddlAttendance.DataSource = objAttdCodeDs.Tables(0)
        ddlAttendance.DataValueField = "AttCode"
        ddlAttendance.DataTextField = "_Description"
        ddlAttendance.DataBind()
        ddlAttendance.SelectedIndex = intSelectedIndex
        ddlAttendance.AutoPostBack = True

        lblCountDayType.Text = Trim(objAttdCodeDs.Tables(0).Rows(intSelectedIndex).Item("CountDayType").ToString)        

    End Sub

    Sub onSelect_Attendance(ByVal Sender As Object, ByVal E As EventArgs)

        Dim strOpCd As String = "PR_CLSTRX_DAILYATTENDANCE_ATTDANCE_GET"
        Dim dr As DataRow
        Dim strParam As String
        Dim strSort As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        strSearch = "and atd.Status = '" & objPRSetup.EnumAttStatus.Active & "' "
        strSearch = strSearch & " and atd.attcode = '" & Trim(ddlAttendance.SelectedItem.Value) & "' "

        strSort = "order by atd.AttCode asc"

        strParam = strSort & "|" & strSearch
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objAttdCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        if objAttdCodeDs.Tables(0).Rows.Count > 0 then 
            lblCountDayType.Text = Trim(objAttdCodeDs.Tables(0).Rows(0).Item("CountDayType"))
            lblDayType.Text = Trim(objAttdCodeDs.Tables(0).Rows(0).Item("DayType"))
        end if

                    if Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working then 
                        tblSelection.Visible = false
                        dgLineDet.visible = true
                        SaveBtn.visible = false
                    elseif (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Others) and (Trim(lblDayType.Text) = objPRSetup.EnumDayType.Off) then
                        tblSelection.Visible = false
                        dgLineDet.visible = true
                        SaveBtn.visible = false
                    else
                        tblSelection.Visible = false
                        dgLineDet.visible = false
                        txtJanjangBruto.Text = 0
                        txtJanjangNetto.Text = 0
                        txtLoseFruit.Text = 0
                        txtHarvestInc.Text = 0
                        txtDendaQty.Text = 0   
                        SaveBtn.visible = true  
                    end if


        strSelectedDate = txtAttdDate.Text
        strSelectedEmpCode = Trim(ddlEmployee.SelectedItem.Value)

        onLoad_Display()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString() & "&redirect=")
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
        onSelect_Account_Click()
    End Sub

    Sub onSelect_Account_Click()
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strAcc As String = Request.Form("ddlAccount")
        Dim strPreBlk As String = Request.Form("ddlPreBlock")
        Dim strBlk As String
        Dim strSubBlk As String = Request.Form("ddlSubBlock")
        Dim strVeh As String = Request.Form("ddlVeh")
        Dim strVehExp As String = Request.Form("ddlVehExp")

        If blnUseBlk = True Then
            strBlk = Request.Form("ddlBlock")
        Else
            strBlk = Request.Form("ddlSubBlock")
        End If

        GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

    End Sub


    Sub InsertRecord(ByRef pr_blnIsUpdated As Boolean)
        Dim objAttdId As String
        Dim strOpCdAdd As String = "PR_CLSTRX_ATTENDANCETRX_ADD"
        Dim strOpCdUpd As String = "PR_CLSTRX_ATTENDANCETRX_UPD"
        Dim strOpCd As String
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strAttdDate As String
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim dtCurrDate As Date

        pr_blnIsUpdated = False

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtAttdDate.Text, _
                                       objFormatDate, _
                                       objActualDate) = False Then
            lblErrAttdDate.Text = lblErrAttdDateDesc.Text & objFormatDate
            lblErrAttdDate.Visible = True
            Exit Sub
        Else
            strAttdDate = objActualDate
            dtCurrDate = objActualDate
            strSelectedDate = objGlobal.GetShortDate(strDateFmt, dtCurrDate.AddDays(1))
        End If


        strSelectedAttdId = Trim(attdid.Value)
        strSelectedEmpCode = strEmp
        strOpCd = IIf(intStatus = 0, strOpCdAdd, strOpCdUpd)


        strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRAttendanceTrx) & "|" & _
                   strSelectedAttdId & "|" & _
                   strAttdDate & "|" & _
                   strEmp & "|" & _
                   "|" & _
                   ddlAttendance.SelectedItem.Value & "|" & _
                   objPRTrx.EnumAttdTrxStatus.Active & "|" & _
                   "||" & _
                   objPRTrx.EnumAttdTrxType.PieceRated

        Try
            intErrNo = objPRTrx.mtdUpdAttendanceTrx(strOpCd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    False, _
                                                    objAttdId)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_pieceattd.aspx")
        End Try
        strSelectedAttdId = objAttdId
        attdid.Value = strSelectedAttdId
        pr_blnIsUpdated = True

    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strOpCode_AddLine As String = "PR_CLSTRX_ATTENDANCETRX_LINE_HARVESTER_ADD"
        Dim strOpCode_UpdSts As String = "PR_CLSTRX_ATTENDANCETRX_UPDSTS"
        Dim strOpCode_Attd As String = "PR_CLSSETUP_ATTENDANCE_LIST_GET_BY_ATTCODE"
        Dim strOpCode_TotalHour As String = "PR_CLSTRX_ATTENDANCETRX_LINE_TOTAL_HOUR_GET"
        Dim strOpCode_PaySetup As String = "PR_CLSSETUP_PAYROLLPROCESS_GET"
        Dim strOpCd As String = ""
        Dim strAcc As String = Trim(txtAccount.Text)
        Dim strPreBlk As String = ""
        Dim strBlk As String = ""
        Dim strSubBlk As String = ""
        Dim strVeh As String = Trim(txtVeh.Text)
        Dim strVehExp As String = Trim(txtVehExp.Text)
        Dim strHarvInc As String = Trim(ddlHarvInc.SelectedItem.Value)
        Dim strDenda As String = Trim(txtDendaCode.Text)
        Dim intLoseFruit As Double 
        Dim intDendaQty As Double
        Dim intOTHours As Double
        Dim blnIsUpdated As Boolean
        Dim strParam As String
        Dim intErrNo As Integer
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strOpCd_UpdateHA_TOT As String = "PR_CLSTRX_HAVESTER_TOTAL_UPD"
        Dim strRPParam As String
        Dim strBlkCode As String
        Dim strOpCd_UpdateHA_MD As String = "PR_CLSTRX_HAVESTER_MANDAY_UPD"

        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strAttdDate As String
        Dim dtCurrDate As Date
        Dim strTempDate As String
        Dim strDate As String

        Dim intActualHours As Double 
        Dim intAllowance As Double
        Dim intUnitWorked As Double
        Dim decPremi As Decimal

        lblErrValidation.Text = ""

        if Trim(txtHarvestInc.Text) = "" then
            decPremi = 0
        else
            decPremi = Cdbl(Trim(txtHarvestInc.Text))
        end if

        Dim strOpCdValidationBlock as String = "PR_CLSTRX_DAILYATTD_VALIDATION_BLKSUBBLK"
        Dim strOpCdValidationVeh as String = "PR_CLSTRX_DAILYATTD_VALIDATION_VEHICLE"
        Dim strOpCdValidationVehExp As String = "GL_CLSSETUP_VEHICLE_EXPENSE_CODE_GET_BY_VEHEXPENSECODE1"
        Dim objVehExpenseDs as object
        Dim strSearch as String
        Dim strSort as String



        strOpCd = strOpCode_AddLine & "|" & _
                  strOpCode_UpdSts & "|" & _
                  strOpCode_Attd & "|" & _
                  strOpCode_TotalHour & "|" & _
                  strOpCode_PaySetup


        If strAcc = "" and (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working) Then
            lblErrAccount.Visible = True
            Exit Sub
        End If

        If strHarvInc = "" Then
            lblErrHarvInc.Visible = True
            Exit Sub
        Else
            SearchPickerCategory(ddlHarvInc.SelectedItem.Value)        
        End IF

        If txtLoseFruit.Text = "" Then
            intLoseFruit = 0
        Else
            intLoseFruit = txtLoseFruit.Text
        End if 
        
        If txtDendaQty.Text = "" Then
            intDendaQty = 0
        Else
            intDendaQty = txtDendaQty.Text
        End If





        if (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Others) and (Trim(lblDayType.Text) = objPRSetup.EnumDayType.Off) then
            strAcc = ""
            strPreBlk  = ""
            strBlk  = ""
            strSubBlk  = ""
            strVeh  = ""
            strVehExp  = ""
            strHarvInc  = ""
            strDenda  = ""
        else
            If ddlChargeLevel.SelectedIndex = 0 Then
                strBlk = Trim(txtPreBlock.Text) 
                strSubBlk = ""
                strPreBlk = Trim(txtPreBlock.Text)
            else
                strBlk = "" 
                strSubBlk = Trim(txtPreBlock1.Text)
                strPreBlk = Trim(txtPreBlock1.Text)
            end if
            strVeh = trim(txtVeh.Text)
            strVehExp = trim(txtVehExp.Text)
        end if 



            if Trim(lblcountdaytype.text) <> "" then
                if Trim(lblcountdaytype.text) = Trim(objPRSetup.EnumCountDayType.Working) then 
                    GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

                    If ddlChargeLevel.SelectedIndex = 0 Then
                        If blnIsBlockRequire = True And strPreBlk = "" And blnIsBalanceSheet = False Then
                            lblErrValidation.Text = "<br>" & "Please fill Sub Block or Block Code !"
                            lblErrValidation.Visible = True
                            Exit Sub
                        ElseIf strPreBlk = "" And blnIsBalanceSheet = True And blnIsNurseryInd = True Then
                            lblErrValidation.Text = "<br>" & "Please fill Sub Block or Block Code !"
                            lblErrValidation.Visible = True
                            Exit Sub
                        End If
                    Else
                        If blnUseBlk = True Then
                            If blnIsBlockRequire = True And strBlk = "" And blnIsBalanceSheet = False Then
                                lblErrValidation.Text = "<br>" & "Please fill Sub Block or Block Code !"
                                lblErrValidation.Visible = True
                                Exit Sub
                            ElseIf strBlk = "" And blnIsBalanceSheet = True And blnIsNurseryInd = True Then
                                lblErrValidation.Text = "<br>" & "Please fill Sub Block or Block Code !"
                                lblErrValidation.Visible = True
                                Exit Sub
                            End If
                        Else
                            If blnIsBlockRequire = True And strSubBlk = "" And blnIsBalanceSheet = False Then
                                lblErrValidation.Text = "<br>" & "Please fill Sub Block or Block Code !"
                                lblErrValidation.Visible = True
                                Exit Sub
                            ElseIf strSubBlk = "" And blnIsBalanceSheet = True And blnIsNurseryInd = True Then
                                lblErrValidation.Text = "<br>" & "Please fill Sub Block or Block Code !"
                                lblErrValidation.Visible = True
                                Exit Sub
                            End If
                        End If
                    End If

                    If strVeh = "" And strVehExp <> "" And lblVehicleOption.Text = True Then
                        lblErrValidation.Text = "<br>" & "Please fill Vehicle Code !"
                        lblErrValidation.Visible = True
                        Exit Sub
                    ElseIf strVeh = "" And blnIsVehicleRequire = True Then 
                        lblErrValidation.Text = "<br>" & "Please fill Vehicle Code !"
                        lblErrValidation.Visible = True
                        Exit Sub
                    ElseIf strVehExp = "" And blnIsVehicleRequire = True Then 
                        lblErrValidation.Text = "<br>" & "Please fill Vehicle Expense Code !"
                        lblErrValidation.Visible = True
                        Exit Sub
                    ElseIf strVeh <> "" And strVehExp = "" And lblVehicleOption.Text = True Then
                        lblErrValidation.Text = "<br>" & "Please fill Vehicle Expense Code !"
                        lblErrValidation.Visible = True
                        Exit Sub
                    End If




 
                    if (Trim(txtPreBlock.Text) <> "" or Trim(txtPreBlock1.Text) <> "") and Trim(txtVeh.Text) = "" then 
                        if ddlChargelevel.SelectedIndex = 0 then 
                            strSearch = " Where a.AccCode = '" & Trim(txtAccount.Text) & "' and b.blkcode = '" & Trim(txtPreBlock.Text) & "' "
                            strSearch = strSearch & " and b.status = '1'  and a.loccode = '" & trim(strLocation) & "'"
                        else
                            strSearch = " Where a.AccCode = '" & Trim(txtAccount.Text) & "' and a.subblkcode = '" & Trim(txtPreBlock1.Text) & "'  "
                            strSearch = strSearch & " and b.status = '1'  and a.loccode = '" & trim(strLocation) & "'"
                        end if

                        strSort = " order by a.AccCode asc"
                        strParam = strSort & "|" & strSearch
                        Try
                            intErrNo = objPRSetup.mtdGetMasterList(strOpCdValidationBlock, strParam, 0, objAttdCodeDs)
                        Catch Exp As System.Exception
                            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
                        End Try

                        if objAttdCodeDs.Tables(0).Rows.Count = 0 then 
                            lblErrValidation.Text = "<br>" & "Account No and Tahun tanam/Block doesn't match !"
                            lblErrValidation.Visible = True
                            Exit Sub
                        else
                            lblErrValidation.Text = ""
                            lblErrValidation.Visible = false 
                        end if
                    end if


                    if (Trim(txtPreBlock.Text) = "" or Trim(txtPreBlock1.Text) = "") and Trim(txtVeh.Text) <> "" then 
                        lblErrAccount.Visible = false 

                        strSearch = " Where AccCode = '" & Trim(txtAccount.Text) & "' and vehcode = '" & Trim(txtVeh.Text) & "' "
                        strSearch = strSearch & " and status = '1' and loccode = '" & Trim(strLocation) & "'"

                        strSort = " order by vehcode asc"
                        strParam = strSort & "|" & strSearch
                        Try
                            intErrNo = objPRSetup.mtdGetMasterList(strOpCdValidationveh, strParam, 0, objAttdCodeDs)
                        Catch Exp As System.Exception
                            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
                        End Try

                        if objAttdCodeDs.Tables(0).Rows.Count = 0 then 
                            lblErrValidation.Text = "<br>" & "Account No and Vehicle Code doesn't match !"
                            lblErrValidation.Visible = True
                            Exit Sub
                        else
                            lblErrValidation.Text = ""
                            lblErrValidation.Visible = false
                        end if
                        if Trim(txtVehExp.Text) = "" then
                            lblErrValidation.Text = "<br>" & "Please fill Vehicle Expense Code !"
                            lblErrValidation.Visible = True 
                            Exit Sub      
                        else  
                            lblErrValidation.Text = ""
                            lblErrValidation.Visible = false
                        end if       
                    end if

                    if (Trim(txtPreBlock.Text) = "" or Trim(txtPreBlock1.Text) = "") and Trim(txtVehExp.Text) <> "" then 
                        strparam = Trim(txtVehExp.Text)
                        Try
                            intErrNo = objGLSetup.mtdGetVehExpCode(strOpCdValidationVehExp, _
                                                                strParam, _
                                                                objVehExpenseDs, _
                                                                True)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_EXPENSE_GET_BY_VEHEXPENSECODE&errmesg=" & lblErrMessage.Text & "&redirect=")
                        End Try
                        if objVehExpenseDs.Tables(0).Rows.Count = 0 then 
                            lblErrValidation.Text = "<br>" & "Cannot find the Vehicle Expense Code that you input !"
                            lblErrValidation.Visible = True
                            Exit Sub
                        else
                            lblErrValidation.Text = ""
                            lblErrValidation.Visible = false
                        end if                 
                    end if
                end if 
            end if 



            InsertRecord(blnIsUpdated)


            If blnIsUpdated = False Then
                Exit Sub
            Else


                strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRAttendanceTrxLn) & "||" & _
                            strSelectedAttdId & "|" & _
                            strAcc & "|" & _
                            strBlk & "|" & _
                            strSubBlk & "|" & _
                            strVeh & "|" & _
                            strVehExp & "|" & _
                            "0|" & _
                            "0|" & _
                            Trim(txtJanjangBruto.Text) & "|" & _
                            "1|" & _
                            Trim(strLocation) & "|" & _
                            strHarvInc & "|" & _
                            strDenda & "|" & _
                            intLoseFruit & "|" & _
                            "|" & _
                            intDendaQty & "|" & _
                            Trim(txtOTHours.Text) & "|" & _
                            decPremi & "|" & _
                            Trim(txtJanjangNetto.Text)



                   
                Try
                    If ddlChargeLevel.SelectedIndex = 0 Then


                        If blnIsVehicleRequire = False Then

                                    strParamList = Session("SS_LOCATION") & "|" & _
                                                Trim(strAcc) & "|" & _
                                                Trim(txtPreBlock.Text) & "|" & _
                                                objGLSetup.EnumBlockStatus.Active

                            
                                    intErrNo = objPRTrx.mtdUpdAttdTrxLineByBlock(strOpCodeGLSubBlkByBlk, _
                                                                                strParamList, _ 
                                                                                strOpCd, _
                                                                                strCompany, _
                                                                                strLocation, _
                                                                                strUserId, _
                                                                                strLocType, _
                                                                                strParam)

                            Else
                            intErrNo = objPRTrx.mtdUpdHarvesterAttdTrxLine(strOpCd, _
                                                                       strCompany, _
                                                                       strLocation, _
                                                                       strUserId, _
                                                                       strParam)
                            End If
                    Else


                      
                        intErrNo = objPRTrx.mtdUpdHarvesterAttdTrxLine(strOpCd, _
                                                                       strCompany, _
                                                                       strLocation, _
                                                                       strUserId, _
                                                                       strParam)

                    End If
                    
                    If intErrNo = 0 Then
                        If objGlobal.mtdValidInputDate(strDateFmt, _
                                                    txtAttdDate.Text, _
                                                    objFormatDate, _
                                                    objActualDate) = True Then
                            strAttdDate = objActualDate
                            dtCurrDate = objActualDate
                            strTempDate = objGlobal.GetShortDate(strDateFmt, dtCurrDate.AddDays(1))
                        End If

                        If blnUseBlk = False AND objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfig) then
                            intErrNo = objPDTrx.mtdUpdHarvesterBlock("PD_CLSTRX_HAVESTER_BLOCK_UPD", _
                                                                    strLocation, _
                                                                    strRPParam)
                        End if

                        strRPParam = "" & "|" & _
                                    objActualDate & "||" & _
                                    Iif(objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfig), "PR_LN.BlkCode", "PR_LN.SubBlkCode") & "|"

                        intErrNo = objPDTrx.mtdUpdHarvesterTotal(strOpCd_UpdateHA_TOT, _
                                                                strLocation, _
                                                                strRPParam)
                        intErrNo = objPDTrx.mtdUpdHarvesterManday(strOpCd_UpdateHA_MD, _
                                                                    strLocation, _
                                                                    strRPParam)
                    End If

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_pieceattd.aspx?attdid=" & strSelectedAttdId)
                End Try
            End If
            attdid.Value = strSelectedAttdId
            strSelectedPreBlk = strPreBlk
            If blnUseBlk = True Then
                strSelectedBlk = strBlk
            Else
                strSelectedBlk = strSubBlk
            End If
            strSelectedAcc = strAcc
            strSelectedVeh = strVeh
            strSelectedVehExp = strVehExp
    
            strSelectedDate = txtAttdDate.Text
            strSelectedEmpCode = ddlEmployee.SelectedItem.Value

            
            onLoad_Display()
            ddlharvinc.Enabled = false

    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdCancel As String = "PR_CLSTRX_ATTENDANCETRX_CANCEL"
        Dim strOpCdUpd As String = "PR_CLSTRX_ATTENDANCETRX_UPD"
        Dim objAttdId As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim intErrNo As Integer
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strAttdDate As String
        Dim blnIsUpdated As Boolean
        Dim strParam As String = ""
        Dim dtCurrDate As Date
        Dim strTempDate As String


        strEmp = IIf(strEmp = "", ddlEmployee.SelectedItem.Value, strEmp)

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtAttdDate.Text, _
                                       objFormatDate, _
                                       objActualDate) = False Then
            lblErrAttdDate.Text = lblErrAttdDateDesc.Text & objFormatDate
            lblErrAttdDate.Visible = True
            Exit Sub
        Else
            strAttdDate = objActualDate
            dtCurrDate = objActualDate
            strTempDate = objGlobal.GetShortDate(strDateFmt, dtCurrDate.AddDays(1))
        End If


        If strCmdArgs = "Save" Then
            BtnAddTemp()
        ElseIf strCmdArgs = "Cancel" Then
            strParam = strSelectedAttdId & "|" & objPRTrx.EnumAttdTrxStatus.Cancelled
            Try
                intErrNo = objPRTrx.mtdUpdAttendanceTrx(strOpCdCancel, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        True, _
                                                        objAttdId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_CANCEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_pieceattd.aspx?attdid=" & strSelectedAttdId)
            End Try

        ElseIf strCmdArgs = "Confirm" Then
            strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRAttendanceTrx) & "|" & _
                    strSelectedAttdId & "|" & _
                    strAttdDate & "|" & _
                    strEmp & "|" & _
                    "|" & _
                    ddlAttendance.SelectedItem.Value & "|" & _
                    objPRTrx.EnumAttdTrxStatus.Confirmed & "|" & _
                    "||" & _
                    objPRTrx.EnumAttdTrxType.PieceRated
            Try
                intErrNo = objPRTrx.mtdUpdAttendanceTrx(strOpCdUpd, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        False, _
                                                        objAttdId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_CONTRACTPAY_CLOSE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_pieceattd.aspx?attdid=" & strSelectedAttdId)
            End Try

            strSelectedDate = strTempDate
            strSelectedAttdId = objAttdId
            attdid.Value = strSelectedAttdId
        End If

        If strSelectedAttdId <> "" Then
            onLoad_Display()
        End If
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCdDelLn As String = "PR_CLSTRX_ATTENDANCETRX_LINE_DEL_BYATTID"
        Dim strOpCdUpdID As String = "PR_CLSTRX_ATTENDANCETRX_UPDSTS"
        Dim strOpCd As String = strOpCdDelLn & "|" & strOpCdUpdID & "|||"
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim strAcc As String = ""
        Dim strPreBlk As String = ""
        Dim strBlk As String = ""
        Dim strSubBlk As String = ""
        Dim strVeh As String = Trim(txtVeh.Text)
        Dim strVehExp As String = Trim(txtVehExp.Text)
        Dim strHarvInc As String = Request.Form("ddlHarvInc")
        Dim strDenda As String = Trim(txtDendaCode.Text)
        Dim intLoseFruit As Double 
        Dim intDendaQty As Double
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strParam As String
        Dim lblText As Label
        Dim strAttLnId As String
        Dim strChargeLocCode As String
        Dim intErrNo As Integer
        Dim intOTHours As Double

        Dim intActualHours As Double 
        Dim intAllowance As Double
        Dim intUnitWorked As Double

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblChargeLocCode")
        strChargeLocCode = lblText.Text
        lblText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAttLnId")
        strAttLnId = lblText.Text

        If txtLoseFruit.Text = "" Then
            intLoseFruit = 0
        Else
            intLoseFruit = txtLoseFruit.Text
        End if 
        
        If txtDendaQty.Text = "" Then
            intDendaQty = 0
        Else
            intDendaQty = txtDendaQty.Text
        End If



        Try




            strParam = "|" & strAttLnId & "|" & strSelectedAttdId & "||||||" & _                           
                        "0|" & _
                        "0|" & _
                        "0|" & _
                        "1|" & _
                        Trim(strLocation) & "|" & _
                        strHarvInc & "|" & _
                        strDenda & "|" & _
                        intLoseFruit & "|" & _
                        "|" & _
                        intDendaQty & "|" & _
                        "0|0|0"



        
            intErrNo = objPRTrx.mtdUpdHarvesterAttdTrxLine(strOpCd, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_LINE_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_pieceattd.aspx?attdid=" & strSelectedAttdId)
        End Try

        strSelectedEmpCode = strEmp
        strSelectedAcc = strAcc
        strSelectedPreBlk = strPreBlk
        If blnUseBlk = True Then
            strSelectedBlk = strBlk
        Else
            strSelectedBlk = strSubBlk
        End If
        strSelectedVeh = strVeh
        strSelectedVehExp = strVehExp

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtAttdDate.Text, _
                                       objFormatDate, _
                                       objActualDate) Then
            strSelectedDate = txtAttdDate.Text    
            onLoad_Display()
        End If
    End Sub


    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_AttdList.aspx")
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try

                lblPreBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & "/" & GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                lblPreBlock1.Text = GetCaption(objLangCap.EnumLangCap.Block) & "/" & GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=pr_trx_pieceattd_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=PR/trx/pr_trx_pieceattd.aspx")
        End Try

        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

        dgLineDet.Columns(1).HeaderText = lblAccount.Text
        dgLineDet.Columns(2).HeaderText = lblPreBlock1.Text
        dgLineDet.Columns(3).HeaderText = lblVehicle.Text
        dgLineDet.Columns(4).HeaderText = lblVehExpense.Text

        lblErrAccount.Text = lblErrSelect.Text & lblAccount.Text
        lblErrVeh.Text = lblErrSelect.Text & lblVehicle.Text
        lblErrVehExp.Text = lblErrSelect.Text & lblVehExpense.Text
        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlock.Text =  GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text 
        lblErrPreBlock.Text = lblErrSelect.Text & GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=pr_trx_pieceattd_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PR/trx/pr_trx_pieceattd.aspx")
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


    Sub BindHarvIncentive(ByVal pv_strHarvIncCode As String)
        Dim strOpCd As String = "PR_CLSSETUP_HARVINCENTIVE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By HarvIncCode ASC| Status = '" & objPRSetup.EnumHarvIncentiveStatus.Active & "' and loccode = '" & trim(strLocation)& "' "

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try            
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   0, _
                                                   objHarvIncDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_LIST_GET&errmesg=" & Exp.ToString & "&redirect=")
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
        Dim strOpCd As String = "PR_CLSSETUP_HARVINCENTIVE_GET1"
        Dim strParam As String = Trim(strLocation) & "|" & ddlHarvInc.SelectedItem.Value.Trim & "|||||"
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try
        
        If objResult.Tables(0).Rows.Count > 0 Then
            strDivLabour = objResult.Tables(0).Rows(0).Item("DivLabour")
            strPickerCategory = objResult.Tables(0).Rows(0).Item("PickerCategory")
            lblQuotaInQtyVal.Text = Trim(objResult.Tables(0).Rows(0).Item("QuotaQty"))

            If strDivLabour = objPRSetup.EnumDivisionLabour.NonDOLUnpaid Then
                txtLoseFruit.Enabled = False
            Else
                txtLoseFruit.Enabled = True
            End If    
        End If
    End Sub



    Sub SearchPickerCategory(Byval pv_strHarvInc As String)
        Dim strOpCd As String = "PR_CLSSETUP_HARVINCENTIVE_GET"
        Dim strParam As String = "|" & Trim(pv_strHarvInc) & "|||||"
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try
        
        If objResult.Tables(0).Rows.Count > 0 Then
            strDivLabour = objResult.Tables(0).Rows(0).Item("DivLabour")
            strPickerCategory = objResult.Tables(0).Rows(0).Item("PickerCategory")
        End If
    End Sub

    Function CheckLocBillParty () As Boolean
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strSearch As String
        Dim objBPLocDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strLocCode As String
        Dim objDS As New DataSet()

        strLocCode = TRIM(strLocation)
        
        If Not (strLocCode = "" Or strLocCode = TRIM(strLocation)) Then
            strSearch = " AND BP.Status = '" & objGLSetup.EnumBillPartyStatus.Active & "'" & _
                        " AND BP.InterLocCode = '" & strLocCode & "'" 
                
            Try
                intErrNo = objGLSetup.mtdGetBPInterLoc(strOpCd_Get, strSearch, objBPLocDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_SETUP_BILLPARTY_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objBPLocDs.Tables(0).Rows.Count <= 0 Then
                lblLocCodeErr.Text = strLocCode
                return False
            End If
        End If

        return True
    End Function

    Sub BtnAddTemp()
        Dim objResult As New Object()
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strOpCode_AddLine As String = "PR_CLSTRX_ATTENDANCETRX_LINE_HARVESTER_ADD"
        Dim strOpCode_UpdSts As String = "PR_CLSTRX_ATTENDANCETRX_UPDSTS"
        Dim strOpCode_Attd As String = "PR_CLSSETUP_ATTENDANCE_LIST_GET_BY_ATTCODE"
        Dim strOpCode_TotalHour As String = "PR_CLSTRX_ATTENDANCETRX_LINE_TOTAL_HOUR_GET"
        Dim strOpCode_PaySetup As String = "PR_CLSSETUP_PAYROLLPROCESS_GET"
        Dim strOpCd As String = ""
        Dim strAcc As String = Trim(txtAccount.Text)
        Dim strPreBlk As String = ""
        Dim strBlk As String = ""
        Dim strSubBlk As String = ""
        Dim strVeh As String = Trim(txtVeh.Text)
        Dim strVehExp As String = Trim(txtVehExp.Text)
        Dim strHarvInc As String = Request.Form("ddlHarvInc")
        Dim strDenda As String = Trim(txtDendaCode.Text)
        Dim intLoseFruit As Double 
        Dim intDendaQty As Double
        Dim intOTHours As Double
        Dim blnIsUpdated As Boolean
        Dim strParam As String
        Dim intErrNo As Integer
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strOpCd_UpdateHA_TOT As String = "PR_CLSTRX_HAVESTER_TOTAL_UPD"
        Dim strRPParam As String
        Dim strBlkCode As String
        Dim strOpCd_UpdateHA_MD As String = "PR_CLSTRX_HAVESTER_MANDAY_UPD"

        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strAttdDate As String
        Dim dtCurrDate As Date
        Dim strTempDate As String
        Dim strDate As String

        Dim intActualHours As Double 
        Dim intAllowance As Double
        Dim intUnitWorked As Double
        Dim decPremi As Decimal

        if Trim(txtHarvestInc.Text) = "" then
            decPremi = 0
        else
            decPremi = Cdbl(Trim(txtHarvestInc.Text))
        end if

        Dim strOpCdValidationBlock as String = "PR_CLSTRX_DAILYATTD_VALIDATION_BLKSUBBLK"
        Dim strOpCdValidationVeh as String = "PR_CLSTRX_DAILYATTD_VALIDATION_VEHICLE"
        Dim strOpCdValidationVehExp As String = "GL_CLSSETUP_VEHICLE_EXPENSE_CODE_GET_BY_VEHEXPENSECODE1"
        Dim objVehExpenseDs as object
        Dim strSearch as String
        Dim strSort as String

        if Trim(lblcountdaytype.text) = Trim(objPRSetup.EnumCountDayType.Working) then
            strDenda = Trim(txtDendaCode.Text)
            strHarvInc = Request.Form("ddlHarvInc")
            strVehExp = Trim(txtVehExp.Text)
            strVeh  = Trim(txtVeh.Text)
            strAcc  = Trim(txtVeh.Text)
            If ddlChargeLevel.SelectedIndex = 0 Then
                strBlk = Trim(txtPreBlock.Text) 
                strSubBlk = ""
            else
                strBlk = "" 
                strSubBlk = Trim(txtPreBlock1.Text)
            end if
        else
            strDenda = ""
            strHarvInc = ""
            strVehExp = ""
            strVeh = ""
            strAcc = ""
            strBlk = ""
            strSubBlk = ""
        end if





        strOpCd = strOpCode_AddLine & "|" & _
                  strOpCode_UpdSts & "|" & _
                  strOpCode_Attd & "|" & _
                  strOpCode_TotalHour & "|" & _
                  strOpCode_PaySetup


        GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

            if Trim(lblcountdaytype.text) <> "" then
            if Trim(lblcountdaytype.text) = Trim(objPRSetup.EnumCountDayType.Working) then  
                if (Trim(txtPreBlock.Text) <> ""  or Trim(txtPreBlock1.Text) <> "") and Trim(txtVeh.Text) = "" then 
                    if ddlChargelevel.SelectedIndex = 0 then 
                        strSearch = " Where a.AccCode = '" & Trim(txtAccount.Text) & "' and b.blkcode = '" & Trim(txtPreBlock.Text) & "' "
                        strSearch = strSearch & " and b.status = '1'  and a.loccode = '" & trim(strLocation) & "'"
                    else
                        strSearch = " Where a.AccCode = '" & Trim(txtAccount.Text) & "' and a.subblkcode = '" & Trim(txtPreBlock1.Text) & "'  "
                        strSearch = strSearch & " and b.status = '1'  and a.loccode = '" & trim(strLocation) & "'"
                    end if

                    strSort = " order by a.AccCode asc"
                    strParam = strSort & "|" & strSearch
                    Try
                        intErrNo = objPRSetup.mtdGetMasterList(strOpCdValidationBlock, strParam, 0, objAttdCodeDs)
                    Catch Exp As System.Exception
                        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
                    End Try

                    if objAttdCodeDs.Tables(0).Rows.Count = 0 then 
                        lblErrValidation.Text = "<br>" & "Account No and Tahun tanam/Block doesn't match !"
                        lblErrValidation.Visible = True
                        Exit Sub
                    else
                        lblErrValidation.Text = ""
                        lblErrValidation.Visible = false 
                    end if
                end if


                if (Trim(txtPreBlock.Text) = "" or Trim(txtPreBlock1.Text) = "") and Trim(txtVeh.Text) <> "" then 
                    lblErrAccount.Visible = false 

                    strSearch = " Where AccCode = '" & Trim(txtAccount.Text) & "' and vehcode = '" & Trim(txtVeh.Text) & "' "
                    strSearch = strSearch & " and status = '1' and loccode = '" & Trim(strLocation) & "'"

                    strSort = " order by vehcode asc"
                    strParam = strSort & "|" & strSearch
                    Try
                        intErrNo = objPRSetup.mtdGetMasterList(strOpCdValidationveh, strParam, 0, objAttdCodeDs)
                    Catch Exp As System.Exception
                        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
                    End Try

                    if objAttdCodeDs.Tables(0).Rows.Count = 0 then 
                        lblErrValidation.Text = "<br>" & "Account No and Vehicle Code doesn't match !"
                        lblErrValidation.Visible = True
                        Exit Sub
                    else
                        lblErrValidation.Text = ""
                        lblErrValidation.Visible = false
                    end if
                    if Trim(txtVehExp.Text) = "" then
                        lblErrValidation.Text = "<br>" & "Please fill Vehicle Expense Code !"
                        lblErrValidation.Visible = True 
                        Exit Sub      
                    else  
                        lblErrValidation.Text = ""
                        lblErrValidation.Visible = false
                    end if       
                end if

                if (Trim(txtPreBlock.Text) = "" or Trim(txtPreBlock1.Text) = "") and Trim(txtVehExp.Text) <> "" then 
                    strparam = Trim(txtVehExp.Text)
                    Try
                        intErrNo = objGLSetup.mtdGetVehExpCode(strOpCdValidationVehExp, _
                                                            strParam, _
                                                            objVehExpenseDs, _
                                                            True)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_EXPENSE_GET_BY_VEHEXPENSECODE&errmesg=" & lblErrMessage.Text & "&redirect=")
                    End Try
                    if objVehExpenseDs.Tables(0).Rows.Count = 0 then 
                        lblErrValidation.Text = "<br>" & "Cannot find the Vehicle Expense Code that you input !"
                        lblErrValidation.Visible = True
                        Exit Sub
                    else
                        lblErrValidation.Text = ""
                        lblErrValidation.Visible = false
                    end if                 
                end if
            end if 
            end if 




            InsertRecord(blnIsUpdated)


            If blnIsUpdated = False Then
                Exit Sub
            Else


                strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRAttendanceTrxLn) & "||" & _
                            strSelectedAttdId & "|" & _
                            strAcc & "|" & _
                            strBlk & "|" & _
                            strSubBlk & "|" & _
                            strVeh & "|" & _
                            strVehExp & "|" & _
                            "0|" & _
                            "0|" & _
                            Trim(txtJanjangBruto.Text) & "|" & _
                            "1|" & _
                            Trim(strLocation) & "|" & _
                            strHarvInc & "|" & _
                            strDenda & "|" & _
                            intLoseFruit & "|" & _
                            "|" & _
                            intDendaQty & "|" & _
                            "0|" & _
                            decPremi & "|" & _
                            Trim(txtJanjangNetto.Text)



                   
                Try
                    If ddlChargeLevel.SelectedIndex = 0 Then


                        If blnIsVehicleRequire = False Then

                                    strParamList = Session("SS_LOCATION") & "|" & _
                                                Trim(strAcc) & "|" & _
                                                Trim(txtPreBlock.Text) & "|" & _
                                                objGLSetup.EnumBlockStatus.Active

                            
                                    intErrNo = objPRTrx.mtdUpdAttdTrxLineByBlock(strOpCodeGLSubBlkByBlk, _
                                                                                strParamList, _ 
                                                                                strOpCd, _
                                                                                strCompany, _
                                                                                strLocation, _
                                                                                strUserId, _
                                                                                strLocType, _
                                                                                strParam)

                            Else
                            intErrNo = objPRTrx.mtdUpdHarvesterAttdTrxLine(strOpCd, _
                                                                       strCompany, _
                                                                       strLocation, _
                                                                       strUserId, _
                                                                       strParam)
                            End If
                    Else


                      
                        intErrNo = objPRTrx.mtdUpdHarvesterAttdTrxLine(strOpCd, _
                                                                       strCompany, _
                                                                       strLocation, _
                                                                       strUserId, _
                                                                       strParam)

                    End If
                    
                    If intErrNo = 0 Then
                        If objGlobal.mtdValidInputDate(strDateFmt, _
                                                    txtAttdDate.Text, _
                                                    objFormatDate, _
                                                    objActualDate) = True Then
                            strAttdDate = objActualDate
                            dtCurrDate = objActualDate
                            strTempDate = objGlobal.GetShortDate(strDateFmt, dtCurrDate.AddDays(1))
                        End If

                        If blnUseBlk = False AND objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfig) then
                            intErrNo = objPDTrx.mtdUpdHarvesterBlock("PD_CLSTRX_HAVESTER_BLOCK_UPD", _
                                                                    strLocation, _
                                                                    strRPParam)
                        End if

                        strRPParam = "" & "|" & _
                                    objActualDate & "||" & _
                                    Iif(objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfig), "PR_LN.BlkCode", "PR_LN.SubBlkCode") & "|"

                        intErrNo = objPDTrx.mtdUpdHarvesterTotal(strOpCd_UpdateHA_TOT, _
                                                                strLocation, _
                                                                strRPParam)
                        intErrNo = objPDTrx.mtdUpdHarvesterManday(strOpCd_UpdateHA_MD, _
                                                                    strLocation, _
                                                                    strRPParam)
                    End If

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_pieceattd.aspx?attdid=" & strSelectedAttdId)
                End Try
            End If
            attdid.Value = strSelectedAttdId
            strSelectedPreBlk = strPreBlk
            If blnUseBlk = True Then
                strSelectedBlk = strBlk
            Else
                strSelectedBlk = strSubBlk
            End If
            strSelectedAcc = strAcc
            strSelectedVeh = strVeh
            strSelectedVehExp = strVehExp
    
            strSelectedDate = txtAttdDate.Text
            strSelectedEmpCode = ddlEmployee.SelectedItem.Value

            
            onLoad_Display()
    End Sub

    Sub GetAccountDesc(ByVal strDescription as String)
        Dim _objAccDs As New Object()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = strDescription
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        if _objAccDs.Tables(0).Rows.Count > 0 then
            lblAccountDesc.Text = Trim(_objAccDs.Tables(0).Rows(0).Item("Description"))
        end if
        
    End Sub

    Sub GetPreBlockDesc(ByVal strDescription as String)
        Dim strOpCdBlock As String = "GL_CLSSETUP_BLOCK_GET_BY_BLKCODE"
        Dim strOpCdSubBlock As String = "GL_CLSSETUP_BLOCK_GET_BY_SUBBLKCODE"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim objSubBlkDs as Object

        If ddlChargeLevel.SelectedIndex = 0 Then
            strParam = Trim(txtPreBlock.Text) & "|||"

            Try
                intErrNo = objGLSetup.mtdGetBlock(strOpCdBlock, _
                                                strLocation, _
                                                strParam, _
                                                objBlkDs, _
                                                True)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_BLOCK_GET_BY_BLKCODE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_block.aspx")
            End Try
            if objBlkDs.Tables(0).Rows.Count > 0 then 
                lblPreBlockDesc.Text = Trim(objBlkDs.Tables(0).Rows(0).Item("Description"))
            else
                lblPreBlockDesc.Text = ""
            end if
        else        
            strParam = Trim(txtPreBlock1.Text) & "|||"
            Try
                intErrNo = objGLSetup.mtdGetSubBlock(strOpCdSubBlock, _
                                                    strLocation, _
                                                    strParam, _
                                                    objSubBlkDs, _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_GET_BY_SUBBLKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
            if objSubBlkDs.Tables(0).Rows.Count > 0 then 
                lblPreBlockDesc1.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("Description"))
            else
                lblPreBlockDesc1.Text = ""
            end if
        end if 
    End Sub

    Sub GetVehDesc(Byval strSelectedVehicleCode as string)
        Dim strOpCd As String = "GL_CLSSETUP_VEHICLE_GET_BY_VEHCODE"
        Dim strParam As String = strSelectedVehicleCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetVehicle(strOpCd, _
                                                strLocation, _
                                                strParam, _
                                                objVehDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_GET_BY_VEHCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        if objVehDs.Tables(0).Rows.Count > 0 then 
            lblVehDesc.Text = Trim(objVehDs.Tables(0).Rows(0).Item("Description"))
        else
            lblVehDesc.Text = ""
        end if
    End Sub

    Sub GetVehExpDesc(ByVal strSelectedVehExpenseCode as string)
        Dim strOpCd As String = "GL_CLSSETUP_VEHICLE_EXPENSE_CODE_GET_BY_VEHEXPENSECODE"
        Dim strParam As String = strSelectedVehExpenseCode        
        Dim intErrNo As Integer
        Dim objVehExpenseDs as object

        Try
            intErrNo = objGLSetup.mtdGetVehExpCode(strOpCd, _
                                                   strParam, _
                                                   objVehExpenseDs, _
                                                   True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_EXPENSE_GET_BY_VEHEXPENSECODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        if objVehExpenseDs.Tables(0).Rows.Count > 0 then 
            lblVehExpDesc.Text = Trim(objVehExpenseDs.Tables(0).Rows(0).Item("Description"))
        else
            lblVehExpDesc.Text = ""
        end if 
    End Sub


    Sub GetDendaDesc(Byval strSelectedVehicleCode as string)
        Dim strOpCd As String = "PR_CLSSETUP_DENDA_SEARCH"
        Dim dr As DataRow
        Dim strParam As String = "Order By D.DendaCode ASC|And D.Status = '" & objPRSetup.EnumDendaStatus.Active & "' And D.LocCode = '" & strLocation & "' and d.dendacode = '" & Trim(strSelectedVehicleCode) & "' "
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
        if objDendaDs.Tables(0).Rows.Count > 0 then 
            lblDendaDesc.Text = Trim(objDendaDs.Tables(0).Rows(0).Item("_Description"))
        else
            lblDendaDesc.Text = ""
        end if
    End Sub

End Class
