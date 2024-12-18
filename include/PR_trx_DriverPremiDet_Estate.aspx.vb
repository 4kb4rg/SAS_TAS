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
Public Class PR_trx_DriverPremiDet_Estate : Inherits Page

#Region "Declaration"

    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgDrvDet As DataGrid
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents LblidM As Label
    Protected WithEvents LblidD As Label
    Protected WithEvents txtWPDate As TextBox
    Protected WithEvents ddlEmpCode As DropDownList
    Protected WithEvents ddlEmpCode_Kenek1 As DropDownList
    Protected WithEvents ddlEmpCode_Kenek2 As DropDownList
    Protected WithEvents ddlJobDesc As DropDownList
    Protected WithEvents ddlCarNo As DropDownList

    Protected WithEvents LbPsn1 As Label
    Protected WithEvents LbPsn2 As Label

    Protected WithEvents txtpsn1 As TextBox
    Protected WithEvents txtpsn2 As TextBox
    Protected WithEvents TxtDrvCarNo As TextBox
    Protected WithEvents TxtDrvKg As TextBox
    Protected WithEvents TxtDrvTrip As TextBox
    Protected WithEvents TxtDrvBasis As TextBox
    Protected WithEvents TxtDrvPremi As TextBox
    Protected WithEvents TxtDrvTotal As TextBox

    Protected WithEvents TmpDrvBasis As HtmlInputHidden
    Protected WithEvents TmpDrvBasisUOM As HtmlInputHidden
    Protected WithEvents TmpDrvTotal As HtmlInputHidden
    Protected WithEvents TmpDrvPremi As HtmlInputHidden

    Protected WithEvents totkg As HtmlInputHidden
    Protected WithEvents tottrip As HtmlInputHidden
    Protected WithEvents totpremi As HtmlInputHidden

    Protected WithEvents LblLbr1 As Label
    Protected WithEvents LblLbr2 As Label
    Protected WithEvents LblLbr3 As Label

    Protected WithEvents LblAmntP1 As Label
    Protected WithEvents LblAmntP2 As Label
    Protected WithEvents LblAmntP3 As Label

    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblCarNo As Label
    Protected WithEvents lblKenek1 As Label
    Protected WithEvents lblPsn1 As Label
    Protected WithEvents lblKenek2 As Label
    Protected WithEvents lblPsn2 As Label



    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DeleteBtn As ImageButton

    Protected WithEvents isNew As HtmlInputHidden

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

    Dim intStatus As Integer
    Dim TarifLembur As Single
    Dim strAcceptFormat As String

    Dim strDrvPrmCode As String = ""
    Dim StrDrvPrmLnCode As String = ""
    Dim strEmpCode As String = ""
    Dim strKenekCode_1 As String = ""
    Dim strKenekCode_2 As String = ""


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
            lblErrMessage.Visible = False
            strDrvPrmCode = Trim(IIf(Request.QueryString("PrmDrvID") <> "", Request.QueryString("PrmDrvID"), Request.Form("PrmDrvID")))

            If Not IsPostBack Then
                txtWPDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                If strDrvPrmCode <> "" Then
                    isNew.Value = "False"
                    LblidM.Text = strDrvPrmCode
                    onLoad_Display()
                Else
                    isNew.Value = "True"
                    LblidM.Text = getCode()
                    BindJob("")
                    BindCarNo("")
                    BindEmployee("")
                End If
                onLoad_button()
            End If

            If LblidM.Text <> "" Then
                BindPremiDriverLn(LblidM.Text)
            End If

            If TxtDrvKg.Text <> "" Or TxtDrvTrip.Text <> "" Then
                TxtDrvBasis.Text = TmpDrvPremi.Value
                TxtDrvTotal.Text = TmpDrvTotal.Value
            End If
        End If
    End Sub

#End Region

