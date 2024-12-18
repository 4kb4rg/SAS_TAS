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

Public Class PR_StdRpt_WagesPaymentList : Inherits Page

    Protected RptSelect As UserControl

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objHR As New agri.HR.clsTrx()
    Dim objPR As New agri.PR.clsReport()
    Dim objPRTrx As New agri.PR.clsTrx()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblCompCode As Label
    Protected WithEvents ddlCompCode As DropDownList
    Protected WithEvents lblLocCode As Label
    Protected WithEvents ddlLocCode As DropDownList
    Protected WithEvents txtEmpCodeFrom As TextBox
    Protected WithEvents txtEmpCodeTo As TextBox
    Protected WithEvents txtGangCode As TextBox
    Protected WithEvents lblDeptCode As Label
    Protected WithEvents ddlDeptCode As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents ddlPayMode As DropDownList

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblDate As Label
    
    Protected WithEvents cblThumbPrint As CheckBoxList

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()
    Dim objCompDs As New Object()
    Dim objLocDs As New Object()
    Dim objDeptDs As New Object()

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

    Dim strSelectedCompCode As String = ""
    Dim strSelectedLocCode As String = ""
    Dim strSelectedDeptCode As String = ""
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
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindComp()
                BindLoc()
                BindDept()
                BindStatus()
                BindPayMode()
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

        lblCompCode.Text = GetCaption(objLangCap.EnumLangCap.Company)
        lblLocCode.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblDeptCode.Text = GetCaption(objLangCap.EnumLangCap.Department)
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
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

    Sub BindComp()
        Dim strOpCd_Get As String = "ADMIN_CLSCOMP_COMPANY_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intCompIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objAdminComp.EnumCompanyStatus.Active & "||CompCode|" 

        Try
            intErrNo = objAdminComp.mtdGetComp(strOpCd_Get, strParam, objCompDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_COMPANY&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objCompDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objCompDs.Tables(0).Rows.Count - 1
                objCompDs.Tables(0).Rows(intCnt).Item("CompCode") = Trim(objCompDs.Tables(0).Rows(intCnt).Item("CompCode"))
                objCompDs.Tables(0).Rows(intCnt).Item("CompName") = Trim(objCompDs.Tables(0).Rows(intCnt).Item("CompCode")) & " (" & _
                                                                    Trim(objCompDs.Tables(0).Rows(intCnt).Item("CompName")) & ")"
                
                If objCompDs.Tables(0).Rows(intCnt).Item("CompCode") = strSelectedCompCode
                    intCompIndex = intCnt + 1
                End If
            Next
        End If

        dr = objCompDs.Tables(0).NewRow()
        dr("CompCode") = ""
        dr("CompName") = lblSelect.text & lblCompCode.text & lblCode.text
        objCompDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlCompCode.DataSource = objCompDs.Tables(0)
        ddlCompCode.DataTextField = "CompName"
        ddlCompCode.DataValueField = "CompCode"
        ddlCompCode.DataBind()
        ddlCompCode.SelectedIndex = intCompIndex
    End Sub

    Sub BindLoc()
        Dim strOpCd_Get As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intLocIndex As Integer = 0
        Dim dr As DataRow

        strParam = "||" & objAdminLoc.EnumLocStatus.Active & "||LocCode||" 

        Try
            intErrNo = objAdminLoc.mtdGetLocCode(strOpCd_Get, strParam, objLocDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_LOCATION&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objLocDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
                objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objLocDs.Tables(0).Rows(intCnt).Item("LocCode"))
                objLocDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objLocDs.Tables(0).Rows(intCnt).Item("LocCode")) & " (" & _
                                                                      Trim(objLocDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = strSelectedLocCode
                    intLocIndex = intCnt + 1
                End If
            Next
        End If

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelect.text & lblLocation.text & lblCode.text
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocCode.DataSource = objLocDs.Tables(0)
        ddlLocCode.DataTextField = "Description"
        ddlLocCode.DataValueField = "LocCode"
        ddlLocCode.DataBind()
        ddlLocCode.SelectedIndex = intLocIndex
    End Sub

    Sub BindDept()
        Dim strOpCd_Get As String = "HR_CLSSETUP_DEPT_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intDeptIndex As Integer = 0
        Dim dr As DataRow

        strParam = "|" & objHRSetup.EnumDeptStatus.Active & "||A.DeptCode||" 

        Try
            intErrNo = objHRSetup.mtdGetDept(strOpCd_Get, strParam, objDeptDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_DEPARTMENT&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDeptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDeptDs.Tables(0).Rows.Count - 1
                objDeptDs.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(objDeptDs.Tables(0).Rows(intCnt).Item("DeptCode"))
                objDeptDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDeptDs.Tables(0).Rows(intCnt).Item("DeptCode")) & " (" & _
                                                                       Trim(objDeptDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                
                If objDeptDs.Tables(0).Rows(intCnt).Item("DeptCode") = strSelectedDeptCode
                    intDeptIndex = intCnt + 1
                End If
            Next
        End If

        dr = objDeptDs.Tables(0).NewRow()
        dr("DeptCode") = ""
        dr("Description") = lblSelect.text & lblDeptCode.text
        objDeptDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDeptCode.DataSource = objDeptDs.Tables(0)
        ddlDeptCode.DataTextField = "Description"
        ddlDeptCode.DataValueField = "DeptCode"
        ddlDeptCode.DataBind()
        ddlDeptCode.SelectedIndex = intDeptIndex
    End Sub

    Sub BindStatus()
        ddlStatus.Items.Add(New ListItem("All", ""))
        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetWagesStatus(objPRTrx.EnumWagesStatus.Active), objPRTrx.EnumWagesStatus.Active))
        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetWagesStatus(objPRTrx.EnumWagesStatus.Printed), objPRTrx.EnumWagesStatus.Printed))
        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetWagesStatus(objPRTrx.EnumWagesStatus.Paid), objPRTrx.EnumWagesStatus.Paid))
        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetWagesStatus(objPRTrx.EnumWagesStatus.Void), objPRTrx.EnumWagesStatus.Void))
        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetWagesStatus(objPRTrx.EnumWagesStatus.Duplicated), objPRTrx.EnumWagesStatus.Duplicated))
        ddlStatus.SelectedIndex = 1
    End Sub

    Sub BindPayMode()
        ddlPayMode.Items.Add(New ListItem("All", ""))
        ddlPayMode.Items.Add(New ListItem(objHR.mtdGetPayMode(objHR.EnumPayMode.Bank), objHR.EnumPayMode.Bank))
        ddlPayMode.Items.Add(New ListItem(objHR.mtdGetPayMode(objHR.EnumPayMode.Cheque), objHR.EnumPayMode.Cheque))
        ddlPayMode.Items.Add(New ListItem(objHR.mtdGetPayMode(objHR.EnumPayMode.Cash), objHR.EnumPayMode.Cash))
        ddlPayMode.SelectedIndex = 0
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDdlAccMth As String
        Dim strDdlAccYr As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strCompCode As String
        Dim strLocCode As String
        Dim strEmpCodeFrom As String
        Dim strEmpCodeTo As String
        Dim strGangCode As String
        Dim strDeptCode As String
        Dim strStatus As String
        Dim strStatus_Value As String
        Dim strPayMode As String
        Dim strPayMode_Value As String
        Dim strDec As String
        Dim strThumbPrint As String = ""

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRptName As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strDdlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strDdlAccYr = Trim(tempAccYr.SelectedItem.Value)

        tempRptName = RptSelect.FindControl("lstRptName")
        strRptName = Trim(tempRptName.SelectedItem.Text)
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

        If ddlCompCode.SelectedItem.Value <> "" Then
            strCompCode = ddlCompCode.SelectedItem.Value
        End If

        If ddlLocCode.SelectedItem.Value <> "" Then
            strLocCode = ddlLocCode.SelectedItem.Value
        End If

        If txtEmpCodeFrom.Text = "" Then
            strEmpCodeFrom = ""
        Else
            strEmpCodeFrom = Trim(txtEmpCodeFrom.Text)
        End If

        If txtEmpCodeTo.Text = "" Then
            strEmpCodeTo = ""
        Else
            strEmpCodeTo = Trim(txtEmpCodeTo.Text)
        End If

        strGangCode = Trim(txtGangCode.Text)

        If ddlDeptCode.SelectedItem.Value <> "" Then
            strDeptCode = ddlDeptCode.SelectedItem.Value
        End If

        If ddlStatus.SelectedItem.Value <> "" Then
            strStatus = ddlStatus.SelectedItem.Text
            strStatus_Value = ddlStatus.SelectedItem.Value
        End If

        If ddlPayMode.SelectedItem.Value <> "" Then
            strPayMode = ddlPayMode.SelectedItem.Text
            strPayMode_Value = ddlPayMode.SelectedItem.Value
        End If

        If cblThumbPrint.Items(0).Selected Then
            strThumbPrint = "Yes"
        else
            strThumbPrint = "No"
        End If    

        Response.Write("<Script Language=""JavaScript"">window.open(""PR_StdRpt_WagesPaymentListPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & "&lblLocation=" & lblLocation.Text & _
                       "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & _
                       "&RptName=" & strRptName & "&Decimal=" & strDec & _
                       "&CompCode=" & strCompCode & "&lblCompany=" & lblCompCode.Text & _
                       "&LocCode=" & strLocCode & "&EmpCodeFrom=" & strEmpCodeFrom & _
                       "&EmpCodeTo=" & strEmpCodeTo & "&GangCode=" & strGangCode & _
                       "&DeptCode=" & strDeptCode & "&lblDepartment=" & lblDeptCode.Text & _
                       "&Status=" & strStatus & "&Status_Value=" & strStatus_Value & _
                       "&PayMode=" & strPayMode & "&PayMode_Value=" & strPayMode_Value & _ 
                       "&ThumbPrint=" & strThumbPrint & _ 
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

End Class
