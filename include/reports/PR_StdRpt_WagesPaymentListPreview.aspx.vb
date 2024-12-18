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

Public Class PR_StdRpt_WagesPaymentListPreview : Inherits Page

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

    Dim strUserLoc As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As String
    Dim strCompCode As String
    Dim strLocCode As String
    Dim strEmpCodeFrom As String
    Dim strEmpCodeTo As String
    Dim strGangCode As String
    Dim strDeptCode As String
    Dim strStatus As String
    Dim strStatus_Value As String
    Dim strPayMode As String
    Dim strPayMode_Value As String
    Dim strThumbPrint As String
    Dim strRptTitle As String
    Dim strReportID As String

    Dim rdCrystalViewer As ReportDocument

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        crvView.Visible = False

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

        strUserLoc = Trim(Request.QueryString("Location"))
        strSelAccMonth = Request.QueryString("DdlAccMth")
        strSelAccYear = Request.QueryString("DdlAccYr")
        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strCompCode = Request.QueryString("CompCode")
        strLocCode = Request.QueryString("LocCode")
        strEmpCodeFrom = Request.QueryString("EmpCodeFrom")
        strEmpCodeTo = Request.QueryString("EmpCodeTo")
        strGangCode = Request.QueryString("GangCode")
        strDeptCode = Request.QueryString("DeptCode")
        strStatus = Request.QueryString("Status")
        strStatus_Value = Request.QueryString("Status_Value")
        strPayMode = Request.QueryString("PayMode")
        strPayMode_Value = Request.QueryString("PayMode_Value")        
        strThumbPrint = Request.QueryString("ThumbPrint")
        strRptTitle = Request.QueryString("RptName")

        strReportID = "RPTPR1000024"

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
        Dim tempLoc As String

        If Right(strUserLoc, 1) = "," Then
            Session("SS_LOC") = Left(strUserLoc, Len(strUserLoc) - 1)
        Else
            Session("SS_LOC") = strUserLoc.Replace("'", "")
        End If

        Try
            strRptPrefix = "PR_StdRpt_WagesPaymentList"

            strOpCd = "PR_STDRPT_WAGESPAYMENT_GET" & "|" & objPRRpt.mtdGetPRReportTable(objPRRpt.EnumPRReportTable.WagesPaymentList) & Chr(9) & _
                      "GET_REPORT_INFO_BY_REPORTID" & "|" & "SH_REPORT"

            strParam = strSelAccMonth & "|" & _
                       strSelAccYear & "|" & _
                       strUserLoc & "|" & _
                       strCompCode & "|" & _
                       strLocCode & "|" & _
                       strEmpCodeFrom & "|" & _
                       strEmpCodeTo & "|" & _
                       strDeptCode & "|" & _
                       strStatus_Value & "|" & _
                       strPayMode_Value & "|" & _ 
                       strReportID & "|" & _
                       strGangCode

            intErrNo = objPRRpt.mtdGetReport_WagesPaymentList(strOpCd, _
                                                              strCompany, _
                                                              strLocation, _
                                                              strUserId, _
                                                              strAccMonth, _
                                                              strAccYear, _
                                                              strParam, _
                                                              objRptDs, _
                                                              objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_WAGESPAYMENTLIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
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
        Dim ParamFieldDef14 As ParameterFieldDefinition
        Dim ParamFieldDef15 As ParameterFieldDefinition
        Dim ParamFieldDef16 As ParameterFieldDefinition
        Dim ParamFieldDef17 As ParameterFieldDefinition
        Dim ParamFieldDef18 As ParameterFieldDefinition
        Dim ParamFieldDef19 As ParameterFieldDefinition
        Dim ParamFieldDef20 As ParameterFieldDefinition
        Dim ParamFieldDef21 As ParameterFieldDefinition

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
        Dim ParameterValues16 As New ParameterValues()
        Dim ParameterValues17 As New ParameterValues()
        Dim ParameterValues18 As New ParameterValues()
        Dim ParameterValues19 As New ParameterValues()
        Dim ParameterValues20 As New ParameterValues()
        Dim ParameterValues21 As New ParameterValues()

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

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = Session("SS_USERNAME")
        ParamDiscreteValue4.Value = strAccMonth
        ParamDiscreteValue5.Value = strAccYear
        ParamDiscreteValue6.Value = intSelDecimal
        ParamDiscreteValue7.Value = strSelAccMonth
        ParamDiscreteValue8.Value = strSelAccYear
        ParamDiscreteValue9.Value = strCompCode
        ParamDiscreteValue10.Value = strLocCode
        ParamDiscreteValue11.Value = strEmpCodeFrom
        ParamDiscreteValue12.Value = strEmpCodeTo
        ParamDiscreteValue13.Value = strDeptCode
        ParamDiscreteValue14.Value = strStatus
        ParamDiscreteValue15.Value = Request.QueryString("lblCompany")
        ParamDiscreteValue16.Value = Request.QueryString("lblLocation")
        ParamDiscreteValue17.Value = Request.QueryString("lblDepartment")
        ParamDiscreteValue18.Value = strGangCode
        ParamDiscreteValue19.Value = UCase(strRptTitle)
        ParamDiscreteValue20.Value = strReportID
        ParamDiscreteValue21.Value = strThumbPrint

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamAccMonth")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamAccYear")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamSelDecimal")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamSelAccMonth")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamSelAccYear")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamCompCode")
        ParamFieldDef10 = ParamFieldDefs.Item("ParamLocCode")
        ParamFieldDef11 = ParamFieldDefs.Item("ParamEmpCodeFrom")
        ParamFieldDef12 = ParamFieldDefs.Item("ParamEmpCodeTo")
        ParamFieldDef13 = ParamFieldDefs.Item("ParamDeptCode")
        ParamFieldDef14 = ParamFieldDefs.Item("ParamStatus")
        ParamFieldDef15 = ParamFieldDefs.Item("lblCompany")
        ParamFieldDef16 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef17 = ParamFieldDefs.Item("lblDepartment")
        ParamFieldDef18 = ParamFieldDefs.Item("SrchGangCode")
        ParamFieldDef19 = ParamFieldDefs.Item("ParamRptTitle")
        ParamFieldDef20 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef21 = ParamFieldDefs.Item("ParamThumbPrint")

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
        ParameterValues16 = ParamFieldDef16.CurrentValues
        ParameterValues17 = ParamFieldDef17.CurrentValues
        ParameterValues18 = ParamFieldDef18.CurrentValues
        ParameterValues19 = ParamFieldDef19.CurrentValues
        ParameterValues20 = ParamFieldDef20.CurrentValues
        ParameterValues21 = ParamFieldDef21.CurrentValues

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
        ParameterValues16.Add(ParamDiscreteValue16)
        ParameterValues17.Add(ParamDiscreteValue17)
        ParameterValues18.Add(ParamDiscreteValue18)
        ParameterValues19.Add(ParamDiscreteValue19)
        ParameterValues20.Add(ParamDiscreteValue20)
        ParameterValues21.Add(ParamDiscreteValue21)

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
        ParamFieldDef16.ApplyCurrentValues(ParameterValues16)
        ParamFieldDef17.ApplyCurrentValues(ParameterValues17)
        ParamFieldDef18.ApplyCurrentValues(ParameterValues18)
        ParamFieldDef19.ApplyCurrentValues(ParameterValues19)
        ParamFieldDef20.ApplyCurrentValues(ParameterValues20)
        ParamFieldDef21.ApplyCurrentValues(ParameterValues21)

    End Sub

End Class
