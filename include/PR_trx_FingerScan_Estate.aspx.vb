Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports agri.PR
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap



Public Class PR_trx_FingerScan_Estate : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label
	
	Protected WithEvents txtDateStart As TextBox
    Protected WithEvents txtDateEnd As TextBox
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtEmpName As TextBox
	Protected WithEvents ddlEmpDiv As DropDownList
	Protected WithEvents ddlEmpType As DropDownList

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SortExpression As Label
	
	Protected WithEvents ref As HiddenField


    Protected objPRTrx As New agri.PR.clsTrx()
    Protected objHRTrx As New agri.HR.clsTrx()
    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim Objok As New agri.GL.ClsTrx

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Dim strLocType As String
	Dim strDateFmt As String

    Dim objEmpTypeDs As New Object()
    Dim objEmpDivDs As New Object()
    Dim objEmpJobDs As New Object()

    Dim strAcceptFormat As String
    Dim ArrayHoliday As New ArrayList()
    Dim ArrayWeekend As New ArrayList()


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        strLocType = Session("SS_LOCTYPE")
		strDateFmt = Session("SS_DATEFMT")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxAttendance), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            If SortExpression.Text = "" Then
                SortExpression.Text = "EmpName"
            End If
            ref.Value = Session("ATT")
            If Not Page.IsPostBack Then
				txtDateStart.Text = objGlobal.GetShortDate(strDateFmt, Now())
				txtDateEnd.Text = objGlobal.GetShortDate(strDateFmt, Now())
                BindEmpDiv()
				BindEmpType()
                BindGrid()
				
            End If
        End If

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

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        BindGrid()
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsTemp As DataSet

        dsTemp = LoadAttData()
        dgLine.DataSource = dsTemp
        dgLine.DataBind()
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
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
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
        ddlEmpDiv.SelectedIndex = 0

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

    End Sub
    
    Protected Function LoadAttData() As DataSet
        Dim strOpCd_Get As String = "PR_PR_TRX_FINGER_SCAN_GET"
        Dim strSrchEmpCode As String
        Dim strSrchEmpName As String
        Dim strSrchEmpDiv As String
		Dim strSrchEmpType As String
        
		Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        
		Dim intCnt As Integer
        
		Dim strSortExpression As String
        Dim objEmpDs As New Object()
        Dim ObjAtt As New Object()
        Dim StrEmp As String
        Dim ListEmp As String
        Dim strPrd as String = "" 

       'GET Data Emp
        strSrchEmpCode = IIf(txtEmpCode.Text = "", "", txtEmpCode.Text)
        strSrchEmpName = IIf(txtEmpName.Text = "", "", txtEmpName.Text)
        strSrchEmpDiv  = IIf(ddlEmpDiv.SelectedItem.Value = "0", "", ddlEmpDiv.SelectedItem.Value)
        strSrchEmpType = IIf(ddlEmpType.SelectedItem.Value = "", "", ddlEmpType.SelectedItem.Value)
		
        
        strParamName = "LOC|SD|ED|DV|EC|EN|ET"
        strParamValue = strlocation & "|" & _
		                Date_Validation(Trim(txtDateStart.Text), False) & "|" & _
						Date_Validation(Trim(txtDateEnd.Text), False) & "|" & _
						strSrchEmpDiv & "|" & _
                        strSrchEmpCode 	& "|" & _					
						strSrchEmpName  & "|" & _ 
						strSrchEmpType
        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_FINGER_SCAN_GET&errmesg=" & Exp.Message )
        End Try

        Return objEmpDs
    End Function
 

    Sub dgLineBindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")

            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If

        End If
    End Sub

    Sub RefrehBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        srchBtn_Click(Sender, E)
    End Sub

    Sub GenerateBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
	 Dim strOpCd_Upd As String = "PR_PR_TRX_FINGER_SCAN_GENERATE"
	 Dim ParamName As String
     Dim ParamValue As String
	 Dim strSrchEmpCode As String
     Dim strSrchEmpName As String
     Dim strSrchEmpDiv As String
	 Dim intErrNo As Integer
        
		strSrchEmpCode = IIf(txtEmpCode.Text = "", "", txtEmpCode.Text)
        strSrchEmpName = IIf(txtEmpName.Text = "", "", txtEmpName.Text)
        strSrchEmpDiv  = IIf(ddlEmpDiv.SelectedItem.Value = "0", "", ddlEmpDiv.SelectedItem.Value)
      
        ParamName = "LOC|DV|SD|ED|EC|EN"
        ParamValue = strLocation & "|" & _
		             strSrchEmpDiv & "|" & _
                     Date_Validation(Trim(txtDateStart.Text), False) & "|" & _
					 Date_Validation(Trim(txtDateEnd.Text), False) & "|" & _
                     strSrchEmpCode & "|" & _					
					 strSrchEmpName 
        Try
            intErrNo = Objok.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_UPD&errmesg=" & Exp.Message & "&redirect=PR/trx/PR_trx_DailyAttd_ESTATE.aspx")
        End Try
		Response.Flush()
		Response.Write("<Script Language=""JavaScript"">window.location.href=""PR_Trx_AttdCheckrolllist_Estate.aspx"";window.close(); </Script>")
	End Sub

	Sub on_BindGrid(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) 
    	
		 If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
		 Dim lbl As Label
		 Dim lbl2 As Label
		 
		 lbl = CType(e.Item.FindControl("dgin"), Label)
		 if (lbl.text = "00:00") 
			lbl.text = "-"
		 end if
		 
		 lbl = CType(e.Item.FindControl("dgout"), Label)
		 if (lbl.text = "00:00") 
			lbl.text = "-"
		 end if
		 
		 lbl = CType(e.Item.FindControl("dgin2"), Label)
		 if (lbl.text = "00:00") 
			lbl.text = "-"
		 end if
		 
		 lbl = CType(e.Item.FindControl("dgout2"), Label)
		 if (lbl.text = "00:00") 
			lbl.text = "-"
		 end if
		 
		 lbl = CType(e.Item.FindControl("dgin3"), Label)
		 if (lbl.text = "00:00") 
			lbl.text = "-"
		 end if
		 
		 lbl = CType(e.Item.FindControl("dgout3"), Label)
		 if (lbl.text = "00:00") 
			lbl.text = "-"
		 end if
		 
		 
		 end if
		 
	
	End Sub
	
	Sub ExportView()
			
        Dim cAccMonth As String = replace(txtDateStart.Text & "-" & txtDateEnd.Text,"/","") 
		
		Dim strSrchEmpDiv As String = IIf(ddlEmpDiv.SelectedItem.Value = "0", "", ddlEmpDiv.SelectedItem.Value)

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=FingerScan-" & rtrim(strSrchEmpDiv) & "-" & rtrim(cAccMonth) &  ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgLine.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
    
	Sub ExportBtn_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        ExportView() 
    End Sub
	
End Class
