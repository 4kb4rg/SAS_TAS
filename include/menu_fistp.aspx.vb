
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

Public Class menu_fistp : Inherits Page


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
    Dim intPUAR As Integer
    Dim intHRAR As Long
    Dim intTXAR As Long

    Const C_ADMIN = "ADMIN"
   
 
    Protected WithEvents lnkStpFI01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI08 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI09 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI10 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI11 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI12 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI13 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI14 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI15 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI16 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI17 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI18 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI19 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI20 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI21 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFI22 As System.Web.UI.WebControls.HyperLink


    Protected WithEvents lnkStpAP01 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkStpAR01 As System.Web.UI.WebControls.HyperLink


    Protected WithEvents lnkStpCB01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpCB02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpCB03 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents lnkStpFA01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFA02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFA03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFA04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFA05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFA06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpFA07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpTX01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpTX02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpTX03 As System.Web.UI.WebControls.HyperLink


    Protected WithEvents tlbStpFA As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpFAHead As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tlbSpc4 As System.Web.UI.HtmlControls.HtmlTable
	
    Protected WithEvents tlbStpCB As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpCBHead As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tlbSpc3 As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tlbStpAR As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpARHead As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tlbSpc2 As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tlbStpAP As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpAPHead As System.Web.UI.HtmlControls.HtmlTable
   
    Protected WithEvents tlbSpc1 As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tlbStpFI As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpFIHead As System.Web.UI.HtmlControls.HtmlTable
    
    Protected WithEvents tblSpc5 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblTXHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblTX As System.Web.UI.HtmlControls.HtmlTable

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
           intPUAR = Session("SS_PUAR")
           intHRAR = Session("SS_HRAR")
            'tax dari canteen
            intTXAR = Session("SS_CTAR")

      
           GetEntireLangCap()

           onLoad_Display()

        End If

    End Sub

   Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then

            Call DisableAllHead()

	    
                tlbStpFI.Rows(21).Visible = True
           

		tlbStpFI.Rows(0).Visible = False
		tlbStpFI.Rows(1).Visible = False
		tlbStpFI.Rows(2).Visible = False
		tlbStpFI.Rows(3).Visible = False
		tlbStpFI.Rows(4).Visible = False
		tlbStpFI.Rows(5).Visible = False
		tlbStpFI.Rows(6).Visible = False
		tlbStpFI.Rows(7).Visible = False
		tlbStpFI.Rows(8).Visible = False
		tlbStpFI.Rows(9).Visible = False
		tlbStpFI.Rows(10).Visible = False
		tlbStpFI.Rows(11).Visible = False
		tlbStpFI.Rows(12).Visible = False
		tlbStpFI.Rows(13).Visible = False
		tlbStpFI.Rows(14).Visible = False
		tlbStpFI.Rows(15).Visible = False
		tlbStpFI.Rows(16).Visible = False
		tlbStpFI.Rows(17).Visible = False
		tlbStpFI.Rows(18).Visible = False
		tlbStpFI.Rows(19).Visible = False
                tlbStpFI.Rows(20).Visible = False
        Else
            
          
            Call StpGLRight()
            Call stpARRight()
            Call StpAPRight()
            Call StpCBRight()
            Call StpFARight()
            Call TrxTXRight()

           
        End If

    End Sub


   'dari purchasing
   Private Sub stpAPRight()

       
      If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier), intPUAR) = True) Then
           tlbStpAPHead.Visible = True
           tlbStpAP.visible = True
      Else
           tlbStpAPHead.Visible = False
           tlbStpAP.visible = False
      End If

           
   End Sub


   Private Sub StpARRight()

      If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIBillParty), intBIAR) = True) Then
           tlbStpARHead.Visible = True
           tlbStpAR.visible = True
      Else
           tlbStpARHead.Visible = False
           tlbStpAR.visible = False

      End If

   End Sub


   Private Sub StpCBRight()
       Dim blnCB As Boolean

       blnCB = False

      
       If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRBank), intHRAR) = True) Then
             tlbStpCB.Rows(0).Visible = True
             blnCB = True
       Else
             tlbStpCB.Rows(0).Visible = False
       End If


       If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier), intPUAR) = True) Then
	   tlbStpCB.Rows(1).Visible = True
           blnCB = True
       Else
           tlbStpCB.Rows(1).Visible = False
       End If

       If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumBIAccessRights.BIBillParty), intBIAR) = True) Then
	  tlbStpCB.Rows(2).Visible = True
          blnCB = True
       Else
          tlbStpCB.Rows(2).Visible = False
       End If


       If blnCB = False Then

          tlbStpCBHead.Visible = False
          tlbStpCB.Visible = False
          tlbSpc4.Visible = False

       End If


   End Sub


   
   Private Sub StpFARight()

        Dim strAssetClassTag As String
        Dim strAssetGrpTag As String
        Dim strAssetRegTag As String
        Dim strAssetReglnTag As String
        Dim strAssetPermitTag As String
        Dim strAssetMasterTag As String
        Dim strAssetItemTag As String

        Dim blnFAS  As Boolean
        blnFAS  = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.FixAsset), intModuleActivate) Then

            strAssetClassTag = GetCaption(objLangCap.EnumLangCap.AssetClass)
            strAssetGrpTag = GetCaption(objLangCap.EnumLangCap.AssetGrp)
            strAssetRegTag = GetCaption(objLangCap.EnumLangCap.AssetRegHeader)
            strAssetReglnTag = GetCaption(objLangCap.EnumLangCap.AssetRegLine)
            strAssetPermitTag = GetCaption(objLangCap.EnumLangCap.AssetPermit)
            strAssetMasterTag = GetCaption(objLangCap.EnumLangCap.AssetMaster)
            strAssetItemTag = GetCaption(objLangCap.EnumLangCap.AssetItem)

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAClassSetup), intFAAR) = True) Then
                tlbStpFA.Rows(0).Visible = True
                lnkStpFA01.Text = strAssetClassTag
                blnFAS = True
            Else
                tlbStpFA.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAGroupSetup), intFAAR) = True) Then
                tlbStpFA.Rows(1).Visible = True
                lnkStpFA02.Text = strAssetGrpTag
                blnFAS = True
            Else
                tlbStpFA.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegistrationSetup), intFAAR) = True) Then
                tlbStpFA.Rows(2).Visible = True
                lnkStpFA03.Text = strAssetRegTag
                blnFAS = True
            Else
                tlbStpFA.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegLine), intFAAR) = True) Then
                tlbStpFA.Rows(3).Visible = True
                lnkStpFA04.Text = strAssetReglnTag
                blnFAS = True
            Else
                tlbStpFA.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAPermissionSetup), intFAAR) = True) Then
                tlbStpFA.Rows(4).Visible = True
                lnkStpFA05.Text = strAssetPermitTag
                blnFAS = True
            Else
                tlbStpFA.Rows(4).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAMaster), intFAAR) = True) Then
                tlbStpFA.Rows(5).Visible = True
                lnkStpFA06.Text = strAssetMasterTag
                blnFAS = True
            Else
                tlbStpFA.Rows(5).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAItem), intFAAR) = True) Then
                tlbStpFA.Rows(6).Visible = True
                lnkStpFA07.Text = strAssetItemTag
                blnFAS = True
            Else
                tlbStpFA.Rows(6).Visible = False
            End If

            If blnFAS = False Then
                tlbStpFAHead.Visible = False
                tlbStpFA.Visible = False
            End If

        Else
            tlbStpFAHead.Visible = False
            tlbStpFA.Visible = False
        End If

    End Sub




   Private Sub StpGLRight()

        Dim strAccClsGrpTag As String
        Dim strAccClsTag As String
        Dim strActGrpTag As String
        Dim strActTag As String
        Dim strSubActTag As String
        Dim strExpenseCodeTag As String
        Dim strVehExpGrpCodeTag As String
        Dim strVehExpCodeTag As String
        Dim strVehTypeTag As String
        Dim strVehicleTag As String
        Dim strBlockGrpTag As String
        Dim strBlockTag As String
        Dim strSubBlockTag As String
        Dim strAccGrpTag As String
        Dim strAccountTag As String

        Dim blnGLS As Boolean
        blnGLS  = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.GeneralLedger), intModuleActivate) Then

            strAccClsGrpTag = GetCaption(objLangCap.EnumLangCap.AccClsGrp)
            strAccClsTag = GetCaption(objLangCap.EnumLangCap.AccClass)
            strActGrpTag = GetCaption(objLangCap.EnumLangCap.ActGrp)
            strActTag = GetCaption(objLangCap.EnumLangCap.Activity)
            strSubActTag = GetCaption(objLangCap.EnumLangCap.SubAct)
            strExpenseCodeTag = GetCaption(objLangCap.EnumLangCap.Expense) & " Code"
            strVehExpGrpCodeTag = GetCaption(objLangCap.EnumLangCap.VehExpGrp) & " Code"
            strVehExpCodeTag = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code"
            strVehTypeTag = GetCaption(objLangCap.EnumLangCap.VehType)
            strVehicleTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
            strBlockGrpTag = GetCaption(objLangCap.EnumLangCap.BlockGrp)
            strBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            strSubBlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            strAccGrpTag = GetCaption(objLangCap.EnumLangCap.AccGrp)
            strAccountTag = GetCaption(objLangCap.EnumLangCap.Account)

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccCls), intGLAR) = True) Then
                tlbStpFI.Rows(0).Visible = True
                lnkStpFI01.Text = strAccClsTag
                blnGLS = True
            Else
                tlbStpFI.Rows(0).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccClsGrp), intGLAR) = True) Then
                tlbStpFI.Rows(1).Visible = True
                lnkStpFI02.Text = strAccClsGrpTag
                blnGLS = True
            Else
                tlbStpFI.Rows(1).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivityGrp), intGLAR) = True) Then
                tlbStpFI.Rows(2).Visible = True
                lnkStpFI03.Text = strActGrpTag
                blnGLS = True
            Else
                tlbStpFI.Rows(2).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLActivity), intGLAR) = True) Then
                tlbStpFI.Rows(3).Visible = True
                lnkStpFI04.Text = strActTag
                blnGLS = True
            Else
                tlbStpFI.Rows(3).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubActivity), intGLAR) = True) Then
                tlbStpFI.Rows(4).Visible = True
                lnkStpFI05.Text = strSubActTag
                blnGLS = True
            Else
                tlbStpFI.Rows(4).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLExpense), intGLAR) = True) Then
                tlbStpFI.Rows(5).Visible = True
                lnkStpFI06.Text = strExpenseCodeTag
                 blnGLS = True
            Else
                tlbStpFI.Rows(5).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpenseGrp), intGLAR) = True) Then
                tlbStpFI.Rows(6).Visible = True
                lnkStpFI07.Text = strVehExpGrpCodeTag
                blnGLS = True
            Else
                tlbStpFI.Rows(6).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpense), intGLAR) = True) Then
                tlbStpFI.Rows(7).Visible = True
                lnkStpFI08.Text = strVehExpCodeTag
                blnGLS = True
            Else
                tlbStpFI.Rows(7).Visible = False
            End If

         
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleType), intGLAR) = True) Then
                tlbStpFI.Rows(8).Visible = True
                lnkStpFI09.Text = strVehTypeTag
                blnGLS = True
            Else
                tlbStpFI.Rows(8).Visible = False
            End If


            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intGLAR) = True) Then
                tlbStpFI.Rows(9).Visible = True
                lnkStpFI10.Text = strVehicleTag
                blnGLS = True
            Else
                tlbStpFI.Rows(9).Visible = False
            End If


            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlock), intGLAR) = True) Then
                tlbStpFI.Rows(10).Visible = True
                lnkStpFI11.Text = strBlockGrpTag
                blnGLS = True
            Else
                tlbStpFI.Rows(10).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlk), intGLAR) = True) Then
                tlbStpFI.Rows(11).Visible = True
                lnkStpFI12.Text = strBlockTag
                blnGLS = True
            Else
                tlbStpFI.Rows(11).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubBlk), intGLAR) = True) Then
                tlbStpFI.Rows(12).Visible = True
                tlbStpFI.Rows(13).Visible = True
                lnkStpFI13.Text = strSubBlockTag
                blnGLS = True
            Else
                tlbStpFI.Rows(12).Visible = False
                tlbStpFI.Rows(13).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccountGrp), intGLAR) = True) Then
                tlbStpFI.Rows(14).Visible = True
                lnkStpFI14.Text = strAccGrpTag
                blnGLS = True
            Else
                tlbStpFI.Rows(14).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccount), intGLAR) = True) Then
                tlbStpFI.Rows(15).Visible = True
                lnkStpFI15.Text = strAccountTag
                blnGLS = True
            Else
                tlbStpFI.Rows(15).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLEntrySetup), intGLAR) = True) Then
                tlbStpFI.Rows(16).Visible = True
                blnGLS = True
            Else
                tlbStpFI.Rows(16).Visible = False
            End If

            'If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBalSheetSetup), intGLAR) = True) Then
            '    tlbStpFI.Rows(16).Visible = True
            '    blnGLS = True
            'Else
            '    tlbStpFI.Rows(16).Visible = False
            'End If
            'If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLProfLossSetup), intGLAR) = True) Then
            '    tlbStpFI.Rows(17).Visible = True
            '    blnGLS = True
            'Else
            '    tlbStpFI.Rows(17).Visible = False
            'End If
            'If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLCOGS), intGLAR) = True) Then
            '    tlbStpFI.Rows(18).Visible = True
            '    tlbStpFI.Rows(19).Visible = True
            '    blnGLS = True
            'Else
            '    tlbStpFI.Rows(18).Visible = False
            '    tlbStpFI.Rows(19).Visible = False
            'End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLFSSETUP), intGLAR) = True) Then
                tlbStpFI.Rows(20).Visible = True
                blnGLS = True
            Else
                tlbStpFI.Rows(20).Visible = False
            End If

            tlbStpFI.Rows(21).Visible = True
           
         
            If blnGLS = False Then
                tlbStpFIHead.Visible = False
                tlbStpFI.Visible = False
                tlbSpc1.Visible = False
            End If

        Else
            tlbStpFIHead.Visible = False
            tlbStpFI.Visible = False
            tlbSpc1.Visible = False
        End If
    End Sub





    Private Sub DisableAllHead()


        'tlbStpFIHead .Visible = False
        tlbSpc1.Visible = False
        tlbStpAPHead.Visible = False
        tlbSpc2.Visible = False
        tlbStpARHead .Visible = False
        tlbSpc3.Visible = False
        tlbStpCBHead.Visible = False
        tlbSpc4.Visible = False
        tlbStpFAHead.Visible = False
        tblSpc5.Visible = False
        tblTXHead.Visible = False

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

    
    Private Sub TrxTXRight()
        Dim blnTX As Boolean
        blnTX = False


        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Canteen), intModuleActivate) Then

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster), intTXAR) = True) Then
                tblTX.Rows(0).Visible = True
                lnkStpTX01.Text = "Tax Object Rate"
                blnTX = True
            Else
                tblTX.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster), intTXAR) = True) Then
                tblTX.Rows(0).Visible = True
                lnkStpTX02.Text = "PTKP"
                blnTX = True
            Else
                tblTX.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier), intPUAR) = True) Then
                tblTX.Rows(0).Visible = True
                lnkStpTX03.Text = "Supplier"
                blnTX = True
            Else
                tblTX.Rows(0).Visible = False
            End If

            If blnTX = False Then
                tblTXHead.Visible = False
                tblTX.Visible = False

            End If
        Else
            tblTXHead.Visible = False
            tblTX.Visible = False

        End If
    End Sub

End Class
