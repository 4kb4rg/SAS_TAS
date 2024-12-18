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


Public Class PR_trx_PieceRatePayList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtPieceRateId As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label

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

    Dim objPieceRateDs As New Object()

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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxRatePayment), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
           If SortExpression.Text = "" Then
                SortExpression.Text = "PAY.PieceRateID"
            End If
            If Not Page.IsPostBack Then
                BindGrid() 
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
        lblCurrentIndex.Text = 0
        dgLine.EditItemIndex = -1
        BindGrid() 
        BindPageList()
    End Sub 

    Sub BindGrid() 
        Dim PageNo As Integer 
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim dsTemp As New DataSet()
        Dim lbl As Label


        Dim PageCount As Integer
        
        dsTemp = LoadData
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgLine.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsTemp = LoadData
            End If
        End If
        
        dgLine.DataSource = dsTemp
        dgLine.DataBind()
        lblPageCount.Text = PageCount
        BindPageList()
        PageNo = lblCurrentIndex.Text + 1
        lblTracker.Text="Page " & pageno & " of " & PageCount

        For intCnt = 0 To dgLine.Items.Count - 1
            lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
            lbButton.Visible = False
            lbl = dgLine.Items.Item(intCnt).FindControl("lblAllowCancel")
            If lbl.Text = "yes" Then
                lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus") 
           	    Select Case CInt(Trim(lbl.Text)) 
                    Case objPRTrx.EnumPieceRateStatus.Active
                            lbButton.Visible = True
                End Select
            End If
        Next

    End Sub 

    Sub BindPageList() 
        Dim count As Integer = 1   
        Dim arrDList As New ArrayList()

        While not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub 

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "PR_CLSTRX_PIECERATEPAY_SEARCH"
        Dim strSrchPieceRateId As String
        Dim strSrchDesc As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Session("SS_PAGING") = lblCurrentIndex.Text

        strSrchPieceRateId = IIF(txtPieceRateId.Text = "", "", txtPieceRateId.Text)
        strSrchDesc = IIF(txtDescription.Text = "", "", txtDescription.Text)
        strSrchStatus = IIF(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIF(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchPieceRateId & "|" & _
                   strSrchDesc & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text

        Try
            intErrNo = objPRTrx.mtdGetPieceRatePay(strOpCd_GET, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strParam, _
                                                   objPieceRateDs, _
                                                   False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objPieceRateDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objPieceRateDs.Tables(0).Rows.Count - 1
                objPieceRateDs.Tables(0).Rows(intCnt).Item("PieceRateID") = Trim(objPieceRateDs.Tables(0).Rows(intCnt).Item("PieceRateID"))
                objPieceRateDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objPieceRateDs.Tables(0).Rows(intCnt).Item("Status"))
                objPieceRateDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objPieceRateDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objPieceRateDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objPieceRateDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objPieceRateDs
    End Function

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                lblCurrentIndex.Text = 0
            Case "prev"
                lblCurrentIndex.Text = _
                    Math.Max(0, lblCurrentIndex.Text - 1)
            Case "next"
                lblCurrentIndex.Text = _
                    Math.Min(lblPageCount.Text - 1, lblCurrentIndex.Text + 1)
            Case "last"
                lblCurrentIndex.Text = lblPageCount.Text - 1
        End Select
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        BindGrid()
    End Sub

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIF(SortCol.Text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid() 
    End Sub

    Sub DEDR_Delete(Sender As Object, e As DataGridCommandEventArgs)
        Dim strOpCd As String = "PR_CLSTRX_PIECERATEPAY_STATUS_UPD"
        Dim strPieceRateId As String
        Dim strParam As String = ""
        Dim strSelectedConID As String 
        Dim intErrNo As Integer        
        Dim lblTemp As Label

        dgLine.EditItemIndex = CInt(E.Item.ItemIndex)
        lblTemp = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblPieceRateId")
        strSelectedConID = lblTemp.Text
        strParam = strSelectedConID & "|" & objPRTrx.EnumPieceRateStatus.Cancelled
        
        Try
            intErrNo = objPRTrx.mtdUpdPieceRatePay(strOpCd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strAccMonth, _
                                                  strAccYear, _
                                                  strParam, _
                                                  True, _
                                                  strPieceRateId)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECERATEPAY_CANCEL&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/PR_trx_PieceRatePayList.aspx")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub NewPayBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_trx_PieceRatePayDet.aspx")
    End Sub


End Class
