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

Public Class PD_StdRpt_ProdStatPreview : Inherits Page
    Dim objPD As New agri.PD.clsReport()
    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc As String
    Dim strBlkType As String
    Dim strAccMth As String
    Dim strAccYr As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim intDecimal As Integer
    Dim strDateFrom As String
    Dim strDateTo As String

    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strSelAccPeriod As String
    Dim strSelBlk As String
    Dim strSelBlkGrp As String
    Dim strSelSubBlk As String
    Dim strSelLocation As String
    Dim strLocTag As String
    Dim strBlkGrpTag As String
    Dim strBlkTag As String
    Dim strSubBlkTag As String
    Dim strLevel As String

    Dim strRptId As String
    Dim strRptName As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            crvView.Visible = False

            If Right(Request.QueryString("Location"), 1) = "," Then
                strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strUserLoc = Trim(Request.QueryString("Location"))
            End If

            strDDLAccMth = Request.QueryString("DDLAccMth")
            strDDLAccYr = Request.QueryString("DDLAccYr")
            strLevel = Request.QueryString("Level")
            intDecimal = Request.QueryString("Decimal")
            strDateFrom = Trim(Request.QueryString("DateFrom"))
            strDateTo = Trim(Request.QueryString("DateTo"))
            strSelLocation = Trim(Request.QueryString("Location"))
            strSelBlk = Trim(Request.QueryString("Blk"))
            strSelBlkGrp = Trim(Request.QueryString("BlkGrp"))
            strSelSubBlk = Trim(Request.QueryString("SubBlk"))
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))

            strBlkTag = Trim(Request.QueryString("lblBlk"))
            strBlkGrpTag = Trim(Request.QueryString("lblBlkGrp"))
            strSubBlkTag = Trim(Request.QueryString("lblSubBlk"))
            strLevel = Trim(Request.QueryString("Level"))
            strLocTag = Trim(Request.QueryString("lblLocation"))

            BindReport()

        End If
    End Sub


    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer

        Try
            strRptPrefix = "PD_StdRpt_ProdStat"

            strOpCd = "PD_STDRPT_PRODUCTIONSTATISTIC_GET_SP"

            strParam = strSelLocation & "|" & _
                       strSelBlk & "|" & _
                       strSelBlkGrp & "|" & _
                       strSelSubBlk & "|" & _
                       strDDLAccMth & "|" & _
                       strDDLAccYr & "|" & _
                       strLevel & "|" & _
                       strDateFrom & "|" & _
                       strDateTo 

            intErrNo = objPD.mtdGetReport_ProdStat(strOpCd, _
                                                    strParam, _
                                                    objRptDs, _
                                                    objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PD_STDRPT_PRODSTAT_GET_SP&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        PassParam()
        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
    End Sub

    Sub PassParam()
        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParamFieldDef2 As ParameterFieldDefinition
        Dim ParamFieldDef3 As ParameterFieldDefinition
        Dim ParamFieldDef4 As ParameterFieldDefinition
        Dim ParamFieldDef5 As ParameterFieldDefinition
        Dim ParamFieldDef6 As ParameterFieldDefinition
        Dim ParamFieldDef7 As ParameterFieldDefinition
        Dim ParamFieldDef8 As ParameterFieldDefinition
        Dim ParamFieldDef9 As ParameterFieldDefinition
        Dim ParamFieldDef10 As ParameterFieldDefinition
        Dim ParamFieldDef11 As ParameterFieldDefinition
        Dim ParamFieldDef12 As ParameterFieldDefinition
        Dim ParamFieldDef13 As ParameterFieldDefinition
        Dim ParamFieldDef14 As ParameterFieldDefinition
        Dim ParamFieldDef15 As ParameterFieldDefinition

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()
        Dim ParameterValues6 As New ParameterValues()
        Dim ParameterValues7 As New ParameterValues()
        Dim ParameterValues8 As New ParameterValues()
        Dim ParameterValues9 As New ParameterValues()
        Dim ParameterValues10 As New ParameterValues()
        Dim ParameterValues11 As New ParameterValues()
        Dim ParameterValues12 As New ParameterValues()
        Dim ParameterValues13 As New ParameterValues()
        Dim ParameterValues14 As New ParameterValues()
        Dim ParameterValues15 As New ParameterValues()

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

        strSelLocation = Replace(strSelLocation, "','", ", ")

        ParamDiscreteValue1.Value = UCase(strCompanyName)
        ParamDiscreteValue2.Value = UCase(strSelLocation)
        ParamDiscreteValue3.Value = UCase(strUserId)
        ParamDiscreteValue4.Value = strDDLAccMth
        ParamDiscreteValue5.Value = strDDLAccYr
        ParamDiscreteValue6.Value = intDecimal
        ParamDiscreteValue7.Value = UCase(strRptId)
        ParamDiscreteValue8.Value = UCase(strRptName)
        ParamDiscreteValue9.Value = UCase(strLocTag)
        ParamDiscreteValue10.Value = UCase(strBlkGrpTag)
        ParamDiscreteValue11.Value = UCase(strBlkTag)
        ParamDiscreteValue12.Value = UCase(strSubBlkTag)
        ParamDiscreteValue13.Value = strDateFrom
        ParamDiscreteValue14.Value = strDateTo
        ParamDiscreteValue15.Value = strLevel
        

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef9 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef10 = ParamFieldDefs.Item("lblBlkGrp")
        ParamFieldDef11 = ParamFieldDefs.Item("lblBlk")
        ParamFieldDef12 = ParamFieldDefs.Item("lblSubBlk")
        ParamFieldDef13 = ParamFieldDefs.Item("ParamDateFrom")
        ParamFieldDef14 = ParamFieldDefs.Item("ParamDateTo")
        ParamFieldDef15 = ParamFieldDefs.Item("ParamLevel")


        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues
        ParameterValues6 = ParamFieldDef6.CurrentValues
        ParameterValues7 = ParamFieldDef7.CurrentValues
        ParameterValues8 = ParamFieldDef8.CurrentValues
        ParameterValues9 = ParamFieldDef9.CurrentValues
        ParameterValues10 = ParamFieldDef10.CurrentValues
        ParameterValues11 = ParamFieldDef11.CurrentValues
        ParameterValues12 = ParamFieldDef12.CurrentValues
        ParameterValues13 = ParamFieldDef13.CurrentValues
        ParameterValues14 = ParamFieldDef14.CurrentValues
        ParameterValues15 = ParamFieldDef15.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)
        ParameterValues6.Add(ParamDiscreteValue6)
        ParameterValues7.Add(ParamDiscreteValue7)
        ParameterValues8.Add(ParamDiscreteValue8)
        ParameterValues9.Add(ParamDiscreteValue9)
        ParameterValues10.Add(ParamDiscreteValue10)
        ParameterValues11.Add(ParamDiscreteValue11)
        ParameterValues12.Add(ParamDiscreteValue12)
        ParameterValues13.Add(ParamDiscreteValue13)
        ParameterValues14.Add(ParamDiscreteValue14)
        ParameterValues15.Add(ParamDiscreteValue15)

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)
        ParamFieldDef6.ApplyCurrentValues(ParameterValues6)
        ParamFieldDef7.ApplyCurrentValues(ParameterValues7)
        ParamFieldDef8.ApplyCurrentValues(ParameterValues8)
        ParamFieldDef9.ApplyCurrentValues(ParameterValues9)
        ParamFieldDef10.ApplyCurrentValues(ParameterValues10)
        ParamFieldDef11.ApplyCurrentValues(ParameterValues11)
        ParamFieldDef12.ApplyCurrentValues(ParameterValues12)
        ParamFieldDef13.ApplyCurrentValues(ParameterValues13)
        ParamFieldDef14.ApplyCurrentValues(ParameterValues14)
        ParamFieldDef15.ApplyCurrentValues(ParameterValues15)

    End Sub
End Class
