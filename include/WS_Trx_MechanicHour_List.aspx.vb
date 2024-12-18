
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Text
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class WS_TRX_MECHANIC_HOUR : Inherits Page

    Protected WithEvents dgMechanicHour As DataGrid
    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblOrderBy As Label
    Protected WithEvents lblOrderDir As Label
    Protected WithEvents lblActionResult As Label
    Protected Withevents lblSearchWorkingDateErr As Label
    
    Protected WithEvents ddlPage As DropDownList
    Protected WithEvents ddlSearchStatus As DropDownList
    
    Protected WithEvents txtSearchWorkingDate As TextBox
    Protected WithEvents txtSearchEmpCode As TextBox
    Protected WithEvents txtSearchEmpName As TextBox
    Protected WithEvents txtSearchUpdateBy As TextBox
    
    Protected objWSTrx As New agri.WS.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    
    Dim strOpCode_Mechanic_Hour_Get As String = "WS_CLSTRX_MECHANIC_HOUR_GET"
    Dim strOpCode_Mechanic_Hour_Upd As String = "WS_CLSTRX_MECHANIC_HOUR_UPD"
    
    Dim intErrNo As Integer
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    
    Dim dsLangCap As New DataSet()
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
		
	        strLocType = Session("SS_LOCTYPE")
	        

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMechanicHour), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If lblOrderBy.Text = "" Then
                lblOrderBy.Text = "MechHourID"
            End If
            If lblOrderDir.Text = "" Then
                lblOrderDir.Text = "ASC"
            End If
            
            lblActionResult.Visible = False 
            
            If Not Page.IsPostBack Then
                BindSearchStatusDropDownList()
                BindDataGrid()
            End If
        End IF
    End Sub
    
    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To dsLangCap.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(dsLangCap.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(dsLangCap.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(dsLangCap.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function

    Sub BindSearchStatusDropDownList() 
        ddlSearchStatus.Items.Clear
        ddlSearchStatus.Items.Add(New ListItem(objWSTrx.mtdGetMechanicHourStatus(objWSTrx.EnumMechanicHourStatus.All), objWSTrx.EnumMechanicHourStatus.All))
        ddlSearchStatus.Items.Add(New ListItem(objWSTrx.mtdGetMechanicHourStatus(objWSTrx.EnumMechanicHourStatus.Active), objWSTrx.EnumMechanicHourStatus.Active))
        ddlSearchStatus.Items.Add(New ListItem(objWSTrx.mtdGetMechanicHourStatus(objWSTrx.EnumMechanicHourStatus.Deleted), objWSTrx.EnumMechanicHourStatus.Deleted))
        ddlSearchStatus.Items.Add(New ListItem(objWSTrx.mtdGetMechanicHourStatus(objWSTrx.EnumMechanicHourStatus.Closed), objWSTrx.EnumMechanicHourStatus.Closed))
        ddlSearchStatus.SelectedIndex = 1
    End Sub 

    Sub BindPageDropDownList() 
        Dim intCnt As Integer = 1   
        Dim arrPageNo As New ArrayList()

        While Not intCnt = dgMechanicHour.PageCount + 1
            arrPageNo.Add("Page " & intCnt)
            intCnt = intCnt + 1
        End While
        ddlPage.DataSource = arrPageNo
        ddlPage.DataBind()
        ddlPage.SelectedIndex = dgMechanicHour.CurrentPageIndex
    End Sub 
    
    Sub ddlPage_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgMechanicHour.CurrentPageIndex = ddlPage.SelectedIndex
            BindDataGrid()
        End If
    End Sub
    
    Protected Function GetMechanicHourListDS() As DataSet
        Dim colParam As New Collection
        Dim dsTemp As New DataSet
        Dim sbTemp As New StringBuilder
        Dim strErrMsg As String
        Dim strWorkingDate As String

        sbTemp.Append("WHERE MH.LocCode = '" & FixSQL(strLocation) & "' " & vbCrLf)
        sbTemp.Append("  AND MH.AccMonth = '" & FixSQL(strAccMonth) & "' " & vbCrLf)
        sbTemp.Append("  AND MH.AccYear = '" & FixSQL(strAccYear) & "' " & vbCrLf)
        
        If Trim(txtSearchWorkingDate.Text) <> "" Then
            strWorkingDate = GetValidDate(Trim(txtSearchWorkingDate.Text), strErrMsg)
            If strWorkingDate <> "" Then
                sbTemp.Append("  AND MH.WorkingDate = '" & FixSQL(strWorkingDate) & "' " & vbCrLf)
                lblSearchWorkingDateErr.Text = ""
            Else
                lblSearchWorkingDateErr.Text = strErrMsg
            End If
        Else
            lblSearchWorkingDateErr.Text = ""
        End If
        
        If Trim(txtSearchEmpCode.Text) <> "" Then
            sbTemp.Append("  AND MH.EmpCode LIKE '" & FixSQL(Trim(txtSearchEmpCode.Text)) & "' " & vbCrLf)
        End If
        
        If Trim(txtSearchEmpName.Text) <> "" Then
            sbTemp.Append("  AND EMP.EmpName LIKE '" & FixSQL(Trim(txtSearchEmpName.Text)) & "' " & vbCrLf)
        End If
        
        If GetDropDownListValue(ddlSearchStatus) <> objWSTrx.EnumMechanicHourStatus.All Then
            sbTemp.Append("  AND MH.Status = '" & FixSQL(GetDropDownListValue(ddlSearchStatus)) & "' " & vbCrLf)
        End If
        
        If Trim(txtSearchUpdateBy.Text) <> "" Then
            sbTemp.Append("  AND USR.UserName = '" & FixSQL(Trim(txtSearchUpdateBy.Text)) & "' " & vbCrLf)
        End If
        
        sbTemp.Append("ORDER BY " & Trim(lblOrderBy.Text) & " " & Trim(lblOrderDir.Text))
        
        colParam.Add(sbTemp.ToString, "PM_SEARCH")
        colParam.Add(strOpCode_Mechanic_Hour_Get, "OC_MECHANIC_HOUR_GET")
        
        Try
            intErrNo = objWSTrx.mtdMechanicHour_Get(colParam, dsTemp, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If
            Return dsTemp
        Catch ex As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_LIST_GET_MECHANIC_HOUR_LIST&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=")
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
    End Function
        
    Sub BindDataGrid() 
        Dim intPageNo As Integer 
        Dim intPageCount As Integer
        Dim dsData As DataSet
        
        dsData = GetMechanicHourListDS()
        intPageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgMechanicHour.PageSize)
        
        dgMechanicHour.DataSource = dsData
        If dgMechanicHour.CurrentPageIndex >= intPageCount Then
            If intPageCount = 0 Then
                dgMechanicHour.CurrentPageIndex = 0
            Else
                dgMechanicHour.CurrentPageIndex = intPageCount - 1
            End If
        End If
        
        dgMechanicHour.DataBind()
        BindPageDropDownList()
        intPageNo = dgMechanicHour.CurrentPageIndex + 1
        lblTracker.Text="Page " & intPageNo & " of " & dgMechanicHour.PageCount
    End Sub 
    
    Sub dgMechanicHour_OnItemDataBound(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim lblMechHourID As Label
        Dim lblWorkingDate As Label
        Dim lblStatus As Label
        Dim lblDeleteDependency As Label
        Dim btnDelete As LinkButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            lblWorkingDate = e.Item.FindControl("lblWorkingDate")
            lblMechHourID = e.Item.FindControl("lblMechHourID")
            e.Item.Cells(0).Text = "<a href=""ws_trx_mechanichour_detail.aspx?mechhourid=" & Trim(lblMechHourID.Text) & """>" & objGlobal.GetLongDate(lblWorkingDate.Text) & "</a>"
            
            lblStatus = e.Item.FindControl("lblStatus")
            lblDeleteDependency = e.Item.FindControl("lblDeleteDependency")
            btnDelete = e.Item.FindControl("lbDelete")
            
            Select Case Trim(lblStatus.Text)
                Case Trim(CStr(objWSTrx.EnumMechanicHourStatus.Active))
                    If CInt(Trim(lblDeleteDependency.Text)) <> 0 Then
                        btnDelete.Visible = False
                    Else
                        btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                        btnDelete.Text = "Delete"
                        btnDelete.Visible = True
                    End If
                Case Trim(CStr(objWSTrx.EnumMechanicHourStatus.Deleted))
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                    btnDelete.Text = "Undelete"
                    btnDelete.Visible = True
                Case Else
                    btnDelete.Visible = False
            End Select
        End If
    End Sub
    
     Sub btnSearch_OnClick(sender As Object, e As System.EventArgs) 
        dgMechanicHour.CurrentPageIndex = 0
        dgMechanicHour.EditItemIndex = -1
        BindDataGrid() 
        BindPageDropDownList()
    End Sub 
    
    Sub ibPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgMechanicHour.CurrentPageIndex = 0
            Case "prev"
                dgMechanicHour.CurrentPageIndex = Math.Max(0, dgMechanicHour.CurrentPageIndex - 1)
            Case "next"
                dgMechanicHour.CurrentPageIndex = Math.Min(dgMechanicHour.PageCount - 1, dgMechanicHour.CurrentPageIndex + 1)
            Case "last"
                dgMechanicHour.CurrentPageIndex = dgMechanicHour.PageCount - 1
        End Select
        ddlPage.SelectedIndex = dgMechanicHour.CurrentPageIndex
        BindDataGrid()
    End Sub
    
    Sub ibNew_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("ws_trx_mechanichour_detail.aspx")
    End Sub
    
    Sub dgMechanicHour_OnPageIndexChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgMechanicHour.CurrentPageIndex = e.NewPageIndex
        BindDataGrid()
    End Sub

    Sub dgMechanicHour_OnSortCommand(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        lblOrderBy.Text = e.SortExpression.ToString()
        lblOrderDir.Text = IIf(lblOrderDir.Text = "ASC", "DESC", "ASC")
        dgMechanicHour.CurrentPageIndex = ddlPage.SelectedIndex
        BindDataGrid()
    End Sub

    Sub dgMechanicHour_OnDeleteCommand(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim colParam As New Collection
        Dim lblTemp As Label
        Dim strErrMsg As String
        
        lblTemp = E.Item.FindControl("lblMechHourID")
        colParam.Add(Trim(lblTemp.Text), "PM_MECHHOURID")
        
        lblTemp = E.Item.FindControl("lblStatus")
        colParam.Add(strUserId, "PM_UPDATEID")
        colParam.Add(strOpCode_Mechanic_Hour_Get, "OC_MECHANIC_HOUR_GET")
        colParam.Add(strOpCode_Mechanic_Hour_Upd, "OC_MECHANIC_HOUR_UPD")
        Try
            If Trim(lblTemp.Text) = Trim(CStr(objWSTrx.EnumMechanicHourStatus.Active)) Then
                intErrNo = objWSTrx.mtdMechanicHour_Delete(colParam, strErrMsg)
            Else
                intErrNo = objWSTrx.mtdMechanicHour_Undelete(colParam, strErrMsg)
            End If
            
            If intErrNo <> objWSTrx.EnumException.NoError Then
                lblActionResult.Text = "Cannot perform the operation. " & strErrMsg
                lblActionResult.Visible = True
            End If
        Catch ex As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_LIST_DELETE_UNDELETE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=")
        End Try
        BindDataGrid()
    End Sub
    
    Protected Function FixSQL(ByVal pv_strParam As String) As String
        Return Replace(Trim(pv_strParam), "'", "''")
    End Function
    
    Protected Function RoundNumber(ByVal d As Double, Optional ByVal decimals As Double = 0) As Decimal
        RoundNumber = Decimal.Divide(Int(Decimal.Add(Decimal.Multiply(d, 10.0 ^ decimals), 0.5)), 10.0 ^ decimals)
    End Function
    
    Protected Function GetDropDownListValue(ByRef pr_ddlObject As DropDownList) As String
        If Trim(Request.Form(pr_ddlObject.ID)) <> "" Then
            GetDropDownListValue = Trim(Request.Form(pr_ddlObject.ID))
        Else
            GetDropDownListValue = pr_ddlObject.SelectedItem.Value
        End If
    End Function
    
    Protected Function GetValidDate(ByVal pv_strInputDate As String, ByRef pr_strErrMsg As String) As String
        Dim strDateFormat As String
        Dim strSQLDate As String

        If objGlobal.mtdValidInputDate(Session("SS_DATEFMT"), _
                                       pv_strInputDate, _
                                       strDateFormat, _
                                       strSQLDate) = True Then
            GetValidDate = strSQLDate
            pr_strErrMsg = ""
        Else
            GetValidDate = ""
            pr_strErrMsg = "Date format should be in " & strDateFormat
        End If
    End Function

End Class
