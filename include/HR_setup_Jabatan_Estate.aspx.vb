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
Imports agri.HR
Imports agri.PWSystem.clsLangCap
Imports agri.GL

Public Class HR_Setup_Jabatan_Estate : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchgroupjbt As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchJobCode As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblReligion As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblValidate As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents isNew As HtmlInputHidden

    Protected WithEvents ddlCodeGrpJob As DropDownList
    Protected WithEvents lblCodeGrpJob As Label
	
	Protected WithEvents ddlStation As DropDownList
	Protected WithEvents lblStation As Label

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "HR_HR_STP_JABATAN_GET"
    Dim strOppCd_UPD As String = "HR_HR_STP_JABATAN_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim Prefix As String = "JB"
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

    Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object

		Prefix = "JB" & trim(strlocation)
        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Function getcodetmp() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETIDTMP"
        Dim objNewID As New Object

		Prefix = "JB" & trim(strlocation)
		
        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRReligion), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            'onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "JbtCode"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then
                src_Bindgroup()
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

        dsData = LoadData()
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
        lblTracker.Text = "Page " & PageNo & " of " & EventData.PageCount
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = EventData.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex

    End Sub

    Sub BindStatusList(ByVal index As Integer)

        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objHR.mtdGetReligionStatus(objHR.EnumReligionStatus.Active), objHR.EnumReligionStatus.Active))
        StatusList.Items.Add(New ListItem(objHR.mtdGetReligionStatus(objHR.EnumReligionStatus.Deleted), objHR.EnumReligionStatus.Deleted))

    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objHR.mtdGetReligionStatus(objHR.EnumReligionStatus.All), objHR.EnumReligionStatus.All))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetReligionStatus(objHR.EnumReligionStatus.Active), objHR.EnumReligionStatus.Active))
        srchStatusList.Items.Add(New ListItem(objHR.mtdGetReligionStatus(objHR.EnumReligionStatus.Deleted), objHR.EnumReligionStatus.Deleted))
        srchStatusList.SelectedIndex = 1

    End Sub

    Protected Function LoadData() As DataSet

        Dim SearchStr As String
        Dim sortitem As String


        SearchStr = " AND A.LocCode='" & strlocation & "' AND A.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = "3", _
                       srchStatusList.SelectedItem.Value, "%") & "' "
        If Not srchJobCode.Text = "" Then
            SearchStr = SearchStr & " AND A.JbtCode like '%" & srchJobCode.Text & "%' "
        End If
        If Not srchDesc.Text = "" Then
            SearchStr = SearchStr & " AND A.Description like '%" & _
                        srchDesc.Text & "%' "
        End If
        If Not srchgroupjbt.SelectedItem.Value.Trim = "" Then
            SearchStr = SearchStr & " AND A.CodeGrpJob like '%" & _
                                    srchgroupjbt.SelectedItem.Value.Trim & "%' "
        End If
        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND B.UserName like '" & _
                        srchUpdateBy.Text & "%' "
        End If
        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text

        ParamNama = "SEARCH|SORT|LOC"
        ParamValue = SearchStr & "|" & sortitem & "|" & strlocation


        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOppCd_GET, ParamNama, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_GROUPJOB_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objDataSet

    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
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
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As DropDownList
        Dim lblTemp As Label
        Dim ddlTemp As DropDownList
        Dim liTemp As ListItem
        Dim Updbutton As LinkButton
        Dim Validator As RequiredFieldValidator

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)
        onLoad_BindCodeGrpJob(EventData.EditItemIndex)
		onLoad_BindCodeSt(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(EditText.Text) = "1"
            Case True
                StatusList.SelectedIndex = 0
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("JbtCode")
                EditText.ReadOnly = True
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                'Updbutton.Attributes.Add("onclick","return confirm('Are you sure you want to delete this code?');")
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                lblTemp = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCodeGrpJob")
                ddlTemp = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlCodeGrpJob")
                If Not (lblTemp Is Nothing) Then
                    ddlTemp.SelectedValue = Trim(lblTemp.Text)
                End If
				lblTemp = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblStation")
                ddlTemp = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlStation")
                If Not (lblTemp Is Nothing) Then
                    ddlTemp.SelectedItem.Text = Trim(lblTemp.Text)
                End If
            Case False
                StatusList.SelectedIndex = 1
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("JbtCode")
                EditText.Enabled = False
                lblTemp = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCodeGrpJob")
                ddlTemp = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlCodeGrpJob")
                liTemp = ddlTemp.Items.FindByValue(CInt(lblTemp.Text))
                If Not (lblTemp Is Nothing) Then
                    ddlTemp.SelectedItem.Text = Trim(lblTemp.Text)
                End If
				lblTemp = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblStation")
                ddlTemp = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlStation")
                liTemp = ddlTemp.Items.FindByValue(CInt(lblTemp.Text))
                If Not (lblTemp Is Nothing) Then
                    ddlTemp.SelectedItem.Text = Trim(lblTemp.Text)
                End If
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Description")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UpdateDate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UserName")
                EditText.Enabled = False
                List = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("StatusList")
                List.Enabled = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validateCode")
        Validator.ErrorMessage = strValidateCode
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validateDesc")
        Validator.ErrorMessage = strValidateDesc
        isNew.Value = "False"
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim JobCode As String
        Dim Description As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim CreateDate As String
        Dim StrGrpCode As String
		Dim strStation AS String

        list = E.Item.FindControl("ddlCodeGrpJob")
        StrGrpCode = list.SelectedItem.Value
        lblMsg = E.Item.FindControl("lblErrCodeGrpJobe")
        If Trim(StrGrpCode) = "" Then
            lblMsg.Visible = True
            Exit Sub
        End If
		
		list = E.Item.FindControl("ddlStation")
		strStation = list.SelectedItem.Value

        EditText = E.Item.FindControl("JbtCode")

        If isNew.Value = "True" Then
            JobCode = getCode()
        Else
            JobCode = EditText.Text
        End If


        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.SelectedItem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        ParamNama = "JC|GJ|Desc|LOC|ST|CD|UD|UI|BLK"
        ParamValue = JobCode & "|" & _
                    StrGrpCode & "|" & _
                    Description & "|" & _
                    strLocation & "|" & _
                    Status & "|" & _
                    CreateDate & "|" & _
                    DateTime.Now() & "|" & _
                    strUserId & "|" & _
					strStation 

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_JABATAN_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Religion.aspx")
        End Try

        If blnDupKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid()
        End If
        isNew.Value = "False"
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        isNew.Value = "False"
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim list As DropDownList
        Dim EditText As TextBox
        Dim JobCode As String
        Dim Description As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim CreateDate As String
        Dim StrGrpCode As String


        EditText = E.Item.FindControl("JbtCode")
        JobCode = EditText.Text
        list = E.Item.FindControl("ddlCodeGrpJob")
        StrGrpCode = list.SelectedItem.Value
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        EditText = E.Item.FindControl("Status")
        Status = IIf(Trim(EditText.Text) = "1", _
                        "2", _
                        "1")
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        ParamNama = "JC|GJ|Desc|LOC|ST|CD|UD|UI"
        ParamValue = JobCode & "|" & _
                    StrGrpCode & "|" & _
                    Description & "|" & _
                    strLocation & "|" & _
                    Status & "|" & _
                    CreateDate & "|" & _
                    DateTime.Now() & "|" & _
                    strUserId

        If JobCode = "" Then

            Exit Sub
        End If

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_JABATAN_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Religion.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim Validator As RequiredFieldValidator
        Dim PageCount As Integer

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("JbtCode") = getcodetmp()
        newRow.Item("CodeGrpJob") = ""
        newRow.Item("Description") = ""
		newRow.Item("Station") = "OH"
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UserName") = ""
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
        onLoad_BindCodeGrpJob(EventData.EditItemIndex)
		onLoad_BindCodeSt(EventData.EditItemIndex)
        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        Validator.ErrorMessage = strValidateCode
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")
        Validator.ErrorMessage = strValidateDesc
        isNew.Value = "True"
    End Sub

    Function BindCodeGrpJob() As DataSet
        Dim strOpCd_DivId As String = "HR_HR_STP_GROUPJOB_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objJobGroup As New Object

        ParamNama = "SEARCH|SORT"
        ParamValue = "|Order by GrpJobCode"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_GROUPJOB_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objJobGroup.Tables(0).Rows.Count - 1
            objJobGroup.Tables(0).Rows(intCnt).Item("GrpJobCode") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("GrpJobCode"))
            objJobGroup.Tables(0).Rows(intCnt).Item("Description") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("Description"))
        Next

        dr = objJobGroup.Tables(0).NewRow()
        dr("GrpJobCode") = ""
        dr("Description") = "Select Group Job"
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)

        Return objJobGroup
    End Function

    Sub onLoad_BindCodeGrpJob(ByVal index As Integer)
        ddlCodeGrpJob = EventData.Items.Item(index).FindControl("ddlCodeGrpJob")
        ddlCodeGrpJob.DataSource = BindCodeGrpJob()
        ddlCodeGrpJob.DataTextField = "Description"
        ddlCodeGrpJob.DataValueField = "GrpJobCode"
        ddlCodeGrpJob.DataBind()
    End Sub

    Sub src_Bindgroup()
        srchgroupjbt.DataSource = BindCodeGrpJob()
        srchgroupjbt.DataTextField = "Description"
        srchgroupjbt.DataValueField = "GrpJobCode"
        srchgroupjbt.DataBind()
    End Sub
	
	Function BindCodeSt() As DataSet
		Dim strOpCd As String = "PR_PR_STP_YEARPLAN_GET"
        Dim objCodeDs As New Object()
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strSearch As String
        Dim sortitem As String

        strSearch = " AND A.LocCode='" & strLocation & "' AND A.Status='1' "
        sortitem = "Order by BlkCode"
        ParamNama = "STRSEARCH|SORTEXP"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBCATEGORY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objCodeDs.Tables(0).Rows.Count - 1
                objCodeDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objCodeDs.Tables(0).Rows(intCnt).Item("BlkCode"))
                objCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objCodeDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If
		
		dr = objCodeDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Select Tahun Tanam"
        objCodeDs.Tables(0).Rows.InsertAt(dr, 0)
		
        
        Return objCodeDs
    End Function
	
	Sub onLoad_BindCodeSt(ByVal index As Integer)
        ddlStation = EventData.Items.Item(index).FindControl("ddlStation")
        ddlStation.DataSource = BindCodeSt()
        ddlStation.DataTextField = "Description"
        ddlStation.DataValueField = "BlkCode"
        ddlStation.DataBind()
    End Sub

End Class
