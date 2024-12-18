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

Public Class CB_StdRpt_ReceiptList : Inherits Page

    Protected RptSelect As UserControl
    Dim objCBRpt As New agri.CB.clsReport()
    Dim objCBTrx As New agri.CB.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()


    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblBillParty As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCOA As Label
    Protected WithEvents lblBlkCode As Label

    Protected WithEvents txtReceiptIDFrom As TextBox    
    Protected WithEvents txtReceiptIDTo As TextBox
    Protected WithEvents txtBillParty As TextBox
    Protected WithEvents txtBankCode As TextBox

    Protected WithEvents txtChequeNo As TextBox
    Protected WithEvents txtDocumentIDFrom As TextBox
    Protected WithEvents txtDocumentIDTo As TextBox
    Protected WithEvents ddlDocumentType As DropDownList
    Protected WithEvents txtAccount As TextBox
    Protected WithEvents ddlStatus As DropDownList 

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()
    Dim objTermTypeDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As String


    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_CBACCMONTH")
        strAccYear = Session("SS_CBACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        
        

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatus()     
            End If
       End If
    End Sub

    Sub BindStatus()
        ddlStatus.Items.Add(New ListItem("All", 0))
        ddlStatus.Items.Add(New ListItem(objCBTrx.mtdGetReceiptStatus(objCBTrx.EnumReceiptStatus.Active), objCBTrx.EnumReceiptStatus.Active))
        ddlStatus.Items.Add(New ListItem(objCBTrx.mtdGetReceiptStatus(objCBTrx.EnumReceiptStatus.Confirmed), objCBTrx.EnumReceiptStatus.Confirmed))
        ddlStatus.Items.Add(New ListItem(objCBTrx.mtdGetReceiptStatus(objCBTrx.EnumReceiptStatus.Void), objCBTrx.EnumReceiptStatus.Void))        
        ddlStatus.Items.Add(New ListItem(objCBTrx.mtdGetReceiptStatus(objCBTrx.EnumReceiptStatus.Deleted), objCBTrx.EnumReceiptStatus.Deleted))
        ddlStatus.Items.Add(New ListItem(objCBTrx.mtdGetReceiptStatus(objCBTrx.EnumReceiptStatus.Closed), objCBTrx.EnumReceiptStatus.Closed))        
    End Sub      
    




    
    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblBillParty.Text =  GetCaption(objLangCap.EnumLangCap.BillParty)
        lblCOA.Text = GetCaption(objLangCap.EnumLangCap.Account)
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CB_STDRPT_INVOICE_LIST_COST_LEVEL_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/CB_StdRpt_Selection.aspx")
        End Try
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CB_STDRPT_INVOICE_LIST_CLSLANGCAR_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/CB_StdRpt_Selection.aspx")
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



    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strSupplier As String
        Dim strStmtType As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strDateSetting As String
        Dim strlblBillParty As String
        Dim strlblCOA As String
        Dim strBlkCode As String

        Dim strReceiptIDFrom As String
        Dim strReceiptIDTo As String
        Dim strBillParty As String
        Dim strBankCode As String
        Dim strChequeNo As String
        Dim strStatus As String
        Dim strlblLocation As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String

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

        strReceiptIDFrom = Trim(txtReceiptIDFrom.Text)
        strReceiptIDTo = Trim(txtReceiptIDTo.Text)
        strlblBillParty = Trim(lblBillParty.Text) & " Code"
        strBillParty = Trim(txtBillParty.Text)
        strlblCOA = Trim(lblCOA.Text) & " Code"
        strBlkCode = lblBlkCode.Text.Trim() & " Code"
        strBankCode = Trim(txtBankCode.Text)
        strChequeNo = Trim(txtChequeNo.Text)
        strStatus = Trim(ddlStatus.SelectedItem.value)
        strlblLocation = Trim(lblLocation.Text)

        strReceiptIDFrom = Server.UrlEncode(strReceiptIDFrom)
        strReceiptIDTo = Server.UrlEncode(strReceiptIDTo)
        strlblBillParty = Server.UrlEncode(strlblBillParty)
        strBillParty = Server.UrlEncode(strBillParty)
        strBankCode = Server.UrlEncode(strBankCode)
        strChequeNo = Server.UrlEncode(strChequeNo)
        strStatus = Server.UrlEncode(strStatus)
        strlblLocation = Server.UrlEncode(strlblLocation)
        strlblCOA = Server.UrlEncode(strlblCOA)
        strBlkCode = Server.UrlEncode(strBlkCode)
    
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
        Response.Write("<Script Language=""JavaScript"">window.open(""CB_StdRpt_ReceiptListPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                            "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & lblLocation.Text & "&lblCOA=" & strlblCOA & "&lblBlkCode=" & strBlkCode & _
                            "&ReceiptIDFrom=" & strReceiptIDFrom & "&ReceiptIDTo=" & strReceiptIDTo & "&BillParty=" & strBillParty & "&lblBillParty=" & strlblBillParty & _
                            "&BankCode=" & strBankCode & "&ChequeNo=" & strChequeNo & "&Status=" & strStatus & "&strlblLocation=" & strlblLocation & _
                            """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class

