Imports System
Imports System.Data
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings


Public Class PopUpFindNew : Inherits Page

    Protected WithEvents ddlAccount As dropdownlist
    Protected WithEvents ddlBlock As dropdownlist
    Protected WithEvents ddlSubBlock As dropdownlist
    Protected WithEvents ddlVeh As dropdownlist
    Protected WithEvents ddlVehExp As dropdownlist
    Protected WithEvents ddlDendaCode As dropdownlist

    Protected WithEvents ibConfirm As ImageButton
    Protected WithEvents trAcc As HtmlTableRow
    Protected WithEvents trBlk As HtmlTableRow
    Protected WithEvents trSubBlk As HtmlTableRow
    Protected WithEvents trVeh As HtmlTableRow
    Protected WithEvents trVehExp As HtmlTableRow
    Protected WithEvents trDenda As HtmlTableRow

    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblErrVeh As Label
    Protected WithEvents lblErrVehExp As Label
    Protected WithEvents lblChartOfAccount as Label
    Protected WithEvents lblBlock as Label
    Protected WithEvents lblVehicle as Label
    Protected WithEvents lblVehicleExpense as Label
    Protected WithEvents lblSubBlock as Label
    Protected WithEvents lblErrSubBlock as Label
    Protected WithEvents lblDendaCode as Label
    Protected WithEvents lblErrDendaCode as Label
    

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

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
    Dim strBlockCharge as string
    Dim strDendaCode as String

    Dim strAccDesc As String = ""
    Dim strBlkDesc As String = ""
    Dim strVehDesc As String = ""
    Dim strVehExpDesc As String = ""
    Dim strChargeLevel as String = ""
    Dim arrSplitItemCdPart As Array
   
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim objPRSetup As New agri.PR.clsSetup()
	Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        
        strAcc = Request.QueryString("acc")
        strBlk = Request.QueryString("blk")
        strSubBlk = Request.QueryString("subblk")
        strVeh = Request.QueryString("veh")
        strVehExp = Request.QueryString("vehexp")
        strForm = Request.QueryString("fname")
        strBlockCharge = Request.QueryString("blockcharge")
        strDendaCode = Request.QueryString("emp")

        if strAcc <> "" then 
            trAcc.visible = true 
        end if     

        if strBlockCharge <> "" then
            if strBlockCharge = "0" then
                trBlk.Visible = true 
                trSubBlk.Visible = false 
            else
                trBlk.Visible = false 
                trSubBlk.visible = true 
            end if
        end if 

        if strVeh <> "" then
            trVeh.visible = true 
        end if
        if strVehExp <> "" then
            trVehExp.visible = true 
        end if
        if strDendaCode <> "" then
            trDenda.visible = true 
        end if 

        If Not Page.IsPostBack Then
            GetLangCap()
            if strAcc <> "" then
                BindAccount("")
            end if
            if strBlockCharge <> "" then
                if strBlockCharge = "0" then
                    BindBlk("")
                else               
                    BindSubBlk("")
                end if 
            end if
            if strVeh <> "" then
                BindVeh("")
            end if
            if strVehExp <> "" then
                BindVehExp("")
            end if
            if strDendaCode <> "" then
                BindDendaCode("")
            end if
        else
            onsubmit_checkcode()
        end if 
        onload_display()
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
        lblDendaCode.Text = GetCaption(objLangCap.EnumLangCap.Denda)
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer
            if objLangCapDs.Tables(0).Rows.Count > 0 then
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
            end if 
        End Function

    Sub onload_display()
        open_script()
        onload_printfunc()
        onload_closefunc()
        close_script()
        print_script()
    End Sub

    Sub onclick_reload(ByVal pv_strProperty As String, ByVal pv_strValue As String, ByVal pv_blnIsInstr As Boolean, ByVal pv_blnIsPostBack As Boolean)
        strJavaScript += "_GetValue('" + strForm + "','" + pv_strProperty + "','" + pv_strValue + "','" & pv_blnIsInstr & "');" + chr(10) + _
                         " _returnopenerdropdown(); " + chr(10)
        If pv_blnIsPostBack = True Then
        strJavaScript += "   if (window.opener.document." + strForm + "." + pv_strProperty + ".fireEvent('onchange') == null) {" + chr(10) + _
                         "       window.opener.document." + strForm + ".fireEvent('onchange') = true; }" + chr(10)
        End If
        strJavaScript += "   self.close();" + chr(10) + _
                         " " + chr(10)
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


    Sub opener_dropdownval(ByVal pv_strProperty As String, ByVal pv_strValue As String)
        strJavaScript += "_cmdstr += 'window.opener.FindTextBox(\'" & strForm & "\',\'" & pv_strProperty & "\',\'" & pv_strValue & "\'); '" + chr(10) 
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


    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objAccDs as Object 

            strOpCd = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
            strParam = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "' "

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_TRX_ATTENDANCETRX_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Please Select The Account Code"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = objAccDs.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "_Description"
        ddlAccount.DataBind()
        ddlAccount.SelectedIndex = intSelectedIndex
        ddlAccount.AutoPostBack = True
    End Sub

    Sub BindSubBlk(ByVal pv_strSubBlk As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objSubBlkDs as Object 

        Try
            strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET1"

            strParam = "|LocCode = '" & Trim(strLocation) & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'   AND DATEDIFF(day, InitialChargeDate, GETDATE()) >= 0 "
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   objSubBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


            dr = objSubBlkDs.Tables(0).NewRow()
            dr("SubBlkCode") = ""
            dr("_Description") = "Please Select Sub Block "
            objSubBlkDs.Tables(0).Rows.InsertAt(dr, 0)

            ddlSubBlock.DataSource = objSubBlkDs.Tables(0)
            ddlSubBlock.DataValueField = "SubBlkCode"
            ddlSubBlock.DataTextField = "_Description"
            ddlSubBlock.DataBind()
            ddlSubBlock.SelectedIndex = intSelectedIndex

    End Sub

    Sub BindBlk(ByVal pv_strBlk As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objBlkDs as Object 


        Try
            strOpCd = "GL_CLSSETUP_ACCOUNT_BLOCK_GET1"

            strParam = "|LocCode = '" & Trim(strLocation) & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'   AND DATEDIFF(day, InitialChargeDate, GETDATE()) >= 0 "
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


            dr = objBlkDs.Tables(0).NewRow()
            dr("BlkCode") = ""
            dr("_Description") = "Please Select Block "
            objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

            ddlBlock.DataSource = objBlkDs.Tables(0)
            ddlBlock.DataValueField = "BlkCode"
            ddlBlock.DataTextField = "_Description"
            ddlBlock.DataBind()
            ddlBlock.SelectedIndex = intSelectedIndex

    End Sub

    Sub BindVeh(ByVal pv_strVehCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objVehDs as Object

        pv_strVehCode = Trim(pv_strVehCode)

        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"

            strParam = "|LocCode = '" & Trim(strLocation) & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'"
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   objVehDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        dr = objVehDs.Tables(0).NewRow()
        dr("VehCode") = ""
        dr("_Description") = "Please select Vehicle Code"
        objVehDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVeh.DataSource = objVehDs.Tables(0)
        ddlVeh.DataValueField = "VehCode"
        ddlVeh.DataTextField = "_Description"
        ddlVeh.DataBind()
        ddlVeh.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindVehExp(ByVal pv_strVehExpCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By VehExpenseCode ASC| And Veh.Status = '" & objGLSetup.EnumVehExpenseStatus.Active & "' "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objVehExpDs as Object

        pv_strVehExpCode = Trim(pv_strVehExpCode)

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.VehicleExpense, _
                                                   objVehExpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_PIECEATTDTRX_VEHEXPENSE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try


        dr = objVehExpDs.Tables(0).NewRow()
        dr("VehExpenseCode") = ""
        dr("_Description") = "Please Select Vehicle Expense Code"
        objVehExpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehExp.DataSource = objVehExpDs.Tables(0)
        ddlVehExp.DataValueField = "VehExpenseCode"
        ddlVehExp.DataTextField = "_Description"
        ddlVehExp.DataBind()
        ddlVehExp.SelectedIndex = intSelectedIndex
    End Sub


    Sub BindDendaCode(Byval strSelectedVehicleCode as string)
        Dim strOpCd As String = "PR_CLSSETUP_DENDA_SEARCH"
        Dim dr As DataRow
        Dim strParam As String = "Order By D.DendaCode ASC|And D.Status = '" & objPRSetup.EnumDendaStatus.Active & "' And D.LocCode = '" & strLocation & "'  "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objDendaDs as Object

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objPRSetup.EnumPayrollMasterType.Route, _
                                                   objDendaDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_DENDA_SEARCH&errmesg=" & Exp.ToString & "&redirect=PR/trx/PR_trx_TripList.aspx")
        End Try

        dr = objDendaDs.Tables(0).NewRow()
        dr("DendaCode") = ""
        dr("_Description") = "Please Select Denda Code"
        objDendaDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDendaCode.DataSource = objDendaDs.Tables(0)
        ddlDendaCode.DataValueField = "DendaCode"
        ddlDendaCode.DataTextField = "_Description"
        ddlDendaCode.DataBind()
        ddlDendaCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub onsubmit_checkcode()
        Dim arrItemValue As Array
        open_script()
        opener_findtextbox()
        If trAcc.Visible = True Then
            onload_printfunc()
            onload_closefunc()
            opener_dropdownval(strAcc, ddlAccount.SelectedItem.Value)
            onclick_reload(strAcc, ddlAccount.SelectedItem.Value, False, False)
        end if
        If trSubBlk.Visible = True Then
            onload_printfunc()
            onload_closefunc()
            opener_dropdownval(strSubBlk, ddlSubBlock.SelectedItem.Value)
            onclick_reload(strSubBlk, ddlSubBlock.SelectedItem.Value, False, False)
        end if
        If trBlk.Visible = True Then
            onload_printfunc()
            onload_closefunc()
            opener_dropdownval(strBlk, ddlBlock.SelectedItem.Value)
            onclick_reload(strBlk, ddlBlock.SelectedItem.Value, False, False)
        end if
        If trVeh.Visible = True Then
            onload_printfunc()
            onload_closefunc()
            opener_dropdownval(strVeh, ddlVeh.SelectedItem.Value)
            onclick_reload(strVeh, ddlVeh.SelectedItem.Value, False, False)
        end if
        If trVehExp.Visible = True Then
            onload_printfunc()
            onload_closefunc()
            opener_dropdownval(strVehExp, ddlVehExp.SelectedItem.Value)
            onclick_reload(strVehExp, ddlVehExp.SelectedItem.Value, False, False)
        end if
        If trDenda.Visible = True Then
            onload_printfunc()
            onload_closefunc()
            opener_dropdownval(strDendaCode, ddlDendaCode.SelectedItem.Value)
            onclick_reload(strDendaCode, ddlDendaCode.SelectedItem.Value, False, False)
        end if
        close_script()
        print_script()
    end sub
End Class
