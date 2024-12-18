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

Public Class PR_StdRpt_DaftarBrondolanPreview_Estate : Inherits Page

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
    Dim strDateFrom As String
    Dim strDateTo As String
    Dim strEmpCode As String
    Dim strDivisi As String
    Dim strMandor As String
    Dim strPekerjaan As String
	Dim strDivisiKerja as String
	Dim strExportToExcel As String

 Dim objFTPFolder As String
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

        strAccMonth = Request.QueryString("AccMonth")
        strAccYear = Request.QueryString("AccYear")
        strRptName = Request.QueryString("RptName")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Request.QueryString("SelPhyMonth")
        strPhyYear = Request.QueryString("SelPhyYear")
        strDateFrom = Request.QueryString("SelDateFrom")
        strDateTo = Request.QueryString("SelDateTo")
        strEmpCode = Request.QueryString("EmpCode")
        strDivisi = Request.QueryString("Divisi")
        strMandor = Request.QueryString("Mandor")
		strDivisiKerja = Request.QueryString("DivisiKerja")
		strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim objVehicle As New DataSet()
        Dim objMapPath As String
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String = "PR_PR_STDRPT_DAFTAR_BRONDOLAN_REPORT"
        Dim strParamName As String
        Dim strParamValue As String
        Dim ReportID As String = Request.QueryString("RptID")


        'strOpCd = "PR_PR_STDRPT_DAFTAR_ABSENSI_REPORT" & "|" & objPRRpt.mtdGetPRReportTable(objPRRpt.EnumPRReportTable.DaftarAbsensi) & Chr(9) & _
        '          "GET_REPORT_INFO_BY_REPORTID|SH_REPORT"


        strParamName = "ACCMONTH|ACCYEAR|PHYMONTH|PHYYEAR|COMPCODE|LOCCODE|DATEFROM|DATETO|DECIMAL|F_EMPCODE|F_DIVCODE|F_MCODE|F_KERJA"

        strParamValue = strAccMonth & "|" & _
                        strAccYear & "|" & _
                        strAccMonth & "|" & _
                        strAccYear & "|" & _
                        strCompany & "|" & _
                        strLocation & "|" & _
                        strDateFrom & "|" & _
                        strDateTo & "|" & _
                        intDecimal & "|" & _
                        strEmpCode & "|" & _
                        strDivisi & "|" & _
                        strMandor  & "|" & _
						strDivisiKerja 

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_DAFTARABSENSI&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\reports\crystal\PR_StdRpt_DaftarBrondolan_Estate.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))


        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
		If strExportToExcel = "0" Then
			crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PR_StdRpt_DaftarBrondolan_Estate.pdf"
		Else
			crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PR_StdRpt_DaftarBrondolan_Estate.xls"
		End if
  
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
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PR_StdRpt_DaftarBrondolan_Estate.pdf"">")
		else
		Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/PR_StdRpt_DaftarBrondolan_Estate.xls"">")
		end if 
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        'Dim paramField2 As New ParameterField()
        'Dim paramField3 As New ParameterField()
        'Dim paramField4 As New ParameterField()
        'Dim paramField5 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue5 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        'Dim crParameterValues2 As ParameterValues
        'Dim crParameterValues3 As ParameterValues
        'Dim crParameterValues4 As ParameterValues
        'Dim crParameterValues5 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("userprint")
        'paramField2 = paramFields.Item("SelLocCode")
        'paramField3 = paramFields.Item("ParamUserName")
        'paramField4 = paramFields.Item("Period")
        'paramField5 = paramFields.Item("SelDecimal")

        crParameterValues1 = paramField1.CurrentValues
        'crParameterValues2 = paramField2.CurrentValues
        'crParameterValues3 = paramField3.CurrentValues
        'crParameterValues4 = paramField4.CurrentValues
        'crParameterValues5 = paramField5.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_USERNAME")
        'ParamDiscreteValue2.Value = Request.QueryString("Location")
        'ParamDiscreteValue3.Value = Session("SS_USERNAME")
        'ParamDiscreteValue4.Value = strAccMonth & "/" & strAccYear
        'ParamDiscreteValue5.Value = intDecimal

        crParameterValues1.Add(ParamDiscreteValue1)
        'crParameterValues2.Add(ParamDiscreteValue2)
        'crParameterValues3.Add(ParamDiscreteValue3)
        'crParameterValues4.Add(ParamDiscreteValue4)
        'crParameterValues5.Add(ParamDiscreteValue5)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        'PFDefs(1).ApplyCurrentValues(crParameterValues2)
        'PFDefs(2).ApplyCurrentValues(crParameterValues3)
        'PFDefs(3).ApplyCurrentValues(crParameterValues4)
        'PFDefs(4).ApplyCurrentValues(crParameterValues5)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
