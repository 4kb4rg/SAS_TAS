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


Public Class PR_setup_PrmiMndor_Estate : Inherits Page


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
    Protected WithEvents lblNoLocCode As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblDeptHead As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblList As Label
    Protected WithEvents txtid As TextBox
    Protected WithEvents txtPMP As TextBox
    Protected WithEvents txtpstart As TextBox
    Protected WithEvents txtpend As TextBox
    Protected WithEvents txtPMB As TextBox
    Protected WithEvents txtPKP As TextBox
    Protected WithEvents txtPMT As TextBox
    Protected WithEvents txtPKT As TextBox
    Protected WithEvents isNew As HtmlInputHidden
    
    Protected WithEvents txtSPL As TextBox

    Protected WithEvents txtInit As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents ddlLahan As DropDownList
    Protected WithEvents txtBasis As TextBox
    Protected WithEvents txtUOM As TextBox
    Protected WithEvents txtRate As TextBox
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents hidPMdrLnID As HtmlInputHidden

    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents revCode As RegularExpressionValidator


    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim ObjOK As New agri.GL.ClsTrx


    Dim objDeptDs As New Object()


    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intHRAR As Long

    Dim strId As String = ""

    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim Prefix As String = "PM"

    Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrno As Integer

        Try
            intErrno = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & strLocation & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Function getcodetmp() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETIDTMP"
        Dim objNewID As New Object
        Dim intErrno As Integer

        Try
            intErrno = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & strLocation & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strId = Trim(IIf(Request.QueryString("PMdrID") <> "", Request.QueryString("PMdrID"), Request.Form("PMdrID")))

            lblErrMessage.Visible = False
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strId <> "" Then
                    isNew.Value = "False"
                    onLoad_Display()
                    BindGrid()
                Else
                    isNew.Value = "True"
                    txtid.Text = getcodetmp()
                    onLoad_BindButton()
                End If
                onLoad_Display()
                BindGrid()
            End If
        End If

    End Sub


    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_PREMI_MANDOR_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String


        strSearch = "AND A.Status=1 AND A.LocCode='" & strLocation & "' And PMdrID='" & strId & "' "
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_MANDOR_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblHiddenSts.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Status"))
            txtid.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PMdrID"))
            txtpstart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeStart"))
            txtpend.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeEnd"))
            txtPMP.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PsnMdrPanen"))
            txtPMB.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PsnMdrBrondol"))
            txtPKP.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PsnMdrKcs"))
            txtPMT.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PsnMdrTraksi"))
            txtPKT.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PayKrnTimbang"))
            txtSPL.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PayMdrSipil"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindButton()
        End If

    End Sub

    Sub onLoad_BindButton()

        SaveBtn.Visible = False
        UnDelBtn.Visible = False
        DelBtn.Visible = False

        Select Case Trim(intStatus)
            Case "1"
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                UnDelBtn.Visible = True
            Case Else
                SaveBtn.Visible = True
        End Select

    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_PREMI_MANDOR_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strstatus As String = ""
        Dim objID As New Object()

        If txtPMB.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please Input Mandor Panen"
            Exit Sub
        End If

        If txtPMP.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please Input Mandor Brondolan"
            Exit Sub
        End If

        If txtPKP.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please Input KCS"
            Exit Sub
        End If

        If txtPKT.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please Input Krani Timbangan"
            Exit Sub
        End If

        If txtPMT.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please Krani Traksi"
            Exit Sub
        End If

        If txtSPL.Text = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please Input Mandor Sipil"
            Exit Sub
        End If

        If isNew.Value = "True" Then
            txtid.Text = getCode()
        End If


        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If

        strId = txtid.Text.Trim

        ParamNama = "ID|LOC|PS|PE|PMP|PMB|PKP|PMT|PKT|SPL|ST|CD|UD|UI"
        ParamValue = txtid.Text.Trim & "|" & _
                     strLocation & "|" & _
                     txtpstart.Text.Trim & "|" & _
                     txtpend.Text.Trim & "|" & _
                     txtPMP.Text & "|" & txtPMB.Text & "|" & txtPKP.Text & "|" & txtPMT.Text & "|" & txtPKT.Text & "|" & _
                     txtSPL.Text & "|" & _
                     strstatus & "|" & DateTime.Now & "|" & DateTime.Now & "|" & strUserId


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_MANDOR_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        isNew.Value = "False"

        If strId <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_PrmiMndorList_Estate.aspx")
    End Sub


    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_PREMI_MANDOR_UPD_LINE"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim id As String = ""
        Dim objID As New Object()

        If txtInit.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input initial description"
            Exit Sub
        End If

        If txtBasis.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input basis"
            Exit Sub
        End If

        If txtUOM.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input uom"
            Exit Sub
        End If

        If txtRate.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input rate"
            Exit Sub
        End If

        strId = txtid.Text.Trim
        If strId = "" Then
            lblErrMessage.Text = "Please save before add detail premi rawat"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        ParamNama = "ID|PMDRLNID|RWTINIT|RWTDESCR|RWTLAHAN|RWTBASIS|RWTUOM|RWTRATE|UD|UI"
        ParamValue = txtid.Text.Trim & "|" & _
                     hidPMdrLnID.Value & "|" & _
                     UCase(txtInit.Text) & "|" & _
                     txtDesc.Text & "|" & _
                     ddlLahan.SelectedItem.Value & "|" & _
                     txtBasis.Text & "|" & _
                     txtUOM.Text & "|" & _
                     txtRate.Text & "|" & _
                     DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_KERAJINAN_UPD_LINE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        onLoad_Display()
        BindGrid()

        txtInit.Text = ""
        txtDesc.Text = ""
        txtBasis.Text = "0"
        txtUOM.Text = ""
        txtRate.Text = "0"
        hidPMdrLnID.Value = ""
    End Sub

    Sub BindGrid()
        Dim dsData As DataSet
        Dim intCnt As Integer
        Dim DeleteButton As LinkButton
        Dim EditButton As LinkButton
        Dim CancelButton As LinkButton

        dsData = LoadData()
        dgLineDet.DataSource = dsData.Tables(0)
        dgLineDet.DataBind()

        For intCnt = 0 To dsData.Tables(0).Rows.Count - 1
            DeleteButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
            DeleteButton.Visible = True
            DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            EditButton = dgLineDet.Items.Item(intCnt).FindControl("lbEdit")
            EditButton.Visible = True
            CancelButton = dgLineDet.Items.Item(intCnt).FindControl("lbCancel")
            CancelButton.Visible = False
        Next
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "PR_PR_STP_PREMI_MANDOR_GET_LINE"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String

        strSearch = "WHERE PMdrID='" & txtid.Text.Trim & "'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_KERAJINAN_GET_LINE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objDeptDs
    End Function

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lbl As Label

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lbl = E.Item.FindControl("lblRwtInit")
        txtInit.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblPMdrLnID")
        hidPMdrLnID.Value = lbl.Text.Trim

        lbl = E.Item.FindControl("lblRwtDescr")
        txtDesc.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblRwtLahan")
        ddlLahan.SelectedValue = lbl.Text.Trim
        lbl = E.Item.FindControl("lblRwtBasis")
        txtBasis.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblRwtUOM")
        txtUOM.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblRwtRate")
        txtRate.Text = lbl.Text.Trim

        btn = E.Item.FindControl("lbDelete")
        btn.Visible = False
        btn = E.Item.FindControl("lbEdit")
        btn.Visible = False
        btn = E.Item.FindControl("lbCancel")
        btn.Visible = True
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgLineDet.EditItemIndex = -1
        BindGrid()

        txtInit.Text = ""
        txtDesc.Text = ""
        txtBasis.Text = "0"
        txtUOM.Text = ""
        txtRate.Text = "0"
        hidPMdrLnID.Value = ""
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_PREMI_MANDOR_DEL_LINE"
        Dim lbl As Label
        Dim intErrNo As Integer
        Dim id As String = ""
        Dim objID As New Object()

        lbl = E.Item.FindControl("lblPMdrLnID")
        hidPMdrLnID.Value = lbl.Text

        ParamNama = "ID|PMDRLNID|UD|UI"
        ParamValue = txtid.Text & "|" & _
                     hidPMdrLnID.Value & "|" & _
                     DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_KERAJINAN_DEL_LINE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        BindGrid()
    End Sub
End Class
