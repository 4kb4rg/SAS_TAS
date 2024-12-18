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

Public Class HR_setup_JamsostekDet : Inherits Page

    Protected WithEvents txtJamCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents ddlEmprDeCode As DropDownList
    Protected WithEvents ddlEmpeDeCode As DropDownList
    Protected WithEvents txtEmprRate As TextBox
    Protected WithEvents txtEmpeRate As TextBox
    Protected WithEvents rbJKK As RadioButton
    Protected WithEvents rbJK As RadioButton
    Protected WithEvents rbJHT As RadioButton
    Protected WithEvents rbJPK As RadioButton
    
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrSelectOne As Label
    Protected WithEvents lblErrEmprRate As Label
    Protected WithEvents lblErrEmpeRate As Label

    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton

    Protected WithEvents lblTitle As Label
    Protected WithEvents lblJamCode As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblCode As Label

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objJamDs As New Object()
    Dim objEmprADDs As New Object()
    Dim objEmpeADDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strSelJamCode As String = ""
    Dim intStatus As Integer
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intHRAR = Session("SS_HRAR")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRJamsostek), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblErrSelectOne.Visible = False
            lblErrEmprRate.Visible = False
            lblErrEmpeRate.Visible = False
            strSelJamCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)

            onload_GetLangCap()

            If Not IsPostBack Then
                If strSelJamCode <> "" Then
                    tbcode.Value = strSelJamCode
                    onLoad_Display()
                    onLoad_BindButton()
                Else
                    BindADCode("", "")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_BindButton()
        txtJamCode.Enabled = False
        txtDesc.Enabled = False
        ddlEmprDeCode.Enabled = False
        ddlEmpeDeCode.Enabled = False
        txtEmprRate.Enabled = False
        txtEmpeRate.Enabled = False
        rbJKK.Enabled = False
        rbJK.Enabled = False
        rbJHT.Enabled = False
        rbJPK.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objHRSetup.EnumJamsostekStatus.Active
                txtDesc.Enabled = True
                ddlEmprDeCode.Enabled = True
                ddlEmpeDeCode.Enabled = True
                txtEmprRate.Enabled = True
                txtEmpeRate.Enabled = True
                rbJKK.Enabled = True
                rbJK.Enabled = True
                rbJHT.Enabled = True
                rbJPK.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objHRSetup.EnumJamsostekStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtJamCode.Enabled = True
                txtDesc.Enabled = True
                ddlEmprDeCode.Enabled = True
                ddlEmpeDeCode.Enabled = True
                txtEmprRate.Enabled = True
                txtEmpeRate.Enabled = True
                rbJKK.Enabled = True
                rbJK.Enabled = True
                rbJHT.Enabled = True
                rbJPK.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub BindADCode(ByVal pv_strEmprDeCode As String, ByVal pv_strEmpeDeCode As String)
        Dim strOpCdGet As String = "PR_CLSSETUP_ADCODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSort As String
        Dim strSearch As String
        
        strSort = "order by ad.ADCode"

        strSearch = "and ad.Status = '" & objPRSetup.EnumADStatus.Active & "' " & _
                    "and ad.ADType = '" & objPRSetup.EnumADType.MemoItem & "' " 

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCdGet, _
                                                   strParam, _
                                                   0, _
                                                   objEmprADDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_JAMSOSTEKDET_GETEMPRDECODE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_jamsosteklist.aspx")
        End Try
        
        For intCnt = 0 To objEmprADDs.Tables(0).Rows.Count - 1
            If Trim(objEmprADDs.Tables(0).Rows(intCnt).Item("ADCode")) = Trim(pv_strEmprDeCode) Then
                intSelectIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objEmprADDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("_Description") = "Select Employer Deduction Code"
        objEmprADDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmprDeCode.DataSource = objEmprADDs.Tables(0)
        ddlEmprDeCode.DataValueField = "ADCode"
        ddlEmprDeCode.DataTextField = "_Description"
        ddlEmprDeCode.DataBind()
        ddlEmprDeCode.SelectedIndex = intSelectIndex

        intSelectIndex = 0
        strSearch = "and ad.Status = '" & objPRSetup.EnumADStatus.Active & "' " & _
                    "and ad.ADType = '" & objPRSetup.EnumADType.Deduction & "' " 

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCdGet, _
                                                   strParam, _
                                                   0, _
                                                   objEmpeADDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_JAMSOSTEKDET_GETEMPEDECODE&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_jamsosteklist.aspx")
        End Try
        
        For intCnt = 0 To objEmpeADDs.Tables(0).Rows.Count - 1
            If Trim(objEmpeADDs.Tables(0).Rows(intCnt).Item("ADCode")) = Trim(pv_strEmpeDeCode) Then
                intSelectIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objEmpeADDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("_Description") = "Select Employee Deduction Code"
        objEmpeADDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpeDeCode.DataSource = objEmpeADDs.Tables(0)
        ddlEmpeDeCode.DataValueField = "ADCode"
        ddlEmpeDeCode.DataTextField = "_Description"
        ddlEmpeDeCode.DataBind()
        ddlEmpeDeCode.SelectedIndex = intSelectIndex
    End Sub


    Sub onLoad_Display()
        Dim strOpCdGet As String = "HR_CLSSETUP_JAMSOSTEK_GET"
        Dim strParam As String        
        Dim intErrNo As Integer

        strParam = "order by jam.JamCode" & "|" & "and jam.JamCode = '" & strSelJamCode & "' " 

        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCdGet, strParam, 0, objJamDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_JAMSOSTEKDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_jamsosteklist.aspx")
        End Try

        txtJamCode.Text = objJamDs.Tables(0).Rows(0).Item("JamCode")
        txtDesc.Text = objJamDs.Tables(0).Rows(0).Item("Description")
        BindADCode(objJamDs.Tables(0).Rows(0).Item("EmprDeCode"), objJamDs.Tables(0).Rows(0).Item("EmpeDeCode"))
        txtEmprRate.Text = FormatNumber(objJamDs.Tables(0).Rows(0).Item("EmprRate"), 2, True, False, False)
        txtEmpeRate.Text = FormatNumber(objJamDs.Tables(0).Rows(0).Item("EmpeRate"), 2, True, False, False)

        If objJamDs.Tables(0).Rows(0).Item("Category") = objHRSetup.EnumJamsostekCategory.JKK Then
            rbJKK.Checked = True
        ElseIf objJamDs.Tables(0).Rows(0).Item("Category") = objHRSetup.EnumJamsostekCategory.JK Then
            rbJK.Checked = True
        ElseIf objJamDs.Tables(0).Rows(0).Item("Category") = objHRSetup.EnumJamsostekCategory.JHT Then
            rbJHT.Checked = True
        ElseIf objJamDs.Tables(0).Rows(0).Item("Category") = objHRSetup.EnumJamsostekCategory.JPK Then
            rbJPK.Checked = True
        End If
        
        lblStatus.Text = objHRSetup.mtdGetJamsostekStatus(Trim(objJamDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objJamDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objJamDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objJamDs.Tables(0).Rows(0).Item("UserName"))
        intStatus = CInt(Trim(objJamDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objJamDs.Tables(0).Rows(0).Item("Status"))
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCdUpd As String = "HR_CLSSETUP_JAMSOSTEK_UPD"
        Dim strOpCdGet As String = "HR_CLSSETUP_JAMSOSTEK_GET"
        Dim strOpCdAdd As String = "HR_CLSSETUP_JAMSOSTEK_ADD"
        Dim strOpCdUpdSts As String = "HR_CLSSETUP_JAMSOSTEK_UPD_STS"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument   
        Dim blnIsDup As Boolean = False
        Dim blnIsUpdate As Boolean     
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim dblEmprRate As Double
        Dim dblEmpeRate As Double
        Dim strCategory As String

        If strCmdArgs = "Save" Then
            If ddlEmprDeCode.SelectedItem.Value = "" And ddlEmpeDeCode.SelectedItem.Value = "" Then
                lblErrSelectOne.Visible = True
                Exit Sub
            End If

            If txtEmprRate.Text = "" Then
                dblEmprRate = 0
                txtEmprRate.Text = 0
            Else
                dblEmprRate = CDbl(txtEmprRate.Text)
            End If

            If txtEmpeRate.Text = "" Then
                dblEmpeRate = 0
                txtEmpeRate.Text = 0
            Else
                dblEmpeRate = CDbl(txtEmpeRate.Text)
            End If
            
            If dblEmprRate < 0 Or dblEmprRate > 100 Then
                lblErrEmprRate.Visible = True
                Exit Sub
            End If
        
            If dblEmpeRate < 0 Or dblEmpeRate > 100 Then
                lblErrEmpeRate.Visible = True
                Exit Sub
            End If

            If rbJKK.Checked Then 
                strCategory = objHRSetup.EnumJamsostekCategory.JKK
            ElseIf rbJK.Checked Then
                strCategory = objHRSetup.EnumJamsostekCategory.JK
            ElseIf rbJHT.Checked Then
                strCategory = objHRSetup.EnumJamsostekCategory.JHT
            ElseIf rbJPK.Checked Then
                strCategory = objHRSetup.EnumJamsostekCategory.JPK
            End If

            blnIsUpdate = IIf(intStatus = 0, False, True)
            strParam = Trim(txtJamCode.Text) & chr(9) & _
                       Trim(txtDesc.Text) & chr(9) & _
                       ddlEmprDeCode.SelectedItem.Value & chr(9) & _
                       ddlEmpeDeCode.SelectedItem.Value & chr(9) & _
                       dblEmprRate & chr(9) & _
                       dblEmpeRate & chr(9) & _
                       strCategory & chr(9) & _
                       objHRSetup.EnumJamsostekStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdJamsostek(strOpCdGet, _
                                                      strOpCdAdd, _
                                                      strOpCdUpd, _
                                                      strCompany, _
                                                      strLocation, _
                                                      strUserId, _
                                                      strParam, _
                                                      blnIsUpdate, _
                                                      False, _
                                                      blnIsDup)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_JAMSOSTEKDET_UPD&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_jamsosteklist.aspx")
            End Try

            If blnIsDup = True Then
                lblErrDup.Visible = True
                Exit Sub
            Else
                strSelJamCode = Trim(txtJamCode.Text)
                tbcode.Value = strSelJamCode 
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(txtJamCode.Text) & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & objHRSetup.EnumJamsostekStatus.Deleted
            Try
                intErrNo = objHRSetup.mtdUpdJamsostek(strOpCdGet, _
                                                      strOpCdAdd, _
                                                      strOpCdUpdSts, _
                                                      strCompany, _
                                                      strLocation, _
                                                      strUserId, _
                                                      strParam, _
                                                      True, _
                                                      True, _
                                                      False)
           Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_JAMSOSTEKDET_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_jamsostekdet.aspx?tbcode=" & strSelJamCode)
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = Trim(txtJamCode.Text) & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & chr(9) & objHRSetup.EnumJamsostekStatus.Active
            Try
                intErrNo = objHRSetup.mtdUpdJamsostek(strOpCdGet, _
                                                      strOpCdAdd, _
                                                      strOpCdUpdSts, _
                                                      strCompany, _
                                                      strLocation, _
                                                      strUserId, _
                                                      strParam, _
                                                      True, _
                                                      True, _
                                                      False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_JAMSOSTEKDET_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/hr_setup_jamsostekdet.aspx?tbcode=" & strSelJamCode)
            End Try
        End If
        If strSelJamCode <> "" Then
            onLoad_Display()
            onLoad_BindButton()
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_JamsostekList.aspx")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Jamsostek))
        lblJamCode.Text = GetCaption(objLangCap.EnumLangCap.Jamsostek) & lblCode.Text
        lblDesc.Text = GetCaption(objLangCap.EnumLangCap.JamsostekDesc)
        rbJKK.Text = GetCaption(objLangCap.EnumLangCap.JKK)
        rbJK.Text = GetCaption(objLangCap.EnumLangCap.JK)
        rbJHT.Text = GetCaption(objLangCap.EnumLangCap.JHT)
        rbJPK.Text = GetCaption(objLangCap.EnumLangCap.JPK)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_JAMSOSTEKLIST_LANGCAP_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function


End Class
