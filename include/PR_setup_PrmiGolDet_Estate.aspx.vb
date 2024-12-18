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


Public Class PR_setup_PrmiGolDet_Estate : Inherits Page


    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents GolId As HtmlInputHidden
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
    Protected WithEvents lblDeptHead As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblList As Label
    Protected WithEvents lblGolId As Label
    Protected WithEvents lblGolonganId As Label
    Protected WithEvents lblGol As Label
    Protected WithEvents txtUTMin As TextBox
    Protected WithEvents txtGolId As TextBox
    Protected WithEvents txtGol As TextBox
    Protected WithEvents txtUTMax As TextBox
    Protected WithEvents txtBTGsKG As TextBox
    Protected WithEvents txtBTGsBJR As TextBox
    Protected WithEvents txtBTGsJJG As TextBox
    Protected WithEvents txtBprmKG As TextBox
    Protected WithEvents txtBPrmJJG As TextBox
    Protected WithEvents txtPrmOt1 As TextBox
    Protected WithEvents txtPrmOt2 As TextBox
    Protected WithEvents txtPrmOt3 As TextBox
    Protected WithEvents txtPStart As TextBox
    Protected WithEvents txtPEnd As TextBox

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

    Dim strSelectedGolId As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim Prefix As String = "PGL"

    Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrno As Integer

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||8", objNewID)
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
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||8", objNewID)
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
         '   Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblGolId.Visible = False
           
            strSelectedGolId = Trim(IIf(Request.QueryString("GolId") <> "", Request.QueryString("GolId"), Request.Form("GolId")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedGolId <> "" Then
                    GolId.Value = strSelectedGolId
                    onLoad_Display()
                    txtGolId.Enabled = False
                Else
                    
                    onLoad_BindButton()
                End If
            End If
        End If

    End Sub


    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_PREMI_GOLONGAN_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String



        strSearch = "AND A.GolId like '" & Trim(GolId.Value) & "%'"
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
            txtGolId.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("GolId"))
            txtGol.Text = (Trim(objDeptDs.Tables(0).Rows(0).Item("Gol")))
            txtPStart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeStart"))
            txtPEnd.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeEnd"))
            txtUTMin.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UmrTnmMin"))
            txtUTMax.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UmrTnmMax"))
            txtBTGsKG.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("BTgsKg"))
            txtBTGsBJR.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("BTgsBJR"))
            txtBTGsJJG.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("BTgsJJG"))
            txtBPrmJJG.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("BPrmJJG"))
            txtBPrmKG.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("BPrmKg"))
            txtPrmOt1.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PrmOvr1"))
            txtPrmOt2.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PrmOvr2"))
            txtPrmOt3.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PrmOvr3"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindButton()
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_GET&errmesg=" & lblNoRecord.Text & "&redirect=pr/setup/PR_setup_PrmBrsDet_Estate.aspx")
        End If

    End Sub

    Sub onLoad_BindButton()

        lblGolonganId.Enabled = True
        txtGolId.Enabled = False
        txtGol.Enabled = False
        txtUTMin.Enabled = False
        txtUTMax.Enabled = False
        txtBTGsKG.Enabled = False
        txtBTGsBJR.Enabled = False
        txtBTGsJJG.Enabled = False
        txtBprmKG.Enabled = False
        txtBPrmJJG.Enabled = False
        txtPrmOt1.Enabled = False
        txtPrmOt2.Enabled = False
        txtPrmOt3.Enabled = False
        SaveBtn.Visible = False
        UnDelBtn.Visible = False
        DelBtn.Visible = False

        Select Case Trim(intStatus)
            Case "1"
                lblGolonganId.Visible = True
                txtGolId.Visible = True
                txtGol.Enabled = True
                txtUTMin.Enabled = True
                txtUTMax.Enabled = True
                txtBTGsKG.Enabled = True
                txtBTGsBJR.Enabled = True
                txtBTGsJJG.Enabled = True
                txtBprmKG.Enabled = True
                txtBPrmJJG.Enabled = True
                txtPrmOt1.Enabled = True
                txtPrmOt2.Enabled = True
                txtPrmOt3.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                lblGolonganId.Visible = True
                txtGolId.Visible = True
                UnDelBtn.Visible = True
            Case Else
                lblGolonganId.Visible = True
                txtGolId.Visible = True
                txtGolId.Text = getcodetmp()
                txtGol.Enabled = True
                txtUTMin.Enabled = True
                txtUTMax.Enabled = True
                txtBTGsKG.Enabled = True
                txtBTGsBJR.Enabled = True
                txtBTGsJJG.Enabled = True
                txtBprmKG.Enabled = True
                txtBPrmJJG.Enabled = True
                txtPrmOt1.Enabled = True
                txtPrmOt2.Enabled = True
                txtPrmOt3.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_PREMI_GOLONGAN_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strstatus As String = ""
        Dim objID As New Object()


        If intStatus = 0 Then
            txtGolId.Text = getCode()
        Else
            txtGolId.Text = strSelectedGolId
        End If


        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If


        GolId.Value = Trim(txtGolId.Text)
        strSelectedGolId = GolId.Value


        ParamNama = "GI|PS|PE|UTMin|UTMax|Gol|BTKg|BTBJR|BTJJG|BPJJG|BPKG|POvr1|POvr2|POvr3|ST|CD|UD|UI"
        ParamValue = txtGolId.Text & "|" & txtPStart.Text & "|" & txtPEnd.Text & "|" & txtUTMin.Text & "|" & txtUTMax.Text & "|" & txtGol.Text & "|" & _
                      txtBTGsKG.Text & "|" & txtBTGsBJR.Text & "|" & txtBTGsJJG.Text & "|" & txtBPrmJJG.Text & "|" & _
                      txtBprmKG.Text & "|" & txtPrmOt1.Text & "|" & txtPrmOt2.Text & "|" & txtPrmOt3.Text & "|" & _
                      strstatus & "|" & DateTime.Now & "|" & DateTime.Now & "|" & strUserId


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If strSelectedGolId <> "" Then
            onLoad_Display()
        End If

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_PrmiGolList_Estate.aspx")
    End Sub


End Class
