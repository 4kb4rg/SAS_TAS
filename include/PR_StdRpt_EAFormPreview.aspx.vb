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

Public Class PR_StdRpt_EAForm_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objPRDoc As New agri.PRDoc.clsEA()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim intSelDecimal As Integer
    Dim strEAYear As String
    Dim strAddress As String

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

        Session("SS_SerialFrom") = Request.QueryString("SerialFrom")
        Session("SS_SerialTo") = Request.QueryString("SerialTo")
        Session("SS_PeriodDateFrom") = Request.QueryString("PeriodDateFrom")
        Session("SS_PeriodDateTo") = Request.QueryString("PeriodDateTo")
        Session("SS_SignName") = Request.QueryString("SignName")
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
        End If
    End Sub

    Sub BindReport()
        Dim strStatus As String
        Dim strEAInd As String
        Dim strUserLoc As String
        Dim strTaxBranch As String
        Dim strEmpFrTo As String
        Dim strGangCode As String
        Dim strSerialFrTo As String
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

        strStatus = "HR_Employee.Status = '" & objHRTrx.EnumEmpStatus.Active & "' And "
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

        If Not (Request.QueryString("EmpCodeFrom") = "" And Request.QueryString("EmpCodeTo") = "") Then
            strEmpFrTo = "HR_Employee.EmpCode Between '" & Request.QueryString("EmpCodeFrom") & _
                         "' And '" & Request.QueryString("EmpCodeTo") & "' And "
        End If

        If Not Request.QueryString("GangCode") = "" Then
            strGangCode = "(HR_Employee.EmpCode in (Select ln.GangMember from HR_GANG G, HR_GANGLN ln Where G.GangCode = ln.GangCode and G.Status = '" & objHRSetup.EnumGangStatus.Active & "' and G.GangCode like '" & Request.QueryString("GangCode") & "') " & _
                          "Or HR_Employee.EmpCode in (Select G.GangLeader from HR_GANG G Where G.GangLeader <> '' And G.Status = '" & objHRSetup.EnumGangStatus.Active & "' and G.GangCode like '" & Request.QueryString("GangCode") & "')) And "
        End If

        If Not (Request.QueryString("SerialFrom") = "" And Request.QueryString("SerialTo") = "") Then
            strSerialFrTo = "HR_Payroll.Serial Between '" & Request.QueryString("SerialFrom") & _
                            "' And '" & Request.QueryString("SerialTo") & "' And "
        End If

        strSearch = strStatus & strEAInd & strUserLoc & strTaxBranch & strEmpFrTo  & strGangCode & strSerialFrTo
        strSorting = "Order By HR_Statutory.TaxBranch, HR_Payroll.Serial"

        If Not strSearch = "" Then
            If Right(strSearch, 4) = "And " Then
                 strSearch = Left(strSearch, Len(strSearch) - 4)
            End If
        
            strParam = "And " & strSearch & strSorting
        End If
        
        strRptPrefix = "PR_StdRpt_EAForm"

        strOpCd = "PRDOC_STDRPT_EMPLOYEE_EA|" & objPRDoc.mtdGetEAReportTable(objPRDoc.EnumEAReportTable.EA) & Chr(9) & _
                  "GET_REPORT_INFO_BY_REPORTID|SH_REPORT"

        strParamDs = strParam & "|RPTPR1000002|" & strEAYear

        Try
            intErrNo = objPRDoc.mtdGetReport_EAForm(strOpCd, strParamDs, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PRDOC_GET_EMPLOYEEEA&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

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

        Dim crParameterValues1 As New ParameterValues()
        Dim crParameterValues2 as New ParameterValues()
        Dim crParameterValues3 as New ParameterValues()
        Dim crParameterValues4 as New ParameterValues()
        Dim crParameterValues5 as New ParameterValues()
        Dim crParameterValues6 as New ParameterValues()
        Dim crParameterValues7 as New ParameterValues()

        Dim crParameterDiscreteValue1 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue2 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue3 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue4 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue5 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue6 As New ParameterDiscreteValue()
        Dim crParameterDiscreteValue7 As New ParameterDiscreteValue()

        crParameterDiscreteValue1.Value = session("SS_PeriodDateFrom")
        crParameterDiscreteValue2.Value = session("SS_PeriodDateTo")
        crParameterDiscreteValue3.Value = session("SS_SignName") 
        crParameterDiscreteValue4.Value = session("SS_SignDesignation") 
        crParameterDiscreteValue5.Value = session("SS_CompanyName") 
        crParameterDiscreteValue6.Value = intSelDecimal 
        crParameterDiscreteValue7.Value = strAddress 

        crParameterFieldDefinitions = rdCrystalViewer.DataDefinition.ParameterFields
        crParameterFieldDefinition1 = crParameterFieldDefinitions.Item("ParamPeriodDateFrom")
        crParameterFieldDefinition2 = crParameterFieldDefinitions.Item("ParamPeriodDateTo")
        crParameterFieldDefinition3 = crParameterFieldDefinitions.Item("ParamSignName")
        crParameterFieldDefinition4 = crParameterFieldDefinitions.Item("ParamSignDesignation")
        crParameterFieldDefinition5 = crParameterFieldDefinitions.Item("ParamCompanyName")
        crParameterFieldDefinition6 = crParameterFieldDefinitions.Item("ParamSelDecimal")
        crParameterFieldDefinition7 = crParameterFieldDefinitions.Item("ParamAddress")

        crParameterValues1 = crParameterFieldDefinition1.CurrentValues
        crParameterValues2 = crParameterFieldDefinition2.CurrentValues
        crParameterValues3 = crParameterFieldDefinition3.CurrentValues
        crParameterValues4 = crParameterFieldDefinition4.CurrentValues
        crParameterValues5 = crParameterFieldDefinition5.CurrentValues
        crParameterValues6 = crParameterFieldDefinition6.CurrentValues
        crParameterValues7 = crParameterFieldDefinition7.CurrentValues

        crParameterValues1.Add(crParameterDiscreteValue1)
        crParameterValues2.Add(crParameterDiscreteValue2)
        crParameterValues3.Add(crParameterDiscreteValue3)
        crParameterValues4.Add(crParameterDiscreteValue4)
        crParameterValues5.Add(crParameterDiscreteValue5)
        crParameterValues6.Add(crParameterDiscreteValue6)
        crParameterValues7.Add(crParameterDiscreteValue7)

        crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1)
        crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2)
        crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3)
        crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)
        crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5)
        crParameterFieldDefinition6.ApplyCurrentValues(crParameterValues6)
        crParameterFieldDefinition7.ApplyCurrentValues(crParameterValues7)
    End Sub

End Class
