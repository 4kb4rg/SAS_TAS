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

Public Class GL_StdRpt_SummVehTypeExpensePreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblCode As Label
    Protected WithEvents lblRefDesc As Label

    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

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
    Dim strSelAccPeriod As String
    Dim intSelDecimal As Integer
    Dim strSelSupress As String
    Dim strSelLocation As String

    Dim strSrchVehCode As String
    Dim strSrchVehTypeCode As String
    Dim strSrchVehExpCode As String
    Dim strSrchAccCode As String
    Dim strSelBlkType As String
    Dim strSrchBlkCode As String
    Dim strSrchSubBlkCode As String
    Dim strSrchBlkGrpCode As String
    Dim dblEstExpenditure As Double
    Dim strCostLevel As String
    Dim strVehType As String
    Dim strVehTypeTag As String = ""

    Dim strSearchExp As String = ""
    Dim strVehCodeTag As String = ""
    Dim strVehTypeCodeTag As String = ""
    Dim strVehExpCodeTag As String = ""
    Dim strAccCodeTag As String = ""
    Dim strBlkCodeTag As String = ""
    Dim strBlkGrpCodeTag As String = ""
    Dim strVehTag As String = ""
    Dim strVehExpTag As String = ""
    Dim strAccTag As String = ""
    Dim strBlkTag As String = ""
    Dim strULocTag As String = ""
    Dim strLocTag As String = ""

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
            strSelAccYear = Request.QueryString("DDLAccYr")
            intSelDecimal = CInt(Request.QueryString("Decimal"))
            strSelSupress = Request.QueryString("Supp")

            strSrchVehCode = Trim(Request.QueryString("SrchVehCode"))
            strSrchVehTypeCode = Trim(Request.QueryString("SrchVehTypeCode"))
            strSrchVehExpCode = Trim(Request.QueryString("SrchVehExpCode"))
            strSrchAccCode = Trim(Request.QueryString("SrchAccCode"))
            strSelBlkType = Trim(Request.QueryString("SelBlkType"))
            strSrchBlkCode = Trim(Request.QueryString("SrchBlkCode"))
            strSrchSubBlkCode = Trim(Request.QueryString("SrchSubBlkCode"))
            strSrchBlkGrpCode = Trim(Request.QueryString("SrchBlkGrpCode"))
            strCostLevel = Trim(Request.QueryString("CostLevel"))

            If Right(Request.QueryString("Location"), 1) = "," Then
                strSelLocation = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strSelLocation = Trim(Request.QueryString("SelLocation"))
            End If

            If Right(Request.QueryString("DDLAccMth"), 1) = "," Then
                strSelAccMonth = Left(Request.QueryString("DDLAccMth"), Len(Request.QueryString("DDLAccMth")) - 1)
            Else
                strSelAccMonth = Trim(Request.QueryString("DDLAccMth"))
            End If

            onload_GetLangCap()
            BindSummReport()
        End If

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

        strSelLocation = Replace(strSelLocation, "','", ", ")

        strSelAccPeriod = Replace(strSelAccMonth, "','", "/" & strSelAccYear & ", ")
        If Len(strSelAccPeriod) <> 1 Then
            strSelAccPeriod = Left(strSelAccPeriod, Len(strSelAccPeriod) - 2)
        Else
            strSelAccPeriod = strSelAccPeriod & "/" & strSelAccYear
        End If

        ParamDiscreteValue1.Value = strSelLocation
        ParamDiscreteValue2.Value = strSelAccMonth
        ParamDiscreteValue3.Value = strSelAccYear
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strSelSupress
        ParamDiscreteValue6.Value = strCompanyName
        ParamDiscreteValue7.Value = strLocationName
        ParamDiscreteValue8.Value = strPrintedBy
        ParamDiscreteValue9.Value = UCase(strULocTag)
        ParamDiscreteValue10.Value = UCase(strVehCodeTag)
        ParamDiscreteValue11.Value = UCase(strVehExpCodeTag)
        ParamDiscreteValue12.Value = UCase(strAccCodeTag)
        ParamDiscreteValue13.Value = UCase(strBlkCodeTag)
        ParamDiscreteValue14.Value = strSrchVehCode
        ParamDiscreteValue15.Value = strSrchVehExpCode
        ParamDiscreteValue16.Value = strSrchAccCode

        If LCase(strSelBlkType) = "blkCode" Then
            ParamDiscreteValue17.Value = strSrchBlkCode
        ElseIf LCase(strSelBlkType) = "subblkcode" Then
            ParamDiscreteValue17.Value = strSrchSubBlkCode
        Else
            ParamDiscreteValue17.Value = strSrchBlkGrpCode
        End If

        ParamDiscreteValue18.Value = strVehTag
        ParamDiscreteValue19.Value = strVehExpTag
        ParamDiscreteValue20.Value = strAccTag
        ParamDiscreteValue21.Value = strBlkTag
        ParamDiscreteValue22.Value = strLocTag
        ParamDiscreteValue23.Value = UCase(strVehTypeCodeTag)
        ParamDiscreteValue24.Value = strSrchVehTypeCode
        ParamDiscreteValue25.Value = strRptId
        ParamDiscreteValue26.Value = strRptName
        ParamDiscreteValue27.Value = strSelAccPeriod

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
        ParamFieldDef10 = ParamFieldDefs.Item("VehCodeTag")
        ParamFieldDef11 = ParamFieldDefs.Item("VehExpenseCodeTag")
        ParamFieldDef12 = ParamFieldDefs.Item("AccCodeTag")
        ParamFieldDef13 = ParamFieldDefs.Item("BlkCodeTag")
        ParamFieldDef14 = ParamFieldDefs.Item("SrchVehCode")
        ParamFieldDef15 = ParamFieldDefs.Item("SrchVehExpCode")
        ParamFieldDef16 = ParamFieldDefs.Item("SrchAccCode")
        ParamFieldDef17 = ParamFieldDefs.Item("SrchBlkCode")
        ParamFieldDef18 = ParamFieldDefs.Item("VehTag")
        ParamFieldDef19 = ParamFieldDefs.Item("VehExpTag")
        ParamFieldDef20 = ParamFieldDefs.Item("AccTag")
        ParamFieldDef21 = ParamFieldDefs.Item("BlkTag")
        ParamFieldDef22 = ParamFieldDefs.Item("LocTag")
        ParamFieldDef23 = ParamFieldDefs.Item("VehTypeCodeTag")
        ParamFieldDef24 = ParamFieldDefs.Item("SrchVehTypeCode")
        ParamFieldDef25 = ParamFieldDefs.Item("RptId")
        ParamFieldDef26 = ParamFieldDefs.Item("RptName")
        ParamFieldDef27 = ParamFieldDefs.Item("AccPeriod")

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
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        strVehCodeTag = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        strVehTypeCodeTag = GetCaption(objLangCap.EnumLangCap.VehType) & lblCode.Text
        strVehExpCodeTag = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
        strAccCodeTag = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text

        If LCase(strSelBlkType) = "blkcode" Then
            strBlkCodeTag = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        ElseIf LCase(strSelBlkType) = "subblkcode" Then
            strBlkCodeTag = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        Else
            strBlkCodeTag = GetCaption(objLangCap.EnumLangCap.BlockGrp) & lblCode.Text
        End If

        strULocTag = GetCaption(objLangCap.EnumLangCap.Location)
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strVehExpTag = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
        strAccTag = GetCaption(objLangCap.EnumLangCap.Account)

        If LCase(strCostLevel) = "subblock" Then
            strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
        Else
            strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
        End If
        strLocTag = GetCaption(objLangCap.EnumLangCap.Location)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_VEHICLE_EXPENSE_DETAIL_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
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


    Sub BindSummReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim strOpCdExpenses As String
        Dim strOpCdVehExp As String
        Dim objRptDs As New DataSet()
        Dim pr_totalDebit As New Object()
        Dim pr_totalCredit As New Object()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim objFTPFolder As String

        Try
            strRptPrefix = "GL_StdRpt_SummVehTypeExpense"

            strOpCd = "GL_STDRPT_SUMM_VEHTYPE_EXPENSE" & "|" & objGLRpt.mtdGetGLReportTable(objGLRpt.EnumGLReportTable.SummVehTypeExpense)

            strParam = strSelAccMonth & "|" & _
                       strSelAccYear & "|" & _
                       strSelLocation & "|" & _
                       strSrchVehCode & "|" & _
                       strSrchVehTypeCode & "|" & _
                       strSrchVehExpCode & "|" & _
                       strSrchAccCode & "|" & _
                       strSelBlkType & "|" & _
                       strSrchBlkCode & "|" & _
                       strSrchBlkGrpCode & "|" & _
                       strSrchSubBlkCode & "|" & _
                       strCostLevel & "|" & _
                       strRptId & "|"

            intErrNo = objGLRpt.mtdGetReport_SummVehTypeExpense(strOpCd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objRptDs, _
                                                    objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_VEHICLE_EXPENSE_DETAIL_SUMM_GET_DETAIL&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
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

End Class
