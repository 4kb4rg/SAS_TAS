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
Imports agri.GlobalHdl.clsGlobalHdl


Public Class PR_DailyAttd_Estate : Inherits Page

    Protected WithEvents dgEmpList As DataGrid
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList
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
    'New
    Protected WithEvents txtAttdDate As TextBox
    Protected WithEvents ddlEmpType As DropDownList
    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlEmpJob As DropDownList
    Protected WithEvents ddlabsen As DropDownList



    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim Objok As New agri.GL.ClsTrx
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String

    Dim intHRAR As Long
    Dim blnCanDelete As Boolean = False

    Dim objEmpDs As New Object()
    Dim objEmpTypeDs As New Object()
    Dim objEmpDivDs As New Object()
    Dim objEmpJobDs As New Object()

    Dim ListEmp As String
    Dim strDateFmt As String
    Dim strAcceptFormat As String

    Dim Absn As New Hashtable

    Function GetCheck(ByVal n As String) As Boolean
        If n = "0" Then
            Return False
        Else
            Return True
        End If
    End Function


    Function getCode() As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        tcode = "ATT/" & strLocation & "/" & Mid(txtAttdDate.Text, 4, 2) & Right(txtAttdDate.Text, 2) & "/"

        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|5|" & tcode & "|5"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))
    End Function

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


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")
        strDateFmt = Session("SS_DATEFMT")

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
            BindAbsenType()
            If Not Page.IsPostBack Then
                txtAttdDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                BindEmpDiv()
                BindEmpType()
                BindEmpJob()
                BindGrid()


            End If
        End If
    End Sub

    Sub SwitchMode(ByVal pv_strQuery As String)
        If pv_strQuery = "empdet" Then
            blnCanDelete = True
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        dgEmpList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub BindAbsType(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim DDLabs As DropDownList = CType(e.Item.FindControl("ddlabsen"), DropDownList)
            Dim item As ListItem
            Dim lbl As Label

            lbl = CType(e.Item.FindControl("lblabsen"), Label)

            DDLabs.DataSource = Absn
            DDLabs.DataTextField = "value"
            DDLabs.DataValueField = "key"
            DDLabs.DataBind()
            DDLabs.SelectedValue = lbl.Text
            If lbl.Text <> "" Then
                DDLabs.Enabled = False
            Else
                DDLabs.SelectedValue = "M"
            End If
            'item = DDLabs.Items.FindByValue("lblabsen")
            'If Not item Is Nothing Then item.Selected = True
        End If

    End Sub

    Sub BindAbsenType()
        Dim strOpCd_Type As String = "PR_PR_STP_ATTCODE_LIST_GET"
        Dim strName As String
        Dim strParam As String
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim objAbsType As New Object()

        strName = "SEARCH|SORT"
        strParam = "Where status='1'|"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_Type, strName, strParam, objAbsType)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=mtdIDGet&errmesg=" & ex.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Absn.Clear()
        If objAbsType.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objAbsType.Tables(0).Rows.Count - 1
                Absn.Add(Trim(objAbsType.Tables(0).Rows(intCnt).Item("AttCode")), Trim(objAbsType.Tables(0).Rows(intCnt).Item("Description")))               
            Next
        End If
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
            For intCnt = 0 To objEmpTypeDs.Tables(0).Rows.Count - 1
                objEmpJobDs.Tables(0).Rows(intCnt).Item("GrpJobCode") = Trim(objEmpJobDs.Tables(0).Rows(intCnt).Item("GrpJobCode"))
                objEmpJobDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpJobDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objEmpJobDs.Tables(0).NewRow()
        dr("GrpJobCode") = ""
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
        dr("EmpTyCode") = ""
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
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpDiv.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDiv.DataTextField = "Description"
        ddlEmpDiv.DataValueField = "BlkGrpCode"
        ddlEmpDiv.DataBind()
        ddlEmpDiv.SelectedIndex = 0

    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()

        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgEmpList.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsData = LoadData()
            End If
        End If

        dgEmpList.DataSource = dsData
        dgEmpList.DataBind()


        lblPageCount.Text = PageCount
        BindPageList(PageCount)
        PageNo = lblCurrentIndex.Text + 1

        'If blnCanDelete = True Then
        '    For intCnt = 0 To dgEmpList.Items.Count - 1
        '        Select Case CInt(objEmpDs.Tables(0).Rows(intCnt).Item("Status"))
        '            Case objHR.EnumEmpStatus.Pending
        '                lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
        '                lbButton.Visible = True
        '                lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        '            Case objHR.EnumEmpStatus.Active
        '                lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
        '                lbButton.Visible = True
        '                lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        '            Case objHR.EnumEmpStatus.Deleted
        '                lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
        '                lbButton.Visible = False
        '        End Select
        '    Next
        '    NewEmpBtn.Visible = True
        'Else
        '    For intCnt = 0 To dgEmpList.Items.Count - 1
        '        lbButton = dgEmpList.Items.Item(intCnt).FindControl("lbDelete")
        '        lbButton.Visible = False
        '    Next
        '    NewEmpBtn.Visible = False
        'End If

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "PR_PR_TRX_EMPATT_LIST_GET"
        Dim strOpCd_AbsGet As String = "PR_PR_TRX_ATTEMP_LIST_GET"
        Dim strSrchEmpCode As String
        Dim strSrchEmpName As String
        Dim strSrchEmpDiv As String
        Dim strSrchEmpType As String
        Dim strSrchEmpJob As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intCnt2 As Integer
        Dim strSortExpression As String
        Dim ObjAtt As New Object()
        Dim StrEmp As String
      
        Session("SS_PAGING") = lblCurrentIndex.Text

        'GET Data Emp
        strSrchEmpCode = IIf(txtEmpCode.Text = "", "", txtEmpCode.Text)
        strSrchEmpName = IIf(txtEmpName.Text = "", "", txtEmpName.Text)
        strSrchEmpDiv = IIf(ddlEmpDiv.SelectedItem.Value = "0", "", ddlEmpDiv.SelectedItem.Value)
        strSrchEmpType = IIf(ddlEmpType.SelectedItem.Value = "0", "", ddlEmpType.SelectedItem.Value)
        strSrchEmpJob = IIf(ddlEmpJob.SelectedItem.Value = "0", "", ddlEmpJob.SelectedItem.Value)
        'strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        'strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)

        If SortExpression.Text = "UserName" Then
            strSortExpression = "B.UserName"
        Else
            strSortExpression = "A." & SortExpression.Text
        End If

        strParamName = "SEARCH|SORT"

        strParamValue = "AND A.EmpCode Like '%" & strSrchEmpCode & "%' " & _
                        "AND A.EmpName Like '%" & strSrchEmpName & "%' " & _
                        "AND C.IDDiv Like '%" & strSrchEmpDiv & "%'" & _
                        "AND C.CodeEmpty Like '%" & strSrchEmpType & "%'" & _
                        "AND D.JobCode Like '%" & strSrchEmpJob & "%'" & _
                        "AND A.Status Like '%1%'" & _
                        "AND A.LocCode Like '%" & strLocation & "%'|" & _
                        "ORDER BY " & strSortExpression

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        ListEmp = "("

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpName"))
            objEmpDs.Tables(0).Rows(intCnt).Item("CodeEmpty") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("CodeEmpty"))
            objEmpDs.Tables(0).Rows(intCnt).Item("CodeAbsent") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("CodeAbsent"))
            objEmpDs.Tables(0).Rows(intCnt).Item("HK") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("HK"))
            objEmpDs.Tables(0).Rows(intCnt).Item("isAbsent") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("isAbsent"))

            ListEmp = ListEmp & "'" & objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") & "'"
            If intCnt = objEmpDs.Tables(0).Rows.Count - 1 Then
                ListEmp = ListEmp & ""
            Else
                ListEmp = ListEmp & ","
            End If
        Next

        If ListEmp = "(" Then
            ListEmp = "('')"
        Else
            ListEmp = ListEmp & ")"
        End If

        'GET Data Absent

        If Not ListEmp = "('')" Then
            strSrchEmpCode = ListEmp
            strParamName = "SEARCH|SORT"

            strParamValue = "AND AttDate='" & Date_Validation(txtAttdDate.Text, False) & "' AND EmpCode IN " & strSrchEmpCode & "|"

            Try
                intErrNo = Objok.mtdGetDataCommon(strOpCd_AbsGet, strParamName, strParamValue, ObjAtt)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList_Estate.aspx")
            End Try

            If ObjAtt.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
                    StrEmp = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                    For intCnt2 = 0 To ObjAtt.Tables(0).Rows.Count - 1
                        If StrEmp = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("EmpCode")) Then
                            objEmpDs.Tables(0).Rows(intCnt).Item("isAbsent") = "1"
                            objEmpDs.Tables(0).Rows(intCnt).Item("CodeAbsent") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("AttCode"))
                            objEmpDs.Tables(0).Rows(intCnt).Item("HK") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("HK"))
                            objEmpDs.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("UpdateDate"))
                            objEmpDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(ObjAtt.Tables(0).Rows(intCnt2).Item("UserName"))
                        End If
                    Next
                Next
            End If

        End If
        Return objEmpDs
    End Function

    Sub EmpLink_Click(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If Not E.CommandName.ToString = "Sort" And Not E.CommandName.ToString = "Delete" Then
            Dim lbl As Label
            Dim intIndex As Integer = E.Item.ItemIndex
            Dim strEmpCode As String
            Dim strEmpName As String
            Dim strDateAtt As String

            lbl = dgEmpList.Items.Item(intIndex).FindControl("lblEmpCode")
            strEmpCode = lbl.Text
            lbl = dgEmpList.Items.Item(intIndex).FindControl("lblEmpName")
            strEmpName = lbl.Text

            strDateAtt = Date_Validation(txtAttdDate.Text, False)
            Response.Redirect("PR_trx_DailyAttdDet_Estate.aspx?redirect=attd&EmpCode=" & strEmpCode & "&Attdate=" & strDateAtt & "&EmpName=" & strEmpName)
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
        Dim strOpCd_Status As String = "HR_CLXTRX_EMPLOYEE_STATUS_UPD"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim lblEmpCode As Label
        Dim strEmpCode As String

        dgEmpList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblEmpCode = dgEmpList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblEmpCode")
        strEmpCode = lblEmpCode.Text

        strParam = strEmpCode & "|" & objHR.EnumEmpStatus.Deleted

        Try
            intErrNo = objHR.mtdUpdEmployeeDetStatus(strOpCd_Status, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strParam)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_DELETE_EMPLOYEE&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try
        dgEmpList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub Btnsave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_TRX_ATTENDANCE_UPD"
        Dim lblEmpCode As Label
        Dim chkabsent As CheckBox
        Dim txtHk As TextBox
        Dim DDLabs As DropDownList
        Dim DDLunt As DropDownList
        Dim i As Integer
        Dim attcode As String = ""
        Dim Paramtmp As String = ""
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        For i = 0 To dgEmpList.Items.Count - 1

            lblEmpCode = dgEmpList.Items.Item(i).FindControl("lblEmpCode")
            chkabsent = dgEmpList.Items.Item(i).FindControl("Absent")
            txtHk = dgEmpList.Items.Item(i).FindControl("HK")
            DDLabs = dgEmpList.Items.Item(i).FindControl("ddlabsen")


            If chkabsent.Enabled And chkabsent.Checked Then
                attcode = getCode()
                ParamName = "AI|Loc|AD|EC|AM|AY|AC|Hk|CD|UD|UI|ST"
                Paramtmp = txtHk.Text

                ParamValue = attcode & "|" & _
                             strLocation & "|" & _
                             Date_Validation(txtAttdDate.Text, False) & "|" & _
                             lblEmpCode.Text & "|" & _
                             Mid(Trim(txtAttdDate.Text), 4, 2) & "|" & _
                             Right(Trim(txtAttdDate.Text), 4) & "|" & _
                             DDLabs.SelectedItem.Value & "|" & _
                             Paramtmp & "|" & _
                             DateTime.Now() & "|" & _
                             DateTime.Now() & "|" & _
                             strUserId & "|0"
                Try
                    intErrNo = Objok.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_UPD&errmesg=" & Exp.Message & "&redirect=PR/trx/PR_trx_DailyAttd_ESTATE.aspx")
                End Try
            End If
        Next
    

        srchBtn_Click(Sender, E)
    End Sub


End Class
