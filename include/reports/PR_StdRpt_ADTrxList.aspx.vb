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

Public Class PR_StdRpt_ADTrxList : Inherits Page

    Protected RptSelect As UserControl

    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Protected WithEvents lblTracker As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents txtFromEmp As Textbox
    Protected WithEvents txtToEmp As TextBox
    Protected WithEvents lstEmpStatus As DropDownList
    Protected WithEvents txtDocNoFrom As Textbox
    Protected WithEvents txtDocNoTo As TextBox
    Protected WithEvents txtGangCode As TextBox
    Protected WithEvents lblAccCode As Label
    Protected WithEvents txtSrchAccCode As TextBox
    Protected WithEvents lblBlock As Label
    Protected WithEvents ddlBlkType As DropDownList
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents lblBlkGrpCode As Label
    Protected WithEvents txtSrchBlkGrpCode As TextBox
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents txtSrchBlkCode As TextBox
    Protected WithEvents TrSubBlk As HtmlTableRow
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents txtSrchSubBlkCode As TextBox
    Protected WithEvents lblVehCode As Label
    Protected WithEvents txtSrchVehCode As TextBox
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents txtSrchVehExpCode As TextBox
    Protected WithEvents lstSelMonth As DropDownList
    Protected WithEvents lstSelYear As DropDownList
    Protected WithEvents lstSelPeriod As DropDownList
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents lstOrderBy As DropDownList
    Protected WithEvents lstSortBy As DropDownList

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblHidCostLevel As Label

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

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim tempAD As String

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
                BindAccMonthList(BindAccYearList(strLocation, strAccYear))  
                BindBlkType()
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

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblVehCode.text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense)
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code "
        lblAccCode.text = GetCaption(objLangCap.EnumLangCap.Account) & " Code" 

        lblBlock.text = GetCaption(objLangCap.EnumLangCap.Block)
        lblBlkGrpCode.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & " Code "
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code "
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_REPORTS_DAILYALLOWANCETRANS_LANGCAP_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
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

    Sub BindAccMonthList(ByVal pv_intMaxMonth As Integer)
        Dim intCnt As Integer
        Dim intSelIndex As Integer = 0

        lstSelMonth.Items.Clear
        For intCnt = 1 To pv_intMaxMonth
            lstSelMonth.Items.Add(intCnt)
            If intCnt = Convert.ToInt16(strAccMonth) Then
                intSelIndex = intCnt - 1
            End If
        Next
        lstSelMonth.SelectedIndex = intSelIndex
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_ADTRX_LIST_ACCCFG_DIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        intSelIndex = 0 
        lstSelYear.Items.Clear

        If objAccCfg.Tables(0).Rows.Count > 0 Then      
            For intCnt = 0 To objAccCfg.Tables(0).Rows.Count - 1    
                lstSelYear.Items.Add(objAccCfg.Tables(0).Rows(intCnt).Item("AccYear"))

                If objAccCfg.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                    intSelIndex = intCnt    
                End If
            Next

            lstSelYear.SelectedIndex = intSelIndex
            intAccYear = lstSelYear.SelectedItem.Value
            Try
                strParam = "||" & intAccYear             
                intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_Max_Get, _
                                                        strCompany, _
                                                        pv_strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        objAccCfg)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_ADTRX_LIST_ACCCFG_MAX_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            Try
                intMaxPeriod = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_ADTRX_LIST_ACCCFG_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & Convert.ToString(intAccYear) & "&redirect=")
            End Try

        Else
            lstSelYear.Items.Add(strAccYear)    
            lstSelYear.SelectedIndex = intSelIndex
            intMaxPeriod = Convert.ToInt16(strAccMonth) 
        End If

        objAccCfg = Nothing
        Return intMaxPeriod
    End Function

    Sub OnIndexChage_FromAccPeriod(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim hidUserLoc As HtmlInputHidden

        hidUserLoc = RptSelect.FindControl("hidUserLoc")
        BindAccMonthList(BindAccYearList(hidUserLoc.Value, lstSelYear.SelectedItem.Value))
    End Sub

    Sub BindBlkType()
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
            lblHidCostLevel.text = "block"
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.Text, "BlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkGrpCode.text, "BlkGrp"))
        Else
            lblHidCostLevel.text = "subblock"
            ddlBlkType.Items.Add(New ListItem(lblSubBlkCode.text, "SubBlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.text, "BlkCode"))
        End If
    End Sub

    Sub BindSortByList()
        lstSortBy.Items.Add(New ListItem("AD Code", "ad"))
        lstSortBy.Items.Add(New ListItem("Employee Code", "emp"))
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDdlAccMth As String
        Dim strDdlAccYr As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strFromEmp As String
        Dim strToEmp As String
        Dim strEmpStatus As String
        Dim strDocNoFrom As String
        Dim strDocNoTo As String
        Dim strEmpStatusText As String
        Dim strSrchGangCode As String
        Dim strDec As String
        Dim strBlkType As String
        Dim strSrchAccCode As String
        Dim strSrchBlkCode As String
        Dim strSrchSubBlkCode As String
        Dim strSrchBlkGrpCode As String
        Dim strSrchVehCode As String
        Dim strSrchVehExpCode As String
        Dim strDdlSelMth As String
        Dim strDdlSelYr As String
        Dim strDdlSelPeriodText As String
        Dim strDdlSelPeriod As String
        Dim strSortBy As String
        Dim strSortByText As String
        Dim strOrderBy As String
        Dim strOrderByText As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRptName As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        strDdlAccMth = lstSelMonth.SelectedItem.Value
        strDdlAccYr = lstSelYear.SelectedItem.Value

        tempRptName = RptSelect.FindControl("lstRptName")
        strRptName = Trim(tempRptName.SelectedItem.Value)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)
        strFromEmp = txtFromEmp.Text
        strToEmp = txtToEmp.Text
        strDocNoFrom = txtDocNoFrom.Text
        strDocNoTo = txtDocNoTo.Text

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
            strEmpStatusText = "ALL"
        Else
            strEmpStatus = lstEmpStatus.SelectedItem.Value
            strEmpStatusText = UCase(lstEmpStatus.SelectedItem.Text)
        End If

        strSrchGangCode = Trim(txtGangCode.Text)
        strBlkType = Trim(ddlBlkType.SelectedItem.Value)
        strSrchAccCode = Trim(txtSrchAccCode.Text)
        strSrchBlkCode = Trim(txtSrchBlkCode.Text)
        strSrchSubBlkCode = Trim(txtSrchSubBlkCode.Text)
        strSrchBlkGrpCode = Trim(txtSrchBlkGrpCode.Text)
        strSrchVehCode = Trim(txtSrchVehCode.Text)
        strSrchVehExpCode = Trim(txtSrchVehExpCode.Text)
        strDdlSelMth = Trim(lstSelMonth.SelectedItem.Value)
        strDdlSelYr = Trim(lstSelYear.SelectedItem.Value)
        strDdlSelPeriodText = Trim(lstSelPeriod.SelectedItem.Text)
        strDdlSelPeriod = Trim(lstSelPeriod.SelectedItem.Value)
        strSortBy = Trim(lstSortBy.SelectedItem.Value)
        strSortByText = Trim(lstSortBy.SelectedItem.Text)
        strOrderBy = Trim(lstOrderBy.SelectedItem.Value)
        strOrderByText = Trim(lstOrderBy.SelectedItem.Text)

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_ADTrxListPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & "&ddlAccMth=" & strDdlAccMth & "&ddlAccYr=" & strDdlAccYr & _
                       "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & _
                       "&FromEmp=" & strFromEmp & "&ToEmp=" & strToEmp & _
                       "&EmpStatus=" & strEmpStatus & "&EmpStatusText=" & strEmpStatusText & _
                       "&DocNoFrom=" & strDocNoFrom & "&DocNoTo=" & strDocNoTo & _
                       "&SrchGangCode=" & strSrchGangCode & _
                       "&strBlkType=" & strBlkType & "&strCostLevel=" & lblHidCostLevel.Text & _
                       "&lblAccCode=" & lblAccCode.Text & "&strSrchAccCode=" & strSrchAccCode & _
                       "&lblBlkGrpCode=" & lblBlkGrpCode.Text & "&strSrchBlkGrpCode=" & strSrchBlkGrpCode & _
                       "&lblBlkCode=" & lblBlkCode.Text & "&strSrchBlkCode=" & strSrchBlkCode & _
                       "&lblSubBlkCode=" & lblSubBlkCode.Text & "&strSrchSubBlkCode=" & strSrchSubBlkCode & _
                       "&lblVehCode=" & lblVehCode.Text & "&strSrchVehCode=" & strSrchVehCode & _
                       "&lblVehExpCode=" & lblVehExpCode.Text & "&strSrchVehExpCode=" & strSrchVehExpCode & _
                       "&ddlSelMth=" & strDdlSelMth & "&ddlSelYr=" & strDdlSelYr & _
                       "&lblSelPeriodText=" & strDdlSelPeriodText & "&ddlSelPeriod=" & strDdlSelPeriod & _
                       "&SortBy=" & strSortBy & "&OrderBy=" & strOrderBy & _
                       "&SortByText=" & strSortByText & "&OrderByText=" & strOrderByText & _
                       "&lblBlockTag=" & lblBlock.Text & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
