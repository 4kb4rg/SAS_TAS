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


Public Class Data_Upload_Budget : Inherits Page

    Protected WithEvents tblBefore As HtmlTable
    Protected WithEvents flUpload As HtmlInputFile
    Protected WithEvents flUploadMill As HtmlInputFile

    Protected WithEvents flUploadPD As HtmlInputFile
    Protected WithEvents lblErrNoFile As Label
    Protected WithEvents lblErrNoFilePD As Label
    Protected WithEvents lblErrNoFileMill As Label


    Protected WithEvents lblSuccess As Label
    Protected WithEvents lblSuccessRec As Label
    Protected WithEvents lblSuccessPath As Label
    Protected WithEvents lblError As Label

    Protected WithEvents lblPathExcel As Label
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents SaveBtnPD As ImageButton



    Protected WithEvents SaveMill As ImageButton
    Protected WithEvents UploadMill As ImageButton
    Protected WithEvents CancelMill As ImageButton

    Protected WithEvents dgDataList As DataGrid


    Protected WithEvents dgCek As DataGrid

    Dim ObjOk As New agri.GL.ClsTrx()


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
    Dim strSrcPath As String

    Private Const FAngka As String = "###,##0"

#Region "Component"
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

            tblBefore.Visible = True
        End If
    End Sub
    Sub UploadBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strSrcName As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intTotalRecords As Integer = 0
        Dim strFtpPath As String

        SaveBtn.Visible = True
        SaveBtnPD.Visible = False
        SaveMill.Visible = False


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
        lblSuccess.Text = strSrcPath
        BindGrid(True)
    End Sub
    Sub UploadMill_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strSrcName As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intTotalRecords As Integer = 0
        Dim strFtpPath As String

        SaveMill.Visible = True
        SaveBtn.Visible = False
        SaveBtnPD.Visible = False

        If Trim(flUploadMill.Value) = "" Then
            lblErrNoFileMill.Text = "Please select a file before clicking Upload button."
            lblErrNoFileMill.Visible = True
            Exit Sub
        ElseIf flUploadMill.PostedFile.ContentLength = 0 Then
            lblErrNoFileMill.Text = "The selected data transfer file is either not found or is corrupted."
            lblErrNoFileMill.Visible = True
            Exit Sub
        End If

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_GET_FTPPATH&errmesg=" & Exp.ToString() & "&redirect=wm/data/wm_data_uploadweigh.aspx")
        End Try

        strSrcName = Path.GetFileName(flUploadMill.PostedFile.FileName)
        strSrcPath = strFtpPath & strSrcName
        'If objGlobal.mtdValidateUploadFileName(strSrcName, objGlobal.EnumDataTransferFileType.WM_WeighBridgeData, strErrMsg) = False Then
        '    lblErrNoFile.Text = "<br>" & strErrMsg
        '    lblErrNoFile.Visible = True
        '    Exit Sub
        'End If
        Try
            flUploadMill.PostedFile.SaveAs(strSrcPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SAVEAS&errmesg=" & Exp.Message.ToString() & "&redirect=wm/data/wm_data_uploadweigh.aspx")
        End Try
        lblSuccess.Text = strSrcPath
        BindGrid_Mill(False)
    End Sub
    Sub UploadBtnPD_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strSrcName As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intTotalRecords As Integer = 0
        Dim strFtpPath As String


        SaveBtnPD.Visible = True
        SaveBtn.Visible = False
      '  SaveMill.Visible = False


        If Trim(flUploadPD.Value) = "" Then
            lblErrNoFilePD.Text = "Please select a file before clicking Upload button."
            lblErrNoFilePD.Visible = True
            Exit Sub
        ElseIf flUploadPD.PostedFile.ContentLength = 0 Then
            lblErrNoFilePD.Text = "The selected data transfer file is either not found or is corrupted."
            lblErrNoFilePD.Visible = True
            Exit Sub
        End If



        Try
            intErrNo = objSysCfg.mtdGetFtpPath(strFtpPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_GET_FTPPATH&errmesg=" & Exp.ToString() & "&redirect=wm/data/wm_data_uploadweigh.aspx")
        End Try


        strSrcName = Path.GetFileName(flUploadPD.PostedFile.FileName)
        strSrcPath = strFtpPath & strSrcName

        Try
            flUploadPD.PostedFile.SaveAs(strSrcPath)
            SaveBtn.Visible = False
            SaveBtnPD.Visible = True
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SAVEAS&errmesg=" & Exp.Message.ToString() & "&redirect=wm/data/wm_data_uploadweigh.aspx")
        End Try

        lblSuccess.Text = strSrcPath
        BindGridPD()
    End Sub
    Sub CancelBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        With Me
            .dgDataList.DataSource = Nothing
            .dgDataList.DataBind()

            .dgCek.DataSource = Nothing
            .dgCek.DataBind()

        End With
    End Sub
    Sub CancelBtnPD_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        With Me
            .dgDataList.DataSource = Nothing
            .dgDataList.DataBind()
        End With
    End Sub
    Sub SaveBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdBudget_Upload As String = "GL_CLSTRX_BUDGET_ESTATE_UPLOAD"
        Dim ParamNama As String
        Dim ParamValue As String

        Dim nI As Integer
        Dim intErrNo As Integer

        Dim nColNomor As Byte = 0
        Dim nColLokasi As Byte = 1
        Dim nColTahun As Byte = 2
        Dim nColCategory As Byte = 3
        Dim nColDivisi As Byte = 4
        Dim nColTahunTanam As Byte = 5
        Dim nColBlok As Byte = 6
        Dim nColKodeAkun As Byte = 7
        Dim nColKodeBarang As Byte = 8
        Dim nColLevel As Byte = 9
        Dim nColTotalBudgetRP As Byte = 10
        Dim nColTotalBudgetFisik As Byte = 11
        Dim nColRp1 As Byte = 12
        Dim nColFs1 As Byte = 13
        Dim nColRp2 As Byte = 14
        Dim nColFs2 As Byte = 15
        Dim nColRp3 As Byte = 16
        Dim nColFs3 As Byte = 17
        Dim nColRp4 As Byte = 18
        Dim nColFs4 As Byte = 19
        Dim nColRp5 As Byte = 20
        Dim nColFs5 As Byte = 21
        Dim nColRp6 As Byte = 22
        Dim nColFs6 As Byte = 23
        Dim nColRp7 As Byte = 24
        Dim nColFs7 As Byte = 25
        Dim nColRp8 As Byte = 26
        Dim nColFs8 As Byte = 27
        Dim nColRp9 As Byte = 28
        Dim nColFs9 As Byte = 29
        Dim nColRp10 As Byte = 30
        Dim nColFs10 As Byte = 31
        Dim nColRp11 As Byte = 32
        Dim nColFs11 As Byte = 33
        Dim nColRp12 As Byte = 34
        Dim nColFs12 As Byte = 35

        Dim StrLokasi, StrTahun, StrCategory, StrDivisi, StrTahunTanam, StrBlok, StrKodeAkun As String
        Dim StrKodeBarang, SrLevel As String
        Dim nTotalBudgetRP, nTotalBudgetFisik As Double
        Dim nNomRp1, nNomFs1, nNomRp2, nNomFs2, nNomRp3, nNomFs3 As Double
        Dim nNomRp4, nNomFs4, nNomRp5, nNomFs5, nNomRp6, nNomFs6 As Double
        Dim nNomRp7, nNomFs7, nNomRp8, nNomFs8, nNomRp9, nNomFs9 As Double
        Dim nNomRp10, nNomFs10, nNomRp11, nNomFs11 As Double
        Dim nNomRp12, nNomFs12 As Double

        Dim nStatusSave As Boolean = False


        For nI = 0 To dgDataList.Items.Count - 1
            If RTrim(dgDataList.Items(nI).Cells(nColKodeAkun).Text) <> "&nbsp;" Or RTrim(dgDataList.Items(nI).Cells(nColKodeAkun).Text) <> vbNullString Then

                StrLokasi = dgDataList.Items(nI).Cells(nColLokasi).Text
                StrTahun = dgDataList.Items(nI).Cells(nColTahun).Text

                If RTrim(dgDataList.Items(nI).Cells(nColCategory).Text) = "&nbsp;" Then
                    StrCategory = ""
                Else
                    StrCategory = dgDataList.Items(nI).Cells(nColCategory).Text
                End If
                If RTrim(dgDataList.Items(nI).Cells(nColDivisi).Text) = "&nbsp;" Then
                    StrDivisi = ""
                Else
                    StrDivisi = dgDataList.Items(nI).Cells(nColDivisi).Text
                End If
                If RTrim(dgDataList.Items(nI).Cells(nColTahunTanam).Text) = "&nbsp;" Then
                    StrTahunTanam = ""
                Else
                    StrTahunTanam = dgDataList.Items(nI).Cells(nColTahunTanam).Text
                End If
                If RTrim(dgDataList.Items(nI).Cells(nColBlok).Text) = "&nbsp;" Then
                    StrBlok = ""
                Else
                    StrBlok = dgDataList.Items(nI).Cells(nColBlok).Text
                End If
                If RTrim(dgDataList.Items(nI).Cells(nColKodeAkun).Text) = "&nbsp;" Then
                    StrKodeAkun = ""
                Else
                    StrKodeAkun = dgDataList.Items(nI).Cells(nColKodeAkun).Text
                End If
                If RTrim(dgDataList.Items(nI).Cells(nColKodeBarang).Text) = "&nbsp;" Then
                    StrKodeBarang = ""
                Else
                    StrKodeBarang = RTrim(dgDataList.Items(nI).Cells(nColKodeBarang).Text)
                End If
                If RTrim(dgDataList.Items(nI).Cells(nColLevel).Text) = "&nbsp;" Then
                    SrLevel = ""
                Else
                    SrLevel = dgDataList.Items(nI).Cells(nColLevel).Text
                End If

                nNomRp1 = 0
                nNomRp2 = 0
                nNomRp3 = 0
                nNomRp4 = 0
                nNomRp5 = 0
                nNomRp6 = 0
                nNomRp7 = 0
                nNomRp8 = 0
                nNomRp9 = 0
                nNomRp10 = 0
                nNomRp11 = 0
                nNomRp12 = 0
                nNomFs1 = 0
                nNomFs2 = 0
                nNomFs3 = 0
                nNomFs4 = 0
                nNomFs5 = 0
                nNomFs6 = 0
                nNomFs7 = 0
                nNomFs8 = 0
                nNomFs9 = 0
                nNomFs10 = 0
                nNomFs11 = 0
                nNomFs12 = 0

                nTotalBudgetRP = lCDbl(dgDataList.Items(nI).Cells(nColTotalBudgetRP).Text)
                nTotalBudgetFisik = lCDbl(dgDataList.Items(nI).Cells(nColTotalBudgetFisik).Text)
                nNomRp1 = lCDbl(dgDataList.Items(nI).Cells(nColRp1).Text)
                nNomFs1 = lCDbl(dgDataList.Items(nI).Cells(nColFs1).Text)
                nNomRp2 = lCDbl(dgDataList.Items(nI).Cells(nColRp2).Text)
                nNomFs2 = lCDbl(dgDataList.Items(nI).Cells(nColFs2).Text)
                nNomRp3 = lCDbl(dgDataList.Items(nI).Cells(nColRp3).Text)
                nNomFs3 = lCDbl(dgDataList.Items(nI).Cells(nColFs3).Text)
                nNomRp4 = lCDbl(dgDataList.Items(nI).Cells(nColRp4).Text)
                nNomFs4 = lCDbl(dgDataList.Items(nI).Cells(nColFs4).Text)
                nNomRp5 = lCDbl(dgDataList.Items(nI).Cells(nColRp5).Text)
                nNomFs5 = lCDbl(dgDataList.Items(nI).Cells(nColFs6).Text)
                nNomRp6 = lCDbl(dgDataList.Items(nI).Cells(nColRp6).Text)
                nNomFs6 = lCDbl(dgDataList.Items(nI).Cells(nColFs6).Text)
                nNomRp7 = lCDbl(dgDataList.Items(nI).Cells(nColRp7).Text)
                nNomFs7 = lCDbl(dgDataList.Items(nI).Cells(nColFs7).Text)
                nNomRp8 = lCDbl(dgDataList.Items(nI).Cells(nColRp8).Text)
                nNomFs8 = lCDbl(dgDataList.Items(nI).Cells(nColFs8).Text)
                nNomRp9 = lCDbl(dgDataList.Items(nI).Cells(nColRp9).Text)
                nNomFs9 = lCDbl(dgDataList.Items(nI).Cells(nColFs9).Text)
                nNomRp10 = lCDbl(dgDataList.Items(nI).Cells(nColRp10).Text)
                nNomFs10 = lCDbl(dgDataList.Items(nI).Cells(nColFs10).Text)
                nNomRp11 = lCDbl(dgDataList.Items(nI).Cells(nColRp11).Text)
                nNomFs11 = lCDbl(dgDataList.Items(nI).Cells(nColFs11).Text)
                nNomRp12 = lCDbl(dgDataList.Items(nI).Cells(nColRp12).Text)
                nNomFs12 = lCDbl(dgDataList.Items(nI).Cells(nColFs12).Text)

                If StrKodeAkun <> vbNullString Then
                    ParamNama = "LOC|TAHUN|BLKGRP|BLK|BLKSUB|COA|ITEM|STATUS|TAMOUNT|TFISIK|RP1|FS1|RP2|FS2|RP3|FS3|RP4|FS4|RP5|FS5|RP6|FS6|RP7|FS7|RP8|FS8|RP9|FS9|RP10|FS10|RP11|FS11|RP12|FS12|UD|CD|UID|SUBCAT|COALEVEL"
                    ParamValue = StrLokasi & "|" & StrTahun & "|" & StrDivisi & "|" & _
                                StrTahunTanam & "|" & StrBlok & "|" & StrKodeAkun & "|" & StrKodeBarang & "|" & 1 & "|" & _
                                nTotalBudgetRP & "|" & nTotalBudgetFisik & "|" & nNomRp1 & "|" & nNomFs1 & "|" & _
                                nNomRp2 & "|" & nNomFs2 & "|" & nNomRp3 & "|" & nNomFs3 & "|" & nNomRp4 & "|" & _
                                nNomFs4 & "|" & nNomRp5 & "|" & nNomFs5 & "|" & nNomRp6 & "|" & nNomFs6 & "|" & _
                                nNomRp7 & "|" & nNomFs7 & "|" & nNomRp8 & "|" & nNomFs8 & "|" & nNomRp9 & "|" & _
                                nNomFs9 & "|" & nNomRp10 & "|" & nNomFs10 & "|" & nNomRp11 & "|" & nNomFs11 & "|" & nNomRp12 & "|" & nNomFs12 & "|" & _
                                Format(Date.Now, "yyy-MM-dd HH:mm:ss") & "|" & Format(Date.Now, "yyy-MM-dd HH:mm:ss") & "|" & strUserId & "|" & _
                                StrCategory & "|" & SrLevel
                    Try
                        intErrNo = ObjOk.mtdInsertDataCommon(strOpCdBudget_Upload, ParamNama, ParamValue)
                        nStatusSave = True
                    Catch ex As Exception
                        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblError.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                    End Try
                End If
            End If
        Next

        If nStatusSave = True Then
            lblSuccess.Text = vbNullString
            lblSuccess.Text = "Upload Budget Sucsess...."
            lblSuccess.Visible = True
            dgDataList.DataSource = Nothing
            dgDataList.DataBind()
        Else
            lblSuccess.Visible = False
        End If
    End Sub
    Sub SaveBtnMill_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdBudget_Upload As String = "GL_CLSTRX_BUDGET_MILL_UPLOAD"
        Dim ParamNama As String
        Dim ParamValue As String

        Dim nI As Integer
        Dim intErrNo As Integer

        Dim nColLokasi As Byte = 0
        Dim nColTahun As Byte = 1
        Dim nColKodeAkun As Byte = 2
        Dim nColNamaAkun As Byte = 3	
	Dim nColMachine As Byte = 4
        Dim nColGroupCOA As Byte = 5
        Dim nColTotalBudgetRP As Byte = 6
        Dim nColRp1 As Byte = 7
        Dim nColRp2 As Byte = 8
        Dim nColRp3 As Byte = 9
        Dim nColRp4 As Byte = 10
        Dim nColRp5 As Byte = 11
        Dim nColRp6 As Byte = 12
        Dim nColRp7 As Byte = 13
        Dim nColRp8 As Byte = 14
        Dim nColRp9 As Byte = 15
        Dim nColRp10 As Byte = 16
        Dim nColRp11 As Byte = 17
        Dim nColRp12 As Byte = 18

        Dim StrLokasi, StrTahun, StrKodeAkun, StrMachine ,StrGroupCOA As String
        Dim nTotalBudgetRP As Double
        Dim nNomRp1, nNomRp2, nNomRp3 As Double
        Dim nNomRp4, nNomRp5, nNomRp6 As Double
        Dim nNomRp7, nNomRp8, nNomRp9 As Double
        Dim nNomRp10, nNomRp11 As Double
        Dim nNomRp12 As Double

        Dim Ps1, Ps2, Ps3, Ps4, Ps5, Ps6 As Double
        Dim Ps7, Ps8, Ps9, Ps10, Ps11, Ps12 As Double

        Dim FS1, FS2, Fs3, Fs4, Fs5, Fs6 As Double
        Dim Fs7, Fs8, Fs9, Fs10, Fs11, Fs12 As Double

        Dim nStatusSave As Boolean = False

        For nI = 0 To dgDataList.Items.Count - 1
            If RTrim(dgDataList.Items(nI).Cells(nColKodeAkun).Text) <> "&nbsp;" Or RTrim(dgDataList.Items(nI).Cells(nColKodeAkun).Text) <> vbNullString Then

                StrLokasi = dgDataList.Items(nI).Cells(nColLokasi).Text
                StrTahun = dgDataList.Items(nI).Cells(nColTahun).Text

                If RTrim(dgDataList.Items(nI).Cells(nColKodeAkun).Text) = "&nbsp;" Then
                    StrKodeAkun = ""
                Else
                    StrKodeAkun = dgDataList.Items(nI).Cells(nColKodeAkun).Text
                End If

                If RTrim(dgDataList.Items(nI).Cells(nColMachine).Text) = "&nbsp;" Then
                    StrMachine = ""
                Else
                    StrMachine = dgDataList.Items(nI).Cells(nColMachine).Text
                End If


		If RTrim(dgDataList.Items(nI).Cells(nColGroupCOA).Text) = "&nbsp;" Then
                    StrGroupCOA = ""
                Else
                    StrGroupCOA = dgDataList.Items(nI).Cells(nColGroupCOA).Text
                End If

                nNomRp1 = 0
                nNomRp2 = 0
                nNomRp3 = 0
                nNomRp4 = 0
                nNomRp5 = 0
                nNomRp6 = 0
                nNomRp7 = 0
                nNomRp8 = 0
                nNomRp9 = 0
                nNomRp10 = 0
                nNomRp11 = 0
                nNomRp12 = 0

                Ps1 = 0
                Ps2 = 0
                Ps3 = 0
                Ps4 = 0
                Ps5 = 0
                Ps6 = 0
                Ps7 = 0
                Ps8 = 0
                Ps9 = 0
                Ps10 = 0
                Ps11 = 0
                Ps12 = 0

                FS1 = 0
                FS2 = 0
                Fs3 = 0
                Fs4 = 0
                Fs5 = 0
                Fs6 = 0
                Fs7 = 0
                Fs8 = 0
                Fs9 = 0
                Fs10 = 0
                Fs11 = 0
                Fs12 = 0

                nTotalBudgetRP = lCDbl(dgDataList.Items(nI).Cells(nColTotalBudgetRP).Text)

                nNomRp1 = lCDbl(dgDataList.Items(nI).Cells(nColRp1).Text)
                nNomRp2 = lCDbl(dgDataList.Items(nI).Cells(nColRp2).Text)
                nNomRp3 = lCDbl(dgDataList.Items(nI).Cells(nColRp3).Text)
                nNomRp4 = lCDbl(dgDataList.Items(nI).Cells(nColRp4).Text)
                nNomRp5 = lCDbl(dgDataList.Items(nI).Cells(nColRp5).Text)
                nNomRp6 = lCDbl(dgDataList.Items(nI).Cells(nColRp6).Text)
                nNomRp7 = lCDbl(dgDataList.Items(nI).Cells(nColRp7).Text)
                nNomRp8 = lCDbl(dgDataList.Items(nI).Cells(nColRp8).Text)
                nNomRp9 = lCDbl(dgDataList.Items(nI).Cells(nColRp9).Text)
                nNomRp10 = lCDbl(dgDataList.Items(nI).Cells(nColRp10).Text)
                nNomRp11 = lCDbl(dgDataList.Items(nI).Cells(nColRp11).Text)
                nNomRp12 = lCDbl(dgDataList.Items(nI).Cells(nColRp12).Text)

                If StrKodeAkun <> vbNullString Then
                    ParamNama = "LOC|TAHUN|COA|BLKCODE|GRPCOA|TAMOUNT|PR1|PR2|PR3|PR4|PR5|PR6|PR7|PR8|PR9|PR10|PR11|PR12|RP1|RP2|RP3|RP4|RP5|RP6|RP7|RP8|RP9|RP10|RP11|RP12|FS1|FS2|FS3|FS4|FS5|FS6|FS7|FS8|FS9|FS10|FS11|FS12|CDATE|UDATE|UID"
                    ParamValue = StrLokasi & "|" & StrTahun & "|" & StrKodeAkun & "|" & StrMachine & "|" & _
                                StrGroupCOA & "|" & nTotalBudgetRP & "|" & _
                                Ps1 & "|" & Ps2 & "|" & Ps3 & "|" & Ps4 & "|" & Ps5 & "|" & Ps6 & "|" & _
                                Ps7 & "|" & Ps8 & "|" & Ps9 & "|" & Ps10 & "|" & Ps11 & "|" & Ps12 & "|" & _
                                nNomRp1 & "|" & nNomRp2 & "|" & nNomRp3 & "|" & nNomRp4 & "|" & nNomRp5 & "|" & nNomRp6 & "|" & _
                                nNomRp7 & "|" & nNomRp8 & "|" & nNomRp9 & "|" & nNomRp10 & "|" & nNomRp11 & "|" & nNomRp12 & "|" & _
                                FS1 & "|" & FS2 & "|" & Fs3 & "|" & Fs4 & "|" & Fs5 & "|" & Fs6 & "|" & _
                                Fs7 & "|" & Fs8 & "|" & Fs9 & "|" & Fs10 & "|" & Fs11 & "|" & Fs12 & "|" & _
                                Format(Date.Now, "yyy-MM-dd HH:mm:ss") & "|" & Format(Date.Now, "yyy-MM-dd HH:mm:ss") & "|" & strUserId
                    Try
                        intErrNo = ObjOk.mtdInsertDataCommon(strOpCdBudget_Upload, ParamNama, ParamValue)
                        nStatusSave = True
                    Catch ex As Exception
                        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblError.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                    End Try
                End If
            End If
        Next

        If nStatusSave = True Then
            lblSuccess.Text = vbNullString
            lblSuccess.Text = "Upload Budget Mill Sucsess...."
            lblSuccess.Visible = True
            dgDataList.DataSource = Nothing
            dgDataList.DataBind()
        Else
            lblSuccess.Visible = False
        End If
    End Sub
    Sub SaveBtnPD_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdBudgetProd_Upload As String = "GL_CLSTRX_BUDGET_ESTATE_PROD_UPLOAD"
        Dim ParamNama As String
        Dim ParamValue As String

        Dim nI As Integer
        Dim intErrNo As Integer

        Dim nColNomor As Byte = 0
        Dim nColLokasi As Byte = 1
        Dim nColTahun As Byte = 2
        Dim nColTahunTanam As Byte = 3
        Dim nColBlok As Byte = 4
        Dim nColBudgetRp1 As Byte = 5
        Dim nColRealisasiRp1 As Byte = 6
        Dim nColBudgetRp2 As Byte = 7
        Dim nColRealisasiRp2 As Byte = 8
        Dim nColBudgetRp3 As Byte = 9
        Dim nColRealisasiRp3 As Byte = 10
        Dim nColBudgetRp4 As Byte = 11
        Dim nColRealisasiRp4 As Byte = 12
        Dim nColBudgetRp5 As Byte = 13
        Dim nColRealisasiRp5 As Byte = 14
        Dim nColBudgetRp6 As Byte = 15
        Dim nColRealisasiRp6 As Byte = 16
        Dim nColBudgetRp7 As Byte = 17
        Dim nColRealisasiRp7 As Byte = 18
        Dim nColBudgetRp8 As Byte = 19
        Dim nColRealisasiRp8 As Byte = 20
        Dim nColBudgetRp9 As Byte = 21
        Dim nColRealisasiRp9 As Byte = 22
        Dim nColBudgetRp10 As Byte = 23
        Dim nColRealisasiRp10 As Byte = 24
        Dim nColBudgetRp11 As Byte = 25
        Dim nColRealisasiRp11 As Byte = 26
        Dim nColBudgetRp12 As Byte = 27
        Dim nColRealisasiRp12 As Byte = 28

        Dim StrLokasi, StrTahun, StrTahunTanam, StrBlok As String
        Dim nNomBudget1, nNomReal1, nNomBudget2, nNomReal2, nNomBudget3, nNomReal3 As Double
        Dim nNomBudget4, nNomReal4, nNomBudget5, nNomReal5, nNomBudget6, nNomReal6 As Double
        Dim nNomBudget7, nNomReal7, nNomBudget8, nNomReal8, nNomBudget9, nNomReal9 As Double
        Dim nNomBudget10, nNomReal10, nNomBudget11, nNomReal11 As Double
        Dim nNomBudget12, nNomReal12 As Double

        Dim nStatusSave As Boolean = False

        For nI = 0 To dgDataList.Items.Count - 1
            If (RTrim(dgDataList.Items(nI).Cells(nColTahunTanam).Text) <> "&nbsp;" Or RTrim(dgDataList.Items(nI).Cells(nColTahunTanam).Text) <> vbNullString) And _
                (RTrim(dgDataList.Items(nI).Cells(nColBlok).Text) <> "&nbsp;" Or RTrim(dgDataList.Items(nI).Cells(nColBlok).Text) <> vbNullString) Then

                StrLokasi = dgDataList.Items(nI).Cells(nColLokasi).Text
                StrTahun = dgDataList.Items(nI).Cells(nColTahun).Text

                If RTrim(dgDataList.Items(nI).Cells(nColTahunTanam).Text) = "&nbsp;" Then
                    StrTahunTanam = ""
                Else
                    StrTahunTanam = dgDataList.Items(nI).Cells(nColTahunTanam).Text
                End If
                If RTrim(dgDataList.Items(nI).Cells(nColBlok).Text) = "&nbsp;" Then
                    StrBlok = ""
                Else
                    StrBlok = dgDataList.Items(nI).Cells(nColBlok).Text
                End If


                nNomBudget1 = 0
                nNomReal1 = 0
                nNomBudget2 = 0
                nNomReal2 = 0
                nNomBudget3 = 0
                nNomReal3 = 0
                nNomBudget4 = 0
                nNomReal4 = 0
                nNomBudget5 = 0
                nNomReal5 = 0
                nNomBudget6 = 0
                nNomReal6 = 0
                nNomBudget7 = 0
                nNomReal7 = 0
                nNomBudget8 = 0
                nNomReal8 = 0
                nNomReal9 = 0
                nNomBudget9 = 0
                nNomReal10 = 0
                nNomBudget10 = 0
                nNomBudget11 = 0
                nNomReal11 = 0
                nNomBudget12 = 0
                nNomReal12 = 0

                nNomBudget1 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp1).Text)
                nNomReal1 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp1).Text)
                nNomBudget2 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp2).Text)
                nNomReal2 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp2).Text)
                nNomBudget3 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp3).Text)
                nNomReal3 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp3).Text)
                nNomBudget4 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp4).Text)
                nNomReal4 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp4).Text)
                nNomBudget5 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp5).Text)
                nNomReal5 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp5).Text)
                nNomBudget6 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp6).Text)
                nNomReal6 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp6).Text)
                nNomBudget7 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp7).Text)
                nNomReal7 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp7).Text)
                nNomBudget8 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp8).Text)
                nNomReal8 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp8).Text)
                nNomBudget9 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp9).Text)
                nNomReal9 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp9).Text)
                nNomBudget10 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp10).Text)
                nNomReal10 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp10).Text)
                nNomBudget11 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp11).Text)
                nNomReal1 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp11).Text)
                nNomBudget12 = lCDbl(dgDataList.Items(nI).Cells(nColBudgetRp12).Text)
                nNomReal2 = lCDbl(dgDataList.Items(nI).Cells(nColRealisasiRp12).Text)

                If StrTahunTanam <> vbNullString And StrBlok <> vbNullString Then
                    ParamNama = "LOC|TAHUN|GRPCOA|BLKSUB|RP1|FS1|RP2|FS2|RP3|FS3|RP4|FS4|RP5|FS5|RP6|FS6|RP7|FS7|RP8|FS8|RP9|FS9|RP10|FS10|RP11|FS11|RP12|FS12|YBUDGET|YREAL|CD|UID|KMDT"
                    ParamValue = StrLokasi & "|" & StrTahun & "|" & StrTahunTanam & "|" & StrBlok & "|" & _
                                nNomBudget1 & "|" & nNomReal1 & "|" & _
                                nNomBudget2 & "|" & nNomReal2 & "|" & nNomBudget3 & "|" & nNomReal3 & "|" & nNomBudget4 & "|" & _
                                nNomReal4 & "|" & nNomBudget5 & "|" & nNomReal5 & "|" & nNomBudget6 & "|" & nNomReal6 & "|" & _
                                nNomBudget7 & "|" & nNomReal7 & "|" & nNomBudget8 & "|" & nNomReal8 & "|" & nNomBudget9 & "|" & _
                                nNomReal9 & "|" & nNomBudget10 & "|" & nNomReal10 & "|" & nNomBudget11 & "|" & nNomReal11 & "|" & nNomBudget12 & "|" & nNomReal12 & "|" & _
                                0 & "|" & 0 & "|" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "|" & strUserId & "|" & _
                                strUserId & "|" & "SWT"
                    Try
                        intErrNo = ObjOk.mtdInsertDataCommon(strOpCdBudgetProd_Upload, ParamNama, ParamValue)
                        nStatusSave = True
                    Catch ex As Exception
                        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblError.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
                    End Try
                End If
            End If
        Next

        If nStatusSave = True Then
            lblSuccess.Text = vbNullString
            lblSuccess.Text = "Upload Budget Produksi Sucsess...."
            lblSuccess.Visible = True
            dgDataList.DataSource = Nothing
            dgDataList.DataBind()
        Else
            lblSuccess.Visible = False
        End If
    End Sub
