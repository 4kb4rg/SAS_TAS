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

Public Class PR_StdRpt_EpfDeductListPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objPRRpt As New agri.PR.clsReport()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objPWSysConfig As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strLocationName As String
    Dim strUserId As String
    Dim strPrintedBy As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As String
    Dim strEmpCodeFrom As String
    Dim strEmpCodeTo As String
    Dim strGangCode As String
    Dim strSelSupress As String
    Dim strUserLoc As String

    Dim rdCrystalViewer As ReportDocument

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        crvView.Visible = False

        strUserLoc = Trim(Request.QueryString("Location"))
        strSelAccMonth = Request.QueryString("DdlAccMth")
        strSelAccYear = Request.QueryString("DdlAccYr")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strEmpCodeFrom = Request.QueryString("EmpCodeFrom")
        strEmpCodeTo = Request.QueryString("EmpCodeTo")
        strGangCode = Request.QueryString("GangCode")
        strSelSupress = Request.QueryString("Supp")


        Session("SS_LBLLOCATION") = Request.QueryString("lblLocation")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub




    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim strReportID As String
        Dim tempLoc As String

        strReportID = "RPTPR1000010"

        If Right(strUserLoc, 1) = "," Then
            Session("SS_LOC") = Left(strUserLoc, Len(strUserLoc) - 1)
        Else
            Session("SS_LOC") = strUserLoc.Replace("'", "")
        End If

        Try
            strRptPrefix = "PR_StdRpt_EpfDeductList"

            strOpCd = "PR_STDRPT_EPFDEDUCTLIST_GET" & "|" & objPRRpt.mtdGetPRReportTable(objPRRpt.EnumPRReportTable.EpfDeduct) & Chr(9) & _
                      "GET_REPORT_INFO_BY_REPORTID" & "|" & "SH_REPORT"

            strParam = strSelAccMonth & "|" & _
                       strSelAccYear & "|" & _
                       strUserLoc & "|" & _
                       objHRTrx.EnumEmpStatus.Active & "|" & _
                       strEmpCodeFrom & "|" & _
                       strEmpCodeTo & "|" & _
                       strReportID & "|" & _
                       strSelSupress & "|" & _
                       strGangCode

            intErrNo = objPRRpt.mtdGetReport_EpfDeductList(strOpCd, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           objRptDs, _
                                                           objMapPath)



        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_EPFDEDUCTLIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3


        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                               

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



















    Sub BindReport_old()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim strReportID As String
        Dim tempLoc As String

        strReportID = "RPTPR1000010"

        If Right(strUserLoc, 1) = "," Then
            Session("SS_LOC") = Left(strUserLoc, Len(strUserLoc) - 1)
        Else
            Session("SS_LOC") = strUserLoc.Replace("'", "")
        End If

        Try
            strRptPrefix = "PR_StdRpt_EpfDeductList"

            strOpCd = "PR_STDRPT_EPFDEDUCT_EMP_GET" & "|" & objPRRpt.mtdGetPRReportTable(objPRRpt.EnumPRReportTable.EpfDeduct) & Chr(9) & _
                      "GET_REPORT_INFO_BY_REPORTID" & "|" & "SH_REPORT"

            strParam = strSelAccMonth & "|" & _
                       strSelAccYear & "|" & _
                       strUserLoc & "|" & _
                       objHRTrx.EnumEmpStatus.Active & "|" & _
                       strEmpCodeFrom & "|" & _
                       strEmpCodeTo & "|" & _
                       strReportID & "|" & _
                       strSelSupress

            intErrNo = objPRRpt.mtdGetReport_EpfDeductList(strOpCd, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           objRptDs, _
                                                           objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_EPFDEDUCTLIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3


        crvView.Visible = False                          
        crvView.ReportSource = rdCrystalViewer           
        crvView.DataBind()                               

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = strAccMonth
        ParamDiscreteValue5.Value = strAccYear
        ParamDiscreteValue6.Value = intSelDecimal
        ParamDiscreteValue7.Value = strSelAccMonth
        ParamDiscreteValue8.Value = strSelAccYear
        ParamDiscreteValue9.Value = strEmpCodeFrom
        ParamDiscreteValue10.Value = strEmpCodeTo
        ParamDiscreteValue11.Value = strSelSupress
        ParamDiscreteValue12.Value = Session("SS_LBLLOCATION")
        ParamDiscreteValue13.Value = strGangCode

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamSelDecimal")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamSelAccMonth")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamSelAccYear")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamEmpCodeFrom")
        ParamFieldDef10 = ParamFieldDefs.Item("ParamEmpCodeTo")
        ParamFieldDef11 = ParamFieldDefs.Item("ParamSelSuppress")
        ParamFieldDef12 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef13 = ParamFieldDefs.Item("SrchGangCode")

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
    End Sub

End Class
