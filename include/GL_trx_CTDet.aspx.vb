
Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class gl_trx_CTDet : Inherits Page

    Protected WithEvents txtWorkOrderId As System.Web.UI.WebControls.TextBox
    Protected WithEvents reqWorkOrderId As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSupplier As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblErrSupplier As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrMessage As System.Web.UI.WebControls.Label
    Protected WithEvents lblDateCreated As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSelDate As System.Web.UI.WebControls.Image
    Protected WithEvents lblErrDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusHidden As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdate As System.Web.UI.WebControls.Label
    Protected WithEvents txtExpectedDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSelDate1 As System.Web.UI.WebControls.Image
    Protected WithEvents lblErrDate1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblUpdatedBy As System.Web.UI.WebControls.Label
    Protected WithEvents txtFinishDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSelDate2 As System.Web.UI.WebControls.Image
    Protected WithEvents lblErrDate2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtContact As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRemark As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAmount As System.Web.UI.WebControls.TextBox
    Protected WithEvents SaveBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents RefreshBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ConfirmBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents PrintBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents CancelBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents DeleteBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents UnDeleteBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents BackBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents cnid As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLSetup As New agri.GL.clsSetup()
    Protected objGLTrx As New agri.GL.ClsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objPUTrx As New agri.PU.clsTrx()
    Dim objAPTrx As New agri.AP.clsTrx()    
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objSuppDs As New Object()
    Dim objAccDs As New Object()
    Dim objCNLnDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intAPAR As Integer

    Dim strSelectedCNID As String
    Dim intCNStatus As Integer
    Dim strAcceptDateFormat As String
    Dim intConfig As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intAPAR = Session("SS_APAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditNote), intAPAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
          
            SaveBtn.Visible = False
            RefreshBtn.Visible = False
            ConfirmBtn.Visible = False
            CancelBtn.Visible = False
            DeleteBtn.Visible = False
            UnDeleteBtn.Visible = False
            lblErrSupplier.Visible = False
            lblErrDate.Visible = False
            strSelectedCNID = Trim(IIf(Request.QueryString("cnid") = "", Request.Form("cnid"), Request.QueryString("cnid")))
            cnid.Value = strSelectedCNID
            onload_GetLangCap()
            If Not IsPostBack Then
                If strSelectedCNID <> "" Then
                    onLoad_Display(strSelectedCNID)
                    onLoad_Button()
                Else
                    onLoad_Button()
                    BindSupplier("")
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_TRX_CNDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=ap/setup/AP_trx_CNList.aspx")
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


    Sub onLoad_Button()
        Dim intStatus As Integer

        ddlSupplier.Enabled = False
        txtRemark.Enabled = False
   
        ddlSupplier.Enabled = False
        btnSelDate.Visible = False

        If (lblStatusHidden.Text <> "") Then
            intStatus = CInt(lblStatusHidden.Text)
            Select Case intStatus
                Case objAPTrx.EnumCreditNoteStatus.Active
                    txtRemark.Enabled = True
                    btnSelDate.Visible = True
                    SaveBtn.Visible = True
                    RefreshBtn.Visible = True
                    'ConfirmBtn.Visible = True
                    DeleteBtn.Visible = True
                Case objAPTrx.EnumCreditNoteStatus.Deleted
                    UnDeleteBtn.Visible = True
            End Select
        Else
            ddlSupplier.Enabled = True
          
            txtRemark.Enabled = True
            SaveBtn.Visible = True
            ConfirmBtn.Visible = False
            btnSelDate.Visible = True
        End If
        DeleteBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        UnDeleteBtn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
    End Sub


    Sub onLoad_Display(ByVal pv_strCreditNoteId As String)
        Dim strOpCd_Get As String = "GL_CLSTRX_WORKORDER_DETAILS_GET"
        Dim objCNDs As New Object()
        Dim intErrNo As Integer
        Dim strParamName As String = "WORKORDERID|LOCCODE"
        Dim strParamValue As String = pv_strCreditNoteId & "|" & strLocation
        Dim intCnt As Integer = 0

        cnid.Value = pv_strCreditNoteId

        Try


            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get, _
                                                strParamName, _
                                                strParamValue, _
                                                objCNDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CREDITNOTE_GET_HEADER&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_cnlist.aspx")
        End Try

        lblStatus.Text = objAPTrx.mtdGetDebitNoteStatus(Trim(objCNDs.Tables(0).Rows(0).Item("Status")))
        intCNStatus = CInt(Trim(objCNDs.Tables(0).Rows(0).Item("Status")))
        lblStatusHidden.Text = intCNStatus
        lblDateCreated.Text = objGlobal.GetLongDate(objCNDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objCNDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objCNDs.Tables(0).Rows(0).Item("UserName"))
        txtRemark.Text = Trim(objCNDs.Tables(0).Rows(0).Item("Descr"))
        BindSupplier(Trim(objCNDs.Tables(0).Rows(0).Item("SupplierCode")))
      

    End Sub


    Sub BindSupplier(ByVal pv_strSupplierId As String)
        Dim strOpCode_GetSupp As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedSuppIndex As Integer = 0
        Dim dr As DataRow

        Try
            strParam = "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||"
            intErrNo = objPUSetup.mtdGetSupplier(strOpCode_GetSupp, strParam, objSuppDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CREDITNOTE_GET_SUPP1&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_cnlist.aspx")
        End Try

        For intCnt = 0 To objSuppDs.Tables(0).Rows.Count - 1
            objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = Trim(objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode"))
            objSuppDs.Tables(0).Rows(intCnt).Item("Name") = Trim(objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode")) & " (" & Trim(objSuppDs.Tables(0).Rows(intCnt).Item("Name")) & ")"
            If objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = pv_strSupplierId Then
                intSelectedSuppIndex = intCnt + 1
            End If
        Next intCnt

        dr = objSuppDs.Tables(0).NewRow()
        dr("SupplierCode") = ""
        dr("Name") = "Please select Supplier Code"
        objSuppDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlSupplier.DataSource = objSuppDs.Tables(0)
        ddlSupplier.DataValueField = "SupplierCode"
        ddlSupplier.DataTextField = "Name"
        ddlSupplier.DataBind()
        ddlSupplier.SelectedIndex = intSelectedSuppIndex
    End Sub

   

    Sub Update_CreditNote(ByVal pv_intStatus As Integer, ByRef pr_objNewCNID As Object, ByRef pr_objIsValid As Object)
        'Dim strCNRefNo As String = txtCNRefNo.Text
        'Dim strCNRefDate As String = txtCNRefDate.Text
        'Dim strSupplierCode As String = ddlSupplier.SelectedItem.Value
        'Dim strRemark As String = txtRemark.Text
        'Dim strOpCd_AddCreditNote As String = "AP_CLSTRX_CREDITNOTE_ADD"
        'Dim strOpCd_UpdCreditNote As String = "AP_CLSTRX_CREDITNOTE_UPD"
        'Dim strOpCodes As String = strOpCd_AddCreditNote & "|" & _
        '                           strOpCd_UpdCreditNote
        'Dim intErrNo As Integer
        'Dim strParam As String = ""

        'Dim objChkRef As Object
        'Dim intErrNoRef As Integer
        'Dim strParamRef As String = ""
        'Dim strParamID As String = ""
        'Dim strOpCd_RefNo As String = "AP_CLSTRX_CHK_REF_NO"

        'pr_objIsValid = False
        'If (strSupplierCode = "") Then
        '    lblErrSupplier.Visible = True
        '    Exit Sub
        'ElseIf IsDBNull(strCNRefDate) Or strCNRefDate = "" Then
        '    strCNRefDate = ""
        'Else
        '    strCNRefDate = Date_Validation(strCNRefDate, False)
        '    If strCNRefDate = "" Then
        '        lblErrDate.Visible = True
        '        lblErrDate.Text = lblErrDate.Text & strAcceptDateFormat
        '        pr_objIsValid = False
        '        Exit Sub
        '    End If
        'End If


        'if strSelectedCNID <> "" then
        '    strParamID = "AND CreditNoteID NOT LIKE '" & strSelectedCNID & "'"
        'else
        '    strParamID = ""
        'end if

        'strParamRef = "AP_CREDITNOTE|SupplierDocRefNo|" & _
        '              strParamID & "|" & strCNRefNo & "|" & _
        '              strSupplierCode

        'Try
        '    intErrNoRef = objAPTrx.mtdChkRefNo(strOpCd_RefNo, _
        '                                      strParamRef, _
        '                                      objChkRef)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_CHK_REF_NO&errmesg=" & Exp.ToString() & "&redirect=pu/trx/ap/trx/ap_trx_invrcvlist.aspx")
        'End Try

        'If objChkRef.Tables(0).Rows.Count > 0  AND strCNRefNo <> ""
        '    lblUnqErrRefNo.Visible = True
        '    exit sub
        'end if

        'strParam = strParam & objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.APCreditNote) & "|" & _
        '                        strSelectedCNID & "|" & _
        '                        strCNRefNo & "|" & _
        '                        strCNRefDate & "|" & _
        '                        strSupplierCode & "|" & _
        '                        strRemark & "|" & _
        '                        pv_intStatus & "|" & ddlInvoiceRcvRefNo.SelectedItem.Value.Trim
        'Try
        '    intErrNo = objAPTrx.mtdUpdCreditNote(strOpCodes, _
        '                                        strCompany, _
        '                                        strLocation, _
        '                                        strUserId, _
        '                                        strAccMonth, _
        '                                        strAccYear, _
        '                                        strParam, _
        '                                        pr_objNewCNID)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CREDITNOTE_UPD_DATA&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_cnlist.aspx")
        'End Try

        'pr_objNewCNID = IIf(strSelectedCNID = "", pr_objNewCNID, strSelectedCNID)
        'pr_objIsValid = True
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INVOICERCV_GET_CONFIG&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_polist.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptDateFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function


   

    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim objCNID As String
        'Dim blnIsValidDate As Boolean

        'Update_CreditNote(objAPTrx.EnumCreditNoteStatus.Active, objCNID, blnIsValidDate)
        'If blnIsValidDate = True Then
        '    onLoad_Display(objCNID)
        '    onLoad_DisplayLine(objCNID)
        '    onLoad_Button()
        'Else
        '    onLoad_Button()
        'End If
    End Sub

    Sub RefreshBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'onLoad_Display(strSelectedCNID)
        'onLoad_Button()
    End Sub

    Sub ConfirmBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim objCNID As New Object()
        'Dim blnIsValidDate As Boolean

        'Update_CreditNote(objAPTrx.EnumCreditNoteStatus.Confirmed, objCNID, blnIsValidDate)
        'If blnIsValidDate = True Then
        '    onLoad_Display(objCNID)
        '    onLoad_Button()
        'End If
    End Sub

    Sub CancelBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim objCNID As New Object()
        'Dim blnIsValidDate As Boolean

        'Update_CreditNote(objAPTrx.EnumCreditNoteStatus.Cancelled, objCNID, blnIsValidDate)
        'If blnIsValidDate = True Then
        '    onLoad_Display(objCNID)
        '    onLoad_Button()
        'End If
    End Sub

    Sub DeleteBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim objCNID As New Object()
        'Dim blnIsValidDate As Boolean

        'Update_CreditNote(objAPTrx.EnumCreditNoteStatus.Deleted, objCNID, blnIsValidDate)
        'If blnIsValidDate = True Then
        '    onLoad_Display(objCNID)
        '    onLoad_DisplayLine(objCNID)
        '    onLoad_Button()
        'End If
    End Sub

    Sub UnDeleteBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim objCNID As New Object()
        'Dim blnIsValidDate As Boolean

        'Update_CreditNote(objAPTrx.EnumCreditNoteStatus.Active, objCNID, blnIsValidDate)
        'If blnIsValidDate = True Then
        '    onLoad_Display(objCNID)
        '    onLoad_DisplayLine(objCNID)
        '    onLoad_Button()
        'End If
    End Sub


    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("gl_trx_CTList.aspx")
    End Sub

End Class

