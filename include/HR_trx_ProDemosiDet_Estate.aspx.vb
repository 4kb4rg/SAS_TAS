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

Public Class HR_trx_ProDemosiDet_Estate : Inherits Page

#Region "Declare Var"


    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents LblidM As Label
    Protected WithEvents ddlempdivisi As DropDownList
    Protected WithEvents ddlempcode As DropDownList
    Protected WithEvents txtemptype As TextBox
    Protected WithEvents chkempgol As CheckBox
    Protected WithEvents txtempgol As TextBox
    Protected WithEvents txtempjabatan As TextBox

    Protected WithEvents ddlemptype_baru As DropDownList
    Protected WithEvents ddlempjabatan_baru As DropDownList
    Protected WithEvents txtempdoc_baru As TextBox
    Protected WithEvents txtemptgl_baru As TextBox
    Protected WithEvents txtempket_baru As TextBox

    Protected WithEvents txtempsalary_baru As TextBox
    Protected WithEvents chkempgol_baru As CheckBox
    Protected WithEvents ddlempgol_baru As DropDownList
    Protected WithEvents txtempgaji_baru As TextBox
    Protected WithEvents txtemppremi_baru As TextBox
    Protected WithEvents txtemptunj_baru As TextBox
    Protected WithEvents txtempupah_baru As TextBox
    Protected WithEvents txtemppjmm_baru As TextBox
    Protected WithEvents txtempmhk_baru As TextBox
    Protected WithEvents txtempspsi_baru As TextBox
    Protected WithEvents txtempplain_baru As TextBox
    Protected WithEvents txtempberas_baru As TextBox
    Protected WithEvents txtempovt_baru As TextBox

    Protected WithEvents chkempcatu_baru As CheckBox
    Protected WithEvents chkempastek_baru As CheckBox
	Protected WithEvents chkempbpjs_baru  As CheckBox
	Protected WithEvents chkempjp_baru  As CheckBox
    Protected WithEvents chkempspsi_baru As CheckBox
    Protected WithEvents chkempbonus_baru As CheckBox

    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

    Protected WithEvents isNew As HtmlInputHidden
    Protected WithEvents idPayHist As HtmlInputHidden
    Protected WithEvents idWrkHist As HtmlInputHidden
	
	Protected WithEvents chkempastekJKM_baru As CheckBox
	Protected WithEvents chkempastekJHT_baru As CheckBox


    Protected WithEvents ddlstatus As DropDownList

    Protected WithEvents DelBtn As ImageButton

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

    Dim strCompany As String
    Dim strLocation As String
    Dim strLocationName As String

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

    Dim strID As String = ""
    Dim strEmpCode As String = ""
    Dim strEmpDivCode As String = ""
    Dim strEmpTypeCode As String = ""
    Dim strAcceptFormat As String
    Dim strIDPay As String = ""
	Dim strIDWok As String = ""
	

#End Region

#Region "Page Load"
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
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
            DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            strID = Trim(IIf(Request.QueryString("ProDeID") <> "", Request.QueryString("ProDeID"), Request.Form("ProDeID")))

            lblErrMessage.Visible = False
            If Not IsPostBack Then
                txtemptgl_baru.Text = objGlobal.GetShortDate(strDateFmt, Now())
                If strID <> "" Then
                    isNew.Value = "False"
                    onLoad_Display()
                Else
                    isNew.Value = "True"
                    LblidM.Text = getCode("EPDI/" & strLocation & "/" & Mid(Trim(txtemptgl_baru.Text), 4, 2) & Right(Trim(txtemptgl_baru.Text), 2) & "/", "46")
                    BindDivision("")
                    BindJabatan("")
                    BindEmpType("")
                End If
                onLoad_button()
            End If
        End If
    End Sub

#End Region

