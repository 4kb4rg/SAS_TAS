

Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class TX_trx_FPEntryDet : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents txtSplCode As TextBox
    Protected WithEvents txtSplName As TextBox
    Protected WithEvents txtSplNPWP As TextBox
    Protected WithEvents txtSplAddress As HtmlTextArea
    Protected WithEvents txtFromTo As TextBox
    Protected WithEvents txtDocId As TextBox
    Protected WithEvents txtTrxID As TextBox
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblCreatedBy As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents AddBtn As ImageButton
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents hidOriDoc As HtmlInputHidden
    Protected WithEvents txtDescr As HtmlTextArea
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents hidTrxID As HtmlInputHidden
    Protected WithEvents hidTrxLnID As HtmlInputHidden
    Protected WithEvents txtFPNo As TextBox
    Protected WithEvents txtFPDate As TextBox
    Protected WithEvents txtFPAmount As TextBox
    Protected WithEvents txtFPDPPAmount As TextBox
    Protected WithEvents txtAddNote As TextBox
    Protected WithEvents lblerrFPDate As Label
    Protected WithEvents hidTtlDocAmount As HtmlInputHidden
    Protected WithEvents hidTtlFPAmount As HtmlInputHidden
    Protected WithEvents hidFPAmount As HtmlInputHidden
    Protected WithEvents lblTtlFPAmt As Label
    Protected WithEvents lblTtlFPDPPAmt As Label
    Protected WithEvents VerifiedBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Protected objCBTrx As New agri.CB.clsTrx()

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCapDs As New DataSet()
    Dim objRptDs As New DataSet()
    Dim objTaxDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim intConfig As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intDocType As Integer

    Dim strFPAccMonth As String
    Dim strFPAccYear As String
    Dim strTrxID As String
    Dim strAccCode As String
    Dim strDocID As String
    Dim strDate As String
    Dim strDescr As String
    Dim strSupplierCode As String
    Dim strSupplierName As String
    Dim strDPPAmount As String
    Dim strDocAmount As String
    Dim strDocLnID As String
    Dim strLocType As String
    Dim strLocLevel As String
    Dim strAcceptDateFormat As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        End If

        AddBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(AddBtn).ToString())
        VerifiedBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(VerifiedBtn).ToString())
        CancelBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(CancelBtn).ToString())

        lblErrMessage.Visible = False
        lblerrFPDate.Visible = False

        If Request.QueryString("TrxID") = "" Then
            strTrxID = Request.QueryString("TrxID")
            strDocID = Request.QueryString("DocID")
            strDate = Request.QueryString("DocDate")
            strAccCode = Request.QueryString("AccCode")
            strAccMonth = Request.QueryString("AccMonth")
            strAccYear = Request.QueryString("AccYear")
            strDescr = Request.QueryString("Descr")
            strDocAmount = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Request.QueryString("DocAmount"), 2), 2)
            strSupplierCode = Request.QueryString("SplCode")
            strSupplierName = Request.QueryString("SplName")
            strDocLnID = Request.QueryString("DocLnID")

            hidTrxID.Value = strTrxID
            hidTtlDocAmount.Value = Request.QueryString("DocAmount")

            onLoad_Display(hidTrxID.Value)
            onLoad_DisplayLn(hidTrxID.Value)
        Else
            strTrxID = Request.QueryString("TrxID")
            strDocID = Request.QueryString("DocID")
            strDate = Request.QueryString("DocDate")
            strAccCode = Request.QueryString("AccCode")
            strAccMonth = Request.QueryString("AccMonth")
            strAccYear = Request.QueryString("AccYear")
            strDescr = Request.QueryString("Descr")
            strDocAmount = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Request.QueryString("DocAmount"), 2), 2)
            strSupplierCode = Request.QueryString("SplCode")
            strSupplierName = Request.QueryString("SplName")
            strDocLnID = Request.QueryString("DocLnID")

            hidTrxID.Value = strTrxID
            hidTtlDocAmount.Value = Request.QueryString("DocAmount")

            onLoad_Display(hidTrxID.Value)
            onLoad_DisplayLn(hidTrxID.Value)
        End If

        txtDescr.Value = strDescr
        txtAmount.Text = strDocAmount
    End Sub

    Sub close_window()
        Response.Write("<Script Language=""JavaScript"">window.close();</Script>")
    End Sub

    Sub onLoad_Display(ByVal pv_strDocID As String)
        Dim strOpCd As String = "TX_CLSTRX_FPENTRY_GET"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer = 0
        Dim TtlDPPAmt As Double
        Dim TtlTaxAmt As Double

        strParamName = "TRXID|DOCID|SUPPLIERCODE|SUPPLIERNAME|LOCCODE|ACCYEAR|ACCMONTH"
        strParamValue = hidTrxID.Value & "|" & _
                        strDocID & "|" & _
                        strSupplierCode & "|" & _
                        strSupplierName & "|" & _
                        strLocation & "|" & _
                        strAccYear & "|" & _
                        strAccMonth

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If ObjTaxDs.Tables(0).Rows.Count > 0 Then
            txtSplCode.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("SupplierCode"))
            txtFromTo.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("SupplierName"))
            txtSplName.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("SplName"))
            txtSplNPWP.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("SplNPWP"))
            txtSplAddress.Value = Trim(ObjTaxDs.Tables(0).Rows(0).Item("SplAddress"))
            txtDocId.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("DocID"))
            strDocID = Trim(txtDocId.Text)

            lblCreateDate.Text = objGlobal.GetLongDate(ObjTaxDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(ObjTaxDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblCreatedBy.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("CreateName"))
            lblUpdatedBy.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("UpdateName"))

            If Trim(objTaxDs.Tables(0).Rows(0).Item("Status")) = objCBTrx.EnumTaxStatus.Verified Then
                CancelBtn.Visible = True
                VerifiedBtn.Visible = False
            Else
                CancelBtn.Visible = False
                VerifiedBtn.Visible = True
            End If
        End If
    End Sub

    Sub onLoad_DisplayLn(ByVal pv_strDocID As String)
        Dim strOpCd As String = "TX_CLSTRX_FPENTRYLN_GET"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer = 0
        Dim TtlFPDPPAmt As Double
        Dim TtlFPAmt As Double
        Dim DelButton As LinkButton
        Dim EdtButton As LinkButton
        Dim CanButton As LinkButton
        Dim lbl As Label
        Dim strStatus As String
        Dim strInitStatus As String

        strParamName = "TRXID|LOCCODE|DOCID|SUPPLIERCODE"
        strParamValue = hidTrxID.Value & "|" & _
                        strLocation & "|" & _
                        strDocID & "|" & _
                        strSupplierCode

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLineDet.DataSource = objTaxDs.Tables(0)
        dgLineDet.DataBind()

        For intCnt = 0 To objTaxDs.Tables(0).Rows.Count - 1
            EdtButton = dgLineDet.Items.Item(intCnt).FindControl("lbEdit")
            DelButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
            CanButton = dgLineDet.Items.Item(intCnt).FindControl("lbCancel")
            DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            If hidTrxLnID.Value = "" Then
                EdtButton.Visible = True
                DelButton.Visible = True
                CanButton.Visible = False
            Else
                EdtButton.Visible = False
                DelButton.Visible = False
                CanButton.Visible = True
            End If

            lbl = dgLineDet.Items.Item(intCnt).FindControl("lblStatus")
            strStatus = Trim(lbl.Text)
            lbl = dgLineDet.Items.Item(intCnt).FindControl("lblInitStatus")
            strInitStatus = Trim(lbl.Text)

            If Trim(strInitStatus) = "3" Then 'closed
                EdtButton.Visible = False
                DelButton.Visible = False
                CanButton.Visible = False
            End If

            TtlFPAmt += objTaxDs.Tables(0).Rows(intCnt).Item("FPAmount")
            TtlFPDPPAmt += objTaxDs.Tables(0).Rows(intCnt).Item("FPDPPAmount")
        Next

        hidTtlFPAmount.Value = TtlFPAmt
        lblTtlFPAmt.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TtlFPAmt, 2), 2)
        lblTtlFPDPPAmt.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TtlFPDPPAmt, 2), 2)
    End Sub

    Sub AddBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "TX_CLSTRX_FPENTRY_UPDATE"
        Dim strOpCd_UpdLn As String = "TX_CLSTRX_FPENTRYLN_UPDATE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim strTrxID As String = IIf(hidTrxID.Value = "", "", hidTrxID.Value)
        Dim strDocDate As String = Date_Validation(strDate, False)
        Dim strFPDate As String = Date_Validation(txtFPDate.Text, False)

        If txtFPNo.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input Faktur Pajak No."
            Exit Sub
        End If

        If txtFPAmount.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input Faktur Pajak Amount"
            Exit Sub
        End If

        If txtFPDPPAmount.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input Faktur Pajak DPP Amount"
            Exit Sub
        End If

        'toleransi 100000 rupiah sementara
        If (CDbl(hidTtlFPAmount.Value) - CDbl(hidFPAmount.Value) + CDbl(txtFPAmount.Text)) > CDbl(hidTtlDocAmount.Value) + CDbl(100000) Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Faktur Pajak Amount exceeded"
            Exit Sub
        End If

        If hidTrxID.Value = "" Then
            strParamName = "TRXID|LOCCODE|DOCID|DOCDATE|ACCMONTH|ACCYEAR|SUPPLIERCODE|SUPPLIERNAME|ACCCODE|DESCRIPTION|DOCAMOUNT|STATUS|UPDATEID|DOCLNID"
            strParamValue = hidTrxID.Value & "|" & _
                            strLocation & "|" & _
                            strDocID & "|" & _
                            strDocDate & "|" & _
                            strAccMonth & "|" & _
                            strAccYear & "|" & _
                            txtSplCode.Text & "|" & _
                            txtFromTo.Text & "|" & _
                            strAccCode & "|" & _
                            Replace(Trim(txtDescr.Value), "'", "''") & "|" & _
                            hidTtlDocAmount.Value & "|" & _
                            objCBTrx.EnumTaxStatus.Unverified & "|" & _
                            Trim(strUserId) & "|" & _
                            strDocLnID

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Upd, strParamName, strParamValue, objTaxDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objTaxDs.Tables(0).Rows.Count > 0 Then
                strTrxID = Trim(objTaxDs.Tables(0).Rows(0).Item("TrxID"))
                hidTrxID.Value = strTrxID
            End If
        End If

        If hidTrxID.Value = "" Then
            Exit Sub
        Else
            'field FPAccMonth/FPAccYear adalah Periode dari Tanggal FP.
            'field PostAccMonth/PostAccYear adalah Periode Posting PPN Masukan.
            strFPAccYear = Year(strFPDate)
            strFPAccMonth = Month(strFPDate)

            strParamName = "TRXID|TRXLNID|DOCID|FPNO|FPDATE|FPAMOUNT|FPDPPAMOUNT|ADDNOTE|DOCDATE|ACCMONTH|ACCYEAR|SUPPLIERCODE|SUPPLIERNAME|ACCCODE|DESCRIPTION|DOCAMOUNT|STATUS|UPDATEID|DOCLNID|FPACCMONTH|FPACCYEAR"
            strParamValue = hidTrxID.Value & "|" & _
                            Trim(hidTrxLnID.Value) & "|" & _
                            strDocID & "|" & _
                            txtFPNo.Text & "|" & _
                            strFPDate & "|" & _
                            txtFPAmount.Text & "|" & _
                            txtFPDPPAmount.Text & "|" & _
                            Trim(txtAddNote.Text) & "|" & _
                            strDocDate & "|" & _
                            strAccMonth & "|" & _
                            strAccYear & "|" & _
                            txtSplCode.Text & "|" & _
                            txtFromTo.Text & "|" & _
                            strAccCode & "|" & _
                            Replace(Trim(txtDescr.Value), "'", "''") & "|" & _
                            hidTtlDocAmount.Value & "|" & _
                            objCBTrx.EnumTaxStatus.Unverified & "|" & _
                            Trim(strUserId) & "|" & _
                            strDocLnID & "|" & _
                            strFPAccMonth & "|" & _
                            strFPAccYear

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd_UpdLn, strParamName, strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        End If

        txtFPNo.Text = ""
        txtFPDate.Text = ""
        txtFPAmount.Text = "0"
        txtFPDPPAmount.Text = "0"
        txtAddNote.Text = ""
        hidTrxLnID.Value = ""
        hidTrxID.Value = strTrxID
        onLoad_Display(hidTrxID.Value)
        onLoad_DisplayLn(hidTrxID.Value)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INVOICERCV_GET_CONFIG&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_polist.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                            pv_strInputDate, _
                                            strAcceptDateFormat, _
                                            objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs) Handles dgLineDet.DeleteCommand
        Dim strOpCode_DelLine As String = "TX_CLSTRX_FPENTRYLN_DELETE"
        Dim lbl As Label
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lbl = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTrxLnID")
        hidTrxLnID.Value = lbl.Text

        strParamName = "TRXID|TRXLNID|UPDATEID"
        strParamValue = Trim(strTrxID) & "|" & Trim(hidTrxLnID.Value) & "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_DelLine, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INVOICERECEIVE_DEL_LINE&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvnotelist.aspx")
        End Try

        hidTrxLnID.Value = ""
        hidFPAmount.Value = 0
        onLoad_Display(txtDocId.Text.Trim)
        onLoad_DisplayLn(txtDocId.Text.Trim)
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim btn As LinkButton

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblTrxLnID")
        hidTrxLnID.Value = lbl.Text.Trim
        lbl = E.Item.FindControl("lblFPNo")
        txtFPNo.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblFPDate")
        txtFPDate.Text = Date_Validation(lbl.Text.Trim, True)
        lbl = E.Item.FindControl("lblFPAmount")
        txtFPAmount.Text = lbl.Text.Trim
        hidFPAmount.Value = txtFPAmount.Text
        lbl = E.Item.FindControl("lblFPDPPAmount")
        txtFPDPPAmount.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblFPNote")
        txtAddNote.Text = lbl.Text.Trim

        btn = E.Item.FindControl("lbDelete")
        btn.Visible = False
        btn = E.Item.FindControl("lbEdit")
        btn.Visible = False
        btn = E.Item.FindControl("lbCancel")
        btn.Visible = True
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        txtFPNo.Text = ""
        txtFPDate.Text = ""
        txtFPAmount.Text = "0"
        txtFPDPPAmount.Text = "0"
        txtAddNote.Text = ""
        hidTrxLnID.Value = ""
        hidFPAmount.Value = 0
        onLoad_DisplayLn(txtDocId.Text.Trim)
    End Sub

    Sub VerifiedBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "TX_CLSTRX_FPENTRY_UPDATE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim strTrxID As String = IIf(hidTrxID.Value = "", "", hidTrxID.Value)
        Dim strDocDate As String = Date_Validation(strDate, False)
        Dim strFPDate As String = Date_Validation(txtFPDate.Text, False)

        If hidTrxID.Value <> "" And hidTtlFPAmount.Value <> 0 Then
            strParamName = "TRXID|LOCCODE|DOCID|DOCDATE|ACCMONTH|ACCYEAR|SUPPLIERCODE|SUPPLIERNAME|ACCCODE|DESCRIPTION|DOCAMOUNT|STATUS|UPDATEID|DOCLNID"
            strParamValue = hidTrxID.Value & "|" & _
                            strLocation & "|" & _
                            strDocID & "|" & _
                            strDocDate & "|" & _
                            strAccMonth & "|" & _
                            strAccYear & "|" & _
                            txtSplCode.Text & "|" & _
                            txtFromTo.Text & "|" & _
                            strAccCode & "|" & _
                            Replace(Trim(txtDescr.Value), "'", "''") & "|" & _
                            hidTtlDocAmount.Value & "|" & _
                            objCBTrx.EnumTaxStatus.Verified & "|" & _
                            Trim(strUserId) & "|" & _
                            strDocLnID

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Upd, strParamName, strParamValue, objTaxDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objTaxDs.Tables(0).Rows.Count > 0 Then
                strTrxID = Trim(objTaxDs.Tables(0).Rows(0).Item("TrxID"))
                hidTrxID.Value = strTrxID
            End If
        End If
    End Sub

    Sub CancelBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "TX_CLSTRX_FPENTRY_UPDATE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim strTrxID As String = IIf(hidTrxID.Value = "", "", hidTrxID.Value)
        Dim strDocDate As String = Date_Validation(strDate, False)
        Dim strFPDate As String = Date_Validation(txtFPDate.Text, False)

        If hidTrxID.Value <> "" And hidTtlFPAmount.Value <> 0 Then
            strParamName = "TRXID|LOCCODE|DOCID|DOCDATE|ACCMONTH|ACCYEAR|SUPPLIERCODE|SUPPLIERNAME|ACCCODE|DESCRIPTION|DOCAMOUNT|STATUS|UPDATEID|DOCLNID"
            strParamValue = hidTrxID.Value & "|" & _
                            strLocation & "|" & _
                            strDocID & "|" & _
                            strDocDate & "|" & _
                            strAccMonth & "|" & _
                            strAccYear & "|" & _
                            txtSplCode.Text & "|" & _
                            txtFromTo.Text & "|" & _
                            strAccCode & "|" & _
                            Replace(Trim(txtDescr.Value), "'", "''") & "|" & _
                            hidTtlDocAmount.Value & "|" & _
                            objCBTrx.EnumTaxStatus.Unverified & "|" & _
                            Trim(strUserId) & "|" & _
                            strDocLnID

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Upd, strParamName, strParamValue, objTaxDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objTaxDs.Tables(0).Rows.Count > 0 Then
                strTrxID = Trim(objTaxDs.Tables(0).Rows(0).Item("TrxID"))
                hidTrxID.Value = strTrxID
            End If
        End If
    End Sub
End Class
