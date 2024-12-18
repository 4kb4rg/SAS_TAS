Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class IN_ItemToMachineDetail : Inherits Page

    Protected WithEvents ddlItem As DropDownList
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents ddlSubBlock As DropDownList 
    Protected WithEvents txtDate As TextBox
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid

    Protected WithEvents lblBlkTag As Label
    Protected WithEvents lblSubBlkTag As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents NewBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents BackBtn As ImageButton

    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblErrSubBlock As Label
    Protected WithEvents lblErrDate As Label
    Protected WithEvents lblErrDateFmt As Label
    Protected WithEvents lblErrDateFmtMsg As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrExceeding As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblErrDupl As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrMessage As Label
    Protected lblCloseExist As Label    
    Protected lblLifespan As Label
    Protected lblActHourMeter As Label
    Protected TxtLifeTime As TextBox
    Protected WithEvents txtPartQty As TextBox
    Protected WithEvents ddlLineNo As DropDownList
    Protected WithEvents txtLineDesc As TextBox
    Protected WithEvents lblLineNoErr As Label
    Protected WithEvents txtMechHour As TextBox

    Protected objINTrx As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLTrx As New agri.GL.ClsTrx()

    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objWS As New agri.WS.clsSetup()
    
    Dim objLangCapDs As New Dataset()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intINAR As Integer
    Dim intConfig As Integer
    Dim strDateFmt As String
    Dim strSelectedSubBlkCode As String = ""
    Dim strSelectedBlkCode As String = ""
    Dim intStatus As Integer
    Dim strAcceptFormat As String
    Dim pv_strSubBlkCode As String
    Dim pv_strBlockCode As String
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")
        intConfig = Session("SS_CONFIGSETTING")
        strDateFmt = Session("SS_DATEFMT")
 	    
        strLocType = Session("SS_LOCTYPE")
       
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INItemToMachine), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrBlock.Visible = False
            lblErrDupl.Visible = False
            lblErrExceeding.Visible = False
            lblErrDate.Visible = False
            lblErrDateFmt.Visible = False
            lblErrDateFmtMsg.Visible = False
            lblLineNoErr.Visible = False

            strSelectedSubBlkCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = Convert.ToInt32(lblHiddenSts.Text)
            
            If Not IsPostBack Then
                If strSelectedSubBlkCode <> "" Then
                    BindSubBlock("")
                    BindBlock("")  
                    BindItem("")   
                    onLoad_Display()
                    onLoad_DisplayLine()
                    onLoad_BindButton()
                Else
                    txtDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                    BindSubBlock("")
                    BindBlock("")            
                    BindItem("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        Try
            lblBlkTag.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
            lblSubBlkTag.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text

        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=PR/trx/IN_trx_wpdet.aspx")
        End Try
        lblErrBlock.Text = lblErrSelect.Text & lblBlkTag.Text
        lblErrSubBlock.Text = lblErrSelect.Text & lblSubBlkTag.Text
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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PR/trx/IN_trx_WPDet.aspx")
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


    Sub onLoad_BindButton()
        txtDate.Enabled = False
        ddlItem.Enabled = False
        ddlBlock.Enabled = False
        ddlSubBlock.Enabled = False
        SaveBtn.Visible = False
        tblSelection.Visible = False
        UnDelBtn.visible = False
        DelBtn.visible = False

        Select Case intStatus
            Case objINTrx.EnumItemToMachineStatus.Active
                txtDate.Enabled = True
                ddlItem.Enabled = True
                ddlBlock.Enabled = False
                ddlSubBlock.Enabled = False
                SaveBtn.Visible = True
                tblSelection.Visible = True
                SaveBtn.Visible = True 
                DelBtn.visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            Case objINTrx.EnumItemToMachineStatus.Deleted
                txtDate.Enabled = False    
                ddlItem.Enabled = False
                ddlBlock.Enabled = False
                ddlSubBlock.Enabled = False
                tblSelection.Visible = False
                SaveBtn.Visible = False 
                UnDelBtn.Visible = True
                UnDelBtn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            
            Case Else
                txtDate.Enabled = True
                ddlItem.Enabled = True
                ddlBlock.Enabled = True
                ddlSubBlock.Enabled = True
                SaveBtn.Visible = True
                tblSelection.Visible = True
                SaveBtn.Visible = True 
                
         End Select
    End Sub

    Sub onLoad_Display()
        Dim objTrxDs As New Dataset
        Dim strOpCd As String = "IN_CLSTRX_ITEMTOMACHINE_GET"
        Dim strParam As String = strSelectedSubBlkCode & "|" & strLocation
        Dim intErrNo As Integer

        Try
            intErrNo = objINTrx.mtdGetItemToMachineTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            objTrxDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEMTOMACHINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        intStatus = Convert.ToInt32(objTrxDs.Tables(0).Rows(0).Item("Status"))
        lblHiddenSts.Text = objTrxDs.Tables(0).Rows(0).Item("Status").Trim()
        lblStatus.Text = objINTrx.mtdGetItemToMachineStatus(Convert.ToInt16(objTrxDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objTrxDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objTrxDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = objTrxDs.Tables(0).Rows(0).Item("UserName")
        BindBlock(objTrxDs.Tables(0).Rows(0).Item("BlkCode").Trim())
        strSelectedBlkCode = objTrxDs.Tables(0).Rows(0).Item("BlkCode").Trim()
        BindSubBlock(strSelectedSubBlkCode)
        GetActHourMeter(strSelectedSubBlkCode)
        objTrxDs = Nothing
    End Sub

    Sub onLoad_DisplayLine()
        Dim objTrxLnDs As New Dataset()
        Dim strOpCd As String = "IN_CLSTRX_ITEMTOMACHINE_LINE_GET"
        Dim strParam As String = strSelectedSubBlkCode & "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
    
        Try
            intErrNo = objINTrx.mtdGetItemToMachineTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            objTrxLnDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEMTOMACHINE_LINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dgLineDet.DataSource = objTrxLnDs.Tables(0)
        dgLineDet.DataBind()

        If intStatus = objINTrx.EnumItemToMachineStatus.Active Then
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = True
                lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Next
        Else
            For intCnt = 0 To dgLineDet.Items.Count - 1
                lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = False
            Next
        End If
    End Sub

    Sub BindItem(ByVal pv_strItemCode As String)
        Dim objItemDs As New Dataset
        Dim strOpCd As String = "IN_CLSTRX_ITEMTOMACHINE_ITEM_GET"
        Dim dr As DataRow           
        Dim strParam As String = "|" & "where LocCode = '" & Trim(strLocation) & "' And Status = '" & objINSetup.EnumStockItemStatus.Active & "' And ItemType = '" & objINSetup.EnumInventoryItemType.WorkshopItem & "' " & "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objItemDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_ITEM_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strItemCode = Trim(UCase(pv_strItemCode))

        For intCnt = 0 To objItemDs.Tables(0).Rows.Count - 1
            objItemDs.Tables(0).Rows(intCnt).Item("Description") = objItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & objItemDs.Tables(0).Rows(intCnt).Item("Description") & ")"
            If UCase(objItemDs.Tables(0).Rows(intCnt).Item("ItemCode")) = pv_strItemCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objItemDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strItemCode <> "" Then
                dr("ItemCode") = Trim(pv_strItemCode)
                dr("Description") = Trim(pv_strItemCode)
            Else
                dr("ItemCode") = ""
                dr("Description") = "Select one Item Code"
            End If
        Else
            dr("ItemCode") = ""
            dr("Description") = "Select one Item Code"
        End If
        objItemDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlItem.DataSource = objItemDs.Tables(0)
        ddlItem.DataValueField = "ItemCode"
        ddlItem.DataTextField = "Description"
        ddlItem.DataBind()
        ddlItem.SelectedIndex = intSelectedIndex
        objItemDs = Nothing
    End Sub

    Sub onSelect_Item(ByVal Sender As Object, ByVal E As EventArgs)
        Dim objItemDs As New Dataset
        Dim strOpCd As String = "IN_CLSTRX_ITEMTOMACHINE_ITEM_GET"
        Dim dr As DataRow           
        Dim strParam As String = "|" & "where LocCode = '" & Trim(strLocation) & "' And Status = '" & objINSetup.EnumStockItemStatus.Active & "' And ItemType = '" & objINSetup.EnumInventoryItemType.WorkshopItem & "' And ItemCode = '" & Trim(ddlItem.SelectedItem.Value) & "' " & "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objItemDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_ITEM_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objItemDs.Tables(0).Rows.Count <=0 Then
            Exit Sub
        Else
            lblLifespan.Text = objItemDs.Tables(0).Rows(0).Item("Lifespan")
        End If 
    End Sub

    Sub BindBlock(ByVal pv_strBlockCode As String)
        Dim objBlkDs As New Dataset()
        Dim strOpCd As String = "GL_CLSSETUP_BLOCK_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "|" & "And blk.Status = '" & objGLSetup.EnumBlockStatus.Active & "' And blk.LocCode ='" & strLocation & "'" '& "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objBlkDs)
 
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strBlockCode = Trim(UCase(pv_strBlockCode))

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & objBlkDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ") "
            If UCase(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) = pv_strBlockCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strBlockCode <> "" Then
                dr("BlkCode") = Trim(pv_strBlockCode)
                dr("Description") = Trim(pv_strBlockCode)
            Else
                dr("BlkCode") = ""
                dr("Description") = lblSelect.Text & lblBlkTag.Text
            End If
        Else
            dr("BlkCode") = ""
            dr("Description") = lblSelect.Text & lblBlkTag.Text
        End If
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectedIndex
        objBlkDs = Nothing
    End Sub

    Sub onSelect_Block(ByVal Sender As Object, ByVal E As EventArgs)
        If Not (ddlBlock.SelectedItem.Value = "")
           strSelectedBlkCode = Trim(ddlBlock.SelectedItem.Value)
           BindSubBlock("")
        End If
    End Sub

    Sub BindSubBlock(ByVal pv_strSubBlkCode As String)
        Dim objSubBlkDs As New Dataset()
        Dim strOpCd As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0
        
        strParam = "|" & "And Sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "' And BlkCode = '" & Trim(strSelectedBlkCode) & "'" & "|" & strLocation
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objSubBlkDs)
 
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strSubBlkCode = Trim(UCase(pv_strSubBlkCode))

        For intCnt = 0 To objSubBlkDs.Tables(0).Rows.Count - 1
            objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode").Trim()
            objSubBlkDs.Tables(0).Rows(intCnt).Item("Description") = objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode") & " (" & objSubBlkDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ") "
            If UCase(objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode")) = pv_strSubBlkCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objSubBlkDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strSubBlkCode <> "" Then
                dr("SubBlkCode") = Trim(pv_strSubBlkCode)
                dr("Description") = Trim(pv_strSubBlkCode)
            Else
                dr("SubBlkCode") = ""
                dr("Description") = lblSelect.Text & lblSubBlkTag.Text
            End If
        Else
            dr("SubBlkCode") = ""
            dr("Description") = lblSelect.Text & lblSubBlkTag.Text
        End If
        objSubBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSubBlock.DataSource = objSubBlkDs.Tables(0)
        ddlSubBlock.DataValueField = "SubBlkCode"
        ddlSubBlock.DataTextField = "Description"
        ddlSubBlock.DataBind()
        ddlSubBlock.SelectedIndex = intSelectedIndex
        objSubBlkDs = Nothing
    End Sub

    Sub onSelect_SubBlock(ByVal Sender As Object, ByVal E As EventArgs)
        GetActHourMeter(ddlSubBlock.SelectedItem.Value)
    End Sub
        
    Sub GetActHourMeter(ByVal pv_strSubBlkCode As String)
        Dim objSubBlkDs As New Dataset()
        Dim strOpCd As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "|" & "And Sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "' And sub.BlkCode = '" & Trim(ddlBlock.SelectedItem.Value) & "' And sub.SubBlkCode = '" & Trim(pv_strSubBlkCode) & "' "& "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objSubBlkDs)
 
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objSubBlkDs.Tables(0).Rows.Count <=0 Then
            Exit Sub
        Else
            lblActHourMeter.Text = objSubBlkDs.Tables(0).Rows(0).Item("ActHourMeter")
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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_WP_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_GRList.aspx")
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

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim objTrxDs As New DataSet()
        Dim objItemDs As New DataSet
        Dim blnIsUpdated As Boolean
        Dim strpoCode_Estimation As String = "IN_CLSTRX_ITEMTOMACHINE_ESTIMATION_GET"
        Dim strOpCode_GetLine As String = "IN_CLSTRX_ITEMTOMACHINE_LINEDUPL_GET"
        Dim strOpCode_AddLine As String = "IN_CLSTRX_ITEMTOMACHINE_LINE_ADD"
        Dim strOpCode_UpdID As String = "IN_CLSTRX_ITEMTOMACHINE_STATUS_UPD"

        Dim strSubBlkCode As String = Trim(ddlSubBlock.SelectedItem.Value)
        Dim strBlkCode As String = Trim(ddlBlock.SelectedItem.Value)
        Dim strItemCode As String = Trim(ddlItem.SelectedItem.Value)
        Dim strRemainLifespan As String = lblLifespan.Text
        Dim strInstallDate As String = Date_Validation(txtDate.Text, False)
        Dim strLinesNo As String = Trim(ddlLineNo.SelectedItem.Value)
        Dim strLinesDesc As String = Trim(txtLineDesc.Text)
        Dim strPartQty As String = Trim(IIf(txtPartQty.Text = "", 1, txtPartQty.Text))
        Dim strMechHour As String = Trim(IIf(txtMechHour.Text = "", 1, txtMechHour.Text))
        Dim strOpCd_AddPM As String = "IN_CLSTRX_PREVENTIVE_ACTUAL_ADD"
        Dim NextReplHM As Integer
        Dim NextReplDate As Date
        Dim strStatus As String = 1

        Dim strParam As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim pWorkMachineAverage As Double = 0
        Dim pEstimationDate As Date
        Dim pWorkAverage As Double

        If strBlkCode = "" Then
            lblErrBlock.Visible = True
            Exit Sub
        ElseIf strSubBlkCode = "" Then
            lblErrSubBlock.Visible = True
            Exit Sub
        ElseIf strLinesNo = "" Then
            lblLineNoErr.Visible = True
            Exit Sub
        End If

        strSelectedSubBlkCode = Trim(strSubBlkCode)

        InsertRecord(blnIsUpdated)
        If strSelectedSubBlkCode = "" Then
            Exit Sub
        End If

        strParam = "LOCCODE|ACCYEAR"
        strParamValue = strLocation & "|" & strAccYear

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strpoCode_Estimation, _
                                                strParam, _
                                                strParamValue, _
                                                objItemDs)
        Catch exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEMTOMACHINELN_GET&errmesg=" & exp.ToString() & "&redirect=")
        End Try

        If objItemDs.Tables(0).Rows.Count > 0 Then
            pWorkMachineAverage = objItemDs.Tables(0).Rows(0).Item("THMonthAverage")
        End If
        objItemDs = Nothing

        pWorkAverage = 0
        If CDbl(0 & TxtLifeTime.Text) > 0 Then
            pWorkAverage = CDbl(0 & TxtLifeTime.Text) / pWorkMachineAverage
        Else
            pWorkAverage = CDbl(0 & lblLifespan.Text) / pWorkMachineAverage
        End If

        pEstimationDate = DateAdd(DateInterval.Day, pWorkAverage, CDate(strInstallDate))

        strParam = "|" & "LocCode = '" & Trim(strLocation) & "' And IML.BlkCode = '" & Trim(strBlkCode) & "' " & _
                   " And IML.SubBlkCode = '" & strSelectedSubBlkCode & "' AND IML.InstallDate='" & strInstallDate & "' And ItemCode = '" & Trim(strItemCode) & "' " & _
                   " And LinesNo = '" & Trim(strLinesNo) & "' "

        ' ==JHON : LINES NO TIDAK MENGUNCI KRN ADA KEMUNGKINAN INSTAL DI LINES NO YG SAMA

        Try
            intErrNo = objINTrx.mtdGetItemToMachineTrxLn(strOpCode_GetLine, _
                                            strParam, _
                                            objTrxDs, _
                                            False)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEMTOMACHINELN_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objTrxDs.Tables(0).Rows.Count > 0 Then
            lblErrDupl.Visible = True
            Exit Sub
        End If



        GetActHourMeter(Trim(strBlkCode))
        If Not (lblLifespan.Text = 0 And lblActHourMeter.Text = 0) Then
            If CInt(TxtLifeTime.Text) <= 0 Then
                NextReplHM = CInt(lblActHourMeter.Text) + CInt(lblLifespan.Text)
            Else
                NextReplHM = CInt(lblActHourMeter.Text) + CDbl(0 & TxtLifeTime.Text)
            End If
            NextReplDate = strInstallDate

        Else
            NextReplHM = 0
            NextReplDate = strInstallDate
        End If
        strParam = vbNullString
        strParamValue = vbNullString

        strParam = "BLKCODE|SUBBLKCODE|ITEMCODE|LIFESPAN|REMAINLIFESPAN|INSTALLDATE|REPLACEDATE|PARTQTY|LINESNO|LINESDESC|REPLACEHM|MECHANICHOUR|ESTIMATIONDATE|STATUS"
        strParamValue = strBlkCode & "|" & _
                    strSelectedSubBlkCode & "|" & _
                    strItemCode & "|" & _
                    IIf((CDbl(0 & TxtLifeTime.Text)) > 0, CDbl(0 & TxtLifeTime.Text), lblLifespan.Text) & "|" & _
                    strRemainLifespan & "|" & _
                    strInstallDate & "|" & _
                    NextReplDate & "|" & _
                    strPartQty & "|" & _
                    strLinesNo & "|" & _
                    strLinesDesc & "|" & _
                    NextReplHM & "|" & _
                    strMechHour & "|" & _
                    pEstimationDate & "|" & _
                    strStatus
        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode_AddLine, strParam, strParamValue)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/IN_clstrx_WPdet.aspx?WPTrxID=" & strSelectedSubBlkCode)
        End Try

        'strParam = strBlkCode & "|" & _
        '            strSelectedSubBlkCode & "|" & _
        '            strItemCode & "|" & _
        '            IIf(CDbl(0 & TxtLifeTime.Text) > 0, TxtLifeTime.Text, lblLifespan.Text) & "|" & _
        '            strRemainLifespan & "|" & _
        '            strInstallDate & "|" & _
        '            NextReplDate & "|" & _
        '            strPartQty & "|" & _
        '            strLinesNo & "|" & _
        '            strLinesDesc & "|" & _
        '            NextReplHM & "|" & _
        '            strMechHour & "|" & _
        '            pEstimationDate & "|" & _
        '            1

        'Try
        '    intErrNo = objINTrx.mtdUpdItemToMachineTrxLine(strOpCode_UpdID, _
        '                                            strOpCode_AddLine, _
        '                                            strCompany, _
        '                                            strLocation, _
        '                                            strUserId, _
        '                                            strParam, _
        '                                            objINTrx.EnumItemToMachineStatus.Active, _
        '                                            False, _
        '                                            objTrxDs)
        'Catch Exp As Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_LINE_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/IN_clstrx_WPdet.aspx?WPTrxID=" & strSelectedSubBlkCode)
        'End Try


        strParam = Trim(strLocation) & "|" & Trim(strAccYear) & "|" & Trim(strBlkCode) & "|" & Trim(strSubBlkCode) & "|" & _
                                            Trim(strItemCode) & "|" & Trim(strLinesNo) & "|" & Trim(strLinesDesc) & "|" & strInstallDate & "|1"

        Try
            intErrNo = objINTrx.mtdUpdPreventiveMaintenance(strOpCd_AddPM, _
                                        strCompany, _
                                        strLocation, _
                                        strAccYear, _
                                        strUserId, _
                                        strParam, _
                                        False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_PREV_ESTIMATION_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        'End If

        If strSelectedSubBlkCode <> "" Then
            onLoad_Display()
            onLoad_DisplayLine()
            onLoad_BindButton()
        End If

        objTrxDs = Nothing
    End Sub

    Sub InsertRecord(ByRef IN_blnIsUpdated As Boolean)
        Dim objTrxDs As New Dataset()
        'Dim objTrxID As String
        Dim strOpCd_Add As String = "IN_CLSTRX_ITEMTOMACHINE_ADD"
        Dim strOpCd_Get As String = "IN_CLSTRX_ITEMTOMACHINE_GET"
        Dim strOpCd_Sts As String = "IN_CLSTRX_ITEMTOMACHINE_STATUS_UPD"
        Dim strOpCd As String
        Dim intErrNo As Integer
        Dim strInstallDate As String = txtDate.Text
        Dim strParam As String = strSelectedSubBlkCode
        Dim objFormatDate As String
        Dim objActualDate As String
      
        strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Sts)
        
        If strSelectedSubBlkCode = "" Then
            Exit Sub
        End If
       
        If Trim(txtDate.Text) = "" Then
            lblErrDate.Visible = True
            Exit Sub
        ElseIf objGlobal.mtdValidInputDate(strDateFmt, _
                                           txtDate.Text, _
                                           objFormatDate, _
                                           objActualDate) = False Then
    	        lblErrDateFmt.Visible = True
                lblErrDateFmt.Text = lblErrDateFmtMsg.Text & objFormatDate
                Exit Sub
            Else
                txtDate.Text = objGlobal.GetShortDate(strDateFmt, Date_Validation(txtDate.Text, False)) 
        End If
        
        strSelectedSubBlkCode = Trim(ddlSubBlock.SelectedItem.Value)   
        strParam = Trim(ddlBlock.SelectedItem.Value) & "|" & _
                strSelectedSubBlkCode & "|" & _
                objINTrx.EnumItemToMachineStatus.Active  
           
        Try
            intErrNo = objINTrx.mtdUpdItemToMachineTrx(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            False)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEMTOMACHINE_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/trx/IN_clstrx_WPdet.aspx")
        End Try

        objTrxDs = Nothing
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "IN_CLSTRX_ITEMTOMACHINE_ADD"
        Dim strOpCd_Upd As String = "IN_CLSTRX_ITEMTOMACHINE_UPD"
        Dim strOpCd_Get As String = "IN_CLSTRX_ITEMTOMACHINE_GET"
        Dim strOpCd_Sts As String = "IN_CLSTRX_ITEMTOMACHINE_STATUS_UPD"
        Dim blnIsUpdated As Boolean
        Dim strOpCd As String
        Dim strGang As String = Request.Form("ddlItem")
        Dim objTrxID As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strParam As String = "" 

        If strCmdArgs = "Save" Then
            InsertRecord(blnIsUpdated)
        End If

        If strSelectedSubBlkCode <> "" Then
            onLoad_Display()
            onLoad_DisplayLine()
            onLoad_BindButton()
        End If
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "IN_CLSTRX_ITEMTOMACHINE_LINE_DEL"
        Dim strOpCode_UpdID As String = "IN_CLSTRX_ITEMTOMACHINE_STATUS_UPD"
        Dim strOpCode_UpdPM As String = "IN_CLSTRX_PREVENTIVE_ACTUAL_UPD_STATUS"
        Dim strParam As String
        Dim lblItem As Label
        Dim strItemCode As String
        Dim lblLineNo As Label
        Dim strLinesNo As String
        Dim intErrNo As Integer

        strSelectedBlkCode = Trim(ddlBlock.SelectedItem.Value)
        strSelectedSubBlkCode = Trim(ddlSubBlock.SelectedItem.Value)

        dgLineDet.EditItemIndex = Convert.ToInt32(E.Item.ItemIndex)
        lblItem = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("ItemCode")
        strItemCode = lblItem.Text
        lblLineNo = dgLineDet.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("LinesNo")
        strLinesNo = lblLineNo.Text
        
        Try
            strParam = strSelectedBlkCode & "|" & _
                       strSelectedSubBlkCode & "|" & _
                       strItemCode & "|" & strLinesNo 
            intErrNo = objINTrx.mtdUpdItemToMachineTrxLine(strOpCode_UpdID, _
                                                strOpCode_DelLine, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objINTrx.EnumItemToMachineStatus.Active, _ 
                                                True, _
                                                objResult)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_ITEMTOMACHINE_LINE_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/IN_clstrx_WPdet.aspx?WPTrxID=" & strSelectedSubBlkCode)
        End Try

        strParam = Trim(strLocation) & "|" & Trim(strAccYear) & "|" & Trim(strSelectedBlkCode) & "|" & Trim(strSelectedSubBlkCode) & "|" & _
                   Trim(strItemCode) & "|" & Trim(strLinesNo) & "|" & objINTrx.EnumItemToMachineStatus.Deleted & "|1"
           
        Try
            intErrNo = objINTrx.mtdUpdPreventiveMaintenance(strOpCode_UpdPM, _
                                        strCompany, _
                                        strLocation, _
                                        strAccYear, _
                                        strUserId, _
                                        strParam, _
                                        True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_PREV_ESTIMATION_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        onLoad_Display()
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub

    Sub DeleteBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "IN_CLSTRX_ITEMTOMACHINE_STATUS_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSelectedBlkCode = ddlBlock.SelectedItem.Value 
        strSelectedSubBlkCode = ddlSubBlock.SelectedItem.Value 

        strParam = strSelectedBlkCode & "|" & strSelectedSubBlkCode & "|" & objINTrx.EnumItemToMachineStatus.Deleted
       
        Try
            intErrNo = objINTrx.mtdUpdItemToMachineTrx(strOpCd, _
                                           strCompany, _
                                           strLocation, _
                                           strUserId, _
                                           strParam, _
                                           True)

        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEMTOMACHINE_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/IN_Trx_ItemToMachine_list.aspx")
        End Try

        onLoad_Display()
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub

    Sub UnDeleteBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "IN_CLSTRX_ITEMTOMACHINE_STATUS_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        strSelectedBlkCode = ddlBlock.SelectedItem.Value 
        strSelectedSubBlkCode = ddlSubBlock.SelectedItem.Value 

        strParam = strSelectedBlkCode & "|" & strSelectedSubBlkCode & "|" & objINTrx.EnumItemToMachineStatus.Active
       
        Try
            intErrNo = objINTrx.mtdUpdItemToMachineTrx(strOpCd, _
                                           strCompany, _
                                           strLocation, _
                                           strUserId, _
                                           strParam, _
                                           True)

        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEMTOMACHINE_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/IN_Trx_ItemToMachine_list.aspx")
        End Try

        onLoad_Display()
        onLoad_DisplayLine()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'BackBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        Response.Redirect("IN_Trx_ItemToMachine_list.aspx")
    End Sub

End Class
