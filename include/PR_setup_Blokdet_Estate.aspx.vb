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


Public Class PR_setup_Blokdet_Estate : Inherits Page

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents BlokCode As HtmlInputHidden
    Protected WithEvents isNew As HtmlInputHidden

    Protected WithEvents txtNoBlok As TextBox
    Protected WithEvents txtdeskripsi As TextBox
    Protected WithEvents txtpend As TextBox
    Protected WithEvents txtdtplant As TextBox
    Protected WithEvents ddlypcode As DropDownList
    Protected WithEvents rbTypeInMatureField As RadioButton
    Protected WithEvents rbTypeMatureField As RadioButton
    Protected WithEvents rbTypeNursery As RadioButton
    Protected WithEvents txttarea As TextBox
    Protected WithEvents txttpokok As TextBox
    Protected WithEvents txtBJR As TextBox

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim objYearPlan As New Object()
    Dim objDeptCodeDs As New Object()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long

    Dim strSelectedBlockCode As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim Prefix As String
    Dim strAcceptFormat As String

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
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        Dim Prefix As String = "BK" & strLocation

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedBlockCode = Trim(IIf(Request.QueryString("SubBlkCode") <> "", Request.QueryString("SubBlkCode"), Request.Form("SubBlkCode")))
            If Not IsPostBack Then
                If strSelectedBlockCode <> "" Then
                    isNew.Value = "False"
                    BlokCode.Value = strSelectedBlockCode
                    onLoad_Display()
                Else
                    intStatus = 0
                    isNew.Value = "True"
                    onLoad_BindYearPlan("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_BLOK_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim objDeptDs As New Object()


        strSearch = "AND sub.SubBlkCode='" & BlokCode.Value & "' AND Sub.LocCode like '" & strLocation & "%'"
        sortitem = ""
        ParamNama = "SEARCH|SORT|Loc"
        ParamValue = strSearch & "|" & sortitem & "|" & strLocation

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            isNew.Value = "False"
            txtNoBlok.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("SubBlkCode"))
            txtdeskripsi.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Description"))
            txtdtplant.Text = Date_Validation(objDeptDs.Tables(0).Rows(0).Item("PlantingDate"), True)
            txttarea.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("TotalArea"))
            txttpokok.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("TotalStand"))
            txtBJR.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("EstBJR"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindYearPlan(Trim(objDeptDs.Tables(0).Rows(0).Item("ThnCode")))
            onLoad_BindButton()
            Select Case Trim(objDeptDs.Tables(0).Rows(0).Item("SubBlkType"))
                Case "1"
                    rbTypeInMatureField.Checked = True
                Case "2"
                    rbTypeMatureField.Checked = True
                Case "4"
                    rbTypeNursery.Checked = True
            End Select
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End If
    End Sub

    Sub onLoad_BindButton()
        Select Case intStatus
            Case "0"
                txtNoBlok.Enabled = True
            Case "1"
                txtNoBlok.Enabled = False
                DelBtn.Visible = True
                UnDelBtn.Visible = False
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                txtNoBlok.Enabled = False
                UnDelBtn.Attributes("onclick") = "javascript:return ConfirmAction('Undelete');"
                DelBtn.Visible = False
                UnDelBtn.Visible = True
        End Select


    End Sub

    Sub onLoad_BindYearPlan(ByVal pv_strYrPln As String)
        Dim strOpCd_yearPlan As String = "PR_PR_STP_YEARPLAN_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim strSearch_all As String = ""
        Dim sortitem As String

        strSearch = "AND A.LocCode='" & strLocation & "' And A.Status='1'"
        sortitem = ""
        ParamNama = "STRSEARCH|SORTEXP"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_yearPlan, ParamNama, ParamValue, objYearPlan)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEPTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objYearPlan.Tables(0).Rows.Count - 1
            objYearPlan.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objYearPlan.Tables(0).Rows(intCnt).Item("BlkCode"))
            objYearPlan.Tables(0).Rows(intCnt).Item("Description") = Trim(objYearPlan.Tables(0).Rows(intCnt).Item("Description"))

            If Trim(objYearPlan.Tables(0).Rows(intCnt).Item("BlkCode")) = Trim(pv_strYrPln) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objYearPlan.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Pilih Tahun Tanam"
        objYearPlan.Tables(0).Rows.InsertAt(dr, 0)

        ddlypcode.DataSource = objYearPlan.Tables(0)
        ddlypcode.DataValueField = "BlkCode"
        ddlypcode.DataTextField = "Description"
        ddlypcode.DataBind()
        ddlypcode.SelectedIndex = intSelectedIndex
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_BLOK_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim valty As String = ""
        Dim valdt As String = txtdtplant.Text
        Dim strstatus As String = ""
        Dim objID As New Object()

        If txtNoBlok.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "silakan input kode blok !"
            Exit Sub
        End If

        If txtdeskripsi.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "silakan input deskripsi !"
            Exit Sub
        End If

        If ddlypcode.SelectedItem.Value = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "silakan pilih tahun tanam !"
            Exit Sub
        End If

        If valdt <> "" Then
            valdt = Date_Validation(valdt, False)
        Else
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan input tanggal tanam !"
            Exit Sub
        End If

        If txtBJR.Text = "" Then
            txtBJR.Text = "0"
        End If

        If txttarea.Text = "" Then
            txttarea.Text = "0"
        End If

        If txttpokok.Text = "" Then
            txttpokok.Text = "0"
        End If

        If rbTypeInMatureField.Checked Then
            valty = "1"
        End If

        If rbTypeMatureField.Checked Then
            valty = "2"
        End If

        If rbTypeNursery.Checked Then
            valty = "4"
        End If

        BlokCode.Value = Trim(txtNoBlok.Text)
        strSelectedBlockCode = Trim(BlokCode.Value)

        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If


        ParamNama = "SBC|BC|LOC|Desc|PD|TY|TA|TP|BJR|ST|CD|UD|UI"
        ParamValue = strSelectedBlockCode & "|" & _
                     ddlypcode.SelectedItem.Value & "|" & _
                     strLocation & "|" & _
                     txtdeskripsi.Text.ToUpper.Trim & "|" & _
                     valdt & "|" & _
                     valty & "|" & _
                     txttarea.Text.Trim & "|" & _
                     txttpokok.Text.Trim & "|" & _
                     txtBJR.Text & "|" & strstatus & "|" & _
                     DateTime.Now & "|" & DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        isNew.Value = "False"
        onLoad_Display()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_Bloklist_Estate.aspx")
    End Sub


End Class
