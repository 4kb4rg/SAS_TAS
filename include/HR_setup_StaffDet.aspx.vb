
Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class HR_setup_StaffDet : Inherits Page

    Dim objWM As New agri.WM.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLTrx As New agri.GL.ClsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()

    Protected WithEvents txtStaffID As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents txtNIK As TextBox
    Protected WithEvents txtAddress As HtmlTextArea
    Protected WithEvents txtTelNo As TextBox
    Protected WithEvents ddlBankCode As DropDownList
    Protected WithEvents txtBankAccNo As TextBox

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrAddress As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelectList As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents rbExt As RadioButton
    Protected WithEvents rbInt As RadioButton

    Protected WithEvents hidStaffID As HtmlInputHidden
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWMAR As Integer

    Dim objData As New DataSet()

    Dim strSelectedStaffID As String = ""
    Dim strParamName As String
    Dim strParamValue As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        intWMAR = Session("SS_WMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
            'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMTransporterSetup), intWMAR) = False Then
            '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedStaffID = Request.QueryString("staffid")

            If Not IsPostBack Then
                BindBankCode("")
                If strSelectedStaffID <> "" Then
                    onLoad_Display()
                Else
                    EnableControl()
                End If
            End If
        End If
    End Sub

    Sub BindBankCode(ByVal pv_BankCode As String)
        Dim strOpCd_GET As String = "HR_CLSSETUP_BANK_SEARCH"
        Dim strSrchBankCode As String
        Dim strSrchDesc As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow
        Dim dsBank As DataSet
        Dim objBankDs As Object

        intSelectedIndex = 0

        strParam = "||||B.BankCode|"

        Try
            intErrNo = objHRSetup.mtdGetBank(strOpCd_GET, strParam, objBankDs, False)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_BANK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objBankDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
                objBankDs.Tables(0).Rows(intCnt).Item("BankCode") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode"))
                objBankDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBankDs.Tables(0).Rows(intCnt).Item("Description"))
                If objBankDs.Tables(0).Rows(intCnt).Item("BankCode") = Trim(pv_BankCode) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objBankDs.Tables(0).NewRow()
        dr("BankCode") = ""
        dr("Description") = "Please Select Bank Code"
        objBankDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBankCode.DataSource = objBankDs.Tables(0)
        ddlBankCode.DataValueField = "BankCode"
        ddlBankCode.DataTextField = "Description"
        ddlBankCode.DataBind()
        ddlBankCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_Display()
        Dim strOpCode As String = "HR_CLSSETUP_STAFF_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim objData As Object

        hidStaffID.Value = strSelectedStaffID
        strParamName = "STRSEARCH"
        strParamValue = " AND StaffID='" & Trim(hidStaffID.Value) & "' "

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objData)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_STAFF&errmesg=" & lblErrMessage.Text & "&redirect=WM/setup/WM_setup_TransporterList.aspx")
        End Try

        txtStaffID.Text = objData.Tables(0).Rows(0).Item("StaffID")
        txtName.Text = objData.Tables(0).Rows(0).Item("Name")
        txtNIK.Text = objData.Tables(0).Rows(0).Item("NIK")
        txtAddress.Value = objData.Tables(0).Rows(0).Item("Address")
        txtTelNo.Text = objData.Tables(0).Rows(0).Item("TelNo")
        BindBankCode(objData.Tables(0).Rows(0).Item("BankCode"))
        txtBankAccNo.Text = objData.Tables(0).Rows(0).Item("BankAccNo")
        lblStatus.Text = objWM.mtdGetTransporterStatus(Trim(objData.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objData.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objData.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdateBy.Text = objData.Tables(0).Rows(0).Item("UserName")

        Select Case objData.Tables(0).Rows(0).Item("StaffType")
            Case objWM.EnumTransporterType.External
                rbExt.Checked = True
            Case Else
                rbInt.Checked = True
        End Select

        Select Case Trim(lblStatus.Text)
            Case objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Active)
                EnableControl()
            Case objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Deleted)
                DisableControl()
        End Select

    End Sub

    Sub DisableControl()
        txtStaffID.Enabled = False
        txtName.Enabled = False
        txtAddress.Disabled = True
        txtTelNo.Enabled = False
        btnSave.Visible = False
        btnDelete.Visible = False
        btnUnDelete.Visible = True
        rbExt.Enabled = False
        rbInt.Enabled = False
    End Sub

    Sub EnableControl()
        If txtStaffID.Text = "" Then
            txtStaffID.ReadOnly = False
            btnDelete.Visible = False
        Else
            txtStaffID.Enabled = True
            txtStaffID.ReadOnly = True
            btnDelete.Visible = True
            btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        End If

        txtName.Enabled = True
        txtAddress.Disabled = False
        txtTelNo.Enabled = True
        btnSave.Visible = True
        btnUnDelete.Visible = False
        rbExt.Enabled = True
        rbInt.Enabled = True

    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strSrchTrans As String
        Dim strName As String = txtName.Text.Trim
        Dim strAddress As String = IIf(txtAddress.Value = "", "", txtAddress.Value.Trim)
        Dim strTelNo As String = IIf(txtTelNo.Text = "", "", txtTelNo.Text.Trim)
        Dim strSType As String

        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object

        If txtName.Text = "" Then
            lblErrMessage.Text = "Name cannot be empty"
            lblErrMessage.Visible = True
            Exit Sub
        End If
        If rbExt.Checked = True Then
            strSType = objWM.EnumTransporterType.External
        Else
            strSType = objWM.EnumTransporterType.Internal
        End If

        If Len(strAddress) > 512 Then
            lblErrAddress.Visible = True
            Exit Sub
        End If

        If txtStaffID.Text = "" Then
            strOpCode = "HR_CLSSETUP_STAFF_ADD_AUTO"
        Else
            strOpCode = "HR_CLSSETUP_STAFF_UPDATE"
        End If

        strParamName = "STAFFID|STAFFTYPE|NAME|NIK|ADDRESS|TELNO|BANKCODE|BANKACCNO|STATUS|CREATEDATE|UPDATEDATE|UPDATEID"
        strParamValue = txtStaffID.Text & "|" & strSType & "|" & Trim(strName) & "|" & Trim(txtNIK.Text) & "|" & Trim(strAddress) & "|" & Trim(strTelNo) & "|" & _
                        ddlBankCode.SelectedItem.Value & "|" & Trim(txtBankAccNo.Text) & "|" & _
                        objWM.EnumTransporterStatus.Active & "|" & _
                        Now() & "|" & Now() & "|" & Trim(strUserId)

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objData)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_STAFF&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objData.Tables(0).Rows.Count > 0 Then
            txtStaffID.Text = Trim(objData.Tables(0).Rows(0).Item("StaffID"))
        End If

        hidStaffID.Value = txtStaffID.Text.Trim
        strSelectedStaffID = txtStaffID.Text.Trim

        onLoad_Display()
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode As String = "HR_CLSSETUP_STAFF_UPDATE"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSType As String
        Dim strName As String = txtName.Text.Trim
        Dim strAddress As String = IIf(txtAddress.Value = "", "", txtAddress.Value.Trim)
        Dim strTelNo As String = IIf(txtTelNo.Text = "", "", txtTelNo.Text.Trim)
        Dim objData As New Object

        If txtName.Text = "" Then
            lblErrMessage.Text = "Name cannot be empty"
            lblErrMessage.Visible = True
            Exit Sub
        End If
        If rbExt.Checked = True Then
            strSType = objWM.EnumTransporterType.External
        Else
            strSType = objWM.EnumTransporterType.Internal
        End If

        strParamName = "STAFFID|STAFFTYPE|NAME|NIK|ADDRESS|TELNO|BANKCODE|BANKACCNO|STATUS|CREATEDATE|UPDATEDATE|UPDATEID"
        strParamValue = strParamValue = txtStaffID.Text & "|" & strSType & "|" & Trim(strName) & "|" & Trim(txtNIK.Text) & "|" & Trim(strAddress) & "|" & Trim(strTelNo) & "|" & _
                        ddlBankCode.SelectedItem.Value & "|" & Trim(txtBankAccNo.Text) & "|" & _
                        objWM.EnumTransporterStatus.Deleted & "|" & _
                        Now() & "|" & Now() & "|" & Trim(strUserId)

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objData)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_STAFF&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        onLoad_Display()
    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode As String = "HR_CLSSETUP_STAFF_UPDATE"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSType As String
        Dim strName As String = txtName.Text.Trim
        Dim strAddress As String = IIf(txtAddress.Value = "", "", txtAddress.Value.Trim)
        Dim strTelNo As String = IIf(txtTelNo.Text = "", "", txtTelNo.Text.Trim)
        Dim objData As New Object

        If txtName.Text = "" Then
            lblErrMessage.Text = "Name cannot be empty"
            lblErrMessage.Visible = True
            Exit Sub
        End If
        If rbExt.Checked = True Then
            strSType = objWM.EnumTransporterType.External
        Else
            strSType = objWM.EnumTransporterType.Internal
        End If

        strParamName = "STAFFID|STAFFTYPE|NAME|NIK|ADDRESS|TELNO|BANKCODE|BANKACCNO|STATUS|CREATEDATE|UPDATEDATE|UPDATEID"
        strParamValue = strParamValue = txtStaffID.Text & "|" & strSType & "|" & Trim(strName) & "|" & Trim(txtNIK.Text) & "|" & Trim(strAddress) & "|" & Trim(strTelNo) & "|" & _
                        ddlBankCode.SelectedItem.Value & "|" & Trim(txtBankAccNo.Text) & "|" & _
                        objWM.EnumTransporterStatus.Active & "|" & _
                        Now() & "|" & Now() & "|" & Trim(strUserId)

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objData)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_STAFF&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        onLoad_Display()
    End Sub


    Sub btnNew_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_setup_StaffDet.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_setup_StaffList.aspx")
    End Sub


End Class
