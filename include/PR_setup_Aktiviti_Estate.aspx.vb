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

Public Class PR_setup_Aktiviti_Estate : Inherits Page

    Protected WithEvents dgaktiviti As DataGrid
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
    Protected WithEvents srchJobCode As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblReligion As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblValidate As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents isNew As HtmlInputHidden

    Protected WithEvents ddlcatid As DropDownList
    Protected WithEvents lblcatid As Label

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "PR_PR_STP_BK_JOB_GET"
    Dim strOppCd_UPD As String = "PR_PR_STP_BK_JOB_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim Prefix As String = "JOB"
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

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||6", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Function getcodetmp() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETIDTMP"
        Dim objNewID As New Object

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||6", objNewID)
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
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRReligion), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            'onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "JobCode"
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
        dgaktiviti.CurrentPageIndex = 0
        dgaktiviti.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgaktiviti.PageSize)

        dgaktiviti.DataSource = dsData
        If dgaktiviti.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgaktiviti.CurrentPageIndex = 0
            Else
                dgaktiviti.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgaktiviti.DataBind()
        BindPageList()
        PageNo = dgaktiviti.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgaktiviti.PageCount
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgaktiviti.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgaktiviti.CurrentPageIndex

    End Sub

    Sub BindStatusList(ByVal index As Integer)

        StatusList = dgaktiviti.Items.Item(index).FindControl("StatusList")
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


        SearchStr = ""
        If Not srchJobCode.Text = "" Then
            SearchStr = SearchStr & " AND JobCode like '%" & srchJobCode.Text & "%' "
        End If
        If Not srchDesc.Text = "" Then
            SearchStr = SearchStr & " AND JobName like '%" & _
                        srchDesc.Text & "%' "
        End If
        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND B.UserName like '" & _
                        srchUpdateBy.Text & "%' "
        End If
        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text

        ParamNama = "SEARCH|SORT"
        ParamValue = SearchStr & "|" & sortitem


        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOppCd_GET, ParamNama, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objDataSet

    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgaktiviti.CurrentPageIndex = 0
            Case "prev"
                dgaktiviti.CurrentPageIndex = _
                    Math.Max(0, dgaktiviti.CurrentPageIndex - 1)
            Case "next"
                dgaktiviti.CurrentPageIndex = _
                    Math.Min(dgaktiviti.PageCount - 1, dgaktiviti.CurrentPageIndex + 1)
            Case "last"
                dgaktiviti.CurrentPageIndex = dgaktiviti.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgaktiviti.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgaktiviti.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgaktiviti.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgaktiviti.CurrentPageIndex = lstDropList.SelectedIndex
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

        'blnUpdate.Text = True

        dgaktiviti.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= dgaktiviti.Items.Count Then
            dgaktiviti.EditItemIndex = -1
            Exit Sub
        End If

        BindStatusList(dgaktiviti.EditItemIndex)
        onLoad_BindCodeGrpJob(dgaktiviti.EditItemIndex)

        ''EditText = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Status")
        ''Select Case CInt(EditText.Text) = "1"
        ''    Case True
        '' StatusList.SelectedIndex = 0
        EditText = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("JbtCode")
        EditText.ReadOnly = True
        Updbutton = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
        Updbutton.Text = "Delete"
        Updbutton.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this code?');")
        Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        lblTemp = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblcatid")
        ddlTemp = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlcatid")
        If Not (lblTemp Is Nothing) Then
            ddlTemp.SelectedValue = Trim(lblTemp.Text)
        End If
        '   Case False
        ''StatusList.SelectedIndex = 1
        ''EditText = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("JobCode")
        ''EditText.Enabled = False
        ' ''lblTemp = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblcatid")
        ' ''ddlTemp = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ddlcatid")
        ' ''liTemp = ddlTemp.Items.FindByValue(CInt(lblTemp.Text))
        ' ''If Not (lblTemp Is Nothing) Then
        ' ''    ddlTemp.SelectedItem.Text = Trim(lblTemp.Text)
        ' ''End If
        ''EditText = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("JobName")
        ''EditText.Enabled = False
        ''EditText = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UpdateDate")
        ''EditText.Enabled = False
        ''EditText = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UserName")
        ''EditText.Enabled = False
        ''List = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("StatusList")
        ''List.Enabled = False
        ''Updbutton = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
        ''Updbutton.Visible = False
        ''Updbutton = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
        ''Updbutton.Text = "Undelete"
        ''End Select
        Validator = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validateCode")
        Validator.ErrorMessage = strValidateCode
        Validator = dgaktiviti.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validateDesc")
        Validator.ErrorMessage = strValidateDesc
        'isNew.Value = "False"
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim JobCode As String
        Dim Description As String
        Dim Uom As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim CreateDate As String
        Dim StrGrpCode As String

        list = E.Item.FindControl("ddlcatid")
        StrGrpCode = list.SelectedItem.Value
        lblMsg = E.Item.FindControl("lblErrCodeGrpJobe")
        If Trim(StrGrpCode) = "" Then
            lblMsg.Visible = True
            Exit Sub
        End If

        EditText = E.Item.FindControl("JobCode")

        If isNew.Value = "True" Then
            JobCode = getCode()
        Else
            JobCode = EditText.Text
        End If


        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        EditText = E.Item.FindControl("Uom")
        Uom = EditText.Text

        list = E.Item.FindControl("StatusList")
        Status = list.SelectedItem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        ParamNama = "JC|GJ|Desc|UM|ST|CD|UD|UI"
        ParamValue = JobCode & "|" & _
                     StrGrpCode & "|" & _
                     Description & "|" & _
                     Uom & "|" & _
                     Status & "|" & _
                     CreateDate & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_UPD&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_Religion.aspx")
        End Try

        If blnDupKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            dgaktiviti.EditItemIndex = -1
            BindGrid()
        End If
        isNew.Value = "False"
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        isNew.Value = "False"
        dgaktiviti.EditItemIndex = -1
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


        EditText = E.Item.FindControl("JobCode")
        JobCode = EditText.Text
        list = E.Item.FindControl("ddlcatid")
        StrGrpCode = list.SelectedItem.Value
        EditText = E.Item.FindControl("Description")
        Description = EditText.Text
        EditText = E.Item.FindControl("Status")
        Status = IIf(Trim(EditText.Text) = "1", _
                        "2", _
                        "1")
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        ParamNama = "JC|GJ|Desc|ST|CD|UD|UI"
        ParamValue = JobCode & "|" & _
                    StrGrpCode & "|" & _
                    Description & "|" & _
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

        dgaktiviti.EditItemIndex = -1
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
        newRow.Item("JobCode") = getcodetmp()
        newRow.Item("CatId") = ""
        newRow.Item("JobName") = ""
        newRow.Item("UOM") = ""
        newRow.Item("Status") = "1"
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)

        dgaktiviti.DataSource = dataSet
        PageCount = objGlobal.mtdGetPageCount(dataSet.Tables(0).Rows.Count, dgaktiviti.PageSize)
        If dgaktiviti.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgaktiviti.CurrentPageIndex = 0
            Else
                dgaktiviti.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgaktiviti.DataBind()
        BindPageList()

        dgaktiviti.CurrentPageIndex = dgaktiviti.PageCount - 1
        lblTracker.Text = "Page " & (dgaktiviti.CurrentPageIndex + 1) & " of " & dgaktiviti.PageCount
        lstDropList.SelectedIndex = dgaktiviti.CurrentPageIndex
        dgaktiviti.DataBind()
        dgaktiviti.EditItemIndex = dgaktiviti.Items.Count - 1
        dgaktiviti.DataBind()
        BindStatusList(dgaktiviti.EditItemIndex)
        onLoad_BindCodeGrpJob(dgaktiviti.EditItemIndex)
        Updbutton = dgaktiviti.Items.Item(CInt(dgaktiviti.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False
        Validator = dgaktiviti.Items.Item(CInt(dgaktiviti.EditItemIndex)).FindControl("validateCode")
        Validator.ErrorMessage = strValidateCode
        Validator = dgaktiviti.Items.Item(CInt(dgaktiviti.EditItemIndex)).FindControl("validateDesc")
        Validator.ErrorMessage = strValidateDesc
        isNew.Value = "True"
    End Sub

    Sub onLoad_BindCodeGrpJob(ByVal index As Integer)
        ddlcatid = dgaktiviti.Items.Item(index).FindControl("ddlcatid")

        Dim strOpCd_DivId As String = "PR_PR_STP_BK_CATEGORY_GET_LIST"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objJobGroup As New Object

        ParamNama = "SEARCH|SORT"
        ParamValue = "|Order by CatId"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_CATEGORY_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objJobGroup.Tables(0).Rows.Count - 1
            objJobGroup.Tables(0).Rows(intCnt).Item("CatId") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("CatId"))
            objJobGroup.Tables(0).Rows(intCnt).Item("CatName") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("CatName"))
        Next

        dr = objJobGroup.Tables(0).NewRow()
        dr("CatId") = ""
        dr("CatName") = "Select kategori"
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)

        ddlcatid.DataSource = objJobGroup.Tables(0)
        ddlcatid.DataValueField = "CatId"
        ddlcatid.DataTextField = "CatName"
        ddlcatid.DataBind()
    End Sub

End Class
