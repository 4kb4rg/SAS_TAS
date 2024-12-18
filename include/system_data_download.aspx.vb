Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.Page
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.FileSystem
Imports Microsoft.VisualBasic
Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights

Public Class system_data_download : Inherits Page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objDownloadDs As New DataSet()
    Dim strUserId As String
    Dim strLangCode As String
    Dim intADAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADSystemConfig), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetData()
            End If
        End If
    End Sub

    Sub onload_GetData()
        Dim objStreamReader As StreamReader
        Dim strOpCode_SysCfg As String = "PWSYSTEM_CLSCONFIG_ACCLENGTH_DOWNLOAD"

        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strXmlFileName As String = objGlobal.mtdGenerateDownloadFile(objGlobal.EnumDataTransferFileType.SC_SystemConfigurationData, False)
        Dim strZipFileName As String = Mid(strXmlFileName, 1, Len(strXmlFileName) - 3) & "zip"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intFreeFile As Integer
        Dim strXmlb4Encrypt As String = ""
        Dim ObjXmlEncrypted As New Object()
        Dim ObjXmlDecrypted As New Object()

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
            strXmlPath = strFtpPath & strXmlFileName
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSCFG_GET_FTPPATH&errmesg=&redirect=")
        End Try

        strParam = strOpCode_SysCfg & "|" & _
                   objSysCfg.mtdGetDownloadTable(objSysCfg.EnumDownloadType.AccCodeLength) & Chr(9)
        strParam = strParam & strOpCode_SysCfg & "|" & _
                   objSysCfg.mtdGetDownloadTable(objSysCfg.EnumDownloadType.AccCodeLength)

        If strParam <> "" Then
            Try
                intErrNo = objSysCfg.mtdDownloadRef(strParam, _
                                                    objDownloadDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INDATA_DOWNLOAD_REF&errmesg=&redirect=")
            End Try

            objDownloadDs.WriteXml(strXmlPath, XmlWriteMode.WriteSchema)

            objStreamReader = File.OpenText(strXmlPath)
            strXmlb4Encrypt = objStreamReader.ReadToEnd()
            objStreamReader.Close()

            Try
                intErrNo = objSysCfg.mtdEncryptRef(strXmlb4Encrypt, objXmlEncrypted)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INDATA_ENCRYPT_REF&errmesg=&redirect=")
            End Try

            Response.Write(objXmlEncrypted)
            Response.ContentType = "application/zip"
            Response.AddHeader("Content-Disposition", _
                                "attachment; filename=""" & strZipFileName & """")
        End If
    End Sub



End Class
