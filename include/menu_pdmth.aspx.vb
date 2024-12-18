
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

Public Class menu_pdmth : Inherits Page


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

    Dim intPMAR As Integer
    Dim intPDAR AS Integer

    Const C_ADMIN = "ADMIN"

   
    Protected WithEvents lnkMth1 As System.Web.UI.WebControls.HyperLink
   
   
    Protected WithEvents tblMth As System.Web.UI.HtmlControls.HtmlTable

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

	
        intPMAR = Session("SS_PMAR")
        intPDAR = Session("SS_PDAR")
      
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

        tblMth.Visible = False
      
    End Sub


    Private Sub MonthEndRight()

     
 	If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMonthEnd), intPMAR) = True) Or _
            (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMonthEnd), intPDAR) = True) Then
             
	    If intLocType = 4 Then
                lnkMth1.NavigateUrl = "/" & strLangCode & "/PM/mthend/PM_mthend_Process.aspx"
            Else
                lnkMth1.NavigateUrl = "/" & strLangCode & "/PD/mthend/PD_mthend_Process_Estate.aspx"
            End If

	     
             
	     tblMth.Rows(0).Visible = True
        Else
            lnkMth1.NavigateUrl = ""
            tblMth.Rows(0).Visible = False
        End If
       
        
 
    End Sub



End Class
