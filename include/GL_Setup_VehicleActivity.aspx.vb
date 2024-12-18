
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


Public Class GL_Setup_VehicleActivity : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchVehActCd As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchUpdBy As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents lblVehActCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblList As Label
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents hidStatusEdited As HtmlInputHidden

    Protected WithEvents ddlType As DropDownList

    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim ObjGLTrx As New agri.GL.ClsTrx

    Dim strOppCd_GET As String = "GL_CLSSETUP_VEHACTIVITY_LIST_SEARCH"
    Dim strOppCd_ADD As String = "GL_CLSSETUP_VEHACTIVITY_LIST_ADD"
    Dim strOppCd_UPD As String = "GL_CLSSETUP_VEHACTIVITY_LIST_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim strAccCodeTag As String
    Dim intConfigsetting As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim ParamName As String
    Dim ParamValue As String

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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "VehActCode"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
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
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Activity)) & " " & UCase(GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text)
        lblVehActCode.Text = GetCaption(objLangCap.EnumLangCap.Activity) & " " & GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblDescription.Text = GetCaption(objLangCap.EnumLangCap.Activity) & " " & GetCaption(objLangCap.EnumLangCap.VehicleDesc)
        strValidateCode = lblPleaseEnter.Text & lblVehActCode.Text & "."
        strValidateDesc = lblPleaseEnter.Text & lblDescription.Text & "."
        strAccCodeTag = GetCaption(objLangCap.EnumLangCap.Account)

        EventData.Columns(0).HeaderText = lblVehActCode.Text
        EventData.Columns(1).HeaderText = lblDescription.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHEXPGRPLIST_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_VehicleSubGrpCode.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
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
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetVehicleStatus(objGLSetup.EnumVehicleStatus.Active), objGLSetup.EnumVehicleStatus.Active))
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetVehicleStatus(objGLSetup.EnumVehicleStatus.Deleted), objGLSetup.EnumVehicleStatus.Deleted))
    End Sub

    Sub BindSearchList()
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetVehicleStatus(objGLSetup.EnumVehicleStatus.All), objGLSetup.EnumVehicleStatus.All))
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetVehicleStatus(objGLSetup.EnumVehicleStatus.Active), objGLSetup.EnumVehicleStatus.Active))
        srchStatus.Items.Add(New ListItem(objGLSetup.mtdGetVehicleStatus(objGLSetup.EnumVehicleStatus.Deleted), objGLSetup.EnumVehicleStatus.Deleted))
        srchStatus.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet

        Dim strParam As String
        Dim SearchStr As String
        Dim sortItem As String


        SearchStr = " AND Acc.Status like '" & IIf(srchStatus.SelectedItem.Value <> objGLSetup.EnumVehicleStatus.All, srchStatus.SelectedItem.Value, "%") & "' "

        If Not srchVehActCd.Text = "" Then
            SearchStr = SearchStr & " AND Acc.VehActCode like '" & srchVehActCd.Text & "%'"
        End If
        If Not srchDescription.Text = "" Then
            SearchStr = SearchStr & " AND Acc.Description like '" & srchDescription.Text & "%'"
        End If
        If Not srchUpdBy.Text = "" Then
            SearchStr = SearchStr & " AND Usr.UserName like '" & srchUpdBy.Text & "%'"
        End If
        SearchStr = SearchStr & " And Acc.LocCode= '" & strLocation & "'"

        sortItem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortItem & "|" & SearchStr

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOppCd_GET, strParam, objGLSetup.EnumGLMasterType.Vehicle, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_GET_VehActCode&errmesg=" & Exp.ToString() & "&redirect=GL/Setup/GL_Setup_VehicleSubGrpCode.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strVehActCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIf(Not srchStatus.SelectedItem.Value = objGLSetup.EnumVehicleStatus.All, srchStatus.SelectedItem.Value, "")
        strVehActCode = srchVehActCd.Text
        strDescription = srchDescription.Text
        strUpdateBy = srchUpdBy.Text
        strSortExp = SortExpression.Text
        strSortCol = sortcol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_VehExpGrp.aspx?strStatus=" & strStatus & _
                       "&strVehActCode=" & strVehActCode & _
                       "&strDescription=" & strDescription & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&strVehActCodeTag=" & lblVehActCode.Text & _
                       "&strDescTag=" & lblDescription.Text & _
                       "&strTitleTag=" & lblTitle.Text & lblList.Text & _
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

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim lbUpdbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim lblTemp As Label
        Dim ddlTemp As DropDownList
        hidStatusEdited.Value = "1"

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid()
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If

        BindStatusList(EventData.EditItemIndex)
        BindAccCodeDropList(EventData.EditItemIndex)

        txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(txtEditText.Text) = objGLSetup.EnumVehicleStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("VehActCode")
                txtEditText.ReadOnly = True
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Delete"
                lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

                lblTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccCode")
                ddlTemp = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlAccount")
                If Not (lblTemp Is Nothing) Then
                    ddlTemp.SelectedValue = Trim(lblTemp.Text)
                End If

            Case False
                StatusList.SelectedIndex = 1
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("VehActCode")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Description")
                txtEditText.Enabled = False
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
        validateDesc = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("validateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim strVehActCode As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim strCreateDate As String
        Dim list As DropDownList
        Dim strAccCode As String
        Dim strType As String

        txtEditText = E.Item.FindControl("VehActCode")
        strVehActCode = txtEditText.Text
        txtEditText = E.Item.FindControl("Description")
        strDescription = txtEditText.Text
        ddlList = E.Item.FindControl("StatusList")
        strStatus = ddlList.SelectedItem.Value
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text

        list = E.Item.FindControl("ddlAccount")
        strAccCode = list.SelectedItem.Value

        list = E.Item.FindControl("ddlType")
        strType = list.SelectedItem.Value

        ParamName = "SEARCHSTR|SORTEXP"
        ParamValue = " And Acc.LocCode = '" & Trim(strLocation) & "' And Acc.VehActCode = '" & Trim(strVehActCode) & "'|Order by VehActCode"

        Try
            intErrNo = ObjGLTrx.mtdGetDataCommon(strOppCd_GET, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_CATEGORY_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            If hidStatusEdited.Value = "1" Then
                ParamName = "LOCCODE|VEHACTCODE|DESCRIPTION|ACCCODE|STATUS|CREATEDATE|UPDATEDATE|UPDATEID|TYPE"
                ParamValue = strLocation & "|" & _
                             strVehActCode & "|" & _
                             strDescription & "|" & _
                             strAccCode & "|" & _
                             strStatus & "|" & _
                             strCreateDate & "|" & _
                             Now() & "|" & _
                             strUserId & "|" & strType
                Try
                    intErrNo = ObjGLTrx.mtdInsertDataCommon(strOppCd_UPD, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_VEHICLEACTIVITY&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Religion.aspx")
                End Try

                EventData.EditItemIndex = -1
                BindGrid()
            Else
                lblMsg = E.Item.FindControl("lblDupMsg")
                lblMsg.Visible = True
            End If
        Else
            ParamName = "LOCCODE|VEHACTCODE|DESCRIPTION|ACCCODE|STATUS|CREATEDATE|UPDATEDATE|UPDATEID|TYPE"
            ParamValue = strLocation & "|" & _
                         strVehActCode & "|" & _
                         strDescription & "|" & _
                         strAccCode & "|" & _
                         strStatus & "|" & _
                         Now() & "|" & _
                         Now() & "|" & _
                         strUserId & "|" & strType
            Try
                intErrNo = ObjGLTrx.mtdInsertDataCommon(strOppCd_ADD, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_VEHICLEACTIVITY&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Religion.aspx")
            End Try

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
        Dim strVehActCode As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim strCreateDate As String
        Dim strAccCode As String
        Dim list As DropDownList

        txtEditText = E.Item.FindControl("VehActCode")
        strVehActCode = txtEditText.Text
        txtEditText = E.Item.FindControl("Description")
        strDescription = txtEditText.Text
        txtEditText = E.Item.FindControl("Status")
        strStatus = IIf(txtEditText.Text = objGLSetup.EnumVehicleStatus.Active, _
                        objGLSetup.EnumVehicleStatus.Deleted, _
                        objGLSetup.EnumVehicleStatus.Active)
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        list = E.Item.FindControl("ddlAccount")
        strAccCode = list.SelectedItem.Value


        ParamName = "LOCCODE|VEHACTCODE|DESCRIPTION|ACCCODE|STATUS|CREATEDATE|UPDATEDATE|UPDATEID"
        ParamValue = strLocation & "|" & _
                     strVehActCode & "|" & _
                     strDescription & "|" & _
                     strAccCode & "|" & _
                     strStatus & "|" & _
                     strCreateDate & "|" & _
                     Now() & "|" & _
                     strUserId
        Try
            intErrNo = ObjGLTrx.mtdInsertDataCommon(strOppCd_UPD, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_VEHICLEACTIVITY&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Religion.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData()
        Dim newRow As DataRow
        Dim lbUpdbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer
        hidStatusEdited.Value = "0"

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("LocCode") = strLocation
        newRow.Item("VehActCode") = ""
        newRow.Item("Description") = ""
        newRow.Item("AccCode") = ""
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
        BindAccCodeDropList(EventData.EditItemIndex)

        lbUpdbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        lbUpdbutton.Visible = False

        validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        validateDesc = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

    End Sub

    Sub BindAccCodeDropList(ByVal index As Integer)
        ddlAccount = EventData.Items.Item(index).FindControl("ddlAccount")

        Dim strOpCdAccCode_Get As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET" '"GL_CLSTRX_ACCOUNTCODE_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim strFieldCheck As String
        'Dim strParam As New StringBuilder("")
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim strWorkShopFilter As String = ""

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.InternalLabourCharge), intConfigsetting) = True Then
            strWorkShopFilter = " AND WSAccDistUse = '" & objGLSetup.EnumWSAccDistUse.No & "' "
        End If

        strParam = strParam & " AND (AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "' OR (AccType = '" & objGLSetup.EnumAccountType.ProfitAndLost & "' AND AccPurpose = '" & objGLSetup.EnumAccountPurpose.NonVehicle & "'))" & strWorkShopFilter
        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND ACC.LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCdAccCode_Get, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            strFieldCheck = dsForDropDown.Tables(0).Rows(intCnt).Item(0)
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
        Next intCnt

        Dim drinsert As DataRow
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = " "
        drinsert(1) = lblPleaseEnter.Text & strAccCodeTag & lblCode.Text
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        ddlAccount.DataSource = dsForDropDown.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "Description"
        ddlAccount.DataBind()

        'If Not dsForDropDown Is Nothing Then
        '    dsForDropDown = Nothing
        'End If
    End Sub
End Class
