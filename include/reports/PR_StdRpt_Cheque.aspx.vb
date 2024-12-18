
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
Imports Microsoft.VisualBasic.FileSystem

Public Class PR_StdRpt_Cheque : Inherits Page

    Protected RptSelect As UserControl

    Dim objPR As New agri.PR.clsReport()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents ddlBankCode As DropDownList
    Protected WithEvents txtProgramPath As TextBox
    Protected WithEvents ddlCompCode As DropDownList
    Protected WithEvents ddlDeptCode As DropDownList
    Protected WithEvents txtFromEmp As Textbox
    Protected WithEvents txtToEmp As TextBox
    Protected WithEvents txtTerminateDateFr As TextBox
    Protected WithEvents txtTerminateDateTo As TextBox
    Protected WithEvents btnSelDateFr As Image
    Protected WithEvents btnSelDateTo As Image
    Protected WithEvents txtStartChequeNo As TextBox
    Protected WithEvents txtNoOfCheque As TextBox

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblErrChequeFormat As Label
    Protected WithEvents lblErrTerminateDateFr As Label
    Protected WithEvents lblErrTerminateDateTo As Label
    Protected WithEvents lblErrBankCode As Label

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strUserLoc As String
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Dim strSelectedBankCode As String

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
                BindBankCode("")
                BindCompCode()
                BindDeptCode()
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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


    Sub BindBankCode(ByVal pv_strSelectedBankCode As Object)
        Dim dsForBankCodeDropDown As New DataSet()
        Dim strOpCd As String = "PR_STDRPT_BANK_GET_CHEQUEFMT"
        Dim strParam As String 
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer

        strParam = objHRSetup.EnumBankStatus.Active & "|" & _
                   objHRSetup.EnumBankFormatStatus.Active & "|" & _
                   objHRSetup.EnumBankFormatType.Cheque & "|"
        Try
            intErrNo = objPR.mtdGetBank(strOpCd, strParam, dsForBankCodeDropDown)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GET_PR_BANKCODE&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/PR_StdRpt_Selection.aspx")
        End Try

        If Not dsForBankCodeDropDown.Tables(0).Rows.Count > 0 Then
            lblErrBankCode.Visible = True
            ddlCompCode.Enabled = False
            ddlDeptCode.Enabled = False
            txtFromEmp.Enabled = False
            btnSelDateFr.Enabled = False
            txtToEmp.Enabled = False
            btnSelDateTo.Enabled = False
            txtTerminateDateFr.Enabled = False
            txtTerminateDateTo.Enabled = False
            txtStartChequeNo.Enabled = False
            txtNoOfCheque.Enabled = False
            PrintPrev.Enabled = False
            Exit Sub
        End If
        
        If dsForBankCodeDropDown.Tables(0).Rows.Count > 0 Then
            txtProgramPath.Text = dsForBankCodeDropDown.Tables(0).Rows(0).Item("ProgramPath").Trim()
            For intCnt = 0 To dsForBankCodeDropDown.Tables(0).Rows.Count - 1
                dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("BankCode") = dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("BankCode").Trim()
                dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("BankCode").Trim() & " (" & dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"

                If dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("BankCode") = pv_strSelectedBankCode Then
                    txtProgramPath.Text = dsForBankCodeDropDown.Tables(0).Rows(intCnt).Item("ProgramPath").Trim()
                    intSelectedIndex = intCnt
                End If
            Next intCnt
            
            ddlBankCode.DataSource = dsForBankCodeDropDown.Tables(0)
            ddlBankCode.DataValueField = "BankCode"
            ddlBankCode.DataTextField = "Description"
            ddlBankCode.DataBind()
            ddlBankCode.SelectedIndex = intSelectedIndex
        End If
    End Sub

    Sub IndexBankChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedBankCode = ddlBankCode.SelectedItem.Value
        BindBankCode(strSelectedBankCode)
    End Sub

    Sub BindCompCode()
        Dim objCompDs As New DataSet()
        Dim strOpCd_Get As String = "ADMIN_CLSCOMP_COMPANY_LIST_GET"
        Dim strParam As String = ""
        Dim intCnt As Integer
        Dim intErrNo As Integer

        strParam = "||" & objAdminComp.EnumCompanyStatus.Active & "||CompCode|" 

        Try
            intErrNo = objAdminComp.mtdGetComp(strOpCd_Get, strParam, objCompDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_COMPCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/PR_StdRpt_Selection.aspx")
        End Try

        If objCompDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objCompDs.Tables(0).Rows.Count - 1
                objCompDs.Tables(0).Rows(intCnt).Item("CompCode") = objCompDs.Tables(0).Rows(intCnt).Item("CompCode").Trim()
                objCompDs.Tables(0).Rows(intCnt).Item("CompName") = objCompDs.Tables(0).Rows(intCnt).Item("CompCode").Trim() & " (" & objCompDs.Tables(0).Rows(intCnt).Item("CompName").Trim() & ")"
            Next
        End If

        ddlCompCode.DataSource = objCompDs.Tables(0)
        ddlCompCode.DataTextField = "CompName"
        ddlCompCode.DataValueField = "CompCode"
        ddlCompCode.DataBind()
    End Sub

    Sub BindDeptCode()
        Dim dsForDeptCodeDropDown As New DataSet()
        Dim strOpCd As String = "HR_CLSSETUP_DEPT_SEARCH"
        Dim strParam As String 
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim dr As DataRow

        strParam = "|" & objHRSetup.EnumDeptStatus.Active & "||A.DeptCode||" 

        Try
            intErrNo = objHRSetup.mtdGetDept(strOpCd, strParam, dsForDeptCodeDropDown, False)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_DEPTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/PR_StdRpt_Selection.aspx")
        End Try

        If dsForDeptCodeDropDown.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To dsForDeptCodeDropDown.Tables(0).Rows.Count - 1
                dsForDeptCodeDropDown.Tables(0).Rows(intCnt).Item("DeptCode") = dsForDeptCodeDropDown.Tables(0).Rows(intCnt).Item("DeptCode").Trim()
                dsForDeptCodeDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDeptCodeDropDown.Tables(0).Rows(intCnt).Item("Description").Trim()
            Next intCnt

            dr = dsForDeptCodeDropDown.Tables(0).NewRow()
            dr("DeptCode") = ""
            dr("Description") = "All"
            dsForDeptCodeDropDown.Tables(0).Rows.InsertAt(dr, 0)

            ddlDeptCode.DataSource = dsForDeptCodeDropDown.Tables(0)
            ddlDeptCode.DataValueField = "DeptCode"
            ddlDeptCode.DataTextField = "Description"
            ddlDeptCode.DataBind()
        End If
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDdlAccMth As String
        Dim strDdlAccYr As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strBankCode As String
        Dim strCompCode As String
        Dim strDeptCode As String
        Dim strFromEmp As String
        Dim strToEmp As String
        Dim strTerminateDateFr As String
        Dim strTerminateDateTo As String
        Dim strStartChequeNo As String
        Dim strNoOfCheque As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRptName As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs as New Object()
        Dim objDateFormat as New Object()
        Dim objExpiryDate as String
        Dim intErrNo As Integer

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)

        tempRptName = RptSelect.FindControl("lstRptName")
        strRptName = Trim(tempRptName.SelectedItem.Value)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)
        strFromEmp = txtFromEmp.Text
        strToEmp = txtToEmp.Text
        strTerminateDateFr = txtTerminateDateFr.Text
        strTerminateDateTo = txtTerminateDateTo.Text
        strStartChequeNo = txtStartChequeNo.Text
        strNoOfCheque = txtNoOfCheque.Text

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

        strBankCode = ddlBankCode.SelectedItem.Value
        strCompCode = ddlCompCode.SelectedItem.Value

        If ddlDeptCode.SelectedIndex = 0 Then
            strDeptCode = ""
        Else
            strDeptCode = Trim(ddlDeptCode.SelectedItem.Value)
        End If

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PR_DOWNLOADBANK_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strTerminateDateFr = "" And strTerminateDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strTerminateDateFr, objDateFormat, objExpiryDate) = True _
                And objGlobal.mtdValidInputDate(strDateSetting, strTerminateDateTo, objDateFormat, objExpiryDate) = True Then

                Response.Write("<Script Language=""JavaScript"">window.open(""" & txtProgramPath.Text & "?Type=Print&CompName=" & strCompany & _
                            "&Location=" & strUserLoc & "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & _
                            "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & _
                            "&BankCode=" & strBankCode & "&CompCode=" & strCompCode & "&DeptCode=" & strDeptCode & _
                            "&EmpCodeFrom=" & strFromEmp & "&EmpCodeTo=" & strToEmp & _
                            "&TerminateDateFr=" & strTerminateDateFr & "&TerminateDateTo=" & strTerminateDateTo & _
                            "&StartChequeNo=" & strStartChequeNo & "&NoOfCheque=" & strNoOfCheque & _
                            """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblErrTerminateDateFr.Text = lblErrTerminateDateFr.Text & objDateFormat & "."
                lblErrTerminateDateTo.Text = lblErrTerminateDateTo.Text & objDateFormat & "."
                lblErrTerminateDateFr.Visible = True
                lblErrTerminateDateTo.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""" & txtProgramPath.Text & "?Type=Print&CompName=" & strCompany & _
                        "&Location=" & strUserLoc & "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & _
                        "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & _
                        "&BankCode=" & strBankCode & "&CompCode=" & strCompCode & "&DeptCode=" & strDeptCode & _
                        "&EmpCodeFrom=" & strFromEmp & "&EmpCodeTo=" & strToEmp & _
                        "&TerminateDateFr=" & strTerminateDateFr & "&TerminateDateTo=" & strTerminateDateTo & _
                        "&StartChequeNo=" & strStartChequeNo & "&NoOfCheque=" & strNoOfCheque & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

End Class
