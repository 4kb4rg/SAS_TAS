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

Public Class HR_setup_BankDet : Inherits Page

    Protected WithEvents txtBankCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents txtAddress As HtmlTextArea
    Protected WithEvents txtBankAcc As Textbox
    Protected WithEvents txtBarCode As Textbox
    Protected WithEvents txtCustID As Textbox
    Protected WithEvents txtTransCharges As Textbox
    Protected WithEvents txtBatchNo As Textbox
    Protected WithEvents lblLastDate As Label
    Protected WithEvents ddlAutoCredit As Dropdownlist
    Protected WithEvents ddlChequeFormat As Dropdownlist
    Protected WithEvents ddlReportFormat As DropDownList
    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents bankcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents txtBankAccOwner As TextBox
    Protected WithEvents txtSwift As TextBox
    Protected WithEvents txtCorBankDescription As TextBox
    Protected WithEvents txtCorBankAccNo As TextBox
    Protected WithEvents txtCorBankSwift As TextBox
    Protected WithEvents ddlGiroFormat As DropDownList
    Protected WithEvents ddlSetoranFormat As DropDownList
    Protected WithEvents ddlTransferFormat As DropDownList

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGL As New agri.GL.clsSetup()
    Dim objGLTrx As New agri.GL.ClsTrx()

    Dim objBankDs As New Object()
    Dim objBankFmtDs As New Object()
    Dim objRsl As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Dim strSelectedBankCode As String = ""
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
            lblErrDup.Visible = False
            strSelectedBankCode = Trim(IIf(Request.QueryString("bankcode") <> "", Request.QueryString("bankcode"), Request.Form("bankcode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedBankCode <> "" Then
                    bankcode.Value = strSelectedBankCode
                    onLoad_Display()
                Else
                    BindBankFormat("", "", "", "", "", "")
                    BindAccCode("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_BindButton()
        txtBankCode.Enabled = False
        txtDescription.Enabled = False
        txtBankAcc.Enabled = False
        txtAddress.Disabled = True
        txtBarCode.Enabled = False
        txtCustID.Enabled = False
        txtBatchNo.Enabled = False
        ddlAutoCredit.Enabled = False
        ddlChequeFormat.Enabled = False
        ddlReportFormat.Enabled = False
        ddlAccCode.Enabled = False
        txtBankAccOwner.Enabled = False
	
        txtTransCharges.Enabled = True
	
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objHRSetup.EnumCPStatus.Active
                txtDescription.Enabled = True
                txtBankAcc.Enabled = True
                txtAddress.Disabled = False
                txtBarCode.Enabled = True
                txtCustID.Enabled = True
                txtBatchNo.Enabled = True
                ddlAutoCredit.Enabled = True
                ddlChequeFormat.Enabled = True
                ddlReportFormat.Enabled = True
                ddlAccCode.Enabled = True
                txtBankAccOwner.Enabled = True

                txtTransCharges.Enabled = True
		
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumCPStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtBankCode.Enabled = True
                txtDescription.Enabled = True
                txtBankAcc.Enabled = True
                txtAddress.Disabled = False
                txtBarCode.Enabled = True
                txtCustID.Enabled = True
                txtBatchNo.Enabled = True
                ddlAutoCredit.Enabled = True
                ddlChequeFormat.Enabled = True
                ddlReportFormat.Enabled = True
                ddlAccCode.Enabled = True
                txtBankAccOwner.Enabled = True
                txtTransCharges.Enabled = True
		
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_BANK_GET"
        Dim strParam As String = strSelectedBankCode        
        Dim intErrNo As Integer

        Try
            intErrNo = objHRSetup.mtdGetBank(strOpCd, _
                                             strParam, _
                                             objBankDs, _
                                             True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtBankCode.Text = Trim(objBankDs.Tables(0).Rows(0).Item("BankCode"))
        txtDescription.Text = Trim(objBankDs.Tables(0).Rows(0).Item("Description"))
        txtBankAcc.Text = Trim(objBankDs.Tables(0).Rows(0).Item("AccNo"))
        txtAddress.Value = Trim(objBankDs.Tables(0).Rows(0).Item("Address"))
        txtBarCode.Text = Trim(objBankDs.Tables(0).Rows(0).Item("BarCode"))
        txtCustID.Text = Trim(objBankDs.Tables(0).Rows(0).Item("CustId"))
        txtBatchNo.Text = Trim(objBankDs.Tables(0).Rows(0).Item("BatchNo"))
        lblLastDate.Text = objGlobal.GetLongDate(objBankDs.Tables(0).Rows(0).Item("LastIntegrateDate"))
        intStatus = CInt(Trim(objBankDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objBankDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objHRSetup.mtdGetCPStatus(Trim(objBankDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objBankDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objBankDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objBankDs.Tables(0).Rows(0).Item("UserName"))
	    
	    txtTransCharges.Text = objBankDs.Tables(0).Rows(0).Item("TransferCharges")
        txtBankAccOwner.Text = Trim(objBankDs.Tables(0).Rows(0).Item("AccOwner"))
        txtSwift.Text = Trim(objBankDs.Tables(0).Rows(0).Item("Swift"))
        txtCorBankDescription.Text = Trim(objBankDs.Tables(0).Rows(0).Item("CorBankDescription"))
        txtCorBankAccNo.Text = Trim(objBankDs.Tables(0).Rows(0).Item("CorBankAccNo"))
        txtCorBankSwift.Text = Trim(objBankDs.Tables(0).Rows(0).Item("CorBankSwift"))
        
        BindBankFormat(Trim(objBankDs.Tables(0).Rows(0).Item("AutoCRFmtInd")), _
                       Trim(objBankDs.Tables(0).Rows(0).Item("ChequeFmtInd")), _
                       Trim(objBankDs.Tables(0).Rows(0).Item("RptFmtInd")), _
                       Trim(objBankDs.Tables(0).Rows(0).Item("BilyetGiroFmtInd")), _
                       Trim(objBankDs.Tables(0).Rows(0).Item("SlipSetoranFmtInd")), _
                       Trim(objBankDs.Tables(0).Rows(0).Item("SlipTransferFmtInd")))
        BindAccCode(Trim(objBankDs.Tables(0).Rows(0).Item("AccCode")))
        onLoad_BindButton()
    End Sub

    Sub BindBankFormat(ByVal pv_strAutoCredit As String, ByVal pv_strCheque As String, ByVal pv_strReport As String, ByVal pv_strBilyetGiro As String, ByVal pv_strSlipSetoran As String, ByVal pv_strSlipTransfer As String)
        Dim strOpCode As String = "HR_CLSSETUP_BANKFORMAT_LIST_GET"
        Dim strParam As String = ""
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelect As Integer = 0

        intSelect = 0
        Try
            strParam = "Order By BF.FormatCode|And BF.FormatType = '" & objHRSetup.EnumBankFormatType.Autocredit & "' And BF.Status = '" & objHRSetup.EnumBankFormatStatus.Active & "'"
            intErrNo = objHRSetup.mtdGetMasterList(strOpCode, strParam, objHRSetup.EnumHRMasterType.BankFormat, objBankFmtDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_AUTOCREDIT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBankFmtDs.Tables(0).Rows.Count - 1
            objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode"))
            objBankFmtDs.Tables(0).Rows(intCnt).Item("Description") = objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") & " (" & Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(pv_strAutoCredit) Then
                intSelect = intCnt + 1
            End If
        Next

        dr = objBankFmtDs.Tables(0).NewRow()
        dr("FormatCode") = " "
        dr("Description") = "Select Autocredit Format"
        objBankFmtDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlAutoCredit.DataSource = objBankFmtDs.Tables(0)
        ddlAutoCredit.DataValueField = "FormatCode"
        ddlAutoCredit.DataTextField = "Description"
        ddlAutoCredit.DataBind()
        ddlAutoCredit.SelectedIndex = intSelect

        intSelect = 0
        Try
            strParam = "Order By BF.FormatCode|And BF.FormatType = '" & objHRSetup.EnumBankFormatType.Cheque & "' And BF.Status = '" & objHRSetup.EnumBankFormatStatus.Active & "'"
            intErrNo = objHRSetup.mtdGetMasterList(strOpCode, strParam, objHRSetup.EnumHRMasterType.BankFormat, objBankFmtDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_CHEQUE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBankFmtDs.Tables(0).Rows.Count - 1
            objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode"))
            objBankFmtDs.Tables(0).Rows(intCnt).Item("Description") = objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") & " (" & Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(pv_strCheque) Then
                intSelect = intCnt + 1
            End If
        Next

        dr = objBankFmtDs.Tables(0).NewRow()
        dr("FormatCode") = " "
        dr("Description") = "Select Cheque Format"
        objBankFmtDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlChequeFormat.DataSource = objBankFmtDs.Tables(0)
        ddlChequeFormat.DataValueField = "FormatCode"
        ddlChequeFormat.DataTextField = "Description"
        ddlChequeFormat.DataBind()
        ddlChequeFormat.SelectedIndex = intSelect

        intSelect = 0
        Try
            strParam = "Order By BF.FormatCode|And BF.FormatType = '" & objHRSetup.EnumBankFormatType.Report & "' And BF.Status = '" & objHRSetup.EnumBankFormatStatus.Active & "'"
            intErrNo = objHRSetup.mtdGetMasterList(strOpCode, strParam, objHRSetup.EnumHRMasterType.BankFormat, objBankFmtDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_REPORT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBankFmtDs.Tables(0).Rows.Count - 1
            objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode"))
            objBankFmtDs.Tables(0).Rows(intCnt).Item("Description") = objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") & " (" & Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(pv_strReport) Then
                intSelect = intCnt + 1
            End If
        Next

        dr = objBankFmtDs.Tables(0).NewRow()
        dr("FormatCode") = " "
        dr("Description") = "Select Report Format"
        objBankFmtDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlReportFormat.DataSource = objBankFmtDs.Tables(0)
        ddlReportFormat.DataValueField = "FormatCode"
        ddlReportFormat.DataTextField = "Description"
        ddlReportFormat.DataBind()
        ddlReportFormat.SelectedIndex = intSelect

        intSelect = 0
        Try
            strParam = "Order By BF.FormatCode|And BF.FormatType = '" & objHRSetup.EnumBankFormatType.BilyetGiro & "' And BF.Status = '" & objHRSetup.EnumBankFormatStatus.Active & "'"
            intErrNo = objHRSetup.mtdGetMasterList(strOpCode, strParam, objHRSetup.EnumHRMasterType.BankFormat, objBankFmtDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_REPORT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBankFmtDs.Tables(0).Rows.Count - 1
            objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode"))
            objBankFmtDs.Tables(0).Rows(intCnt).Item("Description") = objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") & " (" & Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(pv_strBilyetGiro) Then
                intSelect = intCnt + 1
            End If
        Next

        dr = objBankFmtDs.Tables(0).NewRow()
        dr("FormatCode") = " "
        dr("Description") = "Select Bilyet Giro Format"
        objBankFmtDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlGiroFormat.DataSource = objBankFmtDs.Tables(0)
        ddlGiroFormat.DataValueField = "FormatCode"
        ddlGiroFormat.DataTextField = "Description"
        ddlGiroFormat.DataBind()
        ddlGiroFormat.SelectedIndex = intSelect

        intSelect = 0
        Try
            strParam = "Order By BF.FormatCode|And BF.FormatType = '" & objHRSetup.EnumBankFormatType.SlipSetoran & "' And BF.Status = '" & objHRSetup.EnumBankFormatStatus.Active & "'"
            intErrNo = objHRSetup.mtdGetMasterList(strOpCode, strParam, objHRSetup.EnumHRMasterType.BankFormat, objBankFmtDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_REPORT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBankFmtDs.Tables(0).Rows.Count - 1
            objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode"))
            objBankFmtDs.Tables(0).Rows(intCnt).Item("Description") = objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") & " (" & Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(pv_strSlipSetoran) Then
                intSelect = intCnt + 1
            End If
        Next

        dr = objBankFmtDs.Tables(0).NewRow()
        dr("FormatCode") = " "
        dr("Description") = "Select Slip Setoran Format"
        objBankFmtDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlSetoranFormat.DataSource = objBankFmtDs.Tables(0)
        ddlSetoranFormat.DataValueField = "FormatCode"
        ddlSetoranFormat.DataTextField = "Description"
        ddlSetoranFormat.DataBind()
        ddlSetoranFormat.SelectedIndex = intSelect

        intSelect = 0
        Try
            strParam = "Order By BF.FormatCode|And BF.FormatType = '" & objHRSetup.EnumBankFormatType.SlipTransfer & "' And BF.Status = '" & objHRSetup.EnumBankFormatStatus.Active & "'"
            intErrNo = objHRSetup.mtdGetMasterList(strOpCode, strParam, objHRSetup.EnumHRMasterType.BankFormat, objBankFmtDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_REPORT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBankFmtDs.Tables(0).Rows.Count - 1
            objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode"))
            objBankFmtDs.Tables(0).Rows(intCnt).Item("Description") = objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") & " (" & Trim(objBankFmtDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBankFmtDs.Tables(0).Rows(intCnt).Item("FormatCode") = Trim(pv_strSlipTransfer) Then
                intSelect = intCnt + 1
            End If
        Next

        dr = objBankFmtDs.Tables(0).NewRow()
        dr("FormatCode") = " "
        dr("Description") = "Select Slip Transfer Format"
        objBankFmtDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlTransferFormat.DataSource = objBankFmtDs.Tables(0)
        ddlTransferFormat.DataValueField = "FormatCode"
        ddlTransferFormat.DataTextField = "Description"
        ddlTransferFormat.DataBind()
        ddlTransferFormat.SelectedIndex = intSelect

    End Sub

    Sub BindAccCode(ByVal pv_strAccCode As String)
        Dim strOpCode As String = "HR_CLSSETUP_ACCCODE_LIST_GET"
        Dim strParam As String = ""
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelect As Integer = 0

        intSelect = 0

        strParam = "Order By AccCode|Status ='" & objGL.EnumAccStatus.Active & "' and AccType ='" & objGL.EnumAccountType.BalanceSheet & "' And NurseryInd = '" & objGL.EnumNurseryAccount.No & "' And COALevel='2'"
        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objHRSetup.mtdGetBankAccCode(strOpCode, strParam, objRsl)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objRsl.Tables(0).Rows.Count - 1

            objRsl.Tables(0).Rows(intCnt).Item("Description") = objRsl.Tables(0).Rows(intCnt).Item("AccCode") & " (" & Trim(objRsl.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If Trim(objRsl.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(pv_strAccCode) Then
                intSelect = intCnt + 1
            End If
        Next

        dr = objRsl.Tables(0).NewRow()
        dr("Description") = "Select Account Code"
        objRsl.Tables(0).Rows.InsertAt(dr, 0)
        ddlAccCode.DataSource = objRsl.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelect

    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "HR_CLSSETUP_BANK_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_BANK_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_BANK_GET"
        Dim strOpCd_Sts As String = "HR_CLSSETUP_BANK_STATUS_UPD"
        Dim strOpCd As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If strCmdArgs = "Save" Then

            'If ddlAccCode.SelectedItem.Value = "" Or ddlAccCode.SelectedItem.Value = "Select Account Code" Then
            '    lblErrAccCode.Visible = True
            '    Exit Sub
            'Else
            '    lblErrAccCode.Visible = False
            '    strParam = Trim(txtBankCode.Text)
            'End If
            strParam = Trim(txtBankCode.Text)

            Try
                intErrNo = objHRSetup.mtdGetBank(strOpCd_Get, _
                                                strParam, _
                                                objBankDs, _
                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objBankDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDup.Visible = True
                Exit Sub
            Else
                strSelectedBankCode = Trim(txtBankCode.Text)
                bankcode.Value = strSelectedBankCode
                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)

                strParam = Trim(txtBankCode.Text) & Chr(9) & _
                           Trim(txtDescription.Text) & Chr(9) & _
                           Trim(txtBankAcc.Text) & Chr(9) & _
                           Trim(txtAddress.Value) & Chr(9) & _
                           Trim(txtBarCode.Text) & Chr(9) & _
                           Trim(txtCustID.Text) & Chr(9) & _
                           Trim(txtBatchNo.Text) & Chr(9) & _
                           ddlAutoCredit.SelectedItem.Value & Chr(9) & _
                           ddlChequeFormat.SelectedItem.Value & Chr(9) & _
                           ddlReportFormat.SelectedItem.Value & Chr(9) & _
                           objHRSetup.EnumBankStatus.Active & Chr(9) & _
                           ddlAccCode.SelectedItem.Value() & Chr(9) & _
                           txtTransCharges.Text & Chr(9) & _
                           Trim(txtBankAccOwner.Text) & Chr(9) & _
                           Trim(txtSwift.Text) & Chr(9) & _
                           Trim(txtCorBankDescription.Text) & Chr(9) & _
                           Trim(txtCorBankAccNo.Text) & Chr(9) & _
                           Trim(txtCorBankSwift.Text) & Chr(9) & _
                           ddlGiroFormat.SelectedItem.Value & Chr(9) & _
                           ddlSetoranFormat.SelectedItem.Value & Chr(9) & _
                           ddlTransferFormat.SelectedItem.Value

                Try
                    intErrNo = objHRSetup.mtdUpdBank(strOpCd, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     False)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_BankDet.aspx")
                End Try
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = strSelectedBankCode & Chr(9) & objHRSetup.EnumBankStatus.Deleted
            Try
                intErrNo = objHRSetup.mtdUpdBank(strOpCd_Sts, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_BankDet.aspx?bankcode=" & strSelectedBankCode)
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = strSelectedBankCode & Chr(9) & objHRSetup.EnumBankStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdBank(strOpCd_Sts, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strParam, _
                                                 True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_BankDet.aspx?bankcode=" & strSelectedBankCode)
            End Try
        End If

        If strSelectedBankCode <> "" Then
            Bank_Synchronized(strSelectedBankCode)
            onLoad_Display()
        End If
    End Sub

    Sub Bank_Synchronized(ByVal pv_strBankCode As String)
        Dim strOpCd_Upd As String = "HR_CLSSETUP_BANK_SYNCHRONIZED"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmp As New Object()
        
        strSelectedBankCode = Trim(pv_strBankCode)
        strParamName = "BANKCODE|ORICOMPCODE"
        strParamValue = strSelectedBankCode & "|" & Trim(strCompany)

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_setup_Banklist.aspx")
    End Sub

End Class
