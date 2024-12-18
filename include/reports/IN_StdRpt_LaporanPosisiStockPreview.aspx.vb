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

Public Class IN_StdRpt_LaporanPosisiStockPreview : Inherits Page
    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents tblCrystal As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents EventData As DataGrid

    Dim objINRpt As New agri.IN.clsReport()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLrpt As New agri.GL.clsReport()

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
    Dim strStorageCode As String
    Dim strItemCode As String
    Dim strSDate_Trx As String
    Dim strEDate_Trx As String
	Dim strRptType As String
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

            strBin = Trim(Request.QueryString("Bin"))
            strStorageCode = Trim(Request.QueryString("StorageCode"))
            strItemCode = Trim(Request.QueryString("ItemCode"))
            strSDate_Trx = Trim(Request.QueryString("SDate_Trx"))
            strEDate_Trx = Trim(Request.QueryString("EDate_Trx"))
			strRptType = Trim(Request.QueryString("RptType"))
            strExportToExcel = Trim(Request.QueryString("ExportToExcel"))
            BindReport()
        End If
    End Sub


    Sub BindReport()
        Dim objRptDsItem As New DataSet()
        Dim objMapPath As String
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim strOpCode As String = "IN_STDRPT_LAPORAN_POSISI_STOCK_STORAGE"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim _AccMonth As String
        Dim _AccYear As String
        Dim objFTPFolder As String
        Dim strFileName As String

        If Month(strSDate_Trx) = 1 Then
            _AccMonth = "12"
            _AccYear = Str(Year(strSDate_Trx) - 1)
        Else
            _AccMonth = Str(Month(strSDate_Trx) - 1)
            _AccYear = Str(Year(strSDate_Trx))
        End If


		If Trim(strRptType) = "0" Then
			strFileName = "IN_StdRpt_LaporanPosisiStock_Storage"
			strOpCode = "IN_STDRPT_LAPORAN_POSISI_STOCK_STORAGE"
		Else
			strFileName = "IN_StdRpt_LaporanPosisiStockRekap_Storage"
			strOpCode = "IN_STDRPT_LAPORAN_POSISI_STOCK_STORAGE"
		End If
 

        strParamName = "STORAGE|ITEM|DTFR|DTTO|LOC"
        strParamValue = strStorageCode & _
                        "|" & strItemCode & _                        
                        "|" & strSDate_Trx & _
                        "|" & strEDate_Trx & _
                        "|" & strUserLoc

        Try
            intErrNo = objGLrpt.mtdGetReportDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objRptDsItem, _
                                                objMapPath, objFTPFolder)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ID&errmesg=" & Exp.ToString() & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
        End Try
 
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strFileName & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        rdCrystalViewer.SetDataSource(objRptDsItem.Tables(0))
        'rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
      

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
        Dim paramField3 As New ParameterField()
        Dim paramField4 As New ParameterField()
        Dim paramField5 As New ParameterField()
        'Dim paramField6 As New ParameterField()
        'Dim paramField7 As New ParameterField()
        'Dim paramField8 As New ParameterField()
        'Dim paramField9 As New ParameterField()
        'Dim paramField10 As New ParameterField()
        'Dim paramField11 As New ParameterField()
        'Dim paramField12 As New ParameterField()
        'Dim paramField13 As New ParameterField()
        'Dim paramField14 As New ParameterField()
        'Dim paramField15 As New ParameterField()
        'Dim paramField16 As New ParameterField()
        'Dim paramField17 As New ParameterField()
        'Dim paramField18 As New ParameterField()
        'Dim paramField19 As New ParameterField()
        'Dim paramField20 As New ParameterField()
        'Dim paramField21 As New ParameterField()
        'Dim paramField22 As New ParameterField()
        'Dim paramField23 As New ParameterField()
        'Dim paramField24 As New ParameterField()
        'Dim paramField25 As New ParameterField()
        'Dim paramField26 As New ParameterField()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue3 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue4 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue5 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue6 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue7 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue8 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue9 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue10 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue11 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue12 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue13 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue14 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue15 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue16 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue17 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue18 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue19 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue20 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue21 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue22 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue23 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue24 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue25 As New ParameterDiscreteValue()
        'Dim ParamDiscreteValue26 As New ParameterDiscreteValue()

        Dim crParameterValues1 As ParameterValues
        Dim crParameterValues2 As ParameterValues
        Dim crParameterValues3 As ParameterValues
        Dim crParameterValues4 As ParameterValues
        Dim crParameterValues5 As ParameterValues
      

        Dim crDataDef As DataDefinition
        Dim PFDefs As ParameterFieldDefinitions

        crDataDef = rdCrystalViewer.DataDefinition
        PFDefs = crDataDef.ParameterFields
        paramFields = crvView.ParameterFieldInfo

        paramField1 = paramFields.Item("ParamCompanyName")
        paramField2 = paramFields.Item("ParamLocation")
        paramField3 = paramFields.Item("SDATE_TRX")
        paramField4 = paramFields.Item("EDATE_TRX")
        paramField5 = paramFields.Item("ParamDecimal")


        crParameterValues1 = paramField1.CurrentValues
        crParameterValues2 = paramField2.CurrentValues
        crParameterValues3 = paramField3.CurrentValues
        crParameterValues4 = paramField4.CurrentValues
        crParameterValues5 = paramField5.CurrentValues
      

        ParamDiscreteValue1.Value = Session("SS_COMPANYNAME")
        ParamDiscreteValue2.Value = Session("SS_LOC")
        ParamDiscreteValue3.Value = strSDate_Trx
        ParamDiscreteValue4.Value = strEDate_Trx
        ParamDiscreteValue5.Value = Request.QueryString("Decimal")
        


        crParameterValues1.Add(ParamDiscreteValue1)
        crParameterValues2.Add(ParamDiscreteValue2)
        crParameterValues3.Add(ParamDiscreteValue3)
        crParameterValues4.Add(ParamDiscreteValue4)
        crParameterValues5.Add(ParamDiscreteValue5)
        
        PFDefs(0).ApplyCurrentValues(crParameterValues1)
        PFDefs(1).ApplyCurrentValues(crParameterValues2)
        PFDefs(2).ApplyCurrentValues(crParameterValues3)
        PFDefs(3).ApplyCurrentValues(crParameterValues4)
        PFDefs(4).ApplyCurrentValues(crParameterValues5)
        

        crvView.ParameterFieldInfo = paramFields
    End Sub
End Class
