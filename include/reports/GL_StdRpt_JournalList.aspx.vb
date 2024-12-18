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

Public Class GL_StdRpt_JournalList : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGLTrx As New agri.GL.clsTrx()
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

    Protected WithEvents txtJournalIDFrom As TextBox
    Protected WithEvents txtJournalIDTo As TextBox
    Protected WithEvents txtRefNo As TextBox
    Protected WithEvents lstTransType As DropDownList
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

    Protected WithEvents cbExcel As CheckBox

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
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")


        lblDate.visible = false
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindTransType()
                BlkTypeList()
                BindStatus()

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
        Dim UCTrFromTo As HtmlTableRow
        Dim UCTrMthYr As HtmlTableRow

        UCTrMthYr = RptSelect.FindControl("TrMthYr")
        UCTrMthYr.Visible = True
        UCTrFromTo = RptSelect.FindControl("trfromto")
        UCTrFromTo.Visible = True
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
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
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

        lstTransType.Items.Add(New ListItem(objGLTrx.mtdGetJournalTransactType(objGLTrx.EnumJournalTransactType.All), objGLTrx.EnumJournalTransactType.All))
        lstTransType.Items.Add(New ListItem(objGLTrx.mtdGetJournalTransactType(objGLTrx.EnumJournalTransactType.Adjustment), objGLTrx.EnumJournalTransactType.Adjustment))
        lstTransType.Items.Add(New ListItem(objGLTrx.mtdGetJournalTransactType(objGLTrx.EnumJournalTransactType.CreditNote), objGLTrx.EnumJournalTransactType.CreditNote))
        lstTransType.Items.Add(New ListItem(objGLTrx.mtdGetJournalTransactType(objGLTrx.EnumJournalTransactType.DebitNote), objGLTrx.EnumJournalTransactType.DebitNote))
        lstTransType.Items.Add(New ListItem(objGLTrx.mtdGetJournalTransactType(objGLTrx.EnumJournalTransactType.Invoice), objGLTrx.EnumJournalTransactType.Invoice))
        lstTransType.Items.Add(New ListItem(objGLTrx.mtdGetJournalTransactType(objGLTrx.EnumJournalTransactType.WorkshopDistribution), objGLTrx.EnumJournalTransactType.WorkshopDistribution))
        lstTransType.Items.Add(New ListItem(objGLTrx.mtdGetJournalTransactType(objGLTrx.EnumJournalTransactType.Umum), objGLTrx.EnumJournalTransactType.Umum))

    End Sub

    Sub BindStatus()

        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalStatus(objGLTrx.EnumJournalStatus.All), objGLTrx.EnumJournalStatus.All))
        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalStatus(objGLTrx.EnumJournalStatus.Active), objGLTrx.EnumJournalStatus.Active))
