Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services

Public Class WS_StdRpt_JobList : Inherits Page

    Protected RptSelect As UserControl

    Dim objWS As New agri.WS.clsReport()
    Dim objWSTrx As New agri.WS.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()


    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblCompany As Label
    Protected WithEvents lblBillPartyCode As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblWorkCode As Label

    Protected WithEvents txtJobIDFrom As TextBox
    Protected WithEvents txtJobIDTo As TextBox
    Protected WithEvents txtJobStartDateFrom As TextBox
    Protected WithEvents txtJobStartDateTo As TextBox
    Protected WithEvents txtBillPartyCode As TextBox
    Protected WithEvents txtEmpID As TextBox    
    Protected WithEvents lstStatus As DropDownList
    Protected WithEvents lstJobType As DropDownList

    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents btnSelDateTo As Image

    Protected WithEvents rowBillPartyCode As HtmlTableRow
    Protected WithEvents rowEmpCode As HtmlTableRow

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
    Dim strLocType as String

    Dim dr As DataRow
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

        lblDate.Visible = False
        lblDateFormat.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatusList()
                BindJobType()                
            End If

        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim UCTrMthYr As HtmlTableRow

        UCTrMthYr = RptSelect.FindControl("TrMthYr")
        UCTrMthYr.Visible = True
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code"
        lblCompany.Text = GetCaption(objLangCap.EnumLangCap.Company)
        lblBillPartyCode.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & " Code"
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & " Code"
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code"
        lblWorkCode.Text = GetCaption(objLangCap.EnumLangCap.Work) & " Code"
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code"
        
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/WS_StdRpt_Selection.aspx")
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

    Sub BindStatusList()
        lstStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.All), objWSTrx.EnumJobStatus.All))
        lstStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Active), objWSTrx.EnumJobStatus.Active))
        lstStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Closed), objWSTrx.EnumJobStatus.Closed))
        lstStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Deleted), objWSTrx.EnumJobStatus.Deleted))
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strJobIDFrom As String
        Dim strJobIDTo As String
        Dim strJobStartDateFrom As String
        Dim strJobStartDateTo As String
        Dim strBillPartyCode As String
        Dim strEmpID As String
        Dim strErrMsg As String
        Dim strStatus As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
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

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)
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
        strUserLoc = Server.UrlEncode(strUserLoc)


        If txtJobIDFrom.Text = "" Then
            strJobIDFrom = ""
        Else
            strJobIDFrom = Trim(txtJobIDFrom.Text)
        End If

        If txtJobIDTo.Text = "" Then
            strJobIDTo = ""
        Else
            strJobIDTo = Trim(txtJobIDTo.Text)
        End If

        strJobStartDateFrom = txtJobStartDateFrom.Text
        strJobStartDateTo = txtJobStartDateTo.Text

        If txtBillPartyCode.Text = "" Then
            strBillPartyCode = ""
        Else
            strBillPartyCode = Trim(txtBillPartyCode.Text)
        End If

        If txtEmpID.Text = "" Then
            strEmpID = ""
        Else
            strEmpID = Trim(txtEmpID.Text)
        End If

        strStatus = Trim(lstStatus.SelectedItem.Value)

        strBillPartyCode = Server.UrlEncode(strBillPartyCode)

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WS_STDRPT_COMPLETEJOB_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/WS_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strJobStartDateFrom = "" And strJobStartDateTo = "") Then
            If strJobStartDateFrom <> ""  Then
                Response.write(strJobStartDateFrom)
                strJobStartDateFrom = GetValidDate(strJobStartDateFrom, strErrMsg)                
                Response.write(strJobStartDateFrom)
                
                If strJobStartDateFrom = "" Then
                    lblDateFormat.Text = objDateFormat & "."
                    lblDate.Visible = True
                    lblDateFormat.Visible = True
                    Exit Sub
                End If
            End If

            If strJobStartDateTo <> ""  Then
                strJobStartDateTo = GetValidDate(strJobStartDateTo, strErrMsg)
                If strJobStartDateTo = "" Then
                    lblDateFormat.Text = objDateFormat & "."
                    lblDate.Visible = True
                    lblDateFormat.Visible = True
                    Exit Sub
                End If
            End If

                Response.Write("<Script Language=""JavaScript"">window.open(""WS_StdRpt_JobListPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & "&DDLAccMth=" & strddlAccMth & _
                               "&DDLAccYr=" & strddlAccYr & "&lblVehicle=" & lblVehicle.Text & "&lblCompany=" & lblCompany.Text & "&lblBillPartyCode=" & lblBillPartyCode.Text & _
                               "&lblLocation=" & lblLocation.Text & "&lblAccCode=" & lblAccCode.Text & "&lblBlkCode=" & lblBlkCode.Text & "&lblVehExpCode=" & lblVehExpCode.Text & _
                               "&lblWorkCode=" & lblWorkCode.Text & _
                               "&JobIDFrom=" & strJobIDFrom & "&JobIDTo=" & strJobIDTo & _
                               "&JobStartDateFrom=" & strJobStartDateFrom & "&JobStartDateTo=" & strJobStartDateTo & _
                               "&JobType=" & Trim(fnGetValueFromDropDownList(lstJobType)) & _
                               "&BillPartyCode=" & strBillPartyCode & _                               
                               "&EmpID=" & strEmpID & "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""WS_StdRpt_JobListPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & "&DDLAccMth=" & strddlAccMth & _
                           "&DDLAccYr=" & strddlAccYr & "&lblVehicle=" & lblVehicle.Text & "&lblCompany=" & lblCompany.Text & "&lblBillPartyCode=" & lblBillPartyCode.Text & _
                           "&lblLocation=" & lblLocation.Text & "&lblAccCode=" & lblAccCode.Text & "&lblBlkCode=" & lblBlkCode.Text & "&lblVehExpCode=" & lblVehExpCode.Text & _
                           "&lblWorkCode=" & lblWorkCode.Text & _
                           "&JobIDFrom=" & strJobIDFrom & "&JobIDTo=" & strJobIDTo & _
                           "&JobStartDateFrom=" & objDateFrom & "&JobStartDateTo=" & objDateTo & _
                           "&JobType=" & Trim(fnGetValueFromDropDownList(lstJobType)) & _
                           "&BillPartyCode=" & strBillPartyCode & _
                           "&EmpID=" & strEmpID & "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

    Sub BindJobType()
            lstJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.All), objWSTrx.EnumJobType.All))
            lstJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.InternalUse), objWSTrx.EnumJobType.InternalUse))
            lstJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.StaffPayroll), objWSTrx.EnumJobType.StaffPayroll))
            lstJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.StaffDebitNote), objWSTrx.EnumJobType.StaffDebitNote))
            lstJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.ExternalParty), objWSTrx.EnumJobType.ExternalParty))
    End Sub

    Sub JobTypeChange(ByVal sender As Object, ByVal e As EventArgs)
            Dim intJobType As Integer = Trim(fnGetValueFromDropDownList(lstJobType))
            
            rowBillPartyCode.Visible = False
            rowEmpCode.Visible = False

            Select Case intJobType
                Case objWSTrx.EnumJobType.ExternalParty
                        rowBillPartyCode.Visible = True
                Case objWSTrx.EnumJobType.StaffPayroll, objWSTrx.EnumJobType.StaffDebitNote
                        rowEmpCode.Visible = True
                Case objWSTrx.EnumJobType.All
                        rowBillPartyCode.Visible = True
                        rowEmpCode.Visible = True
                Case Else

            End Select 
    End Sub
    
    Function fnGetValueFromDropDownList(ByRef ddlObject As DropDownList) As String
        If Trim(Request.Form(ddlObject.ID)) <> "" Then
            fnGetValueFromDropDownList = Trim(Request.Form(ddlObject.ID))
        Else
            fnGetValueFromDropDownList = ddlObject.SelectedItem.Value
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
