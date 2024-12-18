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

Public Class AP_StdRpt_CreditorJournalList : Inherits Page

    Protected RptSelect As UserControl
    Dim objAPRpt As New agri.AP.clsReport()
    Dim objAPTrx As New agri.AP.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents LocTag As Label
    Protected WithEvents BlockTag As Label
    Protected WithEvents AccountTag As Label
    Protected WithEvents VehTag As Label
    Protected WithEvents VehExpenseTag As Label


    Protected WithEvents txtCreditJrnIDFrom As TextBox    
    Protected WithEvents txtCreditJrnIDTo As TextBox
    Protected WithEvents txtSupplier As TextBox

    Protected WithEvents ddlJournalType As DropDownList
    Protected WithEvents ddlStatus As DropDownList 

    Protected WithEvents PrintPrev As ImageButton

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
        
        

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatus()
                BindJournalType()
            End If
        End If
    End Sub

    
    Sub BindJournalType()
        ddlJournalType.Items.Add(New ListItem("All", ""))
        ddlJournalType.Items.Add(New ListItem(objAPTrx.mtdGetCreditorJournalType(objAPTrx.EnumCreditorJournalType.Adjustment), objAPTrx.EnumCreditorJournalType.Adjustment))
        ddlJournalType.Items.Add(New ListItem(objAPTrx.mtdGetCreditorJournalType(objAPTrx.EnumCreditorJournalType.Void), objAPTrx.EnumCreditorJournalType.Void))
        ddlJournalType.Items.Add(New ListItem(objAPTrx.mtdGetCreditorJournalType(objAPTrx.EnumCreditorJournalType.WriteOff), objAPTrx.EnumCreditorJournalType.WriteOff))
    End Sub


    Sub BindStatus()
        ddlStatus.Items.Add(New ListItem("All", ""))
        ddlStatus.Items.Add(New ListItem(objAPTrx.mtdGetCreditorJournalStatus(objAPTrx.EnumCreditorJournalStatus.Active), objAPTrx.EnumCreditorJournalStatus.Active))
        ddlStatus.Items.Add(New ListItem(objAPTrx.mtdGetCreditorJournalStatus(objAPTrx.EnumCreditorJournalStatus.Closed), objAPTrx.EnumCreditorJournalStatus.Closed))
        ddlStatus.Items.Add(New ListItem(objAPTrx.mtdGetCreditorJournalStatus(objAPTrx.EnumCreditorJournalStatus.Confirmed), objAPTrx.EnumCreditorJournalStatus.Confirmed))
        ddlStatus.Items.Add(New ListItem(objAPTrx.mtdGetCreditorJournalStatus(objAPTrx.EnumCreditorJournalStatus.Deleted), objAPTrx.EnumCreditorJournalStatus.Deleted))
    End Sub


    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        LocTag.Text = GetCaption(objLangCap.EnumLangCap.Location)
        VehExpenseTag.Text = GetCaption(objLangCap.EnumLangCap.VehExpense)
        VehTag.Text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        AccountTag.Text = GetCaption(objLangCap.EnumLangCap.Account)
        VehTag.Text = GetCaption(objLangCap.EnumLangCap.VehType)

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            BlockTag.Text = GetCaption(objLangCap.EnumLangCap.Block)
        Else
            BlockTag.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
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
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_INVOICE_LIST_CLSLANGCAR_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/AR_StdRpt_Selection.aspx")
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
        Dim strCJIDFrom As String
        Dim strCJIDTo As String
        Dim strSupplier As String
        Dim strJournalType As String
        Dim strStatus As String
        Dim strAccountTag As String
        Dim strBlockTag As String
        Dim strVehTag As String
        Dim strVehExpenseTag As String
        Dim strLocTag As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim strParam As String


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

        strCJIDFrom = Trim(txtCreditJrnIDFrom.Text)
        strCJIDTo = Trim(txtCreditJrnIDTo.Text)
        strSupplier = Trim(txtSupplier.Text)
        strJournalType = Trim(ddlJournalType.SelectedItem.Value)
        strStatus = Trim(ddlStatus.SelectedItem.Value)
        strAccountTag = Trim(AccountTag.Text) & " Code"
        strBlockTag = Trim(BlockTag.Text) & " Code"
        strVehTag = Trim(VehTag.Text) & " Code"
        strVehExpenseTag = Trim(VehExpenseTag.Text) & " Code"
        strLocTag = Trim(LocTag.Text)

        strCJIDFrom = Server.UrlEncode(strCJIDFrom)
        strCJIDTo = Server.UrlEncode(strCJIDTo)
        strSupplier = Server.UrlEncode(strSupplier)
        strJournalType = Server.UrlEncode(strJournalType)
        strStatus = Server.UrlEncode(strStatus)

        strAccountTag = Server.UrlEncode(strAccountTag)
        strBlockTag = Server.UrlEncode(strBlockTag)
        strVehTag = Server.UrlEncode(strVehTag)
        strVehExpenseTag = Server.UrlEncode(strVehExpenseTag)
        strLocTag = Server.UrlEncode(strLocTag)

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


        Response.Write("<Script Language=""JavaScript"">window.open(""AP_StdRpt_CreditorJournalListPreview.aspx?CompName=" & strCompany & _
                        "&Location=" & strUserLoc & _
                        "&RptID=" & strRptID & _
                        "&RptName=" & strRptName & _
                        "&Decimal=" & strDec & _
                        "&DDLAccMth=" & strddlAccMth & _
                        "&DDLAccYr=" & strddlAccYr & _
                        "&CJIDFrom=" & strCJIDFrom & _
                        "&CJIDTo=" & strCJIDTo & _
                        "&SupplierCode=" & strSupplier & _
                        "&JournalType=" & strJournalType & _
                        "&LocTag=" & strLocTag & _
                        "&AccountTag=" & strAccountTag & _
                        "&BlockTag=" & strBlockTag & _
                        "&VehTag=" & strVehTag & _
                        "&VehExpenseTag=" & strVehExpenseTag & _
                        "&Status=" & strStatus & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")


    End Sub

End Class

