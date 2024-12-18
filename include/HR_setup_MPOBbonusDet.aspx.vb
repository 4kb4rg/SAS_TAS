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

Public Class HR_setup_MPOBbonusDet : Inherits Page

    Protected WithEvents txtBonusCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents ddlAD As DropDownList
    Protected WithEvents rbPriceBonus As RadioButton
    Protected WithEvents rbAddPay As RadioButton
    Protected WithEvents rbLoadBasicPay As RadioButton
    Protected WithEvents rbHarvBasicPay As RadioButton
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents txtFromPrice As Textbox
    Protected WithEvents txtToPrice As Textbox
    Protected WithEvents txtFromYieldBracket As Textbox
    Protected WithEvents txtToYieldBracket As Textbox
    Protected WithEvents txtBonusPrice As Textbox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents btnFind1 As HtmlInputButton
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents bonuscode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrAD As Label
    Protected WithEvents lblErrRange As Label
    Protected WithEvents lblErrYieldRange As Label
    Protected WithEvents lblInvalidRange As Label
    Protected WithEvents lblInvalidYieldRange As Label
    Protected WithEvents lblHidIncomeRange As Label
    Protected WithEvents lblHidYieldRange As Label
    Protected WithEvents lblPriceRate As Label
    Protected WithEvents lblHidPriceBonus As Label
    Protected WithEvents lblHidAddPay As Label
    Protected WithEvents lblHidLoadBasicPay As Label
    Protected WithEvents lblHidHarvBasicPay As Label
    Protected WithEvents TrYield As HTMLTableRow

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objBonusDs As New Object()
    Dim objBonusLnDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim strSelBonusCode As String = ""
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
            lblErrAD.Visible = False
            lblErrDup.Visible = False
            lblErrRange.Visible = False
            lblErrYieldRange.Visible = False
            strSelBonusCode = Trim(IIf(Request.QueryString("bonuscode") <> "", Request.QueryString("bonuscode"), Request.Form("bonuscode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelBonusCode <> "" Then
                    bonuscode.value = strSelBonusCode
                    onLoad_Display()
                    onLoad_DisplayLine()
                    onLoad_BindButton()
                Else
                    rbPriceBonus.checked = True
                    lblPriceRate.text = lblHidPriceBonus.text
                    BindAD("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onCheck_PriceRate(ByVal Sender As Object, ByVal E As EventArgs)
        If rbPriceBonus.Checked = True Then
            lblPriceRate.text = lblHidPriceBonus.text
            TrYield.Visible = False
            txtFromYieldBracket.Text = "0"
            txtToYieldBracket.Text = "0"
        ElseIf rbAddPay.Checked = True
            lblPriceRate.text = lblHidAddPay.text
            TrYield.Visible = False
            txtFromYieldBracket.Text = "0"
            txtToYieldBracket.Text = "0"
        ElseIf rbLoadBasicPay.Checked = True
            lblPriceRate.text = lblHidLoadBasicPay.text
            TrYield.Visible = False
            txtFromYieldBracket.Text = "0"
            txtToYieldBracket.Text = "0"
        Elseif rbHarvBasicPay.Checked = True
            lblPriceRate.text = lblHidHarvBasicPay.text
            TrYield.Visible = True
        End If
    End Sub

    Sub onLoad_BindButton()
        txtBonusCode.Enabled = False
        txtDescription.Enabled = False
        ddlAD.Enabled = False
        rbPriceBonus.Enabled = False
        rbAddPay.Enabled = False
        rbLoadBasicPay.Enabled = False
        rbHarvBasicPay.Enabled = False
        tblSelection.Visible = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        btnFind1.Disabled = False

        Select Case intStatus
            Case objHRSetup.EnumBonusStatus.Active
                txtBonusCode.Enabled = True
                txtDescription.Enabled = True
                ddlAD.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumBonusStatus.Deleted
                btnFind1.Disabled = True
                UnDelBtn.Visible = True
            Case Else
                txtBonusCode.Enabled = True
                txtDescription.Enabled = True
                ddlAD.Enabled = True
                rbPriceBonus.Enabled = True
                rbAddPay.Enabled = True
                rbLoadBasicPay.Enabled = True
                rbHarvBasicPay.Enabled = True
                tblSelection.Visible = True
                SaveBtn.Visible = True
        End Select
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_MPOBBONUS_GET"
        Dim strParam As String = strSelBonusCode        
        Dim intErrNo As Integer

        Try
            intErrNo = objHRSetup.mtdGetBonus(strOpCd, _
                                              strParam, _
                                              objBonusDs, _
                                              True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BONUS_GET&errmesg=" & Exp.ToString() & "&redirect=hr/setup/hr_setup_mpobbonuslist.aspx")
        End Try

        txtBonusCode.Text = strSelBonusCode
        txtDescription.Text = objBonusDs.Tables(0).Rows(0).Item("Description").Trim()
        BindAD(objBonusDs.Tables(0).Rows(0).Item("ADCode").Trim())

        Select Case CInt(objBonusDs.Tables(0).Rows(0).Item("BonusType"))
            Case objHRSetup.EnumBonusType.PriceBonus
                rbPriceBonus.Checked = True
                lblPriceRate.text = lblHidPriceBonus.text
                TrYield.Visible = False
            Case objHRSetup.EnumBonusType.AdditionalPay
                rbAddPay.Checked = True
                lblPriceRate.text = lblHidAddPay.text
                TrYield.Visible = False
            Case objHRSetup.EnumBonusType.LoadBasicPay
                rbLoadBasicPay.Checked = True
                lblPriceRate.text = lblHidLoadBasicPay.text
                TrYield.Visible = False
            Case objHRSetup.EnumBonusType.HarvestBasicPay
                rbHarvBasicPay.Checked = True
                lblPriceRate.text = lblHidHarvBasicPay.text
                TrYield.Visible = True
        End Select


        intStatus = CInt(objBonusDs.Tables(0).Rows(0).Item("Status"))
        lblHiddenSts.Text = objBonusDs.Tables(0).Rows(0).Item("Status").Trim()
        lblStatus.Text = objHRSetup.mtdGetBonusStatus(objBonusDs.Tables(0).Rows(0).Item("Status"))
        lblDateCreated.Text = objGlobal.GetLongDate(objBonusDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objBonusDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = objBonusDs.Tables(0).Rows(0).Item("UserName")

    End Sub


    Sub onLoad_DisplayLine()
        Dim strOpCd_GetLine As String = "HR_CLSSETUP_MPOBBONUS_LINE_GET"
        Dim strParam As String = strSelBonusCode
        Dim lbButton As LinkButton
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Try
            intErrNo = objHRSetup.mtdGetBonus(strOpCd_GetLine, _
                                              strParam, _
                                              objBonusLnDs, _
                                              True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BONUSLINE_GET&errmesg=" & Exp.ToString() & "&redirect=hr/setup/hr_setup_mpobbonuslist.aspx")
        End Try

        For intCnt = 0 To objBonusLnDs.Tables(0).Rows.Count - 1
            objBonusLnDs.Tables(0).Rows(intCnt).Item("BonusLnId") = Trim(objBonusLnDs.Tables(0).Rows(intCnt).Item("BonusLnId"))
            lblHidIncomeRange.Text = lblHidIncomeRange.Text & Chr(9) & _
                                     objBonusLnDs.Tables(0).Rows(intCnt).Item("FromPrice") & "|" & _
                                     objBonusLnDs.Tables(0).Rows(intCnt).Item("ToPrice")
            lblHidYieldRange.Text = lblHidYieldRange.Text & Chr(9) & _
                                    objBonusLnDs.Tables(0).Rows(intCnt).Item("FromPrice") & "|" & _
                                    objBonusLnDs.Tables(0).Rows(intCnt).Item("ToPrice") & "|" & _
                                    objBonusLnDs.Tables(0).Rows(intCnt).Item("FromYieldBracket") & "|" & _
                                    objBonusLnDs.Tables(0).Rows(intCnt).Item("ToYieldBracket")
        Next intCnt
        dgLineDet.Columns(4).HeaderText = lblPriceRate.text 
        If rbHarvBasicPay.Checked Then
            dgLineDet.Columns(2).Visible = True
            dgLineDet.columns(3).Visible = True
        End If
        dgLineDet.DataSource = objBonusLnDs.Tables(0)
        dgLineDet.DataBind()

        For intCnt = 0 To dgLineDet.Items.Count - 1
            Select Case intStatus
                Case objHRSetup.EnumBonusStatus.Active
                        lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objHRSetup.EnumBonusStatus.Deleted
                        lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BONUS_AD_GET&errmesg=" & Exp.ToString() & "&redirect=")
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

    Sub InsertBonusRecord(ByRef pv_blnIsFail As Boolean)
        Dim strOpCd_Add As String = "HR_CLSSETUP_MPOBBONUS_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_MPOBBONUS_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_MPOBBONUS_GET"
        Dim strOpCd As String
        Dim strBonusType As String
        Dim intErrNo As Integer
        Dim strParam As String = ""

        pv_blnIsFail = True

        If ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        End If

        strParam = Trim(txtBonusCode.Text)
        Try
            intErrNo = objHRSetup.mtdGetBonus(strOpCd_Get, _
                                              strParam, _
                                              objBonusDs, _
                                              True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_MPOBBONUS_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objBonusDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
            lblErrDup.Visible = True
            Exit Sub
        Else
            strSelBonusCode = Trim(txtBonusCode.Text)
            bonuscode.value = strSelBonusCode

            strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
            If rbPriceBonus.Checked = True Then
                strBonusType = objHRSetup.EnumBonusType.PriceBonus
            ElseIf rbAddPay.Checked = True Then
                strBonusType = objHRSetup.EnumBonusType.AdditionalPay
            ElseIf rbLoadBasicPay.Checked = True Then
                strBonusType = objHRSetup.EnumBonusType.LoadBasicPay
            ElseIf rbHarvBasicPay.Checked = True Then
                strBonusType = objHRSetup.EnumBonusType.HarvestBasicPay
            End If

            strParam = Trim(txtBonusCode.Text) & "|" & _
                       Trim(txtDescription.Text) & "|" & _
                       ddlAD.SelectedItem.Value & "|" & _
                       strBonusType & "|" & _
                       objHRSetup.EnumBonusStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdBonus(strOpCd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_MPOBBONUS_SAVE&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_MPOBbonusDet.aspx?bonuscode=" & strSelBonusCode)
            End Try
            pv_blnIsFail = False
        End If        
    End Sub

    Sub AddBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim blnIsFail As Boolean
        Dim strOpCode_AddLine As String = "HR_CLSSETUP_MPOBBONUS_LINE_ADD"
        Dim strOpCode_UpdID As String = "HR_CLSSETUP_MPOBBONUS_UPDATEID_UPD"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intCnt2 As Integer
        Dim arrRange As Array
        Dim arrYieldRange As Array
        Dim arrPrice As Array
        Dim arrYieldBracket As Array
        Dim dblFromPrice As Double
        Dim dblToPrice As Double
        Dim dblFromYieldBracket As Double
        Dim dblToYieldBracket As Double

        If strSelBonusCode = "" And Trim(txtBonusCode.Text) = "" Then
            Exit Sub
        Else
            InsertBonusRecord(blnIsFail)
            If blnIsFail = True Then
                Exit Sub
            End If

            If rbHarvBasicPay.Checked Then
                If CDbl(txtFromPrice.Text) > CDbl(txtToPrice.Text) Then
                    lblErrRange.Visible = True
                    Exit Sub
                End If

                If CDbl(txtFromYieldBracket.Text) > CDbl(txtToYieldBracket.Text) Then
                    lblErrYieldRange.Visible = True
                    Exit Sub
                End If
                arrYieldRange = Split(lblHidYieldRange.Text, Chr(9))

                If UBound(arrYieldRange,1) > 0 Then
                    For intCnt = 1 To UBound(arrYieldRange, 1)
                        arrPrice = Split(arrYieldRange(intCnt), "|")
                        dblFromPrice = CDbl(arrPrice(0))
                        dblToPrice = CDbl(arrPrice(1))

                        If txtFromPrice.text = dblFromPrice And txtToPrice.text = dblToPrice Then
                            dblFromYieldBracket = CDbl(arrPrice(2))
                            dblToYieldBracket = CDbl(arrPrice(3))
                            If (txtFromYieldBracket.text >= dblFromYieldBracket And txtFromYieldBracket.text <= dblToYieldBracket) _
                            Or (txtToYieldBracket.text >= dblFromYieldBracket And txtToYieldBracket.text <= dblToYieldBracket) _
                            Or (txtFromYieldBracket.text <= dblFromYieldBracket And txtToYieldBracket.text >= dblToYieldBracket) Then
                                lblErrYieldRange.Visible = True
                                Exit Sub
                            End If
                        Else
                            If (txtFromPrice.text >= dblFromPrice And txtFromPrice.text <= dblToPrice) _
                            Or (txtToPrice.text >= dblFromPrice And txtToPrice.text <= dblToPrice) _
                            Or (txtFromPrice.text <= dblFromPrice And txtToPrice.text >= dblToPrice) Then
                                lblErrRange.Visible = True
                                Exit Sub
                            End If
                        End If              
                    Next
                End If
            Else
                If CDbl(txtFromPrice.Text) > CDbl(txtToPrice.Text) Then
                    lblErrRange.Visible = True
                    Exit Sub
                End If
                arrRange = Split(lblHidIncomeRange.Text, Chr(9))
                If UBound(arrRange, 1) > 0 Then
                    For intCnt = 1 To UBound(arrRange, 1) 
                        arrPrice = Split(arrRange(intCnt), "|")
                        dblFromPrice = CDbl(arrPrice(0))
                        dblToPrice = CDbl(arrPrice(1))
                        If (txtFromPrice.text >= dblFromPrice And txtFromPrice.text <= dblToPrice) _
                        Or (txtToPrice.text >= dblFromPrice And txtToPrice.text <= dblToPrice) _
                        Or (txtFromPrice.text <= dblFromPrice And txtToPrice.text >= dblToPrice) Then
                            lblErrRange.Visible = True
                            Exit Sub
                        End If
                    Next
                End If
            End If

            Try
                strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.MPOBBonusLn) & "||" & _
                           strSelBonusCode & "|" & _
                           txtFromPrice.Text & "|" & _
                           txtToPrice.Text & "|" & _
                           txtFromYieldBracket.Text & "|" & _
                           txtToYieldBracket.Text & "|" & _
                           txtBonusPrice.Text

                intErrNo = objHRSetup.mtdUpdBonusLine(strOpCode_UpdID, _
                                                      strOpCode_AddLine, _
                                                      strCompany, _
                                                      strLocation, _
                                                      strUserId, _
                                                      strParam, _
                                                      False, _
                                                      objResult)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_MPOBBONUSLN_ADD&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_MPOBbonusDet.aspx?bonuscode=" & strSelBonusCode)
            End Try
        End If

        onLoad_Display()
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "HR_CLSSETUP_MPOBBONUS_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_MPOBBONUS_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_MPOBBONUS_GET"
        Dim strOpCd_Sts As String = "HR_CLSSETUP_MPOBBONUS_STATUS_UPD"
        Dim blnIsFail As Boolean
        Dim strOpCd As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim strParam As String = ""

        If strCmdArgs = "Save" Then
            InsertBonusRecord(blnIsFail)
        ElseIf strCmdArgs = "Del" Then
            strParam = strSelBonusCode & "|" & objHRSetup.EnumBonusStatus.Deleted
            Try
                intErrNo = objHRSetup.mtdUpdBonus(strOpCd_Sts, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_MPOBBONUS_DEL&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_MPOBbonusDet.aspx?bonuscode=" & strSelBonusCode)
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = strSelBonusCode & "|" & objHRSetup.EnumBonusStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdBonus(strOpCd_Sts, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_MPOBBONUS_UNDEL&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_MPOBbonusDet.aspx?bonuscode=" & strSelBonusCode)
            End Try
        End If

        If strSelBonusCode <> "" Then
            onLoad_Display()
            onLoad_DisplayLine()
            onLoad_BindButton()
        End If
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "HR_CLSSETUP_MPOBBONUS_LINE_DEL"
        Dim strOpCode_UpdID As String = "HR_CLSSETUP_MPOBBONUS_UPDATEID_UPD"
        Dim strParam As String
        Dim lblDelText As Label
        Dim strBonusLnId As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("BonusLnId")
        strBonusLnId = lblDelText.Text

        Try
            strParam = "|" & strBonusLnId & "||||||"
            intErrNo = objHRSetup.mtdUpdBonusLine(strOpCode_UpdID, _
                                                  strOpCode_DelLine, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  False, _
                                                  objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_MPOBBONUSLINE_DEL&errmesg=" & Exp.ToString() & "&redirect=hr/setup/HR_setup_MPOBbonusDet.aspx?bonuscode=" & strSelBonusCode)
        End Try

        lblHidIncomeRange.text = ""
        onLoad_Display()
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_MPOBbonusList.aspx")
    End Sub

End Class
