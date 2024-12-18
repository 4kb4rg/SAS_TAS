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


Public Class IN_ProdModel : Inherits Page

    Protected WithEvents EventData as DataGrid
    Protected WithEvents ErrorMessage as Label
    Protected WithEvents lblTracker as Label
    Protected WithEvents lblErrMessage as Label
    Protected WithEvents SQLStatement as Label
    Protected WithEvents lstDropList as DropDownList
    Protected WithEvents StatusList as DropDownList
    Protected WithEvents srchStatusList as DropDownList
    Protected WithEvents SortExpression as Label
    Protected WithEvents blnUpdate as Label
    Protected WithEvents curStatus as Label
    Protected WithEvents sortcol as Label
    Protected WithEvents srchProdModelCode as TextBox
    Protected WithEvents srchDesc as TextBox
    Protected WithEvents srchUpdateBy as TextBox
    Protected WithEvents lblTitle as Label
    Protected WithEvents lblProdModelCode as Label
    Protected WithEvents lblDescription as Label
    Protected WithEvents lblCode as Label
    Protected WithEvents lblPleaseEnter as Label
    Protected WithEvents lblList as Label

    Protected objIN As New agri.IN.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "IN_CLSSETUP_PRODMODEL_LIST_GET"
    Dim strOppCd_ADD As String = "IN_CLSSETUP_PRODMODEL_LIST_ADD"
    Dim strOppCd_UPD As String = "IN_CLSSETUP_PRODMODEL_LIST_UPD"

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim DocTitleTag As String
    Dim ProdModelCodeTag As String
    Dim ProdModelDescTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String
    Sub Page_Load(Sender As Object, E As EventArgs)
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INProductModel), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.text = "" Then
                SortExpression.text = "ProdModelCode"
                sortcol.text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindSearchList() 
                BindGrid() 
                BindPageList()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.ProdModel))
        lblProdModelCode.text = GetCaption(objLangCap.EnumLangCap.ProdModel) & lblCode.text
        lblDescription.text = GetCaption(objLangCap.EnumLangCap.ProdModelDesc)

        strValidateCode = lblPleaseEnter.text & lblProdModelCode.text & "."
        strValidateDesc = lblPleaseEnter.text & lblDescription.text & "."

        EventData.Columns(0).HeaderText = lblProdModelCode.text
        EventData.Columns(1).HeaderText = lblDescription.text

        DocTitleTag = lblTitle.text & lblList.text
        ProdModelCodeTag = lblProdModelCode.text
        ProdModelDescTag = lblDescription.text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_SETUP_PRODMODEL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_prodmodel.aspx")
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



    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid() 
        BindPageList()
    End Sub 

    Sub BindGrid() 
        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, EventData.PageSize)
        
        EventData.DataSource = dsData
        If EventData.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EventData.CurrentPageIndex = 0
            Else
                EventData.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        EventData.DataBind()
        BindPageList()
        PageNo = EventData.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & EventData.PageCount
    End Sub 

    Sub BindPageList() 

        Dim count as integer = 1   
        Dim arrDList As New ArrayList()

        While not count = EventData.PageCount + 1
            arrDList.Add("Page " & count)
            Count = Count +1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex

    End Sub 

    Sub BindStatusList(index as integer) 

        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objIN.mtdGetProductModelStatus(objIN.EnumProductModelStatus.Active), objIN.EnumProductModelStatus.Active))
        StatusList.Items.Add(New ListItem(objIN.mtdGetProductModelStatus(objIN.EnumProductModelStatus.Deleted), objIN.EnumProductModelStatus.Deleted))

    End Sub 

    Sub BindSearchList() 

        srchStatusList.Items.Add(New ListItem(objIN.mtdGetProductModelStatus(objIN.EnumProductModelStatus.Active), objIN.EnumProductModelStatus.Active))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetProductModelStatus(objIN.EnumProductModelStatus.Deleted), objIN.EnumProductModelStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetProductModelStatus(objIN.EnumProductModelStatus.All), objIN.EnumProductModelStatus.All))

    End Sub 

    Protected Function LoadData() As DataSet
        
        Dim strParam as string
        Dim SearchStr as string
        Dim sortitem as string
        Dim intCnt as integer

       
        SearchStr =  " AND PModel.Status like '" & IIF(Not srchStatusList.selectedItem.Value = objIN.EnumProductTypeStatus.All, _
                       srchStatusList.selectedItem.Value, "%" ) &"' "

        If NOT srchProdModelCode.text = "" Then
            SearchStr = SearchStr & " AND PModel.ProdModelCode like '" & srchProdModelCode.text &"%' "
        End If

        If NOT srchDesc.text = "" Then
            SearchStr = SearchStr & " AND PModel.Description like '" & _
                        srchDesc.text &"%' "
        End If

        If NOT srchUpdateBy.text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & _
                        srchUpdateBy.text &"%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text 
        strParam =  sortitem & "|" & SearchStr

        Try
        intErrNo = objIN.mtdGetMasterList(strOppCd_GET, strParam, objIN.EnumInventoryMasterType.ProductModel, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_PRODMODEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_prodmodel.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            For intCnt=0 To objDataSet.Tables(0).Rows.Count - 1
                objDataSet.Tables(0).Rows(intCnt).Item("ProdModelCode") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("ProdModelCode"))
                objDataSet.Tables(0).Rows(intCnt).Item("Description") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Description"))
                objDataSet.Tables(0).Rows(intCnt).Item("Status") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Status"))
                objDataSet.Tables(0).Rows(intCnt).Item("CreateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("CreateDate"))
                objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate"))
                objDataSet.Tables(0).Rows(intCnt).Item("UserName") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

       Return objDataSet
    End Function

    Sub btnPreview_Click (sender As Object, e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strProdModelCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIF(Not srchStatusList.selectedItem.Value = objIN.EnumProductModelStatus.All, srchStatusList.selectedItem.Value, "")
        strProdModelCode = srchProdModelCode.text
        strDescription = srchDesc.text
        strUpdateBy =  srchUpdateBy.text
        strSortExp = sortexpression.text
        strSortCol = sortcol.text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_ProdModel.aspx?strStatus=" & strStatus & _
                       "&strProdModelCode=" & strProdModelCode & _
                       "&strDescription=" & strDescription & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&DocTitleTag=" & DocTitleTag & _
                       "&ProdModelCodeTag=" & ProdModelCodeTag & _
                       "&ProdModelDescTag=" & ProdModelDescTag & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                EventData.CurrentPageIndex = 0
            Case "prev"
                EventData.CurrentPageIndex = _
                    Math.Max(0, EventData.CurrentPageIndex - 1)
            Case "next"
                EventData.CurrentPageIndex = _
                    Math.Min(EventData.PageCount - 1, EventData.CurrentPageIndex + 1)
            Case "last"
                EventData.CurrentPageIndex = EventData.PageCount - 1
        End Select
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            EventData.CurrentPageIndex = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        EventData.CurrentPageIndex=e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.text = e.SortExpression.ToString()
        sortcol.text = IIF(sortcol.text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub
 
    Sub DEDR_Edit(Sender As Object, E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As Dropdownlist
        Dim Updbutton As linkbutton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator

        blnUpdate.text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid() 
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case cint(Edittext.text) = objIN.EnumProductModelStatus.Active
            Case True
                Statuslist.selectedindex = 0
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ProdModelCode")
                EditText.readonly = true
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                Statuslist.selectedindex = 1
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ProdModelCode")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Description")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UpdateDate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UserName")
                EditText.Enabled = False
                List = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("StatusList")
                List.Enabled = False
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select  
        validateCode = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateCode")
        validateDesc = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc    
    End Sub

    Sub DEDR_Update(Sender As Object, E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As Dropdownlist
        Dim ProdModelCode As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg as label
        Dim Description As String
        Dim Status As String
        Dim CreateDate As String

        EditText = E.Item.FindControl("ProdModelCode")
        ProdModelCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.Selecteditem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  ProdModelCode & "|" & _
                    Description & "|" & _
                    Status & "|" & _
                    CreateDate 
        Try
        intErrNo = objIN.mtdUpdMasterList(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objIN.EnumInventoryMasterType.ProductModel, _
                                                blnDupKey, _
                                                blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_PRODMODEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_prodmodel.aspx")
        End Try

        If blnDupKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid() 
        End If

  End Sub

    Sub DEDR_Cancel(Sender As Object, E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)

        Dim EditText As TextBox
        Dim ProdModelCode As String
        Dim Description As String
        Dim blnDupKey As Boolean = False
        Dim Status As String
        Dim CreateDate As String

        EditText = E.Item.FindControl("ProdModelCode")
        ProdModelCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        EditText = E.Item.FindControl("Status")
        Status = IIF(EditText.Text = objIN.EnumProductModelStatus.Active, _
                        objIN.EnumProductModelStatus.Deleted, _
                        objIN.EnumProductModelStatus.Active )
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  ProdModelCode & "|" & _
                    Description & "|" & _
                    Status & "|" & _
                    CreateDate 
        Try
        intErrNo = objIN.mtdUpdMasterList(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objIN.EnumInventoryMasterType.ProductModel, _
                                                blnDupKey, _
                                                blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_PRODMODEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_prodmodel.aspx")
        End Try
      
        EventData.EditItemIndex = -1
        
        
        BindGrid() 
        BindPageList()
    End Sub

    Sub DEDR_Add(Sender As Object, E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer
        
        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("ProdModelCode") = ""
        newRow.Item("Description") = ""
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.now()
        NewRow.Item("UpdateDate") = DateTime.now()
        NewRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)
        
        EventData.DataSource = dataSet
        PageCount = objGlobal.mtdGetPageCount(dataSet.Tables(0).Rows.Count, EventData.PageSize)
        If EventData.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EventData.CurrentPageIndex = 0
            Else
                EventData.CurrentPageIndex = PageCount - 1
            End If
        End If
        EventData.DataBind()
        BindPageList()

        EventData.CurrentPageIndex = EventData.PageCount - 1
        lblTracker.Text="Page " & (EventData.CurrentPageIndex + 1) & " of " & EventData.PageCount
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count -1
        EventData.DataBind()
        BindStatusList(EventData.EditItemIndex)
        
        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.visible = False

        validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        validateDesc = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

    End Sub

End Class
