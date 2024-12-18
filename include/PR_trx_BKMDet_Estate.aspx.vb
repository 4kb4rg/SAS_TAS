Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Collections.Generic
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Infragistics.WebUI.UltraWebTab

Public Class PR_BKMDet_Estate : Inherits Page

#Region "Declare Var"

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents isNew As HtmlInputHidden
    Protected WithEvents hid_cat As HtmlInputHidden
    Protected WithEvents hid_subcat As HtmlInputHidden
    Protected WithEvents hid_div As HtmlInputHidden
    Protected WithEvents txt_hid_emp As HtmlInputHidden

#Region "Var BK"
    Protected WithEvents txtWPDate As TextBox
    Protected WithEvents ddlbkcategory As DropDownList
    Protected WithEvents ddlbksubcategory As DropDownList
    Protected WithEvents LblidM As Label
    Protected WithEvents ddldivisicode As DropDownList
    Protected WithEvents ddlMandorCode As DropDownList
    Protected WithEvents ddlKCSCode As DropDownList

    Protected WithEvents lblWPDate As Label
    Protected WithEvents btnSelDate As Image
    Protected WithEvents lblbkcategory As Label
    Protected WithEvents lblbksubcategory As Label
    Protected WithEvents lbldivisicode As Label
    Protected WithEvents lblMandorCode As Label
    Protected WithEvents lblKCSCode As Label
    Protected WithEvents lbltxtmandor As Label
    Protected WithEvents TABBK As UltraWebTab

    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblupdatedby As Label


    Protected WithEvents SaveBtn As ImageButton

    Dim strBKCategory As String
    Dim strBKSubCategory As String
    Dim strEmpMandorCode As String
    Dim strEmpKCSCode As String
    Dim strEmpDivCode As String
    Dim hastbl_jb As New System.Collections.Hashtable()
    Dim hastbl_cb As New System.Collections.Hashtable()
    Dim hastbl_jbbhn As New System.Collections.Hashtable()
    Dim hastbl_bhn As New System.Collections.Hashtable()
    Dim hastbl_jbbms As New System.Collections.Hashtable()



#End Region

#Region "Var RW"
    Protected WithEvents dgRW As DataGrid
#End Region

#Region "Var PN"
    Protected WithEvents dgPBS As DataGrid
    Protected WithEvents dgKBS As DataGrid
    Protected WithEvents dgBMS As DataGrid

#End Region

#Region "var BHN"
    Protected WithEvents dgBHN As DataGrid
#End Region

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

    Dim strBKMCode As String = ""
    Dim intStatus As Integer
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
            strBKMCode = Trim(IIf(Request.QueryString("BKMCode") <> "", Request.QueryString("BKMCode"), Request.Form("BKMCode")))
            lblErrMessage.Visible = False

            If Not IsPostBack Then
                txtWPDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                If strBKMCode <> "" Then
                    isNew.Value = "False"
                    onLoad_Display()
                Else
                    isNew.Value = "True"
                    BindBkCategory()
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

    Function toNumber(ByVal s As String) As String
        If (s = "" Or s = "NULL") Then
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber("0", 2), 2)
        Else
            Return objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(s, 2), 2)
        End If

    End Function

    Function getCode(ByVal cat As String, ByVal scat As String) As String
        Dim strOpCd_GetID As String = "HR_PR_TRX_IDNUMBER"
        Dim objNewID As New Object
        Dim intErrNo As Integer
        Dim tcode As String
        Dim ParamName As String
        Dim ParamValue As String

        tcode = "BKM/" & strLocation & "/" & cat & "-" & scat & "/" & Mid(Trim(txtWPDate.Text), 4, 2) & Right(Trim(txtWPDate.Text), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|17|" & tcode & "|4"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Private Function BKM_Already_Exist() As SByte
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_MAIN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "SEARCH|SORT"
        strParamValue = " AND A.IDCat = '" & ddlbkcategory.SelectedItem.Value.Trim() & "' " & _
                        " AND A.IDSubCat = '" & ddlbksubcategory.SelectedItem.Value.Trim() & "' " & _
                        " AND A.BKMDate = '" & Date_Validation(Trim(txtWPDate.Text), False) & "' " & _
                        " AND A.MandorCode = '" & ddlMandorCode.SelectedItem.Value.Trim() & "' " & _
                        " AND A.LocCode ='" & strLocation & "' " & _
                        " AND A.Status='1'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return objEmpCodeDs.Tables(0).Rows.Count

    End Function

    Private Function BKM_beforesave() As Boolean
        Dim SIdSubCat As String = ddlbksubcategory.SelectedItem.Value.Trim()
        Dim SDivCode As String
        Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)
        Dim SMdrCode As String
        Dim SKCSCode As String

        If ddldivisicode.Text <> "" Then
            SDivCode = ddldivisicode.SelectedItem.Value.Trim()
        Else
            SDivCode = ""
        End If

        If ddlMandorCode.Text <> "" Then
            SMdrCode = ddlMandorCode.SelectedItem.Value.Trim()
        Else
            SMdrCode = ""
        End If

        If ddlKCSCode.Text <> "" Then
            SKCSCode = ddlKCSCode.SelectedItem.Value.Trim()
        Else
            SKCSCode = ""
        End If

        If (SIdSubCat = "") Then
            lblErrMessage.Text = "Silakan pilih sub kategori..."
            Return False
        End If

        If (SDate = "") Then
            lblErrMessage.Text = "Silakan input tanggal..."
            Return False
        End If

        If (SDivCode = "") Then
            lblErrMessage.Text = "Silakan pilih divisi... "
            Return False
        End If

        If (SMdrCode = "") Then
            lblErrMessage.Text = "Silakan pilih mandor... "
            Return False
        End If

        If (ddlKCSCode.Visible And SKCSCode = "") Then
            lblErrMessage.Text = "Silakan pilih KCS..."
            Return False
        End If

        'cek tgl,cat,subcat,mandor sudah ada blm...
        If (BKM_Already_Exist() >= 1) Then
            lblErrMessage.Text = "BKM dengan tanggal,kategori,sub kategori,& mandor tersebut sudah ada ..."
            Return False
        End If

        Return True
    End Function

    Sub BKM_onSave()
        Dim strIDM As String = LblidM.Text
        Dim SIdCat As String = ddlbkcategory.SelectedItem.Value.Trim()
        Dim SIdSubCat As String = ddlbksubcategory.SelectedItem.Value.Trim()
        Dim SDivCode As String
        Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)
        Dim SMdrCode As String
        Dim SKCSCode As String

        Dim strOpCd_Up As String = "PR_PR_TRX_BKM_MAIN_UPD"
        Dim dt As String = objGlobal.GetShortDate(strDateFmt, Now())
        Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        If ddldivisicode.Text <> "" Then
            SDivCode = ddldivisicode.SelectedItem.Value.Trim()
        Else
            SDivCode = ""
        End If

        If ddlMandorCode.Text <> "" Then
            SMdrCode = ddlMandorCode.SelectedItem.Value.Trim()
        Else
            SMdrCode = ""
        End If

        If ddlKCSCode.Text <> "" Then
            SKCSCode = ddlKCSCode.SelectedItem.Value.Trim()
        Else
            SKCSCode = ""
        End If

        ParamNama = "BKM|LOC|IC|IS|MC|KC|AM|AY|BD|DC|ST|CD|UD|UI"
        ParamValue = strIDM & "|" & _
                     strLocation & "|" & _
                     SIdCat & "|" & _
                     SIdSubCat & "|" & _
                     SMdrCode & "|" & _
                     SKCSCode & "|" & _
                     SM & "|" & _
                     SY & "|" & _
                     SDate & "|" & _
                     SDivCode & "|1|" & _
                     DateTime.Now() & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_MAIN_UPD&errmesg=" & ex.ToString() & "&redirect=")
        End Try

    End Sub

