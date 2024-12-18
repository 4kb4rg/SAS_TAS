
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

Public Class menu_mmstp : Inherits Page


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

  
    Dim intINAR As Integer
    Dim intPUAR As Integer
    DIM intCMAR As Integer
   

    Const C_ADMIN = "ADMIN"

    Protected WithEvents lnkStpIN01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpIN02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpIN03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpIN04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpIN05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpIN06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpIN07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpIN08 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpIN09 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpIN10 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpIN11 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents tlbStpINHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpIN As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tlbSpc1 As System.Web.UI.HtmlControls.HtmlTable
	
    Protected WithEvents tlbStpPUHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpPU As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents lnkStpPU01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPU02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpPU03 As System.Web.UI.WebControls.HyperLink


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


        intINAR = Session("SS_INAR")
        intPUAR = Session("SS_PUAR")
        intCMAR = Session("SS_CMAR")

        GetEntireLangCap()
        onLoad_Display()


        End If

    End Sub

   Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then
            Call DisableAllHead()

        Else
            
            Call StpINRight()
            Call StpPURight()
         
        End If

    End Sub


     Private Sub StpINRight()

        Dim strProdTypeTag As String
        Dim strProdBrandTag As String
        Dim strProdModelTag As String
        Dim strProdCatTag As String
        Dim strProdMatTag As String
        Dim strStockAnaTag As String
        Dim strStockMasterTag As String
        Dim strStockItemTag As String
        Dim strDirectMasterTag As String
        Dim strDirectChargeTag As String

	Dim blnINS As Boolean
	blnINS  = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Inventory), intModuleActivate) Then

            strProdTypeTag = GetCaption(objLangCap.EnumLangCap.ProdType)
            strProdBrandTag = GetCaption(objLangCap.EnumLangCap.ProdBrand)
            strProdModelTag = GetCaption(objLangCap.EnumLangCap.ProdModel)
            strProdCatTag = GetCaption(objLangCap.EnumLangCap.ProdCat)
            strProdMatTag = GetCaption(objLangCap.EnumLangCap.ProdMat)
            strStockAnaTag = GetCaption(objLangCap.EnumLangCap.StockAnalysis)
            strStockMasterTag = GetCaption(objLangCap.EnumLangCap.StockMaster)
            strDirectMasterTag = GetCaption(objLangCap.EnumLangCap.DirectChgMaster)
            strStockItemTag = GetCaption(objLangCap.EnumLangCap.StockItem)
            strDirectChargeTag = GetCaption(objLangCap.EnumLangCap.DirectChgItem)

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductSetup), intINAR) = True) Then
                tlbStpIN.Rows(0).Visible = True
                lnkStpIN01.Text = strProdTypeTag
                blnINS = True
            Else
                tlbStpIN.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductBrand), intINAR) = True) Then
                tlbStpIN.Rows(1).Visible = True
                lnkStpIN02.Text = strProdBrandTag
                blnINS = True
            Else
                tlbStpIN.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductModel), intINAR) = True) Then
                tlbStpIN.Rows(2).Visible = True
                lnkStpIN03.Text = strProdModelTag
                blnINS = True
            Else
                tlbStpIN.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductCategory), intINAR) = True) Then
                tlbStpIN.Rows(3).Visible = True
                lnkStpIN04.Text = strProdCatTag
                blnINS = True
            Else
                tlbStpIN.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductMaterial), intINAR) = True) Then
                tlbStpIN.Rows(4).Visible = True
                lnkStpIN05.Text = strProdMatTag
                blnINS = True
            Else
                tlbStpIN.Rows(4).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INAnalisisStok), intINAR) = True) Then
                tlbStpIN.Rows(5).Visible = True
                lnkStpIN06.Text = strStockAnaTag
                blnINS = True
            Else
                tlbStpIN.Rows(5).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItemMaster), intINAR) = True) Then
                tlbStpIN.Rows(6).Visible = True
                lnkStpIN07.Text = strStockMasterTag
                blnINS = True
            Else
                tlbStpIN.Rows(6).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItem), intINAR) = True) Then
                tlbStpIN.Rows(7).Visible = True
                lnkStpIN08.Text = strStockItemTag
                blnINS = True
            Else
                tlbStpIN.Rows(7).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDirectChargeItem), intINAR) = True) Then
                tlbStpIN.Rows(8).Visible = True
                lnkStpIN09.Text = strDirectMasterTag
                blnINS = True
            Else
                tlbStpIN.Rows(8).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDirectChargeItem), intINAR) = True) Then
                tlbStpIN.Rows(9).Visible = True
                lnkStpIN10.Text = strDirectChargeTag
                blnINS = True
            Else
                tlbStpIN.Rows(9).Visible = False
            End If
            '*temp used 
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItem), intINAR) = True) Then
                tlbStpIN.Rows(10).Visible = True
                'lnkStpIN011.Text = strStockItemTag
                blnINS = True
            Else
                tlbStpIN.Rows(10).Visible = False
            End If

            If blnINS = False Then
                tlbStpINHead.Visible = False
                tlbStpIN.Visible = False
                tlbSpc1.Visible = False
            End If
        Else
            tlbStpINHead.Visible = False
            tlbStpIN.Visible = False
            tlbSpc1 .Visible = False
        End If
    End Sub

   
    Private Sub StpPURight()
	Dim blnPU As Boolean
        blnPU  = False
     
        'supplier
        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier), intPUAR) = True) Then
           tlbStpPU.Rows(0).Visible = True
           blnPU  = True
        Else
          tlbStpPU.Rows(0).Visible = False
         End If
        
        'currency
         If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMMasterSetup), intCMAR) = True) Then
              tlbStpPU.Rows(1).Visible = True
              blnPU  = True
         Else
              tlbStpPU.Rows(1).Visible = False
         End If
            
          'exchange rate
         If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCMAccessRights.CMExchangeRate), intCMAR) = True) Then
              tlbStpPU.Rows(2).Visible = True
              blnPU  = True
         Else
              tlbStpPU.Rows(2).Visible = False
         End If

         if BlnPU = False Then
            tlbStpPUHead.Visible = False
            tlbStpPU.Visible = False
	 End If

    End Sub



    Private Sub DisableAllHead()

        tlbStpINHead.Visible = False
        tlbSpc1.Visible = False
        tlbStpPUHead.Visible = False
       
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