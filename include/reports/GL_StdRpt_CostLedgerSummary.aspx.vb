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

Public Class GL_StdRpt_CostLedgerSummary : Inherits Page


    Protected RptSelect As UserControl
    Protected WithEvents hidActGrpCode As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblActivity As Label
    Protected WithEvents lblActGrpCode As Label
    Protected WithEvents lblActCode As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblBlkGrpCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblExpenseCode As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblHidCostLevel As Label
    Protected WithEvents ddlBlkType As DropDownList
    Protected WithEvents ddlActType As DropDownList
    Protected WithEvents txtSrchActCode As TextBox
    Protected WithEvents txtSrchBlkGrpCode As TextBox
    Protected WithEvents txtSrchBlkCode As TextBox
    Protected WithEvents txtSrchSubBlkCode As TextBox
    Protected WithEvents txtSrchExpCode As TextBox
    Protected WithEvents cblActGrp As CheckBoxList
    Protected WithEvents cbActGrpAll As CheckBox
    Protected WithEvents TrActGrp As HtmlTableRow
    Protected WithEvents TrAct As HtmlTableRow
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow
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
    Dim tempActGrp As String
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
            If Not Page.IsPostBack
                GetActGrp()
                BindActType()
                BindBlkType()
            End If
            
            If ddlActType.SelectedItem.Value = "ActCode" Then
                TrActGrp.Visible = False
                TrAct.Visible = True
            Else 
                TrActGrp.Visible = True
                TrAct.Visible = False
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
        htmltr.visible = True

        If Page.IsPostBack Then
        end if
    End Sub

    Sub GetActGrp()
        Dim strParam As String
        Dim objActGrp As New DataSet()
        Dim arrParam As Array
        Dim intCntActGrp As Integer
        Dim intCntUB As Integer
        Dim strArrUserLoc As String
        Dim strOppCd_GetActGrp As String = "GL_STDRPT_MAINHARVEST_ACTGRP_GET"

        strParam = objGLSetup.EnumActGrpStatus.Active & "|"
        Try
            intErrNo = objGL.mtdGetMaintainHarvestActGrpList(strOppCd_GetActGrp, strParam, objActGrp)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_GL_COSTLEDGERSUM_ACTGRP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To objActGrp.Tables(0).Rows.Count - 1
            objActGrp.Tables(0).Rows(intCnt).Item("ActGrpCode") = Trim(objActGrp.Tables(0).Rows(intCnt).Item("ActGrpcode"))
        Next

        cblActGrp.DataSource = objActGrp.Tables(0)
        cblActGrp.DataValueField = "ActGrpCode"
        cblActGrp.DataBind()

    End Sub

    Sub BindActType()
        ddlActType.Items.Add(New ListItem(lblActCode.text, "ActCode"))
        ddlActtype.Items.Add(New ListItem(lblActGrpCode.text, "ActGrp"))
    End Sub

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

    Sub Check_Clicked(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntActGrpAll As Integer = 0
        Dim intCntActGrp As Integer = 0

        If cbActGrpAll.Checked Then
            For intCntActGrpAll = 0 To cblActGrp.Items.Count - 1
                cblActGrp.Items(intCntActGrpAll).Selected = True
            Next
        Else
            For intCntActGrpAll = 0 To cblActGrp.Items.Count - 1
                cblActGrp.Items(intCntActGrpAll).Selected = False
            Next

        End If
        ActGrpCheck()
    End Sub

    Sub ActGrpCheckList(ByVal Sender As Object, ByVal E As EventArgs)
        ActGrpCheck()
    End Sub

    Sub ActGrpCheck()
        Dim intCntActGrp As Integer = 0

        For intCntActGrp = 0 To cblActGrp.Items.Count - 1
            If cblActGrp.Items(intCntActGrp).Selected Then
                If cblActGrp.Items.Count = 1 Then
                    tempActGrp = cblActGrp.Items(intCntActGrp).Text
                Else
                    tempActGrp = tempActGrp & "','" & cblActGrp.Items(intCntActGrp).Text
                End If
            End If
        Next

        If Left(tempActGrp, 3) = "','" Then
            tempActGrp = Right(tempActGrp, Len(tempActGrp) - 3)
        End If
        hidActGrpCode.Value = tempActGrp

    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strActType As String
        Dim strBlkType As String
        Dim strSrchActGrpCode As String
        Dim strSrchActCode As String
        Dim strSrchBlkGrpCode As String
        Dim strSrchBlkCode As String
        Dim strSrchSubBlkCode As String
        Dim strSrchExpCode As String
        Dim strSupp As String
        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim intCntActGrp As Integer

        Dim enStrActCode As String
        Dim enStrBlkGrpCode As string
        Dim enStrBlkCode As String
        Dim enStrSubBlkCode As String
        Dim enStrExpCode As String

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim intCnt As Integer

        ddlist = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(ddlist.SelectedItem.value)

        ddlist = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(ddlist.SelectedItem.value)
        
        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.value)

        strActType = ddlActType.SelectedItem.value

        strBlkType = ddlBlkType.SelectedItem.value

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

        For intCntActGrp = 0 To cblActGrp.Items.Count - 1
            If cblActGrp.Items(intCntActGrp).Selected Then
                Exit For
            End If
        Next
        
        strCostLevel = Trim(lblHidCostLevel.text)
        strSrchActGrpCode = Trim(hidActGrpCode.Value)
        strSrchActCode = Trim(txtSrchActCode.text)
        strSrchBlkGrpCode = Trim(txtSrchBlkGrpCode.text)
        strSrchBlkCode = Trim(txtSrchBlkCode.text)
        strSrchSubBlkCode = Trim(txtSrchSubBlkCode.text)
        strSrchExpCode = Trim(txtSrchExpCode.text)

        enStrActCode = Server.UrlEncode(strSrchActCode)
        enStrBlkGrpCode = Server.UrlEncode(strSrchBlkGrpCode)
        enStrBlkCode = Server.UrlEncode(strSrchBlkCode)
        enStrSubBlkCode = Server.UrlEncode(strSrchSubBlkCode)
        enStrExpCode = Server.UrlEncode(strSrchExpCode)

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_CostLedgerSummaryPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&SelLocation=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & "&sum=yes" & _
                       "&SrchActType=" & strActType & _
                       "&SrchBlkType=" & strBlkType & _
                       "&SrchActGrpCode=" & strSrchActGrpCode & _
                       "&SrchActCode=" & enStrActCode & _
                       "&SrchBlkGrpCode=" & enStrBlkGrpCode & _
                       "&SrchBlkCode=" & enStrBlkCode & _
                       "&SrchSubBlkCode=" & enStrSubBlkCode & _
                       "&SrchExpCode=" & enStrExpCode & _ 
                       "&CostLevel=" & strCostLevel & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()

        lblActivity.text = GetCaption(objLangCap.EnumLangCap.Activity)
        lblActGrpCode.text = GetCaption(objLangCap.EnumLangCap.ActGrp) & lblCode.text 
        lblActCode.text = GetCaption(objLangCap.EnumLangCap.Activity) & lblCode.text
        lblBlock.text = GetCaption(objLangCap.EnumLangCap.Block)
        lblBlkGrpCode.text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & lblCode.text
        lblBlkCode.text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.text
        lblSubBlkCode.text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.text
        lblExpenseCode.text = GetCaption(objLangCap.EnumLangCap.Expense) & lblCode.text

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_REPORTS_COSTLEDGERSUMMARY_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
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
