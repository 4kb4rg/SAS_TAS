Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PR.clsMthEnd
Imports agri.Admin.clsShare
Imports agri.PWSystem.clsConfig


Public Class PR_MthEnd_PPH21_Estate : Inherits Page

    Protected WithEvents lblNoRecord As Label
    Protected WithEvents lblSuccess As Label
    Protected WithEvents lblFailed As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents btnProceed As ImageButton
    Protected WithEvents dgKRYWN As DataGrid
    Protected WithEvents dgNONKRYWN As DataGrid
    Protected WithEvents dgKRYWNThn As DataGrid
    Protected WithEvents dgNONKRYWNThn As DataGrid
    Protected WithEvents dgReconAmt As DataGrid
    Protected WithEvents dgReconAcc As DataGrid
    Protected WithEvents dgReconAmtCr As DataGrid
    Protected WithEvents dgReconAccCr As DataGrid

    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents ddlyear As DropDownList

    Protected WithEvents btnGenerate As ImageButton
    Protected WithEvents cbExcel As CheckBox
    Protected WithEvents BtnPreview As ImageButton

    Protected WithEvents btnGenerateThn As ImageButton
    Protected WithEvents cbExcelThn As CheckBox
    Protected WithEvents BtnPreviewThn As ImageButton

    Protected WithEvents cbExcelNONKRYWN As CheckBox
    Protected WithEvents BtnPreviewNONKRYWN As ImageButton
    Protected WithEvents btnGenerateNONKRYWN As ImageButton

    Protected WithEvents cbExcelNONKRYWNThn As CheckBox
    Protected WithEvents BtnPreviewNONKRYWNThn As ImageButton
    Protected WithEvents btnGenerateNONKRYWNThn As ImageButton

    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents btnFind As HtmlInputButton
    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents lblErrAccCode As Label

    Protected WithEvents ddlAccCodeCr As DropDownList
    Protected WithEvents btnFindCr As HtmlInputButton
    Protected WithEvents btnAddCr As ImageButton
    Protected WithEvents lblErrAccCodeCr As Label

    Protected WithEvents BtnPreviewRecAmt As ImageButton
    Protected WithEvents cbExcelRecAmt As CheckBox

    Protected WithEvents BtnPreviewRecAmtCr As ImageButton
    Protected WithEvents cbExcelRecAmtCr As CheckBox

    Dim objOk As New agri.GL.ClsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objMthEnd As New agri.PR.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGLSetup As New agri.GL.clsSetup()

    Dim strCompany As String
    Dim strLocation As String

    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
    Dim objLangCapDs As New DataSet()

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")

        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")

        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMthPayroll), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            btnProceed.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnProceed).ToString())
            btnProceed.Attributes("onclick") = "javascript:return confirm('Anda yakin akan melakukan Proses PPh 21 ?');"
            btnGenerateThn.Attributes("onclick") = "javascript:return confirm('Anda yakin akan melakukan Proses Jurnal PPh 21 Karyawan ?');"
            btnGenerateNONKRYWNThn.Attributes("onclick") = "javascript:return confirm('Anda yakin akan melakukan Proses Jurnal PPh 21 Non Karyawan ?');"

            lblErrMessage.Text = ""
            lblErrMessage.Visible = False
            lblNoRecord.Visible = False

            If Not Page.IsPostBack Then
                ddlMonth.SelectedIndex = CInt(Session("SS_SELACCMONTH")) - 1
                BindAccYear(Session("SS_SELACCYEAR"))

                BindAccCode("")
                BindAccCodeCr("")
                'ViewData()
                'ViewDataNonKrywn()
                'ViewDataThn()
                'ViewDataNonKrywnThn()
                'viewReconAmt()
                'ViewReconAcc()
                'ViewReconAmtCr()
                'ViewReconAccCr()
            End If
        End If
    End Sub

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
            intErrNo = objOk.mtdGetDataCommon(strOpCd, _
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
    End Sub

    Sub ViewData()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_GETLIST_ALL"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|GROUPTYPE|PPHTYPE"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & "1" & "|" & "BLN"

        'ParamName = "LOCCODE|ACCMONTH|ACCYEAR|STRSEARCH"
        'ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & _
        '     "AND A.EmpCode IN ( " & _
        '     " SELECT EmpCode " & _
        '     "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='1' AND CAST(AccMonth AS INT)=RTRIM('1') AND AccYear=RTRIM('" & Trim(strYr) & "') " & _
        '     "    UNION " & _
        '     "    SELECT EmpCode " & _
        '     "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='1' AND EmpCode NOT IN (SELECT EmpCode FROM HR_TRX_EMPHIST WHERE LocCode='" & Trim(strLocation) & "' AND CAST(AccMonth AS INT)=RTRIM('" & Trim(strMn) & "') AND AccYear=RTRIM('" & Trim(strYr) & "')) " & _
        '     "    AND (CAST(AccYear AS INT)*100)+CAST(AccMonth AS INT) <  (CAST('" & Trim(strYr) & "' AS INT)*100)+CAST(RTRIM('" & Trim(strMn) & "') AS INT) " & _
        '     "    GROUP BY LocCode, EmpCode, CodeEmpTy, IdDiv, CodeGrpJob " & _
        '     " ) "


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgKRYWN.DataSource = objDataSet
        dgKRYWN.DataBind()

        If objDataSet.Tables(0).Rows.Count > 0 Then
            'btnGenerate.Visible = False
            BtnPreview.Visible = True
            cbExcel.Visible = True

            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblEmpCode")
                If Trim(lbl.Text) = "ZZZZZZ" Then
                    lbl.Visible = False
                    'lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblEmpType")
                    'lbl.Visible = False
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblNama")
                    lbl.Text = Replace(lbl.Text, "ZZZ", "")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblRowNo")
                    lbl.Visible = False
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPeriod")
                    lbl.Visible = False
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblIDDiv")
                    lbl.Visible = False
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblCodeGrbJob")
                    lbl.Visible = False
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblCodeTg")
                    lbl.Visible = False
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblDOJ")
                    lbl.Visible = False
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblNPWP")
                    lbl.Visible = False
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblGapok")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPremi")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPremiTetap")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPremiLain")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblLembur")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblTPajak")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblTLain")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblAstek")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblCatuBeras")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblRapel")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblTHRBonus")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblTotPDP")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPotJbt")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPotJHT")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPotLain")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblTotPOT")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblDPP")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPTKP")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPKP")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPPH21Non")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPPH21Selisih")
                    lbl.Font.Bold = True
                    lbl = dgKRYWN.Items.Item(intCnt).FindControl("lblPPH21")
                    lbl.Font.Bold = True
                End If
            Next
        End If

    End Sub

    Sub ViewDataNonKrywn()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_GETLIST_ALL"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|GROUPTYPE|PPHTYPE"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & "2" & "|" & "BLN"

        'ParamName = "LOCCODE|ACCMONTH|ACCYEAR|STRSEARCH"
        'ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & _
        '            "AND A.EmpCode IN ( " & _
        '            " SELECT EmpCode " & _
        '            "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='2' AND CAST(AccMonth AS INT)=RTRIM('1') AND AccYear=RTRIM('" & Trim(strYr) & "') " & _
        '            "    UNION " & _
        '            "    SELECT EmpCode " & _
        '            "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='2' AND EmpCode NOT IN (SELECT EmpCode FROM HR_TRX_EMPHIST WHERE LocCode='" & Trim(strLocation) & "' AND CAST(AccMonth AS INT)=RTRIM('" & Trim(strMn) & "') AND AccYear=RTRIM('" & Trim(strYr) & "')) " & _
        '            "    AND (CAST(AccYear AS INT)*100)+CAST(AccMonth AS INT) <  (CAST('" & Trim(strYr) & "' AS INT)*100)+CAST(RTRIM('" & Trim(strMn) & "') AS INT) " & _
        '            "    GROUP BY LocCode, EmpCode, CodeEmpTy, IdDiv, CodeGrpJob " & _
        '            " ) "
                    
        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
            End Try

        dgNONKRYWN.DataSource = objDataSet
        dgNONKRYWN.DataBind()

        If objDataSet.Tables(0).Rows.Count > 0 Then
                'btnGenerateNONKRYWN.Visible = False
            BtnPreviewNONKRYWN.Visible = True
            cbExcelNONKRYWN.Visible = True

            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblEmpCodePPH")
                If Trim(lbl.Text) = "ZZZZZZ" Then
                    lbl.Visible = False
                    'lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblEmpTypePPH")
                    'lbl.Visible = False
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblNamaPPH")
                    lbl.Text = Replace(lbl.Text, "ZZZ", "")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblRowNoPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPeriodPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblIDDivPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblCodeGrbJobPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblCodeTgPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblDOJPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblNPWPPPH")
                    lbl.Visible = False
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblGapokPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPremiPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPremiTetapPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPremiLainPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblLemburPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblTPajakPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblTLainPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblAstekPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblCatuBerasPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblRapelPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblTHRBonusPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblTotPDPPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPotJbtPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPotJHTPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPotLainPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblTotPOTPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblDPPPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPTKPPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPKPPPH")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPPH21PPHNon")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPPH21PPHSelisih")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWN.Items.Item(intCnt).FindControl("lblPPH21PPH")
                    lbl.Font.Bold = True
                    End If
            Next
            End If

    End Sub

    Sub ViewDataThn()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_GETLIST_ALL"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|GROUPTYPE|PPHTYPE"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & "1" & "|" & "THN"

        'ParamName = "LOCCODE|ACCMONTH|ACCYEAR|STRSEARCH"
        'ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & _
        '      "AND A.EmpCode IN ( " & _
        '      " SELECT EmpCode " & _
        '      "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='1' AND CAST(AccMonth AS INT)=RTRIM('1') AND AccYear=RTRIM('" & Trim(strYr) & "') " & _
        '      "    UNION " & _
        '      "    SELECT EmpCode " & _
        '      "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='1' AND EmpCode NOT IN (SELECT EmpCode FROM HR_TRX_EMPHIST WHERE LocCode='" & Trim(strLocation) & "' AND CAST(AccMonth AS INT)=RTRIM('" & Trim(strMn) & "') AND AccYear=RTRIM('" & Trim(strYr) & "')) " & _
        '      "    AND (CAST(AccYear AS INT)*100)+CAST(AccMonth AS INT) <  (CAST('" & Trim(strYr) & "' AS INT)*100)+CAST(RTRIM('" & Trim(strMn) & "') AS INT) " & _
        '      "    GROUP BY LocCode, EmpCode, CodeEmpTy, IdDiv, CodeGrpJob " & _
        '      " ) "


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgKRYWNThn.DataSource = objDataSet
        dgKRYWNThn.DataBind()

        If objDataSet.Tables(0).Rows.Count > 0 Then
            btnGenerateThn.Visible = True
            BtnPreviewThn.Visible = True
            cbExcelThn.Visible = True

            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblEmpCodeThn")
                If Trim(lbl.Text) = "ZZZZZZ" Then
                    lbl.Visible = False
                    'lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblEmpTypeThn")
                    'lbl.Visible = False
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblNamaThn")
                    lbl.Text = Replace(lbl.Text, "ZZZ", "")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblRowNoThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPeriodThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblIDDivThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblCodeGrbJobThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblCodeTgThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblDOJThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblNPWPThn")
                    lbl.Visible = False
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblGapokThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPremiThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPremiTetapThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPremiLainThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblLemburThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblTPajakThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblTLainThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblAstekThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblCatuBerasThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblRapelThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblTHRBonusThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblTotPDPThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPotJbtThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPotJHTThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPotLainThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblTotPOTThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblDPPThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPTKPThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPKPThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPPH21NonThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPPH21SelisihThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPPH21Thn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPPH21DiSetorThn")
                    lbl.Font.Bold = True
                    lbl = dgKRYWNThn.Items.Item(intCnt).FindControl("lblPPH21KurangBayarThn")
                    lbl.Font.Bold = True
                End If
            Next
        End If

    End Sub

    Sub ViewDataNonKrywnThn()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_GETLIST_ALL"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|GROUPTYPE|PPHTYPE"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & "2" & "|" & "THN"

        'ParamName = "LOCCODE|ACCMONTH|ACCYEAR|STRSEARCH"
        'ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & _
        '            "AND A.EmpCode IN ( " & _
        '            " SELECT EmpCode " & _
        '            "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='2' AND CAST(AccMonth AS INT)=RTRIM('1') AND AccYear=RTRIM('" & Trim(strYr) & "') " & _
        '            "    UNION " & _
        '            "    SELECT EmpCode " & _
        '            "    FROM HR_TRX_EMPHIST A INNER JOIN HR_STP_EMPTYPE B ON A.CodeEmpTy = B.EmpTyCode WHERE LocCode='" & Trim(strLocation) & "' AND GroupType='2' AND EmpCode NOT IN (SELECT EmpCode FROM HR_TRX_EMPHIST WHERE LocCode='" & Trim(strLocation) & "' AND CAST(AccMonth AS INT)=RTRIM('" & Trim(strMn) & "') AND AccYear=RTRIM('" & Trim(strYr) & "')) " & _
        '            "    AND (CAST(AccYear AS INT)*100)+CAST(AccMonth AS INT) <  (CAST('" & Trim(strYr) & "' AS INT)*100)+CAST(RTRIM('" & Trim(strMn) & "') AS INT) " & _
        '            "    GROUP BY LocCode, EmpCode, CodeEmpTy, IdDiv, CodeGrpJob " & _
        '            " ) "


        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgNONKRYWNThn.DataSource = objDataSet
        dgNONKRYWNThn.DataBind()

        If objDataSet.Tables(0).Rows.Count > 0 Then
            btnGenerateNONKRYWNThn.Visible = True
            BtnPreviewNONKRYWNThn.Visible = True
            cbExcelNONKRYWNThn.Visible = True

            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblEmpCodePPHThn")
                If Trim(lbl.Text) = "ZZZZZZ" Then
                    lbl.Visible = False
                    'lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblEmpTypePPHThn")
                    'lbl.Visible = False
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblNamaPPHThn")
                    lbl.Text = Replace(lbl.Text, "ZZZ", "")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblRowNoPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPeriodPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblIDDivPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblCodeGrbJobPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblCodeTgPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblDOJPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblNPWPPPHThn")
                    lbl.Visible = False
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblGapokPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPremiPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPremiTetapPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPremiLainPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblLemburPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblTPajakPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblTLainPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblAstekPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblCatuBerasPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblRapelPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblTHRBonusPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblTotPDPPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPotJbtPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPotJHTPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPotLainPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblTotPOTPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblDPPPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPTKPPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPKPPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPPH21PPHNonThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPPH21PPHSelisihThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPPH21PPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPPH21DiSetorPPHThn")
                    lbl.Font.Bold = True
                    lbl = dgNONKRYWNThn.Items.Item(intCnt).FindControl("lblPPH21KurangBayarPPHThn")
                    lbl.Font.Bold = True
                End If
            Next
        End If

    End Sub


    Sub btnProceed_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_pph As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN"
        Dim strOpCd_Jrn As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_JOURNAL"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOCCODE|ACCMONTH|ACCYEAR"
        ParamValue = strLocation & "|" & strMn & "|" & strYr

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_pph, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblNoRecord.Visible = True
            lblNoRecord.Text = objDataSet.Tables(0).Rows(0).Item("Msg")
        End If

        ViewData()
        ViewDataNonKrywn()
        ViewDataThn()
        ViewDataNonKrywnThn()
        ViewReconAmt()
        ViewReconAmtCr()
    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Jrn As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_JOURNAL"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        '-- Generate auto jurnal
        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID|STATUSKRYWN"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & Trim(strUserId) & "|" & "1"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_Jrn, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblNoRecord.Visible = True
            lblNoRecord.Text = objDataSet.Tables(0).Rows(0).Item("Msg")
        End If
    End Sub

    Sub btnGenerateNONKRYWN_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Jrn As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_JOURNAL"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        '-- Generate auto jurnal
        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID|STATUSKRYWN"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & Trim(strUserId) & "|" & "2"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_Jrn, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblNoRecord.Visible = True
            lblNoRecord.Text = objDataSet.Tables(0).Rows(0).Item("Msg")
        End If
    End Sub

    Sub btnGenerateThn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Jrn As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_JOURNAL"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        '-- Generate auto jurnal
        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID|STATUSKRYWN"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & Trim(strUserId) & "|" & "1"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_Jrn, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblNoRecord.Visible = True
            lblNoRecord.Text = objDataSet.Tables(0).Rows(0).Item("Msg")
        End If
    End Sub

    Sub btnGenerateNONKRYWNThn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Jrn As String = "TX_CLSTRX_GENERATE_PPH21_KARYAWAN_JOURNAL"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        '-- Generate auto jurnal
        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID|STATUSKRYWN"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & Trim(strUserId) & "|" & "2"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_Jrn, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblNoRecord.Visible = True
            lblNoRecord.Text = objDataSet.Tables(0).Rows(0).Item("Msg")
        End If
    End Sub

    Sub btnRefresh_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        BindAccCode("")
        BindAccCodeCr("")
        ViewData()
        ViewDataNonKrywn()
        ViewDataThn()
        ViewDataNonKrywnThn()
        ViewReconAmt()
        ViewReconAcc()
        ViewReconAmtCr()
        ViewReconAccCr()
    End Sub

    Sub btnPreview_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=SPT_BULANAN_KARYAWAN-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgKRYWN.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

    Sub BtnPreviewNONKRYWN_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=SPT_BULANAN_NON_KARYAWAN-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgNONKRYWN.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

    Sub btnPreviewThn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=SPT_TAHUNAN_KARYAWAN-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgKRYWNThn.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

    Sub BtnPreviewNONKRYWNThn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlMonth.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=SPT_TAHUNAN_NON_KARYAWAN-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgNONKRYWNThn.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

    Sub dgKRYWN_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Private Sub dgKRYWN_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgKRYWN.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "NO"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PERIODE"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 7
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DATA KARYAWAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 11
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PENDAPATAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TOTAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "POTONGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TOTAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DISETAHUNKAN (KARYAWAN)"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PPh 21"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgItem.Font.Bold = True
            dgKRYWN.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgKRYWN_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgKRYWN.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(20).Visible = False
            e.Item.Cells(24).Visible = False
        End If
    End Sub

    Sub dgNONKRYWN_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Private Sub dgNONKRYWN_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgNONKRYWN.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "NO"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PERIODE"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 7
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DATA KARYAWAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 11
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PENDAPATAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TOTAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "POTONGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TOTAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DISETAHUNKAN (KARYAWAN)"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PPh 21"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgItem.Font.Bold = True
            dgNONKRYWN.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgNONKRYWN_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgNONKRYWN.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(20).Visible = False
            e.Item.Cells(24).Visible = False
        End If
    End Sub

    Sub dgKRYWNThn_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Private Sub dgKRYWNThn_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgKRYWNThn.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "NO"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PERIODE"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 7
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DATA KARYAWAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 11
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PENDAPATAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TOTAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "POTONGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TOTAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PENDAPATAN <BR>NETTO"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PTKP"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PKP"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PPh 21"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PPh 21 <BR>DISETOR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PPh 21 <BR>KURANG BAYAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgItem.Font.Bold = True
            dgKRYWNThn.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgKRYWNThn_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgKRYWNThn.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(20).Visible = False
            e.Item.Cells(24).Visible = False
            e.Item.Cells(25).Visible = False
            e.Item.Cells(26).Visible = False
            e.Item.Cells(27).Visible = False
            e.Item.Cells(31).Visible = False
            e.Item.Cells(32).Visible = False
        End If
    End Sub

    Sub dgNONKRYWNThn_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

    Private Sub dgNONKRYWNThn_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgNONKRYWNThn.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "NO"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PERIODE"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 7
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DATA KARYAWAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 11
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PENDAPATAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TOTAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "POTONGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TOTAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PENDAPATAN <BR>NETTO"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PTKP"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PKP"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PPh 21"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PPh 21 <BR>DISETOR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PPh 21 <BR>KURANG BAYAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgItem.Font.Bold = True
            dgNONKRYWNThn.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgNONKRYWNThn_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgNONKRYWNThn.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(20).Visible = False
            e.Item.Cells(24).Visible = False
            e.Item.Cells(25).Visible = False
            e.Item.Cells(26).Visible = False
            e.Item.Cells(27).Visible = False
            e.Item.Cells(31).Visible = False
            e.Item.Cells(32).Visible = False
        End If
    End Sub


    Sub BindAccCode(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ") & _
                    " AND ACC.AccCode NOT IN (SELECT AccCode FROM TX_PPH21_RECONACC WHERE LocCode='" & Trim(strLocation) & "' AND AccType='1')"

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Select Akun"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccCode.DataSource = dsForDropDown.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "_Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindAccCodeCr(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ") & _
                    " AND ACC.AccCode NOT IN (SELECT AccCode FROM TX_PPH21_RECONACC WHERE LocCode='" & Trim(strLocation) & "' AND AccType='2')"

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Select Akun"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccCodeCr.DataSource = dsForDropDown.Tables(0)
        ddlAccCodeCr.DataValueField = "AccCode"
        ddlAccCodeCr.DataTextField = "_Description"
        ddlAccCodeCr.DataBind()
        ddlAccCodeCr.SelectedIndex = intSelectedIndex
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode As String = "TX_CLSTRX_GENERATE_PPH21_RECONACC_ADD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strAccCode As String
        Dim intErrNo As Integer

        strAccCode = Trim(ddlAccCode.SelectedItem.Value)

        If strAccCode = "" Then
            Exit Sub
        Else
            Try
                strParamName = "ACCCODE|LOCCODE|ACCTYPE"
                strParamValue = strAccCode & "|" & strLocation & "|" & 1

                intErrNo = objOk.mtdInsertDataCommon(strOpCode, _
                                                         strParamName, _
                                                         strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.ToString() & "&redirect=")
            End Try
        End If

        BindAccCode("")
        ViewReconAcc()
    End Sub

    Sub btnAddCr_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCode As String = "TX_CLSTRX_GENERATE_PPH21_RECONACC_ADD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strAccCode As String
        Dim intErrNo As Integer

        strAccCode = Trim(ddlAccCodeCr.SelectedItem.Value)

        If strAccCode = "" Then
            Exit Sub
        Else
            Try
                strParamName = "ACCCODE|LOCCODE|ACCTYPE"
                strParamValue = strAccCode & "|" & strLocation & "|" & 2

                intErrNo = objOk.mtdInsertDataCommon(strOpCode, _
                                                         strParamName, _
                                                         strParamValue)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.ToString() & "&redirect=")
            End Try
        End If

        BindAccCodeCr("")
        ViewReconAccCr()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCode As String = "TX_CLSTRX_GENERATE_PPH21_RECONACC_DEL"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim lblDelText As Label
        Dim strAccCode As String
        Dim intErrNo As Integer

        dgReconAcc.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgReconAcc.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAccCode")
        strAccCode = lblDelText.Text

        Try
            strParamName = "ACCCODE|LOCCODE|ACCTYPE"
            strParamValue = strAccCode & "|" & strLocation & "|" & 1

            intErrNo = objOk.mtdInsertDataCommon(strOpCode, _
                                                        strParamName, _
                                                        strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        ViewReconAcc()
    End Sub

    Sub DEDR_DeleteCr(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCode As String = "TX_CLSTRX_GENERATE_PPH21_RECONACC_DEL"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim lblDelText As Label
        Dim strAccCode As String
        Dim intErrNo As Integer

        dgReconAccCr.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgReconAcc.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAccCode")
        strAccCode = lblDelText.Text

        Try
            strParamName = "ACCCODE|LOCCODE|ACCTYPE"
            strParamValue = strAccCode & "|" & strLocation & "|" & 2

            intErrNo = objOk.mtdInsertDataCommon(strOpCode, _
                                                        strParamName, _
                                                        strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        ViewReconAccCr()
    End Sub

    Sub ViewReconAcc()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_RECONACC_GET"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        ParamName = "LOCCODE|ACCTYPE"
        ParamValue = strLocation & "|" & 1

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgReconAcc.DataSource = objDataSet
        dgReconAcc.DataBind()
    End Sub

    Sub ViewReconAccCr()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_RECONACC_GET"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        ParamName = "LOCCODE|ACCTYPE"
        ParamValue = strLocation & "|" & 2

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgReconAccCr.DataSource = objDataSet
        dgReconAccCr.DataBind()
    End Sub

    Sub ViewReconAmt()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_RECONACC_GET_AMOUNT"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|ACCTYPE"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & 1

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgReconAmt.DataSource = objDataSet
        dgReconAmt.DataBind()

        If objDataSet.Tables(0).Rows.Count > 0 Then
            BtnPreviewRecAmt.Visible = True
            cbExcelRecAmt.Visible = True

            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                lbl = dgReconAmt.Items.Item(intCnt).FindControl("lblAccCodeRec")
                If Trim(lbl.Text) = "ZZZZZZ" Then
                    lbl.Visible = False
                    lbl = dgReconAmt.Items.Item(intCnt).FindControl("lblDescRec")
                    lbl.Font.Bold = True
                    lbl = dgReconAmt.Items.Item(intCnt).FindControl("lblAmtRec")
                    lbl.Font.Bold = True
                End If
            Next
        End If
    End Sub

    Sub ViewReconAmtCr()
        Dim strOpCd_DKtr As String = "TX_CLSTRX_GENERATE_PPH21_RECONACC_GET_AMOUNT"
        Dim objDataSet As New Object()
        Dim objPPHDs As New DataSet()
        Dim intErrNo As Integer
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim strMn As String
        Dim strYr As String
        Dim lbl As Label
        Dim intCnt As Integer

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        ParamName = "LOCCODE|ACCMONTH|ACCYEAR|ACCTYPE"
        ParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & 2

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, ParamName, ParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgReconAmtCr.DataSource = objDataSet
        dgReconAmtCr.DataBind()

        If objDataSet.Tables(0).Rows.Count > 0 Then
            BtnPreviewRecAmtCr.Visible = True
            cbExcelRecAmtCr.Visible = True

            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                lbl = dgReconAmtCr.Items.Item(intCnt).FindControl("lblAccCodeRec")
                If Trim(lbl.Text) = "ZZZZZZ" Then
                    lbl.Visible = False
                    lbl = dgReconAmtCr.Items.Item(intCnt).FindControl("lblDescRec")
                    lbl.Font.Bold = True
                    lbl = dgReconAmtCr.Items.Item(intCnt).FindControl("lblAmtRec")
                    lbl.Font.Bold = True
                End If
            Next
        End If
    End Sub

    Sub btnPreviewRecAmt_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlyear.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=PPH21REKON-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgReconAmt.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

    Sub btnPreviewRecAmtCr_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlyear.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=PPH21REKONCR-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgReconAmtCr.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
End Class
