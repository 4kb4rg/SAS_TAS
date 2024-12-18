
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

Public Class PU_Trx_RFQ_Detail : Inherits Page

    Protected WithEvents dgPRLn As DataGrid

    Protected WithEvents hidPQID As HtmlInputHidden
    Protected WithEvents PRLnTable As HtmlTable
    Protected WithEvents BtnViewPR As HtmlInputButton

    Protected WithEvents LblStatus As Label
    Protected WithEvents LblStatusDesc As Label
    Protected WithEvents lblRemark As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SortExpression As Label

    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtDateDL As TextBox

    Protected WithEvents txtSupCode As TextBox
    Protected WithEvents txtSupName As TextBox
    Protected WithEvents txtPRID_Plmph As TextBox
    Protected WithEvents txtDeliverTo As TextBox
    Protected WithEvents txtDevDate As TextBox
    Protected WithEvents txtPlandDevDate As TextBox
    Protected WithEvents txtTermOfPay As TextBox
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


    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPUAR As Integer
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

#Region "TOOLS & COMPONENT"
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim prqtype As String

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder), intPUAR) = False Then
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

            If Not IsPostBack Then
                txtPRID_Plmph.Attributes.Add("readonly", "readonly")
                txtSupCode.Attributes.Add("readonly", "readonly")
                txtSupName.Attributes.Add("readonly", "readonly")
                lblPurReqID.Text = Request.QueryString("RFQID")
                If lblPurReqID.Text <> "" Then
                    onProcess_Load(lblPurReqID.Text)
                Else
                    Cancel.Visible = False
                    Save.Visible = False
                    Confirm.Visible = False
                    PRDelete.Visible = False
                    Undelete.Visible = False
                    Print.Visible = False
                    txtRemarks.Visible = False
                    lblRemark.Visible = True
                    txtDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    txtDateDL.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                End If
            End If
            End If
    End Sub

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
        'Dim EditAddNote As TextBox
        Dim EditQty As TextBox
        Dim Updbutton As LinkButton

        'EditAddNote = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtaddNote")
        'EditAddNote.Visible = False

        EditQty = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtQtyReg")
        EditQty.Visible = False

        Updbutton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Edit")
        Updbutton.Visible = True
        Updbutton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
        Updbutton.Visible = False
        Updbutton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Cancel")
        Updbutton.Visible = False
        Updbutton = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
        Updbutton.Visible = True

        onProcess_Load(lblPurReqID.Text)
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim UpdButton As LinkButton
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        Dim EditAddNote As TextBox
        Dim EditQty As TextBox
        Dim nQtyOrder As String

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

        nQtyOrder = CType(dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyReqDisplay"), Label).Text

        EditQty = dgPRLn.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtQtyReg")
        EditQty.Visible = True
        EditQty.Text = Replace(Replace(nQtyOrder, ".", ""), ",", ".")

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
        Dim strOpCd As String = "PU_CLSTRX_RFQ_DETAIL_UPD"
        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strItemCode As String
        Dim strQtyApp As String        
        Dim strAddNote As String
        Dim EditItem As Label
        Dim EditText As TextBox
        Dim EditNote As TextBox
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim indDate As String = ""
        Dim sTrPrLnID As String = ""
        Dim LnID As Label

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


        EditNote = E.Item.FindControl("txtaddNote")
        'strAddNote = EditNote.Text

        EditItem = E.Item.FindControl("ItemCode")
        strItemCode = EditItem.Text
        EditText = E.Item.FindControl("txtQtyReg")
        strQtyApp = EditText.Text
    
        LnID = E.Item.FindControl("lblRfqLNID")
        sTrPrLnID = LnID.Text

        strParamName = "STRSEARCH|RFQID|RFQLNID|ITEMCODE"
        strParamValue = "SET Qty=" & strQtyApp & _
                            ",AdditionalNote='" & strAddNote & "'|" & lblPurReqID.Text & "|" & sTrPrLnID & "|" & strItemCode

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        onProcess_Load(strPRID)        
    End Sub

    Sub NewSuppBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_Trx_RFQ_Detail.aspx")
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd As String = "PU_CLSTRX_RFQ_UPD"

        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim blnIsDetail As Boolean = True
        Dim strSortExpression As String = "LocCode"
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strDateDL As String = Date_Validation(txtDateDL.Text, False)
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

        If txtSupCode.Text = "" Then
            UserMsgBox(Me, "Please Input Supplier...!!!")
            txtSupCode.Focus()
            Exit Sub
        End If


        If lblPurReqID.Text = "" Then
            UserMsgBox(Me, "No Record found...!!!")
            Exit Sub
        End If

        strParamName = "STRSEARCH|RFQID"
        strParamValue = "SET SupplierCode='" & txtSupCode.Text & _
                            "',UpdateDate='" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & _
                            "',UpdateID='" & strUserId & _
                            "',DeliverTo='" & txtDeliverTo.Text & _
                            "',DelvDate='" & txtDevDate.Text & _
                            "',PlandDelvDate='" & txtPlandDevDate.Text & _
                            "',TermOfPayment='" & txtTermOfPay.Text & _
                            "',RfqDate='" & strDate & _
                            "',RfqDeadLine='" & strDateDL & _
                            "',Remark='" & txtRemarks.Text.Trim & "'" & "|" & lblPurReqID.Text

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOppCd, _
                                                strParamName, _
                                                strParamValue)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
            Exit Sub
        End Try

        onProcess_Load(lblPurReqID.Text)

        If lblPurReqID.Text.Trim <> "" Then
            UserMsgBox(Me, "Generate Succses...!!!")
        End If
    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd As String = "PU_CLSTRX_RFQ_ADD"
        Dim objInserts As New Object()
        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim blnIsDetail As Boolean = True
        Dim strSortExpression As String = "LocCode"
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strDateDL As String = Date_Validation(txtDateDL.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        If CheckDate(txtDateDL.Text.Trim(), indDate) = False Then
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

        If txtSupCode.Text = "" Then
            UserMsgBox(Me, "Please Input Supplier...!!!")
            txtSupCode.Focus()
            Exit Sub
        End If

        strParamName = "RFQID|PRID|SUPCODE|LOC|USERID|UPDID|CDATE|DEADLINE|DELTO|DEVDATE|PLDEVDATE|TERM|REMARK"
        strParamValue = lblPurReqID.Text & _
                        "|" & txtPRID_Plmph.Text & _
                        "|" & txtSupCode.Text & _
                        "|" & strLocation & _
                        "|" & strUserId & _
                        "|" & strUserId & _
                        "|" & strDate & _
                        "|" & strDateDL & _
                        "|" & txtDeliverTo.Text & _
                        "|" & txtDevDate.Text & _
                        "|" & txtPlandDevDate.Text & _
                        "|" & txtTermOfPay.Text & _
                        "|" & txtRemarks.Text

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOppCd, _
                                                strParamName, _
                                                strParamValue, objInserts)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
            Exit Sub
        End Try

        If objInserts.Tables(0).Rows.Count > 0 Then
            lblPurReqID.Text = Trim(objInserts.Tables(0).Rows(0).Item("RfqID"))
            onProcess_Load(lblPurReqID.Text)
        End If

        If lblPurReqID.Text.Trim <> "" Then
            UserMsgBox(Me, "Generate Succses...!!!")
        Else
            UserMsgBox(Me, "No Record Found...!!!")

            Cancel.Visible = False
            Save.Visible = False
            Confirm.Visible = False
            PRDelete.Visible = False
            Undelete.Visible = False
            Print.Visible = False
            Exit Sub
        End If
    End Sub

    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd As String = "PU_CLSTRX_RFQ_UPD"
        Dim blnIsDetail As Boolean = True
        Dim strSortExpression As String = "LocCode"
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strDateDL As String = Date_Validation(txtDateDL.Text, False)
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
            UserMsgBox(Me, "No Record found...!!!")
            Exit Sub
        End If

        strParamName = "STRSEARCH|RFQID"
        strParamValue = "SET UpdateDate='" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & _
                            "',UpdateID='" & strUserId & _
                            "',RfqDate='" & strDate & _
                            "',RfqDeadLine='" & strDateDL & _
                            "',Status='2',Remark='" & txtRemarks.Text.Trim & "'" & "|" & lblPurReqID.Text

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOppCd, _
                                                strParamName, _
                                                strParamValue)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
            Exit Sub
        End Try

        onProcess_Load(lblPurReqID.Text)

        If lblPurReqID.Text.Trim <> "" And LblStatus.Text = "2" Then
            UserMsgBox(Me, "Confirm Succses...!!!")
        End If

    End Sub

    Sub btnPRDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd As String = "PU_CLSTRX_RFQ_UPD"

        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim blnIsDetail As Boolean = True
        Dim strSortExpression As String = "LocCode"
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strDateDL As String = Date_Validation(txtDateDL.Text, False)
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
            UserMsgBox(Me, "No Record found...!!!")
            Exit Sub
        End If

        strParamName = "STRSEARCH|RFQID"
        strParamValue = "SET UpdateDate='" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & _
                            "',UpdateID='" & strUserId & _
                            "',Status='3'" & "|" & lblPurReqID.Text

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOppCd, _
                                                strParamName, _
                                                strParamValue)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
        End Try

        If lblPurReqID.Text.Trim <> "" Then
            UserMsgBox(Me, "Delete Succses...!!!")
            onProcess_Load(lblPurReqID.Text)
        End If
    End Sub

    Sub btnPRUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd As String = "PU_CLSTRX_RFQ_UPD"

        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim blnIsDetail As Boolean = True
        Dim strSortExpression As String = "LocCode"
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strDateDL As String = Date_Validation(txtDateDL.Text, False)
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
            UserMsgBox(Me, "No Record found...!!!")
            Exit Sub
        End If

        strParamName = "STRSEARCH|RFQID"
        strParamValue = "SET UpdateDate='" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & _
                            "',UpdateID='" & strUserId & _
                            "',Status='1'" & "|" & lblPurReqID.Text

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOppCd, _
                                                strParamName, _
                                                strParamValue)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
        End Try

        If lblPurReqID.Text.Trim <> "" Then
            UserMsgBox(Me, "Undelete Succses...!!!")
            onProcess_Load(lblPurReqID.Text)
        End If
    End Sub

    Sub btnCancel_click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd As String = "PU_CLSTRX_RFQ_UPD"

        Dim strPRID As String = lblPurReqID.Text.Trim
        Dim blnIsDetail As Boolean = True
        Dim strSortExpression As String = "LocCode"
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strDateDL As String = Date_Validation(txtDateDL.Text, False)
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
            UserMsgBox(Me, "No Record found...!!!")
            Exit Sub
        End If

        strParamName = "STRSEARCH|RFQID"
        strParamValue = "SET UpdateDate='" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & _
                            "',UpdateID='" & strUserId & _
                            "',Status='4'" & "|" & lblPurReqID.Text

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOppCd, _
                                                strParamName, _
                                                strParamValue)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADMIN_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=admin/location/admin_location_locdet.aspx")
        End Try

        If lblPurReqID.Text.Trim <> "" Then
            UserMsgBox(Me, "Cancel Succses...!!!")
            onProcess_Load(lblPurReqID.Text)
        End If
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String
        Dim intStatus As Integer = 0
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String = ""
        Dim intErrNo As Integer = 0

        strStatus = Trim(LblStatus.Text)
        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_RFQDet.aspx?strRPHId=" & lblPurReqID.Text & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_Trx_RFQ_List.aspx")
    End Sub

