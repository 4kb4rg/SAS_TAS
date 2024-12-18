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

Public Class GL_StdRpt_TrialBalDailyTemp : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents hidAccMonthPX As HtmlInputHidden
    Protected WithEvents hidAccYearPX As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents lstAccCode2 As DropDownList
    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents Find2 As HtmlInputButton
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents btnDate As Image
    Protected WithEvents btnDateTo As Image


    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lblChartofAccCode As Label
    Protected WithEvents lblChartofAccCode2 As Label
    Dim strDateFmt As String
    Dim strAcceptFormat As String


    Protected WithEvents lblCode As Label

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
    Dim tempActGrp As String

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

        onload_GetLangCap()

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                txtDate.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                txtDateTo.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())

                BindAccCodeDropList("")

                BindChargeLevelDropDownList()
                BindPreBlock("", "")
                BindBlockDropList("")
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.visible = False
        htmltr = RptSelect.FindControl("TrCheckLoc")
        htmltr.visible = True
        htmltr = RptSelect.FindControl("TrRadioLoc")
        htmltr.visible = False

        If Page.IsPostBack Then
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNAL_DETAIL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/journal_details.aspx")
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
        Dim strSrchAccCode2 As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strDateTo As String = Date_Validation(txtDateTo.Text, False)
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim intCntActGrp As Integer

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim intCnt As Integer

        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim strTemp As String
        Dim strExportToExcel As String

        Dim strSrchBlkCode As String
        Dim strSrchBlkCodeText As String
        Dim strSrchBlkCodeTo As String
        Dim strSrchBlkCodeToText As String

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)

        strSrchAccCode = lstAccCode.SelectedItem.Value.Trim()

        If lstAccCode2.SelectedItem.Value.Trim() = "" Then
            strSrchAccCode2 = strSrchAccCode
        Else
            strSrchAccCode2 = lstAccCode2.SelectedItem.Value.Trim()
        End If

        If ddlChargeLevel.SelectedIndex = 0 Then
            strSrchBlkCode = ddlPreBlock.SelectedItem.Value.Trim()
            strSrchBlkCodeTo = ddlPreBlockTo.SelectedItem.Value.Trim()
            strSrchBlkCodeText = Trim(ddlChargeLevel.SelectedItem.Text)
        Else
            strSrchBlkCode = lstBlock.SelectedItem.Value.Trim()
            strSrchBlkCodeTo = lstBlockTo.SelectedItem.Value.Trim()
            strSrchBlkCodeText = Trim(ddlChargeLevel.SelectedItem.Text)
        End If

        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")
        
        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_TrialBalDailyTempPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&SrchAccCode=" & strSrchAccCode & _
                       "&SrchAccCode2=" & strSrchAccCode2 & _
                       "&DateFrom=" & strDate & _
                       "&DateTo=" & strDateTo & _
                       "&ChargeLevel=" & ddlChargeLevel.SelectedIndex & _
                       "&SrchBlkCode=" & strSrchBlkCode & _
                       "&SrchBlkCodeTo=" & strSrchBlkCodeTo & _
                       "&SrchBlkCodeText=" & strSrchBlkCodeText & _
                       "&ExportToExcel=" & strExportToExcel & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub


    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")

        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try



        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Select COA"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstAccCode.DataSource = dsForDropDown.Tables(0)
        lstAccCode.DataValueField = "AccCode"
        lstAccCode.DataTextField = "_Description"
        lstAccCode.DataBind()
        lstAccCode.SelectedIndex = intSelectedIndex

        lstAccCode2.DataSource = dsForDropDown.Tables(0)
        lstAccCode2.DataValueField = "AccCode"
        lstAccCode2.DataTextField = "_Description"
        lstAccCode2.DataBind()
        lstAccCode2.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Function blnValidEndStartDate(ByVal pv_strEndDate As String, ByVal pv_strStartDate As String) As Boolean
        blnValidEndStartDate = False
        If CDate(pv_strStartDate) <= CDate(pv_strEndDate) Then
            blnValidEndStartDate = True
        End If
    End Function

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DAILY_GET&errmesg=" & Exp.Message & "&redirect=CB_StdRpt_BankTransaction.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.selectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
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

        If dsForDropDown.Tables(0).Rows.Count = 1 Then
            intSelectedIndex = 1
        End If

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
        Dim strAcc As String = Request.Form("lstAccCode")
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
        ElseIf intAccType = objGLSetup.EnumAccountType.BalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
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
End Class
