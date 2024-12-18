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

Imports agri.GL
Imports agri.Admin.clsShare
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class GL_Rpt_FSReport : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objOk As New agri.GL.clsReport

    Dim objGLRpt As New agri.GL.clsReport()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strExportToExcel As String
    Dim strAudited As String
    Dim strYearly As String
    Dim strBudget As String
    Dim strRptInit As String
    Dim strRptName As String
    Dim DateOfPeriod As Date
    Dim DateOfLastPeriod As Date
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strOpCode As String
    Dim strMethod As String

    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strTemplate As String

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strUserName As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strUserName = Session("SS_USERNAME")

        crvView.Visible = False

        strRptName = Trim(Request.QueryString("strRptName"))
        strOpCode = Trim(Request.QueryString("strOpCode"))
        strExportToExcel = Trim(Request.QueryString("strExportToExcel"))
        strAccMonth = Trim(Request.QueryString("strAccMonth"))
        strAccYear = Trim(Request.QueryString("strAccYear"))
        strTemplate = Trim(Request.QueryString("strTemplate"))

        Bind_Report(True)
    End Sub

    Sub Bind_Report(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim objRptDs As New DataSet()
        Dim objRptSubDs As New DataSet()
        Dim objMapPath As String
        Dim objFTPFolder As String
        Dim strOpCd_Get As String = "GL_FS_REPORT_DETAIL"

        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "ACCYEAR|ACCMONTH|RPTCODE|RPTTYPE"
        strParamValue = strAccYear & "|" &
                        strAccMonth & "|" &
                        strRptName & "|" &
                        strTemplate

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd_Get, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & Exp.Message & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\GL\reports\Crystal\GL_StdRpt_FSSingleNewLR.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        If Not blnIsPDFFormat Then
            crvView.Visible = True
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
            PassParam()
        Else
            crvView.Visible = False
            crvView.ReportSource = rdCrystalViewer
            crvView.DataBind()
            PassParam()
            Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
            rdCrystalViewer.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            rdCrystalViewer.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            DiskOpts.DiskFileName = objMapPath & "web\ftp\GL_StdRpt_FSSingleNewLR.pdf"
            rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
            rdCrystalViewer.Export()

            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/GL_StdRpt_FSSingleNewLR.pdf"">")
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


        Dim ParamDiscreteValue1 As New ParameterDiscreteValue
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        Dim paramField10 As New ParameterField
        Dim ParamDiscreteValue10 As New ParameterDiscreteValue
        Dim crParameterValues10 As ParameterValues

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("prmCompanyName")
        paramField2 = paramFields.Item("prmLocation")
        paramField3 = paramFields.Item("prmUserName")
        paramField4 = paramFields.Item("RptName")
        paramField5 = paramFields.Item("prmAccMonth")
        paramField6 = paramFields.Item("prmAccYear")


        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues


        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = "DDDDDDDDD"
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = "aaaaaaaaaa"

        ParamDiscreteValue5.Value = "2023"
        ParamDiscreteValue6.Value = "1"


        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)


        PFDefs(0).ApplyCurrentValues(crParameterValues1)

        PFDefs(1).ApplyCurrentValues(crParameterValues2)

        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)



        crvView.ParameterFieldInfo = paramFields
    End Sub

End Class

