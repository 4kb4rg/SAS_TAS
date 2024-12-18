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

Public Class System_user_DailyControl : Inherits Page

    Protected WithEvents dgUserList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents ddlUser As DropDownList

    Protected WithEvents lblErrMessage As Label

    Protected objSysCfg As New agri.PWSystem.clsUser()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim ObjOk As New agri.GL.ClsTrx()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intADAR As Integer
    Dim objUserDs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
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
                BindUserLevel()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgUserList.CurrentPageIndex = 0
        dgUserList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        'Dim PageNo As Integer
        'Dim intCnt As Integer
        'Dim lbButton As LinkButton
        'Dim lblLabel As Label
        'Dim lblStatus As Label

        dgUserList.DataSource = LoadData()
        dgUserList.DataBind()

        'For intCnt = 0 To dgUserList.Items.Count - 1
        '    lblLabel = dgUserList.Items.Item(intCnt).FindControl("lblUserId")
        '    Select Case lblLabel.Text
        '        Case strUserId
        '            lbButton = dgUserList.Items.Item(intCnt).FindControl("lbDelete")
        '            lbButton.Visible = False
        '        Case Else
        '            lblStatus = dgUserList.Items.Item(intCnt).FindControl("lblStatus")
        '            Select Case Trim(lblStatus.Text)
        '                Case Trim(CStr(objSysCfg.EnumUserStatus.Active)), Trim(CStr(objSysCfg.EnumUserStatus.Inactive))
        '                    lbButton = dgUserList.Items.Item(intCnt).FindControl("lbDelete")
        '                    lbButton.Visible = True
        '                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        '                Case Else
        '                    lbButton = dgUserList.Items.Item(intCnt).FindControl("lbDelete")
        '                    lbButton.Visible = False
        '            End Select

        '    End Select
        'Next

        'PageNo = dgUserList.CurrentPageIndex + 1
        'lblTracker.Text = "Page " & PageNo & " of " & dgUserList.PageCount
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgUserList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgUserList.CurrentPageIndex
    End Sub

    Sub BindUserLevel()
        ddlUser.Items.Clear()
        ddlUser.Items.Add("ALL")
        ddlUser.Items.Add("User")
        ddlUser.Items.Add("Supervisor")
        ddlUser.Items.Add("Manager")
        ddlUser.Items.Add("General Manager")
        ddlUser.Items.Add("VP/CEO")
    End Sub

    Function GetLevelNumberUser(ByVal pLeveName As String) As Integer
        Dim nValLevel As Integer
        Select Case pLeveName
            Case "User"
                nValLevel = 0
            Case "Supervisor"
                nValLevel = 1
            Case "Manager"
                nValLevel = 2
            Case "General Manager"
                nValLevel = 3
            Case "VP/CEO"
                nValLevel = 4
        End Select
        Return nValLevel
    End Function

    Private Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception
        End Try
    End Sub

    Protected Function LoadData() As DataSet

        Dim strOpCode_GetUserList As String = "PWSYSTEM_CLSUSER_USERDAILY_CONTROL_GET"
        Dim objDetail As New Object()
        Dim strSrchUserID As String
        Dim strSrchName As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim nLevelValue As Integer

        nLevelValue = GetLevelNumberUser(ddlUser.SelectedItem.Text)

        strParamName = "USRLEVEL"
        strParamValue = IIf(ddlUser.SelectedItem.Value = "ALL", "ALL", GetLevelNumberUser(ddlUser.SelectedItem.Value))

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCode_GetUserList, strParamName, strParamValue, objDetail)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_GET_BY_VEHCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        For intCnt = 0 To objDetail.Tables(0).Rows.Count - 1
            objDetail.Tables(0).Rows(intCnt).Item(0) = Trim(objDetail.Tables(0).Rows(intCnt).Item(0))
            objDetail.Tables(0).Rows(intCnt).Item(1) = Trim(objDetail.Tables(0).Rows(intCnt).Item(1))
            objDetail.Tables(0).Rows(intCnt).Item(2) = Trim(objDetail.Tables(0).Rows(intCnt).Item(2))
            objDetail.Tables(0).Rows(intCnt).Item(3) = Trim(objDetail.Tables(0).Rows(intCnt).Item(3))
            objDetail.Tables(0).Rows(intCnt).Item(4) = Trim(objDetail.Tables(0).Rows(intCnt).Item(4))
        Next

        Return objDetail
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
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

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgUserList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgUserList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgUserList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label
        dgUserList.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblLocCode")
        Session("USERLOC") = lbl.Text

        lbl = E.Item.FindControl("lblUserLevel")
        Session("USERLEVEL") = lbl.Text
        Response.Redirect("userDailyControl_Det.aspx")

    End Sub

    Sub NewUserBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Session("USERLEVEL") = vbNullString
        Session("USERLOC") = vbNullString
        Response.Redirect("userDailyControl_Det.aspx")
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
