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


Public Class PR_setup_BlokBJRDet_Estate : Inherits Page

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
    Protected WithEvents ddlypcode As DropDownList
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
            BCode = Trim(IIf(Request.QueryString("blkcode") <> "", Request.QueryString("blkcode"), Request.Form("blkcode")))

            If Not IsPostBack Then
                If PStart <> "" And PEnd <> "" And BCode <> "" Then
                    onLoad_Display(PStart, PEnd, BCode)
                Else
                    intStatus = 0
                    txtpstart.Text = Format(DateTime.Now, "MMyyyy")
                    txtpend.Text = Format(DateTime.Now, "MMyyyy")
                    onLoad_BindYearPlan("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display(ByVal ps As String, ByVal pe As String, ByVal bc As String)
        Dim strOpCd_Get As String = "PR_PR_STP_BLOK_BJR_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim objDeptDs As New Object()


        strSearch = "AND periodestart='" & ps & "' AND periodeend='" & pe & "' AND sub.blkcode='" & bc & "' AND Sub.LocCode like '" & strLocation & "%'"
        sortitem = ""
        ParamNama = "SEARCH|SORT|LOC"
        ParamValue = strSearch & "|" & sortitem & "|" & strLocation

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            txtpstart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("periodestart"))
            txtpend.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("periodeend"))
            txtBJR.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("bjr"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindYearPlan(Trim(objDeptDs.Tables(0).Rows(0).Item("blkcode")))
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
        Dim strOpCd_Upd As String = "PR_PR_STP_BLOK_BJR_UPD"
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

        If ddlypcode.SelectedItem.Value = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please year plant code !"
            Exit Sub
        End If


        If txtBJR.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input bjr !"
            Exit Sub
        End If


        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If


        ParamNama = "PS|PE|BC|LOC|BJR|ST|CD|UD|UI"
        ParamValue = txtpstart.Text.Trim & "|" & _
                     txtpstart.Text.Trim & "|" & _
                     ddlypcode.SelectedItem.Value & "|" & _
                     strLocation & "|" & _
                     txtBJR.Text & "|" & strstatus & "|" & _
                     DateTime.Now & "|" & DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_BJR_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Response.Redirect("PR_setup_BlokBJRlist_Estate.aspx")
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_BlokBJRlist_Estate.aspx")
    End Sub


End Class
