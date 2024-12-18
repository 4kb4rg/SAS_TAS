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

Public Class AR_StdRpt_DebtorJournalPreview : Inherits Page

    Dim objBIRpt As New agri.BI.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objBITrx As New agri.BI.clsTrx()

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
    Dim strSelSupress As String
    Dim strSelStatus As String
    Dim strUserLoc As String
    Dim strFromJrnId As String
    Dim strToJrnId As String
    Dim strBillPartyCode As String
    Dim strJournalType As String
    Dim strSearchExp As String = ""
    Dim BillPartyTag As String
    Dim AccCodeTag As String    
    Dim BlkCodeTag As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Request.QueryString("SelAccMonth")
            strSelAccYear = Request.QueryString("SelAccYear")
            intSelDecimal= CInt(Request.QueryString("Decimal"))
            strSelSupress = Request.QueryString("Supp")
            strSelStatus = Trim(Request.QueryString("Status"))

            strFromJrnId = Trim(Request.QueryString("FromJrnId"))
            strToJrnId = Trim(Request.QueryString("ToJrnId"))
            strBillPartyCode = Trim(Request.QueryString("BillPartyCode"))
            strJournalType = Trim(Request.QueryString("JournalType"))
            BillPartyTag = Trim(Request.QueryString("BillPartyTag"))
            AccCodeTag = Trim(Request.QueryString("AccCodeTag"))
            BlkCodeTag = Trim(Request.QueryString("BlkCodeTag"))

            


            strSearchExp = " AND JRN.LocCode = '" & strUserLoc & "'" 
            
            If Trim(strFromJrnId) <> "" Then
                strSearchExp = strSearchExp & " AND JRN.DebtorJrnID >= '" & strFromJrnId & "'"
            End If

            If Trim(strToJrnId) <> "" Then
                strSearchExp = strSearchExp & " AND JRN.DebtorJrnID <= '" & strToJrnId & "'"
            End If

            If Trim(strBillPartyCode) <> "" Then
                strSearchExp = strSearchExp & " AND JRN.BillPartyCode LIKE '" & strBillPartyCode & "'"
            End If
   
            If Trim(strJournalType) <> "" Then
                strSearchExp = strSearchExp & " AND JRN.JrnType = '" & strJournalType & "'"
            End If
            
            If strSelStatus.Trim() <> objBITrx.EnumDebtorJournalStatus.All Then
                strSearchExp = strSearchExp & " AND JRN.Status = '" & strSelStatus & "'"
            End If
            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer

        strRptPrefix = "AR_StdRpt_DebtorJournal"

        Select Case CInt(strJournalType)
            Case objBITrx.EnumDebtorJournalType.Adjustment
                strOpCd = "AR_STDRPT_DEBTORJOURNAL_ADJUSTMENT_GET_SP" & "|" & "AR_DEBTORJRN"
            Case objBITrx.EnumDebtorJournalType.Void
                strOpCd = "AR_STDRPT_DEBTORJOURNAL_VOID_GET_SP" & "|" & "AR_DEBTORJRN"
            Case objBITrx.EnumDebtorJournalType.WriteOff
                strOpCd = "AR_STDRPT_DEBTORJOURNAL_WRITEOFF_GET_SP" & "|" & "AR_DEBTORJRN"
        End Select


    
        strParam = strSelAccMonth & "|" & _
                   strSelAccYear & "|" & _
                   strSearchExp & "|"

        Try
            intErrNo = objBIRpt.mtdGetReport_DebtorJournal(strOpCd, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           objRptDs, _
                                                           objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_DEBTORJOURNAL_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objRptDs.Tables("AR_DEBTORJRN").Rows.Count - 1
            objRptDs.Tables("AR_DEBTORJRN").Rows(intCnt).Item("Status") = objBITrx.mtdGetDebtorJournalStatus(objRptDs.Tables("AR_DEBTORJRN").Rows(intCnt).Item("Status"))
            objRptDs.Tables("AR_DEBTORJRN").Rows(intCnt).Item("JrnType") = objBITrx.mtdGetDebtorJournalType(objRptDs.Tables("AR_DEBTORJRN").Rows(intCnt).Item("JrnType"))
        Next

   
        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)
        


        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objMapPath & "web\ftp\" & strRptPrefix & ".pdf"

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        rdCrystalViewer.Export()
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/" & strRptPrefix & ".pdf"">")
    End Sub

    Sub PassParam()
        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParamFieldDef2 As ParameterFieldDefinition
        Dim ParamFieldDef3 As ParameterFieldDefinition
        Dim ParamFieldDef4 As ParameterFieldDefinition
        Dim ParamFieldDef5 As ParameterFieldDefinition

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()
        Dim ParameterValues5 As New ParameterValues()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()

        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strPrintedBy
        ParamDiscreteValue3.Value = strLocationName
        ParamDiscreteValue4.Value = AccCodeTag
        ParamDiscreteValue5.Value = BlkCodeTag

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SellerName")
        ParamFieldDef2 = ParamFieldDefs.Item("SessionUserName")
        ParamFieldDef3 = ParamFieldDefs.Item("SessionLocName")
        ParamFieldDef4 = ParamFieldDefs.Item("AccTag")
        ParamFieldDef5 = ParamFieldDefs.Item("BlkCodeTag")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues
        ParameterValues4 = ParamFieldDef4.CurrentValues
        ParameterValues5 = ParamFieldDef5.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)
        ParameterValues5.Add(ParamDiscreteValue5)

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
        ParamFieldDef5.ApplyCurrentValues(ParameterValues5)

    End Sub

End Class