'        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalStatus(objGLTrx.EnumJournalStatus.Cancelled), objGLTrx.EnumJournalStatus.Cancelled))
        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalStatus(objGLTrx.EnumJournalStatus.Closed), objGLTrx.EnumJournalStatus.Closed))
        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalStatus(objGLTrx.EnumJournalStatus.Posted), objGLTrx.EnumJournalStatus.Posted))

        lstStatus.SelectedIndex = 1

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strJournalIDFrom As String
        Dim strJournalIDTo As String
        Dim strRefNo As String
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
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox
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
        Dim objDateTo As String

        Dim strExportToExcel As String

        tempAccMonth = RptSelect.FindControl("lstAccMonth")
        strAccMonth = Trim(tempAccMonth.SelectedItem.Text)
        tempAccYear = RptSelect.FindControl("lstAccYear")
        strAccYear = Trim(tempAccYear.SelectedItem.Text)
        tempDateFrom = RptSelect.FindControl("txtDateFrom")
        strDateFrom = Trim(tempDateFrom.Text)
        tempDateTo = RptSelect.FindControl("txtDateTo")
        strDateTo = Trim(tempDateTo.Text)
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

        If txtJournalIDFrom.Text = "" Then
            strJournalIDFrom = ""
        Else
            strJournalIDFrom = Trim(txtJournalIDFrom.Text)
        End If

        If txtJournalIDTo.Text = "" Then
            strJournalIDTo = ""
        Else
            strJournalIDTo = Trim(txtJournalIDTo.Text)
        End If

        If txtRefNo.Text = "" Then
            strRefNo = ""
        Else
            strRefNo = Trim(txtRefNo.Text)
        End If

        strTransType = Trim(lstTransType.SelectedItem.Value)

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

        strStatus = Trim(lstStatus.SelectedItem.Value)

        strJournalIDFrom = Server.UrlEncode(strJournalIDFrom)
        strJournalIDTo = Server.UrlEncode(strJournalIDTo)
        strRefNo = Server.UrlEncode(strRefNo)
        strUpdatedBy = Server.UrlEncode(strUpdatedBy)
        strAccCode = Server.UrlEncode(strAccCode)
        strVehTypeCode = Server.UrlEncode(strVehTypeCode)
        strVehCode = Server.UrlEncode(strVehCode)
        strVehExpCode = Server.UrlEncode(strVehExpCode)
        strBlkGrpCode = Server.UrlEncode(strBlkGrpCode)
        strBlkCode = Server.UrlEncode(strBlkCode)
        strSubBlkCode = Server.UrlEncode(strSubBlkCode)

        strExportToExcel = IIF(cbExcel.Checked = True, "1", "0")

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_JOURNAL_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strDateFrom = "" And strDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_JournalListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & _
                               "&strAccMonth=" & strAccMonth & "&strAccYear=" & strAccYear & _
                               "&RptName=" & strRptName & "&Decimal=" & strDec & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&lblAccCode=" & lblAccCode.Text & "&lblVehTypeCode=" & lblVehType.Text & _
                               "&lblVehCode=" & lblVehCode.Text & "&lblVehExpCode=" & lblVehExpCode.Text & "&lblBlkType=" & lblBlkType.Text & "&lblBlkGrp=" & lblBlkGrp.Text & "&lblBlkCode=" & lblBlkCode.Text & _
                               "&lblSubBlkCode=" & lblSubBlkCode.Text & "&lblLocation=" & lblLocation.Text & "&JournalIDFrom=" & strJournalIDFrom & "&JournalIDTo=" & strJournalIDTo & "&RefNo=" & strRefNo & _
                               "&TransType=" & strTransType & "&UpdatedBy=" & strUpdatedBy & "&Status=" & strStatus & "&AccCode=" & strAccCode & "&VehTypeCode=" & strVehTypeCode & "&VehCode=" & strVehCode & _
                               "&ExportToExcel=" & strExportToExcel & _
                               "&VehExpCode=" & strVehExpCode & "&BlkType=" & strBlkType & "&BlkGrp=" & strBlkGrpCode & "&BlkCode=" & strBlkCode & "&SubBlkCode=" & strSubBlkCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_JournalListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & _
                           "&strAccMonth=" & strAccMonth & "&strAccYear=" & strAccYear & _
                           "&RptName=" & strRptName & "&Decimal=" & strDec & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&lblAccCode=" & lblAccCode.Text & "&lblVehTypeCode=" & lblVehType.Text & _
                           "&lblVehCode=" & lblVehCode.Text & "&lblVehExpCode=" & lblVehExpCode.Text & "&lblBlkType=" & lblBlkType.Text & "&lblBlkGrp=" & lblBlkGrp.Text & "&lblBlkCode=" & lblBlkCode.Text & _
                           "&lblSubBlkCode=" & lblSubBlkCode.Text & "&lblLocation=" & lblLocation.Text & "&JournalIDFrom=" & strJournalIDFrom & "&JournalIDTo=" & strJournalIDTo & "&RefNo=" & strRefNo & _
                           "&TransType=" & strTransType & "&UpdatedBy=" & strUpdatedBy & "&Status=" & strStatus & "&AccCode=" & strAccCode & "&VehTypeCode=" & strVehTypeCode & "&VehCode=" & strVehCode & _
                            "&ExportToExcel=" & strExportToExcel & _
                            "&VehExpCode=" & strVehExpCode & "&BlkType=" & strBlkType & "&BlkGrp=" & strBlkGrpCode & "&BlkCode=" & strBlkCode & "&SubBlkCode=" & strSubBlkCode & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

End Class
