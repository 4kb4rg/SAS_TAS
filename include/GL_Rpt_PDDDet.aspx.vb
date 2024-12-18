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

Imports agri.IN.clsTrx
Imports agri.GL.clsReport
Imports agri.Admin.clsShare
Imports agri.Admin.clsComp
Imports agri.Admin.clsLoc
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class GL_Rpt_PDDDet : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objIn As New agri.IN.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim rdCrystalViewer As New ReportDocument()
    Dim objLangCapDs As New Object()
    Dim objOk As New agri.GL.clsReport

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strStatus As String
    Dim strStockTxId As String
	
    Dim strPrintDate As String
    Dim strSortLine As String
    Dim strDocTitle As String

    Dim strCompName As String
    Dim strLocName As String
    Dim strUserName As String
    Dim strLocationTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim objFTPFolder As String


    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
       
        strCompName = Session("SS_COMPANYNAME")
        strLocName = Session("SS_LOCATIONNAME")
        strUserName = Session("SS_USERNAME")
        strLocType = Session("SS_LOCTYPE")
        crvView.Visible = False

        strStatus = Trim(Request.QueryString("strStatus"))
        strStockTxId = Trim(Request.QueryString("TRXID"))
		strAccMonth = Trim(Request.QueryString("AccMonth"))
        strAccYear = Trim(Request.QueryString("AccYear"))
        strPrintDate = Trim(Request.QueryString("strPrintDate"))
        strSortLine = Trim(Request.QueryString("strSortLine"))
        strDocTitle = Trim(Request.QueryString("strDocTitle"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        
		Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
		Dim strRptPrefix As String = "GL_RPT_PDD"
        Dim strOpCd_Get As String = "GL_CLSTRX_PDD_GSJ_GET_REPORT"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "PDOID|AM|AY|LOC"
        strParamValue = strStockTxId & "|"& _ 
		                strAccMonth & "|"& _ 
						strAccYear & "|"& _ 
					    strLocation 
                        

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd_Get, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & Exp.Message & "&redirect=")
        End Try

		rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\GL\reports\Crystal\GL_Rpt_PDD.rpt", OpenReportMethod.OpenReportByTempCopy)
        objRptDs.Tables(0).TableName = "PDD"
        objRptDs.Tables(1).TableName = "PDDLN"
        rdCrystalViewer.SetDataSource(objRptDs)
		
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

        ParamDiscreteValue1.Value = UCase(strLocName)
        ParamDiscreteValue2.Value = UCase(strStockTxId)
		ParamDiscreteValue3.Value = UCase(strCompName)
		

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

