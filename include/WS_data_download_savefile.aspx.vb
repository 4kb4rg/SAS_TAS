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
Imports agri.WS.clsData


Public Class ws_data_download_savefile : inherits page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objWSData As New agri.WS.clsData()
    Dim objDownloadDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intWSAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intWSAR = Session("SS_WSAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDataTransfer), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub


    Sub SaveFile()
        Dim objStreamReader As StreamReader

        Dim strProdType As String = lcase(Request.QueryString("prodtype"))
        Dim strProdBrand As String = lcase(Request.QueryString("prodbrand"))
        Dim strProdModel As String = lcase(Request.QueryString("prodmodel"))
        Dim strProdCat As String = lcase(Request.QueryString("prodcat"))
        Dim strProdMat As String = lcase(Request.QueryString("prodmat"))
        Dim strStockAnalysis As String = lcase(Request.QueryString("stockanalysis"))
        Dim strDirectChargeItem As String = lcase(Request.QueryString("directchargeitem"))
        Dim strWorkCode As String = lcase(Request.QueryString("workcode"))
        Dim strItemPart As String = LCase(Request.QueryString("itempart"))

        Dim strOpCode_ProdType As String = "IN_CLSDATA_PRODTYPE_DOWNLOAD"
        Dim strOpCode_ProdBrand As String = "IN_CLSDATA_PRODBRAND_DOWNLOAD"
        Dim strOpCode_ProdModel As String = "IN_CLSDATA_PRODMODEL_DOWNLOAD"
        Dim strOpCode_ProdCat As String = "IN_CLSDATA_PRODCAT_DOWNLOAD"
        Dim strOpCode_ProdMat As String = "IN_CLSDATA_PRODMAT_DOWNLOAD"
        Dim strOpCode_StockAnalysis As String = "IN_CLSDATA_STOCKANALYSIS_DOWNLOAD"
        Dim strOpCode_DirectChargeItem As String = "IN_CLSDATA_DIRECTCHARGEMASTER_DOWNLOAD"
        Dim strOpCode_WorkCode As String = "WS_CLSDATA_WORKCODE_DOWNLOAD"
        Dim strOpCode_WorkShop As String = "WS_CLSDATA_WORKSHOP_DOWNLOAD"
        Dim strOpCode_WorkShopLn As String = "WS_CLSDATA_WORKSHOP_LINE_DOWNLOAD"
        Dim strOpCode_ItemPart As String = "IN_CLSDATA_ITEMPART_DOWNLOAD"

        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strXmlFileName As String = objGlobal.mtdGenerateDownloadFile(objGlobal.EnumDataTransferFileType.WS_WorkshopReferenceData, False)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INDATA_GET_FTPPATH&errmesg=&redirect=")
        End Try

        If strProdType = "true" Then strParam = strParam & strOpCode_ProdType & "|" & _
                                    objWSData.mtdGetDownloadTable(objWSData.EnumDownloadType.ProdType) & Chr(9)
        If strProdBrand = "true" Then strParam = strParam & strOpCode_ProdBrand & "|" & _
                                    objWSData.mtdGetDownloadTable(objWSData.EnumDownloadType.ProdBrand) & Chr(9)
        If strProdModel = "true" Then strParam = strParam & strOpCode_ProdModel & "|" & _
                                    objWSData.mtdGetDownloadTable(objWSData.EnumDownloadType.ProdModel) & Chr(9)
        If strProdCat = "true" Then strParam = strParam & strOpCode_ProdCat & "|" & _
                                    objWSData.mtdGetDownloadTable(objWSData.EnumDownloadType.ProdCat) & Chr(9)
        If strProdMat = "true" Then strParam = strParam & strOpCode_ProdMat & "|" & _
                                    objWSData.mtdGetDownloadTable(objWSData.EnumDownloadType.ProdMat) & Chr(9)
        If strStockAnalysis = "true" Then strParam = strParam & strOpCode_StockAnalysis & "|" & _
                                    objWSData.mtdGetDownloadTable(objWSData.EnumDownloadType.StockAnalysis) & Chr(9)
        If strDirectChargeItem = "true" Then strParam = strParam & strOpCode_DirectChargeItem & "|" & _
                                    objWSData.mtdGetDownloadTable(objWSData.EnumDownloadType.DirectChargeItem) & Chr(9)
        If strWorkCode = "true" Then strParam = strParam & strOpCode_WorkCode & "|" & _
                                    objWSData.mtdGetDownloadTable(objWSData.EnumDownloadType.WorkCode) & Chr(9) & _
                                    strOpCode_WorkShop & "|" & _
                                    objWSData.mtdGetDownloadTable(objWSData.EnumDownloadType.WorkShop) & Chr(9) & _
                                    strOpCode_WorkShopLn & "|" & _
                                    objWSData.mtdGetDownloadTable(objWSData.EnumDownloadType.WorkShopLn) & Chr(9)
        If strItemPart = "true" Then strParam = strParam & strOpCode_ItemPart & "|" & _
                                    objWSData.mtdGetDownloadTable(objWSData.EnumDownloadType.WSItemPart) & Chr(9)

        If strParam <> "" Then
            strParam = Mid(strParam, 1, Len(strParam) - 1)
            Try
                intErrNo = objWSData.mtdDownloadRef(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objDownloadDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WSDATA_DOWNLOAD_REF&errmesg=&redirect=")
            End Try


            objDownloadDs.WriteXml(strXmlPath, XmlWriteMode.WriteSchema)

            objStreamReader = File.OpenText(strXmlPath)
            strXmlb4Encrypt = objStreamReader.ReadToEnd()
            objStreamReader.Close()

            Try
                intErrNo = objSysCfg.mtdEncryptRef(strXmlb4Encrypt, ObjXmlEncrypted)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WSDATA_ENCRYPT_REF&errmesg=&redirect=")
            End Try

            Response.Write(ObjXmlEncrypted)
            Response.ContentType = "application/zip"
            Response.AddHeader("Content-Disposition", _
                                "attachment; filename=""" & strZipFileName & """")
        End If
    End Sub



End Class

