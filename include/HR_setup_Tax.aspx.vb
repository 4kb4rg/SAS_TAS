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

Public Class HR_Setup_Tax : Inherits Page

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
    Protected WithEvents lblErrIncomeRage as Label
    Protected WithEvents sortcol as Label
    Protected WithEvents srchTaxCode as TextBox
    Protected WithEvents srchIncomeFrom as TextBox
    Protected WithEvents srchIncomeTo as TextBox
    Protected WithEvents srchTaxAmt as TextBox
    Protected WithEvents srchUpdateBy as TextBox
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "HR_CLSSETUP_TAX_LIST_GET"
    Dim strOppCd_ADD As String = "HR_CLSSETUP_TAX_LIST_ADD"
    Dim strOppCd_UPD As String = "HR_CLSSETUP_TAX_LIST_UPD"
    Dim objDataSet As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""


    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRTax), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.text = "" Then
                SortExpression.text = "TaxCode"
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

    Sub BindPageList() 
        Dim count as integer = 1   
        Dim arrDList As New ArrayList()

        While not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count)
            Count = Count +1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub 

    Sub BindStatusList(index as integer)
        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objHR.mtdGetTaxStatus(objHR.EnumTaxStatus.Active), objHR.EnumTaxStatus.Active))
        StatusList.Items.Add(New ListItem(objHR.mtdGetTaxStatus(objHR.EnumTaxStatus.Deleted), objHR.EnumTaxStatus.Deleted))
    End Sub 

    Sub BindSearchList() 
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetTaxStatus(objHR.EnumTaxStatus.All), objHR.EnumTaxStatus.All))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetTaxStatus(objHR.EnumTaxStatus.Active), objHR.EnumTaxStatus.Active))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetTaxStatus(objHR.EnumTaxStatus.Deleted), objHR.EnumTaxStatus.Deleted))
        srchStatusList.SelectedIndex = 1
    End Sub 

    Protected Function LoadData() As DataSet
        Dim srchStatus as string
        Dim strParam as string
        Dim SearchStr as string
        Dim sortitem as string

        lblCurrentIndex.Text = IIf(lblCurrentIndex.Text < 0, 0, lblCurrentIndex.Text)

        Session("SS_PAGING") = lblCurrentIndex.Text

        SearchStr =  " AND TAX.Status like '" & IIF(Not srchStatusList.selectedItem.Value = objHR.EnumTaxStatus.All, _
                       srchStatusList.selectedItem.Value, "%" ) &"' "

        If Not srchTaxCode.text = "" Then
            SearchStr =  SearchStr & " AND TAX.TaxCode like '" & srchTaxCode.text & "%' "
        End If

        If Not srchIncomeFrom.text = "" Then
            SearchStr =  SearchStr & " AND TAX.IncomeFrom >= " & srchIncomeFrom.text & " "
        End If

        If Not srchIncomeTo.text = "" Then
            SearchStr =  SearchStr & " AND TAX.IncomeTo <= " & srchIncomeTo.text & " "
        End If

        If Not srchTaxAmt.text = "" Then
            SearchStr =  SearchStr & " AND TAX.TaxAmt = " & srchTaxAmt.text & " "
        End If

        If Not srchUpdateBy.text = "" Then
            SearchStr = SearchStr & " AND USR.UserName like '" & srchUpdateBy.text & "%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text 
        strParam =  sortitem & "|" & SearchStr

        Try
        intErrNo = objHR.mtdGetMasterList(strOppCd_GET, strParam, objHR.EnumHRMasterType.Tax, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_TAX_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        Return objDataSet
    End Function

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
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

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text=e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.text = e.SortExpression.ToString()
        sortcol.text = IIF(sortcol.text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid() 
    End Sub
 
    Sub DEDR_Edit(Sender As Object, E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As Dropdownlist
        Dim Updbutton As linkbutton

        blnUpdate.text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid() 
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case cint(Edittext.text) = objHR.EnumTaxStatus.Active
            Case True
                Statuslist.selectedindex = 0
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("TaxCode")
                EditText.readonly = true
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("IncomeFrom")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("IncomeTo")
                EditText.Enabled = False
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
         		'Updbutton.Attributes.Add("onclick","return confirm('Are you sure you want to delete this code?');")
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                Statuslist.selectedindex = 1
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("TaxCode")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("IncomeFrom")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("IncomeTo")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("TaxAmt")
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
    End Sub

    Sub DEDR_Update(Sender As Object, E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As Dropdownlist
        Dim ErrLabel As Label
        Dim TaxCode As String
        Dim IncomeFrom As String
        Dim IncomeTo As String
        Dim TaxAmt As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg as label
        Dim CreateDate As String
 
        EditText = E.Item.FindControl("TaxCode")
        TaxCode = EditText.Text
        EditText = E.Item.FindControl("IncomeFrom")
        IncomeFrom = EditText.Text
        EditText = E.Item.FindControl("IncomeTo")
        IncomeTo = EditText.Text
        EditText = E.Item.FindControl("TaxAmt")
        TaxAmt = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.Selecteditem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  TaxCode & "|" & _
                    IncomeFrom & "|" & _
                    IncomeTo & "|" & _
                    TaxAmt & "|" & _
                    Status & "|" & _
                    CreateDate 

        If (CDbl(IncomeFrom) >= CDbl(IncomeTo)) Then
            ErrLabel = E.Item.FindControl("lblErrIncomeFrom")
            ErrLabel.Text = lblErrIncomeRage.Text
            Exit Sub
        End If

        Try
        intErrNo = objHR.mtdUpdMasterList(strOppCd_ADD, _
                                          strOppCd_UPD, _
                                          strOppCd_GET, _
                                          strCompany, _
                                          strLocation, _
                                          strUserId, _
                                          strParam, _
                                          objHR.EnumHRMasterType.Tax, _
                                          blnDupKey, _
                                          blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_UPDATE_TAX&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Tax.aspx")
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
        Dim TaxCode As String
        Dim IncomeFrom As String
        Dim IncomeTo As String
        Dim TaxAmt As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim CreateDate As String


        EditText = E.Item.FindControl("TaxCode")
        TaxCode = EditText.Text
        EditText = E.Item.FindControl("IncomeFrom")
        IncomeFrom = EditText.Text
        EditText = E.Item.FindControl("IncomeTo")
        IncomeTo = EditText.Text
        EditText = E.Item.FindControl("TaxAmt")
        TaxAmt = EditText.Text
        EditText = E.Item.FindControl("Status")
        Status = IIF(EditText.Text = objHR.EnumTaxStatus.Active, _
                                     objHR.EnumTaxStatus.Deleted, _
                                     objHR.EnumTaxStatus.Active )
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  TaxCode & "|" & _
                    IncomeFrom & "|" & _
                    IncomeTo & "|" & _
                    TaxAmt & "|" & _
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
                                              objHR.EnumHRMasterType.Tax, _
                                              blnDupKey, _
                                              blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DELETE_TAX&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Tax.aspx")
        End Try
      
        EventData.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub DEDR_Add(Sender As Object, E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim Updbutton as LinkButton

        Dim intRecNo As Integer, intPageNo As Integer
        
        intRecNo = Session("SS_PAGINGCOUNT")
        intPageNo = objGlobal.mtdGetPageCount(intRecNo, EventData.PageSize)
        If intRecNo Mod EventData.PageSize = 0 Then
            dataSet.Tables(0).Clear
            intPageNo = intPageNo + 1
        ElseIf lblCurrentIndex.Text <> intPageNo - 1
            lblCurrentIndex.Text = intPageNo - 1
            dataSet = LoadData
        End If
        If intPageNo = 0 Then
            intPageNo = 1
        End If
        lblCurrentIndex.Text = intPageNo - 1
        lblPageCount.Text = intPageNo
        
        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("TaxCode") = ""
        newRow.Item("IncomeFrom") = 0
        newRow.Item("IncomeTo") = 0
        newRow.Item("TaxAmt") = 0
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.now()
        NewRow.Item("UpdateDate") = DateTime.now()
        NewRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)
        
        EventData.DataSource = dataSet

        lblTracker.Text="Page " & lblPageCount.Text & " of " & lblPageCount.Text
        BindPageList()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count -1
        EventData.DataBind()
        BindStatusList(EventData.EditItemIndex)

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.visible = False

    End Sub

End Class
