
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

Public Class menu_sdstp : Inherits Page


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

    Dim intCMAR  As Integer
    Dim intBIAR  As Integer
    Dim intWMAR  As Integer
    

    Const C_ADMIN = "ADMIN"

    Protected WithEvents lnkStpSD1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpSD2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpSD3 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpSD4 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpSD5 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpSD6 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents tlbStpSD As System.Web.UI.HtmlControls.HtmlTable
    

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

	
        	intCMAR = Session("SS_CMAR")
        	intBIAR = Session("SS_BIAR")
		intWMAR = Session("SS_WMAR")
      
        	'GetEntireLangCap()

        	onLoad_Display()


        End If

    End Sub

   Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then
            Call DisableAllHead()
        Else
            
          
            Call StpSDRight()
         
         
        End If

    End Sub


   Private Sub StpSDRight()

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMMasterSetup), intCMAR) = True) Then
            tlbStpSD.Rows(0).Visible = True
        Else
            tlbStpSD.Rows(0).Visible = False
        End If
            
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMExchangeRate), intCMAR) = True) Then
            tlbStpSD.Rows(1).Visible = True
        Else
            tlbStpSD.Rows(1).Visible = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractQuality), intCMAR) = True) Then
            tlbStpSD.Rows(2).Visible = True
        Else
            tlbStpSD.Rows(2).Visible = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMClaimQuality), intCMAR) = True) Then
            tlbStpSD.Rows(3).Visible = True
        Else
            tlbStpSD.Rows(3).Visible = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIBillParty), intBIAR) = True) Then
            tlbStpSD.Rows(4).Visible = True
        Else
            tlbStpSD.Rows(4).Visible = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMTransporterSetup), intWMAR) = True) Then
            tlbStpSD.Rows(5).Visible = True
        Else
            tlbStpSD.Rows(5).Visible = False
        End If

        
        
    End Sub


    Private Sub DisableAllHead()

        tlbStpSD.Visible = False
       
    End Sub


    Sub GetEntireLangCap()
        Dim objLangCapDs As New DataSet()
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

            objLangCapVw = New DataView(objLangCapDs.Tables(0))

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=MENU_FASETUP_GET_LANGCAP&errmesg=&redirect=")
        End Try

    End Sub


    Function GetCaption(ByVal pv_TermCode As Integer) As String

        Dim intIndex As Integer

        objLangCapVw.Sort = "TermCode"

        intIndex = objLangCapVw.Find(pv_TermCode)

        If intIndex = -1 Then
            Return ""
        Else
            If intLocType = 4 Then
                Return Trim(objLangCapVw(intIndex).Item("BusinessTermMW"))
            Else
                Return Trim(objLangCapVw(intIndex).Item("BusinessTerm"))
            End If
        End If

    End Function



End Class


