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

Public Class PR_trx_koreksibasisEmp_estate : Inherits Page

    Protected WithEvents rbAllEmp As RadioButton
    Protected WithEvents rbSelectedEmp As RadioButton
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents lblCompleteSetup As Label
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents dgprocess As DataGrid
	Protected WithEvents dgProcessKoreksi As DataGrid

    Protected WithEvents ddlEmpCode As DropDownList
    Protected WithEvents ddlEmpDiv As DropDownList
  
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
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")

        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            lblErrMessage.Text = ""
            If Not Page.IsPostBack Then
                ddlMonth.SelectedIndex = Cint(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))
                BindEmpDiv()
				BindEmployee("")
				HitView()
				HitViewKoreksi()
            End If
        End If
    End Sub

	Sub bindemp_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		BindEmployee(ddlEmpDiv.SelectedItem.Value.Trim)
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

    Sub BindEmpDiv()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim objEmpDivDs As New Object

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
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
	
	Sub BindEmployee(byval ed as String)
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
		Dim strAm as String
		
	        Strdate2 = ddlyear.SelectedItem.Value & ddlMonth.SelectedItem.Value 
		
		
		strsrc = " AND c.IDDiv like '" & ed & "%'  AND  A.EmpCode Not in (Select empcode from HR_TRX_EMPRESIGN where status='1' and " & Strdate2 & " > convert(char(6),efektifdate,112)) "



        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & ddlMonth.SelectedItem.Value & "|" & ddlyear.SelectedItem.Value &"|AND A.Status='1' AND A.LocCode = '" & strLocation & "'" & strsrc & "|ORDER BY C.IdDiv,A.EmpName"

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
        dr("_Description") = "All"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpCode.DataSource = objEmpCodeDs.Tables(0)
        ddlEmpCode.DataTextField = "_Description"
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intSelectedIndex

    End Sub
	
    Sub SearchBtn_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        HitView()
		HitViewKoreksi()
    End Sub

    Sub HitView()
        Dim strOpCdSP As String = "PR_PR_TRX_KOREKSI_BASIS"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "AM|AY|LOC|DIV|EMP"

        ParamValue = strMn & "|" & _
                     strYr & "|" & _
                     strLocation & "|" & _
					 ddlEmpDiv.SelectedItem.Value.Trim & "|" & _
                     ddlEmpCode.SelectedItem.Value.Trim 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_SMALLPAYROLL_AWAL_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgprocess.DataSource = objDataSet.Tables(0)
        dgprocess.DataBind()

    End Sub
	
	Sub HitViewKoreksi()
        Dim strOpCdSP As String = "PR_PR_TRX_KOREKSI_BASIS_GET"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "AM|AY|LOC|DIV"

        ParamValue = strMn & "|" & _
                     strYr & "|" & _
                     strLocation & "|" & _
					 "" & "|" 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_SMALLPAYROLL_AWAL_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

		dgProcessKoreksi.DataSource = Nothing
        dgProcessKoreksi.DataBind()
        dgProcessKoreksi.DataSource = objDataSet.Tables(0)
        dgProcessKoreksi.DataBind()

    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs) 
        Dim strOpCd As String = "PR_PR_TRX_KOREKSI_BASIS_ADD"
        Dim strMn As String
        Dim strYr As String
        Dim strEC As String
        Dim strDT As String
        Dim strJJG As String
        Dim strBTugas As String
        Dim strBasis As String		
	    Dim lbl As Label
		Dim chk as CheckBox
        Dim tcek As Boolean = False

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        For i = 0 To dgprocess.Items.Count - 1
		    Chk = dgprocess.Items.Item(i).FindControl("dgProcessKoreksi") 
			IF (Chk.Checked = True AND tcek = False) or (Chk.Checked = False AND tcek = True) THEN 
            lbl = dgprocess.Items.Item(i).FindControl("dgProcessAM")
			strMN = lbl.Text.Trim()
            lbl = dgprocess.Items.Item(i).FindControl("dgProcessAY")
			strYR = lbl.Text.Trim()
            lbl = dgprocess.Items.Item(i).FindControl("dgProcessEC")
			strEC= lbl.Text.Trim()
            lbl = dgprocess.Items.Item(i).FindControl("dgProcessDt")
			strDT= lbl.Text.Trim()
            lbl = dgprocess.Items.Item(i).FindControl("dgProcessJJG")
			strJJG= lbl.Text.Trim()
            lbl = dgprocess.Items.Item(i).FindControl("dgProcessBTugas")
			strBTugas = lbl.Text.Trim()
			lbl = dgprocess.Items.Item(i).FindControl("dgProcessIsBasis")
			strBasis = lbl.Text.Trim()
			
			

            ParamName = "AM|AY|LOC|EC|PD|JJG|BT|IS"
            ParamValue = strMn & "|" & _
                         strYr & "|" & _
                         strLocation & "|" & _
                         strEC & "|" & _
                         strDT & "|" & _
                         strJJG & "|" & _
                         strBTugas & "|" & _
                         iif(Chk.Checked=True,"0","1")


                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_SMALLPAYROLL_AWAL_UPD&errmesg=" & Exp.Message & "&redirect=")
                End Try
			
				IF (Chk.Checked = True AND Tcek=False) Then
					tcek = True
				ELSE 
					tcek = False	
				END IF			
				
            End If

        Next
		
	HitViewKoreksi()	
	End Sub

	Sub dgpay_BindGrid(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) 
		
		If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
		 Dim chk As Checkbox
		 Dim lbl As Label
		 
		 lbl = CType(e.Item.FindControl("dgProcessIsBasis"), Label)
		 chk = CType(e.Item.FindControl("dgProcessKoreksi"), Checkbox)
		 
		 IF lbl.Text.Trim() = "1" Then
			chk.Enabled = True
		 Else
		 	chk.Enabled = False
		 End IF
		 
		 
		End If
    End Sub
	
	Sub dgkoreksi_BindGrid(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) 
		
		If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
		 Dim lbldel As LinkButton
		 Dim lbl As Label
		 
		 lbldel = CType(e.Item.FindControl("lbDelete"), LinkButton)
		 lbl = CType(e.Item.FindControl("dgProcessKoreksiIsBasis"), Label)
		 
		 IF lbl.Text.Trim() = "1" Then
			lbldel.visible = True
		 Else
		 	lbldel.visible = False
		 End IF
		End If
    End Sub
	
	Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Status As String = "PR_PR_TRX_KOREKSI_BASIS_DEL"
        Dim intErrNo As Integer
        Dim strParam As String
        Dim strValue As String
        Dim lbl As Label
        Dim strEC As String
        Dim strPD As String
		Dim strAM As String
        Dim strAY As String

		
		
        dgProcessKoreksi.EditItemIndex = CInt(E.Item.ItemIndex)
		
        lbl = dgProcessKoreksi.Items.Item(CInt(E.Item.ItemIndex)).FindControl("dgProcessKoreksiAM")
        strAM = lbl.Text.Trim()
		lbl = dgProcessKoreksi.Items.Item(CInt(E.Item.ItemIndex)).FindControl("dgProcessKoreksiAY")
        strAY = lbl.Text.Trim()
		lbl = dgProcessKoreksi.Items.Item(CInt(E.Item.ItemIndex)).FindControl("dgProcessKoreksiEC")
        strEC = lbl.Text.Trim()
		lbl = dgProcessKoreksi.Items.Item(CInt(E.Item.ItemIndex)).FindControl("dgProcessKoreksiDt")
        strPD = lbl.Text.Trim()
		
	
        strParam = "AM|AY|LOC|EC|PD"
        strValue = strAM & "|" & strAY & "|" & strlocation & "|" & strEC & "|" & strPD 

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Status, strParam, strValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MAIN_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try
		
        dgProcessKoreksi.EditItemIndex = -1
        HitViewKoreksi()	
    End Sub

End Class
