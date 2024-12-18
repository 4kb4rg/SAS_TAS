Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap

Public Class HR_EmployeeCPList : Inherits Page

    Protected WithEvents dgEmpCPList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblEmpName As Label
    Protected WithEvents lblCPID As Label
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblEmpStatus As Label
    Protected WithEvents NewEmpCPBtn As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents lblTitle As Label

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbDetails As LinkButton
    Protected WithEvents lbPayroll As LinkButton
    Protected WithEvents lbEmployment As LinkButton
    Protected WithEvents lbStatutory As LinkButton
    Protected WithEvents lbFamily As LinkButton
    Protected WithEvents lbQualific As LinkButton
    Protected WithEvents lbSkill As LinkButton
    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim objEmpDs As New Object()
    Dim objLangCapDs AS New Object()

    Dim strSelectedEmpCode As String = ""
    Dim strSelectedEmpName As String = ""
    Dim strSelectedCPID As String = ""
    Dim strSelectedEmpStatus As String = ""
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCareerProgress), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblRedirect.Text = Request.QueryString("redirect")
            If SortExpression.Text = "" Then
                SortExpression.Text = "CPID"
                SortCol.Text = "DESC"
            End If

            strSelectedEmpCode = Trim(IIf(Request.QueryString("EmpCode") <> "", Request.QueryString("EmpCode"), Request.Form("EmpCode")))
            strSelectedEmpName = Trim(IIf(Request.QueryString("EmpName") <> "", Request.QueryString("EmpName"), Request.Form("EmpName")))
            strSelectedCPID = Trim(IIf(Request.QueryString("CPID") <> "", Request.QueryString("CPID"), Request.Form("CPID")))
            strSelectedEmpStatus = Trim(IIf(Request.QueryString("EmpStatus") <> "", Request.QueryString("EmpStatus"), Request.Form("EmpStatus")))
            
            If Not Page.IsPostBack Then
                BindGrid() 
                BindPageList()
                onLoad_BindButton()
                onload_LinkButton()     
            End If
        End If
    End Sub

    Sub onload_LinkButton()
        If lblEmpStatus.Text = 0 Then
            TrLink.Visible = False
        Else
            TrLink.Visible = True
        End If
    End Sub

    Private Sub lbDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDetails.Click
        Response.Redirect("HR_trx_EmployeeDet.aspx?redirect=empdet&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbPayroll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbPayroll.Click
        Response.Redirect("HR_trx_EmployeePay.aspx?redirect=emppay&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbEmployment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEmployment.Click
        Response.Redirect("HR_trx_EmployeeEmp.aspx?redirect=empemp&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbStatutory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbStatutory.Click
        Response.Redirect("HR_trx_EmployeeStat.aspx?redirect=empstat&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFamily.Click
        Response.Redirect("HR_trx_EmployeeFamList.aspx?redirect=empfam&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbQualific_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbQualific.Click
        Response.Redirect("HR_trx_EmployeeQlfList.aspx?redirect=empqlf&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbSkill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSkill.Click
        Response.Redirect("HR_trx_EmployeeSkillList.aspx?redirect=empskill&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Sub onLoad_BindButton()
        NewEmpCPBtn.visible = False
        btnBack.visible = False

        Select Case CInt(strSelectedEmpStatus)
            Case objHR.EnumEmpStatus.Active
                NewEmpCPBtn.visible = True
                btnBack.visible = True 
            Case objHR.EnumEmpStatus.Pending, objHR.EnumEmpStatus.Deleted
                btnBack.visible = True
        End Select
    End Sub

    Sub BindGrid() 
        Dim strOpCd_Get As String = "HR_CLSTRX_CAREERPROGRESS_GET"
        Dim strParam as string
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim PageNo as Integer
        Dim lbButton As LinkButton

        lblEmpCode.Text = strSelectedEmpCode
        lblEmpName.Text = strSelectedEmpName
        lblEmpStatus.Text = strSelectedEmpStatus

        strParam = "|" & strSelectedEmpCode & "|||" & SortExpression.Text & "|" & SortCol.Text

        Try
            intErrNo = objHR.mtdGetEmployeeCP(strOpCd_Get, strParam, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_GET_CP&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpName"))
            objEmpDs.Tables(0).Rows(intCnt).Item("CPID") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("CPID"))
            objEmpDs.Tables(0).Rows(intCnt).Item("CPCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("CPCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("CPDesc") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("CPCode")) & " (" & _
                                                             Trim(objEmpDs.Tables(0).Rows(intCnt).Item("CPDesc")) & ")"
            objEmpDs.Tables(0).Rows(intCnt).Item("SalSchemeCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("SalSchemeCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("Remark") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("Remark"))
            objEmpDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("LocCode")) & " (" & _
                                                                 Trim(objEmpDs.Tables(0).Rows(intCnt).Item("LocDesc")) & ")"
            objEmpDs.Tables(0).Rows(intCnt).Item("EvalCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EvalCode")) & " (" & _
                                                                 Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EvalDesc")) & ")"
        Next 

        dgEmpCPList.DataSource = objEmpDs
        dgEmpCPList.DataBind()

        PageNo = dgEmpCPList.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgEmpCPList.PageCount
    End Sub 

    Sub BindPageList() 
        Dim count as integer = 1   
        Dim arrDList As New ArrayList()

        While not count = dgEmpCPList.PageCount + 1
            arrDList.Add("Page " & count)
            Count = Count + 1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgEmpCPList.CurrentPageIndex
    End Sub 

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                dgEmpCPList.CurrentPageIndex = 0
            Case "prev"
                dgEmpCPList.CurrentPageIndex = _
                Math.Max(0, dgEmpCPList.CurrentPageIndex - 1)
            Case "next"
                dgEmpCPList.CurrentPageIndex = _
                Math.Min(dgEmpCPList.PageCount - 1, dgEmpCPList.CurrentPageIndex + 1)
            Case "last"
                dgEmpCPList.CurrentPageIndex = dgEmpCPList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgEmpCPList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            dgEmpCPList.CurrentPageIndex = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        dgEmpCPList.CurrentPageIndex=e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgEmpCPList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub
 
    Sub DEDR_CPDet(Sender As Object, E As DataGridCommandEventArgs)
        Dim lblCodeValue As Label
        Dim lblNameValue As Label
        Dim lblCPIDValue As Label
        Dim intIndex As Integer = E.Item.ItemIndex    
        Dim strEmpCode As String
        Dim strEmpName As String
        Dim strCPID As String
        Dim strEmpStatus As String
        Dim strLink As String

        lblCodeValue = dgEmpCPList.Items.Item(intIndex).FindControl("lblEmpCode")
        lblNameValue = dgEmpCPList.Items.Item(intIndex).FindControl("lblEmpName")
        lblCPIDValue = dgEmpCPList.Items.Item(intIndex).FindControl("lblCPID")
        strEmpCode = lblCodeValue.Text
        strEmpName = lblNameValue.Text
        strCPID = lblCPIDValue.Text
        strEmpStatus = lblEmpStatus.Text

        strLink = "HR_trx_EmployeeCPDet.aspx?redirect=empcp&EmpCode=" & strEmpCode & "&EmpName=" & strEmpName & "&CPID=" & strCPID & "&EmpStatus=" & strEmpStatus

        Response.Redirect(strLink)
    End Sub

    Sub NewEmpCPBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeCPDet.aspx?redirect=" & lblRedirect.Text & "&new=yes&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.text)
    End Sub

    Sub btnBack_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeList.aspx?redirect=" & lblRedirect.Text)
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.CareerProgress))
        dgEmpCPList.Columns(1).HeaderText = GetCaption(objLangCap.EnumLangCap.CareerProgress)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
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
