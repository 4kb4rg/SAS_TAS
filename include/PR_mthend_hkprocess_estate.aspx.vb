Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient


Imports agri.HR
Imports agri.PR
Imports agri.GlobalHdl
Imports agri.PWSystem

Public Class PR_mthend_hkprocess : Inherits Page

    Protected WithEvents rbAllEmp As RadioButton
    Protected WithEvents rbSelectedEmp As RadioButton
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents lblCompleteSetup As Label
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList
    Protected WithEvents lblErrMessage As Label
	Protected WithEvents cbGenIssue As CheckBox
	Protected WithEvents btnIssue As ImageButton
	Protected WithEvents dgViewCek  As DataGrid 


    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()


    Dim objOk As New agri.GL.ClsTrx()
    Dim objHR As New agri.HR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objPR As New agri.PR.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim objEmpDs As New Object()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
    Dim strAcceptFormat As String
	

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        intPRAR = Session("SS_PRAR")

        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
			btnIssue.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnIssue).ToString())
			btnIssue.Attributes("onclick") = "javascript:return ConfirmAction('generate stock issue');"

            lblErrMessage.Text = ""
            If Not Page.IsPostBack Then
                ddlMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))
                BindEmployee()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_GRList.aspx")
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

    Function GetCutOff(ByVal yr As String) As Integer
        Dim strOpCd As String = "PR_PR_STP_CUTOFF_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objCodeDs As New Object()

        strParamName = "SEARCH"
        strParamValue = "AND A.LocCode = '" & strLocation & "' AND A.COYear='" & yr & "'"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_CUTOFF_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objCodeDs.Tables(0).Rows.Count

    End Function

    Function cekbjr(ByVal mn As String,ByVal yr As String) As Integer
        Dim strOpCd As String = "PR_PR_STP_BLOK_BJR_COMPARE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objCodeDs As New Object()

        strParamName = "LOC|MN|YR"
        strParamValue = strLocation & "|" & mn & "|" & yr
                       
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_BJR_COMPARE&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

		Return Trim(objCodeDs.Tables(1).Rows(0).Item("Msg"))
		
    End Function

    Sub BindEmployee()
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
		Dim Strdate2 AS String 
		Dim strsrc As String
		Dim strAM as String
		
		If ddlMonth.SelectedItem.Value < 10 Then
            Strdate2 = ddlyear.SelectedItem.Value & "0" & ddlMonth.SelectedItem.Value 
			strAM    = "0" & ddlMonth.SelectedItem.Value 
        Else
            Strdate2 = ddlyear.SelectedItem.Value & ddlMonth.SelectedItem.Value 
			strAM   =  ddlMonth.SelectedItem.Value 
        End If
		
		strsrc = " AND  A.EmpCode Not in (Select empcode from HR_TRX_EMPRESIGN where status='1' and " & Strdate2 & " > convert(char(6),efektifdate,112)) "



        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & strAM & "|" & ddlyear.SelectedItem.Value.trim()  & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "'" & strsrc & "|ORDER BY A.EmpName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description"))
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = ""
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmployee.DataSource = objEmpCodeDs.Tables(0)
        ddlEmployee.DataTextField = "_Description"
        ddlEmployee.DataValueField = "EmpCode"
        ddlEmployee.DataBind()
        ddlEmployee.SelectedIndex = intSelectedIndex

    End Sub

    Sub Check_Clicked(ByVal Sender As Object, ByVal E As EventArgs)
        If rbSelectedEmp.Checked Then
            ddlEmployee.Enabled = True
        Else
            ddlEmployee.Enabled = False
        End If
    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdSP As String = "PR_PR_MTHEND_HK_PROCESS_SP"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim strEc As String = ""

        IF StatusPayroll(cint(ddlMonth.SelectedItem.Value.Trim),ddlyear.SelectedItem.Value.Trim,strLocation) = "3" Then
		  Exit Sub
		End IF
		

        If ddlMonth.SelectedItem.Value < 10 Then
            strMn = "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strMn = ddlMonth.SelectedItem.Value.Trim
        End If

        strYr = ddlyear.SelectedItem.Value.Trim


        If ddlEmployee.Enabled Then
            If ddlEmployee.Text = "" Then
                lblErrMessage.Text = "Silakan pilih Employee code"
                UserMsgBox(Me, lblErrMessage.Text)
                Exit Sub
            End If
            strEc = ddlEmployee.SelectedItem.Value.Trim
        Else
            strEc = ""
        End If

        If GetCutOff(strYr) = 0 Then
            lblErrMessage.Text = "Silakan isi setup cut off"
            UserMsgBox(Me, lblErrMessage.Text)
            Exit Sub
        End If

        If cekbjr(RTrim(strMn),RTrim(strYr)) = 0 Then
            lblErrMessage.Text = "Silakan lengkapi bjr di payroll setup"
             UserMsgBox(Me, lblErrMessage.Text)
            Exit Sub
        End If

        ParamName = "ACCMONTH|ACCYEAR|LOC|UI|EC"
        ParamValue = strMn & "|" & strYr & "|" & strLocation & "|" & strUserId & "|" & strEc

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_HK_PROCESS_SP&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count = 0 Then
            lblErrMessage.Text = "No Record Created"
        ElseIf objDataSet.Tables(0).Rows.Count > 0 Then
            lblErrMessage.Text = "Process Success"
        Else
            lblErrMessage.Text = "Process Failed"
        End If


        UserMsgBox(Me, lblErrMessage.Text)

    End Sub
	
	Sub ddlEmpDiv_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindEmployee()
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
	
	Function CekStock(Byval mn as String,Byval yr as String,Byval loc as String)as Integer
        Dim strOpCdSP As String = "IN_CLSTRX_STOCKISSUE_FROM_BKM_CEK"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
		Dim i as Integer
      
       
        ParamName = "ACCMONTH|ACCYEAR|LOCCODE"
        ParamValue = mn & "|" & _
                     yr & "|" & _
                     loc
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKISSUE_FROM_BKM_CEK&errmesg=" & Exp.Message & "&redirect=")
        End Try

			dgViewCek.DataSource = Nothing
			dgViewCek.DataBind()
			
		If objDataSet.Tables(0).Rows.Count > 0 Then
        		
				UserMsgBox(Me, "Cek Transaksi Stock Transfer Divisi dan BKM Pemakaian Bahan")
				dgViewCek.DataSource = objDataSet.Tables(0)
				dgViewCek.DataBind()
				i = 0
		Else
		        i = 1 
		end if
		
     Return i
	 End Function
	
	 Sub btnIssue_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode_GenSI As String
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        
	
		
		If cbGenIssue.Checked = True Then
			strOpCode_GenSI = "IN_CLSTRX_STOCKISSUE_FROM_BKM_ADD"

			strParamName = "LOCCODE|ACCYEAR|ACCMONTH|USERID"
			strParamValue = Trim(strLocation) & _
							"|" & ddlyear.SelectedItem.Value.Trim & _
							"|" & ddlMonth.SelectedItem.Value.Trim & _
							"|" & Trim(strUserId)

			Try
				intErrNo = objOk.mtdInsertDataCommon(strOpCode_GenSI, _
														strParamName, _
														strParamValue)

			Catch Exp As System.Exception
				Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
			End Try
			
			UserMsgBox(Me, "Auto Generate Stock Issue Succeed")
		    CekStock(ddlMonth.SelectedItem.Value.Trim,ddlyear.SelectedItem.Value.Trim,Trim(strLocation)) 
			
		Else
			UserMsgBox(Me, "Please checked the option of Auto Generate Stock Issue")
		End If
		
		
    End Sub
	
	Sub ExportView()
			
        Dim cAccMonth As String = ddlyear.SelectedItem.Value.Trim & ddlMonth.SelectedItem.Value.Trim

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=StkIssueBKM-" & cAccMonth & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgViewCek.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	
	Sub ExportBtn_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        ExportView() 
    End Sub

	Sub GenGR_SPKBtn_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strOpCode_GenSI As String
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        
	
		
		If cbGenIssue.Checked = True Then
			strOpCode_GenSI = "IN_CLSTRX_GOODSRECEIVE_FROM_BKM_ADD"

			strParamName = "LOCCODE|ACCYEAR|ACCMONTH|USERID"
			strParamValue = Trim(strLocation) & _
							"|" & ddlyear.SelectedItem.Value.Trim & _
							"|" & ddlMonth.SelectedItem.Value.Trim & _
							"|" & Trim(strUserId)

			Try
				intErrNo = objOk.mtdInsertDataCommon(strOpCode_GenSI, _
														strParamName, _
														strParamValue)

			Catch Exp As System.Exception
				Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() )
			End Try
			
			UserMsgBox(Me, "Auto Generate SPK Goods Receive Succeed")
		Else
			UserMsgBox(Me, "Please checked the option of Auto Generate Stock Issue")
		End If
		
    End Sub	
End Class
