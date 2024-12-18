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

Imports agri.PU.clsTrx
Imports agri.Admin.clsShare
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class PU_Rpt_RPHDet : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objPU As New agri.PU.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim rdCrystalViewer As New ReportDocument()
    Dim objOk As New agri.GL.clsReport

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strStatus As String
    Dim strRPHId As String
    Dim strPrintDate As String
    Dim strSortLine As String
    Dim batchPrint As String
    Dim strReprintedID As String
    Dim arrReprintedID As Array
    Dim intCnt2 As Integer

    Dim strCompName As String
    Dim strLocName As String
    Dim strUserName As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strCompName = Session("SS_COMPANYNAME")
        strLocName = Session("SS_LOCATIONNAME")
        strUserName = Session("SS_USERNAME")

        crvView.Visible = False

        strStatus = Trim(Request.QueryString("strStatus"))
        strRPHId = Trim(Request.QueryString("strRPHId"))
        strPrintDate = Trim(Request.QueryString("strPrintDate"))
        strSortLine = Trim(Request.QueryString("strSortLine"))
        batchPrint = Trim(Request.QueryString("batchPrint"))
        strReprintedID = Trim(Request.QueryString("reprintId"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim strOpCd_Get As String = ""
        Dim strOpCd_GetLine As String = ""
        Dim strOpCd_GetLocInfo As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strReportName As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim objFTPFolder As String
        'Dim dr As DataRow

        strOpCd_GetLine = "PU_CLSTRX_RPH_LINE_GET_FOR_DOCRPT"
        strReportName = "PU_Rpt_RPHDet.rpt"

        strParamName = "DOCID"
        strParamValue = strRPHId

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd_GetLine, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PU_CLSTRX_RPH_LINE_GET_FOR_DOCRPT&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        'Try
        '    intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        'End Try

        rdCrystalViewer.Load(objMapPath & "Web\EN\PU\Reports\Crystal\" & strReportName, OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        'lblErrMesage.Text = objMapPath & "Web\EN\PU\Reports\Crystal\" & strReportName
        'lblErrMesage.Visible = True

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\PU_Rpt_RPHDet.pdf"
        crExportOptions = rdCrystalViewer.ExportOptions

        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()


        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/PU_Rpt_RPHDet.pdf"">")

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


        paramField1 = paramFields.Item("strPrintedBy")

        crParameterValues1 = paramField1.CurrentValues


        ParamDiscreteValue1.Value = strUserName

        crParameterValues1.Add(ParamDiscreteValue1)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)

        crvView.ParameterFieldInfo = paramFields
    End Sub

End Class

