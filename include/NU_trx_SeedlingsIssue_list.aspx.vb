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

Public Class NU_trx_SeedlingsIssueList : Inherits Page
    Protected WithEvents dgTxList as DataGrid
    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage as Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblOrderBy As Label
    Protected WithEvents lblOrderDir As Label
    Protected WithEvents lblActionResult As Label
    Protected WithEvents lblNurseryBlockTag As Label

    Protected WithEvents txtSearchIssueID As TextBox
    Protected WithEvents txtSearchDocRefNo As TextBox
    Protected WithEvents txtSearchBlkCode As TextBox
    Protected WithEvents txtSearchUpdateBy As TextBox
    
    Protected WithEvents ddlSearchStatus As DropDownList
    Protected WithEvents ddlPage as DropDownList
    
    Protected objNUTrx As New agri.NU.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    
    Dim strOpCdSeedlingsIssue_ADD As String = "NU_CLSTRX_SEEDLINGS_ISSUE_ADD"
    Dim strOpCdSeedlingsIssue_GET As String = "NU_CLSTRX_SEEDLINGS_ISSUE_GET"
    Dim strOpCdSeedlingsIssue_UPD As String = "NU_CLSTRX_SEEDLINGS_ISSUE_UPD"
    Dim strOpCdSeedlingsIssueLine_ADD As String = "NU_CLSTRX_SEEDLINGS_ISSUE_LINE_ADD"
    Dim strOpCdSeedlingsIssueLine_GET As String = "NU_CLSTRX_SEEDLINGS_ISSUE_LINE_GET"
    Dim strOpCdSeedlingsIssueLine_UPD As String = "NU_CLSTRX_SEEDLINGS_ISSUE_LINE_UPD"
    Dim strOpCdSeedlingsIssueLine_DEL As String = "NU_CLSTRX_SEEDLINGS_ISSUE_LINE_DEL"
    
    Dim objLangCapDs As New DataSet()
    
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intConfigsetting As Integer
    Dim intNUAR As Integer
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_NUACCMONTH")
        strAccYear = Session("SS_NUACCYEAR")
        intNUAR = Session("SS_NUAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedIssue), intNUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            objLangCapDs = GetLanguageCaptionDS()
            
            If lblOrderBy.Text = "" Then
                lblOrderBy.Text = "SI.IssueID"
            End If
            If lblOrderDir.Text = "" Then
                lblOrderDir.Text = "ASC"
            End If
            
            lblActionResult.Visible = False 
            
            If Not Page.IsPostBack Then
                GetLangCap()
                BindSearchStatusDropDownList()
                BindDataGrid()
            End If
        End IF
    End Sub
    
    Sub GetLangCap()
        Dim strBlkCode As String
        
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlkCode = GetCaption(objLangCap.EnumLangCap.NurseryBlock)
            Else
                strBlkCode = GetCaption(objLangCap.EnumLangCap.NurserySubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_GET_LANGCAP_COSTLEVEL&errmesg=&redirect=")
        End Try
        
        lblNurseryBlockTag.Text = strBlkCode & lblCode.Text

        dgTxList.Columns(3).HeaderText = strBlkCode & lblCode.Text
    End Sub

    Function GetCaption(ByVal pv_TermCode As String) As String
        Dim I As Integer

        For I = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(I).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(I).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(I).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    Sub BindSearchStatusDropDownList() 
        ddlSearchStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedlingsIssueStatus(objNUTrx.EnumSeedlingsIssueStatus.All), objNUTrx.EnumSeedlingsIssueStatus.All))
        ddlSearchStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedlingsIssueStatus(objNUTrx.EnumSeedlingsIssueStatus.Active), objNUTrx.EnumSeedlingsIssueStatus.Active))
        ddlSearchStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedlingsIssueStatus(objNUTrx.EnumSeedlingsIssueStatus.Deleted), objNUTrx.EnumSeedlingsIssueStatus.Deleted))
        ddlSearchStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedlingsIssueStatus(objNUTrx.EnumSeedlingsIssueStatus.Confirmed), objNUTrx.EnumSeedlingsIssueStatus.Confirmed))
        ddlSearchStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedlingsIssueStatus(objNUTrx.EnumSeedlingsIssueStatus.Closed), objNUTrx.EnumSeedlingsIssueStatus.Closed))
        ddlSearchStatus.SelectedIndex = 1
    End Sub 

    Sub BindPageDropDownList() 
        Dim intCnt As Integer = 1   
        Dim arrDList As New ArrayList()

        While Not intCnt = dgTxList.PageCount + 1
            arrDList.Add("Page " & intCnt)
            intCnt = intCnt + 1
        End While
        ddlPage.DataSource = arrDList
        ddlPage.DataBind()
        ddlPage.SelectedIndex = dgTxList.CurrentPageIndex
    End Sub 
    
    Sub ddlPage_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgTxList.CurrentPageIndex = ddlPage.SelectedIndex
            BindDataGrid()
        End If
    End Sub
    
    Function GetLanguageCaptionDS() As DataSet
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim dsLC As DataSet
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
                                                 dsLC, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_GET_LANGCAP&errmesg=&redirect=")
        End Try
        
        Return dsLC
        If Not dsLC Is Nothing Then
            dsLC = Nothing
        End If
    End Function
    
    Protected Function GetSeedingIssueListDS() As DataSet
        Dim dsList As DataSet
        Dim strSelect As String
        Dim strFrom As String
        Dim strWhere As String
        Dim strOrderBy As String

        strSelect = ", COALESCE(USR.UserName, '') AS UserName"
        strFrom = " LEFT OUTER JOIN SH_USER USR ON USR.UserID = SI.UpdateID "
        
        strWhere = "SI.LocCode = '" & strLocation.Trim() & "' AND SI.AccMonth = '" & strAccMonth.Trim() & "' AND SI.AccYear = '" & strAccYear.Trim() & "'"
        If txtSearchIssueID.Text.Trim() <> "" Then
            strWhere = strWhere & " AND SI.IssueID LIKE '" & Replace(txtSearchIssueID.Text.Trim(), "'", "''") & "'"
        End If
        If txtSearchDocRefNo.Text.Trim() <> "" Then
            strWhere = strWhere & " AND SI.DocRefNo LIKE '" & Replace(txtSearchDocRefNo.Text.Trim(), "'", "''") & "'"
        End If
        If txtSearchBlkCode.Text.Trim() <> "" Then
            strWhere = strWhere & " AND SI.BlkCode LIKE '" & Replace(txtSearchBlkCode.Text.Trim(), "'", "''") & "'"
        End If
        If ddlSearchStatus.SelectedItem.Value <> objNUTrx.EnumSeedlingsIssueStatus.All Then
            strWhere = strWhere & " AND SI.Status = '" & ddlSearchStatus.SelectedItem.Value & "'"
        End If
        If txtSearchUpdateBy.Text.Trim() <> "" Then
            strWhere = strWhere & " AND USR.UserName LIKE '" & Replace(txtSearchUpdateBy.Text.Trim(), "'", "''") & "'"
        End If
        
        strOrderBy = lblOrderBy.Text.Trim() & " " & lblOrderDir.Text.Trim()
        Try
            intErrNo = objNUTrx.mtdGetSeedlingsIssue(strOpCdSeedlingsIssue_GET, strSelect, strFrom, strWhere, strOrderBy, dsList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        Return dsList
    End Function
    
    Sub BindDataGrid() 
        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        
        dsData = GetSeedingIssueListDS()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTxList.PageSize)
        
        dgTxList.DataSource = dsData
        If dgTxList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgTxList.CurrentPageIndex = 0
            Else
                dgTxList.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgTxList.DataBind()
        BindPageDropDownList()
        PageNo = dgTxList.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgTxList.PageCount
    End Sub 
    
    Sub dgTxList_OnItemDataBound(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim lbl As Label
        Dim btn As LinkButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            lbl = e.Item.FindControl("lblStatusCode")
            btn = e.Item.FindControl("lbDelete")
            Select Case CInt(lbl.Text.Trim())
                Case objNUTrx.EnumSeedlingsIssueStatus.Active
                    btn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    btn.Text = "Delete"
                    btn.visible = True
                Case objNUTrx.EnumSeedlingsIssueStatus.Deleted
                    btn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                    btn.Text = "Undelete"
                    btn.visible = True
                Case Else
                    btn.visible = False
            End Select
        End If
    End Sub
    
     Sub btnSearch_OnClick(sender As Object, e As System.EventArgs) 
        dgTxList.CurrentPageIndex = 0
        dgTxList.EditItemIndex = -1
        BindDataGrid() 
        BindPageDropDownList()
    End Sub 
    
    Sub ibPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgTxList.CurrentPageIndex = 0
            Case "prev"
                dgTxList.CurrentPageIndex = Math.Max(0, dgTxList.CurrentPageIndex - 1)
            Case "next"
                dgTxList.CurrentPageIndex = Math.Min(dgTxList.PageCount - 1, dgTxList.CurrentPageIndex + 1)
            Case "last"
                dgTxList.CurrentPageIndex = dgTxList.PageCount - 1
        End Select
        ddlPage.SelectedIndex = dgTxList.CurrentPageIndex
        BindDataGrid()
    End Sub
    
    Sub ibNew_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("NU_trx_SeedlingsIssue_details.aspx")
    End Sub
    
    Sub dgTxList_OnPageIndexChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgTxList.CurrentPageIndex = e.NewPageIndex
        BindDataGrid()
    End Sub

    Sub dgTxList_OnSortCommand(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        lblOrderBy.Text = e.SortExpression.ToString()
        lblOrderDir.Text = IIf(lblOrderDir.Text = "ASC", "DESC", "ASC")
        dgTxList.CurrentPageIndex = ddlPage.SelectedIndex
        BindDataGrid()
    End Sub

    Sub dgTxList_OnDeleteCommand(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lblDG As Label
        Dim strIssueID As String
        Dim intStatus As Integer
        Dim intErrNum As Integer
        Dim intAction As Integer
        Dim strParam As String
        
        lblDG = E.Item.FindControl("lblIssueID")
        strIssueID = lblDG.Text.Trim()

        lblDG = E.Item.FindControl("lblStatusCode")
        If CInt(lblDG.Text.Trim()) = objNUTrx.EnumSeedlingsIssueStatus.Active Then
            intStatus = objNUTrx.EnumSeedlingsIssueStatus.Deleted
            intAction = 1
        Else
            intStatus = objNUTrx.EnumSeedlingsIssueStatus.Active
            intAction = 2
        End If
        
        strParam = strIssueID & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & _
                   "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & intStatus & vbCrLf & "" 
        Try
            intErrNo = objNUTrx.mtdUpdSeedlingsIssue(strOpCdSeedlingsIssue_ADD, _
                                                     strOpCdSeedlingsIssue_UPD, _
                                                     strOpCdSeedlingsIssue_GET, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     strParam, _
                                                     objNUTrx.EnumOperation.Update, _
                                                     strIssueID)
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_UPDATE&errmesg=&redirect=")
            End If
        End Try
        
        If intErrNum <> objNUTrx.EnumErrorType.NoError Then
            lblActionResult.Text = "System failed to " & IIf(intAction = 1, "delete", "undelete") & " the Seedlings issue " & strIssueID & "."
            lblActionResult.Visible = True
        End If
        BindDataGrid()
    End Sub

End Class
