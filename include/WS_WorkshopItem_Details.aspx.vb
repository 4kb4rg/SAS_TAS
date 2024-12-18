
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Math  

Public Class WS_WorkshopItemDet : Inherits Page

    Dim dsStockItem As DataSet

    Protected objWS As New agri.WS.clsSetup()
    Protected objIN As New agri.IN.clsSetup()
    Protected objGL As New agri.GL.clsSetup()
    Protected objAdmin As New agri.Admin.clsUOM()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()


    Protected WithEvents blnUpdate As Label
    Protected WithEvents StckItemCode As TextBox
    Protected WithEvents Desc As TextBox
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents InitialCost As Label
    Protected WithEvents HighestCost As Label
    Protected WithEvents LowestCost As Label
    Protected WithEvents AvrgCost As Label
    Protected WithEvents LatestCost As Label
    Protected WithEvents DiffAverageCost As Label
    Protected WithEvents purchaseACNo As TextBox
    Protected WithEvents IssueACNo As TextBox
    Protected WithEvents LatestPrice As TextBox
    Protected WithEvents AvrgPrice As TextBox
    Protected WithEvents FixedPrice As TextBox
    Protected WithEvents HandQty As Label
    Protected WithEvents HoldQty As Label
    Protected WithEvents OrderQty As Label
    Protected WithEvents txtBin As TextBox
    Protected WithEvents txtFuelMeter As TextBox
    Protected WithEvents ReorderLvl As TextBox
    Protected WithEvents ReorderQty As TextBox
    Protected WithEvents Remark As TextBox
    Protected WithEvents Fuel_Yes As RadioButton
    Protected WithEvents Fuel_No As RadioButton
    Protected WithEvents Fuel_Lub As RadioButton
    Protected WithEvents Fixed_Price As RadioButton
    Protected WithEvents Latest_Price As RadioButton
    Protected WithEvents Avrg_Price As RadioButton
    Protected WithEvents lstProdType As DropDownList
    Protected WithEvents lstProdCat As DropDownList
    Protected WithEvents lstProdBrand As DropDownList
    Protected WithEvents lstProdModel As DropDownList
    Protected WithEvents lstProdMat As DropDownList
    Protected WithEvents lstStockAnalysis As DropDownList
    Protected WithEvents lstVehExpense As DropDownList
    Protected WithEvents PurchaseUOM As DropDownList
    Protected WithEvents lstItemCode As DropDownList
    Protected WithEvents InventoryUOM As DropDownList
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents Delete As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents lblCode As Label
    Protected WithEvents lblChargeTo As Label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents label2 As Label
    Protected WithEvents label3 As Label
    Protected WithEvents label5 As Label
    Protected WithEvents label6 As Label
    Protected WithEvents label7 As Label
    Protected WithEvents label8 As Label
    Protected WithEvents label9 As Label
    Protected WithEvents label10 As Label

    Protected WithEvents LatestCostFinal As Label
    Protected WithEvents LatestCostSecond As Label
    Protected WithEvents LatestCostInitial As Label

    Protected WithEvents lblErrMessage as Label 
    Protected WithEvents SynchronizeData as ImageButton

    Dim strOppCd_GET As String = "IN_CLSSETUP_INVITEM_DETAILS_GET"
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

    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New DataSet()

    Dim ItemCode As String
    Dim strParam As String
    Dim intErrNo As Integer
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intWSAR As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intWSAR = Session("SS_WSAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSItem), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrAccCode.Visible = False
            onload_GetLangCap()
            If Not IsPostBack Then
                If Request.QueryString("ID") <> "" Then
                    ItemCode = Request.QueryString("ID")
                End If

                If ItemCode <> "" Then
                    Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');" 'csloo
                    DisplayData()
                    DisplayDropDown()
                    BindItemCodeDropList()
                    DisableControl()
                    blnUpdate.Text = True
                    StckItemCode.ReadOnly = True
                Else
                    BindItemCodeDropList()
                    ItemCode = lstItemCode.SelectedItem.Value
                    DisplayDropDown()
                    blnUpdate.Text = False
                    Delete.Visible = False
                    SynchronizeData.Visible = False 
                End If
            End If
        End If
    End Sub



    Sub onload_GetLangCap()
        GetEntireLangCap()

        label2.Text = GetCaption(objLangCap.EnumLangCap.ProdType)
        label3.Text = GetCaption(objLangCap.EnumLangCap.ProdCat)
        label5.Text = GetCaption(objLangCap.EnumLangCap.ProdBrand)
        label6.Text = GetCaption(objLangCap.EnumLangCap.ProdModel)
        label7.Text = GetCaption(objLangCap.EnumLangCap.ProdMat)
        label8.Text = GetCaption(objLangCap.EnumLangCap.StockAnalysis)
        label9.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_WORKSHOPITEM_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=ws/setup/ws_workshopitem_detail.aspx")
        End Try

    End Sub



    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
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
        Dim strSearch As String 
        
       
        strParam = ItemCode & "|" & objWS.EnumInventoryItemType.WorkshopItem 
        If Not Request.QueryString("ID") = "" Then
            If pv_blnIsMaster = False Then
                strOppCd_GET = "WS_CLSSETUP_INVITEM_DETAILS_GET" 
            Else
                strOppCd_GET = "IN_CLSSETUP_INVMASTER_DETAILS_GET"
            End If
        Else
            strOppCd_GET = "IN_CLSSETUP_INVMASTER_DETAILS_GET"
        End If

        If strOppCd_GET = "WS_CLSSETUP_INVITEM_DETAILS_GET" Then
            strSearch = " AND itm.LocCode = '" & strLocation & "'"
            strParam = strParam & "|" & strSearch

            Try
                intErrNo = objWS.mtdGetMasterDetailLoc(strOppCd_GET, strParam, objDataSet)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_WORKSHOPITEM_DETAILS&errmesg=" & Exp.ToString() & "&redirect=WS/Setup/WS_WorkShopItem_List.aspx")
            End Try
        Else

            strParam = strParam & "|" & Trim(strLocation) 
            Try
                intErrNo = objWS.mtdGetMasterDetail(strOppCd_GET, strParam, objDataSet)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_WORKSHOPITEM_DETAILS&errmesg=" & Exp.ToString() & "&redirect=WS/Setup/WS_WorkShopItem_List.aspx")
            End Try
        End If 

        Return objDataSet
    End Function

    Sub DisableControl()
        lstItemCode.Enabled = False 
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

        If dsStockItem.Tables(0).Rows(0).Item("Status") = objWS.EnumStockItemStatus.Deleted Then
            SynchronizeData.Visible = False 
            Save.Visible = False
            lstAccCode.Enabled = False

            Delete.ImageUrl = "../../images/butt_Undelete.gif"
            Delete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');" 'csloo
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

        End If
    End Sub

    Sub DisplayData()
        Dim objMasterDs As New DataSet()
        dsStockItem = LoadData(False)

        StckItemCode.Text = dsStockItem.Tables(0).Rows(0).Item("ItemCode").Trim()
        Desc.Text = dsStockItem.Tables(0).Rows(0).Item("Description").Trim()
        Status.Text = objWS.mtdGetStockItemStatus(dsStockItem.Tables(0).Rows(0).Item("Status"))
        CreateDate.Text = objGlobal.Getlongdate(dsStockItem.Tables(0).Rows(0).Item("CreateDate"))
        UpdateDate.Text = objGlobal.Getlongdate(dsStockItem.Tables(0).Rows(0).Item("UpdateDate"))
        UpdateBy.Text = dsStockItem.Tables(0).Rows(0).Item("UserName")

        InitialCost.Text = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("InitialCost"),0))
        HighestCost.Text = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("HighCost"),0))
        LowestCost.Text  = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("LowCost"),0))
        AvrgCost.Text    = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("AverageCost"),0))
        LatestCost.Text  = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("LatestCost"),0))
       
        purchaseACNo.Text = dsStockItem.Tables(0).Rows(0).Item("purchaseACcNo").Trim()
        IssueACNo.Text = dsStockItem.Tables(0).Rows(0).Item("IssueACcNo").Trim()        
        
        LatestPrice.Text = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("SellLatestCost"),0))
        AvrgPrice.Text = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("SellAverageCost"),0))
        DiffAverageCost.Text = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("DiffAverageCost"),0))
        FixedPrice.Text = ObjGlobal.DisplayForEditCurrencyFormat(dsStockItem.Tables(0).Rows(0).Item("SellFixedPrice"))
        
        HandQty.Text = ObjGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyonHand")),5)
        HoldQty.Text = ObjGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyOnHold")),5)
        OrderQty.Text = ObjGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsStockItem.Tables(0).Rows(0).Item("QtyOnOrder")),5)

        Remark.Text = dsStockItem.Tables(0).Rows(0).Item("Remark").Trim()
        txtBin.Text = dsStockItem.Tables(0).Rows(0).Item("Bin").Trim()
     
        ReorderLvl.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("ReOrderLevel"))
        ReorderQty.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("QtyReOrder"))
        txtFuelMeter.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("FuelMeterReading"))
     
        LatestCostFinal.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("LatestCostFinal"))    
        LatestCostSecond.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("LatestCostSecond"))       
        LatestCostInitial.Text = ObjGlobal.GetIDDecimalSeparator(dsStockItem.Tables(0).Rows(0).Item("LatestCostInitial"))        

        If dsStockItem.Tables(0).Rows(0).Item("FuelTypeInd") = objWS.EnumFuelItemIndicator.Fuel Then
            Fuel_Yes.Checked = True
        ElseIf dsStockItem.Tables(0).Rows(0).Item("FuelTypeInd") = objWS.EnumFuelItemIndicator.Lubricant Then
            Fuel_Lub.Checked = True
        Else
            Fuel_No.Checked = True
        End If

        If dsStockItem.Tables(0).Rows(0).Item("UsePrice") = objWS.EnumSellingPriceForItem.Fixed Then
            Fixed_Price.Checked = True
        ElseIf dsStockItem.Tables(0).Rows(0).Item("UsePrice") = objWS.EnumSellingPriceForItem.PercentageOfLatestCost Then
            Latest_Price.Checked = True
        ElseIf dsStockItem.Tables(0).Rows(0).Item("UsePrice") = objWS.EnumSellingPriceForItem.PercentageOfAverageCost Then
            Avrg_Price.Checked = True
        End If

        objMasterDs = LoadData(True)

        If objMasterDs.Tables(0).Rows.Count > 0 Then
            If objMasterDs.Tables(0).Rows(0).Item("Status") = objWS.EnumStockItemStatus.Deleted And _
                dsStockItem.Tables(0).Rows(0).Item("Status") = objWS.EnumStockItemStatus.Deleted Then
                SynchronizeData.Visible = False 
                Delete.Visible = False
            End If
        End If
    End Sub

    Sub DisplayDropDown()

        BindDropList(strOppCd_ProdType_GET, "ProdTypeCode", "ProdTypeCode", objWS.EnumInventoryMasterType.ProductType, _
                       objGlobal.EnumModule.Inventory, objWS.EnumProductTypeStatus.Active, lstProdType)
        BindDropList(strOppCd_ProdCat_GET, "ProdCatCode", "ProdCatCode", objWS.EnumInventoryMasterType.ProductCategory, _
                        objGlobal.EnumModule.Inventory, objWS.EnumProductCategoryStatus.Active, lstProdCat)
        BindDropList(strOppCd_ProdBrand_GET, "ProdBrandCode", "ProdBrandCode", objWS.EnumInventoryMasterType.ProductBrand, _
                        objGlobal.EnumModule.Inventory, objWS.EnumProductBrandStatus.Active, lstProdBrand)
        BindDropList(strOppCd_ProdModel_GET, "ProdModelCode", "ProdModelCode", objWS.EnumInventoryMasterType.ProductModel, _
                        objGlobal.EnumModule.Inventory, objWS.EnumProductModelStatus.Active, lstProdModel)
        BindDropList(strOppCd_ProdMaterial_GET, "ProdMatCode", "ProdMatCode", objWS.EnumInventoryMasterType.ProductMaterial, _
                        objGlobal.EnumModule.Inventory, objWS.EnumProductMaterialStatus.Active, lstProdMat)
        BindDropList(strOppCd_StockAna_GET, "StockAnalysisCode", "StockAnalysisCode", objWS.EnumInventoryMasterType.StockAnalysis, _
                        objGlobal.EnumModule.Inventory, objWS.EnumStockAnalysisStatus.Active, lstStockAnalysis)
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
                Case objWS.EnumInventoryMasterType.ProductType
                    TblAlias = "PType"
                Case objWS.EnumInventoryMasterType.ProductCategory
                    TblAlias = "PCat"
                Case objWS.EnumInventoryMasterType.ProductBrand
                    TblAlias = "PBrand"
                Case objWS.EnumInventoryMasterType.ProductModel
                    TblAlias = "PModel"
                Case objWS.EnumInventoryMasterType.ProductMaterial
                    TblAlias = "PMat"
                Case objWS.EnumInventoryMasterType.StockAnalysis
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

        If ModuleType = objGlobal.EnumModule.GeneralLedger And MasterType = objGL.EnumGLMasterType.AccountCode Then
            strParam = "ORDER BY " & MasterKeyField & "|" & "AND " & TblAlias & ".Status = '" & Status & "' AND " & TblAlias & ".AccType = '1' AND " & TblAlias & ".NurseryInd = '" & objGL.EnumNurseryAccount.No & "' "
        Else
            strParam = "ORDER BY " & MasterKeyField & "|" & "AND " & TblAlias & ".Status = '" & Status & "' "
        End If

        Try
            Select Case ModuleType
                Case objGlobal.EnumModule.Admin
                    intErrNo = objAdmin.mtdGetMasterList(Oppcode, strParam, dsForDropDown)
                Case objGlobal.EnumModule.GeneralLedger
                    intErrNo = objGL.mtdGetMasterList(Oppcode, strParam, MasterType, dsForDropDown)
                Case objGlobal.EnumModule.Inventory
                    intErrNo = objWS.mtdGetMasterList(Oppcode, strParam, MasterType, dsForDropDown)
            End Select
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADDING_ITEMTODROPDOWN&errmesg=" & Exp.ToString() & "&redirect=WS/Setup/WS_WorkShopItem_List.aspx")
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
        drinsert(1) = "Please select a Code "
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        BindToList.DataSource = dsForDropDown.Tables(0)
        BindToList.DataValueField = MasterKeyField
        BindToList.DataTextField = DataTextField
        BindToList.DataBind()

        If SelectedIndex = -1 And Not ItemCode = "" Then
            strParam = "Order by " & MasterKeyField & "|" & "AND " & MasterKeyField & " = '" & _
                        Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField)) & "'"

            Try
                intErrNo = objWS.mtdGetMasterList(Oppcode, strParam, MasterType, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                    BindToList.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0)) & _
                     " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Deleted) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0))))
                Else 
                    BindToList.Items.Add(New ListItem(Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField)) & _
                       " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Unknown) & ") ", Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField))))
                End If
                SelectedIndex = BindToList.Items.Count - 1

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & Exp.ToString() & "&redirect=IN/Setup/WS_WorkShopItem_List.aspx")
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
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow

        SearchStr = "AND ItemType = '" & objIN.EnumInventoryItemType.WorkshopItem & "' AND itm.Status = '" & objIN.EnumStockItemStatus.Active & "' AND Not ItemCode In (Select ItemCode From IN_Item where ItemType =" & objIN.EnumInventoryItemType.WorkshopItem & " AND IN_Item.LocCode='" & strLocation & "' ) "

        strParam = "ORDER BY ItemCode Desc|" & SearchStr

        Try
            intErrNo = objIN.mtdGetMasterList(strOpCdItemCode_Get, strParam, objIN.EnumInventoryMasterType.StockItem, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKITEM&errmesg=" & Exp.ToString() & "&redirect=IN/Setup/IN_stockitem.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If Not ItemCode = "" Then
                If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode"))) = UCase(Trim(objDataSet.Tables(0).Rows(0).Item("ItemCode"))) Then
                    SelectedIndex = intCnt + 1
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

            lstItemCode.Items.Add(New ListItem(Trim(objDataSet.Tables(0).Rows(0).Item("ItemCode")), Trim(objDataSet.Tables(0).Rows(0).Item("ItemCode"))))
            SelectedIndex = lstItemCode.Items.Count - 1
            lstItemCode.SelectedIndex = SelectedIndex

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
            StckItemCode.Text = ""
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
            txtFuelMeter.Text = "0"
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

        If Fuel_Yes.Checked Then
            FuelTypeInd = objWS.EnumFuelItemIndicator.Fuel
        ElseIf Fuel_Lub.Checked Then
            FuelTypeInd = objWS.EnumFuelItemIndicator.Lubricant
        Else
            FuelTypeInd = objWS.EnumFuelItemIndicator.No
        End If

        If Fixed_Price.Checked Then
            UseSellPrice = objWS.EnumSellingPriceForItem.Fixed
        ElseIf Latest_Price.Checked Then
            UseSellPrice = objWS.EnumSellingPriceForItem.PercentageOfLatestCost
        ElseIf Avrg_Price.Checked Then
            UseSellPrice = objWS.EnumSellingPriceForItem.PercentageOfAverageCost
        End If

        Select Case strAction
            Case "Save"
                strStatus = IIf(Status.Text = objWS.mtdGetStockItemStatus(objWS.EnumStockItemStatus.Deleted), _
                                    objWS.EnumStockItemStatus.Deleted, _
                                    objWS.EnumStockItemStatus.Active)
            Case "Delete"
                strStatus = IIf(Status.Text = objWS.mtdGetStockItemStatus(objWS.EnumStockItemStatus.Active), _
                                    objWS.EnumStockItemStatus.Deleted, _
                                    objWS.EnumStockItemStatus.Active)

        End Select
        
        
        strParam = _
        Trim(StckItemCode.Text) & "|" & _
        Trim(Desc.Text) & "|" & _
        Trim(txtBin.Text) & "|" & _
        Trim(objIN.EnumInventoryItemType.WorkshopItem) & "|" & _
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
        Trim(LatestPrice.Text) & "|" & _
        Trim(AvrgPrice.Text) & "|" & _
        Trim(Remark.Text) & "|" & _
        Trim(strStatus) & "|" & _
        Trim(txtFuelMeter.Text) & "|||"

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_WORKSHOPITEM_DETAILS&errmesg=" & Exp.ToString() & "&redirect=IN/Setup/WS_WorkShopItem_List.aspx")
        End Try

        If blnDupKey Then
            lblDupMsg.Visible = True
        Else
            Select Case blnUpdate.Text
                Case True
                    Response.Redirect("WS_WorkshopItem_List.aspx")
                Case False
                    Response.Redirect("WS_WorkshopItem_Detail.aspx")
            End Select
        End If

    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If Trim(lstAccCode.SelectedItem.Value) = "" Then
            lblErrAccCode.Visible = True
        Else
            UpdateData("Save")
        End If
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Delete")
        Response.Redirect("WS_WorkshopItem_List.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WS_WorkshopItem_List.aspx")
    End Sub

    Sub btnSynchronizeData_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode_GetMaster As String = "IN_CLSSETUP_INVMASTER_DETAILS_GET"
        Dim strOpCode_UpdItem   As String = "IN_CLSSETUP_INVITEM_DETAILS_UPD"
        Dim strParam As String
        Dim strLocation As String = Session("SS_LOCATION")
        Dim strUserId As String = Session("SS_USERID")
        
        strParam = Trim(StckItemCode.Text) & "|" & _
                   Trim(objIN.EnumInventoryItemType.WorkshopItem)
        Try
            intErrNo = objIN.mtdSynchronizeItemData(strOpCode_GetMaster, _
                                                    strOpCode_UpdItem, _
                                                    strParam, _
                                                    strUserId, _
                                                    strLocation)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_WorkShopItem_List.aspx")
        End Try
        
        Call LoadItemMaster(Sender, E)
    End Sub

End Class
