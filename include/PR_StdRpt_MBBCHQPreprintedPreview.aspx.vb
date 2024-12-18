Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class PR_StdRpt_MBBCHQPreprintedPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objPRRpt As New agri.PR.clsReport()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objPRTrx As New agri.PR.clsTrx()
    Dim objPWSysConfig As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strLocationName As String
    Dim strUserId As String
    Dim strPrintedBy As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strAcceptFormat As String

    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strUserLoc As String
    Dim intSelDecimal As String
    Dim strBankCode As String
    Dim strCompCode As String
    Dim strDeptCode As String
    Dim strEmpCodeFrom As String
    Dim strEmpCodeTo As String
    Dim strTerminateDateFr As String
    Dim strTerminateDateTo As String
    Dim strStartChequeNo As String
    Dim strNoOfCheque As String

    Dim rdCrystalViewer As ReportDocument

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        crvView.Visible = False

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        strSelAccMonth = Request.QueryString("DdlAccMth")
        strSelAccYear = Request.QueryString("DdlAccYr")
        strUserLoc = Trim(Request.QueryString("Location"))
        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strBankCode = Trim(Request.QueryString("BankCode"))
        strCompCode = Trim(Request.QueryString("CompCode"))
        strDeptCode = Trim(Request.QueryString("DeptCode"))
        strEmpCodeFrom = Request.QueryString("EmpCodeFrom")
        strEmpCodeTo = Request.QueryString("EmpCodeTo")
        strTerminateDateFr = Request.QueryString("TerminateDateFr")
        strTerminateDateTo = Request.QueryString("TerminateDateTo")
        strStartChequeNo = Request.QueryString("StartChequeNo")
        strNoOfCheque = Request.QueryString("NoOfCheque")

        Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            Bind_PR_Cheque_Print()

        End If
    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object()
        Dim objActualDate As New Object()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_GRList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

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
        End if
    End Function

    Sub Bind_PR_Cheque_Print()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim strReportID As String
        Dim tempLoc As String

        Dim intCnt As Integer
        Dim strTotalAmount As String = ""
        Dim arrTotalAmount(15) As String
        Dim strBeforeDec As String = ""
        Dim strAfterDec As String = ""

        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strTable As String = "PR_WAGES"
        Dim intChequeNo As Integer = CInt(strStartChequeNo)
        Dim strStatus As String
        Dim strPrintDate As String
        Dim strUpdString As String

        Dim dr As DataRow

        strRptPrefix = "PR_StdRpt_MBBCHQ_Preprinted"
        strReportID = "RPTPR1000022"

        If Request.QueryString("trx") = "ap_trx_payment" Then

            objMapPath = Request.QueryString("objMapPath")

            Dim tbl As DataTable = New DataTable(objPRRpt.mtdGetPRReportTable(objPRRpt.EnumPRReportTable.Cheque))
            
            Dim Col1 As DataColumn = New DataColumn()
            Dim Col2 As DataColumn = New DataColumn()
            Dim Col3 As DataColumn = New DataColumn()

            Col1.DataType = System.Type.GetType("System.String")
            Col1.AllowDBNull = True
            Col1.Caption = "EmpName"
            Col1.ColumnName = "EmpName"
            Col1.DefaultValue = ""
            tbl.Columns.Add(Col1)

            Col2.DataType = System.Type.GetType("System.Decimal")
            Col2.AllowDBNull = True
            Col2.Caption = "ChequeAmount"
            Col2.ColumnName = "ChequeAmount"
            Col2.DefaultValue = 0
            tbl.Columns.Add(Col2)

            Col3.DataType = System.Type.GetType("System.String")
            Col3.AllowDBNull = True
            Col3.Caption = "ChequeWords"
            Col3.ColumnName = "ChequeWords"
            Col3.DefaultValue = ""
            tbl.Columns.Add(Col3)

            dr = tbl.NewRow()
            dr("EmpName") = Request.QueryString("SupplierName") 'Request.QueryString("SupplierCode")
            dr("ChequeAmount") = Request.QueryString("TotalAmount")
            dr("ChequeWords") = ""
            tbl.Rows.Add(dr)

            objRptDs.Tables.Add(tbl)

        Else

            If Right(strUserLoc, 1) = "," Then
                Session("SS_LOC") = Left(strUserLoc, Len(strUserLoc) - 1)
            Else
                Session("SS_LOC") = strUserLoc.Replace("'", "")
            End If

            Try

                strOpCd = "PR_STDRPT_WAGES_GET_CHQ" & "|" & objPRRpt.mtdGetPRReportTable(objPRRpt.EnumPRReportTable.Cheque) & Chr(9) & _
                          "GET_REPORT_INFO_BY_REPORTID|SH_REPORT"

                strParam = strSelAccMonth & "|" & _
                           strSelAccYear & "|" & _
                           strUserLoc & "||" & _
                           strCompCode & "|" & _
                           strDeptCode & "|" & _
                           strEmpCodeFrom & "|" & _
                           strEmpCodeTo & "|" & _
                           objGlobal.GetLongDate(strTerminateDateFr) & "|" & _
                           objGlobal.GetLongDate(strTerminateDateTo) & "|" & _
                           strNoOfCheque & "|" & _
                           objPRTrx.EnumWagesStatus.Active & "|" & _
                           objHRTrx.EnumPayMode.Cheque & "|" & _
                           strReportID

                intErrNo = objPRRpt.mtdGetReport_Cheque(strOpCd, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        objRptDs, _
                                                        objMapPath)

            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_CHEQUE&errmesg=" & Exp.ToString() & "&redirect=")
            End Try
        End If

        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
                arrTotalAmount = Split(objRptDs.Tables(0).Rows(intCnt).Item("ChequeAmount"), ".")

                strBeforeDec = objGlobal.ConvertNo2Words(CInt(arrTotalAmount(0)))
                If UBound(arrTotalAmount) > 0 Then
                    If Len(CStr(arrTotalAmount(1))) = 1 Then
                        strAfterDec = objGlobal.ConvHund(CInt(arrTotalAmount(1)) & "0")
                    Else
                        strAfterDec = objGlobal.ConvHund(CInt(arrTotalAmount(1)))
                    End If
                    If LCase(Trim(strAfterDec)) = "zero" Then
                        strTotalAmount = strBeforeDec & "Only"
                    Else
                        strTotalAmount = strBeforeDec & "and Cents " & strAfterDec & "Only"
                    End If
                Else
                    strTotalAmount = strBeforeDec & "Only"
                End If

                If Request.QueryString("trx") = "ap_trx_payment" Then
                    objRptDs.Tables(0).Rows(0).BeginEdit()
                    objRptDs.Tables(0).Rows(0).Item("ChequeWords") = strTotalAmount
                    objRptDs.Tables(0).Rows(0).EndEdit()

                Else
                    objRptDs.Tables(0).Rows(intCnt).Item("ChequeWords") = strTotalAmount
                End If

                If Not Request.QueryString("trx") = "ap_trx_payment" Then
                    strStatus = objRptDs.Tables(0).Rows(intCnt).Item("Status").Trim()
                    strPrintDate = objRptDs.Tables(0).Rows(intCnt).Item("PrintDate")

                    If CStr(objRptDs.Tables(0).Rows(intCnt).Item("PrintDate")) <> "" Then
                        strPrintDate = Date_Validation(objRptDs.Tables(0).Rows(intCnt).Item("PrintDate"), False)
                    End If

                    strUpdString = ", BankCode = '" & strBankCode & "' " & _
                                    ", ChequeNo = '" & CStr(intChequeNo) & "' " & _
                                    ", Status = '" & objPRTrx.EnumWagesStatus.Printed & "' " & _
                                    "Where WagesID = '" & objRptDs.Tables(0).Rows(intCnt).Item("WagesID") & "' " & _
                                    "And AccMonth = '" & objRptDs.Tables(0).Rows(intCnt).Item("AccMonth").Trim() & "' " & _
                                    "And AccYear = '" & objRptDs.Tables(0).Rows(intCnt).Item("AccYear").Trim() & "' " & _
                                    "And CompCode = '" & objRptDs.Tables(0).Rows(intCnt).Item("CompCode").Trim() & "' " & _
                                    "And LocCode = '" & objRptDs.Tables(0).Rows(intCnt).Item("LocCode").Trim() & "' " & _
                                    "And EmpCode = '" & objRptDs.Tables(0).Rows(intCnt).Item("EmpCode").Trim() & "' "

                    If strStatus = objPRTrx.EnumWagesStatus.Active Then
                        If strPrintDate = "" Then
                            Try
                                intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                                    strUpdString, _
                                                                    strTable, _
                                                                    strCompany, _
                                                                    strLocation, _
                                                                    strUserId)
                            Catch Exp As System.Exception
                                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CHQ_UPDPRINTDATE_PO&errmesg=" & lblErrMessage.Text & "&redirect=")
                            End Try
                        End If
                    End If
                    intChequeNo = intChequeNo + 1
                End If
            Next
        End If


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperFanfoldStdGerman


        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
    End Sub

End Class
