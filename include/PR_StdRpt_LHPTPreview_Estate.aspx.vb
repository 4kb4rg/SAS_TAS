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

Public Class PR_StdRpt_LHPTPreview_Estate : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPRRpt As New agri.PR.clsReport()
    Dim objHRReport As New agri.HR.clsReport()
    Dim objOk As New agri.GL.clsReport

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim tempLoc As String
    Dim intDecimal As Integer
    Dim strRptName As String
    Dim strPhyMonth As String
    Dim strPhyYear As String
    Dim strDate As String
    Dim strDivisi As String
    Dim strMandor As String
    Dim objFTPFolder As String
	
	Dim strmanager As String = ""
	Dim strasisten As String = ""

    Dim isMandor As Boolean
	Dim isToday As Boolean 
	Dim isexcel As Boolean 
	Dim strstat AS String
	
	

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

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


        strRptName = Request.QueryString("RptName")
        strLangCode = Session("SS_LANGCODE")
        strDate = Request.QueryString("SelDate")
        strDivisi = Request.QueryString("Divisi")
        strMandor = Request.QueryString("Mandor")
        isMandor = Request.QueryString("isMandor")
        isToday = Request.QueryString("isToday")
		
		strmanager = Request.QueryString("Manager")
		strasisten = Request.QueryString("Asisten")
		strstat = Request.QueryString("status")
		
		isexcel = Request.QueryString("excel")
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
        Dim strOpCd As String

        Dim fname As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim ReportID As String = Request.QueryString("RptID")

        If Not isMandor Then
            strOpCd = "PR_PR_STDRPT_LHPT_REPORT_DIVISI"
			
			if isexcel then
			fname = "PR_STDRPT_LHPT_ESTATE_DIVISI_EXCEL"
			else
			fname = "PR_STDRPT_LHPT_ESTATE_DIVISI"
			end if 
            
            strParamName = "LOCCODE|DIVISI|BKMDATE|TD|STATUS"
            strParamValue = strLocation & "|" & _
                            strDivisi & "|" & _
                            strDate & "|" & _
							iif(isToday, "", "1") & "|" & _
							strStat

            Try
                intErrNo = objOk.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRpt, objMapPath, objFTPFolder)
                objRpt.Tables(0).TableName = "PR_StdRpt_LHPT_Divisi"
                objRpt.Tables(1).TableName = "PR_StdRpt_LHPT_JobSumary"
                objRpt.Tables(2).TableName = "PR_StdRpt_LHPT_BhnSumary"

            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STDRPT_LHPT_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            objRptDs.Tables.Add(objRpt.Tables(0).Copy())
            objRptDs.Tables(0).TableName = "PR_StdRpt_LHPT_Divisi"
            objRptDs.Tables.Add(objRpt.Tables(1).Copy())
            objRptDs.Tables(1).TableName = "PR_StdRpt_LHPT_JobSumary"
            objRptDs.Tables.Add(objRpt.Tables(2).Copy())
            objRptDs.Tables(2).TableName = "PR_StdRpt_LHPT_BhnSumary"

        Else
            strOpCd = "PR_PR_STDRPT_LHPT_REPORT_MANDOR"
		
			if isexcel then
			fname = "PR_STDRPT_LHPT_ESTATE_MANDOR"
			else
			fname = "PR_STDRPT_LHPT_ESTATE_MANDOR"
			end if 
      
            strParamName = "LOCCODE|DIVISI|MANDOR|BKMDATE|STATUS"
            strParamValue = strLocation & "|" & _
                            strDivisi & "|" & _
                            strMandor & "|" & _
                            strDate 

            Try
                intErrNo = objOk.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRpt, objMapPath, objFTPFolder)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STDRPT_LHPT_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
            objRpt.Tables(0).TableName = "PR_StdRpt_LHPT_Mandor"
            objRptDs.Tables.Add(objRpt.Tables(0).Copy())
            objRptDs.Tables(0).TableName = "PR_StdRpt_LHPT_Mandor"
        End If


        

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\reports\crystal\" & fname & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)


        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
       
		If isexcel Then
			crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & fname & ".xls"
        Else
            crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & fname & ".pdf"
        End If

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
			If isexcel Then
			 .ExportFormatType = ExportFormatType.Excel
            Else
             .ExportFormatType = ExportFormatType.PortableDocFormat
            End If
           
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
		If isexcel Then
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & fname & ".xls"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & fname & ".pdf"">")
        End If
		
       
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

        ParamDiscreteValue1.Value = Session("SS_USERNAME")
        ParamDiscreteValue2.Value = rtrim(strmanager)
        ParamDiscreteValue3.Value = rtrim(strasisten)

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef2 = ParamFieldDefs.Item("Manager")
        ParamFieldDef3 = ParamFieldDefs.Item("Asisten")

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
