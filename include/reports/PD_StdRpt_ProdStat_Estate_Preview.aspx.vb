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

Public Class PD_StdRpt_ProdStat_Estate_Preview : Inherits Page
  
    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
	Dim objOk As New agri.GL.clsReport
	
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc As String
    Dim strBlkType As String
    Dim strAccMth As String
    Dim strAccYr As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim intDecimal As Integer
    Dim strDateFrom As String
    Dim strDateTo As String

    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strSelAccPeriod As String
    Dim strSelBlk As String
    Dim strSelDivisi As String
    Dim strSelPabrik As String
    Dim strSelLocation As String
    Dim strLocTag As String
    Dim strBlkGrpTag As String
    Dim strBlkTag As String
    Dim strSubBlkTag As String
    Dim strLevel As String
	 Dim objFTPFolder As String
	  Dim strExportToExcel As String

    Dim strRptId As String
    Dim strRptName As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            crvView.Visible = False

            If Right(Request.QueryString("Location"), 1) = "," Then
                strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strUserLoc = Trim(Request.QueryString("Location"))
            End If

			strSelLocation = Trim(Request.QueryString("Location"))
            strDDLAccMth = Request.QueryString("DDLAccMth")
            strDDLAccYr = Request.QueryString("DDLAccYr")
			strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            intDecimal = Request.QueryString("Decimal")
			strSelDivisi = Trim(Request.QueryString("Divisi"))
            strSelPabrik = Trim(Request.QueryString("Pabrik"))
            strDateFrom = Trim(Request.QueryString("DateFrom"))
            strDateTo = Trim(Request.QueryString("DateTo"))
			strExportToExcel = Trim(Request.QueryString("ExportToExcel"))
            
            BindReport()

        End If
    End Sub


    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strname As String
		Dim strParam As String
		
        Dim strRptPrefix As String
        Dim intCnt As Integer

        Try
            strRptPrefix = "PD_StdRpt_PENGIRIMAN_TBS"
            strOpCd = "PD_PD_STDRPT_PENGIRIMAN_TBS"
            strname = "LOCCODE|DIVISI|SD|ED|MC"
            strParam = strSelLocation & "|" & _
                       strSelDivisi & "|" & _
                       strDateFrom & "|" & _
                       strDateTo & "|" & _
					   strSelPabrik

            intErrNo = intErrNo = objOk.mtdGetReportDataCommon(strOpCd, strname, strParam, objRptDs, objMapPath, objFTPFolder)
			
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PD_PD_STDRPT_PENGIRIMAN_TBS&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
       ' rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        'rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        PassParam()
        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
		If strExportToExcel = "0" Then
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
        Else
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".xls"
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
		
		If strExportToExcel = "0" Then
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
        Else
             Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".xls"">")
        End If
		
       
    End Sub

    Sub PassParam()
        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParamFieldDef2 As ParameterFieldDefinition
       
        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
       

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
       
        ParamDiscreteValue1.Value = trim(strUserName)
        ParamDiscreteValue2.Value = trim(strDateFrom & " s/d " & strDateTo)
       
        

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("paramUserName")
        ParamFieldDef2 = ParamFieldDefs.Item("paramPeriode")
        

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        
        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        
        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        
    End Sub
End Class
