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


Public Class PR_setup_DendaDet_Estate : Inherits Page


    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents DendaCode As HtmlInputHidden
    Protected WithEvents isNew As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    
    Protected WithEvents lblDndCode As Label
    Protected WithEvents lblDesc As Label

    Protected WithEvents txtDesc As DropDownList
    Protected WithEvents txtDendaCd As TextBox
    Protected WithEvents txtpstart As TextBox
    Protected WithEvents txtpend As TextBox

    Protected WithEvents txtgol As TextBox
    Protected WithEvents txtsatuan As TextBox
    Protected WithEvents txtDRate As TextBox
    Protected WithEvents ddlsubsubcat As DropDownList


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

    Dim strSelectedDendaCode As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim Prefix As String = "DDP"

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

    Sub Bind_SubSubKategori(ByVal sc As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_SUBSUBCATEGORY_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim slc As Integer = 0
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE IdSubCat='KBS' or IdSubCat='PBS'|Order by SubSubCatName"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBSUBCATEGORY_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("SubSubCatId") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("SubSubCatId"))
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("SubSubCatName") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("SubSubCatName"))
                If objEmpBlkDs.Tables(0).Rows(intCnt).Item("SubSubCatId") = Trim(sc) Then
                    slc = intCnt + 1
                End If
            Next
        End If

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("SubSubCatId") = ""
        dr("SubSubCatName") = "Pilih sub kategori"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlsubsubcat.DataSource = objEmpBlkDs.Tables(0)
        ddlsubsubcat.DataTextField = "SubSubCatName"
        ddlsubsubcat.DataValueField = "SubSubCatId"
        ddlsubsubcat.DataBind()
        ddlsubsubcat.SelectedIndex = slc
    End Sub

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
            lblDndCode.Visible = False
            lblDesc.Visible = False
          
            strSelectedDendaCode = Trim(IIf(Request.QueryString("DendaCode") <> "", Request.QueryString("DendaCode"), Request.Form("DendaCode")))

            If Not IsPostBack Then
                If strSelectedDendaCode <> "" Then
                    DendaCode.Value = strSelectedDendaCode
                    isNew.value = "False"
                    onLoad_Display()
                Else
                    isNew.Value = "True"
                    Bind_SubSubKategori("")
                    onLoad_BindButton()
                End If
            End If
        End If

    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_DENDA_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String

        strSearch = "AND A.DendaCode like '" & Trim(DendaCode.Value) & "%'"
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
            txtDendaCd.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("DendaCode"))
            Bind_SubSubKategori(Trim(objDeptDs.Tables(0).Rows(0).Item("idsubsubcat")))
            txtgol.text = (Trim(objDeptDs.Tables(0).Rows(0).Item("Gol")))
            txtDesc.SelectedItem.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Description"))
            txtpstart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("periodestart"))
            txtpend.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("periodeend"))
            txtsatuan.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Satuan"))
            txtDRate.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("DendaRate"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindButton()
        End If

    End Sub

    Sub onLoad_BindButton()

        txtgol.Enabled = False
        txtDesc.Enabled = False
        txtsatuan.Enabled = False
        txtDRate.Enabled = False
        SaveBtn.Visible = False
        UnDelBtn.Visible = False
        DelBtn.Visible = False

        Select Case Trim(intStatus)
            Case "1"
                txtgol.Enabled = True
                txtDesc.Enabled = True
                txtsatuan.Enabled = True
                txtDRate.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                UnDelBtn.Visible = True
            Case Else
                txtDendaCd.Text = getcodetmp()
                txtgol.Enabled = True
                txtDesc.Enabled = True
                txtsatuan.Enabled = True
                txtDRate.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub


    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_DENDA_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strstatus As String = ""

        If isNew.Value = "True" Then
            txtDendaCd.Text = getCode()
        Else
            txtDendaCd.Text = DendaCode.Value
        End If

        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If


        DendaCode.Value = Trim(txtDendaCd.Text)
        strSelectedDendaCode = Trim(DendaCode.Value)

        ParamNama = "DC|SSC|DESC|PS|PE|GOL|UM|DR|ST|CD|UD|UI"
        ParamValue = Trim(txtDendaCd.Text) & "|" & ddlsubsubcat.SelectedItem.Value.Trim() & "|" & txtDesc.SelectedItem.Text.Trim & "|" & _
                     txtpstart.Text.Trim & "|" & txtpend.Text.Trim & "|" & _
                     txtgol.Text & "|" & _
                     txtsatuan.Text & "|" & txtDRate.Text & "|" & strstatus & "|" & DateTime.Now & "|" & _
                     DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        isNew.Value = "False"


        If strSelectedDendaCode <> "" Then
            onLoad_Display()
        End If

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_DendaList_Estate.aspx")
    End Sub


End Class
