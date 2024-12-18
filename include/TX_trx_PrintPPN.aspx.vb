Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.XML
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Imports agri.PU.clsTrx
Imports agri.Admin.clsShare
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class TX_trx_PrintPPN : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMessage As Label

    Dim objGLRpt As New agri.GL.clsReport()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strPPNInit As String
    Dim strDocStatus As String
    Dim strPostAccMonth As String
    Dim strPostAccYear As String
    Dim strDocID As String
    Dim strFPNo As String
    Dim strSupplier As String
    Dim strExportToExcel As String

    Dim strCompName As String
    Dim strLocName As String
    Dim strUserName As String

    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strCompName = Session("SS_COMPANYNAME")
        strLocName = Session("SS_LOCATIONNAME")
        strUserName = Session("SS_USERNAME")

        crvView.Visible = False

        strPostAccMonth = Trim(Request.QueryString("PostAccMonth"))
        strPostAccYear = Trim(Request.QueryString("PostAccYear"))
        strFPNo = Trim(Request.QueryString("FPNo"))
        strDocID = Trim(Request.QueryString("DocID"))
        strSupplier = Trim(Request.QueryString("Supplier"))
        strPPNInit = Trim(Request.QueryString("PPNInit"))
        strDocStatus = Trim(Request.QueryString("DocStatus"))
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

        Bind_ITEM(True)
    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)
        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim strOpCd As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strReportName As String
        Dim strPDFName As String
        Dim objFTPFolder As String
        Dim strSearch As String

        If Trim(UCase(strPPNInit)) = "MASUKAN" Then
            strOpCd = "TX_STDRPT_GET_PPNMASUKAN_LIST_REPORT"
            strReportName = "RptPPNMasukanListing"
        Else
            strOpCd = "TX_STDRPT_GET_PPNKELUARAN_LIST_REPORT"
            strReportName = "RptPPNKeluaranListing"
        End If

        strSearch = IIf(Trim(strFPNo) = "", "", "FPNo Like '%" & Trim(strFPNo) & "%' AND ")
        strSearch = strSearch + IIf(Trim(strDocID) = "", "", "DocID LIKE '%" & Trim(strDocID) & "%' AND ")
        strSearch = strSearch + IIf(Trim(strSupplier) = "", "", "(SupplierCode LIKE '%" & Trim(strSupplier) & "%' OR SupplierName LIKE '%" & Trim(strSupplier) & "%') AND ")

        If (Trim(strSearch) <> "") Then
            strSearch = " AND " & Mid(strSearch, 1, Len(strSearch) - 4) & " "
        End If

        strParamName = "LOCCODE|POSTACCMONTH|POSTACCYEAR|STATUS|STRSEARCH|ORDERBY"
        strParamValue = strLocation & "|" & strPostAccMonth & "|" & strPostAccYear & "|" & strDocStatus & "|" & strSearch & "|" & "ORDER BY FPDate, FPNo ASC"

        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
            intErrNo = objAdmin.mtdGetBasePath(objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objRptDs.Tables(0).Rows.Count > 0 Then
        Else
            Response.Write("Cannot found any data !")
            Exit Sub
        End If

        rdCrystalViewer.Load(objMapPath & "Web\EN\TX\Reports\Crystal\" & strReportName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()
        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()

        If strExportToExcel = "0" Then
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strReportName & ".pdf"
        Else
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strReportName & ".xls"
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
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../" & strUrl & strReportName & ".pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../" & strUrl & strReportName & ".xls"">")
        End If


    End Sub

    Sub PassParam()
        Dim paramFields As New ParameterFields()
        Dim paramField1 As New ParameterField()
        Dim paramField2 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("strPrintedBy")
        paramField2 = paramFields.Item("strCompName")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues

        ParamDiscreteValue1.Value = strUserName
        ParamDiscreteValue2.Value = strCompName

        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)

        crvView.ParameterFieldInfo = paramFields
    End Sub

End Class

