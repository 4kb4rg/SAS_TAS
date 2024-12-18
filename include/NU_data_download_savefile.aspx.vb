Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.FileSystem

Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.NU.clsData

Public Class NU_data_download_savefile : inherits page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objNUData As New agri.NU.clsData()
    Dim objDownloadDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intNUAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_NUACCMONTH")
        strAccYear = Session("SS_NUACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intNUAR = Session("SS_NUAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDataTransfer), intNUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub


    Sub SaveFile()
        Dim objStreamReader As StreamReader

        Dim strNuBatch As String = lcase(Request.QueryString("nubatch"))
        Dim strCullType As String = lcase(Request.QueryString("culltype"))
        Dim strAccCls As String = lcase(Request.QueryString("acccls"))

        Dim strOpCode_NuBatch As String = "NU_CLSDATA_NUBATCH_DOWNLOAD"
        Dim strOpCode_CullType As String = "NU_CLSDATA_CULLTYPE_DOWNLOAD"
        Dim strOpCode_AccCls As String = "NU_CLSDATA_ACCCLS_DOWNLOAD"

        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strXmlFileName As String = objGlobal.mtdGenerateDownloadFile(objGlobal.EnumDataTransferFileType.NU_NurseryReferenceData, False)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NUDATA_GET_FTPPATH&errmesg=&redirect=")
        End Try

        If strNuBatch = "true" Then strParam = strParam & strOpCode_NuBatch & "|" & _
                                    objNUData.mtdGetDownloadTable(objNUData.EnumDownloadType.NUBatch) & Chr(9)
        If strCullType = "true" Then strParam = strParam & strOpCode_CullType & "|" & _
                                    objNUData.mtdGetDownloadTable(objNUData.EnumDownloadType.CullType) & Chr(9)
        If strAccCls = "true" Then strParam = strParam & strOpCode_AccCls & "|" & _
                                    objNUData.mtdGetDownloadTable(objNUData.EnumDownloadType.AccCls) & Chr(9)
    
        If strParam <> "" Then
            strParam = Mid(strParam, 1, Len(strParam) - 1)
            Try
                intErrNo = objNUData.mtdDownloadRef(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objDownloadDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NUDATA_DOWNLOAD_REF&errmesg=&redirect=")
            End Try


            objDownloadDs.WriteXml(strXmlPath, XmlWriteMode.WriteSchema)

            objStreamReader = File.OpenText(strXmlPath)
            strXmlb4Encrypt = objStreamReader.ReadToEnd()
            objStreamReader.Close()

            Try
                intErrNo = objSysCfg.mtdEncryptRef(strXmlb4Encrypt, objXmlEncrypted)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NUDATA_ENCRYPT_REF&errmesg=&redirect=")
            End Try

            Response.Write(objXmlEncrypted)
            Response.ContentType = "application/zip"
            Response.AddHeader("Content-Disposition", _
                                "attachment; filename=""" & strZipFileName & """")
            objDownloadDs = Nothing
        End If
    End Sub

End Class

