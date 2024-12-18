Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PR
Imports agri.PWSystem.clsLangCap

Public Class PR_Setup_SalIncList : Inherits Page

    Protected WithEvents EventData as DataGrid
    Protected WithEvents ErrorMessage as Label
    Protected WithEvents lblTracker as Label
    Protected WithEvents lblErrMessage as Label
    Protected WithEvents strState as Label
    Protected WithEvents SQLStatement as Label
    Protected WithEvents lstDropList as DropDownList
    Protected WithEvents StatusList as DropDownList
    Protected WithEvents srchStatusList as DropDownList
    Protected WithEvents SortExpression as Label
    Protected WithEvents blnUpdate as Label
    Protected WithEvents curStatus as Label
    Protected WithEvents sortcol as Label
    Protected WithEvents srchYear as TextBox
    Protected WithEvents srchValue as TextBox
    Protected WithEvents srchIncrease as TextBox
    Protected WithEvents srchUpdateBy as TextBox
    Protected WithEvents lblTitle as Label
    Protected WithEvents lblYear as Label
    Protected WithEvents lblIncrease as Label
    Protected WithEvents lblCode as Label
    Protected WithEvents lblPleaseEnter as Label
    Protected WithEvents lblList as Label

    Protected WithEvents txtValue as TextBox
    Protected WithEvents txtIncrease as TextBox
    Protected WithEvents txtYear as TextBox

    Protected objPR As New agri.PR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objHRTrx As New agri.HR.clsTrx()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOppCd_GET As String = "PR_CLSSETUP_SALINC_LIST_GET"
    Dim strOppCd_ADD As String = "PR_CLSSETUP_SALINC_LIST_ADD"
    Dim strOppCd_UPD As String = "PR_CLSSETUP_SALINC_LIST_UPD"

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
    Dim intPRAR As Long
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strValidateYear As String
    Dim strValidateCont As String
    Dim strValidateIncrease As String
    Dim strValidateValue As String
    Dim DocTitleTag As String
    Dim SalaryIncreaseTag As String
    Dim strLocType as String
    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRSalaryIncrease), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.text = "" Then
                SortExpression.text = "Year"
                sortcol.text = "DESC"
            End If
            If Not Page.IsPostBack Then
                BindSearchList() 
                BindGrid() 
                BindPageList()
            End If
        End If
    End Sub
    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid() 
        BindPageList()
    End Sub 



    Sub btnPreview_Click (sender As Object, e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strYear As String
        Dim strValue As String
        Dim strIncrease As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIF(Not srchStatusList.selectedItem.Value = objPR.EnumSalaryIncreaseStatus.All, srchStatusList.selectedItem.Value, "")
        strYear = srchYear.text
        strIncrease = srchIncrease.text
        strValue = srchValue.text
        strUpdateBy =  srchUpdateBy.text
        strSortExp = sortexpression.text
        strSortCol = sortcol.text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PR_Rpt_SalInc.aspx?strStatus=" & strStatus & _
                       "&strYear=" & strYear & _
                       "&strIncrease=" & strIncrease & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&DocTitleTag=" & DocTitleTag & _
                       "&strValue=" & strValue & _
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
        Dim ValidateCont As RequiredFieldValidator
        Dim ValidateIncrease As RequiredFieldValidator
        Dim ValidateValue As RequiredFieldValidator
        Dim DataLabel As Label

        strState.Text = "edit"
        blnUpdate.text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid() 
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case cint(Edittext.text) = objPR.EnumSalaryIncreaseStatus.Active
            Case True
                Statuslist.selectedindex = 0
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtYear")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtValue")
                EditText.Enabled = False
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                Statuslist.selectedindex = 1
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtYear")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UpdateDate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UserName")
                EditText.Enabled = False
                List = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("StatusList")
                List.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtValue")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtIncrease")
                EditText.Enabled = False
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select  
        ValidateIncrease = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ValidateIncrease")
        ValidateIncrease.ErrorMessage = strValidateIncrease 
        ValidateValue  = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ValidateValue")
        ValidateValue.ErrorMessage = strValidateValue
    End Sub

    Sub DEDR_Update(Sender As Object, E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As Dropdownlist
        Dim Year As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg as label
        Dim Value As String
        Dim Status As String
        Dim CreateDate As String
        Dim Increase As String
        Dim Cont As String

        EditText = E.Item.FindControl("txtYear")
        Year = EditText.Text
        EditText = E.Item.FindControl("txtValue")
        Value = EditText.Text
        EditText = E.Item.FindControl("txtIncrease")
        Increase = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.Selecteditem.Value

        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  Year & "|" & _
                    Value & "|" & _
                    Increase & "|" & _
                    Status & "|||" & _
                    strUserId 
        Try
        intErrNo = objPR.mtdUpdMasterList(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objPR.EnumPayrollMasterType.SalaryIncrease, _
                                                blnDupKey, _
                                                blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_SalIncLIST&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_SalIncList.aspx")
        End Try

        If blnDupKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid() 
            strState.Text = ""
        End If

  End Sub

    Sub DEDR_Cancel(Sender As Object, E As DataGridCommandEventArgs)
        strState.Text = ""
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)

        Dim EditText As TextBox
        Dim list As DropDownList
        Dim Year As String
        Dim Description As String
        Dim blnDupKey As Boolean = False
        Dim Status As String
        Dim CreateDate As String

        Dim Value As String       
        Dim Increase As String
        Dim Cont As String

        EditText = E.Item.FindControl("txtYear")
        Year = EditText.Text
        EditText = E.Item.FindControl("Status")
        Status = IIF(EditText.Text = objPR.EnumSalaryIncreaseStatus.Active, _
                        objPR.EnumSalaryIncreaseStatus.Deleted, _
                        objPR.EnumSalaryIncreaseStatus.Active )
        EditText = E.Item.FindControl("txtValue")
        Value = Trim(EditText.Text)
        EditText = E.Item.FindControl("txtIncrease")
        Increase = Trim(EditText.Text)
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  Year & "|" & _
                    Value & "|" & _
                    Increase & "|" & _
                    Status & "|||" & _
                    strUserId 
        Try
        intErrNo = objPR.mtdUpdMasterList(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objPR.EnumPayrollMasterType.SalaryIncrease, _
                                                blnDupKey, _
                                                blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_PRODMODEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_prodmodel.aspx")
        End Try
      
        EventData.EditItemIndex = -1
        
        If  CInt(e.Item.ItemIndex) = 0 and EventData.Items.Count = 1  And EventData.PageCount <> 1 And srchStatusList.selectedItem.Value <> objPR.EnumSalaryIncreaseStatus.All then
            EventData.CurrentPageIndex = EventData.PageCount - 2 
        End If
        
        BindGrid() 
        BindPageList()
    End Sub

    Sub DEDR_Add(Sender As Object, E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim txtCont As TextBox
        Dim txtValue As TextBox
        Dim txtIncrease As TextBox
        Dim ValidateYear As RequiredFieldValidator
        Dim ValidateCont As RequiredFieldValidator
        Dim ValidateIncrease As RequiredFieldValidator
        Dim ValidateValue As RequiredFieldValidator
        Dim PageCount As Integer

        strState.Text = "add"

        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("Year") = ""
        newRow.Item("Value") = 0
        newRow.Item("Increase") = 0
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.now()
        NewRow.Item("UpdateDate") = DateTime.now()
        NewRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)
        
        EventData.DataSource = dataSet.Tables(0)
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
        EventData.EditItemIndex = EventData.Items.Count - 1

        EventData.DataBind()
        BindStatusList(EventData.EditItemIndex)
        
        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.visible = False


        txtValue = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("txtValue")
        txtIncrease = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("txtIncrease")

        If dataSet.Tables(0).Rows(EventData.EditItemIndex).Item("Value") = 0 Then
            txtValue.Text = ""
        End If
        If dataSet.Tables(0).Rows(EventData.EditItemIndex).Item("Increase") = 0 Then
            txtIncrease.Text = ""
        End If
        ValidateYear = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateYear")
        ValidateYear.ErrorMessage = strValidateYear
        ValidateIncrease = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateIncrease")
        ValidateIncrease.ErrorMessage = strValidateIncrease
        ValidateValue = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateValue")
        ValidateValue.ErrorMessage = strValidateValue

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.SalaryIncrease))

        strValidateYear = "<br>" & lblPleaseEnter.text & "Year"
        strValidateCont = "<br>" & lblPleaseEnter.text & "Value"
        strValidateIncrease = "<br>" & lblPleaseEnter.text & "Min. Score"
        strValidateValue = "<br>" & lblPleaseEnter.text & "Max. Score"

        DocTitleTag = lblTitle.text & lblList.text
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
        StatusList.Items.Add(New ListItem(objPR.mtdGetSalaryIncreaseStatus(objPR.EnumSalaryIncreaseStatus.Active), objPR.EnumSalaryIncreaseStatus.Active))
        StatusList.Items.Add(New ListItem(objPR.mtdGetSalaryIncreaseStatus(objPR.EnumSalaryIncreaseStatus.Deleted), objPR.EnumSalaryIncreaseStatus.Deleted))

    End Sub 

    Sub BindSearchList() 

        srchStatusList.Items.Add(New ListItem(objPR.mtdGetSalaryIncreaseStatus(objPR.EnumSalaryIncreaseStatus.Active), objPR.EnumSalaryIncreaseStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPR.mtdGetSalaryIncreaseStatus(objPR.EnumSalaryIncreaseStatus.Deleted), objPR.EnumSalaryIncreaseStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objPR.mtdGetSalaryIncreaseStatus(objPR.EnumSalaryIncreaseStatus.All), objPR.EnumSalaryIncreaseStatus.All))

    End Sub 

    Protected Function LoadData() As DataSet
        
        Dim Year as string
        Dim Value as string
        Dim Increase as string
        Dim EvalValue as string
        Dim UpdateBy as string
        Dim srchStatus as string
        Dim strParam as string
        Dim SearchStr as string
        Dim sortitem as string
        Dim intCnt as integer

       
        SearchStr =  " AND SI.Status like '" & IIF(Not srchStatusList.selectedItem.Value = objPR.EnumSalaryIncreaseStatus.All, _
                       srchStatusList.selectedItem.Value, "%" ) &"' "

        If NOT srchYear.text = "" Then
            SearchStr = SearchStr & " AND SI.Year like '" & srchYear.text &"%' "
        End If


        If NOT srchValue.text = "" Then
            SearchStr = SearchStr & " AND SI.Value like '" & _
                        Trim(srchValue.Text) &"%' "
        End If

        If NOT srchIncrease.text = "" Then
            SearchStr = SearchStr & " AND SI.Increase like '" & _
                        Trim(srchIncrease.Text) &"%' "
        End If

        If NOT srchUpdateBy.text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & _
                        srchUpdateBy.text &"%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text 
        strParam =  sortitem & "|" & SearchStr

        Try
        intErrNo = objPR.mtdGetMasterList(strOppCd_GET, strParam, objPR.EnumPayrollMasterType.SalaryIncrease, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STANDARD_EVALUATION&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_SalIncList.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            For intCnt=0 To objDataSet.Tables(0).Rows.Count - 1
                objDataSet.Tables(0).Rows(intCnt).Item("Year") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Year"))
                objDataSet.Tables(0).Rows(intCnt).Item("Value") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Value"))
                objDataSet.Tables(0).Rows(intCnt).Item("Increase") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Increase")) 
                objDataSet.Tables(0).Rows(intCnt).Item("Status") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Status"))
                objDataSet.Tables(0).Rows(intCnt).Item("CreateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("CreateDate"))
                objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate"))
                objDataSet.Tables(0).Rows(intCnt).Item("UserName") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

       Return objDataSet
    End Function




End Class
