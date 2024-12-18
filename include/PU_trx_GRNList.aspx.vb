
Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class PU_GRNList : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrOnHand As Label
    Protected WithEvents lblErrOnHold As Label
    Protected WithEvents dgGRNList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtGoodsRetId As TextBox
    Protected WithEvents ddlGRNType As DropDownList
    Protected WithEvents txtSupplierCode As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents NewINGRNBtn As ImageButton
    Protected WithEvents NewDCGRNBtn As ImageButton
    Protected WithEvents NewFAGRNBtn As ImageButton
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected objPU As New agri.PU.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPUAR As Integer
    Dim intFAAR As Integer

    Dim objGRNDs As New Object()
    Dim objGRNLnDs As New Object()
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
        intFAAR = Session("SS_FAAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReturnNote), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            NewINGRNBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewINGRNBtn).ToString())
            NewDCGRNBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewDCGRNBtn).ToString())
            NewFAGRNBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewFAGRNBtn).ToString())

            If objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = False Then
                NewFAGRNBtn.Visible = False
            End If

            If SortExpression.Text = "" Then
                SortExpression.Text = "A.GoodsRetId"
            End If
            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                If intLevel = 0 Then
                    ddlStatus.SelectedIndex = 1
                Else
                    ddlStatus.SelectedIndex = 0
                End If
                BindGRNType()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgGRNList.CurrentPageIndex = 0
        dgGRNList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGRNType()
        ddlGRNType.Items.Clear()
        ddlGRNType.Items.Add(New ListItem(objPU.mtdGetGRNType(objPU.EnumGRNType.All), objPU.EnumGRNType.All))
        ddlGRNType.Items.Add(New ListItem(objPU.mtdGetGRNType(objPU.EnumGRNType.DirectCharge), objPU.EnumGRNType.DirectCharge))
        ddlGRNType.Items.Add(New ListItem(objPU.mtdGetGRNType(objPU.EnumGRNType.FixedAsset), objPU.EnumGRNType.FixedAsset))
        ddlGRNType.Items.Add(New ListItem("Stock / Workshop", objPU.EnumGRNType.Stock)) 
    End Sub

    Sub BindGrid()
        Dim strOpCd As String = "PU_CLSTRX_GRN_GET"
        Dim strSrchGoodsRetId As String
        Dim strSrchGoodsRetType As String
        Dim strSrchSuppliercode As String
        Dim strSrchName As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strAccYear = lstAccYear.SelectedItem.Value
        strSrchGoodsRetId = IIf(txtGoodsRetId.Text = "", "", txtGoodsRetId.Text)
        strSrchGoodsRetType = IIf(ddlGRNType.SelectedItem.Value = 0, "", ddlGRNType.SelectedItem.Value)
        strSrchSuppliercode = IIf(txtSupplierCode.Text = "", "", txtSupplierCode.Text)
        strSrchName = IIf(txtName.Text = "", "", txtName.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)

        strParam = strSrchGoodsRetId & "|" & _
                   strLocation & "|" & _
                   strSrchGoodsRetType & "|" & _
                   strSrchSuppliercode & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|" & _
                   strSrchName & "|" & _
                   strAccMonth & "|" & _
                   strAccYear

        Try
            intErrNo = objPU.mtdGetGRN(strOpCd, _
                                       strParam, _
                                       objGRNDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_GRN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_GRNList.aspx")
        End Try

        For intCnt = 0 To objGRNDs.Tables(0).Rows.Count - 1
            objGRNDs.Tables(0).Rows(intCnt).Item("GoodsRetId") = Trim(objGRNDs.Tables(0).Rows(intCnt).Item("GoodsRetId"))
            If objGRNDs.Tables(0).Rows(intCnt).Item("GoodsRetType").Trim() = objPU.EnumGRNType.Stock Then
                objGRNDs.Tables(0).Rows(intCnt).Item("GoodsRetType") = "Stock / Workshop"
            Else
                objGRNDs.Tables(0).Rows(intCnt).Item("GoodsRetType") = objPU.mtdGetGRNType(objGRNDs.Tables(0).Rows(intCnt).Item("GoodsRetType").Trim())
            End If
            objGRNDs.Tables(0).Rows(intCnt).Item("SupplierCode") = Trim(objGRNDs.Tables(0).Rows(intCnt).Item("SupplierCode"))
            objGRNDs.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objGRNDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objGRNDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objGRNDs.Tables(0).Rows(intCnt).Item("Status"))
            objGRNDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objGRNDs.Tables(0).Rows(intCnt).Item("UserName"))
        Next

        PageCount = objGlobal.mtdGetPageCount(objGRNDs.Tables(0).Rows.Count, dgGRNList.PageSize)
        dgGRNList.DataSource = objGRNDs
        If dgGRNList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgGRNList.CurrentPageIndex = 0
            Else
                dgGRNList.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgGRNList.DataBind()
        BindPageList()

        For intCnt = 0 To dgGRNList.Items.Count - 1
            lbl = dgGRNList.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objPU.EnumGRNStatus.Active
                    lbButton = dgGRNList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    lbButton = dgGRNList.Items.Item(intCnt).FindControl("lbUndelete")
                    lbButton.Visible = False
                Case objPU.EnumGRNStatus.Confirmed, objPU.EnumGRNStatus.Cancelled, objPU.EnumGRNStatus.Closed
                    lbButton = dgGRNList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    lbButton = dgGRNList.Items.Item(intCnt).FindControl("lbUndelete")
                    lbButton.Visible = False
                Case objPU.EnumGRNStatus.Deleted
                    lbButton = dgGRNList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    If lstAccMonth.SelectedItem.Value >= Session("SS_PUACCMONTH") Then
                        lbButton = dgGRNList.Items.Item(intCnt).FindControl("lbUndelete")
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                        lbButton.Visible = True
                    Else
                        lbButton = dgGRNList.Items.Item(intCnt).FindControl("lbUndelete")
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                        lbButton.Visible = False
                    End If
            End Select
        Next

        PageNo = dgGRNList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgGRNList.PageCount
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgGRNList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgGRNList.CurrentPageIndex
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgGRNList.CurrentPageIndex = 0
            Case "prev"
                dgGRNList.CurrentPageIndex = _
                    Math.Max(0, dgGRNList.CurrentPageIndex - 1)
            Case "next"
                dgGRNList.CurrentPageIndex = _
                    Math.Min(dgGRNList.PageCount - 1, dgGRNList.CurrentPageIndex + 1)
            Case "last"
                dgGRNList.CurrentPageIndex = dgGRNList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgGRNList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgGRNList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgGRNList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgGRNList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_GetGRNLn As String = "PU_CLSTRX_GRN_LINE_GET"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGRN As String = "PU_CLSTRX_GRN_UPD"
        Dim GRCell As TableCell = e.Item.Cells(0)
        Dim strSelectedGoodsRetId As String = GRCell.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer

        strParam = strSelectedGoodsRetId & "|" & _
                   strLocation & "|||" & _
                   objPU.EnumGRNStatus.Deleted

        Try
            intErrNo = objPU.mtdUpdGRNLn(strOpCd_GetGRNLn, _
                                         strOpCd_GetItem, _
                                         strOpCd_UpdItem, _
                                         strOpCd_UpdGRN, _
                                         strCompany, _
                                         strLocation, _
                                         strUserId, _
                                         strParam, _
                                         intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DELETE_GRN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_grnlist.aspx")
            End If
        End Try

        dgGRNList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_GetGRNLn As String = "PU_CLSTRX_GRN_LINE_GET"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGRN As String = "PU_CLSTRX_GRN_UPD"
        Dim GRCell As TableCell = E.Item.Cells(0)
        Dim strSelectedGoodsRetId As String = GRCell.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer

        strParam = strSelectedGoodsRetId & "|" & _
                   strLocation & "|||" & _
                   objPU.EnumGRNStatus.Active

        Try
            intErrNo = objPU.mtdUpdGRNLn(strOpCd_GetGRNLn, _
                                         strOpCd_GetItem, _
                                         strOpCd_UpdItem, _
                                         strOpCd_UpdGRN, _
                                         strCompany, _
                                         strLocation, _
                                         strUserId, _
                                         strParam, _
                                         intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UNDELETE_GRN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_grnlist.aspx")
            End If
        End Try

        dgGRNList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewINGRNBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_GRNDet.aspx?GRNType=" & objPU.EnumGRNType.Stock)
    End Sub

    Sub NewDCGRNBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_GRNDet.aspx?GRNType=" & objPU.EnumGRNType.DirectCharge)
    End Sub

    Sub NewFAGRNBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_GRNDet.aspx?GRNType=" & objPU.EnumGRNType.FixedAsset)
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
