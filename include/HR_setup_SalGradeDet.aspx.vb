Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.HR
Imports agri.GlobalHdl

Public Class HR_setup_SalGradeDet : Inherits Page

    Protected WithEvents ddlSalScheme As DropDownList
    Protected WithEvents txtSalGradeCode As Textbox
    Protected WithEvents txtDescription As Textbox
    Protected WithEvents txtMaxSalary As Textbox
    Protected WithEvents txtYearIncAmount As Textbox
    Protected WithEvents txtYearIncRate As Textbox
    Protected WithEvents txtOTRate As Textbox
    Protected WithEvents txtOTLimit As Textbox    
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents sgcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrIncrement As Label
    Protected WithEvents lblErrPercent As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objSalGradeDs As New Object()
    Dim objSalSchemeDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Dim strSelectedSGCode As String = ""
    Dim intStatus As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRSalGrade), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblErrIncrement.Visible = False
            lblErrPercent.Visible = False
            strSelectedSGCode = Trim(IIf(Request.QueryString("sgcode") <> "", Request.QueryString("sgcode"), Request.Form("sgcode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedSGCode <> "" Then
                    sgcode.Value = strSelectedSGCode
                    onLoad_Display()
                Else
                    BindSalScheme("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Sub onLoad_BindButton()
        ddlSalScheme.Enabled = False
        txtSalGradeCode.Enabled = False
        txtDescription.Enabled = False
        txtMaxSalary.Enabled = False
        txtYearIncAmount.Enabled = False
        txtYearIncRate.Enabled = False
        txtOTRate.Enabled = False
        txtOTLimit.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objHRSetup.EnumSalGradeStatus.Active
                txtDescription.Enabled = True
                txtMaxSalary.Enabled = True
                txtYearIncAmount.Enabled = True
                txtYearIncRate.Enabled = True
                txtOTRate.Enabled = True
                txtOTLimit.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumSalGradeStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                ddlSalScheme.Enabled = True
                txtSalGradeCode.Enabled = True
                txtDescription.Enabled = True
                txtMaxSalary.Enabled = True
                txtYearIncAmount.Enabled = True
                txtYearIncRate.Enabled = True
                txtOTRate.Enabled = True
                txtOTLimit.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "HR_CLSSETUP_SALGRADE_GET"
        Dim strParam As String = strSelectedSGCode        
        Dim intErrNo As Integer

        Try
            intErrNo = objHRSetup.mtdGetSalGrade(strOpCd, _
                                                 strParam, _
                                                 objSalGradeDs, _
                                                 True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SALGRADE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtSalGradeCode.Text = strSelectedSGCode
        txtDescription.Text = Trim(objSalGradeDs.Tables(0).Rows(0).Item("Description"))
        txtMaxSalary.Text = objSalGradeDs.Tables(0).Rows(0).Item("MaxBasicSal")
        txtYearIncAmount.Text = objSalGradeDs.Tables(0).Rows(0).Item("YearIncAmt")
        txtYearIncRate.Text = objSalGradeDs.Tables(0).Rows(0).Item("YearIncPercent")
        txtOTRate.Text = objSalGradeDs.Tables(0).Rows(0).Item("OTHourRate")
        txtOTLimit.Text = objSalGradeDs.Tables(0).Rows(0).Item("OTLimit")
        intStatus = CInt(Trim(objSalGradeDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objSalGradeDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objHRSetup.mtdGetCPStatus(Trim(objSalGradeDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objSalGradeDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objSalGradeDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objSalGradeDs.Tables(0).Rows(0).Item("UserName"))

        BindSalScheme(Trim(objSalGradeDs.Tables(0).Rows(0).Item("SalSchemeCode")))
        onLoad_BindButton()
    End Sub

    Sub BindSalScheme(ByVal pv_strSalSchemeCode As String)
        Dim strOpCode As String = "HR_CLSSETUP_SALSCHEME_LIST_GET"
        Dim strParam As String = strSelectedSGCode        
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        Try
            strParam = "Order By SAL.SalSchemeCode ASC|And SAL.Status = '" & objHRSetup.EnumSalSchemeStatus.Active & "'"
            intErrNo = objHRSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   objHRSetup.EnumHRMasterType.SalScheme, _
                                                   objSalSchemeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SALSCHEME_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For intCnt = 0 To objSalSchemeDs.Tables(0).Rows.Count - 1
            objSalSchemeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode") = Trim(objSalSchemeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode"))
            objSalSchemeDs.Tables(0).Rows(intCnt).Item("Description") = objSalSchemeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode") & " (" & Trim(objSalSchemeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objSalSchemeDs.Tables(0).Rows(intCnt).Item("SalSchemeCode") = pv_strSalSchemeCode Then
                intSelectIndex = intCnt
            End If
        Next

        ddlSalScheme.DataSource = objSalSchemeDs.Tables(0)
        ddlSalScheme.DataValueField = "SalSchemeCode"
        ddlSalScheme.DataTextField = "Description"
        ddlSalScheme.DataBind()
        ddlSalScheme.SelectedIndex = intSelectIndex
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "HR_CLSSETUP_SALGRADE_ADD"
        Dim strOpCd_Upd As String = "HR_CLSSETUP_SALGRADE_UPD"
        Dim strOpCd_Get As String = "HR_CLSSETUP_SALGRADE_GET"
        Dim strOpCd_Sts As String = "HR_CLSSETUP_SALGRADE_STATUS_UPD"
        Dim strOpCd As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim intBlackList As Integer
        Dim intPeriod As Integer
        Dim strParam As String = ""

        If strCmdArgs = "Save" Then
            If (txtYearIncAmount.Text = "" And txtYearIncRate.Text = "") Or _
               (txtYearIncAmount.Text <> "0" And txtYearIncRate.Text <> "0") Or _
               (txtYearIncAmount.Text = "0" And txtYearIncRate.Text = "0") Then
                lblErrIncrement.Visible = True
                Exit Sub
            ElseIf txtYearIncRate.Text <> "" Then
                If CDbl(txtYearIncRate.Text) > 100 Then
                    lblErrPercent.Visible = True
                    Exit Sub
                End If
            End If

            strParam = Trim(txtSalGradeCode.Text)
            Try
                intErrNo = objHRSetup.mtdGetSalGrade(strOpCd_Get, _
                                                    strParam, _
                                                    objSalGradeDs, _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SALGRADE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objSalGradeDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDup.Visible = True
                Exit Sub
            Else
                strSelectedSGCode = Trim(txtSalGradeCode.Text)
                sgcode.Value = strSelectedSGCode
                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)

                strParam = ddlSalScheme.SelectedItem.Value & "|" & _
                           Trim(txtSalGradeCode.Text) & "|" & _
                           Trim(txtDescription.Text) & "|" & _
                           Trim(txtMaxSalary.Text) & "|" & _
                           Trim(txtYearIncAmount.Text) & "|" & _
                           Trim(txtYearIncRate.Text) & "|" & _
                           Trim(txtOTRate.Text) & "|" & _
                           Trim(txtOTLimit.Text) & "|" & _
                           objHRSetup.EnumSalGradeStatus.Active
                Try
                    intErrNo = objHRSetup.mtdUpdSalGrade(strOpCd, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        False)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SALGRADE_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_SalGradeDet.aspx")
                End Try
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = strSelectedSGCode & "|" & objHRSetup.EnumSalGradeStatus.Deleted
            Try
                intErrNo = objHRSetup.mtdUpdSalGrade(strOpCd_Sts, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SALGRADE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_SalGradeDet.aspx?sgcode=" & strSelectedSGCode)
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = strSelectedSGCode & "|" & objHRSetup.EnumSalGradeStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdSalGrade(strOpCd_Sts, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_SALGRADE_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/HR_setup_SalGradeDet.aspx?sgcode=" & strSelectedSGCode)
            End Try
        End If

        If strSelectedSGCode <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_SalGradeList.aspx")
    End Sub

End Class
