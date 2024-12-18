Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Infragistics.WebUI.UltraWebTab

Public Class PR_trx_RKBDet : Inherits Page


#Region "Declare Var"

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents isNew As HtmlInputHidden
    Protected WithEvents hid_div As HtmlInputHidden
	Protected WithEvents hid_status As HtmlInputHidden
    Protected WithEvents txt_hid_emp As HtmlInputHidden
	
    'Summary
	Protected WithEvents lb_a_skub As Label
	Protected WithEvents lb_a_skuh As Label
	Protected WithEvents lb_a_phl As Label
	Protected WithEvents lb_a_ktk As Label
	Protected WithEvents lb_a_rkn As Label
	Protected WithEvents lb_a_lln As Label
	Protected WithEvents lb_b_skub As Label
	Protected WithEvents lb_b_skuh As Label
	Protected WithEvents lb_b_phl As Label
	Protected WithEvents lb_b_ktk As Label
	Protected WithEvents lb_b_rkn As Label
	Protected WithEvents lb_b_lln As Label
	Protected WithEvents lb_c_skub  As TextBox
	Protected WithEvents lb_c_skuh As TextBox
	Protected WithEvents lb_c_phl As TextBox
	Protected WithEvents lb_c_ktk As Label
	Protected WithEvents lb_c_rkn As Label
	Protected WithEvents lb_c_lln As Label
	Protected WithEvents lb_d_skub  As Label
	Protected WithEvents lb_d_skuh As Label
	Protected WithEvents lb_d_phl As Label
	Protected WithEvents lb_d_ktk As Label
	Protected WithEvents lb_d_rkn As Label
	Protected WithEvents lb_d_lln As Label
	Protected WithEvents lb_e_skub As Label 
	Protected WithEvents lb_e_skuh As Label
	Protected WithEvents lb_e_phl As Label
	Protected WithEvents lb_e_ktk As Label
	Protected WithEvents lb_e_rkn As Label
	Protected WithEvents lb_e_lln As Label
	Protected WithEvents lb_e_tot As Label
	
	
	
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

#Region "Var BK"
    Protected WithEvents LblidM As Label
    Protected WithEvents ddldivisicode As DropDownList
	Protected WithEvents ddlEmpMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList
    Protected WithEvents txtnotes As TextBox
    Protected WithEvents lbldivisicode As Label
  

    Protected WithEvents TABBKTOT As UltraWebTab


    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblupdatedby As Label

    Protected WithEvents Newbtn As Button
    Protected WithEvents SaveBtn As Button
	Protected WithEvents ConfmBtn As Button
	Protected WithEvents ReActBtn As Button
	Protected WithEvents CopyBtn As Button
	Protected WithEvents DelBtn As Button 

    Dim alert As Byte = 0
    Dim strEmpDivCode As String
    Dim hastbl_jb As New System.Collections.Hashtable()
    Dim hastbl_cb As New System.Collections.Hashtable()
    Dim hastbl_jbbhn As New System.Collections.Hashtable()
    Dim hastbl_bhn As New System.Collections.Hashtable()
    Dim hastbl_cbbhn As New System.Collections.Hashtable()
    Dim hastbl_jbbhnall As New System.Collections.Hashtable()
    Dim hastbl_subsub As New System.Collections.Hashtable()

#End Region

#Region "Var HKJ"

    Protected WithEvents shgu as LinkButton
	Protected WithEvents shlb as LinkButton
	Protected WithEvents shkt as LinkButton
	Protected WithEvents shrk as LinkButton
	Protected WithEvents shln as LinkButton
	
    Protected WithEvents TRGaji as HtmlTableRow
	Protected WithEvents TRLembur as HtmlTableRow
	Protected WithEvents TRKontraktor as HtmlTableRow
	Protected WithEvents TRRekanan as HtmlTableRow
	Protected WithEvents TRLain as HtmlTableRow
	Protected WithEvents TRGaji2 as HtmlTableRow
	Protected WithEvents TRLembur2 as HtmlTableRow
	Protected WithEvents TRKontraktor2 as HtmlTableRow
	Protected WithEvents TRRekanan2 as HtmlTableRow
	Protected WithEvents TRLain2 as HtmlTableRow
	
	Protected WithEvents ddlrkbbefore As DropDownList
	
    Protected WithEvents dgjob As DataGrid
    Protected WithEvents dgjoblbr As DataGrid
	Protected WithEvents dgjobktk As DataGrid
	Protected WithEvents dgjobrkn As DataGrid
	Protected WithEvents dgjoblln As DataGrid
	
	Protected WithEvents ddltiperkb As DropDownList
	Protected WithEvents ddlEmpType As DropDownList
	Protected WithEvents ddlEmpTypelbr As DropDownList
    
	Protected WithEvents ddljobkat As DropDownList
    Protected WithEvents ddljobskat As DropDownList
    Protected WithEvents ddljob As DropDownList
    Protected WithEvents ddljobblk As DropDownList
	
	Protected WithEvents ddljobkatlbr As DropDownList
    Protected WithEvents ddljobskatlbr As DropDownList
    Protected WithEvents ddljoblbr As DropDownList
    Protected WithEvents ddljobblklbr As DropDownList
	
	Protected WithEvents ddljobkatktk As DropDownList
    Protected WithEvents ddljobskatktk As DropDownList
    Protected WithEvents ddljobktk As DropDownList
    Protected WithEvents ddljobblkktk As DropDownList
	Protected WithEvents ddljobsupktk As DropDownList
	Protected WithEvents ddljobpoktk  As DropDownList
	Protected WithEvents txtketktk As TextBox
	
	Protected WithEvents ddljobkatrkn As DropDownList
    Protected WithEvents ddljobskatrkn As DropDownList
    Protected WithEvents ddljobrkn As DropDownList
    Protected WithEvents ddljobblkrkn As DropDownList
	Protected WithEvents ddljobitemrkn As DropDownList
	Protected WithEvents ddljobsuprkn As DropDownList
	Protected WithEvents txtketrkn As TextBox
	
	Protected WithEvents ddljobkatlln As DropDownList
    Protected WithEvents ddljobskatlln As DropDownList
    Protected WithEvents ddljoblln As DropDownList
    Protected WithEvents ddljobblklln As DropDownList
	Protected WithEvents txtketlln As TextBox


#End Region


#End Region

