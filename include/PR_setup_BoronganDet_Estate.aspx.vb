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


Public Class PR_setup_BoronganDet_Estate : Inherits Page

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents JobCode As HtmlInputHidden
    Protected WithEvents BlokCode As HtmlInputHidden
    Protected WithEvents isNew As HtmlInputHidden

    Protected WithEvents txtNo As TextBox
    Protected WithEvents ddlkat As DropDownList
    Protected WithEvents ddlsubkat As DropDownList
    Protected WithEvents ddlaktiviti As DropDownList
    Protected WithEvents ddltahun As DropDownList
    Protected WithEvents ddltransit As DropDownList
    Protected WithEvents ddlalokasi As DropDownList

    Protected WithEvents ddltype As DropDownList
    Protected WithEvents txtbasis As TextBox

    Protected WithEvents txtpstartold As HtmlInputHidden

    Protected WithEvents txtpstart As TextBox
    Protected WithEvents txtpend As TextBox
    Protected WithEvents txtrate As TextBox
    Protected WithEvents txtnorma As TextBox
    Protected WithEvents txtuom As TextBox



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

    Dim strSelectedJobCode As String = ""
    Dim strSelectedBlockCode As String = ""
	Dim strSelectedPStart As String = ""
	Dim strSelectedPEnd As String = ""
	Dim strSelectedPTemp As String = ""
	
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim Prefix As String
    Dim strAcceptFormat As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        Dim Prefix As String = "B0" & strLocation

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
        '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedJobCode = Trim(IIf(Request.QueryString("codejob") <> "", Request.QueryString("codejob"), ""))
            strSelectedBlockCode = Trim(IIf(Request.QueryString("codeblk") <> "", Request.QueryString("codeblk"), ""))
			strSelectedPStart = Trim(IIf(Request.QueryString("pstart") <> "", Request.QueryString("pstart"), ""))
			strSelectedPEnd = Trim(IIf(Request.QueryString("pend") <> "", Request.QueryString("pend"), ""))
			strSelectedPTemp = Trim(IIf(Request.QueryString("temp") <> "", Request.QueryString("temp"), ""))

            If Not IsPostBack Then
                If (strSelectedJobCode <> "" And strSelectedBlockCode <> "") Then
                    isNew.Value = "False"
                    JobCode.Value = strSelectedJobCode.Trim
                    BlokCode.Value = strSelectedBlockCode.Trim
					
                    onLoad_Display()
                Else
                    intStatus = 0
                    isNew.Value = "True"
                    onLoad_BindKategori("")
                    onLoad_BindTahunTanam("")
                    onLoad_BindCOA("", "")
                    onLoad_BindButton()
					BindEmpType()
                End If
            End If
        End If
    End Sub

	 Sub BindEmpType()
        Dim strOpCd_EmpType As String = "HR_HR_STP_EMPTYPE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim objEmpTypeDs As New Object()
        Dim dr As DataRow

        strParamName = "SEARCH|SORT"
        strParamValue = "|"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpType, strParamName, strParamValue, objEmpTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_EMPTYPE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objEmpTypeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpTypeDs.Tables(0).Rows.Count - 1
                objEmpTypeDs.Tables(0).Rows(intCnt).Item("EmpTyCode") = Trim(objEmpTypeDs.Tables(0).Rows(intCnt).Item("EmpTyCode"))
                objEmpTypeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpTypeDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        ddltype.DataSource = objEmpTypeDs.Tables(0)
        ddltype.DataTextField = "Description"
        ddltype.DataValueField = "EmpTyCode"
        ddltype.DataBind()
        ddltype.SelectedIndex = 0

    End Sub

	
    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_BK_JOB_BORONGAN_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim objDeptDs As New Object()


        strSearch = "AND A.CodeJob='" & JobCode.Value.Trim & "' AND A.CodeBlk='" & BlokCode.Value.Trim & "' AND A.LocCode ='" & strLocation & "' " & _
		            "AND PeriodeStart='" & strSelectedPStart & "' AND PeriodeEnd='" & strSelectedPEnd & "' AND TypeEmp='" & strSelectedPTemp & "' "
					
        sortitem = ""
        ParamNama = "SEARCH|SORT|Loc"
        ParamValue = strSearch & "|" & sortitem & "|" & strLocation

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_BORONGAN_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

		BindEmpType()

        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            isNew.Value = "False"
            txtpstartold.Value = Trim(objDeptDs.Tables(0).Rows(0).Item("periodestart"))
            txtpstart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("periodestart"))
            txtpend.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("periodeend"))
            txtrate.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Rate"))
            txtuom.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UOM"))
            txtnorma.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Norma"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindKategori(Trim(objDeptDs.Tables(0).Rows(0).Item("CatId")))
            onLoad_BindSubKategori(Trim(objDeptDs.Tables(0).Rows(0).Item("CatId")), Trim(objDeptDs.Tables(0).Rows(0).Item("SubCatID")))
            onLoad_BindJob(Trim(objDeptDs.Tables(0).Rows(0).Item("CatId")), Trim(objDeptDs.Tables(0).Rows(0).Item("SubCatID")), Trim(objDeptDs.Tables(0).Rows(0).Item("CodeJob")))
            onLoad_BindTahunTanam(Trim(objDeptDs.Tables(0).Rows(0).Item("CodeBlk")))
            onLoad_BindCOA(Trim(objDeptDs.Tables(0).Rows(0).Item("Transit")), Trim(objDeptDs.Tables(0).Rows(0).Item("AlokasiGajiUpah")))
            ddltype.SelectedValue = Trim(objDeptDs.Tables(0).Rows(0).Item("TypeEmp"))
            txtbasis.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("BasisSKU"))
            onLoad_BindButton()
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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

    Sub onLoad_BindKategori(ByVal pv_kat As String)
        Dim strOpCd As String = "PR_PR_STP_BK_CATEGORY_GET"
        Dim objCodeDs As New Object()
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim sortitem As String

        strSearch = ""
        sortitem = "Order by CatId"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_CATEGORY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objCodeDs.Tables(0).Rows.Count - 1
            If Trim(objCodeDs.Tables(0).Rows(intCnt).Item("CatId")) = Trim(pv_kat) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objCodeDs.Tables(0).NewRow()
        dr("CatId") = ""
        dr("CatName") = "Select Kategori"
        objCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlkat.DataSource = objCodeDs.Tables(0)
        ddlkat.DataValueField = "CatId"
        ddlkat.DataTextField = "CatName"
        ddlkat.DataBind()
        ddlkat.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindSubKategori(ByVal pv_kat As String, ByVal pv_subkat As String)
        Dim strOpCd As String = "PR_PR_STP_BK_SUBCATEGORY_GET"
        Dim objCodeDs As New Object()
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim sortitem As String

        strSearch = "WHERE IdCat Like '%" & pv_kat & "%' "
        sortitem = "Order by IdCat,SubCatID"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBCATEGORY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objCodeDs.Tables(0).Rows.Count - 1
            If Trim(objCodeDs.Tables(0).Rows(intCnt).Item("SubCatID")) = Trim(pv_subkat) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objCodeDs.Tables(0).NewRow()
        dr("SubCatID") = ""
        dr("SubCatName") = "Select Sub Kategori"
        objCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlsubkat.DataSource = objCodeDs.Tables(0)
        ddlsubkat.DataValueField = "SubCatID"
        ddlsubkat.DataTextField = "SubCatName"
        ddlsubkat.DataBind()
        ddlsubkat.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindJob(ByVal pv_kat As String, ByVal pv_subkat As String, ByVal pv_job As String)
        Dim strOpCd As String = "PR_PR_STP_BK_JOB_GET"
        Dim objCodeDs As New Object()
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim sortitem As String

        strSearch = " AND A.CatID Like '%" & pv_kat & "%' AND A.SubCatID Like '%" & pv_subkat & "%' AND A.LocCode='" & strLocation & "' AND A.Status='1' "
        sortitem = "Order by A.CatID,A.SubCatID"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objCodeDs.Tables(0).Rows.Count - 1
            If Trim(objCodeDs.Tables(0).Rows(intCnt).Item("JobCode")) = Trim(pv_job) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objCodeDs.Tables(0).NewRow()
        dr("JobCode") = ""
        dr("Description") = "Select Aktiviti"
        objCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlaktiviti.DataSource = objCodeDs.Tables(0)
        ddlaktiviti.DataValueField = "JobCode"
        ddlaktiviti.DataTextField = "Description"
        ddlaktiviti.DataBind()
        ddlaktiviti.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindTahunTanam(ByVal pv_blk As String)
        Dim strOpCd As String = "PR_PR_STP_YEARPLAN_GET"
        Dim objCodeDs As New Object()
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim sortitem As String

        strSearch = "AND A.LocCode='" & strLocation & "' AND A.Status='1' "
        sortitem = "Order by BlkCode"
        ParamNama = "STRSEARCH|SORTEXP"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBCATEGORY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objCodeDs.Tables(0).Rows.Count - 1
            If Trim(objCodeDs.Tables(0).Rows(intCnt).Item("BlkCode")) = Trim(pv_blk) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objCodeDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Select Tahun Tanam"
        objCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddltahun.DataSource = objCodeDs.Tables(0)
        ddltahun.DataValueField = "BlkCode"
        ddltahun.DataTextField = "Description"
        ddltahun.DataBind()
        ddltahun.SelectedIndex = intSelectedIndex
    End Sub

    Sub onLoad_BindCOA(ByVal p_transit As String, ByVal p_upah As String)
        Dim strOpCd As String = "PR_PR_STP_BK_JOB_ALOKASI_COA_GET"
        Dim objCodeDs As New Object()
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strSearch As String
        Dim sortitem As String

        Dim inttransit As Integer = 0
        Dim intupah As Integer = 0
        

        strSearch = " AND LocCode='" & strLocation & "' AND Status='1' "
        sortitem = "Order by AccCode"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_ALOKASI_COA_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objCodeDs.Tables(0).Rows.Count - 1
            If Trim(objCodeDs.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(p_transit) Then
                inttransit = intCnt + 1
            End If

            If Trim(objCodeDs.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(p_upah) Then
                intupah = intCnt + 1
            End If

            objCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objCodeDs.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(objCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

        Next

        dr = objCodeDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Select COA"
        objCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddltransit.DataSource = objCodeDs.Tables(0)
        ddltransit.DataValueField = "AccCode"
        ddltransit.DataTextField = "Description"
        ddltransit.DataBind()
        ddltransit.SelectedIndex = inttransit

        ddlalokasi.DataSource = objCodeDs.Tables(0)
        ddlalokasi.DataValueField = "AccCode"
        ddlalokasi.DataTextField = "Description"
        ddlalokasi.DataBind()
        ddlalokasi.SelectedIndex = intupah

    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_BK_JOB_BORONGAN_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strstatus As String = ""
        Dim objID As New Object()

        If ddlkat.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select kategori !"
            Exit Sub
        End If

        If ddlsubkat.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select sub kategori !"
            Exit Sub
        End If

        If ddlaktiviti.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select aktiviti !"
            Exit Sub
        End If

        If ddltahun.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select tahun tanam !"
            Exit Sub
        End If

        If txtrate.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input rate !"
            Exit Sub
        End If


        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If

        If isNew.Value = "True" Then
            txtpstartold.Value = "-"
        End If

        ParamNama = "JC|CB|OPS|PS|PE|RT|UOM|NOR|TRAN|ALOK|LOC|ST|CD|UD|UI|TY|BS"
        ParamValue = ddlaktiviti.SelectedItem.Value.Trim & "|" & _
                     ddltahun.SelectedItem.Value.Trim & "|" & _
                     txtpstartold.Value.Trim & "|" & _
                     txtpstart.Text.Trim & "|" & _
                     txtpend.Text.Trim & "|" & _
                     txtrate.Text.Trim & "|" & _
                     "" & "|" & _
					 "" & "|" & _
                     "" & "|" & _
                     "" & "|" & _
                     strLocation & "|" & _
                     strstatus & "|" & _
                     DateTime.Now & "|" & DateTime.Now & "|" & strUserId & "|" & _
					 ddltype.SelectedItem.Value.Trim  & "|" & _ 
					 iif(txtbasis.Text.Trim="","0",txtbasis.Text.Trim)

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_BORONGAN_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        isNew.Value = "False"
        JobCode.Value = ddlaktiviti.SelectedItem.Value.Trim
        BlokCode.Value = ddltahun.SelectedItem.Value.Trim
		strSelectedPStart = txtpstart.Text.Trim
		strSelectedPEnd = txtpend.Text.Trim
		strSelectedPTemp = ddltype.SelectedItem.Value.Trim 

		
        onLoad_Display()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_Borongan_Estate.aspx")
    End Sub

    Sub ddlkat_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindSubKategori(ddlkat.SelectedItem.Value.Trim(), "")
    End Sub

    Sub ddlsubkat_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindJob(ddlkat.SelectedItem.Value.Trim(), ddlsubkat.SelectedItem.Value.Trim(), "")
    End Sub

End Class
