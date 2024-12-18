
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

Public Class TX_Setup_TaxObjectRateDet : Inherits Page

    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents txtAdditionalNote As TextBox
    Protected WithEvents txtTaxObject As TextBox
    Protected WithEvents txtRowNo As TextBox
    Protected WithEvents txtWRate As TextBox
    Protected WithEvents txtWORate As TextBox
    Protected WithEvents txtAddNote As TextBox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents taxid As HtmlInputHidden
    Protected WithEvents taxlnid As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents AddBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrTaxObjectGrp As Label
    Protected WithEvents lblErrLen As Label
    Protected WithEvents chkExpired As CheckBox
    Protected WithEvents txtTransDate As TextBox
    Protected WithEvents lblErrTransDate As Label
    Protected WithEvents lblerrTaxObject As Label
    Protected WithEvents lblErrRowNo As Label
    Protected WithEvents lblErrRate As Label
    Protected WithEvents lblErrWRate As Label
    Protected WithEvents lblErrWORate As Label
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents txtSPTMasa As TextBox
    Protected WithEvents txtDaftarPtg As TextBox
    Protected WithEvents txtBuktiPtg As TextBox
    Protected WithEvents txtTaxInit As TextBox
    Protected WithEvents chkAccumulative As CheckBox
    Protected WithEvents txtGrpNo As TextBox
    Protected WithEvents lblErrGrpNo As Label

    Protected WithEvents txt_min1 As TextBox
    Protected WithEvents txt_max1 As TextBox
    Protected WithEvents txt_tax1 As TextBox
    Protected WithEvents txt_min2 As TextBox
    Protected WithEvents txt_max2 As TextBox
    Protected WithEvents txt_tax2 As TextBox
    Protected WithEvents txt_min3 As TextBox
    Protected WithEvents txt_max3 As TextBox
    Protected WithEvents txt_tax3 As TextBox
    Protected WithEvents txt_min4 As TextBox
    Protected WithEvents txt_max4 As TextBox
    Protected WithEvents txt_tax4 As TextBox

    Protected WithEvents tblPPH21 As HtmlTable
    Protected WithEvents txtPotJbt As TextBox
    Protected WithEvents txtPotJbtPsn As TextBox
    Protected WithEvents ddlTransit As DropDownList
    Protected WithEvents ddlAlokasi As DropDownList
    Protected WithEvents ddlAlokasiCR As DropDownList

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGLTrx As New agri.GL.ClsTrx()
    Dim objAdmin As New agri.Admin.clsUom()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
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
    Dim strTaxID As String = ""
    Dim intStatus As Integer
    Dim intMaxLen As Integer = 0
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strAccountTag As String
    Dim strLocType As String
    Dim strAcceptDateFormat As String
    Dim intLocLevel As Integer


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
        intLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster), intCTAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            strTaxID = Trim(IIf(Request.QueryString("taxid") <> "", Request.QueryString("taxid"), Request.Form("taxid")))

            intStatus = CInt(lblHiddenSts.Text)
            lblerrTaxObject.Visible = False
            lblAccCodeErr.Visible = False
            lblErrRate.Visible = False
            lblErrRowNo.Visible = False
            lblErrWRate.Visible = False
            lblErrWORate.Visible = False
            lblErrTransDate.Visible = False
            lblErrGrpNo.Visible = False

            If Not IsPostBack Then
                txtTransDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                BindAccCodeDropList()
                BindAccCodePPH21("")
                If strTaxID <> "" Then
                    taxid.Value = strTaxID
                    onLoad_Display(taxid.Value)
                    onLoad_DisplayLn(taxid.Value)
                Else
                    onLoad_BindButton()
                End If
            End If

            If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then                
				SaveBtn.Visible = True
				DelBtn.Visible = True
				AddBtn.Visible = True
            Else
                SaveBtn.Visible = True
                DelBtn.Visible = True
                AddBtn.Visible = True
            End If
           
        End If
    End Sub

    Sub onLoad_Display(ByVal pv_strTaxID As String)
        Dim strOpCd As String = "TX_CLSSETUP_TAXOBJECTRATE_LIST_GET"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer

        strParamName = "STRSEARCH"
        strParamValue = " AND TOB.TaxID = '" & Trim(pv_strTaxID) & "' "

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtDescription.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("Description"))
        'ddlAccCode.SelectedItem.Value = objTaxDs.Tables(0).Rows(0).Item("AccCode")
        BindAccCodeDropList(objTaxDs.Tables(0).Rows(0).Item("AccCode"))
        txtTransDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), objTaxDs.Tables(0).Rows(0).Item("ExpiredDate"))
        If Trim(objTaxDs.Tables(0).Rows(0).Item("ExpiredStatus")) = "1" Then
            chkExpired.Checked = True
            txtTransDate.Enabled = False
        Else
            chkExpired.Checked = False
            txtTransDate.Enabled = False
        End If

        intStatus = CInt(Trim(objTaxDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objCTSetup.mtdGetStockItemStatus(Trim(objTaxDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objTaxDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objTaxDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("UserName"))
        txtSPTMasa.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("SPTMasa"))
        txtDaftarPtg.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("DaftarPtg"))
        txtBuktiPtg.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("BuktiPtg"))
        txtTaxInit.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("TaxInit"))
        txtPotJbt.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("PotJbt"))
        txtPotJbtPsn.Text = Trim(objTaxDs.Tables(0).Rows(0).Item("PotJbtPsn"))

        ddlTransit.SelectedValue = Trim(objTaxDs.Tables(0).Rows(0).Item("COATransit"))
        ddlAlokasi.SelectedValue = Trim(objTaxDs.Tables(0).Rows(0).Item("COAAlokasi"))
        ddlAlokasiCR.SelectedValue = Trim(objTaxDs.Tables(0).Rows(0).Item("COAAlokasiCR"))

        If Trim(objTaxDs.Tables(0).Rows(0).Item("ExpiredStatus")) = "1" Then
            chkExpired.Checked = True
            chkExpired.Text = "  Expire date"
            txtTransDate.Enabled = True
        Else
            chkExpired.Checked = False
            chkExpired.Text = "  No expire date"
            txtTransDate.Enabled = False
        End If

        If Trim(objTaxDs.Tables(0).Rows(0).Item("Accumulative")) = "1" Then
            chkAccumulative.Checked = True
            chkAccumulative.Text = "  Accumulative"
        Else
            chkAccumulative.Checked = False
            chkAccumulative.Text = "  No Accumulative"
        End If

        If txtTaxInit.Text <> "21" Then
            tblPPH21.Visible = False
        Else
            tblPPH21.Visible = True

            strOpCd = "TX_CLSSETUP_TAXOBJECTRATELN21_GET"

            strParamName = "TAXID"
            strParamValue = Trim(pv_strTaxID)

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
                    If objTaxDs.Tables(0).Rows(intCnt).Item("id") = 1 Then
                        txt_min1.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("income_min"))
                        txt_max1.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("income_max"))
                        txt_tax1.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("tax_rate"))
                    End If

                    If objTaxDs.Tables(0).Rows(intCnt).Item("id") = 2 Then
                        txt_min2.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("income_min"))
                        txt_max2.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("income_max"))
                        txt_tax2.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("tax_rate"))
                    End If

                    If objTaxDs.Tables(0).Rows(intCnt).Item("id") = 3 Then
                        txt_min3.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("income_min"))
                        txt_max3.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("income_max"))
                        txt_tax3.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("tax_rate"))
                    End If

                    If objTaxDs.Tables(0).Rows(intCnt).Item("id") = 4 Then
                        txt_min4.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("income_min"))
                        txt_max4.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("income_max"))
                        txt_tax4.Text = Trim(objTaxDs.Tables(0).Rows(intCnt).Item("tax_rate"))
                    End If
                Next
            End If
        End If

        onLoad_BindButton()
    End Sub

    Sub onLoad_DisplayLn(ByVal pv_strTaxID As String)
        Dim strOpCd As String = "TX_CLSSETUP_TAXOBJECTRATELN_LIST_GET"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer = 0
        Dim lbl As Label
        Dim DelButton As LinkButton
        Dim EdtButton As LinkButton
        Dim CanButton As LinkButton

        strParamName = "STRSEARCH|INTSPL"
        strParamValue = IIf(Session("SS_COACENTRALIZED") = "1", "TaxID = '" & Trim(pv_strTaxID) & "'  ORDER By RowNo ASC", "TaxID = '" & Trim(pv_strTaxID) & "' AND AccCode IN (SELECT AccCode FROM GL_ACCOUNT WHERE LocCode='" & strLocation & "') ORDER By RowNo ASC") & "|" & "0"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLineDet.DataSource = objTaxDs.Tables(0)
        dgLineDet.DataBind()

        If objTaxDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objTaxDs.Tables(0).Rows.Count - 1
                EdtButton = dgLineDet.Items.Item(intCnt).FindControl("lbEdit")
                DelButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                CanButton = dgLineDet.Items.Item(intCnt).FindControl("lbCancel")
                DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

                Select Case intStatus
                    Case objCTSetup.EnumStockItemStatus.Active
                        If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
							If taxlnid.Value = "" Then
								EdtButton.Visible = True
								DelButton.Visible = True
								CanButton.Visible = False
							Else
								EdtButton.Visible = False
								DelButton.Visible = False
								CanButton.Visible = True
							End If
                            ' If strCompany <> "KAS" Then
                                ' If taxlnid.Value = "" Then
                                    ' EdtButton.Visible = False
                                    ' DelButton.Visible = False
                                    ' CanButton.Visible = False
                                ' Else
                                    ' EdtButton.Visible = False
                                    ' DelButton.Visible = False
                                    ' CanButton.Visible = False
                                ' End If
                            ' Else
                                ' If taxlnid.Value = "" Then
                                    ' EdtButton.Visible = True
                                    ' DelButton.Visible = True
                                    ' CanButton.Visible = False
                                ' Else
                                    ' EdtButton.Visible = False
                                    ' DelButton.Visible = False
                                    ' CanButton.Visible = True
                                ' End If
                            ' End If
                        Else
                            If taxlnid.Value = "" Then
                                EdtButton.Visible = True
                                DelButton.Visible = True
                                CanButton.Visible = False
                            Else
                                EdtButton.Visible = False
                                DelButton.Visible = False
                                CanButton.Visible = True
                            End If
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
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        txtTaxObject.Text = ""
        txtWRate.Text = "0"
        txtWORate.Text = "0"
        txtAddNote.Text = ""

        Select Case intStatus
            Case objCTSetup.EnumStockItemStatus.Active
                txtDescription.Enabled = True
                ddlAccCode.Enabled = True
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
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strDate As String = Date_Validation(txtTransDate.Text, False)

        If ddlAccCode.SelectedItem.Value = "0" Then
            lblErrTaxObjectGrp.Visible = True
            Exit Sub
        End If

		
        If strCmdArgs = "Save" Then
            taxid.Value = strTaxID

            If taxid.Value = "" Then
                Exit Sub
            End If

            strParamName = "TAXID|ACCCODE|DESCRIPTION|EXPIREDDATE|EXPIREDSTATUS|STATUS|UPDATEID|SPTMASA|DAFTARPTG|BUKTIPTG|TAXINIT|ACCUMULATIVE|POTJBT|POTJBTPSN|COATRANSIT|COAALOKASI|COAALOKASICR"
            strParamValue = strTaxID & "|" & _
                            ddlAccCode.SelectedItem.Value & "|" & _
                            Trim(txtDescription.Text) & "|" & _
                            strDate & "|" & _
                            IIf(chkExpired.Checked = True, "1", "0") & "|" & _
                            objCTSetup.EnumStockItemStatus.Active & "|" & _
                            Trim(strUserId) & "|" & _
                            Trim(txtSPTMasa.Text) & "|" & _
                            Trim(txtDaftarPtg.Text) & "|" & _
                            Trim(txtBuktiPtg.Text) & "|" & _
                            Trim(txtTaxInit.Text) & "|" & _
                            IIf(chkAccumulative.Checked = True, "1", "0") & "|" & _
                            IIf(txtPotJbt.Text = "", "0", txtPotJbt.Text) & "|" & _
                            IIf(txtPotJbtPsn.Text = "", "0", txtPotJbtPsn.Text) & "|" & _
                            IIf(ddlTransit.SelectedItem.Value = "", "", ddlTransit.SelectedItem.Value) & "|" & _
                            IIf(ddlAlokasi.SelectedItem.Value = "", "", ddlAlokasi.SelectedItem.Value) & "|" & _
                            IIf(ddlAlokasiCR.SelectedItem.Value = "", "", ddlAlokasiCR.SelectedItem.Value)

        ElseIf strCmdArgs = "Del" Then
            strParamName = "TAXID|ACCCODE|DESCRIPTION|EXPIREDDATE|EXPIREDSTATUS|STATUS|UPDATEID|SPTMASA|DAFTARPTG|BUKTIPTG|TAXINIT|ACCUMULATIVE|POTJBT|POTJBTPSN|COATRANSIT|COAALOKASI|COAALOKASICR"
            strParamValue = strTaxID & "|" & _
                            ddlAccCode.SelectedItem.Value & "|" & _
                            Trim(txtDescription.Text) & "|" & _
                            strDate & "|" & _
                            IIf(chkExpired.Checked = True, "1", "0") & "|" & _
                            objCTSetup.EnumStockItemStatus.Deleted & "|" & _
                            Trim(strUserId) & "|" & _
                            Trim(txtSPTMasa.Text) & "|" & _
                            Trim(txtDaftarPtg.Text) & "|" & _
                            Trim(txtBuktiPtg.Text) & "|" & _
                            Trim(txtTaxInit.Text) & "|" & _
                            IIf(chkAccumulative.Checked = True, "1", "0") & "|" & _
                            IIf(txtPotJbt.Text = "", "0", txtPotJbt.Text) & "|" & _
                            IIf(txtPotJbtPsn.Text = "", "0", txtPotJbtPsn.Text) & "|" & _
                            IIf(ddlTransit.SelectedItem.Value = "", "", ddlTransit.SelectedItem.Value) & "|" & _
                            IIf(ddlAlokasi.SelectedItem.Value = "", "", ddlAlokasi.SelectedItem.Value) & "|" & _
                            IIf(ddlAlokasiCR.SelectedItem.Value = "", "", ddlAlokasiCR.SelectedItem.Value)

        ElseIf strCmdArgs = "UnDel" Then
            strParamName = "TAXID|ACCCODE|DESCRIPTION|EXPIREDDATE|EXPIREDSTATUS|STATUS|UPDATEID|SPTMASA|DAFTARPTG|BUKTIPTG|TAXINIT|ACCUMULATIVE|POTJBT|POTJBTPSN|COATRANSIT|COAALOKASI|COAALOKASICR"
            strParamValue = strTaxID & "|" & _
                            ddlAccCode.SelectedItem.Value & "|" & _
                            Trim(txtDescription.Text) & "|" & _
                            strDate & "|" & _
                            IIf(chkExpired.Checked = True, "1", "0") & "|" & _
                            objCTSetup.EnumStockItemStatus.Active & "|" & _
                            Trim(strUserId) & "|" & _
                            Trim(txtSPTMasa.Text) & "|" & _
                            Trim(txtDaftarPtg.Text) & "|" & _
                            Trim(txtBuktiPtg.Text) & "|" & _
                            Trim(txtTaxInit.Text) & "|" & _
                            IIf(chkAccumulative.Checked = True, "1", "0") & "|" & _
                            IIf(txtPotJbt.Text = "", "0", txtPotJbt.Text) & "|" & _
                            IIf(txtPotJbtPsn.Text = "", "0", txtPotJbtPsn.Text) & "|" & _
                            IIf(ddlTransit.SelectedItem.Value = "", "", ddlTransit.SelectedItem.Value) & "|" & _
                            IIf(ddlAlokasi.SelectedItem.Value = "", "", ddlAlokasi.SelectedItem.Value) & "|" & _
                            IIf(ddlAlokasiCR.SelectedItem.Value = "", "", ddlAlokasiCR.SelectedItem.Value)
        End If

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If taxid.Value <> "" Then
            If strCmdArgs = "Save" And txtTaxInit.Text = "21" Then
                If txt_min1.Text.Trim = "" Then
                    txt_min1.Text = "0"
                End If
                If txt_max1.Text.Trim = "" Then
                    txt_max1.Text = "0"
                End If
                If txt_tax1.Text.Trim = "" Then
                    txt_tax1.Text = "0"
                End If

                If txt_min2.Text.Trim = "" Then
                    txt_min2.Text = "0"
                End If
                If txt_max2.Text.Trim = "" Then
                    txt_max2.Text = "0"
                End If
                If txt_tax2.Text.Trim = "" Then
                    txt_tax2.Text = "0"
                End If

                If txt_min3.Text.Trim = "" Then
                    txt_min3.Text = "0"
                End If
                If txt_max3.Text.Trim = "" Then
                    txt_max3.Text = "0"
                End If
                If txt_tax3.Text.Trim = "" Then
                    txt_tax3.Text = "0"
                End If

                If txt_min4.Text.Trim = "" Then
                    txt_min4.Text = "0"
                End If
                If txt_max4.Text.Trim = "" Then
                    txt_max4.Text = "0"
                End If
                If txt_tax4.Text.Trim = "" Then
                    txt_tax4.Text = "0"
                End If

                Dim strOpCdLn_Upd As String = "TX_CLSSETUP_TAXOBJECTRATELN21_UPDATE"

                'Level 1
                strParamName = "ID|IMIN|IMAX|TAX|TAXID"
                strParamValue = "1|" & _
                             txt_min1.Text & "|" & _
                             txt_max1.Text & "|" & _
                             txt_tax1.Text & "|" & _
                             Trim(taxid.Value)

                Try
                    intErrNo = objGLTrx.mtdInsertDataCommon(strOpCdLn_Upd, strParamName, strParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21LN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                'Level 2
                strParamName = "ID|IMIN|IMAX|TAX|TAXID"
                strParamValue = "2|" & _
                             txt_min2.Text & "|" & _
                             txt_max2.Text & "|" & _
                             txt_tax2.Text & "|" & _
                             Trim(taxid.Value)

                Try
                    intErrNo = objGLTrx.mtdInsertDataCommon(strOpCdLn_Upd, strParamName, strParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21LN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                'Level 3
                strParamName = "ID|IMIN|IMAX|TAX|TAXID"
                strParamValue = "3|" & _
                             txt_min3.Text & "|" & _
                             txt_max3.Text & "|" & _
                             txt_tax3.Text & "|" & _
                             Trim(taxid.Value)

                Try
                    intErrNo = objGLTrx.mtdInsertDataCommon(strOpCdLn_Upd, strParamName, strParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21LN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                'Level 4
                strParamName = "ID|IMIN|IMAX|TAX|TAXID"
                strParamValue = "4|" & _
                             txt_min4.Text & "|" & _
                             txt_max4.Text & "|" & _
                             txt_tax4.Text & "|" & _
                             Trim(taxid.Value)

                Try
                    intErrNo = objGLTrx.mtdInsertDataCommon(strOpCdLn_Upd, strParamName, strParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21LN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
            End If

            onLoad_Display(taxid.Value)
            onLoad_DisplayLn(taxid.Value)
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("TX_Setup_TaxObjectRateList.aspx")
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
        Dim strTaxID As String = IIf(taxid.Value = "", "", taxid.Value)
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

        If txtRowNo.Text = "" Or txtRowNo.Text = 0 Or txtRowNo.Text = "0" Then
            lblErrRowNo.Visible = True
            Exit Sub
        End If

        If txtWRate.Text = 0 And txtWORate.Text = 0 Then
            lblErrRate.Visible = True
            Exit Sub
        End If

        If strTaxID = "" Then
             strParamName = "TAXID|ACCCODE|DESCRIPTION|EXPIREDDATE|EXPIREDSTATUS|STATUS|UPDATEID|SPTMASA|DAFTARPTG|BUKTIPTG|TAXINIT|ACCUMULATIVE|POTJBT|POTJBTPSN|COATRANSIT|COAALOKASI|COAALOKASICR"
            strParamValue = strTaxID & "|" & _
                            ddlAccCode.SelectedItem.Value & "|" & _
                            Trim(txtDescription.Text) & "|" & _
                            strDate & "|" & _
                            IIf(chkExpired.Checked = True, "1", "0") & "|" & _
                            objCTSetup.EnumStockItemStatus.Active & "|" & _
                            Trim(strUserId) & "|" & _
                            Trim(txtSPTMasa.Text) & "|" & _
                            Trim(txtDaftarPtg.Text) & "|" & _
                            Trim(txtBuktiPtg.Text) & "|" & _
                            Trim(txtTaxInit.Text) & "|" & _
                            IIf(chkAccumulative.Checked = True, "1", "0") & "|" & _
                            IIf(txtPotJbt.Text = "", "0", txtPotJbt.Text) & "|" & _
                            IIf(txtPotJbtPsn.Text = "", "0", txtPotJbtPsn.Text) & "|" & _
                            IIf(ddlTransit.SelectedItem.Value = "", "", ddlTransit.SelectedItem.Value) & "|" & _
                            IIf(ddlAlokasi.SelectedItem.Value = "", "", ddlAlokasi.SelectedItem.Value) & "|" & _
                            IIf(ddlAlokasiCR.SelectedItem.Value = "", "", ddlAlokasiCR.SelectedItem.Value)

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Upd, strParamName, strParamValue, objTaxDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objTaxDs.Tables(0).Rows.Count > 0 Then
                strTaxID = Trim(objTaxDs.Tables(0).Rows(0).Item("TaxID"))
            End If
        End If

        If strTaxID = "" Then
            Exit Sub
        Else
            strParamName = "TAXID|TAXLNID|ACCCODE|UPDATEID|TAXOBJECT|ROWNO|WRATE|WORATE|ADDNOTE|GRPNO"
            strParamValue = strTaxID & "|" & _
                            Trim(taxlnid.Value) & "|" & _
                            ddlAccCode.SelectedItem.Value & "|" & _
                            Trim(strUserId) & "|" & _
                            Trim(txtTaxObject.Text) & "|" & _
                            txtRowNo.Text & "|" & _
                            txtWRate.Text & "|" & _
                            txtWORate.Text & "|" & _
                            Trim(txtAddNote.Text) & "|" & _
                            iif(txtGrpNo.Text="",0,txtGrpNo.Text)

            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_UpdLn, strParamName, strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        End If

        taxlnid.Value = ""
        taxid.Value = strTaxID
        onLoad_Display(taxid.Value)
        onLoad_DisplayLn(taxid.Value)
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim strOpCd_Upd As String = "TX_CLSSETUP_TAXOBJECTRATELN_DELETE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim strTaxID As String = IIf(taxid.Value = "", "", taxid.Value)

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblTaxLnID")
        taxlnid.Value = lbl.Text.Trim

        strParamName = "TAXID|TAXLNID|UPDATEID"
        strParamValue = strTaxID & "|" & _
                       Trim(taxlnid.Value) & "|" & _
                       Trim(strUserId)
        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        onLoad_Display(taxid.Value)
        onLoad_DisplayLn(taxid.Value)
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        TaxLnID.Value = ""
        onLoad_BindButton()
        onLoad_Display(taxid.Value)
        onLoad_DisplayLn(taxid.Value)
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim btn As LinkButton

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)

        lbl = E.Item.FindControl("lblTaxLnID")
        TaxLnID.Value = lbl.Text.Trim
        lbl = E.Item.FindControl("lblTaxObject")
        txtTaxObject.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblWRate")
        txtWRate.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblWORate")
        txtWORate.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblAddNote")
        txtAddNote.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblRowNo")
        txtRowNo.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblGrpNo")
        txtGrpNo.Text = lbl.Text.Trim

        btn = E.Item.FindControl("lbDelete")
        btn.Visible = False
        btn = E.Item.FindControl("lbEdit")
        btn.Visible = False
        btn = E.Item.FindControl("lbCancel")
        btn.Visible = True

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

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

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

    Sub change_accumulative(ByVal Sender As Object, ByVal E As EventArgs)
        If chkAccumulative.Checked = True Then
            chkAccumulative.Text = "  Accumulative"
        Else
            chkAccumulative.Text = "  No Accumulative"
        End If
    End Sub

    Sub BindAccCodePPH21(Optional ByVal pv_strAccCode As String = "")
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

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = lblSelect.Text & strAccountTag
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlTransit.DataSource = dsForDropDown.Tables(0)
        ddlTransit.DataValueField = "AccCode"
        ddlTransit.DataTextField = "_Description"
        ddlTransit.DataBind()
        ddlTransit.SelectedIndex = intSelectedIndex

        ddlAlokasi.DataSource = dsForDropDown.Tables(0)
        ddlAlokasi.DataValueField = "AccCode"
        ddlAlokasi.DataTextField = "_Description"
        ddlAlokasi.DataBind()
        ddlAlokasi.SelectedIndex = intSelectedIndex

        ddlAlokasiCR.DataSource = dsForDropDown.Tables(0)
        ddlAlokasiCR.DataValueField = "AccCode"
        ddlAlokasiCR.DataTextField = "_Description"
        ddlAlokasiCR.DataBind()
        ddlAlokasiCR.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub
End Class
