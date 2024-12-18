
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

Imports agri.PWSystem
Imports agri.Admin
Imports agri.GlobalHdl
Imports agri.GL
Imports agri.PD


Public Class GL_trx_download_savefile : inherits page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmShare As New agri.Admin.clsShare()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLData As New agri.GL.clsData()
    Dim objPDData As New agri.PD.clsData()
    Dim objDownloadDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intGLAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Request.QueryString("m")
        strAccYear = Request.QueryString("y")
        strLangCode = Session("SS_LANGCODE")
        intGLAR = Session("SS_GLAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLDataTransfer), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub

    Sub SaveFile()
        Dim objStreamReader As StreamReader
        Dim strOpCode_MthEnd_DEL As String = "GL_CLSDATA_SHMTHENDTRXTMP_DEL"
        Dim strOpCode_MthEnd_ADD As String = "GL_CLSDATA_SHMTHENDTRXTMP_ADD"
        Dim strOpCode_MthEnd As String = "GL_CLSDATA_SHMTHENDTRX_DOWNLOAD"
        Dim strOpCode_Blk As String = "GL_CLSDATA_BLK_DOWNLOAD"
        Dim strOpCode_SubBlk As String = "GL_CLSDATA_SUBBLK_DOWNLOAD"
        Dim strOpCode_Veh As String = "GL_CLSDATA_VEH_DOWNLOAD"
        Dim strOpCode_VehExp As String = "GL_CLSDATA_VEHEXP_DOWNLOAD"
        Dim strOpCode_VehUsg As String = "GL_CLSDATA_VEHUSAGE_DOWNLOAD"
        Dim strOpCode_VehUsgLn As String = "GL_CLSDATA_VEHUSAGE_LINE_DOWNLOAD"
        Dim strOpCode_Prod As String = "GL_CLSDATA_OILPALM_DOWNLOAD"
        Dim strOpCode_ As String
        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strXmlFileName As String = objGlobal.mtdGenerateDownloadFile(objGlobal.EnumDataTransferFileType.GL_GeneralLedgerTransaction, False)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GLDATA_TRX_GET_FTPPATH&errmesg=&redirect=")
        End Try

        Try
            strParam = strParam & strOpCode_MthEnd & "|" & objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.SHMthEndTrxTmp) & Chr(9) & _
                                  strOpCode_Blk & "|" & objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.Block) & Chr(9) & _
                                  strOpCode_SubBlk & "|" & objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.SubBlock) & Chr(9) & _
                                  strOpCode_Veh & "|" & objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.Vehicle) & Chr(9) & _
                                  strOpCode_VehUsg & "|" & objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.VehUsage) & Chr(9) & _
                                  strOpCode_VehUsgLn & "|" & objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.VehUsageLn) & Chr(9) & _
                                  strOpCode_Prod & "|" & objPDData.mtdGetDownloadTable(objPDData.EnumDownloadType.PDEstYield)
            intErrNo = objGLData.mtdDownloadGLTrx(strOpCode_MthEnd_DEL, _
                                                  strOpCode_MthEnd_ADD, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strAccMonth, _
                                                  strAccYear, _
                                                  strParam, _
                                                  objDownloadDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GLDATA_TRX_DOWNLOAD_REF&errmesg=&redirect=")
        End Try

        objDownloadDs.WriteXml(strXmlPath, XmlWriteMode.WriteSchema)

        objStreamReader = File.OpenText(strXmlPath)
        strXmlb4Encrypt = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        Try
            intErrNo = objSysCfg.mtdEncryptRef(strXmlb4Encrypt, objXmlEncrypted)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GLDATA_TRX_ENCRYPT_REF&errmesg=&redirect=")
        End Try

        Response.Write(objXmlEncrypted)
        Response.ContentType = "application/zip"
        Response.AddHeader("Content-Disposition", _
                            "attachment; filename=""" & strZipFileName & """")
    End Sub


End Class

