Imports System
Imports System.Data
Imports System.Math
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

Public Class IN_IssueDet : Inherits Page

    Protected lblDNIDTag As Label
    Dim strDateFMT As String


    Dim PreBlockTag As String
    

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
    Dim objNUSetup As New agri.NU.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()

    Dim strOpCdStckTxDet_ADD As String = "IN_CLSTRX_STOCKISSUE_DETAIL_ADD"
    Dim strOpCdStckTxDet_UPD As String = "IN_CLSTRX_STOCKISSUE_DETAIL_UPD"
    Dim strOpCdStckTxLine_GET As String = "IN_CLSTRX_STOCKISSUE_LINE_GET"
    Dim strOpCdLocList_GET As String = "ADMIN_CLSLOC_SYSLOC_LIST_GET"
    Dim strOpCdAccCode_GET As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
    Dim strOpCdItem_Details_GET As String = "IN_CLSSETUP_STOCKITEM_DETAILS_GET"
    Dim strOpCdItem_Details_UPD As String = "IN_CLSTRX_STOCKITEM_DETAIL_UPD"
    Dim objDataSet As New DataSet()
    Dim dsGrid As New DataSet()
    Dim objLangCapDs As New Dataset()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
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
    Dim strBatTag As String
    Dim strBlkTag As String
    Dim strVehTag As String
    Dim strVehExpCodeTag As String
    Dim strBillPartyTag As String
    Dim strRefSIS As String = ""
    Dim strRefDate As String = ""
    Dim strRefRemark As String = ""
    Dim intDummy As Integer = 0

    Dim strParamName As String
    Dim strParamValue As String
    Dim objPODs As New Object()
 
    Dim strLocType As String
    Dim strLocLevel As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim IssueTyp As String
        Dim TrxType As String
        Dim objDateFormat As String
        Dim strValidDate As String

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
        strDateFMT = Session("SS_DATEFMT")
        strLocLevel = Session("SS_LOCLEVEL")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockIssue), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            btnUpdate.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnUpdate).ToString())
            btnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnNew).ToString())
            btnSave.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnSave).ToString())
            btnConfirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnConfirm).ToString())
            btnCancel.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnCancel).ToString())
            btnPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnPrint).ToString())
            btnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnDelete).ToString())
            btnDebitNote.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnDebitNote).ToString())
            btnBack.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnBack).ToString())
 
            onload_GetLangCap()
            strRefSIS = IIf(Trim(txtRefSIS.Text) = "", "", Trim(txtRefSIS.Text))
            strRefDate = IIf(Trim(txtRefDate.Text) = "", "", Trim(txtRefDate.Text))

            If strRefSIS <> "" Or strRefDate <> "" Then
                strRefRemark = "SIS: " & strRefSIS & " Date: " & strRefDate
                'txtRefSIS.Text = ""
                'txtRefDate.Text = ""
            Else
                strRefRemark = ""
            End If

            lblInventoryBin.Visible = False

            If Not Page.IsPostBack Then
                If strLocLevel = "1" Then
                    BindInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central)
                ElseIf strLocLevel = "3" Then
                    BindInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO)
                Else
                    BindInventoryBinLevel("")
                End If
              
                TxtItemCode.Attributes.Add("readonly", "readonly")
                TxtItemName.Attributes.Add("readonly", "readonly")
                txtCost.Attributes.Add("readonly", "readonly")

                BindStorage("")
                BindVehicleCodeDropList("")
                BindAccCodeDropList("")
                BindChargeLevelDropDownList()
                BindLocationDropDownList(Trim(Session("SS_LOCATION")))
                lblStckTxID.Text = Request.QueryString("Id")

                TrxType = Request.QueryString("Type")
                lblStkName.Text = "STOCK ISSUE DETAIL"
                lblStkID.Text = "Stock Issue ID"

                If lblStckTxID.Text = "" Then
                    IssueTyp = Request.QueryString("isType")

                    Select Case IssueTyp
                        Case "Issue"
                            IssueType.Text = objINtx.EnumStockIssueType.OwnUse
                        Case "Staff"
                            IssueType.Text = objINtx.EnumStockIssueType.StaffPayroll
                        Case "External"
                            IssueType.Text = objINtx.EnumStockIssueType.External
                        Case "Nursery"
                            IssueType.Text = objINtx.EnumStockIssueType.Nursery
                    End Select
                    txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    TrLink.Visible = False
                Else
                    If strLocLevel = "1" Then
                        BindInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central)
                    ElseIf strLocLevel = "3" Then
                        BindInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO)
                    Else
                        BindInventoryBinLevel("")
                    End If
                    LoadStockTxDetails()

                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        DisplayFromDB(False)
                        BindGrid()
                        If IssueType.Text = objINtx.EnumStockIssueType.External Then
                            If dsGrid.Tables(0).Rows.Count > 0 Then
                                chkMarkUp.Enabled = False
                                If Trim(dsGrid.Tables(0).Rows(0).Item("ToCharge")) = objINtx.EnumToCharge.Yes Then
                                    chkMarkUp.Checked = True
                                Else
                                    chkMarkUp.Checked = False
                                End If
                            End If
                        End If
                    End If

                End If
                PageControl()
                'IssueTypeControl()
            End If

            lblEmpCodeErr.Visible = False
            lblErrNum.Visible = False
            lblErrStock.Visible = False
            lblUnDel.Visible = False
            lblConfirmErr.Visible = False
            lblAccCodeErr.Visible = False
            lblPreBlockErr.Visible = False
            lblBatchNoErr.Visible = False
            lblBlockHidden.Text = ""
            lblBatchHidden.Text = ""
            lblBlockErr.Visible = False
            lblVehCodeErr.Visible = False
            lblVehExpCodeErr.Visible = False
            lblItemCodeErr.Visible = False
            lblBillPartyErr.Visible = False
            lblBPErr.Visible = False
            lblLocCodeErr.Visible = False
            lblDate.Visible = False
            LblChargeLevel.Visible = False
        End If

    End Sub

    Sub BindStorage(ByVal pv_strcode As String)

        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer


        sSQLKriteria = "Select StorageCode,Description From IN_STORAGE Where LocCode='" & strLocation & "'"


        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objdsST.Tables(0).Rows.Count - 1
            objdsST.Tables(0).Rows(intCnt).Item("StorageCode") = Trim(objdsST.Tables(0).Rows(intCnt).Item("StorageCode"))
            objdsST.Tables(0).Rows(intCnt).Item("Description") = objdsST.Tables(0).Rows(intCnt).Item("StorageCode") & " (" & Trim(objdsST.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objdsST.Tables(0).Rows(intCnt).Item("StorageCode") = Trim(pv_strcode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objdsST.Tables(0).NewRow()
        dr("StorageCode") = ""
        dr("Description") = "Please Select Storage"
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        lstStorage.DataSource = objdsST.Tables(0)
        lstStorage.DataValueField = "StorageCode"
        lstStorage.DataTextField = "Description"
        lstStorage.DataBind()
        lstStorage.SelectedIndex = intSelectedIndex

    End Sub

    Sub BindChargeLevelDropDownList()
        lstChargeLevel.Items.Add("Select Charge Level")
        lstChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        lstChargeLevel.Items.Add(New ListItem(strBlkTag, objLangCap.EnumLangCap.SubBlock))
        lstChargeLevel.SelectedIndex=0 '''Session("SS_BLOCK_CHARGE_DEFAULT") '''0  ''
        ToggleChargeLevel()
    End Sub
    
    Sub lstChargeLevel_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        'lstPreBlock.SelectedIndex = 0
        'lstBlock.SelectedIndex = 0
        ''CheckVehicleUse()
        ToggleChargeLevel()
    End Sub
    
    Sub SelectedCOA_OnSelectedIndexChanged()
        lstChargeLevel.SelectedIndex=2
        lstPreBlock.SelectedIndex = 0
        lstBlock.SelectedIndex = 0
        CheckVehicleUse()
        ToggleChargeLevel()
    End Sub

    Sub ToggleChargeLevel()
        If lstChargeLevel.SelectedIndex = 1 Then
            RowBlk.Visible = False
            RowPreBlk.Visible = True
            hidBlockCharge.Value = "yes"
            'BindBatchNoList(Request.Form("lstPreBlock"), "")
			BindBatchNoList("", "")
        ElseIf lstChargeLevel.SelectedIndex = 2 Then
            RowBlk.Visible = True
            RowPreBlk.Visible = False
            hidBlockCharge.Value = ""
            'BindBatchNoList(Request.Form("lstBlock"), "")
			BindBatchNoList("", "")
        End If
    End Sub

    Sub BindPreBlkBatchNo(ByVal sender As Object, ByVal e As System.EventArgs)
        'BindBatchNoList(lstPreBlock.SelectedItem.Value, "")
		BindBatchNoList("", "")
    End Sub

    Sub BindBlkBatchNo(ByVal sender As Object, ByVal e As System.EventArgs)
        'BindBatchNoList(lstBlock.SelectedItem.Value, "")
		BindBatchNoList("", "")
    End Sub

    Sub ValidateBlkBatch(ByVal sender As Object, ByVal e As System.EventArgs)

        'Response.Write(lstItem.SelectedItem.Value)

        Select Case IssueType.Text
            Case objINtx.EnumStockIssueType.OwnUse
                BindValBlkBatch(TxtItemCode.Text.Trim)
            Case objINtx.EnumStockIssueType.StaffPayroll, objINtx.EnumStockIssueType.StaffDN
                BindValBlkBatch(TxtItemCode.Text.Trim)
            Case objINtx.EnumStockIssueType.External
                BindValBlkBatch(TxtItemCode.Text.Trim)
            Case objINtx.EnumStockIssueType.Nursery
                BindValBlkBatch(lstItem.SelectedItem.Value)                
        End Select

    End Sub

    Sub ddlLocation_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strAccCode As String = radcmbCOA.SelectedValue ''Request.Form("txtAccCode")
        Dim strVehCode As String = lstVehCode.SelectedValue 'Request.Form("lstVehCode")
        Dim strPreBlkCode As String = Request.Form("lstPreBlock")
        Dim strBlkCode As String = lstBlock.SelectedValue ''Request.Form("lstBlock")
        Dim strBatchNo As String = Request.Form("lstBatchNo") 
        Dim strChargeLevel As String = Request.Form("lstChargeLevel") 
        
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKISSUE_DET_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_trx_stockissue_list.aspx")
        End Try

        strAccTag = GetCaption(objLangCap.EnumLangCap.Account)
        strBatTag = GetCaption(objLangCap.EnumLangCap.BatchNo) 
        lblBatchNoTag.Text = strBatTag
        strVehTag = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strVehExpCodeTag = GetCaption(objLangCap.EnumLangCap.VehExpense)
        strBillPartyTag = GetCaption(objLangCap.EnumLangCap.BillParty)

        lblAccTag.Text = strAccTag & lblCode.Text & " :* "
        lblBlkTag.Text = strBlkTag & lblCode.Text & " : "
        lblVehTag.Text = strVehTag & lblCode.Text & " : "
        lblVehExpTag.text = strVehExpCodeTag & lblCode.text & " : "
        lblBPartyTag.Text = strBillPartyTag & " :*"

        lblAccCodeErr.Text = "<Br>" & lblPleaseSelect.Text & strAccTag
        lblBlockErr.Text = lblPleaseSelect.Text & strBlkTag
        lblVehCodeErr.Text = lblPleaseSelect.Text & strVehTag
        lblVehExpCodeErr.Text = lblPleaseSelect.Text & strVehExpCodeTag
        lblBillPartyErr.Text = lblPleaseSelect.Text & strBillPartyTag

        AccountCode.text = strAccTag & lblCode.text
        dgStkTx.columns(2).headertext = strBlkTag & lblCode.text
        dgStkTx.Columns(3).HeaderText = strBlkTag 'strBatTag
        dgStkTx.Columns(4).HeaderText = strVehTag & lblCode.Text & "<br>" & strVehExpCodeTag & lblCode.Text
        'dgStkTx.columns(5).headertext = strVehExpCodeTag & lblCode.text
        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTag.Text = PreBlockTag & lblCode.Text & " : "
        lblPreBlockErr.Text = lblPleaseSelect.Text & PreBlockTag & lblCode.Text
        lblLocationErr.Text = lblPleaseSelect.Text & GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text
        lblBatchNoErr.Text = lblPleaseSelect.Text & "Batch No."
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKISSUE_DET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
        End Try

        lblTotCost.Text = "Total Cost :"
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

    Sub IssueTypeControl()

        Select Case IssueType.Text
            Case objINtx.EnumStockIssueType.OwnUse
                RowEmp.Visible = False
                RowAcc.Visible = True
                lblLocationTag.Visible = Session("SS_INTER_ESTATE_CHARGING")
                ddlLocation.Visible = Session("SS_INTER_ESTATE_CHARGING")
                RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
                ToggleChargeLevel()
                RowBatchNo.Visible = False
                RowVeh.Visible = True
                RowVehExp.Visible = True
                btnDebitNote.Visible = False
                BindPreBlock("", "")
                BindBlockDropList("")
                'BindBatchNoList("", "")
                BindVehicleCodeDropList("")
                BindVehicleExpDropList(True)
                txtDate.Enabled = False
                RowCost.Visible = True
                txtCost.Text = "0"
                ''txtCost.Enabled = False
                lblTotCost.Visible = True

                TxtItemCode.Visible = True
                TxtItemCode.Visible = True
                Find.Visible = True
                lstItem.Visible = False

                Select Case lblStatusHid.Text
                    Case CStr(objINtx.EnumStockIssueStatus.Confirmed), CStr(objINtx.EnumStockIssueStatus.Deleted), _
                         CStr(objINtx.EnumStockIssueStatus.Cancelled), CStr(objINtx.EnumStockIssueStatus.DbNote)
                        radcmbCOA.Enabled = False
                        lstChargeLevel.Enabled = False
                        lstPreBlock.Enabled = False
                        lstBlock.Enabled = False
                        lstBatchNo.Enabled = False
                        lstVehCode.Enabled = False
                        lstVehExp.Enabled = False
               
                        txtDate.Enabled = False
                    Case Else
                        radcmbCOA.Enabled = True
                        lstChargeLevel.Enabled = True
                        lstPreBlock.Enabled = True
                        lstBlock.Enabled = True
                        lstBatchNo.Enabled = False
                        lstVehCode.Enabled = True
                        lstVehExp.Enabled = True
                
                        txtDate.Enabled = True

                End Select

                'add by aam - 15/07/2010
                If strLocLevel = objAdminLoc.EnumLocLevel.HQ Then
                    txtCost.Enabled = True
                End If
                'If ddlInventoryBin.SelectedIndex = objINstp.EnumInventoryBinLevel.HO Then
                '    txtCost.Enabled = True
                'End If

                CheckVehicleUse()

            Case objINtx.EnumStockIssueType.StaffPayroll, objINtx.EnumStockIssueType.StaffDN
                If objINtx.EnumStockIssueType.StaffPayroll = IssueType.Text Then
                    btnDebitNote.Visible = False
                End If

                TxtItemCode.Visible = True
                TxtItemCode.Visible = True
                Find.Visible = True
                lstItem.Visible = False

                RowEmp.Visible = True
                RowAcc.Visible = False
                lblLocationTag.Visible = False
                ddlLocation.Visible = False
                RowChargeLevel.Visible = False
                RowBatchNo.Visible = False
                RowPreBlk.Visible = False
                RowBlk.Visible = False
                RowVeh.Visible = False
                RowVehExp.Visible = False
                RowCost.Visible = True

                lblBillTo.Visible = True
                lstBillTo.Visible = True
                BindEmployeeDropList()
                BindBillToList()


                Select Case lblStatusHid.Text
                    Case CStr(objINtx.EnumStockIssueStatus.Confirmed), CStr(objINtx.EnumStockIssueStatus.Deleted), _
                        CStr(objINtx.EnumStockIssueStatus.Cancelled), CStr(objINtx.EnumStockIssueStatus.DbNote)
                        lstEmpID.Enabled = False
                        lstBillTo.Enabled = False
                        FindEmp.Visible = False
                    Case Else
                        lstEmpID.Enabled = True
                        lstBillTo.Enabled = True
                        FindEmp.Visible = True
                End Select

            Case objINtx.EnumStockIssueType.External


                TxtItemCode.Visible = True
                TxtItemCode.Visible = True
                Find.Visible = True
                lstItem.Visible = False

                RowEmp.Visible = False
                RowAcc.Visible = False
                lblLocationTag.Visible = False
                ddlLocation.Visible = False
                RowChargeLevel.Visible = False
                RowBatchNo.Visible = False
                RowPreBlk.Visible = False
                RowBlk.Visible = False
                RowVeh.Visible = False
                RowVehExp.Visible = False
                lblChargeMarkUp.Visible = True
                chkMarkUp.Visible = True
                lblBPartyTag.Visible = True
                lstBillParty.Visible = True
                BindBillPartyDropList()

                If Not lblStckTxID.Text = "" Then
                    lstBillParty.Enabled = False
                End If

                'change for oleion by pram at 12/12/08
                chkMarkUp.Checked = True
                chkMarkUp.Enabled = False
                RowCost.Visible = True
                lblTotCost.Visible = False
                lblTotAmtFig.Visible = False
                ddlInventoryBin.SelectedIndex = objINstp.EnumInventoryBinLevel.Central
                ddlInventoryBin.Enabled = False
                dgStkTx.Columns(7).Visible = False
                dgStkTx.Columns(8).Visible = False

            Case objINtx.EnumStockIssueType.Nursery

                TxtItemCode.Visible = False
                TxtItemCode.Visible = False
                Find.Visible = False
                lstItem.Visible = True

                RowEmp.Visible = False
                RowAcc.Visible = True

                lblLocationTag.Visible = Session("SS_INTER_ESTATE_CHARGING")
                ddlLocation.Visible = Session("SS_INTER_ESTATE_CHARGING")
                RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
                ToggleChargeLevel()
                RowBatchNo.Visible = True
                RowVeh.Visible = True
                RowVehExp.Visible = True
                btnDebitNote.Visible = False
                BindItemCodeList()
                BindPreBlock("", "")
                BindBlockDropList("")
                BindBatchNoList("", "")
                BindVehicleCodeDropList("")
                BindVehicleExpDropList(True)

                Select Case lblStatusHid.Text
                    Case CStr(objINtx.EnumStockIssueStatus.Confirmed), CStr(objINtx.EnumStockIssueStatus.Deleted), _
                         CStr(objINtx.EnumStockIssueStatus.Cancelled)
                        radcmbCOA.Enabled = False
                        lstChargeLevel.Enabled = False
                        lstPreBlock.Enabled = False
                        lstBlock.Enabled = False
                        lstBatchNo.Enabled = False
                        lstVehCode.Enabled = False
                        lstVehExp.Enabled = False
                    Case Else
                        radcmbCOA.Enabled = True
                        lstChargeLevel.Enabled = True
                        lstPreBlock.Enabled = True
                        lstBlock.Enabled = True
                        lstBatchNo.Enabled = True
                        lstVehCode.Enabled = True
                        lstVehExp.Enabled = True

                End Select
                CheckVehicleUse()
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
        btnSave.Visible = False
        btnConfirm.Visible = False
        btnPrint.Visible = False
        btnConfirm.Visible = False
        btnDelete.Visible = False
        btnDebitNote.Visible = False
        btnCancel.Visible = False
        btnNew.Visible = False
        ddlInventoryBin.Enabled = False

        Select Case Trim(lblStatusHid.Text)
            Case Trim(CStr(objINtx.EnumStockIssueStatus.Deleted))
                btnDelete.Visible = True
                btnNew.Visible = True
                btnDelete.ImageUrl = "../../images/butt_undelete.gif"
                btnDelete.AlternateText = "Undelete"
                btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objINtx.EnumStockIssueStatus.Confirmed))
                btnNew.Visible = True
                If IssueType.Text = objINtx.EnumStockIssueType.External Or _
                   IssueType.Text = objINtx.EnumStockIssueType.StaffDN Then
                    btnDebitNote.Visible = True
                End If
                btnPrint.Visible = True
                cblDisplayCost.Visible = True
                btnCancel.Visible = True
            Case Trim(CStr(objINtx.EnumStockIssueStatus.DbNote))
                btnNew.Visible = True
                btnPrint.Visible = True
                cblDisplayCost.Visible = True
            Case Else
                txtRefSIS.Enabled = True
                txtRefDate.Enabled = True
                txtRemarks.Enabled = True
                cblDisplayCost.Visible = True
                
                Find.Visible = True
                btnSave.Visible = True
                ddlInventoryBin.Enabled = True

                If Trim(lblStckTxID.Text) = "" Then
                    ddlLocation.Enabled = False
                Else
                    If intDummy > 0 Then
                        btnConfirm.Visible = False
                    Else
                        btnConfirm.Visible = True
                    End If
                    'btnConfirm.Visible = True
                    btnDelete.Visible = True
                    btnNew.Visible = True
                    btnDelete.ImageUrl = "../../images/butt_delete.gif"
                    btnDelete.AlternateText = "Delete"
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    If Trim(IssueType.Text) <> CStr(objINtx.EnumStockIssueType.External) Then
                        btnPrint.Visible = True
                        cblDisplayCost.Visible = True
                    End If
                End If
        End Select
        DisableItemTable()
    End Sub

    Sub DisableItemTable()

        If lblStatusHid.Text = CStr(objINtx.EnumStockIssueStatus.Deleted) Or _
           lblStatusHid.Text = CStr(objINtx.EnumStockIssueStatus.Cancelled) Or _
           lblStatusHid.Text = CStr(objINtx.EnumStockIssueStatus.DbNote) Then
            tblAdd.Visible = False
            
        ElseIf lblStatusHid.Text = CStr(objINtx.EnumStockIssueStatus.Confirmed) Then
            tblAdd.Visible = False
        Else
            tblAdd.Visible = True
        End If

        If IssueType.Text = objINtx.EnumStockIssueType.External Then
            ddlInventoryBin.Enabled = False
        End If

    End Sub

    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim lbl As Label

        Select Case e.Item.ItemType
            Case ListItemType.Header
                Select Case IssueType.Text
                    Case objINtx.EnumStockIssueType.OwnUse
                        e.Item.Cells(2).Text = AccountCode.Text
                        e.Item.Cells(4).Visible = False
                    Case objINtx.EnumStockIssueType.Nursery
                        e.Item.Cells(2).Text = AccountCode.Text
                        e.Item.Cells(4).Visible = True
                    Case objINtx.EnumStockIssueType.StaffPayroll, objINtx.EnumStockIssueType.StaffDN
                        e.Item.Cells(2).Text = EmployeeCode.Text
                        e.Item.Cells(3).Visible = True 'False
                        e.Item.Cells(4).Visible = False
                        e.Item.Cells(5).Visible = False
                        e.Item.Cells(6).Visible = False
                    Case objINtx.EnumStockIssueType.External
                        e.Item.Cells(2).Text = ""
                        e.Item.Cells(3).Visible = True 'False
                        e.Item.Cells(4).Visible = False
                        e.Item.Cells(5).Visible = False
                        e.Item.Cells(6).Visible = False
                End Select

            Case ListItemType.Item, ListItemType.AlternatingItem
                lbl = e.Item.FindControl("lblIdx")
                lbl.Text = e.Item.ItemIndex.ToString + 1

                Select Case IssueType.Text
                    Case objINtx.EnumStockIssueType.OwnUse
                        lbl = e.Item.FindControl("AccCode")
                        lbl.Visible = True
                        dgStkTx.Columns(3).Visible = True 'False
                        dgStkTx.Columns(4).Visible = False
                    Case objINtx.EnumStockIssueType.StaffPayroll, objINtx.EnumStockIssueType.StaffDN
                        lbl = e.Item.FindControl("PsEmpCode")
                        lbl.Visible = True
                        dgStkTx.Columns(3).Visible = True 'False
                        dgStkTx.Columns(4).Visible = False
                        dgStkTx.Columns(5).Visible = False
                        dgStkTx.Columns(6).Visible = False
                    Case objINtx.EnumStockIssueType.External
                        dgStkTx.Columns(3).Visible = True 'False
                        dgStkTx.Columns(4).Visible = False
                        dgStkTx.Columns(5).Visible = False
                        dgStkTx.Columns(6).Visible = False
                    Case objINtx.EnumStockIssueType.Nursery
                        lbl = e.Item.FindControl("AccCode")
                        lbl.Visible = True
                        dgStkTx.Columns(3).Visible = True 'True
                        dgStkTx.Columns(4).Visible = True
                End Select
        End Select

        If lblStatusHid.Text = CStr(objINtx.EnumStockIssueStatus.Active) Then
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim DeleteButton As LinkButton
                    Dim EditButton As LinkButton
                    DeleteButton = e.Item.FindControl("Delete")
                    DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    EditButton = e.Item.FindControl("Edit")
                    EditButton.Visible = True
                    EditButton = e.Item.FindControl("Cancel")
                    EditButton.Visible = False
            End Select
        ElseIf lblStatusHid.Text = CStr(objINtx.EnumStockIssueStatus.Confirmed) Then
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim DeleteButton As LinkButton
                    Dim EditButton As LinkButton
                    EditButton = e.Item.FindControl("Edit")
                    EditButton.Visible = True
                    DeleteButton = e.Item.FindControl("Delete")
                    DeleteButton.Visible = False
                    EditButton = e.Item.FindControl("Cancel")
                    EditButton.Visible = False
            End Select
        Else
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Dim DeleteButton As LinkButton
                    Dim EditButton As LinkButton
                    EditButton = e.Item.FindControl("Edit")
                    EditButton.Visible = False
                    DeleteButton = e.Item.FindControl("Delete")
                    DeleteButton.Visible = False
                    EditButton = e.Item.FindControl("Cancel")
                    EditButton.Visible = False
            End Select
        End If

    End Sub

    Sub DisplayFromDB(ByVal pv_blnIsRedirect As Boolean)
        Dim intDate1 As Integer
        Dim intDate2 As Integer

        lblStatusHid.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Status"))
        IssueType.Text = Trim(objDataSet.Tables(0).Rows(0).Item("IssueType"))
        Status.Text = objINtx.mtdGetStockIssueStatus(Trim(objDataSet.Tables(0).Rows(0).Item("Status")))
        CreateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("CreateDate")))
        UpdateDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("UpdateDate")))
        UpdateBy.Text = Trim(objDataSet.Tables(0).Rows(0).Item("UserName"))
        txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))
        txtDate.Text = objGlobal.GetShortDate(strDateFMT, Trim(objDataSet.Tables(0).Rows(0).Item("StockIssueDate")))


        If Trim(txtRemarks.Text) = "Remarks:" Then
            txtRemarks.Text = ""
        End If
        If txtRemarks.Text <> "" Then
            intDate1 = InStr(Trim(txtRemarks.Text), "Date:")
            If intDate1 > 0 Then
                txtRefSIS.Text = Right(Left(Trim(txtRemarks.Text), (intDate1 - 1)), (intDate1 - 6))
            Else
                txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))
            End If

            txtRefDate.Text = Right(Trim(txtRemarks.Text), (Len(Trim(txtRemarks.Text)) - (5 + Len(Trim(txtRefSIS.Text)))))
            intDate2 = InStr(Trim(txtRefDate.Text), "Remarks:")
            If intDate2 > 0 Then
                txtRefDate.Text = Right(Left(Trim(txtRefDate.Text), (intDate2 - 1)), (intDate2 - 7))
            Else
                txtRefDate.Text = ""
                txtRemarks.Text = Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))
            End If

            If Trim(txtRefDate.Text) <> "" And Trim(txtRefSIS.Text) <> "" Then
                txtRemarks.Text = Right(Trim(txtRemarks.Text), IIf((Len(Trim(txtRemarks.Text)) - (5 + Len(Trim(txtRefSIS.Text)) + 7 + Len(Trim(txtRefDate.Text)) + 10)) <= 0, 0, (Len(Trim(txtRemarks.Text)) - (5 + Len(Trim(txtRefSIS.Text)) + 7 + Len(Trim(txtRefDate.Text)) + 10))))
            ElseIf Trim(txtRefDate.Text) = "" And Trim(txtRefSIS.Text) <> "" Then
                txtRemarks.Text = Right(Trim(txtRemarks.Text), IIf((Len(Trim(txtRemarks.Text)) - (5 + Len(Trim(txtRefSIS.Text)) + 7 + 0 + 10)) <= 0, 0, (Len(Trim(txtRemarks.Text)) - (5 + Len(Trim(txtRefSIS.Text)) + 7 + 0 + 10))))
            ElseIf Trim(txtRefDate.Text) <> "" And Trim(txtRefSIS.Text) = "" Then
                txtRemarks.Text = Right(Trim(txtRemarks.Text), IIf((Len(Trim(txtRemarks.Text)) - (5 + 0 + 7 + Len(Trim(txtRefDate.Text)) + 10)) <= 0, 0, (Len(Trim(txtRemarks.Text)) - (5 + 0 + 7 + Len(Trim(txtRefDate.Text)) + 10))))
            Else
                txtRemarks.Text = Trim(Replace(UCase(Trim(objDataSet.Tables(0).Rows(0).Item("Remark"))), "REMARKS:", ""))
            End If
        End If

        lblTotAmtFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objDataSet.Tables(0).Rows(0).Item("TotalAmount"), 2), 2)
        lblTotPriceFig.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(objDataSet.Tables(0).Rows(0).Item("TotalPrice"), 2), 2)
        lblPrintDate.Text = objGlobal.GetLongDate(Trim(objDataSet.Tables(0).Rows(0).Item("PrintDate")))
        BindLocationDropDownList(Trim(objDataSet.Tables(0).Rows(0).Item("ChargeLocCode")))
        lblAccPeriod.Text = objDataSet.Tables(0).Rows(0).Item("AccMonth") & "/" & objDataSet.Tables(0).Rows(0).Item("AccYear")
        BindInventoryBinLevel(Trim(objDataSet.Tables(0).Rows(0).Item("Bin")))
        If Not Trim(objDataSet.Tables(0).Rows(0).Item("DNID")) = "" Then
            lblDNIDTag.Visible = True
            lblDNNoteID.Text = Trim(objDataSet.Tables(0).Rows(0).Item("DNID"))
            If pv_blnIsRedirect = True Then
                Response.Redirect("../../BI/trx/BI_trx_DNDet.aspx?dbnid=" & lblDNNoteID.Text & "&referer=" & Request.ServerVariables("SCRIPT_NAME") & "?Id=" & lblStckTxID.Text)
            End If
        End If
    End Sub

    Sub BindGrid()
        dgStkTx.DataSource = LoadDataGrid()
        dgStkTx.DataBind()
    End Sub

    Protected Function LoadDataGrid() As DataSet
        Dim strParam As String
        Dim strStorageCode As String = ""
        Dim intCnt As Integer = 0

        strParam = Trim(lblStckTxID.Text)

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
        End Try

        lstStorage.enabled = True
        If dsGrid.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
            For intCnt = 0 To dsGrid.Tables(0).Rows.Count - 1
                If UCase(Trim(dsGrid.Tables(0).Rows(intCnt).Item("AccCode"))) = "DUMMY" Then
                    intDummy = intDummy + 1
                End If
                lstStorage.Enabled = False
                strStorageCode = Trim(dsGrid.Tables(0).Rows(intCnt).Item("StorageCode"))
            Next
            BindStorage(strStorageCode)
        Else
            TrLink.Visible = False
        End If
         
        Return dsGrid
    End Function

    Sub LoadStockTxDetails()

        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_STOCKISSUE_DETAIL_GET"
        Dim StockCode As String

        strParam = Trim(lblStckTxID.Text)
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKISSUELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
        End Try

    End Sub

    Sub GetCOADetail(ByVal pv_strCode As String)
        ' 'Dim dr As DataRow
        ' Dim intCnt As Integer = 0
        ' Dim intErrNo As Integer
        ' Dim intSelectedIndex As Integer = 0


        ' Dim strOpCode As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        ' Dim objCOADs As New DataSet
        ' Dim strParamName As String = ""
        ' Dim strParamValue As String = ""

        ' strParamName = "SEARCHSTR|SORTEXP"
        ' strParamValue = " And ACC.AccCode = '" & Trim(pv_strCode) & "'  " & "|Order By ACC.AccCode"

        ' Try
        '     intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
        '                                         strParamName, _
        '                                         strParamValue, _
        '                                         objCOADs)
        ' Catch Exp As System.Exception
        '     Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        ' End Try

        ' If objCOADs.Tables(0).Rows.Count > 0 Then
        '     txtAccName.Text = objCOADs.Tables(0).Rows(0).Item("Description")
        ' End If
    End Sub

    Sub Initialize()
        txtQty.Text = ""
        'lstItem.SelectedIndex = 0

        TxtItemCode.Text = ""
        TxtItemName.Text = ""
        txtCost.Text = ""
        'lstEmpID.SelectedIndex = 0
        radcmbCOA.SelectedIndex=0

        lstBlock.SelectedIndex = 0
        lstVehCode.SelectedIndex = 0
        lstVehExp.SelectedIndex = 0
        txtAddNote.Value = ""
        btnAdd.Visible = True
        btnUpdate.Visible = False
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label
        Dim Delbutton As LinkButton
        Dim strItemCode As String
        Dim strItemName As String
        Dim strAcc As String
        Dim strBat As String
        Dim strBlk As String
        Dim strVeh As String
        Dim strVehExp As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim lblCharge As Label
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

        'edit confirmed
        If lblStatusHid.Text = CStr(objINtx.EnumStockIssueStatus.Confirmed) Then
            tblAdd.Visible = True
            radcmbCOA.Enabled = True
         
            lstChargeLevel.Enabled = True
            lstBlock.Enabled = True
            lstVehCode.Enabled = True
            lstVehExp.Enabled = True
            txtQty.Enabled = False
            Delbutton = E.Item.FindControl("Edit")
            Delbutton.Visible = False
            Delbutton = E.Item.FindControl("Cancel")
            Delbutton.Visible = True
        Else
            Delbutton = E.Item.FindControl("Edit")
            Delbutton.Visible = False
            Delbutton = E.Item.FindControl("Delete")
            Delbutton.Visible = False
            Delbutton = E.Item.FindControl("Cancel")
            Delbutton.Visible = True
        End If

        dgStkTx.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("LnID")

        chkMarkUp.Enabled = False
        lblCharge = E.Item.FindControl("lblToCharge")

        If Trim(lblCharge.Text) = objINtx.EnumToCharge.Yes Then
            chkMarkUp.Checked = True
        Else
            chkMarkUp.Checked = False
        End If

        lblTxLnID.Text = lbl.Text.Trim

        lbl = E.Item.FindControl("ItemCode")
        strItemCode = lbl.Text.Trim

        lbl = E.Item.FindControl("Description")
        strItemName = lbl.Text.Trim


        Select Case IssueType.Text
            Case objINtx.EnumStockIssueType.OwnUse
                TxtItemCode.Text = strItemCode
                TxtItemName.Text = strItemName
            Case objINtx.EnumStockIssueType.StaffPayroll, objINtx.EnumStockIssueType.StaffDN
                TxtItemCode.Text = strItemCode
                TxtItemName.Text = strItemName
            Case objINtx.EnumStockIssueType.External
                TxtItemCode.Text = strItemCode
                TxtItemName.Text = strItemName
            Case objINtx.EnumStockIssueType.Nursery
                GetItem(strItemCode)
        End Select


        lstChargeLevel.Items.Clear()
        BindChargeLevelDropDownList()
        'lstChargeLevel.SelectedIndex = 0

        Select Case IssueType.Text
            Case objINtx.EnumStockIssueType.OwnUse, objINtx.EnumStockIssueType.Nursery
                lbl = E.Item.FindControl("AccCode")
                strAcc = lbl.Text.Trim
                'BindAccCodeDropList(strAcc)
                radcmbCOA.SelectedValue = strAcc
            Case objINtx.EnumStockIssueType.StaffPayroll, objINtx.EnumStockIssueType.StaffDN
                lbl = E.Item.FindControl("PsEmpCode")
                strAcc = lbl.Text.Trim
                BindEmployeeDropList(strAcc)
        End Select

        lbl = E.Item.FindControl("lblBlkCode")
        strBlk = lbl.Text.Trim
        lbl = E.Item.FindControl("lblBatchNo")
        strBat = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVehCode")
        strVeh = lbl.Text.Trim
        lbl = E.Item.FindControl("lblVehExpCode")
        strVehExp = lbl.Text.Trim
        lbl = E.Item.FindControl("lblQtyTrx")
        txtQty.Text = lbl.Text.Trim
        lblOldQty.Text = CDbl(0 & lbl.Text)

        lbl = E.Item.FindControl("ItemCode")
        lblOldItemCode.Text = lbl.Text.Trim


        lbl = E.Item.FindControl("lblUnitCost")
        txtCost.Text = lbl.Text.Trim 'Trim(Replace(Replace(Replace(lbl.Text, ".", ""), ",", "."), "-", ""))

        lbl = E.Item.FindControl("lblAddNote")
        txtAddNote.Value = lbl.Text.Trim

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd)
        
        If intAccType = objGLset.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLset.EnumAccountPurpose.NonVehicle
                    BindBlockDropList(strAcc, strBlk)
                    ''BindVehicleCodeDropList("")
                    lstVehCode.Selectedvalue=strVeh
                    BindVehicleExpDropList(True)
                Case objGLset.EnumAccountPurpose.VehicleDistribution
                    BindBlockDropList("")
                    lstVehCode.Selectedvalue=strAcc
                    ''BindVehicleCodeDropList(strAcc, strVeh)
                    BindVehicleExpDropList(False, strVehExp)
                Case objGLset.EnumAccountPurpose.Others
                    BindBlockDropList(strAcc, strBlk)
                    lstVehCode.Selectedvalue=strVeh
                    ''BindVehicleCodeDropList("%", strVeh)
                    BindVehicleExpDropList(False, strVehExp)
            End Select
        ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet Or intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
            'BindBlockDropList(strAcc, strBlk)
            BindBlockBalanceSheetDropList(strAcc, strBlk)
           '' BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)

        Else
            BindBlockDropList("")
            ''BindVehicleCodeDropList("", strVeh)
            lstVehCode.Selectedvalue=strVeh
            BindVehicleExpDropList(True)
        End If

        'BindGrid()

        If lblStckTxID.Text <> "" Then
            btnAdd.Visible = False
            btnUpdate.Visible = True
        Else
            btnAdd.Visible = True
            btnUpdate.Visible = False
        End If

        lstChargeLevel.SelectedIndex=lblCharge.text.trim
        ToggleChargeLevel()
        'lstChargeLevel.Enabled = False
        RowBlk.Visible = True
        RowPreBlk.Visible = False
        lstItem.Enabled = False
        TxtItemCode.Enabled = False
 

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgStkTx.EditItemIndex = -1
        DisableItemTable()
        BindGrid()
        Initialize()
        lstChargeLevel.Enabled = True
        lblTxLnID.Text = ""

    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
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

        If lblStatusHid.Text = CStr(objINtx.EnumStockIssueStatus.Active) Then

            Dim strOpCdStckTxLine_Det_GET As String = "IN_CLSTRX_STOCKISSUE_LINE_DETAILS_GET"
            Dim strOpCdStckTxLine_DEL As String = "IN_CLSTRX_STOCKISSUE_LINE_DEL"
            Dim lbl As Label
            Dim LnID As String
            Dim ItemCode As String
            Dim strParam As String
            Dim StrTxParam As String
            Dim TxID As String
            Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError

            lbl = E.Item.FindControl("LnID")
            LnID = lbl.Text
            lbl = E.Item.FindControl("ItemCode")
            ItemCode = lbl.Text

            strParam = LnID & "|" & ItemCode & "|" & Trim(lblStckTxID.Text)
            Try
                intErrNo = objINtx.mtdDelStockTransactLn(strOpCdStckTxLine_DEL, _
                                                        strOpCdStckTxLine_Det_GET, _
                                                        strOpCdItem_Details_UPD, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_STOCKISSUELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End If
            End Try

            StrTxParam = lblStckTxID.Text & "||||||||||||||||||||"
            Try
                intErrNo = objINtx.mtdUpdStockIssueDetail(strOpCdStckTxDet_ADD, _
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
                                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue), _
                                                          ErrorChk, _
                                                          TxID)

                If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                    lblErrNum.Visible = True
                End If

                'lblStckTxID.Text = TxID
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_NEWSTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End If
            End Try
            LoadStockTxDetails()
            DisplayFromDB(False)
            BindGrid()
            BindItemCodeList()
        End If
    End Sub

    Sub BindBillToList()
        lstBillTo.Items.Clear()
        lstBillTo.Items.Add(New ListItem(objINtx.mtdGetStockIssueType(objINtx.EnumStockIssueType.StaffPayroll), objINtx.EnumStockIssueType.StaffPayroll))
        lstBillTo.Items.Add(New ListItem(objINtx.mtdGetStockIssueType(objINtx.EnumStockIssueType.StaffDN), objINtx.EnumStockIssueType.StaffDN))
        If Not lblStckTxID.Text = "" Then
            Select Case Trim(objDataSet.Tables(0).Rows(0).Item("IssueType"))
                Case objINtx.EnumStockIssueType.StaffPayroll
                    lstBillTo.SelectedIndex = 0
                Case objINtx.EnumStockIssueType.StaffDN
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

        If objBlkDs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & PreBlockTag & lblCode.Text

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
                strParam = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLset.EnumBlockStatus.Active
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
       '' dr("Description") = lblSelect.Text & strBlkTag & lblCode.Text
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

    Sub BindPreBlockBalanceSheet(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0


        strOpCd = "GL_CLSSETUP_ACCOUNT_PREBLOCK_BALANCESHEET_GET"

        strParamName = "ACCCODE|LOCCODE|STATUS"
        strParamValue = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLset.EnumBlockStatus.Active
        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                            strParamName, _
                                            strParamValue, _
                                            objPODs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objPODs.Tables(0).Rows(intCnt).Item("Description") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objPODs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If objPODs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objPODs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & PreBlockTag & lblCode.Text

        objPODs.Tables(0).Rows.InsertAt(dr, 0)
        lstPreBlock.DataSource = objPODs.Tables(0)
        lstPreBlock.DataValueField = "BlkCode"
        lstPreBlock.DataTextField = "Description"
        lstPreBlock.DataBind()
        lstPreBlock.SelectedIndex = intSelectedIndex

        If Not objPODs Is Nothing Then
            objPODs = Nothing
        End If
    End Sub

    Sub BindBlockBalanceSheetDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim dr As DataRow

        Try

            strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_BALANCE_SHEET_GET"

            strParamName = "ACCCODE|LOCCODE|STATUS"
            strParamValue = pv_strAccCode & "|" & ddlLocation.SelectedItem.Value.Trim() & "|" & objGLset.EnumBlockStatus.Active

            intErrNo = objGLtrx.mtdGetDataCommon(strOpCdBlockList_Get, _
                                            strParamName, _
                                            strParamValue, _
                                            objPODs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objPODs.Tables(0).Rows(intCnt).Item("Description") = Trim(objPODs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objPODs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objPODs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If objPODs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objPODs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & strBlkTag & lblCode.Text
        objPODs.Tables(0).Rows.InsertAt(dr, 0)

        lstBlock.DataSource = objPODs.Tables(0)
        lstBlock.DataValueField = "BlkCode"
        lstBlock.DataTextField = "Description"
        lstBlock.DataBind()
        lstBlock.SelectedIndex = intSelectedIndex

        If Not objPODs Is Nothing Then
            objPODs = Nothing
        End If
    End Sub

    Sub BindValBlkBatch(ByVal pv_strItem As String)

        Dim strOpCode As String
        If lstChargeLevel.SelectedIndex = 1 Then
            strOpCode = "IN_CLSTRX_NURSERYITEM_BLKBATCH_GET"
        ElseIf lstChargeLevel.SelectedIndex = 2 Then
            strOpCode = "IN_CLSTRX_NURSERYITEM_SUBBLKBATCH_GET"
        End If
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr1 As DataRow
        Dim dr2 As DataRow
        Dim dsForDropDown As DataSet
        Dim dsForCheckItem As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        If objINtx.EnumStockIssueType.External And chkMarkUp.Checked = True Then
            strOpCode = "IN_CLSTRX_ITEM_DETAIL_GET"

            strParamName = "ITEMCODE|LOCCODE"
            strParamValue = Trim(pv_strItem) & "|" & strLocation

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsForDropDown)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_GET_NURSERY_ITEM_BY_BATCH&errmesg=" & Exp.ToString() & "&redirect=IN/Trx/IN_Trx_StockIssue_Details.aspx")
            End Try

            If dsForDropDown.Tables(0).Rows.Count > 0 Then
                txtCost.Text = Trim(dsForDropDown.Tables(0).Rows(0).Item("SellFixedPrice"))
                TxtItemName.Text = Trim(dsForDropDown.Tables(0).Rows(0).Item("Description"))
            Else
                txtCost.Text = "0"
            End If
            'add by aam
        ElseIf objINtx.EnumStockIssueType.OwnUse Then 'And ddlInventoryBin.SelectedIndex = objINstp.EnumInventoryBinLevel.HO Then
            strOpCode = "IN_CLSTRX_STOCKISSUE_LINE_GET_ITEM"
            strParamName = "STOCKTXID|LOCCODE|ITEMCODE"
            strParamValue = Trim(lblStckTxID.Text) & "|" & strLocation & "|" & Trim(pv_strItem)

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsForCheckItem)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEM_DETAIL_GET&errmesg=" & Exp.ToString() & "&redirect=IN/Trx/IN_Trx_StockIssue_Details.aspx")
            End Try

            If dsForCheckItem.Tables(0).Rows.Count > 0 Then
                txtCost.Text = Trim(dsForCheckItem.Tables(0).Rows(0).Item("Cost"))
            Else
                strOpCode = "IN_CLSTRX_ITEM_DETAIL_GET"
                strParamName = "ITEMCODE|LOCCODE"
                strParamValue = Trim(pv_strItem) & "|" & strLocation

                Try
                    intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                        strParamName, _
                                                        strParamValue, _
                                                        dsForDropDown)

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEM_DETAIL_GET&errmesg=" & Exp.ToString() & "&redirect=IN/Trx/IN_Trx_StockIssue_Details.aspx")
                End Try

                If dsForDropDown.Tables(0).Rows.Count > 0 Then
                    txtCost.Text = Trim(dsForDropDown.Tables(0).Rows(0).Item("AverageCost"))
                    TxtItemName.Text = Trim(dsForDropDown.Tables(0).Rows(0).Item("Description"))
                Else
                    TxtItemName.Text = ""
                    txtCost.Text = "0"
                End If
            End If

        Else
            strParam = pv_strItem & "|" & _
                   objNUSetup.EnumNurseryBatchStatus.Active & "|" & _
                   strLocation

            Try
                intErrNo = objINtx.mtdGetNurseryItemByBatch(strOpCode, strParam, dsForDropDown)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_GET_NURSERY_ITEM_BY_BATCH&errmesg=" & Exp.ToString() & "&redirect=IN/Trx/IN_Trx_StockIssue_Details.aspx")
            End Try
        End If

        Try
            For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
                If Not pv_strItem = "" Then
                    If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("ItemCode")) = Trim(pv_strItem) Then
                        dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode"))
                        dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
                        dsForDropDown.Tables(0).Rows(intCnt).Item("BatchNo") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BatchNo"))
                        dsForDropDown.Tables(0).Rows(intCnt).Item("PlantMaterial") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BatchNo")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("PlantMaterial")) & ")"
                        lblBlockHidden.Text = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode"))
                        lblBatchHidden.Text = dsForDropDown.Tables(0).Rows(intCnt).Item("BatchNo")
                        intSelectedIndex = intCnt + 1
                    End If
                End If
            Next intCnt

            If lstChargeLevel.SelectedIndex = 1 Then
                dr1 = dsForDropDown.Tables(0).NewRow()
                dr1("BlkCode") = ""
                dr1("Description") = lblSelect.Text & PreBlockTag & lblCode.Text
                dsForDropDown.Tables(0).Rows.InsertAt(dr1, 0)

                lstPreBlock.DataSource = dsForDropDown.Tables(0)
                lstPreBlock.DataValueField = "BlkCode"
                lstPreBlock.DataTextField = "Description"
                lstPreBlock.DataBind()
                lstPreBlock.SelectedIndex = intSelectedIndex
            ElseIf lstChargeLevel.SelectedIndex = 2 Then
                dr1 = dsForDropDown.Tables(0).NewRow()
                dr1("BlkCode") = ""
                dr1("Description") = lblSelect.Text & strBlkTag & lblCode.Text
                dsForDropDown.Tables(0).Rows.InsertAt(dr1, 0)

                lstBlock.DataSource = dsForDropDown.Tables(0)
                lstBlock.DataValueField = "BlkCode"
                lstBlock.DataTextField = "Description"
                lstBlock.DataBind()
                lstBlock.SelectedIndex = intSelectedIndex
            End If

            dr2 = dsForDropDown.Tables(0).NewRow()

            dr2("BatchNo") = 0
            dr2("PlantMaterial") = lblSelect.Text & "Batch No."
            dsForDropDown.Tables(0).Rows.InsertAt(dr2, 0)

            lstBatchNo.DataSource = dsForDropDown.Tables(0)
            lstBatchNo.DataValueField = "BatchNo"
            lstBatchNo.DataTextField = "PlantMaterial"
            lstBatchNo.DataBind()
            lstBatchNo.SelectedIndex = intSelectedIndex

            If Not lblBlockHidden.Text = "" Then
                If lstChargeLevel.SelectedIndex = 1 Then
                    lstPreBlock.Enabled = False
                    'BindValBlkAcc(lblBlockHidden.Text)
                ElseIf lstChargeLevel.SelectedIndex = 2 Then
                    lstBlock.Enabled = False
                    'BindValSubBlkAcc(lblBlockHidden.Text)
                End If
            Else
                lstBlock.Enabled = True
                'BindAccCodeDropList()
            End If
        Catch Exp As System.Exception

        End Try

        If lblBatchHidden.Text = "" Or lblBatchHidden.Text = "-1" Then
            lstBatchNo.Enabled = True
        Else
            lstBatchNo.Enabled = False
        End If

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindBatchNoList(ByVal pv_strBlkCode As String, ByVal pv_strBatchNo As String)
        Dim strOpCode As String = "IN_CLSTRX_NURSERYBATCH_ITEM_GET_NEW"
        Dim strParam As String
        Dim dr As DataRow
        Dim dsForDropDown As DataSet
        Dim dsForInactiveItem As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim DataTextField As String

        strParam = pv_strBlkCode & "|" & _
                    pv_strBatchNo & "|" & _
                    objNUSetup.EnumNurseryBatchStatus.Active & "||" & _
                    strLocation & "|" & _
                    "NB.BatchNo" & "|"
        Try
            intErrNo = objNUSetup.mtdGetNurseryBatch(strOpCode, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDDISPATCH_DET_BIND_BATCHNO_DROPDOWNLIST&errmesg=" & Exp.ToString() & "&redirect=IN/Trx/IN_Trx_StockIssue_Details.aspx")
        End Try

        Try
            For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
                If Not pv_strBlkCode = "" Then
                    If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode")) = Trim(pv_strBlkCode) Then
                        dsForDropDown.Tables(0).Rows(intCnt).Item("BatchNo") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BatchNo"))
                        dsForDropDown.Tables(0).Rows(intCnt).Item("PlantMaterial") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BatchNo")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("PlantMaterial")) & ")"

                        If dsForDropDown.Tables(0).Rows(intCnt).Item("BatchNo") = Trim(pv_strBatchNo) Then
                            intSelectedIndex = intCnt + 1
                        End If
                    End If
                End If
            Next

            dr = dsForDropDown.Tables(0).NewRow()
            dr("BatchNo") = ""
            dr("PlantMaterial") = lblSelect.Text & "Batch No."
            dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

            lstBatchNo.DataSource = dsForDropDown.Tables(0)
            lstBatchNo.DataValueField = "BatchNo"
            lstBatchNo.DataTextField = "PlantMaterial"
            lstBatchNo.DataBind()
            lstBatchNo.SelectedIndex = 0
        Catch Exp As System.Exception

        End Try

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub CallCheckVehicleUse(ByVal sender As Object, ByVal e As System.EventArgs)
     
        CheckVehicleUse()
        
    End Sub

    Sub CheckVehicleUse()
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strBlkVal As String
        Dim strAcc As String = radcmbCOA.SelectedValue ''Request.Form("txtAccCode")
        Dim strPreBlk As String = Request.Form("lstPreBlock")
        Dim strBlk As String = Request.Form("lstBlock")
        Dim strBtch As String = Request.Form("lstBatchNo")  
        Dim strVeh As String =lstVehCode.SelectedValue ''Request.Form("lstVehCode")
        Dim strVehExp As String = Request.Form("lstVehExp")

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd)

        If lstChargeLevel.SelectedIndex = 1 Then
            strBlkVal = lstPreBlock.SelectedItem.Value
        ElseIf lstChargeLevel.SelectedIndex = 2 Then
            strBlkVal = lstBlock.SelectedItem.Value
        End If

        If intAccType = objGLset.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLset.EnumAccountPurpose.NonVehicle
                    If strBlkVal = "" Then
                        'BindPreBlock(strAcc, strPreBlk)
                        'BindBlockDropList(strAcc, strBlk)
                        If IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
                            If lstChargeLevel.SelectedIndex = 1 Then
                                'BindBatchNoList(strPreBlk, strBtch)
								BindBatchNoList("", "")
                            ElseIf lstChargeLevel.SelectedIndex = 2 Then
                                'BindBatchNoList(strBlk, strBtch)
								BindBatchNoList("", "")
                            End If

                        End If
                    End If
                    BindPreBlock(strAcc, strPreBlk)
                    BindBlockDropList(strAcc, strBlk)
                    BindVehicleCodeDropList("")
                    BindVehicleExpDropList(True)
                Case objGLset.EnumAccountPurpose.VehicleDistribution
                    If strBlkVal = "" Then
                        BindPreBlock("", "")
                        BindBlockDropList("")
                        If IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
                            BindBatchNoList("", "")
                        End If
                    End If
                    BindVehicleCodeDropList(strAcc, strVeh)
                    BindVehicleExpDropList(False, strVehExp)
                Case objGLset.EnumAccountPurpose.Others
                    If strBlkVal = "" Then
                        BindPreBlock(strAcc, strPreBlk)
                        BindBlockDropList(strAcc, strBlk)

                        If IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
                            If lstChargeLevel.SelectedIndex = 1 Then
                                'BindBatchNoList(strPreBlk, strBtch)
								BindBatchNoList("", "")
                            ElseIf lstChargeLevel.SelectedIndex = 2 Then
                                'BindBatchNoList(strBlk, strBtch)
								BindBatchNoList("", "")
                            End If
                        End If

                    End If
                    BindVehicleCodeDropList("%", strVeh)
                    BindVehicleExpDropList(False, strVehExp)
            End Select
        ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet Or intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
            'ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.Yes Then

            If strBlkVal = "" Then

                'BindPreBlock(strAcc, strPreBlk)
                'BindBlockDropList(strAcc, strBlk)
                BindPreBlockBalanceSheet(strAcc, strPreBlk)
                BindBlockBalanceSheetDropList(strAcc, strBlk)

                If IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
                    If lstChargeLevel.SelectedIndex = 1 Then
                        'BindBatchNoList(strPreBlk, strBtch)
						BindBatchNoList("", "")
                    ElseIf lstChargeLevel.SelectedIndex = 2 Then
                        'BindBatchNoList(strBlk, strBtch)
						BindBatchNoList("", "")
                    End If
                End If

            End If
            BindVehicleCodeDropList("")
            BindVehicleExpDropList(True)
            'ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet Then


        Else

            BindPreBlock("", "")
            BindBlockDropList("")
            If IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
                BindBatchNoList("", "")
            End If
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
            pr_strAccType = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
        End If
    End Sub

    Function CheckBillPartyField() As Boolean
        If lstBillParty.SelectedItem.Value = "" Then
            lblBillPartyErr.Visible = True
            Return True
        End If
    End Function

    Function CheckRequiredField() As Boolean
        'Dim strItem As String = Request.Form("lstItem").Trim
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strAcc As String
        Dim strBlk As String
        Dim strVeh As String
        Dim strVehExp As String

        If IssueType.Text = objINtx.EnumStockIssueType.OwnUse Or IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
            strAcc = radcmbCOA.SelectedValue ''Request.Form("txtAccCode").Trim
            If lstChargeLevel.SelectedIndex = 1 Then
                strBlk = lstPreBlock.SelectedItem.Value
            ElseIf lstChargeLevel.SelectedIndex = 2 Then
                strBlk = lstBlock.SelectedItem.Value
            End If
            strVeh =lstVehCode.SelectedValue  ''Request.Form("lstVehCode").Trim
            strVehExp = Request.Form("lstVehExp").Trim
            GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd)
        End If

		
        Select Case IssueType.Text
            Case objINtx.EnumStockIssueType.OwnUse
                If strAcc = "" Then
                    lblAccCodeErr.Visible = True
                    Return True
                End If

				
                If intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.No Then
                    Return False
                ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
                    If strBlk = "" Then
                        If lstChargeLevel.SelectedIndex = 1 Then
                            lblPreBlockErr.Visible = True
                        ElseIf lstChargeLevel.SelectedIndex = 2 Then
                            lblBlockErr.Visible = True
                        End If
                        Return True
                    Else
                        Return False
                    End If
                Else
                    If strBlk = "" And Not intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution Then
                        If lstChargeLevel.SelectedIndex = 1 Then
                            lblPreBlockErr.Visible = True
                        ElseIf lstChargeLevel.SelectedIndex = 2 Then
                            lblBlockErr.Visible = True
                        End If
                        Return True
                    ElseIf strVeh = "" And intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution Then
                        lblVehCodeErr.Visible = True
                        Return True
                    ElseIf strVehExp = "" And intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution Then
                        lblVehExpCodeErr.Visible = True
                        Return True
                    ElseIf strVeh <> "" And strVehExp = "" And intAccPurpose = objGLset.EnumAccountPurpose.Others Then
                        lblVehExpCodeErr.Visible = True
                        Return True
                    ElseIf strVeh = "" And strVehExp <> "" And intAccPurpose = objGLset.EnumAccountPurpose.Others Then
                        lblVehCodeErr.Visible = True
                        Return True
                    Else
                        Return False
                    End If
                End If

            Case objINtx.EnumStockIssueType.External
                If lstBillParty.SelectedItem.Value = "" Then
                    lblBillPartyErr.Visible = True
                    Return True
                End If
            Case objINtx.EnumStockIssueType.StaffPayroll, objINtx.EnumStockIssueType.StaffDN
                If lstEmpID.SelectedItem.Value = "" Then
                    lblEmpCodeErr.Visible = True
                    Return True
                End If

            Case objINtx.EnumStockIssueType.Nursery
                If strAcc = "" Then
                    lblAccCodeErr.Visible = True
                    Return True
                End If

                If intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.No Then
                    Return False
                ElseIf intAccType = objGLset.EnumAccountType.BalanceSheet And intNurseryInd = objGLset.EnumNurseryAccount.Yes Then
                    If strBlk = "" Then
                        If lstChargeLevel.SelectedIndex = 1 Then
                            lblPreBlockErr.Visible = True
                        ElseIf lstChargeLevel.SelectedIndex = 2 Then
                            lblBlockErr.Visible = True
                        End If
                        Return True
                    Else
                        Return False
                    End If
                Else
                    If strBlk = "" And Not intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution Then
                        If lstChargeLevel.SelectedIndex = 1 Then
                            lblPreBlockErr.Visible = True
                        ElseIf lstChargeLevel.SelectedIndex = 2 Then
                            lblBlockErr.Visible = True
                        End If
                        Return True
                    ElseIf strVeh = "" And intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution Then
                        lblVehCodeErr.Visible = True
                        Return True
                    ElseIf strVehExp = "" And intAccPurpose = objGLset.EnumAccountPurpose.VehicleDistribution Then
                        lblVehExpCodeErr.Visible = True
                        Return True
                    ElseIf strVeh <> "" And strVehExp = "" And intAccPurpose = objGLset.EnumAccountPurpose.Others Then
                        lblVehExpCodeErr.Visible = True
                        Return True
                    ElseIf strVeh = "" And strVehExp <> "" And intAccPurpose = objGLset.EnumAccountPurpose.Others Then
                        lblVehCodeErr.Visible = True
                        Return True
                    Else
                        Return False
                    End If
                End If
            Case Else
                Return False
        End Select
    End Function

    Sub BindVehicleCodeDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strVehCode As String = "")
        Dim dsForDropDown As DataSet
        Dim strOpCd As String
        Dim drinsert As DataRow
        Dim strParam As New StringBuilder()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        strParam.Append("| Status = '" & objGLSetup.EnumVehicleStatus.Active & "'")
        
        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam.ToString, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=CB/Trx/cb_trx_CashBankList.aspx")
        End Try

        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert("VehCode") = ""
       ' drinsert("_Description") = lblSelect.Text & strVehTag
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstVehCode.DataSource = dsForDropDown.Tables(0)
        lstVehCode.DataValueField = "VehCode"
        lstVehCode.DataTextField = "_Description"
        lstVehCode.DataBind()
        lstVehCode.SelectedIndex = 0

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
        drinsert(1) = "Select employee"
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If Not lblStckTxID.Text = "" Then
                If UCase(Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BillPartyCode"))) = UCase(Trim(objDataSet.Tables(0).Rows(0).Item("BillPartyCode"))) Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt
        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = ""
        drinsert(1) = lblSelect.text & strBillPartyTag
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        lstBillParty.DataSource = dsForDropDown.Tables(0)
        lstBillParty.DataValueField = "BillPartyCode"
        lstBillParty.DataTextField = "Name"
        lstBillParty.DataBind()

        If SelectedIndex = -1 And Not lblStckTxID.Text = "" Then

            Try
                strParam.Remove(0, strParam.Length)
                strParam.Append("||")
                strParam.Append(" AND BillPartyCode = '" & Trim(objDataSet.Tables(0).Rows(0).Item("psEmpCode")) & "'")
                strParam.Append("||")
                strParam.Append("|BillPartyCode|ASC")

                intErrNo = objGLtrx.mtdGetGLMasterList(strOpCdBillParty_Get, strParam.ToString, dsForInactiveItem)

            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_BILLPARTYNOTFOUND&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
            End Try

            If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                lstBillParty.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0)) & _
                " (" & objGlobal.mtdGetItemDBStatus(objGlobal.EnumItemDBStatus.Unknown) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item(0))))
                SelectedIndex = lstBillParty.Items.Count - 1
            Else 
                SelectedIndex = 0
            End If
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
        Dim strOpCdItem_List_GET As String = "IN_CLSTRX_ITEMPART_ITEM_GET" '"IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim dsItemCodeDropList As DataSet
        Dim intCnt As Integer
        Dim strparam As String
        Dim intSelectedIndex As Integer = 0

        If IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
            strparam = objINstp.EnumInventoryItemType.NurseryItem & "|" & objINstp.EnumStockItemStatus.Active & "|" & lblStckTxID.Text & "|" & "itm.ItemCode"
        Else
            strparam = objINstp.EnumInventoryItemType.Stock & "','" & objINstp.EnumInventoryItemType.WorkshopItem & "|" & objINstp.EnumStockItemStatus.Active & "|" & lblStckTxID.Text & "|" & "itm.ItemCode"
        End If

        Try
            intErrNo = objINstp.mtdGetFilteredItemList(strOpCdItem_List_GET, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strparam, _
                                                       objINtx.EnumInventoryTransactionType.StockIssue, _
                                                       dsItemCodeDropList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTOSERVTYPEDROPDOWNLIST&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
        End Try



        'For intCnt = 0 To dsItemCodeDropList.Tables(0).Rows.Count - 1
        '    dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")
        '    dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") & " ( " & _
        '                                                        dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
        '                                                        "Rp. " & objGlobal.GetIDDecimalSeparator(Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("AverageCost"))) & ", " & _
        '                                                        objGlobal.GetIDDecimalSeparator_FreeDigit(Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand")), 5) & ", " & _
        '                                                        Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode"))
        '    If dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(pv_strItemCode) Then
        '        intSelectedIndex = intCnt + 1
        '    End If
        'Next intCnt
        
        Dim drinsert As DataRow
        drinsert = dsItemCodeDropList.Tables(0).NewRow()
        drinsert("ItemCode") = ""
        drinsert("Description") = "Select Item Code"
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

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_STOCKISSUE_LINE_ADD"
        Dim strOpCodeGetItemDetail As String = "IN_CLSTRX_ITEM_DETAIL_GET"
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
   
        Dim strParamList As String
        Dim TxID As String
        Dim IssType As String
        Dim strStatus As String
        Dim strAmount As String
        Dim StrTxParam As New StringBuilder()
        Dim strTxLnParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strItem As String = ""
        Dim strBlkVal As String 
        Dim strAcc As String
        Dim strBlk As String
        Dim strBat As String
        Dim strVeh As String
        Dim strVehExp As String
        Dim strMarkUpYes As String
        Dim strMarkUpNo As String
        Dim strIDFormat As String
        Dim strNo As String
        Dim strOpCode As String = "IN_CLSTRX_STOCKISSUE_DETAIL_GETNO"
        Dim strOpCodeSR As String = "IN_CLSTRX_STOCKISSUE_STOCKRECEIVE_DETAILS_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim dtSet As New DataSet()
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


        If lstChargeLevel.SelectedIndex = 0 Then
            LblChargeLevel.Visible = True
            Exit Sub
        Else
            LblChargeLevel.Visible = False
        End If

        If ddlLocation.SelectedItem.Value.Trim() = "" Then
            lblLocationErr.Visible = True
            Exit Sub
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        If lstStorage.SelectedItem.Value = "" Then
            lblstoragemsg.Visible = True
            Exit Sub
        Else
            lblstoragemsg.Visible = False
        End If

        Select Case IssueType.Text
            Case objINtx.EnumStockIssueType.OwnUse
                strItem = Request.Form("txtItemCode")
            Case objINtx.EnumStockIssueType.StaffPayroll, objINtx.EnumStockIssueType.StaffDN
                strItem = Request.Form("txtItemCode")
            Case objINtx.EnumStockIssueType.External
                strItem = Request.Form("txtItemCode")
            Case objINtx.EnumStockIssueType.Nursery
                strItem = Request.Form("lstItem")
        End Select

        If strItem.Trim = "" Then
            lblItemCodeErr.Visible = True
            Exit Sub
        End If

        If IssueType.Text = objINtx.EnumStockIssueType.OwnUse Or IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
            If lstChargeLevel.SelectedIndex = 1 Then
                strBlkVal = lstPreBlock.SelectedItem.Value
            ElseIf lstChargeLevel.SelectedIndex = 2 Then
                strBlkVal = lstBlock.SelectedItem.Value
            End If
        End If

        If IssueType.Text = objINtx.EnumStockIssueType.OwnUse Or IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
            strAcc = radcmbCOA.SelectedValue ''Request.Form("txtAccCode")
            If strBlkVal = "" Then
                If lstChargeLevel.SelectedIndex = 1 Then
                    strBlk = Request.Form("lstPreBlock")
                ElseIf lstChargeLevel.SelectedIndex = 2 Then
                    strBlk = Request.Form("lstBlock")
                End If
            Else
                strBlk = strBlkVal
            End If

            If IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
                If lstBatchNo.SelectedItem.Value = "" Or lstBatchNo.SelectedItem.Value = "-1" Then
                    strBat = Request.Form("lstBatchNo")
                Else
                    strBat = lstBatchNo.SelectedItem.Value
                End If
            End If
            strVeh =lstVehCode.SelectedValue ''Request.Form("lstVehCode")
            strVehExp = Request.Form("lstVehExp")
        End If

        If CheckRequiredField() Then
            Exit Sub
        End If

        IF lGetCheckVehicle()=False Then
            Exit Sub
        End IF

        If IssueType.Text = objINtx.EnumStockIssueType.StaffPayroll Or IssueType.Text = objINtx.EnumStockIssueType.StaffDN Then
            IssType = lstBillTo.SelectedItem.Value
        Else
            IssType = IssueType.Text
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)


        ''////control stock by storage

        Dim vStockBalance As Double = lGetStockBalanceStorage(strDate, lstStorage.SelectedItem.value, 0)
        If vStockBalance < CDbl(0 & txtQty.text) Then
                UserMsgBox(Me, "Stock Balance Less Then Qty Issue " & vbCrLf & " Stock Balance : " & vStockBalance & "")
                txtQty.Focus
                Exit Sub
            End If

        strNewIDFormat = "SIS" & "/" & strCompany & "/" & strLocation & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & strAccYear & "/"

			
        If lblStckTxID.Text = "" Then
            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("||||||||")
            If IssueType.Text = objINtx.EnumStockIssueType.External Then
                StrTxParam.Append(lstBillParty.SelectedItem.Value)
            End If
            StrTxParam.Append("|")
            StrTxParam.Append(IssType)
            StrTxParam.Append("|0|")
            StrTxParam.Append(IIf(strRefRemark = "", txtRemarks.Text, IIf(Trim(txtRemarks.Text) = "", strRefRemark, strRefRemark & " Remarks: " & txtRemarks.Text)))
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|||")
            StrTxParam.Append(ddlLocation.SelectedItem.Value.Trim())
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|")
            StrTxParam.Append(Trim(ddlInventoryBin.SelectedItem.Value))
            StrTxParam.Append("|")
            StrTxParam.Append(strNewIDFormat)

            If IssueType.Text = objINtx.EnumStockIssueType.External Then
                strParamName = "LOCCODE|STOCKISSUEDATE"
                strParamValue = strLocation & "|" & strDate

                Try
                    intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                        strParamName, _
                                                        strParamValue, _
                                                        dtSet)

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_GET_NURSERY_ITEM_BY_BATCH&errmesg=" & Exp.ToString() & "&redirect=IN/Trx/IN_Trx_StockIssue_Details.aspx")
                End Try

                If dtSet.Tables(0).Rows.Count > 0 Then
                    strNo = dtSet.Tables(0).Rows.Count + 1
                Else
                    strNo = "1"
                End If

                strIDFormat = "OLN" & Trim(Day(strDate)) & IIf(Len(Trim(Month(strDate))) = 1, "0" & Trim(Month(strDate)), Trim(Month(strDate))) & Right(Trim(Year(strDate)), 2) & "-" & IIf(Len(strNo) = 1, "0" & Trim(strNo), Trim(strNo))

            Else
                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue)
            End If

            Try
                intErrNo = objINtx.mtdUpdStockIssueDetail(strOpCdStckTxDet_ADD, _
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
                                                          IIf(IssueType.Text = objINtx.EnumStockIssueType.External, strIDFormat, objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue)), _
                                                          ErrorChk, _
                                                          TxID)

                lblStckTxID.Text = TxID
                If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                    lblErrNum.Visible = True
                End If

                If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
                    lblErrStock.Visible = True
     
                End If

            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_NEWSTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End If
            End Try

        End If

        strTxLnParam.Append(lblStckTxID.Text)
        strTxLnParam.Append("|")
        strTxLnParam.Append(strItem)
        strTxLnParam.Append("|")
        strTxLnParam.Append(txtQty.Text)
        strTxLnParam.Append("|")
        strTxLnParam.Append(IssueType.Text)
        strTxLnParam.Append("|")

        If IssueType.Text = objINtx.EnumStockIssueType.OwnUse Then
            strTxLnParam.Append(strAcc)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strBlk)
            strTxLnParam.Append("||")
            strTxLnParam.Append(strVeh)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strVehExp)
        ElseIf IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
            strTxLnParam.Append(strAcc)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strBlk)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strBat)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strVeh)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strVehExp)
        Else
            strTxLnParam.Append("||||")
        End If
        strTxLnParam.Append("|")
        If IssueType.Text = objINtx.EnumStockIssueType.StaffPayroll Or IssueType.Text = objINtx.EnumStockIssueType.StaffDN Then
            strTxLnParam.Append(Request.Form("lstEmpID"))
        End If

        strTxLnParam.Append("|")
        strMarkUpYes = objINtx.EnumToCharge.Yes
        strMarkUpNo = objINtx.EnumToCharge.No
        strTxLnParam.Append(IIf(chkMarkUp.Checked, strMarkUpYes, strMarkUpNo))
        chkMarkUp.Enabled = False
        strTxLnParam.Append("|")
        strTxLnParam.Append(txtCost.Text)
        strTxLnParam.Append("|")
        strTxLnParam.Append(Trim(txtAddNote.Value))

        'If (objINtx.EnumStockIssueType.External And strMarkUpYes = objINtx.EnumToCharge.Yes) Or (objINtx.EnumStockIssueType.OwnUse And ddlInventoryBin.SelectedIndex = objINstp.EnumInventoryBinLevel.HO) Then
        '    strTxLnParam.Append(txtCost.Text)
        'Else
        '    strTxLnParam.Append("")
        'End If

        Try
            If lstChargeLevel.SelectedIndex = 1 And RowPreBlk.Visible = True Then
                strParamList = ddlLocation.SelectedItem.Value.Trim() & "|" & _
                               radcmbCOA.SelectedValue & "|" & _
                               lstPreBlock.SelectedItem.Value.Trim & "|" & _
                               objGLset.EnumBlockStatus.Active & "|" & _
                               strAccMonth & "|" & strAccYear

                intErrNo = objINtx.mtdAddStockIssueLnByBlock(strOpCodeGLSubBlkByBlk, _
                                                             strParamList, _
                                                             strOpCdStckTxLine_ADD, _
                                                             strOpCodeGetItemDetail, _
                                                             strOpCdItem_Details_UPD, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             ErrorChk, _
                                                             objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssueLn), _
                                                             strTxLnParam.ToString, _
                                                             strLocType)
            ElseIf lstChargeLevel.SelectedIndex = 2 Then

                intErrNo = objINtx.mtdAddStockIssueLn(strOpCdStckTxLine_ADD, _
                                                    strOpCodeGetItemDetail, _
                                                    strOpCdItem_Details_UPD, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    ErrorChk, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssueLn), _
                                                    strTxLnParam.ToString)
            End If

            Select Case ErrorChk
                Case objINtx.EnumInventoryErrorType.Overflow
                    lblErrNum.Visible = True
                Case objINtx.EnumInventoryErrorType.InsufficientQty
                    lblErrStock.Visible = True
            End Select

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ISSUELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
            End If
        End Try

        StrTxParam.Remove(0, StrTxParam.Length)
        StrTxParam.Append(lblStckTxID.Text)
        StrTxParam.Append("||||||||||||||||||" & strDate & "||")

        Try
            intErrNo = objINtx.mtdUpdStockIssueDetail(strOpCdStckTxDet_ADD, _
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
                                                      objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue), _
                                                      ErrorChk, _
                                                      TxID)
            If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                lblErrNum.Visible = True
            End If

            lblStckTxID.Text = TxID


        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_NEWSTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
            End If
        End Try

        '''---------UPDATE STORAGE SELECT - SUPAYA TIDAK MENGUBAH DLL
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet


        sSQLKriteria = "UPDATE IN_STOCKISSUELN SET StorageCode='" & lstStorage.SelectedItem.Value & "' Where StockIssueID='" & lblStckTxID.Text & "'"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        ''---------------------

        LoadStockTxDetails()
        DisplayFromDB(False)
        BindGrid()
        
        txtQty.Text = ""
        TxtItemCode.Text = ""
        TxtItemName.Text = ""
        radcmbCOA.SelectedIndex = 0

        lstItem.Enabled = True

        lstPreBlock.Items.Clear()
        

        PageControl()
        lstBlock.SelectedIndex = 0
        lstPreBlock.SelectedIndex = 0
        lstChargeLevel.SelectedIndex = 0

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        If Not strTxLnParam Is Nothing Then
            strTxLnParam = Nothing
        End If
    End Sub

    Sub btnUpdate_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strOpCdStckTxLine_ADD As String = "IN_CLSTRX_STOCKISSUE_LINE_ADD"
        Dim strOpCdStckTxLine_Upd As String = "IN_CLSTRX_STOCKISSUE_LINE_DETAILS_UPD"
        Dim strOpCodeGetItemDetail As String = "IN_CLSTRX_ITEM_DETAIL_GET"
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim TxID As String
        Dim IssType As String
        Dim strStatus As String
        Dim strAmount As String
        Dim strTxLnParam As New StringBuilder()
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim strItem As String = ""
        Dim StrTxParam As String
        Dim strBlkVal As String
        Dim strAcc As String
        Dim strBlk As String
        Dim strBat As String
        Dim strVeh As String
        Dim strVehExp As String
        Dim strMarkUpYes As String
        Dim strMarkUpNo As String
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



        Select Case IssueType.Text
            Case objINtx.EnumStockIssueType.OwnUse
                strItem = Trim(TxtItemCode.Text) 'Request.Form("txtItemCode")
            Case objINtx.EnumStockIssueType.StaffPayroll, objINtx.EnumStockIssueType.StaffDN
                strItem = Trim(TxtItemCode.Text) 'Request.Form("txtItemCode")
            Case objINtx.EnumStockIssueType.External
                strItem = Trim(TxtItemCode.Text) 'Request.Form("txtItemCode")
            Case objINtx.EnumStockIssueType.Nursery
                strItem = lstItem.SelectedItem.Value 'Request.Form("lstItem")
        End Select

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

        If ddlLocation.SelectedItem.Value.Trim() = "" Then
            lblLocationErr.Visible = True
            Exit Sub
        End If

        If lstChargeLevel.SelectedIndex = 1 Then
            strBlkVal = lstPreBlock.SelectedItem.Value
        ElseIf lstChargeLevel.SelectedIndex = 2 Then
            strBlkVal = lstBlock.SelectedValue
        Else
            LblChargeLevel.Visible = True
            Exit Sub
        End If

        If IssueType.Text = objINtx.EnumStockIssueType.OwnUse Or IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
            strAcc = Trim(radcmbCOA.SelectedValue)  ''Request.Form("txtAccCode")
            If strBlkVal = "" Then
                If lstChargeLevel.SelectedIndex = 1 Then
                    strBlk = Request.Form("lstPreBlock")
                ElseIf lstChargeLevel.SelectedIndex = 2 Then
                    strBlk =lstBlock.SelectedValue ''Request.Form("lstBlock")
                End If
            Else
                strBlk = strBlkVal
            End If

            If lstBatchNo.SelectedItem.Value = "" Or lstBatchNo.SelectedItem.Value = "-1" Then
                strBat = Request.Form("lstBatchNo")
            Else
                strBat = lstBatchNo.SelectedItem.Value
            End If
            strVeh =lstVehCode.SelectedValue ''Request.Form("lstVehCode")
            strVehExp = Request.Form("lstVehExp")
        End If

        If CheckRequiredField() Then
            Exit Sub
        End If


        'If lblTxLnID.Text.trim = "" Then
        '    ''///control stock by Storage
        'Dim vStockBalance As Double = lGetStockBalanceStorage(strDate, lstStorage.SelectedItem.value, 0)
        'Dim vStockBalance_Edit As Double = (vStockBalance + CDbl(0 & lblOldQty.text))

        'If vStockBalance_Edit < CDbl(0 & txtQty.text) Then
        '    UserMsgBox(Me, "Stock Balance Less Then Qty Issue " & vbCrLf & " Stock Balance : " & vStockBalance & "")
        '    txtQty.Focus
        '    Exit Sub
        'End If
        'End If


        Dim vStockBalance As Double = lGetStockBalanceStorage(strDate, lstStorage.SelectedItem.value, CDbl(0 & lblOldQty.text))
        If vStockBalance < CDbl(0 & txtQty.text) Then
            UserMsgBox(Me, "Stock Balance Less Then Qty Issue " & vbCrLf & " Stock Balance : " & vStockBalance & "")
            txtQty.Focus
            Exit Sub
        End If

        If IssueType.Text = objINtx.EnumStockIssueType.StaffPayroll Or IssueType.Text = objINtx.EnumStockIssueType.StaffDN Then
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
        strTxLnParam.Append(lblOldQty.Text)
        strTxLnParam.Append("|")
        strTxLnParam.Append(IssType)
        strTxLnParam.Append("|")

        If IssueType.Text = objINtx.EnumStockIssueType.OwnUse Then
            strTxLnParam.Append(strAcc)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strBlk)
            strTxLnParam.Append("||")
            strTxLnParam.Append(strVeh)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strVehExp)
        ElseIf IssueType.Text = objINtx.EnumStockIssueType.Nursery Then
            strTxLnParam.Append(strAcc)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strBlk)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strBat)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strVeh)
            strTxLnParam.Append("|")
            strTxLnParam.Append(strVehExp)
        Else
            strTxLnParam.Append("||||")
        End If

        strTxLnParam.Append("|")
        If IssueType.Text = objINtx.EnumStockIssueType.StaffPayroll Or IssueType.Text = objINtx.EnumStockIssueType.StaffDN Then
            strTxLnParam.Append(Request.Form("lstEmpID"))
        End If

        strTxLnParam.Append("|")
        strTxLnParam.Append(lblOldItemCode.Text)
        strTxLnParam.Append("|")
        strMarkUpYes = objINtx.EnumToCharge.Yes
        strMarkUpNo = objINtx.EnumToCharge.No
        strTxLnParam.Append(IIf(chkMarkUp.Checked, strMarkUpYes, strMarkUpNo))
        chkMarkUp.Enabled = False
        strTxLnParam.Append("|")
        strTxLnParam.Append(txtCost.Text)
        strTxLnParam.Append("|")
        strTxLnParam.Append(Trim(txtAddNote.Value).Replace("'", "''"))
        strTxLnParam.Append("|")
        strTxLnParam.Append(Trim(lblStckTxID.Text))

        Try
            If lstChargeLevel.SelectedIndex = 1 And RowPreBlk.Visible = True Then
                strParamList = ddlLocation.SelectedItem.Value.Trim() & "|" & _
                               Trim(radcmbCOA.SelectedValue)  & "|" & _
                               lstPreBlock.SelectedItem.Value.Trim & "|" & _
                               objGLset.EnumBlockStatus.Active

                intErrNo = objINtx.mtdUpdStockIssueLnByBlock(strOpCodeGLSubBlkByBlk, _
                                                             strParamList, _
                                                             strOpCdStckTxLine_ADD, _
                                                             strOpCodeGetItemDetail, _
                                                             strOpCdItem_Details_UPD, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             ErrorChk, _
                                                             objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssueLn), _
                                                             strTxLnParam.ToString, _
                                                             strLocType)

            ElseIf lstChargeLevel.SelectedIndex = 2 Then

                intErrNo = objINtx.mtdUpdStockIssueLn(strOpCdStckTxLine_Upd, _
                                                    strOpCodeGetItemDetail, _
                                                    strOpCdItem_Details_UPD, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    ErrorChk, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssueLn), _
                                                    strTxLnParam.ToString)
            End If

            Select Case ErrorChk
                Case objINtx.EnumInventoryErrorType.Overflow
                    lblErrNum.Visible = True
                Case objINtx.EnumInventoryErrorType.InsufficientQty
                    lblErrStock.Visible = True
            End Select

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ISSUELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
            End If
        End Try

        If intErrNo = objINtx.EnumTransactionError.NoError Then

            Dim strOpCode_ItemUpd As String = "IN_CLSTRX_STOCKISSUE_LINE_DETAILS_UPD_STOCKRTN"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""

            strParamName = "STOCKISSUEID|STOCKISSUELNID|ACCCODE|BLKCODE|VEHCODE|VEHEXPCODE|PSEMPCODE"

            strParamValue = Trim(lblStckTxID.Text) & _
                            "|" & Trim(lblTxLnID.Text) & _
                            "|" & strAcc & _
                            "|" & strBlk & _
                            "|" & strVeh & _
                            "|" & strVehExp & _
                            "|" & Request.Form("lstEmpID")

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
            End Try
        End If
        If Not strTxLnParam Is Nothing Then
            strTxLnParam = Nothing
        End If


        StrTxParam = lblStckTxID.Text & "|||||||||||" & IIf(strRefRemark = "", txtRemarks.Text, IIf(Trim(txtRemarks.Text) = "", strRefRemark, strRefRemark & " Remarks: " & txtRemarks.Text)) & _
                   "|||||||||"

        Try
            intErrNo = objINtx.mtdUpdStockIssueDetail(strOpCdStckTxDet_ADD, _
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
                                                      objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue), _
                                                      ErrorChk, _
                                                      TxID)

            If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                lblErrNum.Visible = True
            End If

            lblStckTxID.Text = TxID
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_NEWSTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
            End If
        End Try

        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        '''---------UPDATE STORAGE SELECT - SUPAYA TIDAK MENGUBAH DLL
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet


        sSQLKriteria = "UPDATE IN_STOCKISSUELN SET StorageCode='" & lstStorage.SelectedItem.Value & "' Where StockIssueID='" & lblStckTxID.Text & "'"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        ''---------------------


        lstChargeLevel.Enabled = True

        Select Case IssueType.Text
            Case objINtx.EnumStockIssueType.OwnUse
                TxtItemCode.Enabled = True
                TxtItemCode.Text = ""
                TxtItemName.Text = ""

            Case objINtx.EnumStockIssueType.StaffPayroll, objINtx.EnumStockIssueType.StaffDN
                TxtItemCode.Enabled = True
                TxtItemCode.Text = ""
                TxtItemName.Text = ""
            Case objINtx.EnumStockIssueType.External
                TxtItemCode.Enabled = True
                TxtItemCode.Text = ""
                TxtItemName.Text = ""
            Case objINtx.EnumStockIssueType.Nursery
                lstItem.Enabled = True
        End Select


        Initialize()
        LoadStockTxDetails()
        DisplayFromDB(False)

        txtQty.Text = ""
        TxtItemCode.Text = ""
        TxtItemName.Text = ""
        radcmbCOA.SelectedIndex=0
 
        lstItem.Enabled = True

        lstPreBlock.Items.Clear()
        'lstBlock.Items.Clear()

        PageControl()
        lstBlock.SelectedIndex = 0
        lstPreBlock.SelectedIndex = 0
        lstChargeLevel.SelectedIndex = 0

        dgStkTx.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim TxID As String
        Dim IssType As String
        Dim strAllowVehicle As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
        Dim StrTxParam As New StringBuilder()
        Dim strIDFormat As String
        Dim strNo As String
        Dim strOpCode As String = "IN_CLSTRX_STOCKISSUE_DETAIL_GETNO"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim dtSet As New DataSet()
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

        If lblStckTxID.Text = "" Then
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

        If ddlLocation.SelectedItem.Value.Trim() = "" Then
            lblLocationErr.Visible = True
            Exit Sub
        End If

        If IssueType.Text = objINtx.EnumStockIssueType.External Then
            If CheckBillPartyField() Then
                Exit Sub
            End If
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        IF lGetCheckVehicle()=False Then
            Exit Sub
        End IF

        If IssueType.Text = objINtx.EnumStockIssueType.StaffPayroll Or IssueType.Text = objINtx.EnumStockIssueType.StaffDN Then
            IssType = lstBillTo.SelectedItem.Value
        Else
            IssType = IssueType.Text
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strNewIDFormat = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue) & Right(strAccYear, 2) & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth)
        '    Case Else
        '        strNewIDFormat = "NPB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        strNewIDFormat = "NPB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        If lblStckTxID.Text = "" Then
            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("||||||||")
            If IssueType.Text = objINtx.EnumStockIssueType.External Then
                StrTxParam.Append(lstBillParty.SelectedItem.Value)
            End If
            StrTxParam.Append("|")
            StrTxParam.Append(IssType)
            StrTxParam.Append("|0|")
            StrTxParam.Append(IIf(strRefRemark = "", txtRemarks.Text, IIf(Trim(txtRemarks.Text) = "", strRefRemark, strRefRemark & " Remarks: " & txtRemarks.Text)))
            StrTxParam.Append("|")
            StrTxParam.Append(strAccMonth)
            StrTxParam.Append("|")
            StrTxParam.Append(strAccYear)
            StrTxParam.Append("|")
            StrTxParam.Append(strLocation)
            StrTxParam.Append("|||")
            StrTxParam.Append(ddlLocation.SelectedItem.Value.Trim())
            StrTxParam.Append("|")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|")
            StrTxParam.Append(Trim(ddlInventoryBin.SelectedItem.Value))
            StrTxParam.Append("|")
            StrTxParam.Append(strNewIDFormat)
        Else
            StrTxParam.Append(lblStckTxID.Text)
            StrTxParam.Append("||||||||")
            If IssueType.Text = objINtx.EnumStockIssueType.External Then
                StrTxParam.Append(lstBillParty.SelectedItem.Value)
            End If
            StrTxParam.Append("|")
            StrTxParam.Append(IssType)
            StrTxParam.Append("||")
            StrTxParam.Append(IIf(strRefRemark = "", txtRemarks.Text, IIf(Trim(txtRemarks.Text) = "", strRefRemark, strRefRemark & " Remarks: " & txtRemarks.Text)))
            StrTxParam.Append("|||||||")
            StrTxParam.Append(strDate)
            StrTxParam.Append("|")
            StrTxParam.Append(Trim(ddlInventoryBin.SelectedItem.Value))
            StrTxParam.Append("|")
            StrTxParam.Append(strNewIDFormat)
        End If

      
        If IssueType.Text = objINtx.EnumStockIssueType.External Then
            strParamName = "LOCCODE|STOCKISSUEDATE"
            strParamValue = strLocation & "|" & strDate

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dtSet)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_GET_NURSERY_ITEM_BY_BATCH&errmesg=" & Exp.ToString() & "&redirect=IN/Trx/IN_Trx_StockIssue_Details.aspx")
            End Try

            If dtSet.Tables(0).Rows.Count > 0 Then
                strNo = dtSet.Tables(0).Rows.Count + 1
            Else
                strNo = "1"
            End If
            strIDFormat = "OLN" & Trim(Day(strDate)) & IIf(Len(Trim(Month(strDate))) = 1, "0" & Trim(Month(strDate)), Trim(Month(strDate))) & Right(Trim(Year(strDate)), 2) & "-" & IIf(Len(strNo) = 1, "0" & Trim(strNo), Trim(strNo))
        Else
            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue)
        End If

        Try
            intErrNo = objINtx.mtdUpdStockIssueDetail(strOpCdStckTxDet_ADD, _
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
                                                      IIf(IssueType.Text = objINtx.EnumStockIssueType.External, strIDFormat, objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue)), _
                                                      ErrorChk, _
                                                      TxID)

            lblStckTxID.Text = TxID
            If ErrorChk = objINtx.EnumInventoryErrorType.Overflow Then
                lblErrNum.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_NEWSTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
            End If
        End Try
        If Not StrTxParam Is Nothing Then
            StrTxParam = Nothing
        End If

        '''---------UPDATE STORAGE SELECT - SUPAYA TIDAK MENGUBAH DLL
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet


        sSQLKriteria = "UPDATE IN_STOCKISSUELN SET StorageCode='" & lstStorage.SelectedItem.Value & "' Where StockIssueID='" & lblStckTxID.Text & "'"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        ''---------------------

        LoadStockTxDetails()
        DisplayFromDB(False)
        PageControl()
        
    End Sub

    Sub btnConfirm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim colParam As New Collection
        Dim colOpCode As New Collection
        Dim intError As Integer
        Dim strErrMsg As String
        Dim strMarkUpYes As String
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


        If lblStckTxID.Text = "" Then
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

        If CheckLocBillParty() = False Then
            lblBPErr.Visible = True
            lblLocCodeErr.Visible = True
            Exit Sub
        End If


        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        'colOpCode.Add("IN_CLSTRX_STOCK_ISSUE_GET_FOR_CONFIRM", "STOCK_ISSUE_GET_FOR_CONFIRM")
        'colOpCode.Add("IN_CLSTRX_STOCKISSUE_LINE_SYN", "STOCK_ISSUE_LINE_UPD")
        'colOpCode.Add("IN_CLSTRX_STOCKISSUE_DETAIL_UPD", "STOCK_ISSUE_UPD")
        'colOpCode.Add("IN_CLSTRX_STOCKITEM_DETAILS_GET", "STOCK_ITEM_GET")
        'colOpCode.Add("IN_CLSTRX_STOCKITEM_DETAIL_UPD", "STOCK_ITEM_UPD")
        'colOpCode.Add("GL_CLSTRX_JOURNAL_DETAIL_ADD", "JOURNAL_ADD")
        'colOpCode.Add("GL_CLSTRX_JOURNAL_DETAIL_UPD", "JOURNAL_UPD")
        'colOpCode.Add("GL_CLSTRX_JOURNAL_LINE_ADD", "JOURNAL_LINE_ADD")
        'colOpCode.Add("GL_CLSTRX_JOURNAL_LINE_GET", "JOURNAL_LINE_GET")
        'colOpCode.Add("IN_CLSTRX_ITEM_NURSERYBATCH_UPD", "NURSERYBATCH_ITEM_UPD")

        'colParam.Add(strCompany, "COMPANY")
        'colParam.Add(strLocation, "LOCCODE")
        'colParam.Add(strUserId, "USER_ID")
        'colParam.Add(lblStckTxID.Text, "STOCK_ISSUE_ID")
        'colParam.Add(IssueType.Text, "ISSUE_TYPE")
        'colParam.Add("Inter-" & GetCaption(objLangCap.EnumLangCap.Location), "MS_INTER_LOCATION")
        'colParam.Add(GetCaption(objLangCap.EnumLangCap.Account), "MS_COA")
        'If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), Session("SS_CONFIGSETTING")) = True Then
        '    colParam.Add(GetCaption(objLangCap.EnumLangCap.Block), "MS_BLOCK")
        'Else
        '    colParam.Add(GetCaption(objLangCap.EnumLangCap.SubBlock), "MS_BLOCK")
        'End If
        'colParam.Add(GetCaption(objLangCap.EnumLangCap.Vehicle), "MS_VEHICLE")
        'colParam.Add(GetCaption(objLangCap.EnumLangCap.VehExpense), "MS_VEHEXP")


        'intError = objINtx.EnumTransactionError.NoError
        'strErrMsg = ""

        'Try
        '    intErrNo = objINtx.mtdStockIssue_Confirm(colOpCode, _
        '                                             colParam, _
        '                                             intError, _
        '                                             strErrMsg)

        '    If intError = objINtx.EnumTransactionError.NoError Then
        '        LoadStockTxDetails()
        '        DisplayFromDB(False)
        '        PageControl()
        '        IssueTypeControl()
        '        BindGrid()
        '    Else
        '        lblConfirmErr.Text = strErrMsg 
        '        lblConfirmErr.Visible = True
        '    End If
        'Catch Exp As System.Exception
        '    If intErrNo <> -5 Then
        '        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CONFIRMISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
        '    End If
        'End Try

        'If Trim(strLocation) <> Trim(ddlLocation.SelectedItem.Value) Then
        '    Dim strDNParam As String = Trim(lblStckTxID.Text) & "|" & Trim(IssueType.Text) & "|" & Trim(ddlLocation.SelectedItem.Value)
        '    Dim objDNId As Object
        '    Dim strDocTypeId As String = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNote) & "|" & _
        '                                objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNoteLn)

        '    Try
        '        intErrNo = objINtx.mtdAddBillingDebitNote_InterEstate(strCompany, _
        '                                                    strLocation, _
        '                                                    strUserId, _
        '                                                    strAccMonth, _
        '                                                    strAccYear, _
        '                                                    Session("SS_ARACCMONTH"), _
        '                                                    Session("SS_ARACCYEAR"), _
        '                                                    strDocTypeId, _
        '                                                    strDNParam, _
        '                                                    1, _
        '                                                    objDNId)
        '    Catch Exp As System.Exception
        '        If intErrNo <> -5 Then
        '            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_AUTO_DN_ADD&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
        '        End If
        '    End Try
        'End If

        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""


        sSQLKriteria = "UPDATE IN_STOCKISSUE SET Status='2' Where StockIssueID='" & lblStckTxID.Text & "'"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get,
                                         strParamName,
                                         strParamValue,
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        'If intError = objINtx.EnumTransactionError.NoError Then


        strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

            strParamValue = Trim(strLocation) & _
                            "|" & "IN_STOCKISSUE" & _
                            "|" & "IN_STOCKISSUELN" & _
                            "|" & "STOCKISSUEID" & _
                            "|" & Trim(lblStckTxID.Text) & _
                            "|" & "QTY" & _
                            "|" & ddlInventoryBin.SelectedItem.Value & _
                            "|" & "-" & _
                            "|" & Trim(strUserId)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockReceive_List.aspx")
            End Try
        'End If

        LoadStockTxDetails()
        DisplayFromDB(False)
        PageControl()
        IssueTypeControl()
        BindGrid()
    End Sub

    Sub btnDebitNote_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim strDocTypeId As String = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNote) & "|" & objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNoteLn)
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
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

        Select Case IssueType.Text
            Case objINtx.EnumStockIssueType.StaffDN
                StrTxParam = lblStckTxID.Text & "||||||||" & Request.Form("lstEmpID") & "|" & IssueType.Text & "|||||||" & objINtx.EnumStockIssueStatus.DbNote & "||||"
            Case Else
                StrTxParam = lblStckTxID.Text & "||||||||" & lstBillParty.SelectedItem.Value & "|" & IssueType.Text & "|||||||" & objINtx.EnumStockIssueStatus.DbNote & "||||"
        End Select
        Try
            intErrNo = objINtx.mtdUpdStockIssueDetail(strOpCdStckTxDet_ADD, _
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

            lblStckTxID.Text = TxID
            If ErrorChk = objINtx.EnumInventoryErrorType.OverFlow Then
                lblErrNum.Visible = True
            End If

        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_NEWSTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
            End If
        End Try

        LoadStockTxDetails()
        DisplayFromDB(True)
        PageControl()
        BindGrid()
    End Sub

    Sub btnCancel_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
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

        Try
            intErrNo = objINtx.mtdAdjustInvItemLevel(strOpCdStckTxLine_GET, _
                                                     strOpCdItem_Details_UPD, _
                                                     strOpCdItem_Details_GET, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     lblStckTxID.Text, _
                                                     objINtx.EnumTransactionAction.Cancel, _
                                                     ErrorChk)
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=REMOVEINVENTORYITEM&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
            End If
        End Try
        StrTxParam = lblStckTxID.Text & "||||||||||||||||" & objINtx.EnumStockIssueStatus.Cancelled & "||||"
        If intErrNo = 0 Then
            Try
                intErrNo = objINtx.mtdUpdStockIssueDetail(strOpCdStckTxDet_ADD, _
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
                                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue), _
                                                          ErrorChk, _
                                                          TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CANCELSTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End If
            End Try
        End If

        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|ID|BIN|SIGN|UPDATEID"

        strParamValue = Trim(strLocation) & _
                        "|" & Trim(lblStckTxID.Text) & _
                        "|" & "IN_STOCKISSUE" & _
                        "|" & "IN_STOCKISSUELN" & _
                        "|" & "STOCKISSUEID" & _
                        "|" & ddlInventoryBin.SelectedItem.Value & _
                        "|" & "+" & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        LoadStockTxDetails()
        DisplayFromDB(False)
        PageControl()
        BindGrid()

    End Sub

    Sub btnDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim StrTxParam As String
        Dim TxID As String
        Dim ErrorChk As Integer = objINtx.EnumInventoryErrorType.NoError
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

        If lblStatusHid.Text = CStr(objINtx.EnumStockIssueStatus.Deleted) Then
            Try
                intErrNo = objINtx.mtdAdjustInvItemLevel(strOpCdStckTxLine_GET, _
                                                         strOpCdItem_Details_UPD, _
                                                         strOpCdItem_Details_GET, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         lblStckTxID.Text, _
                                                         objINtx.EnumTransactionAction.Undelete, _
                                                         ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UNDELETESTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End If
            End Try
            StrTxParam = lblStckTxID.Text & "||||||||||||||||" & objINtx.EnumStockIssueStatus.Active & "||||"
        Else
            Try
                intErrNo = objINtx.mtdAdjustInvItemLevel(strOpCdStckTxLine_GET, _
                                                         strOpCdItem_Details_UPD, _
                                                         strOpCdItem_Details_GET, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         lblStckTxID.Text, _
                                                         objINtx.EnumTransactionAction.Delete, _
                                                         ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETENEWSTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End If
            End Try
            
            StrTxParam = lblStckTxID.Text & "||||||||||||||||" & objINtx.EnumStockIssueStatus.Deleted & "||||"
        End If

        If ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            lblUnDel.Visible = True
            Exit Sub
        End If

        If intErrNo = 0 And Not ErrorChk = objINtx.EnumInventoryErrorType.InsufficientQty Then
            Try
                intErrNo = objINtx.mtdUpdStockIssueDetail(strOpCdStckTxDet_ADD, _
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
                                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue), _
                                                          ErrorChk, _
                                                          TxID)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_NEWSTOCKISSUE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End If
            End Try
        End If

        LoadStockTxDetails()
        DisplayFromDB(False)
        PageControl()
        BindGrid()

    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String
        Dim intStatus As Integer
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String
        Dim strStockTxId As String
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

        strStockTxId = Trim(lblStckTxID.Text)
        strUpdString = "where StockIssueID = '" & strStockTxId & "'"
        strStatus = Trim(Status.Text)
        intStatus = CInt(Trim(lblStatusHid.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strIssueType = Trim(IssueType.Text)
        strTable = "IN_STOCKISSUE"
        strSortLine = ""

        If cblDisplayCost.Items(0).Selected Then
            strDisplayCost = "1"
        End If

        If intStatus = objINtx.EnumStockIssueStatus.Confirmed Or intStatus = objINtx.EnumStockIssueStatus.DbNote Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                        strUpdString, _
                                                        strTable, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_SHARE_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_StockIssue_List.aspx")
                End Try
                LoadStockTxDetails()
                DisplayFromDB(False)
                PageControl()
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockIssueDet.aspx?strStockIssueId=" & strStockTxId & _
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

    Sub btnNew_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim intIssueType As Integer = CInt(IssueType.Text)
        Dim strIssueType As String
        Select Case intIssueType
            Case objINtx.EnumStockIssueType.OwnUse
                strIssueType = "Issue"
            Case objINtx.EnumStockIssueType.StaffPayroll
                strIssueType = "Staff"
            Case objINtx.EnumStockIssueType.External
                strIssueType = "External"
            Case objINtx.EnumStockIssueType.Nursery
                strIssueType = "Nursery"
            Case Else
                strIssueType = "Issue"
        End Select
        Response.Redirect("IN_Trx_StockIssue_Details.aspx?isType=" & strIssueType)
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../IN/Trx/IN_Trx_StockIssue_List.aspx")
    End Sub

    Function CheckLocBillParty () As Boolean
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_STOCKISSUE_DETAIL_GET"
        Dim strSearch As String
        Dim objBPLocDs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim intCnt As Integer
        Dim strLocCode As String
        Dim objStockIssueDS As New DataSet()

        strParam = Trim(lblStckTxID.Text)

        Try
            intErrNo = objINtx.mtdGetStockTransactDetails(strOpCdStckTxDet_GET, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strAccMonth, _
                                                          strAccYear, _
                                                          strParam, _
                                                          objStockIssueDS)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_FUELISSUELINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_FuelIssue_List.aspx")
        End Try

        For intCnt = 0 To objStockIssueDS.Tables(0).Rows.Count - 1
            strLocCode = TRIM(objStockIssueDS.Tables(0).Rows(intCnt).Item("ChargeLocCode"))

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

  Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")        
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objAccDs AS New Dataset

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                    strParam, _
                                                    objGLSetup.EnumGLMasterType.AccountCode, _
                                                    objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_DEBITNOTEDET_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try 
        
            dr = objAccDs.Tables(0).NewRow()
            dr("AccCode") = ""
            ''dr("_Description") = ""
            objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        radcmbCOA.DataSource = objAccDs.Tables(0)
        radcmbCOA.DataValueField = "AccCode"
        radcmbCOA.DataTextField =  "_Description"
        radcmbCOA.DataBind()
        radcmbCOA.SelectedIndex = 0
           
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

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
        'Dim strText = "Please select Inventory Bin"

        'ddlInventoryBin.Items.Clear()
        'ddlInventoryBin.Items.Add(New ListItem(strText, "0"))


        'ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO), objINstp.EnumInventoryBinLevel.HO))
        'ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Other), objINstp.EnumInventoryBinLevel.Other))

        'If Session("SS_LOCLEVEL") = "1" Then
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central), objINstp.EnumInventoryBinLevel.Central))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinI), objINstp.EnumInventoryBinLevel.BinI))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinII), objINstp.EnumInventoryBinLevel.BinII))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIII), objINstp.EnumInventoryBinLevel.BinIII))
        '    ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIV), objINstp.EnumInventoryBinLevel.BinIV))
        'End If

        'If Not Trim(pv_strInvBin) = "" Then
        '    With ddlInventoryBin
        '        .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
        '    End With
        'Else
        '    ddlInventoryBin.SelectedIndex = -1
        'End If


        Dim strText = "Please select Inventory Bin"

        ddlInventoryBin.Items.Clear()
        ddlInventoryBin.Items.Add(New ListItem(strText, "0"))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO), objINstp.EnumInventoryBinLevel.HO))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central), objINstp.EnumInventoryBinLevel.Central))
        ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.Other), objINstp.EnumInventoryBinLevel.Other))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinI), objINstp.EnumInventoryBinLevel.BinI))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinII), objINstp.EnumInventoryBinLevel.BinII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIII), objINstp.EnumInventoryBinLevel.BinIII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINstp.mtdGetInventoryBinLevel(objINstp.EnumInventoryBinLevel.BinIV), objINstp.EnumInventoryBinLevel.BinIV))

        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        Else
            ddlInventoryBin.SelectedIndex = -1
        End If
    End Sub

    Sub GetItem(ByVal pv_strItemCode As String)
        Dim dr As DataRow
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strPOType As String

        Dim strOpCode As String = "IN_CLSTRX_ITEMPART_ITEM_GET"
        Dim dsMaster As Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = " And itm.ItemCode = '" & Trim(pv_strItemCode) & "' AND itm.LocCode = '" & strLocation & "' AND itm.Status = '" & objINstp.EnumStockItemStatus.Active & "'  " & "|itm.ItemCode"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                dsMaster)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATE_INVOICE_WM_GET_HEADER&errmesg=" & Exp.ToString() & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        For intCnt = 0 To dsMaster.Tables(0).Rows.Count - 1
            If dsMaster.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(pv_strItemCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsMaster.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = "Please select Item Code"
        dsMaster.Tables(0).Rows.InsertAt(dr, 0)

        lstItem.DataSource = dsMaster.Tables(0)
        lstItem.DataValueField = "ItemCode"
        lstItem.DataTextField = "Description"
        lstItem.DataBind()
        lstItem.SelectedIndex = intSelectedIndex
    End Sub

    Function lGetStockBalanceStorage(ByVal pTransDate AS String,ByVal pStorage AS String,ByVal pOldValue As double ) AS Double
        Dim strOpCode_StorageBalance AS String="IN_CLSTRX_STOCKBALANCE_CONTROL_INOUT"
        Dim vValueExist as Double=0
        Dim vQtyStockBalance AS Double=0
        Dim dtSet AS new Dataset

        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "ITEMCODE|STORAGE|VEXIST|VNEW|DT|LOC"
        strParamValue = txtItemCode.text.trim & "|" & lstStorage.SelectedItem.value & "|" & pOldValue & "|" &  CDbl(txtQty.text) & "|" & pTransDate & "|" &  strlocation

        Try
           intErrNo = objGLtrx.mtdGetDataCommon(strOpCode_StorageBalance, _
                                               strParamName, _
                                               strParamValue, _
                                               dtSet)

        Catch Exp As System.Exception
           Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKISSUE_STOCKRECEIVE_DETAILS_GET&errmesg=" & Exp.ToString() & "&redirect=IN/Trx/IN_Trx_StockIssue_Details.aspx")
        End Try

        If dtSet.Tables(0).Rows.Count > 0 Then       
           vQtyStockBalance = dtSet.Tables(0).Rows(0).Item("QtyEnd")       
        End If

        Return vQtyStockBalance
    End Function

    Function lGetCheckVehicle() AS Boolean     
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim bExist AS Boolean=False
        Dim bOK AS Boolean=true

    
        sSQLKriteria = "SELECT Distinct AccCOde FROM GL_VehicleACC Where AccCode='" & radcmbCOA.SelectedValue & "'"
    
        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        If objdsST.Tables(0).Rows.Count > 0 Then       
           bExist=True
        Else
            bExist=False
        End If

        IF bExist=True Then
            IF RTrim(lstVehCode.SelectedValue)="" Then
                UserMsgBox(Me,"You Choose Vehicle Account, Please Select Vehicle Code to Completed Transaction...!")
                lstVehCode.Focus()
                bOK=False
            End IF            
        Else
            IF RTrim(lstVehCode.SelectedValue)<> "" Then
                UserMsgBox(Me,"You Choose Vehicle Code, Please Select Vehicle Account To Completed Transaction...!")
                radcmbCOA.Focus()
                bOK=False
            End If
        End IF
        
        Return bOK
    End Function

    Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
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
                        Session("SS_USERID") & "|" & Trim(lblStckTxID.Text)

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

        LoadStockTxDetails()
        DisplayFromDB(False)
        PageControl()
    End Sub

    Sub ddlInventoryBin_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        IssueTypeControl()
    End Sub

End Class
