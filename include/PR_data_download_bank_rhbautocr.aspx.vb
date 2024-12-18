Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.FileSystem

Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PR.clsData


Public Class PR_data_download_bank_rhbautocr : Inherits Page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objPRData As New agri.PR.clsData()
    Dim objPRRpt As New agri.PR.clsReport()
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownloadBankAuto), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub

    Sub SaveFile()
        Dim objStreamReader As StreamReader
        Dim strSelAccMonth As String = Request.QueryString("AccMth")
        Dim strSelAccYear As String = Request.QueryString("AccYr")
        Dim strBankCode As String = Request.QueryString("BankCode")
        Dim strCreditDate As String = Request.QueryString("CreditDate")
        Dim strAccPeriod As String
        Dim strOpCode As String 
        Dim strParam As String
        Dim strRecord As String = ""
        Dim strString As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strZipFileName As String 
        Dim strOpCode_UpdBank As String

        objPRRpt.GetCharPeriod(strSelAccMonth, strSelAccYear, strAccPeriod)

        strOpCode = "PR_CLSDATA_WAGES_GET_AUTOCR"

        strParam = strSelAccMonth & "|" & strSelAccYear & "|" & strAccPeriod & "|" & strBankCode & "|" & strCreditDate

        Try
            intErrNo = objPRData.mtdDownloadBank(strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 strOpCode, _
                                                 strParam, _
                                                 objDownloadDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_DOWNLOAD_BANK&errmesg=&redirect=")
        End Try

        If objDownloadDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDownloadDs.Tables(0).Rows.Count - 1
                strRecord = objDownloadDs.Tables(0).Rows(intCnt).Item("Temp") & chr(13) & chr(10)

                strString = strString & strRecord             
            Next
        End If

        strZipFileName = "DATAFILE"
 
        Response.Write(strString)
        Response.ContentType = "application/text"
        Response.AddHeader("Content-Disposition", "attachment; filename=""" & strZipFileName & """")
    
        strParam = ""
        strParam = strBankCode & "|" & strCreditDate

        strOpCode_UpdBank = "PR_CLSDATA_BANK_UPD"

        Try
            intErrNo = objPRData.mtdDownloadBank_UpdBank(strOpCode_UpdBank, _
                                                         strParam, _
                                                         strUserId)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_DOWNLOAD_UPDBANK&errmesg=&redirect=")
        End Try
    End Sub

End Class

