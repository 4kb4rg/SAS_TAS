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

Public Class PopUpRemainLifeTimeScreenListPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objWS As New agri.WS.clsReport()
    Dim objWSTrx As New agri.WS.clsTrx()
    Dim objWSSetup As New agri.WS.clsSetup()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim intDecimal As Integer
    Dim tempLoc As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        If Left(Request.QueryString("Location"), 3) = "','" Then
            strUserLoc = Right(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 3)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

        intDecimal = Request.QueryString("")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        Session("SS_lblBlkTag") = Request.QueryString("lblBlkTag")
        Session("SS_lblSubBlkTag") = Request.QueryString("lblSubBlkTag")
        Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")

        Session("SS_Decimal") = Request.QueryString("Decimal")
        Session("SS_RptID") = Request.QueryString("RptID")
        Session("SS_RptName") = Request.QueryString("RptName")
        Session("SS_BlkCode") = Request.QueryString("BlkCode")
        Session("SS_SubBlkCode") = Request.QueryString("SubBlkCode")

        Session("SS_ReplDateFrom") = Request.QueryString("ReplDateFrom")
        Session("SS_ReplDateTo") = Request.QueryString("ReplDateTo")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim objRptPRLn As New DataSet()
        Dim objMapPath As String
        Dim objFTPFolder As String

        Dim intCnt As Integer
        Dim intItemCode As Integer
        Dim intPRLn As Integer

        Dim SearchStr As String
        Dim OrderByStr As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCdItem_GET As String = "GL_STDRPT_REMAININGLIFETIME_PRINT_GET"
        'Dim strOpCdItem_GET As String = "WS_STDRPT_VEHICLE_PREVENTIVE_MAINTENANCE_GET"

        Dim strParam As String
        Dim strParamItemCode As String

        Dim strBlkCode As String
        Dim strSubBlkCode As String
        Dim strItemCode As String
        Dim strQtyOut As String
        Dim strTotQtyOut As String
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "LOCCODE"
        strParamValue = strLocation

        Try
            '    intErrNo = objWS.mtdGetReport_ReplacementScheduleList(strOpCdItem_GET, strParam, objRptDs, objMapPath)
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCdItem_GET, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_REPLACEMENTSCHEDULE_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        'rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\WS_StdRpt_ReplacementScheduleList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\WS_StdRpt_RemainLifeTime.rpt", OpenReportMethod.OpenReportByTempCopy)
        'rdCrystalViewer.Load(objMapPath & "Web\en\IN\reports\Crystal\WS_StdRpt_PreventiveMaintenance.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
        'MsgBox("D")
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()


        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\WS_StdRpt_ReplacementScheduleList.pdf"
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\WS_StdRpt_RemainLifeTime.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        '    .ExportFormatType = ExportFormatType.Excel
        End With

        rdCrystalViewer.Export()
        'MsgBox("D")
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../ftp/WS_StdRpt_RemainLifeTime.pdf"">")
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
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamCompany")


        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues


        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_LOCATIONNAME")


        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)


        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)


        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
