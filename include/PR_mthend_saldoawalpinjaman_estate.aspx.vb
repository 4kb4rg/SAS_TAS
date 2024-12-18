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

Public Class PR_mthend_saldoawalpinjaman_estate : Inherits Page

    Protected WithEvents rbAllEmp As RadioButton
    Protected WithEvents rbSelectedEmp As RadioButton
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents lblCompleteSetup As Label
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents dgprocess As DataGrid

    Protected WithEvents ddlEmpCode As DropDownList
    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlmandor As DropDownList

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
                ddlMonth.SelectedIndex = Month(Now()) - 1
                BindAccYear(Year(Now()))
                BindEmpDiv()
				BindEmployee("")
                BindMandoran("")
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
		Dim strAM as String
		
		If ddlMonth.SelectedItem.Value < 10 Then
            Strdate2 = ddlyear.SelectedItem.Value & "0" & ddlMonth.SelectedItem.Value 
			strAm = "0" & ddlMonth.SelectedItem.Value
        Else
            Strdate2 = ddlyear.SelectedItem.Value & ddlMonth.SelectedItem.Value 
			strAm =  ddlMonth.SelectedItem.Value
        End If
		
		strsrc = " AND c.IDDiv like '" & ed & "%'  AND  A.EmpCode Not in (Select empcode from HR_TRX_EMPRESIGN where status='1' and " & Strdate2 & " > convert(char(6),efektifdate,112)) "



        strParamName =  "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & strAm & "|" & ddlyear.SelectedItem.Value & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "'" & strsrc & "|ORDER BY C.IdDiv,A.EmpName"

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
	

    Sub BindMandoran(ByVal strdivcode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
		Dim strAM as String
		
		If ddlMonth.SelectedItem.Value < 10 Then
            strAm = "0" & ddlMonth.SelectedItem.Value
        Else
            strAm =  ddlMonth.SelectedItem.Value
        End If


        strParamName =  "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & strAm & "|" & ddlyear.SelectedItem.Value & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strdivcode & "%' AND (F.isMandor<>'0')|ORDER BY A.EmpName"

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

        ddlmandor.DataSource = objEmpCodeDs.Tables(0)
        ddlmandor.DataTextField = "_Description"
        ddlmandor.DataValueField = "EmpCode"
        ddlmandor.DataBind()
    End Sub

    Sub SearchBtn_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        HitView()
    End Sub

    Sub SubmitBtn_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        SaveView()
        HitView()
    End Sub

    Sub HitView()
        Dim strOpCdSP As String = "PR_PR_MTHEND_SMALLPAYROLL_AWAL_GET"
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

        ParamName = "ACCMONTH|ACCYEAR|LOCCODE|F_EMPCODE|F_DIVCODE|F_MCODE"

        ParamValue = strMn & "|" & _
                        strYr & "|" & _
                        strLocation & "|" & _
                        ddlEmpCode.SelectedItem.Value.Trim & "|" & _
                        ddlEmpDiv.SelectedItem.Value.Trim & "|" & _
                        ddlmandor.SelectedItem.Value.Trim

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_SMALLPAYROLL_AWAL_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgprocess.DataSource = objDataSet.Tables(0)
        dgprocess.DataBind()

    End Sub

    Sub SaveView()
        Dim strOpCd As String = "PR_PR_MTHEND_SMALLPAYROLL_AWAL_UPD"
        Dim strMn As String
        Dim strYr As String
        Dim lbl_ec As Label
        Dim txt_hk As TextBox
        Dim txt_lembur As TextBox
        Dim txt_premi As TextBox
        Dim txt_denda As TextBox
        Dim txt_pinjaman As TextBox
        Dim txt_tot As String

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        If ddlMonth.SelectedItem.Value < 10 Then
            strMn = "0" & ddlMonth.SelectedItem.Value.Trim
        Else
            strMn = ddlMonth.SelectedItem.Value.Trim
        End If

        strYr = ddlyear.SelectedItem.Value.Trim

        For i = 0 To dgprocess.Items.Count - 1
            lbl_ec = dgprocess.Items.Item(i).FindControl("dgProcess_lbl_ec")
            txt_hk = dgprocess.Items.Item(i).FindControl("dgProcess_lbl_hkd")
            txt_lembur = dgprocess.Items.Item(i).FindControl("dgProcess_lbl_lbr")
            txt_premi = dgprocess.Items.Item(i).FindControl("dgProcess_lbl_prm")
            txt_denda = dgprocess.Items.Item(i).FindControl("dgProcess_lbl_dnd")
            txt_pinjaman = dgprocess.Items.Item(i).FindControl("dgProcess_lbl_pjm")

            

            If (txt_pinjaman.Text.Trim <> "") Then
                If txt_hk.Text.Trim = "" Then txt_hk.Text = "0"
                If txt_lembur.Text.Trim = "" Then txt_lembur.Text = "0"
                If txt_premi.Text.Trim = "" Then txt_premi.Text = "0"
                If txt_denda.Text.Trim = "" Then txt_denda.Text = "0"
                If txt_pinjaman.Text.Trim = "" Then txt_pinjaman.Text = "0"

                ParamName = "ACCMONTH|ACCYEAR|LOC|EC|UI|HK|LEMBUR|PREMI|DENDA|PINJAMAN"

                ParamValue = strMn & "|" & _
                             strYr & "|" & _
                             strLocation & "|" & _
                             lbl_ec.Text.Trim & "|" & _
                             strUserId & "|" & _
                             txt_hk.Text & "|" & _
                             txt_lembur.Text & "|" & _
                             txt_premi.Text & "|" & _
                             txt_denda.Text & "|" & _
                             txt_pinjaman.Text


                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_SMALLPAYROLL_AWAL_UPD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next

    End Sub



End Class
