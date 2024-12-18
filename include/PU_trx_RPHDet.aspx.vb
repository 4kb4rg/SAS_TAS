Imports System
Imports System.Data
Imports System.Collections
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Web.UI.HtmlControls
Imports Infragistics.WebUI.UltraWebTab
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.Drawing.Color
Imports System.Math


Public Class PU_trx_RPHDet : Inherits Page

    Protected WithEvents lblErrManySelectDoc As Label
    Protected WithEvents lblErrRef As Label
    Protected WithEvents lblErrItem As Label
    Protected WithEvents lblRPHId As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblLocCode As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblStockItem As Label
    Protected WithEvents lblSelectListLoc As Label
    Protected WithEvents lblSelectListItem As Label
    Protected WithEvents lblPR As Label
    Protected WithEvents lblPRRef As Label
    Protected WithEvents lblRphLNID As Label
    Protected WithEvents lblTptPenyerahan As Label

    'Protected WithEvents ddlSuppCode1 As DropDownList
    Protected WithEvents lblSuppCode1 As Label
    'Protected WithEvents ddlSuppCode2 As DropDownList
    Protected WithEvents lblSuppCode2 As Label
    'Protected WithEvents ddlSuppCode3 As DropDownList
    Protected WithEvents lblSuppCode3 As Label
    Protected WithEvents ddlPPN1 As DropDownList
    Protected WithEvents ddlPPN2 As DropDownList
    Protected WithEvents ddlPPN3 As DropDownList
    Protected WithEvents chkPrinted As CheckBox

    Protected WithEvents DDLPPNTr1 As DropDownList
    Protected WithEvents DDLPPNTr2 As DropDownList
    Protected WithEvents DDLPPNTr3 As DropDownList
    Protected WithEvents DDLLocPenyerahan As DropDownList

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblUpdateDate As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents txtPRID_Plmph As TextBox
    Protected WithEvents lblPRLocCode As TextBox
    Protected WithEvents txtPRRefId As TextBox
    Protected WithEvents ddlPRRefLocCode As DropDownList
    Protected WithEvents lblNote As Label

    Protected WithEvents txtPrQtyOrder As TextBox
    Protected WithEvents txtPurchUOM As TextBox
    Protected WithEvents txtPrCostOrder As TextBox
    Protected WithEvents txtPrTCostOrder As TextBox

    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents TxtItemName As TextBox
    Protected WithEvents txtQtyOrder1 As TextBox
    Protected WithEvents txtCost1 As TextBox
    Protected WithEvents txtQtyOrder2 As TextBox
    Protected WithEvents txtCost2 As TextBox
    Protected WithEvents txtQtyOrder3 As TextBox
    Protected WithEvents txtCost3 As TextBox
    Protected WithEvents txtTtlCost1 As TextBox    
    Protected WithEvents txtTtlCost2 As TextBox
    Protected WithEvents txtTtlCost3 As TextBox
    Protected WithEvents txtPBBKB1 As TextBox
    Protected WithEvents txtPBBKB2 As TextBox
    Protected WithEvents txtPBBKB3 As TextBox
    Protected WithEvents txtPBBKBRate1 As TextBox
    Protected WithEvents txtPBBKBRate2 As TextBox
    Protected WithEvents txtPBBKBRate3 As TextBox
    Protected WithEvents TABBK As UltraWebTab


    Protected WithEvents dgLine As DataGrid
    Protected WithEvents dgLineSup2 As DataGrid
    Protected WithEvents dgLineSup3 As DataGrid
    Protected WithEvents dgLineTrans1 As DataGrid
    Protected WithEvents dgLineTrans2 As DataGrid
    Protected WithEvents dgLineTrans3 As DataGrid

    Protected WithEvents dgRPHDet As DataGrid
    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents lblTotalAmountAll As Label
    Protected WithEvents txtRemark As TextBox

    Protected WithEvents txtSuppCode1 As TextBox
    Protected WithEvents txtSuppName1 As TextBox
    Protected WithEvents txtSuppCode2 As TextBox
    Protected WithEvents txtSuppName2 As TextBox
    Protected WithEvents txtSuppCode3 As TextBox
    Protected WithEvents txtSuppName3 As TextBox

    Protected WithEvents txtsuppNote1 As TextBox
    Protected WithEvents txtsuppNote2 As TextBox
    Protected WithEvents txtsuppNote3 As TextBox

    Protected WithEvents BtnViewPR As HtmlInputButton
    Protected WithEvents BtnViewSPK As HtmlInputButton


    Protected WithEvents tblRPHLine As HtmlTable
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnConfirm As ImageButton
    Protected WithEvents btnPrint As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnEdited As ImageButton
    Protected WithEvents ImgGen As ImageButton
    Protected WithEvents BtnVerified As ImageButton

    Protected WithEvents lblHidStatus As Label
    Protected WithEvents lblRPHTypeName As Label

    Protected WithEvents lblErrSuppCode1 as Label
    Protected WithEvents lblErrSuppCode2 as Label
    Protected WithEvents lblErrSuppCode3 as Label
    Protected WithEvents lblExistSuppCode as Label
    Protected WithEvents lblCheckBoxError as Label
    Protected WithEvents lblRPHType as Label

    Protected WithEvents ddlDeptCode As DropDownList
    Protected WithEvents ddlPOIssued As DropDownList
    Protected WithEvents lblDeptCode As Label
    Protected WithEvents lblPOIssued As Label

    Protected WithEvents lblErrMessage As Label
    Protected objHR As New agri.HR.clsSetup()
    Dim objDataSet As New Object()

    Protected WithEvents lblErrItemExist As Label
    Protected WithEvents ChkPPN As CheckBox
    Protected WithEvents ChkSupp1 As CheckBox
    Protected WithEvents ChkSupp2 As CheckBox
    Protected WithEvents ChkSupp3 As CheckBox

    Protected WithEvents lblValidationQtyOrder As Label
    Protected WithEvents hidOrgQtyOrder As HtmlInputHidden

    Protected WithEvents lblErrPRID As Label

    Protected WithEvents dgRPHPO As DataGrid
    Protected WithEvents chkCentralized As CheckBox
    Protected WithEvents txtPRID As TextBox
    Protected WithEvents Centralized_Yes As HtmlTableRow
    Protected WithEvents Centralized_No As HtmlTableRow

    Protected WithEvents ddlCurrency As DropDownList
    Protected WithEvents txtExRate As TextBox
    Protected WithEvents txtAddNote As TextBox
    'Protected WithEvents ddlTransporter As DropDownList
    Protected WithEvents txtAmtTransportFee As TextBox
    Protected WithEvents txttransporterName2 As TextBox
    Protected WithEvents txttransporter2 As TextBox
    Protected WithEvents txttransporterName3 As TextBox

    Protected WithEvents txttransporterCode1 As TextBox
    Protected WithEvents txttransporterName1 As TextBox

    Protected WithEvents txtItemCode_Rep As TextBox
    Protected WithEvents TxtItemName_Rep As TextBox
    Protected WithEvents txtRphTipe As TextBox


    Protected WithEvents txtAmtTransportFee2 As TextBox
    Protected WithEvents txttransporter3 As TextBox
    Protected WithEvents txtAmtTransportFee3 As TextBox
    Protected WithEvents lblErrTransporter As Label
    Protected WithEvents txtDiscount1 As TextBox
    Protected WithEvents txtDiscount2 As TextBox
    Protected WithEvents txtDiscount3 As TextBox
    Protected WithEvents lblErrSelected As Label
    Protected WithEvents txtPPN221 As TextBox
    Protected WithEvents txtPPN222 As TextBox
    Protected WithEvents txtPPN223 As TextBox
    Protected WithEvents txtAddDisc As TextBox
    Protected WithEvents txtTtlAftDisc As TextBox
    Protected WithEvents hidTtlAmount As HtmlInputHidden
    Protected WithEvents hidTtlAmtAftDisc As HtmlInputHidden
    Protected WithEvents hidAddDisc As HtmlInputHidden

    Protected WithEvents lblErrRPHDate As Label
    Protected WithEvents txtRPHDate As TextBox

    Protected WithEvents ddlPurchUom1 As DropDownList

    Protected WithEvents tblPR As HtmlTable
    Protected WithEvents tblNote As HtmlTable
    Protected WithEvents tblDetail As HtmlTable


    Protected WithEvents FindIN As HtmlInputButton
    Protected WithEvents FindWS As HtmlInputButton
    Protected WithEvents FindDC As HtmlInputButton
    Protected WithEvents FindFA As HtmlInputButton
    Protected WithEvents FindNU As HtmlInputButton

    Protected WithEvents lblErrTxtPRID As Label



    Protected ObjOk As New agri.GL.ClsTrx()
    Protected objPU As New agri.PU.clsTrx()
    Protected objPUSetup As New agri.PU.clsSetup()
    Protected objIN As New agri.IN.clsTrx()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objAdmin As New agri.Admin.clsLoc()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objSysCfg As New agri.PWSystem.clsConfig()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmShare As New agri.Admin.clsShare()
    Dim objLangCapDs As New DataSet()
    Dim objCMSetup As New agri.CM.clsSetup()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objAdminUOM As New agri.Admin.clsUom()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPUAR As Integer

    Dim intLevel As Integer

    Dim strSelectedRPHId As String
    Dim strSelectedRPHType As String
    Dim strSelectedSuppCode1 As String
    Dim strSelectedSuppCode2 As String
    Dim strSelectedSuppCode3 As String
    Dim strSelectedPRId As String
    Dim strSelectedPRRefLocCode As String
    Dim strSelectedItemCode As String
    Dim blnSelectManyDoc As Boolean = False

    Dim strPhyYear As String
    Dim strPhyMonth As String
    Dim strLastPhyYear As String

    Const ITEM_PART_SEPERATOR As String = " @ "
    Dim strLocType As String
    Dim strRPHType As String

    Dim strPRLocCode As String
    Dim strCurrency As String
    Dim strExRate As String
    Dim strAcceptFormat As String

#Region "TOOLS & COMPONENT"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        'strAccMonth = Session("SS_PUACCMONTH")
        'strAccYear = Session("SS_PUACCYEAR")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
        intLevel = Session("SS_USRLEVEL")
        strPhyYear = Session("SS_PHYYEAR")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyMonth = strAccMonth
        strLastPhyYear = Session("SS_LASTPHYYEAR")
        strLocType = Session("SS_LOCTYPE")

        ddlPPN1.Enabled = False
        ddlPPN2.Enabled = False
        ddlPPN3.Enabled = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrSuppCode1.Visible = False
            lblErrSuppCode2.Visible = False
            lblErrSuppCode3.Visible = False
            lblErrRef.Visible = False
            lblErrItem.Visible = False
            lblExistSuppCode.Visible = False
            lblDeptCode.Visible = False
            lblPOIssued.Visible = False
            lblErrItemExist.Visible = False
            lblErrTransporter.Visible = False
            lblErrSelected.Visible = False
            lblCheckBoxError.Visible = False
            lblErrRPHDate.Visible = False
            lblErrTxtPRID.Visible = True

            txtItemCode.Attributes.Add("readonly", "readonly")
            txtPrQtyOrder.Attributes.Add("readonly", "readonly")
            txtPrCostOrder.Attributes.Add("readonly", "readonly")
            txtPurchUOM.Attributes.Add("readonly", "readonly")
            txtPrTCostOrder.Attributes.Add("readonly", "readonly")
            TxtItemName.Attributes.Add("readonly", "readonly")

            txtItemCode_Rep.Attributes.Add("readonly", "readonly")
            TxtItemName_Rep.Attributes.Add("readonly", "readonly")

            txtSuppCode1.Attributes.Add("readonly", "readonly")
            txtSuppCode2.Attributes.Add("readonly", "readonly")
            txtSuppCode3.Attributes.Add("readonly", "readonly")

            txttransporterCode1.Attributes.Add("readonly", "readonly")
            txttransporter2.Attributes.Add("readonly", "readonly")
            txttransporter3.Attributes.Add("readonly", "readonly")
            txtPRID_Plmph.Attributes.Add("readonly", "readonly")
            txtPRRefId.Attributes.Add("readonly", "readonly")
            lblPRLocCode.Attributes.Add("readonly", "readonly")

            If blnSelectManyDoc = False Then
                lblErrManySelectDoc.ForeColor = Black
            End If

            strSelectedRPHId = Trim(IIf(Request.QueryString("RPHID") = "", Request.Form("RPHID"), Request.QueryString("RPHID")))
            strSelectedRPHType = Trim(IIf(Request.QueryString("RPHType") = "", Request.Form("RPHType"), Request.QueryString("RPHType")))

            onload_GetLangCap()

            If strSelectedRPHId = "" Then
                If strSelectedRPHType = objPU.EnumRPHType.Stock Or strSelectedRPHType = objPU.EnumRPHType.FixedAsset Then
                    ddlDeptCode.Enabled = True
                    ddlPOIssued.Enabled = True
                Else
                    ddlDeptCode.Enabled = True
                    ddlPOIssued.Enabled = True
                End If
            End If

            If Not IsPostBack Then
                BtnVerified.Visible = False
                btnConfirm.Attributes("onclick") = "javascript:return ConfirmAction('Confirmed DTH');"
                ImgGen.Attributes("onclick") = "javascript:return ConfirmAction('Generate Item');"
                BtnVerified.Attributes("onclick") = "javascript:return ConfirmAction('Verified DTH');"
                btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('Verified DTH');"

                BindPPNList()
                lClearDetail()

                If strSelectedRPHId <> "" Then
                    BindUOM("")
                    BindPRItem("")
                    onLoad_DisplayRPH(strSelectedRPHId)
                    onLoad_DisplayRPHLn(strSelectedRPHId)
                    BindLoc(strSelectedRPHId)
                    BindPR(strSelectedRPHId)
                Else
                    If strSelectedRPHType <> "" Then
                        lblRPHType.Text = strSelectedRPHType
                        txtRphTipe.Text = strSelectedRPHType
                        If lblRPHType.Text = objPU.EnumRPHType.Stock Then
                            lblRPHTypeName.Text = "Stock / Workshop"
                        Else
                            lblRPHTypeName.Text = objPU.mtdGetRPHType(CInt(strSelectedRPHType))
                        End If
                    End If

                    txtRPHDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    btnDelete.Visible = False
                    btnUnDelete.Visible = False

                    'btnCancel.Visible = False
                    btnPrint.Visible = False
                    btnConfirm.Visible = False
                    BindUOM("")
                    BindPR("")
                    BindLoc("")

                    If lblRPHType.Text = objPU.EnumRPHType.Stock Then
                        BindLocPenyerahan(strLocation)
                    Else
                        BindLocPenyerahan("")
                    End If
                    BindDeptCode("")
                        BindPOIssued("")
                        BindCurrencyList("")
                        txtAddDisc.Text = "0"
                        txtTtlAftDisc.Text = "0"
                    End If
                End If
            CheckType()

            If lblRPHType.Text.Trim = "2" Then
                BtnViewSPK.Visible = True
                BtnViewPR.Visible = False
            Else
                BtnViewSPK.Visible = False
                BtnViewPR.Visible = True
            End If

        End If
    End Sub

    Sub SupplierChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedSuppCode1 = txtSuppCode1.Text
        strSelectedSuppCode2 = txtSuppCode2.Text
        strSelectedSuppCode3 = txtSuppCode3.Text

        If strSelectedSuppCode1 <> "" Then
            If strSelectedSuppCode2 <> "" Then
                If strSelectedSuppCode3 = "" Then
                    If (strSelectedSuppCode1 = strSelectedSuppCode2) Then
                        lblExistSuppCode.Visible = True
                    End If
                Else
                    If (strSelectedSuppCode1 = strSelectedSuppCode2) Then
                        lblExistSuppCode.Visible = True
                    ElseIf (strSelectedSuppCode1 = strSelectedSuppCode3) Then
                        lblExistSuppCode.Visible = True
                    ElseIf (strSelectedSuppCode2 = strSelectedSuppCode3) Then
                        lblExistSuppCode.Visible = True
                    End If
                End If
            Else
                If strSelectedSuppCode3 <> "" Then
                    If (strSelectedSuppCode1 = strSelectedSuppCode3) Then
                        lblExistSuppCode.Visible = True
                    End If
                End If
            End If
        Else
            If strSelectedSuppCode2 <> "" Then
                If strSelectedSuppCode3 <> "" Then
                    If (strSelectedSuppCode2 = strSelectedSuppCode3) Then
                        lblExistSuppCode.Visible = True
                    End If
                End If
            End If
        End If

    End Sub

    Sub PRIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'strSelectedPRId = ddlPRId.SelectedItem.Value

        'If strSelectedPRId <> "" Then
        '    BindPRItem(strSelectedPRId)
        '    txtPRRefId.Enabled = False
        '    ddlPRRefLocCode.Enabled = False
        '    lblErrPRID.Visible = False
        '    lblPRLocCode.Text = strPRLocCode
        'Else
        '    BindINItem("")
        '    txtPRRefId.Enabled = True
        '    ddlPRRefLocCode.Enabled = True
        '    ddlPRId.Enabled = True
        '    lblPRLocCode.Text = ""
        'End If

    End Sub

    Sub LocIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'If strSelectedPRId <> "" Then
        '    BindPRItem("")
        'Else
        '    BindINItem("")
        'End If

        'If ddlPRRefLocCode.SelectedItem.Value <> "" Then
        '    strSelectedSuppCode1 = txtSuppCode1.Text
        '    strSelectedSuppCode2 = txtSuppCode2.Text
        '    strSelectedSuppCode3 = txtSuppCode3.Text
        '    strSelectedPRId = IIf(chkCentralized.Checked = True, txtPRID_Plmph.Text, Trim(txtPRID.Text))
        '    strSelectedPRRefLocCode = ddlPRRefLocCode.SelectedItem.Value
        '    strSelectedItemCode = txtPRID_Plmph.Text 'ddlItemCode.SelectedItem.Value
        '    'txtPRID_Plmph.Enabled = False
        '    lblPRLocCode.Text = ""
        '    txtPRID.Enabled = False
        'Else
        '    'txtPRID_Plmph.Enabled = True
        '    txtPRRefId.Enabled = False
        '    ddlPRRefLocCode.Enabled = False
        '    txtPRID.Enabled = True
        'End If
    End Sub

    Sub ItemIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedSuppCode1 = txtSuppCode1.Text
        strSelectedSuppCode2 = txtSuppCode2.Text
        strSelectedSuppCode3 = txtSuppCode3.Text
        strSelectedPRId = IIf(chkCentralized.Checked = True, txtPRID_Plmph.Text, Trim(txtPRID.Text))
        strSelectedPRRefLocCode = ddlPRRefLocCode.SelectedItem.Value
        strSelectedItemCode = txtItemCode.Text 'ddlItemCode.SelectedItem.Value
        strSelectedRPHType = lblRPHType.Text

        Dim objItemDs As New Object()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim strOpCd As String = "IN_CLSTRX_PURREQ_ITEMLIST_GET"

        strParamName = "LOCCODE|ITEMTYPE|ITEMSTATUS|ITEMCODE"
        strParamValue = strLocation & _
                        "|" & strSelectedRPHType & _
                        "|" & objINSetup.EnumStockItemStatus.Active & _
                        "|" & strSelectedItemCode

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If objItemDs.Tables(0).Rows.Count > 0 Then
            If chkCentralized.Checked = True Then
                txtCost1.Text = objItemDs.Tables(0).Rows(0).Item("PRCost")
            Else
                txtCost1.Text = objItemDs.Tables(0).Rows(0).Item("AverageCost")
            End If
        Else
            txtCost1.Text = "0"
        End If

        txtCost2.Text = txtCost1.Text
        txtCost3.Text = txtCost1.Text
        'If strSelectedPRId <> "" Then
        '    If chkCentralized.checked = True Then
        '        BindPRItem(strSelectedPRId)
        '    Else
        '        BindINItem(strSelectedItemCode)
        '    End If
        'Else
        '    BindINItem(strSelectedItemCode)
        'End If
    End Sub

