Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control

Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic
Imports agri.PWSystem.clsUser
Imports agri.GlobalHdl.clsGlobalHdl

Public Class system_user_userlist : Inherits Page

    Protected WithEvents dgUserList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtUserId As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMesage As Label

    Protected objSysCfg As New agri.PWSystem.clsUser()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intADAR As Integer
    Dim objUserDs As New Object()

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADUser), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "UserID"
            End If

            If Not Page.IsPostBack Then
                BindGrid() 
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
        dgUserList.CurrentPageIndex = 0
        dgUserList.EditItemIndex = -1
        BindGrid() 
        BindPageList()
    End Sub 

    Sub BindGrid() 
        Dim PageNo as Integer 
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lblLabel As Label
        Dim lblStatus As Label

        dgUserList.DataSource = LoadData
        dgUserList.DataBind()

        For intCnt = 0 To dgUserList.Items.Count - 1
            lblLabel = dgUserList.Items.Item(intCnt).FindControl("lblUserId")
            Select Case lblLabel.Text
                Case strUserId
                    lbButton = dgUserList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                Case Else
                    lblStatus = dgUserList.Items.Item(intCnt).FindControl("lblStatus")                    
                    Select Case Trim(lblStatus.Text)
                        Case Trim(CStr(objSysCfg.EnumUserStatus.Active)), Trim(CStr(objSysCfg.EnumUserStatus.Inactive))
                            lbButton = dgUserList.Items.Item(intCnt).FindControl("lbDelete")
                            lbButton.Visible = True
                            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                        Case Else
                            lbButton = dgUserList.Items.Item(intCnt).FindControl("lbDelete")
                            lbButton.Visible = False
                    End Select
                    
            End Select
        Next

        PageNo = dgUserList.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgUserList.PageCount
    End Sub 

    Sub BindPageList() 
        Dim count as integer = 1   
        Dim arrDList As New ArrayList()

        While not count = dgUserList.PageCount + 1
            arrDList.Add("Page " & count)
            Count = Count +1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgUserList.CurrentPageIndex
    End Sub 


    Protected Function LoadData() As DataSet
        Dim strOpCode_GetUserList As String = "PWSYSTEM_CLSUSER_USERLIST_GET"
        Dim strSrchUserID as string
        Dim strSrchName as string
        Dim strSrchStatus as string
        Dim strSrchLastUpdate as string
        Dim strSearch as string
        Dim strParam as string
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchUserID = IIf(txtUserId.Text = "", "", txtUserId.Text)
        strSrchName = IIf(txtName.Text = "", "", txtName.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchUserID & "|" & _
                   strSrchName & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text

        Try
            intErrNo = objSysCfg.mtdGetUser(strOpCode_GetUserList, strParam, objUserDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERLIST_GET_USER&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objUserDs.Tables(0).Rows.Count - 1
            objUserDs.Tables(0).Rows(intCnt).Item(0) = Trim(objUserDs.Tables(0).Rows(intCnt).Item(0))
            objUserDs.Tables(0).Rows(intCnt).Item(1) = Trim(objUserDs.Tables(0).Rows(intCnt).Item(1))
            objUserDs.Tables(0).Rows(intCnt).Item(2) = Trim(objUserDs.Tables(0).Rows(intCnt).Item(2))
            objUserDs.Tables(0).Rows(intCnt).Item(3) = Trim(objUserDs.Tables(0).Rows(intCnt).Item(3))
            objUserDs.Tables(0).Rows(intCnt).Item(4) = Trim(objUserDs.Tables(0).Rows(intCnt).Item(4))
        Next                

        Return objUserDs
    End Function

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                dgUserList.CurrentPageIndex = 0
            Case "prev"
                dgUserList.CurrentPageIndex = _
                Math.Max(0, dgUserList.CurrentPageIndex - 1)
            Case "next"
                dgUserList.CurrentPageIndex = _
                Math.Min(dgUserList.PageCount - 1, dgUserList.CurrentPageIndex + 1)
            Case "last"
                dgUserList.CurrentPageIndex = dgUserList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgUserList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            dgUserList.CurrentPageIndex = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        dgUserList.CurrentPageIndex=e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgUserList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub
 
    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim strOpCode_UpdUserDet As String = "PWSYSTEM_CLSUSER_USERDETAILS_UPD"
        Dim strUserParam As String = ""
        Dim UserIdCell As TableCell = E.Item.Cells(0)
        Dim strSelectedUserId As String
        Dim intErrNo As Integer

        strSelectedUserId = UserIdCell.Text

        If strSelectedUserId <> strUserId Then
            strUserParam = strSelectedUserId & "||||||" & objSysCfg.EnumUserStatus.Deleted & "|"
            Try
                intErrNo = objSysCfg.mtdUpdUser(strOpCode_UpdUserDet, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strUserParam, _
                                                objSysCfg.EnumUserUpdType.Update)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=USERLIST_UPD_USER&errmesg=" & lblErrMesage.Text & "&redirect=")
            End Try

            dgUserList.EditItemIndex = -1
        End If

        BindGrid()
    End Sub

    Sub NewUserBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("userdet.aspx")
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
