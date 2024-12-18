
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


Public Class IN_StockAdjust_Det : Inherits Page
      
    Dim strDateFMT As String

    Protected objINTrx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objINSetup As New agri.IN.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strOpCdStockAdjustmentHeader_ADD As String = "IN_CLSTRX_STOCKADJUST_DETAIL_ADD"
    Dim strOpCdStockAdjustmentHeader_GET As String = "IN_CLSTRX_STOCKADJUST_DETAIL_GET"
    Dim strOpCdStockAdjustmentHeader_UPD As String = "IN_CLSTRX_STOCKADJUST_DETAIL_UPD"
    Dim strOpCdStockAdjustmentLine_ADD As String = "IN_CLSTRX_STOCKADJUST_LINE_ADD"
    Dim strOpCdStockAdjustmentLine_DEL As String = "IN_CLSTRX_STOCKADJUST_LINE_DEL"
    Dim strOpCdStockAdjustmentLine_GET As String = "IN_CLSTRX_STOCKADJUST_LINE_GET"
    Dim strOpCdStockAdjustmentLine_UPD As String = "IN_CLSTRX_STOCKADJUST_LINE_UPD"
    Dim strOpCdStockAdjustmentList_GET As String = "IN_CLSTRX_STOCKADJUST_LIST_GET"
    Dim strOpCdItem_Det_GET As String = "IN_CLSSETUP_ITEM_DETAIL_GET"
    Dim strOpCdItem_Det_UPD As String = "IN_CLSSETUP_ITEM_DETAIL_UPD"
    Dim strOpCodeBlockChargingList_GET As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"  
    Dim strOpCodeBlockChargingList_GET2 As String = "GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_GET"  

    Dim objLangCapDs As New DataSet()

    Const APPEND_ITEM_CODE As Boolean = False
    Const APPEND_ACC_CODE As Boolean = False
    Const APPEND_BLK_CODE As Boolean = False
    Const APPEND_VEH_CODE As Boolean = False
    Const APPEND_VEH_EXP_CODE As Boolean = False
    Const ITEM_PART_SEPERATOR As String = " @ "

    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intINAR As Integer
    Dim intConfigsetting As Integer
    Dim strAction As String
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer
    Dim strAcceptFormat As String

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
        intINAR = Session("SS_INAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strDateFMT = Session("SS_DATEFMT")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockAdjustment), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            objLangCapDs = GetLanguageCaptionDS()

            lblActionResult.Visible = False 
            lblInventoryBin.Visible = False
            lblDate.Visible = False

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            ibAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibAdd).ToString())
            ibSave.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibSave).ToString())
            ibConfirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibConfirm).ToString())
            ibDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibDelete).ToString())
            ibNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibNew).ToString())
            ibBack.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibBack).ToString())
            ibCancel.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibCancel).ToString())

            strAction = Request.QueryString("Action")
            If Trim(strAction) = "SYN" Then
                Call SynchronizeStockAdjustment(Request.QueryString("id").Trim())
                Exit Sub
            End If

            If Not Page.IsPostBack Then
               
                txtAccCode.Attributes.Add("readonly", "readonly")
                txtAccName.Attributes.Add("readonly", "readonly")

                txtCost.Attributes.Add("readonly", "readonly")


                txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                lblStockAdjID.Text = Trim(Request.QueryString("Id"))
                GetLangCap()
                BindAdjustmentTypeDropDownList()
                BindTransactionTypeDropDownList()
                BindItemCodeDropDownList()
                'BindAccCodeDropDownList("")
                BindChargeLevelDropDownList()
                BindPreBlkCodeDropDownList("", "")
                BindBlkCodeDropDownList("", "")
                BindVehCodeDropDownList("", "")
                BindVehExpCodeDropDownList("", True)
                BindInventoryBinLevel("")
                BindStorage("")

                If lblStockAdjID.Text.Trim() = "" Then
                    intDefaultAdjType = IIf(Trim(Request.QueryString("AdjType")) = "", objINTrx.EnumAdjustmentType.Quantity, CInt(Val(Trim(Request.QueryString("AdjType")))))
                    intDefaultTransType = IIf(Trim(Request.QueryString("TransType")) = "", objINTrx.EnumTransactionType.NewValue, CInt(Val(Trim(Request.QueryString("TransType")))))
                    For intCnt = 0 To ddlAdjType.Items.Count - 1
                        If ddlAdjType.Items(intCnt).Value = intDefaultAdjType Then
                            ddlAdjType.SelectedIndex = intCnt
                            Exit For
                        End If
                    Next
                    For intCnt = 0 To ddlTransType.Items.Count - 1
                        If ddlTransType.Items(intCnt).Value = intDefaultTransType Then
                            ddlTransType.SelectedIndex = intCnt
                            Exit For
                        End If
                    Next
                Else
                    DisplayStockAdjustmentHeader()
                    DisplayStockAdjustmentLines()
                End If
                LoadActionDescription()
                SetObjectAccessibilityByStatus()
                SetObjectAccessibilityByAdjustmentType()
            End If
        End If
        ToggleChargeLevel()

    End Sub

    Sub GetLangCap()
        Dim strAccCode As String
        Dim strPreBlkCode As String
        Dim strBlkCode As String
        Dim strVehCode As String
        Dim strVehExpCode As String

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlkCode = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                strBlkCode = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKADJ_DET_GET_LANGCAP_COSTLEVEL&errmesg=&redirect=IN/trx/IN_trx_StockAdj_list.aspx")
        End Try

        strPreBlkCode = GetCaption(objLangCap.EnumLangCap.Block)
        strAccCode = GetCaption(objLangCap.EnumLangCap.Account)
        strVehCode = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strVehExpCode = GetCaption(objLangCap.EnumLangCap.VehExpense)

        lblAccCodeTag.Text = strAccCode & lblCode.Text
        lblPreBlkCodeTag.Text = strPreBlkCode & lblCode.Text
        lblBlkCodeTag.Text = strBlkCode & lblCode.Text
        lblVehCodeTag.Text = strVehCode & lblCode.Text
        lblVehExpCodeTag.Text = strVehExpCode & lblCode.Text

        lblAccCodeErr.Text = lblPleaseSelect.Text & strAccCode & lblCode.Text & "."
        lblPreBlkCodeErr.Text = lblPleaseSelect.Text & strPreBlkCode & lblCode.Text & "."
        lblBlkCodeErr.Text = lblPleaseSelect.Text & strBlkCode & lblCode.Text & "."
        lblVehCodeErr.Text = lblPleaseSelect.Text & strVehCode & lblCode.Text & "."
        lblVehExpCodeErr.Text = lblPleaseSelect.Text & strVehExpCode & lblCode.Text & "."

        dgLines.Columns(1).HeaderText = strAccCode & lblCode.Text
        dgLines.Columns(2).HeaderText = strBlkCode & lblCode.Text
        dgLines.Columns(3).HeaderText = strVehCode & lblCode.Text
        dgLines.Columns(4).HeaderText = strVehExpCode & lblCode.Text
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

    Sub SetObjectAccessibilityByStatus()
        ddlAdjType.Enabled = False
        ddlTransType.Enabled = False
        RadComboBoxProduct.Enabled = False
        txtAccCode.Enabled = False
        ddlPreBlkCode.Enabled = False
        ddlBlkCode.Enabled = False
        ddlVehCode.Enabled = False
        ddlVehExpCode.Enabled = False

        txtRemark.Enabled = False
        txtAdjDocRef.Enabled = False
        txtQty.Enabled = False
        txtAverageCost.Enabled = False
        txtTotalCost.Enabled = False
        'Find.Visible = False
        'FindAcc.Visible = False

        revQty.Enabled = False
        revAverageCost.Enabled = False
        revTotalCost.Enabled = False

        ibSave.Visible = False
        ibConfirm.Visible = False
        ibDelete.Visible = False
        ibNew.Visible = False
        ibCancel.Visible = False

        tblAdd.Visible = False
        tdDummy.Visible = False
        dgLines.Columns(15).Visible = False

        Select Case lblHidStatus.Text.Trim()
            Case Trim(CStr(objINTrx.EnumStockAdjustStatus.Deleted))
                ibNew.Visible = True
                ibDelete.Visible = True
                ibDelete.ImageUrl = "../../images/butt_undelete.gif"
                ibDelete.AlternateText = "Undelete"
                ibDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Trim(CStr(objINTrx.EnumStockAdjustStatus.Confirmed))
                ibNew.Visible = True
                ibCancel.Visible = True
            Case Trim(CStr(objINTrx.EnumStockAdjustStatus.Cancelled))
                ibNew.Visible = True
            Case Trim(CStr(objINTrx.EnumStockAdjustStatus.Closed))
                ibNew.Visible = True
            Case Else
                tblAdd.Visible = True
                RadComboBoxProduct.Enabled = True
                txtAccCode.Enabled = True
                ddlPreBlkCode.Enabled = True
                ddlBlkCode.Enabled = True
                ddlVehCode.Enabled = True
                ddlVehExpCode.Enabled = True
                txtAdjDocRef.Enabled = True
                txtQty.Enabled = True
                txtAverageCost.Enabled = True
                txtTotalCost.Enabled = True
                'Find.Visible = True
                'FindAcc.Visible = True
                ibSave.Visible = True
                txtRemark.Enabled = True
                If lblStockAdjID.Text = "" Then
                    ddlAdjType.Enabled = True
                    ddlTransType.Enabled = True
                Else
                    ibNew.Visible = True
                    ibDelete.Visible = True
                    ibDelete.ImageUrl = "../../images/butt_delete.gif"
                    ibDelete.AlternateText = "Delete"
                    ibDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    If dgLines.Items.Count = 0 Then
                        ddlAdjType.Enabled = True
                        ddlTransType.Enabled = True
                    Else
                        ibConfirm.Visible = True
                    End If
                    tdDummy.Visible = True
                    dgLines.Columns(15).Visible = True
                End If
        End Select
    End Sub

    Sub SetObjectAccessibilityByAdjustmentType()
        Dim blnQty As Boolean
        Dim blnAverageCost As Boolean
        Dim blnTotalCost As Boolean
        Dim blnCharging As Boolean

        dgLines.Columns(6).Visible = False  
        dgLines.Columns(7).Visible = False  
        dgLines.Columns(8).Visible = False  
        dgLines.Columns(9).Visible = False  
        dgLines.Columns(10).Visible = False 
        dgLines.Columns(11).Visible = False 
        dgLines.Columns(12).Visible = False 
        dgLines.Columns(13).Visible = False 
        dgLines.Columns(14).Visible = False 

        Select Case ddlAdjType.SelectedItem.Value
            Case Trim(CStr(objINTrx.EnumAdjustmentType.Quantity))
                blnCharging = False
                blnQty = True
                blnAverageCost = False
                blnTotalCost = False
                dgLines.Columns(6).Visible = True
                If ddlTransType.SelectedItem.Value = objINTrx.EnumTransactionType.NewValue Then
                    dgLines.Columns(9).Visible = True
                Else
                    dgLines.Columns(12).Visible = True
                End If

            Case Trim(CStr(objINTrx.EnumAdjustmentType.AverageCost))
                blnCharging = True
                blnQty = False
                blnAverageCost = True
                blnTotalCost = False
                dgLines.Columns(7).Visible = True
                If ddlTransType.SelectedItem.Value = objINTrx.EnumTransactionType.NewValue Then
                    dgLines.Columns(10).Visible = True
                Else
                    dgLines.Columns(13).Visible = True
                End If

            Case Trim(CStr(objINTrx.EnumAdjustmentType.TotalCost))
                blnCharging = True
                blnQty = False
                blnAverageCost = False
                blnTotalCost = True
                dgLines.Columns(8).Visible = True
                If ddlTransType.SelectedItem.Value = objINTrx.EnumTransactionType.NewValue Then
                    dgLines.Columns(11).Visible = True
                Else
                    dgLines.Columns(14).Visible = True
                End If

            Case Trim(CStr(objINTrx.EnumAdjustmentType.QuantityAtAverageCost))
                blnCharging = True
                blnQty = True
                blnAverageCost = True
                blnTotalCost = False
                dgLines.Columns(6).Visible = True
                dgLines.Columns(7).Visible = True
                If ddlTransType.SelectedItem.Value = objINTrx.EnumTransactionType.NewValue Then
                    dgLines.Columns(9).Visible = True
                    dgLines.Columns(10).Visible = True
                Else
                    dgLines.Columns(12).Visible = True
                    dgLines.Columns(13).Visible = True
                End If

            Case Trim(CStr(objINTrx.EnumAdjustmentType.QuantityAtTotalCost))
                blnCharging = True
                blnQty = True
                blnAverageCost = False
                blnTotalCost = True
                dgLines.Columns(6).Visible = True
                dgLines.Columns(8).Visible = True
                If ddlTransType.SelectedItem.Value = objINTrx.EnumTransactionType.NewValue Then
                    dgLines.Columns(9).Visible = True
                    dgLines.Columns(11).Visible = True
                Else
                    dgLines.Columns(12).Visible = True
                    dgLines.Columns(14).Visible = True
                End If

        End Select

        trAccCode.Visible = blnCharging
        If Session("SS_BLOCK_CHARGE_VISIBLE") = True And blnCharging = True Then
            trChargeLevel.Visible = True
        Else
            trChargeLevel.Visible = False
        End If
        ToggleChargeLevel()
        trVehCode.Visible = blnCharging
        trVehExpCode.Visible = blnCharging
        dgLines.Columns(1).Visible = blnCharging    
        dgLines.Columns(2).Visible = blnCharging    
        dgLines.Columns(3).Visible = blnCharging    
        dgLines.Columns(4).Visible = blnCharging    

        lblQuantityTag.Visible = blnQty
        txtQty.Visible = blnQty
        revQty.Enabled = blnQty

        lblAverageCostTag.Visible = blnAverageCost
        txtAverageCost.Visible = blnAverageCost
        revAverageCost.Enabled = blnAverageCost

        lblTotalCostTag.Visible = blnTotalCost
        txtTotalCost.Visible = blnTotalCost
        revTotalCost.Enabled = blnTotalCost

        If blnTotalCost = True Then
            lblActionDesc1.Visible = False
            lblActionDesc2.Visible = False
            lblActionDesc3.Visible = True
            tdAverageCost.Visible = True
            tdDiffAverageCost.Visible = True
            tdTotalCost.Visible = True
            tdActionDesc.Visible = True
            tdAverageCost.RowSpan = 1
            tdDiffAverageCost.RowSpan = 1
            tdTotalCost.RowSpan = 1
            tdAverageCost.VAlign = ""
            tdDiffAverageCost.VAlign = ""

        ElseIf blnAverageCost = True Then
            lblActionDesc1.Visible = False
            lblActionDesc2.Visible = True
            lblActionDesc3.Visible = False
            tdAverageCost.Visible = True
            tdDiffAverageCost.Visible = True
            tdTotalCost.Visible = False
            tdActionDesc.Visible = False
            tdAverageCost.RowSpan = 1
            tdDiffAverageCost.RowSpan = 3
            tdTotalCost.RowSpan = 1
            tdAverageCost.VAlign = ""
            tdDiffAverageCost.VAlign = "Top"

        ElseIf blnQty = True Then
            lblActionDesc1.Visible = True
            lblActionDesc2.Visible = False
            lblActionDesc3.Visible = False
            tdAverageCost.Visible = True
            tdDiffAverageCost.Visible = False
            tdTotalCost.Visible = False
            tdActionDesc.Visible = False
            tdAverageCost.RowSpan = 4
            tdDiffAverageCost.RowSpan = 1
            tdTotalCost.RowSpan = 1
            tdAverageCost.VAlign = "Top"
            tdDiffAverageCost.VAlign = ""
        End If
    End Sub

    Sub LoadActionDescription()
        Dim strActionDesc As String
        Select Case ddlTransType.SelectedItem.Value
            Case Trim(CStr(objINTrx.EnumTransactionType.NewValue))
                Select Case ddlAdjType.SelectedItem.Value
                    Case Trim(CStr(objINTrx.EnumAdjustmentType.Quantity))
                        strActionDesc = "This Quantity will be your new quantity after confirm."
                    Case Trim(CStr(objINTrx.EnumAdjustmentType.AverageCost))
                        strActionDesc = "This Average Cost will be your new Average Cost after confirm."
                    Case Trim(CStr(objINTrx.EnumAdjustmentType.TotalCost))
                        strActionDesc = "This Total Cost will be your new stock Total Cost after confirm."
                    Case Trim(CStr(objINTrx.EnumAdjustmentType.QuantityAtAverageCost))
                        strActionDesc = "This Quantity, Average Cost will be your new stock balance and Average Cost after confirm."
                    Case Trim(CStr(objINTrx.EnumAdjustmentType.QuantityAtTotalCost))
                        strActionDesc = "This Quantity and Total Cost will be your new stock Quantity and Total Cost after confirm."
                    Case Else
                        strActionDesc = ""
                End Select
            Case Trim(CStr(objINTrx.EnumTransactionType.Difference))
                Select Case ddlAdjType.SelectedItem.Value
                    Case Trim(CStr(objINTrx.EnumAdjustmentType.Quantity))
                        strActionDesc = "This Quantity will be added or subtracted from your original quantity after confirm."
                    Case Trim(CStr(objINTrx.EnumAdjustmentType.AverageCost))
                        strActionDesc = "This Average Cost will be added or subtracted from your original Average Cost after confirm."
                    Case Trim(CStr(objINTrx.EnumAdjustmentType.TotalCost))
                        strActionDesc = "This Total Cost will be added or subtracted from your original Total Cost after confirm."
                    Case Trim(CStr(objINTrx.EnumAdjustmentType.QuantityAtAverageCost))
                        strActionDesc = "This Quantity and Average Cost will be added or subtracted from your respective original Quantity and original Average Cost after confirm."
                    Case Trim(CStr(objINTrx.EnumAdjustmentType.QuantityAtTotalCost))
                        strActionDesc = "This Quantity and Total Cost will be added or subtracted from your original stock Quantity and Total Cost after confirm."
                    Case Else
                        strActionDesc = ""
                End Select
            Case Else
                strActionDesc = ""
        End Select

        lblActionDesc1.Text = strActionDesc
        lblActionDesc2.Text = strActionDesc
        lblActionDesc3.Text = strActionDesc
    End Sub

    Sub ResetPage(ByVal blnHeader As Boolean, ByVal blnAdd As Boolean, ByVal blnLines As Boolean)
        If blnHeader = True Then
            DisplayStockAdjustmentHeader()
        End If

        If blnLines = True Then
            DisplayStockAdjustmentLines()
        End If

        SetObjectAccessibilityByStatus()
        SetObjectAccessibilityByAdjustmentType()
        If blnAdd = True Then
            'BindItemCodeDropDownList("")
            BindAccCodeDropDownList("")
            BindPreBlkCodeDropDownList("", "")
            BindBlkCodeDropDownList("", "")
            BindVehCodeDropDownList("", "")
            BindVehExpCodeDropDownList("", True)
            txtAdjDocRef.Text = ""
            txtQty.Text = ""
            txtAverageCost.Text = ""
            txtTotalCost.Text = ""
        End If
    End Sub

    Sub ToggleChargeLevel()
        If ddlChargeLevel.SelectedIndex = 0 Then
            trBlkCode.Visible = False
            trPreBlkCode.Visible = IIf(ddlAdjType.SelectedItem.Value = Trim(CStr(objINTrx.EnumAdjustmentType.Quantity)), False, True)
            hidBlockCharge.Value = "yes"
        Else
            trBlkCode.Visible = IIf(ddlAdjType.SelectedItem.Value = Trim(CStr(objINTrx.EnumAdjustmentType.Quantity)), False, True)
            trPreBlkCode.Visible = False
            hidBlockCharge.Value = ""
        End If
    End Sub

    Sub BindAdjustmentTypeDropDownList()
        ddlAdjType.Items.Clear()
        ddlAdjType.Items.Add(New ListItem(objINTrx.mtdGetAdjustmentType(objINTrx.EnumAdjustmentType.Quantity), objINTrx.EnumAdjustmentType.Quantity))
        ddlAdjType.Items.Add(New ListItem(objINTrx.mtdGetAdjustmentType(objINTrx.EnumAdjustmentType.TotalCost), objINTrx.EnumAdjustmentType.TotalCost))
        ddlAdjType.SelectedIndex = 0
    End Sub

    Sub BindTransactionTypeDropDownList()
        ddlTransType.Items.Clear()
        ddlTransType.Items.Add(New ListItem(objINTrx.mtdGetTransactionType(objINTrx.EnumTransactionType.NewValue), objINTrx.EnumTransactionType.NewValue))
        ddlTransType.Items.Add(New ListItem(objINTrx.mtdGetTransactionType(objINTrx.EnumTransactionType.Difference), objINTrx.EnumTransactionType.Difference))
        ddlTransType.SelectedIndex = 0
    End Sub

    Sub BindItemCodeDropDownList()
         Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer
 
        sSQLKriteria="SELECT  ItemCode,RTRIM(Description) AS ItemName From IN_Item Where Status='1' And LocCode='" & strlocation & "'"
        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
        End Try
 
        dr = objdsST.Tables(0).NewRow()
        dr("ItemCode") = "" 
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        RadComboBoxProduct.DataSource = objdsST.Tables(0)
        RadComboBoxProduct.DataValueField = "ItemCode"
        RadComboBoxProduct.DataTextField = "ItemName"
        RadComboBoxProduct.DataBind()
        RadComboBoxProduct.SelectedIndex = intSelectedIndex
 
    End Sub

    Sub BindStorage(ByVal pv_strcode As String)
        Dim strParamName As String
        Dim strParamValue As String
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
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

    Sub BindAccCodeDropDownList(ByVal pv_strAccCode As String)
        
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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKADJ_DET_GET_LANGCAP_COSTLEVEL&errmesg=&redirect=IN/trx/IN_trx_StockAdj_list.aspx")
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
            strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     dsList)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=&redirect=")
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
                strParam = pv_strAccCode.Trim() & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode.Trim() & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     dsList)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=&redirect=")
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

        strParam = "|AccCode = '" & pv_strAccCode.Trim() & "' AND LocCode = '" & Session("SS_LOCATION") & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'"
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   dsList)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=&redirect=GL/Trx/GL_Trx_Journal_List.aspx")
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

        strParam = "Order By VehExpenseCode ASC|And Veh.Status = '" & objGLSetup.EnumVehicleExpenseStatus.Active & "'"
        Try
            If pv_IsBlankList Then
                strParam += " And Veh.VehExpensecode = ''"
            End If
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.VehicleExpense, _
                                                   dsList)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=JR_TRX_ADTRX_VEHEXPENSE_GET&errmesg=&redirect=")
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

    Sub ddlAdjType_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblQtyErr.Text = ""
        lblAverageCostErr.Text = ""
        lblTotalCostErr.Text = ""
        LoadActionDescription()
        SetObjectAccessibilityByAdjustmentType()
        setFocus(ddlTransType)
    End Sub

    Sub ddlTransType_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadActionDescription()
        SetObjectAccessibilityByAdjustmentType()
        setFocus(txtRemark)
    End Sub

    Sub ddlItemCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'DisplayItemProperties()
        'setFocus(ddlAccCode)
    End Sub

    Sub GetItemBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        DisplayItemProperties()        
    End Sub

    Sub BtnGetCOA_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        GetAccCodeBlock()
    End Sub

    Sub GetAccCodeBlock()
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strPreBlk As String = ddlPreBlkCode.SelectedItem.Value.Trim()

        Dim strAcc As String = Request.Form("txtAccCode")
        Dim strBlk As String = Request.Form("ddlBlkCode")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")

        GetAccountProperties(strAcc, intAccType, intAccPurpose, intNurseryInd)

        If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLSetup.EnumAccountPurpose.NonVehicle
                    BindPreBlkCodeDropDownList(strAcc, strPreBlk)
                    BindBlkCodeDropDownList(strAcc, strBlk)
                    BindVehCodeDropDownList("", "")
                    BindVehExpCodeDropDownList("", True)
                Case objGLSetup.EnumAccountPurpose.VehicleDistribution
                    BindPreBlkCodeDropDownList("", "")
                    BindBlkCodeDropDownList("", "")
                    BindVehCodeDropDownList(strAcc, strVeh)
                    BindVehExpCodeDropDownList(strVehExp, False)
                Case Else
                    BindPreBlkCodeDropDownList(strAcc, strPreBlk)
                    BindBlkCodeDropDownList(strAcc, strBlk)
                    BindVehCodeDropDownList("%", strVeh)
                    BindVehExpCodeDropDownList(strVehExp, False)
            End Select
        ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
            BindPreBlkCodeDropDownList(strAcc, strPreBlk)
            BindBlkCodeDropDownList(strAcc, strBlk)
            BindVehCodeDropDownList("", "")
            BindVehExpCodeDropDownList("", True)
        Else
            BindPreBlkCodeDropDownList("", "")
            BindBlkCodeDropDownList("", "")
            BindVehCodeDropDownList("", "")
            BindVehExpCodeDropDownList("", True)
        End If
        setFocus(ddlChargeLevel)
    End Sub

    Sub ddlAccCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strPreBlk As String = ddlPreBlkCode.SelectedItem.Value.Trim()

        Dim strAcc As String = Request.Form("txtAccCode")
        Dim strBlk As String = Request.Form("ddlBlkCode")
        Dim strVeh As String = Request.Form("ddlVehCode")
        Dim strVehExp As String = Request.Form("ddlVehExpCode")

        GetAccountProperties(strAcc, intAccType, intAccPurpose, intNurseryInd)

        If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLSetup.EnumAccountPurpose.NonVehicle
                    BindPreBlkCodeDropDownList(strAcc, strPreBlk)
                    BindBlkCodeDropDownList(strAcc, strBlk)
                    BindVehCodeDropDownList("", "")
                    BindVehExpCodeDropDownList("", True)
                Case objGLSetup.EnumAccountPurpose.VehicleDistribution
                    BindPreBlkCodeDropDownList("", "")
                    BindBlkCodeDropDownList("", "")
                    BindVehCodeDropDownList(strAcc, strVeh)
                    BindVehExpCodeDropDownList(strVehExp, False)
                Case Else
                    BindPreBlkCodeDropDownList(strAcc, strPreBlk)
                    BindBlkCodeDropDownList(strAcc, strBlk)
                    BindVehCodeDropDownList("%", strVeh)
                    BindVehExpCodeDropDownList(strVehExp, False)
            End Select
        ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
            BindPreBlkCodeDropDownList(strAcc, strPreBlk)
            BindBlkCodeDropDownList(strAcc, strBlk)
            BindVehCodeDropDownList("", "")
            BindVehExpCodeDropDownList("", True)
        Else
            BindPreBlkCodeDropDownList("", "")
            BindBlkCodeDropDownList("", "")
            BindVehCodeDropDownList("", "")
            BindVehExpCodeDropDownList("", True)
        End If
        setFocus(ddlChargeLevel)
    End Sub

    Sub ddlChargeLevel_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ToggleChargeLevel()
        setFocus(ddlPreBlkCode)
    End Sub

    Protected Function GetStockAdjustmentHeaderDS() As DataSet
        Dim dsHeader As DataSet
        Dim strWhere As String
        Dim strOrderBy As String

        strWhere = " WHERE HD.StockAdjID = '" & lblStockAdjID.Text.Trim() & "'"
        strOrderBy = ""
        Try
            intErrNo = objINTrx.mtdGetStockAdjustment(strOpCdStockAdjustmentHeader_GET, _
                                                      strWhere, _
                                                      strOrderBy, _
                                                      dsHeader)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFERLINE&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
        End Try

        Return dsHeader
        If Not dsHeader Is Nothing Then
            dsHeader = Nothing
        End If
    End Function

    Protected Function GetStockAdjustmentLinesDS() As DataSet
        Dim dsLines As DataSet
        Dim strWhere As String
        Dim strOrderBy As String
        Dim strStorageCode As String

        strWhere = " WHERE LN.StockAdjID = '" & lblStockAdjID.Text.Trim() & "'"
        strOrderBy = " ORDER BY LN.StockAdjLNID"

        Try
            intErrNo = objINTrx.mtdGetStockAdjustment(strOpCdStockAdjustmentLine_GET, _
                                                      strWhere, _
                                                      strOrderBy, _
                                                      dsLines)
        Catch Exp As Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ADJUSTITEM&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
            End If
        End Try

        lstStorage.Enabled = True
        For intCnt = 0 To dsLines.Tables(0).Rows.Count - 1
            lstStorage.Enabled = False
            strStorageCode = Trim(dsLines.Tables(0).Rows(intCnt).Item("StorageCode"))
        Next

        BindStorage(strStorageCode)
        If dsLines.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
        Else
            TrLink.Visible = False
        End If

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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKADJ_DET_GET_LANGCAP&errmesg=&redirect=IN/trx/IN_trx_StockAdj_list.aspx")
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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCOUNT_DETAILS&errmesg=&redirect=")
        End Try

        If objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
        End If
    End Sub

    Sub DisplayStockAdjustmentHeader()
        Dim intCnt As Integer
        Dim dsHeader As DataSet

        dsHeader = GetStockAdjustmentHeaderDS()
        lblHidStatus.Text = dsHeader.Tables(0).Rows(0).Item("Status").Trim()
        lblStockAdjID.Text = dsHeader.Tables(0).Rows(0).Item("StockAdjID").Trim()
        lblAccPeriod.Text = dsHeader.Tables(0).Rows(0).Item("AccMonth").Trim() & "/" & dsHeader.Tables(0).Rows(0).Item("AccYear").Trim()
        lblStatus.Text = objINTrx.mtdGetStockAdjustStatus(dsHeader.Tables(0).Rows(0).Item("Status").Trim())
        lblCreateDate.Text = objGlobal.GetLongDate(dsHeader.Tables(0).Rows(0).Item("CreateDate"))
        lblUpdateDate.Text = objGlobal.GetLongDate(dsHeader.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdateID.Text = dsHeader.Tables(0).Rows(0).Item("UserName").Trim()
        txtDate.Text = Date_Validation(dsHeader.Tables(0).Rows(0).Item("StockAdjDate"), True)

        For intCnt = 0 To ddlAdjType.Items.Count - 1
            If ddlAdjType.Items(intCnt).Value = dsHeader.Tables(0).Rows(0).Item("AdjType").Trim() Then
                ddlAdjType.SelectedIndex = intCnt
                Exit For
            End If
        Next
        If ddlTransType.Items(0).Value = dsHeader.Tables(0).Rows(0).Item("TransType").Trim() Then
            ddlTransType.SelectedIndex = 0
        Else
            ddlTransType.SelectedIndex = 1
        End If
        txtRemark.Text = dsHeader.Tables(0).Rows(0).Item("Remark").Trim()
        BindInventoryBinLevel(Trim(dsHeader.Tables(0).Rows(0).Item("Bin")))
    End Sub

    Sub DisplayStockAdjustmentLines()
        Dim decN_Quantity As Decimal
        Dim decN_AverageCost As Decimal
        Dim decN_TotalCost As Decimal
        Dim decD_Quantity As Decimal
        Dim decD_AverageCost As Decimal
        Dim decD_TotalCost As Decimal
        Dim lbl As Label
        Dim dgiLine As DataGridItem

        dgLines.DataSource = GetStockAdjustmentLinesDS()
        dgLines.DataBind()

        For Each dgiLine In dgLines.Items

            If dgiLine.ItemType = ListItemType.Item Or dgiLine.ItemType = ListItemType.AlternatingItem Then
                lbl = dgiLine.FindControl("lblN_Quantity")
                decN_Quantity = Decimal.Add(decN_Quantity, lbl.Text.Trim())
                lbl = dgiLine.FindControl("lblN_AverageCost")
                decN_AverageCost = Decimal.Add(decN_AverageCost, lbl.Text.Trim())
                lbl = dgiLine.FindControl("lblN_TotalCost")
                decN_TotalCost = Decimal.Add(decN_TotalCost, lbl.Text.Trim())
                lbl = dgiLine.FindControl("lblD_Quantity")
                decD_Quantity = Decimal.Add(decD_Quantity, lbl.Text.Trim())
                lbl = dgiLine.FindControl("lblD_AverageCost")
                decD_AverageCost = Decimal.Add(decD_AverageCost, lbl.Text.Trim())
                lbl = dgiLine.FindControl("lblD_TotalCost")
                decD_TotalCost = Decimal.Add(decD_TotalCost, lbl.Text.Trim())
            End If
        Next

        lblTotal1.Visible = False
        lblTotal2.Visible = False
        Select Case ddlAdjType.SelectedItem.Value
            Case objINTrx.EnumAdjustmentType.Quantity
                lblTotal2.Visible = True
                If ddlTransType.SelectedItem.Value = objINTrx.EnumTransactionType.NewValue Then
                    lblTotal2.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decN_Quantity, 5)
                Else
                    lblTotal2.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decD_Quantity, 5)
                End If
            Case objINTrx.EnumAdjustmentType.AverageCost
                lblTotal2.Visible = True
                If ddlTransType.SelectedItem.Value = objINTrx.EnumTransactionType.NewValue Then
                    lblTotal2.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decN_AverageCost, 2)
                Else
                    lblTotal2.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decD_AverageCost, 2)
                End If
            Case objINTrx.EnumAdjustmentType.TotalCost
                lblTotal2.Visible = True
                If ddlTransType.SelectedItem.Value = objINTrx.EnumTransactionType.NewValue Then
                    lblTotal2.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decN_TotalCost, 2)
                Else
                    lblTotal2.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decD_TotalCost, 2)
                End If
            Case objINTrx.EnumAdjustmentType.QuantityAtAverageCost
                lblTotal1.Visible = True
                lblTotal2.Visible = True
                If ddlTransType.SelectedItem.Value = objINTrx.EnumTransactionType.NewValue Then
                    lblTotal1.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decN_Quantity, 5)
                    lblTotal2.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decN_AverageCost, 2)
                Else
                    lblTotal1.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decD_Quantity, 5)
                    lblTotal2.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decD_AverageCost, 2)
                End If
            Case objINTrx.EnumAdjustmentType.QuantityAtTotalCost
                lblTotal1.Visible = True
                lblTotal2.Visible = True
                If ddlTransType.SelectedItem.Value = objINTrx.EnumTransactionType.NewValue Then
                    lblTotal1.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decN_Quantity, 5)
                    lblTotal2.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decN_TotalCost, 2)
                Else
                    lblTotal1.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decD_Quantity, 5)
                    lblTotal2.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decD_TotalCost, 2)
                End If
        End Select

    End Sub

    Sub DisplayItemProperties()
        Dim dsItem As DataSet
        Dim strItemCode As String
        Dim strWhere As String
        Dim strOrderBy As String
        Dim decQuantity As Decimal
        Dim decQtyOnHand As Decimal
        Dim decQtyOnHold As Decimal
        Dim decAverageCost As Decimal
        Dim decDiffAverageCost As Decimal
        Dim decTotalCost As Decimal

        strItemCode = RadComboBoxProduct.SelectedValue ''ddlItemCode.SelectedItem.Value.Trim()
        If strItemCode = "" Then
            lblOriginalQty.Text = ""
            lblOriginalAverageCost.Text = ""
            lblOriginalDiffAverageCost.Text = ""
            lblOriginalTotalCost.Text = ""
        Else
            If InStr(strItemCode, ITEM_PART_SEPERATOR) <> 0 Then
                strItemCode = Trim(Mid(strItemCode, 1, InStr(strItemCode, ITEM_PART_SEPERATOR) - 1))
            End If
            strWhere = " WHERE ItemCode = '" & strItemCode & "' AND LocCode = '" & strLocation & "'"
            strOrderBy = ""

            Try
                intErrNo = objINTrx.mtdGetStockAdjustment(strOpCdItem_Det_GET, _
                                                        strWhere, _
                                                        strOrderBy, _
                                                        dsItem)
            Catch Exp As Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFERLINE&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
            End Try

            If dsItem.Tables(0).Rows.Count = 1 Then
                decQtyOnHand = RoundNumber(dsItem.Tables(0).Rows(0).Item("QtyOnHand"), 5)
                decQtyOnHold = RoundNumber(dsItem.Tables(0).Rows(0).Item("QtyOnHold"), 5)
                decQuantity = RoundNumber(Decimal.Add(decQtyOnHand, decQtyOnHold), 5)
                decAverageCost = RoundNumber(dsItem.Tables(0).Rows(0).Item("AverageCost"), 5)
                decDiffAverageCost = RoundNumber(dsItem.Tables(0).Rows(0).Item("DiffAverageCost"), 5)
                decTotalCost = objGlobal.DBCurrencyFormat(Decimal.Add(objGlobal.DBCurrencyFormat(Decimal.Multiply(decQuantity, decAverageCost)), decDiffAverageCost))
                lblOriginalQty.Text = decQuantity
                lblOriginalQtyOnHand.Text = decQtyOnHand
                lblOriginalQtyOnHold.Text = decQtyOnHold
                lblOriginalAverageCost.Text = decAverageCost
                lblOriginalDiffAverageCost.Text = decDiffAverageCost
                lblOriginalTotalCost.Text = decTotalCost

                lblOriginalQtyDisplay.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(decQuantity, 5)
                lblOriginalAverageCostDisplay.Text = objGlobal.GetIDDecimalSeparator(decAverageCost)
                lblOriginalDiffAverageCostDisplay.Text = objGlobal.GetIDDecimalSeparator(decDiffAverageCost)
                lblOriginalTotalCostDisplay.Text = objGlobal.GetIDDecimalSeparator(decTotalCost)

            Else
                lblOriginalQty.Text = ""
                lblOriginalAverageCost.Text = ""
                lblOriginalDiffAverageCost.Text = ""
                lblOriginalTotalCost.Text = ""
            End If
        End If

        If Not dsItem Is Nothing Then
            dsItem = Nothing
        End If
    End Sub

    Sub dgLines_OnItemDataBound(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim btn As LinkButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            btn = e.Item.FindControl("Delete")
            If lblHidStatus.Text.Trim() = Trim(CStr(objINTrx.EnumStockAdjustStatus.Active)) Then
                btn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                btn.Visible = True
            Else
                btn.Visible = False
            End If
        End If
    End Sub

    Sub dgLines_OnDeleteCommand(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lblDG As Label
        Dim strStockAdjID As String
        Dim strStockAdjLNID As String
        Dim intErrNum As Integer
        Dim strErrMsg As String
        Dim strParam As String

        If lblHidStatus.Text.Trim() = Trim(CStr(objINTrx.EnumStockAdjustStatus.Active)) Then
            strStockAdjID = lblStockAdjID.Text.Trim()
            lblDG = E.Item.FindControl("lblStockAdjLNID")
            strStockAdjLNID = lblDG.Text.Trim()
            strParam = strStockAdjID & vbCrLf & strStockAdjLNID
            Try
                intErrNo = objINTrx.mtdStockAdjustmentLine_Upd(strOpCdStockAdjustmentLine_ADD, _
                                                               strOpCdStockAdjustmentLine_DEL, _
                                                               strOpCdStockAdjustmentLine_GET, _
                                                               strOpCdStockAdjustmentLine_UPD, _
                                                               strOpCdStockAdjustmentHeader_UPD, _
                                                               strOpCdItem_Det_GET, _
                                                               strOpCodeBlockChargingList_GET, _
                                                               "", _
                                                               False, _
                                                               strCompany, _
                                                               strLocation, _
                                                               strUserId, _
                                                               strAccMonth, _
                                                               strAccYear, _
                                                               strParam, _
                                                               objINTrx.EnumTransactionAction.Delete, _
                                                               intErrNum, _
                                                               strErrMsg, _
                                                               strStockAdjLNID)
            Catch Exp As Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_STOCKTRANSFERLINE&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
                End If
            End Try

            If intErrNum = objINTrx.EnumTransactionError.NoError Then    
                ResetPage(True, True, True)
            Else
                lblActionResult.Text = "System failed to delete stock adjustment line."
                lblActionResult.Visible = True
            End If
        End If
    End Sub

    Sub ibAdd_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStockAdjID As String
        Dim strStockAdjLNID As String
        Dim strParam As String
        Dim strParamList As String
        Dim intErrNum As Integer
        Dim strErrMsg As String

        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strItemCode As String = RadComboBoxProduct.SelectedValue ''ddlItemCode.SelectedItem.Value.Trim()
        Dim strAccCode As String = Request.Form("txtAccCode")  'Request.Form("ddlAccCode") 
        Dim strPreBlkCode As String = Request.Form("ddlPreBlkCode") 
        Dim strBlkCode As String = Request.Form("ddlBlkCode") 
        Dim strVehCode As String = Request.Form("ddlVehCode") 
        Dim strVehExpCode As String = Request.Form("ddlVehExpCode") 
        Dim decITMQtyOnHand As Decimal
        Dim decITMQtyOnHold As Decimal
        Dim decITMTotalQty As Decimal
        Dim decITMAverageCost As Decimal
        Dim decITMDiffAverageCost As Decimal
        Dim decITMTotalCost As Decimal
        Dim decQty As Decimal
        Dim decAverageCost As Decimal
        Dim decTotalCost As Decimal
        Dim blnQty As Boolean
        Dim blnAverageCost As Boolean
        Dim blnTotalCost As Boolean
        Dim blnBlockCharge As Boolean = False   
        Dim strNewIDFormat As String
        Dim strDate As String = CheckDate()

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

        lblActionResult.Visible = False
        lblItemCodeErr.Visible = False
        lblAccCodeErr.Visible = False
        lblPreBlkCodeErr.Visible = False
        lblBlkCodeErr.Visible = False
        lblVehCodeErr.Visible = False
        lblVehExpCodeErr.Visible = False
        lblQtyErr.Visible = False
        lblAverageCostErr.Visible = False
        lblTotalCostErr.Visible = False

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

        If strItemCode = "" Then
            lblItemCodeErr.Visible = True
            Exit Sub
        Else
            If InStr(strItemCode, ITEM_PART_SEPERATOR) <> 0 Then
                strItemCode = Trim(Mid(strItemCode, 1, InStr(strItemCode, ITEM_PART_SEPERATOR) - 1))
            End If
        End If

        If ddlAdjType.SelectedItem.Value = Trim(CStr(objINTrx.EnumAdjustmentType.Quantity)) Then
            strAccCode = ""
            strBlkCode = ""
            strVehCode = ""
            strVehExpCode = ""
        Else
            If strAccCode = "" Then
                lblAccCodeErr.Visible = True
            End If

            GetAccountProperties(strAccCode, intAccType, intAccPurpose, intNurseryInd)

            If ddlChargeLevel.SelectedIndex = 0 Then
                strBlkCode = strPreBlkCode
                blnBlockCharge = True
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
                ElseIf strVehCode <> "" And strVehExpCode = "" And intAccPurpose = objGLSetup.EnumAccountPurpose.Others Then
                    lblVehExpCodeErr.Visible = True
                ElseIf strVehCode = "" And strVehExpCode <> "" And intAccPurpose = objGLSetup.EnumAccountPurpose.Others Then
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

        decQty = 0
        decAverageCost = 0
        decTotalCost = 0
        blnQty = False
        blnAverageCost = False
        blnTotalCost = False

        Select Case ddlAdjType.SelectedItem.Value
            Case objINTrx.EnumAdjustmentType.Quantity
                blnQty = True
            Case objINTrx.EnumAdjustmentType.AverageCost
                blnAverageCost = True
            Case objINTrx.EnumAdjustmentType.TotalCost
                blnTotalCost = True
            Case objINTrx.EnumAdjustmentType.QuantityAtAverageCost
                blnQty = True
                blnAverageCost = True
            Case objINTrx.EnumAdjustmentType.QuantityAtTotalCost
                blnQty = True
                blnTotalCost = True
        End Select

        If blnQty = True And txtQty.Text.Trim() = "" Then
            lblQtyErr.Text = "Please enter Quantity."
            lblQtyErr.Visible = True
        ElseIf txtQty.Text.Trim() <> "" Then
            decQty = txtQty.Text.Trim()
        End If

        If blnAverageCost = True And txtAverageCost.Text.Trim() = "" Then
            lblAverageCostErr.Text = "Please enter Average Cost."
            lblAverageCostErr.Visible = True
        ElseIf txtAverageCost.Text.Trim() <> "" Then
            decAverageCost = txtAverageCost.Text.Trim()
        End If

        If blnTotalCost = True And txtTotalCost.Text.Trim() = "" Then
            lblTotalCostErr.Text = "Please enter Total Cost."
            lblTotalCostErr.Visible = True
        ElseIf txtTotalCost.Text.Trim() <> "" Then
            decTotalCost = txtTotalCost.Text.Trim()
        End If

        If lblItemCodeErr.Visible = True Or lblAccCodeErr.Visible = True Or _
            lblPreBlkCodeErr.Visible = True Or lblBlkCodeErr.Visible = True Or lblVehCodeErr.Visible = True Or _
            lblVehExpCodeErr.Visible = True Or lblQtyErr.Visible = True Or _
            lblAverageCostErr.Visible = True Or lblTotalCostErr.Visible = True Then
            Exit Sub
        End If

        decITMQtyOnHand = lblOriginalQtyOnHand.Text.Trim()
        decITMQtyOnHold = lblOriginalQtyOnHold.Text.Trim()
        decITMTotalQty = lblOriginalQty.Text.Trim()
        decITMAverageCost = lblOriginalAverageCost.Text.Trim().Replace(",", "")
        decITMDiffAverageCost = lblOriginalDiffAverageCost.Text.Trim().Replace(",", "")
        decITMTotalCost = lblOriginalTotalCost.Text.Trim().Replace(",", "")
        Try
            intErrNo = objINTrx.mtdStockAdjustment_Validate(ddlAdjType.SelectedItem.Value, _
                        ddlTransType.SelectedItem.Value, _
                        decQty, _
                        decAverageCost, _
                        decTotalCost, _
                        decITMQtyOnHand, _
                        decITMQtyOnHold, _
                        decITMTotalQty, _
                        decITMAverageCost, _
                        decITMTotalCost, _
                        intErrNum, _
                        strErrMsg)
        Catch Exp As Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
            End If
        End Try
        Select Case intErrNum
            Case objINTrx.EnumTransactionError.Quantity
                lblQtyErr.Text = strErrMsg
                lblQtyErr.Visible = True
                Exit Sub
            Case objINTrx.EnumTransactionError.AverageCost
                lblAverageCostErr.Text = strErrMsg
                lblAverageCostErr.Visible = True
                Exit Sub
            Case objINTrx.EnumTransactionError.TotalCost
                lblTotalCostErr.Text = strErrMsg
                lblTotalCostErr.Visible = True
                Exit Sub
        End Select



        strStockAdjID = lblStockAdjID.Text.Trim()
        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strNewIDFormat = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockAdjust) & Right(strAccYear, 2) & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth)
        '    Case Else
        '        strNewIDFormat = "NAB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        strNewIDFormat = "NAB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        If strStockAdjID = "" Then
            strParam = ddlAdjType.SelectedItem.Value & vbCrLf & ddlTransType.SelectedItem.Value & vbCrLf & txtRemark.Text.Trim() & vbCrLf & Trim(ddlInventoryBin.SelectedItem.Value) & vbCrLf & strNewIDFormat & vbCrLf & strDate
            Try
                intErrNo = objINTrx.mtdStockAdjustmentHeader_Upd(strOpCdStockAdjustmentHeader_ADD, _
                                                                 strOpCdStockAdjustmentHeader_UPD, _
                                                                 strOpCdStockAdjustmentLine_GET, _
                                                                 strCompany, _
                                                                 strLocation, _
                                                                 strUserId, _
                                                                 strAccMonth, _
                                                                 strAccYear, _
                                                                 strParam, _
                                                                 objINTrx.EnumTransactionAction.Insert, _
                                                                 intErrNum, _
                                                                 strStockAdjID)
            Catch Exp As Exception
                If intErrNo <> -5 Then
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
                End If
            End Try

            If intErrNum = objINTrx.EnumTransactionError.NoError And strStockAdjID <> "" Then
                lblStockAdjID.Text = strStockAdjID
            Else
                lblActionResult.Text = "System failed to create new stock adjustment. Adjustment line not created."
                lblActionResult.Visible = True
                Exit Sub
            End If
        End If

        strParam = strStockAdjID & vbCrLf & _
                   strItemCode & vbCrLf & _
                   strAccCode & vbCrLf & _
                   strBlkCode & vbCrLf & _
                   strVehCode & vbCrLf & _
                   strVehExpCode & vbCrLf & _
                   txtAdjDocRef.Text.Trim() & vbCrLf & _
                   decQty & vbCrLf & _
                   decAverageCost & vbCrLf & _
                   decTotalCost & vbCrLf & _
                   ddlAdjType.SelectedItem.Value & vbCrLf & _
                   ddlTransType.SelectedItem.Value

        strParamList = Session("SS_LOCATION") & "|" & _
                       txtAccCode.Text.Trim & "|" & _
                       ddlPreBlkCode.SelectedItem.Value.Trim() & "|" & _
                       objGLSetup.EnumBlockStatus.Active
        Try
            intErrNo = objINTrx.mtdStockAdjustmentLine_Upd(strOpCdStockAdjustmentLine_ADD, _
                                                           strOpCdStockAdjustmentLine_DEL, _
                                                           strOpCdStockAdjustmentLine_GET, _
                                                           strOpCdStockAdjustmentLine_UPD, _
                                                           strOpCdStockAdjustmentHeader_UPD, _
                                                           strOpCdItem_Det_GET, _
                                                           strOpCodeBlockChargingList_GET, _
                                                           strParamList, _
                                                           blnBlockCharge, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           objINTrx.EnumTransactionAction.Insert, _
                                                           intErrNum, _
                                                           strErrMsg, _
                                                           strStockAdjLNID)
        Catch Exp As Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
            End If
        End Try

        '''---------UPDATE STORAGE SELECT - SUPAYA TIDAK MENGUBAH DLL
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim strParamName As String
        Dim strParamValue As String

        sSQLKriteria = "UPDATE IN_STOCKADJLN SET StorageCode='" & lstStorage.SelectedItem.Value & "' Where StockAdjID='" & lblStockAdjID.Text & "'"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
        End Try

        ''---------------------


        If intErrNum = objINTrx.EnumTransactionError.NoError Then
            ResetPage(True, True, True)
        Else
            Select Case intErrNum
                Case objINTrx.EnumTransactionError.ItemNotFound
                    'BindItemCodeDropDownList("")
                    lblItemCodeErr.Visible = True

                Case objINTrx.EnumTransactionError.Quantity
                    lblQtyErr.Text = strErrMsg
                    lblQtyErr.Visible = True

                Case objINTrx.EnumTransactionError.AverageCost
                    lblAverageCostErr.Text = strErrMsg
                    lblAverageCostErr.Visible = True

                Case objINTrx.EnumTransactionError.TotalCost
                    lblTotalCostErr.Text = strErrMsg
                    lblTotalCostErr.Visible = True

                Case objINTrx.EnumTransactionError.SubBlockNotFound
                    lblActionResult.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " record not found for " & GetCaption(objLangCap.EnumLangCap.Block) & " code " & strBlkCode & "."
                    lblActionResult.Visible = True

                Case Else
                    lblActionResult.Text = "System failed to create new stock adjustment line."
                    lblActionResult.Visible = True
            End Select
        End If
    End Sub

    Sub ibSave_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStockAdjID As String
        Dim intStatus As Integer
        Dim intAction As Integer
        Dim intErrNum As Integer
        Dim strParam As String
        Dim strNewIDFormat As String
        Dim strDate As String = CheckDate()

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

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        lblActionResult.Visible = False
        strStockAdjID = lblStockAdjID.Text.Trim()
        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strNewIDFormat = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockAdjust) & Right(strAccYear, 2) & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth)
        '    Case Else
        '        strNewIDFormat = "NAB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        strNewIDFormat = "NAB" & "/" & strCompany & "/" & strLocation & "/" & Trim(ddlInventoryBin.SelectedItem.Value) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        If strStockAdjID = "" Then
            strParam = ddlAdjType.SelectedItem.Value & vbCrLf & ddlTransType.SelectedItem.Value & vbCrLf & txtRemark.Text.Trim() & vbCrLf & Trim(ddlInventoryBin.SelectedItem.Value) & vbCrLf & strNewIDFormat
            intAction = objINTrx.EnumTransactionAction.Insert
        Else
            strParam = strStockAdjID & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & ddlAdjType.SelectedItem.Value & vbCrLf & _
                    ddlTransType.SelectedItem.Value & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & _
                    Now() & vbCrLf & strUserId & vbCrLf & txtRemark.Text.Trim() & vbCrLf & _
                    Trim(ddlInventoryBin.SelectedItem.Value) & vbCrLf & strDate

            intAction = objINTrx.EnumTransactionAction.Update
        End If

        Try
            intErrNo = objINTrx.mtdStockAdjustmentHeader_Upd(strOpCdStockAdjustmentHeader_ADD, _
                                                             strOpCdStockAdjustmentHeader_UPD, _
                                                             strOpCdStockAdjustmentLine_GET, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strParam, _
                                                             intAction, _
                                                             intErrNum, _
                                                             strStockAdjID)
        Catch Exp As Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
            End If
        End Try

        If intAction = objINTrx.EnumTransactionAction.Insert And strStockAdjID <> "" Then
            lblStockAdjID.Text = strStockAdjID
        End If

        If intErrNum = objINTrx.EnumTransactionError.NoError Then
            DisplayStockAdjustmentHeader()
            DisplayStockAdjustmentLines()
            SetObjectAccessibilityByStatus()
            SetObjectAccessibilityByAdjustmentType()
        Else
            If intAction = objINTrx.EnumTransactionAction.Insert Then
                lblActionResult.Text = "System failed to create new stock adjustment."
            Else
                lblActionResult.Text = "System failed to update the stock adjustment " & strStockAdjID & "."
            End If
            lblActionResult.Visible = True
        End If
    End Sub

    Sub ibConfirm_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStockAdjID As String
        Dim intStatus As Integer
        Dim intAction As Integer
        Dim intErrNum As Integer
        Dim strErrMsg As String
        Dim strParam As String
        Dim strDate As String = CheckDate()

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

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        strStockAdjID = lblStockAdjID.Text.Trim()
        strParam = strStockAdjID & vbCrLf
        Try
            intErrNo = objINTrx.mtdStockAdjustment_Confirm(strOpCdStockAdjustmentHeader_UPD, _
                                                           strOpCdStockAdjustmentList_GET, _
                                                           strOpCdStockAdjustmentLine_UPD, _
                                                           strOpCdItem_Det_GET, _
                                                           strOpCdItem_Det_UPD, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId, _
                                                           strAccMonth, _
                                                           strAccYear, _
                                                           strParam, _
                                                           intErrNum, _
                                                           strErrMsg)
        Catch Exp As Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
            End If
        End Try

        Select Case intErrNum
            Case objINTrx.EnumTransactionError.DataOutdated
                Response.Write("<script language=""javascript"">if(window.confirm(""" & strErrMsg & """)){ window.location.href=""IN_Trx_StockAdj_Details.aspx?id=" & strStockAdjID & "&Action=SYN""; } </script>")
                Exit Sub

            Case objINTrx.EnumTransactionError.DescriptedError, objINTrx.EnumTransactionError.Quantity, _
                 objINTrx.EnumTransactionError.AverageCost, objINTrx.EnumTransactionError.TotalCost
                lblActionResult.Text = strErrMsg
                lblActionResult.Visible = True

            Case objINTrx.EnumTransactionError.InvalidParameter, objINTrx.EnumTransactionError.GeneralSystemError
                lblActionResult.Text = "System failed to confirm the stock adjustment."
                lblActionResult.Visible = True

        End Select

        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

        strParamValue = Trim(strLocation) & _
                        "|" & "IN_STOCKADJ" & _
                        "|" & "IN_STOCKADJLN" & _
                        "|" & "STOCKADJID" & _
                        "|" & Trim(lblStockAdjID.Text) & _
                        "|" & IIF(ddlTransType.SelectedItem.Value = objINTrx.EnumTransactionType.NewValue, "N_QUANTITY", "D_QUANTITY") & _
                        "|" & ddlInventoryBin.SelectedItem.Value & _
                        "|" & "+" & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEMBIN&errmesg=" & strErrMsg & "&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
        End Try

        DisplayStockAdjustmentHeader()
        DisplayStockAdjustmentLines()
        SetObjectAccessibilityByStatus()
        SetObjectAccessibilityByAdjustmentType()
    End Sub

    Sub SynchronizeStockAdjustment(ByVal pv_strStockAdjID As String)
        Dim intStatus As Integer
        Dim intAction As Integer
        Dim intErrNum As Integer
        Dim strErrMsg As String
        Dim strParam As String

        strParam = pv_strStockAdjID.Trim() & vbCrLf
        Try
            intErrNo = objINTrx.mtdStockAdjustment_Syn(strOpCdStockAdjustmentHeader_UPD, _
                                                       strOpCdStockAdjustmentList_GET, _
                                                       strOpCdStockAdjustmentLine_UPD, _
                                                       strOpCdItem_Det_GET, _
                                                       strOpCodeBlockChargingList_GET2, _
                                                       strCompany, _
                                                       strLocation, _
                                                       strUserId, _
                                                       strAccMonth, _
                                                       strAccYear, _
                                                       strParam, _
                                                       intErrNum, _
                                                       strErrMsg)
        Catch Exp As Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
            End If
        End Try

        Select Case intErrNum
            Case objINTrx.EnumTransactionError.DataOutdated 
                Response.Write("<script language=""javascript"">window.alert(""" & "Cannot update the stock adjustment due to " & GetCaption(objLangCap.EnumLangCap.SubBlock) & " setup changed." & """); window.location.href=""IN_Trx_StockAdj_Details.aspx?id=" & pv_strStockAdjID.Trim() & """; </script>")
            Case objINTrx.EnumTransactionError.DescriptedError
                Response.Write("<script language=""javascript"">window.alert(""" & strErrMsg & """); window.location.href=""IN_Trx_StockAdj_Details.aspx?id=" & pv_strStockAdjID.Trim() & """; </script>")
            Case objINTrx.EnumTransactionError.NoError
                Response.Redirect("IN_Trx_StockAdj_Details.aspx?id=" & pv_strStockAdjID.Trim())
            Case Else 
                Response.Write("<script language=""javascript"">window.alert(""" & "System failed to update the stock adjustment." & """); window.location.href=""IN_Trx_StockAdj_Details.aspx?id=" & pv_strStockAdjID.Trim() & """; </script>")
        End Select
    End Sub

    Sub ibDelete_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStockAdjID As String
        Dim intStatus As Integer
        Dim intAction As Integer
        Dim intErrNum As Integer
        Dim strParam As String
        Dim strDate As String = CheckDate()

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

        strStockAdjID = lblStockAdjID.Text.Trim()
        If CInt(lblHidStatus.Text.Trim()) = objINTrx.EnumStockAdjustStatus.Active Then
            intStatus = objINTrx.EnumStockAdjustStatus.Deleted
            intAction = objINTrx.EnumTransactionAction.Delete
        Else
            intStatus = objINTrx.EnumStockAdjustStatus.Active
            intAction = objINTrx.EnumTransactionAction.Undelete
        End If

        strParam = strStockAdjID & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & _
                   "" & vbCrLf & "" & vbCrLf & intStatus & vbCrLf & "" & vbCrLf & "" & vbCrLf & _
                   "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & ""
        Try
            intErrNo = objINTrx.mtdStockAdjustmentHeader_Upd(strOpCdStockAdjustmentHeader_ADD, _
                                                             strOpCdStockAdjustmentHeader_UPD, _
                                                             strOpCdStockAdjustmentLine_GET, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strParam, _
                                                             intAction, _
                                                             intErrNum, _
                                                             strStockAdjID)
        Catch Exp As Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
            End If
        End Try

        If intErrNum <> objINTrx.EnumTransactionError.NoError Then
            lblActionResult.Text = "System failed to " & IIf(intAction = objINTrx.EnumTransactionAction.Delete, "delete", "undelete") & " the stock adjustment " & strStockAdjID & "."
            lblActionResult.Visible = True
        End If

        DisplayStockAdjustmentHeader()
        DisplayStockAdjustmentLines()
        SetObjectAccessibilityByStatus()
        SetObjectAccessibilityByAdjustmentType()
    End Sub

    Sub ibNew_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_Trx_StockAdj_Details.aspx?AdjType=" & ddlAdjType.SelectedItem.Value & "&TransType=" & ddlTransType.SelectedItem.Value)
    End Sub

    Sub ibBack_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_Trx_StockAdj_List.aspx")
    End Sub

    Sub ibCancel_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStockAdjID As String
        Dim intStatus As Integer
        Dim intAction As Integer
        Dim intErrNum As Integer
        Dim strParam As String
        Dim strDate As String = CheckDate()

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

        strStockAdjID = lblStockAdjID.Text.Trim()
        If CInt(lblHidStatus.Text.Trim()) = objINTrx.EnumStockAdjustStatus.Confirmed Then
            intStatus = objINTrx.EnumStockAdjustStatus.Cancelled
            intAction = objINTrx.EnumTransactionAction.Cancel
        End If

        strParam = strStockAdjID & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & _
                   "" & vbCrLf & "" & vbCrLf & intStatus & vbCrLf & "" & vbCrLf & "" & vbCrLf & _
                   "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & ""
        Try
            intErrNo = objINTrx.mtdStockAdjustmentHeader_Upd(strOpCdStockAdjustmentHeader_ADD, _
                                                             strOpCdStockAdjustmentHeader_UPD, _
                                                             strOpCdStockAdjustmentLine_GET, _
                                                             strCompany, _
                                                             strLocation, _
                                                             strUserId, _
                                                             strAccMonth, _
                                                             strAccYear, _
                                                             strParam, _
                                                             intAction, _
                                                             intErrNum, _
                                                             strStockAdjID)
        Catch Exp As Exception
            If intErrNo <> -5 Then
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
            End If
        End Try

        If intErrNum <> objINTrx.EnumTransactionError.NoError Then
            lblActionResult.Text = "System failed to " & IIf(intAction = objINTrx.EnumTransactionAction.Delete, "delete", "undelete") & " the stock adjustment " & strStockAdjID & "."
            lblActionResult.Visible = True
        End If

        DisplayStockAdjustmentHeader()
        DisplayStockAdjustmentLines()
        SetObjectAccessibilityByStatus()
        SetObjectAccessibilityByAdjustmentType()
    End Sub

    Protected Function RoundNumber(ByVal d As Double, Optional ByVal decimals As Double = 0) As Double
        RoundNumber = Decimal.Divide(Int(Decimal.Add(Decimal.Multiply(d, 10.0 ^ decimals), 0.5)), 10.0 ^ decimals)
    End Function

    Protected Sub setFocus(ByVal ctrl As System.Web.UI.Control)
        Dim s As String = ("<SCRIPT language='javascript'>document.getElementById('" _
                    + (ctrl.ID + "').focus() </SCRIPT>"))
        Me.RegisterStartupScript("focus", s)
    End Sub

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
        Dim strText = "Please select Inventory Bin"

        'ddlInventoryBin.Items.Clear()
        ddlInventoryBin.Items.Add(New ListItem(strText, "0"))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.HO), objINSetup.EnumInventoryBinLevel.HO))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Central), objINSetup.EnumInventoryBinLevel.Central))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Other), objINSetup.EnumInventoryBinLevel.Other))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinI), objINSetup.EnumInventoryBinLevel.BinI))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinII), objINSetup.EnumInventoryBinLevel.BinII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIII), objINSetup.EnumInventoryBinLevel.BinIII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIV), objINSetup.EnumInventoryBinLevel.BinIV))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinV), objINSetup.EnumInventoryBinLevel.BinV))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinVI), objINSetup.EnumInventoryBinLevel.BinVI))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinVII), objINSetup.EnumInventoryBinLevel.BinVII))

        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        Else
            ddlInventoryBin.SelectedIndex = -1
        End If
    End Sub

    Protected Function CheckDate() As String

        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String

        If Not txtDate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFMT, txtDate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Text = objDateFormat & "."
                lblDate.Visible = True
                lblFmt.Visible = True

            End If
        End If

    End Function

    Protected Sub RadComboBoxProduct_ItemsRequested()

        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer
 
        sSQLKriteria="SELECT   ItemCode,RTRIM(Description) AS ItemName From IN_Item Where Status='1' And LocCode='" & strlocation & _
                    "' And Description LIKE 'oli%'"
        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
        End Try
 
        dr = objdsST.Tables(0).NewRow()
        dr("ItemCode") = "" 
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        RadComboBoxProduct.DataSource = objdsST.Tables(0)
        RadComboBoxProduct.DataValueField = "ItemCode"
        RadComboBoxProduct.DataTextField = "ItemName"
        RadComboBoxProduct.DataBind()
        '''RadComboBoxProduct.SelectedIndex = intSelectedIndex
    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

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
                        Session("SS_USERID") & "|" & Trim(lblStockAdjID.Text)

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

        DisplayStockAdjustmentHeader()
        DisplayStockAdjustmentLines()
    End Sub

End Class
