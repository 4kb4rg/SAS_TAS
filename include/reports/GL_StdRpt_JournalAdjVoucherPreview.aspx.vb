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

Public Class GL_StdRpt_JournalAdjVourcherPreview : Inherits Page

    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblNoRecord As Label
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
    Dim strAccTag As String
    Dim strBlkTag As String
    Dim strRptId As String
    Dim strRptName As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intSelDecimal As Integer
    Dim strStatus As String
    Dim strSelSupress As String
    Dim strUserLoc As String
    Dim strJrnAdjIdFr As String
    Dim strJrnAdjIdTo As String
    Dim strSearchExp As String = ""
    Dim strUpdExp As String = ""

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

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            strUserLoc = Trim(Request.QueryString("Location"))
            strRptId = Trim(Request.QueryString("RptId"))
            strRptName = Trim(Request.QueryString("RptName"))
            strSelAccMonth = Request.QueryString("AccMonth")
            strSelAccYear = Request.QueryString("AccYear")
            strStatus = Request.QueryString("Status")
            intSelDecimal= CInt(Request.QueryString("Decimal"))
            strSelSupress = Request.QueryString("Supp")
            strAccTag = Request.QueryString("AccTag")
            strBlkTag = Request.QueryString("BlkTag")

            strJrnAdjIdFr = Trim(Request.QueryString("JournalAdjIDFrom"))
            strJrnAdjIdTo = Trim(Request.QueryString("JournalAdjTo"))

            strSearchExp = "AND HD.LocCode IN ('" & strUserLoc & "') "
      
            If Trim(strJrnAdjIdFr) <> "" Then
                strSearchExp = strSearchExp & "AND HD.JournalAdjID >= '" & strJrnAdjIdFr & "' "
                strUpdExp = strUpdExp & "AND JournalAdjID >= '" & strJrnAdjIdFr & "' "
            End If
            If Trim(strJrnAdjIdTo) <> "" Then
                strSearchExp = strSearchExp & "AND HD.JournalAdjID <= '" & strJrnAdjIdTo & "' "
                strUpdExp = strUpdExp & "AND JournalAdjID <= '" & strJrnAdjIdFr & "' "
            End If

            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCd As String = "GL_CLSREPORT_JOURNALADJ_VOUCHER_GET" & "|" & "GL_JOURNALADJVOUCHER"
        Dim strOpCd_Upd As String = "GL_CLSTRX_JOURNALADJ_DETAIL_UPD"
        Dim objRptDs As New Dataset()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim objFTPFolder As String

        strRptPrefix = "GL_StdRpt_JournalAdjVoucher"

        strParam = strSelAccMonth & "|" & _
                   strSelAccYear & "|" & _
                   strStatus & "|" & _
                   strSearchExp & "|" & _
                   strUpdExp

        Try
            intErrNo = objGLRpt.mtdGetReport_JournalAdjVoucher(strCompany, _
                                                           strLocation, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strOpCd, _
                                                           strOpCd_Upd, _
                                                           strParam, _
                                                           objRptDs, _
                                                           objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_JOURNALADJ_VOUCHER_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objRptDs.Tables("GL_JOURNALADJVOUCHER").Rows.Count = 0 Then
            lblNoRecord.visible = True
            Exit Sub
        End If

   
        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs)
        


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
        ParamDiscreteValue3.Value = strAccTag
        ParamDiscreteValue4.Value = strBlkTag
        ParamDiscreteValue5.Value = intSelDecimal

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SellerName")
        ParamFieldDef2 = ParamFieldDefs.Item("SessionUserName")
        ParamFieldDef3 = ParamFieldDefs.Item("AccTag")
        ParamFieldDef4 = ParamFieldDefs.Item("BlkCodeTag")
        ParamFieldDef5 = ParamFieldDefs.Item("DecimalPlace")

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
