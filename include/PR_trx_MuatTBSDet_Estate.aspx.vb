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

Public Class PR_MuatTBSDet_Estate : Inherits Page

#Region "Declare Var"
    Protected WithEvents Label1 As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents LblidM As Label
    Protected WithEvents LblidD As Label

    Protected WithEvents txtWPDate As TextBox
    Protected WithEvents txtdenda As TextBox
    Protected WithEvents ddldivisicode As DropDownList
    Protected WithEvents ddlblokcode As DropDownList
    Protected WithEvents ddlEmpCode As DropDownList
    Protected WithEvents ddlMandorCode As DropDownList
    Protected WithEvents ddlKraniCode As DropDownList

    Protected WithEvents LblKraniCode As Label
    Protected WithEvents LblMandorCode As Label
    Protected WithEvents lblblokcode As Label
    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lbldivisicode As Label

    Protected WithEvents TxtPPanenHK As TextBox
    Protected WithEvents TxtPPanenKg As TextBox
    
    Protected WithEvents txt_tdkbsis As TextBox

    Protected WithEvents rbmuat As RadioButton
    Protected WithEvents rbbongkar As RadioButton

    Protected WithEvents isNew As HtmlInputHidden
    Protected WithEvents Hidblok As HtmlInputHidden
    Protected WithEvents tothk As HtmlInputHidden
    Protected WithEvents totkg As HtmlInputHidden
    Protected WithEvents totDenda As HtmlInputHidden
    Protected WithEvents totprm As HtmlInputHidden
    Protected WithEvents totupah As HtmlInputHidden


    Protected WithEvents BackBtn As ImageButton
    Protected WithEvents BtnSavePrm As ImageButton


    Protected WithEvents dgpremiln As DataGrid

    Dim objOk As New agri.GL.ClsTrx()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFmt As String
    Dim intPRAR As Long
    Dim intConfig As Integer

    Dim strHvCode As String = ""
    Dim intStatus As Integer


    Dim strEmpCode As String
    Dim strEmpMandorCode As String
    Dim strEmpKCSCode As String
    Dim strEmpDivCode As String
    Dim strEmpBlockCode As String
    Dim strDendaCode As String
    Dim strBlokGol As String
    Dim strAcceptFormat As String

#End Region

#Region "Page Load"
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strDateFmt = Session("SS_DATEFMT")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractPayment), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strHvCode = Trim(IIf(Request.QueryString("MtbID") <> "", Request.QueryString("MtbID"), Request.Form("MtbID")))
            lblErrMessage.Visible = False

            If Not IsPostBack Then
                txtWPDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                If strHvCode <> "" Then
                    isNew.Value = "False"
                    LblidM.Text = strHvCode
                    onLoad_Display()
                Else
                    isNew.Value = "True"
                    LblidM.Text = getCode()
                    BindDivisi()
                End If
                onLoad_button()
            End If

            If LblidM.Text <> "" Then
                BindHarvestLn(LblidM.Text)
            End If
        End If
    End Sub

#End Region