#Region "DEDR"

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_DelRPHLn As String = "PU_CLSTRX_RPH_LINE_DEL"
        Dim strOpCd_GetRPHLn As String = "PU_CLSTRX_RPH_LINE_GET"
        Dim strOpCd_UpdRPH As String = "PU_CLSTRX_RPH_UPD"
        Dim strParam As String = ""
        Dim RPHLnIdCell As TableCell = E.Item.Cells(0)
        Dim strRPHLnId As String
        Dim intErrNo As Integer
        Dim EditItem As Label

        strExRate = Trim(txtExRate.Text)
        'strRPHLnId = RPHLnIdCell.Text
        EditItem = E.Item.FindControl("lblRPHLnDetId")
        strRPHLnId = EditItem.Text

        strParam = lblRPHId.Text & "|" & strRPHLnId & "|" & strAccYear & "|" & strExRate
        Try
            intErrNo = objPU.mtdDelRPHLn(strOpCd_DelRPHLn, _
                                        strOpCd_GetRPHLn, _
                                        strOpCd_UpdRPH, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_DEL_POLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        strSelectedRPHId = lblRPHId.Text
        strSelectedPRId = IIf(chkCentralized.Checked = True, Request.Form("txtPRID_Plmph"), Request.Form("txtPRID"))
        onLoad_DisplayRPH(strSelectedRPHId)
        onLoad_DisplayRPHLn(strSelectedRPHId)
        BindPR("")
        BindLoc("")
        'BindPRItem(strSelectedPRId)
        If chkCentralized.Checked = True Then
            BindPRItem(strSelectedPRId)
            BindPR(strSelectedRPHId)
        Else
            BindINItem("")
        End If
        lblRphLNID.Text = ""
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_GetRPHLn As String = "PU_CLSTRX_RPH_LINE_GET"
        Dim strOpCd_UpdRPHLn As String = "PU_CLSTRX_RPH_LINE_UPD"
        Dim strOpCd_UpdRPH As String = "PU_CLSTRX_RPH_UPD"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdCost As String = "PU_CLSTRX_COST_UPD"
        Dim strParam As String = ""
        Dim RPHLnIdCell As TableCell = E.Item.Cells(0)
        Dim ItemCodeCell As TableCell = E.Item.Cells(1)
        Dim QtyOrderCell As TableCell = E.Item.Cells(2)
        Dim CostCell As TableCell = E.Item.Cells(3)
        Dim strRPHLnId As String
        Dim strItemCode As String
        Dim strQtyOrder1 As String
        Dim strCost1 As String
        Dim strQtyOrder2 As String
        Dim strCost2 As String
        Dim strQtyOrder3 As String
        Dim strCost3 As String
        Dim strPRLocCode As String
        Dim strPRRefLocCode As String
        Dim intErrNo As Integer
        Dim strQtyReceive1 As String

        strRPHLnId = RPHLnIdCell.Text
        strItemCode = ItemCodeCell.Text
        strQtyOrder1 = QtyOrderCell.Text
        strCost1 = CostCell.Text

        strQtyReceive1 = "0"

        If strPRLocCode = "&nbsp;" Then
            strPRLocCode = ""
        End If
        If strPRRefLocCode = "&nbsp;" Then
            strPRRefLocCode = ""
        End If

        'strParam = lblRPHId.Text & "|" & _
        '           strRPHLnId & "|" & _
        '           objPU.EnumRPHLnStatus.Cancelled & "|" & _
        '           strItemCode & "|" & _
        '           strQtyOrder1 & "|" & _
        '           strPRLocCode & "|" & _
        '           strPRRefLocCode & "|" & _
        '           strQtyReceive1

        'Try
        '    intErrNo = objPU.mtdUpdRPHLn(strOpCd_GetRPHLn, _
        '                                strOpCd_UpdRPHLn, _
        '                                strOpCd_UpdRPH, _
        '                                strOpCd_UpdItem, _
        '                                strOpCd_UpdCost, _
        '                                strCompany, _
        '                                strLocation, _
        '                                strUserId, _
        '                                strParam, _
        '                                True)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CANCEL_POLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        'End Try
        onLoad_DisplayRPH(lblRPHId.Text)
        onLoad_DisplayRPHLn(lblRPHId.Text)
        lClearDetail()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim chkDetSup1 As CheckBox
        Dim chkDetSup2 As CheckBox
        Dim chkDetSup3 As CheckBox
        Dim UpdButton As LinkButton
        Dim DelButton As LinkButton
        Dim EdtButton As LinkButton
        Dim CanButton As LinkButton
        Dim Lbl As Label
        Dim RPHLnIdCell As TableCell = E.Item.Cells(0)
        Dim strRPHLnId As String
        'strRPHLnId = RPHLnIdCell.Text

        dgRPHDet.EditItemIndex = CInt(E.Item.ItemIndex)

        Lbl = dgRPHDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblRPHLnDetId")
        strRPHLnId = Lbl.Text
        chkDetSup1 = dgRPHDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("chkDetSup1")
        chkDetSup1.Enabled = True

        chkDetSup2 = dgRPHDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("chkDetSup2")
        chkDetSup2.Enabled = True

        chkDetSup3 = dgRPHDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("chkDetSup3")
        chkDetSup3.Enabled = True

        EdtButton = dgRPHDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Edit")
        EdtButton.Visible = False
        UpdButton = dgRPHDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
        UpdButton.Visible = False
        CanButton = dgRPHDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Cancel")
        CanButton.Visible = True
        DelButton = dgRPHDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
        DelButton.Visible = False

        onLoad_EditDetail(lblRPHId.Text, strRPHLnId)
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd As String = "PU_CLSTRX_RPH_LINE_UPD_SPL"
        Dim strOpCd_AddRPH As String = "PU_CLSTRX_RPH_ADD"
        Dim strOpCd_UpdRPH As String = "PU_CLSTRX_RPH_UPD"
        Dim strOppCd_GetID As String = "PU_CLSTRX_PO_GETID"
        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"
        Dim objRPHId As New Object()
        Dim strPRID As String = lblRPHId.Text.Trim
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim chkDetSup1 As CheckBox
        Dim chkDetSup2 As CheckBox
        Dim chkDetSup3 As CheckBox
        Dim strFSupp1 As String
        Dim strFSupp2 As String
        Dim strFSupp3 As String
        Dim strRPHLnId As String
        Dim EditItem As Label
        Dim strParam As String
        Dim strAddDiscount As String = Trim(txtAddDisc.Text)
        Dim strPPN As String

        EditItem = E.Item.FindControl("lblRPHLnDetId")
        strRPHLnId = EditItem.Text

        chkDetSup1 = dgRPHDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("chkDetSup1")
        chkDetSup1.Enabled = True

        chkDetSup2 = dgRPHDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("chkDetSup2")
        chkDetSup2.Enabled = True

        chkDetSup3 = dgRPHDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("chkDetSup3")
        chkDetSup3.Enabled = True

        'If chkDetSup1.Checked = False Then
        '    If chkDetSup2.Checked = False Then
        '        If chkDetSup3.Checked = False Then
        '            lblCheckBoxError.Visible = True
        '            Exit Sub
        '        End If
        '    End If
        'End If

        If chkDetSup1.Checked = True Then
            strFSupp1 = "1"
        Else
            strFSupp1 = "2"
        End If
        If chkDetSup2.Checked = True Then
            strFSupp2 = "1"
        Else
            strFSupp2 = "2"
        End If
        If chkDetSup3.Checked = True Then
            strFSupp3 = "1"
        Else
            strFSupp3 = "2"
        End If

        If ChkPPN.Checked = True Then
            strPPN = "1"
        Else
            strPPN = "2"
        End If

        strParamName = "RPHID|RPHLNID|FSUPP1|FSUPP2|FSUPP3|UPDATEID|LOCCODE"
        strParamValue = lblRPHId.Text & "|" & strRPHLnId & "|" & strFSupp1 & "|" & strFSupp2 & "|" & strFSupp3 & "|" & _
                        strUserId & "|" & strLocation

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        'strParam = lblRPHId.Text & "|" & _
        '                    "|" & _
        '                    "|" & _
        '                    strSelectedRPHType & "|" & _
        '                    strAccMonth & "|" & _
        '                    strAccYear & "|" & _
        '                    objPU.EnumRPHStatus.Active & "|" & _
        '                    strSelectedRPHType & "|" & _
        '                    "||||||" & strPPN & "|" & IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
        '                    strCurrency & "|" & strExRate & "|" & strAddDiscount & "|" & DDLLocPenyerahan.SelectedItem.Value

        'Try
        '    intErrNo = objPU.mtdUpdRPH(strOpCd_AddRPH, _
        '                            strOpCd_UpdRPH, _
        '                            strOppCd, _
        '                            strOppCd_Back, _
        '                            strCompany, _
        '                            strLocation, _
        '                            strUserId, _
        '                            strParam, _
        '                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.NewPurchaseOrder), _
        '                            objRPHId)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_SAVE_RPH&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        'End Try

        lClearDetail()
        onLoad_DisplayRPH(lblRPHId.Text)
        onLoad_DisplayRPHLn(lblRPHId.Text)

    End Sub

#End Region

#Region "COMPONENT BUTTON & IMAGE"
    Sub linkClear_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtItemCode_Rep.Text = ""
        TxtItemName_Rep.Text = ""
    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddRPHLn As String = "PU_CLSTRX_RPH_LINE_ADD"
        Dim strOpCd_UpdRPHLn As String = "PU_CLSTRX_RPH_LINE_UPD"
        Dim strOpCd_GetRPHLn As String = "PU_CLSTRX_RPH_LINE_DETAILS_GET"
        Dim strOpCd_AddRPH As String = "PU_CLSTRX_RPH_ADD"
        Dim strOpCd_UpdRPH As String = "PU_CLSTRX_RPH_UPD"

        Dim strOppCd_GetID As String = "PU_CLSTRX_SYS_ID_GET"
        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "RPH"
        Dim strHistYear As String = ""
        Dim blnIsDetail As Boolean = True

        Dim objAutoNum As New Object

        Dim objRPHId As New Object()
        Dim objRPHLnId As New Object()
        Dim strRPHId As String
        Dim strSuppCode1 As String = txtSuppCode1.Text
        Dim strSuppCode2 As String = txtSuppCode2.Text
        Dim strSuppCode3 As String = txtSuppCode3.Text
        Dim strPRId As String
        Dim strPRLocCode As String = lblPRLocCode.Text
        Dim strPRRefId As String = txtPRRefId.Text
        Dim strPRRefLocCode As String = ddlPRRefLocCode.SelectedItem.Value
        Dim strItemCode As String = txtItemCode.Text 'ddlItemCode.SelectedItem.Value

        Dim strRemark As String = Trim(Request.Form("txtRemark"))
        Dim strParam As String = ""
        Dim strParamName As String = ""

        Dim intErrNo As Integer

        Dim intPRIDInd As Integer
        Dim intPRRefIDInd As Integer
        Dim intPRRefLocCode As Integer
        Dim strNewIDFormat As String
        Dim DecQtyOrder As Decimal

        Dim strDeptCode As String = Trim(ddlDeptCode.SelectedItem.Value)

        Dim strAddDiscount As String = Trim(txtAddDisc.Text)
        Dim SelectedPOIssued As String = ddlPOIssued.SelectedItem.Value
        Dim strPOIssued As String
        Dim strDate As String = Date_Validation(txtRPHDate.Text, False)
        Dim strPRLocation As String

        Dim StrNoUrut As String = ""

        If strDeptCode = "" Then
            lblDeptCode.Visible = True
            Exit Sub
        End If

        If DDLLocPenyerahan.SelectedItem.Value = "" Then
            lblTptPenyerahan.Visible = True
            DDLLocPenyerahan.Focus()
            Exit Sub
        Else
            lblTptPenyerahan.Visible = False
        End If

        If SelectedPOIssued = "" Then
            lblPOIssued.Visible = True
            Exit Sub
        End If

        If chkCentralized.Checked = True Then
            strPRId = Trim(txtPRID_Plmph.Text)
            If strPRId = "" Then
                lblErrPRID.Visible = True
                Exit Sub
            End If
        Else
            strPRId = Trim(txtPRID.Text)
            If strPRId = "" Then
                lblErrTxtPRID.Visible = True
                Exit Sub
            End If
        End If

        If hidOrgQtyOrder.Value = "" Then
            hidOrgQtyOrder.Value = 0
        End If

        If checkSupplier() = True Then
            Exit Sub
        End If

        strCurrency = ddlCurrency.SelectedItem.Value
        strExRate = Trim(txtExRate.Text)

        lblCheckBoxError.Visible = False

        intPRIDInd = IIf(strPRId = "", 0, 1)
        intPRRefIDInd = IIf(strPRRefId = "", 0, 1)
        intPRRefLocCode = IIf(strPRRefLocCode = "", 0, 1)

        If (intPRRefIDInd + intPRRefLocCode) = 1 Then
            lblErrRef.Visible = True
            Exit Sub
        End If

        If strItemCode = "" Then
            lblErrItem.Visible = True
            lblErrItemExist.Visible = True
            Exit Sub
        Else
            If InStr(strItemCode, ITEM_PART_SEPERATOR) <> 0 Then
                strItemCode = Trim(Mid(strItemCode, 1, InStr(strItemCode, ITEM_PART_SEPERATOR) - 1))
            End If
        End If

        If Len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
        End If

        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|TIPE"
        strParam = strAccMonth & "|" & _
                    strAccYear & "|" & _
                    strLocation & "|" & _
                    "RPH"
        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOppCd_GetID, _
                                            strParamName, _
                                            strParam, _
                                            objAutoNum)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If objAutoNum.Tables(0).Rows.Count > 0 Then
            StrNoUrut = objAutoNum.Tables(0).Rows(0).Item("NewNo").Trim()
        End If

        Select Case CInt(SelectedPOIssued)
            Case objPU.EnumPOIssued.JKT
                strPOIssued = "J"
            Case objPU.EnumPOIssued.PKU
                strPOIssued = "P"
            Case objPU.EnumPOIssued.LMP
                strPOIssued = "L"
            Case objPU.EnumPOIssued.PLM
                strPOIssued = "P"
            Case objPU.EnumPOIssued.BKL
                strPOIssued = "B"
            Case objPU.EnumPOIssued.LOK
                strPOIssued = "L"
            Case objPU.EnumPOIssued.MDN
                strPOIssued = "M"
            Case objPU.EnumPOIssued.JMB
                strPOIssued = "J"
            Case objPU.EnumPOIssued.PDG
                strPOIssued = "P"
            Case objPU.EnumPOIssued.ACH
                strPOIssued = "A"
            Case objPU.EnumPOIssued.PON
                strPOIssued = "P"
            Case objPU.EnumPOIssued.SAM
                strPOIssued = "S"
            Case objPU.EnumPOIssued.PLK
                strPOIssued = "P"
            Case objPU.EnumPOIssued.BJR
                strPOIssued = "B"
        End Select

        If Month(strDate) < strAccMonth And Year(strDate) <= strAccYear Then
            lblErrRPHDate.Visible = True
            lblErrRPHDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        strPRLocation = Mid(Trim(strPRId), 9, 3) 'if online

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If Len(lblRPHId.Text) = 0 Then
            'strNewIDFormat = "RPH" & "/" & strCompany & "/" & strLocation & "/" & strPOIssued & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & StrNoUrut
            strNewIDFormat = StrNoUrut & "/" & "DTH" & "/" & strCompany & "/" & strlocation & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & strAccYear
        Else
            strNewIDFormat = lblRPHId.Text.Trim
        End If

        ''header

        strRPHId = strNewIDFormat

        If strRPHId = "" Then
            UserMsgBox(Me, "No Record Found !!!")
            Exit Sub
        End If

        strParamName = "RPHID|LOCCODE|REMARK|ACCMONTH|ACCYEAR|STATUS|CREATEDATE|UPDATEDATE|PRINTDATE|UPDATEID|RPHTYPE|DEPTCODE|POISSUED|PPN|CENTRALIZED|CURRENCYCODE|EXCHANGERATE|ADDDISCOUNT|USERPO|LOCPENYERAHAN|ISPRINT"
        strParam = strRPHId & "|" & _
                   strLocation & "|" & _
                   strRemark & "|" & _
                   strAccMonth & "|" & _
                   strAccYear & "|" & _
                   "1" & "|" & _
                   strDate & "|" & _
                   strDate & "|" & _
                   "2000-01-01" & "|" & _
                   strUserId & "|" & _
                   lblRPHType.Text & "|" & _
                   strDeptCode & "|" & _
                   SelectedPOIssued & "|" & _
                   IIf(ChkPPN.Checked = True, Session("SS_PPNRATE"), 0) & "|" & _
                   IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                   strCurrency & "|" & _
                   strExRate & "|" & _
                   strAddDiscount & "|" & _
                   strUserId & "|" & _
                   DDLLocPenyerahan.SelectedItem.Value & "|" & _
                   "0"
        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd_AddRPH, _
                                                    strParamName, _
                                                    strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        onLoad_DisplayRPH(strRPHId)

        If Len(lblRPHId.Text) = 0 Then
            UserMsgBox(Me, "No Record Found !!!")
            Exit Sub
        End If

        Session("DTID") = lblRPHId.Text.Trim
        Session("EXRATE") = lCDbl(txtExRate.Text)
        Session("PRREFLOCCODE") = ddlPRRefLocCode.SelectedItem.Value
        Session("PRID") = txtPRID_Plmph.Text
        Session("SupCode1") = txtSuppCode1.Text.Trim
        Session("SupCode2") = txtSuppCode2.Text.Trim
        Session("SupCode3") = txtSuppCode3.Text.Trim

        Session("SupName1") = txtSuppName1.Text.Trim
        Session("SupName2") = txtSuppName2.Text.Trim
        Session("SupName3") = txtSuppName3.Text.Trim

        Session("PPN1") = ddlPPN1.SelectedItem.Value
        Session("PPN2") = ddlPPN2.SelectedItem.Value
        Session("PPN3") = ddlPPN3.SelectedItem.Value

        Response.Redirect("PU_trx_RPHDet_Generate.aspx")
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCd_AddRPHLn As String = "PU_CLSTRX_RPH_LINE_ADD"
        Dim strOpCd_UpdRPHLn As String = "PU_CLSTRX_RPH_LINE_UPD"
        Dim strOpCd_GetRPHLn As String = "PU_CLSTRX_RPH_LINE_DETAILS_GET"
        Dim strOpCd_AddRPH As String = "PU_CLSTRX_RPH_ADD"
        Dim strOpCd_UpdRPH As String = "PU_CLSTRX_RPH_UPD"

        Dim strOppCd_GetID As String = "PU_CLSTRX_SYS_ID_GET"
        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "RPH"
        Dim strHistYear As String = ""
        Dim blnIsDetail As Boolean = True
        Dim objPUDs As Object
        Dim objAutoNum As New Object

        Dim objRPHId As New Object()
        Dim objRPHLnId As New Object()
        Dim strRPHId As String
        Dim strSuppCode1 As String = txtSuppCode1.Text
        Dim strSuppCode2 As String = txtSuppCode2.Text
        Dim strSuppCode3 As String = txtSuppCode3.Text
        Dim strPRId As String
        Dim strPRLocCode As String = lblPRLocCode.Text
        Dim strPRRefId As String = txtPRRefId.Text
        Dim strPRRefLocCode As String = ddlPRRefLocCode.SelectedItem.Value
        Dim strItemCode As String = txtItemCode.Text 'ddlItemCode.SelectedItem.Value


        Dim strQtyOrder1 As Double = lCDbl(txtQtyOrder1.Text)
        Dim strCost1 As Double = lCDbl(txtCost1.Text)
        Dim strTtlCost1 As Double = lCDbl(txtTtlCost1.Text)

        Dim strQtyOrder2 As Double = lCDbl(txtQtyOrder2.Text)
        Dim strCost2 As Double = lCDbl(txtCost2.Text)
        Dim strTtlCost2 As Double = lCDbl(txtTtlCost2.Text)

        Dim strQtyOrder3 As Double = lCDbl(txtQtyOrder3.Text)
        Dim strCost3 As Double = lCDbl(txtCost3.Text)
        Dim strTtlCost3 As Double = lCDbl(txtTtlCost3.Text)

        Dim strRemark As String = Trim(Request.Form("txtRemark"))
        Dim strParam As String = ""
        Dim strParamName As String = ""

        Dim intErrNo As Integer
        Dim blnUpdate As Boolean
        Dim intPRIDInd As Integer
        Dim intPRRefIDInd As Integer
        Dim intPRRefLocCode As Integer
        Dim strNewIDFormat As String
        Dim strFSupp1 As String
        Dim strFSupp2 As String
        Dim strFSupp3 As String
        Dim DecQtyOrder As Decimal
        Dim strPPN As String
        Dim strDeptCode As String = Trim(ddlDeptCode.SelectedItem.Value)
        Dim strPBBKB1 As Double = lCDbl(txtPBBKB1.Text)
        Dim strPBBKB2 As Double = lCDbl(txtPBBKB2.Text)
        Dim strPBBKB3 As Double = lCDbl(txtPBBKB3.Text)

        Dim strPBBKBRate1 As Double = lCDbl(txtPBBKBRate1.Text)
        Dim strPBBKBRate2 As Double = lCDbl(txtPBBKBRate2.Text)
        Dim strPBBKBRate3 As Double = lCDbl(txtPBBKBRate3.Text)

        Dim strAddNote As String = Trim(txtAddNote.Text)
        Dim strTransporter As String = Trim(txttransporterCode1.Text)
        Dim strAmtTransportFee As Double = lCDbl(txtAmtTransportFee.Text)
        Dim strTransporter2 As String = Trim(txttransporter2.Text)
        Dim strAmtTransportFee2 As Double = lCDbl(txtAmtTransportFee2.Text)
        Dim strTransporter3 As String = Trim(txttransporter3.Text)
        Dim strAmtTransportFee3 As Double = lCDbl(txtAmtTransportFee3.Text)

        Dim strDiscount1 As Double = lCDbl(txtDiscount1.Text)
        Dim strDiscount2 As Double = lCDbl(txtDiscount2.Text)
        Dim strDiscount3 As Double = lCDbl(txtDiscount3.Text)
        Dim strPPN221 As Double = lCDbl(txtPPN221.Text)
        Dim strPPN222 As Double = lCDbl(txtPPN222.Text)
        Dim strPPN223 As Double = lCDbl(txtPPN223.Text)
        Dim strAddDiscount As String = Trim(txtAddDisc.Text)
        Dim SelectedPOIssued As String = ddlPOIssued.SelectedItem.Value
        Dim strPOIssued As String
        Dim strDate As String = Date_Validation(txtRPHDate.Text, False)
        Dim strPRLocation As String

        Dim nPPNAmount1 As Double
        Dim nPPNAmount2 As Double
        Dim nPPNAmount3 As Double

        Dim nPPNAmounTrans1 As Double
        Dim nPPNAmountTrans2 As Double
        Dim nPPNAmountTrans3 As Double

        Dim StrNoUrut As String = ""

        If strDeptCode = "" Then
            lblDeptCode.Visible = True
            Exit Sub
        End If

        If DDLLocPenyerahan.SelectedItem.Value = "" Then
            lblTptPenyerahan.Visible = True
            DDLLocPenyerahan.Focus()
            Exit Sub
        Else
            lblTptPenyerahan.Visible = False
        End If

        If SelectedPOIssued = "" Then
            lblPOIssued.Visible = True
            Exit Sub
        End If

        If chkCentralized.Checked = True Then
            strPRId = Trim(txtPRID_Plmph.Text)
            If strPRId = "" Then
                lblErrPRID.Visible = True
                Exit Sub
            End If
        Else
            strPRId = Trim(txtPRID.Text)
            If strPRId = "" Then
                lblErrTxtPRID.Visible = True
                Exit Sub
            End If
        End If

        If hidOrgQtyOrder.Value = "" Then
            hidOrgQtyOrder.Value = 0
        End If

        If checkSupplier() = True Then
            Exit Sub
        End If

        strCurrency = ddlCurrency.SelectedItem.Value
        strExRate = Trim(txtExRate.Text)

        lblCheckBoxError.Visible = False

        DecQtyOrder = CDec(strQtyOrder1) + CDec(strQtyOrder2) + CDec(strQtyOrder3)

        If lblRPHType.Text = objPU.EnumRPHType.Stock Or lblRPHType.Text = objPU.EnumRPHType.FixedAsset Then

        End If

        If strItemCode = "" Then
            lblErrItem.Visible = True
            Exit Sub
        End If

        If strSuppCode1 = "" And strSuppCode2 = "" And strSuppCode3 = "" Then
            UserMsgBox(Me, "Please Select Supplier Name !!!")
            'lblErrSuppCode1.Visible = True
            Exit Sub
        End If
        If strAmtTransportFee <> "0" And strTransporter = "" Then
            lblErrTransporter.Visible = True
            Exit Sub
        End If

        intPRIDInd = IIf(strPRId = "", 0, 1)
        intPRRefIDInd = IIf(strPRRefId = "", 0, 1)
        intPRRefLocCode = IIf(strPRRefLocCode = "", 0, 1)

        If (intPRRefIDInd + intPRRefLocCode) = 1 Then
            lblErrRef.Visible = True
            Exit Sub
        End If

        If strItemCode = "" Then
            lblErrItem.Visible = True
            lblErrItemExist.Visible = True
            Exit Sub
        Else
            If InStr(strItemCode, ITEM_PART_SEPERATOR) <> 0 Then
                strItemCode = Trim(Mid(strItemCode, 1, InStr(strItemCode, ITEM_PART_SEPERATOR) - 1))
            End If
        End If


        If (lCDbl(txtQtyOrder1.Text) > lCDbl(txtPrQtyOrder.Text)) And Len(txtSuppCode1.Text) > 0 Then
            UserMsgBox(Me, "Please Check Qty Order Supplier 1 " & vbCrLf & " Qty Order > Qty PR  !!!")
            Exit Sub
        End If

        If (lCDbl(txtQtyOrder2.Text) > lCDbl(txtPrQtyOrder.Text)) And Len(txtSuppCode2.Text) > 0 Then
            UserMsgBox(Me, "Please Check Qty Order Supplier 2 " & vbCrLf & " Qty Order > Qty PR  !!!")
            Exit Sub
        End If

        If (lCDbl(txtQtyOrder3.Text)) > lCDbl(txtPrQtyOrder.Text) And Len(txtSuppCode3.Text) > 0 Then
            UserMsgBox(Me, "Please Check Qty Order Supplier 3 " & vbCrLf & " Qty Order > Qty PR  !!!")
            Exit Sub
        End If


        If Len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
        End If

        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|TIPE"
        strParam = strAccMonth & "|" & _
                    strAccYear & "|" & _
                    strLocation & "|" & _
                    "RPH"
        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOppCd_GetID, _
                                            strParamName, _
                                            strParam, _
                                            objAutoNum)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If objAutoNum.Tables(0).Rows.Count > 0 Then
            StrNoUrut = objAutoNum.Tables(0).Rows(0).Item("NewNo").Trim()
        End If

        If ChkSupp1.Checked = True Then
            strFSupp1 = "1"
        Else
            strFSupp1 = "2"
        End If
        If ChkSupp2.Checked = True Then
            strFSupp2 = "1"
        Else
            strFSupp2 = "2"
        End If
        If ChkSupp3.Checked = True Then
            strFSupp3 = "1"
        Else
            strFSupp3 = "2"
        End If

        If ChkPPN.Checked = True Then
            strPPN = "1"
        Else
            strPPN = "2"
        End If

        Select Case CInt(SelectedPOIssued)
            Case objPU.EnumPOIssued.JKT
                strPOIssued = "J"
            Case objPU.EnumPOIssued.PKU
                strPOIssued = "P"
            Case objPU.EnumPOIssued.LMP
                strPOIssued = "L"
            Case objPU.EnumPOIssued.PLM
                strPOIssued = "P"
            Case objPU.EnumPOIssued.BKL
                strPOIssued = "B"
            Case objPU.EnumPOIssued.LOK
                strPOIssued = "L"
            Case objPU.EnumPOIssued.MDN
                strPOIssued = "M"
            Case objPU.EnumPOIssued.JMB
                strPOIssued = "J"
            Case objPU.EnumPOIssued.PDG
                strPOIssued = "P"
            Case objPU.EnumPOIssued.ACH
                strPOIssued = "A"
            Case objPU.EnumPOIssued.PON
                strPOIssued = "P"
            Case objPU.EnumPOIssued.SAM
                strPOIssued = "S"
            Case objPU.EnumPOIssued.PLK
                strPOIssued = "P"
            Case objPU.EnumPOIssued.BJR
                strPOIssued = "B"
        End Select

        If Month(strDate) < strAccMonth And Year(strDate) <= strAccYear Then
            lblErrRPHDate.Visible = True
            lblErrRPHDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        strPRLocation = Mid(Trim(strPRId), 9, 3) 'if online

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If Len(lblRPHId.Text) = 0 Then
            '            strNewIDFormat = "RPH" & "/" & strCompany & "/" & strLocation & "/" & strPOIssued & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & StrNoUrut
            strNewIDFormat = StrNoUrut & "/" & "DTH" & "/" & strCompany & "/" & strlocation & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & strAccYear
        Else
            strNewIDFormat = lblRPHId.Text.Trim
        End If

        nPPNAmount1 = 0
        nPPNAmount2 = 0
        nPPNAmount3 = 0

        nPPNAmounTrans1 = 0
        nPPNAmountTrans2 = 0
        nPPNAmountTrans3 = 0

        strTtlCost1 = Round((strQtyOrder1 * strCost1), CInt(Session("SS_ROUNDNO"))) '- Divide((strCost1 * strQtyOrder1) * strDiscount1, 100)
        strTtlCost2 = Round((strQtyOrder2 * strCost2), CInt(Session("SS_ROUNDNO"))) '- Divide((strCost2 * strQtyOrder2) * strDiscount2, 100)
        strTtlCost3 = Round((strQtyOrder3 * strCost3), CInt(Session("SS_ROUNDNO"))) '- Divide((strCost3 * strQtyOrder3) * strDiscount3, 100)

        If ddlPPN1.SelectedItem.Value = "1" Then
            'If RTrim(strSuppCode1) = RTrim(strTransporter) Then
            '    nPPNAmount1 = Round(Divide(Session("SS_PPNRATE") * (strTtlCost1 + strAmtTransportFee), 100), 0)
            'Else
            nPPNAmount1 = Round(Divide(Session("SS_PPNRATE") * (strTtlCost1), 100), 0)
            'End If
        Else
        nPPNAmount1 = 0
        End If

        If ddlPPN2.SelectedItem.Value = "1" Then
            'If RTrim(strSuppCode2) = RTrim(strTransporter2) Then
            '    nPPNAmount2 = Round(Divide(Session("SS_PPNRATE") * (strTtlCost2 + strAmtTransportFee2), 100), 0)
            'Else
            nPPNAmount2 = Round(Divide(Session("SS_PPNRATE") * (strTtlCost2), 100), 0)
            'End If
        Else
        nPPNAmount2 = 0
        End If

        If ddlPPN3.SelectedItem.Value = "1" Then
            'If RTrim(strSuppCode3) = RTrim(strTransporter3) Then
            '    nPPNAmount3 = Round(Divide(Session("SS_PPNRATE") * (strTtlCost3 + strAmtTransportFee3), 100), 0)
            'Else
            nPPNAmount3 = Round(Divide(Session("SS_PPNRATE") * (strTtlCost3), 100), 0)
            'End If
        Else
        nPPNAmount3 = 0
        End If

        If DDLPPNTr1.SelectedItem.Value = "1" Then
            'If RTrim(strSuppCode1) <> RTrim(strTransporter) Then
            nPPNAmounTrans1 = Round(Divide(Session("SS_PPNRATE") * strAmtTransportFee, 100), CInt(Session("SS_ROUNDNO")))
            'Else
            'nPPNAmounTrans1 = Round(Divide(Session("SS_PPNRATE"), 100), CInt(Session("SS_ROUNDNO")))
            'End If
        Else
            nPPNAmounTrans1 = 0
        End If

        If DDLPPNTr2.SelectedItem.Value = "1" Then
            'If RTrim(strSuppCode2) <> RTrim(strTransporter2) Then
            nPPNAmountTrans2 = Round(Divide(Session("SS_PPNRATE") * strAmtTransportFee2, 100), CInt(Session("SS_ROUNDNO")))
            'Else
            '    nPPNAmountTrans2 = Round(Divide(Session("SS_PPNRATE"), 100), CInt(Session("SS_ROUNDNO")))
            'End If
        Else
            nPPNAmountTrans2 = 0
        End If

        If DDLPPNTr3.SelectedItem.Value = "1" Then
            'If RTrim(strSuppCode3) <> RTrim(strTransporter3) Then
            nPPNAmountTrans3 = Round(Divide(Session("SS_PPNRATE") * strAmtTransportFee3, 100), CInt(Session("SS_ROUNDNO")))
            'Else
            '    nPPNAmountTrans3 = Round(Divide(Session("SS_PPNRATE"), 100), CInt(Session("SS_ROUNDNO")))
            'End If
        Else
            nPPNAmountTrans3 = 0
        End If


        ''detail
        Dim StrRPHLnID As String

        StrRPHLnID = ""
        If lblRphLNID.Text.Trim <> "" Then
            StrRPHLnID = lblRphLNID.Text
        Else
            StrRPHLnID = "RPHLN" & Right(RTrim(strAccYear), 2)
        End If

        strRPHId = strNewIDFormat
        strParamName = "RPHLNID|RPHID|PRID|PRLNID|PRLOCCODE|PRREFID|PRREFLOCCODE|ITEMCODE|" & _
                        "SUPPCODE1|QTYORDER1|COST1|NETAMOUNT1|PPNAMOUNT1|FSUPP1|" & _
                        "SUPPCODE2|QTYORDER2|COST2|NETAMOUNT2|PPNAMOUNT2|FSUPP2|" & _
                        "SUPPCODE3|QTYORDER3|COST3|NETAMOUNT3|PPNAMOUNT3|FSUPP3|" & _
                        "PPN|PPN2|PPN3|STATUS|PBBKB1|PBBKB2|PBBKB3|PPNPSN1|PPNPSN2|PPNPSN3|" & _
                        "PBBKBRATE1|PBBKBRATE2|PBBKBRATE3|ACCYEAR|ADDITIONALNOTE|" & _
                        "AMOUNTCURRENCY1|AMOUNTCURRENCY2|AMOUNTCURRENCY3|" & _
                        "TRANSPORTER|AMTTRANSPORTFEE|PPNTR1|NOMPPNTR1|" & _
                        "TRANSPORTER2|AMTTRANSPORTFEE2|PPNTR2|NOMPPNTR2|" & _
                        "TRANSPORTER3|AMTTRANSPORTFEE3|PPNTR3|NOMPPNTR3|" & _
                        "DISCOUNT1|DISCOUNT2|DISCOUNT3|PPN221|PPN222|PPN223|LOCCODE|EXRATE|SUPNOTE1|SUPNOTE2|SUPNOTE3|REPITEM"
        strParam = StrRPHLnID & "|" & _
                   strRPHId & "|" & _
                   strPRId & "|" & _
                   txtPRRefId.Text.Trim & "|" & _
                   lblPRLocCode.Text & "|" & _
                   strPRRefId & "|" & _
                   strPRRefLocCode & "|" & _
                   strItemCode & "|" & _
                   strSuppCode1 & "|" & _
                   strQtyOrder1 & "|" & _
                   strCost1 & "|" & _
                   strTtlCost1 & "|" & _
                   nPPNAmount1 & "|" & _
                   strFSupp1 & "|" & _
                   strSuppCode2 & "|" & _
                   strQtyOrder2 & "|" & _
                   strCost2 & "|" & _
                   strTtlCost2 & "|" & _
                   nPPNAmount2 & "|" & _
                   strFSupp2 & "|" & _
                   strSuppCode3 & "|" & _
                   strQtyOrder3 & "|" & _
                   strCost3 & "|" & _
                   strTtlCost3 & "|" & _
                   nPPNAmount3 & "|" & _
                   strFSupp3 & "|" & _
                   IIf(ddlPPN1.SelectedItem.Value = "1", objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                   IIf(ddlPPN2.SelectedItem.Value = "1", objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                   IIf(ddlPPN3.SelectedItem.Value = "1", objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                   "1" & "|" & _
                   strPBBKB1 & "|" & _
                   strPBBKB2 & "|" & _
                   strPBBKB3 & "|" & _
                   IIf(ddlPPN1.SelectedItem.Value = "1", Session("SS_PPNRATE"), 0) & "|" & _
                   IIf(ddlPPN2.SelectedItem.Value = "1", Session("SS_PPNRATE"), 0) & "|" & _
                   IIf(ddlPPN3.SelectedItem.Value = "1", Session("SS_PPNRATE"), 0) & "|" & _
                   strPBBKBRate1 & "|" & _
                   strPBBKBRate2 & "|" & _
                   strPBBKBRate3 & "|" & _
                   strAccYear & "|" & _
                   Replace(strAddNote, "'", "''") & "|" & _
                   strCost1 & "|" & _
                   strCost2 & "|" & _
                   strCost3 & "|" & _
                   strTransporter & "|" & strAmtTransportFee & "|" & _
                   IIf(DDLPPNTr1.SelectedItem.Value = "1", objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                   nPPNAmounTrans1 & "|" & _
                   strTransporter2 & "|" & strAmtTransportFee2 & "|" & _
                   IIf(DDLPPNTr2.SelectedItem.Value = "1", objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                   nPPNAmountTrans2 & "|" & _
                   strTransporter3 & "|" & strAmtTransportFee3 & "|" & _
                   IIf(DDLPPNTr3.SelectedItem.Value = "1", objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                   nPPNAmountTrans3 & "|" & _
                   strDiscount1 & "|" & _
                   strDiscount2 & "|" & _
                   strDiscount3 & "|" & _
                   strPPN221 & "|" & _
                   strPPN222 & "|" & _
                   strPPN223 & "|" & _
                   strLocation & "|" & _
                   txtExRate.Text & "|" & _
                   Replace(txtsuppNote1.Text, "'", "''") & "|" & _
                   Replace(txtsuppNote2.Text, "'", "''") & "|" & _
                   Replace(txtsuppNote3.Text, "'", "''") & "|" & _
                   txtItemCode_Rep.Text

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd_AddRPHLn, _
                                                    strParamName, _
                                                    strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try


        ''header
        strParamName = "RPHID|LOCCODE|REMARK|ACCMONTH|ACCYEAR|STATUS|CREATEDATE|UPDATEDATE|PRINTDATE|UPDATEID|RPHTYPE|DEPTCODE|POISSUED|PPN|CENTRALIZED|CURRENCYCODE|EXCHANGERATE|ADDDISCOUNT|USERPO|LOCPENYERAHAN|ISPRINT"
        strParam = strRPHId & "|" & _
                   strLocation & "|" & _
                   Replace(strRemark, "'", "''") & "|" & _
                   strAccMonth & "|" & _
                   strAccYear & "|" & _
                   "1" & "|" & _
                   strDate & "|" & _
                   strDate & "|" & _
                   "2000-01-01" & "|" & _
                   strUserId & "|" & _
                   lblRPHType.Text & "|" & _
                   strDeptCode & "|" & _
                   SelectedPOIssued & "|" & _
                   IIf(ChkPPN.Checked = True, Session("SS_PPNRATE"), 0) & "|" & _
                   IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                   strCurrency & "|" & _
                   strExRate & "|" & _
                   strAddDiscount & "|" & _
                   strUserId & "|" & _
                   DDLLocPenyerahan.SelectedItem.Value & "|" & _
                   "0"


        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd_AddRPH, _
                                                    strParamName, _
                                                    strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        'Try
        '    intErrNo = objPU.mtdAddRPHLn(strOpCd_AddRPHLn, _
        '                                strOpCd_UpdRPHLn, _
        '                                strOpCd_AddRPH, _
        '                                strOpCd_GetRPHLn, _
        '                                strOpCd_UpdRPH, _
        '                                strOppCd, _
        '                                strOppCd_Back, _
        '                                strCompany, _
        '                                strLocation, _
        '                                strUserId, _
        '                                strParam, _
        '                                blnUpdate, _
        '                                objRPHId, _
        '                                objRPHLnId, _
        '                                "RPH", _
        '                                "RPHLN")
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_NEW_POLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        'End Try



        If intErrNo = 3 Then
            lblErrItemExist.Visible = True
            Exit Sub
        End If

        lClearDetail()
        onLoad_DisplayRPH(strRPHId)
        onLoad_DisplayRPHLn(strRPHId)
    End Sub

    Sub btnEdit_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddRPH As String = "PU_CLSTRX_RPH_EDIT"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParam As String

        ''header
        strParamName = "RPHID|STATUS|USRID"
        strParam = lblRPHId.Text & "|" & _
                   "1" & "|" & _
                   strUserId
                   
        Try
            If Len(lblRPHId.Text) > 0 Then
                Try
                    intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd_AddRPH, _
                                                            strParamName, _
                                                            strParam)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
                End Try

                UserMsgBox(Me, "Edit Sucsess !!!")

                onLoad_DisplayRPH(lblRPHId.Text)
                onLoad_DisplayRPHLn(lblRPHId.Text)
            End If

        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try
    End Sub

    Sub BtnSup1Find_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtSuppName1.Text.Trim = "" Then
            lblErrSuppCode1.Visible = True
            lblErrSuppCode1.Text = "Please Input Supplier Name To Find Record"
            Exit Sub
        Else
            lblErrSuppCode1.Visible = False
        End If

        dgLine.Visible = True
        BindSup1(txtSuppName1.Text)
    End Sub

    Sub BtnSup2Find_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtSuppName2.Text.Trim = "" Then
            lblErrSuppCode2.Visible = True
            lblErrSuppCode2.Text = "Please Input Supplier Name To Find Record"
            Exit Sub
        Else
            lblErrSuppCode2.Visible = False
        End If

        dgLineSup2.Visible = True
        BindSup2(txtSuppName2.Text)
    End Sub

    Sub BtnSup3Find_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtSuppName3.Text.Trim = "" Then
            lblErrSuppCode3.Visible = True
            lblErrSuppCode3.Text = "Please Input Supplier Name To Find Record"
            Exit Sub
        Else
            lblErrSuppCode3.Visible = False
        End If

        dgLineSup3.Visible = True
        BindSup3(txtSuppName3.Text)
    End Sub


    Sub BtnTrans1Find_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txttransporterName1.Text.Trim = "" Then
            Exit Sub
        End If

        dgLineTrans1.Visible = True
        BindTrans1(txttransporterName1.Text)
    End Sub

    Sub BtnTrans2Find_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txttransporterName2.Text.Trim = "" Then
            Exit Sub
        End If

        dgLineTrans2.Visible = True
        BindTrans2(txttransporterName2.Text)
    End Sub

    Sub BtnTrans3Find_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txttransporterName3.Text.Trim = "" Then
            Exit Sub
        End If

        dgLineTrans3.Visible = True
        BindTrans3(txttransporterName3.Text)
    End Sub

    Sub BtnSup1Close_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLine.DataSource = Nothing
        dgLine.Visible = False
    End Sub

    Sub BtnSup2Close_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLineSup2.DataSource = Nothing
        dgLineSup2.Visible = False
    End Sub

    Sub BtnSup3Close_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLineSup3.DataSource = Nothing
        dgLineSup3.Visible = False
    End Sub

    Sub BtnTrans1Close_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLineTrans1.DataSource = Nothing
        dgLineTrans1.Visible = False
    End Sub

    Sub BtnTrans2Close_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLineTrans2.DataSource = Nothing
        dgLineTrans2.Visible = False
    End Sub

    Sub BtnTrans3Close_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLineTrans3.DataSource = Nothing
        dgLineTrans3.Visible = False
    End Sub

    Sub BtnCalcSup1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtTtlCost1.Text = Round((lCDbl(txtQtyOrder1.Text) * lCDbl(txtCost1.Text)), CInt(Session("SS_ROUNDNO"))) - Round(Divide((lCDbl(txtCost1.Text) * lCDbl(txtQtyOrder1.Text)) * lCDbl(txtDiscount1.Text), 100), CInt(Session("SS_ROUNDNO")))
    End Sub

    Sub BtnCalcSup2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtTtlCost2.Text = Round((lCDbl(txtQtyOrder2.Text) * lCDbl(txtCost2.Text)), CInt(Session("SS_ROUNDNO"))) - Round(Divide((lCDbl(txtCost2.Text) * lCDbl(txtQtyOrder2.Text)) * lCDbl(txtDiscount2.Text), 100), CInt(Session("SS_ROUNDNO")))
    End Sub

    Sub BtnCalcSup3_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtTtlCost3.Text = Round((lCDbl(txtQtyOrder3.Text) * lCDbl(txtCost3.Text)), CInt(Session("SS_ROUNDNO"))) - Round(Divide((lCDbl(txtCost3.Text) * lCDbl(txtQtyOrder3.Text)) * lCDbl(txtDiscount3.Text), 100), CInt(Session("SS_ROUNDNO")))
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "PU_CLSTRX_RPH_LINE_UPD_SPL"
        Dim strOpCd_AddRPH As String = "PU_CLSTRX_RPH_ADD"
        Dim strOpCd_UpdRPH As String = "PU_CLSTRX_RPH_UPD"
        Dim strOppCd_GetID As String = "PU_CLSTRX_PO_GETID"
        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "RPH"
        Dim strHistYear As String = ""
        Dim blnIsDetail As Boolean = True
        Dim objPUDs As Object
        Dim objRPHId As New Object()
        Dim strSuppCode1 As String = txtSuppCode1.Text
        Dim strSuppCode2 As String = txtSuppCode2.Text
        Dim strSuppCode3 As String = txtSuppCode3.Text
        Dim strRemark As String = Trim(Request.Form("txtRemark"))
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strNewIDFormat As String
        Dim strRPHIssued As String
        Dim strPPN As String
        Dim strDeptCode As String = Trim(ddlDeptCode.SelectedItem.Value)
        Dim strPOIssued As String = Trim(ddlPOIssued.SelectedItem.Value)
        Dim strPRID As String = IIf(chkCentralized.Checked = True, Trim(txtPRID_Plmph.Text), Trim(txtPRID.Text))
        Dim strItemCode As String = txtItemCode.Text 'ddlItemCode.SelectedItem.Value
        Dim strAddDiscount As String = Trim(txtAddDisc.Text)
        Dim strDate As String = Date_Validation(txtRPHDate.Text, False)
        Dim intCnt As Integer


        Dim strFSupp1 As String
        Dim strFSupp2 As String
        Dim strFSupp3 As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim strRPHLnId As String

        If Len(lblRPHId.Text) = 0 Then
            UserMsgBox(Me, "No Record Found !!!")
            Exit Sub
        End If

        If strDeptCode = "" Then
            lblDeptCode.Visible = True
            Exit Sub
        End If

        If strPOIssued = "" Then
            lblPOIssued.Visible = True
            Exit Sub
        End If

        If DDLLocPenyerahan.SelectedItem.Value = "" Then
            lblTptPenyerahan.Visible = True
            DDLLocPenyerahan.Focus()
            Exit Sub
        Else
            lblTptPenyerahan.Visible = False
        End If

        strCurrency = ddlCurrency.SelectedItem.Value
        strExRate = Trim(txtExRate.Text)

        If Len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
        End If



        strParam = "where phyyear = '" & Right(Trim(strPhyYear), 2) & "' and tran_prefix = 'RPH'" & "|"

        Try
            intErrNo = objPU.mtdGetNewPOIDFormat(strOppCd_GetID, _
                                                   strParam, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strLocation, _
                                                   objPUDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try


        If objPUDs.Tables(0).Rows.Count > 0 Then
            strNewYear = ""
        Else
            strHistYear = Right(strLastPhyYear, 2)
            strNewYear = "1"
        End If

        If ChkPPN.Checked = True Then
            strPPN = "1"
        Else
            strPPN = "2"
        End If



        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        strNewIDFormat = "DTH" & "/" & strCompany & "/" & strLocation & "/" & strPOIssued & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        strParamName = "RPHID|LOCCODE|REMARK|ACCMONTH|ACCYEAR|STATUS|CREATEDATE|UPDATEDATE|PRINTDATE|UPDATEID|RPHTYPE|DEPTCODE|POISSUED|PPN|CENTRALIZED|CURRENCYCODE|EXCHANGERATE|ADDDISCOUNT|USERPO|LOCPENYERAHAN|ISPRINT"
        strParam = lblRPHId.Text & "|" & _
                   strLocation & "|" & _
                  Replace(strRemark, "'", "''") & "|" & _
                   strAccMonth & "|" & _
                   strAccYear & "|" & _
                   "1" & "|" & _
                   strDate & "|" & _
                   strDate & "|" & _
                   "2000-01-01" & "|" & _
                   strUserId & "|" & _
                   lblRPHType.Text & "|" & _
                   strDeptCode & "|" & _
                   strPOIssued & "|" & _
                   IIf(ChkPPN.Checked = True, Session("SS_PPNRATE"), 0) & "|" & _
                   IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                   strCurrency & "|" & _
                   strExRate & "|" & _
                   strAddDiscount & "|" & _
                   strUserId & "|" & _
                   DDLLocPenyerahan.SelectedItem.Value & "|" & _
                   IIf(chkPrinted.Checked = True, "1", "0")



        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd_AddRPH, _
                                                    strParamName, _
                                                    strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        'strParam = lblRPHId.Text & "|" & _
        '                    IIf(lblTotalAmountAll.Text = "", 0, lblTotalAmountAll.Text) & "|" & _
        '                    strRemark & "|" & _
        '                    lblRPHType.Text & "|" & _
        '                    strAccMonth & "|" & _
        '                    strAccYear & "|" & _
        '                    objPU.EnumRPHStatus.Active & "|" & _
        '                    strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
        '                    strDeptCode & "|" & _
        '                    strPOIssued & "|" & _
        '                    strPPN & "|" & IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
        '                    strCurrency & "|" & strExRate & "|" & strAddDiscount & "|" & DDLLocPenyerahan.SelectedItem.Value

        'Try
        '    intErrNo = objPU.mtdUpdRPH(strOpCd_AddRPH, _
        '                            strOpCd_UpdRPH, _
        '                            strOppCd, _
        '                            strOppCd_Back, _
        '                            strCompany, _
        '                            strLocation, _
        '                            strUserId, _
        '                            strParam, _
        '                            "RPH", _
        '                            objRPHId)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_SAVE_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_rphlist.aspx")
        'End Try

        For intCnt = 0 To dgRPHDet.Items.Count - 1

            If Len(CType(dgRPHDet.Items(intCnt).FindControl("lblItemCode"), Label).Text) > 0 Then
                strRPHLnId = CType(dgRPHDet.Items(intCnt).FindControl("lblRPHLnDetId"), Label).Text

                If CType(dgRPHDet.Items(intCnt).FindControl("chkDetSup1"), CheckBox).Checked = True Then
                    strFSupp1 = "1"
                Else
                    strFSupp1 = "2"
                End If

                If CType(dgRPHDet.Items(intCnt).FindControl("chkDetSup2"), CheckBox).Checked = True Then
                    strFSupp2 = "1"
                Else
                    strFSupp2 = "2"
                End If

                If CType(dgRPHDet.Items(intCnt).FindControl("chkDetSup3"), CheckBox).Checked = True Then
                    strFSupp3 = "1"
                Else
                    strFSupp3 = "2"
                End If

                strParamName = "RPHID|RPHLNID|FSUPP1|FSUPP2|FSUPP3|UPDATEID|LOCCODE"
                strParamValue = lblRPHId.Text & "|" & strRPHLnId & "|" & strFSupp1 & "|" & strFSupp2 & "|" & strFSupp3 & "|" & _
                                strUserId & "|" & strLocation

                Try
                    intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                            strParamName, _
                                                            strParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
                End Try
            End If
        Next

        UserMsgBox(Me, "Save Sucsess !!!")

        'lblStatus.Text = objPU.mtdGetRPHStatus(objPU.EnumRPHStatus.Active)
        txtRemark.Text = strRemark
        onLoad_DisplayRPH(lblRPHId.Text)
        onLoad_DisplayRPHLn(lblRPHId.Text)
    End Sub

    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "PU_CLSTRX_RPH_LINE_UPD_SPL"
        Dim strOpCd_Confirm As String = "PU_CLSTRX_RPH_CONFIRM"
        Dim strOpCd_GetConfirm As String = "PU_CLSTRX_RPH_CONFIRM_GET"
        Dim strOpCd_UpdRPH As String = "PU_CLSTRX_RPH_UPD"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPoLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdCost As String = "PU_CLSTRX_COST_UPD"
        Dim strOpCd_CheckID As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_GetID As String = "PU_CLSTRX_PO_GETID"
        Dim strParam As String = ""
        Dim objRPHGet As New Object()
        Dim objPUDs As New Object()
        Dim strHistYear As String
        Dim strNewYear As String
        Dim strPOId As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim dsDataset As DataSet
        Dim strOpCode As String
        Dim strDate As String = Date_Validation(txtRPHDate.Text, False)

        Dim strFSupp1 As String
        Dim strFSupp2 As String
        Dim strFSupp3 As String
        Dim strRPHLnId As String

        strOpCode = "PU_CLSTRX_RPH_GET_STATUS"

        If DDLLocPenyerahan.SelectedItem.Value = "" Then
            lblTptPenyerahan.Visible = True
            DDLLocPenyerahan.Focus()
            Exit Sub
        Else
            lblTptPenyerahan.Visible = False
        End If



        ''confirm supplier yang dicentang
        For intCnt = 0 To dgRPHDet.Items.Count - 1
            If Len(CType(dgRPHDet.Items(intCnt).FindControl("lblItemCode"), Label).Text) > 0 Then
                strRPHLnId = CType(dgRPHDet.Items(intCnt).FindControl("lblRPHLnDetId"), Label).Text

                If CType(dgRPHDet.Items(intCnt).FindControl("chkDetSup1"), CheckBox).Checked = True Then
                    strFSupp1 = "1"
                Else
                    strFSupp1 = "2"
                End If

                If CType(dgRPHDet.Items(intCnt).FindControl("chkDetSup2"), CheckBox).Checked = True Then
                    strFSupp2 = "1"
                Else
                    strFSupp2 = "2"
                End If

                If CType(dgRPHDet.Items(intCnt).FindControl("chkDetSup3"), CheckBox).Checked = True Then
                    strFSupp3 = "1"
                Else
                    strFSupp3 = "2"
                End If

                strParamName = "RPHID|RPHLNID|FSUPP1|FSUPP2|FSUPP3|UPDATEID|LOCCODE"
                strParamValue = lblRPHId.Text & "|" & strRPHLnId & "|" & strFSupp1 & "|" & strFSupp2 & "|" & strFSupp3 & "|" & _
                                strUserId & "|" & strLocation

                Try
                    intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                            strParamName, _
                                                            strParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
                End Try
            End If
        Next



        strParamName = "RPHID|LOCCODE"
        strParamValue = Trim(lblRPHId.Text) & "|" & strLocation

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsDataset)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_GET_NURSERY_ITEM_BY_BATCH&errmesg=" & Exp.ToString() & "&redirect=IN/Trx/IN_Trx_StockIssue_Details.aspx")
        End Try

        If dsDataset.Tables(0).Rows.Count > 0 Then
            lblErrSelected.Visible = True
            Exit Sub
        End If

        If lblRPHType.Text = objPU.EnumRPHType.Stock Then
            strRPHType = objIN.EnumPurReqDocType.StockPR & "','" & objIN.EnumPurReqDocType.WorkshopPR
        Else
            strRPHType = lblRPHType.Text
        End If

        strParam = "where phyyear = '" & Right(Trim(strPhyYear), 2) & "' and tran_prefix = 'PO'" & "|"
        Try
            intErrNo = objPU.mtdGetNewPOIDFormat(strOppCd_GetID, _
                                                   strParam, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strLocation, _
                                                   objPUDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try


        If objPUDs.Tables(0).Rows.Count > 0 Then
            strNewYear = ""
        Else
            strHistYear = Right(strLastPhyYear, 2)
            strNewYear = "1"

            strParam = "PO|" & Right(strPhyYear, 2)

            Try
                intErrNo = objPU.mtdCheckID(strOpCd_CheckID, strParam, objRPHGet)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CONFIRM_PO&errmesg=" & Exp.ToString() & "&redirect")
            End Try
        End If

        strParam = "where phyyear = '" & Right(Trim(strPhyYear), 2) & "' and tran_prefix = 'POL'" & "|"
        Try
            intErrNo = objPU.mtdGetNewPOIDFormat(strOppCd_GetID, _
                                                   strParam, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strLocation, _
                                                   objPUDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try


        If objPUDs.Tables(0).Rows.Count > 0 Then
            strNewYear = ""
        Else
            strHistYear = Right(strLastPhyYear, 2)
            strNewYear = "1"

            strParam = "POL|" & Right(strPhyYear, 2)

            Try
                intErrNo = objPU.mtdCheckID(strOpCd_CheckID, strParam, objRPHGet)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CONFIRM_PO&errmesg=" & Exp.ToString() & "&redirect")
            End Try
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        strPhyMonth = strAccMonth

        strParam = "Where a.rphid = '" & Trim(lblRPHId.Text) & "' And a.AccYear ='" & Trim(strAccYear) & "'" & "|" & _
                    "Where tran_prefix = 'PO' and phyyear = '" & Right(Trim(strAccYear), 2) & "'" & "|" & _
                    "Where tran_prefix = 'POL' and phyyear = '" & Right(Trim(strAccYear), 2) & "'" & "|" & _
                    strPhyMonth


        Try
            intErrNo = objPU.mtdUpdRPHConfirm(strOpCd_Confirm, strParam, objRPHGet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CONFIRM_PO&errmesg=" & Exp.ToString() & "&redirect")
        End Try

        strParam = "Where a.rphid = '" & Trim(lblRPHId.Text) & "'" & "|||"
        Try
            intErrNo = objPU.mtdUpdRPHConfirm(strOpCd_GetConfirm, strParam, objRPHGet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CONFIRM_PO&errmesg=" & Exp.ToString() & "&redirect")
        End Try

        If objRPHGet.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRPHGet.Tables(0).Rows.Count - 1
                strPOId = objRPHGet.Tables(0).Rows(intCnt).Item("POID").Trim()

                strParamName = "STRUPDATE"
                strParam = "SET STATUS='2' Where POID='" & strPOId & "' AND LocCode='" & strLocation & "' AND Status NOT IN ('4','3')"

                Try
                    intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd_UpdPO, _
                                                            strParamName, _
                                                            strParam)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
                End Try
            Next
        End If

        strParam = "Where a.rphid = '" & Trim(lblRPHId.Text) & "'" & "|||" & _
                    "Set Status = '2' Where rphid = '" & Trim(lblRPHId.Text) & "' "
        Try
            intErrNo = objPU.mtdUpdRPHConfirm(strOpCd_UpdRPH, strParam, objRPHGet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CONFIRM_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_rphlist.aspx")
        End Try

        UserMsgBox(Me, "Confirm Sucsess !!!")
        onLoad_DisplayRPH(lblRPHId.Text)
        onLoad_DisplayRPHLn(lblRPHId.Text)
        onload_PORPH(lblRPHId.Text)

        Dim strOpCodeGenSPK As String = "PU_CLSTRX_PO_GENERATE_SPK"
        Dim dsMaster As Object
        Dim poID As String = ""

        For intCnt = 0 To dgRPHPO.Items.Count - 1
            poID = CType(dgRPHPO.Items(intCnt).FindControl("lblPOID"), Label).Text
            strParamName = "POID|LOCCODE"
            strParamValue = Trim(poID) & "|" & Trim(strLocation)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCodeGenSPK, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_SPK&errmesg=" & Exp.ToString() & "&redirect=PU/trx/PU_PODET.aspx")
            End Try
        Next

    End Sub

    Sub btnVerified_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim strOppCd As String = "PU_CLSTRX_RPH_UPD"
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "STRUPDATE"
        strParamValue = "SET Status='2' Where RPHID='" & lblRPHId.Text & "'"

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOppCd, _
                                                strParamName, _
                                                strParamValue)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
            Exit Sub
        End Try

        UserMsgBox(Me, "Verified Sucsess !!!")
        onLoad_DisplayRPH(lblRPHId.Text)
        onLoad_DisplayRPHLn(lblRPHId.Text)
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim strOppCd As String = "PU_CLSTRX_RPH_UPD"
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "STRUPDATE"
        strParamValue = "SET Status='5' Where RPHID='" & lblRPHId.Text & "'"

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOppCd, _
                                                strParamName, _
                                                strParamValue)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
            Exit Sub
        End Try

        UserMsgBox(Me, "Delete Sucsess !!!")
        onLoad_DisplayRPH(lblRPHId.Text)
        onLoad_DisplayRPHLn(lblRPHId.Text)
    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddRPH As String = "PU_CLSTRX_RPH_ADD"
        Dim strOpCd_UpdRPH As String = "PU_CLSTRX_RPH_UPD"
        Dim strOppCd_GetID As String = "PU_CLSTRX_PO_GETID"
        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "RPH"
        Dim strHistYear As String = ""
        Dim blnIsDetail As Boolean = True
        Dim objPUDs As Object
        Dim objRPHId As New Object()
        Dim strSuppCode1 As String = txtSuppCode1.Text
        Dim strSuppCode2 As String = txtSuppCode2.Text
        Dim strSuppCode3 As String = txtSuppCode3.Text
        Dim strRemark As String = Trim(Request.Form("txtRemark"))
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strNewIDFormat As String
        Dim strAddDiscount As String = Trim(txtAddDisc.Text)

        If Len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
        End If

        strParam = "where phyyear = '" & Right(Trim(strPhyYear), 2) & "' and tran_prefix = 'RPH'" & "|"
        Try
            intErrNo = objPU.mtdGetNewPOIDFormat(strOppCd_GetID, _
                                                   strParam, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strLocation, _
                                                   objPUDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try


        If objPUDs.Tables(0).Rows.Count > 0 Then
            strNewYear = ""
        Else
            strHistYear = Right(strLastPhyYear, 2)
            strNewYear = "1"
        End If


        strNewIDFormat = "RPH"

        strParam = lblRPHId.Text & "|" & _
                    lblTotalAmountAll.Text & "|" & _
                    strRemark & "|" & _
                    strSelectedRPHType & "|" & _
                    strAccMonth & "|" & _
                    strAccYear & "|" & _
                    objPU.EnumRPHStatus.Active & "|" & _
                    strSelectedRPHType & "|" & _
                    strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|||" & IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                    strCurrency & "|" & strExRate & "|" & strAddDiscount


        Try
            intErrNo = objPU.mtdUpdRPH(strOpCd_AddRPH, _
                                    strOpCd_UpdRPH, _
                                    strOppCd, _
                                    strOppCd_Back, _
                                    strCompany, _
                                    strLocation, _
                                    strUserId, _
                                    strParam, _
                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.NewPurchaseOrder), _
                                    objRPHId)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_SAVE_RPH&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        txtRemark.Text = strRemark
        lblStatus.Text = objPU.mtdGetPOStatus(objPU.EnumRPHStatus.Active)
        onLoad_DisplayRPH(lblRPHId.Text)
        onLoad_DisplayRPHLn(lblRPHId.Text)
        BindPR(lblRPHId.Text)
        BindPRItem("")

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

        strUpdString = "where RPHId = '" & lblRPHId.Text & "' "
        strStatus = Trim(lblStatus.Text)
        intStatus = CInt(Trim(lblHidStatus.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strSortLine = "PU_RPHLN.RPHLnId"
        strTable = "PU_RPH"

        If intStatus = objPU.EnumRPHStatus.Confirmed Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmShare.mtdUpdPrintDate(strOpCodePrint, _
                                                           strUpdString, _
                                                           strTable, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_RPH_DETAILS_UPD_PRINTDATE&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
                End Try
                'onLoad_DisplayRPH(lblRPHId.Text)
                'onLoad_DisplayRPHLn(lblRPHId.Text)
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_RPHDet.aspx?strRPHId=" & lblRPHId.Text & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_RPHList.aspx")
    End Sub

#End Region

#Region "ONSELECT ITEM GRID"

    Sub OnSelectItem(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim CPCellCode As TableCell = e.Item.Cells(0)
        Dim CPCellName As TableCell = e.Item.Cells(1)
        Dim CPCellTerm As TableCell = e.Item.Cells(2)
        Dim CPCellPPN As TableCell = e.Item.Cells(3)
        'Dim CPCellPPN2 As TableCell = e.Item.Cells(3)
        Dim nPPN As String

        If e.Item.ItemIndex < 0 Then
            Exit Sub
        End If

        BindPPNList()
        txtSuppCode1.Text = CPCellCode.Text.Trim
        txtSuppName1.Text = CPCellName.Text.Trim
        nPPN = CPCellPPN.Text.Trim
        ddlPPN1.SelectedValue = nPPN
        txtQtyOrder1.Text = txtPrQtyOrder.Text
        txtCost1.Text = txtPrCostOrder.Text
        txtTtlCost1.Text = lCDbl(txtPrQtyOrder.Text) * lCDbl(txtPrCostOrder.Text)
        dgLine.DataSource = Nothing
        dgLine.Visible = False

    End Sub

    Sub OnSelectSup2(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim CPCellCode As TableCell = e.Item.Cells(0)
        Dim CPCellName As TableCell = e.Item.Cells(1)
        Dim CPCellTerm As TableCell = e.Item.Cells(2)
        Dim CPCellPPN As TableCell = e.Item.Cells(3)
        'Dim CPCellPPN2 As TableCell = e.Item.Cells(3)
        Dim nPPN As String

        If e.Item.ItemIndex < 0 Then
            Exit Sub
        End If

        BindPPNList()

        txtSuppCode2.Text = CPCellCode.Text.Trim
        txtSuppName2.Text = CPCellName.Text.Trim
        nPPN = CPCellPPN.Text.Trim
        ddlPPN2.SelectedValue = nPPN

        txtQtyOrder2.Text = txtPrQtyOrder.Text
        txtCost2.Text = txtPrCostOrder.Text
        txtTtlCost2.Text = lCDbl(txtPrQtyOrder.Text) * lCDbl(txtPrCostOrder.Text)

        dgLineSup2.DataSource = Nothing
        dgLineSup2.Visible = False
    End Sub

    Sub OnSelectSup3(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim CPCellCode As TableCell = e.Item.Cells(0)
        Dim CPCellName As TableCell = e.Item.Cells(1)
        Dim CPCellTerm As TableCell = e.Item.Cells(2)
        Dim CPCellPPN As TableCell = e.Item.Cells(3)
        'Dim CPCellPPN2 As TableCell = e.Item.Cells(3)
        Dim nPPN As String

        If e.Item.ItemIndex < 0 Then
            Exit Sub
        End If

        BindPPNList()

        txtSuppCode3.Text = CPCellCode.Text.Trim
        txtSuppName3.Text = CPCellName.Text.Trim

        nPPN = CPCellPPN.Text.Trim
        ddlPPN3.SelectedValue = nPPN

        txtQtyOrder3.Text = txtPrQtyOrder.Text
        txtCost3.Text = txtPrCostOrder.Text
        txtTtlCost3.Text = lCDbl(txtPrQtyOrder.Text) * lCDbl(txtPrCostOrder.Text)

        dgLineSup3.DataSource = Nothing
        dgLineSup3.Visible = False
    End Sub

    Sub OnSelectTrans1(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim CPCellCode As TableCell = e.Item.Cells(0)
        Dim CPCellName As TableCell = e.Item.Cells(1)
        Dim CPCellTerm As TableCell = e.Item.Cells(2)
        Dim CPCellPPN As TableCell = e.Item.Cells(3)
        'Dim CPCellPPN2 As TableCell = e.Item.Cells(3)
        Dim nPPN As String

        If e.Item.ItemIndex < 0 Then
            Exit Sub
        End If

        BindPPNList()

        nPPN = CPCellPPN.Text.Trim

        txttransporterCode1.Text = CPCellCode.Text.Trim
        txttransporterName1.Text = CPCellName.Text.Trim

        DDLPPNTr1.SelectedValue = nPPN
        dgLineTrans1.DataSource = Nothing
        dgLineTrans1.Visible = False
    End Sub

    Sub OnSelectTrans2(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim CPCellCode As TableCell = e.Item.Cells(0)
        Dim CPCellName As TableCell = e.Item.Cells(1)
        Dim CPCellTerm As TableCell = e.Item.Cells(2)
        Dim CPCellPPN As TableCell = e.Item.Cells(3)
        Dim nPPN As String
        'Dim CPCellPPN2 As TableCell = e.Item.Cells(3)

        If e.Item.ItemIndex < 0 Then
            Exit Sub
        End If

        BindPPNList()

        nPPN = CPCellPPN.Text.Trim

        txttransporter2.Text = CPCellCode.Text.Trim
        txttransporterName2.Text = CPCellName.Text.Trim
        DDLPPNTr2.SelectedValue = nPPN

        dgLineTrans2.DataSource = Nothing
        dgLineTrans2.Visible = False
    End Sub

    Sub OnSelectTrans3(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim CPCellCode As TableCell = e.Item.Cells(0)
        Dim CPCellName As TableCell = e.Item.Cells(1)
        Dim CPCellTerm As TableCell = e.Item.Cells(2)
        Dim CPCellPPN As TableCell = e.Item.Cells(3)
        'Dim CPCellPPN2 As TableCell = e.Item.Cells(3)
        Dim nPPN As String

        If e.Item.ItemIndex < 0 Then
            Exit Sub
        End If

        BindPPNList()

        nPPN = CPCellPPN.Text.Trim
        txttransporter3.Text = CPCellCode.Text.Trim
        txttransporterName3.Text = CPCellName.Text.Trim
        DDLPPNTr3.SelectedValue = nPPN

        dgLineTrans3.DataSource = Nothing
        dgLineTrans3.Visible = False
    End Sub

    Sub dgLine_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

#End Region

#End Region

#Region "LOCAL & PROCEDURE"

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblLocCode.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblStockItem.Text = GetCaption(objLangCap.EnumLangCap.StockItem)
        lblErrRef.Text = "Please enter PR Ref. No. and PR Ref. " & GetCaption(objLangCap.EnumLangCap.Location)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_PODET_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=pu/setup/PU_trx_POList.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = objLangCapDs.Tables(0).Rows(count).Item("TermCode") Then
                If strLocType = objAdmin.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub onLoad_DisplayRPH(ByVal pv_strRPHId As String)
        Dim strOpCd As String = "PU_CLSTRX_RPH_GET"
        Dim strOpCd1 As String = "PU_CLSTRX_RPH_GET1"
        Dim objRPHDs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String = pv_strRPHId & "|" & strLocation & "|" & lblRPHType.Text & "|||||A.RPHId|" & "||"
        Dim intCnt As Integer = 0
        Dim blnDisplayCancel As Boolean
        Dim strSuppCode1 As String
        Dim strSuppCode2 As String
        Dim strSuppCode3 As String

        Try
            intErrNo = objPU.mtdGetRPH(strOpCd1, strParam, objRPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_RPH&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_rphlist.aspx")
        End Try

        lblRPHId.Text = pv_strRPHId
        lblAccPeriod.Text = objRPHDs.Tables(0).Rows(0).Item("AccMonth") & "/" & objRPHDs.Tables(0).Rows(0).Item("AccYear")
        lblRPHType.Text = objRPHDs.Tables(0).Rows(0).Item("RPHType").Trim()

        If Trim(objRPHDs.Tables(0).Rows(0).Item("IsPrint")) = "1" Then
            chkPrinted.Checked = True
        Else
            chkPrinted.Checked = False
        End If

        If objRPHDs.Tables(0).Rows(0).Item("RPHType") = objPU.EnumRPHType.Stock Then
            lblRPHTypeName.Text = "Stock / Workshop"
        Else
            lblRPHTypeName.Text = objPU.mtdGetRPHType(objRPHDs.Tables(0).Rows(0).Item("RPHType"))
        End If


        lblStatus.Text = objPU.mtdGetRPHStatus(objRPHDs.Tables(0).Rows(0).Item("Status"))
        lblHidStatus.Text = objRPHDs.Tables(0).Rows(0).Item("Status").Trim()
        lblCreateDate.Text = objGlobal.GetLongDate(objRPHDs.Tables(0).Rows(0).Item("CreateDate"))
        lblUpdateDate.Text = objGlobal.GetLongDate(objRPHDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblPrintDate.Text = objGlobal.GetLongDate(objRPHDs.Tables(0).Rows(0).Item("PrintDate"))
        lblUpdateBy.Text = objRPHDs.Tables(0).Rows(0).Item("UpdateID")
        lblTotalAmountAll.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(objRPHDs.Tables(0).Rows(0).Item("TotalAmount"), 0))
        txtRemark.Text = Trim(objRPHDs.Tables(0).Rows(0).Item("Remark"))

        If objRPHDs.Tables(0).Rows(0).Item("PPN").Trim() = "1" Then
            ChkPPN.Checked = True
        Else
            ChkPPN.Checked = False
        End If

        BindDeptCode(objRPHDs.Tables(0).Rows(0).Item("DeptCode").Trim())
        BindPOIssued(objRPHDs.Tables(0).Rows(0).Item("POIssued").Trim())
        BindCurrencyList(Trim(objRPHDs.Tables(0).Rows(0).Item("CurrencyCode")))
        BindLocPenyerahan(objRPHDs.Tables(0).Rows(0).Item("LocPenyerahan").Trim())

        txtExRate.Text = objRPHDs.Tables(0).Rows(0).Item("ExchangeRate")
        lblTotalAmountAll.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objRPHDs.Tables(0).Rows(0).Item("TotalAmountCurrency") + objRPHDs.Tables(0).Rows(0).Item("AddDiscount"), 2), 2)
        hidTtlAmount.Value = objRPHDs.Tables(0).Rows(0).Item("TotalAmountCurrency") + objRPHDs.Tables(0).Rows(0).Item("AddDiscount")
        hidAddDisc.Value = objRPHDs.Tables(0).Rows(0).Item("AddDiscount")
        txtAddDisc.Text = objRPHDs.Tables(0).Rows(0).Item("AddDiscount")
        txtTtlAftDisc.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objRPHDs.Tables(0).Rows(0).Item("TotalAmountCurrency"), 2), 2)
        txtRPHDate.Text = Date_Validation(objRPHDs.Tables(0).Rows(0).Item("CreateDate"), True)

        If Trim(objRPHDs.Tables(0).Rows(0).Item("Centralized")) = "1" Then
            Centralized_Yes.Visible = True
            Centralized_No.Visible = False
            chkCentralized.Checked = True
            chkCentralized.Text = "  Yes"
        Else
            Centralized_Yes.Visible = False
            Centralized_No.Visible = True
            chkCentralized.Checked = False
            chkCentralized.Text = "  No"
        End If

        BtnVerified.Visible = False

        Select Case objRPHDs.Tables(0).Rows(0).Item("Status")
            Case objPU.EnumRPHStatus.Active
                tblDetail.Visible = True
                tblRPHLine.Visible = True
                txtSuppCode1.Enabled = True
                txtSuppCode2.Enabled = True
                txtSuppCode3.Enabled = True
                txtRemark.Enabled = True
                btnSave.Visible = True
                btnEdited.Visible = False
                'If intLevel = 0 Then
                '    btnConfirm.Visible = False
                'Else
                btnConfirm.Visible = True
                'End If
                btnDelete.Visible = True
                btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                btnUnDelete.Visible = False
                'btnCancel.Visible = False
                btnPrint.Visible = True
                ddlDeptCode.Enabled = True
                ddlPOIssued.Enabled = True
                ChkPPN.Enabled = False
                chkCentralized.Enabled = False
                ddlCurrency.Enabled = True
                txtExRate.Enabled = True
                txttransporterCode1.Enabled = True
                txtAmtTransportFee.Enabled = True
                btnAdd.Visible = True
                ImgGen.Visible = True
                TABBK.Visible = True
                tblPR.Visible = True
                tblNote.Visible = True
                onload_PORPH(lblRPHId.Text)

                Dim nPOExist As String = ""
                nPOExist = ""
                For intCnt = 0 To dgRPHPO.Items.Count - 1
                    nPOExist = CType(dgRPHPO.Items(intCnt).FindControl("lblPOID"), Label).Text
                Next


                If Len(Trim(lblRPHId.Text)) > 0 And Len(Trim(nPOExist)) > 0 Then
                    BtnVerified.Visible = True
                Else
                    BtnVerified.Visible = False
                End If

            Case objPU.EnumRPHStatus.Confirmed
                Try
                    intErrNo = objPU.mtdGetDocStatusAction(strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           True, False, False, _
                                                           pv_strRPHId, _
                                                           blnDisplayCancel)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PO_CANCEL_BUTTON&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_rphlist.aspx")
                End Try

                If blnDisplayCancel = True Then
                    'btnCancel.Visible = True
                Else
                    'btnCancel.Visible = False
                End If

                'tblDetail.Visible = False
                'tblNote.Visible = False
                tblPR.Visible = False
                TABBK.Visible = False
                ImgGen.Visible = False
                btnAdd.Visible = False
                tblRPHLine.Visible = False
                txtSuppCode1.Enabled = False
                txtSuppCode2.Enabled = False
                txtSuppCode3.Enabled = False
                txtRemark.Enabled = False
                btnSave.Visible = False
                btnConfirm.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Visible = False
                btnPrint.Visible = True
                ddlDeptCode.Enabled = False
                ddlPOIssued.Enabled = False
                ChkPPN.Enabled = False
                chkCentralized.Enabled = False
                ddlCurrency.Enabled = False
                txtExRate.Enabled = False
                txttransporterCode1.Enabled = False
                txtAmtTransportFee.Enabled = False
                onload_PORPH(lblRPHId.Text)

                ''level menager akses edit yg sudah confirm dibuka
                If intLevel > 2 Then
                    btnEdited.Visible = True
                End If

            Case objPU.EnumRPHStatus.Deleted
                tblDetail.Visible = False
                tblNote.Visible = False
                tblPR.Visible = False
                TABBK.Visible = False
                btnAdd.Visible = False
                tblRPHLine.Visible = False
                txtSuppCode1.Enabled = False
                txtSuppCode2.Enabled = False
                txtSuppCode3.Enabled = False
                txtRemark.Enabled = False
                btnSave.Visible = False
                btnConfirm.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                btnUnDelete.Visible = False
                'btnCancel.Visible = False
                btnPrint.Visible = False
                ddlDeptCode.Enabled = False
                ddlPOIssued.Enabled = False
                ChkPPN.Enabled = False
                chkCentralized.Enabled = False
                ddlCurrency.Enabled = False
                txtExRate.Enabled = False
                txttransporterCode1.Enabled = False
                txtAmtTransportFee.Enabled = False
                btnEdited.Visible = False
                ImgGen.Visible = False
            Case objPU.EnumRPHStatus.Cancelled
                tblDetail.Visible = False
                tblNote.Visible = False
                tblPR.Visible = False
                TABBK.Visible = False
                btnAdd.Visible = False
                tblRPHLine.Visible = False
                txtSuppCode1.Enabled = False
                txtSuppCode2.Enabled = False
                txtSuppCode3.Enabled = False
                txtRemark.Enabled = False
                btnSave.Visible = False
                btnConfirm.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Visible = False
                'btnCancel.Visible = False
                btnPrint.Visible = False
                ddlDeptCode.Enabled = False
                ddlPOIssued.Enabled = False
                ChkPPN.Enabled = False
                chkCentralized.Enabled = False
                ddlCurrency.Enabled = False
                txtExRate.Enabled = False
                txttransporterCode1.Enabled = False
                txtAmtTransportFee.Enabled = False
                btnEdited.Visible = False
                ImgGen.Visible = False
            Case Else
                tblDetail.Visible = True
                tblNote.Visible = True
                tblPR.Visible = True
                TABBK.Visible = True
                btnAdd.Visible = True
                tblRPHLine.Visible = False
                txtSuppCode1.Enabled = False
                txtSuppCode2.Enabled = False
                txtSuppCode3.Enabled = False
                txtRemark.Enabled = False
                btnSave.Visible = False
                btnConfirm.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Visible = False
                'btnCancel.Visible = False
                btnPrint.Visible = False
                ddlDeptCode.Enabled = False
                ddlPOIssued.Enabled = False
                ChkPPN.Enabled = False
                chkCentralized.Enabled = False
                ddlCurrency.Enabled = False
                txtExRate.Enabled = False
                txttransporterCode1.Enabled = False
                txtAmtTransportFee.Enabled = False
                btnEdited.Visible = False
                ImgGen.Visible = True
        End Select

        ChkSupp1.Checked = False
        ChkSupp2.Checked = False
        ChkSupp3.Checked = False
    End Sub

    Sub onLoad_DisplayRPHLn(ByVal pv_strRPHId As String)
        Dim strOpCd As String = "PU_CLSTRX_RPH_LINE_GET"
        Dim objRPHLnDs As New Object()
        Dim strParam As String = pv_strRPHId & "|" & strAccYear & "|"
        'Dim UpdButton As LinkButton
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim chkDetSup1 As CheckBox
        Dim chkDetSup2 As CheckBox
        Dim chkDetSup3 As CheckBox
        Dim UpdButton As LinkButton
        Dim DelButton As LinkButton
        Dim EdtButton As LinkButton
        Dim CanButton As LinkButton

        Try
            intErrNo = objPU.mtdGetRPHLn(strOpCd, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strParam, _
                                        objRPHLnDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_RPHLn&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_rphlist.aspx")
        End Try

        txtSuppCode1.Text = ""
        txtSuppName1.Text = ""
        txtSuppCode2.Text = ""
        txtSuppName2.Text = ""
        txtSuppCode3.Text = ""
        txtSuppName3.Text = ""

        If objRPHLnDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRPHLnDs.Tables(0).Rows.Count - 1
                txtSuppCode1.Text = objRPHLnDs.Tables(0).Rows(intCnt).Item("SuppCode1").Trim()
                txtSuppName1.Text = objRPHLnDs.Tables(0).Rows(intCnt).Item("SuppName1").Trim()
                txtSuppCode2.Text = objRPHLnDs.Tables(0).Rows(intCnt).Item("SuppCode2").Trim()
                txtSuppName2.Text = objRPHLnDs.Tables(0).Rows(intCnt).Item("SuppName2").Trim()
                txtSuppCode3.Text = objRPHLnDs.Tables(0).Rows(intCnt).Item("SuppCode3").Trim()
                txtSuppName3.Text = objRPHLnDs.Tables(0).Rows(intCnt).Item("SuppName3").Trim()
                ddlPPN1.SelectedItem.Value = objRPHLnDs.Tables(0).Rows(intCnt).Item("PPNCheck").Trim()
                ddlPPN2.SelectedItem.Value = objRPHLnDs.Tables(0).Rows(intCnt).Item("PPNCheck2").Trim()
                ddlPPN3.SelectedItem.Value = objRPHLnDs.Tables(0).Rows(intCnt).Item("PPNCheck3").Trim()

                If Trim(objRPHLnDs.Tables(0).Rows(0).Item("PPNCheck")) = "1" Then
                    ddlPPN1.SelectedIndex = 1
                Else
                    ddlPPN1.SelectedIndex = 0
                End If

                If Trim(objRPHLnDs.Tables(0).Rows(0).Item("PPNCheck2")) = "1" Then
                    ddlPPN2.SelectedIndex = 1
                Else
                    ddlPPN2.SelectedIndex = 0
                End If

                If Trim(objRPHLnDs.Tables(0).Rows(0).Item("PPNCheck3")) = "1" Then
                    ddlPPN3.SelectedIndex = 1
                Else
                    ddlPPN3.SelectedIndex = 0
                End If

                objRPHLnDs.Tables(0).Rows(intCnt).Item("PRId") = objRPHLnDs.Tables(0).Rows(intCnt).Item("PRId").Trim()
                objRPHLnDs.Tables(0).Rows(intCnt).Item("ItemCode") = objRPHLnDs.Tables(0).Rows(intCnt).Item("ItemCode").Trim()
                objRPHLnDs.Tables(0).Rows(intCnt).Item("Description") = objRPHLnDs.Tables(0).Rows(intCnt).Item("ItemCode").Trim() & " (" & _
                                                                    objRPHLnDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
                objRPHLnDs.Tables(0).Rows(intCnt).Item("QtyOrder1") = objRPHLnDs.Tables(0).Rows(intCnt).Item("QtyOrder1")
                objRPHLnDs.Tables(0).Rows(intCnt).Item("Cost1") = objRPHLnDs.Tables(0).Rows(intCnt).Item("Cost1")
                objRPHLnDs.Tables(0).Rows(intCnt).Item("QtyOrder2") = objRPHLnDs.Tables(0).Rows(intCnt).Item("QtyOrder2")
                objRPHLnDs.Tables(0).Rows(intCnt).Item("Cost2") = objRPHLnDs.Tables(0).Rows(intCnt).Item("Cost2")
                objRPHLnDs.Tables(0).Rows(intCnt).Item("QtyOrder3") = objRPHLnDs.Tables(0).Rows(intCnt).Item("QtyOrder3")
                objRPHLnDs.Tables(0).Rows(intCnt).Item("Cost3") = objRPHLnDs.Tables(0).Rows(intCnt).Item("Cost3")
                objRPHLnDs.Tables(0).Rows(intCnt).Item("Status") = objRPHLnDs.Tables(0).Rows(intCnt).Item("Status").Trim()
            Next intCnt
        Else
            hidAddDisc.Value = "0"
            txtAddDisc.Text = "0"
            txtTtlAftDisc.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(hidTtlAmount.Value, 2), 2)
        End If

        dgRPHDet.DataSource = Nothing
        dgRPHDet.DataSource = objRPHLnDs.Tables(0)
        dgRPHDet.DataBind()

        For intCnt = 0 To objRPHLnDs.Tables(0).Rows.Count - 1
            CType(dgRPHDet.Items(intCnt).FindControl("lblNo"), Label).Text = intCnt + 1
            chkDetSup1 = dgRPHDet.Items.Item(intCnt).FindControl("chkDetSup1")
            chkDetSup1.Checked = IIf(objRPHLnDs.Tables(0).Rows(intCnt).Item("FSupp1") = 1, True, False)
            If objRPHLnDs.Tables(0).Rows(intCnt).Item("SuppCode1") <> "" Then
                chkDetSup1.Enabled = True
            Else
                chkDetSup1.Enabled = False
            End If

            chkDetSup2 = dgRPHDet.Items.Item(intCnt).FindControl("chkDetSup2")
            chkDetSup2.Checked = IIf(objRPHLnDs.Tables(0).Rows(intCnt).Item("FSupp2") = 1, True, False)
            If objRPHLnDs.Tables(0).Rows(intCnt).Item("SuppCode2") <> "" Then
                chkDetSup2.Enabled = True
            Else
                chkDetSup2.Enabled = False
            End If

            chkDetSup3 = dgRPHDet.Items.Item(intCnt).FindControl("chkDetSup3")
            chkDetSup3.Checked = IIf(objRPHLnDs.Tables(0).Rows(intCnt).Item("FSupp3") = 1, True, False)

            If objRPHLnDs.Tables(0).Rows(intCnt).Item("SuppCode3") <> "" Then
                chkDetSup3.Enabled = True
            Else
                chkDetSup3.Enabled = False
            End If

            If objRPHLnDs.Tables(0).Rows(intCnt).Item("ItemCode_Repl") = "" Then
                CType(dgRPHDet.Items(intCnt).FindControl("lblItemRepNote"), Label).Visible = False
                CType(dgRPHDet.Items(intCnt).FindControl("lblItemRepNote"), Label).Text = ""
            Else
                CType(dgRPHDet.Items(intCnt).FindControl("lblItemRepNote"), Label).Visible = True
                CType(dgRPHDet.Items(intCnt).FindControl("lblItemRepNote"), Label).Text = "Replace With : "
            End If

            EdtButton = dgRPHDet.Items.Item(intCnt).FindControl("Edit")
            DelButton = dgRPHDet.Items.Item(intCnt).FindControl("Delete")
            UpdButton = dgRPHDet.Items.Item(intCnt).FindControl("Update")
            CanButton = dgRPHDet.Items.Item(intCnt).FindControl("Cancel")

            Select Case Trim(lblStatus.Text)
                Case objPU.mtdGetRPHStatus(objPU.EnumRPHStatus.Active)
                    DelButton.Visible = True
                    DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    UpdButton.Visible = False
                    CanButton.Visible = False

                    'If intLevel = 1 Then
                    EdtButton.Visible = True
                    btnConfirm.Visible = True
                    'Else
                    'EdtButton.Visible = False
                    'btnConfirm.Visible = False
                    'End If

                Case objPU.mtdGetRPHStatus(objPU.EnumRPHStatus.Deleted), objPU.mtdGetRPHStatus(objPU.EnumRPHStatus.Cancelled), objPU.mtdGetRPHStatus(objPU.EnumRPHStatus.Closed), objPU.mtdGetRPHStatus(objPU.EnumRPHStatus.Confirmed)
                    EdtButton.Visible = False
                    UpdButton.Visible = False
                    CanButton.Visible = False
                    DelButton.Visible = False

            End Select
        Next intCnt

        If chkCentralized.Checked = True Then
            BindPRItem(strSelectedPRId)
            BindPR(strSelectedRPHId)
        Else
            BindINItem("")
        End If
    End Sub

    Sub onLoad_EditDetail(ByVal pv_strRPHID As String, ByVal pv_strRPHLNID As String)
        Dim strOpCode As String = "PU_CLSTRX_RPH_LINE_GET"
        Dim ssQLKriteria As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim objItemDs
        Dim intErrNo As Integer

        ssQLKriteria = "And a.RPHID='" & pv_strRPHID & "' AND a.RPHLnId='" & pv_strRPHLNID & "'"
        strParamName = "LOC|STRSEARCH"
        strParamValue = strLocation & "|" & ssQLKriteria


        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If objItemDs.Tables(0).Rows.Count > 0 Then
            With Me
                BindPPNList()

                .txtPRID_Plmph.Text = Trim(objItemDs.Tables(0).Rows(0).Item("PRId"))
                .lblRphLNID.Text = Trim(objItemDs.Tables(0).Rows(0).Item("RPHLNID"))
                .lblPRLocCode.Text = Trim(objItemDs.Tables(0).Rows(0).Item("PRLocCode"))
                .ddlPRRefLocCode.SelectedItem.Value = objItemDs.Tables(0).Rows(0).Item("PRRefLocCode")
                BindLoc(Trim(objItemDs.Tables(0).Rows(0).Item("PRRefLocCode")))
                .txtPRRefId.Text = objItemDs.Tables(0).Rows(0).Item("PRLNId")

                .txtPrQtyOrder.Text = objItemDs.Tables(0).Rows(0).Item("PrQty")
                .txtPurchUOM.Text = objItemDs.Tables(0).Rows(0).Item("PurchaseUom")
                .txtPrCostOrder.Text = objItemDs.Tables(0).Rows(0).Item("PrCost")
                .txtPrTCostOrder.Text = objItemDs.Tables(0).Rows(0).Item("PrAmount")

                .txtItemCode.Text = Trim(objItemDs.Tables(0).Rows(0).Item("ItemCode"))
                .TxtItemName.Text = Trim(objItemDs.Tables(0).Rows(0).Item("Description"))

                .txtItemCode_Rep.Text = Trim(objItemDs.Tables(0).Rows(0).Item("ItemCode_Repl"))
                .TxtItemName_Rep.Text = Trim(objItemDs.Tables(0).Rows(0).Item("ItemRep_Description"))

                .txtSuppCode1.Text = Trim(objItemDs.Tables(0).Rows(0).Item("suppcode1"))
                .txtSuppName1.Text = Trim(objItemDs.Tables(0).Rows(0).Item("SuppName1"))
                .txtQtyOrder1.Text = Trim(objItemDs.Tables(0).Rows(0).Item("QtyOrder1"))
                .txtCost1.Text = Trim(objItemDs.Tables(0).Rows(0).Item("cost1"))
                .txtTtlCost1.Text = Trim(objItemDs.Tables(0).Rows(0).Item("Amount1"))
                .txtDiscount1.Text = objItemDs.Tables(0).Rows(0).Item("DisCount1")
                .txtPBBKB1.Text = objItemDs.Tables(0).Rows(0).Item("PBBKB1")
                .txtPBBKBRate1.Text = objItemDs.Tables(0).Rows(0).Item("PBBKBRate1")
                .txtPPN221.Text = objItemDs.Tables(0).Rows(0).Item("PPN221")
                .txttransporterCode1.Text = Trim(objItemDs.Tables(0).Rows(0).Item("Transporter"))
                .txttransporterName1.Text = Trim(objItemDs.Tables(0).Rows(0).Item("TransName1"))
                .txtAmtTransportFee.Text = objItemDs.Tables(0).Rows(0).Item("AmtTransportFee")

                If Trim(objItemDs.Tables(0).Rows(0).Item("PPNCheck")) = "1" Then
                    .ddlPPN1.SelectedIndex = 1
                Else
                    .ddlPPN1.SelectedIndex = 0
                End If

                .txtSuppCode2.Text = Trim(objItemDs.Tables(0).Rows(0).Item("suppcode2"))
                .txtSuppName2.Text = Trim(objItemDs.Tables(0).Rows(0).Item("SuppName2"))
                .txtQtyOrder2.Text = Trim(objItemDs.Tables(0).Rows(0).Item("QtyOrder2"))
                .txtCost2.Text = Trim(objItemDs.Tables(0).Rows(0).Item("cost2"))
                .txtTtlCost2.Text = Trim(objItemDs.Tables(0).Rows(0).Item("Amount2"))
                .txtDiscount2.Text = objItemDs.Tables(0).Rows(0).Item("DisCount2")
                .txtPBBKB2.Text = objItemDs.Tables(0).Rows(0).Item("PBBKB2")
                .txtPBBKBRate2.Text = objItemDs.Tables(0).Rows(0).Item("PBBKBRate2")
                .txtPPN222.Text = objItemDs.Tables(0).Rows(0).Item("PPN222")
                .txttransporter2.Text = Trim(objItemDs.Tables(0).Rows(0).Item("Transporter2"))
                .txttransporterName2.Text = Trim(objItemDs.Tables(0).Rows(0).Item("TransName2"))

                .txtAmtTransportFee2.Text = objItemDs.Tables(0).Rows(0).Item("AmtTransportFee2")
                .ddlPPN2.SelectedItem.Value = objItemDs.Tables(0).Rows(0).Item("PPNCheck2")

                If Trim(objItemDs.Tables(0).Rows(0).Item("PPNTr1")) = "1" Then
                    .DDLPPNTr1.SelectedIndex = 1
                Else
                    .DDLPPNTr1.SelectedIndex = 0
                End If

                If Trim(objItemDs.Tables(0).Rows(0).Item("PPNTr2")) = "1" Then
                    .DDLPPNTr2.SelectedIndex = 1
                Else
                    .DDLPPNTr2.SelectedIndex = 0
                End If

                If Trim(objItemDs.Tables(0).Rows(0).Item("PPNTr3")) = "1" Then
                    .DDLPPNTr3.SelectedIndex = 1
                Else
                    .DDLPPNTr3.SelectedIndex = 0
                End If

                If Trim(objItemDs.Tables(0).Rows(0).Item("PPNCheck2")) = "1" Then
                    .ddlPPN2.SelectedIndex = 1
                Else
                    .ddlPPN2.SelectedIndex = 0
                End If

                .txtSuppCode3.Text = Trim(objItemDs.Tables(0).Rows(0).Item("suppcode3"))
                .txtSuppName3.Text = Trim(objItemDs.Tables(0).Rows(0).Item("SuppName3"))
                .txtQtyOrder3.Text = Trim(objItemDs.Tables(0).Rows(0).Item("QtyOrder3"))
                .txtCost3.Text = Trim(objItemDs.Tables(0).Rows(0).Item("cost3"))
                .txtTtlCost3.Text = Trim(objItemDs.Tables(0).Rows(0).Item("Amount3"))
                .txtDiscount3.Text = objItemDs.Tables(0).Rows(0).Item("DisCount3")
                .txtPBBKB3.Text = objItemDs.Tables(0).Rows(0).Item("PBBKB3")
                .txtPBBKBRate3.Text = objItemDs.Tables(0).Rows(0).Item("PBBKBRate3")
                .txtPPN223.Text = objItemDs.Tables(0).Rows(0).Item("PPN223")
                .txttransporter3.Text = Trim(objItemDs.Tables(0).Rows(0).Item("Transporter3"))
                .txttransporterName3.Text = Trim(objItemDs.Tables(0).Rows(0).Item("TransName3"))
                .txtAmtTransportFee3.Text = objItemDs.Tables(0).Rows(0).Item("AmtTransportFee3")
                .ddlPPN3.SelectedItem.Value = objItemDs.Tables(0).Rows(0).Item("PPNCheck3")

                If Trim(objItemDs.Tables(0).Rows(0).Item("PPNCheck3")) = "1" Then
                    .ddlPPN3.SelectedIndex = 1
                Else
                    .ddlPPN3.SelectedIndex = 0
                End If

                .txtAddNote.Text = Replace(Trim(objItemDs.Tables(0).Rows(0).Item("AdditionalNote")), "'", "''")

                .txtsuppNote1.Text = Trim(objItemDs.Tables(0).Rows(0).Item("SupplierNote1"))
                .txtsuppNote2.Text = Trim(objItemDs.Tables(0).Rows(0).Item("SupplierNote2"))
                .txtsuppNote3.Text = Trim(objItemDs.Tables(0).Rows(0).Item("SupplierNote3"))

            End With
        End If
    End Sub

    Sub BindPPNList()
        ddlPPN1.Items.Clear()
        ddlPPN2.Items.Clear()
        ddlPPN3.Items.Clear()

        DDLPPNTr1.Items.Clear()
        DDLPPNTr2.Items.Clear()
        DDLPPNTr3.Items.Clear()

        ddlPPN1.Items.Add(New ListItem("No", "0"))
        ddlPPN1.Items.Add(New ListItem("Yes", "1"))

        ddlPPN2.Items.Add(New ListItem("No", "0"))
        ddlPPN2.Items.Add(New ListItem("Yes", "1"))

        ddlPPN3.Items.Add(New ListItem("No", "0"))
        ddlPPN3.Items.Add(New ListItem("Yes", "1"))

        DDLPPNTr1.Items.Add(New ListItem("No", "0"))
        DDLPPNTr1.Items.Add(New ListItem("Yes", "1"))

        DDLPPNTr2.Items.Add(New ListItem("No", "0"))
        DDLPPNTr2.Items.Add(New ListItem("Yes", "1"))

        DDLPPNTr3.Items.Add(New ListItem("No", "0"))
        DDLPPNTr3.Items.Add(New ListItem("Yes", "1"))

    End Sub

    Sub BindPR(ByVal pv_strPRId As String)
        'Dim dr As DataRow
        'Dim strParamName As String
        'Dim strParamValue As String
        'Dim intErrNo As Integer
        'Dim intCnt As Integer = 0
        'Dim intSelectedIndex As Integer
        'Dim objUser As New Object
        'Dim strOpCd As String = "PU_CLSTRX_RPH_GET_PR"

        'strParamName = "LOCCODE|STRSEARCH"
        'strParamValue = strLocation & "|" & "AND UserPO='" & strUserId & "'"

        'Try
        '    intErrNo = ObjOk.mtdGetDataCommon(strOpCd, _
        '                                        strParamName, _
        '                                        strParamValue, _
        '                                        objUser)

        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        'End Try

        'For intCnt = 0 To objUser.Tables(0).Rows.Count - 1
        '    objUser.Tables(0).Rows(intCnt).Item("PRId") = objUser.Tables(0).Rows(intCnt).Item("PRId").Trim()
        '    objUser.Tables(0).Rows(intCnt).Item("PRId") = objUser.Tables(0).Rows(intCnt).Item("PRId").Trim()

        '    If objUser.Tables(0).Rows(intCnt).Item("PRId") = strSelectedPRId Then
        '        intSelectedIndex = intCnt + 1
        '        lblPRLocCode.Text = objUser.Tables(0).Rows(intCnt).Item("LocCode")
        '        strPRLocCode = lblPRLocCode.Text
        '    Else
        '        lblPRLocCode.Text = ""
        '    End If
        'Next intCnt

        'dr = objUser.Tables(0).NewRow()
        'dr("PRId") = ""
        'dr("PRId") = "Please Select Purchase Requisition ID"
        'objUser.Tables(0).Rows.InsertAt(dr, 0)

        'ddlPRId.DataSource = objUser.Tables(0)
        'ddlPRId.DataValueField = "PRId"
        'ddlPRId.DataTextField = "PRId"
        'ddlPRId.DataBind()
        'ddlPRId.SelectedIndex = intSelectedIndex
        'strSelectedPRId = ddlPRId.SelectedItem.Value


    End Sub

    Sub BindPRItem(ByVal pv_strPRId As String)
        'Dim strOpCd As String = "PU_CLSTRX_PR_LINE_GET1"
        'Dim objINItemDs As New Object()
        'Dim strItemCode As String
        'Dim dr As DataRow
        'Dim intCnt As Integer = 0
        'Dim intErrNo As Integer
        'Dim intSelectedIndex As Integer = 0
        'Dim strRPHType As String

        'Dim strParamName As String
        'Dim strParamValue As String

        'If pv_strPRId <> "" Then
        '    'strParamName = "LOCCODE|STRSEARCH|STRSEARCH1|USERPO"
        '    'strParamValue = strLocation & _
        '    '                "|" & "AND B.PRId IN ('" & RTrim(pv_strPRId) & "','" & RTrim(pv_strPRId) & "-ADD" & "') AND A.PRType IN ('1','4')" & _
        '    '                "|" & "WHERE A.PRId IN ('" & RTrim(pv_strPRId) & "','" & RTrim(pv_strPRId) & "-ADD" & "')" & _
        '    '                "|" & strUserId

        '    strParamName = "LOCCODE|STRSEARCH|USERPO"
        '    strParamValue = strLocation & _
        '                    "|" & "AND B.PRId IN ('" & RTrim(pv_strPRId) & "','" & RTrim(pv_strPRId) & "-ADD" & "') AND A.PRType IN ('1','4')" & _
        '                    "|" & strUserId


        '    Try
        '        intErrNo = ObjOk.mtdGetDataCommon(strOpCd, _
        '                                            strParamName, _
        '                                            strParamValue, _
        '                                            objINItemDs)

        '    Catch Exp As System.Exception
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        '    End Try

        '    For intCnt = 0 To objINItemDs.Tables(0).Rows.Count - 1
        '        objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode").Trim()
        '        objINItemDs.Tables(0).Rows(intCnt).Item("Description") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode").Trim() & "(" & objINItemDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")" & objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode").Trim()
        '    Next intCnt

        '    dr = objINItemDs.Tables(0).NewRow()
        '    dr("ItemCode") = ""
        '    dr("Description") = lblSelectListItem.Text & lblStockItem.Text
        '    objINItemDs.Tables(0).Rows.InsertAt(dr, 0)

        '    ddlItemCode.DataSource = objINItemDs.Tables(0)
        '    ddlItemCode.DataValueField = "ItemCode"
        '    ddlItemCode.DataTextField = "Description"
        '    ddlItemCode.DataBind()
        '    ddlItemCode.SelectedIndex = intSelectedIndex

        '    If ddlItemCode.SelectedItem.Value = "" Then
        '        txtQtyOrder1.Text = "0"
        '        txtQtyOrder2.Text = "0"
        '        txtQtyOrder3.Text = "0"
        '        txtCost1.Text = "0"
        '        txtCost2.Text = "0"
        '        txtCost3.Text = "0"
        '        txtTtlCost1.Text = "0"
        '        txtTtlCost2.Text = "0"
        '        txtTtlCost3.Text = "0"
        '        txtPBBKB1.Text = "0"
        '        txtPBBKB2.Text = "0"
        '        txtPBBKB3.Text = "0"
        '        txtAddNote.Text = ""
        '        txtAmtTransportFee.Text = "0"
        '        txtDiscount1.Text = "0"
        '        txtDiscount2.Text = "0"
        '        txtDiscount3.Text = "0"
        '        txtPPN221.Text = "0"
        '        txtPPN222.Text = "0"
        '        txtPPN223.Text = "0"
        '    End If
        'Else
        '    ddlItemCode.Items.Clear()
        '    ddlItemCode.Items.Add(New ListItem(lblSelectListItem.Text & lblStockItem.Text, ""))
        '    txtQtyOrder1.Text = "0"
        '    txtQtyOrder2.Text = "0"
        '    txtQtyOrder3.Text = "0"
        '    txtCost1.Text = "0"
        '    txtCost2.Text = "0"
        '    txtCost3.Text = "0"
        '    txtTtlCost1.Text = "0"
        '    txtTtlCost2.Text = "0"
        '    txtTtlCost3.Text = "0"
        '    txtPBBKB1.Text = "0"
        '    txtPBBKB2.Text = "0"
        '    txtPBBKB3.Text = "0"
        '    txtAddNote.Text = ""
        '    txtAmtTransportFee.Text = "0"
        '    txtDiscount1.Text = "0"
        '    txtDiscount2.Text = "0"
        '    txtDiscount3.Text = "0"
        '    txtPPN221.Text = "0"
        '    txtPPN222.Text = "0"
        '    txtPPN223.Text = "0"
        'End If
    End Sub

    Sub BindINItem(ByVal pv_strSelectedPOType As String)
        'Dim strOpCd As String = "IN_CLSTRX_ITEMPART_ITEM_GET"
        'Dim objINItemDs As New Object()
        'Dim strParam As String = ""
        'Dim dr As DataRow
        'Dim intCnt As Integer = 0
        'Dim intErrNo As Integer
        'Dim intSelectedIndex As Integer = 0
        'Dim strRPHType As String

        'If lblRPHType.Text = objPU.EnumRPHType.Stock Then
        '    strRPHType = objINSetup.EnumInventoryItemType.Stock & "','" & objINSetup.EnumInventoryItemType.WorkshopItem
        'Else
        '    strRPHType = lblRPHType.Text
        'End If

        'strParam = strRPHType & "|" & _
        '           strLocation & "|" & _
        '           objINSetup.EnumStockItemStatus.Active & "|itm.ItemCode"

        'Try
        '    intErrNo = objPU.mtdGetItem(strOpCd, strParam, objINItemDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_INITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        'End Try

        'txtQtyOrder1.Text = "0"
        'txtQtyOrder2.Text = "0"
        'txtQtyOrder3.Text = "0"
        'txtCost1.Text = "0"
        'txtCost2.Text = "0"
        'txtCost3.Text = "0"
        'txtTtlCost1.Text = "0"
        'txtTtlCost2.Text = "0"
        'txtTtlCost3.Text = "0"
        'txtPBBKB1.Text = "0"
        'txtPBBKB2.Text = "0"
        'txtPBBKB3.Text = "0"
        'txtAddNote.Text = ""
        'txtAmtTransportFee.Text = "0"
        'txtDiscount1.Text = "0"
        'txtDiscount2.Text = "0"
        'txtDiscount3.Text = "0"
        'txtPPN221.Text = "0"
        'txtPPN222.Text = "0"
        'txtPPN223.Text = "0"

        'dr = objINItemDs.Tables(0).NewRow()
        'dr("ItemCode") = ""
        'dr("Description") = lblSelectListItem.Text & lblStockItem.Text
        'objINItemDs.Tables(0).Rows.InsertAt(dr, 0)

        'ddlItemCode.DataSource = objINItemDs.Tables(0)
        'ddlItemCode.DataValueField = "ItemCode"
        'ddlItemCode.DataTextField = "Description"
        'ddlItemCode.DataBind()
        'ddlItemCode.SelectedIndex = intSelectedIndex
    End Sub

    Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Function Divide(ByVal Val1 As Double, ByVal Val2 As Double) As Double
        Dim nVal As Double

        If IsNothing(Val2) Then
            nVal = 0
        Else
            If Val2 = 0 Then
                nVal = 0
            Else
                nVal = Val1 / Val2
            End If
        End If

        Divide = nVal
    End Function

    Sub CheckType()
        If lblRPHType.Text <> "" Then

            Select Case lblStatus.Text
                Case objPU.mtdGetPOStatus(objPU.EnumPOStatus.Active)
                    Select Case lblRPHType.Text
                        Case objPU.EnumPOType.Stock
                            FindIN.Visible = True
                            FindDC.Visible = False
                            FindWS.Visible = False
                            FindNU.Visible = False
                        Case objPU.EnumPOType.DirectCharge
                            FindIN.Visible = False
                            FindDC.Visible = True
                            FindWS.Visible = False
                            FindNU.Visible = False
                        Case objPU.EnumPOType.Workshop
                            FindIN.Visible = False
                            FindDC.Visible = False
                            FindWS.Visible = True
                            FindNU.Visible = False
                        Case objPU.EnumPOType.Nursery
                            FindIN.Visible = False
                            FindDC.Visible = False
                            FindWS.Visible = False
                            FindNU.Visible = True
                    End Select
                Case objPU.mtdGetPOStatus(objPU.EnumPOStatus.Confirmed), _
                     objPU.mtdGetPOStatus(objPU.EnumPOStatus.Deleted), _
                     objPU.mtdGetPOStatus(objPU.EnumPOStatus.Cancelled), _
                     objPU.mtdGetPOStatus(objPU.EnumPOStatus.Invoiced)
                    FindIN.Visible = False
                    FindDC.Visible = False
                    FindWS.Visible = False
                    FindNU.Visible = False
                Case Else
                    Select Case lblRPHType.Text
                        Case objPU.EnumPOType.Stock
                            FindIN.Visible = True
                            FindDC.Visible = False
                            FindWS.Visible = False
                            FindNU.Visible = False

                        Case objPU.EnumPOType.DirectCharge
                            FindIN.Visible = False
                            FindDC.Visible = True
                            FindWS.Visible = False
                            FindNU.Visible = False

                        Case objPU.EnumPOType.Workshop
                            FindIN.Visible = False
                            FindDC.Visible = False
                            FindWS.Visible = True
                            FindNU.Visible = False

                        Case objPU.EnumPOType.Nursery
                            FindIN.Visible = False
                            FindDC.Visible = False
                            FindWS.Visible = False
                            FindNU.Visible = True
                    End Select
            End Select
        End If
    End Sub

    Sub lClearDetail()
        With Me
            .lblRphLNID.Text = ""
            .txtPRID_Plmph.Text = ""
            .txtPRRefId.Text = ""
            '.txtSuppCode1.Text = ""
            '.txtSuppCode2.Text = ""
            '.txtSuppCode3.Text = ""
            '.txtSuppName1.Text = ""
            '.txtSuppName2.Text = ""
            '.txtSuppName3.Text = ""
            .txtSuppName1.ToolTip = "Please type here to find supplier code"
            .txtSuppName2.ToolTip = "Please type here to find supplier code"
            .txtSuppName3.ToolTip = "Please type here to find supplier code"
            .txtPrQtyOrder.Text = 0
            .txtPrCostOrder.Text = 0
            .txtPurchUOM.Text = ""
            .txtPrTCostOrder.Text = 0
            .txttransporter2.Text = ""
            .txttransporter3.Text = ""
            .txtItemCode.Text = ""
            .TxtItemName.Text = ""
            .txtTtlAftDisc.Text = 0
            .txtTtlCost1.Text = 0
            .txtTtlCost2.Text = 0
            .txtTtlCost3.Text = 0
            .txtQtyOrder1.Text = 0
            .txtQtyOrder2.Text = 0
            .txtQtyOrder3.Text = 0
            .txtCost1.Text = 0
            .txtCost2.Text = 0
            .txtCost3.Text = 0
            .txtDiscount1.Text = 0
            .txtDiscount2.Text = 0
            .txtDiscount3.Text = 0
            .txtPBBKB1.Text = 0
            .txtPBBKB2.Text = 0
            .txtPBBKB3.Text = 0
            .txtPBBKBRate1.Text = 0
            .txtPBBKBRate2.Text = 0
            .txtPBBKBRate3.Text = 0
            .txtPPN221.Text = 0
            .txtPPN222.Text = 0
            .txtPPN223.Text = 0
            .txtAmtTransportFee.Text = 0
            .txtAmtTransportFee2.Text = 0
            .txtAmtTransportFee3.Text = 0
            .txtAddNote.Text = ""
            .txtItemCode_Rep.Text = ""
            .TxtItemName_Rep.Text = ""
            .txttransporterCode1.Text = ""
            .txttransporterName1.Text = ""
            .txttransporterName2.Text = ""
            .txttransporterName2.Text = ""
            .txttransporterName2.ToolTip = "Please type here to find transporter code"
            .txttransporterName3.ToolTip = "Please type here to find transporter code"
            .lblPRLocCode.Text = ""
            .ddlPRRefLocCode.SelectedIndex = 0
            .txtsuppNote1.Text = ""
            .txtsuppNote2.Text = ""
            .txtsuppNote3.Text = ""

            If .lblRPHType.Text.Trim = "2" Then
                BtnViewSPK.Visible = True
                BtnViewPR.Visible = False
            Else
                BtnViewSPK.Visible = False
                BtnViewPR.Visible = True
            End If

        End With
    End Sub

    Sub BindSup1(ByVal pName As String)
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData(pName)
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLine.PageSize)

        dgLine.DataSource = dsData
        If dgLine.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLine.CurrentPageIndex = 0
            Else
                dgLine.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgLine.DataBind()

    End Sub

    Sub BindSup2(ByVal pName As String)
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet


        dsData = LoadData(pName)
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLineSup2.PageSize)

        dgLineSup2.DataSource = dsData
        If dgLineSup2.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLineSup2.CurrentPageIndex = 0
            Else
                dgLineSup2.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgLineSup2.DataBind()

    End Sub

    Sub BindSup3(ByVal pName As String)
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet


        dsData = LoadData(pName)
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLineSup3.PageSize)

        dgLineSup3.DataSource = dsData
        If dgLineSup3.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLineSup3.CurrentPageIndex = 0
            Else
                dgLineSup3.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgLineSup3.DataBind()
    End Sub

    Sub BindTrans1(ByVal pName As String)
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet


        dsData = LoadData(pName)
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLineTrans1.PageSize)

        dgLineTrans1.DataSource = dsData
        If dgLineTrans1.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLineTrans1.CurrentPageIndex = 0
            Else
                dgLineTrans1.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgLineTrans1.DataBind()
    End Sub

    Sub BindTrans2(ByVal pName As String)
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet


        dsData = LoadData(pName)
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLineTrans2.PageSize)

        dgLineTrans2.DataSource = dsData
        If dgLineTrans2.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLineTrans2.CurrentPageIndex = 0
            Else
                dgLineTrans2.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgLineTrans2.DataBind()
    End Sub

    Sub BindTrans3(ByVal pName As String)
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet


        dsData = LoadData(pName)
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLineTrans3.PageSize)

        dgLineTrans3.DataSource = dsData
        If dgLineTrans3.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLineTrans3.CurrentPageIndex = 0
            Else
                dgLineTrans3.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgLineTrans3.DataBind()
    End Sub

    Private Function checkSupplier() As Boolean
        checkSupplier = False

        strSelectedSuppCode1 = txtSuppCode1.Text
        strSelectedSuppCode2 = txtSuppCode2.Text
        strSelectedSuppCode3 = txtSuppCode3.Text


        If strSelectedSuppCode1 <> "" Then
            If strSelectedSuppCode2 <> "" Then
                If strSelectedSuppCode3 = "" Then
                    If (strSelectedSuppCode1 = strSelectedSuppCode2) Then
                        lblExistSuppCode.Visible = True
                        checkSupplier = True
                    End If
                Else
                    If (strSelectedSuppCode1 = strSelectedSuppCode2) Then
                        lblExistSuppCode.Visible = True
                        checkSupplier = True
                    ElseIf (strSelectedSuppCode1 = strSelectedSuppCode3) Then
                        lblExistSuppCode.Visible = True
                        checkSupplier = True
                    ElseIf (strSelectedSuppCode2 = strSelectedSuppCode3) Then
                        lblExistSuppCode.Visible = True
                        checkSupplier = True
                    End If
                End If
            Else
                If strSelectedSuppCode3 <> "" Then
                    If (strSelectedSuppCode1 = strSelectedSuppCode3) Then
                        lblExistSuppCode.Visible = True
                        checkSupplier = True
                    End If
                End If
            End If
        Else
            If strSelectedSuppCode2 <> "" Then
                If strSelectedSuppCode3 <> "" Then
                    If (strSelectedSuppCode2 = strSelectedSuppCode3) Then
                        lblExistSuppCode.Visible = True
                        checkSupplier = True
                    End If
                End If
            End If
        End If
    End Function

    Sub BindUOM(ByVal pv_strUOM As String)
        Dim strOpCd_GetUOM As String = "ADMIN_CLSUOM_UOM_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim objUOMDs As New Object()
        Dim intSelectedIndex As Integer

        If Trim(pv_strUOM) = "" Then
            strParam = "|"
        Else
            strParam = "|And UOMCode = '" & pv_strUOM & "'"
        End If

        Try
            intErrNo = objAdminUOM.mtdGetUOM(strOpCd_GetUOM, strParam, objUOMDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_UOM&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objUOMDs.Tables(0).Rows.Count - 1
            objUOMDs.Tables(0).Rows(intCnt).Item(0) = Trim(objUOMDs.Tables(0).Rows(intCnt).Item(0))
            objUOMDs.Tables(0).Rows(intCnt).Item(1) = Trim(objUOMDs.Tables(0).Rows(intCnt).Item(0)) & " (" & Trim(objUOMDs.Tables(0).Rows(intCnt).Item(1)) & ")"

            If Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode")) = Trim(pv_strUOM) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objUOMDs.Tables(0).NewRow()
        dr("UOMDesc") = "Select Unit of Measurement"
        dr("UOMCode") = ""
        objUOMDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPurchUom1.DataSource = objUOMDs.Tables(0)
        ddlPurchUom1.DataTextField = "UOMDesc"
        ddlPurchUom1.DataValueField = "UOMCode"
        ddlPurchUom1.DataBind()
        ddlPurchUom1.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindLocPenyerahan(ByVal StrLocCode As String)

        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strLocPenyerahan As String
        Dim strParam As String = ""
        Dim strParamValue As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        'strSelectedPRRefLocCode = StrLocCode
        'strLocPenyerahan = IIf(StrLocCode = "", "", strSelectedPRRefLocCode)
        'strParam = strLocPenyerahan & "|" & objAdmin.EnumLocStatus.Active & "|LocCode|"

        strParam = "STRSEARCH"
        strParamValue = "AND ((A.LocCOde='" & StrLocCode & "') OR ( '" & StrLocCode & "'=''))"

        Try
            'intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objLocDs)
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, strParam, strParamValue, objLocDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode").Trim()
            objLocDs.Tables(0).Rows(intCnt).Item("Description") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode") & " (" & objLocDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"

            If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = StrLocCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "Please Select Tempat Penyerahan"
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        DDLLocPenyerahan.DataSource = objLocDs.Tables(0)
        DDLLocPenyerahan.DataValueField = "LocCode"
        DDLLocPenyerahan.DataTextField = "Description"
        DDLLocPenyerahan.DataBind()
        DDLLocPenyerahan.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindLoc(ByVal StrLocCode As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim strParamValue As String = ""

        strParam = "STRSEARCH"
        strParamValue = ""

        Try
            'intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objLocDs)
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, strParam, strParamValue, objLocDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode").Trim()
            objLocDs.Tables(0).Rows(intCnt).Item("Description") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode") & " (" & objLocDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"

            If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = StrLocCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelectListLoc.Text & lblLocation.Text
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPRRefLocCode.DataSource = objLocDs.Tables(0)
        ddlPRRefLocCode.DataValueField = "LocCode"
        ddlPRRefLocCode.DataTextField = "Description"
        ddlPRRefLocCode.DataBind()
        ddlPRRefLocCode.SelectedIndex = intSelectedIndex
    End Sub

    Function GoodsReceived(ByVal pv_strPOLnId As String) As String
        Dim strParam As String
        Dim strOpCd As String = "PU_GET_PO_GOODS_RECEIVED"
        Dim objRsl As Object
        Dim intErrNo As Integer

        If Trim(pv_strPOLnId) = "" Then
            Return False
        End If

        strParam = pv_strPOLnId

        Try
            intErrNo = objPU.mtdGetPOGDRcv(strOpCd, strParam, objRsl)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PO_GDRCV&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        If objRsl.Tables(0).Rows.Count > 0 Then
            Return True
        End If

        Return False
    End Function

    Sub BindDeptCode(ByVal pv_strDeptCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_DEPTCODE_LIST_GET"
        Dim strParam As String
        Dim sortitem As String
        Dim SearchStr As String
        Dim drinsert As DataRow
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer

        sortitem = "ORDER BY DEPT.deptcode"
        SearchStr = ""
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objHR.mtdGetMasterList(strOpCd, strParam, objHR.EnumHRMasterType.DeptCode, objDataSet)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_PURREQ_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")

        End Try

        For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
            objDataSet.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("DeptCode"))
            objDataSet.Tables(0).Rows(intCnt).Item("Description") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("DeptCode")) & " ( " & _
                                                                            Trim(objDataSet.Tables(0).Rows(intCnt).Item("Description")) & " ) "

            If objDataSet.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(pv_strDeptCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drinsert = objDataSet.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Select Department Code"
        objDataSet.Tables(0).Rows.InsertAt(drinsert, 0)

        ddlDeptCode.DataSource = objDataSet.Tables(0)
        ddlDeptCode.DataValueField = "DeptCode"
        ddlDeptCode.DataTextField = "Description"
        ddlDeptCode.DataBind()
        ddlDeptCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindPOIssued(ByVal pv_strPOIssued As String)
        ddlPOIssued.Items.Clear()
        ddlPOIssued.Items.Add(New ListItem("Select RPH Issued Location", ""))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.JKT), objPU.EnumPOIssued.JKT))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PKU), objPU.EnumPOIssued.PKU))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.LMP), objPU.EnumPOIssued.LMP))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PLM), objPU.EnumPOIssued.PLM))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.BKL), objPU.EnumPOIssued.BKL))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.LOK), objPU.EnumPOIssued.LOK))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.MDN), objPU.EnumPOIssued.MDN))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.JMB), objPU.EnumPOIssued.JMB))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PDG), objPU.EnumPOIssued.PDG))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.ACH), objPU.EnumPOIssued.ACH))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PON), objPU.EnumPOIssued.PON))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.SAM), objPU.EnumPOIssued.SAM))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PLK), objPU.EnumPOIssued.PLK))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.BJR), objPU.EnumPOIssued.BJR))

        If Not Trim(pv_strPOIssued) = "" Then
            With ddlPOIssued
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strPOIssued)))
            End With
        End If
    End Sub

    Sub onload_PORPH(ByVal pv_strRPHId As String)
        Dim strOpCd As String = "PU_CLSTRX_RPH_PO_GET"
        Dim objRPHPODs As New Object()
        Dim strParam As String = pv_strRPHId & "|" & ""
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim strParamName As String
        Dim strParamValue As String
        'Try
        '    intErrNo = objPU.mtdGetRPHPO(strOpCd, strParam, objRPHPODs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_RPH_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_rphlist.aspx")
        'End Try

        'If objRPHPODs.Tables(0).Rows.Count > 0 Then
        '    For intCnt = 0 To objRPHPODs.Tables(0).Rows.Count - 1
        '        objRPHPODs.Tables(0).Rows(intCnt).Item("POID") = objRPHPODs.Tables(0).Rows(intCnt).Item("POID").Trim()
        '    Next intCnt
        'End If

        strParamName = "STRSEARCH"
        strParamValue = "Where a.RphID='" & pv_strRPHId & "'"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objRPHPODs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        dgRPHPO.DataSource = Nothing
        dgRPHPO.DataSource = objRPHPODs.Tables(0)
        dgRPHPO.DataBind()

    End Sub

    Sub Centralized_Type(ByVal Sender As Object, ByVal E As EventArgs)
        If chkCentralized.Checked = True Then
            Centralized_Yes.Visible = True
            Centralized_No.Visible = False
            chkCentralized.Text = "  Yes"
        Else
            Centralized_Yes.Visible = False
            Centralized_No.Visible = True
            chkCentralized.Text = "  No"
            BindINItem("")
        End If
    End Sub

    Sub BindCurrencyList(ByVal pv_strCurrencyCode As String)
        Dim strParam As String
        Dim strSearch As String
        Dim strSort As String
        Dim strOpCdGet As String = "CM_CLSSETUP_CURRENCY_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer
        Dim objCurrencyDs As New Object()

        strSearch = "and curr.Status = '" & objCMSetup.EnumCurrencyStatus.Active & "' "
        strSort = "order by curr.CurrencyCode "

        strParam = strSearch & "|" & strSort

        Try
            intErrNo = objCMSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objCurrencyDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_CURRENCYLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=CM/Trx/CM_Trx_ContractRegList.aspx")
        End Try

        If pv_strCurrencyCode = "" Then
            pv_strCurrencyCode = "IDR"
        End If
        If objCurrencyDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objCurrencyDs.Tables(0).Rows.Count - 1
                objCurrencyDs.Tables(0).Rows(intCnt).Item("CurrencyCode") = Trim(objCurrencyDs.Tables(0).Rows(intCnt).Item("CurrencyCode"))
                objCurrencyDs.Tables(0).Rows(intCnt).Item("Description") = objCurrencyDs.Tables(0).Rows(intCnt).Item("CurrencyCode")
                If objCurrencyDs.Tables(0).Rows(intCnt).Item("CurrencyCode") = pv_strCurrencyCode Then
                    intSelectedIndex = intCnt
                End If
            Next
        End If


        ddlCurrency.DataSource = objCurrencyDs.Tables(0)
        ddlCurrency.DataValueField = "CurrencyCode"
        ddlCurrency.DataTextField = "Description"
        ddlCurrency.DataBind()
        ddlCurrency.SelectedIndex = intSelectedIndex

    End Sub

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

    Protected Function LoadData(ByVal pName As String) As DataSet
        Dim strOpCode As String = "IN_CLSTRX_SUPPLIER_SEARCH"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strSortExp As String
        Dim objItemDs As New Object()

        strSortExp = " ORDER BY SupplierCode ASC"
        strParamName = "SEARCH|LOCCODE|SORTEXP"
        strParamValue = "%" & pName & "%||" & strSortExp

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objItemDs

    End Function

    Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

#End Region

    End Class
