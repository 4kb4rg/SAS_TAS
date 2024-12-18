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
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports System.Collections

Public Class WS_StdRpt_WorkshopIssueReturnSlipPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgReport As DataGrid
    Protected WithEvents lblErrMessage As Label
    
    Dim objWS As New agri.WS.clsReport()
    Dim objWSTrx As New agri.WS.clsTrx()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strUserId As String
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strLocationName As String
    Dim strPrintedBy As String
    Dim strPrintedByName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    
    Dim strRptID As String
    Dim strRptName As String
    Dim strRptLoc As String
    Dim strRptAccMonth As String
    Dim strRptAccYear As String
    Dim strRptDecimal As String
    Dim strRptJobIDFrom As String
    Dim strRptJobIDTo As String
    Dim strRptJobStartDateFrom As String
    Dim strRptJobStartDateTo As String
    Dim strRptJobType As String
    Dim strRptBillPartyCode As String
    Dim strRptEmpCode As String
    Dim strRptStatus As String
    Dim strLblLocation As String
    Dim strLblBillParty As String
    
    Dim rdCrystalViewer As ReportDocument
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strUserId = Session("SS_USERID")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strPrintedBy = Session("SS_USERID")
        strPrintedByName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            crvView.Visible = False
            
            strRptID = Trim(Request.QueryString("RptID"))
            strRptName = Trim(Request.QueryString("RptName"))
            strRptLoc = Trim(Request.QueryString("RptLocation"))
            strRptAccMonth = Trim(Request.QueryString("RptAccMonth"))
            strRptAccYear = Trim(Request.QueryString("RptAccYear"))
            strRptDecimal = Trim(Request.QueryString("Decimal"))
            strRptJobIDFrom = Trim(Request.QueryString("JobIDFrom"))
            strRptJobIDTo = Trim(Request.QueryString("JobIDTo"))
            strRptJobStartDateFrom = Trim(Request.QueryString("JobStartDateFrom"))
            strRptJobStartDateTo = Trim(Request.QueryString("JobStartDateTo"))
            strRptJobType = Trim(Request.QueryString("JobType"))
            strRptBillPartyCode = Trim(Request.QueryString("BillPartyCode"))
            strRptEmpCode = Trim(Request.QueryString("EmpCode"))
            strRptStatus = Trim(Request.QueryString("Status"))
            strLblLocation = Trim(Request.QueryString("lblLocation"))
            strLblBillParty = Trim(Request.QueryString("lblBillParty"))
            
            If Right(strRptLoc, 1) = "," Then
                strRptLoc = Left(strRptLoc, Len(strRptLoc) - 1)
            End If

            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim strRptPrefix As String = "WS_StdRpt_WorkshopIssueReturnSlip"
        Dim dsReport As New DataSet
        Dim strMapPath As String '= "C:\agri\Plantware\MY\TW\"
        Dim colParam As New Collection
        Dim intCnt As Integer
        
        colParam.Add(strRptLoc, "PARAM_LOCATION")
        colParam.Add(strRptAccMonth, "PARAM_ACCMONTH")
        colParam.Add(strRptAccYear, "PARAM_ACCYEAR")
        colParam.Add(strRptJobIDFrom, "PARAM_JOBIDFROM")
        colParam.Add(strRptJobIDTo, "PARAM_JOBIDTO")
        colParam.Add(strRptJobStartDateFrom, "PARAM_JOBSTARTDATEFROM")
        colParam.Add(strRptJobStartDateTo, "PARAM_JOBSTARTDATETO")
        colParam.Add(strRptJobType, "PARAM_JOBTYPE")
        colParam.Add(strRptBillPartyCode, "PARAM_BILLPARTYCODE")
        colParam.Add(strRptEmpCode, "PARAM_EMPCODE")
        colParam.Add(strRptStatus, "PARAM_STATUS")
        
        colParam.Add("WS_STDRPT_WORKSHOP_ISSUE_RETURN_SLIP_GET", "OC_REPORT_GET")
        
        Try
            intErrNo = objWS.mtdGetReport_WorkshopIssueReturnSlip(colParam, dsReport, strMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WS_STDRPT_WORKSHOP_ISSUE_RETURN_LISTING_GET&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=")
        End Try
        
        For intCnt = 0 To dsReport.Tables(0).Rows.Count - 1
            dsReport.Tables(0).Rows(intCnt).Item("TransType") = objWSTrx.mtdGetJobStockType(dsReport.Tables(0).Rows(intCnt).Item("TransType"))
            dsReport.Tables(0).Rows(intCnt).Item("Status") = objWSTrx.mtdGetJobStatus(dsReport.Tables(0).Rows(intCnt).Item("Status"))
            dsReport.Tables(0).Rows(intCnt).Item("ItemType") = objINSetup.mtdGetInventoryItemType(dsReport.Tables(0).Rows(intCnt).Item("ItemType"))
        Next

        
        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(strMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(dsReport.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

        PassParam()
        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = strMapPath & "web\ftp\" & strRptPrefix & ".pdf"

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
        Const PARAM_UBOUND As Integer = 6
        
        Dim intCnt As Integer
        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef(PARAM_UBOUND) As ParameterFieldDefinition
        Dim ParameterValues(PARAM_UBOUND) As ParameterValues
        Dim ParamDiscreteValue(PARAM_UBOUND) As ParameterDiscreteValue
        
        For intCnt = 0 To ParamFieldDef.Length - 1
            ParameterValues(intCnt) = New ParameterValues()
            ParamDiscreteValue(intCnt) = New ParameterDiscreteValue()
        Next
        
        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef(0) = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef(1) = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef(2) = ParamFieldDefs.Item("ParamCompany")
        ParamFieldDef(3) = ParamFieldDefs.Item("ParamPrintedBy")
        ParamFieldDef(4) = ParamFieldDefs.Item("ParamPrintedByName")
        ParamFieldDef(5) = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef(6) = ParamFieldDefs.Item("lblBillParty")
        
        ParamDiscreteValue(0).Value = strRptID
        ParamDiscreteValue(1).Value = strRptName
        ParamDiscreteValue(2).Value = strCompanyName
        ParamDiscreteValue(3).Value = strPrintedBy
        ParamDiscreteValue(4).Value = strPrintedByName
        ParamDiscreteValue(5).Value = CInt(strRptDecimal)
        ParamDiscreteValue(6).Value = strLblBillParty
        
        For intCnt = 0 To ParamFieldDef.Length - 1
            ParameterValues(intCnt) = ParamFieldDef(intCnt).CurrentValues
            ParameterValues(intCnt).Add(ParamDiscreteValue(intCnt))
            ParamFieldDef(intCnt).ApplyCurrentValues(ParameterValues(intCnt))
        Next
    End Sub
End Class
