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


Public Class PR_data_download_statutory_savefile : Inherits Page

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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDownload), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub

    Sub SaveFile()
        Dim objStreamReader As StreamReader
        Dim strStatutory As String = Request.QueryString("Statutory")
        Dim strSelAccMonth As String = Request.QueryString("AccMth")
        Dim strSelAccYear As String = Request.QueryString("AccYr")
        Dim strTaxBranch As String = Request.QueryString("TaxBranch")
        Dim strOpCode As String 
        Dim strParam As String
        Dim strRecord As String = ""
        Dim strString As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strZipFileName As String 

        Select Case strStatutory 
        Case 1
            strOpCode = "PR_CLSDATA_EPFBORANGA"
        Case 2
            strOpCode = "PR_CLSDATA_TAXBORANGCP39"
        Case 3
            strOpCode = "PR_CLSDATA_SOCSOBORANG8A"
        End Select

        strParam = strSelAccMonth & "|" & strSelAccYear & "|" & strTaxBranch

        Try
            intErrNo = objPRData.mtdDownloadStatutory(strCompany, _
                                                      strLocation, _
                                                      strUserId, _
                                                      strAccMonth, _
                                                      strAccYear, _
                                                      strOpCode, _
                                                      strParam, _
                                                      objDownloadDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_DOWNLOAD_STATUTORY&errmesg=&redirect=")
        End Try

        If objDownloadDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDownloadDs.Tables(0).Rows.Count - 1
                Select Case strStatutory
                Case 1
                        strRecord = objDownloadDs.Tables(0).Rows(intCnt).Item("Temp") & chr(13) & chr(10)
                Case 2
                        strRecord = objDownloadDs.Tables(0).Rows(intCnt).Item("Temp") & chr(13) & chr(10)
                Case 3
                        strRecord = objDownloadDs.Tables(0).Rows(intCnt).Item("SocsoRef") & _
                                    objDownloadDs.Tables(0).Rows(intCnt).Item("SocsoId") & _                
                                    objDownloadDs.Tables(0).Rows(intCnt).Item("SocsoNo") & _                
                                    objDownloadDs.Tables(0).Rows(intCnt).Item("Period") & _                
                                    objDownloadDs.Tables(0).Rows(intCnt).Item("EmpName") & _                
                                    objDownloadDs.Tables(0).Rows(intCnt).Item("Amount") & chr(13) & chr(10)
                End Select 

                strString = strString & strRecord             
            Next
        End If

        Select Case strStatutory
        Case 1
            strZipFileName = "EPFORMA"
        Case 2
            strZipFileName = "CP39.txt"
        Case 3
            strZipFileName = "BRG8A.txt"
        End Select
 
        Response.Write(strString)
        Response.ContentType = "application/text"
        Response.AddHeader("Content-Disposition", "attachment; filename=""" & strZipFileName & """")

    End Sub


End Class

