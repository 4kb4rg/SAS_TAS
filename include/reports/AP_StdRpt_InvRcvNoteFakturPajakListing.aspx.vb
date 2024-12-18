
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services

Public Class AP_StdRpt_InvRcvNoteFakturPajakListing : Inherits Page

    Protected RptSelect As UserControl
    Dim objAP As New agri.AP.clsReport()
    Dim objAP1 As New agri.AP.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblErrInvRcvRefDateFrom As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents cbExcel As CheckBox
    Protected lblPageTitle As Label
    Protected lblAccPayable As Label
    Protected lblListing As Label
    Protected lblDash As Label
    Protected lblInvRcv As Label
    Protected lblInvRcvRefNo As Label
    Protected lblInvRcvRefDateFrom As Label
    Protected lblInvRcvRefDateTo As Label
    Protected lblRefNo As Label
    Protected lblRefDateFrom As Label
    Protected lblRefDateTo As Label
    Protected WithEvents txtInvRcvNo As TextBox
    Protected WithEvents txtDocNoFrom As TextBox
    Protected WithEvents txtDocNoTo As TextBox
    Protected WithEvents txtInvRcvRefDateFrom As TextBox
    Protected WithEvents txtInvRcvRefDateTo As TextBox
    Protected WithEvents txtPOID As TextBox
    Protected WithEvents txtCreditTerm As TextBox
    Protected WithEvents txtCreditTermType As TextBox
    Protected WithEvents txtSuppCode As TextBox
    Protected WithEvents ddlInvoiceType As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents ddlTermType As DropDownList
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents txtInvDueDateFrom As TextBox
    Protected WithEvents txtInvDueDateTo As TextBox

    Dim objLangCapDs As New Object()
    Dim objTermTypeDs As New Object()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As String
    Dim strBlock As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        lblDate.Visible = False
        lblDateFormat.Visible = False
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindInvoiceType()
                BindStatus()
                BindTermType("")
            End If
        End If
    End Sub

    Sub BindInvoiceType()
        ddlInvoiceType.Items.Add(New ListItem("All", 0))
        ddlInvoiceType.Items.Add(New ListItem(objAP1.mtdGetInvoiceType(objAP1.EnumInvoiceType.SupplierPO), objAP1.EnumInvoiceType.SupplierPO))
        ddlInvoiceType.Items.Add(New ListItem(objAP1.mtdGetInvoiceType(objAP1.EnumInvoiceType.Others), objAP1.EnumInvoiceType.Others))
        ddlInvoiceType.Items.Add(New ListItem(objAP1.mtdGetInvoiceType(objAP1.EnumInvoiceType.TransportFee), objAP1.EnumInvoiceType.TransportFee))
        ddlInvoiceType.Items.Add(New ListItem(objAP1.mtdGetInvoiceType(objAP1.EnumInvoiceType.FFBSupplier), objAP1.EnumInvoiceType.FFBSupplier))
        ddlInvoiceType.Items.Add(New ListItem(objAP1.mtdGetInvoiceType(objAP1.EnumInvoiceType.ContractorWorkOrder), objAP1.EnumInvoiceType.ContractorWorkOrder))
    End Sub

    Sub BindStatus()
        ddlStatus.Items.Add(New ListItem("All", 0))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetInvoiceRcvNoteStatus(objAP1.EnumInvoiceRcvNoteStatus.Active), objAP1.EnumInvoiceRcvNoteStatus.Active))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetInvoiceRcvNoteStatus(objAP1.EnumInvoiceRcvNoteStatus.Invoiced), objAP1.EnumInvoiceRcvNoteStatus.Invoiced))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetInvoiceRcvNoteStatus(objAP1.EnumInvoiceRcvNoteStatus.Deleted), objAP1.EnumInvoiceRcvNoteStatus.Deleted))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetInvoiceRcvNoteStatus(objAP1.EnumInvoiceRcvNoteStatus.Cancelled), objAP1.EnumInvoiceRcvNoteStatus.Cancelled))
    End Sub

    Sub BindTermType(ByVal pv_TermType As String)
        Dim strOpCd_GetTermType As String = "ADMIN_CLSSHARE_CREDITTERMTYPE_LIST_GET1"
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intDefIndex As Integer = 0
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objAdminShare.mtdGetCreditTermType(strOpCd_GetTermType, strParam, objTermTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INVOICERCV_GET_TERMTYPELIST&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_polist.aspx")
        End Try


        For intCnt = 0 To objTermTypeDs.Tables(0).Rows.Count - 1
            If objTermTypeDs.Tables(0).Rows(intCnt).Item("CreditTermTypeCode") = pv_TermType Then
                intSelectedIndex = intCnt
            End If

            If objTermTypeDs.Tables(0).Rows(intCnt).Item("DefaultInd") = "0" Then
                intDefIndex = intCnt
            End If
        Next intCnt

        dr = objTermTypeDs.Tables(0).NewRow()
        dr("CreditTermTypeCode") = "13"
        dr("Description") = "All Credit Terms"
        objTermTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        If intSelectedIndex = 0 Then intSelectedIndex = intDefIndex
        ddlTermType.DataSource = objTermTypeDs.Tables(0)
        ddlTermType.DataValueField = "CreditTermTypeCode"
        ddlTermType.DataTextField = "Description"
        ddlTermType.DataBind()
        ddlTermType.SelectedIndex = 0
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblInvRcv.Text = "Invoice Reception"
        lblInvRcvRefNo.Text = lblInvRcv.Text & lblRefNo.Text
        lblInvRcvRefDateFrom.Text = "Invoice Reception Date From" 'lblInvRcv.Text & lblRefDateFrom.Text
        lblInvRcvRefDateTo.Text = "Invoice Reception Date To" 'lblInvRcv.Text & lblRefDateTo.Text
        lblPageTitle.Text = lblAccPayable.Text & lblDash.Text & UCase(lblInvRcv.Text) & lblListing.Text
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_LIST_CLSLANGCAP_BUSSTERM_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/AP_StdRpt_Selection.aspx")
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

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strDateSetting As String
        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strParam As String
        Dim objSysCfgDs As New DataSet()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String
        Dim strDueDateFrom As String
        Dim strDueDateTo As String
        Dim objDueDateFrom As String
        Dim objDueDateTo As String
        Dim strExportToExcel As String


        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

        If strUserLoc = "" Then
            templblUL = RptSelect.FindControl("lblUserLoc")
            templblUL.Visible = True
            Exit Sub
        Else
            If Left(strUserLoc, 3) = "','" Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
            ElseIf Right(strUserLoc, 3) = "','" Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
            End If
        End If


        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=IN_STDRPT_STKISSUE_GET_CONFIG_DATE&errmesg=" & Exp.ToString() & "&redirect=../../en/reports/IN_StdRpt_Selection.aspx")
        End Try

        strDateFrom = Trim(txtInvRcvRefDateFrom.Text)
        strDateTo = Trim(txtInvRcvRefDateTo.Text)
        strDueDateFrom = Trim(txtInvDueDateFrom.Text)
        strDueDateTo = Trim(txtInvDueDateTo.Text)

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strDateFrom = "" And strDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
                Exit Sub
            End If
        End If

        If Not (strDueDateFrom = "" And strDueDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strDueDateFrom, objDateFormat, objDueDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDueDateTo, objDateFormat, objDueDateTo) = True Then
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
                Exit Sub
            End If
        End If
        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

        'If Not (strDateFrom = "" And strDateTo = "") Then
        '    If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
        '        Response.Write("<Script Language=""JavaScript"">window.open(""AP_StdRpt_InvRcvListingPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
        '                    "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & lblLocation.Text & _
        '                    "&DocNoFrom=" & Server.UrlEncode(Trim(txtDocNoFrom.Text)) & "&DocNoTo=" & Server.UrlEncode(Trim(txtDocNoTo.Text)) & _
        '                    "&txtInvRcvNo=" & Server.UrlEncode(Trim(txtInvRcvNo.Text)) & "&txtInvRcvRefDateFrom=" & objDateFrom & "&txtPOID=" & Server.UrlEncode(Trim(txtPOID.Text)) & "&txtInvRcvRefDateTo=" & objDateTo & _
        '                    "&txtCreditTermTypeValue=" & Trim(ddlTermType.SelectedItem.Value) & "&txtCreditTerm=" & Trim(txtCreditTerm.Text) & "&txtCreditTermType=" & Trim(ddlTermType.SelectedItem.Text) & _
        '                    "&txtStatus=" & Trim(ddlStatus.SelectedItem.Value) & _
        '                    "&txtSuppCode=" & Server.UrlEncode(Trim(txtSuppCode.Text)) & _
        '                    "&ddlInvoiceType=" & Server.UrlEncode(Trim(ddlInvoiceType.SelectedItem.Value)) & "&lblInvRcv=" & lblInvRcv.Text & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        '    Else
        '        lblDateFormat.Text = objDateFormat & "."
        '        lblDate.Visible = True
        '        lblDateFormat.Visible = True
        '    End If
        'Else
        Response.Write("<Script Language=""JavaScript"">window.open(""AP_StdRpt_InvRcvNoteFakturPajakListingPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                    "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & lblLocation.Text & _
                    "&DocNoFrom=" & Server.UrlEncode(Trim(txtDocNoFrom.Text)) & "&DocNoTo=" & Server.UrlEncode(Trim(txtDocNoTo.Text)) & _
                    "&txtInvRcvNo=" & Server.UrlEncode(Trim(txtInvRcvNo.Text)) & "&txtInvRcvRefDateFrom=" & objDateFrom & "&txtPOID=" & Server.UrlEncode(Trim(txtPOID.Text)) & "&txtInvRcvRefDateTo=" & objDateTo & _
                    "&txtCreditTermTypeValue=" & Trim(ddlTermType.SelectedItem.Value) & "&txtCreditTerm=" & Trim(txtCreditTerm.Text) & "&txtCreditTermType=" & Trim(ddlTermType.SelectedItem.Text) & _
                    "&txtStatus=" & Trim(ddlStatus.SelectedItem.Value) & _
                    "&txtSuppCode=" & Server.UrlEncode(Trim(txtSuppCode.Text)) & _
                    "&txtInvDueDateFrom=" & objDueDateFrom & "&txtInvDueDateTo=" & objDueDateTo & _
                    "&ddlInvoiceType=" & Server.UrlEncode(Trim(ddlInvoiceType.SelectedItem.Value)) & "&lblInvRcv=" & lblInvRcv.Text & _
                    "&ExportToExcel=" & strExportToExcel & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        'End If
        objSysCfgDs = Nothing
    End Sub

End Class
