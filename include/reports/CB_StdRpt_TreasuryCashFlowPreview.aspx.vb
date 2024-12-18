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

Public Class CB_StdRpt_TreasuryCashFlowPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objCB As New agri.CB.clsReport()
    Dim objCBTrx As New agri.CB.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdminCty As New agri.Admin.clsCountry()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()

    
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
    
    Dim strDateTo As String
    Dim strDateFrom As String
    Dim strRptPkID As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        crvView.Visible = False

        strFileName = "CB_StdRpt_TreasuryCashFlow"

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        intConfig = Session("SS_CONFIGSETTING")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")

        strLocType = Session("SS_LOCTYPE")

        strRptID = Trim(Request.QueryString("RptID"))
        strRptTitle = Trim(Request.QueryString("RptTitle"))
        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strDateTo = Trim(Request.QueryString("DateTo"))
        strDateFrom = Trim(Request.QueryString("DateFrom"))
        strRptPkID = Trim(Request.QueryString("RptPkID"))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            onload_GetLangCap()
            BindReport()
        End If
    End Sub



    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim strParam As String 
        Dim crExportOptions As ExportOptions

        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim pv_strOpCdCalculate As String = "CB_CLSTRX_TREASURY_CALCULATE"
        Dim pv_strOpCdGetFormula As String = "CB_TRX_TREASURYTEMPLDTL_GET"
        Dim pv_strOpCdGetFigure As String = "CB_CLSTRX_TREASURYFIG_REPORT_GET"
        Dim pv_strOpCdGetFormulaFig As String = "CB_CLSTRX_TREASURYFIG_AMOUNT_GET"
        Dim pv_strOpCdCalc As String = "CB_CLSTRX_TREASURYFORMULA_CALCULATE"
        Dim objFTPFolder As String

        strParam = strRptPkID & "|" & strDateFrom & "|" & strDateTo 

        Try
            intErrNo = objCB.mtdGetReport_TreasuryCashFlow(pv_strOpCdCalculate, _
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=CB_StdRpt_TreasuryCashFlow&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind() 


        PassParam()

        Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
        Dim myLogin As CrystalDecisions.Shared.TableLogOnInfo

        For Each myTable In rdCrystalViewer.Database.Tables
            If myTable.Name = "CB_StdRpt_TreasuryCashFlow" Then
                myLogin = myTable.LogOnInfo
                Try
                    intErrNo = objGLRpt.mtdGet_DBConnObj(myLogin)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_MTHENDTRX_GET&errmesg=" & Exp.ToString() & "&redirect=")
                End Try

                myTable.ApplyLogOnInfo(myLogin)
            End If                                          
        Next

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".pdf"

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
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".pdf"">")
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")

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
        
        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strLocation
        ParamDiscreteValue3.Value = strUserName
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = LocationTag
        ParamDiscreteValue6.Value = strRptID
        ParamDiscreteValue7.Value = strRptTitle
        ParamDiscreteValue8.Value = strDateFrom
        ParamDiscreteValue9.Value = strDateTo
        ParamDiscreteValue10.Value = strRptPkID
        
        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("prmCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("prmLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("prmUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("prmDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("prmLocationTag")
        ParamFieldDef6 = ParamFieldDefs.Item("RptId")
        ParamFieldDef7 = ParamFieldDefs.Item("RptName")
        ParamFieldDef8 = ParamFieldDefs.Item("prmStartDate")
        ParamFieldDef9 = ParamFieldDefs.Item("prmEndDate")
        ParamFieldDef10 = ParamFieldDefs.Item("prmRptId")
       
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


End Class