#Region "Function & Procedure PN"
    Function GetBjr(ByVal str As String) As Single
        Dim strOpCd As String = "HR_HR_STP_BLOK_GET_BJR"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpAttDs As New Object()

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE BlokCode='" & str & "' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objEmpAttDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_BLOK_GET_BJR&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpAttDs.Tables(0).Rows.Count = 1 Then
            Return (Trim(objEmpAttDs.Tables(0).Rows(0).Item("BJR")))
        Else
            Return "0"
        End If
    End Function


    Private Function GetBasisTgs(ByVal str As String) As Single
        Dim strOpCd As String = "HR_HR_STP_BLOK_GET_BSS_TGS"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpAttDs As New Object()

        strParamName = "BC"
        strParamValue = str

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objEmpAttDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_BLOK_GET_BSS_TGS&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpAttDs.Tables(0).Rows.Count = 1 Then
            Return (Trim(objEmpAttDs.Tables(0).Rows(0).Item("bss")))
        Else
            Return "0"
        End If
    End Function


    Private Sub PN_Save()
        If isNew.Value = "True" Then
            If (Not BKM_beforesave()) Then
                lblErrMessage.Visible = True
                Exit Sub
            End If
            BKM_onSave()
        End If
        Select Case ddlbksubcategory.SelectedItem.Value.Trim()
            Case "PBS"
                If (Not PBS_BeforeSave()) Then
                    lblErrMessage.Visible = True
                    Exit Sub
                End If
                PBS_Save()
            Case "KBS"
                If (Not KBS_BeforeSave()) Then
                    lblErrMessage.Visible = True
                    Exit Sub
                End If
                KBS_Save()
            Case "BMS"
                BMS_Save()
        End Select
    End Sub

    Private Function PBS_BeforeSave() As Boolean
        Dim i As Integer
        Dim row As Integer = dgPBS.Items.Count
        Dim noinput As Integer = 0
        Dim txt_jg As TextBox
        Dim ddl_cb As DropDownList
        Dim txt_hk As TextBox
        Dim cb As String = ""

        ' jika semua kryawan jenjang ga diisi return false
        For i = 0 To row - 1
            txt_jg = dgPBS.Items.Item(i).FindControl("dgPBS_txt_jg")
            If txt_jg.Text.Trim() = "" Then
                noinput = noinput + 1
            End If
        Next

        If noinput = row Then
            lblErrMessage.Text = "Silakan isi jenjang ..."
            Return False
        End If

        For i = 0 To row - 1
            txt_jg = dgPBS.Items.Item(i).FindControl("dgPBS_txt_jg")
            ddl_cb = dgPBS.Items.Item(i).FindControl("dgPBS_ddl_cb")
            txt_hk = dgPBS.Items.Item(i).FindControl("dgPBS_txt_hk")

            If ddl_cb.Visible Then
                cb = ddl_cb.SelectedItem.Value.Trim()
            End If

            If (txt_jg.Text.Trim = "") And ((ddl_cb.Visible And cb = "") Or Trim(txt_hk.Text = "")) Then
                lblErrMessage.Text = "Silakan isi jejang,blok dan hk..."
                Return False
            End If
        Next


        Return True
    End Function

    Sub PBS_Save()
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKMLN_PNPBS_UPD"
        Dim strIDM As String = LblidM.Text
        Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)
        Dim sIdSubCat As String
        Dim sIdDIv As String

        Dim hid_id As HiddenField
        Dim hid_idx As HiddenField
        Dim hid_ec As HiddenField
        Dim hid_jb As HiddenField

        Dim lbl_ed As Label
        Dim txt_hk As TextBox
        Dim ddl_cb As DropDownList
        Dim txt_jjg As TextBox
        Dim txt_hs As TextBox
        Dim txt_rot As TextBox
        Dim txt_bjr As TextBox
        Dim txt_kg As TextBox

        Dim ddl_ed As DropDownList
        Dim ed As String = ""
        Dim jc As String = ""
        Dim cb As String = ""

        Dim chk_Dmentah As CheckBox
        Dim chk_Dtinggal As CheckBox
        Dim chk_TPH As CheckBox
        Dim chk_Panjang As CheckBox
        Dim chk_Sengkleh As CheckBox
        Dim txt_DBasis As TextBox

        Dim d1 As String
        Dim d2 As String
        Dim d3 As String
        Dim d4 As String
        Dim d5 As String


        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        If isNew.Value = "True" Then
            sIdSubCat = ddlbksubcategory.SelectedItem.Value.Trim()
            sIdDIv = ddldivisicode.SelectedItem.Value.Trim()
        Else
            sIdSubCat = Left(lblbksubcategory.Text.Trim(), InStr(lblbksubcategory.Text.Trim(), " (") - 1)
            sIdDIv = Left(lbldivisicode.Text.Trim(), InStr(lbldivisicode.Text.Trim(), " (") - 1)
        End If

        For i = 0 To dgPBS.Items.Count - 1

            hid_id = dgPBS.Items.Item(i).FindControl("dgPBS_hid_id")
            hid_idx = dgPBS.Items.Item(i).FindControl("dgPBS_hid_idx")
            hid_ec = dgPBS.Items.Item(i).FindControl("dgPBS_hid_ec")
            hid_jb = dgPBS.Items.Item(i).FindControl("dgPBS_hid_jb")
            If hid_id.Value = 0 Then
                ddl_ed = dgPBS.Items.Item(i).FindControl("dgPBS_ddl_ededit")
                ed = ddl_ed.SelectedItem.Text.Trim()
            Else
                lbl_ed = dgPBS.Items.Item(i).FindControl("dgPBS_lbl_ed")
                ed = lbl_ed.Text
            End If

            txt_hk = dgPBS.Items.Item(i).FindControl("dgPBS_txt_hk")
            ddl_cb = dgPBS.Items.Item(i).FindControl("dgPBS_ddl_cb")
            txt_jjg = dgPBS.Items.Item(i).FindControl("dgPBS_txt_jg")
            txt_hs = dgPBS.Items.Item(i).FindControl("dgPBS_txt_ha")
            txt_rot = dgPBS.Items.Item(i).FindControl("dgPBS_txt_rot")
            txt_bjr = dgPBS.Items.Item(i).FindControl("dgPBS_txt_bjr")
            txt_kg = dgPBS.Items.Item(i).FindControl("dgPBS_txt_kg")

            chk_Dmentah = dgPBS.Items.Item(i).FindControl("dgPBS_chk_Dmentah")
            chk_Dtinggal = dgPBS.Items.Item(i).FindControl("dgPBS_chk_Dtinggal")
            chk_TPH = dgPBS.Items.Item(i).FindControl("dgPBS_chk_Dtph")
            chk_Panjang = dgPBS.Items.Item(i).FindControl("dgPBS_chk_Dpjg")
            chk_Sengkleh = dgPBS.Items.Item(i).FindControl("dgPBS_chk_DSengkleh")
            txt_DBasis = dgPBS.Items.Item(i).FindControl("dgPBS_txt_Dbasis")

            d1 = IIf(chk_Dmentah.Checked, "1", "0")
            d2 = IIf(chk_Dtinggal.Checked, "1", "0")
            d3 = IIf(chk_TPH.Checked, "1", "0")
            d4 = IIf(chk_Panjang.Checked, "1", "0")
            d5 = IIf(chk_Sengkleh.Checked, "1", "0")

            If (ddl_cb.Visible) Then
                cb = ddl_cb.SelectedItem.Value.Trim()
            End If

            'Save only employee and job exists and blok  and hk and hasil
            If (hid_ec.Value <> "") And (ddl_cb.Visible And cb <> "") And (txt_hk.Text <> "") And (txt_jjg.Text <> "") And (txt_hs.Text <> "") Then
                ParamName = "BC|BD|D1|D2|D3|D4|D5|JC|DI|ID|BKM|EC|HK|HA|JJG|BJR|KG|D6|ROT|LOC|ST|CD|UD|UI"
                ParamValue = cb & "|" & _
                             SDate & "|" & _
                             d1 & "|" & _
                             d2 & "|" & _
                             d3 & "|" & _
                             d4 & "|" & _
                             d5 & "|" & _
                             Trim(hid_jb.Value) & "|" & _
                             sIdDIv & "|" & _
                             Trim(hid_idx.Value) & "|" & _
                             strIDM & "|" & _
                             Trim(hid_ec.Value) & "|" & _
                             IIf(txt_hk.Text = "", "0", txt_hk.Text) & "|" & _
                             IIf(txt_hs.Text = "", "0", txt_hs.Text) & "|" & _
                             IIf(txt_jjg.Text = "", "0", txt_jjg.Text) & "|" & _
                             IIf(txt_bjr.Text = "", "0", txt_bjr.Text) & "|" & _
                             IIf(txt_kg.Text = "", "0", txt_kg.Text) & "|" & _
                             IIf(txt_DBasis.Text = "", "0", txt_DBasis.Text) & "|" & _
                             IIf(txt_rot.Text = "", "0", txt_rot.Text) & "|" & _
                             strLocation & "|1|" & _
                             DateTime.Now() & "|" & _
                             DateTime.Now() & "|" & _
                             strUserId

                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_PNPBS_UPD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next
    End Sub

    Private Function KBS_BeforeSave() As Boolean
        Dim i As Integer
        Dim row As Integer = dgKBS.Items.Count
        Dim noinput As Integer = 0
        Dim txt_jg As TextBox
        Dim ddl_cb As DropDownList
        Dim txt_hk As TextBox
        Dim cb As String = ""

        ' jika semua kryawan jenjang ga diisi return false
        For i = 0 To row - 1
            txt_jg = dgKBS.Items.Item(i).FindControl("dgKBS_txt_kg")
            If txt_jg.Text.Trim() = "" Then
                noinput = noinput + 1
            End If
        Next

        If noinput = row Then
            lblErrMessage.Text = "Silakan isi kg ..."
            Return False
        End If

        For i = 0 To row - 1
            txt_jg = dgKBS.Items.Item(i).FindControl("dgKBS_txt_kg")
            ddl_cb = dgKBS.Items.Item(i).FindControl("dgKBS_ddl_cb")
            txt_hk = dgKBS.Items.Item(i).FindControl("dgKBS_txt_hk")

            If ddl_cb.Visible Then
                cb = ddl_cb.SelectedItem.Value.Trim()
            End If

            If (txt_jg.Text.Trim = "") And ((ddl_cb.Visible And cb = "") Or Trim(txt_hk.Text = "")) Then
                lblErrMessage.Text = "Silakan isi kg,blok dan hk..."
                Return False
            End If
        Next


        Return True
    End Function

    Sub KBS_Save()
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKMLN_PNKBS_UPD"
        Dim strIDM As String = LblidM.Text
        Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)
        Dim sIdSubCat As String
        Dim sIdDIv As String

        Dim hid_id As HiddenField
        Dim hid_idx As HiddenField
        Dim hid_ec As HiddenField
        Dim hid_jb As HiddenField

        Dim lbl_ed As Label
        Dim txt_hk As TextBox
        Dim ddl_cb As DropDownList

        Dim txt_hs As TextBox
        Dim txt_rot As TextBox

        Dim txt_kg As TextBox

        Dim ddl_ed As DropDownList
        Dim ed As String = ""
        Dim jc As String = ""
        Dim cb As String = ""

        Dim chk_Dmentah As CheckBox
        Dim chk_TPH As CheckBox
        Dim txt_DBasis As TextBox

        Dim d2 As String
        Dim d3 As String


        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        If isNew.Value = "True" Then
            sIdSubCat = ddlbksubcategory.SelectedItem.Value.Trim()
            sIdDIv = ddldivisicode.SelectedItem.Value.Trim()
        Else
            sIdSubCat = Left(lblbksubcategory.Text.Trim(), InStr(lblbksubcategory.Text.Trim(), " (") - 1)
            sIdDIv = Left(lbldivisicode.Text.Trim(), InStr(lbldivisicode.Text.Trim(), " (") - 1)
        End If

        For i = 0 To dgKBS.Items.Count - 1

            hid_id = dgKBS.Items.Item(i).FindControl("dgKBS_hid_id")
            hid_idx = dgKBS.Items.Item(i).FindControl("dgKBS_hid_idx")
            hid_ec = dgKBS.Items.Item(i).FindControl("dgKBS_hid_ec")
            hid_jb = dgKBS.Items.Item(i).FindControl("dgKBS_hid_jb")
            If hid_id.Value = 0 Then
                ddl_ed = dgKBS.Items.Item(i).FindControl("dgKBS_ddl_ededit")
                ed = ddl_ed.SelectedItem.Text.Trim()
            Else
                lbl_ed = dgKBS.Items.Item(i).FindControl("dgKBS_lbl_ed")
                ed = lbl_ed.Text
            End If

            txt_hk = dgKBS.Items.Item(i).FindControl("dgKBS_txt_hk")
            ddl_cb = dgKBS.Items.Item(i).FindControl("dgKBS_ddl_cb")
            txt_hs = dgKBS.Items.Item(i).FindControl("dgKBS_txt_ha")
            txt_rot = dgKBS.Items.Item(i).FindControl("dgKBS_txt_rot")
            txt_kg = dgKBS.Items.Item(i).FindControl("dgKBS_txt_kg")

            chk_Dmentah = dgKBS.Items.Item(i).FindControl("dgKBS_chk_Dbk")
            chk_TPH = dgKBS.Items.Item(i).FindControl("dgKBS_chk_Dtph")
            txt_DBasis = dgKBS.Items.Item(i).FindControl("dgKBS_txt_Dkk")

            d2 = IIf(chk_Dmentah.Checked, "1", "0")
            d3 = IIf(chk_TPH.Checked, "1", "0")


            If (ddl_cb.Visible) Then
                cb = ddl_cb.SelectedItem.Value.Trim()
            End If

            'Save only employee and job exists and blok  and hk and hasil
            If (hid_ec.Value <> "") And (ddl_cb.Visible And cb <> "") And (txt_hk.Text <> "") And (txt_kg.Text <> "") And (txt_hs.Text <> "") Then
                ParamName = "BC|D2|D3|JC|DI|ID|BKM|EC|HK|HA|KG|ROT|D1|LOC|ST|CD|UD|UI"
                ParamValue = cb & "|" & _
                             d2 & "|" & _
                             d3 & "|" & _
                             Trim(hid_jb.Value) & "|" & _
                             sIdDIv & "|" & _
                             Trim(hid_idx.Value) & "|" & _
                             strIDM & "|" & _
                             Trim(hid_ec.Value) & "|" & _
                             IIf(txt_hk.Text = "", "0", txt_hk.Text) & "|" & _
                             IIf(txt_hs.Text = "", "0", txt_hs.Text) & "|" & _
                             IIf(txt_kg.Text = "", "0", txt_kg.Text) & "|" & _
                             IIf(txt_rot.Text = "", "0", txt_rot.Text) & "|" & _
                             IIf(txt_DBasis.Text = "", "0", txt_DBasis.Text) & "|" & _
                             strLocation & "|1|" & _
                             DateTime.Now() & "|" & _
                             DateTime.Now() & "|" & _
                             strUserId

                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_PNKBS_UPD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next
    End Sub

    Private Function BMS_BeforeSave() As Boolean
        Dim i As Integer
        Dim row As Integer = dgBMS.Items.Count
        Dim noinput As Integer = 0
        Dim txt_jg As TextBox
        Dim ddl_cb As DropDownList
        Dim txt_hk As TextBox
        Dim cb As String = ""

        ' jika semua kryawan jenjang ga diisi return false
        For i = 0 To row - 1
            txt_jg = dgBMS.Items.Item(i).FindControl("dgBMS_txt_kg")
            If txt_jg.Text.Trim() = "" Then
                noinput = noinput + 1
            End If
        Next

        If noinput = row Then
            lblErrMessage.Text = "Silakan isi kg ..."
            Return False
        End If

        For i = 0 To row - 1
            txt_jg = dgBMS.Items.Item(i).FindControl("dgBMS_txt_kg")
            ddl_cb = dgBMS.Items.Item(i).FindControl("dgBMS_ddl_cb")
            txt_hk = dgBMS.Items.Item(i).FindControl("dgBMS_txt_hk")

            If ddl_cb.Visible Then
                cb = ddl_cb.SelectedItem.Value.Trim()
            End If

            If (txt_jg.Text.Trim = "") And ((ddl_cb.Visible And cb = "") Or Trim(txt_hk.Text = "")) Then
                lblErrMessage.Text = "Silakan isi kg,blok dan hk..."
                Return False
            End If
        Next


        Return True
    End Function

    Sub BMS_Save()
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKMLN_PNBMS_UPD"
        Dim strIDM As String = LblidM.Text
        Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)
        Dim sIdSubCat As String
        Dim sIdDIv As String

        Dim hid_id As HiddenField
        Dim hid_idx As HiddenField
        Dim hid_ec As HiddenField
        Dim hid_jb As HiddenField

        Dim lbl_ed As Label
        Dim txt_hk As TextBox
        Dim ddl_cb As DropDownList
        Dim ddl_ty As DropDownList

        Dim txt_hs As TextBox
        Dim txt_rot As TextBox

        Dim txt_kg As TextBox

        Dim ddl_ed As DropDownList
        Dim ed As String = ""
        Dim jc As String = ""
        Dim cb As String = ""

        Dim txt_DBasis As TextBox

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        If isNew.Value = "True" Then
            sIdSubCat = ddlbksubcategory.SelectedItem.Value.Trim()
            sIdDIv = ddldivisicode.SelectedItem.Value.Trim()
        Else
            sIdSubCat = Left(lblbksubcategory.Text.Trim(), InStr(lblbksubcategory.Text.Trim(), " (") - 1)
            sIdDIv = Left(lbldivisicode.Text.Trim(), InStr(lbldivisicode.Text.Trim(), " (") - 1)
        End If

        For i = 0 To dgBMS.Items.Count - 1

            hid_id = dgBMS.Items.Item(i).FindControl("dgBMS_hid_id")
            hid_idx = dgBMS.Items.Item(i).FindControl("dgBMS_hid_idx")
            hid_ec = dgBMS.Items.Item(i).FindControl("dgBMS_hid_ec")
            hid_jb = dgBMS.Items.Item(i).FindControl("dgBMS_hid_jb")
            If hid_id.Value = 0 Then
                ddl_ed = dgBMS.Items.Item(i).FindControl("dgBMS_ddl_ededit")
                ed = ddl_ed.SelectedItem.Text.Trim()
            Else
                lbl_ed = dgBMS.Items.Item(i).FindControl("dgBMS_lbl_ed")
                ed = lbl_ed.Text
            End If

            txt_hk = dgBMS.Items.Item(i).FindControl("dgBMS_txt_hk")
            ddl_cb = dgBMS.Items.Item(i).FindControl("dgBMS_ddl_cb")
            txt_rot = dgBMS.Items.Item(i).FindControl("dgBMS_txt_rot")
            txt_kg = dgBMS.Items.Item(i).FindControl("dgBMS_txt_kg")
            ddl_ty = dgBMS.Items.Item(i).FindControl("dgBMS_ddl_tyjob")

            txt_DBasis = dgBMS.Items.Item(i).FindControl("dgBMS_txt_dnd")

            If (ddl_cb.Visible) Then
                cb = ddl_cb.SelectedItem.Value.Trim()
            End If

            'Save only employee and job exists and blok  and hk and hasil
            If (hid_ec.Value <> "") And (ddl_cb.Visible And cb <> "") And (txt_hk.Text <> "") And (txt_kg.Text <> "") Then
                ParamName = "BC|JC|DI|ID|BKM|EC|TY|HK|KG|ROT|DD|LOC|ST|CD|UD|UI"
                ParamValue = cb & "|" & _
                             Trim(hid_jb.Value) & "|" & _
                             sIdDIv & "|" & _
                             Trim(hid_idx.Value) & "|" & _
                             strIDM & "|" & _
                             Trim(hid_ec.Value) & "|" & _
                             ddl_ty.SelectedItem.Value.Trim() & "|" & _
                             IIf(txt_hk.Text = "", "0", txt_hk.Text) & "|" & _
                             IIf(txt_kg.Text = "", "0", txt_kg.Text) & "|" & _
                             IIf(txt_rot.Text = "", "0", txt_rot.Text) & "|" & _
                             IIf(txt_DBasis.Text = "", "0", txt_DBasis.Text) & "|" & _
                             strLocation & "|1|" & _
                             DateTime.Now() & "|" & _
                             DateTime.Now() & "|" & _
                             strUserId

                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_PNKBS_UPD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next
    End Sub
