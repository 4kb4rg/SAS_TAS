
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

Public Class menu_pdstp : Inherits Page


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

    Dim intPDAR  As Integer
    Dim intPMAR  As Integer

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
    Protected WithEvents lnkPD14 As System.Web.UI.WebControls.HyperLink


    Protected WithEvents tlbStpPD As System.Web.UI.HtmlControls.HtmlTable



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

        	onLoad_Display()


        End If

    End Sub

   Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then

            Call DisableAllHead()

        Else
            
          
            Call StpPDRight()
         
         
        End If

    End Sub


  	Private Sub StpPDRight()


        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Production), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMill), intPMAR) = True) Then
                tlbStpPD.Rows(0).Visible = True
                lnkPD1.Text = "Mill"
            Else
                tlbStpPD.Rows(0).Visible = False
            End If


	   If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMasterSetup), intPMAR) = True) Then
                tlbStpPD.Rows(1).Visible = True
                lnkPD2.Text = "Ullage - Volume Table Master"
            Else
                tlbStpPD.Rows(1).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMVolConvMaster), intPMAR) = True) Then
                tlbStpPD.Rows(2).Visible = True
                lnkPD3.Text = "Ullage - Volume Conversion Master"
            Else
                tlbStpPD.Rows(2).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAvgCapConvMaster), intPMAR) = True) Then
                tlbStpPD.Rows(3).Visible = True
                lnkPD4.Text = "Ullage - Average Capacity Conversion Master"
            Else
                tlbStpPD.Rows(3).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMCPOPropertyMaster), intPMAR) = True) Then
                tlbStpPD.Rows(4).Visible = True
                lnkPD5.Text = "CPO Properties Master"
            Else
                tlbStpPD.Rows(4).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageTypeMaster), intPMAR) = True) Then
                tlbStpPD.Rows(5).Visible = True
                lnkPD6.Text = "Storage Type Master"
            Else
                tlbStpPD.Rows(5).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageAreaMaster), intPMAR) = True) Then
                tlbStpPD.Rows(6).Visible = True
                lnkPD7.Text = "Storage Area Master"
            Else
                tlbStpPD.Rows(6).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProcessingLineMaster), intPMAR) = True) Then
                tlbStpPD.Rows(7).Visible = True
                lnkPD8.Text = "Processing Line Master"
            Else
                tlbStpPD.Rows(7).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineMaster), intPMAR) = True) Then
                tlbStpPD.Rows(8).Visible = True
                lnkPD9.Text = "Machine Master"
            Else
                tlbStpPD.Rows(8).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableOilQuality), intPMAR) = True) Then
                tlbStpPD.Rows(9).Visible = True
                lnkPD10.Text = "Acceptable Oil Quality"
            Else
                tlbStpPD.Rows(9).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableKernelQuality), intPMAR) = True) Then
                tlbStpPD.Rows(10).Visible = True
                lnkPD11.Text = "Acceptable Kernel Quality"
            Else
                tlbStpPD.Rows(10).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMTestSample), intPMAR) = True) Then
                tlbStpPD.Rows(11).Visible = True
                lnkPD12.Text = "Test Sample"
            Else
                tlbStpPD.Rows(11).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMHarvestingInterval), intPMAR) = True) Then
                tlbStpPD.Rows(12).Visible = True
                lnkPD13.Text = "Harvesting Interval"
            Else
                tlbStpPD.Rows(12).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineCriteria), intPMAR) = True) Then
                tlbStpPD.Rows(13).Visible = True
                lnkPD14.Text = "Machine Criteria"
            Else
                tlbStpPD.Rows(13).Visible = False
            End If

        Else
         
            tlbStpPD.Visible = False
           
        End If

    End Sub


    


    Private Sub DisableAllHead()

        tlbStpPD.Visible = False
       
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


