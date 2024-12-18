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
Imports Microsoft.VisualBasic

Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.WM


Public Class Data_Upload : Inherits Page

    Protected WithEvents tblBefore As HtmlTable
    Protected WithEvents flUpload As HtmlInputFile
    Protected WithEvents lblErrNoFile As Label
    Protected WithEvents lblSuccess As Label
    Protected WithEvents lblSuccessRec As Label
    Protected WithEvents lblSuccessPath As Label
    Protected WithEvents lblError As Label
    Protected WithEvents lblErrSupplier As Label
    Protected WithEvents lblErrBuyer As Label
    Protected WithEvents lblErrTransporter As Label

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objWMData As New agri.WM.clsData()
    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intWMAR As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intWMAR = Session("SS_WMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            lblErrNoFile.Visible = False
            lblSuccess.Visible = False
            lblErrSupplier.Visible = False
            lblErrBuyer.Visible = False
            lblErrTransporter.Visible = False
            tblBefore.Visible = True
        End If
    End Sub

    Sub UploadBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objStreamReader As StreamReader
        Dim objDtsXml As New DataSet()
        Dim strSrcPath As String = ""
        Dim arrSrcPath As Array
        Dim strSrcName As String = ""
        Dim strParam As String = ""
        Dim strSeller As String = ""
        Dim strBuyer As String = ""
        Dim strTransporter As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intTotalRecords As Integer = 0
        Dim strFtpPath As String
        Dim strXmlSrc As String
        Dim objSeller As String
        Dim objBuyer As String
        Dim objTransporter As String
        Dim blnUpdate As Boolean = False
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_GET_FTPPATH&errmesg=" & Exp.ToString() & "&redirect=wm/data/wm_data_uploadweigh.aspx")
        End Try


        strSrcName = Path.GetFileName(flUpload.PostedFile.FileName)
        strSrcPath = strFtpPath & strSrcName
        'If objGlobal.mtdValidateUploadFileName(strSrcName, objGlobal.EnumDataTransferFileType.WM_WeighBridgeData, strErrMsg) = False Then
        '    lblErrNoFile.Text = "<br>" & strErrMsg
        '    lblErrNoFile.Visible = True
        '    Exit Sub
        'End If
        Try
            flUpload.PostedFile.SaveAs(strSrcPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SAVEAS&errmesg=" & Exp.Message.ToString() & "&redirect=wm/data/wm_data_uploadweigh.aspx")
        End Try

        blnUpdate = True


        If blnUpdate = True Then
            objStreamReader = File.OpenText(strSrcPath)
            strXmlSrc = objStreamReader.ReadToEnd()
            objStreamReader.Close()

            'strXmlSrc = Replace(strXmlSrc, "<AccMonth></AccMonth>", "<AccMonth>" & Session("SS_PMACCMONTH") & "</AccMonth>")
            'strXmlSrc = Replace(strXmlSrc, "<AccYear></AccYear>", "<AccYear>" & Session("SS_PMACCYEAR") & "</AccYear>")
            'strXmlSrc = Replace(strXmlSrc, "<UpdateID></UpdateID>", "<UpdateID>" & Session("SS_USERID") & "</UpdateID>")

            Dim objStreamWrite As StreamWriter = New StreamWriter(strSrcPath)
            objStreamWrite.Write(strXmlSrc)
            objStreamWrite.Close()

            Try
                intErrNo = objWMData.mtdUploadRef(strSrcPath)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_UPLOAD_REF&errmesg=" & Exp.Message.ToString() & "&redirect=wm/data/wm_data_uploadweigh.aspx")
            End Try

            lblSuccess.Text = lblSuccessRec.Text & lblSuccessPath.Text & flUpload.PostedFile.FileName
            lblSuccess.Visible = True
        End If
    End Sub



End Class
