
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


Public Class popUpSetRemainLifeTime : Inherits Page

    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objINTrx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents lblErrMessage As System.Web.UI.WebControls.Label
    Protected WithEvents SortExpression As System.Web.UI.WebControls.Label
    Protected WithEvents lblTracker As System.Web.UI.WebControls.Label
    Protected WithEvents dgLine As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnPrev As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lstDropList As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnNext As System.Web.UI.WebControls.ImageButton
    Protected WithEvents SortCol As System.Web.UI.WebControls.Label
    Protected WithEvents tblMain As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents TxtMinScreen As TextBox
    Protected WithEvents rdotampil As RadioButton
    Protected WithEvents rdoTampilNo As RadioButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblerrisActive As Label
    Private Const FAngka As String = "###,##0.00"

    Dim objItemDs As New Object()
    Dim strUserId As String
    Dim strLocCode As String
    Dim strCode As String
    Dim strDescr As String

    Dim strJavaScript As String
    Dim strForm As String
    Dim intStatus As Integer
    Dim strFormAction As String
    Dim strINItem As String
    Dim blnPostBack As Boolean
    Dim strCompany As String
    Dim objSetParam As New Object()


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strUserId = Session("SS_USERID")
        strLocCode = Session("SS_LOCATION")
        strCompany = Session("SS_COMPANY")
        strForm = Request.QueryString("fname")
        strFormAction = Request.QueryString("faction")
        'intStatus = Convert.ToInt32(lblHiddenSts.Text)

        If strUserId <> "" Then
            If Not Page.IsPostBack Then
                onload_display()
                LoadData()
            End If
        Else
            lblErrMessage.Text = "Page Is Expired"
            lblErrMessage.Visible = True
            tblMain.Visible = False
        End If

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCode As String = "IN_CLSTRX_ITEMTOMACHINE_PARAMETER_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim IntStatus As Integer

        strParamName = "LOCCODE"
        strParamValue = "" & strLocCode

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objItemDs.Tables(0).Rows.Count > 0 Then
            lblHiddenSts.Text = objItemDs.Tables(0).Rows(0).Item("Status")
            TxtMinScreen.Text = Format(objItemDs.Tables(0).Rows(0).Item("SetMinPopUp"), FAngka)
            IntStatus = objItemDs.Tables(0).Rows(0).Item("IsPopUp")
            If IntStatus = 0 Then
                rdoTampilNo.Checked = True
                rdotampil.Checked = False
            Else
                rdotampil.Checked = True
                rdoTampilNo.Checked = False
            End If
        End If
        Return objItemDs

    End Function

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim strOpCd_Add As String = "IN_CLSTRX_ITEMTOMACHINE_PARAMETER_ADD"
        'Dim strOpCd_Upd As String = "IN_CLSTRX_ITEMTOMACHINE_UPD"
        'Dim strOpCd_Get As String = "IN_CLSTRX_ITEMTOMACHINE_GET"
        'Dim strOpCd_Sts As String = "IN_CLSTRX_ITEMTOMACHINE_STATUS_UPD"
        'Dim blnIsUpdated As Boolean
        'Dim strOpCd As String
        'Dim strGang As String = Request.Form("ddlItem")
        'Dim objTrxID As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        'Dim intErrNo As Integer
        'Dim strParam As String = ""

        If strCmdArgs = "Save" Then

            InsertRecord()
        End If

        'If strSelectedSubBlkCode <> "" Then
        '    onload_display()
        '    onLoad_DisplayLine()
        '    onLoad_BindButton()
        'End If
    End Sub

    Sub InsertRecord()
        Dim objTrxDs As New DataSet()
        Dim strOpCd_Add As String = "IN_CLSTRX_ITEMTOMACHINE_PARAMETER_ADD"
        Dim strOpCd_Sts As String = "IN_CLSTRX_ITEMTOMACHINE_PARAMETER_STATUS_UPD"
        Dim strOpCd As String
        Dim intErrNo As Integer
        'Dim strParam As String
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim IntMinScreen As Integer

        strOpCd = IIf(lblHiddenSts.Text = vbNullString, strOpCd_Add, strOpCd_Sts)

        IntMinScreen = CDbl(0 & TxtMinScreen.Text)
      
        If rdotampil.Checked = True And CDbl(0 & TxtMinScreen.Text) = 0 Then
            lblErrMessage.Text = "Please Input Minimum Life Time Screen"
            lblErrMessage.Visible = True
        ElseIf rdotampil.Checked = True And CDbl(0 & TxtMinScreen.Text) > 0 Then
            IntMinScreen = CDbl(0 & TxtMinScreen.Text)
        Else
            IntMinScreen = 0
        End If

        strParamName = _
                IIf(rdotampil.Checked = True, 1, 0) & "|" & _
                            IntMinScreen & "|" & _
                            strLocCode & "|" & _
                            1 & "|" & _
                            Date.Now & "|" & _
                            Date.Now & "|" & _
                            strUserId
        Try

            intErrNo = objINTrx.mtdUpdItemToMachineTrx_Parameter(strOpCd, _
                                            strCompany, _
                                            strLocCode, _
                                            strUserId, _
                                            strParamName, _
                                            False)

        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEMTOMACHINE_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/IN_clstrx_WPdet.aspx")
        End Try
        objTrxDs = Nothing
    End Sub

    Sub onload_display()
        open_script()
        onload_printfunc()
        onload_closefunc()
        close_script()
        print_script()
    End Sub

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
