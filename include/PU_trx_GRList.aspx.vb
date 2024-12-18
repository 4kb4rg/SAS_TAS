

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


Public Class PU_GRList : Inherits Page

    Protected WithEvents lblErrOnHand As Label
    Protected WithEvents lblErrOnHold As Label
    Protected WithEvents dgGRList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtGoodsRcvId As TextBox
    Protected WithEvents txtGoodsRcvRefNo As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents txtPOID As TextBox
    Protected WithEvents ddlPOType As DropDownList
    Protected WithEvents txtSuppCode As TextBox
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents NewGRBtn As ImageButton

    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents ddlGRLocation As DropDownList

    Protected objPU As New agri.PU.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdminLoc As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPUAR As Integer

    Dim objGRDs As New Object()
    Dim objGRLnDs As New Object()
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer
    Dim strLocLevel As String


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
        intLevel = Session("SS_USRLEVEL")
        strLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUGoodsReceive), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            NewGRBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewGRBtn).ToString())

            If SortExpression.Text = "" Then
                SortExpression.Text = "A.GoodsRcvId"
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

                If strLocLevel = "2" Or strLocLevel = "3" Then
                    BindLocation("", strLocation)
                Else
                    BindLocation(strLocation, strLocation)
                    ddlLocation.Enabled = False
                    ddlGRLocation.Enabled = False
                End If

                BindPOType()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgGRList.CurrentPageIndex = 0
        dgGRList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim strOpCd As String = "PU_CLSTRX_GR_GET"
        Dim strSrchGoodsRcvId As String
        Dim strSrchGoodsRcvRefNo As String
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
        Dim strSrchPOID As String
        Dim strSrchPOType As String
        Dim strSrchSuppCode As String
        Dim sSQLKriteria As String


        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

		strAccYear = lstAccYear.SelectedItem.Value	
        sSQLKriteria = "AND a.AccYear='" & strAccYear & _
                    "' AND a.AccMonth in ('" & strAccMonth & "') "

        'If strLocLevel = "2" Or strLocLevel = "3" Then
        '    sSQLKriteria = sSQLKriteria & "AND A.LocCode='" & strLocation & "'"
        'End If

        If ddlPOType.SelectedIndex > 0 Then
            sSQLKriteria = sSQLKriteria & "AND D.POType='" & ddlPOType.SelectedItem.Value & "'"
        End If

        If Len(txtGoodsRcvId.Text) > 0 Then
            sSQLKriteria = sSQLKriteria & "AND a.GoodsRcvID LIKE '%" & txtGoodsRcvId.Text & "%'"
        End If

        If Len(txtPOID.Text) > 0 Then
            sSQLKriteria = sSQLKriteria & "AND a.POID LIKE '%" & txtPOID.Text & "%'"
        End If

        'If ddlLocation.SelectedIndex > 0 Then
        '    sSQLKriteria = sSQLKriteria & "AND A.POID IN (SELECT POID FROM PU_POLN WHERE PRRefLocCode = '" & ddlLocation.SelectedItem.Value & "')"
        'End If

        If ddlGRLocation.SelectedIndex > 0 Then
            sSQLKriteria = sSQLKriteria & "AND A.GRLocCode = '" & ddlGRLocation.SelectedItem.Value & "'"
        End If

        If Len(txtSuppCode.Text) > 0 Then
            sSQLKriteria = sSQLKriteria & "AND (A.SupplierCode like '%" & txtSuppCode.Text & "%' OR B.Name like '%" & txtSuppCode.Text & "%')"
        End If

        If ddlStatus.SelectedIndex > 0 Then
            sSQLKriteria = sSQLKriteria & "AND A.Status='" & ddlStatus.SelectedItem.Value & "'"
        End If

        strParam = "STRSEARCH|LOCCODE"
        strSearch = sSQLKriteria & "|" & strLocation

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, strParam, strSearch, objGRDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/PU_trx_GRList.aspx")
        End Try

        'strAccYear = lstAccYear.SelectedItem.Value
        'strSrchGoodsRcvId = IIf(txtGoodsRcvId.Text = "", "", txtGoodsRcvId.Text)
        'strSrchGoodsRcvRefNo = IIf(txtGoodsRcvRefNo.Text = "", "", txtGoodsRcvRefNo.Text)
        'strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        'strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        'strSrchPOID = IIf(txtPOID.Text = "", "", txtPOID.Text)
        'strSrchPOType = IIf(ddlPOType.SelectedItem.Value = 0, "", ddlPOType.SelectedItem.Value)
        'strSrchSuppCode = IIf(txtSuppCode.Text = "", "", txtSuppCode.Text)

        'strParam = strSrchGoodsRcvId & "|" & _
        '           strLocation & "|" & _
        '           strSrchGoodsRcvRefNo & "|" & _
        '           strSrchStatus & "|" & _
        '           strSrchLastUpdate & "|" & _
        '           strSrchPOID & "|" & _
        '           strSrchPOType & "|" & _
        '           SortExpression.Text & "|" & _
        '           SortCol.Text & "|" & _
        '           strSrchSuppCode

        'Try
        '    intErrNo = objPU.mtdGetGR(strCompany, _
        '                              strLocation, _
        '                              strUserId, _
        '                              strAccMonth, _
        '                              strAccYear, _
        '                              strOpCd, _
        '                              strParam, _
        '                              objGRDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/PU_trx_GRList.aspx")
        'End Try

        For intCnt = 0 To objGRDs.Tables(0).Rows.Count - 1
            objGRDs.Tables(0).Rows(intCnt).Item("GoodsRcvId") = objGRDs.Tables(0).Rows(intCnt).Item("GoodsRcvId").Trim()
            objGRDs.Tables(0).Rows(intCnt).Item("GoodsRcvRefNo") = objGRDs.Tables(0).Rows(intCnt).Item("GoodsRcvRefNo").Trim()
            objGRDs.Tables(0).Rows(intCnt).Item("Status") = objGRDs.Tables(0).Rows(intCnt).Item("Status").Trim()
            objGRDs.Tables(0).Rows(intCnt).Item("UserName") = objGRDs.Tables(0).Rows(intCnt).Item("UserName").Trim()
        Next

        PageCount = objGlobal.mtdGetPageCount(objGRDs.Tables(0).Rows.Count, dgGRList.PageSize)
        dgGRList.DataSource = objGRDs
        If dgGRList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgGRList.CurrentPageIndex = 0
            Else
                dgGRList.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgGRList.DataBind()
        BindPageList()

        For intCnt = 0 To dgGRList.Items.Count - 1
            lbl = dgGRList.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objPU.EnumGRStatus.Active
                    lbButton = dgGRList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False 'True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    lbButton = dgGRList.Items.Item(intCnt).FindControl("lbUndelete")
                    lbButton.Visible = False
                Case objPU.EnumGRStatus.Confirmed, objPU.EnumGRStatus.Cancelled
                    lbButton = dgGRList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    lbButton = dgGRList.Items.Item(intCnt).FindControl("lbUndelete")
                    lbButton.Visible = False
                Case objPU.EnumGRStatus.Deleted
                    lbButton = dgGRList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    If lstAccMonth.SelectedItem.Value >= Session("SS_PUACCMONTH") Then
                        lbButton = dgGRList.Items.Item(intCnt).FindControl("lbUndelete")
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                        lbButton.Visible = False 'True
                    Else
                        lbButton = dgGRList.Items.Item(intCnt).FindControl("lbUndelete")
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                        lbButton.Visible = False
                    End If
                Case Else
                    lbButton = dgGRList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    lbButton = dgGRList.Items.Item(intCnt).FindControl("lbUndelete")
                    lbButton.Visible = False
            End Select
        Next

        PageNo = dgGRList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgGRList.PageCount
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgGRList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgGRList.CurrentPageIndex
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgGRList.CurrentPageIndex = 0
            Case "prev"
                dgGRList.CurrentPageIndex = _
                    Math.Max(0, dgGRList.CurrentPageIndex - 1)
            Case "next"
                dgGRList.CurrentPageIndex = _
                    Math.Min(dgGRList.PageCount - 1, dgGRList.CurrentPageIndex + 1)
            Case "last"
                dgGRList.CurrentPageIndex = dgGRList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgGRList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgGRList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgGRList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgGRList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_GetGRLn As String = "PU_CLSTRX_GR_LINE_GET"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGR As String = "PU_CLSTRX_GR_UPD"
        Dim GRCell As TableCell = e.Item.Cells(0)
        Dim StsCell As TableCell = e.Item.Cells(1)
        Dim strSelectedGoodsRcvId As String = GRCell.Text
        Dim strSelectedStatus As Integer = CInt(StsCell.Text)
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer

        strParam = strSelectedGoodsRcvId & "||||||" & objPU.EnumGRStatus.Deleted

        Try
            intErrNo = objPU.mtdUpdGRLn(strOpCd_GetGRLn, _
                                        strOpCd_GetPOLn, _
                                        strOpCd_UpdPOLn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdGR, _
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DELETE_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End If
        End Try

        dgGRList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_GetGRLn As String = "PU_CLSTRX_GR_LINE_GET"
        Dim strOpCd_GetPOLn As String = "PU_CLSTRX_PO_LINE_GET"
        Dim strOpCd_UpdPOLn As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGR As String = "PU_CLSTRX_GR_UPD"
        Dim GRCell As TableCell = e.Item.Cells(0)
        Dim StsCell As TableCell = e.Item.Cells(1)
        Dim strSelectedGoodsRcvId As String = GRCell.Text
        Dim strSelectedStatus As Integer = CInt(StsCell.Text)
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer

        strParam = strSelectedGoodsRcvId & "||||||" & objPU.EnumGRStatus.Active

        Try
            intErrNo = objPU.mtdUpdGRLn(strOpCd_GetGRLn, _
                                        strOpCd_GetPOLn, _
                                        strOpCd_UpdPOLn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdGR, _
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DELETE_GR&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
            End If
        End Try

        dgGRList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewGRBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_GRDet.aspx")
    End Sub

    Sub BindPOType()
        ddlPOType.Items.Clear()
        ddlPOType.Items.Add(New ListItem(objPU.mtdGetPOType(objPU.EnumPOType.All), objPU.EnumPOType.All))
        ddlPOType.Items.Add(New ListItem(objPU.mtdGetPOType(objPU.EnumPOType.DirectCharge), objPU.EnumPOType.DirectCharge))
        ddlPOType.Items.Add(New ListItem(objPU.mtdGetPOType(objPU.EnumPOType.FixedAsset), objPU.EnumPOType.FixedAsset))
        ddlPOType.Items.Add(New ListItem("Stock / Workshop", objPU.EnumPOType.Stock))

        ddlPOType.Items.Add(New ListItem(objPU.mtdGetPOType(objPU.EnumPOType.Nursery), objPU.EnumPOType.Nursery))

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.ToString() & "&redirect=")
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

    Sub BindLocation(ByVal pv_strLocCode As String, ByVal pv_strGRLocCode As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim intSelectedIndexGR As Integer

        strPRRefLocCode = IIf(pv_strLocCode = "", "", pv_strLocCode)
        strParam = strPRRefLocCode & "|" & objAdminLoc.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objLocDs, "")

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            With objLocDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If .Item("LocCode") = Trim(strPRRefLocCode) Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "-All-"
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocation.DataSource = objLocDs.Tables(0)
        ddlLocation.DataValueField = "LocCode"
        ddlLocation.DataTextField = "Description"
        ddlLocation.DataBind()
        ddlLocation.SelectedIndex = intSelectedIndex

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            With objLocDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If .Item("LocCode") = Trim(pv_strGRLocCode) Then intSelectedIndexGR = intCnt + 1
            End With
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "-All-"
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlGRLocation.DataSource = objLocDs.Tables(0)
        ddlGRLocation.DataValueField = "LocCode"
        ddlGRLocation.DataTextField = "Description"
        ddlGRLocation.DataBind()
        ddlGRLocation.SelectedIndex = intSelectedIndexGR
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
