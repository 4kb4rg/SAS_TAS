Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Collections.Generic
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Infragistics.WebUI.UltraWebTab

Public Class PR_trx_KTNDet_New_Estate : Inherits Page

#Region "Declare Var"

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents isNew As HtmlInputHidden
    Protected WithEvents hid_cat As HtmlInputHidden
    Protected WithEvents hid_subcat As HtmlInputHidden
    Protected WithEvents hid_div As HtmlInputHidden
    Protected WithEvents txt_hid_emp As HtmlInputHidden
	Protected WithEvents hid_status As HtmlInputHidden
	Protected WithEvents hid_jobblk As HtmlInputHidden

    Dim objOk As New agri.GL.ClsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFmt As String
    Dim intPRAR As Long
    Dim intConfig As Integer
	Dim intlevel As Integer

    Dim strBKMCode As String = ""
    Dim intStatus As Integer
    Dim strAcceptFormat As String
	
	Protected WithEvents TRCopyBKM as HtmlTableRow

#Region "Var BK"
    Protected WithEvents txtWPDate As TextBox
    Protected WithEvents ddlbkcategory As DropDownList
    Protected WithEvents ddlbksubcategory As DropDownList
    Protected WithEvents LblidM As Label
    Protected WithEvents ddldivisicode As DropDownList
    Protected WithEvents ddlsupplier As DropDownList
	Protected WithEvents ddlpoid  As DropDownList
    Protected WithEvents ddlreg_bor As DropDownList
    Protected WithEvents txtnotes As TextBox
    
    Protected WithEvents lblWPDate As Label
    Protected WithEvents btnSelDate As Image
    Protected WithEvents lblbkcategory As Label
    Protected WithEvents lblbksubcategory As Label
	Protected WithEvents lbldivisicode As Label
	Protected WithEvents lblregbor As Label
    Protected WithEvents lblsupplier As Label

    Protected WithEvents TABBK As UltraWebTab
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblupdatedby As Label


	Protected WithEvents NewBtn As ImageButton
    Protected WithEvents NewBtn2 As ImageButton
	
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents SaveBtn2 As ImageButton
	
	Protected WithEvents VerBtn As ImageButton
    Protected WithEvents VerBtn2 As ImageButton
	
	Protected WithEvents ConfBtn As ImageButton
    Protected WithEvents ConfBtn2 As ImageButton
	
	Protected WithEvents ReActBt As ImageButton
	
    Protected WithEvents Pn_btnclear As ImageButton
    Protected WithEvents Bor_btnclear As ImageButton
    Protected WithEvents RW_btnclear As ImageButton

    Dim alert As Byte = 0
    Dim strBKCategory As String
    Dim strBKSubCategory As String
    Dim strSuppllierCode As String
	Dim strSuppllierPO As String
	Dim strEmpDivCode AS String
    Dim strRegBor As String
	
    Dim hastbl_jb As New System.Collections.Hashtable()
    Dim hastbl_cb As New System.Collections.Hashtable()
    Dim hastbl_jbbhn As New System.Collections.Hashtable()
    Dim hastbl_bhn As New System.Collections.Hashtable()
    Dim hastbl_cbbhn As New System.Collections.Hashtable()
    Dim hastbl_jbbhnall As New System.Collections.Hashtable()
    Dim hastbl_subsub As New System.Collections.Hashtable()

#End Region

#Region "Var NRW"
    Protected WithEvents dgNRW As DataGrid
	Protected WithEvents NRW_src_ddl_divisi As DropDownList
    Protected WithEvents NRW_src_ddl_emp As DropDownList
#End Region


#Region "BHNALL"
    Protected WithEvents dgBHNALL As DataGrid
    Protected WithEvents dgBHNALL_idd As DropDownList

#End Region
    

#End Region

#Region "Page Load"
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")
		intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strBKMCode = Trim(IIf(Request.QueryString("BKMCode") <> "", Request.QueryString("BKMCode"), Request.Form("BKMCode")))
            lblErrMessage.Visible = False

            If Not IsPostBack Then
                txtWPDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
				If strBKMCode <> "" Then
                    isNew.Value = "False"
                    onLoad_Display()
                Else
                    isNew.Value = "True"
					hid_status.value = "3" 'Active
                    BindBkCategory()
                End If
				BindDivisi()
                onLoad_button()
            End If
        End If
    End Sub

#End Region

#Region "Function & Procedure"

    Public Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object()
        Dim objActualDate As New Object()
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=pu/trx/pu_GRList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

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

    Function toNumber(ByVal s As String) As String
        If (s = "" Or s = "NULL") Then
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber("0", 2), 2)
        Else
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(s, 2), 2)
        End If

    End Function

    Function getCode(ByVal cat As String, ByVal scat As String) As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        tcode = "SPK/" & strLocation & "/" & cat & "-" & scat & "/" & Mid(Trim(txtWPDate.Text), 4, 2) & Right(Trim(txtWPDate.Text), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|21|" & tcode & "|4"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Private Function BKM_Already_Exist() As SByte
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_KTN_MAIN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "LOC|SEARCH|SORT"

        strParamValue = strLocation & "| AND A.IDCat = '" & ddlbkcategory.SelectedItem.Value.Trim() & "' " & _
                        " AND A.IDSubCat = '" & ddlbksubcategory.SelectedItem.Value.Trim() & "' " & _
                        " AND A.BKMDate = '" & Date_Validation(Trim(txtWPDate.Text), False) & "' " & _
                        " AND A.SUPPLIERCode = '" & ddlsupplier.SelectedItem.Value.Trim() & "' " & _
                        " AND A.LocCode ='" & strLocation & "' " & _
                        " AND A.Status='1'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return objEmpCodeDs.Tables(0).Rows.Count

    End Function

    Private Function BKM_beforesave() As Boolean
        Dim SIdCat As String = ddlbkcategory.SelectedItem.Value.Trim()
        Dim SIdSubCat As String = ddlbksubcategory.SelectedItem.Value.Trim()
        Dim SDivCode As String
        Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)
        Dim SSupCode As String
        Dim SPOCode As String
		
		

		Dim sreg_bor As String = ddlreg_bor.SelectedItem.Value.Trim()
        If ddldivisicode.SelectedItem.Value.Trim() <> "" Then
            SDivCode = ddldivisicode.SelectedItem.Value.Trim()
        Else
            lblErrMessage.Text = "Silakan isi  divisi"
            Return False
        End If

        If ddlsupplier.SelectedItem.Value.Trim() <> "" Then
            SSupCode = ddlsupplier.SelectedItem.Value.Trim()
        Else
		    SSupCode = ""
        End If
		
		 If ddlpoid.SelectedItem.Value.Trim() <> "" Then
            SPOCode = ddlpoid.SelectedItem.Value.Trim()
        Else
		    SPOCode = ""
        End If

        If (SIdCat = "") Then
            lblErrMessage.Text = "Silakan pilih kategori..."
            Return False
        End If

        If (SIdSubCat = "") Then
            lblErrMessage.Text = "Silakan pilih sub kategori..."
            Return False
        End If

        If (SDate = "") Then
            lblErrMessage.Text = "Silakan input tanggal..."
            Return False
        End If

        If (SDivCode = "") Then
            lblErrMessage.Text = "Silakan pilih divisi... "
            Return False
        End If

        If (SSupCode = "") Then
            lblErrMessage.Text = "Silakan pilih Supplier... "
            Return False
        End If
		
		If (SPOCode = "") Then
            lblErrMessage.Text = "Silakan pilih Supplier... "
            Return False
        End If
        Return True
    End Function

    Sub BKM_onSave()
        Dim SIdCat As String = ddlbkcategory.SelectedItem.Value.Trim()
        Dim SIdSubCat As String = ddlbksubcategory.SelectedItem.Value.Trim()
		Dim strIDM As String = getCode(SIdCat, SIdSubCat)
        Dim SDivCode As String
        Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)
        Dim SSupCode As String
        Dim SPOCode As String
        Dim sreg_bor As String = ddlreg_bor.SelectedItem.Value.Trim()
        Dim strnotes As String
	

        Dim strOpCd_Up As String = "PR_PR_TRX_KTN_MAIN_ADD"
        Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer

		LblidM.Text = strIDM
		
        If ddldivisicode.SelectedItem.Value.Trim() <> "" Then
            SDivCode = ddldivisicode.SelectedItem.Value.Trim()
        Else
            SDivCode = ""
        End If

        If ddlsupplier.SelectedItem.Value.Trim() <> "" Then
            SSupCode = ddlsupplier.SelectedItem.Value.Trim()
        Else
            SSupCode = ""
        End If

        If ddlpoid.SelectedItem.Value.Trim() <> "" Then
            SPOCode = ddlpoid.SelectedItem.Value.Trim()
        Else
            SPOCode = ""
        End If

        strnotes = txtnotes.Text.Trim()

        ParamNama = "BKM|LOC|IC|IS|SC|AM|AY|BD|DC|NTS|ST|CD|UD|UI|PO"
        ParamValue = strIDM & "|" & _
                     strLocation & "|" & _
                     SIdCat & "|" & _
                     SIdSubCat & "|" & _
                     SSupCode & "|" & _
                     SM & "|" & _
                     SY & "|" & _
                     SDate & "|" & _
                     SDivCode & "|" & _
                     strnotes & "|3|" & _
                     DateTime.Now() & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId & "|" & _
					 SPOCode 
		
        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_KTN_MAIN_ADD&errmesg=" & ex.ToString() & "&redirect=")
        End Try

    End Sub

    Function BKM_onUpdate() as Boolean
        Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)
        Dim SMdrCode As String
		Dim SKCSCode As String
		Dim STRNCode as String
		Dim SMdr1Code As String
        Dim strnotes As String
        Dim strOpCd_Up As String = "PR_PR_TRX_KTN_MAIN_UPD"
        Dim dt As String = objGlobal.GetShortDate(strDateFmt, Now())
        Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
        Dim ParamNama As String
        Dim strnopolisi As String = ""
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim sreg_bor As String = ddlreg_bor.SelectedItem.Value.Trim() 

        strnotes = txtnotes.Text.Trim()
        ParamNama = "BKM|LOC|BD|NTS|UD|UI"
        ParamValue = LblidM.Text.Trim & "|" & _
                     strLocation & "|" & _
                     SDate & "|" & _
                     strnotes & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId 
					 
					 
        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_KTN_MAIN_UPD&errmesg=" & ex.ToString() & "&redirect=")
        End Try
		
		Return True
      End Function
	  
	  Private Function Cek_PO(ByVal ec As String,ByVal hs As Integer) As String
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_KTNLN_ECODE_CEK"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
		Dim strIDM As String = LblidM.Text
		Dim hsl As Integer

	    Dim PD As String = ""
        
		strParamName = "LOC|PO"
        strParamValue = strLocation & "|" & ec 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_PN_CEK&errmesg=" & Exp.Message & "&redirect=")
        End Try
		
		If objEmpCodeDs.Tables(0).Rows.Count > 0 Then 
		    hsl = hs + cint(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("qtyhasil"))) 
			If  hsl > cint(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("qtyorder")))
			PD = "Supplier : " + Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Name"))+" PO : "+Trim(objEmpCodeDs.Tables(0).Rows(0).Item("poid"))+" (hasil:"+cstr(hsl)+" > order:"+Trim(objEmpCodeDs.Tables(0).Rows(0).Item("qtyorder"))+")"			
			else 
			PD = ""
			end if
		Else 
		    PD = "" 
		End if

        Return PD


    End Function
	
