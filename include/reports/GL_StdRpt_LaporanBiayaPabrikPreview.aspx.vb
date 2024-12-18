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
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Imports agri.PWSystem.clsLangCap

Public Class GL_StdRpt_LaporanBiayaPabrikPreview : Inherits Page
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim rdCrystalViewer As ReportDocument
    Dim objLangCapDs As New Object()
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
    Dim strRptType As String

    Dim strExportToExcel As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        Dim tempLoc As String
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


        strRptId = Trim(Request.QueryString("RptId"))
        strRptName = Trim(Request.QueryString("RptName"))
        strSelAccMonth = Request.QueryString("DDLAccMth")
        strSelAccYear = Request.QueryString("DDLAccYr")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        intSelDecimal = CInt(Request.QueryString("Decimal"))
       
        strRptType = Trim(Request.QueryString("StrRptType"))
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCode As String
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim objFTPFolder As String


        Select Case strRptType
            Case 0 'rekapitulasi
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Rekapitulasi"
                strOpCode = "GL_CLSTRX_MILL_MGR_REKAPITULASI"
            Case 1 'Biaya Pengolahan
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Pengolahan"
                strOpCode = "GL_CLSTRX_MILL_MGR_PENGOLAHAN"
            Case 2 'Biaya Consumable
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Consumable"
                strOpCode = "GL_CLSTRX_MILL_MGR_CONSUMABLE"
            Case 3 'Biaya Peralatan Kerja
                strRptPrefix = "GL_StdRpt_Mill_Mgr_PeralatanKerja"
                strOpCode = "GL_CLSTRX_MILL_MGR_PERALATANKERJA"
            Case 4 'Biaya Manitenance
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Maintenance"
                strOpCode = "GL_CLSTRX_MILL_MGR_MAINTENANCE"
            Case 5 'Biaya Workshop
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Workshop"
                strOpCode = "GL_CLSTRX_MILL_MGR_WORKSHOP"
            Case 6 'Biaya Alokasi
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Alokasi"
                strOpCode = "GL_CLSTRX_MILL_MGR_ALOKASI"
            Case 7 'Biaya Umum dan Administrasi
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Umum"
                strOpCode = "GL_CLSTRX_MILL_MGR_UMUM"
            Case 8 'Capital Expenditure
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Capex"
                strOpCode = "GL_CLSTRX_MILL_MGR_CAPEX"
            Case 9 'Laporan Biaya Transit
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Vehicle"
                strOpCode = "GL_CLSTRX_MILL_MGR_VEHICLE"
            Case 10 'Rekap Biaya Manitenance
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Maintenance_Rekap"
                strOpCode = "GL_CLSTRX_MILL_MGR_MAINTENANCE_REKAP"
            Case 11 'Rekap Biaya Manitenance
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Station"
                strOpCode = "GL_CLSTRX_MILL_MGR_STATION"
            Case 12 'Rekap Biaya Manitenance
                strRptPrefix = "GL_StdRpt_Mill_Mgr_Station_Detail"
                strOpCode = "GL_CLSTRX_MILL_MGR_STATION_DETAIL"
            Case 13 'Rekap Biaya Manitenance
                strRptPrefix = "GL_StdRpt_Mill_Mgr_ExvSummary"
                strOpCode = "GL_CLSTRX_MILL_MGR_EXECUTIVE_SUMMARY"
        End Select

        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation

        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCode, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=LAPORAN_BIAYA_PRODUKSI&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        objRptDs.Tables(0).TableName = "GL_BIAYA"
        objRptDs.Tables(1).TableName = "GL_PROD"


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
'        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3


        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        If strExportToExcel = "0" Then
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf"
        Else
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".xls"
        End If
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

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")

        If strExportToExcel = "0" Then
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".xls"">")
        End If


    End Sub

    Sub PassParam()

        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParameterValues1 As New ParameterValues()
        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
       
        ParamDiscreteValue1.Value = ucase(strLocationName)
    
        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("strLocName")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues1.Add(ParamDiscreteValue1)
        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)


    End Sub

End Class
