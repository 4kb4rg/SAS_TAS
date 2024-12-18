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
Public Class HR_trx_MutasiDet_Estate : Inherits Page

#Region "Declare Var"


    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents idMDR As HtmlInputHidden
    Protected WithEvents isNew As HtmlInputHidden
    Protected WithEvents idWH As HtmlInputHidden
    Protected WithEvents LblidM As Label
    Protected WithEvents lbldivisilama As Label
    Protected WithEvents lbljabatanlama As Label
    Protected WithEvents lblempcode As Label
    Protected WithEvents lbldivisibaru As Label
    Protected WithEvents lbljabatanbaru As Label
    Protected WithEvents lblefektifdate As Label

    Protected WithEvents ddldivisicode As DropDownList
    Protected WithEvents ddlempcode As DropDownList
    Protected WithEvents ddldivisibaru As DropDownList
    Protected WithEvents ddljabatanbaru As DropDownList

    Protected WithEvents txtefektifdate As TextBox
    Protected WithEvents txtdoc As TextBox
    Protected WithEvents txtket As TextBox

    Protected WithEvents btnSelDate As Image
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents BackBtn As ImageButton

    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label

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
    Dim strLocationName As String
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

    Dim strID As String = ""
    Dim strEmpCode As String = ""
    Dim strEmpDivCode As String = ""
    Dim strAcceptFormat As String

#End Region

#Region "Page Load"
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strLocationName = Session("SS_LOCATIONNAME")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
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
            strID = Trim(IIf(Request.QueryString("MutasiCode") <> "", Request.QueryString("MutasiCode"), Request.Form("MutasiCode")))

            lblErrMessage.Visible = False
            If Not IsPostBack Then
                txtefektifdate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                If strID <> "" Then
                    isNew.Value = "False"
                    onLoad_Display()
                Else
                    isNew.Value = "True"
                    LblidM.Text = getCode()
                    BindDivision("")
                    BindJabatan("")
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

    Function getCode() As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String
        Dim dt As String = objGlobal.GetShortDate(strDateFmt, Now())

        tcode = "MTS/" & strLocation & "/" & Mid(Trim(dt), 4, 2) & Right(Trim(dt), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|3|" & tcode & "|3"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Function getCode2(ByVal hdr As String, ByVal id As String) As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim dt As String = objGlobal.GetShortDate(strDateFmt, Now())
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|" & id & "|" & hdr & "|6"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Function getJabatanLama(ByVal ec As String) As String
        Dim strOpCd As String = "HR_HR_TRX_EMPWORK_GET_BY_EMPCODE"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer

        ParamName = "SEARCH|SORT"
        ParamValue = "AND x.codeemp='" & ec & "'|ORDER By JbtCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_JABATAN_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=HR/trx/HR_trx_EmployeeList.aspx")
        End Try

        getJabatanLama = Trim(objDs.Tables(0).Rows(0).Item("jbt")) & " (" & Trim(objDs.Tables(0).Rows(0).Item("Codejbt")) & ")"

    End Function

    Sub clearAll()
        LblidM.Text = ""
    End Sub

    Sub onLoad_button()
        If strID <> "" Then
            lbldivisilama.Visible = True
            lbldivisibaru.Visible = True
            lbljabatanbaru.Visible = True
            lblempcode.Visible = True
            ddldivisicode.Visible = False
            ddlempcode.Visible = False
            ddldivisibaru.Visible = False
            ddljabatanbaru.Visible = False
        Else
            lbldivisilama.Visible = False
            lbldivisibaru.Visible = False
            lbljabatanbaru.Visible = False
            lblempcode.Visible = False
            ddldivisicode.Visible = True
            ddlempcode.Visible = True
            ddldivisibaru.Visible = True
            ddljabatanbaru.Visible = True
        End If
    End Sub

    Sub Save_Work(ByVal id As String)
        Dim strOpCd As String = "HR_HR_TRX_EMPWORK_UPD"
        Dim strYM As String = Mid(txtefektifdate.Text.Trim, 4, 2) + Right(txtefektifdate.Text.Trim, 4)

        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String


        ParamNama = "ID|CE|CN|PS|PE|JB|LOC|CD|UD|UI|CDV|CJB"
        ParamValue = id.Trim() & "|" & _
                     ddlempcode.SelectedItem.Value.Trim() & "|" & _
                     strLocationName & "|" & _
                     strYM & "|" & _
                     "000000" & "|" & _
                     ddldivisibaru.SelectedItem.Text.Trim() & "-" & ddljabatanbaru.SelectedItem.Text.Trim() & "|" & _
                     strLocation & "|" & _
                     Now() & "|" & _
                     Now() & "|" & _
                     strUserId & "|" & _
					 ddldivisibaru.SelectedItem.Value.Trim() & "|" & _
					 ddljabatanbaru.SelectedItem.Value.Trim()


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
            Next
        End If

        dr = ObjDiv.Tables(0).NewRow()
        dr("BlkGrpCode") = " "
        dr("Description") = "Please Select Division"
        ObjDiv.Tables(0).Rows.InsertAt(dr, 0)

        ddldivisicode.DataSource = ObjDiv.Tables(0)
        ddldivisicode.DataTextField = "Description"
        ddldivisicode.DataValueField = "BlkGrpCode"
        ddldivisicode.DataBind()
        ddldivisicode.SelectedIndex = 0

        ddldivisibaru.DataSource = ObjDiv.Tables(0)
        ddldivisibaru.DataTextField = "Description"
        ddldivisibaru.DataValueField = "BlkGrpCode"
        ddldivisibaru.DataBind()
        ddldivisibaru.SelectedIndex = 0

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
		Dim strAm as String

		If Cint(strAccMonth) < 10 Then
            strAM = "0" & rtrim(strAccMonth)
        Else
            strAM = rtrim(strAccMonth)
        End If


        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & strAM & "|" & strAccyear & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%'|ORDER BY A.EmpName"

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
        ParamValue = "WHERE LocCode='"+ strLocation + "' AND Status='1'|ORDER By Description"

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
        dr("Description") = "Select Jabatan"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddljabatanbaru.DataSource = objDs.Tables(0)
        ddljabatanbaru.DataTextField = "Description"
        ddljabatanbaru.DataValueField = "JbtCode"
        ddljabatanbaru.DataBind()
        ddljabatanbaru.SelectedIndex = intselectIndex

    End Sub

