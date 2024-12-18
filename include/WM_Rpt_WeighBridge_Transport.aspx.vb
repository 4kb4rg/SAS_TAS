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

Imports agri.GL
Imports agri.Admin.clsShare
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class WM_Rpt_Weighbridge_Transport : Inherits Page

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblErrMesage As Label

    Dim objGLRpt As New agri.GL.clsReport()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim rdCrystalViewer As New ReportDocument()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String

    Dim strSelLoc As String
    Dim strSelComp As String
    Dim strSearch As String
    Dim strSearchMonth As String
    Dim strSearchYear As String
    Dim strTerbilang As String = ""
    Dim strUserName As String
    Dim srchTicketNo As String
    Dim srchContractNo As String
    Dim srchDONo As String
    Dim srchCust As String
    Dim srchProductList As String


    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strUserName = Session("SS_USERNAME")


        crvView.Visible = False

        strSelLoc = Trim(Request.QueryString("Location"))
        strSelComp = Trim(Request.QueryString("CompName"))
        strSearch = Trim(Request.QueryString("strSearch"))
        strSearchMonth = Trim(Request.QueryString("strSearchMonth"))
        strSearchYear = Trim(Request.QueryString("strSearchYear"))
        srchTicketNo = Trim(Request.QueryString("srchTicketNo"))
        srchContractNo = Trim(Request.QueryString("srchContractNo"))
        srchDONo = Trim(Request.QueryString("srchDONo"))
        srchCust = Trim(Request.QueryString("srchCust"))
        srchProductList = Trim(Request.QueryString("srchProductList"))

        Bind_ITEM(True)

    End Sub

    Sub Bind_ITEM(ByVal blnIsPDFFormat As Boolean)

        Dim crLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim objRptDs As New DataSet()
        Dim objRptDs1 As New DataSet
        Dim objMapPath As String
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strReportName As String
        Dim strPDFName As String
        Dim strOpCd As String = ""
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objFTPFolder As String
        Dim TtlAmt As String
        Dim strBeforeDec As String

        Dim sSQLKriteria As String = ""

        'sSQLKriteria = "AND r.AccYear='" & strSearchYear & "' And r.AccMonth='" & strSearchMonth & "'"

        'If strSearchMonth = 0 Then
        '    sSQLKriteria = "AND r.AccYear='" & strSearchYear & "' "
        'Else
        '    sSQLKriteria = "AND r.AccYear='" & strSearchYear & "' And r.AccMonth='" & strSearchMonth & "'"
        'End If

        If srchTicketNo <> "" Then
            sSQLKriteria = sSQLKriteria & "AND k.TicketNo LIKE '%" & srchTicketNo & "%'"
        End If
        If srchContractNo <> "" Then
            sSQLKriteria = sSQLKriteria & "AND k.ContractNo LIKE '%" & srchContractNo & "%'"
        End If


        If srchDONo <> "" Then
            sSQLKriteria = sSQLKriteria & "AND k.DONo LIKE '%" & srchDONo & "%'"
        End If
        If srchCust <> "" Then
            sSQLKriteria = sSQLKriteria & "AND b.Name LIKE '%" & srchCust & "%'"
        End If

        'If srchProductList <> 0 Then
        '    sSQLKriteria = sSQLKriteria & "AND r.ProductCode = '" & srchProductList & "'"
        'End If

        strOpCd = "WM_CLSTRX_WEIGHBRIDGE_TICKET_EDIT_LIST_GET_REPORT"
        strParamName = "LOCCODE|STRSEARCH"
        strParamValue = strLocation & "|" & sSQLKriteria


        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_SPK_INFO&errmesg=" & "" & "&redirect=../WM/trx/WM_Rpt_WeightBridgeTicketList.aspx")
        End Try

        strReportName = "WM_Rpt_Weighbridge_Transport"
        If objRptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
                If objRptDs.Tables(0).Rows(intCnt).Item("TotalDibayar") = 0 Then
                    strTerbilang = ""
                    objRptDs.Tables(0).Rows(intCnt).Item("Terbilang") = strTerbilang
                Else
                    TtlAmt = Trim(CStr(FormatNumber(objRptDs.Tables(0).Rows(intCnt).Item("TotalDibayar"), 2)))
                    strBeforeDec = objGlobal.TerbilangDesimal(Replace(TtlAmt, ",", ""), "Rupiah")
                    strTerbilang = strBeforeDec
                    objRptDs.Tables(0).Rows(intCnt).Item("Terbilang") = Replace(Left(LTrim(LCase(objGlobal.TerbilangDesimal(Replace(TtlAmt, ",", ""), "Rupiah"))), 1), Left(LTrim(LCase(objGlobal.TerbilangDesimal(Replace(TtlAmt, ",", ""), "Rupiah"))), 1), UCase(Left(LTrim(LCase(objGlobal.TerbilangDesimal(Replace(TtlAmt, ",", ""), "Rupiah"))), 1))) + Mid(LTrim(LCase(objGlobal.TerbilangDesimal(Replace(TtlAmt, ",", ""), "Rupiah"))), 2, Len(LTrim(LCase(objGlobal.TerbilangDesimal(Replace(TtlAmt, ",", ""), "Rupiah")))) - 1)
                End If
            Next
        End If


        rdCrystalViewer.Load(objMapPath & "Web\EN\WM\Reports\Crystal\" & strReportName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDs.Tables(0))

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        'If strExportToExcel = "0" Then
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strReportName & ".pdf"
        'Else
        'crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strReportName & ".xls"
        'End If

        crExportOptions = rdCrystalViewer.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            'If strExportToExcel = "0" Then
            .ExportFormatType = ExportFormatType.PortableDocFormat
            'Else
            '.ExportFormatType = ExportFormatType.Excel
            'End If
        End With

        rdCrystalViewer.Export()
        rdCrystalViewer.Close()
        rdCrystalViewer.Dispose()

        Dim strUrl As String
        strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
        strUrl = Replace(strUrl, "\", "/")

        'If strExportToExcel = "0" Then
        Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../" & strUrl & strReportName & ".pdf"">")
        'Else
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../../" & strUrl & strReportName & ".xls"">")
        'End If

        objRptDs.Dispose()
        If Not objRptDs Is Nothing Then
            objRptDs = Nothing
        End If

    End Sub

    Sub PassParam()
        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParamFieldDef2 As ParameterFieldDefinition
        Dim ParamFieldDef3 As ParameterFieldDefinition
        Dim ParamFieldDef4 As ParameterFieldDefinition

        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
        Dim ParameterValues3 As New ParameterValues()
        Dim ParameterValues4 As New ParameterValues()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()

        ParamDiscreteValue1.Value = strSearchMonth
        ParamDiscreteValue2.Value = strSearchYear
        ParamDiscreteValue3.Value = strUserName
        ParamDiscreteValue4.Value = strCompanyName

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("prmAccMonth")
        ParamFieldDef2 = ParamFieldDefs.Item("prmAccYear")
        ParamFieldDef3 = ParamFieldDefs.Item("prmUserName")
        ParamFieldDef4 = ParamFieldDefs.Item("prmCompName")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
        ParameterValues3 = ParamFieldDef3.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
        ParameterValues3.Add(ParamDiscreteValue3)
        ParameterValues4.Add(ParamDiscreteValue4)

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
        ParamFieldDef3.ApplyCurrentValues(ParameterValues3)
        ParamFieldDef4.ApplyCurrentValues(ParameterValues4)
    End Sub

End Class

