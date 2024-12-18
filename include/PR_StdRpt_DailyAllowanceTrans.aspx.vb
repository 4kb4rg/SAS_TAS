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

Public Class PR_StdRpt_DailyAllowanceTrans : Inherits Page

    Protected RptSelect As UserControl

    Dim objPR As New agri.PR.clsReport()
    Dim objHRTrx as New agri.HR.clsTrx()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents txtFromEmp As Textbox
    Protected WithEvents txtToEmp As TextBox
    Protected WithEvents txtDocNoFrom As Textbox
    Protected WithEvents txtDocNoTo As TextBox
    Protected WithEvents txtGangCode As TextBox
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblBlkGrpCode As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblHidCostLevel As Label
    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents ddlBlkType As DropDownList
    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow
    Protected WithEvents txtSrchAccCode As TextBox
    Protected WithEvents txtSrchBlkGrpCode As TextBox
    Protected WithEvents txtSrchBlkCode As TextBox
    Protected WithEvents txtSrchSubBlkCode As TextBox
    Protected WithEvents txtSrchVehCode As TextBox
    Protected WithEvents txtSrchVehExpCode As TextBox

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
            End If

            If ddlBlkType.SelectedItem.Value = "BlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
                TrSubBlk.Visible = False
                txtSrchSubBlkCode.text = ""
                txtSrchBlkGrpCode.text = ""
            ElseIf ddlBlkType.SelectedItem.Value = "BlkGrp" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
                TrSubBlk.Visible = False
                txtSrchSubBlkCode.text = ""
                txtSrchBlkCode.text = ""
            ElseIf ddlBlkType.SelectedItem.Value = "SubBlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = True
                txtSrchBlkCode.text = ""
                txtSrchBlkGrpCode.text = ""
            End If
        End If
    End Sub


    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblVehCode.text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.text
        lblVehExpCode.text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.text
        lblBlkCode.text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.text
        lblAccCode.text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.text

        lblBlock.text = GetCaption(objLangCap.EnumLangCap.Block)
        lblBlkGrpCode.text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & lblCode.text
        lblSubBlkCode.text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.text

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



    Sub BindBlkType()
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
            lblHidCostLevel.text = "block"
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.text, "BlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkGrpCode.text, "BlkGrp"))
        Else
            lblHidCostLevel.text = "subblock"
            ddlBlkType.Items.Add(New ListItem(lblSubBlkCode.text, "SubBlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.text, "BlkCode"))
        End If
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strFromEmp As String
        Dim strToEmp As String
        Dim strDocNoFrom As String
        Dim strDocNoTo As String
        Dim strGangCode As String
        Dim strStatus As String
        Dim strStatusText As String
        Dim strDec As String
        Dim strBlkType As String
        Dim strSrchAccCode As String
        Dim strSrchBlkCode As String
        Dim strSrchSubBlkCode As String
        Dim strSrchBlkGrpCode As String
        Dim strSrchVehCode As String
        Dim strSrchVehExpCode As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRptName As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)

        tempRptName = RptSelect.FindControl("lstRptName")
        strRptName = Trim(tempRptName.SelectedItem.Text)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)
        strFromEmp = txtFromEmp.Text
        strToEmp = txtToEmp.Text
        strDocNoFrom = txtDocNoFrom.Text
        strDocNoTo = txtDocNoTo.Text
        strGangCode = txtGangCode.Text

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

        If lstStatus.SelectedItem.Value = "All" Then
            strStatus = ""
            strStatusText = "ALL"
        Else
            strStatus = lstStatus.SelectedItem.Value
            strStatusText = UCase(lstStatus.SelectedItem.Text)
        End If

        strBlkType = Trim(ddlBlkType.SelectedItem.value)
        strSrchAccCode = Trim(txtSrchAccCode.text) 
        strSrchBlkCode = Trim(txtSrchBlkCode.text)
        strSrchSubBlkCode = Trim(txtSrchSubBlkCode.text)
        strSrchBlkGrpCode = Trim(txtSrchBlkGrpCode.text) 
        strSrchVehCode = Trim(txtSrchVehCode.text) 
        strSrchVehExpCode = Trim(txtSrchVehExpCode.text)

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_DailyAllowanceTransPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&lblLocation=" & lblLocation.Text & _
                       "&lblVehCode=" & lblVehCode.text & _
                       "&lblVehExpCode=" & lblVehExpCode.text & _
                       "&lblBlkCode=" & lblBlkCode.text & _
                       "&lblAccCode=" & lblAccCode.text & _
                       "&lblSubBlkCode=" & lblSubBlkCode.text & _
                       "&lblBlkGrpCode=" & lblBlkGrpCode.text & _
                       "&strCostLevel=" & lblHidCostLevel.text & _
                       "&FromEmp=" & strFromEmp & "&ToEmp=" & strToEmp & _
                       "&DocNoFrom=" & strDocNoFrom & "&DocNoTo=" & strDocNoTo & _
                       "&GangCode=" & strGangCode & _
                       "&strBlkType=" & strBlkType & _
                       "&StatusText=" & strStatusText & _
                       "&strSrchAccCode=" & strSrchAccCode & _
                       "&strSrchBlkCode=" & strSrchBlkCode & _
                       "&strSrchSubBlkCode=" & strSrchSubBlkCode & _
                       "&strSrchBlkGrpCode=" & strSrchBlkGrpCode & _
                       "&strSrchVehCode=" & strSrchVehCode & _
                       "&strSrchVehExpCode=" & strSrchVehExpCode & _
                       "&strRptTitle=" & strRptName & _
                       "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
