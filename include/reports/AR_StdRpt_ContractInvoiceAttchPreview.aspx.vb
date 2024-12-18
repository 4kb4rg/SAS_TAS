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

Public Class AR_StdRpt_ContractInvoiceAttchPreview : Inherits Page

    Dim objBIRpt As New agri.BI.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objCMTrx As New agri.CM.clsTrx()

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
    Dim strUserLoc As String
    Dim strFromMatchId As String
    Dim strToMatchId As String
    Dim strBillPartyCode As String
    Dim strSearchExp As String = ""
    Dim strSearchExp2 As String = ""
    Dim BillPartyTag As String

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

            strFromMatchId = Trim(Request.QueryString("FromMatchId"))
            strToMatchId = Trim(Request.QueryString("ToMatchId"))
            strBillPartyCode = Trim(Request.QueryString("BillPartyCode"))
            BillPartyTag = Trim(Request.QueryString("BillPartyTag"))


            strSearchExp = "and mat.Status = '" & objCMTrx.EnumContractMatchStatus.Confirmed & "' "
            strSearchExp2 = "where ctr.Status = '" & objCMTrx.EnumContractMatchStatus.Confirmed & "' "
            
            If Trim(strFromMatchId) <> "" Then
                strSearchExp = strSearchExp & "and mat.MatchingId >= '" & strFromMatchId & "' "
                strSearchExp2 = strSearchExp2 & "and ti.MatchingId >= '" & strFromMatchId & "' "
            End If

            If Trim(strToMatchId) <> "" Then
                strSearchExp = strSearchExp & "and mat.MatchingId <= '" & strToMatchId & "' "
                strSearchExp2 = strSearchExp2 & "and ti.MatchingId <= '" & strToMatchId & "' "
            End If

            If Trim(strBillPartyCode) <> "" Then
                strSearchExp = strSearchExp & "and mat.BuyerCode like '" & strBillPartyCode & "' "
                strSearchExp2 = strSearchExp2 & "and ti.CustomerCode  like '" & strBillPartyCode & "' "
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


        strRptPrefix = "AR_StdRpt_InvoiceAttch"
        strOpCd = "AR_STDRPT_INVOICE_ATTCH_GET_MATCH_SP" & "|" & "AR_INVATTCH_MATCH" & _
                  Chr(9) & "AR_STDRPT_INVOICE_ATTCH_GET_TICKET_SP" & "|" & "AR_INVATTCH_TICKET"

        strParam = strSelAccMonth & "|" & _
                   strSelAccYear & "|" & _
                   strSearchExp & "|" & _
                   strSearchExp2


        Try
            intErrNo = objBIRpt.mtdGetReport_InvoiceAttch(strOpCd, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          objRptDs, _
                                                          objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_CONTRACTINVOICE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


   
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

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()

        ParamDiscreteValue1.Value = strCompanyName
        ParamDiscreteValue2.Value = strLocationName

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("strCompName")
        ParamFieldDef2 = ParamFieldDefs.Item("strLocName")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)

    End Sub

End Class
