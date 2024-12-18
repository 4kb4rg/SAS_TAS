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
Imports agri.GL


Public Class PR_setup_PrmBrsDet_Estate : Inherits Page

    Protected WithEvents ddlTunjangan As DropDownList
    Protected WithEvents ddlBerasRt As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents PMBCd As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupDept As Label
    Protected WithEvents lblNoBrsRt As Label
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
    Protected WithEvents lblCodeDiv As Label
    Protected WithEvents lblDivHead As Label
    Protected WithEvents lblTunj As Label
    Protected WithEvents txtpstart As TextBox
    Protected WithEvents txtpend As TextBox
    Protected WithEvents txtKry As TextBox
    Protected WithEvents txtIstri As TextBox
    Protected WithEvents txtAnk As TextBox
    Protected WithEvents txtptkp_pstart As TextBox
    Protected WithEvents txtptkp_pend As TextBox
    Protected WithEvents txtJmlAnk As TextBox
    Protected WithEvents txtPTKPRt As TextBox
    Protected WithEvents validateNoBlok As RequiredFieldValidator
    Protected WithEvents revCode As RegularExpressionValidator
	Protected WithEvents ddlfisik As DropDownList


    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim ObjOK As New agri.GL.ClsTrx

    Dim objJobCode As New Object()
    Dim objDeptDs As New Object()
    Dim objDeptCodeDs As New Object()
    Dim objCompDs As New Object()
    Dim objDivHeadDs As New Object()
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

    Dim strSelectedPMBCd As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblNoBrsRt.Visible = False
            lblTunj.Visible = False
            strSelectedPMBCd = Trim(IIf(Request.QueryString("PMBerasCode") <> "", Request.QueryString("PMBerasCode"), Request.Form("PMBerasCode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedPMBCd <> "" Then
                    PMBCd.Value = strSelectedPMBCd
                    onLoad_Display()
                Else
                    onLoad_BindTang("")
                    onLoad_BindBrsRt("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcceptFormat As String

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

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


    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_PREMIBERAS_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String


        strSearch = "AND A.PMBerasCode like '" & PMBCd.Value & "%' AND A.LocCode like '" & strLocation & "%'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblHiddenSts.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Status"))
            txtpstart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeStart"))
            txtpend.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeEnd"))
            txtJmlAnk.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("JmlAnk"))
            txtKry.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("KryKg"))
            txtIstri.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Istrikg"))
            txtAnk.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("AnkKg"))
            txtptkp_pstart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PTKP_PeriodeStart"))
            txtptkp_pend.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PTKP_PeriodeEnd"))
            txtPTKPRt.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PTKPRate"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
			ddlfisik.SelectedValue = Trim(objDeptDs.Tables(0).Rows(0).Item("isFisik"))
            onLoad_BindTang(Trim(objDeptDs.Tables(0).Rows(0).Item("CodeTG")))
            onLoad_BindBrsRt(Trim(objDeptDs.Tables(0).Rows(0).Item("BerasCode")))
            onLoad_BindButton()
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_GET&errmesg=" & lblNoRecord.Text & "&redirect=pr/setup/PR_setup_PrmBrsDet_Estate.aspx")
        End If
    End Sub

    Sub onLoad_BindButton()

        txtpstart.Enabled = False
        txtpend.Enabled = False
        txtKry.Enabled = False
        txtIstri.Enabled = False
        txtAnk.Enabled = False
        txtptkp_pstart.Enabled = False
        txtptkp_pend.Enabled = False
        ddlBerasRt.Enabled = False
        ddlTunjangan.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        txtJmlAnk.Enabled = False
        txtPTKPRt.Enabled = False

        Select Case intStatus
            Case "1"
                txtpstart.Enabled = True
                txtpend.Enabled = True
                txtKry.Enabled = True
                txtIstri.Enabled = True
                txtAnk.Enabled = True
                txtptkp_pstart.Enabled = True
                txtptkp_pend.Enabled = True
                txtJmlAnk.Enabled = True
                txtPTKPRt.Enabled = True
                ddlBerasRt.Enabled = True
                ddlTunjangan.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                UnDelBtn.Visible = True
            Case Else
                txtpstart.Enabled = True
                txtpend.Enabled = True
                txtKry.Enabled = True
                txtIstri.Enabled = True
                txtAnk.Enabled = True
                txtptkp_pstart.Enabled = True
                txtptkp_pend.Enabled = True
                txtJmlAnk.Enabled = True
                txtPTKPRt.Enabled = True
                ddlBerasRt.Enabled = True
                ddlTunjangan.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub

    Sub onLoad_BindTang(ByVal pv_strTang As String)
        Dim strOpCd_Tang As String = "PR_PR_STP_TANGGUNGAN_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim strSearch_all As String = ""
        Dim sortitem As String

        strSearch = "WHERE Status='1'"
        sortitem = "order by TGCode"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Tang, ParamNama, ParamValue, objDeptCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_TANGGUNGAN_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objDeptCodeDs.Tables(0).Rows.Count - 1
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("TGCode") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("TGCode"))
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("TGCode")) & " (" & Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDeptCodeDs.Tables(0).Rows(intCnt).Item("TGCode") = pv_strTang Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDeptCodeDs.Tables(0).NewRow()
        dr("TGCode") = ""
        dr("Description") = "Select Tanggungan"
        objDeptCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTunjangan.DataSource = objDeptCodeDs.Tables(0)
        ddlTunjangan.DataValueField = "TGCode"
        ddlTunjangan.DataTextField = "Description"
        ddlTunjangan.DataBind()
        ddlTunjangan.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindBrsRt(ByVal pv_strBrsRt As String)
        Dim strOpCd_JobCode As String = "PR_PR_STP_BERASRATE_GET_LIST"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim strSearch_all As String = ""
        Dim sortitem As String

        strSearch = "Status='1' AND LocCode='" & strLocation & "' "
        sortitem = "order by BerasCode"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_JobCode, ParamNama, ParamValue, objJobCode)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BERASRATE_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objJobCode.Tables(0).Rows.Count - 1
            objJobCode.Tables(0).Rows(intCnt).Item("BerasCode") = Trim(objJobCode.Tables(0).Rows(intCnt).Item("BerasCode"))
            objJobCode.Tables(0).Rows(intCnt).Item("BerasRate") = Trim(objJobCode.Tables(0).Rows(intCnt).Item("BerasCode")) & " (" & Trim(objJobCode.Tables(0).Rows(intCnt).Item("BerasRate")) & ")"
            If objJobCode.Tables(0).Rows(intCnt).Item("BerasCode") = pv_strBrsRt Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objJobCode.Tables(0).NewRow()
        dr("BerasCode") = ""
        dr("BerasRate") = "Select Beras Rate"
        objJobCode.Tables(0).Rows.InsertAt(dr, 0)

        ddlBerasRt.DataSource = objJobCode.Tables(0)
        ddlBerasRt.DataValueField = "BerasCode"
        ddlBerasRt.DataTextField = "BerasRate"
        ddlBerasRt.DataBind()
        ddlBerasRt.SelectedIndex = intSelectedIndex
    End Sub

    Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrno As Integer
        Dim Prefix = "PMC"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_PREMIBERAS_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim valPMCd As String
        Dim Pstart As String = txtpstart.Text.Trim
        Dim Pend As String = txtpend.Text.Trim
        Dim Ptkpstart As String = txtptkp_pstart.Text.Trim
        Dim Ptkpend As String = txtptkp_pend.Text.Trim
        Dim strstatus As String = ""
		Dim sfisik As String = ddlfisik.SelectedItem.Value.Trim
        Dim objID As New Object()




        If ddlTunjangan.SelectedItem.Value = "" Then
            lblTunj.Visible = True
            lblTunj.Text = "Please Select Tanggungan"
            Exit Sub
        ElseIf ddlBerasRt.SelectedItem.Value = "" Then
            lblNoBrsRt.Visible = True
            lblNoBrsRt.Text = "Please Select Beras Rate"
            Exit Sub
        End If

        If intStatus = 0 Then
            valPMCd = getCode()
        Else
            valPMCd = strSelectedPMBCd
        End If

        PMBCd.Value = valPMCd
        strSelectedPMBCd = PMBCd.Value



        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then

            strstatus = "1"

        ElseIf strCmdArgs = "Del" Then

            strstatus = "2"

        End If

        If Trim(txtKry.Text) = "" Then
            txtKry.Text = "0"
        End If

        If Trim(txtIstri.Text) = "" Then
            txtIstri.Text = "0"
        End If

        If Trim(txtAnk.Text) = "" Then
            txtAnk.Text = "0"
        End If

        If Trim(txtJmlAnk.Text) = "" Then
            txtJmlAnk.Text = "0"
        End If



        ParamNama = "PMBC|Loc|CTG|KKg|IKg|AKg|JA|PS|PE|BC|PTKPRt|PTKP_PS|PTKP_PE|ST|CD|UD|UI|FS"
        ParamValue = valPMCd & "|" & strLocation & "|" & ddlTunjangan.SelectedValue & "|" & txtKry.Text & "|" & _
                     txtIstri.Text & "|" & txtAnk.Text & "|" & txtJmlAnk.Text & "|" & Pstart & "|" & Pend & "|" & _
                     ddlBerasRt.SelectedValue & "|" & txtPTKPRt.Text & "|" & Ptkpstart & "|" & Ptkpend & "|" & strstatus & "|" & _
                     DateTime.Now & "|" & DateTime.Now & "|" & strUserId & "|" & sfisik



        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If strSelectedPMBCd <> "" Then
            onLoad_Display()
        End If

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_PrmBrsList_Estate.aspx")
    End Sub


End Class
