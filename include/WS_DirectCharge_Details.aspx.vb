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


Public Class WS_DirectChargeDet : Inherits Page

    Dim dsStockItem As DataSet

    Protected objIN As New agri.IN.clsSetup()
    Protected objWS As New agri.WS.clsSetup()
    Protected objGL As New agri.GL.clsSetup()
    Protected objAdmin As New agri.Admin.clsUOM()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents ErrorMessage As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents DCCode As TextBox
    Protected WithEvents Desc As TextBox
    Protected WithEvents Status As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblInitialCost As Label
    Protected WithEvents lblHighestCost As Label
    Protected WithEvents lblLowestCost As Label
    Protected WithEvents lblAvrgCost As Label
    Protected WithEvents lblLatestCost As Label
    Protected WithEvents DiffAverageCost As Label
    Protected WithEvents purchaseACNo As TextBox
    Protected WithEvents IssueACNo As TextBox
    Protected WithEvents lstUOM As DropDownList
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents Delete As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents lstItemCode As DropDownList
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblHead As Label
    Protected WithEvents lblDCItemCode As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblChargeTo As Label
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator

    Protected WithEvents SynchronizeData as ImageButton 

    Dim strOppCd_GET As String = "IN_CLSSETUP_INVITEM_DETAILS_GET"
    Dim strOppCd_DirectChargeList_GET As String = "IN_CLSSETUP_INVITEM_LIST_GET"
    Dim strOppCd_ADD As String = "IN_CLSSETUP_INVITEM_DETAILS_ADD"
    Dim strOppCd_UPD As String = "IN_CLSSETUP_INVITEM_DETAILS_UPD"
    Dim strOppCd_AccountCode_GET As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
    Dim strOppCd_UOM_GET As String = "ADMIN_CLSUOM_UOM_LIST_GET"

    Dim ItemCode As String
    Dim strParam As String
    Dim intErrNo As Integer

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New DataSet()
    Dim objLoc As New agri.Admin.clsLoc()

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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItem), intWSAR) = False Then
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

                Else
                    DisplayDropDown()
                    BindItemCodeDropList()
                    ItemCode = lstItemCode.SelectedItem.Value
                    DisplayDropDown()
                    blnUpdate.Text = False
                    Delete.Visible = False

                    SynchronizeData.Visible = False 
                End If
                DisableControl()
            End If
        End If
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblDCItemCode.Text = GetCaption(objLangCap.EnumLangCap.DirectChgItem) & lblCode.Text
        lblDesc.Text = GetCaption(objLangCap.EnumLangCap.DirectChgItemDesc)
        lblAccCodeTag.Text = lblChargeTo.Text & GetCaption(objLangCap.EnumLangCap.Account)
        lblHead.Text = UCase(GetCaption(objLangCap.EnumLangCap.DirectChgItem))

        validateCode.ErrorMessage = lblPleaseEnter.Text & lblDCItemCode.Text
        validateDesc.ErrorMessage = lblPleaseEnter.Text & lblDesc.Text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_DIRECTCHARGE_DETAIL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=ws/setup/ws_directcharge_detail.aspx")
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

        strParam = ItemCode & "|" & objWS.EnumInventoryItemType.DirectCharge
        
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
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_DIRECTCHARGEITEM_DETAIL&errmesg=" & Exp.ToString() & "&redirect=WS/Setup/WS_DirectCharge.aspx")
            End Try
        Else
            strParam = strParam & "|" & Trim(strLocation) 
            Try
                intErrNo = objWS.mtdGetMasterDetail(strOppCd_GET, strParam, objDataSet)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_DIRECTCHARGEITEM_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_DirectCharge.aspx")
            End Try
        End If 

        Return objDataSet
    End Function

    Sub DisableControl()

        DCCode.Enabled = False

        If Status.Text = objIN.mtdGetStockItemStatus(objIN.EnumStockItemStatus.Deleted) Then
            SynchronizeData.Visible = False 
            Save.Visible = False

            Delete.ImageUrl = "../../images/butt_Undelete.gif"
            Delete.Attributes("onclick") = ""
            lstUOM.Enabled = False
            lstAccCode.Enabled = False
            Desc.Enabled = False
            purchaseACNo.Enabled = False
            IssueACNo.Enabled = False
        Else
            Save.Visible = True 

            Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            lstUOM.Enabled = True
            lstAccCode.Enabled = True
            Desc.Enabled = True
            purchaseACNo.Enabled = True
            IssueACNo.Enabled = True
        End If
    End Sub

    Sub DisplayData()
        Dim objMasterDs As New DataSet()
        dsStockItem = LoadData(False)

        DCCode.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("ItemCode"))
        Desc.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("Description"))
        Status.Text = Trim(objWS.mtdGetStockItemStatus(dsStockItem.Tables(0).Rows(0).Item("Status")))
        CreateDate.Text = Trim(objGlobal.Getlongdate(dsStockItem.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = Trim(objGlobal.Getlongdate(dsStockItem.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("UserName"))


        lblInitialCost.Text = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("InitialCost"),0))
        lblHighestCost.Text = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("HighCost"),0))
        lblLowestCost.Text = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("LowCost"),0))
        lblAvrgCost.Text = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("AverageCost"),0))
        DiffAverageCost.Text = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("DiffAverageCost"),0))
        lblLatestCost.Text = ObjGlobal.GetIDDecimalSeparator(ROUND(dsStockItem.Tables(0).Rows(0).Item("LatestCost"),0))
        purchaseACNo.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("purchaseACcNo"))
        IssueACNo.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("IssueACcNo"))

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

        BindDropList(strOppCd_AccountCode_GET, "ActCode", "AccCode", objGL.EnumGLMasterType.AccountCode, _
                        objGlobal.EnumModule.GeneralLedger, objGL.EnumAccountCodeStatus.Active, lstAccCode)
        BindDropList(strOppCd_UOM_GET, "UOMCode", "UOMCode", "0", _
                         objGlobal.EnumModule.Admin, objAdmin.EnumUOMStatus.Active, lstUOM)
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

        SearchStr = "AND ItemType = '" & objIN.EnumInventoryItemType.DirectCharge & "' AND itm.Status = '" & objIN.EnumStockItemStatus.Active & "' AND Not ItemCode In (Select ItemCode From IN_Item where ItemType =" & objIN.EnumInventoryItemType.DirectCharge & " AND IN_Item.LocCode='" & strLocation & "' ) " 'csloo

        strParam = "ORDER BY ItemCode Desc|" & SearchStr

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
                If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode"))) = UCase(Trim(objDataSet.Tables(0).Rows(0).Item("ItemCode"))) Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Please select Item Code"
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
            lstUOM.Enabled = False 
            Desc.Enabled = False
            purchaseACNo.Enabled = False
            IssueACNo.Enabled = False
        Else
            DCCode.Text = ""
            Desc.Text = ""
            Status.Text = ""
            CreateDate.Text = ""
            UpdateDate.Text = ""
            UpdateBy.Text = ""
            lblInitialCost.Text = "0"
            lblHighestCost.Text = "0"
            lblLowestCost.Text = "0"
            lblAvrgCost.Text = "0"
            DiffAverageCost.Text = "0"
            lblLatestCost.Text = "0"
            purchaseACNo.Text = ""
            IssueACNo.Text = ""
        End If
        DisplayDropDown()

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

        strParam = "ORDER BY " & MasterKeyField & "|" & "AND " & TblAlias & ".Status =" & Status

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_DirectCharge.aspx")
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
                    SelectedIndex = BindToList.Items.Count - 1
                Else 
                    SelectedIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_DirectCharge.aspx")
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
        Dim FuelTypeInd As String
        Dim UseSellPrice As String
        Dim blnDupKey As Boolean = False
        Dim strStatus As String
        Dim strParam As String

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
        Trim(lstItemCode.SelectedItem.Value) & "|" & _
        Trim(Desc.Text) & "||" & _
        Trim(objIN.EnumInventoryItemType.DirectCharge) & "|||||||||" & _
        Trim(lstAccCode.SelectedItem.Value) & "|" & _
        Trim(lstUOM.SelectedItem.Value) & "||0|0|0|0|" & _
        Trim(purchaseACNo.Text) & "|" & _
        Trim(IssueACNo.Text) & "||0|0|0||" & _
        Trim(strStatus) & "||||"

        Try
            intErrNo = objIN.mtdUpdInvItemsDetails(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_DirectChargeList_GET, _
                                                    strUserId, _
                                                    strLocation, _
                                                    strParam, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_DIRECTCHARGEITEM_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_DirectCharge.aspx")
        End Try

        If blnDupKey Then
            lblDupMsg.Visible = True
        Else
            Select Case blnUpdate.Text
                Case True
                    Response.Redirect("WS_DirectCharge.aspx")
                Case False
                    Response.Redirect("WS_DirectCharge_Detail.aspx")
            End Select

        End If

    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Save")

    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Delete")
        Response.Redirect("WS_DirectCharge.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WS_DirectCharge.aspx")
    End Sub

    Sub btnSynchronizeData_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode_GetMaster As String = "IN_CLSSETUP_INVMASTER_DETAILS_GET"
        Dim strOpCode_UpdItem   As String = "IN_CLSSETUP_INVITEM_DETAILS_UPD"
        Dim strParam As String
        Dim strLocation As String = Session("SS_LOCATION")
        Dim strUserId As String = Session("SS_USERID")
        
        strParam = Trim(DCCode.Text) & "|" & _
                   Trim(objIN.EnumInventoryItemType.DirectCharge)
        Try
            intErrNo = objIN.mtdSynchronizeItemData(strOpCode_GetMaster, _
                                                    strOpCode_UpdItem, _
                                                    strParam, _
                                                    strUserId, _
                                                    strLocation)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_DirectCharge.aspx")
        End Try
        
        Call LoadItemMaster(Sender, E)
    End Sub



End Class
