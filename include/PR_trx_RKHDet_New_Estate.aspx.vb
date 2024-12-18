Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Infragistics.WebUI.UltraWebTab

Public Class PR_RKHDet_New_Estate : Inherits Page


#Region "Declare Var"

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents isNew As HtmlInputHidden
    Protected WithEvents hid_div As HtmlInputHidden
    Protected WithEvents txt_hid_emp As HtmlInputHidden

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

#Region "Var BK"
    Protected WithEvents txtWPDate As TextBox
    Protected WithEvents LblidM As Label
    Protected WithEvents ddldivisicode As DropDownList
 
    Protected WithEvents txtnotes As TextBox
  
    Protected WithEvents lblWPDate As Label
    Protected WithEvents btnSelDate As Image
    Protected WithEvents lbldivisicode As Label
  

    Protected WithEvents TABBKTOT As UltraWebTab


    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblupdatedby As Label


    Protected WithEvents SaveBtn As ImageButton

    Dim alert As Byte = 0
    Dim strEmpDivCode As String
    Dim hastbl_jb As New System.Collections.Hashtable()
    Dim hastbl_cb As New System.Collections.Hashtable()
    Dim hastbl_jbbhn As New System.Collections.Hashtable()
    Dim hastbl_bhn As New System.Collections.Hashtable()
    Dim hastbl_cbbhn As New System.Collections.Hashtable()
    Dim hastbl_jbbhnall As New System.Collections.Hashtable()
    Dim hastbl_subsub As New System.Collections.Hashtable()

#End Region

#Region "Var HKJ"
    Protected WithEvents dgjob As DataGrid
    Protected WithEvents dgbhn As DataGrid

    Protected WithEvents ddljobkat As DropDownList
    Protected WithEvents ddljobskat As DropDownList
    Protected WithEvents ddljob As DropDownList
    Protected WithEvents ddljobblk As DropDownList
    Protected WithEvents txtjobrot As TextBox

    Protected WithEvents txtjobhasil As TextBox
    Protected WithEvents txtjobhk As TextBox
    Protected WithEvents lbljobuom As Label

    Protected WithEvents ddlbhnjob As DropDownList
    Protected WithEvents ddlbhnblk As DropDownList
    Protected WithEvents ddlbhn As DropDownList
    Protected WithEvents txtbhnqty As TextBox
    Protected WithEvents lblbhnuom As Label


    Protected WithEvents tblSelection As HtmlTable

#End Region


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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strBKMCode = Trim(IIf(Request.QueryString("RKHCode") <> "", Request.QueryString("RKHCode"), Request.Form("RKHCode")))
            lblErrMessage.Visible = False

            If Not IsPostBack Then
                txtWPDate.Text = objGlobal.GetShortDate(strDateFmt, Now())
                If strBKMCode <> "" Then
                    isNew.Value = "False"
                    onLoad_Display()
                    BindBkCategory()
                Else
                    isNew.Value = "True"
                    BindDivisi()
                    BindBkCategory()
                End If
                onLoad_button()
            End If
        End If
    End Sub

#End Region

