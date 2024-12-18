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

Public Class BD_StdRpt_NurseryActivityPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objBD As New agri.BD.clsReport()
    Dim objBDTrx As New agri.BD.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim tblTotalSeed As DataTable = New DataTable("tblTotalSeed")
    Dim dsTotalSeed As New DataSet()
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

    Sub BindReport()

        Dim objRptDs As New DataSet()
        Dim dsSeed As New DataSet()
        Dim dsBlkCode As New DataSet()
        Dim objMapPath As String

        Dim strParam As String
        Dim strParamSeed As String
        Dim strParamNurseryActivity As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_NurseryActivity_Format_GET As String = "BD_CLSTRX_NURSERYACTIVITY_FORMAT_GET"
        Dim strOpCd_NurseryActivity_BlkCode_GET As String = "BD_STDRPT_NURSERYACTIVITY_BLKCODE_GET"
        Dim strOppCd_NurseryActivity_TotalSeed_GET As String = "BD_CLSTRX_NURSERYACTIVITY_COSTPERSEED_SUM"
        
        Dim intCnt As Integer
        Dim strBlkCode As String
        Dim SearchStr As String

        Dim decTotalSeed As Decimal

        
        If Not Request.QueryString("BlkCode") = "" Then
            SearchStr = " AND NA.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' "
        Else
            SearchStr = " AND NA.BlkCode LIKE '%' "
        End If
        

        CreateTableCols()

        strParamNurseryActivity = Request.QueryString("DDLPeriodID") & "|" & strLocation & "|" & SearchStr & "|" & "NA.BlkCode, NAS.DispSeq ASC"
        Try
            intErrNo = objBD.mtdGetReport_NurseryActivity(strOpCd_NurseryActivity_Format_GET, strParamNurseryActivity, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_NURSERYACTIVITY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam = strLocation & "|" & Request.QueryString("DDLPeriodID") & "|"
        Try
            intErrNo = objBD.mtdGetNurseryActivityBlkCode(strOpCd_NurseryActivity_BlkCode_GET, strParam, dsBlkCode)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_NURSERYACTIVITY_BLKCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsBlkCode.Tables(0).Rows.Count - 1
            strBlkCode = Trim(dsBlkCode.Tables(0).Rows(intCnt).Item("BlkCode"))

             strParamSeed = "|1|" & strLocation & "|" & strBlkCode & "|"
            Try
                intErrNo = objBDTrx.mtdGetNurseryActivityTotalSeed(strOppCd_NurseryActivity_TotalSeed_GET, strParamSeed, dsSeed)
            Catch Exp As System.Exception
                Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_NURSERYACTIVITY_TOTALSEED_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If dsSeed.Tables(0).Rows.Count > 0 Then
                decTotalSeed = dsSeed.Tables(0).Rows(0).Item("Qty")
            Else
                decTotalSeed = 0
            End If

            dr = tblTotalSeed.NewRow()
            dr("BlkCode") = strBlkCode
            dr("TotalSeed") = decTotalSeed
            tblTotalSeed.Rows.Add(dr)
        Next

        dsTotalSeed.Tables.Add(tblTotalSeed)

        objRptDs.Tables(0).TableName = "BD_FORMAT"
        objRptDs.Tables.Add(dsTotalSEED.Tables(0).Copy())
        objRptDs.Tables(1).TableName = "BD_TOTALSEED"


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\BD_StdRpt_NurseryActivity.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\BD_StdRpt_NurseryActivity.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/BD_StdRpt_NurseryActivity.pdf"">")
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
        paramField9 = paramFields.Item("lblLocationTag")
        paramField10 = paramFields.Item("lblBlockCodeTag")
        paramField11 = paramFields.Item("ParamBlockCode")

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
        ParamDiscreteValue9.Value = Request.QueryString("lblLocationTag")
        ParamDiscreteValue10.Value = Request.QueryString("lblBlkCodeTag")
        ParamDiscreteValue11.Value = Request.QueryString("BlkCode")

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
