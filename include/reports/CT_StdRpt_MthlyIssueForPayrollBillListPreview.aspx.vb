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

Public Class CT_StdRpt_MthlyIssueForPayrollBillList_Preview : Inherits Page
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objCT As New agri.CT.clsReport()
    Dim objCTSetup As New agri.CT.clsSetup()
    Dim objCTTrx As New agri.CT.clsTrx()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim intDecimal As Integer
    Dim tempLoc As String
    Dim strQueryDisplay As String
    Dim strUserLoc As String

    Dim strDisplay As String
    Dim strSelect As String = ""
    Dim strFrom As String = ""
    Dim strWhere As String = ""
    Dim strGroupBy As String = ""
    Dim strOrderDir As String = ""
    Dim strIssueType As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Dim tblTotal As DataTable = New DataTable("tblTotal")
    Dim dr As DataRow

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

        intDecimal = Request.QueryString("Decimal")
        strQueryDisplay = Request.QueryString("Display")

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub
    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objMapPath As String

        Dim intCnt As Integer

        Dim SearchStr As String
        Dim strParam As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd_MthlyIssueTransPayBill_GET As String = "CT_STDRPT_MONTHLY_ISSUE_TRANSFER_PAYROLL_BILLING_LIST_GET"

        If strQueryDisplay = "emp" Then
            strDisplay = "Employee Code"
            strSelect = "SI.PsEmpCode AS Code, EMP.EmpName AS Name,"
            strFrom = "HR_EMPLOYEE EMP ON EMP.EmpCode = SI.PsEmpCode"
            If Not Trim(Request.QueryString("EmpCode")) = "" Then
                strWhere = "AND SI.PsEmpCode LIKE '" & Trim(Request.QueryString("EmpCode")) & "'"
            End If
            strGroupBy = "SI.PsEmpCode, EMP.EmpName"
            strOrderDir = Trim(Request.QueryString("OrderBy"))
            strIssueType = objCTTrx.EnumStockIssueType.StaffPayroll
        ElseIf strQueryDisplay = "bill" Then
            strDisplay = UCase(Left(Request.QueryString("lblBillPartyCode"), Len(Request.QueryString("lblBillPartyCode")) - 6))
            strSelect = "SI.BillPartyCode AS Code, BP.Name AS Name,"
            strFrom = "GL_BILLPARTY BP ON BP.BillPartyCode = SI.BillPartyCode"
            If Not Trim(Request.QueryString("BillPartyCode")) = "" Then
                strWhere = "AND SI.BillPartyCode LIKE '" & Trim(Request.QueryString("BillPartyCode")) & "'"
            End If
            strGroupBy = "SI.BillPartyCode, BP.Name"
            strOrderDir = Trim(Request.QueryString("OrderBy"))
            strIssueType = objCTTrx.EnumStockIssueType.External
        End If
        strParam = strUserLoc & "|" & _
                   Request.QueryString("DDLAccMth") & "|" & _
                   Request.QueryString("DDLAccYr") & "|" & _
                   strIssueType & "|" & _
                   objCTTrx.EnumStockIssueStatus.Confirmed & "','" & objCTTrx.EnumStockIssueStatus.Closed & "','" & objCTTrx.EnumStockIssueStatus.DBNote & "|" & _
                   objCTTrx.EnumStockReturnStatus.Confirmed & "','" & objCTTrx.EnumStockReturnStatus.Closed & "|" & _
                   strSelect & "|" & strFrom & "|" & strWhere & "|" & strGroupBy & "|" & strOrderDir
                   
        Try
            intErrNo = objCT.mtdGetReport_MthlyIssueForPayrollBillList(strOpCd_MthlyIssueTransPayBill_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_MTHLY_ISSUE_FOR_PAYROLLBILL_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try






        objRptDs.Tables(0).TableName = "CT_PAYROLLBILL"


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\CT_StdRpt_MthlyIssueForPayrollBillList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables("CT_PAYROLLBILL"))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\CT_StdRpt_MthlyIssueForPayrollBillList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/CT_StdRpt_MthlyIssueForPayrollBillList.pdf"">")
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()
        Dim paramField5 As New ParameterField()
        Dim paramField6 As New ParameterField()
        Dim paramField7 As New ParameterField()
        Dim paramField8 As New ParameterField()
        Dim paramField9 As New ParameterField()
        Dim paramField10 As New ParameterField()
        Dim paramField11 As New ParameterField()
        Dim paramField12 As New ParameterField()
        Dim paramField13 As New ParameterField()
        Dim paramField14 As New ParameterField()
        Dim paramField15 As New ParameterField()

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
        Dim ParamDiscreteValue14 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue15 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues
        Dim crParameterValues8 As ParameterValues
        Dim crParameterValues9 As ParameterValues
        Dim crParameterValues10 As ParameterValues
        Dim crParameterValues11 As ParameterValues
        Dim crParameterValues12 As ParameterValues
        Dim crParameterValues13 As ParameterValues
        Dim crParameterValues14 As ParameterValues
        Dim crParameterValues15 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamLocation")
        paramField2 = paramFields.Item("ParamCompanyName")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptID")
        paramField6 = paramFields.Item("ParamRptName")
        paramField7 = paramFields.Item("ParamAccMonth")
        paramField8 = paramFields.Item("ParamAccYear")
        paramField9 = paramFields.Item("lblLocation")
        paramField10 = paramFields.Item("ParamDisplayText")
        paramField11 = paramFields.Item("lblBillPartyCode")
        paramField12 = paramFields.Item("ParamOrderByText")
        paramField13 = paramFields.Item("ParamEmpCode")
        paramField14 = paramFields.Item("ParamBillPartyCode")
        paramField15 = paramFields.Item("ParamDisplay")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues
        crParameterValues7 = paramField7.CurrentValues
        crParameterValues8 = paramField8.CurrentValues
        crParameterValues9 = paramField9.CurrentValues
        crParameterValues10 = paramField10.CurrentValues
        crParameterValues11 = paramField11.CurrentValues
        crParameterValues12 = paramField12.CurrentValues
        crParameterValues13 = paramField13.CurrentValues
        crParameterValues14 = paramField14.CurrentValues
        crParameterValues15 = paramField15.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_LOC")
        ParamDiscreteValue2.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("RptID")
        ParamDiscreteValue6.Value = Request.QueryString("RptName")
        ParamDiscreteValue7.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue8.Value = Request.QueryString("DDLAccYr")
        ParamDiscreteValue9.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue10.Value = strDisplay
        ParamDiscreteValue11.Value = Request.QueryString("lblBillPartyCode")
        ParamDiscreteValue12.Value = Request.QueryString("OrderByText")
        ParamDiscreteValue13.Value = Request.QueryString("EmpCode")
        ParamDiscreteValue14.Value = Request.QueryString("BillPartyCode")
        ParamDiscreteValue15.Value = Request.QueryString("Display")

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)
        crParameterValues7.Add(ParamDiscreteValue7)
        crParameterValues8.Add(ParamDiscreteValue8)
        crParameterValues9.Add(ParamDiscreteValue9)
        crParameterValues10.Add(ParamDiscreteValue10)
        crParameterValues11.Add(ParamDiscreteValue11)
        crParameterValues12.Add(ParamDiscreteValue12)
        crParameterValues13.Add(ParamDiscreteValue13)
        crParameterValues14.Add(ParamDiscreteValue14)
        crParameterValues15.Add(ParamDiscreteValue15)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)
        PFDefs(6).ApplyCurrentValues(crParameterValues7)
        PFDefs(7).ApplyCurrentValues(crParameterValues8)
        PFDefs(8).ApplyCurrentValues(crParameterValues9)
        PFDefs(9).ApplyCurrentValues(crParameterValues10)
        PFDefs(10).ApplyCurrentValues(crParameterValues11)
        PFDefs(11).ApplyCurrentValues(crParameterValues12)
        PFDefs(12).ApplyCurrentValues(crParameterValues13)
        PFDefs(13).ApplyCurrentValues(crParameterValues14)
        PFDefs(14).ApplyCurrentValues(crParameterValues15)

        crvView.ParameterFieldInfo = paramFields
    End Sub

    Sub CreateTableCols()
        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()
        Dim Col3 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "LocCode"
        Col1.ColumnName = "LocCode"
        Col1.DefaultValue = ""
        tblTotal.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.String")
        Col2.AllowDBNull = True
        Col2.Caption = "Code"
        Col2.ColumnName = "Code"
        Col2.DefaultValue = ""
        tblTotal.Columns.Add(Col2)

        Col3.DataType = System.Type.GetType("System.Decimal")
        Col3.AllowDBNull = True
        Col3.Caption = "TotalAmt"
        Col3.ColumnName = "TotalAmt"
        Col3.DefaultValue = 0
        tblTotal.Columns.Add(Col3)

    End Sub
End Class
