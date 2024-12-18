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

Public Class PR_StdRpt_DailyAttendanceSummaryListPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents EventData As DataGrid

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objPRRpt As New agri.PR.clsReport()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objPWSysConfig As New agri.PWSystem.clsConfig()

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

    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strSelDecimal As String
    Dim strSelLocTag As String
    Dim strEmpFrom As String
    Dim strEmpTo As String
    Dim strEmpStatus As String
    
    Dim strEmpStatusText AS String
    Dim strSelLocCode As String
    Dim strLocTag As String
    Dim strCostLevel As String
    Dim strRptTitle As String
    Dim strReportID As String

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

        strSelLocCode = Trim(Request.QueryString("Location"))
        strSelAccMonth = Request.QueryString("DDLAccMth")
        strSelAccYear = Request.QueryString("DDLAccYr")
        strSelDecimal = Request.QueryString("Decimal")
        strEmpFrom = Request.QueryString("EmpFrom")
        strEmpTo = Request.QueryString("EmpTo")
        strEmpStatus = Request.QueryString("Status")

        strEmpStatusText = Trim(Request.QueryString("StatusText"))
        strLocTag = Trim(Request.QueryString("lblLocation"))
        strCostLevel = Trim(Request.QueryString("strCostLevel"))
        strReportID = Trim(Request.QueryString("strRptID"))
        strRptTitle = Trim(Request.QueryString("strRptTitle"))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim dtResult As New DataTable
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim tempLoc As String
        Dim intSelDecimal As Integer

        
        intSelDecimal = CInt(strSelDecimal)

        If Right(strSelLocCode, 1) = "," Then
            strSelLocCode = Left(strSelLocCode, Len(strSelLocCode) - 1)
        End If

        Try
            strRptPrefix = "PR_StdRpt_DailyAttendanceSummaryList"
            
            strOpCd = "PR_STDRPT_DAILY_ATTENDANCE_SUMMARY_LIST_SP"

            strParam = Replace(strSelLocCode, "'", "''") & "|" & _
                       strSelAccMonth & "|" & _
                       strSelAccYear & "|" & _
                       strEmpFrom & "|" & _
                       strEmpTo & "|" & _
                       strEmpStatus & "|" & _
                       strCostLevel & "|" 


            intErrNo = objPRRpt.mtdGetReport_DailyAttendanceSummaryList(strOpCd, _
                                                                        strCompany, _
                                                                        strLocation, _
                                                                        strUserId, _
                                                                        strAccMonth, _
                                                                        strAccYear, _
                                                                        strParam, _
                                                                        dtResult, _
                                                                        objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_DAILY_ATTENDANCE_SUMMARY_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
  

        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(dtResult)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                               

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
        dtResult = Nothing
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

        ParamDiscreteValue1.Value = strReportID
        ParamDiscreteValue2.Value = strRptTitle
        ParamDiscreteValue3.Value = strCompanyName
        ParamDiscreteValue4.Value = Replace(strSelLocCode, "','", ", ")
        ParamDiscreteValue5.Value = strPrintedBy
        ParamDiscreteValue6.Value = strSelAccMonth
        ParamDiscreteValue7.Value = strSelAccYear
        ParamDiscreteValue8.Value = strEmpFrom
        ParamDiscreteValue9.Value = strEmpTo
        ParamDiscreteValue10.Value = strEmpStatusText
        ParamDiscreteValue11.Value = strSelDecimal
        ParamDiscreteValue12.Value = strLocTag

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ReportID")
        ParamFieldDef2 = ParamFieldDefs.Item("ReportName")
        ParamFieldDef3 = ParamFieldDefs.Item("CompanyName")
        ParamFieldDef4 = ParamFieldDefs.Item("Location")
        ParamFieldDef5 = ParamFieldDefs.Item("PrintedBy")
        ParamFieldDef6 = ParamFieldDefs.Item("AccMonth")
        ParamFieldDef7 = ParamFieldDefs.Item("AccYear")
        ParamFieldDef8 = ParamFieldDefs.Item("EmpCodeFrom")
        ParamFieldDef9 = ParamFieldDefs.Item("EmpCodeTo")
        ParamFieldDef10 = ParamFieldDefs.Item("EmpStatus")
        ParamFieldDef11 = ParamFieldDefs.Item("DecimalNo")
        ParamFieldDef12 = ParamFieldDefs.Item("lblLocation")

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
    End Sub
End Class
