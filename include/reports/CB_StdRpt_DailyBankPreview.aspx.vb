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

Public Class CB_StdRpt_MutasiBankPreview : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdminCty As New agri.Admin.clsCountry()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strLocName As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfig As Integer
    Dim objMapPath As String
    
    Dim strRptID As String
    Dim strRptTitle As String
    Dim intSelDecimal As String

    Dim strDate As String
    Dim strDateTo As String
    Dim strAccCode As String
    Dim strFileName As String
    Dim strUserLoc As String
    Dim strDispLoc As String
    Dim strExportToExcel As String
    Dim strKomparatif As String
    Dim strSaldo As String
    Dim strRptInit As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        crvView.Visible = False

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        intConfig = Session("SS_CONFIGSETTING")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocation = Session("SS_LOCATION")
        strLocName = Session("SS_LOCATIONNAME")

        strRptID = Trim(Request.QueryString("RptID"))
        strRptTitle = Trim(Request.QueryString("RptTitle"))
        intSelDecimal = Cint(Request.QueryString("Decimal"))
        strDate = Trim(Request.QueryString("Date"))
        strDateTo = Trim(Request.QueryString("DateTo"))
        strAccCode = Trim(Request.QueryString("AccCode"))
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))
        strKomparatif = Trim(Request.QueryString("Komparatif"))
        strSaldo = Trim(Request.QueryString("Saldo"))
        strRptInit = Trim(Request.QueryString("RptInit"))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub



    Sub BindReport()
      
        Dim objRptDs As New DataSet()
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objMapPath As String

        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCode As String = "CB_STDRPT_DAILYBANK_GET"
        Dim strFileName As String

        Dim strParam As String
        Dim searchStr As String
        Dim objFTPFolder As String

        

        strParamName = "TRXDATE|TRXDATETO|ACCCODE|LOCCODE|KOMPARATIF|SALDO"
        strParamValue = strDate & "|" & strDateTo & "|" & strAccCode & "|" & strLocation & "|" & strKomparatif & "|" & strSaldo

        Try
            intErrNo = objGLRpt.mtdGetReport_BiayaProduksi(strOpCode, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

            

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=CB_STDRPT_DAILYBANK_RPT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        'rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        'rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        Select Case strRptInit
            Case "Daily"
                strFileName = "CB_StdRpt_DailyBank_Report"

                rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
                rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

            Case "Saldo"
                strFileName = "CB_StdRpt_DailyBankSaldo_Report"

                rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
                rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

            Case "Komparatif"
                strFileName = "CB_StdRpt_DailyBankKomparatif_Report"
                objRptDs.Tables(1).TableName = "CB_StdRpt_DailyBank_Header"
                objRptDs.Tables(0).TableName = "CB_StdRpt_DailyBank_Report"

                rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
                rdCrystalViewer.SetDataSource(objRptDs)
        End Select

        'rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        'rdCrystalViewer.PrintOptions.PaperSize = PaperSize.A4

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        'crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"

        'crExportOptions = rdCrystalViewer.ExportOptions
        'With crExportOptions
        '    .DestinationOptions = crDiskFileDestinationOptions
        '    .ExportDestinationType = ExportDestinationType.DiskFile
        '    .ExportFormatType = ExportFormatType.PortableDocFormat
        'End With

        'rdCrystalViewer.Export()

        'rdCrystalViewer.Close()
        'rdCrystalViewer.Dispose()

        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        If strExportToExcel = "0" Then
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".pdf"
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".pdf"
        Else
            'crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strFileName & ".xls"
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
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".pdf"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".pdf"">")
        Else
            'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strFileName & ".xls"">")
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strFileName & ".xls"">")
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
      
       
        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()
        Dim ParameterValues6 As New ParameterValues()
        Dim ParameterValues7 As New ParameterValues()
    
       
        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
     
       
        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strLocName
        ParamDiscreteValue3.Value = strUserName
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strRptID
        ParamDiscreteValue6.Value = UCase(strRptTitle)
        ParamDiscreteValue7.Value = strDate
      
        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamDate")
       
        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues
        ParameterValues6 = ParamFieldDef6.CurrentValues
        ParameterValues7 = ParamFieldDef7.CurrentValues
      
        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)
        ParameterValues6.Add(ParamDiscreteValue6)
        ParameterValues7.Add(ParamDiscreteValue7)
      
        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)
        ParamFieldDef6.ApplyCurrentValues(ParameterValues6)
        ParamFieldDef7.ApplyCurrentValues(ParameterValues7)
      

    End Sub

End Class
