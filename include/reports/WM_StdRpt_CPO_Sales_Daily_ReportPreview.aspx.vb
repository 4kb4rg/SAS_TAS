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

Public Class WM_StdRpt_CPO_Sales_Daily_ReportPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objWM As New agri.WM.clsReport()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
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

    Dim strTransporter As String
    Dim strRptType As String
    Dim strCustCode As String
    Dim strProdCode As String
    Dim strContract As String
    Dim strDateFr As String
    Dim strDateTo As String

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

        intDecimal = Request.QueryString("Decimal")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        strRptType = Trim(Request.QueryString("RptType"))
        strCustCode = Trim(Request.QueryString("CustCode"))
        strProdCode = Trim(Request.QueryString("ProdCode"))
        strDateFr = Trim(Request.QueryString("DateFrom"))
        strDateTo = Trim(Request.QueryString("DateTo"))
        strContract = Trim(Request.QueryString("ContractNo"))
        strRptType = Trim(Request.QueryString("RptType"))
        strTransporter = Trim(Request.QueryString("Transporter"))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objFTPFolder As String
        Dim dsOutDate As New DataSet()
        Dim dsSupplier As New DataSet()
        Dim objMapPath As String
        Dim strOpCd_Daily As String = ""
        Dim strFileName As String = ""

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strParam As String
        Dim strParamName As String

        If strRptType = "1" Then
            strOpCd_Daily = "WM_STDRPT_DAILY_SALES_PRODUCT_GET_SP"
            strFileName = "WM_StdRpt_WeighBridgeSalesDet"
        Else
            strOpCd_Daily = "WM_STDRPT_DAILY_SALES_PRODUCT_GET_SP"
            strFileName = "WM_StdRpt_WeighBridgeSalesSummary"
        End If

        strParamName = "SUPCODE|PRODCODE|PERIODEDR|PERIODESD|LOCCODE|CONTRACTNO|TRANSPORTER"
        strParam = strCustCode & "|" & _
                   strProdCode & "|" & _
                   strDateFr & "|" & _
                   strDateTo & "|" & _
                   strLocation & "|" & _
                   strContract & "|" & _
                   strTransporter
        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCd_Daily, strParamName, strParam, dsSupplier, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_PRODUCTION_TBS_CPO_KERNEL_MONTHLY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(dsSupplier.Tables(0))
        ''rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()


        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues


        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamRptName")


        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("RptName")


        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
