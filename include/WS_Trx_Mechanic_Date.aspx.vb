Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports agri.PWSystem.clsLangCap


Public Class WS_MechanicDate : Inherits Page

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCapDs As New Dataset()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim strLocType as String


    Protected WithEvents Cal As Calendar
    Protected WithEvents lblCompName As Label
    Protected WithEvents lblLocName As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCompany As Label

    Protected Sub Page_Load(ByVal Sender As System.Object, _
                            ByVal e As System.EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMechanicHour), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        End If

        lblCompName.Text = strCompany
        lblLocName.Text = strLocation
        onload_GetLangCap()

    End Sub


    Public Sub Cal_SelectionChanged(ByVal sender As Object, _
                                            ByVal e As System.EventArgs)
        Dim d As Date = Cal.SelectedDate()
        Response.Redirect("WS_Trx_Mechanic_List.aspx?dt=" & d)
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As EventArgs)
        Response.Redirect("..\..\menu\menu_WSTrx_page.aspx")
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblCompany.text = GetCaption(objLangCap.EnumLangCap.Company)
        lblLocation.text = GetCaption(objLangCap.EnumLangCap.Location)
   
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode

        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_TRXF_MECHANICDATE_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=ws/trx/ws_trx_mechanic_date.aspx")
        End Try

    End Sub

        

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function



End Class
