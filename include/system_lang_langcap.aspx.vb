

Imports System
Imports System.Data
Imports System.Collections
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Interaction
Imports System.Drawing.Color

Imports agri.PWSystem
Imports agri.GlobalHdl.clsGlobalHdl

Public Class system_lang_langcap : inherits page

    Protected WithEvents ddlLanguage As DropDownList
    Protected WithEvents btnLoad As Button
    Protected WithEvents dgBussTerm As DataGrid
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents ldTermCode As Label

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String 
    Dim strAccYear As String 
    Dim intADAR As Integer

    Dim objConfig As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLanguageDS As New Dataset()
    Dim objBussTermDS As New Dataset()

    Public Sub New()
    End Sub

    Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intADAR = Session("SS_ADAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADLanguageCaption), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If Not IsPostBack Then
                onload_display()
            End If
        End If
    End Sub

    Sub onload_display()
        Dim strOpCd_Language AS String = "PWSYSTEM_CLSCONFIG_LANGUAGE_GET"
        Dim strOpCd_BussTerm AS String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim intErrNo As Integer
        Dim strErrHeader As String
        Dim strParam As String = ""
        Dim intCnt As Integer
        
        strErrHeader = "objConfig.mtdGetLanguage"

        Try
            intErrNo = objConfig.mtdGetLanguage(strOpCd_Language, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                objLanguageDS, _
                                                strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=LANGCAP_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=system/langcap/language_cap.aspx")
        End Try
                                       
        ddlLanguage.DataSource = objLanguageDS.Tables(0)
        ddlLanguage.DataTextField = "LangDesc"
        ddlLanguage.DataValueField = "LangCode"
        ddlLanguage.DataBind()

        strErrHeader = "objLangCap.mtdGetBussTerm"
        strParam = ddlLanguage.SelectedItem.Value.Trim()

        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCd_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objBussTermDS, _
                                                 strParam)
                                    
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=LANGCAP_GET_BUSSTERM&errmesg=" & lblErrMessage.Text & "&redirect=system/langcap/language_cap.aspx")
        End Try

        dgBussTerm.DataSource = objBussTermDS.Tables(0)
        dgBussTerm.DataBind()
    End Sub

    Sub IndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strOpCd_BussTerm AS String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim intErrNo As Integer
        Dim strErrHeader As String
        Dim strParam As String = ""
        Dim intCnt As Integer
        
        strErrHeader = "objLangCap.mtdGetBussTerm"
        strParam = ddlLanguage.SelectedItem.Value.Trim()
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCd_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objBussTermDS, _
                                                 strParam)
                                    
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=LANGCAP_GET_BUSSTERM&errmesg=" & lblErrMessage.Text & "&redirect=system/langcap/language_cap.aspx")
        End Try

        dgBussTerm.DataSource = objBussTermDS.Tables(0)
        dgBussTerm.DataBind()
    End Sub 

    Sub btnUpdate_click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCd_BussTerm AS String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_UPD"
        Dim intErrNo As Integer
        Dim strErrHeader As String
        Dim txtBussTerm As TextBox
        Dim strParam As String = ""
        Dim intCnt As Integer
        Dim _item As DataGridItem
        Dim lbTermCode As Label
        Dim txtBussTermMW As TextBox            
        
        For intCnt = 0 To dgBussTerm.Items.Count - 1
            _item = dgBussTerm.Items(intCnt)
            txtBussTerm = _item.FindControl("txtBussTerm")
            lbTermCode = _item.FindControl("lbTermCode")
            txtBussTermMW = _item.FindControl("txtBussTermMW")

            strParam = strParam & _
                       ddlLanguage.SelectedItem.value.trim() & _
                       "|" & lbTermCode.text.trim() & _
                       "|" & txtBussTerm.text.trim() & _
                       "|" & txtBussTermMW.text.trim() & chr(9)

        Next

        If strParam <> "" Then
            strErrHeader = "objLangCap.mtdUpdBussTerm"
            strParam = Mid(strParam, 1, Len(strParam) - 1)

            Try                
                intErrNo = objLangCap.mtdUpdBussTerm(strOpCd_BussTerm, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     objBussTermDS, _
                                                     strParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=LANGCAP_UPD_BUSSTERM&errmesg=" & lblErrMessage.Text & "&redirect=system/langcap/language_cap.aspx")
            End Try
        End If
    End Sub



End Class
