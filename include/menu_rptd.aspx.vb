
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


Public Class menu_rptd : Inherits Page


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

    Dim intGLAR As Integer
    Dim intBIAR As Integer
    Dim intAPAR As Integer
    Dim intCBAR As Integer
    Dim intFAAR As Integer

    Const C_ADMIN = "ADMIN"

    Protected WithEvents lnkRptMM1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptMM2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptPM1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptPM2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptPD1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptSD1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptSD2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptFI1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptFI2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptFI3 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptFI4 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptFI5 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptHR1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRptHR2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents tblMM As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblPM As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblPD As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblSD As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblFI As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblHR As System.Web.UI.HtmlControls.HtmlTable


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
            intGLAR = Session("SS_GLAR")
            intBIAR = Session("SS_BIAR")
            intAPAR = Session("SS_APAR")
            intCBAR = Session("SS_CBAR")
            intFAAR = Session("SS_FAAR")
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
        tblMM.Visible = False
    End Sub


    Private Sub ReportsRight()
        '--material management
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Inventory), intModuleActivate) Then
            lnkRptMM1.NavigateUrl = "/" & strLangCode & "/reports/IN_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblMM.Rows(0).Visible = True
        Else
            lnkRptMM1.NavigateUrl = ""
            tblMM.Rows(0).Visible = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Purchasing), intModuleActivate) Then
            lnkRptMM2.NavigateUrl = "/" & strLangCode & "/reports/PU_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblMM.Rows(1).Visible = True
        Else
            lnkRptMM2.NavigateUrl = ""
            tblMM.Rows(1).Visible = False
        End If

        '--plant maintenance
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Nursery), intModuleActivate) Then
            lnkRptPM1.NavigateUrl = "/" & strLangCode & "/reports/NU_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblPM.Rows(0).Visible = True
        Else
            lnkRptPM1.NavigateUrl = ""
            tblPM.Rows(0).Visible = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Workshop), intModuleActivate) Then
            lnkRptPM2.NavigateUrl = "/" & strLangCode & "/reports/WS_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblPM.Rows(1).Visible = True
        Else
            lnkRptPM2.NavigateUrl = ""
            tblPM.Rows(1).Visible = False
        End If

        '--production
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Production), intModuleActivate) Then
            If intLocType = 4 Then
                lnkRptPD1.NavigateUrl = "/" & strLangCode & "/reports/PM_StdRpt_Selection.aspx?UserLoc=" & strLocation
            Else
                lnkRptPD1.NavigateUrl = "/" & strLangCode & "/reports/PD_StdRpt_Selection.aspx?UserLoc=" & strLocation
            End If
            tblPD.Rows(0).Visible = True
        Else
            lnkRptPD1.NavigateUrl = ""
            tblPD.Rows(0).Visible = False
        End If

        '--sales & distribution
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillContract), intModuleActivate) Then
            lnkRptSD1.NavigateUrl = "/" & strLangCode & "/reports/CM_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblSD.Rows(0).Visible = True
        Else
            lnkRptSD1.NavigateUrl = ""
            tblSD.Rows(0).Visible = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillWeighing), intModuleActivate) Then
            lnkRptSD2.NavigateUrl = "/" & strLangCode & "/reports/WM_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblSD.Rows(1).Visible = True
        Else
            lnkRptSD2.NavigateUrl = ""
            tblSD.Rows(1).Visible = False
        End If

        '--finance
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.GeneralLedger), intModuleActivate) Then
            lnkRptFI1.NavigateUrl = "/" & strLangCode & "/reports/GL_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblFI.Rows(0).Visible = True
        Else
            lnkRptFI1.NavigateUrl = ""
            tblFI.Rows(0).Visible = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.AccountPayable), intModuleActivate) Then
            lnkRptFI2.NavigateUrl = "/" & strLangCode & "/reports/AP_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblFI.Rows(1).Visible = True
        Else
            lnkRptFI2.NavigateUrl = ""
            tblFI.Rows(1).Visible = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Billing), intModuleActivate) Then
            lnkRptFI3.NavigateUrl = "/" & strLangCode & "/reports/AR_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblFI.Rows(2).Visible = True
        Else
            lnkRptFI3.NavigateUrl = ""
            tblFI.Rows(2).Visible = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.CashAndBank), intModuleActivate) Then
            lnkRptFI4.NavigateUrl = "/" & strLangCode & "/reports/CB_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblFI.Rows(3).Visible = True
        Else
            lnkRptFI4.NavigateUrl = ""
            tblFI.Rows(3).Visible = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.FixAsset), intModuleActivate) Then
            lnkRptFI5.NavigateUrl = "/" & strLangCode & "/reports/FA_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblFI.Rows(4).Visible = True
        Else
            lnkRptFI5.NavigateUrl = ""
            tblFI.Rows(4).Visible = False
        End If

        '--human resource & payroll
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.HumanResource), intModuleActivate) Then
            lnkRptHR1.NavigateUrl = "/" & strLangCode & "/reports/HR_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblHR.Rows(0).Visible = True
        Else
            lnkRptHR1.NavigateUrl = ""
            tblHR.Rows(0).Visible = False
        End If
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Payroll), intModuleActivate) Then
            lnkRptHR2.NavigateUrl = "/" & strLangCode & "/reports/PR_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tblHR.Rows(1).Visible = True
        Else
            lnkRptHR2.NavigateUrl = ""
            tblHR.Rows(1).Visible = False
        End If
    End Sub
End Class
