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

Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.PWSystem.clsConfig
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.BI.clsData

Public Class GL_data_download_flatfile_save : Inherits Page

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLData As New agri.GL.clsData()
    Dim objDownloadDs As New DataSet()
    Dim objPopulateDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intGLAR As Integer
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intGLAR = Session("SS_GLAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLUpload), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            SaveFile()
        End If
    End Sub


    Sub SaveFile()
        Dim objStreamReader As StreamReader
        Dim strPopulateParam As String 
        Dim strRetrieveParam As String 
        Dim strIsBlock As String = ""
        Dim SelCompCode As String = Request.QueryString("SelCompCode")
        Dim SelCropCode As String = Request.QueryString("SelCropCode")
        Dim SelCurrency As String = Request.QueryString("SelCurrency")
        Dim SelStatus As String = Request.QueryString("SelStatus")
        Dim SelAccMonth As String = Request.QueryString("SelAccMonth")
        Dim SelAccYear As String = Request.QueryString("SelAccYear")
        Dim SelBlockStatus As String = Request.QueryString("SelBlockStatus")
        Dim SelSubBlockStatus As String = Request.QueryString("SelSubBlockStatus")

        Dim strHQORD As String = Request.QueryString("HQORD")
        Dim strHQORC As String = Request.QueryString("HQORC")
        Dim strHQGLD As String = Request.QueryString("HQGLD")
        Dim strHQGLC As String = Request.QueryString("HQGLC")
        Dim strHQXTD As String = Request.QueryString("HQXTD")
        Dim strHQXTC As String = Request.QueryString("HQXTC")
        Dim strHQXRD As String = Request.QueryString("HQXRD")
        Dim strHQXRC As String = Request.QueryString("HQXRC")
        Dim strHQOBD As String = Request.QueryString("HQOBD")
        Dim strHQOBC As String = Request.QueryString("HQOBC")

        Dim strOpCode_Populate_HQORD As String = "GL_CLSDATA_HQORD_POPULATE"
        Dim strOpCode_Populate_HQORC As String = "GL_CLSDATA_HQORC_POPULATE"
        Dim strOpCode_Populate_HQGLD As String = "GL_CLSDATA_HQGLD_POPULATE"
        Dim strOpCode_Populate_HQGLC As String = "GL_CLSDATA_HQGLC_POPULATE"
        Dim strOpCode_Populate_HQXTD As String = "GL_CLSDATA_HQXTD_POPULATE"
        Dim strOpCode_Populate_HQXTC As String = "GL_CLSDATA_HQXTC_POPULATE"
        Dim strOpCode_Populate_HQXRD As String = "GL_CLSDATA_HQXRD_POPULATE"
        Dim strOpCode_Populate_HQXRC As String = "GL_CLSDATA_HQXRC_POPULATE"
        Dim strOpCode_Populate_HQOBD As String = "GL_CLSDATA_HQOBD_POPULATE"
        Dim strOpCode_Populate_HQOBC As String = "GL_CLSDATA_HQOBC_POPULATE"

        Dim strOpCode_Get_HQORD As String = "GL_CLSDATA_HQORD_GET"
        Dim strOpCode_Get_HQORC As String = "GL_CLSDATA_HQORC_GET"
        Dim strOpCode_Get_HQGLD As String = "GL_CLSDATA_HQGLD_GET"
        Dim strOpCode_Get_HQGLC As String = "GL_CLSDATA_HQGLC_GET"
        Dim strOpCode_Get_HQXTD As String = "GL_CLSDATA_HQXTD_GET"
        Dim strOpCode_Get_HQXTC As String = "GL_CLSDATA_HQXTC_GET"
        Dim strOpCode_Get_HQXRD As String = "GL_CLSDATA_HQXRD_GET"
        Dim strOpCode_Get_HQXRC As String = "GL_CLSDATA_HQXRC_GET"
        Dim strOpCode_Get_HQOBD As String = "GL_CLSDATA_HQOBD_GET"
        Dim strOpCode_Get_HQOBC As String = "GL_CLSDATA_HQOBC_GET"

        Dim FileLocation
        Dim strHQORDFileName As String = "HQORD.DAT"
        Dim strHQORCFileName As String = "HQORC.DAT"
        Dim strHQGLDFileName As String = "HQGLD.DAT"
        Dim strHQGLCFileName As String = "HQGLC.DAT"
        Dim strHQXTDFileName As String = "HQXTD.DAT"
        Dim strHQXTCFileName As String = "HQXTC.DAT"
        Dim strHQXRDFileName As String = "HQXRD.DAT"
        Dim strHQXRCFileName As String = "HQXRC.DAT"
        Dim strHQOBDFileName As String = "HQOBD.DAT"
        Dim strHQOBCFileName As String = "HQOBC.DAT"

        Dim strFileList As String = ""

        If SelSubBlockStatus = "" Then
            strIsBlock = "YES"
        End If

        Try
            intErrNo = objSysCfg.mtdGetFtpPath(FileLocation)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_GET_FTPPATH&errmesg=&redirect=")
        End Try

        If strHQORD = "True" Then
            strPopulateParam = strPopulateParam & strOpCode_Populate_HQORD & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQORD) & Chr(9)
            strRetrieveParam = strRetrieveParam & strOpCode_Get_HQORD & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQORD) & "|" & _
                               FileLocation & strHQORDFileName & Chr(9)
            strFileList = strFileList & strHQORDFileName & "=Yes&"
        End If

        If strHQORC = "True" Then
            strPopulateParam = strPopulateParam & strOpCode_Populate_HQORC & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQORC) & Chr(9)
            strRetrieveParam = strRetrieveParam & strOpCode_Get_HQORC & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQORC) & "|" & _
                               FileLocation & strHQORCFileName & Chr(9)
            strFileList = strFileList & strHQORCFileName & "=Yes&"
        End If

        If strHQGLD = "True" Then
            strPopulateParam = strPopulateParam & strOpCode_Populate_HQGLD & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQGLD) & Chr(9)
            strRetrieveParam = strRetrieveParam & strOpCode_Get_HQGLD & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQGLD) & "|" & _
                               FileLocation & strHQGLDFileName & Chr(9)
            strFileList = strFileList & strHQGLDFileName & "=Yes&"
        End If

        If strHQGLC = "True" Then
            strPopulateParam = strPopulateParam & strOpCode_Populate_HQGLC & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQGLC) & Chr(9)
            strRetrieveParam = strRetrieveParam & strOpCode_Get_HQGLC & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQGLC) & "|" & _
                               FileLocation & strHQGLCFileName & Chr(9)
            strFileList = strFileList & strHQGLCFileName & "=Yes&"
        End If

        If strHQXTD = "True" Then
            strPopulateParam = strPopulateParam & strOpCode_Populate_HQXTD & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQXTD) & Chr(9)
            strRetrieveParam = strRetrieveParam & strOpCode_Get_HQXTD & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQXTD) & "|" & _
                               FileLocation & strHQXTDFileName & Chr(9)
            strFileList = strFileList & strHQXTDFileName & "=Yes&"
        End If

        If strHQXTC = "True" Then
            strPopulateParam = strPopulateParam & strOpCode_Populate_HQXTC & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQXTC) & Chr(9)
            strRetrieveParam = strRetrieveParam & strOpCode_Get_HQXTC & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQXTC) & "|" & _
                               FileLocation & strHQXTCFileName & Chr(9)
            strFileList = strFileList & strHQXTCFileName & "=Yes&"
        End If

        If strHQXRD = "True" Then
            strPopulateParam = strPopulateParam & strOpCode_Populate_HQXRD & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQXRD) & Chr(9)
            strRetrieveParam = strRetrieveParam & strOpCode_Get_HQXRD & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQXRD) & "|" & _
                               FileLocation & strHQXRDFileName & Chr(9)
            strFileList = strFileList & strHQXRDFileName & "=Yes&"
        End If

        If strHQXRC = "True" Then
            strPopulateParam = strPopulateParam & strOpCode_Populate_HQXRC & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQXRC) & Chr(9)
            strRetrieveParam = strRetrieveParam & strOpCode_Get_HQXRC & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQXRC) & "|" & _
                               FileLocation & strHQXRCFileName & Chr(9)
            strFileList = strFileList & strHQXRCFileName & "=Yes&"
        End If

        If strHQOBD = "True" Then
            strPopulateParam = strPopulateParam & strOpCode_Populate_HQOBD & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQOBD) & Chr(9)
            strRetrieveParam = strRetrieveParam & strOpCode_Get_HQOBD & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQOBD) & "|" & _
                               FileLocation & strHQOBDFileName & Chr(9)
            strFileList = strFileList & strHQOBDFileName & "=Yes&"
        End If

        If strHQOBC = "True" Then
            strPopulateParam = strPopulateParam & strOpCode_Populate_HQOBC & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQOBC) & Chr(9)
            strRetrieveParam = strRetrieveParam & strOpCode_Get_HQOBC & "|" & _
                               objGLData.mtdGetDownloadTable(objGLData.EnumDownloadType.HQOBC) & "|" & _
                               FileLocation & strHQOBCFileName & Chr(9)
            strFileList = strFileList & strHQOBCFileName & "=Yes&"
        End If

        If strPopulateParam <> "" Then
            strPopulateParam = Mid(strPopulateParam, 1, Len(strPopulateParam) - 1)

            Try
                intErrNo = objGLData.mtdPopulateFlatFileTable(SelCompCode, _
                            SelCropCode, _
                            SelCurrency, _
                            SelStatus, _
                            strLocation, _
                            strUserId, _
                            SelAccMonth, _
                            SelAccYear, _
                            strIsBlock, _
                            strPopulateParam, _
                            objPopulateDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GLDATA_POPULATE_FLATFILE&errmesg=&redirect=")
            End Try
        End If

        If strRetrieveParam <> "" Then
            strRetrieveParam = Mid(strRetrieveParam, 1, Len(strRetrieveParam) - 1)

            Try 
                intErrNo = objGLData.mtdGetFlatFileData(SelCompCode, _
                                                            SelCropCode, _
                                                            SelCurrency, _
                                                            SelAccMonth, _
                                                            SelAccYear, _
                                                            strRetrieveParam, _
                                                            objDownloadDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GLDATA_GET_FLATFILE&errmesg=&redirect=")
            End Try
        End If












        strFileList = Mid(strFileList, 1, Len(strFileList) - 1)

        Response.Redirect("GL_Data_Download_FlatFile_SaveTo.aspx?" & strFileList)

    End Sub


End Class