#Region "Function"

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


    Function GetBlokGol(ByVal blk As String) As String
        Dim strOpCd_GetID As String = "HR_HR_STP_BLOK_GET_GOL"
        Dim objNewID As New Object
        Dim intErrNo As Integer

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_GetID, "BC", blk, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_BLOK_GET_GOL&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("gol"))
    End Function

    Function Getpremi(ByVal jb As String) As Single
        If jb = "M" Then
            Return 6
        Else
            Return 5
        End If
    End Function

    Function toNumber(ByVal s As String) As String
        If (s = "" Or s = "NULL") Then
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber("0", 2), 2)
        Else
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(s, 2), 2)
        End If

    End Function

    Function getCode() As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        tcode = "MTB/" & strLocation & "/" & Mid(Trim(txtWPDate.Text), 4, 2) & Right(Trim(txtWPDate.Text), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|16|" & tcode & "|5"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Function GetAttDate(ByVal strEmpCode As String, ByVal strAttDate As String) As String
        Dim strOpCd_EmpAtt As String = "PR_PR_TRX_ATTENDANCE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpAttDs As New Object()

        strParamName = "SEARCH"
        strParamValue = "AND EmpCode='" & strEmpCode & "' AND AttDate='" & strAttDate & "' AND LocCode='" & strLocation & "'"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpAtt, strParamName, strParamValue, objEmpAttDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpAttDs.Tables(0).Rows.Count = 1 Then
            Return (Trim(objEmpAttDs.Tables(0).Rows(0).Item("Hk")))
        Else
            Return "0"
        End If
    End Function

#End Region

#Region "Binding"


    Sub BindDivisi()
        Dim strOpCd_EmpDiv As String = "HR_HR_STP_DIVISI_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode = '" & strLocation & "'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_DIVISI_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("DivID") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("DivID"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("DivID")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("DivID") = ""
        dr("Description") = "Please Select Divisi Code"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddldivisicode.DataSource = objEmpDivDs.Tables(0)
        ddldivisicode.DataTextField = "Description"
        ddldivisicode.DataValueField = "DivID"
        ddldivisicode.DataBind()
        ddldivisicode.SelectedIndex = 0
    End Sub

    Sub BindBlok(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_STP_BLOK_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0



        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode = '" & strLocation & "' AND IDDiv Like '%" & strDivCode & "%'|ORDER BY A.BlokCode"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_BLOK_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode"))
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("Description")) & " (" & Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("YearPlan")) & ")"
                If objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode") = Trim(strEmpBlockCode) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("BlokCode") = " "
        dr("Description") = "Please Select Block Code"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlblokcode.DataSource = objEmpBlkDs.Tables(0)
        ddlblokcode.DataTextField = "Description"
        ddlblokcode.DataValueField = "BlokCode"
        ddlblokcode.DataBind()
        ddlblokcode.SelectedIndex = intSelectedIndex

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


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%' AND JobCode='MTB'|ORDER BY A.EmpName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
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


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%' AND isMandor='1'|ORDER BY A.EmpName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
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
        dr("_Description") = "Please Select Mandor Code"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlMandorCode.DataSource = objEmpCodeDs.Tables(0)
        ddlMandorCode.DataTextField = "_Description"
        ddlMandorCode.DataValueField = "EmpCode"
        ddlMandorCode.DataBind()
        ddlMandorCode.SelectedIndex = intSelectedIndex

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


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%' AND isMandor='1'|ORDER BY A.EmpName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
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

        ddlKraniCode.DataSource = objEmpCodeDs.Tables(0)
        ddlKraniCode.DataTextField = "_Description"
        ddlKraniCode.DataValueField = "EmpCode"
        ddlKraniCode.DataBind()
        ddlKraniCode.SelectedIndex = intSelectedIndex

    End Sub

    Sub BindHarvestID(ByVal str_EmpBlok As String, ByVal Str_EmpCode As String)

        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_PREMIMUATTBS_GET"
        Dim strParamName As String
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "SEARCH|SORT"
        strParamValue = " AND A.EmpCode = '" & Str_EmpCode & "' AND A.CodeBlok='" & str_EmpBlok & "' AND A.LocCode='" & strLocation & "'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIMUATTBS_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            strHvCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("MtbID").ToString)
            LblidM.Text = strHvCode
            tothk.Value = toNumber(objEmpCodeDs.Tables(0).Rows(0).Item("TotalHK").ToString)
            totkg.Value = toNumber(objEmpCodeDs.Tables(0).Rows(0).Item("TotalKg").ToString)
            totDenda.Value = toNumber(objEmpCodeDs.Tables(0).Rows(0).Item("Denda").ToString)
        End If
    End Sub

    Sub BindHarvestLn(ByVal StrHvsID As String)
        Dim strOpCd_Get As String = "PR_PR_TRX_PREMIMUATTBSLN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objHvLnDs As New Object()
        Dim cnt As Integer = 0

        strSearch = "AND IDMtb='" & StrHvsID & "'"

        strParamName = "SEARCH|SORT"
        strParamValue = strSearch & "|ORDER BY MtbLnID"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objHvLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIMUATTBSLN_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objHvLnDs.Tables(0).Rows.Count > 0 Then
            cnt = objHvLnDs.Tables(0).Rows.Count
        End If

        For intCnt = 0 To objHvLnDs.Tables(0).Rows.Count - 1
            objHvLnDs.Tables(0).Rows(intCnt).Item("MtbLnID") = Trim(objHvLnDs.Tables(0).Rows(intCnt).Item("MtbLnID"))
        Next



        dgpremiln.PageSize = cnt + 1
        dgpremiln.DataSource = objHvLnDs
        dgpremiln.DataBind()
    End Sub


