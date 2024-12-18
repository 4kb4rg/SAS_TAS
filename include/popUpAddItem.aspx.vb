Imports System
Imports System.Data
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings


Public Class PopUpAddItem : Inherits Page

    Protected WithEvents ddlItemCode As DropDownList
    Protected WithEvents txtQuantity As TextBox
    Protected WithEvents txtAddNote As TextBox
    Protected WithEvents dgLineDet As DataGrid

    Protected WithEvents ibConfirm As ImageButton
    Protected WithEvents trItm As HtmlTableRow
    Protected WithEvents trQty As HtmlTableRow

    Protected WithEvents lblErrItemCode As Label
    Protected WithEvents lblErrQuantity As Label
    Protected WithEvents lblItemCode As Label

    Protected WithEvents lblErrMessage As Label

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objPRTrx As New agri.PR.clsTrx()

    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim intConfig As Integer
    Dim strJavaScript As String

    Dim strForm As String
    Dim strFormAction As String
    Dim strAccCode As String
    Dim strBlkCode As String
    Dim strID As String
    Dim strItm As String

    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strItmDesc As String = ""
    Dim arrSplitItemCdPart As Array
    Dim intErrNo As Integer
    Dim intCnt As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")

        strID = Request.QueryString("id")
        strAccCode = Request.QueryString("acc")
        strBlkCode = Request.QueryString("blk")

        If Not Page.IsPostBack Then
            GetLangCap()
            onLoad_DisplayLine(strID, strAccCode, strBlkCode)
            BindItemCode("")
        End If

    End Sub

    Sub onLoad_DisplayLine(ByVal pv_strID As String, ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim objWPTrxLnDs As New DataSet()
        Dim strOpCd As String = "PR_CLSTRX_WPTRX_LINE_ITEM_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton1 As LinkButton
        Dim intStatus As Integer

        strParam = "|" & " Where A.WPTrxID = '" & Trim(pv_strID) & "' And A.AccCode = '" & Trim(pv_strAccCode) & "' And A.BlkCode = '" & Trim(pv_strBlkCode) & "' "

        Try
            intErrNo = objPRTrx.mtdGetWPTrxLn(strOpCd, _
                                                strParam, _
                                                objWPTrxLnDs, _
                                                False)

        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRXLINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dgLineDet.DataSource = objWPTrxLnDs.Tables(0)
        dgLineDet.DataBind()
        If objWPTrxLnDs.Tables(0).Rows.Count > 0 Then
            intStatus = objWPTrxLnDs.Tables(0).Rows(0).Item("Status").Trim()
            If intStatus = objPRTrx.EnumWPTrxStatus.Active Then
                For intCnt = 0 To dgLineDet.Items.Count - 1
                    lbButton1 = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton1.Visible = True
                    lbButton1.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Next
            Else
                For intCnt = 0 To dgLineDet.Items.Count - 1
                    lbButton1 = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton1.Visible = False
                Next
            End If
        End If
    End Sub

    Sub BindItemCode(ByVal pv_ItemCode As String)
        Dim strOpCdItemCode_Get As String = "IN_CLSSETUP_INVMASTER_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim SearchStr As String
        Dim strParam As String


        SearchStr = "AND ItemType in ('" & objINSetup.EnumInventoryItemType.Stock & "') AND itm.Status = '" & objINSetup.EnumStockItemStatus.Active & "' "

        strParam = "ORDER BY ItemCode asc|" & SearchStr

        Try
            intErrNo = objINSetup.mtdGetMasterList(strOpCdItemCode_Get, strParam, objINSetup.EnumInventoryMasterType.StockItem, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_stockitem.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
        Next intCnt

        Dim drinsert As DataRow
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Please Select Item Code "
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        ddlItemCode.DataSource = dsForDropDown.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
    End Sub

    Sub GetLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strItmMonth As String = ""
        Dim strItmYear As String = ""

        strParam = Session("SS_LANGCODE")
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strItmMonth, _
                                                 strItmYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKADJ_DET_GET_LANGCAP&errmesg=&redirect=IN/trx/IN_trx_StockAdj_list.aspx")
        End Try

        lblItemCode.Text = GetCaption(objLangCap.EnumLangCap.StockItem)
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objWPTrxDs As New DataSet()
        Dim strOpCode_AddLine As String = "PR_CLSTRX_WPTRX_LINE_ITEM_ADD"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_WPTRX_STATUS_UPD"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strItem As String = Trim(ddlItemCode.SelectedItem.Value)
        Dim strQty As String = Trim(txtQuantity.Text)
        Dim strAddNote As String = Trim(txtAddNote.Text)

        If strItem = "" Then
            lblErrItemCode.Visible = True
            Exit Sub
        ElseIf strQty = "" Or strQty = "0" Then
            lblErrQuantity.Visible = True
            Exit Sub
        End If

        strParam = strID & "|" & _
                    strAccCode & "|" & _
                    strBlkCode & "|" & _
                    strItem & "|" & _
                    strQty & "|" & _
                    strAddNote & "|" & _
                    Convert.ToString(objPRTrx.EnumWPTrxStatus.Active)

        Try
            intErrNo = objPRTrx.mtdUpdWPTrxLineItem(strOpCode_UpdID, _
                                                    strOpCode_AddLine, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    False, _
                                                    objWPTrxDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strID)
        End Try

        onLoad_DisplayLine(strID, strAccCode, strBlkCode)

        objWPTrxDs = Nothing
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "PR_CLSTRX_WPTRX_LINE_ITEM_DEL"
        Dim strOpCode_UpdID As String = "PR_CLSTRX_WPTRX_STATUS_UPD"
        Dim strParam As String
        Dim lblItemCode As Label
        Dim strItem As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = Convert.ToInt32(E.Item.ItemIndex)
        lblItemCode = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("lblItemCode")
        strItem = lblItemCode.Text

        Try
            strParam = strID & "|" & _
                       strAccCode & "|" & _
                       strBlkCode & "|" & _
                       strItem & "|||" & _
                       Convert.ToString(objPRTrx.EnumWPTrxStatus.Active)

            intErrNo = objPRTrx.mtdUpdWPTrxLineItem(strOpCode_UpdID, _
                                                strOpCode_DelLine, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                False, _
                                                objResult)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_LINE_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strID)
        End Try

        onLoad_DisplayLine(strID, strAccCode, strBlkCode)
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        If objLangCapDs.Tables(0).Rows.Count > 0 Then
            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill Then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    Else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    End If
                    Exit For
                End If
            Next
        End If
    End Function

 
    'Sub onload_display()
    '    open_script()
    '    onload_printfunc()
    '    onload_closefunc()
    '    close_script()
    '    print_script()
    'End Sub

    'Sub onclick_reload(ByVal pv_strProperty As String, ByVal pv_strValue As String, ByVal pv_blnIsInstr As Boolean, ByVal pv_blnIsPostBack As Boolean)
    '    strJavaScript += "_GetValue('" + strForm + "','" + pv_strProperty + "','" + pv_strValue + "','" & pv_blnIsInstr & "');" + Chr(10) + _
    '                     " _returnopenerdropdown(); " + Chr(10)
    '    If pv_blnIsPostBack = True Then
    '        strJavaScript += "   if (window.opener.document." + strForm + "." + pv_strProperty + ".fireEvent('onchange') == null) {" + Chr(10) + _
    '                         "       window.opener.document." + strForm + ".fireEvent('onchange') = true; }" + Chr(10)
    '    End If
    '    strJavaScript += "   self.close();" + Chr(10) + _
    '                     " " + Chr(10)
    'End Sub

    'Sub onload_printfunc()
    '    strJavaScript += "function onload_setfocus() { " + Chr(10)
    'End Sub

    'Sub onload_closefunc()
    '    strJavaScript += " } " + Chr(10)
    'End Sub

    'Sub onload_setfocus(ByVal pv_strProperty As String)
    '    strJavaScript += "document.frmMain." & pv_strProperty & ".focus(); " + Chr(10) + "document.frmMain." & pv_strProperty & ".select();" + Chr(10)
    'End Sub

    'Sub opener_findtextbox()

    '    strJavaScript += "function _GetValue(_form, _property, _searchval, _isinstr) {" + Chr(10) + _
    '                     "  var txt = eval('window.opener.document.' + _form + '.' + _property);" + Chr(10) + _
    '                     "  txt.Text = _searchval; }" + Chr(10)


    '    strJavaScript += "function _returnopenerdropdown() {" + Chr(10) + _
    '                     "   var _execmd; " + Chr(10) + _
    '                     "   if (_cmdstr != '') {" + Chr(10) + _
    '                     "      _execmd = eval(_cmdstr);" + Chr(10) + _
    '                     "      _execmd;" + Chr(10) + _
    '                     "   }" + Chr(10) + _
    '                     "}" + Chr(10)
    'End Sub


    'Sub opener_dropdownval(ByVal pv_strProperty As String, ByVal pv_strValue As String)
    '    strJavaScript += "_cmdstr += 'window.opener.FindTextBox(\'" & strForm & "\',\'" & pv_strProperty & "\',\'" & pv_strValue & "\'); '" + Chr(10)
    'End Sub

    'Sub open_script()
    '    strJavaScript += "<Script Language=""JavaScript"">" + Chr(10) + _
    '                     "var _cmdstr = '';" + Chr(10)
    'End Sub

    Sub close_script()
        strJavaScript += "</Script>" + Chr(10)
    End Sub

    'Sub print_script()
    '    If strJavaScript <> "<Script Language=""JavaScript""></Script>" Or _
    '       strJavaScript <> "<Script Language=""JavaScript"">" Or _
    '       strJavaScript <> "</Script>" Then
    '        Response.Write(strJavaScript)
    '    End If
    '    strJavaScript = ""
    'End Sub

    'Sub onsubmit_checkcode()
    '    Dim arrItemValue As Array
    '    open_script()
    '    opener_findtextbox()
    '    If trItm.Visible = True Then
    '        onload_printfunc()
    '        onload_closefunc()
    '        opener_dropdownval(strItm, ddlItemCode.SelectedItem.Value)
    '        onclick_reload(strItm, ddlItemCode.SelectedItem.Value, False, False)
    '    End If
    '    close_script()
    '    print_script()
    'End Sub
End Class
