Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Web.Services

Public Class PR_StdRpt_LoaderPaymentList : Inherits Page

    Protected RptSelect As UserControl

    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objPRTrx As New agri.PR.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow

    Protected WithEvents txtFromEmp As TextBox
    Protected WithEvents txtToEmp As TextBox
    Protected WithEvents lstEmpStatus As DropDownList
    Protected WithEvents txtGangCode As TextBox
    Protected WithEvents txtSrchAccCode As TextBox
    Protected WithEvents ddlBlkType As DropDownList
    Protected WithEvents txtSrchBlkGrpCode As TextBox
    Protected WithEvents txtSrchBlkCode As TextBox
    Protected WithEvents txtSrchSubBlkCode As TextBox
    Protected WithEvents txtSrchVehCode As TextBox
    Protected WithEvents txtSrchVehExpCode As TextBox
    Protected WithEvents lstTrxStatus As DropDownList
    Protected WithEvents lstSortBy As DropDownList
    Protected WithEvents lstOrderBy As DropDownList

    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblBlkGrpCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblBlockType As Label
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lblCostLevel As Label

    Dim objLangCapDs As New Object()
    Dim objSysCfgDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc As String
    Dim strCostLevel As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim tempAD As String
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                BindBlkType()
                BindTrxStatus()
                BindSortByList()
            End If

            If ddlBlkType.SelectedItem.Value = "BlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
                TrSubBlk.Visible = False
            ElseIf ddlBlkType.SelectedItem.Value = "BlkGrp" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
                TrSubBlk.Visible = False
            ElseIf ddlBlkType.SelectedItem.Value = "SubBlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = True
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub




    Sub BindBlkType()
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.Text, "BlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkGrpCode.Text, "BlkGrp"))
            lblCostLevel.text = "block"
        Else
            ddlBlkType.Items.Add(New ListItem(lblSubBlkCode.Text, "SubBlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.Text, "BlkCode"))
            lblCostLevel.text = "subblock"
        End If
    End Sub

    Sub BindTrxStatus()
        lstTrxStatus.Items.Add(New ListItem("All"))
        lstTrxStatus.Items.Add(New ListItem(objPRTrx.mtdGetLoaderPayStatus(objPRTrx.EnumLoaderPayStatus.Active), objPRTrx.EnumLoaderPayStatus.Active))
        lstTrxStatus.Items.Add(New ListItem(objPRTrx.mtdGetLoaderPayStatus(objPRTrx.EnumLoaderPayStatus.Closed), objPRTrx.EnumLoaderPayStatus.Closed))
        lstTrxStatus.Items.Add(New ListItem(objPRTrx.mtdGetLoaderPayStatus(objPRTrx.EnumLoaderPayStatus.Cancelled), objPRTrx.EnumLoaderPayStatus.Cancelled))
    End Sub

    Sub BindSortByList()
        lstSortBy.Items.Add(New ListItem("Employee Code", "code"))
        lstSortBy.Items.Add(New ListItem("Payment ID ", "other"))
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strFromEmp As String
        Dim strToEmp As String
        Dim strEmpStatus As String
        Dim strEmpStatusText As String
        Dim strGangCode As String
        Dim strBlkType As String
        Dim strSrchAccCode As String
        Dim strSrchBlkCode As String
        Dim strSrchSubBlkCode As String
        Dim strSrchBlkGrpCode As String
        Dim strSrchVehCode As String
        Dim strSrchVehExpCode As String
        Dim strTrxStatus As String
        Dim strTrxStatusText As String
        Dim strSortBy As String
        Dim strOrderBy As String

        Dim strDDLAccMth As String
        Dim strDDLAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strDDLAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strDDLAccYr = Trim(tempAccYr.SelectedItem.Value)

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)
        strFromEmp = Trim(txtFromEmp.Text)
        strToEmp = Trim(txtToEmp.Text)
        strGangCode = Trim(txtGangCode.Text)

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

        If lstEmpStatus.SelectedItem.Value = "" Then
            strEmpStatus = ""
            strEmpStatusText = "All"
        Else
            strEmpStatus = lstEmpStatus.SelectedItem.Value
            strEmpStatusText = lstEmpStatus.SelectedItem.Text
        End If

        strBlkType = Trim(ddlBlkType.SelectedItem.Value)
        strSrchAccCode = Server.UrlEncode(Trim(txtSrchAccCode.Text))
        strSrchBlkCode = Server.UrlEncode(Trim(txtSrchBlkCode.Text))
        strSrchSubBlkCode = Server.UrlEncode(Trim(txtSrchSubBlkCode.Text))
        strSrchBlkGrpCode = Server.UrlEncode(Trim(txtSrchBlkGrpCode.Text))
        strSrchVehCode = Server.UrlEncode(Trim(txtSrchVehCode.Text))
        strSrchVehExpCode = Server.UrlEncode(Trim(txtSrchVehExpCode.Text))

        If lstTrxStatus.SelectedItem.Value = "" Then
            strTrxStatus = ""
            strTrxStatusText = "All"
        Else
            strTrxStatus = lstTrxStatus.SelectedItem.Value
            strTrxStatusText = lstTrxStatus.SelectedItem.Text
        End If

        strSortBy = Trim(lstSortBy.SelectedItem.Value)
        strOrderBy = Trim(lstOrderBy.SelectedItem.Value)
        strCostLevel = Trim(lblCostLevel.text)

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_LoaderPaymentListPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & "&ddlAccMth=" & strDDLAccMth & "&ddlAccYr=" & strDDLAccYr & _
                       "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & _
                       "&FromEmp=" & strFromEmp & "&ToEmp=" & strToEmp & _
                       "&EmpStatus=" & strEmpStatus & "&EmpStatusText=" & strEmpStatusText & _
                       "&GangCode=" & strGangCode & "&strBlkType=" & strBlkType & _
                       "&lblAccCode=" & lblAccCode.Text & "&strSrchAccCode=" & strSrchAccCode & _
                       "&lblBlkGrpCode=" & lblBlkGrpCode.Text & "&strSrchBlkGrpCode=" & strSrchBlkGrpCode & _
                       "&lblBlkCode=" & lblBlkCode.Text & "&strSrchBlkCode=" & strSrchBlkCode & _
                       "&lblSubBlkCode=" & lblSubBlkCode.Text & "&strSrchSubBlkCode=" & strSrchSubBlkCode & _
                       "&lblVehCode=" & lblVehCode.Text & "&strSrchVehCode=" & strSrchVehCode & _
                       "&lblVehExpCode=" & lblVehExpCode.Text & "&strSrchVehExpCode=" & strSrchVehExpCode & _
                       "&TrxStatus=" & strTrxStatus & "&TrxStatusText=" & strTrxStatusText & _
                       "&SortBy=" & strSortBy & "&OrderBy=" & strOrderBy & "&CostLevel=" & strCostLevel & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblBlockType.Text = GetCaption(objLangCap.EnumLangCap.Block)

        lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        lblBlkGrpCode.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & lblCode.Text
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_LOADERPAY_LANGCAP_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
        End Try
    End Sub



    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function


End Class
