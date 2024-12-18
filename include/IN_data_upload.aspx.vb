Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.FileSystem

Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.IN.clsData

Public Class IN_data_upload : Inherits Page

    Protected WithEvents tblBefore As HtmlTable
    Protected WithEvents tblAfter As HtmlTable
    Protected WithEvents flUpload As HtmlInputFile
    Protected WithEvents lblErrUpload As Label
    Protected WithEvents lblErrMesage As Label

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objInData As New agri.IN.clsData()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDataTransfer), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            tblBefore.Visible = True
            tblAfter.Visible = False
            lblErrUpload.Visible = False
            lblErrUpload.ForeColor = System.Drawing.Color.Red
        End If
    End Sub

    Sub UploadBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objStreamReader As StreamReader
        Dim strZipPath As String = ""
        Dim strXmlPath As String = ""

        Dim arrZipPath As Array
        Dim strZipName As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intFreeFile As Integer
        Dim strFtpPath As String
        Dim strXmlEncrypted As String = ""
        Dim objXmlDecrypted As New Object()
        Dim strErrMsg As String
        
        If Trim(flUpload.Value) = "" Then
            lblErrUpload.Text = "Please select a file before clicking Upload button."
            lblErrUpload.Visible = True
            Exit Sub
        ElseIf flUpload.PostedFile.ContentLength = 0 Then
            lblErrUpload.Text = "The selected data transfer file is either not found or is corrupted."
            lblErrUpload.Visible = True
            Exit Sub
        End If
        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INDATA_GET_FTPPATH&errmesg=" & lblErrMesage.Text & "&redirect=in/data/in_data_upload.aspx")
        End Try        
        strZipPath = flUpload.PostedFile.FileName
        arrZipPath = Split(strZipPath, "\")
        strZipName = arrZipPath(UBound(arrZipPath))
        strZipPath = strFtpPath & strZipName
        If objGlobal.mtdValidateUploadFileName(strZipName, objGlobal.EnumDataTransferFileType.IN_InventoryReferenceData, strErrMsg) = False Then
            lblErrUpload.Text = "<br>" & strErrMsg
            lblErrUpload.Visible = True
            Exit Sub
        End If
        strXmlPath = strFtpPath & Mid(strZipName, 1, Len(strZipName) - 3) & "xml"

        Dim Xmlfile As New FileInfo(strXmlPath)

        If Xmlfile.Exists Then
            File.Delete(strXmlPath)
        End If

        Try
            flUpload.PostedFile.SaveAs(strZipPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INDATA_SAVEAS&errmesg=" & lblErrUpload.Text & "&redirect=in/data/in_data_upload.aspx")
        End Try


        objStreamReader = File.OpenText(strZipPath)
        strXmlEncrypted = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        Try
            intErrNo = objSysCfg.mtdDecryptRef(strXmlEncrypted, objXmlDecrypted)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INDATA_DECRYPT_REF&errmesg=" & lblErrMesage.Text & "&redirect=in/data/in_data_upload.aspx")
        End Try

        intFreeFile = FreeFile()
        FileOpen(intFreeFile, strXmlPath, 8)  
        Print(intFreeFile, objXmlDecrypted)
        FileClose(intFreeFile)

        Try
            intErrNo = objInData.mtdUploadRef(strXmlPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INDATA_UPLOAD_REF&errmesg=" & lblErrMesage.Text & "&redirect=in/data/in_data_upload.aspx")
        End Try
        tblBefore.Visible = False
        tblAfter.Visible = True
    End Sub

End Class
