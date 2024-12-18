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

Public Class HR_Setup_Evaluation : Inherits Page

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
    Protected WithEvents lblErrIncMessage As Label
    Protected WithEvents lblErrRateMessage As Label
    Protected WithEvents sortcol as Label
    Protected WithEvents srchEvalCode as TextBox
    Protected WithEvents srchDesc as TextBox
    Protected WithEvents srchUpdateBy as TextBox

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "HR_CLSSETUP_EVALUATION_LIST_GET"
    Dim strOppCd_ADD As String = "HR_CLSSETUP_EVALUATION_LIST_ADD"
    Dim strOppCd_UPD As String = "HR_CLSSETUP_EVALUATION_LIST_UPD"
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREvaluation), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.text = "" Then
                SortExpression.text = "EvalCode"
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
        StatusList.Items.Add(New ListItem(objHR.mtdGetEvaluationStatus(objHR.EnumEvaluationStatus.Active), objHR.EnumEvaluationStatus.Active))
        StatusList.Items.Add(New ListItem(objHR.mtdGetEvaluationStatus(objHR.EnumEvaluationStatus.Deleted), objHR.EnumEvaluationStatus.Deleted))

    End Sub 

    Sub BindSearchList() 

        srchStatusList.Items.Add(New ListItem(objHR.mtdGetEvaluationStatus(objHR.EnumEvaluationStatus.All), objHR.EnumEvaluationStatus.All))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetEvaluationStatus(objHR.EnumEvaluationStatus.Active), objHR.EnumEvaluationStatus.Active))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetEvaluationStatus(objHR.EnumEvaluationStatus.Deleted), objHR.EnumEvaluationStatus.Deleted))
        srchStatusList.SelectedIndex = 1

    End Sub 

    Protected Function LoadData() As DataSet
        Dim srchStatus as string
        Dim strParam as string
        Dim SearchStr as string
        Dim sortitem as string


        SearchStr =  " AND EVAL.Status like '" & IIF(Not srchStatusList.selectedItem.Value = objHR.EnumEvaluationStatus.All, _
                       srchStatusList.selectedItem.Value, "%" ) &"' "
        If NOT srchEvalCode.text = "" Then
            SearchStr =  SearchStr & " AND EVAL.EvalCode like '" & srchEvalCode.text &"%' "
        End If
        If NOT srchDesc.text = "" Then
            SearchStr = SearchStr & " AND EVAL.Description like '" & _
                        srchDesc.text &"%' "
        End If
        If NOT srchUpdateBy.text = "" Then
            SearchStr = SearchStr & " AND USR.UserName like '" & _
                        srchUpdateBy.text &"%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text 
        strParam =  sortitem & "|" & SearchStr

        Try
        intErrNo = objHR.mtdGetMasterList(strOppCd_GET, strParam, objHR.EnumHRMasterType.Evaluation, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_EVALUATION_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        Select Case cint(Edittext.text) = objHR.EnumEvaluationStatus.Active
            Case True
                Statuslist.selectedindex = 0
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("EvalCode")
                EditText.readonly = true
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
         		'Updbutton.Attributes.Add("onclick","return confirm('Are you sure you want to delete this code?');")
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                Statuslist.selectedindex = 1
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("EvalCode")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Description")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("IncRate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("IncAmt")
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
        Dim EvalCode As String
        Dim Description As String
        Dim IncRate As String
        Dim IncAmt As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg as label
        Dim CreateDate As String
 
        EditText = E.Item.FindControl("EvalCode")
        EvalCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        EditText = E.Item.FindControl("IncRate")
        IncRate = EditText.Text
        EditText = E.Item.FindControl("IncAmt")
        IncAmt = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.Selecteditem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  EvalCode & "|" & _
                    Description & "|" & _
                    IncRate & "|" & _
                    IncAmt & "|" & _
                    Status & "|" & _
                    CreateDate 

        If (IncRate = "0" And IncAmt = "0") Or (IncRate <> "0" And IncAmt <> "0") Then
            ErrLabel = E.Item.FindControl("lblRateErr")
            ErrLabel.Text = lblErrIncMessage.Text
            Exit Sub
        ElseIf (CDbl(IncRate) > 100) Then
            ErrLabel = E.Item.FindControl("lblRateErr")
            ErrLabel.Text = lblErrRateMessage.Text
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
                                          objHR.EnumHRMasterType.Evaluation, _
                                          blnDupKey, _
                                          blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_UPDATE_EVALUATION&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Evaluation.aspx")
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
        Dim EvalCode As String
        Dim Description As String
        Dim IncRate As String
        Dim IncAmt As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim CreateDate As String


        EditText = E.Item.FindControl("EvalCode")
        EvalCode = EditText.Text
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        EditText = E.Item.FindControl("IncRate")
        IncRate = EditText.Text
        EditText = E.Item.FindControl("IncAmt")
        IncAmt = EditText.Text
        EditText = E.Item.FindControl("Status")
        Status = IIF(EditText.Text = objHR.EnumEvaluationStatus.Active, _
                                     objHR.EnumEvaluationStatus.Deleted, _
                                     objHR.EnumEvaluationStatus.Active )
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  EvalCode & "|" & _
                    Description & "|" & _
                    IncRate & "|" & _
                    IncAmt & "|" & _
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
                                          objHR.EnumHRMasterType.Evaluation, _
                                          blnDupKey, _
                                          blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DELETE_EVALUATION&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Evaluation.aspx")
        End Try
      
        EventData.EditItemIndex = -1
        BindGrid() 

    End Sub

    Sub DEDR_Add(Sender As Object, E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim Updbutton as LinkButton
        Dim PageCount As Integer

        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("EvalCode") = ""
        newRow.Item("Description") = ""
        newRow.Item("IncRate") = 0
        newRow.Item("IncAmt") = 0
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

    End Sub

End Class
