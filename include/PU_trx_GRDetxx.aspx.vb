

Imports System
Imports System.Data
Imports System.Collections
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.Drawing.Color
Imports System.Math



Public Class PU_GRDet : Inherits Page

    Protected WithEvents lblErrOnHand As Label
    Protected WithEvents lblErrOnHold As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblErrReceiveQty As Label
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblErrVehExp As Label
    Protected WithEvents lblErrConfirm As Label
    Protected WithEvents lblGoodsRcvId As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents txtGoodsRcvRefNo As TextBox
    Protected WithEvents lblStatus As Label
    Protected WithEvents txtGoodsRcvRefDate As TextBox
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents ddlSuppCode As DropDownList
    Protected WithEvents lblUpdateDate As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents ddlPOId As DropDownList
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents ddlItemCode As DropDownList
    Protected WithEvents lblGoodsRcvLnId As Label
    Protected WithEvents lblSelectedItemCode As Label
    Protected WithEvents lblItemLocCode As Label
    Protected WithEvents lblItemCode As Label
    Protected WithEvents lblQtyOrder As Label
    Protected WithEvents lblPurchaseUOM As Label
    Protected WithEvents lblUOMCode As Label
    Protected WithEvents lblQtyOutStanding As Label
    Protected WithEvents txtReceiveQty As TextBox
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents ddlVehExpCode As DropDownList
    Protected WithEvents lblLocCode As Label
    Protected WithEvents dgGRDet As DataGrid
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents lblVehicleOption As Label

    Protected WithEvents lblAccount As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehExpense As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblPleaseSelectOne As Label
    Protected WithEvents lblErrItemCode As Label
    Protected WithEvents lblErrReceiveQtyZero As Label
    Protected WithEvents lblFACode As Label
    Protected WithEvents lblErrFACode As Label
    Protected WithEvents ddlFACode As DropDownList

    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnConfirm As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnCancel As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnPrint As ImageButton
    Protected WithEvents GoodsRcvId As HtmlInputHidden
    Protected WithEvents tblLine As HtmlTable
    Protected WithEvents tblDoc As HtmlTable
    Protected WithEvents tblAcc As HtmlTable
    Protected WithEvents hidItemType As HtmlInputHidden
    Protected WithEvents FindAcc As HtmlInputButton
    Protected WithEvents tblDoc1 As HtmlTable
    Protected WithEvents tblFACode As HtmlTable
    Protected WithEvents btnSelDate As Image
    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlk As HtmlTableRow
    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents lblPreBlockErr As Label
    Protected WithEvents lblPreBlkTag As Label
    Protected WithEvents ddlPreBlock As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden

    Protected WithEvents lblErrGRRefNo As Label
    Protected WithEvents lblFA As Label
    Protected WithEvents lblIDQtyOutStanding As Label

    Protected WithEvents ddlInventoryBin As DropDownList
    Protected WithEvents lblInventoryBin As Label
    Protected WithEvents btnDispatch As ImageButton
    Protected WithEvents dgDA As DataGrid
    Protected WithEvents TransferVia As HtmlTableRow
    Protected WithEvents txtTransferVia As TextBox
    Protected WithEvents lblHidStatus As Label
    Protected WithEvents SaveDtlBtn As ImageButton
    Protected WithEvents lblTxLnID As Label
    Protected WithEvents lblHidStatusEdited As Label
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents txtCost As TextBox
    Protected WithEvents txtTtlCost As TextBox

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbViewJournal As LinkButton
    Protected WithEvents dgViewJournal As DataGrid
    Protected WithEvents lblTotalDB As Label
    Protected WithEvents lblTotalCR As Label
    Protected WithEvents lblTotalViewJournal As Label

    Protected WithEvents btnAddAllItem As ImageButton
    Protected WithEvents btnIssue As ImageButton
    Protected WithEvents tblDA As HtmlTableRow
    Protected WithEvents tblSI As HtmlTableRow
    Protected WithEvents dgSI As DataGrid

    Dim PreBlockTag As String
    Dim BlockTag As String

    Protected objPU As New agri.PU.clsTrx()
    Protected objFA As New agri.FA.clsSetup()
    Protected objFATrx As New agri.FA.clsTrx()
    Protected objPUSetup As New agri.PU.clsSetup()
    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objAdmin As New agri.Admin.clsUOM()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdminSetup As New agri.Admin.clsLoc()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objAdminShare As New agri.Admin.clsShare()
    

    Dim objGRLnDs As New Dataset()
    Dim objAccDs As New Dataset()
    Dim objBlkDs As New Dataset()
    Dim objVehDs As New Dataset()
    Dim objVehExpDs As New Dataset()
    Dim objLangCapDs As New Dataset()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPUAR As Integer
    Dim intFAAR As Integer
    Dim intModule As Integer
    Dim intConfigSetting As Integer

    Dim strSelectedGoodsRcvId As String
    Dim strSelectedSuppCode As String
    Dim strSelectedPOId As String
    Dim strSelectedPOLnId As String
    Dim strItemType As String
    Dim strAcceptFormat As String
    Dim ErrorChk As Integer
    Dim POLineID As String
    Dim strDAID As String
    Dim strSIID As String

    Const ITEM_PART_SEPERATOR As String = " @ "
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
        intFAAR = Session("SS_FAAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        intModule = Session("SS_MODULEACTIVATE")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReceive), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            tblFACode.Visible = False
            lblDate.Visible = False
            lblErrReceiveQty.Visible = False
            lblErrConfirm.Visible = False
            lblErrAccount.Visible = False
            lblPreBlockErr.Visible = False
            lblErrBlock.Visible = False
            lblErrVehicle.Visible = False
            lblErrVehExp.Visible = False
            lblErrItemCode.Visible = False
            lblErrReceiveQtyZero.Visible = False
            lblErrGRRefNo.Visible = False
            btnDelete.Visible = False
            btnUnDelete.Visible = False
            btnCancel.Visible = False
            btnConfirm.Visible = False
            lblDate.Visible = False
            btnDispatch.Visible = False
            lblInventoryBin.Visible = False
            lblFmt.Visible = False
            btnIssue.Visible = False

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            btnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnNew).ToString())
            btnSave.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnSave).ToString())
            btnConfirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnConfirm).ToString())
            btnPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnPrint).ToString())
            btnUnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnUnDelete).ToString())
            btnBack.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnBack).ToString())
            btnCancel.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnCancel).ToString())
            btnDispatch.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnDispatch).ToString())
            btnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnDelete).ToString())
            btnAddAllItem.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAddAllItem).ToString())
            btnIssue.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnIssue).ToString())

            strSelectedGoodsRcvId = Trim(IIf(Request.QueryString("GoodsRcvId") = "", Request.Form("GoodsRcvId"), Request.QueryString("GoodsRcvId")))
            onload_GetLangCap()

            If Not IsPostBack Then
                BindInventoryBinLevel("")
                BindChargeLevelDropDownList()
                If strSelectedGoodsRcvId <> "" Then
                    GoodsRcvId.Value = strSelectedGoodsRcvId
                    onLoad_Display(strSelectedGoodsRcvId)
                    onLoad_DisplayLn(strSelectedGoodsRcvId)
                    BindSupp(strSelectedGoodsRcvId)
                    BindPO(strSelectedSuppCode)

                    If ddlPOId.SelectedIndex <> -1 Then
                        BindPOItem(strSelectedPOId)
                    End If
                Else
                    BindSupp("")
                    BindPO(strSelectedSuppCode)
                    If ddlPOId.SelectedIndex <> -1 Then
                        BindPOItem(strSelectedPOId)
                    End If
                    txtGoodsRcvRefDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now) 'Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                    TrLink.Visible = False
                End If

                lblHidStatusEdited.Text = "0"
                BindAccount("")
                BindPreBlock("", "")
                BindBlock("", "")
                BindVehicle("", "")
                BindVehicleExpense(True, "")
            End If
        End If
        'btnCancel.Visible = False
        lblFA.Text = "Fixed Asset Code :"
        lblFA.visible = false 
        ddlFACode.visible = false
    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.SelectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub
    
    Sub ddlChargeLevel_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        ToggleChargeLevel()
    End Sub
    
    Sub ToggleChargeLevel()
        If ddlChargeLevel.selectedIndex = 0 Then
            RowBlk.Visible = False
            RowPreBlk.Visible = True
            hidBlockCharge.value = "yes"
        Else
            RowBlk.Visible = True
            RowPreBlk.Visible = False
            hidBlockCharge.value = ""
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GRDET_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=pu/trx/PU_trx_GRList.aspx")
        End Try

        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
        lblLocCode.Text = GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text

        lblErrAccount.Text = "<BR>" & lblPleaseSelectOne.Text & lblAccount.Text
        lblErrBlock.Text = lblPleaseSelectOne.Text & lblBlock.Text
        lblErrVehicle.Text = lblPleaseSelectOne.Text & lblVehicle.Text
        lblErrVehExp.Text = lblPleaseSelectOne.Text & lblVehExpense.Text

        dgGRDet.Columns(5).HeaderText = lblLocCode.Text
        dgGRDet.Columns(7).HeaderText = lblAccount.Text
        dgGRDet.Columns(8).HeaderText = lblBlock.Text
        dgGRDet.Columns(9).HeaderText = lblVehicle.Text & "<BR>" & lblVehExpense.Text
        'dgGRDet.Columns(10).HeaderText = lblVehExpense.Text
        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTag.Text = PreBlockTag & lblCode.Text & " : "
        lblPreBlockErr.Text = lblPleaseSelectOne.Text & PreBlockTag & lblCode.Text
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GRDET_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=pu/trx/PU_trx_GRList.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = objLangCapDs.Tables(0).Rows(count).Item("TermCode").trim() Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    Sub onLoad_Display(ByVal pv_strGoodsRcvId As String)
        Dim strOpCd As String = "PU_CLSTRX_GR_GET"
        Dim objGRDs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String = pv_strGoodsRcvId & "|" & strLocation & "||||||A.GoodsRcvId||"
        Dim intCnt As Integer = 0
        Dim POType As String
        Dim blnDisplayCancel As Boolean

        Try
            intErrNo = objPU.mtdGetGR(strCompany, _
                                      strLocation, _
                                      strUserId, _
                                      "", _
                                      "", _
                                      strOpCd, _
                                      strParam, _
                                      objGRDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

        If objGRDs.Tables(0).Rows.Count > 0 Then
            lblGoodsRcvId.Text = pv_strGoodsRcvId
            lblAccPeriod.Text = objGRDs.Tables(0).Rows(0).Item("AccMonth") & "/" & objGRDs.Tables(0).Rows(0).Item("AccYear")
            txtGoodsRcvRefNo.Text = objGRDs.Tables(0).Rows(0).Item("GoodsRcvRefNo").Trim()
            lblStatus.Text = objPU.mtdGetGRStatus(objGRDs.Tables(0).Rows(0).Item("Status"))
            lblHidStatus.Text = Trim(objGRDs.Tables(0).Rows(0).Item("Status"))
            txtGoodsRcvRefDate.Text = Date_Validation(objGRDs.Tables(0).Rows(0).Item("GoodsRcvRefDate"), True)
            lblCreateDate.Text = objGlobal.GetLongDate(objGRDs.Tables(0).Rows(0).Item("CreateDate"))
            strSelectedSuppCode = objGRDs.Tables(0).Rows(0).Item("SupplierCode").Trim()
            lblUpdateDate.Text = objGlobal.GetLongDate(objGRDs.Tables(0).Rows(0).Item("UpdateDate"))
            strSelectedPOId = objGRDs.Tables(0).Rows(0).Item("POID").Trim()
            lblUpdateBy.Text = objGRDs.Tables(0).Rows(0).Item("UserName")
            txtRemark.Text = objGRDs.Tables(0).Rows(0).Item("Remark").Trim()
            BindInventoryBinLevel(Trim(objGRDs.Tables(0).Rows(0).Item("Bin")))
            POType = objGRDs.Tables(0).Rows(0).Item("POType").Trim()
            strSelectedGoodsRcvId = lblGoodsRcvId.Text

            If strLocation <> Mid(Trim(strSelectedPOId), 9, 3) Then
                onLoad_DisplayDA(Trim(lblGoodsRcvId.Text))
            ElseIf strLocation = Mid(Trim(strSelectedPOId), 9, 3) Then
                onLoad_DisplaySI(Trim(lblGoodsRcvId.Text))
            End If

            Select Case Trim(objGRDs.Tables(0).Rows(0).Item("Status"))
                Case objPU.EnumGRStatus.Active
                    tblLine.Visible = True
                    tblDoc.Visible = True
                    tblDoc1.Visible = True

                    If POType = objPU.EnumPOType.FixedAsset And objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.FixAsset), intModule) = True Then
                        tblFACode.Visible = True
                        BindFACode("")
                    End If

                    btnSelDate.Visible = True
                    txtGoodsRcvRefNo.Enabled = True
                    txtGoodsRcvRefDate.Enabled = True
                    ddlSuppCode.Enabled = True
                    ddlPOId.Enabled = True
                    btnSave.Visible = True
                    btnConfirm.Visible = True
                    btnDelete.Visible = True
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    btnUnDelete.Visible = False
                    btnCancel.Visible = False
                    txtRemark.Enabled = True
                    ddlInventoryBin.Enabled = True

                    If NoLineAdded(pv_strGoodsRcvId) = True Then
                        ddlPOId.Enabled = True
                    Else
                        ddlPOId.Enabled = False
                    End If


                Case objPU.EnumGRStatus.Confirmed
                    Try
                        intErrNo = objPU.mtdGetDocStatusAction(strCompany, _
                                                               strLocation, _
                                                               strUserId, _
                                                               strAccMonth, _
                                                               strAccYear, _
                                                               False, True, False, _
                                                               pv_strGoodsRcvId, _
                                                               blnDisplayCancel)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GR_GET_GR_CANCEL_BUTTON&errmesg=" & Exp.ToString() & "&redirect=PU/trx/pu_trx_GRList.aspx")
                    End Try


                    If blnDisplayCancel = True Then
                        btnCancel.Visible = True
                        btnCancel.Attributes("onclick") = "javascript:return ConfirmAction('cancel');"
                    Else
                        btnCancel.Visible = False
                    End If

                    tblLine.Visible = False
                    tblDoc.Visible = False
                    tblDoc1.Visible = False
                    tblFACode.Visible = False
                    tblAcc.Visible = False
                    btnSelDate.Visible = False
                    txtGoodsRcvRefNo.Enabled = False
                    txtGoodsRcvRefDate.Enabled = False
                    ddlSuppCode.Enabled = False
                    ddlPOId.Enabled = False
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                    btnUnDelete.Visible = False
                    txtRemark.Enabled = False
                    ddlInventoryBin.Enabled = False

                    If POType <> objPU.EnumPOType.DirectCharge Then
                        'Select Case strCompany
                        '    Case "SAM", "MIL"
                        '        If ddlInventoryBin.SelectedItem.Value = objINSetup.EnumInventoryBinLevel.HO Then
                        '            btnDispatch.Visible = True
                        '            btnDispatch.Attributes("onclick") = "javascript:return ConfirmAction('generate dispatch');"
                        '        End If
                        '    Case Else
                        '        If strLocation <> Mid(Trim(strSelectedPOId), 9, 3) Then
                        '            If strDAID = "" Then
                        '                TransferVia.Visible = True
                        '                btnDispatch.Visible = True
                        '                btnDispatch.Attributes("onclick") = "javascript:return ConfirmAction('generate dispatch');"
                        '            End If
                        '        End If
                        'End Select
                        If strLocation <> Mid(Trim(strSelectedPOId), 9, 3) Then
                            If strDAID = "" Then
                                TransferVia.Visible = True
                                btnDispatch.Visible = True
                                btnDispatch.Attributes("onclick") = "javascript:return ConfirmAction('generate dispatch');"
                            End If
                        ElseIf strLocation = Mid(Trim(strSelectedPOId), 9, 3) Then
                            btnIssue.Visible = True
                            btnIssue.Attributes("onclick") = "javascript:return ConfirmAction('generate stock issue');"
                        End If

                    ElseIf POType = objPU.EnumPOType.DirectCharge Then
                        'Select Case strCompany
                        '    Case "SAM", "MIL"
                        '        If ddlInventoryBin.SelectedItem.Value = objINSetup.EnumInventoryBinLevel.HO Then
                        '            btnDispatch.Visible = True
                        '            btnDispatch.Attributes("onclick") = "javascript:return ConfirmAction('generate dispatch');"
                        '        End If
                        '    Case Else
                        '        'special for SPK ... cannot include in dispatch monthend
                        '        If strLocation <> Mid(Trim(strSelectedPOId), 9, 3) Then
                        '            If strDAID = "" Then
                        '                TransferVia.Visible = True
                        '                btnDispatch.Visible = True
                        '                btnDispatch.Attributes("onclick") = "javascript:return ConfirmAction('generate dispatch');"
                        '            End If
                        '        End If
                        'End Select

                        'special for SPK ... cannot include in dispatch monthend
                        If strLocation <> Mid(Trim(strSelectedPOId), 9, 3) Then
                            If strDAID = "" Then
                                TransferVia.Visible = True
                                btnDispatch.Visible = True
                                btnDispatch.Attributes("onclick") = "javascript:return ConfirmAction('generate dispatch');"
                            End If
                            'ElseIf strLocation = Mid(Trim(strSelectedPOId), 9, 3) Then
                            '    btnIssue.Visible = True
                            '    btnIssue.Attributes("onclick") = "javascript:return ConfirmAction('generate stock issue');"
                        End If
                    End If

                Case objPU.EnumGRStatus.Deleted
                    tblLine.Visible = False
                    tblDoc.Visible = False
                    tblDoc1.Visible = False
                    tblFACode.Visible = False
                    tblAcc.Visible = False
                    btnSelDate.Visible = False
                    txtGoodsRcvRefNo.Enabled = False
                    txtGoodsRcvRefDate.Enabled = False
                    ddlSuppCode.Enabled = False
                    ddlPOId.Enabled = False
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                    btnUnDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                    btnUnDelete.Visible = True
                    btnCancel.Visible = False
                    txtRemark.Enabled = False
                    ddlInventoryBin.Enabled = False

                Case objPU.EnumGRStatus.Cancelled
                    tblLine.Visible = False
                    tblDoc.Visible = False
                    tblDoc1.Visible = False
                    tblFACode.Visible = False
                    tblAcc.Visible = False
                    btnSelDate.Visible = False
                    txtGoodsRcvRefNo.Enabled = False
                    txtGoodsRcvRefDate.Enabled = False
                    ddlSuppCode.Enabled = False
                    ddlPOId.Enabled = False
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                    btnUnDelete.Visible = False
                    btnCancel.Visible = False
                    txtRemark.Enabled = False
                    ddlInventoryBin.Enabled = False

                Case Else
                    btnAdd.Visible = False
                    ddlInventoryBin.Enabled = False
                    tblLine.Visible = False
                    tblDoc.Visible = False
                    tblDoc1.Visible = False
                    tblFACode.Visible = False
                    tblAcc.Visible = False
                    btnSelDate.Visible = False
                    txtGoodsRcvRefNo.Enabled = False
                    txtGoodsRcvRefDate.Enabled = False
                    ddlSuppCode.Enabled = False
                    ddlPOId.Enabled = False
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                    btnUnDelete.Visible = False
                    txtRemark.Enabled = False
                    ddlInventoryBin.Enabled = False
            End Select

            If POType <> objPU.EnumPOType.DirectCharge Then
                If Trim(objGRDs.Tables(0).Rows(0).Item("Status")) = objPU.EnumGRStatus.Confirmed Or Trim(objGRDs.Tables(0).Rows(0).Item("Status")) = objPU.EnumGRStatus.Closed Then
                    btnPrint.Visible = True
                Else
                    btnPrint.Visible = False
                End If
            Else
                btnPrint.Visible = True
            End If
        End If
    End Sub

    Sub onLoad_DisplayLn(ByVal pv_strGoodsRcvId As String)
        Dim strOpCd As String = "PU_CLSTRX_GR_LINE_GET"
        Dim strParam As String = pv_strGoodsRcvId & "|"
        Dim UpdButton As LinkButton
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intDummy As Integer = 0

        'strAccYear = Year(Now())
        'strAccMonth = Month(Now())
        Try
            intErrNo = objPU.mtdGetGRLn(strOpCd, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strParam, _
                                        objGRLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_GRLn&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

        'If objGRLnDs.Tables(0).Rows.count > 0 Then
        ddlSuppCode.Enabled = False

        For intCnt = 0 To objGRLnDs.Tables(0).Rows.Count - 1
            objGRLnDs.Tables(0).Rows(intCnt).Item("GoodsRcvLnId") = objGRLnDs.Tables(0).Rows(intCnt).Item("GoodsRcvLnId").Trim()
            objGRLnDs.Tables(0).Rows(intCnt).Item("GoodsRcvId") = objGRLnDs.Tables(0).Rows(intCnt).Item("GoodsRcvId").Trim()
            objGRLnDs.Tables(0).Rows(intCnt).Item("POLnId") = objGRLnDs.Tables(0).Rows(intCnt).Item("POLnId").Trim()
            objGRLnDs.Tables(0).Rows(intCnt).Item("LocCode") = IIf(objGRLnDs.Tables(0).Rows(intCnt).Item("PRLocCode").Trim() <> "", _
                                                            objGRLnDs.Tables(0).Rows(intCnt).Item("PRLocCode").Trim(), _
                                                            objGRLnDs.Tables(0).Rows(intCnt).Item("PRRefLocCode").Trim())
            objGRLnDs.Tables(0).Rows(intCnt).Item("ItemCode") = objGRLnDs.Tables(0).Rows(intCnt).Item("ItemCode").Trim()
            objGRLnDs.Tables(0).Rows(intCnt).Item("Description") = objGRLnDs.Tables(0).Rows(intCnt).Item("ItemCode").Trim() & " (" & _
                                                                objGRLnDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            objGRLnDs.Tables(0).Rows(intCnt).Item("ReceiveUOM") = objGRLnDs.Tables(0).Rows(intCnt).Item("ReceiveUOM").Trim()
            objGRLnDs.Tables(0).Rows(intCnt).Item("ReceiveQty") = objGRLnDs.Tables(0).Rows(intCnt).Item("ReceiveQty")
            objGRLnDs.Tables(0).Rows(intCnt).Item("StockUOM") = objGRLnDs.Tables(0).Rows(intCnt).Item("StockUOM").Trim()
            objGRLnDs.Tables(0).Rows(intCnt).Item("StockQty") = objGRLnDs.Tables(0).Rows(intCnt).Item("StockQty")
            objGRLnDs.Tables(0).Rows(intCnt).Item("AccCode") = objGRLnDs.Tables(0).Rows(intCnt).Item("AccCode").Trim()
            objGRLnDs.Tables(0).Rows(intCnt).Item("BlkCode") = objGRLnDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            objGRLnDs.Tables(0).Rows(intCnt).Item("VehCode") = objGRLnDs.Tables(0).Rows(intCnt).Item("VehCode").Trim()
            objGRLnDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = objGRLnDs.Tables(0).Rows(intCnt).Item("VehExpenseCode").Trim()

            If UCase(Trim(objGRLnDs.Tables(0).Rows(intCnt).Item("AccCode"))) = "DUMMY" Then
                intDummy = intDummy + 1
            End If
        Next intCnt


        dgGRDet.DataSource = objGRLnDs.Tables(0)
        dgGRDet.DataBind()

        For intCnt = 0 To objGRLnDs.Tables(0).Rows.Count - 1
            Select Case Trim(lblStatus.Text)
                Case objPU.mtdGetGRStatus(objPU.EnumGRStatus.Active)
                    If intDummy > 0 Then
                        btnConfirm.Visible = False
                    Else
                        btnConfirm.Visible = True
                    End If
                Case objPU.mtdGetGRStatus(objPU.EnumGRStatus.Confirmed)
                    UpdButton = dgGRDet.Items.Item(intCnt).FindControl("lbDelete")
                    UpdButton.Visible = False
                    UpdButton = dgGRDet.Items.Item(intCnt).FindControl("Edit")
                    UpdButton.Visible = True
                Case objPU.mtdGetGRStatus(objPU.EnumGRStatus.Deleted), objPU.mtdGetGRStatus(objPU.EnumGRStatus.Cancelled), objPU.mtdGetGRStatus(objPU.EnumGRStatus.Closed)
                    UpdButton = dgGRDet.Items.Item(intCnt).FindControl("lbDelete")
                    UpdButton.Visible = False
                    UpdButton = dgGRDet.Items.Item(intCnt).FindControl("Edit")
                    UpdButton.Visible = False
                Case Else
                    UpdButton = dgGRDet.Items.Item(intCnt).FindControl("lbDelete")
                    UpdButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            End Select
        Next intCnt
        'End If

        If objGRLnDs.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
            If Trim(lblStatus.Text) = objPU.mtdGetGRStatus(objPU.EnumGRStatus.Active) Then
                btnConfirm.Visible = True
            End If
        Else
            TrLink.Visible = False
            If Trim(lblStatus.Text) = objPU.mtdGetGRStatus(objPU.EnumGRStatus.Active) Then
                btnConfirm.Visible = False
            End If
        End If
    End Sub

    Sub BindSupp(ByVal pv_strGoodsRcvId As String)
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim objSuppDs As New Object()
        Dim strSuppCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim strSuppType As String = objPUSetup.EnumSupplierType.Internal & "','" & objPUSetup.EnumSupplierType.External & "','" & objPUSetup.EnumSupplierType.Associate & "','" & objPUSetup.EnumSupplierType.Contractor

        strSuppCode = IIf(pv_strGoodsRcvId = "", "", strSelectedSuppCode)
        strParam = strSuppCode & "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||SELECT"
        strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")
        strParam = strParam & "|" & strSuppType

        Try
            intErrNo = objPUSetup.mtdGetSupplier(strOpCd, strParam, objSuppDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_SUPPLIER&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

        For intCnt = 0 To objSuppDs.Tables(0).Rows.Count - 1
            objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode").Trim()
            objSuppDs.Tables(0).Rows(intCnt).Item("Name") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") & " (" & objSuppDs.Tables(0).Rows(intCnt).Item("Name").Trim() & ")"

            If objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = strSelectedSuppCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = objSuppDs.Tables(0).NewRow()
        dr("SupplierCode") = ""
        dr("Name") = "Please select Supplier Code"
        objSuppDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSuppCode.DataSource = objSuppDs.Tables(0)
        ddlSuppCode.DataValueField = "SupplierCode"
        ddlSuppCode.DataTextField = "Name"
        ddlSuppCode.DataBind()
        ddlSuppCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub SuppIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        BindPO(ddlSuppCode.SelectedItem.Value)
        If ddlPOId.SelectedIndex <> -1 Then
            BindPOItem(ddlPOId.SelectedItem.Value)
        End If
    End Sub

    Sub BindPO(ByVal pv_strSuppCode As String)
        Dim strOpCd As String 
        If strSelectedPOId <> "" Then
            strOpCd = "PU_CLSTRX_PO_GET"
        Else
            strOpCd = "PU_CLSTRX_PO_GET_NEW"
        End If

        Dim objPODs As New Object()
        Dim strParam As String
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        If strSelectedPOId <> "" Then
            strParam = "|" & strLocation & "||" & ddlSuppCode.SelectedItem.Value & "||" & objPU.EnumPOStatus.Deleted & "','" & objPU.EnumPOStatus.Cancelled & "','" & objPU.EnumPOStatus.Confirmed & "','" & objPU.EnumPOStatus.Invoiced & "','" & objPU.EnumPOStatus.Closed & "||||||"
            Try
                intErrNo = objPU.mtdGetPO(strOpCd, strParam, objPODs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End Try
        Else
            strParamName = "STRSEARCH"
            strParamValue = "AND B.LocCode = '" & strLocation & "' AND (B.SupplierCode LIKE '" & ddlSuppCode.SelectedItem.Value & "' OR B.Name LIKE '%" & ddlSuppCode.SelectedItem.Value & "%') AND B.Status IN ('" & objPU.EnumPOStatus.Confirmed & "','" & objPU.EnumPOStatus.Invoiced & "')  "

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objPODs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End Try
        End If
        
        For intCnt = 0 To objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("POId") = objPODs.Tables(0).Rows(intCnt).Item("POId").Trim()
            objPODs.Tables(0).Rows(intCnt).Item("DispPOId") = objPODs.Tables(0).Rows(intCnt).Item("POId").Trim() & "," & objPODs.Tables(0).Rows(intCnt).Item("Remark").Trim()

            If objPODs.Tables(0).Rows(intCnt).Item("POId") = strSelectedPOId Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        If Trim(ddlSuppCode.SelectedItem.Value) = "" Then
            objPODs.Tables(0).Clear()
            intSelectedIndex = 0
        End If

        dr = objPODs.Tables(0).NewRow()
        dr("POId") = ""
        dr("DispPOId") = "Please select Purchase Order"
        objPODs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPOId.DataSource = objPODs.Tables(0)
        ddlPOId.DataValueField = "POId"
        ddlPOId.DataTextField = "DispPOId"
        ddlPOId.DataBind()
        ddlPOId.SelectedIndex = intSelectedIndex

        If ddlPOId.SelectedIndex <> -1 Then
            strSelectedPOId = ddlPOId.SelectedItem.Value
        End If

    End Sub

    Sub POIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim POType As String

        strSelectedPOId = ddlPOId.SelectedItem.Value

        If ddlPOId.SelectedItem.Text <> "Please select Purchase Order" Then
            POType = GetPOType(ddlPOId.SelectedItem.Value)
        End if

        If POType = objPU.EnumPOType.FixedAsset And (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.FixAsset), intModule) = True) Then
            tblFACode.Visible = True
            BindFACode("")
        End If

        If ddlPOId.SelectedIndex <> -1 Then
            BindPOItem(ddlPOId.SelectedItem.Value)
        End If

    End Sub

    Sub BindFACode(ByVal pv_strItemCode As String)
        Dim strOpCd As String = "PU_CLSTRX_GR_FACODE_GET"
        Dim objFADs As New Object()
        Dim strParam As String
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParam = "|" & strLocation & "|" & objFA.EnumAssetAddPerm.Yes & "|" & objFA.EnumAssetItemStatus.Active & "|"

        Try
            intErrNo = objPU.mtdGetFACode(strOpCd, strParam, objFADs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_FACODE&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

        For intCnt = 0 To objFADs.Tables(0).Rows.Count - 1
            objFADs.Tables(0).Rows(intCnt).Item("AssetCode") = objFADs.Tables(0).Rows(intCnt).Item("AssetCode").Trim()

            If objFADs.Tables(0).Rows(intCnt).Item("AssetCode") = strSelectedPOId Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt


        dr = objFADs.Tables(0).NewRow()
        dr("AssetCode") = "Please select Fixed Asset Code"
        objFADs.Tables(0).Rows.InsertAt(dr, 0)

        ddlFACode.DataSource = objFADs.Tables(0)
        ddlFACode.DataValueField = "AssetCode"
        ddlFACode.DataTextField = "AssetCode"
        ddlFACode.DataBind()
        ddlFACode.SelectedIndex = intSelectedIndex
        If ddlFACode.SelectedIndex <> -1 Then
            strSelectedPOId = ddlFACode.SelectedItem.Value
        End If
    End Sub

    Function GetPOType(ByVal strSelectedPOId As String) As String
        Dim strOpCd_GRPOType As String = "PU_CLSTRX_GR_POTYPE_GET"
        Dim objPOType As New Object()
        Dim intErrNo As Integer
        Dim POType As String

        Try
            intErrNo = objPU.mtdGetPOType(strOpCd_GRPOType, strSelectedPOId, objPOType)
            POType = objPOType.Tables(0).Rows(0).Item("POType").Trim()
            Return POType
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_POType&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

    End Function

    Function GetItemType(ByVal strSelectedItemCode As String) As String
        Dim strOpCd_ItemType As String = "PU_CLSTRX_GR_ITEMTYPE_GET"
        Dim objItemType As New Object()
        Dim intErrNo As Integer
        Dim ItemType As String

        Try
            intErrNo = objPU.mtdGetItemType(strOpCd_ItemType, strSelectedItemCode, objItemType)
            ItemType = objItemType.Tables(0).Rows(0).Item("ItemType").Trim()
            Return ItemType
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_ItemType&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

    End Function

    Sub BindPOItem(ByVal pv_strPOId As String)
        Dim strOpCd As String
        Dim objPOItemDs As New Object()
        Dim strParam As String = lblGoodsRcvId.Text & "|" & pv_strPOId & "|" & objPU.EnumPOLnStatus.Active & "','" & objPU.EnumPOLnStatus.Edited
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim POType As String

        'If lblGoodsRcvId.Text = "" Then
        '    strOpCd = "PU_CLSTRX_PO_LINE_ITEMPART_GET" 
        'Else
        '    strOpCd = "PU_CLSTRX_GR_PO_LINE_ITEMPART_GET" 
        'End If
        strOpCd = "PU_CLSTRX_GR_PO_LINE_ITEMPART_GET"
        strParamName = "STRSEARCH|STRSEARCH1"

        If lblGoodsRcvId.Text = "" Then
            strParamValue = "AND A.POId = '" & Trim(pv_strPOId) & "' AND A.Status in ('" & objPU.EnumPOLnStatus.Active & "','" & objPU.EnumPOLnStatus.Edited & "') " & "|"
        Else
            POType = GetPOType(ddlPOId.SelectedItem.Value)

            If POType = objPU.EnumPOType.DirectCharge Then
                strParamValue = "AND A.POId = '" & Trim(pv_strPOId) & "' AND A.Status in ('" & objPU.EnumPOLnStatus.Active & "','" & objPU.EnumPOLnStatus.Edited & "') " & _
                        "|" '& "AND rtrim(A.POLnID)+rtrim(A.ItemCode) Not in (Select rtrim(POLnID)+rtrim(ItemCode) From PU_GoodsRcvln Where GoodsRcvID = '" & Trim(lblGoodsRcvId.Text) & "')"
            Else
                strParamValue = "AND A.POId = '" & Trim(pv_strPOId) & "' AND A.Status in ('" & objPU.EnumPOLnStatus.Active & "','" & objPU.EnumPOLnStatus.Edited & "') " & _
                        "|" & "AND rtrim(A.POLnID)+rtrim(A.ItemCode) Not in (Select rtrim(POLnID)+rtrim(ItemCode) From PU_GoodsRcvln Where GoodsRcvID = '" & Trim(lblGoodsRcvId.Text) & "')"
            End If
            
        End If


        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objPOItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_POItem&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try


        'Try
        '    intErrNo = objPU.mtdGetGRPOLn(strOpCd, _
        '                                  strCompany, _
        '                                  strLocation, _
        '                                  strUserId, _
        '                                  strAccMonth, _
        '                                  strAccYear, _
        '                                  strParam, _
        '                                  objPOItemDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_POItem&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        'End Try

        lblSelectedItemCode.Text = ""
        lblItemLocCode.Text = ""
        lblPurchaseUOM.Text = ""
        lblUOMCode.Text = ""
        lblQtyOrder.Text = ""
        lblQtyOutStanding.Text = ""
        txtReceiveQty.Text = 0
        lblIDQtyOutStanding.Text = ""
        txtCost.Text = 0
        txtTtlCost.Text = 0

        For intCnt = 0 To objPOItemDs.Tables(0).Rows.Count - 1

            If objPOItemDs.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then
                objPOItemDs.Tables(0).Rows(intCnt).Item("Description") = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                         objPOItemDs.Tables(0).Rows(intCnt).Item("PartNo") & " (" & _
                                                                         objPOItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                         IIf(objPOItemDs.Tables(0).Rows(intCnt).Item("PRID") <> "", objPOItemDs.Tables(0).Rows(intCnt).Item("PRID"), objPOItemDs.Tables(0).Rows(intCnt).Item("PRRefID")) & " ," & _
                                                                         IIf(objPOItemDs.Tables(0).Rows(intCnt).Item("PRLocCode") <> "", objPOItemDs.Tables(0).Rows(intCnt).Item("PRLocCode"), objPOItemDs.Tables(0).Rows(intCnt).Item("PRRefLocCode"))
                objPOItemDs.Tables(0).Rows(intCnt).Item("POLnID") = objPOItemDs.Tables(0).Rows(intCnt).Item("POLnID") & ITEM_PART_SEPERATOR & _
                                                                    objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                    objPOItemDs.Tables(0).Rows(intCnt).Item("PartNo")
            Else
                objPOItemDs.Tables(0).Rows(intCnt).Item("Description") = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                         objPOItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                         IIf(objPOItemDs.Tables(0).Rows(intCnt).Item("PRID") <> "", objPOItemDs.Tables(0).Rows(intCnt).Item("PRID"), objPOItemDs.Tables(0).Rows(intCnt).Item("PRRefID")) & " ," & _
                                                                         IIf(objPOItemDs.Tables(0).Rows(intCnt).Item("PRLocCode") <> "", objPOItemDs.Tables(0).Rows(intCnt).Item("PRLocCode"), objPOItemDs.Tables(0).Rows(intCnt).Item("PRRefLocCode"))
            End If


            If objPOItemDs.Tables(0).Rows(intCnt).Item("POLnId") = strSelectedPOLnId Then
                intSelectedIndex = intCnt + 1

                lblSelectedItemCode.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode")
                lblItemLocCode.Text = IIf(objPOItemDs.Tables(0).Rows(intCnt).Item("PRLocCode") <> "", _
                                    objPOItemDs.Tables(0).Rows(intCnt).Item("PRLocCode"), _
                                    objPOItemDs.Tables(0).Rows(intCnt).Item("PRRefLocCode"))
                lblPurchaseUOM.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")
                lblUOMCode.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("UOMCode")
                lblQtyOrder.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objPOItemDs.Tables(0).Rows(intCnt).Item("QtyOrder"), 5), 5)
                lblIDQtyOutStanding.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objPOItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding"), 5), 5)
                lblQtyOutStanding.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding")
                txtReceiveQty.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("ReceiveQty")
                strItemType = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemType")
                hidItemType.Value = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemType")
                txtCost.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("CostToDisplay")
                txtTtlCost.Text = txtReceiveQty.Text * txtCost.Text
            End If
        Next intCnt

        If ddlPOId.SelectedItem.Value.Trim = "" Then
            objPOItemDs.Tables(0).Clear()
        End If

        dr = objPOItemDs.Tables(0).NewRow()
        dr("POLnId") = ""
        dr("Description") = "Please select Item Code"
        objPOItemDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlItemCode.DataSource = objPOItemDs.Tables(0)
        ddlItemCode.DataValueField = "POLnId"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex

        If ddlItemCode.SelectedIndex = 0 Then
            tblAcc.Visible = False
        Else
            txtReceiveQty.Enabled = True
            If Trim(strItemType) = objINSetup.EnumInventoryItemType.DirectCharge Or Trim(strItemType) = objINSetup.EnumInventoryItemType.FixedAssetItem Then
                tblAcc.Visible = True
                FindAcc.Visible = True
            Else
                tblAcc.Visible = False
            End If
        End If
    End Sub

    Sub ItemIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim POType As String
        Dim strOpCd_GetCOA as string = "PU_CLSTRX_GR_COAITEM_GET"
        Dim strParam as string
        Dim objCOA as Object 
        Dim intErrNo as integer

        strSelectedSuppCode = Request.Form("ddlSuppCode")
        strSelectedPOId = Request.Form("ddlPOId")
        strSelectedPOLnId = Request.Form("ddlItemCode")

        POType = GetPOType(ddlPOId.SelectedItem.Value)


        If POType = objPU.EnumPOType.FixedAsset And objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.FixAsset), intModule) = True Then
            tblFACode.Visible = True
            BindFACode("")

            strParam =  "|" & " where a.polnid = '" & trim(ddlItemCode.SelectedItem.Value) & "' and a.poid = '" & trim(ddlPOId.SelectedItem.Value) & "' and itemtype = '6' "

            Try
            intErrNo = objINSetup.mtdGetMasterList(strOpCd_GetCOA, strParam, objINSetup.EnumInventoryMasterType.StockItem, objCOA)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_GET_COA&errmesg=" & Exp.ToString() & "&redirect=")
            End Try    

            If objCOA.Tables(0).Rows.Count > 0 Then
                BindAccount(objCOA.Tables(0).Rows(0).Item("actcode"))
                ddlAccount.Enabled = False
            End If
        ElseIf POType = objPU.EnumPOType.DirectCharge Then
            Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
            Dim objToLocCodeDs As New Object()
            Dim strToLocCode As String
            Dim intCnt As Integer = 0
            Dim dr As DataRow
            Dim intSelectedIndex As Integer

            strToLocCode = Mid(Trim(ddlPOId.SelectedItem.Value), 9, 3)
            If strLocation <> strToLocCode Then
                strParam = strToLocCode & "|" & objAdminSetup.EnumLocStatus.Active & "|LocCode|"
                Try
                    intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objToLocCodeDs, "")

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_dalist.aspx")
                End Try

                If objToLocCodeDs.Tables(0).Rows.Count > 0 Then
                    BindAccount(objToLocCodeDs.Tables(0).Rows(0).Item("AccCode"))
                End If
            Else
                BindAccount("DUMMY")
            End If
           
        End If


        BindPOItem(ddlPOId.SelectedItem.Value)
        ddlFACode.Visible = False
        lblFA.Visible = False
    End Sub

    Sub FACodeIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lblErrFACode.Text = ""
        tblFACode.Visible = True
    End Sub

    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = lblPleaseSelect.Text & lblAccount.Text & lblCode.Text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = objAccDs.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "_Description"
        ddlAccount.DataBind()
        ddlAccount.SelectedIndex = intSelectedIndex
        ddlAccount.AutoPostBack = True
    End Sub

    Sub onSelect_Account(ByVal Sender As Object, ByVal E As EventArgs)
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim intNurseryInd As Integer
        Dim strVehCode As String
        Dim strVehExpCode As String

        strVehCode = Request.Form("ddlVehCode")
        strVehExpCode = Request.Form("ddlVehExpCode")

        GetAccountDetails(ddlAccount.SelectedItem.Value, blnIsBalanceSheet, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers, intNurseryInd)

        If Not blnIsBalanceSheet Then
            If blnIsBlockRequire Then
                BindPreBlock(ddlAccount.SelectedItem.Value, ddlPreBlock.SelectedItem.Value)
                BindBlock(ddlAccount.SelectedItem.Value, ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            Else
                BindPreBlock("", ddlPreBlock.SelectedItem.Value)
                BindBlock("", ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            End If
            If blnIsVehicleRequire Then
                BindVehicle(ddlAccount.SelectedItem.Value, strVehCode)
                BindVehicleExpense(False, strVehExpCode)
            End If
            If blnIsOthers Then
                lblVehicleOption.Text = True
                BindVehicle("%", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(False, ddlVehExpCode.SelectedItem.Value)
            Else
                lblVehicleOption.Text = False
            End If
        Else
            If blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
                BindPreBlock(ddlAccount.SelectedItem.Value, ddlPreBlock.SelectedItem.Value)
                BindBlock(ddlAccount.SelectedItem.Value, ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            ElseIf blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.No Then
                BindPreBlock("", ddlPreBlock.SelectedItem.Value)
                BindBlock("", ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            End If
        End If
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_IsBalanceSheet As Boolean, _
                          ByRef pr_IsBlockRequire As Boolean, _
                          ByRef pr_IsVehicleRequire As Boolean, _
                          ByRef pr_IsOthers As Boolean, _
                          ByRef pr_strNurseryInd As Integer)

        Dim _objAccDs As New Object
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            pr_IsBalanceSheet = False
            pr_IsBlockRequire = False
            pr_IsVehicleRequire = False
            pr_IsOthers = False
            pr_strNurseryInd = objGLSetup.EnumNurseryAccount.No
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType")) = objGLSetup.EnumAccountType.BalanceSheet Then
                pr_IsBalanceSheet = True
                If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd")) = objGLSetup.EnumNurseryAccount.Yes Then
                    pr_strNurseryInd = objGLSetup.EnumNurseryAccount.Yes
                End If
            End If
            If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.NonVehicle Then
                pr_IsBlockRequire = True
            ElseIf Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.VehicleDistribution Then
                pr_IsVehicleRequire = True
            ElseIf Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.Others Then
                pr_IsBlockRequire = True
                pr_IsOthers = True
            End If
        End If
    End Sub

    Sub BindPreBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strOpCd = "GL_CLSSETUP_ACCOUNT_PREBLOCK_GET"
        intSelectedIndex = 0
        Try
            strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = pv_strBlkCode.Trim() Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        If objBlkDs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("_Description") = lblPleaseSelect.Text & PreBlockTag & lblCode.Text

        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlPreBlock.DataSource = objBlkDs.Tables(0)
        ddlPreBlock.DataValueField = "BlkCode"
        ddlPreBlock.DataTextField = "_Description"
        ddlPreBlock.DataBind()
        ddlPreBlock.SelectedIndex = intSelectedIndex

        If Not objBlkDs Is Nothing Then
            objBlkDs = Nothing
        End If
    End Sub

    Sub BindBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                strOpCd = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & objBlkDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = pv_strBlkCode.Trim() Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        If objBlkDs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("_Description") = lblPleaseSelect.Text & BlockTag & lblCode.Text
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "_Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindVehicle(ByVal pv_strAccCode As String, ByVal pv_strVehCode As String)
        Dim strOpCd As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"
            strParam = "|AccCode = '" & pv_strAccCode & "' AND LocCode = '" & Session("SS_LOCATION") & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'"
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   objVehDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objVehDs.Tables(0).Rows.Count - 1
            objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = objVehDs.Tables(0).Rows(intCnt).Item("VehCode").Trim()
            objVehDs.Tables(0).Rows(intCnt).Item("Description") = objVehDs.Tables(0).Rows(intCnt).Item("VehCode") & " (" & objVehDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = pv_strVehCode.Trim() Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objVehDs.Tables(0).NewRow()
        dr("VehCode") = ""
        dr("Description") = lblPleaseSelect.Text & lblVehicle.Text
        objVehDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehCode.DataSource = objVehDs.Tables(0)
        ddlVehCode.DataValueField = "VehCode"
        ddlVehCode.DataTextField = "Description"
        ddlVehCode.DataBind()
        ddlVehCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindVehicleExpense(ByVal pv_IsBlankList As Boolean, ByVal pv_strVehExpCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim strParam As String = "Order By VehExpenseCode ASC| And Veh.Status = '" & objGLSetup.EnumVehExpenseStatus.Active & "' "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            If pv_IsBlankList Then
                strParam += "And Veh.VehExpensecode = ''"
            End If
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.VehicleExpense, _
                                                   objVehExpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEHEXPENSE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objVehExpDs.Tables(0).Rows.Count - 1
            objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode").Trim()
            objVehExpDs.Tables(0).Rows(intCnt).Item("Description") = objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") & " (" & objVehExpDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = pv_strVehExpCode.Trim() Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objVehExpDs.Tables(0).NewRow()
        dr("VehExpenseCode") = ""
        dr("Description") = lblPleaseSelect.Text & lblVehExpense.Text
        objVehExpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehExpCode.DataSource = objVehExpDs.Tables(0)
        ddlVehExpCode.DataValueField = "VehExpenseCode"
        ddlVehExpCode.DataTextField = "Description"
        ddlVehExpCode.DataBind()
        ddlVehExpCode.SelectedIndex = intSelectedIndex
    End Sub

    Function CheckDate(ByVal pv_strDate As String, ByRef pr_strDate As String) As Boolean
        Dim strDateFormatCode As String = Session("SS_DATEFMT")
        Dim strDateFormat As String

        pr_strDate = ""
        CheckDate = True
        If Not pv_strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormatCode, pv_strDate, strDateFormat, pr_strDate) = False Then
                lblFmt.Text = strDateFormat
                pr_strDate = ""
                CheckDate = False
            End If
        End If
    End Function

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_GRList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strOpCd_GetUOM As String = "ADMIN_CLSUOM_CONVERTION_LIST_GET"
        Dim strOpCd_AddGRLn As String = "PU_CLSTRX_GR_LINE_ADD"
        Dim strOpCd_AddGR As String = "PU_CLSTRX_GR_ADD"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim objUOMDs As New Object
        Dim objGRId As Object
        Dim objGRLnId As Object
        Dim strGoodsRcvRefDate As String = txtGoodsRcvRefDate.Text
        Dim strReceiveQty As String = Trim(Request.Form("txtReceiveQty"))
        Dim strParamUOM As String = ""
        Dim strParam As String = ""
        Dim strParamLn As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strGRId As String
        Dim dblRate As Double
        Dim intErrorCheck As Integer
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim POType As String
        Dim strAssetAddStatus As String
        Dim strPOLnID As String = ""
        Dim arrPOLnID As Array
        Dim intNurseryInd As Integer
        Dim strPreBlk As String = Request.Form("ddlPreBlock")
        Dim strBlk As String = Request.Form("ddlBlock")
        Dim strNewIDFormat As String
        Dim strCost As String = IIf(Trim(txtCost.Text) = "", "0", Trim(txtCost.Text))
        Dim strDate As String = Date_Validation(txtGoodsRcvRefDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRcvRefDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        arrPOLnID = Split(ddlItemCode.SelectedItem.Value.Trim(), ITEM_PART_SEPERATOR)
        If UBound(arrPOLnID) <> -1 Then
            strPOLnID = arrPOLnID(0)
        End If

        strGRId = IIf(lblGoodsRcvId.Text = "", "", lblGoodsRcvId.Text)

        GetAccountDetails(ddlAccount.SelectedItem.Value, blnIsBalanceSheet, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers, intNurseryInd)

        If txtGoodsRcvRefDate.Text <> "" Then
            strGoodsRcvRefDate = Date_Validation(strGoodsRcvRefDate, False)
            If strGoodsRcvRefDate = "" Then
                lblDate.Visible = True
                lblDate.Text = lblDate.Text & strAcceptFormat
                Exit Sub
            End If
        End If

        If ddlItemCode.SelectedItem.Value.Trim() <> "" Then
            If Convert.ToDouble(strReceiveQty) < 0 Or (Convert.ToDouble(strReceiveQty) > Convert.ToDouble(lblQtyOutStanding.Text)) Then
                lblErrReceiveQty.Visible = True
                Exit Sub
            ElseIf CDbl(strReceiveQty) = 0 Then
                lblErrReceiveQtyZero.Visible = True
                Exit Sub
            ElseIf CDbl(strReceiveQty) > 0 Then
                If lblPurchaseUOM.Text.Trim() <> lblUOMCode.Text.Trim() Then
                    strParamUOM = lblPurchaseUOM.Text & "|" & _
                                  lblUOMCode.Text & "|" & _
                                  objAdmin.EnumUOMStatus.Active & "|A.UOMFrom"

                    Try
                        intErrNo = objAdmin.mtdGetUOMRate(strOpCd_GetUOM, strParamUOM, objUOMDs)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_UOMConvertion&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                    End Try
                    If objUOMDs.Tables(0).Rows.Count > 0 Then
                        dblRate = Convert.ToDouble(objUOMDs.Tables(0).Rows(0).Item("Rate"))
                    Else
                        strParamUOM = lblUOMCode.Text & "|" & _
                                    lblPurchaseUOM.Text & "|" & _
                                    objAdmin.EnumUOMStatus.Active & "|A.UOMFrom"

                        Try
                            intErrNo = objAdmin.mtdGetUOMRate(strOpCd_GetUOM, strParamUOM, objUOMDs)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_UOMConvertion&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                        End Try
                        If objUOMDs.Tables(0).Rows.Count > 0 Then
                            If Convert.ToDouble(objUOMDs.Tables(0).Rows(0).Item("Rate")) <> 0 Then
                                dblRate = 1.0 / Convert.ToDouble(objUOMDs.Tables(0).Rows(0).Item("Rate"))
                            Else
                                dblRate = 1
                            End If
                        Else
                            dblRate = 1
                        End If
                    End If
                Else
                    dblRate = 1
                End If


                If hidItemType.Value = objINSetup.EnumInventoryItemType.FixedAssetItem Then
                    If ddlAccount.SelectedIndex = 0 Then
                        lblErrAccount.Visible = True
                        Exit Sub
                    ElseIf (ddlBlock.Items.Count > 1) And (ddlBlock.SelectedItem.Value = "") Then
                        lblErrBlock.Visible = True
                        Exit Sub
                    ElseIf (ddlVehCode.Items.Count > 1) And (ddlVehCode.SelectedItem.Value = "") And (lblVehicleOption.Text = False) Then
                        lblErrVehicle.Visible = True
                        Exit Sub
                    End If
                End If

                If blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
                    If ddlChargeLevel.SelectedIndex = 0 Then
                        If strPreBlk = "" Then
                            lblPreBlockErr.Visible = True
                            Exit Sub
                        End If
                    Else
                        If strBlk = "" Then
                            lblErrBlock.Visible = True
                            Exit Sub
                        End If
                    End If
                End If

                If Not blnIsBalanceSheet Then
                    If hidItemType.Value = objINSetup.EnumInventoryItemType.DirectCharge Then
                        If ddlAccount.SelectedItem.Value = "" Then
                            lblErrAccount.Visible = True
                            Exit Sub
                        ElseIf ddlBlock.SelectedItem.Value = "" And ddlChargeLevel.SelectedIndex = 1 And blnIsBlockRequire = True Then
                            lblErrBlock.Visible = True
                            Exit Sub
                        ElseIf ddlPreBlock.SelectedItem.Value = "" And ddlChargeLevel.SelectedIndex = 0 And blnIsBlockRequire = True Then
                            lblPreBlockErr.Visible = True
                            Exit Sub
                        ElseIf ddlVehCode.SelectedItem.Value = "" And blnIsVehicleRequire = True Then
                            lblErrVehicle.Visible = True
                            Exit Sub
                        ElseIf ddlVehExpCode.SelectedItem.Value = "" And blnIsVehicleRequire = True Then
                            lblErrVehExp.Visible = True
                            Exit Sub
                        ElseIf ddlVehCode.SelectedItem.Value <> "" And ddlVehExpCode.SelectedItem.Value = "" And lblVehicleOption.Text = True Then
                            lblErrVehExp.Visible = True
                            Exit Sub
                        ElseIf ddlVehCode.SelectedItem.Value = "" And ddlVehExpCode.SelectedItem.Value <> "" And lblVehicleOption.Text = True Then
                            lblErrVehicle.Visible = True
                            Exit Sub
                        End If
                    End If
                End If

                'If Month(strDate) < strAccMonth And Year(strDate) <= strAccYear Then
                '    lblDate.Visible = True
                '    lblDate.Text = "Invalid transaction date."
                '    Exit Sub
                'End If

                POType = GetPOType(ddlPOId.SelectedItem.Value)

                strAccYear = Year(strDate)
                strAccMonth = Month(strDate)
                'Select Case strCompany
                '    Case "SAM", "MIL"
                '        strNewIDFormat = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReceive) & Right(strAccYear, 2) & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth)
                '    Case Else
                '        strNewIDFormat = "BTB" & "/" & strCompany & "/" & strLocation & "/" & Trim(POType) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
                'End Select
                strNewIDFormat = "BTB" & "/" & strCompany & "/" & strLocation & "/" & Trim(POType) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

                If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True And GetItemType(lblSelectedItemCode.Text) = objINSetup.EnumInventoryItemType.FixedAssetItem Then
                    If ddlFACode.SelectedItem.Value = "Please select Fixed Asset Code" Then
                        tblFACode.Visible = True
                        lblErrFACode.Visible = True
                        lblErrFACode.Text = "Please select Fixed Asset Code"
                        Exit Sub
                    Else
                        strParamLn = strGRId & "|" & _
                             strPOLnID & "|" & _
                             lblSelectedItemCode.Text & "|" & _
                             lblPurchaseUOM.Text & "|" & _
                             strReceiveQty & "|" & _
                             lblUOMCode.Text & "|" & _
                             dblRate & "|" & _
                             ddlAccount.SelectedItem.Value & "|" & _
                             IIf(ddlChargeLevel.SelectedIndex = 0, ddlPreBlock.SelectedItem.Value, ddlBlock.SelectedItem.Value) & "|" & _
                             ddlVehCode.SelectedItem.Value & "|" & _
                             ddlVehExpCode.SelectedItem.Value & "|" & _
                             lblItemLocCode.Text() & "|" & _
                             ddlFACode.SelectedItem.Value & "|" & _
                             strCost
                    End If
                Else
                    strParamLn = strGRId & "|" & _
                             strPOLnID & "|" & _
                             lblSelectedItemCode.Text & "|" & _
                             lblPurchaseUOM.Text & "|" & _
                             strReceiveQty & "|" & _
                             lblUOMCode.Text & "|" & _
                             dblRate & "|" & _
                             ddlAccount.SelectedItem.Value & "|" & _
                             IIf(ddlChargeLevel.SelectedIndex = 0, ddlPreBlock.SelectedItem.Value, ddlBlock.SelectedItem.Value) & "|" & _
                             ddlVehCode.SelectedItem.Value & "|" & _
                             ddlVehExpCode.SelectedItem.Value & "|" & _
                             lblItemLocCode.Text() & "|" & _
                             "|" & _
                             strCost
                End If
            End If
        Else
            lblErrItemCode.Visible = True
            Exit Sub
        End If

        If lblHidStatusEdited.Text = "0" Then
            strParam = strGRId & "|" & _
                       txtGoodsRcvRefNo.Text & "|" & _
                       strDate & "|" & _
                       ddlSuppCode.SelectedItem.Value & "|" & _
                       ddlPOId.SelectedItem.Value & "|" & _
                       txtRemark.Text & "|" & _
                       objPU.EnumGRStatus.Active & "|" & _
                       Trim(ddlInventoryBin.SelectedItem.Value) & "|" & _
                       strNewIDFormat

            Try
                If ddlChargeLevel.SelectedIndex = 0 And tblAcc.Visible = True And RowPreBlk.Visible = True Then
                    strParamList = Session("SS_LOCATION") & "|" & _
                                           ddlAccount.SelectedItem.Value.Trim() & "|" & _
                                           ddlPreBlock.SelectedItem.Value.Trim() & "|" & _
                                           objGLSetup.EnumBlockStatus.Active
                    intErrNo = objPU.mtdAddGRLnByBlock(strOpCodeGLSubBlkByBlk, _
                                                       strParamList, _
                                                       strOpCd_AddGRLn, _
                                                       strOpCd_AddGR, _
                                                       strOpCd_UpdPOLn, _
                                                       strOpCd_UpdItem, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strParam, _
                                                       strParamLn, _
                                                       intErrorCheck, _
                                                       strLocType, _
                                                       objGRId, _
                                                       objGRLnId, _
                                                       objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReceive), _
                                                       objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReceiveLn))
                Else
                    intErrNo = objPU.mtdAddGRLn(strOpCd_AddGRLn, _
                                                strOpCd_AddGR, _
                                                strOpCd_UpdPOLn, _
                                                strOpCd_UpdItem, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                strParamLn, _
                                                intErrorCheck, _
                                                objGRId, _
                                                objGRLnId, _
                                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReceive), _
                                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReceiveLn))
                End If
                Select Case intErrorCheck
                    Case -1
                        lblErrOnHand.Visible = True
                    Case -2
                        lblErrOnHold.Visible = True
                End Select
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_NEW_GRLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                End If
            End Try

            strSelectedGoodsRcvId = objGRId
        Else
            Dim strOpCode_ItemUpd As String = "PU_CLSTRX_GR_LINE_EDIT"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""

            strParamName = "RECEIVEQTY|STOCKQTY|RECEIVEUOM|STOCKUOM|" & _
                            "ACCCODE|BLKCODE|VEHCODE|VEHEXPENSECODE|FIXEDASSETCODE|" & _
                            "GOODSRCVID|GOODSRCVLNID|ITEMCODE"

            strParamValue = strReceiveQty & "|" & _
                        strReceiveQty * dblRate & "|" & _
                        lblPurchaseUOM.Text & "|" & _
                        lblUOMCode.Text & "|" & _
                        ddlAccount.SelectedItem.Value & "|" & _
                        IIf(ddlChargeLevel.SelectedIndex = 0, ddlPreBlock.SelectedItem.Value, ddlBlock.SelectedItem.Value) & "|" & _
                        ddlVehCode.SelectedItem.Value & "|" & _
                        ddlVehExpCode.SelectedItem.Value & "||" & _
                        lblGoodsRcvId.Text & "|" & _
                        lblTxLnID.Text & "|" & _
                        lblSelectedItemCode.Text

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_NEW_GRLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End Try

            strSelectedGoodsRcvId = lblGoodsRcvId.Text
            lblHidStatusEdited.Text = "0"
        End If

        lblGoodsRcvId.Text = strSelectedGoodsRcvId
        strSelectedSuppCode = ddlSuppCode.SelectedItem.Value
        strSelectedPOId = ddlPOId.SelectedItem.Value
        onLoad_Display(strSelectedGoodsRcvId)
        onLoad_DisplayLn(strSelectedGoodsRcvId)
        BindPO(strSelectedPOId)
        BindPOItem(strSelectedPOId)
        BindAccount("")
        BindPreBlock("", "")
        BindBlock("", "")
        BindVehicle("", "")
        BindVehicleExpense(True, "")
        ddlItemCode.Enabled = True

        btnAdd.Visible = True
        SaveDtlBtn.Visible = False
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_DelGRLn As String = "PU_CLSTRX_GR_LINE_DEL"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strParam As String = ""
        'Dim GoodsRcvLnIdCell As TableCell = E.Item.Cells(0)
        Dim POLnCell As TableCell = E.Item.Cells(1)
        Dim ItemCell As TableCell = E.Item.Cells(2)
        Dim ReceiveQtyCell As TableCell = E.Item.Cells(3)
        Dim StockQtyCell As TableCell = E.Item.Cells(4)
        Dim strGoodsRcvLnId As String '= GoodsRcvLnIdCell.Text
        Dim strPOLnIDCell As String = POLnCell.Text
        Dim strItemCode As String = ItemCell.Text
        Dim strReceiveQty As String = ReceiveQtyCell.Text
        Dim strStockQty As String = StockQtyCell.Text
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim lbl As Label
        Dim strDate As String = Date_Validation(txtGoodsRcvRefDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRcvRefDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        lbl = E.Item.FindControl("lblGRLnId")
        strGoodsRcvLnId = lbl.Text.Trim

        strParam = lblGoodsRcvId.Text & "|" & _
                   strPOLnIDCell & "|" & _
                   "|" & _
                   strItemCode & "|" & _
                   strReceiveQty & "|" & _
                   strStockQty & "|" & _
                   strGoodsRcvLnId

        Try
            intErrNo = objPU.mtdDelGRLn(strOpCd_DelGRLn, _
                                        strOpCd_GetPOLn, _
                                        strOpCd_UpdPOLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DEL_GRLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End If
        End Try

        strSelectedGoodsRcvId = lblGoodsRcvId.Text
        'strSelectedSuppCode = ddlSuppCode.SelectedItem.Value
        strSelectedPOId = ddlPOId.SelectedItem.Value
		onLoad_Display(strSelectedGoodsRcvId) 
        onLoad_DisplayLn(strSelectedGoodsRcvId)
        'BindPO(strSelectedPOId)
        BindPOItem(strSelectedPOId)
        'BindAccount("")
        'BindPreBlock("", "")
        'BindBlock("", "")
        'BindVehicle("", "")
        'BindVehicleExpense(True, "")
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddGR As String = "PU_CLSTRX_GR_ADD"
        Dim strOpCd_UpdGR As String = "PU_CLSTRX_GR_UPD"
        Dim objGRId As Object
        Dim strGoodsRcvRefNo As String = txtGoodsRcvRefNo.Text
        Dim strGoodsRcvRefDate As String = txtGoodsRcvRefDate.Text
        Dim strSuppCode As String = ddlSuppCode.SelectedItem.Value
        Dim strPOID As String = ddlPOId.SelectedItem.Value
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer

        Dim objGRChkRef As Object
        Dim intErrNoRef As Integer
        Dim strParamRef As String = ""
        Dim strOpCd_GRRefNo As String = "PU_CLSTRX_CHK_GR_REF_NO"
        Dim strNewIDFormat As String
        Dim POType As String
        Dim strDate As String = Date_Validation(txtGoodsRcvRefDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRcvRefDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If lblGoodsRcvId.Text = "" Then
            Exit Sub
        End If

        Dim arrParam As Array
        arrParam = Split(lblAccPeriod.Text, "/")
        If Month(strDate) <> arrParam(0) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        ElseIf Year(strDate) <> arrParam(1) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        POType = GetPOType(ddlPOId.SelectedItem.Value)

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strNewIDFormat = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReceive) & Right(strAccYear, 2) & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth)
        '    Case Else
        '        strNewIDFormat = "BTB" & "/" & strCompany & "/" & strLocation & "/" & Trim(POType) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        strNewIDFormat = "BTB" & "/" & strCompany & "/" & strLocation & "/" & Trim(POType) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        strParamRef = strGoodsRcvRefNo & "|" & _
                        strSuppCode & "|" & _
                    lblGoodsRcvId.Text

        Try
            intErrNoRef = objPU.mtdChkGRRefNo(strOpCd_GRRefNo, _
                                              strParamRef, _
                                              objGRChkRef)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CHK_GR_REF_NO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

        If objGRChkRef.Tables(0).Rows.Count = 0 OR strGoodsRcvRefNo = ""
            strParam = lblGoodsRcvId.Text & "|" & _
                       strGoodsRcvRefNo & "|" & _
                       strDate & "|" & _
                       strSuppCode & "|" & _
                       strPOID & "|" & _
                       strRemark & "|" & _
                       objPU.EnumGRStatus.Active & "|" & _
                       Trim(ddlInventoryBin.SelectedItem.Value) & "|" & _
                       strNewIDFormat

            Try
                intErrNo = objPU.mtdUpdGR(strOpCd_AddGR, _
                                          strOpCd_UpdGR, _
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strAccMonth, _
                                          strAccYear, _
                                          strParam, _
                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReceive), _
                                          objGRId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_SAVE_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End Try

        lblGoodsRcvId.Text = objGRId
        lblStatus.Text = objPU.mtdGetGRStatus(objPU.EnumGRStatus.Active)
        txtRemark.Text = strRemark
        onLoad_Display(lblGoodsRcvId.Text)
        onLoad_DisplayLn(lblGoodsRcvId.Text)
        Else
            lblErrGRRefNo.Visible = True
        End If
    End Sub

    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetGRLn As String = "PU_CLSTRX_GR_LINE_GET"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGR As String = "PU_CLSTRX_GR_UPD"
        Dim strOpCd_GRItemDesc As String = "IN_CLSSETUP_INVITEM_DETAILS_GET"
        Dim strOpCd_GRFAGoodRcvLn As String = "PU_CLSTRX_GR_LINE_FAGRCVLN_GET"
        Dim strOpCd_GRFACost As String = "PU_CLSTRX_GR_LINE_FACOST_GET"
        Dim strOpCd_GRFARegLn As String = "PU_CLSTRX_GR_FAREGLN_UPD"
        Dim strOpCd_GRFAAddPerm As String = "FA_CLSSETUP_ASSETPERMIT_GET"
        Dim strOpCd_GRFAAdd As String = "PU_CLSTRX_GR_FAADD_ADD"
        Dim objRsl As Object
        Dim objCostRsl As Object
        Dim objFARegLnRsl As Object
        Dim objFAAddRsl As Object
        Dim objFAAddPermRsl As Object
        Dim objFAItemRsl As Object
        Dim strGoodsRcvRefNo As String = txtGoodsRcvRefNo.Text
        Dim strGoodsRcvRefDate As String = txtGoodsRcvRefDate.Text
        Dim strSuppCode As String = ddlSuppCode.SelectedItem.Value
        Dim strPOID As String = ddlPOId.SelectedItem.Value
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim ReceiveQty As String

        Dim GoodsRcvLnId As String
        Dim FixedAssetCode As String
        Dim ItemDesc As String = ""
        Dim ItemCode As String
        Dim count As Integer
		
		Dim GMValue As Decimal
        Dim NetValue As Decimal
        Dim AssetValue As Decimal
        Dim ItemCost As Decimal

        Dim strParamValue As String = ""
        Dim strParamName As String = ""

        Dim TxID As String
        Dim POType As String
        Dim POLnID As String
        Dim strDate As String = Date_Validation(txtGoodsRcvRefDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRcvRefDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        Dim arrParam As Array
        arrParam = Split(lblAccPeriod.Text, "/")
        If Month(strDate) <> arrParam(0) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        ElseIf Year(strDate) <> arrParam(1) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        '--filtering item cancelled
        Dim strOpCode_Filter As String = "PU_CLSTRX_GR_LINE_DEL"
        
        strParamName = "STRSEARCH"
        strParamValue = " WHERE GOODSRCVID = '" & Trim(lblGoodsRcvId.Text) & "' " & _
                        " AND POLNID IN (SELECT POLNID FROM PU_POLN WHERE STATUS = '2' AND POID ='" & Trim(strPOID) & "')"

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_Filter, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UNDELETE_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try
        '--

        strParam = lblGoodsRcvId.Text & "|" & _
           strGoodsRcvRefNo & "|" & _
           strDate & "|" & _
           strSuppCode & "|" & _
           strPOID & "|" & _
           strRemark & "|" & _
           objPU.EnumGRStatus.Confirmed

        Try
            intErrNo = objPU.mtdUpdGRLn(strOpCd_GetGRLn, _
                                        strOpCd_GetPOLn, _
                                        strOpCd_UpdPOLn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdGR, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CONFIRM_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End If
        End Try

        Try
            intErrNo = objPU.mtdGetGRFALine(strOpCd_GRFAGoodRcvLn, lblGoodsRcvId.Text, objRsl)

            For count = 0 To objRsl.Tables(0).Rows.Count - 1
                POLnID = objRsl.Tables(0).Rows(count).Item("POLnID").Trim()

                ReceiveQty = CDec(objRsl.Tables(0).Rows(count).Item("ReceiveQty"))

                GoodsRcvLnId = objRsl.Tables(0).Rows(count).Item("GoodsRcvLnId").Trim()
                FixedAssetCode = objRsl.Tables(0).Rows(count).Item("FixedAssetCode").Trim()
                ItemCode = objRsl.Tables(0).Rows(count).Item("ItemCode").Trim()

                Try
                    strParam = FixedAssetCode & "|" & strLocation
                    intErrNo = objPU.mtdGetGRFAAddPermission(strOpCd_GRFAAddPerm, strParam, objFAAddPermRsl)

                    If objFAAddPermRsl.tables(0).rows(0).item("AssetAddPerm") = objFA.EnumAssetAddPerm.Yes Then
                        Try
                            strParam = ItemCode & "|" & objINSetup.EnumInventoryItemType.FixedAssetItem & "|" & Trim(strLocation)
                            intErrNo = objINSetup.mtdGetMasterDetail(strOpCd_GRItemDesc, strParam, objFAItemRsl)
                            ItemDesc = objFAItemRsl.tables(0).rows(0).item("Description").trim()
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSSETUP_INVITEM_DETAILS_GET&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                        End Try

                        Try

                            strParam = ItemCode & "||" & POLnID

                            intErrNo = objPU.mtdGetGRFACost(strOpCd_GRFACost, strParam, objCostRsl)
                            ItemCost = CDec(objCostRsl.tables(0).rows(0).item("Cost"))


                            GMValue = ReceiveQty * ItemCost
                            NetValue = GMValue
                            AssetValue = GMValue

                            Try
                                strParam = AssetValue & "|" & NetValue & "|" & GMValue & "|" & _
                                           lblGoodsRcvId.Text & "|" & ItemDesc & "|" & _
                                           strUserId & "|" & strLocation & "|" & FixedAssetCode

                                intErrNo = objPU.mtdUpdGRFARegLn(strOpCd_GRFARegLn, strParam, objFARegLnRsl)
                            Catch Exp As System.Exception
                                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GR_FAREGLN_UPD&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                            End Try

                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GR_LINE_FACOST_GET&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                        End Try


                        Try

                            strParam = strLocation & "|" & _
                                       strGoodsRcvRefNo & "|" & _
                                       strDate & "|" & _
                                       FixedAssetCode & "|" & _
                                       GMValue & "|" & _
                                       "|" & _
                                       strAccMonth & "|" & _
                                       strAccYear & "|" & _
                                       objFATrx.EnumAssetAddStatus.Confirmed & "|" & _
                                       strUserId & "|" & _
                                       objRsl.Tables(0).Rows(count).Item("AccCode") & "|" & _
                                       objRsl.Tables(0).Rows(count).Item("BlkCode") & "|" & _
                                       objRsl.Tables(0).Rows(count).Item("VehCode") & "|" & _
                                       objRsl.Tables(0).Rows(count).Item("VehExpenseCode")

                            intErrNo = objPU.mtdAddFAAdd(strOpCd_GRFAAdd, _
                                                         strParam, _
                                                         objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FAAssetAddition), _
                                                         TxID, _
                                                         objFAAddRsl)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GR_FAADD_ADD&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                        End Try


                    End If
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETPERMIT_GET&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                End Try
            Next


        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_GR_LINE_FAGRCVLN_GET&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

        If intErrorCheck = objPU.EnumPUErrorType.NoError Then
            Dim strOpCode_OA As String = "PU_CLSTRX_GR_GROA_GENERATE"
            strParamName = "LOCCODE|POID|ACCMONTH|ACCYEAR|UPDATEID"
            strParamValue = Trim(strLocation) & _
                            "|" & Trim(ddlPOId.SelectedItem.Value) & _
                            "|" & strAccMonth & _
                            "|" & strAccYear & _
                            "|" & Trim(strUserId)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_OA, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
            End Try

            Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"

            strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"
            strParamValue = Trim(strLocation) & _
                            "|" & "PU_GOODSRCV" & _
                            "|" & "PU_GOODSRCVLN" & _
                            "|" & "GOODSRCVID" & _
                            "|" & Trim(lblGoodsRcvId.Text) & _
                            "|" & "STOCKQTY" & _
                            "|" & ddlInventoryBin.SelectedItem.Value & _
                            "|" & "+" & _
                            "|" & Trim(strUserId)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
            End Try
        End If

        If intErrNo <> 0 Then
            lblErrConfirm.Visible = True
            onLoad_Display(strSelectedGoodsRcvId)
            onLoad_DisplayLn(strSelectedGoodsRcvId)
            BindPO(strSelectedPOId)
            Exit Sub
        End If
        onLoad_Display(lblGoodsRcvId.Text)
        onLoad_DisplayLn(lblGoodsRcvId.Text)
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetGRLn As String = "PU_CLSTRX_GR_LINE_GET"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGR As String = "PU_CLSTRX_GR_UPD"
        Dim strGoodsRcvRefNo As String = txtGoodsRcvRefNo.Text
        Dim strGoodsRcvRefDate As String = txtGoodsRcvRefDate.Text
        Dim strSuppCode As String = ddlSuppCode.SelectedItem.Value
        Dim strPOID As String = ddlPOId.SelectedItem.Value
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim strDate As String = Date_Validation(txtGoodsRcvRefDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRcvRefDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strParam = lblGoodsRcvId.Text & "|" & _
                   strGoodsRcvRefNo & "|" & _
                   strDate & "|" & _
                   strSuppCode & "|" & _
                   strPOID & "|" & _
                   strRemark & "|" & _
                   objPU.EnumGRStatus.Deleted

        Try
            intErrNo = objPU.mtdUpdGRLn(strOpCd_GetGRLn, _
                                        strOpCd_GetPOLn, _
                                        strOpCd_UpdPOLn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdGR, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DELETE_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End If
        End Try
        onLoad_Display(lblGoodsRcvId.Text)
        onLoad_DisplayLn(lblGoodsRcvId.Text)
    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetGRLn As String = "PU_CLSTRX_GR_LINE_GET"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGR As String = "PU_CLSTRX_GR_UPD"
        Dim strGoodsRcvRefNo As String = txtGoodsRcvRefNo.Text
        Dim strGoodsRcvRefDate As String = txtGoodsRcvRefDate.Text
        Dim strSuppCode As String = ddlSuppCode.SelectedItem.Value
        Dim strPOID As String = ddlPOId.SelectedItem.Value
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim strDate As String = Date_Validation(txtGoodsRcvRefDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRcvRefDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strGoodsRcvRefDate = Date_Validation(strGoodsRcvRefDate, False)
        If strGoodsRcvRefDate = "" Then
            lblDate.Visible = True
            Exit Sub
        End If

        '--filtering item cancelled
        Dim strOpCode_Filter As String = "PU_CLSTRX_GR_LINE_DEL"
        Dim strParamValue As String = ""
        Dim strParamName As String = ""

        strParamName = "STRSEARCH"
        strParamValue = " WHERE GOODSRCVID = '" & Trim(lblGoodsRcvId.Text) & "' " & _
                        " AND POLNID IN (SELECT POLNID FROM PU_POLN WHERE STATUS = '2' AND POID ='" & Trim(strPOID) & "')"

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_Filter, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UNDELETE_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try
        '--

        strParam = lblGoodsRcvId.Text & "|" & _
                   strGoodsRcvRefNo & "|" & _
                   strDate & "|" & _
                   strSuppCode & "|" & _
                   strPOID & "|" & _
                   strRemark & "|" & _
                   objPU.EnumGRStatus.Active

        Try
            intErrNo = objPU.mtdUpdGRLn(strOpCd_GetGRLn, _
                                        strOpCd_GetPOLn, _
                                        strOpCd_UpdPOLn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdGR, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UNDELETE_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End If
        End Try
        onLoad_Display(lblGoodsRcvId.Text)
        onLoad_DisplayLn(lblGoodsRcvId.Text)
    End Sub

    Sub btnCancel_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetGRLn As String = "PU_CLSTRX_GR_LINE_GET"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGR As String = "PU_CLSTRX_GR_UPD"
        Dim strGoodsRcvRefNo As String = txtGoodsRcvRefNo.Text
        Dim strGoodsRcvRefDate As String = txtGoodsRcvRefDate.Text
        Dim strSuppCode As String = ddlSuppCode.SelectedItem.Value
        Dim strPOID As String = ddlPOId.SelectedItem.Value
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim strDate As String = Date_Validation(txtGoodsRcvRefDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRcvRefDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strGoodsRcvRefDate = Date_Validation(strGoodsRcvRefDate, False)
        If strGoodsRcvRefDate = "" Then
            lblDate.Visible = True
            Exit Sub
        End If

        strParam = lblGoodsRcvId.Text & "|" & _
                   strGoodsRcvRefNo & "|" & _
                   strDate & "|" & _
                   strSuppCode & "|" & _
                   strPOID & "|" & _
                   strRemark & "|" & _
                   objPU.EnumGRStatus.Cancelled

        Try
            intErrNo = objPU.mtdUpdGRLn(strOpCd_GetGRLn, _
                                        strOpCd_GetPOLn, _
                                        strOpCd_UpdPOLn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdGR, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CANCEL_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End If
        End Try

        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

        strParamValue = Trim(strLocation) & _
                        "|" & "PU_GOODSRCV" & _
                        "|" & "PU_GOODSRCVLN" & _
                        "|" & "GOODSRCVID" & _
                        "|" & Trim(lblGoodsRcvId.Text) & _
                        "|" & "STOCKQTY" & _
                        "|" & ddlInventoryBin.SelectedItem.Value & _
                        "|" & "-" & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try

        onLoad_Display(lblGoodsRcvId.Text)
        onLoad_DisplayLn(lblGoodsRcvId.Text)
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String
        Dim intStatus As Integer
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String
        Dim intErrNo As Integer

        'strUpdString = "where GoodsRcvId = '" & lblGoodsRcvId.Text & "'"
        strStatus = Trim(lblStatus.Text)
        'intStatus = CInt(Trim(lblHidStatus.Text))
        'strPrintDate = Trim(lblPrintDate.Text)
        'strSortLine = "PU_GOODSRCVLN.GoodsRcvLnID"
        'strTable = "PU_GOODSRCV"

        'If intStatus = objPU.EnumDAStatus.Confirmed Then
        '    If strPrintDate = "" Then
        '        Try
        '            intErrNo = objAdminShare.mtdUpdPrintDate(strOpCodePrint, _
        '                                                   strUpdString, _
        '                                                   strTable, _
        '                                                   strCompany, _
        '                                                   strLocation, _
        '                                                   strUserId)
        '        Catch Exp As System.Exception
        '            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_DETAILS_UPD_PRINTDATE&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_dalist.aspx")
        '        End Try
        '    Else
        '        strStatus = strStatus & " (re-printed)"
        '    End If
        'End If

        onLoad_Display(lblGoodsRcvId.Text)
        onLoad_DisplayLn(lblGoodsRcvId.Text)

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_GRDet.aspx?strGoodsRcvId=" & lblGoodsRcvId.Text & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_GRList.aspx")
    End Sub

	Function NoLineAdded(ByVal pv_strGoodsRcvId As String) As Boolean
		Dim strParam As String
		Dim strOpCd As String = "PU_GET_GRLINE_COUNT"
		Dim objRsl As Object
		Dim intErrNo As Integer

		If TRIM(pv_strGoodsRcvId) = "" Then
			Return True
		End If

		strParam = pv_strGoodsRcvId

        Try
            intErrNo = objPU.mtdGetGRLineCount(strOpCd, strParam, objRsl)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_GRLine_Count&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

		If objRsl.Tables(0).Rows.Count > 0 Then
			Return False
		End If

		Return True
	End Function

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
        Dim strText = "Please select Inventory Bin"

        ddlInventoryBin.Items.Clear()
        ddlInventoryBin.Items.Add(New ListItem(strText, "0"))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.HO), objINSetup.EnumInventoryBinLevel.HO))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Central), objINSetup.EnumInventoryBinLevel.Central))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Other), objINSetup.EnumInventoryBinLevel.Other))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinI), objINSetup.EnumInventoryBinLevel.BinI))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinII), objINSetup.EnumInventoryBinLevel.BinII))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIII), objINSetup.EnumInventoryBinLevel.BinIII))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIV), objINSetup.EnumInventoryBinLevel.BinIV))

        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        Else            
            ddlInventoryBin.SelectedIndex = -1
        End If
    End Sub

    Sub btnDispatch_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode_GenDA As String
        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
        Dim strOpCd As String = "PU_CLSTRX_GR_DA_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim strDate As String = Date_Validation(txtGoodsRcvRefDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRcvRefDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strOpCode_GenDA = "PU_CLSTRX_GR_DA_GENERATE_SAM"
        '    Case Else
        '        strOpCode_GenDA = "PU_CLSTRX_GR_DA_GENERATE"
        'End Select
        strOpCode_GenDA = "PU_CLSTRX_GR_DA_GENERATE"

        strParamName = "LOCCODE|GOODSRCVID|ACCYEAR|ACCMONTH|TRANSFERVIA|USERID"

        strParamValue = Trim(strLocation) & _
                        "|" & Trim(lblGoodsRcvId.Text) & _
                        "|" & Year(strDate) & _
                        "|" & Month(strDate) & _
                        "|" & Trim(txtTransferVia.Text) & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_GenDA, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try

        onLoad_DisplayDA(Trim(lblGoodsRcvId.Text))
    End Sub

    Sub onLoad_DisplayDA(ByVal pv_strGoodsRcvId As String)
        Dim strOpCode As String = "PU_CLSTRX_GR_DA_GET"
        Dim dsMaster As Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer

        strParamName = "TRXID"
        strParamValue = Trim(pv_strGoodsRcvId)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        If dsMaster.Tables(0).Rows.Count > 0 Then
            strDAID = Trim(dsMaster.Tables(0).Rows(0).Item("DispAdvID"))

            dgDA.DataSource = Nothing
            dgDA.DataSource = dsMaster.Tables(0)
            dgDA.DataBind()
        Else
            strDAID = ""
        End If

    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label
        Dim Delbutton As LinkButton
        Dim strDesc As String
        Dim strLocCode As String
        Dim strAccCode As String
        Dim strBlkCode As String
        Dim strVehCode As String
        Dim strVehExpCode As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strRcvQty As String
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strItemCode As String
        Dim strItemType As String
        Dim POType As String
        Dim strDate As String = Date_Validation(txtGoodsRcvRefDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRcvRefDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        lblHidStatusEdited.Text = "1"
        dgGRDet.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblGRLnId")
        lblTxLnID.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblItem")
        strItemCode = lbl.Text.Trim
        lblSelectedItemCode.Text = strItemCode
        GetItem(lblSelectedItemCode.Text)
        
        strItemType = Trim(GetItemType(strItemCode))
        If strItemType = objINSetup.EnumInventoryItemType.DirectCharge Or strItemType = objINSetup.EnumInventoryItemType.FixedAssetItem Then
            tblAcc.Visible = True
            FindAcc.Visible = True
            RowChargeLevel.Visible = True
        Else
            tblAcc.Visible = False
            RowChargeLevel.Visible = False
        End If

        If lblTxLnID.Text <> "" Then
            btnAdd.Visible = False
            SaveDtlBtn.Visible = True
        Else
            btnAdd.Visible = True
            SaveDtlBtn.Visible = False
        End If
        'strParamName = "RECEIVEQTY|STOCKQTY|RECEIVEUOM|STOCKUOM|" & _
        '              "ACCCODE|BLKCODE|VEHCODE|VEHEXPENSECODE|FIXEDASSETCODE|" & _
        '              "GOODSRCVID|GOODSRCVLNID|ITEMCODE"

        lbl = E.Item.FindControl("lblReceiveQty")
        strRcvQty = lbl.Text.Trim
        txtReceiveQty.Text = lbl.Text.Trim 'Replace(Replace(strRcvQty, ".", ""), ",", ".")
        lbl = E.Item.FindControl("hidQtyOutstanding")
        lblQtyOutStanding.Text = CDbl(txtReceiveQty.Text) + CDbl(lbl.Text.Trim)
        lbl = E.Item.FindControl("lblCost")
        txtCost.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblReceiveUOM")
        lblPurchaseUOM.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblStockUOM")
        lblUOMCode.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblAccCode")
        strAccCode = lbl.Text.Trim
        ddlAccount.SelectedValue = strAccCode
       
        GetAccountDetails(strAccCode, blnIsBalanceSheet, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers, intNurseryInd)

        lbl = E.Item.FindControl("lblBlkCode")
        strBlkCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVehCode")
        strVehCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVehExpCode")
        strVehExpCode = lbl.Text.Trim

        If Not blnIsBalanceSheet Then
            If blnIsBlockRequire Then
                BindPreBlock(strAccCode, strBlkCode)
                BindBlock(strAccCode, strBlkCode)
                BindVehicle("", strVehExpCode)
                BindVehicleExpense(True, strVehExpCode)
            Else
                BindPreBlock("", strBlkCode)
                BindBlock("", strBlkCode)
                BindVehicle("", strVehExpCode)
                BindVehicleExpense(True, strVehExpCode)
            End If
            If blnIsVehicleRequire Then
                BindVehicle(strAccCode, strVehCode)
                BindVehicleExpense(False, strVehExpCode)
            End If
            If blnIsOthers Then
                lblVehicleOption.Text = True
                BindVehicle("%", strVehExpCode)
                BindVehicleExpense(False, strVehExpCode)
            Else
                lblVehicleOption.Text = False
            End If
        Else
            If blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
                BindPreBlock(strAccCode, strBlkCode)
                BindBlock(strAccCode, strBlkCode)
                BindVehicle("", strVehExpCode)
                BindVehicleExpense(True, strVehExpCode)
            ElseIf blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.No Then
                BindPreBlock("", strBlkCode)
                BindBlock("", strBlkCode)
                BindVehicle("", strVehExpCode)
                BindVehicleExpense(True, strVehExpCode)
            End If
        End If

        ddlBlock.SelectedValue = strBlkCode
        ddlVehCode.SelectedValue = strVehCode
        ddlVehExpCode.SelectedValue = strVehExpCode

        POType = GetPOType(ddlPOId.SelectedItem.Value)
        If POType = objPU.EnumPOType.DirectCharge And Trim(lblStatus.Text) = objPU.mtdGetGRStatus(objPU.EnumGRStatus.Confirmed) Then
            tblLine.Visible = True
            tblDoc.Visible = True
            tblDoc1.Visible = True
            tblAcc.Visible = True
            txtReceiveQty.ReadOnly = True
            txtCost.ReadOnly = True
            txtTtlCost.ReadOnly = True
            txtTtlCost.Text = txtReceiveQty.Text * txtCost.Text
        End If

        Delbutton = E.Item.FindControl("lbDelete")
        Delbutton.Visible = False

        strSelectedGoodsRcvId = lblGoodsRcvId.Text
        hidItemType.Value = strItemType
        ddlChargeLevel.SelectedIndex = 1
        ToggleChargeLevel()
        ddlItemCode.Enabled = False
    End Sub

    Sub GetItem(ByVal pv_strItemCode As String)
        Dim dr As DataRow
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strPOType As String

        Dim strOpCode As String = "IN_CLSTRX_ITEMPART_ITEM_GET"
        Dim dsMaster As Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = " AND itm.ItemCode = '" & Trim(pv_strItemCode) & "' AND itm.LocCode = '" & strLocation & "' AND itm.Status = '" & objINSetup.EnumStockItemStatus.Active & "'  " & "|itm.ItemCode"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        For intCnt = 0 To dsMaster.Tables(0).Rows.Count - 1
            If dsMaster.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(pv_strItemCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsMaster.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = "Please select Item Code"
        dsMaster.Tables(0).Rows.InsertAt(dr, 0)

        ddlItemCode.DataSource = dsMaster.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub btnNew_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("PU_trx_GRDet.aspx")
    End Sub

    Sub BtnAddAllItem_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim dblQty As Double
        Dim dblRate As Double
        Dim arrParam As Array
        Dim strParamID As String
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim objPOItemDs As New Object
        Dim objUOMDs As New Object
        Dim objGRId As Object
        Dim objGRLnId As Object
        Dim intCnt As Integer
        Dim strParam As String
        Dim strParamLn As String = ""
        Dim strParamUOM As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim POType As String
        Dim strGRId As String = Trim(lblGoodsRcvId.Text)
        Dim strNewIDFormat As String
        Dim strOpCd_GetPOLine As String = "PU_CLSTRX_GR_PO_LINE_ITEMPART_GET"
        Dim strOpCd_POLn_UPD As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_GetUOM As String = "ADMIN_CLSUOM_CONVERTION_LIST_GET"
        Dim strOpCd_AddGRLn As String = "PU_CLSTRX_GR_LINE_ADD"
        Dim strOpCd_AddGR As String = "PU_CLSTRX_GR_ADD"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objToLocCodeDs As New Object()
        Dim strToLocCode As String
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim strAccLocCode As String = ""
        Dim strDate As String = Date_Validation(txtGoodsRcvRefDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRcvRefDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)
       
        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        If ddlPOId.SelectedItem.Value = "" Then
            Exit Sub
            'Else
            '    If Left(Trim(ddlPOId.SelectedItem.Value), 3) = "SPK" Then
            '        Exit Sub
            '    End If
        End If

        POType = GetPOType(ddlPOId.SelectedItem.Value)

        strParamName = "STRSEARCH|STRSEARCH1"
        strParamValue = "AND A.POId = '" & Trim(ddlPOId.SelectedItem.Value) & "' AND A.Status in ('" & objPU.EnumPOLnStatus.Active & "','" & objPU.EnumPOLnStatus.Edited & "') " & "|"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_GetPOLine, _
                                                strParamName, _
                                                strParamValue, _
                                                objPOItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_POItem&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

        If objPOItemDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objPOItemDs.Tables(0).Rows.Count - 1
                If objPOItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding") > 0 Then
                    If Trim(objPOItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")) <> Trim(objPOItemDs.Tables(0).Rows(intCnt).Item("UOMCode")) Then
                        strParamUOM = Trim(objPOItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")) & "|" & _
                                      Trim(objPOItemDs.Tables(0).Rows(intCnt).Item("UOMCode")) & "|" & _
                                      objAdmin.EnumUOMStatus.Active & "|A.UOMFrom"

                        Try
                            intErrNo = objAdmin.mtdGetUOMRate(strOpCd_GetUOM, strParamUOM, objUOMDs)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_UOMConvertion&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                        End Try
                        If objUOMDs.Tables(0).Rows.Count > 0 Then
                            dblRate = Convert.ToDouble(objUOMDs.Tables(0).Rows(0).Item("Rate"))
                        Else
                            strParamUOM = lblUOMCode.Text & "|" & _
                                        lblPurchaseUOM.Text & "|" & _
                                        objAdmin.EnumUOMStatus.Active & "|A.UOMFrom"

                            Try
                                intErrNo = objAdmin.mtdGetUOMRate(strOpCd_GetUOM, strParamUOM, objUOMDs)
                            Catch Exp As System.Exception
                                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_UOMConvertion&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                            End Try
                            If objUOMDs.Tables(0).Rows.Count > 0 Then
                                If Convert.ToDouble(objUOMDs.Tables(0).Rows(0).Item("Rate")) <> 0 Then
                                    dblRate = 1.0 / Convert.ToDouble(objUOMDs.Tables(0).Rows(0).Item("Rate"))
                                Else
                                    dblRate = 1
                                End If
                            Else
                                dblRate = 1
                            End If
                        End If
                    Else
                        dblRate = 1
                    End If

                    If POType = objPU.EnumPOType.DirectCharge Then
                        strToLocCode = Mid(Trim(ddlPOId.SelectedItem.Value), 9, 3)
                        If strLocation <> strToLocCode Then
                            strParam = strToLocCode & "|" & objAdminSetup.EnumLocStatus.Active & "|LocCode|"
                            Try
                                intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objToLocCodeDs, "")

                            Catch Exp As System.Exception
                                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_dalist.aspx")
                            End Try

                            If objToLocCodeDs.Tables(0).Rows.Count > 0 Then
                                strAccLocCode = Trim(objToLocCodeDs.Tables(0).Rows(0).Item("AccCode"))
                            End If
                        Else
                            strAccLocCode = "DUMMY"
                        End If
                    End If

                    strGRId = IIf(strGRId = "", "", strGRId)

                    POType = GetPOType(ddlPOId.SelectedItem.Value)
                    strAccYear = Year(strDate)
                    strAccMonth = Month(strDate)
                    strNewIDFormat = "BTB" & "/" & strCompany & "/" & strLocation & "/" & Trim(POType) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

                    If lblHidStatusEdited.Text = "0" Then
                        strParam = strGRId & "|" & _
                                   txtGoodsRcvRefNo.Text & "|" & _
                                   strDate & "|" & _
                                   ddlSuppCode.SelectedItem.Value & "|" & _
                                   ddlPOId.SelectedItem.Value & "|" & _
                                   txtRemark.Text & "|" & _
                                   objPU.EnumGRStatus.Active & "|" & _
                                   Trim(ddlInventoryBin.SelectedItem.Value) & "|" & _
                                   strNewIDFormat

                        strParamLn = strGRId & "|" & _
                                 Trim(objPOItemDs.Tables(0).Rows(intCnt).Item("POLnID")) & "|" & _
                                 Trim(objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode")) & "|" & _
                                 Trim(objPOItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")) & "|" & _
                                 objPOItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding") & "|" & _
                                 Trim(objPOItemDs.Tables(0).Rows(intCnt).Item("UOMCode")) & "|" & _
                                 dblRate & "|" & _
                                 strAccLocCode & "|" & _
                                 "" & "|" & _
                                 "" & "|" & _
                                 "" & "|" & _
                                 IIf(objPOItemDs.Tables(0).Rows(intCnt).Item("PRLocCode") <> "", _
                                        objPOItemDs.Tables(0).Rows(intCnt).Item("PRLocCode"), _
                                        objPOItemDs.Tables(0).Rows(intCnt).Item("PRRefLocCode")) & "|" & _
                                 "|" & _
                                 objPOItemDs.Tables(0).Rows(intCnt).Item("CostToDisplay")

                        Try
                            intErrNo = objPU.mtdAddGRLn(strOpCd_AddGRLn, _
                                        strOpCd_AddGR, _
                                        strOpCd_UpdPOLn, _
                                        strOpCd_UpdItem, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strParam, _
                                        strParamLn, _
                                        intErrorCheck, _
                                        objGRId, _
                                        objGRLnId, _
                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReceive), _
                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReceiveLn))

                            Select Case intErrorCheck
                                Case -1
                                    lblErrOnHand.Visible = True
                                Case -2
                                    lblErrOnHold.Visible = True
                            End Select

                        Catch Exp As System.Exception
                            If intErrNo <> -5 Then
                                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_NEW_GRLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
                            End If
                        End Try

                        strSelectedGoodsRcvId = objGRId
                        strGRId = strSelectedGoodsRcvId
                    End If
                End If
            Next
        End If

        lblGoodsRcvId.Text = strSelectedGoodsRcvId
        strSelectedSuppCode = ddlSuppCode.SelectedItem.Value
        strSelectedPOId = ddlPOId.SelectedItem.Value
        onLoad_Display(strSelectedGoodsRcvId)
        onLoad_DisplayLn(strSelectedGoodsRcvId)
        BindPO(strSelectedPOId)
        BindPOItem(strSelectedPOId)
        BindAccount("")
        BindPreBlock("", "")
        BindBlock("", "")
        BindVehicle("", "")
        BindVehicleExpense(True, "")
        ddlItemCode.Enabled = True

        btnAdd.Visible = True
        SaveDtlBtn.Visible = False
    End Sub

    Sub btnIssue_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode_GenSI As String
        Dim strOpCd As String = "PU_CLSTRX_GR_SI_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim strDate As String = Date_Validation(txtGoodsRcvRefDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtGoodsRcvRefDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strOpCode_GenDA = "PU_CLSTRX_GR_DA_GENERATE_SAM"
        '    Case Else
        '        strOpCode_GenDA = "PU_CLSTRX_GR_DA_GENERATE"
        'End Select
        strOpCode_GenSI = "PU_CLSTRX_GR_SI_GENERATE"

        strParamName = "LOCCODE|GOODSRCVID|ACCYEAR|ACCMONTH|USERID"

        strParamValue = Trim(strLocation) & _
                        "|" & Trim(lblGoodsRcvId.Text) & _
                        "|" & Year(strDate) & _
                        "|" & Month(strDate) & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_GenSI, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try

        onLoad_DisplaySI(Trim(lblGoodsRcvId.Text))
    End Sub

    Sub onLoad_DisplaySI(ByVal pv_strGoodsRcvId As String)
        Dim strOpCode As String = "PU_CLSTRX_GR_SI_GET"
        Dim dsMaster As Object

        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer

        strParamName = "TRXID"
        strParamValue = Trim(pv_strGoodsRcvId)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        If dsMaster.Tables(0).Rows.Count > 0 Then
            strSIID = Trim(dsMaster.Tables(0).Rows(0).Item("StockIssueID"))

            dgSI.DataSource = Nothing
            dgSI.DataSource = dsMaster.Tables(0)
            dgSI.DataBind()
        Else
            strSIID = ""
        End If
    End Sub


    Private Sub lbViewJournal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbViewJournal.Click
        Dim intErrNo As Integer
        Dim dsResult As New Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "GL_JOURNAL_PREDICTION"
        Dim arrPeriod As Array

        arrPeriod = Split(lblAccPeriod.Text, "/")

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|USERID|TRXID"
        strParamValue = strLocation & "|" & arrPeriod(0) & _
                        "|" & arrPeriod(1) & "|" & _
                        Session("SS_USERID") & "|" & Trim(lblGoodsRcvId.Text)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsResult)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DAYEND_PROCESS&errmesg=" & Exp.Message.ToString & "&redirect=")
        End Try

        If dsResult.Tables(0).Rows.Count > 0 Then

            Dim TotalDB As Double
            Dim TotalCR As Double
            Dim intCnt As Integer
            For intCnt = 0 To dsResult.Tables(0).Rows.Count - 1
                TotalDB += dsResult.Tables(0).Rows(intCnt).Item("AmountDB")
                TotalCR += dsResult.Tables(0).Rows(intCnt).Item("AmountCR")
            Next
            lblTotalDB.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TotalDB, 2), 2)
            lblTotalCR.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TotalCR, 2), 2)

            dgViewJournal.DataSource = Nothing
            dgViewJournal.DataSource = dsResult.Tables(0)
            dgViewJournal.DataBind()

            lblTotalDB.Visible = True
            lblTotalCR.Visible = True
            lblTotalViewJournal.Visible = True
            lblTotalViewJournal.Text = "Total Amount : "
        End If

        onLoad_Display(lblGoodsRcvId.Text)
        onLoad_DisplayLn(lblGoodsRcvId.Text)
    End Sub
End Class
