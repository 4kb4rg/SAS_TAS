Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.HR
Imports agri.PWSystem.clsLangCap

Public Class HR_Setup_Function : Inherits Page

    Protected WithEvents EventData as DataGrid
    Protected WithEvents ErrorMessage as Label
    Protected WithEvents lblTracker as Label
    Protected WithEvents SQLStatement as Label
    Protected WithEvents lstDropList as DropDownList
    Protected WithEvents StatusList as DropDownList
    Protected WithEvents srchStatusList as DropDownList
    Protected WithEvents SortExpression as Label
    Protected WithEvents blnUpdate as Label
    Protected WithEvents curStatus as Label
    Protected WithEvents lblErrMessage as Label
    Protected WithEvents lblDupMsg as Label
    Protected WithEvents sortcol as Label
    Protected WithEvents srchFuncCode as TextBox
    Protected WithEvents srchDesc as TextBox
    Protected WithEvents srchUpdateBy as TextBox
    Protected WithEvents lblTitle as Label
    Protected WithEvents lblFunction as Label
    Protected WithEvents lblDesc as Label
    Protected WithEvents lblValidate as Label
    Protected WithEvents lblCode as Label

    Protected WithEvents ddlEmpType As DropDownList
    Protected WithEvents lblEmpType as Label

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "HR_CLSSETUP_FUNC_LIST_GET"
    Dim strOppCd_ADD As String = "HR_CLSSETUP_FUNC_LIST_ADD"
    Dim strOppCd_UPD As String = "HR_CLSSETUP_FUNC_LIST_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRFunction), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.text = "" Then
                SortExpression.text = "FuncCode"
                sortcol.text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindSearchList() 
                BindGrid() 
                BindPageList()
            End If

        End IF
    End Sub

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
        StatusList.Items.Add(New ListItem(objHR.mtdGetFunctionStatus(objHR.EnumFunctionStatus.Active), objHR.EnumFunctionStatus.Active))
        StatusList.Items.Add(New ListItem(objHR.mtdGetFunctionStatus(objHR.EnumFunctionStatus.Deleted), objHR.EnumFunctionStatus.Deleted))

    End Sub 

    Sub BindSearchList() 

        srchStatusList.Items.Add(New ListItem(objHR.mtdGetFunctionStatus(objHR.EnumFunctionStatus.All), objHR.EnumFunctionStatus.All))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetFunctionStatus(objHR.EnumFunctionStatus.Active), objHR.EnumFunctionStatus.Active))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetFunctionStatus(objHR.EnumFunctionStatus.Deleted), objHR.EnumFunctionStatus.Deleted))
        srchStatusList.SelectedIndex = 1

    End Sub 

    Protected Function LoadData() As DataSet
        
        Dim srchStatus as string
        Dim strParam as string
        Dim SearchStr as string
        Dim sortitem as string


        SearchStr =  " AND FUNC.Status like '" & IIF(Not srchStatusList.selectedItem.Value = objHR.EnumFunctionStatus.All, _
                       srchStatusList.selectedItem.Value, "%" ) &"' "
        If NOT srchFuncCode.text = "" Then
            SearchStr =  SearchStr & " AND FUNC.FuncCode like '" & srchFuncCode.text &"%' "
        End If
        If NOT srchDesc.text = "" Then
            SearchStr = SearchStr & " AND FUNC.Description like '" & _
                        srchDesc.text &"%' "
        End If
        If NOT srchUpdateBy.text = "" Then
            SearchStr = SearchStr & " AND USR.UserName like '" & _
                        srchUpdateBy.text &"%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text 
        strParam =  sortitem & "|" & SearchStr

        Try
        intErrNo = objHR.mtdGetMasterList(strOppCd_GET, strParam, objHR.EnumHRMasterType.Func, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_FUNC_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        Return objDataSet
    End Function

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
        Dim lblTemp As Label
        Dim ddlTemp As DropDownList
        Dim liTemp As ListItem
        Dim List As Dropdownlist
        Dim Updbutton As linkbutton
        Dim Validator As RequiredFieldValidator

        blnUpdate.text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid() 
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)
        onLoad_BindEmpType(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case cint(Edittext.text) = objHR.EnumFunctionStatus.Active
            Case True
                Statuslist.selectedindex = 0
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("FuncCode")
                EditText.readonly = true
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
         		'Updbutton.Attributes.Add("onclick","return confirm('Are you sure you want to delete this code?');")
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                lblTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblEmpType")
                ddlTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlEmpType")
                liTemp = ddlTemp.Items.FindByValue(CInt(lblTemp.Text))
                If Not (liTemp Is Nothing) Then
                    ddlTemp.SelectedIndex = ddlTemp.Items.IndexOf(liTemp)
                End If
            Case False
                Statuslist.selectedindex = 1
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("FuncCode")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Description")
                EditText.Enabled = False
                lblTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblEmpType")
                ddlTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlEmpType")
                liTemp = ddlTemp.Items.FindByValue(CInt(lblTemp.Text))
                If Not (liTemp Is Nothing) Then
                    ddlTemp.SelectedIndex = ddlTemp.Items.IndexOf(liTemp)
                End If
                ddlTemp.Enabled = false 
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
        Validator = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateCode")
        Validator.ErrorMessage = strValidateCode
        Validator = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateDesc")
        Validator.ErrorMessage = strValidateDesc   

    End Sub

    Sub DEDR_Update(Sender As Object, E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As Dropdownlist
        Dim FuncCode As String
        Dim Description As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg as label
        Dim CreateDate As String
        Dim strEmpType as String
 
        EditText = E.Item.FindControl("FuncCode")
        FuncCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.Selecteditem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        list = E.Item.FindControl("ddlEmpType")        
        strEmpType = list.Selecteditem.Value 
        lblMsg = E.Item.FindControl("lblErrEmpType")
        If Trim(strEmpType) = "" Then
           lblMsg.Visible = True
           Exit Sub 
        End If

        strParam =  FuncCode & "|" & _
                    Description & "|" & _
                    Status & "|" & _
                    CreateDate & "|" & _
                    strEmpType 

        Try
        intErrNo = objHR.mtdUpdMasterList(strOppCd_ADD, _
                                          strOppCd_UPD, _
                                          strOppCd_GET, _
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          objHR.EnumHRMasterType.Func, _
                                          blnDupKey, _
                                          blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_UPDATE_FUNC&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Function.aspx")
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
        Dim FuncCode As String
        Dim Description As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim CreateDate As String
        Dim strEmpType as String 
        Dim list as dropdownlist

        EditText = E.Item.FindControl("FuncCode")
        FuncCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        EditText = E.Item.FindControl("Status")
        Status = IIF(EditText.Text = objHR.EnumFunctionStatus.Active, _
                        objHR.EnumFunctionStatus.Deleted, _
                        objHR.EnumFunctionStatus.Active )
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        list = E.Item.FindControl("ddlEmpType")        
        strEmpType = list.Selecteditem.Value     

        strParam =  FuncCode & "|" & _
                    Description & "|" & _
                    Status & "|" & _
                    CreateDate & "|" & _
                    strEmpType
        Try
        intErrNo = objHR.mtdUpdMasterList(strOppCd_ADD, _
                                          strOppCd_UPD, _
                                          strOppCd_GET, _
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          objHR.EnumHRMasterType.Func, _
                                          blnDupKey, _
                                          blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DELETE_FUNC&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Function.aspx")
        End Try
      
        EventData.EditItemIndex = -1
        BindGrid() 

    End Sub

    Sub DEDR_Add(Sender As Object, E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim Validator As RequiredFieldValidator
        Dim PageCount As Integer
        
        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("FuncCode") = ""
        newRow.Item("Description") = ""
        newRow.Item("EmpType") = "1"
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
        onLoad_BindEmpType(EventData.EditItemIndex)
        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.visible = False
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        Validator.ErrorMessage = strValidateCode
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")
        Validator.ErrorMessage = strValidateDesc
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.Func))
        lblFunction.text = GetCaption(objLangCap.EnumLangCap.Func)
        lblDesc.text = GetCaption(objLangCap.EnumLangCap.FuncDesc)

        strValidateCode = lblValidate.text & lblFunction.text & lblCode.text & "."
        strValidateDesc = lblValidate.text & lblDesc.text & "." 

        EventData.Columns(0).HeaderText = lblFunction.text & lblCode.text
        EventData.Columns(1).HeaderText = lblDesc.text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Function.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    Sub onLoad_BindEmpType(ByVal index As Integer) 
        ddlEmpType = EventData.Items.Item(index).FindControl("ddlEmpType")
        ddlEmpType.Items.Add(New ListItem("Select Employee Type", ""))
        ddlEmpType.Items.Add(New ListItem(objHR.mtdGetEmpType(objHR.EnumEmpType.Langsung), objHR.EnumEmpType.Langsung))
        ddlEmpType.Items.Add(New ListItem(objHR.mtdGetEmpType(objHR.EnumEmpType.TdkLangsung), objHR.EnumEmpType.TdkLangsung))
    End Sub



End Class
