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


Public Class PR_setup_ComponentGajiDet_Estate : Inherits Page

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

    Protected WithEvents txtidx As TextBox
    Protected WithEvents ddldivisi As DropDownList
    Protected WithEvents ddltype As DropDownList
    Protected WithEvents ddlcoa As DropDownList

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
    Dim intErrNo As Integer
    Dim Prefix As String
    Dim strAcceptFormat As String

    Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object

        Prefix = "KG" & strLocation

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||9", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Function getcodetmp() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETIDTMP"
        Dim objNewID As New Object

        Prefix = "KG" & strLocation
        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||9", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
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

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedBlockCode = Trim(IIf(Request.QueryString("KGCode") <> "", Request.QueryString("KGCode"), Request.Form("KGCode")))
            If Not IsPostBack Then
                If strSelectedBlockCode <> "" Then
                    isNew.Value = "False"
                    BlokCode.Value = strSelectedBlockCode
                    onLoad_Display()
                Else
                    intStatus = 0
                    isNew.Value = "True"
                    txtidx.Text = getcodetmp()
                    onLoad_BindDivisi("")
                    onLoad_BindType("")
                    onLoad_BindCOA("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_BK_COMPONENTGAJI_GET_LIST"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim objDeptDs As New Object()


        strSearch = " AND KGCode='" & BlokCode.Value & "'"
        sortitem = ""
        ParamNama = "LOC|SEARCH|SORT"
        ParamValue = strLocation & "|" & strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_COMPONENTGAJI_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            isNew.Value = "False"
            txtidx.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("KGCode"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindDivisi(Trim(objDeptDs.Tables(0).Rows(0).Item("IDDiv")))
            onLoad_BindType(Trim(objDeptDs.Tables(0).Rows(0).Item("TyAcc")))
            onLoad_BindCOA(Trim(objDeptDs.Tables(0).Rows(0).Item("COA")))
            onLoad_BindButton()

        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End If
    End Sub

    Sub onLoad_BindButton()
        Select Case intStatus
            Case "0"
                txtidx.Enabled = False
                DelBtn.Visible = False
                UnDelBtn.Visible = False
            Case "1"
                txtidx.Enabled = False
                DelBtn.Visible = True
                UnDelBtn.Visible = False
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                txtidx.Enabled = False
                UnDelBtn.Attributes("onclick") = "javascript:return ConfirmAction('Undelete');"
                DelBtn.Visible = False
                UnDelBtn.Visible = True
        End Select


    End Sub

    Sub onLoad_BindDivisi(ByVal pv_strYrPln As String)
        Dim strOpCd_yearPlan As String = "PR_PR_STP_DIVISICODE_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim sortitem As String

        strSearch = "AND A.LocCode='" & strLocation & "' And A.Status='1'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_yearPlan, ParamNama, ParamValue, objYearPlan)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEPTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objYearPlan.Tables(0).Rows.Count - 1
            objYearPlan.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objYearPlan.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
            objYearPlan.Tables(0).Rows(intCnt).Item("Description") = Trim(objYearPlan.Tables(0).Rows(intCnt).Item("Description"))

            If Trim(objYearPlan.Tables(0).Rows(intCnt).Item("BlkGrpCode")) = Trim(pv_strYrPln) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objYearPlan.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "Select Divisi"
        objYearPlan.Tables(0).Rows.InsertAt(dr, 0)

        ddldivisi.DataSource = objYearPlan.Tables(0)
        ddldivisi.DataValueField = "BlkGrpCode"
        ddldivisi.DataTextField = "Description"
        ddldivisi.DataBind()
        ddldivisi.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindType(ByVal pv_strYrPln As String)
        Dim strOpCd_yearPlan As String = "PR_PR_STP_BK_COMPONENTGAJI_GET_TYPE"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim sortitem As String

        strSearch = "WHERE Status='1'"
        sortitem = "order by Description"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_yearPlan, ParamNama, ParamValue, objYearPlan)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_COMPONENTGAJI_GET_TYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objYearPlan.Tables(0).Rows.Count - 1
            objYearPlan.Tables(0).Rows(intCnt).Item("AccTy") = Trim(objYearPlan.Tables(0).Rows(intCnt).Item("AccTy"))
            objYearPlan.Tables(0).Rows(intCnt).Item("Description") = Trim(objYearPlan.Tables(0).Rows(intCnt).Item("Description"))

            If Trim(objYearPlan.Tables(0).Rows(intCnt).Item("AccTy")) = Trim(pv_strYrPln) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objYearPlan.Tables(0).NewRow()
        dr("AccTy") = ""
        dr("Description") = "Select Type"
        objYearPlan.Tables(0).Rows.InsertAt(dr, 0)

        ddltype.DataSource = objYearPlan.Tables(0)
        ddltype.DataValueField = "AccTy"
        ddltype.DataTextField = "Description"
        ddltype.DataBind()
        ddltype.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindCOA(ByVal pv_strYrPln As String)
        Dim strOpCd_yearPlan As String = "PR_PR_STP_BK_COMPONENTGAJI_COA_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim sortitem As String

        strSearch = IIf(Session("SS_COACENTRALIZED") = "1", "And Status='1'", " AND LocCode = '" & Trim(strLocation) & "' And Status='1' ")
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_yearPlan, ParamNama, ParamValue, objYearPlan)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_COMPONENTGAJI_COA_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objYearPlan.Tables(0).Rows.Count - 1
            objYearPlan.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objYearPlan.Tables(0).Rows(intCnt).Item("AccCode"))
            objYearPlan.Tables(0).Rows(intCnt).Item("Description") = Trim(objYearPlan.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(objYearPlan.Tables(0).Rows(intCnt).Item("Description")) & ")"

            If Trim(objYearPlan.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(pv_strYrPln) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objYearPlan.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Select COA"
        objYearPlan.Tables(0).Rows.InsertAt(dr, 0)

        ddlcoa.DataSource = objYearPlan.Tables(0)
        ddlcoa.DataValueField = "AccCode"
        ddlcoa.DataTextField = "Description"
        ddlcoa.DataBind()
        ddlcoa.SelectedIndex = intSelectedIndex
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_BK_COMPONENTGAJI_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim valty As String = ""
        Dim strstatus As String = ""
        Dim objID As New Object()

        If ddldivisi.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select divisi !"
            Exit Sub
        End If

        If ddltype.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select type !"
            Exit Sub
        End If

        If ddlcoa.SelectedItem.Value = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select coa !"
            Exit Sub
        End If

        If isNew.Value = "True" Then
            txtidx.Text = getCode()
        End If


        BlokCode.Value = Trim(txtidx.Text)
        strSelectedBlockCode = Trim(BlokCode.Value)

        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If


        ParamNama = "ID|DI|LOC|COA|TY|CD|UD|UI|ST"
        ParamValue = txtidx.Text.Trim & "|" & _
                     ddldivisi.SelectedItem.Value.Trim & "|" & _
                     strLocation & "|" & _
                     ddlcoa.SelectedItem.Value.Trim & "|" & _
                     ddltype.SelectedItem.Value.Trim & "|" & _
                     DateTime.Now & "|" & DateTime.Now & "|" & strUserId & "|" & strstatus

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_COMPONENTGAJI_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        isNew.Value = "False"
        Response.Redirect("PR_setup_ComponentGajiList_Estate.aspx")
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_ComponentGajiList_Estate.aspx")
    End Sub


End Class
