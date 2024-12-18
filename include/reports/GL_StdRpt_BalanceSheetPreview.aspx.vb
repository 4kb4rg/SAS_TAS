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

Public Class GL_StdRpt_BalanceSheetPreview : Inherits Page
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim rdCrystalViewer As ReportDocument
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
    Dim intConfig As Integer

    Dim strRptId As String
    Dim strRptName As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strSelSupress As String
    Dim strSelLocation As String
    Dim strPrintStmtBy As String
    Dim strStmtAccYear As String
    Dim LocationTag As String = ""
    Dim strStmtType As String
    Dim strRowType As String
    Dim strEntryRow As String
    Dim strEndAccMonth As String
    Dim strEndAccYear As String
    Dim intErrNo As Integer
    Dim pr_intErrNo As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfig = Session("SS_CONFIGSETTING")

        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Right(Request.QueryString("Location"), 1) = "," Then
                strSelLocation = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strSelLocation = Trim(Request.QueryString("SelLocation"))
            End If 
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Trim(Request.QueryString("DDLAccMth"))
            strSelAccYear = Trim(Request.QueryString("DDLAccYr"))
            intSelDecimal= CInt(Request.QueryString("Decimal"))
            strSelSupress = Trim(Request.QueryString("Supp"))
            strPrintStmtBy = Trim(Request.QueryString("PrintStmtBy"))
            strStmtAccYear = Trim(Request.QueryString("StmtAccYear"))
            
            onload_GetLangCap()
            GetLastYearEndPeriod(strStmtAccYear, strEndAccMonth, strEndAccYear)
            BindReport()
        End If
    End Sub

    Sub GetLastYearEndPeriod(ByVal pv_strStmtAccYear As String, ByRef pr_strEndMonth as String, ByRef pr_strEndYear As String)
        Dim strOpCd_Config As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim dsConfig As Dataset
        Dim intStartMonth As Integer

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strOpCd_Config, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  DsConfig, "")
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_BALSHEET_GETENDACCMONTH&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        intStartMonth = CInt(DsConfig.Tables(0).Rows(0).Item("StartAccMonth").Trim())
        
        If intStartMonth = 1 Then
            pr_strEndMonth = "12"
            pr_strEndYear = Cstr(CInt(pv_strStmtAccYear) - 1)
        Else
            pr_strEndMonth = CStr(intStartMonth - 1)
            pr_strEndYear = pv_strStmtAccYear
        End If

    End Sub

        



    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim pv_strOpCdGetBalSheet As String
        Dim pv_strOpCdGetStmtFig As String = "GL_SETUP_STMTFIGURE_GET"
        Dim pv_strOpCdGetTmpl As String = "GL_SETUP_STMTTEMPL_GET"
        Dim pv_strOpCdGetCfg As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim pv_strOpCdAddTmpl As String = "GL_SETUP_STMTTEMPL_ADD"
        Dim pv_strOpCd_SP As String = "GL_SETUP_POPULATE_STMT_GETSP"
        Dim pv_strOpCdGetEntry As String = "GL_SETUP_POPULATE_STMT_GETFIGURE"
        Dim pv_strOpCdCalc As String = "GL_SETUP_STMTFIGURE_CALCULATE"
        Dim pv_strOpCdStmtFigureUpd As String = "GL_SETUP_STMTFIGURE_UPDATE"
        Dim objFTPFolder As String

        strStmtType = CInt(objGLSetup.EnumStmtType.BalanceSheet)
        strRowType = CInt(objGLSetup.EnumRowType.Entry) & "','" & CInt(objGLSetup.EnumRowType.Formula)
        strEntryRow = CInt(objGLSetup.EnumRowType.Entry)

        If Lcase(strPrintStmtBy) = "comp" Then
            strRptPrefix = "GL_StdRpt_CompBalSheet"
            pv_strOpCdGetBalSheet = "GL_STDRPT_COMPBALSHEET_GETSP"
        Else
            strRptPrefix = "GL_StdRpt_LocBalSheet"
            pv_strOpCdGetBalSheet = "GL_STDRPT_LOCBALSHEET_GETSP"
        End If

        strParam = strSelLocation & "|" & _
                    strSelAccMonth & "|" & _
                    strSelAccYear & "|" & _
                    strEndAccMonth & "|" & _
                    strEndAccYear & "|" & _
                    strStmtType & "|" & _
                    strStmtAccYear & "|" & _
                    strRowType & "|" & _
                    strEntryRow

        Try
            intErrNo = objGLRpt.mtdGetReport_BalSheet(pv_strOpCdGetStmtFig, _
                                                      pv_strOpCdGetBalSheet, _
                                                      pv_strOpCdGetTmpl, _
                                                      pv_strOpCdGetCfg, _
                                                      pv_strOpCdAddTmpl, _
                                                      pv_strOpCd_SP, _
                                                      pv_strOpCdGetEntry, _
                                                      pv_strOpCdCalc, _
                                                      pv_strOpCdStmtFigureUpd, _
                                                      strCompany, _
                                                      strLocation, _
                                                      strUserId, _
                                                      strAccMonth, _
                                                      strAccYear, _
                                                      strParam, _
                                                      objRptDs, _
                                                      objMapPath, _
                                                      pr_intErrNo, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_BALSHEET_GET&errmesg=" & Exp.ToString() & "&redirect=")
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
        ParamDiscreteValue9.Value = LocationTag
        ParamDiscreteValue10.Value = strRptId
        ParamDiscreteValue11.Value = strRptName
        ParamDiscreteValue12.Value = strPrintStmtBy
        ParamDiscreteValue13.Value = strEndAccMonth
        ParamDiscreteValue14.Value = strEndAccYear

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        
        ParamFieldDef1 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef2 = ParamFieldDefs.Item("SelAccMonth")
        ParamFieldDef3 = ParamFieldDefs.Item("SelAccYear")
        ParamFieldDef4 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("SelSuppress")
        ParamFieldDef6 = ParamFieldDefs.Item("SessionCompName")
        ParamFieldDef7 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef8 = ParamFieldDefs.Item("SessionPrintedBy")
        ParamFieldDef9 = ParamFieldDefs.Item("LocationTag")
        ParamFieldDef10 = ParamFieldDefs.Item("RptId")
        ParamFieldDef11 = ParamFieldDefs.Item("RptName")
        ParamFieldDef12 = ParamFieldDefs.Item("PrintStmtBy")
        ParamFieldDef13 = ParamFieldDefs.Item("EndAccMonth")
        ParamFieldDef14 = ParamFieldDefs.Item("EndAccYear")

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
        LocationTag = GetCaption(objLangCap.EnumLangCap.Location)
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_BALSHEET_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
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
