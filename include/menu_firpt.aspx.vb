
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


Public Class menu_firpt : Inherits Page
  

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

   
    Protected WithEvents lnkRpt1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRpt2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRpt3 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkRpt4 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkRpt5 As System.Web.UI.WebControls.HyperLink

   
    Protected WithEvents tlbrpt As System.Web.UI.HtmlControls.HtmlTable

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

        tlbRpt.Visible = False
      
    End Sub


  
    Private Sub ReportsRight()


       If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.GeneralLedger), intModuleActivate) Then
            lnkRpt1.NavigateUrl = "/" & strLangCode & "/reports/GL_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tlbRpt.Rows(0).Visible = True
        Else
            lnkRpt1.NavigateUrl = ""
            tlbRpt.Rows(0).Visible = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.AccountPayable), intModuleActivate) Then
            lnkRpt2.NavigateUrl = "/" & strLangCode & "/reports/AP_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tlbrpt.Rows(1).Visible = True
        Else
            lnkRpt2.NavigateUrl = ""
            tlbrpt.Rows(1).Visible = False
        End If

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Billing), intModuleActivate) Then
            lnkRpt3.NavigateUrl = "/" & strLangCode & "/reports/AR_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tlbRpt.Rows(2).Visible = True
        Else
            lnkRpt3.NavigateUrl = ""
            tlbRpt.Rows(2).Visible = False
        End If
      
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.CashAndBank), intModuleActivate) Then
            lnkRpt4.NavigateUrl = "/" & strLangCode & "/reports/CB_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tlbRpt.Rows(3).Visible = True
        Else
            lnkRpt4.NavigateUrl = ""
            tlbRpt.Rows(3).Visible = False
        End If
        
        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.FixAsset), intModuleActivate) Then
            lnkRpt5.NavigateUrl = "/" & strLangCode & "/reports/FA_StdRpt_Selection.aspx?UserLoc=" & strLocation
            tlbRpt.Rows(4).Visible = True
        Else
            lnkRpt5.NavigateUrl = ""
            tlbRpt.Rows(4).Visible = False
        End If

        
    End Sub




End Class
