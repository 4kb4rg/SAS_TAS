Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic
Imports agri.Admin
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap

Public Class HR_setup_Deptdet : Inherits Page

    Protected WithEvents ddlDeptCode As DropDownList
    Protected WithEvents ddlCompCode As DropDownList
    Protected WithEvents ddlLocCode As DropDownList
    Protected WithEvents ddlDeptHead As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents deptcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupDept As Label
    Protected WithEvents lblNoDeptCode As Label
    Protected WithEvents lblNoCompCode As Label
    Protected WithEvents lblNoLocCode As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblCompany As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblDeptHead As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblList As Label

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objDeptDs As New Object()
    Dim objDeptCodeDs As New Object()
    Dim objCompDs As New Object()
    Dim objLocDs As New Object()
    Dim objEmpDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long

    Dim strSelectedDeptCode As String = ""
    Dim intStatus As Integer
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblNoDeptCode.Visible = False
            lblNoCompCode.Visible = False
            lblNoLocCode.Visible = False
            lblErrDupDept.Visible = False
            strSelectedDeptCode = Trim(IIf(Request.QueryString("deptcode") <> "", Request.QueryString("deptcode"), Request.Form("deptcode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedDeptCode <> "" Then
                    deptcode.Value = strSelectedDeptCode
                    onLoad_Display()
                Else
                    onLoad_BindDeptCode("")
                    onLoad_BindCompCode(strCompany)
                    onLoad_BindLocCode(strCompany,strLocation)
                    onLoad_BindEmpCode(strCompany,strLocation,"","")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.Department))
        lblDepartment.text = GetCaption(objLangCap.EnumLangCap.Department)
        lblCompany.text = GetCaption(objLangCap.EnumLangCap.Company)
        lblLocation.text = GetCaption(objLangCap.EnumLangCap.Location)
        lblDeptHead.text = GetCaption(objLangCap.EnumLangCap.DeptHead)

        lblNoDeptCode.text = lblSelect.text & lblDepartment.text & lblCode.text & "."
        lblNoCompCode.text = lblSelect.text & lblCompany.text & lblCode.text & "."
        lblNoLocCode.text = lblSelect.text & lblLocation.text & lblCode.text & "."

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


    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_DEPT_GET"
        Dim strParam As String = strSelectedDeptCode        
        Dim intErrNo As Integer

        Try
            intErrNo = objHRSetup.mtdGetDept(strOpCd, _
                                             strParam, _
                                             objDeptDs, _
                                             True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblHiddenSts.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Status"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindDeptCode(strSelectedDeptCode)
            onLoad_BindCompCode(Trim(objDeptDs.Tables(0).Rows(0).Item("CompCode")))
            onLoad_BindLocCode(Trim(objDeptDs.Tables(0).Rows(0).Item("CompCode")), _
                               Trim(objDeptDs.Tables(0).Rows(0).Item("LocCode")))
            onLoad_BindEmpCode(Trim(objDeptDs.Tables(0).Rows(0).Item("CompCode")), _
                               Trim(objDeptDs.Tables(0).Rows(0).Item("LocCode")), _
                               strSelectedDeptCode, _
                               Trim(objDeptDs.Tables(0).Rows(0).Item("DeptHead")))
            onLoad_BindButton()
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_NODEPT&errmesg=" & lblNoRecord.Text & "&redirect=hr/setup/HR_setup_Deptlist.aspx")
        End If
    End Sub

    Sub onLoad_BindButton()
        ddlDeptCode.Enabled = False
        ddlCompCode.Enabled = False
        ddlLocCode.Enabled = False
        ddlDeptHead.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objHRSetup.EnumDeptStatus.Active
                ddlCompCode.Enabled = True
                ddlLocCode.Enabled = True
                ddlDeptHead.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumDeptStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                ddlDeptCode.Enabled = True
                ddlCompCode.Enabled = True
                ddlLocCode.Enabled = True
                ddlDeptHead.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_BindDeptCode(ByVal pv_strDeptCode As String)
        Dim strOpCd_UnUsed As String = "HR_CLSSETUP_DEPT_UNUSED_DEPTCODE_GET"
        Dim strOpCd_All As String = "HR_CLSSETUP_DEPT_DEPTCODE_GET"
        Dim strOpCd As String = IIf(intStatus = 0, strOpCd_UnUsed, strOpCd_All)
        Dim strParam As String = ""
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objHRSetup.mtdGetDept(strOpCd, _
                                             strParam, _
                                             objDeptCodeDs, _
                                             True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEPTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objDeptCodeDs.Tables(0).Rows.Count - 1
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("DeptCode") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("DeptCode"))
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("DeptCode")) & " (" & Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDeptCodeDs.Tables(0).Rows(intCnt).Item("DeptCode") = pv_strDeptCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDeptCodeDs.Tables(0).NewRow()
        dr("DeptCode") = ""
        dr("Description") = lblList.text & lblDepartment.text
        objDeptCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDeptCode.DataSource = objDeptCodeDs.Tables(0)
        ddlDeptCode.DataValueField = "DeptCode"
        ddlDeptCode.DataTextField = "Description"
        ddlDeptCode.DataBind()
        ddlDeptCode.SelectedIndex = intSelectedIndex
    End Sub


    Sub onLoad_BindCompCode(ByVal pv_strCompCode As String)
        Dim strOpCd As String = "ADMIN_CLSCOMP_COMPANYLIST_GET"
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objAdminComp.mtdGetCompList(strOpCd, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   objCompDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_COMP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objCompDs.Tables(0).Rows.Count - 1
            objCompDs.Tables(0).Rows(intCnt).Item("CompCode") = Trim(objCompDs.Tables(0).Rows(intCnt).Item("CompCode"))
            objCompDs.Tables(0).Rows(intCnt).Item("CompName") = Trim(objCompDs.Tables(0).Rows(intCnt).Item("CompCode")) & " (" & Trim(objCompDs.Tables(0).Rows(intCnt).Item("CompName")) & ")"
            If objCompDs.Tables(0).Rows(intCnt).Item("CompCode") = pv_strCompCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objCompDs.Tables(0).NewRow()
        dr("CompCode") = ""
        dr("CompName") = lblList.text & lblCompany.text
        objCompDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlCompCode.DataSource = objCompDs.Tables(0)
        ddlCompCode.DataValueField = "CompCode"
        ddlCompCode.DataTextField = "CompName"
        ddlCompCode.DataBind()
        ddlCompCode.SelectedIndex = intSelectedIndex
        ddlCompCode.AutoPostBack = True
    End Sub


    Sub onChange_CompCode(Sender As Object, E As EventArgs)
        onLoad_BindLocCode(ddlCompCode.SelectedItem.Value, ddlLocCode.SelectedItem.Value)
    End Sub

    Sub onLoad_BindLocCode(ByVal pv_strCompCode As String, ByVal pv_strLocCode As String)
        Dim strOpCd As String = "ADMIN_CLSCOMP_COMPANY_LOCATION_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = pv_strCompCode
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objAdminComp.mtdGetCompLocList(strOpCd, _
                                                      objLocDs, _
                                                      strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_LOC_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objLocDs.Tables(0).Rows(intCnt).Item("LocCode"))
            objLocDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objLocDs.Tables(0).Rows(intCnt).Item("LocCode")) & " (" & Trim(objLocDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = pv_strLocCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblList.text & lblLocation.text
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocCode.DataSource = objLocDs.Tables(0)
        ddlLocCode.DataValueField = "LocCode"
        ddlLocCode.DataTextField = "Description"
        ddlLocCode.DataBind()
        ddlLocCode.SelectedIndex = intSelectedIndex
        ddlLocCode.AutoPostBack = True
    End Sub

    Sub onChange_LocCode(Sender As Object, E As EventArgs)
        onLoad_BindEmpCode(ddlCompCode.SelectedItem.Value, _
                           ddlLocCode.SelectedItem.Value, _
                           ddlDeptCode.SelectedItem.Value, _
                           Request.Form("ddlDeptHead"))
    End Sub

    Sub onLoad_BindEmpCode(ByVal pv_strCompCode As String, _
                           ByVal pv_strLocCode As String, _
                           ByVal pv_strDeptCode As String, _
                           ByVal pv_strEmpCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_DEPT_EMPLOYEE_GET"
        Dim dr As DataRow
        Dim strParam As String = pv_strCompCode & "|" & pv_strLocCode & "|" & pv_strDeptCode
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objHRSetup.mtdGetDept_Head(strOpCd, _
                                                  strParam, _
                                                  objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_HEAD_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode")) & " (" & Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
            If objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = pv_strEmpCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objEmpDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = lblList.text & lblDeptHead.text
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDeptHead.DataSource = objEmpDs.Tables(0)
        ddlDeptHead.DataValueField = "EmpCode"
        ddlDeptHead.DataTextField = "EmpName"
        ddlDeptHead.DataBind()
        ddlDeptHead.SelectedIndex = intSelectedIndex
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "HR_CLSSETUP_DEPT_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_DEPT_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_DEPT_GET"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If ddlDeptCode.SelectedItem.Value = "" Then
            lblNoDeptCode.Visible = True
            Exit Sub
        ElseIf ddlCompCode.SelectedItem.Value = "" Then
            lblNoCompCode.Visible = True
            Exit Sub
        ElseIf ddlLocCode.SelectedItem.Value = "" Then
            lblNoLocCode.Visible = True
            Exit Sub
        Else
            If strCmdArgs = "Save" Then
                strParam = ddlDeptCode.SelectedItem.Value
                Try
                    intErrNo = objHRSetup.mtdGetDept(strOpCd_Get, _
                                                    strParam, _
                                                    objDeptDs, _
                                                    True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                If objDeptDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                    lblErrDupDept.Visible = True
                Else
                    blnIsUpdate = IIf(intStatus = 0, False, True)

                    strSelectedDeptCode = ddlDeptCode.SelectedItem.Value
                    deptcode.Value = strSelectedDeptCode
                    strParam = ddlDeptCode.SelectedItem.Value & "|" & _
                               ddlCompCode.SelectedItem.Value & "|" & _
                               ddlLocCode.SelectedItem.Value & "|" & _
                               Request.Form("ddlDeptHead") & "|" & _
                               objHRSetup.EnumDeptStatus.Active
                    Try
                        intErrNo = objHRSetup.mtdUpdDept(strOpCd_Add, _
                                                        strOpCd_Upd, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        blnIsUpdate)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_Deptdet.aspx")
                    End Try
                End If

            ElseIf strCmdArgs = "Del" Then
                strParam = ddlDeptCode.SelectedItem.Value & "||||" & objHRSetup.EnumDeptStatus.Deleted
                Try
                    intErrNo = objHRSetup.mtdUpdDept(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_Deptdet.aspx?deptcode=" & strSelectedDeptCode)
                End Try

            ElseIf strCmdArgs = "UnDel" Then
                strParam = ddlDeptCode.SelectedItem.Value & "||||" & objHRSetup.EnumDeptStatus.Active
                Try
                    intErrNo = objHRSetup.mtdUpdDept(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_Deptdet.aspx?deptcode=" & strSelectedDeptCode)
                End Try
            End If

            If strSelectedDeptCode <> "" Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_Deptlist.aspx")
    End Sub


End Class
