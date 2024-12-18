
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services

Public Class AR_StdRpt_ContractInvoice : Inherits Page

    Protected RptSelect As UserControl

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objCMRpt As New agri.CM.clsReport()
    Dim objBITrx As New agri.BI.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objBPDs As New Object()
    Dim objSysCfgDs As New Object()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objAdmin As New agri.Admin.clsUom()
    Private objHRSetup As New agri.HR.clsSetup

    Protected WithEvents ddlProduct As DropDownList
    Protected WithEvents ddlUOMCode As DropDownList
    Protected WithEvents ddlFromInvoiceId As DropDownList
    Protected WithEvents ddlToInvoiceId As DropDownList
    Protected WithEvents txtFromInvoiceDate As Textbox
    Protected WithEvents txtToInvoiceDate As Textbox
    Protected WithEvents txtBuyer As Textbox
    Protected WithEvents ddlInvDocType As Dropdownlist
    Protected WithEvents lblBillParty As Label
    Protected WithEvents txtUndName As TextBox
    Protected WithEvents lblUndName As Label
    Protected WithEvents txtUndPost As Textbox
    Protected WithEvents lblUndPost As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents ChkPPN As CheckBox
    Protected WithEvents ddlBankCode As DropDownList

    Dim TrMthYr As HtmlTableRow

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Private dsBank As DataSet
    Dim objUOMDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objInvoiceDs As New Object()
    Dim strBankAccNo As String
    Dim strBankName As String
    Dim strBankBranch As String
    Dim strUndName As String
    Dim strUndPost As String
    Dim strLocType as String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_ARACCMONTH")
        strAccYear = Session("SS_ARACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")     
        Else
            If Not Page.IsPostBack
                lblUndName.Text = strUndName
                lblUndPost.text = strUndPost
                If Trim(strUndName) <> "" Then
                    txtUndName.Text = strUndName
                End If
                If Trim(strUndPost) <> "" Then
                    txtUndPost.text = strUndPost
                End If 
                onload_GetLangCap()
                BindInvoiceType()
                BindProductList("")
                BindUOMCode("")
                BindBankCode(ddlBankCode, "")
                BindInvoice("")
            End If
        End If
    End Sub

    Sub BindInvoiceType()
        ddlInvDocType.Items.Add(New ListItem(objBITrx.mtdGetInvoiceDocType(objBITrx.EnumInvoiceDocType.Manual), objBITrx.EnumInvoiceDocType.Manual))
        ddlInvDocType.Items.Add(New ListItem(objBITrx.mtdGetInvoiceDocType(objBITrx.EnumInvoiceDocType.Manual_Millware), objBITrx.EnumInvoiceDocType.Manual_Millware))
        ddlInvDocType.Items.Add(New ListItem(objBITrx.mtdGetInvoiceDocType(objBITrx.EnumInvoiceDocType.Auto_Millware), objBITrx.EnumInvoiceDocType.Auto_Millware))
        ddlInvDocType.SelectedIndex = 0
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("SelLocation")
        htmltr.visible = False

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.visible = True

        htmltr = RptSelect.FindControl("SelDecimal")
        htmltr.visible = False

        If Page.IsPostBack Then
        end if
    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strFromInvoiceId As String
        Dim strToInvoiceId As String
        Dim strFromInvoiceDate As String
        Dim strToInvoiceDate As String
        Dim strBillPartyCode As String
        Dim strInvoiceType As String
        Dim strDec As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strSupp As String
        Dim strParam As String

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim txt As TextBox

        Dim strDateSetting As String
        Dim objDateFrom As String
        Dim objDateTo As String
        Dim objDateFormat As String
        Dim intCnt As Integer
        Dim strPPN As String
        Dim strProduct As String
        Dim strUOM As String
        Dim strOpCd_GetBankDetail As String = "HR_CLSSETUP_BANK_GET"
        Dim objBankDs As New Object()
        Dim strBankAccOwner As String

        strFromInvoiceId = ddlFromInvoiceId.text
        strToInvoiceId = ddlToInvoiceId.text
        strFromInvoiceDate = txtFromInvoiceDate.text
        strToInvoiceDate = txtToInvoiceDate.text
        strBillPartyCode = txtBuyer.text
        strInvoiceType = ddlInvDocType.SelectedItem.Value

        ddlist = RptSelect.FindControl("lstAccMonth")
        strSelAccMonth = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstAccYear")
        strSelAccYear = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)

        strParam = Server.UrlEncode(Trim(ddlBankCode.SelectedItem.Value))

        Try
            intErrNo = objHRSetup.mtdGetBank(strOpCd_GetBankDetail, _
                                             strParam, _
                                             objBankDs, _
                                             True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objBankDs.Tables(0).Rows.Count > 0 Then
            strBankAccNo = Server.UrlEncode(Trim(objBankDs.Tables(0).Rows(0).Item("AccNo")))
            strBankName = Server.UrlEncode(Trim(objBankDs.Tables(0).Rows(0).Item("Description")))
            strBankBranch = Server.UrlEncode(Trim(objBankDs.Tables(0).Rows(0).Item("Address")))
            strBankAccOwner = Server.UrlEncode(Trim(objBankDs.Tables(0).Rows(0).Item("AccOwner")))
        Else
            strBankAccNo = ""
            strBankName = ""
            strBankBranch = ""
            strBankAccOwner = ""
        End If

        strUndName = Server.UrlEncode(txtUndName.Text.Trim())
        strUndPost = Server.UrlEncode(txtUndPost.Text.Trim())
        strPPN = IIf(ChkPPN.Checked = True, "Yes", "No")
        strProduct = ddlProduct.SelectedItem.Value
        strUOM = ddlUOMCode.SelectedItem.Value

        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (Trim(strFromInvoiceDate) = "" And Trim(strToInvoiceDate) = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strFromInvoiceDate, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strToInvoiceDate, objDateFormat, objDateTo) = True Then
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
                Exit Sub
            End If
        End If


        Response.Write("<Script Language=""JavaScript"">window.open(""AR_StdRpt_ContractInvoicePreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strLocation & _
                       "&SelAccMonth=" & strSelAccMonth & _
                       "&SelAccYear=" & strSelAccYear & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & _
                       "&FromInvId=" & strFromInvoiceId & _
                       "&ToInvId=" & strToInvoiceId & _
                       "&FromInvDate=" & objDateFrom & _
                       "&ToInvDate=" & objDateTo & _
                       "&BillPartyCode=" & Server.UrlEncode(strBillPartyCode) & _
                       "&InvType=" & strInvoiceType & _
                       "&BillPartyTag=" & lblBillParty.Text & _
                       "&BankAccNo=" & strBankAccNo & _
                       "&BankName=" & strBankName & _
                       "&BankBranch=" & strBankBranch & _
                       "&UndName=" & strUndName & _
                       "&UndPost=" & strUndPost & _
                       "&PPN=" & strPPN & _
                       "&Product=" & strProduct & _
                       "&UOM=" & strUOM & _
                       "&BankAccOwner=" & strBankAccOwner & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblBillParty.Text = GetCaption(objLangCap.EnumLangCap.BillParty)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_TRIALBALANCE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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

    Sub PPN_Type(ByVal Sender As Object, ByVal E As EventArgs)
        If ChkPPN.Checked = True Then
            ChkPPN.Text = "  Yes"
        Else
            ChkPPN.Text = "  No"
        End If
    End Sub

    Sub BindProductList(ByVal pv_strProdCode As String)
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer

        If ddlProduct.Items.Count = 0 Then
            ddlProduct.Items.Add(New ListItem("Select Product", ""))
            ddlProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.CPO), objWMTrx.EnumWeighBridgeTicketProduct.CPO))
            ddlProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.FFB), objWMTrx.EnumWeighBridgeTicketProduct.FFB))
            ddlProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.PK), objWMTrx.EnumWeighBridgeTicketProduct.PK))
            ddlProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.EFB), objWMTrx.EnumWeighBridgeTicketProduct.EFB))
            ddlProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Shell), objWMTrx.EnumWeighBridgeTicketProduct.Shell))
            ddlProduct.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Others), objWMTrx.EnumWeighBridgeTicketProduct.Others))
        End If

        If Trim(pv_strProdCode) <> "" Then
            For intCnt = 0 To ddlProduct.Items.Count - 1
                If ddlProduct.Items(intCnt).Value = pv_strProdCode Then
                    intSelectedIndex = intCnt
                End If
            Next
            ddlProduct.SelectedIndex = intSelectedIndex
        Else
            ddlProduct.SelectedIndex = 0
        End If

    End Sub

    Sub BindUOMCode(ByVal pv_strUOMCode As String)
        Dim strOpCode_GetUOM As String = "ADMIN_CLSUOM_UOM_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            strParam = "Order By UOM.UOMCode|And UOM.Status = '" & _
                        objAdmin.EnumUOMStatus.Active & "' "

            intErrNo = objGLSetup.mtdGetMasterList(strOpCode_GetUOM, strParam, objGLSetup.EnumGLMasterType.AccountCode, objUOMDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CREDITNOTE_GET_SUPP2&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_cnlist.aspx")
        End Try

        For intCnt = 0 To objUOMDs.Tables(0).Rows.Count - 1
            objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode"))
            objUOMDs.Tables(0).Rows(intCnt).Item("UOMDesc") = objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") & " (" & Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMDesc")) & ")"
            If objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(pv_strUOMCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt


        Dim dr As DataRow
        dr = objUOMDs.Tables(0).NewRow()
        dr("UOMCode") = ""
        dr("UOMDesc") = "Please select UOM Code"
        objUOMDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlUOMCode.DataSource = objUOMDs.Tables(0)
        ddlUOMCode.DataValueField = "UOMCode"
        ddlUOMCode.DataTextField = "UOMDesc"
        ddlUOMCode.DataBind()
        ddlUOMCode.SelectedIndex = intSelectedIndex
    End Sub


    Sub BindBankCode(ByVal pv_ddl As DropDownList, ByVal pv_strCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_BANK_SEARCH"
        Dim strParameter As String
        Dim intSelectedIndex As Integer = 0

        strParameter = "||||B.BankCode|ASC|"
        Try
            If dsBank Is Nothing OrElse dsBank.Tables.Count() = 0 Then
                If objHRSetup.mtdGetBank(strOpCd, strParameter, dsBank, False) = 0 Then
                    For intCnt As Integer = 0 To dsBank.Tables(0).Rows.Count - 1
                        dsBank.Tables(0).Rows(intCnt).Item("BankCode") = Trim(dsBank.Tables(0).Rows(intCnt).Item("BankCode"))
                        dsBank.Tables(0).Rows(intCnt).Item("Description") = Trim(dsBank.Tables(0).Rows(intCnt).Item("BankCode")) & " (" & Trim(dsBank.Tables(0).Rows(intCnt).Item("Description")) & ")"
                    Next

                    Dim dr As DataRow
                    dr = dsBank.Tables(0).NewRow()
                    dr("BankCode") = ""
                    dr("Description") = "Please select Bank Code"
                    dsBank.Tables(0).Rows.InsertAt(dr, 0)
                Else
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREG_BINDBANKCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=CM/trx/CM_trx_ContractRegDet.aspx")
                End If
            End If
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREG_BINDBANKCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=CM/trx/CM_trx_ContractRegDet.aspx")
        End Try

        pv_ddl.DataSource = dsBank.Tables(0)
        pv_ddl.DataValueField = "BankCode"
        pv_ddl.DataTextField = "Description"
        pv_ddl.DataBind()

        Dim itm As ListItem = pv_ddl.Items.FindByValue(pv_strCode.Trim())
        If Not itm Is Nothing Then
            intSelectedIndex = pv_ddl.Items.IndexOf(itm)
        End If

        pv_ddl.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindInvoice(ByVal pv_Invoice As String)
        Dim strInvoiceType As String
        Dim strSrchStatus As String
        Dim strSortExpression As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strOpCdGet As String = "BI_CLSTRX_INVOICE_GET"
        Dim lbl As Label

        strInvoiceType = objBITrx.EnumInvoiceDocType.Manual
        strSrchStatus = objBITrx.EnumInvoiceStatus.Confirmed
        strSortExpression = "InvoiceID"
        strParam = "||" & _
                   strInvoiceType & "|" & _
                   strSrchStatus & "||" & strSortExpression & "|"

        Try
            intErrNo = objBITrx.mtdGetInvoice(strOpCdGet, _
                                              strCompany, _
                                              strLocation, _
                                              strUserId, _
                                              strAccMonth, _
                                              strAccYear, _
                                              strParam, _
                                              objInvoiceDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_INVOICELIST_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objInvoiceDs.Tables(0).Rows.Count - 1
            objInvoiceDs.Tables(0).Rows(intCnt).Item("InvoiceId") = Trim(objInvoiceDs.Tables(0).Rows(intCnt).Item("InvoiceId"))
        Next

        Dim dr As DataRow
        dr = objInvoiceDs.Tables(0).NewRow()
        dr("InvoiceId") = ""
        dr("InvoiceId") = "Please select Invoice No"
        objInvoiceDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlFromInvoiceId.DataSource = objInvoiceDs.Tables(0)
        ddlFromInvoiceId.DataValueField = "InvoiceId"
        ddlFromInvoiceId.DataTextField = "InvoiceId"
        ddlFromInvoiceId.DataBind()

        ddlToInvoiceId.DataSource = objInvoiceDs.Tables(0)
        ddlToInvoiceId.DataValueField = "InvoiceId"
        ddlToInvoiceId.DataTextField = "InvoiceId"
        ddlToInvoiceId.DataBind()
    End Sub
End Class
