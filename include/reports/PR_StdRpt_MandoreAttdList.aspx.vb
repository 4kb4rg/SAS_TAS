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

Public Class PR_StdRpt_MandoreAttdList : Inherits Page

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
    Dim intConfig As Integer
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
        intConfig = Session("SS_CONFIGSETTING")
        strCostLevel = Session("SS_COSTLEVEL")

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_MANDOREATTDLIST_LANGCAP_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
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
        If LCase(strCostLevel) = "block" Then
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.text, "BlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkGrpCode.text, "BlkGrp"))
        Else
            ddlBlkType.Items.Add(New ListItem(lblSubBlkCode.text, "SubBlkCode"))
            ddlBlkType.Items.Add(New ListItem(lblBlkCode.text, "BlkCode"))
        End If
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strRptId As String
        Dim strRptTitle As String
        Dim strUserLoc As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String
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

        Dim ddl As DropDownList
        Dim lbl As Label
        Dim hih AS HtmlInputHidden
        
        ddl = RptSelect.FindControl("lstRptName")
        strRptID = Trim(ddl.SelectedItem.Value)
        strRptTitle = Trim(ddl.SelectedItem.Text)

        hih = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(hih.Value)

        ddl = RptSelect.FindControl("lstAccMonth")
        strSelAccMonth = Trim(ddl.SelectedItem.Value)
        ddl = RptSelect.FindControl("lstAccYear")
        strSelAccYear = Trim(ddl.SelectedItem.Value)
        ddl = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddl.SelectedItem.Value)

        strFromEmp = txtFromEmp.Text
        strToEmp = txtToEmp.Text
        strDocNoFrom = txtDocNoFrom.Text
        strDocNoTo = txtDocNoTo.Text
        strGangCode = txtGangCode.Text
        strBlkType = Trim(ddlBlkType.SelectedItem.value)
        strSrchAccCode = Trim(txtSrchAccCode.text) 
        strSrchBlkCode = Trim(txtSrchBlkCode.text)
        strSrchSubBlkCode = Trim(txtSrchSubBlkCode.text)
        strSrchBlkGrpCode = Trim(txtSrchBlkGrpCode.text) 
        strSrchVehCode = Trim(txtSrchVehCode.text) 
        strSrchVehExpCode = Trim(txtSrchVehExpCode.text)

        If strUserLoc = "" Then
            lbl = RptSelect.FindControl("lblUserLoc")
            lbl.Visible = True
            Exit Sub
        Else
            If Left(strUserLoc, 3) = "','" Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
            ElseIf Right(strUserLoc, 3) = "','" Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
            End If
        End If

        If lstStatus.SelectedItem.Value = "" Then
            strStatus = ""
            strStatusText = "ALL"
        Else
            strStatus = lstStatus.SelectedItem.Value
            strStatusText = UCase(lstStatus.SelectedItem.Text)
        End If


        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_MandoreAttdListPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&RptID=" & strRptId & _
                       "&RptTitle=" & strRptTitle & _
                       "&Location=" & strUserLoc & _
                       "&SelAccMonth=" & strSelAccMonth & _
                       "&SelAccYear=" & strSelAccYear & _
                       "&Decimal=" & strDec & _
                       "&FromEmp=" & strFromEmp & _
                       "&ToEmp=" & strToEmp & _
                       "&Status=" & strStatus & _
                       "&StatusText=" & strStatusText & _
                       "&DocNoFrom=" & strDocNoFrom & _
                       "&DocNoTo=" & strDocNoTo & _
                       "&GangCode=" & strGangCode & _
                       "&strBlkType=" & strBlkType & _
                       "&strSrchAccCode=" & strSrchAccCode & _
                       "&strSrchBlkCode=" & strSrchBlkCode & _
                       "&strSrchSubBlkCode=" & strSrchSubBlkCode & _
                       "&strSrchBlkGrpCode=" & strSrchBlkGrpCode & _
                       "&strSrchVehCode=" & strSrchVehCode & _
                       "&strSrchVehExpCode=" & strSrchVehExpCode & _
                       "&lblLocation=" & lblLocation.Text & _
                       "&lblAccCode=" & lblAccCode.text & _            
                       "&lblSubBlkCode=" & lblSubBlkCode.text & _
                       "&lblBlkCode=" & lblBlkCode.text & _
                       "&lblBlkGrpCode=" & lblBlkGrpCode.text & _
                       "&lblVehCode=" & lblVehCode.text & _
                       "&lblVehExpCode=" & lblVehExpCode.text & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
