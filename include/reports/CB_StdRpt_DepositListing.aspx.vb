
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

Public Class CB_StdRpt_DepositListing : Inherits Page

    Protected RptSelect As UserControl
    Dim objCB As New agri.CB.clsReport()
    Dim objCB1 As New agri.CB.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objCMSetup As New agri.CM.clsSetup()
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

    Protected WithEvents txtDepositCodeFrom As TextBox
    Protected WithEvents txtDepositCodeTo As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlBankCode As DropDownList
    Protected WithEvents ddlType As DropDownList
    Protected WithEvents ddlCurrency As DropDownList
    Protected WithEvents txtCOACode As TextBox
    Protected WithEvents ddlStatus As DropDownList
    
  
    Protected WithEvents PrintPrev As ImageButton
    Protected lblDeposit As Label    

    Dim objLangCapDs As New Object()
    Dim objBankDs As New Object()
    Dim objCurrDs As New Object()

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

        lblDate.Visible = False
        lblDateFormat.Visible = False


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatus()
                BindBankCode("")
                BindCurrency("")
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
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CB_RPT_DEPOSIT_GET_BANK&errmesg=" & lblErrMessage.Text & "&redirect=")
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

   Sub BindCurrency(ByVal pv_strCurrency As String)
        Dim strOpCode As String = "CM_CLSSETUP_CURRENCY_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedCurrIndex As Integer = 0

        strParam = "|"

        Try
            intErrNo = objCMSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   0, _
                                                   objCurrDs)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CB_RPT_DEPOSIT_GET_CURRENCY&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objCurrDs.Tables(0).Rows.Count - 1
            objCurrDs.Tables(0).Rows(intCnt).Item("CurrencyCode") = objCurrDs.Tables(0).Rows(intCnt).Item("CurrencyCode").Trim()
            objCurrDs.Tables(0).Rows(intCnt).Item("Description") = objCurrDs.Tables(0).Rows(intCnt).Item("CurrencyCode") & " (" & objCurrDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If pv_strCurrency = objCurrDs.Tables(0).Rows(intCnt).Item("CurrencyCode") Then
                intSelectedCurrIndex = intCnt + 1
            End If
        Next intCnt

        Dim dr As DataRow
        dr = objCurrDs.Tables(0).NewRow()
        dr("CurrencyCode") = ""
        dr("Description") = "All" 
        objCurrDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlCurrency.DataSource = objCurrDs.Tables(0)
        ddlCurrency.DataValueField = "CurrencyCode"
        ddlCurrency.DataTextField = "Description"
        ddlCurrency.DataBind()
        ddlCurrency.SelectedIndex = intSelectedCurrIndex
    End Sub


    Sub BindStatus()

        ddlStatus.Items.Add(New ListItem("All", 0))
        ddlStatus.Items.Add(New ListItem(objCB1.mtdGetDepositStatus(objCB1.EnumDepositStatus.Active), objCB1.EnumDepositStatus.Active))
        ddlStatus.Items.Add(New ListItem(objCB1.mtdGetDepositStatus(objCB1.EnumDepositStatus.Cancelled), objCB1.EnumDepositStatus.Cancelled))
        ddlStatus.Items.Add(New ListItem(objCB1.mtdGetDepositStatus(objCB1.EnumDepositStatus.Confirmed), objCB1.EnumDepositStatus.Confirmed))
        ddlStatus.Items.Add(New ListItem(objCB1.mtdGetDepositStatus(objCB1.EnumDepositStatus.Deleted), objCB1.EnumDepositStatus.Deleted))
        ddlStatus.Items.Add(New ListItem(objCB1.mtdGetDepositStatus(objCB1.EnumDepositStatus.Withdrawn), objCB1.EnumDepositStatus.Withdrawn))

    End Sub


    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

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
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CB_STDRPT_STMTOFACC_LIST_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/CB_StdRpt_Selection.aspx")
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

        Response.Write("<Script Language=""JavaScript"">window.open(""CB_StdRpt_DepositListingPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                    "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblLocation=" & lblLocation.Text & _
                    "&txtCOACode=" & Server.UrlEncode(replace(Trim(txtCOACode.Text), "'", "''")) &  _
                    "&txtCurrency=" & Trim(ddlCurrency.SelectedItem.Value) & "&txtStatus=" & Trim(ddlStatus.SelectedItem.Value) & _
                    "&txtType=" & Trim(ddlType.SelectedItem.Value) & "&txtBankCode=" & Trim(ddlBankCode.SelectedItem.Value) & _
                    "&txtDescription=" & Replace(txtDescription.Text, "'", "''") & "&txtDepositCodeFrom=" & Server.UrlEncode(Trim(Replace(txtDepositCodeFrom.Text, "'", "''"))) & _
                    "&txtDepositCodeTo=" & Server.UrlEncode(Trim(Replace(txtDepositCodeTo.Text, "'", "''"))) & "&lblDeposit=" & Server.UrlEncode(lblDeposit.Text) & _
                    """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
