Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class PR_StdRpt_TripTransListPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents EventData As DataGrid

    Dim objPRRpt As New agri.PR.clsReport()
    Dim objPRSetup As New agri.PR.clsSetup()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strLocationName As String
    Dim strUserId As String
    Dim strPrintedBy As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strSelDecimal As String
    Dim strSelLocTag As String
    Dim strFromEmp As String
    Dim strToEmp As String
    Dim strEmpStatus As String
    Dim strRouteCode As String
    Dim strLoadCode As String
    Dim strSrchAccCode As String
    Dim strSrchBlkCode As String
    Dim strSrchBlkGrpCode As String
    Dim strSrchVehCode As String
    Dim strSrchVehExpCode As String
    Dim strSelTransStat As String
    Dim strSelTransStatText As String
    Dim strSortBy As String
    Dim strSortByText As String
    Dim strOrderBy As String

    Dim strEmpStatusText AS String
    Dim strSelLocCode As String
    Dim strLocTag As String
    Dim strVehCodeTag As String
    Dim strVehExpCodeTag As String
    Dim strBlkCodeTag As String
    Dim strAccCodeTag As String
    Dim strBlkGrpCodeTag As String
    Dim strBlkTypeTag As String

    Dim strBlkType As String
    Dim strSrchBlockCode As String

    Dim rdCrystalViewer As ReportDocument

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            crvView.Visible = False

            strSelLocCode = Trim(Request.QueryString("Location"))
            strSelAccMonth = Trim(Request.QueryString("ddlAccMth"))
            strSelAccYear = Trim(Request.QueryString("ddlAccYr"))
            strSelDecimal = Trim(Request.QueryString("Decimal"))
            strFromEmp = Trim(Request.QueryString("FromEmp"))
            strToEmp = Trim(Request.QueryString("ToEmp"))
            strEmpStatus = Trim(Request.QueryString("EmpStatus"))

            strRouteCode = Trim(Request.QueryString("RouteCode"))
            strLoadCode = Trim(Request.QueryString("LoadCode"))

            strSrchAccCode = Trim(Request.QueryString("strSrchAccCode"))
            strSrchBlkCode = Trim(Request.QueryString("strSrchBlkCode"))
            strSrchBlkGrpCode = Trim(Request.QueryString("strSrchBlkGrpCode"))
            strSrchVehCode = Trim(Request.QueryString("strSrchVehCode"))
            strSrchVehExpCode = Trim(Request.QueryString("strSrchVehExpCode"))
            strSelTransStat = Request.QueryString("ddlTransStat")
            strSelTransStatText = Request.QueryString("ddlTransStatText")

            If Request.QueryString("SortBy") = "trp" Then
                strSortBy = "TR.TripId"
            ElseIf Request.QueryString("SortBy") = "emp" Then
                strSortBy = "TR.EmpCode"
            End If

            strSortByText = Request.QueryString("SortByText")
            strOrderBy = Request.QueryString("OrderBy")

            strEmpStatusText = Trim(Request.QueryString("EmpStatusText"))
            strSelLocTag = UCase(Trim(Request.QueryString("lblLocation")))
            strLocTag = Trim(Request.QueryString("lblLocation"))
            strVehCodeTag = Trim(Request.QueryString("lblVehCode"))
            strVehExpCodeTag = Trim(Request.QueryString("lblVehExpCode"))
            strBlkTypeTag = Trim(Request.QueryString("lblBlockTag"))
            strBlkCodeTag = Trim(Request.QueryString("lblBlkCode"))
            strAccCodeTag = Trim(Request.QueryString("lblAccCode"))
            strBlkGrpCodeTag = Trim(Request.QueryString("lblBlkGrpCode"))

            strBlkType = Trim(Request.QueryString("strBlkType"))

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
        Dim intCnt As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim strReportID As String
        Dim tempLoc As String
        Dim intSelDecimal As Integer

        strReportID = "RPTPR1000036"
        intSelDecimal = CInt(strSelDecimal)

        If Right(strSelLocCode, 1) = "," Then
            strSelLocCode = Left(strSelLocCode, Len(strSelLocCode) - 1)
        End If

        Try
            If Request.QueryString("SortBy") = "trp" Then
                strRptPrefix = "PR_StdRpt_TripTrxList"
            ElseIf Request.QueryString("SortBy") = "emp" Then
                strRptPrefix = "PR_StdRpt_TripTrxEmpList"
            End If



            strOpCd = "PR_STDRPT_TRIPTRXLIST_GET" & "|" & objPRRpt.mtdGetPRReportTable(objPRRpt.EnumPRReportTable.Trip) & Chr(9) & _
                      "GET_REPORT_INFO_BY_REPORTID" & "|" & "SH_REPORT"

            If LCase(strBlkType) = "blkcode" Then
                strSrchBlockCode = strSrchBlkCode
            ElseIf LCase(strBlkType) = "blkgrp" Then
                strSrchBlockCode = strSrchBlkGrpCode
            End If

            strParam = strSelLocCode & "|" & _
                       strSelAccMonth & "|" & _
                       strSelAccYear & "|" & _
                       strSelDecimal & "|" & _
                       strFromEmp & "|" & _
                       strToEmp & "|" & _
                       strEmpStatus & "|" & _
                       strRouteCode & "|" & _
                       strLoadCode & "|" & _
                       strBlkType & "|" & _
                       strSrchAccCode & "|" & _
                       strSrchBlockCode & "|" & _
                       strSrchVehCode & "|" & _
                       strSrchVehExpCode & "|" & _
                       strSelTransStat & "|" & _
                       strReportID & "|" & _
                       strSortBy & " " & strOrderBy 

            intErrNo = objPRRpt.mtdGetReport_TripTrxList(strOpCd, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strParam, _
                                                       objRptDs, _
                                                       objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_ALLOWANCE&DEDUCTIONTRXLIST&errmesg=" & Exp.ToString() & "&redirect=")
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

        objRptDs = Nothing
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
        Dim ParamFieldDef22 As ParameterFieldDefinition
        Dim ParamFieldDef23 As ParameterFieldDefinition
        Dim ParamFieldDef24 As ParameterFieldDefinition
        Dim ParamFieldDef25 As ParameterFieldDefinition
        Dim ParamFieldDef26 As ParameterFieldDefinition
        Dim ParamFieldDef27 As ParameterFieldDefinition
        Dim ParamFieldDef28 As ParameterFieldDefinition
        Dim ParamFieldDef29 As ParameterFieldDefinition
        Dim ParamFieldDef30 As ParameterFieldDefinition

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
        Dim ParameterValues22 As New ParameterValues()
        Dim ParameterValues23 As New ParameterValues()
        Dim ParameterValues24 As New ParameterValues()
        Dim ParameterValues25 As New ParameterValues()
        Dim ParameterValues26 As New ParameterValues()
        Dim ParameterValues27 As New ParameterValues()
        Dim ParameterValues28 As New ParameterValues()
        Dim ParameterValues29 As New ParameterValues()
        Dim ParameterValues30 As New ParameterValues()

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
        Dim ParamDiscreteValue29 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue30 As New ParameterDiscreteValue()

        strSelLocCode = Replace(strSelLocCode, "','", ", ")









        

        ParamDiscreteValue1.Value = strSelLocCode
        ParamDiscreteValue2.Value = strSelAccMonth
        ParamDiscreteValue3.Value = strSelAccYear
        ParamDiscreteValue4.Value = strSelDecimal
        ParamDiscreteValue5.Value = strCompanyName
        ParamDiscreteValue6.Value = strPrintedBy
        ParamDiscreteValue7.Value = strSelLocTag
        ParamDiscreteValue8.Value = UCase(strLocTag)
        ParamDiscreteValue9.Value = strRouteCode
        ParamDiscreteValue10.Value = strLoadCode        
        If LCase(strBlkType) = "blkcode" Then
            ParamDiscreteValue11.Value = "Block"
        ElseIf LCase(strBlkType) = "blkgrp" Then
            ParamDiscreteValue11.Value = "Block Group"
        End If
        ParamDiscreteValue12.Value = strSelTransStatText
        ParamDiscreteValue13.Value = strFromEmp
        ParamDiscreteValue14.Value = strToEmp
        ParamDiscreteValue15.Value = strEmpStatusText
        ParamDiscreteValue16.Value = UCase(strAccCodeTag)
        If LCase(strBlkType) = "blkcode" Then
            ParamDiscreteValue17.Value = UCase(strBlkCodeTag)
        ElseIf LCase(strBlkType) = "blkgrp" Then
            ParamDiscreteValue17.Value = UCase(strBlkGrpCodeTag)
        End If
        ParamDiscreteValue18.Value = UCase(strVehCodeTag)
        ParamDiscreteValue19.Value = UCase(strVehExpCodeTag)
        ParamDiscreteValue20.Value = strSrchAccCode
        If LCase(strBlkType) = "blkcode" Then
            ParamDiscreteValue21.Value = strSrchBlkCode
        ElseIf LCase(strBlkType) = "blkgrp" Then
            ParamDiscreteValue21.Value = strSrchBlkGrpCode
        End If
        ParamDiscreteValue22.Value = strSrchVehCode
        ParamDiscreteValue23.Value = strSrchVehExpCode
        ParamDiscreteValue24.Value = strOrderBy        
        ParamDiscreteValue25.Value = strSortByText
        ParamDiscreteValue26.Value = UCase(strBlkTypeTag)
        ParamDiscreteValue27.Value = strAccCodeTag
        If LCase(strBlkType) = "blkcode" Then
            ParamDiscreteValue28.Value = strBlkCodeTag
        ElseIf LCase(strBlkType) = "blkgrp" Then
            ParamDiscreteValue28.Value = strBlkGrpCodeTag
        End If
        ParamDiscreteValue29.Value = strVehCodeTag
        ParamDiscreteValue30.Value = strVehExpCodeTag


        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields








        

        
        ParamFieldDef1 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef2 = ParamFieldDefs.Item("SelAccMonth")
        ParamFieldDef3 = ParamFieldDefs.Item("SelAccYear")    
	    ParamFieldDef4 = ParamFieldDefs.Item("SelDecimal")
	    ParamFieldDef5 = ParamFieldDefs.Item("SessionCompName")
	    ParamFieldDef6 = ParamFieldDefs.Item("SessionPrintedBy")	
	    ParamFieldDef7 = ParamFieldDefs.Item("SelLocTag")
	    ParamFieldDef8 = ParamFieldDefs.Item("LocTag")
        ParamFieldDef9 = ParamFieldDefs.Item("SrchRouteCode") 
        ParamFieldDef10 = ParamFieldDefs.Item("SrchLoadCode")
        ParamFieldDef11 = ParamFieldDefs.Item("BlockCodeTag")        
        ParamFieldDef12 = ParamFieldDefs.Item("SrchTransStat")
	    ParamFieldDef13 = ParamFieldDefs.Item("SrchEmpCodeFrom")
        ParamFieldDef14 = ParamFieldDefs.Item("SrchEmpCodeTo")
        ParamFieldDef15 = ParamFieldDefs.Item("SrchEmpStatusText")         
	    ParamFieldDef16 = ParamFieldDefs.Item("SrchAccCodeTag")	        
        ParamFieldDef17 = ParamFieldDefs.Item("SrchBlkCodeTag")         
        ParamFieldDef18 = ParamFieldDefs.Item("SrchVehCodeTag")
        ParamFieldDef19 = ParamFieldDefs.Item("SrchVehExpCodeTag")        
	    ParamFieldDef20 = ParamFieldDefs.Item("SrchAccCode") 
	    ParamFieldDef21 = ParamFieldDefs.Item("SrchBlkCode")
        ParamFieldDef22 = ParamFieldDefs.Item("SrchVehCode")
        ParamFieldDef23 = ParamFieldDefs.Item("SrchVehExpCode")
        ParamFieldDef24 = ParamFieldDefs.Item("SelOrderBy")
	    ParamFieldDef25 = ParamFieldDefs.Item("SelSortBy")
        ParamFieldDef26 = ParamFieldDefs.Item("BlockTag")
        ParamFieldDef27 = ParamFieldDefs.Item("AccCodeTag")	        
        ParamFieldDef28 = ParamFieldDefs.Item("BlkCodeTag")         
        ParamFieldDef29 = ParamFieldDefs.Item("VehCodeTag")
        ParamFieldDef30 = ParamFieldDefs.Item("VehExpCodeTag")


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
        ParameterValues22 = ParamFieldDef22.CurrentValues
        ParameterValues23 = ParamFieldDef23.CurrentValues
        ParameterValues24 = ParamFieldDef24.CurrentValues
        ParameterValues25 = ParamFieldDef25.CurrentValues
        ParameterValues26 = ParamFieldDef26.CurrentValues
        ParameterValues27 = ParamFieldDef27.CurrentValues
        ParameterValues28 = ParamFieldDef28.CurrentValues
        ParameterValues29 = ParamFieldDef29.CurrentValues
        ParameterValues30 = ParamFieldDef30.CurrentValues


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
        ParameterValues22.Add(ParamDiscreteValue22)
        ParameterValues23.Add(ParamDiscreteValue23)
        ParameterValues24.Add(ParamDiscreteValue24)
        ParameterValues25.Add(ParamDiscreteValue25)
        ParameterValues26.Add(ParamDiscreteValue26)
        ParameterValues27.Add(ParamDiscreteValue27)
        ParameterValues28.Add(ParamDiscreteValue28)
        ParameterValues29.Add(ParamDiscreteValue29)
        ParameterValues30.Add(ParamDiscreteValue30)

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
        ParamFieldDef22.ApplyCurrentValues(ParameterValues22)
        ParamFieldDef23.ApplyCurrentValues(ParameterValues23)
        ParamFieldDef24.ApplyCurrentValues(ParameterValues24)
        ParamFieldDef25.ApplyCurrentValues(ParameterValues25)
        ParamFieldDef26.ApplyCurrentValues(ParameterValues26)
        ParamFieldDef27.ApplyCurrentValues(ParameterValues27)
        ParamFieldDef28.ApplyCurrentValues(ParameterValues28)
        ParamFieldDef29.ApplyCurrentValues(ParameterValues29)
        ParamFieldDef30.ApplyCurrentValues(ParameterValues30)
    End Sub

End Class