#Region "Page Load"
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")
		intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = False Or IntLevel < 1 Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strBKMCode = Trim(IIf(Request.QueryString("RKBCode") <> "", Request.QueryString("RKBCode"), Request.Form("RKBCode")))
            lblErrMessage.Visible = False

            If Not IsPostBack Then
				BindDivisi()
                BindAccYear(Session("SS_SELACCYEAR"))
                ddlEmpMonth.SelectedIndex = CInt(Session("SS_SELACCMONTH")) - 1
				onLoad_BindJobCat()
				onLoad_BindBahan()
				onLoad_BindSupplier()
				ConfmBtn.Attributes("onclick") = "javascript:return ConfirmAction('confirm');"
				ReActBtn.Attributes("onclick") = "javascript:return ConfirmAction('Re-Active');"
				DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('Re-Active');"
				
                If strBKMCode <> "" Then
                    isNew.Value = "False"
					
                    onLoad_Display()
                Else
                    isNew.Value = "True"
					hid_status.value = "3" 'Active
                End If
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errmesg=" & lblErrMessage.Text )
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

    Function getCode() As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        tcode = "RKB/" & strLocation & "/" & ddlyear.SelectedItem.Value.Trim() & ddlEmpMonth.SelectedItem.Value.Trim() & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|20|" & tcode & "|4"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Private Function RKH_beforesave() As Boolean
        Dim SDivCode As String
       
        If ddldivisicode.SelectedItem.Value.Trim() <> "" Then
            SDivCode = ddldivisicode.SelectedItem.Value.Trim()
        Else
            lblErrMessage.Text = "Silakan isi  divisi"
            Return False
        End If

        If (SDivCode = "") Then
            lblErrMessage.Text = "Silakan pilih divisi... "
            Return False
        End If

        Return True
    End Function

	Sub onLoad_BindBfrRKB()
		Dim strOpCd_EmpDiv As String = "PR_PR_TRX_RKB_MAIN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim StrFilter As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|AND A.RKBCode <> '" & LblidM.Text.Trim & "' AND A.LocCode='" & strLocation & "' AND A.Status='1' AND A.IDDiv='" & ddldivisicode.SelectedItem.Value.Trim() & "' AND A.RKBCode <> '" & LblidM.Text.Trim & "'|ORDER By BlkGrpCode"


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_DIVISI_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try
		
		If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("RKBCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("RKBCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Notes") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("RKBCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Notes")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("RKBCode") = ""
        dr("Notes") = "Pilih RKB"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlrkbbefore.DataSource = objEmpDivDs.Tables(0)
        ddlrkbbefore.DataTextField = "Notes"
        ddlrkbbefore.DataValueField = "RKBCode"
        ddlrkbbefore.DataBind()
        ddlrkbbefore.SelectedIndex = 0
	
	End Sub
	
    Sub RKH_onSave(Byval st as String)
        Dim strIDM As String
        Dim SDivCode As String
        Dim strnotes As String


        Dim strOpCd_Up As String = "PR_PR_TRX_RKB_MAIN_ADD"
        Dim dt As String = objGlobal.GetShortDate(strDateFmt, Now())
        Dim SM As String = ddlEmpMonth.SelectedItem.Value.Trim()
        Dim SY As String = ddlyear.SelectedItem.Value.Trim()
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        If isNew.Value = "True" Then
            strIDM = getCode()
        Else
            strIDM = LblidM.Text.Trim
        End If
        LblidM.Text = strIDM

        If ddldivisicode.SelectedItem.Value.Trim() <> "" Then
            SDivCode = ddldivisicode.SelectedItem.Value.Trim()
        Else
            SDivCode = ""
        End If

        strnotes = txtnotes.Text.Trim()

        ParamNama = "RKB|LOC|AM|AY|DC|NTS|ST|CD|UD|UI|TK_SKUB|TK_SKUH|TK_PHL"
        ParamValue = strIDM & "|" & _
                     strLocation & "|" & _
                     SM & "|" & _
                     SY & "|" & _
                     SDivCode & "|" & _
                     strnotes & "|" & _ 
					 st & "|" & _
                     DateTime.Now() & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId & "|" & _
					 iif(lb_c_skub.Text.trim()="","0",lb_c_skub.Text.trim()) & "|" & _
					 iif(lb_c_skuh.Text.trim()="","0",lb_c_skub.Text.trim()) & "|" & _
					 iif(lb_c_phl.Text.trim()="","0",lb_c_skub.Text.trim()) 

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKH_MAIN_ADD&errmesg=" & ex.ToString() & "&redirect=")
        End Try
		 isNew.Value = "False"

    End Sub
	
	Sub RKB_Del(Byval RKB as String)
		Dim strOpCd_Up As String = "PR_PR_TRX_RKB_MAIN_DEL"
		Dim ParamNama As String = "RKB|LOC"
        Dim ParamValue As String = RKB & "|" & strlocation
        Dim intErrNo As Integer
		
		Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKH_MAIN_ADD&errmesg=" & ex.ToString() & "&redirect=")
        End Try
	
	End Sub
	
	 Sub RKB_Copy(Byval oldRKB as String)
        Dim strIDM As String
        Dim strOpCd_Up As String = "PR_PR_TRX_RKBLN_MJOB_COPY"
        Dim dt As String = objGlobal.GetShortDate(strDateFmt, Now())
        Dim SM As String = ddlEmpMonth.SelectedItem.Value.Trim()
        Dim SY As String = ddlyear.SelectedItem.Value.Trim()
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        strIDM = LblidM.Text.Trim
      
        ParamNama = "RKB|OLDRKB|LOC"
        ParamValue = strIDM & "|" & _
					 oldRKB & "|" & _
                     strLocation 

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKH_MAIN_ADD&errmesg=" & ex.ToString() & "&redirect=")
        End Try

    End Sub
	

#Region "Function & Procedure HJK"
    Sub AddList(byval ty as String,jc as String,bc as String,ic as String,sc as String,po as String,ey as String,ket as string)
		Dim strOpCd_Upd As String = "PR_PR_TRX_RKBLN_MJOB_ADD"
        Dim strIDM As String = LblidM.Text.Trim
       

        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

       
        ParamName = "RKB|TY|JC|BC|IC|SC|PO|LOC|CD|UD|UI|EY|KET"
        ParamValue = strIDM & "|" & _ 
		             ty & "|" & _
     			     jc & "|" & _ 
					 bc & "|" & _ 
					 ic & "|" & _ 
					 sc & "|" & _ 
					 po & "|" & _ 
                     strLocation & "|" & _
                     DateTime.Now() & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId & "|" & _
					 ey & "|" & _
					 ket

            Try
                intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errmesg=" & Exp.Message )
            End Try
      
	
	End Sub
	
	Sub DelList(ByVal search as String)
		Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Upd As String = "PR_PR_TRX_RKBLN_MJOB_DEL"
        Dim intErrNo As Integer

        ParamNama = "RKB|LOC|SEARCH"
        ParamValue = LblidM.Text.Trim() & "|" & _
					 strLocation & "|" & _
					 search 


        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errmesg=" & lblErrMessage.Text )
        End Try

	End Sub
	
    
    Private Function HJK_JOB_Load() As DataSet
        Dim strOpCd As String = "PR_PR_TRX_RKBLN_MJOB_LOAD"
        Dim objCodeDs As New Object()
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String
        
        ParamNama = "RKB|LOC"
        ParamValue =  LblidM.Text.Trim() & "|" & strlocation 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKHLN_MJOB_LOAD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objCodeDs
    End Function

    

    Sub HJK_JOB()
	    Dim tbl as Dataset = HJK_JOB_Load()
		if tbl.Tables.Count > 0 then
	    dgjob.DataSource = tbl.Tables(0)
        dgjob.DataBind()

		dgjoblbr.DataSource = tbl.Tables(1)
		dgjoblbr.DataBind()
		
		dgjobktk.DataSource = tbl.Tables(2)
		dgjobktk.DataBind()
		
		dgjobrkn.DataSource = tbl.Tables(3)
		dgjobrkn.DataBind()
		
		dgjoblln.DataSource = tbl.Tables(4)
		dgjoblln.DataBind()
		
		BindSummary(tbl)
		end if
    End Sub

  

#End Region


#End Region

#Region "Binding"

#Region "Binding BK"

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        ddlyear.DataSource = objAccYearDs.Tables(0)
        ddlyear.DataValueField = "AccYear"
        ddlyear.DataTextField = "UserName"
        ddlyear.DataBind()
        ddlyear.SelectedIndex = intSelectedIndex - 1
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
	
	Sub BindSummary(Byval Ds As DataSet)
	if Ds.Tables(5).Rows.Count > 0 Then
	lb_a_skub.Text = Ds.Tables(5).Rows(0).Item("a_skub")
	lb_a_skuh.Text = Ds.Tables(5).Rows(0).Item("a_skuh")
	lb_a_phl.Text = Ds.Tables(5).Rows(0).Item("a_phl")
	lb_a_ktk.Text = Ds.Tables(5).Rows(0).Item("a_ktk")
	lb_a_rkn.Text = Ds.Tables(5).Rows(0).Item("a_rkn")
	lb_a_lln.Text = Ds.Tables(5).Rows(0).Item("a_lln")
	
	lb_b_skub.Text = Ds.Tables(5).Rows(0).Item("b_skub")
	lb_b_skuh.Text = Ds.Tables(5).Rows(0).Item("b_skuh")
	lb_b_phl.Text = Ds.Tables(5).Rows(0).Item("b_phl")
	lb_b_ktk.Text = Ds.Tables(5).Rows(0).Item("b_ktk")
	lb_b_rkn.Text = Ds.Tables(5).Rows(0).Item("b_rkn")
	lb_b_lln.Text = Ds.Tables(5).Rows(0).Item("b_lln")
	
	lb_c_skub .Text = Ds.Tables(5).Rows(0).Item("c_skub")
	lb_c_skuh.Text = Ds.Tables(5).Rows(0).Item("c_skuh")
	lb_c_phl.Text = Ds.Tables(5).Rows(0).Item("c_phl")
	lb_c_ktk.Text = Ds.Tables(5).Rows(0).Item("c_ktk")
	lb_c_rkn.Text = Ds.Tables(5).Rows(0).Item("c_rkn")
	lb_c_lln.Text = Ds.Tables(5).Rows(0).Item("c_lln")
	
	lb_d_skub .Text = Ds.Tables(5).Rows(0).Item("d_skub")
	lb_d_skuh.Text = Ds.Tables(5).Rows(0).Item("d_skuh")
	lb_d_phl.Text = Ds.Tables(5).Rows(0).Item("d_phl")
	lb_d_ktk.Text = Ds.Tables(5).Rows(0).Item("d_ktk")
	lb_d_rkn.Text = Ds.Tables(5).Rows(0).Item("d_rkn")
	lb_d_lln.Text = Ds.Tables(5).Rows(0).Item("d_lln")
	
	lb_e_skub.Text = FormatNumber(Ds.Tables(5).Rows(0).Item("e_skub")) 
	lb_e_skuh.Text = FormatNumber(Ds.Tables(5).Rows(0).Item("e_skuh"))
	lb_e_phl.Text = FormatNumber(Ds.Tables(5).Rows(0).Item("e_phl"))
	lb_e_ktk.Text = FormatNumber(Ds.Tables(5).Rows(0).Item("e_ktk"))
	lb_e_rkn.Text = FormatNumber(Ds.Tables(5).Rows(0).Item("e_rkn"))
	lb_e_lln.Text = FormatNumber(Ds.Tables(5).Rows(0).Item("e_lln"))
	lb_e_tot.Text = FormatNumber(Ds.Tables(5).Rows(0).Item("e_tot"))
	end if
	End Sub

#End Region

#Region "Binding HKJ"

    'PEKERJAAN
    Function BindBkCategory() As DataSet 
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_CATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE (1 = 1) |"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_CATEGORY_GET_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("CatID") = ""
        dr("CatName") = "Pilih Kategori"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        Return objEmpDivDs
    End Function

    Function BindBKSubKategory(ByVal id As String) As DataSet 
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

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("SubCatID") = ""
        dr("SubCatName") = "Pilih Sub Kategori"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        Return objEmpDivDs

    End Function

    Function BindPekerjaan(ByVal cat As String, ByVal subcat As String) as DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_RKH_JOB_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE CatID='" & cat & "' AND SubCatId='" & subcat & "' AND LocCode='" & strLocation & "' And Status='1'|Order by Description"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKH_JOB_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("jobcode") = ""
        dr("Description") = "Pilih Pekerjaan"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        return objEmpCodeDs

    End Function
	
	Function Bind_Blok(ByVal dv As String, ByVal jc As String) as DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_RKB_BLOK_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow


        strParamName = "LOC|DV|JC"
        strParamValue = strLocation & "|" & dv & "|" & jc 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_BKM_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

		dr = objEmpBlkDs.Tables(0).NewRow()
        dr("BlokCode") = "*"
        dr("Description") = "All Blok"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)
		
        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("BlokCode") = ""
        dr("Description") = "Pilih Blok"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)
		
		

        return objEmpBlkDs


    End Function
	

	
	Function Bind_Item() as DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_RKB_BAHAN_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow


         strParamName = "SEARCH|SORT"
        strParamValue = "WHERE LocCode = '" & strLocation & "'|ORDER BY Descr"


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errmesg=" & Exp.Message )
        End Try

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Descr") = "Pilih Bahan"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        return objEmpBlkDs

    End Function
	
	Function Bind_Supplier() as DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_RKB_SUPPLIER_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow


         strParamName = "SEARCH|SORT"
        strParamValue = "|ORDER BY Name"


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errmesg=" & Exp.Message )
        End Try

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("SupplierCode") = ""
        dr("Name") = "Pilih Supplier"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        return objEmpBlkDs

    End Function

	Function Bind_PO(ByVal su as String) as DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_RKB_PO_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "And a.LocCode='" & strlocation & "' And a.SupplierCode='" & su & "'|ORDER BY a.Poid"


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errmesg=" & Exp.Message )
        End Try

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("poid") = ""
        dr("description") = "Pilih PO"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        return objEmpBlkDs

    End Function

	Sub onLoad_BindJobCat()
        ddljobkat.DataSource = BindBkCategory()
        ddljobkat.DataTextField = "CatName"
        ddljobkat.DataValueField = "CatID"
        ddljobkat.DataBind()
		
		ddljobkatlbr.DataSource = BindBkCategory()
        ddljobkatlbr.DataTextField = "CatName"
        ddljobkatlbr.DataValueField = "CatID"
        ddljobkatlbr.DataBind()
		
		ddljobkatktk.DataSource = BindBkCategory()
        ddljobkatktk.DataTextField = "CatName"
        ddljobkatktk.DataValueField = "CatID"
        ddljobkatktk.DataBind()
		
		ddljobkatrkn.DataSource = BindBkCategory()
        ddljobkatrkn.DataTextField = "CatName"
        ddljobkatrkn.DataValueField = "CatID"
        ddljobkatrkn.DataBind()
		
		ddljobkatlln.DataSource = BindBkCategory()
        ddljobkatlln.DataTextField = "CatName"
        ddljobkatlln.DataValueField = "CatID"
        ddljobkatlln.DataBind()
		
    End Sub
	
	Sub onLoad_BindJobSubCat(ByVal ddl As DropDownList, ByVal cat As String)
        ddl.DataSource = BindBKSubKategory(cat)
        ddl.DataTextField = "SubCatName"
        ddl.DataValueField = "SubCatID"
        ddl.DataBind()
    End Sub
	
	Sub onLoad_BindPekerjaan(ByVal ddl As DropDownList, ByVal cat As String, ByVal scat As String)
	    ddl.DataSource = BindPekerjaan(cat,scat)
        ddl.DataTextField = "Description"
        ddl.DataValueField = "jobcode"
        ddl.DataBind()
    End Sub
	
	Sub onLoad_BindBlok(ByVal ddl As DropDownList, ByVal jc As String)
	    ddl.DataSource = Bind_Blok(ddldivisicode.selectedItem.Value.trim(),jc)
        ddl.DataTextField = "Description"
        ddl.DataValueField = "BlokCode"
        ddl.DataBind()
    End Sub
	
	Sub onLoad_BindBahan()
	    ddljobitemrkn.DataSource = Bind_Item()
        ddljobitemrkn.DataTextField = "Descr"
        ddljobitemrkn.DataValueField = "ItemCode"
        ddljobitemrkn.DataBind()
    End Sub
	
	Sub onLoad_BindSupplier()
	    ddljobsuprkn.DataSource = Bind_Supplier() 
        ddljobsuprkn.DataTextField = "Name"
        ddljobsuprkn.DataValueField = "SupplierCode"
        ddljobsuprkn.DataBind()
		
		ddljobsupktk.DataSource = Bind_Supplier() 
        ddljobsupktk.DataTextField = "Name"
        ddljobsupktk.DataValueField = "SupplierCode"
        ddljobsupktk.DataBind()
    End Sub
	
    Sub onLoad_BindPO()
	    ddljobpoktk.DataSource = Bind_PO(ddljobsupktk.selectedItem.Value.trim()) 
        ddljobpoktk.DataTextField = "description"
        ddljobpoktk.DataValueField = "poid"
        ddljobpoktk.DataBind()
    End Sub
	
    

