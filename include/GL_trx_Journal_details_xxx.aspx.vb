
Imports System
Imports System.Collections
Imports System.Data
Imports System.Data.SqlClient
Imports System.Math
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Public Class GL_Journal_Det : Inherits Page

    Protected WithEvents dgTx As DataGrid
    Protected WithEvents lblTxID As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblBlkTag As Label
    Protected WithEvents lblVehTag As Label
    Protected WithEvents lblVehExpTag As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblReprint As Label
    Protected WithEvents ddlReceiveFrom As DropDownList
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents lstVehExp As DropDownList
    Protected WithEvents lstVehCode As DropDownList
    Protected WithEvents lstBlock As DropDownList
    Protected WithEvents lstTxType As DropDownList
    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblTotAmtFig As Label
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtRefNo As TextBox
    Protected WithEvents txtAmt As TextBox
    Protected WithEvents txtDescLn As TextBox
    Protected WithEvents txtQty As TextBox
    Protected WithEvents txtQtyCR As TextBox
    Protected WithEvents txtPrice As TextBox
    Protected WithEvents txtPriceCR As TextBox
    Protected WithEvents txtDRTotalAmount As TextBox
    Protected WithEvents txtCRTotalAmount As TextBox
    Protected WithEvents TAmt As Label
    Protected WithEvents tblAdd As HtmlTable
    Protected WithEvents lblError As Label
    Protected WithEvents lblStock As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents lblBlockErr As Label
    Protected WithEvents lblVehCodeErr As Label
    Protected WithEvents lblVehExpCodeErr As Label
    Protected WithEvents lblDescErr As Label
    Protected WithEvents lblStsHid As Label
    Protected WithEvents lblCtrlAmtFig As Label
    Protected WithEvents lblTwoAmount As Label
    Protected WithEvents Add As ImageButton
    Protected WithEvents Update As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Print As ImageButton
    Protected WithEvents Cancel As ImageButton
    Protected WithEvents Back As ImageButton
    Protected WithEvents NewBtn As ImageButton
    Protected WithEvents btnSelDate As Image
    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents blnShortCut As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblTxLnID As Label
    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlk As HtmlTableRow
    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents lblPreBlockErr As Label
    Protected WithEvents lblPreBlkTag As Label
    Protected WithEvents ddlPreBlock As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Dim PreBlockTag As String
    Dim BlockTag As String
    Protected WithEvents RowChargeTo As HtmlTableRow
    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents lblLocationErr As Label

    Protected WithEvents hidCtrlAmtFig As HtmlInputHidden
    Protected WithEvents hidDBCR As HtmlInputHidden
    Protected WithEvents hidDBAmt As HtmlInputHidden
    Protected WithEvents hidCRAmt As HtmlInputHidden
    Protected WithEvents hidTtlDBAmt As HtmlInputHidden
    Protected WithEvents hidTtlCRAmt As HtmlInputHidden

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbViewJournal As LinkButton
    Protected WithEvents dgViewJournal As DataGrid
    Protected WithEvents lblTotalDB As Label
    Protected WithEvents lblTotalCR As Label
    Protected WithEvents lblTotalViewJournal As Label

    Protected objGLtx As New agri.GL.ClsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objGLtrx As New agri.GL.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objINTrx As New agri.IN.clsTrx()

    Dim strOpCdStckTxDet_ADD As String = "GL_CLSTRX_JOURNAL_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "GL_CLSTRX_JOURNAL_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "GL_CLSTRX_JOURNAL_LINE_GET"
    Dim strOpCdStckTxLine_UPD As String = "GL_CLSTRX_JOURNAL_LINE_UPD"
    Dim strOpCdAccCode_GET As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"

    Dim objDataSet As New DataSet()
    Dim dsGrid As New DataSet()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strDateFMT As String
    Dim intConfigsetting As Integer

    Dim strAccountTag As String
    Dim strVehTag As String
    Dim strBlockTag As String
    Dim strVehExpTag As String

    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim dblTotDocAmt As Double

    Protected WithEvents lblBPErr As Label
    Protected WithEvents lblLocCodeErr As Label
	
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strLocType As String
    Dim intLevel As Integer
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strDateFMT = Session("SS_DATEFMT")
        intGLAR = Session("SS_GLAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        txtCRTotalAmount.Attributes.Add("onfocus", "gotFocusCR()")
        txtDRTotalAmount.Attributes.Add("onfocus", "gotFocusDR()")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            ddlLocation.Enabled = False
            onload_GetLangCap()
            lblTwoAmount.Visible = False
            lblError.Visible = False
            lblDate.Visible = False
            lblFmt.Visible = False
            lblLocationErr.Visible = False
            lblAccCodeErr.Visible = False
            lblPreBlockErr.Visible = False
            lblBlockErr.Visible = False
            lblVehCodeErr.Visible = False
            lblVehExpCodeErr.Visible = False
            lblDescErr.Visible = False
            lblBPErr.Visible = False
            lblLocCodeErr.Visible = False
            TrLink.Visible = False        

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            Add.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Add).ToString())
            NewBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NewBtn).ToString())
            Save.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Save).ToString())
            Cancel.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Cancel).ToString())
            Print.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Print).ToString())
            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())

            If Not Page.IsPostBack Then
                BindLocationDropDownList(Trim(Session("SS_LOCATION")))
                RowChargeTo.Visible = Session("SS_INTER_ESTATE_CHARGING")
                BindChargeLevelDropDownList()
                Trace.Warn("postback")
                txtDate.Text = "" 'objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                lblTxID.Text = Request.QueryString("Id")
                If Not Request.QueryString("Id") = "" Then
                    LoadStockTxDetails()
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        DisplayFromDB()
                        BindGrid()                    
                    End If
                End If
                BindAccCodeDropList()
                BindPreBlock("", "")
                BindBlockDropList("")
                BindVehicleCodeDropList("")
                BindVehicleExpDropList(True)
                BindTxTypeList()
                PageControl()

            End If
        End If
    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.selectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub
    
    Sub ddlChargeLevel_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        ToggleChargeLevel()
    End Sub
    
    Sub ToggleChargeLevel()
        If ddlChargeLevel.selectedIndex = 0 Then
            RowBlk.Visible = False
            RowPreBlk.Visible = True
            hidBlockCharge.value = "yes"
        Else
            RowBlk.Visible = True
            RowPreBlk.Visible = False
            hidBlockCharge.value = ""
        End If
    End Sub
    Sub ddlLocation_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strAccCode As String = lstAccCode.SelectedItem.Value.Trim()
        Dim strVehCode As String = lstVehCode.SelectedItem.Value.Trim()
        Dim strPreBlkCode As String = ddlPreBlock.SelectedItem.Value.Trim()
        Dim strBlkCode As String = lstBlock.SelectedItem.Value.Trim()
        BindVehicleCodeDropList(strAccCode, strVehCode)
        BindBlockDropList(strAccCode, strBlkCode)
        BindPreBlock(strAccCode, strPreBlkCode)
        hidChargeLocCode.value = ddlLocation.SelectedItem.Value.Trim()
    End Sub

    Sub BindLocationDropDownList(ByVal pv_strLocCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim dsLoc As DataSet
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        strOpCd = "ADMIN_CLSLOC_INTER_ESTATE_LOCATION_GET"
        intSelectedIndex = 0
        Try
            strParam = objAdminLoc.EnumLocStatus.Active & "|" & _
                       Trim(Session("SS_COMPANY")) & "|" & _
                       Trim(Session("SS_LOCATION")) & "|" & _
                       Trim(Session("SS_USERID")) & "|" & _
                       objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.GeneralLedger) & "|" & _
                       "GLAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal) & "|" & _
                       "GLAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal)
            intErrNo = objAdminLoc.mtdGetInterEstateLoc(strOpCd, _
                                                        strParam, _
                                                        dsLoc)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsLoc.Tables(0).Rows.Count - 1
            dsLoc.Tables(0).Rows(intCnt).Item("LocCode") = Trim(dsLoc.Tables(0).Rows(intCnt).Item("LocCode"))
            dsLoc.Tables(0).Rows(intCnt).Item("Description") = Trim(dsLoc.Tables(0).Rows(intCnt).Item("Description"))
            If dsLoc.Tables(0).Rows(intCnt).Item("LocCode") = Trim(pv_strLocCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next
        
        If Trim(pv_strLocCode) <> "" And intSelectedIndex = 0 Then
            dr = dsLoc.Tables(0).NewRow()
            dr("LocCode") = Trim(pv_strLocCode)
            dr("Description") = Trim(pv_strLocCode) & " (Deleted)"
            dsLoc.Tables(0).Rows.InsertAt(dr, 0)
            intSelectedIndex = 1
        End If

        dr = dsLoc.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelect.Text & GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text

        dsLoc.Tables(0).Rows.InsertAt(dr, 0)
        ddlLocation.DataSource = dsLoc.Tables(0)
        ddlLocation.DataValueField = "LocCode"
        ddlLocation.DataTextField = "Description"
        ddlLocation.DataBind()
        ddlLocation.SelectedIndex = intSelectedIndex

        If Not dsLoc Is Nothing Then
            dsLoc = Nothing
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlockTag = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                strBlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNAL_DETAILS_GET_COSTLEVEL_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/journal_details.aspx")
        End Try


        strAccountTag = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        strVehExpTag = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

        lblAccCodeTag.Text = strAccountTag & " :* "
        lblBlkTag.Text = strBlockTag & " : "
        lblVehTag.Text = strVehTag & " : "
        lblVehExpTag.Text = strVehExpTag & " :"

        lblAccCodeErr.Text = "<BR>" & lblPleaseSelect.Text & strAccountTag
        lblVehCodeErr.Text = lblPleaseSelect.Text & strVehTag
        lblBlockErr.Text = lblPleaseSelect.Text & strBlockTag
        lblVehExpCodeErr.Text = lblPleaseSelect.Text & strVehExpTag

        dgTx.Columns(3).HeaderText = strAccountTag
        dgTx.Columns(5).HeaderText = strBlockTag
        dgTx.Columns(6).HeaderText = strVehTag
        dgTx.Columns(7).HeaderText = strVehExpTag

        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTag.Text = PreBlockTag & lblCode.Text & " : "
        lblPreBlockErr.Text = lblPleaseSelect.Text & PreBlockTag & lblCode.Text
        lblLocationErr.Text = lblPleaseSelect.Text & GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNAL_DETAIL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/journal_details.aspx")
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


    Sub PageControl()
        txtDesc.Enabled = False
        lstTxType.Enabled = False
        ddlReceiveFrom.Enabled = False
        txtRefNo.Enabled = False
        txtDate.Enabled = False
        btnSelDate.Visible = False
        txtAmt.Enabled = False

        txtDescLn.Enabled = False
        lstAccCode.Enabled = False
        lstBlock.Enabled = False
        lstVehCode.Enabled = False
        lstVehExp.Enabled = False
        txtDRTotalAmount.Enabled = False
        txtCRTotalAmount.Enabled = False

        Add.Enabled = False
        Save.Visible = False
        Print.Visible = False
        Cancel.Visible = False
        tblAdd.Visible = False
        
        Select Case lblStsHid.Text
            Case CStr(objGLtx.EnumJournalStatus.Cancelled), _
                 CStr(objGLtx.EnumJournalStatus.Posted), _
                 CStr(objGLtx.EnumJournalStatus.Closed)
                
            Case Else
                txtDesc.Enabled = True
                lstTxType.Enabled = True
                ddlReceiveFrom.Enabled = True
                txtRefNo.Enabled = True
                txtDate.Enabled = True
                btnSelDate.Visible = True
                txtAmt.Enabled = False
                
                txtDescLn.Enabled = True
                lstAccCode.Enabled = True
                lstBlock.Enabled = True
                lstVehCode.Enabled = True
                lstVehExp.Enabled = True
                txtDRTotalAmount.Enabled = True
                txtCRTotalAmount.Enabled = True

                Add.Enabled = True
                Save.Visible = True
                If lblTxID.Text.Trim() <> "" Then
                    Cancel.Visible = True
                    txtDate.Enabled = False
                End If
                tblAdd.Visible = True
        End Select

        Select Case Trim(Replace(Replace(Replace(lblCtrlAmtFig.Text, ",", ""), ",", "."), "-", ""))
            Case "0.00", "0,00", "0", ""
                Back.Attributes("onclick") = ""
                NewBtn.Attributes("onclick") = ""
                Save.Attributes("onclick") = ""
            Case Else
                Back.Attributes("onclick") = "javascript:return ConfirmAction('exit while amount in current transaction is not balance. Difference = " & Trim(lblCtrlAmtFig.Text) & " ');"
                Save.Attributes("onclick") = "javascript:return ConfirmAction('save while amount in current transaction is not balance. Difference = " & Trim(lblCtrlAmtFig.Text) & " ');"
                NewBtn.Attributes("onclick") = "javascript:return ConfirmAction('create new transaction while amount in current transaction is not balance. Difference = " & Trim(lblCtrlAmtFig.Text) & " ');"
        End Select

    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)

        If dgTx.Enabled = True Then
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim lbl As Label
                    Dim lblAmt As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    Dim DeleteButton As LinkButton
                    Dim EditButton As LinkButton
                    Select Case Status.Text.Trim
                        Case objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Active)
                            DeleteButton = e.Item.FindControl("Delete")
                            DeleteButton.Visible = True
                            DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                            EditButton = e.Item.FindControl("Edit")
                            EditButton.Visible = True
                        Case objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Cancelled), _
                              objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Posted), _
                              objGLtrx.mtdGetJournalStatus(objGLtrx.EnumJournalStatus.Closed)
                            DeleteButton = e.Item.FindControl("Delete")
                            DeleteButton.Visible = False
                            EditButton = e.Item.FindControl("Edit")
                            EditButton.Visible = False
                    End Select

                    lblAmt = e.Item.FindControl("lblAmount")
                    lbl = e.Item.FindControl("lblAccTx")

                    If Sign(CDbl(Replace(Replace(lblAmt.Text.Trim, ",", ""), ",", "."))) = 1 Then
                        lbl.Text = "DR"
                    ElseIf Sign(CDbl(Replace(Replace(lblAmt.Text.Trim, ",", ""), ",", "."))) = -1 Then
                        lbl.Text = "CR"
                    End If
                Case ListItemType.EditItem
                    Dim lbl As Label
                    Dim lblAmt As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    lblAmt = e.Item.FindControl("lblAmount")
                    lbl = e.Item.FindControl("lblAccTx")
            

                    If Sign(CDbl(Replace(Replace(lblAmt.Text.Trim, ",", ""), ",", "."))) = 1 Then
                        lbl.Text = "DR"
                    ElseIf Sign(CDbl(Replace(Replace(lblAmt.Text.Trim, ",", ""), ",", "."))) = -1 Then
                        lbl.Text = "CR"
                    End If
            End Select

        Else
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim lbl As Label
                    lbl = e.Item.FindControl("lblIdx")
                    lbl.Text = e.Item.ItemIndex.ToString + 1

                    Dim DeleteButton As LinkButton
                    DeleteButton = e.Item.FindControl("Delete")
                    DeleteButton.Enabled = False
            End Select
        End If

    End Sub


    Sub DisplayFromDB()
        ddlReceiveFrom.SelectedIndex = CInt(objDataSet.Tables(0).Rows(0).Item("ReceiveFrom")) - 1
        lblAccPeriod.Text = objDataSet.Tables(0).Rows(0).Item("AccMonth") & "/" & objDataSet.Tables(0).Rows(0).Item("AccYear")
        Status.Text = objGLtx.mtdGetJournalStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
        lblStsHid.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Status"))
        CreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("UserName"))
        lblCtrlAmtFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objDataSet.Tables(0).Rows(0).Item("GrandTotal"), 2), 2)
        'objGlobal.GetIDDecimalSeparator(FormatNumber(Trim(objDataSet.Tables(0).Rows(0).Item("GrandTotal")), 2, True, False, False))
        lblPrintDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("PrintDate")))
        txtDesc.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Description"))
        txtRefNo.Text = Trim(objDataSet.Tables(0).Rows(0).Item("RefNo"))
        txtDate.Text = objGlobal.GetShortDate(strDateFMT, Trim(objDataSet.Tables(0).Rows(0).Item("DocDate")))
        txtAmt.Text = FormatNumber(Trim(objDataSet.Tables(0).Rows(0).Item("DocAmt")), 2, True, False, False)
        lstTxType.SelectedValue = Trim(objDataSet.Tables(0).Rows(0).Item("TransactType"))
        hidCtrlAmtFig.Value = FormatNumber(objDataSet.Tables(0).Rows(0).Item("GrandTotal"), 2, True, False, False)
    End Sub

    Sub Initialize()
        lstAccCode.SelectedIndex = 0
        lstBlock.SelectedIndex = 0
        lstVehCode.SelectedIndex = 0
        lstVehExp.SelectedIndex = 0
        ddlChargeLevel.selectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim Total As Double    

        dgTx.DataSource = LoadDataGrid()
        dgTx.DataBind()

        Total = 0
        hidTtlDBAmt.Value = 0
        hidTtlCRAmt.Value = 0
        For intCnt = 0 To dsGrid.Tables(0).Rows.Count - 1
            If Sign(dsGrid.Tables(0).Rows(intCnt).Item("Total")) = 1 Then
                Total += dsGrid.Tables(0).Rows(intCnt).Item("Total")
                hidTtlDBAmt.Value += dsGrid.Tables(0).Rows(intCnt).Item("Total")
            Else
                hidTtlCRAmt.Value += dsGrid.Tables(0).Rows(intCnt).Item("Total")
            End If
        Next intCnt

        dblTotDocAmt = Total
        txtAmt.Text = FormatNumber(dblTotDocAmt, 2, True, False, False)

        lblTotAmtFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Total, 2), 2)
        'objGlobal.GetIDDecimalSeparator(FormatNumber(Total, 0, True, False, False))
        dgTx.Columns(2).Visible = Session("SS_INTER_ESTATE_CHARGING")
    End Sub

    Protected Function LoadDataGrid() As DataSet
        Dim strParam As String

        strParam = Trim(lblTxID.Text)

        Try
            intErrNo = objGLtx.mtdGetGLTxDetails(strOpCdStckTxLine_GET, strParam, dsGrid)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_JOURNALLN&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
        End Try


        Return dsGrid
    End Function

    Sub LoadStockTxDetails()
        Dim strOpCdStckTxDet_GET As String = "GL_CLSTRX_JOURNAL_DETAIL_GET"
        Dim StockCode As String

        strParam = Trim(lblTxID.Text)
        Try
            intErrNo = objGLtx.mtdGetGLTxDetails(strOpCdStckTxDet_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_USAGEDETAILS&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
        Else
            TrLink.Visible = False
        End If
    End Sub

    'Protected Function CheckDate() As String
    '    Dim strDateSetting As String
    '    Dim objSysCfgDs As DataSet
    '    Dim objDateFormat As String
    '    Dim strValidDate As String

    '    If Not txtDate.Text = "" Then
    '        If objGlobal.mtdValidInputDate(strDateFMT, txtDate.Text, objDateFormat, strValidDate) = True Then
    '            Return strValidDate
    '        Else
    '            lblFmt.Text = objDateFormat & "."
    '            lblDate.Visible = True
    '            lblFmt.Visible = True
    '        End If
    '    End If
    'End Function

    Sub CallCheckVehicleUse(ByVal sender As Object, ByVal e As System.EventArgs)
        CheckVehicleUse()
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer, _
                          ByRef pr_strNurseryInd As Integer, _
                          ByRef pr_blnFound As Boolean)

        Dim _objAccDs As New DataSet()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLset.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
            pr_blnFound = True
        Else
            pr_blnFound = False
            pr_strAccType = 0
            pr_strAccPurpose = 0
            pr_strNurseryInd = 0
        End If
    End Sub

    Sub CheckVehicleUse()
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strAcc As String = Request.Form("lstAccCode")
        Dim strBlk As String = Request.Form("lstBlock")
        Dim strPreBlk As String = Request.Form("ddlPreBlock")
        Dim strVeh As String = Request.Form("lstVehCode")
        Dim strVehExp As String = Request.Form("lstVehExp")

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd, blnFound)

        If intAccType = objGLset.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLset.EnumAccountPurpose.NonVehicle
                    BindPreBlock(strAcc, strPreBlk)
                    BindBlockDropList(strAcc, strBlk)
                    BindVehicleCodeDropList("")
                    BindVehicleExpDropList(True)
                Case objGLset.EnumAccountPurpose.VehicleDistribution
                    BindPreBlock("", "")
                    BindBlockDropList("")
                    BindVehicleCodeDropList(strAcc, strVeh)
                    BindVehicleExpDropList(False, strVehExp)
                Case Else
                    BindPreBlock(strAcc, strPreBlk)
                    BindBlockDropList(strAcc, strBlk)
                    BindVehicleCodeDropList("%", strVeh)
                    BindVehicleExpDropList(False, strVehExp)
            End Select
        ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
            BindPreBlock(strAcc, strPreBlk)
            BindBlockDropList(strAcc, strBlk)
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        Else
            BindPreBlock("", "")
            BindBlockDropList("", "")
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        End If
    End Sub

    Function CheckRequiredField() As Boolean
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strAcc As String = Request.Form("lstAccCode")
        Dim strBlk As String 
        Dim strVeh As String = Request.Form("lstVehCode")
        Dim strVehExp As String = Request.Form("lstVehExp")

        If ddlChargeLevel.SelectedIndex = 1 Then
            strBlk = Request.Form("lstBlock")
        Else
            strBlk = Request.Form("ddlPreBlock")
        End If

        If txtDescLn.Text = "" Then
            lblDescErr.Visible = True
            Return True
        End If

        If ddlLocation.SelectedIndex = 0 Then
            lblLocationErr.Visible = True
            Return True
        End If

        If strAcc = "" Then
            lblAccCodeErr.Visible = True
            Return True
        End If

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd, blnFound)

        If blnFound = True Then
            If intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.No Then
                Return False
            ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
                If strBlk = "" Then
                    If ddlChargeLevel.SelectedIndex = 1 Then
                        lblBlockErr.Visible = True
                    Else
                        lblPreBlockErr.Visible = True
                    End If
                    Return True
                Else
                    Return False
                End If
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.NonVehicle And _
                strBlk = "" Then

                If ddlChargeLevel.SelectedIndex = 1 Then
                    lblBlockErr.Visible = True
                Else
                    lblPreBlockErr.Visible = True
                End If
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution And _
                strVeh = "" Then
                lblVehCodeErr.Visible = True
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution And _
                strVehExp = "" Then
                lblVehExpCodeErr.Visible = True
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.Others And _
                strVeh <> "" And strVehExp = "" Then
                lblVehExpCodeErr.Visible = True
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.Others And _
                strBlk = "" Then
                If ddlChargeLevel.SelectedIndex = 1 Then
                    lblBlockErr.Visible = True
                Else
                    lblPreBlockErr.Visible = True
                End If
                Return True
            ElseIf intAccType = objGLset.EnumAccountType.ProfitAndLost And _
                intAccPurpose = objGLset.EnumAccountPurpose.Others And _
                strVeh = "" And strVehExp <> "" Then
                lblVehCodeErr.Visible = True
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label
        Dim Delbutton As LinkButton
        Dim strDesc As String
        Dim strLocCode As String
        Dim strAccCode As String
        Dim strBlkCode As String
        Dim strVehCode As String
        Dim strVehExpCode As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strDBCR As String = ""
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

        blnUpdate.Text = True
        dgTx.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblID")
        lblTxLnID.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("lblDesc")
        txtDescLn.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblAccTx")
        hidDBCR.Value = lbl.Text.Trim
        strDBCR = lbl.Text.Trim
        If lbl.Text.Trim = "DR" Then
            lbl = E.Item.FindControl("lblAmt")
            txtDRTotalAmount.Text = Replace(lbl.Text.Trim, "-", "") 'Replace(Replace(Replace(lbl.Text.Trim, ".", ""), ",", "."), "-", "")
            hidDBAmt.Value = Replace(Replace(Replace(lbl.Text.Trim, ",", ""), ",", "."), "-", "")
            hidCRAmt.Value = 0
        ElseIf lbl.Text = "CR" Then
            lbl = E.Item.FindControl("lblAmt")
            txtCRTotalAmount.Text = Replace(lbl.Text.Trim, "-", "") 'Replace(Replace(Replace(lbl.Text.Trim, ".", ""), ",", "."), "-", "")
            hidCRAmt.Value = Replace(Replace(Replace(lbl.Text.Trim, ",", ""), ",", "."), "-", "")
            hidDBAmt.Value = 0
        End If

        lbl = E.Item.FindControl("lblLocCode")
        strLocCode = lbl.Text.Trim
        BindLocationDropDownList(strLocCode)
        lbl = E.Item.FindControl("lblAccCode")
        strAccCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBlkCode")
        strBlkCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVehCode")
        strVehCode = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVehExpCode")
        strVehExpCode = lbl.Text.Trim

        BindAccCodeDropList(strAccCode)

        GetAccountDetails(strAccCode, intAccType, intAccPurpose, intNurseryInd, blnFound)

        If intAccType = objGLset.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLset.EnumAccountPurpose.NonVehicle
                    BindBlockDropList(strAccCode, strBlkCode)
                    BindVehicleCodeDropList("")
                    BindVehicleExpDropList(True)
                Case objGLset.EnumAccountPurpose.VehicleDistribution
                    BindBlockDropList("")
                    BindVehicleCodeDropList(strAccCode, strVehCode)
                    BindVehicleExpDropList(False, strVehExpCode)
                Case Else
                    BindBlockDropList(strAccCode, strBlkCode)
                    BindVehicleCodeDropList("%", strVehCode)
                    BindVehicleExpDropList(False, strVehExpCode)
            End Select
        ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
            BindBlockDropList(strAccCode, strBlkCode)
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        Else
            BindBlockDropList("", "")
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        End If

        Delbutton = E.Item.FindControl("Delete")
        Delbutton.Visible = False

        BindGrid()

        If lblTxLnID.Text <> "" Then
            Add.Visible = False
            Update.Visible = True
        Else
            Add.Visible = True
            Update.Visible = False
        End If
        RowChargeLevel.Visible = False
        ddlChargeLevel.SelectedIndex = 1
        ToggleChargeLevel()
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        lblTxLnID.Text = ""
        txtDescLn.Text = ""
        'txtQty.Text = ""
        'txtQtyCR.Text = ""
        'txtPrice.Text = ""
        'txtPriceCR.Text = ""
        txtDRTotalAmount.Text = ""
        txtCRTotalAmount.Text = ""
        hidDBCR.Value = ""
        hidCRAmt.Value = 0
        hidDBAmt.Value = 0
        Initialize()

        dgTx.EditItemIndex = -1
        BindGrid()

        Add.Visible = True
        Update.Visible = False
        BindLocationDropDownList(Trim(Session("SS_LOCATION")))
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
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

        If lblStsHid.Text = CStr(objGLtx.EnumJournalStatus.Active) Then

            Dim strOpCdStckTxLine_DEL As String = "GL_CLSTRX_JOURNAL_LINE_DEL"
            Dim lbl As Label
            Dim id As String
            Dim ItemCode As String
            Dim strParam As String
            Dim StrTxParam As String
            Dim TxID As String
            Dim ErrorChk As Integer = objGLtx.EnumGeneralLedgerTxErrorType.NoError

            lbl = E.Item.FindControl("lblID")
            id = lbl.Text

            strParam = id & "|" & Trim(lblTxID.Text)
            Try
                intErrNo = objGLtx.mtdDelTransactLn(strOpCdStckTxLine_DEL, strParam)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_USAGELINE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If

            End Try

            StrTxParam = lblTxID.Text & "|||||||||||||"

            Try
                intErrNo = objGLtx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strUserId, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                            ErrorChk, _
                                                            TxID)

                If ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.Overflow Then
                    lblError.Visible = True
                End If

                lblTxID.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWJOURNAL&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If
            End Try
            LoadStockTxDetails()
            DisplayFromDB()
            BindGrid()
            PageControl()
        End If
    End Sub

    Sub BindTxTypeList()
        Dim IntCnt As Integer

        lstTxType.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.Adjustment), objGLtx.EnumJournalTransactType.Adjustment))
        lstTxType.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.CreditNote), objGLtx.EnumJournalTransactType.CreditNote))
        lstTxType.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.DebitNote), objGLtx.EnumJournalTransactType.DebitNote))
        lstTxType.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.Invoice), objGLtx.EnumJournalTransactType.Invoice))
        lstTxType.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.Others), objGLtx.EnumJournalTransactType.Others))
        lstTxType.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.Umum), objGLtx.EnumJournalTransactType.Umum))
        If lblTxID.Text <> "" Then
            lstTxType.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.WorkshopDistribution), objGLtx.EnumJournalTransactType.WorkshopDistribution))
            For IntCnt = 0 To lstTxType.Items.Count - 1
                If Trim(objDataSet.Tables(0).Rows(0).Item("Transacttype")) = lstTxType.Items(IntCnt).Value Then
                    lstTxType.SelectedIndex = IntCnt
                End If
            Next
        Else
            lstTxType.SelectedIndex = 5
        End If

    End Sub

    Sub BindPreBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim objBlkDs As DataSet
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        strOpCd = "GL_CLSSETUP_ACCOUNT_PREBLOCK_GET"
        intSelectedIndex = 0
        Try
            strParam = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLset.EnumBlockStatus.Active
            intErrNo = objGLset.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If objBlkDs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblPleaseSelect.Text & PreBlockTag & lblCode.Text

        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlPreBlock.DataSource = objBlkDs.Tables(0)
        ddlPreBlock.DataValueField = "BlkCode"
        ddlPreBlock.DataTextField = "Description"
        ddlPreBlock.DataBind()
        ddlPreBlock.SelectedIndex = intSelectedIndex

        If Not objBlkDs Is Nothing Then
            objBlkDs = Nothing
        End If
    End Sub

    Sub BindBlockDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim strParam As String
        Dim dr As DataRow

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLset.EnumBlockStatus.Active
            Else
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLset.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLset.mtdGetAccountBlock(strOpCdBlockList_Get, _
                                                     strParam, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If dsForDropDown.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & strBlockTag
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)
        lstBlock.DataSource = dsForDropDown.Tables(0)
        lstBlock.DataValueField = "BlkCode"
        lstBlock.DataTextField = "Description"
        lstBlock.DataBind()
        lstBlock.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")

        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLset.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLset.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLset.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = lblSelect.Text & strAccountTag
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstAccCode.DataSource = dsForDropDown.Tables(0)
        lstAccCode.DataValueField = "AccCode"
        lstAccCode.DataTextField = "_Description"
        lstAccCode.DataBind()
        lstAccCode.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindVehicleCodeDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strVehCode As String = "")

        Dim dsForDropDown As DataSet
        Dim strOpCd As String
        Dim drinsert As DataRow
        Dim strParam As New StringBuilder()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        strParam.Append("|AccCode = '" & pv_strAccCode & "' AND LocCode = '" & ddlLocation.SelectedItem.Value.Trim() & "' AND Status = '" & objGLset.EnumVehicleStatus.Active & "'")
        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"
            intErrNo = objGLset.mtdGetMasterList(strOpCd, _
                                                   strParam.ToString, _
                                                   objGLset.EnumGLMasterType.Vehicle, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
        End Try




        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("VehCode") = Trim(pv_strVehCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = lblSelect.Text & strVehTag
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstVehCode.DataSource = dsForDropDown.Tables(0)
        lstVehCode.DataValueField = "VehCode"
        lstVehCode.DataTextField = "Description"
        lstVehCode.DataBind()
        lstVehCode.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindVehicleExpDropList(ByVal pv_IsBlankList As Boolean, Optional ByVal pv_strVehExpCode As String = "")

        Dim dsForDropDown As DataSet
        Dim strOpCd As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim drinsert As DataRow
        Dim strParam As String = "Order By VehExpenseCode ASC|And Veh.Status = '" & objGLset.EnumVehicleExpenseStatus.active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            If pv_IsBlankList Then
                strParam += " And Veh.VehExpensecode = ''"
            End If
            intErrNo = objGLset.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLset.EnumGLMasterType.VehicleExpense, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=JR_TRX_ADTRX_VEHEXPENSE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(pv_strVehExpCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = lblSelect.Text & strVehExpTag
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstVehExp.DataSource = dsForDropDown.Tables(0)
        lstVehExp.DataValueField = "VehExpenseCode"
        lstVehExp.DataTextField = "Description"
        lstVehExp.DataBind()
        lstVehExp.SelectedIndex = intSelectedIndex


        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub


    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If CheckRequiredField() Then
            Exit Sub
        End If
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strOpCdStckTxLine_ADD As String = "GL_CLSTRX_JOURNAL_LINE_ADD"

        Dim strStatus As String
        Dim TxID As String
        Dim IssType As String
        Dim strAmount As String
        Dim StrTxParam As New StringBuilder()
        Dim strTxLnParam As New StringBuilder()
        Dim ErrorChk As Integer = objGLtx.EnumGeneralLedgerTxErrorType.NoError
        Dim strAllowVehicle As String
        Dim dblQty As Double = 1
        Dim dblPrice As Double = 0
        Dim dblTotal As Double = 0
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

        If txtDRTotalAmount.Text <> "" And txtCRTotalAmount.Text <> "" Then
            lblTwoAmount.Visible = True
            Exit Sub
        End If

        If txtDRTotalAmount.Text <> "" Then
            dblQty = 1
            dblPrice = CDbl(txtDRTotalAmount.Text)
            dblTotal = CDbl(txtDRTotalAmount.Text)
        End If

        If txtCRTotalAmount.Text <> "" Then
            dblQty = 1
            dblPrice = objGlobal.mtdGetReverseValue(txtCRTotalAmount.Text)
            dblTotal = CDbl(txtCRTotalAmount.Text)
            dblTotal = dblTotal - (dblTotal * 2)
        End If

        If CheckLocBillParty() = False Then
            lblBPErr.Visible = True
            lblLocCodeErr.Visible = True
            Exit Sub
        End If

        'If Month(strDate) < strAccMonth And Year(strDate) <= strAccYear Then
        '    lblDate.Visible = True
        '    lblDate.Text = "Invalid transaction date."
        '    Exit Sub
        'End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        strNewIDFormat = "MMO" & "/" & strCompany & "/" & strLocation & "/" & Trim(lstTxType.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        If lblTxID.Text = "" Then
            StrTxParam.Append(lblTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(Replace(txtDesc.Text, "'", "''"))
            StrTxParam.Append("|")
            StrTxParam.Append(ddlReceiveFrom.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(lstTxType.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(Replace(txtRefNo.Text, "'", "''"))
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|")
            StrTxParam.Append(IIf(txtAmt.Text = "", "0", txtAmt.Text))
            StrTxParam.Append("|")
            StrTxParam.Append(0)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|||")
            StrTxParam.Append(strNewIDFormat)
            
            Try
                intErrNo = objGLtx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                    strOpCdStckTxDet_UPD, _
                                                    strOpCdStckTxLine_GET, _
                                                    strUserId, _
                                                    StrTxParam.ToString, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                    ErrorChk, _
                                                    TxID)
                lblTxID.Text = TxID
                If ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.Overflow Then
                    lblError.Visible = True
                End If

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWJOURNAL&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If
            End Try
        End If

        txtDescLn.Text = Replace(txtDescLn.Text, "|", " ")
        If lblTxLnID.Text = "" Then
            strTxLnParam.Append(lblTxID.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(txtDescLn.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblQty)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblPrice)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblTotal)
            strTxLnParam.Append("|")
            strTxLnParam.Append(Request.Form("lstAccCode"))
            strTxLnParam.Append("|")

            If ddlChargeLevel.SelectedIndex = 0 Then
                strTxLnParam.Append(Request.Form("ddlPreBlock"))
            Else
                strTxLnParam.Append(Request.Form("lstBlock"))
            End If
            strTxLnParam.Append("||")
            strTxLnParam.Append(Request.Form("lstVehCode"))
            strTxLnParam.Append("|")
            strTxLnParam.Append(Request.Form("lstVehExp"))
            strTxLnParam.Append("|")
            strTxLnParam.Append(ddlLocation.SelectedItem.Value.Trim())

            Try
                If ddlChargeLevel.SelectedIndex = 0 And RowPreBlk.Visible = True Then
                    strParamList = Session("SS_LOCATION") & "|" & _
                                       lstAccCode.SelectedItem.Value.Trim & "|" & _
                                       ddlPreBlock.SelectedItem.Value.Trim & "|" & _
                                       objGLset.EnumBlockStatus.Active



                    intErrNo = objGLtx.mtdAddJournalLnByBlock(strOpCodeGLSubBlkByBlk, _
                                                              strParamList, _
                                                              strOpCdStckTxLine_ADD, _
                                                              objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.JournalLn), _
                                                              strTxLnParam.ToString, _
                                                              strLocType, _
                                                              ErrorChk)

                Else
                    intErrNo = objGLtx.mtdAddJournalLn(strOpCdStckTxLine_ADD, _
                                                       objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.JournalLn), _
                                                       strTxLnParam.ToString, _
                                                       ErrorChk)
                End If
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDUSAGELINE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If
            End Try
        Else
            strTxLnParam.Append(lblTxLnID.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(txtDescLn.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblQty)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblPrice)
            strTxLnParam.Append("|")
            strTxLnParam.Append(dblTotal)
            strTxLnParam.Append("|")
            strTxLnParam.Append(Request.Form("lstAccCode"))
            strTxLnParam.Append("|")
            strTxLnParam.Append(Request.Form("lstBlock"))
            strTxLnParam.Append("||")
            strTxLnParam.Append(Request.Form("lstVehCode"))
            strTxLnParam.Append("|")
            strTxLnParam.Append(Request.Form("lstVehExp"))
            strTxLnParam.Append("|")
            strTxLnParam.Append(ddlLocation.SelectedItem.Value.Trim())
            strTxLnParam.Append("|")
            strTxLnParam.Append(lblTxID.Text)

            Try
                intErrNo = objGLtx.mtdUpdJournalLineDetail(strOpCdStckTxLine_UPD, _
                                                           strTxLnParam.ToString, _
                                                           ErrorChk)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPD_JRNLINE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
            End Try
            lblTxLnID.Text = ""
            dgTx.EditItemIndex = -1

        End If

        Select Case ErrorChk
            Case objGLtx.EnumGeneralLedgerTxErrorType.Overflow
                lblError.Visible = True
        End Select

        StrTxParam.Remove(0, StrTxParam.Length)
        StrTxParam.Append(lblTxID.Text)
        StrTxParam.Append("|||||||||||||")

        Try
            intErrNo = objGLtx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                    strOpCdStckTxDet_UPD, _
                                                    strOpCdStckTxLine_GET, _
                                                    strUserId, _
                                                    StrTxParam.ToString, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                    ErrorChk, _
                                                    TxID)
            If ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.Overflow Then
                lblError.Visible = True
            End If

            lblTxID.Text = TxID
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
            End If
        End Try
        Initialize()
        LoadStockTxDetails()
        DisplayFromDB()
        BindGrid()
        PageControl()
        txtDRTotalAmount.Text = ""
        txtCRTotalAmount.Text = ""
        txtDRTotalAmount.Text = ""
        txtCRTotalAmount.Text = ""
        blnShortCut.Text = False
        hidDBCR.Value = ""
        hidCRAmt.Value = 0
        hidDBAmt.Value = 0

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If
        If Not strTxLnParam Is Nothing Then
            strTxLnParam = Nothing
        End If

        If lblTxLnID.Text <> "" Then
            Add.Visible = False
            Update.Visible = True
        Else
            Add.Visible = True
            Update.Visible = False
        End If

    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim TxID As String
        Dim IssType As String
        Dim strAllowVehicle As String
        Dim ErrorChk As Integer = objGLtx.EnumGeneralLedgerTxErrorType.NoError
        Dim StrTxParam As New StringBuilder()
        Dim strNewIDFormat As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

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

        If lblTxID.Text = "" Then
            Exit Sub
        End If

        Dim arrParam As Array
        arrParam = Split(lblAccPeriod.Text, "/")
        If Month(strDate) <> arrParam(0) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        ElseIf Year(strDate) <> arrParam(1) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        strNewIDFormat = "MMO" & "/" & strCompany & "/" & strLocation & "/" & Trim(lstTxType.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        If lblTxID.Text = "" Then
            StrTxParam.Append(lblTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(Replace(txtDesc.Text, "'", "''"))
            StrTxParam.Append("|")
            StrTxParam.Append(ddlReceiveFrom.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(lstTxType.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(Replace(txtRefNo.Text, "'", "''"))
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|")
            StrTxParam.Append(IIf(txtAmt.Text = "", "0", txtAmt.Text))
            StrTxParam.Append("|")
            StrTxParam.Append(0)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|||")
            StrTxParam.Append(strNewIDFormat)
        Else
            StrTxParam.Append(lblTxID.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(Replace(txtDesc.Text, "'", "''"))
            StrTxParam.Append("|")
            StrTxParam.Append(ddlReceiveFrom.SelectedItem.Value)
            StrTxParam.Append("|")
            'StrTxParam.Append(lstTxType.SelectedItem.Value)
            StrTxParam.Append("|")
            StrTxParam.Append(Replace(txtRefNo.Text, "'", "''"))
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|")
            StrTxParam.Append(txtAmt.Text)
            StrTxParam.Append("||||")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|||")
        End If
        Try

            intErrNo = objGLtx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                        strOpCdStckTxDet_UPD, _
                                                        strOpCdStckTxLine_GET, _
                                                        strUserId, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                        ErrorChk, _
                                                        TxID)

            lblTxID.Text = TxID
            If ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.OverFlow Then
                lblError.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
            End If
        End Try
        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        LoadStockTxDetails()
        DisplayFromDB()
        PageControl()

    End Sub


    Sub btnPrint_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objGLtx.EnumGeneralLedgerTxErrorType.NoError
        If lblPrintDate.Text = "" Then

            StrTxParam = lblTxID.Text & "|||||||||||" & Now() & "||"
            Try
                intErrNo = objGLtx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strUserId, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                            ErrorChk, _
                                                            TxID)
                lblTxID.Text = TxID
                If ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.OverFlow Then
                    lblError.Visible = True
                End If

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If
            End Try
        Else
            lblReprint.Visible = False
        End If

        LoadStockTxDetails()
        DisplayFromDB()
        PageControl()

    End Sub

    Sub btnCancel_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objGLtx.EnumGeneralLedgerTxErrorType.NoError
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""

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

        StrTxParam = lblTxID.Text & "||||||||||||" & objGLtx.EnumJournalStatus.Cancelled & "|"

        If intErrNo = 0 And ErrorChk = objGLtx.EnumGeneralLedgerTxErrorType.NoError Then
            Try
                intErrNo = objGLtx.mtdUpdJournalDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strUserId, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.Journal), _
                                                            ErrorChk, _
                                                            TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CANCELUSAGE&errmesg=" & lblErrMessage.Text & "&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
                End If
            End Try
        End If

        LoadStockTxDetails()
        DisplayFromDB()
        PageControl()
        BindGrid()

    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../GL/Trx/GL_Trx_Journal_List.aspx")
    End Sub

    Function CheckLocBillParty () As Boolean
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strSearch As String
        Dim objBPLocDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strLocCode As String
        Dim objDS As New DataSet()

        strLocCode = TRIM(ddlLocation.SelectedItem.Value)

        If Not (strLocCode = "" Or strLocCode = TRIM(strLocation)) Then
            strSearch = " AND BP.Status = '" & objGLset.EnumBillPartyStatus.Active & "'" & _
                        " AND BP.InterLocCode = '" & strLocCode & "'" 
                
            Try
                intErrNo = objGLset.mtdGetBPInterLoc(strOpCd_Get, strSearch, objBPLocDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_SETUP_BILLPARTY_GET&errmesg=" & Exp.ToString() & "&redirect=PR/trx/PR_trx_DailyAttd.aspx")
            End Try

            If objBPLocDs.Tables(0).Rows.Count <= 0 Then
                lblLocCodeErr.Text = strLocCode
                return False
            End If
        End If

        return True
    End Function

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

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_trx_Journal_details.aspx")
    End Sub

    Private Sub lbViewJournal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbViewJournal.Click
        Dim intErrNo As Integer
        Dim dsResult As New Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "GL_JOURNAL_PREDICTION"
        Dim arrPeriod As Array

        arrPeriod = Split(lblAccPeriod.Text, "/")

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|USERID|TRXID"
        strParamValue = strLocation & "|" & arrPeriod(0) & _
                        "|" & arrPeriod(1) & "|" & _
                        Session("SS_USERID") & "|" & Trim(lblTxID.Text)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsResult)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DAYEND_PROCESS&errmesg=" & Exp.Message.ToString & "&redirect=")
        End Try

        If dsResult.Tables(0).Rows.Count > 0 Then

            Dim TotalDB As Double
            Dim TotalCR As Double
            Dim intCnt As Integer
            For intCnt = 0 To dsResult.Tables(0).Rows.Count - 1
                TotalDB += dsResult.Tables(0).Rows(intCnt).Item("AmountDB")
                TotalCR += dsResult.Tables(0).Rows(intCnt).Item("AmountCR")
            Next
            lblTotalDB.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TotalDB, 2), 2)
            lblTotalCR.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TotalCR, 2), 2)

            dgViewJournal.DataSource = Nothing
            dgViewJournal.DataSource = dsResult.Tables(0)
            dgViewJournal.DataBind()

            lblTotalDB.Visible = True
            lblTotalCR.Visible = True
            lblTotalViewJournal.Visible = True
            lblTotalViewJournal.Text = "Total Amount : "
        End If

    End Sub

  
End Class
