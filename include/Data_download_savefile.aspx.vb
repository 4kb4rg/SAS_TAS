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

Public Class Data_download_savefile : Inherits page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objWMData As New agri.WM.clsData()
    Dim objGLTrx As New agri.GL.clsTrx()
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

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBCashBank), intCBAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
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
                strTableName = "GL_MTHENDTRX_TRIAL"
                strXmlFileName = "GL_MTHENDTRX_TRIAL.xml"
                strParamName = "LOCCODE|DATEFROM|DATETO"
                strParamValue = strLocation & "|" & strDateFrom & "|" & strDateTo
                strOpCd = "DOWNLOAD_GL_MTHENDTRX_TRIAL"
            Case "1"
                strTableName = "GL_ACCOUNT"
                strXmlFileName = "GL_ACCOUNT.xml"
                strParamName = "DATEFROM|DATETO"
                strParamValue = strDateFrom & "|" & strDateTo
                strOpCd = "DOWNLOAD_GL_ACCOUNT"
            Case "2"
                strTableName = "PU_SUPPLIER"
                strXmlFileName = "PU_SUPPLIER.xml"
                strParamName = "DATEFROM|DATETO"
                strParamValue = strDateFrom & "|" & strDateTo
                strOpCd = "DOWNLOAD_PU_SUPPLIER"
            Case "3"
                strTableName = "GL_BILLPARTY"
                strXmlFileName = "GL_BILLPARTY.xml"
                strParamName = "DATEFROM|DATETO"
                strParamValue = strDateFrom & "|" & strDateTo
                strOpCd = "DOWNLOAD_GL_BILLPARTY"
            Case "4"
                strTableName = "GL_JOURNAL"
                strXmlFileName = "GL_JOURNALHDR.xml"
                strParamName = "LOCCODE|DATEFROM|DATETO"
                strParamValue = strLocation & "|" & strDateFrom & "|" & strDateTo
                strOpCd = "DOWNLOAD_GL_JOURNALHDR"
            Case "5"
                strTableName = "GL_JRNLN"
                strXmlFileName = "GL_JOURNALDTL.xml"
                strParamName = "LOCCODE|DATEFROM|DATETO"
                strParamValue = strLocation & "|" & strDateFrom & "|" & strDateTo
                strOpCd = "DOWNLOAD_GL_JOURNALDTL"
            Case "6"
                strTableName = "CB_CASHBANK"
                strXmlFileName = "CB_CASHBANKHDR.xml"
                strParamName = "LOCCODE|DATEFROM|DATETO"
                strParamValue = strLocation & "|" & strDateFrom & "|" & strDateTo
                strOpCd = "DOWNLOAD_CB_CASHBANKHDR"
            Case "7"
                strTableName = "CB_CASHBANKLN"
                strXmlFileName = "CB_CASHBANKDTL.xml"
                strParamName = "LOCCODE|DATEFROM|DATETO"
                strParamValue = strLocation & "|" & strDateFrom & "|" & strDateTo
                strOpCd = "DOWNLOAD_CB_CASHBANKDTL"
            Case "8"
                strTableName = "CB_PAYMENT"
                strXmlFileName = "CB_PAYMENTHDR.xml"
                strParamName = "LOCCODE|DATEFROM|DATETO"
                strParamValue = strLocation & "|" & strDateFrom & "|" & strDateTo
                strOpCd = "DOWNLOAD_CB_PAYMENTHDR"
            Case "9"
                strTableName = "CB_PAYMENTLN"
                strXmlFileName = "CB_PAYMENTDTL.xml"
                strParamName = "LOCCODE|DATEFROM|DATETO"
                strParamValue = strLocation & "|" & strDateFrom & "|" & strDateTo
                strOpCd = "DOWNLOAD_CB_PAYMENTDTL"

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

