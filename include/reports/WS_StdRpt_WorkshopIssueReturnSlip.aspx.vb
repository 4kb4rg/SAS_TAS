Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.Services
Imports System.Xml
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Public Class WS_StdRpt_WorkshopIssueReturnSlip : Inherits Page
    Protected RptSelect As UserControl
    
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblBillParty As Label
    Protected WithEvents lblJobStartDateFromErr As Label
    Protected WithEvents lblJobStartDateToErr As Label
    
    Protected WithEvents txtJobIDFrom As TextBox
    Protected WithEvents txtJobIDTo As TextBox
    Protected WithEvents txtJobStartDateFrom As TextBox
    Protected WithEvents txtJobStartDateTo As TextBox
    Protected WithEvents txtBillPartyCode As TextBox
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents ddlJobType As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    
    Protected WithEvents imgJobStartDateFrom As Image
    Protected WithEvents imgJobStartDateTo As Image
    
    Protected WithEvents trBillPartyCode As HtmlTableRow
    Protected WithEvents trEmpCode As HtmlTableRow
    
    Protected WithEvents ibPrintPreview As ImageButton
    
    Dim objWS As New agri.WS.clsReport()
    Dim objWSTrx As New agri.WS.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()

    
    Dim dsLangCap As New DataSet()
    
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strLocType as String

    
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")
        

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            lblJobStartDateFromErr.Visible = False
            lblJobStartDateToErr.Visible = False
            
            If Not Page.IsPostBack Then
                Call GetLangCap
                Call BindJobTypeDropDownList
                Call BindStatusDropDownList
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim trMthYr As HtmlTableRow
        
        trMthYr = RptSelect.FindControl("TrMthYr")
        trMthYr.Visible = True
    End Sub
    
    Sub GetLangCap()
        dsLangCap = GetLanguageCaptionDS()
        
        lblLocation. Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblBillParty.Text = GetCaption(objLangCap.EnumLangCap.BillParty)
    End Sub



    Function GetCaption(ByVal pv_TermCode as String) As String
        Dim I As Integer

       For I = 0 To dsLangCap.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(dsLangCap.Tables(0).Rows(I).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTermMW"))
                else
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function
    
    Function GetLanguageCaptionDS() As DataSet
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim dsLC As DataSet
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
                                                 dsLC, _
                                                 strParam)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_STDRPT_WORKSHOP_ISSUE_RETURN_LISTING_GET_LANGCAP&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        Return dsLC
        If Not dsLC Is Nothing Then
            dsLC = Nothing
        End If
    End Function
    
    Sub BindJobTypeDropDownList() 
        ddlJobType.iTems.Clear
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.All), objWSTrx.EnumJobType.All))
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.InternalUse), objWSTrx.EnumJobType.InternalUse))
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.StaffPayroll), objWSTrx.EnumJobType.StaffPayroll))
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.StaffDebitNote), objWSTrx.EnumJobType.StaffDebitNote))
        ddlJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.ExternalParty), objWSTrx.EnumJobType.ExternalParty))
        ddlJobType.SelectedIndex = 0
    End Sub 
    
    Sub BindStatusDropDownList() 
        ddlStatus.Items.Clear
        ddlStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.All), objWSTrx.EnumJobStatus.All))
        ddlStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Active), objWSTrx.EnumJobStatus.Active))
        ddlStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Deleted), objWSTrx.EnumJobStatus.Deleted))
        ddlStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Closed), objWSTrx.EnumJobStatus.Closed))
        ddlStatus.SelectedIndex = 1
    End Sub 

     Sub ddlJobType_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim intJobType As Integer = Trim(GetDropDownListValue(ddlJobType))
        Select Case intJobType
            Case objWSTrx.EnumJobType.All
                trBillPartyCode.Visible = True
                trEmpCode.Visible = True
            Case objWSTrx.EnumJobType.StaffPayroll, objWSTrx.EnumJobType.StaffDebitNote
                trBillPartyCode.Visible = False
                trEmpCode.Visible = True
            Case objWSTrx.EnumJobType.ExternalParty
                trBillPartyCode.Visible = True
                trEmpCode.Visible = False
            Case Else   
                trBillPartyCode.Visible = False
                trEmpCode.Visible = False
        End Select
    End Sub
    
    Sub ibPrintPreview_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim lblTemp As Label
        Dim hihTemp As HtmlInputHidden
        Dim ddlTemp As DropDownList
        
        Dim strJobIDFrom As String
        Dim strJobIDTo As String
        Dim strJobStartDateFrom As String
        Dim strJobStartDateTo As String
        Dim strJobType As String
        Dim strBillPartyCode As String
        Dim strEmpCode As String
        Dim strStatus As String
        
        Dim strRptID As String
        Dim strRptName As String
        Dim strRptLocation As String
        Dim strRptAccMonth As String
        Dim strRptAccYear As String
        Dim strDecimal As String
        Dim strErrMsg As String
        Dim blnErrMsg As Boolean = False
        
        ddlTemp = RptSelect.FindControl("lstRptName")
        strRptID = Server.UrlEncode(Trim(ddlTemp.SelectedItem.Value))
        strRptName = Server.UrlEncode(Trim(ddlTemp.SelectedItem.Text))
        hihTemp = RptSelect.FindControl("hidUserLoc")
        strRptLocation = Trim(hihTemp.Value)
        ddlTemp = RptSelect.FindControl("lstAccMonth")
        strRptAccMonth = Server.UrlEncode(Trim(ddlTemp.SelectedItem.Value))
        ddlTemp = RptSelect.FindControl("lstAccYear")
        strRptAccYear = Server.UrlEncode(Trim(ddlTemp.SelectedItem.Value))
        ddlTemp = RptSelect.FindControl("lstDecimal")
        strDecimal = Server.UrlEncode(Trim(ddlTemp.SelectedItem.Value))

        If strRptLocation = "" Then
            lblTemp = RptSelect.FindControl("lblUserLoc")
            lblTemp.Visible = True
            blnErrMsg = True
        Else
            If Left(strRptLocation, 3) = "','" Then
                strRptLocation = Right(strRptLocation, Len(strRptLocation) - 3)
            ElseIf Right(strRptLocation, 3) = "','" Then
                strRptLocation = Left(strRptLocation, Len(strRptLocation) - 3)
            End If
        End If
        strRptLocation = Server.UrlEncode(strRptLocation)
        
        strJobIDFrom = Server.UrlEncode(Trim(txtJobIDFrom.Text))
        strJobIDTo = Server.UrlEncode(Trim(txtJobIDTo.Text))
        strJobStartDateFrom = Trim(txtJobStartDateFrom.Text)
        strJobStartDateTo = Trim(txtJobStartDateTo.Text)
        strJobType = Server.UrlEncode(Trim(GetDropDownListValue(ddlJobType)))
        strBillPartyCode = Server.UrlEncode(Trim(txtBillPartyCode.Text))
        strEmpCode = Server.UrlEncode(Trim(txtEmpCode.Text))
        strStatus = Server.UrlEncode(Trim(GetDropDownListValue(ddlStatus)))
        
        If strJobStartDateFrom <> "" Then
            strJobStartDateFrom = GetValidDate(strJobStartDateFrom, strErrMsg)
            If strJobStartDateFrom = "" Then
                lblJobStartDateFromErr.Text = strErrMsg
                lblJobStartDateFromErr.Visible = True
                blnErrMsg = True
            End If
        End If
        If strJobStartDateTo <> "" Then
            strJobStartDateTo = GetValidDate(strJobStartDateTo, strErrMsg)
            If strJobStartDateTo = "" Then
                lblJobStartDateToErr.Text = strErrMsg
                lblJobStartDateToErr.Visible = True
                blnErrMsg = True
            End If
        End If
        
        If blnErrMsg = True Then
            Exit Sub
        End If
        
        Response.Write("<Script Language=""JavaScript"">window.open(""WS_StdRpt_WorkshopIssueReturnSlipPreview.aspx?CompName=" & strCompany & "&RptID=" & strRptID & "&RptName=" & strRptName & "&RptLocation=" & strRptLocation & "&RptAccMonth=" & strRptAccMonth & "&RptAccYear=" & strRptAccYear & "&Decimal=" & strDecimal & _
                        "&lblLocation=" & lblLocation.Text & "&lblBillParty=" & lblBillParty.Text & _
                        "&JobIDFrom=" & strJobIDFrom & "&JobIDTo=" & strJobIDTo & _
                        "&JobStartDateFrom=" & strJobStartDateFrom & "&JobStartDateTo=" & strJobStartDateTo & _
                        "&JobType=" & strJobType & "&Status=" & strStatus & _
                        "&BillPartyCode=" & strBillPartyCode & "&EmpCode=" & strEmpCode & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
    
    Protected Function GetDropDownListValue(ByRef pr_ddlObject As DropDownList) As String
        If Trim(Request.Form(pr_ddlObject.ID)) <> "" Then
            GetDropDownListValue = Trim(Request.Form(pr_ddlObject.ID))
        Else
            GetDropDownListValue = pr_ddlObject.SelectedItem.Value
        End If
    End Function
    
    Protected Function GetValidDate(ByVal pv_strInputDate As String, ByRef pr_strErrMsg As String) As String
        Dim strDateFormat As String
        Dim strSQLDate As String

        If objGlobal.mtdValidInputDate(Session("SS_DATEFMT"), _
                                       pv_strInputDate, _
                                       strDateFormat, _
                                       strSQLDate) = True Then
            GetValidDate = strSQLDate
            pr_strErrMsg = ""
        Else
            GetValidDate = ""
            pr_strErrMsg = "Date format should be in " & strDateFormat
        End If
    End Function
End Class
