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

Public Class GL_StdRpt_DetAccLedger : Inherits Page

    Protected RptSelect As UserControl
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblBlkGrpCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehTypeCode As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblHidCostLevel As Label
    Protected WithEvents ddlBlkType As DropDownList
    Protected WithEvents txtSrchBlkGrpCode As TextBox
    Protected WithEvents txtSrchBlkCode As TextBox
    Protected WithEvents txtSrchSubBlkCode As TextBox
    Protected WithEvents txtSrchAccCode As TextBox
    Protected WithEvents txtSrchVehCode As TextBox
    Protected WithEvents txtSrchVehTypeCode As TextBox
    Protected WithEvents txtSrchVehExpCode As TextBox
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
    Dim intErrNo As Integer
    Dim strCostLevel As String
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox

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
        htmltr.visible = True
        If Page.IsPostBack Then
        end if
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


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strBlkType As String
        Dim strSrchBlkGrpCode As String
        Dim strSrchBlkCode As String
        Dim strSrchSubBlkCode As String
        Dim strSrchAccCode As String
        Dim strSrchVehCode As String
        Dim strSrchVehTypeCode As String
        Dim strSrchVehExpCode As String
        Dim strSupp As String
        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()

        Dim enStrBlkGrpCode As string
        Dim enStrBlkCode As String
        Dim enStrSubBlkCode As String
        Dim enStrAccCode As String
        Dim enStrVehCode As String
        Dim enStrVehTypeCode As String
        Dim enStrVehExpCode As String

        Dim ddlist As DropDownList
        Dim rdblist As RadioButtonList
        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label


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

        strCostLevel = Trim(lblHidCostLevel.text)
        strSrchBlkGrpCode = Trim(txtSrchBlkGrpCode.text)
        strSrchBlkCode = Trim(txtSrchBlkCode.text)
        strSrchSubBlkCode = Trim(txtSrchSubBlkCode.text)
        strSrchAccCode = Trim(txtSrchAccCode.text)
        strSrchVehCode = Trim(txtSrchVehCode.text)
        strSrchVehTypeCode = Trim(txtSrchVehTypeCode.text)
        strSrchVehExpCode = Trim(txtSrchVehExpCode.text)

        enStrBlkGrpCode = Server.UrlEncode(strSrchBlkGrpCode)
        enStrBlkCode = Server.UrlEncode(strSrchBlkCode)
        enStrSubBlkCode = Server.UrlEncode(strSrchSubBlkCode)
        enStrAccCode = Server.UrlEncode(strSrchAccCode)
        enStrVehCode = Server.UrlEncode(strSrchVehCode)
        enStrVehTypeCode = Server.UrlEncode(strSrchVehTypeCode)
        enStrVehExpCode = Server.UrlEncode(strSrchVehExpCode)

     
        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_DetAccLedgerPreview.aspx?CompName="& strCompany & _
                       "&SelLocation=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _ 
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&Supp=" & strSupp & "&sum=yes" & _
                       "&SrchBlkType=" & strBlkType & _
                       "&SrchBlkGrpCode=" & enStrBlkGrpCode & _
                       "&SrchBlkCode=" & enStrBlkCode & _
                       "&SrchSubBlkCode=" & enStrSubBlkCode & _
                       "&SrchAccCode=" & enStrAccCode & _
                       "&SrchVehCode=" & enStrVehCode & _
                       "&SrchVehTypeCode=" & enStrVehTypeCode & _
                       "&SrchVehExpCode=" & enStrVehExpCode & _
                       "&CostLevel=" & strCostLevel & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblAccCode.text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.text
        lblBlock.text = GetCaption(objLangCap.EnumLangCap.Block) 
        lblBlkGrpCode.text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & lblCode.text
        lblBlkCode.text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.text
        lblSubBlkCode.text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.text
        lblVehCode.text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.text
        lblVehTypeCode.text = GetCaption(objLangCap.EnumLangCap.VehType) & lblCode.text
        lblVehExpCode.text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.text

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_REPORTS_DETACCLEDGER_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
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
