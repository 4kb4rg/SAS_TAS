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

Public Class NU_StdRpt_SeedlingsIssueList : Inherits Page

    Protected RptSelect As UserControl

    Dim objNUReport As New agri.NU.clsReport()
    Dim objNUSetup As New agri.NU.clsSetup()
    Dim objNUTrx As New agri.NU.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents txtIssueIDFrom As TextBox
    Protected WithEvents txtIssueIDTo As TextBox
    Protected WithEvents txtDocRefNo As TextBox
    Protected WithEvents txtIssueDateFrom As TextBox
    Protected WithEvents txtIssueDateTo As TextBox
    Protected WithEvents txtNUBlkCode As TextBox
    Protected WithEvents txtBatchNo As TextBox
    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtVehType As TextBox
    Protected WithEvents txtVehCode As TextBox
    Protected WithEvents txtVehExpCode As TextBox
    Protected WithEvents txtBlkGrp As TextBox
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtSubBlkCode As TextBox

    Protected WithEvents ddlBlkType As DropDownList
    Protected WithEvents ddlStatus As DropDownList

    Protected WithEvents lblNUBlkCode As Label
    Protected WithEvents lblBatchNo As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblVehType As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblBlkType As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label

    Protected WithEvents trBlkGrp As HtmlTableRow
    Protected WithEvents trBlk As HtmlTableRow
    Protected WithEvents trSubBlk As HtmlTableRow

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrAccMonth As Label
    Protected WithEvents lblErrAccYear As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCode As Label
    
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
                GetLangCap()
                BindBlockTypeDropDownList()
                BindStatusDropDownList()
            End If

            SetBlockAccessibilityByBlockType()
        End If
    End Sub
    
     Sub GetLangCap()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_STDRPT_SEEDLINGS_ISSUE_LIST_LANGCAP&errmesg=" & Server.UrlEncode(lblErrMessage.Text) & "&redirect=")
        End Try
        
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblNUBlkCode.Text = GetCaption(objLangCap.EnumLangCap.NurseryBlock) & lblCode.Text
        Else
            lblNUBlkCode.Text = GetCaption(objLangCap.EnumLangCap.NurserySubBlock) & lblCode.Text
        End If
        
        lblBatchNo.Text = GetCaption(objLangCap.EnumLangCap.BatchNo)
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & " Code"
        lblVehType.Text = GetCaption(objLangCap.EnumLangCap.VehType) & " Code"
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code"
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code"
        lblBlkType.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Type"
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp)
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code"
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code"
    End Sub

    Function GetCaption(ByVal pv_TermCode As String) As String
        Dim I As Integer

        For I = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(I).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(I).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(I).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function
    
    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow
        Dim htmltc As HtmlTableCell

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.Visible = True

        htmltc = RptSelect.FindControl("ddlAccMthYrTo")
        htmltc.Visible = True
    End Sub
    
    Sub SetBlockAccessibilityByBlockType()
        If ddlBlkType.SelectedItem.Value = "BlkCode" Then
            trBlkGrp.Visible = False
            trBlk.Visible = True
            trSubBlk.Visible = False
        ElseIf ddlBlkType.SelectedItem.Value = "BlkGrp" Then
            trBlkGrp.Visible = True
            trBlk.Visible = False
            trSubBlk.Visible = False
        ElseIf ddlBlkType.SelectedItem.Value = "SubBlkCode" Then
            trBlkGrp.Visible = False
            trBlk.Visible = False
            trSubBlk.Visible = True
        End If
    End Sub
    
    Sub BindBlockTypeDropDownList()
        ddlBlkType.Items.Clear()
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            ddlBlkType.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.BlockGrp), "BlkGrp"))
            ddlBlkType.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.Block) & " Code", "BlkCode"))
        Else
            ddlBlkType.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.Block) & " Code", "BlkCode"))
            ddlBlkType.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code", "SubBlkCode"))
        End If
        ddlBlkType.SelectedIndex = 1
    End Sub
    
    Sub BindStatusDropDownList()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedlingsIssueStatus(objNUTrx.EnumSeedlingsIssueStatus.All), objNUTrx.EnumSeedlingsIssueStatus.All))
        ddlStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedlingsIssueStatus(objNUTrx.EnumSeedlingsIssueStatus.Active), objNUTrx.EnumSeedlingsIssueStatus.Active))
        ddlStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedlingsIssueStatus(objNUTrx.EnumSeedlingsIssueStatus.Deleted), objNUTrx.EnumSeedlingsIssueStatus.Deleted))
        ddlStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedlingsIssueStatus(objNUTrx.EnumSeedlingsIssueStatus.Confirmed), objNUTrx.EnumSeedlingsIssueStatus.Confirmed))
        ddlStatus.Items.Add(New ListItem(objNUTrx.mtdGetSeedlingsIssueStatus(objNUTrx.EnumSeedlingsIssueStatus.Closed), objNUTrx.EnumSeedlingsIssueStatus.Closed))
        ddlStatus.SelectedIndex = 3
    End Sub
    
    Sub ddlBlkType_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SetBlockAccessibilityByBlockType
    End Sub
    
    Sub ibPrintPreview_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strIssueIDFrom As String
        Dim strIssueIDTo As String
        Dim strDocRefNo As String        
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strNUBlockCode As String
        Dim strBatchNo As String
        Dim strAccCode As String
        Dim strVehTypeCode As String
        Dim strVehCode As String
        Dim strVehExpCode As String
        Dim strBlkType As String
        Dim strBlkGrpCode As String
        Dim strBlkCode As String
        Dim strSubBlkCode As String
        Dim strStatus As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strddlAccMthTo As String
        Dim strddlAccYrTo As String
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
        
        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=NU_STDRPT_SEEDLINGS_ISSUE_LIST_GET_CONFIG_DATE&errmesg=" & Server.UrlEncode(lblErrMessage.Text) & "&redirect=")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))
        strDateFrom = txtIssueDateFrom.Text.Trim()
        strDateTo = txtIssueDateTo.Text.Trim()

        If Not (strDateFrom = "" And strDateTo = "") Then
            If Not (objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, strDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, strDateTo) = True) Then
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
                Exit Sub
            End If
        End If
        
        strIssueIDFrom =  Server.UrlEncode(txtIssueIDFrom.Text.Trim())
        strIssueIDTo =  Server.UrlEncode(txtIssueIDTo.Text.Trim())
        strDocRefNo =  Server.UrlEncode(txtDocRefNo.Text.Trim())
        strDateFrom = Server.UrlEncode(strDateFrom)
        strDateTo = Server.UrlEncode(strDateTo)
        strNUBlockCode =  Server.UrlEncode(txtNUBlkCode.Text.Trim())
        strBatchNo =  Server.UrlEncode(txtBatchNo.Text.Trim())
        strAccCode =  Server.UrlEncode(txtAccCode.Text.Trim())
        strVehTypeCode =  Server.UrlEncode(txtVehType.Text.Trim())
        strVehCode =  Server.UrlEncode(txtVehCode.Text.Trim())
        strVehExpCode =  Server.UrlEncode(txtVehExpCode.Text.Trim())
        strBlkType =  Server.UrlEncode(ddlBlkType.SelectedItem.Value.Trim())
        strBlkGrpCode =  Server.UrlEncode(txtBlkGrp.Text.Trim())
        strBlkCode =  Server.UrlEncode(txtBlkCode.Text.Trim())
        strSubBlkCode =  Server.UrlEncode(txtSubBlkCode.Text.Trim())
        strStatus =  Server.UrlEncode(ddlStatus.SelectedItem.Value.Trim())
        
        Response.Write("<Script Language=""JavaScript"">window.open(""NU_StdRpt_SeedlingsIssueListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                        "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&DDLAccMthTo=" & strddlAccMthTo & "&DDLAccYrTo=" & strddlAccYrTo & _
                        "&IssueIDFrom=" & strIssueIDFrom & "&IssueIDTo=" & strIssueIDTo & "&DocRefNo=" & strDocRefNo & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&NUBlockCode=" & strNUBlockCode & "&BatchNo=" & strBatchNo & _
                        "&AccCode=" & strAccCode & "&VehTypeCode=" & strVehTypeCode & "&VehCode=" & strVehCode & "&VehExpCode=" & strVehExpCode & "&BlkType=" & strBlkType & "&BlkGrp=" & strBlkGrpCode & "&BlkCode=" & strBlkCode & "&SubBlkCode=" & strSubBlkCode & "&Status=" & strStatus & _
                        "&lblAccCode=" & lblAccCode.Text & "&lblVehTypeCode=" & lblVehType.Text & "&lblVehCode=" & lblVehCode.Text & "&lblVehExpCode=" & lblVehExpCode.Text & "&lblBlkType=" & lblBlkType.Text & "&lblBlkGrp=" & lblBlkGrp.Text & _
                        "&lblBlkCode=" & lblBlkCode.Text & "&lblSubBlkCode=" & lblSubBlkCode.Text & "&lblLocation=" & lblLocation.Text & "&lblNUBlockCode=" & lblNUBlkCode.Text & "&lblBatchNo=" & lblBatchNo.Text & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
