Imports System

Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl


Public Class PR_trx_FingerException_Estate : Inherits Page

    Protected WithEvents dgEmpList As DataGrid
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents NewEmpBtn As ImageButton
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label
    'New
    Protected WithEvents txtAttdDate As TextBox
    Protected WithEvents ddlEmpType As DropDownList
    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlEmpJob As DropDownList
    Protected WithEvents ddlabsen As DropDownList

    Protected WithEvents dgExpList As DataGrid
	Protected WithEvents ddlEmpMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList
    Protected WithEvents ddlEmpDivlst As DropDownList
	Protected WithEvents ddlEmpTypelst As DropDownList
	Protected WithEvents txtniklst As TextBox
	Protected WithEvents txtnamalst  As TextBox

    Protected objHR As New agri.HR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim Objok As New agri.GL.ClsTrx
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String

    Dim intPRAR As Long
    Dim blnCanDelete As Boolean = False

    Dim objEmpDs As New Object()
    Dim objEmpTypeDs As New Object()
    Dim objEmpDivDs As New Object()
    Dim objEmpJobDs As New Object()

    Dim ListEmp As String
    Dim strDateFmt As String
    Dim strAcceptFormat As String
	Dim intLevel As Integer

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


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
		intLevel = Session("SS_USRLEVEL")
        strDateFmt = Session("SS_DATEFMT")

         If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
         ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = False) or (intLevel =0) Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
                If Not Page.IsPostBack Then
                txtAttdDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
				BindAccYear(Session("SS_SELACCYEAR"))
				ddlEmpMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) 
                BindEmpDiv()
				BindEmpType()
                'BindGrid()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgEmpList.EditItemIndex = -1
        BindGrid()
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
	
    Sub BindEmpDiv()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If
		
		dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpDiv.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDiv.DataTextField = "Description"
        ddlEmpDiv.DataValueField = "BlkGrpCode"
        ddlEmpDiv.DataBind()
		
		ddlEmpDivlst.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDivlst.DataTextField = "Description"
        ddlEmpDivlst.DataValueField = "BlkGrpCode"
        ddlEmpDivlst.DataBind()
      
    End Sub
	
	 Sub BindEmpType()
        Dim strOpCd_EmpType As String = "HR_HR_STP_EMPTYPE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
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

        dr = objEmpTypeDs.Tables(0).NewRow()
        dr("EmpTyCode") = ""
        dr("Description") = "All"
        objEmpTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpType.DataSource = objEmpTypeDs.Tables(0)
        ddlEmpType.DataTextField = "Description"
        ddlEmpType.DataValueField = "EmpTyCode"
        ddlEmpType.DataBind()
        ddlEmpType.SelectedIndex = 0
		
		ddlEmpTypelst.DataSource = objEmpTypeDs.Tables(0)
        ddlEmpTypelst.DataTextField = "Description"
        ddlEmpTypelst.DataValueField = "EmpTyCode"
        ddlEmpTypelst.DataBind()
        ddlEmpTypelst.SelectedIndex = 0

    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        dgEmpList.DataSource = dsData
        dgEmpList.DataBind()
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "PR_PR_TRX_ATTENDANCE_EXP_GET"
        Dim strSrchEmpDiv As String
		Dim strSrchStatus As String
		Dim strSrchEC As String
		Dim strSrchEN As String
		
		Dim strParamName As String
		Dim strParamValue As String
		Dim intErrNo As Integer
      
        strSrchEmpDiv = IIf(ddlEmpDiv.SelectedItem.Value = "0", "", ddlEmpDiv.SelectedItem.Value)
		strSrchStatus = ddlEmpType.SelectedItem.Value.Trim()
		
		strParamName = "LOC|DT|DV|TY|EC|EN"
        strParamValue = strLocation & "|" & _
		                Date_Validation(txtAttdDate.Text, False) & "|" & _
                        strSrchEmpDiv & "|" & _
						strSrchStatus & "|" & _
                        strSrchEC & "|" & _
						strSrchEN 

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objEmpDs
    End Function
	
	Sub BindGridLst()
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadDataLst()
        dgExpList.DataSource = dsData
        dgExpList.DataBind()
    End Sub

    Protected Function LoadDataLst() As DataSet
        Dim strOpCd_Get As String = "PR_PR_TRX_ATTENDANCE_EXPLST_GET"
        Dim strSrchEmpDiv As String
		Dim strSrchStatus As String
		Dim strSrchEC As String
		Dim strSrchEN As String
		
		Dim strParamName As String
		Dim strParamValue As String
		Dim intErrNo As Integer
      
        strSrchEmpDiv = IIf(ddlEmpDiv.SelectedItem.Value = "0", "", ddlEmpDiv.SelectedItem.Value)
		strSrchStatus = ddlEmpType.SelectedItem.Value.Trim()
		strSrchEC = txtniklst.Text.Trim()
		strSrchEN = txtnamalst.Text.Trim()
		
		strParamName = "LOC|AM|AY|DV|TY|EC|EN"
        strParamValue = strLocation & "|" & _
		                ddlEmpMonth.SelectedItem.Value.Trim() & "|" & _
						ddlyear.SelectedItem.Value.Trim() & "|" & _
                        strSrchEmpDiv & "|" & _
						strSrchStatus & "|" & _
                        strSrchEC & "|" & _
						strSrchEN 

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objEmpDs
    End Function

    Sub dgExpList_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
		Dim strOpCd_UPD As String = "PR_PR_TRX_ATTENDANCE_EXPLST_DEL"
        Dim int As Integer = e.Item.ItemIndex
        Dim strParamName As String
        Dim strParamValue As String
        Dim strAI As String
		Dim strEC As String
		Dim strDT As String
        Dim intErrNo As Integer
        Dim EditText As Label

		IF intLevel > 2
		EditText = dgExpList.Items.Item(int).FindControl("lbltgllst")
        strDT = EditText.Text.Trim
		EditText = dgExpList.Items.Item(int).FindControl("lblniklst")
        strEC = EditText.Text.Trim

		strParamName = "AD|EC"
        strParamValue = strDT & "|" & _
		                strEC 



        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_UPD, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_KOREKSI_DEL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgExpList.EditItemIndex = -1
		BindGridLst()
		END IF
		
		
	End Sub

	

    Sub Btnsave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_TRX_ATTENDANCE_EXP_UPD"
        Dim lblEmpCode As Label
        Dim chkabsent As CheckBox
        Dim txt As TextBox
        Dim DDLunt As DropDownList
        Dim i As Integer
        
        Dim Paramtmp As String = ""
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		
		DiM strattdate As String 
		DiM strempcode As String 
		DIM jamin As String
		DIM jamout as String
		DIM KET as String
		DIM PEA as String
		
		
        For i = 0 To dgEmpList.Items.Count - 1

       
			lblEmpCode = dgEmpList.Items.Item(i).FindControl("lbltgl")
			strattdate = lblEmpCode.Text.Trim()
			
			lblEmpCode = dgEmpList.Items.Item(i).FindControl("lblnik")
			strempcode = lblEmpCode.Text.Trim()
			
			lblEmpCode = dgEmpList.Items.Item(i).FindControl("lblin1")
			jamin = lblEmpCode.Text.Trim()
			
			lblEmpCode = dgEmpList.Items.Item(i).FindControl("lblout1")
			jamout = lblEmpCode.Text.Trim()
			
			txt = dgEmpList.Items.Item(i).FindControl("txtalasan")
			KET = txt.text.trim()
			
			txt = dgEmpList.Items.Item(i).FindControl("txtpea")
			PEA = txt.text.trim()
			
			chkabsent = dgEmpList.Items.Item(i).FindControl("Absent")
			
			If (chkabsent.Checked) and (KET <> "") and (PEA <> "") Then
			    
                ParamName = "AD|EC|LOC|IN|OUT|KET|PEA|CD|UD|UI"
                ParamValue = strattdate & "|" & _
				             strempcode & "|" & _
							 strlocation & "|" & _
							 jamin & "|" & _
							 jamout & "|" & _
							 KET & "|" & _
							 PEA & "|" & _
							 DateTime.Now() & "|" & _
                             DateTime.Now() & "|" & _
                             strUserId
				
                Try
                    intErrNo = Objok.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_UPD&errmesg=" & Exp.Message & "&redirect=PR/trx/PR_trx_DailyAttd_ESTATE.aspx")
                End Try
   		     End If
        Next
        srchBtn_Click(Sender, E)
    End Sub

	Sub srchBtn_Clicklst(ByVal sender As Object, ByVal e As System.EventArgs)
        dgExpList.EditItemIndex = -1
        BindGridLst()
    End Sub
	
	Sub Btnprint_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
	
	End Sub
End Class
