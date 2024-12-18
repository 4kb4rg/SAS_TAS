Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Public Class IN_STDRPT_MUTASI_STOCK_BINPreview : Inherits Page
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim objINRpt As New agri.IN.clsReport()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objOk As New agri.GL.clsReport

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim tempLoc As String
    Dim strDDLAccMth As String
    Dim strDDLAccYr As String
    Dim rdCrystalViewer As New ReportDocument()
    Dim intErrNo As Integer
    Dim strBin As String
    Dim strItemCode As String
    Dim strItemCodeTo As String
    Dim strExportToExcel As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            crvView.Visible = False

            If Right(Request.QueryString("Location"), 1) = "," Then
                strUserLoc = Left(Request.QueryString("Location"), Len(Request.QueryString("Location")) - 1)
            Else
                strUserLoc = Trim(Request.QueryString("Location"))
            End If

            tempLoc = Request.QueryString("Location")
            If Right(tempLoc, 1) = "," Then
                Session("SS_LOC") = Left(tempLoc, Len(tempLoc) - 1)
            Else
                Session("SS_LOC") = tempLoc.Replace("','", ", ")
            End If

            strDDLAccMth = Request.QueryString("DDLAccMth")
            strDDLAccYr = Request.QueryString("DDLAccYr")
            strItemCode = "" & Request.QueryString("ItemCode") & ""
            strItemCodeTo = "" & Request.QueryString("ItemCodeTo") & ""
            strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim objRptDsItem As New DataSet()
        Dim objMapPath As String
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCode As String = "IN_STDRPT_STOCK_MUTASI_BIN_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objFTPFolder As String
        Dim strFileName As String

        strFileName = "IN_STDRPT_MUTASI_STOCK_BIN"
        strParamName = "LocCode|AccMonth|AccYear|ItemCode|ItemCodeTo"
        strParamValue = strUserLoc & _
                        "|" & Trim(strDDLAccMth) & _
                        "|" & Trim(strDDLAccYr) & _
                        "|" & Trim(strItemCode) & _
                        "|" & Trim(strItemCodeTo)

        Try
            intErrNo = objOk.mtdGetReportDataCommon(strOpCode, strParamName, strParamValue, objRptDsItem, objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ID&errmesg=" & Exp.ToString() & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
        End Try

        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        '   rdCrystalViewer.Load("C:\GreenGolden\Web\en\reports\crystal\IN_STDRPT_MUTASI_STOCK_BIN.rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDsItem.Tables(0))


        'rdCrystalViewer.Load("C:\GreenGolden\Web\en\reports\crystal\IN_STDRPT_MUTASI_STOCK_BIN.rpt", OpenReportMethod.OpenReportByTempCopy)
        'rdCrystalViewer.SetDataSource(objRptDsItem.Tables(0))

        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperLegal

        crvView.Visible = False
        crvView.ReportSource = rdCrystalViewer
        crvView.DataBind()

        PassParam()

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strFileName & ".pdf"

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

        'rdCrystalViewer.Export()
        'Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../ftp/IN_StdRpt_LaporanPosisiStock.pdf"">")

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

        objRptDsItem = Nothing
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

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")

        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOCATIONNAME")
        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)

        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)


        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
