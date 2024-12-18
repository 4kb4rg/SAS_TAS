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


Public Class PD_trx_POMStaticList : Inherits Page

    Protected WithEvents EventData as DataGrid
    Protected WithEvents lblTracker as Label
    Protected WithEvents SQLStatement as Label
    Protected WithEvents lstDropList as DropDownList
    Protected WithEvents StatusList as DropDownList
    Protected WithEvents SortExpression as Label
    Protected WithEvents blnUpdate as Label
    Protected WithEvents curStatus as Label
    Protected WithEvents lblDupMsg as Label
    Protected WithEvents sortcol as Label
    Protected WithEvents srchDate as TextBox
    Protected WithEvents srchFFA as TextBox
    Protected WithEvents srchMI as TextBox
    Protected WithEvents srchDOBI as TextBox
    Protected WithEvents srchMM as TextBox
    Protected WithEvents srchStatus as DropDownList
    Protected WithEvents srchUpdBy as TextBox
    
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objPD As New agri.PD.clsTrx()
    Protected strDateFmt As String

    Dim strOpCd_SEARCH As String = "PD_CLSTRX_POMSTATIC_LIST_SEARCH"
    Dim strOpCd_ADD As String = "PD_CLSTRX_POMSTATIC_LIST_ADD"
    Dim strOpCd_UPD As String = "PD_CLSTRX_POMSTATIC_LIST_UPD"

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
    Dim strSrchDate As String = ""

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPDAR = Session("SS_PDAR")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMProduction), intPDAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.text = "" Then
                SortExpression.text = "StaticDate"
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
        Dim objFormatDate As New Object()
        Dim objActualDate As New Object()

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                    srchDate.Text, _
                                    objFormatDate, _
                                    objActualDate) = True Then
            strSrchDate = objActualDate
        Else
            srchDate.Text = ""
        End If

        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid() 
        BindPageList()
    End Sub 

    Sub BindGrid() 
        Dim PageNo as Integer 

        EventData.DataSource = LoadData
        EventData.DataBind()
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
        StatusList.Items.Add(New ListItem(objPD.mtdGetPOMStaticStatus(objPD.EnumPOMStaticStatus.Active), objPD.EnumPOMStaticStatus.Active))
        StatusList.Items.Add(New ListItem(objPD.mtdGetPOMStaticStatus(objPD.EnumPOMStaticStatus.Deleted), objPD.EnumPOMStaticStatus.Deleted))
    End Sub 

    Sub BindSearchList()
        srchStatus.Items.Add(New ListItem(objPD.mtdGetPOMStaticStatus(objPD.EnumPOMStaticStatus.Active), objPD.EnumPOMStaticStatus.Active))
        srchStatus.Items.Add(New ListItem(objPD.mtdGetPOMStaticStatus(objPD.EnumPOMStaticStatus.Deleted), objPD.EnumPOMStaticStatus.Deleted ))
        srchStatus.Items.Add(New ListItem(objPD.mtdGetPOMStaticStatus(objPD.EnumPOMStaticStatus.All), objPD.EnumPOMStaticStatus.All))
        srchStatus.SelectedIndex = 0
    End Sub

    Protected Function LoadData() As DataSet
        Dim strParam as string
        Dim SearchStr as string = ""
        Dim sortItem as string

        SearchStr = " AND S.Status like '" & IIf(srchStatus.SelectedItem.Value <> objPD.EnumPOMStaticStatus.All, srchStatus.SelectedItem.Value, "%" ) & "' "

        If Not strSrchDate = "" Then
            SearchStr = SearchStr & " AND S.StaticDate = '" & strSrchDate & "'"
        End If
        If Not srchFFA.text = "" Then
            SearchStr = SearchStr & " AND S.FFA = " & srchFFA.text
        End If
        If Not srchMI.text = "" Then
            SearchStr = SearchStr & " AND S.MI = " & srchMI.text
        End If
        If Not srchDOBI.text = "" Then
            SearchStr = SearchStr & " AND S.DOBI = " & srchDOBI.text
        End If
        If Not srchMM.text = "" Then
            SearchStr = SearchStr & " AND S.MM = " & srchMM.text
        End If
        If Not srchUpdBy.text = "" Then
            SearchStr = SearchStr & " AND Usr.UserName like '" & srchUpdBy.text & "%'"
        End If
        
        sortItem = "ORDER BY " & sortexpression.text & " " & sortcol.text 
        strParam =  sortItem & "|" & SearchStr

        Try
            intErrNo = objPD.mtdGetPOMStatic(strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strOpCd_SEARCH, _
                                            strParam, _
                                            objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMSTATIC&errmesg=" & Exp.ToString() & "&redirect=PD/trx/PD_trx_POMStaticList.aspx")
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

        BindStatusList(EventData.EditItemIndex)

        txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case cint(txtEditText.text) = objPD.EnumPOMStaticStatus.Active
            Case True
                Statuslist.selectedindex = 0
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("StaticDate")
                txtEditText.readonly = true
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Delete"
                lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                Statuslist.selectedindex = 1
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("StaticDate")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("FFA")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("MI")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("DOBI")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("MM")
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
        Dim lblLabel As Label
        Dim ddlList As DropDownList
        Dim strDate As String
        Dim strFFA As String
        Dim strMI As String
        Dim strDOBI As String
        Dim strMM As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg as label
        Dim strCreateDate As String
        Dim strEmpName As String
        Dim objFormatDate As New Object
        Dim objActualDate As New Object
 
        txtEditText = E.Item.FindControl("StaticDate")

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                    txtEditText.Text, _
                                    objFormatDate, _
                                    objActualDate) = True Then
            strDate = objActualDate
            txtEditText = E.Item.FindControl("FFA")
            strFFA = txtEditText.Text
            txtEditText = E.Item.FindControl("MI")
            strMI = txtEditText.Text
            txtEditText = E.Item.FindControl("DOBI")
            strDOBI = txtEditText.Text
            txtEditText = E.Item.FindControl("MM")
            strMM = txtEditText.Text
            ddlList = E.Item.FindControl("StatusList")
            strStatus = ddllist.SelectedItem.Value
            txtEditText = E.Item.FindControl("CreateDate")
            strCreateDate = txtEditText.Text

            strParam = strDate & "|" & _
                    strFFA & "|" & _
                    strMI & "|" & _
                    strDOBI & "|" & _
                    strMM & "|" & _
                    strStatus & "|" & _
                    strCreateDate 
            Try
                intErrNo = objPD.mtdUpdPOMStatic(strOpCd_ADD, _
                                                strOpCd_UPD, _
                                                strOpCd_SEARCH, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                blnUpdate.text)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMSTATIC_UPDATE&errmesg=" & Exp.ToString() & "&redirect=PD/trx/PD_trx_POMStaticList.aspx")
            End Try
            
            If blnDupKey Then
                lblMsg = E.Item.FindControl("lblDupMsg")
                lblMsg.Visible = True
            Else
                EventData.EditItemIndex = -1
                BindGrid() 
            End If
        Else
            lblLabel = E.Item.FindControl("lblErrStaticDate")
            lblLabel.Text = lblLabel.Text & objFormatDate
            lblLabel.Visible = True
        End If
    End Sub

    Sub DEDR_Cancel(Sender As Object, E As DataGridCommandEventArgs)
        If  CInt(e.Item.ItemIndex) = 0 and EventData.Items.Count = 1 and not EventData.CurrentPageIndex = 0 then
            EventData.CurrentPageIndex = EventData.Pagecount - 2 
            BindGrid()
            BindPageList()
        End If
        EventData.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim txtEditText As TextBox
        Dim lblLabel As Label
        Dim strDate As String
        Dim strFFA As String
        Dim strMI As String
        Dim strDOBI As String
        Dim strMM As String
        Dim strStatus As String
        Dim blnDupKey As Boolean = False
        Dim strCreateDate As String
        Dim objFormatDate As New Object
        Dim objActualDate As New Object

        txtEditText = E.Item.FindControl("StaticDate")

        If objGlobal.mtdValidInputDate(strDateFmt, _
                                       txtEditText.Text, _
                                       objFormatDate, _
                                       objActualDate) = True Then
            strDate = objActualDate
            txtEditText = E.Item.FindControl("FFA")
            strFFA = txtEditText.Text
            txtEditText = E.Item.FindControl("MI")
            strMI = txtEditText.Text
            txtEditText = E.Item.FindControl("DOBI")
            strDOBI = txtEditText.Text
            txtEditText = E.Item.FindControl("MM")
            strMM = txtEditText.Text
            txtEditText = E.Item.FindControl("Status")
            strStatus = IIF(txtEditText.Text = objPD.EnumPOMStaticStatus.Active, _
                            objPD.EnumPOMStaticStatus.Deleted, _
                            objPD.EnumPOMStaticStatus.Active )
            txtEditText = E.Item.FindControl("CreateDate")
            strCreateDate = txtEditText.Text
            strParam =  strDate & "|" & _
                        strFFA & "|" & _
                        strMI & "|" & _
                        strDOBI & "|" & _
                        strMM & "|" & _
                        strStatus & "|" & _
                        strCreateDate 

            Try
                intErrNo = objPD.mtdUpdPOMStatic(strOpCd_ADD, _
                                                strOpCd_UPD, _
                                                strOpCd_SEARCH, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                blnDupKey, _
                                                blnUpdate.text)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMSTATIC_DELETE&errmesg=" & Exp.ToString() & "&redirect=PD/trx/PD_trx_POMStaticList.aspx")
            End Try
          
            EventData.EditItemIndex = -1
            BindGrid()
        Else
            lblLabel = E.Item.FindControl("lblErrStaticDate")
            lblLabel.Text = lblLabel.Text & objFormatDate
            lblLabel.Visible = True
        End If
    End Sub

    Sub DEDR_Add(Sender As Object, E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim lbUpdbutton As LinkButton
        Dim strEmpCode As String = ""
        Dim ddlEmpCode As DropDownList
        Dim intSelected As Integer

        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("StaticDate") = DateTime.now()
        newRow.Item("FFA") = "0"
        newRow.Item("MI") = "0"
        newRow.Item("DOBI") = "0"
        newRow.Item("MM") = "0"
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.now()
        NewRow.Item("UpdateDate") = DateTime.now()
        NewRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)
        
        EventData.DataSource = dataSet
        EventData.DataBind()
        BindPageList()

        EventData.CurrentPageIndex = EventData.PageCount - 1
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count -1
        EventData.DataBind()

        BindStatusList(EventData.EditItemIndex)

        lbUpdbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        lbUpdbutton.visible = False
    End Sub


End Class
