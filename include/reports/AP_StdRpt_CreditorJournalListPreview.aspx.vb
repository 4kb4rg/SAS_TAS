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

Public Class AP_StdRpt_CreditorJournalList_Preview : Inherits Page

    Dim objAPRpt As New agri.AP.clsReport()
    Dim objAPTrx As New agri.AP.clsTrx()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim rdCrystalViewer As ReportDocument
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
    Dim strSelLocation As String

    Dim strFromCreditJrnId As String
    Dim strToCreditJrnId As String
    Dim strSupplierCode As String
    Dim strJournalType As String
    Dim strJournalTypeDesc As String
    Dim strStatus As String
    Dim strStatusDesc As String
    Dim strSearchExp As String = ""

    Dim strLocTag As String
    Dim strAccountTag As String
    Dim strBlockTag As String
    Dim strVehTag As String
    Dim strVehExpenseTag As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strSelLocation = Trim(Request.QueryString("Location"))
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Request.QueryString("DDLAccMth")
            strSelAccYear = Request.QueryString("DDLAccYr")
            intSelDecimal= CInt(Request.QueryString("Decimal"))

            strFromCreditJrnId = Trim(Request.QueryString("CJIDFrom"))
            strToCreditJrnId = Trim(Request.QueryString("CJIDTo"))
            strSupplierCode = Trim(Request.QueryString("SupplierCode"))
            strJournalType = Trim(Request.QueryString("JournalType"))
            strStatus = Trim(Request.QueryString("Status"))
            strLocTag = Trim(Request.QueryString("LocTag"))
            strAccountTag = Trim(Request.QueryString("AccountTag"))
            strBlockTag = Trim(Request.QueryString("BlockTag"))
            strVehTag = Trim(Request.QueryString("VehTag"))
            strVehExpenseTag = Trim(Request.QueryString("VehExpenseTag"))

            If strJournalType = "" Then
                strJournalTypeDesc = "All"
            Else
                strJournalTypeDesc = objAPTrx.mtdGetCreditorJournalType(CInt(strJournalType))
            End If

            If strStatus = "" Then
                strStatusDesc = "All"
            Else
                strStatusDesc = objAPTrx.mtdGetCreditorJournalStatus(CInt(strStatus))
            End If

            strSearchExp = "and cj.LocCode in ('" & strSelLocation & "') " 
     
            If Trim(strFromCreditJrnId) <> "" Then
                strSearchExp = strSearchExp & "and cj.CreditJrnID >= '" & strFromCreditJrnId & "' "
            End If

            If Trim(strToCreditJrnId) <> "" Then
                strSearchExp = strSearchExp & "and cj.CreditJrnID <= '" & strToCreditJrnId & "' "
            End If

            If Trim(strSupplierCode) <> "" Then
                strSearchExp = strSearchExp & "and (cj.SupplierCode like '%" & strSupplierCode & "%' or sup.Name like '%" & strSupplierCode & "%') "
            End If
   
            If Trim(strJournalType) <> "" Then
                strSearchExp = strSearchExp & "and ln.DocType = '" & strJournalType & "' "
            End If

            If Trim(strStatus) <> "" Then
                strSearchExp = strSearchExp & "and cj.Status = '" & strStatus & "' "
            End If

            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim strOpCdRslGet As String = ""
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim objFTPFolder As String

        strRptPrefix = "AP_StdRpt_CreditJrnList"
        strOpCd = "AP_STDRPT_CREDITOR_JOURNAL_LIST_GET"
      
        strParam = strSelAccMonth & "|" & _
                   strSelAccYear & "|" & _
                   strSearchExp

        Try
            intErrNo = objAPRpt.mtdGetReport_CreditorJournalList(strOpCd, _
                                                                 strCompany, _
                                                                 strLocation, _
                                                                 strUserId, _
                                                                 strAccMonth, _
                                                                 strAccYear, _
                                                                 strParam, _
                                                                 objRptDs, _
                                                                 objMapPath, _
                                                                 objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_CREDITOR_JOURNAL_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

   
        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
        


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

        If instr(strSelLocation, "','") > 0 Then
            strSelLocation = Replace(strSelLocation, "','", ", ")
        End If

        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strPrintedBy
        ParamDiscreteValue3.Value = strRptName
        ParamDiscreteValue4.Value = strRptId
        ParamDiscreteValue5.Value = strSelLocation
        ParamDiscreteValue6.Value = strSelAccMonth
        ParamDiscreteValue7.Value = strSelAccYear
        ParamDiscreteValue8.Value = strFromCreditJrnId
        ParamDiscreteValue9.Value = strToCreditJrnId
        ParamDiscreteValue10.Value = strSupplierCode
        ParamDiscreteValue11.Value = strJournalTypeDesc 
        ParamDiscreteValue12.Value = intSelDecimal
        ParamDiscreteValue13.Value = UCase(strLocTag)
        ParamDiscreteValue14.Value = strLocTag
        ParamDiscreteValue15.Value = strAccountTag
        ParamDiscreteValue16.Value = strBlockTag
        ParamDiscreteValue17.Value = strVehTag
        ParamDiscreteValue18.Value = strVehExpenseTag
        ParamDiscreteValue19.Value = strStatusDesc

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SessionCompName")
        ParamFieldDef2 = ParamFieldDefs.Item("SessionUserName")
        ParamFieldDef3 = ParamFieldDefs.Item("RptName")
        ParamFieldDef4 = ParamFieldDefs.Item("RptId")
        ParamFieldDef5 = ParamFieldDefs.Item("SelLocCode")
        ParamFieldDef6 = ParamFieldDefs.Item("SelAccMonth")
        ParamFieldDef7 = ParamFieldDefs.Item("SelAccYear")
        ParamFieldDef8 = ParamFieldDefs.Item("SelFromId")
        ParamFieldDef9 = ParamFieldDefs.Item("SelToId")
        ParamFieldDef10 = ParamFieldDefs.Item("SelSupplierCode")
        ParamFieldDef11 = ParamFieldDefs.Item("SelJournalType")
        ParamFieldDef12 = ParamFieldDefs.Item("SelDecimal")
        ParamFieldDef13 = ParamFieldDefs.Item("ULocTag")
        ParamFieldDef14 = ParamFieldDefs.Item("LocTag")
        ParamFieldDef15 = ParamFieldDefs.Item("AccountTag")
        ParamFieldDef16 = ParamFieldDefs.Item("BlockTag")
        ParamFieldDef17 = ParamFieldDefs.Item("VehTag")
        ParamFieldDef18 = ParamFieldDefs.Item("VehExpenseTag")
        ParamFieldDef19 = ParamFieldDefs.Item("SelStatus")

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

    End Sub

End Class
