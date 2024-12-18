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


Public Class PR_Setup_Contractor : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchContractorCd As TextBox
    Protected WithEvents srchName As TextBox
    Protected WithEvents srchUpdBy As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents srchEmployeeCd As DropDownList
    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblSelect As Label

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objPR As New agri.PR.clsSetup()
    Protected objHR As New agri.HR.clsTrx()
    Protected objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOpCd_SEARCH As String = "PR_CLSSETUP_CONTRACTOR_LIST_SEARCH"
    Dim strOpCd_ADD As String = "PR_CLSSETUP_CONTRACTOR_LIST_ADD"
    Dim strOpCd_UPD As String = "PR_CLSSETUP_CONTRACTOR_LIST_UPD"

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim objEmpDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strAccountTag As String
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAllowanceDeduction), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "ContractorCode"
                sortcol.Text = "ASC"
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
        strAccountTag = GetCaption(objLangCap.EnumLangCap.Account)
        EventData.Columns(3).HeaderText = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_setup_Contractor.aspx")
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

    Sub onSelect_Emp(sender As Object, e As EventArgs)
        Dim txt As TextBox
        Dim EditList As DropDownList
        Dim strEmpCode As String
        Dim strEmpName As String
    
        EditList = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("EmpCode")
        strEmpCode = EditList.SelectedItem.Value

        If Trim(strEmpCode) = "" Then
            txt = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Name")
            txt.Text = ""
            txt.Enabled = True            
        Else
            strEmpName = GetEmpName(strEmpCode)
            txt = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Name")
            txt.Text = strEmpName
            txt.Enabled = False
        End If        
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
        StatusList.Items.Add(New ListItem(objPR.mtdGetContrListStatus(objPR.EnumContrListStatus.Active), objPR.EnumContrListStatus.Active))
        StatusList.Items.Add(New ListItem(objPR.mtdGetContrListStatus(objPR.EnumContrListStatus.Deleted), objPR.EnumContrListStatus.Deleted))

    End Sub

    Sub BindSearchList()
        srchStatus.Items.Add(New ListItem(objPR.mtdGetContrListStatus(objPR.EnumContrListStatus.All), objPR.EnumContrListStatus.All))
        srchStatus.Items.Add(New ListItem(objPR.mtdGetContrListStatus(objPR.EnumContrListStatus.Active), objPR.EnumContrListStatus.Active))
        srchStatus.Items.Add(New ListItem(objPR.mtdGetContrListStatus(objPR.EnumContrListStatus.Deleted), objPR.EnumContrListStatus.Deleted))
        srchStatus.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet

        Dim strParam As String
        Dim SearchStr As String
        Dim sortItem As String


        SearchStr = " AND Ctr.Status like '" & IIf(srchStatus.SelectedItem.Value <> objPR.EnumContrListStatus.All, srchStatus.SelectedItem.Value, "%") & "' "

        If Not srchContractorCd.Text = "" Then
            SearchStr = SearchStr & " AND Ctr.ContractorCode like '" & srchContractorCd.Text & "%'"
        End If
        If Not srchName.Text = "" Then
            SearchStr = SearchStr & " AND Ctr.Name like '" & srchName.Text & "%'"
        End If
        If Not srchUpdBy.Text = "" Then
            SearchStr = SearchStr & " AND Usr.UserName like '" & srchUpdBy.Text & "%'"
        End If

        sortItem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortItem & "|" & SearchStr

        Try
            intErrNo = objPR.mtdGetMasterList(strOpCd_SEARCH, strParam, objPR.EnumPayrollMasterType.Contractor, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_CONTRACTOR&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_Contractor.aspx")
        End Try
        Return objDataSet
    End Function

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

    Protected Function EmpDataSet(ByVal pv_strEmpCode As String, ByRef pr_intIndex As Integer) As DataSet
        Dim strOpCd As String = "PR_CLSSETUP_CONTRACTOR_EMPLOYEELIST_GET"
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String
        Dim intCnt As Integer
        Dim dr As DataRow

        pr_intIndex = 0
        SearchStr = " WHERE Status = '" & objHR.EnumEmpStatus.Active & "' AND LocCode = '" & strLocation & "' "
        sortitem = "ORDER BY EmpCode ASC "
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objPR.mtdGetMasterList(strOpCd, strParam, objPR.EnumPayRollMasterType.Contractor, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_CONTRACTOR_EMPLOYEELIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") & " (" & Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
            If objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(pv_strEmpCode) Then
                pr_intIndex = intCnt + 1
            End If
        Next

        dr = objEmpDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = "Select Employee Code"
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        Return objEmpDs
    End Function

    Protected Function BindAccount(ByVal pv_strAccCode As String, ByRef pr_intIndex As Integer) As DataSet
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim objAccDs As New Object()
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & _
                                "' And ACC.AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer

        pr_intIndex = 0
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CONTRACTOR_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(objAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                pr_intIndex = intCnt + 1
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = lblSelect.Text & strAccountTag & lblCode.Text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        Return objAccDs
    End Function


    Protected Function GetEmpName(ByVal pv_strEmpCode) As String
        Dim strOpCd = "PR_CLSSETUP_CONTRACTOR_EMPLOYEELIST_GET"
        Dim strEmpName As String
        Dim objEmpDs As New Object()
        Dim SearchStr As String
        Dim SortItem As String
        Dim strParam As String

        SearchStr = "WHERE EmpCode = '" & pv_strEmpCode & "' AND LocCode = '" & strLocation & "' "
        SortItem = ""
        strParam = SortItem & "|" & SearchStr

        Try
            intErrNo = objPR.mtdGetMasterList(strOpCd, strParam, objPR.EnumPayRollMasterType.Contractor, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_CONTRACTOR_EMPLOYEENAME_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        objEmpDs.Tables(0).Rows(0).Item("EmpName") = Trim(objEmpDs.Tables(0).Rows(0).Item("EmpName"))
        strEmpName = objEmpDs.Tables(0).Rows(0).Item("EmpName")
        Return strEmpName
    End Function

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim lbUpdbutton As LinkButton
        Dim LabelText As Label
        Dim EditList As DropDownList
        Dim intSelected As Integer
        Dim strEmpCode As String

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid()
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        LabelText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblEmpCode")
        strEmpCode = LabelText.Text

        If Trim(strEmpCode) <> "" Then
            txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Name")
            txtEditText.Enabled = False
        End If

        EditList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("EmpCode")
        EditList.DataSource = EmpDataSet(LabelText.Text, intSelected)
        EditList.DataValueField = "EmpCode"
        EditList.DataTextField = "EmpName"
        EditList.DataBind()
        EditList.SelectedIndex = intSelected

        LabelText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccCode")
        EditList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("AccCode")
        EditList.DataSource = BindAccount(LabelText.Text, intSelected)
        EditList.DataValueField = "AccCode"
        EditList.DataTextField = "Description"
        EditList.DataBind()
        EditList.SelectedIndex = intSelected

        txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(txtEditText.Text) = objPR.EnumContrListStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ContractorCode")
                txtEditText.ReadOnly = True
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Delete"
                lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ContractorCode")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Name")
                txtEditText.Enabled = False
                EditList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("EmpCode")
                EditList.Enabled = False
                EditList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("AccCode")
                EditList.Enabled = False
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
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim lblLabel As Label
        Dim strContractorCd As String
        Dim strName As String
        Dim strEmpCode As String
        Dim strAccCode As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim strCreateDate As String
        Dim strEmpName As String

        txtEditText = E.Item.FindControl("ContractorCode")
        strContractorCd = txtEditText.Text
        txtEditText = E.Item.FindControl("Name")
        strName = txtEditText.Text
        ddlList = E.Item.FindControl("EmpCode")
        strEmpCode = ddlList.SelectedItem.Value

        ddlList = E.Item.FindControl("AccCode")
        strAccCode = ddlList.SelectedItem.Value
        If strAccCode = "" Then
            lblLabel = E.Item.FindControl("lblErrAccCode")
            lblLabel.Text = lblErrSelect.Text & strAccountTag & lblCode.Text
            lblLabel.Visible = True
            Exit Sub
        End If
        ddlList = E.Item.FindControl("StatusList")
        strStatus = ddlList.SelectedItem.Value
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        strParam = strContractorCd & "|" & _
                    strName & "|" & _
                    strEmpCode & "|" & _
                    strAccCode & "|" & _
                    strStatus & "|" & _
                    strCreateDate
        Try
            intErrNo = objPR.mtdUpdMasterList(strOpCd_ADD, _
                                              strOpCd_UPD, _
                                              strOpCd_SEARCH, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objPR.EnumPayrollMasterType.Contractor, _
                                              blnDupKey, _
                                              blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_CONTRACTOR&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_Contractor.aspx")
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
        Dim ddlList As DropDownList
        Dim strContractorCd As String
        Dim strName As String
        Dim strEmpCode As String
        Dim strAccCode As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim strCreateDate As String

        txtEditText = E.Item.FindControl("ContractorCode")
        strContractorCd = txtEditText.Text
        txtEditText = E.Item.FindControl("Name")
        strName = txtEditText.Text
        ddlList = E.Item.FindControl("EmpCode")
        strEmpCode = ddlList.SelectedItem.Value
        ddlList = E.Item.FindControl("AccCode")
        strAccCode = ddlList.SelectedItem.Value
        txtEditText = E.Item.FindControl("Status")
        strStatus = IIf(txtEditText.Text = objPR.EnumContrListStatus.Active, _
                        objPR.EnumContrListStatus.Deleted, _
                        objPR.EnumContrListStatus.Active)
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        strParam = strContractorCd & "|" & _
                    strName & "|" & _
                    strEmpCode & "|" & _
                    strAccCode & "|" & _
                    strStatus & "|" & _
                    strCreateDate
        Try
            intErrNo = objPR.mtdUpdMasterList(strOpCd_ADD, _
                                              strOpCd_UPD, _
                                              strOpCd_SEARCH, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objPR.EnumPayrollMasterType.Contractor, _
                                              blnDupKey, _
                                              blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_CONTRACTOR_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_Contractor.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim lbUpdbutton As LinkButton
        Dim strEmpCode As String = ""
        Dim ddlEmpCode As DropDownList
        Dim intSelected As Integer
        Dim PageCount As Integer

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("ContractorCode") = ""
        newRow.Item("Name") = ""
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

        ddlEmpCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("EmpCode")
        ddlEmpCode.DataSource = EmpDataSet("", intSelected)
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataTextField = "EmpName"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intSelected

        ddlEmpCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("AccCode")
        ddlEmpCode.DataSource = BindAccount("", intSelected)
        ddlEmpCode.DataValueField = "AccCode"
        ddlEmpCode.DataTextField = "Description"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intSelected

        lbUpdbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        lbUpdbutton.Visible = False
    End Sub


End Class
