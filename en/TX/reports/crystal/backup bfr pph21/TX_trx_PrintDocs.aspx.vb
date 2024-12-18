Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class TX_trx_PrintDocs : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents txtTrxId As TextBox
    Protected WithEvents txtSupplier As TextBox
    Protected WithEvents txtToId As TextBox
    Protected WithEvents ddlPrintOpt As DropDownList
    Protected WithEvents PrintDoc As ImageButton
    Protected WithEvents txtQtySSP1 As TextBox
    Protected WithEvents txtQtySSP2 As TextBox
    Protected WithEvents txtQtySSP3 As TextBox
    Protected WithEvents txtBuktiPtg As TextBox
    Protected WithEvents txtTransDate As TextBox
    Protected WithEvents lblErrTransDate As Label
    Protected WithEvents lblFmtTransDate As Label
    Protected WithEvents ddlTaxObjectGrp As DropDownList
    Protected WithEvents txtSPTRev As TextBox
    Protected WithEvents intRec As HtmlInputHidden
    Protected WithEvents ddlPelapor As DropDownList
    Protected WithEvents txtPelapor As TextBox
    Protected WithEvents ddlKPP As DropDownList
    Protected WithEvents cbExcel As CheckBox
    Protected WithEvents RowSSP As HtmlTableRow
    Protected WithEvents RowSSPLembar As HtmlTableRow
    Protected WithEvents ddlSSPAkunPjk As DropDownList
    Protected WithEvents ddlSSPJnsStr As DropDownList
    Protected WithEvents ddlSSPLembar As DropDownList
    Protected WithEvents hidTaxInit As HtmlInputHidden
    Protected WithEvents hidKPPInit As HtmlInputHidden

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()

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
    Dim strDocDate As String
    Dim strTaxID As String
    Dim strTaxInit As String
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

        lblErrMessage.Visible = False

        If Request.QueryString("docid") = "" Then
        Else
            strTrxID = Request.QueryString("TrxID")
            strDocID = Request.QueryString("DocID")
            strAccCode = Request.QueryString("AccCode")
            strDocDate = Request.QueryString("DocDate")
            strTaxID = Request.QueryString("TaxID")
            strTaxInit = Request.QueryString("TaxInit")
            strSupplierCode = Request.QueryString("SupplierCode")
            strSupplierName = Request.QueryString("SupplierName")
            strDPPAmount = Request.QueryString("DPPAmount")
            strTaxAmount = Request.QueryString("TaxAmount")

            txtTrxId.Text = IIf(strTaxInit = "22", "", strTrxID)
            txtSupplier.Text = Request.QueryString("SupplierName")
        End If

        strAccMonth = Request.QueryString("AccMonth")
        strAccYear = Request.QueryString("AccYear")
        txtTransDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)

        BindAccCodeDropList(strTaxInit)
        BindCompKPP("")
    End Sub

    Sub close_window()
        Response.Write("<Script Language=""JavaScript"">window.close();</Script>")
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDate As String = Date_Validation(txtTransDate.Text, False)
        Dim strTaxInitFind As String = Trim(Request.Form("ddlTaxObjectGrp"))
        Dim strKPPInitFind As String = Trim(Request.Form("ddlKPP"))
        Dim strExportToExcel As String
        Dim strAkunPjk As String
        Dim strJnsSetoran As String
        Dim strUraianBayar As String
        Dim strLembar As String
        Dim strLembarInt As String

        If ddlPrintOpt.SelectedItem.Value <> "5" Then
            hidTaxInit.Value = strTaxInitFind
            hidKPPInit.Value = strKPPInitFind
        End If
        

        'BindAccCodeDropList(strTaxInitFind)
        'BindCompKPP(strKPPInitFind)
        If ddlPrintOpt.SelectedItem.Value <> "4" Then
            If ddlTaxObjectGrp.SelectedItem.Value = "0" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Please select tax object group."
                Exit Sub
            End If
            If ddlPelapor.SelectedItem.Value = "0" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Please select Pemotong/Kuasa Wajib Pajak."
                Exit Sub
            End If
        End If
        If ddlPrintOpt.SelectedItem.Value = "0" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select print option."
            Exit Sub
        End If

        If ddlPrintOpt.SelectedItem.Value = "1" Or ddlPrintOpt.SelectedItem.Value = "2" Or ddlPrintOpt.SelectedItem.Value = "5" Then
            If hidKPPInit.Value = "0" Or hidKPPInit.Value = "" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Please select KPP Location."
                Exit Sub
            End If
        End If

        If ddlPrintOpt.SelectedItem.Value = "5" Then
            Dim arrParam1 As Array
            Dim arrParam2 As Array

            If ddlSSPJnsStr.SelectedItem.Value = "0" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Please select kode akun pajak - jenis setoran"
                Exit Sub
            End If
            If ddlSSPLembar.SelectedItem.Value = "0" Then
                lblErrMessage.Visible = True
                lblErrMessage.Text = "Please select lembar ke-"
                Exit Sub
            End If

            arrParam1 = Split(ddlSSPLembar.SelectedItem.Text, ".")
            arrParam2 = Split(ddlSSPJnsStr.SelectedItem.Text, "-")

            strAkunPjk = ddlSSPAkunPjk.SelectedItem.Value
            strJnsSetoran = ddlSSPJnsStr.SelectedItem.Value
            strUraianBayar = Trim(arrParam2(1))
            strLembar = Trim(arrParam1(1))
            strLembarInt = ddlSSPLembar.SelectedItem.Value
        Else
            strAkunPjk = ""
            strJnsSetoran = ""
            strUraianBayar = ""
            strLembar = ""
            strLembarInt = ""
        End If

        'strTaxInit = ddlTaxObjectGrp.SelectedItem.Value

        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/TX_Rpt_TaxReports.aspx?strTrxID=" & strTrxID & _
                "&strDocID=" & Trim(strDocID) & _
                "&strSupplierCode=" & Trim(strSupplierCode) & _
                "&strCreateDate=" & strDate & _
                "&strAccMonth=" & strAccMonth & _
                "&strAccYear=" & strAccYear & _
                "&strPrintOpt=" & ddlPrintOpt.SelectedItem.Value & _
                "&strAccCode=" & Trim(strAccCode) & _
                "&strSPTRev=" & Trim(txtSPTRev.Text) & _
                "&strQtySPP1=" & Trim(txtQtySSP1.Text) & _
                "&strQtySPP2=" & Trim(txtQtySSP2.Text) & _
                "&strQtySPP3=" & Trim(txtQtySSP3.Text) & _
                "&strQtyBuktiPtg=" & Trim(txtBuktiPtg.Text) & _
                "&strCompNPWPNo=" & "" & _
                "&strCompNPWPLoc=" & "" & _
                "&strTaxInit=" & hidTaxInit.Value & _
                "&strPelapor=" & Trim(ddlPelapor.SelectedItem.Text) & _
                "&strPelaporNPWP=" & Trim(ddlPelapor.SelectedItem.Value) & _
                "&strPelapor2=" & Trim(txtPelapor.Text) & _
                "&strKPPInit=" & hidKPPInit.Value & _
                "&ExportToExcel=" & strExportToExcel & _
                "&strAkunPjk=" & strAkunPjk & _
                "&strJnsSetoran=" & strJnsSetoran & _
                "&strUraianBayar=" & strUraianBayar & _
                "&strLembar=" & strLembar & _
                "&strLembarInt=" & strLembarInt & _
                """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")
        Dim strOpCd As String = "TX_CLSSETUP_TAXOBJECTRATE_GROUP_GET"
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet
        Dim intCnt As Integer = 0

        strParamName = "STRSEARCH"
        If pv_strAccCode = "" Then
            strParamValue = "WHERE AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & strLocation & "')"
        Else
            strParamValue = "WHERE AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & strLocation & "') AND TaxInit = '" & pv_strAccCode & "'"
        End If

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            If Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxInit")) = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = ObjTaxDs.Tables(0).NewRow()
        dr("TaxInit") = ""
        dr("Description") = "Please select tax group"
        ObjTaxDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTaxObjectGrp.DataSource = ObjTaxDs.Tables(0)
        ddlTaxObjectGrp.DataValueField = "TaxInit"
        ddlTaxObjectGrp.DataTextField = "Description"
        ddlTaxObjectGrp.DataBind()
        ddlTaxObjectGrp.SelectedIndex = intSelectedIndex

        If Not ObjTaxDs Is Nothing Then
            ObjTaxDs = Nothing
        End If
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

    Sub PrintOpt_Changed(ByVal Sender As System.Object, ByVal E As System.EventArgs)
        If ddlPrintOpt.SelectedItem.Value = "5" Then
            RowSSP.Visible = True
            RowSSPLembar.Visible = True
            BindTaxAccCode()
        Else
            RowSSP.Visible = False
            RowSSPLembar.Visible = False
        End If
    End Sub

    Sub SSPAkunPjk_Changed(ByVal Sender As System.Object, ByVal E As System.EventArgs)
        If ddlPrintOpt.SelectedItem.Value = "5" Then
            Dim strTaxInitFind As String = Trim(Request.Form("ddlTaxObjectGrp"))
            Dim strKPPInitFind As String = Trim(Request.Form("ddlKPP"))

            hidTaxInit.Value = strTaxInitFind
            hidKPPInit.Value = strKPPInitFind
            BindSSPType(ddlSSPAkunPjk.SelectedItem.Value)
        End If
    End Sub

    Sub BindTaxAccCode()
        Dim strOpCd As String = "TX_CLSSETUP_SSP_TAXACC_GET"
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet
        Dim intCnt As Integer = 0

        strParamName = ""
        strParamValue = ""

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxAccCode") = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxAccCode"))
            ObjTaxDs.Tables(0).Rows(intCnt).Item("Description") = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxAccCode")) & " - " & _
                                                                            Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("Description"))
        Next intCnt

        dr = ObjTaxDs.Tables(0).NewRow()
        dr("TaxAccCode") = ""
        dr("Description") = "Please select kode akun pajak"
        ObjTaxDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSSPAkunPjk.DataSource = ObjTaxDs.Tables(0)
        ddlSSPAkunPjk.DataValueField = "TaxAccCode"
        ddlSSPAkunPjk.DataTextField = "Description"
        ddlSSPAkunPjk.DataBind()
        ddlSSPAkunPjk.SelectedIndex = intSelectedIndex

        If Not ObjTaxDs Is Nothing Then
            ObjTaxDs = Nothing
        End If
    End Sub

    Sub BindSSPType(Optional ByVal pv_strKPP As String = "")
        Dim strOpCd As String = "TX_CLSSETUP_SSP_TAXACC_LINE_GET"
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet
        Dim intCnt As Integer = 0

        strParamName = "TAXACCCODE"
        strParamValue = Trim(ddlSSPAkunPjk.SelectedItem.Value)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            ObjTaxDs.Tables(0).Rows(intCnt).Item("SSPType") = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SSPType"))
            ObjTaxDs.Tables(0).Rows(intCnt).Item("Description") = Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("SSPType")) & " - " & _
                                                                            Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("Description"))
        Next intCnt

        dr = ObjTaxDs.Tables(0).NewRow()
        dr("SSPType") = ""
        dr("Description") = "Please select jenis setoran"
        ObjTaxDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSSPJnsStr.DataSource = ObjTaxDs.Tables(0)
        ddlSSPJnsStr.DataValueField = "SSPType"
        ddlSSPJnsStr.DataTextField = "Description"
        ddlSSPJnsStr.DataBind()
        ddlSSPJnsStr.SelectedIndex = intSelectedIndex

        If Not ObjTaxDs Is Nothing Then
            ObjTaxDs = Nothing
        End If
    End Sub
End Class