#Region "Function & Procedure"
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=pu/trx/pu_GRList.aspx")
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

    Function getCode(ByVal hdr As String, ByVal id As String) As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String
        Dim dt As String = objGlobal.GetShortDate(strDateFmt, Now())
        tcode = "EPDI/" & strLocation & "/" & Mid(Trim(dt), 4, 2) & Right(Trim(dt), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|" & id & "|" & hdr & "|6"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Sub getDetailLama(ByVal ec As String)
        Dim strOpCd As String = "HR_HR_TRX_EMPHIST_PRODEMOSI_GET_EMP"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer

        ParamName = "LOC|SEARCH"
        ParamValue = strLocation & "|AND b.codeemp='" & ec & "'"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPHIST_PRODEMOSI_GET_EMP&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            txtemptype.Text = objDs.Tables(0).Rows(0).Item("codeempty")
            chkempgol.Checked = objDs.Tables(0).Rows(0).Item("isGol")
            txtempgol.Text = Trim(objDs.Tables(0).Rows(0).Item("CodeGol"))
            txtempjabatan.Text = Trim(objDs.Tables(0).Rows(0).Item("description")) & " (" & Trim(objDs.Tables(0).Rows(0).Item("codejbt")) & ")"
        End If


    End Sub

    Sub GetNewPayroll(ByVal ty As String)
        Dim strOpCd As String = "PR_PR_STP_EMPSALARY_GET"

        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strYM As String = Right(txtemptgl_baru.Text.Trim, 4) + Mid(txtemptgl_baru.Text.Trim, 4, 2)
        Dim objDs As New Object()
        Dim intErrNo As Integer

        ParamName = "SEARCH|SORT"
        ParamValue = "AND ('" & strYM & "' >= right(rtrim(periodestart),4)+left(rtrim(periodestart),2)) " & _
                     "AND ('" & strYM & "' <= right(rtrim(periodeend),4)+left(rtrim(periodeend),2)) " & _
                     "AND A.CodeEmpTy like '" & ty & "' AND A.LocCode='" & strLocation & "' AND A.Status='1'|"


        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPHIST_PRODEMOSI_GET_EMP&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            txtempsalary_baru.Text = Trim(objDs.Tables(0).Rows(0).Item("SalaryCode"))
            chkempgol_baru.Checked = IIf(Trim(objDs.Tables(0).Rows(0).Item("isGol")) = "1", True, False)
            ddlempgol_baru.Enabled = chkempgol_baru.Checked
            If chkempgol_baru.Checked Then
                BindEmGol("")
            End If

            If Trim(objDs.Tables(0).Rows(0).Item("Symbol")) = "K" Then
                txtempgaji_baru.Text = CDbl(objDs.Tables(0).Rows(0).Item("UMPRate"))
                txtempplain_baru.Text = CDbl(objDs.Tables(0).Rows(0).Item("MakanRate"))
            Else
                txtempplain_baru.Text = "0.00"
                txtempgaji_baru.Text = "0.00"
            End If

            txtemppremi_baru.Text = "0.00"
            txtemptunj_baru.Text = "0.00"
            txtempupah_baru.Text = Trim(objDs.Tables(0).Rows(0).Item("HKKgRate"))
            chkempastek_baru.Checked = IIf(Trim(objDs.Tables(0).Rows(0).Item("isastek")) = "1", True, False)
            chkempcatu_baru.Checked = IIf(Trim(objDs.Tables(0).Rows(0).Item("isberas")) = "1", True, False)

            txtempmhk_baru.Text = objDs.Tables(0).Rows(0).Item("MinHk")
            txtemppjmm_baru.Text = objDs.Tables(0).Rows(0).Item("SmallPay")

            If chkempcatu_baru.Checked Then
                txtempberas_baru.Text = GetBerasRate()
            Else
                txtempberas_baru.Text = "0"
            End If

            txtempspsi_baru.Text = "0.00"

        Else
            txtempsalary_baru.Text = ""
            chkempgol_baru.Checked = False
            ddlempgol_baru.Enabled = False
            txtempgaji_baru.Text = "0.00"
            txtemppremi_baru.Text = "0.00"
            txtemptunj_baru.Text = "0.00"
            txtempupah_baru.Text = "0.00"
            chkempastek_baru.Checked = False
            chkempcatu_baru.Checked = False
            txtempmhk_baru.Text = "0.00"
            txtemppjmm_baru.Text = "0.00"
            txtempberas_baru.Text = "0"
            txtempspsi_baru.Text = "0.00"
            txtempplain_baru.Text = "0.00"
        End If
    End Sub

    Function GetBerasRate() As Double
        Dim strOpCd As String = "PR_PR_STP_BERASRATE_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strYM As String = Right(txtemptgl_baru.Text.Trim, 4) + Mid(txtemptgl_baru.Text.Trim, 4, 2)
        Dim objDs As New Object()
        Dim intErrNo As Integer
        ParamName = "SEARCH|SORT"

        ParamValue = "AND ('" & strYM & "' >= right(rtrim(periodestart),4)+left(rtrim(periodestart),2)) AND " & _
                         "('" & strYM & "' <= right(rtrim(periodeend),4)+left(rtrim(periodeend),2)) AND " & _
                          "A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BerasCode"
        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BERASRATE_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            Return (objDs.Tables(0).Rows(0).Item("BerasRate"))
        End If
    End Function

    Function getovertime(ByVal et As String, ByVal br As String, ByVal up As String, ByVal gp As String) As String
        Dim strOpCd As String = "PR_PR_OVERTIME_CALCULATE_SP"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strYM As String = Mid(txtemptgl_baru.Text.Trim, 4, 2) + Right(txtemptgl_baru.Text.Trim, 4)
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer


        ParamName = "ET|BR|UP|GP|PR|LOC"
        If br = "" Then br = "0"
        If up = "" Then up = "0"
        If gp = "" Then gp = "0"
        ParamValue = et & "|" & br & "|" & up & "|" & gp & "|" & strYM & "|" & strLocation

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_OVERTIME_CALCULATE_SP&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try


        Return (objDs.Tables(0).Rows(intCnt).Item("hasil"))
    End Function

    Sub BindEmGol(ByVal str As String)
        Dim strOpCd As String = "PR_PR_STP_EMPGOL_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strYM As String = Right(txtemptgl_baru.Text.Trim, 4) + Mid(txtemptgl_baru.Text.Trim, 4, 2)
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0
        Dim dr As DataRow


        ParamName = "SEARCH|SORT"
        ParamValue = "WHERE Status='1' " & _
               "AND ('" & strYM & "' >= right(rtrim(periodestart),4)+left(rtrim(periodestart),2)) AND " & _
                     "('" & strYM & "' <= right(rtrim(periodeend),4)+left(rtrim(periodeend),2)) " & _
                     " |ORDER By GolCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPGOL_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("GolCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("GolCode"))
                objDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDs.Tables(0).Rows(intCnt).Item("GolCode"))

                If objDs.Tables(0).Rows(intCnt).Item("GolCode") = str Then
                    intselectIndex = intCnt + 1
                End If

            Next
        End If

        dr = objDs.Tables(0).NewRow()
        dr("GolCode") = ""
        dr("Description") = "pilih golongan"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlempgol_baru.DataSource = objDs.Tables(0)
        ddlempgol_baru.DataTextField = "Description"
        ddlempgol_baru.DataValueField = "GolCode"
        ddlempgol_baru.DataBind()
        ddlempgol_baru.SelectedIndex = intselectIndex

    End Sub

    Function GetGolSalary(ByVal gl As String) As Double
        Dim strOpCd As String = "PR_PR_STP_EMPGOL_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strYM As String = Right(txtemptgl_baru.Text.Trim, 4) + Mid(txtemptgl_baru.Text.Trim, 4, 2)
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intselectIndex As Integer = 0

        If gl <> "" Then

            ParamName = "SEARCH|SORT"
            ParamValue = "WHERE GolCode='" & gl & "' " & _
                         "AND ('" & strYM & "' >= right(rtrim(periodestart),4)+left(rtrim(periodestart),2)) AND " & _
                             "('" & strYM & "' <= right(rtrim(periodeend),4)+left(rtrim(periodeend),2)) AND " & _
                         " Status='1'|ORDER By GolCode"

            Try
                intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_EMPGOL_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
            End Try

            If objDs.Tables(0).Rows.count > 0 Then
                Return (objDs.Tables(0).Rows(0).Item("BasicSalary"))
            Else
                Return (0)
            End If

        Else
            Return (0)
        End If


    End Function

    Sub clearAll()
        LblidM.Text = ""
    End Sub

    Sub onLoad_button()
        'If strID <> "" Then
        '    lbldivisilama.Visible = True
        '    lbldivisibaru.Visible = True
        '    lbljabatanbaru.Visible = True
        '    lblempcode.Visible = True
        '    ddldivisicode.Visible = False
        '    ddlempcode.Visible = False
        '    ddldivisibaru.Visible = False
        '    ddljabatanbaru.Visible = False
        'Else
        '    lbldivisilama.Visible = False
        '    lbldivisibaru.Visible = False
        '    lbljabatanbaru.Visible = False
        '    lblempcode.Visible = False
        '    ddldivisicode.Visible = True
        '    ddlempcode.Visible = True
        '    ddldivisibaru.Visible = True
        '    ddljabatanbaru.Visible = True
        'End If
    End Sub

    Sub Save_ProDemosi()
        Dim strOpCd As String = "HR_HR_TRX_EMPHIST_PRODEMOSI_UPD"
        Dim id As String = ""
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String
        Dim dt As String = objGlobal.GetShortDate(strDateFmt, Now())
        Dim golb As String

        If isNew.Value = "True" Then
            id = getCode("EPDI/" & strLocation & "/" & Right(Trim(dt), 2) & "/", "46")
            idPayHist.Value = getCode("ERPH/" & strLocation & "/", "47")
            idWrkHist.Value = getCode("EWKH/" & strLocation & "/", "45")
        Else
            id = LblidM.Text.Trim()
        End If


        If chkempgol_baru.Checked Then
            golb = ddlempgol_baru.SelectedItem.Value.Trim()
        Else
            golb = ""
        End If

        ParamNama = "ID|CE|TY|TA|IGA|GA|JA|TB|IGB|GB|JB|ED|DOC|KET|AK|IDP|LOC|CD|UD|UI"
        ParamValue = id.Trim() & "|" & _
                     ddlempcode.SelectedItem.Value.Trim() & "|" & _
                     ddlstatus.SelectedItem.Value.Trim() & "|" & _
                     txtemptype.Text.Trim() & "|" & _
                     CInt(chkempgol.Checked) * -1 & "|" & _
                     txtempgol.Text.Trim() & "|" & _
                     Mid(Trim(txtempjabatan.Text), InStr(txtempjabatan.Text, "(") + 1, Len(Trim(txtempjabatan.Text)) - InStr(Trim(txtempjabatan.Text), "(") - 1) & "|" & _
                     ddlemptype_baru.SelectedItem.Value.Trim() & "|" & _
                     CInt(chkempgol_baru.Checked) * -1 & "|" & _
                     golb & "|" & _
                     ddlempjabatan_baru.SelectedItem.Value.Trim & "|" & _
                     Date_Validation(txtemptgl_baru.Text, False) & "|" & _
                     txtempdoc_baru.Text.Trim() & "|" & _
                     txtempket_baru.Text.Trim() & "|" & _
                     "0" & "|" & _
                     idPayHist.Value.Trim & "|" & _
                     strLocation & "|" & _
                     Now() & "|" & _
                     Now() & "|" & _
                     strUserId


        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try


    End Sub

    Sub Save_Payroll()
        Dim strOpCd As String = "HR_HR_TRX_EMPHIST_PYROL_UPD"
        Dim id As String = idPayHist.Value.Trim
        Dim strYM As String = Mid(txtemptgl_baru.Text.Trim, 4, 2) + Right(txtemptgl_baru.Text.Trim, 4)

        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String

        Dim golb As String

        If chkempgol_baru.Checked Then
            golb = ddlempgol_baru.SelectedItem.Value.Trim()
        Else
            golb = ""
        End If

        ParamNama = "ID|CE|PS|PE|CS|CG|BS|RS|TS|UP|MH|SS|IG|IB|IA|IS|SR|OR|BR|OT|IN|IV|LOC|CD|UD|UI|IANB|PM|IM|IT|MR|TR|BPJS|JP|JKM|JHT"
        ParamValue = id.Trim() & "|" & _
                     ddlempcode.SelectedItem.Value.Trim() & "|" & _
                     strYM & "|" & _
                     "000000" & "|" & _
                     txtempsalary_baru.Text.Trim() & "|" & _
                     golb & "|" & _
                     txtempgaji_baru.Text & "|" & _
                     txtemppremi_baru.Text & "|" & _
                     txtemptunj_baru.Text & "|" & _
                     txtempupah_baru.Text & "|" & _
                     txtempmhk_baru.Text & "|" & _
                     txtemppjmm_baru.Text & "|" & _
                     CInt(chkempgol_baru.Checked) * -1 & "|" & _
                     CInt(chkempcatu_baru.Checked) * -1 & "|" & _
                     CInt(chkempastek_baru.Checked) * -1 & "|" & _
                     CInt(chkempspsi_baru.Checked) * -1 & "|" & _
                     txtempspsi_baru.Text & "|" & 0 & "|" & _
                     txtempberas_baru.Text & "|" & _
                     txtempovt_baru.Text & "|" & _
                     CInt(chkempbonus_baru.Checked) * -1 & "|" & _
                     "0" & "|" & _
                     strLocation & "|" & _
                     Now() & "|" & _
                     Now() & "|" & _
                     strUserId & "|0|0|0|0| " & CDbl(txtempplain_baru.Text) & " |0" & "|" & _
                      CInt(chkempbpjs_baru.Checked) * -1 & "|" & _
                      CInt(chkempjp_baru.Checked) * -1 & "|" & _
                      CInt(chkempastekJKM_baru.Checked) * -1 & "|" & _
                      CInt(chkempastekJHT_baru.Checked) * -1


        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try



    End Sub

    Sub Save_Work()
        Dim strOpCd As String = "HR_HR_TRX_EMPWORK_UPD"
        Dim id As String = idWrkHist.Value.Trim
        Dim strYM As String = Mid(txtemptgl_baru.Text.Trim, 4, 2) + Right(txtemptgl_baru.Text.Trim, 4)

        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String


        ParamNama = "ID|CE|CN|PS|PE|JB|LOC|CD|UD|UI|CDV|CJB"
        ParamValue = id.Trim() & "|" & _
                     ddlempcode.SelectedItem.Value.Trim() & "|" & _
                     strLocationName & "|" & _
                     strYM & "|" & _
                     "000000" & "|" & _
                     ddlemptype_baru.SelectedItem.Value.Trim() & "-" & ddlempjabatan_baru.SelectedItem.Text.Trim() & "|" & _
                     strLocation & "|" & _
                     Now() & "|" & _
                     Now() & "|" & _
                     strUserId & "|" & _
					 ddlempdivisi.SelectedItem.Value.Trim()  & "|" & _ 
					 ddlempjabatan_baru.SelectedItem.Value.Trim() 


        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try



    End Sub

