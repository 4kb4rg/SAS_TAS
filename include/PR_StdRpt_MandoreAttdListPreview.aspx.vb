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

Public Class PR_StdRpt_MandoreAttdListPreview : Inherits Page
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents EventData As DataGrid
    Dim rdCrystalViewer As ReportDocument

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
    Dim intConfig As Integer
    Dim strInterEstateEnabled As String
    
    Dim strRptTitle As String
    Dim strReportID As String
    Dim strSelLocCode As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strSelDecimal As String
    Dim strFromEmp As String
    Dim strToEmp As String    
    Dim strEmpStatus As String
    Dim strEmpStatusText AS String
    Dim strDocNoFrom As String
    Dim strDocNoTo As String
    Dim strGangCode As String
    Dim strSrchAccCode As String
    Dim strSrchBlkCode As String
    Dim strSrchSubBlkCode As String
    Dim strSrchBlkGrpCode As String
    Dim strSrchVehCode As String
    Dim strSrchVehExpCode As String
    Dim strCostLevel As String
    Dim strBlkType As String

    Dim strLocTag As String
    Dim strVehCodeTag As String
    Dim strVehExpCodeTag As String
    Dim strBlkCodeTag As String
    Dim strAccCodeTag As String
    Dim strSubBlkCodeTag As String
    Dim strBlkGrpCodeTag As String
    
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
        intConfig = Session("SS_CONFIGSETTING")
        strCostLevel = Session("SS_COSTLEVEL")
        strInterEstateEnabled = IIF(Session("SS_INTER_ESTATE_CHARGING"), "True", "False")

        strReportID = Trim(Request.QueryString("RptID"))
        strRptTitle = Trim(Request.QueryString("RptTitle"))
        strSelLocCode = Trim(Request.QueryString("Location"))
        strSelAccMonth = Request.QueryString("SelAccMonth")
        strSelAccYear = Request.QueryString("SelAccYear")
        strSelDecimal = Request.QueryString("Decimal")
        strFromEmp = Request.QueryString("FromEmp")
        strToEmp = Request.QueryString("ToEmp")
        strEmpStatus = Request.QueryString("Status")
        strEmpStatusText = Trim(Request.QueryString("StatusText"))
        strDocNoFrom = Trim(Request.QueryString("DocNoFrom"))
        strDocNoTo = Trim(Request.QueryString("DocNoTo"))
        strGangCode = Trim(Request.QueryString("GangCode"))
        strSrchAccCode = Trim(Request.QueryString("strSrchAccCode"))
        strSrchBlkCode = Trim(Request.QueryString("strSrchBlkCode"))
        strSrchSubBlkCode = Trim(Request.QueryString("strSrchSubBlkCode"))
        strSrchBlkGrpCode = Trim(Request.QueryString("strSrchBlkGrpCode"))
        strSrchVehCode = Trim(Request.QueryString("strSrchVehCode"))
        strSrchVehExpCode = Trim(Request.QueryString("strSrchVehExpCode"))
        strBlkType = Trim(Request.QueryString("strBlkType"))

        strLocTag = Trim(Request.QueryString("lblLocation"))
        strVehCodeTag = Trim(Request.QueryString("lblVehCode"))
        strVehExpCodeTag = Trim(Request.QueryString("lblVehExpCode"))
        strBlkCodeTag = Trim(Request.QueryString("lblBlkCode"))
        strAccCodeTag = Trim(Request.QueryString("lblAccCode"))
        strSubBlkCodeTag = Trim(Request.QueryString("lblSubBlkCode"))
        strBlkGrpCodeTag = Trim(Request.QueryString("lblBlkGrpCode"))

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
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intSelDecimal As Integer

        intSelDecimal = CInt(strSelDecimal)

        If Right(strSelLocCode, 1) = "," Then
            strSelLocCode = Left(strSelLocCode, Len(strSelLocCode) - 1)
        End If

        Try
            strRptPrefix = "PR_StdRpt_MandoreAttdList"

            strOpCd = "PR_STDRPT_MANDOREATTDLIST_SP" & "|" & objPRRpt.mtdGetPRReportTable(objPRRpt.EnumPRReportTable.MandoreAttdList)
            
            strParam = Replace(strSelLocCode, "'", "''") & "|" & _
                       strSelAccMonth & "|" & _
                       strSelAccYear & "|" & _
                       strFromEmp & "|" & _
                       strToEmp & "|" & _
                       strEmpStatus & "|" & _
                       strDocNoFrom & "|" & _
                       strDocNoTo & "|" & _
                       strGangCode & "|" & _
                       strSrchAccCode & "|" & _
                       strSrchSubBlkCode & "|" & _
                       strSrchBlkCode & "|" & _
                       strSrchBlkGrpCode & "|" & _
                       strSrchVehCode & "|" & _
                       strSrchVehExpCode & "|" & _
                       strCostLevel


            intErrNo = objPRRpt.mtdGetReport_MandoreAttdList(strOpCd, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strParam, _
                                                             objRptDs, _
                                                             objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_MANDOREATTDLIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
  
        EventData.DataSource = objRptDs.Tables(0)
        EventData.DataBind()
        Exit Sub

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

        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strLocationName
        ParamDiscreteValue3.Value = strReportID
        ParamDiscreteValue4.Value = strRptTitle
        ParamDiscreteValue5.Value = strPrintedBy
        ParamDiscreteValue6.Value = strSelLocCode
        ParamDiscreteValue7.Value = strSelAccMonth
        ParamDiscreteValue8.Value = strSelAccYear
        ParamDiscreteValue9.Value = strSelDecimal
        ParamDiscreteValue10.Value = strFromEmp
        ParamDiscreteValue11.Value = strToEmp
        ParamDiscreteValue12.Value = strEmpStatusText
        ParamDiscreteValue13.Value = strDocNoFrom
        ParamDiscreteValue14.Value = strDocNoTo
        ParamDiscreteValue15.Value = strGangCode
        ParamDiscreteValue16.Value = strSrchAccCode
        If LCase(strBlkType) = "blkcode" Then
            ParamDiscreteValue17.Value = strSrchBlkCode
        ElseIf LCase(strBlkType) = "blkgrp" Then
            ParamDiscreteValue17.Value = strSrchBlkGrpCode
        Else
            ParamDiscreteValue17.Value = strSrchSubBlkCode
        End If
        ParamDiscreteValue18.Value = strSrchVehCode
        ParamDiscreteValue19.Value = strSrchVehExpCode
        ParamDiscreteValue20.Value = strInterEstateEnabled

        ParamDiscreteValue21.Value = UCase(strLocTag)
        ParamDiscreteValue22.Value = UCase(strAccCodeTag)
        If LCase(strBlkType) = "blkcode" Then
            ParamDiscreteValue23.Value = UCase(strBlkCodeTag)
        ElseIf LCase(strBlkType) = "blkgrp" Then
            ParamDiscreteValue23.Value = UCase(strBlkGrpCodeTag)
        Else
            ParamDiscreteValue23.Value = UCase(strSubBlkCodeTag)
        End If
        ParamDiscreteValue24.Value = UCase(strVehCodeTag)
        ParamDiscreteValue25.Value = UCase(strVehExpCodeTag)
        ParamDiscreteValue26.Value = strLocTag
        ParamDiscreteValue27.Value = strAccCodeTag
        If LCase(strBlkType) = "subblkcode" Then
            ParamDiscreteValue28.Value = strSubBlkCodeTag
        Else
            ParamDiscreteValue28.Value = strBlkCodeTag
        End If        
        ParamDiscreteValue29.Value = strVehCodeTag
        ParamDiscreteValue30.Value = strVehExpCodeTag

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SessionCompName")
        ParamFieldDef2 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef3 = ParamFieldDefs.Item("ReportID")
        ParamFieldDef4 = ParamFieldDefs.Item("RptTitle")
        ParamFieldDef5 = ParamFieldDefs.Item("SessionPrintedBy")
        ParamFieldDef6 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef7 = ParamFieldDefs.Item("SelAccMonth")
        ParamFieldDef8 = ParamFieldDefs.Item("SelAccYear")
        ParamFieldDef9 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef10 = ParamFieldDefs.Item("SrchEmpCodeFrom")
        ParamFieldDef11 = ParamFieldDefs.Item("SrchEmpCodeTo")
        ParamFieldDef12 = ParamFieldDefs.Item("SrchStatus")
        ParamFieldDef13 = ParamFieldDefs.Item("SrchDoNoFrom")
        ParamFieldDef14 = ParamFieldDefs.Item("SrchDocNoTo")
        ParamFieldDef15 = ParamFieldDefs.Item("SrchGangCode")
        ParamFieldDef16 = ParamFieldDefs.Item("SrchAccCode")
        ParamFieldDef17 = ParamFieldDefs.Item("SrchBlkCode")
        ParamFieldDef18 = ParamFieldDefs.Item("SrchVehCode")
        ParamFieldDef19 = ParamFieldDefs.Item("SrchVehExpCode")
        ParamFieldDef20 = ParamFieldDefs.Item("SessionInterEstate")
        ParamFieldDef21 = ParamFieldDefs.Item("ULocTag")         
        ParamFieldDef22 = ParamFieldDefs.Item("UAccCodeTag")    
        ParamFieldDef23 = ParamFieldDefs.Item("UBlkCodeTag")    
        ParamFieldDef24 = ParamFieldDefs.Item("UVehCodeTag")    
        ParamFieldDef25 = ParamFieldDefs.Item("UVehExpCodeTag") 
        ParamFieldDef26 = ParamFieldDefs.Item("NLocTag")         
        ParamFieldDef27 = ParamFieldDefs.Item("NAccCodeTag")    
        ParamFieldDef28 = ParamFieldDefs.Item("NBlkCodeTag")    
        ParamFieldDef29 = ParamFieldDefs.Item("NVehCodeTag")    
        ParamFieldDef30 = ParamFieldDefs.Item("NVehExpCodeTag") 

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
