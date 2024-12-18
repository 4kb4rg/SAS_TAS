Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class PM_StdRpt_DailyProdRpt_Preview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objOk As New agri.GL.clsReport

    Dim objPM As New agri.PM.clsReport()
    Dim objPMTrx As New agri.PM.clsTrx()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim intDecimal As Integer
    Dim tempLoc As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Dim strTransDate As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        strTransDate = Request.QueryString("TransDate")

        If Left(Request.QueryString("Location"), 3) = "','" Then
            strUserLoc = Right(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 3)
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
        strAccMonth = Request.QueryString("strddlAccMth")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objRpt As New DataSet()
        Dim objMapPath As String
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String = "WM_STDRPT_PRODUCTION_DAILY_REPORT_GET"
        Dim strOpCd_DespCPO As String = "WM_STDRPT_PRODUCTION_DAILY_DESPATCH_CPO_REPORT_GET"
        Dim strOpCd_DespSHELL As String = "WM_STDRPT_PRODUCTION_DAILY_DESPATCH_SHELL_REPORT_GET"
        Dim strOpCd_TBS As String = "WM_STDRPT_PRODUCTION_DAILY_TBS_TERIMA_REPORT_GET"
        Dim strOpCd_Kernel As String = "WM_STDRPT_PRODUCTION_DAILY_SILO_REPORT_GET"
        Dim strOpCd_CPO As String = "WM_STDRPT_PRODUCTION_DAILY_CPO_REPORT_GET"

        Dim strParamName As String
        Dim strParamValue As String
        Dim strterbilang As String = ""
        Dim totalrp As String = ""
        Dim ReportID As String = Request.QueryString("RptID")
        Dim strExportToExcel As String = "0"

        Dim objFTPFolder As String
        Dim dsDespatchCPO As New DataSet()
        Dim dsDespatchShell As New DataSet()
        Dim dsTBSTerima As New DataSet()
        Dim dsKernel As New DataSet()
        Dim dsCPO As New DataSet()

        strParamName = "PERIODE|LOCCODE|USRID"
        strParamValue = strTransDate & "|" & _
                        strLocation & "|" & _
                        strUserId

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_PRODUCTION_DAILY_REPORT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd_DespCPO, strParamName, strParamValue, dsDespatchCPO, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_PRODUCTION_DAILY_DESPATCH_CPO_REPORT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd_DespSHELL, strParamName, strParamValue, dsDespatchShell, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_PRODUCTION_DAILY_DESPATCH_SHELL_REPORT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd_TBS, strParamName, strParamValue, dsTBSTerima, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_PRODUCTION_DAILY_DESPATCH_SHELL_REPORT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd_Kernel, strParamName, strParamValue, dsKernel, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_PRODUCTION_DAILY_SILO_REPORT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd_CPO, strParamName, strParamValue, dsCPO, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_PRODUCTION_DAILY_CPO_REPORT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\reports\crystal\PM_StdRpt_DailyProdReport.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
        rdCrystalViewer.Subreports.Item("PK_DESPATCH").SetDataSource(dsDespatchCPO.Tables(0))
        rdCrystalViewer.Subreports.Item("SHELL_DESPATCH").SetDataSource(dsDespatchShell.Tables(0))
        rdCrystalViewer.Subreports.Item("TBS_TERMA").SetDataSource(dsTBSTerima.Tables(0))
        rdCrystalViewer.Subreports.Item("KERNEL_STOCK").SetDataSource(dsKernel.Tables(0))
        rdCrystalViewer.Subreports.Item("CPO_STOCK").SetDataSource(dsCPO.Tables(0))

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        'PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        If strExportToExcel = "0" Then
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PM_StdRpt_DailyProdReport.pdf"
        Else
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PM_StdRpt_DailyProdReport.xls"
        End If
        crExportOptions = rdCrystalViewer.ExportOptions

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            If strExportToExcel = "0" Then
                .ExportFormatType = ExportFormatType.PortableDocFormat
            Else
                .ExportFormatType = ExportFormatType.Excel
            End If
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        If strExportToExcel = "0" Then
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PM_StdRpt_DailyProdReport.pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PM_StdRpt_DailyProdReport.xls"">")
        End If
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields
        Dim paramField1 As New ParameterField
        Dim paramField2 As New ParameterField
        Dim paramField3 As New ParameterField
        Dim paramField4 As New ParameterField
        Dim paramField5 As New ParameterField
        Dim paramField6 As New ParameterField
        Dim paramField7 As New ParameterField
        Dim paramField8 As New ParameterField
        Dim paramField9 As New ParameterField

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue
        Dim ParamDiscreteValue8 As New ParameterDiscreteValue
        Dim ParamDiscreteValue9 As New ParameterDiscreteValue

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues
        Dim crParameterValues8 As ParameterValues
        Dim crParameterValues9 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        Dim paramField10 As New ParameterField
        Dim ParamDiscreteValue10 As New ParameterDiscreteValue
        Dim crParameterValues10 As ParameterValues

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptName")
        paramField6 = paramFields.Item("ParamRptID")
        paramField7 = paramFields.Item("ParamAccMonth")
        paramField8 = paramFields.Item("ParamAccYear")
        paramField9 = paramFields.Item("LblLocation")
        paramField10 = paramFields.Item("ParamTransDate")

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("RptName")
        ParamDiscreteValue6.Value = Request.QueryString("RptID")
        ParamDiscreteValue7.Value = Request.QueryString("strddlAccMth")
        ParamDiscreteValue8.Value = Request.QueryString("strddlAccYr")
        ParamDiscreteValue9.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue10.Value = strTransDate

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

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