#End Region

#Region "LOCAL & PROCEDURE"

    Sub onProcess_Load(ByVal pv_strRFQID As String)
        onLoad_DisplayPR(pv_strRFQID)
        onLoad_DisplayPRLn(pv_strRFQID)
    End Sub

    Sub onLoad_DisplayPR(ByVal pv_strRFQID As String)
        Dim strOpCode As String = "PU_CLSTRX_RFQ_LIST_GET"
        Dim ssQLKriteria As String

        ssQLKriteria = "And r.RfqID='" & pv_strRFQID & "'"
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
            txtSupCode.Text = Trim(objItemDs.Tables(0).Rows(0).Item("SupplierCode"))
            txtSupName.Text = Trim(objItemDs.Tables(0).Rows(0).Item("Name"))
            txtDate.Text = objGlobal.GetShortDate(strDateFMT, Trim(objItemDs.Tables(0).Rows(0).Item("rfQDate")))
            txtDateDL.Text = objGlobal.GetShortDate(strDateFMT, Trim(objItemDs.Tables(0).Rows(0).Item("rfqDeadline")))
            txtPRID_Plmph.Text = Trim(objItemDs.Tables(0).Rows(0).Item("PRID"))
            txtRemarks.Text = Trim(objItemDs.Tables(0).Rows(0).Item("Remark"))
            txtDeliverTo.Text = Trim(objItemDs.Tables(0).Rows(0).Item("DeliverTo"))
            LblStatus.Text = Trim(objItemDs.Tables(0).Rows(0).Item("Status"))
            LblStatusDesc.Text = Trim(objItemDs.Tables(0).Rows(0).Item("StatusDesc"))            
            txtPlandDevDate.Text = Trim(objItemDs.Tables(0).Rows(0).Item("PlandDelvDate"))
            txtDevDate.Text = Trim(objItemDs.Tables(0).Rows(0).Item("DelvDate"))
            txtTermOfPay.Text = Trim(objItemDs.Tables(0).Rows(0).Item("TermOfPayment"))

            Select Case Trim(objItemDs.Tables(0).Rows(0).Item("Status"))
                Case "1"
                    Confirm.Visible = True
                    PRDelete.Visible = True
                    Undelete.Visible = False
                    Cancel.Visible = False
                    Save.Visible = True
                    Print.Visible = True
                Case "2"
                    Confirm.Visible = False
                    Save.Visible = False
                    PRDelete.Visible = False
                    Undelete.Visible = False
                    Cancel.Visible = True
                    Print.Visible = True
                Case "3"
                    Confirm.Visible = False
                    Save.Visible = False
                    PRDelete.Visible = False
                    Undelete.Visible = True
                    Cancel.Visible = False
                    Print.Visible = False
                Case "4"
                    Confirm.Visible = False
                    Save.Visible = False
                    PRDelete.Visible = False
                    Undelete.Visible = False
                    Cancel.Visible = False
                    Print.Visible = False

            End Select

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
            PRLnTable.Visible = True
            txtPRID_Plmph.ReadOnly = True
            BtnViewPR.Visible = False
            For intCnt = 0 To intPRLnCount - 1
                CType(dgPRLn.Items(intCnt).FindControl("lblNo"), Label).Text = intCnt + 1
                EdtButton = dgPRLn.Items.Item(intCnt).FindControl("Edit")
                DelButton = dgPRLn.Items.Item(intCnt).FindControl("Delete")
                UpdButton = dgPRLn.Items.Item(intCnt).FindControl("Update")
                CanButton = dgPRLn.Items.Item(intCnt).FindControl("Cancel")
                If LblStatus.Text = "1" Then
                    EdtButton.Visible = True
                    DelButton.Visible = True
                    UpdButton.Visible = False
                    CanButton.Visible = False
                    txtRemarks.Visible = True
                    txtDeliverTo.Visible = True
                Else
                    EdtButton.Visible = False
                    DelButton.Visible = False
                    UpdButton.Visible = False
                    CanButton.Visible = False
                    txtRemarks.Visible = False
                    txtDeliverTo.Visible = False                
                End If
            Next intCnt
        Else
            BtnViewPR.Visible = True
            PRLnTable.Visible = False
            Confirm.Visible = False
            txtRemarks.Visible = False            
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


        strParamName = "RFQID"
        strParamValue = lblPurReqID.Text

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

#End Region

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
