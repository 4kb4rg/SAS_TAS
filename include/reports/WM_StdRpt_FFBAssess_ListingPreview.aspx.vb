Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class WM_StdRpt_FFBAssess_ListingPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objWM As New agri.WM.clsReport()
    Dim objWMFFBrx As New agri.WM.clsTrx()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String

    Dim tempLoc As String
    Dim strDecimal As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        If Right(Request.QueryString("Location"), 1) = "," Then
            strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
        End If

        tempLoc = Request.QueryString("Location")
        If Right(tempLoc, 1) = "," Then
            Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
        Else
            Session("SS_LOC") = tempLoc.Replace("','", ", ")
        End If

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

        Dim objRptDs As New DataSet()
        Dim objMapPath As String

        Dim SearchStr As String
        Dim strStatus As String
        Dim strParam As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOppCode_Get As String = "WM_STDRPT_FFBASSESS_LIST"

        If Not (Request.QueryString("TicketNoFrom") = "" And Request.QueryString("TicketNoTo") = "") Then
            SearchStr = " AND WMFFB.TICKETNO >= '" & Request.QueryString("TicketNoFrom") & "' AND WMFFB.TICKETNO <= '" & Request.QueryString("TicketNoTo") & "'"
        End If

        If Not (Request.QueryString("InspDateFrom") = "" And Request.QueryString("InspDateTo") = "") Then
            SearchStr = SearchStr & " AND (DateDiff(Day, '" & Request.QueryString("InspDateFrom") & "', WMFFB.InspectedDate) >= 0) And (DateDiff(Day, '" & Request.QueryString("InspDateTo") & "', WMFFB.InspectedDate) <= 0)"
        End If

        If Not Request.QueryString("Ripe") = "" Then
            SearchStr = SearchStr & " AND WMFFB.RipeBunch LIKE '" & Request.QueryString("Ripe") & "'"
        End If

        If Not Request.QueryString("OverRipe") = "" Then
            SearchStr = SearchStr & " AND WMFFB.OverRipeBunch LIKE '" & Request.QueryString("OverRipe") & "'"
        End If

        If Not Request.QueryString("UnderRipe") = "" Then
            SearchStr = SearchStr & " AND WMFFB.UnderRipeBunch LIKE '" & Request.QueryString("UnderRipe") & "'"
        End If

        If Not Request.QueryString("Unripe") = "" Then
            SearchStr = SearchStr & " AND WMFFB.UnripeBunch LIKE '" & Request.QueryString("Unripe") & "'"
        End If

        If Not Request.QueryString("Empty") = "" Then
            SearchStr = SearchStr & " AND WMFFB.EmptyBunch LIKE '" & Request.QueryString("Empty") & "'"
        End If

        If Not Request.QueryString("Rotten") = "" Then
            SearchStr = SearchStr & " AND WMFFB.RottenBunch LIKE '" & Request.QueryString("Rotten") & "'"
        End If

        If Not Request.QueryString("Poor") = "" Then
            SearchStr = SearchStr & " AND WMFFB.PoorBunch LIKE '" & Request.QueryString("Poor") & "'"
        End If

        If Not Request.QueryString("Small") = "" Then
            SearchStr = SearchStr & " AND WMFFB.SmallBunch LIKE '" & Request.QueryString("Small") & "'"
        End If

        If Not Request.QueryString("LongStalk") = "" Then
            SearchStr = SearchStr & " AND WMFFB.LongStalkBunch LIKE '" & Request.QueryString("LongStalk") & "'"
        End If

         If Not Request.QueryString("Contamination") = "" Then
            SearchStr = SearchStr & " AND WMFFB.Contamination LIKE '" & Request.QueryString("Contamination") & "'"
         End If

         If Not Request.QueryString("Others") = "" Then
            SearchStr = SearchStr & " AND WMFFB.Others LIKE '" & Request.QueryString("Others") & "'"
         End If

        If Not Request.QueryString("Total") = "" Then
            SearchStr = SearchStr & " AND WMFFB.GradedBunch LIKE '" & Request.QueryString("Total") & "'"
        End If

        If Not Request.QueryString("GradedPercent") = "" Then
            SearchStr = SearchStr & " AND WMFFB.GradedPercent LIKE '" & Request.QueryString("GradedPercent") & "'"
        End If

        If Not Request.QueryString("UngradableBunch") = "" Then
            SearchStr = SearchStr & " AND WMFFB.UngradableBunch LIKE '" & Request.QueryString("UngradableBunch") & "'"
        End If

        If Request.QueryString("Status") = "All" Then
        ElseIf Request.QueryString("Status") = "Active" Then
            strStatus = " AND WMFFB.Status = '" & objWMFFBrx.EnumFFBAssessStatus.Active & "'"
        ElseIf Request.QueryString("Status") = "Deleted" Then
            strStatus = " AND WMFFB.Status = '" & objWMFFBrx.EnumFFBAssessStatus.Deleted & "'"
        End If

        strParam = strUserLoc & "|" & Request.QueryString("DDLAccMth") & "|" & Request.QueryString("DDLAccYear") & "|" & strStatus & "|" & SearchStr
        Try
            intErrNo = objWM.mtdGetReport_FFBAssessmentList(strOppCode_Get, strParam, objRptDs, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_Stdrpt_FFBAssess_List&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        rdCrystalViewer.Load(objMapPath & "web\en\Reports\Crystal\WM_Stdrpt_FFBAssessList.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False                         
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              
        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\WM_Stdrpt_FFBAssessList.pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
 	rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/WM_Stdrpt_FFBAssessList.pdf"">")
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
        Dim paramField17 As New ParameterField()
        Dim paramField18 As New ParameterField()
        Dim paramField19 As New ParameterField()
        Dim paramField20 As New ParameterField()
        Dim paramField21 As New ParameterField()
        Dim paramField22 As New ParameterField()
        Dim paramField23 As New ParameterField()
        Dim paramField24 As New ParameterField()
        Dim paramField25 As New ParameterField()
        Dim paramField26 As New ParameterField()
        Dim paramField27 As New ParameterField()
        Dim paramField28 As New ParameterField()


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
        Dim ParamDiscreteValue17 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue18 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue19 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue20 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue21 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue22 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue23 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue24 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue25 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue26 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue27 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue28 As New ParameterDiscreteValue()

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
        Dim crParameterValues17 As ParameterValues
        Dim crParameterValues18 As ParameterValues
        Dim crParameterValues19 As ParameterValues
        Dim crParameterValues20 As ParameterValues
        Dim crParameterValues21 As ParameterValues
        Dim crParameterValues22 As ParameterValues
        Dim crParameterValues23 As ParameterValues
        Dim crParameterValues24 As ParameterValues
        Dim crParameterValues25 As ParameterValues
        Dim crParameterValues26 As ParameterValues
        Dim crParameterValues27 As ParameterValues
        Dim crParameterValues28 As ParameterValues


        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("ParamUserName")
        paramField4 = paramFields.Item("ParamDecimal")
        paramField5 = paramFields.Item("ParamTicketNoFrom")
        paramField6 = paramFields.Item("ParamTicketNoTo")
        paramField7 = paramFields.Item("ParamRptName")
        paramField8 = paramFields.Item("ParamStatus")
        paramField9 = paramFields.Item("ParamRptID")
        paramField10 = paramFields.Item("ParamRipe")
        paramField11 = paramFields.Item("ParamOverRipe")
        paramField12 = paramFields.Item("ParamUnderRipe")
        paramField13 = paramFields.Item("ParamUnripe")
        paramField14 = paramFields.Item("ParamEmpty")
        paramField15 = paramFields.Item("ParamRotten")
        paramField16 = paramFields.Item("ParamPoor")
        paramField17 = paramFields.Item("ParamSmall")
        paramField18 = paramFields.Item("ParamLongStalk")
        paramField19 = paramFields.Item("ParamTotal")
        paramField20 = paramFields.Item("ParamGradedPercent")
        paramField21 = paramFields.Item("ParamUngradableBunch")
        paramField22 = paramFields.Item("ParamLblLocation")
        paramField23 = paramFields.Item("ParamAccMth")
        paramField24 = paramFields.Item("ParamAccYear")
        paramField25 = paramFields.Item("ParamInspDateFrom")
        paramField26 = paramFields.Item("ParamInspDateTo")
        paramField27 = paramFields.Item("ParamContamination")
        paramField28 = paramFields.Item("ParamOthers")



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
        crParameterValues17 = paramField17.CurrentValues
        crParameterValues18 = paramField18.CurrentValues
        crParameterValues19 = paramField19.CurrentValues
        crParameterValues20 = paramField20.CurrentValues
        crParameterValues21 = paramField21.CurrentValues
        crParameterValues22 = paramField22.CurrentValues
        crParameterValues23 = paramField23.CurrentValues
        crParameterValues24 = paramField24.CurrentValues
        crParameterValues25 = paramField25.CurrentValues
        crParameterValues26 = paramField26.CurrentValues
        crParameterValues27 = paramField27.CurrentValues
        crParameterValues28 = paramField28.CurrentValues


        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = Request.QueryString("Decimal")
        ParamDiscreteValue5.Value = Request.QueryString("TicketNoFrom")
        ParamDiscreteValue6.Value = Request.QueryString("TicketNoTo")
        ParamDiscreteValue7.Value = UCase(Request.QueryString("RptName"))
        ParamDiscreteValue8.Value = Request.QueryString("Status")
        ParamDiscreteValue9.Value = Request.QueryString("RptID")
        ParamDiscreteValue10.Value = Request.QueryString("ripe")
        ParamDiscreteValue11.Value = Request.QueryString("OverRipe")
        ParamDiscreteValue12.Value = Request.QueryString("UnderRipe")
        ParamDiscreteValue13.Value = Request.QueryString("Unripe")
        ParamDiscreteValue14.Value = Request.QueryString("Empty")
        ParamDiscreteValue15.Value = Request.QueryString("Rotten")
        ParamDiscreteValue16.Value = Request.QueryString("Poor")
        ParamDiscreteValue17.Value = Request.QueryString("Small")
        ParamDiscreteValue18.Value = Request.QueryString("LongStalk")
        ParamDiscreteValue19.Value = Request.QueryString("Total")
        ParamDiscreteValue20.Value = Request.QueryString("GradedPercent")
        ParamDiscreteValue21.Value = Request.QueryString("UngradableBunch")
        ParamDiscreteValue22.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue23.Value = Request.QueryString("DDLAccMth")
        ParamDiscreteValue24.Value = Request.QueryString("DDLAccYear")
        ParamDiscreteValue25.Value = Request.QueryString("InspDateFrom")
        ParamDiscreteValue26.Value = Request.QueryString("InspDateTo")
        ParamDiscreteValue27.Value = Request.QueryString("Contamination")
        ParamDiscreteValue28.Value = Request.QueryString("Others")

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
        crParameterValues17.Add(ParamDiscreteValue17)
        crParameterValues18.Add(ParamDiscreteValue18)
        crParameterValues19.Add(ParamDiscreteValue19)
        crParameterValues20.Add(ParamDiscreteValue20)
        crParameterValues21.Add(ParamDiscreteValue21)
        crParameterValues22.Add(ParamDiscreteValue22)
        crParameterValues23.Add(ParamDiscreteValue23)
        crParameterValues24.Add(ParamDiscreteValue24)
        crParameterValues25.Add(ParamDiscreteValue25)
        crParameterValues26.Add(ParamDiscreteValue26)
        crParameterValues27.Add(ParamDiscreteValue27)
        crParameterValues28.Add(ParamDiscreteValue28)


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
        PFDefs(16).ApplyCurrentValues(crParameterValues17)
        PFDefs(17).ApplyCurrentValues(crParameterValues18)
        PFDefs(18).ApplyCurrentValues(crParameterValues19)
        PFDefs(19).ApplyCurrentValues(crParameterValues20)
        PFDefs(20).ApplyCurrentValues(crParameterValues21)
        PFDefs(21).ApplyCurrentValues(crParameterValues22)
        PFDefs(22).ApplyCurrentValues(crParameterValues23)
        PFDefs(23).ApplyCurrentValues(crParameterValues24)
        PFDefs(24).ApplyCurrentValues(crParameterValues25)
        PFDefs(25).ApplyCurrentValues(crParameterValues26)
        PFDefs(26).ApplyCurrentValues(crParameterValues27)
        PFDefs(27).ApplyCurrentValues(crParameterValues28)

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
