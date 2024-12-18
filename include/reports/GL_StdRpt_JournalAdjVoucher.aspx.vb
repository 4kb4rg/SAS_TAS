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

Public Class GL_StdRpt_JournalAdjVoucher : Inherits Page

    Protected RptSelect As UserControl

    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents txtJournalAdjIDFrom As TextBox
    Protected WithEvents txtJournalAdjIDTo As TextBox
    Protected WithEvents ddlAccMonth As DropDownList
    Protected WithEvents ddlAccYear As DropDownList
    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strAccTag As String
    Dim strBlkTag As String
	
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
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                OnLoad_AccPeriod("")
                BindStatus()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        strAccTag = GetCaption(objLangCap.EnumLangCap.Account)
        strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_JOURNALADJ_VOUCHER&errmesg=" & Exp.ToString() & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
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

    Sub BindStatus()
        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjStatus(objGLTrx.EnumJournalAdjStatus.All), "%"))
        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjStatus(objGLTrx.EnumJournalAdjStatus.Active), objGLTrx.EnumJournalAdjStatus.Active))
        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjStatus(objGLTrx.EnumJournalAdjStatus.Deleted), objGLTrx.EnumJournalAdjStatus.Deleted))
        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjStatus(objGLTrx.EnumJournalAdjStatus.Posted), objGLTrx.EnumJournalAdjStatus.Posted))
        lstStatus.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjStatus(objGLTrx.EnumJournalAdjStatus.Closed), objGLTrx.EnumJournalAdjStatus.Closed))
        lstStatus.SelectedIndex = 3 
    End Sub

    Sub OnLoad_AccPeriod(ByVal pv_strAccYear As String)
        Dim objAccCfg As New Dataset()
        Dim strOpCd_AccCfg_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_GET"
        Dim intMaxPeriod As Integer
        Dim intAccMonth As Integer
        Dim _strAccYear As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strParam As String

        If pv_strAccYear = "" Then
            ddlAccYear.Items.Clear
            If strAccMonth > 1 Then
                For intCnt = (Convert.ToInt16(strAccYear) - 1) to Convert.ToInt16(strAccYear)
                    ddlAccYear.Items.Add(New ListItem(intCnt, intCnt))
                Next
            Else
                For intCnt = (Convert.ToInt16(strAccYear) - 1) to (Convert.ToInt16(strAccYear) - 1)
                    ddlAccYear.Items.Add(New ListItem(intCnt, intCnt))
                Next
            End If
            ddlAccYear.SelectedIndex = 1
            _strAccYear = ddlAccYear.SelectedItem.Value
        Else
            _strAccYear = pv_strAccYear
        End If

        ddlAccMonth.Items.Clear()
        If _strAccYear = strAccYear Then      
            intAccMonth = Convert.ToInt16(strAccMonth) - 1
            For intCnt = 1 To intAccMonth
                ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            Next
            ddlAccMonth.SelectedIndex = intAccMonth - 1
        Else    
            Try
                strParam = "||" & _strAccYear
                intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_AccCfg_Get, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        objAccCfg)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_REPORT_JOURNALADJ_DETAIL_ACCCFG_GET&errmesg=" & Exp.ToString() & "&redirect=GL/trx/GL_trx_JournalAdj_List.aspx")
            End Try

            Try
                intAccMonth = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_REPORT_JOURNALADJ_DETAIL_ACCCFG_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & _strAccYear & "&redirect=")
            End Try
            objAccCfg = Nothing

            For intCnt = 1 To intAccMonth + 1
                ddlAccMonth.Items.Add(New ListItem(intCnt, intCnt))
            Next
            ddlAccMonth.SelectedIndex = intAccMonth
        End If
    End Sub

    Sub OnIndexChage_ReloadAccPeriod(ByVal sender As Object, ByVal e As System.EventArgs)
        OnLoad_AccPeriod(ddlAccYear.SelectedItem.Value)
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strJrnAdjIDFr As String
        Dim strJrnAdjIDTo As String
        Dim selAccMonth As String
        Dim selAccYear As String
        Dim strStatus As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)
        selAccMonth = ddlAccMonth.SelectedItem.Value
        selAccYear = ddlAccYear.SelectedItem.Value

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

        If txtJournalAdjIDFrom.Text = "" Then
            strJrnAdjIDFr = ""
        Else
            strJrnAdjIDFr = Trim(txtJournalAdjIDFrom.Text)
        End If

        If txtJournalAdjIDTo.Text = "" Then
            strJrnAdjIDTo = ""
        Else
            strJrnAdjIDTo = Trim(txtJournalAdjIDTo.Text)
        End If

        strStatus = Trim(lstStatus.SelectedItem.Value)
        strJrnAdjIDFr = Server.UrlEncode(strJrnAdjIDFr)
        strJrnAdjIDTo = Server.UrlEncode(strJrnAdjIDTo)

        Response.Write("<Script Language=""JavaScript"">window.open(""GL_StdRpt_JournalAdjVoucherPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & _
                       "&AccMonth=" & selAccMonth & "&AccYear=" & selAccYear & "&AccTag=" & strAccTag & "&BlkTag=" & strBlkTag & _
                       "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & "&JournalAdjIDFrom=" & strJrnAdjIDFr & "&JournalAdjTo=" & strJrnAdjIDTo & _
                       "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
