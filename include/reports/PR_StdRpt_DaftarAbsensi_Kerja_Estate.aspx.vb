Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services

Public Class PR_StdRpt_DaftarAbsensi_Kerja_Estate : Inherits Page
    Protected RptSelect As UserControl

    Dim objOk As New agri.GL.ClsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
	Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblLocation As Label


	Protected WithEvents dgpay As DataGrid
	Protected WithEvents ddlbulan As DropDownList
    Protected WithEvents ddltahun As DropDownList
	Protected WithEvents ddlDiv As DropDownList
	Protected WithEvents ddlEmp As DropDownList
	Protected WithEvents ddlMandor As DropDownList

    Dim objLangCapDs As New Object()
    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strPhyMonth As String
    Dim strPhyYear As String


    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Dim strDateFmt As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strPhyMonth = Session("SS_PHYMONTH")
        strPhyYear = Session("SS_PHYYEAR")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
				ddlbulan.SelectedIndex = Month(Now()) - 1
				BindAccYear(Year(Now()))
                BindDivisiCode()
                ddldivisi_OnSelectedIndexChanged(Sender, E)
            End If
        End If

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

   		ddltahun.DataSource = objAccYearDs.Tables(0)
        ddltahun.DataValueField = "AccYear"
        ddltahun.DataTextField = "UserName"
        ddltahun.DataBind()
        ddltahun.SelectedIndex = intSelectedIndex - 1
		
    End Sub

    Sub BindDivisiCode()
        Dim strOpCd As String = "PR_PR_STP_DIVISICODE_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0

        Dim dr As DataRow

        ParamName = "SEARCH|SORT"
        ParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "All"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddldiv.DataSource = objDs.Tables(0)
        ddldiv.DataTextField = "Description"
        ddldiv.DataValueField = "BlkGrpCode"
        ddldiv.DataBind()
    End Sub

	
    Sub BindMandor(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_MANDOR_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow


        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|AND a.Status='1' AND c.IDDiv like '%" & strDivCode & "%' |ORDER BY b.EmpName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MANDOR_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("empname") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("empname"))
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("codeemp") = ""
        dr("empname") = "All"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlMandor.DataSource = objEmpCodeDs.Tables(0)
        ddlMandor.DataTextField = "empname"
        ddlMandor.DataValueField = "codeemp"
        ddlMandor.DataBind()
    End Sub

    Sub BindEmployee(ByVal strdivcode As String, ByVal MandorCode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
        Dim tmpParam As String
		Dim strAM as String

		If cint(ddlbulan.SelectedItem.Value.Trim()) < 10 then
			strAM = "0" & ddlbulan.SelectedItem.Value.Trim()
		Else 
			strAM = ddlbulan.SelectedItem.Value.Trim()
		End if



        strParamName = "LOC|AM|AY|SEARCH|SORT"
        If MandorCode <> "" Then
		'by aam 9 sep 2012
            tmpParam = "AND A.EmpCode IN (SELECT distinct y.CodeEmp FROM hr_trx_empmandor x,hr_trx_empmandorln y WHERE x.mandorcode=y.codemandor and x.codeemp='" & MandorCode & "') "
        Else
            tmpParam = ""
        End If
        strParamValue = strLocation & "|" & ddlbulan.SelectedItem.Value.Trim & "|" & ddltahun.SelectedItem.Value.Trim & "|" & tmpParam & " AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strdivcode & "%' |ORDER BY A.EmpName"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
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

        ddlEmp.DataSource = objEmpCodeDs.Tables(0)
        ddlEmp.DataTextField = "_Description"
        ddlEmp.DataValueField = "EmpCode"
        ddlEmp.DataBind()
    End Sub

    Sub ddldivisi_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindMandor(ddlDiv.SelectedItem.Value.Trim())
        BindEmployee(ddlDiv.SelectedItem.Value.Trim(), "")
    End Sub
	
	 Sub HitView(Byval v as String)
        Dim strOpCdSP As String = "PR_PR_STDRPT_DAFTAR_ABSENSI_KERJA_REPORT"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String

        strMn = ddlbulan.SelectedItem.Value.Trim
        strYr = ddltahun.SelectedItem.Value.Trim

        ParamName = "LOC|DIV|MN|YR|EMP|MDR"

        ParamValue = strLocation & "|" & _
					 ddlDiv.SelectedItem.Value.Trim & "|" & _
		             strMn & "|" & _
					 strYr & "|" & _
                     ddlEmp.SelectedItem.Value.Trim & "|" & _
					 ddlMandor.SelectedItem.Value.Trim 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCdSP, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_MTHEND_RICERATION_PROCESS_SP&errmesg=" & Exp.Message & "&redirect=")
        End Try

		 If objDataSet.Tables(0).Rows.Count > 0 Then
		    dgpay.DataSource = objDataSet.Tables(0)
			dgpay.DataBind()
         
        End If
      
        
    End Sub
	
	Sub ViewBtn_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        HitView("A")
    End Sub
		
	Sub ExportView()
			
        Dim cAccMonth As String = ddltahun.SelectedItem.Value.Trim & ddlbulan.SelectedItem.Value.Trim

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Payroll-" & cAccMonth & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgpay.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
	
	Sub ExportBtn_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        ExportView() 
    End Sub
	
	Sub dgpay_BindGrid(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) 
    	
		 If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
		 Dim lbl As Label
		 Dim lbl2 As Label
		 
			 	
			e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
			
		 End If
    End Sub
	
End Class
