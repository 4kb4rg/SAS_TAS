Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Globalization

Public Class NU_trx_SeedDispatchDetails : Inherits Page

    Dim objNUTrx As New agri.NU.clsTrx()
    Dim objNUSetup As New agri.NU.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblOper As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblTxIDTag As Label
    Protected WithEvents lblTxID As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblBlkCodeTag As Label
    Protected WithEvents lblBlkCodeErr As Label
    Protected WithEvents lblDeleteErr As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblBatchNoTag As Label
    Protected WithEvents lblBatchNoErr As Label
    Protected WithEvents lblBillPartyCodeTag As Label
    Protected WithEvents lblBillPartyCodeErr As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents ddlBlkCode As DropDownList
    Protected WithEvents ddlBatchNo As DropDownList
    Protected WithEvents ddlBillPartyCode As DropDownList
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtQty As TextBox
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents txtVehNo As TextBox
    Protected WithEvents txtDocRefNo As TextBox
    Protected WithEvents Delete As ImageButton
    Protected WithEvents Confirm As ImageButton
    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents ddlAccCode As DropDownList

    Dim strParam As String
    Dim intErrNo As Integer
    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intNUAR As Integer
    Dim strDateFormat As String
    Dim intConfigSetting As Integer

    Dim strOppCd_GET As String = "NU_CLSTRX_SEEDDISPATCH_GET"
    Dim strOppCd_ADD As String = "NU_CLSTRX_SEEDDISPATCH_ADD"
    Dim strOppCd_UPD As String = "NU_CLSTRX_SEEDDISPATCH_UPD"
    Dim strOppCd_BlkAccList_Get As String = "GL_CLSSETUP_BLOCK_ACCOUNTLINE_GET"
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_NUACCMONTH")
        strAccYear = Session("SS_NUACCYEAR")
        intNUAR = Session("SS_NUAR")
        strDateFormat = Session("SS_DATEFMT")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDispatch), intNUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblBlkCodeErr.Visible = False
            lblErrAccCode.Visible = False
            lblBatchNoErr.Visible = False
            lblBillPartyCodeErr.Visible = False

            onload_GetLangCap()

            If Not Page.IsPostBack Then
                txtDate.Text = objGlobal.GetShortDate(strDateFormat, Now)
                If Not Request.QueryString("Id") = "" Then
                    lblTxID.Text = Request.QueryString("Id")
                    ViewState.Item("Id") = Request.QueryString("Id")
                End If

                If Not lblTxID.Text = "" Then
                    DisplayData()
                    lblOper.Text = objNUTrx.EnumOperation.Delete
                    DisableControl()
                    If lblStatus.Text = objNUTrx.mtdGetSeedDispatchStatus(objNUTrx.EnumSeedDispatchStatus.Deleted) Then
                        Delete.Visible = False
                    ElseIf lblStatus.Text = objNUTrx.mtdGetSeedDispatchStatus(objNUTrx.EnumSeedDispatchStatus.Closed) Then
                        Delete.Visible = False
                    Else
                        Delete.Visible = True
                        Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    End If
                Else
                    BindBlkCode("")
                    BindAccCode("") 
                    BindBatchNo("", "")
                    BindBillPartyCode("")
                    lblOper.Text = objNUTrx.EnumOperation.Add
                    Delete.Visible = False
                End If
            End If
            lblDeleteErr.Visible = False
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.NurseryBlock) & lblCode.Text
            lblBlkCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.NurseryBlock) & "."
        Else
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.NurserySubBlock) & lblCode.Text
            lblBlkCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.NurserySubBlock) & "."
        End If
        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text  
        lblErrAccCode.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.Account) & "."  
        lblBatchNoTag.Text = GetCaption(objLangCap.EnumLangCap.BatchNo)
        lblBillPartyCodeTag.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & lblCode.Text
        lblBatchNoErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.BatchNo) & "."
        lblBillPartyCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.BillParty) & "."
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDDISPATCH_DET_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=NU/trx/NU_trx_SeedDispatchDetails.aspx")
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



    Protected Function LoadData() As DataSet

        strParam = lblTxID.Text & "|||||||||"

        Try
            intErrNo = objNUTrx.mtdGetSeedDispatch(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strOppCd_GET, _
                                                    strParam, _
                                                    objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDDISPATCH_DET_GET&errmesg=" & Exp.ToString() & "&redirect=NU/trx/NU_trx_SeedDispatchList.aspx")
        End Try

        Return objDataSet
    End Function


    Sub DisableControl()
        Dim strView As Boolean

        strView = False
        txtDocRefNo.Enabled = strView
        ddlBlkCode.Enabled = strView
        ddlAccCode.Enabled = strView
        ddlBatchNo.Enabled = strView
        ddlBillPartyCode.Enabled = strView
        txtDate.Enabled = strView
        btnSelDateFrom.Visible = strView
        txtVehNo.Enabled = strView
        txtQty.Enabled = strView
        txtAmount.Enabled = strView
        Confirm.Visible = strView
        Delete.Visible = strView
    End Sub

    Sub DisplayData()

        Dim dsTx As DataSet = LoadData()

        If dsTx.Tables(0).Rows.Count > 0 Then
            lblTxID.Text = dsTx.Tables(0).Rows(0).Item("DispatchID").Trim()
            txtDocRefNo.Text = dsTx.Tables(0).Rows(0).Item("DocRefNo").Trim()
            txtVehNo.Text = dsTx.Tables(0).Rows(0).Item("VehNo").Trim()
            txtDate.Text = objGlobal.GetShortDate(strDateFormat, dsTx.Tables(0).Rows(0).Item("DispatchDate"))
            txtQty.Text = dsTx.Tables(0).Rows(0).Item("Qty")
            txtAmount.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(dsTx.Tables(0).Rows(0).Item("Amount"), 0))

            lblAccPeriod.Text = dsTx.Tables(0).Rows(0).Item("AccMonth") & "/" & dsTx.Tables(0).Rows(0).Item("AccYear")
            lblStatus.Text = objNUTrx.mtdGetSeedDispatchStatus(dsTx.Tables(0).Rows(0).Item("Status"))
            lblCreateDate.Text = objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdateBy.Text = dsTx.Tables(0).Rows(0).Item("Username")

            BindBlkCode(dsTx.Tables(0).Rows(0).Item("BlkCode").Trim())
            BindAccCode(dsTx.Tables(0).Rows(0).Item("BlkCode").Trim()) 
            BindBatchNo(dsTx.Tables(0).Rows(0).Item("BlkCode").Trim(), dsTx.Tables(0).Rows(0).Item("BatchNo"))
            BindBillPartyCode(dsTx.Tables(0).Rows(0).Item("BillPartyCode").Trim())
        End If
    End Sub

    Sub BindBlkCode(ByVal pv_strBlkCode As String)
        Dim strOpCode As String
        Dim strParam As String
        Dim dsForDropDown As DataSet
        Dim dsForInactiveItem As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim dr As DataRow
        Dim SearchStr As String
        Dim sortitem As String

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
            SearchStr = " AND Blk.Status = '" & objGLSetup.EnumBlockStatus.Active & "'" & _
                        " AND Blk.BlkType = '" & objGLSetup.EnumBlockType.Nursery & "'" & _
                        " AND BLK.LocCode = '" & strLocation & "'"
            sortitem = "ORDER BY Blk.BlkCode ASC "
            strOpCode = "GL_CLSSETUP_BLOCK_LIST_GET"
        Else
            SearchStr = " AND sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "'" & _
                        " AND sub.SubBlkType = '" & objGLSetup.EnumSubBlockType.Nursery & "'" & _
                        " AND sub.LocCode = '" & strLocation & "'"

            sortitem = "ORDER BY sub.SubBlkCode ASC "
            strOpCode = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        End If

        strParam = sortitem & "|" & SearchStr
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.Block, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDDISPATCH_DET_BIND_BLOCK_DROPDOWNLIST&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_trx_SeedDispatchDetails.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode").Trim() & " (" & dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Select " & lblBlkCodeTag.Text
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlkCode.DataSource = dsForDropDown.Tables(0)
        ddlBlkCode.DataValueField = "BlkCode"
        ddlBlkCode.DataTextField = "Description"
        ddlBlkCode.DataBind()

        If intSelectedIndex = 0 And Not lblTxID.Text = "" Then
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                SearchStr = " AND Blk.BlkCode = '" & pv_strBlkCode & "'" & _
                            " AND Blk.BlkType = '" & objGLSetup.EnumBlockType.Nursery & "'" & _
                            " AND BLK.LocCode = '" & strLocation & "'"
                sortitem = "ORDER BY Blk.BlkCode ASC "
            Else
                SearchStr = " AND sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "'" & _
                            " AND sub.SubBlkType = '" & objGLSetup.EnumSubBlockType.Nursery & "'" & _
                            " AND sub.LocCode = '" & strLocation & "'"
                sortitem = "ORDER BY sub.SubBlkCode ASC "
            End If

            strParam = sortitem & "|" & SearchStr
            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.Block, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                    ddlBlkCode.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("BlkCode")) & _
                     " (" & objGLSetup.mtdGetBlockStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item("BlkCode"))))
                    intSelectedIndex = ddlBlkCode.Items.Count - 1
                Else 
                    intSelectedIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDDISPATCH_DET_BIND_BLOCK_DROPDOWNLIST_2&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_trx_SeedDispatchDetails.aspx")
            End Try
        End If

        ddlBlkCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindBatchNo(ByVal pv_strBlkCode As String, ByVal pv_strBatchNo As String)
        Dim strOpCode As String = "NU_CLSSETUP_NURSERYBATCH_LIST_GET"
        Dim strParam As String
        Dim dsForDropDown As DataSet
        Dim dsForInactiveItem As DataSet
        Dim intSelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim dr As DataRow
        Dim SearchStr As String
        Dim sortitem As String

        If Not pv_strBlkCode = "" Then
            strParam = pv_strBlkCode & "|" & _
                        pv_strBatchNo & "|" & _
                        objNUSetup.EnumNurseryBatchStatus.Active & "||" & _
                        strLocation & "|" & _
                        "NB.BatchNo" & "|"

            Try
                intErrNo = objNUSetup.mtdGetNurseryBatch(strOpCode, strParam, dsForDropDown)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDDISPATCH_DET_BIND_BATCHNO_DROPDOWNLIST&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_trx_SeedDispatchDetails.aspx")
            End Try

            For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
                If Not pv_strBatchNo = "" Then
                    If dsForDropDown.Tables(0).Rows(intCnt).Item("BatchNo") = pv_strBatchNo Then
                        intSelectedIndex = intCnt
                    End If
                End If
            Next intCnt

            ddlBatchNo.DataSource = dsForDropDown.Tables(0)
            ddlBatchNo.DataValueField = "BatchNo"
            ddlBatchNo.DataTextField = "BatchNo"
            ddlBatchNo.DataBind()

            If intSelectedIndex = -1 And Not lblTxID.Text = "" Then
                strParam = pv_strBlkCode & "|" & _
                            pv_strBatchNo & "|||" & _
                            strLocation & "|" & _
                            "NB.BatchNo" & "|"

                Try
                    intErrNo = objNUSetup.mtdGetNurseryBatch(strOpCode, strParam, dsForInactiveItem)
                    If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                        ddlBatchNo.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("BatchNo")) & _
                         " (" & objNUSetup.mtdGetNurseryBatchStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item("BatchNo"))))
                        intSelectedIndex = ddlBatchNo.Items.Count - 1
                    Else 
                        intSelectedIndex = 0
                    End If

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDDISPATCH_DET_BIND_BATCHNO_DROPDOWNLIST_2&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_trx_SeedDispatchDetails.aspx")
                End Try
            End If

            ddlBatchNo.SelectedIndex = intSelectedIndex
        Else
            ddlBatchNo.Items.Add(New ListItem(" "))
        End If
    End Sub

    Sub BindAccCode(ByVal pv_strBlkCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_BLOCK_ACCOUNTLINE_GET"
        Dim strParam As String
        Dim dsBlkAccCode As DataSet
        Dim intSelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim DataTextField As String

        strParam = pv_strBlkCode & "|" & objGLSetup.EnumAccStatus.Active & "||"
        Try
            intErrNo = objGLSetup.mtdGetBlock(strOpCode, _
                                                strLocation, _
                                                strParam, _
                                                dsBlkAccCode, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCKLINE_GET&errmesg=" & Exp.ToString() & "&redirect=gl/setup/gl_setup_block.aspx")
        End Try

        For intCnt = 0 To dsBlkAccCode.Tables(0).Rows.Count - 1
            dsBlkAccCode.Tables(0).Rows(intCnt).Item("AccCode") = dsBlkAccCode.Tables(0).Rows(intCnt).Item("AccCode").Trim()
            dsBlkAccCode.Tables(0).Rows(intCnt).Item("Description") = dsBlkAccCode.Tables(0).Rows(intCnt).Item("AccCode").Trim() & " (" & dsBlkAccCode.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If dsBlkAccCode.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        ddlAccCode.DataSource = dsBlkAccCode.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataBind()
    End Sub

    Sub BindBillPartyCode(ByVal pv_strBillPartyCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strParam As String
        Dim dsForDropDown As DataSet
        Dim dsForInactiveItem As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim dr As DataRow
        Dim SearchStr As String
        Dim sortitem As String

        strParam = "||" & objGLSetup.EnumBillPartyStatus.Active & "||" & _
                    "BillPartyCode" & "||"

        Try
            intErrNo = objGLSetup.mtdGetBillParty(strOpCode, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDDISPATCH_DET_BIND_BILLPARTY_DROPDOWNLIST&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_trx_SeedDispatchDetails.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("BillPartyCode") = dsForDropDown.Tables(0).Rows(intCnt).Item("BillPartyCode").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Name") = dsForDropDown.Tables(0).Rows(intCnt).Item("BillPartyCode").Trim() & " (" & dsForDropDown.Tables(0).Rows(intCnt).Item("Name").Trim() & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("BillPartyCode") = Trim(pv_strBillPartyCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BillPartyCode") = ""
        dr("Name") = "Select " & lblBillPartyCodeTag.Text
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlBillPartyCode.DataSource = dsForDropDown.Tables(0)
        ddlBillPartyCode.DataValueField = "BillPartyCode"
        ddlBillPartyCode.DataTextField = "Name"
        ddlBillPartyCode.DataBind()

        If intSelectedIndex = 0 And Not lblTxID.Text = "" Then
            strParam = pv_strBillPartyCode & "||||" & _
                        "BillPartyCode" & "||"

            Try
                intErrNo = objGLSetup.mtdGetBillParty(strOpCode, strParam, dsForInactiveItem)
                If dsForInactiveItem.Tables(0).Rows.Count > 0 Then  
                    ddlBillPartyCode.Items.Add(New ListItem(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("BillPartyCode")) & _
                     " (" & objGLSetup.mtdGetBillPartyStatus(Trim(dsForInactiveItem.Tables(0).Rows(0).Item("Status"))) & ") ", Trim(dsForInactiveItem.Tables(0).Rows(0).Item("BillPartyCode"))))
                    intSelectedIndex = ddlBillPartyCode.Items.Count - 1
                Else 
                    intSelectedIndex = 0
                End If

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDDISPATCH_DET_BIND_BILLPARTY_DROPDOWNLIST_2&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_trx_SeedDispatchDetails.aspx")
            End Try
        End If

        ddlBillPartyCode.SelectedIndex = intSelectedIndex
    End Sub


    Sub UpdateData(ByVal strAction As String)
        Dim blnDupKey As Boolean = False
        Dim strStatus As String
        Dim strDate As String = CheckDate()
        Dim TxID As String
        Dim blnDeleteErr As Boolean
        Dim intError As Integer

        If Not strDate = "" Then
            Select Case strAction
                Case "Confirm"
                    strStatus = objNUTrx.EnumSeedDispatchStatus.Confirmed
                Case "Delete"
                    strStatus = objNUTrx.EnumSeedDispatchStatus.Deleted
            End Select

            strParam = lblTxID.Text & "|" & _
                        txtDocRefNo.Text & "|" & _
                        strDate & "|" & _
                        ddlBlkCode.SelectedItem.Value & "|" & _
                        ddlBatchNo.SelectedItem.Value & "|" & _
                        ddlBillPartyCode.SelectedItem.Value & "|" & _
                        txtVehNo.Text & "|" & _
                        txtQty.Text & "|" & _
                        IIF(txtAmount.Text<>"",txtAmount.Text,"0") & "|" & _
                        ddlAccCode.SelectedItem.Value
            Try
                intErrNo = objNUTrx.mtdUpdSeedDispatch(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    intError, _
                                                    lblOper.Text, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.SeedDispatch), _
                                                    TxID, _
                                                    blnDeleteErr, _
                                                    intConfigSetting)
                If blnDeleteErr = True Then
                    lblDeleteErr.Visible = True
                Else
                    lblDeleteErr.Visible = False
                End If
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDDISPATCHDETAILS_UPDATE&errmesg=" & Exp.ToString() & "&redirect=NU/trx/NU_trx_SeedDispatchDetails.aspx")
            End Try

            If intError = objNUTrx.EnumErrorType.DuplicateKey Then
                lblDupMsg.Visible = True
            Else
                If blnDeleteErr = False Then
                    Response.Redirect("NU_trx_SeedDispatchList.aspx")
                End If
            End If
        End If

    End Sub

    Protected Function CheckDate() As String
        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        If Not txtDate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, txtDate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Text = objDateFormat & "."
                lblDate.Visible = True
                lblFmt.Visible = True
            End If
        End If

    End Function

    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If ddlBlkCode.SelectedItem.Value = "" Then
            lblBlkCodeErr.Visible = True
            Exit Sub
        End If
        If ddlAccCode.SelectedItem.Value = "" Then
            lblErrAccCode.Visible = True
            Exit Sub
        End If
        If ddlBatchNo.SelectedItem.Value = "" Then
            lblBatchNoErr.Visible = True
            Exit Sub
        End If
        If ddlBillPartyCode.SelectedItem.Value = "" Then
            lblBillPartyCodeErr.Visible = True
            Exit Sub
        End If
        UpdateData("Confirm")
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Delete")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("NU_trx_SeedDispatchList.aspx")
    End Sub

    Sub CallFillData(ByVal sender As Object, ByVal e As System.EventArgs)
        BindBatchNo(ddlBlkCode.SelectedItem.Value, "")
        BindAccCode(ddlBlkCode.SelectedItem.Value)
    End Sub

End Class