#End Region

#Region "Binding"
    Sub BindDivision(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim ObjDiv As New Object()
		 Dim intSelectedIndex As Integer = 0
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, ObjDiv)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_DIVISICODE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If ObjDiv.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To ObjDiv.Tables(0).Rows.Count - 1
                ObjDiv.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(ObjDiv.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                ObjDiv.Tables(0).Rows(intCnt).Item("Description") = Trim(ObjDiv.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(ObjDiv.Tables(0).Rows(intCnt).Item("Description")) & ")" 
				 If ObjDiv.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(strDivCode) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = ObjDiv.Tables(0).NewRow()
        dr("BlkGrpCode") = " "
        dr("Description") = "Pilih Divisi"
        ObjDiv.Tables(0).Rows.InsertAt(dr, 0)

        ddlempdivisi.DataSource = ObjDiv.Tables(0)
        ddlempdivisi.DataTextField = "Description"
        ddlempdivisi.DataValueField = "BlkGrpCode"
        ddlempdivisi.DataBind()
		ddlempdivisi.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindEmployee(ByVal strDivCode As String, ByVal ec As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow
		Dim strAm as String
		
		strAM = Mid(Trim(txtemptgl_baru.Text), 4, 2)

        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & strAM & "|" & Right(Trim(txtemptgl_baru.Text), 4) & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%'|ORDER BY A.EmpName"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPLOYEE_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("_Description")) & " - " & Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("Job"))
                If objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(ec) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("_Description") = "Please Select Employee Code"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlempcode.DataSource = objEmpCodeDs.Tables(0)
        ddlempcode.DataTextField = "_Description"
        ddlempcode.DataValueField = "EmpCode"
        ddlempcode.DataBind()
        ddlempcode.SelectedIndex = intSelectedIndex

    End Sub

    Sub BindJabatan(ByVal str As String)
        Dim strOpCd As String = "HR_HR_STP_JABATAN_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0

        Dim dr As DataRow

        ParamName = "SEARCH|SORT"
        ParamValue = "WHERE LocCode='" & strLocation & "' AND Status='1'|ORDER By JbtCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_JABATAN_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("JbtCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("JbtCode"))
                objDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDs.Tables(0).Rows(intCnt).Item("Description"))


                If objDs.Tables(0).Rows(intCnt).Item("JbtCode") = str Then
                    intselectIndex = intCnt + 1
                End If

            Next
        End If

        dr = objDs.Tables(0).NewRow()
        dr("JbtCode") = ""
        dr("Description") = "Pilih Jabatan"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlempjabatan_baru.DataSource = objDs.Tables(0)
        ddlempjabatan_baru.DataTextField = "Description"
        ddlempjabatan_baru.DataValueField = "JbtCode"
        ddlempjabatan_baru.DataBind()
        ddlempjabatan_baru.SelectedIndex = intselectIndex

    End Sub

    Sub BindEmpType(ByVal str As String)
        Dim strOpCd As String = "HR_HR_STP_EMPTYPE_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0

        Dim dr As DataRow

        ParamName = "SEARCH|SORT"
        ParamValue = "WHERE Status='1'|ORDER By EmpTyCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_EMPTYPE_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
                objDs.Tables(0).Rows(intCnt).Item("EmpTyCode") = Trim(objDs.Tables(0).Rows(intCnt).Item("EmpTyCode"))
                objDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDs.Tables(0).Rows(intCnt).Item("Symbol")) & " (" & Trim(objDs.Tables(0).Rows(intCnt).Item("Description")) & ")"

                If objDs.Tables(0).Rows(intCnt).Item("EmpTyCode") = str Then
                    intselectIndex = intCnt + 1
                End If

            Next
        End If

        dr = objDs.Tables(0).NewRow()
        dr("EmpTyCode") = ""
        dr("Description") = "Pilih Tipe Karyawan"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlemptype_baru.DataSource = objDs.Tables(0)
        ddlemptype_baru.DataTextField = "Description"
        ddlemptype_baru.DataValueField = "EmpTyCode"
        ddlemptype_baru.DataBind()
        ddlemptype_baru.SelectedIndex = intselectIndex

    End Sub

