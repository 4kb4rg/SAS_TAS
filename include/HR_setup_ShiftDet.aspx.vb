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

Public Class HR_setup_ShiftDet : Inherits Page

    Protected WithEvents txtShiftCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents ddlAD As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents ddlShift As DropDownList
    Protected WithEvents txtAllowance As TextBox
    Protected WithEvents btnFind1 As HtmlInputButton
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents shiftcode As HtmlInputHidden
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrAD As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrShift As Label
    Protected WithEvents lblErrAllowance As Label
    Protected WithEvents lblActiveShift As Label

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objShiftDs As New Object()
    Dim objShiftLnDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim strSelShiftCode As String = ""
    Dim intStatus As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRShift), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrAD.Visible = False
            lblErrDup.Visible = False
            lblErrShift.Visible = False
            lblErrAllowance.Visible = False
            strSelShiftCode = Trim(IIf(Request.QueryString("shiftcode") <> "", Request.QueryString("shiftcode"), Request.Form("shiftcode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelShiftCode <> "" Then
                    shiftcode.Value = strSelShiftCode
                    onLoad_Display()
                    onLoad_LineDisplay()
                    BindShift()
                    onLoad_BindButton()
                Else
                    BindAD("")
                    BindShift()
                    onLoad_BindButton()
                End If
            Else
                BindShift()
            End If
        End If
    End Sub

    Sub onLoad_BindButton()
        txtShiftCode.Enabled = False
        txtDescription.Enabled = False
        ddlAD.Enabled = False
        tblSelection.Visible = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        btnFind1.Disabled = False

        Select Case intStatus
            Case objHRSetup.EnumShiftStatus.Active
                txtDescription.Enabled = True
                ddlAD.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumShiftStatus.Deleted
                btnFind1.Disabled = True
                UnDelBtn.Visible = True
            Case Else
                txtShiftCode.Enabled = True
                txtDescription.Enabled = True
                ddlAD.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_SHIFT_ALL_GET"
        Dim strParam As String = strSelShiftCode & "|"
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Try
            intErrNo = objHRSetup.mtdGetShift(strOpCd, _
                                            strParam, _
                                            objShiftDs, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SHIFT_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        txtShiftCode.Text = objShiftDs.Tables(0).Rows(0).Item("ShiftCode").Trim()
        txtDescription.Text = objShiftDs.Tables(0).Rows(0).Item("Description").Trim()
        BindAD(objShiftDs.Tables(0).Rows(0).Item("ADCode").Trim())
        intStatus = CInt(objShiftDs.Tables(0).Rows(0).Item("Status"))
        lblHiddenSts.Text = objShiftDs.Tables(0).Rows(0).Item("Status")
        lblStatus.Text = objHRSetup.mtdGetShiftStatus(objShiftDs.Tables(0).Rows(0).Item("Status"))
        lblDateCreated.Text = objGlobal.GetLongDate(objShiftDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objShiftDs.Tables(0).Rows(0).Item("UpdateDate"))
    End Sub


    Sub onLoad_LineDisplay()
        Dim strOpCd As String = "HR_CLSSETUP_SHIFT_LINE_GET"
        Dim strParam As String = strSelShiftCode & "|" & objHRSetup.EnumShiftLnStatus.Active
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        Try
            intErrNo = objHRSetup.mtdGetShift(strOpCd, _
                                            strParam, _
                                            objShiftLnDs, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SHIFTLINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        lblActiveShift.Text = ""
        For intCnt = 0 To objShiftLnDs.Tables(0).Rows.Count - 1
            lblActiveShift.Text = lblActiveShift.Text & "|" & objShiftLnDs.Tables(0).Rows(intCnt).Item("Shift")
        Next

        If Len(lblActiveShift.Text) > 0 Then
            lblActiveShift.Text = Mid(lblActiveShift.Text, 2, Len(lblActiveShift.Text) - 1)
        End If

        dgLineDet.DataSource = objShiftLnDs.Tables(0)
        dgLineDet.DataBind()

        For intCnt = 0 To dgLineDet.Items.Count - 1
            Select Case CInt(objShiftDs.Tables(0).Rows(0).Item("Status"))
                Case objHRSetup.EnumShiftStatus.Active
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objHRSetup.EnumShiftStatus.Deleted
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.visible = False
            End Select
        Next
    End Sub

    Sub BindAD(ByVal pv_strAD As String)
        Dim strOpCode As String = "PR_CLSSETUP_ADLIST_GET"
        Dim objADDs As New Dataset()
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCode, _
                                           strParam, _
                                           objADDs, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SHIFT_AD_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        
        For intCnt = 0 To objADDs.Tables(0).Rows.Count - 1
            objADDs.Tables(0).Rows(intCnt).Item("ADCode") = objADDs.Tables(0).Rows(intCnt).Item("ADCode").Trim()
            objADDs.Tables(0).Rows(intCnt).Item("Description") = objADDs.Tables(0).Rows(intCnt).Item("ADCode") & " (" & objADDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If objADDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(pv_strAD) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objADDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("Description") = "Select Allowance & Deduction"
        objADDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAD.DataSource = objADDs.Tables(0)
        ddlAD.DataValueField = "ADCode"
        ddlAD.DataTextField = "Description"
        ddlAD.DataBind()
        ddlAD.SelectedIndex = intSelectIndex
    End Sub

    Sub BindShift()
        ddlShift.Items.Clear()
        If lblActiveShift.Text = "" Then
            ddlShift.Items.Add(New ListItem(objHRSetup.mtdGetShiftName(objHRSetup.EnumShift.First), objHRSetup.EnumShift.First))
            ddlShift.Items.Add(New ListItem(objHRSetup.mtdGetShiftName(objHRSetup.EnumShift.Second), objHRSetup.EnumShift.Second))
            ddlShift.Items.Add(New ListItem(objHRSetup.mtdGetShiftName(objHRSetup.EnumShift.Third), objHRSetup.EnumShift.Third))
        Else
            If InStr(lblActiveShift.Text, objHRSetup.EnumShift.First) = 0 Then
                ddlShift.Items.Add(New ListItem(objHRSetup.mtdGetShiftName(objHRSetup.EnumShift.First), objHRSetup.EnumShift.First))
            End If

            If InStr(lblActiveShift.Text, objHRSetup.EnumShift.Second) = 0 Then
                ddlShift.Items.Add(New ListItem(objHRSetup.mtdGetShiftName(objHRSetup.EnumShift.Second), objHRSetup.EnumShift.Second))
            End If

            If InStr(lblActiveShift.Text, objHRSetup.EnumShift.Third) = 0 Then
                ddlShift.Items.Add(New ListItem(objHRSetup.mtdGetShiftName(objHRSetup.EnumShift.Third), objHRSetup.EnumShift.Third))
            End If
        End If
    End Sub

    Sub InsertShiftRecord()
        Dim strOpCd_Add As String = "HR_CLSSETUP_SHIFT_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_SHIFT_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_SHIFT_ALL_GET"
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        End If

        strParam = Trim(txtShiftCode.Text) & "|"

        Try
            intErrNo = objHRSetup.mtdGetShift(strOpCd_Get, _
                                            strParam, _
                                            objShiftDs, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SHIFT_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objShiftDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
            lblErrDup.Visible = True
            Exit Sub
        Else
            strSelShiftCode = Trim(txtShiftCode.Text)
            blnIsUpdate = IIf(intStatus = 0, False, True)
            shiftcode.Value = strSelShiftCode

            strParam = Trim(txtShiftCode.Text) & "|" & _
                        Trim(txtDescription.Text) & "|" & _
                        ddlAD.SelectedItem.Value & "|" & _
                        objHRSetup.EnumShiftStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdShift(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnIsUpdate)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SHIFT_SAVE&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ShiftDet.aspx?shiftcode=" & strSelShiftCode)
            End Try
        End If
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCd_Add As String = "HR_CLSSETUP_SHIFT_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_SHIFT_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If strCmdArgs = "Save" Then
            InsertShiftRecord()
        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(txtShiftCode.Text) & "|||" & objHRSetup.EnumShiftStatus.Deleted
            Try
                intErrNo = objHRSetup.mtdUpdShift(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SHIFT_DEL&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ShiftDet.aspx?shiftcode=" & strSelShiftCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParam = Trim(txtShiftCode.Text) & "|||" & objHRSetup.EnumShiftStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdShift(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SHIFT_UNDEL&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ShiftDet.aspx?shiftcode=" & strSelShiftCode)
            End Try
        End If

        If strSelShiftCode <> "" Then
            onLoad_Display()
            onLoad_LineDisplay()
            BindShift()
            onLoad_BindButton()
        End If
    End Sub

    Sub btnAdd_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_AddLine As String = "HR_CLSSETUP_SHIFT_LINE_ADD"
        Dim strOpCode_UpdID As String = "HR_CLSSETUP_SHIFT_UPDATEID_UPD"
        Dim strParam As String
        Dim strShift As String = Request.Form("ddlShift")
        Dim intErrNo As Integer

        If strShift = "" Then
            lblErrShift.Visible = True
            Exit Sub
        ElseIf txtAllowance.Text = "" Then
            lblErrAllowance.Visible = True
            Exit Sub
        End If

        InsertShiftRecord()

        If strSelShiftCode = "" Then
            Exit Sub
        Else
            Try
                strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.ShiftLn) & "||" & _
                           strSelShiftCode & "|" & strShift & "|" & txtAllowance.Text & "|" & _
                           objHRSetup.EnumShiftLnStatus.Active
                intErrNo = objHRSetup.mtdUpdShiftLine(strOpCode_UpdID, _
                                                    strOpCode_AddLine, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    False, _
                                                    objResult)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SHIFTLINE_UPD&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ShiftDet.aspx?shiftcode=" & strSelShiftCode)
            End Try
        End If

        onLoad_Display()
        onLoad_LineDisplay()
        BindShift()
        onLoad_BindButton()
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "HR_CLSSETUP_SHIFT_LINE_UPD"
        Dim strOpCode_UpdID As String = "HR_CLSSETUP_SHIFT_UPDATEID_UPD"
        Dim strParam As String
        Dim lblDelText As Label
        Dim strShiftLn As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCode")
        strShiftLn = lblDelText.Text

        Try
            strParam = "|" & strShiftLn & "||||" & objHRSetup.EnumShiftLnStatus.Deleted
            intErrNo = objHRSetup.mtdUpdShiftLine(strOpCode_UpdID, _
                                                strOpCode_DelLine, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                False, _
                                                objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SHIFTLINE_DEL&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ShiftDet.aspx?shiftcode=" & strSelShiftCode)
        End Try

        onLoad_Display()
        onLoad_LineDisplay()
        BindShift()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_Shiftlist.aspx")
    End Sub


End Class
