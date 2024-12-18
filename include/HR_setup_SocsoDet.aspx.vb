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

Imports agri.HR
Imports agri.PR
Imports agri.GlobalHdl

Public Class HR_setup_SocsoDet : Inherits Page

    Protected WithEvents txtSocsoCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents ddlEmprDeductCode As DropDownList
    Protected WithEvents ddlEmpeDeductCode As DropDownList

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents txtIncomeFrom As Textbox
    Protected WithEvents txtIncomeTo As Textbox
    Protected WithEvents txtEmprContributePercent As Textbox
    Protected WithEvents txtEmprContributeAmount As Textbox
    Protected WithEvents txtEmpeContributePercent As Textbox
    Protected WithEvents txtEmpeContributeAmount As Textbox

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents Socsocode As HtmlInputHidden
    Protected WithEvents btnFind1 As HtmlInputButton
    Protected WithEvents btnFind2 As HtmlInputButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHidIncomeRange As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrEmployer As Label
    Protected WithEvents lblErrEmployee As Label
    Protected WithEvents lblErrEmployerPercent As Label
    Protected WithEvents lblErrEmployeePercent As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrRange As Label
    Protected WithEvents lblErrConsistent As Label
    Protected WithEvents lblErrEmprDeductCode As Label
    Protected WithEvents lblErrEmpeDeductCode As Label
    Protected WithEvents lblInvalidRange As Label

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objSocsoDs As New Object()
    Dim objSocsoLnDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Dim strSelectedSocsoCode As String = ""
    Dim intStatus As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBank), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrEmployerPercent.Visible = False
            lblErrEmployeePercent.Visible = False
            lblErrEmployee.Visible = False
            lblErrEmployer.Visible = False
            lblErrDup.Visible = False
            lblErrRange.Visible = False
            lblErrConsistent.Visible = False
            lblInvalidRange.Visible = False
            lblErrEmprDeductCode.Visible = False
            lblErrEmpeDeductCode.Visible = False
            strSelectedSocsoCode = Trim(IIf(Request.QueryString("socsocode") <> "", Request.QueryString("socsocode"), Request.Form("socsocode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedSocsoCode <> "" Then
                    socsocode.value = strSelectedSocsoCode
                    onLoad_Display()
                    onLoad_DisplayLine()
                    onLoad_BindButton()
                Else
                    BindAD("","")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Sub onLoad_BindButton()
        txtSocsoCode.Enabled = False
        txtDescription.Enabled = False
        ddlEmprDeductCode.Enabled = False
        ddlEmpeDeductCode.Enabled = False
        tblSelection.Visible = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        btnFind1.Disabled = False
        btnFind2.Disabled = False

        Select Case intStatus
            Case objHRSetup.EnumSocsoStatus.Active
                txtDescription.Enabled = True
                ddlEmprDeductCode.Enabled = True
                ddlEmpeDeductCode.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumSocsoStatus.Deleted
                btnFind1.Disabled = True
                btnFind2.Disabled = True
                UnDelBtn.Visible = True
            Case Else
                txtSocsoCode.Enabled = True
                txtDescription.Enabled = True
                ddlEmprDeductCode.Enabled = True
                ddlEmpeDeductCode.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
        End Select
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_SOCSO_GET"
        Dim strParam As String = strSelectedSocsoCode        
        Dim intErrNo As Integer

        Try
            intErrNo = objHRSetup.mtdGetSocso(strOpCd, _
                                              strParam, _
                                              objSocsoDs, _
                                              True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SOCSO_GET&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_socsolist.aspx")
        End Try

        txtSocsoCode.Text = strSelectedSocsoCode
        txtDescription.Text = Trim(objSocsoDs.Tables(0).Rows(0).Item("Description"))
        intStatus = CInt(Trim(objSocsoDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objSocsoDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objHRSetup.mtdGetSocsoStatus(Trim(objSocsoDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objSocsoDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objSocsoDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objSocsoDs.Tables(0).Rows(0).Item("UserName"))

        BindAD(Trim(objSocsoDs.Tables(0).Rows(0).Item("EmprDeductCode")), Trim(objSocsoDs.Tables(0).Rows(0).Item("EmpeDeductCode")))
    End Sub

    Sub onLoad_DisplayLine()
        Dim strOpCd_GetLine As String = "HR_CLSSETUP_SOCSO_LINE_GET"
        Dim strParam As String = strSelectedSocsoCode
        Dim lbButton As LinkButton
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Try
            intErrNo = objHRSetup.mtdGetSocso(strOpCd_GetLine, _
                                              strParam, _
                                              objSocsoLnDs, _
                                              True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SOCSOLINE_GET&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_socsolist.aspx")
        End Try

        For intCnt = 0 To objSocsoLnDs.Tables(0).Rows.Count - 1
            objSocsoLnDs.Tables(0).Rows(intCnt).Item("SocsoLnId") = Trim(objSocsoLnDs.Tables(0).Rows(intCnt).Item("SocsoLnId"))
            objSocsoLnDs.Tables(0).Rows(intCnt).Item("EmprContribute") = Trim(objSocsoLnDs.Tables(0).Rows(intCnt).Item("EmprContribute"))
            objSocsoLnDs.Tables(0).Rows(intCnt).Item("EmpeContribute") = Trim(objSocsoLnDs.Tables(0).Rows(intCnt).Item("EmpeContribute"))
            objSocsoLnDs.Tables(0).Rows(intCnt).Item("IncomeRange") = objSocsoLnDs.Tables(0).Rows(intCnt).Item("IncomeFr") & " - " & objSocsoLnDs.Tables(0).Rows(intCnt).Item("IncomeTo")
            lblHidIncomeRange.Text = lblHidIncomeRange.Text & Chr(9) & _
                                     objSocsoLnDs.Tables(0).Rows(intCnt).Item("IncomeFr") & "|" & _
                                     objSocsoLnDs.Tables(0).Rows(intCnt).Item("IncomeTo")
        Next intCnt

        If objSocsoLnDs.Tables(0).Rows.Count > 0 Then
            If CDbl(objSocsoLnDs.Tables(0).Rows(0).Item("EmprContribute")) = "0" Then
                txtEmprContributeAmount.Enabled = True
                txtEmpeContributeAmount.Enabled = True
                txtEmprContributePercent.Enabled = False
                txtEmpeContributePercent.Enabled = False
            Else
                txtEmprContributeAmount.Enabled = False
                txtEmpeContributeAmount.Enabled = False
                txtEmprContributePercent.Enabled = True
                txtEmpeContributePercent.Enabled = True
            End If
        Else
            txtEmprContributeAmount.Enabled = True
            txtEmpeContributeAmount.Enabled = True
            txtEmprContributePercent.Enabled = True
            txtEmpeContributePercent.Enabled = True            
        End If

        dgLineDet.DataSource = objSocsoLnDs.Tables(0)
        dgLineDet.DataBind()

        For intCnt = 0 To dgLineDet.Items.Count - 1
            Select Case intStatus
                Case objHRSetup.EnumSocsoStatus.Active
                        lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objHRSetup.EnumSocsoStatus.Deleted
                        lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
            End Select
        Next
        
    End Sub

    Sub BindAD(ByVal pv_strEmpr As String, ByVal pv_strEmpe As String)
        Dim strOpCode As String = "PR_CLSSETUP_ADLIST_GET"
        Dim objADrDs As New Dataset()
        Dim objADeDs As New Dataset()
        Dim dr As DataRow
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectEmpr As Integer = 0
        Dim intSelectEmpe As Integer = 0

        Try
            strParam = "AND AD.ADType = '" & objPRSetup.EnumADType.MemoItem & "'"
            intErrNo = objPRSetup.mtdGetAD(strOpCode, _
                                           strParam, _
                                           objADrDs, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SOCSO_AD_EMPLOYER_GET&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_socsolist.aspx")
        End Try

        Try
            strParam = "AND AD.ADType = '" & objPRSetup.EnumADType.Deduction & "'"
            intErrNo = objPRSetup.mtdGetAD(strOpCode, _
                                           strParam, _
                                           objADeDs, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SOCSO_AD_EMPLOYEE_GET&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_socsolist.aspx")
        End Try
        
        For intCnt = 0 To objADrDs.Tables(0).Rows.Count - 1
            objADrDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(objADrDs.Tables(0).Rows(intCnt).Item("ADCode"))
            objADrDs.Tables(0).Rows(intCnt).Item("Description") = objADrDs.Tables(0).Rows(intCnt).Item("ADCode") & " (" & Trim(objADrDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objADrDs.Tables(0).Rows(intCnt).Item("ADCode") = pv_strEmpr Then
                intSelectEmpr = intCnt + 1
            End If
        Next

        For intCnt = 0 To objADeDs.Tables(0).Rows.Count - 1
            objADeDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(objADeDs.Tables(0).Rows(intCnt).Item("ADCode"))
            objADeDs.Tables(0).Rows(intCnt).Item("Description") = objADeDs.Tables(0).Rows(intCnt).Item("ADCode") & " (" & Trim(objADeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objADeDs.Tables(0).Rows(intCnt).Item("ADCode") = pv_strEmpe Then
                intSelectEmpe = intCnt + 1
            End If
        Next

        dr = objADrDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("Description") = "Select Allowance & Deduction Code"
        objADrDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmprDeductCode.DataSource = objADrDs.Tables(0)
        ddlEmprDeductCode.DataValueField = "ADCode"
        ddlEmprDeductCode.DataTextField = "Description"
        ddlEmprDeductCode.DataBind()
        ddlEmprDeductCode.SelectedIndex = intSelectEmpr

        dr = objADeDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("Description") = "Select Allowance & Deduction Code"
        objADeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpeDeductCode.DataSource = objADeDs.Tables(0)
        ddlEmpeDeductCode.DataValueField = "ADCode"
        ddlEmpeDeductCode.DataTextField = "Description"
        ddlEmpeDeductCode.DataBind()
        ddlEmpeDeductCode.SelectedIndex = intSelectEmpe
    End Sub

    Sub InsertSocsoRecord(ByRef pv_blnIsFail As Boolean)
        Dim strOpCd_Add As String = "HR_CLSSETUP_SOCSO_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_SOCSO_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_SOCSO_GET"
        Dim strEmprAD As String = Request.Form("ddlEmprDeductCode")
        Dim strEmpeAD As String = Request.Form("ddlEmpeDeductCode")
        Dim strOpCd As String
        Dim intErrNo As Integer
        Dim strParam As String = ""

        pv_blnIsFail = True

        If strEmprAD = "" Then
            lblErrEmprDeductCode.Visible = True
            Exit Sub
        ElseIf strEmpeAD = "" Then
            lblErrEmpeDeductCode.Visible = True
            Exit Sub
        End If

        strParam = Trim(txtSocsoCode.Text)
        Try
            intErrNo = objHRSetup.mtdGetSocso(strOpCd_Get, _
                                                strParam, _
                                                objSocsoDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SOCSO_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objSocsoDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
            lblErrDup.Visible = True
            Exit Sub
        Else
            strSelectedSocsoCode = Trim(txtSocsoCode.Text)
            socsocode.value = strSelectedSocsoCode
            strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)

            strParam = Trim(txtSocsoCode.Text) & "|" & _
                       Trim(txtDescription.Text) & "|" & _
                       strEmprAD & "|" & _
                       strEmpeAD & "|" & _
                       objHRSetup.EnumSocsoStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdSocso(strOpCd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SOCSO_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_SocsoDet.aspx?Socsocode=" & strSelectedSocsoCode)
            End Try
            pv_blnIsFail = False
        End If        
    End Sub

    Sub AddBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim blnIsFail As Boolean
        Dim strOpCode_AddLine As String = "HR_CLSSETUP_SOCSO_LINE_ADD"
        Dim strOpCode_UpdID As String = "HR_CLSSETUP_SOCSO_UPDATEID_UPD"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim arrRange As Array
        Dim arrIncome As Array
        Dim dblIncomeFrom As Double
        Dim dblIncomeTo As Double


        If strSelectedSocsoCode = "" And Trim(txtSocsoCode.Text) = "" Then
            Exit Sub
        Else
            InsertSocsoRecord(blnIsFail)
            If blnIsFail = True Then
                Exit Sub
            End If

            If CDbl(txtIncomeFrom.Text) > CDbl(txtIncomeTo.Text) Then
                lblErrRange.Visible = True
                onLoad_Display()
                onLoad_DisplayLine()
                onLoad_BindButton()
                Exit Sub
            End If

            If (txtEmprContributePercent.Text = "" And txtEmprContributeAmount.Text = "") Or _
               (txtEmprContributePercent.Text <> "0" And txtEmprContributeAmount.Text <> "0") Or _
               (txtEmprContributePercent.Text = "0" And txtEmprContributeAmount.Text = "0") Then
                lblErrEmployer.Visible = True
                onLoad_Display()
                onLoad_DisplayLine()
                onLoad_BindButton()
                Exit Sub
            ElseIf txtEmprContributePercent.Text <> "" Then
                If CInt(txtEmprContributePercent.Text) > 100 Then
                    lblErrEmployerPercent.Visible = True
                    onLoad_Display()
                    onLoad_DisplayLine()
                    onLoad_BindButton()
                    Exit Sub
                End If
            End If

            If (txtEmpeContributePercent.Text = "" And txtEmpeContributeAmount.Text = "") Or _
               (txtEmpeContributePercent.Text <> "0" And txtEmpeContributeAmount.Text <> "0") Or _
               (txtEmpeContributePercent.Text = "0" And txtEmpeContributeAmount.Text = "0") Then
                lblErrEmployee.Visible = True
                onLoad_Display()
                onLoad_DisplayLine()
                onLoad_BindButton()
                Exit Sub
            ElseIf txtEmpeContributePercent.Text <> "" Then
                If CInt(txtEmpeContributePercent.Text) > 100 Then
                    lblErrEmployeePercent.Visible = True
                    onLoad_Display()
                    onLoad_DisplayLine()
                    onLoad_BindButton()
                    Exit Sub
                End If
            End If

            If (txtEmprContributePercent.Text <> "0" And txtEmpeContributePercent.Text = "0") Or _
                (txtEmprContributePercent.Text = "0" And txtEmpeContributePercent.Text <> "0") Or _
                (txtEmprContributeAmount.Text <> "0" And txtEmpeContributeAmount.Text = "0") Or _
                (txtEmprContributeAmount.Text = "0" And txtEmpeContributeAmount.Text <> "0") Then
                lblErrConsistent.Visible = True
                Exit Sub
            End If

            arrRange = Split(lblHidIncomeRange.Text, Chr(9))
            If UBound(arrRange, 1) > 0 Then
                For intCnt = 1 To UBound(arrRange, 1) 
                    arrIncome = Split(arrRange(intCnt), "|")
                    dblIncomeFrom = CDbl(arrIncome(0))
                    dblIncomeTo = CDbl(arrIncome(1))
                    If (dblIncomeTo >= CDbl(txtIncomeFrom.Text)) Then
                        lblInvalidRange.visible = True
                        Exit Sub
                    End If
                Next
            End If

            Try
                strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.SocsoLn) & "||" & _
                           strSelectedSocsoCode & "|" & _
                           txtIncomeFrom.Text & "|" & _
                           txtIncomeTo.Text & "|" & _
                           txtEmprContributePercent.Text & "|" & _
                           txtEmprContributeAmount.Text & "|" & _
                           txtEmpeContributePercent.Text & "|" & _
                           txtEmpeContributeAmount.Text

                intErrNo = objHRSetup.mtdUpdSocsoLine(strOpCode_UpdID, _
                                                      strOpCode_AddLine, _
                                                      strCompany, _
                                                      strLocation, _
                                                      strUserId, _
                                                      strParam, _
                                                      False, _
                                                      objResult)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_HSLINE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_SocsoDet.aspx?Socsocode=" & strSelectedSocsoCode)
            End Try
        End If

        onLoad_Display()
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "HR_CLSSETUP_SOCSO_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_SOCSO_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_SOCSO_GET"
        Dim strOpCd_Sts As String = "HR_CLSSETUP_SOCSO_STATUS_UPD"
        Dim blnIsFail As Boolean
        Dim strOpCd As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim strParam As String = ""

        If strCmdArgs = "Save" Then
            InsertSocsoRecord(blnIsFail)
        ElseIf strCmdArgs = "Del" Then
            strParam = strSelectedSocsoCode & "|" & objHRSetup.EnumSocsoStatus.Deleted
            Try
                intErrNo = objHRSetup.mtdUpdSocso(strOpCd_Sts, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SOCSO_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_SocsoDet.aspx?Socsocode=" & strSelectedSocsoCode)
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = strSelectedSocsoCode & "|" & objHRSetup.EnumSocsoStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdSocso(strOpCd_Sts, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SOCSO_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_SocsoDet.aspx?Socsocode=" & strSelectedSocsoCode)
            End Try
        End If

        If strSelectedSocsoCode <> "" Then
            onLoad_Display()
            onLoad_DisplayLine()
            onLoad_BindButton()
        End If
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "HR_CLSSETUP_SOCSO_LINE_DEL"
        Dim strOpCode_UpdID As String = "HR_CLSSETUP_SOCSO_UPDATEID_UPD"
        Dim strParam As String
        Dim lblDelText As Label
        Dim strSocsoLnId As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("SocsoLnId")
        strSocsoLnId = lblDelText.Text

        Try
            strParam = "|" & strSocsoLnId & "|||||||"
            intErrNo = objHRSetup.mtdUpdSocsoLine(strOpCode_UpdID, _
                                                strOpCode_DelLine, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                False, _
                                                objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SOCSOLINE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_SocsoDet.aspx?Socsocode=" & strSelectedSocsoCode)
        End Try

        lblHidIncomeRange.text = ""
        onLoad_Display()
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_SocsoList.aspx")
    End Sub


End Class
