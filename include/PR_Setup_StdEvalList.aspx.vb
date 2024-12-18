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


Public Class PR_Setup_StdEvalList : Inherits Page

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
    Protected WithEvents srchGrade as TextBox
    Protected WithEvents srchScoreMin as TextBox
    Protected WithEvents srchScoreMax as TextBox
    Protected WithEvents srchCont as TextBox
    Protected WithEvents srchUpdateBy as TextBox
    Protected WithEvents lblTitle as Label
    Protected WithEvents lblGrade as Label
    Protected WithEvents lblScoreMin as Label
    Protected WithEvents lblScoreMax as Label
    Protected WithEvents lblValue as Label
    Protected WithEvents lblCode as Label
    Protected WithEvents lblPleaseEnter as Label
    Protected WithEvents lblList as Label

    Protected WithEvents txtScoreMin as TextBox
    Protected WithEvents txtScoreMax as TextBox
    Protected WithEvents txtGrade as TextBox
    Protected WithEvents txtRemarks as TextBox
    Protected WithEvents txtCont as TextBox

    Protected objPR As New agri.PR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objHRTrx As New agri.HR.clsTrx()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOppCd_GET As String = "PR_CLSSETUP_STDEVAL_LIST_GET"
    Dim strOppCd_ADD As String = "PR_CLSSETUP_STDEVAL_LIST_ADD"
    Dim strOppCd_UPD As String = "PR_CLSSETUP_STDEVAL_LIST_UPD"

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
    Dim strValidateGrade As String
    Dim strValidateCont As String
    Dim strValidateScoreMax As String
    Dim strValidateScoreMin As String
    Dim DocTitleTag As String
    Dim StandardEvaluationTag As String
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRStandardEvaluation), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.text = "" Then
                SortExpression.text = "Cont"
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
        Dim strGrade As String
        Dim strScoreMax As String
        Dim strScoreMin As String
        Dim strCont As String
        Dim strSortExp As String
        Dim strSortCol As String
        Dim strUpdateBy As String

        strStatus = IIF(Not srchStatusList.selectedItem.Value = objPR.EnumStandardEvaluationStatus.All, srchStatusList.selectedItem.Value, "")
        strGrade = srchGrade.text
        strScoreMax = srchScoreMax.text
        strScoreMin = srchScoreMin.text
        strCont = srchCont.text
        strUpdateBy =  srchUpdateBy.text
        strSortExp = sortexpression.text
        strSortCol = sortcol.text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PR_Rpt_StdEval.aspx?strStatus=" & strStatus & _
                       "&strGrade=" & strGrade & _
                       "&strScoreMax=" & strScoreMax & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&DocTitleTag=" & DocTitleTag & _
                       "&strScoreMin=" & strScoreMin & _
                       "&strCont=" & strCont & _
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
        Dim ValidateScoreMax As RequiredFieldValidator
        Dim ValidateScoreMin As RequiredFieldValidator
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
        Select Case cint(Edittext.text) = objPR.EnumStandardEvaluationStatus.Active
            Case True
                Statuslist.selectedindex = 0
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtGrade")
                EditText.Enabled = False
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                Statuslist.selectedindex = 1
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtGrade")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtCont")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UpdateDate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UserName")
                EditText.Enabled = False
                List = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("StatusList")
                List.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtScoreMin")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("txtScoreMax")
                EditText.Enabled = False
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select  
        ValidateCont = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ValidateCont")
        ValidateCont.ErrorMessage = strValidateCont
        ValidateScoreMax = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ValidateScoreMax")
        ValidateScoreMax.ErrorMessage = strValidateScoreMax 
        ValidateScoreMin  = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ValidateScoreMin")
        ValidateScoreMin.ErrorMessage = strValidateScoreMin
    End Sub

    Sub DEDR_Update(Sender As Object, E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As Dropdownlist
        Dim Grade As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg as label
        Dim ScoreMin As String
        Dim Status As String
        Dim CreateDate As String
        Dim ScoreMax As String
        Dim Cont As String

        EditText = E.Item.FindControl("txtGrade")
        Grade = EditText.Text
        EditText = E.Item.FindControl("txtCont")
        Cont = EditText.Text
        EditText = E.Item.FindControl("txtScoreMin")
        ScoreMin = EditText.Text
        EditText = E.Item.FindControl("txtScoreMax")
        ScoreMax = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.Selecteditem.Value

        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  Grade & "|" & _
                    ScoreMin & "|" & _
                    ScoreMax & "|" & _
                    Cont & "|" & _
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
                                                objPR.EnumPayrollMasterType.StandardEvaluation, _
                                                blnDupKey, _
                                                blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_StdEvalLIST&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_StdEvalList.aspx")
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
        Dim Grade As String
        Dim Description As String
        Dim blnDupKey As Boolean = False
        Dim Status As String
        Dim CreateDate As String

        Dim ScoreMin As String       
        Dim ScoreMax As String
        Dim Cont As String

        EditText = E.Item.FindControl("txtGrade")
        Grade = EditText.Text
        EditText = E.Item.FindControl("txtCont")
        Cont = EditText.Text
        EditText = E.Item.FindControl("Status")
        Status = IIF(EditText.Text = objPR.EnumStandardEvaluationStatus.Active, _
                        objPR.EnumStandardEvaluationStatus.Deleted, _
                        objPR.EnumStandardEvaluationStatus.Active )
        EditText = E.Item.FindControl("txtScoreMin")
        ScoreMin = Trim(EditText.Text)
        EditText = E.Item.FindControl("txtScoreMax")
        ScoreMax = Trim(EditText.Text)
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam =  Grade & "|" & _
                    ScoreMin & "|" & _
                    ScoreMax & "|" & _
                    Cont & "|" & _
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
                                                objPR.EnumPayrollMasterType.StandardEvaluation, _
                                                blnDupKey, _
                                                blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_PRODMODEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_prodmodel.aspx")
        End Try
      
        EventData.EditItemIndex = -1
        
        If  CInt(e.Item.ItemIndex) = 0 and EventData.Items.Count = 1  And EventData.PageCount <> 1 And srchStatusList.selectedItem.Value <> objPR.EnumStandardEvaluationStatus.All then
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
        Dim txtScoreMin As TextBox
        Dim txtScoreMax As TextBox
        Dim ValidateGrade As RequiredFieldValidator
        Dim ValidateCont As RequiredFieldValidator
        Dim ValidateScoreMax As RequiredFieldValidator
        Dim ValidateScoreMin As RequiredFieldValidator
        Dim PageCount As Integer

        strState.Text = "add"

        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("Grade") = ""
        newRow.Item("ScoreMin") = 0
        newRow.Item("ScoreMax") = 0
        newRow.Item("Cont") = 0
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

        txtCont = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("txtCont")
        If dataSet.Tables(0).Rows(EventData.EditItemIndex).Item("Cont") = 0 Then
            txtCont.Text = ""
        End If

        txtScoreMin = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("txtScoreMin")
        txtScoreMax = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("txtScoreMax")

        If dataSet.Tables(0).Rows(EventData.EditItemIndex).Item("ScoreMin") = 0 Then
            txtScoreMin.Text = ""
        End If
        If dataSet.Tables(0).Rows(EventData.EditItemIndex).Item("ScoreMax") = 0 Then
            txtScoreMax.Text = ""
        End If
        ValidateGrade = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateGrade")
        ValidateGrade.ErrorMessage = strValidateGrade
        ValidateScoreMax = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateScoreMax")
        ValidateScoreMax.ErrorMessage = strValidateScoreMax
        ValidateScoreMin = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateScoreMin")
        ValidateScoreMin.ErrorMessage = strValidateScoreMin
        ValidateCont = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateCont")
        ValidateCont.ErrorMessage = strValidateCont

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.StandardEvaluation))

        strValidateGrade = "<br>" & lblPleaseEnter.text & "Grade"
        strValidateCont = "<br>" & lblPleaseEnter.text & "Value"
        strValidateScoreMax = "<br>" & lblPleaseEnter.text & "Min. Score"
        strValidateScoreMin = "<br>" & lblPleaseEnter.text & "Max. Score"

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
        StatusList.Items.Add(New ListItem(objPR.mtdGetStandardEvaluationStatus(objPR.EnumStandardEvaluationStatus.Active), objPR.EnumStandardEvaluationStatus.Active))
        StatusList.Items.Add(New ListItem(objPR.mtdGetStandardEvaluationStatus(objPR.EnumStandardEvaluationStatus.Deleted), objPR.EnumStandardEvaluationStatus.Deleted))

    End Sub 

    Sub BindSearchList() 

        srchStatusList.Items.Add(New ListItem(objPR.mtdGetStandardEvaluationStatus(objPR.EnumStandardEvaluationStatus.Active), objPR.EnumStandardEvaluationStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPR.mtdGetStandardEvaluationStatus(objPR.EnumStandardEvaluationStatus.Deleted), objPR.EnumStandardEvaluationStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objPR.mtdGetStandardEvaluationStatus(objPR.EnumStandardEvaluationStatus.All), objPR.EnumStandardEvaluationStatus.All))

    End Sub 

    Protected Function LoadData() As DataSet
        
        Dim Grade as string
        Dim ScoreMin as string
        Dim ScoreMax as string
        Dim EvalValue as string
        Dim UpdateBy as string
        Dim srchStatus as string
        Dim strParam as string
        Dim SearchStr as string
        Dim sortitem as string
        Dim intCnt as integer

       
        SearchStr =  " AND S.Status like '" & IIF(Not srchStatusList.selectedItem.Value = objPR.EnumStandardEvaluationStatus.All, _
                       srchStatusList.selectedItem.Value, "%" ) &"' "

        If NOT srchGrade.text = "" Then
            SearchStr = SearchStr & " AND S.Grade like '" & srchGrade.text &"%' "
        End If

        If NOT srchCont.text = "" Then
            SearchStr = SearchStr & " AND S.Cont like '" & _
                        srchCont.text &"%' "
        End If

        If NOT srchScoreMin.text = "" Then
            SearchStr = SearchStr & " AND S.ScoreMin like '" & _
                        Trim(srchScoreMin.Text) &"%' "
        End If

        If NOT srchScoreMax.text = "" Then
            SearchStr = SearchStr & " AND S.ScoreMax like '" & _
                        Trim(srchScoreMax.Text) &"%' "
        End If

        If NOT srchUpdateBy.text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & _
                        srchUpdateBy.text &"%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text 
        strParam =  sortitem & "|" & SearchStr

        Try
        intErrNo = objPR.mtdGetMasterList(strOppCd_GET, strParam, objPR.EnumPayrollMasterType.StandardEvaluation, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STANDARD_EVALUATION&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_StdEvalList.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            For intCnt=0 To objDataSet.Tables(0).Rows.Count - 1
                objDataSet.Tables(0).Rows(intCnt).Item("Grade") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Grade"))
                objDataSet.Tables(0).Rows(intCnt).Item("ScoreMin") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("ScoreMin"))
                objDataSet.Tables(0).Rows(intCnt).Item("ScoreMax") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("ScoreMax")) 
                objDataSet.Tables(0).Rows(intCnt).Item("Cont") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Cont"))
                objDataSet.Tables(0).Rows(intCnt).Item("Status") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Status"))
                objDataSet.Tables(0).Rows(intCnt).Item("CreateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("CreateDate"))
                objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate"))
                objDataSet.Tables(0).Rows(intCnt).Item("UserName") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

       Return objDataSet
    End Function


End Class
