
Imports System
Imports System.Data
Imports System.Math
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
'Imports Infragistics.WebUI.WebCombo

Public Class PU_RFQ_Detail : Inherits Page

    Protected WithEvents dgPRLn As DataGrid

    Protected WithEvents hidPQID As HtmlInputHidden
    Protected WithEvents PRLnTable As HtmlTable



    Protected WithEvents lblDate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SortExpression As Label

    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtDateDL As TextBox

    Protected WithEvents txtPRID_Plmph As TextBox
    Protected WithEvents txtRemarks As TextBox

    Protected WithEvents lblPurReqID As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblErrGR As Label
    Protected WithEvents Save As Image
    Protected WithEvents Confirm As Image
    Protected WithEvents Cancel As Image
    Protected WithEvents Print As Image
    Protected WithEvents PRDelete As Image
    Protected WithEvents Undelete As Image
    Protected WithEvents Back As Image

    'Protected WithEvents hidPRType As HtmlInputHidden

    Dim strDateFMT As String

    Protected objIN As New agri.IN.clsTrx()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl
    Protected objAdmin As New agri.Admin.clsShare()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLocCodeDs As New Object()

    Protected WithEvents lblErrMesage As Label
    Protected objHR As New agri.HR.clsSetup()
    Dim objSysComp As New agri.Admin.clsComp()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Protected objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim PreBlockTag As String
    Dim BlockTag As String
    Dim objAccDs As New Dataset()
    Dim objBlkDs As New Dataset()
    Dim objVehDs As New Dataset()
    Dim objVehExpDs As New Dataset()
    Dim objPODs As New Object()
    Dim objLangCapDs As New Dataset()
    Dim intConfigSetting As Integer
    Dim strParamName As String
    Dim strParamValue As String

    Dim strOppCd_GET_PRList As String = "IN_CLSTRX_PURREQ_LIST_GET"
    Dim strOppCd_GET_PRLnList As String = "IN_CLSTRX_PURREQLN_LIST_GET"
    Dim strOppCd_UpdPQ As String = "IN_CLSTRX_PURREQ_LIST_UPD"
    Dim strOppCd_DetUpdPQ As String = "IN_CLSTRX_PURREQ_DETAIL_UPD"

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strPhyYear As String
    Dim strPhyMonth As String
    Dim strLastPhyYear As String
    Dim intLevel As Integer

    Const ITEM_PART_SEPERATOR As String = " @ "

    Dim objItemDs As New Object()
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strPRID As String
    Dim strPRQType As String
    Dim dsStkDCItem As DataSet
    Dim pv_strItemCode As String
    Dim intPRLnCount As Integer
    Dim strLocLevel As String
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim prqtype As String

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strPhyYear = Session("SS_PHYYEAR")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyMonth = strAccMonth
        strLastPhyYear = Session("SS_LASTPHYYEAR")
        strLocLevel = Session("SS_LOCLEVEL")
        strLocType = Session("SS_LOCTYPE")
        intLevel = Session("SS_USRLEVEL")
        strDateFMT = Session("SS_DATEFMT")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "ItemCode"
            End If

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            'btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            Save.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Save).ToString())
            Confirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Confirm).ToString())
            Cancel.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Cancel).ToString())
            Print.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Print).ToString())
            PRDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(PRDelete).ToString())
            Undelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Undelete).ToString())
            Back.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Back).ToString())

            strPRID = IIf(Request.QueryString("prid") = "", Request.Form("hidPRID"), Request.QueryString("prid"))

            If Not IsPostBack Then
                Cancel.Visible = False
                'If lblPurReqID.Text <> "" Then
                onProcess_Load("RFQ/CSR/CSER/201607/0001")
                'Else
                txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)

                'End If
            End If
        End If
    End Sub

    Sub onProcess_Load(ByVal pv_strRFQID As String)
        onLoad_DisplayPR(pv_strRFQID)
        onLoad_DisplayPRLn(pv_strRFQID)
    End Sub

    Sub onLoad_DisplayPR(ByVal pv_strRFQID As String)
        Dim strOpCode As String = "PU_CLSTRX_RFQ_LIST_GET"
        Dim ssQLKriteria As String

        ssQLKriteria = "And RfqID='" & pv_strRFQID & "'"
        strParamName = "LOC|STRSEARCH"
        strParamValue = strLocation & "|" & ssQLKriteria


        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try


        If objItemDs.Tables(0).Rows.Count > 0 Then

            lblPurReqID.Text = pv_strRFQID
            txtDate.Text = objGlobal.GetShortDate(strDateFMT, Trim(objItemDs.Tables(0).Rows(0).Item("rfQDate")))
            txtDateDL.Text = objGlobal.GetShortDate(strDateFMT, Trim(objItemDs.Tables(0).Rows(0).Item("rfqDeadline")))
            txtPRID_Plmph.Text = Trim(objItemDs.Tables(0).Rows(0).Item("PRID"))
            'txtRemarks.Text = Trim(objPRDs.Tables(0).Rows(0).Item("Remark"))

            Save.Visible = True            
            Print.Visible = True
            If Trim(objItemDs.Tables(0).Rows(0).Item("Status")) = "1" Then
                Confirm.Visible = True
                PRDelete.Visible = True
                Undelete.Visible = False
            Else
                PRDelete.Visible = False
                Confirm.Visible = False
            End If
        Else
            PRDelete.Visible = False
            Undelete.Visible = False            
            Save.Visible = False
            Confirm.Visible = False
            Print.Visible = False
        End If

    End Sub

    Sub onLoad_DisplayPRLn(ByVal pv_strPRID As String)
        Dim UpdButton As LinkButton
        Dim DelButton As LinkButton
        Dim EdtButton As LinkButton
        Dim CanButton As LinkButton

        dgPRLn.DataSource = LoadData()
        dgPRLn.DataBind()
        PRLnTable.Visible = True
        If objItemDs.Tables(0).Rows.Count > 0 Then
            Confirm.Visible = True
            PRLnTable.Visible = True
            For intCnt = 0 To intPRLnCount - 1
                EdtButton = dgPRLn.Items.Item(intCnt).FindControl("Edit")
                DelButton = dgPRLn.Items.Item(intCnt).FindControl("Delete")
                UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Update")
                CanButton = dgPRLn.Items.Item(intCnt).FindControl("Cancel")

                EdtButton.Visible = False
                DelButton.Visible = True
                UpdButton.Visible = False
                CanButton.Visible = False
                UpdButton.Visible = False
                EdtButton.Visible = True
            Next intCnt

            
        Else
        PRLnTable.Visible = False
        Confirm.Visible = False
        End If
    End Sub

    Protected Function LoadData() As DataSet        
        Dim strOpCode As String = "PU_CLSTRX_RFQ_DETAIL_GET"

        Dim strSearchLocLevel As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object
        Dim StatusLn As String = ""
        Dim PrLevel As String = ""
        Dim sSQLKriteria As String = ""


        strParamName = ""
        strParamValue = ""

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        intPRLnCount = objItemDs.Tables(0).Rows.Count

        Return objItemDs
    End Function

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)       
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_PurReqLn_DEL As String = "PU_CLSTRX_RFQ_DETAIL_DEL"
        Dim strPRID As String = lblPurReqID.Text.Trim

        Dim DelText As Label
        Dim strResultLocCode As String = strLocation
        
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

        dgPRLn.EditItemIndex = CInt(E.Item.ItemIndex)
        DelText = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblRfqLNID")

        strParamName = "RFQLNID|RFQID"
        strParamValue = DelText.Text & "|" & lblPurReqID.Text.Trim

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOppCd_PurReqLn_DEL, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DEL_PURREQLN&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        onProcess_Load(strPRID)
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim Updbutton As LinkButton

        'Dim strRemarks As String = Request.Form("txtRemarks").Trim
        Dim strPRID As String = lblPurReqID.Text.Trim

        Updbutton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Edit")
        Updbutton.Visible = True
        Updbutton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
        Updbutton.Visible = False
        Updbutton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Cancel")
        Updbutton.Visible = False
        Updbutton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
        Updbutton.Visible = True
        onProcess_Load(strPRID)
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim UpdButton As LinkButton
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        Dim EditAddNote As TextBox

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

        dgPRLn.EditItemIndex = CInt(E.Item.ItemIndex)        
        EditAddNote = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtaddNote")
        EditAddNote.Visible = True

        UpdButton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Edit")
        UpdButton.Visible = False
        UpdButton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
        UpdButton.Visible = True
        UpdButton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Cancel")
        UpdButton.Visible = True
        UpdButton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
        UpdButton.Visible = False

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        'Dim strOpCd As String = "IN_CLSTRX_PURREQLN_UPD"
        'Dim strPRID As String = lblPurReqID.Text.Trim
        'Dim objItemDs As New Object()
        'Dim intCnt As Integer = 0
        'Dim intErrNo As Integer
        'Dim intSelectedIndex As Integer = 0
        'Dim strParamName As String = ""
        'Dim strParamValue As String = ""
        'Dim strItemCode As String
        'Dim strQtyApp As String
        'Dim strStatusLn As String
        'Dim strAddNote As String
        'Dim EditItem As Label
        'Dim EditText As TextBox
        'Dim EditStatus As DropDownList
        'Dim strDate As String = Date_Validation(txtDate.Text, False)
        'Dim indDate As String = ""
        'Dim sTrPrLnID As String = ""
        'Dim LnID As Label

        'If CheckDate(txtDate.Text.Trim(), indDate) = False Then
        '    lblDate.Visible = True
        '    lblFmt.Visible = True
        '    lblDate.Text = "<br>Date Entered should be in the format"
        '    Exit Sub
        'End If

        'Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        'Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        'Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        'If Session("SS_FILTERPERIOD") = "0" Then
        '    If intCurPeriod < intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        'Else
        '    If intSelPeriod <> intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        '    If intSelPeriod < intCurPeriod And intLevel < 2 Then
        '        lblDate.Visible = True
        '        lblDate.Text = "This period already locked."
        '        Exit Sub
        '    End If
        'End If

        'EditItem = E.Item.FindControl("ItemCode")
        'strItemCode = EditItem.Text
        'EditText = E.Item.FindControl("lstQtyApp")
        'strQtyApp = EditText.Text
        'EditText = E.Item.FindControl("lstAddNote")
        'strAddNote = EditText.Text
        'EditStatus = E.Item.FindControl("lstStatusLn")
        'strStatusLn = EditStatus.SelectedItem.Value
        'LnID = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("LnID")
        'sTrPrLnID = LnID.Text

        ''sTrPrLnID = dgPRLn.Items.Item(E.Item.ItemIndex).Cells(0).Text

        'strParamName = "PRID|ITEMCODE|QTYAPP|QTYOUTSTANDING|STATUSLN|ADDITIONALNOTE|APPROVEDBY|PRLNID"

        'strParamValue = strPRID & "|" & strItemCode & "|" & strQtyApp & "|" & strQtyApp & "|" & _
        '                strStatusLn & "|" & strAddNote & "|" & intLevel & "|" & sTrPrLnID

        'Try
        '    intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
        '                                            strParamName, _
        '                                            strParamValue)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        'End Try

        'onProcess_Load(strPRID)
        ''BindStkDCList(strPRID)
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim strOppCd_NewPQ As String = "IN_CLSTRX_PURREQ_ADD"
        'Dim strOppCd As String = "IN_CLSTRX_PURREQ_MOVEID"
        'Dim strOppCd_Back As String = "IN_CLSTRX_PURREQ_BACKID"
        'Dim strRemarks As String = Request.Form("txtRemarks").Trim
        'Dim strPrintDate As String
        'Dim strPRStatus As String = objIN.EnumPurReqStatus.Active
        'Dim objPRID As Object
        'Dim strPRID As String = lblPurReqID.Text.Trim
        'Dim strParam As String
        'Dim strOppCd_RDP As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        'Dim strOppCd_GetID As String = "IN_CLSTRX_PURREQ_GETID"
        'Dim strDeptCode As String = ddlDeptCode.SelectedItem.Value
        'Dim strNewIDFormat As String
        'Dim strNewYear As String = ""
        'Dim strTranPrefix As String = "PR"
        'Dim strHistYear As String = ""
        'Dim strRDP As String
        'Dim objCompDs As New Object
        'Dim blnIsDetail As Boolean = True
        'Dim objPRDs As Object
        'Dim strSortExpression As String = "LocCode"
        'Dim strPRLevel As String
        'Dim strPRLevelCode As String
        'Dim strDate As String = Date_Validation(txtDate.Text, False)
        'Dim indDate As String = ""

        'If CheckDate(txtDate.Text.Trim(), indDate) = False Then
        '    lblDate.Visible = True
        '    lblFmt.Visible = True
        '    lblDate.Text = "<br>Date Entered should be in the format"
        '    Exit Sub
        'End If

        'Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        'Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        'Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        'If Session("SS_FILTERPERIOD") = "0" Then
        '    If intCurPeriod < intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        'Else
        '    If intSelPeriod <> intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        '    If intSelPeriod < intCurPeriod And intLevel < 2 Then
        '        lblDate.Visible = True
        '        lblDate.Text = "This period already locked."
        '        Exit Sub
        '    End If
        'End If

        'If lblPurReqID.Text = "" Then
        '    Exit Sub
        'End If


        'Dim arrParam As Array
        'arrParam = Split(lblAccPeriod.Text, "/")
        'If Month(strDate) <> arrParam(0) Then
        '    lblDate.Visible = True
        '    lblDate.Text = "Invalid transaction date."
        '    Exit Sub
        'ElseIf Year(strDate) <> arrParam(1) Then
        '    lblDate.Visible = True
        '    lblDate.Text = "Invalid transaction date."
        '    Exit Sub
        'End If

        'If ddlPRLevel.SelectedItem.Value = "" Then
        '    lblErrPRLevel.Visible = True
        '    Exit Sub
        'Else
        '    lblErrPRLevel.Visible = False
        'End If

        'If strDeptCode = "" Then
        '    lblErrDeptCode.Visible = True
        '    Exit Sub
        'Else
        '    lblErrDeptCode.Visible = False
        'End If

        'strParam = strLocation & "||||" & strSortExpression & "||"

        'Try
        '    intErrNo = objAdminLoc.mtdGetLocCode(strOppCd_RDP, strParam, objLocCodeDs)
        'Catch Exp As Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
        'End Try

        'If objLocCodeDs.Tables(0).Rows.Count > 0 Then
        '    strRDP = objLocCodeDs.Tables(0).Rows(0).Item("RDP")
        'End If

        'If Len(strPhyMonth) = 1 Then
        '    strPhyMonth = "0" & strPhyMonth
        'End If

        'strParam = "where phyyear = '" & Right(Trim(strPhyYear), 2) & "' and tran_prefix = 'PR'" & "|"
        'Try
        '    intErrNo = objIN.mtdGetPurchaseRequest(strOppCd_GetID, _
        '                                           strParam, _
        '                                           objIN.EnumPurReqDocType.StockPR, _
        '                                           strAccMonth, _
        '                                           strAccYear, _
        '                                           Trim(strLocation), _
        '                                           objPRDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        'End Try


        'If objPRDs.Tables(0).Rows.Count > 0 Then
        '    strNewYear = ""
        'Else
        '    strHistYear = Right(strLastPhyYear, 2)
        '    strNewYear = "1"
        'End If

        'Select Case ddlPRLevel.SelectedItem.Value
        '    Case objAdminLoc.EnumLocLevel.HQ
        '        strPRLevelCode = "M"
        '    Case objAdminLoc.EnumLocLevel.Perwakilan
        '        strPRLevelCode = "R"
        '    Case objAdminLoc.EnumLocLevel.Estate
        '        strPRLevelCode = "L"
        '    Case objAdminLoc.EnumLocLevel.Mill
        '        strPRLevelCode = "L"
        'End Select

        'strAccYear = Year(strDate)
        'strAccMonth = Month(strDate)
        'strNewIDFormat = "BOR" & "/" & strCompany & "/" & strLocation & "/" & strPRLevelCode & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"



        'If lblPurReqID.Text = "" Then
        '    If hidPRType.Value <> objIN.EnumPurReqDocType.DirectChargePR Then
        '        strParam = "|" & strRemarks & "|" & lblTotAmtFig.Text & "|" & strPRStatus & "|" & strPrintDate & "|" & PRType.Text & "|" & _
        '           strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
        '           ddlDeptCode.SelectedItem.Value & "|" & ddlPRLevel.SelectedItem.Value & "|" & strDate & "|" & IIf(chkCentralized.Checked = True, "1", "0")
        '    Else
        '        strParam = "|" & strRemarks & "|" & lblTotAmtFig.Text & "|" & strPRStatus & "|" & strPrintDate & "|" & objIN.EnumPurReqDocType.DirectChargePR & "|" & _
        '           strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
        '           ddlDeptCode.SelectedItem.Value & "|" & ddlPRLevel.SelectedItem.Value & "|" & strDate & "|" & IIf(chkCentralized.Checked = True, "1", "0")

        '    End If

        '    Try
        '        intErrNo = objIN.mtdNewPurchaseRequest(strOppCd_NewPQ, _
        '                                               strOppCd_DetUpdPQ, _
        '                                               strOppCd, _
        '                                               strOppCd_Back, _
        '                                               strParam, _
        '                                               strCompany, _
        '                                               Trim(strLocation), _
        '                                               strUserId, _
        '                                               strAccMonth, _
        '                                               strAccYear, _
        '                                               objPRID, _
        '                                               objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
        '    Catch Exp As System.Exception
        '        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NEW_PURREQ_WITHOUT_PRID_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        '    End Try

        '    hidPQID.Value = objPRID
        '    onProcess_Load(hidPQID.Value)
        'Else
        '    If hidPRType.Value <> objIN.EnumPurReqDocType.DirectChargePR Then
        '        strParam = strPRID & "|" & strRemarks & "|" & lblTotAmtFig.Text & "|" & strPRStatus & "|" & strPrintDate & "|" & PRType.Text & "|" & _
        '                           strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
        '                           ddlDeptCode.SelectedItem.Value & "|" & ddlPRLevel.SelectedItem.Value & "|" & strDate & "|" & IIf(chkCentralized.Checked = True, "1", "0")
        '    Else
        '        strParam = strPRID & "|" & strRemarks & "|" & lblTotAmtFig.Text & "|" & strPRStatus & "|" & strPrintDate & "|" & hidPRType.Value & "|" & _
        '                           strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
        '                           ddlDeptCode.SelectedItem.Value & "|" & ddlPRLevel.SelectedItem.Value & "|" & strDate & "|" & IIf(chkCentralized.Checked = True, "1", "0")
        '    End If


        '    Try
        '        intErrNo = objIN.mtdNewPurchaseRequest(strOppCd_NewPQ, _
        '                                              strOppCd_DetUpdPQ, _
        '                                               strOppCd, _
        '                                               strOppCd_Back, _
        '                                               strParam, _
        '                                               strCompany, _
        '                                               Trim(strLocation), _
        '                                               strUserId, _
        '                                               strAccMonth, _
        '                                               strAccYear, _
        '                                               objPRID, _
        '                                               objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
        '    Catch Exp As System.Exception
        '        Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=NEW_PURREQLN_WITH_PRID_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        '    End Try

        '    onProcess_Load(strPRID)
        'End If
    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd As String = "PU_CLSTRX_RFQ_ADD"

        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim blnIsDetail As Boolean = True
        Dim strSortExpression As String = "LocCode"
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

        If lblPurReqID.Text = "" Then
            Exit Sub
        End If

        strParamName = "RFQID|PRID|LOC|USERID|UPDID"
        strParamValue = lblPurReqID.Text & "|" & txtPRID_Plmph.Text & "|" & strLocation & "|" & strUserId & "|" & strUserId

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOppCd, _
                                                strParamName, _
                                                strParamValue)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
        End Try

        onProcess_Load(lblPurReqID.Text)

        If lblPurReqID.Text.Trim <> "" Then
            UserMsgBox(Me, "Generate Succses...!!!")
        End If
    End Sub

    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim strPRID As String = Trim(lblPurReqID.Text)
        'Dim strStatus As Integer = objIN.EnumPurReqStatus.Confirmed
        'Dim objPRDs As DataSet
        'Dim objPRID As Object
        'Dim strTotalAmt As String = lblTotAmtFig.Text.Trim
        'Dim lblRemarks As Label
        'Dim strRemarks As String = Request.Form("txtRemarks").Trim
        'Dim strParam As String
        'Dim strParamTemp As String
        'Dim strResultLocCode As String = strLocation
        'Dim strDate As String = Date_Validation(txtDate.Text, False)
        'Dim indDate As String = ""

        'If CheckDate(txtDate.Text.Trim(), indDate) = False Then
        '    lblDate.Visible = True
        '    lblFmt.Visible = True
        '    lblDate.Text = "<br>Date Entered should be in the format"
        '    Exit Sub
        'End If

        'Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        'Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        'Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        'If Session("SS_FILTERPERIOD") = "0" Then
        '    If intCurPeriod < intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        'Else
        '    If intSelPeriod <> intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        '    If intSelPeriod < intCurPeriod And intLevel < 2 Then
        '        lblDate.Visible = True
        '        lblDate.Text = "This period already locked."
        '        Exit Sub
        '    End If
        'End If

        'Dim arrParam As Array
        'arrParam = Split(lblAccPeriod.Text, "/")
        'If Month(strDate) <> arrParam(0) Then
        '    lblDate.Visible = True
        '    lblDate.Text = "Invalid transaction date."
        '    Exit Sub
        'ElseIf Year(strDate) <> arrParam(1) Then
        '    lblDate.Visible = True
        '    lblDate.Text = "Invalid transaction date."
        '    Exit Sub
        'End If

        'If hidPRType.Value <> objIN.EnumPurReqDocType.DirectChargePR Then
        '    strParamTemp = strPRID & "|" & strRemarks & "|" & strStatus & "|" & strTotalAmt & "|" & PRType.Text & "|" & ddlPRLevel.SelectedItem.Value
        'Else
        '    strParamTemp = strPRID & "|" & strRemarks & "|" & strStatus & "|" & strTotalAmt & "|" & hidPRType.Value & "|" & ddlPRLevel.SelectedItem.Value
        'End If

        'If ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Estate Or ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Mill Then
        '    strParamTemp = strParamTemp & "|" & strLocation
        'ElseIf ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Perwakilan Then
        '    GetLocCode(objAdminLoc.EnumLocLevel.Perwakilan, strResultLocCode)
        '    'to capture not online PR
        '    If chkCentralized.Checked = False Then
        '        strParamTemp = strParamTemp & "|" & strLocation
        '    Else
        '        strParamTemp = strParamTemp & "|" & strResultLocCode
        '    End If

        'ElseIf ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.HQ Then
        '    GetLocCode(objAdminLoc.EnumLocLevel.HQ, strResultLocCode)
        '    'to capture not online PR
        '    If chkCentralized.Checked = False Then
        '        strParamTemp = strParamTemp & "|" & strLocation
        '    Else
        '        strParamTemp = strParamTemp & "|" & strResultLocCode
        '    End If
        'Else
        '    strParamTemp = strParamTemp & "|" & strLocation
        'End If

        'If ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Estate Or ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Mill Then
        '    strLocation = Trim(strLocation)
        'ElseIf ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Perwakilan Then
        '    GetLocCode(objAdminLoc.EnumLocLevel.Perwakilan, strResultLocCode)
        '    'to capture not online PR
        '    If chkCentralized.Checked = False Then
        '        strLocation = Trim(strLocation)
        '    Else
        '        strLocation = Trim(strResultLocCode)
        '    End If
        'ElseIf ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.HQ Then
        '    GetLocCode(objAdminLoc.EnumLocLevel.HQ, strResultLocCode)
        '    'to capture not online PR
        '    If chkCentralized.Checked = False Then
        '        strLocation = Trim(strLocation)
        '    Else
        '        strLocation = Trim(strResultLocCode)
        '    End If
        'Else
        '    strLocation = Trim(strLocation)
        'End If



        'Try
        '    intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
        '                                           strParamTemp, _
        '                                           strCompany, _
        '                                           Trim(strLocation), _
        '                                           strUserId, _
        '                                           objPRID, _
        '                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_CONFIRM&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        'End Try
        'strLocation = strResultLocCode
        'onProcess_Load(strPRID)
    End Sub

    Sub btnPRDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim objPRDs As DataSet
        'Dim objPRLnDs As DataSet
        'Dim objPRID As Object
        'Dim strPRID As String = Trim(lblPurReqID.Text)
        'Dim strPRStatus As Integer = objIN.EnumPurReqStatus.Deleted
        'Dim strRemarks As String = Trim(txtRemarks.Text)
        'Dim strTotalAmt As String = Trim(lblTotAmtFig.Text)
        'Dim lblRemarks As Label
        'Dim strParamTemp As String
        'Dim strParam As String
        'Dim strResultLocCode As String = strLocation
        'Dim strDate As String = Date_Validation(txtDate.Text, False)
        'Dim indDate As String = ""
        'Dim IntAppLevel As Integer

        'If CheckDate(txtDate.Text.Trim(), indDate) = False Then
        '    lblDate.Visible = True
        '    lblFmt.Visible = True
        '    lblDate.Text = "<br>Date Entered should be in the format"
        '    Exit Sub
        'End If

        'Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        'Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        'Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        'If Session("SS_FILTERPERIOD") = "0" Then
        '    If intCurPeriod < intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        'Else
        '    If intSelPeriod <> intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        '    If intSelPeriod < intCurPeriod And intLevel < 2 Then
        '        lblDate.Visible = True
        '        lblDate.Text = "This period already locked."
        '        Exit Sub
        '    End If
        'End If

        'strParam = strPRID & "|" & strRemarks & "|" & strPRStatus & "|" & strTotalAmt & "|" & PRType.Text & "|" & ddlPRLevel.SelectedItem.Value

        'strParam = strParam & "|" & strLocation

        'If strLocLevel = objAdminLoc.EnumLocLevel.Estate Then
        '    strLocation = strLocation
        'ElseIf strLocLevel = objAdminLoc.EnumLocLevel.Perwakilan Then
        '    GetLocCode(objAdminLoc.EnumLocLevel.Perwakilan, strResultLocCode)
        '    strLocation = strResultLocCode
        'ElseIf strLocLevel = objAdminLoc.EnumLocLevel.HQ Then
        '    GetLocCode(objAdminLoc.EnumLocLevel.HQ, strResultLocCode)
        '    strLocation = strResultLocCode
        'Else
        '    strLocation = strLocation
        'End If

        'Try
        '    intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
        '                                           strParam, _
        '                                           strCompany, _
        '                                           Trim(strLocation), _
        '                                           strUserId, _
        '                                           objPRID, _
        '                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_DELETE_1REC&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        'End Try

        'onProcess_Load(strPRID)
    End Sub

    Sub btnPRUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim objPRDs As DataSet
        'Dim strPRID As String = Trim(lblPurReqID.Text)
        'Dim strPRStatus As Integer = objIN.EnumPurReqStatus.Active
        'Dim objPRID As Object
        'Dim strTotalAmt As String = Trim(lblTotAmtFig.Text)
        'Dim strRemarks As String = Trim(txtRemarks.Text)
        'Dim strParam As String

        'Dim strResultLocCode As String = strLocation
        'Dim strDate As String = Date_Validation(txtDate.Text, False)
        'Dim indDate As String = ""

        'If CheckDate(txtDate.Text.Trim(), indDate) = False Then
        '    lblDate.Visible = True
        '    lblFmt.Visible = True
        '    lblDate.Text = "<br>Date Entered should be in the format"
        '    Exit Sub
        'End If

        'Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        'Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        'Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        'If Session("SS_FILTERPERIOD") = "0" Then
        '    If intCurPeriod < intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        'Else
        '    If intSelPeriod <> intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        '    If intSelPeriod < intCurPeriod And intLevel < 2 Then
        '        lblDate.Visible = True
        '        lblDate.Text = "This period already locked."
        '        Exit Sub
        '    End If
        'End If


        'strParam = strPRID & "|" & strRemarks & "|" & strPRStatus & "|" & strTotalAmt & "|" & PRType.Text & "|" & ddlPRLevel.SelectedItem.Value

        'strParam = strParam & "|" & strLocation

        'If ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Estate Then
        '    strLocation = strLocation
        'ElseIf ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.Perwakilan Then
        '    GetLocCode(objAdminLoc.EnumLocLevel.Perwakilan, strResultLocCode)
        '    strLocation = strResultLocCode
        'ElseIf ddlPRLevel.SelectedItem.Value = objAdminLoc.EnumLocLevel.HQ Then
        '    GetLocCode(objAdminLoc.EnumLocLevel.HQ, strResultLocCode)
        '    strLocation = strResultLocCode
        'Else
        '    strLocation = strLocation
        'End If

        'Try
        '    intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
        '                                           strParam, _
        '                                           strCompany, _
        '                                           Trim(strLocation), _
        '                                           strUserId, _
        '                                           objPRID, _
        '                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        'End Try
        'onProcess_Load(strPRID)
    End Sub

    Sub btnCancel_click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim objPRDs As DataSet
        'Dim strPRID As String = Trim(lblPurReqID.Text)
        'Dim strPRStatus As Integer = objIN.EnumPurReqStatus.Cancelled
        'Dim objPRID As Object
        'Dim strTotalAmt As String = Trim(lblTotAmtFig.Text)
        'Dim strRemarks As String = Trim(txtRemarks.Text)
        'Dim strParam As String
        'Dim strResultLocCode As String = strLocation
        'Dim strDate As String = Date_Validation(txtDate.Text, False)
        'Dim indDate As String = ""

        'If CheckDate(txtDate.Text.Trim(), indDate) = False Then
        '    lblDate.Visible = True
        '    lblFmt.Visible = True
        '    lblDate.Text = "<br>Date Entered should be in the format"
        '    Exit Sub
        'End If

        'Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        'Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        'Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        'If Session("SS_FILTERPERIOD") = "0" Then
        '    If intCurPeriod < intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        'Else
        '    If intSelPeriod <> intInputPeriod Then
        '        lblDate.Visible = True
        '        lblDate.Text = "Invalid transaction date."
        '        Exit Sub
        '    End If
        '    If intSelPeriod < intCurPeriod And intLevel < 2 Then
        '        lblDate.Visible = True
        '        lblDate.Text = "This period already locked."
        '        Exit Sub
        '    End If
        'End If

        'strParam = strPRID & "|" & strRemarks & "|" & strPRStatus & "|" & strTotalAmt & "|" & PRType.Text & "|" & ddlPRLevel.SelectedItem.Value
        'strParam = strParam & "|" & strLocation

        'Try
        '    intErrNo = objIN.mtdUpdPurchaseRequest(strOppCd_UpdPQ, _
        '                                           strParam, _
        '                                           strCompany, _
        '                                           Trim(strLocation), _
        '                                           strUserId, _
        '                                           objPRID, _
        '                                           objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.PurchaseRequest))
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_UPD_CANCEL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        'End Try

        'onProcess_Load(strPRID)
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        'Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        'Dim strUpdString As String = ""
        'Dim strStatus As String
        'Dim intStatus As Integer
        'Dim strSortLine As String
        'Dim strPrintDate As String
        'Dim strTable As String

        'strUpdString = "where PRID = '" & strPRID & "'"
        'strStatus = Trim(Status.Text)
        'intStatus = CInt(lblStatus.Text.Trim)
        'strPrintDate = lblPrintDate.Text.Trim
        'strSortLine = "PRln.PRID"
        'strTable = "IN_PR"

        'If intStatus = objIN.EnumPurReqStatus.Confirmed Then
        '    If strPrintDate = "" Then
        '        Try
        '            intErrNo = objAdmin.mtdUpdPrintDate(strOpCodePrint, _
        '                                                strUpdString, _
        '                                                strTable, _
        '                                                strCompany, _
        '                                                Trim(strLocation), _
        '                                                strUserId)
        '        Catch Exp As System.Exception
        '            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=IN_PURREQ_DETAILS_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        '        End Try

        '    Else
        '        strStatus = strStatus & " (re-printed)"
        '    End If
        'End If
        'onLoad_DisplayPR(lblPurReqID.Text)
        'Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_PurReqDet.aspx?strPRID=" & lblPurReqID.Text & _
        '               "&strPrintDate=" & strPrintDate & "&strStatus=" & strStatus & "&strSortLine=" & strSortLine & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("IN_PurReq_APP.aspx")
    End Sub

    Sub GetLocCode(ByVal pv_strLocLevel As String, ByRef pv_strResult As String)
        Dim strOpCd_GetLocLevel As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim objLocCodeDs As New Object()

        strParam = " and a.loclevel = '" & pv_strLocLevel & "'"

        Try
            intErrNo = objAdminLoc.mtdGetDataLocCode(strOpCd_GetLocLevel, strParam, objLocCodeDs)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
        End Try

        If objLocCodeDs.Tables(0).Rows.Count > 0 Then
            pv_strResult = objLocCodeDs.Tables(0).Rows(0).Item("LocCode")
        End If
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GRDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_GRList.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = objLangCapDs.Tables(0).Rows(count).Item("TermCode").trim() Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
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
End Class
