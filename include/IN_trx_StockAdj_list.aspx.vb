
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class IN_StockAdjust : Inherits Page
    Protected WithEvents dgStockAdj as DataGrid
    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage as Label
    Protected WithEvents lblOrderBy As Label
    Protected WithEvents lblOrderDir As Label
    Protected WithEvents lblActionResult As Label
    
    Protected WithEvents ddlSearchAdjType as DropDownList
    Protected WithEvents ddlSearchTransType as DropDownList
    Protected WithEvents ddlSearchStatus as DropDownList
    Protected WithEvents ddlPage as DropDownList
    
    Protected WithEvents txtSearchStockAdjID as TextBox
    Protected WithEvents txtSearchUpdateBy as TextBox
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected WithEvents ibNew As ImageButton
    Protected WithEvents ibPrint As ImageButton

    Dim objGLtrx As New agri.GL.ClsTrx()
    Protected objIN As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    
    Dim strOpCdStockAdjustmentHeader_ADD As String = "IN_CLSTRX_STOCKADJUST_DETAIL_ADD"
    Dim strOpCdStockAdjustmentHeader_GET As String = "IN_CLSTRX_STOCKADJUST_DETAIL_GET"
    Dim strOpCdStockAdjustmentHeader_UPD As String = "IN_CLSTRX_STOCKADJUST_DETAIL_UPD"
    Dim strOpCdStockAdjustmentLine_GET As String = "IN_CLSTRX_STOCKADJUST_LINE_GET"

    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intINAR As Integer
    Dim strSelAccMonth As String
    Dim strSelAccYear As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intINAR = Session("SS_INAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockAdjustment), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If lblOrderBy.Text = "" Then
                lblOrderBy.Text = "StockAdjID"
            End If
            If lblOrderDir.Text = "" Then
                lblOrderDir.Text = "ASC"
            End If

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            ibNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibNew).ToString())
            ibPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibPrint).ToString())

            lblActionResult.Visible = False 
            
            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                BindSearchAdjustmentTypeDropDownList()
                BindSearchTransactionDropDownList()
                BindSearchStatusDropDownList()
                BindDataGrid()
            End If
        End IF
    End Sub

    Sub BindSearchAdjustmentTypeDropDownList() 
        ddlSearchAdjType.Items.Add(New ListItem(objIN.mtdGetAdjustmentType(objIN.EnumAdjustmentType.All), objIN.EnumAdjustmentType.All))
        ddlSearchAdjType.Items.Add(New ListItem(objIN.mtdGetAdjustmentType(objIN.EnumAdjustmentType.Quantity), objIN.EnumAdjustmentType.Quantity))
        ddlSearchAdjType.Items.Add(New ListItem(objIN.mtdGetAdjustmentType(objIN.EnumAdjustmentType.AverageCost), objIN.EnumAdjustmentType.AverageCost))
        ddlSearchAdjType.Items.Add(New ListItem(objIN.mtdGetAdjustmentType(objIN.EnumAdjustmentType.TotalCost), objIN.EnumAdjustmentType.TotalCost))
        ddlSearchAdjType.Items.Add(New ListItem(objIN.mtdGetAdjustmentType(objIN.EnumAdjustmentType.QuantityAtAverageCost), objIN.EnumAdjustmentType.QuantityAtAverageCost))
        ddlSearchAdjType.Items.Add(New ListItem(objIN.mtdGetAdjustmentType(objIN.EnumAdjustmentType.QuantityAtTotalCost), objIN.EnumAdjustmentType.QuantityAtTotalCost))
        ddlSearchAdjType.SelectedIndex = 0
    End Sub 
    
    Sub BindSearchTransactionDropDownList() 
        ddlSearchTransType.Items.Add(New ListItem(objIN.mtdGetTransactionType(objIN.EnumTransactionType.All), objIN.EnumTransactionType.All))
        ddlSearchTransType.Items.Add(New ListItem(objIN.mtdGetTransactionType(objIN.EnumTransactionType.NewValue), objIN.EnumTransactionType.NewValue))
        ddlSearchTransType.Items.Add(New ListItem(objIN.mtdGetTransactionType(objIN.EnumTransactionType.Difference), objIN.EnumTransactionType.Difference))
        ddlSearchTransType.SelectedIndex = 0
    End Sub 
    
    Sub BindSearchStatusDropDownList() 
        ddlSearchStatus.Items.Add(New ListItem(objIN.mtdGetStockAdjustStatus(objIN.EnumStockAdjustStatus.All), objIN.EnumStockAdjustStatus.All))
        ddlSearchStatus.Items.Add(New ListItem(objIN.mtdGetStockAdjustStatus(objIN.EnumStockAdjustStatus.Active), objIN.EnumStockAdjustStatus.Active))
        ddlSearchStatus.Items.Add(New ListItem(objIN.mtdGetStockAdjustStatus(objIN.EnumStockAdjustStatus.Deleted), objIN.EnumStockAdjustStatus.Deleted))
        ddlSearchStatus.Items.Add(New ListItem(objIN.mtdGetStockAdjustStatus(objIN.EnumStockAdjustStatus.Confirmed), objIN.EnumStockAdjustStatus.Confirmed))
        ddlSearchStatus.SelectedIndex = 1
    End Sub 

    Sub BindPageDropDownList() 
        Dim intCnt As Integer = 1   
        Dim arrDList As New ArrayList()

        While Not intCnt = dgStockAdj.PageCount + 1
            arrDList.Add("Page " & intCnt)
            intCnt = intCnt + 1
        End While
        ddlPage.DataSource = arrDList
        ddlPage.DataBind()
        ddlPage.SelectedIndex = dgStockAdj.CurrentPageIndex
    End Sub 
    
    Sub ddlPage_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgStockAdj.CurrentPageIndex = ddlPage.SelectedIndex
            BindDataGrid()
        End If
    End Sub
    
    Protected Function GetStockAdjustmentListDS() As DataSet
        Dim dsList As DataSet
        Dim strWhere As String
        Dim strOrderBy As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strAccYear = lstAccYear.SelectedItem.Value
        strWhere = "WHERE HD.LocCode = '" & strLocation.Trim() & "' AND HD.AccMonth IN ('" & strAccMonth & "') AND HD.AccYear = '" & strAccYear.Trim() & "'"
        If txtSearchStockAdjID.Text.Trim() <> "" Then
            strWhere = strWhere & " AND HD.StockAdjID LIKE '" & Replace(txtSearchStockAdjID.Text.Trim(), "'", "''") & "'"
        End If
        If ddlSearchAdjType.SelectedItem.Value <> objIN.EnumAdjustmentType.All Then
            strWhere = strWhere & " AND HD.AdjType = '" & ddlSearchAdjType.SelectedItem.Value & "'"
        End If
        If ddlSearchTransType.SelectedItem.Value <> objIN.EnumTransactionType.All Then
            strWhere = strWhere & " AND HD.TransType = '" & ddlSearchTransType.SelectedItem.Value & "'"
        End If
        If ddlSearchStatus.SelectedItem.Value <> objIN.EnumStockAdjustStatus.All Then
            strWhere = strWhere & " AND HD.Status = '" & ddlSearchStatus.SelectedItem.Value & "'"
        End If
        If txtSearchUpdateBy.Text.Trim() <> "" Then
            strWhere = strWhere & " AND USR.UserName LIKE '" & Replace(txtSearchUpdateBy.Text.Trim(), "'", "''") & "'"
        End If

        strOrderBy = " ORDER BY " & lblOrderBy.Text.Trim() & " " & lblOrderDir.Text.Trim()
        Try
            intErrNo = objIN.mtdGetStockAdjustment(strOpCdStockAdjustmentHeader_GET, strWhere, strOrderBy, dsList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFERLIST&errmesg=" & lblErrMessage.Text & "&redirect=../EN/menu/menu_INTrx_page.aspx")
        End Try
        Return dsList
        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Function
    
    Sub BindDataGrid() 
        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        
        dsData = GetStockAdjustmentListDS()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgStockAdj.PageSize)
        
        dgStockAdj.DataSource = dsData
        If dgStockAdj.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgStockAdj.CurrentPageIndex = 0
            Else
                dgStockAdj.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgStockAdj.DataBind()
        BindPageDropDownList()
        PageNo = dgStockAdj.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgStockAdj.PageCount
    End Sub 
    
    Sub dgStockAdj_OnItemDataBound(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim lbl As Label
        Dim btn As LinkButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            lbl = e.Item.FindControl("lblStatus")
            btn = e.Item.FindControl("Delete")
            Select Case CInt(lbl.Text.Trim())
                Case objIN.EnumStockAdjustStatus.Active
                    btn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    btn.Text = "Delete"
                    btn.visible = True
                Case objIN.EnumStockAdjustStatus.Deleted
                    btn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                    btn.Text = "Undelete"
                    btn.visible = True
                Case Else
                    btn.visible = False
            End Select
        End If
    End Sub
    
     Sub btnSearch_OnClick(sender As Object, e As System.EventArgs) 
        dgStockAdj.CurrentPageIndex = 0
        dgStockAdj.EditItemIndex = -1
        BindDataGrid() 
        BindPageDropDownList()
    End Sub 
    
    Sub ibPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgStockAdj.CurrentPageIndex = 0
            Case "prev"
                dgStockAdj.CurrentPageIndex = Math.Max(0, dgStockAdj.CurrentPageIndex - 1)
            Case "next"
                dgStockAdj.CurrentPageIndex = Math.Min(dgStockAdj.PageCount - 1, dgStockAdj.CurrentPageIndex + 1)
            Case "last"
                dgStockAdj.CurrentPageIndex = dgStockAdj.PageCount - 1
        End Select
        ddlPage.SelectedIndex = dgStockAdj.CurrentPageIndex
        BindDataGrid()
    End Sub
    
    Sub ibNew_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_trx_StockAdj_Details.aspx")
    End Sub
    
    Sub dgStockAdj_OnPageIndexChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgStockAdj.CurrentPageIndex = e.NewPageIndex
        BindDataGrid()
    End Sub

    Sub dgStockAdj_OnSortCommand(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        lblOrderBy.Text = e.SortExpression.ToString()
        lblOrderDir.Text = IIf(lblOrderDir.Text = "ASC", "DESC", "ASC")
        dgStockAdj.CurrentPageIndex = ddlPage.SelectedIndex
        BindDataGrid()
    End Sub

    Sub dgStockAdj_OnDeleteCommand(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lblDG As Label
        Dim strStockAdjId As String
        Dim intStatus As Integer
        Dim intAction As Integer
        Dim intErrNum As Integer
        Dim strParam As String
        
        lblDG = E.Item.FindControl("lblStockAdjId")
        strStockAdjId = lblDG.Text.Trim()

        lblDG = E.Item.FindControl("lblStatus")
        If CInt(lblDG.Text.Trim()) = objIN.EnumStockAdjustStatus.Active Then
            intStatus = objIN.EnumStockAdjustStatus.Deleted
            intAction = objIN.EnumTransactionAction.Delete
        Else
            intStatus = objIN.EnumStockAdjustStatus.Active
            intAction = objIN.EnumTransactionAction.Undelete
        End If
        
        strParam = strStockAdjId & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & _
                   "" & vbCrLf & "" & vbCrLf & intStatus & vbCrLf & "" & vbCrLf & "" & vbCrLf & _
                   "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & ""
        Try
            intErrNo = objIN.mtdStockAdjustmentHeader_Upd(strOpCdStockAdjustmentHeader_ADD, _
                                                          strOpCdStockAdjustmentHeader_UPD, _
                                                          strOpCdStockAdjustmentLine_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          intAction, _
                                                          intErrNum, _
                                                          strStockAdjId)
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
            End If
        End Try
        
        If intErrNum <> objIN.EnumTransactionError.NoError Then
            lblActionResult.Text = "System failed to " & IIf(intAction = objIN.EnumTransactionAction.Delete, "delete", "undelete") & " the stock adjustment " & strStockAdjId & "."
            lblActionResult.Visible = True
        End If
        BindDataGrid()
    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        lstAccYear.DataSource = objAccYearDs.Tables(0)
        lstAccYear.DataValueField = "AccYear"
        lstAccYear.DataTextField = "UserName"
        lstAccYear.DataBind()
        lstAccYear.SelectedIndex = intSelectedIndex - 1
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