#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPMUTASI_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & " AND MutasiCode Like '%" & strID & "%' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMUTASI_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            LblidM.Text = strID
            strEmpCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpCode"))
            txtdoc.text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("DocNo"))
            lbldivisilama.Text = objEmpCodeDs.Tables(0).Rows(0).Item("Divisi_Asal") & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("divisiA")) & ")"
            lbldivisibaru.Text = objEmpCodeDs.Tables(0).Rows(0).Item("Divisi_Baru") & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("divisiB")) & ")"
            lbljabatanlama.Text = objEmpCodeDs.Tables(0).Rows(0).Item("Jabatan_Asal") & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("JabatanA")) & ")"
            lbljabatanbaru.Text = objEmpCodeDs.Tables(0).Rows(0).Item("Jabatan_Baru") & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("JabatanB")) & ")"
            lblempcode.Text = strEmpCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpName")) & ")"
            txtefektifdate.Text = Date_Validation(objEmpCodeDs.Tables(0).Rows(0).Item("EfektifDate"), True)
            txtket.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Notes"))

            lblStatus.Text = IIf(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Status")) = "1", "Aktive", "Delete")
            lblDateCreated.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("uName"))
            idWH.Value = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IdWorkHist"))

        End If
    End Sub


    Sub ddldivisicode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpDivCode = ddldivisicode.SelectedItem.Value.Trim()
        BindEmployee(strEmpDivCode)
    End Sub

    Sub ddlempcode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        lbljabatanlama.Text = getJabatanLama(ddlempcode.SelectedItem.Value.Trim())
    End Sub

    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "HR_HR_TRX_EMPMUTASI_UPD"
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String
        Dim strdtefektif As String

        If ddlempcode.Text = "" Then
            lblErrMessage.Text = "Silakan pilih employee code !!"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If ddldivisibaru.SelectedItem.Value = "" Then
            lblErrMessage.Text = "Silakan pilih divisi baru !!"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        If ddljabatanbaru.Text = "" Then
            lblErrMessage.Text = "Silakan pilih jabatan baru !!"
            lblErrMessage.Visible = True
            Exit Sub
        End If

        strdtefektif = Date_Validation(txtefektifdate.Text.Trim(), False)
        If strdtefektif = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan input tanggal efektif !"
            Exit Sub
        End If

        If txtdoc.Text.Trim = "" Then
            lblErrMessage.Visible = True
            lblErrMessage.Text = "Silakan input doc no !"
            Exit Sub
        End If


        If isNew.Value = "True" Then
            idWH.Value = getCode2("EWKH/" & strLocation & "/", "45")
            Save_Work(idWH.Value.Trim())
        End If


        ParamNama = "MC|DOC|MD|CE|DA|JA|ED|DB|JB|KET|LOC|CD|UD|UI|WH"
        ParamValue = LblidM.Text.Trim() & "|" & _
                     txtdoc.Text.Trim() & "|" & _
                     Date_Validation(Format(Now(), "dd/MM/yyyy"), False) & "|" & _
                     ddlempcode.SelectedItem.Value.Trim() & "|" & _
                     ddldivisicode.SelectedItem.Value.Trim() & "|" & _
                     Mid(Trim(lbljabatanlama.Text), InStr(lbljabatanlama.Text, "(") + 1, Len(Trim(lbljabatanlama.Text)) - InStr(Trim(lbljabatanlama.Text), "(") - 1) & "|" & _
                     strdtefektif & "|" & _
                     ddldivisibaru.SelectedItem.Value.Trim() & "|" & _
                     ddljabatanbaru.SelectedItem.Value.Trim() & "|" & _
                     txtket.Text.Trim() & "|" & _
                     strLocation & "|" & _
                     Now() & "|" & _
                     Now() & "|" & _
                     strUserId & "|" & _
                     idWH.Value.Trim()


        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=" & Exp.Message & "&redirect=")
        End Try

      

        BackBtn_Click(Sender, E)
    End Sub

    Sub DelBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "HR_HR_TRX_EMPMUTASI_DEL"
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String

        ParamNama = "ST|UD|UI|MC|WH"
        ParamValue = "2|" & Now() & "|" & strUserId & "|" & LblidM.Text.Trim() & "|" & idWH.Value.Trim()

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOpCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_TRX_EMPMUTASI_DEL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        BackBtn_Click(Sender, E)
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("HR_trx_MutasiList_Estate.aspx")
    End Sub


#End Region

End Class