#End Region

#Region "Function & Procedure RW"


    Private Function RW_Pekerjaan_beforesave() As Boolean
        Dim i As Integer
        Dim row As Integer = dgRW.Items.Count
        Dim noinput As Integer = 0
        Dim ddl_jb As DropDownList
        Dim ddl_cb As DropDownList
        Dim txt_hk As TextBox
        Dim txt_hs As TextBox
        Dim cb As String = ""

        ' jika semua kryawan pekerjaannya ga diisi return false
        For i = 0 To row - 1
            ddl_jb = dgRW.Items.Item(i).FindControl("dgRW_ddl_jb")
            If ddl_jb.SelectedItem.Value.Trim = "" Then
                noinput = noinput + 1
            End If
        Next

        If noinput = row Then
            lblErrMessage.Text = "Silakan isi pekerjaan ..."
            Return False
        End If

        For i = 0 To row - 1
            ddl_jb = dgRW.Items.Item(i).FindControl("dgRW_ddl_jb")
            ddl_cb = dgRW.Items.Item(i).FindControl("dgRW_ddl_cb")
            txt_hk = dgRW.Items.Item(i).FindControl("dgRW_txt_hk")
            txt_hs = dgRW.Items.Item(i).FindControl("dgRW_txt_hs")

            If ddl_cb.Visible Then
                cb = ddl_cb.SelectedItem.Value.Trim()
            End If

            If (ddl_jb.SelectedItem.Value.Trim <> "") And ((ddl_cb.Visible And cb = "") Or Trim(txt_hk.Text = "") Or Trim(txt_hs.Text = "")) Then
                lblErrMessage.Text = "Silakan isi pekerjaan,blok,hk dan hasil kerja ..."
                Return False
            End If
        Next


        Return True
    End Function

    Private Sub RW_Pekerjaan_onSave()
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKMLN_RWJOB_UPD"
        Dim strIDM As String = LblidM.Text
        Dim sIdSubCat As String
        Dim sIdDIv As String
        Dim hid_id As HiddenField
        Dim hid_idx As HiddenField
        Dim hid_ec As HiddenField
        Dim lbl_ed As Label
        Dim ddl_jb As DropDownList
        Dim lbl_nr As Label
        Dim ddl_cb As DropDownList
        Dim txt_hk As TextBox
        Dim txt_hs As TextBox
        Dim txt_rot As TextBox
        Dim lbl_um As Label
        Dim ddl_ed As DropDownList
        Dim ed As String = ""
        Dim jc As String = ""
        Dim cb As String = ""

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        If isNew.Value = "True" Then
            sIdSubCat = ddlbksubcategory.SelectedItem.Value.Trim()
            sIdDIv = ddldivisicode.SelectedItem.Value.Trim()
        Else
            sIdSubCat = Left(lblbksubcategory.Text.Trim(), InStr(lblbksubcategory.Text.Trim(), " (") - 1)
            sIdDIv = Left(lbldivisicode.Text.Trim(), InStr(lbldivisicode.Text.Trim(), " (") - 1)
        End If

        For i = 0 To dgRW.Items.Count - 1

            hid_id = dgRW.Items.Item(i).FindControl("dgRW_hid_id")
            hid_idx = dgRW.Items.Item(i).FindControl("dgRW_hid_idx")
            hid_ec = dgRW.Items.Item(i).FindControl("dgRW_hid_ec")
            If hid_id.Value = 0 Then
                ddl_ed = dgRW.Items.Item(i).FindControl("dgRW_ddl_ededit")
                ed = ddl_ed.SelectedItem.Text.Trim()
            Else
                lbl_ed = dgRW.Items.Item(i).FindControl("dgRW_lbl_ed")
                ed = lbl_ed.Text
            End If
            ddl_jb = dgRW.Items.Item(i).FindControl("dgRW_ddl_jb")
            lbl_nr = dgRW.Items.Item(i).FindControl("dgRW_lbl_nr")
            ddl_cb = dgRW.Items.Item(i).FindControl("dgRW_ddl_cb")
            txt_hk = dgRW.Items.Item(i).FindControl("dgRW_txt_hk")
            txt_hs = dgRW.Items.Item(i).FindControl("dgRW_txt_hs")
            txt_rot = dgRW.Items.Item(i).FindControl("dgRW_txt_rot")
            lbl_um = dgRW.Items.Item(i).FindControl("dgRW_lbl_um")


            If (ddl_cb.Visible) Then
                cb = ddl_cb.SelectedItem.Value.Trim()
            End If

            'Save only employee and job exists and blok  and hk and hasil
            If (hid_ec.Value <> "") And (ddl_jb.SelectedItem.Value.Trim <> "") And (ddl_cb.Visible And cb <> "") And (txt_hk.Text <> "") And (txt_hs.Text <> "") Then
                ParamName = "BC|SI|DI|JC|ID|BKM|EC|NR|HK|HS|UM|ROT|LOC|ST|CD|UD|UI"
                ParamValue = cb & "|" & _
                             sIdSubCat & "|" & _
                             sIdDIv & "|" & _
                             ddl_jb.SelectedItem.Value.Trim() & "|" & _
                             Trim(hid_idx.Value) & "|" & _
                             strIDM & "|" & _
                             Trim(hid_ec.Value) & "|" & _
                             Trim(lbl_nr.Text) & "|" & _
                             IIf(txt_hk.Text = "", "0", txt_hk.Text) & "|" & _
                             IIf(txt_hs.Text = "", "0", txt_hs.Text) & "|" & _
                             Trim(lbl_um.Text) & "|" & _
                             IIf(txt_rot.Text = "", "0", txt_rot.Text) & "|" & _
                             strLocation & "|1|" & _
                             DateTime.Now() & "|" & _
                             DateTime.Now() & "|" & _
                             strUserId

                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_RWJOB_UPD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next


    End Sub

    Private Sub RW_Bahan_onSave()
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKMLN_BAHAN_UPD"
        Dim strIDM As String = LblidM.Text
        Dim sIdSubCat As String
        Dim sIdDIv As String
        Dim hid_id As HiddenField
        Dim hid_idx As HiddenField
        Dim ddl_jb As DropDownList
        Dim ddl_cb As DropDownList
        Dim txt_qty As TextBox
        Dim lbl_um As Label
        Dim jc As String = ""
        Dim cb As String = ""

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        If isNew.Value = "True" Then
            sIdSubCat = ddlbksubcategory.SelectedItem.Value.Trim()
            sIdDIv = ddldivisicode.SelectedItem.Value.Trim()
        Else
            sIdSubCat = Left(lblbksubcategory.Text.Trim(), InStr(lblbksubcategory.Text.Trim(), " (") - 1)
            sIdDIv = Left(lbldivisicode.Text.Trim(), InStr(lbldivisicode.Text.Trim(), " (") - 1)
        End If

        For i = 0 To dgBHN.Items.Count - 1

            hid_id = dgBHN.Items.Item(i).FindControl("dgBHN_hid_id")
            hid_idx = dgBHN.Items.Item(i).FindControl("dgBHN_hid_idx")
            ddl_jb = dgBHN.Items.Item(i).FindControl("dgBHN_ddl_jb")
            ddl_cb = dgBHN.Items.Item(i).FindControl("dgBHN_ddl_itm")
            txt_qty = dgBHN.Items.Item(i).FindControl("dgBHN_txt_Qty")
            lbl_um = dgBHN.Items.Item(i).FindControl("dgBHN_lbl_um")

            jc = ddl_jb.SelectedItem.Value.Trim()
            cb = Left(ddl_cb.SelectedItem.Value.Trim(), InStr(ddl_cb.SelectedItem.Value.Trim(), "|") - 1)

            'Save only employee and job exists and blok  and hk and hasil
            If (jc <> "") And (cb <> "") And (txt_qty.Text <> "") Then
                ParamName = "SI|DI|JC|ID|BKM|BC|QT|UM|LOC|ST|CD|UD|UI"
                ParamValue = sIdSubCat & "|" & _
                             sIdDIv & "|" & _
                             jc & "|" & _
                             Trim(hid_idx.Value) & "|" & _
                             strIDM & "|" & _
                             cb & "|" & _
                             txt_qty.Text.Trim() & "|" & _
                             Trim(lbl_um.Text) & "|" & _
                             strLocation & "|1|" & _
                             DateTime.Now() & "|" & _
                             DateTime.Now() & "|" & _
                             strUserId

                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_RWJOB_UPD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next


    End Sub

    Private Sub RW_Save()
        If isNew.Value = "True" Then
            If (Not BKM_beforesave()) Then
                lblErrMessage.Visible = True
                Exit Sub
            End If

            If (Not RW_Pekerjaan_beforesave()) Then
                lblErrMessage.Visible = True
                Exit Sub
            End If
            BKM_onSave()
        End If
        RW_Pekerjaan_onSave()
        RW_Bahan_onSave()
    End Sub

#End Region

#End Region

#Region "Binding"

#Region "Binding BK"

    Sub BindBkCategory()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_CATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE (CatID <> 'AD') and (CatID <> 'KM')|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_CATEGORY_GET_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("CatName") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatID")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("CatName")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("CatID") = ""
        dr("CatName") = "Pilih Kategori"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlbkcategory.DataSource = objEmpDivDs.Tables(0)
        ddlbkcategory.DataTextField = "CatName"
        ddlbkcategory.DataValueField = "CatID"
        ddlbkcategory.DataBind()
    End Sub

    Sub BindBKSubKategory(ByVal id As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_SUBCATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE idCat='" & id & "' and UpahorKerja='K'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_SUBCATEGORY_GET_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatName") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatID")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("SubCatName")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("SubCatID") = ""
        dr("SubCatName") = "Pilih Sub Kategori"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlbksubcategory.DataSource = objEmpDivDs.Tables(0)
        ddlbksubcategory.DataTextField = "SubCatName"
        ddlbksubcategory.DataValueField = "SubCatID"
        ddlbksubcategory.DataBind()

    End Sub

    Sub BindDivisi(ByVal id As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim StrFilter As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow
        strBKSubCategory = ddlbksubcategory.SelectedItem.Value.Trim()


        Select Case Trim(id)
            Case "UM"
                Select Case Trim(strBKSubCategory)
                    Case "TRK"
                        StrFilter = " AND (TyDiv='T')"
                    Case "SEC"
                        StrFilter = " AND (TyDiv='S')"
                    Case "UMM"
                        StrFilter = " AND (TyDiv='K' or TyDiv='O')"
                    Case Else
                        StrFilter = ""
                End Select
            Case "PN"
                StrFilter = " AND (TyDiv='K')"
            Case "RW"
                StrFilter = " AND (TyDiv='K')"
        End Select

        strParamName = "SEARCH|SORT"
        strParamValue = "AND A.LocCode='" & strLocation & "' AND A.Status='1'|ORDER By BlkGrpCode"


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpDivDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_DIVISI_LIST_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        If objEmpDivDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpDivDs.Tables(0).Rows.Count - 1
                objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
                objEmpDivDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("BlkGrpCode")) & " (" & Trim(objEmpDivDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            Next
        End If

        dr = objEmpDivDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "Pilih Divisi"
        objEmpDivDs.Tables(0).Rows.InsertAt(dr, 0)

        ddldivisicode.DataSource = objEmpDivDs.Tables(0)
        ddldivisicode.DataTextField = "Description"
        ddldivisicode.DataValueField = "BlkGrpCode"
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
            Next
        End If

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("BlokCode") = " "
        dr("Description") = "Pilih Blok"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)

    End Sub

    Sub BindMandor(ByVal strDivCode As String, ByVal sfilter As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_TRX_EMPLOYEE_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow

		Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)

        strParamName = "LOC|AM|AY|SEARCH|SORT"
        strParamValue = strLocation & "|" & SM & "|" & SY & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%' AND (isMandor<>'0')|ORDER BY A.EmpName"

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
        dr("_Description") = "Pilih Mandor"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlMandorCode.DataSource = objEmpCodeDs.Tables(0)
        ddlMandorCode.DataTextField = "_Description"
        ddlMandorCode.DataValueField = "EmpCode"
        ddlMandorCode.DataBind()
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


		Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
		
        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "|" & SM & "|" & SY & "|AND A.Status='1' AND A.LocCode = '" & strLocation & "' AND C.IDDiv like '%" & strDivCode & "%' AND isMandor='1'|ORDER BY A.EmpName"

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
        dr("_Description") = "Pilih KCS"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlKCSCode.DataSource = objEmpCodeDs.Tables(0)
        ddlKCSCode.DataTextField = "_Description"
        ddlKCSCode.DataValueField = "EmpCode"
        ddlKCSCode.DataBind()

    End Sub

#End Region

#Region "Binding AD"

#End Region