#Region "Function & Procedure HJK"
    Private Function Cek_ALokasi(ByVal jb As String, ByVal cb As String) As Boolean
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_KTNLN_RW_CEK"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()

		Dim PD As String = Right(Trim(txtWPDate.Text), 4) & Mid(Trim(txtWPDate.Text), 4, 2)
        
		strParamName = "JC|BC|LOC|PD"
        strParamValue = jb & "|" & cb & "|" & strLocation & "|" & PD 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_KTNLN_RW_CEK&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return Trim(objEmpCodeDs.Tables(0).Rows(0).Item("status"))


    End Function

#End Region

#Region "Function & Procedure NRW"
    Private Function NRW_Pekerjaan_beforesave() As Boolean
        Dim i As Integer
        Dim row As Integer = dgNRW.Items.Count
        Dim noinput As Integer = 0
        Dim ddl_jb As DropDownList
        Dim ddl_cb As DropDownList
		Dim hid_ec As HiddenField
        Dim txt_hk As TextBox
        Dim txt_hs As TextBox
        Dim cb As String = ""
		Dim pd as String = ""
		Dim tothasil As Integer = 0

        ' jika semua kryawan pekerjaannya ga diisi return false
        For i = 0 To row - 1
            ddl_jb = dgNRW.Items.Item(i).FindControl("dgNRW_ddl_jb")
            If ddl_jb.SelectedItem.Value.Trim = "" Then
                noinput = noinput + 1
            End If
        Next

        If noinput = row Then
            lblErrMessage.Text = "Silakan isi pekerjaan ..."
            Return False
        End If

        For i = 0 To row - 1
            ddl_jb = dgNRW.Items.Item(i).FindControl("dgNRW_ddl_jb")
            ddl_cb = dgNRW.Items.Item(i).FindControl("dgNRW_ddl_cb")
            txt_hk = dgNRW.Items.Item(i).FindControl("dgNRW_txt_hk")
            txt_hs = dgNRW.Items.Item(i).FindControl("dgNRW_txt_hs")
			hid_ec = dgNRW.Items.Item(i).FindControl("dgNRW_hid_ec")

            
            If (ddl_jb.SelectedItem.Value.Trim <> "") And (Trim(txt_hs.Text = "")) Then
                lblErrMessage.Text = "Silakan isi pekerjaan,blok dan hasil kerja..."
                Return False
            End If

            If (ddl_jb.SelectedItem.Value.Trim <> "") And (ddl_cb.SelectedItem.Value.Trim <> "") Then
                If Cek_ALokasi(ddl_jb.SelectedItem.Value.Trim, ddl_cb.SelectedItem.Value.Trim) Then
                    lblErrMessage.Text = "Alokasi Aktiviti '" & ddl_jb.SelectedItem.Text.Trim & "' dithn tnm '" & ddl_cb.SelectedItem.Text.Trim & "' belum terisi ..."
                    Return False
                End If
            End If
			
			tothasil = tothasil + cint(txt_hs.Text.Trim())
			
        Next

				pd = Cek_PO(ddlpoid.SelectedItem.Value.Trim.Trim,tothasil)
				if pd <> "" Then
					lblErrMessage.Text = pd
                    Return False
				End if


        Return True
    End Function

    Private Sub NRW_Pekerjaan_onSave()
        Dim strOpCd_Del As String = "PR_PR_TRX_KTNLN_RW_CLEAR"
        Dim strOpCd_Upd As String = "PR_PR_TRX_KTNLN_RW_UPD"
        Dim strIDM As String = LblidM.Text
        Dim sIdSubCat As String
        Dim sIdDIv As String
        Dim hid_ec As HiddenField
        Dim ddl_jb As DropDownList
        Dim lbl_nr As Label
        Dim ddl_cb As DropDownList
        Dim txt_hk As TextBox
        Dim txt_hs As TextBox
        Dim txt_rot As TextBox
        Dim lbl_um As Label
        Dim ed As String = ""
        Dim jc As String = ""
        Dim cb As String = ""

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        'Clear
        ParamName = "BKM"
        ParamValue = strIDM

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Del, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_RW_CLEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try


        ' Save
        If isNew.Value = "True" Then
            sIdSubCat = ddlbksubcategory.SelectedItem.Value.Trim()
            sIdDIv = ddldivisicode.SelectedItem.Value.Trim()
        Else
            sIdSubCat = Left(lblbksubcategory.Text.Trim(), InStr(lblbksubcategory.Text.Trim(), " (") - 1)
            sIdDIv = Left(lbldivisicode.Text.Trim(), InStr(lbldivisicode.Text.Trim(), " (") - 1)
        End If

        For i = 0 To dgNRW.Items.Count - 1

           
            hid_ec = dgNRW.Items.Item(i).FindControl("dgNRW_hid_ec")
            ddl_jb = dgNRW.Items.Item(i).FindControl("dgNRW_ddl_jb")
            ddl_cb = dgNRW.Items.Item(i).FindControl("dgNRW_ddl_cb")
            txt_hk = dgNRW.Items.Item(i).FindControl("dgNRW_txt_hk")
            txt_hs = dgNRW.Items.Item(i).FindControl("dgNRW_txt_hs")
            lbl_um = dgNRW.Items.Item(i).FindControl("dgNRW_lbl_um")


            'Save only employee and job exists and blok  and hk and hasil
            If (ddl_jb.SelectedItem.Value.Trim <> "") And (ddl_cb.SelectedItem.Value.Trim <> "") And (txt_hs.Text <> "") Then
                ParamName = "BC|EC|JC|BKM|JJG|HS|UM|LOC|ST|CD|UD|UI"
                ParamValue = ddl_cb.SelectedItem.Value.Trim & "|" & _
                             hid_ec.Value.Trim() & "|" & _
                             ddl_jb.SelectedItem.Value.Trim() & "|" & _
                             strIDM & "|" & _
                             IIf(txt_hk.Text = "", "0", txt_hk.Text) & "|" & _
                             IIf(txt_hs.Text = "", "0", txt_hs.Text) & "|" & _
                             Trim(lbl_um.Text) & "|" & _
                             strLocation & "|3|" & _
                             DateTime.Now() & "|" & _
                             DateTime.Now() & "|" & _
                             strUserId

                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_RW_UPD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next


    End Sub

    Private Sub NRW_Save()
        If isNew.Value = "True" Then
            If (Not BKM_beforesave()) Then
                UserMsgBox(Me, lblErrMessage.Text)
                alert = 1
                Exit Sub
            End If

            If (Not NRW_Pekerjaan_beforesave()) Then
                UserMsgBox(Me, lblErrMessage.Text)
                alert = 1
                Exit Sub
            End If
            BKM_onSave()
        Else
            if (Not BKM_onUpdate()) Then
			    UserMsgBox(Me, lblErrMessage.Text)
			    alert = 1
                Exit Sub
			end if
        End If
       NRW_Pekerjaan_onSave()
       BHNALL_Bahan_onSave()
    End Sub

    Function NRW_Footer() As String
        Dim i As Integer
        Dim txt_hk As TextBox
        Dim txt_ha As TextBox
        Dim cntHk As Single = 0
        Dim cntHa As Single = 0

        For i = 0 To dgNRW.Items.Count - 1
            txt_hk = dgNRW.Items.Item(i).FindControl("dgNRW_txt_hk")
            txt_ha = dgNRW.Items.Item(i).FindControl("dgNRW_txt_hs")
            If txt_hk.Text.Trim <> "" Then
                cntHk = cntHk + CSng(txt_hk.Text)
            End If
            If txt_ha.Text.Trim <> "" Then
                cntHa = cntHa + CSng(txt_ha.Text)
            End If
        Next
        NRW_Footer = objGlobal.GetIDDecimalSeparator_FreeDigit(cntHk, 2) & "|" & objGlobal.GetIDDecimalSeparator_FreeDigit(cntHa, 2)
    End Function
