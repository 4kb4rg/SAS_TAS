
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


Public Class menu_pmstp : Inherits Page

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
    Dim intHRAR As Long
    Dim intWSAR As Integer
    Dim intNUAR As Integer

    Const C_ADMIN = "ADMIN"
   


    Protected WithEvents tlbStpUpHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpUp As System.Web.UI.HtmlControls.HtmlTable 


    Protected WithEvents lnkStpUP01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpUP02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpUP03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpUP04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpUP05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpUP06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpUP07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpUP08 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpUP09 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpUP10 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents tlbSpc1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpNUHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpNU As System.Web.UI.HtmlControls.HtmlTable
   
    Protected WithEvents lnkStpNU01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpNU02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpNU03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpNU04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpNU05 As System.Web.UI.WebControls.HyperLink

    Protected WithEvents tlbSpc2 As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents tlbStpWSHead As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tlbStpWS As System.Web.UI.HtmlControls.HtmlTable

    Protected WithEvents lnkStpWS01 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpWS02 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpWS03 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpWS04 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpWS05 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpWS06 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpWS07 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpWS08 As System.Web.UI.WebControls.HyperLink 
    Protected WithEvents lnkStpWS09 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpWS10 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpWS11 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpWS12 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lnkStpWS13 As System.Web.UI.WebControls.HyperLink

   

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
           intHRAR = Session("SS_HRAR")   
           intNUAR = Session("SS_NUAR")
           intWSAR = Session("SS_WSAR")

           GetEntireLangCap()

           onLoad_Display()

        End If

    End Sub

   Sub onLoad_Display()

        If Session("SS_LOCATION") = "" Or Session("SS_MAXPERIOD") = 0 Then

            Call DisableAllHead()

        Else
            
          
            Call StpUPRight()
            Call stpNURight()
            Call StpWSRight()
           
           
        End If

    End Sub


   Private Sub stpUPRight()
        Dim strExpenseCodeTag As String
        Dim strVehExpGrpCodeTag As String
        Dim strVehExpCodeTag As String
        Dim strVehTypeTag As String
        Dim strVehicleTag As String
        Dim strBlockGrpTag As String
        Dim strBlockTag As String
        Dim strSubBlockTag As String
        Dim strActTag As String

        Dim blnUp As Boolean
        blnUp = False

        strExpenseCodeTag = GetCaption(objLangCap.EnumLangCap.Expense) & " Code"
        strVehExpGrpCodeTag = GetCaption(objLangCap.EnumLangCap.VehExpGrp) & " Code"
        strVehExpCodeTag = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code"
        strVehTypeTag = GetCaption(objLangCap.EnumLangCap.VehType)
        strVehicleTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strBlockGrpTag = GetCaption(objLangCap.EnumLangCap.BlockGrp)
        strBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        strSubBlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
        strActTag = GetCaption(objLangCap.EnumLangCap.Activity)

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpenseGrp), intGLAR) = True) Then
            tlbStpUp.Rows(0).Visible = True
            lnkStpUP01.Text = strVehExpGrpCodeTag
            blnUp = True
        Else
            tlbStpUp.Rows(0).Visible = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleExpense), intGLAR) = True) Then
            tlbStpUp.Rows(1).Visible = True
            lnkStpUP02.Text = strVehExpCodeTag
            blnUp = True
        Else
            tlbStpUp.Rows(1).Visible = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleType), intGLAR) = True) Then
            tlbStpUp.Rows(2).Visible = True
            lnkStpUP03.Text = strVehTypeTag
            blnUp = True
        Else
            tlbStpUp.Rows(2).Visible = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intGLAR) = True) Then
            tlbStpUp.Rows(3).Visible = True
            lnkStpUP04.Text = strVehicleTag
            blnUp = True
        Else
            tlbStpUp.Rows(3).Visible = False
        End If


        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlock), intGLAR) = True) Then
            tlbStpUp.Rows(4).Visible = True
            lnkStpUP05.Text = strBlockGrpTag
            blnUp = True
        Else
            tlbStpUp.Rows(4).Visible = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlk), intGLAR) = True) Then
            tlbStpUp.Rows(5).Visible = True
            lnkStpUP06.Text = strBlockTag
            blnUp = True
        Else
            tlbStpUp.Rows(5).Visible = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubBlk), intGLAR) = True) Then
            tlbStpUp.Rows(6).Visible = True
            lnkStpUP07.Text = strSubBlockTag
            blnUp = True
        Else
            tlbStpUp.Rows(6).Visible = False
        End If


        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRGang), intHRAR) = True) Then
            tlbStpUp.Rows(7).Visible = True
            blnUp = True
        Else
            tlbStpUp.Rows(7).Visible = False
        End If

        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRContractorSupervision), intHRAR) = True) Then
            tlbStpUp.Rows(8).Visible = True
            blnUp = True
        Else
            tlbStpUp.Rows(8).Visible = False
        End If


        If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intGLAR) = True) Then
            tlbStpUp.Rows(9).Visible = True
            lnkStpUP10.Text = strActTag & " " & strVehicleTag
            blnUp = True
        Else
            tlbStpUp.Rows(9).Visible = False
        End If

        If blnUp = False Then
            tlbStpUpHead.Visible = False
            tlbStpUp.Visible = False
            tlbSpc1.Visible = False
        End If

           
   End Sub


   Private Sub stpNURight()

        Dim strNurseryBatchTag As String
        Dim strCullingTypeTag As String
        Dim strAccClassification As String
	
        Dim blnNUS As Boolean

        blnNUS  = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Nursery), intModuleActivate) Then

            strNurseryBatchTag = GetCaption(objLangCap.EnumLangCap.NurseryBatch)
            strCullingTypeTag = GetCaption(objLangCap.EnumLangCap.CullType)
            strAccClassification = GetCaption(objLangCap.EnumLangCap.AccountDistribution)

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterItem), intNUAR) = True) Then
                tlbStpNU.Rows(0).Visible = True
                lnkStpNU01.Text = "Nursery Master"
                blnNUS = True
            Else
                tlbStpNU.Rows(0).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUItem), intNUAR) = True) Then
                tlbStpNU.Rows(1).Visible = True
                lnkStpNU02.Text = "Nursery Item"
                blnNUS = True
            Else
                tlbStpNU.Rows(1).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMasterSetup), intNUAR) = True) Then
                tlbStpNU.Rows(2).Visible = True
                lnkStpNU03.Text = strNurseryBatchTag
                blnNUS = True
            Else
                tlbStpNU.Rows(2).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCullType), intNUAR) = True) Then
                tlbStpNU.Rows(3).Visible = True
                lnkStpNU04.Text = strCullingTypeTag
                blnNUS = True
            Else
                tlbStpNU.Rows(3).Visible = False
            End If
            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUWorkAccDist), intNUAR) = True) Then
                tlbStpNU.Rows(4).Visible = True
                lnkStpNU05.Text = strAccClassification
                blnNUS = True
            Else
                tlbStpNU.Rows(4).Visible = False
            End If

            If blnNUS = False Then
                tlbSpc2.Visible = False
                tlbStpNUHead.Visible = False
                tlbStpNU.Visible = False
            End If

        Else
            tlbSpc2.Visible = False
            tlbStpNUHead.Visible = False
            tlbStpNU.Visible = False
        End If

    End Sub

    
    Private Sub StpWSRight()

        Dim ProdTypeTag As String
        Dim ProdBrandTag As String
        Dim ProdModelTag As String
        Dim ProdCatTag As String
        Dim ProdMatTag As String
        Dim StockAnalTag As String
        Dim WorkTag As String
        Dim DirectChgMasterTag As String
        Dim DirectChgItemTag As String

        Dim blnWSS As Boolean
        blnWSS  = False

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Workshop), intModuleActivate) Then

            ProdTypeTag = GetCaption(objLangCap.EnumLangCap.ProdType)
            ProdBrandTag = GetCaption(objLangCap.EnumLangCap.ProdBrand)
            ProdModelTag = GetCaption(objLangCap.EnumLangCap.ProdModel)
            ProdCatTag = GetCaption(objLangCap.EnumLangCap.ProdCat)
            ProdMatTag = GetCaption(objLangCap.EnumLangCap.ProdMat)
            StockAnalTag = GetCaption(objLangCap.EnumLangCap.StockAnalysis)
            WorkTag = GetCaption(objLangCap.EnumLangCap.Work)
            DirectChgMasterTag = GetCaption(objLangCap.EnumLangCap.DirectChgMaster)
            DirectChgItemTag = GetCaption(objLangCap.EnumLangCap.DirectChgItem)

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaster), intWSAR) = True) Then
                tlbStpWS.Rows(0).Visible = True
                lnkStpWS01.Text = ProdTypeTag
                blnWSS = True
            Else
                tlbStpWS.Rows(0).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductBrand), intWSAR) = True) Then
                tlbStpWS.Rows(1).Visible = True
                lnkStpWS02.Text = ProdBrandTag
                blnWSS = True
            Else
                tlbStpWS.Rows(1).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductModel), intWSAR) = True) Then
                tlbStpWS.Rows(2).Visible = True
                lnkStpWS03.Text = ProdModelTag
                blnWSS = True
            Else
                tlbStpWS.Rows(2).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductCategory), intWSAR) = True) Then
                tlbStpWS.Rows(3).Visible = True
                lnkStpWS04.Text = ProdCatTag
                blnWSS = True
            Else
                tlbStpWS.Rows(3).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSProductMaterial), intWSAR) = True) Then
                tlbStpWS.Rows(4).Visible = True
                lnkStpWS05.Text = ProdMatTag
                blnWSS = True
            Else
                tlbStpWS.Rows(4).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSAnalisisStok), intWSAR) = True) Then
                tlbStpWS.Rows(5).Visible = True
                lnkStpWS06.Text = StockAnalTag
                blnWSS = True
            Else
                tlbStpWS.Rows(5).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkMaster), intWSAR) = True) Then
                tlbStpWS.Rows(6).Visible = True
                lnkStpWS07.Text = WorkTag & " Code"
                blnWSS = True
            Else
                tlbStpWS.Rows(6).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkshopService), intWSAR) = True) Then
                tlbStpWS.Rows(7).Visible = True
                lnkStpWS08.Text = "Service Type"
                blnWSS = True
            Else
                tlbStpWS.Rows(7).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItem), intWSAR) = True) Then
                tlbStpWS.Rows(8).Visible = True
                lnkStpWS09.Text = DirectChgItemTag
                blnWSS = True
            Else
                tlbStpWS.Rows(8).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItemMaster), intWSAR) = True) Then
                tlbStpWS.Rows(9).Visible = True
                lnkStpWS10.Text = DirectChgMasterTag
                blnWSS = True
            Else
                tlbStpWS.Rows(9).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItemPart), intWSAR) = True) Then
                tlbStpWS.Rows(10).Visible = True
                lnkStpWS11.Text = "Workshop Item Part"
                blnWSS = True
            Else
                tlbStpWS.Rows(10).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMillProcDistr), intWSAR) = True) Then
                tlbStpWS.Rows(11).Visible = True
                lnkStpWS12.Text = "Monthly Mill Process Distribution"
                blnWSS = True
            Else
                tlbStpWS.Rows(11).Visible = False
            End If

            If (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMillProcDistr), intWSAR) = True) Then
                tlbStpWS.Rows(12).Visible = True
                lnkStpWS13.Text = "Calendarized Machine"
                blnWSS = True
            Else
                tlbStpWS.Rows(12).Visible = False
            End If

            If blnWSS = False Then
                tlbStpWSHead.Visible = False
                tlbStpWS.Visible = False
            End If

        Else
            tlbStpWSHead.Visible = False
            tlbStpWS.Visible = False
        End If

    End Sub



    Private Sub DisableAllHead()


        tlbStpUpHead.Visible = False
        tlbSpc1.Visible = False
        tlbStpNUHead.Visible = False
        tlbSpc2.Visible = False
        tlbStpWSHead.Visible = False
	

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
