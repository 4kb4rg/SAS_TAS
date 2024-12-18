Imports System
Imports System.Data
Imports System.Collections 
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

Public Class PR_StdRpt_RetRemu_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objHR As New agri.HR.clsTrx()
    Dim objPRDoc As New agri.PRDoc.clsEA()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth as String
    Dim strAccYear as String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim intSelDecimal As Integer
    Dim strEAYear As String

    Dim intCnt As Integer

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        crvView.Visible = False  
 
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strEAYear = Request.QueryString("EAYear")
        

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End if
    End Sub

    Sub BindReport()
        Dim strStatus As String
        Dim strEAInd As String
        Dim strUserLoc As String
        Dim strTaxBranch As String
        Dim strSearch As String
        Dim strSorting As String
        Dim strParam As String

        Dim strRptPrefix As String
        Dim strOpCd As String
        Dim strParamDs As String

        Dim objRptDs As New DataSet()
        Dim objMapPath As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        strStatus = "HR_Employee.Status = '" & objHR.EnumEmpStatus.Active & "' And "
        strEAInd = "HR_Payroll.EAInd <> 0 And "

        If Not Request.QueryString("Location") = "" Then
            strUserLoc = "HR_Employment.LocCode IN ('" & Request.QueryString("Location") & "') And "
        Else
            strUserLoc = "HR_Employment.LocCode LIKE '%' And "
        End If

        If Not Request.QueryString("TaxBranch") = "" Then
            strTaxBranch = "HR_Statutory.TaxBranch = '" & Request.QueryString("TaxBranch") & "' And "
        Else
            strTaxBranch = "HR_Statutory.TaxBranch LIKE '%' And "
        End If

        strSearch = strStatus & strEAInd & strUserLoc & strTaxBranch
        strSorting = "Order By HR_Statutory.TaxBranch, HR_Payroll.Serial"

        If Not strSearch = "" Then
            If Right(strSearch, 4) = "And " Then
                 strSearch = Left(strSearch, Len(strSearch) - 4)
            End If
        
            strParam = "And " & strSearch & strSorting
        End If
        
        strRptPrefix = "PR_StdRpt_RetRemu"

        strOpCd = "PRDOC_STDRPT_EMPLOYEE_EA|" & objPRDoc.mtdGetEAReportTable(objPRDoc.EnumEAReportTable.EA) & Chr(9) & _
                  "GET_REPORT_INFO_BY_REPORTID|SH_REPORT" & Chr(9) & _
                  "PRDOC_STDRPT_EMPLOYEE_REMU|" & objPRDoc.mtdGetEAReportTable(objPRDoc.EnumEAReportTable.Remu)

        strParamDs = strParam & "|RPTPR1000005|" & strEAYear

        Try
            intErrNo = objPRDoc.mtdGetReport_REMUForm(strOpCd, strParamDs, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PRDOC_GET_EMPLOYEEREMU&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

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

    Sub PassParam()
        Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        Dim crParameterFieldDefinition1 As ParameterFieldDefinition

        Dim crParameterValues1 As New ParameterValues()

        Dim crParameterDiscreteValue1 As New ParameterDiscreteValue()

        crParameterDiscreteValue1.Value = intSelDecimal

        crParameterFieldDefinitions = rdCrystalViewer.DataDefinition.ParameterFields
        crParameterFieldDefinition1 = crParameterFieldDefinitions.Item("ParamSelDecimal")

        crParameterValues1 = crParameterFieldDefinition1.CurrentValues

        crParameterValues1.Add(crParameterDiscreteValue1)

        crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1)
    End Sub

End Class
