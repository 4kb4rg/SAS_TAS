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
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PD

Public Class PD_Setup_TankList : Inherits Page

    Protected WithEvents EventData as DataGrid
    Protected WithEvents lblTracker as Label
    Protected WithEvents SQLStatement as Label
    Protected WithEvents lstDropList as DropDownList
    Protected WithEvents TypeList as DropDownList
    Protected WithEvents StatusList as DropDownList
    Protected WithEvents SortExpression as Label
    Protected WithEvents blnUpdate as Label
    Protected WithEvents curStatus as Label
    Protected WithEvents lblDupMsg as Label
    Protected WithEvents sortcol as Label
    Protected WithEvents srchTankCd as TextBox
    Protected WithEvents srchName as TextBox
    Protected WithEvents srchType as DropDownList
    Protected WithEvents srchStatus as DropDownList
    Protected WithEvents srchUpdBy as TextBox
    
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objPD As New agri.PD.clsSetup()

    Dim strOpCd_SEARCH As String = "PD_CLSSETUP_TANK_LIST_SEARCH"
    Dim strOpCd_ADD As String = "PD_CLSSETUP_TANK_LIST_ADD"
    Dim strOpCd_UPD As String = "PD_CLSSETUP_TANK_LIST_UPD"

    Dim objDataSet As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights() 
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPDAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strAccountTag As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPDAR = Session("SS_PDAR")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMProduction), intPDAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.text = "" Then
                SortExpression.text = "T.TankCode"
                sortcol.text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindSearchList() 
                BindSearchType() 
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
            Count = Count + 1
        End While 

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
    End Sub 

    Sub BindStatusList(index as integer) 
        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objPD.mtdGetTankStatus(objPD.EnumTankStatus.Active), objPD.EnumTankStatus.Active))
        StatusList.Items.Add(New ListItem(objPD.mtdGetTankStatus(objPD.EnumTankStatus.Deleted), objPD.EnumTankStatus.Deleted))
    End Sub 

    Sub BindTypeList(index as integer) 
        TypeList = EventData.Items.Item(index).FindControl("TypeList")
        TypeList.Items.Add(New ListItem("FFB", "FFB"))
        TypeList.Items.Add(New ListItem("CPO", "CPO"))
        TypeList.Items.Add(New ListItem("PK", "PK"))
    End Sub 

    Sub BindSearchList()
        srchStatus.Items.Add(New ListItem(objPD.mtdGetTankStatus(objPD.EnumTankStatus.Active), objPD.EnumTankStatus.Active))
        srchStatus.Items.Add(New ListItem(objPD.mtdGetTankStatus(objPD.EnumTankStatus.Deleted), objPD.EnumTankStatus.Deleted ))
        srchStatus.Items.Add(New ListItem(objPD.mtdGetTankStatus(objPD.EnumTankStatus.All), objPD.EnumTankStatus.All))
        srchStatus.SelectedIndex = 0
    End Sub

    Sub BindSearchType()
        srchType.Items.Add(New ListItem("All", ""))
        srchType.Items.Add(New ListItem("FFB", "FFB"))
        srchType.Items.Add(New ListItem("CPO", "CPO"))
        srchType.Items.Add(New ListItem("PK", "PK"))
        srchType.SelectedIndex = 0
    End Sub

    Protected Function LoadData() As DataSet
        Dim strParam as string
        Dim SearchStr as string = ""
        Dim sortItem as string

        SearchStr = " AND T.Status like '" & IIf(srchStatus.SelectedItem.Value <> objPD.EnumTankStatus.All, srchStatus.SelectedItem.Value, "%" ) & _
                    "' AND T.Type like '" & IIf(srchType.SelectedItem.Value <> "", srchType.SelectedItem.Value, "%" ) & "' "

        If Not srchTankCd.text = "" Then
            SearchStr = SearchStr & " AND T.TankCode like '" & srchTankCd.text & "%'"
        End If
        If Not srchName.text = "" Then
            SearchStr = SearchStr & " AND T.Name like '" & srchName.text & "%'"
        End If
        If Not srchUpdBy.text = "" Then
            SearchStr = SearchStr & " AND Usr.UserName like '" & srchUpdBy.text & "%'"
        End If
        
        sortItem = "ORDER BY " & sortexpression.text & " " & sortcol.text 
        strParam =  sortItem & "|" & SearchStr

        Try
            intErrNo = objPD.mtdGetTank(strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strOpCd_SEARCH, _
                                        strParam, _
                                        objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PD_SETUP_TANKDETAILS_LOAD&errmesg=" & Exp.ToString() & "&redirect=PD/Setup/PD_Setup_TankList.aspx")
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
 
    Sub DEDR_Edit(Sender As Object, e As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As Dropdownlist
        Dim lbUpdbutton As linkbutton
        Dim lblLabel As Label
        Dim EditList As Dropdownlist
        Dim intSelected As Integer
        
        blnUpdate.text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)
        BindGrid() 
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count then
            EventData.EditItemIndex = -1
            Exit Sub
        End If

        BindTypeList(EventData.EditItemIndex)
        BindStatusList(EventData.EditItemIndex)

        txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case cint(txtEditText.text) = objPD.EnumTankStatus.Active
            Case True
                Statuslist.selectedindex = 0
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("TankCode")
                txtEditText.readonly = true
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Delete"
                lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                Statuslist.selectedindex = 1
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("TankCode")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Name")
                txtEditText.Enabled = False
                EditList = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("TypeList")
                EditList.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Storage")
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

    Sub DEDR_Update(Sender As Object, E As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim ddlList As Dropdownlist
        Dim lblLabel As Label
        Dim strTankCd As String
        Dim strName As String
        Dim strType As String
        Dim strStorage As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg as label
        Dim strCreateDate As String
        Dim strEmpName As String
 
        txtEditText = E.Item.FindControl("TankCode")
        strTankCd = txtEditText.Text
        txtEditText = E.Item.FindControl("Name")
        strName = txtEditText.Text
        ddlList = E.Item.FindControl("TypeList")
        strType = ddlList.SelectedItem.value
        txtEditText = E.Item.FindControl("Storage")
        strStorage = txtEditText.Text
        ddlList = E.Item.FindControl("StatusList")
        strStatus = ddllist.SelectedItem.Value
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        strParam =  strTankCd & "|" & _
                    strName & "|" & _
                    strType & "|" & _
                    strStorage & "|" & _
                    strStatus & "|" & _
                    strCreateDate 
        Try
        intErrNo = objPD.mtdUpdTank(strOpCd_ADD, _
                                    strOpCd_UPD, _
                                    strOpCd_SEARCH, _
                                    strCompany, _
                                    strLocation, _
                                    strUserId, _
                                    strParam, _
                                    blnDupKey, _
                                    blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PD_SETUP_TANKDETAILS_UPDATE&errmesg=" & Exp.ToString() & "&redirect=PD/Setup/PD_setup_TankList.aspx")
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
        Dim txtEditText As TextBox
        Dim ddlList  As Dropdownlist
        Dim strTankCd As String
        Dim strName As String
        Dim strType As String
        Dim strStorage As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim strCreateDate As String

        txtEditText = E.Item.FindControl("TankCode")
        strTankCd = txtEditText.Text
        txtEditText = E.Item.FindControl("Name")
        strName = txtEditText.Text
        ddlList = E.Item.FindControl("TypeList")
        strType = ddlList.SelectedItem.value
        txtEditText = E.Item.FindControl("Storage")
        strStorage = txtEditText.Text
        txtEditText = E.Item.FindControl("Status")
        strStatus = IIF(txtEditText.Text = objPD.EnumTankStatus.Active, _
                        objPD.EnumTankStatus.Deleted, _
                        objPD.EnumTankStatus.Active )
        txtEditText = E.Item.FindControl("CreateDate")
        strCreateDate = txtEditText.Text
        strParam =  strTankCd & "|" & _
                    strName & "|" & _
                    strType & "|" & _
                    strStorage & "|" & _
                    strStatus & "|" & _
                    strCreateDate 
        Try

        intErrNo = objPD.mtdUpdTank(strOpCd_ADD, _
                                    strOpCd_UPD, _
                                    strOpCd_SEARCH, _
                                    strCompany, _
                                    strLocation, _
                                    strUserId, _
                                    strParam, _
                                    blnDupKey, _
                                    blnUpdate.text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PD_SETUP_TANKDETAILS_DELETE&errmesg=" & Exp.ToString() & "&redirect=PD/Setup/PD_Setup_TankList.aspx")
        End Try
      
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Add(Sender As Object, E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim lbUpdbutton As LinkButton
        Dim strEmpCode As String = ""
        Dim ddlEmpCode As DropDownList
        Dim intSelected As Integer
        Dim PageCount As Integer
        
        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("TankCode") = ""
        newRow.Item("Name") = ""
        newRow.Item("Type") = "0"
        newRow.Item("Storage") = "0"
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
        BindTypeList(EventData.EditItemIndex)

        lbUpdbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        lbUpdbutton.visible = False
    End Sub


End Class
