Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class PR_StdRpt_Payslip_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
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
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As String
    Dim strDeptCode As String
    Dim strEmpCodeFrom As String
    Dim strEmpCodeTo As String
    Dim strGangCode As String
    Dim strUserLoc As String
    
    Dim rdCrystalViewer As New ReportDocument()

    Sub Page_Load(Sender As Object, E As EventArgs)
        crvView.Visible = False  
 
        strUserLoc = Trim(Request.QueryString("Location"))
        strSelAccMonth = Request.QueryString("DDLAccMth")
        strSelAccYear = Request.QueryString("DDLAccYr")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strDeptCode = Request.QueryString("DeptCode")
        strEmpCodeFrom = Request.QueryString("EmpCodeFrom")
        strEmpCodeTo = Request.QueryString("EmpCodeTo")
        strGangCode = Request.QueryString("GangCode")
        
        Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End if
    End Sub



    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim strReportID As String

        strRptPrefix = "PR_StdRpt_Payslip2170"
        strReportID = "RPTPR1000016"

        strOpCd = "PR_STDRPT_PAYSLIP|" & objPRRpt.mtdGetPRReportTable(objPRRpt.EnumPRReportTable.Payslip) & Chr(9) & _
                  "GET_REPORT_INFO_BY_REPORTID|SH_REPORT"

        strParam = strUserLoc & "|" & _
                   strDeptCode & "|" & _
                   strEmpCodeFrom & "|" & _
                   strEmpCodeTo & "|" & _
                   strGangCode & "|" & _
                   strReportID & "|" & _
                   strSelAccMonth & "|" & _
                   strSelAccYear 



        Try
            intErrNo = objPRRpt.mtdGetReport_Payslip(strOpCd, strCompany, strLocation, strUserId, _
                                                     strAccMonth, strAccYear, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_GET_PAYSLIP&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        SetPageMargins()
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperLetter

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
    End Sub

    Sub SetPageMargins()
        
        Dim margins As PageMargins

        margins = rdCrystalViewer.PrintOptions.PageMargins
        margins.bottomMargin = 1

        rdCrystalViewer.PrintOptions.ApplyPageMargins(margins)

           
    End Sub























    Sub PassParam()
        Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        Dim crParameterFieldDefinition1 As ParameterFieldDefinition
        Dim crParameterFieldDefinition2 As ParameterFieldDefinition
        Dim crParameterFieldDefinition3 As ParameterFieldDefinition
        Dim crParameterFieldDefinition4 As ParameterFieldDefinition
        Dim crParameterFieldDefinition5 As ParameterFieldDefinition

        Dim crParameterValues1 As New ParameterValues()
        Dim crParameterValues2 as New ParameterValues()
        Dim crParameterValues3 as New ParameterValues()
        Dim crParameterValues4 as New ParameterValues()
        Dim crParameterValues5 as New ParameterValues()

        Dim crParameterDiscreteValue1 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue2 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue3 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue4 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue5 As New ParameterDiscreteValue()

        crParameterDiscreteValue1.Value = session("SS_CompanyName") 
        crParameterDiscreteValue2.Value = intSelDecimal 
        crParameterDiscreteValue3.Value = Session("SS_LBLLOCATION")
        crParameterDiscreteValue4.Value = strSelAccMonth
        crParameterDiscreteValue5.Value = strSelAccYear

        crParameterFieldDefinitions = rdCrystalViewer.DataDefinition.ParameterFields
        crParameterFieldDefinition1 = crParameterFieldDefinitions.Item("ParamCompanyName")
        crParameterFieldDefinition2 = crParameterFieldDefinitions.Item("ParamSelDecimal")
        crParameterFieldDefinition3 = crParameterFieldDefinitions.Item("lblLocation")
        crParameterFieldDefinition4 = crParameterFieldDefinitions.Item("ParamAccMonth")
        crParameterFieldDefinition5 = crParameterFieldDefinitions.Item("ParamAccYear")

        crParameterValues1 = crParameterFieldDefinition1.CurrentValues
        crParameterValues2 = crParameterFieldDefinition2.CurrentValues
        crParameterValues3 = crParameterFieldDefinition3.CurrentValues
        crParameterValues4 = crParameterFieldDefinition4.CurrentValues
        crParameterValues5 = crParameterFieldDefinition5.CurrentValues

        crParameterValues1.Add(crParameterDiscreteValue1)
        crParameterValues2.Add(crParameterDiscreteValue2)
        crParameterValues3.Add(crParameterDiscreteValue3)
        crParameterValues4.Add(crParameterDiscreteValue4)
        crParameterValues5.Add(crParameterDiscreteValue5)

        crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1)
        crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2)
        crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3)
        crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)
        crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5)
    End Sub

End Class
