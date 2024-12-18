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

Public Class CB_StdRpt_CashTransferList : Inherits Page

    Protected RptSelect As UserControl

    Dim objCB As New agri.CB.clsReport()
    Dim objCBTrx As New agri.CB.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblVehType As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblBlkType As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents txtCashBankIDFrom As TextBox
    Protected WithEvents txtCashBankIDTo As TextBox
    Protected WithEvents txtRefNo As TextBox
    Protected WithEvents lstCashBankType As DropDownList
    Protected WithEvents txtUpdatedBy As TextBox
    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtVehType As TextBox
    Protected WithEvents txtVehCode As TextBox
    Protected WithEvents txtVehExpCode As TextBox
    Protected WithEvents lstBlkType As DropDownList
    Protected WithEvents txtBlkGrp As TextBox
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtSubBlkCode As TextBox
    Protected WithEvents lstStatus As DropDownList

    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow

    Protected WithEvents txtDateFrom As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents lblErrDate As Label

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
    Dim strDateFmt As String
    Dim strAcceptFormat As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String


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
        strDateFmt = Session("SS_DATEFMT")


        lblDate.visible = False
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            lblErrDate.Visible = False

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindTransType()
                BlkTypeList()
                BindStatus()

                txtDateTo.Text = objGlobal.GetShortDate(strDateFmt, Now())
                txtDateFrom.Text = objGlobal.GetShortDate(strDateFmt, DateAdd("d", -7, Now()))

            End If

            If lstBlkType.SelectedItem.Value = "BlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "BlkGrp" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "SubBlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = True
            End If

        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim UCTrMthYr As HtmlTableRow

        UCTrMthYr = RptSelect.FindControl("TrMthYr")
        UCTrMthYr.Visible = True
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & " Code :"
        lblVehType.Text = GetCaption(objLangCap.EnumLangCap.VehType) & " Code :"
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code :"
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code :"
        lblBlkType.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Type :"
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & " Code :"
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code :"
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code :"
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

    Sub BlkTypeList()

        Dim strBlkGrp As String
        Dim strBlk As String
        Dim strSubBlk As String

        strBlkGrp = Left(lblBlkGrp.Text, Len(lblBlkGrp.Text) - 2)
        strBlk = Left(lblBlkCode.Text, Len(lblBlkCode.Text) - 2)
        strSubBlk = Left(lblSubBlkCode.Text, Len(lblSubBlkCode.Text) - 2)

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lstBlkType.Items.Add(New ListItem(strBlk, "BlkCode"))
            lstBlkType.Items.Add(New ListItem(strBlkGrp, "BlkGrp"))
        Else
            lstBlkType.Items.Add(New ListItem(strSubBlk, "SubBlkCode"))
            lstBlkType.Items.Add(New ListItem(strBlk, "BlkCode"))
        End If

    End Sub

    Sub BindTransType()
        lstCashBankType.Items.Add(New ListItem(objCBTrx.mtdGetCashBankType(objCBTrx.EnumCashBankType.All), objCBTrx.EnumCashBankType.All))
        lstCashBankType.Items.Add(New ListItem(objCBTrx.mtdGetCashBankType(objCBTrx.EnumCashBankType.Payment), objCBTrx.EnumCashBankType.Payment))
        lstCashBankType.Items.Add(New ListItem(objCBTrx.mtdGetCashBankType(objCBTrx.EnumCashBankType.Receipt), objCBTrx.EnumCashBankType.Receipt))
    End Sub

    Sub BindStatus()
        lstStatus.Items.Add(New ListItem(objCBTrx.mtdGetCashBankStatus(objCBTrx.EnumCashBankStatus.All), objCBTrx.EnumCashBankStatus.All))
        lstStatus.Items.Add(New ListItem(objCBTrx.mtdGetCashBankStatus(objCBTrx.EnumCashBankStatus.Active), objCBTrx.EnumCashBankStatus.Active))
        lstStatus.Items.Add(New ListItem(objCBTrx.mtdGetCashBankStatus(objCBTrx.EnumCashBankStatus.Confirmed), objCBTrx.EnumCashBankStatus.Confirmed))
        lstStatus.Items.Add(New ListItem(objCBTrx.mtdGetCashBankStatus(objCBTrx.EnumCashBankStatus.Deleted), objCBTrx.EnumCashBankStatus.Deleted))
        lstStatus.Items.Add(New ListItem(objCBTrx.mtdGetCashBankStatus(objCBTrx.EnumCashBankStatus.Closed), objCBTrx.EnumCashBankStatus.Closed))

        lstStatus.SelectedIndex = 0
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strCashBankIDFrom As String
        Dim strCashBankIDTo As String
        Dim strTransType As String
        Dim strUpdatedBy As String
        Dim strStatus As String
        Dim strAccCode As String
        Dim strVehTypeCode As String
        Dim strVehCode As String
        Dim strVehExpCode As String
        Dim strBlkType As String
        Dim strBlkGrpCode As String
        Dim strBlkCode As String
        Dim strSubBlkCode As String

        Dim strAccMonth As String
        Dim strAccYear As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim tempAccMonth As DropDownList
        Dim tempAccYear As DropDownList

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String

        Dim strDateFrom As String = Date_Validation(txtDateFrom.Text, False)
        Dim strDateTo As String = Date_Validation(txtDateTo.Text, False)

        tempAccMonth = RptSelect.FindControl("lstAccMonth")
        strAccMonth = Trim(tempAccMonth.SelectedItem.Text)
        tempAccYear = RptSelect.FindControl("lstAccYear")
        strAccYear = Trim(tempAccYear.SelectedItem.Text)
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

        If txtCashBankIDFrom.Text = "" Then
            strCashBankIDFrom = ""
        Else
            strCashBankIDFrom = Trim(txtCashBankIDFrom.Text)
        End If

        If txtCashBankIDTo.Text = "" Then
            strCashBankIDTo = ""
        Else
            strCashBankIDTo = Trim(txtCashBankIDTo.Text)
        End If

        strTransType = Trim(lstCashBankType.SelectedItem.Value)

        If txtUpdatedBy.Text = "" Then
            strUpdatedBy = ""
        Else
            strUpdatedBy = Trim(txtUpdatedBy.Text)
        End If

        If txtAccCode.Text = "" Then
            strAccCode = ""
        Else
            strAccCode = Trim(txtAccCode.Text)
        End If

        If txtVehType.Text = "" Then
            strVehTypeCode = ""
        Else
            strVehTypeCode = Trim(txtVehType.Text)
        End If

        If txtVehCode.Text = "" Then
            strVehCode = ""
        Else
            strVehCode = Trim(txtVehCode.Text)
        End If

        If txtVehExpCode.Text = "" Then
            strVehExpCode = ""
        Else
            strVehExpCode = Trim(txtVehExpCode.Text)
        End If

        strBlkType = Trim(lstBlkType.SelectedItem.Value)

        If txtBlkGrp.Text = "" Then
            strBlkGrpCode = ""
        Else
            strBlkGrpCode = Trim(txtBlkGrp.Text)
        End If

        If txtBlkCode.Text = "" Then
            strBlkCode = ""
        Else
            strBlkCode = Trim(txtBlkCode.Text)
        End If

        If txtSubBlkCode.Text = "" Then
            strSubBlkCode = ""
        Else
            strSubBlkCode = Trim(txtSubBlkCode.Text)
        End If


        If Trim(txtDateFrom.Text) <> "" Then
            If Trim(strDateFrom) = "" Then
                lblErrDate.Visible = True
                lblErrDate.Text = "Invalid date format." & strAcceptFormat
                Exit Sub
            End If
        Else
            lblErrDate.Visible = True
            lblErrDate.Text = "Please Insert Date"
            Exit Sub
        End If

        If Trim(txtDateTo.Text) <> "" Then
            If Trim(strDateTo) = "" Then
                lblErrDate.Visible = True
                lblErrDate.Text = "Invalid date format." & strAcceptFormat
                Exit Sub
            End If
        Else
            lblErrDate.Visible = True
            lblErrDate.Text = "Please Insert Date"
            Exit Sub
        End If

        strStatus = Trim(lstStatus.SelectedItem.Value)

        strCashBankIDFrom = Server.UrlEncode(strCashBankIDFrom)
        strCashBankIDTo = Server.UrlEncode(strCashBankIDTo)
        strUpdatedBy = Server.UrlEncode(strUpdatedBy)
        strAccCode = Server.UrlEncode(strAccCode)
        strVehTypeCode = Server.UrlEncode(strVehTypeCode)
        strVehCode = Server.UrlEncode(strVehCode)
        strVehExpCode = Server.UrlEncode(strVehExpCode)
        strBlkGrpCode = Server.UrlEncode(strBlkGrpCode)
        strBlkCode = Server.UrlEncode(strBlkCode)
        strSubBlkCode = Server.UrlEncode(strSubBlkCode)


        Response.Write("<Script Language=""JavaScript"">window.open(""CB_StdRpt_CashTransferListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & _
                        "&strAccMonth=" & strAccMonth & "&strAccYear=" & strAccYear & _
                        "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblAccCode=" & lblAccCode.Text & "&lblVehTypeCode=" & lblVehType.Text & _
                        "&lblVehCode=" & lblVehCode.Text & "&lblVehExpCode=" & lblVehExpCode.Text & "&lblBlkType=" & lblBlkType.Text & "&lblBlkGrp=" & lblBlkGrp.Text & "&lblBlkCode=" & lblBlkCode.Text & _
                        "&lblSubBlkCode=" & lblSubBlkCode.Text & "&lblLocation=" & lblLocation.Text & "&CashBankIDFrom=" & strCashBankIDFrom & "&CashBankIDTo=" & strCashBankIDTo & _
                        "&DateFrom=" & strDateFrom & _
                        "&DateTo=" & strDateTo & _
                        "&TransType=" & strTransType & "&UpdatedBy=" & strUpdatedBy & "&Status=" & strStatus & "&AccCode=" & strAccCode & "&VehTypeCode=" & strVehTypeCode & "&VehCode=" & strVehCode & _
                        "&VehExpCode=" & strVehExpCode & "&BlkType=" & strBlkType & "&BlkGrp=" & strBlkGrpCode & "&BlkCode=" & strBlkCode & "&SubBlkCode=" & strSubBlkCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub



    Function blnValidEndStartDate(ByVal pv_strEndDate As String, ByVal pv_strStartDate As String) As Boolean
        blnValidEndStartDate = False
        If CDate(pv_strStartDate) < CDate(pv_strEndDate) Then
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_LISTOFBANK_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/CB_StdRpt_ListOfBank.aspx")
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