#Region "Binding PN"

    Protected Function PN_EmpByMdr(ByVal pn As String, ByVal mc As String, ByVal dt As String) As DataSet
        Dim strOpCd_EmpDiv As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()

        Select Case pn
            Case "PBS"
                strOpCd_EmpDiv = "PR_PR_TRX_BKM_PNPBS_TEMP_UPDNEW"
            Case "KBS"
                strOpCd_EmpDiv = "PR_PR_TRX_BKM_PNKBS_TEMP_UPDNEW"
            Case "BMS"
                strOpCd_EmpDiv = "PR_PR_TRX_BKM_PNBMS_TEMP_UPDNEW"
        End Select

        strParamName = "IP|MC|DT"
        strParamValue = Request.UserHostAddress & "|" & _
                        mc & "|" & _
                        dt

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_EMPBYMDR_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet"))
            Next
        End If

        Return objEmpCodeDs

    End Function

    Sub PN_Blok(ByVal sdc As String, ByVal sjc As String, ByVal ssc As String, ByVal sku As String)
        Dim strOpCd_EmpDiv As String = "HR_HR_STP_BLOK_GET_BKM"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim strfilter As String

        strParamName = "DC|JC|SC|KU|SEARCH"
        strParamValue = sdc & "|" & sjc & "|" & ssc & "|" & sku & "|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_BLOK_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        hastbl_cb.Clear()
        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
                hastbl_cb.Add(Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode")), Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokDet")))
            Next
        End If

        hastbl_cb.Add("", "Pilih Blok")
    End Sub

    Sub Job_BMS()
        hastbl_jbbms.Clear()
        hastbl_jbbms.Add("M", "Muat TBS")
        hastbl_jbbms.Add("L", "Langsir TBS")
    End Sub
    'POTONG BUAH

    Protected Function dgPBS_Reload() As DataSet
        'Binding Data
        If ddlbksubcategory.Text <> "" Then
            PN_Blok(Trim(ddldivisicode.SelectedItem.Value), "POTONG", Trim(ddlbksubcategory.SelectedItem.Value), "U")
        Else
            PN_Blok(hid_div.Value, "POTONG", hid_subcat.Value, "U")
        End If

        'load data
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_PNPBS_TEMP_UPDGET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim intErrNo As Integer

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE ipuser='" & Request.UserHostAddress & "'|ORDER BY empdet"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNPBS_TEMP_UPDGET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return objEmpCodeDs

    End Function

    Protected Function PN_PBS_EmpByMdrAll() As DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_PNPBS_TEMP_LISTEMP"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE ipuser='" & Request.UserHostAddress & "'|ORDER BY empdet"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNPBS_TEMP_LISTEMP&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet"))
            Next
        End If

        Return objEmpCodeDs

    End Function

    Sub PN_PBS_BindEmpByMdr()
        Dim dt As String = Date_Validation(txtWPDate.Text, False)
        dgPBS.DataSource = PN_EmpByMdr("PBS", ddlMandorCode.SelectedItem.Value.Trim(), dt)
        dgPBS.DataBind()
    End Sub

    Sub PN_PBS_EmpByMdr_Clear()
        Dim strOpCd As String = "PR_PR_TRX_BKM_PNPBS_TEMP_CLEAR"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP"
        strParamValue = Request.UserHostAddress

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
            dgPBS.DataSource = Nothing
            dgPBS.DataBind()
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNPBS_TEMP_CLEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub PN_PBS_BindEmpByMdr_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList
        Dim ds As DataSet = PN_PBS_EmpByMdrAll()
        Dim dr As DataRow

        dr = ds.Tables(0).NewRow()
        dr("codeemp") = ""
        dr("empdet") = "Pilih Karyawan"
        ds.Tables(0).Rows.InsertAt(dr, 0)

        DDLabs = dgPBS.Items.Item(index).FindControl("dgPBS_ddl_ededit")
        DDLabs.DataSource = ds
        DDLabs.DataTextField = "empdet"
        DDLabs.DataValueField = "codeemp"
        DDLabs.DataBind()

    End Sub

    Sub PN_PBS_Blok_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList
        DDLabs = dgPBS.Items.Item(index).FindControl("dgPBS_ddl_cb")
        Dim sorted_blk = New SortedList(hastbl_cb)
        DDLabs.DataSource = sorted_blk
        DDLabs.DataTextField = "value"
        DDLabs.DataValueField = "key"
        DDLabs.DataBind()

    End Sub

    Sub PN_PBS_BindPekerjaan_OnLoad()
        dgPBS.EditItemIndex = -1
        dgPBS.DataSource = dgPBS_Reload()
        dgPBS.DataBind()
    End Sub

    'KUTIP BRONDOLAN

    Protected Function dgKBS_Reload() As DataSet
        'Binding Data
        If ddlbksubcategory.Text <> "" Then
            PN_Blok(Trim(ddldivisicode.SelectedItem.Value), "KUTIP", Trim(ddlbksubcategory.SelectedItem.Value), "U")
        Else
            PN_Blok(hid_div.Value, "KUTIP", hid_subcat.Value, "U")
        End If


        'load data
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_PNKBS_TEMP_UPDGET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim intErrNo As Integer

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE ipuser='" & Request.UserHostAddress & "'|ORDER BY empdet"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNPBS_TEMP_UPDGET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return objEmpCodeDs

    End Function

    Protected Function PN_KBS_EmpByMdrAll() As DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_PNKBS_TEMP_LISTEMP"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE ipuser='" & Request.UserHostAddress & "'|ORDER BY empdet"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNKBS_TEMP_LISTEMP&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet"))
            Next
        End If

        Return objEmpCodeDs

    End Function

    Sub PN_KBS_EmpByMdr_Clear()
        Dim strOpCd As String = "PR_PR_TRX_BKM_PNKBS_TEMP_CLEAR"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP"
        strParamValue = Request.UserHostAddress

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
            dgKBS.DataSource = Nothing
            dgKBS.DataBind()
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNKBS_TEMP_CLEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub PN_KBS_BindEmpByMdr_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList
        Dim ds As DataSet = PN_KBS_EmpByMdrAll()
        Dim dr As DataRow

        dr = ds.Tables(0).NewRow()
        dr("codeemp") = ""
        dr("empdet") = "Pilih Karyawan"
        ds.Tables(0).Rows.InsertAt(dr, 0)

        DDLabs = dgKBS.Items.Item(index).FindControl("dgKBS_ddl_ededit")
        DDLabs.DataSource = ds
        DDLabs.DataTextField = "empdet"
        DDLabs.DataValueField = "codeemp"
        DDLabs.DataBind()

    End Sub

    Sub PN_KBS_Blok_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList
        DDLabs = dgKBS.Items.Item(index).FindControl("dgKBS_ddl_cb")
        Dim sorted_blk = New SortedList(hastbl_cb)
        DDLabs.DataSource = sorted_blk
        DDLabs.DataTextField = "value"
        DDLabs.DataValueField = "key"
        DDLabs.DataBind()

    End Sub

    Sub PN_KBS_BindEmpByMdr()
        Dim dt As String = Date_Validation(txtWPDate.Text, False)
        dgKBS.DataSource = PN_EmpByMdr("KBS", ddlMandorCode.SelectedItem.Value.Trim(), dt)
        dgKBS.DataBind()
    End Sub

    Sub PN_KBS_BindPekerjaan_OnLoad()
        dgKBS.EditItemIndex = -1
        dgKBS.DataSource = dgKBS_Reload()
        dgKBS.DataBind()
    End Sub

    'MUAT TBS
    Protected Function dgBMS_Reload() As DataSet
        'Binding Data
        If ddlbksubcategory.Text <> "" Then
            PN_Blok(Trim(ddldivisicode.SelectedItem.Value), "MUAT", Trim(ddlbksubcategory.SelectedItem.Value), "U")
        Else
            PN_Blok(hid_div.Value, "MUAT", hid_subcat.Value, "U")
        End If

        Job_BMS()

        'load data
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_PNBMS_TEMP_UPDGET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim intErrNo As Integer

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE ipuser='" & Request.UserHostAddress & "'|ORDER BY empdet"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNPBS_TEMP_UPDGET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return objEmpCodeDs

    End Function

    Protected Function PN_BMS_EmpByMdrAll() As DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_PNBMS_TEMP_LISTEMP"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE ipuser='" & Request.UserHostAddress & "'|ORDER BY empdet"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNBMS_TEMP_LISTEMP&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet"))
            Next
        End If

        Return objEmpCodeDs

    End Function

    Sub PN_BMS_EmpByMdr_Clear()
        Dim strOpCd As String = "PR_PR_TRX_BKM_PNBMS_TEMP_CLEAR"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP"
        strParamValue = Request.UserHostAddress

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
            dgBMS.DataSource = Nothing
            dgBMS.DataBind()
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNBMS_TEMP_CLEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub PN_BMS_BindEmpByMdr_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList
        Dim ds As DataSet = PN_BMS_EmpByMdrAll()
        Dim dr As DataRow

        dr = ds.Tables(0).NewRow()
        dr("codeemp") = ""
        dr("empdet") = "Pilih Karyawan"
        ds.Tables(0).Rows.InsertAt(dr, 0)

        DDLabs = dgBMS.Items.Item(index).FindControl("dgBMS_ddl_ededit")
        DDLabs.DataSource = ds
        DDLabs.DataTextField = "empdet"
        DDLabs.DataValueField = "codeemp"
        DDLabs.DataBind()

    End Sub

    Sub PN_BMS_Blok_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList
        DDLabs = dgBMS.Items.Item(index).FindControl("dgBMS_ddl_cb")
        Dim sorted_blk = New SortedList(hastbl_cb)
        DDLabs.DataSource = sorted_blk
        DDLabs.DataTextField = "value"
        DDLabs.DataValueField = "key"
        DDLabs.DataBind()
    End Sub

    Sub PN_BMS_TyJob_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList
        DDLabs = dgBMS.Items.Item(index).FindControl("dgBMS_ddl_tyjob")
        Dim sorted_blk = New SortedList(hastbl_jbbms)
        DDLabs.DataSource = sorted_blk
        DDLabs.DataTextField = "value"
        DDLabs.DataValueField = "key"
        DDLabs.DataBind()
    End Sub

    Sub PN_BMS_BindEmpByMdr()
        Dim dt As String = Date_Validation(txtWPDate.Text, False)
        dgBMS.DataSource = PN_EmpByMdr("BMS", ddlMandorCode.SelectedItem.Value.Trim(), dt)
        dgBMS.DataBind()
    End Sub

    Sub PN_BMS_BindPekerjaan_OnLoad()
        dgBMS.EditItemIndex = -1
        dgBMS.DataSource = dgBMS_Reload()
        dgBMS.DataBind()
    End Sub


#End Region

#Region "Binding RW"

    Protected Function dgRW_Reload() As DataSet
        'Binding Data
        If ddlbksubcategory.Text <> "" Then
            RW_Pekerjaan(Trim(ddlbksubcategory.SelectedItem.Value), Trim(ddldivisicode.SelectedItem.Value))
			RW_Blok(Trim(ddldivisicode.SelectedItem.Value), "", Trim(ddlbksubcategory.SelectedItem.Value), "K")
		Else
            RW_Pekerjaan(hid_subcat.Value, hid_div.Value)
			RW_Blok(hid_div.Value, "", hid_subcat.Value, "K")
        End If

		

        'load data
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_RWJOB_TEMP_UPDGET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim intErrNo As Integer

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE ipuser='" & Request.UserHostAddress & "'|ORDER BY empdet"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RWJOB_TEMP_UPDGET&errmesg=" & Exp.Message & "&redirect=")
        End Try


        Return objEmpCodeDs

    End Function

    Protected Function dgRw_GetUOM(ByVal sjc As String, ByVal ssc As String, ByVal sdi As String, ByVal sku As String) As String
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_JOBUOM_GET_BY_JOB"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objCodeDs As New Object()
        Dim intErrNo As Integer

        strParamName = "JC|SC|DI|KU"
        strParamValue = sjc & "|" & ssc & "|" & sdi & "|" & sku

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOBUOM_GET_BY_JOB&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objCodeDs.Tables(0).Rows.Count = 0 Then
            dgRw_GetUOM = ""
        Else
            dgRw_GetUOM = Trim(objCodeDs.Tables(0).Rows(0).Item("UOM"))
        End If
    End Function

    Protected Function dgRw_GetNorma(ByVal sjc As String, ByVal ssc As String, ByVal sdi As String, ByVal sku As String, ByVal syr As String) As String
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_NORMA_GET_BY_JOBBLOK"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objCodeDs As New Object()
        Dim intErrNo As Integer

        strParamName = "JC|SC|DI|KU|YR"
        strParamValue = sjc & "|" & ssc & "|" & sdi & "|" & sku & "|" & syr

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_NORMA_GET_BY_JOBBLOK&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objCodeDs.Tables(0).Rows.Count = 0 Then
            dgRw_GetNorma = ""
        Else
            dgRw_GetNorma = Trim(objCodeDs.Tables(0).Rows(0).Item("hk_ha_rot")) & "Hk/Ha/Rot"
        End If
    End Function

    Protected Function RW_Blok(ByVal sdc As String, ByVal sjc As String, ByVal ssc As String, ByVal sku As String) As DataSet
        Dim strOpCd_EmpDiv As String = "HR_HR_STP_BLOK_GET_BKM"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow

        strParamName = "DC|JC|SC|KU|SEARCH"
        'strParamValue = sdc & "|" & sjc & "|" & ssc & "|" & sku & "|"
		strParamValue = sdc & "||" & ssc & "|" & sku & "|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_HR_STP_BLOK_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        hastbl_cb.Clear()
        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
              '  objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode"))
               ' objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokDet") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokDet"))

                hastbl_cb.Add(Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode")), Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokDet")))
            Next
        End If

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("BlokCode") = ""
        dr("BlokDet") = "Pilih Blok"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)

     

        hastbl_cb.Add("", "Pilih Blok")
		Return objEmpBlkDs

    End Function

    Protected Function RW_EmpByMdrAll() As DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_RWJOB_TEMP_LISTEMP"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "SEARCH|SORT"
        strParamValue = "|ORDER BY empdet"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RWJOB_TEMP_LISTEMP&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet"))
            Next
        End If

        Return objEmpCodeDs

    End Function

    Protected Function RW_EmpByMdr(ByVal mc As String, ByVal dt As String) As DataSet
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_RWJOB_TEMP_UPDNEW"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "IP|MC|DT"
        strParamValue = Request.UserHostAddress & "|" & _
                        mc & "|" & _
                        dt

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_EMPBYMDR_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("codeemp"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("empdet"))
            Next
        End If

        Return objEmpCodeDs

    End Function

    Sub RW_EmpByMdr_Clear()
        Dim strOpCd As String = "PR_PR_TRX_BKM_RWJOB_TEMP_CLEAR"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP"
        strParamValue = Request.UserHostAddress

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
            dgRW.DataSource = Nothing
            dgRW.DataBind()
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_CATEGORY_GET_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub RW_BindEmpByMdr()
        Dim dt As String = Date_Validation(txtWPDate.Text, False)
        dgRW.DataSource = RW_EmpByMdr(ddlMandorCode.SelectedItem.Value.Trim(), dt)
        dgRW.DataBind()
    End Sub

    Sub RW_BindEmpByMdr_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList
        Dim ds As DataSet = RW_EmpByMdrAll()
        Dim dr As DataRow

        dr = ds.Tables(0).NewRow()
        dr("codeemp") = ""
        dr("empdet") = "Pilih Karyawan"
        ds.Tables(0).Rows.InsertAt(dr, 0)

        DDLabs = dgRW.Items.Item(index).FindControl("dgRW_ddl_ededit")
        DDLabs.DataSource = ds
        DDLabs.DataTextField = "empdet"
        DDLabs.DataValueField = "codeemp"
        DDLabs.DataBind()

    End Sub

    Sub RW_BindPekerjaan_OnLoad()
        dgRW.EditItemIndex = -1
        dgRW.DataSource = dgRW_Reload()
        dgRW.DataBind()
    End Sub

    Sub RW_Pekerjaan_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList

        If ddlbksubcategory.Text <> "" Then
            RW_Pekerjaan(Trim(ddlbksubcategory.SelectedItem.Value), Trim(ddldivisicode.SelectedItem.Value))
        Else
            RW_Pekerjaan(hid_subcat.Value, hid_div.Value)
        End If

        DDLabs = dgRW.Items.Item(index).FindControl("dgRW_ddl_jb")
        Dim sorted_job = New SortedList(hastbl_jb)
        DDLabs.DataSource = sorted_job
        DDLabs.DataTextField = "value"
        DDLabs.DataValueField = "key"
        DDLabs.DataBind()
    End Sub

    Sub RW_Blok_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList
        DDLabs = dgRW.Items.Item(index).FindControl("dgRW_ddl_cb")
        Dim sorted_blk = New SortedList(hastbl_cb)
        DDLabs.DataSource = sorted_blk
        DDLabs.DataTextField = "value"
        DDLabs.DataValueField = "key"
        DDLabs.DataBind()

    End Sub

    Sub RW_Pekerjaan(ByVal subcat As String, ByVal iddiv As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_RWJOB_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "SEARCH|SORT"
        strParamValue = "AND IdSubCat='" & subcat & "' AND IDDiv='" & iddiv & "' AND KerjaOrUpah='K'|Order by JobName"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RW_JOB_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        hastbl_jb.Clear()
        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                hastbl_jb.Add(Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("JobName")), Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("JobName")))
            Next
        End If
        hastbl_jb.Add("", "Pilih Pekerjaan")

    End Sub

