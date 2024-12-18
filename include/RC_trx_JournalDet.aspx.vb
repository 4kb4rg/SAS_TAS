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

Public Class RC_trx_JournalDet : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblJournalId As Label
    Protected WithEvents lblDocType As Label
    Protected WithEvents lblDocTypeValue As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents txtJournalRefNo As TextBox
    Protected WithEvents ddlDocument As DropDownList
    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents txtQty As TextBox
    Protected WithEvents txtCost As TextBox
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents ConfirmBtn As ImageButton
    Protected WithEvents PrintBtn As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents DeleteBtn As ImageButton
    Protected WithEvents BackBtn As ImageButton
    Protected WithEvents jrnid As HtmlInputHidden
    Protected WithEvents lblStatusHidden As Label
    Protected WithEvents lblDocTypeHidden As Label
    Protected WithEvents lblErrLocation As Label
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrDescription As Label
    Protected WithEvents lblErrTotalUnits As Label
    Protected WithEvents lblErrRate As Label
    Protected WithEvents lblErrAmount As Label
    Protected WithEvents lblErrTotal As Label
    Protected WithEvents lblReferer As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblLocation AS Label
    Protected WithEvents lblAccount As Label

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objPUTrx As New agri.PU.clsTrx()
    Dim objRCTrx As New agri.RC.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objINSetup As New agri.IN.clsSetup()

    Dim objRCDs As New Object()
    Dim objRCLnDs As New Object()
    Dim objAccDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFormat As String
    Dim intADAR As Integer

    Dim strSelectedJrnID As String
    Dim intJrnStatus As Integer
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCJournal), intADAR) = False Then
            Response.Redirect ("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrLocation.Visible = False
            lblErrAccount.Visible = False
            lblErrDescription.Visible = False
            lblErrTotalUnits.Visible = False
            lblErrRate.Visible = False
            lblErrAmount.Visible = False
            lblErrTotal.Visible = False
            lblReferer.Text = Request.QueryString("referer")
            strSelectedJrnID = Trim(IIf(Request.QueryString("jrnid") = "", Request.Form("jrnid"), Request.QueryString("jrnid")))
            jrnid.Value = strSelectedJrnID
            
            If Not IsPostBack Then
                If strSelectedJrnID <> "" Then
                    onLoad_Display(strSelectedJrnID)
                    onLoad_DisplayLine(strSelectedJrnID)
                    onLoad_Button()
                Else
                    BindLocation("")
                    BindAccount("")
                    onLoad_Button()
                End If
            End If
         End If
    End Sub



    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.text = GetCaption(objLangCap.EnumLangCap.Location)
        lblAccount.text = GetCaption(objLangCap.EnumLangCap.Account)
        
        lblErrLocation.text = lblPleaseSelect.text & lblLocation.text
        lblErrAccount.text = "<br>" & lblPleaseSelect.text & lblAccount.text & lblCode.text

        dgLineDet.Columns(0).HeaderText = lblAccount.text & lblCode.text
        
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_JOURNALDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=rc/trx/RC_trx_JournalDet.aspx")
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
        txtJournalRefNo.Enabled = False
        ddlDocument.Enabled = False
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
                   Case objRCTrx.EnumJournalStatus.Active
                        txtJournalRefNo.Enabled = True
                        ddlDocument.Enabled = True
                        ddlLocation.Enabled = True
                        tblSelection.Visible = True
                        SaveBtn.Visible = True
                        ConfirmBtn.Visible = True
                        DeleteBtn.Visible = True
                        DeleteBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                        PrintBtn.Visible = True
                   Case objRCTrx.EnumJournalStatus.Confirmed
                        CancelBtn.Visible = True
                        PrintBtn.Visible = True
                   Case Else
            End Select
        Else
            txtJournalRefNo.Enabled = True
            ddlDocument.Enabled = True
            ddlLocation.Enabled = True
            tblSelection.Visible = True
            SaveBtn.Visible = True
            ConfirmBtn.Visible = True
        End If
    End Sub

    Sub onLoad_Display(ByVal pv_strJrnID As String)
        Dim strOpCd_Get As String = "RC_CLSTRX_JOURNAL_DETAILS_GET"
        Dim objRCDs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String = pv_strJrnID
        Dim intCnt As Integer = 0

        jrnid.Value = pv_strJrnID

        Try
            intErrNo = objRCTrx.mtdGetJournal(strOpCd_Get, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objRCDs, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect ("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_JOURNALDET_GET_HEADER&errmesg=" & Exp.ToString & "&redirect=RC/trx/RC_trx_JournalList.aspx")
        End Try

        lblJournalId.Text = pv_strJrnID
        txtJournalRefNo.Text = objRCDs.Tables(0).Rows(0).Item("DocRefNo").Trim()
        lblDocTypeValue.Text = objRCTrx.mtdGetJournalDocType(CInt(objRCDs.Tables(0).Rows(0).Item("JournalType")))
        lblDocTypeHidden.Text = Trim(objRCDs.Tables(0).Rows(0).Item("JournalType"))
        lblAccPeriod.Text = Trim(objRCDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objRCDs.Tables(0).Rows(0).Item("AccYear"))
        lblStatus.Text = objRCTrx.mtdGetDispatchAdviceStatus(Trim(objRCDs.Tables(0).Rows(0).Item("Status")))
        intJrnStatus = CInt(Trim(objRCDs.Tables(0).Rows(0).Item("Status")))
        lblStatusHidden.Text = intJrnStatus
        lblDateCreated.Text = objGlobal.GetLongDate(objRCDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objRCDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblPrintDate.Text = objGlobal.GetLongDate(objRCDs.Tables(0).Rows(0).Item("PrintDate"))
        lblUpdatedBy.Text = Trim(objRCDs.Tables(0).Rows(0).Item("UserName"))
        lblTotalAmount.Text = FormatNumber(objRCDs.Tables(0).Rows(0).Item("TotalAmount"), 2)

        ddlDocument.SelectedIndex = CInt(objRCDs.Tables(0).Rows(0).Item("Document")) - 1
        BindLocation(objRCDs.Tables(0).Rows(0).Item("ToLocCode").Trim())
        BindAccount("")
    End Sub


    Sub onLoad_DisplayLine(ByVal pv_strJrnID As String)
        Dim strOpCd_GetLine As String = "RC_CLSTRX_JOURNAL_LINE_GET"
        Dim strParam As String = pv_strJrnID
        Dim lbButton As LinkButton
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Try
            intErrNo = objRCTrx.mtdGetJournalLine(strOpCd_GetLine, strParam, objRCLnDs)
        Catch Exp As System.Exception
            Response.Redirect ("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_JOURNALDET_GET_LINE&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_JournalList.aspx")
        End Try

        For intCnt = 0 To objRCLnDs.Tables(0).Rows.Count - 1
            objRCLnDs.Tables(0).Rows(intCnt).Item("JournalLnID") = Trim(objRCLnDs.Tables(0).Rows(intCnt).Item("JournalLnID"))
            objRCLnDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objRCLnDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objRCLnDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objRCLnDs.Tables(0).Rows(intCnt).Item("Description"))
        Next intCnt
        
        dgLineDet.DataSource = objRCLnDs.Tables(0)
        dgLineDet.DataBind()

        For intCnt = 0 To objRCLnDs.Tables(0).Rows.Count - 1
            Select Case intJrnStatus
                Case objRCTrx.EnumJournalStatus.Active
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RC_JOURNALDET_GET_LOC&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_JournalList.aspx")
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

    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim objAccDs As New Dataset()
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "' And ACC.AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_JOURNALDET_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(objAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = lblSelect.text & lblAccount.text & lblCode.text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = objAccDs.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "Description"
        ddlAccount.DataBind()
        ddlAccount.SelectedIndex = intSelectedIndex

        objAccDs = Nothing
    End Sub


    Sub Update_Journal(ByVal pv_intStatus As Integer, ByRef pr_objNewJrnID As Object, ByRef pr_intSuccess As Integer)
        Dim strOpCd_AddJrn As String = "RC_CLSTRX_JOURNAL_ADD"
        Dim strOpCd_UpdJrn As String = "RC_CLSTRX_JOURNAL_UPD"
        Dim strOpCodes As String = strOpCd_AddJrn & "|" & _
                                   strOpCd_UpdJrn
        Dim intErrNo As Integer
        Dim strParam As String = ""

        pr_intSuccess = 1

        If ddlLocation.SelectedItem.Value = "" Then
            lblErrLocation.Visible = True
            pr_intSuccess = 0
            Exit Sub
        End If

        If lblDocTypeHidden.Text = "" Then
            lblDocTypeHidden.Text = objRCTrx.EnumJournalDocType.Manual
        End If

        Try
            strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.RCJournal) & "|" & _
                       strSelectedJrnID & "|" & _
                       txtJournalRefNo.Text & "|" & _
                       ddlDocument.SelectedItem.Value & "|" & _
                       ddlLocation.SelectedItem.Value & "|" & _
                       lblDocTypeHidden.Text & "|" & _
                       lblTotalAmount.Text & "|" & _
                       pv_intStatus
            intErrNo = objRCTrx.mtdUpdJournal(strOpCodes, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            pr_objNewJrnID)
        Catch Exp As System.Exception
            Response.Redirect ("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_JOURNALDET_UPD_DATA&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_JournalList.aspx")
        End Try
        pr_objNewJrnID = IIf(strSelectedJrnID = "", pr_objNewJrnID, strSelectedJrnID)
    End Sub


    Sub AddBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objJrnId As New Object()
        Dim strAccCode As String = Request.Form("ddlAccount")
        Dim ddlQty As Double
        Dim dblRate As Double
        Dim dblAmount As Double
        Dim strOpCode_AddLine As String = "RC_CLSTRX_JOURNAL_LINE_ADD"
        Dim strOpCode_GetSumAmount As String = "RC_CLSTRX_JOURNAL_SUM_LINEAMOUNT_GET"
        Dim strOpCode_UpdTotalAmount As String = "RC_CLSTRX_JOURNAL_TOTALAMOUNT_UPD"
        Dim strOpCodes As String = strOpCode_AddLine & "|" & strOpCode_GetSumAmount & "|" & strOpCode_UpdTotalAmount
        Dim intErrNo As Integer
        Dim intSuccess As Integer

        If (strAccCode = "") Then
            lblErrAccount.Visible = True
            Exit Sub
        ElseIf (txtDescription.Text = "") Then
            lblErrDescription.Visible = True
            Exit sub
        End If
        If Trim(txtQty.Text) = "" Then
            lblErrTotalUnits.Visible = True
            Exit Sub
        Else
            ddlQty = CDbl(txtQty.Text)
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

        If strSelectedJrnID = "" Then
            Update_Journal(objRCTrx.EnumJournalStatus.Active, objJrnId, intSuccess)
            If intSuccess = 1 Then
                If UCase(TypeName(objJrnId)) = "OBJECT" Then
                    Exit Sub
                Else
                    strSelectedJrnID = objJrnId
                End If
            Else
                Exit Sub
            End If
        End If

        Try
            Dim strParam As String = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.RCJournalLn) & "|" & _
                                    strSelectedJrnID & "|" & _
                                    strAccCode & "|" & _
                                    txtDescription.Text & "|" & _
                                    ddlQty & "|" & _
                                    dblRate & "|" & _
                                    dblAmount
            intErrNo = objRCTrx.mtdUpdJournalLine(strOpCodes, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam)
        Catch Exp As System.Exception
            Response.Redirect ("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_JOURNALDET_ADD_LINEA&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_JournalList.aspx")
        End Try

        onLoad_Display(strSelectedJrnID)
        onLoad_DisplayLine(strSelectedJrnID)
        onLoad_Button()
    End Sub

    Sub SaveBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objJrnId As String
        Dim intSuccess As Integer
        
        Update_Journal(objRCTrx.EnumJournalStatus.Active, objJrnId, intSuccess)
        If intSuccess = 1 Then
            onLoad_Display(objJrnId)
            onLoad_DisplayLine(objJrnId)
            onLoad_Button()
        Else
            Exit Sub
        End If
    End Sub

    Sub ConfirmBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objJrnId As New Object()
        Dim intSuccess As Integer
        
        If CDbl(lblTotalAmount.Text) <= 0 Then
            lblErrTotal.Visible = True
        Else
            Update_Journal(objRCTrx.EnumJournalStatus.Confirmed, objJrnId, intSuccess)
            If intSuccess = 1 Then
                onLoad_Display(objJrnId)
                onLoad_DisplayLine(objJrnId)
                onLoad_Button()
            Else
                Exit Sub
            End If
        End If
    End Sub

    Sub CancelBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objJrnId As New Object()
        Dim intSuccess As Integer
        
        Update_Journal(objRCTrx.EnumJournalStatus.Cancelled, objJrnId, intSuccess)
        If intSuccess = 1 then
            onLoad_Display(objJrnId)
            onLoad_DisplayLine(objJrnId)
            onLoad_Button()
        Else
            Exit Sub
        End If
    End Sub

    Sub DeleteBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim objJrnId As New Object()
        Dim intSuccess As Integer

        Update_Journal(objRCTrx.EnumJournalStatus.Deleted, objJrnId, intSuccess)
        If intSuccess = 1 Then
            onLoad_Display(objJrnId)
            onLoad_DisplayLine(objJrnId)
            onLoad_Button()
        Else
            Exit Sub
        End If
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim strOpCode_DelLine As String = "RC_CLSTRX_JOURNAL_LINE_DEL"
        Dim strOpCode_GetSumAmount As String = "RC_CLSTRX_JOURNAL_SUM_LINEAMOUNT_GET"
        Dim strOpCode_UpdTotalAmount As String = "RC_CLSTRX_JOURNAL_TOTALAMOUNT_UPD"
        Dim strOpCodes = strOpCode_DelLine & "|" & strOpCode_GetSumAmount & "|" & strOpCode_UpdTotalAmount
        Dim strParam As String
        Dim lblDelText As Label
        Dim strJrnLnID As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("jrnlnid")
        strJrnLnID = lblDelText.Text

        Try
            strParam = strJrnLnID & "|" & strSelectedJrnID
            intErrNo = objRCTrx.mtdDelJournalLine(strOpCodes, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam)
        Catch Exp As System.Exception
            Response.Redirect ("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_JOURNALDET_DEL_LINE&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_JournalList.aspx")
        End Try

        onLoad_Display(strSelectedJrnID)
        onLoad_DisplayLine(strSelectedJrnID)
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
        Dim strJrnID As String
        Dim intErrNo As Integer
        Dim strDocType As String

        strJrnID = Trim(strSelectedJrnID)

        strUpdString = "Where JournalID = '" & strJrnID & "'"
        strStatus = Trim(lblStatus.Text)
        intStatus = CInt(Trim(lblStatusHidden.Text))
        strDocType = Trim(lblDocTypeHidden.Text)
        strPrintDate = Trim(lblPrintDate.Text)
        strTable = "RC_JOURNAL"
        strSortLine = ""


        If intStatus = objRCTrx.EnumJournalStatus.Confirmed Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
                                                        strUpdString, _
                                                        strTable, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_SHARE_UPD_PRINTDATE&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_JournalList.aspx")
                End Try
                onLoad_Display(strSelectedJrnID)
                onLoad_DisplayLine(strSelectedJrnID)
                onLoad_Button()
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/RC_Rpt_DADet.aspx?strJrnID=" & strJrnID & _
                       "&strPrintDate=" & strPrintDate & "&strStatus=" & strStatus & "&strDocType=" & strDocType & "&strSortLine=" & strSortLine & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        If lblReferer.Text = "" Then
            Response.Redirect("RC_trx_JournalList.aspx")
        Else
            Response.Redirect(lblReferer.Text)
        End If
    End Sub

End Class

