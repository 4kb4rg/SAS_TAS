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
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class FA_setup_AssetReg : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents lblOper As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents srchAssetHeader As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchAssetClass As TextBox
    Protected WithEvents srchAssetGrp As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblAssetHeader As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblAssetClass As Label
    Protected WithEvents lblAssetGrp As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblList As Label

    Protected objFASetup As New agri.FA.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOppCd_GET As String = "FA_CLSSETUP_ASSETREG_LIST_GET"
    Dim strOppCd_ADD As String = "FA_CLSSETUP_ASSETREG_LIST_ADD"
    Dim strOppCd_UPD As String = "FA_CLSSETUP_ASSETREG_LIST_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim objClsDs As New Object()
    Dim objGrpDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intFAAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim strValidateAC As String
    Dim strValidateAG As String
    Dim DocTitleTag As String
    Dim AssetHeaderTag As String
    Dim DescriptionTag As String
    Dim AssetClassTag As String
    Dim AssetGrpTag As String

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim intConfigSetting As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intFAAR = Session("SS_FAAR")
        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FARegistrationSetup), intFAAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "FA.AssetHeaderCode"
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
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.AssetRegHeader))
        lblAssetHeader.Text = GetCaption(objLangCap.EnumLangCap.AssetHeader) & lblCode.Text
        lblDescription.Text = GetCaption(objLangCap.EnumLangCap.AssetHeaderDesc)
        lblAssetClass.Text = GetCaption(objLangCap.EnumLangCap.AssetClass)
        lblAssetGrp.Text = GetCaption(objLangCap.EnumLangCap.AssetGrp)

        DocTitleTag = lblTitle.Text & lblList.Text
        AssetHeaderTag = GetCaption(objLangCap.EnumLangCap.AssetHeader) & lblCode.Text
        DescriptionTag = GetCaption(objLangCap.EnumLangCap.AssetHeaderDesc)
        AssetClassTag = GetCaption(objLangCap.EnumLangCap.AssetClass)
        AssetGrpTag = GetCaption(objLangCap.EnumLangCap.AssetGrp)

        strValidateCode = lblPleaseEnter.Text & lblAssetHeader.Text & "."
        strValidateDesc = lblPleaseEnter.Text & lblDescription.Text & "."
        strValidateAC = lblPleaseEnter.Text & lblAssetClass.Text & "."
        strValidateAG = lblPleaseEnter.Text & lblAssetGrp.Text & "."

        EventData.Columns(0).HeaderText = lblAssetHeader.Text
        EventData.Columns(1).HeaderText = lblDescription.Text
        EventData.Columns(2).HeaderText = lblAssetClass.Text
        EventData.Columns(3).HeaderText = lblAssetGrp.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_ASSETREG_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=FA/Setup/FA_setup_AssetReg.aspx")
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
        StatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetRegStatus(objFASetup.EnumAssetRegStatus.Active), objFASetup.EnumAssetRegStatus.Active))
        StatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetRegStatus(objFASetup.EnumAssetRegStatus.Deleted), objFASetup.EnumAssetRegStatus.Deleted))
    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetRegStatus(objFASetup.EnumAssetRegStatus.Active), objFASetup.EnumAssetRegStatus.Active))
        srchStatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetRegStatus(objFASetup.EnumAssetRegStatus.Deleted), objFASetup.EnumAssetRegStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetRegStatus(objFASetup.EnumAssetRegStatus.All), objFASetup.EnumAssetRegStatus.All))
    End Sub

    Protected Function LoadData() As DataSet
        Dim Period As String

        strParam = srchAssetHeader.Text & "|" & _
                    srchDescription.Text & "|" & _
                    srchAssetClass.Text & "|" & _
                    srchAssetGrp.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & _
                    srchUpdateBy.Text & "|" & _
                    SortExpression.Text & " " & SortCol.Text

        Try
            intErrNo = objFASetup.mtdGetAssetReg(strOppCd_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_ASSETREG_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/Setup/FA_setup_AssetReg.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strAssetHeader As String
        Dim strDescription As String
        Dim strAssetClass As String
        Dim strAssetGrp As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = srchStatusList.SelectedItem.Value
        strAssetHeader = srchAssetHeader.Text
        strDescription = srchDescription.Text
        strAssetClass = srchAssetClass.Text
        strAssetGrp = srchAssetGrp.Text
        strUpdateBy = srchUpdateBy.Text
        strSortExp = SortExpression.Text
        strSortCol = SortCol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/FA_Rpt_AssetReg.aspx?strStatus=" & strStatus & _
                       "&strAssetHeader=" & strAssetHeader & _
                       "&strDescription=" & strDescription & _
                       "&strAssetClass=" & strAssetClass & _
                       "&strAssetGrp=" & strAssetGrp & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&DocTitleTag=" & lblTitle.Text & _
                       "&AssetHeaderTag=" & AssetHeaderTag & _
                       "&DescriptionTag=" & DescriptionTag & _
                       "&AssetClassTag=" & AssetClassTag & _
                       "&AssetGrpTag=" & AssetGrpTag & _
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

    Protected Function ClassDataSet(ByVal pv_ClsCode As String, ByRef pr_intIndex As Integer) As DataSet
        Dim strOpCd As String = "FA_CLSSETUP_ASSETCLASS_LIST_GET"
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim dsForInactiveItem As DataSet

        pr_intIndex = 0
        SearchStr = " AND FA.Status = '" & objFASetup.EnumAssetClassStatus.Active & "'" 
        sortitem = "ORDER BY FA.AssetClassCode ASC "
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objFASetup.mtdGetMasterList(strOpCd, strParam, objClsDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CLASS_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objClsDs.Tables(0).Rows.Count - 1
            objClsDs.Tables(0).Rows(intCnt).Item("AssetClassCode") = Trim(objClsDs.Tables(0).Rows(intCnt).Item("AssetClassCode"))
            objClsDs.Tables(0).Rows(intCnt).Item("Description") = objClsDs.Tables(0).Rows(intCnt).Item("AssetClassCode") & " (" & Trim(objClsDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objClsDs.Tables(0).Rows(intCnt).Item("AssetClassCode") = Trim(pv_ClsCode) Then
                pr_intIndex = intCnt + 1
            End If
        Next


        dr = objClsDs.Tables(0).NewRow()
        dr("AssetClassCode") = ""
        dr("Description") = "Select " & lblAssetClass.Text
        objClsDs.Tables(0).Rows.InsertAt(dr, 0)

        If pr_intIndex = 0 And Not lblOper.Text = objFASetup.EnumOperation.Add Then
            SearchStr = " AND FA.AssetClassCode = '" & Trim(pv_ClsCode) & "'"
            sortitem = "ORDER BY FA.AssetClassCode ASC "
            strParam = sortitem & "|" & SearchStr

            Try
                intErrNo = objFASetup.mtdGetMasterList(strOpCd, strParam, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                    dr = objClsDs.Tables(0).NewRow()
                    dr("AssetClassCode") = dsForInactiveItem.Tables(0).Rows(0).Item("AssetClassCode")
                    dr("Description") = dsForInactiveItem.Tables(0).Rows(0).Item("AssetClassCode") & " (" & objFASetup.mtdGetAssetClassStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") "
                    objClsDs.Tables(0).Rows.add(dr)
                    pr_intIndex = objClsDs.Tables(0).Rows.Count - 1
                Else 
                    pr_intIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CLASS_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        End If

        Return objClsDs
    End Function

    Protected Function GrpDataSet(ByVal pv_GRPCode As String, ByRef pr_intIndex As Integer) As DataSet
        Dim strOpCd As String = "FA_CLSSETUP_ASSETGRP_LIST_GET"
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim dsForInactiveItem As DataSet

        pr_intIndex = 0
        SearchStr = " AND FA.Status = '" & objFASetup.EnumAssetGrpStatus.Active & "'"
        sortitem = "ORDER BY FA.AssetGrpCode ASC "
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objFASetup.mtdGetMasterList(strOpCd, strParam, objGrpDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GROUP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objGrpDs.Tables(0).Rows.Count - 1
            objGrpDs.Tables(0).Rows(intCnt).Item("AssetGrpCode") = Trim(objGrpDs.Tables(0).Rows(intCnt).Item("AssetGrpCode"))
            objGrpDs.Tables(0).Rows(intCnt).Item("Description") = objGrpDs.Tables(0).Rows(intCnt).Item("AssetGrpCode") & " (" & Trim(objGrpDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objGrpDs.Tables(0).Rows(intCnt).Item("AssetGrpCode") = Trim(pv_GRPCode) Then
                pr_intIndex = intCnt + 1
            End If
        Next

        dr = objGrpDs.Tables(0).NewRow()
        dr("AssetGrpCode") = ""
        dr("Description") = "Select " & lblAssetGrp.Text
        objGrpDs.Tables(0).Rows.InsertAt(dr, 0)

        If pr_intIndex = 0 And Not lblOper.Text = objFASetup.EnumOperation.Add Then
            SearchStr = " AND FA.AssetGrpCode = '" & Trim(pv_GRPCode) & "'"
            sortitem = "ORDER BY FA.AssetGrpCode ASC "
            strParam = sortitem & "|" & SearchStr

            Try
                intErrNo = objFASetup.mtdGetMasterList(strOpCd, strParam, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                    dr = objGrpDs.Tables(0).NewRow()
                    dr("AssetGrpCode") = dsForInactiveItem.Tables(0).Rows(0).Item("AssetGrpCode")
                    dr("Description") = dsForInactiveItem.Tables(0).Rows(0).Item("AssetGrpCode") & " (" & objFASetup.mtdGetAssetGrpStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") "
                    objGrpDs.Tables(0).Rows.add(dr)
                    pr_intIndex = objGrpDs.Tables(0).Rows.Count - 1
                Else 
                    pr_intIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GROUP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        End If

        Return objGrpDs
    End Function

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim LabelText As Label
        Dim EditList As DropDownList
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim validateAC As RequiredFieldValidator
        Dim validateAG As RequiredFieldValidator
        Dim intSelectedAcc As Integer

        lblOper.Text = objFASetup.EnumOperation.Update
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        LabelText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAssetClassCode")
        EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("AssetClassCode")
        EditList.DataSource = ClassDataSet(LabelText.Text, intSelectedAcc)
        EditList.DataValueField = "AssetClassCode"
        EditList.DataTextField = "Description"
        EditList.DataBind()
        EditList.SelectedIndex = intSelectedAcc

        LabelText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAssetGrpCode")
        EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("AssetGrpCode")
        EditList.DataSource = GrpDataSet(LabelText.Text, intSelectedAcc)
        EditList.DataValueField = "AssetGrpCode"
        EditList.DataTextField = "Description"
        EditList.DataBind()
        EditList.SelectedIndex = intSelectedAcc

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(EditText.Text) = objFASetup.EnumAssetRegStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("AssetHeaderCode")
                EditText.ReadOnly = True
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("AssetHeaderCode")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Description")
                EditText.Enabled = False

                EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("AssetClassCode")
                EditList.Enabled = False
                EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("AssetGrpCode")
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
        validateDesc = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateDesc")
        validateAC = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateAC")
        validateAG = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateAG")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc
        validateAC.ErrorMessage = strValidateAC
        validateAG.ErrorMessage = strValidateAG

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim EditList As DropDownList
        Dim strAssetHeader As String
        Dim strDescription As String
        Dim strAssetClass As String
        Dim strAssetGrp As String
        Dim strStatus As String
        Dim strCreateDate As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim intError As Integer

        EditText = E.Item.FindControl("AssetHeaderCode")
        strAssetHeader = EditText.Text
        EditText = E.Item.FindControl("Description")
        strDescription = EditText.Text
        EditList = E.Item.FindControl("AssetClassCode")
        strAssetClass = EditList.SelectedItem.Value
        EditList = E.Item.FindControl("AssetGrpCode")
        strAssetGrp = EditList.SelectedItem.Value
        EditList = E.Item.FindControl("StatusList")
        strStatus = EditList.SelectedItem.Value
        EditText = E.Item.FindControl("CreateDate")
        strCreateDate = EditText.Text

        strParam = strAssetHeader & "|" & _
                    strDescription & "|" & _
                    strAssetClass & "|" & _
                    strAssetGrp & "|" & _
                    strStatus

        Try

            intErrNo = objFASetup.mtdUpdAssetReg(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    lblOper.Text, _
                                                    intError)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_ASSETREG&errmesg=" & lblErrMessage.Text & "&redirect=FA/Setup/FA_setup_AssetReg.aspx")
        End Try

        If intError = objFASetup.EnumErrorType.DuplicateKey Then
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
        Dim strAssetHeader As String
        Dim strDescription As String
        Dim strAssetClass As String
        Dim strAssetGrp As String
        Dim strStatus As String
        Dim strCreateDate As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim intError As Integer

        EditText = E.Item.FindControl("AssetHeaderCode")
        strAssetHeader = EditText.Text
        EditText = E.Item.FindControl("Description")
        strDescription = EditText.Text
        EditList = E.Item.FindControl("AssetClassCode")
        strAssetClass = EditList.SelectedItem.Value
        EditList = E.Item.FindControl("AssetGrpCode")
        strAssetGrp = EditList.SelectedItem.Value

        EditText = E.Item.FindControl("Status")
        strStatus = IIf(EditText.Text = objFASetup.EnumAssetRegStatus.Active, _
                        objFASetup.EnumAssetRegStatus.Deleted, _
                        objFASetup.EnumAssetRegStatus.Active)
        EditText = E.Item.FindControl("CreateDate")
        strCreateDate = EditText.Text

        strParam = strAssetHeader & "|" & _
                    strDescription & "|" & _
                    strAssetClass & "|" & _
                    strAssetGrp & "|" & _
                    strStatus

        Try
            intErrNo = objFASetup.mtdUpdAssetReg(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    lblOper.Text, _
                                                    intError)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETE_ASSETREG&errmesg=" & lblErrMessage.Text & "&redirect=FA/Setup/FA_setup_AssetReg.aspx")
        End Try

        If intError = objFASetup.EnumErrorType.DuplicateKey Then
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
        Dim validateDesc As RequiredFieldValidator
        Dim validateAC As RequiredFieldValidator
        Dim validateAG As RequiredFieldValidator
        Dim PageCount As Integer
        Dim ddlClassCode As DropDownList
        Dim strClsCode As String = ""
        Dim ddlGrpCode As DropDownList
        Dim strGrpCode As String = ""
        Dim intSelectedAcc As Integer

        lblOper.Text = objFASetup.EnumOperation.Add
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("LocCode") = strLocation
        newRow.Item("AssetHeaderCode") = ""
        newRow.Item("Description") = ""
        newRow.Item("AssetClassCode") = ""
        newRow.Item("AssetGrpCode") = ""
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

        ddlClassCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("AssetClassCode")
        ddlClassCode.DataSource = ClassDataSet(strClsCode, intSelectedAcc)
        ddlClassCode.DataValueField = "AssetClassCode"
        ddlClassCode.DataTextField = "Description"
        ddlClassCode.DataBind()
        ddlClassCode.SelectedIndex = intSelectedAcc

        ddlGrpCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("AssetGrpCode")
        ddlGrpCode.DataSource = GrpDataSet(strGrpCode, intSelectedAcc)
        ddlGrpCode.DataValueField = "AssetGrpCode"
        ddlGrpCode.DataTextField = "Description"
        ddlGrpCode.DataBind()
        ddlGrpCode.SelectedIndex = intSelectedAcc

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

        validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        validateDesc = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")
        validateAC = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateAC")
        validateAG = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateAG")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc
        validateAC.ErrorMessage = strValidateAC
        validateAG.ErrorMessage = strValidateAG

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
