
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

Public Class menu_pdtrx : Inherits Page


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

   
    Dim intPDAR As Integer
    Dim intPMAR As Integer

    Const C_ADMIN = "ADMIN"
 
    Protected WithEvents lnkPD1 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkPD2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPD3 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPD4 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPD5 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPD6 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPD7 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPD8 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPD9 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPD10 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPD11 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPD12 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkPD13 As System.Web.UI.WebControls.HyperLink
   
    Protected WithEvents tblProd As System.Web.UI.HtmlControls.HtmlTable
    
  

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


            intPDAR = Session("SS_PDAR")
            intPMAR = Session("SS_PMAR")

            'GetEntireLangCap()
            'onLoad_Display()


        End If

    End Sub

   Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then

            Call DisableAllHead()

        Else
            
            Call TrxPDRight()
           
        End If

    End Sub


    Private Sub DisableAllHead()

        'tblProd.Visible = False
       
    End Sub


     Private Sub TrxPDRight()

        Dim blnProd As Boolean
        Dim blnIsProd As Boolean
        blnIsProd = False

        'If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Production), intModuleActivate) Then
        '    blnProd = True
        'End If
        'If Session("PW_ISMILLWARE") = True And _
        '    objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillProduction), intModuleActivate) Then
        '    blnProd = True
        'End If

        'If blnProd = True Then

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDEstateProduction), intPDAR) = True) Then
            tblProd.Rows(0).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(0).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction), intPMAR) = True) Then
            tblProd.Rows(1).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(1).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilLoss), intPMAR) = True) Then
            tblProd.Rows(2).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(2).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilQuality), intPMAR) = True) Then
            tblProd.Rows(3).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(3).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelQuality), intPMAR) = True) Then
            tblProd.Rows(4).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(4).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProducedKernel), intPMAR) = True) Then
            tblProd.Rows(5).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(5).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDispatchedKernel), intPMAR) = True) Then
            tblProd.Rows(6).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(6).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOStorage), intPMAR) = True) Then
            tblProd.Rows(7).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(7).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMPKStorage), intPMAR) = True) Then
            tblProd.Rows(8).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(8).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelLoss), intPMAR) = True) Then
            tblProd.Rows(9).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(9).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWaterQuality), intPMAR) = True) Then
            tblProd.Rows(10).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(10).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWastedWaterQuality), intPMAR) = True) Then
            tblProd.Rows(11).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(11).Visible = False
        End If
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction), intPMAR) = True) Then
            tblProd.Rows(12).Visible = True
            blnIsProd = True
        Else
            tblProd.Rows(12).Visible = False
        End If


        If blnIsProd = False Then
            tblProd.Visible = False
        End If

        'Else

        '    tblProd.Visible = False
           
        'End If

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