#End Region

#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_RKB_MAIN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "| AND RKBCode = '" & strBKMCode & "' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKH_MAIN_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            LblidM.Text = strBKMCode
            strEmpDivCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDDiv"))

            ddldivisicode.selectedValue = strEmpDivCode
            lbldivisicode.Text = strEmpDivCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("divisi")) & ")"

            lblPeriod.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("AccYear"))
			txtnotes.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("notes"))

            hid_status.value = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Status"))
			Select Case hid_status.value.Trim() 
				Case "1" 
					lblStatus.Text = "Confirm"
				Case "3" 
					lblStatus.Text = "Active/Next Approve by Manager"
				Case "4"
					lblStatus.Text = "Delete"
			End Select  
            lblDateCreated.Text = Date_Validation(objEmpCodeDs.Tables(0).Rows(0).Item("CreateDate"),True)
            lblLastUpdate.Text = Date_Validation(objEmpCodeDs.Tables(0).Rows(0).Item("UpdateDate"),True)
            lblupdatedby.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("UserName"))

            onLoad_BindBfrRKB()
            HJK_JOB()
        End If
    End Sub

    Sub onLoad_button()

    SaveBtn.Attributes("onclick") = "javascript:return ConfirmAction('updateall');"
    TRGaji.Visible = False
	TRLembur.Visible = False
	TRKontraktor.Visible = False
	TRRekanan.Visible = False
	TRLain.Visible = False
	TRGaji2.Visible = False
	TRLembur2.Visible = False
	TRKontraktor2.Visible = False
	TRRekanan2.Visible = False
	TRLain2.Visible = False
	
		Newbtn.visible = False
		SaveBtn.visible = False
		ConfmBtn.visible = False
		ReActBtn.visible = False
		CopyBtn.visible = False
		DelBtn.visible = False
		
		
				
						
	
        If isNew.Value = "False" Then
            ddldivisicode.Visible = False
            lbldivisicode.Visible = True
            ddlEmpMonth.Visible = False
			ddlyear.Visible = False
			lblPeriod.Visible = True
			TRGaji.Visible = True
	        TRLembur.Visible = True
	        TRKontraktor.Visible = True
	        TRRekanan.Visible = True
	        TRLain.Visible = True
			TRGaji2.Visible = True
	        TRLembur2.Visible = True
	        TRKontraktor2.Visible = True
	        TRRekanan2.Visible = True
	        TRLain2.Visible = True
        Else
            ddldivisicode.Visible = True
            ddlEmpMonth.Visible = True
			ddlyear.Visible = True
            lbldivisicode.Visible = False
            lblPeriod.Visible = False
        End If
		
		Select Case hid_status.Value.Trim()
				Case "3" 'Active
				Select Case intlevel 
						Case 1
						Newbtn.visible = True
		                SaveBtn.visible = True
		                CopyBtn.visible = True
						DelBtn.visible = True
						Case > 1 
						if Trim(LblidM.Text) <> "" Then
						ConfmBtn.visible = True
						else
						ConfmBtn.visible = False
						end if
				End Select
				Case "1" 'Cofirm
				TRGaji2.Visible = False
				TRLembur2.Visible = False
				TRKontraktor2.Visible = False
				TRRekanan2.Visible = False
				TRLain2.Visible = False
				Select Case intlevel 
						Case 1
						Newbtn.visible = True
						Case > 1
						ReActBtn.visible = True
				End Select
		End Select

    End Sub

