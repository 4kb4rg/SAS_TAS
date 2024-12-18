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


Public Class PR_setup_PrmBrondolDet_Estate : Inherits Page


    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents GolId As HtmlInputHidden
    Protected WithEvents isNew As HtmlInputHidden

    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents lblGolId As Label
    Protected WithEvents lblPrmi As Label

    Protected WithEvents ddlGolId As DropDownList
    Protected WithEvents txtgol As TextBox
    Protected WithEvents txtpstart As TextBox
    Protected WithEvents txtpend As TextBox
    Protected WithEvents txtPrmiBrondol As TextBox
   

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
    Dim Prefix As String = "PBR"

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
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblGolId.Visible = False
            lblPrmi.Visible = False
            strSelectedGolId = Trim(IIf(Request.QueryString("PRBrondolCode") <> "", Request.QueryString("PRBrondolCode"), Request.Form("PRBrondolCode")))

            If Not IsPostBack Then
                If strSelectedGolId <> "" Then
                    GolId.Value = strSelectedGolId
                    isNew.Value = "False"
                    onLoad_Display()
                Else
                    isNew.Value = "True"
                    txtgol.Text = getcodetmp()
                    onLoad_BindGolId("")
                    onLoad_BindButton()
                End If
            End If
        End If

    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_PREMI_BERONDOL_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String



        strSearch = "AND A.PRBrondolCode like '" & Trim(strSelectedGolId) & "%'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_BERONDOL_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            txtgol.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PRBrondolCode"))
            txtpstart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("periodestart"))
            txtpend.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("periodeend"))

            onLoad_BindGolId(Trim(objDeptDs.Tables(0).Rows(0).Item("Gol")))
            txtPrmiBrondol.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PrmBdl"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindButton()
          End If

    End Sub

    Sub onLoad_BindGolId(ByVal pv_strBrsRt As String)
        Dim strOpCd_JobCode As String = "PR_PR_STP_PREMI_GOLONGAN_GET_LIST"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim strSearch_all As String = ""
        Dim sortitem As String

        strSearch = "WHERE Status='1'"
        sortitem = "order by Gol"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_JobCode, ParamNama, ParamValue, objJobCode)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_GOLONGAN_GET_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        For intCnt = 0 To objJobCode.Tables(0).Rows.Count - 1
            objJobCode.Tables(0).Rows(intCnt).Item("Gol") = Trim(objJobCode.Tables(0).Rows(intCnt).Item("Gol"))
            If objJobCode.Tables(0).Rows(intCnt).Item("Gol") = pv_strBrsRt Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objJobCode.Tables(0).NewRow()
        dr("Gol") = "Select Golongan ID"
        objJobCode.Tables(0).Rows.InsertAt(dr, 0)

        ddlGolId.DataSource = objJobCode.Tables(0)
        ddlGolId.DataValueField = "Gol"
        ddlGolId.DataTextField = "Gol"
        ddlGolId.DataBind()
        ddlGolId.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindButton()

        txtPrmiBrondol.Enabled = False
        SaveBtn.Visible = False
        UnDelBtn.Visible = False
        DelBtn.Visible = False

        Select Case Trim(intStatus)
            Case "1"
                txtPrmiBrondol.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                UnDelBtn.Visible = True
            Case Else
                txtPrmiBrondol.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_PREMI_BERONDOL_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strstatus As String = ""
        Dim objID As New Object()

        If ddlGolId.Text = "Select Golongan ID" Then
            lblPrmi.Visible = True
            lblPrmi.Text = "Pleae Select Golongan ID"
            Exit Sub
        End If

        If isNew.Value = "True" Then
            txtgol.Text = getCode()
        Else
            txtgol.Text = GolId.Value
        End If


        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then

            strstatus = "1"

        ElseIf strCmdArgs = "Del" Then

            strstatus = "2"

        End If


        strSelectedGolId = txtgol.Text


        ParamNama = "IG|PS|PE|GL|PB|ST|CD|UD|UI"
        ParamValue = Trim(txtgol.Text) & "|" & _
                     txtpstart.Text.Trim & "|" & _
                     txtpend.Text.Trim & "|" & _
                     ddlGolId.SelectedItem.Value.Trim & "|" & txtPrmiBrondol.Text & "|" & strstatus & "|" & DateTime.Now & "|" & _
                     DateTime.Now & "|" & strUserId


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_BERONDOL_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        isNew.Value = "False"

        If strSelectedGolId <> "" Then
            onLoad_Display()
        End If

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_PrmBrondolList_Estate.aspx")
    End Sub


End Class
