Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.IN
Imports agri.PWSystem.clsLangCap


Public Class IN_StockMaster : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents srchStockCode As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblStockItemCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label
	Protected WithEvents cbExcel As CheckBox

    Protected WithEvents ibNew As ImageButton
	Protected WithEvents lstProdCat As DropDownList

    Protected objIN As New agri.IN.clsSetup()
	Protected objGL As New agri.GL.clsSetup()
	Protected objAdmin As New agri.Admin.clsUOM()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim strExportToExcel As String

    Dim strOppCd_GET As String = "IN_CLSSETUP_INVMASTER_LIST_GET"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strStockItemCodeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String
    
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strLocLevel As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLocType = Session("SS_LOCTYPE")
        strLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItemMaster), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "right(rtrim(ItemCode),5)"
            End If
            If Not Page.IsPostBack Then
				DisplayDropDown()
                BindSearchList()
                BindGrid()
                BindPageList()				
            End If

            'If strCompany <> "KAS" Then
            '    ibNew.Visible = False
            'Else
            '    If strLocLevel = "3" And strLocType = "4" Then
            '        ibNew.Visible = True
            '    Else
            '        ibNew.Visible = False
            '    End If
            'End If
            
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.StockMaster))
        lblStockItemCode.text = GetCaption(objLangCap.EnumLangCap.StockItem) & lblCode.text
        lblDescription.text = GetCaption(objLangCap.EnumLangCap.StockItemDesc)

        EventData.Columns(0).HeaderText = lblStockItemCode.text
        EventData.Columns(1).HeaderText = lblDescription.text

        strStockItemCodeTag = lblStockItemCode.text
        strDescTag = lblDescription.text
        strTitleTag = lblTitle.text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=IN/setup/IN_setup_stockmaster.aspx")
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

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        EventData.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()


        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), EventData.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsData = LoadData
            End If
        End If
        
        EventData.DataSource = dsData
        EventData.DataBind()
        lblPageCount.Text = PageCount
        BindPageList()
        PageNo = lblCurrentIndex.Text + 1
        lblTracker.Text="Page " & pageno & " of " & PageCount
    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objIN.mtdGetStockItemStatus(objIN.EnumStockItemStatus.Active), objIN.EnumStockItemStatus.Active))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetStockItemStatus(objIN.EnumStockItemStatus.Deleted), objIN.EnumStockItemStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetStockItemStatus(objIN.EnumStockItemStatus.All), objIN.EnumStockItemStatus.All))

    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub

    Protected Function LoadData() As DataSet

        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String

        Session("SS_PAGING") = lblCurrentIndex.Text

        SearchStr = "AND ItemType in ('" & objIN.EnumInventoryItemType.Stock & "','" & objIN.EnumInventoryItemType.WorkshopItem & "','7') AND itm.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objIN.EnumStockItemStatus.All, _
                       srchStatusList.SelectedItem.Value, "%") & "' "

        If Not srchStockCode.Text = "" Then
            SearchStr = SearchStr & " AND itm.ItemCode like '" & srchStockCode.Text & "%' "
        End If

        If Not srchDesc.Text = "" Then
            SearchStr = SearchStr & " AND itm.Description like '%" & _
                        srchDesc.Text & "%' "
        End If

        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & _
                        srchUpdateBy.Text & "%' "
        End If
		
		If Not lstProdCat.selectedItem.Value = "" Then
            SearchStr = SearchStr & " AND itm.ProdCatCode = '" & _
                        lstProdCat.selectedItem.Value & "' "		
        End If

        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text

        strParam = sortitem & "|" & _
                    SearchStr

        Try
            intErrNo = objIN.mtdGetMasterList(strOppCd_GET, strParam, objIN.EnumInventoryMasterType.StockItem, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_setup_stockmaster.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPreview_Click (sender As Object, e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strStockCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIF(Not srchStatusList.selectedItem.Value = objIN.EnumStockItemStatus.All, srchStatusList.selectedItem.Value, "")
        strStockCode = srchStockCode.text
        strDescription = srchDesc.text
        strUpdateBy =  srchUpdateBy.text
        strSortExp = sortexpression.text
        strSortCol = sortcol.text
		strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockMaster.aspx?strStockItemCodeTag=" & strStockItemCodeTag & _
                    "&strDescTag=" & strDescTag & "&strTitleTag=" & strTitleTag & _
                    "&strStatus=" & strStatus & _
                    "&strStockCode=" & strStockCode & "&strDescription=" & strDescription & _
                    "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
					"&ExportToExcel=" & strExportToExcel & _ 
                    "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                lblCurrentIndex.Text = 0
            Case "prev"
                lblCurrentIndex.Text = _
                    Math.Max(0, lblCurrentIndex.Text - 1)
            Case "next"
                lblCurrentIndex.Text = _
                    Math.Min(lblPageCount.Text - 1, lblCurrentIndex.Text + 1)
            Case "last"
                lblCurrentIndex.Text = lblPageCount.Text - 1
        End Select
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_StockMaster_Detail.aspx")
    End Sub

	Sub DisplayDropDown()
        Dim strOppCd_ProdCat_GET As String = "IN_CLSSETUP_PRODCAT_LIST_GET"
                
        BindDropList(strOppCd_ProdCat_GET, "ProdCatCode", "ProdCatCode", objIN.EnumInventoryMasterType.ProductCategory, _
                        objGlobal.EnumModule.Inventory, objIN.EnumProductCategoryStatus.Active, lstProdCat)
        
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_StockMaster.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"            
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Please Select a Code "
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        BindToList.DataSource = dsForDropDown.Tables(0)
        BindToList.DataValueField = MasterKeyField
        BindToList.DataTextField = DataTextField
        BindToList.DataBind()


        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
        
    End Sub

    Sub dgLine_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

End Class
