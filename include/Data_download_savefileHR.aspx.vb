Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.FileSystem
Imports Microsoft.VisualBasic
Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.WM.clsData

Public Class Data_download_savefileHR : Inherits Page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objWMData As New agri.WM.clsData()
    Dim objGLTrx As New agri.GL.ClsTrx()
    Dim dsResult As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intCBAR As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intCBAR = Session("SS_CBAR")

        'If strUserId = "" Then
        '    Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDataTransfer), intCBAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        'Else
        SaveFile()
        'End If
    End Sub

    Sub SaveFile()

        Dim objStreamReader As StreamReader
        Dim strTable As String = Trim(Request.QueryString("Table"))
        Dim strDateFrom As String = Trim(Request.QueryString("DateFrom"))
        Dim strDateTo As String = Trim(Request.QueryString("DateTo"))
        Dim strOpCd As String = ""

        Dim strFtpPath As String
        Dim strXmlPath As String = ""

        Dim strXmlFileName As String = ""
        Dim strTableName As String = ""

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        Dim intErrNo As Integer
        Dim strXmlb4Encrypt As String = ""
        Dim ObjXmlEncrypted As New Object()
        Dim ObjXmlDecrypted As New Object()

        Select Case strTable
            Case "0"
                strTableName = "HR_STP_EMPTYPE"
                strXmlFileName = "HR_STP_EMPTYPE.xml"
                strParamName = ""
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_STP_EMPTYPE "
            Case "1"
                strTableName = "HR_STP_EMPTYPE"
                strXmlFileName = "HR_STP_EMPTYPE.xml"
                strParamName = ""
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_STP_EMPTYPE "
            Case "2"
                strTableName = "HR_STP_JABATAN"
                strXmlFileName = "HR_STP_JABATAN.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_STP_JABATAN "
            Case "3"
                strTableName = "HR_STP_YEARPLAN"
                strXmlFileName = "HR_STP_YEARPLAN.xml"
                strParamName = ""
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_STP_YEARPLAN "
            Case "4"
                strTableName = "hr_trx_employee"
                strXmlFileName = "hr_trx_employee.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPLOYEE "
            Case "5"
                strTableName = "HR_TRX_EMPFOTO"
                strXmlFileName = "HR_TRX_EMPFOTO.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPFOTO "
            Case "6"
                strTableName = "HR_TRX_EMPADDRESS"
                strXmlFileName = "HR_TRX_EMPADDRESS.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPADDRESS "
            Case "7"
                strTableName = "HR_TRX_EMPFAMILY"
                strXmlFileName = "HR_TRX_EMPFAMILY.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPFAMILY "
            Case "8"
                strTableName = "HR_TRX_EMPPYROL"
                strXmlFileName = "HR_TRX_EMPPYROL.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPPYROL "
            Case "9"
                strTableName = "HR_TRX_EMPWORK_HIST"
                strXmlFileName = "HR_TRX_EMPWORK_HIST.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPWORK_HIST "
            Case "10"
                strTableName = "HR_TRX_EMPPRODEMOSI "
                strXmlFileName = "HR_TRX_EMPPRODEMOSI.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPPRODEMOSI "
            Case "11"
                strTableName = "HR_TRX_EMPMUTASI "
                strXmlFileName = "HR_TRX_EMPMUTASI .xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPMUTASI "
            Case "12"
                strTableName = "HR_TRX_EMPPYROL_HIST"
                strXmlFileName = "HR_TRX_EMPPYROL_HIST.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPPYROL_HIST "
            Case "13"
                strTableName = "HR_TRX_EMPMEDICALREC"
                strXmlFileName = "HR_TRX_EMPMEDICALREC.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPMEDICALREC "
            Case "14"
                strTableName = "HR_TRX_EMPSTUDY_HIST"
                strXmlFileName = "HR_TRX_EMPSTUDY_HIST.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPSTUDY_HIST "
            Case "15"
                strTableName = "HR_TRX_EMPWSHOP_HIST"
                strXmlFileName = "HR_TRX_EMPWSHOP_HIST.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPWSHOP_HIST "
            Case "16"
                ''MANDOR
                strTableName = "HR_TRX_EMPMANDOR"
                strXmlFileName = "HR_TRX_EMPMANDOR.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPMANDOR"
            Case "17"
                ''MANDOR LN
                strTableName = "HR_TRX_EMPMANDORLN"
                strXmlFileName = "HR_TRX_EMPMANDORLN.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPMANDORLN"
            Case "18"
                strTableName = "HR_TRX_EMPRESIGN"
                strXmlFileName = "HR_TRX_EMPRESIGN.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_HR_TRX_EMPRESIGN "
            Case "19"
                ''DIVISI
                strTableName = "GL_BLKGRP"
                strXmlFileName = "GL_BLKGRP.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_GL_BLKGRP"
            Case "20"
                ''TAHUN TANAMss
                strTableName = "GL_BLOCK"
                strXmlFileName = "GL_BLOCK.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_GL_BLOCK"
            Case "21"
                ''BLOK
                strTableName = "GL_SUBBLK"
                strXmlFileName = "GL_SUBBLK.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_GL_SUBBLK"
            Case "22"
                ''BJR
                strTableName = "PR_STP_BJR_BLOCK"
                strXmlFileName = "PR_STP_BJR_BLOCK.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_BJR_BLOCK"
            Case "23"
                ''DAFTAR GOL SKU-BULANAN
                strTableName = "PR_STP_EMPGOL"
                strXmlFileName = "PR_STP_EMPGOL.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_EMPGOL"
            Case "24"
                ''STATUS GOL KARYAWAN,UMP
                strTableName = "PR_STP_EMPSALARY"
                strXmlFileName = "PR_STP_EMPSALARY.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_EMPSALARY"
            Case "25"
                ''KODE TANGGUNGAN
                strTableName = "PR_STP_TANGGUNGAN"
                strXmlFileName = "PR_STP_TANGGUNGAN.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_TANGGUNGAN"
            Case "26"
                ''HARGA BERAS
                strTableName = "PR_STP_BERASRATE"
                strXmlFileName = "PR_STP_BERASRATE.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_BERASRATE"
            Case "27"
                ''PREMI BERAS
                strTableName = "PR_STP_PREMIBERAS"
                strXmlFileName = "PR_STP_PREMIBERAS.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_PREMIBERAS"
            Case "28"
                ''ASTEK
                strTableName = "PR_STP_ASTEK"
                strXmlFileName = "PR_STP_ASTEK.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_ASTEK"
            Case "29"
                ''CUT OF SETTING
                strTableName = "PR_STP_CUTOFF"
                strXmlFileName = "PR_STP_CUTOFF.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_CUTOFF"
            Case "30"
                ''CUT OF SETTING LN
                strTableName = "PR_STP_CUTOFFLN"
                strXmlFileName = "PR_STP_CUTOFFLN.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_CUTOFFLN"
            Case "31"
                ''KODE ABSENSI
                strTableName = "PR_STP_ATTCODE"
                strXmlFileName = "PR_STP_ATTCODE.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_ATTCODE"
            Case "32"
                ''SETTING KODE ABSENSI
                strTableName = "PR_STP_ATTID"
                strXmlFileName = "PR_STP_ATTID.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_ATTID"
            Case "33"
                ''HARI LIBUR
                strTableName = "PR_STP_HOLIDAY"
                strXmlFileName = "PR_STP_HOLIDAY.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_HOLIDAY"
            Case "34"
                ''TARIF LEMBUR
                strTableName = "PR_STP_OVERTIME"
                strXmlFileName = "PR_STP_OVERTIME.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_OVERTIME"
            Case "35"
                ''TABEL BASIS PREMI & PANEN
                strTableName = "PR_STP_PREMI_GOLONGAN"
                strXmlFileName = "PR_STP_PREMI_GOLONGAN.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_PREMI_GOLONGAN"
            Case "36"
                ''DENDA PANEN
                strTableName = "PR_STP_DENDA"
                strXmlFileName = "PR_STP_DENDA.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_DENDA"
            Case "37"
                ''PREMI KERAJINAN
                strTableName = "PR_STP_PREMI_KERAJINAN"
                strXmlFileName = "PR_STP_PREMI_KERAJINAN.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_PREMI_KERAJINAN"
            Case "38"
                ''PREMI SUPIR
                strTableName = "PR_STP_PREMI_DRIVER"
                strXmlFileName = "PR_STP_PREMI_DRIVER.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_PREMI_DRIVER"
            Case "39"
                ''PREMI MANDOR
                strTableName = "PR_STP_PREMI_MANDOR"
                strXmlFileName = "PR_STP_PREMI_MANDOR.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_PREMI_MANDOR"
            Case "40"
                ''PREMI MANDOR LN
                strTableName = "PR_STP_PREMI_MANDORLN"
                strXmlFileName = "PR_STP_PREMI_MANDORLN.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_PREMI_MANDORLN"
            Case "41"
                ''PREMI DERES
                strTableName = "PR_STP_PREMI_KARET"
                strXmlFileName = "PR_STP_PREMI_KARET.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_PREMI_KARET"
            Case "42"
                ''JENIS KENDARAAN
                strTableName = "PR_STP_KENDARAAN"
                strXmlFileName = "PR_STP_KENDARAAN.xml"
                strParamName = ""
                strParamValue = "LOCCODE"
                strOpCd = "DOWNLOAD_PR_STP_KENDARAAN"
            Case "43"
                ''KATEGORI AKTIVITI
                strTableName = "PR_STP_BKCATEGORY"
                strXmlFileName = "PR_STP_BKCATEGORY.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_BKCATEGORY"
            Case "44"
                ''SUB KATEGORI AKTIVITI
                strTableName = "PR_STP_BKSUBSUBCATEGORY"
                strXmlFileName = "PR_STP_BKSUBSUBCATEGORY.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_BKSUBSUBCATEGORY"
            Case "45"
                ''AKTIVITI
                strTableName = "PR_STP_BKJOB"
                strXmlFileName = "PR_STP_BKJOB.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_BKJOB"
            Case "46"
                ''BORONGAN
                strTableName = "PR_STP_BKBORONGAN"
                strXmlFileName = "PR_STP_BKBORONGAN.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_BKBORONGAN"
            Case "47"
                ''KOMPONEN GAJI
                strTableName = "PR_STP_MTHENDAC"
                strXmlFileName = "PR_STP_MTHENDAC.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_MTHENDACC"
            Case "48"
                ''PPH 21
                strTableName = "PR_STP_PPH21"
                strXmlFileName = "PR_STP_PPH21.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_PPH21"
            Case "49"
                ''PPH 21 LN
                strTableName = "PR_STP_PPH21LN"
                strXmlFileName = "PR_STP_PPH21LN.xml"
                strParamName = "LOCCODE"
                strParamValue = strLocation
                strOpCd = "DOWNLOAD_PR_STP_PPH21LN"
        End Select

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
            strXmlPath = strFtpPath & strXmlFileName
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_GET_FTPPATH&errmesg=&redirect=")
        End Try

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                 strParamName, _
                                                 strParamValue, _
                                                 dsResult)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=DATA_DOWNLOAD_REF&errmesg=&redirect=")
        End Try

        dsResult.Tables(0).TableName = strTableName
        dsResult.WriteXml(strXmlPath, XmlWriteMode.WriteSchema)

        Dim file As System.IO.FileInfo = New System.IO.FileInfo(strXmlPath) '-- if the file exists on the server  
        If file.Exists Then 'set appropriate headers  
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(file.FullName)
            Response.End() 'if file does not exist  
        Else
            Response.Write("This file does not exist.")
        End If 'nothing in the URL as HTTP GET  

    End Sub
End Class