#Region "Function & Procedure"

    Public Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
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

        tcode = "RKH/" & strLocation & "/" & Mid(Trim(txtWPDate.Text), 4, 2) & Right(Trim(txtWPDate.Text), 2) & "/"
        ParamName = "LOC|ID|HDR|DIGIT"
        ParamValue = strLocation & "|20|" & tcode & "|4"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_GetID, ParamName, ParamValue, objNewID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_PR_TRX_IDNUMBER&errmesg=" & Exp.Message & "&redirect=hr/trx/HR_trx_EmployeeList.aspx")
        End Try

        Return Trim(objNewID.Tables(0).Rows(0).Item("N"))

    End Function

    Private Function RKH_beforesave() As Boolean
        Dim SDivCode As String
        Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)

        If ddldivisicode.SelectedItem.Value.Trim() <> "" Then
            SDivCode = ddldivisicode.SelectedItem.Value.Trim()
        Else
            lblErrMessage.Text = "Silakan isi  divisi"
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

        Return True
    End Function

    Sub RKH_onSave()
        Dim strIDM As String
        Dim SDivCode As String
        Dim SDate As String = Date_Validation(Trim(txtWPDate.Text), False)
        Dim strnotes As String


        Dim strOpCd_Up As String = "PR_PR_TRX_RKH_MAIN_ADD"
        Dim dt As String = objGlobal.GetShortDate(strDateFmt, Now())
        Dim SM As String = Mid(Trim(txtWPDate.Text), 4, 2)
        Dim SY As String = Right(Trim(txtWPDate.Text), 4)
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        If isNew.Value = "True" Then
            strIDM = getCode()
        Else
            strIDM = LblidM.Text.Trim
        End If
        LblidM.Text = strIDM

        If ddldivisicode.SelectedItem.Value.Trim() <> "" Then
            SDivCode = ddldivisicode.SelectedItem.Value.Trim()
        Else
            SDivCode = ""
        End If

        strnotes = txtnotes.Text.Trim()

        ParamNama = "RKH|LOC|AM|AY|BD|DC|NTS|ST|CD|UD|UI"
        ParamValue = strIDM & "|" & _
                     strLocation & "|" & _
                     SM & "|" & _
                     SY & "|" & _
                     SDate & "|" & _
                     SDivCode & "|" & _
                     strnotes & "|1|" & _
                     DateTime.Now() & "|" & _
                     DateTime.Now() & "|" & _
                     strUserId

        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Up, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKH_MAIN_ADD&errmesg=" & ex.ToString() & "&redirect=")
        End Try

    End Sub


#Region "Function & Procedure HJK"
    Private Sub HJK_JOB_onSave()
        Dim strOpCd_Upd As String = "PR_PR_TRX_RKHLN_MJOB_UPD"
        Dim strIDM As String = LblidM.Text.Trim
        Dim sIdDIv As String
        Dim jc As String = ""
        Dim cb As String = ""
        Dim hs As String = ""
        Dim um As String = ""
        Dim ro As String = ""
        Dim hk As String = ""

        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        'Save
        If isNew.Value = "True" Then
            sIdDIv = ddldivisicode.SelectedItem.Value.Trim()
        Else
            sIdDIv = Left(lbldivisicode.Text.Trim(), InStr(lbldivisicode.Text.Trim(), " (") - 1)
        End If

        jc = ddljob.SelectedItem.Value.Trim()
        cb = ddljobblk.SelectedItem.Value.Trim()
        hs = IIf(txtjobhasil.Text.Trim = "", "0", txtjobhasil.Text.Trim)
        um = lbljobuom.Text.Trim
        ro = IIf(txtjobrot.Text.Trim = "", "0", txtjobrot.Text.Trim)
        hk = IIf(txtjobhk.Text.Trim = "", "0", txtjobhk.Text.Trim)


        'Save only employee and job exists and blok  and hk and hasil
        If (jc <> "") And (cb <> "") And (hk <> "") And (hs <> "") Then
            ParamName = "RKH|JC|BC|LOC|HK|HS|UM|ROT|ST|CD|UD|UI"
            ParamValue = strIDM & "|" & _
                         jc & "|" & _
                         cb & "|" & _
                         strLocation & "|" & _
                         hk & "|" & _
                         hs & "|" & _
                         um & "|" & _
                         ro & "|" & _
                         "1" & "|" & _
                         DateTime.Now() & "|" & _
                         DateTime.Now() & "|" & _
                         strUserId

            Try
                intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKHLN_MJOB_UPD&errmesg=" & Exp.Message & "&redirect=")
            End Try
        Else
            UserMsgBox(Me, "Silakan input pekerjaan,blok,hasil kerja & hk")
            Exit Sub
        End If

    End Sub

    Private Sub HJK_BHN_onSave()
        Dim strOpCd_Upd As String = "PR_PR_TRX_RKHLN_MBHN_UPD"
        Dim strIDM As String = LblidM.Text
        Dim jc As String = ""
        Dim cb As String = ""
        Dim it As String
        Dim qt As String
        Dim um As String


        Dim i As Integer
        Dim ParamName As String
        Dim ParamValue As String
        Dim intErrNo As Integer


        'Save


        jc = ddlbhnjob.SelectedItem.Value.Trim
        jc = Left(jc, jc.IndexOf(" ") + 1).Trim()

        cb = ddlbhnjob.SelectedItem.Value.Trim()
        cb = Mid(cb, cb.IndexOf(" ") + 1, (cb.LastIndexOf(" ")) - cb.IndexOf(" ")).Trim
        
        it = ddlbhn.SelectedItem.Value.Trim
        qt = IIf(txtbhnqty.Text.Trim = "", "0", txtbhnqty.Text.Trim)

        um = lblbhnuom.Text.Trim


        'Save only employee and job exists and blok  and hk and hasil
        If (jc <> "") And (cb <> "") And (it <> "") And (qt <> "") Then
            ParamName = "RKH|JC|BC|IC|QT|UM|LOC|ST|CD|UD|UI"
            ParamValue = strIDM & "|" & _
                         jc & "|" & _
                         cb & "|" & _
                         it & "|" & _
                         qt & "|" & _
                         um & "|" & _
                         strLocation & "|1|" & _
                         DateTime.Now() & "|" & _
                         DateTime.Now() & "|" & _
                         strUserId

            Try
                intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamName, ParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKMLN_MBHN_UPD&errmesg=" & Exp.Message & "&redirect=")
            End Try
        End If

    End Sub

    Private Function HJK_JOB_Load() As DataSet
        Dim strOpCd As String = "PR_PR_TRX_RKHLN_MJOB_LOAD"
        Dim objCodeDs As New Object()
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String
        
        ParamNama = "SEARCH|SORT"
        ParamValue = "AND CodeRKH='" & LblidM.Text.Trim() & "'|ORDER BY job,codeblok"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKHLN_MJOB_LOAD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objCodeDs
    End Function

    Private Function HJK_BHN_Load() As DataSet
        Dim strOpCd As String = "PR_PR_TRX_RKHLN_MBHN_LOAD"
        Dim objCodeDs As New Object()
        Dim intErrNo As Integer
        Dim ParamNama As String
        Dim ParamValue As String

        ParamNama = "LOC|SEARCH|SORT"
        ParamValue = strLocation & "|WHERE CodeRKH='" & LblidM.Text.Trim() & "'|ORDER BY job,codeblok,item"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, ParamNama, ParamValue, objCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKHLN_MBHN_LOAD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objCodeDs
    End Function

    Sub HJK_JOB()
        dgjob.DataSource = HJK_JOB_Load()
        dgjob.DataBind()
    End Sub

    Sub HJK_BHN()
        dgbhn.DataSource = HJK_BHN_Load()
        dgbhn.DataBind()
    End Sub

    Function getbhnuom(ByVal cd As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_BAHAN_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim filter As String = ""
        Dim dr As DataRow

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE itemcode = '" & cd & "'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_BAHAN_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        Return Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("UomCode"))
    End Function