#End Region

#Region "Function & Procedure BHNALL"
    Private Sub BHNALL_Bahan_onSave()
        Dim strOpCd_Del As String = "PR_PR_TRX_BKMLN_BHN_CLEAR"
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKMLN_BHN_UPD"
        Dim strIDM As String = LblidM.Text
        Dim hid_ec As HiddenField
        Dim hid_cb As HiddenField
        Dim hid_jb As HiddenField
        Dim ddl_ic As DropDownList

        Dim txt_qty As TextBox
        Dim lbl_um As Label
        Dim jc As String = ""
        Dim ic As String = ""


        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        'Clear
        ParamName = "BKM"
        ParamValue = strIDM

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Del, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_RWBHN_CLEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        'Save
        For i = 0 To dgBHNALL.Items.Count - 1


            hid_ec = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_hid_ec")
            hid_jb = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_hid_jb")
            hid_cb = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_hid_cb")
            ddl_ic = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_ddl_itm")
            txt_qty = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_txt_Qty")
            lbl_um = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_lbl_um")

            If ddl_ic.SelectedItem.Value.Trim <> "" Then
                ic = Left(ddl_ic.SelectedItem.Value.Trim(), InStr(ddl_ic.SelectedItem.Value.Trim(), "|") - 1)
            Else
                ic = ""
            End If

            'Save only employee and job exists and blok  and hk and hasil
            If (ddl_ic.SelectedItem.Value.Trim <> "") And (txt_qty.Text.Trim <> "") Then
                ParamName = "BC|LOC|JC|BKM|EC|IC|QT|UM|ST|CD|UD|UI"
                ParamValue = hid_cb.Value.Trim & "|" & _
                             strLocation & "|" & _
                             hid_jb.Value.Trim & "|" & _
                             strIDM & "|" & _
                             hid_ec.Value.Trim & "|" & _
                             ic & "|" & _
                             txt_qty.Text.Trim() & "|" & _
                             Trim(lbl_um.Text) & "|" & _
                             "1|" & _
                             DateTime.Now() & "|" & _
                             DateTime.Now() & "|" & _
                             strUserId

                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_RWBHN_UPD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next


    End Sub
#End Region

#End Region

#Region "Binding"

