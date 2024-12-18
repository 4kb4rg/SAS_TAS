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


Public Class PR_trx_DailyAttd : Inherits Page

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents txtAttdDate As TextBox
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents ddlAttdCode As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents ddlGang As DropDownList
    Protected WithEvents txtOT As TextBox
    Protected WithEvents btnSelDate As Image
    Protected WithEvents lblAttdId As Label
    Protected WithEvents lblRemainHour As Label
    Protected WithEvents lblOTAllowed As Label  
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents lblTotalHour As Label          
    Protected WithEvents lblTotalVolume As Label        
    Protected WithEvents lblTotalHourWithOT As Label    
    Protected WithEvents lblTotalVolumeWithOT As Label  
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
    Protected WithEvents lblErrEmployee As Label
    Protected WithEvents lblErrAttdCode As Label
    Protected WithEvents lblErrTotal As Label
    Protected WithEvents lblErrGang As Label


    Protected WithEvents lblErrPreBlock As Label
    Protected WithEvents lblErrPreBlock1 As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrVeh As Label
    Protected WithEvents lblErrVehExp As Label
    Protected WithEvents lblPreBlock As Label
    Protected WithEvents lblPreBlock1 As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehExpense As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPayType As Label
    Protected WithEvents lblHasShift As Label
    Protected WithEvents lblOTInd As Label  
    Protected WithEvents lblQuotaLevel As Label
    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlock As HtmlTableRow
    Protected WithEvents RowPreBlock1 As HtmlTableRow
    Protected WithEvents RowBlock As HtmlTableRow

    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Protected WithEvents lblErrAnnualLeaveBalance As Label
    Protected WithEvents lblErrSickLeaveBalance As Label
    Protected WithEvents lblCountDayType As Label
    Protected WithEvents lblDayType As Label
    Protected WithEvents lblErrEmpValidation As Label
    Protected WithEvents lblErrNoAnnualLeave As Label
    Protected WithEvents lblErrNoSickLeave As Label
    Protected WithEvents txtHarvestInc As TextBox
    Protected WithEvents lblOTHours as Label

    Protected WithEvents txtAccount As TextBox
    Protected WithEvents txtPreBlock As TextBox
    Protected WithEvents txtPreBlock1 As TextBox
    Protected WithEvents txtVeh As TextBox
    Protected WithEvents txtVehExp As TextBox
    Protected WithEvents btnSearch1 As ImageButton
    Protected WithEvents btnSearch2 As ImageButton
    Protected WithEvents btnSearch3 As ImageButton
    Protected WithEvents btnSearch4 As ImageButton
    Protected WithEvents btnSearch5 As ImageButton
    Protected WithEvents lblAccountDesc as Label
    Protected WithEvents lblPreBlockDesc as Label
    Protected WithEvents lblPreBlockDesc1 as Label
    Protected WithEvents lblVehDesc as Label
    Protected WithEvents lblVehExpDesc as Label
    Protected WithEvents lblErrMessage as Label
    Protected WithEvents lblTotalOTHours as Label
    Protected WithEvents lblTotalPremi as Label
    Protected WithEvents lblErrValidation as Label
    Protected WithEvents lblFlagBindAttCode as Label


    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected objPRTrx As New agri.PR.clsTrx()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()

    Dim objAttdDs As New Object()
    Dim objAttdLnDs As New Object()
    Dim objEmpDs As New Object()
    Dim objAttdCodeDs As New Object()
    Dim objAccDs As New Object()
    Dim objBlkDs As New Object()
    Dim objVehDs As New Object()
    Dim objVehExpDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objGangDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFmt As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strCostLevel As String
    Dim blnUseBlk As Boolean = False

    Dim strSelectedAcc As String = ""
    Dim strSelectedPreBlk As String = ""
    Dim strSelectedBlk As String = ""
    Dim strSelectedVeh As String = ""
    Dim strSelectedVehExp As String = ""    
    Dim dblTotalHour As Double
    Dim dblADHours As Double
    
    Dim strSelectedAttdId As String = ""
    Dim strSelectedEmpCode As String = ""
    Dim strSelectedDate As String = ""    
    Dim intStatus As Integer
    Dim strGangCode As String = ""
    Dim PreBlockTag As String
    Dim BlockTag As String
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Protected WithEvents lblBPErr As Label
    Protected WithEvents lblLocCodeErr As Label


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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDailyAttd), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            dgLineDet.Visible = True
            lblRfvAttdDate.Visible = False
            lblErrAttdDate.Visible = False
            lblErrAttdDateDesc.Visible = False
            lblErrEmployee.Visible = False
            lblErrAttdCode.Visible = False
            lblErrPreBlock.Visible = False
            lblErrVeh.Visible = False
            lblErrVehExp.Visible = False
            lblErrTotal.Visible = False
            lblFlagBindAttCode.Text = "0"

            lblErrAnnualLeaveBalance.Visible = False
            lblErrSickLeaveBalance.Visible = False
            lblErrEmpValidation.Visible = False
            lblErrNoSickLeave.Visible = False
            lblErrNoAnnualLeave.Visible = False


            strSelectedAttdId = Trim(IIf(Request.QueryString("attdid") <> "", Request.QueryString("attdid"), Request.Form("attdid")))
            strGangCode = Request.QueryString("gangcode")
            if Trim(strGangCode) <> "" then
                strGangCode = Trim(strLocation) & Request.QueryString("gangcode")
            end if
            strSelectedEmpCode = Trim(Request.QueryString("empcode"))
            strSelectedDate = objGlobal.GetShortDate(strDateFmt, Trim(Request.QueryString("date")))
            intStatus = Convert.ToInt16(lblHiddenSts.Text)
            If LCase(strCostLevel) = "block" Then 
                blnUseBlk = True
            End If
            
            onload_GetLangCap()
            
            lblAccountDesc.Text = ""
            lblPreBlockDesc.Text = ""
            lblVehDesc.Text = ""
            lblVehExpDesc.Text = ""

    
            If Not IsPostBack Then
                BindChargeLevelDropDownList()
                If strSelectedAttdId = "" And (strSelectedEmpCode = "" Or strSelectedDate = "") Then
                    txtAttdDate.Text = objGlobal.GetShortDate(strDateFmt, Now())                    
                    BindEmployee("",strSelectedEmpCode)
                    BindGangCode(strGangCode)
                    BindAttdCode("", "")
                    onLoad_BindButton()
                Else
                    onLoad_Display()
                    onLoad_BindButton()
                End If
            End If

            lblBPErr.Visible = False
            lblLocCodeErr.Visible = False
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCdGet As String = "PR_CLSTRX_ATTENDANCETRX_ATTDATE_GET"
        Dim objValidFormat As String
        Dim objValidDate As String
        Dim strParam As String
        Dim intErrNo As Integer
       
        If Trim(strSelectedDate) = "" Then
            lblRfvAttdDate.Visible = True
            Exit Sub
        Else
            If objGlobal.mtdValidInputDate(strDateFmt, strSelectedDate, objValidFormat, objValidDate) = False Then
                lblErrAttdDate.Text = lblErrAttdDateDesc.Text & objValidFormat
                lblErrAttdDate.Visible = True
                Exit Sub
            End If
        End If

        If Trim(strSelectedEmpCode) <> "" Then
            GetEmpPayrollDetails(strSelectedEmpCode)
            strParam = "and trx.EmpCode = '" & Trim(strSelectedEmpCode) & _
                        "' and trx.AttDate = '" & objValidDate & _
                        "' and trx.Status in ('" & objPRTrx.EnumAttdTrxStatus.Active & _
                        "','" & objPRTrx.EnumAttdTrxStatus.Confirmed & "','" & objPRTrx.EnumAttdTrxStatus.Closed & "') "
            Try
                intErrNo = objPRTrx.mtdGetAttendanceTrx(strOpCdGet, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        objAttdDs, _
                                                        True)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            If objAttdDs.Tables(0).Rows.Count > 0 Then
                strSelectedAttdId = objAttdDs.Tables(0).Rows(0).Item("AttId").Trim()
                attdid.Value = objAttdDs.Tables(0).Rows(0).Item("AttId").Trim()
                lblAttdId.Text = objAttdDs.Tables(0).Rows(0).Item("AttId").Trim()
                txtAttdDate.Text = objGlobal.GetShortDate(strDateFmt, objAttdDs.Tables(0).Rows(0).Item("AttDate"))
                lblPeriod.Text = objAttdDs.Tables(0).Rows(0).Item("AccMonth").Trim() & "/" & objAttdDs.Tables(0).Rows(0).Item("AccYear").Trim()
                intStatus = Convert.ToInt16(objAttdDs.Tables(0).Rows(0).Item("Status"))
                lblHiddenSts.Text = objAttdDs.Tables(0).Rows(0).Item("Status").Trim()
                lblStatus.Text = objPRTrx.mtdGetAttdTrxStatus(objAttdDs.Tables(0).Rows(0).Item("Status").Trim())
                lblDateCreated.Text = objGlobal.GetLongDate(objAttdDs.Tables(0).Rows(0).Item("CreateDate"))
                lblLastUpdate.Text = objGlobal.GetLongDate(objAttdDs.Tables(0).Rows(0).Item("UpdateDate"))
                lblUpdatedBy.Text = objAttdDs.Tables(0).Rows(0).Item("UserName").Trim()
                lblPayType.Text = objAttdDs.Tables(0).Rows(0).Item("PayType").Trim()
                lblTotalHour.Text = FormatNumber(objAttdDs.Tables(0).Rows(0).Item("TotalHours"), 2, True, False, False)
                dblTotalHour = Convert.ToDouble(lblTotalHour.Text)
                lblTotalVolume.Text = FormatNumber(objAttdDs.Tables(0).Rows(0).Item("TotalVolume"), 2, True, False, False)

                if objAttdDs.Tables(0).Rows(0).Item("Status").Trim() = objPRTrx.EnumAttdTrxStatus.Confirmed then
                    tblSelection.Visible = false
                else
                    tblSelection.Visible = true
                end if
                BindGangCode(objAttdDs.Tables(0).Rows(0).Item("GangCode").Trim())
    
                If lblHiddenSts.Text = objPRTrx.EnumAttdTrxStatus.Confirmed Then
                    BindEmployee("",objAttdDs.Tables(0).Rows(0).Item("EmpName").Trim() & "|" & objAttdDs.Tables(0).Rows(0).Item("EmpCode").Trim())
                Else
                    BindEmployee(objAttdDs.Tables(0).Rows(0).Item("GangCode").Trim(),objAttdDs.Tables(0).Rows(0).Item("EmpCode").Trim())
                End If
                BindAttdCode(objAttdDs.Tables(0).Rows(0).Item("AttCode").Trim(), objAttdDs.Tables(0).Rows(0).Item("PayType").Trim())
                onSelect_Account_Click()
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
                BindGangCode(strGangCode)
                BindEmployee(strGangCode, strSelectedEmpCode)
                if lblFlagBindAttCode.Text = "0" then
                    BindAttdCode("", "1")
                end if 
                ddlAttdCode.Enabled = True
            End If
        Else
            lblPayType.Text = "0"
            BindGangCode("")
        End If
    End Sub

    Sub onLoad_DisplayLine()
        Dim strOpCd As String = "PR_CLSTRX_ATTENDANCETRX_LINE_GET"
        Dim strParam As String = strSelectedAttdId
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim decTotalPremi As Decimal = 0 
        Dim decTotalOTHours As Decimal = 0
        Dim strCOA as String
        
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_LINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objAttdLnDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objAttdLnDs.Tables(0).Rows.Count - 1
                If blnUseBlk = True Then
                    objAttdLnDs.Tables(0).Rows(intCnt).Item("BlkCode") = objAttdLnDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
                Else
                    objAttdLnDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = objAttdLnDs.Tables(0).Rows(intCnt).Item("SubBlkCode").Trim()
                End If
                decTotalOTHours += objAttdLnDs.Tables(0).Rows(intCnt).Item("OTHours")
                decTotalPremi += objAttdLnDs.Tables(0).Rows(intCnt).Item("HarvestInc")
                strCOA = Trim(objAttdLnDs.Tables(0).Rows(intCnt).Item("Acccode"))           
            Next
            ddlAttdCode.Enabled = False
            lblTotalOTHours.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decTotalOTHours,2)
            lblTotalPremi.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decTotalPremi,2)

        Else
            If intStatus = objPRTrx.EnumAttdTrxStatus.Confirmed Then
                ddlAttdCode.Enabled = False
            Else
                ddlAttdCode.Enabled = True
            End If
        End If


        dgLineDet.DataSource = objAttdLnDs.Tables(0)
        dgLineDet.DataBind()

        If intStatus = objPRTrx.EnumAttdTrxStatus.Active Then
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

    End Sub

    Sub onLoad_BindButton()
        btnSelDate.Visible = False
        RefreshBtn.Visible = False
        txtAttdDate.Enabled = False
        ddlGang.Enabled = False
        ddlEmployee.Enabled = False
        SaveBtn.Visible = False
        ConfirmBtn.Visible = False
        CancelBtn.Visible = False
        tblSelection.Visible = False
        btnFind1.Disabled = False

        Select Case intStatus
            Case objPRTrx.EnumAttdTrxStatus.Active
                btnSelDate.Visible = True
                RefreshBtn.Visible = True
                txtAttdDate.Enabled = True
                ddlEmployee.Enabled = False
                SaveBtn.Visible = True
                ConfirmBtn.Visible = True
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
                ddlGang.Enabled = True
                ddlEmployee.Enabled = True
                btnSelDate.Visible = True
                RefreshBtn.Visible = True
                txtAttdDate.Enabled = True
                SaveBtn.Visible = True
                ConfirmBtn.Visible = True
                dgLineDet.DataBind()
        End Select

    End Sub

    Sub BindGangCode(ByVal pv_strGangCode As String)
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


    Sub BindEmployee(ByVal pv_strGangCode As String, Byval pv_strEmpCode As String)
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
                                            txtAttdDate.Text, _
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
                                objEmpDs.Tables(0).Rows(intCnt).Item("GangMember")  = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("GangMember"))
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
                    ddlEmployee.Items.Add(New ListItem("Select Employee Code",""))
                End If
            Else
                Try
                    strParam = "|||" & objHRTrx.EnumEmpStatus.active & "|" & strLocation & "|Mst.EmpCode|ASC"

                    intErrNo = objHRTrx.mtdGetEmployeeList(strOpCdEmployee_Get, strParam, objEmpDs)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & Exp.Message & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
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



    Sub BindAttdCode(ByVal pv_strAttdCode As String, ByVal pv_strPayType As String)
        Dim strOpCd As String = "PR_CLSTRX_DAILYATTENDANCE_ATTDANCE_GET"
        Dim dr As DataRow
        Dim strParam As String
        Dim strSort As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        strSearch = "and atd.Status = '" & objPRSetup.EnumAttStatus.Active & "' "
        strSort = "order by atd.AttCode asc"

        strParam = strSort & "|" & strSearch
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objAttdCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If pv_strAttdCode <> "" Then
            For intCnt = 0 To objAttdCodeDs.Tables(0).Rows.Count - 1
                If Trim(objAttdCodeDs.Tables(0).Rows(intCnt).Item("AttCode")) = Trim(pv_strAttdCode) Then
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
        dr("OTAllowed") = "1"
        objAttdCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        
        dblADHours = objAttdCodeDs.Tables(0).Rows(intSelectedIndex).Item("Hours")
        lblRemainHour.Text = objAttdCodeDs.Tables(0).Rows(intSelectedIndex).Item("Hours")
        lblOTAllowed.Text = objAttdCodeDs.Tables(0).Rows(intSelectedIndex).Item("OTAllowed")

        ddlAttdCode.DataSource = objAttdCodeDs.Tables(0)
        ddlAttdCode.DataValueField = "AttCode"
        ddlAttdCode.DataTextField = "_Description"
        ddlAttdCode.DataBind()
        ddlAttdCode.SelectedIndex = intSelectedIndex
        ddlAttdCode.AutoPostBack = True

        lblCountDayType.Text = Trim(objAttdCodeDs.Tables(0).Rows(intSelectedIndex).Item("CountDayType").ToString)        
        lblDayType.Text = Trim(objAttdCodeDs.Tables(0).Rows(intSelectedIndex).Item("DayType").ToString)        
        
    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.selectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub

    Sub onClick_Refresh(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        lblAttdId.text = ""
        strSelectedDate = txtAttdDate.Text
        strGangCode = ddlGang.SelectedItem.Value
        strSelectedEmpCode = ddlEmployee.SelectedItem.Value
        BindAttdCode("", "1")
        onLoad_Display()
        onLoad_BindButton()
    End Sub


    Sub onChg_GangCode(ByVal Sender As Object, ByVal E As EventArgs)
        strSelectedDate = txtAttdDate.Text
        strGangCode = ddlGang.SelectedItem.Value        
        strSelectedEmpCode = ddlEmployee.SelectedItem.Value

        BindEmployee(strGangCode,"")
    End Sub

    Sub onSelect_ChangeLevel(ByVal Sender As Object, ByVal E As EventArgs)
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


    Sub GetEmpPayrollDetails(ByVal pv_strEmpCode As String)
        Dim strOpCd As String = "HR_CLSTRX_PAYROLL_GET"
        Dim objPayDs As New DataSet()
        Dim strParam As String
        Dim intErrNo As Integer

        lblPayType.Text = "0"
        strParam = pv_strEmpCode & "|||EmpCode|ASC"
        Try
            intErrNo = objHRTrx.mtdGetEmployeePay(strOpCd, strParam, objPayDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_PAYROLL_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objPayDs.Tables(0).Rows.Count > 0 Then
            lblOTInd.Text = objPayDs.Tables(0).Rows(0).Item("OTInd").Trim()
            lblPayType.Text = objPayDs.Tables(0).Rows(0).Item("PayType").Trim()
            lblQuotaLevel.Text = objPayDs.Tables(0).Rows(0).Item("QuotaLevel").Trim()
        Else
            lblOTInd.Text = "0"
            lblPayType.Text = "1"
            lblQuotaLevel.Text = "1"
        End If
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
        strSearch = strSearch & " and atd.attcode = '" & Trim(ddlAttdCode.SelectedItem.Value) & "' "

        strSort = "order by atd.AttCode asc"

        strParam = strSort & "|" & strSearch
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objAttdCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objAttdCodeDs.Tables(0).Rows.Count > 0 Then
            lblCountDayType.Text = Trim(objAttdCodeDs.Tables(0).Rows(0).Item("CountDayType"))
            lblDayType.Text = Trim(objAttdCodeDs.Tables(0).Rows(0).Item("DayType"))
        End If

        If (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Others) And (Trim(lblDayType.Text) = objPRSetup.EnumDayType.Off) Then
            tblSelection.Visible = True
            dgLineDet.Visible = True
            SaveBtn.Visible = False
            txtAccount.Enabled = False
            txtPreBlock.Enabled = False
            txtPreBlock1.Enabled = False
            txtVeh.Enabled = False
            txtVehExp.Enabled = False
            txtAccount.Text = ""
            txtPreBlock.Text = ""
            txtPreBlock1.Text = ""
            txtVeh.Text = ""
            txtVehExp.Text = ""
            lblAccountDesc.Text = ""
            lblPreBlockDesc.Text = ""
            lblPreBlockDesc1.Text = ""
        ElseIf (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working) Then
            tblSelection.Visible = True
            dgLineDet.Visible = True
            SaveBtn.Visible = False
            txtAccount.Enabled = True
            txtPreBlock.Enabled = True
            txtPreBlock1.Enabled = True
            txtVeh.Enabled = True
            txtVehExp.Enabled = True
        Else
            tblSelection.Visible = False
            dgLineDet.Visible = True
            SaveBtn.Visible = True
        End If

        lblTotalPremi.Text = "0"
        lblTotalOTHours.Text = "0"
        lblTotalHour.Text = trim(objAttdCodeDs.Tables(0).Rows(0).Item("Hours"))
        strSelectedDate = txtAttdDate.Text
        strSelectedEmpCode = Trim(ddlEmployee.SelectedItem.Value)
        strGangCode = Trim(ddlGang.SelectedItem.Value)

        lblFlagBindAttCode.Text = "1"
        'onLoad_Display()
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString() & "&redirect=")
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
        onSelect_Account_Click()
    End Sub

    Sub onSelect_Account_Click()
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strAcc As String = ""
        Dim strPreBlk As String = ""
        Dim strBlk As String = ""
        Dim strSubBlk As String = ""
        Dim strVeh As String = ""
        Dim strVehExp As String = ""

        GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

    End Sub

    Sub UpdateHeader(ByVal pv_strAction As String, ByRef pr_blnIsUpdated As Boolean)
        Dim strOpCd_Add As String = "PR_CLSTRX_ATTENDANCETRX_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_ATTENDANCETRX_UPD"
        Dim strOpCd As String
        Dim objAttdId As String
        Dim strEmpCode As String = ddlEmployee.SelectedItem.Value
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strAttdDate As String
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim dtCurrDate As Date
        Dim strNextDate As String
        Dim strStatus As String
        Dim strAcc as String = ""

        pr_blnIsUpdated = True
        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtAttdDate.Text, _
                                       objFormatDate, _
                                       objActualDate) = False Then
            lblErrAttdDate.Text = lblErrAttdDateDesc.Text & objFormatDate
            lblErrAttdDate.Visible = True
            pr_blnIsUpdated = False
            Exit Sub
        Else
            strAttdDate = objActualDate
            dtCurrDate = objActualDate
            strNextDate = objGlobal.GetShortDate(strDateFmt, dtCurrDate.AddDays(1))
        End If

        If strEmpCode = "" Then
            lblErrEmployee.Visible = True
            pr_blnIsUpdated = False
            Exit Sub
        ElseIf ddlAttdCode.SelectedItem.Value = "" Then
            lblErrAttdCode.Visible = True
            pr_blnIsUpdated = False
            Exit Sub
        End If

        strSelectedAttdId = Trim(attdid.Value)
        strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)

        If intStatus = 0 Then
            strOpCd = strOpCd_Add
            If Trim(pv_strAction) = "Save" Then
                strStatus = objPRTrx.EnumAttdTrxStatus.Active
            Else
                strStatus = objPRTrx.EnumAttdTrxStatus.Confirmed
            End If
        Else
            strOpCd = strOpCd_Upd
            strStatus = objPRTrx.EnumAttdTrxStatus.Active
        End If



        strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRAttendanceTrx) & "|" & _
                   strSelectedAttdId & "|" & _
                   strAttdDate & "|" & _
                   strEmpCode & "|" & _
                   "1" & "|" & _
                   ddlAttdCode.SelectedItem.Value & "|" & _
                   strStatus & "|" & _
                   "|" & _
                   ddlGang.SelectedItem.Value & "|" & _
                   objPRSetup.EnumPayType.DailyRate 
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_dailyattd.aspx")
        End Try
        strSelectedAttdId = objAttdId
        attdid.Value = strSelectedAttdId
        strSelectedEmpCode = strEmpCode

        If Trim(pv_strAction) = "Save" Then
            strSelectedDate = strNextDate
        Else
            strSelectedDate = txtAttdDate.Text
        End If

        If Trim(pv_strAction) = "Confirm" Then
            If lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Annual Or lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Sick Then
                UpdEmpLeaveBalance("Confirm")
            End If
        End If
    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSTRX_ATTENDANCETRX_ADD"
        Dim strOpCd_Cancel As String = "PR_CLSTRX_ATTENDANCETRX_CANCEL"
        Dim objAttdId As String
        Dim intErrNo As Integer
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim blnIsUpdated As Boolean
        Dim strParam As String = ""


        If strCmdArgs = "Save" Then 
            BtnAddTemp_Click()   
        
        ElseIf strCmdArgs = "Cancel" Then
            If ddlAttdCode.SelectedItem.Value = "" Then
                lblErrAttdCode.Visible = True
                Exit Sub
            End If
            strParam = strSelectedAttdId & "|" & objPRTrx.EnumAttdTrxStatus.Cancelled
            Try
                intErrNo = objPRTrx.mtdUpdAttendanceTrx(strOpCd_Cancel, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        True, _
                                                        objAttdId)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_CANCEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_dailyattd.aspx?attdid=" & strSelectedAttdId)
            End Try
            strSelectedDate = txtAttdDate.Text
            strSelectedEmpCode = ddlEmployee.SelectedItem.Value

            If lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Annual Or lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Sick Then
                    UpdEmpLeaveBalance("Cancel")
            End If

        ElseIf strCmdArgs = "Confirm" Then
            If lblAttdId.Text = "" Then
                UpdateHeader("Confirm", blnIsUpdated)
                If blnIsUpdated = False Then
                    Exit Sub
                End If
            Else
                If ddlAttdCode.SelectedItem.Value = "" Then
                    lblErrAttdCode.Visible = True
                    Exit Sub
                End If
                strParam = strSelectedAttdId & "|" & objPRTrx.EnumAttdTrxStatus.Confirmed
                Try
                    intErrNo = objPRTrx.mtdUpdAttendanceTrx(strOpCd_Cancel, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            strParam, _
                                                            True, _
                                                            objAttdId)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_DAILYATTD_CONFIRM&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_dailyattd.aspx?attdid=" & strSelectedAttdId)
                End Try
                strSelectedDate = txtAttdDate.Text
                strSelectedEmpCode = ddlEmployee.SelectedItem.Value
                If lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Annual Or lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Sick Then
                    UpdEmpLeaveBalance("Confirm")
               End If
        End If
        If attdid.Value <> "" Then
            onLoad_Display()
            onLoad_BindButton()
        End If
        End If
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdAddLine As String = "PR_CLSTRX_ATTENDANCETRX_LINE_ADD"
        Dim strOpCdUpdHeader As String = "PR_CLSTRX_ATTENDANCETRX_UPDSTS"
        
        Dim strOpCdValidationBlock as String = "PR_CLSTRX_DAILYATTD_VALIDATION_BLKSUBBLK"
        Dim strOpCdValidationVeh as String = "PR_CLSTRX_DAILYATTD_VALIDATION_VEHICLE"
        Dim strOpCdValidationVehExp As String = "GL_CLSSETUP_VEHICLE_EXPENSE_CODE_GET_BY_VEHEXPENSECODE1"
        Dim objVehExpenseDs as object
        Dim strSearch as String
        Dim strSort as String


        Dim strOpCodes As String = strOpCdAddLine & "|" & strOpCdUpdHeader
        Dim strOpCdGetSubBlkByBlk As String
        Dim strParamList As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcc As String = ""
        Dim strPreBlk As String = ""
        Dim strBlk As String = ""
        Dim strSubBlk As String = ""
        Dim strVeh As String = Trim(txtVeh.Text)
        Dim strVehExp As String = Trim(txtVehExp.Text)
        Dim blnIsUpdated As Boolean = True
        Dim decHour As Decimal
        Dim decVolume As Decimal

        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strChargeLocCode = ""
        Dim decPremi As Decimal = Cdbl(Trim(txtHarvestInc.Text))

        lblErrValidation.Text = ""
        lblErrValidation.Visible = False


            if Trim(lblcountdaytype.text) = Trim(objPRSetup.EnumCountDayType.Working) then
                strAcc = Trim(txtAccount.Text)
            else
                strAcc = ""
            end if
            If (strAcc = "") and (Trim(lblcountdaytype.text) = Trim(objPRSetup.EnumCountDayType.Working)) Then
                lblErrAccount.Visible = True
                Exit Sub
            Else
                lblErrAccount.Visible = false
            End If

         If ddlChargeLevel.SelectedIndex = 0 Then
            strBlk = Trim(txtPreBlock.Text) 
            strSubBlk = ""
            strPreBlk = Trim(txtPreBlock.Text)
         else
            strBlk = "" 
            strSubBlk = Trim(txtPreBlock1.Text)
            strPreBlk = Trim(txtPreBlock1.Text)
         end if


        

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
                        lblErrAccount.Visible = false
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

            If strSelectedAttdId = "" Then

                If ddlAttdCode.SelectedItem.Value = "" Then    
                    lblErrAttdCode.Visible = True
                Exit Sub        
                End If
                If lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Annual Or lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Sick Then
                    ValidationEmployee(ddlEmployee.SelectedItem.Value.ToString)
                    If lblErrEmpValidation.Visible = True Then
                        Exit Sub
                    End If 
                    CheckLeaveBalance()
                    If lblErrAnnualLeaveBalance.Visible = True Or lblErrSickLeaveBalance.Visible = True      
                        Exit Sub
                    End If  
                    If lblErrNoAnnualLeave.Visible = True Or lblErrNoSickLeave.Visible = True      
                        Exit Sub
                    End If  
                End If   

                UpdateHeader("Save", blnIsUpdated)
            End If

            If blnIsUpdated = False Then
                Exit Sub
            Else
                Try


                   
                    strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRAttendanceTrxLn) & "||" & _
                               strSelectedAttdId & "|" & _
                               Trim(txtAccount.Text) & "|" & _
                               strBlk & "|" & _
                               strSubBlk & "|" & _
                               Trim(txtVeh.Text) & "|" & _
                               Trim(txtVehExp.Text) & "|" & _
                               lblTotalHour.Text & "|" & _
                               "0" & "|" & _
                               "0" & "|" & _
                               "1" & "|" & _
                               strChargeLocCode & "|" & _
                               Cdbl(lblTotalHour.Text) & "|0|0||0|" & _
                               Cdbl(Trim(txtOT.Text)) & "|" & _
                               decPremi & "|0"
                                

                    If ddlChargeLevel.SelectedIndex = 0 And RowPreBlock.Visible = True Then
                        If blnIsVehicleRequire = False Then
                                If lblQuotaLevel.Text = objHRTrx.EnumQuotaLevel.Block Then
                                    strOpCdGetSubBlkByBlk = "PR_CLSTRX_DAILYATTD_GET_SUBBLK_BY_BLK"
                                Else
                                    strOpCdGetSubBlkByBlk = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
                                End If

                                strParamList = Session("SS_LOCATION") & "|" & _
                                            Trim(strAcc) & "|" & _
                                            Trim(txtPreBlock.Text) & "|" & _
                                            objGLSetup.EnumBlockStatus.Active

                                intErrNo = objPRTrx.mtdUpdAttdTrxLineByBlock(strOpCdGetSubBlkByBlk, _
                                                                            strParamList, _
                                                                            strOpCodes, _
                                                                            strCompany, _
                                                                            strLocation, _
                                                                            strUserId, _
                                                                            strLocType, _
                                                                            strParam)

                        Else
                                intErrNo = objPRTrx.mtdUpdAttdTrxLine(strOpCodes, _
                                                                        strCompany, _
                                                                        strLocation, _
                                                                        strUserId, _
                                                                        strParam)
                        End If
                    
                    Else
                        intErrNo = objPRTrx.mtdUpdAttdTrxLine(strOpCodes, _
                                                              strCompany, _
                                                              strLocation, _
                                                              strUserId, _
                                                              strParam)
                    End If
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_dailyattd.aspx?attdid=" & strSelectedAttdId)
                End Try

                strSelectedDate = txtAttdDate.Text
                strSelectedEmpCode = ddlEmployee.SelectedItem.Value

                If strSelectedAttdId <> "" Then
                    onLoad_Display()
                    onLoad_BindButton()
                End If
            End If

            strSelectedPreBlk = strPreBlk
            If blnUseBlk = True Then
                strSelectedBlk = strBlk
            Else
                strSelectedBlk = strSubBlk
            End If
            strSelectedAcc = strAcc
            strSelectedVeh = strVeh
            strSelectedVehExp = strVehExp
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCdDelLn As String = "PR_CLSTRX_ATTENDANCETRX_LINE_DEL"
        Dim strOpCdUpdHeader As String = "PR_CLSTRX_ATTENDANCETRX_UPDSTS"
        Dim strOpCodes As String = strOpCdDelLn & "|" & strOpCdUpdHeader & "|||"
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim strAcc As String = ""
        Dim strPreBlk As String = ""
        Dim strBlk As String = ""
        Dim strSubBlk As String = ""
        Dim strVeh As String = ""
        Dim strVehExp As String = ""
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strParam As String
        Dim lblText As Label
        Dim strAttLnId As String
        Dim strChargeLocCode As String
        Dim strOTWorkInd As String
        Dim decLineHours As Decimal
        Dim decLineVolume As Decimal

        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = Convert.ToInt16(E.Item.ItemIndex)
        lblText = dgLineDet.Items.Item(Convert.ToInt16(E.Item.ItemIndex)).FindControl("lblAttLnId")
        strAttLnId = lblText.Text


      Try
             strParam = "|" & strAttLnId & "|" & strSelectedAttdId & "||||||1|0|1|1||" & cdbl(lblTotalHour.Text)-0 & "|1||||||"

            intErrNo = objPRTrx.mtdUpdAttdTrxLine(strOpCodes, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_LINE_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_dailyattd.aspx?attdid=" & strSelectedAttdId)
        End Try

        strSelectedAcc = strAcc
        strSelectedPreBlk = strPreBlk
        If blnUseBlk = True Then
            strSelectedBlk = strBlk
        Else
            strSelectedBlk = strSubBlk
        End If
        strSelectedVeh = strVeh

        strSelectedDate = txtAttdDate.Text
        strSelectedEmpCode = ddlEmployee.SelectedItem.Value
        onLoad_Display()
        onLoad_BindButton()
        
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_DAILYATTD_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_DailyAttd.aspx")
        End Try

        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
        dgLineDet.Columns(0).HeaderText = lblAccount.Text
        dgLineDet.Columns(1).HeaderText = lblPreBlock.Text
        dgLineDet.Columns(2).HeaderText = lblVehicle.Text
        dgLineDet.Columns(3).HeaderText = lblVehExpense.Text

        lblErrAccount.Text = "<br>" & lblErrSelect.Text & lblAccount.Text
        lblErrVeh.Text = lblErrSelect.Text & lblVehicle.Text
        lblErrVehExp.Text = lblErrSelect.Text & lblVehExpense.Text
        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_DAILYATTD_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_DailyAttd.aspx")
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

    Function CheckLocBillParty () As Boolean
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strSearch As String
        Dim objBPLocDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strLocCode As String
        Dim objDS As New DataSet()

            strLocCode = ""
            If Not (strLocCode = "" Or strLocCode = TRIM(strLocation)) Then
                strSearch = " AND BP.Status = '" & objGLSetup.EnumBillPartyStatus.Active & "'" & _
                            " AND BP.InterLocCode = '" & strLocCode & "'" 
                    
                Try
                    intErrNo = objGLSetup.mtdGetBPInterLoc(strOpCd_Get, strSearch, objBPLocDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_SETUP_BILLPARTY_GET&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_DailyAttd.aspx")
                End Try

                If objBPLocDs.Tables(0).Rows.Count <= 0 Then
                    lblLocCodeErr.Text = strLocCode
                    return False
                End If
            End If

        return True
    End Function

    Sub CheckLeaveBalance() 
        Dim strOpCd_EmpEmp As String = "HR_CLSTRX_EMPLOYMENT_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSortExpression As String = "EmpCode"
        Dim objEmpEmpDs as New DataSet
        Dim strAnnualLeaveBalance As String
        Dim strSickLeaveBalance As String = ""

        strParam = ddlEmployee.SelectedItem.Value.ToString & "|||" & strSortExpression & "|"

        Try
            intErrNo = objHRTrx.mtdGetEmployeeEmp(strOpCd_EmpEmp, strParam, objEmpEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_EMPLOYEE_EMPLOYMENT&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objEmpEmpDs.Tables(0).Rows.Count > 0 Then
            objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeaveBalance") = objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeaveBalance").Trim()   
            objEmpEmpDs.Tables(0).Rows(0).Item("SickLeaveBalance") = objEmpEmpDs.Tables(0).Rows(0).Item("SickLeaveBalance").Trim() 
            strAnnualLeaveBalance = objEmpEmpDs.Tables(0).Rows(0).Item("AnnualLeaveBalance")
            strSickLeaveBalance = objEmpEmpDs.Tables(0).Rows(0).Item("SickLeaveBalance").Trim() 

            If lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Annual Then
                If strAnnualLeaveBalance = "0" Then
                    lblErrAnnualLeaveBalance.Visible = True
                    lblErrSickLeaveBalance.Visible = False
                    lblErrNoSickLeave.Visible = False
                    lblErrNoAnnualLeave.Visible = False                    
                ElseIf Trim(strAnnualLeaveBalance) = "" Then
                    lblErrAnnualLeaveBalance.Visible = False
                    lblErrSickLeaveBalance.Visible = False
                    lblErrNoSickLeave.Visible = False
                    lblErrNoAnnualLeave.Visible = True                    
                Else 
                    lblErrAnnualLeaveBalance.Visible = False
                    lblErrSickLeaveBalance.Visible = False
                    lblErrNoSickLeave.Visible = False
                    lblErrNoAnnualLeave.Visible = False                    
                End If
            ElseIf lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Sick Then
                If strSickLeaveBalance = "0" Then
                    lblErrSickLeaveBalance.Visible = True
                    lblErrAnnualLeaveBalance.Visible = False
                    lblErrNoSickLeave.Visible = False
                    lblErrNoAnnualLeave.Visible = False
                ElseIf Trim(strSickLeaveBalance) = "" Then
                    lblErrSickLeaveBalance.Visible = False
                    lblErrAnnualLeaveBalance.Visible = False
                    lblErrNoSickLeave.Visible = True
                    lblErrNoAnnualLeave.Visible = False
                Else
                    lblErrSickLeaveBalance.Visible = False
                    lblErrAnnualLeaveBalance.Visible = False
                    lblErrNoSickLeave.Visible = False
                    lblErrNoAnnualLeave.Visible = False
                End If
            End If
        End If
    End Sub

    Sub UpdEmpLeaveBalance(pr_Status as String) 
        Dim strOpCd_Emp As String 
        Dim strParam As String = ""
        Dim intErrNo As Integer        
        Dim objEmpEmpDs as New DataSet
        Dim strLeave As String      

        If Trim(pr_Status) = "Confirm" Then
            strOpCd_Emp  = "HR_CLSTRX_EMPLOYMENT_LEAVE_UPD"
        ElseIf Trim(pr_Status) = "Cancel" Then
            strOpCd_Emp  = "HR_CLSTRX_EMPLOYMENT_LEAVE_CANCEL"
        End If 

        If lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Annual Then
            strLeave = "AnnualLeaveBalance"
        ElseIf lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Sick Then
            strLeave = "SickLeaveBalance"
        End If

        strParam = ddlEmployee.SelectedItem.Value.ToString & "|" & strLeave

        Try
            intErrNo = objHRTrx.mtdUpdEmpLeave(strOpCd_Emp, strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSTRX_EMPLOYMENT_LEAVE_UPD&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_DailyAttd.aspx")
        End Try

    End Sub

    Sub ValidationEmployee(pr_strEmpCode as String)


        Dim objADDs As New Dataset()
        Dim strOpCd_Get As String = "HR_CLSTRX_EMPLOYMENT_CHECKEMP_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strCategory As String
        Dim strPosition As String        
        Dim strSearch As String
        Dim strEmpCode As String

        strEmpCode = Trim(pr_strEmpCode)
        strCategory = " AND (SAL.CategoryTypeInd ='" & objHRSetup.EnumCategoryType.Staff & "' OR SAL.CategoryTypeInd ='" & objHRSetup.EnumCategoryType.NonStaff & "' OR SAL.CategoryTypeInd ='" & objHRSetup.EnumCategoryType.SKUB & "') "
        strPosition = " AND (SAL.PositionInd ='" & objHRSetup.EnumPosition.HQRegional & "' OR SAL.PositionInd ='" & objHRSetup.EnumPosition.Estate & "') "
                
        strParam = strEmpCode & "|" & _
                    strCategory & "|" & _
                      strPosition
                      

            Try
                intErrNo = objHRTrx.mtdValEmployee(strOpCd_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strParam, _
                                                objADDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSTRX_EMPLOYMENT_CHECKEMP_GET&errmesg=" & Exp.ToString() & "&redirect=HR/trx/HR_trx_EmployeeEmp.aspx")
            End Try

            If objADDs.Tables(0).Rows.Count > 0 Then
                lblErrEmpValidation.Visible = False                 
            Else
                lblErrEmpValidation.Visible = False                 
            End If 
        
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

    Sub BtnAddTemp_Click()
        Dim strOpCdAddLine As String = "PR_CLSTRX_ATTENDANCETRX_LINE_ADD"
        Dim strOpCdUpdHeader As String = "PR_CLSTRX_ATTENDANCETRX_UPDSTS"
        
        Dim strOpCdValidationBlock as String = "PR_CLSTRX_DAILYATTD_VALIDATION_BLKSUBBLK"
        Dim strOpCdValidationVeh as String = "PR_CLSTRX_DAILYATTD_VALIDATION_VEHICLE"
        Dim strOpCdValidationVehExp As String = "GL_CLSSETUP_VEHICLE_EXPENSE_CODE_GET_BY_VEHEXPENSECODE1"
        Dim objVehExpenseDs as object
        Dim strSearch as String
        Dim strSort as String


        Dim strOpCodes As String = strOpCdAddLine & "|" & strOpCdUpdHeader
        Dim strOpCdGetSubBlkByBlk As String
        Dim strParamList As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcc As String = ""
        Dim strPreBlk As String = ""
        Dim strBlk As String = ""
        Dim strSubBlk As String = ""
        Dim strVeh As String = ""
        Dim strVehExp As String = ""
        Dim blnIsUpdated As Boolean = True
        Dim decHour As Decimal
        Dim decVolume As Decimal

        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strChargeLocCode = ""
        Dim decPremi As Decimal = 0

           

        GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)


        If strSelectedAttdId = "" Then

            If ddlAttdCode.SelectedItem.Value = "" Then
                lblErrAttdCode.Visible = True
                Exit Sub
            End If
            If lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Annual Or lblCountDayType.Text.Trim() = objPRSetup.EnumCountDayType.Sick Then
                ValidationEmployee(ddlEmployee.SelectedItem.Value.ToString)
                If lblErrEmpValidation.Visible = True Then
                    Exit Sub
                End If
                CheckLeaveBalance()
                If lblErrAnnualLeaveBalance.Visible = True Or lblErrSickLeaveBalance.Visible = True Then
                    Exit Sub
                End If
                If lblErrNoAnnualLeave.Visible = True Or lblErrNoSickLeave.Visible = True Then
                    Exit Sub
                End If
            End If
            UpdateHeader("Save", blnIsUpdated)
        End If

        If blnIsUpdated = False Then
            Exit Sub
        Else
            If Not (Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working) Then
                UpdateHeader("Save", blnIsUpdated)
            End If
            Try
                If ddlChargeLevel.SelectedIndex = 0 Then
                    strBlk = Trim(txtPreBlock.Text)
                    strSubBlk = ""
                Else
                    strBlk = ""
                    strSubBlk = Trim(txtPreBlock1.Text)
                End If

                If (Trim(ddlAttdCode.SelectedItem.Value) <> Trim(objPRSetup.EnumCountDayType.Working)) Then
                    txtAccount.Text = ""
                    strBlk = ""
                    strSubBlk = ""
                    txtVeh.Text = ""
                    txtVehExp.Text = ""
                    txtOT.Text = "0"
                    decPremi = 0
                End If

                strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PRAttendanceTrxLn) & "||" & _
                           strSelectedAttdId & "|" & _
                           Trim(txtAccount.Text) & "|" & _
                           strBlk & "|" & _
                           strSubBlk & "|" & _
                           Trim(txtVeh.Text) & "|" & _
                           Trim(txtVehExp.Text) & "|" & _
                           lblTotalHour.Text & "|" & _
                           "0" & "|" & _
                           "0" & "|" & _
                           "1" & "|" & _
                           strChargeLocCode & "|" & _
                           CDbl(lblTotalHour.Text) & "|0|0||0|" & _
                           CDbl(Trim(txtOT.Text)) & "|" & _
                           decPremi & "|0"

                If ddlChargeLevel.SelectedIndex = 0 And RowPreBlock.Visible = True Then
                    If blnIsVehicleRequire = False Then
                        If lblQuotaLevel.Text = objHRTrx.EnumQuotaLevel.Block Then
                            strOpCdGetSubBlkByBlk = "PR_CLSTRX_DAILYATTD_GET_SUBBLK_BY_BLK"
                        Else
                            strOpCdGetSubBlkByBlk = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
                        End If

                        strParamList = Session("SS_LOCATION") & "|" & _
                                    Trim(strAcc) & "|" & _
                                    Trim(txtPreBlock.Text) & "|" & _
                                    objGLSetup.EnumBlockStatus.Active

                        intErrNo = objPRTrx.mtdUpdAttdTrxLineByBlock(strOpCdGetSubBlkByBlk, _
                                                                    strParamList, _
                                                                    strOpCodes, _
                                                                    strCompany, _
                                                                    strLocation, _
                                                                    strUserId, _
                                                                    strLocType, _
                                                                    strParam)

                    Else
                        intErrNo = objPRTrx.mtdUpdAttdTrxLine(strOpCodes, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strParam)
                    End If

                Else
                    intErrNo = objPRTrx.mtdUpdAttdTrxLine(strOpCodes, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strParam)
                End If
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_trx_dailyattd.aspx?attdid=" & strSelectedAttdId)
            End Try

            strSelectedDate = txtAttdDate.Text
            strSelectedEmpCode = ddlEmployee.SelectedItem.Value

            If strSelectedAttdId <> "" Then
                onLoad_Display()
                onLoad_BindButton()
            End If
        End If

        strSelectedPreBlk = strPreBlk
        If blnUseBlk = True Then
            strSelectedBlk = strBlk
        Else
            strSelectedBlk = strSubBlk
        End If
        strSelectedAcc = strAcc
        strSelectedVeh = strVeh
        strSelectedVehExp = strVehExp
    End Sub



End Class
