Imports System

Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.HR
Imports agri.GlobalHdl.clsGlobalHdl

Public Class PR_trx_DailyAttdGenerate_Estate : Inherits Page

    Protected WithEvents lblRedirect As Label
    Protected WithEvents lblReback As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents ddldivisicode As DropDownList
    Protected WithEvents ddlmandorcode As DropDownList
    Protected WithEvents ddlempcode As DropDownList
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlabsent As DropDownList
    Protected WithEvents txtyear As TextBox
    Protected WithEvents txtstartdt As TextBox
    Protected WithEvents txtenddt As TextBox
    Protected WithEvents rbbulan As RadioButton
    Protected WithEvents rbtanggal As RadioButton
    Protected WithEvents ref As HiddenField

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

    Dim strAttDate As String = ""
    Dim StrEmpCode As String = ""
    Dim StrEmpName As String = ""
    Dim StrEmpTy As String = ""

    Dim StrAMonth As String = ""
    Dim StrAYear As String = ""


    Dim StrAttCode As String = ""
    Dim StrAttType As String = ""
    Dim StrAttQty As String = ""
    Dim strAttDatetmp As String = ""
    Dim StrPrev As String = ""
    Dim ListEmp As String
    Dim strDateFmt As String
    Dim strAcceptFormat As String
    Dim prevset As String

    Function getCode() As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        tcode = "ATT/" & strLocation & "/" & ddlMonth.SelectedItem.Value.Trim() & Right(txtyear.Text, 2) & "/"

        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|5|" & tcode & "|5"

        Try
            intErrNo = Objok.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))
    End Function

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


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strDateFmt = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            lblRedirect.Text = Request.QueryString("redirect")
            StrAMonth = Trim(IIf(Request.QueryString("AccMonth") <> "", Request.QueryString("AccMonth"), Request.Form("AccMonth")))
            StrAYear = Trim(IIf(Request.QueryString("AccYear") <> "", Request.QueryString("AccYear"), Request.Form("AccYear")))
            lblErrMessage.Visible = False
            ref.Value = Session("ATT")
            If Not IsPostBack Then
                ddlMonth.SelectedValue = StrAMonth
                txtyear.Text = StrAYear
                txtstartdt.Text = "01/" & StrAMonth & "/" & StrAYear

                Dim ed As String = DateTime.DaysInMonth(CInt(StrAYear), CInt(StrAMonth))
                txtenddt.Text = ed & "/" & StrAMonth & "/" & StrAYear
                BindDivisi()
            End If
        End If

    End Sub
    Sub BindDivisi()
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
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description"))
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "All"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)



        ddldivisicode.DataSource = objEmpDivDs.Tables(0)
        ddldivisicode.DataTextField = "Description"
        ddldivisicode.DataValueField = "BlkGrpCode"
        ddldivisicode.DataBind()
        ddldivisicode.SelectedIndex = 0

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


        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & ddlMonth.SelectedItem.Value & "|" & txtyear.Text & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strdivcode & "%' AND (isMandor<>'0')|ORDER BY A.EmpName"

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

        ddlmandorcode.DataSource = objEmpCodeDs.Tables(0)
        ddlmandorcode.DataTextField = "_Description"
        ddlmandorcode.DataValueField = "EmpCode"
        ddlmandorcode.DataBind()
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



        strParamName = "LOC|AM|AY|SEARCH|SORT"
        If MandorCode <> "" Then
		'by aam 9 sep 2012
            tmpParam = "AND A.EmpCode IN (SELECT distinct y.CodeEmp FROM hr_trx_empmandor x,hr_trx_empmandorln y WHERE x.mandorcode=y.codemandor and x.codeemp='" & MandorCode & "') "
        Else
            tmpParam = ""
        End If
        strParamValue = strLocation & "|" & ddlMonth.SelectedItem.Value & "|" & txtyear.Text & "|" & tmpParam & " AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strdivcode & "%' |ORDER BY A.EmpName"

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

        ddlempcode.DataSource = objEmpCodeDs.Tables(0)
        ddlempcode.DataTextField = "_Description"
        ddlempcode.DataValueField = "EmpCode"
        ddlempcode.DataBind()
    End Sub

    Sub ddldivisicode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindMandoran(ddldivisicode.SelectedItem.Value.Trim())
        BindEmployee(ddldivisicode.SelectedItem.Value.Trim(), "")
    End Sub

    Sub ddlmandorcode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindEmployee(ddldivisicode.SelectedItem.Value.Trim(), ddlmandorcode.SelectedItem.Value.Trim())
    End Sub

    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "PR_PR_TRX_ATTENDANCE_GENERATE"
        Dim ParamName As String
        Dim ParamValue As String
        Dim Paramtmp As String = ""
        Dim intErrNo As Integer
        Dim sd As String
        Dim ed As String

        If Trim(ddldivisicode.SelectedItem.Value.Trim()) = "" Then
            lblErrMessage.Text = "Silakan pilih divisi"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If (Not rbbulan.Checked) And (Not rbtanggal.Checked) Then
            lblErrMessage.Text = "Silakan pilih berdasarkan bulan / tanggal"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If rbbulan.Checked Then
            Dim mn As String = ddlMonth.SelectedItem.Value.Trim()
            Dim yr As String = txtyear.Text.Trim()

            sd = "01/" & mn & "/" & yr
            Dim edtmp As String = DateTime.DaysInMonth(CInt(yr), CInt(mn))
            ed = edtmp & "/" & StrAMonth & "/" & StrAYear
            sd = Date_Validation(sd, False)
            ed = Date_Validation(ed, False)
        Else
            sd = Date_Validation(txtstartdt.Text, False)
            ed = Date_Validation(txtenddt.Text, False)
            If sd = "" Or ed = "" Then
                lblErrMessage.Text = "isi format tanggal dd/mm/yyyy"
                lblErrMessage.Visible = True
                Exit Sub
            End If
        End If

        ParamName = "LOC|HD|UI|SD|ED|DI|MC|EC|AT"
        ParamValue = strLocation & "|" & _
                     "ATT/" & strLocation & "/|" & _
                     strUserId & "|" & _
                     sd & "|" & _
                     ed & "|" & _
                     ddldivisicode.SelectedItem.Value.Trim() & "|" & _
                     ddlmandorcode.SelectedItem.Value.Trim() & "|" & _
                     ddlempcode.SelectedItem.Value.Trim() & "|" & _
                     ddlabsent.SelectedItem.Value.Trim()
        Try
            intErrNo = Objok.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_ATTENDANCE_UPD&errmesg=" & Exp.Message & "&redirect=PR/trx/PR_trx_DailyAttd_ESTATE.aspx")
        End Try
        Session("ATT") = ref.Value
        Response.Write("<Script Language=""JavaScript"">window.location.href=""PR_Trx_AttdCheckrolllist_Estate.aspx"";window.close(); </Script>")
    End Sub


    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Session("ATT") = ref.Value
        Response.Write("<Script Language=""JavaScript"">window.location.href=""PR_Trx_AttdCheckrolllist_Estate.aspx"";window.close(); </Script>")
    End Sub

End Class
