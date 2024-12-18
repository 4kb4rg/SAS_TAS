Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Math
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Public Class IN_FuelIssueDet : Inherits Page

    Protected WithEvents dgStkTx As DataGrid
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblConfirmErr As Label
    Protected WithEvents lblEmpCodeErr As Label
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents lblBlockErr As Label
    Protected WithEvents lblVehCodeErr As Label
    Protected WithEvents lblVehExpCodeErr As Label
    Protected WithEvents lblItemCodeErr As Label
    Protected WithEvents lblBillPartyErr As Label
    Protected WithEvents lblStatusHid As Label
    Protected WithEvents lblError As Label
    Protected WithEvents lblStock As Label
    Protected WithEvents lblUnDel As Label
    Protected WithEvents lbleither As Label
    Protected WithEvents lblFuelMeter As Label

    Protected WithEvents lblFuelTxID As Label
    Protected WithEvents IssueType As Label
    Protected WithEvents lblEmpTag As Label
    Protected WithEvents lblAccTag As Label
    Protected WithEvents lblBlkTag As Label
    Protected WithEvents lblVehTag As Label
    Protected WithEvents lblVehExpTag As Label
    Protected WithEvents lblBPartyTag As Label
    Protected WithEvents lblBillTo As Label
    Protected WithEvents lblReprint As Label
    Protected WithEvents lstBillParty As DropDownList
    Protected WithEvents lstEmpID As DropDownList
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents lstVehExp As DropDownList
    Protected WithEvents lstVehCode As DropDownList
    Protected WithEvents lstBlock As DropDownList
    Protected WithEvents lstBillTo As DropDownList
    Protected WithEvents cblDisplayCost As CheckBoxList

    Protected WithEvents lblChargeMarkUp As Label
    Protected WithEvents chkMarkUp As CheckBox

    Protected WithEvents Status As Label
    Protected WithEvents CreateDate As Label
    Protected WithEvents UpdateDate As Label
    Protected WithEvents UpdateBy As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblDNNoteID As Label
    Protected WithEvents lblTotAmtFig As Label
    Protected WithEvents txtRefSIS As TextBox
    Protected WithEvents txtRefDate As TextBox
    Protected WithEvents txtRemarks As TextBox
    Protected WithEvents txtQty As TextBox
    Protected WithEvents txtMeter As TextBox
    Protected WithEvents lstItem As DropDownList
    Protected WithEvents lblPDateTag As Label
    Protected WithEvents lblDNIDTag As Label
    Protected WithEvents AccountCode As Label
    Protected WithEvents EmployeeCode As Label
    Protected WithEvents lblTotPriceFig As Label

    Protected WithEvents Update As ImageButton
    Protected WithEvents lblTxLnID As Label
    Protected WithEvents lblOldQty As Label
    Protected WithEvents lblOldItemCode As Label
    Protected WithEvents tblAdd As HtmlTable
    Protected WithEvents Add As ImageButton
    Protected WithEvents PRDelete As ImageButton
    Protected WithEvents Save As ImageButton
    Protected WithEvents Confirm As ImageButton
    Protected WithEvents Print As ImageButton
    Protected WithEvents DebitNote As ImageButton
    Protected WithEvents Cancel As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents AccCode As Label
    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents FindAcc As HtmlInputButton
    Protected WithEvents FindEmp As HtmlInputButton

    Protected WithEvents RowEmp As HtmlTableRow
    Protected WithEvents RowAcc As HtmlTableRow
    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents RowVeh As HtmlTableRow
    Protected WithEvents RowVehExp As HtmlTableRow
    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlk As HtmlTableRow
    Protected WithEvents lblPreBlockErr As Label
    Protected WithEvents lblPreBlkTag As Label
    Protected WithEvents lstPreBlock As DropDownList
    Protected WithEvents lstChargeLevel As DropDownList
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden
    Dim PreBlockTag As String
    Protected WithEvents lblLocationErr As Label
    Protected WithEvents lblLocationTag As Label
    Protected WithEvents ddlLocation As DropDownList

    Protected objINtx As New agri.IN.clstrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objINstp As New agri.IN.clsSetup()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objGLtrx As New agri.GL.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_FUELISSUE_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_FUELISSUE_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_FUELISSUE_LINE_GET"
    Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_FUELISSUE_LINE_ADD"
    Dim strOpCdLocList_GET As String = "ADMIN_CLSLOC_SYSLOC_LIST_GET"
    Dim strOpCdAccCode_GET As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
    Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
    Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"

    Dim objDataSet As New DataSet()
    Dim dsGrid As New DataSet()
    Dim objLangCapDs As New Dataset()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strARAccMonth As String
    Dim strARAccYear As String
    Dim intINAR As Integer
    Dim intConfigsetting As Integer
    Dim strAccTag As String
    Dim strBlkTag As String
    Dim strVehTag As String
    Dim strVehExpCodeTag As String
    Dim strBillPartyTag As String
    Dim strRefSIS As String = ""
    Dim strRefDate As String = ""
    Dim strRefRemark As String = ""

    Protected WithEvents lblBPErr As Label
    Protected WithEvents lblLocCodeErr As Label
	
	
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim IssueTyp As String
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strARAccMonth = Session("SS_ARACCMONTH")
        strARAccYear = Session("SS_ARACCYEAR")
        intINAR = Session("SS_INAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
	    
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INFuelIssue), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            strRefSIS = IIf(Trim(txtRefSIS.Text) = "", "", Trim(txtRefSIS.Text))
            strRefDate = IIf(Trim(txtRefDate.Text) = "", "", Trim(txtRefDate.Text))
            If strRefSis <> "" Or strRefDate <> "" Then
                strRefRemark = "SIS: " & strRefSIS & " Date: " & strRefDate
                txtRefSIS.Text = ""
                txtRefDate.Text = ""
            Else
                strRefRemark = ""
            End If

            If Not Page.IsPostBack Then
                BindChargeLevelDropDownList()
                BindLocationDropDownList(Trim(Session("SS_LOCATION")))
                lblFuelTxID.Text = Request.QueryString("Id")

                If Request.QueryString("Id") = "" Then
                    IssueTyp = Request.QueryString("isType")
                    Select Case IssueTyp
                        Case "Issue"
                            IssueType.Text = objINtx.EnumFuelIssueType.OwnUse
                        Case "Staff"
                            IssueType.Text = objINtx.EnumFuelIssueType.StaffPayroll
                        Case "External"
                            IssueType.Text = objINtx.EnumFuelIssueType.External
                    End Select
                Else
                    LoadStockTxDetails()

                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        DisplayFromDB(False)
                        BindGrid()
                    End If

                End If
                PageControl()
                IssueTypeControl()

            End If
            lblError.Visible = False
            lblStock.Visible = False
            lblUnDel.Visible = False
            lbleither.Visible = False
            lblFuelMeter.Visible = False
            lblConfirmErr.Visible = False
            lblEmpCodeErr.Visible = False
            lblAccCodeErr.Visible = False
            lblPreBlockErr.Visible = False
            lblBlockErr.Visible = False
            lblVehCodeErr.Visible = False
            lblVehExpCodeErr.Visible = False
            lblItemCodeErr.Visible = False
            lblBillPartyErr.Visible = False
            lblBillPartyErr.Visible = False
            lblLocCodeErr.Visible = False
        End If
    End Sub

    Sub BindChargeLevelDropDownList()
        lstChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        lstChargeLevel.Items.Add(New ListItem(strBlkTag, objLangCap.EnumLangCap.SubBlock))
        lstChargeLevel.selectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        ToggleChargeLevel()
    End Sub
    
    Sub lstChargeLevel_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        ToggleChargeLevel()
    End Sub
    
    Sub ToggleChargeLevel()
        If lstChargeLevel.selectedIndex = 0 Then
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
        Dim strPreBlkCode As String = lstPreBlock.SelectedItem.Value.Trim()
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
                       objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Inventory) & "|" & _
                       "INAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue) & "|" & _
                       "INAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue)
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
                strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
               strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_FUELISSUE_DET_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_trx_fuelissue_list.aspx")
        End Try

        strAccTag = GetCaption(objLangCap.EnumLangCap.Account)
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strVehExpCodeTag = GetCaption(objLangCap.EnumLangCap.VehExpense)
        strBillPartyTag = GetCaption(objLangCap.EnumLangCap.BillParty)

        lblAccTag.Text = strAccTag & lblCode.Text & " (DR) :* "
        lblBlkTag.Text = strBlkTag & lblCode.Text & " : "
        lblVehTag.Text = strVehTag & lblCode.Text & " : "
        lblVehExpTag.text = strVehExpCodeTag & lblCode.text & " : "
        lblBPartyTag.Text = strBillPartyTag & lblCode.Text & " :* "

        lblAccCodeErr.Text = "<Br>" & lblPleaseSelect.Text & strAccTag & lblCode.Text
        lblBlockErr.Text = lblPleaseSelect.Text & strBlkTag & lblCode.Text
        lblVehCodeErr.Text = lblPleaseSelect.Text & strVehTag & lblCode.Text
        lblVehExpCodeErr.Text = lblPleaseSelect.Text & strVehExpCodeTag & lblCode.Text
        lblBillPartyErr.Text = lblPleaseSelect.Text & strBillPartyTag & lblCode.Text

        AccountCode.Text = strAccTag & lblCode.text
        dgStkTx.columns(2).headertext = strBlkTag & lblCode.text
        dgStkTx.columns(3).headertext = strVehTag & lblCode.text
        dgStkTx.columns(4).headertext = strVehExpCodeTag & lblCode.text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_FUELISSUE_DET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
        End Try

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


    Sub IssueTypeControl()

        Select Case IssueType.Text
            Case objINtx.EnumFuelIssueType.OwnUse
                lblLocationTag.Visible = Session("SS_INTER_ESTATE_CHARGING")
                ddlLocation.Visible = Session("SS_INTER_ESTATE_CHARGING")

                RowAcc.Visible = True
                RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
                ToggleChargeLevel()
                RowVeh.Visible = True
                RowVehExp.Visible = True
                RowEmp.Visible = False

                DebitNote.Visible = False
                BindItemCodeList()
                BindAccCodeDropList()
                BindPreBlock("", "")
                BindBlockDropList("")
                BindVehicleCodeDropList("")
                BindVehicleExpDropList(True)

                Select Case lblStatusHid.Text
                    Case CStr(objINtx.EnumFuelIssueStatus.Confirmed), _
                        CStr(objINtx.EnumFuelIssueStatus.Deleted), _
                        CStr(objINtx.EnumFuelIssueStatus.Cancelled), _
                        CStr(objINtx.EnumFuelIssueStatus.DbNote)
                        tblAdd.Visible = False
                    Case Else
                        tblAdd.Visible = True

                End Select

            Case objINtx.EnumFuelIssueType.StaffPayroll, objINtx.EnumFuelIssueType.StaffDN
                lblLocationTag.Visible = False
                ddlLocation.Visible = False
                If objINtx.EnumFuelIssueType.StaffPayroll = IssueType.Text Then
                    DebitNote.Visible = False
                End If

                RowAcc.Visible = False
                RowChargeLevel.Visible = False
                RowPreBlk.Visible = False
                RowBlk.Visible = False
                RowVeh.Visible = False
                RowVehExp.Visible = False
                RowEmp.Visible = True

                lblBillTo.Visible = True
                lstBillTo.Visible = True
                BindEmployeeDropList()
                BindBillToList()
                BindItemCodeList()

                If lblStatusHid.Text.Trim() = "" Or lblStatusHid.Text = CStr(objINtx.EnumFuelIssueStatus.Active) Then
                    tblAdd.Visible = True
                    lstBillTo.Enabled = True
                Else
                    tblAdd.Visible = False
                    lstBillTo.Enabled = False
                End If
            Case objINtx.EnumFuelIssueType.External
                lblLocationTag.Visible = False
                ddlLocation.Visible = False
                lblBPartyTag.Visible = True
                lstBillParty.Visible = True
                RowAcc.Visible = False
                RowChargeLevel.Visible = False
                RowPreBlk.Visible = False
                RowBlk.Visible = False
                RowVeh.Visible = False
                RowVehExp.Visible = False
                RowEmp.Visible = False
                lblChargeMarkUp.Visible = True
                chkMarkUp.Visible = True
                chkMarkUp.Checked = True
                BindBillPartyDropList()
                BindItemCodeList()

                If Not lblFuelTxID.Text = "" Then
                    lstBillParty.Enabled = False
                End If


        End Select

    End Sub

    Sub PageControl()

        IssueTypeControl()
        txtRefSIS.Enabled = False
        txtRefDate.Enabled = False
        txtRemarks.Enabled = False
        ddlLocation.Enabled = False
        cblDisplayCost.Visible = False
        Find.Visible = False
        Save.Visible = False
        Confirm.Visible = False
        Print.Visible = False
        Confirm.Visible = False
        PRDelete.Visible = False
        DebitNote.Visible = False
        Cancel.Visible = False
        btnNew.Visible = False
        Select Case Trim(lblStatusHid.text)
            Case Trim(CStr(objINtx.EnumFuelIssueStatus.Deleted))
                PRDelete.Visible = True
                btnNew.Visible = True
                PRDelete.ImageUrl = "../../images/butt_undelete.gif"
                PRDelete.AlternateText = "Undelete"
                PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objINtx.EnumFuelIssueStatus.Confirmed))
                btnNew.Visible = True
                If issueType.text = objINtx.EnumFuelIssueType.External Or _
                   issueType.text = objINtx.EnumFuelIssueType.StaffDN Then
                    DebitNote.Visible = True
                End If
                Print.Visible = True
                cblDisplayCost.Visible = True
            Case Trim(CStr(objINtx.EnumFuelIssueStatus.DbNote))
                Print.Visible = True
                btnNew.Visible = True
                cblDisplayCost.Visible = True
            Case Trim(CStr(objINtx.EnumFuelIssueStatus.DbNote))
                Print.Visible = True
                btnNew.Visible = True
                cblDisplayCost.Visible = True
            Case Trim(CStr(objINtx.EnumFuelIssueStatus.Active)), ""
                txtRefSIS.Enabled = True
                txtRefDate.Enabled = True
                txtRemarks.Enabled = True
                Find.Visible = True
                Save.Visible = True
                If Trim(lblFuelTxID.Text) = "" Then
                    ddlLocation.Enabled = True
                Else
                    Confirm.Visible = True
                    PRDelete.Visible = True
                    btnNew.Visible = True
                    PRDelete.ImageUrl = "../../images/butt_delete.gif"
                    PRDelete.AlternateText = "Delete"
                    PRDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                End If
            Case Else
                btnNew.Visible = True
        End Select    
        DisableItemTable()
    End Sub

    Sub DisableItemTable()

        If lblStatusHid.Text = CStr(objINtx.EnumFuelIssueStatus.Deleted) _
        Or lblStatusHid.Text = CStr(objINtx.EnumFuelIssueStatus.Cancelled) _
        Or lblStatusHid.Text = CStr(objINtx.EnumFuelIssueStatus.DbNote) Then
            tblAdd.Visible = False
        ElseIf lblStatusHid.Text = CStr(objINtx.EnumFuelIssueStatus.Confirmed) Then
            tblAdd.Visible = False
        ElseIf lblStatusHid.Text = CStr(objINtx.EnumFuelIssueStatus.Active) Then
            tblAdd.Visible = True

        End If

    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)

        Dim lbl As Label
        Select Case e.Item.ItemType
            Case ListItemType.Header
                Select Case IssueType.Text
                    Case objINtx.EnumFuelIssueType.OwnUse
                        e.Item.Cells(1).Text = AccountCode.Text
                    Case objINtx.EnumFuelIssueType.StaffPayroll, objINtx.EnumFuelIssueType.StaffDN
                        e.Item.Cells(1).Text = EmployeeCode.Text
                        e.Item.Cells(2).Visible = False
                        e.Item.Cells(3).Visible = False
                        e.Item.Cells(4).Visible = False
                    Case objINtx.EnumFuelIssueType.External
                        e.Item.Cells(1).Text = " "
                        e.Item.Cells(2).Visible = False
                        e.Item.Cells(3).Visible = False
                        e.Item.Cells(4).Visible = False
                End Select

            Case ListItemType.Item, ListItemType.AlternatingItem
                Select Case IssueType.Text
                    Case objINtx.EnumFuelIssueType.OwnUse
                        lbl = e.Item.FindControl("AccCode")
                        lbl.Visible = True

                    Case objINtx.EnumFuelIssueType.StaffPayroll, objINtx.EnumFuelIssueType.StaffDN
                        lbl = e.Item.FindControl("PsEmpCode")
                        lbl.Visible = True
                        dgStkTx.Columns(2).Visible = False
                        dgStkTx.Columns(3).Visible = False
                        dgStkTx.Columns(4).Visible = False

                    Case objINtx.EnumFuelIssueType.External
                        dgStkTx.Columns(2).Visible = False
                        dgStkTx.Columns(3).Visible = False
                        dgStkTx.Columns(4).Visible = False

                End Select

        End Select

        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim DeleteButton As LinkButton
                Select Case Trim(Status.Text)
                    Case objINtx.mtdGetFuelIssueStatus(objINtx.EnumFuelIssueStatus.Active)
                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = True
                        DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objINtx.mtdGetFuelIssueStatus(objINtx.EnumFuelIssueStatus.Confirmed), _
                          objINtx.mtdGetFuelIssueStatus(objINtx.EnumFuelIssueStatus.Deleted), _
                          objINtx.mtdGetFuelIssueStatus(objINtx.EnumFuelIssueStatus.Cancelled), _
                          objINtx.mtdGetFuelIssueStatus(objINtx.EnumFuelIssueStatus.DBNote)
                        DeleteButton = e.Item.FindControl("Delete")
                        DeleteButton.Visible = False
                        Dim EditButton As LinkButton
                        EditButton = e.Item.FindControl("Edit")
                        EditButton.Visible = False
                End Select

        End Select

    End Sub

    Sub DisplayFromDB(ByVal pv_blnIsRedirect As Boolean)
        Dim intDate1 as integer
        Dim intDate2 as integer


        IssueType.Text = Trim(objDataSet.Tables(0).Rows(0).Item("IssueType"))
        lblStatusHid.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Status"))
        Status.Text = objINtx.mtdGetFuelIssueStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
        CreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("UserName"))
        txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))

        if trim(txtRemarks.text) = "Remarks:" then
            txtRemarks.text = ""
        end if 
        if txtRemarks.Text <> "" then
        intDate1 = InStr(Trim(txtRemarks.Text), "Date:")
        if intDate1 > 0 then 
            txtRefSIS.Text = right(left(Trim(txtRemarks.Text), (intDate1 - 1)), (intDate1 - 6))
        else
            txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))
        end if

        txtRefDate.Text = right(Trim(txtRemarks.Text), (len(Trim(txtRemarks.Text)) - (5 + len(trim(txtRefSIS.Text)))))
        intDate2 = InStr(Trim(txtRefDate.Text), "Remarks:")
        if intDate2 > 0 then
            txtRefDate.Text = right(left(Trim(txtRefDate.Text), (intDate2 - 1)), (intDate2 - 7))
        else 
            txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))            
        end if 

        if trim(txtRefDate.Text) <> "" and trim(txtRefSIS.Text) <> "" then 
            txtRemarks.Text = right(Trim(txtRemarks.Text), iif((len(Trim(txtRemarks.Text)) - (5 + len(trim(txtRefSIS.Text)) + 7 + len(trim(txtRefDate.Text)) + 10)) <= 0, 0, (len(Trim(txtRemarks.Text)) - (5 + len(trim(txtRefSIS.Text)) + 7 + len(trim(txtRefDate.Text)) + 10))))
        elseif trim(txtRefDate.Text) = "" and trim(txtRefSIS.Text) <> "" then 
            txtRemarks.Text = right(Trim(txtRemarks.Text), iif((len(Trim(txtRemarks.Text)) - (5 + len(trim(txtRefSIS.Text)) + 7 + 0 + 10)) <= 0, 0, (len(Trim(txtRemarks.Text)) - (5 + len(trim(txtRefSIS.Text)) + 7 + 0 + 10))))
        elseif trim(txtRefDate.Text) <> "" and trim(txtRefSIS.Text) = "" then
            txtRemarks.Text = right(Trim(txtRemarks.Text), iif((len(Trim(txtRemarks.Text)) - (5 + 0 + 7 + len(trim(txtRefDate.Text)) + 10)) <= 0, 0,(len(Trim(txtRemarks.Text)) - (5 + 0 + 7 + len(trim(txtRefDate.Text)) + 10))))
        else 
            txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))                
        end if 
        end if 



        lblTotPriceFig.Text = ObjGlobal.GetIDDecimalSeparator(Round(objDataSet.Tables(0).Rows(0).Item("TotalPrice"),0))
        lblTotAmtFig.Text = ObjGlobal.GetIDDecimalSeparator(Round(objDataSet.Tables(0).Rows(0).Item("TotalAmount"),0))
        BindLocationDropDownList(Trim(objDataSet.Tables(0).Rows(0).Item("ChargeLocCode")))
        If Not Trim(objDataSet.Tables(0).Rows(0).Item("DNID")) = "" Then
            lblDNIDTag.Visible = True
            lblDNNoteID.Text = Trim(objDataSet.Tables(0).Rows(0).Item("DNID"))
        End If

        If Not Trim(objDataSet.Tables(0).Rows(0).Item("DNID")) = "" Then
            lblDNIDTag.Visible = True
            lblDNNoteID.Text = Trim(objDataSet.Tables(0).Rows(0).Item("DNID"))
            If pv_blnIsRedirect = True Then
                Response.Redirect("../../BI/trx/BI_trx_DNDet.aspx?dbnid=" & lblDNNoteID.Text & "&referer=" & Request.ServerVariables("SCRIPT_NAME") & "?Id=" & lblFuelTxID.Text)
            End If
        End If
    End Sub

    Sub BindGrid()

        dgStkTx.DataSource = LoadDataGrid()
        dgStkTx.DataBind()
    End Sub

    Protected Function LoadDataGrid() As DataSet

        Dim strParam As String

        strParam = Trim(lblFuelTxID.Text)

        Try
            intErrNo = objINtx.mtdGetStockTransactDetails(strOpCdStckTxLine_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          dsGrid)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_FuelIssue&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
        End Try
        Return dsGrid
    End Function

    Sub LoadStockTxDetails()

        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_FUELISSUE_DETAIL_GET"
        Dim StockCode As String
        strParam = Trim(lblFuelTxID.Text)

        Try
            intErrNo = objINtx.mtdGetStockTransactDetails(strOpCdStckTxDet_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_FUELISSUELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
        End Try

    End Sub


    Sub Initialize()
        txtQty.Text = ""
        txtMeter.Text =""
        lstItem.SelectedIndex = 0
        lstEmpID.SelectedIndex = 0
        lstAccCode.SelectedIndex = 0
        lstBlock.SelectedIndex = 0
        lstVehCode.SelectedIndex = 0
        lstVehExp.SelectedIndex = 0
        Add.Visible = True
        Update.Visible = False
        chkMarkUp.Checked = True
    End Sub


    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label
        Dim Delbutton As LinkButton
        Dim strItemCode As String
        Dim strAcc As String
        Dim strBlk As String
        Dim strVeh As String
        Dim strVehExp As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim lblCharge As Label
        
        dgStkTx.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblID")
        lblCharge = E.Item.FindControl("lblToCharge")        
        If Trim(lblCharge.Text) = objINtx.EnumToCharge.Yes Then
           chkMarkUp.Checked = True
        else
           chkMarkUp.Checked = False
        End If
        lblTxLnID.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("ItemCode")
        strItemCode = lbl.Text.Trim
        BindItemCodeList(strItemCode)

        lstChargeLevel.Items.Clear()
        BindChargeLevelDropDownList()
        lstChargeLevel.SelectedIndex = 1

        Select Case IssueType.Text
               Case objINtx.EnumStockIssueType.OwnUse
                    lbl = E.Item.FindControl("AccCode")
                    strAcc = lbl.Text.Trim
                    BindAccCodeDropList(strAcc)
               Case objINtx.EnumStockIssueType.StaffPayroll, objINtx.EnumStockIssueType.StaffDN
                    lbl = E.Item.FindControl("PsEmpCode")
                    strAcc = lbl.Text.Trim
                    BindEmployeeDropList(strAcc)
        End Select

        lbl = E.Item.FindControl("lblBlkCode")
        strBlk = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVehCode")
        strVeh = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVehExpCode")
        strVehExp = lbl.Text.Trim
        lbl = E.Item.FindControl("lblQtyTrx")
        txtQty.Text = lbl.Text.Trim
        lblOldQty.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("ItemCode")
        lblOldItemCode.Text = lbl.Text.Trim

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd)

        If intAccType = objGLset.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLset.EnumAccountPurpose.NonVehicle
                    BindBlockDropList(strAcc, strBlk)
                    BindVehicleCodeDropList("")
                    BindVehicleExpDropList(True)
                Case objGLset.EnumAccountPurpose.VehicleDistribution
                    BindBlockDropList("")
                    BindVehicleCodeDropList(strAcc, strVeh)
                    BindVehicleExpDropList(False, strVehExp)
                Case objGLset.EnumAccountPurpose.Others
                    BindBlockDropList(strAcc, strBlk)
                    BindVehicleCodeDropList("%", strVeh)
                    BindVehicleExpDropList(False, strVehExp)
            End Select
        ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
            BindBlockDropList(strAcc, strBlk)
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        Else
            BindBlockDropList("")
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        End If

        Delbutton = E.Item.FindControl("Delete")
        Delbutton.Visible = False
        
        BindGrid()

        If lblFuelTxID.Text <> "" Then
            Add.Visible = False
            Update.Visible = True
        Else
            Add.Visible = True
            Update.Visible = False
        End If

        ToggleChargeLevel()
        lstChargeLevel.Enabled = False
        RowBlk.Visible = True
        RowPreBlk.Visible = False
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        dgStkTx.EditItemIndex = -1
        BindGrid()

        Initialize()        

        lstChargeLevel.Enabled = True
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If lblStatusHid.Text = CStr(objINtx.EnumFuelIssueStatus.Active) Then

            Dim strOpCdStckTxLine_Det_GET As String = "IN_CLSTRX_FUELISSUE_LINE_GET"
            Dim strOpCdStckTxLine_DEL As String = "IN_CLSTRX_FUELISSUE_LINE_DEL"
            Dim lbl As Label
            Dim id As String
            Dim ItemCode As String
            Dim strParam As String
            Dim StrTxParam As String
            Dim TxID As String
            Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

            lbl = E.Item.FindControl("lblID")
            id = lbl.Text
            lbl = E.Item.FindControl("ItemCode")
            ItemCode = lbl.Text

            strParam = lblFuelTxID.Text & "|" & id & "|" & ItemCode
            Try
                intErrNo = objINtx.mtdDelFuelTransactLn(strOpCdStckTxLine_DEL, _
                                                        strOpCdStckTxLine_Det_GET, _
                                                        strOpCdItem_Details_UPD, _
                                                        strOpCdItem_Details_GET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_FuelIssueLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                End If

            End Try
            StrTxParam = lblFuelTxID.Text & "|||||||||||||||||"
            Try
                intErrNo = objINtx.mtdUpdFuelIssueDetail(strOpCdStckTxDet_ADD, _
                                                        strOpCdStckTxDet_UPD, _
                                                        strOpCdStckTxLine_GET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strARAccMonth, _
                                                        strARAccYear, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FuelIssue), _
                                                        ErrorChk, _
                                                        TxID)
                If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                    lblError.Visible = True
                End If

                lblFuelTxID.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWFuelIssue&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                End If
            End Try
            LoadStockTxDetails()
            DisplayFromDB(False)
            BindGrid()
            BindItemCodeList()
        End If
        chkMarkUp.Checked = True
    End Sub

    Sub BindBillToList()
        lstBillTo.Items.Clear()
        lstBillTo.Items.Add(New ListItem(objINtx.mtdGetFuelIssueType(objINtx.EnumFuelIssueType.StaffPayroll), objINtx.EnumFuelIssueType.StaffPayroll))
        lstBillTo.Items.Add(New ListItem(objINtx.mtdGetFuelIssueType(objINtx.EnumFuelIssueType.StaffDN), objINtx.EnumFuelIssueType.StaffDN))
        If Not Request.QueryString("Id") = "" Then
            Select Case Trim(objDataSet.Tables(0).Rows(0).Item("IssueType"))
                Case objINtx.EnumFuelIssueType.StaffPayroll
                    lstBillTo.SelectedIndex = 0
                Case objINtx.EnumFuelIssueType.StaffDN
                    lstBillTo.SelectedIndex = 1
            End Select

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

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblPleaseSelect.Text & PreBlockTag & lblCode.Text

        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)
        lstPreBlock.DataSource = objBlkDs.Tables(0)
        lstPreBlock.DataValueField = "BlkCode"
        lstPreBlock.DataTextField = "Description"
        lstPreBlock.DataBind()
        lstPreBlock.SelectedIndex = intSelectedIndex

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

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & strBlkTag & lblCode.Text

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

        Try
            intErrNo = objGLset.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLset.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = lblSelect.Text & strAccTag & lblCode.Text
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstAccCode.DataSource = dsForDropDown.Tables(0)
        lstAccCode.DataValueField = "AccCode"
        lstAccCode.DataTextField = "Description"
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
        drinsert(1) = lblSelect.Text & strVehTag & lblCode.Text
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
        drinsert(1) = lblSelect.Text & strVehExpCodeTag & lblCode.Text
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

    Sub BindEmployeeDropList(Optional ByVal pv_strEmpCode As String = "")

        Dim strOpCdEmployee_Get As String = "HR_CLSSETUP_EMPLOYEE_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim dsForInactiveItem As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As New StringBuilder("")
        Dim drinsert As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            strParam.Append("|||")
            strParam.Append(objHRTrx.EnumEmpStatus.active)
            strParam.Append("|")
            strParam.Append(strLocation)
            strParam.Append("|Mst.EmpCode|ASC")

            intErrNo = objHRTrx.mtdGetEmployeeList(strOpCdEmployee_Get, strParam.ToString, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(pv_strEmpCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Select Employee Code"
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstEmpID.DataSource = dsForDropDown.Tables(0)
        lstEmpID.DataValueField = "EmpCode"
        lstEmpID.DataTextField = "EmpName"
        lstEmpID.DataBind()
        lstEmpID.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
        If Not dsForInactiveItem Is Nothing Then
            dsForInactiveItem = Nothing
        End If
    End Sub

    Sub BindBillPartyDropList()

        Dim strOpCdBillParty_Get As String = "GL_CLSTRX_BILLPARTY_LIST_GET"
        Dim dsForDropDown As DataSet
        Dim dsForInactiveItem As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim SearchStr As String
        Dim strParam As New StringBuilder("")
        Dim drinsert As DataRow

        Try
            strParam.Append("|")
            strParam.Append(objGLset.EnumBillPartyStatus.active)
            strParam.Append("|||")
            strParam.Append("|BillPartyCode|ASC")

            intErrNo = objGLtrx.mtdGetGLMasterList(strOpCdBillParty_Get, strParam.ToString, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If Not lblFuelTxID.Text = "" Then
                If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BillPartyCode"))) = UCase(Trim(objDataSet.Tables(0).Rows(0).Item("BillPartyCode"))) Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = lblSelect.Text & strBillPartyTag & lblCode.Text
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstBillParty.DataSource = dsForDropDown.Tables(0)
        lstBillParty.DataValueField = "BillPartyCode"
        lstBillParty.DataTextField = "Name"
        lstBillParty.DataBind()

        If SelectedIndex = -1 And Not lblFuelTxID.Text = "" Then

            Try
                strParam.Remove(0, strParam.Length)
                strParam.Append("||")
                strParam.Append(" AND BillPartyCode = '" & Trim(objDataSet.Tables(0).Rows(0).Item("EmpCode")) & "'")
                strParam.Append("||")
                strParam.Append("|BillPartyCode|ASC")

                intErrNo = objGLtrx.mtdGetGLMasterList(strOpCdBillParty_Get, strParam.ToString, dsForInactiveItem)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMNOTFOUND&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
            End Try

            If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                lstBillParty.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0)) & _
                " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Unknown) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0))))
            Else 
                lstBillParty.Items.Add(New ListItem(Trim(objDataSet.Tables(0).Rows(0).Item("BillPartyCode")) & _
                    " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Unknown) & ") ", Trim(objDataSet.Tables(0).Rows(0).Item("BillPartyCode"))))
            End If
            SelectedIndex = lstBillParty.Items.Count - 1
        End If

        lstBillParty.SelectedIndex = SelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
        If Not dsForInactiveItem Is Nothing Then
            dsForInactiveItem = Nothing
        End If
    End Sub

    Sub BindItemCodeList(Optional ByVal pv_strItemCode As String = "")

        Dim strOpCdItem_List_GET As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim dsItemCodeDropList As DataSet
        Dim intCnt As Integer
        Dim strparam As String
        Dim intSelectedIndex As Integer = 0

        strparam = objINstp.EnumInventoryItemType.Stock & "|" & objINstp.EnumStockItemStatus.Active & "|" & lblFuelTxID.Text & "|" & "itm.ItemCode"
        Try
            intErrNo = objINstp.mtdGetFilteredItemList(strOpCdItem_List_GET, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strparam, _
                                                       objINtx.EnumInventoryTransactionType.FuelIssue, _
                                                       dsItemCodeDropList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTOSERVTYPEDROPDOWNLIST&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
        End Try

        For intCnt = 0 To dsItemCodeDropList.Tables(0).Rows.Count - 1
            dsItemCodeDropList.Tables(0).Rows(intCnt).Item(0) = Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item(0))
            dsItemCodeDropList.Tables(0).Rows(intCnt).Item(1) = Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")) & " ( " _
                                                                & Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description")) & " ) - " & _
                                                                "Rp. " & objGlobal.GetIDDecimalSeparator(Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("AverageCost"))) & ", " & _
                                                                objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand")), 5) & " " & _
                                                                Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode"))    & " - Meter :" & _
                                                                objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("FuelMeterReading")), 5)
            If dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(pv_strItemCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        Dim drinsert As DataRow
        drinsert = dsItemCodeDropList.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = "Select Item Code"
        dsItemCodeDropList.Tables(0).Rows.InsertAt(drinsert, 0)

        lstItem.DataSource = dsItemCodeDropList.Tables(0)
        lstItem.DataValueField = "ItemCode"
        lstItem.DataTextField = "Description"
        lstItem.DataBind()
        lstItem.SelectedIndex = intSelectedIndex

        DisableItemTable()

        If Not dsItemCodeDropList Is Nothing Then
            dsItemCodeDropList = Nothing
        End If
    End Sub

    Sub CallCheckVehicleUse(ByVal sender As Object, ByVal e As System.EventArgs)
        CheckVehicleUse()
    End Sub

    Sub CheckVehicleUse()
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strAcc As String = Request.Form("lstAccCode")
        Dim strPreBlk As String = Request.Form("lstPreBlock")
        Dim strBlk As String = Request.Form("lstBlock")
        Dim strVeh As String = Request.Form("lstVehCode")
        Dim strVehExp As String = Request.Form("lstVehExp")

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd)

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
            BindBlockDropList("")
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
        End If

    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer, _
                          ByRef pr_strNurseryInd As Integer)

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
            pr_strAccType = CInt(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = CInt(_objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
        End If
    End Sub

    Function CheckEntry() As Boolean
        If Not txtQty.Text = "" And Not txtMeter.Text = "" Then
            lbleither.Visible = True
            Return False
        ElseIf txtQty.Text = "" And txtMeter.Text = "" Then
            lbleither.Visible = True
            Return False
        Else
            Return True
        End If
    End Function

    Function CheckBillPartyField() As Boolean
        If lstBillParty.SelectedItem.Value = "" Then
            lblBillPartyErr.Visible = True
            Return True
        End If
    End Function

    Function CheckRequiredField() As Boolean
        Dim strItem As String = Request.Form("lstItem")
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strAcc As String
        Dim strBlk As String
        Dim strVeh As String
        Dim strVehExp As String

        If IssueType.Text = objINtx.EnumFuelIssueType.OwnUse Then

            strAcc = Request.Form("lstAccCode")
            If lstChargeLevel.selectedIndex = 0 Then
                strBlk = Request.Form("lstPreBlock")
            Else
                strBlk = Request.Form("lstBlock")
            End If
            strVeh = Request.Form("lstVehCode")
            strVehExp = Request.Form("lstVehExp")
            GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd)

        End If

        If strItem.Trim = "" Then
            lblItemCodeErr.Visible = True
            Return True
        End If

        Select Case IssueType.Text
            Case objINtx.EnumFuelIssueType.OwnUse
                If strAcc = "" Then
                    lblAccCodeErr.Visible = True
                    Return True
                End If

                If intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.No Then
                    Return False
                ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
                    If strBlk = "" Then
                        If lstChargeLevel.selectedIndex = 0 Then
                            lblPreBlockErr.Visible = True
                        Else
                            lblBlockErr.Visible = True
                        End If
                        Return True
                    Else
                        Return False
                    End If
                Else
                    If strBlk = "" And Not intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution Then
                        If lstChargeLevel.selectedIndex = 0 Then
                            lblPreBlockErr.Visible = True
                        Else
                            lblBlockErr.Visible = True
                        End If
                        Return True
                    ElseIf strVeh = "" And intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution Then
                        lblVehCodeErr.Visible = True
                        Return True
                    ElseIf strVehExp = "" And intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution Then
                        lblVehExpCodeErr.Visible = True
                        Return True
                    ElseIf strVeh <> "" And strVehExp = "" And intAccPurpose = objGLset.EnumAccountPurpose.others Then
                        lblVehExpCodeErr.Visible = True
                        Return True
                    ElseIf strVeh = "" And strVehExp <> "" And intAccPurpose = objGLset.EnumAccountPurpose.others Then
                        lblVehCodeErr.Visible = True
                        Return True
                    Else
                        Return False
                    End If
                End If

            Case objINtx.EnumFuelIssueType.External
                If lstBillParty.SelectedItem.Value = "" Then
                    lblBillPartyErr.Visible = True
                    Return True
                End If
            Case objINtx.EnumFuelIssueType.StaffPayroll, objINtx.EnumFuelIssueType.StaffDN
                If lstEmpID.SelectedItem.Value = "" Then
                    lblEmpCodeErr.Visible = True
                    Return True
                End If
            Case Else
                Return False
        End Select
    End Function

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strTxLnParam As New StringBuilder()
        Dim strStatus As String
        Dim TxID As String
        Dim IssType As String
        Dim strAmount As String
        Dim StrTxParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strItem As String = Request.Form("lstItem").Trim

        Dim strAcc As String
        Dim strBlk As String
        Dim strVeh As String
        Dim strVehExp As String
        Dim strMarkUpYes As String
        Dim strMarkUpNo As String

        If ddlLocation.SelectedItem.Value.Trim() = "" Then
            lblLocationErr.Visible = True
            Exit Sub
        End If

        If IssueType.Text = objINtx.EnumStockIssueType.OwnUse Then
            strAcc = Request.Form("lstAccCode")
            If lstChargeLevel.selectedIndex = 0 Then
                strBlk = Request.Form("lstPreBlock")
            Else
                strBlk = Request.Form("lstBlock")
            End If
            strVeh = Request.Form("lstVehCode")
            strVehExp = Request.Form("lstVehExp")
        End If

        If CheckRequiredField() Then
            Exit Sub
        End If

        If CheckEntry() Then

            If IssueType.Text = objINtx.EnumFuelIssueType.StaffPayroll Or IssueType.Text = objINtx.EnumFuelIssueType.StaffDN Then
                IssType = lstBillTo.SelectedItem.Value
            Else
                IssType = IssueType.Text
            End If

            If lblFuelTxID.Text = "" Then
                StrTxParam.Append(lblFuelTxID.Text)
                StrTxParam.Append("||||||||")
                If IssueType.Text = objINtx.EnumFuelIssueType.External Then
                    StrTxParam.Append(lstBillParty.SelectedItem.Value)
                End If
                StrTxParam.Append("|")
                StrTxParam.Append(IssType)
                StrTxParam.Append("|0|")
                StrTxParam.Append(strRefRemark & " Remarks: " & txtRemarks.Text)
                StrTxParam.Append("|")
                StrTxParam.Append(strAccMonth)
                StrTxParam.Append("|")
                StrTxParam.Append(strAccYear)
                StrTxParam.Append("|")
                StrTxParam.Append(strLocation)
                StrTxParam.Append("|||")
                StrTxParam.Append(ddlLocation.SelectedItem.Value.Trim())

                Try
                    intErrNo = objINtx.mtdUpdFuelIssueDetail(strOpCdStckTxDet_ADD, _
                                                            strOpCdStckTxDet_UPD, _
                                                            strOpCdStckTxLine_GET, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            strARAccMonth, _
                                                            strARAccYear, _
                                                            StrTxParam.ToString, _
                                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FuelIssue), _
                                                            ErrorChk, _
                                                            TxID)
                    lblFuelTxID.Text = TxID
                    If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                        lblError.Visible = True
                    End If

                Catch Exp As System.Exception
                    If intErrNo <> -5 Then
                        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWFuelIssue&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                    End If
                End Try

            End If

            strTxLnParam.Append(lblFuelTxID.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strItem)
            strTxLnParam.Append("|")
            strTxLnParam.Append(txtQty.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(txtMeter.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(IssueType.Text)
            strTxLnParam.Append("|")

            If IssueType.Text = objINtx.EnumFuelIssueType.OwnUse Then
                strTxLnParam.Append(strAcc)
                strTxLnParam.Append("|")
                strTxLnParam.Append(strBlk)
                strTxLnParam.Append("|")
                strTxLnParam.Append(strVeh)
                strTxLnParam.Append("|")
                strTxLnParam.Append(strVehExp)
            Else
                strTxLnParam.Append("|||")
            End If
            strTxLnParam.Append("|")
            If IssueType.Text = objINtx.EnumFuelIssueType.StaffPayroll Or IssueType.Text = objINtx.EnumFuelIssueType.StaffDN Then
                strTxLnParam.Append(Request.Form("lstEmpID"))
            End If
            strTxLnParam.Append("|")
            strMarkUpYes = objINtx.EnumToCharge.Yes
            strMarkUpNo = objINtx.EnumToCharge.No
            strTxLnParam.Append(IIf(chkMarkUp.Checked , strMarkUpYes, strMarkUpNo))
            strTxLnParam.Append("|")
            strTxLnParam.Append(strLocation)
            Try
                If lstChargeLevel.selectedIndex = 0 And RowPreBlk.Visible = True Then
                    strParamList = ddlLocation.SelectedItem.Value.Trim() & "|" & _
                                   lstAccCode.SelectedItem.Value.Trim & "|" & _
                                   lstPreBlock.SelectedItem.Value.Trim & "|" & _
                                   objGLset.EnumBlockStatus.Active
                    intErrNo = objINtx.mtdAddFuelIssueLnByBlock(strOpCodeGLSubBlkByBlk, _
                                                                strParamList, _ 
                                                                strOpCdStckTxLine_ADD, _
                                                                strOpCdItem_Details_GET, _
                                                                strOpCdItem_Details_UPD, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strAccMonth, _
                                                                strAccYear, _
                                                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FuelIssueLN), _
                                                                ErrorChk, _
                                                                strTxLnParam.ToString, _
                                                                strLocType)

                Else
                    intErrNo = objINtx.mtdAddFuelIssueLn(strOpCdStckTxLine_ADD, _
                                                        strOpCdItem_Details_GET, _
                                                        strOpCdItem_Details_UPD, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FuelIssueLN), _
                                                        ErrorChk, _
                                                        strTxLnParam.ToString)
                End If
                Select Case ErrorChk
                    Case objINtx.EnumInventoryErrorType.OverFlow
                        lblError.Visible = True
                    Case objINtx.EnumInventoryErrorType.InsufficientQty
                        lblStock.Visible = True
                    Case objINtx.EnumInventoryErrorType.FuelMeterIncorrect
                        lblFuelMeter.Visible = True

                End Select

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDISSUELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                End If
            End Try

            StrTxParam.Remove(0, StrTxParam.Length)
            StrTxParam.Append(lblFuelTxID.Text)
            StrTxParam.Append("|||||||||||||||||")

            Try
                intErrNo = objINtx.mtdUpdFuelIssueDetail(strOpCdStckTxDet_ADD, _
                                                        strOpCdStckTxDet_UPD, _
                                                        strOpCdStckTxLine_GET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strARAccMonth, _
                                                        strARAccYear, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FuelIssue), _
                                                        ErrorChk, _
                                                        TxID)
                If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                    lblError.Visible = True
                End If

                lblFuelTxID.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWFuelIssue&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                End If
            End Try
            LoadStockTxDetails()
            DisplayFromDB(False)
            BindGrid()
            PageControl()
            BindItemCodeList(strItem)
            txtQty.Text = ""
            txtMeter.Text = ""

            If Not StrTxParam Is Nothing Then
                StrTxParam = Nothing
            End If

        End If

    End Sub

    Sub btnUpdate_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strOpCdFuelTxLine_Upd As String = "IN_CLSTRX_FUELISSUE_LINE_DETAILS_UPD"
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strTxLnParam As New StringBuilder()
        Dim strStatus As String
        Dim TxID As String
        Dim IssType As String
        Dim strAmount As String
        Dim StrTxParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strItem As String = Request.Form("lstItem").Trim

        Dim strAcc As String
        Dim strBlk As String
        Dim strVeh As String
        Dim strVehExp As String
        Dim strMarkUpYes As String
        Dim strMarkUpNo As String

        If ddlLocation.SelectedItem.Value.Trim() = "" Then
            lblLocationErr.Visible = True
            Exit Sub
        End If

        If IssueType.Text = objINtx.EnumStockIssueType.OwnUse Then
            strAcc = Request.Form("lstAccCode")
            If lstChargeLevel.selectedIndex = 0 Then
                strBlk = Request.Form("lstPreBlock")
            Else
                strBlk = Request.Form("lstBlock")
            End If
            strVeh = Request.Form("lstVehCode")
            strVehExp = Request.Form("lstVehExp")
        End If

        If CheckRequiredField() Then
            Exit Sub
        End If

        If CheckEntry() Then

            If IssueType.Text = objINtx.EnumFuelIssueType.StaffPayroll Or IssueType.Text = objINtx.EnumFuelIssueType.StaffDN Then
                IssType = lstBillTo.SelectedItem.Value
            Else
                IssType = IssueType.Text
            End If

            strTxLnParam.Append(lblTxLnID.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strItem)
            strTxLnParam.Append("|")
            strTxLnParam.Append(txtQty.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(txtMeter.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(IssueType.Text)
            strTxLnParam.Append("|")
            strTxLnParam.Append(lblOldQty.Text)
            strTxLnParam.Append("|")

            If IssueType.Text = objINtx.EnumFuelIssueType.OwnUse Then
                strTxLnParam.Append(strAcc)
                strTxLnParam.Append("|")
                strTxLnParam.Append(strBlk)
                strTxLnParam.Append("|")
                strTxLnParam.Append(strVeh)
                strTxLnParam.Append("|")
                strTxLnParam.Append(strVehExp)
            Else
                strTxLnParam.Append("|||")
            End If
            strTxLnParam.Append("|")
            If IssueType.Text = objINtx.EnumFuelIssueType.StaffPayroll Or IssueType.Text = objINtx.EnumFuelIssueType.StaffDN Then
                strTxLnParam.Append(Request.Form("lstEmpID"))
            End If

            strTxLnParam.Append("|")
            strTxLnParam.Append(lblOldItemCode.Text)
            strTxLnParam.Append("|")
            strMarkUpYes = objINtx.EnumToCharge.Yes
            strMarkUpNo = objINtx.EnumToCharge.No
            strTxLnParam.Append(IIf(chkMarkUp.Checked , strMarkUpYes, strMarkUpNo))

            Try
                If lstChargeLevel.selectedIndex = 0 And RowPreBlk.Visible = True Then
                    strParamList = ddlLocation.SelectedItem.Value.Trim() & "|" & _
                                   lstAccCode.SelectedItem.Value.Trim & "|" & _
                                   lstPreBlock.SelectedItem.Value.Trim & "|" & _
                                   objGLset.EnumBlockStatus.Active
                    intErrNo = objINtx.mtdUpdFuelIssueLnByBlock(strOpCodeGLSubBlkByBlk, _
                                                                strParamList, _ 
                                                                strOpCdStckTxLine_ADD, _
                                                                strOpCdItem_Details_GET, _
                                                                strOpCdItem_Details_UPD, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strAccMonth, _
                                                                strAccYear, _
                                                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FuelIssueLN), _
                                                                ErrorChk, _
                                                                strTxLnParam.ToString, _
                                                                strLocType)

                Else
                  intErrNo = objINtx.mtdUpdFuelIssueLn(strOpCdFuelTxLine_Upd, _
                                                        strOpCdItem_Details_GET, _
                                                        strOpCdItem_Details_UPD, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FuelIssueLN), _
                                                        ErrorChk, _
                                                        strTxLnParam.ToString)
               
                End If

                Select Case ErrorChk
                    Case objINtx.EnumInventoryErrorType.OverFlow
                        lblError.Visible = True
                    Case objINtx.EnumInventoryErrorType.InsufficientQty
                        lblStock.Visible = True
                    Case objINtx.EnumInventoryErrorType.FuelMeterIncorrect
                        lblFuelMeter.Visible = True

                End Select

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDISSUELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                End If
            End Try

            StrTxParam.Remove(0, StrTxParam.Length)
            StrTxParam.Append(lblFuelTxID.Text)
            StrTxParam.Append("|||||||||||||||||")

            Try
                intErrNo = objINtx.mtdUpdFuelIssueDetail(strOpCdStckTxDet_ADD, _
                                                        strOpCdStckTxDet_UPD, _
                                                        strOpCdStckTxLine_GET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strARAccMonth, _
                                                        strARAccYear, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FuelIssue), _
                                                        ErrorChk, _
                                                        TxID)
                If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                    lblError.Visible = True
                End If

                lblFuelTxID.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWFuelIssue&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                End If
            End Try

            lstChargeLevel.Enabled = True

            Initialize()
            LoadStockTxDetails()
            DisplayFromDB(False)
            dgStkTx.EditItemIndex = -1
            BindGrid()
            
            If Not StrTxParam Is Nothing Then
                StrTxParam = Nothing
            End If

           End If
    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim TxID As String
        Dim IssType As String
        Dim strAllowVehicle As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim StrTxParam As New StringBuilder()

        If ddlLocation.SelectedItem.Value.Trim() = "" Then
            lblLocationErr.Visible = True
            Exit Sub
        End If

        If IssueType.Text = objINtx.EnumFuelIssueType.External Then
            If CheckBillPartyField() Then
                Exit Sub
            End If
        End If

        If IssueType.Text = objINtx.EnumFuelIssueType.StaffPayroll Or IssueType.Text = objINtx.EnumFuelIssueType.StaffDN Then
            IssType = lstBillTo.SelectedItem.Value
        Else
            IssType = IssueType.Text
        End If

        If lblFuelTxID.Text = "" Then
            StrTxParam.Append(lblFuelTxID.Text)
            StrTxParam.Append("||||||||")
            If IssueType.Text = objINtx.EnumFuelIssueType.External Then
                StrTxParam.Append(lstBillParty.SelectedItem.Value)
            End If
            StrTxParam.Append("|")
            StrTxParam.Append(IssType)
            StrTxParam.Append("|0|")
            StrTxParam.Append(strRefRemark & " Remarks: " & txtRemarks.Text)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|||")
            StrTxParam.Append(ddlLocation.SelectedItem.Value.Trim())
        Else
            StrTxParam.Append(lblFuelTxID.Text)
            StrTxParam.Append("||||||||")
            If IssueType.Text = objINtx.EnumFuelIssueType.External Then
                StrTxParam.Append(lstBillParty.SelectedItem.Value)
            End If
            StrTxParam.Append("|")
            StrTxParam.Append(IssType)
            StrTxParam.Append("||")
            StrTxParam.Append(strRefRemark & " Remarks: " & txtRemarks.Text)
            StrTxParam.Append("||||||")
        End If
        Try
            intErrNo = objINtx.mtdUpdFuelIssueDetail(strOpCdStckTxDet_ADD, _
                                                    strOpCdStckTxDet_UPD, _
                                                    strOpCdStckTxLine_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strARAccMonth, _
                                                    strARAccYear, _
                                                    StrTxParam.ToString, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FuelIssue), _
                                                    ErrorChk, _
                                                    TxID)
            lblFuelTxID.Text = TxID
            If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                lblError.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWFuelIssue&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
            End If
        End Try
        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        LoadStockTxDetails()
        DisplayFromDB(False)
        PageControl()
    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim colParam As New Collection
        Dim colOpCode As New Collection
        Dim intError As Integer
        Dim strErrMsg As String
        
        If CheckLocBillParty() = False Then
            lblBPErr.Visible = True
            lblLocCodeErr.Visible = True
            Exit Sub
        End If

        colOpCode.Add("IN_CLSTRX_FUEL_ISSUE_GET_FOR_CONFIRM", "FUEL_ISSUE_GET_FOR_CONFIRM")
        colOpCode.Add("IN_CLSTRX_FUELISSUE_LINE_SYN", "FUEL_ISSUE_LINE_UPD")
        colOpCode.Add("IN_CLSTRX_FUELISSUE_DETAIL_UPD", "FUEL_ISSUE_UPD")
        colOpCode.Add("IN_CLSTRX_STOCKITEM_DETAILS_GET", "FUEL_ITEM_GET")
        colOpCode.Add("IN_CLSTRX_STOCKITEM_DETAIL_UPD", "FUEL_ITEM_UPD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_DETAIL_ADD", "JOURNAL_ADD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_DETAIL_UPD", "JOURNAL_UPD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_LINE_ADD", "JOURNAL_LINE_ADD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_LINE_GET", "JOURNAL_LINE_GET")
        
        colParam.Add(strCompany, "COMPANY")
        colParam.Add(strLocation, "LOCCODE")
        colParam.Add(strUserId, "USER_ID")
        colParam.Add(lblFuelTxID.Text, "FUEL_ISSUE_ID")
        colParam.Add(IssueType.Text, "ISSUE_TYPE")
        colParam.Add("Inter-" & GetCaption(objLangCap.EnumLangCap.Location), "MS_INTER_LOCATION")
        colParam.Add(GetCaption(objLangCap.EnumLangCap.Account), "MS_COA")
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), Session("SS_CONFIGSETTING")) = True Then
            colParam.Add(GetCaption(objLangCap.EnumLangCap.Block), "MS_BLOCK")
        Else
            colParam.Add(GetCaption(objLangCap.EnumLangCap.SubBlock), "MS_BLOCK")
        End If
        colParam.Add(GetCaption(objLangCap.EnumLangCap.Vehicle), "MS_VEHICLE")
        colParam.Add(GetCaption(objLangCap.EnumLangCap.VehExpense), "MS_VEHEXP")
        
        intError = objINtx.EnumTransactionError.NoError
        strErrMsg = ""
        
        Try
            intErrNo = objINtx.mtdFuelIssue_Confirm(colOpCode, _
                                                    colParam, _
                                                    intError, _
                                                    strErrMsg)
            
            If intError = objINtx.EnumTransactionError.NoError Then
                LoadStockTxDetails()
                DisplayFromDB(False)
                PageControl()
                BindGrid()
            Else
                lblConfirmErr.Text = strErrMsg
                lblConfirmErr.Visible = True
            End If
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CONFIRMISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
            End If
        End Try

        If TRIM(strLocation) <> TRIM(ddlLocation.SelectedItem.Value) Then
            Dim strDNParam As String = TRIM(lblFuelTxID.Text) & "|" & TRIM(IssueType.Text) & "|" & Trim(ddlLocation.SelectedItem.Value)
            Dim objDNId As Object
            Dim strDocTypeId As String = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNote) & "|" & _ 
                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNoteLn)

            Try
                intErrNo = objINtx.mtdAddBillingDebitNote_InterEstate(strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            Session("SS_ARACCMONTH"), _
                                                            Session("SS_ARACCYEAR"), _
                                                            strDocTypeId, _
                                                            strDNParam, _
                                                            2, _
                                                            objDNId)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_AUTO_DN_ADD&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
                End If
            End Try
        End If

    End Sub
    Sub btnDebitNote_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim strDocTypeId As String = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNote) & "|" & objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNoteLn)
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

        Select Case IssueType.Text
            Case objINtx.EnumFuelIssueType.StaffDN
                StrTxParam = lblFuelTxID.Text & "||||||||" & lstEmpID.SelectedItem.Value & "|" & IssueType.Text & "|||||||" & objINtx.EnumFuelIssueStatus.DBNote & "|"
            Case Else
                StrTxParam = lblFuelTxID.Text & "|||||||" & lstBillParty.SelectedItem.Value & "||" & IssueType.Text & "|||||||" & objINtx.EnumFuelIssueStatus.DBNote & "|"
        End Select

        Try
            intErrNo = objINtx.mtdUpdFuelIssueDetail(strOpCdStckTxDet_ADD, _
                                                    strOpCdStckTxDet_UPD, _
                                                    strOpCdStckTxLine_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strARAccMonth, _
                                                    strARAccYear, _
                                                    StrTxParam.ToString, _
                                                    strDocTypeId, _
                                                    ErrorChk, _
                                                    TxID)
            lblFuelTxID.Text = TxID
            If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                lblError.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWFuelIssue&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
            End If
        End Try
        LoadStockTxDetails()
        DisplayFromDB(True)
        PageControl()
        BindGrid()
    End Sub


    Sub btnPrint_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String
        Dim intStatus As Integer
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String
        Dim strFuelTxId As String
        Dim strIssueType As String
        Dim strDisplayCost As String = "0"

        Dim AccountTag As String
        Dim BlockTag As String
        Dim VehicleTag As String
        Dim VehExpenseTag As String

        AccountTag = strAccTag 
        BlockTag = strBlkTag
        VehicleTag = strVehTag
        VehExpenseTag = strVehExpCodeTag

        strFuelTxId = Trim(lblFuelTxID.Text)
        strUpdString = "where FuelIssueID = '" & strFuelTxId & "'"
        strStatus = Trim(Status.Text)
        intStatus = CInt(Trim(lblStatusHid.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strIssueType = Trim(IssueType.Text)
        strTable = "IN_FUELISSUE"
        strSortLine = ""

        If cblDisplayCost.Items(0).Selected Then
            strDisplayCost = "1"
        End If

        If intStatus = objINtx.EnumFuelIssueStatus.Confirmed Or intStatus = objINtx.EnumFuelIssueStatus.DbNote Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                        strUpdString, _
                                                        strTable, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_SHARE_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                End Try
                LoadStockTxDetails()
                DisplayFromDB(False)
                PageControl()
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_FuelIssueDet.aspx?strFuelIssueId=" & strFuelTxId & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strDisplayCost=" & strDisplayCost & _
                       "&strIssueType=" & strIssueType & _
                       "&strSortLine=" & strSortLine & _
                       "&AccountTag=" & AccountTag & _
                       "&BlockTag=" & BlockTag & _
                       "&VehicleTag=" & VehicleTag & _
                       "&VehExpenseTag=" & VehExpenseTag & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub






    Sub btnCancel_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

        Try
            intErrNo = objINtx.mtdAdjustFuelItemLevel(strOpCdStckTxLine_GET, _
                                                      strCompany, _
                                                      strLocation, _
                                                      strUserId, _
                                                      strAccMonth, _
                                                      strAccYear, _
                                                      strOpCdItem_Details_UPD, _
                                                      strOpCdItem_Details_GET, _
                                                      lblFuelTxID.Text, _
                                                      objINtx.EnumTransactionAction.Cancel, _
                                                      ErrorChk)
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=REMOVEINVENTORYITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
            End If

        End Try
        StrTxParam = lblFuelTxID.Text & "||||||||||||||||" & objINtx.EnumFuelIssueStatus.Cancelled & "|"
        If intErrNo = 0 And ErrorChk = objINtx.EnumInventoryErrorType.NoError Then
            Try
                intErrNo = objINtx.mtdUpdFuelIssueDetail(strOpCdStckTxDet_ADD, _
                                                        strOpCdStckTxDet_UPD, _
                                                        strOpCdStckTxLine_GET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strARAccMonth, _
                                                        strARAccYear, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FuelIssue), _
                                                        ErrorChk, _
                                                        TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CANCELFuelIssue&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                End If
            End Try
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            lblUnDel.Visible = True
        End If
        LoadStockTxDetails()
        DisplayFromDB(False)
        PageControl()
        BindGrid()

    End Sub

    Sub btnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

        If lblStatusHid.Text = CStr(objINtx.EnumFuelIssueStatus.Deleted) Then
            Try
                intErrNo = objINtx.mtdAdjustFuelItemLevel(strOpCdStckTxLine_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strOpCdItem_Details_UPD, _
                                                          strOpCdItem_Details_GET, _
                                                          lblFuelTxID.Text, _
                                                          objINtx.EnumTransactionAction.Undelete, _
                                                          ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UNDELETEFuelIssue&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                End If
            End Try
            StrTxParam = lblFuelTxID.Text & "||||||||||||||||" & objINtx.EnumFuelIssueStatus.Active & "|"
        Else
            Try
                intErrNo = objINtx.mtdAdjustFuelItemLevel(strOpCdStckTxLine_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strOpCdItem_Details_UPD, _
                                                          strOpCdItem_Details_GET, _
                                                          lblFuelTxID.Text, _
                                                          objINtx.EnumTransactionAction.Delete, _
                                                          ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETENEWFuelIssue&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                End If
            End Try
            StrTxParam = lblFuelTxID.Text & "||||||||||||||||" & objINtx.EnumFuelIssueStatus.Deleted & "|"
        End If

        If intErrNo = 0 And Not ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            Try
                intErrNo = objINtx.mtdUpdFuelIssueDetail(strOpCdStckTxDet_ADD, _
                                                        strOpCdStckTxDet_UPD, _
                                                        strOpCdStckTxLine_GET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strARAccMonth, _
                                                        strARAccYear, _
                                                        StrTxParam.ToString, _
                                                        objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.FuelIssue), _
                                                        ErrorChk, _
                                                        TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDNEWFuelIssue&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
                End If
            End Try
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            lblUnDel.Visible = True
        End If
        LoadStockTxDetails()
        DisplayFromDB(False)
        PageControl()
        BindGrid()

    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_FuelIssue_List.aspx")
    End Sub

    Sub btnNew_Click(ByVal sender As Object, Byval e As ImageClickEventArgs)
        Dim intIssueType As Integer = CInt(IssueType.Text)
        Dim strIssueType As String
        Select Case intIssueType
            Case objINtx.EnumFuelIssueType.OwnUse
                strIssueType = "Issue"
            Case objINtx.EnumFuelIssueType.StaffPayroll
                strIssueType = "Staff"
            Case objINtx.EnumFuelIssueType.External
                strIssueType = "External"
            Case Else
                strIssueType = "Issue"
        End Select
        Response.Redirect("IN_Trx_FuelIssue_Details.aspx?isType=" & strIssueType)
    End Sub

    Function CheckLocBillParty () As Boolean
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_FUELISSUE_DETAIL_GET"
        Dim strSearch As String
        Dim objBPLocDs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim intCnt As Integer
        Dim strLocCode As String
        Dim objFuelIssueDS As New DataSet()

        strParam = Trim(lblFuelTxID.Text)

        Try
            intErrNo = objINtx.mtdGetStockTransactDetails(strOpCdStckTxDet_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          objFuelIssueDS)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_FUELISSUELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
        End Try

        For intCnt = 0 To objFuelIssueDS.Tables(0).Rows.Count - 1
            strLocCode = TRIM(objFuelIssueDS.Tables(0).Rows(intCnt).Item("ChargeLocCode"))

            If Not (strLocCode = "" Or strLocCode = TRIM(strLocation)) Then
                strSearch = " AND BP.Status = '" & objGLset.EnumBillPartyStatus.Active & "'" & _
                            " AND BP.InterLocCode = '" & strLocCode & "'" 
                    
                Try
                    intErrNo = objGLset.mtdGetBPInterLoc(strOpCd_Get, strSearch, objBPLocDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_SETUP_BILLPARTY_GET&errmesg=" & lblErrMessage.Text & "&redirect=BI/setup/BI_setup_BillPartyDet.aspx")
                End Try

                If objBPLocDs.Tables(0).Rows.Count <= 0 Then
                    lblLocCodeErr.Text = strLocCode
                    return False
                End If
            End If
        Next intCnt

        return True
    End Function

End Class
