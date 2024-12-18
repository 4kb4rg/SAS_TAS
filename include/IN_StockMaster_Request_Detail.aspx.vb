
Imports System
Imports System.Data
Imports System.IO
Imports System.Math
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports agri.PWSystem.clsLangCap


Public Class IN_StockMaster_Request_Detail : Inherits Page
    Protected objCBTrx As New agri.CB.clsTrx()
    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objIN As New agri.IN.clsSetup()
    Protected objGL As New agri.GL.clsSetup()
    Protected objAdmin As New agri.Admin.clsUom()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrPurchaseUOM As Label
    Protected WithEvents lblErrInventoryUOM As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents StckItemCode As TextBox
    Protected WithEvents Desc As TextBox
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label

    Protected WithEvents LblNoDoc As Label
    Protected WithEvents Remark As TextBox
    Protected WithEvents Fuel_Yes As RadioButton
    Protected WithEvents Fuel_No As RadioButton
    Protected WithEvents Fuel_Lub As RadioButton
    Protected WithEvents Fixed_Price As RadioButton
    Protected WithEvents Latest_Price As RadioButton
    Protected WithEvents lstProdType As DropDownList
    Protected WithEvents lstProdCat As DropDownList
    Protected WithEvents lstProdBrand As DropDownList
    Protected WithEvents lstProdModel As DropDownList
    Protected WithEvents lstProdMat As DropDownList
    Protected WithEvents InventoryUOM As DropDownList
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents Delete As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblStockItemCode As Label
    Protected WithEvents lblProdType As Label
    Protected WithEvents lblProdCat As Label
    Protected WithEvents lblProdBrand As Label
    Protected WithEvents lblProdModel As Label
    Protected WithEvents lblProdMat As Label

    'Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator

    Protected WithEvents chkFertInd As CheckBox

    Protected WithEvents LatestCostFinal As Label
    Protected WithEvents LatestCostSecond As Label
    Protected WithEvents LatestCostInitial As Label

    Protected WithEvents lblErrProdType As Label
    Protected WithEvents lblErrProdCat As Label
    Protected WithEvents lblErrProdModel As Label

    Protected WithEvents Itm_Stock As RadioButton
    Protected WithEvents Itm_Workshop As RadioButton
    Protected WithEvents LabelLifespan As Label
    Protected WithEvents LabelHour As Label
    Protected WithEvents txtLifespan As TextBox
    Protected WithEvents lblErrLifespan As Label

    Dim NoDoc As String
    Dim strParam As String

    Dim dsStockItem As DataSet
    Dim objDataSet As New DataSet()
    Dim objItemDs As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intINAR As Integer
    Dim strLocType As String
    Dim strLocLevel As String

    Dim strParamName As String
    Dim MaxNo As String = "0"
    Dim Newno As Long = 0
    Dim NewNoStr As String

    Dim objAdminLoc As New agri.Admin.clsLoc()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intINAR = Session("SS_INAR")
        strLocType = Session("SS_LOCTYPE")
        strLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
            'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItemMaster), intINAR) = False Then
            '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrInventoryUOM.Visible = False
            lblErrProdType.Visible = False
            lblErrProdCat.Visible = False
            lblErrProdModel.Visible = False
            onload_GetLangCap()

            If Not IsPostBack Then
                LblNoDoc.Text = vbNullString
                NoDoc = Request.QueryString("ID")
                If Not NoDoc = "" Then
                    DisplayData(NoDoc)
                    DisplayDropDown()

                    If Len(StckItemCode.Text.Trim) > 0 Then
                        Save.Visible = False
                        Delete.Visible = False
                    Else
                        Save.Visible = True
                        Delete.Visible = True
                    End If

                    If dsStockItem.Tables(0).Rows(0).Item("Status") = objIN.EnumStockItemStatus.Deleted Then
                        Delete.ImageUrl = "../../images/butt_Undelete.gif"
                        DisableControl()
                    Else
                        Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    End If

                    blnUpdate.Text = True
                    StckItemCode.ReadOnly = True
                Else
                    DisplayDropDown()
                    blnUpdate.Text = False
                    Delete.Visible = False
                End If
				
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.StockMaster))
        lblStockItemCode.Text = GetCaption(objLangCap.EnumLangCap.StockItem) & " Code"
        lblProdType.Text = GetCaption(objLangCap.EnumLangCap.ProdType)
        lblProdCat.Text = GetCaption(objLangCap.EnumLangCap.ProdCat)
        lblProdBrand.Text = GetCaption(objLangCap.EnumLangCap.ProdBrand)
        lblProdModel.Text = GetCaption(objLangCap.EnumLangCap.ProdModel)
        lblProdMat.Text = GetCaption(objLangCap.EnumLangCap.ProdMat)

        ' validateCode.ErrorMessage = "Please enter " & lblStockItemCode.Text & "."
        validateDesc.ErrorMessage = "Please enter Deskripsi / Stock Name"

    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=IN/setup/IN_StockMaster.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Protected Function LoadData(ByVal pKode As String) As DataSet
        Dim strParamName As String
        Dim strParamValue As String

        Dim strOppCd_GET As String = "IN_CLSSETUP_INVMASTER_DETAILS_REQUEST_FIND"

        strParamName = "ID|LOCCODE"
        strParamValue = pKode & "|" & strLocation & ""

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOppCd_GET, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objItemDs
    End Function

    Sub DisableControl()
        StckItemCode.Enabled = False
        Desc.Enabled = False
        Remark.Enabled = False
        Fuel_Yes.Enabled = False
        Fuel_No.Enabled = False
        Fuel_Lub.Enabled = False
        lstProdType.Enabled = False
        lstProdCat.Enabled = False
        lstProdBrand.Enabled = False
        lstProdModel.Enabled = False
        lstProdMat.Enabled = False
        InventoryUOM.Enabled = False
        StckItemCode.Enabled = False
        Status.Enabled = False
        CreateDate.Enabled = False
        UpdateDate.Enabled = False
        UpdateBy.Enabled = False

        Itm_Stock.Enabled = False
        Itm_Workshop.Enabled = False

    End Sub

    Sub DisplayData(ByVal pKode As String)
        dsStockItem = LoadData(pKode)
        If dsStockItem.Tables(0).Rows.Count > 0 Then
            LblNoDoc.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("NoDoc"))
            StckItemCode.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("ItemCode"))
            Desc.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("Description"))
            Status.Text = Trim(objCBTrx.mtdGetCashBankStatus(dsStockItem.Tables(0).Rows(0).Item("Status"))) 'Trim(objIN.mtdGetStockItemStatus(dsStockItem.Tables(0).Rows(0).Item("Status")))
            CreateDate.Text = Trim(objGlobal.GetLongDate(dsStockItem.Tables(0).Rows(0).Item("CreateDate")))
            UpdateDate.Text = Trim(objGlobal.GetLongDate(dsStockItem.Tables(0).Rows(0).Item("UpdateDate")))
            UpdateBy.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("UserName"))
            Remark.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("Remark"))

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

            If dsStockItem.Tables(0).Rows(0).Item("ItemType") = objIN.EnumInventoryItemType.Stock Then
                Itm_Stock.Checked = True
                Itm_Workshop.Checked = False
            ElseIf dsStockItem.Tables(0).Rows(0).Item("ItemType") = objIN.EnumInventoryItemType.WorkshopItem Then
                Itm_Stock.Checked = False
                Itm_Workshop.Checked = True
            End If
        End If
    End Sub

    Sub DisplayDropDown()
        Dim strOppCd_ProdType_GET As String = "IN_CLSSETUP_PRODTYPE_LIST_GET"
        Dim strOppCd_ProdCat_GET As String = "IN_CLSSETUP_PRODCAT_LIST_GET"
        Dim strOppCd_ProdBrand_GET As String = "IN_CLSSETUP_PRODBRAND_LIST_GET"
        Dim strOppCd_ProdModel_GET As String = "IN_CLSSETUP_PRODMODEL_LIST_GET"
        Dim strOppCd_ProdMaterial_GET As String = "IN_CLSSETUP_PRODMAT_LIST_GET"
        Dim strOppCd_StockAna_GET As String = "IN_CLSSETUP_STOCKANA_LIST_GET"
        Dim strOppCd_VehExpense_GET As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim strOppCd_AccountCode_GET As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strOppCd_UOM_GET As String = "ADMIN_CLSUOM_UOM_LIST_GET"

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
        BindDropList(strOppCd_UOM_GET, "UOMCode", "UOMCode", "0", _
                         objGlobal.EnumModule.Admin, objAdmin.EnumUOMStatus.Active, InventoryUOM)
    End Sub

    Sub BindDropList(ByVal Oppcode As String, ByVal ItemKeyField As String, ByVal MasterKeyField As String, ByVal MasterType As Integer, _
                     ByVal ModuleType As Integer, ByVal Status As Integer, ByVal BindToList As DropDownList)

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_StockMaster.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If Not NoDoc = "" Then
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

        If SelectedIndex = -1 And Not NoDoc = "" Then
            strParam = "Order by " & MasterKeyField & "|" & "AND " & MasterKeyField & " = '" & _
                        Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField)) & "'"

            Try
                intErrNo = objIN.mtdGetMasterList(Oppcode, strParam, MasterType, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then
                    BindToList.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0)) & _
                     " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Deleted) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0))))
                    SelectedIndex = BindToList.Items.Count - 1
                Else
                    SelectedIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_StockMaster.aspx")
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

    Sub UpdateData(ByVal strAction As String)
        Dim strOppCd_ADD As String = "IN_CLSSETUP_INVMASTER_DETAILS_REQUEST_ADD"
        Dim strOppCd_UPD As String = "IN_CLSSETUP_INVMASTER_DETAILS_REQUEST_UPD"
        Dim strOppCd_GETNEWNO As String = "IN_CLSSETUP_INVMASTER_DETAILS_REQUEST_GET_NEWNUMBER"

        Dim FuelTypeInd As String
        Dim UseSellPrice As String
        Dim blnDupKey As Boolean = False
        Dim strStatus As String

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

        If Trim(InventoryUOM.SelectedItem.Value) = "" Then
            lblErrInventoryUOM.Visible = True
            Exit Sub
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


        If Len(Trim(LblNoDoc.Text)) = 0 Then
            strParamName = "LOCCODE|TAHUN|BULAN"
            strParam = Trim(strLocation) & "|" & Now.Year & "|" & Now.Month

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOppCd_GETNEWNO, _
                                                    strParamName, _
                                                    strParam, _
                                                    objItemDs)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_StockMaster.aspx")
            End Try

            If objItemDs.Tables(0).Rows.Count > 0 Then
                MaxNo = Trim(objItemDs.Tables(0).Rows(0).Item("NoDoc"))
            Else
                MaxNo = "0"
            End If

            Newno = CDbl(0 & MaxNo) + 1
            NewNoStr = strLocation & Now.Year & Now.Month & Strings.Right("0000" & CStr(Newno), 4)

            ''INSERT
            strParamName = vbNullString
            strParam = vbNullString

            strParamName = "NODOC|LOCCODE|ITEMCODE|DESCRIPTION|FUELTYPE|ITEMTYPE|PRODTYPECODE|PRODCATCODE|PRODBRANDCODE|PRODMODELCODE|PRODMATCODE|UOMCODE|REMARK|STATUS|CREATEDATE|UPDATEDATE|UPDATEID|APPROVED"
            strParam = Trim(NewNoStr) & "|" & _
                        Trim(strLocation) & "|" & _
                        Trim(StckItemCode.Text) & "|" & _
                        Trim(Desc.Text).Replace("'", "''") & "|" & _
                        Trim(FuelTypeInd) & "|" & _
                        Trim(ItmTypeInd) & "|" & _
                        Trim(lstProdType.SelectedItem.Value) & "|" & _
                        Trim(lstProdCat.SelectedItem.Value) & "|" & _
                        Trim(lstProdBrand.SelectedItem.Value) & "|" & _
                        Trim(lstProdModel.SelectedItem.Value) & "|" & _
                        Trim(lstProdMat.SelectedItem.Value) & "|" & _
                        Trim(InventoryUOM.SelectedItem.Value) & "|" & _
                        Trim(Remark.Text).Replace("'", "''") & "|" & _
                        Trim(strStatus) & "|" & _
                        Date.Now & "|" & _
                        Date.Now & "|" & _
                        Trim(strUserId) & "|" & _
                        ""
            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOppCd_ADD, _
                                                        strParamName, _
                                                        strParam)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_StockMaster.aspx")
            End Try

            LblNoDoc.Text = NewNoStr
        Else

            ''UPDATE
            strParamName = vbNullString
            strParam = vbNullString

            strParamName = "NODOC|LOCCODE|ITEMCODE|DESCRIPTION|FUELTYPE|ITEMTYPE|PRODTYPECODE|PRODCATCODE|PRODBRANDCODE|PRODMODELCODE|PRODMATCODE|UOMCODE|REMARK|STATUS|UPDATEDATE|UPDATEID"
            strParam = Trim(LblNoDoc.Text) & "|" & _
                        Trim(strLocation) & "|" & _
                        Trim(StckItemCode.Text) & "|" & _
                        Trim(Desc.Text) & "|" & _
                        Trim(FuelTypeInd) & "|" & _
                        Trim(ItmTypeInd) & "|" & _
                        Trim(lstProdType.SelectedItem.Value) & "|" & _
                        Trim(lstProdCat.SelectedItem.Value) & "|" & _
                        Trim(lstProdBrand.SelectedItem.Value) & "|" & _
                        Trim(lstProdModel.SelectedItem.Value) & "|" & _
                        Trim(lstProdMat.SelectedItem.Value) & "|" & _
                        Trim(InventoryUOM.SelectedItem.Value) & "|" & _
                        Trim(Remark.Text) & "|" & _
                        Trim(strStatus) & "|" & _
                        Date.Now & "|" & _
                        Trim(strUserId)

            Try

                intErrNo = objGLTrx.mtdInsertDataCommon(strOppCd_UPD, _
                                                        strParamName, _
                                                        strParam)


            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_StockMaster.aspx")
            End Try

        End If

        DisplayData(LblNoDoc.Text)
    End Sub

    Sub DeleteData(ByVal strAction As String)
        Dim strOppCd_DEL As String = "IN_CLSSETUP_INVMASTER_DETAILS_REQUEST_DEL"

        strParamName = vbNullString
        strParam = vbNullString

        strParamName = "NODOC|LOCCODE|STATUS|UPDDATE|USERID"
        strParam = Trim(LblNoDoc.Text) & "|" & _
                    Trim(strLocation) & "|" & _
                    4 & "|" & _
                    Date.Now & "|" & _
                    Trim(strUserId)

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOppCd_DEL, _
                                                    strParamName, _
                                                    strParam)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_StockMaster.aspx")
        End Try
        DisplayData(LblNoDoc.Text)
    End Sub

    Sub bEnable()
        With Me
            .Save.Enabled = True
            '.StckItemCode.Enabled = True
            .Desc.Enabled = True
            .lstProdType.Enabled = True
            .lstProdCat.Enabled = True
            .lstProdBrand.Enabled = True
            .lstProdModel.Enabled = True
            .lstProdMat.Enabled = True
            .InventoryUOM.Enabled = True
            .Remark.Enabled = True
        End With
    End Sub

    Sub bDisable()
        With Me
            '.StckItemCode.Enabled = False
            .Desc.Enabled = False
            .lstProdType.Enabled = False
            .lstProdCat.Enabled = False
            .lstProdBrand.Enabled = False
            .lstProdModel.Enabled = False
            .lstProdMat.Enabled = False
            .InventoryUOM.Enabled = False
            .Remark.Enabled = False
        End With
    End Sub

    Sub lClear()
        With Me
            .StckItemCode.Text = vbNullString
            .LblNoDoc.Text = vbNullString
            .Desc.Text = vbNullString
            .lstProdType.Text = vbNullString
            .lstProdCat.Text = vbNullString
            .lstProdBrand.Text = vbNullString
            .lstProdModel.Text = vbNullString
            .lstProdMat.Text = vbNullString
            .InventoryUOM.Text = vbNullString
            .Remark.Text = vbNullString
            .Desc.Focus()
        End With
    End Sub

    Sub btnNew_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        bEnable()
        lClear()
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'If lstProdType.SelectedIndex = 0 Then
        '    lblErrProdType.Visible = True
        '    Exit Sub
        'ElseIf lstProdCat.SelectedIndex = 0 Then
        '    lblErrProdCat.Visible = True
        '    Exit Sub
        If lstProdModel.SelectedIndex = 0 Then
            lblErrProdModel.Visible = True
            Exit Sub
        Else
            UpdateData("Save")
        End If
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        DeleteData("Delete")
        Response.Redirect("IN_StockMaster_Request.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("IN_StockMaster_Request.aspx")
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