#End Region


#End Region

#Region "Binding"

#Region "Binding BK"

    Sub BindDivisi()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_DIVISICODE_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim StrFilter As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow

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

#End Region

#Region "Binding HKJ"

    'PEKERJAAN
    Sub BindBkCategory()
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BK_CATEGORY_GET_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpDivDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE (CatID <> 'AD') |"

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

        ddljobkat.DataSource = objEmpDivDs.Tables(0)
        ddljobkat.DataTextField = "CatName"
        ddljobkat.DataValueField = "CatID"
        ddljobkat.DataBind()
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
        strParamValue = "AND idCat='" & id & "' |Order by SubCatID"


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

        ddljobskat.DataSource = objEmpDivDs.Tables(0)
        ddljobskat.DataTextField = "SubCatName"
        ddljobskat.DataValueField = "SubCatID"
        ddljobskat.DataBind()

    End Sub

    Sub BindPekerjaan(ByVal cat As String, ByVal subcat As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_RKH_JOB_LIST"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpCodeDs As New Object()
        Dim dr As DataRow


        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE CatID='" & cat & "' AND SubCatId='" & subcat & "' AND LocCode='" & strLocation & "' And Status='1'|Order by Description"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKH_JOB_LIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpCodeDs.Tables(0).Rows.Count - 1
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("jobcode") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("jobcode"))
                objEmpCodeDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("Description")) & " (" & Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("jobcode")) & ") " & Trim(objEmpCodeDs.Tables(0).Rows(intCnt).Item("Uom"))
            Next
        End If

        dr = objEmpCodeDs.Tables(0).NewRow()
        dr("jobcode") = ""
        dr("Description") = "Pilih Pekerjaan"
        objEmpCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddljob.DataSource = objEmpCodeDs.Tables(0)
        ddljob.DataTextField = "Description"
        ddljob.DataValueField = "jobcode"
        ddljob.DataBind()

    End Sub

    Sub Bind_Blok(ByVal sdc As String, ByVal sjc As String, ByVal ssc As String, ByVal sku As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_STP_BLOK_RKH_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow


        strParamName = "LOC|SC|DC|JC"
        strParamValue = strLocation & "|" & ssc & "|" & sdc & "|" & sjc

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BLOK_BKM_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode"))
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("blok") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("BlokCode")) & " (" & Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("blok")) & ")"
            Next
        End If

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("BlokCode") = ""
        dr("blok") = "Pilih Blok"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddljobblk.DataSource = objEmpBlkDs.Tables(0)
        ddljobblk.DataTextField = "blok"
        ddljobblk.DataValueField = "BlokCode"
        ddljobblk.DataBind()


    End Sub


    'BAHAN
    Sub BindJobBahan()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_RKHLN_MBHN_LOAD_JOB"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim dr As DataRow

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE CodeRKH='" & LblidM.Text.Trim() & "' AND A.LocCode = '" & strLocation & "' AND A.Status='1'|ORDER BY job"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKHLN_MBHN_LOAD_JOB&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("codejob") = ""
        dr("Job") = "Pilih Pekerjaan+Blok"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)


        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("codejob") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("codejob"))
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("job") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("job"))
            Next
        End If


        ddlbhnjob.DataSource = objEmpBlkDs.Tables(0)
        ddlbhnjob.DataTextField = "job"
        ddlbhnjob.DataValueField = "codejob"
        ddlbhnjob.DataBind()
    End Sub

    Sub BindBahan(ByVal cat As String)
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_BKM_BAHAN_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objEmpBlkDs As New Object()
        Dim filter As String = ""
        Dim dr As DataRow

        Select Case Trim(cat)
            Case "AD"
                filter = "AND (ProdTypeCode in ('005')) "
            Case "RW"
                filter = "AND (ProdTypeCode in ('001','002','003','004','005','201')) "
            Case "UM"
                filter = "AND (ProdTypeCode in ('005','201')) "
            Case "CV"
                filter = "AND (ProdTypeCode in ('101','102','103','201')) "
            Case "TR"
                filter = "AND (ProdTypeCode in ('201','102','103') or ProdTypeCode like '4%' or ProdTypeCode like '5%' or ProdTypeCode like '9%')"
            Case "PN"
                filter = "AND (ProdTypeCode in ('005','661','201')) "
        End Select

        strParamName = "SEARCH|SORT"
        strParamValue = "WHERE LocCode = '" & strLocation & "' " & filter & "|ORDER BY Descr"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_BKM_BAHAN_LIST_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dr = objEmpBlkDs.Tables(0).NewRow()
        dr("itemCode") = ""
        dr("Descr") = "Pilih Bahan"
        objEmpBlkDs.Tables(0).Rows.InsertAt(dr, 0)


        If objEmpBlkDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objEmpBlkDs.Tables(0).Rows.Count - 1
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("itemCode") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("itemCode"))
                objEmpBlkDs.Tables(0).Rows(intCnt).Item("Descr") = Trim(objEmpBlkDs.Tables(0).Rows(intCnt).Item("Descr"))
            Next
        End If


        ddlbhn.DataSource = objEmpBlkDs.Tables(0)
        ddlbhn.DataTextField = "Descr"
        ddlbhn.DataValueField = "itemCode"
        ddlbhn.DataBind()
    End Sub


