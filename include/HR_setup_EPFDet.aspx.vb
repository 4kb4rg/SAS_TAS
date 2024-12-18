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

Public Class HR_setup_EPFDet : Inherits Page

    Protected WithEvents txtEPFCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents ddlEmprDeductCode As DropDownList
    Protected WithEvents ddlEmpeDeductCode As DropDownList
    Protected WithEvents txtMinIncome As Textbox
    Protected WithEvents txtMaxAmount As Textbox
    Protected WithEvents txtEmprContributePercent As Textbox
    Protected WithEvents txtEmprContributeAmount As Textbox
    Protected WithEvents txtEmpeContributePercent As Textbox
    Protected WithEvents txtEmpeContributeAmount As Textbox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents epfcode As HtmlInputHidden
    Protected WithEvents btnFind1 As HtmlInputButton
    Protected WithEvents btnFind2 As HtmlInputButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrEmployer As Label
    Protected WithEvents lblErrEmployee As Label
    Protected WithEvents lblErrEmployerPercent As Label
    Protected WithEvents lblErrEmployeePercent As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrConsistent As Label
    Protected WithEvents lblErrEmprDeductCode As Label
    Protected WithEvents lblErrEmpeDeductCode As Label

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objEPFDs As New Object()
    Dim objADDsEmpr As New Object()
    Dim objADDsEmpe As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Dim strSelectedEPFcode As String = ""
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
            lblErrConsistent.Visible = False
            lblErrEmprDeductCode.Visible = False
            lblErrEmpeDeductCode.Visible = False
            strSelectedEPFcode = Trim(IIf(Request.QueryString("epfcode") <> "", Request.QueryString("epfcode"), Request.Form("epfcode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedEPFcode <> "" Then
                    epfcode.Value = strSelectedEPFcode
                    onLoad_Display()
                Else
                    BindAD("","")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Sub onLoad_BindButton()
        txtEPFCode.Enabled = False
        txtDescription.Enabled = False
        ddlEmprDeductCode.Enabled = False
        ddlEmpeDeductCode.Enabled = False
        txtMinIncome.Enabled = False
        txtMaxAmount.Enabled = False
        txtEmprContributePercent.Enabled = False
        txtEmprContributeAmount.Enabled = False
        txtEmpeContributePercent.Enabled = False
        txtEmpeContributeAmount.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        btnFind1.Disabled = False
        btnFind2.Disabled = False

        Select Case intStatus
            Case objHRSetup.EnumEPFStatus.Active
                txtDescription.Enabled = True
                ddlEmprDeductCode.Enabled = True
                ddlEmpeDeductCode.Enabled = True
                txtMinIncome.Enabled = True
                txtMaxAmount.Enabled = True
                txtEmprContributePercent.Enabled = True
                txtEmprContributeAmount.Enabled = True
                txtEmpeContributePercent.Enabled = True
                txtEmpeContributeAmount.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumEPFStatus.Deleted
                btnFind1.Disabled = True
                btnFind2.Disabled = True
                UnDelBtn.Visible = True
            Case Else
                txtEPFCode.Enabled = True
                txtDescription.Enabled = True
                ddlEmprDeductCode.Enabled = True
                ddlEmpeDeductCode.Enabled = True
                txtMinIncome.Enabled = True
                txtMaxAmount.Enabled = True
                txtEmprContributePercent.Enabled = True
                txtEmprContributeAmount.Enabled = True
                txtEmpeContributePercent.Enabled = True
                txtEmpeContributeAmount.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_EPF_GET"
        Dim strParam As String = strSelectedEPFcode        
        Dim intErrNo As Integer

        Try
            intErrNo = objHRSetup.mtdGetEPF(strOpCd, _
                                            strParam, _
                                            objEPFDs, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_EPF_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtEPFCode.Text = strSelectedEPFcode
        txtDescription.Text = Trim(objEPFDs.Tables(0).Rows(0).Item("Description"))
        txtMinIncome.Text = objEPFDs.Tables(0).Rows(0).Item("MinIncome")
        txtMaxAmount.Text = objEPFDs.Tables(0).Rows(0).Item("MaxAmount")
        txtEmprContributePercent.Text = Trim(objEPFDs.Tables(0).Rows(0).Item("EmprContribute"))
        txtEmprContributeAmount.Text = Trim(objEPFDs.Tables(0).Rows(0).Item("EmprCAmt"))
        txtEmpeContributePercent.Text = Trim(objEPFDs.Tables(0).Rows(0).Item("EmpeContribute"))
        txtEmpeContributeAmount.Text = Trim(objEPFDs.Tables(0).Rows(0).Item("EmpeCAmt"))
        intStatus = CInt(Trim(objEPFDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objEPFDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objHRSetup.mtdGetEPFStatus(Trim(objEPFDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objEPFDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objEPFDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objEPFDs.Tables(0).Rows(0).Item("UserName"))

        BindAD(Trim(objEPFDs.Tables(0).Rows(0).Item("EmprDeductCode")), Trim(objEPFDs.Tables(0).Rows(0).Item("EmpeDeductCode")))
        onLoad_BindButton()
    End Sub

    Sub BindAD(ByVal pv_strEmpr As String, ByVal pv_strEmpe As String)
        Dim strOpCode As String = "PR_CLSSETUP_ADLIST_GET"
        Dim dr As DataRow
        Dim strParamEmpr As String = "AND AD.ADType = '" & objPRSetup.EnumADType.MemoItem & "'"
        Dim strParamEmpe As String = "AND AD.ADType = '" & objPRSetup.EnumADType.Deduction & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectEmpr As Integer = 0
        Dim intSelectEmpe As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCode, _
                                           strParamEmpr, _
                                           objADDsEmpr, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_EPF_ADTYPE_MEMO_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For intCnt = 0 To objADDsEmpr.Tables(0).Rows.Count - 1
            objADDsEmpr.Tables(0).Rows(intCnt).Item("ADCode") = Trim(objADDsEmpr.Tables(0).Rows(intCnt).Item("ADCode"))
            objADDsEmpr.Tables(0).Rows(intCnt).Item("Description") = objADDsEmpr.Tables(0).Rows(intCnt).Item("ADCode") & " (" & Trim(objADDsEmpr.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objADDsEmpr.Tables(0).Rows(intCnt).Item("ADCode") = pv_strEmpr Then
                intSelectEmpr = intCnt + 1
            End If

        Next

        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCode, _
                                           strParamEmpe, _
                                           objADDsEmpe, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_EPF_ADTYPE_DEDUCT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objADDsEmpe.Tables(0).Rows.Count - 1
            objADDsEmpe.Tables(0).Rows(intCnt).Item("ADCode") = Trim(objADDsEmpe.Tables(0).Rows(intCnt).Item("ADCode"))
            objADDsEmpe.Tables(0).Rows(intCnt).Item("Description") = objADDsEmpe.Tables(0).Rows(intCnt).Item("ADCode") & " (" & Trim(objADDsEmpe.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objADDsEmpe.Tables(0).Rows(intCnt).Item("ADCode") = pv_strEmpe Then
                intSelectEmpe = intCnt + 1
            End If
        Next

        dr = objADDsEmpr.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("Description") = "Select Allowance & Deduction Code"
        objADDsEmpr.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmprDeductCode.DataSource = objADDsEmpr.Tables(0)
        ddlEmprDeductCode.DataValueField = "ADCode"
        ddlEmprDeductCode.DataTextField = "Description"
        ddlEmprDeductCode.DataBind()
        ddlEmprDeductCode.SelectedIndex = intSelectEmpr

        dr = objADDsEmpe.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("Description") = "Select Allowance & Deduction Code"
        objADDsEmpe.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpeDeductCode.DataSource = objADDsEmpe.Tables(0)
        ddlEmpeDeductCode.DataValueField = "ADCode"
        ddlEmpeDeductCode.DataTextField = "Description"
        ddlEmpeDeductCode.DataBind()
        ddlEmpeDeductCode.SelectedIndex = intSelectEmpe
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "HR_CLSSETUP_EPF_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_EPF_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_EPF_GET"
        Dim strOpCd_Sts As String = "HR_CLSSETUP_EPF_STATUS_UPD"
        Dim strOpCd As String
        Dim strEmprAD As String = Request.Form("ddlEmprDeductCode")
        Dim strEmpeAD As String = Request.Form("ddlEmpeDeductCode")
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim strParam As String = ""

        If strCmdArgs = "Save" Then

            If strEmprAD = "" Then
                lblErrEmprDeductCode.Visible = True
                Exit Sub
            ElseIf strEmpeAD = "" Then
                lblErrEmpeDeductCode.Visible = True
                Exit Sub
            End If

            If (txtEmprContributePercent.Text = "" And txtEmprContributeAmount.Text = "") Or _
               (txtEmprContributePercent.Text <> "0" And txtEmprContributeAmount.Text <> "0") Then
                lblErrEmployer.Visible = True
                Exit Sub
            ElseIf txtEmprContributePercent.Text <> "" Then
                If CInt(txtEmprContributePercent.Text) > 100 Then
                    lblErrEmployerPercent.Visible = True
                    Exit Sub
                End If
            End If

            If (txtEmpeContributePercent.Text = "" And txtEmpeContributeAmount.Text = "") Or _
               (txtEmpeContributePercent.Text <> "0" And txtEmpeContributeAmount.Text <> "0") Then
                lblErrEmployee.Visible = True
                Exit Sub
            ElseIf txtEmpeContributePercent.Text <> "" Then
                If CInt(txtEmpeContributePercent.Text) > 100 Then
                    lblErrEmployeePercent.Visible = True
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

            strParam = Trim(txtEPFCode.Text)
            Try
                intErrNo = objHRSetup.mtdGetEPF(strOpCd_Get, _
                                                strParam, _
                                                objEPFDs, _
                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_EPF_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objEPFDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDup.Visible = True
                Exit Sub
            Else
                strSelectedEPFcode = Trim(txtEPFCode.Text)
                epfcode.Value = strSelectedEPFcode
                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)

                strParam = Trim(txtEPFCode.Text) & "|" & _
                           Trim(txtDescription.Text) & "|" & _
                           strEmprAD & "|" & _
                           strEmpeAD & "|" & _
                           Trim(txtMinIncome.Text) & "|" & _
                           Trim(txtMaxAmount.Text) & "|" & _
                           Trim(txtEmprContributePercent.Text) & "|" & _
                           Trim(txtEmprContributeAmount.Text) & "|" & _
                           Trim(txtEmpeContributePercent.Text) & "|" & _
                           Trim(txtEmpeContributeAmount.Text) & "|" & _
                           objHRSetup.EnumEPFStatus.Active
                Try
                    intErrNo = objHRSetup.mtdUpdEPF(strOpCd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    False)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_EPF_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_EPFDet.aspx")
                End Try
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = strSelectedEPFcode & "|" & objHRSetup.EnumEPFStatus.Deleted
            Try
                intErrNo = objHRSetup.mtdUpdEPF(strOpCd_Sts, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_EPF_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_EPFDet.aspx?epfcode=" & strSelectedEPFcode)
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = strSelectedEPFcode & "|" & objHRSetup.EnumEPFStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdEPF(strOpCd_Sts, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_EPF_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_EPFDet.aspx?epfcode=" & strSelectedEPFcode)
            End Try
        End If

        If strSelectedEPFcode <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_EPFList.aspx")
    End Sub

End Class
