Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.HR
Imports agri.GL
Imports agri.GlobalHdl.clsGlobalHdl

Public Class HR_EmployeeList : Inherits Page

    Protected WithEvents dgEmpList As DataGrid
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents ddlEmpType As DropDownList
    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlEmpJob As DropDownList

    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents NewEmpBtn As ImageButton
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label

    Protected objHR As New agri.HR.clsTrx()
    Protected objOk As New agri.GL.ClsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
	Dim strAccMonth As String
    Dim strAccYear As String
    Dim blnCanDelete As Boolean = False
	Dim intLevel As Integer

    Dim objEmpDs As New Object()
    Dim objEmpTypeDs As New Object()
    Dim objEmpDivDs As New Object()
    Dim objEmpJobDs As New Object()


    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")
		strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
		intLevel = Session("SS_USRLEVEL")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblRedirect.Text = Request.QueryString("redirect")
            If SortExpression.Text = "" Then
                SortExpression.Text = "EmpCode"
            End If

            SwitchMode(lblRedirect.Text)
			
            If Not Page.IsPostBack Then
                BindEmpDiv()
                BindEmpType()
                BindEmpJob()
                If Session("HRE") <> "" Then
                    Dim prevset As String
                    Dim ary As Array
                    prevset = Session("HRE")
                    ary = Split(prevset, "|")
                    txtEmpName.Text = ary(0)
                    ddlEmpDiv.SelectedValue = ary(1)
                    ddlEmpType.SelectedValue = ary(2)
                    lblCurrentIndex.Text = ary(3)
                End If
                BindGrid()
            End If
        End If
    End Sub

    Sub SwitchMode(ByVal pv_strQuery As String)
        If (pv_strQuery = "empdet" and intLevel >= 2) Then
            blnCanDelete = True
        End If
    End Sub

    Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        dgEmpList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub BindGrid() 
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
		
		      
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgEmpList.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If

        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsData = LoadData
            End If
        End If
        
        dgEmpList.DataSource = dsData
        dgEmpList.DataBind()
        lblPageCount.Text = PageCount
        BindPageList(PageCount)
        PageNo = lblCurrentIndex.Text + 1


        If blnCanDelete = True Then
            For intCnt = 0 To dgEmpList.Items.Count - 1
                Select Case CInt(objEmpDs.Tables(0).Rows(intCnt).Item("Status"))
                    Case objHR.EnumEmpStatus.Pending
                        lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Text = "Delete"
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objHR.EnumEmpStatus.Active
                        lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                        lbButton.Text = "Delete"
                    Case objHR.EnumEmpStatus.Deleted
                        lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Text = "UnDelete"
                        lbButton.Visible = False
                End Select
            Next
        Else
            For intCnt = 0 To dgEmpList.Items.Count - 1
                lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
                lbButton.Visible = False
            Next
			
        End If
      ' NewEmpBtn.Visible = blnCanDelete

    End Sub 

    Sub BindEmpJob()
        Dim strOpCd_EmpJob As String = "HR_HR_STP_GROUPJOB_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
		
        strParamName = "SEARCH|SORT"
        strParamValue = "|"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpJob, strParamName, strParamValue, objEmpJobDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_GROUPJOB_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objEmpJobDs.Tables(0).Rows.Count > 0 Then
		  For intCnt = 0 To objEmpJobDs.Tables(0).Rows.Count - 1          
   			objEmpJobDs.Tables(0).Rows(intCnt).Item("GrpJobCode") = Trim(objEmpJobDs.Tables(0).Rows(intCnt).Item("GrpJobCode"))
            objEmpJobDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpJobDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objEmpJobDs.Tables(0).NewRow()
        dr("GrpJobCode") = "0"
        dr("Description") = "All"
        objEmpJobDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpJob.DataSource = objEmpJobDs.Tables(0)
        ddlEmpJob.DataTextField = "Description"
        ddlEmpJob.DataValueField = "GrpJobCode"
        ddlEmpJob.DataBind()
        ddlEmpJob.SelectedIndex = 0

    End Sub

    Sub BindEmpType()
        Dim strOpCd_EmpType As String = "HR_HR_STP_EMPTYPE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow

        strParamName = "SEARCH|SORT"
        strParamValue = "|"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpType, strParamName, strParamValue, objEmpTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_EMPTYPE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objEmpTypeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpTypeDs.Tables(0).Rows.Count - 1
                objEmpTypeDs.Tables(0).Rows(intCnt).Item("EmpTyCode") = Trim(objEmpTypeDs.Tables(0).Rows(intCnt).Item("EmpTyCode"))
                objEmpTypeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpTypeDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objEmpTypeDs.Tables(0).NewRow()
        dr("EmpTyCode") = "0"
        dr("Description") = "All"
        objEmpTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpType.DataSource = objEmpTypeDs.Tables(0)
        ddlEmpType.DataTextField = "Description"
        ddlEmpType.DataValueField = "EmpTyCode"
        ddlEmpType.DataBind()
        ddlEmpType.SelectedIndex = 0

    End Sub

    Sub BindEmpDiv()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = "0"
        dr("Description") = "All Divisi"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpDiv.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDiv.DataTextField = "Description"
        ddlEmpDiv.DataValueField = "BlkGrpCode"
        ddlEmpDiv.DataBind()
        ddlEmpDiv.SelectedIndex = 0

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
		Dim strOpCd_Get2 As String = "HR_HR_TRX_EMPLOYEE_GEN"
		
        Dim strSrchEmpCode as string
        Dim strSrchEmpName as string
        Dim strSrchEmpDiv As String
        Dim strSrchEmpType As String
        Dim strSrchEmpJob As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strSortExpression as string
		Dim Strdate2 AS String 
		Dim strsrc As String
		Dim SAccMonth As String 
		
		If Cint(strAccMonth) < 10 Then
            Strdate2 = strAccyear & "0" & rtrim(strAccMonth)
			SAccMonth = "0" & rtrim(strAccMonth)
        Else
            Strdate2 = strAccyear & rtrim(strAccMonth)
			SAccMonth = rtrim(strAccMonth)
        End If
		
		strParamName = "AM|AY|LOC"
        strParamValue = SAccMonth & "|" & _
						strAccyear & "|" & _
						strLocation
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_Get2, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_GEN &errmesg=" & Exp.Message )
        End Try
		
        Session("SS_PAGING") = lblCurrentIndex.Text
        strSrchEmpCode = IIf(txtEmpCode.Text = "", "", txtEmpCode.Text)
        strSrchEmpName = IIf(txtEmpName.Text = "", "", txtEmpName.Text)
        strSrchEmpDiv = IIf(ddlEmpDiv.SelectedItem.Value = "0", "", ddlEmpDiv.SelectedItem.Value)
        strSrchEmpType = IIf(ddlEmpType.SelectedItem.Value = "0", "", ddlEmpType.SelectedItem.Value)
        strSrchEmpJob = IIf(ddlEmpJob.SelectedItem.Value = "0", "", ddlEmpJob.SelectedItem.Value)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)

        strSortExpression = "C.IDDiv," & SortExpression.Text & ",JobCode"

		
		strsrc = " AND  A.EmpCode Not in (Select empcode from HR_TRX_EMPRESIGN where status='1' and " & Strdate2 & " > convert(char(6),efektifdate,112)) "

        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & _
		                SAccMonth & "|" & _
						strAccyear & "|" & _
                        "AND A.EmpCode Like '%" & strSrchEmpCode & "%' " & _
                        "AND A.EmpName Like '%" & strSrchEmpName & "%' " & _
                        "AND C.IDDiv Like '%" & strSrchEmpDiv & "%'" & _
                        "AND C.CodeEmpty Like '%" & strSrchEmpType & "%'" & _
                        "AND D.JobCode Like '%" & strSrchEmpJob & "%'" & _
                        "AND A.Status Like '%" & strSrchStatus & "%'" & _
                        "AND A.LocCode Like '%" & strLocation & "%'" & _
						strsrc & _
                        "AND B.UserName Like '%" & strSrchLastUpdate & "%'|" & _
                        "ORDER BY " & strSortExpression

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpName"))
            objEmpDs.Tables(0).Rows(intCnt).Item("IDDiv") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("IDDiv"))
            objEmpDs.Tables(0).Rows(intCnt).Item("CodeEmpty") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("CodeEmpty"))
            objEmpDs.Tables(0).Rows(intCnt).Item("JobCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("JobCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objEmpDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("Status"))
            objEmpDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("UserName"))
        Next

        Return objEmpDs
    End Function

    Sub EmpLink_Click(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If Not E.CommandName.ToString = "Sort" And Not E.CommandName.ToString = "Delete" Then
            Dim lbl As Label            
            Dim intIndex As Integer = E.Item.ItemIndex
            Dim strEmpCode As String
            Dim strEmpName As String
            Dim strEmpStatus As String
            Dim strLink As String

            lbl = dgEmpList.Items.Item(intIndex).FindControl("lblEmpCode")
            strEmpCode = lbl.Text
            lbl = dgEmpList.Items.Item(intIndex).FindControl("lblEmpName")
            strEmpName = lbl.Text
            lbl = dgEmpList.Items.Item(intIndex).FindControl("lblEmpStatus")
            strEmpStatus = lbl.Text

            Session("HRE") = txtEmpName.Text.Trim() & "|" & _
                             ddlEmpDiv.SelectedItem.Value.Trim() & "|" & _
                             ddlEmpType.SelectedItem.Value.Trim() & "|" & _
                             lblCurrentIndex.Text


            Response.Redirect("HR_trx_EmployeeDet_Estate.aspx?EmpCode=" & strEmpCode)
        End If

    End Sub

    Sub BindPageList(ByVal cnt As String)
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count & " of " & cnt)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                lblCurrentIndex.Text = 0
            Case "prev"
                lblCurrentIndex.Text = _
                Math.Max(0, lblCurrentIndex.Text - 1)
            Case "next"
                lblCurrentIndex.Text = _
                Math.Min(lblPageCount.Text - 1, lblCurrentIndex.Text + 1)
            Case "last"
                lblCurrentIndex.Text = lblPageCount.Text - 1
        End Select
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Status As String = "HR_HR_TRX_EMPLOYEE_STATUS_UPD"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strValue As String
        Dim lblEmpCode As Label
        Dim strEmpCode As String

        dgEmpList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblEmpCode = dgEmpList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblEmpCode")
        strEmpCode = lblEmpCode.Text

        strParam = "STATUS|UPDATEID|UPDATEDATE|EMPCODE|LOCCODE"
        strValue = "2|" & strUserId & "|" & Now() & "|" & strEmpCode & "|" & strLocation
        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Status, strParam, strValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_STATUS_UPD&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        dgEmpList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewEmpBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeDet_Estate.aspx?redirect=empdet")
    End Sub

    Sub dgLine_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub
End Class
