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
Imports Microsoft.VisualBasic.DateAndTime
Imports Microsoft.VisualBasic
Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.WM

Public Class WM_data_downloaddiap_savefile : inherits page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objWMData As New agri.WM.clsData()
    Dim objBIData As New agri.BI.clsData()
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
        Dim strOpCd_Ticket_Upd As String = "WM_CLSDATA_TICKET_BATCHNO_UPD"
        Dim strOpCd_Ticket As String = "WM_CLSDATA_DISPATCH_GET"
        Dim strOpCd_Bill As String = "BI_CLSDATA_SUPPLIER_DOWNLOAD"
        Dim strOpCd_Transporter As String = "WM_CLSDATA_TRANSPORTER_DOWNLOAD"
        Dim strOpCds As String
        Dim strBatchNo As String = Request.QueryString("batchno")
        Dim strFrom As String = Request.QueryString("from")
        Dim strTo As String = Request.QueryString("to")
        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strXmlFileName As String = objGlobal.mtdGenerateDownloadFile(objGlobal.EnumDataTransferFileType.WM_WeighingDispatchData, False)
        Dim strZipFileName As String = Mid(strXmlFileName, 1, Len(strXmlFileName) - 3) & "zip"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strXmlb4Encrypt As String = ""
        Dim blnIsNewBatch As Boolean = False
        Dim ObjXmlEncrypted As New Object()
        Dim ObjXmlDecrypted As New Object()

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
            strXmlPath = strFtpPath & strXmlFileName
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_GET_FTPPATH&errmesg=&redirect=")
        End Try

        If strBatchNo <> "" Then
            strParam = " AND DownloadBatchNo = '" & strBatchNo & "'"
        ElseIf strFrom = "" And strTo = "" Then
            strParam = " AND DownloadBatchNo = '' "
            blnIsNewBatch = True
        Else
            strParam = " AND DownloadBatchNo = '' " & _
                       " AND (DateDiff(Day, '" & strFrom & "', OutDate) >= 0) " & _
                       " AND (DateDiff(Day, '" & strTo & "', OutDate) <= 0) "
            blnIsNewBatch = True
        End If
        strOpCds = strOpCd_Ticket & "|" & objWMData.mtdGetDownloadTable(objWMData.EnumDownloadType.Ticket) & Chr(9) & _
                   strOpCd_Bill & "|" & objBIData.mtdGetDownloadTable(objBIData.EnumDownloadType.BillParty) & Chr(9) & _
                   strOpCd_Transporter & "|" & objWMData.mtdGetDownloadTable(objWMData.EnumDownloadType.Transporter)
        Try
            intErrNo = objWMData.mtdDownloadDispatch(strOpCd_Ticket_Upd, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     strOpCds, _
                                                     strParam, _
                                                     blnIsNewBatch, _
                                                     objDownloadDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WMDATA_DOWNLOAD_DISPATCH&errmesg=&redirect=")
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
    End Sub



End Class

