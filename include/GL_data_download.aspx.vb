Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsAccessRights

Public Class GL_data_download : Inherits Page

    Protected WithEvents tblDownload As HtmlTable
    Protected WithEvents tblSave As HtmlTable

    Protected WithEvents cbAccClsGrp As CheckBox
    Protected WithEvents cbAccCls As CheckBox
    Protected WithEvents cbActGrp As CheckBox
    Protected WithEvents cbAct As CheckBox
    Protected WithEvents cbSubAct As CheckBox
    Protected WithEvents cbExp As CheckBox
    Protected WithEvents cbVehExpGrp As CheckBox
    Protected WithEvents cbVehExp As CheckBox
    Protected WithEvents cbVehType As CheckBox
    Protected WithEvents cbVeh As CheckBox
    Protected WithEvents cbBlkGrp As CheckBox
    Protected WithEvents cbBlk As CheckBox
    Protected WithEvents cbSubBlk As CheckBox
    Protected WithEvents cbAccGrp As CheckBox
    Protected WithEvents cbAcc As CheckBox
    Protected WithEvents lblErrGenerate As Label
    Protected WithEvents lblDownloadfile As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lnkSaveTheFile As Hyperlink

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intGLAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intGLAR = Session("SS_GLAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLDataTransfer), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If Not Page.IsPostBack Then
                tblDownload.Visible = True
                tblSave.Visible = False
            Else
                tblDownload.Visible = True
                tblSave.Visible = False
            End If
        End If
    End Sub


    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strParam As String = ""
        Dim strQuery As String = ""

        If cbAccClsGrp.Checked Then strParam = strParam & "cbAccClsGrp"
        If cbAccCls.Checked Then strParam = strParam & "cbAccCls"
        If cbActGrp.Checked Then strParam = strParam & "cbActGrp"
        If cbAct.Checked Then strParam = strParam & "cbAct"
        If cbSubAct.Checked Then strParam = strParam & "cbSubAct"
        If cbExp.Checked Then strParam = strParam & "cbExp"
        If cbVehExpGrp.Checked Then strParam = strParam & "cbVehExpGrp"
        If cbVehExp.Checked Then strParam = strParam & "cbVehExp"
        If cbVehType.Checked Then strParam = strParam & "cbVehType"
        If cbVeh.Checked Then strParam = strParam & "cbVeh"
        If cbBlkGrp.Checked Then strParam = strParam & "cbBlkGrp"
        If cbBlk.Checked Then strParam = strParam & "cbBlk"
        If cbSubBlk.Checked Then strParam = strParam & "cbSubBlk"
        If cbAccGrp.Checked Then strParam = strParam & "cbAccGrp"
        If cbAcc.Checked Then strParam = strParam & "cbAcc"

        If strParam = "" Then
            lblErrGenerate.Visible = True
            Exit Sub
        Else
            strQuery = "accclsgrp=" & cbAccClsGrp.Checked & "&"
            strQuery += "acccls=" & cbAccCls.Checked & "&"
            strQuery += "actgrp=" & cbActGrp.Checked & "&"
            strQuery += "act=" & cbAct.Checked & "&"
            strQuery += "subact=" & cbSubAct.Checked & "&"
            strQuery += "exp=" & cbExp.Checked & "&"
            strQuery += "vehexpgrp=" & cbVehExpGrp.Checked & "&"
            strQuery += "vehexp=" & cbVehExp.Checked & "&"
            strQuery += "vehtype=" & cbVehType.Checked & "&"
            strQuery += "veh=" & cbVeh.Checked & "&"
            strQuery += "blkgrp=" & cbBlkGrp.Checked & "&"
            strQuery += "blk=" & cbBlk.Checked & "&"
            strQuery += "subblk=" & cbSubBlk.Checked & "&"
            strQuery += "accgrp=" & cbAccGrp.Checked & "&"
            strQuery += "acc=" & cbAcc.Checked

            Response.Redirect("GL_data_download_savefile.aspx?" & strQuery)
        End If
    End Sub


End Class
