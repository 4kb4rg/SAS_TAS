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

Public Class HR_setup_HSDet : Inherits Page

    Protected WithEvents txtHSCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents ddlGPH As DropDownList
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents hscode As HtmlInputHidden
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrField As Label
    Protected WithEvents cbTHR As CheckBox
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objGPHDs As New Object()
    Dim objHSDs As New Object()
    Dim objHSLnDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Dim strSelectedHSCode As String = ""
    Dim intStatus As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRHoliday), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblErrField.Visible = False
            strSelectedHSCode = Trim(IIf(Request.QueryString("hscode") <> "", Request.QueryString("hscode"), Request.Form("hscode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedHSCode <> "" Then
                    hscode.Value = strSelectedHSCode
                    onLoad_Display()
                    onLoad_LineDisplay()
                    BindGPH()
                    onLoad_BindButton()
                Else
                    BindGPH()
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_HS_GET"
        Dim strParam As String = strSelectedHSCode '& "|" & "AND HS.Status = '" & objHRSetup.EnumGPHStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Try
            intErrNo = objHRSetup.mtdGetHolidaySchedule(strOpCd, _
                                                        strParam, _
                                                        objHSDs, _
                                                        True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_HS_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtHSCode.Text = Trim(objHSDs.Tables(0).Rows(0).Item("HSCode"))
        txtDescription.Text = Trim(objHSDs.Tables(0).Rows(0).Item("Description"))
        intStatus = CInt(Trim(objHSDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objHSDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objHRSetup.mtdGetCPStatus(Trim(objHSDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objHSDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objHSDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objHSDs.Tables(0).Rows(0).Item("UserName"))
    End Sub


    Sub onLoad_LineDisplay()
        Dim strOpCd As String = "HR_CLSSETUP_HS_LINE_GET"
        Dim strParam As String = strSelectedHSCode
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        Try
            intErrNo = objHRSetup.mtdGetHolidaySchedule(strOpCd, _
                                                        strParam, _
                                                        objHSLnDs, _
                                                        True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_HSLINE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLineDet.DataSource = objHSLnDs.Tables(0)
        dgLineDet.DataBind()
        
        For intCnt=0 To dgLineDet.Items.Count - 1
            Select Case CInt(objHSDs.Tables(0).Rows(0).Item("Status"))
                Case objHRSetup.EnumHSStatus.Active
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objHRSetup.EnumHSStatus.Deleted
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.visible = False
            End Select
        Next

    End Sub

    Sub BindGPH()
        Dim strOpCd_GPH As String = "HR_CLSSETUP_HS_GPHLIST_GET"
        Dim strParam As String 
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Try
            strParam = strSelectedHSCode & "||" & "AND Status = " & objHRSetup.EnumGPHStatus.Active
            intErrNo = objHRSetup.mtdUpdHolidayScheduleLine("", _
                                                            strOpCd_GPH, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            True, _
                                                            objGPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_HSLINE_GETGPH&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For intCnt = 0 To objGPHDs.Tables(0).Rows.Count - 1
            objGPHDs.Tables(0).Rows(intCnt).Item("GPHCode") = Trim(objGPHDs.Tables(0).Rows(intCnt).Item("GPHCode"))
            objGPHDs.Tables(0).Rows(intCnt).Item("Description") = objGPHDs.Tables(0).Rows(intCnt).Item("GPHCode") & " (" & Trim(objGPHDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next

        ddlGPH.DataSource = objGPHDs.Tables(0)
        ddlGPH.DataValueField = "GPHCode"
        ddlGPH.DataTextField = "Description"
        ddlGPH.DataBind()
    End Sub


    Sub onLoad_BindButton()
        txtHSCode.Enabled = False
        txtDescription.Enabled = False
        tblSelection.Visible = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objHRSetup.EnumHSStatus.Active
                txtDescription.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumHSStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtHSCode.Enabled = True
                txtDescription.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub InsertHSRecord()
        Dim strOpCd_Add As String = "HR_CLSSETUP_HS_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_HS_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_HS_GET"
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If Trim(txtHSCode.Text) = "" Or Trim(txtDescription.Text) = "" Then
            lblErrField.Visible = True
            Exit Sub
        End If

        strParam = Trim(txtHSCode.Text)
        Try
            intErrNo = objHRSetup.mtdGetHolidaySchedule(strOpCd_Get, _
                                                        strParam, _
                                                        objHSDs, _
                                                        True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_HS_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objHSDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
            lblErrDup.Visible = True
            Exit Sub
        Else
            strSelectedHSCode = Trim(txtHSCode.Text)
            blnIsUpdate = IIf(intStatus = 0, False, True)
            hscode.Value = strSelectedHSCode

            strParam = Trim(txtHSCode.Text) & "|" & _
                        Trim(txtDescription.Text) & "|" & _
                        objHRSetup.EnumHSStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdHolidaySchedule(strOpCd_Add, _
                                                            strOpCd_Upd, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            blnIsUpdate)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_HS_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_HSDet.aspx?hscode=" & strSelectedHSCode)
            End Try
        End If
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCd_Add As String = "HR_CLSSETUP_HS_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_HS_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_HS_GET"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If strCmdArgs = "Save" Then
            InsertHSRecord()
        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(txtHSCode.Text) & "||" & objHRSetup.EnumHSStatus.Deleted
            Try
                intErrNo = objHRSetup.mtdUpdHolidaySchedule(strOpCd_Add, _
                                                            strOpCd_Upd, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_HS_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_HSDet.aspx?hscode=" & strSelectedHSCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParam = Trim(txtHSCode.Text) & "||" & objHRSetup.EnumHSStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdHolidaySchedule(strOpCd_Add, _
                                                            strOpCd_Upd, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_HS_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_HSDet.aspx?hscode=" & strSelectedHSCode)
            End Try
        End If

        If strSelectedHSCode <> "" Then
            onLoad_Display()
            onLoad_LineDisplay()
            BindGPH()
            onLoad_BindButton()
        End If
    End Sub

    Sub btnAdd_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_AddLine As String = "HR_CLSSETUP_HS_LINE_ADD"
        Dim strOpCode_UpdID As String = "HR_CLSSETUP_HS_UPDATEID_UPD"
        Dim strParam As String
        Dim strGPHCode As String
        Dim intErrNo As Integer

        Try
            strGPHCode = ddlGPH.SelectedItem.Value
        Catch Exp As System.Exception
            Exit Sub
        End Try

        InsertHSRecord()
        If strSelectedHSCode = "" Then
            Exit Sub
        Else
            Try
                strParam = strSelectedHSCode & "|" & strGPHCode & "|"
                intErrNo = objHRSetup.mtdUpdHolidayScheduleLine(strOpCode_UpdID, _
                                                                strOpCode_AddLine, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strParam, _
                                                                False, _
                                                                objResult)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_HSLINE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_HSDet.aspx?hscode=" & strSelectedHSCode)
            End Try
        End If

        onLoad_Display()
        onLoad_LineDisplay()
        BindGPH()
        onLoad_BindButton()
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "HR_CLSSETUP_HS_LINE_DEL"
        Dim strOpCode_UpdID As String = "HR_CLSSETUP_HS_UPDATEID_UPD"
        Dim strParam As String
        Dim lblDelText As Label
        Dim strGPHCode As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCode")
        strGPHCode = lblDelText.Text

        Try
            strParam = strSelectedHSCode & "|" & strGPHCode & "|"
            intErrNo = objHRSetup.mtdUpdHolidayScheduleLine(strOpCode_UpdID, _
                                                            strOpCode_DelLine, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            False, _
                                                            objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_HSLINE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_HSDet.aspx?hscode=" & strSelectedHSCode)
        End Try

        onLoad_Display()
        onLoad_LineDisplay()
        BindGPH()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_HSlist.aspx")
    End Sub

End Class
