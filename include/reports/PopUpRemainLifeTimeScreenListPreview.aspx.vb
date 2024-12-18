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
        MsgBox("d'")
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

        'If Not Request.QueryString("BlkCode") = "" Then
        '    SearchStr = SearchStr & "AND R.BlkCode LIKE '" & Request.QueryString("BlkCode") & "' AND "
        'Else
        '    SearchStr = SearchStr & "AND R.BlkCode LIKE '%' AND "
        'End If

        'If Not Request.QueryString("SubBlkCode") = "" Then
        '    SearchStr = SearchStr & "G.SubBlkCode LIKE '" & Request.QueryString("SubBlkCode") & "' AND "
        'Else
        '    SearchStr = SearchStr & "G.SubBlkCode LIKE '%' AND "
        'End If

        'If Not Request.QueryString("ReplDate") = "" Then
        '    SearchStr = SearchStr & "(DateDiff(Day, '" & Request.QueryString("ReplDate") & "', R.TransactDate) = 0) AND "
        'End If

        'If Not SearchStr = "" Then
        '    If Right(SearchStr, 4) = "AND " Then
        '        SearchStr = Left(SearchStr, Len(SearchStr) - 4)
        '    End If

        '    strParam = strUserLoc & "|" & objWSSetup.EnumStockItemStatus.Active & "|" & _
        '               SearchStr
        'End If

        Try
            intErrNo = objWS.mtdGetReport_ReplacementScheduleList(strOpCdItem_GET, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_REPLACEMENTSCHEDULE_LIST_REPORT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        'rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\WS_StdRpt_ReplacementScheduleList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\IN_Rpt_RemainLifeTime.rpt", OpenReportMethod.OpenReportByTempCopy)
        'rdCrystalViewer.Load(objMapPath & "Web\en\IN\reports\Crystal\WS_StdRpt_PreventiveMaintenance.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        'PassParam()
        'MsgBox("d")
        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\WS_StdRpt_ReplacementScheduleList.pdf"
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\IN_Rpt_RemainLifeTime.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
           ' .ExportFormatType = ExportFormatType.Excel
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/IN_Rpt_RemainLifeTime.pdf"">")
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
        Dim paramField12 As New ParameterField()
        Dim paramField13 As New ParameterField()
        Dim paramField14 As New ParameterField()
        Dim paramField15 As New ParameterField()
        Dim paramField16 As New ParameterField()

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
        Dim ParamDiscreteValue12 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue13 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue14 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue15 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue16 As New ParameterDiscreteValue()

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
        Dim crParameterValues12 As ParameterValues
        Dim crParameterValues13 As ParameterValues
        Dim crParameterValues14 As ParameterValues
        Dim crParameterValues15 As ParameterValues
        Dim crParameterValues16 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamAccMonth")
        paramField5 = paramFields.Item("ParamAccYear")
        paramField6 = paramFields.Item("ParamRptID")
        paramField7 = paramFields.Item("ParamRptName")
        paramField8 = paramFields.Item("ParamItemCode")
        paramField9 = paramFields.Item("ParamReplDateFrom")
        paramField10 = paramFields.Item("ParamReplDateTo")
        paramField11 = paramFields.Item("lblLocation")
        paramField12 = paramFields.Item("lblBlkTag")
        paramField13 = paramFields.Item("lblSubBlkTag")
        paramField14 = paramFields.Item("ParamDecimal")
        paramField15 = paramFields.Item("ParamBlkCode")
        paramField16 = paramFields.Item("ParamSubBlkCode")

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
        crParameterValues12 = paramField12.CurrentValues
        crParameterValues13 = paramField13.CurrentValues
        crParameterValues14 = paramField14.CurrentValues
        crParameterValues15 = paramField15.CurrentValues
        crParameterValues16 = paramField16.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Session("SS_INACCMONTH")
        ParamDiscreteValue5.Value = Session("SS_INACCYEAR")
        ParamDiscreteValue6.Value = Session("SS_RPTID")
        ParamDiscreteValue7.Value = Session("SS_RPTNAME")
        ParamDiscreteValue8.Value = Session("SS_ITEMCODE")
        ParamDiscreteValue9.Value = Session("SS_REPLDATEFROM")
        ParamDiscreteValue10.Value = Session("SS_REPLDATETO")
        ParamDiscreteValue11.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue12.Value = Session("SS_LBLBLKTAG")
        ParamDiscreteValue13.Value = Session("SS_LBLSUBBLKTAG")
        ParamDiscreteValue14.Value = Session("SS_DECIMAL")
        ParamDiscreteValue15.Value = Session("SS_BLKCODE")
        ParamDiscreteValue16.Value = Session("SS_SUBBLKCODE")

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
        crParameterValues12.Add(ParamDiscreteValue12)
        crParameterValues13.Add(ParamDiscreteValue13)
        crParameterValues14.Add(ParamDiscreteValue14)
        crParameterValues15.Add(ParamDiscreteValue15)
        crParameterValues16.Add(ParamDiscreteValue16)

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
        PFDefs(11).ApplyCurrentValues(crParameterValues12)
        PFDefs(12).ApplyCurrentValues(crParameterValues13)
        PFDefs(13).ApplyCurrentValues(crParameterValues14)
        PFDefs(14).ApplyCurrentValues(crParameterValues15)
        PFDefs(15).ApplyCurrentValues(crParameterValues16)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
