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

Public Class HR_Setup_Position : Inherits Page

    Protected WithEvents EventData as DataGrid
    Protected WithEvents ErrorMessage as Label
    Protected WithEvents lblTracker as Label
    Protected WithEvents SQLStatement as Label
    Protected WithEvents lblFuncCode As Label
    Protected WithEvents lstDropList as DropDownList
    Protected WithEvents StatusList as DropDownList
    Protected WithEvents srchStatusList as DropDownList
    Protected WithEvents SortExpression as Label
    Protected WithEvents blnUpdate as Label
    Protected WithEvents curStatus as Label
    Protected WithEvents lblErrMessage as Label
    Protected WithEvents lblDupMsg as Label
    Protected WithEvents sortcol as Label
    Protected WithEvents srchPositionCode as TextBox
    Protected WithEvents srchDesc as TextBox
    Protected WithEvents srchUpdateBy as TextBox
    Protected WithEvents lblCode As Label
    Protected WithEvents lblFunction As Label
    Protected WithEvents SearchBtn As Button

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "HR_CLSSETUP_POSITION_LIST_GET"
    Dim strOppCd_ADD As String = "HR_CLSSETUP_POSITION_LIST_ADD"
    Dim strOppCd_UPD As String = "HR_CLSSETUP_POSITION_LIST_UPD"
    Dim objDataSet As New Object()  
    Dim objLangCapDs As New Object()
    Dim objFuncDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap AS New agri.PWSystem.clsLangCap()
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
                SortExpression.text = "PositionCode"
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
        StatusList.Items.Add(New ListItem(objHR.mtdGetPositionStatus(objHR.EnumPositionStatus.Active), objHR.EnumPositionStatus.Active))
        StatusList.Items.Add(New ListItem(objHR.mtdGetPositionStatus(objHR.EnumPositionStatus.Deleted), objHR.EnumPositionStatus.Deleted))

    End Sub 

    Sub BindSearchList() 

        srchStatusList.Items.Add(New ListItem(objHR.mtdGetPositionStatus(objHR.EnumPositionStatus.All), objHR.EnumPositionStatus.All))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetPositionStatus(objHR.EnumPositionStatus.Active), objHR.EnumPositionStatus.Active))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetPositionStatus(objHR.EnumPositionStatus.Deleted), objHR.EnumPositionStatus.Deleted))
        srchStatusList.SelectedIndex = 1

    End Sub 

    Protected Function LoadData() As DataSet
        
        Dim srchStatus as string
        Dim SearchStr as string
        Dim strParam as string
        Dim sortitem as string
        Dim intCnt As Integer


        SearchStr =  " AND POS.Status like '" & IIf(Not srchStatusList.selectedItem.Value = objHR.EnumPositionStatus.All, srchStatusList.selectedItem.Value, "%" ) & "' "

        If Not srchPositionCode.text = "" Then
            SearchStr =  SearchStr & " AND POS.PositionCode like '" & srchPositionCode.text & "%' "
        End If

        If Not srchDesc.text = "" Then
            SearchStr = SearchStr & " AND POS.Description like '" & _
                        srchDesc.text & "%' "
        End If

        If Not srchUpdateBy.text = "" Then
            SearchStr = SearchStr & " AND USR.UserName like '" & _
                        srchUpdateBy.text & "%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text 
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objHR.mtdGetMasterList(strOppCd_GET, strParam, objHR.EnumHRMasterType.Position, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_POSITION_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
            objDataSet.Tables(0).Rows(0).Item("FuncCode") = Trim(objDataSet.Tables(0).Rows(0).Item("FuncCode"))
        Next

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

    Protected Function FuncDataSet(ByVal pv_strFuncCode As String, ByRef pr_intIndex As Integer) As DataSet
        Dim strOpCd As String = "HR_CLSSETUP_FUNC_LIST_GET"
        Dim srchStatus as string
        Dim strParam as string
        Dim SearchStr as string
        Dim sortitem as string
        Dim intCnt As Integer

        pr_intIndex = 0
        SearchStr = " AND FUNC.Status = '" & objHR.EnumFunctionStatus.Active & "'"
        sortitem = "ORDER BY FUNC.FuncCode ASC " 
        strParam =  sortitem & "|" & SearchStr

        Try
        intErrNo = objHR.mtdGetMasterList(strOpCd, strParam, objHR.EnumHRMasterType.Func, objFuncDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_POSITION_FUNCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objFuncDs.Tables(0).Rows.Count - 1
            objFuncDs.Tables(0).Rows(intCnt).Item("FuncCode") = Trim(objFuncDs.Tables(0).Rows(intCnt).Item("FuncCode"))
            objFuncDs.Tables(0).Rows(intCnt).Item("Description") = objFuncDs.Tables(0).Rows(intCnt).Item("FuncCode") & " (" & Trim(objFuncDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objFuncDs.Tables(0).Rows(intCnt).Item("FuncCode") = Trim(pv_strFuncCode) Then
                pr_intIndex = intCnt
            End If
        Next
        
        Return objFuncDs
    End Function
 
    Sub DEDR_Edit(Sender As Object, E As DataGridCommandEventArgs)
        Dim LabelText As Label
        Dim EditText As TextBox
        Dim EditList As DropDownList
        Dim ddlFuncCode As DropDownList
        Dim List As Dropdownlist
        Dim Updbutton As linkbutton
        Dim intSelectedFunc As Integer

        blnUpdate.text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid()
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        LabelText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblFuncCode")
        EditList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("FuncCode")
        EditList.DataSource = FuncDataSet(LabelText.Text, intSelectedFunc)
        EditList.DataValueField = "FuncCode"
        EditList.DataTextField = "Description"
        EditList.DataBind()
        EditList.SelectedIndex = intSelectedFunc        

        EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case cint(Edittext.text) = objHR.EnumPositionStatus.Active
            Case True
                Statuslist.selectedindex = 0
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("PositionCode")
                EditText.readonly = true
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
         		'Updbutton.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this code?');")
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                Statuslist.selectedindex = 1
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("PositionCode")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Description")
                EditText.Enabled = False
                EditList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("FuncCode")
                EditList.Enabled = False
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
    End Sub

    Sub DEDR_Update(Sender As Object, E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As Dropdownlist
        Dim PositionCode As String
        Dim Description As String
        Dim FuncCode As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg as label
        Dim CreateDate As String
 
        EditText = E.Item.FindControl("PositionCode")
        PositionCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        list = E.Item.FindControl("FuncCode")
        FuncCode = list.Selecteditem.Value
        list = E.Item.FindControl("StatusList")
        Status = list.Selecteditem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  PositionCode & "|" & _
                    Description & "|" & _
                    FuncCode & "|" & _
                    Status & "|" & _
                    CreateDate 

        Try
        intErrNo = objHR.mtdUpdMasterList(strOppCd_ADD, _
                                          strOppCd_UPD, _
                                          strOppCd_GET, _
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          objHR.EnumHRMasterType.Position, _
                                          blnDupKey, _
                                          blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_UPDATE_POSITION&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Position.aspx")
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

        Dim list As Dropdownlist
        Dim EditText As TextBox
        Dim PositionCode As String
        Dim Description As String
        Dim strFuncCode As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim CreateDate As String


        EditText = E.Item.FindControl("PositionCode")
        PositionCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        list = E.Item.FindControl("FuncCode")
        strFuncCode = list.Selecteditem.Value
        EditText = E.Item.FindControl("Status")
        Status = IIF(EditText.Text = objHR.EnumPositionStatus.Active, _
                    objHR.EnumPositionStatus.Deleted, _
                    objHR.EnumPositionStatus.Active )
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  PositionCode & "|" & _
                    Description & "|" & _
                    strFuncCode & "|" & _
                    Status & "|" & _
                    CreateDate 

        Try
            intErrNo = objHR.mtdUpdMasterList(strOppCd_ADD, _
                                              strOppCd_UPD, _
                                              strOppCd_GET, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objHR.EnumHRMasterType.Position, _
                                              blnDupKey, _
                                              blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DELETE_POSITION&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Position.aspx")
        End Try
      
        EventData.EditItemIndex = -1
        BindGrid() 

    End Sub

    Sub DEDR_Add(Sender As Object, E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim Updbutton as LinkButton
        Dim ddlFuncCode As DropDownList
        Dim strFuncCode As String = ""
        Dim intSelectedFunc As Integer
        Dim PageCount As Integer

        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("PositionCode") = ""
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

        ddlFuncCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("FuncCode")
        ddlFuncCode.DataSource = FuncDataSet(strFuncCode, intSelectedFunc)
        ddlFuncCode.DataValueField = "FuncCode"
        ddlFuncCode.DataTextField = "Description"
        ddlFuncCode.DataBind()
        ddlFuncCode.SelectedIndex = intSelectedFunc        

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.visible = False
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()

        EventData.Columns(2).HeaderText = GetCaption(objLangCap.EnumLangCap.Func) & lblCode.text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Position.aspx")
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


End Class