#End Region

#End Region

#Region "Event"

    Sub onLoad_Display()
        Dim strOpCd_EmpDiv As String = "PR_PR_TRX_RKH_MAIN_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmpCodeDs As New Object()
        Dim e As New System.EventArgs()

        strParamName = "LOC|SEARCH|SORT"
        strParamValue = strLocation & "| AND RKHCode Like '%" & strBKMCode & "%' AND LocCode='" & strLocation & "'|"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_EmpDiv, strParamName, strParamValue, objEmpCodeDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_TRX_RKH_MAIN_GET&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objEmpCodeDs.Tables(0).Rows.Count = 1 Then
            LblidM.Text = strBKMCode
            strEmpDivCode = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("IDDiv"))

            BindDivisi()

            lbldivisicode.Text = strEmpDivCode & " (" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("divisi")) & ")"

            lblWPDate.Text = objGlobal.GetLongDate(objEmpCodeDs.Tables(0).Rows(0).Item("RKHDate"))
            txtWPDate.Text = Date_Validation(objEmpCodeDs.Tables(0).Rows(0).Item("RKHDate"), True)
            txtnotes.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("notes"))


            lblPeriod.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objEmpCodeDs.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = IIf(Trim(objEmpCodeDs.Tables(0).Rows(0).Item("Status")) = "1", "Active", "Delete")
            lblDateCreated.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblupdatedby.Text = Trim(objEmpCodeDs.Tables(0).Rows(0).Item("UserName"))


            HJK_JOB()
            BindJobBahan()
            HJK_BHN()
        End If
    End Sub

    Sub onLoad_button()

        SaveBtn.Attributes("onclick") = "javascript:return ConfirmAction('updateall');"

        If isNew.Value = "False" Then
            ddldivisicode.Visible = False
            txtWPDate.Visible = True
            btnSelDate.Visible = True
            lbldivisicode.Visible = True
            lblWPDate.Visible = False
        Else
            ddldivisicode.Visible = True
            txtWPDate.Visible = True
            btnSelDate.Visible = True
            lbldivisicode.Visible = False
            lblWPDate.Visible = False
        End If

        If isNew.Value = "False" Then
            tblSelection.Visible = True
        Else
            tblSelection.Visible = False
        End If

    End Sub