#Region "Binding BK"

    Sub BindBkCategory()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_CATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE (CatID <> 'AD') |"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_CATEGORY_GET_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("CatName") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatName")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("CatID") = ""
        dr("CatName") = "Pilih Kategori"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlbkcategory.DataSource = objEmpDivDs.Tables(0)
        ddlbkcategory.DataTextField = "CatName"
        ddlbkcategory.DataValueField = "CatID"
        ddlbkcategory.DataBind()
    End Sub

    Sub BindBKSubKategory(ByVal id As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_SUBCATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND idCat='" & id & "' |Order by SubCatID"


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBCATEGORY_GET_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatName") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatName")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("SubCatID") = ""
        dr("SubCatName") = "Pilih Sub Kategori"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlbksubcategory.DataSource = objEmpDivDs.Tables(0)
        ddlbksubcategory.DataTextField = "SubCatName"
        ddlbksubcategory.DataValueField = "SubCatID"
        ddlbksubcategory.DataBind()

    End Sub

    Sub BindDivisi(ByVal id As String, ByVal sid As String, ByVal regbo As String)
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
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
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

        ddldivisicode.DataSource = objEmpDivDs.Tables(0)
        ddldivisicode.DataTextField = "Description"
        ddldivisicode.DataValueField = "BlkGrpCode"
        ddldivisicode.DataBind()
        ddldivisicode.SelectedIndex = 0

    End Sub

    Sub BindSupplier(ByVal mdr As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_KTN_SUPPLIER_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
		Dim intSelectedIndex2 As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
		Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "'|ORDER By Name"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_KTN_SUPPLIER_LIST&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("SupplierCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("SupplierCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("Name") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("Name")) 
				 If Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("SupplierCode")) = Trim(mdr) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("SupplierCode") = ""
        dr("Name") = "Pilih Supplier"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSupplier.DataSource = objEmpCodeDs.Tables(0)
        ddlSupplier.DataTextField = "Name"
        ddlSupplier.DataValueField = "SupplierCode"
        ddlSupplier.DataBind()
		ddlSupplier.SelectedIndex = intSelectedIndex
		
    End Sub
	
	 Sub BindSupplierPO(ByVal Sup As String,ByVal Po As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_KTN_SUPPLIER_PO_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
		Dim intSelectedIndex2 As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
		Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)

        strParamName = "SEARCH|SORT"
        ''strParamValue = "WHERE LocCode='" & strLocation & "' AND SupplierCode='" & Sup & "' AND LEFT(POID,3)='SPK'|ORDER By POID"
        strParamValue = "WHERE A.LocCode='" & strLocation & "' AND A.SupplierCode='" & Sup & "' |ORDER By A.POID"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_KTN_SUPPLIER_PO_LIST&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("POID") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("POID"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("Ket") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("Ket")) 
				 If Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("POID")) = Trim(Po) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("POID") = ""
        dr("Ket") = "Pilih PO"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPOid.DataSource = objEmpCodeDs.Tables(0)
        ddlPOid.DataTextField = "Ket"
        ddlPOid.DataValueField = "POID"
        ddlPOid.DataBind()
		ddlPOid.SelectedIndex = intSelectedIndex
		
    End Sub

    Sub BindBahan()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_BAHAN_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim filter As String = ""

        If isNew.Value = "True" Then
            strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
        Else
            strBKCategory = Left(lblbkcategory.Text.Trim(), InStr(lblbkcategory.Text.Trim(), "(") - 1)
        End If

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE LocCode = '" & strLocation & "' " & filter & "|ORDER BY Descr"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_BAHAN_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        hastbl_bhn.Clear()
        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
                hastbl_bhn.Add(Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("ItemCode")) & "|" & Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("UOMCode")), Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("Descr")))
            Next
        End If

        hastbl_bhn.Add("", "Pilih Bahan")
    End Sub

    Sub Bind_Blok(ByVal sdc As String, ByVal sjc As String, ByVal ssc As String, ByVal sku As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BLOK_BKM_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()

        strParamName = "LOC|SC|DC|JC|DT"
        strParamValue = strLocation & "|" & ssc & "|" & sdc & "|" & sjc & "|" & Date_Validation(Trim(txtWPDate.Text), False)

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_BKM_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        hastbl_cb.Clear()
        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
                hastbl_cb.Add(Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode")), Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("blok")))
            Next
        End If
        If objEmpBlkDs.Tables(0).Rows.Count > 1 Then
            hastbl_cb.Add("", "Pilih Blok")
        End If
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
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_DIVISI_LIST_GET&errmesg=" & Exp.Message)
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "Pilih Divisi"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        NRW_src_ddl_divisi.DataSource = objEmpDivDs.Tables(0)
        NRW_src_ddl_divisi.DataTextField = "Description"
        NRW_src_ddl_divisi.DataValueField = "BlkGrpCode"
        NRW_src_ddl_divisi.DataBind()

    End Sub

    Sub BindBorongan(ByVal id As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_KTN_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
        Dim dt As String

        If isNew.Value = "True" Then
            dt = Date_Validation(txtWPDate.Text, False)
        Else
            dt = lblWPDate.Text.Trim
        End If

        strParamName = "DT|LOC|SEARCH|SORT"
        strParamValue = dt & "|" & _
                        strLocation & "|" & _
                        "AND y.IDDiv = '" & id & "'|ORDER BY EmpName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_KTN_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpName") = replace(Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode")), rtrim(strlocation), "") & " - " & Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpName"))
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = "Pilih Karyawan"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        NRW_src_ddl_emp.DataSource = objEmpCodeDs.Tables(0)
        NRW_src_ddl_emp.DataTextField = "EmpName"
        NRW_src_ddl_emp.DataValueField = "EmpCode"
        NRW_src_ddl_emp.DataBind()
    End Sub
	
#End Region

#Region "Binding NRW"
    Protected Function dgNRW_Reload() As DataSet
		'Binding Data
        If ddlbksubcategory.Text <> "" Then
            NRW_Pekerjaan(Trim(ddlbkcategory.SelectedItem.Value), Trim(ddlbksubcategory.SelectedItem.Value))
            Bind_Blok(Trim(ddldivisicode.SelectedItem.Value), "", Trim(ddlbksubcategory.SelectedItem.Value), "K")
        Else
            NRW_Pekerjaan(hid_cat.Value, hid_subcat.Value)
            Bind_Blok(Trim(hid_div.Value), "", Trim(hid_subcat.Value), "K")
        End If
		
        'load data
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_KTN_RW_TEMP_UPDGET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim sc As String
        Dim intErrNo As Integer

        If ddlbksubcategory.Text <> "" Then
            sc = ddlbksubcategory.SelectedItem.Value.Trim
        Else
            sc = hid_subcat.Value.Trim
        End If

        strParamName = "SC|LOC|IP|UI|SEARCH|SORT"
        strParamValue = sc & "|" & _
                        strLocation & "|" & _
                        Request.UserHostAddress & "|" & _
                        strUserId & "||"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RW_TEMP_UPDGET&errmesg=" & Exp.Message & "&redirect=")
        End Try


        Return objEmpCodeDs

    End Function

    Sub NRW_EmpByMdr_Clear()
        Dim strOpCd As String = "PR_PR_TRX_KTN_RW_TEMP_CLEAR"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "IP|UI|LOC"
        strParamValue = Request.UserHostAddress & "|" & _
                        strUserId & "|" & _
                        strLocation

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
            dgNRW.DataSource = Nothing
            dgNRW.DataBind()
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_KTN_RW_TEMP_CLEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

	Sub HKJ_RW_Pekerjaan_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList

        If ddlbksubcategory.Text <> "" Then
            NRW_Pekerjaan(Trim(ddlbkcategory.SelectedItem.Value), Trim(ddlbksubcategory.SelectedItem.Value))
        Else
            NRW_Pekerjaan(hid_cat.Value, hid_subcat.Value)
        End If

        DDLabs = dgNRW.Items.Item(index).FindControl("dgNRW_ddl_jb")
        Dim sorted_job = New SortedList(hastbl_jb)
        DDLabs.DataSource = sorted_job
        DDLabs.DataTextField = "value"
        DDLabs.DataValueField = "key"
        DDLabs.DataBind()
    End Sub

    Sub HKJ_RW_Blok_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList
        DDLabs = dgNRW.Items.Item(index).FindControl("dgNRW_ddl_cb")
        Dim sorted_blk = New SortedList(hastbl_cb)
        DDLabs.DataSource = sorted_blk
        DDLabs.DataTextField = "value"
        DDLabs.DataValueField = "key"
        DDLabs.DataBind()

    End Sub
	
    Sub NRW_Pekerjaan(ByVal cat As String, ByVal subcat As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_KTN_JOB_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE CatID='" & cat & "' AND SubCatId='" & subcat & "' AND LocCode='" & strLocation & "' And Status='1'|Order by Description"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RW_JOB_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        hastbl_jb.Clear()
        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                hastbl_jb.Add(Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("jobcode")), Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("Description")))
            Next
        End If
        hastbl_jb.Add("", "Pilih Pekerjaan")

    End Sub
	
	Protected Function RW_EmpByMdrAll() As DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_KTN_RW_TEMP_LISTEMP"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "IP|UI|LOC|SEARCH|SORT"
        strParamValue = Request.UserHostAddress & "|" & _
                        strUserId & "|" & _
                        strLocation & "||" & _
                        "ORDER BY empdet"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RW_TEMP_LISTEMP&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet"))
            Next
        End If

        Return objEmpCodeDs

    End Function

	 
    Sub NRWT_add_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_TRX_KTN_RW_TEMP_UPDADD"
        Dim ed As String = ""
        Dim ec As String = ""

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        If NRW_src_ddl_emp.Text.trim = "" Then
            UserMsgBox(Me, "Silakan pilih divisi & karyawan untuk di tambah")
            Exit Sub
        End If

        ' save dulu
        ec = NRW_src_ddl_emp.SelectedItem.Value.Trim
        'ed = Right(NRW_src_ddl_emp.SelectedItem.Text.Trim, InStr(NRW_src_ddl_emp.SelectedItem.Text.Trim, "-") - 3)
        ed = Left(NRW_src_ddl_emp.SelectedItem.Text.Trim, 50)

        ParamName = "ID|IP|UI|LOC|JB|CB|JJG|HS|UM|EC|ED|VC"
        ParamValue = "0|" & _
            Trim(Request.UserHostAddress) & "|" & _
            strUserId & "|" & _
            strLocation & "|||null|null||" & _
            ec & "|" & _
            ed & "||"

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_KTN_RW_TEMP_UPDADD&errmesg=" & Exp.Message)
        End Try

        'reload
        NRW_BindPekerjaan_OnLoad()

    End Sub

	'Sub NRW_BindEmpByMdr()
    '    Dim dt As String = Date_Validation(txtWPDate.Text, False)
    '    dgNRW.DataSource = RW_EmpByMdr(ddlMandorCode.SelectedItem.Value.Trim(), dt)
    '    dgNRW.DataBind()
    'End Sub
    
    Sub NRW_BindPekerjaan_OnLoad()
        dgNRW.EditItemIndex = -1
        dgNRW.DataSource = dgNRW_Reload()
        dgNRW.DataBind()
    End Sub

