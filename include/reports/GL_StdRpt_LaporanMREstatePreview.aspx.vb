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
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Imports agri.PWSystem.clsLangCap

Public Class GL_StdRpt_LaporanMREstatePreview : Inherits Page
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLRpt As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents crvView As CrystalDecisions.Web.CrystalReportViewer
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents EventData As DataGrid

    Dim rdCrystalViewer As ReportDocument
    Dim objLangCapDs As New Object()
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
    Dim strRptType As String
    Dim strBy As String
    Dim strType As String
    Dim strExportToExcel As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        crvView.Visible = False
        Dim tempLoc As String
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


        strRptId = Trim(Request.QueryString("RptId"))
        strRptName = Trim(Request.QueryString("RptName"))
        strSelAccMonth = Request.QueryString("DDLAccMth")
        strSelAccYear = Request.QueryString("DDLAccYr")
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strPrintedBy = Session("SS_USERNAME")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        intSelDecimal = CInt(Request.QueryString("Decimal"))
        strRptType = Trim(Request.QueryString("StrRptType"))
        strExportToExcel = Trim(Request.QueryString("ExportToExcel"))

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            BindReport()
        End If
    End Sub

    Sub BindReport()
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim strOpCode As String
        Dim objRptDs As New DataSet()
        Dim objMapPath As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strRptPrefix As String
        Dim intCnt As Integer
        Dim objFTPFolder As String
        

        Select Case strRptType
            Case 0 'Areal Statement I.1 Hektar Staement
                strRptPrefix = "GL_ESTATE_MR_I1_ARESTA_TEST"
                strOpCode = "GL_ESTATE_MR_I1_ARESTA_CT"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 1 'Areal Statement 1.2 Jumlah Pokok
                strRptPrefix = "GL_ESTATE_MR_I2_ARESTA"
                strOpCode = "GL_ESTATE_MR_I2_ARESTA"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 2 'II.Personalia & Penduduk - II.1 Komposisi Tenaga Kerja
                strRptPrefix = "GL_ESTATE_MR_II1_PERSONALIA"
                strOpCode = "GL_ESTATE_MR_II1_PERSONALIA"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 3 'II.Personalia & Penduduk - II.2 Mutasi Tanaga Kerja
                strRptPrefix = "GL_ESTATE_MR_II2_PERSONALIA"
                strOpCode = "GL_ESTATE_MR_II2_PERSONALIA"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 4 'II.Personalia & Penduduk - II.3 Hari Kerja Efektif
                strRptPrefix = "GL_ESTATE_MR_II3_PERSONALIA"
                strOpCode = "GL_ESTATE_MR_II3_PERSONALIA"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 5 'III.Data Curah Hujan - III.1 Harian
                strRptPrefix = "GL_ESTATE_MR_III1_CHUJAN"
                strOpCode = "GL_ESTATE_MR_III1_CHUJAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 6 'III.Data Curah Hujan - III.2 PerBulan Dalam 5 Tahun Terakhir
                strRptPrefix = "GL_ESTATE_MR_III2_CHUJAN"
                strOpCode = "GL_ESTATE_MR_III2_CHUJAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 7 'IV.Produksi TBS - IV.1.a Statistik Produksi PerBlok
                strRptPrefix = "GL_ESTATE_MR_IV1A_PRODUKSI"
                strOpCode = "GL_ESTATE_MR_IV1A_PRODUKSI"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 8 'IV.Produksi TBS - IV.1.b Statistik Produksi Yield
                strRptPrefix = "GL_ESTATE_MR_IV1B_PRODUKSI"
                strOpCode = "GL_ESTATE_MR_IV1B_PRODUKSI"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 9 'IV.Produksi TBS - IV.1.c Statistik Produksi Harian Panen PerBlok
                strRptPrefix = "GL_ESTATE_MR_IV1C_PRODUKSI"
                strOpCode = "GL_ESTATE_MR_IV1C_PRODUKSI"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 10 'IV.Produksi TBS - IV.2.a Analisa Output Tenaga Kerja Pemanen
                strRptPrefix = "GL_ESTATE_MR_IV2A_PRODUKSI"
                strOpCode = "GL_ESTATE_MR_IV2A_PRODUKSI"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 11 'IV.Produksi TBS - IV.2.b Analisa Output Tenaga Kerja Pembrondol
                strRptPrefix = "GL_ESTATE_MR_IV2B_PRODUKSI"
                strOpCode = "GL_ESTATE_MR_IV2B_PRODUKSI"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 12 'IV.Produksi TBS - IV.3 Laporan Restan
                strRptPrefix = "GL_ESTATE_MR_IV3_PRODUKSI"
                strOpCode = "GL_ESTATE_MR_IV3_PRODUKSI"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 13 'IV.Produksi TBS - IV.4 Laporan Pengiriman TBS
                strRptPrefix = "GL_ESTATE_MR_IV4_PRODUKSI"
                strOpCode = "GL_ESTATE_MR_IV4_PRODUKSI"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 14 'IV.Produksi TBS - IV.5.a Biaya Panen
                strRptPrefix = "GL_ESTATE_MR_IV5A_PRODUKSI"
                strOpCode = "GL_ESTATE_MR_IV5A_PRODUKSI"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 15 'IV.Produksi TBS - IV.5.b Biaya Transport Buah
                strRptPrefix = "GL_ESTATE_MR_IV5B_PRODUKSI"
                strOpCode = "GL_ESTATE_MR_IV5B_PRODUKSI"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 16 'V.Pemeliharaan Tanaman - V.1.a Rekap Pemeliharaan Tanaman TM
                strRptPrefix = "GL_ESTATE_MR_V1A_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V1A_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 17 'V.Pemeliharaan Tanaman - V.1.b Rekap Pemeliharaan Tanaman TBM3
                strRptPrefix = "GL_ESTATE_MR_V1B_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V1B_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 18 'V.Pemeliharaan Tanaman - V.1.c Rekap Pemeliharaan Tanaman TBM2
                strRptPrefix = "GL_ESTATE_MR_V1C_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V1C_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 19 'V.Pemeliharaan Tanaman - V.1.d Rekap Pemeliharaan Tanaman TBM1
                strRptPrefix = "GL_ESTATE_MR_V1D_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V1D_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 20 'V.Pemeliharaan Tanaman - V.2.a Rencana & Realisasi Pemupukan PerBlok TM
                strRptPrefix = "GL_ESTATE_MR_V2A_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V2A_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 21 'V.Pemeliharaan Tanaman - V.2.b Rencana & Realisasi Pemupukan PerBlok TBM3
                strRptPrefix = "GL_ESTATE_MR_V2B_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V2B_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 22 'V.Pemeliharaan Tanaman - V.2.c Rencana & Realisasi Pemupukan PerBlok TBM2
                strRptPrefix = "GL_ESTATE_MR_V2C_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V2C_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 23 'V.Pemeliharaan Tanaman - V.2.d Rencana & Realisasi Pemupukan PerBlok TBM1
                strRptPrefix = "GL_ESTATE_MR_V2D_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V2D_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 24 'V.Pemeliharaan Tanaman - V.3.a Rakap Aplikasi Pemupukan TM
                strRptPrefix = "GL_ESTATE_MR_V3A_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V3A_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 25 'V.Pemeliharaan Tanaman - V.3.b Rakap Aplikasi Pemupukan TBM
                strRptPrefix = "GL_ESTATE_MR_V3B_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V3B_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 26 'V.Pemeliharaan Tanaman - V.4.a Rakap Aplikasi Pemupukan Extra TM
                strRptPrefix = "GL_ESTATE_MR_V4A_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V4A_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 27 'V.Pemeliharaan Tanaman - V.4.b Rakap Aplikasi Pemupukan Extra TBM
                strRptPrefix = "GL_ESTATE_MR_V4B_PEMELIHARAAN"
                strOpCode = "GL_ESTATE_MR_V4B_PEMELIHARAAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 28 'VI.LC dan Tanam - VI.1 Laporan Kemajuan LC dan Tanam Baru
                strRptPrefix = "GL_ESTATE_MR_VI1_LC"
                strOpCode = "GL_ESTATE_MR_VI1_LC"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 29 'VI.LC dan Tanam - VI.2 Laporan Thining OUT dan Tanam Sisip
                strRptPrefix = "GL_ESTATE_MR_VI2_LC"
                strOpCode = "GL_ESTATE_MR_VI2_LC"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 30 'VII.Bibitan - VII.1 Persediaan Bibit
				strRptPrefix = "GL_ESTATE_MR_VII1_BIBIT"
                strOpCode = "GL_ESTATE_MR_VII1_BIBIT"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
				
			Case 31 'VII.Bibitan - VII.2 Seleksi Bibit
                strRptPrefix = "GL_ESTATE_MR_VII2_BIBIT"
                strOpCode = "GL_ESTATE_MR_VII2_BIBIT"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 32 'VIII.Traksi & Transport - VIII.1 Utility Unit
                strRptPrefix = "GL_ESTATE_MR_VIII1_TRAKSI"
                strOpCode = "GL_ESTATE_MR_VIII1_TRAKSI"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 33 'VIII.Traksi & Transport - VIII.2 Hari Efektif Unit
                strRptPrefix = "GL_ESTATE_MR_VIII2_TRAKSI"
                strOpCode = "GL_ESTATE_MR_VIII2_TRAKSI"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 34 'IX.SPK
                strRptPrefix = "GL_ESTATE_MR_IX_SPK"
                strOpCode = "GL_ESTATE_MR_IX_SPK"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 35 'X.Gudang - X.1 Stock Pupuk dan AgroChemical
                strRptPrefix = "GL_ESTATE_MR_X1_STOCK"
                strOpCode = "GL_ESTATE_MR_X1_STOCK"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 36 'X.Gudang - X.2 Distribusi Pupuk dan AgroChemical
                strRptPrefix = "GL_ESTATE_MR_X2_STOCK"
                strOpCode = "GL_ESTATE_MR_X2_STOCK"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 37 'X.Gudang - X.3 Rincian Persediaan
                strRptPrefix = "GL_ESTATE_MR_X3_STOCK"
                strOpCode = "GL_ESTATE_MR_X3_STOCK"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 38 'Areal Statement
                strRptPrefix = "GL_StdRpt_Estate_Mgr_Aresta"
                strOpCode = "GL_CLSRPT_ESTATE_MGR_ARESTA"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation	
			Case 39 'Rekap Statistik
                strRptPrefix = "GL_StdRpt_Estate_Mgr_Statistik_Rekap"
                strOpCode = "GL_CLSRPT_ESTATE_MGR_STATISTIK_REKAP"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & ""
			Case 40 'Detail Statistik
                 strRptPrefix = "GL_StdRpt_Estate_Mgr_Statistik"
                 strOpCode = "GL_CLSRPT_ESTATE_MGR_STATISTIK"
                 strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                 strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & ""
			Case 41 'Areal Statement
                strRptPrefix = "GL_StdRpt_Estate_Mgr_Aresta_Summary"
                strOpCode = "GL_CLSRPT_ESTATE_MGR_ARESTA"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation					 
        End Select


    IF rtrim(strRptPrefix) <> ""  
	
        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCode, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=LAPORAN_MR&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
		
		

        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