#End Region

#Region "Binding BHN"
    Sub dgBHN_Clear()
        Dim strOpCd As String = "PR_PR_TRX_BKM_RWBHN_TEMP_CLEAR"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP"
        strParamValue = Request.UserHostAddress

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
            dgBHN.DataSource = Nothing
            dgBHN.DataBind()
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RWBHN_TEMP_CLEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub BHN_BindPekerjaan_OnLoad()
        dgBHN.EditItemIndex = -1
        dgBHN.DataSource = dgBHN_Reload()
        dgBHN.DataBind()
    End Sub

    Protected Function dgBHN_Reload() As DataSet
        'Bind Data
        BindBahan()
        BHN_JOBAll()

        'load data
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_RWBHN_TEMP_UPDGET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim objEmpCodeDs As New Object()
        Dim intErrNo As Integer

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE ipuser='" & Request.UserHostAddress & "'|ORDER BY job"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RWBHN_TEMP_UPDGET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return objEmpCodeDs

    End Function

    Sub BHN_JOBAll()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_RWBHN_JOB_TEMP_LISTEMP"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE ipuser='" & Request.UserHostAddress & "'|ORDER BY job"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RWBHN_JOB_TEMP_LISTEMP&errmesg=" & Exp.Message & "&redirect=")
        End Try

        hastbl_jbbhn.Clear()

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                hastbl_jbbhn.Add(Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("job")), Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("job")))
            Next
        End If


    End Sub

    Sub BindBahan()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_BAHAN_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = "AND itm.LocCode = '" & strLocation & "'|ORDER BY itm.Description"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_BAHAN_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        hastbl_bhn.Clear()
        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
                hastbl_bhn.Add(Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("ItemCode")) & "|" & Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("UOMCode")), Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("Descr")))
            Next
        End If

        hastbl_bhn.Add("", "Pilih Bahan")
    End Sub

    Sub BHN_Bahan_edit(ByVal index As Integer)
        Dim DDLabs As DropDownList

        'BindBahan()

        DDLabs = dgBHN.Items.Item(index).FindControl("dgBHN_ddl_itm")
        Dim sorted_job = New SortedList(hastbl_bhn)
        DDLabs.DataSource = sorted_job
        DDLabs.DataTextField = "value"
        DDLabs.DataValueField = "key"
        DDLabs.DataBind()
    End Sub

    Sub BHN_Pekerjaan_edit(ByVal index As Integer)
        Dim DDLjob As DropDownList
        Dim DDLjobBhn As DropDownList
        Dim i As Integer

        hastbl_jbbhn.Clear()

        For i = 0 To dgRW.Items.Count - 1
            DDLjob = dgRW.Items.Item(i).FindControl("dgRW_ddl_jb")
            If DDLjob.SelectedItem.Value <> "" Then
                If Not hastbl_jbbhn.ContainsKey(DDLjob.SelectedItem.Text) Then
                    hastbl_jbbhn.Add(DDLjob.SelectedItem.Text, DDLjob.SelectedItem.Value)
                End If
            End If
        Next

        DDLjobBhn = dgBHN.Items.Item(index).FindControl("dgBHN_ddl_jb")
        Dim sorted_job = New SortedList(hastbl_jbbhn)
        DDLjobBhn.DataSource = sorted_job
        DDLjobBhn.DataTextField = "value"
        DDLjobBhn.DataValueField = "key"
        DDLjobBhn.DataBind()
    End Sub

#End Region

#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_MAIN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "SEARCH|SORT"
        strParamValue = " AND BKMCode Like '%" & strBKMCode & "%' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            LblidM.Text = strBKMCode
            strEmpMandorCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("MandorCode"))
            strEmpDivCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDDiv"))
            strBKCategory = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDCat"))
            strBKSubCategory = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDSubCat"))
            lbldivisicode.Text = strEmpDivCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("divisi")) & ")"
            lblMandorCode.Text = strEmpMandorCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("EmpName")) & ")"
            lblWPDate.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("BKMDate"))
            lblbkcategory.Text = strBKCategory & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CatName")) & ")"
            lblbksubcategory.Text = strBKSubCategory & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("SubcatName")) & ")"

            lblPeriod.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = IIf(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Status")) = "1", "Active", "Delete")
            lblDateCreated.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblupdatedby.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("UserName"))

            hid_cat.Value = strBKCategory
            hid_subcat.Value = strBKSubCategory
            hid_div.Value = strEmpDivCode
            TABBK.Visible = True
            Select Case Trim(strBKCategory)
                Case "UM"
                    Select Case Trim(strBKSubCategory)
                        Case "CVL"
                            ShowHideTab("CVL|BHN|")
                        Case "TRK"
                            ShowHideTab("TRK")
                        Case "SEC"
                            ShowHideTab("SEC")
                        Case "UMM"
                            ShowHideTab("UMM")
                    End Select
                Case "PN"

                    Select Case Trim(strBKSubCategory)
                        Case "PBS"
                            PBS_onLoad_Display(strBKMCode)
                            PN_PBS_BindPekerjaan_OnLoad()
                        Case "KBS"
                            KBS_onLoad_Display(strBKMCode)
                            PN_KBS_BindPekerjaan_OnLoad()
                        Case "BMS"
                            BMS_onLoad_Display(strBKMCode)
                            PN_BMS_BindPekerjaan_OnLoad()
                    End Select
                    ShowHideTab(strBKSubCategory)
                Case "RW"
                    RW_onLoad_Display(strBKMCode)
                    BHN_onLoad_Display(strBKMCode)
                    RW_BindPekerjaan_OnLoad()
                    BHN_BindPekerjaan_OnLoad()
                    ShowHideTab("RWT|BHN|")
            End Select
        End If
    End Sub

    Sub onLoad_button()

        SaveBtn.Attributes("onclick") = "javascript:return ConfirmAction('updateall');"

        If isNew.Value = "False" Then
            ddldivisicode.Visible = False
            ddlMandorCode.Visible = False
            txtWPDate.Visible = False
            btnSelDate.Visible = False
            ddlbkcategory.Visible = False
            ddlbksubcategory.Visible = False
            ddlKCSCode.Visible = False
            lbldivisicode.Visible = True
            lblMandorCode.Visible = True
            lblKCSCode.Visible = True
            lblWPDate.Visible = True
            lblbkcategory.Visible = True
            lblbksubcategory.Visible = True

        Else
            ddldivisicode.Visible = True
            ddlMandorCode.Visible = True
            txtWPDate.Visible = True
            btnSelDate.Visible = True
            ddlbkcategory.Visible = True
            ddlbksubcategory.Visible = True

            lbldivisicode.Visible = False
            lblMandorCode.Visible = False
            lblWPDate.Visible = False
            lblbkcategory.Visible = False
            lblbksubcategory.Visible = False

        End If

    End Sub

#Region "Event BK"
    Sub ShowHideTab(ByVal key As String)
        Dim oTab As Infragistics.WebUI.UltraWebTab.Tab
        Dim i As Byte
        Dim ary As Array
        Dim arycnt As Byte
        ary = Split(key, "|")
        arycnt = UBound(ary)
        For i = 0 To TABBK.Tabs.Count - 1
            If TypeOf TABBK.Tabs(i) Is Infragistics.WebUI.UltraWebTab.Tab Then
                oTab = CType(TABBK.Tabs(i), Infragistics.WebUI.UltraWebTab.Tab)
                If arycnt = 2 Then
                    If oTab.Key = Trim(ary(0)) Or oTab.Key = Trim(ary(1)) Then
                        oTab.Visible = True
                    Else
                        oTab.Visible = False
                    End If
                Else
                    If oTab.Key = Trim(ary(0)) Then
                        oTab.Visible = True
                    Else
                        oTab.Visible = False
                    End If
                End If
            End If
        Next
    End Sub

    Sub ddlbkcategory_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
        BindBKSubKategory(strBKCategory)
        ShowHideTab("")
        lbltxtmandor.Text = ""
        ddlMandorCode.Visible = False
        ddlKCSCode.Visible = False
    End Sub

    Sub ddlbksubcategory_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
        strBKSubCategory = ddlbksubcategory.SelectedItem.Value.Trim()
        LblidM.Text = getCode(strBKCategory, strBKSubCategory)
        BindDivisi(strBKCategory)
        TABBK.Visible = True
        ddlMandorCode.Items.Clear()
        ddlKCSCode.Items.Clear()


        Select Case Trim(strBKCategory)
            Case "UM"
                Select Case Trim(strBKSubCategory)
                    Case "CVL"
                        lbltxtmandor.Text = "Mandor Code:*"
                        ddlMandorCode.Visible = True
                        ddlKCSCode.Visible = False
                        ShowHideTab("CVL|BHN|")
                    Case "TRK"
                        lbltxtmandor.Text = "Krani Code:*"
                        ddlKCSCode.Visible = True
                        ddlKCSCode.Visible = False
                        ShowHideTab("TRK")
                    Case "SEC"
                        lbltxtmandor.Text = "Mandor Code:*"
                        ddlMandorCode.Visible = True
                        ddlKCSCode.Visible = False
                        ShowHideTab("SEC")
                    Case "UMM"
                        lbltxtmandor.Text = ""
                        ddlKCSCode.Visible = False
                        ddlMandorCode.Visible = False
                End Select
                dgBHN_Clear()
            Case "PN"
                lbltxtmandor.Text = "Mandor & KCS Code:*"
                ddlMandorCode.Visible = True
                ddlKCSCode.Visible = True
                PN_PBS_EmpByMdr_Clear()
                PN_KBS_EmpByMdr_Clear()
                PN_BMS_EmpByMdr_Clear()
                ShowHideTab(strBKSubCategory)
            Case "RW"
                lbltxtmandor.Text = "Mandor Code:*"
                ddlKCSCode.Visible = False
                ddlMandorCode.Visible = True
                RW_EmpByMdr_Clear()
                dgBHN_Clear()
                ShowHideTab("RWT|BHN|")

        End Select


    End Sub

    Sub ddldivisicode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strEmpDivCode = ddldivisicode.SelectedItem.Value.Trim()
        strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
        strBKSubCategory = ddlbksubcategory.SelectedItem.Value.Trim()
        '      BindBlok(strEmpDivCode)

        Select Case Trim(strBKCategory)
            Case "UM"
                If strBKSubCategory = "TRK" Then BindKCS(strEmpDivCode)
                If strBKSubCategory = "SEC" Then BindMandor(strEmpDivCode, "Centeng")
                If strBKSubCategory = "CVL" Then BindMandor(strEmpDivCode, "rawat")
            Case "PN"
                BindMandor(strEmpDivCode, "panen")
                BindKCS(strEmpDivCode)
            Case "RW"
                BindMandor(strEmpDivCode, "rawat")
        End Select


    End Sub

    Sub ddl_DgSorting(ByVal ddl As DropDownList, ByVal obj As Hashtable, ByVal slc As String)
        Dim sorted = New SortedList(obj)
        ddl.DataSource = sorted
        ddl.DataTextField = "value"
        ddl.DataValueField = "key"
        ddl.DataBind()
        ddl.SelectedValue = Trim(slc)
    End Sub

    Sub ddlMandorCode_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
        strBKSubCategory = ddlbksubcategory.SelectedItem.Value.Trim()
        Dim strBKDivisi = ddldivisicode.SelectedItem.Value.Trim()

        Select Case strBKCategory
            Case "UM"
                dgBHN_Clear()
            Case "PN"
                Select Case strBKSubCategory
                    Case "PBS"
                        PN_Blok(strBKDivisi, "POTONG", strBKSubCategory, "U")
                        PN_PBS_BindEmpByMdr()
                    Case "KBS"
                        PN_Blok(strBKDivisi, "KUTIP", strBKSubCategory, "U")
                        PN_KBS_BindEmpByMdr()
                    Case "BMS"
                        PN_Blok(strBKDivisi, "MUAT", strBKSubCategory, "U")
                        Job_BMS()
                        PN_BMS_BindEmpByMdr()
                End Select
            Case "RW"
                RW_Pekerjaan(strBKSubCategory,strBKDivisi)
                RW_Blok(strBKDivisi,"",strBKSubCategory,"K")
				RW_BindEmpByMdr()
                dgBHN_Clear()
        End Select

    End Sub

    Sub BtnNewBK_OnClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_BKMDet_Estate.aspx")
    End Sub

    Sub BtnSaveBK_OnClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If isNew.Value = "True" Then
            strBKCategory = ddlbkcategory.SelectedItem.Value.Trim()
        Else
            strBKCategory = Left(lblbkcategory.Text.Trim(), InStr(lblbkcategory.Text.Trim(), "(") - 1)
        End If

        Select Case Trim(strBKCategory)
            Case "UM"
            Case "PN"
                PN_Save()
            Case "RW"
                RW_Save()
        End Select

        If lblErrMessage.Visible = False Then
            strBKMCode = LblidM.Text.Trim()
            isNew.Value = "False"
            onLoad_Display()
            onLoad_button()
        End If
    End Sub

    Sub BtnBackBK_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_BKMList_Estate.aspx")
    End Sub

