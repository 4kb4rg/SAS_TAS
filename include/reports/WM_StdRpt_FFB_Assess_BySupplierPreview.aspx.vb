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

Public Class WM_StdRpt_FFB_Assess_BySupplierPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objWM As New agri.WM.clsReport()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()

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
    Dim strRptName As String

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

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()

        Dim objRptDsBch As New DataSet()
        Dim objRptDsPct As New DataSet()
        Dim objMapPath As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCd_FFBAssess_BySupplier_Bunch_GET As String = "WM_STDRPT_FFBASSESS_BYSUPPLIER_BUNCH_GET"
        Dim strOpCd_FFBAssess_BySupplier_Pct_GET As String = "WM_STDRPT_FFBASSESS_BYSUPPLIER_PCT_GET"
        Dim strFileName As String

        Dim strParam As String
        Dim SearchStr As String

        If Request.QueryString("QualityType") = "Bunch Count" Then
            strRptName = Request.QueryString("RptName") & " (Quality in Bunch Count)"
            strFileName = "WM_StdRpt_FFBAssessBySupplier_Bunch"
        ElseIf Request.QueryString("QualityType") = "Percentage" Then
            strRptName = Request.QueryString("RptName") & " (Quality in Percentage)"
            strFileName = "WM_StdRpt_FFBAssessBySupplier_Pct"
        End If

        If Not Request.QueryString("SuppCode") = "" Then
            SearchStr = "AND TIC.CustomerCode LIKE '" & Request.QueryString("SuppCode") & "' "
        End If

        If Not Request.QueryString("SuppType") = objPUSetup.EnumSupplierType.All Then
            SearchStr = SearchStr & "AND SUP.SuppType = '" & Request.QueryString("SuppType") & "' "
        End If

        If Not (Request.QueryString("TicketNoFrom") = "" And Request.QueryString("TicketNoTo") = "") Then
            SearchStr = SearchStr & "AND TIC.TicketNo IN (SELECT SUBTIC.TicketNo FROM WM_TICKET SUBTIC WHERE SUBTIC.TicketNo >= '" & Request.QueryString("TicketNoFrom") & _
                        "' AND SUBTIC.TicketNo <= '" & Request.QueryString("TicketNoTo") & "') "
        End If

        If Not (Request.QueryString("InspDateFrom") = "" And Request.QueryString("InspDateTo") = "") Then
            SearchStr = SearchStr & "AND (DateDiff(Day, '" & Request.QueryString("InspDateFrom") & "', FFB.InspectedDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("InspDateTo") & "', FFB.InspectedDate) <= 0) "
        End If

        strParam = strUserLoc & "|" & Request.QueryString("strddlAccMth") & "|" & Request.QueryString("strddlAccYr") & "|" & _
                   objWMTrx.EnumWeighBridgeTicketTransType.Purchase & "|" & objWMTrx.EnumWeighBridgeTicketStatus.Active & "|" & _
                   "AND FFB.Status = '" & objWMTrx.EnumFFBAssessStatus.Active & "'" & SearchStr
        Try
            intErrNo = objWM.mtdGetReport_FFBAssessBySupplier(strOpCd_FFBAssess_BySupplier_Bunch_GET, _
                                                              strOpCd_FFBAssess_BySupplier_Pct_GET, _
                                                              strParam, _
                                                              objRptDsBch, _
                                                              objRptDsPct, _
                                                              objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_FFBASSESS_BYSUPPLIER_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        objRptDsBch.Tables(0).TableName = "WM_FFBASSESS_SUPP_BUNCH"
        If objRptDsPct.Tables(0).Rows.Count > 0 Then
            objRptDsBch.Tables.Add(objRptDsPct.Tables(0).Copy())
            objRptDsBch.Tables(1).TableName = "WM_FFBASSESS_SUPP_PERCENT"
        End If


        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDsBch)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

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
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamRptName")
        paramField6 = paramFields.Item("ParamRptID")
        paramField7 = paramFields.Item("ParamAccMonth")
        paramField8 = paramFields.Item("ParamAccYear")
        paramField9 = paramFields.Item("LblLocation")
        paramField10 = paramFields.Item("ParamSuppCode")
        paramField11 = paramFields.Item("ParamSuppType")
        paramField12 = paramFields.Item("ParamTicketNoFrom")
        paramField13 = paramFields.Item("ParamTicketNoTo")
        paramField14 = paramFields.Item("ParamInspDateFrom")
        paramField15 = paramFields.Item("ParamInspDateTo")
        paramField16 = paramFields.Item("lblVehicleTag")

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
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = strRptName
        ParamDiscreteValue6.Value = Request.QueryString("RptID")
        ParamDiscreteValue7.Value = Request.QueryString("strddlAccMth")
        ParamDiscreteValue8.Value = Request.QueryString("strddlAccYr")
        ParamDiscreteValue9.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue10.Value = Request.QueryString("SuppCode")
        ParamDiscreteValue11.Value = Request.QueryString("SuppType")
        ParamDiscreteValue12.Value = Request.QueryString("TicketNoFrom")
        ParamDiscreteValue13.Value = Request.QueryString("TicketNoTo")
        ParamDiscreteValue14.Value = Request.QueryString("InspDateFrom")
        ParamDiscreteValue15.Value = Request.QueryString("InspDateTo")
        ParamDiscreteValue16.Value = Request.QueryString("lblVehicleTag")

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
