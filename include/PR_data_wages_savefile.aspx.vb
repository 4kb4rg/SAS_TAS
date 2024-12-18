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
Imports agri.Admin.clsShare
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PR.clsData


Public Class PR_data_wages_savefile : Inherits Page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmShare As New agri.Admin.clsShare()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objPRData As New agri.PR.clsData()
    Dim objDownloadDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Sub Page_Load(Sender As Object, E As EventArgs)
        Dim intAccMonth As Integer
        Dim intAccYear As Integer

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Request.QueryString("accmonth")
        strAccYear = Request.QueryString("accyear")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadWages), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub

    Sub SaveFile()
        Dim objStreamReader As StreamReader
        Dim strOpCode As String = "PR_CLSDATA_WAGES_DOWNLOAD"
        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strXmlFileName As String = objGlobal.mtdGenerateDownloadFile(objGlobal.EnumDataTransferFileType.PR_PayrollWagesData, False)
        Dim strZipFileName As String = Mid(strXmlFileName, 1, Len(strXmlFileName) - 3) & "zip"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intPosted As Integer
        Dim strXmlb4Encrypt As String = ""
        Dim ObjXmlEncrypted As New Object()
        Dim ObjXmlDecrypted As New Object()

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
            strXmlPath = strFtpPath & strXmlFileName
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_WAGES_GET_FTPPATH&errmesg=&redirect=")
        End Try

        Try
            strParam = strParam & strOpCode & "|" & objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.Wages)
            intErrNo = objPRData.mtdDownloadRef(strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                objDownloadDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_WAGES_DOWNLOAD_REF&errmesg=&redirect=")
        End Try

        objDownloadDs.WriteXml(strXmlPath, XmlWriteMode.WriteSchema)

        objStreamReader = File.OpenText(strXmlPath)
        strXmlb4Encrypt = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        Try
            intErrNo = objSysCfg.mtdEncryptRef(strXmlb4Encrypt, objXmlEncrypted)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_WAGES_ENCRYPT_REF&errmesg=&redirect=")
        End Try

        Response.Write(objXmlEncrypted)
        Response.ContentType = "application/zip"
        Response.AddHeader("Content-Disposition", _
                            "attachment; filename=""" & strZipFileName & """")
    End Sub

End Class

