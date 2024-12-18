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
Imports System.Data.OleDb

Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.WM


Public Class Data_UploadHR_Absensi : Inherits Page

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
    Protected WithEvents GridView1 As GridView
    Protected WithEvents ddlTEST As DropDownList
    Protected WithEvents LblData As Label

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objWMData As New agri.WM.clsData()
    Dim objOk As New agri.GL.ClsTrx()
    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intWMAR As Integer

    Dim dbConn As OleDbConnection
    Dim MyDataAdapter As OleDbDataAdapter
    Dim MyDataTable As New DataTable
    Dim MyDataRow As DataRow

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim sStrSQL As String
        Dim strOpCd_Up As String = "PR_PR_TRX_ATTENDANCE_GENERATE_MACHINE"

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

        'dbConn = New OleDbConnection("Provider=microsoft.jet.oledb.4.0;data source=D:\att2000.mdb")
        'dbConn.Open()

        'sStrSQL = "SELECT H.USERID,U.SSN,H.CHECKTIME,H.CHECKTYPE,H.VERIFYCODE,H.SENSORID,H.WorkCode,H.sn,H.UserExtFmt From CHECKINOUT H " & _
        '                    "LEFT JOIN USERINFO U ON H.USERID=U.UserId Order By H.Checktime,H.Userid "
        'MyDataAdapter = New OleDbDataAdapter(sStrSQL, dbConn)
        'MyDataAdapter.Fill(MyDataTable)
        'dbConn.Close()
    End Sub

    Sub UploadBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strSrcPath As String = ""
        Dim arrSrcPath As Array
        Dim strSrcName As String = ""
        Dim intErrNo As Integer
        Dim strFtpPath As String
        Dim blnUpdate As Boolean = False
        Dim strOpCd_Up As String = "PR_PR_TRX_ATTENDANCE_GENERATE_MACHINE"
        Dim ParamNama As String
        Dim ParamValue As String

        Dim cUid As String
        Dim cIndate As Date
        Dim cChekTime As Date
        Dim cChekTipe As String
        Dim cSSN As String
        Dim sStrSQL As String
        Dim cStatus As String

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

        Try
            If File.Exists(strSrcPath) Then
                Kill(strSrcPath)
            End If
            flUpload.PostedFile.SaveAs(strSrcPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SAVEAS&errmesg=" & Exp.Message.ToString() & "&redirect=wm/data/wm_data_uploadweigh.aspx")
        End Try


        blnUpdate = True

        If blnUpdate = True Then

            Try
                intErrNo = objWMData.mtdUploadRef(strSrcPath)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_UPLOAD_REF&errmesg=" & Exp.Message.ToString() & "&redirect=wm/data/wm_data_uploadweigh.aspx")
            End Try

            dbConn = New OleDbConnection("Provider=microsoft.jet.oledb.4.0;data source=" & strFtpPath & strSrcName)
            dbConn.Open()

            sStrSQL = "SELECT H.USERID,U.SSN,H.CHECKTIME,H.CHECKTYPE,H.VERIFYCODE,H.SENSORID,H.WorkCode,H.sn,H.UserExtFmt From CHECKINOUT H " & _
                                "LEFT JOIN USERINFO U ON H.USERID=U.UserId Order By H.Checktime,H.Userid "
            MyDataAdapter = New OleDbDataAdapter(sStrSQL, dbConn)
            MyDataAdapter.Fill(MyDataTable)

            Try
                For Each MyDataRow In MyDataTable.Rows
                    cChekTime = "00:00:00"
                    cUid = MyDataRow.Item("USERID")
                    cIndate = Format(MyDataRow.Item("CHECKTIME"), "yyyy-MM-dd")
                    cChekTime = MyDataRow.Item("CHECKTIME")
                    cChekTipe = MyDataRow.Item("CHECKTYPE")
                    cSSN = "" & MyDataRow.Item("SSN")

                    Select Case cChekTipe
                        Case UCase("I")
                            cStatus = "IN"
                        Case LCase("o")
                            cStatus = "OUTP"
                        Case LCase("i")
                            cStatus = "INP"
                        Case UCase("O")
                            cStatus = "OUT"
                    End Select

                    ParamNama = "TIPE|LOC|TGL|CUID|ECODE|DI|DOP|DIP|DO|CD|UD|UI"
                    ParamValue = cStatus & "|" & _
                                 strLocation & "|" & _
                                 Format(cIndate, "yyyy-MM-dd") & "|" & _
                                 cUid & "|" & _
                                 cSSN & "|" & _
                                 cChekTime & "|" & _
                                 cChekTime & "|" & _
                                 cChekTime & "|" & _
                                 cChekTime & "|" & _
                                 Format(DateTime.Now(), "yyyy-MM-dd HH:mm:ss") & "|" & _
                                 Format(DateTime.Now(), "yyyy-MM-dd HH:mm:ss") & "|" & _
                                 strUserId

                    Try
                        intErrNo = objOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
                    Catch ex As Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MAIN_ADD&errmesg=" & ex.ToString() & "&redirect=")
                    End Try
                Next

            Catch ex As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MAIN_ADD&errmesg=" & ex.ToString() & "&redirect=")
            Finally
                dbConn.Close()
                dbConn = Nothing
            End Try

            lblSuccess.Text = lblSuccessRec.Text & lblSuccessPath.Text & flUpload.PostedFile.FileName
            lblSuccess.Visible = True
        End If
    End Sub
 
End Class