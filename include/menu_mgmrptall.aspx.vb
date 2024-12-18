
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

Imports agri.GlobalHdl
Imports agri.PWSystem.clsLangCap
Imports agri.PWSystem.clsConfig

Public Class menu_mgmrptall : Inherits Page


    Dim strUserId As String
    Dim strLangCode As String
    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intLocType As Integer
    Dim intModuleActivate As Integer
    Dim strLocTag As String

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objLangCapVw As New DataView


    Protected WithEvents lnkRpt1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRpt2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptFIGL As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptFIAP As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptFIAR As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptFICB As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptFIFA As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkRptPMNU As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptPMWS As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptPD As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkRptSDCM As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptSDWM As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptHRPR As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptHRHR As System.Web.UI.WebControls.HyperLink


    Protected WithEvents tlbRpt As System.Web.UI.HtmlControls.HtmlTable

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strUserId = Session("SS_USERID")

        intModuleActivate = Session("SS_MODULEACTIVATE")
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strLangCode = Session("SS_LANGCODE")
        intLocType = Session("SS_LOCTYPE")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")

        If strUserId = "" Then
            Response.Write("<Script language=""Javascript"">parent.right.location.href = '/Login.aspx'</Script>")
        Else

            onLoad_Display()

        End If

    End Sub

    Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then
            Call DisableAllHead()
        Else

            Call ReportsRight()

        End If

    End Sub

    Private Sub DisableAllHead()

        tlbRpt.Visible = False

    End Sub


    Private Sub ReportsRight()

        ''material
        lnkRpt1.NavigateUrl = "/" & strLangCode & "/reports/IN_StdRpt_Selection.aspx?UserLoc=" & strLocation
        lnkRpt2.NavigateUrl = "/" & strLangCode & "/reports/PU_StdRpt_Selection.aspx?UserLoc=" & strLocation

        ''financial
        lnkRptFIGL.NavigateUrl = "/" & strLangCode & "/reports/GL_StdRpt_Selection.aspx?UserLoc=" & strLocation
        lnkRptFIAP.NavigateUrl = "/" & strLangCode & "/reports/AP_StdRpt_Selection.aspx?UserLoc=" & strLocation
        lnkRptFIAR.NavigateUrl = "/" & strLangCode & "/reports/AR_StdRpt_Selection.aspx?UserLoc=" & strLocation
        lnkRptFICB.NavigateUrl = "/" & strLangCode & "/reports/CB_StdRpt_Selection.aspx?UserLoc=" & strLocation
        lnkRptFIFA.NavigateUrl = "/" & strLangCode & "/reports/FA_StdRpt_Selection.aspx?UserLoc=" & strLocation

        ''plant maintenance
        lnkRptPMNU.NavigateUrl = "/" & strLangCode & "/reports/NU_StdRpt_Selection.aspx?UserLoc=" & strLocation
        lnkRptPMWS.NavigateUrl = "/" & strLangCode & "/reports/WS_StdRpt_Selection.aspx?UserLoc=" & strLocation


        ''production
        If intLocType = 4 Then
            lnkRptPD.NavigateUrl = "/" & strLangCode & "/reports/PM_StdRpt_Selection.aspx?UserLoc=" & strLocation
        Else
            lnkRptPD.NavigateUrl = "/" & strLangCode & "/reports/PD_StdRpt_Selection.aspx?UserLoc=" & strLocation
        End If


        ''SALES & DISTRIBUTION
        lnkRptSDCM.NavigateUrl = "/" & strLangCode & "/reports/CM_StdRpt_Selection.aspx?UserLoc=" & strLocation
        lnkRptSDWM.NavigateUrl = "/" & strLangCode & "/reports/WM_StdRpt_Selection.aspx?UserLoc=" & strLocation

        ''HUMAN RESOURCES
        lnkRptHRHR.NavigateUrl = "/" & strLangCode & "/reports/HR_StdRpt_Selection_Estate.aspx?UserLoc=" & strLocation
        lnkRptHRPR.NavigateUrl = "/" & strLangCode & "/reports/PR_StdRpt_Selection_Estate.aspx?UserLoc=" & strLocation
    End Sub



End Class

