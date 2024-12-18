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

Public Class GL_StdRpt_UpahPanenPreview : Inherits Page

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
    Dim strSelToDateMonth As String
    Dim strSelBlk As String
    Dim strSelBlkGrp As String
    Dim strSelSubBlk As String
    Dim strSelLocation As String
    Dim strBlkTag As String
    Dim strRemark As String

    Dim strSearchExp As String = ""
    Dim strLocTag As String

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
            strSelAccYear = Trim(Request.QueryString("DDLAccYr"))
            intSelDecimal = CInt(Request.QueryString("Decimal"))
            strSelAccMonth = Trim(Request.QueryString("SelAccMth"))
            strSelToDateMonth = Trim(Request.QueryString("DDLAccMth"))
            strSelBlk = Trim(Request.QueryString("SelBlk"))
            strSelLocation = Trim(Request.QueryString("SelLocation"))
            strRemark = Trim(Request.QueryString("Remark"))

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
        
        strSelLocation = Replace(strSelLocation, "','", ", ")

        strSelAccPeriod = Replace(strSelAccMonth, "','", "/" & strSelAccYear & ", ")
        If Len(strSelAccPeriod) <> 1 Then
            strSelAccPeriod = Left(strSelAccPeriod, Len(strSelAccPeriod) - 2)
        Else
            strSelAccPeriod = strSelAccPeriod & "/" & strSelAccYear
        End If

        ParamDiscreteValue1.Value = strRptName
        ParamDiscreteValue2.Value = strRptId
        ParamDiscreteValue3.Value = strLocTag
        ParamDiscreteValue4.Value = strSelLocation
        ParamDiscreteValue5.Value = strCompanyName
        ParamDiscreteValue6.Value = strUserId
        ParamDiscreteValue7.Value = strSelAccPeriod
        ParamDiscreteValue8.Value = intSelDecimal
        ParamDiscreteValue9.Value = UCase(strBlkTag)
        ParamDiscreteValue10.Value = strRemark

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ReportName")
        ParamFieldDef2 = ParamFieldDefs.Item("RptID")
        ParamFieldDef3 = ParamFieldDefs.Item("lblLocation")
        ParamFieldDef4 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef5 = ParamFieldDefs.Item("CompName")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef7 = ParamFieldDefs.Item("Period")
        ParamFieldDef8 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef9 = ParamFieldDefs.Item("lblBlk")
        ParamFieldDef10 = ParamFieldDefs.Item("Remark")

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
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        strLocTag = GetCaption(objLangCap.EnumLangCap.Location)
        strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_UpahPanen_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
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
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim objFTPFolder As String

        Try
            strRptPrefix = "GL_StdRpt_UpahPanen"

            strOpCd = "GL_STDRPT_MR_UpahPanen_GET_SP" & "|" & objGLRpt.mtdGetGLReportTable(objGLRpt.EnumGLReportTable.UpahPanen) & Chr(9) & _
                  "GET_REPORT_INFO_BY_REPORTID|SH_REPORT"

            strParam = strSelLocation & "|" & _
                       strSelBlk & "|" & _
                       strSelAccYear & "|" & _
                       strSelAccMonth & "|" & _
                       strRptID


            intErrNo = objGLRpt.mtdGetReport_UpahPanen(strOpCd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objRptDs, _
                                                    objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_UpahPanen_GET_SP&errmesg=" & Exp.ToString() & "&redirect=")
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
