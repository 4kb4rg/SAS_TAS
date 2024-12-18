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

Public Class BD_StdRpt_NurseryActivityDistPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objBD As New agri.BD.clsReport()
    Dim objBDTrx As New agri.BD.clsTrx()
    Dim objBDSetup As New agri.BD.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim tblTotalSeed As DataTable = New DataTable("tblTotalSeed")
    Dim dsTotalSeed As New DataSet()
    Dim dsAccPeriod As New DataSet()
    Dim dr As DataRow

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub CreateTableCols()

        Dim Col1 As DataColumn = New DataColumn()
        Dim Col2 As DataColumn = New DataColumn()

        Col1.DataType = System.Type.GetType("System.String")
        Col1.AllowDBNull = True
        Col1.Caption = "BlkCode"
        Col1.ColumnName = "BlkCode"
        Col1.DefaultValue = 0
        tblTotalSeed.Columns.Add(Col1)

        Col2.DataType = System.Type.GetType("System.Decimal")
        Col2.AllowDBNull = True
        Col2.Caption = "TotalSeed"
        Col2.ColumnName = "TotalSeed"
        Col2.DefaultValue = 0
        tblTotalSeed.Columns.Add(Col2)


    End Sub

    Sub CountAccPeriod()
        Dim strOppCd_NurseryActivityDist_AccPeriod_GET As String = "BD_STDRPT_NURSERYACTIVITYDIST_ACCPERIOD_GET"
        Dim strParam As String

        strParam = "|" & strLocation & "|" & Request.QueryString("DDLPeriodID").Trim & "||||"
        Try
            intErrNo = objBDTrx.mtdGetNurseryActivityDist(strOppCd_NurseryActivityDist_AccPeriod_GET, strParam, dsAccPeriod)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_NURSERYACTIVITYDIST_YEAR_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


    End Sub

    Sub BindReport()

        Dim dsTotalSeed As New DataSet()
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim strParam As String
        Dim SearchStr As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_NurseryActivityDist_Format_GET As String = "BD_STDRPT_NURSERYACTIVITYDIST_FORMAT_GET"

            If Not Request.QueryString("BlkCode") = "" Then
                SearchStr = " AND NAD.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
            Else
                SearchStr = " AND NAD.BlkCode LIKE '%' "
            End If

        CountAccPeriod()

        strParam = Request.QueryString("DDLPeriodID").Trim & "|" & strLocation & "|" & SearchStr
        Try
            intErrNo = objBD.mtdGetReport_NurseryActivityDist(strOpCd_NurseryActivityDist_Format_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_NURSERYACTIVITYDIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        objRptDs.Tables(0).TableName = "BD_NURSERYACTIVITYDIST_FORMAT"
        objRptDs.Tables.Add(dsAccPeriod.Tables(0).Copy())
        objRptDs.Tables(1).TableName = "BD_NURSERYACTIVITYDIST_PERIOD"


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\BD_StdRpt_NurseryActivityDist.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\BD_StdRpt_NurseryActivityDist.pdf"

        Dim excelFormatOpts As New ExcelFormatOptions()
        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/BD_StdRpt_NurseryActivityDist.pdf"">")
    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()
        Dim paramField5 As New ParameterField()
        Dim paramField6 As New ParameterField()
        Dim paramField7 As New ParameterField()
        Dim paramField8 As New ParameterField()
        Dim paramField9 As New ParameterField()
        Dim paramField10 As New ParameterField()
        Dim paramField11 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue8 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue9 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue10 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue11 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
        Dim crParameterValues6 As ParameterValues
        Dim crParameterValues7 As ParameterValues
        Dim crParameterValues8 As ParameterValues
        Dim crParameterValues9 As ParameterValues
        Dim crParameterValues10 As ParameterValues
        Dim crParameterValues11 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptID")
        paramField6 = paramFields.Item("ParamRptName")
        paramField7 = paramFields.Item("ParamPeriod")
        paramField8 = paramFields.Item("lblAccCodeTag")
        paramField9 = paramFields.Item("lblBlockCodeTag")
        paramField10 = paramFields.Item("ParamBlockCode")
        paramField11 = paramFields.Item("lblLocationTag")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
        crParameterValues6 = paramField6.CurrentValues
        crParameterValues7 = paramField7.CurrentValues
        crParameterValues8 = paramField8.CurrentValues
        crParameterValues9 = paramField9.CurrentValues
        crParameterValues10 = paramField10.CurrentValues
        crParameterValues11 = paramField11.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOCATION")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("RptID")
        ParamDiscreteValue6.Value = Request.QueryString("RptName")
        ParamDiscreteValue7.Value = Request.QueryString("DDLPeriodName")
        ParamDiscreteValue8.Value = Request.QueryString("lblAccCodeTag")
        ParamDiscreteValue9.Value = Request.QueryString("lblBlkCodeTag")
        ParamDiscreteValue10.Value = Request.QueryString("BlkCode")
        ParamDiscreteValue11.Value = Request.QueryString("lblLocationTag")

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        crParameterValues6.Add(ParamDiscreteValue6)
        crParameterValues7.Add(ParamDiscreteValue7)
        crParameterValues8.Add(ParamDiscreteValue8)
        crParameterValues9.Add(ParamDiscreteValue9)
        crParameterValues10.Add(ParamDiscreteValue10)
        crParameterValues11.Add(ParamDiscreteValue11)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        PFDefs(5).ApplyCurrentValues(crParameterValues6)
        PFDefs(6).ApplyCurrentValues(crParameterValues7)
        PFDefs(7).ApplyCurrentValues(crParameterValues8)
        PFDefs(8).ApplyCurrentValues(crParameterValues9)
        PFDefs(9).ApplyCurrentValues(crParameterValues10)
        PFDefs(10).ApplyCurrentValues(crParameterValues11)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
