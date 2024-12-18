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


Public Class PR_setup_NormaDet_Estate : Inherits Page

    Protected WithEvents ddlsubcat As DropDownList
    Protected WithEvents ddljob As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents NorCode As HtmlInputHidden
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

    Protected WithEvents lblnormacode As Label
    Protected WithEvents txtJobNote As TextBox
    Protected WithEvents txtMinUmr As TextBox
    Protected WithEvents txtMaxUmr As TextBox

    Protected WithEvents txtrotthn As TextBox
    Protected WithEvents txthkharot As TextBox

    Protected WithEvents validateNoBlok As RequiredFieldValidator
    Protected WithEvents revCode As RegularExpressionValidator


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

    Dim strSelectedSalCode As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim Prefix As String

    Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrNo As Integer

        Prefix = "NRM" 

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||7", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

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
            strSelectedSalCode = Trim(IIf(Request.QueryString("NormaCode") <> "", Request.QueryString("NormaCode"), Request.Form("SalaryCode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedSalCode <> "" Then
                    NorCode.Value = strSelectedSalCode
                    onLoad_Display()
                Else
                    onLoad_BindSubCat("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_BK_NORMA_GET_LIST"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim Gol As String
        Dim Astek As String
        Dim Beras As String

        strSearch = "AND A.NormaCode like '" & NorCode.Value & "%' "
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
            lblnormacode.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("NormaCode"))
            txtJobNote.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("JobNote"))
            txtMinUmr.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("umurmin"))
            txtMaxUmr.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("umurmax"))
            txtrotthn.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("rot_thn"))
            txthkharot.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("hk_ha_rot"))

            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))

            onLoad_BindSubCat(Trim(objDeptDs.Tables(0).Rows(0).Item("idSubCat")))
            onLoad_BindJob(Trim(objDeptDs.Tables(0).Rows(0).Item("idSubCat")), Trim(objDeptDs.Tables(0).Rows(0).Item("CodeJob")))
            onLoad_BindButton()
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPSALARY_GET&errmesg=" & lblNoRecord.Text & "&redirect=")
        End If
    End Sub

    Sub onLoad_BindButton()

        ddlsubcat.Enabled = False
        ddljob.Enabled = False
        txtMinUmr.Enabled = False
        txtMaxUmr.Enabled = False
        txtJobNote.Enabled = False
        txtrotthn.Enabled = False
        txthkharot.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case "1"
                ddlsubcat.Enabled = True
                ddljob.Enabled = True
                txtMinUmr.Enabled = True
                txtMaxUmr.Enabled = True
                txtJobNote.Enabled = True
                txtrotthn.Enabled = True
                txthkharot.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                UnDelBtn.Visible = True
            Case Else
                ddlsubcat.Enabled = True
                ddljob.Enabled = True
                txtMinUmr.Enabled = True
                txtMaxUmr.Enabled = True
                txtJobNote.Enabled = True
                txtrotthn.Enabled = True
                txthkharot.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub

    Sub onLoad_BindSubCat(ByVal pv_strEmpTyCode As String)
        Dim strOpCd_DivId As String = "PR_PR_STP_BK_SUBCATEGORY_GET_LIST"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objJobGroup As New Object

        ParamNama = "SEARCH|SORT"
        ParamValue = "|Order by SubCatName"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBCATEGORY_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objJobGroup.Tables(0).Rows.Count - 1
            objJobGroup.Tables(0).Rows(intCnt).Item("SubCatID") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("SubCatID"))
            objJobGroup.Tables(0).Rows(intCnt).Item("SubCatName") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("SubCatName"))
            If objJobGroup.Tables(0).Rows(intCnt).Item("SubCatID") = pv_strEmpTyCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objJobGroup.Tables(0).NewRow()
        dr("SubCatID") = ""
        dr("SubCatName") = "Select sub kategori"
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)

        ddlsubcat.DataSource = objJobGroup.Tables(0)
        ddlsubcat.DataValueField = "SubCatID"
        ddlsubcat.DataTextField = "SubCatName"
        ddlsubcat.DataBind()
        ddlsubcat.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindJob(ByVal pv_strEmpTyCode As String, ByVal jc As String)
        Dim strOpCd_DivId As String = "PR_PR_STP_BK_JOB_GET_BY_SUBCAT"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objJobGroup As New Object

        ParamNama = "SEARCH|SORT"
        ParamValue = " AND subcatid like '%" & pv_strEmpTyCode & "%'|Order by JobName"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_DivId, ParamNama, ParamValue, objJobGroup)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBCATEGORY_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objJobGroup.Tables(0).Rows.Count - 1
            objJobGroup.Tables(0).Rows(intCnt).Item("jobcode") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("jobcode"))
            objJobGroup.Tables(0).Rows(intCnt).Item("JobName") = Trim(objJobGroup.Tables(0).Rows(intCnt).Item("JobName"))
            If Trim(objJobGroup.Tables(0).Rows(intCnt).Item("jobcode")) = Trim(jc) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objJobGroup.Tables(0).NewRow()
        dr("jobcode") = ""
        dr("JobName") = "Select aktiviti"
        objJobGroup.Tables(0).Rows.InsertAt(dr, 0)

        ddljob.DataSource = objJobGroup.Tables(0)
        ddljob.DataValueField = "jobcode"
        ddljob.DataTextField = "JobName"
        ddljob.DataBind()
        ddljob.SelectedIndex = intSelectedIndex
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

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_BK_NORMA_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim valSalCd As String
        Dim valSubCat As String
        Dim valJob As String
        Dim strstatus As String = ""
        Dim objID As New Object()

        If ddlsubcat.SelectedItem.Value = "" Then
            lblNoDeptCode.Visible = True
            lblNoDeptCode.Text = "Please Select sub kategori "
            Exit Sub
        End If

        If ddljob.SelectedItem.Value = "" Then
            lblNoDeptCode.Visible = True
            lblNoDeptCode.Text = "Please Select Aktiviti "
            Exit Sub
        End If

        If intStatus = 0 Then
            valSalCd = getCode()
        Else
            valSalCd = strSelectedSalCode
        End If


        NorCode.Value = valSalCd
        strSelectedSalCode = NorCode.Value.Trim()

        valSubCat = ddlsubcat.SelectedItem.Value
        valJob = ddljob.SelectedItem.Value.Trim()



        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If

        ParamNama = "NC|SC|JC|JN|UMIN|UMAX|RT|HHR|ST|CD|UD|UI"
        ParamValue = valSalCd & "|" & valSubCat & "|" & valJob & "|" & Trim(txtJobNote.Text) & "|" & _
                     txtMinUmr.Text & "|" & txtMaxUmr.Text & "|" & _
                     txtrotthn.Text & "|" & txthkharot.Text & "|" & _
                     strstatus & "|" & DateTime.Now & "|" & DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPSALARY_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If strSelectedSalCode <> "" Then
            onLoad_Display()
        End If

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_NormaList_Estate.aspx")
    End Sub



    Protected Sub ddlsubcat_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindJob(ddlsubcat.SelectedItem.Value.Trim(), "")
    End Sub

End Class
