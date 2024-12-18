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


Public Class IN_Receive : Inherits Page

    Protected WithEvents dgStockTX As DataGrid
    Protected WithEvents dgStockRcvAdvOst As DataGrid
    Protected WithEvents dgStockTransOst As DataGrid

    Protected WithEvents lblTracker As Label
    Protected WithEvents lblUnDel As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchStockRcvType As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents srchStockTxID As TextBox
    Protected WithEvents srchRef As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected WithEvents ibNew As ImageButton
    Protected WithEvents ibPrint As ImageButton

    Protected objINtx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()


    Dim objOk As New agri.GL.ClsTrx()
    Dim strOppCd_GET As String = "IN_CLSTRX_STOCKRECEIVE_LIST_GET"
    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intINAR As Integer
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intINAR = Session("SS_INAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockReceive), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            ibNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibNew).ToString())
            'ibPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibPrint).ToString())

            If SortExpression.Text = "" Then
                SortExpression.Text = "tx.StockReceiveID"
            End If
            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                BindSearchList()
                BindStkRcvTypeList()
                BindGrid()
                BindGrid_OutStandingDispAdv()
                BindGrid_OutStandingStockTransfer()

                BindPageList()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim objLangCapDs As New DataSet()
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode & "|" & objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Inventory)
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../../menu/menu_INTrx_Page.aspx")
        End Try


    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgStockTX.CurrentPageIndex = 0
        dgStockTX.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbl As Label
        Dim btn As LinkButton

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgStockTX.PageSize)

        dgStockTX.DataSource = dsData
        If dgStockTX.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgStockTX.CurrentPageIndex = 0
            Else
                dgStockTX.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgStockTX.DataBind()
        BindPageList()
        PageNo = dgStockTX.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgStockTX.PageCount

        For intCnt = 0 To dgStockTX.Items.Count - 1
            lbl = dgStockTX.Items.Item(intCnt).FindControl("lblStatus")
            btn = dgStockTX.Items.Item(intCnt).FindControl("Delete")
            If CInt(lbl.Text) = objINtx.EnumStockTransferStatus.Active Then
                btn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            ElseIf CInt(lbl.Text) = objINtx.EnumStockTransferStatus.Deleted Then
                If lstAccMonth.SelectedItem.Value >= Session("SS_INACCMONTH") Then
                    btn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                    btn.Text = "Undelete"
                    btn.Visible = False
                Else
                    btn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                    btn.Text = "Undelete"
                    btn.Visible = False
                End If
            Else
                btn.Visible = False
            End If
        Next intCnt

    End Sub

    Sub BindGrid_OutStandingDispAdv()
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim intCnt As Integer = 0
        Dim lbl As Label

        dsData = LoadData_OutStandingDispAdv()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgStockRcvAdvOst.PageSize)

        dgStockRcvAdvOst.DataSource = dsData
        If dgStockRcvAdvOst.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgStockRcvAdvOst.CurrentPageIndex = 0
            Else
                dgStockRcvAdvOst.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgStockRcvAdvOst.DataBind()

        'For intCnt = 0 To dgInvOst.Items.Count - 1
        '    lbl = dgInvOst.Items.Item(intCnt).FindControl("lblNo")
        '    lbl.Text = intCnt + 1
        'Next
    End Sub

    Sub BindGrid_OutStandingStockTransfer()
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim intCnt As Integer = 0
        Dim lbl As Label

        dsData = LoadData_OutStandingStockTransfer()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgStockTransOst.PageSize)

        dgStockTransOst.DataSource = dsData
        If dgStockTransOst.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgStockTransOst.CurrentPageIndex = 0
            Else
                dgStockTransOst.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgStockTransOst.DataBind()

        'For intCnt = 0 To dgInvOst.Items.Count - 1
        '    lbl = dgInvOst.Items.Item(intCnt).FindControl("lblNo")
        '    lbl.Text = intCnt + 1
        'Next
    End Sub


    Protected Function LoadData_OutStandingDispAdv() As DataSet
        Dim strParamName As String
        Dim strParamValue As String
        Dim objOst As New DataSet()


        Dim strOppCode_Get As String = "IN_CLSTRX_DA_LINE_OUTSTANDING_GET"
        Dim intErrNo As Integer

        strParamName = "LOCCODE|ACCYEAR|ACCMONTH|SEARCHSTR"
        strParamValue = strLocation & "|" & lstAccYear.SelectedItem.Value & "|" & lstAccMonth.SelectedItem.Value & "|" & ""

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objOst)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgStockRcvAdvOst.DataSource = objOst
        dgStockRcvAdvOst.DataBind()

        Return objOst
    End Function

    Protected Function LoadData_OutStandingStockTransfer() As DataSet
        Dim strParamName As String
        Dim strParamValue As String
        Dim objOst As New DataSet()


        Dim strOppCode_Get As String = "IN_CLSTRX_SR_ST_LINE_ITEM_OUTSTANDING_GET"
        Dim intErrNo As Integer

        strParamName = "LOCCODE|STRSEARCH"
        strParamValue = strLocation & "|" & ""

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objOst)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgStockTransOst.DataSource = objOst
        dgStockTransOst.DataBind()

        Return objOst
    End Function


    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockReceiveStatus(objINtx.EnumStockReceiveStatus.All), objINtx.EnumStockReceiveStatus.All))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockReceiveStatus(objINtx.EnumStockReceiveStatus.Active), objINtx.EnumStockReceiveStatus.Active))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockReceiveStatus(objINtx.EnumStockReceiveStatus.Confirmed), objINtx.EnumStockReceiveStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objINtx.mtdGetStockReceiveStatus(objINtx.EnumStockReceiveStatus.Deleted), objINtx.EnumStockReceiveStatus.Deleted))

        If intLevel = 0 Then
            srchStatusList.SelectedIndex = 9
        Else
            srchStatusList.SelectedIndex = 0
        End If
    End Sub

    Sub BindStkRcvTypeList()
        srchStockRcvType.Items.Add(New ListItem(objINtx.mtdGetStockReceiveDocType(objINtx.EnumStockReceiveDocType.All), objINtx.EnumStockReceiveDocType.All))
        srchStockRcvType.Items.Add(New ListItem(objINtx.mtdGetStockReceiveDocType(objINtx.EnumStockReceiveDocType.DispatchAdvice), objINtx.EnumStockReceiveDocType.DispatchAdvice))
        srchStockRcvType.Items.Add(New ListItem(objINtx.mtdGetStockReceiveDocType(objINtx.EnumStockReceiveDocType.StockReturnAdvice), objINtx.EnumStockReceiveDocType.StockReturnAdvice))
        srchStockRcvType.Items.Add(New ListItem(objINtx.mtdGetStockReceiveDocType(objINtx.EnumStockReceiveDocType.StockTransfer), objINtx.EnumStockReceiveDocType.StockTransfer))
		srchStockRcvType.Items.Add(New ListItem(objINtx.mtdGetStockReceiveDocType(objINtx.EnumStockReceiveDocType.PurchaseRequisition), objINtx.EnumStockReceiveDocType.PurchaseRequisition))
		'srchStockRcvType.Items.Add(New ListItem(objINtx.mtdGetStockReceiveDocType(objINtx.EnumStockReceiveDocType.Production), objINtx.EnumStockReceiveDocType.Production))
		
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgStockTX.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgStockTX.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet

        Dim StockCode As String
        Dim Desc As String
        Dim UpdateBy As String
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strAccYear = lstAccYear.SelectedItem.Value
        strParam = srchStockTxID.Text & "|" & srchRef.Text & "|" & srchUpdateBy.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & SortExpression.Text & "|" & sortcol.Text _
                    & "|" & strAccMonth & "|" & strAccYear & "|" & strLocation & "|" & srchStockRcvType.SelectedItem.Value.Trim

        Try
            intErrNo = objINtx.mtdGetStockReceiveList(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKRECEIVELIST&errmesg=" & lblErrMessage.Text & "&redirect=../EN/menu/menu_INTrx_page.aspx")
        End Try
        Return objDataSet
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strStockTxID As String
        Dim strRefNo As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String
        Dim strRcvType As String

        strStatus = srchStatusList.SelectedItem.Value
        strStockTxID = srchStockTxID.Text
        strRefNo = srchRef.Text
        strUpdateBy = srchUpdateBy.Text
        strRcvType = srchStockRcvType.SelectedItem.Value
        strSortExp = SortExpression.Text
        strSortCol = sortcol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockReceive.aspx?strStatus=" & strStatus & _
                       "&strStockTxID=" & strStockTxID & "&strRefNo=" & strRefNo & _
                       "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & "&strRcvType=" & strRcvType & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub


    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgStockTX.CurrentPageIndex = 0
            Case "prev"
                dgStockTX.CurrentPageIndex = _
                    Math.Max(0, dgStockTX.CurrentPageIndex - 1)
            Case "next"
                dgStockTX.CurrentPageIndex = _
                    Math.Min(dgStockTX.PageCount - 1, dgStockTX.CurrentPageIndex + 1)
            Case "last"
                dgStockTX.CurrentPageIndex = dgStockTX.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgStockTX.CurrentPageIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim StrTxParam As String
        Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_STOCKRECEIVE_DETAIL_ADD"
        Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_STOCKRECEIVE_DETAIL_UPD"
        Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_STOCKRECEIVE_LINE_GET"
        Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"
        Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
        Dim strOpPRLNDet_Details_GET As String = "IN_CLSTRX_PURREQLN_DET_GET"
        Dim strOpPRLN_UPD As String = "IN_CLSTRX_PRLN_UPD"
        Dim strOpCdPR_Count_GET As String = "IN_CLSTRX_PURREQLN_FINDLIST_GET"
        Dim strOpCdPR_Details_UPD As String = "IN_CLSTRX_PURREQ_DET_UPD"

        Dim TxID As String
        Dim strTxID As String
        Dim strstatus As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim lbl As Label

        lbl = E.Item.FindControl("lblTxID")
        strTxID = Trim(lbl.Text)
        lbl = E.Item.FindControl("lblStatus")
        strstatus = Trim(lbl.Text)

        If strstatus = objINtx.EnumStockReceiveStatus.Deleted Then
            Try
                intErrNo = objINtx.mtdReceiveInvItemLevel(strOpCdStckTxLine_GET, _
                                                          strOpCdItem_Details_UPD, _
                                                          strOpCdItem_Details_GET, _
                                                          strOpCdPR_Count_GET, _
                                                          strOpCdPR_Details_UPD, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strTxID, _
                                                          strOpPRLNDet_Details_GET, _
                                                          strOpPRLN_UPD, _
                                                          objINtx.EnumTransactionAction.Undelete, _
                                                          objGlobal.EnumDocType.DispatchAdvice, _
                                                          ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UNDELETESTOCKRECEIVE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                End If

            End Try
            StrTxParam = strTxID & "|||||||||||||||" & objINtx.EnumStockReceiveStatus.Active & "|||"

        ElseIf strstatus = objINtx.EnumStockReceiveStatus.Active Then
            Try
                intErrNo = objINtx.mtdReceiveInvItemLevel(strOpCdStckTxLine_GET, _
                                                          strOpCdItem_Details_UPD, _
                                                          strOpCdItem_Details_GET, _
                                                          strOpCdPR_Count_GET, _
                                                          strOpCdPR_Details_UPD, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strTxID, _
                                                          strOpPRLNDet_Details_GET, _
                                                          strOpPRLN_UPD, _
                                                          objINtx.EnumTransactionAction.Delete, _
                                                          objGlobal.EnumDocType.DispatchAdvice, _
                                                          ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETENEWSTOCKRECEIVE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                End If

            End Try
            StrTxParam = strTxID & "|||||||||||||||" & objINtx.EnumStockReceiveStatus.Deleted & "|||"
        End If

        If intErrNo = 0 And Not ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            Try
                intErrNo = objINtx.mtdUpdStockReceiveDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            StrTxParam, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReceive), _
                                                            ErrorChk, _
                                                            TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWSTOCKRECEIVE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                End If

            End Try
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            lblUnDel.Visible = True
        End If
        BindGrid()
        BindPageList()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgStockTX.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgStockTX.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgStockTX.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_StockReceive_Details.aspx")
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
