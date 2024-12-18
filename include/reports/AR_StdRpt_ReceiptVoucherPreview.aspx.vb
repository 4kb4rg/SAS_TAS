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

Public Class AR_StdRpt_ReceiptVoucherPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objBIRpt As New agri.BI.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objBITrx As New agri.BI.clsTrx()

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



            strSearchExp = "  AND RCP.LocCode = '" & strUserLoc & "' " 
            If Trim(strStatus) <> Trim(CStr(objBITrx.EnumReceiptStatus.All)) Then
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
        dim totalRp as string

        strRptPrefix = "AR_StdRpt_ReceiptVoucher"
        strOpCd = "AR_STDRPT_RECEIPTVOUCHER_GET" & "|" & "AR_RECEIPT"

        strParam = strSelAccMonth & "|" & _
                   strSelAccYear & "|" & _
                   strSearchExp & "|"

        Try
            intErrNo = objBIRpt.mtdGetReport_ReceiptVoucher(strOpCd, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            strParam, _
                                                            objRptDs, _
                                                            objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_RECEIPTVOUCER_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
            totalrp = Trim(CStr(objRptDs.Tables(0).Rows(intCnt).Item("TotalAmount")))
            if totalrp>=0 then
            objRptDs.Tables(0).rows(intcnt).Item("Terbilang") = objGlobal.TerbilangDesimal(totalrp, "Rupiah")
            else
            objRptDs.Tables(0).rows(intcnt).Item("Terbilang") = "Negatif " & objGlobal.TerbilangDesimal(abs(cdbl(totalrp)), "Rupiah")
            end if            
        Next intCnt


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)



        PassParam()

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

    Sub PassParam()
        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParamFieldDef2 As ParameterFieldDefinition
        Dim ParamFieldDef3 As ParameterFieldDefinition
        Dim ParamFieldDef4 As ParameterFieldDefinition
        Dim ParamFieldDef5 As ParameterFieldDefinition

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()

        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strPrintedBy
        ParamDiscreteValue3.Value = strLocationName
        ParamDiscreteValue4.Value = AccCodeTag
        ParamDiscreteValue5.Value = BlkCodeTag

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SellerName")
        ParamFieldDef2 = ParamFieldDefs.Item("SessionUserName")
        ParamFieldDef3 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef4 = ParamFieldDefs.Item("AccCodeTag")
        ParamFieldDef5 = ParamFieldDefs.Item("BlkCodeTag")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)

    End Sub

End Class
