Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class GL_StdRpt_FSPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid


    Dim objGL As New agri.GL.clsReport()
    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdminCty As New agri.Admin.clsCountry()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objAdmin As New agri.Admin.clsShare()

    
    Dim strLocType as String
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objLangCapDs As New Object()
    Dim strCompany  As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim LocationTag As String = ""
    Dim strLangCode As String
    Dim intConfig As Integer
    Dim objMapPath As String
    
    Dim strRptID As String
    Dim strRptTitle As String
    Dim strFileName As String
    Dim intSelDecimal As String
    
    Dim strRptPkID As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strSelLocation As String
    Dim strDispLocation As String
    Dim strExportToExcel As String

    Dim intRptType As Integer
    Dim strOption As String
    Dim strAudited As String
    Dim strDetail As String
    Dim DateOfPeriod As Date
    Dim DateOfLastPeriod As Date

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        intConfig = Session("SS_CONFIGSETTING")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")

        strLocType = Session("SS_LOCTYPE")

        strRptID = Trim(Request.QueryString("RptID"))
        strRptTitle = Trim(Request.QueryString("RptTitle"))
        intSelDecimal = Cint(Request.QueryString("Decimal"))
        strSelAccMonth = Trim(Request.QueryString("AccMonth"))
        strSelAccYear = Trim(Request.QueryString("AccYear"))
        strRptPkID  = Trim(Request.QueryString("RptPkID"))
        intRptType = Trim(Request.QueryString("RepType"))
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

        strSelLocation = Request.QueryString("SelLocation")
        strOption = Request.QueryString("RepOption")
        strAudited = Request.QueryString("Audited")
        strDetail = Request.QueryString("Detail")

        Dim dDay As Integer = GetTotDayInMonth(strSelAccMonth, strSelAccYear)
        Dim lDay As Integer = GetTotDayInMonth("12", strSelAccYear)
        DateOfPeriod = strSelAccMonth & "/" & dDay & "/" & strSelAccYear
        DateOfLastPeriod = "12/" & dDay & "/" & Val(strSelAccYear) - 1

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            onload_GetLangCap()
            BindReport()
        End If
    End Sub

  
    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim objRptDsLoc As New DataSet()
        Dim strParam As String
        Dim crExportOptions As ExportOptions

        Dim strGetLoc As String
        Dim strLoc As String
        Dim arrLoc As Array
        Dim intCnt As Integer

        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim pv_strOpCdCalculate As String = "GL_CLSSETUP_FS_CALCULATE"
        Dim pv_strOpCdGetFormula As String = "GL_SETUP_FSTEMPLDTL_GET"
        Dim pv_strOpCdGetFigure As String = "GL_clsSetup_FSFIG_REPORT_GET"
        Dim pv_strOpCdGetFormulaFig As String = "GL_clsSetup_FSFIG_AMOUNT_GET"
        Dim pv_strOpCdCalc As String = "GL_clsSetup_FSFORMULA_CALCULATE"

        'for check report cogs
        Dim pv_strOpCheckReportType As String = "GL_CLSSETUP_FSTEMPLATE_LIST_GET"
        Dim pv_strParamName As String = "SEARCHSTR"
        Dim pv_strParamValue As String
        Dim blnCOGS As Boolean
        Dim objFTPFolder As String
       

        'Select Case intRptType
        '    Case 0 ' 1 kolom
        '        strFileName = "GL_StdRpt_FS"
        '        pv_strOpCdCalculate = "GL_CLSSETUP_FS_CALCULATE"
        '    Case 1 ' 2 kolom
        '        strFileName = "GL_StdRpt_FS2"
        '        pv_strOpCdCalculate = "GL_CLSSETUP_FS_CALCULATE2"
        '    Case 2 ' 3 kolom
        '        strFileName = "GL_StdRpt_FS3"
        '        pv_strOpCdCalculate = "GL_CLSSETUP_FS_CALCULATE2"
        'End Select

        If strOption = "1" Then
            pv_strOpCdCalculate = "GL_CLSSETUP_FS_CALCULATE_YEARLY"
            If strDetail = "0" Then
                pv_strOpCdGetFigure = "GL_CLSSETUP_FSFIG_REPORT_GET"
                strFileName = "GL_StdRpt_FSYearly"
            Else
                pv_strOpCdGetFigure = "GL_CLSSETUP_FSFIG_REPORT_GET_DET"
                strFileName = "GL_StdRpt_FSYearly_NRC"
            End If
        Else
            pv_strOpCdCalculate = "GL_CLSSETUP_FS_CALCULATE_MONTHLY"
            strFileName = "GL_StdRpt_FSMonthly"
        End If
        

        strParam = strRptPkID & "|" & strSelAccMonth & "|" & strSelAccYear & "|" & strSelLocation & "|" & intRptType


        Try

            intErrNo = objGL.mtdGetReport_FS(pv_strOpCdCalculate, _
                                                       pv_strOpCdGetFormula, _
                                                       pv_strOpCdGetFigure, _
                                                       pv_strOpCdGetFormulaFig, _
                                                       pv_strOpCdCalc, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strParam, _
                                                       objRptDs, objMapPath, objFTPFolder)


        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_StdRpt_FS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        If intRptType = 2 Then
            rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
            rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4
        Else
            rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
            rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4
        End If

        If strDetail = "1" Then
            rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
            rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4
        End If


        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        If strExportToExcel = "0" Then
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".pdf"
        Else
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".xls"
        End If

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            If strExportToExcel = "0" Then
                .ExportFormatType = ExportFormatType.PortableDocFormat
            Else
                .ExportFormatType = ExportFormatType.Excel
            End If
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")

        If strExportToExcel = "0" Then
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".xls"">")
        End If

        objRptDs.Dispose()
        If Not objRptDs Is Nothing Then
            objRptDs = Nothing
        End If

        If Not objRptDsLoc Is Nothing Then
            objRptDsLoc.Dispose()
        End If
        If Not objRptDsLoc Is Nothing Then
            objRptDsLoc = Nothing
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
        
        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strSelLocation
        ParamDiscreteValue3.Value = strUserName
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = LocationTag
        ParamDiscreteValue6.Value = strRptID
        ParamDiscreteValue7.Value = strRptTitle
        ParamDiscreteValue8.Value = strSelAccMonth
        ParamDiscreteValue9.Value = strSelAccYear
        ParamDiscreteValue10.Value = strRptPkID
        ParamDiscreteValue11.Value = strAudited
        ParamDiscreteValue12.Value = DateOfPeriod
        ParamDiscreteValue13.Value = DateOfLastPeriod

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("prmCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("prmLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("prmUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("prmDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("prmLocationTag")
        ParamFieldDef6 = ParamFieldDefs.Item("RptId")
        ParamFieldDef7 = ParamFieldDefs.Item("RptName")
        ParamFieldDef8 = ParamFieldDefs.Item("prmAccMonth")
        ParamFieldDef9 = ParamFieldDefs.Item("prmAccYear")
        ParamFieldDef10 = ParamFieldDefs.Item("prmRptId")
        ParamFieldDef11 = ParamFieldDefs.Item("Audited")
        ParamFieldDef12 = ParamFieldDefs.Item("DateOfPeriod")
        ParamFieldDef13 = ParamFieldDefs.Item("DateOfLastPeriod")
       
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

     Private Function strGetUserLoc() As String
        Dim intCnt As Integer
        Dim strParam As String
        Dim strUserLoc As String = ""
        Dim objUserLoc As New DataSet()
        Dim strOppCd_UserLoc_GET As String = "GL_STDRPT_USERLOCATION_GET"

        Try
            strParam = "AND USERLOC.UserID = '" & strUserId & "'"
            intErrNo = objAdmin.mtdGetUserLocation(strOppCd_UserLoc_GET, strParam, objUserLoc, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_StdRpt_FS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        For intCnt = 0 To objUserLoc.Tables(0).Rows.Count - 1
           
            strUserLoc = strUserLoc & ", " & objUserLoc.Tables(0).Rows(intCnt).Item("LocCode") 
            
        Next

        If Len(strUserLoc) > 0 then
            strUserLoc = Mid(strUserLoc, 3)
        End if
        
        Return strUserLoc

    End Function
    

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
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                 If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                 else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                 end if
                Exit For
            End If
        Next
    End Function

    Public Function GetTotDayInMonth(ByVal dMonth As Integer, ByVal dYear As Integer) As Integer
        Dim dDate As Date
        Dim lDate As Date
        Dim dDay As Integer
        Dim dSunday As Integer = 0
        Dim dWeek As Integer = 0
        Dim cDay As Integer = 0

        dDate = dMonth & "/1/" & dYear
        Do While Month(dDate) = dMonth
            If DatePart(DateInterval.Weekday, CDate(dDate)) = vbSunday Then
                dSunday = dSunday + 1
            End If

            If Month(dDate) = dMonth Then
                If DatePart(DateInterval.Weekday, CDate(dDate)) = vbSaturday Then
                    dWeek = dWeek + 1
                End If
            End If

            dDate = DateAdd(DateInterval.Day, 1, CDate(dDate))
            If Month(dDate) <> dMonth Then
                lDate = DateAdd(DateInterval.Day, -1, CDate(dDate))
                If DatePart(DateInterval.Weekday, CDate(lDate)) <> vbSaturday Then
                    dWeek = dWeek + 1
                End If
            End If
        Loop
        dDate = DateAdd(DateInterval.Day, -1, CDate(dDate))
        dDay = DatePart(DateInterval.Day, dDate)
        GetTotDayInMonth = dDay '- dSunday
    End Function

End Class
