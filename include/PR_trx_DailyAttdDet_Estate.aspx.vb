Imports System

Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl


Public Class PR_DailyAttdDet_Estate : Inherits Page

    Protected WithEvents Lblattcode As Label
    Protected WithEvents LblEmpCode As Label
    Protected WithEvents LblEmpName As Label
    Protected WithEvents LblAttDate As Label
    Protected WithEvents LblAttDatetmp As Label
    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblReback As Label
    Protected WithEvents txtHk As TextBox
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents ttt As HiddenField
    Protected WithEvents lblaa As Label

    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents DelBtn As ImageButton

    Protected WithEvents chk_rev As CheckBox
    Protected WithEvents txtrev_hk As DropDownList
    Protected WithEvents txtrev_ket As TextBox

    Protected WithEvents ddlabsent As DropDownList

    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim Objok As New agri.GL.ClsTrx
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
	Dim intLevel As Integer

    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Dim objEmpDs As New Object()
    Dim objEmpTypeDs As New Object()
    Dim objEmpDivDs As New Object()
    Dim objEmpJobDs As New Object()

    Dim strAttDate As String = ""
    Dim StrEmpCode As String = ""
    Dim StrEmpName As String = ""
    Dim StrEmpTy As String = ""

    Dim StrAMonth As String = ""
    Dim StrAYear As String = ""



    Dim StrAttCode As String = ""
    Dim StrAttType As String = ""
    Dim StrAttQty As String = ""
    Dim strAttDatetmp As String = ""
    Dim StrPrev As String = ""
    Dim ListEmp As String
    Dim strDateFmt As String
    Dim strAcceptFormat As String

    Function getgetdefaulthk(ByVal ty As String) As String
        Dim strOpCd_GetID As String = "PR_PR_STP_EMPSALARY_GET"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        ParamName = "SEARCH|SORT"
        ParamValue = "AND A.Status='1' " & _
                     "AND LocCode='" & strLocation & "' " & _
                     "AND (" & StrAYear.Trim & StrAMonth.Trim & " >=  right(rtrim(periodestart),4)+left(rtrim(periodestart),2)) And (" & StrAYear.Trim & StrAMonth.Trim & " <=  right(rtrim(periodeend),4)+left(rtrim(periodeend),2)) " & _
                     "AND CodeEmpTy='" & ty & "'|"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPSALARY_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("Hk"))
    End Function
	
    Function getCode() As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        tcode = "ATT/" & strLocation & "/" & Left(lblaa.Text, 2) & Right(lblaa.Text, 2) & "/"

        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|5|" & tcode & "|5"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))
    End Function

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

    Sub ShowHideRev(ByVal tf As Boolean)

        Dim found As Control = Me.FindControl("divhk")
        If Not found Is Nothing Then
            Dim cast As HtmlGenericControl = CType(found, HtmlGenericControl)
            If Not cast Is Nothing Then
                cast.Visible = tf
            End If
        End If


    End Sub
	
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

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strDateFmt = Session("SS_DATEFMT")
		intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
            'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDailyAttd), intPRAR) = False Then
            '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
            'ElseIf intLevel < 2 Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            lblErrMessage.Visible = False

            lblRedirect.Text = Request.QueryString("redirect")
            StrEmpCode = Trim(IIf(Request.QueryString("EmpCode") <> "", Request.QueryString("EmpCode"), Request.Form("EmpCode")))
            strAttDate = Trim(IIf(Request.QueryString("Attdate") <> "", Request.QueryString("Attdate"), Request.Form("Attdate")))
            StrEmpName = Trim(IIf(Request.QueryString("EmpName") <> "", Request.QueryString("EmpName"), Request.Form("EmpName")))
            StrEmpTy = Trim(IIf(Request.QueryString("EmpTy") <> "", Request.QueryString("EmpTy"), Request.Form("EmpTy")))

            StrAMonth = Trim(IIf(Request.QueryString("AccMonth") <> "", Request.QueryString("AccMonth"), Request.Form("AccMonth")))
            StrAYear = Trim(IIf(Request.QueryString("AccYear") <> "", Request.QueryString("AccYear"), Request.Form("AccYear")))
            'Session("ATT") = Trim(IIf(Request.QueryString("HdrDiv") <> "", Request.QueryString("HdrDiv"), Request.Form("HdrDiv")))
            DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            If Not IsPostBack Then
                If strAttDate <> "" Then
                    LblEmpCode.Text = StrEmpCode
                    LblEmpName.Text = StrEmpName
                    LblAttDate.Text = strAttDate
                    ttt.Value = StrEmpTy
                    lblaa.Text = Trim(StrAMonth) & Trim(StrAYear)
                    onLoad_Display()
                End If

            End If


        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_EmpAtt As String = "PR_PR_TRX_ATTENDANCE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpAttDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "SEARCH"
        strParamValue = "AND EmpCode='" & StrEmpCode & "' AND CONVERT(char(20),AttDate,106)='" & strAttDate & "' AND LocCode='" & strLocation & "'"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpAtt, strParamName, strParamValue, objEmpAttDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpAttDs.Tables(0).Rows.Count = 1 Then
            StrAttCode = Trim(objEmpAttDs.Tables(0).Rows(0).Item("AttID"))
            Lblattcode.Text = StrAttCode
            StrAttType = Trim(objEmpAttDs.Tables(0).Rows(0).Item("AttCode"))
            StrAttQty = Trim(objEmpAttDs.Tables(0).Rows(0).Item("Hk"))
            txtHk.Text = StrAttQty
            LblAttDatetmp.Text = Format(objEmpAttDs.Tables(0).Rows(0).Item("AttDate"), "dd/MM/yyyy")
            lblStatus.Text = "Aktive"
            lblPeriod.Text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objEmpAttDs.Tables(0).Rows(0).Item("AccYear"))
            lblCreateDate.Text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("uName"))
            BindAbsenType(StrAttType)
            if (StrAttType="K") or (StrAttType="HM") or (StrAttType="HB") then 
				chk_rev.Visible = True
			else 
				chk_rev.Visible = False
			end if
			
        Else
            BindAbsenType("K")
            chk_rev.Visible = False
            txtHk.Text = getgetdefaulthk(StrEmpTy)
        End If

     
    End Sub

    Sub BindAbsenType(ByVal at As String)
        Dim strOpCd_Type As String = "PR_PR_STP_ATTCODE_LIST_GET"
        Dim strName As String
        Dim strParam As String
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim objAbsType As New Object()
        Dim intSelectedIndex As Integer = 0


        strName = "SEARCH|SORT"
        strParam = "WHERE status='1'|"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_Type, strName, strParam, objAbsType)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=mtdIDGet&errmesg=" & ex.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objAbsType.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objAbsType.Tables(0).Rows.Count - 1
                objAbsType.Tables(0).Rows(intCnt).Item("AttCode") = Trim(objAbsType.Tables(0).Rows(intCnt).Item("AttCode"))
                objAbsType.Tables(0).Rows(intCnt).Item("Description") = Trim(objAbsType.Tables(0).Rows(intCnt).Item("AttCode")) & " (" & Trim(objAbsType.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If Trim(objAbsType.Tables(0).Rows(intCnt).Item("AttCode")) = at.Trim Then
                    intSelectedIndex = intCnt
                End If

            Next
        End If

        ddlabsent.DataSource = objAbsType.Tables(0)
        ddlabsent.DataTextField = "Description"
        ddlabsent.DataValueField = "AttCode"
        ddlabsent.DataBind()
        ddlabsent.SelectedIndex = intSelectedIndex
    End Sub

    Sub chk_rev_changed(ByVal sender As Object, ByVal e As EventArgs)
		lblErrMessage.Visible = False
        if intLevel = 0 then
			lblErrMessage.Text = "Access Denied !"
            lblErrMessage.Visible = True
			exit sub
		else
			ShowHideRev(chk_rev.Checked)
		end if
    End Sub

    Sub SaveBtn_Click1(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_TRX_ATTENDANCE_UPD"
        Dim strOpCd_korek As String = "PR_PR_TRX_ATTENDANCE_KOREKSI_UPD"

        Dim ParamName As String
        Dim ParamValue As String
        Dim Paramtmp As String = ""
        Dim intErrNo As Integer
		
		IF StatusPayroll(Cint(StrAMonth),strAYear,strLocation) = "3" Then
		  Exit Sub
		End IF

		if (ddlabsent.SelectedValue.trim() = "S") or (ddlabsent.SelectedValue.trim() = "M") or (ddlabsent.SelectedValue.trim() = "C") Then
			lblErrMessage.Text = "Perhatian, BKM (" & LblEmpName.Text &") pada tanggal " & strAttDate & " akan terhapus, karena absensi 'Sakit/Mangkir/Cuti'"
            lblErrMessage.Visible = True
		end if

        If chk_rev.Checked Then
		
		    if (ddlabsent.SelectedValue.trim() <> "K") and (ddlabsent.SelectedValue.trim() <> "HM") and (ddlabsent.SelectedValue.trim() <> "HB") Then
 			    lblErrMessage.Text = "Koreksi hk hanya pada absen Kerja !"
                lblErrMessage.Visible = True
                Exit Sub
            End If

            If txtrev_hk.SelectedItem.Value.Trim() = "" Then
                lblErrMessage.Text = "Silakan isi hk koreksi !"
                lblErrMessage.Visible = True
                Exit Sub
            End if
			
			 If cint(txtrev_hk.SelectedItem.Value.Trim()) > 1 Then
                lblErrMessage.Text = "hk koreksi > 1 !"
                lblErrMessage.Visible = True
                Exit Sub
            End if
			
			If txtrev_ket.Text.Trim = "" Then
                lblErrMessage.Text = "Silakan isi keterangan koreksi hk !"
                lblErrMessage.Visible = True
                Exit Sub
            End If
        End If
		
		

		If chk_rev.Checked Then
            ParamName = "AI|LOC|AD|EC|HKA|HKB|KET|CD|UD|UI"          
            ParamValue = Lblattcode.Text & "|" & _
                         strLocation & "|" & _
                         strAttDate & "|" & _
                         LblEmpCode.Text & "|" & _
                         txtHk.Text & "|" & _
                         txtrev_hk.SelectedItem.Value.Trim() & "|" & _
                         txtrev_ket.Text.Trim & "|" & _
                         DateTime.Now() & "|" & _
                         DateTime.Now() & "|" & _
                         strUserId
            Try
                intErrNo = Objok.mtdInsertDataCommon(strOpCd_korek, ParamName, ParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_KOREKSI_UPD&errmesg=" & Exp.Message & "&redirect=PR/trx/PR_trx_DailyAttd_ESTATE.aspx")
            End Try

        End If
		
        ParamName = "AI|Loc|AD|EC|AM|AY|AC|Hk|CD|UD|UI|ST"

        Paramtmp =  Request.Form("txtHk")

        If Lblattcode.Text = "" Then
            Lblattcode.Text = getCode()
        End If

        ParamValue = Lblattcode.Text & "|" & _
                     strLocation & "|" & _
                     strAttDate & "|" & _
                     LblEmpCode.Text & "|" & _
                     StrAMonth & "|" & _
                     StrAYear & "|" & _
                     ddlabsent.SelectedValue & "|" & _
                     Paramtmp & "|" & _
                     DateTime.Now() & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId & "|1"
        Try
            intErrNo = Objok.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_UPD&errmesg=" & Exp.Message & "&redirect=PR/trx/PR_trx_DailyAttd_ESTATE.aspx")
        End Try

        

		if (ddlabsent.SelectedValue.trim() = "K") or (ddlabsent.SelectedValue.trim() = "P2") or (ddlabsent.SelectedValue.trim() = "P4") or (ddlabsent.SelectedValue.trim() = "HM") or (ddlabsent.SelectedValue.trim() = "HB") Then
			Response.Flush()
			Response.Write("<Script Language=""JavaScript"">window.location.href=""PR_Trx_AttdCheckrolllist_Estate.aspx"";window.close(); </Script>")
		end if
    End Sub


    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Flush()
        Response.Write("<Script Language=""JavaScript"">window.location.href=""PR_Trx_AttdCheckrolllist_Estate.aspx"";window.close(); </Script>")
    End Sub

    Sub DelBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_TRX_ATTENDANCE_DEL"
        Dim ParamName As String
        Dim ParamValue As String
        Dim Paramtmp As String = ""
        Dim intErrNo As Integer

		IF StatusPayroll(Cint(StrAMonth),strAYear,strLocation) = "3" Then
		  Exit Sub
		End IF
		
        ParamName = "AI"
        ParamValue = Lblattcode.Text
        Try
            intErrNo = Objok.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_DEL&errmesg=" & Exp.Message & "&redirect=PR/trx/PR_trx_DailyAttd_ESTATE.aspx")
        End Try
        Response.Flush()
        Response.Write("<Script Language=""JavaScript"">window.location.href=""PR_Trx_AttdCheckrolllist_Estate.aspx"";window.close(); </Script>")
    End Sub
	
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