'        If strRptType = 0 Then
'            objRptDs.Tables(0).TableName = "GL_StdRpt_AreaStatement"
'            objRptDs.Tables(1).TableName = "GL_StdRpt_BlkGrpDesc"
'            objRptDs.Tables(2).TableName = "GL_StdRpt_BlkGrpDetails"
'            rdCrystalViewer.SetDataSource(objRptDs)
'        Else
            rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
'        End If
		
            PassParam()
  
		

        crDiskFileDestinationOptions = New DiskFileDestinationOptions()
        If strExportToExcel = "0" Then
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".pdf"
        Else
            crDiskFileDestinationOptions.DiskFileName = objFTPFolder & strRptPrefix & ".xls"
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
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".pdf"">")
        Else
            Response.Write("<META HTTP-EQUIV=""refresh"" CONTENT=""0; URL=../../" & strUrl & strRptPrefix & ".xls"">")
        End If
    Else
		Response.Write("Report Not Available")
	End If 

    End Sub

    Sub PassParam()
        Dim ParamFieldDefs As ParameterFieldDefinitions

        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParamFieldDef2 As ParameterFieldDefinition
		Dim ParamFieldDef3 As ParameterFieldDefinition
		
        Dim ParameterValues1 As New ParameterValues()
        Dim ParameterValues2 As New ParameterValues()
		Dim ParameterValues3 As New ParameterValues()

        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
        Dim ParamDiscreteValue2 As New ParameterDiscreteValue()
		Dim ParamDiscreteValue3 As New ParameterDiscreteValue()

        ParamDiscreteValue1.Value = UCase(strLocationName)
        ParamDiscreteValue2.Value = UCase(strType)
		ParamDiscreteValue3.Value = UCase(strCompanyName)
		

        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields

        ParamFieldDef1 = ParamFieldDefs.Item("strLocName")
        ParamFieldDef2 = ParamFieldDefs.Item("strGroupName")
		ParamFieldDef3 = ParamFieldDefs.Item("strCompName")
		

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues2 = ParamFieldDef2.CurrentValues
		ParameterValues3 = ParamFieldDef3.CurrentValues

        ParameterValues1.Add(ParamDiscreteValue1)
        ParameterValues2.Add(ParamDiscreteValue2)
		ParameterValues3.Add(ParamDiscreteValue3)
		

        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)
        ParamFieldDef2.ApplyCurrentValues(ParameterValues2)
		ParamFieldDef3.ApplyCurrentValues(ParameterValues3)

    End Sub
	
    Sub PassParam2()

        Dim ParamFieldDefs As ParameterFieldDefinitions
        Dim ParamFieldDef1 As ParameterFieldDefinition
        Dim ParameterValues1 As New ParameterValues()
        Dim ParamDiscreteValue1 As New ParameterDiscreteValue()
       
        ParamDiscreteValue1.Value = intSelDecimal
    
        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("SelDecimal")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues1.Add(ParamDiscreteValue1)
        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)


    End Sub


End Class
