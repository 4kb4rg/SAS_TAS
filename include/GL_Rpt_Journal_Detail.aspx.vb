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


Public Class GL_Rpt_Journal_Detail : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objGLRpt As New agri.GL.clsReport()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strUserName As String

    Dim strSelTRXID As String


    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strUserName = Session("SS_USERNAME")

        crvView.Visible = False

        strSelTRXID = Trim(Request.QueryString("TRXID"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)

        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strReportName As String
        Dim strPDFName As String
        Dim strOpCd As String = ""
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objFTPFolder As String



        strOpCd = "GL_STDRPT_JOURNAL_DETAIL"
        strParamName = "TRXID|LOCCODE"
        strParamValue = strSelTRXID & "|" & strLocation


        strReportName = "GL_Rpt_Journal_Detail.rpt"


        Try
            intErrNo = objGLRpt.mtdGetReport_KaryawanStaff(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objRptDs, _
                                                objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_JOURNAL_DETAIL&errmesg=" & "" & "&redirect=../GL/Trx/GL_Trx_Journal_Detail.aspx")
        End Try
     
        rdCrystalViewer.Load(objMapPath & "Web\EN\GL\Reports\Crystal\" & strReportName, OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()
        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()

        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strReportName & ".pdf"
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\GL_Rpt_Journal_Detail.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../" & strUrl & strReportName & ".pdf"">")


        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/GL_Rpt_Journal_Detail.pdf"">")

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
    
        ParamField1 = ParamFields.Item("strCompName")
        paramField2 = paramFields.Item("strLocName")
        paramField3 = paramFields.Item("ParamUser")

        crParameterValues1 = ParamField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues

        ParamDiscreteValue1.value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOCATIONNAME")
        ParamDiscreteValue3.Value = strUserName

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
      
        crvView.ParameterFieldInfo = paramFields
    End Sub

End Class

