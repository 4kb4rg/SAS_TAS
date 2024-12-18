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


Public Class HR_trx_EmployeePayDet_Estate : Inherits Page

    Protected WithEvents Lblpayhistid As Label
    Protected WithEvents Lblcodeemp As Label
	Protected WithEvents Lblemptype As Label
    Protected WithEvents txtperiodeAwl As TextBox
    Protected WithEvents txtperiodeaAhr As TextBox
    Protected WithEvents ddlcodesalary As DropDownList
    Protected WithEvents ddlcodegol As DropDownList
    Protected WithEvents txtbasicsalary As TextBox
    Protected WithEvents txtpremisalary As TextBox
	Protected WithEvents txttunjsalary As TextBox
	Protected WithEvents txtupah As TextBox
	Protected WithEvents txtminhk As TextBox
	Protected WithEvents txtsmallsalary As TextBox
	Protected WithEvents txtSPSIRate As TextBox
	Protected WithEvents txtOvrTmRate As TextBox
	Protected WithEvents txtBeras As TextBox
	Protected WithEvents txtpotmangkir As TextBox
	
	Protected WithEvents  txtmakan As TextBox
	Protected WithEvents  txttrans As TextBox
	Protected WithEvents  chkismakan As CheckBox
	Protected WithEvents  chkistrans As CheckBox
	
    Protected WithEvents chkisgol As CheckBox
	Protected WithEvents chkisberas As CheckBox
	Protected WithEvents chkisastek As CheckBox
	Protected WithEvents chkisbpjs  As CheckBox
	Protected WithEvents chkisjp  As CheckBox
	Protected WithEvents chkisspsi As CheckBox
	Protected WithEvents chkisbonus As CheckBox
	Protected WithEvents chkisasteknberas As CheckBox
    
	Protected WithEvents lblRedirect As Label
	Protected WithEvents lblErrMessage As Label
    
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents DelBtn As ImageButton

	Protected WithEvents dgTnj As DataGrid
	Protected WithEvents ddlcodetnj As DropDownList
	Protected WithEvents txtnominal As TextBox
	
	Protected WithEvents chkisastekJKM As CheckBox
	Protected WithEvents chkisastekJHT As CheckBox
    
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

    Dim objEmpDs As New Object()
    Dim objEmpTypeDs As New Object()
    Dim objEmpDivDs As New Object()
    Dim objEmpJobDs As New Object()
    Dim strLocType As String  
	Dim strAccMonth As String
    Dim strAccYear As String
    Dim strID As String = ""
	Dim cntrp As Double = 0
    
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
		strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
      
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            lblErrMessage.Visible = False

            lblRedirect.Text = Request.QueryString("redirect")
            strID = Trim(IIf(Request.QueryString("PayHistID") <> "", Request.QueryString("PayHistID"), Request.Form("PayHistID")))
            DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            If Not IsPostBack Then
                If strID <> "" Then
                    Lblpayhistid.Text = strID
					BindIDTunj()
                    onLoad_Display()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_EmpAtt As String = "HR_HR_TRX_EMPHIST_PYROL_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpAttDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.PayHistID='" & strID & "'|"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_EmpAtt, strParamName, strParamValue, objEmpAttDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPHIST_PYROL_GET&errmesg=" & Exp.Message)
        End Try

        If objEmpAttDs.Tables(0).Rows.Count = 1 Then
            lblStatus.Text = "Aktive"
            Lblcodeemp.Text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("CodeEmp"))
            txtperiodeAwl.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("PeriodeAwl"))
			txtperiodeaAhr.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("PeriodeAhr"))
			
			BindSalaryCode(Trim(objEmpAttDs.Tables(0).Rows(0).Item("CodeSalary")))
			if Trim(objEmpAttDs.Tables(0).Rows(0).Item("CodeGol")) <> "" then 
			BindGolCode(Trim(objEmpAttDs.Tables(0).Rows(0).Item("basicsalary")))
			end if
			
			Lblemptype.text = GetTypeEmp(ddlcodesalary.SelectedItem.Value.Trim()) 
		    txtbasicsalary.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("basicsalary"))
			txtpremisalary.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("premisalary"))
			txttunjsalary.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("tunjsalary"))
			txtupah.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("upah"))
			txtminhk.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("minhk"))
			txtsmallsalary.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("smallsalary"))
			txtSPSIRate.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("SPSIRate"))
			txtOvrTmRate.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("OvrTmRate"))
			txtBeras.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("BerasRate"))
			txtpotmangkir.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("PotMangkir"))
			txtmakan.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("makanrate"))
			txttrans.text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("transrate"))
			
			chkisgol.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("isGOL")) = "1", True, False)
			chkisberas.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("isBeras")) = "1", True, False)
			chkisastek.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("isAstek")) = "1", True, False)
			chkisspsi.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("isSPSI")) = "1", True, False)
			chkisbonus.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("isBonus")) = "1", True, False)
			chkisasteknberas.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("isAstekNBeras")) = "1", True, False)
			chkismakan.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("ismakan")) = "1", True, False)
			chkistrans.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("istrans")) = "1", True, False)
			chkisbpjs.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("isBPJS")) = "1", True, False)
			chkisjp.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("isJP")) = "1", True, False)
			
			chkisastekJKM.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("isAstekJKM")) = "1", True, False)
			chkisastekJHT.Checked = IIf(Trim(objEmpAttDs.Tables(0).Rows(0).Item("isAstekJHT")) = "1", True, False)
			
  		    lblCreateDate.Text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objEmpAttDs.Tables(0).Rows(0).Item("UserName"))
			
			DIM etype As String = GetTypeEmp(ddlcodesalary.SelectedItem.Value.Trim())
	        IF (etype <> "") Then
			
                 If (rtrim(etype)="B" or rtrim(etype)="S")Then
              		txtupah.text = 0
					ddlcodegol.Enabled = True
					txtbasicsalary.Enabled = False
					txtupah.Enabled = False 
				else if (rtrim(etype)="K") then 
				    ddlcodegol.Enabled = False
					txtbasicsalary.Enabled = True 
					txtupah.Enabled = False
				else 
				    ddlcodegol.Enabled = False
					txtbasicsalary.text = 0
					txtbasicsalary.Enabled = False
					txtupah.Enabled = False 
                End If
               
            End If
				BindTunjangan(strID)
        End If

    End Sub
	
	 Sub BindIDTunj()
	    Dim strOpCd As String = "PR_PR_STP_TUNJANGAN_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer 
		Dim dr As DataRow 
        Dim intselectIndex As Integer = 0

            ParamName = "SEARCH|SORT"
            ParamValue = "|ORDER By Description"

            Try
                intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPSALARY_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
            End Try

			
        dr = objDs.Tables(0).NewRow()
        dr("idTnj") = ""
        dr("Description") = "Select Kode Tunjangan"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlcodetnj.DataSource = objDs.Tables(0)
        ddlcodetnj.DataTextField = "Description"
        ddlcodetnj.DataValueField = "idTnj"
        ddlcodetnj.DataBind()
        ddlcodetnj.SelectedIndex = intselectIndex               
	End Sub
	
	 Sub BindSalaryCode(ByVal str As String)
        Dim strOpCd As String = "PR_PR_STP_EMPSALARY_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer 
		Dim dr As DataRow 
        Dim intselectIndex As Integer = 0

            ParamName = "SEARCH|SORT"
            ParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By SalaryCode"

            Try
                intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPSALARY_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
            End Try

			If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("SalaryCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("SalaryCode"))
                objDs.Tables(0).Rows(intCnt).Item("DescEmpty") = "Type: " & Trim(objDs.Tables(0).Rows(intCnt).Item("CodeEmpTy")) & " Upah: " & Trim(objDs.Tables(0).Rows(intCnt).Item("HKKgRate"))  & " [Periode : " & Trim(objDs.Tables(0).Rows(intCnt).Item("PeriodeStart")) & "-" & Trim(objDs.Tables(0).Rows(intCnt).Item("PeriodeEnd")) & "]"
                If objDs.Tables(0).Rows(intCnt).Item("SalaryCode") = str Then
                    intselectIndex = intCnt + 1
                End If

            Next
        End If
        dr = objDs.Tables(0).NewRow()
        dr("SalaryCode") = ""
        dr("DescEmpty") = "Select Kode Salary"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlcodesalary.DataSource = objDs.Tables(0)
        ddlcodesalary.DataTextField = "DescEmpty"
        ddlcodesalary.DataValueField = "SalaryCode"
        ddlcodesalary.DataBind()
        ddlcodesalary.SelectedIndex = intselectIndex               
    End Sub
	
	Function getovertime(ByVal et As String, ByVal br As String, ByVal up As String, ByVal gp As String) As String
        Dim strOpCd As String = "PR_PR_OVERTIME_CALCULATE_SP"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer


        ParamName = "ET|BR|UP|GP|PR|LOC|EMPGOL"
        If br = "" Then br = "0"
        If up = "" Then up = "0"
        If gp = "" Then gp = "0"
        ParamValue = et & "|" & br & "|" & up & "|" & gp & "|" & Format(Now(), "MMyyyy") & "|" & strLocation & "|" & ddlcodegol.SelectedItem.Value

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_OVERTIME_CALCULATE_SP&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try


        Return (objDs.Tables(0).Rows(intCnt).Item("hasil"))
    End Function

	Function getpmangkir(ByVal et As String, ByVal br As String, ByVal up As String, ByVal gp As String) As String
        Dim strOpCd As String = "PR_PR_POTMANGKIR_CALCULATE_SP"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim Ty As String = GetTgEmp(Lblcodeemp.Text.tRIM())

        ParamName = "ACYEAR|AMONTH|ET|BR|UP|GP|TK|PR|LOC"
        If br = "" Then br = "0"
        If up = "" Then up = "0"
        If gp = "" Then gp = "0"
        ParamValue =  strAccYear & "|" & strAccMonth & "|" &et & "|" & br & "|" & up & "|" & gp & "|" & trim(TY) & "|" & Format(Now(), "MMyyyy") & "|" & strLocation

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_POTMANGKIR_CALCULATE_SP&errmesg=" & lblErrMessage.Text )
        End Try


        Return (objDs.Tables(0).Rows(intCnt).Item("hasil"))
    End Function
	
	Function GetTgEmp(ByVal str As String) As String
		Dim strOpCd As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0
		Dim SM As String = ""
		Dim SY As String = strAccYear


		If Cint(strAccMonth) < 10 Then
			SM = "0" & rtrim(strAccMonth)
        Else
			SM = rtrim(strAccMonth)
        End If
		
		ParamName = "LOC|AM|AY|SEARCH|SORT"
        ParamValue = strLocation & "|" & SM & "|" & SY & "|AND A.EmpCOde = '" & str & "' AND A.LocCode = '" & strLocation & "' AND A.Status='1'|"

            
            Try
                intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPSALARY_GET&errmesg=" & lblErrMessage.Text )
            End Try

            If objDs.Tables(0).Rows.count > 0 Then
                Return (objDs.Tables(0).Rows(0).Item("CodeTgHrd"))
            Else
                Return ("NA")
            End If

        
	End Function
	
	Function GetTypeEmp(ByVal gl As String) As String
        Dim strOpCd As String = "PR_PR_STP_EMPSALARY_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0

        If gl <> "" Then

            ParamName = "SEARCH|SORT"
            ParamValue = "AND SalaryCode='" & gl & "'|"

            Try
                intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPSALARY_GET&errmesg=" & lblErrMessage.Text )
            End Try

            If objDs.Tables(0).Rows.count > 0 Then
                Return (objDs.Tables(0).Rows(0).Item("Symbol"))
            Else
                Return ("")
            End If

        Else
            Return ("")
        End If
    End Function
	
	Sub GetSalary(ByVal str As String)
        Dim strOpCd As String = "PR_PR_STP_EMPSALARY_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0

        If str <> "" Then

            ParamName = "SEARCH|SORT"
            ParamValue = "AND SalaryCode='" & str & "'|"

            Try
                intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPSALARY_GET&errmesg=" & lblErrMessage.Text)
            End Try

            If objDs.Tables(0).Rows.Count > 0 Then
                chkisgol.Checked = IIf(Trim(objDs.Tables(0).Rows(intCnt).Item("isGol")) = "1", True, False)
                ddlcodegol.Enabled = chkisgol.Checked
                txtupah.Text = Trim(objDs.Tables(0).Rows(intCnt).Item("HKKgRate"))
                chkisastek.Checked = IIf(Trim(objDs.Tables(0).Rows(intCnt).Item("isastek")) = "1", True, False)
                chkisberas.Checked = IIf(Trim(objDs.Tables(0).Rows(intCnt).Item("isberas")) = "1", True, False)
			    txtminhk.Text = objDs.Tables(0).Rows(intCnt).Item("MinHk")
                txtsmallsalary.Text = objDs.Tables(0).Rows(intCnt).Item("SmallPay")
            End If
        End If
    End Sub
	
	Protected Sub ddlcodesalaryChanged(ByVal sender As Object, ByVal e As System.EventArgs)
         
		Lblemptype.text = GetTypeEmp(ddlcodesalary.SelectedItem.Value.Trim())
		GetSalary(ddlcodesalary.SelectedItem.Value.Trim())
            If (Lblemptype.text.trim() <> "") Then
                 If Lblemptype.text.trim()="B" or Lblemptype.text.trim()="S"  Then
                    BindGolCode("0")
					ddlcodegol.Enabled = True
					txtbasicsalary.text = 0
					txtbasicsalary.Enabled = True 
					txtupah.Enabled = False 
				else if (Lblemptype.text.trim()="K" ) then
					ddlcodegol.Enabled = false 
					txtbasicsalary.text = 0
					txtbasicsalary.Enabled = True 
					txtupah.Enabled = False
				else 
				    ddlcodegol.Enabled = false 
					txtbasicsalary.text = 0
					txtbasicsalary.Enabled = False
					txtupah.Enabled = False 
                End If
               
            End If
			txtOvrTmRate.text = getovertime(Lblemptype.text.trim(), txtBeras.Text, txtupah.Text, txtbasicsalary.Text)
			txtpotmangkir.text = getpmangkir(Lblemptype.text.trim(), txtBeras.Text, txtupah.Text, txtbasicsalary.Text)
    End Sub
	
	Protected Sub ddlcodegolChanged(ByVal sender As Object, ByVal e As System.EventArgs)
	if ddlcodegol.SelectedItem.Value.Trim() <> "" then 
       GetGolSalary(ddlcodegol.SelectedItem.Text.Trim()) 
	   txtOvrTmRate.text = getovertime(Lblemptype.text.trim(), txtBeras.Text, txtupah.Text, txtbasicsalary.Text)
	   txtpotmangkir.text = getpmangkir(Lblemptype.text.trim(), txtBeras.Text, txtupah.Text, txtbasicsalary.Text)
	end if
    End Sub
	
	Sub GetGolSalary(ByVal gl As String)
			Dim ar As Array 
            ar = Split(rtrim(gl), "|")
            txtbasicsalary.text =  ar(0)
    End Sub
	
    Sub BindGolCode(ByVal str As Double)
        Dim strOpCd As String = "PR_PR_STP_EMPGOL_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer 
		Dim dr As DataRow 
        Dim intselectIndex As Integer = 0

            ParamName = "SEARCH|SORT"
            ParamValue = "WHERE LocCode='" & strlocation & "' AND Status='1'|ORDER By GolCode,BasicSalary"

            Try
                intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPGOL_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
            End Try

			If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("Description") = cint(Trim(objDs.Tables(0).Rows(intCnt).Item("BasicSalary"))) & "|" & Trim(objDs.Tables(0).Rows(intCnt).Item("GolCode")) 
				objDs.Tables(0).Rows(intCnt).Item("GolCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("GolCode")) & "|" &  cint(Trim(objDs.Tables(0).Rows(intCnt).Item("BasicSalary")))
               
                If objDs.Tables(0).Rows(intCnt).Item("BasicSalary") = str Then
                    intselectIndex = intCnt + 1
                End If

            Next
        End If
        dr = objDs.Tables(0).NewRow()
        dr("GolCode") = ""
        dr("Description") = "Select Kode Golongan"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlcodegol.DataSource = objDs.Tables(0)
        ddlcodegol.DataTextField = "Description"
        ddlcodegol.DataValueField = "GolCode"
        ddlcodegol.DataBind()
        ddlcodegol.SelectedIndex = intselectIndex
          
        
    End Sub

    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
		Dim EditText As TextBox
        Dim id As String
        Dim strOppCd As String = "HR_HR_TRX_EMPHIST_PYROL_UPD"
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		Dim strgol As String
		Dim ar As Array 
		
		if ddlcodegol.Text.Trim() = "" then
		strgol = ""
		else
		'strgol = left(ddlcodegol.SelectedItem.Value.Trim() ,2)		   
            ar = Split(ddlcodegol.SelectedItem.Value.Trim(), "|")
            strgol =  ar(0)		
		end if

        ParamNama = "ID|CE|PS|PE|CS|CG|BS|RS|TS|UP|MH|SS|IG|IB|IA|IS|SR|OR|BR|OT|IN|IV|LOC|CD|UD|UI|IANB|PM|IM|IT|MR|TR|BPJS|JP|JKM|JHT"
        ParamValue = Lblpayhistid.Text.Trim() & "|" & _
		             Lblcodeemp.Text.Trim() & "|" & _
					 txtperiodeAwl.Text.Trim() & "|" & _
					 txtperiodeaAhr.Text.Trim() & "|" & _
					 ddlcodesalary.SelectedItem.Value.Trim() & "|" & _
					 strgol & "|" & _
					 iif(txtbasicsalary.Text.Trim()="","0",txtbasicsalary.Text.Trim()) & "|" & _
					 iif(txtpremisalary.Text.Trim()="","0",txtpremisalary.Text.Trim()) & "|" & _
					 iif(txttunjsalary.Text.Trim()="","0",txttunjsalary.Text.Trim()) & "|" & _
					 iif(txtupah.Text.Trim()="","0",txtupah.Text.Trim()) & "|" & _
					 iif(txtminhk.Text.Trim()="","0",txtminhk.Text.Trim()) & "|" & _
					 iif(txtsmallsalary.Text.Trim()="","0",txtsmallsalary.Text.Trim()) & "|" & _
					 CInt(chkisgol.Checked) * -1 & "|" & _
					 CInt(chkisberas.Checked) * -1 & "|" & _
					 CInt(chkisastek.Checked) * -1 & "|" & _
					 CInt(chkisspsi.Checked) * -1 & "|" & _
					 iif(txtSPSIRate.Text.Trim()="","0",txtSPSIRate.Text.Trim()) & "|" & _
					 "0" & "|" & _
					 iif(txtBeras.Text.Trim()="","0",txtBeras.Text.Trim()) & "|" & _
					 iif(txtOvrTmRate.Text.Trim()="","0",txtOvrTmRate.Text.Trim()) & "|" & _
					 CInt(chkisbonus.Checked) * -1  & "|" & _
					 "1"  & "|" & _
					 strLocation & "|" & _
                     Now() & "|" & _
                     Now() & "|" & _
                     strUserId & "|" & _
					 CInt(chkisasteknberas.Checked) * -1  & "|" & _
					 iif(txtpotmangkir.Text.Trim()="","0",txtpotmangkir.Text.Trim())  & "|" & _
					 CInt(chkismakan.Checked) * -1 & "|" & _
					 CInt(chkistrans.Checked) * -1 & "|" & _
					 iif(txtmakan.Text.Trim()="","0",txtmakan.Text.Trim()) & "|" & _
					 iif(txttrans.Text.Trim()="","0",txttrans.Text.Trim()) & "|" & _ 
					 CInt(chkisbpjs.Checked) * -1  & "|" & _ 
					 CInt(chkisjp.Checked) * -1 & "|" & _
					 CInt(chkisastekJKM.Checked) * -1 & "|" & _
					 CInt(chkisastekJHT.Checked) * -1 
					 
					 
        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOppCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPHIST_PYROL_UPD&errmesg=" & lblErrMessage.Text )
        End Try
		
		Response.Flush()
        Response.Write("<Script Language=""JavaScript"">window.location.href=""HR_trx_EmployeeDet_Estate.aspx"";window.close(); </Script>")

    End Sub

    Sub SetBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
	   Lblemptype.text = GetTypeEmp(ddlcodesalary.SelectedItem.Value.Trim())
	   txtOvrTmRate.text = getovertime(Lblemptype.text.trim(), txtBeras.Text, txtupah.Text, txtbasicsalary.Text)
	   txtpotmangkir.text = getpmangkir(Lblemptype.text.trim(), txtBeras.Text, txtupah.Text, txtbasicsalary.Text)
	End Sub
	
    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Flush()
        Response.Write("<Script Language=""JavaScript"">window.location.href=""HR_trx_EmployeeDet_Estate.aspx"";window.close(); </Script>")
    End Sub

    Sub DelBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
	    Dim EditText As TextBox
        Dim id As String
        Dim strOppCd As String = "HR_HR_TRX_EMPHIST_PYROL_DEL"
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        ParamNama = "ID"
        ParamValue = Lblpayhistid.Text.Trim() 

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOppCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPHIST_PYROL_DEL&errmesg=" & lblErrMessage.Text )
        End Try

        Response.Flush()
        Response.Write("<Script Language=""JavaScript"">window.location.href=""HR_trx_EmployeeDet_Estate.aspx"";window.close(); </Script>")
    End Sub
	
	Sub BindTunjangan(ByVal str As String)
	    Dim strOpCd As String = "HR_HR_TRX_EMPHIST_TUNJANGAN_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0
		
		cntrp = 0
        If str <> "" Then

            ParamName = "ID"
            ParamValue = str

            Try
                intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPSALARY_GET&errmesg=" & lblErrMessage.Text)
            End Try

			
			
            If objDs.Tables(0).Rows.Count > 0 Then
			    For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
					cntrp = cntrp + + objDs.Tables(0).Rows(intCnt).Item("Nominal")
				Next
				
				'dgTnj.EditItemIndex = -1
				dgTnj.DataSource = objDs
				dgTnj.DataBind()
			End If
			txttunjsalary.Text = cntrp
		End If
	End Sub
	
	Sub BtnAddTnj_OnClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
		Dim strOpCd As String = "HR_HR_TRX_EMPHIST_TUNJANGAN_UPD"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim idTnj As String
		Dim RP as String
		Dim intErrNo as Integer
		
		idTnj = ddlcodetnj.SelectedItem.Value
		RP = txtnominal.Text
		
		If idTnj <> "" Then

            ParamName = "ID|IDTNJ|RP|CD|UD|UI"
            ParamValue = Lblpayhistid.Text.Trim() & "|" & _
			             idTnj & "|" & _
						 RP & "|" & _
						 DateTime.Now & "|" & DateTime.Now & "|" & strUserId
						 

            Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd, ParamName, ParamValue)

			Catch Exp As System.Exception
				Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
			End Try
		End If
		BindTunjangan(Lblpayhistid.Text.Trim())
	End Sub
		
	Sub dgTnj_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(1).Text = cntrp 
        End If
    End Sub
	
	Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
		dgTnj.EditItemIndex = CInt(E.Item.ItemIndex)
        BindTunjangan(Lblpayhistid.Text.Trim())
	End Sub
	
	Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
		Dim strOpCd As String = "HR_HR_TRX_EMPHIST_TUNJANGAN_UPD"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
		Dim HideText As HiddenField
		Dim EditText As TextBox
		
        Dim idTnj As String
		Dim RP as String
		Dim intErrNo as Integer
		
		HideText = E.Item.FindControl("dgTnj_hid_idtnj")
		IDTNJ = HideText.Value.Trim()
		EditText = E.Item.FindControl("NominalRP")
		RP    = EditText.Text.Trim()
		
		If idTnj <> "" Then

            ParamName = "ID|IDTNJ|RP|CD|UD|UI"
            ParamValue = Lblpayhistid.Text.Trim() & "|" & _
			             idTnj & "|" & _
						 RP & "|" & _
						 DateTime.Now & "|" & DateTime.Now & "|" & strUserId
						 

            Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd, ParamName, ParamValue)

			Catch Exp As System.Exception
				Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
			End Try
		End If
		dgTnj.EditItemIndex = -1
        BindTunjangan(Lblpayhistid.Text.Trim())
	End Sub
	
	 Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgTnj.EditItemIndex = -1
        BindTunjangan(Lblpayhistid.Text.Trim())
    End Sub
	
	Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
	 Dim EditText As HiddenField
	 Dim ID As String
	 Dim IDTNJ As String
	 Dim intErrNo As Integer
	 
	 Dim strOpCd As String = "HR_HR_TRX_EMPHIST_TUNJANGAN_DEL"
     Dim ParamName As String = ""
     Dim ParamValue As String = ""
    
	 
	 EditText = E.Item.FindControl("dgTnj_hid_payid")
	 ID = EditText.Value.Trim()
	 EditText = E.Item.FindControl("dgTnj_hid_idtnj")
	 IDTNJ = EditText.Value.Trim()

			ParamName = "ID|IDTNJ"
            ParamValue = ID & "|" & _
			             idTnj 

            Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd, ParamName, ParamValue)

			Catch Exp As System.Exception
				Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMIBERAS_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
			End Try
			
	 dgTnj.EditItemIndex = -1
     BindTunjangan(Lblpayhistid.Text.Trim())
    End Sub
End Class
