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


Public Class WS_DirectMasterDet : Inherits Page

    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents DCCode As TextBox
    Protected WithEvents Desc As TextBox
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
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblChargeTo As Label
    Protected WithEvents lblDCItemCode As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator

    Protected WithEvents Undelete As ImageButton 

    Dim objIN As New agri.IN.clsSetup()
    Dim objGL As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsUOM()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objDataSet As New DataSet()
    Dim dsStockItem As DataSet
    Dim objLangCapDs As New DataSet()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim ItemCode As String
    Dim strParam As String

    Dim intErrNo As Integer
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim strLocType as String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSDirectChargeItemMaster), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If Not IsPostBack Then
                ItemCode = Request.QueryString("ID")

                If Not ItemCode = "" Then
                    DisplayData()
                    DisplayDropDown()

                    If dsStockItem.Tables(0).Rows(0).Item("Status") = objIN.EnumStockItemStatus.Deleted Then
                        DisableControl()
                        Undelete.Visible = True
                        Delete.Visible = False
                        Undelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');" 'csloo
                        Save.Visible = False
                    Else
                        Undelete.Visible = False
                        Delete.Visible = True
                        Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    End If

                    blnUpdate.Text = True
                    DCCode.ReadOnly = True
                Else
                    DisplayDropDown()
                    blnUpdate.Text = False
                    Delete.Visible = False
                    Undelete.Visible = False
                End If
            End If
        End If
    End Sub



    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblDCItemCode.Text = GetCaption(objLangCap.EnumLangCap.DirectChgItem) & lblCode.Text
        lblDesc.Text = GetCaption(objLangCap.EnumLangCap.DirectChgItemDesc)
        lblAccCodeTag.Text = lblChargeTo.Text & GetCaption(objLangCap.EnumLangCap.Account)
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.DirectChgMaster))

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_DIRECTMASTER_DETAIL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=ws/setup/ws_directmaster_detail.aspx")
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



    Protected Function LoadData() As DataSet
        Dim strOppCd_GET As String = "IN_CLSSETUP_INVMASTER_DETAILS_GET"

        strParam = ItemCode & "|" & objIN.EnumInventoryItemType.DirectCharge & "|" & Trim(strLocation) 
        Try
            intErrNo = objIN.mtdGetMasterDetail(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_DIRECTCHARGEITEM_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_DirectMaster.aspx")
        End Try
        Return objDataSet
    End Function

    Sub DisableControl()
        DCCode.Enabled = False
        lstUOM.Enabled = False
        lstAccCode.Enabled = False
        Desc.Enabled = False
        purchaseACNo.Enabled = False
        IssueACNo.Enabled = False
    End Sub

    Sub DisplayData()
        dsStockItem = LoadData()

        DCCode.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("ItemCode"))
        Desc.Text = Trim(dsStockItem.Tables(0).Rows(0).Item("Description"))
        Status.Text = Trim(objIN.mtdGetStockItemStatus(dsStockItem.Tables(0).Rows(0).Item("Status")))
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
    End Sub

    Sub DisplayDropDown()
        Dim strOppCd_AccountCode_GET As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strOppCd_UOM_GET As String = "ADMIN_CLSUOM_UOM_LIST_GET"

        BindDropList(strOppCd_AccountCode_GET, "ActCode", "AccCode", objGL.EnumGLMasterType.AccountCode, _
                        objGlobal.EnumModule.GeneralLedger, objGL.EnumAccountCodeStatus.Active, lstAccCode)
        BindDropList(strOppCd_UOM_GET, "UOMCode", "UOMCode", "0", _
                         objGlobal.EnumModule.Admin, objAdmin.EnumUOMStatus.Active, lstUOM)
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
                intErrNo = objIN.mtdGetMasterList(Oppcode, strParam, MasterType, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                    BindToList.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0)) & _
                     " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Deleted) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0))))
                    SelectedIndex = BindToList.Items.Count - 1
                Else 
                    SelectedIndex = 0
                End If

                If intErrNo <> 0 Then
                    ErrorMessage.Text = "Not Retrieved Successfully."
                End If
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_DirectMaster.aspx")
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
        Dim strOppCd_ItemList_GET As String = "IN_CLSSETUP_INVMASTER_LIST_GET"
        Dim strOppCd_ADD As String = "IN_CLSSETUP_INVMASTER_DETAILS_ADD"
        Dim strOppCd_UPD As String = "IN_CLSSETUP_INVMASTER_DETAILS_UPD"

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
        Trim(DCCode.Text) & "|" & _
        Trim(Desc.Text) & "||" & _
        Trim(objIN.EnumInventoryItemType.DirectCharge) & "|||||||||" & _
        Trim(lstAccCode.SelectedItem.Value) & "|" & _
        Trim(lstUOM.SelectedItem.Value) & "||0|0|0|0|" & _
        Trim(purchaseACNo.Text) & "|" & _
        Trim(IssueACNo.Text) & "||0||||" & _
        Trim(strStatus) & "||||"

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPDATE_DIRECTCHARGEITEM_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_DirectMaster.aspx")
        End Try

        If blnDupKey Then
            lblDupMsg.Visible = True
        Else
            Select Case blnUpdate.Text
                Case True
                    Response.Redirect("WS_DirectMaster.aspx")
                Case False
                    Response.Redirect("WS_DirectMaster_Detail.aspx")
            End Select

        End If

    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Save")
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Delete")
        Response.Redirect("WS_DirectMaster.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WS_DirectMaster.aspx")
    End Sub



End Class
