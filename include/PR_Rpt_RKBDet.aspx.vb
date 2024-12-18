
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.XML
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web


Public Class PR_Rpt_RKBDet : inherits page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
 
    Protected WithEvents lblErrMesage As Label

	Dim objGLRpt As New agri.GL.clsReport()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objComp As New agri.Admin.clsComp()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
	
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
	Dim strCompanyName As String
    Dim strLocationName As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strStatus As String
    Dim strRKB As String
    

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
		strLocationName = Session("SS_LOCATIONNAME")
		strCompanyName = Session("SS_COMPANYNAME")
        
        crvView.Visible = False  

        strStatus = Trim(Request.QueryString("strStatus"))
        strRKB = Trim(Request.QueryString("RKBCode"))
        
        Bind_RKB(True)

    End Sub

    Sub Bind_RKB(ByVal blnIsPDFFormat As Boolean)
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim objRpt As New DataSet()
        Dim objMapPath As String
		Dim intErrNo As Integer
		Dim strRptPrefix As String = "PR_RPT_RKB"
		Dim strOpCode As String = "PR_PR_TRX_RKBLN_MJOB_PRINT" 
		Dim strParamName As String
        Dim strParamValue As String
		Dim objFTPFolder As String

        strParamName = "RKB|LOC"
		strParamValue = strRKB & "|" & strLocation 
        
		Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCode, strParamName, strParamValue, objRpt, objMapPath, objFTPFolder)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=LAPORAN_BIAYA_PRODUKSI&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
		
	    rdCrystalViewer = New ReportDocument()
		
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\PR\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        
		objRpt.Tables(0).TableName = "RKB"
        objRpt.Tables(1).TableName = "RKBSUM"
        rdCrystalViewer.SetDataSource(objRpt)
		'rdCrystalViewer.SetDataSource(objRpt.Tables(0))
		
		crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()
		
		'rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        'rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3
		PassParam()
		crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf"
        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
		
		Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../" & strUrl & strRptPrefix & ".pdf"">")
       


    End Sub

    Sub PassParam()
        Dim ParamFieldDefs As ParameterFieldDefinitions

        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParamFieldDef2 As ParameterFieldDefinition
		Dim ParamFieldDef3 As ParameterFieldDefinition
		
        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
		Dim ParameterValues3 As New ParameterValues()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
		Dim ParamDiscreteValue3 As New ParameterDiscreteValue()

        ParamDiscreteValue1.Value = UCase(strLocationName)
        ParamDiscreteValue2.Value = UCase(strRKB)
		ParamDiscreteValue3.Value = UCase(strCompanyName)
		

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields

        ParamFieldDef1 = ParamFieldDefs.Item("strLocName")
        ParamFieldDef2 = ParamFieldDefs.Item("strGroupName")
		ParamFieldDef3 = ParamFieldDefs.Item("strCompName")
		

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
		ParameterValues3 = ParamFieldDef3.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
		ParameterValues3.Add(ParamDiscreteValue3)
		

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
		ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
    End Sub


End Class