#Region "Procedure & Function"

    Sub onLoad_button()
        If isNew.Value = "False" Then
            ddlEmpCode.Visible = False
            ddlEmpCode_Kenek1.Visible = False
            ddlEmpCode_Kenek2.Visible = False
            ddlCarNo.Visible = False
            txtpsn1.Visible = False
            txtpsn2.Visible = False


            lblEmpCode.Visible = True
            lblCarNo.Visible = True
            lblKenek1.Visible = True
            lblPsn1.Visible = True
            lblKenek2.Visible = True
            lblPsn2.Visible = True

        Else
            ddlEmpCode.Visible = True
            ddlEmpCode_Kenek1.Visible = True
            ddlEmpCode_Kenek2.Visible = True
            ddlCarNo.Visible = True
            txtpsn1.Visible = True
            txtpsn2.Visible = True

            lblEmpCode.Visible = False
            lblCarNo.Visible = False
            lblKenek1.Visible = False
            lblPsn1.Visible = False
            lblKenek2.Visible = False
            lblPsn2.Visible = False

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

    Function getCode() As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        tcode = "DRV/" & strLocation & "/" & Mid(Trim(txtWPDate.Text), 4, 2) & Right(Trim(txtWPDate.Text), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|13|" & tcode & "|5"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Function toNumber(ByVal s As String) As String
        If (s = "" Or s = "NULL") Then
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber("0", 2), 2)
        Else
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(s, 2), 2)
        End If

    End Function

    Sub Getbasis(ByVal s As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_PREMI_SUPIR_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer


        strParamName = "SEARCH|SORT"
        strParamValue = "AND PRDriverCode='" & s & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_SUPIR_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count = 1 Then
            TxtDrvPremi.Text = objEmpDivDs.Tables(0).Rows(0).Item("Rate")
            TmpDrvBasis.Value = objEmpDivDs.Tables(0).Rows(0).Item("Basis")
            TmpDrvBasisUOM.Value = Trim(objEmpDivDs.Tables(0).Rows(0).Item("unit"))
        End If


    End Sub

    Sub clearAll()
        LblAmntP1.Text = toNumber("0")
        LblAmntP2.Text = toNumber("0")
        LblAmntP3.Text = toNumber("0")
    End Sub

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

    Sub BindJob(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_PREMI_SUPIR_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.Status='1'|ORDER BY CodeJob"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_PREMI_SUPIR_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("PRDriverCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("PRDriverCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("CodeJob") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CodeJob"))
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("PRDriverCode") = " "
        dr("CodeJob") = "Please Select Job Desc"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlJobDesc.DataSource = objEmpDivDs.Tables(0)
        ddlJobDesc.DataTextField = "CodeJob"
        ddlJobDesc.DataValueField = "PRDriverCode"
        ddlJobDesc.DataBind()
        ddlJobDesc.SelectedIndex = 0

    End Sub

    Sub BindEmployee(ByVal strDivCode As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim intSelectedIndex_2 As Integer = 0
        Dim intSelectedIndex_3 As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%' AND JobCode='SPR'|ORDER BY A.EmpName"

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

                If objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(strKenekCode_1) Then
                    intSelectedIndex_2 = intCnt + 1
                End If

                If objEmpCodeDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(strKenekCode_2) Then
                    intSelectedIndex_3 = intCnt + 1
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

        ddlEmpCode_Kenek1.DataSource = objEmpCodeDs.Tables(0)
        ddlEmpCode_Kenek1.DataTextField = "_Description"
        ddlEmpCode_Kenek1.DataValueField = "EmpCode"
        ddlEmpCode_Kenek1.DataBind()
        ddlEmpCode_Kenek1.SelectedIndex = intSelectedIndex_2

        ddlEmpCode_Kenek2.DataSource = objEmpCodeDs.Tables(0)
        ddlEmpCode_Kenek2.DataTextField = "_Description"
        ddlEmpCode_Kenek2.DataValueField = "EmpCode"
        ddlEmpCode_Kenek2.DataBind()
        ddlEmpCode_Kenek2.SelectedIndex = intSelectedIndex_3

    End Sub

    Sub BindPremiDriverID(ByVal Str_EmpCode As String)

        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_PREMIDRIVER_GET"
        Dim strParamName As String
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "SEARCH"

        strParamValue = " AND EmpCode = '" & Str_EmpCode & "' AND LocCode='" & strLocation & "'"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIDRIVER_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            strDrvPrmCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("PrmDrvID").ToString)
            LblidM.Text = strDrvPrmCode
        End If
    End Sub

    Sub BindCarNo(ByVal sCarNo As String)
        Dim strOpCd_EmpDiv As String = "GL_CLSSETUP_VEHICLE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objcarno As New Object
        Dim dr As DataRow


        strParamName = "STRSEARCH"
        strParamValue = "AND veh.Status='1'"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objcarno)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objcarno.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objcarno.Tables(0).Rows.Count - 1
                objcarno.Tables(0).Rows(intCnt).Item("VehCode") = Trim(objcarno.Tables(0).Rows(intCnt).Item("VehCode"))
                objcarno.Tables(0).Rows(intCnt).Item("Description") = Trim(objcarno.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objcarno.Tables(0).NewRow()
        dr("VehCode") = ""
        dr("Description") = "Please Select Car No"
        objcarno.Tables(0).Rows.InsertAt(dr, 0)

        ddlCarNo.DataSource = objcarno.Tables(0)
        ddlCarNo.DataTextField = "Description"
        ddlCarNo.DataValueField = "VehCode"
        ddlCarNo.DataBind()
        ddlCarNo.SelectedIndex = 0

    End Sub

    Sub BindPremiDriverLn(ByVal StrDrvPrmID As String)
        Dim strOpCd_Get As String = "PR_PR_TRX_PREMIDRIVERLN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objHvLnDs As New Object()
        Dim cnt As Integer = 0

        strSearch = "AND IDPrmDrv='" & StrDrvPrmID & "'"

        strParamName = "SEARCH|SORT"
        strParamValue = strSearch & "|ORDER BY PrmDrvLnID"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_Get, strParamName, strParamValue, objHvLnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIDRIVERLN_GET&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        If objHvLnDs.Tables(0).Rows.Count > 0 Then
            cnt = objHvLnDs.Tables(0).Rows.Count
        End If

        For intCnt = 0 To objHvLnDs.Tables(0).Rows.Count - 1
            objHvLnDs.Tables(0).Rows(intCnt).Item("PrmDrvLnID") = Trim(objHvLnDs.Tables(0).Rows(intCnt).Item("PrmDrvLnID"))
            objHvLnDs.Tables(0).Rows(intCnt).Item("CodeJob") = Trim(objHvLnDs.Tables(0).Rows(intCnt).Item("CodePRDriver")) & " (" & Trim(objHvLnDs.Tables(0).Rows(intCnt).Item("CodeJob")) & ")"
        Next



        dgDrvDet.PageSize = cnt + 1
        dgDrvDet.DataSource = objHvLnDs
        dgDrvDet.DataBind()
    End Sub

#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_PREMIDRIVER_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "SEARCH|SORT"
        strParamValue = " AND PrmDrvID Like '%" & strDrvPrmCode & "%' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIDRIVER_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            strEmpCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpCode"))
            lblCarNo.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CarNo"))
            strKenekCode_1 = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Kenek1"))
            txtpsn1.Text = objEmpCodeDs.Tables(0).Rows(0).Item("PsnKenek1")
            strKenekCode_2 = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Kenek2"))
            txtpsn2.Text = objEmpCodeDs.Tables(0).Rows(0).Item("PsnKenek2")

            totkg.Value = objEmpCodeDs.Tables(0).Rows(0).Item("TotalKg")
            tottrip.Value = objEmpCodeDs.Tables(0).Rows(0).Item("TotalTrip")
            totpremi.Value = objEmpCodeDs.Tables(0).Rows(0).Item("TotalPremi")

            LblAmntP2.Text = objEmpCodeDs.Tables(0).Rows(0).Item("PremiKenek1")
            LblAmntP3.Text = objEmpCodeDs.Tables(0).Rows(0).Item("PremiKenek2")

            LblAmntP1.Text = totpremi.Value
            LblLbr1.Text = LblAmntP1.Text
            LblLbr2.Text = LblAmntP1.Text
            LblLbr3.Text = LblAmntP1.Text

            lblEmpCode.Text = strEmpCode
            lblKenek1.Text = strKenekCode_1
            lblPsn1.Text = txtpsn1.Text
            lblKenek2.Text = strKenekCode_2
            lblPsn2.Text = txtpsn2.Text
        End If

    End Sub

    Sub ddlJobDesc_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim Strcd As String = ddlJobDesc.SelectedItem.Value.Trim()
        Getbasis(Strcd)
        TxtDrvKg.Text = ""
        TxtDrvTrip.Text = ""
    End Sub

    Sub KeepRunningSum_premi(ByVal sender As Object, ByVal E As DataGridItemEventArgs)
        If E.Item.ItemType = ListItemType.Footer Then
            E.Item.Cells(3).Text = totkg.Value
            E.Item.Cells(4).Text = tottrip.Value
            E.Item.Cells(7).Text = totpremi.Value
        End If
    End Sub
    Sub SaveBtn_OnClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strIDM As String = LblidM.Text
        Dim SEmpCode As String
        Dim SEmpName As String
        Dim SCarNo As String
        Dim SK1Code As String
        Dim SK2Code As String
        Dim SPsn1 As String
        Dim SPsn2 As String
        Dim SDate As String = Date_Validation(txtWPDate.Text, False)
        Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
        Dim SHadir As String
        Dim SKg As String = TxtDrvKg.Text
        Dim STrip As String = TxtDrvTrip.Text

        Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Up As String = "PR_PR_TRX_PREMIDRIVER_UPD"
        Dim strOpCd_Cl As String = "PR_PR_TRX_PREMIDRIVER_CALC"
        Dim intErrNo As Integer
        

        If ddlEmpCode.Visible Then
            SEmpCode = ddlEmpCode.SelectedItem.Value.Trim()
            SEmpName = ddlEmpCode.SelectedItem.Text
        Else
            SEmpCode = Left(lblEmpCode.Text, InStr(lblEmpCode.Text, "(") - 1)
            SEmpName = Mid(Trim(lblEmpCode.Text), InStr(lblEmpCode.Text, "(") + 1, Len(Trim(lblEmpCode.Text)) - InStr(Trim(lblEmpCode.Text), "(") - 1)
        End If

        If ddlCarNo.Visible Then
            SCarNo = ddlCarNo.SelectedItem.Value.Trim()
        Else
            SCarNo = lblCarNo.Text
        End If

        If ddlEmpCode_Kenek1.Visible Then
            SK1Code = ddlEmpCode_Kenek1.SelectedItem.Value.Trim()
        Else
            SK1Code = Left(lblKenek1.Text, InStr(lblKenek1.Text, "(") - 1)
        End If

        If ddlEmpCode_Kenek2.Visible Then
            SK2Code = ddlEmpCode_Kenek1.SelectedItem.Value.Trim()
        Else
            SK2Code = Left(lblKenek2.Text, InStr(lblKenek2.Text, "(") - 1)
        End If

        If txtpsn1.Visible Then
            SPsn1 = txtpsn1.Text
        Else
            SPsn1 = LbPsn1.Text
        End If

        If txtpsn2.Visible Then
            SPsn2 = txtpsn2.Text
        Else
            SPsn2 = LbPsn2.Text
        End If


        If SEmpCode = "" Then
            lblErrMessage.Text = "Silakan pilih Employee Code "
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If SCarNo = "" Then
            lblErrMessage.Text = "Silakan isi No.Kendaraan "
            lblErrMessage.Visible = True
            Exit Sub
        End If

        SHadir = GetAttDate(SEmpCode, SDate)

        If SHadir = "0" Then
            lblErrMessage.Text = SEmpName & " belum melakukan absent di tanggal " & SDate & " !"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If ddlJobDesc.SelectedItem.Value.Trim = "" Then
            lblErrMessage.Text = "Silakan pilih pekerjaan"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If (TxtDrvKg.Text = "" And TxtDrvTrip.Text = "") Then
            lblErrMessage.Text = "Silakan isi Kg/Trip"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If TxtDrvKg.Text = "" Then SKg = "0"
        If TxtDrvTrip.Text = "" Then STrip = "0"
        If SPsn1 = "" Then SPsn1 = "0"
        If SPsn2 = "" Then SPsn2 = "0"



        ' Insert 2 Master & Detail Premi Panen

        ParamNama = "EC|AM|AY|LC|CN|AI|K1|P1|K2|P2|ST|CD|UD|UI|PD|JB|KG|TP|PM|RT|TA"
        ParamValue = SEmpCode & "|" & _
                     SM & "|" & _
                     SY & "|" & _
                     strLocation & "|" & _
                     SCarNo & "|" & _
                     strIDM & "|" & _
                     SK1Code & "|" & _
                     SPsn1 & "|" & _
                     SK2Code & "|" & _
                     SPsn2 & "|1|" & _
                     DateTime.Now() & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId & "|" & _
                     SDate & "|" & _
                     ddlJobDesc.SelectedItem.Value.Trim & "|" & _
                     SKg & "|" & _
                     STrip & "|" & _
                     TmpDrvPremi.Value & "|" & _
                     TxtDrvPremi.Text & "|" & _
                     TmpDrvTotal.Value


        Response.Write(ParamValue)


        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIDRIVER_CALC&errmesg=" & ex.ToString() & "&redirect=")
        End Try

        'Calculate Premi
        ParamNama = "ID"
        ParamValue = strIDM

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd_Cl, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_PREMIBRONDOLAN_CALC&errmesg=" & ex.ToString() & "&redirect=PR/Setup/PR_SETUP_CUTOFF_ESTATE.aspx")
        End Try


        onLoad_Display()
        BindPremiDriverLn(LblidM.Text)
        isNew.Value = "False"

    End Sub

    Sub BackBtn_OnClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_DriverPremiList_Estate.aspx")
    End Sub
#End Region






End Class
