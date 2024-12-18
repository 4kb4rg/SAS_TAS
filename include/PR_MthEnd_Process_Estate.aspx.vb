Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PR.clsMthEnd
Imports agri.Admin.clsShare
Imports agri.PWSystem.clsConfig


Public Class PR_mthend_Process_Estate : Inherits Page

    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblSuccess As Label
    Protected WithEvents lblFailed As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents btnProceed As ImageButton
	Protected WithEvents dgViewJournal As DataGrid
	Protected WithEvents dgValidasi  As DataGrid 

    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList

    Dim objOk As New agri.GL.ClsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objMthEnd As New agri.PR.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
    Dim objLangCapDs As New DataSet()

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")

        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            btnProceed.Attributes("onclick") = "javascript:return confirm('Anda yakin akan melakukan Proses Tutup Buku Payroll ?');"
            lblErrMessage.Text = ""
			lblErrMessage.visible = False

            If Not Page.IsPostBack Then
                ddlMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))
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
            intErrNo = objOk.mtdGetDataCommon(strOpCd, _
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

	Function cekvalidasi(ByVal mn As String,ByVal yr As String) As Integer
        Dim strOpCd As String = "PR_PR_MTHEND_TUTUPBUKU_PROCESS_SP_VALID"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objCodeDs As New Object()

        strParamName = "ACCMONTH|ACCYEAR|LOC"
        strParamValue = mn & "|" & yr & "|" & strLocation
                       
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_TUTUPBUKU_PROCESS_SP_VALID&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

		If objCodeDs.Tables(0).Rows.Count > 0 Then
			dgValidasi.DataSource = Nothing
			dgValidasi.DataBind()
			dgValidasi.DataSource = objCodeDs.Tables(0)
			dgValidasi.DataBind()
		end if
		
		Return objCodeDs.Tables(0).Rows.Count
		
    End Function
	
	Sub viewerror()
		Dim strOpCd_DKtr As String = "PR_PR_MTHEND_TUTUPBUKU_PROCESS_SP_CEK" '"PR_PR_MTHEND_TUTUPBUKU_PROCESS_SP"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
		
		If ddlMonth.SelectedItem.Value < 10 Then
            strMn = "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strMn = ddlMonth.SelectedItem.Value.Trim
        End If

        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "ACCMONTH|ACCYEAR|COMP|LOC|UI"
        ParamValue = strMn & "|" & strYr & "|" & strCompany & "|" & strLocation & "|" & strUserId

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_TUTUPBUKU_PROCESS_SP_CEK&errmesg=" & Exp.Message & "&redirect=")
        End Try

		If objDataSet.Tables(0).Rows.Count > 0 Then
            lblErrMessage.Text = "Silakan cek setting Alokasi Pekerjaan , berikut daftar jurnal yang COA nya tidak lengkap"
			lblErrMessage.Visible = True
			UserMsgBox(Me, lblErrMessage.Text)
			dgViewJournal.DataSource = objDataSet
			dgViewJournal.DataBind()
	    else
		    dgViewJournal.DataSource = Nothing
			dgViewJournal.DataBind()
	    end if
		
	End sub
	
    Sub btnProceed_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_DKtr As String = "PR_PR_MTHEND_TUTUPBUKU_PROCESS_SP_ALL" '"PR_PR_MTHEND_TUTUPBUKU_PROCESS_SP"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String

		IF StatusPayroll(cint(ddlMonth.SelectedItem.Value.Trim),ddlyear.SelectedItem.Value.Trim,strLocation) = "3" Then
		  Exit Sub
		ELSE 
		  UpdateStatusPayroll(cint(ddlMonth.SelectedItem.Value.Trim),ddlyear.SelectedItem.Value.Trim,strLocation)
		End IF
		
        If ddlMonth.SelectedItem.Value < 10 Then
            strMn = "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strMn = ddlMonth.SelectedItem.Value.Trim
        End If

        strYr = ddlyear.SelectedItem.Value.Trim
		
        If cekvalidasi(strMn, strYr) <> 0 Then
            lblErrMessage.Text = "Silakan Cek Validasi berikut"
            UserMsgBox(Me, lblErrMessage.Text)
            Exit Sub 'optional mau ditutup / nda ?
        End If
			

        ParamName = "ACCMONTH|ACCYEAR|COMP|LOC|UI"
        ParamValue = strMn & "|" & strYr & "|" & strCompany & "|" & strLocation & "|" & strUserId

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_TUTUPBUKU_PROCESS_SP&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count = 0 Then
            lblErrMessage.Text = "No Record Created"
        ElseIf objDataSet.Tables(0).Rows.Count > 0 Then
            lblErrMessage.Text = "Process Success"
        Else
            lblErrMessage.Text = "Process Failed"
        End If

        UserMsgBox(Me, lblErrMessage.Text)
		
        viewerror()
		
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
      
       
        ParamName = "MN|YR|LOC|S1|S2|S3|S4|S5"
        ParamValue = mn & "|" & _
                     yr & "|" & _
                     loc & "||||1|" 
        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCdSP, ParamName, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_MTHEND_UPD&errmesg=" & Exp.Message & "&redirect=")
        End Try

    End Sub

End Class
