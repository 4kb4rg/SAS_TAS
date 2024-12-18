
Imports System
Imports System.Data
Imports System.Text
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Public Class NU_setup_AccDist : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents lblOper As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents srchAccCode As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblList As Label
    Protected WithEvents NurseryStage As DropDownList

    Protected objNUSetup As New agri.NU.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLSetup As New agri.GL.clsSetup()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOppCd_GET As String = "NU_CLSSETUP_ACCDIST_LIST_GET"
    Dim strOppCd_ADD As String = "NU_CLSSETUP_ACCDIST_LIST_ADD"
    Dim strOppCd_UPD As String = "NU_CLSSETUP_ACCDIST_LIST_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim objAccDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intNUAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strValidateCode As String
    Dim DocTitleTag As String
    Dim AccCodeTag As String
    Dim NurseryStageTag As String

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim intConfigSetting As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intNUAR = Session("SS_NUAR")
        strAccMonth = Session("SS_NUACCMONTH")
        strAccYear = Session("SS_NUACCYEAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUWorkAccDist), intNUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "AD.AccCode"
                SortCol.Text = "ASC"
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
       
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text

       
        AccCodeTag = GetCaption(objLangCap.EnumLangCap.Account)
        NurseryStageTag = GetCaption(objLangCap.EnumLangCap.NurseryStage)

        strValidateCode = lblPleaseEnter.Text & lblAccCode.Text & "."

        EventData.Columns(0).HeaderText = lblAccCode.Text
        EventData.Columns(1).HeaderText = NurseryStageTag
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_SETUP_ACCDIST_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=NU/Setup/NU_setup_ACCDIST.aspx")
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
        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
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
        lblTracker.Text = "Page " & PageNo & " of " & EventData.PageCount
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = EventData.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
    End Sub

    Sub BindStatusList(ByVal index As Integer)
        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objNUSetup.mtdGetAccDistStatus(objNUSetup.EnumAccDistStatus.Active), objNUSetup.EnumAccDistStatus.Active))
        StatusList.Items.Add(New ListItem(objNUSetup.mtdGetAccDistStatus(objNUSetup.EnumAccDistStatus.Deleted), objNUSetup.EnumAccDistStatus.Deleted))
    End Sub

    Sub BindNurseryStage(ByVal index As Integer)
        NurseryStage = EventData.Items.Item(index).FindControl("NurseryStage")
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.OneStageNursery), intConfigSetting) = True Then
            NurseryStage.Items.Add(New ListItem(objNUSetup.mtdGetNurseryStage(objNUSetup.EnumNurseryStage.Nursery), objNUSetup.EnumNurseryStage.Nursery))
        Else
            NurseryStage.Items.Add(New ListItem(objNUSetup.mtdGetNurseryStage(objNUSetup.EnumNurseryStage.Infrastructure), objNUSetup.EnumNurseryStage.Infrastructure)) 
            NurseryStage.Items.Add(New ListItem(objNUSetup.mtdGetNurseryStage(objNUSetup.EnumNurseryStage.PreparationPN), objNUSetup.EnumNurseryStage.PreparationPN)) 
            NurseryStage.Items.Add(New ListItem(objNUSetup.mtdGetNurseryStage(objNUSetup.EnumNurseryStage.MaintenancePN), objNUSetup.EnumNurseryStage.MaintenancePN)) 
            NurseryStage.Items.Add(New ListItem(objNUSetup.mtdGetNurseryStage(objNUSetup.EnumNurseryStage.PreparationMN), objNUSetup.EnumNurseryStage.PreparationMN)) 
            NurseryStage.Items.Add(New ListItem(objNUSetup.mtdGetNurseryStage(objNUSetup.EnumNurseryStage.MaintenanceMN), objNUSetup.EnumNurseryStage.MaintenanceMN)) 
            NurseryStage.Items.Add(New ListItem(objNUSetup.mtdGetNurseryStage(objNUSetup.EnumNurseryStage.Overhead), objNUSetup.EnumNurseryStage.Overhead)) 
            NurseryStage.Items.Add(New ListItem(objNUSetup.mtdGetNurseryStage(objNUSetup.EnumNurseryStage.WorkingAcc), objNUSetup.EnumNurseryStage.WorkingAcc)) 
        End If
    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objNUSetup.mtdGetAccDistStatus(objNUSetup.EnumAccDistStatus.All), objNUSetup.EnumAccDistStatus.All))
        srchStatusList.Items.Add(New ListItem(objNUSetup.mtdGetAccDistStatus(objNUSetup.EnumAccDistStatus.Active), objNUSetup.EnumAccDistStatus.Active))
        srchStatusList.Items.Add(New ListItem(objNUSetup.mtdGetAccDistStatus(objNUSetup.EnumAccDistStatus.Deleted), objNUSetup.EnumAccDistStatus.Deleted))

        srchStatusList.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet
        Dim Period As String

        strParam = srchAccCode.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & _
                    srchUpdateBy.Text & "|" & _
                    SortExpression.Text & " " & SortCol.Text

        Try
            intErrNo = objNUSetup.mtdGetAccDist(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../include/mesg/ErrorMessage.aspx?errcode=NU_SETUP_ACCDIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=NU/Setup/NU_setup_AccDist.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strAccCode As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = srchStatusList.SelectedItem.Value
        strAccCode = srchAccCode.Text
        strUpdateBy = srchUpdateBy.Text
        strSortExp = SortExpression.Text
        strSortCol = SortCol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/NU_Rpt_AccDist.aspx?strStatus=" & strStatus & _
                       "&strAccCode=" & strAccCode & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&DocTitleTag=" & lblTitle.Text & _
                       "&AccCodeTag=" & AccCodeTag & _
                       "&NurseryStageTag=" & NurseryStageTag & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
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

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            EventData.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        EventData.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Protected Function AccDataSet(ByVal pv_AccCode As String, ByRef pr_intIndex As Integer) As DataSet
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim dsForInactiveItem As DataSet

        pr_intIndex = 0
        SearchStr = " AND Acc.Status = '" & objGLSetup.EnumAccStatus.Active & "'" & _
                        " AND Acc.NurseryInd = '" & objGLSetup.EnumNurseryAccount.Yes & "'"
        sortitem = "ORDER BY Acc.AccCode ASC "
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.AccountCode, objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ACCOUNT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = objAccDs.Tables(0).Rows(intCnt).Item("AccCode") & " (" & Trim(objAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_AccCode) Then
                pr_intIndex = intCnt + 1
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Select " & lblAccCode.Text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        If pr_intIndex = 0 And Not lblOper.Text = objNUSetup.EnumOperation.Add Then
            SearchStr = " AND Acc.AccCode = '" & pv_AccCode & "'" & _
                            " AND Acc.NurseryInd = '" & objGLSetup.EnumNurseryAccount.Yes & "'"
            sortitem = "ORDER BY Acc.AccCode ASC "
            strParam = sortitem & "|" & SearchStr

            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                    dr = objAccDs.Tables(0).NewRow()
                    dr("AccCode") = Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode"))
                    dr("Description") = Trim(dsForInactiveItem.Tables(0).Rows(0).Item("AccCode")) & _
                                        " (" & objGLSetup.mtdGetAccStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") "
                    objAccDs.Tables(0).Rows.Add(dr)
                    pr_intIndex = objAccDs.Tables(0).Rows.Count - 1
                Else 
                    pr_intIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ACCOUNT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        End If

        Return objAccDs
    End Function



    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim LabelText As Label
        Dim EditList As DropDownList
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim intSelectedAcc As Integer

        lblOper.Text = objNUSetup.EnumOperation.Update
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)
        BindNurseryStage(EventData.EditItemIndex)

        LabelText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAccCode")
        EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("AccCode")
        EditList.DataSource = AccDataSet(LabelText.Text, intSelectedAcc)
        EditList.DataValueField = "AccCode"
        EditList.DataTextField = "Description"
        EditList.DataBind()
        EditList.SelectedIndex = intSelectedAcc

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(EditText.Text) = objNUSetup.EnumAccDistStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("AccCode")
                EditList.Enabled = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("AccCode")
                EditList.Enabled = False
                EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("NurseryStage")
                EditList.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UpdateDate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UserName")
                EditText.Enabled = False
                EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("StatusList")
                EditList.Enabled = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select
        validateCode = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateCode")

        validateCode.ErrorMessage = strValidateCode

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim EditList As DropDownList
        Dim strAccCode As String
        Dim strNurseryStage As String
        Dim strStatus As String
        Dim strCreateDate As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim intError As Integer

        EditList = E.Item.FindControl("AccCode")
        strAccCode = EditList.SelectedItem.Value
        EditList = E.Item.FindControl("NurseryStage")
        strNurseryStage = EditList.SelectedItem.Value
        EditList = E.Item.FindControl("StatusList")
        strStatus = EditList.SelectedItem.Value
        EditText = E.Item.FindControl("CreateDate")
        strCreateDate = EditText.Text

        strParam = strAccCode & "|" & _
                    strNurseryStage & "|" & _
                    strStatus

        Try
            intErrNo = objNUSetup.mtdUpdAccDist(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                lblOper.Text, _
                                                intError)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_ACCDIST&errmesg=" & lblErrMessage.Text & "&redirect=NU/Setup/NU_setup_AccDist.aspx")
        End Try

        If intError = objNUSetup.EnumErrorType.DuplicateKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid()
        End If
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim EditList As DropDownList
        Dim strAccCode As String
        Dim strNurseryStage As String
        Dim strStatus As String
        Dim strCreateDate As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim intError As Integer

        EditList = E.Item.FindControl("AccCode")
        strAccCode = EditList.SelectedItem.Value
        EditList = E.Item.FindControl("NurseryStage")
        strNurseryStage = EditList.SelectedItem.Value
        EditText = E.Item.FindControl("Status")
        strStatus = IIf(EditText.Text = objNUSetup.EnumAccDistStatus.Active, _
                        objNUSetup.EnumAccDistStatus.Deleted, _
                        objNUSetup.EnumAccDistStatus.Active)
        EditText = E.Item.FindControl("CreateDate")
        strCreateDate = EditText.Text

        strParam = strAccCode & "|" & _
                    strNurseryStage & "|" & _
                    strStatus

        Try
            intErrNo = objNUSetup.mtdUpdAccDist(strOppCd_ADD, _
                                                        strOppCd_UPD, _
                                                        strOppCd_GET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        lblOper.Text, _
                                                        intError)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETE_ACCDIST&errmesg=" & lblErrMessage.Text & "&redirect=NU/Setup/NU_setup_AccDist.aspx")
        End Try

        If intError = objNUSetup.EnumErrorType.DuplicateKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid()
        End If
    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim intSelectedAcc As Integer
        Dim ddlAccCode As DropDownList
        Dim strAccCode As String = ""
        Dim PageCount As Integer

        lblOper.Text = objNUSetup.EnumOperation.Add
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("AccCode") = ""
        newRow.Item("NurseryStage") = "0"
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UserName") = ""
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
        lblTracker.Text = "Page " & (EventData.CurrentPageIndex + 1) & " of " & EventData.PageCount
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count - 1
        EventData.DataBind()
        BindStatusList(EventData.EditItemIndex)
        BindNurseryStage(EventData.EditItemIndex)

        ddlAccCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("AccCode")
        ddlAccCode.DataSource = AccDataSet(strAccCode, intSelectedAcc)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectedAcc

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

        validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")

        validateCode.ErrorMessage = strValidateCode

    End Sub

End Class