#Region "Event BK"



    Sub BtnNewBK_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
        Response.Redirect("PR_trx_RKBDet.aspx")
    End Sub
	

    Sub BtnSaveBK_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
        If isNew.Value = "True" Then
            If (Not RKH_beforesave()) Then
                UserMsgBox(Me, lblErrMessage.Text)
                alert = 1
                Exit Sub
            End If
        End If
        RKH_onSave("3")

        If (alert = 0) Then
            strBKMCode = LblidM.Text.Trim()
            isNew.Value = "False"
            onLoad_Display()
            onLoad_button()
        End If
    End Sub
	
	
	Sub BtnConfm_onClick(ByVal Sender As Object, ByVal E As EventArgs)
		RKH_onSave("1")
		onLoad_Display()
		onLoad_button()
	End Sub
	
	Sub BtnDelete_onClick(ByVal Sender As Object, ByVal E As EventArgs)
		RKB_Del(LblidM.Text.Trim())
		Response.Redirect("PR_trx_RKBList.aspx")
	End Sub
	
	Sub BtnReAct_onClick(ByVal Sender As Object, ByVal E As EventArgs)
		RKH_onSave("3")
		onLoad_Display()
		onLoad_button()
	End Sub
	
    Sub BtnBackBK_onClick(ByVal Sender As Object, ByVal E As EventArgs)
        Response.Redirect("PR_trx_RKBList.aspx")
    End Sub
	
	Sub BtnRefreshBK_onClick(ByVal Sender As Object, ByVal E As EventArgs)
	    HJK_JOB()
	End Sub
	
	Sub Copybtn_Click(ByVal Sender As Object, ByVal E As EventArgs)
	if ddlrkbbefore.selectedItem.value.trim()=""
		exit sub
	end if
	    RKB_Copy(ddlrkbbefore.selectedItem.value.trim())
	    HJK_JOB()
	End Sub
	
	Sub BtnPrint_onClick(ByVal Sender As Object, ByVal E As EventArgs)
	Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PR_Rpt_RKBDet.aspx?RKBCode=" & LblidM.Text.Trim() & _
                       "&strStatus=" & hid_status.value.Trim() & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

	End Sub
	