#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPHIST_PRODEMOSI_GET"
        Dim strOpCd_EmpPay As String = "HR_HR_TRX_EMPHIST_PYROL_GET"
		Dim strOpCd_EmpWok As String = "HR_HR_TRX_EMPHIST_PRODEMOSI_GET_EWKH"

        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "SEARCH|SORT|LOC"
        strParamValue = "AND A.ProDeID='" & strID.Trim & "'||" & strlocation 

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPHIST_PRODEMOSI_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            LblidM.Text = strID
            strEmpCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CodeEmp"))
            BindDivision(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDDIv")))
            BindEmployee(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDDIv")), strEmpCode)
            txtemptype.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpTyAwl"))
            chkempgol.Checked = objEmpCodeDs.Tables(0).Rows(0).Item("isGolAwl")
            txtempgol.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CodeGolAwl"))
            txtempjabatan.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Jabatan"))

            ddlstatus.SelectedValue = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TypePro"))
            BindEmpType(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmptyBru")))
            chkempgol_baru.Checked = objEmpCodeDs.Tables(0).Rows(0).Item("isGolBru")
            ddlempgol_baru.Enabled = chkempgol_baru.Checked
            BindEmGol(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CodeGolBru")))
            BindJabatan(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("JabatanBru")))
            txtempdoc_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("DocNo"))
            txtempket_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Notes"))
            txtemptgl_baru.Text = Date_Validation(objEmpCodeDs.Tables(0).Rows(0).Item("EfektifDate"), True)

            strIDPay = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDPayHist"))
            idPayHist.Value = strIDPay.Trim

            lblStatus.Text = IIf(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Status")) = "1", "Aktive", "Delete")
            lblDateCreated.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("uName"))


        End If

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.PayHistID='" & strIDPay.Trim & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpPay, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPHIST_PRODEMOSI_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            txtempsalary_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CodeSalary"))
            txtempgaji_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("BasicSalary"))
            txtemppremi_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PremiSalary"))
            txtemptunj_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("TunjSalary"))
            txtempupah_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Upah"))
            txtemppjmm_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("SmallSalary"))
            txtempmhk_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("MinHk"))
            txtempspsi_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("SPSIRate"))
            txtempplain_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("OthPotRate"))
            txtempberas_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("BerasRate"))
            txtempovt_baru.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("OvrTmRate"))
            chkempcatu_baru.Checked = objEmpCodeDs.Tables(0).Rows(0).Item("isBeras")
            chkempastek_baru.Checked = objEmpCodeDs.Tables(0).Rows(0).Item("isAstek")
            chkempbonus_baru.Checked = objEmpCodeDs.Tables(0).Rows(0).Item("isBonus")
            chkempspsi_baru.Checked = objEmpCodeDs.Tables(0).Rows(0).Item("isSPSI")
            chkempbpjs_baru.Checked = objEmpCodeDs.Tables(0).Rows(0).Item("isBPJS")
            chkempjp_baru.Checked = objEmpCodeDs.Tables(0).Rows(0).Item("isBPJS")
            chkempastekJKM_baru.Checked = objEmpCodeDs.Tables(0).Rows(0).Item("isAstekJKM")
            chkempastekJHT_baru.Checked = objEmpCodeDs.Tables(0).Rows(0).Item("isAstekJHT")
        End If

		strParamName = "SEARCH|SORT"
        strParamValue = "AND CodeEmp='" & strEmpCode.Trim & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpWok, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPHIST_PRODEMOSI_GET_EWKH&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
		     strIDWok = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("WorkHistId"))
             idWrkHist.Value = strIDWok.Trim
		End If
    End Sub

    Sub ddlempdivisi_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpDivCode = ddlempdivisi.SelectedItem.Value.Trim()
        BindEmployee(strEmpDivCode, "")
    End Sub

    Sub ddlempcode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpCode = ddlempcode.SelectedItem.Value.Trim()
        getDetailLama(strEmpCode)

    End Sub

    Sub ddlemptype_baru_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpTypeCode = ddlemptype_baru.SelectedItem.Value.Trim()
        GetNewPayroll(strEmpTypeCode)
        txtempovt_baru.Text = getovertime(Left(ddlemptype_baru.SelectedItem.Text.Trim(),1), txtempberas_baru.Text, txtempupah_baru.Text, txtempgaji_baru.Text)
    End Sub

    Sub ddlempgol_baru_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If ddlempgol_baru.SelectedItem.Value.Trim <> "" Then
            strEmpTypeCode = ddlemptype_baru.SelectedItem.Value.Trim()
            txtempgaji_baru.Text = GetGolSalary(ddlempgol_baru.SelectedItem.Value.Trim)
            txtempovt_baru.Text = getovertime(Left(ddlemptype_baru.SelectedItem.Text.Trim(),1), txtempberas_baru.Text, txtempupah_baru.Text, txtempgaji_baru.Text)
        End If
    End Sub

    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

		If ddlempdivisi.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Text = "Silakan pilih divisi"
            lblErrMessage.Visible = True
            Exit Sub
        End If
		
        If ddlempcode.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Text = "Silakan pilih nama karyawan"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If ddlemptype_baru.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Text = "Silakan pilih tipe karyawan"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If ddlempjabatan_baru.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Text = "Silakan pilih jabatan karyawan"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If chkempgol_baru.Checked Then
            If ddlempgol_baru.SelectedItem.Value.Trim = "" Then
                lblErrMessage.Text = "Silakan pilih golongan karyawan"
                lblErrMessage.Visible = True
                Exit Sub
            End If
        End If

        Save_ProDemosi()
        Save_Payroll()
        Save_Work()
        'generate update info payroll & empment 

        BackBtn_Click(Sender, E)
    End Sub

    Sub DelBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd1 As String = "HR_HR_TRX_EMPHIST_PRODEMOSI_DEL"
        Dim strOpCd2 As String = "HR_HR_TRX_EMPHIST_PYROL_DEL"
		Dim strOpCd3 As String = "HR_HR_TRX_EMPWORK_DEL"

        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String


        

        ParamNama = "ID"
        ParamValue = idPayHist.Value.Trim()

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd2, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try
		
		ParamNama = "ID"
        ParamValue = idWrkHist.Value.Trim()

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd3, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try
		

		ParamNama = "ID|LOC"
        ParamValue = LblidM.Text.Trim & "|" & strlocation

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd1, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try
		
        BackBtn_Click(Sender, E)

    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_ProDemosiList_Estate.aspx")
    End Sub


#End Region

End Class
