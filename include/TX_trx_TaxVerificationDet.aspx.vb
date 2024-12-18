

Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class TX_trx_TaxVerificationDet : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents txtSplCode As TextBox
    Protected WithEvents txtSplName As TextBox
    Protected WithEvents txtSplNPWP As TextBox
    Protected WithEvents txtSplAddress As HtmlTextArea
    Protected WithEvents txtFromTo As TextBox
    Protected WithEvents txtDocId As TextBox
    Protected WithEvents txtTrxID As TextBox
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents lblTaxStatus As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents ddlKPP As DropDownList
    Protected WithEvents ddlTaxObjectGrp As DropDownList
    Protected WithEvents VerifiedBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents hidOriDoc As HtmlInputHidden
    Protected WithEvents lblTtlDPPAmt As Label
    Protected WithEvents lblTtlTaxAmt As Label

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Protected objCBTrx As New agri.CB.clsTrx()

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCapDs As New DataSet()
    Dim objRptDs As New DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim intConfig As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intDocType As Integer

    Dim strAccCode As String
    Dim strTrxID As String
    Dim strDocID As String
    Dim strDocLnID As String
    Dim strDocDate As String
    Dim strTaxID As String
    Dim strTaxInit As String
    Dim strTaxStatus As String
    Dim strSupplierCode As String
    Dim strSupplierName As String
    Dim strDPPAmount As String
    Dim strTaxAmount As String
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

        VerifiedBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(VerifiedBtn).ToString())
        CancelBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(CancelBtn).ToString())

        lblErrMessage.Visible = False
        BindCompKPP("")
        BindAccCodeDropList("")

        If Request.QueryString("docid") = "" Then
        Else
            strDocID = Request.QueryString("DocID")
            strDocLnID = Request.QueryString("DocLnID")
            strTaxInit = Request.QueryString("TaxInit")
            strTaxStatus = Request.QueryString("TaxStatus")
            strAccMonth = Request.QueryString("AccMonth")
            strAccYear = Request.QueryString("AccYear")
            onLoad_Display(strDocID)
        End If
    End Sub

    Sub close_window()
        Response.Write("<Script Language=""JavaScript"">window.close();</Script>")
    End Sub

    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")
        Dim strOpCd As String = "TX_CLSSETUP_TAXOBJECTRATE_GROUP_GET"
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet

        strParamName = "STRSEARCH"
        strParamValue = "WHERE AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & strLocation & "')"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            If Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = ObjTaxDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Please select tax group"
        ObjTaxDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTaxObjectGrp.DataSource = ObjTaxDs.Tables(0)
        ddlTaxObjectGrp.DataValueField = "AccCode"
        ddlTaxObjectGrp.DataTextField = "Description"
        ddlTaxObjectGrp.DataBind()
        ddlTaxObjectGrp.SelectedIndex = intSelectedIndex

        If Not ObjTaxDs Is Nothing Then
            ObjTaxDs = Nothing
        End If
    End Sub

    Sub BindCompKPP(Optional ByVal pv_strKPP As String = "")
        Dim strOpCd As String = "TX_CLSSETUP_COMPTAX_GET"
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet
        Dim intCnt As Integer = 0

        strParamName = "COMPCODE"
        strParamValue = strCompany

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            If Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("KPPInit")) = Trim(pv_strKPP) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = ObjTaxDs.Tables(0).NewRow()
        dr("KPPInit") = ""
        dr("KPPDescr") = "Please select KPP location"
        ObjTaxDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlKPP.DataSource = ObjTaxDs.Tables(0)
        ddlKPP.DataValueField = "KPPInit"
        ddlKPP.DataTextField = "KPPDescr"
        ddlKPP.DataBind()
        ddlKPP.SelectedIndex = intSelectedIndex

        If Not ObjTaxDs Is Nothing Then
            ObjTaxDs = Nothing
        End If
    End Sub

    Sub onLoad_Display(ByVal pv_strDocID As String)
        Dim strOpCd As String = "TX_CLSTRX_TAXVERIFICATION_DETAIL_GET"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim ObjTaxDs As DataSet
        Dim strSearch As String
        Dim strOrderBy As String
        Dim intCnt As Integer = 0
        Dim TtlDPPAmt As Double
        Dim TtlTaxAmt As Double

        If Left(Trim(pv_strDocID), 3) = "XXX" Then
            strSearch = " AND A.DocID*=C.DocID AND A.DocID = '" & Trim(pv_strDocID) & "'"
        Else
            If strTaxInit = "22" Then
                If strTaxStatus = "1" Then
                    strSearch = " AND A.DocID*=C.DocID AND A.SUPPLIERCODE IN (SELECT SupplierCode FROM #TAXLIST WHERE DocID = '" & Trim(pv_strDocID) & "') AND A.DocID = '" & Trim(pv_strDocID) & "' "
                Else
                    strSearch = " AND A.DocID*=C.DocID AND A.SUPPLIERCODE IN (SELECT SupplierCode FROM #TAXLIST WHERE DocID = '" & Trim(pv_strDocID) & "')"
                End If
            Else
                If strTaxStatus = "1" Then
                    strSearch = " AND A.DocID*=C.DocID AND A.DocID = '" & Trim(pv_strDocID) & "' "
                Else
                    strSearch = " AND A.DocID*=C.DocID AND A.DocID = '" & Trim(pv_strDocID) & "'"
                End If

            End If
            CancelBtn.Visible = False
        End If
        strOrderBy = " ORDER By A.DocLnID Asc"

        strParamName = "LOCCODE|ACCYEAR|ACCMONTH|STRSEARCH|ORDERBY"
        strParamValue = strLocation & "|" & _
                        strAccYear & "|" & _
                        strAccMonth & "|" & _
                        strSearch & "|"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            If Left(Trim(pv_strDocID), 3) = "XXX" Then
                txtSplCode.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SupplierCode"))
                txtFromTo.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("FromTo"))
                txtSplName.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SplName"))
                txtSplNPWP.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SplNPWP"))
                txtSplAddress.Value = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SplAddress"))
                txtDocId.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("DocID"))
                txtTrxID.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("TrxID"))
                txtRemark.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("AdditionalNote"))
                BindCompKPP(Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("KPPInit")))
                BindAccCodeDropList(Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("AccCode")))

                lblTaxStatus.Text = objCBTrx.mtdGetTaxStatus(Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxStatus")))
                lblLastUpdate.Text = objGlobal.GetLongDate(ObjTaxDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
                lblUpdatedBy.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("UserName"))
                hidOriDoc.Value = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("OriDoc"))
            Else
                If Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("TrxID")) <> "" Then
                    txtSplCode.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SupplierCode"))
                    txtFromTo.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("FromTo"))
                    txtSplName.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SplName"))
                    txtSplNPWP.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SplNPWP"))
                    txtSplAddress.Value = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SplAddress"))
                    txtDocId.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("DocID"))
                    txtTrxID.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("TrxID"))
                    txtRemark.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("AdditionalNote"))
                    BindCompKPP(Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("KPPInit")))
                    BindAccCodeDropList(Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("AccCode")))

                    lblTaxStatus.Text = objCBTrx.mtdGetTaxStatus(Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxStatus")))
                    lblLastUpdate.Text = objGlobal.GetLongDate(ObjTaxDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
                    lblUpdatedBy.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("UserName"))
                    hidOriDoc.Value = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("OriDoc"))
                Else
                    txtSplCode.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SupplierCode"))
                    txtFromTo.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("FromTo"))
                    txtSplName.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SplName"))
                    txtSplNPWP.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SplNPWP"))
                    txtSplAddress.Value = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SplAddress"))
                    txtDocId.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("DocID"))
                    txtTrxID.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("TrxID"))
                    txtRemark.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("AdditionalNote"))
                    BindCompKPP(Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("KPPInit")))
                    BindAccCodeDropList(Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("AccCode")))

                    lblTaxStatus.Text = objCBTrx.mtdGetTaxStatus(Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxStatus")))
                    lblLastUpdate.Text = objGlobal.GetLongDate(ObjTaxDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
                    lblUpdatedBy.Text = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("UserName"))
                    hidOriDoc.Value = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("OriDoc"))
                End If
            End If

            TtlDPPAmt += ObjTaxDs.Tables(0).Rows(intCnt).Item("DPPAmount")
            TtlTaxAmt += ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxAmount")
        Next

        lblTtlDPPAmt.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TtlDPPAmt, 2), 2)
        lblTtlTaxAmt.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TtlTaxAmt, 2), 2)

        dgLineDet.DataSource = ObjTaxDs.Tables(0)
        dgLineDet.DataBind()
    End Sub

    Sub VerifiedBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCd As String = "TX_CLSTRX_TAXVERIFICATION_DETAIL_UPDATE"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strKPPInitFind As String = Trim(Request.Form("ddlKPP"))
		Dim strRemark As String = Trim(Request.Form("txtRemark"))

        BindCompKPP(strKPPInitFind)
        If ddlKPP.SelectedItem.Value = "" Then
            lblErrMessage.Text = "Please select KPP location."
            lblErrMessage.Visible = True
            Exit Sub
        End If

        strParamName = "LOCCODE|ORIDOC|DOCID|TRXID|TAXSTATUS|KPPINIT|ADDITIONALNOTE|UPDATEID"
        strParamValue = strLocation & "|" & _
                        UCase(Trim(hidOriDoc.Value)) & "|" & _
                        Trim(txtDocId.Text) & "|" & _
                        Trim(txtTrxID.Text) & "|" & _
                        objCBTrx.EnumTaxStatus.Verified & "|" & _
                        Trim(ddlKPP.SelectedItem.Value) & "|" & _
                        Trim(strRemark) & "|" & _
                        strUserId

       Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/TX_Setup_TaxObjectList.aspx")
        End Try

        onLoad_Display(Trim(txtDocId.Text))
    End Sub

    Sub CancelBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCd As String = "TX_CLSTRX_TAXVERIFICATION_DETAIL_UPDATE"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strKPPInitFind As String = Trim(Request.Form("ddlKPP"))
		Dim strRemark As String = Trim(Request.Form("txtRemark"))
		
        BindCompKPP(strKPPInitFind)
        If ddlKPP.SelectedItem.Value = "" Then
            lblErrMessage.Text = "Please select KPP location."
            lblErrMessage.Visible = True
            Exit Sub
        End If

        strParamName = "LOCCODE|ORIDOC|DOCID|TRXID|TAXSTATUS|KPPINIT|ADDITIONALNOTE|UPDATEID"
        strParamValue = strLocation & "|" & _
                        UCase(Trim(hidOriDoc.Value)) & "|" & _
                        Trim(txtDocId.Text) & "|" & _
                        Trim(txtTrxID.Text) & "|" & _
                        objCBTrx.EnumTaxStatus.Unverified & "|" & _
                        Trim(ddlKPP.SelectedItem.Value) & "|" & _
                        Trim(strRemark) & "|" & _
                        strUserId

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/TX_Setup_TaxObjectList.aspx")
        End Try

        onLoad_Display(Trim(txtDocId.Text))
    End Sub
End Class
