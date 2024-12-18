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

Public Class CB_StdRpt_PaymentSchedPerBankPreview : Inherits Page

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
    Dim objAdmin As New agri.Admin.clsShare

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
    Dim strFileName  As String
    
    Dim strRptID As String
    Dim strRptTitle As String
    Dim intSelDecimal As String
    
    Dim strDateTo As String
    Dim strDateFrom As String
    Dim strBankCode As String

    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
       

        strFileName = "CB_StdRpt_PaymentSchedPerBank"

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strLangCode = Session("SS_LANGCODE")
        intConfig = Session("SS_CONFIGSETTING")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")

        strRptID = Trim(Request.QueryString("RptID"))
        strRptTitle = Trim(Request.QueryString("RptTitle"))
        intSelDecimal = Cint(Request.QueryString("Decimal"))
        strDateFrom  = Trim(Request.QueryString("DateFrom"))
        strDateTo  = Trim(Request.QueryString("DateTo"))
        strBankCode  = Trim(Request.QueryString("BankCode"))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim objRptDs As New DataSet()
        Dim objRptPay As New DataSet()
        Dim dsLoc As New DataSet()
        Dim crExportOptions As ExportOptions
        Dim strParam  As String
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCode As String = "CB_STDRPT_PAYMENTSCHEDPERBANK_GET"
      
        Dim strOpCd_CompLoc_GET As String = "ADMIN_CLSSHARE_COMPLOC_DETAILS_GET"
        Dim objFTPFolder As String

        strParam = strDateFrom  & "|" & strDateTo & "|" & strBankCode  & "|" & strLocation

        Try
            intErrNo = objCB.mtdGetReport_PaymentSchedule(strOpCode, strParam, objRptPay, objMapPath, objFTPFolder)

            objRptPay.Tables(0).TableName = "CB_BANK_ACCOUNT"
            objRptPay.Tables(1).TableName = "CB_PAYMENT_SCHEDULE"
            objRptPay.Tables(2).TableName = "CB_BANK_SUMMARY"

        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=CB_StdRpt_PaymentSchedPerBank&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strParam = strCompany & "|" & strLocation & "|"
        Try
            intErrNo = objAdmin.mtdGetLocDetails(strOpCd_CompLoc_GET, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 dsLoc, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CB_PaymentSchedPerBank_LocCode&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        objRptDs.Tables.Add(objRptPay.Tables(0).Copy())
        objRptDs.Tables(0).TableName = "CB_BANK_ACCOUNT"
        objRptDs.Tables.Add(objRptPay.Tables(2).Copy())
        objRptDs.Tables(1).TableName = "CB_BANK_SUMMARY"
        objRptDs.Tables.Add(dsLoc.Tables(0).Copy())
        objRptDs.Tables(2).TableName = "CB_LOCDET"
        objRptDs.Tables.Add(objRptPay.Tables(1).Copy())
        objRptDs.Tables(3).TableName = "CB_PAYMENT_SCHEDULE"

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA4

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer          
        crvView.DataBind()                              

        PassParam()

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
       
        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()
        Dim ParameterValues6 As New ParameterValues()
        Dim ParameterValues7 As New ParameterValues()
        Dim ParameterValues8 As New ParameterValues()
        Dim ParameterValues9 As New ParameterValues()
       
        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue8 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue9 As New ParameterDiscreteValue()
       
        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strLocation
        ParamDiscreteValue3.Value = strUserName
        ParamDiscreteValue4.Value = intSelDecimal
        ParamDiscreteValue5.Value = strRptID
        ParamDiscreteValue6.Value = UCase(strRptTitle)
        ParamDiscreteValue7.Value = strDateTo
        ParamDiscreteValue8.Value = strDateFrom
        ParamDiscreteValue9.Value = strBankCode
       
        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("ParamCompanyName")
        ParamFieldDef2 = ParamFieldDefs.Item("ParamLocation")
        ParamFieldDef3 = ParamFieldDefs.Item("ParamUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("ParamDecimal")
        ParamFieldDef5 = ParamFieldDefs.Item("ParamRptID")
        ParamFieldDef6 = ParamFieldDefs.Item("ParamRptName")
        ParamFieldDef7 = ParamFieldDefs.Item("ParamDateTo")
        ParamFieldDef8 = ParamFieldDefs.Item("ParamDateFrom")
        ParamFieldDef9 = ParamFieldDefs.Item("ParamBankCode")
       
        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues
        ParameterValues6 = ParamFieldDef6.CurrentValues
        ParameterValues7 = ParamFieldDef7.CurrentValues
        ParameterValues8 = ParamFieldDef8.CurrentValues
        ParameterValues9 = ParamFieldDef9.CurrentValues
        
        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)
        ParameterValues6.Add(ParamDiscreteValue6)
        ParameterValues7.Add(ParamDiscreteValue7)
        ParameterValues8.Add(ParamDiscreteValue8)
        ParameterValues9.Add(ParamDiscreteValue9)
       
        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)
        ParamFieldDef6.ApplyCurrentValues(ParameterValues6)
        ParamFieldDef7.ApplyCurrentValues(ParameterValues7)
        ParamFieldDef8.ApplyCurrentValues(ParameterValues8)
        ParamFieldDef9.ApplyCurrentValues(ParameterValues9)
       

    End Sub

End Class
