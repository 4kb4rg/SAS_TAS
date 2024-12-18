

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
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.Drawing.Color
Imports System.Math
Imports System.Text


Public Class PU_PODet : Inherits Page

    Protected WithEvents lblErrManySelectDoc As Label
    Protected WithEvents lblErrRef As Label
    Protected WithEvents lblErrItem As Label
    Protected WithEvents lblPOId As Label
    Protected WithEvents lblPOType As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblLocCode As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblStockItem As Label
    Protected WithEvents lblSelectListLoc As Label
    Protected WithEvents lblSelectListItem As Label
    Protected WithEvents lblPR As Label
    Protected WithEvents lblPRRef As Label

    Protected WithEvents ddlSuppCode As DropDownList
    Protected WithEvents lblSuppCode As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblUpdateDate As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents ddlPRId As DropDownList
    Protected WithEvents lblPRLocCode As Label
    Protected WithEvents txtPRRefId As TextBox
    Protected WithEvents ddlPRRefLocCode As DropDownList
    Protected WithEvents lblNote As Label
    Protected WithEvents ddlItemCode As DropDownList
    Protected WithEvents txtQtyOrder As TextBox
    Protected WithEvents txtCost As TextBox
    Protected WithEvents dgPODet As DataGrid
    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents txtRemark As TextBox

    Protected WithEvents tblPOLine As HtmlTable
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnConfirm As ImageButton
    Protected WithEvents btnPrint As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnCancel As ImageButton
    Protected WithEvents lblHidStatus As Label
    Protected WithEvents lblPOTypeName As Label

    Protected WithEvents ddlDeptCode As DropDownList
    Protected WithEvents ddlPOIssued As DropDownList
    Protected WithEvents lblDeptCode As Label
    Protected WithEvents lblPOIssued As Label
    Protected WithEvents lblErrMessage As Label
    
    Protected objHR As New agri.HR.clsSetup()
    Dim objDataSet As New Object()

    Protected WithEvents lblErrItemExist As Label
    Protected WithEvents chkPPN As CheckBox

    Protected WithEvents lblValidationQtyOrder As Label
    Protected WithEvents hidOrgQtyOrder As HtmlInputHidden

    Protected WithEvents chkCentralized As CheckBox
    Protected WithEvents txtPRID As TextBox
    Protected WithEvents Centralized_Yes As HtmlTableRow
    Protected WithEvents Centralized_No As HtmlTableRow
    Protected WithEvents ddlCurrency As DropDownList
    Protected WithEvents txtExRate As TextBox
    Protected WithEvents txtAddNote As HtmlTextArea
    Protected WithEvents ddlTransporter As DropDownList
    Protected WithEvents txtAmtTransportFee As TextBox
    Protected WithEvents lblErrTransporter As Label
    Protected WithEvents txtDiscount As TextBox
    Protected WithEvents txtPBBKB As TextBox
    Protected WithEvents txtTtlCost As TextBox
    Protected WithEvents txtPPN22 As TextBox
    Protected WithEvents txtAddDisc As TextBox
    Protected WithEvents txtTtlAftDisc As TextBox
    Protected WithEvents hidTtlAmount As HtmlInputHidden
    Protected WithEvents hidTtlAmtAftDisc As HtmlInputHidden
    Protected WithEvents hidAddDisc As HtmlInputHidden
    Protected WithEvents lblErrExRate As Label

    Protected WithEvents lblDateCreated As Label
    Protected WithEvents txtDateCreated As TextBox
    Protected WithEvents btnDateCreated As Image
    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label

    Protected WithEvents FindIN As HtmlInputButton
    Protected WithEvents FindWS As HtmlInputButton
    Protected WithEvents FindDC As HtmlInputButton
    Protected WithEvents FindFA As HtmlInputButton
    Protected WithEvents FindNU As HtmlInputButton

    Protected WithEvents lblErrDdlPRID As Label
    Protected WithEvents lblErrTxtPRID As Label
    Protected WithEvents txtCreditTerm As TextBox
    Protected WithEvents lblErrCreditTerm As Label
    Protected WithEvents lblErrGR As Label
    Protected WithEvents btnAddendum As ImageButton
    Protected WithEvents btnEdited As ImageButton
    Protected WithEvents lblHidStatusEdited As Label
    Protected WithEvents lblHidPOlnId As Label
    Protected WithEvents ddlUOM As DropDownList
    Protected WithEvents BtnNew As ImageButton
    Protected WithEvents lblErrQtyOrder As Label

    Protected WithEvents lblErrPRRef As Label
    Protected WithEvents chkPPNTransport As CheckBox
    Protected WithEvents tblSPK As HtmlTableRow
    Protected WithEvents dgSPK As DataGrid

    Protected WithEvents lblErrPBBKB As Label
    Protected WithEvents txtPBBKBRate As TextBox

    Protected WithEvents txtPPH23 As TextBox
    Protected WithEvents chkGrossUp As CheckBox
    Protected WithEvents lblerrPPH23 As Label
    Protected WithEvents txtPPH23OA As TextBox
    Protected WithEvents chkGrossUpOA As CheckBox
    Protected WithEvents lblerrPPH23OA As Label
    Protected WithEvents hidOriCost As HtmlInputHidden
    Protected WithEvents hidOriCostOA As HtmlInputHidden

    Protected WithEvents chkPrinted As CheckBox
    Protected WithEvents txtPPH21 As TextBox
    Protected WithEvents chkGrossUp21 As CheckBox
    Protected WithEvents lblerrPPH21 As Label

    Protected WithEvents hidPPN As HtmlInputHidden
    Protected WithEvents hidPPNOA As HtmlInputHidden
    Protected WithEvents chkSurat As CheckBox

    Protected WithEvents btnGetRef As ImageButton
    Protected WithEvents lblRefNoErr As Label
    Protected WithEvents hidGetRefNo As HtmlInputHidden

    Protected objPU As New agri.PU.clsTrx()
    Protected objPUSetup As New agri.PU.clsSetup()
    Protected objIN As New agri.IN.clsTrx()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objAdmin As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objCMSetup As New agri.CM.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmShare As New agri.Admin.clsShare()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objLangCapDs As New DataSet()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminUOM As New agri.Admin.clsUom()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPUAR As Integer

    Dim strSelectedPOId As String
    Dim strSelectedPOType As String
    Dim strSelectedSuppCode As String
    Dim strSelectedPRId As String
    Dim strSelectedPRRefLocCode As String
    Dim strSelectedItemCode As String
    Dim blnSelectManyDoc As Boolean = False
    Dim strSelectedTransCode As String

    Dim strPhyYear As String
    Dim strPhyMonth As String
    Dim strLastPhyYear As String
    Dim strSelPOIssued As String
    Dim strSelDeptCode As String
    Dim strCurrency As String
    Dim strExRate As String
    Dim intLevel As Integer
    Dim strAcceptFormat As String
    Const ITEM_PART_SEPERATOR As String = " @ "
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String



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

        strPhyYear = Session("SS_PHYYEAR")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyMonth = strAccMonth
        strLastPhyYear = Session("SS_LASTPHYYEAR")
        strLocType = Session("SS_LOCTYPE")
        intLevel = Session("SS_USRLEVEL")
        Session("FixAJAXSysBug") = True


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblSuppCode.Visible = False
            lblDeptCode.Visible = False
            lblPOIssued.Visible = False
            lblErrRef.Visible = False
            lblErrItem.Visible = False
            lblValidationQtyOrder.Visible = False
            lblErrItemExist.Visible = False
            lblErrTransporter.Visible = False
            lblErrExRate.Visible = False
            lblErrDdlPRID.Visible = False
            lblErrTxtPRID.Visible = False
            lblErrCreditTerm.Visible = False
            lblErrGR.Visible = False
            btnEdited.Visible = False
            btnAddendum.Visible = False
            btnCancel.Visible = False
            lblDate.Visible = False
            lblErrQtyOrder.Visible = False
            lblErrPRRef.Visible = False
            lblErrPBBKB.Visible = False
            lblerrPPH23.Visible = False
            lblerrPPH23OA.Visible = False
            lblerrPPH21.Visible = False
            lblRefNoErr.Visible = False

            If blnSelectManyDoc = False Then
                lblErrManySelectDoc.ForeColor = Black
            End If

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            BtnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(BtnNew).ToString())
            btnSave.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnSave).ToString())
            btnConfirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnConfirm).ToString())
            btnPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnPrint).ToString())
            btnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnDelete).ToString())
            btnUnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnUnDelete).ToString())
            btnBack.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnBack).ToString())
            btnEdited.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnEdited).ToString())
            btnAddendum.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAddendum).ToString())

            strSelectedPOId = Trim(IIf(Request.QueryString("POId") = "", Request.Form("POId"), Request.QueryString("POId")))
            strSelectedPOType = Trim(IIf(Request.QueryString("POType") = "", Request.Form("POType"), Request.QueryString("POType")))
            'If strSelectedPOId = "" Then
            '    If strSelectedPOType = objPU.EnumPOType.Stock Or strSelectedPOType = objPU.EnumPOType.FixedAsset Or strSelectedPOType = objPU.EnumPOType.DirectCharge Then
            '        ddlDeptCode.Enabled = True
            '        ddlPOIssued.Enabled = True
            '    Else
            '        ddlDeptCode.Enabled = False
            '        ddlPOIssued.Enabled = False
            '    End If
            'End If

            onload_GetLangCap()

            If Not IsPostBack Then
                If strSelectedPOId <> "" Then
                    onLoad_DisplayPO(strSelectedPOId)
                    onLoad_DisplayPOLn(strSelectedPOId)
                    BindSupp(strSelectedPRId)
                    BindPR(strSelectedPRId)
                    BindLoc(strSelectedPRId)
                    If lblPOType.Text = objPU.EnumPOType.DirectCharge Then
                        btnGetRef.Visible = True
                    End If
                Else
                    If strSelectedPOType <> "" Then
                        lblPOType.Text = strSelectedPOType
                        If lblPOType.Text = objPU.EnumPOType.Stock Then
                            lblPOTypeName.Text = "Stock / Workshop"
                            txtPPH23.Enabled = False
                            chkGrossUp.Enabled = False
                            txtPPH21.Enabled = False
                            chkGrossUp21.Enabled = False
                            chkSurat.Enabled = False
                        ElseIf lblPOType.Text = objPU.EnumPOType.DirectCharge Then
                            txtPPH23.Enabled = True
                            chkGrossUp.Enabled = True
                            txtPPH23.ReadOnly = False
                            txtPPH21.Enabled = True
                            chkGrossUp21.Enabled = True
                            txtPPH21.ReadOnly = False
                            chkSurat.Enabled = True
                            btnGetRef.Visible = True
                        Else
                            lblPOTypeName.Text = objPU.mtdGetPOType(CInt(strSelectedPOType))
                            chkSurat.Enabled = False
                        End If
                    End If

                    lblHidStatusEdited.Text = "0"
                    txtDateCreated.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    btnDelete.Visible = False
                    btnUnDelete.Visible = False
                    btnPrint.Visible = False
                    btnConfirm.Visible = False

                    BindSupp("")
                    'BindPR("")
                    BindLoc("")
                    'BindINItem("")
                    BindDeptCode("")
                    BindPOIssued("")
                    BindCurrencyList("")
                    Centralized_Type(Sender, E)
                End If
                BindUOM("")
                CheckType()
                If lblPOType.Text = objPU.EnumPOType.Stock Then
                    lblPOTypeName.Text = "Stock / Workshop"
                    txtPPH23.Enabled = False
                    chkGrossUp.Enabled = False
                    txtPPH21.Enabled = False
                    chkGrossUp21.Enabled = False
                End If
            End If
            End If
    End Sub

    Public Shared Sub ShowArray(ByVal theArray As Array)
        Dim o As Object
        For Each o In theArray
            Console.Write("[{0}]", o)
        Next o
        Console.WriteLine()
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblLocCode.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblStockItem.Text = GetCaption(objLangCap.EnumLangCap.StockItem)
        lblErrRef.Text = "Please enter PR Ref. No. and PR Ref. " & GetCaption(objLangCap.EnumLangCap.Location)

        'dgPODet.Columns(7).HeaderText = lblPR.Text & lblLocCode.Text
        'dgPODet.Columns(9).HeaderText = lblPRRef.Text & lblLocation.Text
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
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub onLoad_DisplayPO(ByVal pv_strPOId As String)
        Dim strOpCd As String = "PU_CLSTRX_PO_GET"
        Dim objPODs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String = pv_strPOId & "|" & strLocation & "|" & lblPOType.Text & "|||||A.POId||||"
        Dim intCnt As Integer = 0
        Dim blnDisplayCancel As Boolean

        Try
            intErrNo = objPU.mtdGetPO(strOpCd, strParam, objPODs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        lblPOId.Text = pv_strPOId
        lblAccPeriod.Text = objPODs.Tables(0).Rows(0).Item("AccMonth") & "/" & objPODs.Tables(0).Rows(0).Item("AccYear")
        lblPOType.Text = objPODs.Tables(0).Rows(0).Item("POType").Trim()
        txtDateCreated.Enabled = True
        If objPODs.Tables(0).Rows(0).Item("POType") = objPU.EnumPOType.Stock Then
            lblPOTypeName.Text = "Stock / Workshop"
        Else
            lblPOTypeName.Text = objPU.mtdGetPOType(objPODs.Tables(0).Rows(0).Item("POType"))
        End If
        strSelectedPOType = objPODs.Tables(0).Rows(0).Item("POType")

        lblStatus.Text = objPU.mtdGetPOStatus(objPODs.Tables(0).Rows(0).Item("Status"))
        lblHidStatus.Text = objPODs.Tables(0).Rows(0).Item("Status").Trim()
        strSelectedSuppCode = objPODs.Tables(0).Rows(0).Item("SupplierCode").Trim()
        lblCreateDate.Text = objGlobal.GetLongDate(objPODs.Tables(0).Rows(0).Item("CreateDate"))
        lblUpdateDate.Text = objGlobal.GetLongDate(objPODs.Tables(0).Rows(0).Item("UpdateDate"))
        lblPrintDate.Text = objGlobal.GetLongDate(objPODs.Tables(0).Rows(0).Item("PrintDate"))
        lblUpdateBy.Text = objPODs.Tables(0).Rows(0).Item("UserName")
        lblTotalAmount.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(objPODs.Tables(0).Rows(0).Item("TotalAmount"), 0))
        txtRemark.Text = objPODs.Tables(0).Rows(0).Item("Remark").Trim()

        strSelPOIssued = objPODs.Tables(0).Rows(0).Item("POIssued").Trim()
        strSelDeptCode = objPODs.Tables(0).Rows(0).Item("DeptCode").Trim()
        BindDeptCode(objPODs.Tables(0).Rows(0).Item("DeptCode").Trim())
        BindPOIssued(objPODs.Tables(0).Rows(0).Item("POIssued").Trim())
        BindCurrencyList(Trim(objPODs.Tables(0).Rows(0).Item("CurrencyCode")))
        txtExRate.Text = objPODs.Tables(0).Rows(0).Item("ExchangeRate")
        lblTotalAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objPODs.Tables(0).Rows(0).Item("TotalAmountCurrency") + objPODs.Tables(0).Rows(0).Item("AddDiscount"), 2), 2)
        hidTtlAmount.Value = objPODs.Tables(0).Rows(0).Item("TotalAmountCurrency") + objPODs.Tables(0).Rows(0).Item("AddDiscount")
        hidAddDisc.Value = objPODs.Tables(0).Rows(0).Item("AddDiscount")
        txtAddDisc.Text = objPODs.Tables(0).Rows(0).Item("AddDiscount")
        txtTtlAftDisc.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objPODs.Tables(0).Rows(0).Item("TotalAmountCurrency"), 2), 2)
        txtDateCreated.Text = Date_Validation(objPODs.Tables(0).Rows(0).Item("CreateDate"), True)
        txtDateCreated.Enabled = False
        txtCreditTerm.Text = objPODs.Tables(0).Rows(0).Item("CreditTerm")
        lblHidStatusEdited.Text = objPODs.Tables(0).Rows(0).Item("Edited").Trim()
        hidPPN.Value = objPODs.Tables(0).Rows(0).Item("PPNInit").Trim()

        strAccMonth = objPODs.Tables(0).Rows(0).Item("AccMonth")
        strAccYear = objPODs.Tables(0).Rows(0).Item("AccYear")

        If hidPPN.Value = "1" Then
            chkPPN.Checked = True
        Else
            chkPPN.Checked = False
        End If
        chkPPN.Enabled = False

        If lblPOType.Text = objPU.EnumPOType.DirectCharge Then
            If chkSurat.Checked = True Then
                chkPPN.Checked = False
            Else
                chkPPN.Checked = IIf(Trim(hidPPN.Value) = "0", False, True)
            End If
        Else
            chkSurat.Enabled = False
        End If

        If Trim(objPODs.Tables(0).Rows(0).Item("Centralized")) = "1" Then
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

        If Trim(objPODs.Tables(0).Rows(0).Item("Printed")) = "1" Then
            chkPrinted.Checked = True
        Else
            chkPrinted.Checked = False
        End If

        Select Case objPODs.Tables(0).Rows(0).Item("Status")
            Case objPU.EnumPOStatus.Active
                tblPOLine.Visible = True
                ddlSuppCode.Enabled = True
                txtRemark.Enabled = True
                btnSave.Visible = True
                'If intLevel = 0 Then
                '    btnConfirm.Visible = False
                '    txtExRate.Enabled = False
                'Else
                '    btnConfirm.Visible = True
                '    txtExRate.Enabled = True
                'End If
                'TEMPORARY
                btnConfirm.Visible = True
                txtExRate.Enabled = True
                '----
                btnDelete.Visible = True
                btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                btnUnDelete.Visible = False
                btnCancel.Visible = False
                btnPrint.Visible = True
                ddlDeptCode.Enabled = True
                ddlPOIssued.Enabled = True
                chkCentralized.Enabled = False
                ddlCurrency.Enabled = True
                '     txtDateCreated.Enabled = True
                btnEdited.Visible = False
                btnAddendum.Visible = False

                If lblHidStatusEdited.Text = "1" Then
                    txtDateCreated.Enabled = False
                    ddlSuppCode.Enabled = False
                ElseIf lblHidStatusEdited.Text = "2" Then
                    txtDateCreated.Enabled = False
                    ddlSuppCode.Enabled = False
                    ddlItemCode.Enabled = False
                End If

            Case objPU.EnumPOStatus.Confirmed
                ' -- to find out whether any line contained QtyReceived/QtyInvoiced
                Try
                    intErrNo = objPU.mtdGetDocStatusAction(strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           True, False, False, _
                                                           pv_strPOId, _
                                                           blnDisplayCancel)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PO_CANCEL_BUTTON&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
                End Try

                If blnDisplayCancel = True Then
                    btnCancel.Visible = True
                Else
                    btnCancel.Visible = False
                End If

                tblPOLine.Visible = False
                ddlSuppCode.Enabled = False
                txtRemark.Enabled = False
                btnSave.Visible = False
                btnConfirm.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Visible = False
                btnPrint.Visible = True
                ddlDeptCode.Enabled = False
                ddlPOIssued.Enabled = False
                chkCentralized.Enabled = False
                ddlCurrency.Enabled = False
                txtExRate.Enabled = False
                txtDateCreated.Enabled = False
                txtCreditTerm.Enabled = False
                onLoad_DisplaySPK(lblPOId.Text)

            Case objPU.EnumPOStatus.Deleted
                tblPOLine.Visible = False
                ddlSuppCode.Enabled = False
                txtRemark.Enabled = False
                btnSave.Visible = False
                btnConfirm.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                btnUnDelete.Visible = True
                btnCancel.Visible = False
                btnPrint.Visible = False
                ddlDeptCode.Enabled = False
                ddlPOIssued.Enabled = False
                chkCentralized.Enabled = False
                ddlCurrency.Enabled = False
                txtExRate.Enabled = False
                txtDateCreated.Enabled = False
                btnEdited.Visible = False
                btnAddendum.Visible = False
                txtCreditTerm.Enabled = False

            Case objPU.EnumPOStatus.Cancelled, objPU.EnumPOStatus.Invoiced
                tblPOLine.Visible = False
                ddlSuppCode.Enabled = False
                txtRemark.Enabled = False
                btnSave.Visible = False
                btnConfirm.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Visible = False
                btnCancel.Visible = False
                btnPrint.Visible = False
                ddlDeptCode.Enabled = False
                ddlPOIssued.Enabled = False
                chkCentralized.Enabled = False
                ddlCurrency.Enabled = False
                txtExRate.Enabled = False
                txtDateCreated.Enabled = False
                btnEdited.Visible = False
                btnAddendum.Visible = False
                txtCreditTerm.Enabled = False
                onLoad_DisplaySPK(lblPOId.Text)

            Case Else
                tblPOLine.Visible = False
                ddlSuppCode.Enabled = False
                txtRemark.Enabled = False
                btnSave.Visible = False
                btnConfirm.Visible = False
                btnDelete.Visible = False
                btnUnDelete.Visible = False
                btnCancel.Visible = False
                btnPrint.Visible = False
                ddlDeptCode.Enabled = False
                ddlPOIssued.Enabled = False
                chkCentralized.Enabled = False
                ddlCurrency.Enabled = False
                txtExRate.Enabled = False
                txtDateCreated.Enabled = False
                txtCreditTerm.Enabled = False
                onLoad_DisplaySPK(lblPOId.Text)
        End Select

        Dim strOpCode As String = "PU_CLSTRX_PO_GR_GET"
        Dim dsMaster As Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strGRID As String

        strParamName = "POID"
        strParamValue = Trim(lblPOId.Text)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_POdet")
        End Try

        If dsMaster.Tables(0).Rows.Count > 0 Then
            strGRID = Trim(dsMaster.Tables(0).Rows(0).Item("GoodsRcvID"))
            If objPODs.Tables(0).Rows(0).Item("Status") = objPU.EnumPOStatus.Active Then
                btnAddendum.Visible = False
            Else
                btnAddendum.Visible = True
                btnAddendum.Attributes("onclick") = "javascript:return ConfirmAction('generate addendum');"
            End If
            lblErrGR.Visible = True
            'lblErrGR.Text = lblErrGR.Text + " Goods Receive ID = " + strGRID
            btnEdited.Visible = False
        Else
            If objPODs.Tables(0).Rows(0).Item("Status") = objPU.EnumPOStatus.Active Then
                btnEdited.Visible = False
            Else
                btnEdited.Visible = True
                btnEdited.Attributes("onclick") = "javascript:return ConfirmAction('edit');"
            End If
            lblErrGR.Visible = False
            btnAddendum.Visible = False
            strGRID = ""
        End If
    End Sub

    Sub onLoad_DisplayPOLn(ByVal pv_strPOId As String)
        Dim strOpCd As String = "PU_CLSTRX_PO_LINE_DETAILS_GET"
        Dim objPOLnDs As New Object()
        Dim strParam As String = pv_strPOId & "|"
        Dim DelButton As LinkButton
        Dim EdtButton As LinkButton
        Dim UpdButton As LinkButton
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim lbl As Label

        Try
            intErrNo = objPU.mtdGetPOLn(strOpCd, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strParam, _
                                        objPOLnDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_POLn&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        If objPOLnDs.Tables(0).Rows.Count > 0 Then
            chkPPN.Enabled = False
            'chkPPN.Checked = IIf(objPOLnDs.Tables(0).Rows(0).Item("PPN") = objPU.EnumPPN.Yes, True, False)
            'chkPPN.Checked = IIf(Trim(splPPN) = "0", False, True)
            chkPPNTransport.Enabled = False
        Else
            'chkPPN.Enabled = True
            'chkPPN.Checked = True

            hidAddDisc.Value = "0"
            txtAddDisc.Text = "0"
            txtTtlAftDisc.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(hidTtlAmount.Value, 2), 2)
            chkPPNTransport.Enabled = True
        End If

        For intCnt = 0 To objPOLnDs.Tables(0).Rows.Count - 1
            objPOLnDs.Tables(0).Rows(intCnt).Item("POId") = objPOLnDs.Tables(0).Rows(intCnt).Item("POId").Trim()
            objPOLnDs.Tables(0).Rows(intCnt).Item("PRId") = objPOLnDs.Tables(0).Rows(intCnt).Item("PRId").Trim()
            objPOLnDs.Tables(0).Rows(intCnt).Item("PRLocCode") = objPOLnDs.Tables(0).Rows(intCnt).Item("PRLocCode").Trim()
            objPOLnDs.Tables(0).Rows(intCnt).Item("PRRefId") = objPOLnDs.Tables(0).Rows(intCnt).Item("PRRefId").Trim()
            objPOLnDs.Tables(0).Rows(intCnt).Item("PRRefLocCode") = objPOLnDs.Tables(0).Rows(intCnt).Item("PRRefLocCode").Trim()
            objPOLnDs.Tables(0).Rows(intCnt).Item("ItemCode") = objPOLnDs.Tables(0).Rows(intCnt).Item("ItemCode").Trim()
            objPOLnDs.Tables(0).Rows(intCnt).Item("Description") = objPOLnDs.Tables(0).Rows(intCnt).Item("ItemCode").Trim() & " (" & _
                                                                   objPOLnDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            objPOLnDs.Tables(0).Rows(intCnt).Item("Status") = objPOLnDs.Tables(0).Rows(intCnt).Item("Status").Trim()
        Next intCnt

        dgPODet.DataSource = objPOLnDs.Tables(0)
        dgPODet.DataBind()

        For intCnt = 0 To objPOLnDs.Tables(0).Rows.Count - 1
            Select Case Trim(lblStatus.Text)
                Case objPU.mtdGetPOStatus(objPU.EnumPOStatus.Active)
                    EdtButton = dgPODet.Items.Item(intCnt).FindControl("Edit")
                    DelButton = dgPODet.Items.Item(intCnt).FindControl("Delete")
                    UpdButton = dgPODet.Items.Item(intCnt).FindControl("Cancel")
                    UpdButton.Visible = False

                    If lblHidStatusEdited.Text = "0" Then
                        EdtButton.Visible = True
                        DelButton.Visible = True                       
                        DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Else
                        EdtButton.Visible = True
                        DelButton.Visible = False
                    End If
                    'If lblHidStatusEdited.Text <> "2" Then
                    '    If Right(Trim(pv_strPOId), 3) = "ADD" Then
                    '        EdtButton.Visible = True
                    '        DelButton.Visible = False
                    '    Else
                    '        EdtButton.Visible = False
                    '        DelButton.Visible = True
                    '        DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    '    End If
                    'Else
                    '    EdtButton.Visible = True
                    '    DelButton.Visible = False
                    'End If
                Case objPU.mtdGetPOStatus(objPU.EnumPOStatus.Confirmed)
                    Select Case objPOLnDs.Tables(0).Rows(intCnt).Item("Status")
                        Case objPU.EnumPOLnStatus.Active
                            UpdButton = dgPODet.Items.Item(intCnt).FindControl("Delete")
                            UpdButton.Visible = False
                        Case objPU.EnumPOLnStatus.Cancelled
                            UpdButton = dgPODet.Items.Item(intCnt).FindControl("Cancel")
                            UpdButton.Visible = False
                            UpdButton = dgPODet.Items.Item(intCnt).FindControl("Delete")
                            UpdButton.Visible = False
                    End Select

                    If GoodsReceived(objPOLnDs.Tables(0).Rows(intCnt).Item("POLnId")) = True Then
                        UpdButton = dgPODet.Items.Item(intCnt).FindControl("Cancel")
                        UpdButton.Visible = False
                        btnCancel.Visible = False
                    End If

                    'cancelling item which have qtyreceive=0, sometime already had receive but had return either
                    If objPOLnDs.Tables(0).Rows(intCnt).Item("QtyReceive") = 0 Then
                        UpdButton = dgPODet.Items.Item(intCnt).FindControl("Cancel")
                        UpdButton.Visible = True
                    Else
                        UpdButton = dgPODet.Items.Item(intCnt).FindControl("Cancel")
                        UpdButton.Visible = False
                    End If

                    EdtButton = dgPODet.Items.Item(intCnt).FindControl("Edit")
                    EdtButton.Visible = False
                    DelButton = dgPODet.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = False

                Case objPU.mtdGetPOStatus(objPU.EnumPOStatus.Confirmed), objPU.mtdGetPOStatus(objPU.EnumPOStatus.Deleted), objPU.mtdGetPOStatus(objPU.EnumPOStatus.Cancelled), objPU.mtdGetPOStatus(objPU.EnumPOStatus.Invoiced), objPU.mtdGetPOStatus(objPU.EnumPOStatus.Closed)
                    EdtButton = dgPODet.Items.Item(intCnt).FindControl("Edit")
                    EdtButton.Visible = False
                    DelButton = dgPODet.Items.Item(intCnt).FindControl("Delete")
                    DelButton.Visible = False
                    UpdButton = dgPODet.Items.Item(intCnt).FindControl("Cancel")
                    UpdButton.Visible = False
            End Select

            lbl = dgPODet.Items.Item(intCnt).FindControl("lblPOlnStatus")
            If CInt(lbl.Text) = objPU.EnumPOLnStatus.Cancelled Then
                dgPODet.Items.Item(intCnt).ForeColor = Drawing.Color.Red
                lbl.Text = objPU.mtdGetPOLnStatus(objPU.EnumPOLnStatus.Cancelled)
                lbl.Visible = True

                UpdButton = dgPODet.Items.Item(intCnt).FindControl("Cancel")
                UpdButton.Visible = False
            Else
                lbl.Visible = False
            End If
        Next intCnt
    End Sub

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
        drinsert(0) = " "
        drinsert(1) = "Select Department Code"
        objDataSet.Tables(0).Rows.InsertAt(drinsert, 0)

        ddlDeptCode.DataSource = objDataSet.Tables(0)
        ddlDeptCode.DataValueField = "DeptCode"
        ddlDeptCode.DataTextField = "Description"
        ddlDeptCode.DataBind()
        ddlDeptCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindPOIssued(ByVal pv_strPOIssued As String)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        ddlPOIssued.Items.Clear()
        '        ddlPOIssued.Items.Add(New ListItem("Select PO Issued Location", ""))
        '        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.JKT), objPU.EnumPOIssued.JKT))
        '        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.LMP), objPU.EnumPOIssued.LMP))
        '        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.BKL), objPU.EnumPOIssued.BKL))
        '        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.LOK), objPU.EnumPOIssued.LOK))
        '        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.MDN), objPU.EnumPOIssued.MDN))
        '    Case Else
        ddlPOIssued.Items.Clear()
        ddlPOIssued.Items.Add(New ListItem("Select PO Issued Location", ""))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.MDN), objPU.EnumPOIssued.MDN))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.JKT), objPU.EnumPOIssued.JKT))
        ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.LOK), objPU.EnumPOIssued.LOK))
        'End Select

        'ddlPOIssued.Items.Clear()
        'ddlPOIssued.Items.Add(New ListItem("Select PO Issued Location", ""))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.JKT), objPU.EnumPOIssued.JKT))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PKU), objPU.EnumPOIssued.PKU))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.LMP), objPU.EnumPOIssued.LMP))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PLM), objPU.EnumPOIssued.PLM))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.BKL), objPU.EnumPOIssued.BKL))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.LOK), objPU.EnumPOIssued.LOK))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.MDN), objPU.EnumPOIssued.MDN))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.JMB), objPU.EnumPOIssued.JMB))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PDG), objPU.EnumPOIssued.PDG))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.ACH), objPU.EnumPOIssued.ACH))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PON), objPU.EnumPOIssued.PON))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.SAM), objPU.EnumPOIssued.SAM))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PLK), objPU.EnumPOIssued.PLK))
        'ddlPOIssued.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.BJR), objPU.EnumPOIssued.BJR))

        If Not Trim(pv_strPOIssued) = "" Then
            With ddlPOIssued
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strPOIssued)))
            End With
        Else
            If Session("SS_LOCLEVEL") <> objAdmin.EnumLocLevel.HQ Then
                ddlPOIssued.SelectedIndex = 3
            Else
                ddlPOIssued.SelectedIndex = 1
            End If
        End If
    End Sub

    Sub BindSupp(ByVal pv_strPRId As String)
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim objSuppDs As New Object()
        Dim strSuppCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim strSuppType As String = objPUSetup.EnumSupplierType.Internal & "','" & objPUSetup.EnumSupplierType.External & "','" & objPUSetup.EnumSupplierType.Associate & "','" & objPUSetup.EnumSupplierType.Contractor

        strSuppCode = IIf(pv_strPRId = "", "", strSelectedSuppCode)
        strParam = strSuppCode & "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||SELECT"
        strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")
        strParam = strParam & "|" & strSuppType

        Try
            intErrNo = objPUSetup.mtdGetSupplier(strOpCd, strParam, objSuppDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_SUPPLIER&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objSuppDs.Tables(0).Rows.Count - 1
            objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode").Trim()
            objSuppDs.Tables(0).Rows(intCnt).Item("Name") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") & " (" & objSuppDs.Tables(0).Rows(intCnt).Item("Name").Trim() & ")" & " - " & objSuppDs.Tables(0).Rows(intCnt).Item("CreditTerm").Trim() & " days"

            If Trim(objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode")) = Trim(strSelectedSuppCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = objSuppDs.Tables(0).NewRow()
        dr("SupplierCode") = ""
        dr("Name") = "Please Select Supplier Code"
        objSuppDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSuppCode.DataSource = objSuppDs.Tables(0)
        ddlSuppCode.DataValueField = "SupplierCode"
        ddlSuppCode.DataTextField = "Name"
        ddlSuppCode.DataBind()
        ddlSuppCode.SelectedIndex = intSelectedIndex

        ddlTransporter.DataSource = objSuppDs.Tables(0)
        ddlTransporter.DataValueField = "SupplierCode"
        ddlTransporter.DataTextField = "Name"
        ddlTransporter.DataBind()
        'ddlTransporter.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindTransporter(ByVal pv_strPRId As String)
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim objSuppDs As New Object()
        Dim strSuppCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim strSuppType As String = objPUSetup.EnumSupplierType.Internal & "','" & objPUSetup.EnumSupplierType.External & "','" & objPUSetup.EnumSupplierType.Associate & "','" & objPUSetup.EnumSupplierType.Contractor

        strSuppCode = IIf(pv_strPRId = "", "", strSelectedSuppCode)
        strParam = strSuppCode & "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||SELECT"
        strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")
        strParam = strParam & "|" & strSuppType

        Try
            intErrNo = objPUSetup.mtdGetSupplier(strOpCd, strParam, objSuppDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_SUPPLIER&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objSuppDs.Tables(0).Rows.Count - 1
            objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode").Trim()
            objSuppDs.Tables(0).Rows(intCnt).Item("Name") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") & " (" & objSuppDs.Tables(0).Rows(intCnt).Item("Name").Trim() & ")"

            If objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = strSelectedTransCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = objSuppDs.Tables(0).NewRow()
        dr("SupplierCode") = ""
        dr("Name") = "Please Select Transporter Code"
        objSuppDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTransporter.DataSource = objSuppDs.Tables(0)
        ddlTransporter.DataValueField = "SupplierCode"
        ddlTransporter.DataTextField = "Name"
        ddlTransporter.DataBind()
        ddlTransporter.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindPR(ByVal pv_strPRId As String)
        Dim strOpCd As String = "PU_CLSTRX_PR_ID_GET"
        Dim objPRDs As New Object()
        Dim strParam As String = ""
        Dim dr As DataRow
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strPOType As String

        If lblPOType.Text = objPU.EnumPOType.Stock Then
            strPOType = objIN.EnumPurReqDocType.StockPR & "','" & objIN.EnumPurReqDocType.WorkshopPR
        Else
            strPOType = lblPOType.Text
        End If

        strParam = "|" & strPOType & "|" & objIN.EnumPurReqStatus.Confirmed & "|" & strLocation


        Try
            intErrNo = objPU.mtdGetPR_New(strOpCd, strAccMonth, strAccYear, strParam, objPRDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_PR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objPRDs.Tables(0).Rows.Count - 1
            objPRDs.Tables(0).Rows(intCnt).Item("PRId") = objPRDs.Tables(0).Rows(intCnt).Item("PRId").Trim()
            objPRDs.Tables(0).Rows(intCnt).Item("DispPRId") = objPRDs.Tables(0).Rows(intCnt).Item("PRId").Trim()
            objPRDs.Tables(0).Rows(intCnt).Item("LocCode") = objPRDs.Tables(0).Rows(intCnt).Item("LocCode").Trim()

            If objPRDs.Tables(0).Rows(intCnt).Item("PRId") = strSelectedPRId Then
                intSelectedIndex = intCnt + 1
                lblPRLocCode.Text = objPRDs.Tables(0).Rows(intCnt).Item("LocCode")
            End If
        Next intCnt

        dr = objPRDs.Tables(0).NewRow()
        dr("PRId") = ""
        dr("DispPRId") = "Please Select Purchase Requisition ID"
        objPRDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPRId.DataSource = objPRDs.Tables(0)
        ddlPRId.DataValueField = "PRId"
        ddlPRId.DataTextField = "DispPRId"
        ddlPRId.DataBind()
        ddlPRId.SelectedIndex = intSelectedIndex
        strSelectedPRId = ddlPRId.SelectedItem.Value

        If chkCentralized.Checked = True Then
            If ddlPRId.SelectedItem.Value = "" Then
                BindINItem("")
            Else
                BindPRItem(strSelectedPRId)
            End If
        Else
            BindINItem("")
        End If
    End Sub

    Sub BindPRItem(ByVal pv_strPRId As String)
        Dim strOpCd As String = "PU_CLSTRX_PR_LINE_GET"
        Dim objINItemDs As New Object()
        Dim strItemCode As String
        Dim strParam As String = ""
        Dim dr As DataRow
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strPOType As String
        Dim dbQtyReq As Double

        If ddlPRId.SelectedItem.Value <> "" Then
            If lblPOType.Text = objPU.EnumPOType.Stock Then
                strPOType = objIN.EnumPurReqDocType.StockPR & "','" & objIN.EnumPurReqDocType.WorkshopPR
            Else
                strPOType = lblPOType.Text
            End If

            strItemCode = IIf(pv_strPRId = "", "", strSelectedItemCode)

            strParam = strSelectedPRId & "|" & _
                       strPOType & "|" & _
                       objIN.EnumPurReqLnStatus.Approved & "|" & _
                       strItemCode

            Try
                intErrNo = objPU.mtdGetItemList(strOpCd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                objINItemDs)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_PRStockItem&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
            End Try

            For intCnt = 0 To objINItemDs.Tables(0).Rows.Count - 1

                If lblPOType.Text = objPU.EnumPOType.Stock Then
                    If objINItemDs.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then
                        objINItemDs.Tables(0).Rows(intCnt).Item("Description") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("PartNo") & " (" & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                    "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("Cost"), 0) & ", " & _
                                                                    objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding"), 5) & ", " & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("UOMCode") & " ( " & _
                                                                    objGlobal.GetIDDecimalSeparator_FreeDigit(objPU.mtdGetQty(Trim(strLocation), objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode"), False, objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding")), 5) & ", " & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") & " ) "

                        objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                              objINItemDs.Tables(0).Rows(intCnt).Item("PartNo")

                    Else
                        objINItemDs.Tables(0).Rows(intCnt).Item("Description") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                   objINItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                   "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("Cost"), 0) & ", " & _
                                                                   objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding"), 5) & ", " & _
                                                                   objINItemDs.Tables(0).Rows(intCnt).Item("UOMCode") & " ( " & _
                                                                   objGlobal.GetIDDecimalSeparator_FreeDigit(objPU.mtdGetQty(Trim(strLocation), objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode"), False, objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding")), 5) & ", " & _
                                                                   objINItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") & " ) "
                    End If
                Else
                    objINItemDs.Tables(0).Rows(intCnt).Item("Description") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                    "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("Cost"), 0) & ", " & _
                                                                    objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding"), 5) & ", " & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("UOMCode") & " ( " & _
                                                                    objGlobal.GetIDDecimalSeparator_FreeDigit(objPU.mtdGetQty(Trim(strLocation), objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode"), False, objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding")), 5) & ", " & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") & " ) "
                End If

                If objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = strSelectedItemCode Then
                    intSelectedIndex = intCnt + 1
                    dbQtyReq = objPU.mtdGetQty(Trim(strLocation), _
                                                       Trim(strSelectedItemCode), _
                                                       False, _
                                                       objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding"))

                    txtQtyOrder.Text = Round(dbQtyReq, 2)
                    hidOrgQtyOrder.Value = objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding")
                    txtCost.Text = "0"
                    txtAddNote.Value = Trim(objINItemDs.Tables(0).Rows(intCnt).Item("AdditionalNote"))
                End If
            Next intCnt

            dr = objINItemDs.Tables(0).NewRow()
            dr("ItemCode") = ""
            dr("Description") = lblSelectListItem.Text & lblStockItem.Text
            objINItemDs.Tables(0).Rows.InsertAt(dr, 0)

            ddlItemCode.DataSource = objINItemDs.Tables(0)
            ddlItemCode.DataValueField = "ItemCode"
            ddlItemCode.DataTextField = "Description"
            ddlItemCode.DataBind()

            ddlItemCode.SelectedIndex = intSelectedIndex
            If ddlItemCode.SelectedItem.Value = "" Then
                txtQtyOrder.Text = "0"
                txtCost.Text = "0"
                txtAddNote.Value = ""
                txtAmtTransportFee.Text = "0"
                txtDiscount.Text = "0"
                txtPBBKB.Text = "0"
                txtPBBKBRate.Text = "0"
                txtPPN22.Text = "0"
                txtTtlCost.Text = "0"
                txtPPH23.Text = "0"
                chkGrossUp.Checked = False
                txtPPH23OA.Text = "0"
                chkGrossUpOA.Checked = False
                txtPPH21.Text = "0"
                chkGrossUp21.Checked = False
            End If
        End If
    End Sub

    Sub PRIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strOpCd As String = "PU_CLSTRX_PR_LINE_GET"
        Dim objPRDs As New Object()
        Dim objINItemDs As New Object()
        Dim objPRMiscDs As New Object()
        Dim strParam As String = ""
        Dim dr As DataRow
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer
        Dim strPOType As String

        If ddlPRId.SelectedItem.Value <> "" Then
            strSelectedPRId = ddlPRId.SelectedItem.Value

            BindPR(strSelectedPRId)

            If lblPOType.Text = objPU.EnumPOType.Stock Then
                strPOType = objIN.EnumPurReqDocType.StockPR & "','" & objIN.EnumPurReqDocType.WorkshopPR
            Else
                strPOType = lblPOType.Text
            End If

            strParam = ddlPRId.SelectedItem.Value & "|" & _
                       strPOType & "|" & _
                       objIN.EnumPurReqLnStatus.Approved & "|"

            Try
                intErrNo = objPU.mtdGetItemList(strOpCd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                objINItemDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PRStockItem&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
            End Try

            For intCnt = 0 To objINItemDs.Tables(0).Rows.Count - 1

                'If lblPOType.Text = objPU.EnumPOType.Stock Then
                '    If objPRStockItemDs.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then
                '        objPRStockItemDs.Tables(0).Rows(intCnt).Item("Description") = objPRStockItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                '                                                                      objPRStockItemDs.Tables(0).Rows(intCnt).Item("PartNo") & " (" & _
                '                                                                      objPRStockItemDs.Tables(0).Rows(intCnt).Item("Description") & ")," & _
                '                                                                      objPRStockItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")
                '        objPRStockItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = objPRStockItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & objPRStockItemDs.Tables(0).Rows(intCnt).Item("PartNo")
                '    Else
                '        objPRStockItemDs.Tables(0).Rows(intCnt).Item("Description") = objPRStockItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                '                                                                      objPRStockItemDs.Tables(0).Rows(intCnt).Item("Description") & ")," & _
                '                                                                      objPRStockItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")
                '    End If
                'Else
                '    objPRStockItemDs.Tables(0).Rows(intCnt).Item("Description") = objPRStockItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                '                                                                  objPRStockItemDs.Tables(0).Rows(intCnt).Item("Description") & ")," & _
                '                                                                  objPRStockItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")
                'End If
                If lblPOType.Text = objPU.EnumPOType.Stock Then
                    If objINItemDs.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then

                        objINItemDs.Tables(0).Rows(intCnt).Item("Description") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("PartNo") & " (" & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                    "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("Cost"), 0) & ", " & _
                                                                    objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding"), 5) & ", " & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("UOMCode") & " ( " & _
                                                                    objGlobal.GetIDDecimalSeparator_FreeDigit(objPU.mtdGetQty(Trim(strLocation), objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode"), False, objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding")), 5) & ", " & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") & " ) "

                        objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                              objINItemDs.Tables(0).Rows(intCnt).Item("PartNo")

                    Else
                        objINItemDs.Tables(0).Rows(intCnt).Item("Description") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                   objINItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                   "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("Cost"), 0) & ", " & _
                                                                   objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding"), 5) & ", " & _
                                                                   objINItemDs.Tables(0).Rows(intCnt).Item("UOMCode") & " ( " & _
                                                                   objGlobal.GetIDDecimalSeparator_FreeDigit(objPU.mtdGetQty(Trim(strLocation), objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode"), False, objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding")), 5) & ", " & _
                                                                   objINItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") & " ) "
                    End If
                Else
                    objINItemDs.Tables(0).Rows(intCnt).Item("Description") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                    "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("Cost"), 0) & ", " & _
                                                                    objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding"), 5) & ", " & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("UOMCode") & " ( " & _
                                                                    objGlobal.GetIDDecimalSeparator_FreeDigit(objPU.mtdGetQty(Trim(strLocation), objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode"), False, objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding")), 5) & ", " & _
                                                                    objINItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") & " ) "
                End If

                If objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = strSelectedItemCode Then
                    intSelectedIndex = intCnt + 1
                    txtQtyOrder.Text = objPU.mtdGetQty(Trim(strLocation), _
                                                       Trim(strSelectedItemCode), _
                                                       False, _
                                                       objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding"))

                    hidOrgQtyOrder.Value = objINItemDs.Tables(0).Rows(intCnt).Item("QtyOutStanding")
                    txtCost.Text = "0"
                    txtAddNote.Value = Trim(objINItemDs.Tables(0).Rows(intCnt).Item("AdditionalNote"))
                End If
            Next intCnt

            dr = objINItemDs.Tables(0).NewRow()
            dr("ItemCode") = ""
            dr("Description") = lblSelectListItem.Text & lblStockItem.Text
            objINItemDs.Tables(0).Rows.InsertAt(dr, 0)

            ddlItemCode.DataSource = objINItemDs.Tables(0)
            ddlItemCode.DataValueField = "ItemCode"
            ddlItemCode.DataTextField = "Description"
            ddlItemCode.DataBind()
        Else
            lblPRLocCode.Text = ""
            BindINItem("")
        End If
    End Sub

    Sub BindLoc(ByVal pv_strPRId As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        strPRRefLocCode = IIf(pv_strPRId = "", "", strSelectedPRRefLocCode)
        strParam = strPRRefLocCode & "|" & objAdmin.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objLocDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode").Trim()
            objLocDs.Tables(0).Rows(intCnt).Item("Description") = objLocDs.Tables(0).Rows(intCnt).Item("LocCode") & " (" & objLocDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"

            If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = strSelectedPRRefLocCode Then
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

    Sub LocIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedSuppCode = ddlSuppCode.SelectedItem.Value
        If chkCentralized.Checked = True Then
            strSelectedPRId = ddlPRId.SelectedItem.Value
        Else
            strSelectedPRId = Trim(txtPRID.Text)
        End If

        strSelectedPRRefLocCode = ddlPRRefLocCode.SelectedItem.Value
        strSelectedItemCode = ddlItemCode.SelectedItem.Value

        'If strSelectedPRId <> "" Then
        '    If chkCentralized.Checked = True Then
        '        BindPRItem("")
        '    Else
        '        BindINItem("")
        '    End If
        'End If
    End Sub

    Sub BindINItem(ByVal pv_strSelectedPOType As String)
        Dim strOpCd As String = "IN_CLSSETUP_STOCKITEM_LIST_GET" '"IN_CLSTRX_ITEMPART_ITEM_GET"
        Dim objINItemDs As New Object()
        Dim strParam As String = ""
        Dim dr As DataRow
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strPOType As String

        If lblPOType.Text = objPU.EnumPOType.Stock Then
            strPOType = objINSetup.EnumInventoryItemType.Stock & "','" & objINSetup.EnumInventoryItemType.WorkshopItem
        Else
            strPOType = lblPOType.Text
        End If

        strParam = strPOType & "|" & _
                   strLocation & "|" & _
                   objINSetup.EnumStockItemStatus.Active & "|itm.ItemCode"

        Try
            intErrNo = objPU.mtdGetItem(strOpCd, strParam, objINItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_INITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        'For intCnt = 0 To objINItemDs.Tables(0).Rows.Count - 1
        '    objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode")
        '    objINItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") = objINItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")
        '    If lblPOType.Text = objPU.EnumPOType.Stock Then
        '        If objINItemDs.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then
        '            objINItemDs.Tables(0).Rows(intCnt).Item("Description") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
        '                                                                     objINItemDs.Tables(0).Rows(intCnt).Item("PartNo") & " (" & _
        '                                                                     objINItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
        '                                                                     "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("LatestCost"), 0) & ", " & _
        '                                                                     objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
        '                                                                     objINItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")
        '            objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
        '                                                                  objINItemDs.Tables(0).Rows(intCnt).Item("PartNo")
        '        Else
        '            objINItemDs.Tables(0).Rows(intCnt).Item("Description") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
        '                                                                     objINItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
        '                                                                     "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("LatestCost"), 0) & ", " & _
        '                                                                     objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
        '                                                                     objINItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")
        '        End If
        '    Else
        '        objINItemDs.Tables(0).Rows(intCnt).Item("Description") = objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
        '                                                                 objINItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
        '                                                                 "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("LatestCost"), 0) & ", " & _
        '                                                                     objGlobal.GetIDDecimalSeparator_FreeDigit(objINItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
        '                                                                 objINItemDs.Tables(0).Rows(intCnt).Item("PurchaseUOM")
        '    End If

        '    If objINItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = strSelectedItemCode Then
        '        intSelectedIndex = intCnt + 1
        '    End If
        'Next intCnt

        txtQtyOrder.Text = "0"
        txtCost.Text = "0"
        txtAddNote.Value = ""
        txtAmtTransportFee.Text = "0"
        txtDiscount.Text = "0"
        txtPBBKB.Text = "0"
        txtPBBKBRate.Text = "0"
        txtPPN22.Text = "0"
        txtTtlCost.Text = "0"
        txtPPH23.Text = "0"
        chkGrossUp.Checked = False
        txtPPH23OA.Text = "0"
        chkGrossUpOA.Checked = False
        txtPPH21.Text = "0"
        chkGrossUp21.Checked = False

        dr = objINItemDs.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = lblSelectListItem.Text & lblStockItem.Text
        objINItemDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlItemCode.DataSource = objINItemDs.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub ItemIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedSuppCode = ddlSuppCode.SelectedItem.Value
        If chkCentralized.Checked = True Then
            strSelectedPRId = ddlPRId.SelectedItem.Value
        Else
            strSelectedPRId = Trim(txtPRID.Text)
        End If
        'strSelectedPRId = IIf(chkCentralized.Checked = True, ddlPRId.SelectedItem.Value, Trim(txtPRID.Text))
        strSelectedPRRefLocCode = ddlPRRefLocCode.SelectedItem.Value
        strSelectedItemCode = Trim(ddlItemCode.SelectedItem.Value)
        strSelectedPOType = lblPOType.Text

        'Dim arrParam As Array
        'arrParam = Split(ddlItemCode.SelectedItem.Text, ".")
        'txtCost.Text = Trim(arrParam(3))

        Dim objItemDs As New Object()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim strOpCd As String = "IN_CLSTRX_PURREQ_ITEMLIST_GET"

        If chkCentralized.Checked = True Then
            strParamName = "LOCCODE|ITEMTYPE|ITEMSTATUS|ITEMCODE|CENTRALIZED|PRID"
            strParamValue = strLocation & _
                            "|" & strSelectedPOType & _
                            "|" & objINSetup.EnumStockItemStatus.Active & _
                            "|" & strSelectedItemCode & _
                            "|" & "1" & _
                            "|" & strSelectedPRId
        Else
            If hidGetRefNo.Value = 0 Then
                strParamName = "LOCCODE|ITEMTYPE|ITEMSTATUS|ITEMCODE|CENTRALIZED|PRID"
                strParamValue = strLocation & _
                                "|" & strSelectedPOType & _
                                "|" & objINSetup.EnumStockItemStatus.Active & _
                                "|" & strSelectedItemCode & _
                                "|" & "0" & _
                                "|" & ""
            Else
                strOpCd = "PU_CLSTRX_PO_GETITEM_SPK"

                strParamName = "POID|LOCCODE|STRSEARCH"
                strParamValue = Trim(txtPRRefId.Text) & "|" & strLocation & "|" & _
                                " AND A.STATUS NOT IN ('" & objPU.EnumPOStatus.Deleted & "','" & objPU.EnumPOStatus.Cancelled & "') "
            End If
            
        End If

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
                txtCost.Text = objItemDs.Tables(0).Rows(0).Item("PRCost")
                txtQtyOrder.Text = objItemDs.Tables(0).Rows(0).Item("QtyApp")
            Else
                If hidGetRefNo.Value = 0 Then
                    If lblPOType.Text = objPU.EnumPOType.Stock Then
                        txtCost.Text = objItemDs.Tables(0).Rows(0).Item("AverageCost")
                    Else
                        txtCost.Text = "0"
                    End If
                    txtQtyOrder.Text = "0"
                Else
                    txtQtyOrder.Text = objItemDs.Tables(0).Rows(0).Item("QtyOrder")
                    lblHidPOlnId.Text = Trim(objItemDs.Tables(0).Rows(0).Item("POLNID"))
                End If
            End If
            BindUOM(objItemDs.Tables(0).Rows(0).Item("PurchaseUOM"))
        Else
            txtCost.Text = "0"
            txtQtyOrder.Text = "0"
        End If

        If strSelectedPRId <> "" Then
            If chkCentralized.Checked = True Then
                BindPRItem(strSelectedPRId)
            End If
        End If
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddPOLn As String = "PU_CLSTRX_PO_LINE_ADD"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_DETAILS_GET"
        Dim strOpCd_AddPO As String = "PU_CLSTRX_PO_ADD"
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"

        Dim strOppCd_GetID As String = "PU_CLSTRX_PO_GETID"
        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "PO"
        Dim strHistYear As String = ""
        Dim blnIsDetail As Boolean = True
        Dim objPUDs As Object

        Dim objPOId As New Object()
        Dim objPOLnId As New Object()
        Dim strPOId As String
        Dim strSuppCode As String = ddlSuppCode.SelectedItem.Value
        Dim strPRID As String
        Dim strPRLocCode As String = lblPRLocCode.Text
        Dim strPRRefId As String = txtPRRefId.Text
        Dim strPRRefLocCode As String = ddlPRRefLocCode.SelectedItem.Value
        Dim strItemCode As String = ddlItemCode.SelectedItem.Value
        Dim strQtyOrder As String = Trim(Request.Form("txtQtyOrder"))
        Dim strCost As String = Trim(Request.Form("txtCost"))
        Dim strRemark As String = Trim(Request.Form("txtRemark"))
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim blnUpdate As Boolean
        Dim intPRIDInd As Integer
        Dim intPRRefIDInd As Integer
        Dim intPRRefLocCode As Integer
        Dim strAddNote As String = Trim(txtAddNote.Value)
        Dim strTransporter As String = Trim(ddlTransporter.SelectedItem.Value)
        Dim strAmtTransportFee As String = IIf(Trim(Request.Form("txtAmtTransportFee")) = "", "0", Trim(Request.Form("txtAmtTransportFee")))
        Dim strDiscount As String = Trim(Request.Form("txtDiscount"))
        Dim strPBBKB As String = IIf(Trim(txtPBBKB.Text) = "", "0", Trim(txtPBBKB.Text))
        Dim strPPN22 As String = IIf(Trim(txtPPN22.Text) = "", "0", Trim(txtPPN22.Text))
        Dim strAddDiscount As String = Trim(txtAddDisc.Text)
        Dim strNewIDFormat As String
        Dim strPOIssued As String
        Dim SelectedPOIssued As String = ddlPOIssued.SelectedItem.Value
        Dim strDeptCode As String = ddlDeptCode.SelectedItem.Value
        Dim strPRLocation As String
        Dim strPBBKBRate As String = IIf(Trim(txtPBBKBRate.Text) = "", "0", Trim(txtPBBKBRate.Text))
        Dim strPPH23 As String = IIf(Trim(txtPPH23.Text) = "", "0", Trim(txtPPH23.Text))
        Dim strPPH23OA As String = IIf(Trim(txtPPH23OA.Text) = "", "0", Trim(txtPPH23OA.Text))
        Dim strPPH21 As String = IIf(Trim(txtPPH21.Text) = "", "0", Trim(txtPPH21.Text))
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDateCreated.Text.Trim(), indDate) = False Then
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

        If lblPOType.Text <> objPU.EnumPOType.FixedAsset Then
            If hidOrgQtyOrder.Value <> "" Then
                If CInt(strQtyOrder) > CInt(hidOrgQtyOrder.Value) Then
                    lblValidationQtyOrder.Visible = True
                    Exit Sub
                End If
            End If
        End If

        If txtCreditTerm.Text = "" Then
            lblErrCreditTerm.Visible = True
            Exit Sub
        End If

        'If strSelectedPOType = Trim(objPU.EnumPOType.Stock) Or strSelectedPOType = Trim(objPU.EnumPOType.FixedAsset) Then
        If strDeptCode = "" Then
            lblDeptCode.Visible = True
            Exit Sub
        End If

        If SelectedPOIssued = "" Then
            lblPOIssued.Visible = True
            Exit Sub
        End If
        'End If

        If strQtyOrder <= 0 And lblPOType.Text = objPU.EnumPOType.Stock Then
            lblErrQtyOrder.Text = "Quantity order cannot less than 1"
            lblErrQtyOrder.Visible = True
            Exit Sub
        End If

        If chkCentralized.Checked = True Then
            strPRID = Trim(ddlPRId.SelectedItem.Value)
            If strPRID = "" Then
                lblErrDdlPRID.Visible = True
                Exit Sub
            End If
        Else
            strPRID = Trim(txtPRID.Text)
            If strPRID = "" Or InStr(Mid(Trim(strPRID), 9, 3), "/") > 0 Then
                lblErrTxtPRID.Visible = True
                Exit Sub
            End If
        End If

        strCurrency = ddlCurrency.SelectedItem.Value
        strExRate = Trim(txtExRate.Text)

        If strCurrency = "IDR" Then
            If CDbl(strExRate) > 1 Or CDbl(strExRate) < 1 Then
                lblErrExRate.Text = "Rate in IDR cannot more than Rp. 1,00"
                lblErrExRate.Visible = True
                Exit Sub
            End If
        Else
            If CDbl(strExRate) = 1 Or CDbl(strExRate) < 1 Then
                lblErrExRate.Text = "Rate in " & Trim(strCurrency) & " cannot equal or less than Rp. 1,00"
                lblErrExRate.Visible = True
                Exit Sub
            End If
        End If

        If strSuppCode = "" Then
            lblSuppCode.Visible = True
            If strSelectedPOType = objPU.EnumPOType.Stock Or strSelectedPOType = objPU.EnumPOType.FixedAsset Then
                lblDeptCode.Visible = True
                lblPOIssued.Visible = True
            End If
            Exit Sub
        End If

        If strTransporter = "" Then
            If CDbl(strAmtTransportFee) > 0 Then
                lblErrTransporter.Visible = True
                Exit Sub
            End If
        End If

        If CDbl(strPBBKB) > 0 Then
            If CDbl(strPBBKBRate) = 0 Then
                lblErrPBBKB.Visible = True
                lblErrPBBKB.Text = "Rate cannot be empty."
            End If
        End If

        intPRIDInd = IIf(strPRID = "", 0, 1)
        intPRRefIDInd = IIf(strPRRefId = "", 0, 1)
        intPRRefLocCode = IIf(strPRRefLocCode = "", 0, 1)

        If (intPRIDInd + intPRRefIDInd) = 0 Then
        End If

        'If (intPRIDInd + intPRRefIDInd) > 1 Then
        '    lblErrManySelectDoc.ForeColor = Red
        '    blnSelectManyDoc = False
        '    Exit Sub
        'End If

        ''temporary remark for sta group
        ''If (intPRRefIDInd + intPRRefLocCode) = 1 Then
        ''    lblErrRef.Visible = True
        ''    Exit Sub
        ''End If

        If ddlItemCode.SelectedIndex = 0 Then
            lblErrItem.Visible = True
            lblErrItemExist.Visible = True
            Exit Sub
        Else
            'If InStr(strItemCode, ITEM_PART_SEPERATOR) <> 0 Then
            '    strItemCode = Trim(Mid(strItemCode, 1, InStr(strItemCode, ITEM_PART_SEPERATOR) - 1))
            'End If
            strItemCode = Trim(ddlItemCode.SelectedItem.Value)
        End If

        If ddlUOM.SelectedIndex = 0 Then
            'lblErrUOM.Visible = True
            ItemIndexChanged(Sender, E)
        End If

        If Len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
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

        strPOId = IIf(lblPOId.Text = "", "", lblPOId.Text)
        blnUpdate = IIf(strPOId = "", False, True)

        'If Month(strDate) < strAccMonth And Year(strDate) <= strAccYear Then
        '    lblDate.Visible = True
        '    lblDate.Text = "Invalid transaction date."
        '    Exit Sub
        'End If

        If strPOId = "" Then
            strPRLocation = Trim(ddlPRRefLocCode.SelectedItem.Value)
            If strPRLocation = "" Then
                lblErrPRRef.Visible = True
                Exit Sub
            End If
        Else
            strPRLocation = Mid(Trim(strPOId), 9, 3)
        End If


        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If Session("SS_LOCLEVEL") <> objAdmin.EnumLocLevel.HQ Then
            strPOIssued = "L"
        Else
            strPOIssued = "M"
        End If

        If lblPOType.Text = objPU.EnumPOType.DirectCharge Then
            strNewIDFormat = "SPK" & "/" & strCompany & "/" & strPRLocation & "/" & strPOIssued & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        Else
            strNewIDFormat = "SPB" & "/" & strCompany & "/" & strPRLocation & "/" & strPOIssued & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        End If

        If lblPOType.Text = objPU.EnumPOType.DirectCharge And CDbl(txtPPH23.Text) <> 0 Then
            If chkGrossUp.Checked = True Then
            Else
                If lblHidStatusEdited.Text = "0" Then
                    strAddNote = strAddNote + " (POTONG PPH 23 " + Trim(strPPH23) + "%)"
                End If
            End If
        End If
        If CDbl(txtPPH23OA.Text) <> 0 And Trim(strSuppCode) = Trim(strTransporter) Then
            If chkGrossUpOA.Checked = True Then
            Else
                If lblHidStatusEdited.Text = "0" Then
                    strAddNote = strAddNote + " (POTONG PPH 23 " + Trim(strPPH23OA) + "%)"
                End If
            End If
        End If
        If lblPOType.Text = objPU.EnumPOType.DirectCharge And CDbl(txtPPH21.Text) <> 0 Then
            If chkGrossUp21.Checked = True Then
            Else
                If lblHidStatusEdited.Text = "0" Then
                    strAddNote = strAddNote + " (POTONG PPH 21 " + Trim(strPPH21) + "%)"
                End If
            End If
        End If

        If lblHidStatusEdited.Text = "0" Then
            If lblHidPOlnId.Text = "" Then
                strParam = strPOId & "|" & _
                                   lblPOType.Text & "|" & _
                                   strPRID & "|" & _
                                   strPRRefId & "|" & _
                                   strPRRefLocCode & "|" & _
                                   strItemCode & "||" & _
                                   strQtyOrder & "|" & _
                                   strCost & "|" & _
                                   strSuppCode & "|" & _
                                   strRemark & "|" & _
                                   strAccMonth & "|" & _
                                   strAccYear & "|" & _
                                   strPRLocCode & "|" & _
                                   IIf(chkPPN.Checked, Session("SS_PPNRATE"), 0) & "|" & _
                                   IIf(chkPPN.Checked, objPU.EnumPPN.Yes, IIf(chkSurat.Checked = True, "3", objPU.EnumPPN.No)) & "|" & _
                                   strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
                                   Trim(strDeptCode) & "|" & Trim(SelectedPOIssued) & "|" & _
                                   IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                                   strCurrency & "|" & strExRate & "|" & Replace(strAddNote, "'", "''") & "|" & _
                                   strTransporter & "|" & strAmtTransportFee & "|" & strDiscount & "|" & strPBBKB & "|" & _
                                   strPPN22 & "|" & strAddDiscount & "|" & strDate & "|" & txtCreditTerm.Text & "|" & Trim(ddlUOM.SelectedItem.Value) & "|" & _
                                   IIf(chkPPNTransport.Checked, objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                                   strPBBKBRate & "|" & _
                                   strPPH23 & "|" & IIf(chkGrossUp.Checked = True, "1", "2") & "|" & _
                                   strPPH23OA & "|" & IIf(chkGrossUpOA.Checked = True, "1", "2") & "|" & _
                                   strPPH21 & "|" & IIf(chkGrossUp21.Checked = True, "1", "2")

                Try
                    intErrNo = objPU.mtdAddPOLn(strOpCd_AddPOLn, _
                                                strOpCd_UpdPOLn, _
                                                strOpCd_AddPO, _
                                                strOpCd_GetPOLn, _
                                                strOpCd_UpdPO, _
                                                strOppCd, _
                                                strOppCd_Back, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnUpdate, _
                                                objPOId, _
                                                objPOLnId, _
                                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.NewPurchaseOrder), _
                                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.NewPurchaseOrderLn))
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_NEW_POLN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
                End Try

                If intErrNo = 3 Then
                    lblErrItemExist.Visible = True
                    Exit Sub
                End If

                strSelectedPOId = objPOId

            Else
                Dim strOpCode_ItemUpd As String = "PU_CLSTRX_PO_LINE_EDIT"
                Dim strParamName As String = ""
                Dim strParamValue As String = ""
                Dim decAmountCurrency As Decimal
                Dim decCostDisc As Decimal
                Dim decAmtDisc As Decimal
                Dim decPBBKBAmount As Decimal
                Dim decPPN22Amount As Decimal
                Dim decPPNAmount As Decimal
                Dim decAmount As Decimal
                Dim decLineTotalAmount As Decimal
                Dim decTotalAmount As Decimal
                Dim decNetAmtTransportFee As Decimal
                Dim decPPNAmtTransportFee As Decimal

                Dim decExRate As Decimal = CDec(strExRate)
                Dim decCost As Decimal = CDec(strCost)
                Dim decQtyOrder As Decimal = CDec(strQtyOrder)
                Dim decDiscount As Decimal = CDec(strDiscount)
                Dim decPBBKB As Decimal = CDec(strPBBKB)
                Dim decPPN22 As Decimal = CDec(strPPN22)
                Dim decAmtTransportFee As Decimal = CDec(strAmtTransportFee)
                Dim decAddDiscount As Decimal = CDec(strAddDiscount)
                Dim decPBBKBRate As Decimal = CDec(strPBBKBRate)

                decAmountCurrency = decCost
                decCost = decCost
                decCostDisc = Round(decCost * (decDiscount / 100), 2)
                decCost = decCost - decCostDisc
                decAmount = decQtyOrder * decCost 'Do not round bcos need to store the orig value
                'decAmtDisc = Round(decAmount * (decDiscount / 100), 2)
                decAmount = decAmount '- decAmtDisc

                decNetAmtTransportFee = decAmtTransportFee
                decPPNAmtTransportFee = IIf(chkPPNTransport.Checked, Round(decNetAmtTransportFee * 0.1, 0), 0)
                decAmtTransportFee = decAmtTransportFee + decPPNAmtTransportFee

                decPPNAmount = Round((decAmount * decExRate) * (IIf(chkPPN.Checked, Session("SS_PPNRATE"), 0) / 100), 0) + decPPNAmtTransportFee
                decPBBKBAmount = Round(decQtyOrder * Round(decCost * (decPBBKBRate / 100) * (decPBBKB / 100), 2), 0)
                decPPN22Amount = Round((decPPN22 / 100) * decAmount, 0)

                decLineTotalAmount = Round((decAmount * decExRate) + (decPPNAmount) + (decPBBKBAmount * decExRate) + decNetAmtTransportFee + (decPPN22Amount * decExRate), 2)
                decLineTotalAmount = decLineTotalAmount - (decAddDiscount * decExRate)
                decLineTotalAmount = IIf(decExRate = 1, Round(decLineTotalAmount, 2), Round(decLineTotalAmount, 2))

                strParamName = "POID|POLNID|PRID|PRLOCCODE|PRREFID|PRREFLOCCODE|" & _
                                "ITEMCODE|QTYORDER|COST|AMOUNT|STATUS|" & _
                                "PPNAMOUNT|NETAMOUNT|PPN|ADDITIONALNOTE|AMOUNTCURRENCY|" & _
                                "TRANSPORTER|AMTTRANSPORTFEE|DISCOUNT|PBBKB|PPN22|USERID|" & _
                                "REMARK|POISSUED|DEPTCODE|CURRENCYCODE|EXCHANGERATE|ADDDISCOUNT|CREDITTERM|PURCHASEUOM|" & _
                                "PPNTRANSPORT|NETAMTTRANSPORTFEE|PPNAMTTRANSPORTFEE|PBBKBRATE|" & _
                                "PPH23|GROSSUP|PPH23TRANSPORT|GROSSUPTRANSPORT|PPH21|GROSSUP21|" & _
                                "ACCMONTH|ACCYEAR|POTYPE|LOCCODE|CENTRALIZED|SUPPLIERCODE"

                strParamValue = strPOId & "|" & _
                        lblHidPOlnId.Text & "|" & _
                        strPRID & "|" & _
                        strPRLocCode & "|" & _
                        strPRRefId & "|" & _
                        strPRRefLocCode & "|" & _
                        strItemCode & "|" & _
                        strQtyOrder & "|" & _
                        decCost * decExRate & "|" & _
                        decLineTotalAmount & "|" & _
                        objPU.EnumPOLnStatus.Edited & "|" & _
                        decPPNAmount & "|" & _
                        (decAmount * decExRate) + decNetAmtTransportFee & "|" & _
                        IIf(chkPPN.Checked, objPU.EnumPPN.Yes, IIf(chkSurat.Checked = True, "3", objPU.EnumPPN.No)) & "|" & _
                        strAddNote & "|" & _
                        decAmountCurrency & "|" & _
                        strTransporter & "|" & _
                        decAmtTransportFee & "|" & _
                        decDiscount & "|" & _
                        decPBBKB & "|" & _
                        decPPN22 & "|" & _
                        strUserId & "|" & _
                        strRemark & "|" & _
                        SelectedPOIssued & "|" & _
                        strDeptCode & "|" & _
                        strCurrency & "|" & _
                        decExRate & "|" & _
                        decAddDiscount & "|" & _
                        txtCreditTerm.Text & "|" & _
                        Trim(ddlUOM.SelectedItem.Value) & "|" & _
                        IIf(chkPPNTransport.Checked, objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                        decNetAmtTransportFee & "|" & _
                        decPPNAmtTransportFee & "|" & _
                        decPBBKBRate & "|" & _
                        strPPH23 & "|" & IIf(chkGrossUp.Checked = True, "1", "2") & "|" & _
                        strPPH23OA & "|" & IIf(chkGrossUpOA.Checked = True, "1", "2") & "|" & _
                        strPPH21 & "|" & IIf(chkGrossUp21.Checked = True, "1", "2") & "|" & _
                        strAccMonth & "|" & _
                        strAccYear & "|" & _
                        lblPOType.Text & "|" & _
                        strLocation & "|" & _
                        IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                        Trim(ddlSuppCode.SelectedItem.Value)

                Try
                    intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                            strParamName, _
                                                            strParamValue)

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
                End Try

                strSelectedPOId = strPOId
            End If
           
        Else
            Dim strOpCode_ItemUpd As String = "PU_CLSTRX_PO_LINE_EDIT"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""
            Dim decAmountCurrency As Decimal
            Dim decCostDisc As Decimal
            Dim decAmtDisc As Decimal
            Dim decPBBKBAmount As Decimal
            Dim decPPN22Amount As Decimal
            Dim decPPNAmount As Decimal
            Dim decAmount As Decimal
            Dim decLineTotalAmount As Decimal
            Dim decTotalAmount As Decimal
            Dim decNetAmtTransportFee As Decimal
            Dim decPPNAmtTransportFee As Decimal

            Dim decExRate As Decimal = CDec(strExRate)
            Dim decCost As Decimal = CDec(strCost)
            Dim decQtyOrder As Decimal = CDec(strQtyOrder)
            Dim decDiscount As Decimal = CDec(strDiscount)
            Dim decPBBKB As Decimal = CDec(strPBBKB)
            Dim decPPN22 As Decimal = CDec(strPPN22)
            Dim decAmtTransportFee As Decimal = CDec(strAmtTransportFee)
            Dim decAddDiscount As Decimal = CDec(strAddDiscount)
            Dim decPBBKBRate As Decimal = CDec(strPBBKBRate)

            decAmountCurrency = decCost
            decCost = decCost
            decCostDisc = Round(decCost * (decDiscount / 100), 2)
            decCost = decCost - decCostDisc
            decAmount = decQtyOrder * decCost 'Do not round bcos need to store the orig value
            'decAmtDisc = Round(decAmount * (decDiscount / 100), 2)
            decAmount = decAmount '- decAmtDisc

            decNetAmtTransportFee = decAmtTransportFee
            decPPNAmtTransportFee = IIf(chkPPNTransport.Checked, Round(decNetAmtTransportFee * 0.1, 0), 0)
            decAmtTransportFee = decAmtTransportFee + decPPNAmtTransportFee

            decPPNAmount = Round((decAmount * decExRate) * (IIf(chkPPN.Checked, Session("SS_PPNRATE"), 0) / 100), 0) + decPPNAmtTransportFee
            decPBBKBAmount = Round(decQtyOrder * Round(decCost * (decPBBKBRate / 100) * (decPBBKB / 100), 2), 0)
            decPPN22Amount = Round((decPPN22 / 100) * decAmount, 0)

            decLineTotalAmount = Round((decAmount * decExRate) + (decPPNAmount) + (decPBBKBAmount * decExRate) + decNetAmtTransportFee + (decPPN22Amount * decExRate), 2)
            decLineTotalAmount = decLineTotalAmount - (decAddDiscount * decExRate)
            decLineTotalAmount = IIf(decExRate = 1, Round(decLineTotalAmount, 2), Round(decLineTotalAmount, 2))

            strParamName = "POID|POLNID|PRID|PRLOCCODE|PRREFID|PRREFLOCCODE|" & _
                            "ITEMCODE|QTYORDER|COST|AMOUNT|STATUS|" & _
                            "PPNAMOUNT|NETAMOUNT|PPN|ADDITIONALNOTE|AMOUNTCURRENCY|" & _
                            "TRANSPORTER|AMTTRANSPORTFEE|DISCOUNT|PBBKB|PPN22|USERID|" & _
                            "REMARK|POISSUED|DEPTCODE|CURRENCYCODE|EXCHANGERATE|ADDDISCOUNT|CREDITTERM|PURCHASEUOM|" & _
                            "PPNTRANSPORT|NETAMTTRANSPORTFEE|PPNAMTTRANSPORTFEE|PBBKBRATE|" & _
                            "PPH23|GROSSUP|PPH23TRANSPORT|GROSSUPTRANSPORT|PPH21|GROSSUP21|" & _
                            "ACCMONTH|ACCYEAR|POTYPE|LOCCODE|CENTRALIZED|SUPPLIERCODE"

            strParamValue = strPOId & "|" & _
                    lblHidPOlnId.Text & "|" & _
                    strPRID & "|" & _
                    strPRLocCode & "|" & _
                    strPRRefId & "|" & _
                    strPRRefLocCode & "|" & _
                    strItemCode & "|" & _
                    strQtyOrder & "|" & _
                    decCost * decExRate & "|" & _
                    decLineTotalAmount & "|" & _
                    objPU.EnumPOLnStatus.Edited & "|" & _
                    decPPNAmount & "|" & _
                    (decAmount * decExRate) + decNetAmtTransportFee & "|" & _
                    IIf(chkPPN.Checked, objPU.EnumPPN.Yes, IIf(chkSurat.Checked = True, "3", objPU.EnumPPN.No)) & "|" & _
                    strAddNote & "|" & _
                    decAmountCurrency & "|" & _
                    strTransporter & "|" & _
                    decAmtTransportFee & "|" & _
                    decDiscount & "|" & _
                    decPBBKB & "|" & _
                    decPPN22 & "|" & _
                    strUserId & "|" & _
                    strRemark & "|" & _
                    SelectedPOIssued & "|" & _
                    strDeptCode & "|" & _
                    strCurrency & "|" & _
                    decExRate & "|" & _
                    decAddDiscount & "|" & _
                    txtCreditTerm.Text & "|" & _
                    Trim(ddlUOM.SelectedItem.Value) & "|" & _
                    IIf(chkPPNTransport.Checked, objPU.EnumPPN.Yes, objPU.EnumPPN.No) & "|" & _
                    decNetAmtTransportFee & "|" & _
                    decPPNAmtTransportFee & "|" & _
                    decPBBKBRate & "|" & _
                    strPPH23 & "|" & IIf(chkGrossUp.Checked = True, "1", "2") & "|" & _
                    strPPH23OA & "|" & IIf(chkGrossUpOA.Checked = True, "1", "2") & "|" & _
                    strPPH21 & "|" & IIf(chkGrossUp21.Checked = True, "1", "2") & "|" & _
                    strAccMonth & "|" & _
                    strAccYear & "|" & _
                    lblPOType.Text & "|" & _
                    strLocation & "|" & _
                    IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                    Trim(ddlSuppCode.SelectedItem.Value)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
            End Try

            strSelectedPOId = strPOId
        End If


        lblPOId.Text = strSelectedPOId
        strSelectedSuppCode = strSuppCode
        strSelectedPRId = strPRID
        strSelectedPRRefLocCode = strPRRefLocCode
        onLoad_DisplayPO(strSelectedPOId)
        onLoad_DisplayPOLn(strSelectedPOId)
        BindPR("")
        lblHidPOlnId.Text = ""
        If chkCentralized.Checked = True Then
            ddlPRId.Enabled = True
        Else
            txtPRID.Enabled = True
            If lblPOType.Text = objPU.EnumPOType.DirectCharge Then
                If txtPRRefId.Text <> "" Then
                    GetRefNoBtn_Click(Sender, E)
                End If
            End If
        End If

        If lblPOId.Text <> "" Then
            btnSave_Click(Sender, E)
            onLoad_DisplayPO(strSelectedPOId)
            onLoad_DisplayPOLn(strSelectedPOId)
        End If

    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_DelPOLn As String = "PU_CLSTRX_PO_LINE_DEL"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"
        Dim strParam As String = ""
        'Dim POLnIdCell As TableCell = E.Item.Cells(0)
        Dim lbl As label
        Dim strPOLnId As String
        Dim intErrNo As Integer
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDateCreated.Text.Trim(), indDate) = False Then
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

        dgPODet.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblPOLnId")
        strPOLnId = lbl.Text.Trim
        lblHidPOlnId.Text = strPOLnId

        strExRate = Trim(txtExRate.Text)
        'strPOLnId = POLnIdCell.Text
        strParam = lblPOId.Text & "|" & strPOLnId & "|" & strExRate

        Try
            intErrNo = objPU.mtdDelPOLn(strOpCd_DelPOLn, _
                                        strOpCd_GetPOLn, _
                                        strOpCd_UpdPO, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_DEL_POLN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try
        strSelectedPOId = lblPOId.Text
        strSelectedPRId = Request.Form("ddlPRId")
        onLoad_DisplayPO(strSelectedPOId)
        onLoad_DisplayPOLn(strSelectedPOId)
        BindSupp("")
        BindPR("")
        BindLoc("")
        lblHidPOlnId.Text = ""
        If chkCentralized.Checked = True Then
            ddlPRId.Enabled = True
        Else
            txtPRID.Enabled = True
        End If
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdCost As String = "PU_CLSTRX_COST_UPD"
        Dim strParam As String = ""
        Dim POLnIdCell As TableCell = E.Item.Cells(0)
        Dim ItemCodeCell As TableCell = E.Item.Cells(1)
        Dim QtyOrderCell As TableCell = E.Item.Cells(2)
        Dim QtyReceiveCell As TableCell = E.Item.Cells(3)
        Dim PRLocCodeCell As TableCell = E.Item.Cells(4)
        Dim PRRefLocCodeCell As TableCell = E.Item.Cells(5)
        Dim strPOLnId As String
        Dim strItemCode As String
        Dim strQtyOrder As String
        Dim strQtyReceive As String
        Dim strPRLocCode As String
        Dim strPRRefLocCode As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        strPOLnId = POLnIdCell.Text
        strItemCode = ItemCodeCell.Text
        strQtyOrder = QtyOrderCell.Text
        strQtyReceive = QtyReceiveCell.Text
        strPRLocCode = PRLocCodeCell.Text
        strPRRefLocCode = PRRefLocCodeCell.Text

        If strPRLocCode = "&nbsp;" Then
            strPRLocCode = ""
        End If
        If strPRRefLocCode = "&nbsp;" Then
            strPRRefLocCode = ""
        End If

        If UCase(Right(Trim(lblPOId.Text), 3)) = "ADD" Then
            strParamName = "STRUPDATE"
            strParamValue = "SET Status = '" & objPU.EnumPOLnStatus.Cancelled & "', QtyCancel = QtyOrder - QtyReceive " & _
                            "WHERE POID = '" & Trim(lblPOId.Text) & "' AND POLNID = '" & Trim(strPOLnId) & "'"

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd_UpdPOLn, strParamName, strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CANCEL_POLN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
            End Try
        Else
            If lblHidPOlnId.Text = "" Then
                strParam = lblPOId.Text & "|" & _
                                               strPOLnId & "|" & _
                                               objPU.EnumPOLnStatus.Cancelled & "|" & _
                                               strItemCode & "|" & _
                                               strQtyOrder & "|" & _
                                               strPRLocCode & "|" & _
                                               strPRRefLocCode & "|" & _
                                               strQtyReceive

                Try
                    intErrNo = objPU.mtdUpdPOLn(strOpCd_GetPOLn, _
                                                strOpCd_UpdPOLn, _
                                                strOpCd_UpdPO, _
                                                strOpCd_UpdItem, _
                                                strOpCd_UpdCost, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CANCEL_POLN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
                End Try
            Else
                BindINItem("")
            End If
        End If

        txtQtyOrder.Text = "0"
        txtCost.Text = "0"
        txtAddNote.Value = ""
        txtAmtTransportFee.Text = "0"
        txtDiscount.Text = "0"
        txtPBBKB.Text = "0"
        txtPBBKBRate.Text = "0"
        txtPPN22.Text = "0"
        txtTtlCost.Text = "0"
        txtPPH23.Text = "0"
        chkGrossUp.Checked = False
        txtPPH23OA.Text = "0"
        chkGrossUpOA.Checked = False
        txtPPH21.Text = "0"
        chkGrossUp21.Checked = False
        lblHidPOlnId.Text = ""
        If chkCentralized.Checked = True Then
            ddlPRId.Enabled = True
        Else
            txtPRID.Enabled = True
        End If

        onLoad_DisplayPO(lblPOId.Text)
        onLoad_DisplayPOLn(lblPOId.Text)
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddPO As String = "PU_CLSTRX_PO_ADD"
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"
        Dim strOppCd_GetID As String = "PU_CLSTRX_PO_GETID"
        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "PO"
        Dim strHistYear As String = ""
        Dim blnIsDetail As Boolean = True
        Dim objPUDs As Object
        Dim objPOId As New Object()
        Dim strSuppCode As String = ddlSuppCode.SelectedItem.Value
        Dim strRemark As String = Trim(Request.Form("txtRemark"))
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strNewIDFormat As String
        Dim strPOIssued As String
        Dim SelectedPOIssued As String = ddlPOIssued.SelectedItem.Value
        Dim strDeptCode As String = ddlDeptCode.SelectedItem.Value
        'Dim strPRID As String = IIf(chkCentralized.Checked = True, Trim(ddlPRId.SelectedItem.Value), Trim(txtPRID.Text))
        Dim strAddDiscount As String = Trim(txtAddDisc.Text)
        Dim strPRLevel As String
        Dim strPRLevelCode As String
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)
        Dim indDate As String = ""
        
        If CheckDate(txtDateCreated.Text.Trim(), indDate) = False Then
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

        If strSelectedPOId = "" Then
            If lblPOId.Text = "" Then
                Exit Sub
            End If
        End If

        If txtCreditTerm.Text = "" Then
            lblErrCreditTerm.Visible = True
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

        If strSelectedPOType = Trim(objPU.EnumPOType.Stock) Or strSelectedPOType = Trim(objPU.EnumPOType.FixedAsset) Then
            If strDeptCode = "" Then
                lblDeptCode.Visible = True
                Exit Sub
            End If

            If SelectedPOIssued = "" Then
                lblPOIssued.Visible = True
                Exit Sub
            End If
        End If

        strCurrency = ddlCurrency.SelectedItem.Value
        strExRate = Trim(txtExRate.Text)

        If strCurrency = "IDR" Then
            If CDbl(strExRate) > 1 Or CDbl(strExRate) < 1 Then
                lblErrExRate.Text = "Rate in IDR cannot more than Rp. 1,00"
                lblErrExRate.Visible = True
                Exit Sub
            End If
        Else
            If CDbl(strExRate) = 1 Or CDbl(strExRate) < 1 Then
                lblErrExRate.Text = "Rate in " & Trim(strCurrency) & " cannot equal or less than Rp. 1,00"
                lblErrExRate.Visible = True
                Exit Sub
            End If
        End If

        strCurrency = ddlCurrency.SelectedItem.Value
        strExRate = Trim(txtExRate.Text)

        If strSuppCode = "" Then
            lblSuppCode.Visible = True
            If strSelectedPOType = Trim(objPU.EnumPOType.Stock) Or strSelectedPOType = Trim(objPU.EnumPOType.FixedAsset) Then
                lblDeptCode.Visible = True
                lblPOIssued.Visible = True
            End If
            Exit Sub
        End If

        If Len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
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
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If Session("SS_LOCLEVEL") <> objAdmin.EnumLocLevel.HQ Then
            strPOIssued = "L"
        Else
            strPOIssued = "M"
        End If

        If lblPOType.Text = objPU.EnumPOType.DirectCharge Then
            strNewIDFormat = "SPK" & "/" & strCompany & "/" & strLocation & "/" & strPOIssued & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        Else
            strNewIDFormat = "SPB" & "/" & strCompany & "/" & strLocation & "/" & strPOIssued & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        End If

        ''--update item ppn berdasarkan ppn dari setup supplier
        'Dim strOpCode_ItemUpd As String = "PU_CLSTRX_PO_LINE_UPD"
        'Dim strParamName As String
        'Dim strParamValue As String

        'strParamName = "STRUPDATE"
        'strParamValue = "SET PPN = '" & IIf(Trim(splPPN) = "0", "2", "1") & "' " & _
        '                "WHERE POID = '" & Trim(lblPOId.Text) & "'"

        'Try
        '    intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
        '                                            strParamName, _
        '                                            strParamValue)

        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        'End Try

        If lblPOType.Text = objPU.EnumPOType.Stock Or lblPOType.Text = objPU.EnumPOType.FixedAsset Then

            strParam = lblPOId.Text & "|" & _
                        lblPOType.Text & "|" & _
                        strSuppCode & "|" & _
                        strRemark & "|" & _
                        objPU.EnumPOStatus.Active & "||" & _
                        strAccMonth & "|" & _
                        strAccYear & "|" & _
                        strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
                        Trim(strDeptCode) & "|" & Trim(SelectedPOIssued) & "|" & _
                        IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                        strCurrency & "|" & strExRate & "|" & strAddDiscount & "|" & strDate & "|" & txtCreditTerm.Text & "|" & lblHidStatusEdited.Text & "|" & _
                        IIf(chkPrinted.Checked = True, "1", "0")

            Try
                intErrNo = objPU.mtdUpdPO(strOpCd_AddPO, _
                                        strOpCd_UpdPO, _
                                        strOppCd, _
                                        strOppCd_Back, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseOrder), _
                                        objPOId)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_SAVE_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
            End Try
        Else
            strParam = lblPOId.Text & "|" & _
                        lblPOType.Text & "|" & _
                        strSuppCode & "|" & _
                        strRemark & "|" & _
                        objPU.EnumPOStatus.Active & "||" & _
                        strAccMonth & "|" & _
                        strAccYear & "||||||" & Trim(strDeptCode) & "|" & Trim(SelectedPOIssued) & "|" & IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                        strCurrency & "|" & strExRate & "|" & strAddDiscount & "|" & strDate & "|" & txtCreditTerm.Text & "|" & lblHidStatusEdited.Text & "|" & _
                        IIf(chkPrinted.Checked = True, "1", "0")

            Try
                intErrNo = objPU.mtdUpdPO(strOpCd_AddPO, _
                                          strOpCd_UpdPO, _
                                          strOppCd, _
                                          strOppCd_Back, _
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.NewPurchaseOrder), _
                                          objPOId)


            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_SAVE_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
            End Try
        End If
        lblPOId.Text = objPOId
        lblStatus.Text = objPU.mtdGetPOStatus(objPU.EnumPOStatus.Active)
        txtRemark.Text = strRemark
        onLoad_DisplayPO(lblPOId.Text)
        onLoad_DisplayPOLn(lblPOId.Text)
    End Sub

    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPoLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdCost As String = "PU_CLSTRX_COST_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        btnSave_Click(Sender, E)

        strParam = lblPOId.Text & "||" & objPU.EnumPOLnStatus.Active & "||"

        Try
            intErrNo = objPU.mtdUpdPOLn(strOpCd_GetPOLn, _
                                        strOpCd_UpdPoLn, _
                                        strOpCd_UpdPO, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdCost, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        False)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CONFIRM_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        Dim strOpCode As String = "PU_CLSTRX_PO_GENERATE_SPK"
        Dim dsMaster As Object

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "POID|LOCCODE"
        strParamValue = Trim(lblPOId.Text) & "|" & Trim(strLocation)
        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)
          
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_SPK&errmesg=" & Exp.ToString() & "&redirect=PU/trx/PU_PODET.aspx")
        End Try

        onLoad_DisplayPO(lblPOId.Text)
        onLoad_DisplayPOLn(lblPOId.Text)
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddPO As String = "PU_CLSTRX_PO_ADD"
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"
        Dim strOppCd_GetID As String = "PU_CLSTRX_PO_GETID"
        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "PO"
        Dim strHistYear As String = ""
        Dim blnIsDetail As Boolean = True
        Dim objPUDs as Object
        Dim objPOId As New Object()
        Dim strSuppCode As String = ddlSuppCode.SelectedItem.Value
        Dim strRemark As String = Trim(Request.Form("txtRemark"))
        Dim strParam As String = ""
        Dim intErrNo As Integer

        Dim strNewIDFormat As String
        Dim strPOIssued As String
        Dim SelectedPOIssued As String = ddlPOIssued.SelectedItem.Value
        Dim strDeptCode As String = ddlDeptCode.SelectedItem.Value
        Dim strAddDiscount As String = Trim(txtAddDisc.Text)
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDateCreated.Text.Trim(), indDate) = False Then
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

        strCurrency = ddlCurrency.SelectedItem.Value
        strExRate = Trim(txtExRate.Text)

        If lblPOType.Text = objPU.EnumPOType.Stock Or lblPOType.Text = objPU.EnumPOType.FixedAsset Then
            Select Case CInt(SelectedPOIssued)
                Case objPU.EnumPOIssued.JKT
                    strPOIssued = "JKT"
                Case objPU.EnumPOIssued.PKU
                    strPOIssued = "PKU"
                Case objPU.EnumPOIssued.LMP
                    strPOIssued = "LMP"
                Case objPU.EnumPOIssued.PLM
                    strPOIssued = "PLM"
                Case objPU.EnumPOIssued.BKL
                    strPOIssued = "BKL"
                Case objPU.EnumPOIssued.LOK
                    strPOIssued = "LOK"
            End Select
        End If


        If len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
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
            strHistYear = right(strLastPhyYear, 2)
            strNewYear = "1"
        End If

        If lblPOType.Text = objPU.EnumPOType.Stock Or lblPOType.Text = objPU.EnumPOType.FixedAsset Then

            strNewIDFormat = left(Trim(strDeptCode), 3) & "/" & strLocation & "/" & strPOIssued & "/" & right(strPhyYear, 2) & "/" & strPhyMonth & "/"

            strParam = lblPOId.Text & "|" & _
                        lblPOType.Text & "|" & _
                        strSuppCode & "|" & _
                        strRemark & "|" & _
                        objPU.EnumPOStatus.Deleted & "||" & _
                        strAccMonth & "|" & _
                        strAccYear & "|" & _
                        strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
                        Trim(strDeptCode) & "|" & Trim(SelectedPOIssued) & "|" & _
                        IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                        strCurrency & "|" & strExRate & "|" & strAddDiscount & "|" & strDate & "|" & txtCreditTerm.Text & "|" & lblHidStatusEdited.Text & "|" & _
                        IIf(chkPrinted.Checked = True, "1", "0")

            Try
                intErrNo = objPU.mtdUpdPO(strOpCd_AddPO, _
                                        strOpCd_UpdPO, _
                                        strOppCd, _
                                        strOppCd_Back, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.NewPurchaseOrder), _
                                        objPOId)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_SAVE_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
            End Try

        Else

            strParam = lblPOId.Text & "|" & _
                       lblPOType.Text & "|" & _
                       strSuppCode & "|" & _
                       strRemark & "|" & _
                       objPU.EnumPOStatus.Deleted & "|||||||||" & Trim(strDeptCode) & "|" & Trim(SelectedPOIssued) & "|" & _
                       IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                       strCurrency & "|" & strExRate & "|" & strAddDiscount & "|" & strDate & "|" & txtCreditTerm.Text & "|" & lblHidStatusEdited.Text & "|" & _
                       IIf(chkPrinted.Checked = True, "1", "0")

            Try
                intErrNo = objPU.mtdUpdPO(strOpCd_AddPO, _
                                          strOpCd_UpdPO, _
                                          strOppCd, _
                                          strOppCd_Back, _
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseOrder), _
                                          objPOId)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DELETE_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
            End Try
        End If
        onLoad_DisplayPO(lblPOId.Text)
        onLoad_DisplayPOLn(lblPOId.Text)
    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddPO As String = "PU_CLSTRX_PO_ADD"
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"
        Dim strOppCd_GetID As String = "PU_CLSTRX_PO_GETID"
        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "PO"
        Dim strHistYear As String = ""
        Dim blnIsDetail As Boolean = True
        Dim objPUDs As Object
        Dim objPOId As New Object()
        Dim strSuppCode As String = ddlSuppCode.SelectedItem.Value
        Dim strRemark As String = Trim(Request.Form("txtRemark"))
        Dim strParam As String = ""
        Dim intErrNo As Integer

        Dim strNewIDFormat As String
        Dim strPOIssued As String
        Dim SelectedPOIssued As String = ddlPOIssued.SelectedItem.Value
        Dim strDeptCode As String = ddlDeptCode.SelectedItem.Value
        Dim strAddDiscount As String = Trim(txtAddDisc.Text)
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDateCreated.Text.Trim(), indDate) = False Then
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

        If lblPOType.Text = objPU.EnumPOType.Stock Or lblPOType.Text = objPU.EnumPOType.FixedAsset Then
            Select Case CInt(SelectedPOIssued)
                Case objPU.EnumPOIssued.JKT
                    strPOIssued = "JKT"
                Case objPU.EnumPOIssued.PKU
                    strPOIssued = "PKU"
                Case objPU.EnumPOIssued.LMP
                    strPOIssued = "LMP"
                Case objPU.EnumPOIssued.PLM
                    strPOIssued = "PLM"
                Case objPU.EnumPOIssued.BKL
                    strPOIssued = "BKL"
                Case objPU.EnumPOIssued.LOK
                    strPOIssued = "LOK"
            End Select
        End If


        If len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
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
            strHistYear = right(strLastPhyYear, 2)
            strNewYear = "1"
        End If
        If lblPOType.Text = objPU.EnumPOType.Stock Or lblPOType.Text = objPU.EnumPOType.FixedAsset Then

            strNewIDFormat = Left(Trim(strDeptCode), 3) & "/" & strLocation & "/" & strPOIssued & "/" & Right(strPhyYear, 2) & "/" & strPhyMonth & "/"

            strParam = lblPOId.Text & "|" & _
                            lblPOType.Text & "|" & _
                            strSuppCode & "|" & _
                            strRemark & "|" & _
                            objPU.EnumPOStatus.Active & "||" & _
                            strAccMonth & "|" & _
                            strAccYear & "|" & _
                            strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
                            Trim(strDeptCode) & "|" & Trim(SelectedPOIssued) & "|" & _
                            IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                            strCurrency & "|" & strExRate & "|" & strAddDiscount & "|" & strDate & "|" & txtCreditTerm.Text & "|" & lblHidStatusEdited.Text & "|" & _
                            IIf(chkPrinted.Checked = True, "1", "0")

            Try
                intErrNo = objPU.mtdUpdPO(strOpCd_AddPO, _
                                        strOpCd_UpdPO, _
                                        strOppCd, _
                                        strOppCd_Back, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.NewPurchaseOrder), _
                                        objPOId)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_SAVE_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
            End Try
        Else

            strParam = lblPOId.Text & "|" & _
                            lblPOType.Text & "|" & _
                            strSuppCode & "|" & _
                            strRemark & "|" & _
                            objPU.EnumPOStatus.Active & "|||||||||" & Trim(strDeptCode) & "|" & Trim(SelectedPOIssued) & "|" & IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                            strCurrency & "|" & strExRate & "|" & strAddDiscount & "|" & strDate & "|" & txtCreditTerm.Text & "|" & lblHidStatusEdited.Text & "|" & _
                       IIf(chkPrinted.Checked = True, "1", "0")

            Try
                intErrNo = objPU.mtdUpdPO(strOpCd_AddPO, _
                                        strOpCd_UpdPO, _
                                        strOppCd, _
                                        strOppCd_Back, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseOrder), _
                                        objPOId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UNDELETE_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
            End Try
        End If
        txtRemark.Text = strRemark
        lblStatus.Text = objPU.mtdGetPOStatus(objPU.EnumPOStatus.Active)
        onLoad_DisplayPO(lblPOId.Text)
        onLoad_DisplayPOLn(lblPOId.Text)
    End Sub

    Sub btnCancel_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPoLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdCost As String = "PU_CLSTRX_COST_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDateCreated.Text.Trim(), indDate) = False Then
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

        strParam = lblPOId.Text & "||" & objPU.EnumPOLnStatus.Cancelled & "||"

        Try
            intErrNo = objPU.mtdUpdPOLn(strOpCd_GetPOLn, _
                                        strOpCd_UpdPoLn, _
                                        strOpCd_UpdPO, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdCost, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        False)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_CANCEL_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try
        onLoad_DisplayPO(lblPOId.Text)
        onLoad_DisplayPOLn(lblPOId.Text)
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDec As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strParam As String
        Dim strPIC1 As String
        Dim strJabatan1 As String
        Dim strPIC2 As String
        Dim strJabatan2 As String
        Dim strCatatan As String
        Dim strLokasi As String
        Dim strPONoFrom As String
        Dim strPONoTo As String
        Dim strPeriodeFrom As String
        Dim strPeriodeTo As String

        Dim strDateSetting As String
        Dim objDateFrom As String
        Dim objDateTo As String
        Dim objDateFormat As String
        Dim intCnt As Integer

        If Trim(lblStatus.Text) = objPU.mtdGetPOStatus(objPU.EnumPOStatus.Active) Then
            btnSave_Click(sender, e)
        End If

        strPIC1 = strUserId
        strJabatan1 = ""
        strPIC2 = ""
        strJabatan2 = ""
        strCatatan = ""
        strLokasi = ""
        strRptId = "RPTPU1000012"
        strRptName = "Purchase Order"
        strDec = "2"
        strPeriodeFrom = ""
        strPeriodeTo = ""
        strPONoFrom = lblPOId.Text
        strPONoTo = ""

        Response.Write("<Script Language=""JavaScript"">window.open(""PU_trx_PrintDocs.aspx?doctype=1&CompName=" & strCompany & _
                        "&TrxID=" & lblPOId.Text & _
                        """,null ,""status=yes, height=400, width=600, top=180, left=220, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")


        'Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_POPFDet.aspx?Type=Print&CompName=" & strCompany & _
        '               "&Location=" & strLocation & _
        '               "&RptId=" & strRptId & _
        '               "&RptName=" & strRptName & _
        '               "&Decimal=" & strDec & _
        '               "&PONoFrom=" & strPONoFrom & _
        '               "&PONoTo=" & strPONoTo & _
        '               "&PeriodeFrom=" & strPeriodeFrom & _
        '               "&PeriodeTo=" & strPeriodeTo & _
        '               "&PIC1=" & strPIC1 & _
        '               "&Jabatan1=" & strJabatan1 & _
        '               "&PIC2=" & strPIC2 & _
        '               "&Jabatan2=" & strJabatan2 & _
        '               "&Catatan=" & strCatatan & _
        '               "&Lokasi=" & strLokasi & _
        '               """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

        'onLoad_DisplayPO(lblPOId.Text)
        'onLoad_DisplayPOLn(lblPOId.Text)
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_POList.aspx")
    End Sub

    Function GoodsReceived(ByVal pv_strPOLnId As String) As String
        Dim strParam As String
        Dim strOpCd As String = "PU_GET_PO_GOODS_RECEIVED"
        Dim objRsl As Object
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        If Trim(pv_strPOLnId) = "" Then
            Return False
        End If

        strParamName = "POLNID|POID|LOCCODE"
        strParamValue = Trim(pv_strPOLnId) & "|" & Trim(lblPOId.Text) & "|" & Trim(strLocation)
        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objRsl)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PO_GDRCV&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        'strParam = pv_strPOLnId

        'Try
        '    intErrNo = objPU.mtdGetPOGDRcv(strOpCd, strParam, objRsl)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_PO_GDRCV&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        'End Try

        If objRsl.Tables(0).Rows.Count > 0 Then
            Return True
        End If

        Return False
    End Function

    Sub Centralized_Type(ByVal Sender As Object, ByVal E As EventArgs)
        If chkCentralized.Checked = True Then
            Centralized_Yes.Visible = True
            Centralized_No.Visible = False
            chkCentralized.Text = "  Yes"
            BindPR("")
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

    Sub CurrencyChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strParam As String
        Dim strSearch As String
        Dim strSort As String
        Dim strOpCdGet As String = "CM_CLSSETUP_EXCHANGERATE_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer
        Dim objCurrencyDs As New Object()
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)

        strSearch = "and exc.SecCurrencyCode = 'IDR' and exc.FirstCurrencyCode = '" & ddlCurrency.SelectedItem.Value & "' and exc.Status = '" & objCMSetup.EnumExchangeRateStatus.Active & "' and exc.TransDate = '" & strDate & "' "
        strSort = "order by exc.FirstCurrencyCode "

        strParam = strSearch & "|" & strSort

        Try
            intErrNo = objCMSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objCurrencyDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_CURRENCYLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=CM/Trx/CM_Trx_ContractRegList.aspx")
        End Try

        If objCurrencyDs.Tables(0).Rows.Count > 0 Then
            txtExRate.Text = objCurrencyDs.Tables(0).Rows(0).Item("ExchangeRate")
        Else
            If Trim(ddlCurrency.SelectedItem.Value) <> "IDR" Then
                lblErrExRate.Visible = True
                Exit Sub
            End If
        End If
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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
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

    Sub CheckType()
        If lblPOType.Text <> "" Then
            Select Case lblStatus.Text
                Case objPU.mtdGetPOStatus(objPU.EnumPOStatus.Active)
                    Select Case lblPOType.Text
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
                    Select Case lblPOType.Text
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
        Dim strItemCode As String
        Dim strPOLnId As String
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDateCreated.Text.Trim(), indDate) = False Then
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

        dgPODet.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblPOLnId")
        strPOLnId = lbl.Text.Trim
        lblHidPOlnId.Text = strPOLnId

        lbl = E.Item.FindControl("lblPRRefId")
        txtPRRefId.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblItem")
        strSelectedItemCode = lbl.Text.Trim

        lbl = E.Item.FindControl("lblGetRefNo")
        hidGetRefNo.Value = lbl.Text.Trim
        GetItem(strSelectedItemCode)

        lbl = E.Item.FindControl("lblPRId")
        If chkCentralized.Checked = True Then
            ddlPRId.SelectedItem.Value = lbl.Text.Trim
            ddlPRId.Enabled = False
        Else
            txtPRID.Text = lbl.Text.Trim
            txtPRID.Enabled = False
        End If

        lbl = E.Item.FindControl("lblPRLocCode")
        lblPRLocCode.Text = lbl.Text.Trim
        
        lbl = E.Item.FindControl("lblPRRefLocCode")
        ddlPRRefLocCode.SelectedItem.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblQtyOrder")
        txtQtyOrder.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", ".")
        'txtQtyOrder.Text = FormatNumber(lbl.Text.Trim, , , , TriState.False)
        lbl = E.Item.FindControl("lblCost")        
        txtCost.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", ".")
        'txtCost.Text = FormatNumber(lbl.Text.Trim, , , , TriState.False)
        lbl = E.Item.FindControl("lblAmountToEdit")
        txtTtlCost.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", ".")
        'txtTtlCost.Text = FormatNumber(lbl.Text.Trim, , , , TriState.False)
        lbl = E.Item.FindControl("lblDiscount")
        txtDiscount.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", ".")
        'txtDiscount.Text = FormatNumber(lbl.Text.Trim, , , , TriState.False)

        If CDbl(txtDiscount.Text) <> 0 Then
            txtCost.Text = Round(CDbl(txtCost.Text) / ((100 - CDbl(txtDiscount.Text)) / 100), 2)
            txtTtlCost.Text = Round(CDbl(txtTtlCost.Text) / ((100 - CDbl(txtDiscount.Text)) / 100), 2)
        End If

        lbl = E.Item.FindControl("lblTransporter")
        ddlTransporter.SelectedItem.Value = lbl.Text.Trim
        strSelectedTransCode = ddlTransporter.SelectedItem.Value
        BindTransporter("")
        lbl = E.Item.FindControl("lblTransportFee")
        txtAmtTransportFee.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", ".")
        'txtAmtTransportFee.Text = FormatNumber(lbl.Text.Trim, , , , TriState.False)
        lbl = E.Item.FindControl("lblPBBKB")
        txtPBBKB.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", ".")
        'txtPBBKB.Text = FormatNumber(lbl.Text.Trim, , , , TriState.False)
        lbl = E.Item.FindControl("lblPBBKBRate")
        txtPBBKBRate.Text = lbl.Text.Trim
        'txtPBBKBRate.Text = FormatNumber(lbl.Text.Trim, , , , TriState.False)
        lbl = E.Item.FindControl("lblPPN22")
        txtPPN22.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", ".")
        'txtPPN22.Text = FormatNumber(lbl.Text.Trim, , , , TriState.False)
        lbl = E.Item.FindControl("lblAddNote")
        txtAddNote.Value = lbl.Text.Trim

        'lbl = E.Item.FindControl("lblPPNStatus")
        'If lbl.Text.Trim = objPU.EnumPPN.Yes Then
        '    chkPPN.Checked = True
        'Else
        '    chkPPN.Checked = False
        'End If
        ''chkPPN.Enabled = True

        lbl = E.Item.FindControl("lblPPNTransporter")
        If lbl.Text.Trim = objPU.EnumPPN.Yes Then
            chkPPNTransport.Checked = True
        Else
            chkPPNTransport.Checked = False
        End If
        chkPPNTransport.Enabled = False

        If Trim(hidPPN.Value) = "1" Then
            chkPPN.Checked = True
        Else
            chkPPN.Checked = False
        End If
        chkPPN.Enabled = False

        'khusus untuk pembuatan surat2 kendaraan maka tidak ada ppn
        lbl = E.Item.FindControl("lblPPNStatus")
        If lbl.Text.Trim = "3" Then
            chkPPN.Checked = False
            chkSurat.Checked = True
        Else
            chkSurat.Checked = False
        End If

        'If Trim(hidPPNOA.Value) = "1" Then
        '    chkPPNTransport.Checked = True
        'Else
        '    chkPPNTransport.Checked = False
        'End If
        'chkPPNTransport.Enabled = False

        lbl = E.Item.FindControl("lblPPH23")
        txtPPH23.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", ".")
        'txtPPH23.Text = FormatNumber(lbl.Text.Trim, , , , TriState.False)
        lbl = E.Item.FindControl("lblGrossUp")
        If Trim(lbl.Text.Trim) = "1" Then
            chkGrossUp.Checked = True
        Else
            chkGrossUp.Checked = False
        End If
        lbl = E.Item.FindControl("lblPPH23OA")
        txtPPH23OA.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", ".")
        'txtPPH23OA.Text = FormatNumber(lbl.Text.Trim, , , , TriState.False)
        lbl = E.Item.FindControl("lblGrossUpOA")
        If Trim(lbl.Text.Trim) = "1" Then
            chkGrossUpOA.Checked = True
        Else
            chkGrossUpOA.Checked = False
        End If
        lbl = E.Item.FindControl("lblPPH21")
        txtPPH21.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", ".")
        'txtPPH21.Text = FormatNumber(lbl.Text.Trim, , , , TriState.False)
        lbl = E.Item.FindControl("lblGrossUp21")
        If Trim(lbl.Text.Trim) = "1" Then
            chkGrossUp21.Checked = True
        Else
            chkGrossUp21.Checked = False
        End If
        lbl = E.Item.FindControl("lblUOMCode")
        BindUOM(lbl.Text.Trim)

        btn = E.Item.FindControl("Delete")
        btn.Visible = False
        btn = E.Item.FindControl("Edit")
        btn.Visible = False
        btn = E.Item.FindControl("Cancel")
        btn.Visible = True
    End Sub

    Sub btnAddendum_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetPO As String = "PU_CLSTRX_PO_GET"
        Dim strOpCd As String = "PU_CLSTRX_PO_ADD_ADDENDUM"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim dsMaster As Object
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDateCreated.Text.Trim(), indDate) = False Then
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

        strParamName = "STRSEARCH"
        strParamValue = "AND A.POID = '" & Trim(lblPOId.Text) & "-ADD' "
        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_GetPO, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_POdet")
        End Try

        If dsMaster.Tables(0).Rows.Count = 0 Then
            strParamName = "POID|USERID"
            strParamValue = Trim(lblPOId.Text) & _
                            "|" & Trim(strUserId)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_POdet")
            End Try

            Response.Redirect("PU_trx_PODet.aspx?POId=" & Trim(lblPOId.Text) & "-ADD")
        Else
            lblErrGR.Visible = True
            lblErrGR.Text = "This PO already have addendum."
            Exit Sub
        End If
    End Sub

    Sub btnEdited_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddPO As String = "PU_CLSTRX_PO_ADD"
        Dim strOpCd_UpdPO As String = "PU_CLSTRX_PO_UPD"
        Dim strOppCd_GetID As String = "PU_CLSTRX_PO_GETID"
        Dim strOppCd As String = "PU_CLSTRX_PO_MOVEID"
        Dim strOppCd_Back As String = "PU_CLSTRX_PO_BACKID"
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "PO"
        Dim strHistYear As String = ""
        Dim blnIsDetail As Boolean = True
        Dim objPUDs As Object
        Dim objPOId As New Object()
        Dim strSuppCode As String = ddlSuppCode.SelectedItem.Value
        Dim strRemark As String = Trim(Request.Form("txtRemark"))
        Dim strParam As String = ""
        Dim intErrNo As Integer

        Dim strNewIDFormat As String
        Dim strPOIssued As String
        Dim SelectedPOIssued As String = ddlPOIssued.SelectedItem.Value
        Dim strDeptCode As String = ddlDeptCode.SelectedItem.Value
        Dim strAddDiscount As String = Trim(txtAddDisc.Text)
        Dim strDate As String = Date_Validation(txtDateCreated.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDateCreated.Text.Trim(), indDate) = False Then
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

        If lblPOType.Text = objPU.EnumPOType.Stock Or lblPOType.Text = objPU.EnumPOType.FixedAsset Then

            strNewIDFormat = Left(Trim(strDeptCode), 3) & "/" & strLocation & "/" & strPOIssued & "/" & Right(strPhyYear, 2) & "/" & strPhyMonth & "/"

            strParam = lblPOId.Text & "|" & _
                                lblPOType.Text & "|" & _
                                strSuppCode & "|" & _
                                strRemark & "|" & _
                                objPU.EnumPOStatus.Active & "||" & _
                                strAccMonth & "|" & _
                                strAccYear & "|" & _
                                strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
                                Trim(strDeptCode) & "|" & Trim(SelectedPOIssued) & "|" & _
                                IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                                strCurrency & "|" & strExRate & "|" & strAddDiscount & "|" & strDate & "|" & txtCreditTerm.Text & "|" & "1" & "|" & _
                                IIf(chkPrinted.Checked = True, "1", "0")


            Try
                intErrNo = objPU.mtdUpdPO(strOpCd_AddPO, _
                                        strOpCd_UpdPO, _
                                        strOppCd, _
                                        strOppCd_Back, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.NewPurchaseOrder), _
                                        objPOId)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_SAVE_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
            End Try
        Else

            strParam = lblPOId.Text & "|" & _
                    lblPOType.Text & "|" & _
                    strSuppCode & "|" & _
                    strRemark & "|" & _
                    objPU.EnumPOStatus.Active & "|||||||||" & Trim(strDeptCode) & "|" & Trim(SelectedPOIssued) & "|" & IIf(chkCentralized.Checked = True, "1", "0") & "|" & _
                    strCurrency & "|" & strExRate & "|" & strAddDiscount & "|" & strDate & "|" & txtCreditTerm.Text & "|" & "1" & "|" & _
                    IIf(chkPrinted.Checked = True, "1", "0")

            Try
                intErrNo = objPU.mtdUpdPO(strOpCd_AddPO, _
                                        strOpCd_UpdPO, _
                                        strOppCd, _
                                        strOppCd_Back, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseOrder), _
                                        objPOId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UNDELETE_PO&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
            End Try
        End If
        txtRemark.Text = strRemark
        lblStatus.Text = objPU.mtdGetPOStatus(objPU.EnumPOStatus.Active)
        onLoad_DisplayPO(lblPOId.Text)
        onLoad_DisplayPOLn(lblPOId.Text)
        CheckType()
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

        If lblPOType.Text = objPU.EnumPOType.Stock Then
            strPOType = objINSetup.EnumInventoryItemType.Stock & "','" & objINSetup.EnumInventoryItemType.WorkshopItem
            strParamValue = " AND (itm.ItemType IN ('" & strPOType & "') OR itm.ItemType = '" & objPU.EnumPOType.DirectCharge & "')"
        ElseIf lblPOType.Text = objPU.EnumPOType.Nursery Then
            strPOType = lblPOType.Text
            strParamValue = " AND (itm.ItemType IN ('" & strPOType & "'))"
        Else
            strPOType = lblPOType.Text
            strParamValue = " AND (itm.ItemType IN ('" & strPOType & "') OR itm.ItemType = '" & objPU.EnumPOType.DirectCharge & "')"
        End If

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = strParamValue & " AND itm.ItemCode = '" & Trim(pv_strItemCode) & "' AND itm.LocCode = '" & strLocation & "' AND itm.Status = '" & objINSetup.EnumStockItemStatus.Active & "'  " & "|itm.ItemCode"

        If hidGetRefNo.Value = 1 Then
            strOpCode = "PU_CLSTRX_PO_GETITEM_SPK"

            strParamName = "POID|LOCCODE|STRSEARCH"
            strParamValue = Trim(txtPRRefId.Text) & "|" & strLocation & "|" & _
                            " AND A.STATUS NOT IN ('" & objPU.EnumPOStatus.Deleted & "','" & objPU.EnumPOStatus.Cancelled & "') AND B.POLNID = SUBSTRING('" & Trim(lblHidPOlnId.Text) & "',1,LEN('" & Trim(lblHidPOlnId.Text) & "')-2) " 'REPLACE(REPLACE(REPLACE(REPLACE('" & Trim(lblHidPOlnId.Text) & "','-0',''),'-1',''),'-2',''),'-3','') "
        End If

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_PO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_POdet")
        End Try

        For intCnt = 0 To dsMaster.Tables(0).Rows.Count - 1
            If Trim(dsMaster.Tables(0).Rows(intCnt).Item("ItemCode")) = Trim(strSelectedItemCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsMaster.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = lblSelectListItem.Text & lblStockItem.Text
        dsMaster.Tables(0).Rows.InsertAt(dr, 0)

        ddlItemCode.DataSource = dsMaster.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex
    End Sub

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

        ddlUOM.DataSource = objUOMDs.Tables(0)
        ddlUOM.DataTextField = "UOMDesc"
        ddlUOM.DataValueField = "UOMCode"
        ddlUOM.DataBind()
        ddlUOM.SelectedIndex = intSelectedIndex
    End Sub

    Sub BtnNew_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("PU_trx_PODet.aspx?POType=" & lblPOType.Text)
    End Sub

    Sub onSelect_Supp(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strOpCd_GetCreditTerm As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim strParam As String '= Trim(ddlSuppCode.SelectedItem.Value) & "||||SupplierCode||"
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intDefIndex As Integer = 0
        Dim intSelectedIndex As Integer = 0
        Dim crtFound As Boolean = False
        Dim objCreditTermDs As New Object()
        Dim strSuppType As String = objPUSetup.EnumSupplierType.Internal & "','" & objPUSetup.EnumSupplierType.External & "','" & objPUSetup.EnumSupplierType.Associate & "','" & objPUSetup.EnumSupplierType.Contractor

        strParam = Trim(ddlSuppCode.SelectedItem.Value) & "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||SELECT"
        strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")
        strParam = strParam & "|" & strSuppType

        Try
            intErrNo = objPUSetup.mtdGetSupplier(strOpCd_GetCreditTerm, strParam, objCreditTermDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        If objCreditTermDs.Tables(0).Rows.Count > 0 Then
            txtCreditTerm.Text = Trim(objCreditTermDs.Tables(0).Rows(0).Item("CreditTerm"))
            If Trim(objCreditTermDs.Tables(0).Rows(0).Item("PPNInit")) = "0" Then
                chkPPN.Checked = False
            Else
                chkPPN.Checked = True
            End If
            If lblPOId.Text <> "" Then
                If hidPPN.Value <> Trim(objCreditTermDs.Tables(0).Rows(0).Item("PPNInit")) Then
                    lblSuppCode.Text = "Supplier changed and " & IIf(Trim(objCreditTermDs.Tables(0).Rows(0).Item("PPNInit")) = "0", "dont have PPN", "have PPN") & ".<br>Please check PPN Amount on each item, edit each item if necessary."
                    lblSuppCode.Visible = True
                End If
            End If

            hidPPN.Value = Trim(objCreditTermDs.Tables(0).Rows(0).Item("PPNInit"))
            chkPPN.Enabled = False
        End If
    End Sub

    Sub onSelect_Transporter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strOpCd_GetCreditTerm As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim strParam As String '= Trim(ddlSuppCode.SelectedItem.Value) & "||||SupplierCode||"
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intDefIndex As Integer = 0
        Dim intSelectedIndex As Integer = 0
        Dim crtFound As Boolean = False
        Dim objCreditTermDs As New Object()
        Dim strSuppType As String = objPUSetup.EnumSupplierType.Internal & "','" & objPUSetup.EnumSupplierType.External & "','" & objPUSetup.EnumSupplierType.Associate & "','" & objPUSetup.EnumSupplierType.Contractor

        strParam = Trim(ddlTransporter.SelectedItem.Value) & "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||SELECT"
        strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")
        strParam = strParam & "|" & strSuppType

        Try
            intErrNo = objPUSetup.mtdGetSupplier(strOpCd_GetCreditTerm, strParam, objCreditTermDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        If objCreditTermDs.Tables(0).Rows.Count > 0 Then
            If Trim(objCreditTermDs.Tables(0).Rows(0).Item("PPNInit")) = "0" Then
                chkPPNTransport.Checked = False
            Else
                chkPPNTransport.Checked = True
            End If
            If lblPOId.Text <> "" Then
                If hidPPNOA.Value <> Trim(objCreditTermDs.Tables(0).Rows(0).Item("PPNInit")) Then
                    lblErrTransporter.Text = "<br>Transporter changed and " & IIf(Trim(objCreditTermDs.Tables(0).Rows(0).Item("PPNInit")) = "0", "dont have PPN", "have PPN") & ".<br>Please check PPN Transport Amount on each item, edit each item if necessary."
                    lblErrTransporter.Visible = True
                End If
            End If

            hidPPNOA.Value = Trim(objCreditTermDs.Tables(0).Rows(0).Item("PPNInit"))
            chkPPNTransport.Enabled = False
        End If
    End Sub

    Sub onLoad_DisplaySPK(ByVal pv_strPOId As String)
        Dim strOpCode As String = "PU_CLSTRX_PO_SPK_GET"
        Dim dsMaster As Object

        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer

        strParamName = "TRXID"
        strParamValue = Trim(pv_strPOId)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        If dsMaster.Tables(0).Rows.Count > 0 Then
            'tblSPK.Visible = True

            dgSPK.DataSource = Nothing
            dgSPK.DataSource = dsMaster.Tables(0)
            dgSPK.DataBind()
            'Else
            '    tblSPK.Visible = False
        End If
    End Sub

    Sub chkGrossUP_Changed(ByVal Sender As Object, ByVal E As EventArgs)
        If chkGrossUp.Checked = True Then
            If txtPPH23.Text = "" Or txtPPH23.Text = "0" Then
                lblerrPPH23.Visible = True
                Exit Sub
            Else
                If chkPPN.Checked = True Then
                    hidOriCost.Value = txtCost.Text
                    txtPPH23.ReadOnly = True
                    txtAddNote.Value = "(NILAI SETELAH POTONG PPH 23 " + Trim(txtPPH23.Text) + "% SEBESAR RP " + Trim(objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(hidOriCost.Value, 2), 2)) + ")"
                Else
                    hidOriCost.Value = txtCost.Text
                    txtCost.Text = Round(CDbl(txtCost.Text) / ((100 - CDbl(txtPPH23.Text)) / 100), 0)
                    txtTtlCost.Text = CDbl(txtQtyOrder.Text) * CDbl(txtCost.Text)
                    txtPPH23.ReadOnly = True
                    txtAddNote.Value = "(NILAI SETELAH POTONG PPH 23 " + Trim(txtPPH23.Text) + "% SEBESAR RP " + Trim(objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(hidOriCost.Value, 2), 2)) + ")"
                End If
            End If
        Else
            If txtCost.Text = "" And (txtPPH23.Text = "" Or txtPPH23.Text = "0") Then
                lblerrPPH23.Visible = True
                Exit Sub
            Else
                If chkPPN.Checked = True Then
                    txtPPH23.ReadOnly = False
                    txtAddNote.Value = "(POTONG PPH 23 " + Trim(txtPPH23.Text) + "%)"
                Else
                    txtCost.Text = CDbl(txtCost.Text) - (Round(CDbl(txtCost.Text) * (txtPPH23.Text / 100), 0))
                    txtTtlCost.Text = CDbl(txtQtyOrder.Text) * CDbl(txtCost.Text)
                    txtPPH23.ReadOnly = False
                    txtAddNote.Value = "(POTONG PPH 23 " + Trim(txtPPH23.Text) + "%)"
                End If
            End If
        End If
    End Sub

    Sub chkGrossUPOA_Changed(ByVal Sender As Object, ByVal E As EventArgs)
        If chkGrossUpOA.Checked = True Then
            If txtPPH23OA.Text = "" Or txtPPH23OA.Text = "0" Then
                lblerrPPH23OA.Visible = True
                Exit Sub
            Else
                If Trim(ddlSuppCode.SelectedItem.Value) = Trim(ddlTransporter.SelectedItem.Value) Then
                    If chkPPNTransport.Checked = True Then
                        hidOriCostOA.Value = txtAmtTransportFee.Text
                        txtPPH23OA.ReadOnly = True
                        txtAddNote.Value = "(NILAI SETELAH POTONG PPH 23 " + Trim(txtPPH23OA.Text) + "% SEBESAR RP " + Trim(objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(hidOriCostOA.Value, 2), 2)) + ")"
                    Else
                        hidOriCostOA.Value = txtAmtTransportFee.Text
                        txtAmtTransportFee.Text = Round(CDbl(txtAmtTransportFee.Text) / ((100 - CDbl(txtPPH23OA.Text)) / 100), 0)
                        txtPPH23OA.ReadOnly = True
                        txtAddNote.Value = "(NILAI SETELAH POTONG PPH 23 " + Trim(txtPPH23OA.Text) + "% SEBESAR RP " + Trim(objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(hidOriCostOA.Value, 2), 2)) + ")"
                        Response.Write(Round(CDbl(txtAmtTransportFee.Text) / ((100 - CDbl(txtPPH23OA.Text)) / 100), 0))
                    End If
                Else
                    If chkPPNTransport.Checked = True Then
                        hidOriCostOA.Value = txtAmtTransportFee.Text
                        txtPPH23OA.ReadOnly = True
                    Else
                        hidOriCostOA.Value = txtAmtTransportFee.Text
                        txtAmtTransportFee.Text = Round(CDbl(txtAmtTransportFee.Text) / ((100 - CDbl(txtPPH23OA.Text)) / 100), 0)
                        txtPPH23OA.ReadOnly = True
                    End If
                End If
            End If
        Else
            If txtAmtTransportFee.Text = "" And (txtPPH23OA.Text = "" Or txtPPH23OA.Text = "0") Then
                lblerrPPH23OA.Visible = True
                Exit Sub
            Else
                If Trim(ddlSuppCode.SelectedItem.Value) = Trim(ddlTransporter.SelectedItem.Value) Then
                    If chkPPNTransport.Checked = True Then
                        txtPPH23OA.ReadOnly = False
                        txtAddNote.Value = "(POTONG PPH 23 " + Trim(txtPPH23OA.Text) + "%)"
                    Else
                        txtAmtTransportFee.Text = CDbl(txtAmtTransportFee.Text) - (Round(CDbl(txtAmtTransportFee.Text) * (txtPPH23OA.Text / 100), 0))
                        txtPPH23OA.ReadOnly = False
                        txtAddNote.Value = "(POTONG PPH 23 " + Trim(txtPPH23OA.Text) + "%)"
                    End If
                Else
                    If chkPPNTransport.Checked = True Then
                        txtPPH23OA.ReadOnly = False
                    Else
                        txtAmtTransportFee.Text = CDbl(txtAmtTransportFee.Text) - (Round(CDbl(txtAmtTransportFee.Text) * (txtPPH23OA.Text / 100), 0))
                        txtPPH23OA.ReadOnly = False
                    End If
                End If
            End If
        End If
    End Sub

    Sub chkGrossUP21_Changed(ByVal Sender As Object, ByVal E As EventArgs)
        If chkGrossUp21.Checked = True Then
            If txtPPH21.Text = "" Or txtPPH21.Text = "0" Then
                lblerrPPH21.Visible = True
                Exit Sub
            Else
                If chkPPN.Checked = True Then
                    hidOriCost.Value = txtCost.Text
                    txtPPH21.ReadOnly = True
                    txtAddNote.Value = "(NILAI SETELAH POTONG PPH 21 " + Trim(txtPPH21.Text) + "% SEBESAR RP " + Trim(objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(hidOriCost.Value, 2), 2)) + ")"
                Else
                    hidOriCost.Value = txtCost.Text
                    txtCost.Text = Round(CDbl(txtCost.Text) / ((100 - CDbl(txtPPH21.Text)) / 100), 0)
                    txtTtlCost.Text = CDbl(txtQtyOrder.Text) * CDbl(txtCost.Text)
                    txtPPH21.ReadOnly = True
                    txtAddNote.Value = "(NILAI SETELAH POTONG PPH 21 " + Trim(txtPPH21.Text) + "% SEBESAR RP " + Trim(objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(hidOriCost.Value, 2), 2)) + ")"
                End If
            End If
        Else
            If txtCost.Text = "" And (txtPPH21.Text = "" Or txtPPH21.Text = "0") Then
                lblerrPPH21.Visible = True
                Exit Sub
            Else
                If chkPPN.Checked = True Then
                    txtPPH21.ReadOnly = False
                    txtAddNote.Value = "(POTONG PPH 21 " + Trim(txtPPH21.Text) + "%)"
                Else
                    txtCost.Text = CDbl(txtCost.Text) - (Round(CDbl(txtCost.Text) * (txtPPH21.Text / 100), 0))
                    txtTtlCost.Text = CDbl(txtQtyOrder.Text) * CDbl(txtCost.Text)
                    txtPPH21.ReadOnly = False
                    txtAddNote.Value = "(POTONG PPH 21 " + Trim(txtPPH21.Text) + "%)"
                End If
            End If
        End If
    End Sub

    Sub chkSuratChanged(ByVal Sender As Object, ByVal E As EventArgs)
        If lblPOType.Text = objPU.EnumPOType.DirectCharge Then
            If chkSurat.Checked = True Then
                chkPPN.Checked = False
            Else
                chkPPN.Checked = IIf(Trim(hidPPN.Value) = "0", False, True)
            End If
        End If
    End Sub

    Sub GetRefNoBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCode As String = "PU_CLSTRX_PO_GETITEM_SPK"
        Dim dsMaster As Object
        Dim dr As DataRow
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0

        strParamName = "POID|LOCCODE|STRSEARCH"
        strParamValue = Trim(txtPRRefId.Text) & "|" & strLocation & "|" & _
                        " AND A.STATUS NOT IN ('" & objPU.EnumPOStatus.Deleted & "','" & objPU.EnumPOStatus.Cancelled & "') "

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GETPOITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        If dsMaster.Tables(0).Rows.Count > 0 Then
            txtQtyOrder.Text = "0"
            txtCost.Text = "0"
            txtAddNote.Value = ""
            txtAmtTransportFee.Text = "0"
            txtDiscount.Text = "0"
            txtPBBKB.Text = "0"
            txtPBBKBRate.Text = "0"
            txtPPN22.Text = "0"
            txtTtlCost.Text = "0"
            txtPPH23.Text = "0"
            chkGrossUp.Checked = False
            txtPPH23OA.Text = "0"
            chkGrossUpOA.Checked = False
            txtPPH21.Text = "0"
            chkGrossUp21.Checked = False
            hidGetRefNo.Value = 1

            dr = dsMaster.Tables(0).NewRow()
            dr("ItemCode") = ""
            dr("Description") = lblSelectListItem.Text & lblStockItem.Text
            dsMaster.Tables(0).Rows.InsertAt(dr, 0)

            ddlItemCode.DataSource = dsMaster.Tables(0)
            ddlItemCode.DataValueField = "ItemCode"
            ddlItemCode.DataTextField = "Description"
            ddlItemCode.DataBind()
            ddlItemCode.SelectedIndex = intSelectedIndex
        Else
            lblRefNoErr.Visible = True
            lblRefNoErr.Text = "Data not found."
            hidGetRefNo.Value = 0
        End If
    End Sub
End Class
