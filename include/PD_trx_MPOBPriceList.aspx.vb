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


Public Class PD_trx_MPOBPriceList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtPeriod As TextBox
    Protected WithEvents ddlProduct As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objPDTrx As New agri.PD.clsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPDAR As Integer

    Dim objMPOBDs As New Object()
    Dim objConfigDs As New Object()
    Dim objLangCapDs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        intPDAR = Session("SS_PDAR")

        If strUserId = "" Then
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMPOBPrice), intPDAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "mo.UpdateDate"
            End If
            If Not Page.IsPostBack Then
                BindProductList()
                BindStatusList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindStatusList()
        ddlStatus.Items.Add(New ListItem(objPDTrx.mtdGetMPOBPriceStatus(objPDTrx.EnumMPOBPriceStatus.All), objPDTrx.EnumMPOBPriceStatus.All))
        ddlStatus.Items.Add(New ListItem(objPDTrx.mtdGetMPOBPriceStatus(objPDTrx.EnumMPOBPriceStatus.Active), objPDTrx.EnumMPOBPriceStatus.Active))
        ddlStatus.Items.Add(New ListItem(objPDTrx.mtdGetMPOBPriceStatus(objPDTrx.EnumMPOBPriceStatus.Deleted), objPDTrx.EnumMPOBPriceStatus.Deleted))
        ddlStatus.SelectedIndex = 1
    End Sub

    Sub BindProductList()
        ddlProduct.Items.Add(New ListItem("All", "all"))
        ddlProduct.Items.Add(New ListItem(objPDTrx.mtdGetProductType(objPDTrx.EnumProductType.CPO), objPDTrx.EnumProductType.CPO))
        ddlProduct.Items.Add(New ListItem(objPDTrx.mtdGetProductType(objPDTrx.EnumProductType.FFB), objPDTrx.EnumProductType.FFB))
        ddlProduct.Items.Add(New ListItem(objPDTrx.mtdGetProductType(objPDTrx.EnumProductType.PK), objPDTrx.EnumProductType.PK))
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
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

        dsData = LoadData()
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
        lblTracker.Text = "Page " & PageNo & " of " & dgLine.PageCount

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objPDTrx.EnumMPOBPriceStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPDTrx.EnumMPOBPriceStatus.Deleted
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
        Next


    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet

        Dim strOpCd_GET As String = "CM_CLSTRX_MPOB_GET"
        Dim strSearch As String = ""
        Dim strSort As String = ""
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer


        strSearch = strSearch & "and mo.LocCode = '" & Trim(strLocation) & "' "

        If Trim(txtPeriod.Text) <> "" Then
            If InStr(txtPeriod.Text, "/") > 0 Then
                strSearch = strSearch & "and rtrim(mo.AccMonth) + '/' + mo.AccYear like '" & Trim(txtPeriod.Text) & "%' "
            Else
                strSearch = strSearch & "and rtrim(mo.AccMonth) + mo.AccYear like '" & Trim(txtPeriod.Text) & "%' "
            End If
        End If

        If ddlProduct.SelectedItem.Value <> "all" Then
            strSearch = strSearch & "and mo.ProductCode = '" & ddlProduct.SelectedItem.Value & "' "
        End If

        If ddlStatus.SelectedItem.Value <> CInt(objPDTrx.EnumMPOBPriceStatus.All) Then
            strSearch = strSearch & "and mo.Status = '" & ddlStatus.SelectedItem.Value & "' "
        End If

        If Trim(txtLastUpdate.Text) <> "" Then
            strSearch = strSearch & "and usr.UserName like '" & Trim(txtLastUpdate.Text) & "%' "
        End If

        strSort = "order by " & Trim(SortExpression.Text) & " " & SortCol.Text
        strParam = strSearch & "|" & strSort

        Try
            intErrNo = objPDTrx.mtdGetMPOBPrice(strOpCd_GET, strParam, 0, objMPOBDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_MPOBPRICELIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_mpobpricelist.aspx")
        End Try

        If objMPOBDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objMPOBDs.Tables(0).Rows.Count - 1
                objMPOBDs.Tables(0).Rows(intCnt).Item("CompositKey") = Trim(objMPOBDs.Tables(0).Rows(intCnt).Item("CompositKey"))
                objMPOBDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objMPOBDs.Tables(0).Rows(intCnt).Item("LocCode"))
                objMPOBDs.Tables(0).Rows(intCnt).Item("Period") = Trim(objMPOBDs.Tables(0).Rows(intCnt).Item("Period"))
                objMPOBDs.Tables(0).Rows(intCnt).Item("AccMonth") = Trim(objMPOBDs.Tables(0).Rows(intCnt).Item("AccMonth"))
                objMPOBDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objMPOBDs.Tables(0).Rows(intCnt).Item("AccYear"))
                objMPOBDs.Tables(0).Rows(intCnt).Item("ProductCode") = Trim(objMPOBDs.Tables(0).Rows(intCnt).Item("ProductCode"))
                objMPOBDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objMPOBDs.Tables(0).Rows(intCnt).Item("Status"))
                objMPOBDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objMPOBDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objMPOBDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objMPOBDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If
        Return objMPOBDs
    End Function


    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
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


    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid()

    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "asc", "desc", "asc")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim strOpCd_GET As String = ""
        Dim strOpCd_ADD As String = ""
        Dim strOpCd_UPD As String = "CM_CLSTRX_MPOB_UPD"
        Dim strParam As String = ""
        Dim strMPOBLocCode As String
        Dim strMPOBAccMonth As String
        Dim strMPOBAccYear As String
        Dim strProductCode As String
        Dim intErrNo As Integer

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lbl = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblLocCode")
        strMPOBLocCode = lbl.Text
        lbl = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccMonth")
        strMPOBAccMonth = lbl.Text
        lbl = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccYear")
        strMPOBAccYear = lbl.Text
        lbl = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblProductCode")
        strProductCode = lbl.Text

        strParam = strMPOBLocCode & Chr(9) & _
                   strMPOBAccMonth & Chr(9) & _
                   strMPOBAccYear & Chr(9) & _
                   strProductCode & Chr(9) & _
                   "" & Chr(9) & _
                   objPDTrx.EnumMPOBPriceStatus.Deleted

        Try
            intErrNo = objPDTrx.mtdUpdMPOBPrice(strOpCd_GET, _
                                                strOpCd_ADD, _
                                                strOpCd_UPD, _
                                                "", "", _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                False, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_MPOBPRICELIST_DEL&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_mpobpricelist.aspx")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewTBBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PD_trx_MPOBPriceDet.aspx")
    End Sub





End Class
