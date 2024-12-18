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

Imports agri.WS.clsTrx
Imports agri.Admin.clsShare
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class WS_Rpt_IssueReturn : inherits page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objWS As New agri.WS.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strJobId As String
    Dim strCompName As String
    Dim strLocName As String
    Dim strUserName As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strCompName = Session("SS_COMPANYNAME")
        strLocName = Session("SS_LOCATIONNAME")
        strUserName = Session("SS_USERNAME")
        
        crvView.Visible = False  

        strJobId = Trim(Request.QueryString("strJobId"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        'Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        'Dim objRptDs As New Dataset()
        'Dim objMapPath As New Object()
        'Dim strOpCd_Get As String = ""
        'Dim strOpCd_GetLine As String = ""
        'Dim strOpCodes As String
        'Dim strParam As String = ""
        'Dim intErrNo As Integer
        'Dim intCnt As Integer
        'Dim strReportName As String
        'Dim strPDFName As String


        'strOpCd_Get = "WS_CLSTRX_JOB_DETAILS_GET_FOR_DOCRPT" & "|" & "Job"
        'strOpCd_GetLine = "WS_CLSTRX_JOB_DETAILS_GET_LINE_FOR_DOCRPT" & "|" & "JobLine"
        'strReportName = "WS_Rpt_IssueReturn.rpt"

        'strOpCodes = strOpCd_Get & chr(9) & strOpCd_GetLine
        'strParam = strJobId & "|"

        'Try
        '    intErrNo = objWS.mtdGetIssueReturnRpt(strOpCodes, _
        '                                          strParam, _
        '                                          strCompany, _
        '                                          strLocation, _
        '                                          strUserId, _
        '                                          strAccMonth, _
        '                                          strAccYear, _
        '                                          objRptDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")        
        'End Try

        'Try
        '    intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMesage.Text & "&redirect=")
        'End Try

        'For intCnt=0 To objRptDs.Tables("Job").Rows.Count - 1
        '    objRptDs.Tables("Job").Rows(intCnt).Item("Status") = objWS.mtdGetJobStatus(CInt(objRptDs.Tables("Job").Rows(intCnt).Item("Status")))
        'Next


        'rdCrystalViewer.Load(objMapPath & "Web\EN\WS\Reports\Crystal\" & strReportName, OpenReportMethod.OpenReportByTempCopy)
        'rdCrystalViewer.SetDataSource(objRptDs)

        'If Not blnIsPDFFormat Then
        '    crvView.Visible = True     
        '    crvView.ReportSource = rdCrystalViewer
        '    crvView.DataBind()
        '    PassParam()
        'Else
        '    crvView.Visible = False     
        '    crvView.ReportSource = rdCrystalViewer
        '    crvView.DataBind()
        '    PassParam()
        '    Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
        '    rdCrystalViewer.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
        '    rdCrystalViewer.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
        '    DiskOpts.DiskFileName = objMapPath & "web\ftp\WS_Rpt_IssueReturn.pdf"
        '    rdCrystalViewer.ExportOptions.DestinationOptions = DiskOpts
        '    rdCrystalViewer.Export()

        '    Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/WS_Rpt_IssueReturn.pdf"">")
        'End If

    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        ParamFields = crvView.ParameterFieldInfo
      
        ParamField1 = ParamFields.Item("strPrintedBy")
        ParamField2 = ParamFields.Item("strCompName")
        ParamField3 = ParamFields.Item("strLocName")

        crParameterValues1 = ParamField1.CurrentValues
        crParameterValues2 = ParamField2.CurrentValues
        crParameterValues3 = ParamField3.CurrentValues

        ParamDiscreteValue1.value = strUserName
        ParamDiscreteValue2.value = strCompName
        ParamDiscreteValue3.value = strLocName

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)

        crvView.ParameterFieldInfo = paramFields
    End Sub


End Class

