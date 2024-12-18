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


Public Class PR_setup_YearPlanDet_Estate : Inherits Page

    Protected WithEvents ddldivisi As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents YpCode As HtmlInputHidden
    Protected WithEvents isNew As HtmlInputHidden

    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupDept As Label

    Protected WithEvents lblCode As Label
   
    Protected WithEvents txtypdeskripsi As TextBox
    Protected WithEvents txtypcode As TextBox
    Protected WithEvents rbTypeInMatureField As RadioButton
    Protected WithEvents rbTypeMatureField As RadioButton
    Protected WithEvents rbTypeNursery As RadioButton

    Protected WithEvents lbl_newyr As Label
    Protected WithEvents txt_newyr As TextBox

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim ObjOK As New agri.GL.ClsTrx

    Dim objYearPlan As New Object()
    Dim objDeptCodeDs As New Object()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long

    Dim strSelectedYpCode As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim Prefix As String

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

            strSelectedYpCode = Trim(IIf(Request.QueryString("BlkCode") <> "", Request.QueryString("BlkCode"), Request.Form("BlkCode")))
            If Not IsPostBack Then
                If strSelectedYpCode <> "" Then
                    isNew.Value = "False"
                    YpCode.Value = strSelectedYpCode
                    onLoad_Display()
                Else
                    intStatus = 0
                    isNew.Value = "True"
                    onLoad_BindDIvCode("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_YEARPLAN_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim objDeptDs As New Object()


        strSearch = "AND BlkCode like '" & YpCode.Value & "%' AND A.LocCode like '" & strLocation & "%'"
        sortitem = ""
        ParamNama = "STRSEARCH|SORTEXP"
        ParamValue = strSearch & "|"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_YEARPLAN_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            txtypcode.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("BlkCode"))
            txtypdeskripsi.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Description"))
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            isNew.Value = "False"
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            Select Case Trim(objDeptDs.Tables(0).Rows(0).Item("BlkType"))
                Case "1"
                    rbTypeInMatureField.Checked = True
                Case "2"
                    rbTypeMatureField.Checked = True
                Case "4"
                    rbTypeNursery.Checked = True
            End Select
            onLoad_BindDIvCode(Trim(objDeptDs.Tables(0).Rows(0).Item("BlkGrpCode")))
            onLoad_BindButton()
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_BLOK_GET&errmesg=" & lblNoRecord.Text & "&redirect=hr/setup/HR_setup_Deptlist.aspx")
        End If
    End Sub

    Sub onLoad_BindButton()
        Select Case intStatus
            Case "0"
                lbl_newyr.Visible = True
                txt_newyr.Visible = True
                txt_newyr.Text = Format(DateTime.Now, "yyyy")
                DelBtn.Visible = False
                UnDelBtn.Visible = False

            Case "1"
                txtypcode.Enabled = False
                DelBtn.Visible = True
                UnDelBtn.Visible = False
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                lbl_newyr.Visible = False
                txt_newyr.Visible = False

            Case "2"
                UnDelBtn.Attributes("onclick") = "javascript:return ConfirmAction('Undelete');"
                txtypcode.Enabled = False
                DelBtn.Visible = False
                UnDelBtn.Visible = True
                lbl_newyr.Visible = False
                txt_newyr.Visible = False

        End Select

    End Sub

    Sub onLoad_BindDIvCode(ByVal pv_strDivCode As String)
        Dim strOpCd_DivId As String = "PR_PR_STP_DIVISICODE_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim strSearch_all As String = ""
        Dim sortitem As String

        strSearch = "And A.LocCode='" & strLocation & "' AND A.Status='1'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objDeptCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objDeptCodeDs.Tables(0).Rows.Count - 1
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDeptCodeDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = pv_strDivCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDeptCodeDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "Pilih Divisi"
        objDeptCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddldivisi.DataSource = objDeptCodeDs.Tables(0)
        ddldivisi.DataValueField = "BlkGrpCode"
        ddldivisi.DataTextField = "Description"
        ddldivisi.DataBind()
        ddldivisi.SelectedIndex = intSelectedIndex
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_YEARPLAN_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strstatus As String = ""
        Dim valty As String = ""
        Dim objID As New Object()

        If ddldivisi.SelectedItem.Value = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan Pilih Divisi !"
            Exit Sub
        End If

        'If isNew.Value = "True" Then
        '    If txt_newyr.Text <> "" Then
        '        txtypcode.Text = Right(ddldivisi.SelectedItem.Value.Trim(), 2) & txt_newyr.Text.Trim
        '    End If
        'End If

        If txtypcode.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan input tahun tanam code !"
            Exit Sub
        End If

        If txtypdeskripsi.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan input deskripsi !"
            Exit Sub
        End If

        If (txt_newyr.Text <> "") And (rbTypeInMatureField.Checked = False) And (rbTypeMatureField.Checked = False) And (rbTypeNursery.Checked = False) Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan pilih type tahun tanam  !"
            Exit Sub
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



        YpCode.Value = Trim(txtypcode.Text)
        strSelectedYpCode = YpCode.Value

        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If

        ParamNama = "BC|Loc|Desc|DC|TY|ST|CD|UD|UI"
        ParamValue = strSelectedYpCode & "|" & strLocation & "|" & _
                     txtypdeskripsi.Text.Trim.ToUpper & "|" & _
                     ddldivisi.SelectedItem.Value.Trim & "|" & _
                     valty & "|" & _
                     strstatus & "|" & _
                     DateTime.Now & "|" & DateTime.Now & "|" & strUserId


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_YEARPLAN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        isNew.Value = "False"
        onLoad_Display()

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_YearPlan_Estate.aspx")
    End Sub


End Class
