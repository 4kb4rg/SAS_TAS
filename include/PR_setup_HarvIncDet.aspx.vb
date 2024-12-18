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

Imports agri.PR
Imports agri.GlobalHdl


Public Class PR_setup_HarvIncDet : Inherits Page
    Protected WithEvents txtHarvIncCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents txtQuota As Textbox
    Protected WithEvents txtQuotaInc As Textbox
    Protected WithEvents txtAboveQuotaInc As Textbox
    Protected WithEvents txtRateFruit As Textbox
    Protected WithEvents ddlAD As DropDownList
    Protected WithEvents ddlUOM As DropDownList    
    Protected WithEvents ddlType As DropDownList    
    Protected WithEvents rdDivLabour As radiobuttonlist        
    Protected WithEvents rbIncTypeFixed As RadioButton    
    Protected WithEvents rbIncTypeMeasure As RadioButton
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents lblUOM As Label
    Protected WithEvents hicode As HtmlInputHidden
    Protected WithEvents btnFind1 As HtmlInputButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents btnAdd As ImageButton 
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblHarvIncCode As Label

    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrAD As Label
    Protected WithEvents lblErrQuotaFruit As Label
    Protected WithEvents lblErrRateFruit As Label
    Protected WithEvents lblErrField As Label
    Protected WithEvents lblErrDet As Label
    Protected WithEvents lblErrQuota As Label
    Protected WithEvents lblErrQuotaInc As Label
    Protected WithEvents lblErrAboveQuotaInc As Label

    Protected WithEvents TrPayBasis As HtmlTableRow
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid

    Protected objPRSetup As New agri.PR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdminUOM As New agri.Admin.clsUOM()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objHarvIncDs As New Object()
    Dim objHarvIncLnDs as New Object()
    Dim objADDs As New Object()
    Dim objResult As New Object()
    Dim objHIDs As New Object
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Dim strSelectedHICode As String = ""
    Dim intStatus As Integer
    Dim intAboveInc As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRHarvInc), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrAD.Visible = False
            lblErrDup.Visible = False
            lblErrDet.Visible = False
            lblErrQuota.Visible = False
            lblErrQuotaInc.Visible = False
            lblErrAboveQuotaInc.Visible = False

            strSelectedHICode = Trim(IIf(Request.QueryString("hicode") <> "", Request.QueryString("hicode"), Request.Form("hicode")))            
            intStatus = CInt(lblHiddenSts.Text)            

            If Not IsPostBack Then
                BindLabourType()                
                If strSelectedHICode <> "" Then
                    hicode.Value = strSelectedHICode                     
                    onLoad_Display()                    
                    onLoad_LineDisplay()
                Else
                    BindAD("")
                    BindddlType("")
                    onLoad_BindUOM("")
                    onLoad_BindButton()
                End If                
            End If
        End If
    End Sub

    Sub onLoad_BindButton()
        txtHarvIncCode.Enabled = False
        txtDescription.Enabled = False
        txtQuota.Enabled = False
        txtQuotaInc.Enabled = False
        txtAboveQuotaInc.Enabled = False
        ddlAD.Enabled = False
        ddlType.Enabled = False
        ddlUOM.Enabled = False
        rdDivLabour.Enabled = False
        rbIncTypeFixed.Enabled = False
        rbIncTypeMeasure.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        btnFind1.Disabled = False
        TrPayBasis.Visible = False

        Select Case intStatus
            Case objPRSetup.EnumHarvIncentiveStatus.Active
                txtDescription.Enabled = True
                txtQuota.Enabled = True
                txtQuotaInc.Enabled = True
                ddlAD.Enabled = True
                If intAboveInc = "" Then
                    txtAboveQuotaInc.Enabled = True
                Else
                    txtAboveQuotaInc.Enabled = False
                    txtAboveQuotaInc.Text = intAboveInc
                End If
                ddlType.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                TrPayBasis.Visible = False
            Case objPRSetup.EnumHarvIncentiveStatus.Deleted
                btnFind1.Disabled = True
                UnDelBtn.Visible = True
            Case Else
                txtHarvIncCode.Enabled = True
                txtDescription.Enabled = True
                txtQuota.Enabled = True
                txtQuotaInc.Enabled = True
                ddlAD.Enabled = True
                ddlType.Enabled = True
                If intAboveInc = "" Then
                    txtAboveQuotaInc.Enabled = True
                Else
                    txtAboveQuotaInc.Enabled = False
                    txtAboveQuotaInc.Text = intAboveInc
                End If
                rdDivLabour.Enabled = True
                rbIncTypeFixed.Enabled = True
                rbIncTypeMeasure.Enabled = True
                SaveBtn.Visible = True
                rbIncTypeFixed.Checked = True
                TrPayBasis.Visible = False
        End Select
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_HARVINCENTIVE_GET"
        Dim strParam As String = strLocation & "|" & strSelectedHICode        
        Dim intErrNo As Integer
        Try
            intErrNo = objPRSetup.mtdGetHarvIncentive(strOpCd, _
                                                      strParam, _
                                                      objHarvIncDs, _
                                                      True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_HARVINCENTIVE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try        
        
        txtHarvIncCode.Text = strSelectedHICode
        If objHarvIncDs.Tables(0).Rows.Count > 0 Then
            txtDescription.Text = Trim(objHarvIncDs.Tables(0).Rows(0).Item("Description"))            
            intStatus = CInt(Trim(objHarvIncDs.Tables(0).Rows(0).Item("Status")))            
            lblHiddenSts.Text = Trim(objHarvIncDs.Tables(0).Rows(0).Item("Status"))
            lblStatus.Text = objPRSetup.mtdGetHarvIncentiveStatus(Trim(objHarvIncDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objHarvIncDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objHarvIncDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objHarvIncDs.Tables(0).Rows(0).Item("UserName"))            
            
            If Cint(objHarvIncDs.Tables(0).Rows(0).Item("DivLabour")) <> 0 Then
                rdDivLabour.SelectedValue = Cint(objHarvIncDs.Tables(0).Rows(0).Item("DivLabour"))
            End If 
            
            If objHarvIncDs.Tables(0).Rows(0).Item("ProgressiveInd") = "1" Then
                rbIncTypeFixed.Checked = True
                rbIncTypeMeasure.Checked = False
                TrPayBasis.Visible = False
            Else            
                rbIncTypeFixed.Checked = False
                rbIncTypeMeasure.Checked = True

                TrPayBasis.Visible = True
            End If

            BindAD(Trim(objHarvIncDs.Tables(0).Rows(0).Item("ADCode")))
            BindddlType(Trim(objHarvIncDs.Tables(0).Rows(0).Item("PickerCategory")))        
            onLoad_BindButton()
            If rdDivLabour.SelectedItem.Value <> objPRSetup.EnumDivisionLabour.NonDOLPaid Then
                If rdDivLabour.SelectedItem.Value <> objPRSetup.EnumDivisionLabour.NonDOLUnpaid Then                            
                    TrPayBasis.Visible = True
                    ddlType.Enabled = False
                End If
            End If

            If rdDivLabour.SelectedItem.Value = objPRSetup.EnumDivisionLabour.Others Then 
                TrPayBasis.Visible = False
                tblSelection.Visible = False
            Else
                tblSelection.Visible = True
            End If
        End If
    End Sub

    Sub BindAD(ByVal pv_strAD As String)
        Dim strOpCode As String = "PR_CLSSETUP_ADLIST_GET"
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_HARVINCENTIVE_AD_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For intCnt = 0 To objADDs.Tables(0).Rows.Count - 1
            objADDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(objADDs.Tables(0).Rows(intCnt).Item("ADCode"))
            objADDs.Tables(0).Rows(intCnt).Item("Description") = objADDs.Tables(0).Rows(intCnt).Item("ADCode") & " (" & Trim(objADDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objADDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(pv_strAD) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objADDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("Description") = "Select Allowance & Deduction Code"
        objADDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAD.DataSource = objADDs.Tables(0)
        ddlAD.DataValueField = "ADCode"
        ddlAD.DataTextField = "Description"
        ddlAD.DataBind()
        ddlAD.SelectedIndex = intSelectIndex
    End Sub

    Sub onLoad_BindUOM(ByVal pv_strUOMCode As String)
        Dim strOpCd As String = "ADMIN_CLSUOM_UOM_LIST_GET"
        Dim objUOMDs As New Dataset()
        Dim dr As DataRow
        Dim strParam As String = "Order By UOM.UOMCode|And UOM.Status = '" & objAdminUOM.EnumUOMStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objAdminUOM.mtdGetUOM(strOpCd, _
                                             strParam, _
                                             objUOMDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_HARVINCENTIVE_UOM_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objUOMDs.Tables(0).Rows.Count - 1
            objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode"))
            objUOMDs.Tables(0).Rows(intCnt).Item("UOMDesc") = Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode")) & " (" & Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMDesc")) & ")"
            If objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(pv_strUOMCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objUOMDs.Tables(0).NewRow()
        dr("UOMCode") = ""
        dr("UOMDesc") = "Select UOM"
        objUOMDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlUOM.DataSource = objUOMDs.Tables(0)
        ddlUOM.DataValueField = "UOMCode"
        ddlUOM.DataTextField = "UOMDesc"
        ddlUOM.DataBind()
        ddlUOM.SelectedIndex = intSelectedIndex
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_HARVINCENTIVE_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_HARVINCENTIVE_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_HARVINCENTIVE_GET"
        Dim strOpCd_Sts As String = "PR_CLSSETUP_HARVINCENTIVE_STATUS_UPD"
        Dim strOpCd As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intIncType As Integer
        Dim intIncAmt As Integer
        Dim intErrNo As Integer
        Dim strParam As String = ""

        If ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            InsertHIRecord()                




        ElseIf strCmdArgs = "Del" Then
            strParam = strSelectedHICode & "|" & objPRSetup.EnumHarvIncentiveStatus.Deleted
            Try
                intErrNo = objPRSetup.mtdUpdHarvIncentive(strOpCd_Sts, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strParam, _
                                                          True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_HARVINCENTIVE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/PR_setup_HarvIncDet.aspx?hicode=" & strSelectedHICode)
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = strSelectedHICode & "|" & objPRSetup.EnumHarvIncentiveStatus.Active
            Try
                intErrNo = objPRSetup.mtdUpdHarvIncentive(strOpCd_Sts, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_HARVINCENTIVE_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/PR_setup_HarvIncDet.aspx?hicode=" & strSelectedHICode)
            End Try
        End If

        If strSelectedHICode <> "" Then
            onLoad_Display()
            onLoad_LineDisplay()
        End If
    End Sub

    Sub HidePaymentBasis(ByVal Sender As Object, ByVal E As EventArgs)
        BindddlType(Cint(objPRSetup.EnumPickerType.NotApplicable))    
        If rdDivLabour.SelectedItem.Value <> objPRSetup.EnumDivisionLabour.NonDOLPaid Then
            If rdDivLabour.SelectedItem.Value <> objPRSetup.EnumDivisionLabour.NonDOLUnpaid Then
                If rbIncTypeFixed.Checked = True Then
                    TrPayBasis.Visible = False                    
                Else
                    TrPayBasis.Visible = True
                    BindddlType(Cint(objPRSetup.EnumPickerType.BHL))                    
                End If
            Else
                TrPayBasis.Visible = False                
                rbIncTypeFixed.Checked = True
            End If 
        Else
            TrPayBasis.Visible = False            
            rbIncTypeFixed.Checked = True
        End IF
        
    End Sub

    Sub ChangeUOM(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strUOM as String = ""
        rbIncTypeFixed.Enabled = True                
        rbIncTypeMeasure.Enabled = True
        TrPayBasis.Visible = False
        tblSelection.Visible = True

        If rdDivLabour.SelectedItem.Value <> objPRSetup.EnumDivisionLabour.NonDOLPaid Then
            txtRateFruit.Enabled = False            
            BindddlType(Cint(objPRSetup.EnumPickerType.NotApplicable))
            If rdDivLabour.SelectedItem.Value <> objPRSetup.EnumDivisionLabour.NonDOLUnpaid Then                
                If rbIncTypeFixed.Checked = True Then                    
                    TrPayBasis.Visible = False                    
                Else                    
                    TrPayBasis.Visible = True
                    BindddlType(objPRSetup.EnumPickerType.BHL)
                End If
            Else
                TrPayBasis.Visible = False                
                rbIncTypeFixed.Checked = True
            End If 
        Else
            txtRateFruit.Enabled = True
            TrPayBasis.Visible = False
            rbIncTypeFixed.Checked = True
        End IF        

        If rdDivLabour.SelectedItem.Value = objPRSetup.EnumDivisionLabour.Prune Then
            strUOM  = "Prunes"
            rbIncTypeFixed.Enabled = False
            rbIncTypeMeasure.Checked = True
            rbIncTypeFixed.Checked = False            
            BindddlType(Cint(objPRSetup.EnumPickerType.NotApplicable))            
        Else
            strUOM = "Bunches"
        End If
        lblUOM.Text = strUOM        

        If rdDivLabour.SelectedItem.Value = objPRSetup.EnumDivisionLabour.Others Then
            rbIncTypeFixed.Enabled = False
            rbIncTypeMeasure.Enabled = False
            TrPayBasis.Visible = False
            tblSelection.Visible = False
        End If        

    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_setup_HarvIncList.aspx")
    End Sub

    Sub BindLabourType()
        rdDivLabour.Items.Add(New ListItem(objPRSetup.mtdGetDivisionLabour(objPRSetup.EnumDivisionLabour.NonDOLPaid), objPRSetup.EnumDivisionLabour.NonDOLPaid))
        rdDivLabour.Items.Add(New ListItem(objPRSetup.mtdGetDivisionLabour(objPRSetup.EnumDivisionLabour.NonDOLUnpaid), objPRSetup.EnumDivisionLabour.NonDOLUnpaid))
        rdDivLabour.Items.Add(New ListItem(objPRSetup.mtdGetDivisionLabour(objPRSetup.EnumDivisionLabour.DOL1), objPRSetup.EnumDivisionLabour.DOL1))
        rdDivLabour.Items.Add(New ListItem(objPRSetup.mtdGetDivisionLabour(objPRSetup.EnumDivisionLabour.DOL2), objPRSetup.EnumDivisionLabour.DOL2))
        rdDivLabour.Items.Add(New ListItem(objPRSetup.mtdGetDivisionLabour(objPRSetup.EnumDivisionLabour.Prune), objPRSetup.EnumDivisionLabour.Prune))
        rdDivLabour.Items.Add(New ListItem(objPRSetup.mtdGetDivisionLabour(objPRSetup.EnumDivisionLabour.Others), objPRSetup.EnumDivisionLabour.Others))
        rdDivLabour.SelectedIndex = 0
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdLn_Add as String = "PR_CLSSETUP_HARVINCENTIVE_LINE_ADD"
        Dim strOpCd_Add As String = "PR_CLSSETUP_HARVINCENTIVE_STATUS_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_HARVINCENTIVE_LINE_SEARCH"
        Dim objSearch As New object()

        Dim strOpCd As String
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim intIncType As Integer
        Dim intPickerType As Integer
        Dim intQuotaFruit As Double
        Dim intRateFruit As Integer
        Dim intQuotaInc As Double
        Dim intAbvQuotaInc As Double

        If ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        ElseIf Trim(txtQuota.Text) = "" Then
            lblErrQuota.Visible = True
            Exit Sub
        ElseIf Trim(txtQuotaInc.Text) = "" Then
            lblErrQuotaInc.Visible = True
            Exit Sub
        ElseIf Trim(txtAboveQuotaInc.Text) = "" Then
            lblErrAboveQuotaInc.Visible = True
            Exit Sub
	    ElseIf rdDivLabour.SelectedItem.Value = objPRSetup.EnumDivisionLabour.NonDOLPaid Then
            If txtRateFruit.Text = "" Then
       		    lblErrRateFruit.Visible = True
        	    Exit Sub
            End If
	    End If
        
        If Trim(txtQuotaInc.Text) = "" Then
            intQuotaInc = "0"
        Else
            intQuotaInc = FormatNumber(txtQuotaInc.Text, 0)
        End If

        If Trim(txtAboveQuotaInc.Text) = "" Then
            intAbvQuotaInc = "0"
        Else
            intAbvQuotaInc = FormatNumber(txtAboveQuotaInc.Text, 0)
        End If

        If Trim(txtRateFruit.Text) = "" Then
            intRateFruit = "0"
        Else
            intRateFruit = FormatNumber(txtRateFruit.Text, 0)
        End If
    
        If intAboveInc = "" Then
            intAboveInc = intAbvQuotaInc
        End If

        InsertHIRecord()        
        If strSelectedHICode = "" Then
            Exit sub
        Else
            strParam = strLocation & "|" & strSelectedHICode & "|" & _
                       Trim(txtQuota.Text) & "|" & _                       
                       " Order BY hln.QuotaQty" 
            Try
                intErrNo = objPRSetup.mtdGetHarvIncentiveLN(strOpCd_Get, _
                                                            strParam, _
                                                            objSearch, _
                                                            False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_LINE_SEARCH&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objSearch.Tables(0).Rows.Count > 0 And intErrNo = 0 Then
                lblErrDet.Visible = True
                Exit Sub
            Else                
                strParam = strSelectedHICode & "|" & _
                       Trim(txtQuota.Text) & "|" & _ 
                       intQuotaInc & "|" & _
                       intAbvQuotaInc & "|" & _
                       intRateFruit & "|" & _ 
                       objPRSetup.EnumHarvIncentiveStatus.Active 

                Try
                    intErrNo = objPRSetup.mtdUpdHarvIncentiveLine(strOpCd_Add, _
                                                    strOpCdLn_Add, _ 
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    False, _
                                                    objResult)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_LINE_ADD&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripDet.aspx")
                End Try
            End If
        End If

        If strSelectedHICode <> "" Then
            onLoad_Display()
            onLoad_LineDisplay()
        End If
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objDel As New Object()
        Dim strOpCode_DelLine As String = "PR_CLSSETUP_HARVINCENTIVE_LINE_DEL"
        Dim strOpCode_UpdID As String = "PR_CLSSETUP_HARVINCENTIVE_STATUS_UPD"
        Dim strParam As String
        Dim lblDelText As Label
        Dim strHICode As String
        Dim intQuotaQty As Integer
        Dim strProgressiveInd As String
        Dim strPickerType As String        
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblHarvIncCode")
        strHICode = lblDelText.Text
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblHdQuotaQty")
        intQuotaQty = lblDelText.Text
        
        strParam =  strHICode & "|" & intQuotaQty & _
                    "||||" & objPRSetup.EnumHarvIncentiveStatus.Active 
        Try
            intErrNo = objPRSetup.mtdUpdHarvIncentiveLine(strOpCode_UpdID, _
                                            strOpCode_DelLine, _ 
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            False, _
                                            objDel)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_LINE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PR/trx/PR_trx_TripDet.aspx")
        End Try        

        If strSelectedHICode <> "" Then
            onLoad_Display()
            onLoad_LineDisplay()
        End If
    End Sub

    Sub onLoad_LineDisplay()
        Dim strOpCd As String = "PR_CLSSETUP_HARVINCENTIVE_LINE_GET"
        Dim strParam As String = strLocation & "|" & strSelectedHICode
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        Try
            intErrNo = objPRSetup.mtdGetHarvIncentive(strOpCd, _
                                                      strParam, _
                                                      objHarvIncLnDs, _
                                                      True)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_LINE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        btnAdd.Visible = True
        If  objHarvIncLnDs.Tables(0).Rows.Count > 0 Then
            If rbIncTypeFixed.Checked Then
                btnAdd.Visible = True
            Else
                btnAdd.Visible = False
            End If
                        
            If CDbl(objHarvIncLnDs.Tables(0).Rows(intCnt).Item("AbvQuotaInc")) <> 0 Then
                intAboveInc = objHarvIncLnDs.Tables(0).Rows(intCnt).Item("AbvQuotaInc")
            End If
            
        End IF

        dgLineDet.DataSource = objHarvIncLnDs.Tables(0)
        dgLineDet.DataBind()
        
        For intCnt=0 To dgLineDet.Items.Count - 1
            Select Case CInt(objHarvIncDs.Tables(0).Rows(0).Item("Status"))
                Case objPRSetup.EnumHarvIncentiveStatus.Active
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPRSetup.EnumHarvIncentiveStatus.Deleted
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.visible = False
            End Select
        Next

        If intAboveInc = "" Then
            txtAboveQuotaInc.Enabled = True
        Else
            txtAboveQuotaInc.Enabled = False
            txtAboveQuotaInc.Text = FormatNumber(intAboveInc, 0)
        End If

    End Sub

    Sub InsertHIRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_HARVINCENTIVE_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_HARVINCENTIVE_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_HARVINCENTIVE_GET"
        dim strOpCd As String
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""
        Dim intIncType As Integer
        Dim intPickerType As Integer
        
        intIncType = IIf(rbIncTypeFixed.Checked, objPRSetup.EnumHarvProgressiveType.Progressive, objPRSetup.EnumHarvProgressiveType.NonProgressive)
        
        
        If ddlType.SelectedItem.Value = "" Then
            intPickerType = 0
        Else
            intPickerType = ddlType.SelectedItem.Value
        End if

        If Trim(txtHarvIncCode.Text) = "" Or Trim(txtDescription.Text) = "" Then
            lblErrField.Visible = True
            Exit Sub
        End If

        strParam = strLocation & "|" & Trim(txtHarvIncCode.Text)
        Try
            intErrNo = objPRSetup.mtdGetHarvIncentive(strOpCd_Get, _
                                                      strParam, _
                                                      objHIDs, _
                                                      True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        If objHIDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
            lblErrDup.Visible = True
            Exit Sub
        Else    
            strOpCd = IIF(intStatus = 0, strOpCd_Add,strOpCd_Upd)            
            strSelectedHICode = Trim(txtHarvIncCode.Text)            
            hicode.Value = strSelectedHICode                        
            strParam = Trim(txtHarvIncCode.Text) & "|" & _
                        Trim(txtDescription.Text) & "|" & _
                        ddlAD.SelectedItem.Value & "|" & _
                        rdDivLabour.SelectedItem.Value & "|" & _
                        intIncType & "|" & _
                        intPickerType & "|" & _
                        objPRSetup.EnumHarvIncentiveStatus.Active
            Try
                intErrNo = objPRSetup.mtdUpdHarvIncentive(strOpCd, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strParam, _
                                                          False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_HARVINCENTIVE_ADD&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_HSDet.aspx?HARVINCCODE=" & strSelectedHICode)
            End Try           
        End If
    End Sub

    Sub BindddlType(Byval pv_strType As String)
        ddlType.Items.Clear        
        ddlType.Items.Add(New ListItem("Select Type", ""))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetPickerType(objPRSetup.EnumPickerType.SKU), objPRSetup.EnumPickerType.SKU))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetPickerType(objPRSetup.EnumPickerType.BHL), objPRSetup.EnumPickerType.BHL))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetPickerType(objPRSetup.EnumPickerType.NonQuota), objPRSetup.EnumPickerType.NonQuota))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetPickerType(objPRSetup.EnumPickerType.Quota), objPRSetup.EnumPickerType.Quota))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetPickerType(objPRSetup.EnumPickerType.Carrier), objPRSetup.EnumPickerType.Carrier))
        ddlType.Items.Add(New ListItem(objPRSetup.mtdGetPickerType(objPRSetup.EnumPickerType.NotApplicable), objPRSetup.EnumPickerType.NotApplicable))
                
        If pv_strType = "" Then
            ddlType.SelectedValue = ""
        Else
            ddlType.SelectedValue = CInt(pv_strType)
        End If        
    End Sub


End Class
