
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

Public Class IN_DirectCharge_Det : Inherits Page

    Dim dsStockItem As Dataset

    Protected objIN As New agri.IN.clsSetup()
    Protected objGL As New agri.GL.clsSetup()
    Protected objAdmin As New agri.Admin.clsUOM()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents DCCode As textBox
    Protected WithEvents Desc As textBox
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblInitialCost As Label
    Protected WithEvents lblHighestCost As Label
    Protected WithEvents lblLowestCost As Label
    Protected WithEvents lblAvrgCost As Label
    Protected WithEvents lblLatestCost As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents DiffAverageCost As Label
    Protected WithEvents purchaseACNo As TextBox
    Protected WithEvents IssueACNo As TextBox
    Protected WithEvents lstUOM As DropDownList
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents Delete As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents lstItemCode As DropDownList
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblDCCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator

    Dim strOppCd_GET As String = "IN_CLSSETUP_INVITEM_DETAILS_GET"
    Dim strOppCd_ADD As String = "IN_CLSSETUP_INVITEM_DETAILS_ADD"
    Dim strOppCd_UPD As String = "IN_CLSSETUP_INVITEM_DETAILS_UPD"
    Dim strOppCd_DirectChargeList_GET As String = "IN_CLSSETUP_INVITEM_LIST_GET"
    Dim strOppCd_AccountCode_GET As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
    Dim strOppCd_UOM_GET As String = "ADMIN_CLSUOM_UOM_LIST_GET"
    Dim ItemCode As String
    Dim strParam As String
    Dim objDataSet As New Dataset()
    Dim objLangCapDs As New Dataset()
    Dim intErrNo As Integer

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INDirectChargeItem), intINAR) = False Then
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
                    Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Else
                    DisplayDropDown()
                    BindItemCodeDropList()
                    ItemCode = lstItemCode.SelectedItem.Value
                    DisplayDropDown()
                    blnUpdate.Text = False
                    Delete.Visible = False
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.DirectChgItem))
        lblDCCode.Text = GetCaption(objLangCap.EnumLangCap.DirectChgItem) & " Code"
        lblDescription.Text = GetCaption(objLangCap.EnumLangCap.DirectChgItemDesc)
        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account)

        validateCode.ErrorMessage = "Please enter " & lblDCCode.Text & "."
        validateDesc.ErrorMessage = "Please enter " & lblDescription.Text & "."

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
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function



    Protected Function LoadData(ByVal pv_blnIsMaster As Boolean) As DataSet

        Dim strOppCd_GET As String
        strParam = ItemCode & "|" & objIN.EnumInventoryItemType.DirectCharge & "|" & Trim(strLocation)
        If Not Request.QueryString("ID") = "" Then
            If pv_blnIsMaster = False Then
                strOppCd_GET = "IN_CLSSETUP_INVITEM_DETAILS_GET"
            Else
                strOppCd_GET = "IN_CLSSETUP_INVMASTER_DETAILS_GET"
            End If
        Else
            strOppCd_GET = "IN_CLSSETUP_INVMASTER_DETAILS_GET"
        End If

        Try
            intErrNo = objIN.mtdGetMasterDetail(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_DIRECTCHARGEITEM_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_DirectCharge.aspx")
        End Try
        Return objDataSet
    End Function

    Sub DisableControl()

        lstUOM.Enabled = False
        lstAccCode.Enabled = False
        Desc.Enabled = False
        purchaseACNo.Enabled = False
        IssueACNo.Enabled = False
        If dsStockItem.Tables(0).Rows(0).Item("Status") = objIN.EnumStockItemStatus.Deleted Then
            Delete.ImageUrl = "../../images/butt_Undelete.gif"
            Delete.Attributes("onclick") = ""
            Save.Visible = False
            DCCode.Enabled = False
            Status.Enabled = False
            CreateDate.Enabled = False
            UpdateDate.Enabled = False
            UpdateBy.Enabled = False
            lblInitialCost.Enabled = False
            lblHighestCost.Enabled = False
            lblLowestCost.Enabled = False
            lblAvrgCost.Enabled = False
            lblLatestCost.Enabled = False
        End If
    End Sub

    Sub DisplayData()
        Dim objMasterDs As New DataSet()
        dsStockItem = LoadData(False)

        DCCode.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("ItemCode"))
        Desc.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("Description"))
        Status.Text = Trim(objIN.mtdGetStockItemStatus(dsStockItem.Tables(0).Rows(0).Item("Status")))
        CreateDate.Text = Trim(objGlobal.GetLongDate(dsStockItem.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = Trim(objGlobal.GetLongDate(dsStockItem.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("UserName"))
        lblInitialCost.Text = objGlobal.GetIDDecimalSeparator(Round(dsStockItem.Tables(0).Rows(0).Item("InitialCost"), 0))
        lblHighestCost.Text = objGlobal.GetIDDecimalSeparator(Round(dsStockItem.Tables(0).Rows(0).Item("HighCost"), 0))
        lblLowestCost.Text = objGlobal.GetIDDecimalSeparator(Round(dsStockItem.Tables(0).Rows(0).Item("LowCost"), 0))
        lblAvrgCost.Text = objGlobal.GetIDDecimalSeparator(Round(dsStockItem.Tables(0).Rows(0).Item("AverageCost"), 0))
        DiffAverageCost.Text = objGlobal.GetIDDecimalSeparator(Round(dsStockItem.Tables(0).Rows(0).Item("DiffAverageCost"), 0))
        lblLatestCost.Text = objGlobal.GetIDDecimalSeparator(Round(dsStockItem.Tables(0).Rows(0).Item("LatestCost"), 0))
        purchaseACNo.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("purchaseACcNo"))
        IssueACNo.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("IssueACcNo"))

        If dsStockItem.Tables(0).Rows(0).Item("Status") = objIN.EnumStockItemStatus.Deleted Then
            DisableControl()
        End If

        objMasterDs = LoadData(True)

        If objMasterDs.Tables(0).Rows.Count > 0 Then
            If objMasterDs.Tables(0).Rows(0).Item("Status") = objIN.EnumStockItemStatus.Deleted And _
               dsStockItem.Tables(0).Rows(0).Item("Status") = objIN.EnumStockItemStatus.Deleted Then
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
        Dim SearchStr As String
        Dim strParam As String
        Dim drinsert As DataRow

        SearchStr = "AND ItemType = '" & objIN.EnumInventoryItemType.DirectCharge & "' AND itm.Status = '" & objIN.EnumStockItemStatus.Active & "' AND Not ItemCode In (Select ItemCode From IN_Item where ItemType =" & objIN.EnumInventoryItemType.DirectCharge & " And LocCode='" & strLocation & "' ) "

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

        Try
            Select Case ModuleType
                Case objGlobal.EnumModule.Admin
                    intErrNo = objAdmin.mtdGetMasterList(Oppcode, strParam, dsForDropDown)
                Case objGlobal.EnumModule.GeneralLedger
                    intErrNo = objGL.mtdGetMasterList(Oppcode, strParam, MasterType, dsForDropDown)
                Case objGlobal.EnumModule.Inventory
                    intErrNo = objIN.mtdGetMasterList(Oppcode, strParam, MasterType, dsForDropDown)
            End Select
        Catch e As System.Exception
            ErrorMessage.Text = e.Message
            dsForDropDown = Nothing
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
            strParam = "Order by " & MasterKeyField & "|" & "AND " & MasterKeyField & " = '" & _
                        Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField)) & "'"

            Try
                intErrNo = objIN.mtdGetMasterList(Oppcode, strParam, MasterType, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                    BindToList.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0)) & _
                     " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Deleted) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0))))
                    SelectedIndex = BindToList.Items.Count - 1
                Else 
                    BindToList.Items.Add(New ListItem(Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField)) & _
                       " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Unknown) & ") ", Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField))))
                    SelectedIndex = 0
                End If

                If intErrNo <> 0 Then
                    ErrorMessage.Text = "Not Retrieved Successfully."
                End If
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_DirectCharge.aspx")
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
        Dim blnDupKey As Boolean = False
        Dim UseSellPrice As String
        Dim strStatus As String
        Dim strParam As String
        Dim strCompany As String = Session("SS_COMPANY")
        Dim strLocation As String = Session("SS_LOCATION")
        Dim strUserId As String = Session("SS_USERID")

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
        Trim(lstItemCode.SelectedItem.Value) & "|" & _
        Trim(Desc.Text) & "||" & _
        Trim(objIN.EnumInventoryItemType.DirectCharge) & "||" & _
        objIN.EnumFuelItemIndicator.No & "|||||||" & _
        Trim(lstAccCode.SelectedItem.Value) & "|" & _
        Trim(lstUOM.SelectedItem.Value) & "|" & _
        Trim(lstUOM.SelectedItem.Value) & "|0|0|0|0|" & _
        Trim(purchaseACNo.Text) & "|" & _
        Trim(IssueACNo.Text) & "||0|0|0||" & _
        Trim(strStatus) & "|||"

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPDATE_DIRECTCHARGEITEM_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_DirectCharge.aspx")
        End Try

        If blnDupKey Then
            lblDupMsg.Visible = True
        Else
            Select Case blnUpdate.Text
                Case True
                    Response.Redirect("IN_DirectCharge.aspx")
                Case False
                    Response.Redirect("IN_DirectCharge_Detail.aspx")
            End Select

        End If

    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Save")
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Delete")
        Response.Redirect("IN_DirectCharge.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("IN_DirectCharge.aspx")
    End Sub

  
End Class
