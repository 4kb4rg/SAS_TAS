Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class GL_RunningHour_Det : Inherits Page

    Protected WithEvents dgStkTx As DataGrid
    Protected WithEvents lblTxID As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblSubBlockErr As Label
    Protected WithEvents lblSubBlockTag As Label
    Protected WithEvents lblReprint As Label
    Protected WithEvents ddlSubBlock As DropDownList
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents txtRunHour As TextBox
    Protected WithEvents txtDate As TextBox
    Protected WithEvents tblAdd As HtmlTable
    Protected WithEvents lblError As Label
    Protected WithEvents Add As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Print As ImageButton
    Protected WithEvents Cancel As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents btnBack As ImageButton

    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblPleaseSpecify As Label
    Protected WithEvents lblTotRunHour As Label
    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowBlock As HtmlTableRow
    Protected WithEvents RowSubBlock As HtmlTableRow
    Protected WithEvents lblBlockErr As Label
    Protected WithEvents lblBlockTag As Label
    Protected WithEvents lblErrDupl As Label
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents RunHourID As HtmlInputHidden
    Dim BlockTag As String

    Protected WithEvents txtRunFrom As TextBox
    Protected WithEvents txtRunTo As TextBox

    Protected objGLTrx As New agri.GL.clstrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objPRSetup As New agri.PR.clsSetup()

    Dim strOpCdStckTxDet_ADD As String = "GL_CLSTRX_RUNNINGHOUR_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "GL_CLSTRX_RUNNINGHOUR_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "GL_CLSTRX_RUNNINGHOUR_LINE_GET"

    Dim objTrxDs As New DataSet()
    Dim dsGrid As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strDateFMT As String
    Dim intGLAR As Integer
    Dim intConfigsetting As Integer
    Dim SubBlockTag As String
    Dim strRunHour As String
    Dim intStatus As Integer
    Dim strID As String 
    Dim strAcceptFormat As String

    Protected WithEvents lblLocCodeErr As Label
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strDateFMT = Session("SS_DATEFMT")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLRunHour), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            strID = Request.QueryString("Id")
            intStatus = Convert.ToInt32(lblHiddenSts.Text)

            lblErrDupl.Visible = False
            lblBlockErr.Visible = False
            lblSubBlockErr.Visible = False
            If Not Page.IsPostBack Then
                BindChargeLevelDropDownList()
                If Not strID = "" Then
                    RunHourID.Value = strID
                    onLoad_Display()
                    onLoad_DisplayLine()
                Else
                    txtDate.Text = objGlobal.GetShortDate(strDateFMT, Now())
                End If

                BindBlock()
                BindSubBlock()
                PageControl()
            End If
            lblError.Visible = False
            lblDate.Visible = False
            lblFmt.Visible = False
            lblBlockErr.Visible = False
            lblSubBlockErr.Visible = False
        End If
    End Sub

    Sub onLoad_Display()
        Dim objTrxDs As New Dataset
        Dim strOpCd As String = "GL_CLSTRX_RUNNINGHOUR_GET"
        Dim strParam As String = strID
        Dim intErrNo As Integer

        Try
            intErrNo = objGLTrx.mtdGetRunningHourTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objTrxDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_RUNNINGHOUR_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        RunHourID.Value = strID
        lblTxID.Text = strID
        txtDate.Text = objGlobal.GetShortDate(strDateFMT, objTrxDs.Tables(0).Rows(0).Item("TransactDate"))    
        intStatus = Convert.ToInt32(objTrxDs.Tables(0).Rows(0).Item("Status"))
        lblHiddenSts.Text = objTrxDs.Tables(0).Rows(0).Item("Status").Trim()
        Status.Text = objGLTrx.mtdGetRunningHourStatus(Convert.ToInt16(objTrxDs.Tables(0).Rows(0).Item("Status")))
        CreateDate.Text = objGlobal.GetLongDate(objTrxDs.Tables(0).Rows(0).Item("CreateDate"))
        UpdateDate.Text = objGlobal.GetLongDate(objTrxDs.Tables(0).Rows(0).Item("UpdateDate"))
        UpdateBy.Text = objTrxDs.Tables(0).Rows(0).Item("UserName")
        objTrxDs = Nothing
    End Sub

    Sub onLoad_DisplayLine()
        Dim objTrxLnDs As New Dataset()
        Dim strOpCd As String = "GL_CLSTRX_RUNNINGHOUR_LINE_GET"
        Dim strParam As String = strID
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim dblRunHour As Integer = 0
        Try
            intErrNo = objGLTrx.mtdGetRunningHourTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objTrxLnDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_RUNNINGHOUR_LINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dgStkTx.DataSource = objTrxLnDs.Tables(0)
        dgStkTx.DataBind()
        
        For intCnt = 0 To objTrxLnDs.Tables(0).Rows.Count - 1
            dblRunHour += objTrxLnDs.Tables(0).Rows(intCnt).Item("RunHour")
        Next

        lblTotRunHour.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(dblRunHour, 2)
        If objTrxLnDs.Tables(0).Rows.Count > 0 Then
            txtDate.Enabled = False
            ddlChargeLevel.Enabled = False 
        End If

        If Status.Text = objGLTrx.mtdGetRunningHourStatus(objGLTrx.EnumRunningHourStatus.Active) Then
            For intCnt = 0 To dgStkTx.Items.Count - 1
                lbButton = dgStkTx.Items.Item(intCnt).FindControl("Delete")
                lbButton.Visible = True
                lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Next
        Else
            For intCnt = 0 To dgStkTx.Items.Count - 1
                lbButton = dgStkTx.Items.Item(intCnt).FindControl("Delete")
                lbButton.Visible = False
            Next
        End If
    End Sub

    Sub PageControl()
        If Status.Text = objGLTrx.mtdGetRunningHourStatus(objGLTrx.EnumRunningHourStatus.Cancelled) Then
            Save.Visible = False
            btnNew.Visible = True
            Select Case Status.Text
                Case objGLTrx.mtdGetRunningHourStatus(objGLTrx.EnumRunningHourStatus.Cancelled)
                    Cancel.Visible = False
            End Select
        Else
            Save.Visible = True
            btnNew.Visible = False
            Print.Visible = False
            If lblTxID.Text <> "" Then
                btnNew.Visible = True
                Cancel.Visible = True
            Else
                btnNew.Visible = False
                Print.Visible = False
            End If

        End If
        DisableItemTable()
    End Sub

    Sub DisableItemTable()

        If Status.Text = objGLTrx.mtdGetRunningHourStatus(objGLTrx.EnumRunningHourStatus.Cancelled) Then
            tblAdd.Visible = False
        ElseIf Status.Text = objGLTrx.mtdGetRunningHourStatus(objGLTrx.EnumRunningHourStatus.Active) Then
            tblAdd.Visible = True
        End If

    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(SubBlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.selectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub
    
    Sub ddlChargeLevel_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        ToggleChargeLevel()
    End Sub
    
    Sub ToggleChargeLevel()
        If ddlChargeLevel.selectedIndex = 0 Then
            RowSubBlock.Visible = False
            RowBlock.Visible = True
            hidBlockCharge.value = "yes"
        Else
            RowSubBlock.Visible = True
            RowBlock.Visible = False
            hidBlockCharge.value = ""
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblBlockTag.Text = BlockTag & lblCode.Text & " : "
        lblBlockErr.Text = lblPleaseSelect.Text & BlockTag & lblCode.Text

        SubBlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
        lblSubBlockTag.Text = SubBlockTag & lblCode.Text
        lblSubBlockErr.Text = lblPleaseSelect.Text & SubBlockTag & lblCode.Text

        If ddlChargeLevel.selectedIndex = 0 Then
            dgStkTx.Columns(1).HeaderText = BlockTag & lblCode.Text
            dgStkTx.Columns(2).HeaderText = BlockTag & " Description"    
        Else
            dgStkTx.Columns(1).HeaderText = SubBlockTag & lblCode.Text
            dgStkTx.Columns(2).HeaderText = SubBlockTag & " Description"
        End If
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_VEHUSAGEDET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/RunningHour_details.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

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
    End Function

    Sub BindBlock()
        Dim strOpCd As String
        Dim dr As DataRow
        Dim objBlockDs As DataSet
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim SelectedIndex As Integer
        
        strOpCd = "GL_CLSSETUP_BLOCK_LIST_GET"
        Try
            strParam = "|" & " And blk.LocCode = '" & Trim(strLocation) & "' And blk.Status = '" & objGLSetup.EnumBlockStatus.Active & "'" & "|" & strLocation
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objBlockDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBlockDs.Tables(0).Rows.Count - 1
            objBlockDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objBlockDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objBlockDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBlockDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objBlockDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBlockDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(Request.Form("ddlBlock")) Then
                SelectedIndex = intCnt + 1
            End If
        Next

        dr = objBlockDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & BlockTag & lblCode.Text

        objBlockDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlBlock.DataSource = objBlockDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = SelectedIndex

        If Not objBlockDs Is Nothing Then
            objBlockDs = Nothing
        End If
    End Sub

    Sub BindSubBlock()
        Dim strOpCd As String
        Dim objSubBlockDs As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim strParam As String
        Dim dr As DataRow

        strOpCd = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Try
            strParam = "|" & "And sub.LocCode = '" & Trim(strLocation) & "' And sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "'" & "|" & strLocation
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objSubBlockDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objSubBlockDs.Tables(0).Rows.Count - 1
            objSubBlockDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objSubBlockDs.Tables(0).Rows(intCnt).Item("SubBlkCode"))
            objSubBlockDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objSubBlockDs.Tables(0).Rows(intCnt).Item("SubBlkCode")) & " (" & Trim(objSubBlockDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objSubBlockDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(Request.Form("ddlSubBlock")) Then
                SelectedIndex = intCnt + 1
            End If

        Next

        dr = objSubBlockDs.Tables(0).NewRow()
        dr("SubBlkCode") = ""
        dr("Description") = lblSelect.Text & SubBlockTag & lblCode.Text

        objSubBlockDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlSubBlock.DataSource = objSubBlockDs.Tables(0)
        ddlSubBlock.DataValueField = "SubBlkCode"
        ddlSubBlock.DataTextField = "Description"
        ddlSubBlock.DataBind()
        ddlSubBlock.SelectedIndex = SelectedIndex

        If Not objSubBlockDs Is Nothing Then
            objSubBlockDs = Nothing
        End If
    End Sub

    Function CheckDate(ByVal pv_strDate As String, ByRef pr_strDate As String) As Boolean
        Dim strDateFormatCode As String = Session("SS_DATEFMT")
        Dim strDateFormat As String

        pr_strDate = ""
        CheckDate = True
        If Not pv_strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormatCode, pv_strDate, strDateFormat, pr_strDate) = False Then
                lblFmt.Text = strDateFormat
                pr_strDate = ""
                CheckDate = False
            End If
        End If
    End Function

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcceptFormat As String

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim objTrxDs As New Dataset()
        Dim objFound As Boolean
        Dim blnIsUpdated As Boolean
        Dim strOpCode_GetLine As String = "GL_CLSTRX_RUNNINGHOUR_LINEDUPL_GET"
        Dim strOpCode_AddLine As String = "GL_CLSTRX_RUNNINGHOUR_LINE_ADD"
        Dim strOpCode_UpdID As String = "GL_CLSTRX_RUNNINGHOUR_STATUS_UPD"
        Dim strOpCode_ItmLifespan As String = "GL_CLSTRX_RUNNINGHOUR_ITEMTOMACHINE_ADD"
        Dim strBlkCode As String 
        Dim strParam As String
        Dim intErrNo As Integer
        Dim BlkType As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strBlkCode = Iif(ddlChargeLevel.selectedIndex=0,ddlBlock.SelectedItem.Value,ddlSubBlock.SelectedItem.Value)   
        strID = Trim(lblTxID.Text)
        InsertRecord(blnIsUpdated)
        If strID = "" Then
            Exit Sub
        End If

        If strBlkCode = "" Then
            If ddlChargeLevel.SelectedIndex=0 Then
                lblBlockErr.Visible = True
            Else
                lblSubBlockErr.Visible = True
            End If
            Exit Sub
        End If
        
        BlkType = Iif(ddlChargeLevel.selectedIndex=0, "BLKCODE", "SUBBLKCODE")
        strParam = "|" & _
                   "RH.Status = '" & objGLTrx.EnumRunningHourStatus.Active & "' And RHL.TransactDate = '" & Date_Validation(txtDate.Text,False) & "'" & "|" & _
                   BlkType & "='" & Trim(strBlkCode) & "'"
        Try
            intErrNo = objGLTrx.mtdGetRunningHourTrxLine(strOpCode_GetLine, _
                                            strParam, _
                                            objTrxDs, _
                                            False)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_RUNNINGHOURLN_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objTrxDs.Tables(0).Rows.Count > 0 Then
            lblErrDupl.Visible = True
            Exit Sub
        Else
            txtRunHour.Text = CDbl(txtRunTo.Text) - CDbl(txtRunFrom.Text)
            strParam = strID & "|" & _
                        strDate & "|" & _
                        strBlkCode & "|" & _
                        Trim(txtRunHour.Text) & "|" & _
                        objGLTrx.EnumRunningHourStatus.Active & "|" & _
                        Trim(txtRunFrom.Text) & "|" & _
                        Trim(txtRunTo.Text)

            Try
                intErrNo = objGLTrx.mtdUpdRunningHourTrxLine(strOpCode_UpdID, _
                                                            strOpCode_AddLine, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.RunningHour), _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            False, _
                                                            objTrxDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_RUNNINGHOUR_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=gl/trx/GL_clstrx_RunninhHour_det.aspx?ActRunningHourID=" & lblTxID.Text)
            End Try
        End If
        
        strParam = strID & "|" & _
                   Date_Validation(txtDate.Text,False) & "|" & _
                   objGLTrx.EnumRunningHourStatus.Active  
        Try
            intErrNo = objGLTrx.mtdUpdItemLifespan(strOpCode_ItmLifespan, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            False, _
                                            objTrxDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_ITEMTOMACHINE_ADD&errmesg=" & Exp.ToString() & "&redirect=gl/trx/GL_clstrx_RunningHour_det.aspx")
        End Try

        If strID <> "" Then
            onLoad_Display()
            onLoad_DisplayLine()
            PageControl()
        End If
  
        objTrxDs = Nothing
    End Sub

    Sub InsertRecord(ByRef GL_blnIsUpdated As Boolean)
        Dim objTrxDs As New Dataset()
        Dim objTrxID As String
        Dim strOpCd_Add As String = "GL_CLSTRX_RUNNINGHOUR_ADD"
        Dim strOpCd_Get As String = "GL_CLSTRX_RUNNINGHOUR_GET"
        Dim strOpCd_Sts As String = "GL_CLSTRX_RUNNINGHOUR_STATUS_UPD"
        Dim strOpCd As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strNewIDFormat As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strID = Trim(lblTxID.Text)
        strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Sts)

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        strNewIDFormat = "RHO" & "/" & strCompany & "/" & strLocation & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.RunningHour) & "|" & _
                    strID & "|" & _
                    strDate & "|" & _
                    objGLTrx.EnumRunningHourStatus.Active & "|" & _
                    strNewIDFormat
           
        Try
            intErrNo = objGLTrx.mtdUpdRunningHourTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            False, _
                                            objTrxID)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_RUNNINGHOUR_SAVE&errmesg=" & Exp.ToString() & "&redirect=gl/trx/GL_clstrx_RunningHour_det.aspx")
        End Try

        strID = objTrxID
        RunHourID.Value = strID
        GL_blnIsUpdated = True

        objTrxDs = Nothing
    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim blnIsUpdated As Boolean

        InsertRecord(blnIsUpdated)

        If strID <> "" Then
            onLoad_Display()
            onLoad_DisplayLine()
            PageControl()
            IsLifeTime
        End If
    End Sub

    Sub IsLifeTime()
        Dim objItemDs As New DataSet
        Dim strOpCode As String = "GL_CLSTRX_REMAININGLIFETIME_SEARCH"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strSortExp As String
        Dim intStatusPopUp As Integer

        strSortExp = " ORDER BY ItemCode ASC"

        strParamName = "LOCCODE"
        strParamValue = "" & strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Try
            intStatusPopUp = objItemDs.Tables(0).Rows(0).Item("IsPopUp")
            If intStatusPopUp <> 0 Then
                If objItemDs.Tables(0).Rows.Count > 0 Then
                    Response.Write("<Script Language=""JavaScript"">window.open(""../../include/Util/PopUpRemainLifeTimeScreen.aspx?Type=Print&CompName=" & strCompany & _
                                    """,null ,""status=yes,width=600,height=350,top=200,left=250, resizable=1, scrollbars=yes, toolbar=no, location=no"");</Script>")
                End If
            End If
        Catch ex As Exception
        Finally
        End Try

        objItemDs = Nothing
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "GL_CLSTRX_RUNNINGHOUR_LINE_DEL"
        Dim strOpCode_UpdID As String = "GL_CLSTRX_RUNNINGHOUR_STATUS_UPD"
        Dim strOpCode_ItmLifespan As String = "GL_CLSTRX_RUNNINGHOUR_ITEMTOMACHINE_CANCELLED"
        Dim strOpCdStckTxLine_DEL As String = "GL_CLSTRX_RUNNINGHOUR_LINE_DEL"
        Dim strParam As String
        Dim lblBlk As Label
        Dim strBlock As String
        Dim intErrNo As Integer
        Dim lbl As Label
        Dim id As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        dgStkTx.EditItemIndex = Convert.ToInt32(E.Item.ItemIndex)
        lblBlk = dgStkTx.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("BlkCode")
        strBlock = lblBlk.Text

        strID = Trim(lblTxID.Text)
        lbl = E.Item.FindControl("lblID")
        ID = lbl.Text

        strParam = id & "|" & strID
        Try
            intErrNo = objGLTrx.mtdDelTransactLn(strOpCdStckTxLine_DEL, strParam)
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_RUNHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_VehicleUsage_List.aspx")
            End If
        End Try

        strParam = strID & "|" & _
                   strDate & "|" & _
                   objGLTrx.EnumRunningHourStatus.Active
        Try
            intErrNo = objGLTrx.mtdUpdItemLifespan(strOpCode_ItmLifespan, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            False, _
                                            objTrxDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_ITEMTOMACHINE_ADD&errmesg=" & Exp.ToString() & "&redirect=gl/trx/GL_clstrx_RunningHour_det.aspx")
        End Try

        strID = lblTxID.Text
        onLoad_Display()
        onLoad_DisplayLine()
        PageControl()
    End Sub

    Sub btnPrint_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

    End Sub

    Sub btnCancel_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim objTrxDs As New Dataset()
        Dim objTrxID As String
        Dim strOpCd_Sts As String = "GL_CLSTRX_RUNNINGHOUR_STATUS_UPD"
        Dim strOpCode_ItmLifespan As String = "GL_CLSTRX_RUNNINGHOUR_ITEMTOMACHINE_CANCELLED"
        Dim strOpCd As String
        Dim intErrNo As Integer
        Dim strParam As String 
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

        If txtDate.Text = "" Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If
        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strID = Trim(lblTxID.Text)
        strParam = strID & "|" & _
                   strDate & "|" & _
                   objGLTrx.EnumRunningHourStatus.Active
        Try
            intErrNo = objGLTrx.mtdUpdItemLifespan(strOpCode_ItmLifespan, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            False, _
                                            objTrxDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_ITEMTOMACHINE_ADD&errmesg=" & Exp.ToString() & "&redirect=gl/trx/GL_clstrx_RunningHour_det.aspx")
        End Try
        
        strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.RunningHour) & "|" & _
                    lblTxID.Text & "||" & _
                    objGLTrx.EnumRunningHourStatus.Cancelled & "|"
           
        Try
            intErrNo = objGLTrx.mtdUpdRunningHourTrx(strOpCd_Sts, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            True, _
                                            objTrxID)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSTRX_RUNNINGHOUR_SAVE&errmesg=" & Exp.ToString() & "&redirect=gl/trx/GL_clstrx_RunningHour_det.aspx")
        End Try

        onLoad_Display()
        onLoad_DisplayLine()
        PageControl()
        objTrxDs = Nothing
    End Sub


    Sub btnNew_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("GL_Trx_RunningHour_details.aspx")
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../GL/Trx/GL_Trx_RunningHour_List.aspx")
    End Sub


End Class
