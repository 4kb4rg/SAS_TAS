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


Public Class PR_setup_AktivitiDet_Estate : Inherits Page

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
	Protected WithEvents PStart As HtmlInputHidden
	Protected WithEvents PEnd As HtmlInputHidden

    Protected WithEvents txtid As TextBox
    Protected WithEvents ddl_kat As DropDownList
    Protected WithEvents ddl_subkat As DropDownList
	Protected WithEvents txtsubsubcat As TextBox
    
    Protected WithEvents txtdesc As TextBox
    Protected WithEvents txtuom As TextBox
    Protected WithEvents txtnorma As TextBox
	
	Protected WithEvents txtperiodestart As TextBox
	Protected WithEvents txtperiodeend As TextBox

    Protected WithEvents tblSelection As HtmlTable

    Protected WithEvents dgLineDet As DataGrid

    Protected WithEvents ddl_divisi As DropDownList
    Protected WithEvents ddl_subblok As DropDownList

    Protected WithEvents ddl_coa_upah As DropDownList

    Protected WithEvents txtAccTransit As TextBox
    Protected WithEvents txtAccPremi As TextBox
    Protected WithEvents txtAccLembur As TextBox
    Protected WithEvents txtAccAstek As TextBox
    Protected WithEvents txtAccBahan As TextBox
    Protected WithEvents txtAccJHT As TextBox
    Protected WithEvents txtAccObat As TextBox
    Protected WithEvents txtAccTHR As TextBox
    Protected WithEvents txtAccBeras As TextBox
	Protected WithEvents txtAccPemborong As TextBox
	Protected WithEvents txtAccKendaraan As TextBox

    Protected WithEvents txtAccName_Transit As TextBox
    Protected WithEvents txtAccName_Premi As TextBox
    Protected WithEvents txtAccName_Lembur As TextBox
    Protected WithEvents txtAccName_Astek As TextBox
    Protected WithEvents txtAccName_Bahan As TextBox
    Protected WithEvents txtAccName_JHT As TextBox
    Protected WithEvents txtAccName_Obat As TextBox
    Protected WithEvents txtAccName_Bonus As TextBox
    Protected WithEvents txtAccName_Beras As TextBox
    Protected WithEvents txtAccName_Pemborong As TextBox
	Protected WithEvents txtAccName_Kendaraan As TextBox


    Protected WithEvents txtyr As TextBox

    Protected WithEvents rbtahuntanam As RadioButtonList
    Protected WithEvents rbtype As RadioButtonList
    Protected WithEvents rbdistribusi As RadioButtonList

    Protected WithEvents ddlInitPremi As DropDownList

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intLevel As Integer
	Dim intPRAR As Long

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

        Prefix = "JOB" & strLocation

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||10", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Function getCode_Alok() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrNo As Integer

        Prefix = "AA" & strLocation

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||10", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function


    Function getcodetmp() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETIDTMP"
        Dim objNewID As New Object
        Dim intErrNo As Integer

        Prefix = "JOB" & strLocation

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||10", objNewID)
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
         intPRAR = Session("SS_PRAR")
		intLevel = Session("SS_USRLEVEL")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = False) and (intLevel < 2) Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedBlockCode = Trim(IIf(Request.QueryString("JobCode") <> "", Request.QueryString("JobCode"), Request.Form("JobCode")))
            If Not IsPostBack Then
                If strSelectedBlockCode <> "" Then
                    BlokCode.Value = strSelectedBlockCode
                    isNew.Value = "False"
                    onLoad_Display()
                    onLoad_BindCOA(ddl_kat.SelectedItem.Value.Trim(), ddl_subkat.SelectedItem.Value.Trim(), "")
                Else
                    intStatus = 0
                    isNew.Value = "True"
                    txtid.Text = getcodetmp()
                    onLoad_BindKategori("")
                    onLoad_BindButton()
                    BindInitPremi("")
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_BK_JOB_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim objDeptDs As New Object()


        strSearch = "AND A.LocCode like '%" & strLocation & "%' AND A.Status='1' AND A.JobCode='" & strSelectedBlockCode & "' "
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            isNew.Value = "False"
            txtid.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("JobCode"))
            txtdesc.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Description"))
            txtuom.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("uom"))
            txtnorma.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Norma1"))
            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindKategori(Trim(objDeptDs.Tables(0).Rows(0).Item("CatId")))
            onLoad_BindSubKategori(Trim(objDeptDs.Tables(0).Rows(0).Item("CatId")), Trim(objDeptDs.Tables(0).Rows(0).Item("SubCatID")))
            txtsubsubcat.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("subsubcatid"))
			
            BindDivisi()
            Load_DataAlokasi()
            onLoad_BindButton()
            BindInitPremi(Trim(objDeptDs.Tables(0).Rows(0).Item("InitPremi")))
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

        If isNew.Value = "False" Then
            tblSelection.Visible = True
        Else
            tblSelection.Visible = False
        End If
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

        ddl_kat.DataSource = objCodeDs.Tables(0)
        ddl_kat.DataValueField = "CatId"
        ddl_kat.DataTextField = "CatName"
        ddl_kat.DataBind()
        ddl_kat.SelectedIndex = intSelectedIndex
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

        ddl_subkat.DataSource = objCodeDs.Tables(0)
        ddl_subkat.DataValueField = "SubCatID"
        ddl_subkat.DataTextField = "SubCatName"
        ddl_subkat.DataBind()
        ddl_subkat.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindDivisi()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim StrFilter As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"


        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_DIVISI_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "Pilih Divisi"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddl_divisi.DataSource = objEmpDivDs.Tables(0)
        ddl_divisi.DataTextField = "Description"
        ddl_divisi.DataValueField = "BlkGrpCode"
        ddl_divisi.DataBind()
        ddl_divisi.SelectedIndex = 0

    End Sub

    Sub onLoad_BindTahunTanam(ByVal pv_blk As String)
        Dim strOpCd As String = "PR_PR_STP_YEARPLAN_GET"
        Dim objCodeDs As New Object()
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strSearch As String
        Dim sortitem As String

        strSearch = " AND A.BlkGrpCode='" & pv_blk & "' AND A.LocCode='" & strLocation & "' AND A.Status='1' "
        sortitem = "Order by BlkCode"
        ParamNama = "STRSEARCH|SORTEXP"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBCATEGORY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objCodeDs.Tables(0).Rows.Count - 1
                objCodeDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objCodeDs.Tables(0).Rows(intCnt).Item("BlkCode"))
                objCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objCodeDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objCodeDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Select Tahun Tanam"
        objCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddl_subblok.DataSource = objCodeDs.Tables(0)
        ddl_subblok.DataValueField = "BlkCode"
        ddl_subblok.DataTextField = "Description"
        ddl_subblok.DataBind()
    End Sub

    Function GetCOA(ByVal pKode As String) As String
        Dim strOpCd As String = "PR_PR_STP_BK_JOB_ALOKASI_COA_GET"

        Dim objCodeDs As New Object()

        Dim intErrNo As Integer
        Dim strSearch As String = ""
        Dim strsearch_tst As String = ""
        Dim ParamValue_tst As String
        Dim sortitem As String

        Dim nNamaCoa As String = ""

        sortitem = "Order by AccCode"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem
        ParamValue_tst = strsearch_tst & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_ALOKASI_COA_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objCodeDs.Tables(0).Rows.Count > 0 Then
            nNamaCoa = Trim(objCodeDs.Tables(0).Rows(0).Item("Description"))
        End If

        Return nNamaCoa
    End Function

    Sub onLoad_BindCOA(ByVal cat As String, ByVal subcat As String, ByVal ttnm As String)
        Dim strOpCd As String = "PR_PR_STP_BK_JOB_ALOKASI_COA_GET"

        Dim objCodeDs As New Object()
        Dim objCodeDs_tst As New Object()

        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strSearch As String = ""
        Dim strsearch_tst As String = ""
        Dim ParamValue_tst As String
        Dim tnm As String
        Dim sortitem As String

        If ttnm.Length > 5 Then
            tnm = Left(ttnm.Trim, 2) & Right(ttnm.Trim, 2)
        Else
            tnm = ""
        End If

        'Select Case cat
        '    Case "PN"
        '        strSearch = " AND (AccCode like '781.%' or AccCode like '782.%' or AccCode like '783.%') AND LocCode='" & strLocation & "' AND Status='1' "
        '    Case "CV", "UM"
        '        strSearch = " AND (AccCode like '71.%'  or AccCode like '7P.%'  or AccCode like '73.%' or AccCode like '780.%' or AccCode like '124.%') AND LocCode='" & strLocation & "' AND Status='1' "
        '    Case "RW"
        '        Select Case subcat
        '            Case "TMS"
        '                strSearch = " AND (AccCode Like '780.A%' or AccCode like '780.%' or AccCode like '781.%') AND LocCode='" & strLocation & "' AND Status='1' "
        '            Case "TMK"
        '                strSearch = " AND (AccCode Like '783.%'  or AccCode like '782.%'  or AccCode like '781.%.103%') AND LocCode='" & strLocation & "' AND Status='1' "
         '           Case "TBS", "TBJ", "TBK" 
        '                strSearch = " AND (AccCode like '19.%'  or AccCode like '781.%.103%' or AccCode like '16.%'  or AccCode like '20.%') AND LocCode='" & strLocation & "' AND Status='1' "
        '            Case "BSM", "BSP", "BJM", "BKM"
        '                strSearch = " AND (AccCode like '15.%'  or AccCode like '781.%.103%' or AccCode like '111.1%' ) AND LocCode='" & strLocation & "' AND Status='1' "
        '            Case "LCJ", "LCK", "LCS"
        '                strSearch = " AND (AccCode like '18.%' or AccCode like '19.%'  or AccCode like '781.%.103%') AND LocCode='" & strLocation & "' AND Status='1' "
	'				Case "MRU"
        '                strSearch = " AND LocCode='" & strLocation & "' AND Status='1' "	
        '        End Select
	'		Case "KM"
    '            strSearch = " AND (AccCode like '42.%' or AccCode like '71.%' or AccCode like '7P.%') AND LocCode='" & strLocation & "' AND Status='1' "
      	
    '        Case Else
                strSearch = ""
    '    End Select

       'strsearch_tst = " AND (AccCode like '40%' or AccCode like '45%') AND LocCode='" & strLocation & "' AND Status='1' "
		'strsearch_tst = " AND (AccCode like '4%') AND LocCode='" & strLocation & "' AND Status='1' "
		strsearch_tst = ""

        sortitem = "Order by AccCode"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem
        ParamValue_tst = strsearch_tst & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_ALOKASI_COA_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        'Try
        '    intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue_tst, objCodeDs_tst)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_ALOKASI_COA_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        'End Try

        For intCnt = 0 To objCodeDs.Tables(0).Rows.Count - 1
            objCodeDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objCodeDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objCodeDs.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(objCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next

        'For intCnt = 0 To objCodeDs_tst.Tables(0).Rows.Count - 1
        '    objCodeDs_tst.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objCodeDs_tst.Tables(0).Rows(intCnt).Item("AccCode"))
        '    objCodeDs_tst.Tables(0).Rows(intCnt).Item("Description") = Trim(objCodeDs_tst.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(objCodeDs_tst.Tables(0).Rows(intCnt).Item("Description")) & ")"
        'Next

        dr = objCodeDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Select COA"
        objCodeDs.Tables(0).Rows.InsertAt(dr, 0)


        'dr = objCodeDs_tst.Tables(0).NewRow()
        'dr("AccCode") = ""
        'dr("Description") = "Select COA"
        'objCodeDs_tst.Tables(0).Rows.InsertAt(dr, 0)


        'ddl_coa_transit.DataSource = objCodeDs_tst.Tables(0)
        'ddl_coa_transit.DataValueField = "AccCode"
        'ddl_coa_transit.DataTextField = "Description"
        'ddl_coa_transit.DataBind()
        'ddl_coa_transit.SelectedIndex = 0

        ddl_coa_upah.DataSource = objCodeDs.Tables(0)
        ddl_coa_upah.DataValueField = "AccCode"
        ddl_coa_upah.DataTextField = "Description"
        ddl_coa_upah.DataBind()
        ddl_coa_upah.SelectedIndex = 0

        '      ddl_coa_premi.DataSource = objCodeDs.Tables(0)
        '      ddl_coa_premi.DataValueField = "AccCode"
        '      ddl_coa_premi.DataTextField = "Description"
        '      ddl_coa_premi.DataBind()
        '      ddl_coa_premi.SelectedIndex = 0

        '      ddl_coa_lembur.DataSource = objCodeDs.Tables(0)
        '      ddl_coa_lembur.DataValueField = "AccCode"
        '      ddl_coa_lembur.DataTextField = "Description"
        '      ddl_coa_lembur.DataBind()
        '      ddl_coa_lembur.SelectedIndex = 0

        '      ddl_coa_astek.DataSource = objCodeDs.Tables(0)
        '      ddl_coa_astek.DataValueField = "AccCode"
        '      ddl_coa_astek.DataTextField = "Description"
        '      ddl_coa_astek.DataBind()
        '      ddl_coa_astek.SelectedIndex = 0

        '      ddl_coa_jht.DataSource = objCodeDs.Tables(0)
        '      ddl_coa_jht.DataValueField = "AccCode"
        '      ddl_coa_jht.DataTextField = "Description"
        '      ddl_coa_jht.DataBind()
        '      ddl_coa_jht.SelectedIndex = 0

        '      ddl_coa_beras.DataSource = objCodeDs.Tables(0)
        '      ddl_coa_beras.DataValueField = "AccCode"
        '      ddl_coa_beras.DataTextField = "Description"
        '      ddl_coa_beras.DataBind()
        '      ddl_coa_beras.SelectedIndex = 0

        '      ddl_coa_obat.DataSource = objCodeDs.Tables(0)
        '      ddl_coa_obat.DataValueField = "AccCode"
        '      ddl_coa_obat.DataTextField = "Description"
        '      ddl_coa_obat.DataBind()
        '      ddl_coa_obat.SelectedIndex = 0

        '      ddl_coa_thr.DataSource = objCodeDs.Tables(0)
        '      ddl_coa_thr.DataValueField = "AccCode"
        '      ddl_coa_thr.DataTextField = "Description"
        '      ddl_coa_thr.DataBind()
        '      ddl_coa_thr.SelectedIndex = 0

        'ddl_coa_bahan.DataSource = objCodeDs.Tables(0)
        '      ddl_coa_bahan.DataValueField = "AccCode"
        '      ddl_coa_bahan.DataTextField = "Description"
        '      ddl_coa_bahan.DataBind()
        '      ddl_coa_bahan.SelectedIndex = 0

    End Sub

    Function onLoad_BindALokasi(ByVal p_job As String, ByVal p_dv As String, ByVal p_tt As String) As DataSet
        Dim strOpCd As String = "PR_PR_STP_BK_JOB_ALOKASI_GET"
        Dim objCodeDs As New Object()
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String

        strSearch = " AND a.CodeJob='" & p_job & "' AND CodeBlkGrp Like '" & p_dv & "%' AND CodeBlk Like '" & p_tt & "%' AND a.Status='1' AND a.LocCode='" & strlocation & "' " 
      
        sortitem = "Order by right(rtrim(periodestart),4)+left(rtrim(periodestart),2) Desc,CodeBlk Asc"
        ParamNama = "LOC|SEARCH|SORT"
        ParamValue = IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "'") & "|" & strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_ALOKASI_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objCodeDs

    End Function

    Sub Load_DataAlokasi()
        dgLineDet.DataSource = onLoad_BindALokasi(txtid.Text.Trim(), "", "")
        dgLineDet.DataBind()
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_BK_JOB_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim typlant As String = ""
        Dim strstatus As String = ""
        Dim objID As New Object()

        If ddl_kat.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select kategori !"
            Exit Sub
        End If

        If ddl_subkat.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select sub kategori !"
            Exit Sub
        End If

        If txtuom.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input uom !"
            Exit Sub
        End If

        If txtdesc.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input deskripsi!"
            Exit Sub
        End If

        If isNew.Value = "True" And left(txtid.Text.trim(), len("JOB" & strLocation)) = "JOB" & strLocation Then
            txtid.Text = getCode()
        End If

        BlokCode.Value = Trim(txtid.Text)
        strSelectedBlockCode = Trim(BlokCode.Value)

		
        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then
            strstatus = "1"
        ElseIf strCmdArgs = "Del" Then
            strstatus = "2"
        End If

		if (strstatus = "2") And (ddl_subkat.SelectedItem.Value.Trim = "ADU") AND ((txtsubsubcat.Text.Trim = "XXX") or  (txtsubsubcat.Text.Trim = "XXY") or (txtsubsubcat.Text.Trim = "XXZ")) Then
			lblErrMessage.Visible = True
            lblErrMessage.Text = "Maaf Aktiviti ini tidak bisa dihapus!"
            Exit Sub
        End If

        ParamNama = "JC|CI|SCI|DESC|UM|NORM|LOC|ST|CD|UD|UI|SSC|IP"
        ParamValue = strSelectedBlockCode & "|" & _
                     ddl_kat.SelectedItem.Value.Trim & "|" & _
                     ddl_subkat.SelectedItem.Value.Trim & "|" & _
                     txtdesc.Text.ToUpper.Trim & "|" & _
                     txtuom.Text.Trim.Trim & "|" & _
                     txtnorma.Text.Trim & "|" & _
                     strLocation & "|" & _
                     strstatus & "|" & _
                     DateTime.Now & "|" & _
                     DateTime.Now & "|" & _
                     strUserId & "|" & _
                     txtsubsubcat.Text.Trim & "|" & _
                     ddlInitPremi.SelectedItem.Value.Trim 
   				    

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        isNew.Value = "False"
        onLoad_Display()
		onLoad_BindCOA(ddl_kat.SelectedItem.Value.Trim(), ddl_subkat.SelectedItem.Value.Trim(), "")
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_AktivitiList_Estate.aspx")
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_BK_JOB_ALOKASI_ADD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim id As String = ""
        Dim objID As New Object()

        If ddl_divisi.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select Divisi !"
            Exit Sub
        End If

        If ddl_subblok.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select Tahun Tanam Code !"
            Exit Sub
        End If

        If Trim(txtAccTransit.Text) = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select coa transit !"
            Exit Sub
        End If

        If ddl_coa_upah.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Please select coa upah !"
            Exit Sub
        End If
		
		if (txtperiodestart.text.trim = "") or (txtperiodeend.text.trim ="") then
			lblErrMessage.Visible = True
            lblErrMessage.Text = "Please input periode coa format (MMYYYY)"
            Exit Sub
        End If

		BlokCode.Value = Trim(txtid.Text)
        strSelectedBlockCode = Trim(BlokCode.Value)

        ParamNama = "JC|GB|CB|LOC|KU|BYP|YP|COA_TR|COA_GU|COA_PR|COA_LB|COA_AK|COA_JH|COA_OB|COA_BR|COA_TB|DS|CD|UD|UI|PS|PE|PSOLD|PEOLD|BHN|BOR|COA_VEH"
        ParamValue = strSelectedBlockCode & "|" & _
                     ddl_divisi.SelectedItem.Value.Trim & "|" & _
                     ddl_subblok.SelectedItem.Value.Trim & "|" & _
                     strLocation & "|" & _
                     rbtype.SelectedValue.Trim & "|" & _
                     rbtahuntanam.SelectedValue.Trim & "|" & _
                     txtyr.Text.Trim & "|" & _
                     txtAccTransit.Text.Trim & "|" & _
                     ddl_coa_upah.SelectedItem.Value.Trim & "|" & _
                     txtAccPremi.Text.Trim & "|" & _
                     txtAccLembur.Text.Trim & "|" & _
                     txtAccAstek.Text.Trim & "|" & _
                     txtAccJHT.Text.Trim & "|" & _
                     txtAccObat.Text.Trim & "|" & _
                     txtAccBeras.Text.Trim & "|" & _
                     txtAccTHR.Text.Trim & "|" & _
                     rbdistribusi.SelectedValue.Trim & "|" & _
                     DateTime.Now & "|" & _
                     DateTime.Now & "|" & _
      strUserId & "|" & _
      txtperiodestart.Text.Trim & "|" & _
      txtperiodeend.Text.Trim & "|" & _
      IIf(PStart.Value.Trim = "", txtperiodestart.Text.Trim, PStart.Value.Trim) & "|" & _
      IIf(PEnd.Value.Trim = "", txtperiodeend.Text.Trim, PEnd.Value.Trim) & "|" & _
      txtAccBahan.Text.Trim & "|" & _
                    txtAccPemborong.Text.Trim & "|" & _
                    txtAccKendaraan.Text.Trim

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_ALOKASI_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Load_DataAlokasi()
    End Sub

    Sub dgLineDet_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_jb As HiddenField = CType(E.Item.FindControl("hidjob"), HiddenField)
        Dim lbl_div As Label = CType(E.Item.FindControl("lbldivisi"), Label)
        Dim lbl_ttnm As Label = CType(E.Item.FindControl("lblttnm"), Label)

        Dim strOpCd_Upd As String = "PR_PR_STP_BK_JOB_ALOKASI_DEL"
        Dim intErrNo As Integer

        ParamNama = "UD|UI|JB|DV|TT|LOC"
        ParamValue = DateTime.Now & "|" & _
                     strUserId & "|" & _
                     hid_jb.Value.Trim & "|" & _
                     lbl_div.Text.Trim & "|" & _
                     lbl_ttnm.Text.Trim & "|" & _
                     strLocation


        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_ALOKASI_DEL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        Load_DataAlokasi()
    End Sub

    Sub getitem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbl As Label
            Dim hid As HiddenField
            Dim objCodeDs As New Object()

            hid = dgLineDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidjob")

            lbl = dgLineDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbldivisi")
            ddl_divisi.SelectedValue = lbl.Text.Trim
            onLoad_BindTahunTanam(ddl_divisi.SelectedItem.Value.Trim())
            lbl = dgLineDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblttnm")
            ddl_subblok.SelectedValue = lbl.Text.Trim
            onLoad_BindCOA(ddl_kat.SelectedItem.Value.Trim(), ddl_subkat.SelectedItem.Value.Trim(), ddl_subblok.SelectedItem.Value.Trim())

            objCodeDs = onLoad_BindALokasi(hid.Value.Trim, ddl_divisi.SelectedItem.Value.Trim, ddl_subblok.SelectedItem.Value.Trim())
            txtyr.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("YearPlan"))

            Try
                txtAccTransit.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("Transit"))
                txtAccName_Transit.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("Transit_Name"))
            Catch Exp As System.Exception
                txtAccTransit.Text = ""
            End Try
			
			try
                ddl_coa_upah.SelectedValue = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiGajiUpah"))

			Catch Exp As System.Exception
				ddl_coa_upah.SelectedValue = ""
			End Try
			
			try
                txtAccPremi.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiPremi"))
                txtAccName_Premi.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiPremi_Name"))
			Catch Exp As System.Exception
                txtAccPremi.Text = ""
                txtAccName_Premi.Text = ""
			End Try
			
            try
                txtAccLembur.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiLembur"))
                txtAccName_Lembur.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiLembur_Name"))
			Catch Exp As System.Exception
                txtAccLembur.Text = ""
                txtAccName_Lembur.Text = ""
			End Try
			
            try
                txtAccAstek.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiAstek"))
                txtAccName_Astek.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiAstek_Name"))
			Catch Exp As System.Exception
                txtAccAstek.Text = ""
                txtAccName_Astek.Text = ""
			End Try
			
			try
                txtAccJHT.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiJht"))
                txtAccName_JHT.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiJht_Name"))
			Catch Exp As System.Exception
                txtAccJHT.Text = ""
                txtAccName_JHT.Text = ""
			End Try
			
            try
                txtAccObat.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiObat"))
                txtAccName_Obat.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiObat_Name"))
			Catch Exp As System.Exception
                txtAccObat.Text = ""
                txtAccName_Obat.Text = ""
			End Try
			
            try
                txtAccTHR.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiThrBonus"))
                txtAccName_Bonus.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiThrBonus_Name"))
			Catch Exp As System.Exception
                txtAccTHR.Text = ""
                txtAccName_Bonus.Text = ""
			End Try
			
			try
                txtAccBeras.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiBeras"))
                txtAccName_Beras.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiBeras_Name"))
			Catch Exp As System.Exception
                txtAccBeras.Text = ""
                txtAccName_Beras.Text = ""
			End Try
			
            rbtahuntanam.SelectedValue = Trim(objCodeDs.Tables(0).Rows(0).Item("byYearPlan"))
            rbtype.SelectedValue = Trim(objCodeDs.Tables(0).Rows(0).Item("KerjaOrUpah"))
            rbdistribusi.SelectedValue = Trim(objCodeDs.Tables(0).Rows(0).Item("isDistribusi"))
			
			try
                txtAccBahan.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiBahan"))
                txtAccName_Bahan.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiBahan_Name"))
			Catch Exp As System.Exception
                txtAccBahan.Text = ""
                txtAccName_Bahan.Text = ""
			End Try

						try
                txtAccPemborong.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiPemborong"))
                txtAccName_Pemborong.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiPemborong_Name"))
			Catch Exp As System.Exception
                txtAccPemborong.Text = ""
                txtAccName_Pemborong.Text = ""
			End Try
			
			try
                txtAccKendaraan.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiKendaraan"))
                txtAccName_Kendaraan.Text = Trim(objCodeDs.Tables(0).Rows(0).Item("AlokasiKendaraan_Name"))
			Catch Exp As System.Exception
                txtAccPemborong.Text = ""
                txtAccName_Pemborong.Text = ""
			End Try
			
			hid = dgLineDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidpstart")
			txtperiodestart.Text = hid.value.trim()
			PStart.value = hid.value.trim() 
			hid = dgLineDet.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidpend")
			txtperiodeend.Text = hid.value.trim()
			PEnd.value = hid.value.trim() 
			
        End If
    End Sub

    Sub dgLineDet_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("Delete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        End If
    End Sub

    Sub ddl_kat_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindSubKategori(ddl_kat.SelectedItem.Value.Trim(), "")
    End Sub

    Sub ddl_subkat_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        txtsubsubcat.text = ddl_subkat.SelectedItem.Value.Trim()
    End Sub


    Sub ddl_divisi_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindTahunTanam(ddl_divisi.SelectedItem.Value.Trim())
    End Sub

    Sub ddl_subblok_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If rbtahuntanam.SelectedItem.Value.Trim = "Y" Or Left(ddl_subblok.SelectedItem.Value.Trim, 2) <> "OH" Then
            txtyr.Text = Right(ddl_subblok.SelectedItem.Value.Trim, 4)
        End If

        'onLoad_BindCOA(ddl_kat.SelectedItem.Value.Trim(), ddl_subkat.SelectedItem.Value.Trim(), ddl_subblok.SelectedItem.Value.Trim())
    End Sub
	
	Sub ddl_coa_upah_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
	   Dim i As Integer = ddl_coa_upah.SelectedIndex
	   

        'ddl_coa_premi.SelectedIndex = i
        'ddl_coa_lembur.SelectedIndex = i
        'ddl_coa_astek.SelectedIndex = i
        'ddl_coa_jht.SelectedIndex = i
        'ddl_coa_obat.SelectedIndex = i
        'ddl_coa_thr.SelectedIndex = i
        'ddl_coa_beras.SelectedIndex = i

        txtAccPremi.Text = ""
        txtAccName_Premi.Text = ""

        txtAccLembur.Text = ""
        txtAccName_Lembur.Text = ""

        txtAccAstek.Text = ""
        txtAccName_Astek.Text = ""

        txtAccJHT.Text = ""
        txtAccName_JHT.Text = ""

        txtAccObat.Text = ""
        txtAccName_Obat.Text = ""

        txtAccTHR.Text = ""
        txtAccName_Bonus.Text = ""

        txtAccBeras.Text = ""
        txtAccName_Beras.Text = ""

        txtAccPremi.Text = ddl_coa_upah.SelectedItem.Value
        txtAccName_Premi.Text = GetCOA(ddl_coa_upah.SelectedItem.Value)

        txtAccLembur.Text = ddl_coa_upah.SelectedItem.Value
        txtAccName_Lembur.Text = txtAccName_Premi.Text

        txtAccAstek.Text = ddl_coa_upah.SelectedItem.Value
        txtAccName_Astek.Text = txtAccName_Premi.Text

        txtAccJHT.Text = ddl_coa_upah.SelectedItem.Value
        txtAccName_JHT.Text = txtAccName_Premi.Text

        txtAccObat.Text = ddl_coa_upah.SelectedItem.Value
        txtAccName_Obat.Text = txtAccName_Premi.Text

        txtAccTHR.Text = ddl_coa_upah.SelectedItem.Value
        txtAccName_Bonus.Text = txtAccName_Premi.Text

        txtAccBeras.Text = ddl_coa_upah.SelectedItem.Value
        txtAccName_Beras.Text = txtAccName_Premi.Text

	End Sub

    Sub BindInitPremi(ByVal pv_initPremi As String)
        Dim strOpCd As String = "PR_PR_STP_PREMI_LAIN_GET"
        Dim objCodeDs As New Object()
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim sortitem As String

        ParamNama = "SEARCH|SORT"
        ParamValue = "AND A.LocCode='" & strlocation & "'" & IIf(pv_initPremi = "", "", "AND ID='" & Trim(pv_initPremi) & "' ") & "|"

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBCATEGORY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objCodeDs.Tables(0).Rows.Count - 1
            objCodeDs.Tables(0).Rows(intCnt).Item("ID") = objCodeDs.Tables(0).Rows(intCnt).Item("ID")
            objCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objCodeDs.Tables(0).Rows(intCnt).Item("Description")) & "( " & Trim(objCodeDs.Tables(0).Rows(intCnt).Item("ID")) & ")"
            If Trim(objCodeDs.Tables(0).Rows(intCnt).Item("ID")) = Trim(pv_initPremi) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objCodeDs.Tables(0).NewRow()
        dr("ID") = ""
        dr("Description") = "Select Premi"
        objCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlInitPremi.DataSource = objCodeDs.Tables(0)
        ddlInitPremi.DataValueField = "ID"
        ddlInitPremi.DataTextField = "Description"
        ddlInitPremi.DataBind()
        ddlInitPremi.SelectedIndex = intSelectedIndex
    End Sub
End Class
