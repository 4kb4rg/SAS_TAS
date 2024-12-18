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

Public Class HR_StdRpt_Mutasi_KaryPreview_Estate : Inherits Page

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
    Dim SDate As String
    Dim EDate As String

    Dim strDivisi As String
    Dim strMandor As String
    Dim objFTPFolder As String

    Dim isMandor As Boolean

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
        SDate = Request.QueryString("SDate")
        EDate = Request.QueryString("EDate")
        strDivisi = Request.QueryString("Divisi")


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

        strOpCd = "HR_HR_STDRPT_EMP_MUTASI_GET"
        fname = "HR_StdRpt_Emp_Mutasi_Estate"
        strParamName = "LOC|DV|SD|ED"
        strParamValue = strLocation & "|" & _
                        strDivisi & "|" & _
                        SDate & "|" & _
                        EDate

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRpt, objMapPath, objFTPFolder)
            objRpt.Tables(0).TableName = "HR_HR_STDRPT_EMP_MUTASI_DET"

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=HR_HR_STDRPT_EMP_BERHENTI_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        objRptDs.Tables.Add(objRpt.Tables(0).Copy())
        objRptDs.Tables(0).TableName = "HR_HR_STDRPT_EMP_MUTASI_DET"

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\reports\crystal\" & fname & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)


        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & fname & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & fname & ".pdf"">")
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues


        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamUserName")


        crParameterValues1 = paramField1.CurrentValues


        ParamDiscreteValue1.Value = Session("SS_USERNAME")


        crParameterValues1.Add(ParamDiscreteValue1)


        PFDefs(0).ApplyCurrentValues(crParameterValues1)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
