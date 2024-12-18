Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Text
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class WS_TRX_JOB_LIST : Inherits Page
    Protected WithEvents dgJob As DataGrid

    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblOrderBy As Label
    Protected WithEvents lblOrderDir As Label
    Protected WithEvents lblActionResult As Label
    Protected WithEvents lblSearchVehCode As Label
    Protected WithEvents lblSearchJobStartDateErr As Label

    Protected WithEvents ddlPage As DropDownList
    Protected WithEvents ddlSearchJobType As DropDownList
    Protected WithEvents ddlSearchStatus As DropDownList

    Protected WithEvents txtSearchJobID As TextBox
    Protected WithEvents txtSearchVehCode As TextBox
    Protected WithEvents txtSearchJobStartDate As TextBox
    Protected WithEvents txtSearchUpdateBy As TextBox

    Protected objWSTrx As New agri.WS.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim strOpCode_Job_Get As String = "WS_CLSTRX_JOB_GET"
    Dim strOpCode_Job_Upd As String = "WS_CLSTRX_JOB_UPD"

    Dim intErrNo As Integer
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim strLocType As String


    Dim dsLangCap As New DataSet()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If lblOrderBy.Text = "" Then
                lblOrderBy.Text = "JobID"
            End If
            If lblOrderDir.Text = "" Then
                lblOrderDir.Text = "ASC"
            End If

            lblActionResult.Visible = False

            If Not Page.IsPostBack Then
                GetLangCap()
                BindSearchJobTypeDropDownList()
                BindSearchStatusDropDownList()
                BindDataGrid()
            End If
        End If
    End Sub

    Sub GetLangCap()
        Dim strVehCode As String

        dsLangCap = GetLanguageCaptionDS()
        strVehCode = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code"

        lblSearchVehCode.Text = strVehCode
        dgJob.Columns(1).HeaderText = strVehCode
    End Sub



    Function GetCaption(ByVal pv_TermCode As String) As String
        Dim I As Integer

        For I = 0 To dsLangCap.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(dsLangCap.Tables(0).Rows(I).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill Then
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTermMW"))
                Else
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub BindSearchJobTypeDropDownList()
        ddlSearchJobType.Items.Clear()
        ddlSearchJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.All), objWSTrx.EnumJobType.All))
        ddlSearchJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.InternalUse), objWSTrx.EnumJobType.InternalUse))
        ddlSearchJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.StaffPayroll), objWSTrx.EnumJobType.StaffPayroll))
        ddlSearchJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.StaffDebitNote), objWSTrx.EnumJobType.StaffDebitNote))
        ddlSearchJobType.Items.Add(New ListItem(objWSTrx.mtdGetJobType(objWSTrx.EnumJobType.ExternalParty), objWSTrx.EnumJobType.ExternalParty))
        ddlSearchJobType.SelectedIndex = 0
    End Sub

    Sub BindSearchStatusDropDownList()
        ddlSearchStatus.Items.Clear()
        ddlSearchStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.All), objWSTrx.EnumJobStatus.All))
        ddlSearchStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Active), objWSTrx.EnumJobStatus.Active))
        ddlSearchStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Deleted), objWSTrx.EnumJobStatus.Deleted))
        ddlSearchStatus.Items.Add(New ListItem(objWSTrx.mtdGetJobStatus(objWSTrx.EnumJobStatus.Closed), objWSTrx.EnumJobStatus.Closed))
        ddlSearchStatus.SelectedIndex = 1
    End Sub

    Sub BindPageDropDownList()
        Dim intCnt As Integer = 1
        Dim arrPageNo As New ArrayList()

        While Not intCnt = dgJob.PageCount + 1
            arrPageNo.Add("Page " & intCnt)
            intCnt = intCnt + 1
        End While
        ddlPage.DataSource = arrPageNo
        ddlPage.DataBind()
        ddlPage.SelectedIndex = dgJob.CurrentPageIndex
    End Sub

    Sub ddlPage_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgJob.CurrentPageIndex = ddlPage.SelectedIndex
            BindDataGrid()
        End If
    End Sub

    Protected Function GetJobListDS() As DataSet
        Dim colParam As New Collection
        Dim dsTemp As New DataSet
        Dim sbTemp As New StringBuilder
        Dim strErrMsg As String
        Dim strJobStartDate As String

        sbTemp.Append("WHERE J.LocCode = '" & FixSQL(strLocation) & "' " & vbCrLf)
        sbTemp.Append("  AND ((J.AccMonth = '" & FixSQL(strAccMonth) & "' AND J.AccYear = '" & FixSQL(strAccYear) & "') OR (J.Status = '" & objWSTrx.EnumJobStatus.Active & "')) " & vbCrLf)

        If Trim(txtSearchJobID.Text) <> "" Then
            sbTemp.Append("  AND J.JobID LIKE '" & FixSQL(Trim(txtSearchJobID.Text)) & "%' " & vbCrLf)
        End If

        If Trim(txtSearchVehCode.Text) <> "" Then
            sbTemp.Append("  AND J.VehCode LIKE '" & FixSQL(Trim(txtSearchVehCode.Text)) & "%' " & vbCrLf)
        End If

        If Trim(txtSearchJobStartDate.Text) <> "" Then
            strJobStartDate = GetValidDate(Trim(txtSearchJobStartDate.Text), strErrMsg)
            If strJobStartDate <> "" Then
                sbTemp.Append("  AND DateDiff(day, J.JobStartDate, '" & FixSQL(strJobStartDate) & "') = 0 " & vbCrLf)
                lblSearchJobStartDateErr.Text = ""
            Else
                lblSearchJobStartDateErr.Text = strErrMsg
            End If
        Else
            lblSearchJobStartDateErr.Text = ""
        End If

        If GetDropDownListValue(ddlSearchJobType) <> objWSTrx.EnumJobType.All Then
            sbTemp.Append("  AND J.JobType = '" & FixSQL(GetDropDownListValue(ddlSearchJobType)) & "' " & vbCrLf)
        End If

        If GetDropDownListValue(ddlSearchStatus) <> objWSTrx.EnumJobStatus.All Then
            sbTemp.Append("  AND J.Status = '" & FixSQL(GetDropDownListValue(ddlSearchStatus)) & "' " & vbCrLf)
        End If

        If Trim(txtSearchUpdateBy.Text) <> "" Then
            sbTemp.Append("  AND USR.UserName LIKE '" & FixSQL(Trim(txtSearchUpdateBy.Text)) & "%' " & vbCrLf)
        End If

        sbTemp.Append("ORDER BY " & Trim(lblOrderBy.Text) & " " & Trim(lblOrderDir.Text))

        colParam.Add(sbTemp.ToString, "PM_SEARCH")
        colParam.Add(strOpCode_Job_Get, "OC_JOB_GET")

        Try
            intErrNo = objWSTrx.mtdJob_Get(colParam, dsTemp, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If
            Return dsTemp
        Catch ex As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_LIST_GET_JOB_LIST&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=")
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
    End Function

    Function GetLanguageCaptionDS() As DataSet
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim dsTemp As DataSet
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
                                                 dsTemp, _
                                                 strParam)
            Return dsTemp
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_LIST_GET_LANGCAP&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=")
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
    End Function

    Sub BindDataGrid()
        Dim intPageNo As Integer
        Dim intPageCount As Integer
        Dim dsData As DataSet

        dsData = GetJobListDS()
        intPageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgJob.PageSize)

        dgJob.DataSource = dsData
        If dgJob.CurrentPageIndex >= intPageCount Then
            If intPageCount = 0 Then
                dgJob.CurrentPageIndex = 0
            Else
                dgJob.CurrentPageIndex = intPageCount - 1
            End If
        End If

        dgJob.DataBind()
        BindPageDropDownList()
        intPageNo = dgJob.CurrentPageIndex + 1
        lblTracker.Text = "Page " & intPageNo & " of " & dgJob.PageCount
    End Sub

    Sub dgJob_OnItemDataBound(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim lblStatus As Label
        Dim lblJobStockCount As Label
        Dim lblMechHourCount As Label
        Dim btnDelete As LinkButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            lblStatus = e.Item.FindControl("lblStatus")
            lblJobStockCount = e.Item.FindControl("lblJobStockCount")
            lblMechHourCount = e.Item.FindControl("lblMechHourCount")
            btnDelete = e.Item.FindControl("lbDelete")

            Select Case Trim(lblStatus.Text)
                Case Trim(CStr(objWSTrx.EnumJobStatus.Active))
                    If CInt(Trim(lblJobStockCount.Text)) <> 0 Or CInt(Trim(lblMechHourCount.Text)) <> 0 Then
                        btnDelete.Visible = False
                    Else
                        btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                        btnDelete.Text = "Delete"
                        btnDelete.Visible = True
                    End If
                Case Trim(CStr(objWSTrx.EnumJobStatus.Deleted))
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                    btnDelete.Text = "Undelete"
                    btnDelete.Visible = True
                Case Else
                    btnDelete.Visible = False
            End Select
        End If
    End Sub

    Sub btnSearch_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        dgJob.CurrentPageIndex = 0
        dgJob.EditItemIndex = -1
        BindDataGrid()
        BindPageDropDownList()
    End Sub

    Sub ibPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgJob.CurrentPageIndex = 0
            Case "prev"
                dgJob.CurrentPageIndex = Math.Max(0, dgJob.CurrentPageIndex - 1)
            Case "next"
                dgJob.CurrentPageIndex = Math.Min(dgJob.PageCount - 1, dgJob.CurrentPageIndex + 1)
            Case "last"
                dgJob.CurrentPageIndex = dgJob.PageCount - 1
        End Select
        ddlPage.SelectedIndex = dgJob.CurrentPageIndex
        BindDataGrid()
    End Sub

    Sub ibNewInternalJob_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("ws_trx_job_detail.aspx?jobtype=" & Server.UrlEncode(objWSTrx.EnumJobType.InternalUse))
    End Sub

    Sub ibNewStaffJob_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("ws_trx_job_detail.aspx?jobtype=" & Server.UrlEncode(objWSTrx.EnumJobType.StaffPayroll))
    End Sub

    Sub ibNewExternalPartyJob_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("ws_trx_job_detail.aspx?jobtype=" & Server.UrlEncode(objWSTrx.EnumJobType.ExternalParty))
    End Sub

    Sub dgJob_OnPageIndexChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgJob.CurrentPageIndex = e.NewPageIndex
        BindDataGrid()
    End Sub

    Sub dgJob_OnSortCommand(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        lblOrderBy.Text = e.SortExpression.ToString()
        lblOrderDir.Text = IIf(lblOrderDir.Text = "ASC", "DESC", "ASC")
        dgJob.CurrentPageIndex = ddlPage.SelectedIndex
        BindDataGrid()
    End Sub

    Sub dgJob_OnDeleteCommand(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim colParam As New Collection
        Dim lblTemp As Label
        Dim strErrMsg As String

        lblTemp = E.Item.FindControl("lblJobID")
        colParam.Add(Trim(lblTemp.Text), "PM_JOBID")

        lblTemp = E.Item.FindControl("lblStatus")
        colParam.Add(strUserId, "PM_UPDATEID")
        colParam.Add(strOpCode_Job_Get, "OC_JOB_GET")
        colParam.Add(strOpCode_Job_Upd, "OC_JOB_UPD")
        Try
            If Trim(lblTemp.Text) = Trim(CStr(objWSTrx.EnumJobStatus.Active)) Then
                intErrNo = objWSTrx.mtdJob_Delete(colParam, strErrMsg)
            Else
                intErrNo = objWSTrx.mtdJob_Undelete(colParam, strErrMsg)
            End If

            If intErrNo <> objWSTrx.EnumException.NoError Then
                lblActionResult.Text = "Cannot perform the operation. " & strErrMsg
                lblActionResult.Visible = True
            End If
        Catch ex As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_LIST_DELETE_UNDELETE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=")
        End Try
        BindDataGrid()
    End Sub

    Protected Function FixSQL(ByVal pv_strParam As String) As String
        Return Replace(Trim(pv_strParam), "'", "''")
    End Function

    Protected Function RoundNumber(ByVal d As Double, Optional ByVal decimals As Double = 0) As Decimal
        RoundNumber = Decimal.Divide(Int(Decimal.Add(Decimal.Multiply(d, 10.0 ^ decimals), 0.5)), 10.0 ^ decimals)
    End Function

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
