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

Public Class HR_setup_ContSuperDet : Inherits Page

    Protected WithEvents ddlSuppCode As DropDownList
    Protected WithEvents txtAllowance As TextBox
    Protected WithEvents ddlEmpCode As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents hidSuppCode As HtmlInputHidden
    Protected WithEvents hidEmpCode As HtmlInputHidden
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents lblErrEmpCode As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label

    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objContSuperDs As New DataSet()
    Dim objContSuperLnDs As New DataSet()
    Dim objSuppDs As New DataSet()
    Dim objEmpDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim intErrNo As Integer

    Dim strSelSuppCode As String = ""
    Dim intStatus As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRContractorSupervision), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblErrEmpCode.Visible = False

            strSelSuppCode = Trim(IIf(Request.QueryString("SupplierCode") <> "", Request.QueryString("SupplierCode"), Request.Form("SupplierCode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelSuppCode <> "" Then
                    hidSuppCode.Value = strSelSuppCode
                    onLoad_Display()
                    onLoad_LineDisplay()
                    BindSupp(hidSuppCode.Value)
                    BindEmp()
                Else
                    BindSupp("")
                    BindEmp()
                End If
                onLoad_BindButton()
            Else
                hidEmpCode.Value = ""
            End If
        End If
    End Sub

    Sub onLoad_BindButton()
        Select Case intStatus
            Case objHRSetup.EnumContSupervision.Active
                ddlSuppCode.Enabled = False
                txtAllowance.Enabled = True
                ddlEmpCode.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                btnAdd.Visible = True
            Case objHRSetup.EnumContSupervision.Deleted
                ddlSuppCode.Enabled = False
                txtAllowance.Enabled = False
                ddlEmpCode.Enabled = False
                SaveBtn.Visible = False
                DelBtn.Visible = False
                btnAdd.Visible = False
            Case Else
                ddlSuppCode.Enabled = True
                txtAllowance.Enabled = True
                ddlEmpCode.Enabled = True
                SaveBtn.Visible = True
                btnAdd.Visible = True
        End Select
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_CONTRACTOR_SUPER_GET"
        Dim strParam As String = hidSuppCode.Value & "||||" & strLocation & "||"
        Dim intCnt As Integer

        Try
            intErrNo = objHRSetup.mtdGetContractorSupervision(strOpCd, _
                                                              strParam, _
                                                              objContSuperDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_CONTSUPER_GET&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ContractorSuperDet.aspx?suppliercode=" & hidSuppCode.Value)
        End Try

        BindSupp(Trim(objContSuperDs.Tables(0).Rows(0).Item("SupplierCode")))
        txtAllowance.Text = objContSuperDs.Tables(0).Rows(0).Item("Allowance")
        intStatus = CInt(Trim(objContSuperDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objContSuperDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objHRSetup.mtdGetGangStatus(Trim(objContSuperDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objContSuperDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objContSuperDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objContSuperDs.Tables(0).Rows(0).Item("UserName"))
    End Sub

    Sub onLoad_LineDisplay()
        Dim strOpCd As String = "HR_CLSSETUP_CONTRACTOR_SUPER_LN_GET"
        Dim strParam As String = hidSuppCode.Value & "|" & strLocation & "||"
        Dim intCnt As Integer
        Dim intCntLn As Integer
        Dim lbButton As LinkButton
        Dim strContSupEmpCode As String

        Try
            intErrNo = objHRSetup.mtdGetContractorSupervisionLn(strOpCd, _
                                                                strParam, _
                                                                objContSuperLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSSETUP_CONT_SUPER_LINE_GET&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ContractorSuperDet.aspx?suppliercode=" & hidSuppCode.Value)
        End Try

        For intCntLn = 0 To objContSuperLnDs.Tables(0).Rows.Count - 1
            strContSupEmpCode = Trim(objContSuperLnDs.Tables(0).Rows(intCntLn).Item("EmpCode"))
            hidEmpCode.Value = strContSupEmpCode & "','" & hidEmpCode.Value
        Next

        If Right(hidEmpCode.Value, 3) = "','" Then
            hidEmpCode.Value = Left(hidEmpCode.Value, Len(hidEmpCode.Value) - 3)
        End If

        dgLineDet.DataSource = objContSuperLnDs.Tables(0)
        dgLineDet.DataBind()

        For intCnt = 0 To dgLineDet.Items.Count - 1
            Select Case CInt(objContSuperDs.Tables(0).Rows(0).Item("Status"))
                Case objHRSetup.EnumContSupervision.Active
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objHRSetup.EnumContSupervision.Deleted
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
        Next
    End Sub

    Sub BindEmp()
        Dim strOpCd As String = "HR_CLSSETUP_CONTRACTOR_SUPER_EMP_GET"
        Dim strParam As String
        Dim intCnt As Integer
        Dim dr As DataRow

        If hidEmpCode.Value <> "" Then
            strParam = "||| AND PR.PayType IN ('" & objPRSetup.EnumPayType.DailyRate & "','" & objPRSetup.EnumPayType.PieceRate & "') " & _
                       "AND EMP.EmpCode NOT IN ('" & hidEmpCode.Value & "') AND EMP.LocCode = '" & strLocation & "' "
        Else
            strParam = "||| AND PR.PayType IN ('" & objPRSetup.EnumPayType.DailyRate & "','" & objPRSetup.EnumPayType.PieceRate & "') AND EMP.LocCode = '" & strLocation & "' "
        End If

        Try
            intErrNo = objHRSetup.mtdGetContractorSupervisionLn(strOpCd, strParam, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSSETUP_CONTSUPER_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ContractorSuperDet.aspx?suppliercode=" & hidSuppCode.Value)
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode").Trim()
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") & " (" & objEmpDs.Tables(0).Rows(intCnt).Item("EmpName").Trim() & ")"
        Next

        dr = objEmpDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = "Please select one Employee"
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpCode.DataSource = objEmpDs.Tables(0)
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataTextField = "EmpName"
        ddlEmpCode.DataBind()
    End Sub

    Sub BindSupp(ByVal pv_strSuppCode As String)
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "|||||AND A.Status = '" & objPUSetup.EnumSuppStatus.Active & "' " & _
                   "AND A.SuppType = '" & objPUSetup.EnumSupplierType.Contractor & "'|A.SupplierCode"

        Try
            intErrNo = objHRSetup.mtdGetContractorSupervision(strOpCd, strParam, objSuppDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSSETUP_CONTSUPER_SUPP_GET&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ContractorSuperDet.aspx?suppliercode=" & hidSuppCode.Value)
        End Try

        For intCnt = 0 To objSuppDs.Tables(0).Rows.Count - 1
            objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = Trim(objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode"))
            objSuppDs.Tables(0).Rows(intCnt).Item("Name") = Trim(objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode")) & " (" & Trim(objSuppDs.Tables(0).Rows(intCnt).Item("Name")) & ")"
            If Trim(objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode")) = pv_strSuppCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objSuppDs.Tables(0).NewRow()
        dr("SupplierCode") = ""
        dr("Name") = "Please select one Supplier"
        objSuppDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSuppCode.DataSource = objSuppDs.Tables(0)
        ddlSuppCode.DataValueField = "SupplierCode"
        ddlSuppCode.DataTextField = "Name"
        ddlSuppCode.DataBind()
        ddlSuppCode.SelectedIndex = intSelectedIndex
    End Sub

    Function InsertContSuperRecord()
        Dim strOpCd_Add As String = "HR_CLSSETUP_CONTRACTOR_SUPER_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_CONTRACTOR_SUPER_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_CONTRACTOR_SUPER_GET"
        Dim strParam As String = ""
        Dim blnIsUpdate As Boolean

        strParam = "||||" & strLocation & "|AND CG.SupplierCode = '" & ddlSuppCode.SelectedItem.Value & "'|"
        Try
            intErrNo = objHRSetup.mtdGetContractorSupervision(strOpCd_Get, _
                                                              strParam, _
                                                              objContSuperDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_CONTSUPER_GET_INSERTREC&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ContractorSuperDet.aspx?suppliercode=" & hidSuppCode.Value)
        End Try

        If objContSuperDs.Tables(0).Rows.Count > 0 And lblStatus.Text = "" Then
            lblErrDup.Visible = True
            Return FALSE
        Else
            strSelSuppCode = ddlSuppCode.SelectedItem.Value.Trim
            hidSuppCode.Value = ddlSuppCode.SelectedItem.Value.Trim
            blnIsUpdate = IIf(lblStatus.Text = "", False, True)

            strParam = ddlSuppCode.SelectedItem.Value.Trim & "|" & txtAllowance.Text.Trim & "|"
            Try
                intErrNo = objHRSetup.mtdUpdContractorSupervision(strOpCd_Add, _
                                                                   strOpCd_Upd, _
                                                                   strCompany, _
                                                                   strLocation, _
                                                                   strUserId, _
                                                                   strParam, _
                                                                   blnIsUpdate)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_CONST_SUPER_ADD&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ContractorSuperDet.aspx?suppliercode=" & hidSuppCode.Value)
            End Try
        End If
        Return TRUE
    End Function


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "HR_CLSSETUP_CONTRACTOR_SUPER_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_CONTRACTOR_SUPER_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim strParam As String = ""
        Dim blnIsSaved As Boolean

        If strCmdArgs = "Save" Then
            blnIsSaved = InsertContSuperRecord()
        ElseIf strCmdArgs = "Del" Then
            blnIsSaved = True
            strParam = ddlSuppCode.SelectedItem.Value.Trim & "||" & objHRSetup.EnumContSupervision.Deleted
            Try
                intErrNo = objHRSetup.mtdUpdContractorSupervision(strOpCd_Add, _
                                                                  strOpCd_Upd, _
                                                                  strCompany, _
                                                                  strLocation, _
                                                                  strUserId, _
                                                                  strParam, _
                                                                  True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSSETUP_CONT_SUPER_DEL&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ContractorSuperDet.aspx?suppliercode=" & hidSuppCode.Value)
            End Try
        End If

        If hidSuppCode.Value <> "" And blnIsSaved = True Then
            onLoad_Display()
            onLoad_LineDisplay()
            BindEmp()
            onLoad_BindButton()
        End If
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode_AddLine As String = "HR_CLSSETUP_CONTRACTOR_SUPER_LN_ADD"
        Dim strOpCode_Upd As String = "HR_CLSSETUP_CONTRACTOR_SUPER_UPD"
        Dim strParam As String
        Dim blnIsAdded As Boolean

        If ddlEmpCode.SelectedItem.Value = "" Then
            lblErrEmpCode.Visible = True
            Exit Sub
        Else
            blnIsAdded = InsertContSuperRecord()

            strParam = ddlSuppCode.SelectedItem.Value.Trim & "|" & ddlEmpCode.SelectedItem.Value.Trim
            Try
                intErrNo = objHRSetup.mtdUpdContractorSupervisionLn(strOpCode_Upd, _
                                                                    strOpCode_AddLine, _
                                                                    strCompany, _
                                                                    strLocation, _
                                                                    strUserId, _
                                                                    strParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_CLSSETUP_CONST_SUPER_LINE_UPD&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ContractorSuperDet.aspx?suppliercode=" & hidSuppCode.Value)
            End Try
        End If

        If blnIsAdded = True Then
            onLoad_Display()
            onLoad_LineDisplay()
            BindEmp()
            onLoad_BindButton()
        End If
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCode_DelLine As String = "HR_CLSSETUP_CONTRACTOR_SUPER_LN_DEL"
        Dim strOpCode_Upd As String = "HR_CLSSETUP_CONTRACTOR_SUPER_UPD"
        Dim strParam As String
        Dim strEmpCode As String
        Dim lblDelText As Label

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblEmpCode")
        strEmpCode = lblDelText.Text

        Try
            strParam = hidSuppCode.Value & "|" & strEmpCode
            intErrNo = objHRSetup.mtdUpdContractorSupervisionLn(strOpCode_Upd, _
                                                                strOpCode_DelLine, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_CONT_SUPER_LINE_DEL&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_ContractorSuperDet.aspx?suppliercode=" & hidSuppCode.Value)
        End Try

        onLoad_Display()
        onLoad_LineDisplay()
        BindEmp()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_setup_ContractorSuperList.aspx")
    End Sub


End Class
