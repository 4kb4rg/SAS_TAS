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


Public Class PR_setup_PrmiLainDet_Estate : Inherits Page


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
	Protected WithEvents txtket As TextBox
	
	Protected WithEvents txtidln As TextBox
    Protected WithEvents txtpstart As TextBox
    Protected WithEvents txtpend As TextBox
	Protected WithEvents txtrate As TextBox
 
    Protected WithEvents isNew As HtmlInputHidden
    Protected WithEvents dgLineDet As DataGrid


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
    Dim Prefix As String = "PL"

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
	
	Function getCodeln() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrno As Integer

        Try
            intErrno = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix  & "||8", objNewID)
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
            strId = Trim(IIf(Request.QueryString("ID") <> "", Request.QueryString("ID"), Request.Form("ID")))

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
                End If
				txtidln.text = ""
                onLoad_Display()
                BindGrid()
            End If
        End If

    End Sub


    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_PREMI_LAIN_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String


        strSearch = "AND A.LocCode='" & strLocation & "' AND A.ID='" & strId & "'" 
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_MANDOR_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            txtid.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("ID"))
			txtket.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Description"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
        End If

    End Sub


    Sub Delete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_PREMI_LAIN_DEL"
		Dim intErrNo As Integer
		
        ParamNama = "ID|LOC|UI"
        ParamValue = txtid.Text.Trim & "|" & _
                     strLocation & "|" & _
                     strUserId					 


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_MANDOR_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
		BindGrid()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_PrmiLain_Estate.aspx")
    End Sub


    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
	    Dim strOpCd As String = "PR_PR_STP_PREMI_LAIN_UPD"
        Dim strOpCd_Upd As String = "PR_PR_STP_PREMI_LAIN_UPD_LINE"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim idln As String = ""
		Dim strstatus As String
        Dim objID As New Object()
		
		If txtket.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan isi Keterangan"
            Exit Sub
        End If
		
		if isNew.Value = "True" Then
			txtid.Text = getCode()
		End If 
		
        ParamNama = "ID|LOC|KET|ST|CD|UD|UI"
        ParamValue = txtid.Text.Trim & "|" & _
                     strLocation & "|" & _
                     txtket.Text.Trim & "|" & _
                     "1" & "|" & DateTime.Now & "|" & DateTime.Now & "|" & strUserId
        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_MANDOR_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

		
        If txtRate.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan  input rate"
            Exit Sub
        End If
		
		If txtpstart.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan input Periode Start"
            Exit Sub
        End If
		
		If txtpend.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan input Periode End"
            Exit Sub
        End If

        If txtidln.text = "" Then
            idln = getCodeln()
		else
		    idln = txtidln.text()
        End If

        ParamNama = "ID|IDLN|LOC|PS|PE|RATE|ST|CD|UD|UI"
        ParamValue = txtid.Text.Trim & "|" & _
                     idln & "|" & _
                     strlocation & "|" & _
                     txtpstart.Text.Trim() & "|" & _
                     txtpend.Text.Trim() & "|" & _
                     txtRate.Text & "|" & _
                     "1" & "|" & _
                     DateTime.Now & "|" & _
                     DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_LAIN_UPD_LINE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

		isNew.Value = "False"
		
        onLoad_Display()
        BindGrid()

        txtidln.Text = ""
        txtpstart.Text = ""
        txtpend.Text = ""
        txtRate.Text = "0"
    End Sub

    Sub BindGrid()
        Dim dsData As DataSet
        Dim intCnt As Integer
       

        dsData = LoadData()
        dgLineDet.DataSource = dsData.Tables(0)
        dgLineDet.DataBind()
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "PR_PR_STP_PREMI_LAIN_GET_LINE"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String

        strSearch = "AND ID='" & txtid.Text.Trim & "'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_LAIN_GET_LINE&errmesg=" & lblErrMessage.Text )
        End Try

        Return objDeptDs
    End Function

   

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgLineDet.EditItemIndex = -1
        BindGrid()

        txtidln.Text = ""
        txtpstart.Text = ""
        txtpend.Text = ""
        txtRate.Text = "0"
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_PREMI_LAIN_DEL_LINE"
        Dim lbl As Label
        Dim intErrNo As Integer
        Dim id As String = ""
        Dim objID As New Object()

        Dim hidjid As HiddenField = CType(E.Item.FindControl("hidjid"), HiddenField)
		Dim hidjidln As HiddenField = CType(E.Item.FindControl("hidjidln"), HiddenField)
		

        ParamNama = "ID|IDLN|LOC"
        ParamValue = hidjid.Value.Trim & "|" & _
                     hidjidln.Value.Trim & "|" & _
                     strlocation 
        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_KERAJINAN_DEL_LINE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        BindGrid()
    End Sub
	
	 Sub dgLineDet_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("lbDelete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        End If
    End Sub
	
	 Sub getitem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbl As Label
            Dim hid As HiddenField
            Dim objCodeDs As New Object()

            hid = dgLineDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidjidln")
			txtidln.Text = hid.Value.Trim()
            
			hid = dgLineDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidjpstart")
			txtpstart.Text = hid.value.trim()
			hid = dgLineDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidjpend")
			txtpend.Text = hid.value.trim()
			hid = dgLineDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidjrate")
			txtrate.Text = hid.value.trim() 
			
        End If
    End Sub
End Class
