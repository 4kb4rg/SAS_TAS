
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

Public Class menu_pmtrx : Inherits Page


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
    Dim intPRAR As Long
    Dim intWSAR As Integer
    Dim intNUAR As Integer
   
   
    Protected WithEvents tblUPHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblUP As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents lnkUP01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkUP02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkUP03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkUP04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkUP05 As System.Web.UI.WebControls.HyperLink


    Protected WithEvents tblSpc1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblNUHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblNU As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents lnkNU01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkNU02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkNU03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkNU04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkNU05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkNU06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkNU07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkNU08 As System.Web.UI.WebControls.HyperLink 

    Protected WithEvents tblSpc2 As System.Web.UI.HtmlControls.HtmlTable
   
    Protected WithEvents tblWSHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblWS As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents lnkWS01 As System.Web.UI.WebControls.HyperLink 
    Protected WithEvents lnkWS02 As System.Web.UI.WebControls.HyperLink 


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
        	intPRAR = Session("SS_PRAR")
        	intWSAR = Session("SS_WSAR")
        	intNUAR = Session("SS_NUAR")

        	'GetEntireLangCap()
        	onLoad_Display()

        End If

    End Sub


   Sub onLoad_Display()
        
        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then

            Call DisableAllHead()

        Else
            
            Call TrxUPRight()
            Call TrxNURight()
            Call TrxWSRight()

        End If

    End Sub


    Private Sub DisableAllHead()

        tblUPHead.Visible = False
        tblSpc1.Visible = False
        tblNUHead.Visible = False
        tblSpc2.Visible = False
        tblWSHead.Visible = False
       
    End Sub


    Private Sub TrxUPRight()

        Dim blnUP As Boolean
        blnUP = False

        
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = True) Then
            tblUP.Rows(0).Visible = True
            blnUP = True
        Else
            tblUP.Rows(0).Visible = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWPContractor), intPRAR) = True) Then
             tblUP.Rows(1).Visible = True
             blnUP = True
        Else
             tblUP.Rows(1).Visible = False
        End If
      
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleUsage), intGLAR) = True) Then
            tblUP.Rows(2).Visible = True
            blnUP = True
            'If intLocType = objAdminLoc.EnumLocType.Mill Then
            tblUP.Rows(3).Visible = True
            'Else
            'tblUP.Rows(3).Visible = False
            'End If

            If intLocType = objAdminLoc.EnumLocType.Mill Then
                tblUP.Rows(4).Visible = True
            Else
                tblUP.Rows(4).Visible = False
            End If

        Else
            tblUP.Rows(2).Visible = False
            'If intLocType = objAdminLoc.EnumLocType.Mill Then
            '    tblUP.Rows(3).Visible = True
            'Else
            tblUP.Rows(3).Visible = False
            'End If

            If intLocType = objAdminLoc.EnumLocType.Mill Then
                tblUP.Rows(4).Visible = True
            Else
                tblUP.Rows(4).Visible = False
            End If

        End If

        If blnUP = False Then
            tblUPHead.Visible = False
            tblUP.Visible = False
            tblSpc1.Visible = False
        End If

    End Sub

    Private Sub TrxNURight()

        Dim blnNU As Boolean
        blnNU = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Nursery), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUPurchaseRequest), intNUAR) = True) Then
                tblNU.Rows(0).Visible = False
                blnNU = True
            Else
                tblNU.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedRcv), intNUAR) = True) Then
                tblNU.Rows(1).Visible = True
                blnNU = True
            Else
                tblNU.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedPlant), intNUAR) = True) Then
                tblNU.Rows(2).Visible = True
                blnNU = True
            Else
                tblNU.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDblTurn), intNUAR) = True) Then
                tblNU.Rows(3).Visible = True
                blnNU = True
            Else
                tblNU.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUTransplanting), intNUAR) = True) Then
                tblNU.Rows(4).Visible = True
                blnNU = True
            Else
                tblNU.Rows(4).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCulling), intNUAR) = True) Then
                tblNU.Rows(5).Visible = True
                blnNU = True
            Else
                tblNU.Rows(5).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch), intNUAR) = True) Then
                tblNU.Rows(6).Visible = True
                blnNU = True
            Else
                tblNU.Rows(6).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedIssue), intNUAR) = True) Then
                tblNU.Rows(7).Visible = True
                blnNU = True
            Else
                tblNU.Rows(7).Visible = False
            End If

            If blnNU = False Then
                tblNUHead.Visible = False
                tblNU.Visible = False
                tblSpc2.Visible = False
            End If

        Else
            tblNUHead.Visible = False
            tblNU.Visible = False
            tblSpc2.Visible = False
        End If
    End Sub

     Private Sub TrxWSRight()

        Dim blnWS As Boolean
        blnWS = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Workshop), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration), intWSAR) = True) Then
                tblWS.Rows(0).Visible = True
                blnWS = True
            Else
                tblWS.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMechanicHour), intWSAR) = True) Then
                tblWS.Rows(1).Visible = True
                blnWS = True
            Else
                tblWS.Rows(1).Visible = False
            End If

            If blnWS = False Then
                tblWSHead.Visible = False
                tblWS.Visible = False
            End If

        Else
            tblWSHead.Visible = False
            tblWS.Visible = False
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
