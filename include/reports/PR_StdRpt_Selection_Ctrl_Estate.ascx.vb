
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic

Public Class PR_StdRpt_Selection_Ctrl : Inherits UserControl

    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objPR As New agri.PR.clsReport()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()

    'lsy -> 160706
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objDataSet As New Object()
    'end lsy

    Protected WithEvents lblLocation As Label
    Protected WithEvents lblUserLoc As Label

    Protected WithEvents lstRptname As DropDownList
    Protected WithEvents lstDecimal As DropDownList
    Protected WithEvents cbLocAll As CheckBox
    Protected WithEvents cblUserLoc As CheckBoxList

    Protected WithEvents hidUserLoc As HtmlInputHidden

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserLoc As String

    'lsy -> 100706
    Dim lblErrMessage As Label

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim dr As DataRow
    Dim intSelIndex As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntDec As Integer

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                GetUserLoc()        '--bind all locations authorised by user
                BindReportNameList() '--bind all module reports
                intSelIndex = Request.QueryString("SelIndex")
                lstRptname.SelectedIndex = intSelIndex

                lstDecimal.SelectedIndex = 2
                If Not Request.QueryString("Dec") = "" Then
                    For intCntDec = 0 To lstDecimal.Items.Count - 1
                        If lstDecimal.Items(intCntDec).Value = Request.QueryString("Dec") Then
                            lstDecimal.SelectedIndex = intCntDec
                        End If
                    Next
                End If
            Else
                lblUserLoc.Visible = False
            End If
        End If
    End Sub

    '-------Get User Location --------------
    Sub GetUserLoc()
        Dim strParam As String
        Dim objMapPath As String
        Dim strUserLoc As String
        Dim arrParam As Array
        Dim intCnt2 As Integer
        Dim intCnt3 As Integer
        Dim objUserLoc As New DataSet()
        Dim strOppCd_UserLoc_GET As String = "PR_STDRPT_USERLOCATION_GET"

        Try
            strParam = "AND USERLOC.UserID = '" & strUserId & "'"
            intErrNo = objAdmin.mtdGetUserLocation(strOppCd_UserLoc_GET, strParam, objUserLoc, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_PR_SELECTIONCTRL_USERLOCATION&errmesg=" & Exp.ToString() & "&redirect=../en/reports/PR_StdRpt_Selection.aspx")
        End Try

        lblLocation.Visible = True
        cblUserLoc.DataSource = objUserLoc.Tables(0)
        cblUserLoc.DataValueField = "LocCode"
        cblUserLoc.DataBind()

        objUserLoc = Nothing

        hidUserLoc.Value = Request.QueryString("UserLoc")
        strUserLoc = Request.QueryString("UserLoc")
        If Left(strUserLoc, 3) = "','" Then
            arrParam = Split(strUserLoc, "','")
        ElseIf Right(strUserLoc, 1) = "," Then
            arrParam = Split(strUserLoc, ",")
        Else
            arrParam = Split(strUserLoc, ",")
        End If

        If Not hidUserLoc.Value = "" Then
            For intCnt2 = 0 To cblUserLoc.Items.Count - 1
                For intCnt3 = 0 To arrParam.GetUpperBound(0)
                    If Trim(cblUserLoc.Items(intCnt2).Value) = Trim(arrParam(intCnt3)) Then
                        cblUserLoc.Items(intCnt2).Selected = True
                    End If
                Next intCnt3
            Next intCnt2
        End If
    End Sub

    Sub Check_Clicked(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intCntLocAll As Integer = 0

        If cbLocAll.Checked Then
            For intCntLocAll = 0 To cblUserLoc.Items.Count - 1
                cblUserLoc.Items(intCntLocAll).Selected = True
            Next
        Else
            For intCntLocAll = 0 To cblUserLoc.Items.Count - 1
                cblUserLoc.Items(intCntLocAll).Selected = False
            Next
        End If
        LocCheck()
    End Sub

    Sub LocCheckList(ByVal Sender As Object, ByVal E As EventArgs)
        LocCheck()
    End Sub

    '-------Check User Selection on Location--------------
    Sub LocCheck()
        Dim intCnt2 As Integer = 0
        Dim tempUserLoc As String
        Dim txt As HtmlInputHidden
        Dim strResult As String

        For intCnt2 = 0 To cblUserLoc.Items.Count - 1
            If cblUserLoc.Items(intCnt2).Selected Then
                If cblUserLoc.Items.Count = 1 Then
                    tempUserLoc = cblUserLoc.Items(intCnt2).Text
                Else
                    tempUserLoc = tempUserLoc & "','" & cblUserLoc.Items(intCnt2).Text
                End If
            End If
        Next intCnt2

        hidUserLoc.Value = tempUserLoc
    End Sub

    '-------Create Report Name Dropdownlist--------------
    Sub BindReportNameList()
        Dim strParam As String
        Dim objMapPath As String
        Dim dsForDropDown As New DataSet()
        Dim strOppCd_StdRptName_GET As String = "PR_STDRPT_NAME_GET"

        strParam = " WHERE ReportType = '4' AND Reportid IN (Select ReportID From SH_REPORT_USER Where UserId='" & strUserId & "') And  Status = '1' ORDER BY RptName"
        Try
            intErrNo = objAdmin.mtdGetStdRptName(strOppCd_StdRptName_GET, strParam, dsForDropDown, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_STDRPT_NAME_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dr = dsForDropDown.Tables(0).NewRow()
        dr("ReportID") = ""
        dr("RptName") = "Select Report Name"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        lstRptname.DataSource = dsForDropDown.Tables(0)
        lstRptname.DataValueField = "ReportID"
        lstRptname.DataTextField = "RptName"
        lstRptname.DataBind()
    End Sub

    Sub CheckRptName(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strRptname As String = Trim(lstRptname.SelectedItem.Value)
        Dim strSelectedIndex As String = LCase(lstRptname.SelectedItem.Value)
        Dim intSelectedIndex As Integer = lstRptname.SelectedIndex
        Dim strUserLoc As String
        Dim strDec As String

        strUserLoc = hidUserLoc.Value
        strDec = lstDecimal.SelectedItem.Value

        If strSelectedIndex = "rptpr1000055" Then 'Daftar Absensi
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DaftarAbsensi_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000056" Then 'Gajian Kecil
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_GajiKecil_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000057" Then 'Gajian Besar
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_GajiBesar_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000058" Then 'Slip Gaji
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_GajiBesar_Slip_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000059" Then 'Catu Beras
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_CatuBeras_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000060" Then 'buku mandor panen
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_bukumandor_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec & "&StrIndex=" & strSelectedIndex)
        ElseIf strSelectedIndex = "rptpr1000061" Then 'buku kcs panen
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_bukumandor_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec & "&StrIndex=" & strSelectedIndex)
        ElseIf strSelectedIndex = "rptpr1000062" Then 'bkm
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_bukumandor_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec & "&StrIndex=" & strSelectedIndex)
        ElseIf strSelectedIndex = "rptpr1000063" Then 'brondolan
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DaftarBrondolan_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000065" Then 'LHPT
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_LHPT_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000066" Then 'LHPB
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_LHPB_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000067" Then 'LPBKS
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_LPBKS_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000070" Then 'LHPK
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_LHPK_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000071" Then 'Gaji Kecil Slip
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_GajiKecil_Slip_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000072" Then 'Premi Potong Buah
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_LHPPB_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000073" Then 'Premi Potong Buah
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PremiPanen_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000074" Then 'muat tbs
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DaftarMuatTBS_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000075" Then 'premi deres
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PremiDeres_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000076" Then 'hutang gaji
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_GajiKecil_Hutang_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000077" Then 'premi muat
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PremiMuat_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000078" Then 'lhd bibitan
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_LHB_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000079" Then 'THR
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_THR_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000080" Then 'absensi per divisi
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DaftarAbsensi_Kerja_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000081" Then 'lhd panen
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_LHPT_Panen_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000082" Then 'muat pupuk
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DaftarMuatPupuk_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000083" Then 'premi traksi
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PremiTraksi_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000084" Then 'Astek & BPJS
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_Astek_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000085" Then 'premi lain
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PremiLain_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000086" Then 'Lembur
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_Lembur_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000088" Then 'Rekap Gaji
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_GajiBesar_Rekap_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000087" Then 'Rekap Gaji
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_Angsuran_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000088" Then 'Rekap Gaji
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_GajiBesar_Rekap_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000089" Then 'Premi mandor
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PremiPanen_Mdr_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000090" Then 'PDO
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_PDO_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
	    ElseIf strSelectedIndex = "rptpr1000091" Then 'SPK Harian
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DaftarSPK_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000092" Then 'SPK Rekap
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_RekapSPK_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
        ElseIf strSelectedIndex = "rptpr1000093" Then 'Tenaga Borongan
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_Tenaga_Borongan_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000094" Then 'Tenaga panen borongan
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DaftarPanenBorongan_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)			
		ElseIf strSelectedIndex = "rptpr1000095" Then 'Rapel
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_Rapel_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)			
        ElseIf strSelectedIndex = "rptpr1000096" Then 'Tenaga panen JJG
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_DaftarPanenJJG_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)			
		ElseIf strSelectedIndex = "rptpr1000097" Then 'Upah Harian
            Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_GajiBesar_Harian_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)			
		ElseIf strSelectedIndex = "rptpr1000100" Then
			Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_GajiBank_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		ElseIf strSelectedIndex = "rptpr1000101" Then
			Response.Redirect("../../" & strLangCode & "/reports/PR_StdRpt_NonGajiBank_Estate.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		'ElseIf strSelectedIndex = "rptpr1000102" Then
		'	Response.Redirect("../../" & strLangCode & "/reports/PU_StdRpt_BAPP_Pembayaran.aspx?RptName=" & strRptname & "&SelIndex=" & intSelectedIndex & "&UserLoc=" & strUserLoc & "&Dec=" & strDec)
		End If
    End Sub
End Class
