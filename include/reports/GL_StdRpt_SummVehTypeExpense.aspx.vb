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

Public Class GL_StdRpt_SummVehTypeExpense : Inherits Page

    Protected RptSelect As UserControl

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehTypeCode As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblBlkGrpCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblHidCostLevel As Label
    Protected WithEvents ddlBlkType As DropDownList
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow
    Protected WithEvents txtSrchVehCode As TextBox
    Protected WithEvents txtSrchVehTypeCode As TextBox
    Protected WithEvents txtSrchAccCode As TextBox
    Protected WithEvents txtSrchBlkGrpCode As TextBox
    Protected WithEvents txtSrchBlkCode As TextBox
    Protected WithEvents txtSrchSubBlkCode As TextBox
    Protected WithEvents txtSrchVehExpCode As TextBox
    Protected WithEvents rbSuppYes As RadioButton
    Protected WithEvents rbSuppNo As RadioButton
    Protected WithEvents PrintPrev As ImageButton

    Dim TrMthYr As HtmlTableRow

    Dim objGL As New agri.GL.clsReport()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()

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
    Dim strCostLevel As String
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox
        Dim intCnt As Integer

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
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                BindBlkType()
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
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.Visible = True

        If Page.IsPostBack Then
        End If
    End Sub

    Sub BindBlkType()
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblHidCostLevel.Text = "block"
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.Text, "BlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkGrpCode.Text, "BlkGrp"))
        Else
            lblHidCostLevel.Text = "subblock"
            ddlBlkType.Items.Add(New ListItem(lblSubBlkCode.Text, "SubBlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.Text, "BlkCode"))
        End If
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strBlkType As String
        Dim strSrchVehCode As String
        Dim strSrchVehTypeCode As String
        Dim strSrchAccCode As String
        Dim strSrchBlkGrpCode As String
        Dim strSrchBlkCode As String
        Dim strSrchSubBlkCode As String
        Dim strSrchVehExpCode As String
        Dim strSupp As String
        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()

        Dim enStrVehCode As String
        Dim enStrVehTypeCode As String
        Dim enStrAccCode As String
        Dim enStrBlkGrpCode As String
        Dim enStrBlkCode As String
        Dim enStrSubBlkCode As String
        Dim enStrVehExpCode As String
        Dim enStrSumm As String
        Dim intCnt As Integer
        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim strTemp As String

        ddlist = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(ddlist.SelectedItem.Value)

            For intCnt = 1 To CInt(strddlAccMth)
                strTemp = CStr(intCnt) & "','" & strTemp
            Next
            strddlAccMth = strTemp

        ddlist = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.Text)

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

        If rbSuppYes.Checked Then
            strSupp = rbSuppYes.Text
        ElseIf rbSuppNo.Checked Then
            strSupp = rbSuppNo.Text
        End If

        strCostLevel = Trim(lblHidCostLevel.Text)
        strBlkType = ddlBlkType.SelectedItem.Value

        strSrchVehCode = Trim(txtSrchVehCode.Text)
        strSrchVehTypeCode = Trim(txtSrchVehTypeCode.Text)
        strSrchAccCode = Trim(txtSrchAccCode.Text)
        strSrchBlkGrpCode = Trim(txtSrchBlkGrpCode.Text)
        strSrchBlkCode = Trim(txtSrchBlkCode.Text)
        strSrchSubBlkCode = Trim(txtSrchSubBlkCode.Text)
        strSrchVehExpCode = Trim(txtSrchVehExpCode.Text)

        enStrVehCode = Server.UrlEncode(strSrchVehCode)
        enStrVehTypeCode = Server.UrlEncode(strSrchVehTypeCode)
        enStrAccCode = Server.UrlEncode(strSrchAccCode)
        enStrBlkGrpCode = Server.UrlEncode(strSrchBlkGrpCode)
        enStrBlkCode = Server.UrlEncode(strSrchBlkCode)
        enStrSubBlkCode = Server.UrlEncode(strSrchSubBlkCode)
        enStrVehExpCode = Server.UrlEncode(strSrchVehExpCode)

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_SummVehTypeExpensePreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&SelLocation=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & "&sum=yes" & _
                       "&SelBlkType=" & strBlkType & _
                       "&SrchVehCode=" & enStrVehCode & _
                       "&SrchVehTypeCode=" & enStrVehTypeCode & _
                       "&SrchAccCode=" & enStrAccCode & _
                       "&SrchBlkGrpCode=" & enStrBlkGrpCode & _
                       "&SrchBlkCode=" & enStrBlkCode & _
                       "&SrchSubBlkCode=" & enStrSubBlkCode & _
                       "&SrchVehExpCode=" & enStrVehExpCode & _
                       "&CostLevel=" & strCostLevel & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, " & _
                       "location=no"");</Script>")

    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()

        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.VehType))
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehTypeCode.Text = GetCaption(objLangCap.EnumLangCap.VehType) & lblCode.Text
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block)
        lblBlkGrpCode.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & lblCode.Text
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_REPORTS_VEHICLEEXPENSEDETAIL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
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


End Class
