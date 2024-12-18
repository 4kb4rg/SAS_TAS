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


Public Class HR_setup_Blokdet_Estate : Inherits Page

    Protected WithEvents ddlDivId As DropDownList
    Protected WithEvents ddlCompCode As DropDownList
    Protected WithEvents ddlLocCode As DropDownList
    Protected WithEvents ddlYearPlan As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents BlokCode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupDept As Label
    Protected WithEvents lblNoDeptCode As Label
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
    Protected WithEvents lblYearPlant As Label
    Protected WithEvents txtLuas As TextBox
    Protected WithEvents txtTotPKK As TextBox
    Protected WithEvents txtBJR As TextBox
    Protected WithEvents txtNoBlok As TextBox
    Protected WithEvents validateNoBlok As Label

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

    Dim strSelectedBlockCode As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim Prefix As String

    Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrNo As Integer
		
		prefix = "BK"+trim(strlocation)

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||10", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=")
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

        Dim Prefix As String = "BK" & strLocation

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblNoDeptCode.Visible = False
            lblYearPlant.Visible = False
            validateNoBlok.Visible = False
            strSelectedBlockCode = Trim(IIf(Request.QueryString("BlokCode") <> "", Request.QueryString("BlokCode"), Request.Form("BlokCode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedBlockCode <> "" Then
                    BlokCode.Value = strSelectedBlockCode
                    onLoad_Display()
                Else
                    onLoad_BindDIvCode("")
                    onLoad_BindYearPlan("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "HR_HR_STP_BLOK_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim objDeptDs As New Object()


        strSearch = "AND A.BlokCode like '" & BlokCode.Value & "%' AND A.LocCode like '" & strLocation & "%'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblHiddenSts.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Status"))
            txtNoBlok.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("NoBlok"))
            txtLuas.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Luas"))
            txtTotPKK.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("TotPKK"))
            txtBJR.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("BJR"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindDIvCode(Trim(objDeptDs.Tables(0).Rows(0).Item("IDDiv")))
            onLoad_BindYearPlan(Trim(objDeptDs.Tables(0).Rows(0).Item("YearPlan")))
            onLoad_BindButton()
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_BLOK_GET&errmesg=" & lblNoRecord.Text & "&redirect=hr/setup/HR_setup_Deptlist.aspx")
        End If
    End Sub

    Sub onLoad_BindButton()
        txtNoBlok.Enabled = False
        txtLuas.Enabled = False
        txtTotPKK.Enabled = False
        txtBJR.Enabled = False
        ddlDivId.Enabled = False
        ddlYearPlan.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case "1"
                txtNoBlok.Enabled = True
                txtLuas.Enabled = True
                txtTotPKK.Enabled = True
                txtBJR.Enabled = True
                ddlDivId.Enabled = True
                ddlYearPlan.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                UnDelBtn.Visible = True
            Case Else
                txtNoBlok.Enabled = True
                txtLuas.Enabled = True
                txtTotPKK.Enabled = True
                txtBJR.Enabled = True
                ddlDivId.Enabled = True
                ddlYearPlan.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub

    Sub onLoad_BindDIvCode(ByVal pv_strDivCode As String)
        Dim strOpCd_DivId As String = "HR_HR_STP_DIVISI_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim strSearch_all As String = ""
        Dim sortitem As String

        strSearch = "And A.Status='1'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objDeptCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEPTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objDeptCodeDs.Tables(0).Rows.Count - 1
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("DivID") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("DivID"))
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("CodeDiv") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("DivID")) & " (" & Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDeptCodeDs.Tables(0).Rows(intCnt).Item("DivID") = pv_strDivCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDeptCodeDs.Tables(0).NewRow()
        dr("DivID") = ""
        dr("CodeDiv") = "Select Division ID"
        objDeptCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDivId.DataSource = objDeptCodeDs.Tables(0)
        ddlDivId.DataValueField = "DivID"
        ddlDivId.DataTextField = "CodeDiv"
        ddlDivId.DataBind()
        ddlDivId.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindYearPlan(ByVal pv_strYrPln As String)
        Dim strOpCd_yearPlan As String = "HR_HR_STP_YEARPLAN_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim strSearch_all As String = ""
        Dim sortitem As String

        strSearch = "And A.Status='1'"
        sortitem = ""
        ParamNama = "STRSEARCH|SORTEXP"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_yearPlan, ParamNama, ParamValue, objYearPlan)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEPTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objYearPlan.Tables(0).Rows.Count - 1
            objYearPlan.Tables(0).Rows(intCnt).Item("YearPlan") = Trim(objYearPlan.Tables(0).Rows(intCnt).Item("YearPlan"))
            If Trim(objYearPlan.Tables(0).Rows(intCnt).Item("YearPlan")) = Trim(pv_strYrPln) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objYearPlan.Tables(0).NewRow()
        dr("YearPlan") = "Select Year Plant"
        objYearPlan.Tables(0).Rows.InsertAt(dr, 0)

        ddlYearPlan.DataSource = objYearPlan.Tables(0)
        ddlYearPlan.DataValueField = "YearPlan"
        ddlYearPlan.DataBind()
        ddlYearPlan.SelectedIndex = intSelectedIndex
    End Sub

    Function getBJR(ByVal thntnm As String) As String
        Dim strOpCd_GetID As String = "HR_HR_STP_YEARPLAN_GET"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim ParamName As String
        Dim ParamValue As String

        ParamName = "STRSEARCH|SORTEXP"
        ParamValue = "AND YearPlan='" & thntnm & "'|"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_YEARPLAN_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("BJR").ToString)

    End Function

    Sub ddlYearPlan_selectedindexchanges(ByVal sender As Object, ByVal e As EventArgs)
        txtBJR.Text = getBJR(ddlYearPlan.SelectedItem.Value.Trim())
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "HR_HR_STP_BLOK_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim valBlkCd As String
        Dim valdivID As String
        Dim valYearPlant As String
        Dim strDesc As String
        Dim strstatus As String = ""
        Dim objID As New Object()


        

        If ddlDivId.SelectedItem.Value = "" Then
            lblNoDeptCode.Visible = True
            lblNoDeptCode.Text = "Please Select Division ID !"
            Exit Sub
        End If

        If txtNoBlok.Text = "" Then
            validateNoBlok.Visible = True
            validateNoBlok.Text = "Please input No Block !"
            Exit Sub
        End If

        If ddlYearPlan.SelectedItem.Value = "Select Year Plant" Then
            lblYearPlant.Visible = True
            lblYearPlant.Text = "Please Select Year Plant !"
            Exit Sub
        End If

        

        If intStatus = 0 Then
            valBlkCd = getCode()
        Else
            valBlkCd = strSelectedBlockCode
        End If


        BlokCode.Value = Trim(valBlkCd)
        strSelectedBlockCode = Trim(BlokCode.Value)

        strDesc = "Block " & Trim(txtNoBlok.Text)
        valdivID = ddlDivId.SelectedValue
        valYearPlant = ddlYearPlan.SelectedValue

        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then

            strstatus = "1"

        ElseIf strCmdArgs = "Del" Then

            strstatus = "2"

        End If

        If txtLuas.Text = "" Then
            txtLuas.Text = "0"
        End If

        If txtTotPKK.Text = "" Then
            txtTotPKK.Text = "0"
        End If



        ParamNama = "BlokCd|NoBlk|Desc|IdDiv|Luas|TotPKK|YP|BJR|LOC|CD|UD|UI|ST"
        ParamValue = valBlkCd & "|" & txtNoBlok.Text & "|" & strDesc & "|" & valdivID & "|" & txtLuas.Text & "|" & _
                     txtTotPKK.Text & "|" & valYearPlant & "|" & txtBJR.Text & "|" & strLocation & "|" & _
                     DateTime.Now & "|" & DateTime.Now & "|" & strUserId & "|" & strstatus



        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If valBlkCd <> "" Then
            BackBtn_Click(Sender, E)
        End If

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_setup_Bloklist_Estate.aspx")
    End Sub


End Class
