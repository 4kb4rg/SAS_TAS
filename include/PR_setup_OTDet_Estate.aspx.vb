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


Public Class PR_setup_OTDet_Estate : Inherits Page


    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
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
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblList As Label
    Protected WithEvents lblOvertime As Label
    Protected WithEvents lblBrate As Label
    Protected WithEvents ddlBrate As DropDownList
    Protected WithEvents ddliswkd As DropDownList

    Protected WithEvents txtOtCode As TextBox
    Protected WithEvents txtmin1 As TextBox
    Protected WithEvents txtmax1 As TextBox
    Protected WithEvents txtPsn1 As TextBox

    Protected WithEvents txtmin2 As TextBox
    Protected WithEvents txtmax2 As TextBox
    Protected WithEvents txtPsn2 As TextBox

    Protected WithEvents txtmin3 As TextBox
    Protected WithEvents txtmax3 As TextBox
    Protected WithEvents txtPsn3 As TextBox

    Protected WithEvents txtmin4 As TextBox
    Protected WithEvents txtmax4 As TextBox
    Protected WithEvents txtPsn4 As TextBox
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents revCode As RegularExpressionValidator


    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
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

    Dim strSelectedOtCode As String = ""
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedOtCode = Trim(IIf(Request.QueryString("OTCode") <> "", Request.QueryString("OTCode"), Request.Form("OTCode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedOtCode <> "" Then
                    onLoad_Display()
                Else
                    onLoad_BindBrsRt("")
                    onLoad_BindButton()
                End If
            End If
        End If

    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_OVERTIME_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String



        strSearch = "AND A.OTCode like '" & Trim(strSelectedOtCode) & "%' And A.LocCode like '" & strLocation & "%'"
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
            txtOtCode.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("OTCode"))
            ddliswkd.SelectedValue = Trim(objDeptDs.Tables(0).Rows(0).Item("TyHari"))

            onLoad_BindBrsRt(Trim(objDeptDs.Tables(0).Rows(0).Item("BerasCode")))

            txtmin1.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("min_1"))
            txtmax1.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("max_1"))
            txtPsn1.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("psn_1"))

            txtmin2.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("min_2"))
            txtmax2.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("max_2"))
            txtPsn2.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("psn_2"))

            txtmin3.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("min_3"))
            txtmax3.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("max_3"))
            txtPsn3.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("psn_3"))

            txtmin4.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("min_4"))
            txtmax4.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("max_4"))
            txtPsn4.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("psn_4"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindButton()
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_GET&errmesg=" & lblNoRecord.Text & "&redirect=pr/setup/PR_setup_PrmBrsDet_Estate.aspx")
        End If

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

        strSearch = "Status='1' AND LocCode='" & strlocation & "' "
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

        ddlBrate.DataSource = objJobCode.Tables(0)
        ddlBrate.DataValueField = "BerasCode"
        ddlBrate.DataTextField = "BerasRate"
        ddlBrate.DataBind()
        ddlBrate.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindButton()

        DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
    End Sub


    Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrno As Integer
        Dim Prefix = "OT" & strLocation

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_OVERTIME_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strstatus As String = ""
        Dim strId As String
        Dim objID As New Object()


        If strSelectedOtCode = "" Then
            txtOtCode.Text = getCode()
        End If


        If ddlBrate.Text = "Select Beras Rate" Then
            lblBrate.Visible = True
            lblBrate.Text = "Please Select Beras Rate"
        End If


        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then

            strstatus = "1"

        ElseIf strCmdArgs = "Del" Then

            strstatus = "2"

        End If


        ParamNama = "OTC|Loc|IW|BC|I1|X1|Psn1|I2|X2|Psn2|I3|X3|Psn3|I4|X4|Psn4|ST|CD|UD|UI"
        ParamValue = txtOtCode.Text.Trim & "|" & strLocation & "|" & ddliswkd.SelectedItem.Value.Trim() & "|" & ddlBrate.SelectedValue.Trim() & "|" & _
                     txtmin1.Text.Trim & "|" & txtmax1.Text.Trim & "|" & txtPsn1.Text & "|" & _
                     txtmin2.Text.Trim & "|" & txtmax2.Text.Trim & "|" & txtPsn2.Text & "|" & _
                     txtmin3.Text.Trim & "|" & txtmax3.Text.Trim & "|" & txtPsn3.Text & "|" & _
                     txtmin4.Text.Trim & "|" & txtmax4.Text.Trim & "|" & txtPsn4.Text & "|" & _
                     strstatus & "|" & DateTime.Now & "|" & DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strSelectedOtCode = Trim(txtOtCode.Text)

        If strSelectedOtCode <> "" Then
            onLoad_Display()
        End If

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_OTList_Estate.aspx")
    End Sub


End Class
