Imports System
Imports System.Data
Imports System.Collections
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic

'----- #1 Start -----
Imports agri.PWSystem
'----- #1 End -----

Public Class BD_STDRPT_SELECTION_CTRL : Inherits UserControl
    Dim objBD As New agri.BD.clsReport()
    Dim objBDSetup As New agri.BD.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
'----- #1 Start -----
    Dim objSysCfg As New agri.PWsystem.clsConfig()
'----- #1 End -----
    Dim objAdminLoc As New agri.Admin.clsLoc()


    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblUserLoc As Label

    Protected WithEvents lstRptname As DropDownList
    Protected WithEvents lstDecimal As DropDownList
    Protected WithEvents cbLocAll As CheckBox
    Protected WithEvents cblUserLoc As CheckBoxList
    Protected WithEvents lblCropTag As Label
    Protected WithEvents lblVehicleTag As Label
    Protected WithEvents lblActivityTag As Label

    Protected WithEvents lstPeriodName As DropDownList
    Protected WithEvents hidUserLoc As HtmlInputHidden
    Protected WithEvents TrLoc As HtmlTableRow

    Dim dsForDropDown As New DataSet()
    Dim objUserLoc As New DataSet()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserLoc As String

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim dr As DataRow
    Dim intSelIndex As Integer
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntDec As Integer

        TrLoc.Visible = False

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If Not Page.IsPostBack Then
            onload_GetLangCap()
            GetUserLoc()
            BindReportNameList()
            BindPeriodList()

            intSelIndex = Request.QueryString("SelIndex")
            lstRptname.SelectedIndex = intSelIndex

            lstDecimal.SelectedIndex = 2
            If Not Request.QueryString("Dec") = "" Then
                For intCntDec = 0 To lstDecimal.Items.Count - 1
                    If lstDecimal.Items(intCntDec).Value = Request.QueryString("Dec") Then
                        lstDecimal.SelectedIndex = intCntDec
                    End If
                Next
            End If
        Else
            LocCheck()
            lblUserLoc.Visible = False
        End If
    End Sub

    '=== For Language Caption==================================================
    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblCropTag.Text = GetCaption(objLangCap.EnumLangCap.Crop)
        lblVehicleTag.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblActivityTag.Text = GetCaption(objLangCap.EnumLangCap.Activity)

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If Trim(strLocType) = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                'Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                Exit For
            End If
        Next
    End Function
    '=====End for Language Caption ===============================================

    Protected Function GetActivePeriod(ByRef BGTPeriod As String) As String
        Dim strOppCd_GET As String = "BD_CLSSETUP_BGTPERIOD_GET"
        Dim dsperiod As New DataSet()
        Dim strParam As String

        '-------Configuring Search----------------

        strParam = "|||||" & objBDSetup.EnumPeriodStatus.Active & "|" & strLocation & "|"

        Try
            '------------ USING COMPONENTS -----------------
            intErrNo = objBDSetup.mtdGetPeriodList(strOppCd_GET, strParam, dsperiod)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_BUDGETACTIVEPERIODS&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try
        If dsperiod.Tables(0).Rows.Count > 0 Then
            BGTPeriod = dsperiod.Tables(0).Rows(0).Item("BGTPeriod")
            Return dsperiod.Tables(0).Rows(0).Item("PeriodID")
        Else
            BGTPeriod = "No Active Period"
            Return ""
        End If
    End Function

    Sub BindPeriodList()
        Dim strParam As String
        Dim objMapPath As String
        Dim intSelectedIndex As Integer

        Dim strOppCd_PeriodName_GET As String = "BD_STDRPT_PERIODNAME_GET"

        strParam = objBDSetup.EnumPeriodStatus.Active & "','" & objBDSetup.EnumPeriodStatus.Passed & "|" & strLocation & "|"
        Try
            intErrNo = objBD.mtdGetPeriodName(strOppCd_PeriodName_GET, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_BD_SELECTIONCTRL_REPORT_NAME_LIST&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("PeriodID") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("PeriodID"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("BGTPeriod") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BGTPeriod"))

            If dsForDropDown.Tables(0).Rows(intCnt).Item("PeriodID") = GetActivePeriod("") Then
                intSelectedIndex = intCnt + 1
            End If

        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("PeriodID") = 0
        dr("BGTPeriod") = "Select Period Name"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstPeriodName.DataSource = dsForDropDown.Tables(0)
        lstPeriodName.DataValueField = "PeriodID"
        lstPeriodName.DataTextField = "BGTPeriod"
        lstPeriodName.DataBind()
        lstPeriodName.SelectedIndex = intSelectedIndex

    End Sub

    Sub GetUserLoc()
        Dim strParam As String
        Dim objMapPath As String
        Dim strUserLoc As String
        Dim arrParam As Array
        Dim intCnt2 As Integer
        Dim intCnt3 As Integer

        Dim strArrUserLoc As String
        Dim strOppCd_UserLoc_GET As String = "BD_STDRPT_USERLOCATION_GET"

        strParam = "AND USERLOC.UserID = '" & strUserId & "'"
        Try
            intErrNo = objAdmin.mtdGetUserLocation(strOppCd_UserLoc_GET, strParam, objUserLoc, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_BD_SELECTIONCTRL_USERLOCATION&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To objUserLoc.Tables(0).Rows.Count - 1
            objUserLoc.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objUserLoc.Tables(0).Rows(intCnt).Item("LocCode"))
        Next intCnt

        'dr = objUserLoc.Tables(0).NewRow()
        'dr("LocCode") = "All"
        ''dr("Description") = ""
        'objUserLoc.Tables(0).Rows.InsertAt(dr, 0)

        lblLocation.Visible = True
        cblUserLoc.DataSource = objUserLoc.Tables(0)
        cblUserLoc.DataValueField = "LocCode"
        'cblUserLoc.DataTextField = "Description"
        cblUserLoc.DataBind()

        hidUserLoc.Value = Request.QueryString("UserLoc")
        strUserLoc = Request.QueryString("UserLoc")
        If Left(strUserLoc, 3) = "','" Then
            arrParam = Split(strUserLoc, "','")
        ElseIf Right(strUserLoc, 1) = "," Then
            arrParam = Split(strUserLoc, ",")
        Else
            arrParam = Split(strUserLoc, ",")
        End If

        'arrParam = Split(strUserLoc, "','")
        'response.write("<BR>strUserLoc"& strUserLoc)
        'response.write("<BR>"& cblUserLoc.Items(intcnt2).value &" >> "&arrParam(1))
        If Not hidUserLoc.Value = "" Then
            For intCnt2 = 0 To cblUserLoc.Items.Count - 1
                For intCnt3 = 0 To arrParam.GetUpperBound(0)
                    If Trim(cblUserLoc.Items(intCnt2).Value) = Trim(arrParam(intCnt3)) Then
                        'response.write("<BR>"& cblUserLoc.Items(intcnt2).value &" === "&arrParam(intcnt3))
                        cblUserLoc.Items(intCnt2).Selected = True
                    End If
                Next intCnt3
            Next intCnt2
            'Else
            'lblUserLoc.Visible = True
            'Exit Sub
        End If
    End Sub

    Sub Check_Clicked(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntLocAll As Integer = 0

        If cbLocAll.Checked Then
            For intCntLocAll = 0 To cblUserLoc.Items.Count - 1
                cblUserLoc.Items(intCntLocAll).Selected = True
            Next
        Else
            For intCntLocAll = 0 To cblUserLoc.Items.Count - 1
                cblUserLoc.Items(intCntLocAll).Selected = False
            Next

        End If
        LocCheck()
    End Sub

    Sub LocCheckList(ByVal Sender As Object, ByVal E As EventArgs)
        LocCheck()
    End Sub

    '-------Check User Selection on Location--------------
    Sub LocCheck()
        Dim intCnt2 As Integer = 0
        Dim tempUserLoc As String
        Dim txt As HtmlInputHidden

        'For intCnt2 = 0 To cblUserLoc.Items.Count - 1
        '    If cblUserLoc.Items(0).Selected Then
        '        cblUserLoc.Items(intCnt2).Selected = True
        '    Else
        '        If Not cblUserLoc.Items(intCnt2).Value = strLocation Then
        '            cblUserLoc.Items(intCnt2).Selected = False
        '        End If
        '    End If
        'Next

        For intCnt2 = 0 To cblUserLoc.Items.Count - 1
            If cblUserLoc.Items(intCnt2).Selected Then
                If cblUserLoc.Items.Count = 1 Then
                    tempUserLoc = cblUserLoc.Items(intCnt2).Text
                Else
                    tempUserLoc = tempUserLoc & "','" & cblUserLoc.Items(intCnt2).Text
                End If
            End If
        Next

        hidUserLoc.Value = tempUserLoc
    End Sub

    '-------Create Report Name Dropdownlist--------------
    Sub BindReportNameList()
        Dim strParam As String
        Dim objMapPath As String

        Dim strOppCd_StdRptName_GET As String = "BD_STDRPT_NAME_GET"

'----- #1 Start -----
        'strParam = " WHERE ReportType = '" & objGlobal.EnumStdRptType.Budgeting & "' AND Status = '" & objGlobal.EnumStdRptStatus.Active & "' ORDER BY ReportID"
        Dim strRptID As String
        
        If Session("PW_EDITION") = objSysCfg.EnumVersionType.Enterprise Then
            strRptID = ""
        Else
'----- #2 Start -----
'            strRptID = "'RPTBD1000028', 'RPTBD1000029'"
            strRptID = "'RPTBD1000029', 'RPTBD1000030'"
'----- #2 End -----
        End If
        
        If Session("PW_ISMILLWARE") = False Then
            If strRptID <> "" Then
                strRptID = strRptID & ", "
            End If
'----- #2 Start -----
'            strRptID = strRptID & "'RPTBD1000024', 'RPTBD1000025', 'RPTBD1000026', 'RPTBD1000027'"
            strRptID = strRptID & "'RPTBD1000025', 'RPTBD1000026', 'RPTBD1000027', 'RPTBD1000028'"
'----- #2 End -----
        End If
        
        If strRptID = "" Then
            strParam = " WHERE ReportType = '" & objGlobal.EnumStdRptType.Budgeting & "' AND Status = '" & objGlobal.EnumStdRptStatus.Active & "'   AND Reportid IN (Select ReportID From SH_REPORT_USER Where UserId='" & strUserId & "') ORDER BY ReportID"
        Else
            strParam = " WHERE ReportID NOT IN(" & strRptID & ") AND ReportType = '" & objGlobal.EnumStdRptType.Budgeting & "' AND Status = '" & objGlobal.EnumStdRptStatus.Active & "'   AND Reportid IN (Select ReportID From SH_REPORT_USER Where UserId='" & strUserId & "') ORDER BY ReportID"
        End If
        
'----- #1 End -----

        Try
            intErrNo = objAdmin.mtdGetStdRptName(strOppCd_StdRptName_GET, strParam, dsForDropDown, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_BD_SELECTIONCTRL_REPORT_NAME_LIST&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("ReportID") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("ReportID"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("RptName") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("RptName"))

            'If LCase(dsForDropDown.Tables(0).Rows(intCnt).Item("ReportID")) = "rptbd1000005" Then
            '    dsForDropDown.Tables(0).Rows(intCnt).Item("RptName") = lblVehicleTag.Text & " Running Details"
            'End If

            'If LCase(dsForDropDown.Tables(0).Rows(intCnt).Item("ReportID")) = "rptbd1000006" Then
            '    dsForDropDown.Tables(0).Rows(intCnt).Item("RptName") = lblVehicleTag.Text & " Running Summary"
            'End If

            'If LCase(dsForDropDown.Tables(0).Rows(intCnt).Item("ReportID")) = "rptbd1000011" Then
            '    dsForDropDown.Tables(0).Rows(intCnt).Item("RptName") = "Mature " & lblCropTag.Text & " " & lblActivityTag.Text
            'End If

            'If LCase(dsForDropDown.Tables(0).Rows(intCnt).Item("ReportID")) = "rptbd1000012" Then
            '    dsForDropDown.Tables(0).Rows(intCnt).Item("RptName") = "Unmature " & lblCropTag.Text & " " & lblActivityTag.Text
            'End If

        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("ReportID") = ""
        dr("RptName") = "Select Report Name"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstRptname.DataSource = dsForDropDown.Tables(0)
        lstRptname.DataValueField = "ReportID"
        lstRptname.DataTextField = "RptName"
        lstRptname.DataBind()
    End Sub

    Sub CheckRptName(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strRptname As String = Trim(lstRptname.SelectedItem.Value)
        Dim strSelectedIndex As String = LCase(lstRptname.SelectedItem.Value)
        Dim intSelectedIndex As Integer = lstRptname.SelectedIndex
        Dim strUserLoc As String

        strUserLoc = hidUserLoc.Value

        If strSelectedIndex = "rptbd1000002" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_AreaStmt.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000003" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_CapitalExp.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000004" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_CropProd.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000007" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_CropProdSum.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000008" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_CropDist.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000009" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_VehicleDist.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000010" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_VehRunning.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000011" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_VehRunningSum.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000012" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_VehRunningDist.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000013" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_WorkAcc.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000014" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_PlantationOH.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000015" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_PlantationOHSum.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000016" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_PlantationOHDist.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000017" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_UnmatureCrop.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000018" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_UnMatureCropSummary.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000019" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_ImMatureCropDist.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000020" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_MatureCrop.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000021" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_MatureCropDist.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000022" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_Manuring.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000023" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_FertUsage.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000024" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_GrandTotalSum.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000025" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_FFBReceivedList.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000026" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_ExtractionRateList.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000027" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_FFBProductionSum.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000028" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_FFBProductionDet.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000029" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_NurseryActivity.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        ElseIf strSelectedIndex = "rptbd1000030" Then
            Response.Redirect("../../" & strLangCode & "/reports/BD_StdRpt_NurseryActivityDist.aspx?SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc)
        End If
    End Sub
End Class

'Response.Write("<Script Language=""JavaScript"">alert('" & strParam & "');</Script>")