#End Region

#Region "Procedure & Function "
    Sub BindGrid(ByVal KeyField As Boolean)
        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim DtSet As System.Data.DataSet
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter

        ''upload grid
        lblPathExcel.Text = lblSuccess.Text
        MyConnection = New System.Data.OleDb.OleDbConnection("Provider=microsoft.jet.oledb.4.0;Data Source=" & lblPathExcel.Text & ";Extended Properties=Excel 8.0;")

        dgDataList.DataSource = Nothing
        dgDataList.DataBind()

        dgCek.DataSource = Nothing
        dgCek.DataBind()

        MyCommand = New System.Data.OleDb.OleDbDataAdapter("SELECT *  From [Budget$]", MyConnection)
        MyCommand.TableMappings.Add("Table", "Green Golden System")
        DtSet = New System.Data.DataSet
        MyCommand.Fill(DtSet)
        dgDataList.Visible = True
        dgDataList.Dispose()
        dgDataList.DataSource = DtSet.Tables(0)
        dgDataList.DataBind()
        MyConnection.Close()

        ''cek jika ada yg double
        MyConnection = New System.Data.OleDb.OleDbConnection("Provider=microsoft.jet.oledb.4.0;Data Source=" & lblPathExcel.Text & ";Extended Properties=Excel 8.0;")
        If KeyField = True Then
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("SELECT Lokasi,Tahun,Category,Divisi,T_Tanam,Blok,AccCode,ItemCode,Count(*) AS JLh_Baris From [Budget$] Where AccCode <> '' " & _
                                                                "Group By Lokasi,Tahun,Category,Divisi,T_Tanam,Blok,AccCode,ItemCode " & _
                                                                "Having (Count (*) > 1)", MyConnection)
            MyCommand.TableMappings.Add("Table", "Green Golden System")
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            dgCek.Visible = True
            dgCek.Dispose()
            dgCek.DataSource = DtSet.Tables(0)
            dgCek.DataBind()
            MyConnection.Close()
        Else
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("SELECT LocCode,Tahun,AccCode,Blok,Vehicle,GROUP_COA, Count(*) AS Jlh_Baris " & _
                                                                "From [Budget$] Where AccCode <> '' " & _
                                                                "Group By LocCode,Tahun,AccCode,Blok,Vehicle,GROUP_COA Having (Count (*) > 1) ", MyConnection)
            MyCommand.TableMappings.Add("Table", "Green Golden System")
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            dgCek.Visible = True
            dgCek.Dispose()
            dgCek.DataSource = DtSet.Tables(0)
            dgCek.DataBind()
            MyConnection.Close()
        End If

    End Sub
    Sub BindGrid_Mill(ByVal KeyField As Boolean)
        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim DtSet As System.Data.DataSet
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter

        ''upload grid
        lblPathExcel.Text = lblSuccess.Text
        MyConnection = New System.Data.OleDb.OleDbConnection("Provider=microsoft.jet.oledb.4.0;Data Source=" & lblPathExcel.Text & ";Extended Properties=Excel 8.0;")

        dgDataList.DataSource = Nothing
        dgDataList.DataBind()

        dgCek.DataSource = Nothing
        dgCek.DataBind()

        MyCommand = New System.Data.OleDb.OleDbDataAdapter("SELECT *  From [Budget$]", MyConnection)
        MyCommand.TableMappings.Add("Table", "Green Golden System")
        DtSet = New System.Data.DataSet
        MyCommand.Fill(DtSet)
        dgDataList.Visible = True
        dgDataList.Dispose()
        dgDataList.DataSource = DtSet.Tables(0)
        dgDataList.DataBind()
        MyConnection.Close()
        ''cek jika ada yg double      
    End Sub
    Sub BindGridPD()
        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim DtSet As System.Data.DataSet
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter

        lblPathExcel.Text = lblSuccess.Text
        MyConnection = New System.Data.OleDb.OleDbConnection("Provider=microsoft.jet.oledb.4.0;Data Source=" & lblPathExcel.Text & ";Extended Properties=Excel 8.0;")
        'MyConnection = New System.Data.OleDb.OleDbConnection("Provider=Mirosoft.ACE.OLEDB.12.0;Data Source=" & strSrcPath & "\xxx.xls;Extended Properties=Excel 8.0;")
        'MyConnection = New System.Data.OleDb.OleDbConnection("Provider=Mirosoft.Jet.OLEDB.12.0;Data Source=" & strSrcPath & "\xxx.xls;Extended Properties=csv;")

        MyCommand = New System.Data.OleDb.OleDbDataAdapter("SELECT *  From [PRODUKSI$]", MyConnection)
        MyCommand.TableMappings.Add("Table", "Green Golden System")
        DtSet = New System.Data.DataSet
        MyCommand.Fill(DtSet)
        dgDataList.Visible = True
        dgDataList.Dispose()
        dgDataList.DataSource = DtSet.Tables(0)
        dgDataList.DataBind()
        MyConnection.Close()
    End Sub
    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgDataList.CurrentPageIndex = e.NewPageIndex

    End Sub
    Function lCDbl(ByVal PcVal As String) As Double
        If IsNumeric(PcVal) Then lCDbl = CDbl(PcVal) Else lCDbl = 0
    End Function
#End Region
End Class
