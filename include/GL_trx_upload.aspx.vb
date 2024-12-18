
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

Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl
Imports agri.GL.clsData


Public Class GL_trx_upload : Inherits Page

    Protected WithEvents flUpload As HtmlInputFile
    Protected WithEvents lblErrNoFile As Label
    Protected WithEvents lblErrUpload As Label
    Protected WithEvents lblErrNoXmlRecord As Label
    Protected WithEvents lblErrHasDBRecord As Label
    Protected WithEvents lblErrLoc As Label
    Protected WithEvents lblSuccess As Label

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLData As New agri.GL.clsData()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intGLAR As Integer
    Dim intConfig As Integer
    Dim intModuleActivate As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intGLAR = Session("SS_GLAR")
        intConfig = Session("SS_AUTOGLPOSTING")
        intModuleActivate = Session("SS_MODULEACTIVATE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLDataTransfer), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrNoFile.Visible = False
            lblErrUpload.Visible = False
            lblErrNoXmlRecord.Visible = False
            lblErrHasDBRecord.Visible = False
            lblErrLoc.Visible = False
            lblSuccess.Visible = False
            If Not Page.IsPostBack Then
            End If
        End If
    End Sub

    Sub UploadBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objStreamReader As StreamReader
        Dim objResult As Integer
        Dim strZipPath As String = ""
        Dim strXmlPath As String = ""
        Dim arrZipPath As Array
        Dim strZipName As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intFreeFile As Integer
        Dim intModule As Integer
        Dim strFtpPath As String
        Dim strXmlEncrypted As String = ""
        Dim objXmlDecrypted As New Object()
        Dim strErrMsg As String
        
        If Trim(flUpload.Value) = "" Then
            lblErrNoFile.Text = "Please select a file before clicking Upload button."
            lblErrNoFile.Visible = True
            Exit Sub
        ElseIf flUpload.PostedFile.ContentLength = 0 Then
            lblErrNoFile.Text = "The selected data transfer file is either not found or is corrupted."
            lblErrNoFile.Visible = True
            Exit Sub
        End If
        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DTRX_GET_FTPPATH&errmesg=" & Exp.ToString() & "&redirect=gl/data/gl_trx_upload.aspx")
        End Try        

        strZipPath = flUpload.PostedFile.FileName
        arrZipPath = Split(strZipPath, "\")
        strZipName = arrZipPath(UBound(arrZipPath))
        strZipPath = strFtpPath & strZipName
        If objGlobal.mtdValidateUploadFileName(strZipName, objGlobal.EnumDataTransferFileType.GL_GeneralLedgerTransaction, strErrMsg) = False Then
            lblErrNoFile.Text = strErrMsg
            lblErrNoFile.Visible = True
            Exit Sub
        End If
        Try
            strXmlPath = strFtpPath & Mid(strZipName, 1, Len(strZipName) - 3) & "xml"
        Catch Exp As System.Exception
            lblErrNoFile.Visible = True
            Exit sub
        End Try

        Dim Xmlfile As New FileInfo(strXmlPath)

        If Xmlfile.Exists Then
            File.Delete(strXmlPath)
        End If

        Try
            flUpload.PostedFile.SaveAs(strZipPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_SAVEAS&errmesg=" & Exp.ToString() & "&redirect=gl/data/gl_trx_upload.aspx")
        End Try


        objStreamReader = File.OpenText(strZipPath)
        strXmlEncrypted = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        Try
            intErrNo = objSysCfg.mtdDecryptRef(strXmlEncrypted, objXmlDecrypted)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_DECRYPT_REF&errmesg=" & Exp.ToString() & "&redirect=gl/data/gl_trx_upload.aspx")
        End Try

        intFreeFile = FreeFile()
        FileOpen(intFreeFile, strXmlPath, 8)  
        Print(intFreeFile, objXmlDecrypted)
        FileClose(intFreeFile)

        Try
            intErrNo = objGLData.mtdUploadGLTrx(strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strAccMonth, _
                                              strAccYear, _
                                              strXmlPath, _
                                              objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_UPLOAD_REF&errmesg=" & Exp.ToString() & "&redirect=gl/data/gl_trx_upload.aspx")
        End Try
        
        Select Case objResult
            Case 2
                lblErrNoXmlRecord.Visible = True
            Case 3
                lblErrHasDBRecord.Visible = True
            Case 4
                lblErrLoc.Visible = True
            Case 5
                lblSuccess.Visible = True
            Case Else
                lblErrUpload.Visible = True
        End Select
    End Sub


End Class
