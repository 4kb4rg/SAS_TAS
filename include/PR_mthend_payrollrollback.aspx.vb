Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Imports agri.HR
Imports agri.PR
Imports agri.GlobalHdl


Public Class PR_mthend_rollback : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblProcessed As Label
    Protected WithEvents lblErrProcessed As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents rbAllEmp As RadioButton
    Protected WithEvents rbSelectedEmp As RadioButton
    Protected WithEvents ddlEmpCode As DropDownList

    Protected WithEvents btnGenerate As ImageButton

    Dim objHR As New agri.HR.clsTrx()
    Dim objPR As New agri.PR.clsMthEnd()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objEmpDs As New Object()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblPeriod.Text = Trim(strAccMonth) & "/" & Trim(strAccYear)

            If Not Page.IsPostBack Then
                BindEmpCode()
            End If
        End If
    End Sub

    Sub BindEmpCode()
        Dim strOpCd_Get As String = "HR_CLSTRX_EMPLOYEE_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strParam = "|||" & objHR.EnumEmpStatus.Active & "|||EmpCode|"

        Try
            intErrNo = objHR.mtdGetEmployeeDet(strOpCd_Get, strParam, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_GET_EMPLOYEE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objEmpDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
                objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode")) & " (" & _
                                                                  Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
            Next
        End If

        ddlEmpCode.DataSource = objEmpDs.Tables(0)
        ddlEmpCode.DataTextField = "EmpName"
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataBind()
    End Sub

    Sub Check_Clicked(Sender As Object, E As EventArgs)
        If rbSelectedEmp.Checked Then
            ddlEmpCode.Enabled = True
        Else
            ddlEmpCode.Enabled = False
        End If
    End Sub

    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intNoDays As Integer
        Dim objResult As Object
        Dim objDataSet As Object
        Dim strOpCd_SP As String = "PR_CLSMTHEND_PAYROLLROLLBACK_SP"

        If rbSelectedEmp.Checked Then strParam = ddlEmpCode.SelectedItem.Value

        strParam = strParam & "|" & strOpCd_SP

        Try
            intErrNo = objPR.mtdPayrollRollBack_SP(strCompany, _
                                                   strLocation, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strUserId, _
                                                   strParam, _
                                                   objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PAYROLL_ROLLBACK&errmesg=&redirect=")
        End Try

        If objResult = 1 Then 
            lblProcessed.Visible = True
        Else
            lblErrProcessed.Visible = True
        End If        
    End Sub

End Class
