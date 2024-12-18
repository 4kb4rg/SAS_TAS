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

Public Class HR_setup_TaxBranchDet : Inherits Page

    Protected WithEvents txtTaxBranchCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents txtEmpTaxNo As Textbox
    Protected WithEvents txtAddress As HtmlTextArea
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objTaxBranchDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Dim strSelectedTaxCode As String = ""
    Dim intStatus As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTaxBranch), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            strSelectedTaxCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedTaxCode <> "" Then
                    tbcode.Value = strSelectedTaxCode
                    onLoad_Display()
                Else
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_TAXBRANCH_GET"
        Dim strParam As String = strSelectedTaxCode        
        Dim intErrNo As Integer

        Try
            intErrNo = objHRSetup.mtdGetTaxBranch(strOpCd, _
                                                  strParam, _
                                                  objTaxBranchDs, _
                                                  True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXBRANCH_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtTaxBranchCode.Text = Trim(objTaxBranchDs.Tables(0).Rows(0).Item("TaxBranchCode"))
        txtDescription.Text = Trim(objTaxBranchDs.Tables(0).Rows(0).Item("Description"))
        txtEmpTaxNo.Text = Trim(objTaxBranchDs.Tables(0).Rows(0).Item("EmployerTaxNo"))
        txtAddress.Value = Trim(objTaxBranchDs.Tables(0).Rows(0).Item("Address"))
        intStatus = CInt(Trim(objTaxBranchDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objTaxBranchDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objHRSetup.mtdGetCPStatus(Trim(objTaxBranchDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objTaxBranchDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objTaxBranchDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objTaxBranchDs.Tables(0).Rows(0).Item("UserName"))

        onLoad_BindButton()
    End Sub

    Sub onLoad_BindButton()
        txtTaxBranchCode.Enabled = False
        txtDescription.Enabled = False
        txtEmpTaxNo.Enabled = False
        txtAddress.Disabled = True
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objHRSetup.EnumCPStatus.Active
                txtDescription.Enabled = True
                txtEmpTaxNo.Enabled = True
                txtAddress.Disabled = False
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumCPStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtTaxBranchCode.Enabled = True
                txtDescription.Enabled = True
                txtEmpTaxNo.Enabled = True
                txtAddress.Disabled = False
                SaveBtn.Visible = True
        End Select
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "HR_CLSSETUP_TAXBRANCH_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_TAXBRANCH_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_TAXBRANCH_GET"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim intBlackList As Integer
        Dim intPeriod As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If strCmdArgs = "Save" Then
            strParam = Trim(txtTaxBranchCode.Text)
            Try
                intErrNo = objHRSetup.mtdGetTaxBranch(strOpCd_Get, _
                                                    strParam, _
                                                    objTaxBranchDs, _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXBRANCH_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objTaxBranchDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDup.Visible = True
                Exit Sub
            Else
                strSelectedTaxCode = Trim(txtTaxBranchCode.Text)
                blnIsUpdate = IIf(intStatus = 0, False, True)
                tbcode.Value = strSelectedTaxCode

                strParam = Trim(txtTaxBranchCode.Text) & Chr(9) & _
                           Trim(txtDescription.Text) & Chr(9) & _
                           Trim(txtEmpTaxNo.Text) & Chr(9) & _
                           Trim(txtAddress.Value) & Chr(9) & _
                           objHRSetup.EnumTaxBranchStatus.Active
                Try
                    intErrNo = objHRSetup.mtdUpdTaxBranch(strOpCd_Add, _
                                                        strOpCd_Upd, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        blnIsUpdate)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXBRANCH_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_TaxBranchDet.aspx")
                End Try
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(txtTaxBranchCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objHRSetup.EnumTaxBranchStatus.Deleted
            Try
                intErrNo = objHRSetup.mtdUpdTaxBranch(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXBRANCH_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_TaxBranchDet.aspx?tbcode=" & strSelectedTaxCode)
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = Trim(txtTaxBranchCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objHRSetup.EnumTaxBranchStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdTaxBranch(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXBRANCH_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_TaxBranchDet.aspx?tbcode=" & strSelectedTaxCode)
            End Try
        End If

        If strSelectedTaxCode <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_TaxBranchlist.aspx")
    End Sub


End Class
