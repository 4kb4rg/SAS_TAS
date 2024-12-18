Imports System
Imports System.Data
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings


Public Class PopUpFind : Inherits Page

    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtSubBlkCode As TextBox
    Protected WithEvents txtVehCode As TextBox
    Protected WithEvents txtVehExpCode As TextBox
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox
    Protected WithEvents txtINItemCode As TextBox
    Protected WithEvents txtCTItemCode As TextBox
    Protected WithEvents txtWSItemCode As TextBox
    Protected WithEvents txtItemPartNo As TextBox
    Protected WithEvents txtDCItemCode As TextBox
    Protected WithEvents txtADCode As TextBox
    Protected WithEvents ibConfirm As ImageButton
    Protected WithEvents trAcc As HtmlTableRow
    Protected WithEvents trBlk As HtmlTableRow
    Protected WithEvents trSubBlk As HtmlTableRow
    Protected WithEvents trVeh As HtmlTableRow
    Protected WithEvents trVehExp As HtmlTableRow
    Protected WithEvents trEmp As HtmlTableRow
    Protected WithEvents trEmpName As HtmlTableRow
    Protected WithEvents trINItem As HtmlTableRow
    Protected WithEvents trCTItem As HtmlTableRow
    Protected WithEvents trWSItem As HtmlTableRow
    Protected WithEvents trDCItem As HtmlTableRow
    Protected WithEvents trAD As HtmlTableRow
    Protected WithEvents trWSItemPart As HtmlTableRow 

    Protected WithEvents lblChartOfAccount As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblSubBlock As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehicleExpense As Label

    Protected WithEvents lblErrWSCode As Label
    Protected WithEvents lblErrEmpCode As Label
    Protected WithEvents lstEmpCode As DropDownList
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Const INVENTORY_ITEM = 1
    CONST CANTEEN_ITEM = 2
    CONST WORKSHOP_ITEM = 3
    CONST DIRECTCHARGE_ITEM = 4
    Const PART_NO = 5 

    Dim objLangCapDs As New DataSet()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim intConfig As integer
    Dim strJavaScript As String

    Dim strForm As String
    Dim strFormAction As String
    Dim strAcc As String
    Dim strAccType As String
    Dim strBlk As String
    Dim strSubBlk As String
    Dim strVeh As String
    Dim strVehExp As String
    Dim strEmp As String
    Dim strINType As String
    Dim strINItem As String
    Dim strCTItem As String
    Dim strWSItem As String
    Dim strDCItem As String
    Dim strAD As String
    Dim strADType As String
    Dim strChargeLocCode As String
    Dim strBlockCharge As String

    Dim strAccDesc As String = ""
    Dim strBlkDesc As String = ""
    Dim strSubBlkDesc As String = ""
    Dim strVehDesc As String = ""
    Dim strVehExpDesc As String = ""
    Dim strEmpDesc As String = ""
    Dim strItemDesc As String = ""
    Dim strADDesc As String = ""
    Dim strItemPartDesc As String = ""
    Dim strPartNo As String
    Dim arrSplitItemCdPart As Array
   
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        
        strChargeLocCode = Trim(Request.QueryString("chargeloccode"))
        strBlockCharge = Trim(Request.QueryString("blockcharge"))
        strForm = Request.QueryString("fname")
        strFormAction = Request.QueryString("faction")
        strAcc = Request.QueryString("acc")
        strAccType = Request.QueryString("acctype")
        strBlk = Request.QueryString("blk")
        strSubBlk = Request.QueryString("subblk")
        strVeh = Request.QueryString("veh")
        strVehExp = Request.QueryString("vehexp")
        strEmp = Request.QueryString("emp")
        strINType = Request.QueryString("intype")
        strINItem = Request.QueryString("initem")
        strCTItem = Request.QueryString("ctitem")
        strWSItem = Request.QueryString("wsitem")
        strDCItem = Request.QueryString("dcitem")
        strAD = Request.QueryString("ad")
        strADType = Request.QueryString("adtype")

        If strChargeLocCode = "" Then   
            strChargeLocCode = Session("SS_LOCATION")   
        End If

        If Session("SS_COSTLEVEL") <> "block" And strBlockCharge = "" Then 
            strSubBlk = strBlk
            strBlk = ""
        End If

        lblErrWSCode.Visible = False
        lblErrEmpCode.Visible = False
        If Not Page.IsPostBack Then
            GetLangCap()
            txtAccCode.Text = Trim(Request.QueryString("accval"))
            If Session("SS_COSTLEVEL") = "block" Or strBlockCharge <> "" Then
                txtBlkCode.Text = Trim(Request.QueryString("blkval"))
            Else
                If Trim(Request.QueryString("subblkval")) = "" Then
                    txtSubBlkCode.Text = Trim(Request.QueryString("blkval"))
                Else
                    txtSubBlkCode.Text = Trim(Request.QueryString("subblkval"))
                End If
            End If
            txtVehCode.Text = Trim(Request.QueryString("vehval"))
            txtVehExpCode.Text = Trim(Request.QueryString("vehexpval"))
            txtEmpCode.Text = Trim(Request.QueryString("empval"))
            txtCTItemCode.Text = Trim(Request.QueryString("itemval"))

            arrSplitItemCdPart = Split(Trim(Request.QueryString("itemval")), " @ ")
            If arrSplitItemCdPart.GetUpperBound(0) = 1 Then 
                txtINItemCode.Text = arrSplitItemCdPart(0)
                txtWSItemCode.Text = arrSplitItemCdPart(0)
                txtItemPartNo.Text = ""
            Else 
                If Trim(Request.QueryString("itemval")).indexof("@") > -1 Then
                    txtINItemCode.Text = Trim(Request.QueryString("itemval")).substring(0,Trim(Request.QueryString("itemval")).indexof("@")-1)
                    txtWSItemCode.Text = Trim(Request.QueryString("itemval")).substring(0,Trim(Request.QueryString("itemval")).indexof("@")-1)
                    txtItemPartNo.Text = ""
                Else
                    txtINItemCode.Text = Trim(Request.QueryString("itemval"))
                    txtWSItemCode.Text = Trim(Request.QueryString("itemval"))
                    txtItemPartNo.Text = ""
                End If 
            End If   
           
            txtDCItemCode.Text = Trim(Request.QueryString("itemval"))
            txtADCode.Text = Trim(Request.QueryString("adval"))
            onload_display()
        Else
            onsubmit_checkcode()
        End If
    End Sub

    Sub GetLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAccCode As String
        Dim strBlkCode As String
        Dim strVehCode As String
        Dim strVehExpCode As String
        Dim strAccMonth As String = ""
        Dim strAccYear As String = ""

        strParam = Session("SS_LANGCODE")
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKADJ_DET_GET_LANGCAP&errmesg=&redirect=IN/trx/IN_trx_StockAdj_list.aspx")
        End Try

        lblChartOfAccount.Text = GetCaption(objLangCap.EnumLangCap.Account)
        lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block)
        lblSubBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblVehicleExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense)
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function

    Sub onload_display()
        txtAccCode.MaxLength = Session("SS_ACCCODELEN")

        open_script()
        onload_printfunc()

        If strVehExp <> "" Then
            trVehExp.Visible = True
            onload_setfocus("txtVehExpCode")
        End If
        If strVeh <> "" Then
            trVeh.Visible = True
            onload_setfocus("txtVehCode")
        End If
        If strBlk <> "" Then
            trBlk.Visible = True
            onload_setfocus("txtBlkCode")
        End If
        If strSubBlk <> "" Then
            trSubBlk.Visible = True
            onload_setfocus("txtSubBlkCode")
        End If
        If strAcc <> "" Then
            trAcc.Visible = True
            onload_setfocus("txtAccCode")
        End If
        If strEmp <> "" Then
            trEmp.Visible = True
            trEmpName.Visible=True
            onload_setfocus("txtEmpCode")
        End If
        If strINItem <> "" Then
            If strINType = "3" Or strINType = "4" Then
                trWSItemPart.Visible = True
            End If
            trINItem.Visible = True
            onload_setfocus("txtINItemCode")
        End If
        If strCTItem <> "" Then
            trCTItem.Visible = True
            onload_setfocus("txtCTItemCode")
        End If
        If strWSItem <> "" Then
            trWSItem.Visible = True
            trWSItemPart.Visible = True
            onload_setfocus("txtWSItemCode")
        End If
        If strDCItem <> "" Then
            trDCItem.Visible = True
            onload_setfocus("txtDCItemCode")
        End If
        If strAD <> "" Then
            trAD.Visible = True
            onload_setfocus("txtADCode")
        End If

        onload_closefunc()
        close_script()
        print_script()
    End Sub

    Sub onsubmit_checkcode()
        Dim _strAccType As String
        Dim _strAccPurpose As String
        Dim arrItemValue As Array
        Dim _strEmpCode as String
        open_script()
        opener_finddropdown()
        If trAcc.Visible = True Then
            Select Case strAccType
                Case "1","5"
                    onload_printfunc()
                    onload_setfocus("txtAccCode")
                    onload_closefunc()
                    onclick_reload(strAcc, txtAccCode.Text, False, False)
                Case "2","6"
                    onload_printfunc()
                    onload_setfocus("txtAccCode")
                    onload_closefunc()
                    If trBlk.Visible = True Then opener_dropdownval(strBlk, txtBlkCode.Text)
                    If trSubBlk.Visible = True Then opener_dropdownval(strSubBlk, txtSubBlkCode.Text)
                    onclick_reload(strAcc, txtAccCode.Text, False, True)
                Case Else
                    onload_printfunc()
                    onload_setfocus("txtAccCode")
                    onload_closefunc()
                    If trBlk.Visible = True Then opener_dropdownval(strBlk, txtBlkCode.Text)
                    If trSubBlk.Visible = True Then opener_dropdownval(strSubBlk, txtSubBlkCode.Text)
                    opener_dropdownval(strVeh, txtVehCode.Text)
                    opener_dropdownval(strVehExp, txtVehExpCode.Text)
                    onclick_reload(strAcc, txtAccCode.Text, False, True)
            End Select
        End If
        If (trBlk.Visible = True And strAccType = "") Then
            onload_printfunc()
            onload_setfocus("txtBlkCode")
            onload_closefunc()
            onclick_reload(strBlk, txtBlkCode.Text, False, False)
        End If
        If (trSubBlk.Visible = True And strAccType = "") Then
            onload_printfunc()
            onload_setfocus("txtSubBlkCode")
            onload_closefunc()
            onclick_reload(strSubBlk, txtSubBlkCode.Text, False, False)
        End If
        If (trVeh.Visible = True And strAccType = "") Then
            onload_printfunc()
            onload_setfocus("txtVehCode")
            onload_closefunc()
            onclick_reload(strVeh, txtVehCode.Text, False, False)
        End If
        If (trVehExp.Visible = True And strAccType = "") Then
            onload_printfunc()
            onload_setfocus("txtVehExpCode")
            onload_closefunc()
            onclick_reload(strVehExp, txtVehExpCode.Text, False, False)
        End If

        If trINItem.Visible = True Or trWSItem.Visible = True Then        
            If trWSItemPart.Visible = True Then                
                If trWSItem.Visible = True Then
                    If txtWSItemCode.Text = "" AND txtItemPartNo.Text = "" Then
                        lblErrWSCode.Visible = True                        
                        onload_printfunc()
                        onload_setfocus("txtWSItemCode")
                        onload_closefunc()                        
                    Else
                        If txtWSItemCode.Text <> "" Then
                            onload_printfunc()
                            onload_setfocus("txtWSItemCode")
                            onload_closefunc()                        
                            onclick_reload(strWSItem, txtWSItemCode.Text, False, False)
                        ElseIf txtItemPartNo.Text <> "" Then
                            onload_printfunc()
                            onload_setfocus("txtItemPartNo") 
                            onload_closefunc()
                            onclick_reload(strWSItem, txtItemPartNo.Text, True, False) 
                        End If
                    End If
                Else 
                    If (txtINItemCode.Text <> "" And txtItemPartNo.Text <> "") Or (txtINItemCode.Text = "" And txtItemPartNo.Text = "") Then
                        lblErrWSCode.Visible = True
                        onload_printfunc()
                        onload_setfocus("txtINItemCode")
                        onload_closefunc() 
                    Else
                        If txtINItemCode.Text <> "" Then
                            onload_printfunc()
                            onload_setfocus("txtINItemCode")
                            onload_closefunc()                        
                            onclick_reload(strINItem, txtINItemCode.Text, False, False)
                        ElseIf txtItemPartNo.Text <> "" Then
                            onload_printfunc()
                            onload_setfocus("txtItemPartNo") 
                            onload_closefunc()
                            onclick_reload(strINItem, txtItemPartNo.Text, True, False) 
                        End If
                    End If                        
                End If                
            Else
                onload_printfunc()
                onload_setfocus("txtINItemCode")
                onload_closefunc()
                onclick_reload(strINItem, txtINItemCode.Text, False, False)
            End If
        End If


           
            If trEmp.Visible = True Then                
                If txtEmpCode.Text = "" AND txtEmpName.Text = "" Then
                        lblErrEmpCode.Visible = True                        
                        onload_printfunc()
                        onload_setfocus("txtEmpCode")
                        onload_closefunc()                        
                ElseIf txtEmpCode.Text <> "" And txtEmpName.Text <> "" Then
                        _strEmpCode=BindEmpList(True)                       
                        onload_printfunc()
                        onload_setfocus("txtEmpName") 
                        onload_closefunc()                        
                        onclick_reload(strEmp,_strEmpCode , False, False)
                Else
                    If txtEmpCode.Text <> "" Then
                        onload_printfunc()
                        onload_setfocus("txtEmpCode")
                        onload_closefunc()                        
                        onclick_reload(strEmp, txtEmpCode.Text, False, False)
                    ElseIf txtEmpName.Text <> "" Then
                        _strEmpCode=BindEmpList(False)                       
                        onload_printfunc()
                        onload_setfocus("txtEmpName") 
                        onload_closefunc()                        
                        onclick_reload(strEmp,_strEmpCode , False, False)                
                       
                    End If
                End If
            End If
      


        If trCTItem.Visible = True Then
            onload_printfunc()
            onload_setfocus("txtCTItemCode")
            onload_closefunc()
            onclick_reload(strCTItem, txtCTItemCode.Text, False, False)
        End If

        If trDCItem.Visible = True Then
            onload_printfunc()
            onload_setfocus("txtDCItemCode")
            onload_closefunc()
            onclick_reload(strDCItem, txtDCItemCode.Text, False, False)
        End If

        If trAD.Visible = True Then
            onload_printfunc()
            onload_setfocus("txtADCode")
            onload_closefunc()
            onclick_reload(strAD, txtADCode.Text, False, False)
        End If

        close_script()
        print_script()
    End Sub

    Sub onclick_reload(ByVal pv_strProperty As String, ByVal pv_strValue As String, ByVal pv_blnIsInstr As Boolean, ByVal pv_blnIsPostBack As Boolean)
        strJavaScript += "if (_findopenerdropdown('" + strForm + "','" + pv_strProperty + "','" + pv_strValue + "','" & pv_blnIsInstr & "') == false) {" + chr(10) + _
                         "   alert('Invalid code given, please try again.'); " + chr(10) + _
                         "} " + chr(10) + _
                         "else {" + chr(10) + _
                         "   _returnopenerdropdown();" + chr(10)
        If pv_blnIsPostBack = True Then
        strJavaScript += "   if (window.opener.document." + strForm + "." + pv_strProperty + ".fireEvent('onchange') == null) {" + chr(10) + _
                         "       window.opener.document." + strForm + ".fireEvent('onchange') = true; }" + chr(10)
        End If
        strJavaScript += "   self.close();" + chr(10) + _
                         "} " + chr(10)
    End Sub

    Sub onload_printfunc()
        strJavaScript += "function onload_setfocus() { " + chr(10)
    End Sub

    Sub onload_closefunc()
        strJavaScript += " } " + chr(10)
    End Sub

    Sub onload_setfocus(ByVal pv_strProperty As String)
        strJavaScript += "document.frmMain." & pv_strProperty & ".focus(); " + chr(10) + "document.frmMain." & pv_strProperty & ".select();" + chr(10)
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
        strJavaScript += "_cmdstr += 'window.opener.FindDropDownList(\'" & strForm & "\',\'" & pv_strProperty & "\',\'" & pv_strValue & "\'); '" + chr(10)
    End Sub

    Sub open_script()
        strJavaScript += "<Script Language=""JavaScript"">" + chr(10) + _
                         "var _cmdstr = '';" + chr(10)
    End Sub

    Sub close_script()
        strJavaScript += "</Script>" + chr(10)
    End Sub

    Sub print_script()
        If strJavaScript <> "<Script Language=""JavaScript""></Script>" Or _
           strJavaScript <> "<Script Language=""JavaScript"">" Or _
           strJavaScript <> "</Script>" Then
            Response.Write(strJavaScript)
        End If
        strJavaScript = ""
    End Sub
    Function BindEmpList(ByVal _isboth as Boolean) as String
        Dim strOpCdEmployee_Get As String = "HR_CLSSETUP_EMPLOYEE_LIST_GET"
        Dim dsForDropDown As New DataSet               
        Dim intCnt As Integer       
        Dim SearchStr As String
        Dim strParam As New StringBuilder("")
        Dim intErrNo As Integer      
        Dim drinsert As DataRow
        Dim intSelectedIndex as Integer
        
        Try
            strParam.Append("|||")
                        
            strParam.Append(objHRTrx.EnumEmpStatus.active)
            strParam.Append("|")
            strParam.Append(strLocation)
            strParam.Append("|Mst.EmpCode|ASC")

            intErrNo = objHRTrx.mtdGetEmployeeList(strOpCdEmployee_Get, strParam.ToString, dsForDropDown)
        Catch Exp As Exception
        
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))

            If _isBoth Then
                dsForDropDown.Tables(0).Rows(intCnt).Item(1) = UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))) & " ( " & _
                                                                UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1))) & " ) "
                If dsForDropDown.Tables(0).Rows(intCnt).Item(1) = UCase(Trim(txtEmpCode.Text)) & " ( " & UCase(Trim(txtEmpName.Text)) & " ) " Then
                        intSelectedIndex = intCnt + 1
                End If
            Else
                dsForDropDown.Tables(0).Rows(intCnt).Item(1) = UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)))

                If dsForDropDown.Tables(0).Rows(intCnt).Item(1) = UCase(Trim(txtEmpName.Text)) Then
                    intSelectedIndex = intCnt + 1
                End If 
            End If
          
           
          
        Next intCnt
        
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Select employee"
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstEmpCode.DataSource = dsForDropDown.Tables(0)
        lstEmpCode.DataValueField = "EmpCode"
        lstEmpCode.DataTextField = "EmpName"
        lstEmpCode.DataBind()
        lstEmpCode.SelectedIndex = intSelectedIndex
               
        Return lstEmpCode.SelectedItem.Value.ToString
        

        
    End Function
End Class