#End Region

#Region "Event RW"

    Sub RW_onLoad_Display(ByVal bkmcode As String)
        Dim strOpCd As String = "PR_PR_TRX_BKMLN_RWJOB_LOAD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "IP|BKM"
        strParamValue = Request.UserHostAddress & "|" & bkmcode

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_RWJOB_LOAD&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

    End Sub

    Sub dgRW_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim hd As HiddenField
            Dim DDLabs As DropDownList
            Dim DDLabs_blk As DropDownList
            Dim Updbutton As LinkButton


            'e.Item.Cells(0).Text = e.Item.ItemIndex + 1

            hd = CType(e.Item.FindControl("dgRW_hid_jb"), HiddenField)
            DDLabs = CType(e.Item.FindControl("dgRW_ddl_jb"), DropDownList)
            ddl_DgSorting(DDLabs, hastbl_jb, hd.Value.Trim())

            hd = CType(e.Item.FindControl("dgRW_hid_cb"), HiddenField)
            DDLabs_blk = CType(e.Item.FindControl("dgRW_ddl_cb"), DropDownList)
			ddl_DgSorting(DDLabs_blk, hastbl_cb, hd.Value.Trim())
			
		    ' If isNew.Value = "True" Then
            '    If hd.Value = "" Then
            '        DDLabs_blk.Visible = True
            '    Else
            '        DDLabs_blk.Visible = False
            '    End If
            'Else
            '    If hd.Value = "" Then
            '        DDLabs_blk.Visible = False
            '    Else
			'        'DDLabs_blk.DataSource = RW_Blok(hid_div.Value, DDLabs.SelectedItem.Value.Trim(), hid_subcat.Value, "K")
            '        'DDLabs_blk.DataTextField = "BlokDet"
            '        'DDLabs_blk.DataValueField = "BlokCode"
            '        'DDLabs_blk.DataBind()
            '        'DDLabs_blk.SelectedValue = hd.Value
            '    DDLabs_blk.Visible = True
            '    End If

            'End If

            Updbutton = CType(e.Item.FindControl("Delete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem Then
            Dim DDLabs_blk As DropDownList
            DDLabs_blk = CType(e.Item.FindControl("dgRW_ddl_cb"), DropDownList)
            ddl_DgSorting(DDLabs_blk, hastbl_cb, "")
            'DDLabs_blk.Visible = False	
        End If
    End Sub
    '<Script Language=""JavaScript"">pop_bkm=window.open(""PR_trx_BKMPopEmp_Estate.aspx?redirect=attm&Attdate=" & txtWPDate.Text & """, null ,""'pop_bkm',width=650,height=300,top=230,left=200,status=yes, resizable=no, scrollbars=yes, toolbar=no, location=no"");pop_bkm.focus();</Script>

    Sub RWT_add_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        ' save dulu
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKM_RWJOB_TEMP_UPDADD"
        Dim hid_id As HiddenField
        Dim hid_ec As HiddenField
        Dim lbl_ed As Label
        Dim ddl_jb As DropDownList
        Dim lbl_nr As Label
        Dim ddl_cb As DropDownList
        Dim txt_hk As TextBox
        Dim txt_hs As TextBox
        Dim txt_rot As TextBox
        Dim lbl_um As Label
        Dim ddl_ed As DropDownList
        Dim ed As String = ""
        Dim cb As String = ""

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        For i = 0 To dgRW.Items.Count - 1

            hid_id = dgRW.Items.Item(i).FindControl("dgRW_hid_id")
            hid_ec = dgRW.Items.Item(i).FindControl("dgRW_hid_ec")
            If hid_id.Value = 0 Then
                ddl_ed = dgRW.Items.Item(i).FindControl("dgRW_ddl_ededit")
                ed = ddl_ed.SelectedItem.Text.Trim()
            Else
                lbl_ed = dgRW.Items.Item(i).FindControl("dgRW_lbl_ed")
                ed = lbl_ed.Text
            End If
            ddl_jb = dgRW.Items.Item(i).FindControl("dgRW_ddl_jb")
            lbl_nr = dgRW.Items.Item(i).FindControl("dgRW_lbl_nr")
            ddl_cb = dgRW.Items.Item(i).FindControl("dgRW_ddl_cb")
            If ddl_cb.Visible Then
                cb = ddl_cb.SelectedItem.Value.Trim()
            Else
                cb = ""
            End If
            txt_hk = dgRW.Items.Item(i).FindControl("dgRW_txt_hk")
            txt_hs = dgRW.Items.Item(i).FindControl("dgRW_txt_hs")
            lbl_um = dgRW.Items.Item(i).FindControl("dgRW_lbl_um")
            txt_rot = dgRW.Items.Item(i).FindControl("dgRW_txt_rot")
            If hid_ec.Value <> "" Then
                ParamName = "ID|IP|EC|ED|JB|NR|CB|HK|HS|UM|ROT"
                ParamValue = Trim(hid_id.Value) & "|" & _
                             Trim(Request.UserHostAddress) & "|" & _
                             Trim(hid_ec.Value) & "|" & _
                             Trim(ed) & "|" & _
                             ddl_jb.SelectedItem.Value.Trim() & "|" & _
                             Trim(lbl_nr.Text) & "|" & _
                             cb & "|" & _
                             IIf(txt_hk.Text = "", "null", txt_hk.Text) & "|" & _
                             IIf(txt_hs.Text = "", "null", txt_hs.Text) & "|" & _
                             Trim(lbl_um.Text) & "|" & _
                             IIf(txt_rot.Text = "", "null", txt_rot.Text)

                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RWJOB_TEMP_UPDADD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next

        Dim dr As DataRow
        Dim dataSet As DataSet

        dataSet = dgRW_Reload()
        dr = dataSet.Tables(0).NewRow()
        dr("id") = 0
        dr("ipuser") = Request.UserHostAddress
        dr("codeemp") = ""
        dr("empdet") = ""
        dr("job") = ""
        dr("norma") = ""
        dr("codeblok") = ""
        dr("hkjob") = DBNull.Value
        dr("hsljob") = DBNull.Value
        dr("uom") = ""
        dr("rotasi") = DBNull.Value
        dataSet.Tables(0).Rows.Add(dr)

        dgRW.DataSource = dataSet
        dgRW.DataBind()
        dgRW.EditItemIndex = dgRW.Items.Count - 1
        dgRW.DataBind()
        RW_BindEmpByMdr_edit(dgRW.EditItemIndex)
        RW_Pekerjaan_edit(dgRW.EditItemIndex)
        RW_Blok_edit(dgRW.EditItemIndex)

    End Sub

    Sub dgRW_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        RW_BindPekerjaan_OnLoad()
    End Sub

    Sub dgRW_Delete(ByVal id As String, ByVal idx As String)
        Dim strOpCd As String = "PR_PR_TRX_BKM_RWJOB_TEMP_DEL"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP|ID|IDX"
        strParamValue = Request.UserHostAddress & "|" & id & "|" & idx

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RWJOB_TEMP_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub dgRW_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_id As HiddenField = CType(E.Item.FindControl("dgRW_hid_id"), HiddenField)
        Dim hid_idx As HiddenField = CType(E.Item.FindControl("dgRW_hid_idx"), HiddenField)
        dgRW_Delete(hid_id.Value, hid_idx.Value)
        RW_BindPekerjaan_OnLoad()
    End Sub

    Protected Sub dgRW_ddl_ededit_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim content As String = item.Cells(0).Text
        Dim ddlType As DropDownList = CType(item.Cells(0).FindControl("dgRW_ddl_ededit"), DropDownList)

        Dim thd As HiddenField = CType(item.Cells(6).FindControl("dgRW_hid_ec"), HiddenField)
        thd.Value = ddlType.SelectedItem.Value.Trim()

    End Sub

    Protected Sub dgRW_ddl_jb_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim content As String = item.Cells(0).Text
        Dim ddljob As DropDownList = CType(item.Cells(1).FindControl("dgRW_ddl_jb"), DropDownList)
        Dim ddlblk As DropDownList
        Dim lbluom As Label


        Dim sdc As String
        Dim sjc As String
        Dim ssc As String

        If isNew.Value = "True" Then
            sdc = ddldivisicode.SelectedItem.Value.Trim()
            sjc = ddljob.SelectedItem.Text.Trim()
            ssc = ddlbksubcategory.SelectedItem.Value.Trim()
        Else
            sdc = hid_div.Value
            sjc = ddljob.SelectedItem.Text.Trim()
            ssc = hid_subcat.Value
        End If

        'ddlblk = CType(item.Cells(2).FindControl("dgRW_ddl_cb"), DropDownList)
        'ddlblk.DataSource = RW_Blok(sdc, sjc, ssc, "K")
        'ddlblk.DataTextField = "BlokDet"
        'ddlblk.DataValueField = "BlokCode"
        'ddlblk.DataBind()
        'ddl_DgSorting(ddlblk, hastbl_cb, "")
        'ddlblk.Visible = True

        lbluom = CType(item.Cells(7).FindControl("dgRW_lbl_um"), Label)
        lbluom.Text = dgRw_GetUOM(sjc, ssc, sdc, "K")

    End Sub

    Protected Sub dgRW_ddl_cb_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim content As String = item.Cells(0).Text
        Dim ddljob As DropDownList = CType(item.Cells(1).FindControl("dgRW_ddl_jb"), DropDownList)
        Dim ddlblk As DropDownList = CType(item.Cells(2).FindControl("dgRW_ddl_cb"), DropDownList)
        Dim lblnrm As Label

        Dim ssc As String
        Dim sdc As String
        Dim sjc As String
        Dim syr As String

        If isNew.Value = "True" Then
            ssc = ddlbksubcategory.SelectedItem.Value.Trim()
            sdc = ddldivisicode.SelectedItem.Value.Trim()
            sjc = ddljob.SelectedItem.Text.Trim()
            syr = Mid(ddlblk.SelectedItem.Text.Trim(), InStr(ddlblk.SelectedItem.Text.Trim(), "(") + 1, Len(ddlblk.SelectedItem.Text.Trim()) - InStr(ddlblk.SelectedItem.Text.Trim(), "(") - 1)
        Else
            ssc = hid_subcat.Value
            sdc = hid_div.Value
            sjc = ddljob.SelectedItem.Text.Trim()
            syr = Mid(ddlblk.SelectedItem.Text.Trim(), InStr(ddlblk.SelectedItem.Text.Trim(), "(") + 1, Len(ddlblk.SelectedItem.Text.Trim()) - InStr(ddlblk.SelectedItem.Text.Trim(), "(") - 1)
        End If

        lblnrm = CType(item.Cells(5).FindControl("dgRW_lbl_nr"), Label)
        lblnrm.Text = dgRw_GetNorma(sjc, ssc, sdc, "K", syr)

    End Sub

#End Region

#Region "Event PN"
    'Potong Buah
#Region "Event PN-PBS"

    Sub PBS_onLoad_Display(ByVal bkmcode As String)
        Dim strOpCd As String = "PR_PR_TRX_BKMLN_PNPBS_LOAD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "IP|BKM"
        strParamValue = Request.UserHostAddress & "|" & bkmcode

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_PNPBS_LOAD&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

    End Sub

    Sub dgPBS_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim hd As HiddenField
            Dim DDLabs As DropDownList = CType(e.Item.FindControl("dgPBS_ddl_cb"), DropDownList)


            hd = CType(e.Item.FindControl("dgPBS_hid_cb"), HiddenField)
            ddl_DgSorting(DDLabs, hastbl_cb, hd.Value.Trim())

            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("Delete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"


            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Protected Sub dgPBS_Blok_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim content As String = item.Cells(0).Text
        Dim ddlType As DropDownList = CType(item.Cells(2).FindControl("dgPBS_ddl_cb"), DropDownList)

        Dim tbox As TextBox = CType(item.Cells(6).FindControl("dgPBS_txt_bjr"), TextBox)
        tbox.Text = GetBjr(ddlType.SelectedItem.Value.Trim())
        Dim txtjjg As TextBox = CType(item.Cells(3).FindControl("dgPBS_txt_jg"), TextBox)
        Dim txtbjr As TextBox = CType(item.Cells(6).FindControl("dgPBS_txt_bjr"), TextBox)
        Dim txtkg As TextBox = CType(item.Cells(6).FindControl("dgPBS_txt_kg"), TextBox)
        Dim txthk As TextBox = CType(item.Cells(1).FindControl("dgPBS_txt_hk"), TextBox)
        If txtbjr.Text = "" Then txtbjr.Text = "0"
        If txtjjg.Text <> "" Then
            txtkg.Text = CSng(txtjjg.Text) * CSng(txtbjr.Text)
        End If
        txthk.Text = Math.Round(CSng(txtkg.Text) / GetBasisTgs(ddlType.SelectedItem.Value.Trim()), 2)
    End Sub

    Protected Sub dgPBS_jjg_OnTextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim txt As TextBox = CType(sender, TextBox)
        Dim cell As TableCell = CType(txt.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)

        Dim txtjjg As TextBox = CType(item.Cells(3).FindControl("dgPBS_txt_jg"), TextBox)
        Dim txtbjr As TextBox = CType(item.Cells(6).FindControl("dgPBS_txt_bjr"), TextBox)
        Dim txtkg As TextBox = CType(item.Cells(6).FindControl("dgPBS_txt_kg"), TextBox)
        Dim ddlType As DropDownList = CType(item.Cells(2).FindControl("dgPBS_ddl_cb"), DropDownList)
        Dim txthk As TextBox = CType(item.Cells(1).FindControl("dgPBS_txt_hk"), TextBox)

        If txtbjr.Text = "" Then txtbjr.Text = "0"
        txtkg.Text = CSng(txtjjg.Text) * CSng(txtbjr.Text)
        txthk.Text = Math.Round(CSng(txtkg.Text) / GetBasisTgs(ddlType.SelectedItem.Value.Trim()), 2)
    End Sub

    Sub PBS_add_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        ' save dulu
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKM_PNPBS_TEMP_UPDADD"
        Dim hid_id As HiddenField
        Dim hid_ec As HiddenField
        Dim hid_jb As HiddenField
        Dim lbl_ed As Label
        Dim ddl_cb As DropDownList
        Dim txt_hk As TextBox
        Dim txt_ha As TextBox
        Dim txt_jjg As TextBox
        Dim txt_bjr As TextBox
        Dim txt_kg As TextBox
        Dim txt_rot As TextBox
        Dim chk_dm As CheckBox
        Dim chk_dl As CheckBox
        Dim chk_dt As CheckBox
        Dim chk_dp As CheckBox
        Dim chk_ds As CheckBox
        Dim txt_db As TextBox
        Dim ddl_ed As DropDownList
        Dim ed As String = ""

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        For i = 0 To dgPBS.Items.Count - 1

            hid_id = dgPBS.Items.Item(i).FindControl("dgPBS_hid_id")
            hid_ec = dgPBS.Items.Item(i).FindControl("dgPBS_hid_ec")
            If hid_id.Value = 0 Then
                ddl_ed = dgPBS.Items.Item(i).FindControl("dgPBS_ddl_ededit")
                ed = ddl_ed.SelectedItem.Text.Trim()
            Else
                lbl_ed = dgPBS.Items.Item(i).FindControl("dgPBS_lbl_ed")
                ed = lbl_ed.Text
            End If
            hid_jb = dgPBS.Items.Item(i).FindControl("dgPBS_hid_jb")
            ddl_cb = dgPBS.Items.Item(i).FindControl("dgPBS_ddl_cb")
            txt_hk = dgPBS.Items.Item(i).FindControl("dgPBS_txt_hk")
            txt_ha = dgPBS.Items.Item(i).FindControl("dgPBS_txt_ha")
            txt_jjg = dgPBS.Items.Item(i).FindControl("dgPBS_txt_jg")
            txt_bjr = dgPBS.Items.Item(i).FindControl("dgPBS_txt_bjr")
            txt_kg = dgPBS.Items.Item(i).FindControl("dgPBS_txt_kg")
            txt_rot = dgPBS.Items.Item(i).FindControl("dgPBS_txt_rot")
            chk_dm = dgPBS.Items.Item(i).FindControl("dgPBS_chk_Dmentah")
            chk_dl = dgPBS.Items.Item(i).FindControl("dgPBS_chk_Dtinggal")
            chk_dt = dgPBS.Items.Item(i).FindControl("dgPBS_chk_Dtph")
            chk_dp = dgPBS.Items.Item(i).FindControl("dgPBS_chk_Dpjg")
            chk_ds = dgPBS.Items.Item(i).FindControl("dgPBS_chk_DSengkleh")
            txt_db = dgPBS.Items.Item(i).FindControl("dgPBS_txt_Dbasis")

            If hid_ec.Value <> "" Then
                ParamName = "ID|IP|EC|ED|JB|CB|HK|HA|JJG|BJR|KG|ROT|DM|DL|DT|DP|DS|DB"
                ParamValue = Trim(hid_id.Value) & "|" & _
                             Trim(Request.UserHostAddress) & "|" & _
                             Trim(hid_ec.Value) & "|" & _
                             Trim(ed) & "|" & _
                             Trim(hid_jb.Value) & "|" & _
                             ddl_cb.SelectedItem.Value.Trim() & "|" & _
                             IIf(txt_hk.Text = "", "null", txt_hk.Text) & "|" & _
                             IIf(txt_ha.Text = "", "null", txt_ha.Text) & "|" & _
                             IIf(txt_jjg.Text = "", "null", txt_jjg.Text) & "|" & _
                             IIf(txt_bjr.Text = "", "null", txt_bjr.Text) & "|" & _
                             IIf(txt_kg.Text = "", "null", txt_kg.Text) & "|" & _
                             IIf(txt_rot.Text = "", "null", txt_rot.Text) & "|" & _
                             IIf(chk_dm.Checked, "1", "0") & "|" & _
                             IIf(chk_dl.Checked, "1", "0") & "|" & _
                             IIf(chk_dt.Checked, "1", "0") & "|" & _
                             IIf(chk_dp.Checked, "1", "0") & "|" & _
                             IIf(chk_ds.Checked, "1", "0") & "|" & _
                             IIf(txt_db.Text = "", "null", txt_db.Text)


                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RWJOB_TEMP_UPDADD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next

        Dim dr As DataRow
        Dim dataSet As DataSet

        dataSet = dgPBS_Reload()
        dr = dataSet.Tables(0).NewRow()
        dr("id") = 0
        dr("ipuser") = Request.UserHostAddress
        dr("codeemp") = ""
        dr("empdet") = ""
        dr("codejob") = hid_jb.Value
        dr("codeblok") = ""
        dr("hk") = DBNull.Value
        dr("ha") = DBNull.Value
        dr("jjg") = DBNull.Value
        dr("bjr") = DBNull.Value
        dr("kg") = DBNull.Value
        dr("rotasi") = DBNull.Value
        dr("dnd_Mentah") = "0"
        dr("dnd_Tinggal") = "0"
        dr("dnd_Tph") = "0"
        dr("dnd_Pjng") = "0"
        dr("dnd_Sengkleh") = "0"
        dr("dnd_Basis") = DBNull.Value

        dataSet.Tables(0).Rows.Add(dr)
        dgPBS.DataSource = dataSet
        dgPBS.DataBind()
        dgPBS.EditItemIndex = dgPBS.Items.Count - 1
        dgPBS.DataBind()
        PN_PBS_BindEmpByMdr_edit(dgPBS.EditItemIndex)
        PN_PBS_Blok_edit(dgPBS.EditItemIndex)

    End Sub

    Sub dgPBS_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        PN_PBS_BindPekerjaan_OnLoad()
    End Sub

    Protected Sub dgPBS_ddl_ededit_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim content As String = item.Cells(0).Text
        Dim ddlType As DropDownList = CType(item.Cells(0).FindControl("dgPBS_ddl_ededit"), DropDownList)

        Dim ted As HiddenField = CType(item.Cells(3).FindControl("dgPBS_hid_ec"), HiddenField)
        ted.Value = ddlType.SelectedItem.Value.Trim()
        Dim thk As TextBox = CType(item.Cells(1).FindControl("dgPBS_txt_hk"), TextBox)
        thk.Text = "1.00"
    End Sub

    Sub dgPBS_Delete(ByVal id As String, ByVal idx As String)
        Dim strOpCd As String = "PR_PR_TRX_BKM_PNPBS_TEMP_DEL"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP|ID|IDX"
        strParamValue = Request.UserHostAddress & "|" & id & "|" & idx

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNPBS_TEMP_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub dgPBS_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_id As HiddenField = CType(E.Item.FindControl("dgPBS_hid_id"), HiddenField)
        Dim hid_idx As HiddenField = CType(E.Item.FindControl("dgPBS_hid_idx"), HiddenField)
        dgPBS_Delete(hid_id.Value, hid_idx.Value)
        PN_PBS_BindPekerjaan_OnLoad()
    End Sub

#End Region

#Region "Event PN-KBS"

    Sub KBS_onLoad_Display(ByVal bkmcode As String)
        Dim strOpCd As String = "PR_PR_TRX_BKMLN_PNKBS_LOAD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "IP|BKM"
        strParamValue = Request.UserHostAddress & "|" & bkmcode

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_PNKBS_LOAD&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

    End Sub

    'Kutip Brondolan
    Sub dgKBS_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim hd As HiddenField
            Dim DDLabs As DropDownList = CType(e.Item.FindControl("dgKBS_ddl_cb"), DropDownList)
            hd = CType(e.Item.FindControl("dgKBS_hid_cb"), HiddenField)
            ddl_DgSorting(DDLabs, hastbl_cb, hd.Value.Trim())

            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("Delete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Sub KBS_add_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        ' save dulu
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKM_PNKBS_TEMP_UPDADD"
        Dim hid_id As HiddenField
        Dim hid_ec As HiddenField
        Dim hid_jb As HiddenField
        Dim lbl_ed As Label
        Dim ddl_cb As DropDownList
        Dim txt_hk As TextBox
        Dim txt_ha As TextBox
        Dim txt_kg As TextBox
        Dim txt_rot As TextBox
        Dim txt_dk As CheckBox
        Dim chk_db As CheckBox
        Dim chk_dt As CheckBox
        Dim ddl_ed As DropDownList
        Dim ed As String = ""

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        For i = 0 To dgKBS.Items.Count - 1

            hid_id = dgKBS.Items.Item(i).FindControl("dgKBS_hid_id")
            hid_ec = dgKBS.Items.Item(i).FindControl("dgKBS_hid_ec")
            If hid_id.Value = 0 Then
                ddl_ed = dgKBS.Items.Item(i).FindControl("dgKBS_ddl_ededit")
                ed = ddl_ed.SelectedItem.Text.Trim()
            Else
                lbl_ed = dgKBS.Items.Item(i).FindControl("dgKBS_lbl_ed")
                ed = lbl_ed.Text
            End If
            hid_jb = dgKBS.Items.Item(i).FindControl("dgKBS_hid_jb")
            ddl_cb = dgKBS.Items.Item(i).FindControl("dgKBS_ddl_cb")
            txt_hk = dgKBS.Items.Item(i).FindControl("dgKBS_txt_hk")
            txt_ha = dgKBS.Items.Item(i).FindControl("dgKBS_txt_ha")
            txt_kg = dgKBS.Items.Item(i).FindControl("dgKBS_txt_kg")
            txt_rot = dgKBS.Items.Item(i).FindControl("dgKBS_txt_rot")
            txt_dk = dgKBS.Items.Item(i).FindControl("dgKBS_txt_Dkk")
            chk_db = dgKBS.Items.Item(i).FindControl("dgKBS_chk_dbk")
            chk_dt = dgKBS.Items.Item(i).FindControl("dgKBS_chk_Dtph")


            If hid_ec.Value <> "" Then
                ParamName = "ID|IP|EC|ED|JB|CB|HK|HA|JJG|BJR|KG|ROT|DK|DB|DT"
                ParamValue = Trim(hid_id.Value) & "|" & _
                             Trim(Request.UserHostAddress) & "|" & _
                             Trim(hid_ec.Value) & "|" & _
                             Trim(ed) & "|" & _
                             Trim(hid_jb.Value) & "|" & _
                             ddl_cb.SelectedItem.Value.Trim() & "|" & _
                             IIf(txt_hk.Text = "", "null", txt_hk.Text) & "|" & _
                             IIf(txt_ha.Text = "", "null", txt_ha.Text) & "|" & _
                             IIf(txt_kg.Text = "", "null", txt_kg.Text) & "|" & _
                             IIf(txt_rot.Text = "", "null", txt_rot.Text) & "|" & _
                             IIf(txt_dk.Text = "", "null", txt_dk.Text) & "|" & _
                             IIf(chk_db.Checked, "1", "0") & "|" & _
                             IIf(chk_dt.Checked, "1", "0") & "|"



                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNKBS_TEMP_UPDADD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next

        Dim dr As DataRow
        Dim dataSet As DataSet

        dataSet = dgKBS_Reload()
        dr = dataSet.Tables(0).NewRow()
        dr("id") = 0
        dr("ipuser") = Request.UserHostAddress
        dr("codeemp") = ""
        dr("empdet") = ""
        dr("codejob") = hid_jb.Value
        dr("codeblok") = ""
        dr("hk") = DBNull.Value
        dr("ha") = DBNull.Value
        dr("kg") = DBNull.Value
        dr("rotasi") = DBNull.Value
        dr("dnd_ktpktr") = DBNull.Value
        dr("dnd_brdktr") = "0"
        dr("dnd_tph") = "0"

        dataSet.Tables(0).Rows.Add(dr)
        dgKBS.DataSource = dataSet
        dgKBS.DataBind()
        dgKBS.EditItemIndex = dgKBS.Items.Count - 1
        dgKBS.DataBind()
        PN_KBS_BindEmpByMdr_edit(dgKBS.EditItemIndex)
        PN_KBS_Blok_edit(dgKBS.EditItemIndex)

    End Sub

    Sub dgKBS_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        PN_KBS_BindPekerjaan_OnLoad()
    End Sub

    Protected Sub dgKBS_ddl_ededit_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim content As String = item.Cells(0).Text
        Dim ddlType As DropDownList = CType(item.Cells(0).FindControl("dgKBS_ddl_ededit"), DropDownList)

        Dim ted As HiddenField = CType(item.Cells(7).FindControl("dgKBS_hid_ec"), HiddenField)
        ted.Value = ddlType.SelectedItem.Value.Trim()
        Dim thk As TextBox = CType(item.Cells(1).FindControl("dgKBS_txt_hk"), TextBox)
        thk.Text = "1.00"
    End Sub


    Sub dgKBS_Delete(ByVal id As String, ByVal idx As String)
        Dim strOpCd As String = "PR_PR_TRX_BKM_PNKBS_TEMP_DEL"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP|ID|IDX"
        strParamValue = Request.UserHostAddress & "|" & id & "|" & idx

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNPBS_TEMP_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub dgKBS_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_id As HiddenField = CType(E.Item.FindControl("dgKBS_hid_id"), HiddenField)
        Dim hid_idx As HiddenField = CType(E.Item.FindControl("dgKBS_hid_idx"), HiddenField)
        dgKBS_Delete(hid_id.Value, hid_idx.Value)
        PN_KBS_BindPekerjaan_OnLoad()
    End Sub


#End Region

#Region "Event PN_BMS"

    Sub BMS_onLoad_Display(ByVal bkmcode As String)
        Dim strOpCd As String = "PR_PR_TRX_BKMLN_PNBMS_LOAD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "IP|BKM"
        strParamValue = Request.UserHostAddress & "|" & bkmcode

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_PNBMS_LOAD&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

    End Sub

    'Muat TBS
    Sub dgBMS_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim hd As HiddenField
            Dim DDLabs As DropDownList = CType(e.Item.FindControl("dgBMS_ddl_cb"), DropDownList)
            hd = CType(e.Item.FindControl("dgBMS_hid_cb"), HiddenField)
            ddl_DgSorting(DDLabs, hastbl_cb, hd.Value.Trim())

            Dim DDLty As DropDownList = CType(e.Item.FindControl("dgBMS_ddl_tyjob"), DropDownList)
            hd = CType(e.Item.FindControl("dgBMS_hid_tyjob"), HiddenField)
            ddl_DgSorting(DDLty, hastbl_jbbms, hd.Value.Trim())


            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("Delete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Sub BMS_add_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        ' save dulu
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKM_PNBMS_TEMP_UPDADD"
        Dim hid_id As HiddenField
        Dim hid_ec As HiddenField
        Dim hid_jb As HiddenField
        Dim lbl_ed As Label
        Dim ddl_cb As DropDownList
        Dim ddl_tyjb As DropDownList
        Dim txt_hk As TextBox
        Dim txt_kg As TextBox
        Dim txt_rot As TextBox
        Dim txt_dnd As TextBox
        Dim ddl_ed As DropDownList
        Dim ed As String = ""

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        For i = 0 To dgBMS.Items.Count - 1

            hid_id = dgBMS.Items.Item(i).FindControl("dgBMS_hid_id")
            hid_ec = dgBMS.Items.Item(i).FindControl("dgBMS_hid_ec")
            If hid_id.Value = 0 Then
                ddl_ed = dgBMS.Items.Item(i).FindControl("dgBMS_ddl_ededit")
                ed = ddl_ed.SelectedItem.Text.Trim()
            Else
                lbl_ed = dgBMS.Items.Item(i).FindControl("dgBMS_lbl_ed")
                ed = lbl_ed.Text
            End If
            hid_jb = dgBMS.Items.Item(i).FindControl("dgBMS_hid_jb")
            ddl_cb = dgBMS.Items.Item(i).FindControl("dgBMS_ddl_cb")
            txt_hk = dgBMS.Items.Item(i).FindControl("dgBMS_txt_hk")
            txt_kg = dgBMS.Items.Item(i).FindControl("dgBMS_txt_kg")
            txt_rot = dgBMS.Items.Item(i).FindControl("dgBMS_txt_rot")
            txt_dnd = dgBMS.Items.Item(i).FindControl("dgBMS_txt_dnd")
            ddl_tyjb = dgBMS.Items.Item(i).FindControl("dgBMS_ddl_tyjob")

            If hid_ec.Value <> "" Then
                ParamName = "ID|IP|EC|ED|JB|CB|TY|HK|KG|ROT|DD"
                ParamValue = Trim(hid_id.Value) & "|" & _
                             Trim(Request.UserHostAddress) & "|" & _
                             Trim(hid_ec.Value) & "|" & _
                             Trim(ed) & "|" & _
                             Trim(hid_jb.Value) & "|" & _
                             ddl_cb.SelectedItem.Value.Trim() & "|" & _
                             ddl_tyjb.SelectedItem.Value.Trim() & "|" & _
                             IIf(txt_hk.Text = "", "null", txt_hk.Text) & "|" & _
                             IIf(txt_kg.Text = "", "null", txt_kg.Text) & "|" & _
                             IIf(txt_rot.Text = "", "null", txt_rot.Text) & "|" & _
                             IIf(txt_dnd.Text = "", "null", txt_dnd.Text)


                Try
                    intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNBMS_TEMP_UPDADD&errmesg=" & Exp.Message & "&redirect=")
                End Try
            End If

        Next

        Dim dr As DataRow
        Dim dataSet As DataSet

        dataSet = dgBMS_Reload()
        dr = dataSet.Tables(0).NewRow()
        dr("id") = 0
        dr("ipuser") = Request.UserHostAddress
        dr("codeemp") = ""
        dr("empdet") = ""
        dr("codejob") = hid_jb.Value
        dr("codeblok") = ""
        dr("tyjob") = "M"
        dr("hk") = DBNull.Value
        dr("kg") = DBNull.Value
        dr("rotasi") = DBNull.Value
        dr("denda") = DBNull.Value

        dataSet.Tables(0).Rows.Add(dr)
        dgBMS.DataSource = dataSet
        dgBMS.DataBind()
        dgBMS.EditItemIndex = dgBMS.Items.Count - 1
        dgBMS.DataBind()
        PN_BMS_BindEmpByMdr_edit(dgBMS.EditItemIndex)
        PN_BMS_Blok_edit(dgBMS.EditItemIndex)
        PN_BMS_TyJob_edit(dgBMS.EditItemIndex)

    End Sub

    Sub dgBMS_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        PN_BMS_BindPekerjaan_OnLoad()
    End Sub

    Protected Sub dgBMS_ddl_ededit_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim content As String = item.Cells(0).Text
        Dim ddlType As DropDownList = CType(item.Cells(0).FindControl("dgBMS_ddl_ededit"), DropDownList)

        Dim ted As HiddenField = CType(item.Cells(3).FindControl("dgBMS_hid_ec"), HiddenField)
        ted.Value = ddlType.SelectedItem.Value.Trim()
        Dim thk As TextBox = CType(item.Cells(1).FindControl("dgBMS_txt_hk"), TextBox)
        thk.Text = "1.00"
    End Sub

    Sub dgBMS_Delete(ByVal id As String, ByVal idx As String)
        Dim strOpCd As String = "PR_PR_TRX_BKM_PNBMS_TEMP_DEL"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP|ID|IDX"
        strParamValue = Request.UserHostAddress & "|" & id & "|" & idx

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_PNBMS_TEMP_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub dgBMS_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_id As HiddenField = CType(E.Item.FindControl("dgBMS_hid_id"), HiddenField)
        Dim hid_idx As HiddenField = CType(E.Item.FindControl("dgBMS_hid_idx"), HiddenField)
        dgBMS_Delete(hid_id.Value, hid_idx.Value)
        PN_BMS_BindPekerjaan_OnLoad()
    End Sub

#End Region

#End Region

#Region "Event BHN"
    Sub BHN_onLoad_Display(ByVal bkmcode As String)
        Dim strOpCd As String = "PR_PR_TRX_BKMLN_BAHAN_LOAD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer

        strParamName = "IP|BKM"
        strParamValue = Request.UserHostAddress & "|" & bkmcode

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_BAHAN_LOAD&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

    End Sub

    Sub dgBHN_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim hd As HiddenField
            Dim hd2 As HiddenField

            Dim DDLabs As DropDownList
            Dim DDLabs_itm As DropDownList
            Dim Updbutton As LinkButton


            hd = CType(e.Item.FindControl("dgBHN_hid_jb"), HiddenField)
            DDLabs = CType(e.Item.FindControl("dgBHN_ddl_jb"), DropDownList)
            ddl_DgSorting(DDLabs, hastbl_jbbhn, hd.Value.Trim())

            hd = CType(e.Item.FindControl("dgBHN_hid_itm"), HiddenField)
            hd2 = CType(e.Item.FindControl("dgBHN_hid_uom"), HiddenField)
            DDLabs_itm = CType(e.Item.FindControl("dgBHN_ddl_itm"), DropDownList)
            ddl_DgSorting(DDLabs_itm, hastbl_bhn, hd.Value.Trim() & "|" & hd2.Value.Trim())

            Updbutton = CType(e.Item.FindControl("Delete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If

    End Sub

    Sub BHN_add_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        ' save dulu
        Dim strOpCd_Upd As String = "PR_PR_TRX_BKM_RWBHN_TEMP_UPDADD"
        Dim hid_id As HiddenField
        Dim ddl_jb As DropDownList
        Dim ddl_ci As DropDownList
        Dim txt_qty As TextBox
        Dim lbl_um As Label
        Dim ed As String = ""

        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String = ""

        Dim intErrNo As Integer

        For i = 0 To dgBHN.Items.Count - 1

            hid_id = dgBHN.Items.Item(i).FindControl("dgBHN_hid_id")
            ddl_jb = dgBHN.Items.Item(i).FindControl("dgBHN_ddl_jb")
            ddl_ci = dgBHN.Items.Item(i).FindControl("dgBHN_ddl_itm")
            txt_qty = dgBHN.Items.Item(i).FindControl("dgBHN_txt_Qty")
            lbl_um = dgBHN.Items.Item(i).FindControl("dgBHN_lbl_um")

            If ddl_jb.Text <> "" Then

                If ddl_jb.SelectedItem.Value <> "" Then
                    ParamName = "ID|IP|JB|CI|QT|UM"
                    ParamValue = Trim(hid_id.Value) & "|" & _
                                 Trim(Request.UserHostAddress) & "|" & _
                                 ddl_jb.SelectedItem.Value.Trim() & "|" & _
                                 Left(ddl_ci.SelectedItem.Value.Trim(), InStr(ddl_ci.SelectedItem.Value.Trim(), "|") - 1) & "|" & _
                                 IIf(txt_qty.Text = "", "0", txt_qty.Text) & "|" & _
                                 Trim(lbl_um.Text)

                    Try

                        intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)

                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RWBHN_TEMP_UPDADD&errmesg=" & Exp.Message & "&redirect=")
                    End Try
                End If
            End If

        Next

        Dim dr As DataRow
        Dim dataSet As DataSet

        dataSet = dgBHN_Reload()
        dr = dataSet.Tables(0).NewRow()
        dr("id") = 0
        dr("idx") = 0
        dr("ipuser") = Request.UserHostAddress
        dr("job") = ""
        dr("codeitem") = ""
        dr("qty") = DBNull.Value
        dr("uom") = ""
        dataSet.Tables(0).Rows.Add(dr)

        dgBHN.DataSource = dataSet
        dgBHN.DataBind()
        dgBHN.EditItemIndex = dgBHN.Items.Count - 1
        dgBHN.DataBind()

        BHN_Bahan_edit(dgBHN.EditItemIndex)
        BHN_Pekerjaan_edit(dgBHN.EditItemIndex)
    End Sub

    Sub dgBHN_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        BHN_BindPekerjaan_OnLoad()
    End Sub

    Protected Sub dgBHN_ddl_itm_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim content As String = item.Cells(0).Text
        Dim ddlType As DropDownList = CType(item.Cells(0).FindControl("dgBHN_ddl_itm"), DropDownList)

        Dim tum As Label = CType(item.Cells(1).FindControl("dgBHN_lbl_um"), Label)
        tum.Text = Right(ddlType.SelectedItem.Value.Trim(), Len(ddlType.SelectedItem.Value.Trim()) - InStr(ddlType.SelectedItem.Value.Trim(), "|"))
    End Sub

    Sub dgBHN_Delete(ByVal id As String, ByVal idx As String)
        Dim strOpCd As String = "PR_PR_TRX_BKM_RWBHN_TEMP_DEL"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        strParamName = "IP|ID|IDX"
        strParamValue = Request.UserHostAddress & "|" & id & "|" & idx

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_RWBHN_TEMP_DEL&errmesg=" & Exp.Message & "&redirect=")
        End Try
    End Sub

    Sub dgBHN_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_id As HiddenField = CType(E.Item.FindControl("dgBHN_hid_id"), HiddenField)
        Dim hid_idx As HiddenField = CType(E.Item.FindControl("dgBHN_hid_idx"), HiddenField)
        dgBHN_Delete(hid_id.Value, hid_idx.Value)
        BHN_BindPekerjaan_OnLoad()
    End Sub

#End Region

#End Region


End Class
'            SEmpName = Mid(Trim(lblEmpCode.Text), InStr(lblEmpCode.Text, "(") + 1, Len(Trim(lblEmpCode.Text)) - InStr(Trim(lblEmpCode.Text), "(") - 1)
