Imports System
Imports System.Math
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings


Public Class NU_trx_SeedlingsIssueDetails : Inherits Page
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblActionResult As Label
    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents lblIssueID As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblStatusCode As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblUpdateDate As Label
    Protected WithEvents lblUpdateID As Label
    
    Protected WithEvents lblNurseryBlockTag As Label
    Protected WithEvents lblBatchNoTag As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblPreBlkCodeTag As Label
    Protected WithEvents lblBlkCodeTag As Label
    Protected WithEvents lblVehCodeTag As Label
    Protected WithEvents lblVehExpCodeTag As Label
    
    Protected WithEvents lblDocRefNoErr As Label
    Protected WithEvents lblNurseryBlockErr As Label
    Protected WithEvents lblIssueDateErr As Label
    Protected WithEvents lblBatchNoErr As Label
    Protected WithEvents lblChargeToErr As Label
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents lblPreBlkCodeErr As Label
    Protected WithEvents lblBlkCodeErr As Label
    Protected WithEvents lblVehCodeErr As Label
    Protected WithEvents lblVehExpCodeErr As Label
    
    Protected WithEvents lblQtyErr As Label
    Protected WithEvents lblDummySpace As Label
    
    Protected WithEvents txtDocRefNo As TextBox
    Protected WithEvents txtIssueDate As TextBox
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents txtQty As TextBox
    Protected WithEvents txtCost As TextBox
    Protected WithEvents txtAmount As TextBox
    
    Protected WithEvents ddlNurseryBlock As DropDownList
    Protected WithEvents ddlBatchNo As DropDownList
    Protected WithEvents ddlChargeTo As DropDownList
    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents ddlPreBlkCode As DropDownList
    Protected WithEvents ddlBlkCode As DropDownList
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents ddlVehExpCode As DropDownList
    
    Protected WithEvents imgIssueDate As Image
    
    Protected WithEvents ibAdd As ImageButton
    Protected WithEvents ibSave As ImageButton
    Protected WithEvents ibConfirm As ImageButton
    Protected WithEvents ibDelete As ImageButton
    Protected WithEvents ibNew As ImageButton
    Protected WithEvents ibBack As ImageButton
    
    Protected WithEvents dgLines As DataGrid
    
    Protected WithEvents tblAdd As HtmlTable
    
    Protected WithEvents btnFindAccCode As HtmlInputButton
    
    Protected WithEvents trChargeTo As HtmlTableRow
    Protected WithEvents trChargeLevel As HtmlTableRow
    Protected WithEvents trPreBlkCode As HtmlTableRow
    Protected WithEvents trBlkCode As HtmlTableRow
    Protected WithEvents trDataGrid1 As HtmlTableRow
    Protected WithEvents trDataGrid2 As HtmlTableRow
    Protected WithEvents trDataGrid3 As HtmlTableRow
    
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden

    Protected objNUTrx As New agri.NU.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objNUSetup As New agri.NU.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()

    
    Dim strOpCdSeedlingsIssue_ADD As String = "NU_CLSTRX_SEEDLINGS_ISSUE_ADD"
    Dim strOpCdSeedlingsIssue_GET As String = "NU_CLSTRX_SEEDLINGS_ISSUE_GET"
    Dim strOpCdSeedlingsIssue_UPD As String = "NU_CLSTRX_SEEDLINGS_ISSUE_UPD"
    Dim strOpCdSeedlingsIssueLine_ADD As String = "NU_CLSTRX_SEEDLINGS_ISSUE_LINE_ADD"
    Dim strOpCdSeedlingsIssueLine_GET As String = "NU_CLSTRX_SEEDLINGS_ISSUE_LINE_GET"
    Dim strOpCdSeedlingsIssueLine_UPD As String = "NU_CLSTRX_SEEDLINGS_ISSUE_LINE_UPD"
    Dim strOpCdSeedlingsIssueLine_DEL As String = "NU_CLSTRX_SEEDLINGS_ISSUE_LINE_DEL"
    Dim strOpCodeBlockChargingList_GET As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"  
    Dim strOpCodeNurseryBatch_GET As String = "NU_CLSSETUP_NURSERY_BATCH_GET"
    Dim strOpCodeItemCode_GET As String = "IN_CLSTRX_NURSERYITEM_BLKBATCH_GET"
    Dim strOpCodeLocation_GET As String = "IN_CLSTRX_LOCATION_GET_BY_BLKCODE"
    Dim strOpCodeAccCode_GET As String = "IN_CLSTRX_ACCCODE_GET_BY_BLKCODE"
    Dim strOpCodeBatchNo_GET As String = "IN_CLSTRX_BATCHNO_GET_BY_ITEMCODE"
    Dim strOpCodeBlockItemLoc_GET As String = "IN_CLSTRX_LOCATION_GET_BY_ITEMCODE"
    Dim strOpCodeNurseryBatch_UPD As String = "NU_CLSSETUP_NURSERYBATCH_LIST_UPD"
    
    Dim objLangCapDs As New DataSet()
    
    Const APPEND_NU_BLK_CODE As Boolean = True
    Const APPEND_ITEM_CODE As Boolean = False
    Const APPEND_LOC_CODE As Boolean = False
    Const APPEND_ACCOUNT_CODE As Boolean = False
    Const APPEND_ITEM_BATCHNO As Boolean = False
    Const APPEND_LOC_ITEM As Boolean = False
    Const APPEND_BATCH_NO As Boolean = False
    Const APPEND_ACC_CODE As Boolean = False
    Const APPEND_BLK_CODE As Boolean = False
    Const APPEND_VEH_CODE As Boolean = False
    Const APPEND_VEH_EXP_CODE As Boolean = False
    
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intNUAR As Integer
    Dim strDateFormat As String
    Dim intConfigsetting As Integer
    Dim strAction As String

    Protected WithEvents lblBPErr As Label
    Protected WithEvents lblLocCodeErr As Label
    
    Protected WithEvents lstItem As DropDownList
    Protected WithEvents lblItemCodeErr As Label
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intDefaultAdjType As Integer
        Dim intDefaultTransType As Integer
        Dim intCnt As Integer
        Dim strSelItemCode As String
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intNUAR = Session("SS_NUAR")
        strDateFormat = Session("SS_DATEFMT")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedIssue), intNUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            objLangCapDs = GetLanguageCaptionDS()
            
            lblActionResult.Visible = False 
            
            If Not Page.IsPostBack Then
                txtIssueDate.Text = objGlobal.GetShortDate(strDateFormat, Now)
                lblIssueID.Text = Trim(Request.QueryString("ID"))
                GetLangCap()
                BindNurseryBlockDropDownList(strLocation, "")
                BindItemCodeDropDownList(strLocation, "", "")  
                BindBatchNoDropDownList(strLocation, "", "")
                BindLocationByBlock("", "")  
                BindAccCodeByBlock(strLocation, "", "")  
                BindBatchNoByBlkItem(strLocation, "", "", "")  
                BindLocationByBlkItem("", "", "")  
                BindChargeToDropDownList(strLocation)
                BindAccCodeDropDownList("")
                BindChargeLevelDropDownList()
                BindPreBlkCodeDropDownList("", "")
                BindBlkCodeDropDownList("", "")
                BindVehCodeDropDownList("", "")
                BindVehExpCodeDropDownList("", True)

                BindItemCodeList("") 

                If lblIssueID.Text.Trim() <> ""
                    DisplaySeedlingsIssueHeader()
                    DisplaySeedlingsIssueLines()
                End If
                SetObjectAccessibilityByStatus()
                trChargeTo.Visible = Session("SS_INTER_ESTATE_CHARGING")
            End If
            lblBPErr.Visible = False
            lblLocCodeErr.Visible = False
            lblItemCodeErr.Visible = False
        End If
        ToggleChargeLevel()
    End Sub

    Sub GetLangCap()
        Dim strNUBlkCode As String
        Dim strBatchNo As String
        Dim strAccCode As String
        Dim strPreBlkCode As String
        Dim strBlkCode As String
        Dim strVehCode As String
        Dim strVehExpCode As String
        
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlkCode = GetCaption(objLangCap.EnumLangCap.Block)
                strNUBlkCode = GetCaption(objLangCap.EnumLangCap.NurseryBlock)
            Else
                strBlkCode = GetCaption(objLangCap.EnumLangCap.SubBlock)
                strNUBlkCode = GetCaption(objLangCap.EnumLangCap.NurserySubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_GET_LANGCAP&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try
        
        strBatchNo = GetCaption(objLangCap.EnumLangCap.BatchNo)
        strPreBlkCode = GetCaption(objLangCap.EnumLangCap.Block)
        strAccCode = GetCaption(objLangCap.EnumLangCap.Account)
        strVehCode = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strVehExpCode = GetCaption(objLangCap.EnumLangCap.VehExpense)

        lblNurseryBlockTag.Text = strNUBlkCode & lblCode.Text
        lblBatchNoTag.Text = strBatchNo
        lblAccCodeTag.Text = strAccCode & lblCode.Text
        lblPreBlkCodeTag.Text = strPreBlkCode & lblCode.Text
        lblBlkCodeTag.Text = strBlkCode & lblCode.Text
        lblVehCodeTag.Text = strVehCode & lblCode.Text
        lblVehExpCodeTag.Text = strVehExpCode & lblCode.Text

        lblBatchNoErr.Text = lblPleaseSelect.Text & strBatchNo & "."
        lblChargeToErr.Text = lblPleaseSelect.Text & GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text & "."
        lblAccCodeErr.Text = lblPleaseSelect.Text & strAccCode & lblCode.Text & "."
        lblPreBlkCodeErr.Text = lblPleaseSelect.Text & strPreBlkCode & lblCode.Text & "."
        lblBlkCodeErr.Text = lblPleaseSelect.Text & strBlkCode & lblCode.Text & "."
        lblVehCodeErr.Text = lblPleaseSelect.Text & strVehCode & lblCode.Text & "."
        lblVehExpCodeErr.Text = lblPleaseSelect.Text & strVehExpCode & lblCode.Text & "."

        dgLines.Columns(1).HeaderText = strBatchNo
        dgLines.Columns(3).HeaderText = strAccCode & lblCode.Text
        dgLines.Columns(4).HeaderText = strBlkCode & lblCode.Text
        dgLines.Columns(5).HeaderText = strVehCode & lblCode.Text
        dgLines.Columns(6).HeaderText = strVehExpCode & lblCode.Text
        
        lblNurseryBlockErr.Text = strNUBlkCode & lblCode.Text & " cannot be blank!"
    End Sub

    Function GetCaption(ByVal pv_TermCode As String) As String
        Dim I As Integer

        For I = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(I).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(I).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(I).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function
    
    Sub SetObjectAccessibilityByStatus()
        ddlNurseryBlock.Enabled = False
        ddlBatchNo.Enabled = False
        ddlChargeTo.Enabled = False
        ddlAccCode.Enabled = False
        ddlChargeLevel.Enabled = False
        ddlPreBlkCode.Enabled = False
        ddlBlkCode.Enabled = False
        ddlVehCode.Enabled = False
        ddlVehExpCode.Enabled = False

        txtDocRefNo.Enabled = False
        txtIssueDate.Enabled = False
        txtRemark.Enabled = False
        txtDescription.Enabled = False
        txtQty.Enabled = False
        txtCost.Enabled = False
        txtAmount.Enabled = False

        ibSave.Visible = False
        ibConfirm.Visible = False
        ibDelete.Visible = False
        ibNew.Visible = False

        dgLines.Columns(9).Visible = False
        lblDummySpace.Visible = False

        tblAdd.Visible = False

        imgIssueDate.Visible = False
        btnFindAccCode.Visible = False

        Select Case lblStatusCode.Text.Trim()
            Case Trim(CStr(objNUTrx.EnumSeedlingsIssueStatus.Deleted))
                ibNew.Visible = True
                ibDelete.Visible = True
                ibDelete.ImageUrl = "../../images/butt_undelete.gif"
                ibDelete.AlternateText = "Undelete"
                ibDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objNUTrx.EnumSeedlingsIssueStatus.Confirmed))
                ibNew.Visible = True
            Case Trim(CStr(objNUTrx.EnumSeedlingsIssueStatus.Closed))
                ibNew.Visible = True
            Case Else
                ddlBatchNo.Enabled = True
                ddlAccCode.Enabled = True
                ddlChargeLevel.Enabled = True
                ddlPreBlkCode.Enabled = True
                ddlBlkCode.Enabled = True
                ddlVehCode.Enabled = True
                ddlVehExpCode.Enabled = True
                txtDocRefNo.Enabled = True
                txtIssueDate.Enabled = True
                txtRemark.Enabled = True
                txtDescription.Enabled = True
                txtQty.Enabled = True
                txtCost.Enabled = False
                txtAmount.Enabled = False
                imgIssueDate.Visible = True
                btnFindAccCode.Visible = True
                ibSave.Visible = True
                tblAdd.Visible = True
                If lblIssueID.Text = "" Then
                    ddlNurseryBlock.Enabled = True
                Else
                    ibNew.Visible = True
                    ibDelete.Visible = True
                    ibDelete.ImageUrl = "../../images/butt_delete.gif"
                    ibDelete.AlternateText = "Delete"
                    ibDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    If dgLines.Items.Count = 0 Then
                        ddlNurseryBlock.Enabled = True
                    Else
                        ibConfirm.Visible = True
                    End If
                    dgLines.Columns(9).Visible = True
                    lblDummySpace.Visible = True
                End If
        End Select
    End Sub
    
    Sub ResetPage(ByVal blnHeader As Boolean, ByVal blnAdd As Boolean, ByVal blnLines As Boolean)
        Dim strNurseryBlock As String = ddlNurseryBlock.SelectedItem.Value.Trim()
        Dim strItemCode As String = lstItem.SelectedItem.Value.Trim()
        If blnHeader = True Then
            DisplaySeedlingsIssueHeader()
        End If
        
        If blnLines = True Then
            DisplaySeedlingsIssueLines()
        End If
        
        SetObjectAccessibilityByStatus()
        If blnAdd = True Then
            BindItemCodeDropDownList(strLocation, strNurseryBlock, "")  
            BindBatchNoDropDownList(strLocation, strNurseryBlock, "")
            BindLocationByBlock(strNurseryBlock, "")  
            BindAccCodeByBlock(strLocation, strNurseryBlock, "")  
            BindBatchNoByBlkItem(strLocation, strNurseryBlock, strItemCode, "")  
            BindLocationByBlkItem(strNurseryBlock, strItemCode, "")  
            BindChargeToDropDownList(strLocation)
            BindAccCodeDropDownList("")
            BindPreBlkCodeDropDownList("", "")
            BindBlkCodeDropDownList("", "")
            BindVehCodeDropDownList("", "")
            BindVehExpCodeDropDownList("", True)
            txtDescription.Text = ""
            txtQty.Text = ""
            txtCost.Text = ""
            txtAmount.Text = ""
        End If
    End Sub
    
    Sub ToggleChargeLevel()
        If ddlChargeLevel.SelectedIndex = 0 Then
            trBlkCode.Visible = False
            trPreBlkCode.Visible = True
            hidBlockCharge.value = "yes"
        Else
            trBlkCode.Visible = True
            trPreBlkCode.Visible = False
            hidBlockCharge.value = ""
        End If
    End Sub
    
    Sub BindNurseryBlockDropDownList(ByVal pv_strLocation As String, ByVal pv_strBlockCode As String)
        Dim strOpCd As String
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String
        
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strOpCd = "GL_CLSSETUP_BLOCK_LIST_GET"
                strParam = "ORDER BY Blk.BlkCode ASC | " & _
                           "AND Blk.Status = '" & objGLSetup.EnumBlockStatus.Active & "'" & _
                           "AND Blk.BlkType = '" & objGLSetup.EnumBlockType.Nursery & "'" & _
                           "AND BLK.LocCode = '" & pv_strLocation & "'"
            Else
                strOpCd = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
                strParam = "ORDER BY sub.SubBlkCode ASC | " & _
                           "AND sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "'" & _
                           "AND sub.SubBlkType = '" & objGLSetup.EnumSubBlockType.Nursery & "'" & _
                           "AND sub.LocCode = '" & pv_strLocation & "'"
            End If
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.Block, dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_BIND_NURSERY_BLOCK&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("BlkCode") = dsList.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            dsList.Tables(0).Rows(intCnt).Item("Description") = dsList.Tables(0).Rows(intCnt).Item("BlkCode").Trim() & " (" & dsList.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If LCase(dsList.Tables(0).Rows(intCnt).Item("BlkCode").Trim()) = LCase(pv_strBlockCode.Trim()) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("BlkCode") = ""
        drNew("Description") = lblSelect.Text & lblNurseryBlockTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)
        
        If pv_strBlockCode <> "" And intSelectedIndex = 0 And APPEND_NU_BLK_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("BlkCode") = pv_strBlockCode.Trim()
            drNew("Description") = pv_strBlockCode.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If
        
        ddlNurseryBlock.DataSource = dsList.Tables(0)
        ddlNurseryBlock.DataValueField = "BlkCode"
        ddlNurseryBlock.DataTextField = "Description"
        ddlNurseryBlock.DataBind()
        ddlNurseryBlock.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindItemCodeDropDownList(ByVal pv_strLocation As String, ByVal pv_strBlockCode As String, ByVal pv_strItemCode As String)
        Dim intSelectedIndex As Integer = 0
        Dim strParam As String
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        'strParam = pv_strBlockCode & "|" & _
        '           objNUSetup.EnumNurseryBatchStatus.Active & "|" & _
        '           strLocation
        strParamName = "SEARCHSTR"
        strParamValue = " AND  GL.SubBlkCode like '" & Trim(pv_strBlockCode) & "%' AND IT.ItemCode LIKE '%" & Trim(pv_strBlockCode) & "' AND NB.Status like '1%'  AND NB.LocCode = '" & Trim(strLocation) & "'"

        
        Try
            'intErrNo = objNUTrx.mtdGetItemByNUBlock(strOpCodeItemCode_GET, strParam, dsList)
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCodeItemCode_GET, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsList)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_GET_ITEM_BY_BLOCK&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_Trx_SeedlingsIssue_Details.aspx")
        End Try
        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("ItemCode"))
            dsList.Tables(0).Rows(intCnt).Item("ItemDesc") = dsList.Tables(0).Rows(intCnt).Item("ItemCode") & " ( " & _
                                                             dsList.Tables(0).Rows(intCnt).Item("ItemDesc") & " ), " & _
                                                             "Rp. " & objGlobal.GetIDDecimalSeparator(dsList.Tables(0).Rows(intCnt).Item("AverageCost")) & ", " & _
                                                             objGlobal.GetIDDecimalSeparator_FreeDigit(dsList.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & " , " & _
                                                             Trim(dsList.Tables(0).Rows(intCnt).Item("UOMCode"))
        Next

        If dsList.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If


        drNew = dsList.Tables(0).NewRow()
        drNew("ItemCode") = ""
        drNew("ItemDesc") = "Select Item Code"
        dsList.Tables(0).Rows.InsertAt(drNew, 0)
        
        If pv_strBlockCode <> "" And intSelectedIndex = 0 And APPEND_ITEM_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("ItemCode") = pv_strItemCode.Trim()
            drNew("ItemDesc") = pv_strItemCode.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If
        
        lstItem.DataSource = dsList.Tables(0)
        lstItem.DataValueField = "ItemCode"
        lstItem.DataTextField = "ItemDesc"
        lstItem.DataBind()
        lstItem.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindLocationByBlock(ByVal pv_strBlockCode As String, ByVal pv_strLocation As String)
        Dim intSelectedIndex As Integer = 0
        Dim strParam As String
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer

        strParam = objAdminLoc.EnumLocStatus.Active & "|" & _
                   Trim(Session("SS_COMPANY")) & "|" & _
                   Trim(Session("SS_LOCATION")) & "|" & _
                   Trim(Session("SS_USERID")) & "|" & _
                   objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Nursery) & "|" & _
                   "NUAR" & "|" & _
                   objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch) & "|" & _
                   "NUAR" & "|" & _
                   objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch) & "|" & _
                   pv_strBlockCode

        Try
            intErrNo = objINTrx.mtdGetLocationByBlock(strOpCodeLocation_GET, strParam, dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_GET_LOCATION_BY_BLOCK&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_Trx_SeedlingsIssue_Details.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("LocCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("LocCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("LocCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next
        drNew = dsList.Tables(0).NewRow()
        drNew("LocCode") = ""
        drNew("Description") = lblSelect.Text & GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)
        
        If pv_strBlockCode <> "" And intSelectedIndex = 0 And APPEND_LOC_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("LocCode") = pv_strLocation.Trim()
            drNew("Description") = pv_strLocation.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If

        ddlChargeTo.DataSource = dsList.Tables(0)
        ddlChargeTo.DataValueField = "LocCode"
        ddlChargeTo.DataTextField = "Description"
        ddlChargeTo.DataBind()
        'ddlChargeTo.SelectedIndex = intSelectedIndex
        ddlChargeTo.SelectedValue = Session("SS_LOCATION")
        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindAccCodeByBlock(ByVal pv_strLocation As String, ByVal pv_strBlockCode As String, ByVal pv_strAccCode As String)
        Dim intSelectedIndex As Integer = 0
        Dim strParam As String
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        
        strParam = pv_strBlockCode & "|" & _
                   objNUSetup.EnumNurseryBatchStatus.Active & "|" & _
                   strLocation

        Try
            intErrNo = objINTrx.mtdGetAccountByBlock(strOpCodeAccCode_GET, strParam, dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_GET_ACCCODE_BY_BLOCK&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_Trx_SeedlingsIssue_Details.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("AccCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("AccCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next
        drNew = dsList.Tables(0).NewRow()
        drNew("AccCode") = ""
        drNew("Description") = lblSelect.Text & lblAccCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)
        
        If pv_strBlockCode <> "" And intSelectedIndex = 0 And APPEND_ACCOUNT_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("AccCode") = pv_strAccCode.Trim()
            drNew("Description") = pv_strAccCode.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If
        
        ddlAccCode.DataSource = dsList.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindBatchNoByBlkItem(ByVal pv_strLocation As String, ByVal pv_strBlockCode As String, ByVal pv_strItemCode As String, ByVal pv_strBatchNo As String)
        Dim intSelectedIndex As Integer = 0
        Dim strParam As String
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        
        strParam = pv_strBlockCode & "|" & _
                   pv_strItemCode & "|" & _
                   objNUSetup.EnumNurseryBatchStatus.Active & "|" & _
                   strLocation

        Try
            intErrNo = objINTrx.mtdGetBatchNoByItem(strOpCodeBatchNo_GET, strParam, dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_GET_BATCHNO_BY_ITEM&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_Trx_SeedlingsIssue_Details.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("BatchNo") = Trim(dsList.Tables(0).Rows(intCnt).Item("BatchNo"))
            dsList.Tables(0).Rows(intCnt).Item("BatchNoDesc") = Trim(dsList.Tables(0).Rows(intCnt).Item("BatchNo")) & Trim(dsList.Tables(0).Rows(intCnt).Item("BatchNoDesc"))
        Next

        If dsList.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        drNew = dsList.Tables(0).NewRow()
        drNew("BatchNo") = 0
        drNew("BatchNoDesc") = "Select Batch No"
        dsList.Tables(0).Rows.InsertAt(drNew, 0)
        
        If pv_strBlockCode <> "" And pv_strItemCode <> "" And intSelectedIndex = 0 And APPEND_ITEM_BATCHNO = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("BatchNo") = pv_strBatchNo.Trim()
            drNew("BatchNoDesc") = pv_strBatchNo.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If
        
        ddlBatchNo.DataSource = dsList.Tables(0)
        ddlBatchNo.DataValueField = "BatchNo"
        ddlBatchNo.DataTextField = "BatchNoDesc"
        ddlBatchNo.DataBind()
        ddlBatchNo.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindLocationByBlkItem(ByVal pv_strBlockCode As String, ByVal pv_strItemCode As String, ByVal pv_strLocation As String)
        Dim intSelectedIndex As Integer = 0
        Dim strParam As String
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        
        strParam = objAdminLoc.EnumLocStatus.Active & "|" & _
                   Trim(Session("SS_COMPANY")) & "|" & _
                   Trim(Session("SS_LOCATION")) & "|" & _
                   Trim(Session("SS_USERID")) & "|" & _
                   objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Nursery) & "|" & _
                   "NUAR" & "|" & _
                   objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch) & "|" & _
                   "NUAR" & "|" & _
                   objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch) & "|" & _
                   pv_strBlockCode & "|" & _
                   pv_strItemCode

        Try
            intErrNo = objINTrx.mtdGetLocationByBlockItem(strOpCodeBlockItemLoc_GET, strParam, dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_GET_LOC_BY_ITEM&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_Trx_SeedlingsIssue_Details.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("LocCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("LocCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("Description"))
        Next
        drNew = dsList.Tables(0).NewRow()
        drNew("LocCode") = 0
        drNew("Description") = lblSelect.Text & GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)
        
        If pv_strBlockCode <> "" And pv_strItemCode <> "" And intSelectedIndex = 0 And APPEND_LOC_ITEM = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("LocCode") = pv_strLocation.Trim()
            drNew("Description") = pv_strLocation.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If
        
        ddlChargeTo.DataSource = dsList.Tables(0)
        ddlChargeTo.DataValueField = "LocCode"
        ddlChargeTo.DataTextField = "Description"
        ddlChargeTo.DataBind()
        'ddlChargeTo.SelectedIndex = intSelectedIndex
        ddlChargeTo.SelectedValue = Session("SS_LOCATION")

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindBatchNoDropDownList(ByVal pv_strLocation As String, ByVal pv_strBlockCode As String, ByVal pv_strBatchNo As String)
        Dim strOpCd As String
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strSelect As String
        Dim strFrom As String
        Dim strWhere As String
        Dim strOrderBy As String
        
        Try
            strSelect = ", '' As _BatchNo, '' As Description"
            'strFrom = ""
            'strWhere = "NB.LocCode = '" & pv_strLocation & "' And NB.BlkCode = '" & pv_strBlockCode & "' And NB.Status = '" & objNUSetup.EnumNurseryBatchStatus.Active & "'"
            strFrom = "  INNER JOIN GL_SUBBLK SUB ON NB.LocCode=SUB.LocCode AND NB.BlkCode=SUB.BlkCode  AND NB.BatchNo = SUB.SubBlkCode"
            strWhere = "NB.LocCode = '" & pv_strLocation & "' And SUB.SubBlkCode = '" & pv_strBlockCode & "' And NB.Status = '" & objNUSetup.EnumNurseryBatchStatus.Active & "'"
            strOrderBy = "NB.BatchNo"
            
            intErrNo = objNUTrx.mtdGetSeedlingsIssue(strOpCodeNurseryBatch_GET, strSelect, strFrom, strWhere, strOrderBy, dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_BIND_BATCH_NO&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("_BatchNo") = dsList.Tables(0).Rows(intCnt).Item("BatchNo")
            dsList.Tables(0).Rows(intCnt).Item("Description") = dsList.Tables(0).Rows(intCnt).Item("BatchNo")
            If dsList.Tables(0).Rows(intCnt).Item("_BatchNo") = pv_strBatchNo.Trim() Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If dsList.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        drNew = dsList.Tables(0).NewRow()
        drNew("_BatchNo") = ""
        drNew("Description") = lblSelect.Text & lblBatchNoTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)
        
        If pv_strBlockCode <> "" And intSelectedIndex = 0 And APPEND_BATCH_NO = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("_BatchNo") = pv_strBatchNo.Trim()
            drNew("Description") = pv_strBatchNo.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If
        
        ddlBatchNo.DataSource = dsList.Tables(0)
        ddlBatchNo.DataValueField = "_BatchNo"
        ddlBatchNo.DataTextField = "Description"
        ddlBatchNo.DataBind()
        ddlBatchNo.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindChargeToDropDownList(ByVal pv_strLocCode As String)
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
                       objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Nursery) & "|" & _
                       "NUAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch) & "|" & _
                       "NUAR" & "|" & _
                       objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch)
            intErrNo = objAdminLoc.mtdGetInterEstateLoc(strOpCd, _
                                                        strParam, _
                                                        dsLoc)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_BIND_CHARGELOCCODE&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
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
        ddlChargeTo.DataSource = dsLoc.Tables(0)
        ddlChargeTo.DataValueField = "LocCode"
        ddlChargeTo.DataTextField = "Description"
        ddlChargeTo.DataBind()
        'ddlChargeTo.SelectedIndex = intSelectedIndex
        ddlChargeTo.SelectedValue = Session("SS_LOCATION")
        If Not dsLoc Is Nothing Then
            dsLoc = Nothing
        End If
    End Sub

    Sub BindAccCodeDropDownList(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_BIND_ACCOUNT&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("AccCode") = dsList.Tables(0).Rows(intCnt).Item("AccCode").Trim()
            dsList.Tables(0).Rows(intCnt).Item("Description") = dsList.Tables(0).Rows(intCnt).Item("AccCode").Trim() & " (" & dsList.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If LCase(dsList.Tables(0).Rows(intCnt).Item("AccCode").Trim()) = LCase(pv_strAccCode.Trim()) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("AccCode") = ""
        drNew("Description") = lblSelect.Text & lblAccCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If pv_strAccCode <> "" And intSelectedIndex = 0 And APPEND_ACC_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("AccCode") = pv_strAccCode.Trim()
            drNew("Description") = pv_strAccCode.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If
        
        ddlAccCode.DataSource = dsList.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub
    
    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Clear()
        ddlChargeLevel.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.Block), objLangCap.EnumLangCap.Block))
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                ddlChargeLevel.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.Block), objLangCap.EnumLangCap.Block))
            Else
                ddlChargeLevel.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.SubBlock), objLangCap.EnumLangCap.SubBlock))
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_BIND_CHARGE_LEVEL&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try
        
        ddlChargeLevel.SelectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        trChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub
    
     Sub BindPreBlkCodeDropDownList(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNT_PREBLOCK_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        Try
            strParam = pv_strAccCode & "|" & ddlChargeTo.SelectedItem.Value.Trim() & "|" & objGLSetup.EnumBlockStatus.Active
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_BIND_PREBLOCK&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("BlkCode") = dsList.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            dsList.Tables(0).Rows(intCnt).Item("Description") = dsList.Tables(0).Rows(intCnt).Item("BlkCode").Trim() & " (" & dsList.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If LCase(dsList.Tables(0).Rows(intCnt).Item("BlkCode").Trim()) = LCase(pv_strBlkCode.Trim()) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("BlkCode") = ""
        drNew("Description") = lblSelect.Text & lblPreBlkCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)
        
        If pv_strAccCode <> "" And intSelectedIndex = 0 And APPEND_BLK_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("BlkCode") = pv_strBlkCode.Trim()
            drNew("Description") = pv_strBlkCode.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If

        ddlPreBlkCode.DataSource = dsList.Tables(0)
        ddlPreBlkCode.DataValueField = "BlkCode"
        ddlPreBlkCode.DataTextField = "Description"
        ddlPreBlkCode.DataBind()
        ddlPreBlkCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub
    
    Sub BindBlkCodeDropDownList(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String
        
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strOpCd = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode.Trim() & "|" & ddlChargeTo.SelectedItem.Value.Trim() & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode.Trim() & "|" & ddlChargeTo.SelectedItem.Value.Trim() & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_BIND_BLOCK&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("BlkCode") = dsList.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            dsList.Tables(0).Rows(intCnt).Item("Description") = dsList.Tables(0).Rows(intCnt).Item("BlkCode").Trim() & " (" & dsList.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If LCase(dsList.Tables(0).Rows(intCnt).Item("BlkCode").Trim()) = LCase(pv_strBlkCode.Trim()) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If dsList.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If


        drNew = dsList.Tables(0).NewRow()
        drNew("BlkCode") = ""
        drNew("Description") = lblSelect.Text & lblBlkCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)
        
        If pv_strAccCode <> "" And intSelectedIndex = 0 And APPEND_BLK_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("BlkCode") = pv_strBlkCode.Trim()
            drNew("Description") = pv_strBlkCode.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If
        
        ddlBlkCode.DataSource = dsList.Tables(0)
        ddlBlkCode.DataValueField = "BlkCode"
        ddlBlkCode.DataTextField = "Description"
        ddlBlkCode.DataBind()
        ddlBlkCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindVehCodeDropDownList(ByVal pv_strAccCode As String, ByVal pv_strVehCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_VEH_LIST_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String
        
        strParam = "|AccCode = '" & pv_strAccCode.Trim() & "' AND LocCode = '" & ddlChargeTo.SelectedItem.Value.Trim() & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'"
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_BIND_BLOCK&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try
        
        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("VehCode") = dsList.Tables(0).Rows(intCnt).Item("VehCode").Trim()
            dsList.Tables(0).Rows(intCnt).Item("Description") = dsList.Tables(0).Rows(intCnt).Item("VehCode").Trim() & " ( " & _
                                                                dsList.Tables(0).Rows(intCnt).Item("Description").Trim() & " )"
            If LCase(dsList.Tables(0).Rows(intCnt).Item("VehCode").Trim()) = LCase(pv_strVehCode.Trim()) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drNew = dsList.Tables(0).NewRow()
        drNew("VehCode") = ""
        drNew("Description") = lblSelect.Text & lblVehCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)
        
        If pv_strVehCode <> "" And intSelectedIndex = 0 And APPEND_VEH_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("VehCode") = pv_strVehCode.Trim()
            drNew("Description") = pv_strVehCode.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If

        ddlVehCode.DataSource = dsList.Tables(0)
        ddlVehCode.DataValueField = "VehCode"
        ddlVehCode.DataTextField = "Description"
        ddlVehCode.DataBind()
        ddlVehCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindVehExpCodeDropDownList(ByVal pv_strVehExpCode As String, ByVal pv_IsBlankList As Boolean)
        Dim strOpCd As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String
        
        strParam = "Order By VehExpenseCode ASC|And Veh.Status = '" & objGLSetup.EnumVehicleExpenseStatus.active & "'"
        Try
            If pv_IsBlankList Then
                strParam += " And Veh.VehExpensecode = ''"
            End If
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.VehicleExpense, _
                                                   dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_BIND_VEHEXP&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode") = dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode").Trim()
            dsList.Tables(0).Rows(intCnt).Item("Description") = dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode").Trim() & " ( " & _
                                                                dsList.Tables(0).Rows(intCnt).Item("Description").Trim() & " )"
            If LCase(dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode").Trim()) = LCase(pv_strVehExpCode.Trim()) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt
        
        drNew = dsList.Tables(0).NewRow()
        drNew(0) = ""
        drNew(1) = lblSelect.Text & lblVehExpCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If pv_strVehExpCode <> "" And intSelectedIndex = 0 And APPEND_VEH_EXP_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("VehExpenseCode") = pv_strVehExpCode.Trim()
            drNew("Description") = pv_strVehExpCode.Trim() & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count
        End If
        
        ddlVehExpCode.DataSource = dsList.Tables(0)
        ddlVehExpCode.DataValueField = "VehExpenseCode"
        ddlVehExpCode.DataTextField = "Description"
        ddlVehExpCode.DataBind()
        ddlVehExpCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub
    
    Sub ddlChargeTo_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strAccCode As String = ddlAccCode.SelectedItem.Value.Trim() 'Request.Form("ddlAccCode")
        Dim strVehCode As String = ddlVehCode.SelectedItem.Value.Trim() 'Request.Form("ddlVehCode")
        Dim strPreBlkCode As String = ddlPreBlkCode.SelectedItem.Value.Trim()   'Request.Form("ddlPreBlock")
        Dim strBlkCode As String = ddlBlkCode.SelectedItem.Value.Trim() 'Request.Form("ddlBlock")
        BindVehCodeDropDownList(strAccCode, strVehCode)
        BindPreBlkCodeDropDownList(strAccCode, strPreBlkCode)
        BindBlkCodeDropDownList(strAccCode, strBlkCode)
        hidChargeLocCode.value = ddlChargeTo.SelectedItem.Value.Trim()
        lblChargeToErr.Visible = (ddlChargeTo.SelectedIndex < 1)
    End Sub
    
    Sub ddlAccCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strAccCode As String = ddlAccCode.SelectedItem.Value.Trim()
        Dim strPreBlkCode As String = ddlPreBlkCode.SelectedItem.Value.Trim()
        Dim strBlkCode As String = ddlBlkCode.SelectedItem.Value.Trim()
        Dim strVehCode As String = ddlVehCode.SelectedItem.Value.Trim()
        Dim strVehExpCode As String = ddlVehExpCode.SelectedItem.Value.Trim()
        GetAccountProperties(strAccCode, intAccType, intAccPurpose, intNurseryInd)

        If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLSetup.EnumAccountPurpose.NonVehicle
                    BindPreBlkCodeDropDownList(strAccCode, strPreBlkCode)
                    BindBlkCodeDropDownList(strAccCode, strBlkCode)
                    BindVehCodeDropDownList("", "")
                    BindVehExpCodeDropDownList("", True)
                Case objGLSetup.EnumAccountPurpose.VehicleDistribution
                    BindPreBlkCodeDropDownList("", "")
                    BindBlkCodeDropDownList("", "")
                    BindVehCodeDropDownList(strAccCode, strVehCode)
                    BindVehExpCodeDropDownList(strVehExpCode, False)
                Case Else
                    BindPreBlkCodeDropDownList(strAccCode, strPreBlkCode)
                    BindBlkCodeDropDownList(strAccCode, strBlkCode)
                    BindVehCodeDropDownList("%", strVehCode)
                    BindVehExpCodeDropDownList(strVehExpCode, False)
            End Select
        ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
            BindPreBlkCodeDropDownList(strAccCode, strPreBlkCode)
            BindBlkCodeDropDownList(strAccCode, strBlkCode)
            BindVehCodeDropDownList("", "")
            BindVehExpCodeDropDownList("", True)
        Else
            BindPreBlkCodeDropDownList("", "")
            BindBlkCodeDropDownList("", "")
            BindVehCodeDropDownList("", "")
            BindVehExpCodeDropDownList("", True)
        End If
    End Sub
    
    Sub ddlChargeLevel_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ToggleChargeLevel()
    End Sub
    
    Sub ddlNurseryBlock_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strNurseryBlock As String = ddlNurseryBlock.SelectedItem.Value.Trim()
        BindItemCodeDropDownList(strLocation, strNurseryBlock, "")  
        BindBatchNoDropDownList(strLocation, strNurseryBlock, "")
        BindLocationByBlock(strNurseryBlock, "")
        BindAccCodeByBlock(strLocation, strNurseryBlock, "")  
    End Sub

    Sub lstItem_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strNurseryBlock As String = ddlNurseryBlock.SelectedItem.Value.Trim()
        Dim strItemCode As String = lstItem.SelectedItem.Value.Trim()
        BindBatchNoByBlkItem(strLocation, strNurseryBlock, strItemCode, "")
        BindLocationByBlkItem(strNurseryBlock, strItemCode, "")
    End Sub
    
    Protected Function GetSeedlingsIssueHeaderDS() As DataSet
        Dim dsHeader As DataSet
        Dim strSelect As String
        Dim strFrom As String
        Dim strWhere As String
        Dim strOrderBy As String
        
        strSelect = ", COALESCE(USR.UserName, '') AS UserName"
        strFrom = " LEFT OUTER JOIN SH_USER USR ON USR.UserID = SI.UpdateID "
        strWhere = " SI.IssueID = '" & lblIssueID.Text.Trim() & "'"
        strOrderBy = " SI.IssueID"
        Try
            intErrNo = objNUTrx.mtdGetSeedlingsIssue(strOpCdSeedlingsIssue_GET, _
                                                     strSelect, _
                                                     strFrom, _
                                                     strWhere, _
                                                     strOrderBy, _
                                                     dsHeader)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_GET_HEADER&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try
        
        Return dsHeader
        If Not dsHeader Is Nothing Then 
            dsHeader = Nothing
        End If
    End Function
    
    Protected Function GetSeedlingsIssueLineDS() As DataSet
        Dim dsLines As DataSet
        Dim strSelect As String
        Dim strFrom As String
        Dim strWhere As String
        Dim strOrderBy As String
        
        strSelect = ", SIL.ItemCode"
        strFrom = ""
        strWhere = "SIL.IssueID = '" & lblIssueID.Text.Trim() & "'"
        strOrderBy = "SIL.IssueLNID"
        
        Try
            intErrNo = objNUTrx.mtdGetSeedlingsIssue(strOpCdSeedlingsIssueLine_GET, _
                                                     strSelect, _
                                                     strFrom, _
                                                     strWhere, _
                                                     strOrderBy, _
                                                     dsLines)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_GET_LINE&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try
        
        Return dsLines
        If Not dsLines Is Nothing Then
            dsLines = Nothing
        End If
    End Function
    
    Function GetLanguageCaptionDS() As DataSet
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim dsLC As DataSet
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
                                                 dsLC, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_GET_LANG_CAP&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try
        
        Return dsLC
        If Not dsLC Is Nothing Then
            dsLC = Nothing
        End If
    End Function
    
    Sub GetAccountProperties(ByVal pv_strAccCode As String, _
                             ByRef pr_strAccType As Integer, _
                             ByRef pr_strAccPurpose As Integer, _
                             ByRef pr_strNurseryInd As Integer)

        Dim objAccDs As New DataSet()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer
        
        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_GET_ACCOUNT_PROPERTY&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try
        
        If objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
        End If
    End Sub

    Sub DisplaySeedlingsIssueHeader()
        Dim intCnt As Integer
        Dim dsHeader As DataSet
        dsHeader = GetSeedlingsIssueHeaderDS()
        lblStatusCode.Text = dsHeader.Tables(0).Rows(0).Item("Status").Trim()
        lblIssueID.Text = dsHeader.Tables(0).Rows(0).Item("IssueID").Trim()
        lblPeriod.Text = dsHeader.Tables(0).Rows(0).Item("AccMonth").Trim() & "/" & dsHeader.Tables(0).Rows(0).Item("AccYear").Trim()
        lblStatus.Text = objNUTrx.mtdGetSeedlingsIssueStatus(dsHeader.Tables(0).Rows(0).Item("Status").Trim())
        lblCreateDate.Text = objGlobal.GetLongDate(dsHeader.Tables(0).Rows(0).Item("CreateDate"))
        lblUpdateDate.Text = objGlobal.GetLongDate(dsHeader.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdateID.Text = dsHeader.Tables(0).Rows(0).Item("UserName").Trim()
        txtDocRefNo.Text = dsHeader.Tables(0).Rows(0).Item("DocRefNo").Trim()
        txtIssueDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), dsHeader.Tables(0).Rows(0).Item("IssueDate"))
        txtRemark.Text = dsHeader.Tables(0).Rows(0).Item("Remark").Trim()
        BindNurseryBlockDropDownList(dsHeader.Tables(0).Rows(0).Item("LocCode").Trim(), dsHeader.Tables(0).Rows(0).Item("BlkCode").Trim())
        BindItemCodeDropDownList(dsHeader.Tables(0).Rows(0).Item("LocCode").Trim(), dsHeader.Tables(0).Rows(0).Item("BlkCode").Trim(), "")
        BindLocationByBlock(dsHeader.Tables(0).Rows(0).Item("BlkCode").Trim(), "")  
        BindAccCodeByBlock(dsHeader.Tables(0).Rows(0).Item("LocCode").Trim(), dsHeader.Tables(0).Rows(0).Item("BlkCode").Trim(), "")
        BindBatchNoDropDownList(dsHeader.Tables(0).Rows(0).Item("LocCode").Trim(), dsHeader.Tables(0).Rows(0).Item("BlkCode").Trim(), "")
    End Sub
    
    Sub DisplaySeedlingsIssueLines()
        Dim decTotalAmount As Decimal
        Dim lbl As Label
        Dim dgiLine As DataGridItem
        
        dgLines.DataSource = GetSeedlingsIssueLineDS()
        dgLines.DataBind()
        
        For Each dgiLine In dgLines.Items
            If dgiLine.ItemType = ListItemType.Item Or dgiLine.ItemType = ListItemType.AlternatingItem Then
                lbl = dgiLine.FindControl("lblAmount")
                decTotalAmount = Decimal.Add(decTotalAmount, lbl.Text.Trim())
            End If
        Next

        lblTotalAmount.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(decTotalAmount, 0))
    End Sub
    
    Sub dgLines_OnItemDataBound(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim btn As LinkButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            btn = e.Item.FindControl("Delete")
            If lblStatusCode.Text.Trim() = Trim(CStr(objNUTrx.EnumSeedlingsIssueStatus.Active)) Then
                btn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                btn.visible = True
            Else
                btn.visible = False
            End If
        End If
    End Sub
    
    Sub dgLines_OnDeleteCommand(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lblDG As Label
        Dim strIssueID As String
        Dim strIssueLNID As String
        Dim intErrNum As Integer
        Dim strErrMsg As String
        Dim strParam As String
        
        If lblStatusCode.Text.Trim() = Trim(CStr(objNUTrx.EnumSeedlingsIssueStatus.Active)) Then
            strIssueID = lblIssueID.Text.Trim()
            lblDG = E.Item.FindControl("lblIssueLNID")
            strIssueLNID = lblDG.Text.Trim()
            strParam = strIssueLnID & vbCrLf & strIssueID & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & _
                       "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & _
                       "" & vbCrLf & "" & vbCrLf & ddlNurseryBlock.SelectedItem.Value.Trim()
            Try
                intErrNo = objNUTrx.mtdUpdSeedlingsIssueLine(strOpCdSeedlingsIssueLine_ADD, _
                                                             strOpCdSeedlingsIssueLine_UPD, _
                                                             strOpCdSeedlingsIssueLine_GET, _
                                                             strOpCdSeedlingsIssueLine_DEL, _
                                                             strOpCdSeedlingsIssue_UPD, _
                                                             strOpCodeBlockChargingList_GET, _
                                                             "", _
                                                             False, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strParam, _
                                                             objNUTrx.EnumOperation.Delete, _
                                                             strIssueLNID, _
                                                             intErrNum)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_UPDATE_LINE&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
            End Try
            
            If intErrNum = objNUTrx.EnumErrorType.NoError Then    
                Dim strItemParam As String
                Dim strItemQty As String
                Dim strItemCode As String

                lblDG = E.Item.FindControl("lblDGItemCode")
                strItemCode = Trim(lblDG.Text)
                lblDG = E.Item.FindControl("lblIDQty")
                strItemQty = Trim(lblDG.Text)

                strItemParam = strItemCode & "|" & Trim(strItemQty) & "|-" & Trim(strItemQty)
                Try
                    intErrNo = objINTrx.mtdUpdInvItemQty("IN_CLSTRX_STOCKITEM_DETAIL_UPD", _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strItemParam)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_ITEM_QTY_UPDATE&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
                End Try
                BindItemCodeList("")
                ResetPage(True, True, True)
            Else
                lblActionResult.Text = "System failed to delete seedlings issue line."
                lblActionResult.Visible = True
            End If
        End If
    End Sub
    
    Sub ibAdd_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strIssueID As String
        Dim strIssueLNID As String
        Dim strDocRefNo As String
        Dim strIssueDate As String
        Dim strDateFormat As String
        Dim strParam As String
        Dim strParamList As String
        Dim intErrNum As Integer
        Dim strErrMsg As String
        
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        
        Dim strNurseryBlock As String = ddlNurseryBlock.SelectedItem.Value.Trim()
        Dim strBatchNo As String = ddlBatchNo.SelectedItem.Value.Trim()
        Dim strChargeLocCode As String = Request.Form("ddlChargeTo") 
        Dim strAccCode As String = Request.Form("ddlAccCode") 
        Dim strPreBlkCode As String = Request.Form("ddlPreBlkCode") 
        Dim strBlkCode As String = Request.Form("ddlBlkCode") 
        Dim strVehCode As String = Request.Form("ddlVehCode") 
        Dim strVehExpCode As String = Request.Form("ddlVehExpCode") 
        Dim decQty As Decimal
        Dim decCost As Decimal
        Dim decAmount As Decimal
        Dim blnBlockCharge As Boolean = False   
        
        Dim strItemCode As String

        strItemCode = Trim(lstItem.SelectedItem.Value)

        lblActionResult.Visible = False
        lblIssueDateErr.Visible = False
        lblDocRefNoErr.Visible = False
        lblNurseryBlockErr.Visible = False
        lblBatchNoErr.Visible = False
        lblAccCodeErr.Visible = False
        lblPreBlkCodeErr.Visible = False
        lblBlkCodeErr.Visible = False
        lblVehCodeErr.Visible = False
        lblVehExpCodeErr.Visible = False
        lblQtyErr.Visible = False
        
        strDocRefNo = txtDocRefNo.Text.Trim()
        strIssueDate = txtIssueDate.Text.Trim()
        If strDocRefNo = "" Then
            lblDocRefNoErr.Visible = True
        End If
        If strNurseryBlock = "" Then
            lblNurseryBlockErr.Visible = True
        End If
        If strIssueDate = "" Then
            lblIssueDateErr.Text = "<br>Issue Date cannot be blank!"
            lblIssueDateErr.Visible = True
        ElseIf CheckDate(strIssueDate, strIssueDate, strDateFormat) = False Then
            lblIssueDateErr.Text = "<br>Date Entered should be in the format " & strDateFormat
            lblIssueDateErr.Visible = True
        End If
        
        If strItemCode = "" Then
            lblItemCodeErr.Visible = True
            Exit Sub
        End If

        If CheckNurseryItemQty(strItemCode) = False Then
            lblActionResult.Text = "Insufficient quantity for " & strItemCode
            lblActionResult.Visible = True
            Exit Sub
        End If

        If strBatchNo = "" Then
            lblBatchNoErr.Visible = True
        End If
        
        If strAccCode = "" Then
            lblAccCodeErr.Visible = True
        Else
            GetAccountProperties(strAccCode, intAccType, intAccPurpose, intNurseryInd)

            If ddlChargeLevel.SelectedIndex = 0 Then
                If (intAccType = objGLSetup.EnumAccountType.ProfitAndLost And (Not intAccPurpose = objGLSetup.EnumAccountPurpose.VehicleDistribution)) Or _
                   (intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes) Then
                    strBlkCode = strPreBlkCode
                    blnBlockCharge = True
                End If
            End If

            If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
                If strBlkCode = "" And Not intAccPurpose = objGLSetup.EnumAccountPurpose.VehicleDistribution Then
                    If ddlChargeLevel.SelectedIndex = 0 Then
                        lblPreBlkCodeErr.Visible = True
                    Else
                        lblBlkCodeErr.Visible = True
                    End If
                ElseIf strVehCode = "" And intAccPurpose = objGLSetup.EnumAccountPurpose.VehicleDistribution Then
                    lblVehCodeErr.Visible = True
                ElseIf strVehExpCode = "" And intAccPurpose = objGLSetup.EnumAccountPurpose.VehicleDistribution Then
                    lblVehCodeErr.Visible = True
                ElseIf strVehCode <> "" And strVehExpCode = "" And intAccPurpose = objGLSetup.EnumAccountPurpose.others Then
                    lblVehExpCodeErr.Visible = True
                ElseIf strVehCode = "" And strVehExpCode <> "" And intAccPurpose = objGLSetup.EnumAccountPurpose.others Then
                    lblVehCodeErr.Visible = True
                End If
            ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
                If ddlChargeLevel.SelectedIndex = 0 Then
                    If strBlkCode = "" Then
                        lblPreBlkCodeErr.Visible = True
                    End If
                Else
                    If strBlkCode = "" Then
                        lblBlkCodeErr.Visible = True
                    End If    
                End If
            End If
        End If
        
        If txtQty.Text.Trim() = "" Then
            lblQtyErr.Text = "Please enter Quantity - Seedlings."
            lblQtyErr.Visible = True
        Else
            decQty = txtQty.Text.Trim()
        End If
        If txtCost.Text.Trim() = "" Then
            decCost = "0"
        Else
            decCost = txtCost.Text.Trim()
        End If

        If txtAmount.Text.Trim() = "" Then
            decAmount = "0"
        Else
            decAmount = txtAmount.Text.Trim()
        End If
        If lblActionResult.Visible = True Or lblIssueDateErr.Visible = True Or _
            lblDocRefNoErr.Visible = True Or lblNurseryBlockErr.Visible = True Or _
            lblBatchNoErr.Visible = True Or lblChargeToErr.Visible = True Or _
            lblAccCodeErr.Visible = True Or lblPreBlkCodeErr.Visible = True Or _
            lblBlkCodeErr.Visible = True Or lblVehCodeErr.Visible = True Or _
            lblVehExpCodeErr.Visible = True Or lblQtyErr.Visible = True Then
            Exit Sub
        End If
        
        strIssueID = lblIssueID.Text.Trim()
        If strIssueID = "" Then
            strParam = strIssueID & vbCrLf & strLocation & vbCrLf & strDocRefNo & vbCrLf & strNurseryBlock & vbCrLf & strIssueDate & vbCrLf & _
                       txtRemark.Text.Trim() & vbCrLf & strAccMonth & vbCrLf & strAccYear & vbCrLf & objNUTrx.EnumSeedlingsIssueStatus.Active & vbCrLf & Now
            Try
                intErrNo = objNUTrx.mtdUpdSeedlingsIssue(strOpCdSeedlingsIssue_ADD, _
                                                         strOpCdSeedlingsIssue_UPD, _
                                                         strOpCdSeedlingsIssue_GET, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         strParam, _
                                                         objNUTrx.EnumOperation.Add, _
                                                         strIssueID)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_UPDATE_HEADER&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
            End Try

            If strIssueID <> "" Then
                lblIssueID.Text = strIssueID
            Else
                lblActionResult.Text = "System failed to create new Seedlings Issue document. Seedlings Issue line is not created."
                lblActionResult.Visible = True
                Exit Sub
            End If
        End If

        strParam = strIssueLnID & vbCrLf & strIssueID & vbCrLf & txtDescription.Text.Trim() & vbCrLf & strBatchNo & vbCrLf & strAccCode & vbCrLf & _
                   strBlkCode & vbCrLf & strVehCode & vbCrLf & strVehExpCode & vbCrLf & decQty & vbCrLf & decCost & vbCrLf & _
                   decAmount & vbCrLf & "" & vbCrLf & strNurseryBlock & vbCrLf & strChargeLocCode & vbCrLf & strItemCode

        strParamList = ddlChargeTo.SelectedItem.Value.Trim() & "|" & _
                       ddlAccCode.SelectedItem.Value.Trim() & "|" & _
                       ddlPreBlkCode.SelectedItem.Value.Trim() & "|" & _
                       objGLSetup.EnumBlockStatus.Active
        Try
            intErrNo = objNUTrx.mtdUpdSeedlingsIssueLine(strOpCdSeedlingsIssueLine_ADD, _
                                                         strOpCdSeedlingsIssueLine_UPD, _
                                                         strOpCdSeedlingsIssueLine_GET, _
                                                         strOpCdSeedlingsIssueLine_DEL, _
                                                         strOpCdSeedlingsIssue_UPD, _
                                                         strOpCodeBlockChargingList_GET, _
                                                         strParamList, _
                                                         blnBlockCharge, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         strParam, _
                                                         objNUTrx.EnumOperation.Add, _
                                                         strIssueLNID, _
                                                         intErrNum)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_UPDATE_LINE&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try

        If intErrNum = objNUTrx.EnumErrorType.NoError Then    
            Dim strItemParam As String

            strItemParam = strItemCode & "|-" & Trim(txtQty.Text) & "|" & Trim(txtQty.Text)
            Try
                intErrNo = objINTrx.mtdUpdInvItemQty("IN_CLSTRX_STOCKITEM_DETAIL_UPD", _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     strItemParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_ITEM_QTY_UPDATE&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
            End Try
            BindItemCodeList("")
            ResetPage(True, True, True)
        Else
            Select Case intErrNum
                Case objNUTrx.EnumErrorType.SubBlockNotFound
                    lblActionResult.Text = "Please check " & GetCaption(objLangCap.EnumLangCap.SubBlock) & " setup"
                    lblActionResult.Visible = True

                Case Else   
                    lblActionResult.Text = "System failed to create new Seedlings Issue line."
                    lblActionResult.Visible = True
            End Select
        End If
    End Sub
    
    Sub ibSave_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strIssueID As String
        Dim strDocRefNo As String
        Dim strIssueDate As String
        Dim strDateFormat As String
        Dim strNurseryBlock As String = ddlNurseryBlock.SelectedItem.Value.Trim()
        Dim intAction As Integer
        Dim intErrNum As Integer
        Dim strParam As String
        
        lblActionResult.Visible = False
        lblIssueDateErr.Visible = False
        lblDocRefNoErr.Visible = False
        lblNurseryBlockErr.Visible = False
        strDocRefNo = txtDocRefNo.Text.Trim()
        strIssueDate = txtIssueDate.Text.Trim()
        If strDocRefNo = "" Then
            lblDocRefNoErr.Visible = True
        End If
        If strNurseryBlock = "" Then
            lblNurseryBlockErr.Visible = True
        End If
        If strIssueDate = "" Then
            lblIssueDateErr.Text = "<br>Issue Date cannot be blank!"
            lblIssueDateErr.Visible = True
        ElseIf CheckDate(strIssueDate, strIssueDate, strDateFormat) = False Then
            lblIssueDateErr.Text = "<br>Date Entered should be in the format " & strDateFormat
            lblIssueDateErr.Visible = True
        End If
        
        If lblActionResult.Visible = True Or lblIssueDateErr.Visible = True Or _
           lblDocRefNoErr.Visible = True Or lblNurseryBlockErr.Visible = True Then
            Exit Sub
        End If
        
        strIssueID = lblIssueID.Text.Trim()
        If strIssueID = "" Then
            strParam = strIssueID & vbCrLf & strLocation & vbCrLf & strDocRefNo & vbCrLf & strNurseryBlock & vbCrLf & strIssueDate & vbCrLf & _
                       txtRemark.Text.Trim() & vbCrLf & strAccMonth & vbCrLf & strAccYear & vbCrLf & objNUTrx.EnumSeedlingsIssueStatus.Active & vbCrLf & Now
            intAction = objNUTrx.EnumOperation.Add
        Else
            strParam = strIssueID & vbCrLf & "" & vbCrLf & strDocRefNo & vbCrLf & strNurseryBlock & vbCrLf & strIssueDate & vbCrLf & _
                       txtRemark.Text.Trim() & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & ""
            intAction = objNUTrx.EnumOperation.Update
        End If
        
        Try
            intErrNo = objNUTrx.mtdUpdSeedlingsIssue(strOpCdSeedlingsIssue_ADD, _
                                                     strOpCdSeedlingsIssue_UPD, _
                                                     strOpCdSeedlingsIssue_GET, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     strParam, _
                                                     intAction, _
                                                     strIssueID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_UPDATE_HEADER&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try
        
        If intAction = objNUTrx.EnumOperation.Add And strIssueID <> "" Then
            lblIssueID.Text = strIssueID
            DisplaySeedlingsIssueHeader()
            DisplaySeedlingsIssueLines()
            SetObjectAccessibilityByStatus()
        ElseIf intAction = objNUTrx.EnumOperation.Add Then
            lblActionResult.Text = "System failed to create new Seedlings Issue document."
            lblActionResult.Visible = True
        End If
    End Sub
    
    Sub ibConfirm_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim colParam As New Collection
        Dim colOpCode As New Collection
        Dim intError As Integer
        Dim strErrMsg As String
        
        If CheckLocBillParty() = False Then
            lblBPErr.Visible = True
            lblLocCodeErr.Visible = True
            Exit Sub
        End If  

        If CheckAllNurseryItemQty(Trim(lblIssueID.Text)) = False Then
            Exit Sub
        End If

        colOpCode.Add("NU_CLSTRX_SEEDLINGS_ISSUE_GET_FOR_CONFIRM", "ISSUE_LINE_GET_FOR_CONFIRM")
        colOpCode.Add(strOpCodeNurseryBatch_GET, "NURSERY_BATCH_GET")
        colOpCode.Add(strOpCodeNurseryBatch_UPD, "NURSERY_BATCH_UPD")
        colOpCode.Add(strOpCdSeedlingsIssue_UPD, "ISSUE_HEADER_UPD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_DETAIL_ADD", "JOURNAL_ADD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_DETAIL_UPD", "JOURNAL_UPD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_LINE_ADD", "JOURNAL_LINE_ADD")
        colOpCode.Add("GL_CLSTRX_JOURNAL_LINE_GET", "JOURNAL_LINE_GET")
        
        colParam.Add(strCompany, "COMPANY")
        colParam.Add(strLocation, "LOCCODE")
        colParam.Add(strUserId, "USER_ID")
        colParam.Add(lblIssueID.Text, "ISSUE_ID")
        colParam.Add(intConfigsetting, "CONFIG_VALUE")

        colParam.Add(GetCaption(objLangCap.EnumLangCap.BatchNo), "MS_NURSERY_BATCH_NO")
        colParam.Add("Inter-" & GetCaption(objLangCap.EnumLangCap.Location), "MS_INTER_LOCATION")
        colParam.Add(GetCaption(objLangCap.EnumLangCap.Account), "MS_COA")
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), Session("SS_CONFIGSETTING")) = True Then
            colParam.Add(GetCaption(objLangCap.EnumLangCap.Block), "MS_BLOCK")
            colParam.Add(GetCaption(objLangCap.EnumLangCap.NurseryBlock), "MS_NURSERY_BLOCK")
        Else
            colParam.Add(GetCaption(objLangCap.EnumLangCap.SubBlock), "MS_BLOCK")
            colParam.Add(GetCaption(objLangCap.EnumLangCap.NurserySubBlock), "MS_NURSERY_BLOCK")
        End If
        colParam.Add(GetCaption(objLangCap.EnumLangCap.Vehicle), "MS_VEHICLE")
        colParam.Add(GetCaption(objLangCap.EnumLangCap.VehExpense), "MS_VEHEXP")
        
        intError = objNUTrx.EnumErrorType.NoError
        strErrMsg = ""
        
        Try
            intErrNo = objNUTrx.mtdSeedlingsIssue_Confirm(colOpCode, _
                                                          colParam, _
                                                          intError, _
                                                          strErrMsg)
            
            If intError = objNUTrx.EnumErrorType.NoError Then
                DisplaySeedlingsIssueHeader()
                DisplaySeedlingsIssueLines()
                SetObjectAccessibilityByStatus()
            Else
                lblActionResult.Text = strErrMsg
                lblActionResult.Visible = True
                Exit Sub 
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_CONFIRM&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try

        Dim ErrorChk As Integer = objINTrx.EnumInventoryErrorType.NoError

        Try
            intErrNo = objINTrx.mtdAdjustInvItemLevel("NU_CLSTRX_SEEDLINGSISSUE_LINE_GET", _
                                                        "IN_CLSTRX_STOCKITEM_DETAIL_UPD", _
                                                        "IN_CLSSETUP_STOCKITEM_DETAILS_GET", _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        Trim(lblIssueID.Text), _
                                                        objINTrx.EnumTransactionAction.Confirm, _
                                                        ErrorChk)
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_UNDELETE&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
            End If
        End Try

        Dim strDNParam As String = TRIM(lblIssueID.Text) & "|" & strOpCdSeedlingsIssueLine_GET
        Dim objDNId As Object
        Dim strDocTypeId As String = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNote) & "|" & _ 
                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.BIDebitNoteLn)

        Try
            intErrNo = objNUTrx.mtdAddBillingDebitNote_InterEstate(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        Session("SS_ARACCMONTH"), _
                                                        Session("SS_ARACCYEAR"), _
                                                        strDocTypeId, _
                                                        strDNParam, _
                                                        objDNId)
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_AUTO_DN_ADD&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
            End If
        End Try

    End Sub
    
    Sub ibDelete_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strIssueID As String
        Dim intStatus As Integer
        Dim intErrNum As Integer
        Dim intAction As Integer
        Dim strParam As String
        
        strIssueID = lblIssueID.Text.Trim()

        If CInt(lblStatusCode.Text.Trim()) = objNUTrx.EnumSeedlingsIssueStatus.Active Then
            intStatus = objNUTrx.EnumSeedlingsIssueStatus.Deleted
            intAction = 1
        Else
            intStatus = objNUTrx.EnumSeedlingsIssueStatus.Active
            intAction = 2
        End If

        Dim ErrorChk As Integer = objINTrx.EnumInventoryErrorType.NoError

        If intAction = 2 Then
            Try
                intErrNo = objINTrx.mtdAdjustInvItemLevel("NU_CLSTRX_SEEDLINGSISSUE_LINE_GET", _
                                                         "IN_CLSTRX_STOCKITEM_DETAIL_UPD", _
                                                         "IN_CLSSETUP_STOCKITEM_DETAILS_GET", _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         strIssueID, _
                                                         objINTrx.EnumTransactionAction.Undelete, _
                                                         ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_UNDELETE&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
                End If
            End Try
        Else
            Try
                intErrNo = objINTrx.mtdAdjustInvItemLevel("NU_CLSTRX_SEEDLINGSISSUE_LINE_GET", _
                                                         "IN_CLSTRX_STOCKITEM_DETAIL_UPD", _
                                                         "IN_CLSSETUP_STOCKITEM_DETAILS_GET", _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         strIssueID, _
                                                         objINTrx.EnumTransactionAction.Delete, _
                                                         ErrorChk)
            Catch Exp As System.Exception
                If intErrNo <> -5 Then
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_DELETE&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
                End If
            End Try
        End If

        If intErrNo <> 0 Then
            lblActionResult.Text = "System failed to " & IIf(intAction = 1, "delete", "undelete") & " the Seedlings issue " & strIssueID & "."
            lblActionResult.Visible = True
            Exit Sub
        Elseif ErrorChk = objINTrx.EnumInventoryErrorType.InsufficientQty Then
            lblActionResult.Text = "Insufficient quantity."
            lblActionResult.Visible = True
            Exit Sub
        End If

        BindItemCodeList("")
        
        strParam = strIssueID & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & _
                   "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & intStatus & vbCrLf & "" 
        Try
            intErrNo = objNUTrx.mtdUpdSeedlingsIssue(strOpCdSeedlingsIssue_ADD, _
                                                     strOpCdSeedlingsIssue_UPD, _
                                                     strOpCdSeedlingsIssue_GET, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     strParam, _
                                                     objNUTrx.EnumOperation.Update, _
                                                     strIssueID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_DELETE_UNDELETE&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try
        
        If intErrNum <> objNUTrx.EnumErrorType.NoError Then
            lblActionResult.Text = "System failed to " & IIf(intAction = 1, "delete", "undelete") & " the Seedlings issue " & strIssueID & "."
            lblActionResult.Visible = True
        End If
        
        DisplaySeedlingsIssueHeader()
        DisplaySeedlingsIssueLines()
        SetObjectAccessibilityByStatus()
    End Sub
    
    Sub ibNew_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.redirect("NU_trx_SeedlingsIssue_details.aspx")
    End Sub
    
    Sub ibBack_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("NU_trx_SeedlingsIssue_list.aspx")
    End Sub
    
    Protected Function RoundNumber(ByVal d As Double, Optional ByVal decimals As Double = 0) As Double
        RoundNumber = Decimal.Divide(Int(Decimal.Add(Decimal.Multiply(d, 10.0 ^ decimals), 0.5)), 10.0 ^ decimals)
    End Function
    
    Function CheckDate(ByVal pv_strDate As String, ByRef pr_strDate As String, ByRef pr_strDateFormat As String) As Boolean
        Dim strDateFormatCode As String = Session("SS_DATEFMT")
        Dim strDateFormat As String
        
        pr_strDate = ""
        CheckDate = True
        If Not pv_strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormatCode, pv_strDate, strDateFormat, pr_strDate) = False Then
                pr_strDateFormat = strDateFormat
                pr_strDate = ""
                CheckDate = False
            End If
        End If
    End Function

    Function CheckLocBillParty () As Boolean
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strSearch As String
        Dim objBPLocDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strLocCode As String
        Dim objSeedlingIssueDS As New DataSet()

        Dim strSelect As String
        Dim strFrom As String
        Dim strWhere As String
        Dim strOrderBy As String
        
        strSelect = ""
        strFrom = ""
        strWhere = "SIL.IssueID = '" & lblIssueID.Text.Trim() & "'"
        strOrderBy = "SIL.IssueLNID"
        
        Try
            intErrNo = objNUTrx.mtdGetSeedlingsIssue(strOpCdSeedlingsIssueLine_GET, _
                                                     strSelect, _
                                                     strFrom, _
                                                     strWhere, _
                                                     strOrderBy, _
                                                     objSeedlingIssueDS)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_SEEDLINGS_ISSUE_GET_LINE&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try

        For intCnt = 0 To objSeedlingIssueDS.Tables(0).Rows.Count - 1
            strLocCode = TRIM(objSeedlingIssueDS.Tables(0).Rows(intCnt).Item("ChargeLocCode"))

            If Not (strLocCode = "" Or strLocCode = TRIM(strLocation)) Then
                strSearch = " AND BP.Status = '" & objGLSetup.EnumBillPartyStatus.Active & "'" & _
                            " AND BP.InterLocCode = '" & strLocCode & "'" 
                    
                Try
                    intErrNo = objGLSetup.mtdGetBPInterLoc(strOpCd_Get, strSearch, objBPLocDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_SETUP_BILLPARTY_GET&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
                End Try

                If objBPLocDs.Tables(0).Rows.Count <= 0 Then
                    lblLocCodeErr.Text = strLocCode
                    return False
                End If
            End If
        Next intCnt

        return True
    End Function

    Sub BindItemCodeList(Optional ByVal pv_strItemCode As String = "")
        Dim strOpCdItem_List_GET As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim dsItemCodeDropList As DataSet
        Dim intCnt As Integer
        Dim strParam As String
        Dim intSelectedIndex As Integer = 0

        strParam = objINSetup.EnumInventoryItemType.NurseryItem & "|" & _
                    objINSetup.EnumStockItemStatus.Active & "|" & _
                    lblIssueID.Text & "|" & _
                    "itm.ItemCode"
        Try
            intErrNo = objINSetup.mtdGetFilteredItemList(strOpCdItem_List_GET, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strParam, _
                                                       objINTrx.EnumInventoryTransactionType.StockIssue, _
                                                       dsItemCodeDropList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTOSERVTYPEDROPDOWNLIST&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try
        For intCnt = 0 To dsItemCodeDropList.Tables(0).Rows.Count - 1
            dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode")
            dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") = dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") & " ( " & _
                                                                dsItemCodeDropList.Tables(0).Rows(intCnt).Item("Description") & " ), " & _
                                                                "Rp. " & objGlobal.GetIDDecimalSeparator(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("AverageCost")) & ", " & _
                                                                objGlobal.GetIDDecimalSeparator_FreeDigit(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
                                                                Trim(dsItemCodeDropList.Tables(0).Rows(intCnt).Item("UOMCode")) 
            If dsItemCodeDropList.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(pv_strItemCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        If dsItemCodeDropList.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

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

        If Not dsItemCodeDropList Is Nothing Then
            dsItemCodeDropList = Nothing
        End If
    End Sub

    Function CheckNurseryItemQty(ByVal pv_strItemCode As String) As Boolean
        Dim strOpCdItem_List_GET As String = "NU_CLSTRX_STOCKITEM_GET"
        Dim dsItem As DataSet
        Dim bEnufQty As Boolean = False
        Dim strParam As String

        strParam = Trim(pv_strItemCode) & "|" & _ 
                   objINSetup.EnumInventoryItemType.NurseryItem & "|" & _
                   strLocation

        Try
            intErrNo = objNUTrx.mtdGetNurseryItem(strOpCdItem_List_GET, _
                                                    strParam, _
                                                    dsItem)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_NURSERYITEM&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
        End Try

        If dsItem.Tables(0).Rows.Count > 0 Then
            If CDbl(txtQty.Text) > dsItem.Tables(0).Rows(0).Item("QtyOnHand") Then
                bEnufQty = False
            Else
                bEnufQty = True
            End If
        End If

        If Not dsItem Is Nothing Then
            dsItem = Nothing
        End If

        Return bEnufQty
    End Function

    Function CheckAllNurseryItemQty(ByVal pv_strIssueID As String) As Boolean
        Dim intCnt As Integer
        Dim strParam As String
        Dim strItemCode As String
        Dim objResult As DataSet
        Dim dsItem As DataSet
        Dim bResult As Boolean = True

        Try
            intErrNo = objINTrx.mtdGetStockTransactDetails("NU_CLSTRX_SEEDLINGSISSUE_LINE_GET", _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            Trim(pv_strIssueID), _
                                                            objResult)
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_GET_SEEDLINGISSUE_TRX&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
            End If
        End Try

        If objResult.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objResult.Tables(0).Rows.Count - 1
                strItemCode = Trim(objResult.Tables(0).Rows(intCnt).Item("ItemCode"))
                strParam = Trim(strItemCode) & "|" & _ 
                        objINSetup.EnumInventoryItemType.NurseryItem & "|" & _
                        strLocation

                Try
                    intErrNo = objNUTrx.mtdGetNurseryItem("NU_CLSTRX_STOCKITEM_GET", _
                                                            strParam, _
                                                            dsItem)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_NURSERYITEM&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=/en/nu/trx/nu_trx_seedlingsissue_list.aspx")
                End Try

                If dsItem.Tables(0).Rows.Count > 0 Then
                    If CDbl(objResult.Tables(0).Rows(intCnt).Item("Qty")) > dsItem.Tables(0).Rows(0).Item("QtyOnHold") Then
                        lblActionResult.Text = "Insufficient quantity for " & strItemCode & "."
                        lblActionResult.Visible = True
                        bResult = False
                    End If
                End If
            Next
        End If

        return bResult
    End Function


End Class
