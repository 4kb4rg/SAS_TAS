Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GL
Imports agri.HR
Imports agri.PWSystem.clsLangCap

Public Class HR_Stp_YearPlan : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchYearPlan As TextBox
    Protected WithEvents srchName As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblValidate As Label
    Protected WithEvents lblCode As Label

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "HR_HR_STP_YEARPLAN_GET"
    Dim strOppCd_UPD As String = "HR_HR_STP_YEARPLAN_UPD"

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRDepartment), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "YearPlan"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, EventData.PageSize)

        EventData.DataSource = dsData
        If EventData.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EventData.CurrentPageIndex = 0
            Else
                EventData.CurrentPageIndex = PageCount - 1
            End If
        End If

        EventData.DataBind()
        BindPageList()
        PageNo = EventData.CurrentPageIndex + 1
        lblTracker.Text = "Page " & pageno & " of " & EventData.PageCount
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = EventData.PageCount + 1
            arrDList.Add("Page " & count)
            Count = Count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex

    End Sub

    Sub BindStatusList(ByVal index As Integer)

        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Active), objHR.EnumDeptCodeStatus.Active))
        StatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Deleted), objHR.EnumDeptCodeStatus.Deleted))

    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.All), objHR.EnumDeptCodeStatus.All))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Active), objHR.EnumDeptCodeStatus.Active))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetDeptCodeStatus(objHR.EnumDeptCodeStatus.Deleted), objHR.EnumDeptCodeStatus.Deleted))
        srchStatusList.SelectedIndex = 1

    End Sub

    Protected Function LoadData() As DataSet
        Dim SearchStr As String
        Dim sortitem As String


        SearchStr = " And A.Status like '" & IIF(Not srchStatusList.selectedItem.Value = objHR.EnumDeptCodeStatus.All, _
                       srchStatusList.selectedItem.Value, "%") & "' "
        If Not srchYearPlan.text = "" Then
            SearchStr = SearchStr & " AND A.YearPlan like '" & srchYearPlan.text & "%' "
        End If
        If Not srchUpdateBy.text = "" Then
            SearchStr = SearchStr & " AND B.UserName like '" & _
                        srchUpdateBy.text & "%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text
        ParamNama = "STRSEARCH|SORTEXP"
        ParamValue = SearchStr & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOppCd_GET, ParamNama, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Tostring & "&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                EventData.CurrentPageIndex = 0
            Case "prev"
                EventData.CurrentPageIndex = _
                    Math.Max(0, EventData.CurrentPageIndex - 1)
            Case "next"
                EventData.CurrentPageIndex = _
                    Math.Min(EventData.PageCount - 1, EventData.CurrentPageIndex + 1)
            Case "last"
                EventData.CurrentPageIndex = EventData.PageCount - 1
        End Select
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            EventData.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        EventData.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.text = e.SortExpression.ToString()
        sortcol.text = IIF(sortcol.text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As Dropdownlist
        Dim Updbutton As linkbutton
        Dim Validator As RequiredFieldValidator

        blnUpdate.text = True
        EventData.EditItemIndex = CInt(e.Item.ItemIndex)

        BindGrid()
        If CInt(e.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(Edittext.text) = objHR.EnumDeptCodeStatus.Active
            Case True
                Statuslist.selectedindex = 0
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("YearPlan")
                EditText.readonly = True
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                'Updbutton.Attributes.Add("onclick","return confirm('Are you sure you want to delete this code?');")
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                Statuslist.selectedindex = 1
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("YearPlan")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UpdateDate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UserName")
                EditText.Enabled = False
                List = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("StatusList")
                List.Enabled = False
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select
        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Update")
        Updbutton.visible = True
        Validator = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ValidateCode")
        Validator.ErrorMessage = strValidateCode
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As Dropdownlist
        Dim YearPlan As String
        Dim bjr As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As label
        Dim CreateDate As String

        EditText = E.Item.FindControl("YearPlan")
        YearPlan = EditText.Text
        EditText = E.Item.FindControl("bjr")
        bjr = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.Selecteditem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        ParamNama = "YP|BJR|ST|CD|UD|UI|Loc"
        ParamValue = YearPlan & "|" & bjr & "|" & Status & "|" & CreateDate & "|" & DateTime.Now() & "|" & strUserId & "|" & strLocation


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Tostring & "&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_DeptCode.aspx")
        End Try

        If blnDupKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid()
        End If

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim EditText As TextBox
        Dim YearPlan As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim CreateDate As String

        EditText = E.Item.FindControl("YearPlan")
        YearPlan = EditText.Text
        EditText = E.Item.FindControl("Status")
        Status = IIF(trim(EditText.Text) = "1", _
                        "2", _
                        "1")
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        ParamNama = "YP|ST|CD|UD|UI"
        ParamValue = YearPlan & "|" & Status & "|" & CreateDate & "|" & DateTime.now() & "|" & strUserId


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Tostring & "&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_DeptCode.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim Validator As RequiredFieldValidator
        Dim PageCount As Integer

        blnUpdate.text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("YearPlan") = ""
        newRow.Item("BJR") = 0
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.now()
        NewRow.Item("UpdateDate") = DateTime.now()
        NewRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)


        EventData.DataSource = dataSet
        PageCount = objGlobal.mtdGetPageCount(dataSet.Tables(0).Rows.Count, EventData.PageSize)
        If EventData.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EventData.CurrentPageIndex = 0
            Else
                EventData.CurrentPageIndex = PageCount - 1
            End If
        End If
        EventData.DataBind()
        BindPageList()

        EventData.CurrentPageIndex = EventData.PageCount - 1
        lblTracker.Text = "Page " & (EventData.CurrentPageIndex + 1) & " of " & EventData.PageCount
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count - 1
        EventData.DataBind()
        BindStatusList(EventData.EditItemIndex)

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.visible = False
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateCode")
        Validator.ErrorMessage = strValidateCode

    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strYearPlan As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIf(Not srchStatusList.SelectedItem.Value = objHR.EnumDeptCodeStatus.All, srchStatusList.SelectedItem.Value, "")
        strYearPlan = srchYearPlan.Text
        strUpdateBy = srchUpdateBy.Text
        strSortExp = SortExpression.Text
        strSortCol = sortcol.Text

        'Response.Write("<Script Language=""JavaScript"">window.open(""../reports/HR_Rpt_DeptCode.aspx?strStatus=" & strStatus & _
        '            "&strDeptCode=" & srchYearPlan & _
        '            "&strUpdateBy=" & strUpdateBy & _
        '            "&strSortExp=" & strSortExp & _
        '            "&strSortCol=" & strSortCol & _
        '            "&DocTitleTag=" & lblTitle.text & UCase(lblCode.text) & _
        '            "&DeptCodeTag=" & lblDepartment.text & lblCode.text & _
        '            "&DeptDescTag=" & lblDesc.text & _
        '            """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub


End Class
