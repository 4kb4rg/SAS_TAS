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

Public Class PR_StdRpt_StmtTaxDeduct_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objAdminComp As New agri.Admin.clsComp()
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
    Dim strAddress As String
    Dim strTelNo As String
    Dim strFaxNo As String

    Dim intCnt As Integer

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim intCount As Integer

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
        
        Session("SS_PeriodEnd") = Request.QueryString("PeriodEnd")
        Session("SS_RefNo") = Request.QueryString("RefNo")
        Session("SS_NatureBuss") = Request.QueryString("NatureBuss")
        Session("SS_SignName") = Request.QueryString("SignName")
        Session("SS_SignNRIC") = Request.QueryString("SignNRIC")
        Session("SS_SignDesignation") = Request.QueryString("SignDesignation")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            GetCompanyAddress()
            BindReport()
        End if
    End Sub

    Sub GetCompanyAddress()
        Dim objCompDs As New Object()
        Dim strOpCd_Get As String = "ADMIN_CLSCOMP_COMPANY_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        strParam = strCompany & "||" & objAdminComp.EnumCompanyStatus.Active & "||CompCode|" 

        Try
            intErrNo = objAdminComp.mtdGetComp(strOpCd_Get, strParam, objCompDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_COMPANY&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objCompDs.Tables(0).Rows.Count > 0 Then
            strAddress = Trim(objCompDs.Tables(0).Rows(0).Item("Address"))
            strTelNo = Trim(objCompDs.Tables(0).Rows(0).Item("TelNo"))
            strFaxNo = Trim(objCompDs.Tables(0).Rows(0).Item("FaxNo"))
        End If
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
        
        strRptPrefix = "PR_StdRpt_StmtTaxDeduct"

        strOpCd = "PRDOC_STDRPT_EMPLOYEE_EA|" & objPRDoc.mtdGetEAReportTable(objPRDoc.EnumEAReportTable.EA) & Chr(9) & _
                  "GET_REPORT_INFO_BY_REPORTID|SH_REPORT"

        strParamDs = strParam & "|RPTPR1000006|" & strEAYear

        Try
            intErrNo = objPRDoc.mtdGetReport_STMTForm(strOpCd, strParamDs, objRptDs, intCount, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PRDOC_GET_EMPLOYEESTMT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Dim x as integer



        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

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

    End Sub

    Sub PassParam()
        Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        Dim crParameterFieldDefinition1 As ParameterFieldDefinition
        Dim crParameterFieldDefinition2 As ParameterFieldDefinition
        Dim crParameterFieldDefinition3 As ParameterFieldDefinition
        Dim crParameterFieldDefinition4 As ParameterFieldDefinition
        Dim crParameterFieldDefinition5 As ParameterFieldDefinition
        Dim crParameterFieldDefinition6 As ParameterFieldDefinition
        Dim crParameterFieldDefinition7 As ParameterFieldDefinition
        Dim crParameterFieldDefinition8 As ParameterFieldDefinition
        Dim crParameterFieldDefinition9 As ParameterFieldDefinition
        Dim crParameterFieldDefinition10 As ParameterFieldDefinition
        Dim crParameterFieldDefinition11 As ParameterFieldDefinition
        Dim crParameterFieldDefinition12 As ParameterFieldDefinition

        Dim crParameterValues1 As New ParameterValues()
        Dim crParameterValues2 As New ParameterValues()
        Dim crParameterValues3 As New ParameterValues()
        Dim crParameterValues4 As New ParameterValues()
        Dim crParameterValues5 As New ParameterValues()
        Dim crParameterValues6 As New ParameterValues()
        Dim crParameterValues7 As New ParameterValues()
        Dim crParameterValues8 As New ParameterValues()
        Dim crParameterValues9 As New ParameterValues()
        Dim crParameterValues10 As New ParameterValues()
        Dim crParameterValues11 As New ParameterValues()
        Dim crParameterValues12 As New ParameterValues()

        Dim crParameterDiscreteValue1 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue2 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue3 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue4 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue5 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue6 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue7 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue8 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue9 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue10 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue11 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue12 As New ParameterDiscreteValue()

        crParameterDiscreteValue1.Value = session("SS_PeriodEnd")
        crParameterDiscreteValue2.Value = session("SS_RefNo")
        crParameterDiscreteValue3.Value = session("SS_NatureBuss") 
        crParameterDiscreteValue4.Value = session("SS_SignName") 
        crParameterDiscreteValue5.Value = session("SS_SignNRIC") 
        crParameterDiscreteValue6.Value = session("SS_SignDesignation") 
        crParameterDiscreteValue7.Value = session("SS_CompanyName") 
        crParameterDiscreteValue8.Value = intSelDecimal 
        crParameterDiscreteValue9.Value = strAddress 
        crParameterDiscreteValue10.Value = strTelNo 
        crParameterDiscreteValue11.Value = strFaxNo 
        crParameterDiscreteValue12.Value = intCount

        crParameterFieldDefinitions = rdCrystalViewer.DataDefinition.ParameterFields
        crParameterFieldDefinition1 = crParameterFieldDefinitions.Item("ParamPeriodEnd")
        crParameterFieldDefinition2 = crParameterFieldDefinitions.Item("ParamRefNo")
        crParameterFieldDefinition3 = crParameterFieldDefinitions.Item("ParamNatureBuss")
        crParameterFieldDefinition4 = crParameterFieldDefinitions.Item("ParamSignName")
        crParameterFieldDefinition5 = crParameterFieldDefinitions.Item("ParamSignNRIC")
        crParameterFieldDefinition6 = crParameterFieldDefinitions.Item("ParamSignDesignation")
        crParameterFieldDefinition7 = crParameterFieldDefinitions.Item("ParamCompanyName")
        crParameterFieldDefinition8 = crParameterFieldDefinitions.Item("ParamSelDecimal")
        crParameterFieldDefinition9 = crParameterFieldDefinitions.Item("ParamAddress")
        crParameterFieldDefinition10 = crParameterFieldDefinitions.Item("ParamTelNo")
        crParameterFieldDefinition11 = crParameterFieldDefinitions.Item("ParamFaxNo")
        crParameterFieldDefinition12 = crParameterFieldDefinitions.Item("ParamCount")

        crParameterValues1 = crParameterFieldDefinition1.CurrentValues
        crParameterValues2 = crParameterFieldDefinition2.CurrentValues
        crParameterValues3 = crParameterFieldDefinition3.CurrentValues
        crParameterValues4 = crParameterFieldDefinition4.CurrentValues
        crParameterValues5 = crParameterFieldDefinition5.CurrentValues
        crParameterValues6 = crParameterFieldDefinition6.CurrentValues
        crParameterValues7 = crParameterFieldDefinition7.CurrentValues
        crParameterValues8 = crParameterFieldDefinition8.CurrentValues
        crParameterValues9 = crParameterFieldDefinition9.CurrentValues
        crParameterValues10 = crParameterFieldDefinition10.CurrentValues
        crParameterValues11 = crParameterFieldDefinition11.CurrentValues
        crParameterValues12 = crParameterFieldDefinition12.CurrentValues

        crParameterValues1.Add(crParameterDiscreteValue1)
        crParameterValues2.Add(crParameterDiscreteValue2)
        crParameterValues3.Add(crParameterDiscreteValue3)
        crParameterValues4.Add(crParameterDiscreteValue4)
        crParameterValues5.Add(crParameterDiscreteValue5)
        crParameterValues6.Add(crParameterDiscreteValue6)
        crParameterValues7.Add(crParameterDiscreteValue7)
        crParameterValues8.Add(crParameterDiscreteValue8)
        crParameterValues9.Add(crParameterDiscreteValue9)
        crParameterValues10.Add(crParameterDiscreteValue10)
        crParameterValues11.Add(crParameterDiscreteValue11)
        crParameterValues12.Add(crParameterDiscreteValue12)

        crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1)
        crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2)
        crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3)
        crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)
        crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5)
        crParameterFieldDefinition6.ApplyCurrentValues(crParameterValues6)
        crParameterFieldDefinition7.ApplyCurrentValues(crParameterValues7)
        crParameterFieldDefinition8.ApplyCurrentValues(crParameterValues8)
        crParameterFieldDefinition9.ApplyCurrentValues(crParameterValues9)
        crParameterFieldDefinition10.ApplyCurrentValues(crParameterValues10)
        crParameterFieldDefinition11.ApplyCurrentValues(crParameterValues11)
        crParameterFieldDefinition12.ApplyCurrentValues(crParameterValues12)
    End Sub

End Class
