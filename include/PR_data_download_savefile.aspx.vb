Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.FileSystem
Imports Microsoft.VisualBasic

Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PR.clsData

Public Class PR_data_download_savefile : Inherits Page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
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
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDataTransfer), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub


    Sub SaveFile()
        Dim objStreamReader As StreamReader

        Dim strAdGroup As String = lcase(Request.QueryString("adgroup"))
        Dim strContractor As String = lcase(Request.QueryString("contractor"))
        Dim strAttd As String = lcase(Request.QueryString("attd"))
        Dim strAttdIncScheme As String = lcase(Request.QueryString("attdincscheme"))
        Dim strHarvIncScheme As String = lcase(Request.QueryString("harvincscheme"))
        Dim strDiff As String = lcase(Request.QueryString("diff"))
        Dim strPayDiv As String = lcase(Request.QueryString("paydiv"))
        Dim strMAC As String = lcase(Request.QueryString("mac"))

        Dim strOpCode_AdGroup As String = "PR_CLSDATA_ADGROUP_DOWNLOAD"
        Dim strOpCode_Ad As String = "PR_CLSDATA_AD_DOWNLOAD"
        Dim strOpCode_Contractor As String = "PR_CLSDATA_CONTRACTOR_DOWNLOAD"
        Dim strOpCode_Attd As String = "PR_CLSDATA_ATTENDANCE_DOWNLOAD"
        Dim strOpCode_AttdIncScheme As String = "PR_CLSDATA_ATTENDANCE_INCSCHEME_DOWNLOAD"
        Dim strOpCode_AttdIncSchemeLn As String = "PR_CLSDATA_ATTENDANCE_INCSCHEMELN_DOWNLOAD"
        Dim strOpCode_HarvIncScheme As String = "PR_CLSDATA_HARVESTING_INCSCHEME_DOWNLOAD"
        Dim strOpCode_Diff As String = "PR_CLSDATA_DIFFERENTIALS_DOWNLOAD"
        Dim strOpCode_DiffLn As String = "PR_CLSDATA_DIFFERENTIALSLN_DOWNLOAD"
        Dim strOpCode_PayDiv As String = "PR_CLSDATA_PAYDIVISION_DOWNLOAD"
        Dim strOpCode_MAC As String = "PR_CLSDATA_MAC_DOWNLOAD"

        Dim strFtpPath As String
        Dim strXmlPath As String = ""
        Dim strXmlFileName As String = objGlobal.mtdGenerateDownloadFile(objGlobal.EnumDataTransferFileType.PR_PayrollReferenceData, False)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_GET_FTPPATH&errmesg=&redirect=")
        End Try

        If strAdGroup = "true" Then strParam = strParam & strOpCode_AdGroup & "|" & _
                                    objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.ADGroup) & Chr(9)
        If strAdGroup = "true" Then strParam = strParam & strOpCode_Ad & "|" & _
                                    objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.AD) & Chr(9)
        If strContractor = "true" Then strParam = strParam & strOpCode_Contractor & "|" & _
                                    objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.Contractor) & Chr(9)
        If strAttd = "true" Then strParam = strParam & strOpCode_Attd & "|" & _
                                    objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.Attendance) & Chr(9)
        If strAttdIncScheme = "true" Then strParam = strParam & strOpCode_AttdIncScheme & "|" & _
                                    objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.AttdIncentive) & Chr(9)
        If strAttdIncScheme = "true" Then strParam = strParam & strOpCode_AttdIncSchemeLn & "|" & _
                                    objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.AttdIncentiveLn) & Chr(9)
        If strHarvIncScheme = "true" Then strParam = strParam & strOpCode_HarvIncScheme & "|" & _
                                    objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.HarvIncentive) & Chr(9)
        If strDiff = "true" Then strParam = strParam & strOpCode_Diff & "|" & _
                                    objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.Diff) & Chr(9)
        If strDiff = "true" Then strParam = strParam & strOpCode_DiffLn & "|" & _
                                    objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.DiffLn) & Chr(9)
        If strPayDiv = "true" Then strParam = strParam & strOpCode_PayDiv & "|" & _
                                    objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.PayDivision) & Chr(9)
        If strMAC = "true" Then strParam = strParam & strOpCode_MAC & "|" & _
                                    objPRData.mtdGetDownloadTable(objPRData.EnumDownloadType.MAC) & Chr(9)
    
        If strParam <> "" Then
            strParam = Mid(strParam, 1, Len(strParam) - 1)
            Try
                intErrNo = objPRData.mtdDownloadRef(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    objDownloadDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_DOWNLOAD_REF&errmesg=&redirect=")
            End Try


            objDownloadDs.WriteXml(strXmlPath, XmlWriteMode.WriteSchema)

            objStreamReader = File.OpenText(strXmlPath)
            strXmlb4Encrypt = objStreamReader.ReadToEnd()
            objStreamReader.Close()

            Try
                intErrNo = objSysCfg.mtdEncryptRef(strXmlb4Encrypt, objXmlEncrypted)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_ENCRYPT_REF&errmesg=&redirect=")
            End Try

            Response.Write(objXmlEncrypted)
            Response.ContentType = "application/zip"
            Response.AddHeader("Content-Disposition", _
                                "attachment; filename=""" & strZipFileName & """")
        End If
    End Sub


End Class

