Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
imports System.Math
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports agri.GlobalHdl.clsGlobalHdl

Public Class CB_StdRpt_ReceiptVoucherPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objCBRpt As New agri.CB.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objCBTrx As New agri.CB.clsTrx()

    Dim rdCrystalViewer As ReportDocument
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

    Dim strRptId As String
    Dim strRptName As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strSelSupress As String
    Dim strUserLoc As String
    Dim strFromReceiptId As String
    Dim strToReceiptId As String
    Dim strBillPartyCode As String
    Dim strStatus As String
    Dim strSearchExp As String = ""
    Dim BillPartyTag As String
    Dim AccCodeTag As String
    Dim BlkCodeTag As String
    Dim strMonth As String
    Dim strPrintDate As String
    Dim strVoucherNo As String
    Dim strProduct As String
    Dim strUOM As String
    Dim strContract As String
    Dim strUndName As String
    Dim strUndPost As String
    Dim strPPN As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        crvView.Visible = False

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Request.QueryString("SelAccMonth")
            strSelAccYear = Request.QueryString("SelAccYear")
            intSelDecimal = CInt(Request.QueryString("Decimal"))
            strSelSupress = Request.QueryString("Supp")

            strFromReceiptId = Trim(Request.QueryString("FromReceiptId"))
            strToReceiptId = Trim(Request.QueryString("ToReceiptId"))
            strBillPartyCode = Trim(Request.QueryString("BillPartyCode"))
            strStatus = Trim(Request.QueryString("Status"))
            BillPartyTag = Trim(Request.QueryString("BillPartyTag"))
            AccCodeTag = Trim(Request.QueryString("AccCodeTag"))
            BlkCodeTag = Trim(Request.QueryString("BlkCodeTag"))

            strVoucherNo = Request.QueryString("VoucherNo")
            strProduct = Request.QueryString("Product")
            strUOM = Request.QueryString("UOM")
            strContract = Request.QueryString("Contract")
            strUndName = Trim(Request.QueryString("UndName"))
            strUndPost = Trim(Request.QueryString("UndPost"))
            strPPN = Request.QueryString("PPN")


            strSearchExp = "  AND RCP.LocCode = '" & strUserLoc & "' "
            If Trim(strStatus) <> Trim(CStr(objCBTrx.EnumReceiptStatus.All)) Then
                strSearchExp = strSearchExp & " AND RCP.Status = '" & strStatus & "' "
            End If

            If Trim(strFromReceiptId) <> "" Then
                strSearchExp = strSearchExp & " AND RCP.ReceiptId >= '" & strFromReceiptId & "' "
            End If

            If Trim(strToReceiptId) <> "" Then
                strSearchExp = strSearchExp & " AND RCP.ReceiptId <= '" & strToReceiptId & "' "
            End If

            If Trim(strBillPartyCode) <> "" Then
                strSearchExp = strSearchExp & " AND RCP.BillPartyCode LIKE '" & strBillPartyCode & "' "
            End If

            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim strOpCdRslGet As String = ""
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim totalRp As Double
        Dim objFTPFolder As String

        strRptPrefix = "CB_StdRpt_ReceiptVoucher"
        strOpCd = "CB_STDRPT_RECEIPTVOUCHER_GET" & "|" & "CB_RECEIPT"

        strParam = strSelAccMonth & "|" & _
                   strSelAccYear & "|" & _
                   strSearchExp & "|"

        Try
            intErrNo = objCBRpt.mtdGetReport_ReceiptVoucher(strOpCd, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            strParam, _
                                                            objRptDs, _
                                                            objMapPath, objFTPFolder)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=CB_STDRPT_RECEIPTVOUCER_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            totalrp = Trim(CStr(objRptDs.Tables(0).Rows(intCnt).Item("TotalAmount")))
            if totalrp>=0 then
            objRptDs.Tables(0).rows(intcnt).Item("Terbilang") = objGlobal.TerbilangDesimal(totalrp, "Rupiah")
            else
            objRptDs.Tables(0).rows(intcnt).Item("Terbilang") = "Negatif " & objGlobal.TerbilangDesimal(abs(cdbl(totalrp)), "Rupiah")
            end if            
        Next intCnt

        If objRptDs.Tables(0).Rows.Count > 0 Then
            Select Case Month(objRptDs.Tables(0).Rows(0).Item("CreateDate"))
                Case 1
                    strMonth = "Januari"
                Case 2
                    strMonth = "Februari"
                Case 3
                    strMonth = "Maret"
                Case 4
                    strMonth = "April"
                Case 5
                    strMonth = "Mei"
                Case 6
                    strMonth = "Juni"
                Case 7
                    strMonth = "Juli"
                Case 8
                    strMonth = "Agustus"
                Case 9
                    strMonth = "September"
                Case 10
                    strMonth = "Oktober"
                Case 11
                    strMonth = "November"
                Case 12
                    strMonth = "Desember"
            End Select
            strPrintDate = Day(objRptDs.Tables(0).Rows(0).Item("CreateDate")) & " " & strMonth & " " & Year(objRptDs.Tables(0).Rows(0).Item("CreateDate"))
        Else
            strPrintDate = ""
        End If

        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf"

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
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".pdf"">")
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")

    End Sub

    Sub PassParam()
        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParamFieldDef2 As ParameterFieldDefinition
        Dim ParamFieldDef3 As ParameterFieldDefinition
        Dim ParamFieldDef4 As ParameterFieldDefinition
        Dim ParamFieldDef5 As ParameterFieldDefinition
        Dim ParamFieldDef6 As ParameterFieldDefinition
        Dim ParamFieldDef7 As ParameterFieldDefinition
        Dim ParamFieldDef8 As ParameterFieldDefinition
        Dim ParamFieldDef9 As ParameterFieldDefinition
        Dim ParamFieldDef10 As ParameterFieldDefinition
        Dim ParamFieldDef11 As ParameterFieldDefinition
        Dim ParamFieldDef12 As ParameterFieldDefinition
        Dim ParamFieldDef13 As ParameterFieldDefinition

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()
        Dim ParameterValues6 As New ParameterValues()
        Dim ParameterValues7 As New ParameterValues()
        Dim ParameterValues8 As New ParameterValues()
        Dim ParameterValues9 As New ParameterValues()
        Dim ParameterValues10 As New ParameterValues()
        Dim ParameterValues11 As New ParameterValues()
        Dim ParameterValues12 As New ParameterValues()
        Dim ParameterValues13 As New ParameterValues()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue8 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue9 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue10 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue11 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue12 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue13 As New ParameterDiscreteValue()

        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strPrintedBy
        ParamDiscreteValue3.Value = strLocationName
        ParamDiscreteValue4.Value = AccCodeTag
        ParamDiscreteValue5.Value = BlkCodeTag
        ParamDiscreteValue6.Value = strVoucherNo
        ParamDiscreteValue7.Value = strUOM
        ParamDiscreteValue8.Value = strPPN
        ParamDiscreteValue9.Value = strPrintDate
        ParamDiscreteValue10.Value = strUndPost
        ParamDiscreteValue11.Value = strUndName
        ParamDiscreteValue12.Value = strProduct
        ParamDiscreteValue13.Value = strContract

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SellerName")
        ParamFieldDef2 = ParamFieldDefs.Item("SessionUserName")
        ParamFieldDef3 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef4 = ParamFieldDefs.Item("AccCodeTag")
        ParamFieldDef5 = ParamFieldDefs.Item("BlkCodeTag")
        ParamFieldDef6 = ParamFieldDefs.Item("VoucherNo")
        ParamFieldDef7 = ParamFieldDefs.Item("UOM")
        ParamFieldDef8 = ParamFieldDefs.Item("PPN")
        ParamFieldDef9 = ParamFieldDefs.Item("PrintDate")
        ParamFieldDef10 = ParamFieldDefs.Item("UndersignedPost")
        ParamFieldDef11 = ParamFieldDefs.Item("UndersignedName")
        ParamFieldDef12 = ParamFieldDefs.Item("Product")
        ParamFieldDef13 = ParamFieldDefs.Item("Contract")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues
        ParameterValues6 = ParamFieldDef6.CurrentValues
        ParameterValues7 = ParamFieldDef7.CurrentValues
        ParameterValues8 = ParamFieldDef8.CurrentValues
        ParameterValues9 = ParamFieldDef9.CurrentValues
        ParameterValues10 = ParamFieldDef10.CurrentValues
        ParameterValues11 = ParamFieldDef11.CurrentValues
        ParameterValues12 = ParamFieldDef12.CurrentValues
        ParameterValues13 = ParamFieldDef13.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)
        ParameterValues6.Add(ParamDiscreteValue6)
        ParameterValues7.Add(ParamDiscreteValue7)
        ParameterValues8.Add(ParamDiscreteValue8)
        ParameterValues9.Add(ParamDiscreteValue9)
        ParameterValues10.Add(ParamDiscreteValue10)
        ParameterValues11.Add(ParamDiscreteValue11)
        ParameterValues12.Add(ParamDiscreteValue12)
        ParameterValues13.Add(ParamDiscreteValue13)

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)
        ParamFieldDef6.ApplyCurrentValues(ParameterValues6)
        ParamFieldDef7.ApplyCurrentValues(ParameterValues7)
        ParamFieldDef8.ApplyCurrentValues(ParameterValues8)
        ParamFieldDef9.ApplyCurrentValues(ParameterValues9)
        ParamFieldDef10.ApplyCurrentValues(ParameterValues10)
        ParamFieldDef11.ApplyCurrentValues(ParameterValues11)
        ParamFieldDef12.ApplyCurrentValues(ParameterValues12)
        ParamFieldDef13.ApplyCurrentValues(ParameterValues13)

    End Sub

End Class
