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

Public Class CB_StdRpt_ReceiptVoucher : Inherits Page

    Protected RptSelect As UserControl

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objCMRpt As New agri.CM.clsReport()
    Dim objCBTrx As New agri.CB.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objBPDs As New Object()
    Dim objSysCfgDs As New Object()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objAdmin As New agri.Admin.clsUom()
    Dim objCMTrx As New agri.CM.clsTrx()
    Dim objUOMDs As New Object()
    Dim objContractDs As New Object()

    Protected WithEvents txtFromReceiptId As Textbox
    Protected WithEvents txtToReceiptId As Textbox
    Protected WithEvents txtBuyer As Textbox
    Protected WithEvents lblBillParty As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents ddlStatus As DropDownList

    Protected WithEvents ddlProduct As DropDownList
    Protected WithEvents ddlUOMCode As DropDownList
    Protected WithEvents ddlContract As DropDownList
    Protected WithEvents txtVoucherNo As TextBox
    Protected WithEvents txtUndName As TextBox
    Protected WithEvents lblUndName As Label
    Protected WithEvents txtUndPost As TextBox
    Protected WithEvents lblUndPost As Label
    Protected WithEvents ChkPPN As CheckBox


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

    Dim objLangCapDs As New Object()

    Dim objAdminLoc As New agri.Admin.clsLoc()
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
                onload_GetLangCap()
                BindStatus()
                BindProductList("")
                BindUOMCode("")
                BindContract("")
            End If
        End If
    End Sub
    
    Sub BindStatus()
        ddlStatus.Items.Clear
        ddlStatus.Items.Add(New ListItem(objCBTrx.mtdGetReceiptStatus(objCBTrx.EnumReceiptStatus.All), objCBTrx.EnumReceiptStatus.All))
        ddlStatus.Items.Add(New ListItem(objCBTrx.mtdGetReceiptStatus(objCBTrx.EnumReceiptStatus.Active), objCBTrx.EnumReceiptStatus.Active))
        ddlStatus.Items.Add(New ListItem(objCBTrx.mtdGetReceiptStatus(objCBTrx.EnumReceiptStatus.Confirmed), objCBTrx.EnumReceiptStatus.Confirmed))
        ddlStatus.Items.Add(New ListItem(objCBTrx.mtdGetReceiptStatus(objCBTrx.EnumReceiptStatus.Void), objCBTrx.EnumReceiptStatus.Void))
        ddlStatus.Items.Add(New ListItem(objCBTrx.mtdGetReceiptStatus(objCBTrx.EnumReceiptStatus.Deleted), objCBTrx.EnumReceiptStatus.Deleted))
        ddlStatus.Items.Add(New ListItem(objCBTrx.mtdGetReceiptStatus(objCBTrx.EnumReceiptStatus.Closed), objCBTrx.EnumReceiptStatus.Closed))
        ddlStatus.SelectedIndex = 2
    End Sub  
    
    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strFromReceiptId As String
        Dim strToReceiptId As String
        Dim strBillPartyCode As String
        Dim strDec As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strSupp As String
        Dim strParam As String
        Dim strStatus As String
        Dim strVoucherNo As String
        Dim strProduct As String
        Dim strUOM As String
        Dim strContract As String
        Dim strPPN As String
        Dim strUndName As String
        Dim strUndPost As String

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim txt As TextBox
        Dim intCnt As Integer

        strFromReceiptId = txtFromReceiptId.Text
        strToReceiptId = txtToReceiptId.Text

        strBillPartyCode = txtBuyer.Text
        strVoucherNo = txtVoucherNo.Text
        strProduct = ddlProduct.SelectedItem.Value
        strUOM = ddlUOMCode.SelectedItem.Value
        strContract = ddlContract.SelectedItem.Value
        strPPN = IIf(ChkPPN.Checked = True, "Yes", "No")
        strUndName = Server.UrlEncode(txtUndName.Text.Trim())
        strUndPost = Server.UrlEncode(txtUndPost.Text.Trim())

        ddlist = RptSelect.FindControl("lstAccMonth")
        strSelAccMonth = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstAccYear")
        strSelAccYear = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.Text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)

        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If
        strStatus = ddlStatus.SelectedItem.Value
        Response.Write("<Script Language=""JavaScript"">window.open(""CB_StdRpt_ReceiptVoucherPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & Server.UrlEncode(strLocation) & _
                       "&SelAccMonth=" & strSelAccMonth & _
                       "&SelAccYear=" & strSelAccYear & _
                       "&RptId=" & Server.UrlEncode(strRptId) & _
                       "&RptName=" & Server.UrlEncode(strRptName) & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & _
                       "&FromReceiptId=" & Server.UrlEncode(strFromReceiptId) & _
                       "&ToReceiptId=" & Server.UrlEncode(strToReceiptId) & _
                       "&BillPartyCode=" & Server.UrlEncode(strBillPartyCode) & _
                       "&Status=" & Server.UrlEncode(strStatus) & _
                       "&BillPartyTag=" & Server.UrlEncode(lblBillParty.Text) & _
                       "&AccCodeTag=" & Server.UrlEncode(lblAccCode.Text) & _
                       "&BlkCodeTag=" & Server.UrlEncode(lblBlkCode.Text) & _
                       "&VoucherNo=" & strVoucherNo & _
                       "&Product=" & strProduct & _
                       "&UOM=" & strUOM & _
                       "&Contract=" & strContract & _
                       "&UndName=" & strUndName & _
                       "&UndPost=" & strUndPost & _
                       "&PPN=" & strPPN & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblBillParty.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & lblCode.Text
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        Else
            lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        End If

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

    Sub BindContract(ByVal pv_strContNo As String)
        Dim strParam As String
        Dim strOpCdGet As String = "CM_CLSTRX_CONTRACT_REG_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer
        Dim strSearch As String

        strSearch = "and ctr.LocCode = '" & strLocation & "' and ctr.status in ('1', '4') "
        strParam = strSearch & "|" & ""


        Try
            intErrNo = objCMTrx.mtdGetContract(strOpCdGet, strParam, 0, objContractDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=CM/Trx/CM_Trx_ContractRegList.aspx")
        End Try

        If objContractDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objContractDs.Tables(0).Rows.Count - 1
                objContractDs.Tables(0).Rows(intCnt).Item("ContractNo") = Trim(objContractDs.Tables(0).Rows(intCnt).Item("ContractNo"))
                If objContractDs.Tables(0).Rows(intCnt).Item("ContractNo") = pv_strContNo Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objContractDs.Tables(0).NewRow()
        dr("ContractNo") = ""
        dr("ContractNo") = "Please select Contract No"
        objContractDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlContract.DataSource = objContractDs.Tables(0)
        ddlContract.DataValueField = "ContractNo"
        ddlContract.DataTextField = "ContractNo"
        ddlContract.DataBind()
        ddlContract.SelectedIndex = intSelectedIndex
    End Sub

    Sub PPN_Type(ByVal Sender As Object, ByVal E As EventArgs)
        If ChkPPN.Checked = True Then
            ChkPPN.Text = "  Yes"
        Else
            ChkPPN.Text = "  No"
        End If
    End Sub
End Class
