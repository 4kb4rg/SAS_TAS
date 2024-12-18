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
Imports agri.GL
Imports agri.PWSystem.clsLangCap

Public Class PR_setup_BerasRt_Estate : Inherits Page

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
    Protected WithEvents lblAtt As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchDeptCode As TextBox
    Protected WithEvents srchName As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblValidate As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents isNew As HtmlInputHidden
    Protected WithEvents btnGen As ImageButton

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "PR_PR_STP_BERASRATE_GET"
    Dim strOppCd_UPD As String = "PR_PR_STP_BERASRATE_UPD"

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim validateBs As String
    Dim validateActDate As String

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
    
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
	Dim Prefix as String
	
	Dim intPRAR As Long
	Dim intLevel As Integer

	Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
		
		Prefix = "CB" & trim(strlocation)
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

		Prefix = "CB" & trim(strlocation)
		
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
        intPRAR = Session("SS_PRAR")
		intLevel = Session("SS_USRLEVEL")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
         ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = False) and (intLevel < 2) Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            btnGen.Attributes("onclick") = "javascript:return ConfirmAction('generate');"
            lblErrMessage.Visible = False

            If SortExpression.Text = "" Then
                SortExpression.Text = "BerasCode"
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

        SearchStr = " AND A.LocCode = '" & strlocation & "' AND A.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objHR.EnumDeptCodeStatus.All, _
                       srchStatusList.SelectedItem.Value, "%") & "' "
        If Not srchDeptCode.Text = "" Then
            SearchStr = SearchStr & " AND A.BerasCode like '%" & srchDeptCode.Text & "%' "
        End If
       
        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND B.UserName like '%" & _
                        srchUpdateBy.Text & "%' "
        End If


        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        ParamNama = "SEARCH|Sort"
        ParamValue = SearchStr & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOppCd_GET, ParamNama, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_DEPTCODE_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
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

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(EditText.Text) = "1"
            Case True
                StatusList.SelectedIndex = 0
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("BerasCode")
                EditText.ReadOnly = True
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                'Updbutton.Attributes.Add("onclick","return confirm('Are you sure you want to delete this code?');")
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("BerasCode")
                EditText.Enabled = False
                'EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Description")
                'EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("BerasRate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("pstart")
                EditText.Enabled = False
				EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("pend")
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
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateCode")
        Validator.ErrorMessage = strValidateCode
        'Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateDesc")
        'Validator.ErrorMessage = strValidateDesc
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validateBs")
        Validator.ErrorMessage = validateBs
		Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateBerasBkspps")
        Validator.ErrorMessage = validateBs
		Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateBerasLembur")
        Validator.ErrorMessage = validateBs
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validatepstart")
        Validator.ErrorMessage = validateActDate
		 isNew.Value = "False"
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim BerasCode As String
        Dim Description As String
        Dim BerasRate As String
		Dim BerasRate2 As String
		Dim BerasRate3 As String
		Dim BerasRate4 As String
		Dim BerasRate5 As String
        Dim pstart As String
		Dim pend As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim CreateDate As String

		EditText = E.Item.FindControl("BerasCode")
            
		 If isNew.Value = "True" Then
            BerasCode = getCode()
        Else
		    BerasCode = EditText.Text
        End If

		
        EditText = E.Item.FindControl("BerasRate")
        BerasRate = EditText.Text
		
		EditText = E.Item.FindControl("BerasBkspps")
        BerasRate2 = EditText.Text
		
		EditText = E.Item.FindControl("BerasLembur")
        BerasRate3 = EditText.Text
		
		EditText = E.Item.FindControl("BerasTunj")
        BerasRate4 = EditText.Text
		
		EditText = E.Item.FindControl("BerasAstek")
        BerasRate5 = EditText.Text
        
		EditText = E.Item.FindControl("pstart")
        pstart = EditText.Text
		EditText = E.Item.FindControl("pend")
        pend = EditText.Text
		
        list = E.Item.FindControl("StatusList")
        Status = list.SelectedItem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        ParamNama = "BC|BR|Loc|PS|PE|ST|CD|UD|UI|BR2|BR3|BR4|BR5"
        ParamValue = BerasCode & "|" & _
                     BerasRate & "|" & _
                     strLocation & "|" & _
                     pstart & "|" & _
					 pend & "|" & _
                     Status & "|" & _
                     CreateDate & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId  & "|" & _
					 BerasRate2 & "|" & _
					 BerasRate3 & "|" & _
					 BerasRate4 & "|" & _
					 BerasRate5 


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_UPDATE_DEPTCODE&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_DeptCode.aspx")
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
        EventData.EditItemIndex = -1
        BindGrid()
		isNew.Value = "False"
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim EditText As TextBox
        Dim Status As String
        Dim BerasCode As String
        Dim Description As String
        Dim BerasRate As String
		Dim BerasRate2 As String
		Dim BerasRate3 As String
		Dim BerasRate4 As String
		Dim BerasRate5 As String
        Dim pstart As String
		Dim pend As String
        Dim CreateDate As String

        EditText = E.Item.FindControl("BerasCode")
        BerasCode = EditText.Text
        'EditText = E.Item.FindControl("Description")
        'Description = EditText.Text
        EditText = E.Item.FindControl("BerasRate")
        BerasRate = EditText.Text
		EditText = E.Item.FindControl("BerasBkspps")
        BerasRate2 = EditText.Text
		EditText = E.Item.FindControl("BerasLembur")
        BerasRate3 = EditText.Text
		EditText = E.Item.FindControl("BerasTunj")
        BerasRate4 = EditText.Text
		EditText = E.Item.FindControl("BerasAstek")
        BerasRate5 = EditText.Text
        EditText = E.Item.FindControl("pstart")
        pstart = EditText.Text
		EditText = E.Item.FindControl("pend")
        pend = EditText.Text
		
        EditText = E.Item.FindControl("Status")
        Status = IIf(Trim(EditText.Text) = "1", _
                        "2", _
                        "1")
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        ParamNama = "BC|BR|Loc|PS|PE|ST|CD|UD|UI|BR2|BR3|BR4|BR5"
        ParamValue = BerasCode & "|" & _
                     BerasRate & "|" & _
                     strLocation & "|" & _
                     pstart & "|" & _
					 pend & "|" & _
                     Status & "|" & _
                     CreateDate & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId & "|" & _
					 BerasRate2 & "|" & _
					 BerasRate3 & "|" & _
					 BerasRate4 & "|" & _
					 BerasRate5



        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DELETE_DEPTCODE&errmesg=" & lblErrMessage.Text & "&redirect=HR/Setup/HR_setup_DeptCode.aspx")
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
        newRow.Item("BerasCode") = getcodetmp()
        newRow.Item("PeriodeStart") = format(DateTime.Now,"MMyyyy")
		newRow.Item("PeriodeEnd") = format(DateTime.Now,"MMyyyy")
		newRow.Item("BerasRate") = 0
        newRow.Item("BerasBkspps") = 0
		newRow.Item("BerasLembur") = 0
		newRow.Item("BerasTunj") = 0
		newRow.Item("BerasAstek") = 0
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

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateCode")
        Validator.ErrorMessage = strValidateCode
        'Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ValidateDesc")
        'Validator.ErrorMessage = strValidateDesc
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateBs")
        Validator.ErrorMessage = validateBs
		Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateBerasBkspps")
        Validator.ErrorMessage = validateBs
		Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateBerasLembur")
        Validator.ErrorMessage = validateBs
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validatepstart")
        Validator.ErrorMessage = validateActDate
		Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validatepend")
        Validator.ErrorMessage = validateActDate
       isNew.Value = "True"
    End Sub

    Sub onload_GetLangCap()
        strValidateCode = "Please insert code"
		validateBs="Please insert beras rate"
		validateActDate="Please insert periode "
    End Sub

    Sub DEDR_Gen(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "PR_PR_STP_BERASRATE_GEN"
        Dim intErrNo As Integer

        ParamNama = "LOC"
        ParamValue = strLocation

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
            lblErrMessage.Text = "Proses generate harga beras sukses"
            lblErrMessage.ForeColor = Drawing.Color.Blue
        Catch Exp As System.Exception
            lblErrMessage.Text = "Error proses generate harga beras"
            lblErrMessage.ForeColor = Drawing.Color.Red
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BERASRATE_GEN&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        lblErrMessage.Visible = True
    End Sub

End Class
