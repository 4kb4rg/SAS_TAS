Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class PM_DispKrnlQtyList : Inherits Page

    Protected WithEvents dgDispKrnlList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList

    Protected objPMTrx As New agri.PM.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim objProdDs As New DataSet()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPMAR = Session("SS_PMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDailyProduction), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TransDate"
            End If

            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetDailyProdStatus(objPMTrx.EnumDailyProdStatus.Active), objPMTrx.EnumDailyProdStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetDailyProdStatus(objPMTrx.EnumDailyProdStatus.Deleted), objPMTrx.EnumDailyProdStatus.Deleted))

    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgDispKrnlList.CurrentPageIndex = 0
        dgDispKrnlList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim intStatus As Integer

        dgDispKrnlList.DataSource = LoadData()
        dgDispKrnlList.DataBind()

        intStatus = CInt(srchStatusList.SelectedItem.Value)


        PageNo = dgDispKrnlList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgDispKrnlList.PageCount
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgDispKrnlList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgDispKrnlList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "PM_CLSTRX_DISPKRNLQTY_GET"
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = objPMTrx.EnumDailyProdStatus.All, "", srchStatusList.SelectedItem.Value)
        strSrchLastUpdate = IIf(srchUpdateBy.Text = "", "", srchUpdateBy.Text)

        strParam = strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "||"

        Try
            intErrNo = objPMTrx.mtdGetDailyProd(strOppCode_Get, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                objProdDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketList.aspx")
        End Try

        For intCnt = 0 To objProdDs.Tables(0).Rows.Count - 1
            objProdDs.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objProdDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objProdDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objProdDs.Tables(0).Rows(intCnt).Item("Status"))
            objProdDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objProdDs.Tables(0).Rows(intCnt).Item("UserName"))
        Next

        Return objProdDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgDispKrnlList.CurrentPageIndex = 0
            Case "prev"
                dgDispKrnlList.CurrentPageIndex = _
                Math.Max(0, dgDispKrnlList.CurrentPageIndex - 1)
            Case "next"
                dgDispKrnlList.CurrentPageIndex = _
                Math.Min(dgDispKrnlList.PageCount - 1, dgDispKrnlList.CurrentPageIndex + 1)
            Case "last"
                dgDispKrnlList.CurrentPageIndex = dgDispKrnlList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgDispKrnlList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgDispKrnlList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgDispKrnlList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgDispKrnlList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)



        dgDispKrnlList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_DailyProd_Det.aspx")
    End Sub

End Class