#End Region

#Region "Event HKJ"
    'PEKERJAAN 

	Sub ddljobkat_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
          onLoad_BindJobSubCat(ddljobskat,Sender.SelectedItem.Value.Trim())
    End Sub
	Sub ddljobkatlbr_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
          onLoad_BindJobSubCat(ddljobskatlbr,Sender.SelectedItem.Value.Trim())
    End Sub
	Sub ddljobkatktk_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
          onLoad_BindJobSubCat(ddljobskatktk,Sender.SelectedItem.Value.Trim())
    End Sub
	Sub ddljobkatrkn_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
          onLoad_BindJobSubCat(ddljobskatrkn,Sender.SelectedItem.Value.Trim())
    End Sub
	Sub ddljobkatlln_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
          onLoad_BindJobSubCat(ddljobskatlln,Sender.SelectedItem.Value.Trim())
    End Sub
	
	Sub ddljobskat_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindPekerjaan(ddljob,ddljobkat.SelectedItem.Value.Trim(),Sender.SelectedItem.Value.Trim())
    End Sub	
	
	Sub ddljobskatlbr_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindPekerjaan(ddljoblbr,ddljobkatlbr.SelectedItem.Value.Trim(),Sender.SelectedItem.Value.Trim())
    End Sub
	
	Sub ddljobskatktk_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindPekerjaan(ddljobktk,ddljobkatktk.SelectedItem.Value.Trim(),Sender.SelectedItem.Value.Trim())
    End Sub
	
	Sub ddljobskatrkn_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindPekerjaan(ddljobrkn,ddljobkatrkn.SelectedItem.Value.Trim(),Sender.SelectedItem.Value.Trim())
    End Sub
	
	Sub ddljobskatlln_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindPekerjaan(ddljoblln,ddljobkatlln.SelectedItem.Value.Trim(),Sender.SelectedItem.Value.Trim())
    End Sub
	
	Sub ddljob_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindBlok(ddljobblk,Sender.SelectedItem.Value.Trim())
    End Sub
	
	Sub ddljoblbr_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindBlok(ddljobblklbr,Sender.SelectedItem.Value.Trim())
    End Sub
	
	Sub ddljobktk_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindBlok(ddljobblkktk,Sender.SelectedItem.Value.Trim())
    End Sub
	
	Sub ddljobrkn_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindBlok(ddljobblkrkn,Sender.SelectedItem.Value.Trim())
    End Sub
	
	Sub ddljoblln_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        onLoad_BindBlok(ddljobblklln,Sender.SelectedItem.Value.Trim())
    End Sub
	
	Sub ddljobsupktk_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
	    onLoad_BindPO()
	End Sub
	
	Sub ddljobpoktk_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		txtketktk.text = ddljobpoktk.selectedItem.text.trim() 
	End Sub
	
	'--Upah
	
    Sub dgjob_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("Approve"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('Approve');"
			Updbutton = CType(e.Item.FindControl("Delete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('Delete');"
        End If
    End Sub
	
    Sub dgjob_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_ln As HiddenField = CType(E.Item.FindControl("hidrkbln"), HiddenField)
		Dim hid_ty As HiddenField = CType(E.Item.FindControl("hidrkbty"), HiddenField)	
        
        DelList("And RKBCodeLn = '" & hid_ln.value.trim()& "' AND RKBType='" & hid_ty.value.trim() &"'" )
        HJK_JOB()
    End Sub
	
	
	Sub BtnAdduph_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
	    if ddljob.selectedItem.value.trim() = "" then
		    lblErrMessage.Text = "pilih pekerjaan utk gaji dan upah"
			exit sub
		end if
		if ddljobblk.selectedItem.value.trim() = "" then
		    lblErrMessage.Text = "pilih blok utk gaji dan upah"
			exit sub
		end if
		AddList("1",ddljob.selectedItem.value.trim(),ddljobblk.selectedItem.value.trim(),"","","",ddlEmpType.selectedItem.Text.trim(),"")
		HJK_JOB()
    End Sub
	
	Sub BtnSaveuph_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
		Dim i As Integer
		Dim strOpCd_Upd As String = "PR_PR_TRX_RKBLN_MJOB_UPD"
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim KET As HiddenField 
		Dim PO As HiddenField
		Dim HS As TextBox  
		Dim HKUNIT As TextBox 
		Dim HKSKUB As Label 
		Dim HKSKUH As Label 
		Dim HKPHL As Label 
		Dim HKKontr As HiddenField
		Dim RpUnit As TextBox 
		Dim RpRekanan As HiddenField
		Dim RpLain  As HiddenField
        Dim RKBLN As HiddenField 
 
        For i = 0 To dgjob.Items.Count - 1
		 KET = dgjob.Items.Item(i).FindControl("hidket")
		 PO = dgjob.Items.Item(i).FindControl("hidpoid")
		 HS = dgjob.Items.Item(i).FindControl("dgjobvol")
		 HKUNIT = dgjob.Items.Item(i).FindControl("dghkunit")
		 HKSKUB = dgjob.Items.Item(i).FindControl("dghkskub")
		 HKSKUH = dgjob.Items.Item(i).FindControl("dghkskuh") 
		 HKPHL = dgjob.Items.Item(i).FindControl("dghkphl")
		 HKKontr = dgjob.Items.Item(i).FindControl("hidhkkontr")
		 RpUnit = dgjob.Items.Item(i).FindControl("dgrpunit")
		 RpRekanan = dgjob.Items.Item(i).FindControl("hidRpRekanan")
		 RpLain  = dgjob.Items.Item(i).FindControl("hidRpLain")
         RKBLN = dgjob.Items.Item(i).FindControl("hidrkbln")
		 
		 ParamName = "RKB|RKBLN|TY|KET|PO|HS|HKUNIT|HKSKUB|HKSKUH|HKPHL|HKKONTR|RPUNIT|RPREKAN|RPLAIN|UI"
         ParamValue = Trim(LblidM.Text)  & "|" & _
                      RKBLN.value.Trim() & "|" & _
					  "1"                & "|" & _
					  KET.value.Trim()    & "|" & _
					  PO.value.Trim()    & "|" & _
					  IIf(HS.Text.Trim()="","Null",HS.Text.Trim())    & "|" & _
					  IIf(HKUNIT.Text.Trim()="","Null",HKUNIT.Text.Trim())    & "|" & _
					  IIf(HKSKUB.Text.Trim()="","Null",HKSKUB.Text.Trim())    & "|" & _
					  IIf(HKSKUH.Text.Trim()="","Null",HKSKUH.Text.Trim())    & "|" & _
					  IIf(HKPHL.Text.Trim()="","Null",HKPHL.Text.Trim())    & "|" & _
					  IIf(HKKontr.value.Trim()="","Null",HKKontr.value.Trim())    & "|" & _
					  IIf(RpUnit.Text.Trim()="","Null",RpUnit.Text.Trim())    & "|" & _
					  IIf(RpRekanan.value.Trim()="","Null",RpRekanan.value.Trim())    & "|" & _
					  IIf(RpLain.value.Trim()="","Null",RpLain.value.Trim())     & "|" & _
					  struserid 
            Try
                intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errmesg=" & Exp.Message)
            End Try
        Next
	HJK_JOB()	
    End Sub
	
	Sub BtnDeluph_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
        DelList("AND RKBType='" & "1" &"'")
        HJK_JOB()
    End Sub

   '--Lembur
	Sub dgjoblbr_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("Approvelbr"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('Approve');"
			Updbutton = CType(e.Item.FindControl("Deletelbr"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('Delete');"
        End If
    End Sub
	
    Sub dgjoblbr_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_ln As HiddenField = CType(E.Item.FindControl("hidrkblnlbr"), HiddenField)
		Dim hid_ty As HiddenField = CType(E.Item.FindControl("hidrkbtylbr"), HiddenField)	
        
        DelList("And RKBCodeLn = '" & hid_ln.value.trim()& "' AND RKBType='" & hid_ty.value.trim() &"'" )
        HJK_JOB()
    End Sub
	
	
	Sub BtnAddlbr_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
	    if ddljoblbr.selectedItem.value.trim() = "" then
		    lblErrMessage.Text = "pilih pekerjaan utk lembur"
			exit sub
		end if
		if ddljobblklbr.selectedItem.value.trim() = "" then
		    lblErrMessage.Text = "pilih blok utk lembur"
			exit sub
		end if
		AddList("2",ddljoblbr.selectedItem.value.trim(),ddljobblklbr.selectedItem.value.trim(),"","","",ddlEmpTypelbr.selectedItem.Text.trim(),"")
		HJK_JOB()
    End Sub
	
	Sub BtnSavelbr_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
        Dim i As Integer
		Dim strOpCd_Upd As String = "PR_PR_TRX_RKBLN_MJOB_UPD"
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim KET As HiddenField 
		Dim PO As HiddenField
		Dim HS As TextBox  
		Dim HKUNIT As TextBox 
		Dim HKSKUB As Label 
		Dim HKSKUH As Label 
		Dim HKPHL As Label 
		Dim HKKontr As HiddenField
		Dim RpUnit As TextBox 
		Dim RpRekanan As HiddenField
		Dim RpLain  As HiddenField
        Dim RKBLN As HiddenField 
 
        For i = 0 To dgjoblbr.Items.Count - 1
		 KET = dgjoblbr.Items.Item(i).FindControl("hidketlbr")
		 PO = dgjoblbr.Items.Item(i).FindControl("hidpoidlbr")
		 HS = dgjoblbr.Items.Item(i).FindControl("dgjobvollbr")
		 HKUNIT = dgjoblbr.Items.Item(i).FindControl("dghkunitlbr")
		 HKSKUB = dgjoblbr.Items.Item(i).FindControl("dghkskublbr")
		 HKSKUH = dgjoblbr.Items.Item(i).FindControl("dghkskuhlbr") 
		 HKPHL = dgjoblbr.Items.Item(i).FindControl("dghkphllbr")
		 HKKontr = dgjoblbr.Items.Item(i).FindControl("hidhkkontrlbr")
		 RpUnit = dgjoblbr.Items.Item(i).FindControl("dgrpunitlbr")
		 RpRekanan = dgjoblbr.Items.Item(i).FindControl("hidRpRekananlbr")
		 RpLain  = dgjoblbr.Items.Item(i).FindControl("hidRpLainlbr")
         RKBLN = dgjoblbr.Items.Item(i).FindControl("hidrkblnlbr")
		 
		 ParamName = "RKB|RKBLN|TY|KET|PO|HS|HKUNIT|HKSKUB|HKSKUH|HKPHL|HKKONTR|RPUNIT|RPREKAN|RPLAIN|UI"
         ParamValue = Trim(LblidM.Text)  & "|" & _
                      RKBLN.value.Trim() & "|" & _
					  "2"                & "|" & _
					  KET.value.Trim()    & "|" & _
					  PO.value.Trim()    & "|" & _
					  IIf(HS.Text.Trim()="","Null",HS.Text.Trim())    & "|" & _
					  IIf(HKUNIT.Text.Trim()="","Null",HKUNIT.Text.Trim())    & "|" & _
					  IIf(HKSKUB.Text.Trim()="","Null",HKSKUB.Text.Trim())    & "|" & _
					  IIf(HKSKUH.Text.Trim()="","Null",HKSKUH.Text.Trim())    & "|" & _
					  IIf(HKPHL.Text.Trim()="","Null",HKPHL.Text.Trim())    & "|" & _
					  IIf(HKKontr.value.Trim()="","Null",HKKontr.value.Trim())    & "|" & _
					  IIf(RpUnit.Text.Trim()="","Null",RpUnit.Text.Trim())    & "|" & _
					  IIf(RpRekanan.value.Trim()="","Null",RpRekanan.value.Trim())    & "|" & _
					  IIf(RpLain.value.Trim()="","Null",RpLain.value.Trim())  & "|" & _
					  struserid
            Try
                intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errmesg=" & Exp.Message)
            End Try
        Next
		HJK_JOB()
    End Sub
	
	Sub BtnDellbr_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
        DelList("AND RKBType='" & "2" &"'")
        HJK_JOB()
    End Sub
	
	'--kontraktor

    Sub dgjobktk_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("Approvektk"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('Approve');"
			Updbutton = CType(e.Item.FindControl("Deletektk"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('Delete');"
        End If
    End Sub
	
    Sub dgjobktk_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_ln As HiddenField = CType(E.Item.FindControl("hidrkblnktk"), HiddenField)
		Dim hid_ty As HiddenField = CType(E.Item.FindControl("hidrkbtyktk"), HiddenField)	
        
        DelList("And RKBCodeLn = '" & hid_ln.value.trim()& "' AND RKBType='" & hid_ty.value.trim() &"'" )
        HJK_JOB()
    End Sub
	
	
	Sub BtnAddktk_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
	    if ddljobktk.selectedItem.value.trim() = "" then
		    lblErrMessage.Text = "pilih pekerjaan utk kontraktor"
			exit sub
		end if
		if ddljobblkktk.selectedItem.value.trim() = "" then
		    lblErrMessage.Text = "pilih blok utk kontraktor"
			exit sub
		end if
		'AddList(byval ty as String,jc as String,bc as String,ic as String,sc as String,po as String,ey as String,ket as string)
		AddList("3",ddljobktk.selectedItem.value.trim(),ddljobblkktk.selectedItem.value.trim(),"",ddljobsupktk.selectedItem.value.trim(),ddljobpoktk.selectedItem.value.trim(),"",txtketktk.text.trim())
		HJK_JOB()
    End Sub
	
	Sub BtnSavektk_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
        Dim i As Integer
		Dim strOpCd_Upd As String = "PR_PR_TRX_RKBLN_MJOB_UPD"
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim KET As TextBox 
		Dim PO As TextBox
		Dim HS As TextBox  
		Dim HKUNIT As HiddenField 
		Dim HKSKUB As HiddenField 
		Dim HKSKUH As HiddenField 
		Dim HKPHL As HiddenField 
		Dim HKKontr As TextBox
		Dim RpUnit As TextBox 
		Dim RpRekanan As HiddenField
		Dim RpLain  As HiddenField
        Dim RKBLN As HiddenField 
 
        For i = 0 To dgjobktk.Items.Count - 1
		 KET = dgjobktk.Items.Item(i).FindControl("dgjobnotektk")
		 PO = dgjobktk.Items.Item(i).FindControl("dgjobpoktk")
		 HS = dgjobktk.Items.Item(i).FindControl("dgjobvolktk")
		 HKUNIT = dgjobktk.Items.Item(i).FindControl("hidhkunitktk")
		 HKSKUB = dgjobktk.Items.Item(i).FindControl("hidhksubktk")
		 HKSKUH = dgjobktk.Items.Item(i).FindControl("hidhkskuhktk") 
		 HKPHL = dgjobktk.Items.Item(i).FindControl("hidhkphlktk")
		 HKKontr = dgjobktk.Items.Item(i).FindControl("dghkkontrktk")
		 RpUnit = dgjobktk.Items.Item(i).FindControl("dgrpunitktk")
		 RpRekanan = dgjobktk.Items.Item(i).FindControl("hidRpRekananktk")
		 RpLain  = dgjobktk.Items.Item(i).FindControl("hidRpLainktk")
         RKBLN = dgjobktk.Items.Item(i).FindControl("hidrkblnktk")
		 
		 ParamName = "RKB|RKBLN|TY|KET|PO|HS|HKUNIT|HKSKUB|HKSKUH|HKPHL|HKKONTR|RPUNIT|RPREKAN|RPLAIN|UI"
         ParamValue = Trim(LblidM.Text)  & "|" & _
                      RKBLN.value.Trim() & "|" & _
					  "3"                & "|" & _
					  KET.Text.Trim()    & "|" & _
					  PO.Text.Trim()    & "|" & _
					  IIf(HS.Text.Trim()="","Null",HS.Text.Trim())    & "|" & _
					  IIf(HKUNIT.value.Trim()="","Null",HKUNIT.value.Trim())    & "|" & _
					  IIf(HKSKUB.value.Trim()="","Null",HKSKUB.value.Trim())    & "|" & _
					  IIf(HKSKUH.value.Trim()="","Null",HKSKUH.value.Trim())    & "|" & _
					  IIf(HKPHL.value.Trim()="","Null",HKPHL.value.Trim())    & "|" & _
					  IIf(HKKontr.Text.Trim()="","Null",HKKontr.Text.Trim())    & "|" & _
					  IIf(RpUnit.Text.Trim()="","Null",RpUnit.Text.Trim())    & "|" & _
					  IIf(RpRekanan.value.Trim()="","Null",RpRekanan.value.Trim())    & "|" & _
					  IIf(RpLain.value.Trim()="","Null",RpLain.value.Trim()) & "|" & _
					  struserid 
					  
            Try
                intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errmesg=" & Exp.Message)
            End Try
        Next
		HJK_JOB()
    End Sub
	
	Sub BtnDelktk_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
        DelList("AND RKBType='" & "3" &"'")
        HJK_JOB()
    End Sub

   '--Rakanan
	Sub dgjobrkn_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("Approverkn"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('Approve');"
			Updbutton = CType(e.Item.FindControl("Deleterkn"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('Delete');"
        End If
    End Sub
	
    Sub dgjobrkn_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_ln As HiddenField = CType(E.Item.FindControl("hidrkblnrkn"), HiddenField)
		Dim hid_ty As HiddenField = CType(E.Item.FindControl("hidrkbtyrkn"), HiddenField)	
        
        DelList("And RKBCodeLn = '" & hid_ln.value.trim()& "' AND RKBType='" & hid_ty.value.trim() &"'" )
        HJK_JOB()
    End Sub
	
	
	Sub BtnAddrkn_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
	    if ddljobrkn.selectedItem.value.trim() = "" then
		    lblErrMessage.Text = "pilih pekerjaan utk rekanan"
			exit sub
		end if
		if ddljobblkrkn.selectedItem.value.trim() = "" then
		    lblErrMessage.Text = "pilih blok utk rekanan"
			exit sub
		end if
		AddList("4",ddljobrkn.selectedItem.value.trim(),ddljobblkrkn.selectedItem.value.trim(),ddljobitemrkn.selectedItem.value.trim(),ddljobsupktk.selectedItem.value.trim(),"","",txtketrkn.text.trim())
		HJK_JOB()
    End Sub
	
	Sub BtnSaverkn_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
        Dim i As Integer
		Dim strOpCd_Upd As String = "PR_PR_TRX_RKBLN_MJOB_UPD"
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim KET As TextBox 
		Dim PO As HiddenField
		Dim HS As TextBox  
		Dim HKUNIT As HiddenField 
		Dim HKSKUB As HiddenField 
		Dim HKSKUH As HiddenField 
		Dim HKPHL As HiddenField 
		Dim HKKontr As TextBox
		Dim RpUnit As TextBox 
		Dim RpRekanan As TextBox
		Dim RpLain  As HiddenField
        Dim RKBLN As HiddenField 
 
        For i = 0 To dgjobrkn.Items.Count - 1
		 KET = dgjobrkn.Items.Item(i).FindControl("dgjobnoterkn")
		 PO = dgjobrkn.Items.Item(i).FindControl("hidpoidrkn")
		 HS = dgjobrkn.Items.Item(i).FindControl("dgjobvolrkn")
		 HKUNIT = dgjobrkn.Items.Item(i).FindControl("dghkunitrkn")
		 HKSKUB = dgjobrkn.Items.Item(i).FindControl("hidhksubrkn")
		 HKSKUH = dgjobrkn.Items.Item(i).FindControl("hidhkskuhrkn") 
		 HKPHL = dgjobrkn.Items.Item(i).FindControl("hidhkphlrkn")
		 HKKontr = dgjobrkn.Items.Item(i).FindControl("dghkkontrrkn")
		 RpUnit = dgjobrkn.Items.Item(i).FindControl("dgrpunitrkn")
		 RpRekanan = dgjobrkn.Items.Item(i).FindControl("dgrprekanrkn")
		 RpLain  = dgjobrkn.Items.Item(i).FindControl("hidRpLainrkn")
         RKBLN = dgjobrkn.Items.Item(i).FindControl("hidrkblnrkn")
		 
		 ParamName = "RKB|RKBLN|TY|KET|PO|HS|HKUNIT|HKSKUB|HKSKUH|HKPHL|HKKONTR|RPUNIT|RPREKAN|RPLAIN|UI"
         ParamValue = Trim(LblidM.Text)  & "|" & _
                      RKBLN.value.Trim() & "|" & _
					  "4"                & "|" & _
					  KET.Text.Trim()    & "|" & _
					  PO.value.Trim()    & "|" & _
					  IIf(HS.Text.Trim()="","Null",HS.Text.Trim())    & "|" & _
					  IIf(HKUNIT.value.Trim()="","Null",HKUNIT.value.Trim())    & "|" & _
					  IIf(HKSKUB.value.Trim()="","Null",HKSKUB.value.Trim())    & "|" & _
					  IIf(HKSKUH.value.Trim()="","Null",HKSKUH.value.Trim())    & "|" & _
					  IIf(HKPHL.value.Trim()="","Null",HKPHL.value.Trim())    & "|" & _
					  IIf(HKKontr.Text.Trim()="","Null",HKKontr.Text.Trim())    & "|" & _
					  IIf(RpUnit.Text.Trim()="","Null",RpUnit.Text.Trim())    & "|" & _
					  IIf(RpRekanan.Text.Trim()="","Null",RpRekanan.Text.Trim())    & "|" & _
					  IIf(RpLain.value.Trim()="","Null",RpLain.value.Trim()) & "|" & _
					  struserid 
					  
            Try
                intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errmesg=" & Exp.Message)
            End Try
        Next
		HJK_JOB()
    End Sub
	
	Sub BtnDelrkn_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
        DelList("AND RKBType='" & "4" &"'")
        HJK_JOB()
    End Sub
	
	'--lain
	Sub dgjoblln_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("Approvelln"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('Approve');"
			Updbutton = CType(e.Item.FindControl("Deletelln"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('Delete');"
        End If
    End Sub
	
    Sub dgjoblln_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_ln As HiddenField = CType(E.Item.FindControl("hidrkblnlln"), HiddenField)
		Dim hid_ty As HiddenField = CType(E.Item.FindControl("hidrkbtylln"), HiddenField)	
        
        DelList("And RKBCodeLn = '" & hid_ln.value.trim()& "' AND RKBType='" & hid_ty.value.trim() &"'" )
        HJK_JOB()
    End Sub
	
	
	Sub BtnAddlln_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
	    if ddljoblln.selectedItem.value.trim() = "" then
		    lblErrMessage.Text = "pilih pekerjaan utk lain-lain"
			exit sub
		end if
		if ddljobblklln.selectedItem.value.trim() = "" then
		    lblErrMessage.Text = "pilih blok utk lain-lain"
			exit sub
		end if
		AddList("5",ddljoblln.selectedItem.value.trim(),ddljobblklln.selectedItem.value.trim(),"","","","",txtketlln.text.trim())
		HJK_JOB()
    End Sub
	
	Sub BtnSavelln_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
        Dim i As Integer
		Dim strOpCd_Upd As String = "PR_PR_TRX_RKBLN_MJOB_UPD"
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim KET As TextBox 
		Dim PO As HiddenField
		Dim HS As TextBox  
		Dim HKUNIT As HiddenField 
		Dim HKSKUB As HiddenField 
		Dim HKSKUH As HiddenField 
		Dim HKPHL As HiddenField 
		Dim HKKontr As HiddenField
		Dim RpUnit As TextBox 
		Dim RpRekanan As HiddenField
		Dim RpLain  As TextBox
        Dim RKBLN As HiddenField 
 
        For i = 0 To dgjoblln.Items.Count - 1
		 KET = dgjoblln.Items.Item(i).FindControl("dgjobnotelln")
		 PO = dgjoblln.Items.Item(i).FindControl("hidpoidlln")
		 HS = dgjoblln.Items.Item(i).FindControl("dgjobvollln")
		 HKUNIT = dgjoblln.Items.Item(i).FindControl("dghkunitlln")
		 HKSKUB = dgjoblln.Items.Item(i).FindControl("hidhksublln")
		 HKSKUH = dgjoblln.Items.Item(i).FindControl("hidhkskuhlln") 
		 HKPHL = dgjoblln.Items.Item(i).FindControl("hidhkphllln")
		 HKKontr = dgjoblln.Items.Item(i).FindControl("dghkkontrlln")
		 RpUnit = dgjoblln.Items.Item(i).FindControl("dgrpunitlln")
		 RpRekanan = dgjoblln.Items.Item(i).FindControl("dgrprekanlln")
		 RpLain  = dgjoblln.Items.Item(i).FindControl("dgrplainlln")
         RKBLN = dgjoblln.Items.Item(i).FindControl("hidrkblnlln")
		 
		 ParamName = "RKB|RKBLN|TY|KET|PO|HS|HKUNIT|HKSKUB|HKSKUH|HKPHL|HKKONTR|RPUNIT|RPREKAN|RPLAIN|UI"
         ParamValue = Trim(LblidM.Text)  & "|" & _
                      RKBLN.value.Trim() & "|" & _
					  "5"                & "|" & _
					  KET.Text.Trim()    & "|" & _
					  PO.value.Trim()    & "|" & _
					  IIf(HS.Text.Trim()="","Null",HS.Text.Trim())    & "|" & _
					  IIf(HKUNIT.value.Trim()="","Null",HKUNIT.value.Trim())    & "|" & _
					  IIf(HKSKUB.value.Trim()="","Null",HKSKUB.value.Trim())    & "|" & _
					  IIf(HKSKUH.value.Trim()="","Null",HKSKUH.value.Trim())    & "|" & _
					  IIf(HKPHL.value.Trim()="","Null",HKPHL.value.Trim())    & "|" & _
					  IIf(HKKontr.value.Trim()="","Null",HKKontr.value.Trim())    & "|" & _
					  IIf(RpUnit.Text.Trim()="","Null",RpUnit.Text.Trim())    & "|" & _
					  IIf(RpRekanan.value.Trim()="","Null",RpRekanan.value.Trim())    & "|" & _
					  IIf(RpLain.Text.Trim()="","Null",RpLain.Text.Trim()) & "|" & _
					  struserid 
					  
            Try
                intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errmesg=" & Exp.Message)
            End Try
        Next
		HJK_JOB()
    End Sub
	
	Sub BtnDellln_OnClick(ByVal Sender As Object, ByVal E As EventArgs)
        DelList("AND RKBType='" & "5" &"'")
        HJK_JOB()
    End Sub
	
	Sub shGU_Click(ByVal Sender As Object, ByVal E As EventArgs)
        if TRGaji.Visible then
			shgu.text = "Show"
			TRGaji.Visible = False
			TRGaji2.Visible = False
		else
		    shgu.text = "Hide"
			TRGaji.Visible = True
			TRGaji2.Visible = True
		end if
    End Sub
	
	Sub shlb_Click(ByVal Sender As Object, ByVal E As EventArgs)
        if TRLembur.Visible then
			shlb.text = "Show"
			TRLembur.Visible = False
			TRLembur2.Visible = False
		else
		    shlb.text = "Hide"
			TRLembur.Visible = True
			TRLembur2.Visible = True
		end if
    End Sub
	
	Sub shkt_Click(ByVal Sender As Object, ByVal E As EventArgs)
        if TRKontraktor.Visible then
			shkt.text = "Show"
			TRKontraktor.Visible = False
			TRKontraktor2.Visible = False
		else
		    shkt.text = "Hide"
			TRKontraktor.Visible = True
			TRKontraktor2.Visible = True
		end if
    End Sub
	
	Sub shrk_Click(ByVal Sender As Object, ByVal E As EventArgs)
        if TRRekanan.Visible then
			shrk.text = "Show"
			TRRekanan.Visible = False
			TRRekanan2.Visible = False
		else
		    shrk.text = "Hide"
			TRRekanan.Visible = True
			TRRekanan2.Visible = True
		end if
    End Sub
	
	Sub shln_Click(ByVal Sender As Object, ByVal E As EventArgs)
        if TRLain.Visible then
			shln.text = "Show"
			TRLain.Visible = False
			TRLain2.Visible = False
		else
		    shln.text = "Hide"
			TRLain.Visible = True
			TRLain2.Visible = True
		end if
    End Sub
	
#End Region


#End Region





End Class
'            SEmpName = Mid(Trim(lblEmpCode.Text), InStr(lblEmpCode.Text, "(") + 1, Len(Trim(lblEmpCode.Text)) - InStr(Trim(lblEmpCode.Text), "(") - 1)
