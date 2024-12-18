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
Imports System.Collections


Public Class WS_StdRpt_JobList_Preview : Inherits Page
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objWS As New agri.WS.clsReport()
    Dim objWSTrx As New agri.WS.clsTrx()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim intDecimal As Integer
    Dim strUserLoc As String
    Dim tempLoc As String

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
    Dim strlblLocation As String    
    Dim strlblBillParty As String
    Dim strlblVehicle As String
    Dim strlblAccCode As String
    Dim strlblBlkCode As String
    Dim strlblVehExpCode As String
    Dim strlblWorkCode As String
    Dim strParamLoc As String

    Dim rdCrystalViewer As ReportDocument
    Dim intErrNo As Integer
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

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")


        strRptDecimal = Request.QueryString("Decimal")
        strRptID = Request.QueryString("RptID")
        strRptName = Request.QueryString("RptName")
        strRptLoc = Trim(Request.QueryString("Location"))
        strRptAccMonth = Request.QueryString("DDLAccMth")
        strRptAccYear = Request.QueryString("DDLAccYr")
        strRptJobIDFrom = Request.QueryString("JobIDFrom")
        strRptJobIDTo = Request.QueryString("JobIDTo")
        strRptJobStartDateFrom = Request.QueryString("JobStartDateFrom")
        strRptJobStartDateTo = Request.QueryString("JobStartDateTo")
        strRptBillPartyCode = Request.QueryString("BillPartyCode")
        strRptJobType = Request.QueryString("JobType")        
        strRptEmpCode = Request.QueryString("EmpID")
        strRptStatus = Request.QueryString("Status")
        strlblBillParty = Request.QueryString("lblBillPartyCode")
        strlblLocation = Request.QueryString("lblLocation")
        strlblVehicle = Request.QueryString("lblVehicle")
        strlblAccCode = Request.QueryString("lblAccCode")
        strlblBlkCode = Request.QueryString("lblBlkCode")
        strlblVehExpCode = Request.QueryString("lblVehExpCode")
        strlblWorkCode = Request.QueryString("lblWorkCode")

        If Right(strRptLoc, 1) = "," Then
                strRptLoc = Left(strRptLoc, Len(strRptLoc) - 1)
        End If

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else              
            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim strRptPrefix As String = "WS_StdRpt_JobList"
        Dim dsReport As New DataSet
        Dim strMapPath As String
        Dim colParam As New Collection
        Dim intCnt As Integer

        colParam.Add("'" & strRptLoc & "'", "PARAM_LOCATION")
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

        colParam.Add("WS_STDRPT_JOB_LISTING_GET_SP", "OC_REPORT_GET")

        Try
            intErrNo = objWS.mtdGetReport_JobListing(colParam, dsReport, strMapPath)
            dsReport.Tables(0).TableName = "WS_JOB"
            dsReport.Tables(1).TableName = "WS_JOBSTOCK"
            dsReport.Tables(2).TableName = "WS_MECHINICHOUR"
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WS_STDRPT_JOB_LISTING_GET&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=")
        End Try


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(strMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(dsReport)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        PassParam()
        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = strMapPath & "web\ftp\" & strRptPrefix & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        Try
            rdCrystalViewer.Export()
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WS_STDRPT_JOB_LISTING_GET&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=")
        End Try

        
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
    End Sub

    Sub PassParam()
        Const PARAM_UBOUND As Integer = 22
        
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
        ParamFieldDef(0) = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef(1) = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef(2) = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef(3) = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef(4) = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef(5) = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef(6) = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef(7) = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef(8) = ParamFieldDefs.Item("ParamJobIDFrom")
        ParamFieldDef(9) = ParamFieldDefs.Item("ParamJobIDTo")
        ParamFieldDef(10) = ParamFieldDefs.Item("ParamJobStartDateFrom")
        ParamFieldDef(11) = ParamFieldDefs.Item("ParamJobStartDateTo")
        ParamFieldDef(12) = ParamFieldDefs.Item("ParamBillParty")
        ParamFieldDef(13) = ParamFieldDefs.Item("ParamEmpID")
        ParamFieldDef(14) = ParamFieldDefs.Item("ParamStatus")
        ParamFieldDef(15) = ParamFieldDefs.Item("lblBillPartyCode")
        ParamFieldDef(16) = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef(17) = ParamFieldDefs.Item("lblVehicle")
        ParamFieldDef(18) = ParamFieldDefs.Item("ParamJobType")
        ParamFieldDef(19) = ParamFieldDefs.Item("lblAccCode")
        ParamFieldDef(20) = ParamFieldDefs.Item("lblBlkCode")
        ParamFieldDef(21) = ParamFieldDefs.Item("lblVehExpCode")
        ParamFieldDef(22) = ParamFieldDefs.Item("lblWorkCode")
        
        ParamDiscreteValue(0).Value = Session("SS_LOC")
        ParamDiscreteValue(1).Value = strCompanyName
        ParamDiscreteValue(2).Value = strUserName
        ParamDiscreteValue(3).Value = CInt(strRptDecimal)
        ParamDiscreteValue(4).Value = strRptID
        ParamDiscreteValue(5).Value = strRptName
        ParamDiscreteValue(6).Value = strRptAccMonth
        ParamDiscreteValue(7).Value = strRptAccYear
        ParamDiscreteValue(8).Value = strRptJobIDFrom
        ParamDiscreteValue(9).Value = strRptJobIDTo
        ParamDiscreteValue(10).Value = strRptJobStartDateFrom
        ParamDiscreteValue(11).Value = strRptJobStartDateTo
        ParamDiscreteValue(12).Value = strRptBillPartyCode
        ParamDiscreteValue(13).Value = strRptEmpCode
        ParamDiscreteValue(14).Value = strRptStatus
        ParamDiscreteValue(15).Value = strlblBillParty
        ParamDiscreteValue(16).Value = strlblLocation
        ParamDiscreteValue(17).Value = strlblVehicle
        ParamDiscreteValue(18).Value = strRptJobType
        ParamDiscreteValue(19).Value = strlblAccCode
        ParamDiscreteValue(20).Value = strlblBlkCode
        ParamDiscreteValue(21).Value = strlblVehExpCode
        ParamDiscreteValue(22).Value = strlblWorkCode
        
        For intCnt = 0 To ParamFieldDef.Length - 1
            ParameterValues(intCnt) = ParamFieldDef(intCnt).CurrentValues
            ParameterValues(intCnt).Add(ParamDiscreteValue(intCnt))
            ParamFieldDef(intCnt).ApplyCurrentValues(ParameterValues(intCnt))
        Next
    End Sub

    
End Class
