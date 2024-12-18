Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic
Imports agri.HR
Imports agri.PWSystem
Imports agri.GlobalHdl

Public Class HR_trx_EmployeeStat : Inherits Page

    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblEmpName As Label
    Protected WithEvents txtJamsostekNo As TextBox
    Protected WithEvents txtJKKRefNo As TextBox
    Protected WithEvents ddlJKKCode As DropDownList
    Protected WithEvents txtJKKID As TextBox
    Protected WithEvents txtJKRefNo As TextBox
    Protected WithEvents ddlJKCode As DropDownList
    Protected WithEvents txtJKID As TextBox
    Protected WithEvents txtJHTRefNo As TextBox
    Protected WithEvents ddlJHTCode As DropDownList
    Protected WithEvents txtJHTID As TextBox
    Protected WithEvents txtJPKRefNo As TextBox
    Protected WithEvents ddlJPKCode As DropDownList
    Protected WithEvents txtJPKID As TextBox
    Protected WithEvents txtTaxRefNo As TextBox
    Protected WithEvents ddlTaxCode As DropDownList
    Protected WithEvents ddlTaxBranch As DropDownList
    Protected WithEvents txtLevyPort As TextBox
    Protected WithEvents txtLevyArriveDate As TextBox
    Protected WithEvents txtLevyCardNo As TextBox
    Protected WithEvents txtLevyImgNo As TextBox
    Protected WithEvents lblEmpStatus As Label
    Protected WithEvents rfvJamsostekNo As RequiredFieldValidator

    Protected WithEvents lblJamsostek As Label
    Protected WithEvents lblJKK As Label
    Protected WithEvents lblJK As Label
    Protected WithEvents lblJHT As Label
    Protected WithEvents lblJPK As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblJKKCode As Label
    Protected WithEvents lblJKCode As Label
    Protected WithEvents lblJHTCode As Label
    Protected WithEvents lblJPKCode As Label
    Protected WithEvents lblPleaseEnterRefNo As Label

    Protected WithEvents lblErrJKKID As Label
    Protected WithEvents lblErrJKID As Label
    Protected WithEvents lblErrJHTID As Label
    Protected WithEvents lblErrJPKID As Label
    Protected WithEvents lblErrTaxRefNo As Label
    Protected WithEvents lblErrTaxBranch As Label
    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrLevyArriveDate As Label
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents btnSelLevyArriveDate As Image

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbDetails As LinkButton
    Protected WithEvents lbPayroll As LinkButton
    Protected WithEvents lbEmployment As LinkButton
    Protected WithEvents lbFamily As LinkButton
    Protected WithEvents lbQualific As LinkButton
    Protected WithEvents lbSkill As LinkButton
    Protected WithEvents lbCareerProg As LinkButton

    Dim objHR As New agri.HR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objEmpStatDs As New Object()
    Dim objJamDs As New Object()
    Dim objTaxDs As New Object()
    Dim objTaxBranchDs As New Object()
    Dim objLangCapDs As New Object()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long

    Dim strSelectedEmpCode As String = ""
    Dim strSelectedEmpName As String = ""
    Dim strSelectedEmpStatus As String = ""
    Dim strSortExpression As String = "EmpCode"
    Dim strDateFmt As String
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strDateFmt = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")

        lblErrJKKID.Visible = False
        lblErrJKID.Visible = False
        lblErrJHTID.Visible = False
        lblErrJPKID.Visible = False
        lblErrTaxRefNo.Visible = False
        lblErrTaxBranch.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeStatutory), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblRedirect.Text = Request.QueryString("redirect")
            strSelectedEmpCode = Trim(IIf(Request.QueryString("EmpCode") <> "", Request.QueryString("EmpCode"), Request.Form("EmpCode")))
            strSelectedEmpName = Trim(IIf(Request.QueryString("EmpName") <> "", Request.QueryString("EmpName"), Request.Form("EmpName")))
            strSelectedEmpStatus = Trim(IIf(Request.QueryString("EmpStatus") <> "", Request.QueryString("EmpStatus"), Request.Form("EmpStatus")))
            onload_GetLangCap()
            If Not IsPostBack Then
                If strSelectedEmpCode <> "" Then
                    onLoad_Display()   
                End If
                onload_LinkButton()     
            End If
        End If
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lbCareerProg.Text = GetCaption(objLangCap.EnumLangCap.CareerProgress)   
        lblJamsostek.Text = GetCaption(objLangCap.EnumLangCap.Jamsostek)
        lblJKK.Text = lblJamsostek.Text & " - " & GetCaption(objLangCap.EnumLangCap.JKK)
        lblJK.Text = lblJamsostek.Text & " - " & GetCaption(objLangCap.EnumLangCap.JK)
        lblJHT.Text = lblJamsostek.Text & " - " & GetCaption(objLangCap.EnumLangCap.JHT)
        lblJPK.Text = lblJamsostek.Text & " - " & GetCaption(objLangCap.EnumLangCap.JPK)

        lblJKKCode.Text = GetCaption(objLangCap.EnumLangCap.JKK) & lblCode.Text
        lblJKCode.Text = GetCaption(objLangCap.EnumLangCap.JK) & lblCode.Text
        lblJHTCode.Text = GetCaption(objLangCap.EnumLangCap.JHT) & lblCode.Text
        lblJPKCode.Text = GetCaption(objLangCap.EnumLangCap.JPK) & lblCode.Text
        rfvJamsostekNo.Text = lblPleaseEnterRefNo.Text & lblJamsostek.Text & "."
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_STATUTORY_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
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

    Private Sub lbFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFamily.Click
        Response.Redirect("HR_trx_EmployeeFamList.aspx?redirect=empfam&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub

    Private Sub lbQualific_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbQualific.Click
        Response.Redirect("HR_trx_EmployeeQlfList.aspx?redirect=empqlf&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)

    End Sub

    Private Sub lbSkill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSkill.Click
        Response.Redirect("HR_trx_EmployeeSkillList.aspx?redirect=empskill&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)

    End Sub

    Private Sub lbCareerProg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCareerProg.Click
        Response.Redirect("HR_trx_EmployeeCPList.aspx?redirect=empcp&EmpCode=" & lblEmpCode.Text & "&EmpName=" & lblEmpName.Text & "&EmpStatus=" & lblEmpStatus.Text)
    End Sub


    Sub onLoad_Display()
        Dim strOpCd_EmpStat As String = "HR_CLSTRX_STATUTORY_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer

        lblEmpCode.Text = strSelectedEmpCode
        lblEmpName.Text = strSelectedEmpName
        lblEmpStatus.Text = strSelectedEmpStatus

        strParam = strSelectedEmpCode & "|||" & strSortExpression & "|"

        Try
            intErrNo = objHR.mtdGetEmployeeStat(strOpCd_EmpStat, strParam, objEmpStatDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_STATUTORY_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objEmpStatDs.Tables(0).Rows.Count > 0 Then
            txtJamsostekNo.Text = objEmpStatDs.Tables(0).Rows(0).Item("JamsostekNo")
            txtJKKRefNo.Text = objEmpStatDs.Tables(0).Rows(0).Item("JKKRefNo")
            txtJKKID.Text = objEmpStatDs.Tables(0).Rows(0).Item("JKKID")
            txtJKRefNo.Text = objEmpStatDs.Tables(0).Rows(0).Item("JKRefNo")
            txtJKID.Text = objEmpStatDs.Tables(0).Rows(0).Item("JKID")
            txtJHTRefNo.Text = objEmpStatDs.Tables(0).Rows(0).Item("JHTRefNo")
            txtJHTID.Text = objEmpStatDs.Tables(0).Rows(0).Item("JHTID")
            txtJPKRefNo.Text = objEmpStatDs.Tables(0).Rows(0).Item("JPKRefNo")
            txtJPKID.Text = objEmpStatDs.Tables(0).Rows(0).Item("JPKID")
            txtTaxRefNo.Text = objEmpStatDs.Tables(0).Rows(0).Item("TaxRefNo")
            txtLevyPort.Text = objEmpStatDs.Tables(0).Rows(0).Item("LevyPort")
            txtLevyArriveDate.Text = objGlobal.GetShortDate(strDateFmt, objEmpStatDs.Tables(0).Rows(0).Item("LevyArriveDate"))
            txtLevyCardNo.Text = objEmpStatDs.Tables(0).Rows(0).Item("LevyCardNo")
            txtLevyImgNo.Text = objEmpStatDs.Tables(0).Rows(0).Item("LevyImgNo")

            BindJamsostek(objHRSetup.EnumJamsostekCategory.JKK, objEmpStatDs.Tables(0).Rows(0).Item("JKKCode"))
            BindJamsostek(objHRSetup.EnumJamsostekCategory.JK, objEmpStatDs.Tables(0).Rows(0).Item("JKCode"))
            BindJamsostek(objHRSetup.EnumJamsostekCategory.JHT, objEmpStatDs.Tables(0).Rows(0).Item("JHTCode"))
            BindJamsostek(objHRSetup.EnumJamsostekCategory.JPK, objEmpStatDs.Tables(0).Rows(0).Item("JPKCode"))
            BindTax(objEmpStatDs.Tables(0).Rows(0).Item("TaxCode"))
            BindTaxBranch(objEmpStatDs.Tables(0).Rows(0).Item("TaxBranch"))      
        Else
            BindJamsostek(objHRSetup.EnumJamsostekCategory.JKK, "")
            BindJamsostek(objHRSetup.EnumJamsostekCategory.JK, "")
            BindJamsostek(objHRSetup.EnumJamsostekCategory.JHT, "")
            BindJamsostek(objHRSetup.EnumJamsostekCategory.JPK, "")
            BindTax("")
            BindTaxBranch("")       
        End If

        Select case CInt(lblEmpStatus.Text)
            case objHR.EnumEmpStatus.Active
                txtJamsostekNo.Enabled = True
                txtJKKRefNo.Enabled = True
                ddlJKKCode.Enabled = True
                txtJKKID.Enabled = True
                txtJKRefNo.Enabled = True
                ddlJKCode.Enabled = True
                txtJKID.Enabled = True
                txtJHTRefNo.Enabled = True
                ddlJHTCode.Enabled = True
                txtJHTID.Enabled = True
                txtJPKRefNo.Enabled = True
                ddlJPKCode.Enabled = True
                txtJPKID.Enabled = True
                txtTaxRefNo.Enabled = True
                ddlTaxCode.Enabled = True
                ddlTaxBranch.Enabled = True
                txtLevyPort.Enabled = True
                txtLevyArriveDate.Enabled = True
                txtLevyCardNo.Enabled = True
                txtLevyImgNo.Enabled = True
            case objHR.EnumEmpStatus.Deleted
                txtJamsostekNo.Enabled = False
                txtJKKRefNo.Enabled = False
                ddlJKKCode.Enabled = False
                txtJKKID.Enabled = False
                txtJKRefNo.Enabled = False
                ddlJKCode.Enabled = False
                txtJKID.Enabled = False
                txtJHTRefNo.Enabled = False
                ddlJHTCode.Enabled = False
                txtJHTID.Enabled = False
                txtJPKRefNo.Enabled = False
                ddlJPKCode.Enabled = False
                txtJPKID.Enabled = False
                txtTaxRefNo.Enabled = False
                ddlTaxCode.Enabled = False
                ddlTaxBranch.Enabled = False
                txtLevyPort.Enabled = False
                txtLevyArriveDate.Enabled = False
                txtLevyCardNo.Enabled = False
                txtLevyImgNo.Enabled = False
                btnSelLevyArriveDate.visible = False
                btnSave.visible = False
        End Select

    End Sub

    Sub BindJamsostek(ByVal pv_strCategory As String, ByVal pv_strCode As String)
        Dim strOpCdGet As String = "HR_CLSSETUP_JAMSOSTEK_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSort As String
        Dim strSearch As String
        
        strSort = "order by jam.JamCode"

        strSearch = "and jam.Status = '" & objHRSetup.EnumJamsostekStatus.Active & "' " & _
                    "and jam.Category = '" & Trim(pv_strCategory) & "' " 

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCdGet, _
                                                   strParam, _
                                                   0, _
                                                   objJamDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPSTAT_GET_JAMCODE&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try
        
        For intCnt = 0 To objJamDs.Tables(0).Rows.Count - 1
            If Trim(objJamDs.Tables(0).Rows(intCnt).Item("JamCode")) = Trim(pv_strCode) Then
                intSelectIndex = intCnt + 1
                Exit For
            End If
        Next

        Select Case CInt(Trim(pv_strCategory))
            Case objHRSetup.EnumJamsostekCategory.JKK
                dr = objJamDs.Tables(0).NewRow()
                dr("JamCode") = ""
                dr("_Description") = lblSelect.Text & lblJKKCode.Text
                objJamDs.Tables(0).Rows.InsertAt(dr, 0)

                ddlJKKCode.DataSource = objJamDs.Tables(0)
                ddlJKKCode.DataValueField = "JamCode"
                ddlJKKCode.DataTextField = "_Description"
                ddlJKKCode.DataBind()
                ddlJKKCode.SelectedIndex = intSelectIndex

            Case objHRSetup.EnumJamsostekCategory.JK
                dr = objJamDs.Tables(0).NewRow()
                dr("JamCode") = ""
                dr("_Description") = lblSelect.Text & lblJKCode.Text
                objJamDs.Tables(0).Rows.InsertAt(dr, 0)

                ddlJKCode.DataSource = objJamDs.Tables(0)
                ddlJKCode.DataValueField = "JamCode"
                ddlJKCode.DataTextField = "_Description"
                ddlJKCode.DataBind()
                ddlJKCode.SelectedIndex = intSelectIndex

            Case objHRSetup.EnumJamsostekCategory.JHT
                dr = objJamDs.Tables(0).NewRow()
                dr("JamCode") = ""
                dr("_Description") = lblSelect.Text & lblJHTCode.Text
                objJamDs.Tables(0).Rows.InsertAt(dr, 0)

                ddlJHTCode.DataSource = objJamDs.Tables(0)
                ddlJHTCode.DataValueField = "JamCode"
                ddlJHTCode.DataTextField = "_Description"
                ddlJHTCode.DataBind()
                ddlJHTCode.SelectedIndex = intSelectIndex

            Case objHRSetup.EnumJamsostekCategory.JPK
                dr = objJamDs.Tables(0).NewRow()
                dr("JamCode") = ""
                dr("_Description") = lblSelect.Text & lblJPKCode.Text
                objJamDs.Tables(0).Rows.InsertAt(dr, 0)

                ddlJPKCode.DataSource = objJamDs.Tables(0)
                ddlJPKCode.DataValueField = "JamCode"
                ddlJPKCode.DataTextField = "_Description"
                ddlJPKCode.DataBind()
                ddlJPKCode.SelectedIndex = intSelectIndex

        End Select
    End Sub

    Sub BindTax(ByVal pv_strSelTaxCode As String)
        Dim strOpCdGet As String = "HR_ClSSETUP_TAX_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow
        
        strParam = "order by tax.TaxCode" & "|" & _
                   "and tax.Status = '" & objHRSetup.EnumTaxStatus.Active & "' "

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objTaxDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPSTAT_GET_TAX&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objTaxDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objTaxDs.Tables(0).Rows.Count - 1              
                If Trim(objTaxDs.Tables(0).Rows(intCnt).Item("TaxCode")) = Trim(pv_strSelTaxCode)
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objTaxDs.Tables(0).NewRow()
        dr("TaxCode") = ""
        dr("_Description") = "Select Tax Code"
        objTaxDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTaxCode.DataSource = objTaxDs.Tables(0)
        ddlTaxCode.DataTextField = "_Description"
        ddlTaxCode.DataValueField = "TaxCode"
        ddlTaxCode.DataBind()
        ddlTaxCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindTaxBranch(ByVal pv_strTaxBranch As String)
        Dim strOpCdGet As String = "HR_CLSSETUP_TAXBRANCH_SEARCH"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow
        
        strParam = "||" & objHRSetup.EnumTaxBranchStatus.Active & "||TB.TaxBranchCode|" 

        Try
            intErrNo = objHRSetup.mtdGetTaxBranch(strOpCdGet, strParam, objTaxBranchDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPSTAT_GET_TAXBRANCH&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objTaxBranchDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objTaxBranchDs.Tables(0).Rows.Count - 1
                If Trim(objTaxBranchDs.Tables(0).Rows(intCnt).Item("TaxBranchCode")) = Trim(pv_strTaxBranch)
                    intSelectedIndex = intCnt + 1
                    Exit For
                End If
            Next
        End If

        dr = objTaxBranchDs.Tables(0).NewRow()
        dr("TaxBranchCode") = ""
        dr("Description") = "Select Tax Branch"
        objTaxBranchDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTaxBranch.DataSource = objTaxBranchDs.Tables(0)
        ddlTaxBranch.DataTextField = "Description"
        ddlTaxBranch.DataValueField = "TaxBranchCode"
        ddlTaxBranch.DataBind()
        ddlTaxBranch.SelectedIndex = intSelectedIndex
    End Sub

    Sub btnSave_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_GetDet As String = "HR_CLSTRX_EMPLOYEE_GET"
        Dim strOpCd_GetPay As String = "HR_CLSTRX_PAYROLL_GET"
        Dim strOpCd_GetEmp As String = "HR_CLSTRX_EMPLOYMENT_GET"
        Dim strOpCd_GetStat As String = "HR_CLSTRX_STATUTORY_GET"
        Dim strOpCd_UpdStat As String = "HR_CLSTRX_STATUTORY_UPD"
        Dim strOpCd_AddStat As String = "HR_CLSTRX_STATUTORY_ADD"
        Dim strOpCd_UpdDet As String = "HR_CLSTRX_EMPLOYEE_UPD"
        Dim strEmpCode As String = lblEmpCode.Text
        Dim strJamsostekNo As String = txtJamsostekNo.Text
        Dim strJKKCode As String = ddlJKKCode.SelectedItem.Value
        Dim strJKKRefNo As String = txtJKKRefNo.Text
        Dim strJKKID As String = txtJKKID.Text
        Dim strJKCode As String = ddlJKCode.SelectedItem.Value
        Dim strJKRefNo As String = txtJKRefNo.Text
        Dim strJKID As String = txtJKID.Text
        Dim strJHTCode As String = ddlJHTCode.SelectedItem.Value
        Dim strJHTRefNo As String = txtJHTRefNo.Text
        Dim strJHTID As String = txtJHTID.Text
        Dim strJPKCode As String = ddlJPKCode.SelectedItem.Value
        Dim strJPKRefNo As String = txtJPKRefNo.Text
        Dim strJPKID As String = txtJPKID.Text
        Dim strTaxRefNo As String = txtTaxRefNo.Text
        Dim strTaxCode As String = ddlTaxCode.SelectedItem.Value
        Dim strTaxBranch As String = ddlTaxBranch.SelectedItem.Value
        Dim strLevyPort As String = txtLevyPort.Text
        Dim strLevyArriveDate As String = txtLevyArriveDate.Text
        Dim strLevyCardNo As String = txtLevyCardNo.Text
        Dim strLevyImgNo As String = txtLevyImgNo.Text
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim intErrNo As Integer
        Dim strChkParam As String
        Dim strParam As String

        If Trim(strJKKCode) <> "" Then
            If Trim(strJKKID) = "" Then
                lblErrJKKID.Visible = True
                Exit Sub
            End If
        End If

        If Trim(strJKCode) <> "" Then
            If Trim(strJKID) = "" Then
                lblErrJKID.Visible = True
                Exit Sub
            End If
        End If
    
        If Trim(strJHTCode) <> "" Then
            If Trim(strJHTID) = "" Then
                lblErrJHTID.Visible = True
                Exit Sub
            End If
        End If

        If Trim(strJPKCode) <> "" Then
            If Trim(strJPKID) = "" Then
                lblErrJPKID.Visible = True
                Exit Sub
            End If
        End If

        If strTaxCode <> "" Then
            If Trim(strTaxRefNo) = "" Then
                lblErrTaxRefNo.Visible = True
                Exit Sub
            End If
            If Trim(strTaxBranch) = "" Then 
                lblErrTaxBranch.Visible = True
                Exit Sub
            End If
        End If

        If strLevyArriveDate <> "" Then
            If objGlobal.mtdValidInputDate(strDateFmt, _
                                           strLevyArriveDate, _
                                           objFormatDate, _
                                           objActualDate) = False Then
                lblErrLevyArriveDate.Text = lblErrLevyArriveDate.Text & objFormatDate
                lblErrLevyArriveDate.Visible = True
                Exit Sub
            Else
                strLevyArriveDate = objActualDate
            End If
        End If

        strParam = strEmpCode & "|" & _
                   strJamsostekNo & "|" & _
                   strJKKCode & "|" & _
                   strJKKRefNo & "|" & _
                   strJKKID & "|" & _
                   strJKCode & "|" & _
                   strJKRefNO & "|" & _
                   strJKID & "|" & _
                   strJHTCode & "|" & _
                   strJHTRefNo & "|" & _
                   strJHTID & "|" & _
                   strJPKCode & "|" & _
                   strJPKRefNo & "|" & _
                   strJPKID & "|" & _
                   strTaxCode & "|" & _
                   strTaxRefNo & "|" & _
                   strTaxBranch & "|" & _
                   strLevyPort & "|" & _
                   strLevyArriveDate & "|" & _
                   strLevyCardNo & "|" & _
                   strLevyImgNo
        Try
            intErrNo = objHR.mtdUpdEmployeeStat(strOpCd_GetStat, _
                                                strOpCd_UpdStat, _
                                                strOpCd_AddStat, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPSTAT_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        strChkParam = strEmpCode & "|" & lblRedirect.Text

        Try
            intErrNo = objHR.mtdCheckEmployee(strOpCd_GetDet, _
                                              strOpCd_GetPay, _
                                              strOpCd_GetEmp, _
                                              strOpCd_GetStat, _
                                              strOpCd_UpdDet, _
                                              strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_TRX_EMPSTAT_UPD_EMPSTATUS&errmesg=" & lblErrMessage.Text & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        strSelectedEmpCode = strEmpCode
        onLoad_Display()
    End Sub

    Sub btnBack_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_trx_EmployeeList.aspx?redirect=" & lblRedirect.Text)
    End Sub

End Class