#End Region

#Region "Binding BHNALL"
    Sub BHNALL_JOB_NRW()
        Dim hid_id As HiddenField
        Dim hid_ec As HiddenField
        Dim lbl_ed As Label
        Dim ddl_jb As DropDownList
        Dim DDL_cb As DropDownList
        Dim pvalue As String
        Dim i As Integer

        hastbl_jbbhnall.Clear()

        For i = 0 To dgNRW.Items.Count - 1
            hid_id = dgNRW.Items.Item(i).FindControl("dgNRW_hid_id")
            hid_ec = dgNRW.Items.Item(i).FindControl("dgNRW_hid_ec")
            lbl_ed = dgNRW.Items.Item(i).FindControl("dgNRW_lbl_ed")
            ddl_jb = dgNRW.Items.Item(i).FindControl("dgNRW_ddl_jb")
            DDL_cb = dgNRW.Items.Item(i).FindControl("dgNRW_ddl_cb")

            pvalue = hid_ec.Value.Trim & " ( " & lbl_ed.Text.Trim & ")|" & _
                     ddl_jb.SelectedItem.Value.Trim & " ( " & ddl_jb.SelectedItem.Text.Trim & ")|" & _
                     DDL_cb.SelectedItem.Value.Trim

            If hid_ec.Value.Trim <> "" And ddl_jb.SelectedItem.Value.Trim <> "" And DDL_cb.SelectedItem.Value.Trim <> "" Then
                hastbl_jbbhnall.Add(hid_id.Value.Trim, pvalue)
            End If

        Next

        hastbl_jbbhnall.Add("", "Pilih Karyawan|Pekerjaan|Blok")
    End Sub

  

    Protected Function dgBHNALL_Reload() As DataSet

        'load data
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_KTN_BHN_TEMP_UPDGET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim sc As String
        Dim intErrNo As Integer

        If ddlbksubcategory.Text <> "" Then
            sc = ddlbksubcategory.SelectedItem.Value.Trim
        Else
            sc = hid_subcat.Value.Trim
        End If

        strParamName = "SC|LOC|IP|UI|SEARCH|SORT"
        strParamValue = sc & "|" & _
                        strLocation & "|" & _
                        Request.UserHostAddress & "|" & _
                        strUserId & "||" & _
                        "ORDER BY empdet"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_BHN_TEMP_UPDGET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            If hastbl_bhn.Count = 0 Then BindBahan()
        End If

        Return objEmpCodeDs

    End Function

    Sub BHNALL_Clear()
        Dim strOpCd As String = "PR_PR_TRX_KTN_BHN_TEMP_CLEAR"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "IP|UI|LOC"
        strParamValue = Request.UserHostAddress & "|" & _
                        strUserId & "|" & _
                        strLocation

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
            dgBHNALL.DataSource = Nothing
            dgBHNALL.DataBind()
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_BHN_TEMP_CLEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub BHNALL_BindPekerjaan_OnLoad()
        dgBHNALL.EditItemIndex = -1
        dgBHNALL.DataSource = dgBHNALL_Reload()
        dgBHNALL.DataBind()
    End Sub
#End Region

#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_KTN_MAIN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()
		Dim strEmpMandor1 As String

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "| AND BKMCode Like '%" & strBKMCode & "%' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            LblidM.Text = strBKMCode
			lblWPDate.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("BKMDate"))
            txtWPDate.Text = Date_Validation(objEmpCodeDs.Tables(0).Rows(0).Item("BKMDate"), True)
            strSuppllierCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Suppliercode"))
            strSuppllierPO = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("POID"))
            strEmpDivCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDDiv"))

            strBKCategory = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDCat"))
            strBKSubCategory = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDSubCat"))
            strRegBor = "S"
			Select Case Trim(strRegBor) 
				case "S" 
					lblregbor.Text = "S (SPK)"
			End Select 
            
			ddlreg_bor.SelectedValue = Trim(strRegBor) 
            BindDivisi(strBKCategory, strBKSubCategory, strRegBor)
            BindSupplier(strSuppllierCode)
			BindSupplierPO(strSuppllierCode,strSuppllierPO)
            lbldivisicode.Text = strEmpDivCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("divisi")) & ")"
            lblsupplier.Text = strSuppllierCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Name")) & ")"
				
            lblbkcategory.Text = strBKCategory & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CatName")) & ")"
            lblbksubcategory.Text = strBKSubCategory & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("SubcatName")) & ")"
            txtnotes.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("notes")) 
            
            lblPeriod.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("AccYear"))
            hid_status.value = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Status"))
			Select Case hid_status.value.Trim() 
				Case "1" 
					lblStatus.Text = "Confirm"
				Case "2" 
					lblStatus.Text = "Verified / Next approve by Manager"
				Case "3" 
					lblStatus.Text = "Active / Next approve by Asisten"
				Case "4"
					lblStatus.Text = "Delete"
			End Select  
            
			lblDateCreated.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblupdatedby.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("UserName"))
		
            hid_cat.Value = strBKCategory
            hid_subcat.Value = strBKSubCategory
            hid_div.Value = strEmpDivCode
            TABBK.Visible = True

            'clear temp
            NRW_EmpByMdr_Clear()

						NRW_onLoad_Display(strBKMCode)
                        NRW_BindPekerjaan_OnLoad()
                        BHNALL_onLoad_Display(strBKMCode)
                        BHNALL_BindPekerjaan_OnLoad()
                        ShowHideTab("NRW|BHNALL")
                        show_hide_col("RW")
                       
            End If        
    End Sub

    Sub onLoad_button()

        SaveBtn.Attributes("onclick") = "javascript:return ConfirmAction('updateall');"
        SaveBtn2.Attributes("onclick") = "javascript:return ConfirmAction('updateall');"
		
		NewBtn2.visible = False
		NewBtn.visible = False
		ConfBtn2.visible = False
		ConfBtn.visible = False
		VerBtn2.visible = False
		VerBtn.visible = False
		SaveBtn2.visible = False
		SaveBtn.visible = False
		ReActBt.visible = False
		

        If isNew.Value = "False" Then
            ddldivisicode.Visible = False
            txtWPDate.Visible = True
            btnSelDate.Visible = True
            ddlbkcategory.Visible = False
            ddlbksubcategory.Visible = False
            ddlreg_bor.Visible = True
            lbldivisicode.Visible = True
            lblWPDate.Visible = False
            lblbkcategory.Visible = True
            lblbksubcategory.Visible = True
            lblregbor.Visible = False
			lblsupplier.Visible = False
			

			Select Case hid_status.Value.Trim()
				Case "3" 'Active
					Select Case intlevel 
						Case 0
							NewBtn2.visible = True
							NewBtn.visible = True
							ConfBtn2.visible = False
							ConfBtn.visible = False
							VerBtn2.visible = False
							VerBtn.visible = False
							SaveBtn2.visible = True
							SaveBtn.visible = True
						Case 1
							NewBtn2.visible = False
							NewBtn.visible = False
							ConfBtn2.visible = False
							ConfBtn.visible = False
							VerBtn2.visible = True
							VerBtn.visible = True
							SaveBtn2.visible = True
							SaveBtn.visible = True
						Case >= 2
							NewBtn2.visible = False
							NewBtn.visible = False
							ConfBtn2.visible = False
							ConfBtn.visible = False
							VerBtn2.visible = False
							VerBtn.visible = False
							SaveBtn2.visible = False
							SaveBtn.visible = False
					End Select
				Case "2" 'Verified
					Select Case intlevel 
						Case 0
							NewBtn2.visible = True
							NewBtn.visible = True
							ConfBtn2.visible = False
							ConfBtn.visible = False
							VerBtn2.visible = False
							VerBtn.visible = False
							SaveBtn2.visible = False
							SaveBtn.visible = False
						Case 1
							NewBtn2.visible = False
							NewBtn.visible = False
							ConfBtn2.visible = False
							ConfBtn.visible = False
							VerBtn2.visible = False
							VerBtn.visible = False
							SaveBtn2.visible = False
							SaveBtn.visible = False
							ReActBt.visible = True
						Case >= 2
							NewBtn2.visible = False
							NewBtn.visible = False
							ConfBtn2.visible = True
							ConfBtn.visible = True
							VerBtn2.visible = False
							VerBtn.visible = False
							SaveBtn2.visible = True
							SaveBtn.visible = True
							ReActBt.visible = True
					End Select
				Case "1" 'Confirm
					Select Case intlevel 
						Case 0
							NewBtn2.visible = True
							NewBtn.visible = True
							ConfBtn2.visible = False
							ConfBtn.visible = False
							VerBtn2.visible = False
							VerBtn.visible = False
							SaveBtn2.visible = False
							SaveBtn.visible = False
						Case 1
							NewBtn2.visible = False
							NewBtn.visible = False
							ConfBtn2.visible = False
							ConfBtn.visible = False
							VerBtn2.visible = False
							VerBtn.visible = False
							SaveBtn2.visible = False
							SaveBtn.visible = False
							ReActBt.visible = True
						Case >= 2
							NewBtn2.visible = False
							NewBtn.visible = False
							ConfBtn2.visible = False
							ConfBtn.visible = False
							VerBtn2.visible = False
							VerBtn.visible = False
							SaveBtn2.visible = False
							SaveBtn.visible = False
							ReActBt.visible = True
					End Select
				Case "4" 'Delete
					NewBtn2.visible = True
					NewBtn.visible = True
					ConfBtn2.visible = False
					ConfBtn.visible = False
					VerBtn2.visible = False
					VerBtn.visible = False
					SaveBtn2.visible = False
					SaveBtn.visible = False
			End Select
			
        Else
            ddldivisicode.Visible = True
            ddlSupplier.Visible = True
			ddlPoid.Visible = True
            txtWPDate.Visible = True
            btnSelDate.Visible = True
            ddlbkcategory.Visible = True
            ddlbksubcategory.Visible = True
            ddlreg_bor.Visible = True
            lbldivisicode.Visible = False
            lblsupplier.Visible = False
           
            lblWPDate.Visible = False
            lblbkcategory.Visible = False
            lblbksubcategory.Visible = False
            lblregbor.Visible = False

			Select Case intlevel 
				Case 0
					NewBtn2.visible = True
					NewBtn.visible = True
					SaveBtn2.visible = True
					SaveBtn.visible = True
				Case 1
					NewBtn2.visible = False
					NewBtn.visible = False
					SaveBtn2.visible = False
					SaveBtn.visible = False
				Case >=2
					NewBtn2.visible = False
					NewBtn.visible = False
					SaveBtn2.visible = False
					SaveBtn.visible = False
			End Select
        End If

    End Sub

