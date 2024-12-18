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

Public Class NU_trx_DoubleTurnDetails : Inherits Page

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
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents ddlBlkCode As DropDownList
    Protected WithEvents ddlBatchNo As DropDownList
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtQty As TextBox

    Protected WithEvents txtAddNote As HtmlTextArea

    Protected WithEvents Delete As ImageButton
    Protected WithEvents Confirm As ImageButton
    Protected WithEvents btnSelDateFrom As Image

    Dim strParam As String
    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intNUAR As Integer
    Dim strDateFormat As String
    Dim intConfigSetting As Integer
    Dim strAcceptFormat As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer

    Dim strOppCd_GET As String = "NU_CLSTRX_DOUBLETURN_GET"
    Dim strOppCd_ADD As String = "NU_CLSTRX_DOUBLETURN_ADD"
    Dim strOppCd_UPD As String = "NU_CLSTRX_DOUBLETURN_UPD"
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
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUDblTurn), intNUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblBlkCodeErr.Visible = False
            lblBatchNoErr.Visible = False

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
                    If lblStatus.Text = objNUTrx.mtdGetDoubleTurnStatus(objNUTrx.EnumDoubleTurnStatus.Deleted) Then
                        Delete.Visible = False
                    ElseIf lblStatus.Text = objNUTrx.mtdGetDoubleTurnStatus(objNUTrx.EnumDoubleTurnStatus.Closed) Then
                        Delete.Visible = False
                    Else
                        Delete.Visible = True
                        Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    End If
                Else
                    BindBlkCode("")
                    BindBatchNo("", "")
                    lblOper.Text = objNUTrx.EnumOperation.Add
                    Delete.Visible = False
                End If
            End If
            lblDeleteErr.Visible = False
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        'If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
        lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.NurseryBlock) & lblCode.Text
        lblBlkCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.NurseryBlock) & "."
        'Else
        '    lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.NurserySubBlock) & lblCode.Text
        '    lblBlkCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.NurserySubBlock) & "."
        'End If

        lblBatchNoTag.Text = GetCaption(objLangCap.EnumLangCap.BatchNo)
        lblBatchNoErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.BatchNo) & "."
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_DOUBLETURN_DET_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=NU/trx/NU_trx_DoubleTurnDetails.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = objLangCapDs.Tables(0).Rows(count).Item("TermCode") Then
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

        strParam = lblTxID.Text & "||||||"

        Try
            intErrNo = objNUTrx.mtdGetDoubleTurn(strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strOppCd_GET, _
                                                strParam, _
                                                objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_DOUBLETURN_DET_GET&errmesg=" & Exp.ToString() & "&redirect=NU/trx/NU_trx_DoubleTurnList.aspx")
        End Try

        Return objDataSet
    End Function


    Sub DisableControl()
        Dim strView As Boolean

        strView = False
        ddlBlkCode.Enabled = strView
        ddlBatchNo.Enabled = strView
        txtDate.Enabled = strView
        btnSelDateFrom.Visible = strView
        txtQty.Enabled = strView
        Confirm.Visible = strView
        Delete.Visible = strView
        txtAddNote.Disabled = True
    End Sub

    Sub DisplayData()

        Dim dsTx As DataSet = LoadData()

        If dsTx.Tables(0).Rows.Count > 0 Then
            lblTxID.Text = Trim(dsTx.Tables(0).Rows(0).Item("DoubleTurnID"))
            txtDate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsTx.Tables(0).Rows(0).Item("DoubleTurnDate")))
            txtQty.Text = dsTx.Tables(0).Rows(0).Item("Qty")

            lblAccPeriod.Text = dsTx.Tables(0).Rows(0).Item("AccMonth") & "/" & dsTx.Tables(0).Rows(0).Item("AccYear")
            lblStatus.Text = objNUTrx.mtdGetDoubleTurnStatus(dsTx.Tables(0).Rows(0).Item("Status"))
            lblCreateDate.Text = objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdateBy.Text = dsTx.Tables(0).Rows(0).Item("Username")

            BindBlkCode(dsTx.Tables(0).Rows(0).Item("BlkCode").Trim())
            BindBatchNo(dsTx.Tables(0).Rows(0).Item("BlkCode").Trim(), dsTx.Tables(0).Rows(0).Item("BatchNo"))

            txtAddNote.Value = Trim(dsTx.Tables(0).Rows(0).Item("AdditionalNote"))
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

        'remark as new FS, all refer to Thn.Tanam (06022014)
        'If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
        SearchStr = " AND Blk.Status = '" & objGLSetup.EnumBlockStatus.Active & "'" & _
                    " AND Blk.BlkType = '" & objGLSetup.EnumBlockType.Nursery & "'" & _
                    " AND BLK.LocCode = '" & strLocation & "'"
        sortitem = "ORDER BY Blk.BlkCode ASC "
        strOpCode = "GL_CLSSETUP_BLOCK_LIST_GET"
        'Else
        '    SearchStr = " AND sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "'" & _
        '                " AND sub.SubBlkType = '" & objGLSetup.EnumSubBlockType.Nursery & "'" & _
        '                " AND sub.LocCode = '" & strLocation & "'"
        '    sortitem = "ORDER BY sub.SubBlkCode ASC "
        '    strOpCode = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        'End If

        strParam = sortitem & "|" & SearchStr
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, strParam, objGLSetup.EnumGLMasterType.Block, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_DOUBLETURN_DET_BLOCK&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_trx_DoubleTurnDetails.aspx")
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
                sortitem = "ORDER BY Blk.BlkCode ASC "
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_DOUBLETURN_DET_BLOCK_2&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_trx_DoubleTurnDetails.aspx")
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_DOUBLETURN_DET_BATCHNO&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_trx_DoubleTurnDetails.aspx")
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
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_DOUBLETURN_DET_BATCHNO_2N&errmesg=" & Exp.ToString() & "&redirect=NU/Trx/NU_trx_DoubleTurnDetails.aspx")
                End Try
            End If

            ddlBatchNo.SelectedIndex = intSelectedIndex
        Else
            ddlBatchNo.Items.Add(New ListItem(" "))
        End If
    End Sub


    Sub UpdateData(ByVal strAction As String)
        Dim blnDupKey As Boolean = False
        Dim strStatus As String
        Dim TxID As String
        Dim blnDeleteErr As Boolean
        Dim intError As Integer
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

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If Not strDate = "" Then
            Select Case strAction
                Case "Confirm"
                    strStatus = objNUTrx.EnumDoubleTurnStatus.Confirmed
                Case "Delete"
                    strStatus = objNUTrx.EnumDoubleTurnStatus.Deleted
            End Select

            strNewIDFormat = "SDT" & "/" & strCompany & "/" & strLocation & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

            strParam = lblTxID.Text & "|" & _
                        ddlBlkCode.SelectedItem.Value & "|" & _
                        ddlBatchNo.SelectedItem.Value & "|" & _
                        strDate & "|" & _
                        txtQty.Text & "|" & _
                        Trim(txtAddNote.Value) & "|" & _
                        Trim(strNewIDFormat)

            Try
                intErrNo = objNUTrx.mtdUpdDoubleTurn(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    intError, _
                                                    lblOper.Text, _
                                                    objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.DoubleTurn), _
                                                    TxID, _
                                                    blnDeleteErr, _
                                                    intConfigSetting)

                If blnDeleteErr = True Then
                    lblDeleteErr.Visible = True
                Else
                    lblDeleteErr.Visible = False
                End If


            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_DOUBLETURNDETAILS_UPDATE&errmesg=" & Exp.ToString() & "&redirect=NU/trx/NU_trx_DoubleTurnDetails.aspx")
            End Try

            If intError = objNUTrx.EnumErrorType.DuplicateKey Then
                lblDupMsg.Visible = True
            Else
                If blnDeleteErr = False Then
                    Response.Redirect("NU_trx_DoubleTurnList.aspx")
                End If
            End If
        End If

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
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_GRList.aspx")
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

    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If ddlBlkCode.SelectedItem.Value = "" Then
            lblBlkCodeErr.Visible = True
            Exit Sub
        End If
        If ddlBatchNo.SelectedItem.Value = "" Then
            lblBatchNoErr.Visible = True
            Exit Sub
        End If
        UpdateData("Confirm")
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Delete")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("NU_trx_DoubleTurnList.aspx")
    End Sub

    Sub CallFillBatchNo(ByVal sender As Object, ByVal e As System.EventArgs)
        BindBatchNo(ddlBlkCode.SelectedItem.Value, "")
    End Sub


End Class
