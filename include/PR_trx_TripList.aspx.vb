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

Public Class PR_trx_TripList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtTripID As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtEmpId As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected objPRTrx As New agri.PR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Dim objTripDs As New Object()

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxTrip), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
           If SortExpression.Text = "" Then
                SortExpression.Text = "T.TripID"
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
            lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
            lbButton.Visible = False
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus") 
           	Select Case CInt(Trim(lbl.Text)) 
                Case objPRTrx.EnumTripStatus.Active
                        lbButton.Visible = True
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
        Dim strOpCd_GET As String = "PR_CLSTRX_TRIP_SEARCH"
        Dim strSrchTripID As String
        Dim strSrchDesc As String
        Dim strSrchEmp As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchTripID = IIF(txtTripID.Text = "", "", txtTripID.Text)
        strSrchDesc = IIF(txtDesc.Text = "", "", txtDesc.Text)
        strSrchEmp = IIF(txtEmpId.Text = "", "", txtEmpId.Text)
        strSrchStatus = IIF(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIF(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchTripID & "|" & _
                   strSrchDesc & "|" & _
                   strSrchEmp & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|"

        Try
            intErrNo = objPRTrx.mtdGetTrip(strOpCd_GET, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strParam, _
                                        objTripDs, _
                                        False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        Return objTripDs
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
        Dim strOpCd As String = "PR_CLSTRX_TRIP_STATUS_UPD"
        Dim strTripID As String
        Dim strParam As String = ""
        Dim CPCell As TableCell = e.Item.Cells(0)
        Dim strSelectedTripId As String 
        Dim intErrNo As Integer

        strSelectedTripId = CPCell.Text
        strParam = strSelectedTripId & "|" & objPRTrx.EnumTripStatus.Cancelled
        
        Try
            intErrNo = objPRTrx.mtdUpdTripStatus(strOpCd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True, _
                                                strTripID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_TRIP_CANCEL&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/PR_trx_TripList.aspx")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub NewTripBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_trx_TripDet.aspx")
    End Sub



End Class
