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


Public Class PR_setup_PPH21Det_Estate : Inherits Page

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
    Protected WithEvents ddtype As DropDownList
    Protected WithEvents txtbjabatan_psn As TextBox
    Protected WithEvents txtbjabatan_max As TextBox
    Protected WithEvents txtbthr_psn As TextBox
    Protected WithEvents txtpstart As TextBox
    Protected WithEvents txtpend As TextBox

    Protected WithEvents txt_min1 As TextBox
    Protected WithEvents txt_max1 As TextBox
    Protected WithEvents txt_tax1 As TextBox

    Protected WithEvents txt_min2 As TextBox
    Protected WithEvents txt_max2 As TextBox
    Protected WithEvents txt_tax2 As TextBox

    Protected WithEvents txt_min3 As TextBox
    Protected WithEvents txt_max3 As TextBox
    Protected WithEvents txt_tax3 As TextBox

    Protected WithEvents txt_min4 As TextBox
    Protected WithEvents txt_max4 As TextBox
    Protected WithEvents txt_tax4 As TextBox

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
            strSelectedBlockCode = Trim(IIf(Request.QueryString("idx") <> "", Request.QueryString("idx"), Request.Form("idx")))
            If Not IsPostBack Then
                If strSelectedBlockCode <> "" Then
                    isNew.Value = "False"
                    BlokCode.Value = strSelectedBlockCode
                    onLoad_Display()
                Else
                    intStatus = 0
                    isNew.Value = "True"
                    onLoad_BindType("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_PPH21_GET"
        Dim strOpCd_GetLn As String = "PR_PR_STP_PPH21LN_GET"

        Dim intErrNo As Integer
        Dim intCnt As Integer

        Dim strSearch As String
        Dim sortitem As String
        Dim objDeptDs As New Object()
        Dim objLnDs As New Object()


        strSearch = "AND idx='" & BlokCode.Value & "' "
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem & "|" & strLocation

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            isNew.Value = "False"
            txtNoBlok.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("idx"))
            txtbjabatan_psn.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Bjabatan_psn"))
            txtbjabatan_max.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("BJabatan_max"))
            txtbthr_psn.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("BTHR_psn"))
            txtpstart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeStart"))
            txtpend.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeEnd"))

            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindType(Trim(objDeptDs.Tables(0).Rows(0).Item("CodeEmpTy")))
            onLoad_BindButton()
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End If

        ParamNama = ""
        ParamValue = ""

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetLn, ParamNama, ParamValue, objLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21LN_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objLnDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objLnDs.Tables(0).Rows.Count - 1
                If objLnDs.Tables(0).Rows(intCnt).Item("id") = 1 Then
                    txt_min1.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("income_min"))
                    txt_max1.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("income_max"))
                    txt_tax1.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("tax_rate"))
                End If

                If objLnDs.Tables(0).Rows(intCnt).Item("id") = 2 Then
                    txt_min2.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("income_min"))
                    txt_max2.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("income_max"))
                    txt_tax2.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("tax_rate"))
                End If

                If objLnDs.Tables(0).Rows(intCnt).Item("id") = 3 Then
                    txt_min3.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("income_min"))
                    txt_max3.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("income_max"))
                    txt_tax3.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("tax_rate"))
                End If

                If objLnDs.Tables(0).Rows(intCnt).Item("id") = 4 Then
                    txt_min4.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("income_min"))
                    txt_max4.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("income_max"))
                    txt_tax4.Text = Trim(objLnDs.Tables(0).Rows(intCnt).Item("tax_rate"))
                End If
            Next
        End If

    End Sub

    Sub onLoad_BindButton()
        Select Case intStatus
            Case "0"
                txtNoBlok.Enabled = True
                DelBtn.Visible = False
                UnDelBtn.Visible = False
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

    Sub onLoad_BindType(ByVal pv_strYrPln As String)
        Dim strOpCd As String = "HR_HR_STP_EMPTYPE_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim sortitem As String

        strSearch = "WHERE Status='1'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objYearPlan)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_EMPTYPE_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objYearPlan.Tables(0).Rows.Count - 1
            If Trim(objYearPlan.Tables(0).Rows(intCnt).Item("EmpTyCode")) = Trim(pv_strYrPln) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objYearPlan.Tables(0).NewRow()
        dr("EmpTyCode") = ""
        dr("Description") = "Select Type"
        objYearPlan.Tables(0).Rows.InsertAt(dr, 0)

        ddtype.DataSource = objYearPlan.Tables(0)
        ddtype.DataValueField = "EmpTyCode"
        ddtype.DataTextField = "Description"
        ddtype.DataBind()
        ddtype.SelectedIndex = intSelectedIndex
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_PPH21_UPD"
        Dim strOpCdLn_Upd As String = "PR_PR_STP_PPH21LN_UPD"

        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim valty As String = ""
        Dim strstatus As String = ""
        Dim objID As New Object()

        If ddtype.SelectedItem.Value = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select type !"
            Exit Sub
        End If

        If txtbjabatan_psn.Text.Trim = "" Then
            txtbjabatan_psn.Text = "0"
        End If

        If txtbjabatan_max.Text.Trim = "" Then
            txtbjabatan_max.Text = "0"
        End If

        If txtbthr_psn.Text.Trim = "" Then
            txtbthr_psn.Text = "0"
        End If

        If txt_min1.Text.Trim = "" Then
            txt_min1.Text = "0"
        End If
        If txt_max1.Text.Trim = "" Then
            txt_max1.Text = "0"
        End If
        If txt_tax1.Text.Trim = "" Then
            txt_tax1.Text = "0"
        End If

        If txt_min2.Text.Trim = "" Then
            txt_min2.Text = "0"
        End If
        If txt_max2.Text.Trim = "" Then
            txt_max2.Text = "0"
        End If
        If txt_tax2.Text.Trim = "" Then
            txt_tax2.Text = "0"
        End If

        If txt_min3.Text.Trim = "" Then
            txt_min3.Text = "0"
        End If
        If txt_max3.Text.Trim = "" Then
            txt_max3.Text = "0"
        End If
        If txt_tax3.Text.Trim = "" Then
            txt_tax3.Text = "0"
        End If

        If txt_min4.Text.Trim = "" Then
            txt_min4.Text = "0"
        End If
        If txt_max4.Text.Trim = "" Then
            txt_max4.Text = "0"
        End If
        If txt_tax4.Text.Trim = "" Then
            txt_tax4.Text = "0"
        End If

        BlokCode.Value = Trim(txtNoBlok.Text)
        strSelectedBlockCode = Trim(BlokCode.Value)

        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If


        ParamNama = "CE|BJP|BJM|BTP|PS|PE|ST|CD|UD|UI"
        ParamValue = ddtype.SelectedItem.Value.Trim & "|" & _
                     txtbjabatan_psn.Text.Trim & "|" & _
                     txtbjabatan_max.Text.Trim & "|" & _
                     txtbthr_psn.Text.Trim & "|" & _
                     txtpstart.Text.Trim & "|" & _
                     txtpend.Text.Trim & "|" & _
                     strstatus & "|" & _
                     DateTime.Now & "|" & DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        'Level 1
        ParamNama = "ID|IMIN|IMAX|TAX"
        ParamValue = "1|" & _
                     txt_min1.Text & "|" & _
                     txt_max1.Text & "|" & _
                     txt_tax1.Text

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCdLn_Upd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21LN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        'Level 2
        ParamNama = "ID|IMIN|IMAX|TAX"
        ParamValue = "2|" & _
                     txt_min2.Text & "|" & _
                     txt_max2.Text & "|" & _
                     txt_tax2.Text

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCdLn_Upd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21LN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        'Level 3
        ParamNama = "ID|IMIN|IMAX|TAX"
        ParamValue = "3|" & _
                     txt_min3.Text & "|" & _
                     txt_max3.Text & "|" & _
                     txt_tax3.Text

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCdLn_Upd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21LN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        'Level 4
        ParamNama = "ID|IMIN|IMAX|TAX"
        ParamValue = "4|" & _
                     txt_min4.Text & "|" & _
                     txt_max4.Text & "|" & _
                     txt_tax4.Text

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCdLn_Upd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PPH21LN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        isNew.Value = "False"
        Response.Redirect("PR_setup_PPH21List_Estate.aspx")
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_PPH21List_Estate.aspx")
    End Sub


End Class
