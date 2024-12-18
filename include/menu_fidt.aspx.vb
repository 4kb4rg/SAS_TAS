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

Public Class menu_fidt : Inherits Page
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


    Protected WithEvents lnkDT1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkDT2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkDT3 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkDT5 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents LnkDT7 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents LnkDT8 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents LnkDT9 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents tlbDT As System.Web.UI.HtmlControls.HtmlTable

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
        tlbDT.Visible = False
    End Sub

    Private Sub ReportsRight()
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.CashAndBank), intModuleActivate) Then
            lnkDT1.NavigateUrl = "/" & strLangCode & "/Data_Upload.aspx?UserLoc=" & strLocation
            tlbDT.Rows(0).Visible = True
        Else
            lnkDT1.NavigateUrl = ""
            tlbDT.Rows(0).Visible = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.CashAndBank), intModuleActivate) Then
            lnkDT2.NavigateUrl = "/" & strLangCode & "/Data_Download.aspx?UserLoc=" & strLocation
            tlbDT.Rows(1).Visible = True
        Else
            lnkDT2.NavigateUrl = ""
            tlbDT.Rows(1).Visible = False
        End If
		
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.CashAndBank), intModuleActivate) Then
            lnkDT3.NavigateUrl = "/" & strLangCode & "/Data_Upload_Budget.aspx?UserLoc=" & strLocation
            tlbDT.Rows(2).Visible = True
        Else
            lnkDT3.NavigateUrl = ""
            tlbDT.Rows(2).Visible = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.CashAndBank), intModuleActivate) Then
            lnkDT5.NavigateUrl = "/" & strLangCode & "/Data_download_Budget.aspx?UserLoc=" & strLocation
            tlbDT.Rows(3).Visible = True
        Else
            lnkDT5.NavigateUrl = ""
            tlbDT.Rows(3).Visible = False
        End If

        'If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.CashAndBank), intModuleActivate) Then
        LnkDT7.NavigateUrl = "/" & strLangCode & "/Data_Upload_Monthly.aspx?UserLoc=" & strLocation
        '    tlbDT.Rows(4).Visible = True
        'Else
        '    LnkDT7.NavigateUrl = ""
        '    tlbDT.Rows(4).Visible = False
        'End If
		
        LnkDT8.NavigateUrl = "/" & strLangCode & "/Data_Unit_Download.aspx?UserLoc=" & strLocation
        LnkDT9.NavigateUrl = "/" & strLangCode & "/Data_Unit_Upload.aspx?UserLoc=" & strLocation

    End Sub

End Class
