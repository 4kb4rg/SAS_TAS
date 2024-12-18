Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports agri.GL
Imports agri.PU
Imports agri.RC
Imports agri.PWSystem
Imports agri.GlobalHdl
Imports agri.Admin

Public Class RC_trx_DADet : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDispatchAdviceID As Label
    Protected WithEvents lblDocType As Label
    Protected WithEvents lblDocTypeValue As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents txtDispatchAdviceRefNo As TextBox
    Protected WithEvents txtDispatchAdviceRefDate As TextBox
    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents txtQtyDisp As TextBox
    Protected WithEvents txtCost As TextBox
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents ddlItemCode As DropDownList
    Protected WithEvents ddlDirectCode As DropDownList
    Protected WithEvents txtPRID As TextBox
    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents ConfirmBtn As ImageButton
    Protected WithEvents PrintBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents DeleteBtn As ImageButton
    Protected WithEvents BackBtn As ImageButton
    Protected WithEvents daid As HtmlInputHidden
    Protected WithEvents lblStatusHidden As Label
    Protected WithEvents lblDocTypeHidden As Label
    Protected WithEvents lblErrRefDate As Label
    Protected WithEvents lblErrLocation As Label
    Protected WithEvents lblErrItemCode As Label
    Protected WithEvents lblErrDirectCode As Label
    Protected WithEvents lblErrPR As Label
    Protected WithEvents lblErrTotalUnits As Label
    Protected WithEvents lblErrRate As Label
    Protected WithEvents lblErrAmount As Label
    Protected WithEvents lblErrTotal As Label
    Protected WithEvents lblReferer As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblSelectEither As Label
    Protected WithEvents lblOr As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblStockItem As Label
    Protected WithEvents lblDirectChgItem As Label

    Dim objPUTrx As New agri.PU.clsTrx()
    Dim objRCTrx As New agri.RC.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objINSetup As New agri.IN.clsSetup()

    Dim objDADs As New Object()
    Dim objDALnDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFormat As String
    Dim intADAR As Integer

    Dim strSelectedDAID As String
    Dim intDAStatus As Integer
    Dim strAcceptDateFormat As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strDateFormat = Session("SS_DATEFMT")
        intADAR = Session("SS_ADAR")

        If strUserId = "" Then
            Response.Redirect ("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCDispatchAdvice), intADAR) = False Then
            Response.Redirect ("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrRefDate.Visible = False
            lblErrLocation.Visible = False
            lblErrItemCode.Visible = False
            lblErrDirectCode.Visible = False
            lblErrPR.Visible = False
            lblErrTotalUnits.Visible = False
            lblErrRate.Visible = False
            lblErrAmount.Visible = False
            lblErrTotal.Visible = False
            lblReferer.Text = Request.QueryString("referer")
            strSelectedDAID = Trim(IIf(Request.QueryString("daid") = "", Request.Form("daid"), Request.QueryString("daid")))
            daid.Value = strSelectedDAID
            
            If Not IsPostBack Then
                If strSelectedDAID <> "" Then
                    onLoad_Display(strSelectedDAID)
                    onLoad_DisplayLine(strSelectedDAID)
                    onLoad_Button()
                Else
                    BindLocation("")
                    BindItemCode("", True)
                    BindItemCode("", False)
                    onLoad_Button()
                End If
            End If
         End If
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.text = GetCaption(objLangCap.EnumLangCap.Location)
        lblStockItem.text = GetCaption(objLangCap.EnumLangCap.StockItem)
        lblDirectChgItem.text = GetCaption(objLangCap.EnumLangCap.DirectChgItem)
        
        lblErrLocation.text = lblPleaseSelect.text & lblLocation.text
        lblErrItemCode.text = lblSelectEither.text & lblStockItem.text & lblOr.text & lblDirectChgItem.text & "."
        
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_DADET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=rc/trx/RC_trx_DADet.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                Exit For
            End If
        Next
    End Function



    Sub onLoad_Button()
        Dim intStatus As Integer
        txtDispatchAdviceRefNo.Enabled = False
        txtDispatchAdviceRefDate.Enabled = False
        ddlLocation.Enabled = False
        tblSelection.Visible = False
        SaveBtn.Visible = False
        ConfirmBtn.Visible = False
        CancelBtn.Visible = False
        DeleteBtn.Visible = False
        PrintBtn.Visible = False
        If (lblStatusHidden.Text <> "") Then
            intStatus = CInt(lblStatusHidden.Text)
            Select Case intStatus
                   Case objRCTrx.EnumDispatchAdviceStatus.Active
                        txtDispatchAdviceRefNo.Enabled = True
                        txtDispatchAdviceRefDate.Enabled = True
                        ddlLocation.Enabled = True
                        tblSelection.Visible = True
                        SaveBtn.Visible = True
                        ConfirmBtn.Visible = True
                        DeleteBtn.Visible = True
                        DeleteBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                        PrintBtn.Visible = True
                   Case objRCTrx.EnumDispatchAdviceStatus.Confirmed
                        CancelBtn.Visible = True
                        PrintBtn.Visible = True
                   Case Else
            End Select
        Else
            txtDispatchAdviceRefNo.Enabled = True
            txtDispatchAdviceRefDate.Enabled = True
            ddlLocation.Enabled = True
            tblSelection.Visible = True
            SaveBtn.Visible = True
            ConfirmBtn.Visible = True
        End If
    End Sub

    Sub onLoad_Display(ByVal pv_strDAID As String)
        Dim strOpCd_Get As String = "RC_CLSTRX_DISPATCHADVICE_DETAILS_GET"
        Dim objDADs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String = pv_strDAID
        Dim intCnt As Integer = 0

        daid.Value = pv_strDAID

        Try
            intErrNo = objRCTrx.mtdGetDispatchAdvice(strOpCd_Get, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     strParam, _
                                                     objDADs, _
                                                     True)
        Catch Exp As System.Exception
            Response.Redirect ("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_DISPATCHADVICEDET_GET_HEADER&errmesg=" & Exp.ToString & "&redirect=RC/trx/RC_trx_DAList.aspx")
        End Try

        lblDispatchAdviceID.Text = pv_strDAID
        txtDispatchAdviceRefNo.Text = objDADs.Tables(0).Rows(0).Item("DispAdvRefNo").Trim()
        txtDispatchAdviceRefDate.Text = objGlobal.GetShortDate(strDateFormat, objDADs.Tables(0).Rows(0).Item("DispAdvRefDate"))
        lblDocTypeValue.Text = objRCTrx.mtdGetDispatchAdviceDocType(CInt(objDADs.Tables(0).Rows(0).Item("DispAdvType")))
        lblDocTypeHidden.Text = Trim(objDADs.Tables(0).Rows(0).Item("DispAdvType"))
        lblAccPeriod.Text = Trim(objDADs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objDADs.Tables(0).Rows(0).Item("AccYear"))
        lblStatus.Text = objRCTrx.mtdGetDispatchAdviceStatus(Trim(objDADs.Tables(0).Rows(0).Item("Status")))
        intDAStatus = CInt(Trim(objDADs.Tables(0).Rows(0).Item("Status")))
        lblStatusHidden.Text = intDAStatus
        lblDateCreated.Text = objGlobal.GetLongDate(objDADs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objDADs.Tables(0).Rows(0).Item("UpdateDate"))
        lblPrintDate.Text = objGlobal.GetLongDate(objDADs.Tables(0).Rows(0).Item("PrintDate"))
        lblUpdatedBy.Text = Trim(objDADs.Tables(0).Rows(0).Item("UserName"))
        lblTotalAmount.Text = FormatNumber(objDADs.Tables(0).Rows(0).Item("TotalAmount"), 2)

        BindLocation(objDADs.Tables(0).Rows(0).Item("ToLocCode").Trim())
        BindItemCode("", True)
        BindItemCode("", False)
    End Sub


    Sub onLoad_DisplayLine(ByVal pv_strDAID As String)
        Dim strOpCd_GetLine As String = "RC_CLSTRX_DISPATCHADVICE_LINE_GET"
        Dim strParam As String = pv_strDAID
        Dim lbButton As LinkButton
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Try
            intErrNo = objRCTrx.mtdGetDispatchAdviceLine(strOpCd_GetLine, strParam, objDALnDs)
        Catch Exp As System.Exception
            Response.Redirect ("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_DISPATCHADVICEDET_GET_LINE&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_DAList.aspx")
        End Try

        For intCnt = 0 To objDALnDs.Tables(0).Rows.Count - 1
            objDALnDs.Tables(0).Rows(intCnt).Item("DispAdvLnID") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("DispAdvLnID"))
            objDALnDs.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("ItemCode"))
        Next intCnt
        
        dgLineDet.DataSource = objDALnDs.Tables(0)
        dgLineDet.DataBind()

        For intCnt = 0 To objDALnDs.Tables(0).Rows.Count - 1
            Select Case intDAStatus
                Case objRCTrx.EnumDispatchAdviceStatus.Active
                        lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case Else
                        lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
            End Select
        Next
    End Sub

    Sub BindLocation(ByVal pv_strLocCode As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objToLocCodeDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        strParam = "|" & objAdminLoc.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPUTrx.mtdGetLoc(strOpCd, strParam, objToLocCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RC_DADET_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_DAList.aspx")
        End Try
        
        For intCnt = 0 To objToLocCodeDs.Tables(0).Rows.Count - 1
            objToLocCodeDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objToLocCodeDs.Tables(0).Rows(intCnt).Item("LocCode"))
            objToLocCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objToLocCodeDs.Tables(0).Rows(intCnt).Item("LocCode")) & " (" & Trim(objToLocCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objToLocCodeDs.Tables(0).Rows(intCnt).Item("LocCode") = pv_strLocCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = objToLocCodeDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelect.text & lblLocation.text 
        objToLocCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocation.DataSource = objToLocCodeDs.Tables(0)
        ddlLocation.DataValueField = "LocCode"
        ddlLocation.DataTextField = "Description"
        ddlLocation.DataBind()
        ddlLocation.SelectedIndex = intSelectedIndex        
    End Sub

    Sub BindItemCode(ByVal pv_strItemCode As String, ByVal pv_blnIsStockItem As Boolean)
        Dim strOpCd As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim objItemDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        If pv_blnIsStockItem = True Then
            strParam = objINSetup.EnumInventoryItemType.Stock & "||" & _
                    objINSetup.EnumStockItemStatus.Active & "|itm.ItemCode" 
        Else
            strParam = objINSetup.EnumInventoryItemType.DirectCharge & "||" & _
                    objINSetup.EnumStockItemStatus.Active & "|itm.ItemCode" 
        End If

        Try
            intErrNo = objPUTrx.mtdGetDCItem(strOpCd, strParam, objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RC_DADET_GET_ITEM&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_DADet.aspx")
        End Try
        
        For intCnt = 0 To objItemDs.Tables(0).Rows.Count - 1
            objItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("ItemCode"))
            objItemDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("UOMCode"))
            objItemDs.Tables(0).Rows(intCnt).Item("LatestCost") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("LatestCost"))
            objItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"))
            objItemDs.Tables(0).Rows(intCnt).Item("Description") = objItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ", (" & _
                                                                   Trim(objItemDs.Tables(0).Rows(intCnt).Item("Description")) & "), " & _
                                                                   objItemDs.Tables(0).Rows(intCnt).Item("LatestCost") & ", " & _ 
                                                                   objItemDs.Tables(0).Rows(intCnt).Item("UOMCode")
            If objItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = pv_strItemCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt


        dr = objItemDs.Tables(0).NewRow()
        If pv_blnIsStockItem = True Then
            dr("ItemCode") = ""
            dr("Description") = lblSelect.text & lblStockItem.text & lblCode.text
            objItemDs.Tables(0).Rows.InsertAt(dr, 0)
            ddlItemCode.DataSource = objItemDs.Tables(0)
            ddlItemCode.DataValueField = "ItemCode"
            ddlItemCode.DataTextField = "Description"
            ddlItemCode.DataBind()
            ddlItemCode.SelectedIndex = intSelectedIndex
        Else
            dr("ItemCode") = ""
            dr("Description") = lblSelect.text & lblDirectChgItem.text & lblCode.text
            objItemDs.Tables(0).Rows.InsertAt(dr, 0)
            ddlDirectCode.DataSource = objItemDs.Tables(0)
            ddlDirectCode.DataValueField = "ItemCode"
            ddlDirectCode.DataTextField = "Description"
            ddlDirectCode.DataBind()
            ddlDirectCode.SelectedIndex = intSelectedIndex
        End if
    End Sub


    Sub Update_DispatchAdvice(ByVal pv_intStatus As Integer, ByRef pr_objNewDAID As Object, ByRef pr_intSuccess As Integer)
        Dim strOpCd_AddDA As String = "RC_CLSTRX_DISPATCHADVICE_ADD"
        Dim strOpCd_UpdDA As String = "RC_CLSTRX_DISPATCHADVICE_UPD"
        Dim strOpCodes As String = strOpCd_AddDA & "|" & _
                                   strOpCd_UpdDA
        Dim intErrNo As Integer
        Dim objFormatDate As String
        Dim objRefDate As String
        Dim strParam As String = ""

        pr_intSuccess = 1

        If objGlobal.mtdValidInputDate(strDateFormat, _
                                       txtDispatchAdviceRefDate.Text, _
                                       objFormatDate, _
                                       objRefDate) = False Then
            lblErrRefDate.Text = lblErrRefDate.Text & objFormatDate
            lblErrRefDate.Visible = True
            Exit Sub
        End If

        If ddlLocation.SelectedItem.Value = "" Then
            lblErrLocation.Visible = True
            pr_intSuccess = 0
            Exit Sub
        End If

        If lblDocTypeHidden.Text = "" Then
            lblDocTypeHidden.Text = objRCTrx.EnumDispatchAdviceDocType.Manual
        End If

        Try
            strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.RCDispatchAdvice) & "|" & _
                       strSelectedDAID & "|" & _
                       txtDispatchAdviceRefNo.Text & "|" & _
                       objRefDate & "|" & _
                       ddlLocation.SelectedItem.Value & "|" & _
                       lblDocTypeHidden.Text & "|" & _
                       lblTotalAmount.Text & "|" & _
                       pv_intStatus
            intErrNo = objRCTrx.mtdUpdDispatchAdvice(strOpCodes, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strAccMonth, _
                                                     strAccYear, _
                                                     strParam, _
                                                     pr_objNewDAID)
        Catch Exp As System.Exception
            Response.Redirect ("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_DISPATCHADVICEDET_UPD_DATA&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_DAList.aspx")
        End Try
        pr_objNewDAID = IIf(strSelectedDAID = "", pr_objNewDAID, strSelectedDAID)
    End Sub


    Sub AddBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objDAID As New Object()
        Dim strItemCode As String = Request.Form("ddlItemCode")
        Dim strDirectCode As String = Request.Form("ddlDirectCode")
        Dim dblTotalUnit As Double
        Dim dblRate As Double
        Dim dblAmount As Double
        Dim strOpCode_AddLine As String = "RC_CLSTRX_DISPATCHADVICE_LINE_ADD"
        Dim strOpCode_GetSumAmount As String = "RC_CLSTRX_DISPATCHADVICE_SUM_LINEAMOUNT_GET"
        Dim strOpCode_UpdTotalAmount As String = "RC_CLSTRX_DISPATCHADVICE_TOTALAMOUNT_UPD"
        Dim strOpCodes As String = strOpCode_AddLine & "|" & strOpCode_GetSumAmount & "|" & strOpCode_UpdTotalAmount
        Dim intErrNo As Integer
        Dim intSuccess As Integer

        If (strItemCode = "" And strDirectCode = "") Or _
           (strItemCode <> "" And strDirectCode <> "") Then
            lblErrItemCode.Visible = True
            Exit Sub
        Else
            If strItemCode = "" Then
                strItemCode = strDirectCode
            End If
        End If

        If Trim(txtQtyDisp.Text) = "" Then
            lblErrTotalUnits.Visible = True
            Exit Sub
        Else
            dblTotalUnit = CDbl(txtQtyDisp.Text)
        End If
        If Trim(txtCost.Text) = "" Then
            lblErrRate.Visible = True
            Exit Sub
        Else
            dblRate = CDbl(txtCost.Text)
        End If
        If Trim(txtAmount.Text) = "" Then
            lblErrAmount.Visible = True
            Exit Sub
        Else
            dblAmount = CDbl(txtAmount.Text)
        End If

        If strSelectedDAID = "" Then
            Update_DispatchAdvice(objRCTrx.EnumDispatchAdviceStatus.Active, objDAID, intSuccess)
            If intSuccess = 1 Then
                If UCase(TypeName(objDAID)) = "OBJECT" Then
                    Exit Sub
                Else
                    strSelectedDAID = objDAID
                End If
            Else
                Exit Sub
            End If
        End If


        Dim strParam As String = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.RCDispatchAdviceLn) & "|" & _
                                 strSelectedDAID & "|" & _
                                 strItemCode & "|" & _
                                 txtPRID.Text & "|" & _
                                 dblTotalUnit & "|" & _
                                 dblRate & "|" & _
                                 dblAmount

        Try
            intErrNo = objRCTrx.mtdUpdDispatchAdviceLine(strOpCodes, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strAccMonth, _
                                                         strAccYear, _
                                                         strParam)
        Catch Exp As System.Exception
            Response.Redirect ("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_DISPATCHADVICEDET_ADD_LINEA&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_DAList.aspx")
        End Try

        onLoad_Display(strSelectedDAID)
        onLoad_DisplayLine(strSelectedDAID)
        onLoad_Button()
    End Sub

    Sub SaveBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objDAID As String
        Dim intSuccess As Integer
        
        Update_DispatchAdvice(objRCTrx.EnumDispatchAdviceStatus.Active, objDAID, intSuccess)
        If intSuccess = 1 Then
            onLoad_Display(objDAID)
            onLoad_DisplayLine(objDAID)
            onLoad_Button()
        Else
            Exit Sub
        End If
    End Sub

    Sub ConfirmBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objDAID As New Object()
        Dim intSuccess As Integer
        
        If CDbl(lblTotalAmount.Text) <= 0 Then
            lblErrTotal.Visible = True
        Else
            Update_DispatchAdvice(objRCTrx.EnumDispatchAdviceStatus.Confirmed, objDAID, intSuccess)
            If intSuccess = 1 Then
                onLoad_Display(objDAID)
                onLoad_DisplayLine(objDAID)
                onLoad_Button()
            Else
                Exit Sub
            End If
        End If
    End Sub

    Sub CancelBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objDAID As New Object()
        Dim intSuccess As Integer
        
        Update_DispatchAdvice(objRCTrx.EnumDispatchAdviceStatus.Cancelled, objDAID, intSuccess)
        If intSuccess = 1 then
            onLoad_Display(objDAID)
            onLoad_DisplayLine(objDAID)
            onLoad_Button()
        Else
            Exit Sub
        End If
    End Sub

    Sub DeleteBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objDAID As New Object()
        Dim intSuccess As Integer

        Update_DispatchAdvice(objRCTrx.EnumDispatchAdviceStatus.Deleted, objDAID, intSuccess)
        If intSuccess = 1 Then
            onLoad_Display(objDAID)
            onLoad_DisplayLine(objDAID)
            onLoad_Button()
        Else
            Exit Sub
        End If
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim strOpCode_DelLine As String = "RC_CLSTRX_DISPATCHADVICE_LINE_DEL"
        Dim strOpCode_GetSumAmount As String = "RC_CLSTRX_DISPATCHADVICE_SUM_LINEAMOUNT_GET"
        Dim strOpCode_UpdTotalAmount As String = "RC_CLSTRX_DISPATCHADVICE_TOTALAMOUNT_UPD"
        Dim strOpCodes = strOpCode_DelLine & "|" & strOpCode_GetSumAmount & "|" & strOpCode_UpdTotalAmount
        Dim strParam As String
        Dim lblDelText As Label
        Dim strDALnID As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("dalnid")
        strDALnID = lblDelText.Text

        Try
            strParam = strDALnID & "|" & strSelectedDAID
            intErrNo = objRCTrx.mtdDelDispatchAdviceLine(strOpCodes, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam)
        Catch Exp As System.Exception
            Response.Redirect ("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_DISPATCHADVICEDET_DEL_LINE&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_DAList.aspx")
        End Try

        onLoad_Display(strSelectedDAID)
        onLoad_DisplayLine(strSelectedDAID)
        onLoad_Button()
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String
        Dim intStatus As Integer
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String
        Dim strDAId As String
        Dim intErrNo As Integer
        Dim strDocType As String

        strDAId = Trim(strSelectedDAID)

        strUpdString = "Where DispAdvID = '" & strDAId & "'"
        strStatus = Trim(lblStatus.Text)
        intStatus = CInt(Trim(lblStatusHidden.Text))
        strDocType = Trim(lblDocTypeHidden.Text)
        strPrintDate = Trim(lblPrintDate.Text)
        strTable = "RC_DISPADV"
        strSortLine = ""


        If intStatus = objRCTrx.EnumDispatchAdviceStatus.Confirmed Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                        strUpdString, _
                                                        strTable, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_SHARE_UPD_PRINTDATE&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_DAList.aspx")
                End Try
                onLoad_Display(strSelectedDAID)
                onLoad_DisplayLine(strSelectedDAID)
                onLoad_Button()
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/RC_Rpt_DADet.aspx?strDAId=" & strDAId & _
                       "&strPrintDate=" & strPrintDate & "&strStatus=" & strStatus & "&strDocType=" & strDocType & "&strSortLine=" & strSortLine & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        If lblReferer.Text = "" Then
            Response.Redirect("RC_trx_DAList.aspx")
        Else
            Response.Redirect(lblReferer.Text)
        End If
    End Sub

End Class