#Region "Event BK"


    Sub BtnNewBK_OnClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_RKHDet_New_Estate.aspx")
    End Sub

    Sub BtnSaveBK_OnClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        If isNew.Value = "True" Then
            If (Not RKH_beforesave()) Then
                UserMsgBox(Me, lblErrMessage.Text)
                alert = 1
                Exit Sub
            End If
        End If
        RKH_onSave()

        If (alert = 0) Then
            strBKMCode = LblidM.Text.Trim()
            isNew.Value = "False"
            onLoad_Display()
            onLoad_button()
        End If
    End Sub

    Sub BtnBackBK_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_RKHList_Estate.aspx")
    End Sub

#End Region

#Region "Event HKJ"
    'PEKERJAAN 

    Protected Sub ddljobkat_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindBKSubKategory(ddljobkat.SelectedItem.Value.Trim())
    End Sub

    Protected Sub ddljobskat_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindPekerjaan(ddljobkat.SelectedItem.Value.Trim(), ddljobskat.SelectedItem.Value.Trim())
    End Sub

    Protected Sub ddljob_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If ddljob.SelectedItem.Value.Trim <> "" Then
            Dim a As String = ddljob.SelectedItem.Text.Trim()
            Dim d As String = lbldivisicode.Text.Trim
            Bind_Blok(Left(d, d.IndexOf("(")).Trim, ddljob.SelectedItem.Value.Trim(), ddljobskat.SelectedItem.Value.Trim(), "")
            lbljobuom.Text = Right(a, Len(a) - a.LastIndexOf(" "))
        End If       
    End Sub

    Sub HKJ_JOB_add_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        HJK_JOB_onSave()
        HJK_JOB()
        txtjobrot.Text = ""
        txtjobhk.Text = ""
        txtjobhasil.Text = ""
        lbljobuom.Text = ""
        ddljobblk.Items.Clear()
        BindJobBahan()
    End Sub

    Sub dgjob_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("Delete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        End If
    End Sub

    Sub dgjob_GetItem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbl As Label
            Dim hid As HiddenField
            Dim objCodeDs As New Object()
            Dim d As String = lbldivisicode.Text.Trim

            hid = dgjob.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidcat")
            ddljobkat.SelectedValue = hid.Value.Trim()

            hid = dgjob.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidscat")
            BindBKSubKategory(ddljobkat.SelectedItem.Value.Trim())
            ddljobskat.SelectedValue = hid.Value.Trim()

            hid = dgjob.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidjob")
            BindPekerjaan(ddljobkat.SelectedItem.Value.Trim(), ddljobskat.SelectedItem.Value.Trim())
            ddljob.SelectedValue = hid.Value.Trim()

            lbl = dgjob.Items.Item(CInt(e.Item.ItemIndex)).FindControl("dgjobblk")
            Bind_Blok(Left(d, d.IndexOf("(")).Trim, ddljob.SelectedItem.Value.Trim(), ddljobskat.SelectedItem.Value.Trim(), "")
            ddljobblk.SelectedValue = lbl.Text.Trim()

            lbl = dgjob.Items.Item(CInt(e.Item.ItemIndex)).FindControl("dgjobvol")
            txtjobhasil.Text = lbl.Text
            lbl = dgjob.Items.Item(CInt(e.Item.ItemIndex)).FindControl("dgjobuom")
            lbljobuom.Text = lbl.Text
            lbl = dgjob.Items.Item(CInt(e.Item.ItemIndex)).FindControl("dgjobrot")
            txtjobrot.Text = lbl.Text
            lbl = dgjob.Items.Item(CInt(e.Item.ItemIndex)).FindControl("dgjobhk")
            txtjobhk.Text = lbl.Text

        End If
    End Sub

    Sub dgjob_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim hid_jb As HiddenField = CType(E.Item.FindControl("hidjob"), HiddenField)
        Dim lbl_div As Label = CType(E.Item.FindControl("dgjobblk"), Label)
        Dim ParamNama As String
        Dim ParamValue As String
        Dim strOpCd_Upd As String = "PR_PR_TRX_RKHLN_MJOB_DEL"
        Dim intErrNo As Integer

        ParamNama = "UD|UI|RKH|JC|BC|LOC"
        ParamValue = DateTime.Now & "|" & _
                     strUserId & "|" & _
                     LblidM.Text.Trim() & "|" & _
                     hid_jb.Value.Trim & "|" & _
                     lbl_div.Text.Trim & "|" & _
                     strLocation


        Try
            intErrNo = objOk.mtdInsertDataCommon(strOpCd_Upd, ParamNama, ParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_PR_STP_BK_JOB_ALOKASI_DEL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        HJK_JOB()
    End Sub

    'BAHAN
    Sub HKJ_BHN_add_onClick(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        HJK_BHN_onSave()
        HJK_BHN()
        lblbhnuom.Text = ""
        txtbhnqty.Text = ""
        ddlbhn.Items.Clear()

    End Sub

    Protected Sub ddlbhnjob_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If ddlbhnjob.SelectedItem.Value.Trim <> "" Then
            Dim a As String = ddlbhnjob.SelectedItem.Value.Trim()
            BindBahan(Right(a, Len(a) - a.LastIndexOf(" ")).Trim)
        End If
    End Sub

    Protected Sub ddlbhn_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If ddlbhn.SelectedItem.Value.Trim <> "" Then
            Dim a As String = ddlbhn.SelectedItem.Value.Trim()
            lblbhnuom.Text = getbhnuom(a)
        End If
    End Sub

    Sub dgbhn_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Updbutton As LinkButton
            Updbutton = CType(e.Item.FindControl("bhnDelete"), LinkButton)
            Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        End If
    End Sub

    Sub dgbhn_GetItem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbl As Label
            Dim hid As HiddenField
            Dim objCodeDs As New Object()
            Dim d As String = lbldivisicode.Text.Trim

            hid = dgbhn.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidbhnjob")
            ddlbhnjob.SelectedValue = hid.Value.Trim()

            hid = dgbhn.Items.Item(CInt(e.Item.ItemIndex)).FindControl("hidbhnitm")
            Dim a As String = ddlbhnjob.SelectedItem.Value.Trim()
            BindBahan(Right(a, Len(a) - a.LastIndexOf(" ")).Trim)
            ddlbhn.SelectedValue = hid.Value.Trim()

            lbl = dgbhn.Items.Item(CInt(e.Item.ItemIndex)).FindControl("dgbhnqty")
            txtbhnqty.Text = lbl.Text
            lbl = dgbhn.Items.Item(CInt(e.Item.ItemIndex)).FindControl("dgbhnuom")
            lblbhnuom.Text = lbl.Text
          

        End If
    End Sub


#End Region


#End Region





End Class
'            SEmpName = Mid(Trim(lblEmpCode.Text), InStr(lblEmpCode.Text, "(") + 1, Len(Trim(lblEmpCode.Text)) - InStr(Trim(lblEmpCode.Text), "(") - 1)
