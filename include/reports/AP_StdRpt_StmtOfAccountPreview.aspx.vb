Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class AP_StdRpt_StmtOfAccount_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objAP As New agri.AP.clsReport()
    Dim objAPTrx As New agri.AP.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmin As New agri.Admin.clsShare   
    Dim objAdminCty As New agri.Admin.clsCountry

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim strPhyMonth As String
    Dim strPhyYear As String

    Dim tempLoc As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim strParam As String
    Dim strDate As String
    Dim strSuppCode As String
    Dim objMapPath As String
    Dim arrUserLoc As Array
    Dim intCntUserLoc As Integer
    Dim strLocCode As String
    Dim strOrderBy As String
    Dim strFileName As String
    Dim strSelPhyMonth As String
    Dim strSelPhyYear As String
    Dim objDsAgeingYTDAccPeriod As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Dim dsBalBF As New DataSet()
    Dim dsTrx As New DataSet()
    Dim dsStmtTrx As New DataSet()
    Dim dsLoc As New Object
    Dim dsSummary As New DataSet

    Dim decBalBF As Decimal

    Dim tblBalBF As DataTable = New DataTable("tblBalBF")
    Dim tblStmtTrx As DataTable = New DataTable("tblStmtTrx")
    Dim tblSummary As DataTable = New DataTable("tblSummary")

    Dim decInvTotalAmt As Decimal
    Dim dec30DInvTotal As Decimal
    Dim dec60DInvTotal As Decimal
    Dim dec90DInvTotal As Decimal
    Dim dec120DInvTotal As Decimal
    Dim dec120DAbvInvTotal As Decimal

    Dim decDNTotalAmt As Decimal
    Dim dec30DDNTotal As Decimal
    Dim dec60DDNTotal As Decimal
    Dim dec90DDNTotal As Decimal
    Dim dec120DDNTotal As Decimal
    Dim dec120DAbvDNTotal As Decimal

    Dim decCNTotalAmt As Decimal
    Dim dec30DCNTotal As Decimal
    Dim dec60DCNTotal As Decimal
    Dim dec90DCNTotal As Decimal
    Dim dec120DCNTotal As Decimal
    Dim dec120DAbvCNTotal As Decimal

    Dim decCJTotalAmt As Decimal
    Dim dec30DCJTotal As Decimal
    Dim dec60DCJTotal As Decimal
    Dim dec90DCJTotal As Decimal
    Dim dec120DCJTotal As Decimal
    Dim dec120DAbvCJTotal As Decimal

    Dim decPYTotalAmt As Decimal
    Dim dec30DPYTotal As Decimal
    Dim dec60DPYTotal As Decimal
    Dim dec90DPYTotal As Decimal
    Dim dec120DPYTotal As Decimal
    Dim dec120DAbvPYTotal As Decimal

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        strDDLAccMth = Request.QueryString("DDLAccMth")
        strDDLAccYr = Request.QueryString("DDLAccYr")

        If Request.QueryString("StmtType") = objAP.EnumStmtOfAccountType.Movement Then
            strOrderBy = "RefDate, DocNo"
            strFileName = "AP_StdRpt_StmtOfAccountMove"
        Else
            strOrderBy = "DocNo, RefDate"
            strFileName = "AP_StdRpt_StmtOfAccountOut"
        End If

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            HeaderDetails()
            BindReport()
        End If
    End Sub

    Sub HeaderDetails()
        Dim strOpCd_CompLoc_GET As String = "ADMIN_CLSSHARE_COMPLOC_DETAILS_GET" 
        Dim strOpCd_Country_GET As String = "ADMIN_CLSCOUNTRY_COUNTRY_DETAILS_GET"
        Dim dsCountry As New Object()

        Try
            intErrNo = objSysCfg.mtdGetPhyPeriod(strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 strDDLAccMth, _
                                                 strDDLAccYr, _
                                                 strPhyMonth, _
                                                 strPhyYear, _
                                                 strSelPhyMonth, _
                                                 strSelPhyYear)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_PHYPERIOD_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        strParam = Request.QueryString("CompCode").Trim & "|" & strUserLoc & "|"
        Try
            intErrNo = objAdmin.mtdGetLocDetails(strOpCd_CompLoc_GET, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 dsLoc, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_LOCPDET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        strParam = dsLoc.tables(0).rows(0).item("CountryCode").trim() & "|"
        Try
            intErrNo = objAdminCty.mtdGetCountryDetails(strOpCd_Country_GET, dsCountry, strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_COUNTRYDET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dsLoc.tables(0).rows(0).item("LocCountryName") = dsCountry.tables(0).rows(0).item("CountryDesc").trim()

    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim dsLatestInvDate As New DataSet()
        Dim dsPayDate As New DataSet()

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_StmtOfAcc_Supplier_GET As String = "PU_STDRPT_STMTOFACC_SUPPLIER_GET"
        Dim strOpCd_StmtOfAcc_LatestInvDate_GET As String = "PU_STDRPT_STMTOFACC_LATESTINVDATE_GET"
        Dim strOpCd_StmtOfAcc_PayDate_GET As String = "PU_STDRPT_STMTOFACC_PAYDATE_GET"
        Dim SearchStr As String
        Dim SQLStr As String

        Dim intCnt As Integer

        Dim colParam As New Collection
        Dim objStmtAcc As New DataSet()
        Dim objFTPFolder As String

        colParam.Add(Request.QueryString("Supplier"), "PARAM_SUPPLIERCODE")
        colParam.Add("'" & strUserLoc & "'", "PARAM_LOCATION")
        colParam.Add(strDDLAccMth, "PARAM_ACCMONTH")
        colParam.Add(strDDLAccYr, "PARAM_ACCYEAR")
        If Request.QueryString("StmtType") = objAP.EnumStmtOfAccountType.Movement Then
            colParam.Add(objAP.EnumStmtOfAccountType.Movement, "PARAM_RPTTYPE")
        Else
            colParam.Add(objAP.EnumStmtOfAccountType.Oustanding, "PARAM_RPTTYPE")
        End If
        colParam.Add(Request.QueryString("CutOffDate"), "PARAM_CUTOFFDATE")
        colParam.Add("AP_STDRPT_STMTACC_GET_SP", "OC_REPORT_GET")

        Try
            intErrNo = objAP.mtdGetReport_StmtAcc(colParam, objStmtAcc, objMapPath, objFTPFolder)
            objStmtAcc.Tables(0).TableName = "AP_STMTOFACCOUNT"
            objStmtAcc.Tables(1).TableName = "AP_STMTOFACCOUNT_BALBF"
            objStmtAcc.Tables(2).TableName = "AP_STMTOFACCOUNT_TRX"
            objStmtAcc.Tables(3).TableName = "AP_STMTOFACCOUNT_SUMMARY"
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTACC_GET&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=")
        End Try

        objRptDs.Tables.Add(objStmtAcc.Tables(0).Copy())
        objRptDs.Tables(0).TableName = "AP_STMTOFACCOUNT"
        objRptDs.Tables.Add(objStmtAcc.Tables(2).Copy())
        objRptDs.Tables(1).TableName = "AP_STMTOFACCOUNT_TRX"
        objRptDs.Tables.Add(dsLoc.Tables(0).Copy())
        objRptDs.Tables(2).TableName = "AP_STMTOFACCOUNT_LOCDET"
        objRptDs.Tables.Add(objStmtAcc.Tables(3).Copy())
        objRptDs.Tables(3).TableName = "AP_STMTOFACCOUNT_SUMMARY"
        objRptDs.Tables.Add(objStmtAcc.Tables(1).Copy())
        objRptDs.Tables(4).TableName = "AP_STMTOFACCOUNT_BALBF"


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".pdf"">")
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")
    End Sub

    Sub BalBF()
        Dim strOpCd_StmtOfAcc_BalBF_Inv_GET As String = "PU_STDRPT_STMTOFACC_BALBF_INV_GET"
        Dim strOpCd_StmtOfAcc_BalBF_DebitNote_GET As String = "PU_STDRPT_STMTOFACC_BALBF_DN_GET"
        Dim strOpCd_StmtOfAcc_BalBF_CreditNote_GET As String = "PU_STDRPT_STMTOFACC_BALBF_CN_GET"
        Dim strOpCd_StmtOfAcc_BalBF_Payment_GET As String = "PU_STDRPT_STMTOFACC_BALBF_PY_GET"
        Dim strOpCd_StmtOfAcc_BalBF_CreditJournal_WriteOff_IRDN_GET As String = "PU_STDRPT_STMTOFACC_BALBF_WRITEOFF_IRDN_GET"
        Dim strOpCd_StmtOfAcc_BalBF_CreditJournal_WriteOff_CN_GET As String = "PU_STDRPT_STMTOFACC_BALBF_WRITEOFF_CN_GET"
        Dim strOpCd_StmtOfAcc_BalBF_CreditJournal_Void_PY_GET As String = "PU_STDRPT_STMTOFACC_BALBF_VOID_PY_GET"
        Dim strOpCd_StmtOfAcc_BalBF_CreditJournal_Adj_IRDNCN_GET As String = "PU_STDRPT_STMTOFACC_BALBF_ADJ_IRDNCN_GET"
        Dim dsInvAmt As New DataSet()
        Dim dsDNAmt As New DataSet()
        Dim dsCNAmt As New DataSet()
        Dim dsPYAmt As New DataSet()
        Dim dsCJWOIRDNAmt As New DataSet()
        Dim dsCJWOCNAmt As New DataSet()
        Dim dsCJVoidPYAmt As New DataSet()
        Dim dsCJAdjIRDNCNAmt As New DataSet()
        Dim decInvAmt As Decimal
        Dim decDNAmt As Decimal
        Dim decCNAmt As Decimal
        Dim decPYAmt As Decimal
        Dim decCJAmt As Decimal
        Dim objDsYTDAccPeriod As String
        Dim dr As DataRow



        Try
            intErrNo = objSysCfg.mtdGetAccPeriodCol(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strDDLAccMth, _
                                                    strDDLAccYr, _
                                                    objSysCfg.EnumAccountPeriodType.YTDMinusSelPeriod, _
                                                    "AP.AccMonth", _
                                                    "AP.AccYear", _
                                                    objDsYTDAccPeriod)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_YTDACCPERIOD_BALBF&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        strParam = "|||||" & strLocCode & "|||" & _
                   objAPTrx.EnumInvoiceRcvStatus.Confirmed & "','" & objAPTrx.EnumInvoiceRcvStatus.Writeoff & "','" & _
                   objAPTrx.EnumInvoiceRcvStatus.Paid & "','" & objAPTrx.EnumInvoiceRcvStatus.Closed & _
                   "||||||" & strSuppCode & "||||||||" & objDsYTDAccPeriod & "||"
        Try
            intErrNo = objAP.mtdGetReport_StmtOfAccount(strOpCd_StmtOfAcc_BalBF_Inv_GET, strParam, dsInvAmt, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_INVAMT_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        decInvAmt = dsInvAmt.Tables(0).Rows(0).Item("InvAmt")

        strParam = "|||||" & strLocCode & "|||" & _
                   objAPTrx.EnumDebitNoteStatus.Confirmed & "','" & objAPTrx.EnumDebitNoteStatus.Writeoff & "','" & _
                   objAPTrx.EnumDebitNoteStatus.Paid & "','" & objAPTrx.EnumDebitNoteStatus.Closed & _
                   "||||||" & strSuppCode & "||||||||" & objDsYTDAccPeriod & "||"
        Try
            intErrNo = objAP.mtdGetReport_StmtOfAccount(strOpCd_StmtOfAcc_BalBF_DebitNote_GET, strParam, dsDNAmt, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_DNAMT_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        decDNAmt = dsDNAmt.Tables(0).Rows(0).Item("DNAmt")

        strParam = "|||||" & strLocCode & "|||" & _
                   objAPTrx.EnumCreditNoteStatus.Confirmed & "','" & objAPTrx.EnumCreditNoteStatus.Writeoff & "','" & _
                   objAPTrx.EnumCreditNoteStatus.Paid & "','" & objAPTrx.EnumCreditNoteStatus.Closed & _
                   "||||||" & strSuppCode & "||||||||" & objDsYTDAccPeriod & "||"
        Try
            intErrNo = objAP.mtdGetReport_StmtOfAccount(strOpCd_StmtOfAcc_BalBF_CreditNote_GET, strParam, dsCNAmt, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_CNAMT_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        decCNAmt = dsCNAmt.Tables(0).Rows(0).Item("CNAmt")

        strParam = "|||||" & strLocCode & "|||" & objAPTrx.EnumPaymentStatus.Confirmed & "','" & objAPTrx.EnumPaymentStatus.Closed  & "','" & objAPTrx.EnumPaymentStatus.Void & _
                   "||||||" & strSuppCode & "||||||||" & objDsYTDAccPeriod & "||"
        Try
            intErrNo = objAP.mtdGetReport_StmtOfAccount(strOpCd_StmtOfAcc_BalBF_Payment_GET, strParam, dsPYAmt, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_PYAMT_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        decPYAmt = dsPYAmt.Tables(0).Rows(0).Item("PYAmt")

        strParam = "|||||" & strLocCode & "|||" & _
                   objAPTrx.EnumCreditorJournalStatus.Confirmed & "','" & objAPTrx.EnumCreditorJournalStatus.Closed & _
                   "||||||" & strSuppCode & "|" & objAPTrx.EnumCreditorJournalType.Writeoff & "|||" & _
                   objAPTrx.EnumPaymentDocType.InvoiceReceive & "','" & objAPTrx.EnumPaymentDocType.DebitNote & "||||" & _
                   objDsYTDAccPeriod & "||"
        Try
            intErrNo = objAP.mtdGetReport_StmtOfAccount(strOpCd_StmtOfAcc_BalBF_CreditJournal_WriteOff_IRDN_GET, strParam, dsCJWOIRDNAmt, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_CJWOIRDNAMT_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try



        
        decCJAmt = dsCJWOIRDNAmt.Tables(0).Rows(0).Item("CJAmtWOIRDN")
        decBalBF = -decInvAmt - decDNAmt + decCNAmt + decPYAmt + decCJAmt


        dr = tblBalBF.NewRow()
        dr("LocCode") = strLocCode
        dr("SupplierCode") = strSuppCode
        dr("Description") = "Bal B/F"
        If decBalBF > 0 Then
            dr("Debit") = decBalBF
            dr("Credit") = 0
        Else
            dr("Debit") = 0
            dr("Credit") = -1 * decBalBF
        End If
        tblBalBF.Rows.Add(dr)
    End Sub


    Sub TrxListing()
        Dim strOpCd_StmtOfAcc_TrxListing_GET As String = "PU_STDRPT_STMTOFACC_TRXLISTING_GET"
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim decDebit As Decimal
        Dim decCredit As Decimal


        strParam = objGlobal.EnumDocType.InvoiceReceive & "|" & _
                   objGlobal.EnumDocType.APDebitNote & "|" & _
                   objGlobal.EnumDocType.APCreditNote & "|" & _
                   objGlobal.EnumDocType.APPayment & "|" & _
                   objGlobal.EnumDocType.APCreditJrn & "|" & _
                   strLocCode & "|" & strDDLAccMth & "|" & strDDLAccYr & "||" & _
                   objAPTrx.EnumInvoiceRcvStatus.Confirmed & "','" & objAPTrx.EnumInvoiceRcvStatus.Writeoff & "','" & _
                   objAPTrx.EnumInvoiceRcvStatus.Paid & "','" & objAPTrx.EnumInvoiceRcvStatus.Closed & "|" & _
                   objAPTrx.EnumDebitNoteStatus.Confirmed & "','" & objAPTrx.EnumDebitNoteStatus.Writeoff & "','" & _
                   objAPTrx.EnumDebitNoteStatus.Paid & "','" & objAPTrx.EnumDebitNoteStatus.Closed & "|" & _
                   objAPTrx.EnumCreditNoteStatus.Confirmed & "','" & objAPTrx.EnumCreditNoteStatus.Writeoff & "','" & _
                   objAPTrx.EnumCreditNoteStatus.Paid & "','" & objAPTrx.EnumCreditNoteStatus.Closed & "|" & _
                   objAPTrx.EnumPaymentStatus.Confirmed & "','" & objAPTrx.EnumPaymentStatus.Closed & "','" & objAPTrx.EnumPaymentStatus.Void & "|" & _
                   objAPTrx.EnumCreditorJournalStatus.Confirmed & "','" & objAPTrx.EnumCreditorJournalStatus.Closed & "|" & _
                   strSuppCode & "|" & _
                   objAPTrx.EnumCreditorJournalType.Writeoff & "|" & _
                   objAPTrx.EnumCreditorJournalType.Void & "|" & _
                   objAPTrx.EnumCreditorJournalType.Adjustment & "|" & _
                   objAPTrx.EnumPaymentDocType.InvoiceReceive & "','" & objAPTrx.EnumPaymentDocType.DebitNote & "|" & _
                   objAPTrx.EnumPaymentDocType.CreditNote & "|" & _
                   objAPTrx.EnumPaymentDocType.Payment & "|" & _
                   objAPTrx.EnumPaymentDocType.InvoiceReceive & "','" & objAPTrx.EnumPaymentDocType.DebitNote & "','" & _
                   objAPTrx.EnumPaymentDocType.CreditNote & "||" & strOrderBy & "|"

        Try
            intErrNo = objAP.mtdGetReport_StmtOfAccount(strOpCd_StmtOfAcc_TrxListing_GET, strParam, dsTrx, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_TRX_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        For intCnt = 0 To dsTrx.Tables(0).Rows.Count - 1

            dr = tblStmtTrx.NewRow()
            dr("LocCode") = Trim(dsTrx.Tables(0).Rows(intCnt).Item("LocCode"))
            dr("SupplierCode") = Trim(dsTrx.Tables(0).Rows(intCnt).Item("SupplierCode"))
            If Not IsDBNull(dsTrx.Tables(0).Rows(intCnt).Item("DocNo")) Then
                dr("DocNo") = Trim(dsTrx.Tables(0).Rows(intCnt).Item("DocNo"))
            Else
                dr("DocNo") = ""
            End If
            If Not IsDBNull(dsTrx.Tables(0).Rows(intCnt).Item("Type")) Then
                dr("Type") = Trim(dsTrx.Tables(0).Rows(intCnt).Item("Type"))
            Else
                dr("Type") = ""
            End If
            If Not IsDBNull(dsTrx.Tables(0).Rows(intCnt).Item("RefDate")) Then
                dr("RefDate") = Trim(dsTrx.Tables(0).Rows(intCnt).Item("RefDate"))
            Else
                dr("RefDate") = ""
            End If
            If Not IsDBNull(dsTrx.Tables(0).Rows(intCnt).Item("RefNo")) Then
                dr("RefNo") = Trim(dsTrx.Tables(0).Rows(intCnt).Item("RefNo"))
            Else
                dr("RefNo") = ""
            End If
            If Not IsDBNull(dsTrx.Tables(0).Rows(intCnt).Item("Description")) Then
                dr("Description") = Trim(dsTrx.Tables(0).Rows(intCnt).Item("Description"))
            Else
                dr("Description") = ""
            End If
            If Not IsDBNull(dsTrx.Tables(0).Rows(intCnt).Item("Debit")) Then
                dr("Debit") = dsTrx.Tables(0).Rows(intCnt).Item("Debit")
                decDebit += dsTrx.Tables(0).Rows(intCnt).Item("Debit")
            Else
                dr("Debit") = 0
                decDebit = 0
            End If
            If Not IsDBNull(dsTrx.Tables(0).Rows(intCnt).Item("Credit")) Then
                dr("Credit") = dsTrx.Tables(0).Rows(intCnt).Item("Credit")
                decCredit += dsTrx.Tables(0).Rows(intCnt).Item("Credit")
            Else
                dr("Credit") = 0
                decCredit = 0
            End If

            dr("Bal") = 0

            tblStmtTrx.Rows.Add(dr)

        Next

    End Sub

    Sub InvoiceRcvTrx()
        Dim strOpCd_StmtOfAcc_Inv_GET As String = "AP_STDRPT_STMTOFACCOUNT_INV_GET"
        Dim dsInvoice As New DataSet()
        Dim intInvDays As Integer
        Dim intCntInvDays As Integer

        strParam = objGlobal.GetLongDate(strDate) & "|" & strLocCode & "|" & objDsAgeingYTDAccPeriod & "||" & _
                   objAPTrx.EnumInvoiceRcvStatus.Confirmed & "','" & objAPTrx.EnumInvoiceRcvStatus.Writeoff & "','" & _
                   objAPTrx.EnumInvoiceRcvStatus.Paid & "','" & objAPTrx.EnumInvoiceRcvStatus.Closed & "|" & strSuppCode & "|"
        Try
            intErrNo = objAP.mtdGetReport_StmtOfAccTrxList(strOpCd_StmtOfAcc_Inv_GET, strParam, dsInvoice, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_INV_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        For intCntInvDays = 0 To dsInvoice.Tables(0).Rows.Count - 1
            intInvDays = dsInvoice.Tables(0).Rows(intCntInvDays).Item("Days")
            decInvTotalAmt = dsInvoice.Tables(0).Rows(intCntInvDays).Item("TotalAmount")

            If intInvDays >= 0 And intInvDays <= 30 Then
                dec30DInvTotal += decInvTotalAmt
            ElseIf intInvDays >= 31 And intInvDays <= 60 Then
                dec60DInvTotal += decInvTotalAmt
            ElseIf intInvDays >= 61 And intInvDays <= 90 Then
                dec90DInvTotal += decInvTotalAmt
            ElseIf intInvDays >= 91 And intInvDays <= 120 Then
                dec120DInvTotal += decInvTotalAmt
            ElseIf intInvDays >= 121 Then
                dec120DAbvInvTotal += decInvTotalAmt
            End If
        Next

    End Sub

    Sub DebitNoteTrx()
        Dim strOpCd_StmtOfAcc_DebitNote_GET As String = "AP_STDRPT_STMTOFACCOUNT_DN_GET"
        Dim dsDebitNote As New DataSet()
        Dim intDNDays As Integer
        Dim intCntDNDays As Integer

        strParam = objGlobal.GetLongDate(strDate) & "|" & strLocCode & "|" & objDsAgeingYTDAccPeriod & "||" & _
                   objAPTrx.EnumDebitNoteStatus.Confirmed & "','" & objAPTrx.EnumDebitNoteStatus.Writeoff & "','" & _
                   objAPTrx.EnumDebitNoteStatus.Paid & "','" & objAPTrx.EnumDebitNoteStatus.Closed & "|" & strSuppCode & "|"
        Try
            intErrNo = objAP.mtdGetReport_StmtOfAccTrxList(strOpCd_StmtOfAcc_DebitNote_GET, strParam, dsDebitNote, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_DN_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCntDNDays = 0 To dsDebitNote.Tables(0).Rows.Count - 1
            intDNDays = dsDebitNote.Tables(0).Rows(intCntDNDays).Item("Days")
            decDNTotalAmt = dsDebitNote.Tables(0).Rows(intCntDNDays).Item("TotalAmount")

            If intDNDays >= 0 And intDNDays <= 30 Then
                dec30DDNTotal += decDNTotalAmt
            ElseIf intDNDays >= 31 And intDNDays <= 60 Then
                dec60DDNTotal += decDNTotalAmt
            ElseIf intDNDays >= 61 And intDNDays <= 90 Then
                dec90DDNTotal += decDNTotalAmt
            ElseIf intDNDays >= 91 And intDNDays <= 120 Then
                dec120DDNTotal += decDNTotalAmt
            ElseIf intDNDays >= 121 Then
                dec120DAbvDNTotal += decDNTotalAmt
            End If

        Next

    End Sub

    Sub CreditNoteTrx()
        Dim strOpCd_StmtOfAcc_CreditNote_GET As String = "AP_STDRPT_STMTOFACCOUNT_CN_GET"
        Dim dsCreditNote As New DataSet()
        Dim intCNDays As Integer
        Dim intCntCNDays As Integer

        strParam = objGlobal.GetLongDate(strDate) & "|" & strLocCode & "|" & objDsAgeingYTDAccPeriod & "||" & _
                   objAPTrx.EnumCreditNoteStatus.Confirmed & "','" & objAPTrx.EnumCreditNoteStatus.Writeoff & "','" & _
                   objAPTrx.EnumCreditNoteStatus.Paid & "','" & objAPTrx.EnumCreditNoteStatus.Closed & "|" & strSuppCode & "|"
        Try
            intErrNo = objAP.mtdGetReport_StmtOfAccTrxList(strOpCd_StmtOfAcc_CreditNote_GET, strParam, dsCreditNote, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_CN_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCntCNDays = 0 To dsCreditNote.Tables(0).Rows.Count - 1
            intCNDays = dsCreditNote.Tables(0).Rows(intCntCNDays).Item("Days")
            decCNTotalAmt = dsCreditNote.Tables(0).Rows(intCntCNDays).Item("TotalAmount")

            If intCNDays >= 0 And intCNDays <= 30 Then
                dec30DCNTotal += decCNTotalAmt
            ElseIf intCNDays >= 31 And intCNDays <= 60 Then
                dec60DCNTotal += decCNTotalAmt
            ElseIf intCNDays >= 61 And intCNDays <= 90 Then
                dec90DCNTotal += decCNTotalAmt
            ElseIf intCNDays >= 91 And intCNDays <= 120 Then
                dec120DCNTotal += decCNTotalAmt
            ElseIf intCNDays >= 121 Then
                dec120DAbvCNTotal += decCNTotalAmt
            End If

        Next

    End Sub

    Sub CreditJournalTrx()
        Dim strOpCd_StmtOfAcc_CreditJournal_GET As String = "AP_STDRPT_STMTOFACCOUNT_CJ_GET"
        Dim dsCreditJournal As New DataSet()
        Dim intCJDays As Integer
        Dim intCntCJDays As Integer

        strParam = objGlobal.GetLongDate(strDate) & "|" & strLocCode & "|" & objDsAgeingYTDAccPeriod & "||" & _
                   objAPTrx.EnumCreditorJournalStatus.Confirmed & "','" & objAPTrx.EnumCreditorJournalStatus.Closed & "|" & _
                   strSuppCode & "|"
        Try
            intErrNo = objAP.mtdGetReport_StmtOfAccTrxList(strOpCd_StmtOfAcc_CreditJournal_GET, strParam, dsCreditJournal, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_CJ_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCntCJDays = 0 To dsCreditJournal.Tables(0).Rows.Count - 1
            intCJDays = dsCreditJournal.Tables(0).Rows(intCntCJDays).Item("Days")
            decCJTotalAmt = dsCreditJournal.Tables(0).Rows(intCntCJDays).Item("TotalAmount")

            If intCJDays >= 0 And intCJDays <= 30 Then
                dec30DCJTotal += decCJTotalAmt
            ElseIf intCJDays >= 31 And intCJDays <= 60 Then
                dec60DCJTotal += decCJTotalAmt
            ElseIf intCJDays >= 61 And intCJDays <= 90 Then
                dec90DCJTotal += decCJTotalAmt
            ElseIf intCJDays >= 91 And intCJDays <= 120 Then
                dec120DCJTotal += decCJTotalAmt
            ElseIf intCJDays >= 121 Then
                dec120DAbvCJTotal += decCJTotalAmt
            End If

        Next

    End Sub

    Sub PaymentTrx()
        Dim strOpCd_StmtOfAcc_Payment_GET As String = "AP_STDRPT_STMTOFACCOUNT_PY_GET"
        Dim dsPayment As New DataSet
        Dim intPYDays As Integer
        Dim intCntPYDays As Integer

        strParam = objGlobal.GetLongDate(strDate) & "|" & strLocCode & "|" & objDsAgeingYTDAccPeriod & "||" & _
                   objAPTrx.EnumPaymentStatus.Confirmed & "','" & objAPTrx.EnumPaymentStatus.Closed & "','" & objAPTrx.EnumPaymentStatus.Void & "|" & strSuppCode & "|"
        Try
            intErrNo = objAP.mtdGetReport_StmtOfAccTrxList(strOpCd_StmtOfAcc_Payment_GET, strParam, dsPayment, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_PY_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCntPYDays = 0 To dsPayment.Tables(0).Rows.Count - 1
            intPYDays = dsPayment.Tables(0).Rows(intCntPYDays).Item("Days")
            decPYTotalAmt = dsPayment.Tables(0).Rows(intCntPYDays).Item("TotalAmount")

            If intPYDays >= 0 And intPYDays <= 30 Then
                dec30DPYTotal += decPYTotalAmt
            ElseIf intPYDays >= 31 And intPYDays <= 60 Then
                dec60DPYTotal += decPYTotalAmt
            ElseIf intPYDays >= 61 And intPYDays <= 90 Then
                dec90DPYTotal += decPYTotalAmt
            ElseIf intPYDays >= 91 And intPYDays <= 120 Then
                dec120DPYTotal += decPYTotalAmt
            ElseIf intPYDays >= 121 Then
                dec120DAbvPYTotal += decPYTotalAmt
            End If

        Next

    End Sub

    Sub SummaryListing()
        Dim dr As DataRow

        dr = tblSummary.NewRow()
        dr("LocCode") = strLocCode
        dr("SupplierCode") = strSuppCode

        
        dr("Days30") = - dec30DInvTotal - dec30DDNTotal + dec30DCNTotal + dec30DCJTotal + dec30DPYTotal
        dr("Days60") = - dec60DInvTotal - dec60DDNTotal + dec60DCNTotal + dec60DCJTotal + dec60DPYTotal
        dr("Days90") = - dec90DInvTotal - dec90DDNTotal + dec90DCNTotal + dec90DCJTotal + dec90DPYTotal
        dr("Days120") = - dec120DInvTotal - dec120DDNTotal + dec120DCNTotal + dec120DCJTotal + dec120DPYTotal
        dr("Days120Abv") = - dec120DAbvInvTotal - dec120DAbvDNTotal + dec120DAbvCNTotal + dec120DAbvCJTotal + dec120DAbvPYTotal

        dr("AmountDue") = dr("Days30") + dr("Days60") + dr("Days90") + dr("Days120") + dr("Days120Abv")
        dr("NetBal") = dr("Days30") + dr("Days60") + dr("Days90") + dr("Days120") + dr("Days120Abv")
        tblSummary.Rows.Add(dr)

        dec30DInvTotal = 0
        dec30DDNTotal = 0
        dec30DCNTotal = 0
        dec30DCJTotal = 0
        dec30DPYTotal = 0

        dec60DInvTotal = 0
        dec60DDNTotal = 0
        dec60DCNTotal = 0
        dec60DCJTotal = 0
        dec60DPYTotal = 0

        dec90DInvTotal = 0
        dec90DDNTotal = 0
        dec90DCNTotal = 0
        dec90DCJTotal = 0
        dec90DPYTotal = 0

        dec120DInvTotal = 0
        dec120DDNTotal = 0
        dec120DCNTotal = 0
        dec120DCJTotal = 0
        dec120DPYTotal = 0

        dec120DAbvInvTotal = 0
        dec120DAbvDNTotal = 0
        dec120DAbvCNTotal = 0
        dec120DAbvCJTotal = 0
        dec120DAbvPYTotal = 0
    End Sub

    Sub CreateBalBFCols()
        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()
        Dim Col4 As DataColumn = New DataColumn()
        Dim Col5 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "LocCode"
        Col1.ColumnName = "LocCode"
        Col1.DefaultValue = ""
        tblBalBF.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.String")
        Col2.AllowDBNull = True
        Col2.Caption = "SupplierCode"
        Col2.ColumnName = "SupplierCode"
        Col2.DefaultValue = ""
        tblBalBF.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.String")
        Col3.AllowDBNull = True
        Col3.Caption = "Description"
        Col3.ColumnName = "Description"
        Col3.DefaultValue = ""
        tblBalBF.Columns.Add(Col3)

        Col4.DataType = System.Type.GetType("System.Decimal")
        Col4.AllowDBNull = True
        Col4.Caption = "Debit"
        Col4.ColumnName = "Debit"
        Col4.DefaultValue = 0
        tblBalBF.Columns.Add(Col4)

        Col5.DataType = System.Type.GetType("System.Decimal")
        Col5.AllowDBNull = True
        Col5.Caption = "Credit"
        Col5.ColumnName = "Credit"
        Col5.DefaultValue = 0
        tblBalBF.Columns.Add(Col5)

    End Sub

    Sub CreateTrxCols()

        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()
        Dim Col4 As DataColumn = New DataColumn()
        Dim Col5 As DataColumn = New DataColumn()
        Dim Col6 As DataColumn = New DataColumn()
        Dim Col7 As DataColumn = New DataColumn()
        Dim Col8 As DataColumn = New DataColumn()
        Dim Col9 As DataColumn = New DataColumn()
        Dim Col10 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "LocCode"
        Col1.ColumnName = "LocCode"
        Col1.DefaultValue = ""
        tblStmtTrx.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.String")
        Col2.AllowDBNull = True
        Col2.Caption = "SupplierCode"
        Col2.ColumnName = "SupplierCode"
        Col2.DefaultValue = ""
        tblStmtTrx.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.String")
        Col3.AllowDBNull = True
        Col3.Caption = "DocNo"
        Col3.ColumnName = "DocNo"
        Col3.DefaultValue = ""
        tblStmtTrx.Columns.Add(Col3)

        Col4.DataType = System.Type.GetType("System.String")
        Col4.AllowDBNull = True
        Col4.Caption = "Type"
        Col4.ColumnName = "Type"
        Col4.DefaultValue = ""
        tblStmtTrx.Columns.Add(Col4)

        Col5.DataType = System.Type.GetType("System.String")
        Col5.AllowDBNull = True
        Col5.Caption = "RefDate"
        Col5.ColumnName = "RefDate"
        Col5.DefaultValue = ""
        tblStmtTrx.Columns.Add(Col5)

        Col6.DataType = System.Type.GetType("System.String")
        Col6.AllowDBNull = True
        Col6.Caption = "RefNo"
        Col6.ColumnName = "RefNo"
        Col6.DefaultValue = ""
        tblStmtTrx.Columns.Add(Col6)

        Col7.DataType = System.Type.GetType("System.String")
        Col7.AllowDBNull = True
        Col7.Caption = "Description"
        Col7.ColumnName = "Description"
        Col7.DefaultValue = ""
        tblStmtTrx.Columns.Add(Col7)

        Col8.DataType = System.Type.GetType("System.Decimal")
        Col8.AllowDBNull = True
        Col8.Caption = "Debit"
        Col8.ColumnName = "Debit"
        Col8.DefaultValue = 0
        tblStmtTrx.Columns.Add(Col8)

        Col9.DataType = System.Type.GetType("System.Decimal")
        Col9.AllowDBNull = True
        Col9.Caption = "Credit"
        Col9.ColumnName = "Credit"
        Col9.DefaultValue = 0
        tblStmtTrx.Columns.Add(Col9)

        Col10.DataType = System.Type.GetType("System.Decimal")
        Col10.AllowDBNull = True
        Col10.Caption = "Bal"
        Col10.ColumnName = "Bal"
        Col10.DefaultValue = 0
        tblStmtTrx.Columns.Add(Col10)
    End Sub

    Sub CreateSumCols()

        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()
        Dim Col4 As DataColumn = New DataColumn()
        Dim Col5 As DataColumn = New DataColumn()
        Dim Col6 As DataColumn = New DataColumn()
        Dim Col7 As DataColumn = New DataColumn()
        Dim Col8 As DataColumn = New DataColumn()
        Dim Col9 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "LocCode"
        Col1.ColumnName = "LocCode"
        Col1.DefaultValue = ""
        tblSummary.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.String")
        Col2.AllowDBNull = True
        Col2.Caption = "SupplierCode"
        Col2.ColumnName = "SupplierCode"
        Col2.DefaultValue = ""
        tblSummary.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.Decimal")
        Col3.AllowDBNull = True
        Col3.Caption = "Days30"
        Col3.ColumnName = "Days30"
        Col3.DefaultValue = 0
        tblSummary.Columns.Add(Col3)

        Col4.DataType = System.Type.GetType("System.Decimal")
        Col4.AllowDBNull = True
        Col4.Caption = "Days60"
        Col4.ColumnName = "Days60"
        Col4.DefaultValue = 0
        tblSummary.Columns.Add(Col4)

        Col5.DataType = System.Type.GetType("System.Decimal")
        Col5.AllowDBNull = True
        Col5.Caption = "Days90"
        Col5.ColumnName = "Days90"
        Col5.DefaultValue = 0
        tblSummary.Columns.Add(Col5)

        Col6.DataType = System.Type.GetType("System.Decimal")
        Col6.AllowDBNull = True
        Col6.Caption = "Days120"
        Col6.ColumnName = "Days120"
        Col6.DefaultValue = 0
        tblSummary.Columns.Add(Col6)

        Col7.DataType = System.Type.GetType("System.Decimal")
        Col7.AllowDBNull = True
        Col7.Caption = "Days120Abv"
        Col7.ColumnName = "Days120Abv"
        Col7.DefaultValue = 0
        tblSummary.Columns.Add(Col7)

        Col8.DataType = System.Type.GetType("System.Decimal")
        Col8.AllowDBNull = True
        Col8.Caption = "AmountDue"
        Col8.ColumnName = "AmountDue"
        Col8.DefaultValue = 0
        tblSummary.Columns.Add(Col8)

        Col9.DataType = System.Type.GetType("System.Decimal")
        Col9.AllowDBNull = True
        Col9.Caption = "NetBal"
        Col9.ColumnName = "NetBal"
        Col9.DefaultValue = 0
        tblSummary.Columns.Add(Col9)

    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamRptName")
        paramField2 = paramFields.Item("ParamDecimal")
        paramField3 = paramFields.Item("ParamSelPhyMonth")
        paramField4 = paramFields.Item("ParamSelPhyYear")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues

        ParamDiscreteValue1.Value = Request.QueryString("RptName")
        ParamDiscreteValue2.Value = Request.QueryString("Decimal")
        ParamDiscreteValue3.Value = UCase(objGlobal.GetLongMonth(strSelPhyMonth))
        ParamDiscreteValue4.Value = strSelPhyYear

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)

    End Sub
End Class
