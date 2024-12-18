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

Imports agri
Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.RC.clsData


Public Class RC_trx_download_savefile : inherits page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objRCData As New agri.RC.clsData()
    Dim objDownloadDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intADAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(EnumADAccessRights.RCDataTransfer), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub


    Sub SaveFile()
        Dim objStreamReader As StreamReader
        Dim strStkTrf As String = lcase(Request.QueryString("stktrf"))
        Dim strStkRcv As String = lcase(Request.QueryString("stkrcv"))
        Dim strGRN As String = lcase(Request.QueryString("grn"))
        Dim strBill As String = lcase(Request.QueryString("bill"))
        Dim strJrn As String = lcase(Request.QueryString("jrn"))
        Dim strOpCode_StkTrf As String = "RC_CLSDATA_STOCKTRANSFER_DOWNLOAD"
        Dim strOpCode_StkTrfLn As String = "RC_CLSDATA_STOCKTRANSFERLN_DOWNLOAD"
        Dim strOpCode_StkRcv As String = "RC_CLSDATA_STOCKRECEIVE_DOWNLOAD"
        Dim strOpCode_StkRcvLn As String = "RC_CLSDATA_STOCKRECEIVELN_DOWNLOAD"
        Dim strOpCode_GRN As String = "RC_CLSDATA_STOCKRETURN_DOWNLOAD"
        Dim strOpCode_GRNLn As String = "RC_CLSDATA_STOCKRETURNLN_DOWNLOAD"
        Dim strOpCode_DN As String = "RC_CLSDATA_DEBITNOTE_DOWNLOAD"
        Dim strOpCode_DNLn As String = "RC_CLSDATA_DEBITNOTELN_DOWNLOAD"
        Dim strOpCode_CN As String = "RC_CLSDATA_CREDITNOTE_DOWNLOAD"
        Dim strOpCode_CNLn As String = "RC_CLSDATA_CREDITNOTELN_DOWNLOAD"
        Dim strOpCode_Jrn As String = "RC_CLSDATA_JOURNAL_DOWNLOAD"
        Dim strOpCode_JrnLn As String = "RC_CLSDATA_JOURNALLN_DOWNLOAD"
        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strXmlFileName As String = objGlobal.mtdGenerateDownloadFile(EnumModule.Reconciliation, False)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RC_GET_FTPPATH&errmesg=&redirect=")
        End Try

        If strStkTrf = "true" Then strParam = strParam & strOpCode_StkTrf & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCStockTransfer) & Chr(9) & _
                                    strOpCode_StkTrfLn & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCStockTransferLn) & Chr(9)

        If strStkRcv = "true" Then strParam = strParam & strOpCode_StkRcv & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCStockReceive) & Chr(9) & _
                                    strOpCode_StkRcvLn & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCStockReceiveLn) & Chr(9)

        If strGRN = "true" Then strParam = strParam & strOpCode_GRN & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCGoodReturnNote) & Chr(9) & _
                                    strOpCode_GRNLn & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCGoodReturnNoteLn) & Chr(9)

        If strBill = "true" Then strParam = strParam & strOpCode_DN & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCBillDN) & Chr(9) & _
                                    strOpCode_DNLn & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCBillDNLn) & Chr(9) & _
                                    strOpCode_CN & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCBillCN) & Chr(9) & _
                                    strOpCode_CNLn & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCBillCNLn) & Chr(9)

        If strJrn = "true" Then strParam = strParam & strOpCode_Jrn & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCJournal) & Chr(9) & _
                                    strOpCode_JrnLn & "|" & _
                                    objRCData.mtdGetDownloadTable(RC.clsData.EnumDownloadType.RCJournalLn) & Chr(9)

        If strParam <> "" Then
            strParam = Mid(strParam, 1, Len(strParam) - 1)
            Try
                intErrNo = objRCData.mtdDownloadRef(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objDownloadDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RCDATA_DOWNLOAD_REF&errmesg=&redirect=")
            End Try

            objDownloadDs.WriteXml(strXmlPath, XmlWriteMode.WriteSchema)

            objStreamReader = File.OpenText(strXmlPath)
            strXmlb4Encrypt = objStreamReader.ReadToEnd()
            objStreamReader.Close()

            Try
                intErrNo = objSysCfg.mtdEncryptRef(strXmlb4Encrypt, objXmlEncrypted)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RCDATA_ENCRYPT_REF&errmesg=&redirect=")
            End Try

            Response.Write(objXmlEncrypted)
            Response.ContentType = "application/zip"
            Response.AddHeader("Content-Disposition", _
                                "attachment; filename=""" & strZipFileName & """")
        End If
    End Sub

End Class

