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

Public Class GL_StdRpt_TrialBalDaily : Inherits Page

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
    'Protected WithEvents lstAccCode As DropDownList
    'Protected WithEvents lstAccCode2 As DropDownList
    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtAccCode2 As TextBox

    Protected WithEvents lblAccCodeErr As Label
    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents Find2 As HtmlInputButton
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents btnDate As Image
    Protected WithEvents btnDateTo As Image

    Protected WithEvents lblPreBlkTag As Label
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents lblSelect As Label
    Protected WithEvents TxtBlkCode As TextBox
	
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lblChartofAccCode As Label
    Protected WithEvents lblChartofAccCode2 As Label
    Protected WithEvents hidBlockCharge As HtmlInputHidden

    Dim strDateFmt As String
    Dim strAcceptFormat As String


    Protected WithEvents lblCode As Label

    Protected WithEvents cbExcel As CheckBox


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

    Dim PreBlockTag As String
    Dim BlockTag As String
    Dim strBlockTag As String

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
                txtDate.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                txtDateTo.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                onload_GetLangCap()
                BindAccCodeDropList("")
                BindChargeLevelDropDownList()
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

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlockTag = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                strBlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JOURNAL_DETAILS_GET_COSTLEVEL_LANGCAP&errmesg=&redirect=gl/trx/journal_details.aspx")
        End Try

        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTag.Text = PreBlockTag & lblCode.Text & " : "

    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add("Please Select Charge Level")
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.SelectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        ToggleChargeLevel()
    End Sub

    Sub ddlChargeLevel_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ToggleChargeLevel()
    End Sub

    Sub ToggleChargeLevel()
        onload_GetLangCap()
        If ddlChargeLevel.SelectedIndex = 0 Then
            hidBlockCharge.Value = "yes"
            lblPreBlkTag.Text = PreBlockTag & lblCode.Text & " : "
        Else
            hidBlockCharge.Value = ""
            lblPreBlkTag.Text = strBlockTag & " : "
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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
        Dim strSrchBlkCode As String

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim intCnt As Integer

        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim strTemp As String
        Dim strExportToExcel As String


        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.Text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)

        strSrchAccCode = txtAccCode.text.trim() 'lstAccCode.SelectedItem.Value.Trim()
        strSrchBlkCode = TxtBlkCode.Text.Trim()
		
        If txtAccCode2.text.trim() = "" Then
            strSrchAccCode2 = strSrchAccCode
        Else
            strSrchAccCode2 = txtAccCode2.text.trim()
        End If

        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_TrialBalDailyPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&SrchAccCode=" & strSrchAccCode & _
                       "&SrchAccCode2=" & strSrchAccCode2 & _
                       "&DateFrom=" & strDate & _
                       "&DateTo=" & strDateTo & _
                       "&SrchBlkCode=" & strSrchBlkCode & _
                       "&ExportToExcel=" & strExportToExcel & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
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

End Class
