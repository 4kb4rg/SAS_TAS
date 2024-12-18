
Imports System
Imports System.Data
Imports System.Math
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.IN
Imports agri.GL
Imports agri.PWSystem.clsLangCap


Public Class IN_StockDetails : Inherits Page

    Dim dsStockItem AS Dataset

    Protected objIN As New agri.IN.clsSetup()
    Protected objGL As New agri.GL.clsSetup()
    Protected objAdmin As New agri.Admin.clsUOM()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents ErrorMessage as Label
    Protected WithEvents lblErrMessage as Label
    Protected WithEvents blnUpdate as Label
    Protected WithEvents StckItemCode as textBox
    Protected WithEvents Desc as textBox
    Protected WithEvents Status as Label
    Protected WithEvents CreateDate as Label
    Protected WithEvents UpdateDate as Label
    Protected WithEvents UpdateBy as Label
    Protected WithEvents InitialCost as Label
    Protected WithEvents HighestCost as Label
    Protected WithEvents LowestCost as Label
    Protected WithEvents AvrgCost as Label
    Protected WithEvents LatestCost as Label
    Protected WithEvents DiffAverageCost as Label
    Protected WithEvents purchaseACNo as TextBox
    Protected WithEvents IssueACNo as TextBox
    Protected WithEvents LatestPrice as TextBox
    Protected WithEvents AvrgPrice as TextBox
    Protected WithEvents FixedPrice as TextBox
    Protected WithEvents txtBin As TextBox
    Protected WithEvents txtFuelMeter As TextBox
    Protected WithEvents HandQty As Label
    Protected WithEvents HoldQty as Label
    Protected WithEvents OrderQty as Label
    Protected WithEvents ReorderLvl as TextBox
    Protected WithEvents ReorderQty as TextBox
    Protected WithEvents Remark as TextBox
    Protected WithEvents Fuel_Yes as RadioButton
    Protected WithEvents Fuel_No as RadioButton
    Protected WithEvents Fuel_Lub as RadioButton
    Protected WithEvents Fixed_Price as RadioButton
    Protected WithEvents Latest_Price as RadioButton
    Protected WithEvents Avrg_Price as RadioButton
    Protected WithEvents lstProdType as DropDownList
    Protected WithEvents lstProdCat as DropDownList
    Protected WithEvents lstProdBrand as DropDownList
    Protected WithEvents lstProdModel as DropDownList
    Protected WithEvents lstProdMat as DropDownList
    Protected WithEvents lstStockAnalysis as DropDownList
    Protected WithEvents lstVehExpense as DropDownList
    Protected WithEvents PurchaseUOM as DropDownList
    Protected WithEvents InventoryUOM as DropDownList
    Protected WithEvents lblDupMsg as Label
    Protected WithEvents lstAccCode as DropDownList
    Protected WithEvents lstItemCode As DropDownList
    Protected WithEvents Delete As ImageButton
    Protected WithEvents Save as ImageButton
    Protected WithEvents SynchronizeData as ImageButton
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblStockItemCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblProdType As Label
    Protected WithEvents lblProdCat As Label
    Protected WithEvents lblProdBrand As Label
    Protected WithEvents lblProdModel As Label
    Protected WithEvents lblProdMat As Label
    Protected WithEvents lblStockAna As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblAccount As Label 
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator  

    Protected WithEvents chkFertInd As CheckBox

    Protected WithEvents LatestCostFinal As Label
    Protected WithEvents LatestCostSecond As Label
    Protected WithEvents LatestCostInitial As Label

    Protected WithEvents Itm_Stock As RadioButton
    Protected WithEvents Itm_Workshop As RadioButton
    Protected WithEvents LabelLifespan As Label
    Protected WithEvents LabelHour As Label
    Protected WithEvents txtLifespan As TextBox

    Protected WithEvents lblBin1 As Label
    Protected WithEvents QtyBin1 As Label
    Protected WithEvents lblBin2 As Label
    Protected WithEvents QtyBin2 As Label
    Protected WithEvents lblBin3 As Label
    Protected WithEvents QtyBin3 As Label
    Protected WithEvents lblBin4 As Label
    Protected WithEvents QtyBin4 As Label
    Protected WithEvents lblBin5 As Label
    Protected WithEvents QtyBin5 As Label
    Protected WithEvents lblBin6 As Label
    Protected WithEvents QtyBin6 As Label
    Protected WithEvents lblBin7 As Label
    Protected WithEvents QtyBin7 As Label
    Protected WithEvents lblBin8 As Label
    Protected WithEvents QtyBin8 As Label
    Protected WithEvents lblBin9 As Label
    Protected WithEvents QtyBin9 As Label
    Protected WithEvents lblBin10 As Label
    Protected WithEvents QtyBin10 As Label

    Dim strOppCd_ADD As String = "IN_CLSSETUP_INVITEM_DETAILS_ADD"
    Dim strOppCd_UPD As String = "IN_CLSSETUP_INVITEM_DETAILS_UPD"
    Dim strOppCd_ItemList_GET As String = "IN_CLSSETUP_INVITEM_LIST_GET"
    Dim strOppCd_ProdType_GET As String = "IN_CLSSETUP_PRODTYPE_LIST_GET"
    Dim strOppCd_ProdCat_GET As String = "IN_CLSSETUP_PRODCAT_LIST_GET"
    Dim strOppCd_ProdBrand_GET As String = "IN_CLSSETUP_PRODBRAND_LIST_GET"
    Dim strOppCd_ProdModel_GET As String = "IN_CLSSETUP_PRODMODEL_LIST_GET"
    Dim strOppCd_ProdMaterial_GET As String = "IN_CLSSETUP_PRODMAT_LIST_GET"
    Dim strOppCd_StockAna_GET As String = "IN_CLSSETUP_STOCKANA_LIST_GET"
    Dim strOppCd_VehExpense_GET As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
    Dim strOppCd_AccountCode_GET As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
    Dim strOppCd_UOM_GET As String = "ADMIN_CLSUOM_UOM_LIST_GET"
    Dim ItemCode As String 
    Dim strParam As String 
    Dim objDataSet As New Dataset()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intINAR As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String
    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intINAR = Session("SS_INAR")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItem), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If Not IsPostBack Then
                If Not Request.QueryString("ID") = "" Then
                    ItemCode = Request.QueryString("ID")
                End If

                If Not ItemCode = "" Then
                    DisplayData()
                    DisplayDropDown()
                    BindItemCodeDropList()

                    blnUpdate.Text = True
                    lstItemCode.Enabled = False
                    DisableControl()

                    Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Else
                    DisplayDropDown()
                    BindItemCodeDropList()
                    ItemCode = lstItemCode.SelectedItem.Value
                    
                    blnUpdate.Text = False
                    Delete.Visible = False
                    SynchronizeData.Visible = False
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.StockItem))
        lblStockItemCode.text = GetCaption(objLangCap.EnumLangCap.StockItem) & " Code"
        lblDescription.text = GetCaption(objLangCap.EnumLangCap.StockItemDesc)
        lblProdType.text = GetCaption(objLangCap.EnumLangCap.ProdType)
        lblProdCat.text = GetCaption(objLangCap.EnumLangCap.ProdCat)
        lblProdBrand.text = GetCaption(objLangCap.EnumLangCap.ProdBrand)
        lblProdModel.text = GetCaption(objLangCap.EnumLangCap.ProdModel)
        lblProdMat.text = GetCaption(objLangCap.EnumLangCap.ProdMat)
        lblStockAna.text = GetCaption(objLangCap.EnumLangCap.StockAnalysis)
        lblVehExpCode.text = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code"

        validateCode.ErrorMessage = "Please enter " & lblStockItemCode.text & "."
        validateDesc.ErrorMessage = "Please enter " & lblDescription.text & "."

    End Sub

    Sub GetEntireLangCap()
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
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_ActivityDet.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function

    Protected Function LoadData(ByVal pv_blnIsMaster As Boolean) As DataSet
        Dim strOppCd_GET As String
        strParam = ItemCode & "|" & objIN.EnumInventoryItemType.Stock & "','" & objIN.EnumInventoryItemType.WorkshopItem & "','7|" & Trim(strLocation)

        If Request.QueryString("ID") <> "" Then
            If pv_blnIsMaster = False Then
                strOppCd_GET = "IN_CLSSETUP_INVITEM_DETAILS_GET"
            Else
                strOppCd_GET = "IN_CLSSETUP_COPYMASTER_DETAILS_GET"
            End If
        Else
            strOppCd_GET = "IN_CLSSETUP_COPYMASTER_DETAILS_GET"
        End If

        Try
            intErrNo = objIN.mtdGetMasterDetail(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKITEM_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_stockItem.aspx")
        End Try

        Return objDataSet
    End Function

    Sub DisableControl()
        StckItemCode.Enabled = False
        Desc.Enabled = False
        purchaseACNo.Enabled = False
        IssueACNo.Enabled = False
        lstProdType.Enabled = False
        lstProdCat.Enabled = False
        lstProdBrand.Enabled = False
        lstProdModel.Enabled = False
        lstProdMat.Enabled = False
        Fuel_Yes.Enabled = False
        Fuel_No.Enabled = False
        Fuel_Lub.Enabled = False
        lstStockAnalysis.Enabled = False
        lstVehExpense.Enabled = False
        PurchaseUOM.Enabled = False
        InventoryUOM.Enabled = False
      
        chkFertInd.Enabled = False
        If dsStockItem.Tables(0).Rows(0).Item("Status") = objIN.EnumStockItemStatus.Deleted Then
            SynchronizeData.Visible = False
            Delete.ImageUrl = "../../images/butt_Undelete.gif"
            lstAccCode.Enabled = False
            Fixed_Price.Enabled = False
            Latest_Price.Enabled = False
            Avrg_Price.Enabled = False
            ReorderLvl.Enabled = False
            ReorderQty.Enabled = False
            LatestPrice.Enabled = False
            AvrgPrice.Enabled = False
            FixedPrice.Enabled = False
            Remark.Enabled = False
            InitialCost.Enabled = False
            HighestCost.Enabled = False
            LowestCost.Enabled = False
            AvrgCost.Enabled = False
            LatestCost.Enabled = False
            HandQty.Enabled = False
            HoldQty.Enabled = False
            OrderQty.Enabled = False
            UpdateDate.Enabled = False
            UpdateBy.Enabled = False
            StckItemCode.Enabled = False
            Status.Enabled = False
            CreateDate.Enabled = False
            txtBin.Enabled = False
            txtFuelMeter.Enabled = False
            LatestCostFinal.Enabled = False
            LatestCostSecond.Enabled = False
            LatestCostInitial.Enabled = False
            QtyBin1.Enabled = False
            QtyBin2.Enabled = False
            QtyBin3.Enabled = False
            QtyBin4.Enabled = False
            QtyBin5.Enabled = False
            QtyBin6.Enabled = False
            QtyBin7.Enabled = False
        End If
        Itm_Stock.Enabled = False
        Itm_Workshop.Enabled = False
        txtLifespan.Enabled = False
    End Sub

    Sub DisplayData()
        Dim objMasterDs As New Dataset()
        dsStockItem = LoadData(False)
        If dsStockItem.Tables(0).Rows.Count > 0 Then
            StckItemCode.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("ItemCode"))
            Desc.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("Description"))
            Status.Text = Trim(objIN.mtdGetStockItemStatus(dsStockItem.Tables(0).Rows(0).Item("Status")))
            CreateDate.Text = Trim(objGlobal.Getlongdate(dsStockItem.Tables(0).Rows(0).Item("CreateDate")))
            UpdateDate.Text = Trim(objGlobal.Getlongdate(dsStockItem.Tables(0).Rows(0).Item("UpdateDate")))
            UpdateBy.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("UserName"))

            InitialCost.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("InitialCost"))
            HighestCost.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("HighCost"))
            LowestCost.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("LowCost"))
            AvrgCost.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("AverageCost"))
            LatestCost.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("LatestCost"))
            DiffAverageCost.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("DiffAverageCost"))
            purchaseACNo.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("purchaseACcNo"))
            IssueACNo.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("IssueACcNo"))
            LatestPrice.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("SellLatestCost"))
            AvrgPrice.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("SellAverageCost"))         
            FixedPrice.Text = Round(dsStockItem.Tables(0).Rows(0).Item("SellFixedPrice"),0)
            HandQty.Text = ObjGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyonHand")),5)
            HoldQty.Text = ObjGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyOnHold")),5)
            OrderQty.Text = ObjGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyOnOrder")),5)
            ReorderLvl.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("ReOrderLevel"))
            ReorderQty.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("QtyReOrder"))
            Remark.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("Remark"))
            txtBin.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("Bin"))
            txtFuelMeter.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("FuelMeterReading"))
            chkFertInd.Checked = IIf(CInt(IIF(IsNumeric(dsStockItem.Tables(0).Rows(0).Item("FertInd")),dsStockItem.Tables(0).Rows(0).Item("FertInd"),"2")) = objIN.EnumFertilizerInd.Yes, True, False)
            LatestCostFinal.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("LatestCostFinal"))    
            LatestCostSecond.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("LatestCostSecond"))       
            LatestCostInitial.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("LatestCostInitial"))        

            QtyBin1.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyBin1")), 5)
            QtyBin2.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyBin2")), 5)
            QtyBin3.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyBin3")), 5)
            QtyBin4.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyBin4")), 5)
            QtyBin5.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyBin5")), 5)
            QtyBin6.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyBin6")), 5)
            QtyBin7.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyBin7")), 5)
            QtyBin8.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyBin8")), 5)
            QtyBin9.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyBin9")), 5)
            QtyBin10.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyBin10")), 5)

            If dsStockItem.Tables(0).Rows(0).Item("FuelTypeInd") = objIN.EnumFuelItemIndicator.Fuel Then
                Fuel_Yes.Checked = True
                Fuel_Lub.Checked = False
                Fuel_No.Checked = False
            ElseIf dsStockItem.Tables(0).Rows(0).Item("FuelTypeInd") = objIN.EnumFuelItemIndicator.Lubricant Then
                Fuel_Yes.Checked = False
                Fuel_Lub.Checked = True
                Fuel_No.Checked = False
            Else
                Fuel_Yes.Checked = False
                Fuel_Lub.Checked = False
                Fuel_No.Checked = True
            End If

            If dsStockItem.Tables(0).Rows(0).Item("UsePrice") = objIN.EnumSellingPriceForItem.Fixed Then
                Fixed_Price.Checked = True
                Latest_Price.Checked = False
                Avrg_Price.Checked = False
                LatestPrice.Text = ""
                AvrgPrice.Text = ""
            ElseIf dsStockItem.Tables(0).Rows(0).Item("UsePrice") = objIN.EnumSellingPriceForItem.PercentageOfLatestCost Then
                Fixed_Price.Checked = False
                Latest_Price.Checked = True
                Avrg_Price.Checked = False
                AvrgPrice.Text = ""
                FixedPrice.Text = ""
            ElseIf dsStockItem.Tables(0).Rows(0).Item("UsePrice") = objIN.EnumSellingPriceForItem.PercentageOfAverageCost Then
                Fixed_Price.Checked = False
                Latest_Price.Checked = False
                Avrg_Price.Checked = True
                LatestPrice.Text = ""
                FixedPrice.Text = ""
            End If


            If (dsStockItem.Tables(0).Rows(0).Item("ItemType") = objIN.EnumInventoryItemType.Stock) or (dsStockItem.Tables(0).Rows(0).Item("ItemType") = "7") Then
                Itm_Stock.Checked = True
                Itm_Workshop.Checked = False    
                LabelLifespan.Visible = False
                txtLifespan.Visible = False    
                LabelHour.Visible = False
            ElseIf dsStockItem.Tables(0).Rows(0).Item("ItemType") = objIN.EnumInventoryItemType.WorkshopItem Then
                Itm_Stock.Checked = False
                Itm_Workshop.Checked = True
                LabelLifespan.Visible = True
                txtLifespan.Visible = True
                LabelHour.Visible = True
                txtLifespan.Text = FormatNumber(Trim(dsStockItem.Tables(0).Rows(0).Item("Lifespan")),0)
            End If

            objMasterDs = LoadData(True)

            If objMasterDs.Tables(0).Rows.Count > 0 Then
                If objMasterDs.Tables(0).Rows(0).Item("Status") = objIN.EnumStockItemStatus.Deleted And _
                    dsStockItem.Tables(0).Rows(0).Item("Status") = objIN.EnumStockItemStatus.Deleted Then
                    SynchronizeData.Visible = False
                    Delete.Visible = False
                End If
            End If
        End If
    End Sub

    Sub DisplayDropDown()

        BindDropList(strOppCd_ProdType_GET, "ProdTypeCode", "ProdTypeCode", objIN.EnumInventoryMasterType.ProductType, _
                       objGlobal.EnumModule.Inventory, objIN.EnumProductTypeStatus.Active, lstProdType)
        BindDropList(strOppCd_ProdCat_GET, "ProdCatCode", "ProdCatCode", objIN.EnumInventoryMasterType.ProductCategory, _
                        objGlobal.EnumModule.Inventory, objIN.EnumProductCategoryStatus.Active, lstProdCat)
        BindDropList(strOppCd_ProdBrand_GET, "ProdBrandCode", "ProdBrandCode", objIN.EnumInventoryMasterType.ProductBrand, _
                        objGlobal.EnumModule.Inventory, objIN.EnumProductBrandStatus.Active, lstProdBrand)
        BindDropList(strOppCd_ProdModel_GET, "ProdModelCode", "ProdModelCode", objIN.EnumInventoryMasterType.ProductModel, _
                        objGlobal.EnumModule.Inventory, objIN.EnumProductModelStatus.Active, lstProdModel)
        BindDropList(strOppCd_ProdMaterial_GET, "ProdMatCode", "ProdMatCode", objIN.EnumInventoryMasterType.ProductMaterial, _
                        objGlobal.EnumModule.Inventory, objIN.EnumProductMaterialStatus.Active, lstProdMat)
        BindDropList(strOppCd_StockAna_GET, "StockAnalysisCode", "StockAnalysisCode", objIN.EnumInventoryMasterType.StockAnalysis, _
                        objGlobal.EnumModule.Inventory, objIN.EnumStockAnalysisStatus.Active, lstStockAnalysis)
        BindDropList(strOppCd_VehExpense_GET, "ExpenseCode", "VehExpenseCode", objGL.EnumGLMasterType.VehicleExpense, _
                        objGlobal.EnumModule.GeneralLedger, objGL.EnumVehicleExpenseStatus.Active, lstVehExpense)
        BindDropList(strOppCd_AccountCode_GET, "ActCode", "AccCode", objGL.EnumGLMasterType.AccountCode, _
                        objGlobal.EnumModule.GeneralLedger, objGL.EnumAccountCodeStatus.Active, lstAccCode)
        BindDropList(strOppCd_UOM_GET, "PurchaseUOM", "UOMCode", "0", _
                         objGlobal.EnumModule.Admin, objAdmin.EnumUOMStatus.Active, PurchaseUOM)
        BindDropList(strOppCd_UOM_GET, "UOMCode", "UOMCode", "0", _
                         objGlobal.EnumModule.Admin, objAdmin.EnumUOMStatus.Active, InventoryUOM)
    End Sub

    Sub BindDropList(ByVal Oppcode As String, ByVal ItemKeyField As String, ByVal MasterKeyField As String, ByVal MasterType As Integer, _
    ByVal ModuleType As Integer, ByVal Status As Integer, ByVal BindToList As DropDownList)

        Dim strParam As String
        Dim dsForDropDown As DataSet
        Dim dsForInactiveItem As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim drinsert As DataRow

        If ModuleType = objGlobal.EnumModule.Inventory Then
            Select Case MasterType
                Case objIN.EnumInventoryMasterType.ProductType
                    TblAlias = "PType"
                Case objIN.EnumInventoryMasterType.ProductCategory
                    TblAlias = "PCat"
                Case objIN.EnumInventoryMasterType.ProductBrand
                    TblAlias = "PBrand"
                Case objIN.EnumInventoryMasterType.ProductModel
                    TblAlias = "PModel"
                Case objIN.EnumInventoryMasterType.ProductMaterial
                    TblAlias = "PMat"
                Case objIN.EnumInventoryMasterType.StockAnalysis
                    TblAlias = "StockAna"
            End Select
        ElseIf ModuleType = objGlobal.EnumModule.GeneralLedger Then
            Select Case MasterType
                Case objGL.EnumGLMasterType.VehicleExpense
                    TblAlias = "Veh"
                Case objGL.EnumGLMasterType.AccountCode
                    TblAlias = "Acc"
            End Select
        ElseIf ModuleType = objGlobal.EnumModule.Admin Then
            TblAlias = "UOM"
        End If

        If ModuleType = objGlobal.EnumModule.Admin Then
            DataTextField = "UOMDESC"
        Else
            DataTextField = "Description"
        End If

        strParam = "ORDER BY " & MasterKeyField & "|" & "AND " & TblAlias & ".Status =" & Status
        If ModuleType = objGlobal.EnumModule.GeneralLedger And MasterType = objGL.EnumGLMasterType.AccountCode Then
            strParam += " AND " & TblAlias & ".AccType=" & objGL.EnumAccountType.BalanceSheet & _
                        " AND " & TblAlias & ".NurseryInd=" & objGL.EnumNurseryAccount.No
            strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")
        End If

        Try
            Select Case ModuleType
                Case objGlobal.EnumModule.Admin
                    intErrNo = objAdmin.mtdGetMasterList(Oppcode, strParam, dsForDropDown)
                Case objGlobal.EnumModule.GeneralLedger
                    intErrNo = objGL.mtdGetMasterList(Oppcode, strParam, MasterType, dsForDropDown)
                Case objGlobal.EnumModule.Inventory
                    intErrNo = objIN.mtdGetMasterList(Oppcode, strParam, MasterType, dsForDropDown)
            End Select
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_stockItem.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If Not ItemCode = "" Then
                If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(MasterKeyField)) = Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField)) Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = " "
        drinsert(1) = "Please Select a Code "
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        BindToList.DataSource = dsForDropDown.Tables(0)
        BindToList.DataValueField = MasterKeyField
        BindToList.DataTextField = DataTextField
        BindToList.DataBind()

        If SelectedIndex = -1 And Not ItemCode = "" Then
            If ModuleType = objGlobal.EnumModule.GeneralLedger And MasterType = objGL.EnumGLMasterType.AccountCode Then
                'Get AccCode from each Location
                strParam = "Order by " & MasterKeyField & "|" & "AND " & MasterKeyField & " Like '" & _
                                       Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField)) & "%'" & _
                                       IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")
            Else
                strParam = "Order by " & MasterKeyField & "|" & "AND " & MasterKeyField & " = '" & _
                       Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField)) & "'"
            End If
           
            Try
                intErrNo = objIN.mtdGetMasterList(Oppcode, strParam, MasterType, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then
                    'BindToList.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0)) & _
                    ' " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Deleted) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0))))
                    BindToList.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0)) & _
                     " (" & Trim(dsForInactiveItem.Tables(0).Rows(0).Item(1)) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0))))
                    SelectedIndex = BindToList.Items.Count - 1
                Else
                    BindToList.Items.Add(New ListItem(Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField)) & _
                       " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Unknown) & ") ", Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField))))
                    SelectedIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_stockItem.aspx")
            End Try

        End If

        BindToList.SelectedIndex = SelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
        If Not dsForInactiveItem Is Nothing Then
            dsForInactiveItem = Nothing
        End If
    End Sub

    Sub BindItemCodeDropList()
        Dim strOpCdItemCode_Get As String = "IN_CLSSETUP_INVMASTER_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow

        SearchStr = "AND ItemType in ('" & objIN.EnumInventoryItemType.Stock & "','" & objIN.EnumInventoryItemType.WorkshopItem & "','7') AND itm.Status = '" & objIN.EnumStockItemStatus.Active & "' AND Not ItemCode In (Select ItemCode From IN_Item where ItemType in ('" & objIN.EnumInventoryItemType.Stock & "','" & objIN.EnumInventoryItemType.WorkshopItem & "') And LocCode='" & strLocation & "' ) "

        strParam = "ORDER BY ItemCode asc|" & SearchStr

        Try
            intErrNo = objIN.mtdGetMasterList(strOpCdItemCode_Get, strParam, objIN.EnumInventoryMasterType.StockItem, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_stockitem.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If Not ItemCode = "" Then
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode"))) = UCase(Trim(objDataSet.Tables(0).Rows(0).Item("ItemCode"))) Then
                        SelectedIndex = intCnt + 1
                    End If
                End If
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Please Select a Code "
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstItemCode.DataSource = dsForDropDown.Tables(0)
        lstItemCode.DataValueField = "ItemCode"
        lstItemCode.DataTextField = "Description"
        lstItemCode.DataBind()

        If SelectedIndex = -1 And Not ItemCode = "" Then
            If objDataSet.Tables(0).Rows.Count > 0 Then
                lstItemCode.Items.Add(New ListItem(Trim(objDataSet.Tables(0).Rows(0).Item("ItemCode")), Trim(objDataSet.Tables(0).Rows(0).Item("ItemCode"))))
                SelectedIndex = lstItemCode.Items.Count - 1
                lstItemCode.SelectedIndex = SelectedIndex
            Else
                Response.Redirect("IN_StockItem.aspx")
            End If
        End If

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub LoadItemMaster(ByVal Sender As Object, ByVal E As EventArgs)
        ItemCode = lstItemCode.SelectedItem.Value
        If ItemCode <> "" Then
            DisplayData()
            DisableControl()
        Else
            Desc.Text = ""
            Status.Text = ""
            CreateDate.Text = ""
            UpdateDate.Text = ""
            UpdateBy.Text = ""
            InitialCost.Text = "0"
            HighestCost.Text = "0"
            LowestCost.Text = "0"
            AvrgCost.Text = "0"
            LatestCost.Text = "0"
            purchaseACNo.Text = ""
            IssueACNo.Text = ""
            LatestPrice.Text = "0"
            AvrgPrice.Text = "0"
            DiffAverageCost.Text = "0"
            FixedPrice.Text = "0"
            HandQty.Text = "0"
            HoldQty.Text = "0"
            OrderQty.Text = "0"
            ReorderLvl.Text = "0"
            ReorderQty.Text = "0"
            Remark.Text = ""
            txtBin.Text = ""
            txtFuelMeter.Text = ""
            chkFertInd.Checked = False
            LatestCostFinal.Text = "0"
            LatestCostSecond.Text = "0"
            LatestCostInitial.Text = "0"
            txtLifespan.Text = "0"
            QtyBin1.Text = "0"
            QtyBin2.Text = "0"
            QtyBin3.Text = "0"
            QtyBin4.Text = "0"
            QtyBin5.Text = "0"
            QtyBin6.Text = "0"
            QtyBin7.Text = "0"
            QtyBin8.Text = "0"
            QtyBin9.Text = "0"
            QtyBin10.Text = "0"
        End If
        DisplayDropDown()

    End Sub

    Sub UpdateData(ByVal strAction As String)
        Dim FuelTypeInd As String
        Dim UseSellPrice As String
        Dim blnDupKey As Boolean = False
        Dim strStatus As String
        Dim strParam As String
        Dim strCompany As String = Session("SS_COMPANY")
        Dim strLocation As String = Session("SS_LOCATION")
        Dim strUserId As String = Session("SS_USERID")
        Dim ItmTypeInd As String

        If Fuel_Yes.Checked Then
            FuelTypeInd = objIN.EnumFuelItemIndicator.Fuel
        ElseIf Fuel_Lub.Checked Then
            FuelTypeInd = objIN.EnumFuelItemIndicator.Lubricant
        Else
            FuelTypeInd = objIN.EnumFuelItemIndicator.No
        End If

        If Fixed_Price.Checked Then
            UseSellPrice = objIN.EnumSellingPriceForItem.Fixed
        ElseIf Latest_Price.Checked Then
            UseSellPrice = objIN.EnumSellingPriceForItem.PercentageOfLatestCost
        ElseIf Avrg_Price.Checked Then
            UseSellPrice = objIN.EnumSellingPriceForItem.PercentageOfAverageCost
        End If

        If Itm_Stock.Checked Then
            ItmTypeInd = objIN.EnumInventoryItemType.Stock
        ElseIf Itm_Workshop.Checked Then
            ItmTypeInd = objIN.EnumInventoryItemType.WorkshopItem
        End If

        Select Case strAction
            Case "Save"
                strStatus = IIf(Status.Text = objIN.mtdGetStockItemStatus(objIN.EnumStockItemStatus.Deleted), _
                                    objIN.EnumStockItemStatus.Deleted, _
                                    objIN.EnumStockItemStatus.Active)
            Case "Delete"
                strStatus = IIf(Status.Text = objIN.mtdGetStockItemStatus(objIN.EnumStockItemStatus.Active), _
                                    objIN.EnumStockItemStatus.Deleted, _
                                    objIN.EnumStockItemStatus.Active)

        End Select
       

        strParam = _
        Trim(StckItemCode.Text) & "|" & _
        Trim(Desc.Text) & "|" & _
        Trim(txtBin.Text) & "|" & _
        Trim(ItmTypeInd) & "|" & _
        Trim(lstProdType.SelectedItem.Value) & "|" & _
        Trim(lstProdCat.SelectedItem.Value) & "|" & _
        Trim(FuelTypeInd) & "|" & _
        Trim(lstProdBrand.SelectedItem.Value) & "|" & _
        Trim(lstProdModel.SelectedItem.Value) & "|" & _
        Trim(lstProdMat.SelectedItem.Value) & "|" & _
        Trim(lstStockAnalysis.SelectedItem.Value) & "|" & _
        Trim(lstVehExpense.SelectedItem.Value) & "|" & _
        Trim(lstAccCode.SelectedItem.Value) & "|" & _
        Trim(InventoryUOM.SelectedItem.Value) & "|" & _
        Trim(PurchaseUOM.SelectedItem.Value) & "|" & _
        IIf(Trim(txtFuelMeter.Text) = "", 0, Trim(txtFuelMeter.Text)) & "|" & _
        IIf(Trim(ReorderLvl.Text) = "", 0, Trim(ReorderLvl.Text)) & "|" & _
        IIf(Trim(OrderQty.Text) = "", 0, objGlobal.DBDecimalFormat(OrderQty.Text)) & "|" & _
        IIf(Trim(ReorderQty.Text) = "", 0, Trim(ReorderQty.Text)) & "|" & _
        Trim(purchaseACNo.Text) & "|" & _
        Trim(IssueACNo.Text) & "|" & _
        Trim(UseSellPrice) & "|" & _
        IIf(Trim(FixedPrice.Text) = "", 0, Trim(FixedPrice.Text)) & "|" & _
        IIf(Trim(LatestPrice.Text) = "", 0, Trim(LatestPrice.Text)) & "|" & _
        IIf(Trim(AvrgPrice.Text) = "", 0, Trim(AvrgPrice.Text)) & "|" & _
        Trim(Remark.Text) & "|" & _
        Trim(strStatus) & "|" & _
        Trim(txtFuelMeter.Text) & "|" & _
        IIf(chkFertInd.Checked = True, objIN.EnumFertilizerInd.Yes, objIN.EnumFertilizerInd.No)  & "|" & _
        IIf(Trim(txtLifespan.Text) = "", 0, objGlobal.DBDecimalFormat(txtLifespan.Text))

        Try
            intErrNo = objIN.mtdUpdInvItemsDetails(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_ItemList_GET, _
                                                strUserId, _
                                                strLocation, _
                                                strParam, _
                                                blnDupKey, _
                                                blnUpdate.Text)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_stockItem.aspx")
        End Try

        If blnDupKey Then
            lblDupMsg.Visible = True
        Else
            Select Case blnUpdate.Text
                Case True
                Case False
                    Response.Redirect("IN_StockItem_Detail.aspx")
            End Select
        End If

    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Save")
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Delete")
        Response.Redirect("IN_StockItem.aspx")
    End Sub

    Sub btnSynchronizeData_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode_GetMaster As String = "IN_CLSSETUP_INVMASTER_DETAILS_GET"
        Dim strOpCode_UpdItem   As String = "IN_CLSSETUP_INVITEM_DETAILS_UPD"
        Dim strParam As String
        Dim strLocation As String = Session("SS_LOCATION")
        Dim strUserId As String = Session("SS_USERID")
        
        strParam = Trim(StckItemCode.Text) & "|" & _
                   Trim(objIN.EnumInventoryItemType.Stock) & "','" & Trim(objIN.EnumInventoryItemType.WorkshopItem)
        Try
            intErrNo = objIN.mtdSynchronizeItemData(strOpCode_GetMaster, _
                                                    strOpCode_UpdItem, _
                                                    strParam, _
                                                    strUserId, _
                                                    strLocation)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_stockItem.aspx")
        End Try
        
        Call LoadItemMaster(Sender, E)
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("IN_StockItem.aspx")
    End Sub

    Sub ItmType_CheckedChanged(ByVal Sender As Object, ByVal E As EventArgs)  
        If Itm_Stock.Checked = True Then
            LabelLifespan.Visible = False
            txtLifespan.Visible = False
            LabelHour.Visible = False
        Else
            LabelLifespan.Visible = True
            txtLifespan.Visible = True
            LabelHour.Visible = True
        End If
    End Sub

End Class
