Imports System
Imports System.Data
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


Public Class GL_Setup_AccCls : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblLenMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchAccClsCd As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchUpdBy As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents lblAccClsCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label

    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strOppCd_GET As String = "GL_CLSSETUP_ACCCLS_LIST_SEARCH"
    Dim strOppCd_ADD As String = "GL_CLSSETUP_ACCCLS_LIST_ADD"
    Dim strOppCd_UPD As String = "GL_CLSSETUP_ACCCLS_LIST_UPD"
    Dim objDataSet As New Object()
    Dim objAccGrpDs As New Object()
    Dim objConfigDs As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim intMaxLen As Integer = 0
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim strAccClsGrpTag As String
    Dim AccClsTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccCls), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "AccClsCode"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
                AssignMaxLength(intMaxLen)
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub AssignMaxLength(ByRef pr_intMaxLen As Integer)
        Dim strOpCd As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim intErrNo As Integer

        Try
            intErrNo = objSys.mtdGetConfigInfo(strOpCd, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               objConfigDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ACTIVITY_GETMAXLENGTH&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_Activity.aspx")
        End Try

        pr_intMaxLen = CInt(Trim(objConfigDs.Tables(0).Rows(0).Item("AccClassLen")))
        srchAccClsCd.MaxLength = pr_intMaxLen
    End Sub

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

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.AccClass))
        AccClsTag = GetCaption(objLangCap.EnumLangCap.AccClass)
        lblAccClsCode.Text = GetCaption(objLangCap.EnumLangCap.AccClass) & lblCode.Text
        lblDescription.Text = GetCaption(objLangCap.EnumLangCap.AccClassDesc)
        strAccClsGrpTag = GetCaption(objLangCap.EnumLangCap.AccClsGrp)

        strValidateCode = lblPleaseEnter.Text & lblAccClsCode.Text & "."
        strValidateDesc = lblPleaseEnter.Text & lblDescription.Text & "."

        EventData.Columns(0).HeaderText = lblAccClsCode.Text
        EventData.Columns(1).HeaderText = lblDescription.Text
        EventData.Columns(2).HeaderText = strAccClsGrpTag
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_AccCls.aspx")
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
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetAccClsStatus(objGLSetup.EnumAccClsStatus.Active), objGLSetup.EnumAccClsStatus.Active))
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetAccClsStatus(objGLSetup.EnumAccClsStatus.Deleted), objGLSetup.EnumAccClsStatus.Deleted))

    End Sub

    Sub BindSearchList()
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetAccClsStatus(objGLSetup.EnumAccClsStatus.All), objGLSetup.EnumAccClsStatus.All))
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetAccClsStatus(objGLSetup.EnumAccClsStatus.Active), objGLSetup.EnumAccClsStatus.Active))
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetAccClsStatus(objGLSetup.EnumAccClsStatus.Deleted), objGLSetup.EnumAccClsStatus.Deleted))
        srchStatus.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet

        Dim strParam As String
        Dim SearchStr As String
        Dim sortItem As String


        SearchStr = " AND Acc.Status like '" & IIf(srchStatus.SelectedItem.Value <> objGLSetup.EnumAccClsStatus.All, srchStatus.SelectedItem.Value, "%") & "' "

        If Not srchAccClsCd.Text = "" Then
            SearchStr = SearchStr & " AND Acc.AccClsCode like '" & srchAccClsCd.Text & "%'"
        End If
        If Not srchDescription.Text = "" Then
            SearchStr = SearchStr & " AND Acc.Description like '" & srchDescription.Text & "%'"
        End If
        If Not srchUpdBy.Text = "" Then
            SearchStr = SearchStr & " AND Usr.UserName like '" & srchUpdBy.Text & "%'"
        End If

        sortItem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortItem & "|" & SearchStr

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOppCd_GET, strParam, objGLSetup.EnumGLMasterType.AccCls, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ACCOUNTCLASS&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_AccCls.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strAccClsCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIf(Not srchStatus.SelectedItem.Value = objGLSetup.EnumAccClsStatus.All, srchStatus.SelectedItem.Value, "")
        strAccClsCode = srchAccClsCd.Text
        strDescription = srchDescription.Text
        strUpdateBy = srchUpdBy.Text
        strSortExp = SortExpression.Text
        strSortCol = sortcol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_AccCls.aspx?strStatus=" & strStatus & _
                       "&strAccClsCode=" & strAccClsCode & _
                       "&strDescription=" & strDescription & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&DocTitleTag=" & lblTitle.Text & _
                       "&AccClsTag=" & AccClsTag & _
                       "&AccClsDescTag=" & lblDescription.Text & _
                       "&AccClsGrpTag=" & strAccClsGrpTag & _
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
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Protected Function AccClsGroupDataSet(ByVal pv_AccClsGrpCode As String, ByRef pr_intIndex As Integer) As DataSet
        Dim strOpCd As String = "GL_CLSSETUP_ACCCLSGRP_LIST_SEARCH"
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String
        Dim intCnt As Integer
        Dim dr As DataRow

        pr_intIndex = 0
        SearchStr = " AND Acc.Status = '" & objGLSetup.EnumAccClsGrpStatus.Active & "'"
        sortitem = "ORDER BY Acc.AccClsGrpCode ASC "
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.AccCls, objAccGrpDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_CONTRACTOR_EMPLOYEELIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccGrpDs.Tables(0).Rows.Count - 1
            objAccGrpDs.Tables(0).Rows(intCnt).Item("AccClsGrpCode") = Trim(objAccGrpDs.Tables(0).Rows(intCnt).Item("AccClsGrpCode"))
            objAccGrpDs.Tables(0).Rows(intCnt).Item("Description") = objAccGrpDs.Tables(0).Rows(intCnt).Item("AccClsGrpCode") & " (" & Trim(objAccGrpDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objAccGrpDs.Tables(0).Rows(intCnt).Item("AccClsGrpCode") = Trim(pv_AccClsGrpCode) Then
                pr_intIndex = intCnt + 1
            End If
        Next

        dr = objAccGrpDs.Tables(0).NewRow()
        dr("AccClsGrpCode") = ""
        dr("Description") = "Select " & lblAccClsCode.Text
        objAccGrpDs.Tables(0).Rows.InsertAt(dr, 0)
        Return objAccGrpDs
    End Function


    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim lbUpdbutton As LinkButton
        Dim LabelText As Label
        Dim EditList As DropDownList
        Dim intSelectedAcc As Integer
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid()
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        LabelText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccClsGrpCode")
        EditList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("AccClsGrpCode")
        EditList.DataSource = AccClsGroupDataSet(LabelText.Text, intSelectedAcc)
        EditList.DataValueField = "AccClsGrpCode"
        EditList.DataTextField = "Description"
        EditList.DataBind()
        EditList.SelectedIndex = intSelectedAcc

        txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(txtEditText.Text) = objGLSetup.EnumAccClsStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("AccClsCode")
                txtEditText.ReadOnly = True
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Delete"
                lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("AccClsCode")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Description")
                txtEditText.Enabled = False
                ddlList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("AccClsGrpCode")
                ddlList.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UpdateDate")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UserName")
                txtEditText.Enabled = False
                ddlList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("StatusList")
                ddlList.Enabled = False
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Update")
                lbUpdbutton.Visible = False
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Undelete"
        End Select
        validateCode = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateCode")
        validateDesc = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ValidateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim strAccClsCd As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim strCreateDate As String
        Dim strAccClsGrpCd As String

        AssignMaxLength(intMaxLen)

        txtEditText = E.Item.FindControl("AccClsCode")
        strAccClsCd = txtEditText.Text

        If strAccClsCd.Length = intMaxLen Then
        Else
            lblMsg = E.Item.FindControl("lblLenMsg")
            lblMsg.Text = "The A/C Class Code should be in " & intMaxLen & " character(s)."
            lblMsg.Visible = True
            Exit Sub
        End If

        txtEditText = E.Item.FindControl("Description")
        strDescription = txtEditText.Text
        ddlList = E.Item.FindControl("AccClsGrpCode")
        strAccClsGrpCd = ddlList.SelectedItem.Value
        ddlList = E.Item.FindControl("StatusList")
        strStatus = ddlList.SelectedItem.Value
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        strParam = strAccClsCd & "|" & _
                    strDescription & "|" & _
                    strAccClsGrpCd & "|" & _
                    strStatus & "|" & _
                    strCreateDate

        Try
            intErrNo = objGLSetup.mtdUpdMasterList(strOppCd_ADD, _
                                              strOppCd_UPD, _
                                              strOppCd_GET, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objGLSetup.EnumGLMasterType.AccCls, _
                                              blnDupKey, _
                                              blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_ACCOUNTCLASS&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_AccCls.aspx")
        End Try

        If blnDupKey Then
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

        Dim txtEditText As TextBox
        Dim strAccClsCd As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim strCreateDate As String
        Dim strAccClsGrpCd As String
        Dim ddlAccClsGrpCd As DropDownList

        txtEditText = E.Item.FindControl("AccClsCode")
        strAccClsCd = txtEditText.Text
        txtEditText = E.Item.FindControl("Description")
        strDescription = txtEditText.Text
        ddlAccClsGrpCd = E.Item.FindControl("AccClsGrpCode")
        strAccClsGrpCd = ddlAccClsGrpCd.SelectedItem.Value
        txtEditText = E.Item.FindControl("Status")
        strStatus = IIf(txtEditText.Text = objGLSetup.EnumAccClsStatus.Active, _
                        objGLSetup.EnumAccClsStatus.Deleted, _
                        objGLSetup.EnumAccClsStatus.Active)
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        strParam = strAccClsCd & "|" & _
                    strDescription & "|" & _
                    strAccClsGrpCd & "|" & _
                    strStatus & "|" & _
                    strCreateDate
        Try
            intErrNo = objGLSetup.mtdUpdMasterList(strOppCd_ADD, _
                                              strOppCd_UPD, _
                                              strOppCd_GET, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objGLSetup.EnumGLMasterType.AccCls, _
                                              blnDupKey, _
                                              blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_ACCOUNTCLASS&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_AccCls.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim lbUpdbutton As LinkButton
        Dim intSelectedAcc As Integer
        Dim ddlAccClsGrpCode As DropDownList
        Dim strAccClsCode As String = ""
        Dim txtAccClsCode As TextBox
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("AccClsCode") = ""
        newRow.Item("Description") = ""
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

        ddlAccClsGrpCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("AccClsGrpCode")
        ddlAccClsGrpCode.DataSource = AccClsGroupDataSet(strAccClsCode, intSelectedAcc)
        ddlAccClsGrpCode.DataValueField = "AccClsGrpCode"
        ddlAccClsGrpCode.DataTextField = "Description"
        ddlAccClsGrpCode.DataBind()
        ddlAccClsGrpCode.SelectedIndex = intSelectedAcc

        lbUpdbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        lbUpdbutton.Visible = False

        AssignMaxLength(intMaxLen)
        txtAccClsCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("AccClsCode")
        txtAccClsCode.MaxLength = intMaxLen

        validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        validateDesc = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

    End Sub


End Class
