
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


Public Class WS_TRX_JOB : Inherits Page
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblJobID As Label
    Protected WithEvents lblJobStatus As Label
    Protected WithEvents lblJobVehCode As Label
    Protected WithEvents lblTransDateErr As Label
    Protected WithEvents lblEmpCodeErr As Label
    Protected WithEvents lblWorkCodeTag As Label
    Protected WithEvents lblWorkCodeErr As Label
    Protected WithEvents lblItemCodeErr As Label
    Protected Withevents lblItemType As Label
    Protected WithEvents lblItemCode As Label
    Protected WithEvents lblJobStockIssueID As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents lblPreBlkCodeTag As Label
    Protected WithEvents lblPreBlkCodeErr As Label
    Protected WithEvents lblBlkCodeTag As Label
    Protected WithEvents lblBlkCodeErr As Label
    Protected WithEvents lblVehCodeTag As Label
    Protected WithEvents lblVehCodeErr As Label
    Protected WithEvents lblVehExpCodeTag As Label
    Protected WithEvents lblVehExpCodeErr As Label
    Protected WithEvents lblVehStkExpCodeTag As Label
    Protected WithEvents lblVehStkExpCodeErr As Label
    Protected WithEvents lblQuantityTag As Label
    Protected WithEvents lblQtyErr As Label
    Protected WithEvents lblActionResult As Label
    Protected WithEvents lblTotalCost As Label
    Protected WithEvents lblTotalPrice As Label

    Protected WithEvents ddlEmpCode As DropDownList
    Protected WithEvents ddlWorkCode As DropDownList
    Protected WithEvents ddlItemCode As DropDownList
    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents ddlPreBlkCode As DropDownList
    Protected WithEvents ddlBlkCode As DropDownList
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents ddlVehExpCode As DropDownList
    Protected WithEvents ddlVehStkExpCode As DropDownList

    Protected WithEvents txtTransDate As TextBox
    Protected WithEvents txtQty As TextBox

    Protected WithEvents rbTransTypeIssue As RadioButton
    Protected WithEvents rbTransTypeReturn As RadioButton

    Protected WithEvents ibAdd As ImageButton
    Protected WithEvents ibBack As ImageButton

    Protected WithEvents dgJobStock As DataGrid

    Protected WithEvents imgTransDate As Image

    Protected WithEvents revQty As RegularExpressionValidator

    Protected WithEvents tblMain As HtmlTable

    Protected WithEvents lblLineNo As Label
    Protected WithEvents ddlLineNo As DropDownList
    Protected WithEvents lblLineNoErr As Label
    Protected lblSubBlkCode As Label
    Protected lblJobType As Label

    Protected WithEvents trAccCode As HtmlTableRow
    Protected WithEvents trChargeLevel As HtmlTableRow
    Protected WithEvents trPreBlkCode As HtmlTableRow
    Protected WithEvents trBlkCode As HtmlTableRow
    Protected WithEvents trVehCode As HtmlTableRow
    Protected WithEvents trVehExpCode As HtmlTableRow
    Protected WithEvents trVehStkExpCode As HtmlTableRow

    Protected WithEvents btnFindAccCode As HtmlInputButton

    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden

    Protected objWSTrx As New agri.WS.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objWSSetup As New agri.WS.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.PR.clsSetup()
    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim objPRSetup As New agri.PR.clsSetup()

    Dim objPMSetup As New agri.PM.clsSetup()
    
    Dim strOpCode_JobStock_Add As String = "WS_CLSTRX_JOB_STOCK_ADD"
    Dim strOpCode_JobStock_Get As String = "WS_CLSTRX_JOB_STOCK_GET"
    Dim strOpCode_JobStock_Upd As String = "WS_CLSTRX_JOB_STOCK_UPD"
    Dim strOpCode_Job_Get As String = "WS_CLSTRX_JOB_GET"
    Dim strOpCode_Job_Upd As String = "WS_CLSTRX_JOB_UPD"
    Dim strOpCode_Job_Get_For_Upd As String = "WS_CLSTRX_JOB_GET_FOR_UPDATE"
    Dim strOpCode_JobWorkCode_Get As String = "WS_CLSTRX_JOBWORKCODE_GET"
    Dim strOpCode_Item_Get As String = "IN_CLSSETUP_ITEM_DETAIL_GET"
    Dim strOpCode_Item_Upd As String = "IN_CLSSETUP_ITEM_DETAIL_UPD"
    Dim strOpCode_UpdLifespan_ItmIssue As String = "WS_CLSTRX_UPDATELIFESPAN_ITEMISSUE"
    Dim strOpCode_UpdLifespan_ItmReturn As String = "WS_CLSTRX_UPDATELIFESPAN_ITEMRETURN"
    
    Dim dsLangCap As New DataSet()

    Const APPEND_EMP_CODE As Boolean = False
    Const APPEND_WORK_CODE As Boolean = False
    Const APPEND_ITEM_CODE As Boolean = False
    Const APPEND_ACC_CODE As Boolean = False
    Const APPEND_BLK_CODE As Boolean = False
    Const APPEND_VEH_CODE As Boolean = False
    Const APPEND_VEH_EXP_CODE As Boolean = False
    Const APPEND_VEH_STK_EXP_CODE As Boolean = True

    Protected WithEvents FindWS As HtmlInputButton
    Const ITEM_PART_SEPERATOR As String = " @ "
    
    Dim intErrNo As Integer
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim intConfigsetting As Integer
    Dim intCnt As Integer
    Dim strLocType as String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")  
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblTransDateErr.Visible = False
            lblEmpCodeErr.Visible = False
            lblWorkCodeErr.Visible = False
            lblItemCodeErr.Visible = False
            lblAccCodeErr.Visible = False
            lblPreBlkCodeErr.Visible = False
            lblBlkCodeErr.Visible = False
            lblVehCodeErr.Visible = False
            lblVehExpCodeErr.Visible = False
            lblVehStkExpCodeErr.Visible = False
            lblQtyErr.Visible = False
            lblActionResult.Visible = False
            lblLineNoErr.Visible = False

            If Not Page.IsPostBack Then
                GetLangCap()
                lblJobID.Text = Trim(Request.QueryString("jobid"))
                If strLocType = objLoc.EnumLocType.Mill Then
                    lblSubBlkCode.Text = Trim(Request.QueryString("SubBlkCode"))
                    BindChargeLevelDropDownList()
                End If
                rbTransTypeIssue.Text = objWSTrx.mtdGetJobStockType(objWSTrx.EnumJobStockType.Issued)
                rbTransTypeReturn.Text = objWSTrx.mtdGetJobStockType(objWSTrx.EnumJobStockType.Returned)
                If lblJobID.Text = "" Then
                    Response.Redirect("WS_Trx_Job_List.aspx")
                Else
                
                ResetPage()
                End If
            End If
        End If
    End Sub

    Sub GetLangCap()
        dsLangCap = GetLanguageCaptionDS()
        
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        Else
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        End If
        
        lblWorkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Work) & lblCode.Text
        lblPreBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        lblAccCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpCodeTag.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
        lblVehStkExpCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Expense) & lblCode.Text
        
        lblEmpCodeErr.Text = lblPleaseSelect.Text & "Employee Code"
        lblItemCodeErr.Text = lblPleaseSelect.Text & "Item Code"
        lblWorkCodeErr.Text = lblPleaseSelect.Text & lblWorkCodeTag.Text
        lblAccCodeErr.Text = lblPleaseSelect.Text & lblAccCodeTag.Text
        lblPreBlkCodeErr.Text = lblPleaseSelect.Text & lblPreBlkCodeTag.Text
        lblBlkCodeErr.Text = lblPleaseSelect.Text & lblBlkCodeTag.Text
        lblVehCodeErr.Text = lblPleaseSelect.Text & lblVehCodeTag.Text
        lblVehExpCodeErr.Text = lblPleaseSelect.Text & lblVehExpCodeTag.Text
        lblVehStkExpCodeErr.Text = lblPleaseSelect.Text & lblVehStkExpCodeTag.Text
        
        lblLineNoErr.Text = lblPleaseSelect.Text & "Line Number"
        
        dgJobStock.Columns(4).HeaderText = lblWorkCodeTag.Text
        dgJobStock.Columns(6).HeaderText = lblAccCodeTag.Text
        dgJobStock.Columns(7).HeaderText = lblBlkCodeTag.Text
        dgJobStock.Columns(8).HeaderText = lblVehCodeTag.Text
        dgJobStock.Columns(9).HeaderText = lblVehExpCodeTag.Text
        dgJobStock.Columns(10).HeaderText = lblVehStkExpCodeTag.Text
    End Sub


    Function GetCaption(ByVal pv_TermCode as String) As String
        Dim I As Integer

       For I = 0 To dsLangCap.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(dsLangCap.Tables(0).Rows(I).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTermMW"))
                else
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    Sub SetObjectAccessibilityByStatus()
        If Trim(lblJobStatus.Text) = Trim(CStr(objWSTrx.EnumJobStatus.Active)) Then
            ddlEmpCode.Enabled = True
            ddlWorkCode.Enabled = True
            ddlItemCode.Enabled = True
            ddlAccCode.Enabled = True

            If strLocType = objLoc.EnumLocType.Mill Then
                ddlChargeLevel.Enabled = True
                ddlPreBlkCode.Enabled = True
            End If
            ddlBlkCode.Enabled = True
            ddlVehCode.Enabled = True
            ddlVehExpCode.Enabled = True
            ddlVehStkExpCode.Enabled = True
            
            txtTransDate.Enabled = True
            txtQty.Enabled = True
            
            rbTransTypeIssue.Enabled = True
            rbTransTypeReturn.Enabled = True
            
            ibAdd.Visible = True
            
            imgTransDate.Visible = True
            
            revQty.Enabled = True
            
            btnFindAccCode.Visible = True
        Else
            ddlEmpCode.Enabled = False
            ddlWorkCode.Enabled = False
            ddlItemCode.Enabled = False
            ddlAccCode.Enabled = False

            ddlChargeLevel.Enabled = False
            ddlPreBlkCode.Enabled = False

            ddlBlkCode.Enabled = False
            ddlVehCode.Enabled = False
            ddlVehExpCode.Enabled = False
            ddlVehStkExpCode.Enabled = False
            
            txtTransDate.Enabled = False
            txtQty.Enabled = False
            
            rbTransTypeIssue.Enabled = False
            rbTransTypeReturn.Enabled = False
            
            ibAdd.Visible = False
            
            imgTransDate.Visible = False
            
            revQty.Enabled = False
            
            btnFindAccCode.Visible = False
        End If
    End Sub
    
    Sub SetObjectVisibilityByItemType(ByVal pv_strItemCode As String, ByVal pv_strLocCode As String)
        Dim strItemType As String
        Dim decQtyOnHand As Decimal
        Dim decQtyOnHold As Decimal
        Dim decAverageCost As Decimal
        Dim decDiffAverageCost As Decimal
        Dim decPrice As Decimal
        Dim blnError As Boolean
        
        If Trim(pv_strItemCode) = "" Or Trim(pv_strLocCode) = "" Then
            trAccCode.Visible = False

            trChargeLevel.Visible = False
            trPreBlkCode.Visible = False

            trBlkCode.Visible = False
            trVehCode.Visible = False
            trVehExpCode.Visible = False
        Else
            Call GetItemProperties(pv_strItemCode, pv_strLocCode, strItemType, decQtyOnHand, decQtyOnHold, decAverageCost, decDiffAverageCost, decPrice, blnError)
            If blnError = True Then
                lblActionResult.Text = "Cannot get detail information for selected item."
                lblActionResult.Visible = True
            Else
                lblItemType.Text = strItemType
                If strItemType = Trim(CStr(objWSTrx.EnumInventoryItemType.DirectCharge)) Then
                    trAccCode.Visible = True
                    
                    If strLocType = objLoc.EnumLocType.Mill Then
                        trChargeLevel.Visible = True
                        trPreBlkCode.Visible = True
                    End If

                    trBlkCode.Visible = True
                    trVehCode.Visible = True
                    trVehExpCode.Visible = True

                ElseIf strLocType = objLoc.EnumLocType.Mill And strItemType = Trim(CStr(objWSTrx.EnumInventoryItemType.WorkshopItem)) Then
                    trAccCode.Visible = True
                    trChargeLevel.Visible = True
                    trPreBlkCode.Visible = True
                    trBlkCode.Visible = True
                    trVehCode.Visible = True
                    trVehExpCode.Visible = True
                Else
                    trAccCode.Visible = False
                    trChargeLevel.Visible = False
                    trPreBlkCode.Visible = False
                    trBlkCode.Visible = False
                    trVehCode.Visible = False
                    trVehExpCode.Visible = False
                End If
            End If
        End If
    End Sub
    
    Sub LoadLabelText()
        If rbTransTypeIssue.Checked = True Then
            lblQuantityTag.Text = "Quantity to Issue"
        Else
            lblQuantityTag.Text = "Quantity to Return"
        End If
    End Sub
    
    Sub ResetPage()
        DisplayJobHeader()
        DisplayJobStockLines()
        rbTransTypeIssue.Checked = True
        rbTransTypeReturn.Checked = False
        LoadLabelText()
        txtTransDate.Text = ""
        BindEmpCodeDropDownList(Session("SS_LOCATION"), "")
        BindWorkCodeDropDownList("")
        If strLocType = objLoc.EnumLocType.Mill And Trim(lblSubBlkCode.Text) <> "" Then
            lblLineNo.Text = "Line Number : "
            lblLineNo.Visible = True
            ddlLineNo.Visible = True                       
            BindItemToMachineDropDownList(Session("SS_LOCATION"), Trim(lblJobID.Text), "", Trim(lblSubBlkCode.Text), True)        
        Else
            lblLineNoErr.Visible = False
            lblLineNo.Visible = False
            ddlLineNo.Visible = False
            BindItemDropDownList(Session("SS_LOCATION"), Trim(lblJobID.Text), "", True)
        End If
        BindAccCodeDropDownList("")
        Call BindAccountComponents(Session("SS_LOCATION"), "", "", "", "", "")
        BindVehStkExpCodeDropDownList("")
        txtQty.Text = ""
        SetObjectAccessibilityByStatus()
        SetObjectVisibilityByItemType("", Session("SS_LOCATION"))
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


    Sub BindEmpCodeDropDownList(ByVal pv_strLocCode As String, ByVal pv_strEmpCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_EMPLOYEE_LIST_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "ORDER BY Mst.EmpCode|AND Mst.LocCode = '" & FixSQL(pv_strLocCode) & "' AND Mst.Status = '" & objHRTrx.EnumEmpStatus.Active & "' AND Det.MechInd = '" & objHRTrx.mtdGetMechStatus(objHRTrx.EnumMechStatus.Yes) & "'"
        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd, strParam, 1, dsList)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_BIND_EMPLOYEE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("EmpCode"))
            dsList.Tables(0).Rows(intCnt).Item("EmpName") = Trim(dsList.Tables(0).Rows(intCnt).Item("EmpCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("EmpCode"))) = LCase(Trim(pv_strEmpCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("EmpCode") = ""
        drNew("EmpName") = lblSelect.Text & "Employee Code"
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strEmpCode) <> "" And intSelectedIndex = 0 And APPEND_EMP_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("EmpCode") = Trim(pv_strEmpCode)
            drNew("EmpName") = Trim(pv_strEmpCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlEmpCode.DataSource = dsList.Tables(0)
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataTextField = "EmpName"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub
    
    Sub BindWorkCodeDropDownList(ByVal pv_strWorkCode As String)
        Dim strOpCd As String = "WS_CLSTRX_JOBWORKCODE_GET"
        Dim colParam As New Collection
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strSearch As String
        Dim strErrMsg As String

        strSearch = "WHERE J.JobID = '" & FixSQL(Trim(lblJobID.Text)) & "'" & vbCrLf & "ORDER BY WC.WorkCode"
        colParam.Add(strSearch, "PM_SEARCH")
        colParam.Add(strOpCd, "OC_JOB_WORK_CODE_GET")
        Try
            intErrNo = objWSTrx.mtdJobWorkCode_Get(colParam, dsList, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New Exception(strErrMsg)
            End If
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_BIND_WORKCODE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("WorkCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("WorkCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("WorkCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("WorkCode"))) = LCase(Trim(pv_strWorkCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("WorkCode") = ""
        drNew("Description") = lblSelect.Text & lblWorkCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strWorkCode) <> "" And intSelectedIndex = 0 And APPEND_WORK_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("WorkCode") = Trim(pv_strWorkCode)
            drNew("Description") = Trim(pv_strWorkCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlWorkCode.DataSource = dsList.Tables(0)
        ddlWorkCode.DataValueField = "WorkCode"
        ddlWorkCode.DataTextField = "Description"
        ddlWorkCode.DataBind()
        ddlWorkCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub
    
    Sub BindItemDropDownList(ByVal pv_strLocCode As String, ByVal pv_strJobID As String, ByVal pv_strItemCode As String, ByVal pv_blnForIssue As Boolean)
        Dim strOpCd As String = "WS_CLSTRX_JOBSTOCK_ITEM_GET"
        Dim colParam As New Collection
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strSearch As String
        Dim strErrMsg As String
        Dim strCurrentItem as String        
        Dim strDescription as String    
        
        colParam.Add(pv_strLocCode, "PM_LOCCODE")
        colParam.Add(pv_strJobID, "PM_JOBID")
        colParam.Add(IIf(pv_blnForIssue = True, "true", "false"), "PM_GET_FOR_ISSUE")
        colParam.Add(strOpCd, "OC_JOBSTOCK_ITEM_CODE_GET")
        Try
            intErrNo = objWSTrx.mtdJobStockItemCode_Get(colParam, dsList, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New Exception(strErrMsg)
            End If
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_BIND_ITEM_CODE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        If pv_blnForIssue = True Then
            For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
                dsList.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("ItemCode"))
                strCurrentItem = Trim(dsList.Tables(0).Rows(intCnt).Item("ItemCode"))
                strDescription = Trim(dsList.Tables(0).Rows(intCnt).Item("Description"))
                If Trim(dsList.Tables(0).Rows(intCnt).Item("PartNo")) = "" Then
                    dsList.Tables(0).Rows(intCnt).Item("Description") = dsList.Tables(0).Rows(intCnt).Item("ItemCode") & _
                                                                        " (" & strDescription & ") " & ", " & _
                                                                         "Rp. " & objGlobal.GetIDDecimalSeparator(dsList.Tables(0).Rows(intCnt).Item("AverageCost")) & ", " & _
                                                                         objGlobal.GetIDDecimalSeparator_FreeDigit(dsList.Tables(0).Rows(intCnt).Item("QtyOnHand"),5) & ", " & _
                                                                         Trim(dsList.Tables(0).Rows(intCnt).Item("UOMCode"))
                Else
                    dsList.Tables(0).Rows(intCnt).Item("ItemCode") = dsList.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & Trim(dsList.Tables(0).Rows(intCnt).Item("PartNo"))
                    dsList.Tables(0).Rows(intCnt).Item("Description") = strCurrentItem & " (" & strDescription & ") " & " @ " & Trim(dsList.Tables(0).Rows(intCnt).Item("PartNo")) & ", " & _
                                                                         "Rp. " & objGlobal.GetIDDecimalSeparator(dsList.Tables(0).Rows(intCnt).Item("AverageCost")) & ", " & _
                                                                         objGlobal.GetIDDecimalSeparator_FreeDigit(dsList.Tables(0).Rows(intCnt).Item("QtyOnHand"),5) & ", " & _
                                                                         Trim(dsList.Tables(0).Rows(intCnt).Item("UOMCode")) 
                End If
                If LCase(strCurrentItem) = LCase(Trim(pv_strItemCode)) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        Else
            For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
                strDescription = Trim(dsList.Tables(0).Rows(intCnt).Item("Description"))
                dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("ItemCode")) & " (" & strDescription & ") " & ", " & _
                                                                     Trim(dsList.Tables(0).Rows(intCnt).Item("JobStockID")) & ", " & _
                                                                     "Rp. " & objGlobal.GetIDDecimalSeparator(dsList.Tables(0).Rows(intCnt).Item("Cost")) & ", " & _
                                                                      objGlobal.GetIDDecimalSeparator_FreeDigit(dsList.Tables(0).Rows(intCnt).Item("Quantity"),5)
                
                dsList.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("ItemCode")) & " (" & strDescription & ") " & ITEM_PART_SEPERATOR & Trim(dsList.Tables(0).Rows(intCnt).Item("JobStockID"))
                If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("JobStockID"))) = LCase(Trim(pv_strItemCode)) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        drNew = dsList.Tables(0).NewRow()
        drNew("ItemCode") = ""
        drNew("Description") = lblSelect.Text & "Item Code"
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strItemCode) <> "" And intSelectedIndex = 0 And APPEND_ITEM_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("WorkCode") = Trim(pv_strItemCode)
            drNew("Description") = Trim(pv_strItemCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlItemCode.DataSource = dsList.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub
    
    Sub BindAccountComponents(ByVal pv_strLocCode As String, ByVal pv_strAccCode As String, ByVal pv_strPreBlkCode As String, ByVal pv_strBlkCode As String, ByVal pv_strVehCode As String, ByVal pv_strVehExpCode As String)
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        
        GetAccountProperties(pv_strAccCode, intAccType, intAccPurpose, intNurseryInd)

        If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLSetup.EnumAccountPurpose.NonVehicle
                    BindPreBlkCodeDropDownList(pv_strLocCode, pv_strAccCode, pv_strPreBlkCode)
                    BindBlkCodeDropDownList(pv_strLocCode, pv_strAccCode, pv_strBlkCode)
                    BindVehCodeDropDownList(pv_strLocCode, "", "")
                    BindVehExpCodeDropDownList("", True)
                Case objGLSetup.EnumAccountPurpose.VehicleDistribution
                    BindPreBlkCodeDropDownList("", "", "")
                    BindBlkCodeDropDownList(pv_strLocCode, "", "")
                    BindVehCodeDropDownList(pv_strLocCode, pv_strAccCode, pv_strVehCode)
                    BindVehExpCodeDropDownList(pv_strVehExpCode, False)
                Case Else
                    BindPreBlkCodeDropDownList(pv_strLocCode, pv_strAccCode, pv_strPreBlkCode)
                    BindBlkCodeDropDownList(pv_strLocCode, pv_strAccCode, pv_strBlkCode)
                    BindVehCodeDropDownList(pv_strLocCode, "%", pv_strVehCode)
                    BindVehExpCodeDropDownList(pv_strVehExpCode, False)
            End Select
        ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
            BindPreBlkCodeDropDownList(pv_strLocCode, pv_strAccCode, pv_strPreBlkCode)
            BindBlkCodeDropDownList(pv_strLocCode, pv_strAccCode, pv_strBlkCode)
            BindVehCodeDropDownList(pv_strLocCode, "", "")
            BindVehExpCodeDropDownList("", True)
        Else
            BindPreBlkCodeDropDownList("", "", "")
            BindBlkCodeDropDownList(pv_strLocCode, "", "")
            BindVehCodeDropDownList(pv_strLocCode, "", "")
            BindVehExpCodeDropDownList("", True)
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
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.AccountCode, dsList)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_BIND_ACCOUNT&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("AccCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("AccCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("AccCode"))) = LCase(Trim(pv_strAccCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("AccCode") = ""
        drNew("Description") = lblSelect.Text & lblAccCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strAccCode) <> "" And intSelectedIndex = 0 And APPEND_ACC_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("AccCode") = Trim(pv_strAccCode)
            drNew("Description") = Trim(pv_strAccCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
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
        ddlChargeLevel.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.Block), objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.SubBlock), objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.selectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        trChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")

        ToggleChargeLevel()
    End Sub
    
    Sub BindPreBlkCodeDropDownList(ByVal pv_strLocation As String, ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNT_PREBLOCK_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        Try
            strParam = pv_strAccCode & "|" & pv_strLocation & "|" & objGLSetup.EnumBlockStatus.Active
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     dsList)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_BIND_PREBLOCK&errmesg=" & Server.UrlEncode(Exp.ToString()) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
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

    Sub BindBlkCodeDropDownList(ByVal pv_strLocation As String, ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strOpCd = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = Trim(pv_strAccCode) & "|" & pv_strLocation & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = Trim(pv_strAccCode) & "|" & pv_strLocation & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, strParam, dsList)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_BIND_BLOCK&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("BlkCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("BlkCode"))) = LCase(Trim(pv_strBlkCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        drNew = dsList.Tables(0).NewRow()
        drNew("BlkCode") = ""
        drNew("Description") = lblSelect.Text & lblBlkCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strBlkCode) <> "" And intSelectedIndex = 0 And APPEND_BLK_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("BlkCode") = Trim(pv_strBlkCode)
            drNew("Description") = Trim(pv_strBlkCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
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

    Sub BindVehCodeDropDownList(ByVal pv_strLocation As String, ByVal pv_strAccCode As String, ByVal pv_strVehCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_VEH_LIST_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "|AccCode = '" & pv_strAccCode.Trim() & "' AND LocCode = '" & pv_strLocation & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'"
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.Vehicle, dsList)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_BIND_VEHICLE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("VehCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("VehCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("VehCode")) & " ( " & _
                                                                Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & " )"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("VehCode"))) = LCase(Trim(pv_strVehCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drNew = dsList.Tables(0).NewRow()
        drNew("VehCode") = ""
        drNew("Description") = lblSelect.Text & lblVehCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strVehCode) <> "" And intSelectedIndex = 0 And APPEND_VEH_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("VehCode") = Trim(pv_strVehCode)
            drNew("Description") = Trim(pv_strVehCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
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
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.VehicleExpense, dsList)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_BIND_VEHICLE_EXPENSE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode")) & " ( " & _
                                                                Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & " )"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode"))) = LCase(Trim(pv_strVehExpCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drNew = dsList.Tables(0).NewRow()
        drNew(0) = ""
        drNew(1) = lblSelect.Text & lblVehExpCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strVehExpCode) <> "" And intSelectedIndex = 0 And APPEND_VEH_EXP_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("VehExpenseCode") = Trim(pv_strVehExpCode)
            drNew("Description") = Trim(pv_strVehExpCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
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
    
    Sub BindVehStkExpCodeDropDownList(ByVal pv_strVehStkExpCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "Order By VehExpenseCode ASC|And Veh.Status = '" & objGLSetup.EnumVehicleExpenseStatus.active & "'"
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, strParam, objGLSetup.EnumGLMasterType.VehicleExpense, dsList)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_BIND_VEHICLE_STOCK_EXPENSE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode"))
            dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode")) & " ( " & _
                                                                Trim(dsList.Tables(0).Rows(intCnt).Item("Description")) & " )"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("VehExpenseCode"))) = LCase(Trim(pv_strVehStkExpCode)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drNew = dsList.Tables(0).NewRow()
        drNew(0) = ""
        drNew(1) = lblSelect.Text & lblVehStkExpCodeTag.Text
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        If Trim(pv_strVehStkExpCode) <> "" And intSelectedIndex = 0 And APPEND_VEH_STK_EXP_CODE = True Then
            drNew = dsList.Tables(0).NewRow()
            drNew("VehExpenseCode") = Trim(pv_strVehStkExpCode)
            drNew("Description") = Trim(pv_strVehStkExpCode) & " (Deleted)"
            dsList.Tables(0).Rows.InsertAt(drNew, dsList.Tables(0).Rows.Count)
            intSelectedIndex = dsList.Tables(0).Rows.Count - 1
        End If

        ddlVehStkExpCode.DataSource = dsList.Tables(0)
        ddlVehStkExpCode.DataValueField = "VehExpenseCode"
        ddlVehStkExpCode.DataTextField = "Description"
        ddlVehStkExpCode.DataBind()
        ddlVehStkExpCode.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub

    Sub BindItemToMachineDropDownList(ByVal pv_strLocCode As String, ByVal pv_strJobID As String, ByVal pv_strItemCode As String, ByVal pv_strSubBlkCode As String, ByVal pv_blnForIssue As Boolean)
        Dim strOpCd As String = "WS_CLSTRX_JOBSTOCK_ITEMTOMACHINE_GET"
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim strParam As String
        Dim strLocation As String = Session("SS_LOCATION")
        Dim strDescription As String
        Dim strCurrentItem as String

        strParam = IIf(pv_blnForIssue = True, "true", "false") & "|" & _
                   objWSSetup.EnumStockItemStatus.Active & "|" & _
                   objINSetup.EnumInventoryItemType.WorkshopItem & "|" & _
                   strLocation & "|" & _
                   lblSubBlkCode.Text & "|" & _
                   objWSTrx.EnumJobStockType.Issued & "|" & _
                   lblJobID.Text
        Try
            intErrNo = objWSTrx.mtdJobStockItemToMachine_Get(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            dsList)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOBSTOCK_ITEMTOMACHINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
                
        If pv_blnForIssue = True Then
            For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
                dsList.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("ItemCode"))
                strCurrentItem = Trim(dsList.Tables(0).Rows(intCnt).Item("ItemCode"))
                strDescription = Trim(dsList.Tables(0).Rows(intCnt).Item("Description"))

                If Trim(dsList.Tables(0).Rows(intCnt).Item("PartNo")) = "" Then
                    dsList.Tables(0).Rows(intCnt).Item("Description") = dsList.Tables(0).Rows(intCnt).Item("ItemCode") & _
                                                                        " (" & strDescription & ") " & ", " & _
                                                                         "Rp. " & objGlobal.GetIDDecimalSeparator(dsList.Tables(0).Rows(intCnt).Item("AverageCost")) & ", " & _
                                                                         objGlobal.GetIDDecimalSeparator_FreeDigit(dsList.Tables(0).Rows(intCnt).Item("QtyOnHand"),5) & ", " & _
                                                                         Trim(dsList.Tables(0).Rows(intCnt).Item("UOMCode"))
                Else
                    dsList.Tables(0).Rows(intCnt).Item("ItemCode") = dsList.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & Trim(dsList.Tables(0).Rows(intCnt).Item("PartNo"))
                    dsList.Tables(0).Rows(intCnt).Item("Description") = strCurrentItem & " (" & strDescription & ") " & " @ " & Trim(dsList.Tables(0).Rows(intCnt).Item("PartNo")) & ", " & _
                                                                         "Rp. " & objGlobal.GetIDDecimalSeparator(dsList.Tables(0).Rows(intCnt).Item("AverageCost")) & ", " & _
                                                                         objGlobal.GetIDDecimalSeparator_FreeDigit(dsList.Tables(0).Rows(intCnt).Item("QtyOnHand"),5) & ", " & _
                                                                         Trim(dsList.Tables(0).Rows(intCnt).Item("UOMCode")) 
                End If
                If LCase(strCurrentItem) = LCase(Trim(pv_strItemCode)) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        Else
            For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
                strCurrentItem = Trim(dsList.Tables(0).Rows(intCnt).Item("ItemCode")) 
                strDescription = Trim(dsList.Tables(0).Rows(intCnt).Item("Description"))
                dsList.Tables(0).Rows(intCnt).Item("Description") = Trim(dsList.Tables(0).Rows(intCnt).Item("ItemCode")) & " (" & strDescription & ") " & ", " & _
                                                                     Trim(dsList.Tables(0).Rows(intCnt).Item("JobStockID")) & ", " & _
                                                                     "Rp. " & objGlobal.GetIDDecimalSeparator(dsList.Tables(0).Rows(intCnt).Item("Cost")) & ", " & _
                                                                      objGlobal.GetIDDecimalSeparator_FreeDigit(dsList.Tables(0).Rows(intCnt).Item("Quantity"),5) & ", " & _
                                                                     Trim(dsList.Tables(0).Rows(intCnt).Item("LinesNo"))
                
                dsList.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(dsList.Tables(0).Rows(intCnt).Item("ItemCode")) & " (" & strDescription & ") " & ITEM_PART_SEPERATOR & Trim(dsList.Tables(0).Rows(intCnt).Item("JobStockID"))
                If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("JobStockID"))) = LCase(Trim(pv_strItemCode)) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If                

        drNew = dsList.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strItemCode <> "" Then
                drNew("ItemCode") = Trim(pv_strItemCode)
                drNew("Description") = Trim(pv_strItemCode)
            Else
                drNew("ItemCode") = ""
                drNew("Description") = "Select Item Code"
            End If
        Else
            drNew("ItemCode") = ""
            drNew("Description") = "Select Item Code"
        End If
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        ddlItemCode.DataSource = dsList.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex
        
        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub    

    Sub GetItemLineNo(ByVal pv_strLineNo As String, ByVal pv_strSubBlkCode As String, ByVal pv_strItemCode As String)
        Dim strOpCd As String = "WS_CLSTRX_ITEMTOMACHINE_GET"
        Dim strParam As String 
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsList As DataSet
        Dim drNew As DataRow
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim arrItemCode As Array
        Dim strItemCode As String
       
        If rbTransTypeIssue.Checked = True Then
            strParam = "|" & "SubBlkCode = '" & Trim(pv_strSubBlkCode) & "' " & _
                   " And ItemCode = '" & Trim(pv_strItemCode) & "'"
        Else
           strParam = "|" & "SubBlkCode = '" & Trim(pv_strSubBlkCode) & "' " & _
                   " And ItemCode = '" & Trim(pv_strItemCode) & "' " & _
                   " And LinesNo = '" & Trim(pv_strLineNo) & "' " 
        End If

        Try
            intErrNo = objINTrx.mtdGetItemToMachineTrxLn(strOpCd, _
                                            strParam, _
                                            dsList, _
                                            False)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEMTOMACHINELN_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
            dsList.Tables(0).Rows(intCnt).Item("LinesDesc") = Trim(dsList.Tables(0).Rows(intCnt).Item("LinesNo")) & " ( " & _
                                                                Trim(dsList.Tables(0).Rows(intCnt).Item("LinesDesc")) & " )"
            If LCase(Trim(dsList.Tables(0).Rows(intCnt).Item("LinesNo"))) = LCase(Trim(pv_strLineNo)) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        drNew = dsList.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strLineNo <> "" Then
                drNew("LinesNo") = Trim(pv_strLineNo)
                drNew("LinesDesc") = Trim(pv_strLineNo)
            Else
                drNew("LinesNo") = ""
                drNew("LinesDesc") = "Select Line Number"
            End If
        Else
            drNew("LinesNo") = ""
            drNew("LinesDesc") = "Select Line Number"
        End If
        dsList.Tables(0).Rows.InsertAt(drNew, 0)

        ddlLineNo.DataSource = dsList.Tables(0)
        ddlLineNo.DataValueField = "LinesNo"
        ddlLineNo.DataTextField = "LinesDesc"
        ddlLineNo.DataBind()
        ddlLineNo.SelectedIndex = intSelectedIndex

        If Not dsList Is Nothing Then
            dsList = Nothing
        End If
    End Sub


    Sub rbTransType_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadLabelText()
        If strLocType = objLoc.EnumLocType.Mill And Trim(lblSubBlkCode.Text) <> "" Then
            If rbTransTypeIssue.Checked = True Then
                BindItemToMachineDropDownList(Session("SS_LOCATION"), Trim(lblJobID.Text), "", Trim(lblSubBlkCode.Text), True)
            Else
                BindItemToMachineDropDownList(Session("SS_LOCATION"), Trim(lblJobID.Text), "", Trim(lblSubBlkCode.Text), False)
            End If
        Else
            If rbTransTypeIssue.Checked = True Then
                BindItemDropDownList(Session("SS_LOCATION"), Trim(lblJobID.Text), "", True)
            Else
                BindItemDropDownList(Session("SS_LOCATION"), Trim(lblJobID.Text), "", False)
            End If
        End If
        SetObjectVisibilityByItemType("", Session("SS_LOCATION"))
    End Sub
    
    Sub ddlItemCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dsTemp As New DataSet
        Dim drTemp As DataRow()
        dim strItemCode as String = ""
        dim arrItemCode as Array
        Dim arrParam As Array
        Dim strLineNo As String
        
        arrItemCode = Split(Trim(GetDropDownListValue(ddlItemCode)), ITEM_PART_SEPERATOR)
        
        If rbTransTypeIssue.Checked = True Then
            lblJobStockIssueID.Text = ""            
            lblItemCode.Text = Trim(arrItemCode(0))

            SetObjectVisibilityByItemType(lblItemCode.Text, Session("SS_LOCATION"))
        
            If strLocType = objLoc.EnumLocType.Mill Then
                If Trim(lblSubBlkCode.Text) <> "" Then
                    GetItemLineNo("",lblSubBlkCode.Text,lblItemCode.Text)
                End If
                ToggleChargeLevel() 
            End IF
        Else

            if UBound(arrItemCode) > 0 Then lblJobStockIssueID.Text = Trim(arrItemCode(1))
            dsTemp = GetJobStockLineDS()
            drTemp = dsTemp.Tables(0).Select("JobStockID = '" & FixSQL(lblJobStockIssueID.Text) & "'")            
            If drTemp.Length = 1 Then
                lblItemCode.Text = drTemp(0).Item("ItemCode")
            Else
                lblItemCode.Text = ""
            End If
            If strLocType = objLoc.EnumLocType.Mill Then
                If Not (Trim(lblSubBlkCode.Text) = "" And ddlItemCode.SelectedItem.Value = "") Then
                    arrParam = Split(ddlItemCode.SelectedItem.Text, ",")
                    strLineNo = arrParam(3)
                    GetItemLineNo(strLineNo, lblSubBlkCode.Text, lblItemCode.Text)
                End If
                ToggleChargeLevel()
            End If
            SetObjectVisibilityByItemType(lblItemCode.Text, Session("SS_LOCATION"))
        End If
       
    End Sub
    
    Sub ddlAccCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strAcc As String = Trim(GetDropDownListValue(ddlAccCode))
        Dim strPreBlk As String = Trim(GetDropDownListValue(ddlPreBlkCode))
        Dim strBlk As String = Trim(GetDropDownListValue(ddlBlkCode))
        Dim strVeh As String = Trim(GetDropDownListValue(ddlVehCode))
        Dim strVehExp As String = Trim(GetDropDownListValue(ddlVehExpCode))
        
        If strLocType = objLoc.EnumLocType.Mill Then
            Call BindAccountComponents(Session("SS_LOCATION"), strAcc, strPreBlk, lblSubBlkCode.Text, strVeh, strVehExp)
            ddlBlkCode.Enabled = False
        Else
            Call BindAccountComponents(Session("SS_LOCATION"), strAcc, strPreBlk, strBlk, strVeh, strVehExp)
        End If

    End Sub

    Sub ddlChargeLevel_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ToggleChargeLevel()
    End Sub


    Protected Function GetJobHeaderDS() As DataSet
        Dim dsTemp As New DataSet
        Dim colParam As New Collection
        Dim strSearch As String
        Dim strErrMsg As String
    
        Try
            strSearch = " WHERE J.JobID = '" & FixSQL(Trim(lblJobID.Text)) & "'"
            colParam.Add(strSearch, "PM_SEARCH")
            colParam.Add(strOpCode_Job_Get, "OC_JOB_GET")
            
            intErrNo = objWSTrx.mtdJob_Get(colParam, dsTemp, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New Exception(strErrMsg)
            End If
            
            Return dsTemp
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_GET_JOB_HEADER&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
    End Function
    
    Protected Function GetJobStockLineDS() As DataSet
        Dim dsTemp As New DataSet
        Dim colParam As New Collection
        Dim strSearch As String
        Dim strErrMsg As String
    
        Try
            strSearch = " WHERE JS.JobID = '" & FixSQL(Trim(lblJobID.Text)) & "'"
            colParam.Add(strSearch, "PM_SEARCH")
            colParam.Add(strOpCode_JobStock_Get, "OC_JOB_STOCK_GET")
            
            intErrNo = objWSTrx.mtdJobStock_Get(colParam, dsTemp, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New Exception(strErrMsg)
            End If
            
            Return dsTemp
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_GET_JOB_STOCK_LINE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
    End Function

    Function GetLanguageCaptionDS() As DataSet
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim dsTemp As New DataSet
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
                                                 dsTemp, _
                                                 strParam)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_GET_LANGCAP&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        Return dsTemp
        If Not dsTemp Is Nothing Then
            dsTemp = Nothing
        End If
    End Function

    Sub GetAccountProperties(ByVal pv_strAccCode As String, _
                             ByRef pr_strAccType As Integer, _
                             ByRef pr_strAccPurpose As Integer, _
                             ByRef pr_strNurseryInd As Integer)

        Dim dsTemp As New DataSet
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, strParam, dsTemp, True)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_GET_ACCOUNT_PROPERTY&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        If dsTemp.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(dsTemp.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(dsTemp.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(dsTemp.Tables(0).Rows(0).Item("NurseryInd"))
        End If
    End Sub
    
    Sub GetItemProperties(ByVal pv_strItemCode As String, _
                          ByVal pv_strLocCode As String, _
                          ByRef pr_strItemType As String, _
                          ByRef pr_decQtyOnHand As Decimal, _
                          ByRef pr_decQtyOnHold As Decimal, _
                          ByRef pr_decAverageCost As Decimal, _
                          ByRef pr_decDiffAverageCost As Decimal, _
                          ByRef pr_decPrice As Decimal, _
                          ByRef pr_blnError As Boolean)
        Dim dsTemp As New DataSet
        Dim strOpCd As String = "IN_CLSSETUP_INVITEM_LIST_GET_FOR_REPORT"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "|AND itm.ItemCode = '" & FixSQL(pv_strItemCode) & "' AND itm.LocCode = '" & FixSQL(pv_strLocCode) & "'"
        Try
            intErrNo = objINSetup.mtdGetMasterList(strOpCd, strParam, 1, dsTemp)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_GET_ITEM_PROPERTY&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        If dsTemp.Tables(0).Rows.Count = 1 Then
            pr_strItemType = Trim(dsTemp.Tables(0).Rows(0).Item("ItemType"))
            pr_decQtyOnHand = dsTemp.Tables(0).Rows(0).Item("QtyOnHand")
            pr_decQtyOnHold = dsTemp.Tables(0).Rows(0).Item("QtyOnHold")
            pr_decAverageCost = dsTemp.Tables(0).Rows(0).Item("AverageCost")
            pr_decDiffAverageCost = dsTemp.Tables(0).Rows(0).Item("DiffAverageCost")
            Select Case Trim(dsTemp.Tables(0).Rows(0).Item("UsePrice"))
                Case objINSetup.EnumSellingPriceForItem.Fixed
                    pr_decPrice = dsTemp.Tables(0).Rows(0).Item("SellFixedPrice")
                Case objINSetup.EnumSellingPriceForItem.PercentageOfAverageCost
                    pr_decPrice = Decimal.Multiply(Decimal.Add(Decimal.Divide(dsTemp.Tables(0).Rows(0).Item("SellAverageCost"), CDec(100)), CDec(1)), dsTemp.Tables(0).Rows(0).Item("AverageCost"))
                Case objINSetup.EnumSellingPriceForItem.PercentageOfLatestCost
                    pr_decPrice = Decimal.Multiply(Decimal.Add(Decimal.Divide(dsTemp.Tables(0).Rows(0).Item("SellAverageCost"), CDec(100)), CDec(1)), dsTemp.Tables(0).Rows(0).Item("LatestCost"))
                Case Else
                    pr_decPrice = pr_decAverageCost
            End Select
            pr_blnError = False
        Else
            pr_blnError = True
        End If
    End Sub
    
    Sub DisplayJobHeader()
        Dim dsHeader As DataSet
        dsHeader = GetJobHeaderDS()
        lblJobID.Text = Trim(dsHeader.Tables(0).Rows(0).Item("JobID"))
        lblJobType.Text = Trim(dsHeader.Tables(0).Rows(0).Item("JobType"))
        lblJobStatus.Text = Trim(dsHeader.Tables(0).Rows(0).Item("Status"))
        lblJobVehCode.Text = Trim(dsHeader.Tables(0).Rows(0).Item("VehCode"))
    End Sub
    
    Sub DisplayJobStockLines()
        Dim dsLines As DataSet
        Dim intCnt As Integer
        Dim decTotalCost As Decimal = 0
        Dim decTotalPrice As Decimal = 0
        dsLines = GetJobStockLineDS()
        For intCnt = 0 To dsLines.Tables(0).Rows.Count -1
            If Trim(dsLines.Tables(0).Rows(intCnt).Item("TransType")) = Trim(CStr(objWSTrx.EnumJobStockType.Issued)) Then
                decTotalCost = RoundNumber(Decimal.Add(decTotalCost, RoundNumber(dsLines.Tables(0).Rows(intCnt).Item("Amount"), 2)), 2)
                decTotalPrice = RoundNumber(Decimal.Add(decTotalPrice, RoundNumber(dsLines.Tables(0).Rows(intCnt).Item("PriceAmount"), 2)), 2)
            Else
                decTotalCost = RoundNumber(Decimal.Subtract(decTotalCost, RoundNumber(dsLines.Tables(0).Rows(intCnt).Item("Amount"), 2)), 2)
                decTotalPrice = RoundNumber(Decimal.Subtract(decTotalPrice, RoundNumber(dsLines.Tables(0).Rows(intCnt).Item("PriceAmount"), 2)), 2)
            End If
        Next

        lblTotalCost.Text = objGlobal.GetIDDecimalSeparator(RoundNumber(FormatNumber(decTotalCost, 2, True, False, False),0))
        lblTotalPrice.Text = objGlobal.GetIDDecimalSeparator(RoundNumber(FormatNumber(decTotalPrice, 2, True, False, False),0))
        dgJobStock.DataSource = dsLines
        dgJobStock.DataBind()
    End Sub


    Sub ibAdd_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim colParam As New Collection
        Dim strErrMsg As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim strJobStockID As String
        Dim strTransType As String
        Dim strTransDate As String = Trim(txtTransDate.Text)
        Dim strEmpCode As String = Trim(GetDropDownListValue(ddlEmpCode))
        Dim strWorkCode As String = Trim(GetDropDownListValue(ddlWorkCode))
        Dim strItemCode As String = Trim(lblItemCode.Text)
        Dim strAccCode As String = Trim(GetDropDownListValue(ddlAccCode))
        Dim strPreBlkCode As String = Trim(GetDropDownListValue(ddlPreBlkCode))
        Dim strBlkCode As String = Trim(GetDropDownListValue(ddlBlkCode))
        Dim strVehCode As String = Trim(GetDropDownListValue(ddlVehCode))
        Dim strVehExpCode As String = Trim(GetDropDownListValue(ddlVehExpCode))
        Dim strVehStkExpCode As String = Trim(GetDropDownListValue(ddlVehStkExpCode))
        Dim strQty As String = Trim(txtQty.Text)
        Dim strItemType As String = Trim(lblItemType.Text)
        Dim strJobID As String = Trim(lblJobID.Text)
        Dim strJobType As String = Trim(lblJobType.Text)
        Dim strJobStockIssueID As String = Trim(lblJobStockIssueID.Text)
        Dim strLinesNo As String = 0

        If strLocType = objLoc.EnumLocType.Mill And Trim(lblSubBlkCode.Text) <> "" Then
            strLinesNo = Trim(ddlLineNo.SelectedItem.Value)

            If strLinesNo = "" Then
                lblLineNoErr.Visible = True
                Exit Sub
            Else
                lblLineNoErr.Visible = False
            End If
        End If

        Dim blnBlockCharge As Boolean = False   

        If rbTransTypeIssue.Checked = True Then
            strTransType = Trim(CStr(objWSTrx.EnumJobStockType.Issued))
        Else
            strTransType = Trim(CStr(objWSTrx.EnumJobStockType.Returned))
        End If
        If strTransDate <> "" Then
            strTransDate = GetValidDate(strTransDate, strErrMsg)
            If strTransDate = "" Then
                lblTransDateErr.Text = strErrMsg
                lblTransDateErr.Visible = True
            End If
        Else
            lblTransDateErr.Text = strErrMsg
            lblTransDateErr.Visible = True
        End If
        If strEmpCode = "" Then
            lblEmpCodeErr.Visible = True
        End If
        If strWorkCode = "" Then
            lblWorkCodeErr.Visible = True
        End If
        If strItemCode = "" Then
            lblItemCodeErr.Visible = True
        End If
        If Trim(strItemType) = Trim(CStr(objWSTrx.EnumInventoryItemType.DirectCharge)) OR Trim(strItemType) = Trim(CStr(objWSTrx.EnumInventoryItemType.WorkshopItem)) Then
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
                    Select Case intAccPurpose
                        Case objGLSetup.EnumAccountPurpose.NonVehicle
                            If strBlkCode = "" Then
                                If ddlChargeLevel.SelectedIndex = 0 Then
                                    lblPreBlkCodeErr.Visible = True
                                Else
                                    lblBlkCodeErr.Visible = True
                                End If
                            End If
                            strVehCode = ""
                            strVehExpCode = ""
                        Case objGLSetup.EnumAccountPurpose.VehicleDistribution
                            strBlkCode = ""
                            If strVehCode = "" Then
                                lblVehCodeErr.Visible = True
                            End If
                            If strVehExpCode = "" Then
                                lblVehExpCodeErr.Visible = True
                            End If
                        Case Else
                            If ddlChargeLevel.SelectedIndex = 0 Then
                                lblPreBlkCodeErr.Visible = True
                            Else
                                lblBlkCodeErr.Visible = True
                            End If
                            If strVehCode = "" Then
                                lblVehCodeErr.Visible = True
                            End If
                            If strVehExpCode = "" Then
                                lblVehExpCodeErr.Visible = True
                            End If
                    End Select
                ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
                    If strBlkCode = "" Then
                        If ddlChargeLevel.SelectedIndex = 0 Then
                            lblPreBlkCodeErr.Visible = True
                        Else
                            lblBlkCodeErr.Visible = True
                        End If
                    End If
                    strVehCode = ""
                    strVehExpCode = ""
                Else
                    strBlkCode = ""
                    strVehCode = ""
                    strVehExpCode = ""
                End If
            End If
        Else
            strAccCode = ""
            strBlkCode = ""
            strVehCode = ""
            strVehExpCode = ""
        End If
        If Trim(lblJobVehCode.Text) <> "" Then
            If strVehStkExpCode = "" Then
                lblVehStkExpCodeErr.Visible = True
            End If
        End If
        If strQty = "" Then
            lblQtyErr.Text = lblQuantityTag.Text & " cannot be blank"
            lblQtyErr.Visible = True
        ElseIf IsNumeric(strQty) = False Then
            lblQtyErr.Text = lblQuantityTag.Text & " must be a number"
            lblQtyErr.Visible = True
        ElseIf CDec(strQty) <= 0 Then
            lblQtyErr.Text = lblQuantityTag.Text & " must be greater than zero"
            lblQtyErr.Visible = True
        End If

        If lblTransDateErr.Visible = True Or lblEmpCodeErr.Visible = True Or _
           lblWorkCodeErr.Visible = True Or lblItemCodeErr.Visible = True Or _
           lblAccCodeErr.Visible = True Or lblPreBlkCodeErr.Visible = True Or lblBlkCodeErr.Visible = True Or _
           lblVehCodeErr.Visible = True Or lblVehExpCodeErr.Visible = True Or _
           lblVehStkExpCodeErr.Visible = True Or lblQtyErr.Visible = True then
            Exit Sub
        End If
        
        colParam.Add(Trim(strLocation), "PM_LOCCODE")
        colParam.Add(Trim(strAccMonth), "PM_ACCMONTH")
        colParam.Add(Trim(strAccYear), "PM_ACCYEAR")
        colParam.Add(Trim(strUserId), "PM_UPDATEID")
        colParam.Add(strJobID, "PM_JOBID")
        colParam.Add(strJobType, "PM_JOBTYPE")
        colParam.Add(strJobStockIssueID, "PM_JOBSTOCKISSUEID")
        colParam.Add(strTransType, "PM_TRANSTYPE")
        colParam.Add(strTransDate, "PM_TRANSDATE")
        colParam.Add(strEmpCode, "PM_EMPCODE")
        colParam.Add(strWorkCode, "PM_WORKCODE")
        colParam.Add(strItemCode, "PM_ITEMCODE")
        colParam.Add(strItemType, "PM_ITEMTYPE")
        colParam.Add(strAccCode, "PM_ACCCODE")
        colParam.Add(strVehCode, "PM_VEHCODE")
        colParam.Add(strVehExpCode, "PM_VEHEXPENSECODE")
        colParam.Add(strVehStkExpCode, "PM_VEHSTKEXPCODE")
        colParam.Add(strQty, "PM_QTY")
        
        colParam.Add(strOpCode_JobStock_Add, "OC_JOB_STOCK_ADD")
        colParam.Add(strOpCode_JobStock_Get, "OC_JOB_STOCK_GET")
        colParam.Add(strOpCode_JobStock_Upd, "OC_JOB_STOCK_UPD")
        colParam.Add(strOpCode_Job_Upd, "OC_JOB_UPD")
        colParam.Add(strOpCode_Item_Get, "OC_ITEM_GET")
        colParam.Add(strOpCode_Item_Upd, "OC_ITEM_UPD")

        If strLocType = objLoc.EnumLocType.Mill And Trim(lblSubBlkCode.Text) <> "" Then
            colParam.Add(Trim(lblSubBlkCode.Text), "PM_BLKCODE")
        Else
            colParam.Add(strBlkCode, "PM_BLKCODE")        
        End If
        colParam.Add(strLinesNo, "PM_LINESNO")

        Try
            intErrNo = objWSTrx.mtdJobStock_Add(colParam, strJobStockID, strErrMsg)
            
            If intErrNo <> objWSTrx.EnumException.NoError Then
                lblActionResult.Text = "Cannot perform the operation. " & strErrMsg
                lblActionResult.Visible = True
            Else
                If strLocType = objLoc.EnumLocType.Estate Then
                    ResetPage()
                End If
            End If
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_ADD&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
        End Try

        If strLocType = objLoc.EnumLocType.Mill And Trim(lblSubBlkCode.Text) <> "" Then
            Dim strOpCd_GetBlock As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
            Dim strOpCd_GetDate As String = "IN_CLSTRX_ITEMTOMACHINE_LINE_GET_LASTDATE"
            Dim strOpCd_AddPM As String = "IN_CLSTRX_PREVENTIVE_ACTUAL_ADD"
            Dim objSubBlkDs As New Dataset()
            Dim strBlockCode As String 
            Dim strParam As String

            strParam = "|" & "And Sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "' And SubBlkCode = '" & Trim(lblSubBlkCode.Text) & "'" & "|" & strLocation
            Try
                intErrNo = objPRSetup.mtdGetMasterList(strOpCd_GetBlock, strParam, 0, objSubBlkDs)
     
            Catch ex As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
            End Try

            strBlockCode = objSubBlkDs.Tables(0).Rows(0).Item("BlockCode")

            If strTransType = Trim(CStr(objWSTrx.EnumJobStockType.Returned)) Then
                strParam = "|" & " IM.Status = '" & objINTrx.EnumItemToMachineStatus.Active & "' And IM.SubBlkCode = '" & Trim(lblSubBlkCode.Text) & "'" & _
                        " And IM.BlkCode = '" & Trim(strBlockCode) & "' And ItemCode = '" & Trim(strItemCode) & "' And LinesNo = '" & Trim(strLinesNo) & "'" & "|" & strLocation

                Try
                    intErrNo = objPRSetup.mtdGetMasterList(strOpCd_GetDate, strParam, 0, objSubBlkDs)
         
                Catch ex As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
                End Try

                strTransDate = objSubBlkDs.Tables(0).Rows(0).Item("LastInsDate")
            End If
                       
            strParam = Trim(strLocation) & "|" & Trim(strAccYear) & "|" & Trim(strBlockCode) & "|" & Trim(lblSubBlkCode.Text) & "|" & _
                                                Trim(strItemCode) & "|" & Trim(strLinesNo) & "||" & strTransDate & "|" & strTransType
                                                
            Try
                intErrNo = objINTrx.mtdUpdPreventiveMaintenance(strOpCd_AddPM, _
                                            strCompany, _
                                            strLocation, _
                                            strAccYear, _
                                            strUserId, _
                                            strParam, _
                                            False)

            If intErrNo <> objWSTrx.EnumException.NoError Then
                lblActionResult.Text = "Cannot perform the operation. " & strErrMsg
                lblActionResult.Visible = True
            Else
                ResetPage()
            End If

            Catch ex As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_PREVENTIVE_ACTUAL_ADD&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
            End Try
        End If   
    End Sub

    Sub ibBack_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
    End Sub

    Protected Function FixSQL(ByVal pv_strParam As String) As String
        Return Replace(Trim(pv_strParam), "'", "''")
    End Function
    
    Protected Function RoundNumber(ByVal d As Double, Optional ByVal decimals As Double = 0) As Decimal
        RoundNumber = Decimal.Divide(Int(Decimal.Add(Decimal.Multiply(d, 10.0 ^ decimals), 0.5)), 10.0 ^ decimals)
    End Function
    
    Protected Function GetDropDownListValue(ByRef pr_ddlObject As DropDownList) As String
        If Trim(Request.Form(pr_ddlObject.ID)) <> "" Then
            GetDropDownListValue = Trim(Request.Form(pr_ddlObject.ID))
        Else
            GetDropDownListValue = pr_ddlObject.SelectedItem.Value
        End If
    End Function
    
    Protected Function GetValidDate(ByVal pv_strInputDate As String, ByRef pr_strErrMsg As String) As String
        Dim strDateFormat As String
        Dim strSQLDate As String

        If objGlobal.mtdValidInputDate(Session("SS_DATEFMT"), _
                                       pv_strInputDate, _
                                       strDateFormat, _
                                       strSQLDate) = True Then
            GetValidDate = strSQLDate
            pr_strErrMsg = ""
        Else
            GetValidDate = ""
            pr_strErrMsg = "Date format should be in " & strDateFormat
        End If
    End Function
    
    Protected Function ComposeDate(ByVal pv_strLabelDate As String, ByVal pv_strLabelTimeHour As String, pv_strLabelTimeMinute As String, ByRef pr_txtDate As TextBox, ByRef pr_txtTimeHour As TextBox, ByRef pr_txtTimeMinute As TextBox, ByRef pr_ddlDayNight As DropDownList, ByRef pr_strDate As String, ByRef pr_strErrMsg As String) As Boolean
        Dim strDate As String = Trim(pr_txtDate.Text)
        Dim strTimeHour As String = Trim(pr_txtTimeHour.Text)
        Dim strTimeMinute As String = Trim(pr_txtTimeMinute.Text)
        Dim strDayNight As String = GetDropDownListValue(pr_ddlDayNight)
        Dim strValidDate As String
        Dim strErrMsg As String
        
        pr_strDate = ""
        ComposeDate = False
        If strDate = "" And strTimeHour = "" And strTimeMinute = "" Then
            ComposeDate = True
        ElseIf strTimeHour <> "" And strTimeMinute = "" Then
            pr_strErrMsg = pv_strLabelTimeMinute & " cannot be blank if " & pv_strLabelTimeHour & " is not blank"
        ElseIf strTimeHour = "" And strTimeMinute <> "" Then
            pr_strErrMsg = pv_strLabelTimeHour & " cannot be blank if " & pv_strLabelTimeMinute & " is not blank"
        ElseIf strDate <> "" And strTimeHour & strTimeMinute = "" Then
            pr_strErrMsg = pv_strLabelTimeHour & " and " & pv_strLabelTimeHour & " cannot be blank if " & pv_strLabelDate & " is not blank"
        ElseIf strDate = "" And strTimeHour & strTimeMinute <> "" Then
            pr_strErrMsg = pv_strLabelDate & " cannot be blank if " & pv_strLabelTimeHour & " and " & pv_strLabelTimeMinute & " is not blank"
        ElseIf IsNumeric(strTimeHour) = False Then
            pr_strErrMsg = pv_strLabelTimeHour & " must be an integer between 1 and 12"
        ElseIf IsNumeric(strTimeMinute) = False Then
            pr_strErrMsg = pv_strLabelTimeMinute & " must be an integer between 0 and 59"
        ElseIf CInt(strTimeHour) < 1 Or CInt(strTimeHour) > 12 Then
            pr_strErrMsg = pv_strLabelTimeHour & " must be an integer between 1 and 12"
        ElseIf CInt(strTimeMinute) < 0 Or CInt(strTimeMinute) > 59 Then
            pr_strErrMsg = pv_strLabelTimeMinute & " must be an integer between 0 and 59"
        Else
            strValidDate = GetValidDate(strDate, strErrMsg)
            If strValidDate = "" Then
                pr_strErrMsg = strErrMsg
            Else
                pr_strDate = strValidDate & " " & strTimeHour & ":" & Right("00" & strTimeMinute, 2) & ":00 " & strDayNight
                ComposeDate = True
            End If 
        End If
    End Function


End Class
