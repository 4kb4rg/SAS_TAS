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

Public Class PR_setup_GolList_Estate : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srcpmonth As DropDownList
    Protected WithEvents srcpyear As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchDeptCode As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents btnGen As ImageButton
    

    Protected objHR As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected strDateFormat As String

    Dim strOppCd_GET As String = "PR_PR_STP_EMPGOL_GET"
    Dim strOppCd_UPD As String = "PR_PR_STP_EMPGOL_UPD"

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim validateBs As String
    Dim validatePSDate As String
    Dim validatePEDate As String

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim strAcceptFormat As String
	Dim intLevel As Integer

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
        strDateFormat = Session("SS_DATEFMT")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
         ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = False) and (intLevel < 2) Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            btnGen.Attributes("onclick") = "javascript:return ConfirmAction('generate');"
            lblErrMessage.Visible = False

            If SortExpression.Text = "" Then
                SortExpression.Text = "GolCode"
                sortcol.Text = "ASC"
            End If

            If Not Page.IsPostBack Then
                srcpmonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object()
        Dim objActualDate As New Object()
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_GRList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intselection As Integer = 0
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If pv_strAccYear = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear")) Then
                'intselection = intCnt + 1
                intselection = intCnt
            End If
        Next intCnt

        'dr = objAccYearDs.Tables(0).NewRow()
        'dr("AccYear") = ""
        'dr("UserName") = "All"
        'objAccYearDs.Tables(0).Rows.InsertAt(dr, 0)

        srcpyear.DataSource = objAccYearDs.Tables(0)
        srcpyear.DataValueField = "AccYear"
        srcpyear.DataTextField = "UserName"
        srcpyear.DataBind()
        srcpyear.SelectedIndex = intselection
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

        If (srcpmonth.SelectedItem.Value.Trim = "") Or (srcpyear.SelectedItem.Value.Trim = "") Then
            SearchStr = "AND A.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = "0", srchStatusList.SelectedItem.Value, "%") & "' "
        Else
            SearchStr = " AND ('" & srcpyear.SelectedItem.Value.Trim & srcpmonth.SelectedItem.Value.Trim & "' >= right(rtrim(periodestart),4)+left(rtrim(periodestart),2)) AND " & _
                             "('" & srcpyear.SelectedItem.Value.Trim & srcpmonth.SelectedItem.Value.Trim & "' <= right(rtrim(periodeend),4)+left(rtrim(periodeend),2)) AND " & _
                             "A.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = "0", srchStatusList.SelectedItem.Value, "%") & "' "
        End If

        If Not srchDeptCode.Text = "" Then
            SearchStr = SearchStr & " AND A.GolCode like '%" & srchDeptCode.Text & "%' "
        End If

        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND B.UserName like '" & _
                        srchUpdateBy.Text & "%' "
        End If


        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        ParamNama = "LOC|SEARCH|Sort"
        ParamValue = strLocation & "|" & SearchStr & "|" & sortitem

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
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("GolCode")
                EditText.ReadOnly = True
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                 Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("GolCode")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("BasicSalary")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("PeriodeStart")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("PeriodeEnd")
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
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validateBs")
        Validator.ErrorMessage = validateBs
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validatePSDate")
        Validator.ErrorMessage = validatePSDate
        Validator = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("validatePEDate")
        Validator.ErrorMessage = validatePEDate
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim GolCode As String
        Dim BaseSalary As String
        Dim PSdate As String
        Dim PEdate As String
		
		Dim PSdate1 As String
        Dim PEdate1 As String


        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim CreateDate As String

        EditText = E.Item.FindControl("GolCode")
        GolCode = EditText.Text.ToUpper
        EditText = E.Item.FindControl("BasicSalary")
        BaseSalary = EditText.Text
        EditText = E.Item.FindControl("PeriodeStart")
        PSdate = EditText.Text.Trim()
        EditText = E.Item.FindControl("PeriodeEnd")
        PEdate = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("tmp_PeriodeStart")
        PSdate1 = EditText.Text.Trim()
		EditText = E.Item.FindControl("tmp_PeriodeEnd")
        PEdate1 = EditText.Text.Trim()


        list = E.Item.FindControl("StatusList")
        Status = list.SelectedItem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        ParamNama = "GC|BS|PS1|PE1|PS|PE|ST|CD|UD|UI|LOC"
        ParamValue = GolCode & "|" & _
                     BaseSalary & "|" & _
					 PSdate1 & "|" & _
                     PEdate1 & "|" & _
                     PSdate & "|" & _
                     PEdate & "|" & _
                     Status & "|" & _
                     CreateDate & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId & "|" & _
					 strlocation 


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPGOL_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        Dim Status As String
        Dim GolCode As String
        Dim BaseSalary As String
        Dim PSdate As String
        Dim PEdate As String

        Dim CreateDate As String

        EditText = E.Item.FindControl("GolCode")
        GolCode = EditText.Text
        EditText = E.Item.FindControl("BasicSalary")
        BaseSalary = EditText.Text
        EditText = E.Item.FindControl("PeriodeStart")
        PSdate = EditText.Text.Trim()
        EditText = E.Item.FindControl("PeriodeEnd")
        PEdate = EditText.Text.Trim()
        EditText = E.Item.FindControl("Status")
        Status = IIf(Trim(EditText.Text) = "1", _
                        "2", _
                        "1")
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        ParamNama = "GC|BS|PS1|PE1|PS|PE|ST|CD|UD|UI|LOC"
        ParamValue = GolCode & "|" & _
                     BaseSalary & "|" & _
                     PSdate & "|" & _
                     PEdate & "|" & _
                     PSdate & "|" & _
                     PEdate & "|" & _
                     Status & "|" & _
                     CreateDate & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId & "|" & _
					 strlocation 


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOppCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPGOL_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        newRow.Item("GolCode") = ""
        newRow.Item("BasicSalary") = 0
        newRow.Item("PeriodeStart") = Format(DateTime.Now(), "MMyyyy")
        newRow.Item("PeriodeEnd") = Format(DateTime.Now(), "MMyyyy")
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
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateBs")
        Validator.ErrorMessage = validateBs
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validatePSDate")
        Validator.ErrorMessage = validatePSDate
        Validator = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validatePEDate")
        Validator.ErrorMessage = validatePEDate
    End Sub

    Sub DEDR_Gen(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "PR_PR_STP_EMPGOL_GEN"
        Dim intErrNo As Integer

        ParamNama = "LOC"
        ParamValue = strLocation

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
            lblErrMessage.Text = "Proses generate gol skub sukses"
            lblErrMessage.ForeColor = Drawing.Color.Blue
        Catch Exp As System.Exception
            lblErrMessage.Text = "Error proses generate gol skub"
            lblErrMessage.ForeColor = Drawing.Color.Red
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPGOL_GEN&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        lblErrMessage.Visible = True
    End Sub

End Class
