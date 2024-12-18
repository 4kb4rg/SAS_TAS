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

Public Class GL_StdRpt_ActGrpSummaryPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSummary As Label

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
    Dim strCostLevel As String

    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strSelSupress As String
    Dim strSelLocation As String
    
    Dim strSelActGrpCode As String
    Dim strSelActGrpName As String

    Dim strActGrpCodeTag As String = ""
    Dim strSelLocTag As String = ""
    Dim strActTag As String = ""
    Dim strLocTag As String = ""
    Dim strTitleTag As String = ""
    
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

            strSelAccMonth = Request.QueryString("DDLAccMth")
            strSelAccYear = Request.QueryString("DDLAccYr")
            intSelDecimal= CInt(Request.QueryString("Decimal"))
            strSelSupress = Request.QueryString("Supp")

            strSelActGrpCode = Trim(Request.QueryString("SelActGrpCode"))
            strSelActGrpName = Trim(Request.QueryString("SelActGrpName"))
            strCostLevel = Trim(Request.QueryString("CostLevel"))

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
        Dim strOpCdAmt As String
        Dim strOpCdArea As String
        Dim objRptDs As New Dataset()
        Dim objMTDDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim strReportID As String
        Dim intCnt As Integer
        Dim objFTPFolder As String

        strReportID = "RPTGL1000006"

        Try
            strRptPrefix = "GL_StdRpt_ActGrpSummary"

            strOpCd = "GL_STDRPT_ACTIVITY_GROUP_SUMMARY_GET" & "|" & objGLRpt.mtdGetGLReportTable(objGLRpt.EnumGLReportTable.ActGrpSummary) & _
                       Chr(9) & "GET_REPORT_INFO_BY_REPORTID" & "|" & "SH_REPORT" 

            strOpCdAmt = "GL_STDRPT_ACTIVITY_GROUP_SUMMARY_GET_AMOUNT" & "|" & "COUNT_AMOUNT" & Chr(9) & "BD_STDRPT_ACTIVITY_SUM_BY_PERIOD" & "|" & "BUDGET_AMOUNT"

            strOpCdArea = "GL_STDRPT_ACTIVITY_GROUP_SUMMARY_GET_TOTALAREA_BY_SUBBLK" & "|" & "AREA_BY_SUBBLK" & _
                          Chr(9) & "GL_STDRPT_ACTIVITY_GROUP_SUMMARY_GET_TOTALAREA_BY_BLK" & "|" & "AREA_BY_BLK"
                         
            strParam = strSelAccMonth & "|" & _
                       strSelAccYear & "|" & _
                       strSelLocation & "|" & _
                       strSelActGrpCode & "|" & _
                       objGLSetup.EnumActStatus.Active & "|" & _
                       strReportID

            intErrNo = objGLRpt.mtdGetReport_ActGrpSummary(strOpCd, _
                                                           strOpCdAmt, _
                                                           strOpCdArea, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           objRptDs, _
                                                           objMapPath, objFTPFolder)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_ACTGRPSUMMARY_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt=0 To objRptDs.Tables("GL_ACTGRPSUMMARY").Rows.Count - 1
            objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("LocCode") = Trim(objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("LocCode"))
            objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("ActCode") = Trim(objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("ActCode"))
            objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("ActDesc") = Trim(objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("ActDesc"))
            objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("MonthAmt") = FormatNumber(objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("MonthAmt"), intSelDecimal)
            objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("TodateAmt") = FormatNumber(objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("TodateAmt"), intSelDecimal)
            objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("BlkTotalArea") = FormatNumber(objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("BlkTotalArea"), intSelDecimal)
            objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("SubBlkTotalArea") = FormatNumber(objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("SubBlkTotalArea"), intSelDecimal)
            objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("LocTotalArea") = FormatNumber(objRptDs.Tables("GL_ACTGRPSUMMARY").Rows(intCnt).Item("LocTotalArea"), intSelDecimal)
        Next

   
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

        strSelLocation = Replace(strSelLocation, "','", ", ")

        ParamDiscreteValue1.Value = strSelLocation
        ParamDiscreteValue2.Value = strSelAccMonth
        ParamDiscreteValue3.Value = strSelAccYear
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strSelSupress
        ParamDiscreteValue6.Value = strCompanyName
        ParamDiscreteValue7.Value = strLocationName
        ParamDiscreteValue8.Value = strPrintedBy
        ParamDiscreteValue9.Value = UCase(strSelLocTag)
        ParamDiscreteValue10.Value = UCase(strActGrpCodeTag)
        If strSelActGrpCode = "%" Then
            strSelActGrpCode = "All"
        End If
        ParamDiscreteValue11.Value = strSelActGrpCode
        ParamDiscreteValue12.Value = strActTag
        ParamDiscreteValue13.Value = strLocTag
        ParamDiscreteValue14.Value = strTitleTag

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef2 = ParamFieldDefs.Item("SelAccMonth")
        ParamFieldDef3 = ParamFieldDefs.Item("SelAccYear")
        ParamFieldDef4 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("SelSuppress")
        ParamFieldDef6 = ParamFieldDefs.Item("SessionCompName")
        ParamFieldDef7 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef8 = ParamFieldDefs.Item("SessionPrintedBy")
        ParamFieldDef9 = ParamFieldDefs.Item("SelLocTag")
        ParamFieldDef10 = ParamFieldDefs.Item("SelActGrpTag")
        ParamFieldDef11 = ParamFieldDefs.Item("SelActGrpCode")
        ParamFieldDef12 = ParamFieldDefs.Item("ActTag")
        ParamFieldDef13 = ParamFieldDefs.Item("LocTag")
        ParamFieldDef14 = ParamFieldDefs.Item("TitleTag")

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

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        strActGrpCodeTag = GetCaption(objLangCap.EnumLangCap.ActGrp) & lblCode.text
        strSelLocTag = GetCaption(objLangCap.EnumLangCap.Location)
        strActTag = GetCaption(objLangCap.EnumLangCap.Activity) 
        strLocTag = GetCaption(objLangCap.EnumLangCap.Location)
        strTitleTag = GetCaption(objLangCap.EnumLangCap.ActGrp) & lblSummary.text & strSelActGrpName

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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_ACTGRPSUMMARY_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
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
