Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Public Class HR_trx_MandoranDet_Estate : Inherits Page

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLine As DataGrid

    Protected WithEvents idMDR As HtmlInputHidden
    Protected WithEvents LblidM As Label
    Protected WithEvents LblidD As Label

    Protected WithEvents lbldivisi As Label
    Protected WithEvents lblMdrCode As Label
    Protected WithEvents lblkcsCode As Label
    Protected WithEvents lblMdrType As Label

    Protected WithEvents ddlEmpDiv As DropDownList
    Protected WithEvents ddlMdrCode As DropDownList
    Protected WithEvents ddlKcsCode As DropDownList
    Protected WithEvents ddlEmpCode As DropDownList
    Protected WithEvents ddlMdrtype As DropDownList
	Protected WithEvents ddlEmpMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList

	Protected WithEvents ddlTrnCode As DropDownList
	Protected WithEvents lbltrnCode As Label
	
    Protected WithEvents ddlbeforemonth As DropDownList
    Protected WithEvents ddlbeforeyear As DropDownList

    Protected WithEvents validddlEmpDiv As Label
    Protected WithEvents validddlMdrCode As Label
    Protected WithEvents validddlKcsCode As Label
    Protected WithEvents validddlEmpCode As Label

    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents BackBtn As ImageButton

    Dim objPRTrx As New agri.PR.clsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim ObjOk As New agri.GL.ClsTrx()
    Dim strLocType As String

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim objEmpDivDs As New Object()


    Dim objContractLnDs As New Object()
    Dim objContractorDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objAccDs As New Object()
    Dim objBlkDs As New Object()
    Dim objVehDs As New Object()
    Dim objVehExpDs As New Object()
    Dim strCompany As String

    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFmt As String
    Dim intPRAR As Long
    Dim intConfig As Integer

    Dim strSelectedPayId As String = ""
    Dim intStatus As Integer
    Dim TarifLembur As Single

    Dim perfixM As String = "MD"
    Dim perfixD As String = "MDL"

    Dim strMDCode As String = ""
    Dim StrMDLCode As String = ""
    Dim strEmpCode As String = ""
    Dim strEmpDivCode As String = ""
    Dim strEmpMandorCode As String = ""
    Dim strEmpKCSCode As String = ""
	Dim strEmpTrnCode As String = ""

    Function getCode(ByVal P As String, ByVal L As String) As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        tcode = "MDR/" & strLocation & "/"

        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|2|" & tcode & "|5"


        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))
    End Function

    Sub clearAll()
        LblidM.Text = ""
        LblidD.Text = ""              
    End Sub

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_HRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HREmployeeDetails), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            validddlEmpDiv.Visible = False
            validddlMdrCode.Visible = False
            validddlKcsCode.Visible = False
            validddlEmpCode.Visible = False
            strMDCode = Trim(IIf(Request.QueryString("MandorCode") <> "", Request.QueryString("MandorCode"), Request.Form("MandorCode")))

            If Not IsPostBack Then
			    BindAccYear(Session("SS_SELACCYEAR"))

                ddlEmpMonth.SelectedIndex = CInt(Session("SS_SELACCMONTH")) - 1
                ddlbeforemonth.SelectedIndex = CInt(Session("SS_SELACCMONTH")) - 1

                If strMDCode <> "" Then
                    BindDivision("")
					onLoad_Display()
					
                Else
                    BindDivision("")
                    BindMdrType()
                End If
                onLoad_button()
            End If
        End If
    End Sub

    Sub onLoad_button()
        If strMDCode <> "" Then
            lbldivisi.Visible = True
            lblMdrCode.Visible = True
            'lblkcsCode.Visible = True
          			

            ddlEmpDiv.Visible = False
            ddlMdrCode.Visible = False
            'ddlKcsCode.Visible = False
			'ddlTrnCode.Visible = True
           
        Else
            lbldivisi.Visible = False
            lblMdrCode.Visible = False
            'lblkcsCode.Visible = False
			
			'ddlTrnCode.Visible = True
            'ddlKcsCode.Visible = True
            ddlEmpDiv.Visible = True
            ddlMdrCode.Visible = True
           
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPMANDOR_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|AND MandorCode Like '%" & strMDCode & "%' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMANDOR_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            LblidM.Text = strMDCode
            strEmpMandorCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CodeEmp"))
            strEmpDivCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IdDiv"))
            strEmpKCSCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("KCSCodeEmp"))
			strEmpTrnCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TrnCodeEmp"))

			BindEmployee(strEmpDivCode)
			BindMandor(strEmpDivCode)
			BindKCS(strEmpDivCode)
			BindTran(strEmpDivCode)
			ddlEmpDiv.selectedValue = strEmpDivCode
            ddlMdrCode.selectedValue = strEmpMandorCode
			ddlKcsCode.selectedValue = strEmpKCSCode
			ddlTrnCode.selectedValue = strEmpTrnCode

            lbldivisi.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("divisi")) & " (" & strEmpDivCode & ")"
            lblMdrCode.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("MdrName")) & " (" & strEmpMandorCode & ")"
            lblkcsCode.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("KcsName")) & " (" &  strEmpKCSCode & ")"
			lbltrnCode.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TrnName")) & " (" &  strEmpTrnCode & ")"

            Select Case Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TyMdr"))
                Case "P"
                    lblMdrType.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TyMdr")) & " (Potong Buah)"
                Case "K"
                    lblMdrType.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TyMdr")) & " (Kutip Brondolan)"
                Case "M"
                    lblMdrType.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TyMdr")) & " (Muat TBS)"
                Case "R"
                    lblMdrType.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TyMdr")) & " (Perawatan)"
                Case "S"
                    lblMdrType.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TyMdr")) & " (Keamanan)"
                Case "T"
                    lblMdrType.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TyMdr")) & " (Traksi)"
                Case "A"
                    lblMdrType.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TyMdr")) & " (Administrasi)"

					End Select
            BindMandorLn(strMDCode)
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

        ddlbeforeyear.DataSource = objAccYearDs.Tables(0)
        ddlbeforeyear.DataValueField = "AccYear"
        ddlbeforeyear.DataTextField = "UserName"
        ddlbeforeyear.DataBind()
        ddlbeforeyear.SelectedIndex = intSelectedIndex - 1


    End Sub
	
    Sub BindMandorLn(ByVal strMCode As String)
        Dim strOpCd_Get As String = "HR_HR_TRX_EMPMANDORLN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim objHvLnDs As New Object()
        Dim cnt As Integer = 0

		REM /* aam 9 sep 2012 */
        strSearch = "AND CodeMandor='" & strMCode & "' " & _
	          	    "AND AccMonth = '" & ddlEmpMonth.SelectedItem.Value.Trim() & "' " & _   
                    "AND AccYear = '" & ddlyear.SelectedItem.Value.Trim() & "' " 		    

        strParamName = "SEARCH|SORT"
        strParamValue = strSearch & "|ORDER BY EmpName"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objHvLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMANDORLN_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objHvLnDs.Tables(0).Rows.Count > 0 Then
            cnt = objHvLnDs.Tables(0).Rows.Count
        End If

        dgLine.PageSize = cnt + 1
        dgLine.DataSource = objHvLnDs
        dgLine.DataBind()
    End Sub

    Function ExistEmpCode(ByVal str As String) As String
        Dim strOpCd_Get As String = "HR_HR_TRX_EMPMANDORLN_CON"
        Dim strParamName As String
        Dim strParamValue As String
        Dim strSearch As String = ""
        Dim intErrNo As Integer
        Dim objHvLnDs As New Object()

        strParamName = "LOC|SEARCH"
        strParamValue = strLocation & "| WHERE emp='" & str & "'  and accmonth='"& ddlEmpMonth.SelectedItem.Value.Trim() & "' and accyear='" & ddlyear.SelectedItem.Value.Trim() & "'"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objHvLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMANDORLN_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objHvLnDs.Tables(0).Rows.Count = 1 Then
            strSearch = Trim(objHvLnDs.Tables(0).Rows(0).Item("empname")) & " Divisi : " & Trim(objHvLnDs.Tables(0).Rows(0).Item("iddiv"))
        End If

        Return strSearch.Trim

    End Function
	
	Function GetMandorCode(ByVal str As String) As String
        Dim strOpCd_Get As String = "HR_HR_TRX_EMPMANDOR_GET_CODE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim strSearch As String = ""
        Dim intErrNo As Integer
        Dim objHvLnDs As New Object()

        strParamName = "LOC|CE"
        strParamValue = strLocation & "|" & str 

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objHvLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMANDORLN_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objHvLnDs.Tables(0).Rows.Count = 1 Then
            strSearch = Trim(objHvLnDs.Tables(0).Rows(0).Item("MandorCode")) 
			Else
			strSearch = ""
        End If

        Return strSearch.Trim

    End Function

    Sub BindDivision(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
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
        dr("BlkGrpCode") = " "
        dr("Description") = "Please Select Employee Division"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpDiv.DataSource = objEmpDivDs.Tables(0)
        ddlEmpDiv.DataTextField = "Description"
        ddlEmpDiv.DataValueField = "BlkGrpCode"
        ddlEmpDiv.DataBind()
        ddlEmpDiv.SelectedIndex = 0

    End Sub

    Sub BindMdrType()
        ddlMdrtype.Items.Clear()
		ddlMdrtype.Items.Add(New ListItem("", ""))
        ddlMdrtype.Items.Add(New ListItem("Potong Buah", "P"))
        ddlMdrtype.Items.Add(New ListItem("Kutip Brondolan", "K"))
        ddlMdrtype.Items.Add(New ListItem("Muat TBS", "M"))
        ddlMdrtype.Items.Add(New ListItem("Perawatan", "R"))
        ddlMdrtype.Items.Add(New ListItem("Keamanan", "S"))
        ddlMdrtype.Items.Add(New ListItem("Traksi", "T"))
		ddlMdrtype.Items.Add(New ListItem("Administrasi", "A"))		
    End Sub

    Sub BindEmployee(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow


        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & ddlEmpMonth.SelectedItem.Value.Trim() & "|" & ddlyear.SelectedItem.Value.Trim() &"|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%'|ORDER BY EmpName"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description")) & " - " & Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("Job"))
                If objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(strEmpCode) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "Please Select Employee Code"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmpCode.DataSource = objEmpCodeDs.Tables(0)
        ddlEmpCode.DataTextField = "_Description"
        ddlEmpCode.DataValueField = "EmpCode"
        ddlEmpCode.DataBind()
        ddlEmpCode.SelectedIndex = intSelectedIndex

    End Sub

    Sub BindMandor(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow


        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & ddlEmpMonth.SelectedItem.Value.Trim() & "|" & ddlyear.SelectedItem.Value.Trim() & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%' AND (isMandor='1')|ORDER BY A.EmpName"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description")) & " - " & Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("Job"))
                If objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(strEmpCode) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "Please Select Mandor Code"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlMdrCode.DataSource = objEmpCodeDs.Tables(0)
        ddlMdrCode.DataTextField = "_Description"
        ddlMdrCode.DataValueField = "EmpCode"
        ddlMdrCode.DataBind()
        ddlMdrCode.SelectedIndex = intSelectedIndex

    End Sub

    Sub BindKCS(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow


        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & ddlEmpMonth.SelectedItem.Value.Trim() & "|" & ddlyear.SelectedItem.Value.Trim() & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%' AND (isMandor='1')|ORDER BY A.EmpName"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description"))
                If objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(strEmpCode) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "Please Select KCS Code"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlKcsCode.DataSource = objEmpCodeDs.Tables(0)
        ddlKcsCode.DataTextField = "_Description"
        ddlKcsCode.DataValueField = "EmpCode"
        ddlKcsCode.DataBind()
        ddlKcsCode.SelectedIndex = intSelectedIndex

    End Sub

	Sub BindTran(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow


        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & ddlEmpMonth.SelectedItem.Value.Trim() & "|" & ddlyear.SelectedItem.Value.Trim() & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%'|ORDER BY A.EmpName"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description"))
                If objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(strEmpCode) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "Please Select Transport Code"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTrnCode.DataSource = objEmpCodeDs.Tables(0)
        ddlTrnCode.DataTextField = "_Description"
        ddlTrnCode.DataValueField = "EmpCode"
        ddlTrnCode.DataBind()
        ddlTrnCode.SelectedIndex = intSelectedIndex

    End Sub
	
    Sub ddlEmpDiv_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpDivCode = ddlEmpDiv.SelectedItem.Value.Trim()
        BindMandor(strEmpDivCode)
        BindKCS(strEmpDivCode)
		BindTran(strEmpDivCode)
        BindEmployee(strEmpDivCode)
    End Sub
	
	Sub ddlEmpMdr_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
	    strMDCode = GetMandorCode(ddlMdrCode.SelectedItem.Value.Trim()) 
		IF strMDCode <> "" 
		onLoad_Display()
        onLoad_button()
		End If		
	End Sub
	
	Sub ddlEmpMonth_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
	    BindMandorLn(strMDCode)
	End Sub

    Sub Copybtn_Click(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strOpCd As String = "HR_HR_TRX_EMPMANDORLN_COPY"
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String

        ParamNama = "MC|AM|AY|CD|UD|UI|BAM|BAY"
        ParamValue = strMDCode & _
                     "|" & ddlEmpMonth.SelectedItem.Value.Trim() & _
                     "|" & ddlyear.SelectedItem.Value.Trim() & _
                     "|" & DateTime.Now() & _
                     "|" & DateTime.Now() & _
                     "|" & strUserId & _
                     "|" & ddlbeforemonth.SelectedItem.Value.Trim() & _
                     "|" & ddlbeforeyear.SelectedItem.Value.Trim()



        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try

        BindMandorLn(strMDCode)
    End Sub


    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "HR_HR_TRX_EMPMANDOR_UPD"
        Dim strOpCdLn As String = "HR_HR_TRX_EMPMANDORLN_UPD"
        Dim Status As String
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String

        Dim StrDv As String
        Dim StrMd As String
        Dim StrKc As String
        Dim StrEm As String
		Dim StrTr As String

        If LblidM.Text = "" Then

            '- Save Master First
            StrDv = ddlEmpDiv.SelectedItem.Value.Trim()
            StrMd = ddlMdrCode.SelectedItem.Value.Trim()
            StrKc = ddlKcsCode.SelectedItem.Value.Trim()
			StrTr = ddlTrnCode.SelectedItem.Value.Trim()

            If StrDv = "" Then
                validddlEmpDiv.Visible = True
                validddlEmpDiv.Text = "Please Select Divisi"
                Exit Sub
            End If

            If StrMd = "" Then
                validddlMdrCode.Visible = True
                validddlMdrCode.Text = "Please Select Mandor"
                Exit Sub
            End If

            strMDCode = getCode(perfixM, 8)

            ParamNama = "MC|DV|MCE|KCE|AD|TY|LOC|ST|CD|UD|UI|TRE"
            ParamValue = strMDCode & "|" & StrDv & "|" & StrMd & "|" & StrKc & "|" & DateTime.Now() & "|" & ddlMdrtype.SelectedItem.Value.Trim() & "|" & strLocation & "|1|" & DateTime.Now() & "|" & DateTime.Now() & "|" & strUserId & "|" & StrTr

            Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
            End Try
        Else
            strMDCode = LblidM.Text
			StrDv = ddlEmpDiv.SelectedItem.Value.Trim()
            StrMd = ddlMdrCode.SelectedItem.Value.Trim()
            StrKc = ddlKcsCode.SelectedItem.Value.Trim()
			StrTr = ddlTrnCode.SelectedItem.Value.Trim()
			
			ParamNama = "MC|DV|MCE|KCE|AD|TY|LOC|ST|CD|UD|UI|TRE"
            ParamValue = strMDCode & "|" & StrDv & "|" & StrMd & "|" & StrKc & "|" & DateTime.Now() & "||" & strLocation & "|1|" & DateTime.Now() & "|" & DateTime.Now() & "|" & strUserId & "|" & StrTr

			 Try
                intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
            End Try
        End If

        StrEm = ddlEmpCode.SelectedItem.Value.Trim()
        If StrEm = "" Then
            validddlEmpCode.Visible = True
            validddlEmpCode.Text = "Please Select Employee"
            Exit Sub
        End If

        Status = ExistEmpCode(StrEm)
        If Status.Trim <> "" Then
            validddlEmpCode.Visible = True
            validddlEmpCode.Text = "Sudah terdaftar di Mandor " & Status
            Exit Sub
        End If

        '- Save Detail Next
        'by aam 9 sep 2012
        ParamNama = "CM|CE|LOC|ST|CD|UD|UI|AM|AY"
        ParamValue = strMDCode & "|" & StrEm & "|" & strLocation & "|1|" & DateTime.Now() & "|" & DateTime.Now() & "|" & strUserId & _
                     "|" & ddlEmpMonth.SelectedItem.Value.Trim() & "|" & ddlyear.SelectedItem.Value.Trim()

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCdLn, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try

        onLoad_Display()
        onLoad_button()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_MandoranList_Estate.aspx")
    End Sub

    Sub dgLine_Bind(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim lbl As Label
            lbl = CType(e.Item.FindControl("lbID"), Label)
            lbl.Text = e.Item.ItemIndex + 1.ToString


            Dim UpdButton As LinkButton
            UpdButton = CType(e.Item.FindControl("Delete"), LinkButton)
            UpdButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If


        End If
    End Sub

    Sub dgLine_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_UPD As String = "HR_HR_TRX_EMPMANDORLN_DEL"
        Dim strid As String
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String
        Dim lbl As Label

        dgLine.EditItemIndex = CInt(E.Item.ItemIndex)
        lbl = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("CodeEmp")
        strid = lbl.Text.Trim

        ParamNama = "ID|AM|AY"
        ParamValue = strid & "|" & ddlEmpMonth.SelectedItem.Value.Trim() & "|" & ddlyear.SelectedItem.Value.Trim()

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMANDORLN_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgLine.EditItemIndex = -1
        onLoad_Display()
    End Sub

End Class
