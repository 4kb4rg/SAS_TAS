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

Public Class WM_data_download_savefile : inherits page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objWMData As New agri.WM.clsData()
    Dim objDownloadDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intWMAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intWMAR = Session("SS_WMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMDataTransfer), intWMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub


    Sub SaveFile()
        Dim objStreamReader As StreamReader
        Dim strTransporter As String = lcase(Request.QueryString("transporter"))
        Dim strOpCode_Item As String = "WM_CLSDATA_TRANSPORTER_DOWNLOAD"
        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strXmlFileName As String = objGlobal.mtdGenerateDownloadFile(objGlobal.EnumDataTransferFileType.WM_WeighingReferenceData, False)
        Dim strZipFileName As String = Mid(strXmlFileName, 1, Len(strXmlFileName) - 3) & "zip"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strXmlb4Encrypt As String = ""
        Dim ObjXmlEncrypted As New Object()
        Dim ObjXmlDecrypted As New Object()

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
            strXmlPath = strFtpPath & strXmlFileName
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_GET_FTPPATH&errmesg=&redirect=")
        End Try

        If strTransporter = "true" Then strParam = strParam & strOpCode_Item & "|" & _
                                    objWMData.mtdGetDownloadTable(objWMData.EnumDownloadType.Transporter) & Chr(9)

        If strParam <> "" Then
            strParam = Mid(strParam, 1, Len(strParam) - 1)
            Try
                intErrNo = objWMData.mtdDownloadRef(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objDownloadDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WMDATA_DOWNLOAD_REF&errmesg=&redirect=")
            End Try

            objDownloadDs.WriteXml(strXmlPath, XmlWriteMode.WriteSchema)

            objStreamReader = File.OpenText(strXmlPath)
            strXmlb4Encrypt = objStreamReader.ReadToEnd()
            objStreamReader.Close()

            Try
                intErrNo = objSysCfg.mtdEncryptRef(strXmlb4Encrypt, objXmlEncrypted)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WMDATA_ENCRYPT_REF&errmesg=&redirect=")
            End Try

            Response.Write(objXmlEncrypted)
            Response.ContentType = "application/zip"
            Response.AddHeader("Content-Disposition", _
                                "attachment; filename=""" & strZipFileName & """")
        End If
    End Sub



End Class

