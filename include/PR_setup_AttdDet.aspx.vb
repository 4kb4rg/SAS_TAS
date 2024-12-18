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

Imports agri.Admin
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl


Public Class PR_setup_AttdDet : Inherits Page

    Protected WithEvents txtAttCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents txtHour As Textbox
    Protected WithEvents cbCheckrollCostInd As CheckBox
    Protected WithEvents rdPayType As RadioButtonList
    Protected WithEvents rdDayType As RadioButtonList
    Protected WithEvents rdNotApply As RadioButton
    Protected WithEvents rdNormalDay As RadioButton
    Protected WithEvents rdRestDay As RadioButton
    Protected WithEvents rdHoliday As RadioButton
    Protected WithEvents rdAnnual As RadioButton
    Protected WithEvents rdSick As RadioButton
    Protected WithEvents cbxOT As CheckBox
    Protected WithEvents rdWorking As RadioButton
    Protected WithEvents rdAbsent As RadioButton
    Protected WithEvents rdOthers As RadioButton
    Protected WithEvents cbxRiceAllow As CheckBox
    Protected WithEvents cbxIncAllow As CheckBox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblRiceAllowed As Label
    Protected WithEvents lblIncentiveAllowed As Label

    Dim objPRSetup As New agri.PR.clsSetup
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl
    Dim objAR As New agri.GlobalHdl.clsAccessRights
    Dim objLangCap As New agri.PWSystem.clsLangCap
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objAttDs As New Object
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Dim objLangCapDs As New Object
    Dim strSelectedAttCode As String = ""
    Dim intStatus As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_ACCMONTH")
        strAccYear = Session("SS_ACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalary), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            strSelectedAttCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)

            onload_GetLangCap()
            If Not IsPostBack Then
                BindPayType()
                BindDayType()

                If strSelectedAttCode <> "" Then
                    tbcode.Value = strSelectedAttCode
                    onLoad_Display()
                Else
                    rdWorking.Checked = True
                End If
            End If
            onLoad_BindButton()
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblRiceAllowed.Text = GetCaption(objLangCap.EnumLangCap.RiceRation)
        lblIncentiveAllowed.Text = GetCaption(objLangCap.EnumLangCap.Incentive)

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_ATTENDANCECODEDET_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=pr/Setup/pr_setup_AttdDet.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function


    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_ATTENDANCE_LIST_GET_BY_ATTCODE"
        Dim strParam As String = strSelectedAttCode
        Dim intErrNo As Integer

        Try
            intErrNo = objPRSetup.mtdGetAttendance(strOpCd, _
                                                  strParam, _
                                                  objAttDs, _
                                                  True)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_ATTENDANCE_LIST_GET_BY_ATTCODE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_AttdDet.aspx")
        End Try
        If objAttDs.Tables(0).Rows.Count > 0 Then
        txtAttCode.Text = objAttDs.Tables(0).Rows(0).Item("AttCode")
        txtDescription.Text = objAttDs.Tables(0).Rows(0).Item("Description")
        txtHour.Text = FormatNumber(objAttDs.Tables(0).Rows(0).Item("Hours"), 0)

        cbCheckrollCostInd.Checked = IIf(CInt(objAttDs.Tables(0).Rows(0).Item("CheckrollCostInd")) = objPRSetup.EnumCheckrollCostInd.Yes, True, False)
        cbxOT.Checked = IIf(CInt(objAttDs.Tables(0).Rows(0).Item("OTAllowed")) = objPRSetup.EnumOTAllowed.Yes, True, False)

        cbxRiceAllow.Checked = IIf(CInt(objAttDs.Tables(0).Rows(0).Item("RiceInd")) = objPRSetup.EnumRiceRationAllowed.Yes, True, False)

        SelectedPayType(objAttDs.Tables(0).Rows(0).Item("PayType"))
        SelectedDayType(objAttDs.Tables(0).Rows(0).Item("DayType"))
        SelectedCountDayType(objAttDs.Tables(0).Rows(0).Item("CountDayType"))

        If rdPayType.SelectedItem.Value = objPRSetup.EnumPayType.DailyRate Then
            cbxIncAllow.Checked = IIf(CInt(objAttDs.Tables(0).Rows(0).Item("IncentiveInd")) = objPRSetup.EnumIncentiveAllowed.Yes, True, False)
        End If

        If cbxOT.Checked Then
            rdNormalDay.Checked = IIf(CInt(objAttDs.Tables(0).Rows(0).Item("OTClaimType")) = objPRSetup.EnumOTClaimType.OTClaimNormal, True, False)
            rdRestDay.Checked = IIf(CInt(objAttDs.Tables(0).Rows(0).Item("OTClaimType")) = objPRSetup.EnumOTClaimType.OTClaimRestDay, True, False)
            rdHoliday.Checked = IIf(CInt(objAttDs.Tables(0).Rows(0).Item("OTClaimType")) = objPRSetup.EnumOTClaimType.OTClaimHoliday, True, False)
        Else
            rdNotApply.Checked = True
        End If

        intStatus = CInt(objAttDs.Tables(0).Rows(0).Item("Status"))
        lblHiddenSts.Text = objAttDs.Tables(0).Rows(0).Item("Status")
        lblStatus.Text = objPRSetup.mtdGetAttListStatus(objAttDs.Tables(0).Rows(0).Item("Status"))
        lblDateCreated.Text = objGlobal.GetLongDate(objAttDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objAttDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = objAttDs.Tables(0).Rows(0).Item("UserName")
        End If
        onLoad_BindButton()
    End Sub

    Sub BindPayType()

        rdPayType.Items.Add(New ListItem(objPRSetup.mtdGetPayType(objPRSetup.EnumPayType.DailyRate), objPRSetup.EnumPayType.DailyRate))
        rdPayType.Items.Add(New ListItem(objPRSetup.mtdGetPayType(objPRSetup.EnumPayType.PieceRate), objPRSetup.EnumPayType.PieceRate))
        rdPayType.Items.Add(New ListItem(objPRSetup.mtdGetPayType(objPRSetup.EnumPayType.MonthlyRate), objPRSetup.EnumPayType.MonthlyRate))
        rdPayType.Items.Add(New ListItem(objPRSetup.mtdGetPayType(objPRSetup.EnumPayType.NoRate), objPRSetup.EnumPayType.NoRate))

        rdPayType.SelectedIndex = 0
    End Sub

    Sub BindDayType()

        rdDayType.Items.Add(New ListItem(objPRSetup.mtdGetDayType(objPRSetup.EnumDayType.Normal), objPRSetup.EnumDayType.Normal))
        rdDayType.Items.Add(New ListItem(objPRSetup.mtdGetDayType(objPRSetup.EnumDayType.Off), objPRSetup.EnumDayType.Off))
        rdDayType.Items.Add(New ListItem(objPRSetup.mtdGetDayType(objPRSetup.EnumDayType.Holiday), objPRSetup.EnumDayType.Holiday))

        rdDayType.SelectedIndex = 0
    End Sub


    Sub SelectedPayType(ByVal pv_strPayType As String)

        If pv_strPayType = objPRSetup.EnumPayType.DailyRate Then
            rdPayType.SelectedIndex = 0
        ElseIf pv_strPayType = objPRSetup.EnumPayType.PieceRate Then
            rdPayType.SelectedIndex = 1
        ElseIf pv_strPayType = objPRSetup.EnumPayType.MonthlyRate Then
            rdPayType.SelectedIndex = 2
        ElseIf pv_strPayType = objPRSetup.EnumPayType.NoRate Then
            rdPayType.SelectedIndex = 3
        End If

    End Sub

    Sub SelectedDayType(ByVal pv_strDayType As String)
        If pv_strDayType = objPRSetup.EnumDayType.Normal Then
            rdDayType.SelectedIndex = 0
        ElseIf pv_strDayType = objPRSetup.EnumDayType.Off Then
            rdDayType.SelectedIndex = 1
        ElseIf pv_strDayType = objPRSetup.EnumDayType.Holiday Then
            rdDayType.SelectedIndex = 2
        End If
    End Sub

    Sub SelectedCountDayType(ByVal pv_strCountDayType As String)
        If pv_strCountDayType = objPRSetup.EnumCountDayType.Working Then
            rdWorking.Checked = True
        ElseIf pv_strCountDayType = objPRSetup.EnumCountDayType.Absent Then
            rdAbsent.Checked = True
        ElseIf pv_strCountDayType = objPRSetup.EnumCountDayType.Others Then
            rdOthers.Checked = True
        ElseIf pv_strCountDayType = objPRSetup.EnumCountDayType.Annual Then
            rdAnnual.Checked = True
        ElseIf pv_strCountDayType = objPRSetup.EnumCountDayType.Sick Then
            rdSick.Checked = True
        End If
    End Sub

    Sub onLoad_BindButton()
        txtAttCode.Enabled = False
        txtDescription.Enabled = False
        txtHour.Enabled = False
        cbCheckrollCostInd.Enabled = False
        rdPayType.Enabled = False
        rdDayType.Enabled = False
        cbxOT.Enabled = False
        cbxIncAllow.Enabled = False
        rdNotApply.Checked = True
        rdNotApply.Enabled = False
        rdNormalDay.Enabled = False
        rdRestDay.Enabled = False
        rdHoliday.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objPRSetup.EnumAttStatus.Active
                txtDescription.Enabled = True
                txtHour.Enabled = True
                cbCheckrollCostInd.Enabled = True
                rdPayType.Enabled = True
                rdDayType.Enabled = True
                rdAnnual.Enabled = True 
                rdSick.Enabled = True 
                cbxOT.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

                If rdPayType.SelectedItem.Value = objPRSetup.EnumPayType.DailyRate Then
                    cbxIncAllow.Enabled = True
                Else
                    cbxIncAllow.Enabled = False
                    cbxIncAllow.Checked = False
                End If

                If rdAbsent.Checked Then
                    txtHour.Text = 0
                    txtHour.Enabled = False
                    rdDayType.SelectedIndex = 0
                    rdDayType.Enabled = False
                    cbxOT.Enabled = False
                    cbxOT.Checked = False
                    displayOTClaim()
                    cbxIncAllow.Enabled = False
                    cbxIncAllow.Checked = False
                Else
                    cbxIncAllow.Enabled = True
                End If

                If cbxOT.Checked Then
                    rdNormalDay.Enabled = True
                    rdRestDay.Enabled = True
                    rdHoliday.Enabled = True
                End If

            Case objPRSetup.EnumAttStatus.Deleted
                cbxRiceAllow.Enabled = False
                rdWorking.Enabled = False
                rdAbsent.Enabled = False
                rdOthers.Enabled = False
                rdAnnual.Enabled = False 
                rdSick.Enabled = False 
                UnDelBtn.Visible = True
            Case Else
                txtAttCode.Enabled = True
                txtDescription.Enabled = True
                txtHour.Enabled = True
                cbCheckrollCostInd.Enabled = True
                rdPayType.Enabled = True
                rdDayType.Enabled = True
                cbxOT.Enabled = True

                If rdPayType.SelectedItem.Value = objPRSetup.EnumPayType.DailyRate Then
                    cbxIncAllow.Enabled = True
                Else
                    cbxIncAllow.Enabled = False
                    cbxIncAllow.Checked = False
                End If

                If rdAbsent.Checked Then
                    txtHour.Text = 0
                    txtHour.Enabled = False
                    rdDayType.SelectedIndex = 0
                    rdDayType.Enabled = False
                    cbxOT.Enabled = False
                    cbxOT.Checked = False
                    displayOTClaim()
                    cbxIncAllow.Enabled = False
                    cbxIncAllow.Checked = False
                Else
                    cbxIncAllow.Enabled = True
                End If

                If cbxOT.Checked Then
                    rdNormalDay.Enabled = True
                    rdRestDay.Enabled = True
                    rdHoliday.Enabled = True
                End If

                SaveBtn.Visible = True
        End Select
    End Sub

    Sub displayOTClaimType(ByVal Sender As Object, ByVal E As EventArgs)
        displayOTClaim()
    End Sub

    Sub displayOTClaim()
        rdNotApply.Enabled = False
        rdNormalDay.Enabled = False
        rdRestDay.Enabled = False
        rdHoliday.Enabled = False

        rdNotApply.Checked = False
        rdNormalDay.Checked = False
        rdRestDay.Checked = False
        rdHoliday.Checked = False

        If cbxOT.Checked Then
            rdNormalDay.Checked = True
            rdNormalDay.Enabled = True
            rdRestDay.Enabled = True
            rdHoliday.Enabled = True
        Else
            rdNotApply.Checked = True
        End If
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_CLSSETUP_ATTENDANCE_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_ATTENDANCE_LIST_GET_BY_ATTCODE"
        Dim strOpCd_Add As String = "PR_CLSSETUP_ATTENDANCE_LIST_ADD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""
        Dim strCheckrollCostInd As String
        Dim strOTAllowed As String
        Dim strOTClaimType As String
        Dim strCountDayType As String
        Dim strAbsentDay As String
        Dim strRiceRationAllowed As String
        Dim strIncentiveAllowed As String

        If strCmdArgs = "Save" Then
            strParam = txtAttCode.Text.Trim
            Try
                intErrNo = objPRSetup.mtdGetAttendance(strOpCd_Get, _
                                                       strParam, _
                                                       objAttDs, _
                                                       True)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_ATTENDANCE_GET_BY_ATTCODE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_AttdDet.aspx")
            End Try

            If objAttDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDup.Visible = True
                Exit Sub
            Else
                strSelectedAttCode = txtAttCode.Text.Trim
                blnIsUpdate = IIf(intStatus = 0, False, True)
                tbcode.Value = strSelectedAttCode

                If rdNormalDay.Checked Then
                    strOTClaimType = objPRSetup.EnumOTClaimType.OTClaimNormal
                ElseIf rdRestDay.Checked Then
                    strOTClaimType = objPRSetup.EnumOTClaimType.OTClaimRestDay
                ElseIf rdHoliday.Checked Then
                    strOTClaimType = objPRSetup.EnumOTClaimType.OTClaimHoliday
                Else
                    strOTClaimType = objPRSetup.EnumOTClaimType.NotApplicable
                End If

                If rdWorking.Checked Then
                    strCountDayType = objPRSetup.EnumCountDayType.Working
                ElseIf rdAbsent.Checked Then
                    strCountDayType = objPRSetup.EnumCountDayType.Absent
                ElseIf rdOthers.Checked Then
                    strCountDayType = objPRSetup.EnumCountDayType.Others
                ElseIf rdAnnual.Checked Then
                    strCountDayType = objPRSetup.EnumCountDayType.Annual
                ElseIf rdSick.Checked Then
                    strCountDayType = objPRSetup.EnumCountDayType.Sick
                End If

                strCheckrollCostInd = IIf(cbCheckrollCostInd.Checked, objPRSetup.EnumCheckrollCostInd.Yes, objPRSetup.EnumCheckrollCostInd.No)
                strOTAllowed = IIf(cbxOT.Checked, objPRSetup.EnumOTAllowed.Yes, objPRSetup.EnumOTAllowed.No)
                strRiceRationAllowed = IIf(cbxRiceAllow.Checked, objPRSetup.EnumRiceRationAllowed.Yes, objPRSetup.EnumRiceRationAllowed.No)
                strIncentiveAllowed = IIf(cbxIncAllow.Checked, objPRSetup.EnumIncentiveAllowed.Yes, objPRSetup.EnumIncentiveAllowed.No)

                strParam = txtAttCode.Text.Trim & "|" & _
                           txtDescription.Text.Trim & "|" & _
                           txtHour.Text.Trim & "|" & _
                           strCheckrollCostInd & "|" & _
                           rdPayType.SelectedItem.Value & "|" & _
                           rdDayType.SelectedItem.Value & "|" & _
                           strOTAllowed & "|" & _
                           strOTClaimType & "|" & _
                           strCountDayType & "|" & _
                           strRiceRationAllowed & "|" & _
                           strIncentiveAllowed & "|" & _
                           objPRSetup.EnumAttStatus.Active & "|"

                Try
                    intErrNo = objPRSetup.mtdUpdAttendance(strOpCd_Add, _
                                                           strOpCd_Upd, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strParam, _
                                                           blnIsUpdate)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_ATTENDANCE_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_AttdDet.aspx")
                End Try
            End If
        ElseIf strCmdArgs = "Del" Then
            strParam = txtAttCode.Text.Trim & "|||||||||||" & objPRSetup.EnumAttStatus.Deleted
            Try
                intErrNo = objPRSetup.mtdUpdAttendance(strOpCd_Add, _
                                                       strOpCd_Upd, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strParam, _
                                                       True)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_ATTENDANCE_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_AttdDet.aspx?tbcode=" & strSelectedAttCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParam = txtAttCode.Text.Trim & "|||||||||||" & objPRSetup.EnumAttStatus.Active
            Try
                intErrNo = objPRSetup.mtdUpdAttendance(strOpCd_Add, _
                                                       strOpCd_Upd, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strParam, _
                                                       True)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_ATTENDANCE_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_AttdDet.aspx?tbcode=" & strSelectedAttCode)
            End Try
        End If

        If strSelectedAttCode <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_AttdList.aspx")
    End Sub

End Class
