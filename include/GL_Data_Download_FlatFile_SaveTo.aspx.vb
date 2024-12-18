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


Public Class GL_Data_Download_FlatFile_SaveTo : Inherits Page

    Protected WithEvents lbHQORD As LinkButton
    Protected WithEvents lbHQORC As LinkButton
    Protected WithEvents lbHQGLD As LinkButton
    Protected WithEvents lbHQGLC As LinkButton
    Protected WithEvents lbHQXTD As LinkButton
    Protected WithEvents lbHQXTC As LinkButton
    Protected WithEvents lbHQXRD As LinkButton
    Protected WithEvents lbHQXRC As LinkButton
    Protected WithEvents lbHQOBD As LinkButton
    Protected WithEvents lbHQOBC As LinkButton

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

        Dim strHQORD As String = Request.QueryString("HQORD.DAT")
        Dim strHQORC As String = Request.QueryString("HQORC.DAT")
        Dim strHQGLD As String = Request.QueryString("HQGLD.DAT")
        Dim strHQGLC As String = Request.QueryString("HQGLC.DAT")
        Dim strHQXTD As String = Request.QueryString("HQXTD.DAT")
        Dim strHQXTC As String = Request.QueryString("HQXTC.DAT")
        Dim strHQXRD As String = Request.QueryString("HQXRD.DAT")
        Dim strHQXRC As String = Request.QueryString("HQXRC.DAT")
        Dim strHQOBD As String = Request.QueryString("HQOBD.DAT")
        Dim strHQOBC As String = Request.QueryString("HQOBC.DAT")

        lbHQORD.Enabled = False
        lbHQORC.Enabled = False
        lbHQGLD.Enabled = False
        lbHQGLC.Enabled = False
        lbHQXTD.Enabled = False
        lbHQXTC.Enabled = False
        lbHQXRD.Enabled = False
        lbHQXRC.Enabled = False
        lbHQOBD.Enabled = False
        lbHQOBC.Enabled = False

        lbHQORD.Visible = False
        lbHQORC.Visible = False
        lbHQGLD.Visible = False
        lbHQGLC.Visible = False
        lbHQXTD.Visible = False
        lbHQXTC.Visible = False
        lbHQXRD.Visible = False
        lbHQXRC.Visible = False
        lbHQOBD.Visible = False
        lbHQOBC.Visible = False

        If strHQORD = "Yes" Then
            lbHQORD.Enabled = True
            lbHQORD.Visible = True
        End If

        If strHQORC = "Yes" Then
            lbHQORC.Enabled = True
            lbHQORC.Visible = True
        End If

        If strHQGLD = "Yes" Then
            lbHQGLD.Enabled = True
            lbHQGLD.Visible = True
        End If

        If strHQGLC = "Yes" Then
            lbHQGLC.Enabled = True
            lbHQGLC.Visible = True
        End If

        If strHQXTD = "Yes" Then
            lbHQXTD.Enabled = True
            lbHQXTD.Visible = True
        End If

        If strHQXTC = "Yes" Then
            lbHQXTC.Enabled = True
            lbHQXTC.Visible = True
        End If

        If strHQXRD = "Yes" Then
            lbHQXRD.Enabled = True
            lbHQXRD.Visible = True
        End If

        If strHQXRC = "Yes" Then
            lbHQXRC.Enabled = True
            lbHQXRC.Visible = True
        End If

        If strHQOBD = "Yes" Then
            lbHQOBD.Enabled = True
            lbHQOBD.Visible = True
        End If

        If strHQOBC = "Yes" Then
            lbHQOBC.Enabled = True
            lbHQOBC.Visible = True
        End If

    End Sub


End Class

