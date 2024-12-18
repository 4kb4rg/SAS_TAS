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

Public Class NU_trx_SeedReceiveDetails : Inherits Page

    Dim objOk As New agri.GL.ClsTrx()
    Dim objNUTrx As New agri.NU.clsTrx()
    Dim objNUSetup As New agri.NU.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
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
    Protected WithEvents txtPlantMat As TextBox
    Protected WithEvents txtDORef As TextBox
	
	Protected WithEvents ddlTxID As DropDownList
	Protected WithEvents lblBlkCode As Label
	Protected WithEvents lblBatchNo As Label

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

    Dim strOppCd_GET As String = "NU_CLSTRX_SEEDRECEIVE_GET"
    Dim strOppCd_ADD As String = "NU_CLSTRX_SEEDRECEIVE_ADD"
    Dim strOppCd_UPD As String = "NU_CLSTRX_SEEDRECEIVE_UPD"
    Dim strLocType as String

    Protected WithEvents lblCostSeedsTag As Label
    Protected WithEvents lblCostSeeds As Label
    Dim strOpCdStckTxLine_CostGET As String = "NU_CLSTRX_STOCKISSUE_LINE_COSTGET"
    Dim strOpCdStckTxLine_QtyGET As String = "NU_CLSTRX_STOCKISSUE_LINE_QTYGET"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        intNUAR = Session("SS_NUAR")
        strDateFormat = Session("SS_DATEFMT")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUSeedRcv), intNUAR) = False Then
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
					lblTxID.visible = True
					ddlTxID.visible = False
                    DisableControl()
                    If lblStatus.Text = objNUTrx.mtdGetSeedReceiveStatus(objNUTrx.EnumSeedReceiveStatus.Deleted) Then
                        Delete.Visible = False
                    ElseIf lblStatus.Text = objNUTrx.mtdGetSeedReceiveStatus(objNUTrx.EnumSeedReceiveStatus.Closed) Then
                        Delete.Visible = False
                    Else
                        Delete.Visible = True
                        Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    End If
                Else
				    lblTxID.visible = False
					ddlTxID.visible = True
				    BindSR() 
                    'BindBlkCode("")
                    'BindBatchNo("", "")
                    'lblOper.Text = objNUTrx.EnumOperation.Add
                    Delete.Visible = False
                End If
            End If
            lblDeleteErr.Visible = False
        End If
    End Sub

	Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object()
        Dim objActualDate As New Object()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=pu/trx/pu_GRList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

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
	
    Sub onload_GetLangCap()
        GetEntireLangCap()

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.NurseryBlock) & lblCode.Text
            lblBlkCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.NurseryBlock) & "."
        Else
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.NurserySubBlock) & lblCode.Text
            lblBlkCodeErr.Text = "Please select " & GetCaption(objLangCap.EnumLangCap.NurserySubBlock) & "."
        End If

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDRECEIVE_DET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=NU/trx/NU_trx_SeedReceiveDetails.aspx")
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

        strParam = lblTxID.Text & "||||||"

        Try
            intErrNo = objNUTrx.mtdGetSeedReceive(strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strAccMonth, _
                                                  strAccYear, _
                                                  strOppCd_GET, _
                                                  strParam, _
                                                  objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDRECEIVE_DET_GET&errmesg=" & lblErrMessage.Text & "&redirect=NU/trx/NU_trx_SeedReceiveList.aspx")
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
        txtPlantMat.Enabled = strView
        txtDORef.Enabled = strView
        Confirm.Visible = strView
        Delete.Visible = strView
    End Sub

    Sub DisplayData()

        Dim dsTx As DataSet = LoadData()

        If dsTx.Tables(0).Rows.Count > 0 Then
            lblTxID.Text = Trim(dsTx.Tables(0).Rows(0).Item("ReceiveID"))
            txtDate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsTx.Tables(0).Rows(0).Item("ReceiveDate")))
            txtQty.Text = dsTx.Tables(0).Rows(0).Item("Qty")

            lblAccPeriod.Text = Trim(dsTx.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsTx.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objNUTrx.mtdGetSeedReceiveStatus(Trim(dsTx.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("Username"))
            txtPlantMat.Text = Trim(dsTx.Tables(0).Rows(0).Item("PlantMaterial"))
            txtDORef.Text = Trim(dsTx.Tables(0).Rows(0).Item("DORef"))
            BindBlkCode(Trim(dsTx.Tables(0).Rows(0).Item("BlkCode")))
            BindBatchNo(Trim(dsTx.Tables(0).Rows(0).Item("BlkCode")), Trim(dsTx.Tables(0).Rows(0).Item("BatchNo")))

            

            strParam = Trim(dsTx.Tables(0).Rows(0).Item("BlkCode"))


            lblCostSeeds.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(objNUTrx.mtdGetCostSeedsReceive(strOpCdStckTxLine_CostGET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam), 0))

            
        End If
    End Sub

	Sub BindSR()
	    Dim strOpCode As String = "NU_CLSTRX_SEEDRECEIVE_GET_SR"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim objEmpTypeDs As New Object()
        Dim dr As DataRow

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|SEARCHSTR"
        strParamValue = strlocation + "|" + strAccMonth + "|" + strAccYear + "| AND SR.ReceiveID is NULL "

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCode, strParamName, strParamValue, objEmpTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_EMPTYPE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objEmpTypeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpTypeDs.Tables(0).Rows.Count - 1
                objEmpTypeDs.Tables(0).Rows(intCnt).Item("ReceiveID") = Trim(objEmpTypeDs.Tables(0).Rows(intCnt).Item("ReceiveID"))
            Next
        End If

		dr = objEmpTypeDs.Tables(0).NewRow()
        dr("ReceiveID") = ""
        dr("ReceiveID") = "Pilih No.SR"
        objEmpTypeDs.Tables(0).Rows.InsertAt(dr, 0)
		
        ddlTxID.DataSource = objEmpTypeDs.Tables(0)
        ddlTxID.DataTextField = "ReceiveID"
        ddlTxID.DataValueField = "ReceiveID"
        ddlTxID.DataBind()
	End Sub
	
	Sub BindDetSR(ByVal pv_strBlkCode As String)
		Dim strOpCode As String = "NU_CLSTRX_SEEDRECEIVE_GET_SR"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim objEmpTypeDs As New Object()
        Dim dr As DataRow

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|SEARCHSTR"
		
        strParamValue = strlocation + "|" + strAccMonth + "|" + strAccYear + "| AND A.StockReceiveID='" + pv_strBlkCode + "'"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCode, strParamName, strParamValue, objEmpTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_EMPTYPE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try
		 
		lblBlkCode.Text =  Trim(objEmpTypeDs.Tables(0).Rows(0).Item("BatchNo"))
		lblBatchNo.Text =  Trim(objEmpTypeDs.Tables(0).Rows(0).Item("BatchNo"))
		txtDate.Text    =  Date_Validation(Trim(objEmpTypeDs.Tables(0).Rows(0).Item("StockRefDate")),True)
		txtQty.Text    =  Trim(objEmpTypeDs.Tables(0).Rows(0).Item("Qty"))
		txtPlantMat.Text = Trim(objEmpTypeDs.Tables(0).Rows(0).Item("PlantMaterial"))
	End sub
	
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
            SearchStr = " AND BLK.Status = '" & objGLSetup.EnumBlockStatus.Active & "'" & _
                            " AND BLK.BlkType = '" & objGLSetup.EnumBlockType.Nursery & "'" & _
                            " AND BLK.LocCode = '" & strLocation & "'"

            sortitem = "ORDER BY BLK.BlkCode ASC "
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDRCV_DET_BIND_BLOCK_DROPDOWNLIST&errmesg=" & lblErrMessage.Text & "&redirect=NU/Trx/NU_trx_SeedReceiveDetails.aspx")
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
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDRCV_DET_BIND_BLOCK_DROPDOWNLIST_2&errmesg=" & lblErrMessage.Text & "&redirect=NU/Trx/NU_trx_SeedReceiveDetails.aspx")
            End Try
        End If

        ddlBlkCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindBatchNo(ByVal pv_strBlkCode As String, ByVal pv_strBatchNo As String)
        Dim strOpCode As String
        IF pv_strBatchNo <> "" Then
            strOpCode = "NU_CLSSETUP_NURSERYBATCH_LIST_GET"
        ELSE
            strOpCode = "NU_CLSSETUP_NURSERYBATCH_LIST_GET_SR"
        END IF
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
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=NU/Trx/NU_trx_SeedReceiveDetails.aspx")
            End Try

            For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
                If Not pv_strBatchNo = "" Then
                    If dsForDropDown.Tables(0).Rows(intCnt).Item("BatchNo") = pv_strBatchNo Then
                        intSelectedIndex = intCnt
                    End If
                End If
            Next intCnt

            If dsForDropDown.Tables(0).Rows.Count > 0 Then
                ddlBatchNo.DataSource = dsForDropDown.Tables(0)
                ddlBatchNo.DataValueField = "BatchNo"
                ddlBatchNo.DataTextField = "BatchNo"
                ddlBatchNo.DataBind()
            Else
                ddlBatchNo.Items.Add(New ListItem(" "," "))
            End If
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
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=NU/Trx/NU_trx_SeedReceiveDetails.aspx")
                End Try
            End If

            ddlBatchNo.SelectedIndex = intSelectedIndex
        Else
            ddlBatchNo.Items.Add(New ListItem(" "," "))
        End If
    End Sub


    Sub UpdateData(ByVal strAction As String)
		Dim strOpCode As String = "NU_CLSTRX_SEEDRECEIVE_ADD_FROM_SR"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim objEmpTypeDs As New Object()
        Dim dr As DataRow

        strParamName = "STOCKRECEIVEID|LOCCODE|ACCMONTH|ACCYEAR"
        strParamValue = ddlTxID.SelectedItem.Value.Trim() + "|" + strlocation + "|" + strAccMonth + "|" + strAccYear 

        Try
			intErrNo = objOk.mtdInsertDataCommon(strOpCode, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_EMPTYPE_LIST_GET&errmesg=" & Exp.Message )
        End Try

        'If Not strDate = "" Then
         '   Select Case strAction
         '       Case "Confirm"
        '            strStatus = objNUTrx.EnumSeedReceiveStatus.Confirmed
         '       Case "Delete"
         '           strStatus = objNUTrx.EnumSeedReceiveStatus.Deleted
        '    End Select

         '   strParam = lblTxID.Text & "|" & _
         '               ddlBlkCode.SelectedItem.Value & "|" & _
         '               ddlBatchNo.SelectedItem.Value & "|" & _
         '               strDate & "|" & _
         '               txtQty.Text & "|" & txtPlantMat.Text & "|" & txtDORef.Text


          '  Try
          '      intErrNo = objNUTrx.mtdUpdSeedReceive(strOppCd_ADD, _
          '                                          strOppCd_UPD, _
          '                                          strOppCd_GET, _
          '                                          strLocation, _
          '                                          strUserId, _
          '                                          strAccMonth, _
          '                                          strAccYear, _
          '                                          strParam, _
          '                                          intError, _
          '                                          lblOper.Text, _
          '                                          objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.SeedReceive), _
          '                                          TxID, _
          '                                          blnDeleteErr)

           '     If blnDeleteErr = True Then
           '         lblDeleteErr.Visible = True
           '     Else
           '         lblDeleteErr.Visible = False
           '     End If


           ' Catch Exp As System.Exception
           '     Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_SEEDRECEIVEDETAILS_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=NU/trx/NU_trx_SeedReceiveDetails.aspx")
           ' End Try

           ' If intError = objNUTrx.EnumErrorType.DuplicateKey Then
           '     lblDupMsg.Visible = True
           ' Else
            '    If blnDeleteErr = False Then
                    Response.Redirect("NU_trx_SeedReceiveList.aspx")
            '    End If
            'End If
        'End If

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
        If ddlTxID.SelectedItem.Value.Trim() = "" Then
            lblBlkCodeErr.Visible = True
            Exit Sub
        End If
        'If ddlBatchNo.SelectedItem.Value = "" Then
        '    lblBatchNoErr.Visible = True
        '    Exit Sub
        'End If
		
        UpdateData("Confirm")
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode As String = "NU_CLSTRX_SEEDRECEIVE_UPD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim objEmpTypeDs As New Object()
        Dim dr As DataRow

        strParamName = "UPDATESTR"
        strParamValue = " SET Status='2' WHERE ReceiveID='" & lblTxID.Text.Trim() & "'" 

        Try
			intErrNo = objOk.mtdInsertDataCommon(strOpCode, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_EMPTYPE_LIST_GET&errmesg=" & Exp.Message )
        End Try
		
		Response.Redirect("NU_trx_SeedReceiveList.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("NU_trx_SeedReceiveList.aspx")
    End Sub

    Sub CallFillBatchNo(ByVal sender As Object, ByVal e As System.EventArgs)
        BindDetSR(ddlTxID.SelectedItem.Value.Trim())
		'BindBatchNo(ddlBlkCode.SelectedItem.Value, "")
        'strParam = Trim(ddlBlkCode.SelectedItem.Value) & "|" & ddlBatchNo.SelectedItem.Value
        'txtQty.Text = cDbl(objNUTrx.mtdGetQtySeedsReceive(strOpCdStckTxLine_QtyGET, _
        '                                                strCompany, _
        '                                                strLocation, _
        '                                                strUserId, _
        '                                                strAccMonth, _
        '                                                strAccYear, _
        '                                                strParam))

        
        'strParam = Trim(ddlBlkCode.SelectedItem.Value) 
        'lblCostSeeds.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(objNUTrx.mtdGetCostSeedsReceive(strOpCdStckTxLine_CostGET, _
        '                                                strCompany, _
        '                                                strLocation, _
        '                                                strUserId, _
        '                                                strAccMonth, _
        '                                                strAccYear, _
        '                                                strParam), 0))
    End Sub

	Sub CallRefreshBatchNo(ByVal sender As Object, ByVal e As System.EventArgs)
        strParam = Trim(ddlBlkCode.SelectedItem.Value) & "|" & ddlBatchNo.SelectedItem.Value

        txtQty.Text = cDbl(objNUTrx.mtdGetQtySeedsReceive(strOpCdStckTxLine_QtyGET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam))

        
        strParam = Trim(ddlBlkCode.SelectedItem.Value) 

        lblCostSeeds.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(objNUTrx.mtdGetCostSeedsReceive(strOpCdStckTxLine_CostGET, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam), 0))
End Sub
  


End Class
