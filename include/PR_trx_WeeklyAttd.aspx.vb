
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


Public Class PR_trx_WeeklyAttd : Inherits Page

    Protected WithEvents ddlGang As DropDownList
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents btnFind1 As HtmlInputButton
    Protected WithEvents TrOnePayType As HtmlTableRow
    Protected WithEvents cbOnePayType As CheckBox
    Protected WithEvents lblPeriod As Label

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents TrErrorData As HtmlTableRow
    Protected WithEvents txtAttdDateFrom As TextBox
    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents txtAttdDateTo As TextBox
    Protected WithEvents btnSelDateTo As Image
    Protected WithEvents RefreshBtn As ImageButton
    Protected WithEvents ddlAttd As DropDownList

    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents dgLineDet As DataGrid 

    Protected WithEvents txtAccount as TextBox
    Protected WithEvents txtPreBlock as TextBox
    Protected WithEvents txtPreBlock1 as TextBox
    Protected WithEvents txtVeh as TextBox
    Protected WithEvents txtVehExp as TextBox
    Protected WithEvents btnSearch1 as imagebutton
    Protected WithEvents btnSearch2 as imagebutton
    Protected WithEvents btnSearch3 as imagebutton
    Protected WithEvents btnSearch4 as imagebutton
    Protected WithEvents lblCountDayType as Label
    Protected WithEvents lblFlagBindAttCode as Label
    Protected WithEvents lblErrValidation as Label
    Protected WithEvents lblErrMessage as Label
    Protected WithEvents lblAccountDesc as Label
    Protected WithEvents lblPreBlockDesc as Label
    Protected WithEvents lblPreBlockDesc1 as Label
    Protected WithEvents lblVehDesc as Label
    Protected WithEvents lblVehExpDesc as Label
    Protected WithEvents lblVehicleOption As Label

    Protected WithEvents lblVehOption As Label
    Protected WithEvents lblHasShift As Label

    Protected WithEvents lblErrGangOrEmp As Label
    Protected WithEvents lblErrEither As Label
    Protected WithEvents lblErrAttdDate As Label
    Protected WithEvents lblErrAttdDateDesc As Label
    Protected WithEvents lblErrAttCode As Label
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrPreBlock As Label
    Protected WithEvents lblErrPreBlock1 As Label
    Protected WithEvents lblErrVeh As Label
    Protected WithEvents lblErrVehExp As Label
    Protected WithEvents lblErrTotal As Label
    Protected WithEvents lblErrMaxDate As Label

    Protected WithEvents lblAccount As Label
    Protected WithEvents lblPreBlock As Label
    Protected WithEvents lblPreBlock1 As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehExpense As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblEmpName As Label
    Protected WithEvents lblEmpPayType As Label
    Protected WithEvents lblEmpOTInd As Label

    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlock As HtmlTableRow
    Protected WithEvents RowPreBlock1 As HtmlTableRow
    Protected WithEvents txtHarvestInc As TextBox
    Protected WithEvents lblvaldoubledata as label

    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden

    Protected objPRTrx As New agri.PR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()

    Dim objAttdDs As New Object()
    Dim objAttdLnDs As New Object()
    Dim objGangDs As New Object()
    Dim objEmpDs As New Object()
    Dim objAttdCodeDs As New Object()
    Dim objAccDs As New Object()
    Dim objBlkDs As New Object()
    Dim objVehDs As New Object()
    Dim objVehExpDs As New Object()
    Dim objLangCapDs As New Object()

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
    Dim dblADHours As Double

    Dim strSelGangCode As String = ""
    Dim strSelEmpCode As String = ""
    Dim strSelDate As String = ""
    Dim intStatus As Integer
    Dim strCostLevel As String

    Dim PreBlockTag As String
    Dim BlockTag As String

    Protected WithEvents lblBPErr As Label
    Protected WithEvents lblLocCodeErr As Label

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


        If Session("SS_COSTLEVEL") = "block" Then
            blnUseBlk = True
        End If



        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRWeekly), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrGangOrEmp.visible = False
            lblErrEither.visible = False
            lblErrAttdDate.visible = False
            lblErrAttdDateDesc.visible = False
            lblErrAttCode.Visible = False
            lblErrAccount.visible = False
            lblErrPreBlock.Visible = False
            lblErrPreBlock1.Visible = False
            lblErrVeh.visible = False
            lblErrVehExp.visible = False
            TrErrorData.visible = False
            lblErrMaxDate.Visible = False
            lblFlagBindAttCode.Text = "0"

            strSelEmpCode = Trim(Request.QueryString("empcode"))
            strSelDate = Trim(Request.QueryString("date"))
            onload_GetLangCap()

            If Not IsPostBack Then
                BindChargeLevelDropDownList()
                lblPeriod.Text = strAccMonth & "/" & strAccYear
                txtAttdDateFrom.Text = objGlobal.GetShortDate(strDateFmt, Now())
                txtAttdDateTo.Text = objGlobal.GetShortDate(strDateFmt, DateAdd("d", 6, Now()))
                BindGang("")
                BindEmployee("")
                BindAttdCode("", "")
                onLoad_BindButton()
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
        ToggleChargeLevel
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


    Sub BindGang(ByVal pv_strGangCode as String)
        Dim strOpCdGet As String = "PR_CLSTRX_WEEKLYATTD_GET_GANGLIST"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As Datarow
        
        If Trim(pv_strGangCode) = "" Then
            strParam = "|" & "where Status = '" & objHRSetup.EnumGangStatus.Active & "' and LocCode = '" & strLocation & "' "
        Else
            strParam = "|" & "where GangCode = '" & Trim(pv_strGangCode) & "' and  Status = '" & objHRSetup.EnumGangStatus.Active & "' and LocCode = '" & strLocation & "' "
        End If
        
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objGangDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WEEKLYATTD_GET_GANGLIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If Trim(pv_strGangCode) = "" Then
            dr = objGangDs.Tables(0).NewRow()
            dr("GangCode") = ""
            dr("_Description") = "Select Gang Code"
            objGangDs.Tables(0).Rows.InsertAt(dr, 0)

            ddlGang.DataSource = objGangDs.Tables(0)
            ddlGang.DataValueField = "GangCode"
            ddlGang.DataTextField = "_Description"
            ddlGang.DataBind()
        End if
    End Sub

    Sub BindEmployee(ByVal pv_strEmpCode as String)
        Dim strOpCdGet As String = "PR_CLSTRX_WEEKLYATTD_GET_EMPLOYEELIST"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As Datarow
        
        If Trim(pv_strEmpCode) = "" Then
            strParam = "|" & "and e.Status = '" & objHRTrx.EnumEmpStatus.Active & "' and e.LocCode = '" & strLocation & "' "
        Else
            strParam = "|" & "and e.EmpCode = '" & Trim(pv_strEmpCode) & "' and e.Status = '" & objHRTrx.EnumEmpStatus.Active & "' and e.LocCode = '" & strLocation & "' "
        End If
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WEEKLYATTD_GET_EMPLOYEELIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If Trim(pv_strEmpCode) = "" Then
            dr = objEmpDs.Tables(0).NewRow()
            dr("EmpCode") = ""
            dr("_Description") = "Select Employee Code"
            objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

            ddlEmployee.DataSource = objEmpDs.Tables(0)
            ddlEmployee.DataValueField = "EmpCode"
            ddlEmployee.DataTextField = "_Description"
            ddlEmployee.DataBind()
        else
            lblEmpPayType.Text = objEmpDs.Tables(0).Rows(0).Item("PayType")

        End If


    End Sub

    Sub onClick_DateRange(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        onLoad_Display()
    End Sub

    Sub onChg_Gang(Sender As Object, E As EventArgs)
        Dim strGangCode As String        
        
        lblvaldoubledata.visible = false 
        strGangCode = ddlGang.SelectedItem.Value    
        If Trim(strGangCode) <> "" Then
            BindGang(strGangCode)
            ddlEmployee.SelectedIndex = 0
            TrOnePayType.Visible = True
            lblEmpPayType.Text = objGangDs.Tables(0).Rows(0).Item("PayType") 
        End If
        onLoad_Display()
    End Sub

    Sub onChg_Employee(Sender As Object, E As EventArgs)
        Dim arrEmpCode As Array
        Dim strEmpCode As String
        Dim strPayType As String

        lblvaldoubledata.visible = false 
        
        strEmpCode = ddlEmployee.SelectedItem.Value
        If Trim(strEmpCode) = "" Then
            lblErrGangOrEmp.Visible = True
            Exit Sub
        Else
            BindEmployee(strEmpCode)
            ddlGang.SelectedIndex = 0
            TrOnePayType.Visible = False
            lblEmpName.Text = objEmpDs.Tables(0).Rows(0).Item("EmpName")
            lblEmpPayType.Text = objEmpDs.Tables(0).Rows(0).Item("PayType")
            lblEmpOTInd.Text = objEmpDs.Tables(0).Rows(0).Item("OTInd")
        End If
        onLoad_Display()
    End Sub

    Sub onChg_Date(Sender As Object, E As EventArgs)
        lblvaldoubledata.visible = false 
        onLoad_Display()
    End Sub 

    Sub onLoad_BindButton()
        txtAccount.Enabled = True
        txtPreBlock.Enabled = True
        txtPreBlock1.Enabled = True
        txtVeh.Enabled = True
        txtVehExp.Enabled = True

        if lblCountDayType.Text <> "" then
            if Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working then 
                tblSelection.Visible = True
                txtAccount.Enabled = True
                txtPreBlock.Enabled = True
                txtPreBlock1.Enabled = True
                txtVeh.Enabled = True
                txtVehExp.Enabled = True
                txtHarvestInc.Enabled = True
                ddlChargeLevel.Enabled = True
            else
                txtAccount.Enabled = false
                txtPreBlock.Enabled = false
                txtPreBlock1.Enabled = false
                txtVeh.Enabled = false
                txtVehExp.Enabled = false
                txtHarvestInc.Enabled = false
                ddlChargeLevel.Enabled = false

            end if
        end if


    End Sub


    Sub BindAttdCode(ByVal pv_strAttdCode As String, ByVal pv_strPayType As String)
        Dim strOpCd As String = "PR_CLSTRX_WEEKLYATTD_ATTDANCECODE_GET"
        Dim dr As DataRow
        Dim strParam As String
        Dim strSort As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        pv_strAttdCode = Trim(pv_strAttdCode)

        strSort = "ORDER BY Attd.AttCode ASC"
        If Trim(pv_strPayType) = "" Then
            strSearch = "AND Attd.Status = '" & objPRSetup.EnumAttStatus.Active & "' "
        Else
            strSearch = "AND Attd.PayType in ('" & pv_strPayType & "','" & objPRSetup.EnumPayType.NoRate & "') AND Attd.Status = '" & objPRSetup.EnumAttStatus.Active & "' "
        End If

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objAttdCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WEEKLYATTD_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If pv_strAttdCode <> "" Then
            For intCnt = 0 To objAttdCodeDs.Tables(0).Rows.Count - 1
                If objAttdCodeDs.Tables(0).Rows(intCnt).Item("AttCode").Trim() = pv_strAttdCode Then
                    intSelectedIndex = intCnt + 1
                    lblCountDayType.Text = Trim(objAttdCodeDs.Tables(0).Rows(0).Item("CountDayType"))
                    Exit For
                End If
            Next
        End If

        dr = objAttdCodeDs.Tables(0).NewRow()        
        dr("AttCode") = ""
        dr("_Description") = "Select Attendance Code"
        dr("Hours") = 0
        dr("OTAllowed") = "2"
        objAttdCodeDs.Tables(0).Rows.InsertAt(dr, 0)
        
        lblEmpOTInd.Text = objAttdCodeDs.Tables(0).Rows(intSelectedIndex).Item("OTAllowed")
        
        ddlAttd.DataSource = objAttdCodeDs.Tables(0)        
        ddlAttd.DataValueField = "AttCode"
        ddlAttd.DataTextField = "_Description"
        ddlAttd.DataBind()
        ddlAttd.SelectedIndex = intSelectedIndex
        ddlAttd.AutoPostBack = True
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
        strSearch = strSearch & " and atd.attcode = '" & Trim(ddlAttd.SelectedItem.Value) & "' "

        strSort = "order by atd.AttCode asc"

        strParam = strSort & "|" & strSearch
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objAttdCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        if objAttdCodeDs.Tables(0).Rows.Count > 0 then 
            lblCountDayType.Text = Trim(objAttdCodeDs.Tables(0).Rows(0).Item("CountDayType"))
        end if
        if Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working then 
            tblSelection.Visible = True
            dglineDet.visible = false 
        else
            txtAccount.Text = ""
            txtPreBlock.Text = ""
            txtPreBlock1.Text = ""
            txtVeh.Text = ""
            txtVehExp.Text = ""
            txtHarvestInc.Text = ""

            txtAccount.Enabled = false
            txtPreBlock.Enabled = false
            txtPreBlock1.Enabled = false
            txtVeh.Enabled = false
            txtVehExp.Enabled = false
            txtHarvestInc.Enabled = false
            ddlChargeLevel.Enabled = false

        end if
        lblFlagBindAttCode.Text = "1"
        onLoad_Display()
    End Sub

    Sub onLoad_Display()
        Dim strOpCdGet As String = "PR_CLSTRX_WEEKLYATTD_GET"
        Dim strOpCd As String = "PR_CLSTRX_DAILYATTENDANCE_ATTDANCE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strGangCode As String
        Dim strEmpCode As String
        Dim strEmpPayType As String = ""
        Dim strFromDate As String = ""
        Dim strToDate As String = ""
        Dim objFormatDate As String
        Dim strStatus As String
        Dim strCostLevel As String
        Dim intCnt As Integer = 0
        Dim lbButton As LinkButton
        Dim lbl As Label

        Dim strSort As String
        Dim strSearch As String
        Dim intSelectedIndex As Integer = 0

        strCostLevel = Session("SS_COSTLEVEL")
        strGangCode = ddlGang.SelectedItem.Value
        strEmpCode = ddlEmployee.SelectedItem.Value
        strEmpPayType = lblEmpPayType.Text
   
        If strGangCode = "" And strEmpCode = "" Then
            lblErrGangOrEmp.Visible = True
            Exit Sub
        ElseIf Not (Trim(strGangCode) <> "" Or Trim(strEmpCode) <> "") Then
            lblErrEither.Visible = True
            Exit Sub
        End If

        If txtAttdDateFrom.Text = "" Then
            lblErrAttdDate.Visible = True
            Exit Sub
        End If

        If txtAttdDateTo.Text = "" Then
            lblErrAttdDate.Visible = True
            Exit Sub
        End If

        If txtAttdDateFrom.Text <> "" And txtAttdDateTo.Text <> "" Then
            If objGlobal.mtdValidInputDate(strDateFmt, _
                                            txtAttdDateFrom.Text, _
                                            objFormatDate, _
                                            strFromDate) = False  Or _
            objGlobal.mtdValidInputDate(strDateFmt, _
                                            txtAttdDateTo.Text, _
                                            objFormatDate, _
                                            strToDate) = False Then
                lblErrAttdDateDesc.Text = lblErrAttdDateDesc.Text & objFormatDate
                lblErrAttdDateDesc.Visible = True
                Exit Sub
            End If
        End If

        If DateDiff("d", strFromDate, strToDate) < 0 Then
            lblErrAttdDate.Visible = True
            Exit Sub
        End If

        If DateDiff("d", strFromDate, strToDate) > 6 Then
            lblErrMaxDate.Visible = True
            Exit Sub
        End If
        
        strStatus = "'" & objPRTrx.EnumAttdTrxStatus.Active & "','" & objPRTrx.EnumAttdTrxStatus.Confirmed & "'"
        strParam = strCostLevel & "|" & _
                   strLocation & "|" & _
                   strAccMonth & "|" & _
                   strAccYear & "|" & _
                   strGangCode & "|" & _
                   strEmpCode & "|" & _
                   strFromDate & "|" & _
                   strToDate & "|" & _
                   strStatus
        Try
            intErrNo = objPRTrx.mtdGetWeeklyAttdTrx(strOpCdGet, _
                                                    strParam, _
                                                    objAttdDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WEEKLYATTD_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dgLineDet.DataSource = objAttdDs.Tables(0)
        dgLineDet.DataBind()

        If dgLineDet.Items.Count > 0 Then
            For intCnt=0 To dgLineDet.Items.Count - 1
                lbl = dgLineDet.Items.Item(intCnt).FindControl("lblStatus")
                If CInt(lbl.text) = objPRTrx.EnumAttdTrxStatus.Active Then
                    lbl = dgLineDet.Items.Item(intCnt).FindControl("lblAttLnId")
                    If Trim(lbl.text) = "" Then
                        lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
                    Else
                        lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    End If
                Else
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                End If
            Next      
            dglineDet.Visible = true 
            ddlAttd.Enabled = False
         Else
             dglineDet.Visible = false 
             ddlAttd.Enabled = True        
         End If
        
        IF objAttdDs.Tables(0).Rows.Count > 0 Then
            BindAttdCode(objAttdDs.Tables(0).Rows(0).Item("AttCode").Trim(), objAttdDs.Tables(0).Rows(0).Item("PayType").Trim())
        END IF


        strSearch = "and atd.Status = '" & objPRSetup.EnumAttStatus.Active & "' "
        strSearch = strSearch & " and atd.attcode = '" & Trim(ddlAttd.SelectedItem.Value) & "' "

        strSort = "order by atd.AttCode asc"

        strParam = strSort & "|" & strSearch
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objAttdCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        if objAttdCodeDs.Tables(0).Rows.Count > 0 then 
            lblCountDayType.Text = Trim(objAttdCodeDs.Tables(0).Rows(0).Item("CountDayType"))
        end if

        onLoad_BindButton()

        GetAccountDesc(Trim(txtAccount.Text))
        GetPreBlockDesc("")
        GetVehDesc(Trim(txtVeh.Text))
        GetVehExpDesc(Trim(txtVehExp.Text))
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WEEKLYATTD_GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString() & "&redirect=")
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
        Dim strAcc As String = ""
        Dim strPreBlk As String = ""
        Dim strBlk As String = ""
        Dim strSubBlk As String = ""
        Dim strVeh As String = ""
        Dim strVehExp As String = ""

        GetAccountDetails(strAcc, blnIsBalanceSheet, blnIsNurseryInd, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers)

    End Sub




    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsNurseryInd As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strOpCdGetMember As String = "PR_CLSTRX_WEEKLYATTD_MEMBER_GET"
        Dim strOpCdGetTrx As String = "PR_CLSTRX_ATTENDANCETRX_ATTDATE_GET"
        Dim strOpCdGetAtt As String = "PR_CLSSETUP_ATTENDANCE_LIST_GET_BY_ATTCODE"
        Dim strOpCdAdd As String = "PR_CLSTRX_ATTENDANCETRX_ADD"
        Dim strOpAddLn As String = "PR_CLSTRX_ATTENDANCETRX_LINE_ADD"
        Dim strOpCdUpdSts As String = "PR_CLSTRX_ATTENDANCETRX_UPDSTS"
        Dim strOpCdAttd As String = "PR_CLSSETUP_ATTENDANCE_LIST_GET_BY_ATTCODE"
        Dim strOpCdTotalHour As String = "PR_CLSTRX_ATTENDANCETRX_LINE_TOTAL_HOUR_GET"
        Dim strOpCdLn As String = strOpAddLn & "|" & strOpCdUpdSts & "|" & strOpCdAttd & "|" & strOpCdTotalHour & "|"

        Dim strOpCheck As String = "PR_CLSTRX_ATTENDANCETRX_CHECK_DOUBLE_DATA"

        Dim strParam As String
        Dim intErrNo As Integer
        Dim objResult As New Object()
        Dim objErrorData As New Object()
        Dim objFormatDate As String
        Dim cntEmp As Integer
        Dim strErrExp As String = ""
        Dim arrErrEmployee As Array
        Dim arrErrEmpDet As Array

        Dim strAcc As String = ""
        Dim strPreBlk As String = Request.Form("ddlPreBlock")
        Dim strBlk As String = ""
        Dim strSubBlk As String = ""
        Dim strVeh As String = Trim(txtVeh.Text)
        Dim strVehExp As String = Trim(txtVehExp.Text)

        Dim strGangCode As String
        Dim strEmpCode As String
        Dim strEmpPayType As String
        Dim strEmpName As String
        Dim strEmpStatus As String
        Dim strPayType As String
        Dim strFromDate As String
        Dim strToDate As String
        Dim arrAttCode As Array
        Dim strAttCode As String
        Dim strAttOTInd As String
        Dim strShiftLnId As String
        Dim intOTWorkInd As Integer
        Dim decPremi As Decimal

        Dim strOpCdValidationBlock as String = "PR_CLSTRX_DAILYATTD_VALIDATION_BLKSUBBLK"
        Dim strOpCdValidationVeh as String = "PR_CLSTRX_DAILYATTD_VALIDATION_VEHICLE"
        Dim strOpCdValidationVehExp As String = "GL_CLSSETUP_VEHICLE_EXPENSE_CODE_GET_BY_VEHEXPENSECODE1"
        Dim objVehExpenseDs as object
        Dim strSearch as String
        Dim strSort as String

        lblErrValidation.Text = ""

        
        if Trim(lblCountDayType.Text) <> "" then
            if  Trim(lblCountDayType.Text) = objPRSetup.EnumCountDayType.Working then
                if Trim(txtAccount.Text) = ""  then 
                    lblErrAccount.Text = "Please Fill Account No !"
                    lblErrAccount.Visible = true
                    Exit Sub
                else
                    lblErrAccount.Visible = false
                    strAcc = Trim(txtAccount.Text)
                end if
            end if



            if Trim(lblcountdaytype.text) = Trim(objPRSetup.EnumCountDayType.Working) then
                    If ddlChargeLevel.SelectedIndex = 0 Then
                        strBlk = Trim(txtPreBlock.Text) 
                        strSubBlk = ""
                        strPreBlk = Trim(txtPreBlock.Text) 
                    else
                        strBlk = "" 
                        strSubBlk = Trim(txtPreBlock1.Text)
                        strPreBlk = Trim(txtPreBlock1.Text) 
                    end if

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

  


                if (Trim(txtPreBlock.Text) <> "" or Trim(txtPreBlock1.Text) <> "")  and Trim(txtVeh.Text) = "" then 
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
            
        else
            strBlk = "" 
            strSubBlk = ""
        end if

        

        if Trim(txtHarvestInc.Text) = "" then 
            txtHarvestInc.Text = "0"
        end if 

        decPremi = Cdbl(Trim(txtHarvestInc.Text))

        strGangCode = ddlGang.SelectedItem.Value
        strEmpCode = ddlEmployee.SelectedItem.Value
        strEmpName = lblEmpName.Text
        
        If strGangCode = "" And strEmpCode = "" Then
            lblErrGangOrEmp.Visible = True
            Exit Sub
        ElseIf Not (strGangCode <> "" Or strEmpCode <> "") Then
            lblErrEither.Visible = True
            Exit Sub
        End If

        If txtAttdDateFrom.Text = "" Then
            lblErrAttdDate.Visible = True
            Exit Sub
        End If

        If txtAttdDateTo.Text = "" Then
            lblErrAttdDate.Visible = True
            Exit Sub
        End If

        If txtAttdDateFrom.Text <> "" And txtAttdDateTo.Text <> "" Then
            If objGlobal.mtdValidInputDate(strDateFmt, _
                                            txtAttdDateFrom.Text, _
                                            objFormatDate, _
                                            strFromDate) = False  Or _
                objGlobal.mtdValidInputDate(strDateFmt, _
                                            txtAttdDateTo.Text, _
                                            objFormatDate, _
                                            strToDate) = False Then
                lblErrAttdDateDesc.Text = lblErrAttdDateDesc.Text & objFormatDate
                lblErrAttdDateDesc.Visible = True
                Exit Sub
            End If
        End If

        If DateDiff("d", strFromDate, strToDate) < 0 Then
            lblErrAttdDate.Visible = True
            Exit Sub
        End If

        If DateDiff("d", strFromDate, strToDate) > 6 Then
            lblErrMaxDate.Visible = True
            Exit Sub
        End If


        strAttCode = ddlAttd.SelectedItem.Value
        strAttOTInd = lblEmpOTInd.Text 

        If strAttCode = "" Then
            lblErrAttCode.Visible = True
            Exit Sub
        End If

        If CheckLocBillParty() = False Then
            lblBPErr.Visible = True
            lblLocCodeErr.Visible = True
            Exit Sub
        End If  

        Try 
            strParam = "|" & " where a.gangcode like '%" & trim(strGangCode) & "' and a.empcode like '%" & trim(strEmpCode) & "' and a.attdate between '" & Trim(strFromDate) & "' and '" & Trim(strToDate) & "'" & _
                       " and b.chargeloccode like '%" & Trim(strLocation) & "' and b.acccode = '" & Trim(strAcc) & "' and b.blkcode like '%" & trim(strBlk) & "' and b.subblkcode like '%" & trim(strSubBlk) & "'"            
            
            intErrNo = objPRSetup.mtdGetMasterList(strOpCheck, _
                                                   strParam, _ 
                                                   "1", objResult)  

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WEEKLYATTD_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/PR_trx_WeeklyAttd.aspx")
        End Try
        
        if objResult.Tables(0).Rows.Count > 0 then 
            lblvaldoubledata.visible = true
            exit sub
        else
            lblvaldoubledata.visible = false 
        end if     


        Try
            


            strParam = strAccMonth & "|" & _
                        strAccYear & "|" & _
                        strGangCode & "|" & _
                        strEmpCode & "|" & _
                        strEmpName & "|" & _
                        strFromDate & "|" & _
                        strToDate & "|" & _
                        Trim(lblEmpPayType.Text) & "|" & _
                        "1|" & _
                        strAttCode & "|" & _
                        "|" & _
                        Trim(strLocation) & "|" & _
                        strAcc & "|" & _
                        strBlk & "|" & _
                        strSubBlk & "|" & _
                        Trim(txtVeh.Text) & "|" & _
                        Trim(txtVehExp.Text) & "|" & _
                        "0|" & _
                        "0|" & _
                        "0|" & _
                        "1|" & _
                        decPremi & "|0"
            

            If ddlChargeLevel.SelectedIndex = 0 And RowPreBlock.Visible = True Then


                If blnIsVehicleRequire = False Then

                            strParamList = Session("SS_LOCATION") & "|" & _
                                            Trim(strAcc) & "|" & _
                                            Trim(txtPreBlock.Text) & "|" & _
                                            objGLSetup.EnumBlockStatus.Active

                            intErrNo = objPRTrx.mtdUpdWeeklyAttdByBlock(strOpCodeGLSubBlkByBlk, _
                                                                        strParamList, _ 
                                                                        strOpCdGetMember, _
                                                                        strOpCdGetTrx, _
                                                                        strOpCdGetAtt, _
                                                                        strOpCdAdd, _
                                                                        strOpCdLn, _
                                                                        strCompany, _
                                                                        strLocation, _
                                                                        strUserId, _
                                                                        strParam, _
                                                                        strLocType, _
                                                                        objResult, _
                                                                        objErrorData)

                Else
                     intErrNo = objPRTrx.mtdUpdWeeklyAttd(strOpCdGetMember, _
                                                     strOpCdGetTrx, _
                                                     strOpCdGetAtt, _
                                                     strOpCdAdd, _
                                                     strOpCdLn, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     objResult, _
                                                     objErrorData)
                End If
            Else
                intErrNo = objPRTrx.mtdUpdWeeklyAttd(strOpCdGetMember, _
                                                     strOpCdGetTrx, _
                                                     strOpCdGetAtt, _
                                                     strOpCdAdd, _
                                                     strOpCdLn, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     objResult, _
                                                     objErrorData)
            End If

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WEEKLYATTD_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/PR_trx_WeeklyAttd.aspx")
        End Try

        If objResult = 3 Then
            lblErrTotal.Text = "Failed to update transaction details due to incomplete setup of attendance code.<br>"
            lblErrTotal.Visible = True
            TrErrorData.Visible = True
            Exit Sub
        ElseIf objResult = 4 Then
            lblErrTotal.Text = "Relevant employee is not found.<br><br>"
            lblErrTotal.Visible = True
            TrErrorData.Visible = True
            Exit Sub
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

        onLoad_Display()
    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCdDelLn As String = "PR_CLSTRX_ATTENDANCETRX_LINE_DEL"
        Dim strOpCdUpdSts As String = "PR_CLSTRX_ATTENDANCETRX_UPDSTS"
        Dim strOpCodes As String = strOpCdDelLn & "|" & strOpCdUpdSts & "|||"
        Dim strAcc As String = ""
        Dim strPreBlk As String = ""
        Dim strBlk As String = ""
        Dim strSubBlk As String = ""
        Dim strVeh As String = ""
        Dim strVehExp As String = ""

        Dim objResult As New Object()
        Dim strEmp As String = Request.Form("ddlEmployee")
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strParam As String
        Dim lblText As Label
        Dim strAttId As String
        Dim strAttLnId As String
        Dim strChargeLocCode As String
        Dim strOTWorkInd As String
        Dim decLineHours As Decimal
        Dim decLineVolume As Decimal
        Dim decLineTotalHour As Decimal
        Dim decLineTotalVolume As Decimal
        Dim intErrNo As Integer
        Dim strPayType As String

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)

        lblText = dgLineDet.Items.Item(Convert.ToInt16(E.Item.ItemIndex)).FindControl("lblChargeLocCode")
        strChargeLocCode = lblText.Text

        lblText = dgLineDet.Items.Item(Convert.ToInt16(E.Item.ItemIndex)).FindControl("lblAttLnId")
        strAttLnId = lblText.Text
        
        lblText = dgLineDet.Items.Item(Convert.ToInt16(E.Item.ItemIndex)).FindControl("lblAttId")
        strAttId = lblText.Text

        lblText = dgLineDet.Items.Item(Convert.ToInt16(E.Item.ItemIndex)).FindControl("lblTotalHour")
        decLineTotalHour = lblText.Text

        lblText = dgLineDet.Items.Item(Convert.ToInt16(E.Item.ItemIndex)).FindControl("lblTotalVolume")
        decLineTotalVolume = lblText.Text

        Try

            strParam = "|" & strAttLnId & "|" & strAttId & "||||||" & _
                        "0|0|" & _
                        "0|" & _
                        "1||" & _
                        "0|" & _
                        "0|||0|0|0|0"

            intErrNo = objPRTrx.mtdUpdAttdTrxLine(strOpCodes, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WEEKLYATTD_LINE_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/PR_trx_WeeklyAttd.aspx")
        End Try

        strSelEmpCode = strEmp
        strSelectedAcc = strAcc
        strSelectedPreBlk = strPreBlk
        If blnUseBlk = True Then
            strSelectedBlk = strBlk
        Else
            strSelectedBlk = strSubBlk
        End If
        strSelectedVeh = strVeh
        strSelectedVehExp = strVehExp
        onLoad_Display()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WEEKLYATTD_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_WeeklyAttd.aspx")
        End Try

        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
        dgLineDet.Columns(3).HeaderText = lblAccount.Text
        dgLineDet.Columns(4).HeaderText = lblPreBlock.Text
        dgLineDet.Columns(5).HeaderText = lblVehicle.Text
        dgLineDet.Columns(6).HeaderText = lblVehExpense.Text

        lblErrAccount.Text = "<br>" & lblErrSelect.Text & lblAccount.Text
        lblErrVeh.Text = lblErrSelect.Text & lblVehicle.Text
        lblErrVehExp.Text = lblErrSelect.Text & lblVehExpense.Text
        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlock.Text =  GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text 
        lblErrPreBlock.Text = lblErrSelect.Text & GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        lblErrPreBlock1.Text = lblErrSelect.Text & GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WEEKLYATTD_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_WeeklyAttd.aspx")
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



End Class
