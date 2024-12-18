
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

Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.BI.clsData


Public Class GL_data_download_savefile : inherits page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLData As New agri.GL.clsData()
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
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
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
        Dim strAccClsGrp As String = lcase(Request.QueryString("accclsgrp"))
        Dim strAccCls As String = lcase(Request.QueryString("acccls"))
        Dim strActGrp As String = lcase(Request.QueryString("actgrp"))
        Dim strAct As String = lcase(Request.QueryString("act"))
        Dim strSubAct As String = lcase(Request.QueryString("subact"))
        Dim strExp As String = lcase(Request.QueryString("exp"))
        Dim strVehExpGrp As String = lcase(Request.QueryString("vehexpgrp"))
        Dim strVehExp As String = lcase(Request.QueryString("vehexp"))
        Dim strVehType As String = lcase(Request.QueryString("vehtype"))
        Dim strVeh As String = lcase(Request.QueryString("veh"))
        Dim strBlkGrp As String = lcase(Request.QueryString("blkgrp"))
        Dim strBlk As String = lcase(Request.QueryString("blk"))
        Dim strSubBlk As String = lcase(Request.QueryString("subblk"))
        Dim strAccGrp As String = lcase(Request.QueryString("accgrp"))
        Dim strAcc As String = lcase(Request.QueryString("acc"))

        Dim strOpCode_AccClsGrp As String = "GL_CLSDATA_ACCCLSGRP_DOWNLOAD"
        Dim strOpCode_AccCls As String = "GL_CLSDATA_ACCCLS_DOWNLOAD"
        Dim strOpCode_ActGrp As String = "GL_CLSDATA_ACTGRP_DOWNLOAD"
        Dim strOpCode_Act As String = "GL_CLSDATA_ACT_DOWNLOAD"
        Dim strOpCode_SubAct As String = "GL_CLSDATA_SUBACT_DOWNLOAD"
        Dim strOpCode_Exp As String = "GL_CLSDATA_EXP_DOWNLOAD"
        Dim strOpCode_VehExpGrp As String = "GL_CLSDATA_VEHEXPGRP_DOWNLOAD"
        Dim strOpCode_VehExp As String = "GL_CLSDATA_VEHEXP_DOWNLOAD"
        Dim strOpCode_VehType As String = "GL_CLSDATA_VEHTYPE_DOWNLOAD"
        Dim strOpCode_Veh As String = "GL_CLSDATA_VEH_DOWNLOAD"
        Dim strOpCode_BlkGrp As String = "GL_CLSDATA_BLKGRP_DOWNLOAD"
        Dim strOpCode_Blk As String = "GL_CLSDATA_BLK_DOWNLOAD"
        DIM strOpCode_BlkAcc As String = "GL_CLSDATA_BLKACC_DOWNLOAD"
        Dim strOpCode_SubBlk As String = "GL_CLSDATA_SUBBLK_DOWNLOAD"
        Dim strOpCode_SubBlkAcc As String = "GL_CLSDATA_SUBBLKACC_DOWNLOAD"
        Dim strOpCode_AccGrp As String = "GL_CLSDATA_ACCGRP_DOWNLOAD"
        Dim strOpCode_Acc As String = "GL_CLSDATA_ACC_DOWNLOAD"
        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strXmlFileName As String = objGlobal.mtdGenerateDownloadFile(objGlobal.EnumDataTransferFileType.GL_GeneralLedgerReferenceData, False)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_GET_FTPPATH&errmesg=&redirect=")
        End Try

        If strAccClsGrp = "true" Then strParam = strParam & strOpCode_AccClsGrp & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.AccountClassGroup) & Chr(9)
        If strAccCls = "true" Then strParam = strParam & strOpCode_AccCls & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.AccountClass) & Chr(9)
        If strActGrp = "true" Then strParam = strParam & strOpCode_ActGrp & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.ActivityGroup) & Chr(9)
        If strAct = "true" Then strParam = strParam & strOpCode_Act & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.Activity) & Chr(9)
        If strSubAct = "true" Then strParam = strParam & strOpCode_SubAct & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.SubActivity) & Chr(9)
        If strExp = "true" Then strParam = strParam & strOpCode_Exp & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.Expense) & Chr(9)
        If strVehExpGrp = "true" Then strParam = strParam & strOpCode_VehExpGrp & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.VehicleExpenseGrp) & Chr(9)
        If strVehExp = "true" Then strParam = strParam & strOpCode_VehExp & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.VehicleExpense) & Chr(9)
        If strVehType = "true" Then strParam = strParam & strOpCode_VehType & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.VehicleType) & Chr(9)
        If strVeh = "true" Then strParam = strParam & strOpCode_Veh & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.Vehicle) & Chr(9)
        If strBlkGrp = "true" Then strParam = strParam & strOpCode_BlkGrp & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.BlockGroup) & Chr(9)
        If strBlk = "true" Then strParam = strParam & strOpCode_Blk & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.Block) & Chr(9) & _
                                    strOpCode_BlkAcc & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.BlockAccount) & Chr(9)
        If strSubBlk = "true" Then strParam = strParam & strOpCode_SubBlk & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.SubBlock) & Chr(9) & _
                                    strOpCode_SubBlkAcc & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.SubBlockAccount) & Chr(9)
        If strAccGrp = "true" Then strParam = strParam & strOpCode_AccGrp & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.AccountGroup) & Chr(9)
        If strAcc = "true" Then strParam = strParam & strOpCode_Acc & "|" & _
                                    objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.Account) & Chr(9)

        If strParam <> "" Then
            strParam = Mid(strParam, 1, Len(strParam) - 1)
            Try
                intErrNo = objGLData.mtdDownloadRef(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objDownloadDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GLDATA_DOWNLOAD_REF&errmesg=&redirect=")
            End Try
            objDownloadDs.WriteXml(strXmlPath, XmlWriteMode.WriteSchema)
            objStreamReader = File.OpenText(strXmlPath)
            strXmlb4Encrypt = objStreamReader.ReadToEnd()
            objStreamReader.Close()

            Try
                intErrNo = objSysCfg.mtdEncryptRef(strXmlb4Encrypt, ObjXmlEncrypted)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GLDATA_ENCRYPT_REF&errmesg=&redirect=")
            End Try

            Response.Write(ObjXmlEncrypted)
            Response.ContentType = "application/zip"
            Response.AddHeader("Content-Disposition", _
                                "attachment; filename=""" & strZipFileName & """")
        End If
    End Sub


End Class

