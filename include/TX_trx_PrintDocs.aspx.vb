Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class TX_trx_PrintDocs : Inherits Page

    Protected WithEvents LinkDownload2 As HyperLink
    Protected WithEvents LinkDownload1 As HyperLink

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
    Protected WithEvents txtPelapor As DropDownList
    Protected WithEvents ddlKPP As DropDownList
    Protected WithEvents cbExcel As CheckBox
    Protected WithEvents cbCSV As CheckBox
    Protected WithEvents RowSSP As HtmlTableRow
    Protected WithEvents RowSSPLembar As HtmlTableRow
    Protected WithEvents ddlSSPAkunPjk As DropDownList
    Protected WithEvents ddlSSPJnsStr As DropDownList
    Protected WithEvents ddlSSPLembar As DropDownList
    Protected WithEvents hidTaxInit As HtmlInputHidden
    Protected WithEvents hidKPPInit As HtmlInputHidden
    Protected WithEvents RowPPh21a As HtmlTableRow
    Protected WithEvents RowPPh21b As HtmlTableRow
    Protected WithEvents txt21TtpQty As TextBox
    Protected WithEvents txt21TtpDPPAmt As TextBox
    Protected WithEvents txt21TtpTaxAmt As TextBox
    Protected WithEvents txt21TdkTtpQty As TextBox
    Protected WithEvents txt21TdkTtpDPPAmt As TextBox
    Protected WithEvents txt21TdkTtpTaxAmt As TextBox

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objGLRpt As New agri.GL.clsReport()

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

        If Not IsPostBack Then
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
        End If
    End Sub

    Sub close_window()
        Response.Write("<Script Language=""JavaScript"">window.close();</Script>")
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDate As String = Date_Validation(txtTransDate.Text, False)
        Dim strTaxInitFind As String = Trim(Request.Form("ddlTaxObjectGrp"))
        Dim strKPPInitFind As String = Trim(Request.Form("ddlKPP"))
		Dim strTrxID As String = Trim(Request.Form("txtTrxId"))
        Dim strExportToExcel As String
        Dim strAkunPjk As String
        Dim strJnsSetoran As String
        Dim strUraianBayar As String
        Dim strLembar As String
        Dim strLembarInt As String
        Dim ObjTaxDsK As DataSet
        Dim ObjTaxDsNK As DataSet
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim strOpCd As String
        Dim objMapPath As String
        Dim objFTPFolder As String
        Dim strUrl As String

        If ddlPrintOpt.SelectedItem.Value <> "5" Then
            hidTaxInit.Value = strTaxInitFind
            hidKPPInit.Value = strKPPInitFind
        End If

        strAccMonth = Request.QueryString("AccMonth")
        strAccYear = Request.QueryString("AccYear")

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
            'If ddlSSPLembar.SelectedItem.Value = "0" Then
            '    lblErrMessage.Visible = True
            '    lblErrMessage.Text = "Please select lembar ke-"
            '    Exit Sub
            'End If

            If ddlSSPLembar.SelectedItem.Value <> "0" Then
                arrParam1 = Split(ddlSSPLembar.SelectedItem.Text, ".")
                arrParam2 = Split(ddlSSPJnsStr.SelectedItem.Text, "-")
                strUraianBayar = Trim(arrParam2(1))
                strLembar = Trim(arrParam1(1))
                strLembarInt = ddlSSPLembar.SelectedItem.Value
            Else
                arrParam2 = Split(ddlSSPJnsStr.SelectedItem.Text, "-")
                strUraianBayar = Trim(arrParam2(1))
                strLembar = ""
                strLembarInt = ddlSSPLembar.SelectedItem.Value
            End If

            strAkunPjk = ddlSSPAkunPjk.SelectedItem.Value
            strJnsSetoran = ddlSSPJnsStr.SelectedItem.Value

        Else
            strAkunPjk = ""
            strJnsSetoran = ""
            strUraianBayar = ""
            strLembar = ""
            strLembarInt = ""
        End If

        'strTaxInit = ddlTaxObjectGrp.SelectedItem.Value

        If cbCSV.Checked = False Then
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
                    "&str21TtpQty=" & Trim(txt21TtpQty.Text) & _
                    "&str21TtpDPPAmt=" & Trim(txt21TtpDPPAmt.Text) & _
                    "&str21TtpTaxAmt=" & Trim(txt21TtpTaxAmt.Text) & _
                    "&str21TdkTtpQty=" & Trim(txt21TdkTtpQty.Text) & _
                    "&str21TdkTtpDPPAmt=" & Trim(txt21TdkTtpDPPAmt.Text) & _
                    "&str21TdkTtpTaxAmt=" & Trim(txt21TdkTtpTaxAmt.Text) & _
                    """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        Else
            Dim strPeriod As String = IIf(Len(strAccMonth) = 1, "0" & Trim(strAccMonth), Trim(strAccMonth)) & Trim(strAccYear)

            Select Case hidTaxInit.Value
                Case "21"
                    'PPH21 KARYAWAN
                    strOpCd = "TX_STDRPT_GET_CSV_KARYAWAN"
                    strParamName = "ACCYEAR|ACCMONTH|LOCCODE|KPPINIT"
                    strParamValue = strAccYear & "|" & strAccMonth & "|" & strLocation & "|" & Trim(hidKPPInit.Value)

                    Try
                        intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, ObjTaxDsK, objMapPath, objFTPFolder)

                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
                    End Try

                    Try
                        intErrNo = objAdmin.mtdGetBasePath(objMapPath)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMessage.Text & "&redirect=")
                    End Try

                    strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
                    strUrl = Replace(strUrl, "\", "/")

                    Dim MyCSVFileK As String = objFTPFolder & "1721_I_BULANAN-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"
                    If My.Computer.FileSystem.FileExists(MyCSVFileK) = True Then
                        My.Computer.FileSystem.DeleteFile(MyCSVFileK)
                    End If
                    Dim dataToWriteK As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(MyCSVFileK, True)

                    dataToWriteK.WriteLine("Masa Pajak;Tahun Pajak;Pembetulan;NPWP;Nama;Kode Pajak;Jumlah Bruto;Jumlah PPh;Kode Negara") 'diganti dari , menjadi ;
                    If ObjTaxDsK.Tables(0).Rows.Count > 0 Then
                        For intCnt = 0 To ObjTaxDsK.Tables(0).Rows.Count - 1
                            dataToWriteK.WriteLine(Trim(ObjTaxDsK.Tables(0).Rows(intCnt).Item("MasaPajak")) & ";" & Trim(ObjTaxDsK.Tables(0).Rows(intCnt).Item("TahunPajak")) & ";" & _
                            Trim(ObjTaxDsK.Tables(0).Rows(intCnt).Item("Pembetulan")) & ";" & Trim(ObjTaxDsK.Tables(0).Rows(intCnt).Item("NPWP")) & ";" & _
                            Trim(ObjTaxDsK.Tables(0).Rows(intCnt).Item("Nama")) & ";" & Trim(ObjTaxDsK.Tables(0).Rows(intCnt).Item("KodePajak")) & ";" & _
                            Trim(ObjTaxDsK.Tables(0).Rows(intCnt).Item("Bruto")) & ";" & Trim(ObjTaxDsK.Tables(0).Rows(intCnt).Item("PPH")) & ";" & _
                            Trim(ObjTaxDsK.Tables(0).Rows(intCnt).Item("KodeNegara")))
                        Next

                        'lblErrMessage.Visible = True
                        'lblErrMessage.Text = "PPH 21 File created in " & Trim(MyCSVFileK)

                        LinkDownload1.Visible = True
                        LinkDownload1.Text = "Download file 1721_I_BULANAN-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"
                        LinkDownload1.NavigateUrl = "../../../" & strUrl & "1721_I_BULANAN-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"
                    End If
                    dataToWriteK.Close()


                    'PPH21 NON KARYAWAN
                    strOpCd = "TX_STDRPT_GET_CSV_NONKARYAWAN"
                    strParamName = "ACCYEAR|ACCMONTH|LOCCODE|KPPINIT|NMPTG|NPWPPTG"
                    strParamValue = strAccYear & "|" & strAccMonth & "|" & strLocation & "|" & Trim(hidKPPInit.Value) & "|" & _
                                    Trim(ddlPelapor.SelectedItem.Text) & "|" & Trim(ddlPelapor.SelectedItem.Value)

                    Try
                        intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, ObjTaxDsNK, objMapPath, objFTPFolder)

                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
                    End Try

                    strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
                    strUrl = Replace(strUrl, "\", "/")

                    Dim MyCSVFileNK As String = objFTPFolder & "1721_BP_TIDAK_FINAL-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"
                    If My.Computer.FileSystem.FileExists(MyCSVFileNK) = True Then
                        My.Computer.FileSystem.DeleteFile(MyCSVFileNK)
                    End If
                    Dim dataToWriteNK As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(MyCSVFileNK, True)

                    dataToWriteNK.WriteLine("Masa Pajak;Tahun Pajak;Pembetulan;Nomor Bukti Potong;NPWP;NIK;Nama;Alamat;WP Luar Negeri;Kode Negara;Kode Pajak;Jumlah Bruto;Jumlah DPP;Tanpa NPWP;Tarif;Jumlah PPh;NPWP Pemotong;Nama Pemotong;Tanggal Bukti Potong") 'diganti dari , menjadi ;
                    If ObjTaxDsNK.Tables(0).Rows.Count > 0 Then
                        For intCnt = 0 To ObjTaxDsNK.Tables(0).Rows.Count - 1
                            dataToWriteNK.WriteLine(Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("MasaPajak")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("TahunPajak")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Pembetulan")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("NomorPotong")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("NPWP")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("NIK")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Nama")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Alamat")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("WPLuarNegeri")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("KodeNegara")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("KodePajak")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Bruto")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("DPP")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("TanpaNPWP")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tarif")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("PPH")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("NPWPPemotong")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("NamaPemotong")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("TglPotong")))
                        Next

                        'lblErrMessage.Visible = True
                        'lblErrMessage.Text = lblErrMessage.Text & "<br>" & "PPH 21 File created in " & Trim(MyCSVFileNK)
                        'mark 1

                        LinkDownload2.Visible = True
                        LinkDownload2.Text = "Download file 1721_BP_TIDAK_FINAL-!" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"
                        LinkDownload2.NavigateUrl = "../../../" & strUrl & "1721_BP_TIDAK_FINAL-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"

                    End If
                    dataToWriteNK.Close()

                Case "22"
                    strOpCd = "TX_STDRPT_GET_CSV_PPH22"
                    strParamName = "ACCYEAR|ACCMONTH|LOCCODE|KPPINIT|NMPTG|NPWPPTG|COMPCODE|TAXINIT|STRSEARCH"
                    strParamValue = strAccYear & "|" & strAccMonth & "|" & strLocation & "|" & Trim(hidKPPInit.Value) & "|" & _
                                    Trim(ddlPelapor.SelectedItem.Text) & "|" & Trim(ddlPelapor.SelectedItem.Value) & "|" & strCompany & "|" & hidTaxInit.Value & "|"

                    Try
                        intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, ObjTaxDsNK, objMapPath, objFTPFolder)

                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
                    End Try

                    strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
                    strUrl = Replace(strUrl, "\", "/")

                    Dim MyCSVFileNK As String = objFTPFolder & "PPH22-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"
                    If My.Computer.FileSystem.FileExists(MyCSVFileNK) = True Then
                        My.Computer.FileSystem.DeleteFile(MyCSVFileNK)
                    End If
                    Dim dataToWriteNK As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(MyCSVFileNK, True)

                    'dataToWriteNK.WriteLine("Kode Form Bukti Potong;Masa Pajak;Tahun Pajak;Pembetulan;NPWP WP yang Dipotong;Nama WP yang Dipotong;Alamat WP yang Dipotong;Nomor Bukti Potong;Tanggal Bukti Potong;Nilai Bruto 1;Tarif 1;PPh Yang Dipotong  1;Nilai Bruto 2;Tarif 2;PPh Yang Dipotong  2;Nilai Bruto 3;Tarif 3;PPh Yang Dipotong  3;Nilai Bruto 4;Tarif 4;PPh Yang Dipotong  4;Nilai Bruto 5;Tarif 5;PPh Yang Dipotong  5;Nilai Bruto 6a/Nilai Bruto 6;Tarif 6a/Tarif 6;PPh Yang Dipotong  6a/PPh Yang Dipotong  6;Nilai Bruto 6b/Nilai Bruto 7;Tarif 6b/Tarif 7;PPh Yang Dipotong  6b/PPh Yang Dipotong  7;Nilai Bruto 6c/Nilai Bruto 8;Tarif 6c/Tarif 8;PPh Yang Dipotong  6c/PPh Yang Dipotong  8;Nilai Bruto 9;Tarif 9;PPh Yang Dipotong  9;Nilai Bruto 10;Perkiraan Penghasilan Netto10;Tarif 10;PPh Yang Dipotong  10;Nilai Bruto 11;Perkiraan Penghasilan Netto11;Tarif 11;PPh Yang Dipotong  11;Nilai Bruto 12;Perkiraan Penghasilan Netto12;Tarif 12;PPh Yang Dipotong  12;Nilai Bruto 13;Tarif 13;PPh Yang Dipotong  13;Kode Jasa 6d1 PMK-244/PMK.03/2008;Nilai Bruto 6d1;Tarif 6d1;PPh Yang Dipotong  6d1;Kode Jasa 6d2 PMK-244/PMK.03/2008;Nilai Bruto 6d2;Tarif 6d2;PPh Yang Dipotong  6d2;Kode Jasa 6d3 PMK-244/PMK.03/2008;Nilai Bruto 6d3;Tarif 6d3;PPh Yang Dipotong  6d3;Kode Jasa 6d4 PMK-244/PMK.03/2008;Nilai Bruto 6d4;Tarif 6d4;PPh Yang Dipotong  6d4;Kode Jasa 6d5 PMK-244/PMK.03/2008;Nilai Bruto 6d5;Tarif 6d5;PPh Yang Dipotong  6d5;Kode Jasa 6d6 PMK-244/PMK.03/2008;Nilai Bruto 6d6;Tarif 6d6;PPh Yang Dipotong  6d6;Jumlah Nilai Bruto ;Jumlah PPh Yang Dipotong")
                    If ObjTaxDsNK.Tables(0).Rows.Count > 0 Then
                        For intCnt = 0 To ObjTaxDsNK.Tables(0).Rows.Count - 1
                            dataToWriteNK.WriteLine(Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("FormNo")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("AccMonth")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("AccYear")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Pembetulan")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("splNPWP")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("splName")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("splAddress")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("TrxID")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("DocDate")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row1DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row1Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row1TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row2DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row2Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row2TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row3DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row3Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row3TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row4DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row4Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row4TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row5TaxObj")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row5DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row5Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row5TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row6TaxObj")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row6DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row6Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row6TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row7TaxObj")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row7DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row7Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row7TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row8TaxObj")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row8DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row8Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row8TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row9TaxObj")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row9DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row9Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row9TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row10TaxObj")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row10DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row10Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row10TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row11TaxObj")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row11DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row11Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row11TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1TtlDPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1TtlTaxAmt")))
                        Next

                        'lblErrMessage.Visible = True
                        'lblErrMessage.Text = lblErrMessage.Text & "<br>" & "PPH 23 File created in " & Trim(MyCSVFileNK)
                        'mark 1

                        LinkDownload2.Visible = True
                        LinkDownload2.Text = "Download file PPH22-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"
                        LinkDownload2.NavigateUrl = "../../../" & strUrl & "PPH22-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"

                    End If
                    dataToWriteNK.Close()

                Case "23"
                    strOpCd = "TX_STDRPT_GET_CSV_PPH23"
                    strParamName = "ACCYEAR|ACCMONTH|LOCCODE|KPPINIT|NMPTG|NPWPPTG|COMPCODE|TAXINIT|STRSEARCH"
                    strParamValue = strAccYear & "|" & strAccMonth & "|" & strLocation & "|" & Trim(hidKPPInit.Value) & "|" & _
                                    Trim(ddlPelapor.SelectedItem.Text) & "|" & Trim(ddlPelapor.SelectedItem.Value) & "|" & strCompany & "|" & hidTaxInit.Value & "|"

                    Try
                        intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, ObjTaxDsNK, objMapPath, objFTPFolder)

                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
                    End Try

                    strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
                    strUrl = Replace(strUrl, "\", "/")

                    Dim MyCSVFileNK As String = objFTPFolder & "PPH23-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"
                    If My.Computer.FileSystem.FileExists(MyCSVFileNK) = True Then
                        My.Computer.FileSystem.DeleteFile(MyCSVFileNK)
                    End If
                    Dim dataToWriteNK As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(MyCSVFileNK, True)

                    dataToWriteNK.WriteLine("Kode Form Bukti Potong;Masa Pajak;Tahun Pajak;Pembetulan;NPWP WP yang Dipotong;Nama WP yang Dipotong;Alamat WP yang Dipotong;Nomor Bukti Potong;Tanggal Bukti Potong;Nilai Bruto 1;Tarif 1;PPh Yang Dipotong  1;Nilai Bruto 2;Tarif 2;PPh Yang Dipotong  2;Nilai Bruto 3;Tarif 3;PPh Yang Dipotong  3;Nilai Bruto 4;Tarif 4;PPh Yang Dipotong  4;Nilai Bruto 5;Tarif 5;PPh Yang Dipotong  5;Nilai Bruto 6a/Nilai Bruto 6;Tarif 6a/Tarif 6;PPh Yang Dipotong  6a/PPh Yang Dipotong  6;Nilai Bruto 6b/Nilai Bruto 7;Tarif 6b/Tarif 7;PPh Yang Dipotong  6b/PPh Yang Dipotong  7;Nilai Bruto 6c/Nilai Bruto 8;Tarif 6c/Tarif 8;PPh Yang Dipotong  6c/PPh Yang Dipotong  8;Nilai Bruto 9;Tarif 9;PPh Yang Dipotong  9;Nilai Bruto 10;Perkiraan Penghasilan Netto10;Tarif 10;PPh Yang Dipotong  10;Nilai Bruto 11;Perkiraan Penghasilan Netto11;Tarif 11;PPh Yang Dipotong  11;Nilai Bruto 12;Perkiraan Penghasilan Netto12;Tarif 12;PPh Yang Dipotong  12;Nilai Bruto 13;Tarif 13;PPh Yang Dipotong  13;Kode Jasa 6d1 PMK-244/PMK.03/2008;Nilai Bruto 6d1;Tarif 6d1;PPh Yang Dipotong  6d1;Kode Jasa 6d2 PMK-244/PMK.03/2008;Nilai Bruto 6d2;Tarif 6d2;PPh Yang Dipotong  6d2;Kode Jasa 6d3 PMK-244/PMK.03/2008;Nilai Bruto 6d3;Tarif 6d3;PPh Yang Dipotong  6d3;Kode Jasa 6d4 PMK-244/PMK.03/2008;Nilai Bruto 6d4;Tarif 6d4;PPh Yang Dipotong  6d4;Kode Jasa 6d5 PMK-244/PMK.03/2008;Nilai Bruto 6d5;Tarif 6d5;PPh Yang Dipotong  6d5;Kode Jasa 6d6 PMK-244/PMK.03/2008;Nilai Bruto 6d6;Tarif 6d6;PPh Yang Dipotong  6d6;Jumlah Nilai Bruto ;Jumlah PPh Yang Dipotong")
                    If ObjTaxDsNK.Tables(0).Rows.Count > 0 Then
                        For intCnt = 0 To ObjTaxDsNK.Tables(0).Rows.Count - 1
                            dataToWriteNK.WriteLine(Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("FormNo")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("AccMonth")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("AccYear")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Pembetulan")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("splNPWP")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("splName")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("splAddress")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("TrxID")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("DocDate")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row1DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row1Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row1TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row2DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row2Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row2TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row3DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row3Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row3TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row4DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row4Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row4TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row5DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row5Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row5TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row6DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row6Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row6TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row7DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row7Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row7TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row8DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row8Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row8TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row9DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row9Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row9TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row10TaxCode")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row10DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row10Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row10TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row11TaxCode")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row11DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row11Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row11TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row12TaxCode")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row12DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row12Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row12TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row13DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row13Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row13TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row14TaxCode")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row14DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row14Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row14TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row15TaxCode")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row15DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row15Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row15TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row16TaxCode")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row16DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row16Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row16TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row17TaxCode")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row17DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row17Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row17TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row18TaxCode")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row18DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row18Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row18TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row19TaxCode")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row19DPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row19Rate")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1Row19TaxAmt")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1TtlDPPAmt")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Tbl1TtlTaxAmt")))
                        Next

                        'lblErrMessage.Visible = True
                        'lblErrMessage.Text = lblErrMessage.Text & "<br>" & "PPH 23 File created in " & Trim(MyCSVFileNK)
                        'mark 1

                        LinkDownload2.Visible = True
                        LinkDownload2.Text = "Download file PPH23-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"
                        LinkDownload2.NavigateUrl = "../../../" & strUrl & "PPH23-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"

                    End If
                    dataToWriteNK.Close()

                Case "4"
                    strOpCd = "TX_STDRPT_GET_CSV_PPH42"
                    strParamName = "ACCYEAR|ACCMONTH|LOCCODE|KPPINIT|NMPTG|NPWPPTG|COMPCODE|TAXINIT|STRSEARCH"
                    strParamValue = strAccYear & "|" & strAccMonth & "|" & strLocation & "|" & Trim(hidKPPInit.Value) & "|" & _
                                    Trim(ddlPelapor.SelectedItem.Text) & "|" & Trim(ddlPelapor.SelectedItem.Value) & "|" & strCompany & "|" & hidTaxInit.Value & "|"

                    Try
                        intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, ObjTaxDsNK, objMapPath, objFTPFolder)

                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
                    End Try

                    strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
                    strUrl = Replace(strUrl, "\", "/")

                    Dim MyCSVFileNK As String = objFTPFolder & "PPH42-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"
                    If My.Computer.FileSystem.FileExists(MyCSVFileNK) = True Then
                        My.Computer.FileSystem.DeleteFile(MyCSVFileNK)
                    End If
                    Dim dataToWriteNK As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(MyCSVFileNK, True)

                    dataToWriteNK.WriteLine("Kode Form Bukti Potong / Kode Form Input PPh Yang Dibayar Sendiri;Masa Pajak;Tahun Pajak;Pembetulan;NPWP WP yang Dipotong;Nama WP yang Dipotong;Alamat WP yang Dipotong;Nomor Bukti Potong / NTPN;Tanggal Bukti Potong/Tanggal SSP;Jenis Hadiah Undian 1 / Lokasi Tanah dan atau Bangunan / Nama Obligasi;Kode Option Tempat Penyimpanan 1 (Khusus F113310);Jumlah Nilai Bruto 1 / Jumlah Nilai Nominal Obligasi Yg Diperdagangkan Di Bursa Efek / Jumlah Penghasilan Pada Form Input Yang Dibayar Sendiri;Tarif 1 / Tingkat Bunga per Tahun;PPh Yang Dipotong  1 /Jumlah PPh Pada Form Input Yang Dibayar Sendiri;Jenis Hadiah Undian 2 / Nomor Seri Obligasi ;Kode Option Tempat Penyimpanan 2;Jumlah Nilai Bruto 2 / Jumlah Harga Perolehan Bersih (tanpa Bunga) Pada Obligasi Yg Diperdagangkan Di Bursa Efek;Tarif 2;PPh Yang Dipotong  2;Jenis Hadiah Undian 3;Kode Option Tempat Penyimpanan 3;Jumlah Nilai Bruto 3 / Jumlah Harga Penjualan Bersih (tanpa Bunga) Pada Obligasi Yg Diperdagangkan Di Bursa Efek;Tarif 3;PPh Yang Dipotong  3;Jenis Hadiah Undian 4;Kode Option Tempat Penyimpanan 4 / Kode Option Perencanaan (1) atau Pengawasan (2) atau selainnya (0) untuk BP Jasa Konstruksi poin 4;Jumlah Nilai Bruto 4 / Jumlah Diskonto Pada Obligasi Yg Diperdagangkan Di Bursa Efek;Tarif 4;PPh Yang Dipotong  4;Jenis Hadiah Undian 5;Kode Option Tempat Penyimpanan 5 / Kode Option Perencanaan (1) atau Pengawasan (2) atau selainnya (0) untuk BP Jasa Konstruksi poin 5;Jumlah Nilai Bruto 5 / Jumlah Bunga Pada Obligasi Yg Diperdagangkan Di Bursa Efek;Tarif 5;PPh Yang Dipotong  5;Jenis Hadiah Undian 6;Jumlah Nilai Bruto 6 / Jumlah Total Bunga atau Diskonto Obligasi Yang Diperdagangkan;Tarif 6 / Tarif PPh Final Pada Obligasi Yang Diperdagangkan Di Bursa Efek;PPh Yang Dipotong  6;Jumlah Nilai Bruto 7;Tarif 7;PPh Yang Dipotong 7;Jenis Penghasilan 8;Jumlah Nilai Bruto 8;Tarif 8;PPh Yang Dipotong 8;Jumlah PPh Yang Dipotong;Tanggal Jatuh Tempo Obligasi;Tanggal Perolehan Obligasi;Tanggal Penjualan Obligasi;Holding Periode Obligasi (Hari);Time Periode Obligasi (Hari)")
                    If ObjTaxDsNK.Tables(0).Rows.Count > 0 Then
                        For intCnt = 0 To ObjTaxDsNK.Tables(0).Rows.Count - 1
                            dataToWriteNK.WriteLine(Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("FormNo")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("AccMonth")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("AccYear")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Pembetulan")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("splNPWP")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("splName")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("splAddress")) & ";" & _
                            Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("TrxID")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("DocDate")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row1A")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row1B")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row1C")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row1D")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row1E")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row2A")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row2B")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row2C")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row2D")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row2E")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row3A")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row3B")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row3C")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row3D")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row3E")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row4A")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row4B")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row4C")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row4D")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row4E")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row5A")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row5B")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row5C")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row5D")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row5E")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row6A")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row6C")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row6D")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row6E")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row7C")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row7D")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row7E")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row8A")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row8C")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row8D")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("Row8E")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("RowTotal")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("TglJatuhTempo")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("TglPerolehanObligasi")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("TglPenjualanObligasi")) & ";" & _
							Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("HoldingPeriod")) & ";" & Trim(ObjTaxDsNK.Tables(0).Rows(intCnt).Item("TimePeriod")))
                        Next

                        'lblErrMessage.Visible = True
                        'lblErrMessage.Text = lblErrMessage.Text & "<br>" & "PPH 23 File created in " & Trim(MyCSVFileNK)
                        'mark 1

                        LinkDownload2.Visible = True
                        LinkDownload2.Text = "Download file PPH42-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"
                        LinkDownload2.NavigateUrl = "../../../" & strUrl & "PPH42-" & Trim(strCompany) & Trim(strPeriod) & "-" & Trim(hidKPPInit.Value) & ".csv"

                    End If
                    dataToWriteNK.Close()

            End Select
            
        End If

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
            BindAccCodeDropList(hidTaxInit.Value)
            BindCompKPP(hidKPPInit.Value)
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
