Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic
Imports agri.Admin
Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap
Imports agri.GL


Public Class PR_setup_Saldet_Estate : Inherits Page

    Protected WithEvents ddlEmptyCode As DropDownList
    Protected WithEvents ddlCompCode As DropDownList
    Protected WithEvents ddlLocCode As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents SalCode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupDept As Label
    Protected WithEvents lblNoDeptCode As Label
    Protected WithEvents lblNoCompCode As Label
    Protected WithEvents lblNoLocCode As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblCompany As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblDeptHead As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblList As Label
    Protected WithEvents lblCodeDiv As Label
    Protected WithEvents lblDivHead As Label
    Protected WithEvents lblEmptyCode As Label
    Protected WithEvents txtpstart As TextBox
    Protected WithEvents txtpEnd As TextBox
    Protected WithEvents txtUMP As TextBox
    Protected WithEvents txtHK As TextBox
    Protected WithEvents txtMinHK As TextBox
    Protected WithEvents txtSmalPay As TextBox
    Protected WithEvents txthkjam As TextBox


    Protected WithEvents ChkGol As CheckBox
    Protected WithEvents ChkAstek As CheckBox
    Protected WithEvents ChkBeras As CheckBox
    Protected WithEvents validateNoBlok As RequiredFieldValidator
    Protected WithEvents revCode As RegularExpressionValidator

	Protected WithEvents ChkMakan As CheckBox
    Protected WithEvents ChkTrans As CheckBox
	Protected WithEvents txtMakan As TextBox
    Protected WithEvents txtTrans As TextBox
	

    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim ObjOK As New agri.GL.ClsTrx

    Dim objJobCode As New Object()
    Dim objDeptDs As New Object()
    Dim objDeptCodeDs As New Object()
    Dim objCompDs As New Object()
    Dim objDivHeadDs As New Object()
    Dim objLocDs As New Object()
    Dim objEmpDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
	Dim intLevel As Integer

    Dim strSelectedSalCode As String = ""
    Dim intStatus As Integer
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String
    Dim Prefix As String = "SLC"

    Function getCode() As String
        Dim strOpCd_GetID As String = "ALL_ALL_GETID"
        Dim objNewID As New Object
        Dim intErrNo As Integer

        Prefix = "SC" & strLocation

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GetID, "PREFIX|LOCID|LEN", Prefix & "||8", objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ALL_ALL_GETID&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("CURID"))
    End Function

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        intPRAR = Session("SS_PRAR")
		intLevel = Session("SS_USRLEVEL")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf (objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRPaySetup), intPRAR) = False) and (intLevel < 2) Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedSalCode = Trim(IIf(Request.QueryString("SalaryCode") <> "", Request.QueryString("SalaryCode"), Request.Form("SalaryCode")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedSalCode <> "" Then
                    SalCode.Value = strSelectedSalCode
                    onLoad_Display()
                Else
                    onLoad_BindEmpTyCode("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Sub onLoad_Display()
        Dim strOpCd_Get As String = "PR_PR_STP_EMPSALARY_GET"
        Dim intErrNo As Integer
        Dim strSearch As String
        Dim sortitem As String
        Dim Gol As String
        Dim Astek As String
        Dim Beras As String
		Dim Makan As String
		Dim Trans As String

        strSearch = "AND A.SalaryCode like '" & SalCode.Value & "%' AND A.LocCode like '" & strLocation & "%'"
        sortitem = ""
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_Get, ParamNama, ParamValue, objDeptDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objDeptDs.Tables(0).Rows.Count > 0 Then
            intStatus = CInt(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblHiddenSts.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Status"))
            txtpstart.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeStart"))
            txtpEnd.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("PeriodeEnd"))
            txtUMP.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UMPRate"))
            txtHK.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("HKKGRate"))
            txtMinHK.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("MinHK"))
            txtSmalPay.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("SmallPay"))
			txtMakan.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("MakanRate"))
			txtTrans.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("TransRate"))
            If (objDeptDs.Tables(0).Rows(0).Item("MinHK") = 0) And (objDeptDs.Tables(0).Rows(0).Item("HKKGRate") <> 0) Then
                txtMinHK.Text = Math.Round(objDeptDs.Tables(0).Rows(0).Item("SmallPay") / objDeptDs.Tables(0).Rows(0).Item("HKKGRate")) + 1
            End If


            Gol = Trim(objDeptDs.Tables(0).Rows(0).Item("isGol"))
            If Gol = "1" Then
                ChkGol.Checked = True
            Else
                ChkGol.Checked = False
            End If


            Astek = Trim(objDeptDs.Tables(0).Rows(0).Item("isAstek"))
            If Astek = "1" Then
                ChkAstek.Checked = True
            Else
                ChkAstek.Checked = False
            End If

            Beras = Trim(objDeptDs.Tables(0).Rows(0).Item("isBeras"))
            If Beras = "1" Then
                ChkBeras.Checked = True
            Else
                ChkBeras.Checked = False
            End If

			Makan = Trim(objDeptDs.Tables(0).Rows(0).Item("isMakan"))
            If Astek = "1" Then
                ChkMakan.Checked = True
            Else
                ChkMakan.Checked = False
            End If
			
			Trans = Trim(objDeptDs.Tables(0).Rows(0).Item("isTrans"))
            If Astek = "1" Then
                ChkTrans.Checked = True
            Else
                ChkTrans.Checked = False
            End If
			
            ' 
            txthkjam.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("Hk"))

            lblStatus.Text = objHRSetup.mtdGetDeptStatus(Trim(objDeptDs.Tables(0).Rows(0).Item("Status")))
            lblDateCreated.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDeptDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objDeptDs.Tables(0).Rows(0).Item("UserName"))
            onLoad_BindEmpTyCode(Trim(objDeptDs.Tables(0).Rows(0).Item("CodeEmpTy")))
            onLoad_BindButton()
            EnabledObj(Trim(objDeptDs.Tables(0).Rows(0).Item("Symbol")))
        Else
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPSALARY_GET&errmesg=" & lblNoRecord.Text & "&redirect=")
        End If
    End Sub

    Sub onLoad_BindButton()

        txtpstart.Enabled = False
        txtpEnd.Enabled = False

        txtUMP.Enabled = False
        txtHK.Enabled = False
        txtMinHK.Enabled = False
        txtSmalPay.Enabled = False
        ChkGol.Enabled = False
        ChkAstek.Enabled = False
        ChkBeras.Enabled = False
        ddlEmptyCode.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
		ChkMakan.Enabled = False
	    ChkTrans.Enabled = False

        Select Case intStatus
            Case "1"
                txtpstart.Enabled = True
                txtpEnd.Enabled = True
                txtUMP.Enabled = True
                txtHK.Enabled = True
                txtMinHK.Enabled = False
                txtSmalPay.Enabled = True
                ChkGol.Enabled = True
                ChkAstek.Enabled = True
                ChkBeras.Enabled = True
				ChkMakan.Enabled = True
				ChkTrans.Enabled = True
                ddlEmptyCode.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case "2"
                UnDelBtn.Visible = True
            Case Else
                txtpstart.Enabled = True
                txtpEnd.Enabled = True
                txtUMP.Enabled = True
                txtHK.Enabled = True
                txtMinHK.Enabled = True
                txtSmalPay.Enabled = True
                ChkGol.Enabled = True
                ChkAstek.Enabled = True
                ChkBeras.Enabled = True
				ChkMakan.Enabled = True
				ChkTrans.Enabled = True
                ddlEmptyCode.Enabled = True
                SaveBtn.Visible = True
        End Select

    End Sub

    Sub EnabledObj(ByVal tyemp As String)
        Select Case Trim(tyemp)
            Case "B"
                txtUMP.Enabled = False
                txtHK.Enabled = False
                txtMinHK.Enabled = True
            Case Else
                txtUMP.Enabled = True
                txtHK.Enabled = True
                txtMinHK.Enabled = True
        End Select
    End Sub
	
    Sub onLoad_BindEmpTyCode(ByVal pv_strEmpTyCode As String)
        Dim strOpCd_EmptyCode As String = "HR_HR_STP_EMPTYPE_LIST_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSearch As String
        Dim strSearch_all As String = ""
        Dim sortitem As String

        strSearch = "WHERE Status='1'"
        sortitem = "order by EmpTyCode"
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_EmptyCode, ParamNama, ParamValue, objDeptCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEPTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objDeptCodeDs.Tables(0).Rows.Count - 1
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("EmpTyCode") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("EmpTyCode"))
            objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("EmpTyCode")) & " (" & Trim(objDeptCodeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objDeptCodeDs.Tables(0).Rows(intCnt).Item("EmpTyCode") = pv_strEmpTyCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDeptCodeDs.Tables(0).NewRow()
        dr("EmpTyCode") = ""
        dr("Description") = "Select Employment Type Code"
        objDeptCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmptyCode.DataSource = objDeptCodeDs.Tables(0)
        ddlEmptyCode.DataValueField = "EmpTyCode"
        ddlEmptyCode.DataTextField = "Description"
        ddlEmptyCode.DataBind()
        ddlEmptyCode.SelectedIndex = intSelectedIndex
    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcceptFormat As String

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

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

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_STP_EMPSALARY_UPD"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim valSalCd As String
        Dim valEmp As String
        Dim Gol As String
        Dim Astek As String
        Dim Beras As String
        Dim Adate As String
        Dim strstatus As String = ""
        Dim objID As New Object()
		Dim Makan As String
        Dim Trans As String

        If ddlEmptyCode.SelectedItem.Value = "" Then
            lblNoDeptCode.Visible = True
            lblNoDeptCode.Text = "Please Select Empty Code"
            Exit Sub
        End If

        If intStatus = 0 Then
            valSalCd = getCode()
        Else
            valSalCd = strSelectedSalCode
        End If


        SalCode.Value = valSalCd
        strSelectedSalCode = SalCode.Value

        valEmp = ddlEmptyCode.SelectedValue


        If ChkGol.Checked = True Then
            Gol = "1"
        Else
            Gol = "0"
        End If

        If ChkAstek.Checked = True Then
            Astek = "1"
        Else
            Astek = "0"
        End If

        If ChkBeras.Checked = True Then
            Beras = "1"
        Else
            Beras = "0"
        End If
		
		
        If ChkMakan.Checked = True Then
            Makan = "1"
        Else
            Makan = "0"
        End If
		
		
        If ChkTrans.Checked = True Then
            Trans = "1"
        Else
            Trans = "0"
        End If

        If strCmdArgs = "Save" Or strCmdArgs = "UnDel" Then

            strstatus = "1"

        ElseIf strCmdArgs = "Del" Then

            strstatus = "2"

        End If




        ParamNama = "SC|CE|Loc|PS|PE|UMPRt|HKRt|MinHK|Gol|AT|Beras|SP|HK|ST|CD|UD|UI|CM|CT|MKN|TRNS"
        ParamValue = valSalCd & "|" & valEmp & "|" & strLocation & "|" & txtpstart.Text & "|" & txtpEnd.Text & "|" & _
                     iif(txtUMP.Text.trim()="","0",txtUMP.text) & "|" & iif(txtHK.Text.trim()="","0",txtHk.text) & "|" & txtMinHK.Text & "|" & Gol & "|" & Astek & "|" & _
                     Beras & "|" & txtSmalPay.Text & "|" & txthkjam.Text & "|" & strstatus & "|" & DateTime.Now & "|" & DateTime.Now & "|" & strUserId & "|" & _
					 Makan & "|" & Trans & "|" & iif(txtmakan.text.trim()="","0",txtmakan.text) & "|" &  iif(txttrans.text.trim()="","0",txttrans.text)



        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPSALARY_UPD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If strSelectedSalCode <> "" Then
            onLoad_Display()
        End If

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_setup_SalList_Estate.aspx")
    End Sub


End Class
