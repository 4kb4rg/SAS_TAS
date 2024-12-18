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

Public Class GL_StdRpt_LaporanBiayaKebunPreview : Inherits Page
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
        strBy = Trim(Request.QueryString("By"))
        strType = Trim(Request.QueryString("TypeX"))

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
            Case 0 'Biaya Panen
                Select Case strBy
                    Case 0 'all
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Panen"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_PANEN"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation

                    Case 1 'divisi
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Panen_Divisi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_PANEN_DIVISI"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType

                    Case 2 'tahun tanam
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Panen_TahunTanam"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_PANEN_TAHUNTANAM"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType
                End Select
               
            Case 1 'Biaya Pemeliharaan TM
                Select Case strBy
                    Case 0 'all

                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Rawat"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_RAWAT"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation

                    Case 1 'divisi

                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Rawat_Divisi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_RAWAT_DIVISI"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType

                    Case 2 'tahun tanam

                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Rawat_TahunTanam"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_RAWAT_TAHUNTANAM"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType


                End Select
            Case 2 'Biaya Pemeliharaan TBM
                Select Case strBy
                    Case 0 'all
										
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_TBM"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_TBM"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation

                    Case 1 'divisi

                        strRptPrefix = "GL_StdRpt_Estate_Mgr_TBM_Divisi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_TBM_DIVISI"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType

                    Case 2 'tahun tanam

                        strRptPrefix = "GL_StdRpt_Estate_Mgr_TBM_TahunTanam"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_TBM_TAHUNTANAM"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType

                End Select
            Case 3 'Biaya LC
                Select Case strBy
                    Case 0 'all

                        strRptPrefix = "GL_StdRpt_Estate_Mgr_LC"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_LC"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation

                    Case 1 'divisi

                        strRptPrefix = "GL_StdRpt_Estate_Mgr_LC_Divisi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_LC_DIVISI"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType

                    Case 2 'tahun tanam

                        strRptPrefix = "GL_StdRpt_Estate_Mgr_LC_TahunTanam"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_LC_TAHUNTANAM"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType

                End Select
            Case 4 'Biaya Bibitan
                Select Case strBy
                    Case 0 'all

                        strRptPrefix = "GL_StdRpt_Estate_Mgr_NU"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_NU"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation

                    Case 1 'divisi
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_NU_Divisi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_NU_DIVISI"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType

                    Case 2 'tahun tanam
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_NU_TahunTanam"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_NU_TAHUNTANAM"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType


                End Select
            Case 5 'Biaya Umum

                strRptPrefix = "GL_StdRpt_Estate_Mgr_Umum"
                strOpCode = "GL_CLSRPT_ESTATE_MGR_UMUM"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
				
			Case 6'Biaya Karet
                Select Case strBy
                    Case 0 'all
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Karet"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_KARET"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation

                    Case 1 'divisi
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Karet_Divisi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_KARET_DIVISI"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType

                    Case 2 'tahun tanam
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Karet_TahunTanam"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_KARET_TAHUNTANAM"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType


                End Select
            Case 7 'Biaya Karet Pemeliharaan
                Select Case strBy
                    Case 0 'all
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Karet_Rawat"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_KARET_RAWAT"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation

                    Case 1 'divisi
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Karet_Rawat_Divisi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_KARET_RAWAT_DIVISI"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType

                    Case 2 'tahun tanam
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Karet_Rawat_TahunTanam"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_KARET_RAWAT_TAHUNTANAM"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType


                End Select
            Case 8 'Biaya transit kendaraan
                strRptPrefix = "GL_StdRpt_Estate_Mgr_Vehicle"
                strOpCode = "GL_CLSRPT_ESTATE_MGR_VEHICLE"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			 
            Case 9 'Statistik Panen
                Select Case strBy
                    Case 0 'all
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Statistik"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_STATISTIK"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & ""
                    Case 1 'divisi
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Statistik"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_STATISTIK"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType
					Case Else
					    strRptPrefix = ""
                        strOpCode = ""
                        strParamName = ""
                        strParamValue = "" 
                End Select
			Case 10 'Biaya JATI Pemeliharaan
                Select Case strBy
                    Case 0 'all
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Jati_Rawat"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_JATI_RAWAT"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation

                    Case 1 'divisi
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Jati_Rawat_Divisi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_JATI_RAWAT_DIVISI"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType

                    Case 2 'tahun tanam
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Jati_Rawat_TahunTanam"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_JATI_RAWAT_TAHUNTANAM"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType
                End Select
			Case 11 'Statistik Karet
                Select Case strBy
                    Case 0 'all
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Statistik_Karet"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_STATISTIK_KARET"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & ""
                    Case 1 'divisi
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Statistik_Karet"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_STATISTIK_KARET"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType
					Case Else
					    strRptPrefix = ""
                        strOpCode = ""
                        strParamName = ""
                        strParamValue = "" 
                End Select
			Case 12 'Rekapitulasi Biaya Kebun
			     Select Case strBy
					Case 0 'all
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Rekap_Biaya"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_REKAP_BIAYA"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation  
					Case Else
					    strRptPrefix = ""
                        strOpCode = ""
                        strParamName = ""
                        strParamValue = ""
				End Select
			Case 13 'Rekapitulasi Biaya Transit
			     Select Case strBy
					Case 0 'all
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Rekap_Biaya_veh"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_REKAP_BIAYA_VEH"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation  
					Case Else
					    strRptPrefix = ""
                        strOpCode = ""
                        strParamName = ""
                        strParamValue = ""
				End Select	
			Case 14 'Panen & Perawatan
                Select Case strBy
                    Case 0 'all
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Panen_Rawat"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_PANEN_RAWAT"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation

                    Case 1 'divisi
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Panen_Rawat_Divisi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_PANEN_RAWAT_DIVISI"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType

                    Case 2 'tahun tanam
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Panen_Rawat_TTanam"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_PANEN_RAWAT_TAHUNTANAM"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & strType
                End Select
			Case 20 'Transit Payroll
			    Select Case strBy
			        Case 0 'all
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_TransitAlokasi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_JTRANSIT"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation   & "|" & ""
					Case 1 'divisi
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_TransitAlokasi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_JTRANSIT"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation   & "|" & strType 
					Case Else
					    strRptPrefix = ""
                        strOpCode = ""
                        strParamName = ""
                        strParamValue = ""
				End Select	
			Case 21 'Alokasi Payroll
			     Select Case strBy
					Case 0 'all
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_TransitAlokasi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_JALOKASI"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation  & "|" & "" 
					Case 1 'divisi 
						strRptPrefix = "GL_StdRpt_Estate_Mgr_TransitAlokasi"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_JALOKASI"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation   & "|" & strType 
					Case Else
					    strRptPrefix = ""
                        strOpCode = ""
                        strParamName = ""
                        strParamValue = ""
				End Select
			 Case 22 'Realisasi Pemupukan
                strRptPrefix = "GL_StdRpt_Estate_Mgr_Pupuk"
                strOpCode = "GL_CLSRPT_ESTATE_MGR_PUPUK"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			 Case 23 'Realisasi Pemakaian Bahan
                strRptPrefix = "GL_StdRpt_Estate_Mgr_Bahan"
                strOpCode = "GL_CLSRPT_ESTATE_MGR_BAHAN"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			 Case 24 'Areal Statement
                strRptPrefix = "GL_StdRpt_Estate_Mgr_Aresta"
                strOpCode = "GL_CLSRPT_ESTATE_MGR_ARESTA"
                strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			 Case 25 'Rekap Biaya Pemeliharaan TM
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Rawat_Rekap"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_RAWAT_REKAP"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 26 'Rekap Biaya Pemeliharaan TBM
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_TBM_Rekap"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_TBM_REKAP"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 27 'Rekap Biaya Pemeliharaan LC
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_LC_Rekap"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_LC_REKAP"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 28 'Rekap Biaya Pemeliharaan NU
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_NU_Rekap"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_NU_REKAP"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 29 'Rekap Biaya Panen dan Pemeliharaan
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Panen_Rawat_Rekap"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_PANEN_RAWAT_REKAP"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 34 'Rekap Biaya Panen
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Panen_Rekap"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_PANEN_REKAP"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
			Case 30 'Rekap Biaya Umum
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Umum_Rekap"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_UMUM_REKAP"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation
            Case 31 'Rekap Pemakaian bahan
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Bahan"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_BAHAN_REKAP"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation						
			Case 32 'Rekap Pemupukan
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Pupuk"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_PUPUK_REKAP"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation	
			Case 33 'Rekap Statistik
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Statistik_Rekap"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_STATISTIK_REKAP"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|DIVCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation & "|" & ""
          	Case 99 'Summary Biaya
                        strRptPrefix = "GL_StdRpt_Estate_Mgr_Summary_Biaya"
                        strOpCode = "GL_CLSRPT_ESTATE_MGR_SUMMARY_BIAYA"
                        strParamName = "ACCMONTH|ACCYEAR|LOCCODE"
                        strParamValue = strSelAccMonth & "|" & strSelAccYear & "|" & strLocation 			
			
        End Select


    IF rtrim(strRptPrefix) <> ""  
	
        Try
            intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCode, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)
        Catch Exp As Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=LAPORAN_BIAYA_PRODUKSI&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
		
		

        rdCrystalViewer = New ReportDocument()
        rdCrystalViewer.Load(objMapPath & "web\" & strLangCode & "\Reports\Crystal\" & strRptPrefix & ".rpt", OpenReportMethod.OpenReportByTempCopy)
        If strRptType = 8 Then
            objRptDs.Tables(0).TableName = "GL_BIAYA"
            objRptDs.Tables(1).TableName = "GL_PROD"
            rdCrystalViewer.SetDataSource(objRptDs)
        Else
            rdCrystalViewer.SetDataSource(objRptDs.Tables(0))
        End If
		
		select case strRptType 
			case 20,21,24 
		rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperLegal
			case else
        rdCrystalViewer.PrintOptions.PaperOrientation = PaperOrientation.Landscape
        rdCrystalViewer.PrintOptions.PaperSize = PaperSize.PaperA3
		end select
		
        If strRptType <> 8 Then
            PassParam()
        Else
            PassParam2()
        End If
		

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
       
        ParamDiscreteValue1.Value = ucase(strLocationName)
    
        ParamFieldDefs = rdCrystalViewer.DataDefinition.ParameterFields
        ParamFieldDef1 = ParamFieldDefs.Item("strLocName")

        ParameterValues1 = ParamFieldDef1.CurrentValues
        ParameterValues1.Add(ParamDiscreteValue1)
        ParamFieldDef1.ApplyCurrentValues(ParameterValues1)


    End Sub


End Class
