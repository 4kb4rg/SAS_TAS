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


Public Class PR_setup_PrmKaretDet_Estate : Inherits Page

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents BlokCode As HtmlInputHidden

    Protected WithEvents txtid As TextBox
    Protected WithEvents txtpstart As TextBox
    Protected WithEvents txtpend As TextBox
    Protected WithEvents txt_kkk_psn As TextBox
    Protected WithEvents txt_lm_psn As TextBox
    Protected WithEvents txt_kkk_bss As TextBox
    Protected WithEvents txt_lm_bss As TextBox
    Protected WithEvents txt_kkk_ob As TextBox
    Protected WithEvents txt_lm_ob As TextBox
    Protected WithEvents txt_gt_ob As TextBox

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

    Dim PStart As String = ""
    Dim PEnd As String = ""
    Dim BCode As String = ""

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
            lblErrMessage.Visible = False
            PStart = Trim(IIf(Request.QueryString("periodestart") <> "", Request.QueryString("periodestart"), Request.Form("periodestart")))
            PEnd = Trim(IIf(Request.QueryString("periodeend") <> "", Request.QueryString("periodeend"), Request.Form("periodeend")))
            BCode = Trim(IIf(Request.QueryString("loccode") <> "", Request.QueryString("loccode"), strLocation))

            If Not IsPostBack Then
                If PStart <> "" And PEnd <> "" And BCode <> "" Then
                    onLoad_Display(PStart, PEnd, BCode)
                Else
                    intStatus = 0
                    txtpstart.Text = Format(DateTime.Now, "MMyyyy")
                    txtpend.Text = Format(DateTime.Now, "MMyyyy")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display(ByVal ps As String, ByVal pe As String, ByVal bc As String)
        Dim strOpCd_Get As String = "PR_PR_STP_PREMIDERES_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim objDeptDs As New Object()


        strSearch = "AND periodestart='" & ps & "' AND periodeend='" & pe & "' AND Sub.LocCode like '" & strLocation & "%'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIDERES_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            txtpstart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("periodestart"))
            txtpend.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("periodeend"))
            txt_kkk_psn.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("KKK_Psn"))
            txt_lm_psn.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("LM_psn"))
            txt_kkk_bss.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("KKK_Basis"))
            txt_lm_bss.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("LM_Basis"))
            txt_kkk_ob.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("KKK_RpOB"))
            txt_lm_ob.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("LM_RpOB"))
            txt_gt_ob.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("GT_RpOB"))

            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindButton()
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_BJR_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End If
    End Sub

    Sub onLoad_BindButton()
        Select Case intStatus
            Case "0"
                DelBtn.Visible = False
                UnDelBtn.Visible = False
            Case "1"
                DelBtn.Visible = True
                UnDelBtn.Visible = False
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                UnDelBtn.Attributes("onclick") = "javascript:return ConfirmAction('Undelete');"
                DelBtn.Visible = False
                UnDelBtn.Visible = True
        End Select


    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_PREMIDERES_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim typlant As String = ""
        Dim strstatus As String = ""
        Dim objID As New Object()

        If txtpstart.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input periode start !"
            Exit Sub
        End If

        If txtpend.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input periode end !"
            Exit Sub
        End If

        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If


        ParamNama = "PS|PE|LOC|KKK_psn|LM_psn|KKK_bss|LM_bss|KKK_rp|LM_rp|GT_rp|ST|CD|UD|UI"
        ParamValue = txtpstart.Text.Trim & "|" & _
                     txtpend.Text.Trim & "|" & _
                     strLocation & "|" & _
                     txt_kkk_psn.Text & "|" & _
                     txt_lm_psn.Text & "|" & _
                     txt_kkk_bss.Text & "|" & _
                     txt_lm_bss.Text & "|" & _
                     txt_kkk_ob.Text & "|" & _
                     txt_lm_ob.Text & "|" & _
                     txt_gt_ob.Text & "|" & _
                     strstatus & "|" & _
                     DateTime.Now & "|" & DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIDERES_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Response.Redirect("PR_setup_PrmKaretList_Estate.aspx")
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_PrmKaretList_Estate.aspx")
    End Sub


End Class