#Region "Event BK"
    Sub ShowHideTab(ByVal key As String)
        Dim oTab As New Infragistics.WebUI.UltraWebTab.Tab
        Dim i, j As Byte
        Dim ary As Array
        Dim arycnt As Byte
        ary = Split(key, "|")
        arycnt = UBound(ary)

        'clear first
        For i = 0 To TABBK.Tabs.Count - 1
            If TypeOf TABBK.Tabs(i) Is Infragistics.WebUI.UltraWebTab.Tab Then
                oTab = CType(TABBK.Tabs(i), Infragistics.WebUI.UltraWebTab.Tab)
                oTab.Visible = False
            End If
        Next

        'visible
        For j = 0 To arycnt
            For i = 0 To TABBK.Tabs.Count - 1
                If TypeOf TABBK.Tabs(i) Is Infragistics.WebUI.UltraWebTab.Tab Then
                    oTab = CType(TABBK.Tabs(i), Infragistics.WebUI.UltraWebTab.Tab)
                    If oTab.Key = ary(j) Then
                        oTab.Visible = True
                        Exit For
                    End If
                End If
            Next
        Next
    End Sub
	
	  Sub show_hide_col(ByVal subcat As String)
        '1 : Pekerjaan
        '2 : Blok
        '3 : JJG
        '4 : Hasil
        '5 : UOM

        dgNRW.Columns(3).Visible = True
			
    End Sub


    Sub ddlbkcategory_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
        BindBKSubKategory(strBKCategory)
        ShowHideTab("")
    End Sub

    Sub ddlbksubcategory_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
        strBKSubCategory = ddlbksubcategory.SelectedItem.Value.Trim()
        strRegBor = ddlreg_bor.SelectedItem.Value.Trim()
        LblidM.Text = getCode(strBKCategory, strBKSubCategory)
        BindDivisi(strBKCategory, strBKSubCategory, strRegBor)
        TABBK.Visible = True
        ddlSupplier.Items.Clear()
		ddlpoid.Items.Clear()
        ShowHideTab("NRW|BHNALL")
		show_hide_col(strBKSubCategory)
    End Sub

    Sub ddlbkregbor_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) 
	    
		strRegBor = ddlreg_bor.SelectedItem.Value.Trim() 
	    strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
       
		if trim(strBKCategory) = "" then 
			exit sub 
		end if
		strBKSubCategory = ddlbksubcategory.SelectedItem.Value.Trim()
        
        LblidM.Text = getCode(strBKCategory, strBKSubCategory)
        BindDivisi(strBKCategory, strBKSubCategory, strRegBor)
        TABBK.Visible = True
        ddlSupplier.Items.Clear()
		ddlpoid.Items.Clear()
		ShowHideTab("NRW|BHNALL")
		show_hide_col(strBKSubCategory)
    End Sub

    Sub ddldivisicode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpDivCode = ddldivisicode.SelectedItem.Value.Trim()
        strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
        strBKSubCategory = ddlbksubcategory.SelectedItem.Value.Trim()
        strRegBor = ddlreg_bor.SelectedItem.Value.Trim()
        BindSupplier("")
		Bind_Blok(strEmpDivCode, "", strBKSubCategory, "K")
    End Sub

    Sub ddl_DgSorting(ByVal ddl As DropDownList, ByVal obj As Hashtable, ByVal slc As String)
        Dim sorted = New SortedList(obj)
        ddl.DataSource = sorted
        ddl.DataTextField = "value"
        ddl.DataValueField = "key"
        ddl.DataBind()
        ddl.SelectedValue = Trim(slc)
    End Sub

    Sub ddlsupplier_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
	    if isNew.Value = "True" Then
		
        strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
        strBKSubCategory = ddlbksubcategory.SelectedItem.Value.Trim()
        strRegBor = ddlreg_bor.SelectedItem.Value.Trim()
        Dim strBKDivisi = ddldivisicode.SelectedItem.Value.Trim()
		NRW_EmpByMdr_Clear()
        NRW_Pekerjaan(strBKCategory, strBKSubCategory)
        Bind_Blok(strBKDivisi, "", strBKSubCategory, "K")
        
		End If
		
		BindSupplierPO(ddlsupplier.SelectedItem.Value.Trim(),"")
	 
    End Sub

    Sub BtnNewBK_OnClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        NRW_EmpByMdr_Clear()
        Response.Clear()
        Response.Redirect("PR_trx_KTNList_Estate.aspx")
    End Sub

    Sub BtnSaveBK_OnClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
	    Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)
	    Dim SM As String = CInt(Mid(Trim(txtWPDate.Text), 4, 2)) 
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
		
		If (SDate = "") Then
		    UserMsgBox(Me, "Input tanggal BKM")
            Exit Sub 
        End If
		
		
        If isNew.Value = "True" Then
            strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
            strRegBor = ddlreg_bor.SelectedItem.Value.Trim()
        Else
            strBKCategory = Left(lblbkcategory.Text.Trim(), InStr(lblbkcategory.Text.Trim(), "(") - 1)
			strRegBor = ddlreg_bor.SelectedItem.Value.Trim()
        End If

        NRW_Save()

        If (alert = 0) Then
            strBKMCode = LblidM.Text.Trim()
            isNew.Value = "False"
            onLoad_Display()
            onLoad_button()
        End If
    End Sub

    Sub BtnBackBK_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        NRW_EmpByMdr_Clear()
        Response.Clear()
        Response.Redirect("PR_trx_KTNList_Estate.aspx")
    End Sub
	
	Sub BtnVerBK_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
		Dim strOpCd_Upd As String = "PR_PR_TRX_KTN_MAIN_APPROVAL_UPD"
		Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim strBKMCode As String = LblidM.Text.Trim()
		
		 ParamName = "BKM|UD|UI|ST|LOC"
         ParamValue = strBKMCode & "|" & _
				      DateTime.Now() & "|" & _
					  strUserId & "|" & _
					  "2" & "|" & _
					  strlocation
							 
		Try
                    intErrNo = Objok.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_UPD&errmesg=" & Exp.Message & "&redirect=PR/trx/PR_trx_DailyAttd_ESTATE.aspx")
                End Try
		isNew.Value = "False"
            onLoad_Display()
            onLoad_button()
	End Sub
	
	Sub BtnConfBK_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
		Dim strOpCd_Upd As String = "PR_PR_TRX_KTN_MAIN_APPROVAL_UPD"
		Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim strBKMCode As String = LblidM.Text.Trim()
		
		 ParamName = "BKM|UD|UI|ST|LOC"
         ParamValue = strBKMCode & "|" & _
				      DateTime.Now() & "|" & _
					  strUserId & "|" & _
					  "1" & "|" & _
					  strlocation
							 
				Try
                    intErrNo = Objok.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_UPD&errmesg=" & Exp.Message & "&redirect=PR/trx/PR_trx_DailyAttd_ESTATE.aspx")
                End Try
	    isNew.Value = "False"
            onLoad_Display()
            onLoad_button()
	End Sub

	Sub BtnReActBK_onClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
       Dim strOpCd_Upd As String = "PR_PR_TRX_KTN_MAIN_APPROVAL_UPD"
		Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim strBKMCode As String = LblidM.Text.Trim()
		
		 ParamName = "BKM|UD|UI|ST|LOC"
         ParamValue = strBKMCode & "|" & _
				      DateTime.Now() & "|" & _
					  strUserId & "|" & _
					  "3" & "|" & _
					  strlocation
							 
				Try
                    intErrNo = Objok.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_UPD&errmesg=" & Exp.Message & "&redirect=PR/trx/PR_trx_DailyAttd_ESTATE.aspx")
                End Try
	    isNew.Value = "False"
            onLoad_Display()
            onLoad_button()
    End Sub
