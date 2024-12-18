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


Public Class PR_trx_BKMApproval_Estate : Inherits Page

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

    Function GetCheck(ByVal n As String) As Boolean
			Select Case intLevel 
			Case 2 '-- Supervisor
				If rtrim(n) = "3" Then
					Return False
				Else
					Return True
				End If
			Case 3 '--Manager
				If rtrim(n) = "2" Then
					Return False
				Else
					Return True
				End If
			Case > 3
				If rtrim(n)="3" Then
					Return False
				Else
					Return True
				End If
		End Select
    End Function
	
	Function GetEnable(ByVal n As String) As Boolean
		Select Case intLevel 
			Case 2 '-- Supervisor
				If rtrim(n) = "3" Then
					Return True
				Else
					Return False
				End If
			Case 3 '--Manager
				If rtrim(n) = "2" Then
					Return True
				Else
					Return False
				End If
			Case > 3
				Return False
		End Select
    End Function
	
	Function ShowCheck(ByVal n As String) As Boolean
		If n = "A" Then
            Return True
        Else
            Return False
        End If
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
                BindEmpDiv()
				BindType()
                BindGrid()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgEmpList.EditItemIndex = -1
        BindGrid()
    End Sub
	
	Sub BindType()
		Select Case intlevel
			case 2 
				ddlEmpType.Items.Add(New ListItem("Active   / Krani Input", "3"))
				ddlEmpType.Items.Add(New ListItem("Verified / Approved by Asisten", "2"))
				ddlEmpType.Items.Add(New ListItem("Confirm  / Approved by Manager", "1"))
			Case 3
				ddlEmpType.Items.Add(New ListItem("Verified / Approved by Asisten", "2"))
				ddlEmpType.Items.Add(New ListItem("Confirm  / Approved by Manager", "1"))
			Case > 3
				ddlEmpType.Items.Add(New ListItem("Active   / Krani Input", "3"))
				ddlEmpType.Items.Add(New ListItem("Verified / Approved by Asisten", "2"))
				ddlEmpType.Items.Add(New ListItem("Confirm  / Approved by Manager", "1"))
		End Select
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

        ddlEmpDiv.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDiv.DataTextField = "Description"
        ddlEmpDiv.DataValueField = "BlkGrpCode"
        ddlEmpDiv.DataBind()
      
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
        Dim strOpCd_Get As String = "PR_PR_TRX_BKM_MAIN_APPROVAL_LIST"
        Dim strSrchEmpDiv As String
		Dim strSrchStatus As String
		
		Dim strParamName As String
		Dim strParamValue As String
		Dim intErrNo As Integer
      
        strSrchEmpDiv = IIf(ddlEmpDiv.SelectedItem.Value = "0", "", ddlEmpDiv.SelectedItem.Value)
		strSrchStatus = ddlEmpType.SelectedItem.Value.Trim()
		
		strParamName = "DT|DV|LOC|ST"
        strParamValue = Date_Validation(txtAttdDate.Text, False) & "|" & _
                        strSrchEmpDiv & "|" & _
						strLocation & "|" & _
						strSrchStatus
                        

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objEmpDs
    End Function

    Sub EmpLink_Click(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
		If Not E.CommandName.ToString = "Sort" And Not E.CommandName.ToString = "Delete" Then
		   
            Dim lbl As Label
            Dim lbl2 As Label
			Dim intIndex As Integer = E.Item.ItemIndex
            Dim strEmpCode As String
            Dim strEmpName As String
            Dim strDateAtt As String
			Dim strcat As String
			Dim stridx As String
			
			lbl2 = dgEmpList.Items.Item(intIndex).FindControl("lblcat")	
			strcat = lbl2.Text.Trim()
			lbl2 = dgEmpList.Items.Item(intIndex).FindControl("lblidx")	
			stridx = lbl2.Text.Trim()
			lbl = dgEmpList.Items.Item(intIndex).FindControl("lblEmpCode")	
			
			if strcat <> "LBR" then
				if left(lbl.Text.Trim(),3)="BKM" then
					Response.Write("<Script Language=""JavaScript"">pop_Att=window.open(""PR_trx_BKMDet_New_Estate.aspx?redirect=attm&BKMCode=" & lbl.Text.Trim & """, null ,""'pop_Att',width=800,height=500,top=100,left=100,status=yes, resizable=no, scrollbars=yes, toolbar=no, location=no"");pop_Att.focus();</Script>")
				else
					Response.Write("<Script Language=""JavaScript"">pop_Att=window.open(""PR_trx_KTNDet_New_Estate.aspx?redirect=attm&BKMCode=" & lbl.Text.Trim & """, null ,""'pop_Att',width=800,height=500,top=100,left=100,status=yes, resizable=no, scrollbars=yes, toolbar=no, location=no"");pop_Att.focus();</Script>")
				end if
			else
			        Response.Write("<Script Language=""JavaScript"">pop_Att=window.open(""PR_trx_OvertimeDet_Estate.aspx?redirect=attm&OtID=" & stridx & """, null ,""'pop_Att',width=800,height=500,top=100,left=100,status=yes, resizable=no, scrollbars=yes, toolbar=no, location=no"");pop_Att.focus();</Script>")
			end if
		End IF
    End Sub

	

    Sub Btnsave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKM_MAIN_APPROVAL_UPD"
        Dim lblEmpCode As Label
        Dim chkabsent As CheckBox
        Dim txtHk As TextBox
        Dim DDLabs As DropDownList
        Dim DDLunt As DropDownList
        Dim i As Integer
        Dim attcode As String = ""
        Dim Paramtmp As String = ""
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		
		Dim StrBKM As String
		Dim StrID As String
		Dim StrStatus As String
		Dim StrApprove As String = "3"
		Dim Stridx As String
		Dim Strcat As String

        For i = 0 To dgEmpList.Items.Count - 1

            lblEmpCode = dgEmpList.Items.Item(i).FindControl("lblEmpCode")
			StrBKM = lblEmpCode.Text.Trim()
			lblEmpCode = dgEmpList.Items.Item(i).FindControl("lblID")
			StrID = lblEmpCode.Text.Trim()
			lblEmpCode = dgEmpList.Items.Item(i).FindControl("lblStatus")
			StrStatus = lblEmpCode.Text.Trim()
			chkabsent = dgEmpList.Items.Item(i).FindControl("Absent")
			
			 lblEmpCode = dgEmpList.Items.Item(i).FindControl("lblidx")
			 Stridx = lblEmpCode.Text.Trim()
			 lblEmpCode = dgEmpList.Items.Item(i).FindControl("lblcat")
			 Strcat = lblEmpCode.Text.Trim()
			
    
		IF StrID="A" Then
            If chkabsent.Enabled And chkabsent.Checked Then
				Select Case intLevel
				Case 2
					If StrStatus="3" Then 
						StrApprove="2" 
						Else 
						StrApprove= StrStatus 
					End If
				Case 3
					If StrStatus="2" Then 
						StrApprove="1" 
						Else 
						StrApprove= StrStatus 
					End If
				End Select
			    
                ParamName = "BKM|UD|UI|ST|LOC|CAT"
                ParamValue = StrBKM & "|" & _
				             DateTime.Now() & "|" & _
							 strUserId & "|" & _
							 StrApprove & "|" & _
							 strlocation & "|" & _
							 Strcat
				
                Try
                    intErrNo = Objok.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_UPD&errmesg=" & Exp.Message & "&redirect=PR/trx/PR_trx_DailyAttd_ESTATE.aspx")
                End Try
            End If
		End If
        Next
        srchBtn_Click(Sender, E)
    End Sub


End Class
