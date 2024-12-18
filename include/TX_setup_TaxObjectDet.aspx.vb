
Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.Admin
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap

Public Class TX_Setup_TaxObjectDet : Inherits Page

    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents txtAdditionalNote As TextBox
    Protected WithEvents txtTaxObject As TextBox
    Protected WithEvents txtWRate As TextBox
    Protected WithEvents txtWORate As TextBox
    Protected WithEvents txtAddNote As TextBox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents trxid As HtmlInputHidden
    Protected WithEvents trxlnid As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrTaxObjectGrp As Label
    Protected WithEvents lblErrLen As Label
    Protected WithEvents chkExpired As CheckBox
    Protected WithEvents txtTransDate As TextBox
    Protected WithEvents lblErrTransDate As Label
    Protected WithEvents lblerrTaxObject As Label
    Protected WithEvents lblErrRate As Label
    Protected WithEvents lblErrWRate As Label
    Protected WithEvents lblErrWORate As Label
    Protected WithEvents dgLineDet As DataGrid

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGLTrx As New agri.GL.ClsTrx()
    Dim objAdmin As New agri.Admin.clsUom()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objCTSetup As New agri.CT.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim objTaxDs As New Object()
    Dim objActDs As New Object()
    Dim objUOMDs As New Object()
    Dim objConfigDs As New Object()
    Dim objLangCapDs As New Object()

    Dim intConfigsetting As Integer
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intCTAR As Integer
    Dim strTrxID As String = ""
    Dim intStatus As Integer
    Dim intMaxLen As Integer = 0
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strAccountTag As String
    Dim strLocType As String
    Dim strAcceptDateFormat As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intCTAR = Session("SS_CTAR")
        strLocType = Session("SS_LOCTYPE")
        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster), intCTAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            strTrxID = Trim(IIf(Request.QueryString("trxid") <> "", Request.QueryString("trxid"), Request.Form("trxid")))

            intStatus = CInt(lblHiddenSts.Text)
            lblerrTaxObject.Visible = False
            lblAccCodeErr.Visible = False
            lblErrRate.Visible = False
            lblErrWRate.Visible = False
            lblErrWORate.Visible = False
            lblErrTransDate.Visible = False

            If Not IsPostBack Then
                txtTransDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                BindAccCodeDropList()
                If strTrxID <> "" Then
                    trxid.Value = strTrxID
                    onLoad_Display(trxid.Value)
                    onLoad_DisplayLn(trxid.Value)
                Else
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display(ByVal pv_strTrxID As String)
        Dim strOpCd As String = "TX_CLSSETUP_TAXOBJECTRATE_LIST_GET"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "STRSEARCH"
        strParamValue = " AND TOB.TrxID = '" & Trim(pv_strTrxID) & "' "

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtDescription.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("Description"))
        ddlAccCode.SelectedIndex = CInt(objTaxDs.Tables(0).Rows(0).Item("AccCode"))
        txtTransDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), objTaxDs.Tables(0).Rows(0).Item("ExpiredDate"))
        If Trim(objTaxDs.Tables(0).Rows(0).Item("ExpiredStatus")) = "1" Then
            chkExpired.Checked = True
        Else
            chkExpired.Checked = False
        End If

        intStatus = CInt(Trim(objTaxDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objCTSetup.mtdGetStockItemStatus(Trim(objTaxDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objTaxDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objTaxDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("UserName"))

        onLoad_BindButton()
    End Sub

    Sub onLoad_DisplayLn(ByVal pv_strTrxID As String)
        Dim strOpCd As String = "TX_CLSSETUP_TAXOBJECTRATELN_LIST_GET"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer = 0
        Dim lbl As Label
        Dim DelButton As LinkButton
        Dim EdtButton As LinkButton
        Dim CanButton As LinkButton

        strParamName = "STRSEARCH"
        strParamValue = " AND TOB.TrxID = '" & Trim(pv_strTrxID) & "' "

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objTaxDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objTaxDs.Tables(0).Rows.Count - 1
                EdtButton = dgLineDet.Items.Item(intCnt).FindControl("lbEdit")
                DelButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                CanButton = dgLineDet.Items.Item(intCnt).FindControl("lbCancel")

                Select Case Trim(lblStatus.Text)
                    Case objCTSetup.EnumStockItemStatus.Active
                        If trxlnid.Value = "" Then
                            EdtButton.Visible = False
                            DelButton.Visible = False
                            CanButton.Visible = True
                        Else
                            EdtButton.Visible = True
                            DelButton.Visible = True
                            CanButton.Visible = False
                        End If
                    Case objCTSetup.EnumStockItemStatus.Deleted
                        EdtButton.Visible = False
                        DelButton.Visible = False
                        CanButton.Visible = False
                End Select
            Next intCnt
        End If
    End Sub

    Sub onLoad_BindButton()
        txtDescription.Enabled = False
        ddlAccCode.Enabled = False
        txtTransDate.Enabled = False
        chkExpired.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objCTSetup.EnumStockItemStatus.Active
                txtDescription.Enabled = True
                ddlAccCode.Enabled = True
                txtTransDate.Enabled = True
                chkExpired.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objCTSetup.EnumStockItemStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtDescription.Enabled = True
                ddlAccCode.Enabled = True
                SaveBtn.Visible = True
                chkExpired.Enabled = True
        End Select

    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCd_Upd As String = "TX_CLSSETUP_TAXOBJECTRATE_UPDATE"
        Dim strOpCd_Add As String = "GL_CLSSETUP_SUBACTIVITY_LIST_ADD"
        Dim strOpCd_ID_GET As String = "GL_CLSSETUP_SUBACT_GET"
        Dim dsLastRec As New DataSet()

        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParamName As String
        Dim strParamValue As String
        Dim strDate As String = Date_Validation(txtTransDate.Text, False)

        If ddlAccCode.SelectedItem.Value = "0" Then
            lblErrTaxObjectGrp.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            trxid.Value = strTrxID

            If trxid.Value = "" Then
                Exit Sub
            End If

            strParamName = "TRXID|ACCCODE|DESCRIPTION|EXPIREDATE|EXPIREDSTATUS|STATUS|UPDATEID"
            strParamValue = strTrxID & "|" & _
                            ddlAccCode.SelectedItem.Value & "|" & _
                            Trim(txtDescription.Text) & "|" & _
                            strDate & "|" & _
                            IIf(chkExpired.Checked = True, "1", "0") & "|" & _
                            objCTSetup.EnumStockItemStatus.Active & "|" & _
                            Trim(strUserId)
           

        ElseIf strCmdArgs = "Del" Then
            strParamName = "TRXID|ACCCODE|DESCRIPTION|EXPIREDATE|EXPIREDSTATUS|STATUS|UPDATEID"
            strParamValue = strTrxID & "|" & _
                            ddlAccCode.SelectedItem.Value & "|" & _
                            Trim(txtDescription.Text) & "|" & _
                            strDate & "|" & _
                            IIf(chkExpired.Checked = True, "1", "0") & "|" & _
                            objCTSetup.EnumStockItemStatus.Deleted & "|" & _
                            Trim(strUserId)
        ElseIf strCmdArgs = "UnDel" Then
            strParamName = "TRXID|ACCCODE|DESCRIPTION|EXPIREDATE|EXPIREDSTATUS|STATUS|UPDATEID"
            strParamValue = strTrxID & "|" & _
                            ddlAccCode.SelectedItem.Value & "|" & _
                            Trim(txtDescription.Text) & "|" & _
                            strDate & "|" & _
                            IIf(chkExpired.Checked = True, "1", "0") & "|" & _
                            objCTSetup.EnumStockItemStatus.Active & "|" & _
                            Trim(strUserId)
        End If

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If trxid.Value <> "" Then
            onLoad_Display(trxid.Value)
            onLoad_DisplayLn(trxid.Value)
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("TX_Setup_TaxObjectList.aspx")
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

    Sub AddBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "TX_CLSSETUP_TAXOBJECTRATE_UPDATE"
        Dim strOpCd_UpdLn As String = "TX_CLSSETUP_TAXOBJECTRATELN_UPDATE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim strTrxID As String = IIf(trxid.Value = "", "", trxid.Value)
        Dim objEmp As New Object()
        Dim strDate As String = Date_Validation(txtTransDate.Text, False)

        If ddlAccCode.SelectedItem.Value = "" Then
            lblAccCodeErr.Visible = True
            Exit Sub
        End If

        If txtTaxObject.Text = "" Then
            lblerrTaxObject.Visible = True
            Exit Sub
        End If

        If txtWRate.Text = 0 And txtWORate.Text = 0 Then
            lblErrRate.Visible = True
            Exit Sub
        End If

        If strTrxID = "" Then
            strParamName = "TRXID|ACCCODE|DESCRIPTION|EXPIREDATE|EXPIREDSTATUS|STATUS|UPDATEID"
            strParamValue = strTrxID & "|" & _
                            ddlAccCode.SelectedItem.Value & "|" & _
                            Trim(txtDescription.Text) & "|" & _
                            strDate & "|" & _
                            IIf(chkExpired.Checked = True, "1", "0") & "|" & _
                            objCTSetup.EnumStockItemStatus.Active & "|" & _
                            Trim(strUserId)
            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If trxlnid.Value = "" Then
                strParamName = "TRXID|ACCCODE|UPDATEID|TAXOBJECT|WRATE|WORATE|ADDNOTE"
                strParamValue = strTrxID & "|" & _
                                ddlAccCode.SelectedItem.Value & "|" & _
                                Trim(strUserId) & "|" & _
                                Trim(txtTaxObject.Text) & "|" & _
                                txtWRate.Text & "|" & _
                                txtWORate.Text & "|" & _
                                Trim(txtAddNote.Text)
                Try
                    intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
            Else
                strParamName = "TRXID|ACCCODE|UPDATEID|TRXLNID|TAXOBJECT|WRATE|WORATE|ADDNOTE"
                strParamValue = strTrxID & "|" & _
                                ddlAccCode.SelectedItem.Value & "|" & _
                                Trim(strUserId) & "|" & _
                                Trim(trxlnid.Value) & "|" & _
                                Trim(txtTaxObject.Text) & "|" & _
                                txtWRate.Text & "|" & _
                                txtWORate.Text & "|" & _
                                Trim(txtAddNote.Text)
                Try
                    intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
            End If
        End If
        
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim strOpCd_Upd As String = "TX_CLSSETUP_TAXOBJECTRATELN_DELETE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim strTrxID As String = IIf(trxid.Value = "", "", trxid.Value)

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblTrxID")
        trxlnid.Value = lbl.Text.Trim

        strParamName = "TRXID|TRXLNID|UPDATEID"
        strParamValue = strTrxID & "|" & _
                       Trim(trxlnid.Value) & "|" & _
                       Trim(strUserId)
        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        onLoad_Display(trxid.Value)
        onLoad_DisplayLn(trxid.Value)
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        onLoad_Display(trxid.Value)
        onLoad_DisplayLn(trxid.Value)
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblTrxID")
        trxlnid.Value = lbl.Text.Trim
        lbl = E.Item.FindControl("lblTaxObject")
        txtTaxObject.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblWRate")
        txtWRate.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblWORate")
        txtWORate.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblAddNote")
        txtAddNote.Text = lbl.Text.Trim

    End Sub

    Sub change_expired(ByVal Sender As Object, ByVal E As EventArgs)
        If chkExpired.Checked = True Then
            chkExpired.Text = "  Expire date"
            txtTransDate.Enabled = True
        Else
            chkExpired.Text = "  No expire date"
            txtTransDate.Enabled = False
        End If
    End Sub

    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = lblSelect.Text & strAccountTag
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccCode.DataSource = dsForDropDown.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "_Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        strAccountTag = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblAccCodeTag.Text = strAccountTag & " :* "
        lblAccCodeErr.Text = "<BR>" & lblPleaseSelect.Text & strAccountTag
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNAL_DETAIL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=CB/CB_trx_CashBankDet.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function
End Class