#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_PREMIMUATTBS_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "SEARCH|SORT"
        strParamValue = " AND MtbID Like '%" & strHvCode & "%' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIMUATTBS_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            strEmpCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpCode"))
            strEmpMandorCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("MandorCode"))
            strEmpKCSCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("KCSCode"))
            strEmpDivCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IdDiv"))
            strEmpBlockCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CodeBlok"))

            lbldivisicode.Text = strEmpDivCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("divisi")) & ")"
            lblEmpCode.Text = strEmpCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpName")) & ")"
            lblblokcode.Text = strEmpBlockCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("BlokName")) & ")"
            LblMandorCode.Text = strEmpMandorCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("MdrName")) & ")"
            LblKraniCode.Text = strEmpKCSCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("KcsName")) & ")"

            tothk.Value = toNumber(objEmpCodeDs.Tables(0).Rows(0).Item("TotalHK").ToString)
            totkg.Value = toNumber(objEmpCodeDs.Tables(0).Rows(0).Item("TotalKg").ToString)
            totprm.Value = toNumber(objEmpCodeDs.Tables(0).Rows(0).Item("SubTotalPremi").ToString)
            totDenda.Value = toNumber(objEmpCodeDs.Tables(0).Rows(0).Item("Denda").ToString)
            totupah.Value = toNumber(objEmpCodeDs.Tables(0).Rows(0).Item("Total").ToString)


            'BindHarvestLn(strHvCode)
            Hidblok.Value = GetBlokGol(strEmpBlockCode)
        End If
    End Sub

    Sub onLoad_button()
        If isNew.Value = "False" Then
            ddldivisicode.Visible = False
            ddlEmpCode.Visible = False
            ddlblokcode.Visible = False
            ddlMandorCode.Visible = False
            ddlKraniCode.Visible = False

            lbldivisicode.Visible = True
            lblEmpCode.Visible = True
            lblblokcode.Visible = True
            LblMandorCode.Visible = True
            LblKraniCode.Visible = True

        Else
            ddldivisicode.Visible = True
            ddlEmpCode.Visible = True
            ddlblokcode.Visible = True
            ddlMandorCode.Visible = True
            ddlKraniCode.Visible = True

            lbldivisicode.Visible = False
            lblEmpCode.Visible = False
            lblblokcode.Visible = False
            LblMandorCode.Visible = False
            LblKraniCode.Visible = False

        End If

    End Sub


    Sub ddldivisicode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpDivCode = ddldivisicode.SelectedItem.Value.Trim()
        BindMandor(strEmpDivCode)
        BindKCS(strEmpDivCode)
        BindEmployee(strEmpDivCode)
        BindBlok(strEmpDivCode)
    End Sub

    Sub ddlEmpCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpCode = ddlEmpCode.SelectedItem.Value.Trim()
        strEmpBlockCode = ddlblokcode.SelectedItem.Value.Trim()
        BindHarvestID(strEmpBlockCode, strEmpCode)
        BindHarvestLn(strHvCode)
    End Sub

    Sub ddlblokcode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim strBlok As String = ddlblokcode.SelectedItem.Value.Trim
        Hidblok.Value = GetBlokGol(strBlok)
    End Sub

    Sub KeepRunningSum_premi(ByVal sender As Object, ByVal E As DataGridItemEventArgs)
        If E.Item.ItemType = ListItemType.Footer Then
            E.Item.Cells(2).Text = tothk.Value
            E.Item.Cells(3).Text = totkg.Value
            E.Item.Cells(6).Text = totprm.Value
            E.Item.Cells(7).Text = totDenda.Value
            E.Item.Cells(8).Text = totupah.Value

        End If

    End Sub

    Sub GetItem_dtl(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)

        LblidD.Text = ""
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbl As Label


            lbl = dgpremiln.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblID")
            LblidD.Text = lbl.Text
            lbl = dgpremiln.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbldt")
            txtWPDate.Text = lbl.Text
            lbl = dgpremiln.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbhk")
            TxtPPanenHK.Text = lbl.Text
            lbl = dgpremiln.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbkg")
            TxtPPanenKg.Text = lbl.Text
            lbl = dgpremiln.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbrts")
            If Trim(lbl.Text) = "M" Then
                rbmuat.Checked = True
            Else
                rbbongkar.Checked = True
            End If
            lbl = dgpremiln.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbDbasis")
            txt_tdkbsis.Text = CInt(lbl.Text)

        End If
    End Sub

    Sub BtnSavePrm_OnClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strIDM As String = LblidM.Text
        Dim SEmpCode As String
        Dim SEmpName As String
        Dim SMdrCode As String
        Dim SKCSCode As String
        Dim SBlok As String '= IIf(isNew.Value = "False", ddlblokcode.SelectedItem.Value.Trim(), strEmpBlockCode)
        Dim SDate As String = Date_Validation(txtWPDate.Text, False)
        Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
        Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PR_PR_TRX_PREMIMUATTBS_UPD"
        Dim strOpCd_Cl As String = "PR_PR_TRX_PREMIMUATTBS_CALC"
        Dim intErrNo As Integer
        Dim Premi As String
        Dim Upah As String
        Dim tot As String
        Dim BM As String = ""


        If ddlEmpCode.Visible Then
            SEmpCode = ddlEmpCode.SelectedItem.Value.Trim()
            SEmpName = ddlEmpCode.SelectedItem.Text
        Else
            SEmpCode = Left(lblEmpCode.Text, InStr(lblEmpCode.Text, "(") - 1)
            SEmpName = Mid(Trim(lblEmpCode.Text), InStr(lblEmpCode.Text, "(") + 1, Len(Trim(lblEmpCode.Text)) - InStr(Trim(lblEmpCode.Text), "(") - 1)
        End If



        If ddlMandorCode.Visible Then
            SMdrCode = ddlMandorCode.SelectedItem.Value.Trim()
        Else
            SMdrCode = Left(LblMandorCode.Text, InStr(LblMandorCode.Text, "(") - 1)
        End If

        If ddlKraniCode.Visible Then
            SKCSCode = ddlKraniCode.SelectedItem.Value.Trim()
        Else
            SKCSCode = Left(LblKraniCode.Text, InStr(LblKraniCode.Text, "(") - 1)
        End If

        If ddlblokcode.Visible Then
            SBlok = ddlblokcode.SelectedItem.Value.Trim()
        Else
            SBlok = Left(lblblokcode.Text, InStr(lblblokcode.Text, "(") - 1)
        End If


        If SEmpCode = "" Then
            lblErrMessage.Text = "Silakan pilih Employee Code "
            lblErrMessage.Visible = True
            lblErrMessage.Attributes.Add("style", "text-decoration:blink")
            Exit Sub
        End If

        If SMdrCode = "" Then
            lblErrMessage.Text = "Silakan pilih Mandor Code "
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If SKCSCode = "" Then
            lblErrMessage.Text = "Silakan pilih KCS Code "
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If SBlok = "" Then
            lblErrMessage.Text = "Silakan pilih Blok Code "
            lblErrMessage.Visible = True
            Exit Sub
        End If

        TxtPPanenHK.Text = GetAttDate(SEmpCode, SDate)

        If TxtPPanenHK.Text = "0" Then
            lblErrMessage.Text = SEmpName & " belum melakukan absent di tanggal " & SDate & " !"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If TxtPPanenKg.Text = "0" Or TxtPPanenKg.Text = "" Then
            lblErrMessage.Text = "Silakan isi Kg !"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If (Not rbmuat.Checked) And (Not rbbongkar.Checked) Then
            lblErrMessage.Text = "Silakan pilih bongkar / muat !"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If txt_tdkbsis.Text = "" Then txt_tdkbsis.Text = "0"

        If rbmuat.Checked Then BM = "M"
        If rbbongkar.Checked Then BM = "B"
        Premi = Getpremi(BM)
        Upah = CSng(TxtPPanenKg.Text) * CSng(Premi)
        tot = CSng(Upah) - CSng(txt_tdkbsis.Text)

        ' Insert 2 Master & Detail Premi Panen

        ParamNama = "EC|MC|AM|AY|LC|CB|AI|KC|ST|CD|UD|UI|PD|HK|KG|BM|PM|SPM|DDA|TOT"
        ParamValue = SEmpCode & "|" & _
                     SMdrCode & "|" & _
                     SM & "|" & _
                     SY & "|" & _
                     strLocation & "|" & _
                     SBlok & "|" & _
                     strIDM & "|" & _
                     SKCSCode & "|1|" & _
                     DateTime.Now() & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId & "|" & _
                     SDate & "|" & _
                     TxtPPanenHK.Text & "|" & _
                     TxtPPanenKg.Text & "|" & _
                     BM & "|" & _
                     Premi & "|" & _
                     Upah & "|" & _
                     txt_tdkbsis.Text & "|" & _
                     tot


        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIMUATTBS_UPD&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")
        End Try

        'Calculate Premi
        ParamNama = "ID"
        ParamValue = strIDM

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Cl, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIMUATTBS_CALC&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")
        End Try


        onLoad_Display()
        BindHarvestLn(strHvCode)
        isNew.Value = "False"
    End Sub


    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_MuatTBSList_Estate.aspx")
    End Sub
#End Region


End Class
