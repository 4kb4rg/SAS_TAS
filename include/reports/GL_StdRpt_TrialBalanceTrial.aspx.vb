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

Public Class GL_StdRpt_TrialBalanceTrial : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()

    Protected WithEvents hidAccMonthPX As HtmlInputHidden
    Protected WithEvents hidAccYearPX As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents cblAccType As CheckBoxList
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lblChartofAccCode As Label
    Protected WithEvents lblChartofAccCode2 As Label

    Protected WithEvents lblChartofAccType As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblType As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblAccDesc As Label
    'Protected WithEvents txtSrchAccCodeTo As TextBox
    'Protected WithEvents txtSrchAccCode As TextBox
    'Protected WithEvents lstAccCode As DropDownList
    'Protected WithEvents lstAccCode2 As DropDownList
    Protected WithEvents txtAccCode As textbox
    Protected WithEvents txtAccCode2 As textbox

    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents Find2 As HtmlInputButton

    Protected WithEvents cbExcel As CheckBox

    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlk As HtmlTableRow
    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents RowPreBlkTo As HtmlTableRow
    Protected WithEvents RowBlkTo As HtmlTableRow
    Protected WithEvents lblPreBlockErr As Label
    Protected WithEvents lblPreBlkTag As Label
    Protected WithEvents lblBlkTag As Label
    Protected WithEvents lstBlock As DropDownList
    Protected WithEvents ddlPreBlock As DropDownList
    Protected WithEvents lblPreBlockErrTo As Label
    Protected WithEvents lblPreBlkTagTo As Label
    Protected WithEvents lblBlkTagTo As Label
    Protected WithEvents lstBlockTo As DropDownList
    Protected WithEvents ddlPreBlockTo As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden

    Protected WithEvents ddlSrchAccMonthFrom As DropDownList
    Protected WithEvents ddlSrchAccMonthTo As DropDownList
    Protected WithEvents ddlSrchAccYear As DropDownList


    Dim PreBlockTag As String
    Dim BlockTag As String
    Dim strBlockTag As String
    Dim PreBlockTagTo As String
    Dim BlockTagTo As String
    Dim strBlockTagTo As String
    Protected WithEvents RowChargeTo As HtmlTableRow

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
    Dim strLocType As String


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                cblAccType.Items(0).Selected = True
                cblAccType.Items(1).Selected = True
                BindAccCodeDropList("")
                BindChargeLevelDropDownList()
                BindPreBlock("", "")
                BindBlockDropList("")
                BindAccMonthList(BindAccYearList(strLocation, strAccYear))
                BindAccMonthToList(BindAccYearList(strLocation, strAccYear))
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.Visible = False
        htmltr = RptSelect.FindControl("TrCheckLoc")
        htmltr.Visible = True
        htmltr = RptSelect.FindControl("TrRadioLoc")
        htmltr.Visible = False

        If Page.IsPostBack Then
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblChartofAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.text

        lblChartofAccCode2.Text = "To " & GetCaption(objLangCap.EnumLangCap.Account) & lblCode.text

        lblChartofAccType.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblType.text
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblAccDesc.Text = GetCaption(objLangCap.EnumLangCap.AccDesc)


        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlockTag = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)

                strBlockTagTo = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                BlockTagTo = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                strBlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)

                strBlockTagTo = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                BlockTagTo = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNAL_DETAILS_GET_COSTLEVEL_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/journal_details.aspx")
        End Try


        lblBlkTag.Text = strBlockTag & " : "
        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTag.Text = PreBlockTag & lblCode.Text & " : "
        lblPreBlockErr.Text = "Select " & PreBlockTag & lblCode.Text

        lblBlkTagTo.Text = "To " & strBlockTagTo & " : "
        PreBlockTagTo = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTagTo.Text = "To " & PreBlockTagTo & lblCode.Text & " : "
        lblPreBlockErrTo.Text = "Select " & PreBlockTagTo & lblCode.Text
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
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strSrchAccCode As String
        Dim strSrchAccCodeTo As String
        Dim strSupp As String
        Dim strAccType As String
        Dim strAccTypeText As String
        Dim strParam As String

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim intCnt As Integer

        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim strTemp As String

        Dim LocTag As String
        Dim AccCodeTag As String
        Dim AccDescTag As String
        Dim AccTypeTag As String
        Dim strExportToExcel As String

        Dim strSrchBlkCode As String
        Dim strSrchBlkCodeText As String
        Dim strSrchBlkCodeTo As String
        Dim strSrchBlkCodeToText As String

        Dim strSrchAccMonthFrom As String
        Dim strSrchAccMonthTo As String
        Dim strSrchAccYear As String

        ddlist = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
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

        If Right(strUserLoc, 1) = "," Then
            strUserLoc = Left(strUserLoc, Len(strUserLoc) - 1)
        Else
            strUserLoc = Trim(strUserLoc)
        End If


        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If


        strSrchAccCode = txtAccCode.text.trim 'lstAccCode.SelectedItem.Value.Trim()
        strSrchAccCodeTo = txtAccCode2.text.trim 'lstAccCode2.SelectedItem.Value.Trim()

        'strSrchAccCode = Trim(txtSrchAccCode.Text)
        'strSrchAccCodeTo = Trim(txtSrchAccCodeTo.Text)

        For intCnt = 0 To cblAccType.Items.Count - 1
            If cblAccType.Items(intCnt).Selected Then
                If cblAccType.Items.Count = 1 Then
                    strAccType = cblAccType.Items(intCnt).Value
                    strAccTypeText = cblAccType.Items(intCnt).Text
                Else
                    strAccType = strAccType & "','" & cblAccType.Items(intCnt).Value
                    strAccTypeText = strAccTypeText & ", " & cblAccType.Items(intCnt).Text
                End If
            End If
        Next
        If intCnt <> 1 Then
            strAccType = Right(strAccType, Len(strAccType) - 3)
            strAccTypeText = Right(strAccTypeText, Len(strAccTypeText) - 2)
        End If

        LocTag = lblLocation.text
        AccCodeTag = lblChartofAccCode.text
        AccDescTag = lblAccDesc.text
        AccTypeTag = lblChartofAccType.text

        If ddlChargeLevel.SelectedIndex = 0 Then
            strSrchBlkCode = ddlPreBlock.SelectedItem.Value.Trim()
            strSrchBlkCodeTo = ddlPreBlockTo.SelectedItem.Value.Trim()
            strSrchBlkCodeText = Trim(ddlChargeLevel.SelectedItem.Text)
        Else
            strSrchBlkCode = lstBlock.SelectedItem.Value.Trim()
            strSrchBlkCodeTo = lstBlockTo.SelectedItem.Value.Trim()
            strSrchBlkCodeText = Trim(ddlChargeLevel.SelectedItem.Text)
        End If

        strSrchAccMonthFrom = Server.UrlEncode(Trim(ddlSrchAccMonthFrom.SelectedItem.value))
        strSrchAccMonthTo = Server.UrlEncode(Trim(ddlSrchAccMonthTo.SelectedItem.value))
        strSrchAccYear = Server.UrlEncode(Trim(ddlSrchAccYear.SelectedItem.value))

        strExportToExcel = IIF(cbExcel.Checked = True, "1", "0")


        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_TrialBalanceTrialPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & "&sum=no" & _
                       "&SrchAccCode=" & strSrchAccCode & _
                       "&SrchAccCodeTo=" & strSrchAccCodeTo & _
                       "&AccType=" & strAccType & _
                       "&AccTypeText=" & strAccTypeText & _
                       "&LocTag=" & LocTag & _
                       "&AccCodeTag=" & AccCodeTag & _
                       "&AccDescTag=" & AccDescTag & _
                       "&AccTypeTag=" & AccTypeTag & _
                       "&ChargeLevel=" & ddlChargeLevel.SelectedIndex & _
                       "&SrchBlkCode=" & strSrchBlkCode & _
                       "&SrchBlkCodeTo=" & strSrchBlkCodeTo & _
                       "&SrchBlkCodeText=" & strSrchBlkCodeText & _
                       "&SrchAccMonthFrom=" & strSrchAccMonthFrom & _
                       "&SrchAccMonthTo=" & strSrchAccMonthTo & _
                       "&SrchAccYear=" & strSrchAccYear & _
                       "&ExportToExcel=" & strExportToExcel & _
                       """,""" & strRptId & """ ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")

        'Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        'Dim dr As DataRow
        'Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        'Dim intErrNo As Integer
        'Dim intCnt As Integer
        'Dim intSelectedIndex As Integer = 0
        'Dim dsForDropDown As DataSet

        'strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        'Try
        '    intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
        '                                           strParam, _
        '                                           objGLSetup.EnumGLMasterType.AccountCode, _
        '                                           dsForDropDown)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        'End Try



        'dr = dsForDropDown.Tables(0).NewRow()
        'dr("AccCode") = ""
        'dr("_Description") = "Select COA"
        'dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        'lstAccCode.DataSource = dsForDropDown.Tables(0)
        'lstAccCode.DataValueField = "AccCode"
        'lstAccCode.DataTextField = "_Description"
        'lstAccCode.DataBind()
        'lstAccCode.SelectedIndex = intSelectedIndex

        'lstAccCode2.DataSource = dsForDropDown.Tables(0)
        'lstAccCode2.DataValueField = "AccCode"
        'lstAccCode2.DataTextField = "_Description"
        'lstAccCode2.DataBind()
        'lstAccCode2.SelectedIndex = intSelectedIndex

        'If Not dsForDropDown Is Nothing Then
        '    dsForDropDown = Nothing
        'End If
    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add("Please Select Charga Level")
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.SelectedIndex = 0 'Session("SS_BLOCK_CHARGE_DEFAULT")
        RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub

    Sub ddlChargeLevel_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ToggleChargeLevel()
    End Sub

    Sub ToggleChargeLevel()
        If ddlChargeLevel.selectedIndex = 0 Then
            RowBlk.Visible = False
            RowPreBlk.Visible = True
            RowBlkTo.Visible = False
            RowPreBlkTo.Visible = True
            hidBlockCharge.value = "yes"
        Else
            RowBlk.Visible = True
            RowPreBlk.Visible = False
            RowBlkTo.Visible = True
            RowPreBlkTo.Visible = False
            hidBlockCharge.value = ""
        End If
        CheckVehicleUse()
    End Sub

    Sub BindBlockDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim strParam As String
        Dim dr As DataRow

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & strLocation & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & strLocation & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCdBlockList_Get, _
                                                     strParam, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

      '  If dsForDropDown.Tables(0).Rows.Count = 1 Then
       '     intSelectedIndex = 1
       ' End If

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Select " & strBlockTag
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)
        lstBlock.DataSource = dsForDropDown.Tables(0)
        lstBlock.DataValueField = "BlkCode"
        lstBlock.DataTextField = "Description"
        lstBlock.DataBind()
        lstBlock.SelectedIndex = intSelectedIndex

        lstBlockTo.DataSource = dsForDropDown.Tables(0)
        lstBlockTo.DataValueField = "BlkCode"
        lstBlockTo.DataTextField = "Description"
        lstBlockTo.DataBind()
        lstBlockTo.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindPreBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim objBlkDs As DataSet
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        strOpCd = "GL_CLSSETUP_ACCOUNT_PREBLOCK_GET"
        intSelectedIndex = 0
        Try
            strParam = pv_strAccCode & "|" & strLocation & "|" & objGLSetup.EnumBlockStatus.Active
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        If objBlkDs.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Select " & PreBlockTag & lblCode.Text

        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlPreBlock.DataSource = objBlkDs.Tables(0)
        ddlPreBlock.DataValueField = "BlkCode"
        ddlPreBlock.DataTextField = "Description"
        ddlPreBlock.DataBind()
        ddlPreBlock.SelectedIndex = intSelectedIndex

        ddlPreBlockTo.DataSource = objBlkDs.Tables(0)
        ddlPreBlockTo.DataValueField = "BlkCode"
        ddlPreBlockTo.DataTextField = "Description"
        ddlPreBlockTo.DataBind()
        ddlPreBlockTo.SelectedIndex = intSelectedIndex

        If Not objBlkDs Is Nothing Then
            objBlkDs = Nothing
        End If
    End Sub

    Sub CallCheckVehicleUse(ByVal sender As Object, ByVal e As System.EventArgs)
        CheckVehicleUse()
    End Sub

    Sub CheckVehicleUse()
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim intNurseryInd As Integer
        Dim blnFound As Boolean
        Dim strAcc As String = Request.Form("txtAccCode")
        Dim strBlk As String = Request.Form("lstBlock")
        Dim strPreBlk As String = Request.Form("ddlPreBlock")

        GetAccountDetails(strAcc, intAccType, intAccPurpose, intNurseryInd, blnFound)

        If intAccType = objGLSetup.EnumAccountType.ProfitAndLost Then
            Select Case intAccPurpose
                Case objGLSetup.EnumAccountPurpose.NonVehicle
                    BindPreBlock(strAcc, strPreBlk)
                    BindBlockDropList(strAcc, strBlk)
                Case objGLSetup.EnumAccountPurpose.VehicleDistribution
                    BindPreBlock("", "")
                    BindBlockDropList("")
                Case Else
                    BindPreBlock(strAcc, strPreBlk)
                    BindBlockDropList(strAcc, strBlk)
            End Select
        ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet Or intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
            BindPreBlock(strAcc, strPreBlk)
            BindBlockDropList(strAcc, strBlk)
        Else
            BindPreBlock("", "")
            BindBlockDropList("", "")
        End If
    End Sub

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer, _
                          ByRef pr_strNurseryInd As Integer, _
                          ByRef pr_blnFound As Boolean)

        Dim _objAccDs As New DataSet()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
            pr_blnFound = True
        Else
            pr_blnFound = False
            pr_strAccType = 0
            pr_strAccPurpose = 0
            pr_strNurseryInd = 0
        End If
    End Sub

    Sub BindAccMonthList(ByVal pv_intMaxMonth As Integer)
        Dim intCnt As Integer
        Dim intSelIndex As Integer = 0

        ddlSrchAccMonthFrom.Items.Clear()
        For intCnt = 1 To pv_intMaxMonth
            ddlSrchAccMonthFrom.Items.Add(intCnt)
            If intCnt = Convert.ToInt16(strAccMonth) Then
                intSelIndex = intCnt - 1
            End If
        Next
        ddlSrchAccMonthFrom.SelectedIndex = intSelIndex
    End Sub

    Function BindAccYearList(ByVal pv_strLocation As String, _
                             ByVal pv_strAccYear As String) As Integer
        Dim strOpCd_Max_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ALLLOC_MAXPERIOD_GET"
        Dim strOpCd_Dist_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_DISTINCT_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intAccYear As Integer
        Dim intMaxPeriod As Integer
        Dim intCnt As Integer
        Dim intSelIndex As Integer
        Dim objAccCfg As New Dataset()

        If pv_strLocation = "" Then
            pv_strLocation = strLocation
        Else
            If Left(pv_strLocation, 3) = "','" Then
                pv_strLocation = Right(pv_strLocation, Len(pv_strLocation) - 3)
            ElseIf Right(pv_strLocation, 3) = "','" Then
                pv_strLocation = Left(pv_strLocation, Len(pv_strLocation) - 3)
            ElseIf Left(pv_strLocation, 1) = "," Then
                pv_strLocation = Right(pv_strLocation, Len(pv_strLocation) - 1)
            ElseIf Right(pv_strLocation, 1) = "," Then
                pv_strLocation = Left(pv_strLocation, Len(pv_strLocation) - 1)
            End If
        End If

        Try
            strParam = "||"
            intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_Dist_Get, _
                                                    strCompany, _
                                                    pv_strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objAccCfg)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CT_STDRPT_CTRL_ACCCFG_DIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        intSelIndex = 0
        ddlSrchAccYear.Items.Clear()


        If objAccCfg.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objAccCfg.Tables(0).Rows.Count - 1
                ddlSrchAccYear.Items.Add(objAccCfg.Tables(0).Rows(intCnt).Item("AccYear"))
                If objAccCfg.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                    intSelIndex = intCnt
                End If
            Next

            ddlSrchAccYear.SelectedIndex = intSelIndex
            intAccYear = ddlSrchAccYear.SelectedItem.Value

            Try
                strParam = "||" & intAccYear
                intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_Max_Get, _
                                                        strCompany, _
                                                        pv_strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        objAccCfg)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CT_STDRPT_CTRL_ACCCFG_MAX_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            Try
                intMaxPeriod = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CT_STDRPT_CTLR_ACCCFG_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & Convert.ToString(intAccYear) & "&redirect=")
            End Try

        Else
            ddlSrchAccYear.Items.Add(strAccYear)
            ddlSrchAccYear.SelectedIndex = intSelIndex
            intMaxPeriod = Convert.ToInt16(strAccMonth)
        End If

        objAccCfg = Nothing
        Return intMaxPeriod
    End Function


    Sub BindAccMonthToList(ByVal pv_intMaxMonth As Integer)
        Dim intCnt As Integer
        Dim intSelIndex As Integer = 0

        ddlSrchAccMonthTo.Items.Clear()
        For intCnt = 1 To pv_intMaxMonth
            ddlSrchAccMonthTo.Items.Add(intCnt)
            If intCnt = Convert.ToInt16(strAccMonth) Then
                intSelIndex = intCnt - 1
            End If
        Next
        ddlSrchAccMonthTo.SelectedIndex = intSelIndex
    End Sub
End Class