#End Region

#Region "Event NRW"
    Sub NRW_onLoad_Display(ByVal bkmcode As String)
        Dim strOpCd As String = "PR_PR_TRX_KTNLN_RW_LOAD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "IP|BKM|LOC|UI"
        strParamValue = Request.UserHostAddress & "|" & bkmcode & "|" & strLocation & "|" & strUserId

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_RW_LOAD&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

    End Sub

    Sub dgNRW_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim hd As HiddenField
            Dim DDLabs As DropDownList
            Dim DDLabs_blk As DropDownList
            Dim Updbutton As LinkButton


            hd = CType(e.Item.FindControl("dgNRW_hid_jb"), HiddenField)
            DDLabs = CType(e.Item.FindControl("dgNRW_ddl_jb"), DropDownList)
            ddl_DgSorting(DDLabs, hastbl_jb, hd.Value.Trim())

            hd = CType(e.Item.FindControl("dgNRW_hid_cb"), HiddenField)
            DDLabs_blk = CType(e.Item.FindControl("dgNRW_ddl_cb"), DropDownList)
            ddl_DgSorting(DDLabs_blk, hastbl_cb, hd.Value.Trim())

            Updbutton = CType(e.Item.FindControl("Delete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            Dim ar As Array
            ar = Split(NRW_Footer, "|")
            e.Item.Cells(2).Text = ar(0)
            e.Item.Cells(3).Text = ar(1)
        End If

    End Sub

    Protected Function dgRW_GetUOM_Norma(ByVal sjc As String) As String
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_JOB_GET_UOM_BY_JOB"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objCodeDs As New Object()
        Dim intErrNo As Integer

        strParamName = "JC"
        strParamValue = sjc

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOBUOM_GET_BY_JOB&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objCodeDs.Tables(0).Rows.Count = 0 Then
            dgRW_GetUOM_Norma = "|"
        Else
            dgRW_GetUOM_Norma = Trim(objCodeDs.Tables(0).Rows(0).Item("UOM")) & "| " & Trim(objCodeDs.Tables(0).Rows(0).Item("Norma1"))
        End If
    End Function

    Protected Sub dgNRW_ddl_jb_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim content As String = item.Cells(0).Text
        Dim ddljob As DropDownList = CType(item.Cells(0).FindControl("dgNRW_ddl_jb"), DropDownList)
        Dim lbluom As Label
        Dim lblnor As Label

        Dim sjc As String

        sjc = ddljob.SelectedItem.Value.Trim()
        lbluom = CType(item.Cells(3).FindControl("dgNRW_lbl_um"), Label)

        Dim arr As Array
        arr = Split(dgRW_GetUOM_Norma(sjc), "|")
        lbluom.Text = arr(0)

    End Sub

    Sub dgNRW_Delete(ByVal id As String)
        Dim strOpCd As String = "PR_PR_TRX_KTN_RW_TEMP_DEL"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP|UI|LOC|ID"
        strParamValue = Request.UserHostAddress & "|" & _
                        strUserId & "|" & _
                        strLocation & "|" & _
                        id

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RW_TEMP_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub dgNRW_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_id As HiddenField = CType(E.Item.FindControl("dgNRW_hid_id"), HiddenField)
        dgNRW_Delete(hid_id.Value)
        NRW_BindPekerjaan_OnLoad()
    End Sub

    Sub NRW_update()
        Dim strOpCd As String = "PR_PR_TRX_KTN_RW_TEMP_CLEAR"
        Dim strOpCd_Upd As String = "PR_PR_TRX_KTN_RW_TEMP_UPDADD"
        Dim ddl_jb As DropDownList
        Dim ddl_cb As DropDownList
        Dim hid_ec As HiddenField
        Dim lbl_ed As Label
        Dim txt_hk As TextBox
        Dim txt_hs As TextBox
        Dim txt_rot As TextBox
        Dim lbl_nor As Label
        Dim lbl_um As Label

        Dim ed As String = ""
        Dim cb As String = ""

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        'Clear
        ParamName = "IP|UI|LOC"
        ParamValue = Request.UserHostAddress & "|" & _
                        strUserId & "|" & _
                        strLocation

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RW_TEMP_CLEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try



        'Save
        For i = 0 To dgNRW.Items.Count - 1

            hid_ec = dgNRW.Items.Item(i).FindControl("dgNRW_hid_ec")
            ddl_jb = dgNRW.Items.Item(i).FindControl("dgNRW_ddl_jb")
            ddl_cb = dgNRW.Items.Item(i).FindControl("dgNRW_ddl_cb")
            txt_hk = dgNRW.Items.Item(i).FindControl("dgNRW_txt_hk")
            txt_hs = dgNRW.Items.Item(i).FindControl("dgNRW_txt_hs")
            lbl_um = dgNRW.Items.Item(i).FindControl("dgNRW_lbl_um")


            ParamName = "ID|IP|UI|LOC|JB|CB|JJG|HS|UM"
            ParamValue = "0|" & _
        Trim(Request.UserHostAddress) & "|" & _
        strUserId & "|" & _
        strLocation & "|" & _
        ddl_jb.SelectedItem.Value.Trim & "|" & _
        ddl_cb.SelectedItem.Value.Trim & "|" & _
        IIf(txt_hk.Text.Trim = "", "null", txt_hk.Text.Trim) & "|" & _
        IIf(txt_hs.Text.Trim = "", "null", txt_hs.Text.Trim) & "|" & _
        lbl_um.Text.Trim & "|" & _
        hid_ec.value.Trim()


            Try
                intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RW_TEMP_UPDADD&errmesg=" & Exp.Message & "&redirect=")
            End Try
        Next
    End Sub

    Protected Sub NRW_src_ddl_divisi_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindBorongan(NRW_src_ddl_divisi.SelectedItem.Value.Trim())
    End Sub

    Sub NRW_clear_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        NRW_EmpByMdr_Clear()
    End Sub

    Sub NRW_refresh_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        NRW_update()
        NRW_BindPekerjaan_OnLoad()
    End Sub

#End Region

#Region "Event BHNALL"
    Sub BHNALL_add_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If dgBHNALL_idd.Text.Trim <> "" Then
            If dgBHNALL_idd.SelectedItem.Value.Trim <> "" Then
                Dim strOpCd As String = "PR_PR_TRX_BKM_BHN_TEMP_CLEAR"
                Dim strOpCd_Upd As String = "PR_PR_TRX_BKM_BHN_TEMP_UPDADD"
                Dim i As Integer
                Dim ParamName As String
                Dim ParamValue As String
                Dim intErrNo As Integer
                Dim obj_ec As HiddenField
                Dim obj_ed As Label
                Dim obj_jb As HiddenField
                Dim obj_cb As HiddenField
                Dim obj_ci As DropDownList
                Dim obj_qty As TextBox
                Dim obj_um As Label

                Dim ec As String
                Dim ed As String
                Dim jc As String
                Dim cb As String
                Dim ci As String

                'Clear
                ParamName = "IP|UI|LOC"
                ParamValue = Request.UserHostAddress & "|" & _
                                strUserId & "|" & _
                                strLocation

                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_BHN_TEMP_CLEAR&errmesg=" & Exp.Message & "&redirect=")
                End Try


                For i = 0 To dgBHNALL.Items.Count - 1
                    obj_ec = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_hid_ec")
                    obj_ed = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_lbl_ec")
                    obj_jb = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_hid_jb")
                    obj_cb = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_hid_cb")
                    obj_ci = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_ddl_itm")
                    obj_qty = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_txt_Qty")
                    obj_um = dgBHNALL.Items.Item(i).FindControl("dgBHNALL_lbl_um")

                    ec = obj_ec.Value.Trim
                    ed = obj_ed.Text.Trim
                    jc = obj_jb.Value.Trim
                    cb = obj_cb.Value.Trim
                    If obj_ci.SelectedItem.Value.Trim <> "" Then
                        ci = Left(obj_ci.SelectedItem.Value.Trim(), InStr(obj_ci.SelectedItem.Value.Trim(), "|") - 1)
                    Else
                        ci = ""
                    End If

                    'Save 
                    ParamName = "ID|IP|UI|LOC|EC|ED|JB|CB|CI|QT|UM"
                    ParamValue = "0|" & _
                                 Trim(Request.UserHostAddress) & "|" & _
                                 strUserId & "|" & _
                                 strLocation & "|" & _
                                 ec & "|" & _
                                 ed & "|" & _
                                 jc & "|" & _
                                 cb & "|" & _
                                 ci & "|" & _
                                 IIf(obj_qty.Text.Trim = "", "0", obj_qty.Text.Trim) & "|" & _
                                 obj_um.Text.Trim

                    Try
                        intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_BHN_TEMP_UPDADD&errmesg=" & Exp.Message & "&redirect=")
                    End Try

                Next

                Dim arr As Array
                Dim tmp As String
                arr = Split(dgBHNALL_idd.SelectedItem.Text.Trim, "|")

                tmp = Trim(arr(0))
                ec = Left(tmp, InStr(tmp, "(") - 1)
                ed = Mid(tmp, InStr(tmp, "(") + 1, Len(tmp) - InStr(tmp, "(") - 1)
                tmp = Trim(arr(1))
                jc = Left(tmp, InStr(tmp, "(") - 1)
                cb = Trim(arr(2))


                'Add new
                ParamName = "ID|IP|UI|LOC|EC|ED|JB|CB|CI|QT|UM"
                ParamValue = "0|" & _
                             Trim(Request.UserHostAddress) & "|" & _
                             strUserId & "|" & _
                             strLocation & "|" & _
                             ec & "|" & _
                             ed & "|" & _
                             jc & "|" & _
                             cb & "||null|"

                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RW_TEMP_UPDADD&errmesg=" & Exp.Message & "&redirect=")
                End Try
                BHNALL_BindPekerjaan_OnLoad()
            End If
        Else
            UserMsgBox(Me, "Silakan klik refresh untuk menampilkan data dilist")
        End If
    End Sub

    Sub BHNALL_refresh_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If isNew.Value = "True" Then
            strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
            strRegBor = ddlreg_bor.SelectedItem.Value.Trim()
        Else
            strBKCategory = Left(lblbkcategory.Text.Trim(), InStr(lblbkcategory.Text.Trim(), "(") - 1)
            strRegBor = ddlreg_bor.SelectedItem.Value.Trim()

        End If

        BHNALL_JOB_NRW()


        Dim sorted_job = New SortedList(hastbl_jbbhnall)
        dgBHNALL_idd.DataSource = sorted_job
        dgBHNALL_idd.DataTextField = "value"
        dgBHNALL_idd.DataValueField = "key"
        dgBHNALL_idd.DataBind()
    End Sub

    Sub BHNALL_onLoad_Display(ByVal bkmcode As String)
        Dim strOpCd As String = "PR_PR_TRX_KTNLN_BHN_LOAD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "IP|BKM|LOC|UI"
        strParamValue = Request.UserHostAddress & "|" & bkmcode & "|" & strLocation & "|" & strUserId

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_BHN_LOAD&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

    End Sub

    Sub dgBHNALL_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Updbutton As LinkButton
            Dim hd As HiddenField
            Dim hd2 As HiddenField


            Dim DDLabs_itm As DropDownList
            hd2 = CType(e.Item.FindControl("dgBHNALL_hid_uom"), HiddenField)

            hd = CType(e.Item.FindControl("dgBHNALL_hid_itm"), HiddenField)
            DDLabs_itm = CType(e.Item.FindControl("dgBHNALL_ddl_itm"), DropDownList)
            ddl_DgSorting(DDLabs_itm, hastbl_bhn, hd.Value.Trim() & "|" & hd2.Value.Trim())

            Updbutton = CType(e.Item.FindControl("Delete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If

    End Sub

    Sub BHNALL_Delete(ByVal id As String)
        Dim strOpCd As String = "PR_PR_TRX_BKM_BHN_TEMP_DEL"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP|UI|LOC|ID"
        strParamValue = Request.UserHostAddress & "|" & _
                        strUserId & "|" & _
                        strLocation & "|" & _
                        id

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_BHN_TEMP_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub dgBHNALL_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_id As HiddenField = CType(E.Item.FindControl("dgBHNALL_hid_id"), HiddenField)
        BHNALL_Delete(hid_id.Value)
        BHNALL_BindPekerjaan_OnLoad()
    End Sub

    Protected Sub dgBHNALL_ddl_itm_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim ddlType As DropDownList = CType(item.Cells(0).FindControl("dgBHNALL_ddl_itm"), DropDownList)
        Dim tum As Label = CType(item.Cells(1).FindControl("dgBHNALL_lbl_um"), Label)
        Dim arr As Array
        arr = Split(ddlType.SelectedItem.Value.Trim(), "|")
        tum.Text = RTrim(arr(1))
    End Sub
#End Region

#End Region

	Function StatusPayroll(Byval mn as String,Byval yr as String,Byval loc as String)as Integer
        Dim strOpCdSP As String = "PR_PR_TRX_MTHEND_GET_STATUS"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
		Dim i as Integer
      
       
        ParamName = "MN|YR|LOC"
        ParamValue = mn & "|" & _
                     yr & "|" & _
                     loc
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_GET_STATUS&errmesg=" & Exp.Message & "&redirect=")
        End Try
	
		If objDataSet.Tables(0).Rows.Count > 0 Then
        		i = objDataSet.Tables(0).Rows(0).Item("Status")
				IF i = 3 Then
					UserMsgBox(Me, "Proses ditutup, Periode "& mn & "/" & yr & " Sudah Confirm")
				End If
		Else
		        i = 0 
		end if
		
       Return i

    End Function
	
    Sub UpdateStatusPayroll(Byval mn as String,Byval yr as String,Byval loc as String)
        Dim strOpCdSP As String = "PR_PR_TRX_MTHEND_UPD"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
      
       
        ParamName = "MN|YR|LOC|S1|S2|S3|S4"
        ParamValue = mn & "|" & _
                     yr & "|" & _
                     loc & "|1|||" 
        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCdSP, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_UPD&errmesg=" & Exp.Message & "&redirect=")
        End Try

    End Sub

End Class
'            SEmpName = Mid(Trim(lblEmpCode.Text), InStr(lblEmpCode.Text, "(") + 1, Len(Trim(lblEmpCode.Text)) - InStr(Trim(lblEmpCode.Text), "(") - 1)
