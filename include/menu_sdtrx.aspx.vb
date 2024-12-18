
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

Public Class menu_sdtrx : Inherits Page


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

    Const C_ADMIN = "ADMIN"

    Dim intCMAR As Integer
    Dim intWMAR As Integer
    Dim intINAR As Integer
    'Dim intPRAR As Long

    Protected WithEvents tblCMHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblCM As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblWMHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblWM As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblSpc1 As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tblSDHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblSD As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents lnkCM1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkCM2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkCM3 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkCM4 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkWM1 As System.Web.UI.WebControls.HyperLink
	Protected WithEvents lnkWM2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkWM3 As System.Web.UI.WebControls.HyperLink
	Protected WithEvents lnkWM4 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkSD1 As System.Web.UI.WebControls.HyperLink

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
            'intPRAR = Session("SS_WMAR")
            intWMAR = Session("SS_WMAR")
            intCMAR = Session("SS_CMAR")
            intINAR = Session("SS_INAR")
      
            'GetEntireLangCap()
            onLoad_Display()
        End If

    End Sub

   Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then
            Call DisableAllHead()
        Else
            
            'Call TrxWMRight()
            Call TrxCMRight()
            'Call TrxSDRight()
           
        End If

    End Sub


    Private Sub DisableAllHead()
        tblCMHead.Visible = False
        tblSpc1.Visible = False
        tblWMHead.Visible = False
        tblSDHead.Visible = False
    End Sub


    Private Sub TrxWMRight()
        Dim blnWM As Boolean
        blnWM = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillWeighing), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket), intWMAR) = True) Then
                tblWM.Rows(0).Visible = True
                blnWM = True
            Else
                tblWM.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMFFBAssessment), intWMAR) = True) Then
                tblWM.Rows(1).Visible = True
                blnWM = True
            Else
                tblWM.Rows(1).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMDataTransfer), intWMAR) = True) Then
                tblWM.Rows(2).Visible = True
                blnWM = True
            Else
                tblWM.Rows(2).Visible = False
            End If

            If blnWM = False Then
                tblWMHead.Visible = False
                tblWM.Visible = False
            End If

        Else
            tblWMHead.Visible = False
            tblWM.Visible = False
        End If
    End Sub

    Private Sub TrxCMRight()
        Dim blnCM As Boolean

        blnCM = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillContract), intModuleActivate) Then


            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractReg), intCMAR) = True) Then
                tblCM.Rows(0).Visible = True
                blnCM = True
            Else
                tblCM.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMContractMatching), intCMAR) = True) Then
                tblCM.Rows(1).Visible = True
                blnCM = True
            Else
                tblCM.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMDORegistration), intCMAR) = True) Then
                tblCM.Rows(2).Visible = True
                blnCM = True
            Else
                tblCM.Rows(2).Visible = False
            End If

            If blnCM = False Then
                tblCMHead.Visible = False
                tblCM.Visible = False
                tblSpc1.Visible = False
            End If

        Else
            tblCMHead.Visible = False
            tblCM.Visible = False
            tblSpc1.Visible = False
        End If
    End Sub

    Private Sub TrxWMRigh()

        Dim blnWM As Boolean
        blnWM = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillWeighing), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket), intWMAR) = True) Then
                tblWM.Rows(0).Visible = True
				tblWM.Rows(3).Visible = True
                blnWM = True
            Else
                tblWM.Rows(0).Visible = False
				tblWM.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMFFBAssessment), intWMAR) = True) Then
                tblWM.Rows(2).Visible = True
                blnWM = True
            Else
                tblWM.Rows(2).Visible = False
            End If

            If blnWM = False Then
                tblWMHead.Visible = False
                tblWM.Visible = False

            End If

        Else
            tblWMHead.Visible = False
            tblWM.Visible = False

        End If

    End Sub

    Private Sub TrxSDRight()

        Dim blnSD As Boolean
        blnSD = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Inventory), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue), intINAR) = True) Then
                tblWM.Rows(0).Visible = True
                blnSD = True
            Else
                tblSD.Rows(0).Visible = False
            End If

            If blnSD = False Then
                tblSDHead.Visible = False
                tblSD.Visible = False

            End If
        Else
            tblSDHead.Visible = False
            tblSD.Visible = False
        End If
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
