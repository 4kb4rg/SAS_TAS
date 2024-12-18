Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports agri.PR
Imports agri.GlobalHdl.clsGlobalHdl

Public Class PR_Setup_ADList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtADCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected objPRSetup As New agri.PR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Dim objADDs As New Object()

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAllowanceDeduction), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
           If SortExpression.Text = "" Then
                SortExpression.Text = "AD.ADCode"
            End If
            If Not Page.IsPostBack Then
                BindGrid() 
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
        BindGrid() 
        BindPageList()
    End Sub 

    Sub BindGrid() 
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label


        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLine.PageSize)
        
        dgLine.DataSource = dsData
        If dgLine.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLine.CurrentPageIndex = 0
            Else
                dgLine.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgLine.DataBind()
        BindPageList()
        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgLine.PageCount

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objPRSetup.EnumADStatus.Active
                        lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPRSetup.EnumADStatus.Deleted
                        lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
            End Select
        Next

    End Sub 

    Sub BindPageList() 
        Dim count As Integer = 1   
        Dim arrDList As New ArrayList()

        While not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub 

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "PR_CLSSETUP_AD_SEARCH"
        Dim strSrchADCode As String
        Dim strSrchDesc As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchADCode = IIF(txtADCode.Text = "", "", txtADCode.Text)
        strSrchDesc = IIF(txtDescription.Text = "", "", txtDescription.Text)
        strSrchStatus = IIF(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIF(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchADCode & "|" & _
                   strSrchDesc & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|"
        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCd_GET, strParam, objADDs, False)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objADDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objADDs.Tables(0).Rows.Count - 1
                objADDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(objADDs.Tables(0).Rows(intCnt).Item("ADCode"))
                objADDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objADDs.Tables(0).Rows(intCnt).Item("Description"))
                objADDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objADDs.Tables(0).Rows(intCnt).Item("Status"))
                objADDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objADDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objADDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objADDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objADDs
    End Function

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                dgLine.CurrentPageIndex = 0
            Case "prev"
                dgLine.CurrentPageIndex = _
                    Math.Max(0, dgLine.CurrentPageIndex - 1)
            Case "next"
                dgLine.CurrentPageIndex = _
                    Math.Min(dgLine.PageCount - 1, dgLine.CurrentPageIndex + 1)
            Case "last"
                dgLine.CurrentPageIndex = dgLine.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIF(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub

    Sub DEDR_Delete(Sender As Object, e As DataGridCommandEventArgs)
        Dim strOpCd As String = "PR_CLSSETUP_AD_STATUS_UPD"
        Dim strParam As String = ""
        Dim CPCell As TableCell = e.Item.Cells(0)
        Dim strSelectedADCode As String 
        Dim intErrNo As Integer

        strSelectedADCode = CPCell.Text
        strParam = strSelectedADCode & "|" & objPRSetup.EnumADStatus.Deleted
        Try
            intErrNo = objPRSetup.mtdUpdAD(strOpCd, _
                                           strCompany, _
                                           strLocation, _
                                           strUserId, _
                                           strParam, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/PR_Setup_ADList.aspx")
        End Try
      
        dgLine.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub NewADBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_Setup_ADDet.aspx")
    End Sub

End Class
