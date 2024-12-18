
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


Public Class popUpFindINPR : Inherits Page

    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim ObjOk As New agri.GL.ClsTrx()

    Protected WithEvents lblErrMessage As System.Web.UI.WebControls.Label
    Protected WithEvents SortExpression As System.Web.UI.WebControls.Label
    Protected WithEvents lblTracker As System.Web.UI.WebControls.Label
    Protected WithEvents txtItemCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents LblDesc As System.Web.UI.WebControls.Label
    Protected WithEvents txtsaldo As System.Web.UI.WebControls.Label
    Protected WithEvents txtCost As System.Web.UI.WebControls.Label
    Protected WithEvents ibConfirm As System.Web.UI.WebControls.ImageButton
    Protected WithEvents dgLine As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnPrev As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lstDropList As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DDLPrStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnNext As System.Web.UI.WebControls.ImageButton
    Protected WithEvents SortCol As System.Web.UI.WebControls.Label
    Protected WithEvents tblMain As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents txtPrID As System.Web.UI.WebControls.Label
    Protected WithEvents txtTotalCost As System.Web.UI.WebControls.Label
    Protected WithEvents txtPrLoc As System.Web.UI.WebControls.Label
    Protected WithEvents txtAddNote As System.Web.UI.WebControls.Label
	Protected WithEvents txtPRLnID As System.Web.UI.WebControls.Label
	Protected WithEvents txtPurchaseUOM As System.Web.UI.WebControls.Label

    Dim objItemDs As New Object()
    Dim strUserId As String
    Dim strLocCode As String
    Dim strPRID As String
    Dim strCode As String
    Dim strName As String
    Dim strCost As String
    Dim strQty As String
    Dim strTotal As String
    Dim strLoc As String
    Dim strDescr As String
	Dim strNote As String
	Dim strPRLnID As String
	Dim strPOUOM As String
	
	
    Dim strJavaScript As String
    Dim strForm As String
    Dim strFormAction As String

    Dim strINItem As String
    Dim StrInItemName As String
    Dim strUnitCost As String
    Dim strSaldo As String
    Dim strPRNumber As String
    Dim strTotalCost As String
    Dim strPRLoc As String
    Dim strAddNote As String
	Dim strPOType As String
	Dim strPRLnNumber As String
	Dim strPurchaseUOM As String

    Dim blnPostBack As Boolean

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strUserId = Session("SS_USERID")
        strLocCode = Session("SS_LOCATION")
		strPOType = Session("SS_POTYPE")

        strForm = Request.QueryString("fname")
        strFormAction = Request.QueryString("faction")
        strINItem = Request.QueryString("initem")
        StrInItemName = Request.QueryString("initemName")
        strUnitCost = Request.QueryString("initemCost")
        strSaldo = Request.QueryString("initemQty")
        strPRNumber = Request.QueryString("inPRID")
        strTotalCost = Request.QueryString("initemTotalCost")
        strPRLoc = Request.QueryString("inPrLoccode")
        strAddNote = Request.QueryString("inAddNote")
		strPRLnNumber = Request.QueryString("inPRLnID")
		strPurchaseUOM = Request.QueryString("inPurchaseUOM")
        blnPostBack = Request.QueryString("ispostback")

        If strUserId <> "" Then
            If SortExpression.Text = "" Then
                SortExpression.Text = "ItemCode"
            End If
            If Not Page.IsPostBack Then

                DDLPrStatus.Items.Clear()
                DDLPrStatus.Items.Add("PR Active")
                DDLPrStatus.Items.Add("PR Status PO")
                'BindLocation()
                BindGrid()
                BindPageList()
                onload_display()
            End If
        Else
            lblErrMessage.Text = "Page Is Expired"
            lblErrMessage.Visible = True
            tblMain.Visible = False
        End If
    End Sub

    Protected Sub ibConfirm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibConfirm.Click
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub onload_display()
        open_script()
        onload_printfunc()
        onload_setfocus("txtItemCode")
        onload_closefunc()
        close_script()
        print_script()
    End Sub


    '#### GRID SECTION -S --------------------------------------
    '-------Bind dataset to Datagrid---------------
    'Sub BindLocation()
    '    Dim strOpCd As String = "PWSYSTEM_CLSUSER_USERDAILY_CONTROL_GET_LOCATION_GET"
    '    Dim dr As DataRow
    '    Dim intErrNo As Integer
    '    Dim intCnt As Integer
    '    Dim intSelectedIndex As Integer = 0
    '    Dim dsForDropDown As DataSet
    '    Dim strParamValue As String = ""
    '    Dim StrParamName As String = ""
    '    Dim objTransDs As New Object()

    '    StrParamName = ""
    '    strParamValue = ""

    '    Try
    '        intErrNo = ObjOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objTransDs)
    '    Catch Exp As System.Exception
    '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
    '    End Try

    '    dr = objTransDs.Tables(0).NewRow()
    '    dr("LocCode") = ""
    '    'dr("_Description") = lblSelect.Text & strAccountTag
    '    objTransDs.Tables(0).Rows.InsertAt(dr, 0)

    '    ddlPRRefLocCode.DataSource = objTransDs.Tables(0)
    '    ddlPRRefLocCode.DataValueField = "LocCode"
    '    ddlPRRefLocCode.DataTextField = "Description"
    '    ddlPRRefLocCode.DataBind()
    '    ddlPRRefLocCode.SelectedIndex = intSelectedIndex

    '    If Not objTransDs Is Nothing Then
    '        objTransDs = Nothing
    '    End If
    'End Sub

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


        If DDLPrStatus.SelectedIndex = 0 Then
            dgLine.Enabled = True
        Else
            dgLine.Enabled = False
        End If

    End Sub

    '-------Create Paging Dropdownlist--------------------
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

    '-------Calls the com to create a dataset  --------------------
    Protected Function LoadData() As DataSet

        Dim strOpCode As String = "PU_CLSTRX_PR_ID_PO_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim nItem As String = txtItemCode.Text

        strParamName = "TIPE|SEARCH|POTYPE|USERPO"
        If txtItemCode.Text = "" Then
            strParamValue = "" & DDLPrStatus.SelectedItem.Value & "|%" & nItem & "%" & "|" & strPOType & "|" & strUserId
        Else
            strParamValue = "" & DDLPrStatus.SelectedItem.Value & "|%" & txtItemCode.Text & "%" & "|" & strPOType & "|" & strUserId
        End If

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objItemDs
    End Function

    Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    '------------ Event for clicking on Previous or Next ---------------
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

    '------------ Event for Changing the Paging DropDownList ------------
    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    '------------ Event for a Page Change -----------------
    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    '------------ Event for Sorting Of Grid -----------------
    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub OnSelectItem(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)

        Dim CPCellItem As TableCell = e.Item.Cells(0)
        Dim CPCellName As TableCell = e.Item.Cells(1)
        Dim CPCellPRID As TableCell = e.Item.Cells(2)
        Dim CPCellQty As TableCell = e.Item.Cells(3)
        Dim CPCellCost As TableCell = e.Item.Cells(4)
        Dim CPCellTotalCost As TableCell = e.Item.Cells(5)
        Dim CPCellPRLoc As TableCell = e.Item.Cells(6)
        Dim CPCellAddNote As TableCell = e.Item.Cells(7)
		Dim CPCellPRLnID As TableCell = e.Item.Cells(8)
		Dim CPCellPurchaseUOM As TableCell = e.Item.Cells(9)
		
        open_script()

        If Left(strINItem, 3) = "txt" Then
            'fill with description
            'strDescr = CPCell1.Text
            'txtItemCode.Text = Trim(strDescr)
            'fill with code

            strCode = CPCellItem.Text
            strName = CPCellName.Text
            strQty = CPCellQty.Text
            strCost = CPCellCost.Text
            strPRID = CPCellPRID.Text
            strTotal = CPCellTotalCost.Text
            strLoc = CPCellPRLoc.Text
            strNote = CPCellAddNote.Text
			strPRLnID = CPCellPRLnID.Text
			strPOUOM = CPCellPurchaseUOM.Text

            txtItemCode.Text = Trim(strCode)
            LblDesc.Text = Trim(strName)
            txtsaldo.Text = lCDbl(strQty)
            txtCost.Text = lCDbl(strCost)
            txtPrID.Text = strPRID
            txtTotalCost.Text = lCDbl(strTotal)
            txtPrLoc.Text = strLoc
            txtAddNote.Text = replace(replace(Trim(strNote),vbCrlf,""),"&nbsp;","")
			txtPRLnID.Text = replace(replace(Trim(strPRLnID),vbCrlf,""),"&nbsp;","")
			txtPurchaseUOM.Text = replace(replace(Trim(strPOUOM),vbCrlf,""),"&nbsp;","")

            opener_findtextbox()
            opener_dropdownval(strINItem, txtItemCode.Text)
            opener_dropdownval(StrInItemName, LblDesc.Text)
            opener_dropdownval(strUnitCost, txtCost.Text)
            opener_dropdownval(strSaldo, txtsaldo.Text)
            opener_dropdownval(strPRNumber, txtPrID.Text)
            opener_dropdownval(strTotalCost, txtTotalCost.Text)
            opener_dropdownval(strPRLoc, txtPrLoc.Text)
            opener_dropdownval(strAddNote, txtAddNote.Text)
			opener_dropdownval(strPRLnNumber, txtPRLnID.Text)
			opener_dropdownval(strPurchaseUOM, txtPurchaseUOM.Text)
			
			
            'opener_dropdownval(strLoc, strLoc)
        Else
            strCode = CPCellItem.Text
            txtItemCode.Text = Trim(strCode)
            opener_finddropdown()
        End If

        onload_printfunc()
        onload_setfocus("txtItemCode")
        onload_closefunc()

        If Left(strINItem, 3) = "txt" Then
            onclick_reload_txt(strINItem, txtItemCode.Text, False, blnPostBack)
            onclick_reload_txt(StrInItemName, LblDesc.Text, False, blnPostBack)
            onclick_reload_txt(strUnitCost, txtCost.Text, False, blnPostBack)
            onclick_reload_txt(strSaldo, txtsaldo.Text, False, blnPostBack)
            onclick_reload_txt(strPRNumber, txtPrID.Text, False, blnPostBack)
            onclick_reload_txt(strTotalCost, txtTotalCost.Text, False, blnPostBack)
            onclick_reload_txt(strPRLoc, txtPrLoc.Text, False, blnPostBack)
            'onclick_reload_txt(strAddNote, txtAddNote.Text, False, blnPostBack)
        Else
            onclick_reload(strINItem, txtItemCode.Text, False, blnPostBack)
        End If

        close_script()
        print_script()


    End Sub

    Sub ItemIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If DDLPrStatus.SelectedIndex = 0 Then
            dgLine.Enabled = True
        Else
            dgLine.Enabled = False
        End If

    End Sub

    '#### GRID SECTION -E --------------------------------------

    '#### JAVA SECTION -S --------------------------------------
    Sub onclick_reload(ByVal pv_strProperty As String, ByVal pv_strValue As String, ByVal pv_blnIsInstr As Boolean, ByVal pv_blnIsPostBack As Boolean)
        strJavaScript += "if (_findopenerdropdown('" + strForm + "','" + pv_strProperty + "','" + pv_strValue + "','" & pv_blnIsInstr & "') == false) {" + Chr(10) + _
                         "   alert('Invalid code given, please try again.'); " + Chr(10) + _
                         "} " + Chr(10) + _
                         "else {" + Chr(10) + _
                         "   _returnopenerdropdown();" + Chr(10)
        If pv_blnIsPostBack = True Then
            strJavaScript += "   if (window.opener.document." + strForm + "." + pv_strProperty + ".fireEvent('onchange') == null) {" + Chr(10) + _
                             "       window.opener.document." + strForm + ".fireEvent('onchange') = true; }" + Chr(10)
        End If
        strJavaScript += "   self.close();" + Chr(10) + _
                         "} " + Chr(10)
    End Sub

    Sub onclick_reload_txt(ByVal pv_strProperty As String, ByVal pv_strValue As String, ByVal pv_blnIsInstr As Boolean, ByVal pv_blnIsPostBack As Boolean)
        strJavaScript += "_GetValue('" + strForm + "','" + pv_strProperty + "','" + pv_strValue + "','" & pv_blnIsInstr & "');" + Chr(10) + _
                         " _returnopenerdropdown(); " + Chr(10)
        If pv_blnIsPostBack = True Then
            strJavaScript += "   if (window.opener.document." + strForm + "." + pv_strProperty + ".fireEvent('onchange') == null) {" + Chr(10) + _
                             "       window.opener.document." + strForm + ".fireEvent('onchange') = true; }" + Chr(10)

        End If
        strJavaScript += "   self.close();" + Chr(10) + _
                         " " + Chr(10)
    End Sub

    Sub onload_printfunc()
        strJavaScript += "function onload_setfocus() { " + Chr(10)
    End Sub

    Sub onload_closefunc()
        strJavaScript += " } " + Chr(10)
    End Sub

    Sub onload_setfocus(ByVal pv_strProperty As String)
        strJavaScript += "document.frmMain." & pv_strProperty & ".focus(); " + Chr(10) + "document.frmMain." & pv_strProperty & ".select();" + Chr(10)
    End Sub

    Sub opener_findtextbox()

        strJavaScript += "function _GetValue(_form, _property, _searchval, _isinstr) {" + Chr(10) + _
                         "  var txt = eval('window.opener.document.' + _form + '.' + _property);" + Chr(10) + _
                         "  txt.Text = _searchval; }" + Chr(10)


        strJavaScript += "function _returnopenerdropdown() {" + Chr(10) + _
                         "   var _execmd; " + Chr(10) + _
                         "   if (_cmdstr != '') {" + Chr(10) + _
                         "      _execmd = eval(_cmdstr);" + Chr(10) + _
                         "      _execmd;" + Chr(10) + _
                         "   }" + Chr(10) + _
                         "}" + Chr(10)
    End Sub

    Sub opener_finddropdown()
        strJavaScript += "function _findopenerdropdown(_form, _property, _searchval, _isinstr) {" + Chr(10) + _
                         "   var intMid, strMatch, blnAscending; " + Chr(10) + _
                         "   var ddl = eval('window.opener.document.' + _form + '.' + _property);" + Chr(10) + _
                         "   var intLow = 1; " + Chr(10) + _
                         "   var intHigh = ddl.options.length - 1; " + Chr(10) + _
                         "   var i;" + Chr(10) + _
                         "   _searchval = _searchval.toUpperCase();" + Chr(10) + _
                         "   if (ddl.options(intLow).value.toUpperCase() < ddl.options(intHigh).value.toUpperCase()) { " + Chr(10) + _
                         "       blnAscending = true; " + Chr(10) + _
                         "   }" + Chr(10) + _
                         "   else { " + Chr(10) + _
                         "       blnAscending = false; " + Chr(10) + _
                         "   }" + Chr(10) + _
                         " if (_isinstr == 'False') { " + Chr(10) + _
                         "   while (intLow <= intHigh) { " + Chr(10) + _
                         "       intMid = Math.floor((intLow + intHigh) / 2); " + Chr(10) + _
                         "       strMatch = ddl.options[intMid].value.toUpperCase(); " + Chr(10) + _
                         "       if (_isinstr == 'False') { " + Chr(10) + _
                         "          if ((_searchval == strMatch) || (_instr(strMatch, _searchval + ' @ ') > 0)) {" + Chr(10) + _
                         "              ddl.options[intMid].selected = true;" + Chr(10) + _
                         "              return true;" + Chr(10) + _
                         "          }" + Chr(10) + _
                         "          else { " + Chr(10) + _
                         "              if (((_searchval < strMatch) && (blnAscending == true)) || ((_searchval > strMatch) && (blnAscending == false))) {" + Chr(10) + _
                         "                  intHigh = intMid - 1; " + Chr(10) + _
                         "              } " + Chr(10) + _
                         "              else { " + Chr(10) + _
                         "                  intLow = intMid + 1; " + Chr(10) + _
                         "              } " + Chr(10) + _
                         "          }" + Chr(10) + _
                         "       }" + Chr(10) + _
                         "       else {" + Chr(10) + _
                         "          if (_instr(strMatch, ' @ ' + _searchval) > 0) {" + Chr(10) + _
                         "              ddl.options[intMid].selected = true;" + Chr(10) + _
                         "              return true;" + Chr(10) + _
                         "          }" + Chr(10) + _
                         "          else { " + Chr(10) + _
                         "              if (((_searchval < strMatch) && (blnAscending == true)) || ((_searchval > strMatch) && (blnAscending == false))) {" + Chr(10) + _
                         "                  intHigh = intMid - 1; " + Chr(10) + _
                         "              } " + Chr(10) + _
                         "              else { " + Chr(10) + _
                         "                  intLow = intMid + 1; " + Chr(10) + _
                         "              } " + Chr(10) + _
                         "          }" + Chr(10) + _
                         "       }" + Chr(10) + _
                         "   } // end while" + Chr(10) + _
                         "} else {" + Chr(10) + _
                         "   while (intLow <= intHigh) { " + Chr(10) + _
                         "       strMatch = ddl.options[intLow].value.toUpperCase(); " + Chr(10) + _
                         "       if (strMatch.indexOf('@') != -1) {" + Chr(10) + _
                         "           strMatch = strMatch.substring(strMatch.indexOf('@')-1,strMatch.toString().length);" + Chr(10) + _
                         "           if (strMatch == ' @ ' + _searchval) {" + Chr(10) + _
                         "               ddl.options[intLow].selected = true;" + Chr(10) + _
                         "               return true;" + Chr(10) + _
                         "           } " + Chr(10) + _
                         "       } " + Chr(10) + _
                         "       intLow = intLow + 1;" + Chr(10) + _
                         "   } //end while" + Chr(10) + _
                         "}" + Chr(10) + _
                         "   return false; " + Chr(10) + _
                         "}" + Chr(10) + Chr(13) + _
                         "function _instr(String1, String2) {" + Chr(10) + _
                         "   if (String1.toString().length == 0) return 0;" + Chr(10) + _
                         "   if (String2.toString().length == 0) return 0;" + Chr(10) + _
                         "   var index = String1.substring(0, String1.toString().length).lastIndexOf(String2);" + Chr(10) + _
                         "   if (index == -1) {return 0;} else {return index + 1;}" + Chr(10) + Chr(13) + _
                         "}" + Chr(10) + Chr(13) + _
                         "function _returnopenerdropdown() {" + Chr(10) + _
                         "   var _execmd; " + Chr(10) + _
                         "   if (_cmdstr != '') {" + Chr(10) + _
                         "      _execmd = eval(_cmdstr);" + Chr(10) + _
                         "      _execmd;" + Chr(10) + _
                         "   }" + Chr(10) + _
                         "}" + Chr(10)
    End Sub

    Sub opener_dropdownval(ByVal pv_strProperty As String, ByVal pv_strValue As String)
        If Left(strINItem, 3) = "txt" Then
            strJavaScript += "_cmdstr += 'window.opener.FindTextBox(\'" & strForm & "\',\'" & pv_strProperty & "\',\'" & pv_strValue & "\'); '" + Chr(10)
        Else
            strJavaScript += "_cmdstr += 'window.opener.FindDropDownList(\'" & strForm & "\',\'" & pv_strProperty & "\',\'" & pv_strValue & "\'); '" + Chr(10)
        End If
    End Sub

    Sub open_script()
        strJavaScript += "<Script Language=""JavaScript"">" + Chr(10) + _
                         "var _cmdstr = '';" + Chr(10)
    End Sub

    Sub close_script()
        strJavaScript += "</Script>" + Chr(10)
    End Sub

    Sub print_script()
        If strJavaScript <> "<Script Language=""JavaScript""></Script>" Or _
           strJavaScript <> "<Script Language=""JavaScript"">" Or _
           strJavaScript <> "</Script>" Then
            Response.Write(strJavaScript)
        End If
        strJavaScript = ""
    End Sub

    '#### JAVA SECTION -E --------------------------------------


End Class
