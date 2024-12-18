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
Imports Microsoft.VisualBasic.DateAndTime


Public Class PR_trx_ContractCheckrollDet : Inherits Page

    Protected WithEvents ddlSuppCode As DropDownList
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrAttdDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents txtAttdDate As TextBox
    Protected WithEvents ddlAttdCode As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents lblIsUpdate As Label
    Protected WithEvents hidAttdID As HtmlInputHidden
    Protected WithEvents hidSuppCode As HtmlInputHidden
    Protected WithEvents hidAttdCode As HtmlInputHidden
    Protected WithEvents NewBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton

    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPRTrx As New agri.PR.clsTrx()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objAttdCheckDs As New DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strDateSetting As String
    Dim intPRAR As Long
    Dim intErrNo As Integer
    Dim strSelAttdID As String
    Dim intStatus As Integer
    Dim blnError As Boolean = False

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strDateSetting = Session("SS_DATEFMT")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractorCheckroll), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblDateFormat.Visible = False
            lblErrAttdDate.Visible = False

            strSelAttdID = Trim(IIf(Request.QueryString("attdid") <> "", Request.QueryString("attdid"), Request.Form("attdid")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelAttdID <> "" Then
                    lblIsUpdate.Text = True
                    onLoad_Display(strSelAttdID, False)
                Else
                    txtAttdDate.Text = objGlobal.GetShortDate(strDateSetting, Now())
                    lblIsUpdate.Text = False
                    BindSupp("")
                    BindAttdCode("")
                End If
                onLoad_BindButton()
            End If
        End If
    End Sub

    Sub onLoad_BindButton()
        Select Case Convert.ToInt16(lblHiddenSts.Text)
            Case objPRTrx.EnumContractCheckroll.Active
                ddlSuppCode.Enabled = False
                txtAttdDate.Enabled = False
                ddlAttdCode.Enabled = True
                NewBtn.Visible = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case Else
                ddlSuppCode.Enabled = True
                txtAttdDate.Enabled = True
                ddlAttdCode.Enabled = True
                NewBtn.Visible = True
                SaveBtn.Visible = True
                DelBtn.Visible = False
        End Select
    End Sub


    Sub onLoad_Display(ByVal pv_strAttId As String, ByVal pv_blnNextDate As Boolean)
        Dim strOpCd As String = "PR_CLSTRX_CONTRACTOR_CHECKROLL_GET"
        Dim strOpCd_Next As String = "PR_CLSTRX_CONTRACTOR_CHECKROLL_NEXTDATE_GET"
        Dim strParam As String
        Dim intCnt As Integer
        Dim dtCurrDate As DateTime
        Dim strCorrectDatFmt As String
        Dim strTempDate As String

        If pv_strAttId <> "" Then
            strParam = "||||||||CA.AttID||" & pv_strAttId
        Else
            If objGlobal.mtdValidInputDate(strDateSetting, txtAttdDate.Text, strCorrectDatFmt, strTempDate) = True Then
                If pv_blnNextDate = True Then
                    dtCurrDate = strTempDate
                    strTempDate = objGlobal.GetLongDate(dtCurrDate.AddDays(1))
                End If
                strParam = "|||||||" & _
                        "AND CA.SupplierCode = '" & ddlSuppCode.SelectedItem.Value & _
                        "' AND DateDiff(Day, '" & strTempDate & "', CA.AttDate) = 0|CA.AttID||"
            Else
                lblDateFormat.Text = lblErrAttdDate.Text & strCorrectDatFmt
                lblDateFormat.Visible = True
                Exit Sub
            End If
        End If

        Try
            intErrNo = objPRTrx.mtdGetContractCheckroll(strOpCd, _
                                                        strLocation, _
                                                        strParam, _
                                                        objAttdCheckDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_ATTDCHECK_GET&errmesg=" & Exp.ToString() & "&redirect=pr/trx/PR_trx_ContractCheckrollDet.aspx?attdid=" & hidAttdID.Value)
        End Try

        If objAttdCheckDs.Tables(0).Rows.Count > 0 Then
            BindSupp(objAttdCheckDs.Tables(0).Rows(0).Item("SupplierCode").Trim())
            txtAttdDate.Text = objGlobal.GetShortDate(strDateSetting, objAttdCheckDs.Tables(0).Rows(0).Item("AttDate"))
            BindAttdCode(objAttdCheckDs.Tables(0).Rows(0).Item("AttCode").Trim())
            intStatus = Convert.ToInt16(objAttdCheckDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenSts.Text = objAttdCheckDs.Tables(0).Rows(0).Item("Status").Trim()
            lblStatus.Text = objPRTrx.mtdGetContractCheckrollStatus(Convert.ToInt16(objAttdCheckDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objAttdCheckDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objAttdCheckDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objAttdCheckDs.Tables(0).Rows(0).Item("UserName")
            hidAttdID.Value = objAttdCheckDs.Tables(0).Rows(0).Item("AttID").Trim()
        Else
        

            BindSupp(hidSuppCode.Value)
            BindAttdCode(hidAttdCode.Value)
            intStatus = 0
            lblHiddenSts.Text = 0
            lblStatus.Text = ""
            lblDateCreated.Text = ""
            lblLastUpdate.Text = ""
            lblUpdatedBy.Text = ""
            lblIsUpdate.Text = False
            hidAttdID.Value = ""
        End If
    End Sub

    Sub BindSupp(ByVal pv_strSuppCode As String)
        Dim strOpCd As String = "HR_CLSSETUP_CONTRACTOR_SUPER_GET"
        Dim objSuppDs As New DataSet()
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "||" & objHRSetup.EnumContSupervision.Active & "||" & _
        strLocation & "|AND SUP.Status = '" & objPUSetup.EnumSuppStatus.Active & "'|"
        Try
            intErrNo = objHRSetup.mtdGetContractorSupervision(strOpCd, strParam, objSuppDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_ATTDCHECK_SUPP_GET&errmesg=" & Exp.ToString() & "&redirect=pr/trx/PR_trx_ContractCheckrollDet.aspx?attdid=" & hidAttdID.Value)
        End Try

        For intCnt = 0 To objSuppDs.Tables(0).Rows.Count - 1
            objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode").Trim()
            objSuppDs.Tables(0).Rows(intCnt).Item("Name") = objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") & " (" & objSuppDs.Tables(0).Rows(intCnt).Item("Name").Trim() & ")"
            If objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = pv_strSuppCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objSuppDs.Tables(0).NewRow()
        dr("SupplierCode") = ""
        dr("Name") = "Please select one Supplier"
        objSuppDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSuppCode.DataSource = objSuppDs.Tables(0)
        ddlSuppCode.DataValueField = "SupplierCode"
        ddlSuppCode.DataTextField = "Name"
        ddlSuppCode.DataBind()
        ddlSuppCode.SelectedIndex = intSelectedIndex

        objSuppDs = Nothing
    End Sub

    Sub BindAttdCode(ByVal pv_strAttdCode As String)
        Dim strOpCd As String = "PR_CLSSETUP_ATTENDANCE_LIST_GET"
        Dim objAttdCodeDs As New DataSet()
        Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim strParam As String

        strParam = "||" & objPRSetup.EnumAttStatus.Active & "||Attd.AttCode|"

        Try
            intErrNo = objPRSetup.mtdGetAttendance(strOpCd, strParam, objAttdCodeDs, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_ATTDCHECK_ATTDCODE_GET&errmesg=" & Exp.ToString() & "&redirect=pr/trx/PR_trx_ContractCheckrollDet.aspx?attdid=" & hidAttdID.Value)
        End Try

        For intCnt = 0 To objAttdCodeDs.Tables(0).Rows.Count - 1
            If objAttdCodeDs.Tables(0).Rows(intCnt).Item("AttCode") = pv_strAttdCode Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objAttdCodeDs.Tables(0).NewRow()
        dr("AttCode") = ""
        dr("_Description") = "Please select one Attendance Code"
        objAttdCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAttdCode.DataSource = objAttdCodeDs.Tables(0)
        ddlAttdCode.DataValueField = "AttCode"
        ddlAttdCode.DataTextField = "_Description"
        ddlAttdCode.DataBind()
        ddlAttdCode.SelectedIndex = intSelectedIndex

        objAttdCodeDs = Nothing
    End Sub

    Sub OnChange_Reload(ByVal Sender As Object, ByVal E As EventArgs)
        hidSuppCode.Value = ddlSuppCode.SelectedItem.Value
        hidAttdCode.Value = ddlAttdCode.SelectedItem.Value
        onLoad_Display("", False)
        onLoad_BindButton()
    End Sub

    Sub InsertContCheckroll()
        Dim strOpCd_Add As String = "PR_CLSTRX_CONTRACTOR_CHECKROLL_ADD"
        Dim strOpCd_Upd As String = "PR_CLSTRX_CONTRACTOR_CHECKROLL_UPD"
        Dim strOpCd_Get As String = "PR_CLSTRX_CONTRACTOR_CHECKROLL_GET"
        Dim strParam As String = ""
        Dim blnIsUpdate As Boolean
        Dim strDateFormat As String
        Dim strValidDate As String
        Dim strID As String

        If objGlobal.mtdValidInputDate(strDateSetting, txtAttdDate.Text, strDateFormat, strValidDate) = False Then
            lblDateFormat.Text = strDateFormat & "."
            lblDateFormat.Visible = True
            lblErrAttdDate.Visible = True
            blnError = True
            Exit Sub
        Else
            strParam = "|||||||" & _
                        "AND CA.SupplierCode = '" & ddlSuppCode.SelectedItem.Value & "' " & _
                        "AND CA.AttCode = '" & ddlAttdCode.SelectedItem.Value.Trim & "' " & _
                        "AND CA.AttDate = '" & strValidDate & "' |" & _
                        "CA.AttID||"
            Try
                intErrNo = objPRTrx.mtdGetContractCheckroll(strOpCd_Get, _
                                                            strLocation, _
                                                            strParam, _
                                                            objAttdCheckDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_CONTRACT_CHECKROLL_INSERTREC_GET&errmesg=" & Exp.ToString() & "&redirect=pr/trx/PR_trx_ContractCheckrollDet.aspx?attdid=" & hidAttdID.Value)
            End Try

            If objAttdCheckDs.Tables(0).Rows.Count > 0 And lblIsUpdate.Text = False Then
                blnError = True
                lblErrDup.Visible = True
                Exit Sub
            Else

                If lblStatus.Text = "" Then
                    blnIsUpdate = False
                    strParam = ddlSuppCode.SelectedItem.Value.Trim & "|" & strValidDate & "|" & ddlAttdCode.SelectedItem.Value.Trim
                Else
                    blnIsUpdate = True
                    strParam = hidAttdID.Value & "|" & ddlAttdCode.SelectedItem.Value.Trim
                End If

                Try
                    intErrNo = objPRTrx.mtdUpdContractCheckroll(strOpCd_Add, _
                                                                strOpCd_Upd, _
                                                                strCompany, _
                                                                strLocation, _
                                                                strUserId, _
                                                                strParam, _
                                                                strID, _
                                                                blnIsUpdate)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_CONTRACT_CHECKROLL_ADD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/PR_trx_ContractCheckrollDet.aspx?attdid=" & hidAttdID.Value)
                End Try
            End If
        End If

        If blnIsUpdate = False Then
            hidAttdID.Value = strID
        End If
    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Del As String = "PR_CLSTRX_CONTRACTOR_CHECKROLL_DEL"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim strParam As String = ""
        Dim strID As String

        If strCmdArgs = "New" Then
            BindSupp("")
            txtAttdDate.Text = ""
            BindAttdCode("")
            intStatus = 0
            lblHiddenSts.Text = 0
            lblStatus.Text = ""
            lblDateCreated.Text = ""
            lblLastUpdate.Text = ""
            lblUpdatedBy.Text = ""
            lblIsUpdate.Text = False
            hidAttdID.Value = ""
            hidSuppCode.Value = ddlSuppCode.SelectedItem.Value.Trim
            hidAttdCode.Value = ddlAttdCode.SelectedItem.Value.Trim
            onLoad_BindButton()            

        ElseIf strCmdArgs = "Save" Then
            InsertContCheckroll()

            If blnError = False Then
                If hidAttdID.Value <> "" Then
                    hidSuppCode.Value = ddlSuppCode.SelectedItem.Value.Trim
                    hidAttdCode.Value = ddlAttdCode.SelectedItem.Value.Trim

                    onLoad_Display("", True)
                End If
                onLoad_BindButton()
            End If

        ElseIf strCmdArgs = "Del" Then
            Try
                intErrNo = objPRTrx.mtdDelContractCheckroll(strOpCd_Del, _
                                                            hidAttdID.Value, _
                                                            strLocation)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_CONTRACT_CHECKROLL_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/PR_trx_ContractCheckrollDet.aspx?attdid=" & hidAttdID.Value)
            End Try

            Response.Redirect("PR_trx_ContractCheckrollList.aspx")
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_ContractCheckrollList.aspx")
    End Sub


End Class
