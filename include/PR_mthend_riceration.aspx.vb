Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient


Imports agri.HR
Imports agri.PR
Imports agri.GlobalHdl
Imports agri.PWSystem

Public Class PR_mthend_riceration : Inherits Page

    Protected WithEvents lblTitle As Label
    Protected WithEvents lblRiceRation As Label
    Protected WithEvents rbAllEmp As RadioButton
    Protected WithEvents rbSelectedEmp As RadioButton
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lblCompleteSetup As Label

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblSuccess As Label
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblFailed As Label

    Dim objHR As New agri.HR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPR As New agri.PR.clsMthEnd()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim objEmpDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")
	    
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthRice), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblNoRecord.Visible = False
            lblSuccess.Visible = False
            lblFailed.Visible = False
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindEmployee()
            End If
        End If
    End Sub

    Sub BindEmployee()
        Dim strOpCdGet As String = "PR_CLSMTHEND_GET_EMPLOYEELIST"
        Dim strParam As String = "|" & "and e.Status = '" & objHR.EnumEmpStatus.Active & "' and e.LocCode = '" & strLocation & "' "
        Dim intErrNo As Integer

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_PROCESS_EMP_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        ddlEmployee.DataSource = objEmpDs.Tables(0)
        ddlEmployee.DataValueField = "EmpCode"
        ddlEmployee.DataTextField = "_Description"
        ddlEmployee.DataBind()
    End Sub

    Sub Check_Clicked(Sender As Object, E As EventArgs)
        If rbSelectedEmp.Checked Then
            ddlEmployee.Enabled = True
        Else
            ddlEmployee.Enabled = False
        End If
    End Sub

    Sub btnGenerate_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCdSP As String = "PR_MTHEND_RICERATION_PROCESS_SP"
        Dim objResult As Object
        Dim objDataSet As Object
        Dim intErrNo As Integer
        Dim strParam As String = ""
        
        If rbSelectedEmp.Checked Then 
            strParam = ddlEmployee.SelectedItem.Value & "|"
        End If
        
        Try
            intErrNo = objPR.mtdProcessRiceRation(strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strOpCdSP, _
                                                  strParam, _
                                                  objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_RICERATION_GENERATE&errmesg=&redirect=")
        End Try

        If objResult = 0 Then
            lblNoRecord.Visible = True
        ElseIf objResult = 1 Then
            lblSuccess.Visible = True
        Else
            lblFailed.Visible = True
        End If
                
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.RiceRation))
        lblRiceRation.Text = GetCaption(objLangCap.EnumLangCap.RiceRation)
        lblNoRecord.Text = lblRiceRation.Text & " is not updated. This could be no active employee or no employee is entitled with any " & lblRiceRation.Text & " Code."
        lblSuccess.Text = lblRiceRation.Text & " is updated successfully."
        lblFailed.Text = "Error while updating " & lblRiceRation.Text & ". Kindly contact system administrator for assistance."
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 "", _
                                                 "", _
                                                 strUserId, _
                                                 "", _
                                                 "", _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=MENU_PRMTHEND_RICERATION_LANGCAP&errmesg=&redirect=")
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
