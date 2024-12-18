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

Public Class AP_StdRpt_PaymentListing : Inherits Page

    Protected RptSelect As UserControl
    Dim objAP As New agri.AP.clsReport()
    Dim objAP1 As New agri.AP.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label    
    Protected WithEvents lblCOACode As Label    
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label

    Protected WithEvents txtPaymentIDFrom As TextBox
    Protected WithEvents txtPaymentIDTo As TextBox
    Protected WithEvents txtSuppCode As TextBox
    Protected WithEvents ddlPaymentType As DropDownList
    Protected WithEvents ddlBankCode As DropDownList
    Protected WithEvents txtChequeNo As TextBox
    Protected WithEvents txtDocumentID As TextBox
    Protected WithEvents txtCOACode As TextBox

    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents lstBlkType As DropDownList
    Protected WithEvents PrintPrev As ImageButton
    Protected lblInvRcv As Label    

    Dim objLangCapDs As New Object()
    Dim objTermTypeDs As New Object()
    Dim objBankDs As New Object()
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
    Dim strLocType as String

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
                BindStatus()
                BindBankCode("")
            End If

        End If
    End Sub


    Sub BindBankCode(ByVal pv_strBankCode As String)
        Dim strOpCode As String = "HR_CLSSETUP_BANK_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedBankIndex As Integer = 0

        strParam = "|"

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   objHRSetup.EnumHRMasterType.Bank, _
                                                   objBankDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_TRX_PAYMENT_GET_INVOICERCV&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_paylist.aspx")
        End Try

        For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
            objBankDs.Tables(0).Rows(intCnt).Item("BankCode") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode"))
            objBankDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("Description"))
            If pv_strBankCode = objBankDs.Tables(0).Rows(intCnt).Item("BankCode") Then
                intSelectedBankIndex = intCnt + 1
            End If
        Next intCnt

        Dim dr As DataRow
        dr = objBankDs.Tables(0).NewRow()
        dr("BankCode") = ""
        dr("Description") = "All"
        objBankDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBankCode.DataSource = objBankDs.Tables(0)
        ddlBankCode.DataValueField = "BankCode"
        ddlBankCode.DataTextField = "Description"
        ddlBankCode.DataBind()
        ddlBankCode.SelectedIndex = intSelectedBankIndex
    End Sub


    Sub BindStatus()

        ddlStatus.Items.Add(New ListItem("All", 0))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetPaymentStatus(objAP1.EnumPaymentStatus.Active), objAP1.EnumPaymentStatus.Active))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetPaymentStatus(objAP1.EnumPaymentStatus.Confirmed), objAP1.EnumPaymentStatus.Confirmed))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetPaymentStatus(objAP1.EnumPaymentStatus.Deleted), objAP1.EnumPaymentStatus.Deleted))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetPaymentStatus(objAP1.EnumPaymentStatus.Void), objAP1.EnumPaymentStatus.Void))
        ddlStatus.Items.Add(New ListItem(objAP1.mtdGetPaymentStatus(objAP1.EnumPaymentStatus.Closed), objAP1.EnumPaymentStatus.Closed))

    End Sub


    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblCOACode.Text = GetCaption(objLangCap.EnumLangCap.Account)
        lblInvRcv.Text = GetCaption(objLangCap.EnumLangCap.InvoiceReceive)
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AP_STDRPT_STMTOFACC_LIST_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/AP_StdRpt_Selection.aspx")
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


        Response.Write("<Script Language=""JavaScript"">window.open(""AP_StdRpt_PaymentListingPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                    "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & lblLocation.Text & _
                    "&txtCOACode=" & Server.UrlEncode(Trim(txtCOACode.Text)) & "&txtSuppCode=" & Server.UrlEncode(Trim(txtSuppCode.Text)) & _
                    "&txtStatus=" & Trim(ddlStatus.SelectedItem.Value) & "&txtPaymentType=" & Trim(ddlPaymentType.SelectedItem.Value) & _
                    "&lblCOACode=" & Trim(lblCOACode.Text) & "&txtBankCode=" & Trim(ddlBankCode.SelectedItem.Value) & "&txtChequeNo=" & Server.UrlEncode(Trim(txtChequeNo.Text)) & _
                    "&txtDocumentID=" & txtDocumentID.Text & "&txtPaymentIDFrom=" & Server.UrlEncode(Trim(txtPaymentIDFrom.Text)) & _
                    "&txtPaymentIDTo=" & Server.UrlEncode(Trim(txtPaymentIDTo.Text)) & "&lblInvRcv=" & Server.UrlEncode(lblInvRcv.Text) & _
                    """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
