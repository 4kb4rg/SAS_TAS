Imports System
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

Public Class NU_StdRpt_SeedDispatchList : Inherits Page

    Protected RptSelect As UserControl

    Dim objNUReport As New agri.NU.clsReport()
    Dim objNUSetup As New agri.NU.clsSetup()
    Dim objNUTrx As New agri.NU.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrAccMonth As Label
    Protected WithEvents lblErrAccYear As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblBatchNo As Label
    Protected WithEvents lblBillPartyCode As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents txtTxIDFrom As TextBox
    Protected WithEvents txtTxIDTo As TextBox
    Protected WithEvents txtDocRefNoFrom As TextBox
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtBatchNo As TextBox
    Protected WithEvents txtBillPartyCode As TextBox
    Protected WithEvents txtTxDateFrom As TextBox
    Protected WithEvents txtTxDateTo As TextBox
    Protected WithEvents lstStatus As DropDownList

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

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
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_NUACCMONTH")
        strAccYear = Session("SS_NUACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")


        lblDate.Visible = False
        lblDateFormat.Visible = False
        lblErrAccMonth.Visible = False
        lblErrAccYear.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatus()
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow
        Dim htmltc As HtmlTableCell

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.Visible = True

        htmltc = RptSelect.FindControl("ddlAccMthYrTo")
        htmltc.Visible = True
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.NurseryBlock) & " Code"
        Else
            lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.NurserySubBlock) & " Code"
        End If
        lblBatchNo.Text = GetCaption(objLangCap.EnumLangCap.BatchNo)
        lblBillPartyCode.Text = GetCaption(objLangCap.EnumLangCap.BillParty)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/NU_StdRpt_Selection.aspx")
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


    Sub BindStatus()
        lstStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedDispatchStatus(objNUTrx.EnumSeedDispatchStatus.All), objNUTrx.EnumSeedDispatchStatus.All))
        lstStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedDispatchStatus(objNUTrx.EnumSeedDispatchStatus.Confirmed), objNUTrx.EnumSeedDispatchStatus.Confirmed))
        lstStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedDispatchStatus(objNUTrx.EnumSeedDispatchStatus.Deleted), objNUTrx.EnumSeedDispatchStatus.Deleted))
        lstStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedDispatchStatus(objNUTrx.EnumSeedDispatchStatus.Closed), objNUTrx.EnumSeedDispatchStatus.Closed))

        lstStatus.SelectedIndex = 1
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strTxIDFrom As String
        Dim strTxIDTo As String
        Dim strDocRefNoFrom As String
        Dim strStatus As String
        Dim strBlkCode As String
        Dim strTxDateFrom As String
        Dim strTxDateTo As String
        Dim strBatchNo As String
        Dim strBillPartyCode As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strddlAccMthTo As String
        Dim strddlAccYrTo As String
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim intddlAccMth As Integer
        Dim intddlAccYr As Integer
        Dim intddlAccMthTo As Integer
        Dim intddlAccYrTo As Integer

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempAccMthTo As DropDownList
        Dim tempAccYrTo As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)

        tempAccMthTo = RptSelect.FindControl("lstAccMonthTo")
        strddlAccMthTo = Trim(tempAccMthTo.SelectedItem.Value)
        tempAccYrTo = RptSelect.FindControl("lstAccYearTo")
        strddlAccYrTo = Trim(tempAccYrTo.SelectedItem.Value)

        intddlAccMth = CInt(strddlAccMth)
        intddlAccMthTo = CInt(strddlAccMthTo)
        intddlAccYr = CInt(strddlAccYr)
        intddlAccYrTo = CInt(strddlAccYrTo)

        If intddlAccYr > intddlAccYrTo Then
            lblErrAccYear.Visible = True
            Exit Sub
        ElseIf intddlAccYr = intddlAccYrTo Then
            If intddlAccMth > intddlAccMthTo Then
                lblErrAccMonth.Visible = True
                Exit Sub
            End If
        End If

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

        If txtTxIDFrom.Text = "" Then
            strTxIDFrom = ""
        Else
            strTxIDFrom = Trim(txtTxIDFrom.Text)
        End If

        If txtTxIDTo.Text = "" Then
            strTxIDTo = ""
        Else
            strTxIDTo = Trim(txtTxIDTo.Text)
        End If

        If txtDocRefNoFrom.Text = "" Then
            strDocRefNoFrom = ""
        Else
            strDocRefNoFrom = Trim(txtDocRefNoFrom.Text)
        End If

        If txtBlkCode.Text = "" Then
            strBlkCode = ""
        Else
            strBlkCode = Trim(txtBlkCode.Text)
        End If

        If txtBatchNo.Text = "" Then
            strBatchNo = ""
        Else
            strBatchNo = Trim(txtBatchNo.Text)
        End If

        If txtBillPartyCode.Text = "" Then
            strBillPartyCode = ""
        Else
            strBillPartyCode = Trim(txtBillPartyCode.Text)
        End If

        If txtTxDateFrom.Text = "" Then
            strTxDateFrom = ""
        Else
            strTxDateFrom = Trim(txtTxDateFrom.Text)
        End If

        If txtTxDateTo.Text = "" Then
            strTxDateTo = ""
        Else
            strTxDateTo = Trim(txtTxDateTo.Text)
        End If

        strStatus = Trim(lstStatus.SelectedItem.Value)

        strDocRefNoFrom = Server.UrlEncode(strDocRefNoFrom)
        strBlkCode = Server.UrlEncode(strBlkCode)
        strBatchNo = Server.UrlEncode(strBatchNo)
        strBillPartyCode = Server.UrlEncode(strBillPartyCode)

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=NU_STDRPT_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strTxDateFrom = "" And strTxDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strTxDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strTxDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""NU_StdRpt_SeedDispatchListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & _
                               "&RptName=" & strRptName & "&Decimal=" & strDec & "&TxDateFrom=" & objDateFrom & "&TxDateTo=" & objDateTo & _
                               "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&DDLAccMthTo=" & strddlAccMthTo & "&DDLAccYrTo=" & strddlAccYrTo & _
                               "&lblLocation=" & lblLocation.Text & "&lblBlkCode=" & lblBlkCode.Text & "&lblBatchNo=" & lblBatchNo.Text & "&lblBillPartyCode=" & lblBillPartyCode.Text & _
                               "&TxIDFrom=" & strTxIDFrom & "&TxIDTo=" & strTxIDTo & "&DocRefNoFrom=" & strDocRefNoFrom & "&BillPartyCode=" & strBillPartyCode & _
                               "&Status=" & strStatus & "&BlkCode=" & strBlkCode & "&BatchNo=" & strBatchNo & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""NU_StdRpt_SeedDispatchListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & _
                           "&RptName=" & strRptName & "&Decimal=" & strDec & "&TxDateFrom=" & objDateFrom & "&TxDateTo=" & objDateTo & _
                           "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&DDLAccMthTo=" & strddlAccMthTo & "&DDLAccYrTo=" & strddlAccYrTo & _
                           "&lblLocation=" & lblLocation.Text & "&lblBlkCode=" & lblBlkCode.Text & "&lblBatchNo=" & lblBatchNo.Text & "&lblBillPartyCode=" & lblBillPartyCode.Text & _
                           "&TxIDFrom=" & strTxIDFrom & "&TxIDTo=" & strTxIDTo & "&DocRefNoFrom=" & strDocRefNoFrom & "&BillPartyCode=" & strBillPartyCode & _
                           "&Status=" & strStatus & "&BlkCode=" & strBlkCode & "&BatchNo=" & strBatchNo & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

End Class
