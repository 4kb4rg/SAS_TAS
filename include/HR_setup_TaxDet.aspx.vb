
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

Public Class HR_setup_TaxDet : Inherits Page

    Protected WithEvents txtTaxCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents ddlEmprDeCode As DropDownList
    Protected WithEvents ddlEmpeDeCode As DropDownList
    Protected WithEvents txtMinTaxIncome As TextBox
    Protected WithEvents txtFromIncome As TextBox
    Protected WithEvents txtToIncome As TextBox
    Protected WithEvents txtEmprRate As TextBox
    Protected WithEvents txtEmpeRate As TextBox
    Protected WithEvents txtFuncPercentage As TextBox
    Protected WithEvents txtMaxAllowance As Textbox
    Protected WithEvents txtPeronalAllowance As Textbox
    Protected WithEvents txtDependentAllowance As Textbox
    Protected WithEvents txtMaxDependents As TextBox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblHidRange As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrFromIncome As Label
    Protected WithEvents lblErrToIncome As Label
    Protected WithEvents lblErrRange As Label
    Protected WithEvents lblErrSelectOne As Label
    Protected WithEvents lblErrMinIncome As Label
    Protected WithEvents lblErrEmprRate As Label
    Protected WithEvents lblErrEmpeRate AS Label

    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objTaxDs As New Object()
    Dim objTaxLnDs As New Object()
    Dim objEmprADDs As New Object()
    Dim objEmpeADDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Dim strSelTaxCode As String = ""
    Dim intStatus As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTax), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblErrFromIncome.Visible = False
            lblErrToIncome.Visible = False
            lblErrRange.Visible = False
            lblErrSelectOne.Visible = False
            lblErrMinIncome.Visible = False
            lblErrEmprRate.Visible = False
            lblErrEmpeRate.Visible = False
            strSelTaxCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelTaxCode <> "" Then
                    tbcode.Value = strSelTaxCode
                    onLoad_Display()
                    onLoad_LineDisplay()
                    onLoad_BindButton()
                Else
                    BindADCode("", "")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub



    Sub onLoad_BindButton()
        txtTaxCode.Enabled = False
        txtDesc.Enabled = False
        ddlEmprDeCode.Enabled = False
        ddlEmpeDeCode.Enabled = False
        txtMinTaxIncome.Enabled = False
        txtEmprRate.Enabled = False
        txtEmpeRate.Enabled = False 
        tblSelection.Visible = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objHRSetup.EnumTaxStatus.Active
                txtDesc.Enabled = True
                If ddlEmprDeCode.SelectedItem.Value <> "" Then
                    txtEmprRate.Enabled = True
                End If
                If ddlEmpeDeCode.SelectedItem.Value <> "" Then
                    txtEmpeRate.Enabled = True
                End If
                txtMinTaxIncome.Enabled = True 
                tblSelection.Visible = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumTaxStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtTaxCode.Enabled = True
                txtDesc.Enabled = True
                ddlEmprDeCode.Enabled = True
                ddlEmpeDeCode.Enabled = True
                txtMinTaxIncome.Enabled = True 
                tblSelection.Visible = True
                SaveBtn.Visible = True
        End Select
        
    End Sub

    Sub onchange_DeCode(Sender As Object, E As EventArgs)
        ddlEmprDeCode.Enabled = True
        ddlEmpeDeCode.Enabled = True
        txtEmprRate.Enabled = True
        txtEmpeRate.Enabled = True
        If ddlEmprDeCode.SelectedItem.Value <> "" Then
            ddlEmpeDeCode.Enabled = False
            txtEmpeRate.Enabled = False
            txtEmpeRate.Text = "0"
        End If
        If ddlEmpeDeCode.SelectedItem.Value <> "" Then
            ddlEmprDeCode.Enabled = False
            txtEmprRate.Enabled = False
            txtEmprRate.Text = "0"
        End If
    End Sub

    Sub BindADCode(ByVal pv_strEmprDeCode As String, ByVal pv_strEmpeDeCode As String)
        Dim strOpCdGet As String = "PR_CLSSETUP_ADCODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSort As String
        Dim strSearch As String
        
        strSort = "order by ad.ADCode"

        strSearch = "and ad.Status = '" & objPRSetup.EnumADStatus.Active & "' " & _
                    "and ad.ADType = '" & objPRSetup.EnumADType.MemoItem & "' " 

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCdGet, _
                                                   strParam, _
                                                   0, _
                                                   objEmprADDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXDET_GETEMPRDECODE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_taxlist.aspx")
        End Try
        
        For intCnt = 0 To objEmprADDs.Tables(0).Rows.Count - 1
            If Trim(objEmprADDs.Tables(0).Rows(intCnt).Item("ADCode")) = Trim(pv_strEmprDeCode) Then
                intSelectIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objEmprADDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("_Description") = "Select Employer Deduction Code"
        objEmprADDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmprDeCode.DataSource = objEmprADDs.Tables(0)
        ddlEmprDeCode.DataValueField = "ADCode"
        ddlEmprDeCode.DataTextField = "_Description"
        ddlEmprDeCode.DataBind()
        ddlEmprDeCode.SelectedIndex = intSelectIndex

        intSelectIndex = 0
        strSearch = "and ad.Status = '" & objPRSetup.EnumADStatus.Active & "' " & _
                    "and ad.ADType = '" & objPRSetup.EnumADType.Deduction & "' " 

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCdGet, _
                                                   strParam, _
                                                   0, _
                                                   objEmpeADDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXDET_GETEMPEDECODE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_taxlist.aspx")
        End Try
        
        For intCnt = 0 To objEmpeADDs.Tables(0).Rows.Count - 1
            If Trim(objEmpeADDs.Tables(0).Rows(intCnt).Item("ADCode")) = Trim(pv_strEmpeDeCode) Then
                intSelectIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objEmpeADDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("_Description") = "Select Employee Deduction Code"
        objEmpeADDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpeDeCode.DataSource = objEmpeADDs.Tables(0)
        ddlEmpeDeCode.DataValueField = "ADCode"
        ddlEmpeDeCode.DataTextField = "_Description"
        ddlEmpeDeCode.DataBind()
        ddlEmpeDeCode.SelectedIndex = intSelectIndex
    End Sub

    Sub onLoad_Display()
        Dim strOpCdGet As String = "HR_ClSSETUP_TAX_GET"
        Dim strParam As String        
        Dim intErrNo As Integer

        strParam = "order by tax.TaxCode" & "|" & "and tax.TaxCode = '" & strSelTaxCode & "' " 

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objTaxDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_taxlist.aspx")
        End Try

        txtTaxCode.Text = objTaxDs.Tables(0).Rows(0).Item("TaxCode")
        txtDesc.Text = objTaxDs.Tables(0).Rows(0).Item("Description")
        txtMinTaxIncome.Text = FormatNumber(objTaxDs.Tables(0).Rows(0).Item("MinTaxIncome"), 0, True, False, False)
        txtFuncPercentage.Text = FormatNumber(objTaxDs.Tables(0).Rows(0).Item("FunctionalPercentage"), 2, True, False, False)
        txtMaxAllowance.Text = FormatNumber(objTaxDs.Tables(0).Rows(0).Item("MaxFunctionalAllowance"), 0, True, False, False)
        txtPeronalAllowance.Text = FormatNumber(objTaxDs.Tables(0).Rows(0).Item("PersonalAllowance"), 0, True, False, False)
        txtDependentAllowance.Text = FormatNumber(objTaxDs.Tables(0).Rows(0).Item("DependentAllowance"), 0, True, False, False)
        txtMaxDependents.Text = FormatNumber(objTaxDs.Tables(0).Rows(0).Item("MaxDependent"), 0, True, False, False)
        BindADCode(objTaxDs.Tables(0).Rows(0).Item("EmprDeCode"), objTaxDs.Tables(0).Rows(0).Item("EmpeDeCode"))
        
        lblStatus.Text = objHRSetup.mtdGetTaxStatus(Trim(objTaxDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objTaxDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objTaxDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("UserName"))

        intStatus = CInt(Trim(objTaxDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("Status"))

    End Sub

    Sub onLoad_LineDisplay()
        Dim strOpCdGet As String = "HR_CLSSETUP_TAXLN_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim strSort As String
        Dim strSearch As String

        strSort = "order by TaxLnId"
        strSearch = "where TaxCode = '" & strSelTaxCode & "' "
        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCdGet, _
                                                   strParam, _
                                                   0, _
                                                   objTaxLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXDET_GETLINE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_taxlist.aspx")
        End Try

        dgLineDet.DataSource = objTaxLnDs.Tables(0)
        dgLineDet.DataBind()
        For intCnt = 0 To dgLineDet.Items.Count - 1
            Select Case CInt(lblHiddenSts.Text)
                Case objHRSetup.EnumTaxStatus.Active
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                Case objHRSetup.EnumTaxStatus.Deleted
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
            lblHidRange.Text = lblHidRange.Text & Chr(9) & _
                               objTaxLnDs.Tables(0).Rows(intCnt).Item("FromIncome") & "|" & _
                               objTaxLnDs.Tables(0).Rows(intCnt).Item("ToIncome")
        Next

    End Sub

    Sub InsertHeader()
        Dim strOpCdUpd As String = "HR_CLSSETUP_TAX_UPD"
        Dim strOpCdGet As String = "HR_CLSSETUP_TAX_GET"
        Dim strOpCdAdd As String = "HR_CLSSETUP_TAX_ADD"
        Dim blnIsDup As Boolean = False
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""
        Dim intErrNo As Integer

        blnIsUpdate = IIf(intStatus = 0, False, True)

        If ddlEmprDeCode.SelectedItem.Value = "" And ddlEmpeDeCode.SelectedItem.Value = "" Then
            lblErrSelectOne.Visible = True
            Exit Sub
        End If

        If CDbl(txtMinTaxIncome.Text) < 0 Then
            lblErrMinIncome.Visible = True
            Exit Sub
        End If

        strParam = Trim(txtTaxCode.Text) & chr(9) & _
                   Trim(txtDesc.Text) & chr(9) & _
                   ddlEmprDeCode.SelectedItem.Value & chr(9) & _
                   ddlEmpeDeCode.SelectedItem.Value & chr(9) & _
                   txtMinTaxIncome.Text & chr(9) & _
                   objHRSetup.EnumTaxStatus.Active & chr(9) & _
                   txtFuncPercentage.Text & chr(9) & _
                   txtMaxAllowance.Text & chr(9) & _
                   txtPeronalAllowance.Text & chr(9) & _ 
                   txtDependentAllowance.Text & chr(9) & _
                   txtMaxDependents.Text  
        Try
            intErrNo = objHRSetup.mtdUpdTax(strOpCdGet, _
                                            strOpCdAdd, _
                                            strOpCdUpd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            blnIsUpdate, _
                                            False, _
                                            blnIsDup)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXDET_UPD&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_taxlist.aspx")
        End Try

        If blnIsDup = True Then
            lblErrDup.Visible = True
            Exit Sub
        Else
            strSelTaxCode = Trim(txtTaxCode.Text)
            tbcode.Value = strSelTaxCode 
        End If
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCdUpd As String = "HR_CLSSETUP_TAX_UPD"
        Dim strOpCdGet As String = "HR_CLSSETUP_TAX_GET"
        Dim strOpCdAdd As String = "HR_CLSSETUP_TAX_ADD"
        Dim strOpCdUpdSts As String = "HR_CLSSETUP_TAX_UPD_STS"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim strParam As String = ""

        If strCmdArgs = "Save" Then
            InsertHeader()
        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(txtTaxCode.Text) & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & objHRSetup.EnumTaxStatus.Deleted & chr(9) & chr(9) & chr(9) & chr(9) & chr(9)
            Try
                intErrNo = objHRSetup.mtdUpdTax(strOpCdGet, _
                                                strOpCdAdd, _
                                                strOpCdUpdSts, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True, _
                                                True, _
                                                False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXDET_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_taxdet.aspx?tbcode=" & strSelTaxCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParam = Trim(txtTaxCode.Text) & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & objHRSetup.EnumTaxStatus.Active & chr(9) & chr(9) & chr(9) & chr(9) & chr(9)
            Try
                intErrNo = objHRSetup.mtdUpdTax(strOpCdGet, _
                                                strOpCdAdd, _
                                                strOpCdUpdSts, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True, _
                                                True, _
                                                False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXDET_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_taxdet.aspx?tbcode=" & strSelTaxCode)
            End Try
        End If
        If strSelTaxCode <> "" Then
            onLoad_Display()
            onLoad_LineDisplay()
            onLoad_BindButton()
        End If
    End Sub

    Sub btnAdd_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCdAddLine As String = "HR_CLSSETUP_TAXLN_ADD"
        Dim strOpCdUpdHeader As String = "HR_CLSSETUP_TAX_UPDATEID"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strDocPrefix  As String
        Dim dblFromIncome As Double
        Dim dblToIncome As Double
        Dim arrRange As Array
        Dim arrIncome As Array
        Dim intCnt As Integer
        Dim dblEmprRate As Double
        Dim dblEmpeRate As Double

        If Trim(txtTaxCode.Text) = "" Then
            Exit Sub
        Else
            strSelTaxCode = txtTaxCode.Text
            dblFromIncome = txtFromIncome.Text
            dblToIncome = txtToIncome.Text
        
            If dblFromIncome < 0 Then
                lblErrFromIncome.Visible = True
                Exit Sub
            End If

            If dblToIncome < 0 Then
                lblErrToIncome.Visible = True
                Exit Sub
            End If

            If dblFromIncome > dblToIncome Then
                lblErrRange.Visible = True
                Exit Sub
            Else
                arrRange = Split(lblHidRange.Text, Chr(9))
                If UBound(arrRange, 1) > 0 Then
                    For intCnt = 1 To UBound(arrRange, 1) 
                        arrIncome = Split(arrRange(intCnt), "|")
                        dblFromIncome = CDbl(arrIncome(0))
                        dblToIncome = CDbl(arrIncome(1))
                        If (txtFromIncome.text >= dblFromIncome And txtFromIncome.text <= dblToIncome) _
                        Or (txtToIncome.text >= dblFromIncome And txtToIncome.text <= dblToIncome) _
                        Or (txtFromIncome.text <= dblFromIncome And txtToIncome.text >= dblToIncome) Then
                            lblErrRange.Visible = True
                            Exit Sub
                        End If
                    Next
                End If
            End If

            If txtEmprRate.Text = "" Then
                dblEmprRate = 0 
                txtEmprRate.Text = "0"
            Else
                dblEmprRate = CDbl(txtEmprRate.Text)
            End If

            If txtEmpeRate.Text = "" Then
                dblEmpeRate = 0
                txtEmpeRate.Text = "0"
            Else
                dblEmpeRate = CDbl(txtEmpeRate.Text)
            End If

            If dblEmprRate < 0 Or dblEmprRate > 100 Then
                lblErrEmprRate.Visible = True
                Exit Sub
            End If

            If dblEmpeRate < 0 Or dblEmpeRate > 100 Then
                lblErrEmpeRate.Visible = True
                Exit Sub
            End If

            strDocPrefix = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.TaxLn)
            InsertHeader()
            Try
                strParam = strDocPrefix & chr(9) & _
                           strSelTaxCode & chr(9) & _
                           txtFromIncome.Text & chr(9) & _
                           txtToIncome.Text & chr(9) & _
                           dblEmprRate & chr(9) & _
                           dblEmpeRate

                intErrNo = objHRSetup.mtdUpdTaxLn(strOpCdUpdHeader, _
                                                  strOpCdAddLine, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXDET_ADDLINE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_taxdet.aspx?tbcode=" & strSelTaxCode)
            End Try
        End If

        If Trim(tbcode.Value) <> "" Then
            onLoad_Display()
            onLoad_LineDisplay()
            onLoad_BindButton()
        End If
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim strOpCdDelLine As String = "HR_CLSSETUP_TAXLN_DEL"
        Dim strOpCdUpdHeader As String = "HR_CLSSETUP_TAX_UPDATEID"
        Dim strParam As String
        Dim lbl As Label
        Dim strTaxLnId As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lbl = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCode")
        strTaxLnId = lbl.Text

        Try
            strParam = strTaxLnId & chr(9) & strSelTaxCode
            intErrNo = objHRSetup.mtdUpdTaxLn(strOpCdUpdHeader, _
                                              strOpCdDelLine, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_TAXDET_DELLINE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_taxdet.aspx?tbcode=" & strSelTaxCode)
        End Try
        lblHidRange.Text = ""
        onLoad_Display()
        onLoad_LineDisplay()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_TaxList.aspx")
    End Sub

End Class
