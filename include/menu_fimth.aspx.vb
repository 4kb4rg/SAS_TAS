
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

Public Class menu_fimth : Inherits Page


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

   
    Protected WithEvents lnkMth1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkMth2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkMth3 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkMth4 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkMth5 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkMth6 As System.Web.UI.WebControls.HyperLink


    Protected WithEvents tlbMth As System.Web.UI.HtmlControls.HtmlTable

   

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
          
            Call MonthEndRight()
        
        End If

    End Sub

   Private Sub DisableAllHead()

        tlbMth.Visible = False
      
    End Sub


    Private Sub MonthEndRight()

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMthEndGCDist), intGLAR) = True) Or _
           (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJrnMonthEnd), intGLAR) = True) Or _
           (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = True) Then

            lnkMth1.NavigateUrl = "/" & strLangCode & "/GL/mthend/GL_DayEnd_Process.aspx"

            tlbMth.Rows(0).Visible = True
          

        Else

            lnkMth1.NavigateUrl = ""
            tlbMth.Rows(0).Visible = True

        End If


         If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APMonthEnd), intAPAR) = True) Then
            lnkMth2.NavigateUrl = "/" & strLangCode & "/AP/mthend/AP_MthEnd_Process.aspx"
            tlbMth.Rows(1).Visible = False
           
        Else
            lnkMth2.NavigateUrl = ""
            tlbMth.Rows(1).Visible = False
        End If


        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIMonthEnd), intBIAR) = True) Then
            lnkMth3.NavigateUrl = "/" & strLangCode & "/BI/mthend/BI_MthEnd_Process.aspx"
            tlbMth.Rows(2).Visible = False
            
        Else
            lnkMth3.NavigateUrl = ""
            tlbMth.Rows(2).Visible = False
        End If
      

        
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCBAccessRights.CBMthEnd), intCBAR) = True) Then
            lnkMth4.NavigateUrl = "/" & strLangCode & "/CB/mthend/CB_MthEnd_Process.aspx"
            tlbMth.Rows(3).Visible = False
           
        Else
            lnkMth4.NavigateUrl = ""
            tlbMth.Rows(3).Visible = False
        End If


        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGenDepreciation), intFAAR) = True) Or _
            (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMonthEnd), intFAAR) = True) Then
            'lnkMth5.NavigateUrl = "/" & strLangCode & "/menu/menu_FAMthEnd_page.aspx"
            lnkMth5.NavigateUrl = "/" & strLangCode & "/FA/mthend/FA_mthend_Process.aspx"
            tlbMth.Rows(4).Visible = True
           
        Else
            lnkMth5.NavigateUrl = ""
            tlbMth.Rows(4).Visible = False
        End If



        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMthEndGCDist), intGLAR) = True) Or
           (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJrnMonthEnd), intGLAR) = True) Or
           (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = True) Then

            lnkMth6.NavigateUrl = "/" & strLangCode & "/GL/mthend/GL_Financial_Statement.aspx"

            tlbMth.Rows(5).Visible = True

        Else
            lnkMth6.NavigateUrl = ""
            tlbMth.Rows(5).Visible = False
        End If

    End Sub



End Class
