Imports System
Imports System.Data
Imports System.Collections 
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

Imports agri.PWSystem.clsLangCap
Imports agri.IN.clsSetup

Public Class GL_StdRpt_VehRunExpensePreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblCode As Label

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objINSetup As New agri.IN.clsSetup()

    Dim objLangCapDs As New Object()
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

    Dim strRptId As String
    Dim strRptName As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strSelSupress As String
    Dim strSelLocation As String
    
    Dim strSrchVehTypeCode As String
    Dim strSrchVehCode As String
    Dim strSearchExp As String = ""
    Dim strVehTypeCodeTag As String = ""
    Dim strVehCodeTag As String = ""
    Dim strVehExpTag As String = ""
    Dim strVehExpDescTag As String = ""
    Dim strULocTag As String = ""
    Dim strLocTag As String = ""
    Dim strVehTag As String = ""

    Dim objPeriod As String = ""
    Dim arrPeriod As Array
    Dim strPeriod1 As String
    Dim strPeriod2 As String
    Dim strPeriod3 As String
    Dim strPeriod4 As String
    Dim strPeriod5 As String
    Dim strPeriod6 As String
    Dim strPeriod7 As String
    Dim strPeriod8 As String
    Dim strPeriod9 As String
    Dim strPeriod10 As String
    Dim strPeriod11 As String
    Dim strPeriod12 As String
    
    Dim rdCrystalViewer As ReportDocument
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        strUserId = Session("SS_USERID")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Request.QueryString("DDLAccMth")
            strSelAccYear = Request.QueryString("DDLAccYr")
            intSelDecimal= CInt(Request.QueryString("Decimal"))
            strSelSupress = Request.QueryString("Supp")

            strSrchVehTypeCode = Trim(Request.QueryString("SrchVehTypeCode"))
            strSrchVehCode = Trim(Request.QueryString("SrchVehCode"))

            If Right(Request.QueryString("Location"), 1) = "," Then
                strSelLocation = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strSelLocation = Trim(Request.QueryString("SelLocation"))
            End If

            onload_GetLangCap()
            BindReport()
        End If

    End Sub

    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim strOpCdGetConfig As String
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim strNonFuelType As String
        Dim strVehExpStatus As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim strFuelType As String
        Dim strLubricantType As String
        Dim strFuelText As String
        Dim strLubricantText As String
        Dim objFTPFolder As String


        strVehExpStatus = objGLSetup.EnumVehExpenseStatus.Active
        strNonFuelType = objINSetup.EnumFuelItemIndicator.No
        strFuelType = objINSetup.EnumFuelItemIndicator.Fuel
        strLubricantType = objINSetup.EnumFuelItemIndicator.Lubricant
        strFuelText = objINSetup.mtdGetFuelItemIndicator(objINSetup.EnumFuelItemIndicator.Fuel)
        strLubricantText = objINSetup.mtdGetFuelItemIndicator(objINSetup.EnumFuelItemIndicator.Lubricant)
    
        Try
            strRptPrefix = "GL_StdRpt_VehRunExpense"
            strOpCd = "GL_STDRPT_VEH_RUN_EXP_GET_SP_VEHEXP" & "|" & "GL_VEHRUNEXPENSE" & _
                        chr(9) & "GL_STDRPT_VEH_RUN_EXP_GET_SP_UOMBYVEH" & "|" & "UOMBYVEH" & _
                        chr(9) & "GL_STDRPT_VEH_RUN_EXP_GET_SP_UOMSUMMARY" & "|" & "UOMSUMMARY" & _
                        chr(9) & "GL_STDRPT_VEH_RUN_EXP_GET_SP_BUDGET" & "|" & "BUDGET_ROW"

            strOpCdGetConfig = "PWSYSTEM_CLSCONFIG_CONFIG_GET"


            strParam = strSelAccMonth & "|" & _
                       strSelAccYear & "|" & _
                       strSelLocation & "|" & _
                       strVehExpStatus & "|" & _
                       strSrchVehTypeCode & "|" & _
                       strSrchVehCode & "|" & _
                       strNonFuelType & "|" & _
                       strFuelType & "|" & _
                       strLubricantType & "|" & _
                       strFuelText & "|" & _
                       strLubricantText

            intErrNo = objGLRpt.mtdGetReport_VehRunExpense(strOpCd, _
                                                           strOpCdGetConfig, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           objRptDs, _
                                                           objMapPath, _
                                                           objPeriod, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_VEHICLE_RUN_EXPENSE_GET_DETAIL&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If instr(objPeriod, "|") > 0 Then
            arrPeriod = Split(objPeriod, "|")
            strPeriod1 = arrPeriod(0)
            strPeriod2 = arrPeriod(1)
            strPeriod3 = arrPeriod(2)
            strPeriod4 = arrPeriod(3)
            strPeriod5 = arrPeriod(4)
            strPeriod6 = arrPeriod(5)
            strPeriod7 = arrPeriod(6)
            strPeriod8 = arrPeriod(7)
            strPeriod9 = arrPeriod(8)
            strPeriod10 = arrPeriod(9)
            strPeriod11 = arrPeriod(10)
            strPeriod12 = arrPeriod(11)
        End if
        

   
        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3


        PassParam()
        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".pdf"">")
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
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
        Dim ParamFieldDef31 As ParameterFieldDefinition
        Dim ParamFieldDef32 As ParameterFieldDefinition
        Dim ParamFieldDef33 As ParameterFieldDefinition
        Dim ParamFieldDef34 As ParameterFieldDefinition
        Dim ParamFieldDef35 As ParameterFieldDefinition
        Dim ParamFieldDef36 As ParameterFieldDefinition
        Dim ParamFieldDef37 As ParameterFieldDefinition

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
        Dim ParameterValues31 As New ParameterValues()
        Dim ParameterValues32 As New ParameterValues()
        Dim ParameterValues33 As New ParameterValues()
        Dim ParameterValues34 As New ParameterValues()
        Dim ParameterValues35 As New ParameterValues()
        Dim ParameterValues36 As New ParameterValues()
        Dim ParameterValues37 As New ParameterValues()

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
        Dim ParamDiscreteValue16 AS New ParameterDiscreteValue()
        Dim ParamDiscreteValue17 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue18 AS New ParameterDiscreteValue()
        Dim ParamDiscreteValue19 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue20 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue21 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue22 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue23 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue24 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue25 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue26 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue27 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue28 AS New ParameterDiscreteValue()
        Dim ParamDiscreteValue29 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue30 AS New ParameterDiscreteValue()
        Dim ParamDiscreteValue31 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue32 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue33 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue34 AS New ParameterDiscreteValue()
        Dim ParamDiscreteValue35 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue36 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue37 As New ParameterDiscreteValue()

        strSelLocation = Replace(strSelLocation, "','", ", ")

        ParamDiscreteValue1.Value = strSelLocation
        ParamDiscreteValue2.Value = strSelAccMonth
        ParamDiscreteValue3.Value = strSelAccYear
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strSelSupress
        ParamDiscreteValue6.Value = strCompanyName
        ParamDiscreteValue7.Value = strLocationName
        ParamDiscreteValue8.Value = strPrintedBy
        ParamDiscreteValue9.Value = UCase(strULocTag)
        ParamDiscreteValue10.Value = UCase(strVehTypeCodeTag)
        ParamDiscreteValue11.Value = strVehCodeTag        
        ParamDiscreteValue12.Value = strSrchVehTypeCode
        ParamDiscreteValue13.Value = strSrchVehCode
        ParamDiscreteValue14.Value = strVehExpTag
        ParamDiscreteValue15.Value = strVehExpDescTag
        ParamDiscreteValue16.Value = strLocTag
        ParamDiscreteValue17.Value = intSelDecimal
        ParamDiscreteValue18.Value = strSelSupress
        ParamDiscreteValue19.Value = strRptId
        ParamDiscreteValue20.Value = strRptName
        ParamDiscreteValue21.Value = strVehTag
        ParamDiscreteValue22.Value = strPeriod1
        ParamDiscreteValue23.Value = strPeriod2        
        ParamDiscreteValue24.Value = strPeriod3
        ParamDiscreteValue25.Value = strPeriod4
        ParamDiscreteValue26.Value = strPeriod5
        ParamDiscreteValue27.Value = strPeriod6
        ParamDiscreteValue28.Value = strPeriod7
        ParamDiscreteValue29.Value = strPeriod8
        ParamDiscreteValue30.Value = strPeriod9
        ParamDiscreteValue31.Value = strPeriod10
        ParamDiscreteValue32.Value = strPeriod11
        ParamDiscreteValue33.Value = strPeriod12
        ParamDiscreteValue34.Value = intSelDecimal
        ParamDiscreteValue35.Value = strSelSupress
        ParamDiscreteValue36.Value = intSelDecimal
        ParamDiscreteValue37.Value = strSelSupress

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef2 = ParamFieldDefs.Item("SelAccMonth")
        ParamFieldDef3 = ParamFieldDefs.Item("SelAccYear")
        ParamFieldDef4 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("SelSuppress")
        ParamFieldDef6 = ParamFieldDefs.Item("SessionCompName")
        ParamFieldDef7 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef8 = ParamFieldDefs.Item("SessionPrintedBy")
        ParamFieldDef9 = ParamFieldDefs.Item("ULocTag")
        ParamFieldDef10 = ParamFieldDefs.Item("VehTypeCodeTag")
        ParamFieldDef11 = ParamFieldDefs.Item("VehCodeTag")
        ParamFieldDef12 = ParamFieldDefs.Item("SrchVehTypeCode")
        ParamFieldDef13 = ParamFieldDefs.Item("SrchVehCode")
        ParamFieldDef14 = ParamFieldDefs.Item("VehExpTag")
        ParamFieldDef15 = ParamFieldDefs.Item("VehExpDescTag")
        ParamFieldDef16 = ParamFieldDefs.Item("LocTag")
        ParamFieldDef17 = ParamFieldDefs.Item("subSelDecimal")
        ParamFieldDef18 = ParamFieldDefs.Item("subSelSuppress")
        ParamFieldDef19 = ParamFieldDefs.Item("RptId")
        ParamFieldDef20 = ParamFieldDefs.Item("RptName")
        ParamFieldDef21 = ParamFieldDefs.Item("VehTag")
        ParamFieldDef22 = ParamFieldDefs.Item("Period1")
        ParamFieldDef23 = ParamFieldDefs.Item("Period2")
        ParamFieldDef24 = ParamFieldDefs.Item("Period3")
        ParamFieldDef25 = ParamFieldDefs.Item("Period4")
        ParamFieldDef26 = ParamFieldDefs.Item("Period5")
        ParamFieldDef27 = ParamFieldDefs.Item("Period6")
        ParamFieldDef28 = ParamFieldDefs.Item("Period7")
        ParamFieldDef29 = ParamFieldDefs.Item("Period8")
        ParamFieldDef30 = ParamFieldDefs.Item("Period9")
        ParamFieldDef31 = ParamFieldDefs.Item("Period10")
        ParamFieldDef32 = ParamFieldDefs.Item("Period11")
        ParamFieldDef33 = ParamFieldDefs.Item("Period12")
        ParamFieldDef34 = ParamFieldDefs.Item("usgDecimal")
        ParamFieldDef35 = ParamFieldDefs.Item("usgSupress")
        ParamFieldDef36 = ParamFieldDefs.Item("bgDecimal")
        ParamFieldDef37 = ParamFieldDefs.Item("bgSupress")

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
        ParameterValues31 = ParamFieldDef31.CurrentValues
        ParameterValues32 = ParamFieldDef32.CurrentValues
        ParameterValues33 = ParamFieldDef33.CurrentValues
        ParameterValues34 = ParamFieldDef34.CurrentValues
        ParameterValues35 = ParamFieldDef35.CurrentValues
        ParameterValues36 = ParamFieldDef36.CurrentValues
        ParameterValues37 = ParamFieldDef37.CurrentValues

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
        ParameterValues31.Add(ParamDiscreteValue31)
        ParameterValues32.Add(ParamDiscreteValue32)
        ParameterValues33.Add(ParamDiscreteValue33)
        ParameterValues34.Add(ParamDiscreteValue34)
        ParameterValues35.Add(ParamDiscreteValue35)
        ParameterValues36.Add(ParamDiscreteValue36)
        ParameterValues37.Add(ParamDiscreteValue37)

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
        ParamFieldDef31.ApplyCurrentValues(ParameterValues31)
        ParamFieldDef32.ApplyCurrentValues(ParameterValues32)
        ParamFieldDef33.ApplyCurrentValues(ParameterValues33)
        ParamFieldDef34.ApplyCurrentValues(ParameterValues34)
        ParamFieldDef35.ApplyCurrentValues(ParameterValues35)
        ParamFieldDef36.ApplyCurrentValues(ParameterValues36)
        ParamFieldDef37.ApplyCurrentValues(ParameterValues37)

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        strVehTypeCodeTag = GetCaption(objLangCap.EnumLangCap.VehType) & lblCode.text
        strVehCodeTag = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.text
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strULocTag = GetCaption(objLangCap.EnumLangCap.Location)
        strLocTag = GetCaption(objLangCap.EnumLangCap.Location)
        strVehExpTag = GetCaption(objLangCap.EnumLangCap.VehExpense)
        strVehExpDescTag = GetCaption(objLangCap.EnumLangCap.VehExpenseDesc)
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_VEHICLE_RUN_EXPENSE_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function


End Class
