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
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PR.clsData

Public Class PR_data_attdinterface : Inherits Page

    Protected WithEvents tblBefore As HtmlTable
    Protected WithEvents tblAfter As HtmlTable
    Protected WithEvents flUpload As HtmlInputFile
    Protected WithEvents lblErrNoFile As Label
    Protected WithEvents lblErrUpload As Label
    Protected WithEvents lblErrMesage As Label

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objPRData As New agri.PR.clsData()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDwAttdInterface), intPRAR) = False Then
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
        Dim strTxtFile As String = ""
        Dim strXmlPath As String = ""

        Dim arrTxtFile As Array
        Dim strTxtFileName As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intFreeFile As Integer
        Dim strFtpPath As String
        Dim strXmlEncrypted As String = ""
        Dim objXmlDecrypted As New Object()

        Dim strDataRead As String
        Dim arrDataRead As Array
        Dim strEmpCode As String
        Dim strAttDate As String
        Dim strAttTime As String
        Dim strOpCd As String = "PR_CLSDATA_ATTDINTERFACE_ADD"

        Dim strDateSetting As String
        Dim objSysCfgDs as New Object()
        Dim objDateFormat as New Object()
        Dim objResultDate as String
        Dim strOpCd_Dfmt as String = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Dim strErrMsg As String
        
        If Trim(flUpload.Value) = "" Then
            lblErrUpload.Text = "Please select a file before clicking Upload button."
            lblErrUpload.Visible = True
            Exit Sub
        ElseIf flUpload.PostedFile.ContentLength = 0 Then
            lblErrUpload.Text = "The selected data transfer file is either not found or is corrupted."
            lblErrUpload.Visible = True
            Exit Sub
        ElseIf flUpload.PostedFile.ContentType <> "text/plain" Then
            lblErrUpload.Text = "The selected data transfer file is not .txt file format."
            lblErrUpload.Visible = True
            Exit Sub
        End If

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_GET_FTPPATH&errmesg=" & Exp.Message & "&redirect=pr/data/pr_data_attdinterface.aspx")
        End Try        

        strTxtFile = flUpload.PostedFile.FileName
        arrTxtFile = Split(strTxtFile, "\")
        strTxtFileName = arrTxtFile(UBound(arrTxtFile))
        strTxtFile = strFtpPath & strTxtFileName

        Dim Txtfile As New FileInfo(strTxtFile)
        If Txtfile.Exists Then
            File.Delete(strTxtFile)
        End If

        Try
            flUpload.PostedFile.SaveAs(strTxtFile)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_SAVEAS&errmesg=" & Exp.Message & "&redirect=pr/data/pr_data_attdinterface.aspx")
        End Try

        objStreamReader = File.OpenText(strTxtFile)

            While objStreamReader.Peek <> -1
                strDataRead = objStreamReader.ReadLine()
                If Trim(strDataRead) <> "" Then
                    arrDataRead = Split(strDataRead, ",")
                    strEmpCode = Trim(arrDataRead(0))
                    strAttDate = Trim(arrDataRead(1))
                    strAttTime = Trim(arrDataRead(2))


                    Try
                        intErrNo = objSysCfg.mtdGetConfigInfo(strOpCd_Dfmt, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            objSysCfgDs)
                    Catch Exp As System.Exception
                        Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_DOWNLOADBANK_GET_CONFIG_DATE&errmesg=" & Exp.ToString() & "&redirect=")
                    End Try

                    strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

                    If objGlobal.mtdValidInputDate(strDateSetting, strAttDate, objDateFormat, objResultDate) = True Then
                        strParam = strEmpCode & "|" & _
                                    objResultDate & " " & strAttTime
                        Try
                            intErrNo = objPRData.mtdUpload_AttdInterface(strOpCd,strParam,strUserId)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_INSERT_TO_DB&errmesg=" & Exp.Message & "&redirect=pr/data/pr_data_attdinterface.aspx")
                        End Try
                    End If
                End If
                If intErrNo <> 0 Then
                    lblErrUpload.Text = "<br>Error while inserting file to database. Please kindly contact administrator."
                    lblErrUpload.Visible = True
                    objStreamReader.Close()
                    Exit Sub
                End If
            End While

        objStreamReader.Close()

        Process_AttdTrx()
        If lblErrUpload.Visible = True Then
            Exit Sub
        End If
        tblBefore.Visible = False
        tblAfter.Visible = True

    End Sub
    Sub Process_AttdTrx ()
        Dim strParam As String
        Dim strOpCd As String = "PR_CLSDATA_ATTDINTERFACE_SP_GET"
        Dim intErrNo As Integer


        strParam =  objGlobal.mtdGetDocPrefix(EnumDocType.PRAttendanceTrx) & "|" & _
                    objGlobal.mtdGetDocPrefix(EnumDocType.PRAttendanceTrxLn) & "|" & _
                    Trim(strLocation)


        Try
            intErrNo = objPRData.mtdUpdAttdTrx(strOpCd,strParam,strUserId)
        Catch exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRDATA_EXECUTE_SP&errmesg=" & Exp.Message & "&redirect=pr/data/pr_data_attdinterface.aspx")
        End Try
        If intErrNo <> 0 Then
            lblErrUpload.Text = "<br>Error while inserting file to database. Please kindly contact administrator."
            lblErrUpload.Visible = True
        End If
    End Sub


End Class
