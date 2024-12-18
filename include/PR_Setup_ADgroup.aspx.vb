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

Public Class PR_Setup_ADgroup : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchADGrpCd As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchUpdBy As TextBox
    Protected WithEvents srchStatus As DropDownList

    Protected objPR As New agri.PR.clsSetup()  
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()  

    Dim strOppCd_GET As String = "PR_CLSSETUP_ADGRP_LIST_GET"
    Dim strOppCd_ADD As String = "PR_CLSSETUP_ADGROUP_LIST_ADD"
    Dim strOppCd_UPD As String = "PR_CLSSETUP_ADGROUP_LIST_UPD"
    Dim objDataSet As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""


    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRADGroup), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else

            If SortExpression.Text = "" Then
                SortExpression.Text = "ADGrpCode"
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
        StatusList.Items.Add(New ListItem(objPR.mtdGetADGrpStatus(objPR.EnumADGrpStatus.Active), objPR.EnumADGrpStatus.Active))
        StatusList.Items.Add(New ListItem(objPR.mtdGetADGrpStatus(objPR.EnumADGrpStatus.Deleted), objPR.EnumADGrpStatus.Deleted))

    End Sub

    Sub BindSearchList()
        srchStatus.Items.Add(New ListItem(objPR.mtdGetADGrpStatus(objPR.EnumADGrpStatus.All), objPR.EnumAdGrpStatus.All))
        srchStatus.Items.Add(New ListItem(objPR.mtdGetADGrpStatus(objPR.EnumADGrpStatus.Active), objPR.EnumAdGrpStatus.Active))
        srchStatus.Items.Add(New ListItem(objPR.mtdGetADGrpStatus(objPR.EnumADGrpStatus.Deleted), objPR.EnumAdGrpStatus.Deleted))
        srchStatus.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet

        Dim strParam As String
        Dim SearchStr As String
        Dim sortItem As String


        SearchStr = " AND ADGrp.Status like '" & IIf(srchStatus.SelectedItem.Value <> objPR.EnumADGrpStatus.All, srchStatus.SelectedItem.Value, "%") & "' "

        If Not srchADGrpCd.Text = "" Then
            SearchStr = SearchStr & " AND ADGrp.ADGrpCode like '" & srchADGrpCd.Text & "%'"
        End If
        If Not srchDescription.Text = "" Then
            SearchStr = SearchStr & " AND ADGrp.Description like '" & srchDescription.Text & "%'"
        End If
        If Not srchUpdBy.Text = "" Then
            SearchStr = SearchStr & " AND Usr.UserName like '" & srchUpdBy.Text & "%'"
        End If

        sortItem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortItem & "|" & SearchStr

        Try
            intErrNo = objPR.mtdGetMasterList(strOppCd_GET, strParam, objPR.EnumPayrollMasterType.ADGroup, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ADGROUP&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_ADgroup.aspx")
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

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim lbUpdbutton As LinkButton

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid()
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(txtEditText.Text) = objPR.EnumADGrpStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ADGrpCode")
                txtEditText.ReadOnly = True
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Delete"
                lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ADGrpCode")
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
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As DropDownList
        Dim strADGrpCd As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim strCreateDate As String

        txtEditText = E.Item.FindControl("ADGrpCode")
        strADGrpCd = txtEditText.Text
        txtEditText = E.Item.FindControl("Description")
        strDescription = txtEditText.Text
        ddlList = E.Item.FindControl("StatusList")
        strStatus = ddlList.SelectedItem.Value
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        strParam = strADGrpCd & "|" & _
                    strDescription & "|" & _
                    strStatus & "|" & _
                    strCreateDate
        Try
            intErrNo = objPR.mtdUpdMasterList(strOppCd_ADD, _
                                              strOppCd_UPD, _
                                              strOppCd_GET, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objPR.EnumPayrollMasterType.ADGroup, _
                                              blnDupKey, _
                                              blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_ADGROUP&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_ADgroup.aspx")
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
        Dim strADGrpCd As String
        Dim strDescription As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim strCreateDate As String

        txtEditText = E.Item.FindControl("ADGrpCode")
        strADGrpCd = txtEditText.Text
        txtEditText = E.Item.FindControl("Description")
        strDescription = txtEditText.Text
        txtEditText = E.Item.FindControl("Status")
        strStatus = IIf(txtEditText.Text = objPR.EnumADGrpStatus.Active, _
                        objPR.EnumADGrpStatus.Deleted, _
                        objPR.EnumADGrpStatus.Active)
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        strParam = strADGrpCd & "|" & _
                    strDescription & "|" & _
                    strStatus & "|" & _
                    strCreateDate
        Try
            intErrNo = objPR.mtdUpdMasterList(strOppCd_ADD, _
                                              strOppCd_UPD, _
                                              strOppCd_GET, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strParam, _
                                              objPR.EnumPayrollMasterType.ADGroup, _
                                              blnDupKey, _
                                              blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_ADGROUP&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_ADgroup.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim lbUpdbutton As LinkButton
        Dim PageCount As Integer

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("ADGrpCode") = ""
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

        lbUpdbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        lbUpdbutton.Visible = False
    End Sub

End Class
